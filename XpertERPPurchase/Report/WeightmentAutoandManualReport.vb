Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class Weightment_Auto_and_Manual_Report

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        'qry += " where 2=2 and Seg_No = '7' AND GIT='N' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Code", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub


    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub Reset()
        rbtLoadSlip.Checked = False
        rbtWeightment.Checked = True
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtLocation.arrValueMember = Nothing
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If


            If gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))



            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            End If

            If gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Vendor Payment Details Report", gv1, arrHeader, "Vendor Payment Details Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RmSecurityDeduction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'SetUserMgmtNew()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim strqry As String = ""
            GetReportID()
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            If rbtWeightment.Checked = True Then
                strqry = " SELECT aa.Location_Code as Location,
    ISNULL(SUM(aa.Auto_wt), 0) AS [Auto Weighment],
    ISNULL(SUM(aa.Manual_wt), 0) AS [Manual Weighment],
    ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0) AS [Pending],
    ISNULL(SUM(aa.Total_wt), 0) AS Total,
    CAST(ROUND((ISNULL(SUM(aa.Auto_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Auto%],
    CAST(ROUND((ISNULL(SUM(aa.Manual_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Manual%],
    CAST(ROUND(((ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0)) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Pending%]
             FROM (SELECT Location_Code,
             CASE WHEN (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 1) THEN COUNT(*)END AS Auto_wt,
             CASE WHEN (Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 0) OR (Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 1) OR (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 0) THEN COUNT(*) 
             END AS Manual_wt, COUNT(*) AS Total_wt
             FROM TSPL_PO_WEIGHTMENT_HEAD 
             where convert(date,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date,103)<=convert(date,('" + txtToDate.Value + "'),103)
             GROUP BY Location_Code,Is_Auto_Gross_Weight,Is_Auto_Tare_Weight) aa  "

            Else
                strqry = " SELECT aa.Location_Code as Location,
    ISNULL(SUM(aa.Auto_wt), 0) AS [Auto Weighment],
    ISNULL(SUM(aa.Manual_wt), 0) AS [Manual Weighment],
    ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0) AS [Pending],
    ISNULL(SUM(aa.Total_wt), 0) AS Total,
    CAST(ROUND((ISNULL(SUM(aa.Auto_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Auto%],
    CAST(ROUND((ISNULL(SUM(aa.Manual_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Manual%],
    CAST(ROUND(((ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0)) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Pending%]
             FROM (SELECT Location_Code,
             CASE WHEN (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 1) THEN COUNT(*)END AS Auto_wt,
             CASE WHEN (Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 0) OR (Is_Auto_Gross_Weight = 0 AND Is_Auto_Tare_Weight = 1) OR (Is_Auto_Gross_Weight = 1 AND Is_Auto_Tare_Weight = 0) THEN COUNT(*) 
             END AS Manual_wt, COUNT(*) AS Total_wt
             FROM TSPL_RCDF_LOAD_IN 
             where convert(date,TSPL_RCDF_LOAD_IN.Document_Date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_RCDF_LOAD_IN.Document_Date,103)<=convert(date,('" + txtToDate.Value + "'),103)
             GROUP BY Location_Code,Is_Auto_Gross_Weight,Is_Auto_Tare_Weight) aa  "
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strqry += " where aa.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            strqry += "GROUP BY aa.Location_Code "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next

                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If rbtWeightment.Checked Then
            VarID += "_W"
        ElseIf rbtLoadSlip.Checked Then
            VarID += "_LS"
        End If
        gv1.VarID = VarID
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
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Dim qry As String = " "
        Dim str As String = ""
        Try
            If rbtWeightment.Checked = True Then
                str = "Weightment"
            ElseIf rbtLoadSlip.Checked = True Then
                str = "Load Slip"
            End If
            qry = " SELECT '" + str + "' as ReportName,'" + txtFromDate.Value + "' as Date1 ,'" + txtToDate.Value + "' as Date2,
            aa.Location_Code as Location,
    ISNULL(SUM(aa.Auto_wt), 0) AS [Auto Weighment],
    ISNULL(SUM(aa.Manual_wt), 0) AS [Manual Weighment],
    ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0) AS [Pending],
    ISNULL(SUM(aa.Total_wt), 0) AS Total,
    CAST(ROUND((ISNULL(SUM(aa.Auto_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Auto%],
    CAST(ROUND((ISNULL(SUM(aa.Manual_wt), 0) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Manual%],
    CAST(ROUND(((ISNULL(SUM(aa.Total_wt), 0) - ISNULL(SUM(aa.Auto_wt), 0) - ISNULL(SUM(aa.Manual_wt), 0)) * 100.0) / ISNULL(SUM(aa.Total_wt), 0), 2) AS DECIMAL(5, 2)) AS [Pending%],
    aa.Location_Desc
            FROM (SELECT r.Location_Code,l.Location_Desc,CASE 
            WHEN (r.Is_Auto_Gross_Weight = 1 AND r.Is_Auto_Tare_Weight = 1) THEN COUNT(*) END AS Auto_wt,CASE 
            WHEN (r.Is_Auto_Gross_Weight = 0 AND r.Is_Auto_Tare_Weight = 0) 
            OR (r.Is_Auto_Gross_Weight = 0 AND r.Is_Auto_Tare_Weight = 1) 
            OR (r.Is_Auto_Gross_Weight = 1 AND r.Is_Auto_Tare_Weight = 0) 
            THEN COUNT(*)END AS Manual_wt,COUNT(*) AS Total_wt FROM TSPL_RCDF_LOAD_IN r
            LEFT JOIN TSPL_LOCATION_MASTER l ON l.Location_Code = r.Location_Code
            where convert(date,r.Document_Date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,r.Document_Date,103)<=convert(date,('" + txtToDate.Value + "'),103)
            GROUP BY 
            r.Location_Code, l.Location_Desc, r.Is_Auto_Gross_Weight, r.Is_Auto_Tare_Weight ) aa  "

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " where aa.Location_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            qry += " GROUP BY aa.Location_Code, aa.Location_Desc ORDER BY aa.Location_Code "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.Purchase, dt, "WeightmentAutoandManualReport", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class
