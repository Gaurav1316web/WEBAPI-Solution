Imports common
'''' <summary>
'''' ''''''BM00000000514''''''''''''''
'''' </summary>
'''' <remarks></remarks>
Public Class RptListOfAlternateItem
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Functions"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.LALT)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnprint.Visible = MyBase.isPrintFlag
    End Sub
    Sub LoadItems()
        Dim strquery As String = "Select distinct  TSPL_MF_SUBSTITUTE_ITEMS.ITEM_CODE as Code,A.Item_Desc As Description from TSPL_MF_SUBSTITUTE_ITEMS Left outer join TSPL_ITEM_MASTER A  on TSPL_MF_SUBSTITUTE_ITEMS.ITEM_CODE  =A.Item_Code  where 2=2 "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgItem.ValueMember = "Code"
        cbgItem.DisplayMember = "Description"
    End Sub
    Sub Print()

        Dim qry As String = "select  TSPL_MF_SUBSTITUTE_ITEMS.ITEM_CODE as Code,A.Item_Desc As Description,TSPL_MF_SUBSTITUTE_ITEMS.SUBSTITUTE_ITEM_CODE As [Substitute Code],TSPL_MF_SUBSTITUTE_ITEMS.DESCRIPTION as [Subst. Description],TSPL_MF_SUBSTITUTE_ITEMS.QUANTITY as Qty,TSPL_MF_SUBSTITUTE_ITEMS.UNIT_CODE As UOM ,TSPL_UNIT_MASTER.Unit_Desc as [UOM Desc],TSPL_MF_SUBSTITUTE_ITEMS.PRIORITY as Priority,TSPL_MF_SUBSTITUTE_ITEMS.COMMENTS as Comments  from TSPL_MF_SUBSTITUTE_ITEMS"
        qry += " Left outer join TSPL_ITEM_MASTER A  on TSPL_MF_SUBSTITUTE_ITEMS.ITEM_CODE  =A.Item_Code "
        qry += " left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code =TSPL_MF_SUBSTITUTE_ITEMS.UNIT_CODE where 2=2 "
        If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast one ToolType")
            Return
        ElseIf chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
            qry += " and  TSPL_MF_SUBSTITUTE_ITEMS.ITEM_CODE in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
        End If
        qry += " and convert(date,TSPL_MF_SUBSTITUTE_ITEMS.created_date,103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  "
        qry += "convert(date,TSPL_MF_SUBSTITUTE_ITEMS.created_date,103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'   "

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
            gv.EnableGrouping = True
        End If
        gv.GroupDescriptors.Add(New GridGroupByExpression("Code as Code format ""{0}: {1}"" Group By Code"))
     
        gv.MasterTemplate.ExpandAllGroups()
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True



        gv.MasterTemplate.AllowAddNewRow = False
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub
 
    Sub Reset()
        LoadItems()

        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()

        gv.DataSource = Nothing
    End Sub
    'Private Function GetCboStatusDataTable() As DataTable
    '    Dim dt As New DataTable
    '    dt.Columns.Add("RESOURCE_CODE", GetType(String))
    '    dt.Columns.Add("Value", GetType(String))

    '    Dim dr As DataRow

    '    dr = dt.NewRow()
    '    dr("RESOURCE_CODE") = "NA"
    '    dr("Value") = "NA"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("RESOURCE_CODE") = "Active"
    '    dr("Value") = "Active"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("RESOURCE_CODE") = "Inactive"
    '    dr("Value") = "Inactive"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("RESOURCE_CODE") = "Discontinued"
    '    dr("Value") = "Discontinued"
    '    dt.Rows.Add(dr)

    '    dt.AcceptChanges()
    '    Return dt
    'End Function

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

    'Private Function GetCboTypeDataTable() As DataTable
    '    Dim dt As New DataTable
    '    dt.Columns.Add("RESOURCE_CODE", GetType(String))
    '    dt.Columns.Add("Value", GetType(String))

    '    Dim dr As DataRow

    '    dr = dt.NewRow()
    '    dr("RESOURCE_CODE") = "NA"
    '    dr("Value") = "NA"
    '    dt.Rows.Add(dr)


    '    dr = dt.NewRow()
    '    dr("RESOURCE_CODE") = "Setup Labor"
    '    dr("Value") = "Setup Labor"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("RESOURCE_CODE") = "Run Labor"
    '    dr("Value") = "Run Labor"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("RESOURCE_CODE") = "Overhead"
    '    dr("Value") = "Overhead"
    '    dt.Rows.Add(dr)

    '    dt.AcceptChanges()
    '    Return dt
    'End Function
#End Region

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptListOfAlternateItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+S for Print ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        SetUserMgmtNew()
    End Sub

    Private Sub RptListOfAlternateItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = chkItemSelect.IsChecked
    End Sub
End Class
