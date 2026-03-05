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
 TSPL_GRN_HEAD.IsCancel,
 (TSPL_GRN_DETAIL.GRN_Qty) As GRN_Qty, (TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight) As Net_Weight,
 Case When TSPL_GRN_HEAD.IsCancel=1 Then TSPL_GRN_DETAIL.GRN_Qty Else TSPL_GRN_DETAIL.GRN_Qty-TSPL_PO_WEIGHTMENT_DETAIL.Net_Weight End As TotalRejectQty,
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
 WHEN TSPL_QC_CHECK_HEAD.QC_Status='Rejected' OR TSPL_NIR_QC.QC_Status=2 OR TSPL_GRN_HEAD.VisualQCStatusSecond=2 OR TSPL_GRN_HEAD.VisualQCStatus=2 THEN 'FULL' 
 WHEN TSPL_GRN_HEAD.VisualQCStatus=3  OR TSPL_GRN_HEAD.VisualQCStatusSecond=3 THEN 'Partial'     	 
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
left OUTER join TSPL_QC_CHECK_HEAD ON TSPL_QC_CHECK_HEAD.Gate_Entry_No=TSPL_GRN_HEAD.GRN_No
Left Outer Join TSPL_QC_CHECK_DETAIL On TSPL_QC_CHECK_DETAIL.Document_Code=TSPL_QC_CHECK_HEAD.Document_Code
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GRN_HEAD.Bill_To_Location
left outer join (select Document_Code,sum(InputDataDeductionPer) as deduction from TSPL_QC_CHECK_sRN_DETAIL 
group by Document_Code) as TSPL_QC_CHECK_SRN_DETAIL on TSPL_QC_CHECK_SRN_DETAIL.Document_Code=TSPL_QC_CHECK_HEAD.Document_Code
where 2=2 And Convert(Date,TSPL_GRN_HEAD.GRN_Date,103)>=Convert(Date,'" & txtFromDate.Value & "',103) 
          And Convert(Date,TSPL_GRN_HEAD.GRN_Date,103)<=Convert(Date,'" & txtToDate.Value & "',103) "

            Return Qry
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim qry As String = "Select ROW_NUMBER() OVER (ORDER BY GRN_Date) AS 'S.No',
 Convert(Varchar(10),GRN_Date,103) As [Date Of Arrival],Ref_No As [RM No.],Item_Desc As [Name Of Raw Material],VehicleNo As [Truck No.],Convert(Decimal(18,2),GRN_Qty) As [Total Qty],Convert(Decimal(18,2),TotalRejectQty) As [Rejected Qty], Convert(Varchar(10),RejectionDate,103) As [Date of Rejection],Vendor_Name As [Name of Supplier],Status As [Fully Rejected/Partially Rejected],StatusRemark As [Reason of Rejection]
 from (" & BaseQry() & ")xyz Where IsNull(Ref_No,'')<>'' And Status Is Not Null  Order By GRN_Date,Bill_To_Location,Ref_No"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                BlankRejectionGrid()
                gvRejection.AllowAddNewRow = False
                gvRejection.AllowDragToGroup = False
                gvRejection.DataSource = dt
                gvRejection.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class