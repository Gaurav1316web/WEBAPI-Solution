Imports System.IO
Imports common

Public Class rptTruckSheetReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Private Sub rptTruckSheetReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        Reset()
        SetBMCRouteVisble()
    End Sub
    Sub PrintNew(ByVal IsPrint As Exporter)
        Try
            Dim ARRStop As New List(Of String)
            Dim dtDate As DataTable = Nothing
            Dim dtMCCHead As DataTable = Nothing
            Dim dtMCCDetail As DataTable = Nothing

            Dim dtDCSDetail As DataTable = Nothing
            Dim dtDCSDetailUnique As DataTable = Nothing

            Dim dtMCCHeadFilter As DataTable = Nothing


            Dim drFilter As DataRow()

            Dim SumQty As Decimal = 0.0
            Dim SumFATKG As Decimal = 0.0
            Dim SumSNFKG As Decimal = 0.0
            Dim AVGFAT As Decimal = 0.0
            Dim AVGSNF As Decimal = 0.0
            Dim SumQty1 As Decimal = 0.0
            Dim SumFATKG1 As Decimal = 0.0
            Dim SumSNFKG1 As Decimal = 0.0
            Dim SumQty2 As Decimal = 0.0
            Dim SumFATKG2 As Decimal = 0.0
            Dim SumSNFKG2 As Decimal = 0.0
            Dim SumQty11 As Decimal = 0.0
            Dim SumFATKG11 As Decimal = 0.0
            Dim SumSNFKG11 As Decimal = 0.0
            ' History Data
            Dim Hist_SumQty As Decimal = 0.0
            Dim Hist_SumFATKG As Decimal = 0.0
            Dim Hist_SumSNFKG As Decimal = 0.0
            Dim Hist_AVGFAT As Decimal = 0.0
            Dim Hist_AVGSNF As Decimal = 0.0
            Dim Hist_SumQty1 As Decimal = 0.0
            Dim Hist_SumFATKG1 As Decimal = 0.0
            Dim Hist_SumSNFKG1 As Decimal = 0.0
            Dim Hist_SumQty2 As Decimal = 0.0
            Dim Hist_SumFATKG2 As Decimal = 0.0
            Dim Hist_SumSNFKG2 As Decimal = 0.0
            Dim Hist_SumQty11 As Decimal = 0.0
            Dim Hist_SumFATKG11 As Decimal = 0.0
            Dim Hist_SumSNFKG11 As Decimal = 0.0


            Dim RouteSumQty1 As Decimal = 0.0
            Dim RouteSumFATKG1 As Decimal = 0.0
            Dim RouteSumSNFKG1 As Decimal = 0.0
            Dim RouteHist_SumQty1 As Decimal = 0.0
            Dim RouteHist_SumFATKG1 As Decimal = 0.0
            Dim RouteHist_SumSNFKG1 As Decimal = 0.0
            Dim RouteSumQty2 As Decimal = 0.0
            Dim RouteSumFATKG2 As Decimal = 0.0
            Dim RouteSumSNFKG2 As Decimal = 0.0
            Dim RouteHist_SumQty2 As Decimal = 0.0
            Dim RouteHist_SumFATKG2 As Decimal = 0.0
            Dim RouteHist_SumSNFKG2 As Decimal = 0.0
            Dim RouteSumQty As Decimal = 0.0
            Dim RouteSumFATKG As Decimal = 0.0
            Dim RouteSumSNFKG As Decimal = 0.0
            Dim RouteHist_SumQty As Decimal = 0.0
            Dim RouteHist_SumFATKG As Decimal = 0.0
            Dim RouteHist_SumSNFKG As Decimal = 0.0


            Dim MCCSumQty As Decimal = 0.0
            Dim MCCSumFATKG As Decimal = 0.0
            Dim MCCSumSNFKG As Decimal = 0.0
            Dim MCCAVGFAT As Decimal = 0.0
            Dim MCCAVGSNF As Decimal = 0.0

            Dim dtReport As DataTable = New DataTable()
            dtReport.Columns.Add("DCS", GetType(String))
            dtReport.Columns.Add("Date", GetType(String))
            dtReport.Columns.Add("Shift", GetType(String))
            dtReport.Columns.Add("Type", GetType(String))
            dtReport.Columns.Add(New DataColumn("Qty", System.Type.GetType("System.Decimal")))
            dtReport.Columns.Add(New DataColumn("FAT", System.Type.GetType("System.Decimal")))
            dtReport.Columns.Add(New DataColumn("SNF", System.Type.GetType("System.Decimal")))
            dtReport.Columns.Add(New DataColumn("FATKG", System.Type.GetType("System.Decimal")))
            dtReport.Columns.Add(New DataColumn("SNFKG", System.Type.GetType("System.Decimal")))
            '========================Hist Data --==========================================
            dtReport.Columns.Add(New DataColumn("Hist_Qty", System.Type.GetType("System.Decimal")))
            dtReport.Columns.Add(New DataColumn("Hist_FAT", System.Type.GetType("System.Decimal")))
            dtReport.Columns.Add(New DataColumn("Hist_SNF", System.Type.GetType("System.Decimal")))
            dtReport.Columns.Add(New DataColumn("Hist_FATKG", System.Type.GetType("System.Decimal")))
            dtReport.Columns.Add(New DataColumn("Hist_SNFKG", System.Type.GetType("System.Decimal")))
            '==============================================================================

            Dim StrAgainst_Milk_Collection_MCC_Detail As String = ""
            If clsCommon.GetDateWithEndTime(txtToDate.Value) < clsCommon.GetDateWithStartTime(txtFromDate.Value) Then
                clsCommon.MyMessageBoxShow(Me, "To Date cant be less than from date", Me.Text)
                Exit Sub
            End If

            Dim qry As String = Nothing
            qry = "select TSPL_MILK_COLLECTION_MCC.Document_date,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MILK_COLLECTION_MCC_DETAIL.* 
from TSPL_MILK_COLLECTION_MCC_DETAIL
left join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.document_no=TSPL_MILK_COLLECTION_MCC_DETAIL.document_no
left join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_CODE=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_CODE
where 1=1 "
            If TxtMultiRoute.arrValueMember IsNot Nothing AndAlso TxtMultiRoute.arrValueMember.Count > 0 Then
                qry += " and Route_code in (" + clsCommon.GetMulcallString(TxtMultiRoute.arrValueMember) + ")  "
            End If
            If TxtMultiBMC.arrValueMember IsNot Nothing AndAlso TxtMultiBMC.arrValueMember.Count > 0 Then
                qry += " and TSPL_MCC_MASTER.MCC_CODE in (" + clsCommon.GetMulcallString(TxtMultiBMC.arrValueMember) + ")  "
            End If
            qry += " And convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103) <=convert(date,('" + txtToDate.Value + "'),103) 
                    ORDER BY TSPL_MILK_COLLECTION_MCC.document_date,TSPL_MILK_COLLECTION_MCC.Route_Code
,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader"
            dtMCCDetail = clsDBFuncationality.GetDataTable(qry)


            'Mcc Head
            qry = "select TSPL_MILK_COLLECTION_MCC.* from TSPL_MILK_COLLECTION_MCC
                where 1=1"

            If TxtMultiRoute.arrValueMember IsNot Nothing AndAlso TxtMultiRoute.arrValueMember.Count > 0 Then
                qry += " and Route_code in (" + clsCommon.GetMulcallString(TxtMultiRoute.arrValueMember) + ")  "
            End If
            If TxtMultiBMC.arrValueMember IsNot Nothing AndAlso TxtMultiBMC.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_COLLECTION_MCC.Document_No IN ( SELECT DISTINCT TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No FROM TSPL_MILK_COLLECTION_MCC_DETAIL
                        left join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.document_no=TSPL_MILK_COLLECTION_MCC_DETAIL.document_no
                        left join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_CODE=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_CODE
                        WHERE TSPL_MCC_MASTER.MCC_CODE in (" + clsCommon.GetMulcallString(TxtMultiBMC.arrValueMember) + ") ) "
            End If
            qry += " And convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103) <=convert(date,('" + txtToDate.Value + "'),103) 
                     ORDER BY TSPL_MILK_COLLECTION_MCC.document_date,TSPL_MILK_COLLECTION_MCC.Route_Code"
            dtMCCHead = clsDBFuncationality.GetDataTable(qry)

            'Date
            qry = "select distinct TSPL_MILK_COLLECTION_MCC.Document_Date from TSPL_MILK_COLLECTION_MCC
                where 1=1"
            If TxtMultiRoute.arrValueMember IsNot Nothing AndAlso TxtMultiRoute.arrValueMember.Count > 0 Then
                qry += " and Route_code in (" + clsCommon.GetMulcallString(TxtMultiRoute.arrValueMember) + ")  "
            End If
            If TxtMultiBMC.arrValueMember IsNot Nothing AndAlso TxtMultiBMC.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_COLLECTION_MCC.Document_No IN ( SELECT DISTINCT TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No FROM TSPL_MILK_COLLECTION_MCC_DETAIL
                        left join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.document_no=TSPL_MILK_COLLECTION_MCC_DETAIL.document_no
                        left join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_CODE=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_CODE
                        WHERE TSPL_MCC_MASTER.MCC_CODE in (" + clsCommon.GetMulcallString(TxtMultiBMC.arrValueMember) + ") ) "
            End If
            qry += " And convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103) <=convert(date,('" + txtToDate.Value + "'),103) 
                     ORDER BY TSPL_MILK_COLLECTION_MCC.document_date"
            dtDate = clsDBFuncationality.GetDataTable(qry)
            If dtDate IsNot Nothing And dtDate.Rows.Count > 0 Then
                For idxDate As Integer = 0 To dtDate.Rows.Count - 1
                    dtReport.Rows.Add(clsCommon.GetPrintDate(dtDate.Rows(idxDate).Item("Document_date"), "dd/MMM/yyyy"), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value) ' D
                    drFilter = Nothing
                    drFilter = dtMCCHead.Select("[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "'")
                    If drFilter IsNot Nothing AndAlso drFilter.Length > 0 Then
                        dtMCCHeadFilter = drFilter.CopyToDataTable()
                        Dim ArrMCCDoc As New Dictionary(Of String, List(Of String))
                        For i As Integer = 0 To dtMCCDetail.Rows.Count - 1
                            If clsCommon.GetDateWithStartTime(clsCommon.myCDate(dtMCCDetail.Rows(i).Item("Document_date"))) = clsCommon.GetDateWithStartTime(clsCommon.myCDate(dtDate.Rows(idxDate).Item("Document_Date"))) Then
                                If clsCommon.CompairString(StrAgainst_Milk_Collection_MCC_Detail, "") = CompairStringResult.Equal Then
                                    StrAgainst_Milk_Collection_MCC_Detail = clsCommon.myCstr(dtMCCDetail.Rows(i).Item("PK_ID"))
                                Else
                                    StrAgainst_Milk_Collection_MCC_Detail = StrAgainst_Milk_Collection_MCC_Detail + "," + clsCommon.myCstr(dtMCCDetail.Rows(i).Item("PK_ID"))
                                End If

                                If Not ArrMCCDoc.ContainsKey(clsCommon.myCstr(dtMCCDetail.Rows(i).Item("MCC_Code"))) Then
                                    ArrMCCDoc.Add(clsCommon.myCstr(dtMCCDetail.Rows(i).Item("MCC_Code")), New List(Of String))
                                End If
                                If Not ArrMCCDoc.Item(clsCommon.myCstr(dtMCCDetail.Rows(i).Item("MCC_Code"))).Contains(clsCommon.myCstr(dtMCCDetail.Rows(i).Item("Document_No"))) Then
                                    ArrMCCDoc.Item(clsCommon.myCstr(dtMCCDetail.Rows(i).Item("MCC_Code"))).Add(clsCommon.myCstr(dtMCCDetail.Rows(i).Item("Document_No")))
                                End If
                            End If
                        Next
                        qry = "SELECT TSPL_MILK_COLLECTION_DCS.Document_Date, TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail,
                        TSPL_VLC_MASTER_HEAD.VLC_Name,(CASE WHEN TSPL_MILK_COLLECTION_DCS_DETAIL.Dock_Collection_Milk_Type='C' THEN 'Cow' else 'Mixed' end) AS [CMType]
                        ,TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id,TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No,TSPL_MILK_COLLECTION_DCS_DETAIL.SNo,TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code
                        ,TSPL_MILK_COLLECTION_DCS_DETAIL.Shift,TSPL_MILK_COLLECTION_DCS_DETAIL.Milk_Type,TSPL_MILK_COLLECTION_DCS_DETAIL.Dock_Collection_Milk_Type
                        ,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty,TSPL_MILK_COLLECTION_DCS_DETAIL.FAT,TSPL_MILK_COLLECTION_DCS_DETAIL.SNF,TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG
                        ,TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG
                        ,(case when isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.Own_Qty,0)>0 then TSPL_MILK_COLLECTION_DCS_DETAIL.Own_Qty else isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.Qty,0) end) as Own_Qty
                        ,(case when isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.Own_FAT,0)>0 then TSPL_MILK_COLLECTION_DCS_DETAIL.Own_FAT else isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.FAT,0) end) as Own_FAT
                        ,(case when isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.Own_SNF,0)>0 then TSPL_MILK_COLLECTION_DCS_DETAIL.Own_SNF else isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.SNF,0) end) as Own_SNF
                        ,(case when isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.Own_FATKG,0)>0 then TSPL_MILK_COLLECTION_DCS_DETAIL.Own_FATKG else isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG,0) end) as Own_FATKG
                        ,(case when isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.Own_SNFKG,0)>0 then TSPL_MILK_COLLECTION_DCS_DETAIL.Own_SNFKG else isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG,0) end) as Own_SNFKG
                        FROM TSPL_MILK_COLLECTION_DCS_DETAIL
                        left join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.document_no=TSPL_MILK_COLLECTION_DCS_DETAIL.document_no
                        left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No                 
                        left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.vlc_code=TSPL_MILK_COLLECTION_DCS_DETAIL.vlc_code
                        where TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail IN (" + StrAgainst_Milk_Collection_MCC_Detail + ") order by TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader"
                        dtDCSDetail = clsDBFuncationality.GetDataTable(qry)

                        qry = "SELECT TSPL_MILK_COLLECTION_DCS.Document_Date,
                        TSPL_VLC_MASTER_HEAD.VLC_Name,(CASE WHEN TSPL_MILK_COLLECTION_DCS_DETAIL.Dock_Collection_Milk_Type='C' THEN 'Cow' else 'Mixed' end) AS [CMType]
                        ,TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id,TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No,TSPL_MILK_COLLECTION_DCS_DETAIL.SNo,TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code
                        ,TSPL_MILK_COLLECTION_DCS_DETAIL.Shift,TSPL_MILK_COLLECTION_DCS_DETAIL.Milk_Type,TSPL_MILK_COLLECTION_DCS_DETAIL.Dock_Collection_Milk_Type
                        ,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty,TSPL_MILK_COLLECTION_DCS_DETAIL.FAT,TSPL_MILK_COLLECTION_DCS_DETAIL.SNF,TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG
                        ,TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG
                        ,(case when isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.Own_Qty,0)>0 then TSPL_MILK_COLLECTION_DCS_DETAIL.Own_Qty else isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.Qty,0) end) as Own_Qty
                        ,(case when isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.Own_FAT,0)>0 then TSPL_MILK_COLLECTION_DCS_DETAIL.Own_FAT else isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.FAT,0) end) as Own_FAT
                        ,(case when isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.Own_SNF,0)>0 then TSPL_MILK_COLLECTION_DCS_DETAIL.Own_SNF else isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.SNF,0) end) as Own_SNF
                        ,(case when isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.Own_FATKG,0)>0 then TSPL_MILK_COLLECTION_DCS_DETAIL.Own_FATKG else isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG,0) end) as Own_FATKG
                        ,(case when isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.Own_SNFKG,0)>0 then TSPL_MILK_COLLECTION_DCS_DETAIL.Own_SNFKG else isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG,0) end) as Own_SNFKG
                        FROM TSPL_MILK_COLLECTION_DCS_DETAIL
                        left join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.document_no=TSPL_MILK_COLLECTION_DCS_DETAIL.document_no
                        inner join ( select  Document_No from TSPL_MILK_COLLECTION_DCS_MCC_DETAIL where TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail In (" + StrAgainst_Milk_Collection_MCC_Detail + ") group by  Document_No) as TSPL_MILK_COLLECTION_DCS_MCC_DETAIL  on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
                        left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.vlc_code=TSPL_MILK_COLLECTION_DCS_DETAIL.vlc_code
                        where 2=2 order by TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader"
                        dtDCSDetailUnique = clsDBFuncationality.GetDataTable(qry)

                        Dim strCurrentRoute As String = clsCommon.myCstr(dtMCCHeadFilter.Rows(0).Item("Route_Code"))
                        For i As Integer = 0 To dtMCCHeadFilter.Rows.Count - 1
                            Dim isLastRoute As Boolean = True
                            If i + 1 <= dtMCCHeadFilter.Rows.Count - 1 Then
                                If clsCommon.CompairString(clsCommon.myCstr(dtMCCHeadFilter.Rows(i + 1).Item("Route_Code")), clsCommon.myCstr(dtMCCHeadFilter.Rows(i).Item("Route_Code"))) = CompairStringResult.Equal Then
                                    isLastRoute = False
                                End If
                            End If
                            If Not clsCommon.CompairString(strCurrentRoute, clsCommon.myCstr(dtMCCHeadFilter.Rows(i).Item("Route_Code"))) = CompairStringResult.Equal Then
                                strCurrentRoute = clsCommon.myCstr(dtMCCHeadFilter.Rows(i).Item("Route_Code"))
                                RouteSumQty1 = 0.0
                                RouteSumFATKG1 = 0.0
                                RouteSumSNFKG1 = 0.0
                                RouteHist_SumQty1 = 0.0
                                RouteHist_SumFATKG1 = 0.0
                                RouteHist_SumSNFKG1 = 0.0
                                RouteSumQty2 = 0.0
                                RouteSumFATKG2 = 0.0
                                RouteSumSNFKG2 = 0.0
                                RouteHist_SumQty2 = 0.0
                                RouteHist_SumFATKG2 = 0.0
                                RouteHist_SumSNFKG2 = 0.0
                                RouteSumQty = 0.0
                                RouteSumFATKG = 0.0
                                RouteSumSNFKG = 0.0
                                RouteHist_SumQty = 0.0
                                RouteHist_SumFATKG = 0.0
                                RouteHist_SumSNFKG = 0.0
                            End If
                            Dim strUnique As String = ""
                            Dim ActivateStop As Boolean = False
                            ARRStop = New List(Of String)

                            Dim dtMCCDetailFilter As DataTable = Nothing
                            drFilter = Nothing
                            drFilter = dtMCCDetail.Select("[Document_No]='" + clsCommon.myCstr(dtMCCHeadFilter.Rows(i).Item("Document_No")) + "'")
                            If drFilter IsNot Nothing AndAlso drFilter.Length > 0 Then
                                dtMCCDetailFilter = drFilter.CopyToDataTable()
                                For j As Integer = 0 To dtMCCDetailFilter.Rows.Count - 1
                                    strUnique = clsCommon.GetPrintDate(dtDate.Rows(idxDate).Item("Document_Date"), "ddMMyyyy") + clsCommon.myCstr(dtMCCDetailFilter.Rows(j).Item("MCC_Code"))
                                    If rbtnRouteWise.IsChecked Then
                                        strUnique += clsCommon.myCstr(dtMCCHeadFilter.Rows(i).Item("Route_Code"))
                                    End If
                                    If ARRStop.Contains(strUnique) Then
                                        ActivateStop = True
                                        Exit For
                                    End If
                                    If i = 0 AndAlso j = 0 AndAlso rbtnRouteWise.IsChecked = True Then
                                        dtReport.Rows.Add(clsCommon.myCstr("Tanker No : " + dtMCCHeadFilter.Rows(i).Item("Route_Code")), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value) ' D
                                    End If
                                    dtReport.Rows.Add(clsCommon.myCstr("BMC : " + dtMCCDetailFilter.Rows(j).Item("MCC_NAME")), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value) ' D
                                    SumQty = 0.0
                                    SumFATKG = 0.0
                                    SumSNFKG = 0.0
                                    AVGFAT = 0.0
                                    AVGSNF = 0.0
                                    ' History Data
                                    Hist_SumQty = 0.0
                                    Hist_SumFATKG = 0.0
                                    Hist_SumSNFKG = 0.0
                                    Hist_AVGFAT = 0.0
                                    Hist_AVGSNF = 0.0

                                    MCCSumQty = 0.0
                                    MCCSumFATKG = 0.0
                                    MCCSumSNFKG = 0.0
                                    MCCAVGFAT = 0.0
                                    MCCAVGSNF = 0.0

                                    Dim dtDCSDetailFilter As DataTable = Nothing
                                    drFilter = Nothing
                                    drFilter = dtDCSDetail.Select("[Against_Milk_Collection_MCC_Detail]='" + clsCommon.myCstr(dtMCCDetailFilter.Rows(j).Item("PK_ID")) + "'")
                                    If drFilter IsNot Nothing AndAlso drFilter.Length > 0 Then
                                        dtDCSDetailFilter = drFilter.CopyToDataTable()
                                        For K As Integer = 0 To dtDCSDetailFilter.Rows.Count - 1
                                            dtReport.Rows.Add(dtDCSDetailFilter.Rows(K).Item("VLC_Name"), clsCommon.GetPrintDate(dtDCSDetailFilter.Rows(K).Item("document_date"), "dd/MMM/yyyy"), dtDCSDetailFilter.Rows(K).Item("Shift"), dtDCSDetailFilter.Rows(K).Item("CMType"), dtDCSDetailFilter.Rows(K).Item("Qty"), dtDCSDetailFilter.Rows(K).Item("FAT"), dtDCSDetailFilter.Rows(K).Item("SNF"), dtDCSDetailFilter.Rows(K).Item("FATKG"), dtDCSDetailFilter.Rows(K).Item("SNFKG"), dtDCSDetailFilter.Rows(K).Item("Own_Qty"), dtDCSDetailFilter.Rows(K).Item("Own_FAT"), dtDCSDetailFilter.Rows(K).Item("Own_SNF"), dtDCSDetailFilter.Rows(K).Item("Own_FATKG"), dtDCSDetailFilter.Rows(K).Item("Own_SNFKG"))
                                        Next

                                        SumQty = Math.Round(clsCommon.myCdbl(dtDCSDetailFilter.Compute("SUM([Qty])", " [Qty] is not null")), 2)
                                        SumFATKG = Math.Round(clsCommon.myCdbl(dtDCSDetailFilter.Compute("SUM([FATKG])", " [FATKG] is not null")), 2)
                                        SumSNFKG = Math.Round(clsCommon.myCdbl(dtDCSDetailFilter.Compute("SUM([SNFKG])", " [SNFKG] is not null")), 2)
                                        If SumQty > 0 Then
                                            AVGFAT = Math.Round(clsCommon.myCdbl(dtDCSDetailFilter.Compute("(SUM([FATKG])*100)/SUM([Qty])", "")), 2)
                                            AVGSNF = Math.Round(clsCommon.myCdbl(dtDCSDetailFilter.Compute("(SUM([SNFKG])*100)/SUM([Qty])", "")), 2)
                                        Else
                                            AVGFAT = 0.0
                                            AVGSNF = 0.0
                                        End If
                                        '================History Data ===================
                                        Hist_SumQty = Math.Round(clsCommon.myCdbl(dtDCSDetailFilter.Compute("SUM([Own_Qty])", " [Own_Qty] is not null")), 2)
                                        Hist_SumFATKG = Math.Round(clsCommon.myCdbl(dtDCSDetailFilter.Compute("SUM([Own_FATKG])", " [Own_FATKG] is not null")), 2)
                                        Hist_SumSNFKG = Math.Round(clsCommon.myCdbl(dtDCSDetailFilter.Compute("SUM([Own_SNFKG])", " [Own_SNFKG] is not null")), 2)
                                        If Hist_SumQty > 0 Then
                                            Hist_AVGFAT = Math.Round(clsCommon.myCdbl(dtDCSDetailFilter.Compute("(SUM([Own_FATKG])*100)/SUM([Own_Qty])", "")), 2)
                                            Hist_AVGSNF = Math.Round(clsCommon.myCdbl(dtDCSDetailFilter.Compute("(SUM([Own_SNFKG])*100)/SUM([Own_Qty])", "")), 2)
                                        Else
                                            Hist_AVGFAT = 0.0
                                            Hist_AVGSNF = 0.0
                                        End If
                                        '=================================================

                                        dtReport.Rows.Add("Total Collection for BMC : " + clsCommon.myCstr(dtMCCDetailFilter.Rows(j).Item("Mcc_Code_VLC_Uploader")), DBNull.Value, DBNull.Value, DBNull.Value, SumQty, AVGFAT, AVGSNF, SumFATKG, SumSNFKG, Hist_SumQty, Hist_AVGFAT, Hist_AVGSNF, Hist_SumFATKG, Hist_SumSNFKG)
                                    End If


                                    If ArrMCCDoc.ContainsKey(clsCommon.myCstr(dtMCCDetailFilter.Rows(j).Item("MCC_Code"))) Then
                                        Dim strFilter As String = "MCC_code ='" + clsCommon.myCstr(dtMCCDetailFilter.Rows(j).Item("MCC_Code")) + "' and [Document_No] in (" + clsCommon.GetMulcallString(ArrMCCDoc.Item(clsCommon.myCstr(dtMCCDetailFilter.Rows(j).Item("MCC_Code")))) + ")"
                                        Dim dr3 As DataRow() = dtMCCDetail.Select(strFilter)
                                        If dr3 IsNot Nothing AndAlso dr3.Length > 0 Then
                                            Dim dtMCC As DataTable = dr3.CopyToDataTable()
                                            MCCSumQty = Math.Round(clsCommon.myCdbl(dtMCC.Compute("SUM([Qty])", " [Qty] is not null")), 2)
                                            MCCSumFATKG = Math.Round(clsCommon.myCdbl(dtMCC.Compute("SUM([FATKG])", " [FATKG] is not null")), 2)
                                            MCCSumSNFKG = Math.Round(clsCommon.myCdbl(dtMCC.Compute("SUM([SNFKG])", " [SNFKG] is not null")), 2)
                                            If MCCSumQty > 0 Then
                                                MCCAVGFAT = Math.Round(clsCommon.myCdbl(dtMCC.Compute("(SUM([FATKG])*100)/SUM([Qty])", "")), 2)
                                                MCCAVGSNF = Math.Round(clsCommon.myCdbl(dtMCC.Compute("(SUM([SNFKG])*100)/SUM([Qty])", "")), 2)
                                            Else
                                                MCCAVGFAT = 0.0
                                                MCCAVGSNF = 0.0
                                            End If
                                        End If
                                    End If



                                    If rbtnRouteWise.IsChecked = True Then
                                        dtReport.Rows.Add("Dispatch Detail for BMC : " + clsCommon.myCstr(dtMCCDetailFilter.Rows(j).Item("Mcc_Code_VLC_Uploader")), DBNull.Value, DBNull.Value, DBNull.Value, MCCSumQty, MCCAVGFAT, MCCAVGSNF, MCCSumFATKG, MCCSumSNFKG, MCCSumQty, MCCAVGFAT, MCCAVGSNF, MCCSumFATKG, MCCSumSNFKG)
                                    End If

                                    Dim VariationQty As Decimal = Math.Round(SumQty - MCCSumQty, 2)
                                    Dim VariationFATKG As Decimal = Math.Round(SumFATKG - MCCSumFATKG, 2)
                                    Dim VariationSNFKG As Decimal = Math.Round(SumSNFKG - MCCSumSNFKG, 2)

                                    Dim Hist_VariationQty As Decimal = Math.Round(Hist_SumQty - MCCSumQty, 2)
                                    Dim Hist_VariationFATKG As Decimal = Math.Round(Hist_SumFATKG - MCCSumFATKG, 2)
                                    Dim Hist_VariationSNFKG As Decimal = Math.Round(Hist_SumSNFKG - MCCSumSNFKG, 2)

                                    If rbtnRouteWise.IsChecked = True Then
                                        dtReport.Rows.Add("Variation : ", DBNull.Value, DBNull.Value, DBNull.Value, VariationQty, DBNull.Value, DBNull.Value, VariationFATKG, VariationSNFKG, Hist_VariationQty, DBNull.Value, DBNull.Value, Hist_VariationFATKG, Hist_VariationSNFKG)
                                    End If
                                Next

                                If ActivateStop Then
                                    Exit For
                                Else
                                    For j As Integer = 0 To dtMCCDetailFilter.Rows.Count - 1
                                        strUnique = clsCommon.GetPrintDate(dtDate.Rows(idxDate).Item("Document_Date"), "ddMMyyyy") + clsCommon.myCstr(dtMCCDetailFilter.Rows(j).Item("MCC_Code"))
                                        If rbtnRouteWise.IsChecked Then
                                            strUnique += clsCommon.myCstr(dtMCCHeadFilter.Rows(i).Item("Route_Code"))
                                        End If
                                        If Not ARRStop.Contains(strUnique) Then
                                            ARRStop.Add(strUnique)
                                        End If
                                    Next
                                End If
                                'MCC Detail Total
                                SumQty1 = Math.Round(clsCommon.myCdbl(dtMCCDetailFilter.Compute("SUM([Qty])", " [Qty] is not null")), 2)
                                SumFATKG1 = Math.Round(clsCommon.myCdbl(dtMCCDetailFilter.Compute("SUM([FATKG])", " [FATKG] is not null")), 2)
                                SumSNFKG1 = Math.Round(clsCommon.myCdbl(dtMCCDetailFilter.Compute("SUM([SNFKG])", " [SNFKG] is not null")), 2)

                                Hist_SumQty1 = Math.Round(clsCommon.myCdbl(dtMCCDetailFilter.Compute("SUM([Qty])", " [Qty] is not null")), 2)
                                Hist_SumFATKG1 = Math.Round(clsCommon.myCdbl(dtMCCDetailFilter.Compute("SUM([FATKG])", " [FATKG] is not null")), 2)
                                Hist_SumSNFKG1 = Math.Round(clsCommon.myCdbl(dtMCCDetailFilter.Compute("SUM([SNFKG])", " [SNFKG] is not null")), 2)

                                'MCC Head 
                                SumQty2 = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Rows(i).Item("Entered_Qty")), 2)
                                SumFATKG2 = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Rows(i).Item("Entered_FATKg")), 2)
                                SumSNFKG2 = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Rows(i).Item("Entered_SNFKg")), 2)

                                Hist_SumQty2 = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Rows(i).Item("Entered_Qty")), 2)
                                Hist_SumFATKG2 = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Rows(i).Item("Entered_FATKg")), 2)
                                Hist_SumSNFKG2 = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Rows(i).Item("Entered_SNFKg")), 2)

                                'Variation
                                SumQty = Math.Round(SumQty1 - SumQty2, 2)
                                SumFATKG = Math.Round(SumFATKG1 - SumFATKG2, 2)
                                SumSNFKG = Math.Round(SumSNFKG1 - SumSNFKG2, 2)

                                Hist_SumQty = Math.Round(Hist_SumQty1 - Hist_SumQty2, 2)
                                Hist_SumFATKG = Math.Round(Hist_SumFATKG1 - Hist_SumFATKG2, 2)
                                Hist_SumSNFKG = Math.Round(Hist_SumSNFKG1 - Hist_SumSNFKG2, 2)

                                If rbtnRouteWise.IsChecked Then
                                    RouteSumQty1 += SumQty1
                                    RouteSumFATKG1 += SumFATKG1
                                    RouteSumSNFKG1 += SumSNFKG1

                                    RouteHist_SumQty1 += Hist_SumQty1
                                    RouteHist_SumFATKG1 += Hist_SumFATKG1
                                    RouteHist_SumSNFKG1 += Hist_SumSNFKG1

                                    RouteSumQty2 += SumQty2
                                    RouteSumFATKG2 += SumFATKG2
                                    RouteSumSNFKG2 += SumSNFKG2

                                    RouteHist_SumQty2 += Hist_SumQty2
                                    RouteHist_SumFATKG2 += Hist_SumFATKG2
                                    RouteHist_SumSNFKG2 += Hist_SumSNFKG2

                                    RouteSumQty += SumQty
                                    RouteSumFATKG += SumFATKG
                                    RouteSumSNFKG += SumSNFKG

                                    RouteHist_SumQty += Hist_SumQty
                                    RouteHist_SumFATKG += Hist_SumFATKG
                                    RouteHist_SumSNFKG += Hist_SumSNFKG

                                    If isLastRoute Then
                                        dtReport.Rows.Add("Total Dispatch for Tanker : " + clsCommon.myCstr(dtMCCHeadFilter.Rows(i).Item("Route_Code")), DBNull.Value, DBNull.Value, DBNull.Value, RouteSumQty1, DBNull.Value, DBNull.Value, RouteSumFATKG1, RouteSumSNFKG1, RouteHist_SumQty1, DBNull.Value, DBNull.Value, RouteHist_SumFATKG1, RouteHist_SumSNFKG1)
                                        dtReport.Rows.Add("Receipt at dock for : ", DBNull.Value, DBNull.Value, DBNull.Value, RouteSumQty2, DBNull.Value, DBNull.Value, RouteSumFATKG2, RouteSumSNFKG2, RouteHist_SumQty2, DBNull.Value, DBNull.Value, RouteHist_SumFATKG2, RouteHist_SumSNFKG2)
                                        dtReport.Rows.Add("Variation : ", DBNull.Value, DBNull.Value, DBNull.Value, RouteSumQty, DBNull.Value, DBNull.Value, RouteSumFATKG, RouteSumSNFKG, RouteHist_SumQty, DBNull.Value, DBNull.Value, RouteHist_SumFATKG, RouteHist_SumSNFKG)
                                    End If
                                End If

                            End If
                        Next

                        SumQty = Math.Round(clsCommon.myCdbl(dtDCSDetailUnique.Compute("SUM([Qty])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "' and [Qty] is not null")), 2)
                        SumFATKG = Math.Round(clsCommon.myCdbl(dtDCSDetailUnique.Compute("SUM([FATKG])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "' and [FATKG] is not null")), 2)
                        SumSNFKG = Math.Round(clsCommon.myCdbl(dtDCSDetailUnique.Compute("SUM([SNFKG])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "' and [SNFKG] is not null")), 2)

                        Hist_SumQty = Math.Round(clsCommon.myCdbl(dtDCSDetailUnique.Compute("SUM([Own_Qty])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "' and [Own_Qty] is not null")), 2)
                        Hist_SumFATKG = Math.Round(clsCommon.myCdbl(dtDCSDetailUnique.Compute("SUM([Own_FATKG])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "' and [Own_FATKG] is not null")), 2)
                        Hist_SumSNFKG = Math.Round(clsCommon.myCdbl(dtDCSDetailUnique.Compute("SUM([Own_SNFKG])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "' and [Own_SNFKG] is not null")), 2)


                        dtReport.Rows.Add("Total Collection for Date : ", DBNull.Value, DBNull.Value, DBNull.Value, SumQty, DBNull.Value, DBNull.Value, SumFATKG, SumSNFKG, Hist_SumQty, DBNull.Value, DBNull.Value, Hist_SumFATKG, Hist_SumSNFKG)



                        SumQty = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([Qty])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "' and  [Qty] is not null")), 2)
                        SumFATKG = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([FATKG])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "' and  [FATKG] is not null")), 2)
                        SumSNFKG = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([SNFKG])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "' and  [SNFKG] is not null")), 2)

                        Hist_SumQty = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([Qty])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "' and  [Qty] is not null")), 2)
                        Hist_SumFATKG = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([FATKG])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "' and  [FATKG] is not null")), 2)
                        Hist_SumSNFKG = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([SNFKG])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "' and  [SNFKG] is not null")), 2)

                        If rbtnRouteWise.IsChecked = True Then
                            dtReport.Rows.Add("Total Dispatch for Date : ", DBNull.Value, DBNull.Value, DBNull.Value, SumQty, DBNull.Value, DBNull.Value, SumFATKG, SumSNFKG, Hist_SumQty, DBNull.Value, DBNull.Value, Hist_SumFATKG, Hist_SumSNFKG)
                        End If

                        SumQty = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Compute("SUM([Entered_Qty])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "'")), 2)
                        SumFATKG = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Compute("SUM([Entered_FATKg])", " [Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "'")), 2)
                        SumSNFKG = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Compute("SUM([Entered_SNFKg])", " [Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "'")), 2)

                        If SumQty > 0 Then
                            AVGFAT = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Compute("(SUM([Entered_FATKg])*100)/SUM([Entered_Qty])", " [Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "'")), 2)
                            AVGSNF = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Compute("(SUM([Entered_SNFKg])*100)/SUM([Entered_Qty])", " [Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "'")), 2)
                        Else
                            AVGFAT = 0
                            AVGSNF = 0
                        End If

                        '------------History Data--------------------
                        Hist_SumQty = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Compute("SUM([Entered_Qty])", "[Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "'")), 2)
                        Hist_SumFATKG = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Compute("SUM([Entered_FATKg])", " [Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "'")), 2)
                        Hist_SumSNFKG = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Compute("SUM([Entered_SNFKg])", " [Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "'")), 2)

                        If Hist_SumQty > 0 Then
                            Hist_AVGFAT = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Compute("(SUM([Entered_FATKg])*100)/SUM([Entered_Qty])", " [Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "'")), 2)
                            Hist_AVGSNF = Math.Round(clsCommon.myCdbl(dtMCCHeadFilter.Compute("(SUM([Entered_SNFKg])*100)/SUM([Entered_Qty])", " [Document_Date]='" + clsCommon.myCstr(dtDate.Rows(idxDate).Item("Document_Date")) + "'")), 2)
                        Else
                            Hist_AVGFAT = 0
                            Hist_AVGSNF = 0
                        End If
                        '-------------------------------------------
                        If rbtnRouteWise.IsChecked = True Then
                            dtReport.Rows.Add("Receipt at Dock for Date : ", DBNull.Value, DBNull.Value, DBNull.Value, SumQty, AVGFAT, AVGSNF, SumFATKG, SumSNFKG, Hist_SumQty, Hist_AVGFAT, Hist_AVGSNF, Hist_SumFATKG, Hist_SumSNFKG)
                        End If
                    End If
                Next

                SumQty1 = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([Qty])", "")), 2)
                SumFATKG1 = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([FATKG])", "")), 2)
                SumSNFKG1 = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([SNFKG])", "")), 2)
                SumQty2 = Math.Round(clsCommon.myCdbl(dtDCSDetailUnique.Compute("SUM([Qty])", "")), 2)
                SumFATKG2 = Math.Round(clsCommon.myCdbl(dtDCSDetailUnique.Compute("SUM([FATKG])", "")), 2)
                SumSNFKG2 = Math.Round(clsCommon.myCdbl(dtDCSDetailUnique.Compute("SUM([SNFKG])", "")), 2)

                SumQty = Math.Round(SumQty1 - SumQty2, 2)
                SumFATKG = Math.Round(SumFATKG1 - SumFATKG2, 2)
                SumSNFKG = Math.Round(SumSNFKG1 - SumSNFKG2, 2)

                '-------------------History Data ------------------------------------
                Hist_SumQty1 = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([Qty])", "")), 2)
                Hist_SumFATKG1 = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([FATKG])", "")), 2)
                Hist_SumSNFKG1 = Math.Round(clsCommon.myCdbl(dtMCCDetail.Compute("SUM([SNFKG])", "")), 2)
                Hist_SumQty2 = Math.Round(clsCommon.myCdbl(dtDCSDetailUnique.Compute("SUM([Own_Qty])", "")), 2)
                Hist_SumFATKG2 = Math.Round(clsCommon.myCdbl(dtDCSDetailUnique.Compute("SUM([Own_FATKG])", "")), 2)
                Hist_SumSNFKG2 = Math.Round(clsCommon.myCdbl(dtDCSDetailUnique.Compute("SUM([Own_SNFKG])", "")), 2)

                Hist_SumQty = Math.Round(Hist_SumQty1 - Hist_SumQty2, 2)
                Hist_SumFATKG = Math.Round(Hist_SumFATKG1 - Hist_SumFATKG2, 2)
                Hist_SumSNFKG = Math.Round(Hist_SumSNFKG1 - Hist_SumSNFKG2, 2)
                '-------------------------------------------------------------------


                SumQty2 = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_Qty])", "")), 2)
                SumFATKG2 = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_FATKg])", "")), 2)
                SumSNFKG2 = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_SNFKg])", "")), 2)

                SumQty11 = Math.Round(SumQty2 - SumQty1, 2)
                SumFATKG11 = Math.Round(SumFATKG2 - SumFATKG1, 2)
                SumSNFKG11 = Math.Round(SumSNFKG2 - SumSNFKG1, 2)

                '------History Data------------------------------
                Hist_SumQty2 = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_Qty])", "")), 2)
                Hist_SumFATKG2 = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_FATKg])", "")), 2)
                Hist_SumSNFKG2 = Math.Round(clsCommon.myCdbl(dtMCCHead.Compute("SUM([Entered_SNFKg])", "")), 2)

                Hist_SumQty11 = Math.Round(Hist_SumQty2 - Hist_SumQty1, 2)
                Hist_SumFATKG11 = Math.Round(Hist_SumFATKG2 - Hist_SumFATKG1, 2)
                Hist_SumSNFKG11 = Math.Round(Hist_SumSNFKG2 - Hist_SumSNFKG1, 2)
                '------------------------------------------------

                If rbtnRouteWise.IsChecked Then
                    dtReport.Rows.Add("Total Disp-Collection : ", DBNull.Value, DBNull.Value, DBNull.Value, SumQty, DBNull.Value, DBNull.Value, SumFATKG, SumSNFKG, Hist_SumQty, DBNull.Value, DBNull.Value, Hist_SumFATKG, Hist_SumSNFKG)
                    dtReport.Rows.Add("Loss in Transit : ", DBNull.Value, DBNull.Value, DBNull.Value, SumQty11, DBNull.Value, DBNull.Value, SumFATKG11, SumSNFKG11, Hist_SumQty11, DBNull.Value, DBNull.Value, Hist_SumFATKG11, Hist_SumSNFKG11)
                End If


                SumQty = Math.Round(SumQty + SumQty11, 2)
                SumFATKG = Math.Round(SumFATKG + SumFATKG11, 2)
                SumSNFKG = Math.Round(SumSNFKG + SumSNFKG11, 2)

                Hist_SumQty = Math.Round(Hist_SumQty + Hist_SumQty11, 2)
                Hist_SumFATKG = Math.Round(Hist_SumFATKG + Hist_SumFATKG11, 2)
                Hist_SumSNFKG = Math.Round(Hist_SumSNFKG + Hist_SumSNFKG11, 2)

                If rbtnRouteWise.IsChecked = True Then
                    dtReport.Rows.Add("Overall Loss : ", DBNull.Value, DBNull.Value, DBNull.Value, SumQty, DBNull.Value, DBNull.Value, SumFATKG, SumSNFKG, Hist_SumQty, DBNull.Value, DBNull.Value, Hist_SumFATKG, Hist_SumSNFKG)
                End If
            End If





            If dtReport IsNot Nothing And dtReport.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True

                gv1.EnableFiltering = True

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                Throw New Exception("No Data Found")
            End If

            gv1.DataSource = dtReport
            SetGridFormationOFGV1()
            gv1.BestFitColumns()
            ReStoreGridLayout()
            View()
            EnableDisable(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True

            'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") <> CompairStringResult.Equal Then
            '    gv1.Columns("From Location Code").IsVisible = False
            '    gv1.Columns("From Location Desc").IsVisible = False
            '    gv1.Columns("Ref Doc No").IsVisible = False
            '    gv1.Columns("row_num").IsVisible = False

            '    gv1.Columns("Tanker Dis From Loc").HeaderText = "From Location Code"
            '    gv1.Columns("Tanker Dis From Loc Name").HeaderText = "From Location Desc"
            'End If FATKG,SNFKG,FAT,SNF ,Hist_Qty,Hist_FATKG,Hist_SNFKG,Hist_FAT,Hist_SNF
            gv1.Columns("FATKG").HeaderText = "FAT KG"
            gv1.Columns("SNFKG").HeaderText = "SNF KG"
            gv1.Columns("FAT").HeaderText = "FAT %"
            gv1.Columns("SNF").HeaderText = "SNF %"

            gv1.Columns("Hist_Qty").HeaderText = "Qty"
            gv1.Columns("Hist_FATKG").HeaderText = "FAT KG"
            gv1.Columns("Hist_SNFKG").HeaderText = "SNF KG"
            gv1.Columns("Hist_FAT").HeaderText = "FAT %"
            gv1.Columns("Hist_SNF").HeaderText = "SNF %"

            gv1.Columns(ii).BestFit()
        Next

    End Sub
    Sub View()

        If gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(" "))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())

            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("DCS").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Shift").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Type").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Current Data"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("QTY").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("FAT").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SNF").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("FATKG").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SNFKG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("History Data"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Hist_Qty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Hist_FAT").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Hist_SNF").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Hist_FATKG").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Hist_SNFKG").Name)
            gv1.ViewDefinition = view
        End If
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        EnableDisable(True)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub EnableDisable(ByVal val As Boolean)
        TxtMultiBMC.Enabled = val
        TxtMultiRoute.Enabled = val
        RadGroupBox1.Enabled = val
        GroupBox1.Enabled = val
        chkTotalInBold.Enabled = val
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        PrintNew(Exporter.Refresh)
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub rptTruckSheetReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            PrintNew(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Truck Sheet Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If TxtMultiRoute.arrValueMember IsNot Nothing AndAlso TxtMultiRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add("Route No : " + clsCommon.GetMulcallString(TxtMultiRoute.arrValueMember))
                End If
                If TxtMultiBMC.arrValueMember IsNot Nothing AndAlso TxtMultiBMC.arrValueMember.Count > 0 Then
                    arrHeader.Add("BMC : " + clsCommon.GetMulcallString(TxtMultiBMC.arrValueMember))
                End If

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)

            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Truck Sheet Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If TxtMultiRoute.arrValueMember IsNot Nothing AndAlso TxtMultiRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add("Route No : " + clsCommon.GetMulcallString(TxtMultiRoute.arrValueMember))
                End If
                If TxtMultiBMC.arrValueMember IsNot Nothing AndAlso TxtMultiBMC.arrValueMember.Count > 0 Then
                    arrHeader.Add("BMC : " + clsCommon.GetMulcallString(TxtMultiBMC.arrValueMember))
                End If

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Truck Sheet Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub TxtMultiRoute__My_Click(sender As Object, e As EventArgs) Handles TxtMultiRoute._My_Click
        Try
            Dim qry As String = " select ROUTE_NO as Code,ROUTE_NAME as Name from  TSPL_BULK_ROUTE_MASTER 
                   where exists(select 1 from TSPL_BULK_ROUTE_MASTER_MCC where TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO=TSPL_BULK_ROUTE_MASTER.ROUTE_NO )"

            TxtMultiRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("TSultiRoute", qry, "Code", "Name", TxtMultiRoute.arrValueMember, TxtMultiRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rbtnBMCWise_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnBMCWise.ToggleStateChanged, rbtnRouteWise.ToggleStateChanged
        SetBMCRouteVisble()
    End Sub
    Sub SetBMCRouteVisble()
        If rbtnBMCWise.IsChecked = True Then
            lblBMC.Visible = True
            TxtMultiBMC.Visible = True

            lblRoute.Visible = False
            TxtMultiRoute.Visible = False

        ElseIf rbtnBMCWise.IsChecked = False Then
            lblBMC.Visible = False
            TxtMultiBMC.Visible = False

            lblRoute.Visible = True
            TxtMultiRoute.Visible = True
        End If
    End Sub
    Private Sub TxtMultiBMC__My_Click(sender As Object, e As EventArgs) Handles TxtMultiBMC._My_Click
        Try
            Dim qry As String = "select MCC_Code as [MCC Code],MCC_NAME as [MCC Name],TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
            TxtMultiBMC.arrValueMember = clsCommon.ShowMultipleSelectForm("TSRBMC", qry, "MCC Code", "MCC Name", TxtMultiBMC.arrValueMember, TxtMultiBMC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiPDFGrid_Click(sender As Object, e As EventArgs) Handles rmiPDFGrid.Click
        Dim doc As New clsMyPrintDocument()
        Try
            doc.Margins.Top = 50
            doc.Margins.Bottom = 50
            doc.Margins.Left = 50
            doc.Margins.Right = 50
            doc.HeaderHeight = 90
            'doc.Landscape = True
            doc.AssociatedObject = gv1

            doc.DocumentName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTruckSheetReport & "'"))
            doc.MiddleHeader = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTruckSheetReport & "'"))
            doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Bold)

            doc.LeftUpperText = "Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            doc.LeftUpperFont = New Font("Segoe UI", 8, FontStyle.Regular)


            If TxtMultiRoute.arrValueMember IsNot Nothing AndAlso TxtMultiRoute.arrValueMember.Count > 0 Then
                doc.LeftMiddleText = "Route No : " + clsCommon.GetMulcallString(TxtMultiRoute.arrValueMember)
                doc.LeftMiddleFont = New Font("Segoe UI", 8, FontStyle.Regular)
            End If

            If TxtMultiBMC.arrValueMember IsNot Nothing AndAlso TxtMultiBMC.arrValueMember.Count > 0 Then
                doc.LeftLowerText = "BMC : " + clsCommon.GetMulcallString(TxtMultiBMC.arrValueMember)
                doc.LeftLowerFont = New Font("Segoe UI", 8, FontStyle.Regular)
            End If

            doc.AssociatedObject = gv1
            'doc.Print()
            doc.RightFooter = "Page [Page #] of [Total Pages]"

            Dim dialog As New RadPrintPreviewDialog
            dialog.Document = doc
            dialog.ToolMenu.Visible = True
            dialog.ShowDialog()
            doc = Nothing

        Catch ex As Exception
            doc = Nothing
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gv1.RowFormatting
        Try
            If chkTotalInBold.Checked = True Then
                If clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Total Collection for BMC :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Dispatch Detail for BMC :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Variation :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Total Dispatch for Tanker :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Receipt at dock for :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Total Collection for Date :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Total Dispatch for Date :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Receipt at Dock for Date :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Total Disp-Collection :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Loss in Transit :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Overall Loss :") Then
                    'e.CellElement.Font = New Font(e.CellElement.Font, FontStyle.Bold)
                    e.RowElement.Font = New Font(e.RowElement.Font, FontStyle.Bold) 'New Font("Arial", 10, FontStyle.Bold)
                Else
                    'e.RowElement.Font = New Font(e.RowElement.Font, FontStyle.Regular)
                    e.RowElement.ResetValue(LightVisualElement.FontProperty, ValueResetFlags.Local)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_PrintCellFormatting(sender As Object, e As PrintCellFormattingEventArgs) Handles gv1.PrintCellFormatting
        'Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
        'If clsCommon.myCstr(grow.Cells("DCS").Value).Contains("Total Collection for BMC :") Then
        'If clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Total Collection for BMC :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Dispatch Detail for BMC :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Variation :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Total Dispatch for Tanker :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Receipt at dock for :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Total Collection for Date :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Total Dispatch for Date :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Receipt at Dock for Date :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Total Disp-Collection :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Loss in Transit :") OrElse clsCommon.myCstr(e.RowElement.RowInfo.Cells("DCS").Value).Contains("Overall Loss :") Then
        'If clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Total Collection for BMC :") Then
        If chkTotalInBold.Checked = True Then
            If clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Total Collection for BMC :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Dispatch Detail for BMC :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Variation :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Total Dispatch for Tanker :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Receipt at dock for :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Total Collection for Date :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Total Dispatch for Date :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Receipt at Dock for Date :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Total Disp-Collection :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Loss in Transit :") OrElse clsCommon.myCstr(e.Row.Cells("DCS").Value).Contains("Overall Loss :") Then
                'e.PrintCell.Font = New Font("Arial", 8, FontStyle.Bold)
                e.PrintCell.Font = New Font("Segoe UI", 8.25, FontStyle.Bold)
            End If
        End If
        'e.PrintCell.Font = e.Row.Cells("DCS").Style.Font
    End Sub

End Class
