Imports common
'''' <summary>
'''' ''''''''''''BM00000000515
'''' </summary>
'''' <remarks></remarks>
Public Class rptListOfItemCost
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Functions"

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.LOIC)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnprint.Visible = MyBase.isPrintFlag
    End Sub
    Sub LoadResource()
        Dim strquery As String = "select TSPL_MF_ITEM_COST_MAINTENANCE.ITEM_CODE as Code,TSPL_ITEM_MASTER.Item_Desc as Description from TSPL_MF_ITEM_COST_MAINTENANCE Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MF_ITEM_COST_MAINTENANCE.ITEM_CODE where 2=2 "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgItem.ValueMember = "Code"
        cbgItem.DisplayMember = "Description"
    End Sub
    Sub Print()

        Dim qry As String = "select TSPL_MF_ITEM_COST_MAINTENANCE.ITEM_CODE as Code,TSPL_ITEM_MASTER.Item_Desc as Description,MATERIAL_COST as MaterialCost,PACKAGING_COST as PackagingCost,SETUP_COST as SetupCost,LABOR_COST as laborCost,OVERHEAD_COST as OverheadCost,SUBCONTRACT_COST as SubcontractCost,TOOL_COST as ToolCost from TSPL_MF_ITEM_COST_MAINTENANCE"
        qry += " Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MF_ITEM_COST_MAINTENANCE.ITEM_CODE where 2=2"

        If chkItemCostSelect.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast one ItemCost")
        ElseIf chkItemCostSelect.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
            qry += " and  TSPL_MF_ITEM_COST_MAINTENANCE.ITEM_CODE in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
        End If
        qry += " and convert(date,TSPL_MF_ITEM_COST_MAINTENANCE.created_date,103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  "
        qry += "convert(date,TSPL_MF_ITEM_COST_MAINTENANCE.created_date,103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'   "

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
                'gv.Columns(gv.Columns.Count - 1).Width = 500
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

    Private Sub RptListOfOperations_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+S for Print ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        SetUserMgmtNew()
    End Sub

    Private Sub RptListOfOperations_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

   
    Private Sub chkItemCostAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemCostAll.ToggleStateChanged
        cbgItem.Enabled = chkItemCostSelect.IsChecked
    End Sub
#End Region
End Class
