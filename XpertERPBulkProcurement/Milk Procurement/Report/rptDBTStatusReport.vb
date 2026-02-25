Imports common
Public Class rptDBTStatusReport

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub rptDBTStatusReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = txtFromDate.Value.AddMonths(1)
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        EnableDisableFields(True)
        BlankGrid()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub BlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.MasterView.Refresh()
    End Sub

    Sub EnableDisableFields(ByVal isBool As Boolean)
        RadGroupBox1.Enabled = isBool
        btngo.Enabled = isBool
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Try
            LoadDataOrPrintData(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadDataOrPrintData(ByVal isPrint As Boolean)
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found !", Me.Text)
                Exit Sub
            End If

            Dim arrUnion As New ArrayList()
            arrUnion.Add(objCommonVar.CurrComp_Code1)
            If objCommonVar.RCDFCFP Then
                dt = clsMilkUnion.UnionDBName()
            Else
                dt = clsMilkUnion.UnionDBName1(arrUnion)
            End If
            Dim query As String = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                query = " Select [Union],[From Date],SUM(Amount)Amount,Max([Union Status])[Union Status],Max([RCDF Status])[RCDF Status] from ("
                Dim i As Integer = 0
                For Each strUnion In dt.Rows
                    If i <> 0 Then
                        query &= " Union All "
                    End If
                    query &= "Select TSPL_DBT_NEFT.Document_Code, '" & clsCommon.myCstr(strUnion("DataBase_Name")) & "' As [Union],TSPL_DBT_NEFT.Document_Date As [Document Date],FORMAT(TSPL_DBT_NEFT.From_Date, 'MMM-yyyy') As [From Date], TSPL_DBT_NEFT_DETAIL.Amount,
Case When TSPL_DBT_NEFT.Status=1 Then 'Approved' Else 'Pending' End As [Union Status],
Case When TSPL_DBT_NEFT.RCDF_Status=1 Then 'Approved' Else 'Pending' End As [RCDF Status] 
from " & clsCommon.myCstr(strUnion("DataBase_Name")) & ".dbo.TSPL_DBT_NEFT_DETAIL
Left Outer Join " & clsCommon.myCstr(strUnion("DataBase_Name")) & ".dbo.TSPL_DBT_NEFT On TSPL_DBT_NEFT.Document_Code=TSPL_DBT_NEFT_DETAIL.Document_Code
Where TSPL_DBT_NEFT.Document_Date>='" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' And TSPL_DBT_NEFT.Document_Date<='" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "'"
                    i += 1
                Next
                query &= " )BaseQry Group By [Union],[Document Date],[From Date] Order By [Union],[Document Date],[From Date]"
                dt = Nothing
                dt = clsDBFuncationality.GetDataTable(query)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    BlankGrid()
                    gv1.DataSource = dt
                    For ii As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Columns(ii).ReadOnly = True
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    SetGridFormat()
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    EnableDisableFields(False)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
                    Exit Sub
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Union not found !", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub SetGridFormat()
        Try
            gv1.AutoExpandGroups = False
            gv1.ShowGroupPanel = False
            gv1.ShowRowHeaderColumn = False
            gv1.AllowAddNewRow = False
            gv1.AllowDeleteRow = False
            gv1.EnableFiltering = True
            gv1.ShowFilteringRow = True
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).BestFit()
            Next
            gv1.ShowGroupPanel = True
            gv1.MasterTemplate.AutoExpandGroups = False
            Dim summaryRowItem As New GridViewSummaryRowItem()
            summaryRowItem.Add(New GridViewSummaryItem("Amount", "{0:n2}", GridAggregateFunction.Sum))
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        ExportExcel()
    End Sub

    Sub ExportExcel()
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDBTStatusReport & "'"))

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Name : " & strHeading)
            arrHeader.Add("Date Range from : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") & " To " & clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            'transportSql.exportdata(gv1, "", Me.Text, False, arrHeader, False, False, True)
            clsCommon.MyExportToExcel(Nothing, gv1, arrHeader, strHeading)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        ExportPDF()
    End Sub

    Sub ExportPDF()
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "  To " & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy"))
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class