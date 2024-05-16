
'''''''''''''' New Sales Analysis Report added by Panch Raj
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Imports System.Threading
'Imports Telerik.WinControls.UI.Export6
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO

Public Class rptDistributerPerformance
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery, strType As String
    'Dim dr As SqlDataReader
    Dim ArrDBName As ArrayList = Nothing
    Dim strLocation As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()

            Next
        End If
    End Sub
    Sub LoadMonth()
        Dim qry As String
        Dim from_date As Date
        Dim to_Date As Date
        qry = "select coalesce(MIN(document_date), cast('1'+ '-'+ datename(month,current_timestamp)+'-'+ cast(year(current_timestamp) as varchar(4))as date)) as fromDate from TSPL_SD_SALE_INVOICE_HEAD"
        from_date = clsDBFuncationality.getSingleValue(qry)
        qry = "select coalesce(MAX(document_date), cast('1'+ '-'+ datename(month,current_timestamp)+'-'+ cast(year(current_timestamp) as varchar(4))as date)) as fromDate from TSPL_SD_SALE_INVOICE_HEAD"
        to_Date = clsDBFuncationality.getSingleValue(qry)
        If from_date >= to_Date Then
            to_Date = from_date.AddMonths(1)
        End If
        qry = " declare @StartDate datetime='" & clsCommon.GetPrintDate(from_date, "dd/MMM/yyyy") & "' declare @EndDate datetime= '" & clsCommon.GetPrintDate(to_Date, "dd/MMM/yyyy") & "' " & _
              " declare @temp  table ( TheDate DateTime  ) " & _
              " while (@StartDate<=@EndDate) begin insert into @temp values (@StartDate ) select @StartDate=DATEADD(MONTH,1,@StartDate) End    " & _
              " SELECT (CAST(DATENAME(MONTH,TheDate) AS VARCHAR(3))+ '-' + CAST(year(TheDate) AS VARCHAR(4)) ) AS MONTH_YEAR,MONTH(TheDate) AS MONTH_NO FROM @temp"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        Me.cboMonth.DataSource = dt
        Me.cboMonth.ValueMember = "MONTH_YEAR"
        Me.cboMonth.DisplayMember = "MONTH_YEAR"
        If cboMonth.Items.Count > 0 Then
            Me.cboMonth.SelectedIndex = -1
            Me.cboMonth.SelectedIndex = cboMonth.Items.Count - 1
        End If
    End Sub
    Sub LoadLocation()
        Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub

    Sub LoadCustomer()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Cust_Code,Customer_Name,IsDistributor as [Is Distributor] from TSPL_CUSTOMER_MASTER")
        chkCustomer.DataSource = dt
        chkCustomer.ValueMember = "Cust_Code"
        chkCustomer.DisplayMember = "Customer_Name"
    End Sub


    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub rptDistributerPerformance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        dtpFdate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        LoadLocation()
        LoadMonth()
        chkLocAll.IsChecked = True
        LoadCustomer()
        chkClassAll.IsChecked = True
        Me.Text = "Distributer Performance"
        RadGroupBox1.Visible = True
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptDistributerPerformance)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub
    Sub Reset()
        cboMonth.SelectedIndex = -1
        dtpFdate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()

        LoadLocation()
        chkLocAll.IsChecked = True
        LoadCustomer()

    End Sub

    Sub print()
        Try
            Dim dt As DataTable
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Location or select ALL", Me.Text)
                Return
            ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Customer or select ALL", Me.Text)
                Return
            End If
            Dim from_date As Date
            Dim to_Date As Date
            from_date = Me.dtpFdate.Value
            to_Date = dtpToDate.Value

            'Dim strLoca As String = ""
            'Dim strClass As String = ""
            'If chkLocSelect.IsChecked Then
            '    For Each Str As String In cbgLocation.CheckedDisplayMember
            '        If clsCommon.myLen(strLoca) > 0 Then
            '            strLoca += ", "
            '        End If
            '        strLoca += Str
            '    Next
            'End If
            Dim strCust As String = ""
            If chkClassSelect.IsChecked Then
                For Each Str As String In chkCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strCust) > 0 Then
                        strCust += ", "
                    End If
                    strCust += Str
                Next
            End If
            Dim strCustCond As String = ""
            If clsCommon.myLen(strCust) <= 0 Then
                strCustCond = ""
            Else
                strCustCond = "where Cust_Code in (" & clsCommon.GetMulcallString(chkCustomer.CheckedValue) & ")"
            End If
            '''''   Main qyery
            Dim qry As String
            Dim pivotCols As String = "("
            'select @StartDate= @StartDate-(DATEPART(DD,@StartDate)-1)
            qry = " declare @StartDate datetime='" & clsCommon.GetPrintDate(from_date, "dd/MMM/yyyy") & "' declare @EndDate datetime= '" & clsCommon.GetPrintDate(to_Date, "dd/MMM/yyyy") & "' " & _
                  "  declare @temp  table ( TheDate DateTime  ) " & _
                  " while (@StartDate<=@EndDate) begin insert into @temp values (@StartDate ) select @StartDate=DATEADD(DD,1,@StartDate) End " & _
                  " SELECT (CAST(DAY(TheDate) AS VARCHAR(3)) + '-' + CAST(DATENAME(MONTH,TheDate) AS VARCHAR(3))) AS DAY_MONTH FROM @temp"
            dt = clsDBFuncationality.GetDataTable(qry)
            For Each dr As DataRow In dt.Rows
                If dt.Rows.IndexOf(dr) = 0 Then
                    pivotCols = pivotCols & "[" & dr.Item("DAY_MONTH") & "]"
                Else
                    pivotCols = pivotCols & "," & "[" & dr.Item("DAY_MONTH") & "]"
                End If

            Next
            pivotCols = pivotCols & ")"

            qry = " declare @StartDate datetime='" & clsCommon.GetPrintDate(from_date, "dd/MMM/yyyy") & "' declare @EndDate datetime=  '" & clsCommon.GetPrintDate(to_Date, "dd/MMM/yyyy") & "' " & _
                  " declare @temp  table ( TheDate DateTime  ) " & _
                  " while (@StartDate<=@EndDate) begin insert into @temp values (@StartDate ) select @StartDate=DATEADD(DD,1,@StartDate) End " & _
                  " SELECT * FROM (select cust_date.Cust_Code,(CAST(DAY(cust_date.doc_date) AS VARCHAR(3)) + '-' + CAST(DATENAME(MONTH,CUST_DATE.DOC_DATE) AS VARCHAR(3))) AS DAY_MONTH , " & _
                  " cust_date.Customer_Name,cust_date.Route_No,cust_date.Route_Desc,cust_date.Route_Group, " & _
                  " coalesce(cust_sale.totalQty,0) as totalQty  " & _
                  " from ( " & _
                  " select convert(date,TheDate,108) as doc_date,customer.* from @temp, " & _
                  " (select Cust_Code,Customer_Name,TSPL_CUSTOMER_MASTER.Route_No,Route_Group,TSPL_CUSTOMER_MASTER.Route_Desc from TSPL_CUSTOMER_MASTER " & strCustCond & " ) as customer) as cust_date " & _
                  " left join ( " & _
                  " select Cust_Code,convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,108) as Document_Date ,sum(TSPL_SD_SALE_INVOICE_DETAIL.totalQty) as totalQty from TSPL_CUSTOMER_MASTER " & _
                  " left join (select * from TSPL_SD_SALE_INVOICE_HEAD where TSPL_SD_SALE_INVOICE_HEAD.Document_Date between '" & clsCommon.GetPrintDate(from_date, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(to_Date.AddDays(1), "dd/MMM/yyyy") & "') as TSPL_SD_SALE_INVOICE_HEAD  on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & _
                  " left join (select Document_Code,SUM(qty) as totalQty from   TSPL_SD_SALE_INVOICE_DETAIL where FOC_Item=0 group by Document_Code) as TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
                  " group by Cust_Code,convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,108)) as cust_sale on cust_date.Cust_Code=cust_sale.Cust_Code " & _
                  " and cust_date.doc_date=cust_sale.Document_Date ) as final " & _
                  " Pivot " & _
                  "  ( " & _
                  " SUM(totalQty) " & _
                  " FOR [DAY_MONTH] IN " & pivotCols & " " & _
                  " )AS pivot1 "

            dt = clsDBFuncationality.GetDataTable(qry)

            dt = addWeeklyCols(dt, 5)
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                gv1.DataSource = dt
                SetGridFormationOFGV1()
                ReStoreGridLayout()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Function addWeeklyCols(ByVal dt As DataTable, ByVal startCol As Integer) As DataTable
        Dim weeklyTotal As Decimal = 0
        Dim weekName As String = ""
        Dim total As Decimal = 0
        Dim WeekTotal As Decimal = 0
        Dim dtFinal As DataTable
        dtFinal = dt.Copy()

        For Each dr As DataRow In dt.Rows
            total = 0
            WeekTotal = 0
            For Each dcl As DataColumn In dt.Columns
                If (dt.Columns.IndexOf(dcl.ColumnName) + 1) <= startCol Then
                    Continue For
                End If
                If ((dt.Columns.IndexOf(dcl.ColumnName) + 1) - startCol) Mod 7 = 1 Then
                    weeklyTotal = dr.Item(dcl.ColumnName)
                    weekName = Val(dcl.ColumnName) & "-" & (IIf(Val(dcl.ColumnName) = 29, Val(dt.Columns(dt.Columns.Count - 1).ColumnName), Val(dcl.ColumnName) + 6))
                    If dt.Rows.IndexOf(dr) <= 0 Then
                        dtFinal.Columns.Add(weekName)
                    End If

                    'weeklyTotal = weeklyTotal + dr.Item(dcl.ColumnName)
                    dtFinal.Rows(dt.Rows.IndexOf(dr)).Item(weekName) = weeklyTotal

                Else
                    weeklyTotal = weeklyTotal + dr.Item(dcl.ColumnName)
                    dtFinal.Rows(dt.Rows.IndexOf(dr)).Item(weekName) = weeklyTotal

                End If
                WeekTotal = WeekTotal + dr.Item(dcl.ColumnName)
            Next
            'total = total + WeekTotal
            If dt.Rows.IndexOf(dr) <= 0 Then
                dtFinal.Columns.Add("Total")
            End If

            dtFinal.Rows(dt.Rows.IndexOf(dr)).Item("Total") = WeekTotal
        Next
        dtFinal.AcceptChanges()
        Return dtFinal
    End Function




    Sub SetGridFormationOFGV1()
        'Dim strItemCode, head2 As String

        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False

        gv1.Columns("Cust_Code").HeaderText = "Customer Code"
        gv1.Columns("Customer_Name").HeaderText = "Customer Name"
        gv1.Columns("Route_No").HeaderText = "Route No"
        gv1.Columns("Route_Group").HeaderText = "Route Group"
        gv1.Columns("Route_Desc").HeaderText = "Route Description"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim intCount As Integer = 0
        'Dim item1 As New GridViewSummaryItem("Total :", "{0:F2}", GridAggregateFunction.None)
        'summaryRowItem.Add(item1)
        For intloopcol As Integer = 5 To Me.gv1.Columns.Count - 1
            Dim item As New GridViewSummaryItem(gv1.Columns(intloopcol).Name, "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
        Next

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        RadPageView1.SelectedPage = RadPageViewPage2
        gv1.BestFitColumns()
    End Sub

    Private Sub ExportToExcel()
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)


            clsCommon.MyExportToExcel("Distributor Performance", gv1, arrHeader, Me.Text)


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    '''' <summary>        
    '''' '''' using ExcelCellFormatting event for updating progress bar and applying custom format in excel file        
    '''' '''' </summary>        

    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewDataRowInfo) Then
        End If
    End Sub

    '''' <summary>        
    '''' '''' using ExcelTableCreated event for adding custom header row        
    '''' '''' </summary>        

    Private Sub exporter_ExcelTableCreated(ByVal sender As Object, ByVal e As ExcelTableCreatedEventArgs)
        'Dim strReportTitle, strConverted, strOrderby, head2, strSummary, strQty, strType As String



        If e.SheetIndex = 0 Then 'add header row only for the first excel sheet                

            Dim style1 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Sale Discount Report ")
            Dim style2 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Start Date : = " + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy"))
            Dim style3 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "End Date : = " + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy"))

            If chkLocSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
                If strLoca = "" Then
                    strLoca = "All"
                End If
                Dim style4 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Location : " + strLoca)
            End If

            If chkClassSelect.IsChecked Then
                Dim strClass As String = ""
                For Each Str As String In chkCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strClass) > 0 Then
                        strClass += ", "
                    End If
                    strClass += Str
                Next
                If strClass = "" Then
                    strClass = "All"
                End If
                Dim style5 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Customer Type : " + strClass)

            End If

            'If chkHierSelect.IsChecked Then
            '    Dim strHier As String = ""
            '    For Each Str As String In 
            '.CheckedDisplayMember()
            '        If clsCommon.myLen(strHier) > 0 Then
            '            strHier += ", "
            '        End If
            '        strHier += Str
            '    Next
            '    If strHier = "" Then
            '        strHier = "All"
            '    End If
            '    Dim style5 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Hierarchy : " + strHier)

            'End If
        End If
    End Sub

    Sub Export()
        If gv1.Rows.Count > 0 Then
            ExportToExcel()
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Export()
    End Sub

    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkClassAll.ToggleStateChanged
        chkCustomer.Enabled = Not chkClassAll.IsChecked
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub


    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        gv1.EnableFiltering = True
        print()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub rdbDocSummary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)

    End Sub



    Private Sub btSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveLayout.Click
        If clsCommon.myLen(GetReportID()) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = GetReportID()
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Public Function GetReportID() As String

        Return "rptDistributorPerfo"

    End Function

    Private Sub btnDeleteLayour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteLayour.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(GetReportID()) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(GetReportID(), "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub rdbWoFOC_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)

    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv1.CellDoubleClick
        'If rdbDocSummary.IsChecked Then
        '    If gv1.Rows.Count > 0 Then
        '        Dim strTransType As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Type").Value)
        '        Dim strDoc = gv1.CurrentRow.Cells("Sale_Invoice_No").Value
        '        If strTransType = "Sale Invoice" Then
        '            strTransType = "SD-IN"
        '        Else
        '            strTransType = "Sale Return"
        '        End If
        '        Select Case strTransType
        '            Case "SD-IN"
        '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, strDoc)
        '            Case "Sale Return"
        '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleReturn, strDoc)
        '        End Select
        '    End If
        'End If
    End Sub


    Private Sub cbgLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbgLocation.Load

    End Sub

    Private Sub cboMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboMonth.SelectedIndexChanged
        Try
            If cboMonth.SelectedIndex < 0 Then
                Exit Sub
            End If
            displayFromToDates(Me.cboMonth.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub
    Sub displayFromToDates(ByVal monthCode As String)
        dtpFdate.Value = CDate("1" & "-" & monthCode)
        dtpToDate.Value = dtpFdate.Value.AddMonths(1).AddDays(-1)

    End Sub
End Class
