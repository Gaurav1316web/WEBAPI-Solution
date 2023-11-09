Imports common
Imports System.Data.SqlClient
Imports System.IO
Public Class FrmTransferRegister
    Inherits FrmMainTranScreen
    Private Const ReportID As String = "TransferRegister"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing
    Dim qry As String
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmTransferRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport

    End Sub

    Private Sub FrmTransferRegister_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = fromDate.Value
        LoadLocation()
        gv.AllowEditRow = False
        gv.AllowDragToGroup = False
        gv.AllowAddNewRow = False
    End Sub

    Sub LoadLocation()
        qry = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
        cbgLocation.CheckedAll()
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub LoadData(ByVal IsPrint As Exporter)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmTransferRegister & "'"))
            Dim strLoca As String = ""
            For Each Str As String In cbgLocation.CheckedDisplayMember
                If clsCommon.myLen(strLoca) > 0 Then
                    strLoca += ", "
                End If
                strLoca += Str
            Next
            arrHeader.Add("Location : " + strLoca)

            If cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Select atleast single location.")
            End If
            'Ticket No-ERO/16/08/19-000994, Detail Report 
            If IsPrint = Exporter.Print OrElse IsPrint = Exporter.Refresh Then

                ' qry = " Select Final.* from ( Select Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then 'IN' else 'OUT' End as [In/Out],Case When isnull(TSPL_TRANSFER_ORDER_HEAD.Is_AgainstFormF,0)=1 then 'F-Form' else '' end as [Against Form]," & _
                '"  Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then locin.Location_Code else FromLoc.Location_code end as [From Location],Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then locin.Location_Desc else FromLoc.Location_Desc end as [From Location Desc],(Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then locin.Add1  else FromLoc.add1 end+Case When ISNULL(Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then locin.Add2  else FromLoc.add2 end,'')='' Then '' Else ', '+Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then locin.Add2  else FromLoc.add2 end End+Case When ISNULL(Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then locin.Add3  else FromLoc.add3 end,'')='' Then '' Else ', '+Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then locin.Add3  else FromLoc.add3 end End+Case When ISNULL(Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then locin.Add4  else FromLoc.add4 end,'')='' Then '' Else ', '+Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then locin.Add4  else FromLoc.add4 end End) [From Location Address], Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then locin.TIN_No   else FromLoc.TIN_No  end as [From Location Tin No], Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  ToLoc.Location_code else locout.Location_Code end as [To Location]" & _
                '", Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  ToLoc.Location_Desc else locout.Location_Desc end as [To Location Desc],  " & _
                '" (Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  ToLoc.Add1  else locout.Add1  end+Case When ISNULL(Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  ToLoc.Add2  else locout.Add2  end,'')='' Then '' Else ', '+Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  ToLoc.Add2  else locout.Add2  end End+Case When ISNULL(Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  ToLoc.Add3  else locout.Add3  end,'')='' Then '' Else ', '+Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  ToLoc.Add3  else locout.Add3  end End+Case When ISNULL(Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  ToLoc.Add4  else locout.Add4  end,'')='' Then '' Else ', '+Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  ToLoc.Add4  else locout.Add4  end End) [To Location Address], Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  ToLoc.TIN_No   else locout.TIN_No  end as [To Location Tin No], TSPL_TRANSFER_ORDER_HEAD.Document_No as [Document No]," & _
                '" Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then TSPL_TRANSFER_ORDER_HEAD.TransferOutNo Else '' End as [SourceDoc/Invoice Number], convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as [Invoice/ DocDate], TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt as [Total Amount of Invoice]," & _
                '" STUFF((Select DISTINCT ', '+TSPL_ITEM_MASTER.Cheapter_Heads from TSPL_TRANSFER_ORDER_DETAIL LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_TRANSFER_ORDER_DETAIL.Item_Code WHERE TSPL_TRANSFER_ORDER_DETAIL.Document_No=TSPL_TRANSFER_ORDER_HEAD.Document_No For XML Path('')), 1, 1, '') as [commodity]," & _
                '" TSPL_TRANSPORT_MASTER.Transporter_Name as [Transporter Name]," & _
                '" (TSPL_TRANSPORT_MASTER.Add1+Case WHEN ISNULL(TSPL_TRANSPORT_MASTER.Add2,'')='' Then '' Else ', '+TSPL_TRANSPORT_MASTER.Add2 End) as [Transporter Address]" & _
                '" from TSPL_TRANSFER_ORDER_HEAD" & _
                '" left join TSPL_TRANSFER_ORDER_HEAD TransO on TSPL_TRANSFER_ORDER_HEAD.TransferOutNo=TransO.Document_No " & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER FromLoc ON fromLoc.Location_Code =TSPL_TRANSFER_ORDER_HEAD.From_Location LEFT OUTER JOIN TSPL_LOCATION_MASTER ToLoc ON toLoc.Location_Code =TSPL_TRANSFER_ORDER_HEAD.To_Location" & _
                '" LEFT OUTER JOIN TSPL_VEHICLE_MASTER ON TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code" & _
                '" LEFT OUTER JOIN TSPL_TRANSPORT_MASTER ON TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_TRANSFER_ORDER_HEAD.Transport_Id left join TSPL_LOCATION_MASTER locin on locin.Location_Code =TransO.From_Location" & _
                '" left join TSPL_LOCATION_MASTER locout on locout.GIT_Location =toLoc.Location_Code " & _
                '" WHERE CONVERT(DATE,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)>='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'" & _
                '" AND CONVERT(DATE,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'" & _
                '" and Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' Then TSPL_TRANSFER_ORDER_HEAD.From_Location Else TSPL_TRANSFER_ORDER_HEAD.To_Location End in (" & clsCommon.GetMulcallString(cbgLocation.CheckedValue) & ") " & _
                '" Union All " & _
                '" Select Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then 'Purchase Return' else 'Sales Return' End as [In/Out],Case When isnull(TSPL_TRANSFER_ORDER_HEAD.Is_AgainstFormF,0)=1 then 'F-Form' else '' end as [Against Form], " & _
                '" Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then transInToLoc.Location_Code else TransOutToLoc.Location_code end as [From Location]," & _
                '" Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then transInToLoc.Location_Desc else TransOutToLoc.Location_Desc end as [From Location Desc]," & _
                '" (Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then transInToLoc.Add1  else TransOutToLoc.add1 end+ " & _
                '" Case When ISNULL(Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then transInToLoc.Add2  else TransOutToLoc.add2 end,'')='' Then '' Else ', '+ " & _
                '" Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then transInToLoc.Add2  else TransOutToLoc.add2 end End+ " & _
                '" Case When ISNULL(Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then transInToLoc.Add3  else TransOutToLoc.add3 end,'')='' Then '' Else ', '+ " & _
                '" Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then transInToLoc.Add3  else TransOutToLoc.add3 end End+ " & _
                '" Case When ISNULL(Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then transInToLoc.Add4  else TransOutToLoc.add4 end,'')='' Then '' Else ', '+ " & _
                '" Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then transInToLoc.Add4  else TransOutToLoc.add4 end End) [From Location Address], " & _
                '" Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then transInToLoc.TIN_No   else TransOutToLoc.TIN_No  end as [From Location Tin No], " & _
                '" Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  transInFrmLoc.Location_Code else transOutFromLoc.Location_Code end as [To Location], " & _
                '" Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  transInFrmLoc.Location_Desc else transOutFromLoc.Location_Desc end as [To Location Desc], " & _
                '" (Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  transInFrmLoc.Add1  else transOutFromLoc.Add1  end+ " & _
                '" Case When ISNULL(Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  transInFrmLoc.Add2  else transOutFromLoc.Add2  end,'')='' Then '' Else ', '+ " & _
                '" Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  transInFrmLoc.Add2  else transOutFromLoc.Add2  end End+ " & _
                '" Case When ISNULL(Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  transInFrmLoc.Add3  else transOutFromLoc.Add3  end,'')='' Then '' Else ', '+ " & _
                '" Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  transInFrmLoc.Add3  else transOutFromLoc.Add3  end End+ " & _
                '" Case When ISNULL(Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  transInFrmLoc.Add4  else transOutFromLoc.Add4  end,'')='' Then '' Else ', '+ " & _
                '" Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  transInFrmLoc.Add4  else transOutFromLoc.Add4  end End) [To Location Address], " & _
                '" Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then  transInFrmLoc.TIN_No   else transOutFromLoc.TIN_No  end as [To Location Tin No], " & _
                '" TSPL_TRANSFER_RETURN.Document_No as [Document No], " & _
                '" TSPL_TRANSFER_RETURN.Transfer_No [SourceDoc/Invoice Number], convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as [Invoice/ DocDate], -TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt as [Total Amount of Invoice], STUFF((Select DISTINCT ', '+TSPL_ITEM_MASTER.Cheapter_Heads from TSPL_TRANSFER_ORDER_DETAIL LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_TRANSFER_ORDER_DETAIL.Item_Code WHERE TSPL_TRANSFER_ORDER_DETAIL.Document_No=TSPL_TRANSFER_ORDER_HEAD.Document_No For XML Path('')), 1, 1, '') as [commodity], TSPL_TRANSPORT_MASTER.Transporter_Name as [Transporter Name], (TSPL_TRANSPORT_MASTER.Add1+Case WHEN ISNULL(TSPL_TRANSPORT_MASTER.Add2,'')='' Then '' Else ', '+TSPL_TRANSPORT_MASTER.Add2 End) as [Transporter Address] from TSPL_TRANSFER_ORDER_HEAD " & _
                '" left outer join TSPL_TRANSFER_RETURN on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_RETURN.Transfer_No " & _
                '" left join TSPL_TRANSFER_ORDER_HEAD TransO on TSPL_TRANSFER_ORDER_HEAD.TransferOutNo=TransO.Document_No " & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER transOutFromLoc ON transOutFromLoc.Location_Code =TSPL_TRANSFER_ORDER_HEAD.From_Location " & _
                '" left join TSPL_LOCATION_MASTER TransOutToLoc on TransOutToLoc.GIT_Location =TSPL_TRANSFER_ORDER_HEAD.To_Location " & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER transInToLoc ON transInToLoc.Location_Code =TSPL_TRANSFER_ORDER_HEAD.To_Location " & _
                '" LEFT OUTER JOIN TSPL_LOCATION_MASTER transInFrmLoc ON transInFrmLoc.Location_Code =TransO.From_Location  " & _
                '" LEFT OUTER JOIN TSPL_VEHICLE_MASTER ON TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code " & _
                '" LEFT OUTER JOIN TSPL_TRANSPORT_MASTER ON TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_TRANSFER_ORDER_HEAD.Transport_Id " & _
                '" WHERE isnull(TSPL_TRANSFER_RETURN.Transfer_No ,'')<>'' " & _
                '" And CONVERT(DATE,TSPL_TRANSFER_RETURN.Document_Date,103)>='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'" & _
                '" AND CONVERT(DATE,TSPL_TRANSFER_RETURN.Document_Date,103)<='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'" & _
                '" and Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' Then TSPL_TRANSFER_ORDER_HEAD.From_Location Else TSPL_TRANSFER_ORDER_HEAD.To_Location End in (" & clsCommon.GetMulcallString(cbgLocation.CheckedValue) & ") " & _
                '" ) Final ORDER BY convert(date,Final.[Invoice/ DocDate] ,103) "


                If chkSummary.IsChecked = True Then

                    qry = " Select Final.* from ( Select Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then 'IN' else 'OUT' End as [In/Out],Case When isnull(TSPL_TRANSFER_ORDER_HEAD.Is_AgainstFormF,0)=1 then 'F-Form' else '' end as [Against Form]," & _
                    " CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Location_Code  + ' (' + " + "TSPL_TRANSFER_ORDER_HEAD.From_Location " + "+')'" + "  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Location_Code  + ' (' + TSPL_TRANSFER_ORDER_HEAD.From_Location +')'  ELSE TSPL_TRANSFER_ORDER_HEAD.From_Location END" & _
                    " as [From Location], " & _
                    " CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Location_Desc + ' (' + " + "FromLoc.Location_Desc " + "+')'" + "  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Location_Desc  + ' (' + FromLoc.Location_Desc +')' ELSE FromLoc.Location_Desc END" & _
                    "  as [From Location Desc], " & _
                    " CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add1  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add1 Else FromLoc.Add1 end + " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add2  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add2 Else FromLoc.Add2 End + " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add3  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add3 Else FromLoc.Add3 End + " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add4  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add4  Else FromLoc.Add4 End  " & _
                    "  as [From Location Address], " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.gstno  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.gstno  Else FromLoc.gstno End   as [From Location GSTIN No], " & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Location_Code  + ' (' + TSPL_TRANSFER_ORDER_HEAD.To_Location +')'  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Location_Code  + ' (' + TSPL_TRANSFER_ORDER_HEAD.To_Location +')'  ELSE TSPL_TRANSFER_ORDER_HEAD.To_Location END  " & _
                    "  as [To Location]," & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Location_Desc  + ' (' + " + "ToLoc.Location_Desc " + "+')'" + "  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Location_Desc  + ' (' + ToLoc.Location_Desc +')' ELSE ToLoc.Location_Desc END " & _
                    "  as [To Location Desc]," & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add1 when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add1  Else ToLoc.Add1 end + " & _
                    "  CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add2  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add2 Else ToLoc.Add2 End + " & _
                    "  CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add3  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add3  Else ToLoc.Add3 End + " & _
                    "  CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add4  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add4 Else ToLoc.Add4 End  " & _
                    " as [To Location Address], " & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.gstno  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.gstno  Else ToLoc.gstno End as [To Location GSTIN No] " & _
                    " , TSPL_TRANSFER_ORDER_HEAD.Document_No as [Document No]," & _
                    " Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then TSPL_TRANSFER_ORDER_HEAD.TransferOutNo Else '' End as [SourceDoc/Invoice Number], convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as [Invoice/ DocDate], TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt as [Total Amount of Invoice]," & _
                    " STUFF((Select DISTINCT ', '+TSPL_ITEM_MASTER.Cheapter_Heads from TSPL_TRANSFER_ORDER_DETAIL LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_TRANSFER_ORDER_DETAIL.Item_Code WHERE TSPL_TRANSFER_ORDER_DETAIL.Document_No=TSPL_TRANSFER_ORDER_HEAD.Document_No For XML Path('')), 1, 1, '') as [commodity]," & _
                    " case when isnull(TSPL_TRANSPORT_MASTER.Transporter_Name,'')='' then TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual else TSPL_TRANSPORT_MASTER.Transporter_Name end as [Transporter Name]," & _
                    " (TSPL_TRANSPORT_MASTER.Add1+Case WHEN ISNULL(TSPL_TRANSPORT_MASTER.Add2,'')='' Then '' Else ', '+TSPL_TRANSPORT_MASTER.Add2 End) as [Transporter Address]" & _
                    " from TSPL_TRANSFER_ORDER_HEAD" & _
                    " left join TSPL_TRANSFER_ORDER_HEAD TransO on TSPL_TRANSFER_ORDER_HEAD.TransferOutNo=TransO.Document_No " & _
                   " LEFT OUTER JOIN TSPL_VEHICLE_MASTER ON TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code" & _
                    " LEFT OUTER JOIN TSPL_TRANSPORT_MASTER ON TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_TRANSFER_ORDER_HEAD.Transport_Id " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER FromLoc ON FromLoc.Location_Code =TSPL_TRANSFER_ORDER_HEAD.From_Location " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ToLoc on ToLoc.Location_Code =TSPL_TRANSFER_ORDER_HEAD.To_Location " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER GITFromLoc ON GITFromLoc.GIT_Location =TSPL_TRANSFER_ORDER_HEAD.From_Location " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER GITToLoc on GITToLoc.GIT_Location =TSPL_TRANSFER_ORDER_HEAD.To_Location " & _
                    " left outer join TSPL_LOCATION_MASTER  as MainLocationFrom on MainLocationFrom.Location_Code = " & _
                    " (case when FromLoc.Is_Section='N' AND FromLoc.Is_Sub_Location='N' then FromLoc.LOCATION_CODE else FromLoc.MAIN_Location_Code end) " & _
                    " left outer join TSPL_LOCATION_MASTER  as MainLocationTo on MainLocationTo.Location_Code = " & _
                    " (case when ToLoc.Is_Section='N' AND ToLoc.Is_Sub_Location='N' then ToLoc.LOCATION_CODE else ToLoc.MAIN_Location_Code end) " & _
                    " WHERE TSPL_TRANSFER_ORDER_HEAD.status=1 and CONVERT(DATE,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)>='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'" & _
                    " AND CONVERT(DATE,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'" & _
                    " and Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' Then TSPL_TRANSFER_ORDER_HEAD.From_Location Else TSPL_TRANSFER_ORDER_HEAD.To_Location End in (" & clsCommon.GetMulcallString(cbgLocation.CheckedValue) & ") " & _
                    " Union All " & _
                    " Select Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then 'Purchase Return' else 'Sales Return' End as [In/Out],Case When isnull(TSPL_TRANSFER_ORDER_HEAD.Is_AgainstFormF,0)=1 then 'F-Form' else '' end as [Against Form], " & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Location_Code  + ' (' + TSPL_TRANSFER_ORDER_HEAD.To_Location +')'  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Location_Code  + ' (' + TSPL_TRANSFER_ORDER_HEAD.To_Location +')'  ELSE TSPL_TRANSFER_ORDER_HEAD.To_Location END  " & _
                    "  as [From Location]," & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Location_Desc  + ' (' + " + "ToLoc.Location_Desc " + "+')'" + "  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Location_Desc  + ' (' + ToLoc.Location_Desc +')' ELSE ToLoc.Location_Desc END " & _
                    "  as [From Location Desc]," & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add1 when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add1  Else ToLoc.Add1 end + " & _
                    "  CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add2  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add2 Else ToLoc.Add2 End + " & _
                    "  CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add3  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add3  Else ToLoc.Add3 End + " & _
                    "  CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add4  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add4 Else ToLoc.Add4 End  " & _
                    " as [From Location Address], " & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.gstno  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.gstno  Else ToLoc.gstno End as [From Location GSTIN No], " & _
                    " CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Location_Code  + ' (' + " + "TSPL_TRANSFER_ORDER_HEAD.From_Location " + "+')'" + "  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Location_Code  + ' (' + TSPL_TRANSFER_ORDER_HEAD.From_Location +')'  ELSE TSPL_TRANSFER_ORDER_HEAD.From_Location END" & _
                    " as [To Location], " & _
                    " CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Location_Desc + ' (' + " + "FromLoc.Location_Desc " + "+')'" + "  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Location_Desc  + ' (' + FromLoc.Location_Desc +')' ELSE FromLoc.Location_Desc END" & _
                    "  as [To Location Desc], " & _
                    " CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add1  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add1 Else FromLoc.Add1 end + " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add2  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add2 Else FromLoc.Add2 End + " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add3  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add3 Else FromLoc.Add3 End + " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add4  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add4  Else FromLoc.Add4 End  " & _
                    "  as [To Location Address], " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.gstno  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.gstno  Else FromLoc.gstno End   as [To Location GSTIN No], " & _
                    " TSPL_TRANSFER_RETURN.Document_No as [Document No], " & _
                    " TSPL_TRANSFER_RETURN.Transfer_No [SourceDoc/Invoice Number], convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as [Invoice/ DocDate], -TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt as [Total Amount of Invoice], STUFF((Select DISTINCT ', '+TSPL_ITEM_MASTER.Cheapter_Heads from TSPL_TRANSFER_ORDER_DETAIL LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_TRANSFER_ORDER_DETAIL.Item_Code WHERE TSPL_TRANSFER_ORDER_DETAIL.Document_No=TSPL_TRANSFER_ORDER_HEAD.Document_No For XML Path('')), 1, 1, '') as [commodity], case when isnull(TSPL_TRANSPORT_MASTER.Transporter_Name,'')='' then TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual else TSPL_TRANSPORT_MASTER.Transporter_Name end as [Transporter Name], (TSPL_TRANSPORT_MASTER.Add1+Case WHEN ISNULL(TSPL_TRANSPORT_MASTER.Add2,'')='' Then '' Else ', '+TSPL_TRANSPORT_MASTER.Add2 End) as [Transporter Address] from TSPL_TRANSFER_ORDER_HEAD " & _
                    " left outer join TSPL_TRANSFER_RETURN on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_RETURN.Transfer_No " & _
                    " left join TSPL_TRANSFER_ORDER_HEAD TransO on TSPL_TRANSFER_ORDER_HEAD.TransferOutNo=TransO.Document_No " & _
                    " LEFT OUTER JOIN TSPL_VEHICLE_MASTER ON TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code " & _
                    " LEFT OUTER JOIN TSPL_TRANSPORT_MASTER ON TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_TRANSFER_ORDER_HEAD.Transport_Id " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER FromLoc ON FromLoc.Location_Code =TSPL_TRANSFER_ORDER_HEAD.From_Location " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ToLoc on ToLoc.Location_Code =TSPL_TRANSFER_ORDER_HEAD.To_Location " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER GITFromLoc ON GITFromLoc.GIT_Location =TSPL_TRANSFER_ORDER_HEAD.From_Location " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER GITToLoc on GITToLoc.GIT_Location =TSPL_TRANSFER_ORDER_HEAD.To_Location " & _
                    " left outer join TSPL_LOCATION_MASTER  as MainLocationFrom on MainLocationFrom.Location_Code = " & _
                    " (case when FromLoc.Is_Section='N' AND FromLoc.Is_Sub_Location='N' then FromLoc.LOCATION_CODE else FromLoc.MAIN_Location_Code end) " & _
                    " left outer join TSPL_LOCATION_MASTER  as MainLocationTo on MainLocationTo.Location_Code = " & _
                    " (case when ToLoc.Is_Section='N' AND ToLoc.Is_Sub_Location='N' then ToLoc.LOCATION_CODE else ToLoc.MAIN_Location_Code end) " & _
                    " WHERE TSPL_TRANSFER_ORDER_HEAD.status=1 and isnull(TSPL_TRANSFER_RETURN.Transfer_No ,'')<>'' " & _
                    " And CONVERT(DATE,TSPL_TRANSFER_RETURN.Document_Date,103)>='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'" & _
                    " AND CONVERT(DATE,TSPL_TRANSFER_RETURN.Document_Date,103)<='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'" & _
                    " and Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' Then TSPL_TRANSFER_ORDER_HEAD.From_Location Else TSPL_TRANSFER_ORDER_HEAD.To_Location End in (" & clsCommon.GetMulcallString(cbgLocation.CheckedValue) & ") " & _
                    " ) Final ORDER BY convert(date,Final.[Invoice/ DocDate] ,103) "

                Else

                    qry = " Select Final.* from ( Select Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then 'IN' else 'OUT' End as [In/Out],Case When isnull(TSPL_TRANSFER_ORDER_HEAD.Is_AgainstFormF,0)=1 then 'F-Form' else '' end as [Against Form]," & _
                                        " CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Location_Code  + ' (' + " + "TSPL_TRANSFER_ORDER_HEAD.From_Location " + "+')'" + "  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Location_Code  + ' (' + TSPL_TRANSFER_ORDER_HEAD.From_Location +')'  ELSE TSPL_TRANSFER_ORDER_HEAD.From_Location END" & _
                    " as [From Location], " & _
                    " CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Location_Desc + ' (' + " + "FromLoc.Location_Desc " + "+')'" + "  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Location_Desc  + ' (' + FromLoc.Location_Desc +')' ELSE FromLoc.Location_Desc END" & _
                    "  as [From Location Desc], " & _
                    " CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add1  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add1 Else FromLoc.Add1 end + " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add2  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add2 Else FromLoc.Add2 End + " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add3  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add3 Else FromLoc.Add3 End + " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add4  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add4  Else FromLoc.Add4 End  " & _
                    "  as [From Location Address], " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.gstno  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.gstno  Else FromLoc.gstno End   as [From Location GSTIN No], " & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Location_Code  + ' (' + TSPL_TRANSFER_ORDER_HEAD.To_Location +')'  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Location_Code  + ' (' + TSPL_TRANSFER_ORDER_HEAD.To_Location +')'  ELSE TSPL_TRANSFER_ORDER_HEAD.To_Location END  " & _
                    "  as [To Location]," & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Location_Desc  + ' (' + " + "ToLoc.Location_Desc " + "+')'" + "  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Location_Desc  + ' (' + ToLoc.Location_Desc +')' ELSE ToLoc.Location_Desc END " & _
                    "  as [To Location Desc]," & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add1 when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add1  Else ToLoc.Add1 end + " & _
                    "  CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add2  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add2 Else ToLoc.Add2 End + " & _
                    "  CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add3  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add3  Else ToLoc.Add3 End + " & _
                    "  CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add4  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add4 Else ToLoc.Add4 End  " & _
                    " as [To Location Address], " & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.gstno  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.gstno  Else ToLoc.gstno End as [To Location GSTIN No] " & _
                  ", TSPL_TRANSFER_ORDER_HEAD.Document_No as [Document No]," & _
                  " Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then TSPL_TRANSFER_ORDER_HEAD.TransferOutNo Else '' End as [SourceDoc/Invoice Number], convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as [Invoice/ DocDate] " & _
                  " ,TSPL_TRANSFER_ORDER_DETAIL.item_code AS [Item Code],TSPL_ITEM_MASTER.item_desc as [Item Description],TSPL_TRANSFER_ORDER_DETAIL.Unit_code as UOM " & _
                  " ,Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then TSPL_TRANSFER_ORDER_DETAIL.In_Qty else TSPL_TRANSFER_ORDER_DETAIL.Out_Qty End as [Qty] " & _
                  " ,TSPL_TRANSFER_ORDER_DETAIL.amount As [Amount] " & _
                  " , TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt as [Total Amount of Invoice]," & _
                  " TSPL_ITEM_MASTER.Cheapter_Heads as [commodity]," & _
                  " case when isnull(TSPL_TRANSPORT_MASTER.Transporter_Name,'')='' then TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual else TSPL_TRANSPORT_MASTER.Transporter_Name end as [Transporter Name]," & _
                  " (TSPL_TRANSPORT_MASTER.Add1+Case WHEN ISNULL(TSPL_TRANSPORT_MASTER.Add2,'')='' Then '' Else ', '+TSPL_TRANSPORT_MASTER.Add2 End) as [Transporter Address]" & _
                  " from TSPL_TRANSFER_ORDER_DETAIL left join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_ORDER_DETAIL.Document_No " & _
                  " left join TSPL_TRANSFER_ORDER_HEAD TransO on TSPL_TRANSFER_ORDER_HEAD.TransferOutNo=TransO.Document_No " & _
                  " LEFT OUTER JOIN TSPL_VEHICLE_MASTER ON TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code" & _
                  " LEFT OUTER JOIN TSPL_TRANSPORT_MASTER ON TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_TRANSFER_ORDER_HEAD.Transport_Id " & _
                  " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_TRANSFER_ORDER_DETAIL.Item_Code" & _
                  " LEFT OUTER JOIN TSPL_LOCATION_MASTER FromLoc ON FromLoc.Location_Code =TSPL_TRANSFER_ORDER_HEAD.From_Location " & _
                  " LEFT OUTER JOIN TSPL_LOCATION_MASTER ToLoc on ToLoc.Location_Code =TSPL_TRANSFER_ORDER_HEAD.To_Location " & _
                  " LEFT OUTER JOIN TSPL_LOCATION_MASTER GITFromLoc ON GITFromLoc.GIT_Location =TSPL_TRANSFER_ORDER_HEAD.From_Location " & _
                  " LEFT OUTER JOIN TSPL_LOCATION_MASTER GITToLoc on GITToLoc.GIT_Location =TSPL_TRANSFER_ORDER_HEAD.To_Location " & _
                  " left outer join TSPL_LOCATION_MASTER  as MainLocationFrom on MainLocationFrom.Location_Code = " & _
                    " (case when FromLoc.Is_Section='N' AND FromLoc.Is_Sub_Location='N' then FromLoc.LOCATION_CODE else FromLoc.MAIN_Location_Code end) " & _
                    " left outer join TSPL_LOCATION_MASTER  as MainLocationTo on MainLocationTo.Location_Code = " & _
                    " (case when ToLoc.Is_Section='N' AND ToLoc.Is_Sub_Location='N' then ToLoc.LOCATION_CODE else ToLoc.MAIN_Location_Code end) " & _
                  " WHERE TSPL_TRANSFER_ORDER_HEAD.status=1 and CONVERT(DATE,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)>='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'" & _
                  " AND CONVERT(DATE,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'" & _
                  " and Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' Then TSPL_TRANSFER_ORDER_HEAD.From_Location Else TSPL_TRANSFER_ORDER_HEAD.To_Location End in (" & clsCommon.GetMulcallString(cbgLocation.CheckedValue) & ") " & _
                  " Union All " & _
                  " Select Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then 'Purchase Return' else 'Sales Return' End as [In/Out],Case When isnull(TSPL_TRANSFER_ORDER_HEAD.Is_AgainstFormF,0)=1 then 'F-Form' else '' end as [Against Form], " & _
                                      " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Location_Code  + ' (' + TSPL_TRANSFER_ORDER_HEAD.To_Location +')'  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Location_Code  + ' (' + TSPL_TRANSFER_ORDER_HEAD.To_Location +')'  ELSE TSPL_TRANSFER_ORDER_HEAD.To_Location END  " & _
                    "  as [From Location]," & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Location_Desc  + ' (' + " + "ToLoc.Location_Desc " + "+')'" + "  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Location_Desc  + ' (' + ToLoc.Location_Desc +')' ELSE ToLoc.Location_Desc END " & _
                    "  as [From Location Desc]," & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add1 when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add1  Else ToLoc.Add1 end + " & _
                    "  CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add2  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add2 Else ToLoc.Add2 End + " & _
                    "  CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add3  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add3  Else ToLoc.Add3 End + " & _
                    "  CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.Add4  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.Add4 Else ToLoc.Add4 End  " & _
                    " as [From Location Address], " & _
                    " CASE WHEN ToLoc.git_type='Y' THEN GITToLoc.gstno  when (ToLoc.Is_Section='Y' OR ToLoc.Is_Sub_Location='Y') THEN MainLocationTo.gstno  Else ToLoc.gstno End as [From Location GSTIN No], " & _
                    " CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Location_Code  + ' (' + " + "TSPL_TRANSFER_ORDER_HEAD.From_Location " + "+')'" + "  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Location_Code  + ' (' + TSPL_TRANSFER_ORDER_HEAD.From_Location +')'  ELSE TSPL_TRANSFER_ORDER_HEAD.From_Location END" & _
                    " as [To Location], " & _
                    " CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Location_Desc + ' (' + " + "FromLoc.Location_Desc " + "+')'" + "  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Location_Desc  + ' (' + FromLoc.Location_Desc +')' ELSE FromLoc.Location_Desc END" & _
                    "  as [To Location Desc], " & _
                    " CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add1  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add1 Else FromLoc.Add1 end + " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add2  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add2 Else FromLoc.Add2 End + " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add3  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add3 Else FromLoc.Add3 End + " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.Add4  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.Add4  Else FromLoc.Add4 End  " & _
                    "  as [To Location Address], " & _
                    "  CASE WHEN FromLoc.git_type='Y' THEN GITFromLoc.gstno  when (FromLoc.Is_Section='Y' OR FromLoc.Is_Sub_Location='Y') THEN MainLocationFrom.gstno  Else FromLoc.gstno End   as [To Location GSTIN No], " & _
                  " TSPL_TRANSFER_RETURN.Document_No as [Document No], " & _
                  " TSPL_TRANSFER_RETURN.Transfer_No [SourceDoc/Invoice Number], convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as [Invoice/ DocDate] " & _
                  " ,TSPL_TRANSFER_ORDER_DETAIL.item_code AS [Item Code],TSPL_ITEM_MASTER.item_desc as [Item Description],TSPL_TRANSFER_ORDER_DETAIL.Unit_code as UOM " & _
                  " ,- (Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' Then TSPL_TRANSFER_ORDER_DETAIL.In_Qty else TSPL_TRANSFER_ORDER_DETAIL.Out_Qty End) as [Qty] " & _
                  " , -TSPL_TRANSFER_ORDER_DETAIL.amount As [Amount] " & _
                  " , -TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt as [Total Amount of Invoice], TSPL_ITEM_MASTER.Cheapter_Heads as [commodity], case when isnull(TSPL_TRANSPORT_MASTER.Transporter_Name,'')='' then TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual else TSPL_TRANSPORT_MASTER.Transporter_Name end as [Transporter Name], (TSPL_TRANSPORT_MASTER.Add1+Case WHEN ISNULL(TSPL_TRANSPORT_MASTER.Add2,'')='' Then '' Else ', '+TSPL_TRANSPORT_MASTER.Add2 End) as [Transporter Address] " & _
                  " from TSPL_TRANSFER_ORDER_DETAIL left join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_ORDER_DETAIL.Document_No  " & _
                  " left outer join TSPL_TRANSFER_RETURN on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_RETURN.Transfer_No " & _
                  " left join TSPL_TRANSFER_ORDER_HEAD TransO on TSPL_TRANSFER_ORDER_HEAD.TransferOutNo=TransO.Document_No " & _
                  " LEFT OUTER JOIN TSPL_VEHICLE_MASTER ON TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code " & _
                  " LEFT OUTER JOIN TSPL_TRANSPORT_MASTER ON TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_TRANSFER_ORDER_HEAD.Transport_Id " & _
                  " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_TRANSFER_ORDER_DETAIL.Item_Code" & _
                  " LEFT OUTER JOIN TSPL_LOCATION_MASTER FromLoc ON FromLoc.Location_Code =TSPL_TRANSFER_ORDER_HEAD.From_Location " & _
                  " LEFT OUTER JOIN TSPL_LOCATION_MASTER ToLoc on ToLoc.Location_Code =TSPL_TRANSFER_ORDER_HEAD.To_Location " & _
                  " LEFT OUTER JOIN TSPL_LOCATION_MASTER GITFromLoc ON GITFromLoc.GIT_Location =TSPL_TRANSFER_ORDER_HEAD.From_Location " & _
                  " LEFT OUTER JOIN TSPL_LOCATION_MASTER GITToLoc on GITToLoc.GIT_Location =TSPL_TRANSFER_ORDER_HEAD.To_Location " & _
                  " left outer join TSPL_LOCATION_MASTER  as MainLocationFrom on MainLocationFrom.Location_Code = " & _
                    " (case when FromLoc.Is_Section='N' AND FromLoc.Is_Sub_Location='N' then FromLoc.LOCATION_CODE else FromLoc.MAIN_Location_Code end) " & _
                    " left outer join TSPL_LOCATION_MASTER  as MainLocationTo on MainLocationTo.Location_Code = " & _
                    " (case when ToLoc.Is_Section='N' AND ToLoc.Is_Sub_Location='N' then ToLoc.LOCATION_CODE else ToLoc.MAIN_Location_Code end) " & _
                  " WHERE TSPL_TRANSFER_ORDER_HEAD.status=1 and isnull(TSPL_TRANSFER_RETURN.Transfer_No ,'')<>'' " & _
                  " And CONVERT(DATE,TSPL_TRANSFER_RETURN.Document_Date,103)>='" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'" & _
                  " AND CONVERT(DATE,TSPL_TRANSFER_RETURN.Document_Date,103)<='" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'" & _
                  " and Case When TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' Then TSPL_TRANSFER_ORDER_HEAD.From_Location Else TSPL_TRANSFER_ORDER_HEAD.To_Location End in (" & clsCommon.GetMulcallString(cbgLocation.CheckedValue) & ") " & _
                  " ) Final ORDER BY convert(date,Final.[Invoice/ DocDate] ,103) "

                End If

                dt = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "No Record Found")
                Else
                    gv.DataSource = Nothing
                    gv.Columns.Clear()
                    gv.Rows.Clear()
                    gv.GroupDescriptors.Clear()
                    gv.MasterTemplate.SummaryRowsBottom.Clear()
                    gv.ShowGroupPanel = False
                    gv.EnableFiltering = True
                    gv.DataSource = dt
                    SetGridFormation()
                    ReStoreGridLayout()
                    RadPageView1.SelectedPage = RadPageViewPage2
                End If


                Dim strTemp As String = ""

            ElseIf IsPrint = Exporter.Excel Then
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            common.clsCommon.MyMessageBoxShow(Me, err.Message)
        End Try
    End Sub

    Sub SetGridFormation()
        For Each col As GridViewColumn In gv.Columns
            col.BestFit()
        Next
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = ReportID + IIf(ChkDetail.IsChecked = True, "D", "")
        TemplateGridview = gv
        LoadData(Exporter.Refresh)
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = fromDate.Value
        LoadLocation()
        gv.DataSource = Nothing
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If (gv.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export")
            Exit Sub
        End If
        LoadData(Exporter.Excel)
    End Sub

    Private Sub btnPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdf.Click
        If (gv.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export")
            Exit Sub
        End If
        LoadData(Exporter.PDF)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub


End Class
