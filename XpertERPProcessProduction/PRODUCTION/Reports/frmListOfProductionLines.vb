Imports common
'''' <summary>
'''' ''''Ticket No:BM00000000508
'''' </summary>
'''' <remarks></remarks>


Public Class FrmListOfProductionLines
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Functions"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.PRODREPORT)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnprint.Visible = MyBase.isPrintFlag
    End Sub
    Sub LoadToolType()
        Dim strquery As String = "select PRODUCTION_LINE_CODE as Code,PRODUCTION_LINE_NAME as Name from  TSPL_MF_PRODUCTION_LINES  where 2=2 "
        cbgProduction.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgProduction.ValueMember = "Code"
        cbgProduction.DisplayMember = "Description"
    End Sub
    Sub Print()

        Dim qry As String = " select PRODUCTION_LINE_CODE as Code ,PRODUCTION_LINE_NAME as Name ,DESCRIPTION  as Description from TSPL_MF_PRODUCTION_LINES "
  qry += " where 2=2"


       
        If chkProductionSelect.IsChecked AndAlso cbgProduction.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast one ProductionLine", Me.Text)
            Return
        ElseIf chkProductionSelect.IsChecked AndAlso cbgProduction.CheckedValue.Count > 0 Then
            qry += " and  PRODUCTION_LINE_CODE in (" + clsCommon.GetMulcallString(cbgProduction.CheckedValue) + ")"
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
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
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
        LoadToolType()
       fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        gv.DataSource = Nothing
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

    Private Sub FrmListOfProductionLines_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+S for Print ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        SetUserMgmtNew()
    End Sub

    Private Sub FrmListOfProductionLines_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Private Sub chkProductionAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkProductionAll.ToggleStateChanged
        cbgProduction.Enabled = chkProductionSelect.IsChecked
    End Sub
#End Region

End Class
