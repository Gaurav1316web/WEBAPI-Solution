Imports common
'''' <summary>
'''' ''''''''BM00000000510'''''''''''''
'''' </summary>
'''' <remarks></remarks>



Public Class RptListOf_Resource
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.Resource)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        'End If
        'btnprint.Visible = MyBase.isPrintFlag
    End Sub
    Sub LoadResource()
        Dim strquery As String = "select RESOURCE_CODE as [Code], DESCRIPTION as [Description]  from TSPL_MF_RESOURCE_MASTER where 2=2 "
        cbgResource.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgResource.ValueMember = "Code"
        cbgResource.DisplayMember = "Description"
    End Sub
    Sub Print()
       
        Dim qry As String = "select RESOURCE_CODE as [Code], STATUS AS [Status], DESCRIPTION as [Description], RESOURCE_TYPE as [Type], UNIT_CODE as [Unit Code], COST as [Cost], COMMENTS as [COMMENTS]  from TSPL_MF_RESOURCE_MASTER where 2=2"
        If cboStatus.SelectedValue <> "NA" Then
            qry += " and STATUS='" + cboStatus.SelectedValue + "' "
        End If
        If cboType.SelectedValue <> "NA" Then
            qry += " and RESOURCE_TYPE='" + cboType.SelectedValue + "'"
        End If
        If fnduom.Value <> "" Then
            qry += " and UNIT_CODE='" + fnduom.Value + "'"
        End If
        If chkResourceSelect.IsChecked AndAlso cbgResource.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast one resource")
        ElseIf chkResourceSelect.IsChecked AndAlso cbgResource.CheckedValue.Count > 0 Then
            qry += " and  RESOURCE_CODE in (" + clsCommon.GetMulcallString(cbgResource.CheckedValue) + ")"
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
        fnduom.Value = ""
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        cboStatus.DataSource = GetCboStatusDataTable()
        cboStatus.ValueMember = "RESOURCE_CODE"
        cboStatus.DisplayMember = "Value"
        cboStatus.SelectedIndex = 0

      
        cboType.DataSource = GetCboTypeDataTable()
        cboType.ValueMember = "RESOURCE_CODE"
        cboType.DisplayMember = "Value"
        cboType.SelectedIndex = 0
        cboType.Enabled = True
        gv.DataSource = Nothing
    End Sub
    Private Function GetCboStatusDataTable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("RESOURCE_CODE", GetType(String))
        dt.Columns.Add("Value", GetType(String))

        Dim dr As DataRow

        dr = dt.NewRow()
        dr("RESOURCE_CODE") = "NA"
        dr("Value") = "NA"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("RESOURCE_CODE") = "Active"
        dr("Value") = "Active"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("RESOURCE_CODE") = "Inactive"
        dr("Value") = "Inactive"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("RESOURCE_CODE") = "Discontinued"
        dr("Value") = "Discontinued"
        dt.Rows.Add(dr)

        dt.AcceptChanges()
        Return dt
    End Function

    'Private Function GetCboUOMDataTable() As DataTable
    'Dim dt As New DataTable
    '    dt.Columns.Add("RESOURCE_CODE", GetType(String))
    '    dt.Columns.Add("Value", GetType(String))

    'Dim dr As DataRow

    '    dr = dt.NewRow()
    '    dr("RESOURCE_CODE") = "NA"
    '    dr("Value") = "NA"
    '    dt.Rows.Add(dr)


    '    dr = dt.NewRow()
    '    dr("RESOURCE_CODE") = "Hour"
    '    dr("Value") = "Hour"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("RESOURCE_CODE") = "Minute"
    '    dr("Value") = "Minute"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("RESOURCE_CODE") = "Others"
    '    dr("Value") = "Others"
    '    dt.Rows.Add(dr)

    '    dt.AcceptChanges()
    '    Return dt
    'End Function

    Private Function GetCboTypeDataTable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("RESOURCE_CODE", GetType(String))
        dt.Columns.Add("Value", GetType(String))

        Dim dr As DataRow

        dr = dt.NewRow()
        dr("RESOURCE_CODE") = "NA"
        dr("Value") = "NA"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("RESOURCE_CODE") = "Setup Labor"
        dr("Value") = "Setup Labor"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("RESOURCE_CODE") = "Run Labor"
        dr("Value") = "Run Labor"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("RESOURCE_CODE") = "Overhead"
        dr("Value") = "Overhead"
        dt.Rows.Add(dr)

        dt.AcceptChanges()
        Return dt
    End Function
#End Region
#Region "Events"
    Private Sub fnduom__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnduom._MYValidating
        Dim qry As String = " Select Unit_Code  as Code,Unit_Desc  as Description from TSPL_UNIT_MASTER  "
        fnduom.Value = clsCommon.ShowSelectForm("RECd2", qry, "Code", "", fnduom.Value, "", isButtonClicked)

    End Sub
    Private Sub RptListOf_Resource_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+S for Print ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        SetUserMgmtNew()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()


    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub chkResourceAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkResourceAll.ToggleStateChanged
        cbgResource.Enabled = chkResourceSelect.IsChecked
    End Sub
    Private Sub RptListOf_Resource_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
#End Region
End Class
