Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class FarmerDetails

    Public Range As String

    Private Sub FarmerDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            UnionWiseFarmerDetail()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Sub UnionWiseFarmerDetail()
        Try
            Dim UnionName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select [TSPL_APP_LOCATION].Location_Name from [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] 
                              where [TSPL_APP_LOCATION].DataBase_Name=  '" + Range + "' "))

            Dim query As String = ""
            query = "   SELECT '" + UnionName + "' as UnionName,X.MP_Code,TSPL_MP_MASTER.MP_Name,TSPL_MP_MASTER.Father_Name,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,
                        CASE WHEN TSPL_MP_MASTER.Jan_Aadhar_No_Verified IS NULL OR TSPL_MP_MASTER.Jan_Aadhar_No_Verified = 0 THEN 'No' ELSE 'Yes' END AS Jan_Aadhar_No_Verified,
                        isnull(TSPL_MP_MASTER.JA_janaadhaarId,'')JA_janaadhaarId,isnull(TSPL_MP_MASTER.JA_aadhar,'')JA_aadhar,
                        TSPL_MP_MASTER.Telphone,TSPL_MP_MASTER.BankName,
                        TSPL_MP_MASTER.IFCICode,TSPL_MP_MASTER.AccountNO
                        FROM
                        (Select MP_Code from [" + Range + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL  GROUP BY MP_Code
                        )X
                        LEFT OUTER JOIN [" + Range + "].[dbo].TSPL_MP_MASTER ON TSPL_MP_MASTER.MP_Code = X.MP_Code
                        LEFT OUTER JOIN [" + Range + "].[dbo].TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code = [" + Range + "].[dbo].TSPL_MP_MASTER.VLC_Code"

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gvFarmerDetails.DataSource = Nothing
                gvFarmerDetails.Rows.Clear()
                gvFarmerDetails.Columns.Clear()
                gvFarmerDetails.GroupDescriptors.Clear()
                gvFarmerDetails.MasterTemplate.SummaryRowsBottom.Clear()
                gvFarmerDetails.MasterView.Refresh()
                gvFarmerDetails.DataSource = dt2
                For ii As Integer = 0 To gvFarmerDetails.Columns.Count - 1
                    gvFarmerDetails.Columns(ii).ReadOnly = True
                Next
                gvFarmerDetails.EnableFiltering = True
                SetGridFormat1()
                gvFarmerDetails.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat1()
        gvFarmerDetails.AutoExpandGroups = True
        gvFarmerDetails.ShowGroupPanel = True
        gvFarmerDetails.ShowRowHeaderColumn = False
        gvFarmerDetails.AllowAddNewRow = False
        gvFarmerDetails.AllowDeleteRow = False
        gvFarmerDetails.EnableFiltering = True
        gvFarmerDetails.ShowFilteringRow = True


        For ii As Integer = 0 To gvFarmerDetails.Columns.Count - 1
            gvFarmerDetails.Columns(ii).ReadOnly = True
            gvFarmerDetails.Columns(ii).BestFit()
            'gv1.Columns(ii).Width = 200
        Next

        gvFarmerDetails.Columns("MP_Code").HeaderText = "MP Code"
        gvFarmerDetails.Columns("MP_Code").IsVisible = True
        'gvFarmerDetails.Width = 200

        gvFarmerDetails.Columns("MP_Name").HeaderText = "MP Name"
        gvFarmerDetails.Columns("MP_Name").IsVisible = True
        'gvFarmerDetails.Width = 200

        gvFarmerDetails.Columns("Father_Name").HeaderText = "Father Name"
        gvFarmerDetails.Columns("Father_Name").IsVisible = True
        'gvFarmerDetails.Width = 200

        gvFarmerDetails.Columns("VLC_Name").HeaderText = "DCS Name"
        gvFarmerDetails.Columns("VLC_Name").IsVisible = True
        'gvFarmerDetails.Width = 200

        gvFarmerDetails.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Code"
        gvFarmerDetails.Columns("VLC_Code_VLC_Uploader").IsVisible = True
        'gvFarmerDetails.Width = 200

        gvFarmerDetails.Columns("Jan_Aadhar_No_Verified").HeaderText = "Jan Aadhar No.Verified"
        gvFarmerDetails.Columns("Jan_Aadhar_No_Verified").IsVisible = True
        ' gvFarmerDetails.Width = 200

        gvFarmerDetails.Columns("JA_janaadhaarId").HeaderText = "Jan Aadhar No."
        gvFarmerDetails.Columns("JA_janaadhaarId").IsVisible = True
        ' gvFarmerDetails.Width = 200

        gvFarmerDetails.Columns("JA_aadhar").HeaderText = "Aadhar No."
        gvFarmerDetails.Columns("JA_aadhar").IsVisible = True
        ' gvFarmerDetails.Width = 200

        gvFarmerDetails.Columns("Telphone").HeaderText = "Telphone"
        gvFarmerDetails.Columns("Telphone").IsVisible = True
        ' gvFarmerDetails.Width = 200

        gvFarmerDetails.Columns("BankName").HeaderText = "Bank Name"
        gvFarmerDetails.Columns("BankName").IsVisible = True
        ' gvFarmerDetails.Width = 200

        gvFarmerDetails.Columns("IFCICode").HeaderText = "IFSC Code"
        gvFarmerDetails.Columns("IFCICode").IsVisible = True
        ' gvFarmerDetails.Width = 200

        gvFarmerDetails.Columns("AccountNO").HeaderText = "Account NO."
        gvFarmerDetails.Columns("AccountNO").IsVisible = True
        ' gvFarmerDetails.Width = 200

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gvFarmerDetails.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                ' arrHeader.Add("Union : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                ' arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))


                transportSql.applyExportTemplate(gvFarmerDetails, PageSetupReport_ID)
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, False)
                clsCommon.MyExportToExcelGrid(Me.Text, gvFarmerDetails, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gvFarmerDetails.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                ' arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                'arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))

                transportSql.applyExportTemplate(gvFarmerDetails, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gvFarmerDetails, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Dim query As String = ""
        query = "   SELECT X.MP_Code,TSPL_MP_MASTER.MP_Name,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,
                        CASE WHEN TSPL_MP_MASTER.Jan_Aadhar_No_Verified IS NULL OR TSPL_MP_MASTER.Jan_Aadhar_No_Verified = 0 THEN 'No' ELSE 'Yes' END AS Jan_Aadhar_No_Verified,TSPL_MP_MASTER.Telphone,TSPL_MP_MASTER.BankName,
                        TSPL_MP_MASTER.IFCICode,TSPL_MP_MASTER.AccountNO
                        FROM
                        (Select MP_Code from [" + Range + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL  GROUP BY MP_Code
                        )X
                        LEFT OUTER JOIN [" + Range + "].[dbo].TSPL_MP_MASTER ON TSPL_MP_MASTER.MP_Code = X.MP_Code
                        LEFT OUTER JOIN [" + Range + "].[dbo].TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code = [" + Range + "].[dbo].TSPL_MP_MASTER.VLC_Code"

        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)

        If dt2 IsNot Nothing And dt2.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "CrptFarmerDetails", "")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub
End Class