Imports common
'''' <summary>
'''' ''''''BM00000000512
'''' </summary>
'''' <remarks></remarks>
Public Class RptListOfTools
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Functions"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.LToolT)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnprint.Visible = MyBase.isPrintFlag
    End Sub
    Sub LoadToolType()
        Dim strquery As String = "select TOOL_TYPE_CODE as [Code], DESCRIPTION as [Description]  from TSPL_MF_TOOL_TYPE where 2=2 "
        cbgTool.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgTool.ValueMember = "Code"
        cbgTool.DisplayMember = "Description"
    End Sub
    Sub LoadTool()
        Dim strquery As String = "Select TOOL_CODE  as Code,DESCRIPTION from TSPL_MF_TOOL_MASTER where 2=2 "
        cbgtoolM.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgtoolM.ValueMember = "Code"
        cbgtoolM.DisplayMember = "DESCRIPTION"
    End Sub
    Sub LoadSupplier()
        Dim strquery As String = "Select Vendor_Code as Code,Vendor_Name as Description from TSPL_VENDOR_MASTER where 2=2 "
        cbgSupplier.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgSupplier.ValueMember = "Code"
        cbgSupplier.DisplayMember = "Description"
    End Sub
    Sub Print()

        Dim qry As String = " select   TOOL_CODE as Code ,"
        qry += "   TSPL_MF_TOOL_MASTER.DESCRIPTION as Description ,"
        qry += "   TSPL_MF_TOOL_MASTER.STATUS as Status,"
        qry += "    Convert(varchar,TSPL_MF_TOOL_MASTER.INACTIVE_DATE,103) as InactiveDate,"
        qry += " TSPL_MF_TOOL_MASTER. TOOL_TYPE_CODE as [ToolType Code],"
        qry += "  TSPL_MF_TOOL_TYPE.DESCRIPTION  as [ToolType Name] ,"
        qry += "  CONVERT (varchar, RECEIPT_DATE,103) as ReceiptDate ,"
        qry += "   RECEIVED_BY as ReceivedBy ,"
        qry += "   MAINTAINED_BY as MaintainedBy,"
        qry += "    SUPPLIER As SupplierCode ,"
        qry += "   TSPL_VENDOR_MASTER.Vendor_Name As SupplierName,"
        qry += "     RECEIPT_NUMBER as ReceiptNo,"
        qry += "   PO_NUMBER as PoNo,"
        qry += "    SERIAL_NUMBER as SNo ,"
        qry += "     CUSTODIAN as Custodian,"
        qry += "   Convert (varchar,REPLACEMENT_DATE ,103) as ReplacementDate,"
        qry += "   TSPL_MF_TOOL_MASTER. COMMENTS As Comments ,"
        qry += "    TSPL_MF_TOOL_MASTER. COST_PER_UNIT As CostPerUnit,"
        qry += "    TSPL_MF_TOOL_MASTER. ORIGINAL_QUANTITY As OriginalQty ,"
        qry += "     TSPL_MF_TOOL_MASTER.CONSUMED As ConsumedQty,"
        qry += "     TSPL_MF_TOOL_MASTER.ON_HAND_QUANTITY as OnHandQty ,"
        qry += "      TSPL_MF_TOOL_MASTER.ON_HAND_COST as OnHandcost ,"
        qry += "     TSPL_MF_TOOL_MASTER. UNIT_CODE As UOM,"
        qry += "    TSPL_UNIT_MASTER.Unit_Desc as [UOM Desc]"
        qry += "      from TSPL_MF_TOOL_MASTER"
        qry += "   Left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_MF_TOOL_MASTER.SUPPLIER "
        qry += "   Left Outer Join TSPL_MF_TOOL_TYPE  on TSPL_MF_TOOL_TYPE.TOOL_TYPE_CODE =TSPL_MF_TOOL_MASTER.TOOL_TYPE_CODE "
        qry += "   left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code =TSPL_MF_TOOL_MASTER.UNIT_CODE "
        qry += "   where 2=2"
        If cboStatus.SelectedValue <> "NA" Then
            qry += " and TSPL_MF_TOOL_MASTER. STATUS='" + cboStatus.SelectedValue + "' "
        End If

        If fnduom.Value <> "" Then
            qry += " and TSPL_MF_TOOL_MASTER.UNIT_CODE='" + fnduom.Value + "'"
        End If

        If chkToolSelect.IsChecked AndAlso cbgTool.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast one ToolType")
            Return
        ElseIf chkToolSelect.IsChecked AndAlso cbgTool.CheckedValue.Count > 0 Then
            qry += " and  TSPL_MF_TOOL_MASTER.TOOL_TYPE_CODE in (" + clsCommon.GetMulcallString(cbgTool.CheckedValue) + ")"
        End If

        If chkToolMSelect.IsChecked AndAlso cbgtoolM.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast one Tool")
            Return
        ElseIf chkToolMSelect.IsChecked AndAlso cbgtoolM.CheckedValue.Count > 0 Then
            qry += " and  TSPL_MF_TOOL_MASTER.TOOL_CODE in (" + clsCommon.GetMulcallString(cbgtoolM.CheckedValue) + ")"
        End If

        If ChksupplierSelect.IsChecked AndAlso cbgSupplier.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast one Supplier")
            Return
        ElseIf ChksupplierSelect.IsChecked AndAlso cbgSupplier.CheckedValue.Count > 0 Then
            qry += " and  TSPL_MF_TOOL_MASTER.SUPPLIER in (" + clsCommon.GetMulcallString(cbgSupplier.CheckedValue) + ")"
        End If

        qry += " and convert(date,TSPL_MF_TOOL_MASTER.created_date,103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  "
        qry += "convert(date,TSPL_MF_TOOL_MASTER.created_date,103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'   "

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
        LoadToolType()
        LoadSupplier()
        LoadTool()
        fnduom.Value = ""
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        cboStatus.DataSource = GetCboStatusDataTable()
        cboStatus.ValueMember = "RESOURCE_CODE"
        cboStatus.DisplayMember = "Value"
        cboStatus.SelectedIndex = 0
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
#Region "Finders"

#End Region
    Private Sub fnduom__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnduom._MYValidating
        Dim qry As String = " Select Unit_Code  as Code,Unit_Desc  as Description from TSPL_UNIT_MASTER  "
        fnduom.Value = clsCommon.ShowSelectForm("REC_CONfnd2", qry, "Code", "", fnduom.Value, "", isButtonClicked)
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptListOfTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+S for Print ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        SetUserMgmtNew()
    End Sub

    Private Sub RptListOfTools_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub chkToolAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkToolAll.ToggleStateChanged
        cbgTool.Enabled = chkToolSelect.IsChecked
    End Sub

    Private Sub chkToolMAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkToolMAll.ToggleStateChanged
        cbgtoolM.Enabled = chkToolMSelect.IsChecked
    End Sub

    Private Sub ChksupplierAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChksupplierAll.ToggleStateChanged
        cbgSupplier.Enabled = ChksupplierSelect.IsChecked
    End Sub
End Class
