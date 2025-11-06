Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient
Public Class CancelPurchaseInvoiceReport
    Inherits FrmMainTranScreen

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Try
            Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical' "
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtRAL__My_Click(sender As Object, e As EventArgs) Handles TxtRAL._My_Click
        Try
            Dim qry As String = " select Ref_No as TenderNo, Remarks as RefName from TSPL_PI_HEAD_Cancel_Data "
            TxtRAL.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "TenderNo", "", TxtRAL.arrValueMember, TxtRAL.arrDispalyMember)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub TxtRAL__My_Click(sender As Object, e As EventArgs) Handles TxtRAL._My_Click
    '    Try
    '        Dim qry As String = "select Ref_No as TenderNo from TSPL_PI_HEAD_Cancel_Data"
    '        TxtRAL.arrValueMember = clsCommon.ShowMultipleSelectForm(
    '        "TransTypeMulSel",
    '        qry,
    '        "TenderNo",
    '        "",
    '        TxtRAL.arrValueMember,
    '        Nothing
    '    )

    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub


    'Private Sub TxtRAL__My_Click(sender As Object, e As EventArgs) Handles TxtRAL._My_Click
    '    Try
    '        If TxtRAL Is Nothing Then
    '            MessageBox.Show("TxtRAL is not initialized.")
    '            Exit Sub
    '        End If

    '        Dim qry As String = "SELECT DISTINCT Ref_No AS TenderNo FROM TSPL_PI_HEAD_Cancel_Data"
    '        Dim currentValues = If(TxtRAL.arrValueMember, "")

    '        TxtRAL.arrValueMember = clsCommon.ShowMultipleSelectForm(
    '        "TransTypeMulSel",
    '        qry,
    '        "TenderNo",
    '        "",
    '        currentValues,
    '        Nothing
    '    )

    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub


    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = " Select Item_Code as Code,Item_Desc as Name,Short_Description,Structure_Code from TSPL_ITEM_MASTER "
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtVendor__My_Click(sender As Object, e As EventArgs) Handles TxtVendor._My_Click
        Try
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
            TxtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", TxtVendor.arrValueMember, TxtVendor.arrDispalyMember)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        RadPageView1.SelectedPage = RadPageViewPage1
        txtLocation.arrValueMember = Nothing
        TxtRAL.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        TxtVendor.arrValueMember = Nothing
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
    End Sub

    Private Sub CancelPurchaseInvoiceReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE
            txtToDate.Value = clsCommon.GETSERVERDATE
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Close()
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            'If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
            'End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Cancel Purchase Invoice Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Cancel Purchase Invoice Report", gv1, arrHeader, "Cancel Purchase Invoice Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        If gv1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExporttoExcel(EnumExportTo.PDF)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub


    Private Sub LoadData()
        Try
            Dim Whr As String = ""
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Whr += " and TSPL_LOCATION_MASTER.Location_Code In (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If

            If TxtRAL.arrValueMember IsNot Nothing AndAlso TxtRAL.arrValueMember.Count > 0 Then
                Whr += " and TSPL_PI_HEAD_Cancel_Data.Ref_No In (" + clsCommon.GetMulcallString(TxtRAL.arrValueMember) + ")"
            End If

            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                Whr += " and TSPL_ITEM_MASTER.Item_Code In (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
            End If

            If TxtVendor.arrValueMember IsNot Nothing AndAlso TxtVendor.arrValueMember.Count > 0 Then
                Whr += " and TSPL_VENDOR_MASTER.Vendor_Code In (" + clsCommon.GetMulcallString(TxtVendor.arrValueMember) + ")"
            End If


            Dim qry As String = "SELECT DISTINCT TSPL_GRN_HEAD.GRN_No AS [GRN No],
                                CASE  WHEN TSPL_GRN_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_GRN_HEAD.Status = 1 THEN 'Approved' 
	                                ELSE 'Unknown' END AS [GRN Status],
                                TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code as [Weighment No],
                                CASE  WHEN TSPL_PO_WEIGHTMENT_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_PO_WEIGHTMENT_HEAD.Status = 1 THEN 'Approved' 
	                                ELSE 'Unknown' END AS [Weighment Status],
                                TSPL_MRN_HEAD.MRN_No AS [MRN No],
                                CASE  WHEN TSPL_MRN_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_MRN_HEAD.Status = 1 THEN 'Approved' 
	                                ELSE 'Unknown' END AS [MRN Status],
                                TSPL_NIR_QC.Document_No as[NIR QC NO],
                                CASE  WHEN TSPL_NIR_QC.Status = 0 THEN 'Pending' WHEN TSPL_NIR_QC.Status = 1 THEN 'Approved' 
	                                ELSE 'Unknown' END AS [NIR QC Status],
                                TSPL_QC_CHECK_SRN_DETAIL.Document_Code as [Wet Qc No],
								TSPL_QC_CHECK_DETAIL.QC_Status as [Incoming Qc Status],
                                TSPL_SRN_HEAD.SRN_No AS [SRN No],
                                CASE  WHEN TSPL_SRN_HEAD.Status = 0 THEN 'Pending' WHEN TSPL_SRN_HEAD.Status = 1 THEN 'Approved' 
	                                ELSE 'Unknown' END AS [SRN Status],
									TSPL_TENDER_PENALTY_DETAIL.Document_No as [Ral Penalty],
								TSPL_PI_DETAIL_Cancel_Data.PO_ID AS [PO No],
                                TSPL_PI_DETAIL_Cancel_Data.PI_No AS [PI No],
                                CASE  WHEN TSPL_PI_DETAIL_Cancel_Data.Status = 0 THEN 'Pending' WHEN TSPL_PI_DETAIL_Cancel_Data.Status = 1 THEN 'Cancel' 
	                                ELSE 'Unknown' END AS [PI Status],TSPL_LOCATION_MASTER.Location_Code as [Location Code],
                                TSPL_PI_HEAD_Cancel_Data.Ref_No as [Tender],TSPL_ITEM_MASTER.Item_Code as [Item],TSPL_VENDOR_MASTER.Vendor_Code as [Vendor Code]
	                                FROM TSPL_PI_DETAIL_Cancel_Data
									left outer join TSPL_PI_HEAD_Cancel_Data on TSPL_PI_HEAD_Cancel_Data.Against_GRN=TSPL_PI_DETAIL_Cancel_Data.GRN_ID
	                                left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_HEAD_Cancel_Data.PI_No
	                                left outer join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_PI_HEAD_Cancel_Data.Against_GRN
	                                left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No
	                                left outer join TSPL_NIR_QC on TSPL_NIR_QC.MRN_No=TSPL_PI_HEAD_Cancel_Data.Against_MRN
	                                left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.MRN_No=TSPL_PI_HEAD_Cancel_Data.Against_MRN
	                                left outer join TSPL_QC_CHECK_SRN_DETAIL on TSPL_QC_CHECK_SRN_DETAIL.MRN_No=TSPL_PI_HEAD_Cancel_Data.Against_MRN
	                                left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_PI_HEAD_Cancel_Data.Against_SRN
	                                left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_QC_CHECK_SRN_DETAIL.Item_Code
	                                left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PI_HEAD_Cancel_Data.Vendor_Code
	                                left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PO_WEIGHTMENT_HEAD.Location_Code
									left outer join TSPL_QC_CHECK_DETAIL on TSPL_QC_CHECK_DETAIL.Item_Code=TSPL_PI_DETAIL_Cancel_Data.Item_Code
									left outer join TSPL_TENDER_PENALTY_DETAIL on TSPL_TENDER_PENALTY_DETAIL.SRN_No=TSPL_PI_DETAIL_Cancel_Data.SRN_Id
                                    WHERE  (Convert(date,TSPL_PI_HEAD_Cancel_Data.Cancel_On ,103) BETWEEN convert(date,'" + txtFromDate.Value + "',103) and convert(date,'" + txtToDate.Value + "',103)) " + Whr + " "

            Dim dt = clsDBFuncationality.GetDataTable(qry)

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
            gv1.BestFitColumns()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class