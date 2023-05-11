Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports System.Configuration
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.IO
Imports System.Drawing
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports Telerik.WinControls.UI.Export

Public Class FrmExportSetCriteria
    Dim SetColName As String
    Dim operatorValue As String

    Private Sub FrmExportSetCriteria_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As New DataTable
        Dim qry As String = " select code,description from tspl_additional_charges "
        dt = clsDBFuncationality.GetDataTable(qry)
        Dim i As Integer
        For i = 0 To dt.Columns.Count - 1
            chkListOfColumn.Items.Add(dt.Columns(i).ToString())
        Next
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If dgvFilterColumn.Columns.Count > 0 Then
            Dim i As Integer = dgvFilterColumn.Columns.Count - 1
            dgvFilterColumn.Columns.RemoveAt(i)
            chkListOfColumn.SetItemChecked(i, False)
        End If
    End Sub
    Private Sub AddColumn(ByVal i As Integer, ByVal col As String)
        Dim AddCol As New GridViewTextBoxColumn

        With AddCol
            .HeaderText = col
            .Name = col
        End With
        dgvFilterColumn.Columns.Insert(i, AddCol)
        If dgvFilterColumn.Columns.Count > 0 Then
            For j As Integer = 0 To dgvFilterColumn.Columns.Count - 1
                dgvFilterColumn.Columns(j).ReadOnly = True
            Next

        End If

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim chk As CheckedListBox.CheckedItemCollection
        chk = chkListOfColumn.CheckedItems
        dgvFilterColumn.Columns.Clear()
        dgvFilterColumn.Rows.Clear()
        For i As Integer = 0 To chk.Count - 1
            AddColumn(i, chk(i))
        Next
    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Dim arr As New List(Of String)
        Dim str As String = " Select Code,Description "
        Dim Condition As String = ""
        Dim Finalstring As String = ""
        Dim isFirstTime As Boolean = True
        For i As Integer = 0 To dgvFilterColumn.Columns.Count - 1
            'If (Not isFirstTime) Then
            '    str += ","
            'End If
            'str += dgvFilterColumn.Columns(i).Name
            arr.Add(dgvFilterColumn.Columns(i).Name)
            isFirstTime = False
        Next

        str += " From TSPL_Additional_Charges where 2=2"
        isFirstTime = True
        Dim isAddAnd As Boolean = True
        For iiRow As Integer = 0 To dgvFilterColumn.Rows.Count - 1
            If Not isFirstTime Then
                str += " or "
                isAddAnd = False
            End If

            For iCol As Integer = 0 To arr.Count - 1
                If isAddAnd Then
                    str += " and "
                End If
                Dim col As String
                col = dgvFilterColumn.Rows(iiRow).Cells(arr(iCol)).Value
                Dim Colvalue As String = col.Substring(col.IndexOf(" "))
                Dim opratorvalue As String = col.Substring(0, col.IndexOf(" "))
                If opratorvalue = "LIKE" Then
                    str += arr(iCol) + " " + opratorvalue + "'" + clsCommon.myCstr(Colvalue) + "%'"
                    isAddAnd = True
                ElseIf opratorvalue = "Contains" Then
                    opratorvalue = "Like"
                    str += arr(iCol) + " " + opratorvalue + "'%" & clsCommon.myCstr(Colvalue) & "%' "
                    isAddAnd = True

                Else
                str += arr(iCol) + " " + opratorvalue + "'" + clsCommon.myCstr(Colvalue) + "'"
                isAddAnd = True
                End If

            Next
            isFirstTime = False
        Next
        Dim isAddAnd1 As Boolean = True
        'Thsi Code For Get the Value From Buffer Row OF DataGridview
        If dgvFilterColumn.CurrentRow.Index < 0 Then

            If Not isFirstTime Then
                str += " or "
                isAddAnd1 = False

            End If
            isFirstTime = True
            For iCol As Integer = 0 To arr.Count - 1
                If clsCommon.myCstr(dgvFilterColumn.CurrentRow.Cells(arr(iCol)).Value <> "") Then
                    If isAddAnd1 Then
                        str += " and "
                    End If
                    isFirstTime = False
                    Dim Col1 As String
                    Col1 = dgvFilterColumn.CurrentRow.Cells(arr(iCol)).Value
                    Dim ColValue As String = Col1.Substring(Col1.IndexOf(" "))
                    Dim Oprator As String = Col1.Substring(0, Col1.IndexOf(" "))
                    'Dim isStr As Boolean = True
                    'If Oprator = ">" Or Oprator = ">=" Or Oprator = "<" Or Oprator = "<=" Then
                    '    If isStr = True Then
                    '        str = ""
                    '    End If
                    '    str += " with CTE as (Select * ,ROW_NUMBER ()over(Partition by code order by code)as duplicateNum from Tspl_Additional_Charges)select * from CTE where  "
                    '    isStr = False
                    'End If
                    If Oprator = "LIKE" Then
                        str += arr(iCol) + " " + Oprator + "'" & clsCommon.myCstr(ColValue) & "%' "
                        isAddAnd1 = True
                    ElseIf Oprator = "Contains" Then
                        Oprator = "Like"
                        str += arr(iCol) + " " + Oprator + "'%" & clsCommon.myCstr(ColValue) & "%' "
                        isAddAnd1 = True

                    Else
                        str += arr(iCol) + " " + Oprator + "'" & clsCommon.myCstr(ColValue) & "'"
                        isAddAnd1 = True
                    End If

                End If
            Next
            isFirstTime = False
        End If

        transportSql.ExporttoExcel(str, Me)
        ''Dim str As String = " Select "
        ''Dim Condition As String = ""
        ''Dim Finalstring As String = ""
        ''For i As Integer = 0 To dgvFilterColumn.Columns.Count - 1
        ''    str += dgvFilterColumn.Columns(i).Name & ","
        ''    If dgvFilterColumn.CurrentRow.Cells(i).Value <> "" Then
        ''        If dgvFilterColumn.Columns(i).Name = "code" Or dgvFilterColumn.Columns(i).Name = "description" Then
        ''            Dim lngth As Integer = operatorValue.Length

        ''            Dim split12 As String = dgvFilterColumn.CurrentRow.Cells(i).Value.ToString.Substring(0, lngth)
        ''            Dim arr As New ArrayList()
        ''            arr.Add(dgvFilterColumn.CurrentRow.Cells(i).Value.ToString.Replace(split12, ""))
        ''            Condition += dgvFilterColumn.Columns(i).Name & " " & dgvFilterColumn.CurrentRow.Cells(i).Value.ToString.Substring(0, lngth) & "'" & arr(0) & "'" & " And" & " "
        ''        Else
        ''            Condition += dgvFilterColumn.Columns(i).Name & dgvFilterColumn.CurrentRow.Cells(i).Value & " And "
        ''        End If
        ''    End If
        ''Next
        ''str = str.TrimEnd(",")
        ''If Condition.Length > 0 Then
        ''    Condition = Condition.Substring(0, Condition.Length - 4)
        ''    Finalstring = str & "  From TSPL_Additional_Charges where " & Condition
        ''Else
        ''    Finalstring = str & " From TSPL_Additional_Charges "
        ''End If
        ''Dim qry As String = Finalstring
        ''transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvFilterColumn_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvFilterColumn.CellDoubleClick

        Dim arrlist As New ArrayList
        For i As Integer = 0 To dgvFilterColumn.Columns.Count - 1
            arrlist.Add(dgvFilterColumn.Columns(i).Name)
        Next
        Dim frm As New frmSetValue(dgvFilterColumn.Columns.Count, arrlist, dgvFilterColumn.CurrentColumn.Name)
        frm.ShowDialog()
        operatorValue = frm.opratorValue
        Dim value As String = frm.opratorValue + " " + frm.txtValue.Text
        SetColName = frm.lblCurrrentColName.Text
        dgvFilterColumn.CurrentRow.Cells(SetColName).Value = value
    End Sub
End Class
