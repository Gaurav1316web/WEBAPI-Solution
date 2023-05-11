Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class frmSet_SegmentValue
    Dim ColumnCount As Integer
    Dim ColName As ArrayList
    Dim CurrentColName1 As String
    Public opratorValue As String
    Public ColValue As String
    Public Sub New(ByVal i As Integer, ByVal columnname As ArrayList, ByVal CurrentColName As String)
        ColumnCount = i
        ColName = columnname
        CurrentColName1 = CurrentColName
        InitializeComponent()
    End Sub

    Private Sub frmSet_SegmentValue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        For i As Integer = 0 To ColumnCount - 1
            If ColName(i) = "Seg_No" Then
                txtValue.MaxLength = 12
                DrpOprator.Items.Clear()
                DrpOprator.Items.Add("=")
                DrpOprator.Items.Add(">")
                DrpOprator.Items.Add("<")
                DrpOprator.Items.Add(">=")
                DrpOprator.Items.Add("<=")
                DrpOprator.Items.Add("!=")


            Else
                txtValue.MaxLength = 30
                DrpOprator.Items.Clear()
                DrpOprator.Items.Add("=")
                DrpOprator.Items.Add("!=")
                DrpOprator.Items.Add("LIKE")
                DrpOprator.Items.Add("Contains")
            End If
        Next
        DrpOprator.SelectedIndex = 0
        lblCurrrentColName.Text = CurrentColName1
        'Dim Qry As String = "select"
        Dim column As String = lblCurrrentColName.Text
        'Dim dt As New DataTable
        'Dim qry1 As String = Qry & " " & column & "  from TSPL_Additional_Charges"
        'dt = clsDBFuncationality.GetDataTable(qry1)
        'DrpColumnValue.DataSource = dt
        'DrpColumnValue.DisplayMember = column
        'DrpColumnValue.ValueMember = column
        'DrpColumnValue.SelectedValue = "Select"
        'txtValue.Text = DrpColumnValue.Text

    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        opratorValue = DrpOprator.Text
        ColValue = txtValue.Text
        Me.Close()
    End Sub

    Private Sub btnCLose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCLose.Click
        Me.Close()
    End Sub
    'Private Sub DrpColumnValue_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs)
    '    txtValue.Text = DrpColumnValue.Text.ToString
    'End Sub

End Class
