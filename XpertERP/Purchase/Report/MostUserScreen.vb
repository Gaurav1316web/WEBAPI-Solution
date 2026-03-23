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
        chkDateRange.Enabled = True
        RadGroupBox3.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        MyComboBox1.SelectedIndex = 0
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub


    Public Sub LoadData()
        Try
            Dim dt As New DataTable
            If txtTopCount.Value = 0 Then
                Throw New Exception("Top count can't be zero !")
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
            Dim strQry As String = ""
            If chkDateRange.Checked Then
                strQry = " SELECT TOP " & clsCommon.myCstr(txtTopCount.Value) & "  'DBLocation' AS [Union],Max(TSPL_PROGRAM_COUNTER.Created_Date) As [Created Date],Max(TSPL_PROGRAM_COUNTER.Created_By) as [Created By],TSPL_PROGRAM_COUNTER.Program_Code AS [Screen Code],Max(TSPL_PROGRAM_MASTER.Program_Name) AS [Screen Name],COUNT(TSPL_PROGRAM_COUNTER.PK_Id) As [Count]
FROM DBNamePrefixTSPL_PROGRAM_COUNTER 
left outer join DBNamePrefixTSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=TSPL_PROGRAM_COUNTER.program_code  WHERE 2 = 2 
and  Convert(date,TSPL_PROGRAM_COUNTER.created_date,103)>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and Convert(date,TSPL_PROGRAM_COUNTER.created_date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
                strQry &= whereCondition &
" Group By TSPL_PROGRAM_COUNTER.Program_Code,TSPL_PROGRAM_MASTER.Form_Open_Counter "
            Else
                strQry = " SELECT TOP " & clsCommon.myCstr(txtTopCount.Value) & " 'DBLocation' AS [Union],Program_Code AS [Screen Code], Program_Name AS [Screen Name], Form_Open_Counter AS [Count]
FROM DBNamePrefixTSPL_PROGRAM_MASTER WHERE 2 = 2 "
                strQry &= whereCondition
            End If
            Dim FinalQry As String = ""
            If objCommonVar.RCDFCFP Then
                strQry += " order by [Count] desc"
                FinalQry += clsERPFuncationality.ConvertQryForAllUnion(strQry, "DBNamePrefix", "DBLocation", "Sno")

            Else
                strQry = strQry.Replace("DBNamePrefix", "")
                strQry = strQry.Replace("DBLocation", "")
                FinalQry += strQry
            End If
            Dim SuperFinalQry As String = ""
            If chkAcrossUnions.Checked Then
                SuperFinalQry = "  select top " & clsCommon.myCstr(txtTopCount.Value) & " [Screen Code],max([Screen Name]) as [Screen Name],sum([Count]) as [Count],sum(1) as Rep from (" & FinalQry & "
) xx group by [Screen Code] 
order by Rep desc,[Count] desc"
            Else
                SuperFinalQry += "  select * from ( " & FinalQry + " ) xx  ORDER BY [Union],[Count] DESC"
            End If


            dt = clsDBFuncationality.GetDataTable(SuperFinalQry)

            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()
            RadGroupBox3.Enabled = False
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
            Try
                If chkAcrossUnions.Checked Then
                    Gv1.Columns("Rep").IsVisible = False
                ElseIf chkDateRange.Checked Then
                    Gv1.Columns("Created Date").IsVisible = False
                    Gv1.Columns("Created By").IsVisible = False
                End If
                If objCommonVar.RCDFCFP Then
                    Gv1.Columns("Union").IsVisible = True
                Else
                    Gv1.Columns("Union").IsVisible = False
                End If
            Catch ex As Exception
            End Try

            Gv1.BestFitColumns()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkDateRange_Click(sender As Object, e As EventArgs) Handles chkDateRange.Click
        If chkDateRange.Checked Then
            RadGroupBox3.Visible = False
            'RadGroupBox3.Visible = True
        Else
            RadGroupBox3.Visible = True
            'RadGroupBox3.Visible = False
        End If


    End Sub

    Private Sub MostUserScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            chkAcrossUnions.Visible = objCommonVar.RCDFCFP
            RadGroupBox3.Visible = False
            ToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
            fromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
            txtTopCount.Value = 10
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class