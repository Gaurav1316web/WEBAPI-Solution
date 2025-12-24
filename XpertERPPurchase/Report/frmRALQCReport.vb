Imports common
Public Class frmRALQCReport
    Dim strQry As String = Nothing
    Private Sub frmRALQCReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = txtFromDate.Value
            funreset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            funreset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub funreset()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Refresh()
        gv1.MasterTemplate.Refresh()
        gv1.ReadOnly = True
        RadPageView1.SelectedPage = RadPageViewPage1
        txtLocation.arrValueMember = Nothing
        TxtRAL.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        TxtVendor.arrValueMember = Nothing

        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
        EnableDisableCtrl(True)
    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(ReturnSummaryQuery())
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                gv1.ReadOnly = True
                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisableCtrl(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function ReturnSummaryQuery() As String
        strQry = Nothing
        strQry = "select DocumentCode As RAL_Code, Qty as RAL_Qty
,(select Convert(decimal(18,2),sum(TSPL_GRN_DETAIL.GRN_Qty)) from TSPL_PURCHASE_ORDER_HEAD 
LEFT Outer Join TSPL_GRN_HEAD On TSPL_GRN_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No And TSPL_GRN_HEAD.Status=1
LEFT Outer Join TSPL_GRN_DETAIL On TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
where TSPL_PURCHASE_ORDER_HEAD.RefTendorNo=xx.DocumentCode And TSPL_PURCHASE_ORDER_HEAD.Status=1 
) as GRNQty,
(select Convert(decimal(18,2),sum(TSPL_SRN_DETAIL.SRN_Qty)) from TSPL_PURCHASE_ORDER_HEAD 
LEFT Outer Join TSPL_SRN_HEAD On TSPL_SRN_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No And TSPL_SRN_HEAD.Status=1
LEFT Outer Join TSPL_SRN_DETAIL On TSPL_SRN_DETAIL.SRN_No=TSPL_SRN_HEAD.SRN_No
where TSPL_PURCHASE_ORDER_HEAD.RefTendorNo=xx.DocumentCode And TSPL_PURCHASE_ORDER_HEAD.Status=1 
) as SRNQty,
(select Sum(Case When VisualQCStatus<>0 Then 1 Else 0 End) As VisualQC from TSPL_PURCHASE_ORDER_HEAD 
LEFT Outer Join TSPL_GRN_HEAD On TSPL_GRN_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No And TSPL_GRN_HEAD.Status=1
LEFT Outer Join TSPL_GRN_DETAIL On TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
where TSPL_PURCHASE_ORDER_HEAD.RefTendorNo=xx.DocumentCode And TSPL_PURCHASE_ORDER_HEAD.Status=1 
) as VisualQC,
(select COUNT(TSPL_NIR_QC.Document_No) As VisualQC from TSPL_PURCHASE_ORDER_HEAD 
Left Outer join TSPL_MRN_HEAD On TSPL_MRN_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No
LEFT Outer Join TSPL_NIR_QC On TSPL_NIR_QC.MRN_No=TSPL_MRN_HEAD.MRN_No And TSPL_NIR_QC.Status=1
where TSPL_PURCHASE_ORDER_HEAD.RefTendorNo=xx.DocumentCode And TSPL_PURCHASE_ORDER_HEAD.Status=1 
) as NIR_QC,
(Select  COUNT(Distinct TSPL_QC_CHECK_HEAD.Document_Code) As WetChemical_QC from TSPL_PURCHASE_ORDER_HEAD 
Left Outer join TSPL_MRN_HEAD On TSPL_MRN_HEAD.Against_PO=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No
LEFT Outer Join TSPL_QC_CHECK_DETAIL On TSPL_QC_CHECK_DETAIL.MRN_No=TSPL_MRN_HEAD.MRN_No
LEFT Outer join TSPL_QC_CHECK_HEAD On TSPL_QC_CHECK_HEAD.Document_Code=TSPL_QC_CHECK_DETAIL.Document_Code And TSPL_QC_CHECK_HEAD.Posted=1
where TSPL_PURCHASE_ORDER_HEAD.RefTendorNo=xx.DocumentCode And TSPL_PURCHASE_ORDER_HEAD.Status=1 
)WetChemical_QC

from
(
Select  TSPL_TENDER_HEADER.DocumentCode,SUM(TSPL_TENDER_DETAIL.Qty) as Qty from TSPL_TENDER_DETAIL Left Outer Join TSPL_TENDER_HEADER On TSPL_TENDER_HEADER.DocumentCode=TSPL_TENDER_DETAIL.DocumentCode "
        strQry += " where 1=1 "
        strQry += " and convert(Date,TSPL_TENDER_HEADER.DocumentDate,103)>=convert(date,'" & txtFromDate.Value & "',103) and convert(Date,TSPL_TENDER_HEADER.DocumentDate,103)<=convert(date,'" & txtToDate.Value & "',103)"
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strQry += " and TSPL_TENDER_DETAIL.Location In (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If TxtRAL.arrValueMember IsNot Nothing AndAlso TxtRAL.arrValueMember.Count > 0 Then
            strQry += " and TSPL_TENDER_DETAIL.DocumentCode In (" & clsCommon.GetMulcallString(TxtRAL.arrValueMember) & ")"
        End If
        If TxtVendor.arrValueMember IsNot Nothing AndAlso TxtVendor.arrValueMember.Count > 0 Then
            strQry += " and TSPL_TENDER_DETAIL.Vendor_Code In (" & clsCommon.GetMulcallString(TxtVendor.arrValueMember) & ")"
        End If
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            strQry += " and TSPL_TENDER_DETAIL.Item_code In (" & clsCommon.GetMulcallString(txtItem.arrValueMember) & ")"
        End If

        strQry += " group by TSPL_TENDER_HEADER.DocumentCode) xx"
        Return strQry
    End Function


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            ExportToExcel(EnumExportTo.PDF)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & " To " & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            'If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
            'End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("RAL QC Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("RAL QC Report", gv1, arrHeader, "RAL QC Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Try
            Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' "
            If objCommonVar.ApplyLocationFilterBasedOnPermission AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and TSPL_LOCATION_MASTER.Location_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
            txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("@Loc", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtRAL__My_Click(sender As Object, e As EventArgs) Handles TxtRAL._My_Click
        Try
            Dim qry As String = " select DocumentCode As RALNo, Convert(Varchar(10),DocumentDate,103) As [Document Date]  from TSPL_TENDER_Header "
            TxtRAL.arrValueMember = clsCommon.ShowMultipleSelectForm("@RAL", qry, "RALNo", "", TxtRAL.arrValueMember, TxtRAL.arrDispalyMember)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = " Select Item_Code as Code,Item_Desc as Name,Short_Description,Structure_Code from TSPL_ITEM_MASTER "
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("@Item", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtVendor__My_Click(sender As Object, e As EventArgs) Handles TxtVendor._My_Click
        Try
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
            TxtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("@Vendor", qry, "Code", "Name", TxtVendor.arrValueMember, TxtVendor.arrDispalyMember)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class