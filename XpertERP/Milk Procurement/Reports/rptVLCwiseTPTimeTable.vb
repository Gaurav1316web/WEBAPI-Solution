Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class RptVLCwiseTPTimeTable
    Inherits FrmMainTranScreen
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptVLCTragetMasterReport)

    End Sub
    Dim dtItem As DataTable
    Private Sub RptVLCwiseTPTimeTable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Public Sub SetGrdProperties()
        dgvreport.AllowAddNewRow = False
        dgvreport.AllowDeleteRow = False
        dgvreport.ShowGroupPanel = False
        dgvreport.ReadOnly = False
        dgvreport.BestFitColumns()
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            '    Dim qry As String = "select  TSPL_MCC_ROUTE_VLC_MAPPING.SNo [S.NO.], TSPL_VLC_MASTER_HEAD.vlc_code [VLC Code],TSPL_VLC_MASTER_HEAD.vlc_name [VLC Name],TSPL_MCC_ROUTE_VLC_MAPPING.Distance [Distance K.M.],TSPL_MCC_ROUTE_VLC_MAPPING.Mor_Mik_Coll as [Morning Reaching Time],TSPL_MCC_ROUTE_VLC_MAPPING.Eve_Milk_Coll as [Evening Reaching Time],MCC,mcc_name [MCC Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader [VLC Uploader]  from TSPL_MCC_ROUTE_VLC_MAPPING left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.vlc_code=TSPL_MCC_ROUTE_VLC_MAPPING.vlc_code  left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code left join tspl_mcc_master on tspl_mcc_master.MCC_Code=TSPL_VLC_MASTER_HEAD.mcc where 1=1  "
            'If txtMccCode.arrValueMember IsNot Nothing AndAlso txtMccCode.arrValueMember.Count > 0 Then
            '    qry += "and  TSPL_VLC_MASTER_HEAD.mcc in (" + clsCommon.GetMulcallString(txtMccCode.arrValueMember) + ")"
            'End If
            'If txtRouteCode.arrValueMember IsNot Nothing AndAlso txtRouteCode.arrValueMember.Count > 0 Then
            '    qry += " and  TSPL_MCC_ROUTE_VLC_MAPPING.route_Code in (" + clsCommon.GetMulcallString(txtRouteCode.arrValueMember) + ")"
            'End If
            'qry += " order by TSPL_MCC_ROUTE_VLC_MAPPING.SNo"
            Dim qry As String = " SELECT SS.[S.No], ss.Route_CODE, tspl_mcc_route_master.Route_Name, ss.[Mcc Code], tspl_mcc_master.MCC_NAME, ss.[VLC Code], tspl_vlc_master_head.VLC_Name, SS.[Distance b/w VLCs (K.M)] as [Distances] , ss.[Morning Reaching Date], ss.[Evening Reaching Date], ss.[Morning Reaching Time], ss.[Evening Reaching Time] FROM ((SELECT TSPL_MCC_ROUTE_VLC_MAPPING.SNo [S.No], TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE, MCC [Mcc Code], SUM(TSPL_MCC_ROUTE_VLC_MAPPING.Distance) OVER (PARTITION BY TSPL_VLC_MASTER_HEAD.mcc, TSPL_VLC_MASTER_HEAD.Route_Code ORDER BY TSPL_MCC_ROUTE_VLC_MAPPING.SNo) [MCC to VLC's Distance (K.M)], TSPL_MCC_ROUTE_MASTER.Last_VLC_To_MCC_Distance [Last VLC to MCC Distance (K.M)], TSPL_VLC_MASTER_HEAD.vlc_code [VLC Code], TSPL_MCC_ROUTE_VLC_MAPPING.Distance [Distance b/w VLCs (K.M)], CONVERT(varchar(10), TSPL_MCC_ROUTE_VLC_MAPPING.Mor_Mik_Coll, 103) AS [Morning Reaching Date], CONVERT(varchar(5), TSPL_MCC_ROUTE_VLC_MAPPING.Mor_Mik_Coll, 108) AS [Morning Reaching Time], CONVERT(varchar(10), TSPL_MCC_ROUTE_VLC_MAPPING.Eve_Milk_Coll, 103) AS [Evening Reaching Date], CONVERT(varchar(5), TSPL_MCC_ROUTE_VLC_MAPPING.Eve_Milk_Coll, 108) AS [Evening Reaching Time], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader [VLC Uploader] FROM TSPL_MCC_ROUTE_VLC_MAPPING LEFT JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.vlc_code = TSPL_MCC_ROUTE_VLC_MAPPING.vlc_code LEFT JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code LEFT JOIN tspl_mcc_master ON tspl_mcc_master.MCC_Code = TSPL_VLC_MASTER_HEAD.mcc LEFT JOIN TSPL_MCC_ROUTE_MASTER ON TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code WHERE 1 = 1 ) "
            qry += " union all "
            qry += " (SELECT a.SNo + 1 [S.No], a.Route_CODE, TSPL_MCC_ROUTE_MASTER.MCC_Code AS [Mcc Code], 0 [MCC to VLC's Distance (K.M)], 0 [Last VLC to MCC Distance (K.M)], '' [VLC Code], tspl_mcc_route_master.Last_VLC_To_MCC_Distance [Distance b/w VLCs (K.M)], NULL [Morning Reaching Date], NULL [Morning Reaching Time], NULL [Evening Reaching Date], NULL [Evening Reaching Time], NULL [VLC Uploader] FROM TSPL_MCC_ROUTE_VLC_MAPPING a LEFT JOIN TSPL_MCC_ROUTE_MASTER ON TSPL_MCC_ROUTE_MASTER.Route_Code = A.Route_CODE LEFT JOIN (SELECT MAX(SNo) SNo, Route_CODE FROM TSPL_MCC_ROUTE_VLC_MAPPING GROUP BY Route_CODE) AS b ON a.SNo = b.SNo AND a.Route_CODE = b.Route_CODE WHERE b.SNo IS NOT NULL )) AS SS LEFT JOIN tspl_mcc_master ON tspl_mcc_master.mcc_code = ss.[mcc code] LEFT JOIN tspl_vlc_master_head ON tspl_vlc_master_head.vlc_code = ss.[VLC Code] LEFT JOIN tspl_mcc_route_master ON tspl_mcc_route_master.route_code = ss.[Route_code] "
            qry += "  where 1 = 1"
            If txtMccCode.arrValueMember IsNot Nothing AndAlso txtMccCode.arrValueMember.Count > 0 Then
                qry += " AND SS.[Mcc Code] IN  (" + clsCommon.GetMulcallString(txtMccCode.arrValueMember) + ")"
            End If
            If txtRouteCode.arrValueMember IsNot Nothing AndAlso txtRouteCode.arrValueMember.Count > 0 Then
                qry += " AND SS.Route_CODE IN (" + clsCommon.GetMulcallString(txtRouteCode.arrValueMember) + ")"
            End If
            qry += " ORDER BY SS.[Mcc Code], SS.Route_CODE, SS.[S.No] "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptVLCWiseTimeTable", "")
                frmCRV = Nothing
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data To Print", Me.Text)

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= dgvreport.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To dgvreport.Columns.Count - 1 Step ii + 1
                        dgvreport.Columns(ii).IsVisible = False
                        dgvreport.Columns(ii).VisibleInColumnChooser = True
                    Next
                    dgvreport.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click
        Try
            '    Dim qry As String = "select  TSPL_MCC_ROUTE_VLC_MAPPING.SNo [S.NO.], TSPL_VLC_MASTER_HEAD.vlc_code [VLC Code],TSPL_VLC_MASTER_HEAD.vlc_name [VLC Name],TSPL_MCC_ROUTE_VLC_MAPPING.Distance [Distance K.M.],convert(varchar(10),TSPL_MCC_ROUTE_VLC_MAPPING.Mor_Mik_Coll,103) as [Morning Reaching Date],convert(varchar(5),TSPL_MCC_ROUTE_VLC_MAPPING.Mor_Mik_Coll,108) as [Morning Reaching Time],convert(varchar(10),TSPL_MCC_ROUTE_VLC_MAPPING.Eve_Milk_Coll,103) as [Evening Reaching Date],convert(varchar(5),TSPL_MCC_ROUTE_VLC_MAPPING.Eve_Milk_Coll,108) as [Evening Reaching Time],MCC,mcc_name [MCC Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader [VLC Uploader]  from TSPL_MCC_ROUTE_VLC_MAPPING left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.vlc_code=TSPL_MCC_ROUTE_VLC_MAPPING.vlc_code  left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code left join tspl_mcc_master on tspl_mcc_master.MCC_Code=TSPL_VLC_MASTER_HEAD.mcc where 1=1  "
            'If txtMccCode.arrValueMember IsNot Nothing AndAlso txtMccCode.arrValueMember.Count > 0 Then
            '    qry += "and  TSPL_VLC_MASTER_HEAD.mcc in (" + clsCommon.GetMulcallString(txtMccCode.arrValueMember) + ")"
            'End If
            'If txtRouteCode.arrValueMember IsNot Nothing AndAlso txtRouteCode.arrValueMember.Count > 0 Then
            '    qry += " and  TSPL_MCC_ROUTE_VLC_MAPPING.route_Code in (" + clsCommon.GetMulcallString(txtRouteCode.arrValueMember) + ")"
            'End If
            'qry += " order by TSPL_MCC_ROUTE_VLC_MAPPING.SNo"
            'dtItem = clsDBFuncationality.GetDataTable(qry)
            Dim qry As String = " SELECT SS.[S.No], ss.Route_CODE, tspl_mcc_route_master.Route_Name, ss.[Mcc Code], tspl_mcc_master.MCC_NAME, ss.[VLC Code], tspl_vlc_master_head.VLC_Name, SS.[Distance b/w VLCs (K.M)] as [Distances] , ss.[Morning Reaching Date], ss.[Evening Reaching Date], ss.[Morning Reaching Time], ss.[Evening Reaching Time] FROM ((SELECT TSPL_MCC_ROUTE_VLC_MAPPING.SNo [S.No], TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE, MCC [Mcc Code], SUM(TSPL_MCC_ROUTE_VLC_MAPPING.Distance) OVER (PARTITION BY TSPL_VLC_MASTER_HEAD.mcc, TSPL_VLC_MASTER_HEAD.Route_Code ORDER BY TSPL_MCC_ROUTE_VLC_MAPPING.SNo) [MCC to VLC's Distance (K.M)], TSPL_MCC_ROUTE_MASTER.Last_VLC_To_MCC_Distance [Last VLC to MCC Distance (K.M)], TSPL_VLC_MASTER_HEAD.vlc_code [VLC Code], TSPL_MCC_ROUTE_VLC_MAPPING.Distance [Distance b/w VLCs (K.M)], CONVERT(varchar(10), TSPL_MCC_ROUTE_VLC_MAPPING.Mor_Mik_Coll, 103) AS [Morning Reaching Date], CONVERT(varchar(5), TSPL_MCC_ROUTE_VLC_MAPPING.Mor_Mik_Coll, 108) AS [Morning Reaching Time], CONVERT(varchar(10), TSPL_MCC_ROUTE_VLC_MAPPING.Eve_Milk_Coll, 103) AS [Evening Reaching Date], CONVERT(varchar(5), TSPL_MCC_ROUTE_VLC_MAPPING.Eve_Milk_Coll, 108) AS [Evening Reaching Time], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader [VLC Uploader] FROM TSPL_MCC_ROUTE_VLC_MAPPING LEFT JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.vlc_code = TSPL_MCC_ROUTE_VLC_MAPPING.vlc_code LEFT JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code LEFT JOIN tspl_mcc_master ON tspl_mcc_master.MCC_Code = TSPL_VLC_MASTER_HEAD.mcc LEFT JOIN TSPL_MCC_ROUTE_MASTER ON TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code WHERE 1 = 1 ) "
            qry += " union all "
            qry += " (SELECT a.SNo + 1 [S.No], a.Route_CODE, TSPL_MCC_ROUTE_MASTER.MCC_Code AS [Mcc Code], 0 [MCC to VLC's Distance (K.M)], 0 [Last VLC to MCC Distance (K.M)], '' [VLC Code], TSPL_MCC_ROUTE_MASTER.Last_VLC_To_MCC_Distance [Distance b/w VLCs (K.M)], NULL [Morning Reaching Date], NULL [Morning Reaching Time], NULL [Evening Reaching Date], NULL [Evening Reaching Time], NULL [VLC Uploader] FROM TSPL_MCC_ROUTE_VLC_MAPPING a LEFT JOIN TSPL_MCC_ROUTE_MASTER ON TSPL_MCC_ROUTE_MASTER.Route_Code = A.Route_CODE LEFT JOIN (SELECT MAX(SNo) SNo, Route_CODE FROM TSPL_MCC_ROUTE_VLC_MAPPING GROUP BY Route_CODE) AS b ON a.SNo = b.SNo AND a.Route_CODE = b.Route_CODE WHERE b.SNo IS NOT NULL )) AS SS LEFT JOIN tspl_mcc_master ON tspl_mcc_master.mcc_code = ss.[mcc code] LEFT JOIN tspl_vlc_master_head ON tspl_vlc_master_head.vlc_code = ss.[VLC Code] LEFT JOIN tspl_mcc_route_master ON tspl_mcc_route_master.route_code = ss.[Route_code] "
            qry += "  where 1 = 1"
            If txtMccCode.arrValueMember IsNot Nothing AndAlso txtMccCode.arrValueMember.Count > 0 Then
                qry += " AND SS.[Mcc Code] IN  (" + clsCommon.GetMulcallString(txtMccCode.arrValueMember) + ")"
            End If
            If txtRouteCode.arrValueMember IsNot Nothing AndAlso txtRouteCode.arrValueMember.Count > 0 Then
                qry += " AND SS.Route_CODE IN (" + clsCommon.GetMulcallString(txtRouteCode.arrValueMember) + ")"
            End If
            qry += " ORDER BY SS.[Mcc Code], SS.Route_CODE, SS.[S.No] "
            dtItem = clsDBFuncationality.GetDataTable(qry)
            If dtItem IsNot Nothing AndAlso dtItem.Rows.Count > 0 Then
                dgvreport.DataSource = dtItem
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
    Sub funreset()
        txtMccCode.arrValueMember = Nothing
        txtRouteCode.arrValueMember = Nothing
        dtItem.Rows.Clear()
        dgvreport.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        funreset()
    End Sub

    Private Sub txtRouteCode__My_Click(sender As Object, e As EventArgs) Handles txtRouteCode._My_Click
        Try
            Dim qry As String = "select Distinct TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as Description from TSPL_MCC_ROUTE_MASTER "
            txtRouteCode.arrValueMember = clsCommon.ShowMultipleSelectForm("Routed", qry, "Code", "Description", txtRouteCode.arrValueMember, txtRouteCode.arrDispalyMember)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtMccCode__My_Click(sender As Object, e As EventArgs) Handles txtMccCode._My_Click
        Try
            Dim qry As String = "select MCC_Code,MCC_Name  from Tspl_mcc_master order by MCC_Name asc"
            txtMccCode.arrValueMember = clsCommon.ShowMultipleSelectForm("LocatMast", qry, "MCC_Code", "MCC_Name", txtMccCode.arrValueMember, txtMccCode.arrDispalyMember)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Try
                Print(EnumExportTo.Excel)
                'Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add("Name : VLC wise Transporter Time Table")

                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                'transportSql.exportdataChilRows(dgvreport, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)

            Catch ex As Exception
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Print(ByVal exporter As EnumExportTo)
        Dim arrHeader As List(Of String) = Nothing
        Try
            arrHeader = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If exporter = EnumExportTo.Excel Then
                If dgvreport IsNot Nothing AndAlso dgvreport.Rows.Count > 0 Then
                    'clsCommon.MyExportToExcelGrid("VLC wise Transporter Time Table", dgvreport, arrHeader, Me.Text)
                    clsCommon.MyExportToExcel("VLC wise Transporter Time Table", dgvreport, arrHeader, Me.Text)
                Else
                    clsCommon.MyMessageBoxShow("No Data Found in Grid to Export In Excel Sheet", Me.Text)
                End If
            Else
                If dgvreport IsNot Nothing AndAlso dgvreport.Rows.Count > 0 Then
                    clsCommon.MyExportToPDF("VLC wise Transporter Time Table", dgvreport, arrHeader, Me.Text, True)
                Else
                    clsCommon.MyMessageBoxShow("No Data is found in Grid, Load Data in grid and then Export to PDF", Me.Text)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class
