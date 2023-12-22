Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

' Ticket No :  by prabhakar - Create new report 
Public Class rptPendingQCReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim ItemWiseQualityCheckInGeneralPurchase As Boolean = False
    Dim strFormCode = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        strFormCode = MyBase.Form_ID
        ItemWiseQualityCheckInGeneralPurchase = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemWiseQualityCheckInGeneralPurchase, clsFixedParameterCode.ItemWiseQualityCheckInGeneralPurchase, Nothing)) = 1, True, False)
        If ItemWiseQualityCheckInGeneralPurchase Then
            strFormCode = strFormCode + "GPQ"
            chkQCPending.Visible = False
        Else
            chkQCPending.Visible = True
        End If
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        Reset()
    End Sub
    Sub Reset()
        ' ToDate.Value = clsCommon.GETSERVERDATE()
        ' fromDate.Value = ToDate.Value.AddMonths(-1)
        txtVendor.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try

            PageSetupReport_ID = strFormCode ' MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable

            Dim whr As String = " "
            If ItemWiseQualityCheckInGeneralPurchase = True Then
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    whr += " and TSPL_QC_CHECK_HEAD.Bill_To_location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                Else
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        whr += " and TSPL_QC_CHECK_HEAD.Bill_To_location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                End If
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    whr += " and TSPL_QC_CHECK_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
                End If
                'If chkQCPending.Checked = True Then
                '    whr += " and TBL_SRN .SRN_NO is null and TSPL_MRN_Head.Status = 1  and isnull (TSPL_QC_CHECK_HEAD.Posted,0) = 0 "
                'End If

                qry = " select TSPL_QC_CHECK_HEAD.Document_Code as [Document Code], convert (varchar,TSPL_QC_CHECK_HEAD.Document_Date,103) as [Document Date],TSPL_QC_CHECK_SRN_DETAIL.MRN_No as [MRN No],TSPL_QC_CHECK_HEAD.Vendor_Code as [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],TSPL_QC_CHECK_HEAD.Bill_To_location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Desc] ,TSPL_QC_CHECK_HEAD.QC_Status as [QC Status] , TSPL_QC_CHECK_SRN_DETAIL.Item_Code as [Item Code],tspl_item_master.item_desc as [Item Desc], TSPL_QC_CHECK_SRN_DETAIL.Unit_Code as [Unit], TSPL_QC_CHECK_SRN_DETAIL.MRN_Qty as [MRN Qty], TSPL_QC_CHECK_SRN_DETAIL.Param_Status as [Param Status], TSPL_QC_CHECK_SRN_DETAIL.Ok_Qty as [OK Qty], TSPL_QC_CHECK_SRN_DETAIL.Reject_Qty as [Reject Qty], TSPL_QC_CHECK_SRN_DETAIL.Measured_1 as [Measured], TSPL_QC_CHECK_SRN_DETAIL.Net_Measurement as [Net Measurement], TSPL_QC_CHECK_SRN_DETAIL.PO_No as [PO No] ,TSPL_QC_CHECK_SRN_DETAIL.InputData , TSPL_QC_CHECK_SRN_DETAIL.InputDataDeductionPer as [Deduction%] ,TSPL_QC_CHECK_SRN_DETAIL.Remarks, TSPL_QC_CHECK_SRN_DETAIL.Reject_Remarks as [Reject Remarks] 
                        ,tspl_qc_log_sheet_master.description as param_desc 
                        from TSPL_QC_CHECK_SRN_DETAIL 
                        left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Document_Code = TSPL_QC_CHECK_SRN_DETAIL.Document_Code
                        left outer join tspl_item_master on tspl_item_master.item_code=TSPL_QC_CHECK_SRN_DETAIL.item_code 
                        left outer join tspl_qc_log_sheet_master on tspl_qc_log_sheet_master.code=TSPL_QC_CHECK_SRN_DETAIL.qc_param_code and tspl_qc_log_sheet_master.trans_id='standard' 
                        left outer join TSPL_LOCATION_MASTER on Location_Code = TSPL_QC_CHECK_HEAD.Bill_To_location
                        left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_QC_CHECK_HEAD.Vendor_Code
                        where convert (date, TSPL_QC_CHECK_HEAD.Document_Date ,103) >= convert (date,'" + fromDate.Value + "',103) and convert (date, TSPL_QC_CHECK_HEAD.Document_Date ,103) <= convert (date,'" + ToDate.Value + "',103) " + whr + "  order by convert (datetime,TSPL_QC_CHECK_HEAD.Document_Date,103) , TSPL_QC_CHECK_HEAD.Document_Code asc  "
            Else
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    whr += " and TSPL_MRN_DETAIL.Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
                Else
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        whr += " and TSPL_MRN_DETAIL.Location in (" + objCommonVar.strCurrUserLocations + ")"
                    End If
                End If
                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    whr += " and TSPL_MRN_Head.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")"
                End If
                If chkQCPending.Checked = True Then
                    whr += " and TBL_SRN .SRN_NO is null and TSPL_MRN_Head.Status = 1  and isnull (TSPL_QC_CHECK_HEAD.Posted,0) = 0 "
                End If

                qry = "  Select  Convert (varchar, TSPL_MRN_Head .MRN_DATE,103) as [MRN Date] ,TSPL_MRN_DETAIL.MRN_NO as [MRN Document] , TSPL_MRN_Head.Vendor_Code as [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],Case when  TSPL_MRN_Head.Status = 0 then 'Pending' else 'Posted' end as  [MRN Status],TSPL_MRN_DETAIL.Item_Code as [Item Code],TSPL_ITEM_MASTER.ITEM_DESC as [Item Name] ,TSPL_MRN_DETAIL.Unit_Code as [Item UOM],Convert (decimal(18,2),TSPL_MRN_DETAIL.MRN_QTY) as [Item Qty],TSPL_MRN_Head.Invoice_No as [Invoice No] , case when len(TSPL_MRN_Head.Invoice_No ) > 0 then  Convert (varchar, TSPL_MRN_Head.Invoice_Date,103) else '' end as [Invoice Date] ,   " &
                      "  TSPL_MRN_DETAIL.Location as [Location Code],TSPL_Location_Master.Location_Desc as [Location Name], TSPL_MRN_Head.Against_GRN as [GRN No] , Convert (varchar, TSPL_GRN_HEAD.GRN_DATE,103) as [GRN Date] ,  " &
                      "  TSPL_QC_CHECK_DETAIL.Document_Code as [QC Code] , convert (varchar, TSPL_QC_CHECK_HEAD.Document_Date,103) as [QC Date] , TSPL_QC_CHECK_DETAIL.QC_Status as [QC Status], Case when  isnull (TSPL_QC_CHECK_HEAD.Posted,0) =0 then 'No' else 'Yes' end as [QC Posted] , TBL_SRN.SRN_No as [SRN No]  " &
                      "  from TSPL_MRN_DETAIL " &
                      "  left outer join TSPL_MRN_Head on TSPL_MRN_Head.mrn_no=TSPL_MRN_DETAIL.MRN_No  " &
                      "  left join TSPL_GRN_HEAD  on TSPL_GRN_HEAD.GRN_No=TSPL_MRN_Head.Against_GRN   " &
                      "  Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE = TSPL_MRN_DETAIL.ITEM_CODE  " &
                      "  Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MRN_Head.Vendor_Code  " &
                      "  Left Outer Join TSPL_Location_Master on TSPL_Location_Master.Location_Code = TSPL_MRN_DETAIL.Location  " &
                      "  Left Outer Join TSPL_QC_CHECK_DETAIL on TSPL_QC_CHECK_DETAIL.MRN_No = TSPL_MRN_DETAIL.MRN_NO  and TSPL_QC_CHECK_DETAIL.Item_Code = TSPL_MRN_DETAIL.Item_Code  " &
                      "  Left Outer Join TSPL_QC_CHECK_HEAD  on TSPL_QC_CHECK_HEAD.Document_Code = TSPL_QC_CHECK_DETAIL.Document_Code " &
                      "  Left Outer Join (Select TSPL_SRN_DETAIL.Item_Code ,TSPL_SRN_DETAIL.SRN_No,TSPL_SRN_HEAD.Against_MRN  from TSPL_SRN_DETAIL Left Outer Join TSPL_SRN_HEAD   on TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_No ) TBL_SRN on TBL_SRN.Against_MRN = TSPL_MRN_DETAIL.MRN_No and TBL_SRN.Item_Code = TSPL_MRN_DETAIL.Item_Code " &
                      "  where TSPL_MRN_DETAIL.Item_Code in (select distinct TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER.Item_Code from TSPL_ITEM_MASTER_PURCHASE_QC_PARAMETER) and TSPL_MRN_DETAIL.QC_Check=1  " &
                      "  and TSPL_MRN_Head.Against_GRN is not null " &
                      "  and convert(date,TSPL_MRN_Head.MRN_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_MRN_Head.MRN_Date,103)<=convert(date,'" + ToDate.Value + "',103) " &
                      "  " + whr + " " &
                      "    "
            End If

            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                ' Gv1.Columns("Trans Type").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.EnableFiltering = True
                Gv1.BestFitColumns()
                ' Gv1.Columns("Qty in Stocking UOM").FormatString = "{0:n2}"
                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim itemQty As New GridViewSummaryItem("Qty in Stocking UOM", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(itemQty)
                'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(strFormCode) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub


    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = strFormCode ' MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(strFormCode, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPendingQCReport & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If

            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("QC Status Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("QC Status Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = " Select TSPL_VENDOR_MASTER.Vendor_Code as Code , TSPL_VENDOR_MASTER.Vendor_Name as Name from TSPL_VENDOR_MASTER  "
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("QCStatusReport@VendorFinder", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            If Gv1.CurrentRow.Index >= 0 Then
                If e.Column Is Gv1.Columns("MRN Document") Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnMRN, clsCommon.myCstr(Gv1.CurrentRow.Cells("MRN Document").Value))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
