
Imports common
Imports System.IO
Public Class frmDBTUnionPayment
    Inherits FrmMainTranScreen


    Private Sub frmDBTUnionPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        dtpFromDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)

        If clsCommon.myLen(objCommonVar.CurrentUnionDataBase) > 0 Then
            Dim Union As ArrayList = Nothing
            Dim qry As String = " Select DataBase_Name from TSPL_USER_MASTER where User_Code = '" + objCommonVar.CurrentUserCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Union = New ArrayList()
                For Each drZone As DataRow In dt.Rows
                    Union.Add(clsCommon.myCstr(drZone("DataBase_Name")))
                Next
            End If
            txtUnion.arrValueMember = Union
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Sub funreset()
        gv1.DataSource = Nothing
        txtUnion.arrValueMember = Nothing
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        dtpFromDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub LoadData()
        Try
            Dim qry As String = ""
            Dim qryies As String = ""
            Dim Baseqry As String = ""
            Dim Baseqry1 As String = ""
            Dim Baseqry2 As String = ""
            Dim dbNames As String = ""
            Dim portDt As New DataTable
            Dim dtGrandTotal As DataTable
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found", Me.Text)
                gv1.DataSource = Nothing
                Exit Sub
            End If
            Dim ss As String = clsCommon.GetMulcallString(txtUnion.arrValueMember)

            If txtUnion.arrValueMember Is Nothing Then
                qry = " select  [TSPL_APP_LOCATION].PD_Account_Prefix as PortNo,[TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                            from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE Apply_PD_Account = 1 order by [TSPL_APP_LOCATION].Location_Name "
            Else
                qry = " select  [TSPL_APP_LOCATION].PD_Account_Prefix as PortNo,[TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                        from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE Apply_PD_Account = 1 and [TSPL_APP_LOCATION].DataBase_Name  in (" + ss + ") 
                        order by [TSPL_APP_LOCATION].Location_Name "
            End If
            'dt = clsMilkUnion.UnionDBName()
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        qryies += " UNION ALL "
                    Else
                        qryies += " ( "
                    End If

                    qryies += " select '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],FORMAT(MAX(From_Date), 'MMM/yyyy') AS Month_Year,
                               '" + clsCommon.myCstr(dt.Rows(ii).Item("PortNo")) + "'+FORMAT(UKID, '0000')+CAST((Lot_No) as varchar) as Refence_No,count(Lot_No)No_Of_Record,SUM(Qty)Milk_Qty,sum(Amount)Amount from (
                               select isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Lot_No,'')Lot_No,
                               isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.UKID,'')UKID ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Amount,
                               [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.RCDF_Post_Date,
                               [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty,[TSPL_MASTER].[dbo].TSPL_APP_LOCATION.Apply_PD_Account_Date
                               from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL 
                               left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Document_Code
                               left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
                               left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2 = 2
							   left outer join [TSPL_MASTER].[dbo].TSPL_APP_LOCATION ON [TSPL_MASTER].[dbo].TSPL_APP_LOCATION.DataBase_Name = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.DataBase_Name
                               )X where X.From_Date >= X.Apply_PD_Account_Date and convert(date,x.RCDF_Post_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(dtpFromDate.Value) + "',103)
                         and convert(date,x.RCDF_Post_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(dtpToDate.Value) + "',103)  group by x.Lot_No,x.UKID
                                
                               Union all
                               select 'Total' AS [Union Name],'' as Month_Year,'' as Refence_No,sum(No_Of_Record)No_Of_Record,sum(Milk_Qty)Milk_Qty,sum(Amount)Amount from (select '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                               FORMAT(MAX(From_Date), 'MMM/yyyy') AS Month_Year,'" + clsCommon.myCstr(dt.Rows(ii).Item("PortNo")) + "'+FORMAT(UKID, '0000')+CAST((Lot_No) as varchar) as Refence_No,count(Lot_No)No_Of_Record,SUM(Qty)Milk_Qty,sum(Amount)Amount
                               from (
                               select isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Lot_No,'')Lot_No,
                               isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.UKID,'')UKID ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Amount,
                               [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.RCDF_Post_Date,
                               [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty,[TSPL_MASTER].[dbo].TSPL_APP_LOCATION.Apply_PD_Account_Date
                               from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL 
                               left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Document_Code
                               left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
                               left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2 = 2
							   left outer join [TSPL_MASTER].[dbo].TSPL_APP_LOCATION ON [TSPL_MASTER].[dbo].TSPL_APP_LOCATION.DataBase_Name = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.DataBase_Name
                               )X where X.From_Date >= X.Apply_PD_Account_Date   and convert(date,x.RCDF_Post_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(dtpFromDate.Value) + "',103)
                         and convert(date,x.RCDF_Post_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(dtpToDate.Value) + "',103)  group by x.Lot_No,x.UKID)x group by x.[Union Name]
"

                Next

                Baseqry = "  " + qryies + "  ) "

                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        Baseqry1 += " UNION ALL "
                    Else
                        Baseqry1 += " ( "
                    End If

                    Baseqry1 += "  select 'Total' AS [Union Name],'' as Month_Year,'' as Refence_No,sum(No_Of_Record)No_Of_Record,sum(Milk_Qty)Milk_Qty,sum(Amount)Amount from (select '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                               FORMAT(MAX(From_Date), 'MMM/yyyy') AS Month_Year,'" + clsCommon.myCstr(dt.Rows(ii).Item("PortNo")) + "'+FORMAT(UKID, '0000')+CAST((Lot_No) as varchar) as Refence_No,count(Lot_No)No_Of_Record,SUM(Qty)Milk_Qty,sum(Amount)Amount
                               from (
                               select isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Lot_No,'')Lot_No,
                               isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.UKID,'')UKID ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Amount,
                               [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.RCDF_Post_Date,
                               [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty,[TSPL_MASTER].[dbo].TSPL_APP_LOCATION.Apply_PD_Account_Date
                               from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL 
                               left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Document_Code
                               left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
                               left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER ON 2 = 2
							   left outer join [TSPL_MASTER].[dbo].TSPL_APP_LOCATION ON [TSPL_MASTER].[dbo].TSPL_APP_LOCATION.DataBase_Name = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.DataBase_Name
                               )X where X.From_Date >= X.Apply_PD_Account_Date and convert(date,x.RCDF_Post_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(dtpFromDate.Value) + "',103)
                                and convert(date,x.RCDF_Post_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(dtpToDate.Value) + "',103) group by x.Lot_No,x.UKID)x group by x.[Union Name]  "
                Next

                Baseqry2 = " select 'Grand Total' AS [Union Name],'' as Month_Year,'' as Refence_No,sum(No_Of_Record)No_Of_Record,sum(Milk_Qty)Milk_Qty,
                             sum(Amount)Amount from " + Baseqry1 + " )x group by x.[Union Name] "
            End If

            portDt = clsDBFuncationality.GetDataTable(Baseqry)
            dtGrandTotal = clsDBFuncationality.GetDataTable(Baseqry2)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.EnableFiltering = False
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If portDt.Rows.Count > 0 Then
                portDt.Rows.Add("Grand Total", dtGrandTotal.Rows(0)("Month_Year"), dtGrandTotal.Rows(0)("Refence_No"), dtGrandTotal.Rows(0)("No_Of_Record"), dtGrandTotal.Rows(0)("Milk_Qty"), dtGrandTotal.Rows(0)("Amount"))
                gv1.DataSource = portDt
                gv1.BestFitColumns()
                SetGridFormation()
                'ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        gv1.ShowGroupPanel = False

        gv1.Columns("No_Of_Record").HeaderText = "No Of Record"
        gv1.Columns("Milk_Qty").HeaderText = "Milk Qty"
        gv1.Columns("Month_Year").HeaderText = "Month"
        gv1.Columns("Refence_No").HeaderText = "Reference No"

        'Dim item2 As New GridViewSummaryItem("No_Of_Record", "{0:0}", GridAggregateFunction.Sum)
        'Dim totalCount As Decimal = 0
        'For Each row As GridViewRowInfo In gv1.Rows
        '    ' Check if the row meets your condition to skip (e.g., check a specific column value)
        '    If Not row.Cells("Union Name").Value = "Total" Then
        '        ' Add the value to the total
        '        totalCount += Convert.ToDecimal(row.Cells("No_Of_Record").Value)
        '    End If
        'Next
        'item2.SummaryValue = totalCount
        ''gv1.Columns("No_Of_Record").Value = totalCount
        'summaryRowItem.Add(item2)
        'Dim item3 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)
        'Dim item4 As New GridViewSummaryItem("Milk_Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)

        'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmDBTUnionPayment & "'"))
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Month :" & MonthNo)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtUnion__My_Click(sender As Object, e As EventArgs) Handles txtUnion._My_Click
        Try
            Dim dt As DataTable
            Dim qry As String = ""

            If clsCommon.myLen(objCommonVar.CurrentUnionDataBase) > 0 Then
                qry = " Select DataBase_Name as [DataBase Name] from TSPL_USER_MASTER where User_Code = '" + objCommonVar.CurrentUserCode + "' "
                txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("SaleUnionDs", qry, "DataBase Name", "", txtUnion.arrValueMember, Nothing)

            Else
                dt = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
                If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                    Exit Sub
                End If

                qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

                txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("DBTUnionPay", qry, "DataBase Name", "", txtUnion.arrValueMember, Nothing)

            End If
            'dt = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            'If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            '    common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            '    Exit Sub
            'End If

            'qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

            'txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("DBTUnionPay", qry, "DataBase Name", "", txtUnion.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class