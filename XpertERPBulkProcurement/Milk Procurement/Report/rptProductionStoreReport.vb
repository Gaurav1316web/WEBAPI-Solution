Imports common
Imports System.IO

Public Class rptProductionStoreReport
    Inherits FrmMainTranScreen

#Region "Variables"

    Const ReportID As String = "ProductionStoreReport"

#End Region
    Private Sub rptProductionStoreReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
    End Sub

    Sub funreset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        Dim ToDate As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        Dim FromDate As DateTime = ToDate.AddDays(-30)
        txtFromDate.Value = FromDate
        txtToDate.Value = ToDate
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Dim qry As String = ""
            Dim Baseqry As String = ""
            Dim dtFinal As DataTable = New DataTable()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found", Me.Text)
                gv1.DataSource = Nothing
                Exit Sub
            End If
            Dim arrUnion As New ArrayList()
            arrUnion.Add(objCommonVar.CurrComp_Code1)
            If objCommonVar.RCDFCFP Then
                dt = clsMilkUnion.UnionDBName()
            Else
                dt = clsMilkUnion.UnionDBName1(arrUnion)
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        Baseqry += " UNION ALL "
                    End If
                    Baseqry += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name], 
                                 sum([No Of PurchaseOrder])[No Of PurchaseOrder],sum([No Of GRN])[No Of GRN],sum([No Of SRN])[No Of SRN],
                                 sum([No Of PurchaseInvoice])[No Of PurchaseInvoice],sum([No Of StoreRequisition])[No Of StoreRequisition],
                                 sum([No Of StoreIssue])[No Of StoreIssue] from 
                                (Select count(PurchaseOrder_No)[No Of PurchaseOrder],0 AS [No Of GRN],0 AS [No Of SRN],0 AS [No Of PurchaseInvoice],
                                0 as [No Of StoreRequisition],0 as [No Of StoreIssue]  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PURCHASE_ORDER_HEAD 
                                WHERE convert(date,PurchaseOrder_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,PurchaseOrder_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'
                        union all
                                Select 0 AS [No Of PurchaseOrder],count(GRN_No)[No Of GRN],0 AS [No Of SRN],0 AS [No Of PurchaseInvoice],0 as [No Of StoreRequisition],
                                0 as [No Of StoreIssue]  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GRN_HEAD  
                                WHERE convert(date,GRN_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,GRN_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'
                       UNION ALL
                                Select 0 AS [No Of PurchaseOrder],0 AS [No Of GRN],COUNT(SRN_No)[No Of SRN],0 AS [No Of PurchaseInvoice],0 as [No Of StoreRequisition],
                                0 as [No Of StoreIssue]  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SRN_HEAD 
                                WHERE convert(date,SRN_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,SRN_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'
                       UNION ALL
                                Select 0 AS [No Of PurchaseOrder],0 AS [No Of GRN],0 AS [No Of SRN],COUNT(PI_No) AS [No Of PurchaseInvoice],0 as [No Of StoreRequisition],
                                0 as [No Of StoreIssue]  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PI_HEAD 
                                WHERE convert(date,PI_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,PI_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'
                       UNION ALL
                                Select 0 AS [No Of PurchaseOrder],0 AS [No Of GRN],0 AS [No Of SRN],0 AS [No Of PurchaseInvoice],Count(Requisition_Id) as [No Of StoreRequisition],
                                0 as [No Of StoreIssue]  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_REQUISITION_HEAD 
                                WHERE convert(date,Requisition_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,Requisition_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'
                       UNION ALL
                                Select 0 AS [No Of PurchaseOrder],0 AS [No Of GRN],0 AS [No Of SRN],0 AS [No Of PurchaseInvoice],0 as [No Of StoreRequisition],
                                Count(Doc_No) as [No Of StoreIssue] from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_IssueReturn_HEAD  
                                WHERE convert(date,Doc_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,Doc_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' )xx"
                    dt = clsDBFuncationality.GetDataTable(Baseqry)
                    dtFinal.Merge(dt)

                Next
            End If

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dtFinal.Rows.Count > 0 Then
                gv1.DataSource = dtFinal
                gv1.BestFitColumns()
                'SetGridFormatgv1(gv1)
                ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer = 0
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
                obj = Nothing
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub

    Sub CancelPressed()
        Me.Close()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Print(EnumExportTo.PDF)
    End Sub

    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptProductionReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate.Value) + "  To " + clsCommon.myCDate(txtToDate.Value))
                arrHeader.Add("User : " + objCommonVar.CurrentUser)
                If exporter = EnumExportTo.Excel Then
                    transportSql.QuickExportToExcel(gv1, Me.Text, "", , arrHeader)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_DoubleClick(sender As Object, e As EventArgs) Handles gv1.DoubleClick
        If clsCommon.CompairString(gv1.CurrentColumn.Name, "Union Name") = CompairStringResult.Equal Then
            UnionWiseProductionDetail(gv1.CurrentCell.Value)
        End If
    End Sub

    Sub UnionWiseProductionDetail(LocName As String)
        Try
            Dim UnionName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select [TSPL_APP_LOCATION].DataBase_Name from [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] 
                              where [TSPL_APP_LOCATION].Location_Name=  '" + LocName + "' "))

            Dim query As String = ""

            query = "  select '" + LocName + "' as [Union Name],convert(varchar(10),xx.[ Document Date],103)[ Document Date], sum([No Of PurchaseOrder])[No Of PurchaseOrder],sum([No Of GRN])[No Of GRN],sum([No Of SRN])[No Of SRN],sum([No Of PurchaseInvoice])[No Of PurchaseInvoice],
                                     sum([No Of StoreRequisition])[No Of StoreRequisition],sum([No Of StoreIssue])[No Of StoreIssue]
                                     from (Select count(PurchaseOrder_No)[No Of PurchaseOrder],0 AS [No Of GRN],0 AS [No Of SRN],0 AS [No Of PurchaseInvoice],
                                     0 as [No Of StoreRequisition],0 as [No Of StoreIssue],Cast(PurchaseOrder_Date as date) as[ Document Date] 
                                     from [" + UnionName + "].[dbo].TSPL_PURCHASE_ORDER_HEAD 
                                     WHERE convert(date,PurchaseOrder_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,PurchaseOrder_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' group by PurchaseOrder_Date
                                union all
                                     Select 0 AS [No Of PurchaseOrder],count(GRN_No)[No Of GRN],0 AS [No Of SRN],0 AS [No Of PurchaseInvoice],0 as [No Of StoreRequisition],
                                     0 as [No Of StoreIssue],Cast(GRN_Date as date) as[ Document Date]  from [" + UnionName + "].[dbo].TSPL_GRN_HEAD  
                                     WHERE convert(date,GRN_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,GRN_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' group by GRN_Date
                                UNION ALL
                                     Select 0 AS [No Of PurchaseOrder],0 AS [No Of GRN],COUNT(SRN_No)[No Of SRN],0 AS [No Of PurchaseInvoice],0 as [No Of StoreRequisition],
                                     0 as [No Of StoreIssue],Cast(SRN_Date as date) as[ Document Date]  from [" + UnionName + "].[dbo].TSPL_SRN_HEAD 
                                     WHERE convert(date,SRN_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,SRN_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' group by SRN_Date
                                UNION ALL
                                     Select 0 AS [No Of PurchaseOrder],0 AS [No Of GRN],0 AS [No Of SRN],COUNT(PI_No) AS [No Of PurchaseInvoice],0 as [No Of StoreRequisition],
                                     0 as [No Of StoreIssue],Cast(PI_Date as date) as[ Document Date]  from [" + UnionName + "].[dbo].TSPL_PI_HEAD 
                                     WHERE convert(date,PI_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,PI_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' group by PI_Date
                                UNION ALL
                                     Select 0 AS [No Of PurchaseOrder],0 AS [No Of GRN],0 AS [No Of SRN],0 AS [No Of PurchaseInvoice],Count(Requisition_Id) as [No Of StoreRequisition],
                                     0 as [No Of StoreIssue],Cast(Requisition_Date as date) as[ Document Date]  from [" + UnionName + "].[dbo].TSPL_REQUISITION_HEAD 
                                     WHERE convert(date,Requisition_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,Requisition_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' group by Requisition_Date
                                UNION ALL
                                     Select 0 AS [No Of PurchaseOrder],0 AS [No Of GRN],0 AS [No Of SRN],0 AS [No Of PurchaseInvoice],0 as [No Of StoreRequisition],
                                     Count(Doc_No) as [No Of StoreIssue],Cast(Doc_Date as date) as[ Document Date] from [" + UnionName + "].[dbo].TSPL_IssueReturn_HEAD 
                                     WHERE convert(date,Doc_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,Doc_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' group by Doc_Date)xx 
                                     group by [ Document Date]"


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gvfrmUnionProductionStoreDetails.DataSource = Nothing
                gvfrmUnionProductionStoreDetails.Rows.Clear()
                gvfrmUnionProductionStoreDetails.Columns.Clear()
                gvfrmUnionProductionStoreDetails.GroupDescriptors.Clear()
                gvfrmUnionProductionStoreDetails.MasterTemplate.SummaryRowsBottom.Clear()
                gvfrmUnionProductionStoreDetails.MasterView.Refresh()
                gvfrmUnionProductionStoreDetails.DataSource = dt

                ' SetGridFormat()
                gvfrmUnionProductionStoreDetails.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage3
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub
End Class