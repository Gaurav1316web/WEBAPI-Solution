Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports System.IO
Public Class RptRoutewiseTPTimeTable
    Inherits FrmMainTranScreen
    Private Sub SetUserMgmtNew()
        Try
            ''MyBase.SetUserMgmt(clsUserMgtCode.rptRoutewiseTPTimeTable)
            If Not (MyBase.isReadFlag) Then
                Throw New Exception("Permission Denied")
            End If
            btnExp.Visible = MyBase.isExport
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        Try
            gv.DataSource = Nothing
            txtMccCode.arrValueMember = Nothing
            txtVehicleNo.arrValueMember = Nothing
            txtTransporter.arrValueMember = Nothing
            txtRouteCode.arrValueMember = Nothing
            RadPageView1.SelectedPage = RadPageViewPage1
            radioRouteWise.IsChecked = True
            txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
            txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
            dtEffective.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
            LoadShiftFrom()
            LoadShiftTo()
            txtMCC.Value = ""
            lblMCC.Text = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RptRoutewiseTPTimeTable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            Dim settingOnOff As Integer = 0
            settingOnOff = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowRouteWiseAndVLCWiseReport, clsFixedParameterType.ShowRouteWiseAndVLCWiseReport, Nothing))
            If settingOnOff = 1 Then
                groupReports.Visible = True
            Else
                groupReports.Visible = False
                radioRouteWise.IsChecked = True
                radioVLCWise.IsChecked = False
            End If
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub
    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub
    Private Sub txtMccCode__My_Click(sender As Object, e As EventArgs) Handles txtMccCode._My_Click
        Try
            Dim qry As String = "select MCC_Code,MCC_Name  from Tspl_mcc_master order by MCC_Name asc"
            txtMccCode.arrValueMember = clsCommon.ShowMultipleSelectForm("LocatMast", qry, "MCC_Code", "MCC_Name", txtMccCode.arrValueMember, txtMccCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtRouteCode__My_Click(sender As Object, e As EventArgs) Handles txtRouteCode._My_Click
        Try
            Dim qry As String = "select Distinct TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as Description from TSPL_MCC_ROUTE_MASTER "
            txtRouteCode.arrValueMember = clsCommon.ShowMultipleSelectForm("Routed", qry, "Code", "Description", txtRouteCode.arrValueMember, txtRouteCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Report_RouteWise()
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Generate_ReportsQueryString("Report_RouteWise"))
            If dt IsNot Nothing AndAlso dt.Rows.Count Then
                gv.DataSource = Nothing
                gv.DataSource = dt
                SetGrdProperties()
                Set_GridView_Format()
                ReStoreGridLayout()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub SetGrdProperties()
        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.ReadOnly = False
        gv.BestFitColumns()
    End Sub
    Private Sub ReStoreGridLayout_VLC()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Function Generate_ReportsQueryString(ByVal reportType As String, Optional ByVal StrNew As Boolean = False) As String
        Dim qryForReturn As String = ""

        If reportType = "Report_RouteWise" Then
            Dim qryRouteWise As String = Nothing
            If StrNew Then
                qryRouteWise = " select final.*,TSPL_COMPANY_MASTER.Logo_Img from(SELECT distinct TSPL_MCC_ROUTE_MASTER.Route_code as [Route Code],TSPL_MCC_ROUTE_MASTER.Route_Name as [Route Name], COALESCE(RIGHT(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_M, 7), '') AS [Morning Reaching Time], COALESCE(RIGHT(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_E, 7), '') AS [Evening Reaching Time],COALESCE(FORMAT((TSPL_MCC_ROUTE_MASTER.Effective_Date), 'dd/MM/yyyy'), '') as [Effective Date],COALESCE(tspl_mcc_master.mcc_name, '') AS [MCC Name],TSPL_MCC_ROUTE_MASTER.comp_code FROM TSPL_MCC_ROUTE_MASTER LEFT JOIN TSPL_MILK_SRN_HEAD SRN_HD ON SRN_HD.ROUTE_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code AND SRN_HD.MCC_CODE = TSPL_MCC_ROUTE_MASTER.MCC_Code left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MCC_ROUTE_MASTER.MCC_Code where 1=1 "
            Else
                qryRouteWise = " SELECT  MAX(COALESCE(FORMAT((TSPL_MCC_ROUTE_MASTER.Effective_Date), 'dd/MM/yyyy'), '')) AS [Effective Date], MAX(COALESCE(TSPL_MCC_ROUTE_MASTER.route_code, '')) AS [Route Code], MAX(COALESCE(TSPL_MCC_ROUTE_MASTER.route_name, '')) AS [Route Name], MAX(COALESCE(TSPL_MCC_ROUTE_MASTER.mcc_code, '')) AS [MCC Code], MAX(COALESCE(tspl_mcc_master.mcc_name, '')) AS [MCC Name], MAX(COALESCE(TSPL_TRANSPORT_MASTER.Transport_Id, '')) AS [TransportId], MAX(COALESCE(TSPL_TRANSPORT_MASTER.Transporter_Name, '')) AS [Transporter Name], MAX(COALESCE(TSPL_MCC_ROUTE_MASTER.Vehicle_Code, '')) AS [Vehicle Number], MAX(COALESCE(RIGHT(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_M, 7), '')) AS [Morning Reaching Time], MAX(COALESCE(RIGHT(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_E, 7), '')) AS [Evening Reaching Time] FROM TSPL_MCC_ROUTE_MASTER LEFT OUTER JOIN tspl_mcc_master ON Tspl_mcc_master.mcc_code = TSPL_MCC_ROUTE_MASTER.mcc_code LEFT JOIN TSPL_TRANSPORT_MASTER ON TSPL_MCC_ROUTE_MASTER.Supervisor_Name = TSPL_TRANSPORT_MASTER.Transport_Id WHERE 1 = 1 "
                'qryRouteWise = " SELECT MAX(COALESCE(FORMAT((SRN_HD.DOC_DATE), 'dd/MM/yyyy'), '')) AS [DATE(SRN)],convert(varchar(10),max(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_M),103)  as [Mornig Reaching Date],convert(varchar(10),max(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_E),103) as [Evening Reaching Date], MAX(COALESCE(TSPL_MCC_ROUTE_MASTER.route_code, '')) AS [Route Code], MAX(COALESCE(TSPL_MCC_ROUTE_MASTER.route_name, '')) AS [Route Name], MAX(COALESCE(TSPL_MCC_ROUTE_MASTER.mcc_code, '')) AS [MCC Code], MAX(COALESCE(tspl_mcc_master.mcc_name, '')) AS [MCC Name], MAX(COALESCE(SRN_HD.VLC_CODE, '')) AS [VLC_CODE], MAX(COALESCE(VLCM.VLC_Name, '')) AS [VLC_Desc], MAX(COALESCE(TSPL_TRANSPORT_MASTER.Transport_Id, '')) AS [TransportId], MAX(COALESCE(TSPL_TRANSPORT_MASTER.Transporter_Name, '')) AS [Transporter Name], MAX(COALESCE(TSPL_MCC_ROUTE_MASTER.Vehicle_Code, '')) AS [Vehicle Number], MAX(COALESCE(TSPL_MCC_ROUTE_MASTER.kilometer, 0)) AS [Distance (KMs)], MAX(COALESCE(RIGHT(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_M, 7), '')) AS [Morning Reaching Time], MAX(COALESCE(RIGHT(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_E, 7), '')) AS [Evening Reaching Time], MAX(COALESCE(SRN_HD.SHIFT, '')) AS [Shift], MAX(COALESCE(SRN_DT.Item_Code, '')) AS [Item_Code], MAX(COALESCE(IM.Item_Desc, '')) AS [Item_Desc], MAX(COALESCE(SRN_DT.UOM_Code, '')) AS [UOM_Code], CAST(ROUND((SUM(SRN_DT.FAT_KG) * 100 / SUM(SRN_DT.ACC_Qty)), 2) AS numeric(36, 2)) AS [FAT_PER], SUM(COALESCE(SRN_DT.FAT_KG, 0)) AS [FAT_KG], CAST(ROUND((SUM(SRN_DT.SNF_KG) * 100 / SUM(SRN_DT.ACC_Qty)), 2) AS numeric(36, 2)) AS [SNF_PER], SUM(COALESCE(SRN_DT.SNF_KG, 0)) AS [SNF_KG], SUM(COALESCE(SRN_DT.Qty, 0)) AS [Qty], SUM(COALESCE(SRN_DT.ACC_Qty, 0)) AS [ACC_Qty], SUM(COALESCE(SRN_DT.AMOUNT, 0)) AS [AMOUNT], SUM(COALESCE(SRN_DT.Std_Qty, 0)) AS [Std_Qty], SUM(COALESCE(SRN_DT.AMOUNT, 0)) AS [Producer Value (Rs)] FROM TSPL_MCC_ROUTE_MASTER LEFT OUTER JOIN tspl_mcc_master ON Tspl_mcc_master.mcc_code = TSPL_MCC_ROUTE_MASTER.mcc_code LEFT JOIN TSPL_TRANSPORT_MASTER ON TSPL_MCC_ROUTE_MASTER.Supervisor_Name = TSPL_TRANSPORT_MASTER.Transport_Id LEFT JOIN TSPL_MILK_SRN_HEAD SRN_HD ON SRN_HD.ROUTE_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code AND SRN_HD.MCC_CODE = TSPL_MCC_ROUTE_MASTER.MCC_Code LEFT OUTER JOIN TSPL_MILK_SRN_DETAIL SRN_DT ON SRN_HD.DOC_CODE = SRN_DT.DOC_CODE AND SRN_DT.MCC_CODE = SRN_DT.MCC_CODE LEFT OUTER JOIN TSPL_ITEM_MASTER IM ON SRN_DT.Item_Code = IM.Item_Code LEFT OUTER JOIN TSPL_VLC_MASTER_HEAD VLCM ON SRN_HD.VLC_CODE = VLCM.VLC_Code WHERE 1 = 1 AND SRN_HD.SHIFT IS NOT NULL "
            End If
           
            'If clsCommon.CompairString(txtFromShift.Text, "M") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "E") = CompairStringResult.Equal Then
            '    qryRouteWise += " and  Cast(SRN_HD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Cast(SRN_HD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
            'End If
            'If clsCommon.CompairString(txtFromShift.Text, "M") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            '    qryRouteWise += " and  Cast(SRN_HD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Cast(SRN_HD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
            'End If
            'If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            '    qryRouteWise += " and  Cast(SRN_HD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Cast(SRN_HD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
            'End If
            'If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "E") = CompairStringResult.Equal Then
            '    qryRouteWise += " and  Cast(SRN_HD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Cast(SRN_HD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
            'End If
            If clsCommon.myLen(txtMCC.Value) > 0 Then
                qryRouteWise += " and TSPL_MCC_ROUTE_MASTER.mcc_code ='" & txtMCC.Value & "'"
            End If
            qryRouteWise += " and  Cast(TSPL_MCC_ROUTE_MASTER.Effective_Date as Date) >= '" + clsCommon.GetPrintDate(dtEffective.Value, "dd/MMM/yyyy") + "' and Cast(TSPL_MCC_ROUTE_MASTER.Effective_Date as Date) <= '" + clsCommon.GetPrintDate(dtEffective.Value, "dd/MMM/yyyy") + "' "

            If txtRouteCode.arrValueMember IsNot Nothing AndAlso txtRouteCode.arrValueMember.Count > 0 Then
                qryRouteWise += " and  TSPL_MCC_ROUTE_MASTER.route_code in (" + clsCommon.GetMulcallString(txtRouteCode.arrValueMember) + ")"
            End If

            If txtVehicleNo.arrValueMember IsNot Nothing AndAlso txtVehicleNo.arrValueMember.Count > 0 Then
                qryRouteWise += "  and TSPL_MCC_ROUTE_MASTER.Vehicle_Code in (" + clsCommon.GetMulcallString(txtVehicleNo.arrValueMember) + ") " + Environment.NewLine
            End If
            If StrNew Then
                If txtTransporter.arrValueMember IsNot Nothing AndAlso txtTransporter.arrValueMember.Count > 0 Then
                    qryRouteWise += " and TSPL_MCC_ROUTE_MASTER.Supervisor_Name in ( " + clsCommon.GetMulcallString(txtTransporter.arrValueMember) + ") " + Environment.NewLine
                End If
                qryRouteWise += " ) as final left join TSPL_COMPANY_MASTER on tspl_company_master.comp_code=final.comp_code "
            Else
                If txtTransporter.arrValueMember IsNot Nothing AndAlso txtTransporter.arrValueMember.Count > 0 Then
                    qryRouteWise += " and TSPL_TRANSPORT_MASTER.Transport_Id in ( " + clsCommon.GetMulcallString(txtTransporter.arrValueMember) + ") " + Environment.NewLine
                End If
                qryRouteWise += " GROUP BY TSPL_MCC_ROUTE_MASTER.mcc_code, TSPL_MCC_ROUTE_MASTER.route_code ORDER BY TSPL_MCC_ROUTE_MASTER.mcc_code, TSPL_MCC_ROUTE_MASTER.route_code "
            End If


            qryForReturn = qryRouteWise

        Else
            Dim qryVlcWise As String = " SELECT SS.[S.No], ss.Route_CODE, tspl_mcc_route_master.Route_Name, ss.[Mcc Code], tspl_mcc_master.MCC_NAME, ss.[VLC Code], tspl_vlc_master_head.VLC_Name, SS.[Distance b/w VLCs (K.M)] as [Distances] , ss.[Morning Reaching Date], ss.[Evening Reaching Date], ss.[Morning Reaching Time], ss.[Evening Reaching Time] FROM ((SELECT TSPL_MCC_ROUTE_VLC_MAPPING.SNo [S.No], TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE, MCC [Mcc Code], SUM(TSPL_MCC_ROUTE_VLC_MAPPING.Distance) OVER (PARTITION BY TSPL_VLC_MASTER_HEAD.mcc, TSPL_VLC_MASTER_HEAD.Route_Code ORDER BY TSPL_MCC_ROUTE_VLC_MAPPING.SNo) [MCC to VLC's Distance (K.M)], TSPL_MCC_ROUTE_MASTER.Last_VLC_To_MCC_Distance [Last VLC to MCC Distance (K.M)], TSPL_VLC_MASTER_HEAD.vlc_code [VLC Code], TSPL_MCC_ROUTE_VLC_MAPPING.Distance [Distance b/w VLCs (K.M)], CONVERT(varchar(10), TSPL_MCC_ROUTE_VLC_MAPPING.Mor_Mik_Coll, 103) AS [Morning Reaching Date], CONVERT(varchar(5), TSPL_MCC_ROUTE_VLC_MAPPING.Mor_Mik_Coll, 108) AS [Morning Reaching Time], CONVERT(varchar(10), TSPL_MCC_ROUTE_VLC_MAPPING.Eve_Milk_Coll, 103) AS [Evening Reaching Date], CONVERT(varchar(5), TSPL_MCC_ROUTE_VLC_MAPPING.Eve_Milk_Coll, 108) AS [Evening Reaching Time], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader [VLC Uploader] FROM TSPL_MCC_ROUTE_VLC_MAPPING LEFT JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.vlc_code = TSPL_MCC_ROUTE_VLC_MAPPING.vlc_code LEFT JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code LEFT JOIN tspl_mcc_master ON tspl_mcc_master.MCC_Code = TSPL_VLC_MASTER_HEAD.mcc LEFT JOIN TSPL_MCC_ROUTE_MASTER ON TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code WHERE 1 = 1 ) "
            qryVlcWise += " union all "
            qryVlcWise += " (SELECT a.SNo + 1 [S.No], a.Route_CODE, TSPL_MCC_ROUTE_MASTER.MCC_Code AS [Mcc Code], 0 [MCC to VLC's Distance (K.M)], 0 [Last VLC to MCC Distance (K.M)], '' [VLC Code], tspl_mcc_route_master.Last_VLC_To_MCC_Distance [Distance b/w VLCs (K.M)], NULL [Morning Reaching Date], NULL [Morning Reaching Time], NULL [Evening Reaching Date], NULL [Evening Reaching Time], NULL [VLC Uploader] FROM TSPL_MCC_ROUTE_VLC_MAPPING a LEFT JOIN TSPL_MCC_ROUTE_MASTER ON TSPL_MCC_ROUTE_MASTER.Route_Code = A.Route_CODE LEFT JOIN (SELECT MAX(SNo) SNo, Route_CODE FROM TSPL_MCC_ROUTE_VLC_MAPPING GROUP BY Route_CODE) AS b ON a.SNo = b.SNo AND a.Route_CODE = b.Route_CODE WHERE b.SNo IS NOT NULL )) AS SS LEFT JOIN tspl_mcc_master ON tspl_mcc_master.mcc_code = ss.[mcc code] LEFT JOIN tspl_vlc_master_head ON tspl_vlc_master_head.vlc_code = ss.[VLC Code] LEFT JOIN tspl_mcc_route_master ON tspl_mcc_route_master.route_code = ss.[Route_code] "
            qryVlcWise += "  where 1 = 1"

            'If clsCommon.CompairString(txtFromShift.Text, "M") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "E") = CompairStringResult.Equal Then
            '    qryVlcWise += " and  Cast(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_M as Date) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Cast(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_E as Date) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
            'End If
            'If clsCommon.CompairString(txtFromShift.Text, "M") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            '    qryVlcWise += " and  Cast(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_M as Date) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Cast(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_M as Date) <='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
            'End If
            'If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            '    qryVlcWise += " and  Cast(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_E as Date) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Cast(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_M as Date) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
            'End If
            'If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal AndAlso clsCommon.CompairString(txtToShift.Text, "E") = CompairStringResult.Equal Then
            '    qryVlcWise += " and  Cast(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_E as Date) >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and Cast(TSPL_MCC_ROUTE_MASTER.MCC_Reaching_Time_E as Date) <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
            'End If

            'If txtMccCode.arrValueMember IsNot Nothing AndAlso txtMccCode.arrValueMember.Count > 0 Then
            '    qryVlcWise += " AND SS.[Mcc Code] IN  (" + clsCommon.GetMulcallString(txtMccCode.arrValueMember) + ")"
            'End If
            qryVlcWise += " and  Cast(TSPL_MCC_ROUTE_MASTER.Effective_Date as Date) >= '" + clsCommon.GetPrintDate(dtEffective.Value, "dd/MMM/yyyy") + "' and Cast(TSPL_MCC_ROUTE_MASTER.Effective_Date as Date) <= '" + clsCommon.GetPrintDate(dtEffective.Value, "dd/MMM/yyyy") + "' "

            If clsCommon.myLen(txtMCC.Value) > 0 Then
                qryVlcWise += " and SS.[Mcc Code] ='" & txtMCC.Value & "'"
            End If
            If txtRouteCode.arrValueMember IsNot Nothing AndAlso txtRouteCode.arrValueMember.Count > 0 Then
                qryVlcWise += " AND SS.Route_CODE IN (" + clsCommon.GetMulcallString(txtRouteCode.arrValueMember) + ")"
            End If
            qryVlcWise += " ORDER BY  SS.Route_CODE, SS.[S.No] "
            qryForReturn = qryVlcWise
        End If
        Return qryForReturn
    End Function
    Private Sub Report_VLCWise()
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Generate_ReportsQueryString("Report_VLCWise"))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.DataSource = dt
                SetGrdProperties()
                ReStoreGridLayout()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myCDate(txtFromDate.Value) > clsCommon.myCDate(txtToDate.Value) Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            PageSetupReport_ID = MyBase.Form_ID + IIf(radioRouteWise.IsChecked = True, "R", "V")
            TemplateGridview = gv
            If radioRouteWise.IsChecked Then
                Report_RouteWise()
            Else
                Report_VLCWise()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Set_GridView_Format()
        Try

            gv.TableElement.TableHeaderHeight = 45
            gv.MasterTemplate.ShowRowHeaderColumn = True
            For Each col As GridViewColumn In gv.Columns
                col.Width = 120
                col.ReadOnly = True

                If col.Name = "Code" Then
                    col.Width = 180
                    col.HeaderText = "Route Code"
                End If

                If col.Name = "Route Name" Then
                    col.Width = 180
                    col.HeaderText = "Route Desc"
                End If

                If col.Name = "TransportId" Then
                    col.Width = 100
                    col.HeaderText = "Transport"
                End If

                If col.Name = "Transporter Name" Then
                    col.Width = 180
                    col.HeaderText = "Transporter Desc"
                End If

                If col.Name = "MCC Code" Then
                    col.Width = 180
                End If
                If col.Name = "Distence KM" Then
                    col.Width = 100
                    col.HeaderText = "Distance (Km)"

                End If

                If col.Name = "Morning Reaching Time" Then
                    col.Width = 180
                End If

                If col.Name = "Evening Reaching Time" Then
                    col.Width = 180
                End If
            Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub CrystalReport_RouteWise()
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Generate_ReportsQueryString("Report_RouteWise", True))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ' Report_RouteWise()
                'frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, "rptRouteWiseTimeTable", "")
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptRouteWiseTimeTable_New", "")
                frmCRV = Nothing
            Else
                common.clsCommon.MyMessageBoxShow("No data found to print", Me.Text)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub CrystalReport_VLCWise()
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Generate_ReportsQueryString("Report_VLCWise"))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ' Report_VLCWise()
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptVLCWiseTimeTable", "")
                frmCRV = Nothing
            Else
                common.clsCommon.MyMessageBoxShow("No data found to print", Me.Text)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myCDate(txtFromDate.Value) > clsCommon.myCDate(txtToDate.Value) Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            If radioRouteWise.IsChecked Then
                CrystalReport_RouteWise()
            Else
                CrystalReport_VLCWise()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub txtVehicleNo__My_Click(sender As Object, e As EventArgs) Handles txtVehicleNo._My_Click
        Try
            Dim qry As String = " select distinct (TSPL_VEHICLE_MASTER.Number) as [Vehicle Number] , coalesce(TSPL_VEHICLE_MASTER.Description,'') as [Vehicle-Name] , coalesce(TSPL_VEHICLE_MASTER.Vehicle_Id,'') as [Vehicle Id] from TSPL_VEHICLE_MASTER "
            txtVehicleNo.arrValueMember = clsCommon.ShowMultipleSelectForm("RWTT_VehCode", qry, "Vehicle Number", "Vehicle Number", txtVehicleNo.arrValueMember, txtVehicleNo.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtTransporter__My_Click(sender As Object, e As EventArgs) Handles txtTransporter._My_Click
        Try
            Dim qry As String = "select distinct coalesce(TSPL_TRANSPORT_MASTER.Transport_Id,'') as [Transporter Id], coalesce(TSPL_TRANSPORT_MASTER.Transporter_Name,'') as [Transporter Name]  from TSPL_TRANSPORT_MASTER "
            txtTransporter.arrValueMember = clsCommon.ShowMultipleSelectForm("RWTT_VehCode", qry, "Transporter Id", "Transporter Name", txtTransporter.arrValueMember, txtTransporter.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Print(ByVal exporter As EnumExportTo)
        Dim arrHeader As List(Of String) = Nothing
        Try
            If gv.Rows.Count > 0 Then

                arrHeader = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                If radioRouteWise.IsChecked Then
                    arrHeader.Add("Name : " & "Route-Wise Time Table")
                Else
                    arrHeader.Add("Name : " & "VLC-Wise Time Table")
                End If
                arrHeader.Add("Effective Date: " + clsCommon.GetPrintDate(dtEffective.Value, "dd/MM/yyyy"))
                If clsCommon.myLen(lblMCC.Text) > 0 Then
                    arrHeader.Add("MCC : " + lblMCC.Text)
                End If
                If txtRouteCode.arrValueMember IsNot Nothing AndAlso txtRouteCode.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRouteCode.arrDispalyMember) + " "))
                End If
                If txtVehicleNo.arrValueMember IsNot Nothing AndAlso txtVehicleNo.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Vehicle : " + clsCommon.GetMulcallStringWithComma(txtVehicleNo.arrDispalyMember) + " "))
                End If
                If txtTransporter.arrValueMember IsNot Nothing AndAlso txtTransporter.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Transporter : " + clsCommon.GetMulcallStringWithComma(txtTransporter.arrDispalyMember) + " "))
                End If

                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToExcelGrid("Route-wise / VLC-Wise Time Table", gv, arrHeader, Me.Text, True)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Route-wise / VLC-Wise Time Table", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub menuSaveLayout_Click(sender As Object, e As EventArgs) Handles menuSaveLayout.Click
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                gv.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = PageSetupReport_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gv.SaveLayout(obj.GridLayout)
                obj.GridColumns = gv.ColumnCount
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow("Layout Saved Successfully", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub menuDelete_Click(sender As Object, e As EventArgs) Handles menuDelete.Click
        Try
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_KeyDown(sender As Object, e As KeyEventArgs) Handles btnGo.KeyDown
        Try
            If e.Alt And e.KeyCode = Keys.P Then
                Print(EnumExportTo.Excel)
            ElseIf e.Alt And e.KeyCode = Keys.C Then
                Close()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub radioVLCWise_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles radioVLCWise.ToggleStateChanged
        If args.ToggleState = ToggleState.On Then
            txtVehicleNo.Enabled = False
            txtTransporter.Enabled = False
        Else
            txtVehicleNo.Enabled = True
            txtTransporter.Enabled = True
        End If

    End Sub
    Private Sub txtMCC__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Dim qry As String = " select MCC_Code as Code, MCC_Name as Name,Mcc_Type as [MCC Type] from  TSPL_MCC_MASTER  "
        txtMCC.Value = clsCommon.ShowSelectForm("LOCMSTFND", qry, "Code", "", "", "Code", isButtonClicked)


        If clsCommon.myLen(txtMCC.Value) > 0 Then
            lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from TSPL_MCC_MASTER where MCC_Code='" & txtMCC.Value & "'"))
        Else
            lblMCC.Text = ""
        End If
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Print(EnumExportTo.PDF)
    End Sub
End Class

