Imports common

Public Class frmDBTPDAccountReport
    Inherits FrmMainTranScreen
    Private Sub frmDBTApprovalStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndUnion__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndUnion._MYValidating
        Try
            Dim Sqlqry As String = "SELECT [TSPL_APP_LOCATION].DataBase_Name as DataBaseName,[TSPL_APP_LOCATION].Location_Name as [Location Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION]   "
            fndUnion.Value = clsCommon.ShowSelectForm("dbpda", Sqlqry, "DataBaseName", " Union_Report=1 and Apply_PD_Account=1 ", fndUnion.Value, "Location_Name", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub txtDBTNEFTNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDBTNEFTNo._MYValidating
        Try
            If clsCommon.myLen(fndUnion.Value) <= 0 Then
                fndUnion.Focus()
                Throw New Exception("Please select union")
            End If

            Dim Sqlqry As String = "select ISNULL(Apply_PD_Account,0) as Apply_PD_Account,Apply_PD_Account_Date,DataBase_Name from TSPL_MASTER.dbo.TSPL_APP_LOCATION where DataBase_Name='" + fndUnion.Value + "'"
            Dim dtMaster As DataTable = clsDBFuncationality.GetDataTable(Sqlqry)
            If dtMaster Is Nothing OrElse dtMaster.Rows.Count <= 0 Then
                Throw New Exception("Invalid union " + fndUnion.Value)
            End If
            If clsCommon.myCDecimal(dtMaster.Rows(0)("Apply_PD_Account")) = 0 Then
                Throw New Exception("PD Account Not applied")
            End If

            Sqlqry = "select TSPL_DBT_NEFT.Document_Code as [DocumentNo],CONVERT(varchar,TSPL_DBT_NEFT.From_Date,103) as FromDate,CONVERT(varchar,To_Date,103) as ToDate,TSPL_DBT_NEFT.Sanction_Number as SanctionNumber,CONVERT(varchar,TSPL_DBT_NEFT.Sanction_Date,103) as  SanctionDate,TSPL_DBT_NEFT.Sanction_Amount as SanctionAmount
from " + fndUnion.Value + ".dbo.TSPL_DBT_NEFT "
            Dim whr As String = " ISNULL(TSPL_DBT_NEFT.Status, 0) = 1 and isnull(TSPL_DBT_NEFT.RCDF_Status,0)= 1  
and convert(date, TSPL_DBT_NEFT.To_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.myCDate(dtMaster.Rows(0)("Apply_PD_Account_Date")), "dd/MMM/yyyy") + "'"
            txtDBTNEFTNo.Value = clsCommon.ShowSelectForm("dbpdDoc", Sqlqry, "DocumentNo", whr, txtDBTNEFTNo.Value, "", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub




    Private Function ReportQry() As String
        If clsCommon.myLen(fndUnion.Value) <= 0 Then
            fndUnion.Focus()
            Throw New Exception("Please select union")
        End If
        If clsCommon.myLen(txtDBTNEFTNo.Value) <= 0 Then
            txtDBTNEFTNo.Focus()
            Throw New Exception("Please DBT NEFT No")
        End If

        Dim BaseQry As String = "select TSPL_DBT_NEFT_DETAIL.PK_Id,TSPL_DBT_NEFT_DETAIL.MP_Name as NAME,TSPL_DBT_NEFT_DETAIL.MP_IFSC_No as [IFSC_CODE],TSPL_DBT_NEFT_DETAIL.MP_Account_No as BankActNo,TSPL_STATE_MASTER.STATE_NAME as StateName,TSPL_DISTRICT_MASTER.Name as DistrictName,31219 as OfficeId,TSPL_MP_MASTER.Add1 as Address,isnull(TSPL_DBT_NEFT_DETAIL.MP_Mobile_No,'') as ContactNo,'' as [GSTINNO],'' as [PAN],TSPL_DBT_NEFT.Sanction_Number as SancationNo,convert(varchar,TSPL_DBT_NEFT.Sanction_Date ,103) as SancationDate,cast(TSPL_DBT_NEFT_DETAIL.Amount  as integer) as Amount,3831 as [PD account no]
from " + fndUnion.Value + ".dbo.TSPL_DBT_NEFT_DETAIL
left outer join " + fndUnion.Value + ".dbo.TSPL_DBT_NEFT on " + fndUnion.Value + ".dbo.TSPL_DBT_NEFT.Document_Code = " + fndUnion.Value + ".dbo.TSPL_DBT_NEFT_DETAIL.Document_Code
left outer join " + fndUnion.Value + ".dbo.TSPL_MP_INCENTIVE_ENTRY_DETAIL on  " + fndUnion.Value + ".dbo.TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id= " + fndUnion.Value + ".dbo.TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
left outer join " + fndUnion.Value + ".dbo.TSPL_MP_MASTER on " + fndUnion.Value + ".dbo.TSPL_MP_MASTER.MP_Code=" + fndUnion.Value + ".dbo.TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code
left outer join " + fndUnion.Value + ".dbo.TSPL_DISTRICT_MASTER on " + fndUnion.Value + ".dbo.TSPL_DISTRICT_MASTER.Code=" + fndUnion.Value + ".dbo.TSPL_MP_MASTER.DISTRICT_Code
left outer join " + fndUnion.Value + ".dbo.TSPL_STATE_MASTER on " + fndUnion.Value + ".dbo.TSPL_STATE_MASTER.STATE_CODE=" + fndUnion.Value + ".dbo.TSPL_MP_MASTER.State_Code
where ISNULL(TSPL_DBT_NEFT.Status, 0) = 1 and isnull(TSPL_DBT_NEFT.RCDF_Status,0)= 1   and " + fndUnion.Value + ".dbo.TSPL_DBT_NEFT.Document_Code='" + txtDBTNEFTNo.Value + "' "
        Return BaseQry
    End Function

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Try
            GetReportID()

            Dim query = ReportQry()
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                gvData.DataSource = Nothing
                gvData.Rows.Clear()
                gvData.Columns.Clear()
                gvData.GroupDescriptors.Clear()
                gvData.MasterTemplate.SummaryRowsBottom.Clear()
                gvData.MasterView.Refresh()
                gvData.DataSource = dt2
                For ii As Integer = 0 To gvData.Columns.Count - 1
                    gvData.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gvData.EnableFiltering = True
                gvData.AllowAddNewRow = False
                gvData.ShowGroupPanel = False
                gvData.MasterTemplate.SummaryRowsBottom.Clear()
                Dim summaryRowItem As New GridViewSummaryRowItem()
                summaryRowItem.Add(New GridViewSummaryItem("Amount", "{0:n0}", GridAggregateFunction.Sum))
                gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                'SetGridFormat()
                'Dim viewBlank As New TableViewDefinition()
                'gvData.ViewDefinition = viewBlank
                gvData.BestFitColumns()
                enableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""

    End Sub

    Function enableDisable(ByVal val As Boolean)
        txtDBTNEFTNo.Enabled = val
        fndUnion.Enabled = val
    End Function

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        gvData.DataSource = Nothing
        gvData.Rows.Clear()
        gvData.MasterTemplate.SummaryRowsBottom.Clear()
        gvData.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
        enableDisable(True)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            If gvData.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt"))
                arrHeader.Add("Union : " + fndUnion.Value)
                arrHeader.Add("DBTNEft No : " + txtDBTNEFTNo.Value)
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gvData, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gvData, "", "", , arrHeader)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gvData, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class