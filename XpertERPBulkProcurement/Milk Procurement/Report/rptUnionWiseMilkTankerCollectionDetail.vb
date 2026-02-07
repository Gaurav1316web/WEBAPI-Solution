Imports common
Imports System.Data.SqlClient
Public Class rptUnionWiseMilkTankerCollectionDetail
    Inherits FrmMainTranScreen
    Private Sub rptUnionWiseMilkTankerCollectionDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        If objCommonVar.RCDFCFP Then
            txtUnion.Visible = False
            txtRoute.Visible = False
            TxtMultiTanker.Visible = False
            lblLocation.Visible = False
            lblRoute.Visible = False
            lblTanker.Visible = False
        Else
            txtUnion.Visible = True
            txtRoute.Visible = True
            TxtMultiTanker.Visible = True
            lblLocation.Visible = True
            lblRoute.Visible = True
            lblTanker.Visible = True
        End If
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
            txtUnion.arrValueMember = Nothing
            txtRoute.arrValueMember = Nothing
            TxtMultiTanker.arrValueMember = Nothing


            txtUnion.arrValueMember = Nothing
            txtFromDate.Enabled = True
            txtToDate.Enabled = True
            txtFromDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
            txtToDate.Value = clsCommon.GETSERVERDATE()
            'EnableDisableFields(True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
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
    Private Sub txtUnion__My_Click(sender As Object, e As EventArgs) Handles txtUnion._My_Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                    Exit Sub
                End If
                Dim qry As String = ""
                If objCommonVar.RCDFCFP Then
                    qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

                Else
                    qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 AND [TSPL_APP_LOCATION].DataBase_Name='" & objCommonVar.CurrComp_Code1 & "' ORDER BY [TSPL_APP_LOCATION].Location_Name"

                End If
                'qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

                txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("DBTUnionPay", qry, "DataBase Name", "", txtUnion.arrValueMember, Nothing)
            Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Try

            Dim baseqry As String = Nothing
            Dim qry1 As String = Nothing
            Dim dtunion As New DataTable
            Dim uQry As String = ""
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found", Me.Text)
                gv1.DataSource = Nothing
                Exit Sub
            End If
            Dim ss As String = clsCommon.GetMulcallString(txtUnion.arrValueMember)

            If txtUnion.arrValueMember Is Nothing Then
                If objCommonVar.RCDFCFP Then
                    uQry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                            from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE 2=2 order by [TSPL_APP_LOCATION].Location_Name "
                Else
                    uQry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                            from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE 2=2 AND [TSPL_APP_LOCATION].DataBase_Name='" & objCommonVar.CurrComp_Code1 & "' order by [TSPL_APP_LOCATION].Location_Name "
                End If

            Else
                uQry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                        from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE [TSPL_APP_LOCATION].DataBase_Name  in (" + ss + ") 
                        order by [TSPL_APP_LOCATION].Location_Name "
            End If
            dtunion = clsDBFuncationality.GetDataTable(uQry)

            baseqry = ""

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(baseqry)
            'If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
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
                gv1.AllowAddNewRow = False
                gv1.ShowGroupPanel = False
                'SetGridFormat()

                gv1.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select ROUTE_NO,ROUTE_NAME from TSPL_BULK_ROUTE_MASTER where 2=2 "
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "ROUTE_NO", "ROUTE_NAME", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtMultiTanker__My_Click(sender As Object, e As EventArgs) Handles TxtMultiTanker._My_Click
        Try
            Dim qry As String = " select Tanker_No as Code,Tanker_Name as Name,Storage_Capacity,StorageCapacityDesc,Tanker_Transporter_Code from TSPL_TANKER_MASTER "
            TxtMultiTanker.arrValueMember = clsCommon.ShowMultipleSelectForm("MultiTanker", qry, "Code", "Name", TxtMultiTanker.arrValueMember, TxtMultiTanker.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class