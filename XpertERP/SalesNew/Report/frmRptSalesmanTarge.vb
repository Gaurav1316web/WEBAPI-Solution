'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
Imports common
Imports System.IO

Public Class frmRptSalesmanTarge
    Inherits FrmMainTranScreen
#Region "Variables"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptSalesmanTarget)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        LoadSalesman()
        cbgSalesman.CheckedAll()
        LoadTargetType()



        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnRefresh, "Press Alt+R Adding New Trasnaction")


    End Sub

    Sub LoadTargetType()
        Dim dtType As New DataTable
        dtType.Columns.Add("Code", GetType(String))
        dtType.Columns.Add("Desc", GetType(String))
        dtType.Rows.Add("I", "Item Wise")
        dtType.Rows.Add("A", "Amount Wise")
        cboTargetType.DataSource = dtType
        cboTargetType.ValueMember = "Code"
        cboTargetType.DisplayMember = "Desc"
    End Sub

    Private Sub frmRptTrialBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            RefreshData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Sub LoadSalesman()
        Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name from TSPL_EMPLOYEE_MASTER where Emp_type='Salesman'"
        cbgSalesman.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSalesman.ValueMember = "Code"
        cbgSalesman.DisplayMember = "Name"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        EnableDisableControls(True)

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            RefreshData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub RefreshData()
        If cbgSalesman.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one Salesman")
        End If


        gv1.EnableFiltering = True
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Dim FinalQty As String = ""

        Dim dtFrom As Date = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
        Dim dtTo As Date = New Date(txtToDate.Value.Year, txtToDate.Value.Month + 1, 1)
        dtTo = dtTo.AddDays(-1)

        Dim FromDate As String = clsCommon.GetPrintDate(dtFrom, "dd/MMM/yyyy")
        Dim ToDate As String = clsCommon.GetPrintDate(dtTo, "dd/MMM/yyyy")
        Dim Qry As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(cboTargetType.SelectedValue), "A") = CompairStringResult.Equal Then
            Qry = "select Code,Salesman_Code,SalesmanName,MonthYear,REPLACE( SUBSTRING(CONVERT(varchar(11), MonthYear,106),4,9),' ','-') as MonthYearView,TargetAmt,AchivedAmt,(TargetAmt-AchivedAmt) as  VarianceAmt from ("
            Qry += " select TSPL_SD_SALESMAN_TARGET_HEADER.Code,TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code,TSPL_EMPLOYEE_MASTER.Emp_Name as  SalesmanName,  TSPL_SD_SALESMAN_TARGET_HEADER.MonthYear ,TSPL_SD_SALESMAN_TARGET_HEADER.Amount as TargetAmt,(select isnull(SUM(TSPL_SD_SALE_INVOICE_HEAD.Amount_Less_Discount),0)   from  TSPL_SD_SALE_INVOICE_HEAD  where TSPL_SD_SALE_INVOICE_HEAD.Status='1' and TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code=TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code and DATEPART(YYYY,TSPL_SD_SALE_INVOICE_HEAD.Document_Date)= DATEPART(YYYY,TSPL_SD_SALESMAN_TARGET_HEADER.MonthYear) and DATEPART(MM,TSPL_SD_SALE_INVOICE_HEAD.Document_Date)= DATEPART(MM,TSPL_SD_SALESMAN_TARGET_HEADER.MonthYear) ) as AchivedAmt"
            Qry += " from  TSPL_SD_SALESMAN_TARGET_HEADER "
            Qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code"
            Qry += " where TSPL_SD_SALESMAN_TARGET_HEADER.MonthYear>='" + FromDate + "' and TSPL_SD_SALESMAN_TARGET_HEADER.MonthYear<='" + ToDate + "'"
            Qry += " and TSPL_SD_SALESMAN_TARGET_HEADER.Target_Type='A'"
            Qry += " and TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesman.CheckedValue) + ")"
            Qry += " )xxx order by MonthYear,Salesman_Code"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTargetType.SelectedValue), "I") = CompairStringResult.Equal Then
            Qry = "select Code,Salesman_Code,SalesmanName,MonthYear,REPLACE( SUBSTRING(CONVERT(varchar(11), MonthYear,106),4,9),' ','-') as MonthYearView,Item_Code,Item_Desc,TargetQty,AchivedQty,(TargetQty-AchivedQty) as VarianceQty,TargetAmt,AchivedAmt,(TargetAmt-AchivedAmt) as  VarianceAmt"
            Qry += " from ("
            Qry += " select TSPL_SD_SALESMAN_TARGET_HEADER.Code,TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code,TSPL_EMPLOYEE_MASTER.Emp_Name as  SalesmanName,  TSPL_SD_SALESMAN_TARGET_HEADER.MonthYear,TSPL_SD_SALESMAN_TARGET_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SALESMAN_TARGET_DETAIL.Qty as TargetQty,"
            Qry += " (select isnull(SUM(TSPL_SD_SALE_INVOICE_DETAIL.Qty),0) from TSPL_SD_SALE_INVOICE_DETAIL "
            Qry += " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code"
            Qry += " where TSPL_SD_SALE_INVOICE_HEAD.Status='1' and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_SD_SALESMAN_TARGET_DETAIL.Item_Code and DATEPART(YYYY,TSPL_SD_SALE_INVOICE_HEAD.Document_Date)= DATEPART(YYYY,TSPL_SD_SALESMAN_TARGET_HEADER.MonthYear) and DATEPART(MM,TSPL_SD_SALE_INVOICE_HEAD.Document_Date)= DATEPART(MM,TSPL_SD_SALESMAN_TARGET_HEADER.MonthYear) and TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code=TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code) as AchivedQty,TSPL_SD_SALESMAN_TARGET_DETAIL.Amount as TargetAmt,(select isnull(SUM(TSPL_SD_SALE_INVOICE_DETAIL.Amt_Less_Discount),0)   from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code"
            Qry += " where TSPL_SD_SALE_INVOICE_HEAD.Status='1' and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_SD_SALESMAN_TARGET_DETAIL.Item_Code and DATEPART(YYYY,TSPL_SD_SALE_INVOICE_HEAD.Document_Date)= DATEPART(YYYY,TSPL_SD_SALESMAN_TARGET_HEADER.MonthYear) and DATEPART(MM,TSPL_SD_SALE_INVOICE_HEAD.Document_Date)= DATEPART(MM,TSPL_SD_SALESMAN_TARGET_HEADER.MonthYear) and TSPL_SD_SALE_INVOICE_HEAD.Salesman_Code=TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code) as AchivedAmt"
            Qry += " from TSPL_SD_SALESMAN_TARGET_DETAIL"
            Qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALESMAN_TARGET_DETAIL.Item_Code"
            Qry += " left outer join TSPL_SD_SALESMAN_TARGET_HEADER on TSPL_SD_SALESMAN_TARGET_HEADER.Code=TSPL_SD_SALESMAN_TARGET_DETAIL.Code"
            Qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code"
            Qry += " where TSPL_SD_SALESMAN_TARGET_HEADER.MonthYear>='" + FromDate + "' and TSPL_SD_SALESMAN_TARGET_HEADER.MonthYear<='" + ToDate + "'"
            Qry += " and TSPL_SD_SALESMAN_TARGET_HEADER.Target_Type='I'"
            Qry += " and TSPL_SD_SALESMAN_TARGET_HEADER.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesman.CheckedValue) + ")"
            Qry += " )xxx"
            Qry += " order by MonthYear,Salesman_Code,Item_Code"
        Else
            Throw New Exception("Please select Valid Target Type")

        End If

        dt = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count <= 0 Then
            gv1.DataSource = Nothing
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Exit Sub
        End If
        SetGridFormation()
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Sub SetGridFormation()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.DataSource = dt

        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next


        gv1.Columns("Salesman_Code").IsVisible = True
        gv1.Columns("Salesman_Code").Width = 70
        gv1.Columns("Salesman_Code").HeaderText = "Saleman Code"

        gv1.Columns("SalesmanName").IsVisible = True
        gv1.Columns("SalesmanName").Width = 100
        gv1.Columns("SalesmanName").HeaderText = "Salesman"

        gv1.Columns("MonthYearView").IsVisible = True
        gv1.Columns("MonthYearView").Width = 70
        gv1.Columns("MonthYearView").HeaderText = "Month Year"

        gv1.Columns("TargetAmt").IsVisible = True
        gv1.Columns("TargetAmt").Width = 100
        gv1.Columns("TargetAmt").HeaderText = "Target Amount"
        gv1.Columns("TargetAmt").FormatString = "{0:F2}"

        gv1.Columns("AchivedAmt").IsVisible = True
        gv1.Columns("AchivedAmt").Width = 100
        gv1.Columns("AchivedAmt").HeaderText = "Achieved Amount"
        gv1.Columns("AchivedAmt").FormatString = "{0:F2}"

        gv1.Columns("VarianceAmt").IsVisible = True
        gv1.Columns("VarianceAmt").Width = 100
        gv1.Columns("VarianceAmt").HeaderText = "Variance Amount"
        gv1.Columns("VarianceAmt").FormatString = "{0:F2}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        If clsCommon.CompairString(clsCommon.myCstr(cboTargetType.SelectedValue), "A") = CompairStringResult.Equal Then
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTargetType.SelectedValue), "I") = CompairStringResult.Equal Then
            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 100
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 100
            gv1.Columns("Item_Desc").HeaderText = "Item"

            gv1.Columns("TargetQty").IsVisible = True
            gv1.Columns("TargetQty").Width = 100
            gv1.Columns("TargetQty").HeaderText = "Target Qty"
            gv1.Columns("TargetQty").FormatString = "{0:F2}"

            gv1.Columns("AchivedQty").IsVisible = True
            gv1.Columns("AchivedQty").Width = 100
            gv1.Columns("AchivedQty").HeaderText = "Achived Qty"
            gv1.Columns("AchivedQty").FormatString = "{0:F2}"

            gv1.Columns("VarianceQty").IsVisible = True
            gv1.Columns("VarianceQty").Width = 100
            gv1.Columns("VarianceQty").HeaderText = "Achieved Qty"
            gv1.Columns("VarianceQty").FormatString = "{0:F2}"

            Dim item4 As New GridViewSummaryItem("TargetQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)

            Dim item5 As New GridViewSummaryItem("AchivedQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)

            Dim item6 As New GridViewSummaryItem("VarianceQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
        End If



        Dim item1 As New GridViewSummaryItem("TargetAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("VarianceAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("AchivedAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        EnableDisableControls(False)
    End Sub

    Private Function EnableDisableControls(ByVal Val As Boolean)
        grpLocaSegment.Enabled = Val
        txtFromDate.Enabled = Val
        txtToDate.Enabled = Val
        cboTargetType.Enabled = Val
        Return True
    End Function

    Private Sub FunExportToExcel(ByVal exporter As EnumExportTo)
        Try
            RefreshData()
            If dt.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim strTemp As String = ""
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, " MM-yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "MM-yyyy"))

                strTemp = ""
                For Each Str As String In cbgSalesman.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Salesman : " + strTemp)

                strTemp = IIf(clsCommon.CompairString(clsCommon.myCstr(cboTargetType.SelectedValue), "A") = CompairStringResult.Equal, "Amount", "Item")
                arrHeader.Add("Target Type : " + strTemp)

                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcel("Salesman Target Status", gv1, arrHeader, Me.Text)
                ElseIf exporter = EnumExportTo.PDF Then
                    clsCommon.MyExportToPDF("Salesman Target Status", gv1, arrHeader, Me.Text, True)
                End If

            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveLayout.Click
        If clsCommon.myLen(GetReportID()) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = GetReportID()
            obj.UserID = objCommonVar.CurrentUser
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUser
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount

            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Function GetReportID() As String
        Dim str As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(cboTargetType.SelectedValue), "A") = CompairStringResult.Equal Then
            str = "TargetBalanceA"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTargetType.SelectedValue), "I") = CompairStringResult.Equal Then
            str = "TargetBalanceI"
        End If
        Return str
    End Function

    Private Sub btnRestoreLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestoreLayout.Click
        Try
            If clsCommon.myLen(GetReportID()) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(GetReportID(), "", objCommonVar.CurrentUser), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try

    End Sub

    Private Sub btnDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteLayout.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUser)
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        FunExportToExcel(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        FunExportToExcel(EnumExportTo.PDF)
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If gv1.Rows.Count > 0 Then
            Dim strDoc
            strDoc = gv1.CurrentRow.Cells("Code").Value
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesmanTarget, strDoc)
        End If
    End Sub
End Class
