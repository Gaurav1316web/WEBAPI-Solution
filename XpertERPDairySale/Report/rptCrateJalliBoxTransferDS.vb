Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'=============================added by preeti gupta=========================='ticket no []
Public Class RptCrateJalliBoxTransferDS
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptCrateJalliReportForTransfer)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum


    Private Sub txtBranch__My_Click(sender As Object, e As EventArgs) Handles txtBranch._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtBranch.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtBranch.arrValueMember, txtBranch.arrDispalyMember)
    End Sub

    Private Sub RptCrateJalliBoxTransferDS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R ")
        Reset()
    End Sub
    Public Sub loaddata()
        Dim dt As DataTable = Nothing
        Dim qry As String = Nothing
        Dim openingQry As String = Nothing
        Dim closingQry As String = Nothing
        '===========================For Detail Qry=====================================
        openingQry = " select  FromLoc,ToLoc,  sum([Open Crate Qty]*type) as [Open Crate Qty],sum([Open Jaali Qty]*type) as [Open Jaali Qty],sum([Open Box Qty]*type) as [Open Box Qty], SUM([Crate Out]*type) [Crate Out] ,SUM([Jaali Out]*type) [Jaali Out] ,SUM([Box Out]*type) [Box Out],SUM([Crate Received]*type) [Crate Received],  SUM([Jaali Received]*type) [Jaali Received],  SUM([Box Received]*type) [Box Received], SUM([Crate Adjustment]*type) [Crate Adjustment],  SUM([Jaali Adjustment]*type) [Jaali Adjustment], SUM([Box Adjustment]*type) [Box Adjustment] from ( " & _
                    " SELECT 'tranOut' as Doc,  OH.From_Location as FromLoc,ToLocGIT.Location_Code as ToLoc,-1 as type, OH.Document_No [Document] ,  OH.Document_Date  [Document Date],( CASE WHEN OH.Transfer_Type = 'O' THEN OH.From_Location    ELSE  OH.To_Location   END )  [LOCATION], " & _
                    " OH.Vehicle_Code   [Veh Code] ,  OH.Vehicle_No [Veh Number] ,    OH.Crate_Out   [Open Crate Qty], OH.jaali_Out  [Open Jaali Qty], OH.Box_Out [Open Box Qty], 0   [Crate Out], 0  [Jaali Out], 0	 [Box Out], 0   [Crate Received],  0  [Jaali Received],  0  [Box Received], 0   [Crate Adjustment], 0     [Jaali Adjustment], 0     [Box Adjustment] FROM " & _
                    " TSPL_TRANSFER_ORDER_HEAD OH left outer join TSPL_LOCATION_MASTER as ToLocGIT on oh.To_Location=ToLocGIT.GIT_Location  WHERE OH.Transfer_Type = 'O' " & _
                    " UNION ALL" & _
                    " SELECT 'transIn' as Doc,OH.To_Location  as FromLoc,FromLocGIT.From_Location   as ToLoc,1 as type, OH.Document_No [Document] , OH.Document_Date    [Document Date], (CASE WHEN OH.Transfer_Type = 'I' THEN  OH.To_Location    ELSE OH.From_Location    END )  [LOCATION] , OH.Vehicle_Code  [Veh Code] , OH.Vehicle_No  [Veh Number] , OH.Crate_IN [Open Crate Qty], OH.jaali_IN    [Open Jalli Qty], OH.Box_IN [Open Box Qty], 0  [Crate Out],0    [Jaali Out],0	 [Box Out],0 [Crate Received], 0    [Jaali Received], 0   [Box Received],0   [Crate Adjustment],  0 [Jaali Adjustment],0     [Box Adjustment] FROM  " & _
                    " TSPL_TRANSFER_ORDER_HEAD OH left outer join TSPL_TRANSFER_ORDER_HEAD as FromLocGIT  on FromLocGIT.Document_No=OH.TransferOutNo   WHERE OH.Transfer_Type = 'I'  " & _
                    " union all" & _
                    " SELECT 'CrateOut' as doc,H.Location_Code   as FromLoc, D.Branch_Code as ToLoc,-1 as type, H.Document_No [Document],  CONVERT(varchar, H.Document_Date, 120) [Document Date], (CASE WHEN H.Type = 'O' THEN H.Location_Code     ELSE H.Location_Code  END ) [LOCATION], H.Vehicle_Code  [Veh Code], V.Number  [Veh Number], " & _
                    " d.OutQty [Open Crate Qty], d.jaaliOutQty    [Open Jalli Qty], d.boxOutQty [Open Box Qty]," & _
                    "  0    [Crate Out],0  [Jaali Out],0   [Box Out],  0	 [Crate Received],0  [Jaali Received],  0  [Box Received],  0   [Crate Adjustment],  0  [Jaali Adjustment],  0  [Box Adjustment] FROM TSPL_CRATE_RECEIVED_HEAD_TRANSFER H LEFT JOIN TSPL_CRATE_RECEIVED_DETAIL_TRANSFER D ON H.Document_No = D.Document_No  LEFT JOIN TSPL_LOCATION_MASTER L ON H.Location_Code = L.Location_Code LEFT JOIN TSPL_VEHICLE_MASTER V ON H.Vehicle_Code = V.Vehicle_Id WHERE 2 = 2  AND H.Type = 'O' " & _
                    " union all " & _
                    " SELECT 'CrateIN' as   doc,H.Location_Code   as FromLoc, D.Branch_Code as ToLoc,1 as type, H.Document_No [Document], CONVERT(varchar, H.Document_Date, 120) [Document Date],CASE WHEN H.Type = 'I'   THEN    D.Branch_Code  ELSE  D.Branch_Code   END   [LOCATION] , H.Vehicle_Code  [Veh Code], V.Number   [Veh Number]," & _
                    " isnull(D.CrateQtyRecd,0)+isnull(D.Adjustment,0) [Open Crate Qty], isnull(D.JaaliQtyRecd,0)+isnull(D.jaaliAdjustment,0)     [Open Jalli Qty], isnull(D.BoxQtyRecd,0)+ isnull(D.boxAdjustment,0) [Open Box Qty] , 0    [Crate Out],0  [Jaali Out],0   [Box Out],  0	 [Crate Received],0  [Jaali Received],  0  [Box Received],  0   [Crate Adjustment],  0  [Jaali Adjustment],  0  [Box Adjustment]  FROM TSPL_CRATE_RECEIVED_HEAD_TRANSFER H  LEFT JOIN TSPL_CRATE_RECEIVED_DETAIL_TRANSFER D ON H.Document_No = D.Document_No LEFT JOIN TSPL_LOCATION_MASTER L ON H.Location_Code = L.Location_Code LEFT JOIN TSPL_VEHICLE_MASTER V ON H.Vehicle_Code = V.Vehicle_Id WHERE 2 = 2 AND H.Type = 'I' " & _
                    ") as opening WHERE convert(date,[Document Date],103)<(convert(date,'" + fromDate.Value + "',103)) "


        If txtBranch.arrValueMember IsNot Nothing AndAlso txtBranch.arrValueMember.Count > 0 Then
            openingQry += " and Opening.FromLoc  in (" + clsCommon.GetMulcallString(txtBranch.arrValueMember) + ") " + Environment.NewLine
        End If
        openingQry += " group by FromLoc,ToLoc "

        closingQry = "SELECT  FromLoc," & _
                    "  ToLoc,  0 as [Open Crate Qty],0 as [Open Jalli Qty],0 as [Open Box Qty], [Crate Out],  [Jaali Out], [Box Out],  [Crate Received],  [Jaali Received],  [Box Received], [Crate Adjustment], [Jaali Adjustment],  [Box Adjustment] FROM (" & _
                    "  SELECT H.Location_Code as FromLoc,D.Branch_Code as ToLoc,'CrateOut' as doc,-1 as type, H.Document_No [Document],  CONVERT(varchar, H.Document_Date, 120) [Document Date], (CASE WHEN H.Type = 'O' THEN H.Location_Code     ELSE H.Location_Code  END ) [LOCATION] , H.Vehicle_Code  [Veh Code], V.Number  [Veh Number], " & _
                    " d.OutQty    [Crate Out],d.jaaliOutQty  [Jaali Out],d.boxOutQty    [Box Out],  0	 [Crate Received],0  [Jaali Received],  0  [Box Received], " & _
                    " 0   [Crate Adjustment],  0  [Jaali Adjustment],  0  [Box Adjustment] FROM TSPL_CRATE_RECEIVED_HEAD_TRANSFER H LEFT JOIN TSPL_CRATE_RECEIVED_DETAIL_TRANSFER D ON H.Document_No = D.Document_No  LEFT JOIN TSPL_LOCATION_MASTER L ON H.Location_Code = L.Location_Code LEFT JOIN TSPL_VEHICLE_MASTER V ON H.Vehicle_Code = V.Vehicle_Id WHERE 2 = 2  AND H.Type = 'O' " & _
                    " UNION ALL " & _
                    "SELECT OH.From_Location as FromLoc,ToLocGIT.Location_Code  as ToLoc,'tranOut' as doc,-1 as type, OH.Document_No [Document] , OH.Document_Date [Document Date],  ( CASE WHEN OH.Transfer_Type = 'O' THEN OH.From_Location    ELSE  OH.To_Location   END )  [LOCATION] , OH.Vehicle_Code  [Veh Code] ,  OH.Vehicle_No   [Veh Number] ," & _
                    " OH.Crate_Out  [Crate Out], OH.jaali_Out   [Jaali Out], OH.Box_Out [Box Out], 0  [Crate Received], 0   [Jaali Received],0  [Box Received],0    [Crate Adjustment], 0   [Jaali Adjustment], 0  [Box Adjustment] FROM " & _
                    " TSPL_TRANSFER_ORDER_HEAD OH left outer join TSPL_LOCATION_MASTER as ToLocGIT on oh.To_Location=ToLocGIT.GIT_Location   WHERE OH.Transfer_Type = 'O'  " & _
                    " union all " & _
                    " SELECT H.Location_Code   as FromLoc, D.Branch_Code as ToLoc,'CrateIN' as doc,1 as type, H.Document_No [Document], CONVERT(varchar, H.Document_Date, 120) [Document Date],H.Location_Code as   [LOCATION] , H.Vehicle_Code  [Veh Code], V.Number   [Veh Number],0  [Crate Out], 0  [Jaali Out], 0 [Box Out],D.CrateQtyRecd	 [Crate Received], D.JaaliQtyRecd  [Jaali Received],  D.BoxQtyRecd  [Box Received],  D.Adjustment   [Crate Adjustment],  D.jaaliAdjustment   [Jaali Adjustment], D.boxAdjustment  [Box Adjustment] " & _
                    " FROM TSPL_CRATE_RECEIVED_HEAD_TRANSFER H  LEFT JOIN TSPL_CRATE_RECEIVED_DETAIL_TRANSFER D ON H.Document_No = D.Document_No LEFT JOIN TSPL_LOCATION_MASTER L ON H.Location_Code = L.Location_Code LEFT JOIN TSPL_VEHICLE_MASTER V ON H.Vehicle_Code = V.Vehicle_Id WHERE 2 = 2 AND H.Type = 'I' " & _
                    " UNION ALL " & _
                    " SELECT OH.To_Location as FromLoc,FromLocGIT.From_Location   as ToLoc,'transIn' as doc,1 as type, OH.Document_No  [Document] ,OH.Document_Date  [Document Date], (CASE WHEN OH.Transfer_Type = 'I' THEN  OH.To_Location    ELSE OH.From_Location    END )  [LOCATION] , OH.Vehicle_Code    [Veh Code] , OH.Vehicle_No   [Veh Number] , 0  [Crate Out],0  [Jaali Out],0 [Box Out],OH.Crate_IN  [Crate Received], OH.jaali_IN  [Jaali Received], OH.Box_IN  [Box Received],0   [Crate Adjustment],0   [Jaali Adjustment],0  [Box Adjustment]FROM " & _
                    "TSPL_TRANSFER_ORDER_HEAD OH left outer join TSPL_TRANSFER_ORDER_HEAD as FromLocGIT  on FromLocGIT.Document_No=OH.TransferOutNo WHERE OH.Transfer_Type = 'I' " & _
                    ") FINAL " & _
                    " WHERE convert(date,[Document Date] ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,[Document Date],103)<=convert(date,'" + ToDate.Value + "',103)   "
        If txtBranch.arrValueMember IsNot Nothing AndAlso txtBranch.arrValueMember.Count > 0 Then
            closingQry += " and FromLoc  in (" + clsCommon.GetMulcallString(txtBranch.arrValueMember) + ") " + Environment.NewLine
            End If
        If ChkDetail.IsChecked Then
            qry = " SELECT  from_location.Location_Desc as From_Location_Name,To_Location.Location_Desc as To_Location_Name,* from  ( " & _
                "select TSPL_TRANSFER_ORDER_HEAD.Document_No ,TSPL_TRANSFER_ORDER_HEAD.Document_Date , " & _
                "case when TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then  FromLoc.From_Location else TSPL_TRANSFER_ORDER_HEAD.From_Location end  as  From_Location, " & _
                "case when TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' then  ToLoc.Location_Code else TSPL_TRANSFER_ORDER_HEAD.To_Location end   as  To_Location , " & _
                "TSPL_TRANSFER_ORDER_HEAD.Transfer_Type,case when TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then  TSPL_TRANSFER_ORDER_HEAD.Crate_IN else 0 end as Crate_IN  , " & _
                "case when TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then  TSPL_TRANSFER_ORDER_HEAD.jaali_IN else 0 end as jaali_IN , " & _
                "case when TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='I' then  TSPL_TRANSFER_ORDER_HEAD.Box_IN else 0 end as Box_IN , " & _
                "case when TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' then  TSPL_TRANSFER_ORDER_HEAD.Crate_Out else 0 end as Crate_Out , " & _
                "case when TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' then  TSPL_TRANSFER_ORDER_HEAD.jaali_Out else 0 end as jaali_Out , " & _
                "case when TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' then TSPL_TRANSFER_ORDER_HEAD.Box_Out else 0 end as Box_Out  , " & _
                "0  as CrateAdjQty,0  as JaaliAdjQty, 0  as BoxAdjQty  from TSPL_TRANSFER_ORDER_HEAD " & _
                "left outer join TSPL_LOCATION_MASTER as ToLoc on TSPL_TRANSFER_ORDER_HEAD.To_Location=ToLoc.GIT_Location " & _
                "left outer join TSPL_TRANSFER_ORDER_HEAD as FromLoc  on FromLoc.Document_No=TSPL_TRANSFER_ORDER_HEAD.TransferOutNo  " & _
                " WHERE convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) "
            If txtBranch.arrValueMember IsNot Nothing AndAlso txtBranch.arrValueMember.Count > 0 Then
                qry += " and case when TSPL_TRANSFER_ORDER_HEAD.Transfer_Type='O' then TSPL_TRANSFER_ORDER_HEAD.From_Location else TSPL_TRANSFER_ORDER_HEAD.To_Location end in (" + clsCommon.GetMulcallString(txtBranch.arrValueMember) + ") " + Environment.NewLine
                End If
            qry += " union all " & _
                    " select TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Document_No , TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Document_Date  ,case when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type='O' then TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Location_Code when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type='I' then TSPL_CRATE_RECEIVED_DETAIL_TRANSFER.Branch_Code end as From_Location, " & _
                    "case when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type='I' then TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Location_Code  when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type='O' then TSPL_CRATE_RECEIVED_DETAIL_TRANSFER.Branch_Code end as To_location ,TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type ," & _
                   " case when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type ='I' then TSPL_CRATE_RECEIVED_DETAIL_TRANSFER.CrateQtyRecd  else 0 end as CrateQtyRecd," & _
                    " case when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type ='I' then TSPL_CRATE_RECEIVED_DETAIL_TRANSFER.JaaliQtyRecd else 0 end as JaaliQtyRecd," & _
                    " case when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type ='I' then  TSPL_CRATE_RECEIVED_DETAIL_TRANSFER.BoxQtyRecd else 0 end as JaaliQtyRecd," & _
                    " case when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type ='O' then  TSPL_CRATE_RECEIVED_DETAIL_TRANSFER.OutQty else 0 end as CrateOutQty," & _
                    " case when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type ='O' then TSPL_CRATE_RECEIVED_DETAIL_TRANSFER.jaaliOutQty else 0 end  as jaaliOutQty," & _
                    " case when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type ='O' then TSPL_CRATE_RECEIVED_DETAIL_TRANSFER.boxOutQty else 0 end as boxOutQty " & _
                    " , case when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type ='I' then TSPL_CRATE_RECEIVED_DETAIL_TRANSFER.Adjustment else 0 end  as CrateAdjQty," & _
                    "  case when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type ='I' then TSPL_CRATE_RECEIVED_DETAIL_TRANSFER.jaaliAdjustment else 0 end as JaaliAdjQty, " & _
                    " case when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type ='I' then  TSPL_CRATE_RECEIVED_DETAIL_TRANSFER.boxAdjustment else 0 end as BoxAdjQty " & _
                    " from TSPL_CRATE_RECEIVED_DETAIL_TRANSFER " & _
                    " left join TSPL_CRATE_RECEIVED_HEAD_TRANSFER on TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Document_No =TSPL_CRATE_RECEIVED_DETAIL_TRANSFER.Document_No " & _
                    " WHERE convert(date,TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) "
            If txtBranch.arrValueMember IsNot Nothing AndAlso txtBranch.arrValueMember.Count > 0 Then
                qry += " and case when TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Type ='O' then TSPL_CRATE_RECEIVED_HEAD_TRANSFER.Location_Code else TSPL_CRATE_RECEIVED_DETAIL_TRANSFER.Branch_Code end  in (" + clsCommon.GetMulcallString(txtBranch.arrValueMember) + ") " + Environment.NewLine
                End If
            qry += "   ) as final" & _
                    " left join TSPL_LOCATION_MASTER as from_location on from_location.Location_Code =final.From_Location " & _
                    " left join TSPL_LOCATION_MASTER as To_Location on To_Location.Location_Code =final.To_Location "
        ElseIf ChkSummary.IsChecked Then
            qry = "     With CTETemp as (  Select FromLoc,ToLoc,FromLocDesc,ToLocDesc,FromLocType,ToLocType,   OpencrateQty, OpenJaaliQty, OpenBoxQty, CrateQtyRecd, JaaliQtyRecd, BoxQtyRecd,CrateOutQty, jaaliOutQty,boxOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty,SUM(CrateQtyClosing) OVER (Partition BY toloc ORDER BY toloc) as CrateQtyClosing,  SUM(JaaliQtyClosing) OVER (Partition BY toloc ORDER BY toloc) as JaaliQtyClosing,  SUM(BoxQtyClosing) OVER (Partition BY toloc ORDER BY toloc) as BoxQtyClosing, Row_Number() OVER (Partition BY toloc ORDER BY toloc) as RowNo from( " & _
                " select FromLoc,ToLoc,FromLocDesc,ToLocDesc,FromLocType,ToLocType, pp.OpencrateQty as OpencrateQty,pp.OpenJaaliQty  as OpenJaaliQty,pp.OpenBoxQty  as OpenBoxQty,pp.CrateQtyRecd  as CrateQtyRecd,pp.JaaliQtyRecd  as JaaliQtyRecd,pp.BoxQtyRecd  as BoxQtyRecd,pp.CrateOutQty  as CrateOutQty,pp.jaaliOutQty  as jaaliOutQty ,pp.boxOutQty  as boxOutQty ,pp.CrateQtyClosing as CrateQtyClosing, pp.JaaliQtyClosing as  JaaliQtyClosing, pp.BoxQtyClosing as BoxQtyClosing,pp.CrateAdjQty , pp.JaaliAdjQty  , pp.BoxAdjQty from  ( " & _
                " select  TSPL_LOCATION_MASTER.Type as [LOC TYPE],max(TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc,max(FromLocType.Location_Desc) as FromLocDesc,max(ToLocType.Location_Desc) as ToLocDesc,max(FromLocType.Type) as FromLocType,max(ToLocType.Type) as ToLocType, FromLoc,ToLoc, sum(xx.[Open Crate Qty]) as OpencrateQty,sum(xx.[Open Jaali Qty] ) as OpenJaaliQty,sum(xx.[Open Box Qty] )  as OpenBoxQty,sum(xx.[Crate Received]) as CrateQtyRecd,sum(xx.[Jaali Received]) as JaaliQtyRecd,sum(xx.[Box Received]) as BoxQtyRecd,sum(xx.[Crate Out] ) as CrateOutQty,sum(xx.[Jaali Out] ) as jaaliOutQty ,sum(xx.[Box Out] ) as boxOutQty,sum(xx.[Crate Adjustment] ) as CrateAdjQty ,sum(xx.[Jaali Adjustment] ) as JaaliAdjQty  ,sum(xx.[Box Adjustment] ) as BoxAdjQty ,(sum(xx.[Open Crate Qty]) - sum(xx.[Crate Out] ) + sum(xx.[Crate Received]) + sum(xx.[Crate Adjustment] )) as CrateQtyClosing, (sum(xx.[Open Jaali Qty]) - sum(xx.[Jaali Out]   ) + sum(xx.[Jaali Received]) + sum(xx.[Jaali Adjustment] )) as JaaliQtyClosing, (sum(xx.[Open Box Qty]) - sum(xx.[Box Out] ) + sum(xx.[Box Received]) + sum(xx.[Box Adjustment] )) as BoxQtyClosing from " & _
               " ( " & _
                " " & openingQry & " " & _
              " Union all " & _
              " " & closingQry & "" & _
              " ) as xx left outer join TSPL_LOCATION_MASTER FromLocType on xx.FromLoc=FromLocType.Location_Code left outer join TSPL_LOCATION_MASTER ToLocType on xx.ToLoc=ToLocType.Location_Code left outer join TSPL_LOCATION_MASTER  on xx.FromLoc=TSPL_LOCATION_MASTER.Location_Code where 2=2   GROUP BY FromLoc,ToLoc,TSPL_LOCATION_MASTER.Type,FromLocType.Type,ToLocType.Type   " & _
              ") as pp where 2=2   ) " & _
                " YYY ) Select  FromLocType + '  ' + FromLoc as FromLoc,ToLocType  + '   ' + ToLoc as ToLoc,FromLocDesc,ToLocDesc, OpencrateQty,OpenJaaliQty,OpenBoxQty, CrateQtyRecd, JaaliQtyRecd,BoxQtyRecd ,CrateOutQty, jaaliOutQty,boxOutQty, CrateAdjQty , JaaliAdjQty  , BoxAdjQty, OpencrateQty-CrateOutQty +CrateQtyRecd+CrateAdjQty as CrateQtyClosing, OpenJaaliQty-jaaliOutQty+JaaliQtyRecd+JaaliAdjQty as JaaliQtyClosing, OpenBoxQty-boxOutQty+BoxQtyRecd+BoxAdjQty as BoxQtyClosing  from (" & _
                "   Select CTETemp.FromLoc,CTETemp.ToLoc,CTETemp.FromLocDesc,CTETemp.ToLocDesc,CTETemp.FromLocType,CTETemp.ToLocType, CTETemp.OpencrateQty as OpencrateQty,  CTETemp.OpenJaaliQty as OpenJaaliQty,  CTETemp.OpenBoxQty as OpenBoxQty, CTETemp.CrateQtyRecd, CTETemp.JaaliQtyRecd, CTETemp.BoxQtyRecd, CTETemp.CrateOutQty, CTETemp.jaaliOutQty, CTETemp.boxOutQty, CTETemp.CrateAdjQty , CTETemp.JaaliAdjQty  , CTETemp.BoxAdjQty  from CTETemp  LEFt OUTER JOIN CTETemp CT1 ON CT1.ToLoc =CTETemp.ToLoc    AND (CTETemp.RowNo-CT1.RowNo)=1 ) ZZZ "
            End If
            '=====================================END Detail Qry=================================================================



        dt = clsDBFuncationality.GetDataTable(qry)
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.MasterView.Refresh()
        Gv1.DataSource = dt
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
            End If

        Gv1.BestFitColumns()
        If ChkSummary.IsChecked Then
            FormatGrid()
                'View()
        ElseIf ChkDetail.IsChecked Then
            'Dim view As TableViewDefinition = New TableViewDefinition()
            '    '  Gv1.MasterTemplate = TableViewDefinition
            'Gv1.ViewDefinition = view
            formatgridsummary()
            'ViewDetail()


            End If

        RadPageView1.SelectedPage = RadPageViewPage2
        ReStoreGridLayout()
    End Sub
    Sub View()
        If Gv1.Rows.Count > 0 Then
            ''ColumnGroupsViewDefinition view = New ColumnGroupsViewDefinition()
            Dim view As New ColumnGroupsViewDefinition()


            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("LOC TYPE").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Branch Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Branch Name").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("OPENING"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("OpencrateQty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("OpenJaaliQty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("OpenBoxQty").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("RECEIVE"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("CrateQtyRecd").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("JaaliQtyRecd").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("BoxQtyRecd").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("ISSUE"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("CrateOutQty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("jaaliOutQty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("boxOutQty").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("ADJUSTMENT"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("CrateAdjQty").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("JaaliAdjQty").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("BoxAdjQty").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("CLOSING"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("CrateQtyClosing").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("JaaliQtyClosing").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(Gv1.Columns("BoxQtyClosing").Name)


            Gv1.ViewDefinition = view


        End If

    End Sub
    Sub ViewDetail()
        If Gv1.Rows.Count > 0 Then
            ''ColumnGroupsViewDefinition view = New ColumnGroupsViewDefinition()
            Dim ViewDetail As New ColumnGroupsViewDefinition()


            ViewDetail.ColumnGroups.Add(New GridViewColumnGroup(""))
            ViewDetail.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            ViewDetail.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Document_No").Name)
            ViewDetail.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Document_Date").Name)
            ViewDetail.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("From_Location").Name)
            ViewDetail.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("From_Location_Name").Name)
            ViewDetail.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("To_Location").Name)
            ViewDetail.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("To_Location_Name").Name)
            ViewDetail.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Type").Name)


            ViewDetail.ColumnGroups.Add(New GridViewColumnGroup("RECEIVE"))
            ViewDetail.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            ViewDetail.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Crate_IN").Name)
            ViewDetail.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("jaali_IN").Name)
            ViewDetail.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Box_IN").Name)

            ViewDetail.ColumnGroups.Add(New GridViewColumnGroup("ISSUE"))
            ViewDetail.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            ViewDetail.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Crate_Out").Name)
            ViewDetail.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("jaali_Out").Name)
            ViewDetail.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Box_Out").Name)

            ViewDetail.ColumnGroups.Add(New GridViewColumnGroup("ADJUSTMENT"))
            ViewDetail.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            ViewDetail.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("CrateAdjQty").Name)
            ViewDetail.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("JaaliAdjQty").Name)
            ViewDetail.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("BoxAdjQty").Name)

            Gv1.ViewDefinition = ViewDetail


        End If

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Sub formatgridsummary()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).Width = 100
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Gv1.Columns("Document_No").IsVisible = True
        Gv1.Columns("Document_No").Width = 100
        Gv1.Columns("Document_No").HeaderText = "Doc No"

        Gv1.Columns("Document_Date").IsVisible = True
        Gv1.Columns("Document_Date").Width = 100
        Gv1.Columns("Document_Date").HeaderText = "Doc Date"


        Gv1.Columns("From_Location").IsVisible = True
        Gv1.Columns("From_Location").Width = 100
        Gv1.Columns("From_Location").HeaderText = " From Location Code"

        Gv1.Columns("From_Location_Name").IsVisible = True
        Gv1.Columns("From_Location_Name").Width = 100
        Gv1.Columns("From_Location_Name").HeaderText = " From Location Name"

     
        Gv1.Columns("To_Location").IsVisible = True
        Gv1.Columns("To_Location").Width = 100
        Gv1.Columns("To_Location").HeaderText = "To Loction Code"

        Gv1.Columns("To_Location_Name").IsVisible = True
        Gv1.Columns("To_Location_Name").Width = 100
        Gv1.Columns("To_Location_Name").HeaderText = "To Location Name"


        Gv1.Columns("Type").IsVisible = True
        Gv1.Columns("Type").Width = 100
        Gv1.Columns("Type").HeaderText = "Type"

        Gv1.Columns("Crate_IN").IsVisible = True
        Gv1.Columns("Crate_IN").Width = 100
        Gv1.Columns("Crate_IN").HeaderText = "Crate In "
        Gv1.Columns("Crate_IN").FormatString = "{0:F0}"

        Gv1.Columns("jaali_IN").IsVisible = True
        Gv1.Columns("jaali_IN").Width = 100
        Gv1.Columns("jaali_IN").HeaderText = "Jaali In"
        Gv1.Columns("jaali_IN").FormatString = "{0:F0}"


        Gv1.Columns("Box_IN").IsVisible = True
        Gv1.Columns("Box_IN").Width = 100
        Gv1.Columns("Box_IN").HeaderText = "Box In"
        Gv1.Columns("Box_IN").FormatString = "{0:F0}"

        Gv1.Columns("Crate_Out").IsVisible = True
        Gv1.Columns("Crate_Out").Width = 100
        Gv1.Columns("Crate_Out").HeaderText = "Crate Out"
        Gv1.Columns("Crate_Out").FormatString = "{0:F0}"

        Gv1.Columns("jaali_Out").IsVisible = True
        Gv1.Columns("jaali_Out").Width = 100
        Gv1.Columns("jaali_Out").HeaderText = "Jaali out"
        Gv1.Columns("jaali_Out").FormatString = "{0:F0}"


        Gv1.Columns("Box_Out").IsVisible = True
        Gv1.Columns("Box_Out").Width = 100
        Gv1.Columns("Box_Out").HeaderText = "Box Out"
        Gv1.Columns("Box_Out").FormatString = "{0:F0}"



        Gv1.Columns("CrateAdjQty").IsVisible = True
        Gv1.Columns("CrateAdjQty").Width = 100
        Gv1.Columns("CrateAdjQty").HeaderText = "Crate Adj"
        Gv1.Columns("CrateAdjQty").FormatString = "{0:F0}"

        Gv1.Columns("JaaliAdjQty").IsVisible = True
        Gv1.Columns("JaaliAdjQty").Width = 100
        Gv1.Columns("JaaliAdjQty").HeaderText = "Jaali Adj"
        Gv1.Columns("JaaliAdjQty").FormatString = "{0:F0}"


        Gv1.Columns("BoxAdjQty").IsVisible = True
        Gv1.Columns("BoxAdjQty").Width = 100
        Gv1.Columns("BoxAdjQty").HeaderText = "Box Adj"
        Gv1.Columns("BoxAdjQty").FormatString = "{0:F0}"



        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("Crate_IN", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("jaali_IN", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Box_IN", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Crate_Out", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("jaali_Out", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Box_Out", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item7 As New GridViewSummaryItem("CrateAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("JaaliAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("BoxAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub FormatGrid()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).Width = 100
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

       
        Gv1.Columns("FromLoc").IsVisible = True
        Gv1.Columns("FromLoc").Width = 100
        Gv1.Columns("FromLoc").HeaderText = ""

        Gv1.Columns("ToLoc").IsVisible = True
        Gv1.Columns("ToLoc").Width = 100
        Gv1.Columns("ToLoc").HeaderText = "LOC TYPE"


        Gv1.Columns("FromLocDesc").IsVisible = True
        Gv1.Columns("FromLocDesc").Width = 100
        Gv1.Columns("FromLocDesc").HeaderText = " From Location"

        Gv1.Columns("ToLocDesc").IsVisible = True
        Gv1.Columns("ToLocDesc").Width = 100
        Gv1.Columns("ToLocDesc").HeaderText = "To Location"

        Gv1.Columns("OpencrateQty").IsVisible = True
        Gv1.Columns("OpencrateQty").Width = 100
        Gv1.Columns("OpencrateQty").HeaderText = "OP Crate"
        Gv1.Columns("OpencrateQty").FormatString = "{0:F0}"

        Gv1.Columns("OpenJaaliQty").IsVisible = True
        Gv1.Columns("OpenJaaliQty").Width = 100
        Gv1.Columns("OpenJaaliQty").HeaderText = "OP Jaali"
        Gv1.Columns("OpenJaaliQty").FormatString = "{0:F0}"


        Gv1.Columns("OpenBoxQty").IsVisible = True
        Gv1.Columns("OpenBoxQty").Width = 100
        Gv1.Columns("OpenBoxQty").HeaderText = "OP BOX"
        Gv1.Columns("OpenBoxQty").FormatString = "{0:F0}"

        Gv1.Columns("CrateQtyRecd").IsVisible = True
        Gv1.Columns("CrateQtyRecd").Width = 100
        Gv1.Columns("CrateQtyRecd").HeaderText = "Receiving Crate"
        Gv1.Columns("CrateQtyRecd").FormatString = "{0:F0}"

        Gv1.Columns("JaaliQtyRecd").IsVisible = True
        Gv1.Columns("JaaliQtyRecd").Width = 100
        Gv1.Columns("JaaliQtyRecd").HeaderText = "Receiving Jaali"
        Gv1.Columns("JaaliQtyRecd").FormatString = "{0:F0}"


        Gv1.Columns("BoxQtyRecd").IsVisible = True
        Gv1.Columns("BoxQtyRecd").Width = 100
        Gv1.Columns("BoxQtyRecd").HeaderText = "Receiving BOX"
        Gv1.Columns("BoxQtyRecd").FormatString = "{0:F0}"

        Gv1.Columns("CrateOutQty").IsVisible = True
        Gv1.Columns("CrateOutQty").Width = 100
        Gv1.Columns("CrateOutQty").HeaderText = "Issue Crate"
        Gv1.Columns("CrateOutQty").FormatString = "{0:F0}"

        Gv1.Columns("jaaliOutQty").IsVisible = True
        Gv1.Columns("jaaliOutQty").Width = 100
        Gv1.Columns("jaaliOutQty").HeaderText = "Issue Jaali"
        Gv1.Columns("jaaliOutQty").FormatString = "{0:F0}"


        Gv1.Columns("boxOutQty").IsVisible = True
        Gv1.Columns("boxOutQty").Width = 100
        Gv1.Columns("boxOutQty").HeaderText = "Issue BOX"
        Gv1.Columns("boxOutQty").FormatString = "{0:F0}"



        Gv1.Columns("CrateAdjQty").IsVisible = True
        Gv1.Columns("CrateAdjQty").Width = 100
        Gv1.Columns("CrateAdjQty").HeaderText = "Adjustment Crate"
        Gv1.Columns("CrateAdjQty").FormatString = "{0:F0}"

        Gv1.Columns("JaaliAdjQty").IsVisible = True
        Gv1.Columns("JaaliAdjQty").Width = 100
        Gv1.Columns("JaaliAdjQty").HeaderText = "Adjustment Jaali"
        Gv1.Columns("JaaliAdjQty").FormatString = "{0:F0}"


        Gv1.Columns("BoxAdjQty").IsVisible = True
        Gv1.Columns("BoxAdjQty").Width = 100
        Gv1.Columns("BoxAdjQty").HeaderText = "Adjustment BOX"
        Gv1.Columns("BoxAdjQty").FormatString = "{0:F0}"

        Gv1.Columns("CrateQtyClosing").IsVisible = True
        Gv1.Columns("CrateQtyClosing").Width = 100
        Gv1.Columns("CrateQtyClosing").HeaderText = "Closing Crate"
        Gv1.Columns("CrateQtyClosing").FormatString = "{0:F0}"

        Gv1.Columns("JaaliQtyClosing").IsVisible = True
        Gv1.Columns("JaaliQtyClosing").Width = 100
        Gv1.Columns("JaaliQtyClosing").HeaderText = "Closing Jaali"
        Gv1.Columns("JaaliQtyClosing").FormatString = "{0:F0}"


        Gv1.Columns("BoxQtyClosing").IsVisible = True
        Gv1.Columns("BoxQtyClosing").Width = 100
        Gv1.Columns("BoxQtyClosing").HeaderText = "Closing BOX"
        Gv1.Columns("BoxQtyClosing").FormatString = "{0:F0}"




        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("OpencrateQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("OpenJaaliQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("OpenBoxQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("CrateQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("JaaliQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("BoxQtyRecd", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item7 As New GridViewSummaryItem("CrateOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("jaaliOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("boxOutQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("CrateQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("JaaliQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("BoxQtyClosing", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)

        Dim item13 As New GridViewSummaryItem("CrateAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("JaaliAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("BoxAdjQty", "{0:F0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)

        Gv1.GroupDescriptors.Add(New GridGroupByExpression("FromLoc as Location format ""{0}: {1}"" Group By FromLoc"))

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        'Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtBranch.arrValueMember IsNot Nothing AndAlso txtBranch.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtBranch.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Crate Jalli Report For Transfer", Gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Crate Jalli Report For Transfer", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        txtBranch.arrValueMember = Nothing
        txtBranch.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1
        ChkSummary.IsChecked = True
        Gv1.DataSource = Nothing
        Dim view As TableViewDefinition = New TableViewDefinition()

        Gv1.ViewDefinition = view

    End Sub

    Private Sub btnGo_Click_1(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(ChkSummary.IsChecked = True, "S", "D")
        TemplateGridview = Gv1
        loaddata()
    End Sub

    Private Sub btnClose_Click_1(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click_1(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmexcel_Click(sender As Object, e As EventArgs) Handles rmexcel.Click
        print(Exporter.Excel)
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        print(Exporter.PDF)
    End Sub
End Class
