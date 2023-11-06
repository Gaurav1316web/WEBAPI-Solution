Imports common
'''' <summary>
'''' ''''''BM00000000533
'''' </summary>
'''' <remarks></remarks>
Public Class FrmBatchReceipt
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim formtype As String = Nothing

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
#Region "Functions"
    Private Sub SetUserMgmtNew()      
        If formtype = clsUserMgtCode.FrmBatchReceiptSTD Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.FrmBatchReceiptSTD)
        ElseIf formtype = clsUserMgtCode.FrmBatchReceiptPepsi Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.FrmBatchReceiptPepsi)
        End If
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Sub LoadBO()
        Dim strquery As String = "Select BO_CODE as Code,DESCRIPTION as Description,BO_DATE as BODate from TSPL_MF_BATCH_ORDER where 2=2 "
        cbgBO.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgBO.ValueMember = "Code"
        cbgBO.DisplayMember = "Description"
    End Sub
    Sub LoadReceipt()
        Dim strquery As String = "Select receipt_code as Code,DESCRIPTION as Description,RECEIPT_DATE as ReceiptDate from TSPL_MF_RECEIPT where 2=2 "
        cbgReceipt.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgReceipt.ValueMember = "Code"
        cbgReceipt.DisplayMember = "Description"
    End Sub
    Sub LoadItem()
        Dim qry As String = " select item_code ,item_Desc  from TSPL_ITEM_MASTER order by Item_Code "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "item_code"
        cbgItem.DisplayMember = "item_Desc"
    End Sub
    Sub Print()

        Dim qry As String = "Select TSPL_MF_BATCH_ORDER.BO_CODE ,convert(varchar,TSPL_MF_BATCH_ORDER.BO_DATE,103) as BO_DATE, TSPL_MF_BATCH_PP_DETAIL.ITEM_CODE as BO_ItemCode,TSPL_ITEM_MASTER.Item_Desc as BODescription, TSPL_MF_RECEIPT.RECEIPT_CODE , convert(varchar,TSPL_MF_RECEIPT.RECEIPT_DATE,103) as RECEIPT_DATE,TSPL_MF_RECEIPT_DETAIL.ITEM_CODE as Receipt_ItemCode,TSPL_MF_RECEIPT_DETAIL.ITEM_DESCRIPTION as Receipt_ItemDESCRIPTION,TSPL_MF_RECEIPT_DETAIL.BATCH_QTY  ,TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY ,"
        qry += "   (TSPL_MF_RECEIPT_DETAIL.BATCH_QTY  -TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY) as Difference"
        qry += "   from TSPL_MF_BATCH_ORDER "
        qry += "  left outer join TSPL_MF_RECEIPT on TSPL_MF_BATCH_ORDER.BO_CODE =TSPL_MF_RECEIPT.BO_CODE "
        qry += "  left outer join TSPL_MF_BATCH_PP_DETAIL on TSPL_MF_BATCH_ORDER.BO_CODE =TSPL_MF_BATCH_PP_DETAIL.BO_CODE"
        qry += "   left outer join TSPL_MF_RECEIPT_DETAIL on TSPL_MF_RECEIPT.RECEIPT_CODE =TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE "
        qry += "     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_MF_BATCH_PP_DETAIL.ITEM_CODE "

        qry += "   where 2=2"
        'If cboStatus.SelectedValue <> "NA" Then
        '    qry += " and TSPL_MF_TOOL_MASTER. STATUS='" + cboStatus.SelectedValue + "' "
        'End If

        'If fnduom.Value <> "" Then
        '    qry += " and TSPL_MF_TOOL_MASTER.UNIT_CODE='" + fnduom.Value + "'"
        'End If

        If chkBOSelect.IsChecked AndAlso cbgBO.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast one BatchOrder")
            Return
        ElseIf chkBOSelect.IsChecked AndAlso cbgBO.CheckedValue.Count > 0 Then
            qry += " and  TSPL_MF_BATCH_ORDER.BO_CODE in (" + clsCommon.GetMulcallString(cbgBO.CheckedValue) + ")"
        End If

        If chkReceiptSelect.IsChecked AndAlso cbgReceipt.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast one Receipt")
            Return
        ElseIf chkReceiptSelect.IsChecked AndAlso cbgReceipt.CheckedValue.Count > 0 Then
            qry += " and  TSPL_MF_RECEIPT.RECEIPT_CODE in (" + clsCommon.GetMulcallString(cbgReceipt.CheckedValue) + ")"
        End If

        'If chkIemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast one Item")
        '    Return
        'ElseIf chkIemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
        '    qry += " and  TSPL_MF_TOOL_MASTER.SUPPLIER in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
        'End If

        qry += " and convert(date,TSPL_MF_BATCH_ORDER.BO_DATE,103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  "
        qry += "convert(date,TSPL_MF_BATCH_ORDER.BO_DATE,103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'   "

        If rdbSummary.IsChecked Then
            qry = "select BO_CODE ,BO_DATE,  RECEIPT_CODE,RECEIPT_DATE ,sum(BATCH_QTY ) as BATCH_QTY,SUM(RECEIPT_QTY ) as RECEIPT_QTY from (" + qry + ") as Bo Group by BO_CODE,RECEIPT_CODE ,BO_DATE,RECEIPT_DATE "
        End If
        qry += " order by  BO_DATE "
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
            SetGridFormationOFGV1()
        End If

        gv.MasterTemplate.AllowAddNewRow = False
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub
    Sub SetGridFormationOFGV1()
        'Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        If rdbDetail.IsChecked = True Then
            gv.Columns("BO_CODE").IsVisible = True
            gv.Columns("BO_CODE").Width = 100
            gv.Columns("BO_CODE").HeaderText = "BO Code"

            gv.Columns("BO_DATE").IsVisible = True
            gv.Columns("BO_DATE").Width = 70
            gv.Columns("BO_DATE").HeaderText = "BO Date"

            gv.Columns("BO_ItemCode").IsVisible = True
            gv.Columns("BO_ItemCode").Width = 100
            gv.Columns("BO_ItemCode").HeaderText = "BOItemCode"

            gv.Columns("BODescription").IsVisible = True
            gv.Columns("BODescription").Width = 100
            gv.Columns("BODescription").HeaderText = "BOItemDescription"

            gv.Columns("RECEIPT_CODE").IsVisible = True
            gv.Columns("RECEIPT_CODE").Width = 100
            gv.Columns("RECEIPT_CODE").HeaderText = "Receipt Code"

            gv.Columns("RECEIPT_DATE").IsVisible = True
            gv.Columns("RECEIPT_DATE").Width = 120
            gv.Columns("RECEIPT_DATE").HeaderText = "Receipt Date"

            gv.Columns("Receipt_ItemCode").IsVisible = True
            gv.Columns("Receipt_ItemCode").Width = 100
            gv.Columns("Receipt_ItemCode").HeaderText = "ReceiptItemCode"

            gv.Columns("Receipt_ItemDESCRIPTION").IsVisible = True
            gv.Columns("Receipt_ItemDESCRIPTION").Width = 120
            gv.Columns("Receipt_ItemDESCRIPTION").HeaderText = "ReceiptItemDesc"

            gv.Columns("BATCH_QTY").IsVisible = True
            gv.Columns("BATCH_QTY").Width = 100
            gv.Columns("BATCH_QTY").HeaderText = "BOQty"

            gv.Columns("RECEIPT_QTY").IsVisible = True
            gv.Columns("RECEIPT_QTY").Width = 80
            gv.Columns("RECEIPT_QTY").HeaderText = "ReceiptQty"

            gv.Columns("Difference").IsVisible = True
            gv.Columns("Difference").Width = 80
            gv.Columns("Difference").HeaderText = "Qty"


        ElseIf rdbSummary.IsChecked Then
            gv.Columns("BO_CODE").IsVisible = True
            gv.Columns("BO_CODE").Width = 100
            gv.Columns("BO_CODE").HeaderText = "BO Code"

            gv.Columns("BO_DATE").IsVisible = True
            gv.Columns("BO_DATE").Width = 100
            gv.Columns("BO_DATE").HeaderText = "BO Date"

      
            gv.Columns("RECEIPT_CODE").IsVisible = True
            gv.Columns("RECEIPT_CODE").Width = 100
            gv.Columns("RECEIPT_CODE").HeaderText = "Receipt Code"

            gv.Columns("RECEIPT_DATE").IsVisible = True
            gv.Columns("RECEIPT_DATE").Width = 120
            gv.Columns("RECEIPT_DATE").HeaderText = "Receipt Date"

        
            gv.Columns("BATCH_QTY").IsVisible = True
            gv.Columns("BATCH_QTY").Width = 80
            gv.Columns("BATCH_QTY").HeaderText = "BOQty"

            gv.Columns("RECEIPT_QTY").IsVisible = True
            gv.Columns("RECEIPT_QTY").Width = 80
            gv.Columns("RECEIPT_QTY").HeaderText = "ReceiptQty"

         

        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item4 As New GridViewSummaryItem("BATCH_QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("RECEIPT_QTY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        If rdbDetail.IsChecked Then
            Dim item6 As New GridViewSummaryItem("Difference", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
        End If

        'gv.GroupDescriptors.Add(New GridGroupByExpression("Item_Code as Item format ""{0}: {1}"" Group By Item_Code"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("Item_Desc as Item format ""{0}: {1}"" Group By Item_Desc"))
        'gv.ShowGroupPanel = False
        'gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Sub Reset()
        LoadBO()
        LoadItem()
        LoadReceipt()
        'fnduom.Value = ""
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        'cboStatus.DataSource = GetCboStatusDataTable()
        'cboStatus.ValueMember = "RESOURCE_CODE"
        'cboStatus.DisplayMember = "Value"
        'cboStatus.SelectedIndex = 0
        gv.DataSource = Nothing
        rdbSummary.IsChecked = True
    End Sub
  
#End Region
#Region "Finders"

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

    Private Sub RptListOfTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+S for Print ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        SetUserMgmtNew()
    End Sub

    'Private Sub RptListOfTools_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    '    If e.Alt AndAlso e.KeyCode = Keys.P Then
    '        Print()
    '    ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
    '        Me.Close()
    '    ElseIf e.Alt And e.KeyCode = Keys.N Then
    '        Reset()
    '    End If
    'End Sub

    'Private Sub chkToolAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkToolAll.ToggleStateChanged
    '    cbgTool.Enabled = chkToolSelect.IsChecked
    'End Sub

    'Private Sub chkToolMAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkToolMAll.ToggleStateChanged
    '    cbgtoolM.Enabled = chkToolMSelect.IsChecked
    'End Sub

    'Private Sub ChksupplierAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChksupplierAll.ToggleStateChanged
    '    cbgSupplier.Enabled = ChksupplierSelect.IsChecked
    'End Sub

   
    Private Sub chkBOAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBOAll.ToggleStateChanged
        cbgBO.Enabled = chkBOSelect.IsChecked
    End Sub

    Private Sub chkReceiptAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkReceiptAll.ToggleStateChanged
        cbgReceipt.Enabled = chkReceiptSelect.IsChecked
    End Sub
End Class
