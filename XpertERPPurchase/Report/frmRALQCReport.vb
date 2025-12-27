Imports common
Imports System.IO
Public Class frmRALQCReport

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
        ReportGrid()
        ReportDetailsGrid()
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage3.Visible = False
        txtLocation.arrValueMember = Nothing
        TxtRAL.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        TxtVendor.arrValueMember = Nothing
        EnableDisableCtrl(True)
    End Sub

    Sub ReportGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.MasterView.Refresh()
        gv1.ReadOnly = True
    End Sub

    Sub ReportDetailsGrid()
        gvDetails.DataSource = Nothing
        gvDetails.Rows.Clear()
        gvDetails.Columns.Clear()
        gvDetails.GroupDescriptors.Clear()
        gvDetails.MasterTemplate.SummaryRowsBottom.Clear()
        gvDetails.MasterView.Refresh()
        gvDetails.ReadOnly = True
    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim strQry As String = "select [RAL Code],(Select Convert(Varchar(10),DocumentDate,103) from TSPL_TENDER_HEADER Where DocumentCode=xx.[RAL Code]) As [RAL Date],(Select SUM(Qty) from TSPL_TENDER_DETAIL Where DocumentCode=xx.[RAL Code]) As [RAL Qty],SUM(ISNULL([GRN Qty],0)) as [GRN Qty], SUM(ISNULL([SRN Qty],0)) as [SRN Qty], SUM([Visual QC]) as [Visual QC],SUM([NIR QC]) AS [NIR QC],COUNT([WET/Chemical QC])As [WET/Chemical QC] from (" & ReturnBaseQry(False) & ")xx group by [RAL Code]"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ReportGrid()
                gv1.DataSource = dt
                gv1.BestFitColumns()
                gv1.EnableFiltering = True
                gv1.ShowFilteringRow = True
                ReStoreGridLayout()
                gv1.ReadOnly = True
                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisableCtrl(False)
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("RAL Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("GRN Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)

                gv1.MasterTemplate.AutoExpandGroups = True
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                gv1.MasterTemplate.ShowTotals = True
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function ReturnBaseQry(ByVal isDoubleClick As Boolean) As String
        Dim strBase As String = "select TSPL_PURCHASE_ORDER_HEAD.RefTendorNo As [RAL Code],TSPL_GRN_DETAIL.GRN_No As [GRN No],Convert(Varchar(10),TSPL_GRN_HEAD.GRN_Date,103) As [GRN Date],Convert(Decimal(18,2),TSPL_GRN_DETAIL.GRN_Qty)[GRN Qty],TSPL_GRN_DETAIL.Unit_code As [GRN UOM],TSPL_GRN_DETAIL.Item_Code As [Item Code],TSPL_GRN_DETAIL.Item_Desc As [Item Desc],TSPL_GRN_HEAD.Vendor_Code As [Vendor Code],TSPL_GRN_HEAD.Vendor_Name As [Vendor Name],TSPL_GRN_HEAD.VehicleNo As [Vehicle No],TSPL_PO_WEIGHTMENT_DETAIL.Gross_Weight As [Gross Weight],TSPL_PO_WEIGHTMENT_DETAIL.Tare_Weight As [Tare Weight],TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight As [Net Weight], case when TSPL_GRN_HEAD.VisualQCStatus>0 and TSPL_GRN_HEAD.VisualQCStatus<5 then 1 else 0 end as [Visual QC] ,
CASE WHEN TSPL_GRN_HEAD.VisualQCStatus = 1 THEN 'OK' WHEN TSPL_GRN_HEAD.VisualQCStatus = 2 THEN 'Not OK' WHEN TSPL_GRN_HEAD.VisualQCStatus = 3 THEN 'Partial OK' WHEN TSPL_GRN_HEAD.VisualQCStatus = 4 THEN 'Hold' ELSE '' END AS [Visual QC Status],TSPL_SRN_DETAIL.SRN_No As [SRN No],Convert(Varchar(10),TSPL_SRN_HEAD.SRN_Date,103) As [SRN Date],Convert(Decimal(18,2),TSPL_SRN_DETAIL.SRN_Qty) As [SRN Qty],TSPL_SRN_DETAIL.Unit_code As [SRN UOM],case when TSPL_NIR_QC.QC_Status>0 and TSPL_NIR_QC.QC_Status<3 then 1 else 0 end as [NIR QC] ,Case When TSPL_NIR_QC.QC_Status =1 Then 'OK' When TSPL_NIR_QC.QC_Status =2 Then 'Not OK' Else '' End As [NIR QC Status], TSPL_QC_CHECK_DETAIL.QC_Status As [WET/Chemical QC] from TSPL_PURCHASE_ORDER_DETAIL
left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No And TSPL_PURCHASE_ORDER_HEAD.Status=1
LEFT Outer Join TSPL_GRN_DETAIL On TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code
LEFT Outer Join TSPL_GRN_HEAD On TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No And TSPL_GRN_HEAD.Status=1 
Left Outer Join TSPL_PO_WEIGHTMENT_HEAD On TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No And TSPL_PO_WEIGHTMENT_HEAD.Status=1
Left Outer Join TSPL_PO_WEIGHTMENT_DETAIL On TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code=TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code
Left Outer Join TSPL_MRN_DETAIL On TSPL_MRN_DETAIL.GRN_Id=TSPL_GRN_HEAD.GRN_No and TSPL_MRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code 
Left Outer Join TSPL_MRN_HEAD On TSPL_MRN_HEAD.MRN_No=TSPL_MRN_DETAIL.MRN_No And TSPL_MRN_HEAD.Status=1
Left Outer Join TSPL_SRN_DETAIL On TSPL_SRN_DETAIL.MRN_Id=TSPL_MRN_HEAD.MRN_No And TSPL_SRN_DETAIL.Item_Code=TSPL_MRN_DETAIL.Item_Code
Left Outer Join TSPL_SRN_HEAD On TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No  And TSPL_SRN_HEAD.Status=1 
Left Outer Join TSPL_NIR_QC On TSPL_NIR_QC.MRN_No=TSPL_MRN_HEAD.MRN_No And TSPL_NIR_QC.Status=1
LEFT Outer Join TSPL_QC_CHECK_DETAIL On TSPL_QC_CHECK_DETAIL.MRN_No=TSPL_MRN_HEAD.MRN_No "
        strBase += " where TSPL_PURCHASE_ORDER_HEAD.RefTendorNo In (Select TSPL_TENDER_HEADER.DocumentCode from TSPL_TENDER_DETAIL
Inner Join TSPL_TENDER_HEADER On TSPL_TENDER_HEADER.DocumentCode=TSPL_TENDER_DETAIL.DocumentCode"
        strBase += " and convert(Date,TSPL_TENDER_HEADER.DocumentDate,103)>=convert(date,'" & txtFromDate.Value & "',103) and convert(Date,TSPL_TENDER_HEADER.DocumentDate,103)<=convert(date,'" & txtToDate.Value & "',103)"
        strBase += " And (IsNull(TSPL_GRN_HEAD.IsCancel,0)=0 OR TSPL_SRN_HEAD.IsCancel=0) "
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strBase += " and TSPL_TENDER_DETAIL.Location In (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
        End If
        If isDoubleClick Then
            strBase += " and TSPL_TENDER_DETAIL.DocumentCode = ('" & clsCommon.myCstr(gv1.CurrentRow.Cells("RAL Code").Value) & "')"
        ElseIf TxtRAL.arrValueMember IsNot Nothing AndAlso TxtRAL.arrValueMember.Count > 0 Then
            strBase += " and TSPL_TENDER_DETAIL.DocumentCode In (" & clsCommon.GetMulcallString(TxtRAL.arrValueMember) & ")"
        End If
        If TxtVendor.arrValueMember IsNot Nothing AndAlso TxtVendor.arrValueMember.Count > 0 Then
            strBase += " and TSPL_TENDER_DETAIL.Vendor_Code In (" & clsCommon.GetMulcallString(TxtVendor.arrValueMember) & ")"
        End If
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            strBase += " and TSPL_TENDER_DETAIL.Item_code In (" & clsCommon.GetMulcallString(txtItem.arrValueMember) & ")"
        End If
        strBase += ")"
        Return strBase
    End Function

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel, gv1)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            ExportToExcel(EnumExportTo.PDF, gv1)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcelDetail_Click(sender As Object, e As EventArgs) Handles rmiExcelDetail.Click
        Try
            ExportToExcel(EnumExportTo.Excel, gvDetails)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDFDetail_Click(sender As Object, e As EventArgs) Handles rmiPDFDetail.Click
        Try
            ExportToExcel(EnumExportTo.PDF, gvDetails)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo, ByVal gv As RadGridView)
        Try
            If gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim strtemp As String = "Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & " To " & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                arrHeader.Add(strtemp)
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                'If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                '    arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
                'End If
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("RAL QC Report", gv, arrHeader, Me.Text, True)
                Else
                    clsCommon.MyExportToPDF("RAL QC Report", gv, arrHeader, "RAL QC Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtVendor__My_Click(sender As Object, e As EventArgs) Handles TxtVendor._My_Click
        Try
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
            TxtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("@Vendor", qry, "Code", "Name", TxtVendor.arrValueMember, TxtVendor.arrDispalyMember)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            Dim strQry As String = "Select [RAL Code],[Vendor Code],[Vendor Name],[Vehicle No],[Item Code],[Item Desc],[GRN No],[GRN Date],[GRN Qty],[GRN UOM],[Gross Weight],[Tare Weight],[Net Weight],[SRN No],[SRN Qty],[SRN UOM],[SRN Date],[Visual QC Status],[NIR QC Status],[WET/Chemical QC] from (" & ReturnBaseQry(True) & ")xyz"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ReportDetailsGrid()
                gvDetails.DataSource = dt
                gvDetails.BestFitColumns()
                gvDetails.EnableFiltering = True
                gvDetails.ShowFilteringRow = True
                ReStoreGridLayout()
                RadPageView1.SelectedPage = RadPageViewPage3
                RadPageViewPage3.Visible = True

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("GRN Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("Gross Weight", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("Tare Weight", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                Dim item4 As New GridViewSummaryItem("Net Weight", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item4)
                Dim item5 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item5)

                gvDetails.ShowGroupPanel = False
                gvDetails.MasterTemplate.AutoExpandGroups = True
                gvDetails.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gvDetails.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                gvDetails.MasterTemplate.ShowTotals = True
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvDetails_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gvDetails.RowFormatting
        Try
            Dim qcStatus As String = Convert.ToString(e.RowElement.RowInfo.Cells("WET/Chemical QC").Value)
            ' 🔑 Reset first
            e.RowElement.ResetValue(Telerik.WinControls.UI.LightVisualElement.BackColorProperty, Telerik.WinControls.ValueResetFlags.Local)
            e.RowElement.ResetValue(Telerik.WinControls.UI.LightVisualElement.GradientStyleProperty, Telerik.WinControls.ValueResetFlags.Local)
            Select Case qcStatus
                Case "Accepted"
                    e.RowElement.BackColor = Color.LightGreen
                Case "Under Deviation"
                    e.RowElement.BackColor = Color.Orange
                Case "Rejected"
                    e.RowElement.BackColor = Color.Red
                Case Else
                    e.RowElement.BackColor = Color.White
            End Select
            ' 🔒 Hover & selection ko override karo
            e.RowElement.DrawFill = True
            e.RowElement.DrawBorder = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 AndAlso clsCommon.myLen(PageSetupReport_ID & "_RS") > 0 Then
                gv1.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = PageSetupReport_ID & "_RS"
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gv1.SaveLayout(obj.GridLayout)
                obj.GridColumns = gv1.ColumnCount
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
                End If
                ''stuti regarding memory leakage
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
            If gvDetails IsNot Nothing AndAlso gvDetails.Rows.Count > 0 AndAlso clsCommon.myLen(PageSetupReport_ID & "_RD") > 0 Then
                gvDetails.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = PageSetupReport_ID & "_RD"
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gvDetails.SaveLayout(obj.GridLayout)
                obj.GridColumns = gvDetails.ColumnCount
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
                End If
                ''stuti regarding memory leakage
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 AndAlso clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID & "_RS", "", objCommonVar.CurrentUserCode), clsGridLayout)
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

            If gvDetails IsNot Nothing AndAlso gvDetails.Rows.Count > 0 AndAlso clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID & "_RD", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvDetails.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvDetails.Columns.Count - 1 Step ii + 1
                        gvDetails.Columns(ii).IsVisible = False
                        gvDetails.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvDetails.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            clsGridLayout.DeleteData(PageSetupReport_ID & "_RS", objCommonVar.CurrentUserCode)
            clsGridLayout.DeleteData(PageSetupReport_ID & "_RD", objCommonVar.CurrentUserCode)
            clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmRALQCReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.E Then
                Dim strQry As String = "Select 'Export Summary' As [Export Excel] Union All Select 'Export Details' As [Export Excel]"
                Dim frm As New FrmFreeComboBox()
                frm.ComboSource = clsDBFuncationality.GetDataTable(strQry)
                frm.ComboValueMember = "Export Excel"
                frm.ComboDisplayMember = "Export Excel"
                frm.ShowDialog()
                If clsCommon.CompairString(frm.strRetValue, "Export Summary") = CompairStringResult.Equal Then
                    ExportToExcel(EnumExportTo.Excel, gv1)
                ElseIf clsCommon.CompairString(frm.strRetValue, "Export Details") = CompairStringResult.Equal Then
                    ExportToExcel(EnumExportTo.Excel, gvDetails)
                End If
                frm = Nothing
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Close()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class