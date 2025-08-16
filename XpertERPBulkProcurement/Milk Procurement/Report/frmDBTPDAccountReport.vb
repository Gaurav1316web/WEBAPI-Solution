Imports common

Public Class frmDBTPDAccountReport
    Inherits FrmMainTranScreen
    Private Sub frmDBTApprovalStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
            UcAttachment1.Form_ID = MyBase.Form_ID
            UcAttachment1.isDeleteTheAttachment = False
            UcAttachment1.settAutoAttachment = True

            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub fndUnion__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndUnion._MYValidating
        Try
            Dim Sqlqry As String = "SELECT [TSPL_APP_LOCATION].DataBase_Name as DataBaseName,[TSPL_APP_LOCATION].Location_Name as [Location Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION]   "
            fndUnion.Value = clsCommon.ShowSelectForm("dbpda", Sqlqry, "DataBaseName", " Union_Report=1 and Apply_PD_Account=1 ", fndUnion.Value, "Location_Name", isButtonClicked)
            txtDBTNEFTNo.Value = ""
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
        'TSPL_DBT_NEFT_DETAIL.PK_Id,
        Dim BaseQry As String = ""
        BaseQry = " select dbo.RemoveExtraSpaces(UPPER(dbo.RemoveSpecialCharactersWithNumber(TSPL_DBT_NEFT_DETAIL.MP_Name))) as NAME,TSPL_DBT_NEFT_DETAIL.MP_IFSC_No as [IFSC_CODE],TSPL_DBT_NEFT_DETAIL.MP_Account_No as BankActNo,TSPL_STATE_MASTER.STATE_NAME as StateName,TSPL_DISTRICT_MASTER.Name as DistrictName,31219 as OfficeId,dbo.RemoveSpecialCharacters(TSPL_MP_MASTER.Add1) as Address,isnull(TSPL_DBT_NEFT_DETAIL.MP_Mobile_No,'') as ContactNo,'' as [GSTINNO],'' as [PAN],TSPL_DBT_NEFT.Sanction_Number as SancationNo,convert(varchar,TSPL_DBT_NEFT.Sanction_Date ,103) as SancationDate,cast(TSPL_DBT_NEFT_DETAIL.Amount  as integer) as Amount,3831 as [PD account no],
TSPL_MASTER.dbo.TSPL_APP_LOCATION.PD_Account_Prefix +FORMAT(UKID, '0000')+CAST(Lot_No as varchar) as Refenceno,TSPL_MASTER.dbo.TSPL_APP_LOCATION.Location_Name,FORMAT((Document_Date), 'MMM-yy') AS MonthName
from " + fndUnion.Value + ".dbo.TSPL_DBT_NEFT_DETAIL
left outer join " + fndUnion.Value + ".dbo.TSPL_DBT_NEFT on " + fndUnion.Value + ".dbo.TSPL_DBT_NEFT.Document_Code = " + fndUnion.Value + ".dbo.TSPL_DBT_NEFT_DETAIL.Document_Code
left outer join " + fndUnion.Value + ".dbo.TSPL_MP_INCENTIVE_ENTRY_DETAIL on  " + fndUnion.Value + ".dbo.TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id= " + fndUnion.Value + ".dbo.TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
left outer join " + fndUnion.Value + ".dbo.TSPL_MP_MASTER on " + fndUnion.Value + ".dbo.TSPL_MP_MASTER.MP_Code=" + fndUnion.Value + ".dbo.TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code
left outer join " + fndUnion.Value + ".dbo.TSPL_DISTRICT_MASTER on " + fndUnion.Value + ".dbo.TSPL_DISTRICT_MASTER.Code=" + fndUnion.Value + ".dbo.TSPL_MP_MASTER.DISTRICT_Code
left outer join " + fndUnion.Value + ".dbo.TSPL_STATE_MASTER on " + fndUnion.Value + ".dbo.TSPL_STATE_MASTER.STATE_CODE=" + fndUnion.Value + ".dbo.TSPL_MP_MASTER.State_Code
left outer join TSPL_MASTER.dbo.TSPL_APP_LOCATION on TSPL_MASTER.dbo.TSPL_APP_LOCATION.DataBase_Name='" + fndUnion.Value + "'
where ISNULL(TSPL_DBT_NEFT.Status, 0) = 1 and isnull(TSPL_DBT_NEFT.RCDF_Status,0)= 1   and " + fndUnion.Value + ".dbo.TSPL_DBT_NEFT.Document_Code='" + txtDBTNEFTNo.Value + "'
 "
        If rbtnSummary.IsChecked Then
            BaseQry = " Select CASE WHEN Refenceno IS NULL THEN 'Total' ELSE MAX(Location_Name) END AS DataBase_Name,CASE WHEN Refenceno IS NULL THEN '' ELSE MAX(MonthName)END AS MonthName,
Refenceno as[ERP Reference No],COUNT(NAME) AS [No Of Ben. as per ERP],Sum(Amount)Amount from ( " & BaseQry & " )xx 
group by Refenceno 
ORDER BY DataBase_Name "
        End If
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

                Dim item3 As New GridViewSummaryItem("No Of Ben. as per ERP", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)

                gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                'SetGridFormat()
                'Dim viewBlank As New TableViewDefinition()
                'gvData.ViewDefinition = viewBlank
                gvData.BestFitColumns()
                enableDisable(False)


                UcAttachment1.BlankAllControls()
                UcAttachment1.LoadData(fndUnion.Value + "#" + txtDBTNEFTNo.Value)
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
        Panel6.Enabled = val
    End Function
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        UcAttachment1.BlankAllControls()
        gvData.DataSource = Nothing
        gvData.Rows.Clear()
        gvData.MasterTemplate.SummaryRowsBottom.Clear()
        gvData.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
        enableDisable(True)
    End Sub
    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            If gvData.Rows.Count > 0 Then
                gvData.MyExportFilePath = "DoNotOpen"
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

                If clsCommon.CompairString(gvData.MyExportFilePath, "DoNotOpen") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
                Else
                    Dim obj As New clsAttachDocument
                    obj.CODE = ""
                    obj.FormId = Form_ID
                    obj.TransactionId = fndUnion.Value + "#" + txtDBTNEFTNo.Value
                    obj.SNo = -1
                    obj.FileName = System.IO.Path.GetFileName(gvData.MyExportFilePath) ''
                    obj.COMMENTS = "Exported by " + objCommonVar.CurrentUserCode + " at " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm:ss tt")
                    obj.SaveData(obj, Nothing, gvData.MyExportFilePath, False)
                    Process.Start(gvData.MyExportFilePath)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub frmDBTPDAccountReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F11 Then
                If RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed Then
                    RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
                Else
                    RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        print(EnumExportTo.Excel)
    End Sub
End Class