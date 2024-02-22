Imports common
Public Class RptComplainfeedback


    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        cmbReportType.Text = "All"
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        txtCustomer.arrValueMember = Nothing
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Type : " & cmbReportType.Text)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate1.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(MyDateTimePicker1.Value, "dd/MM/yyyy")) + " ")
                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMccMasterDetail & "'"))
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Type : " & cmbReportType.Text)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate1.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(MyDateTimePicker1.Value, "dd/MM/yyyy")) + " ")
                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMccMasterDetail & "'"))

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        Dim dt As New DataTable
        Dim whre As String = ""
        Dim Qry As String = "select TSPL_ANDROID_COMPLAIN_FEEDBACK.Doc_Code,TSPL_ANDROID_COMPLAIN_FEEDBACK.Doc_Date,TSPL_ANDROID_COMPLAIN_FEEDBACK.Type,TSPL_ANDROID_COMPLAIN_FEEDBACK.Remarks,TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_ANDROID_COMPLAIN_FEEDBACK
                            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_ANDROID_COMPLAIN_FEEDBACK.Cust_Code"
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            whre += " and  TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
        End If
        If clsCommon.CompairString(cmbReportType.Text, "Complain") = CompairStringResult.Equal Then
            whre += " and  TSPL_ANDROID_COMPLAIN_FEEDBACK.Type='C' "
        ElseIf clsCommon.CompairString(cmbReportType.Text, "Feedback") = CompairStringResult.Equal Then
            whre += " and  TSPL_ANDROID_COMPLAIN_FEEDBACK.Type='F' "
        End If

        whre += " and convert(date,TSPL_ANDROID_COMPLAIN_FEEDBACK.Doc_Date,103)>=convert(date,('" + dtpfromdate1.Value + "'),103) and convert(date,TSPL_ANDROID_COMPLAIN_FEEDBACK.Doc_Date,103)<=convert(date,('" + MyDateTimePicker1.Value + "'),103)"
        Qry = Qry + whre
        dt = clsDBFuncationality.GetDataTable(Qry)
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.MasterView.Refresh()
        Gv1.DataSource = dt
        SetGridFormat()
    End Sub

    Sub SetGridFormat()
        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item As New GridViewSummaryItem
        If Gv1.Rows IsNot Nothing AndAlso Gv1.Columns.Count > 0 Then
            For ii As Integer = 0 To Gv1.Columns.Count - 1
                If ii > 5 Then
                    item = New GridViewSummaryItem(Gv1.Columns(ii).HeaderText, "{0:n2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item)
                End If
            Next
        End If
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click

        Dim qry As String = " select TSPL_CUSTOMER_MASTER.Cust_Code as Code , TSPL_CUSTOMER_MASTER.Customer_Name as Name from TSPL_CUSTOMER_MASTER "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelCust1", qry, "Code", "Code", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPendingGrn_Qty)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub FrmPendingGrn_Qty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpfromdate1.Value = clsCommon.GETSERVERDATE()
        MyDateTimePicker1.Value = clsCommon.GETSERVERDATE()
        cmbReportType.Text = "All"
    End Sub
End Class
