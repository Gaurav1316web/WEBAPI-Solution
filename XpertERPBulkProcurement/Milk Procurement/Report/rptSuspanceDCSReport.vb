Imports common
Imports System.IO
Public Class rptSuspanceDCSReport
    Inherits FrmMainTranScreen
    Dim ApplySameDayShift As Boolean = False

    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "M"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "E"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
        txtToShift.DisplayMember = "Shift"
    End Sub
    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "M"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "E"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
        txtFromShift.DisplayMember = "Shift"
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(Exporter.Refresh)
    End Sub
    Private Sub Print(ByVal IsPrint As Exporter)
        Try
            Dim Fromshift As String = clsCommon.myCstr(txtFromShift.Text)
            Dim Toshift As String = clsCommon.myCstr(txtToShift.Text)
            Dim FromDate As String = clsCommon.myCstr(txtFromDate.Text)
            Dim TODate As String = clsCommon.myCstr(txtToDate.Text)
            Dim qry As String = ""
            Dim whrcls As String = ""
            'If ApplySameDayShift = False Then
            '    Dim parsedDate As DateTime
            '    If DateTime.TryParse(txtToDate.Text, parsedDate) Then
            '        Dim newDate As DateTime = parsedDate.AddDays(1)
            '    End If
            'End If
            qry = "  SELECT 
 Document_Date AS [COLLECTION DATE],
TSPL_MILK_COLLECTION_DCS_DETAIL.SHIFT AS [ SHIFT],
TSPL_MILK_COLLECTION_DCS.Posted_Date AS [MOVE DATE],
VMH.VLC_Code_VLC_Uploader as [From DCS],
TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [TO DCS],
TSPL_MILK_COLLECTION_DCS_DETAIL.Qty AS [Quantity],
TSPL_MILK_COLLECTION_DCS_DETAIL.fat as [FAT %],
TSPL_MILK_COLLECTION_DCS_DETAIL.SNF as [SNF %],
TSPL_MILK_SRN_DETAIL.AMOUNT as [AMOUNT],
TSPL_MILK_COLLECTION_DCS.Posted_By as [MOVE BY],
TSPL_MILK_COLLECTION_DCS_DETAIL.Suspence_Remarks as [REMARK]
FROM TSPL_MILK_COLLECTION_DCS_DETAIL 
LEFT OUTER JOIN TSPL_MILK_COLLECTION_DCS ON TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code 
left outer join TSPL_VLC_MASTER_HEAD as VMH on VMH.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.Suspence_VLC_Code
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL  on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No
left outer join TSPL_MILK_SRN_DETAIL  on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE
where  2 = 2 and TSPL_MILK_COLLECTION_DCS_DETAIL.Suspence=1  
"

            If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                qry += " and VMH.VLC_Code_VLC_Uploader in (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ")  "
            End If
            If ApplySameDayShift = True Then
                qry += " and (CASE 
                  WHEN TSPL_MILK_COLLECTION_DCS_DETAIL.Shift = '" + Fromshift + "' 
                  THEN CONVERT(DATE, TSPL_MILK_COLLECTION_DCS.Document_Date, 103)
                 WHEN TSPL_MILK_COLLECTION_DCS_DETAIL.Shift = '" + Toshift + "'
                 THEN CONVERT(DATE, TSPL_MILK_COLLECTION_DCS.Document_Date, 103)
                  ELSE NULL  
                END) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and '" + clsCommon.GetPrintDate(txtToDate.Value) + "'"
            Else
                qry += " and (CASE 
                 WHEN TSPL_MILK_COLLECTION_DCS_DETAIL.Shift = 'E' 
                  THEN DATEADD(DAY, -1, CONVERT(DATE, TSPL_MILK_COLLECTION_DCS.Document_Date, 103))
                 WHEN TSPL_MILK_COLLECTION_DCS_DETAIL.Shift = 'M' 
                 THEN CONVERT(DATE, TSPL_MILK_COLLECTION_DCS.Document_Date, 103)
                  ELSE NULL  
                END) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and '" + clsCommon.GetPrintDate(txtToDate.Value) + "'"
            End If


            'qry += "" & whrcls & ""
            'qry += " and Cast(TSPL_MILK_COLLECTION_DCS.Document_Date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(clsCommon.myCDate(txtFromDate.Value)), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_COLLECTION_DCS.Document_Date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtToDate.Value)), "dd/MMM/yyyy") + "'"
            'If clsCommon.CompairString(Fromshift, "E") = CompairStringResult.Equal Then
            '    qry += " and 2=( case when Cast(TSPL_MILK_COLLECTION_DCS.Document_Date as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_COLLECTION_DCS.Document_Date as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtFromDate.Value)), "dd/MMM/yyyy") + "' and TSPL_MILK_COLLECTION_DCS_DETAIL.SHIFT='M' then 3 else 2 end  )"
            'End If
            'If clsCommon.CompairString(Fromshift, "M") = CompairStringResult.Equal Then
            '    qry += " and 2=( case when Cast(TSPL_MILK_COLLECTION_DCS.Document_Date as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(TODate), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_COLLECTION_DCS.Document_Date as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.myCDate(txtToDate.Value)), "dd/MMM/yyyy") + "' and TSPL_MILK_COLLECTION_DCS_DETAIL.SHIFT='E' then 3 else 2 end  )"
            'End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                'View()
                ' SetGridFormation()
                'ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rptSuspanceDCSReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtFromShift.SelectedValue = "M"
        txtToShift.SelectedValue = "E"
        LoadShiftFrom()
        LoadShiftTo()
        ApplySameDayShift = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplySameDayShift, clsFixedParameterCode.ApplySameDayShift, Nothing)) = 1, True, False)

    End Sub
    Sub Reset()
        ' cboDocumentType.SelectedIndex = 0
        'txtRouteCode.Value = ""
        'lblRouteCode.Text = ""

        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        txtFromShift.SelectedValue = "M"
        txtToShift.SelectedValue = "E"
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub txtbmc__My_Click(sender As Object, e As EventArgs) Handles txtbmc._My_Click
        Try
            'If txtRoute.arrValueMember Is Nothing OrElse txtRoute.arrValueMember.Count <= 0 Then
            '    txtRoute.Focus()
            '    Throw New Exception("Please select at least route")
            'End If
            Dim qry As String = "SELECT Mcc_Code_VLC_Uploader AS [BMC CODE],MCC_NAME AS [BMC NAME] FROM TSPL_MCC_MASTER"

            txtbmc.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC1", qry, "BMC Code", "BMC Name", txtbmc.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDCS__My_Click(sender As Object, e As EventArgs) Handles txtDCS._My_Click
        Try
            'If txtRoute.arrValueMember Is Nothing OrElse txtRoute.arrValueMember.Count <= 0 Then
            '    txtRoute.Focus()
            '    Throw New Exception("Please select at least route")
            'End If
            Dim qry As String = "select  TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code] ,TSPL_VLC_MASTER_HEAD.VLC_Name as [DCS Name] from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code where 2=2 and TSPL_VLC_MASTER_HEAD.Active='1' "
            'If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            'End If

            txtDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC1", qry, "DCS Code", "DCS Name", txtDCS.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class