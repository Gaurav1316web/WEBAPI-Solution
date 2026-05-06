
Imports System.Data.SqlClient
Imports common
Public Class frmNIRQCvsWetQc
    Inherits FrmMainTranScreen

    Private Sub frmNIRQCvsWetQc_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N'  order by Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelVUP", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " Select Item_Code as ItemCode,Item_Desc as ItemDescription from TSPL_ITEM_MASTER "
        'TxtItem.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", whrcls, TxtItem.Value, "ItemCode", isButtonClicked)
        ' txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorMafnd", qry, "ItemCode", Nothing, txtItem.arrValueMember, "ItemCode", isButtonClicked)
        '  txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorMafnd", qry, "ItemCode", Nothing, txtItem.arrValueMember, "ItemCode", isButtonClicked)
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelVUP", qry, "ItemCode", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim QRY As String = "SELECT TSPL_NIR_QC.Document_No AS [NIRQC NO],TSPL_NIR_QC.Document_Date AS [NIRQC Date] , TSPL_SRN_HEAD.SRN_NO AS [SRN NO],TSPL_SRN_HEAD.SRN_DATE AS [SRN DATE],TSPL_MRN_HEAD.MRN_NO AS [MRN NO],TSPL_MRN_HEAD.mrn_date AS [MRN Date],TSPL_MRN_HEAD.Against_GRN as [GRN No],tspl_grn_head.GRN_DATE as [GRN Date],
TSPL_MRN_HEAD.Vendor_Code as [Vendor Code],TSPL_MRN_HEAD.Vendor_Name as [Vendor Name],TSPL_MRN_HEAD.Bill_To_Location as [Location],Moisture	,	TSPL_NIR_QC_FOSS.Silica_DM	,	TSPL_NIR_QC_FOSS.Fat_DM	,	TSPL_NIR_QC_FOSS.Protein_DM	,	TSPL_NIR_QC_FOSS.Fiber_DM,

TSPL_QC_CHECK_SRN_DETAIL.ITEM_CODE,QC_Param_Code,TSPL_QC_CHECK_SRN_DETAIL.InputData,TSPL_QC_LOG_SHEET_MASTER.NIRQC_Para_type
	--,TSPL_SRN_TENDER.Against_TenderNo as [RAL]
 FROM TSPL_NIR_QC
 Left outer join TSPL_MRN_HEAD on TSPL_MRN_HEAD.mrn_no=TSPL_NIR_QC.MRN_No
 LEFT OUTER JOIN TSPL_QC_CHECK_SRN_DETAIL ON TSPL_QC_CHECK_SRN_DETAIL.MRN_NO =TSPL_MRN_HEAD.MRN_NO
left outer join tspl_grn_head on tspl_grn_head.Grn_no =TSPL_MRN_HEAD.Against_GRN
LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.Against_MRN=TSPL_MRN_HEAD.MRN_No
left outer join TSPL_QC_LOG_SHEET_MASTER on TSPL_QC_LOG_SHEET_MASTER.code=TSPL_QC_CHECK_SRN_DETAIL.QC_Param_Code
LEFT OUTER JOIN TSPL_NIR_QC_FOSS ON TSPL_NIR_QC_FOSS.PK_ID =TSPL_NIR_QC.Against_Foss_PK_ID  WHERE 2=2 "
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                QRY += " and TSPL_MRN_HEAD.Bill_To_Location in(" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")" & Environment.NewLine
            End If
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                QRY += " and TSPL_MRN_HEAD.Vendor_Code in(" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")" & Environment.NewLine
            End If
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                QRY += " and TSPL_MRN_HEAD.Vendor_Code in(" & clsCommon.GetMulcallString(txtVendor.arrValueMember) & ")" & Environment.NewLine
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
        End Try
    End Sub
End Class