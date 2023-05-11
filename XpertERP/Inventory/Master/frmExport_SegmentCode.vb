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

Public Class FrmExport_SegmentCode
    Dim SetColName As String
    Dim operatorValue As String
    Private Sub FrmExport_SegmentCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As New DataTable
        Dim qry As String = " select Seg_No,Segment_name,Segment_code,Description,Account_Code,GIT from TSPL_GL_SEGMENT_CODE "
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
        Dim str As String = " Select seg_no as 'Segment No',segment_name as 'Segment Name',segment_code as 'Segment Code',Description as 'Description',Account_code as 'Account Code',GIT "
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
        '' Anubhooti 15-Sep-2014 (Removed Where 2=2)
        str += " From TSPL_GL_SEGMENT_CODE "
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
                If arr(iCol) = "Seg_No" Then
                    str += arr(iCol) + " " + opratorvalue + clsCommon.myCstr(Colvalue)
                    isAddAnd = True
                Else
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
                    If arr(iCol) = "Seg_No" Then
                        str += arr(iCol) + " " + Oprator + clsCommon.myCstr(ColValue)
                        isAddAnd1 = True
                    Else
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
                End If

                    
            Next
            isFirstTime = False
        End If
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvFilterColumn_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvFilterColumn.CellDoubleClick

        Dim arrlist As New ArrayList
        For i As Integer = 0 To dgvFilterColumn.Columns.Count - 1
            arrlist.Add(dgvFilterColumn.Columns(i).Name)
        Next
        Dim frm As New frmSet_SegmentValue(dgvFilterColumn.Columns.Count, arrlist, dgvFilterColumn.CurrentColumn.Name)
        frm.ShowDialog()
        operatorValue = frm.opratorValue
        Dim value As String = frm.opratorValue + " " + frm.txtValue.Text
        SetColName = frm.lblCurrrentColName.Text
        dgvFilterColumn.CurrentRow.Cells(SetColName).Value = value
    End Sub
End Class
