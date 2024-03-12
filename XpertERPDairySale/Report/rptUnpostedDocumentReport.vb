Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class rptUnpostedDocumentReport
    Inherits FrmMainTranScreen
#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
    End Sub
    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControls(True)
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Sub LoadData()
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable
            If rbtnDispatch.IsChecked Then
                qry = " select Document_Code as [Document Code], convert (varchar, Document_Date,103) as [Document Date] , DESCRIPTION as [Description] from TSPL_UNPOSTED_DISPATCH_DOC_SCHEDULAR where convert(date,Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)  "
            ElseIf rbtnAutoPaymentProcess.IsChecked Then
                qry = "select TSPL_MCC_PAYMENT_CYCLE_LOCK_FOR_SCHEDULER.Code,TSPL_MCC_PAYMENT_CYCLE_LOCK_FOR_SCHEDULER.MCC_Code as [MCC Code],TSPL_MCC_MASTER.MCC_NAME as [MCC],TSPL_MCC_PAYMENT_CYCLE_LOCK_FOR_SCHEDULER.From_Date as [From Date],TSPL_MCC_PAYMENT_CYCLE_LOCK_FOR_SCHEDULER.To_Date as [To Date],(case when Status=0 and len(isnull(Exception_Desc,''))=0 then 'Pending In Bill Generation' else (case when Status=0 and len(isnull(Exception_Desc,''))>0 then 'Error In Bill Generation' else (case when Status=1 and len(isnull(Exception_Desc,''))=0 then 'Pending For Payment Process' else (case when Status=1 and len(isnull(Exception_Desc,''))>0 then 'Error in Payment Process Saving' else (case when Status=2 and len(isnull(Exception_Desc,''))=0 then 'Pending For Post Payment Process' else (case when Status=2 and len(isnull(Exception_Desc,''))>0 then 'Error In Payment Process Post' else 'Posted' end) end) end) end) end) end) as Status,Payment_Process_Code as [Payment Process Code],Exception_Desc as [Exception] " + Environment.NewLine +
                "From TSPL_MCC_PAYMENT_CYCLE_LOCK_FOR_SCHEDULER " + Environment.NewLine +
                "left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MCC_PAYMENT_CYCLE_LOCK_FOR_SCHEDULER.MCC_Code" + Environment.NewLine +
                "where TSPL_MCC_PAYMENT_CYCLE_LOCK_FOR_SCHEDULER.Status<>3 and convert(date,TSPL_MCC_PAYMENT_CYCLE_LOCK_FOR_SCHEDULER.From_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_MCC_PAYMENT_CYCLE_LOCK_FOR_SCHEDULER.From_Date,103)<=convert(date,'" + ToDate.Value + "',103)  "
            End If

            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                Gv1.BestFitColumns()
                EnableDisableControls(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptUnpostedDocumentReport & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)


            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Unposted Document Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Unposted Document Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub
    Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox3.Enabled = val
        GroupBox1.Enabled = val
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick ''BHO/11/06/21-000003 By Balwidner on 17/06/2021
        If rbtnAutoPaymentProcess.IsChecked Then
            If clsCommon.myLen(Gv1.CurrentRow.Cells("Code").Value) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Status").Value), "Pending In Bill Generation") = CompairStringResult.Equal Then
                    If clsCommon.MyMessageBoxShow(Me, "Do you want to Delete Lock No [" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Code").Value) + "]", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        clsMCCPaymentCycleLockForScheduler.DeleteData(clsCommon.myCstr(Gv1.CurrentRow.Cells("Code").Value))
                        LoadData()
                    End If
                ElseIf clsCommon.myLen(Gv1.CurrentRow.Cells("Exception").Value) > 0 Then
                    If clsCommon.MyMessageBoxShow(Me, "Do you want to Reschedule Lock No [" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Code").Value) + "] For auto payment process", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        clsDBFuncationality.ExecuteNonQuery("Update TSPL_MCC_PAYMENT_CYCLE_LOCK_FOR_SCHEDULER set Exception_Desc=null where Code='" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Code").Value) + "'")
                        Gv1.CurrentRow.Cells("Exception").Value = ""
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub rbtnAutoPaymentProcess_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnAutoPaymentProcess.ToggleStateChanged
        MyLabel1.Visible = rbtnAutoPaymentProcess.IsChecked
    End Sub
End Class
