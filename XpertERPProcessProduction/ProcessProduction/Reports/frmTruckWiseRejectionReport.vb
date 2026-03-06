Imports common
Public Class frmTruckWiseRejectionReport
    Private Sub frmTruckWiseRejectionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = txtFromDate.Value
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        Try
            BlankRejectionGrid()
            RadPageViewPage2.Text = "Report"
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub BlankRejectionGrid()
        gvRejection.DataSource = Nothing
        gvRejection.Rows.Clear()
        gvRejection.GroupDescriptors.Clear()
        gvRejection.SummaryRowsBottom.Clear()
        gvRejection.Refresh()
        gvRejection.MasterTemplate.Refresh()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Function BaseQry() As String
        Try
            Dim Qry As String = "  select  
 TSPL_GRN_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add4,
 TSPL_GRN_HEAD.GRN_No,(TSPL_GRN_HEAD.GRN_Date) As GRN_Date,
 TSPL_GRN_HEAD.Ref_No,TSPL_GRN_HEAD.VehicleNo,TSPL_GRN_HEAD.Vendor_Code,TSPL_GRN_HEAD.Vendor_Name,TSPL_GRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description as 'Item_Desc',
 TSPL_GRN_HEAD.IsCancel,(TSPL_GRN_DETAIL.GRN_Qty) As GRN_Qty, (TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight) As Net_Weight,
Case When TSPL_GRN_HEAD.IsCancel=1 Then TSPL_GRN_DETAIL.GRN_Qty
	  When TSPL_GRN_HEAD.VisualQCStatus=1 And TSPL_NIR_QC.QC_Status=1 And TSPL_QC_CHECK_DETAIL.QC_Status='Accepted'  Then	TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight - TSPL_SRN_DETAIL.SRN_Qty		       
      Else
		TSPL_GRN_DETAIL.GRN_Qty-TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight
 End As TotalRejectQty,
 (TSPL_GRN_HEAD.VisualQCUpdatedDate) As VisualQCUpdatedDate,(TSPL_GRN_HEAD.VisualQCUpdatedDateSecond) As VisualQCUpdatedDateSecond,
CASE 
 WHEN TSPL_QC_CHECK_HEAD.QC_Status='Rejected' Then TSPL_QC_CHECK_HEAD.Document_Date 
 When TSPL_NIR_QC.QC_Status=2 Then TSPL_NIR_QC.Document_Date
 When TSPL_GRN_HEAD.VisualQCStatusSecond=2 Then TSPL_GRN_HEAD.VisualQCUpdatedDateSecond
 When TSPL_GRN_HEAD.VisualQCStatus=2 THEN TSPL_GRN_HEAD.VisualQCUpdatedDate
 WHEN TSPL_GRN_HEAD.VisualQCStatus=3  Then TSPL_GRN_HEAD.VisualQCUpdatedDate
 When TSPL_GRN_HEAD.VisualQCStatusSecond=3 Then TSPL_GRN_HEAD.VisualQCUpdatedDateSecond     	 
End As [RejectionDate],
CASE 
 When TSPL_GRN_HEAD.VisualQCStatus=1 And TSPL_NIR_QC.QC_Status=1 And TSPL_QC_CHECK_DETAIL.QC_Status='Accepted' And  TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight > TSPL_SRN_DETAIL.SRN_Qty  Then 'PARTIAL' 
 When TSPL_GRN_HEAD.VisualQCStatus=1 And TSPL_GRN_HEAD.VisualQCStatusSecond=3 And TSPL_GRN_DETAIL.GRN_Qty>TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight Then 'PARTIAL'
 WHEN TSPL_QC_CHECK_HEAD.QC_Status='Rejected' OR TSPL_NIR_QC.QC_Status=2 OR TSPL_GRN_HEAD.VisualQCStatusSecond=2 OR TSPL_GRN_HEAD.VisualQCStatus=2 THEN 'FULL' 
 WHEN TSPL_GRN_HEAD.VisualQCStatus=3  OR TSPL_GRN_HEAD.VisualQCStatusSecond=3 THEN 'PARTIAL'     	 
End As [Status],
CASE 
 WHEN TSPL_QC_CHECK_HEAD.QC_Status='Rejected' Then TSPL_QC_CHECK_DETAIL.Additional_Remarks 
 When TSPL_NIR_QC.QC_Status=2 Then TSPL_NIR_QC.QC_Remarks
 When TSPL_GRN_HEAD.VisualQCStatusSecond=2 Then TSPL_GRN_HEAD.VisualQCRemarksSecond
 When TSPL_GRN_HEAD.VisualQCStatus=2 THEN TSPL_GRN_HEAD.VisualQCRemarks
 WHEN TSPL_GRN_HEAD.VisualQCStatus=3  Then TSPL_GRN_HEAD.VisualQCRemarks
 When TSPL_GRN_HEAD.VisualQCStatusSecond=3 Then TSPL_GRN_HEAD.VisualQCRemarksSecond     	 
End As [StatusRemark],
TSPL_GRN_HEAD.VisualQCStatus,TSPL_GRN_HEAD.VisualQCStatusSecond,TSPL_NIR_QC.QC_Status As NIR_QC_Status,TSPL_QC_CHECK_HEAD.QC_Status As Wet_QC_Status	
from  TSPL_GRN_HEAD
left join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No
LEFT join TSPL_PO_WEIGHTMENT_HEAD On TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No= TSPL_GRN_HEAD.GRN_No
LEFT join TSPL_PO_WEIGHTMENT_DETAIL On TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL.Item_Code
left Outer Join TSPL_MRN_HEAD On TSPL_MRN_HEAD.Against_GRN=TSPL_GRN_HEAD.GRN_No
Left Outer join TSPL_NIR_QC On TSPL_NIR_QC.MRN_No=TSPL_MRN_HEAD.MRN_No
Left Outer Join TSPL_SRN_DETAIL On TSPL_SRN_DETAIL.GRN_ID=TSPL_GRN_HEAD.GRN_No And TSPL_SRN_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code And TSPL_SRN_DETAIL.Unit_code=TSPL_ITEM_MASTER.Unit_Code
left OUTER join TSPL_QC_CHECK_HEAD ON TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_GRN_HEAD.GRN_No
Left Outer Join TSPL_QC_CHECK_DETAIL On TSPL_QC_CHECK_DETAIL.Document_Code=TSPL_QC_CHECK_HEAD.Document_Code
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GRN_HEAD.Bill_To_Location
left outer join (select Document_Code,sum(InputDataDeductionPer) as deduction from TSPL_QC_CHECK_sRN_DETAIL 
group by Document_Code) as TSPL_QC_CHECK_SRN_DETAIL on TSPL_QC_CHECK_SRN_DETAIL.Document_Code=TSPL_QC_CHECK_HEAD.Document_Code
where 2=2 And Convert(Date,TSPL_GRN_HEAD.GRN_Date,103)>=Convert(Date,'" & txtFromDate.Value & "',103) 
          And Convert(Date,TSPL_GRN_HEAD.GRN_Date,103)<=Convert(Date,'" & txtToDate.Value & "',103) "

            Qry &= " Union All "

            Qry &= " select  
 TSPL_GRN_HEAD_Cancel_Data.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add4,
 TSPL_GRN_HEAD_Cancel_Data.GRN_No,(TSPL_GRN_HEAD_Cancel_Data.GRN_Date) As GRN_Date,
 TSPL_GRN_HEAD_Cancel_Data.Ref_No,TSPL_GRN_HEAD_Cancel_Data.VehicleNo,TSPL_GRN_HEAD_Cancel_Data.Vendor_Code,TSPL_GRN_HEAD_Cancel_Data.Vendor_Name,TSPL_GRN_DETAIL_Cancel_Data.Item_Code,TSPL_ITEM_MASTER.Short_Description as 'Item_Desc',
 TSPL_GRN_HEAD_Cancel_Data.IsCancel,
 (TSPL_GRN_DETAIL_Cancel_Data.GRN_Qty) As GRN_Qty, (TSPL_PO_WEIGHTMENT_DETAIL_Cancel_Data.Net_Weight) As Net_Weight,
Case When TSPL_GRN_HEAD_Cancel_Data.IsCancel=1 Then TSPL_GRN_DETAIL_Cancel_Data.GRN_Qty
	  When TSPL_GRN_HEAD_Cancel_Data.VisualQCStatus=1 And TSPL_NIR_QC_Cancel_Data.QC_Status=1 And TSPL_QC_CHECK_DETAIL_Cancel_Data.QC_Status='Accepted'  Then	TSPL_PO_WEIGHTMENT_DETAIL_Cancel_Data.Net_Weight - TSPL_SRN_DETAIL_Cancel_Data.SRN_Qty		       
      Else
		TSPL_GRN_DETAIL_Cancel_Data.GRN_Qty-TSPL_PO_WEIGHTMENT_DETAIL_Cancel_Data.Net_Weight
 End As TotalRejectQty,
 (TSPL_GRN_HEAD_Cancel_Data.VisualQCUpdatedDate) As VisualQCUpdatedDate,(TSPL_GRN_HEAD_Cancel_Data.VisualQCUpdatedDateSecond) As VisualQCUpdatedDateSecond,
CASE 
 WHEN TSPL_QC_CHECK_HEAD_Cancel_Data.QC_Status='Rejected' Then TSPL_QC_CHECK_HEAD_Cancel_Data.Document_Date 
 When TSPL_NIR_QC_Cancel_Data.QC_Status=2 Then TSPL_NIR_QC_Cancel_Data.Document_Date
 When TSPL_GRN_HEAD_Cancel_Data.VisualQCStatusSecond=2 Then TSPL_GRN_HEAD_Cancel_Data.VisualQCUpdatedDateSecond
 When TSPL_GRN_HEAD_Cancel_Data.VisualQCStatus=2 THEN TSPL_GRN_HEAD_Cancel_Data.VisualQCUpdatedDate
 WHEN TSPL_GRN_HEAD_Cancel_Data.VisualQCStatus=3  Then TSPL_GRN_HEAD_Cancel_Data.VisualQCUpdatedDate
 When TSPL_GRN_HEAD_Cancel_Data.VisualQCStatusSecond=3 Then TSPL_GRN_HEAD_Cancel_Data.VisualQCUpdatedDateSecond     	 
End As [RejectionDate],
CASE 
 When TSPL_GRN_HEAD_Cancel_Data.VisualQCStatus=1 And TSPL_NIR_QC_Cancel_Data.QC_Status=1 And TSPL_QC_CHECK_DETAIL_Cancel_Data.QC_Status='Accepted' And  TSPL_PO_WEIGHTMENT_DETAIL_Cancel_Data.Net_Weight > TSPL_SRN_DETAIL_Cancel_Data.SRN_Qty  Then 'PARTIAL' 
 When TSPL_GRN_HEAD_Cancel_Data.VisualQCStatus=1 And TSPL_GRN_HEAD_Cancel_Data.VisualQCStatusSecond=3 And TSPL_GRN_DETAIL_Cancel_Data.GRN_Qty>TSPL_PO_WEIGHTMENT_DETAIL_Cancel_Data.Net_Weight Then 'PARTIAL'
 WHEN TSPL_QC_CHECK_HEAD_Cancel_Data.QC_Status='Rejected' OR TSPL_NIR_QC_Cancel_Data.QC_Status=2 OR TSPL_GRN_HEAD_Cancel_Data.VisualQCStatusSecond=2 OR TSPL_GRN_HEAD_Cancel_Data.VisualQCStatus=2 THEN 'FULL' 
 WHEN TSPL_GRN_HEAD_Cancel_Data.VisualQCStatus=3  OR TSPL_GRN_HEAD_Cancel_Data.VisualQCStatusSecond=3 THEN 'PARTIAL'     	 
End As [Status],
CASE 
 WHEN TSPL_QC_CHECK_HEAD_Cancel_Data.QC_Status='Rejected' Then TSPL_QC_CHECK_DETAIL_Cancel_Data.Additional_Remarks 
 When TSPL_NIR_QC_Cancel_Data.QC_Status=2 Then TSPL_NIR_QC_Cancel_Data.QC_Remarks
 When TSPL_GRN_HEAD_Cancel_Data.VisualQCStatusSecond=2 Then TSPL_GRN_HEAD_Cancel_Data.VisualQCRemarksSecond
 When TSPL_GRN_HEAD_Cancel_Data.VisualQCStatus=2 THEN TSPL_GRN_HEAD_Cancel_Data.VisualQCRemarks
 WHEN TSPL_GRN_HEAD_Cancel_Data.VisualQCStatus=3  Then TSPL_GRN_HEAD_Cancel_Data.VisualQCRemarks
 When TSPL_GRN_HEAD_Cancel_Data.VisualQCStatusSecond=3 Then TSPL_GRN_HEAD_Cancel_Data.VisualQCRemarksSecond     	 
End As [StatusRemark],
TSPL_GRN_HEAD_Cancel_Data.VisualQCStatus,TSPL_GRN_HEAD_Cancel_Data.VisualQCStatusSecond,TSPL_NIR_QC_Cancel_Data.QC_Status As NIR_QC_Status,TSPL_QC_CHECK_HEAD_Cancel_Data.QC_Status As Wet_QC_Status	
from  TSPL_GRN_HEAD_Cancel_Data
left join TSPL_GRN_DETAIL_Cancel_Data on TSPL_GRN_DETAIL_Cancel_Data.GRN_No=TSPL_GRN_HEAD_Cancel_Data.GRN_No
LEFT join TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data On TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Against_GRN_No= TSPL_GRN_HEAD_Cancel_Data.GRN_No
LEFT join TSPL_PO_WEIGHTMENT_DETAIL_Cancel_Data On TSPL_PO_WEIGHTMENT_DETAIL_Cancel_Data.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD_Cancel_Data.Weighment_Code
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_GRN_DETAIL_Cancel_Data.Item_Code
left Outer Join TSPL_MRN_HEAD_Cancel_Data On TSPL_MRN_HEAD_Cancel_Data.Against_GRN=TSPL_GRN_HEAD_Cancel_Data.GRN_No
Left Outer join TSPL_NIR_QC_Cancel_Data On TSPL_NIR_QC_Cancel_Data.MRN_No=TSPL_MRN_HEAD_Cancel_Data.MRN_No
Left Outer Join TSPL_SRN_DETAIL_Cancel_Data On TSPL_SRN_DETAIL_Cancel_Data.GRN_ID=TSPL_GRN_HEAD_Cancel_Data.GRN_No And TSPL_SRN_DETAIL_Cancel_Data.Item_Code=TSPL_ITEM_MASTER.Item_Code And TSPL_SRN_DETAIL_Cancel_Data.Unit_code=TSPL_ITEM_MASTER.Unit_Code
left OUTER join TSPL_QC_CHECK_HEAD_Cancel_Data ON TSPL_QC_CHECK_HEAD_Cancel_Data.Gate_Entry_No=TSPL_GRN_HEAD_Cancel_Data.GRN_No
Left Outer Join TSPL_QC_CHECK_DETAIL_Cancel_Data On TSPL_QC_CHECK_DETAIL_Cancel_Data.Document_Code=TSPL_QC_CHECK_HEAD_Cancel_Data.Document_Code
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GRN_HEAD_Cancel_Data.Bill_To_Location
left outer join (select Document_Code,sum(InputDataDeductionPer) as deduction from TSPL_QC_CHECK_SRN_DETAIL_Hist_Data 
group by Document_Code) as TSPL_QC_CHECK_sRN_Hist_Data on TSPL_QC_CHECK_sRN_Hist_Data.Document_Code=TSPL_QC_CHECK_HEAD_Cancel_Data.Document_Code
where 2=2 And Convert(Date,TSPL_GRN_HEAD_Cancel_Data.GRN_Date,103)>=Convert(Date,'" & txtFromDate.Value & "',103) 
          And Convert(Date,TSPL_GRN_HEAD_Cancel_Data.GRN_Date,103)<=Convert(Date,'" & txtToDate.Value & "',103)"

            Return Qry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData(False)
    End Sub

    Sub LoadData(ByVal isPrint As Boolean)
        Try
            Dim qry As String = "Select ROW_NUMBER() OVER (ORDER BY GRN_Date) AS 'S.No',Location_Desc As Location,
 Convert(Varchar(10),GRN_Date,103) As [Date Of Arrival],Ref_No As [RM No.],Item_Desc As [Name Of Raw Material],VehicleNo As [Truck No.],Convert(Decimal(18,2),GRN_Qty) As [Total Qty],Convert(Decimal(18,2),TotalRejectQty) As [Rejected Qty], Convert(Varchar(10),RejectionDate,103) As [Date of Rejection],Vendor_Name As [Name of Supplier],Status As [Fully Rejected/Partially Rejected],StatusRemark As [Reason of Rejection]"
            If isPrint Then
                qry &= " ,Format(GRN_Date,'MMMM-yyyy') As MonthYear,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_STATE_MASTER.STATE_NAME,'" & objCommonVar.CurrentUser & "' As PrintBy "
            End If
            qry &= " from (" & BaseQry() & ")xyz "
            If isPrint Then
                qry &= " Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" & objCommonVar.CurrComp_Code1 & "'
Left Outer Join TSPL_STATE_MASTER On TSPL_STATE_MASTER.STATE_CODE=TSPL_COMPANY_MASTER.State "
            End If
            qry &= " Where IsNull(Ref_No,'')<>'' And Status Is Not Null  Order By GRN_Date,Bill_To_Location,Ref_No "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If isPrint Then
                    Dim frm As New frmCrystalReportViewer()
                    frm.funreport(Me.Text, CrystalReportFolder.SalesReport, dt, "crptTruckWiseRejectionReport", "Truck Wise Rejection Report")
                    frm = Nothing
                Else
                    BlankRejectionGrid()
                    gvRejection.AllowAddNewRow = False
                    gvRejection.AllowDragToGroup = False
                    gvRejection.DataSource = dt
                    gvRejection.BestFitColumns()
                    RadPageView1.SelectedPage = RadPageViewPage2
                    RadPageViewPage2.Text = "Rejection Report"
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        LoadData(True)
    End Sub
End Class