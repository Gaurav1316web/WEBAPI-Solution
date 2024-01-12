Imports System.Data.Common
Imports System
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net
Imports Telerik.WinControls
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Drawing

Public Class frmMasterHistory
#Region "Variables"
    Public VendorCode As String = Nothing
    Public dt As DataTable = Nothing
    Public ReportID As String = Nothing
#End Region

    Private Sub frmMasterHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblExportMsg.Text = ""
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
                ' gv1.EnablePaging = True
                FormatGrid()
                ReStoreGridLayout()
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



    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try
            lblExportMsg.Text = ""
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            lblExportMsg.Text = "Exported Successfully."
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            ' Process.Start(filePath)
        Catch ex As Exception
            lblExportMsg.Text = ""
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            Dim obj As New clsGridLayout()
            gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.SaveData()
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
        End If
    End Sub
End Class
