Imports System.Data.SqlClient
Imports common

Public Class FrmItemSelector2
    Private Sub FrmItemSelector2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadItems()
    End Sub
    Public Arr As New List(Of String)
    Public UomCode As String

    Sub LoadItems()
        Try
            dgvItem.Rows.Clear()
            dgvItem.Columns.Clear()
            dgvItem.DataSource = Nothing
            Dim Qry As String = "select CAST(0 as BIT ) as [Select], TSPL_item_master.item_code as [Code], TSPL_item_master.item_desc as [Description]  from TSPL_item_master Left Outer Join TSPL_ITEM_UOM_DETAIL on TSPL_item_master.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code  Where Item_Type='F' AND UOM_Code='" + UomCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            dgvItem.DataSource = dt
            FormatGrid()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub FormatGrid()
        Me.dgvItem.MasterTemplate.Columns("Select").ReadOnly = False
        Me.dgvItem.MasterTemplate.Columns("Code").Width = 151
        Me.dgvItem.MasterTemplate.Columns("Code").ReadOnly = True
        Me.dgvItem.MasterTemplate.Columns("Description").Width = 351
        Me.dgvItem.MasterTemplate.Columns("Description").ReadOnly = True
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            Arr.Clear()
            Dim i As Integer
            For i = 0 To dgvItem.Rows.Count - 1
                If dgvItem.Rows(i).Cells("Select").Value = True Then
                    Dim TmplateId As String = clsCommon.myCstr(dgvItem.Rows(i).Cells("Code").Value)
                    Arr.Add(TmplateId)
                End If
            Next
            If (Arr Is Nothing OrElse Arr.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Item")
                Return
            Else
                Me.Close()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub


    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        If btnSelectAll.Text = "Select All" Then
            For i As Integer = 0 To dgvItem.Rows.Count - 1
                dgvItem.Rows(i).Cells("Select").Value = True
            Next
            btnSelectAll.Text = "UnSelect All"
        ElseIf btnSelectAll.Text = "UnSelect All" Then
            For i As Integer = 0 To dgvItem.Rows.Count - 1
                dgvItem.Rows(i).Cells("Select").Value = False
            Next
            btnSelectAll.Text = "Select All"
        End If

    End Sub


End Class
