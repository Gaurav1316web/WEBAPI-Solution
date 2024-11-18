Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class frmDailyDemandReport
    Inherits FrmMainTranScreen

    Private Sub frmDailyDemandReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        DailyDemandDetail()
    End Sub

    Sub DailyDemandDetail()
        Try
            Dim query As String = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If

            Dim docNo As String = ""
            Dim arrUnion As New ArrayList()
            arrUnion.Add(objCommonVar.CurrComp_Code1)
            If objCommonVar.RCDFCFP Then
                dt = clsMilkUnion.UnionDBName()
            Else
                dt = clsMilkUnion.UnionDBName1(arrUnion)
            End If
            'dt = clsMilkUnion.UnionDBName()
            query = ""

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If

                    query += " select *, Case When IsNull(xy.[Union Demand],0)>0 Then (xy.[Erp Demand]/xy.[Union Demand]*100) Else 0 End  As [Variation%]  from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name], SUM(XX.[Erp Demand])[Erp Demand],SUM(XX.[Union Demand])[Union Demand],SUM(xx.Cust_Code) as [No. Of Booth] FROM
                            (select 0 AS [Erp Demand],0 as Cust_Code,'' as ShiftType , QTY AS [Union Demand] from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DAILY_DEMAND_MASTER
                            WHERE convert(date,Document_Date,103) >='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103) <='" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            UNION ALL
                            SELECT  MAX(TotalQtyInLtr) as [Erp Demand] ,COUNT(DISTINCT TSPL_DEMAND_BOOKING_DETAIL.Cust_Code)Cust_Code,TSPL_DEMAND_BOOKING_MASTER.ShiftType,0 as [Union Demand] 
                            FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER
                            LEFT OUTER JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL.Document_No=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_MASTER.Document_No
                            WHERE convert(date,Document_Date,103) >='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,Document_Date,103) <='" + clsCommon.GetPrintDate(txtToDate.Value) + "' 
                            GROUP BY TSPL_DEMAND_BOOKING_MASTER.ShiftType,TSPL_DEMAND_BOOKING_MASTER.Document_No) XX ) XY "
                Next

            End If

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dt2
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                'SetGridFormat1()
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub
End Class