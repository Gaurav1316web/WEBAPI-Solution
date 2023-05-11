Imports common
'''' <summary>
'''' '''''''''''''''''''''''''Ticket No:BM00000000508''''''''''''
'''' </summary>
'''' <remarks></remarks>
Public Class RptListOfWorkCenter
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Functions"

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.LWC)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnprint.Visible = MyBase.isPrintFlag
    End Sub
    Sub LoadResource()
        Dim strquery As String = "select WORK_CENTER_CODE as Code ,DESCRIPTION as Description  from TSPL_MF_WORK_CENTER where 2=2 "
        cbgWorkCenter.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgWorkCenter.ValueMember = "Code"
        cbgWorkCenter.DisplayMember = "Description"
    End Sub
    Sub Print()

        Dim qry As String = "select WORK_CENTER_CODE as Code ,DESCRIPTION as Description  from TSPL_MF_WORK_CENTER where 2=2"

        If chkWorkCenterSelect.IsChecked AndAlso cbgWorkCenter.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast one WorkCenter")
        ElseIf chkWorkCenterSelect.IsChecked AndAlso cbgWorkCenter.CheckedValue.Count > 0 Then
            qry += " and  WORK_CENTER_CODE in (" + clsCommon.GetMulcallString(cbgWorkCenter.CheckedValue) + ")"
        End If
        qry += " and convert(date,created_date,103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  "
        qry += "convert(date,created_date,103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'   "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        gv.EnableFiltering = True

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            gv.DataSource = dt
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).Width = 100
                gv.Columns(gv.Columns.Count - 1).Width = 500
            Next
            gv.EnableGrouping = False
        End If

        gv.MasterTemplate.AllowAddNewRow = False
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub
    Sub Reset()
        LoadResource()

        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()

    End Sub

#End Region
#Region "Events"

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptListOfWorkCenter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+S for Print ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        SetUserMgmtNew()
    End Sub

    Private Sub RptListOfWorkCenter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub chkWorkCenterAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkWorkCenterAll.ToggleStateChanged
        cbgWorkCenter.Enabled = chkWorkCenterSelect.IsChecked
    End Sub
#End Region

End Class
