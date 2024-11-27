Imports common
Public Class FrmHeadLoadHistory

    Private Sub FrmHeadLoadHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtMultDCS.Enabled = True
        txtMultDCS.Text = ""
        gv1.DataSource = Nothing
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmHeadLoadHistory & "'"))
            arrHeader.Add("Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy"))
            'If clsCommon.CompairString(cmbShift.Text, "Morning") = CompairStringResult.Equal Then
            '    arrHeader.Add("Shift Type : Morning")
            'ElseIf clsCommon.CompairString(cmbShift.Text, "Evening") = CompairStringResult.Equal Then
            '    arrHeader.Add("Shift Type : Evening")
            'End If
            arrHeader.Add("DCS : " + txtMultDCS.Text)
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtMultDCS__My_Click(sender As Object, e As EventArgs) Handles txtMultDCS._My_Click
        Dim qry As String = " select VLC_Code_VLC_Uploader as [Dcs Code],VLC_CODE as [Dcs],VLC_Name as [Dcs Name]  from TSPL_VLC_MASTER_HEAD "
        txtMultDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMulSelect", qry, "Dcs Code", "DCS Name", txtMultDCS.arrValueMember, txtMultDCS.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim whr As String = ""
            If txtMultDCS.arrValueMember IsNot Nothing AndAlso txtMultDCS.arrValueMember.Count > 0 Then
                whr = "And TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader in (" + clsCommon.GetMulcallString(txtMultDCS.arrValueMember) + ")"
            End If
            'whr += " TSPL_MCC_MASTER.MCC_NAME as [Location], "
            Dim strqry As String = " SELECT  
                                    ROW_NUMBER() OVER (ORDER BY (VLC_Code_VLC_Uploader)) As [S.No],
                                (VLC_Code_VLC_Uploader) As [Dcs Code],
                                (VLC_Name) As [Dcs Name],
                                (TSPL_HEAD_LOAD_DCS.Head_Load_Rate) As [Head Load Rate],
                                (TSPL_VENDOR_MASTER.Phone1) As [Mobile No],
                                (CONVERT(VARCHAR(10), TSPL_HEAD_LOAD.Start_Date, 103)) AS [Start Date],
                                (CONVERT(VARCHAR(10), TSPL_HEAD_LOAD.Document_date, 103)) AS [Document Date]
                            From TSPL_VLC_MASTER_HEAD
                            Left OUTER JOIN TSPL_HEAD_LOAD_DCS 
                                On TSPL_HEAD_LOAD_DCS.VLC_CODE = TSPL_VLC_MASTER_HEAD.VLC_CODE
                            Left Join TSPL_MCC_MASTER 
                                On TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC
                            Left Join TSPL_VENDOR_MASTER 
                                On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code
                            Left OUTER JOIN TSPL_HEAD_LOAD 
                                On TSPL_HEAD_LOAD_DCS.Document_No = TSPL_HEAD_LOAD.Document_No                           
                            where CAST(TSPL_HEAD_LOAD.Document_date AS DATE) >='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND CAST(TSPL_HEAD_LOAD.Document_date AS DATE) <='" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + whr + " "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
                If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.MasterView.Refresh()

                    gv1.DataSource = dt
                    For ii As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Columns(ii).ReadOnly = True
                    Next

                    'RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.BestFitColumns()
                    gv1.EnableFiltering = True
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
