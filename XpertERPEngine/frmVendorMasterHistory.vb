Imports System.Data.Common
Imports System
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net
Imports Outlook = Microsoft.Office.Interop.Outlook
Imports Telerik.WinControls
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Drawing

Public Class frmVendorMasterHistory
#Region "Variables"
    Public VendorCode As String = Nothing
    Public dt As DataTable = Nothing

#End Region

    Private Sub frmVendorMasterHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ShowData()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Sub ShowData()
        Try

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                gv1.EnableFiltering = True
                gv1.EnableSorting = True
                FormatGrid()
            Else
                gv1.DataSource = Nothing

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub FormatGrid()
        gv1.AllowAddNewRow = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
        Next

        gv1.Columns("Hist_Version").IsVisible = False
        gv1.Columns("Hist_Version").Width = 101
        gv1.Columns("Hist_Version").HeaderText = "Hist_Version"


    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Me.Close()
    End Sub

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        Try

            If gv1.Rows.Count > 0 Then
                For i As Integer = 0 To gv1.Rows.Count - 2

                    '' Column A
                    For ic As Integer = 2 To gv1.Columns.Count - 1
                        ''Condition
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(ic).Value), clsCommon.myCstr(gv1.Rows(i + 1).Cells(ic).Value)) = CompairStringResult.Equal Then
                        Else
                            gv1.Rows(i + 1).Cells(ic).Style.BackColor = System.Drawing.Color.Yellow
                            gv1.Rows(i + 1).Cells(ic).Style.ForeColor = Color.Red

                        End If

                    Next


                Next

            End If







        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub



End Class
