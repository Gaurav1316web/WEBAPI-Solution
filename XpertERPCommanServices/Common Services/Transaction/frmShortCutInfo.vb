Imports System.Data.SqlClient
Imports common
Public Class frmShortCutInfo
    Dim strQ As String
    Dim Ds As DataSet
    Private Sub frmShortCutInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub
    Private Sub LoadData()

        Dim dt As New DataTable()
        dt.Columns.Add("ShortCut Key", GetType(String))
        dt.Columns.Add("Description", GetType(String))
        Dim dr As DataRow = Nothing


        dr = dt.NewRow()
        dr("ShortCut Key") = "F1"
        dr("Description") = "User Manual"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("ShortCut Key") = "Ctrl+Shift+L"
        dr("Description") = "Label Changing"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("ShortCut Key") = "Alt+Ctrl+Shift+J"
        dr("Description") = "Journal Entry"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("ShortCut Key") = "Alt+Ctrl+Shift+A"
        dr("Description") = "AP Invoice Entry"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("ShortCut Key") = "Alt+Ctrl+Shift+B"
        dr("Description") = "AR Invoice Entry"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("ShortCut Key") = "Alt+Ctrl+Shift+F5"
        dr("Description") = "Control Description Mapping"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("ShortCut Key") = "Ctrl+F"
        dr("Description") = "PDF Page Size"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("ShortCut Key") = "Alt+T"
        dr("Description") = "Template"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("ShortCut Key") = "Alt+E"
        dr("Description") = "Export on grid"
        dt.Rows.Add(dr)

        grdLoginInfo.DataSource = dt

        grdLoginInfo.Columns(0).Width = 100
        grdLoginInfo.Columns(1).Width = 300
        For ii As Integer = 0 To grdLoginInfo.Columns.Count - 1
            grdLoginInfo.Columns(ii).ReadOnly = True
        Next
        grdLoginInfo.AllowAddNewRow = False
        grdLoginInfo.ShowGroupPanel = False
        grdLoginInfo.AllowColumnReorder = False
        grdLoginInfo.AllowRowReorder = False
        grdLoginInfo.EnableSorting = False
    End Sub
End Class
