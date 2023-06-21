Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPHRandPayroll
Public Class rptListofCowDCS
    Private Sub rptListofCowDCS_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Sub FormatGrid()
        ' Dim strItemCode, head2 As String
        'Dim summaryItem As New GridViewSummaryItem()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            'Gv1.Columns(ii).FormatString = "{0:n2}"
        Next

        Gv1.Columns("SNo").IsVisible = True
        Gv1.Columns("SNo").Width = 100
        Gv1.Columns("SNo").HeaderText = "SNo"

        Gv1.Columns("DCS Code").IsVisible = True
        Gv1.Columns("DCS Code").Width = 100
        Gv1.Columns("DCS Code").HeaderText = "DCS Code"

        Gv1.Columns("DCS Name").IsVisible = True
        Gv1.Columns("DCS Name").Width = 100
        Gv1.Columns("DCS Name").HeaderText = "DCS Name"

        Gv1.Columns("Apply Cow Price").IsVisible = True
        Gv1.Columns("Apply Cow Price").Width = 100
        Gv1.Columns("Apply Cow Price").HeaderText = "Apply Cow Price "

        Gv1.Columns("Start Date").IsVisible = True
        Gv1.Columns("Start Date").Width = 100
        Gv1.Columns("Start Date").HeaderText = "Start Date"

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True




    End Sub

    Private Sub txtMCCOwnBMC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCCOwnBMC._MYValidating
        Try
            Dim qry As String = "select MCC_Code as Code  from TSPL_MCC_MASTER "
            txtMCCOwnBMC.Value = clsCommon.ShowSelectForm("MCCFND@VLCVSPM", qry, "Code", "", txtMCCOwnBMC.Value, "Code", isButtonClicked)
            lblMCCOwnBMC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select MCC_NAME from tspl_MCC_MASTER where MCC_Code = '" + txtMCCOwnBMC.Value + "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            txtMCCOwnBMC.Value = ""
            lblMCCOwnBMC.Text = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If txtMCCOwnBMC.Value IsNot Nothing Then

                TemplateGridview = Gv1
                Dim qry As String = ""
                Dim dt As New DataTable

                qry = "select row_number() over(order by(select 1)) as SNo,VLC_Code_VLC_Uploader as DCS  ,VLC_Name as [DCS Name],case when Apply_Cow_Price=1 then 'Y' else 'N' end [Apply Cow Price],convert(date,TSPL_VLC_MASTER_HEAD.ApplyCowPriceDate,103)  as [Apply Cow Price Date] from TSPL_VLC_MASTER_HEAD
                    LEFT JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC where TSPL_MCC_MASTER.MCC_Code ='" + txtMCCOwnBMC.Value + "' "

                dt = clsDBFuncationality.GetDataTable(qry)
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()
                'FormatGrid()

                If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                    Gv1.DataSource = dt
                    For ii As Integer = 0 To Gv1.Columns.Count - 1
                        Gv1.Columns(ii).ReadOnly = True
                        'Gv1.Rows.Add()
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    Gv1.EnableFiltering = True
                    'FormatGrid()
                    Gv1.BestFitColumns()
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found ", Me.Text)
                    Exit Sub
                End If
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
End Class