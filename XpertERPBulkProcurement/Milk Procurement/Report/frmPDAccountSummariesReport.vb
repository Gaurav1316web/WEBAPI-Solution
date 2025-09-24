Imports common
Imports System.Text
Imports common.UserControls
Public Class frmPDAccountSummariesReport
    Private Sub frmPDAccountSummariesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromMonth.Value = clsCommon.GETSERVERDATE()
            txtToMonth.Value = txtFromMonth.Value
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function GetStartDateOfMonth(ByVal inputDate As DateTime) As Date
        Try
            Return New Date(inputDate.Year, inputDate.Month, 1)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Public Function GetEndOrCurrentDateOfMonth(ByVal inputDate As DateTime) As Date
        Try
            Dim today As Date = clsCommon.GETSERVERDATE().Today
            If inputDate.Month = today.Month AndAlso inputDate.Year = today.Year Then
                Return today
            Else
                Dim lastDay As Integer = DateTime.DaysInMonth(inputDate.Year, inputDate.Month)
                Return New Date(inputDate.Year, inputDate.Month, lastDay)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        Try
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()
            RadPageView1.SelectedPage = RadPageViewPage1
            EnableDisableFields(True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub EnableDisableFields(ByVal isEnable As Boolean)
        txtFromMonth.Enabled = isEnable
        txtToMonth.Enabled = isEnable
        txtMultUnion.Enabled = isEnable
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtMultUnion__My_Click(sender As Object, e As EventArgs) Handles txtMultUnion._My_Click
        Try
            If chkDataBase() Then
                Dim Qry As String = "SELECT [TSPL_APP_LOCATION].DataBase_Name As [Code], [TSPL_APP_LOCATION].Location_Name As [Union Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1  ORDER BY [TSPL_APP_LOCATION].Location_Name"
                txtMultUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("@Union", Qry, "Code", "Union Name", txtMultUnion.arrValueMember, txtMultUnion.arrDispalyMember)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function chkDataBase() As Boolean
        Try
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Database [TSPL_MASTER] not found !", Me.Text)
                Reset()
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If chkDataBase() Then
                Dim Qry As String = "SELECT [TSPL_APP_LOCATION].DataBase_Name As [Code], [TSPL_APP_LOCATION].Location_Name As [Union Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 "
                If txtMultUnion.arrValueMember IsNot Nothing AndAlso txtMultUnion.arrValueMember.Count > 0 Then
                    Qry &= " And [TSPL_APP_LOCATION].DataBase_Name In (" & clsCommon.GetMulcallString(txtMultUnion.arrValueMember) & ")"
                End If
                Qry &= " ORDER BY [TSPL_APP_LOCATION].Location_Name"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
                Dim BaseQry As New StringBuilder()
                For Each row In dt.Rows
                    If clsCommon.myLen(BaseQry) <> 0 Then
                        BaseQry.Append(" Union All ")
                        BaseQry.Append(Environment.NewLine)
                    End If
                    BaseQry.Append(" SELECT '" & clsCommon.myCstr(row("Union Name")) & "' As [Union Name],TSPL_DBT_NEFT.Document_Code As [Document Code],Convert(Varchar(10),Max(TSPL_DBT_NEFT.Document_Date),103) AS [Document Date],Convert(varchar(10),Max(TSPL_DBT_NEFT.From_Date),103) AS [From Date],Convert(varchar(10),Max(TSPL_DBT_NEFT.To_Date),103) AS [To Date],Case When Max(TSPL_DBT_NEFT.Status)=1 Then 'Approved' Else 'Pending' End AS [Union Status],MAX(TSPL_DBT_NEFT.Posted_By) AS [Union Approved By],Convert(varchar(10),MAX(TSPL_DBT_NEFT.Posted_Date),103) AS [Union Approved Date],COUNT(TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code) AS [No Of Farmer],COUNT(DISTINCT TSPL_DBT_NEFT_DETAIL.VLC_Uploader_Code) AS [No Of DCS],	SUM(TSPL_DBT_NEFT_DETAIL.Amount) AS Amount,Case When Max(TSPL_DBT_NEFT.RCDF_Status)=1 Then 'Approved' Else 'Pending' End AS [RCDF Status],MAX(TSPL_DBT_NEFT.RCDF_Post_By) AS [RCDF Approved By],Convert(varchar(10),MAX(TSPL_DBT_NEFT.RCDF_Post_Date),103) AS [RCDF Approved Date]    
FROM [" & clsCommon.myCstr(row("Code")) & "].[dbo].TSPL_DBT_NEFT_DETAIL
LEFT JOIN [" & clsCommon.myCstr(row("Code")) & "].[dbo].TSPL_DBT_NEFT ON TSPL_DBT_NEFT.Document_Code = TSPL_DBT_NEFT_DETAIL.Document_Code
WHERE CONVERT(DATE, TSPL_DBT_NEFT.From_Date, 103) >= CONVERT(DATE, '" & GetStartDateOfMonth(txtFromMonth.Value) & "', 103)
  AND CONVERT(DATE, TSPL_DBT_NEFT.To_Date, 103) <= CONVERT(DATE, '" & GetEndOrCurrentDateOfMonth(txtToMonth.Value) & "', 103)
GROUP BY TSPL_DBT_NEFT.Document_Code ")
                    BaseQry.Append(Environment.NewLine)
                Next
                Dim finalQry As String = "Select * from (" & clsCommon.myCstr(BaseQry) & ")xyz ORDER BY [Union Name],[Document Code]  "
                dt = Nothing
                dt = clsDBFuncationality.GetDataTable(finalQry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.MasterView.Refresh()
                    gv1.DataSource = dt
                    SetGridFormat()
                    RadPageView1.SelectedPage = RadPageViewPage2
                    EnableDisableFields(False)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Not Found !", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat()
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
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("No Of Farmer", "{0:f0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("No Of DCS", "{0:f0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("Amount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv1.BestFitColumns()
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            Dim qry As String = "select 'Bank Letter' As Code Union All Select 'Excel' As Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frm As New FrmFreeComboBox()
            frm.Text = Me.Text
            frm.ComboSource = dt
            frm.ComboValueMember = "Code"
            frm.ComboDisplayMember = "Code"
            frm.LabelCaption = "Print/Export"
            frm.ShowDialog()

            qry = Nothing
            dt = Nothing
            qry = "select CODE from TSPL_ATTACHMENTS where FormId='ADBT-NEFT-UPL' And TransactionId='" & clsCommon.myCstr(gv1.CurrentRow.Cells("Document Code").Value) & "' "
            If clsCommon.CompairString(frm.strRetValue, "Bank Letter") = CompairStringResult.Equal Then
                qry += " And FileName='BankLetter.pdf'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)
                Dim obj As New ucAttachment()
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj.FunShow(clsCommon.myCstr(dt.Rows(0)("CODE")))
                    obj = Nothing
                Else
                    clsCommon.MyMessageBoxShow(Me, "Attachment not found !", Me.Text)
                End If
            Else
                qry += " And FileName='NEFTDetail.xls'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)
                Dim obj As New ucAttachment()
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj.FunShow(clsCommon.myCstr(dt.Rows(0)("CODE")))
                    obj = Nothing
                Else
                    clsCommon.MyMessageBoxShow(Me, "Attachment not found !", Me.Text)
                End If
            End If
            dt = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            ExportGrid(EnumExportTo.Excel)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            ExportGrid(EnumExportTo.PDF)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                Dim strHeading As String = clsCommon.myCstr("PD Account Summaries Report")
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(" ")
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid(strHeading, gv1, arrHeader, strHeading)
                Else
                    PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
                    clsCommon.MyExportToPDF(strHeading, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


End Class