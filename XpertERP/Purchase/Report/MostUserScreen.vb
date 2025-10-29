Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls
Public Class MostUserScreen
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        ddlBankType.SelectedIndex = 0
        MyComboBox1.SelectedIndex = 0
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData
    End Sub


    Public Sub LoadData()
        Try
            Dim dt As New DataTable
            Dim topCount As Integer


            If Not Integer.TryParse(ddlBankType.Text, topCount) Then
                topCount = 10
            End If


            Dim whereCondition As String = ""

            Select Case MyComboBox1.Text
                Case "Setup"
                    whereCondition = " and Image_Number = '27'"
                Case "Transaction"
                    whereCondition = " and Image_Number = '8'"
                Case "Report"
                    whereCondition = " and Image_Number = '37'"
            End Select

            Dim strQry As String = "
            SELECT TOP " & topCount & " 
                Program_Code AS [Screen Code],
                Program_Name AS [Screen Name],
                Form_Open_Counter AS [Count]
            FROM TSPL_PROGRAM_MASTER WHERE 2 = 2 
            " & whereCondition & "
            ORDER BY Form_Open_Counter DESC"

            dt = clsDBFuncationality.GetDataTable(strQry)

            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
            Gv1.BestFitColumns()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



End Class