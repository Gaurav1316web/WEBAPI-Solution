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

Public Class frmTransactionHistory
#Region "Variables"
    Public VendorCode As String = Nothing
    Dim isCellValueChangedOpen As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Public dt As DataTable = Nothing
    Public PrimaryKeyValue As String = Nothing
    Public code As String = Nothing
    Public DetailTable As String = Nothing
    Public HeadTable As String = Nothing
    Public StopFirstTime As String = Nothing
    Public SelectVersion As Integer = 0
    Public Const colSelect As String = "colSelect"
    Dim dtAllData As DataTable = Nothing
    Dim IsInsideLoadDataOfItem As Boolean = False
    Dim strVersionNoSelect As String = ""
#End Region

    Private Sub frmTransactionHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ShowData()
            If clsCommon.myLen(DetailTable) > 0 Then
                '  OpenDetailHistoryList(True)
            End If
            btnShowData.Visible = False
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Sub ShowData()
        Try
            loadBlankGrid()
            isInsideLoadData = True
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                gv1.EnableFiltering = True
                gv1.EnableSorting = True
                ' gv1.EnablePaging = True
                FormatGrid()
                FillAllDetailData()
            Else
                gv1.DataSource = Nothing

            End If
        Catch ex As Exception

        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Sub loadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim colChkBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(colChkBox)

        gv1.AllowAddNewRow = False
        gv1.AllowColumnChooser = True
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = True
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        gv1.EnableAlternatingRowColor = True
        gv1.TableElement.TableHeaderHeight = 20
        gv1.EnableFiltering = True

    End Sub
   
    Private Sub FormatGrid()
        gv1.AllowAddNewRow = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next
        gv1.Columns(colSelect).ReadOnly = False

        gv1.Columns("Hist_Version").IsVisible = False
        gv1.Columns("Hist_Version").Width = 101
        gv1.Columns("Hist_Version").HeaderText = "Hist_Version"


    End Sub
    Private Sub FormatGridDetail()
        gvDetail.AllowAddNewRow = False
        gvDetail.TableElement.TableHeaderHeight = 40
        gvDetail.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvDetail.Columns.Count - 1
            gvDetail.Columns(ii).ReadOnly = True
            gvDetail.Columns(ii).IsVisible = True
        Next
      
        gvDetail.Columns("Hist_Version").IsVisible = True
        gvDetail.Columns("Hist_Version").Width = 101
        gvDetail.Columns("Hist_Version").HeaderText = "Hist_Version"

        gvDetail.EnableGrouping = False



    End Sub
    Sub UnCheckAll()
        If Gv1 IsNot Nothing AndAlso Gv1.ChildRows.Count > 0 Then
            For i As Integer = 0 To Gv1.ChildRows.Count - 1
                Gv1.ChildRows(i).Cells(colSelect).Value = False
            Next
            gvDetail.DataSource = Nothing
            gvDetail.Rows.Clear()
            gvDetail.Columns.Clear()
            isCellValueChangedOpen = False
        End If
    End Sub
    Sub CheckAll()
        If Gv1 IsNot Nothing AndAlso Gv1.ChildRows.Count > 0 Then
            For i As Integer = 0 To Gv1.ChildRows.Count - 1
                Gv1.ChildRows(i).Cells(colSelect).Value = True
            Next
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub HistoryDoubleClick(ByVal isButtonClick As Boolean)
        'Dim Versions As String = ""
        'SelectVersion = 0
        'StopFirstTime = 0
        'If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
        '    For ii As Integer = 0 To gv1.Rows.Count - 1
        '        Dim Versions_info As String = clsCommon.myCstr(gv1.ChildRows(ii).Cells(colSelect).Value)
        '        If clsCommon.CompairString(Versions_info, True) = CompairStringResult.Equal Then
        '            If clsCommon.myLen(Versions) > 0 Then
        '                If ii <> 0 Then
        '                    Versions += ","
        '                End If
        '            End If
        '            Versions += "" + clsCommon.myCstr(gv1.Rows(ii).Cells("Version").Value).Trim() + ""
        '            SelectVersion = SelectVersion + 1

        '            If (clsCommon.myCstr(SelectVersion) > 2) Then
        '                gv1.CurrentRow.Cells(colSelect).Value = False
        '                Exit Sub
        '            End If
        '        End If
        '    Next

        'End If

        Dim dt As DataTable = Nothing
        Dim Mainqry As String = ""
        Try
            Dim qry As String = clsDBFuncationality.getSingleValue("select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME='" & DetailTable + clsCommon.HistTablePostFix & "'")
            If clsCommon.myLen(qry) <= 0 Then
                clsCommon.MyMessageBoxShow("No History Table found")
                Exit Sub
            End If
            Dim strMasterCodeColumn As String = ""
            Dim strDetailTransCodeHistColumn As String = ""
            Dim dtMasterCategory As DataTable = Nothing

            '' Sequence MasterTable column 
            Dim Masteryqry As String = ""
            Masteryqry = "  SELECT  c.name as Name "
            Masteryqry += " FROM " & objCommonVar.CurrDatabase & ".sys.tables t"
            Masteryqry += " INNER JOIN " & objCommonVar.CurrDatabase & ".sys.all_columns c "
            Masteryqry += "  ON t.object_id = c.object_id"
            Masteryqry += " INNER JOIN " & objCommonVar.CurrDatabase & ".sys.types ty "
            Masteryqry += "  ON c.system_type_id = ty.system_type_id"
            Masteryqry += " WHERE t.name = '" & DetailTable & "'"
            Masteryqry += " order by c.name asc"
            dtMasterCategory = clsDBFuncationality.GetDataTable(Masteryqry)

            If dtMasterCategory IsNot Nothing AndAlso dtMasterCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtMasterCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strMasterCodeColumn += ","
                        strDetailTransCodeHistColumn += ","
                    End If
                    strMasterCodeColumn += "" + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + ""
                    strDetailTransCodeHistColumn += "max(" & DetailTable + clsCommon.HistTablePostFix & "." + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + ") as " + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + ""
                Next
            End If
            '' End
            '' =========Final Binding Main Qry=======

            Mainqry = "select max(" & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistVersion & ") as [Head version],max(" & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistVersion & ") as " & clsCommon.HistTableColHistVersion & " ,max(" & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistBy & ") as " & clsCommon.HistTableColHistBy & " ,max(" & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistOn & ") as " & clsCommon.HistTableColHistOn & "," & strDetailTransCodeHistColumn & " from " & DetailTable + clsCommon.HistTablePostFix & ""
            Mainqry += " where 2=2 and " & PrimaryKeyValue & "='" & code & "' and " & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistVersion & " in (" & strVersionNoSelect & ") group by " & DetailTable + clsCommon.HistTablePostFix & ".Hist_Version  "
            If clsCommon.CompairString(HeadTable, "TSPL_ITEM_MASTER") = CompairStringResult.Equal Then
                Mainqry += "  ,Conversion_Factor "
            End If
            '    Mainqry = "  select * from "
            '    Mainqry += " ( "
            '    Mainqry += " select " & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistVersion & " as [Head version]," & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistVersion & "," & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistBy & "," & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistOn & "," & strDetailTransCodeHistColumn & " from " & DetailTable + clsCommon.HistTablePostFix & ""
            '    'Mainqry += " left outer join " & HeadTable + clsCommon.HistTablePostFix & " on " & HeadTable + clsCommon.HistTablePostFix & "." & PrimaryKeyValue & "=" & DetailTable + clsCommon.HistTablePostFix & "." & PrimaryKeyValue & ""
            '' Mainqry += " union all "
            'Mainqry += " select '' as [Head version],'' as Version,'Current' as [User By],'' as " & clsCommon.HistTableColHistOn & "," & DetailTable & "." & strMasterCodeColumn & " from " & DetailTable & ""
            '    Mainqry += " )final "
            'Mainqry += " where 2=2 and final." & PrimaryKeyValue & "='" & code & "' and [Head Version] in (" & Versions & ")"
            ''==========End=========
            If clsCommon.myLen(strVersionNoSelect) > 0 Then
                dt = clsDBFuncationality.GetDataTable(Mainqry)
            End If
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                gvDetail.DataSource = Nothing
                gvDetail.Rows.Clear()
                gvDetail.Columns.Clear()
                gvDetail.BestFitColumns()
                gvDetail.EnableFiltering = True
                gvDetail.EnableSorting = True
                gvDetail.DataSource = dt
                FormatGridDetail()
            Else
                gvDetail.DataSource = Nothing
                gvDetail.Rows.Clear()
                gvDetail.Columns.Clear()
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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

    Sub OpenDetailHistoryList(ByVal isButtonClick As Boolean)
        Dim dt As DataTable = Nothing
        Dim Mainqry As String = ""
        StopFirstTime = 1

        Try
            Dim qry As String = clsDBFuncationality.getSingleValue("select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME='" & DetailTable + clsCommon.HistTablePostFix & "'")
            If clsCommon.myLen(qry) <= 0 Then
                clsCommon.MyMessageBoxShow("No History Table found")
                Exit Sub
            End If
            Dim strMasterCodeColumn As String = ""
            Dim strDetailTransCodeHistColumn As String = ""
            Dim dtMasterCategory As DataTable = Nothing

            '' Sequence MasterTable column 
            Dim Masteryqry As String = ""
            Masteryqry = "  SELECT  c.name as Name "
            Masteryqry += " FROM " & objCommonVar.CurrDatabase & ".sys.tables t"
            Masteryqry += " INNER JOIN " & objCommonVar.CurrDatabase & ".sys.all_columns c "
            Masteryqry += "  ON t.object_id = c.object_id"
            Masteryqry += " INNER JOIN " & objCommonVar.CurrDatabase & ".sys.types ty "
            Masteryqry += "  ON c.system_type_id = ty.system_type_id"
            Masteryqry += " WHERE t.name = '" & DetailTable & "'"
            Masteryqry += " order by c.name asc"
            dtMasterCategory = clsDBFuncationality.GetDataTable(Masteryqry)

            If dtMasterCategory IsNot Nothing AndAlso dtMasterCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtMasterCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strMasterCodeColumn += ","
                        strDetailTransCodeHistColumn += ","
                    End If
                    strMasterCodeColumn += "" + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + ""
                    strDetailTransCodeHistColumn += "" & DetailTable + clsCommon.HistTablePostFix & "." + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + ""
                    ' strDetailTransCodeHistColumn += "max(" & DetailTable + clsCommon.HistTablePostFix & "." + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + ") as " + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + ""
                Next
            End If
            '' End
            '' =========Final Binding Main Qry=======

            Mainqry = "  select * from "
            Mainqry += " ( "
            Mainqry += " select " & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistVersion & " as [Head version]," & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistVersion & "," & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistBy & "," & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistOn & "," & strDetailTransCodeHistColumn & " from " & DetailTable + clsCommon.HistTablePostFix & ""
            'Mainqry += " left outer join " & HeadTable + clsCommon.HistTablePostFix & " on " & HeadTable + clsCommon.HistTablePostFix & "." & PrimaryKeyValue & "=" & DetailTable + clsCommon.HistTablePostFix & "." & PrimaryKeyValue & ""
            Mainqry += " union all "
            Mainqry += " select '' as [Head version],'' as Version,'Current' as [User By],'' as " & clsCommon.HistTableColHistOn & "," & DetailTable & "." & strMasterCodeColumn & " from " & DetailTable & ""
            Mainqry += " )final "
            Mainqry += " where 2=2 and final." & PrimaryKeyValue & "='" & code & "'"
            ''==========End=========
            dt = clsDBFuncationality.GetDataTable(Mainqry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                gvDetail.Rows.Clear()
                gvDetail.Columns.Clear()
                gvDetail.BestFitColumns()
                gvDetail.EnableFiltering = True
                gvDetail.EnableSorting = True
                gvDetail.DataSource = dt
                FormatGridDetail()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gvDetail_CellFormatting1(sender As Object, e As CellFormattingEventArgs) Handles gvDetail.CellFormatting
        Try
            If StopFirstTime = 0 Then
                If gvDetail.Rows.Count > 0 Then

                    For i As Integer = 0 To gvDetail.Rows.Count - 2

                        '' Column A
                        For ic As Integer = 3 To gvDetail.Columns.Count - 1
                            ''Condition
                            If clsCommon.CompairString(clsCommon.myCstr(gvDetail.Rows(i).Cells(1).Value), clsCommon.myCstr(gvDetail.Rows(i + 1).Cells(1).Value)) = CompairStringResult.Equal Then
                            Else
                                If clsCommon.CompairString(clsCommon.myCstr(gvDetail.Rows(i).Cells(ic).Value), clsCommon.myCstr(gvDetail.Rows(i + 1).Cells(ic).Value)) = CompairStringResult.Equal Then
                                Else
                                    gvDetail.Rows(i + 1).Cells(ic).Style.BackColor = System.Drawing.Color.Yellow
                                    gvDetail.Rows(i + 1).Cells(ic).Style.ForeColor = Color.Red

                                End If
                            End If
                           
                        Next
                        i = i + 1
                    Next
                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        Try
            UnCheckAll()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        'If e.Column Is gv1.Columns(colSelect) Then
        '    HistoryDoubleClick(False)
        'End If
    End Sub

    Private Sub gv1_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles gv1.ValueChanging
        If Not isInsideLoadData Then
            If gv1.CurrentColumn Is gv1.Columns(colSelect) Then
                Dim strVersion As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Hist_Version").Value)
                If clsCommon.myLen(strVersion) > 0 AndAlso clsCommon.CompairString(strVersion, "99999") <> CompairStringResult.Equal Then
                    LoadDetailData(e.NewValue, strVersion)
                End If
            Else
                e.Cancel = True
            End If
        End If
       
    End Sub
    Sub FillAllDetailData()
        Dim Mainqry As String = ""
        Try
            Dim qry As String = clsDBFuncationality.getSingleValue("select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME='" & DetailTable + clsCommon.HistTablePostFix & "'")
            If clsCommon.myLen(qry) <= 0 Then
                Exit Sub
            End If
            Dim strMasterCodeColumn As String = ""
            Dim strDetailTransCodeHistColumn As String = ""
            Dim dtMasterCategory As DataTable = Nothing

            '' Sequence MasterTable column 
            Dim Masteryqry As String = ""
            Masteryqry = "  SELECT  c.name as Name "
            Masteryqry += " FROM " & objCommonVar.CurrDatabase & ".sys.tables t"
            Masteryqry += " INNER JOIN " & objCommonVar.CurrDatabase & ".sys.all_columns c "
            Masteryqry += "  ON t.object_id = c.object_id"
            Masteryqry += " INNER JOIN " & objCommonVar.CurrDatabase & ".sys.types ty "
            Masteryqry += "  ON c.system_type_id = ty.system_type_id"
            Masteryqry += " WHERE t.name = '" & DetailTable & "'"
            Masteryqry += " order by c.name asc"
            dtMasterCategory = clsDBFuncationality.GetDataTable(Masteryqry)

            If dtMasterCategory IsNot Nothing AndAlso dtMasterCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtMasterCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strMasterCodeColumn += ","
                        strDetailTransCodeHistColumn += ","
                    End If
                    strMasterCodeColumn += "" + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + ""
                    strDetailTransCodeHistColumn += "max(" & DetailTable + clsCommon.HistTablePostFix & "." + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + ") as " + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + ""
                Next
            End If
           
            Mainqry = " select CAST(0 as bit) as Sel, max(" & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistVersion & ") as [Head version],max(" & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistVersion & ") as " & clsCommon.HistTableColHistVersion & " ,max(" & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistBy & ") as " & clsCommon.HistTableColHistBy & " ,max(" & DetailTable + clsCommon.HistTablePostFix & "." & clsCommon.HistTableColHistOn & ") as " & clsCommon.HistTableColHistOn & "," & strDetailTransCodeHistColumn & " from " & DetailTable + clsCommon.HistTablePostFix & ""
            Mainqry += " where 2=2 and " & PrimaryKeyValue & "='" & code & "'  group by " & DetailTable + clsCommon.HistTablePostFix & ".Hist_Version  "
            If clsCommon.CompairString(HeadTable, "TSPL_ITEM_MASTER") = CompairStringResult.Equal Then
                Mainqry += "  ,Conversion_Factor "
            End If
            dtAllData = clsDBFuncationality.GetDataTable(Mainqry)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strVersion As String)
        IsInsideLoadDataOfItem = True
        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strVersion, clsCommon.myCstr(dr("Hist_Version"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strVersion, "99999") <> CompairStringResult.Equal Then
                    If clsCommon.myLen(strVersionNoSelect) <= 0 Then
                        strVersionNoSelect = "'" + strVersion + "'"
                    Else
                        strVersionNoSelect = strVersionNoSelect + ", " + "'" + strVersion + "'"
                    End If

                End If
            Next
            HistoryDoubleClick(False)
        Else
            For ii As Integer = 0 To gv1.Rows.Count - 1 Step 1
                If clsCommon.CompairString(strVersion, clsCommon.myCstr(gv1.Rows(ii).Cells("Hist_Version").Value)) = CompairStringResult.Equal Then
                    Try
                        'gvDetail.Rows.RemoveAt(ii)
                        If clsCommon.myLen(strVersionNoSelect) > 0 Then
                            Dim ddd As String = "'" + strVersion + "'"
                            strVersionNoSelect = strVersionNoSelect.Replace(ddd, "' ' ")
                        End If
                        HistoryDoubleClick(False)
                    Catch ex As Exception

                    End Try

                    'gv1.Rows.RemoveAt(ii)
                End If
            Next
        End If
        IsInsideLoadDataOfItem = False
    End Sub
End Class
