Imports common
Imports System.IO
Public Class frmUnionLastGstInvoice
    Inherits FrmMainTranScreen
    Private Sub frmUnionLastGstInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' txtFromDate.Value = clsCommon.GETSERVERDATE()
        ' txtToDate.Value = clsCommon.GETSERVERDATE()
        UninonWise()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        UninonWise()
    End Sub
    Sub UninonWise()
        Try
            Dim query As String = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim dt1 As DataTable = New DataTable()
            Dim dtFinal As DataTable = New DataTable()
            Dim docNo As String = ""
            'dt = clsMilkUnion.UnionDBName()
            query = ""
            Dim arrUnion As New ArrayList()
            arrUnion.Add(objCommonVar.CurrComp_Code1)
            If objCommonVar.RCDFCFP Then
                dt = clsMilkUnion.UnionDBName()
            Else
                dt = clsMilkUnion.UnionDBName1(arrUnion)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    query = " select Top 1 " + clsCommon.myCstr(ii + 1) + " AS SNo,
'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS Union_Name, 
[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Code as Document_Code,
convert(varchar,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date 
                    FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD  
                    WHERE [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Ack_no IS NOT NULL    AND [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Ack_no <> ''  
                    ORDER BY [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Date DESC ; "
                    dt1 = clsDBFuncationality.GetDataTable(query)
                    dtFinal.Merge(dt1)
                Next
            End If

            If dtFinal IsNot Nothing OrElse dtFinal.Rows.Count > 0 Then

                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dtFinal
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                SetGridFormat1()
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Sub SetGridFormat1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            'gv1.Columns("Document_No").HeaderText = "Document No."
            'gv1.Columns("SNo").IsVisible = True
            gv1.Columns("SNo").HeaderText = "SNo"
            'gv1.Columns("Union_Name").IsVisible = True
            gv1.Columns("Union_Name").HeaderText = "Union Name"
            'gv1.Columns("Document_Date").IsVisible = True
            gv1.Columns("Document_Date").HeaderText = "Invoice Date"
            'gv1.Columns("Document_Code").IsVisible = True
            gv1.Columns("Document_Code").HeaderText = "Invoice Code"
        Next


        gv1.AutoSizeRows = True
        gv1.BestFitColumns()
        gv1.MasterTemplate.AutoExpandGroups = True
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        'txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        'txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        'RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo1_Click(sender As Object, e As EventArgs) 
        UninonWise()
    End Sub
End Class