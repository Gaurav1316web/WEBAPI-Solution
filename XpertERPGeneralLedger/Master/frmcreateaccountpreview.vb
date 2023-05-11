Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Excel = Microsoft.Office.Interop.Excel
Public Class Frmcreateaccountpreview
    Dim dr As SqlDataReader
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub Frmcreateaccountpreview_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim query As String = "Select distinct(case create_acct when'Y'then 'Yes' End) as [Create Account],account_code,Description,Type,[normal balance],account_grp,status,control_account,structure_desc,GL_Main_Code from tspl_gl_preview order by account_code "
        transportSql.FillGridView(query, rdaccountpreview)
        rdaccountpreview.Columns(0).Width = 40
        rdaccountpreview.Columns(0).ReadOnly = True
        rdaccountpreview.Columns(1).Width = 200
        rdaccountpreview.Columns(2).Width = 300
        rdaccountpreview.Columns(3).Width = 50
        rdaccountpreview.Columns(4).Width = 50
        rdaccountpreview.Columns(5).Width = 50
        rdaccountpreview.Columns(6).Width = 50
        rdaccountpreview.Columns(7).Width = 50
        rdaccountpreview.Columns("GL_Main_Code").Width = 100
        rdaccountpreview.Columns("GL_Main_Code").ReadOnly = True

    End Sub
    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Dim i As Integer = 0
        Dim query As String = ""
        Dim query1 As String = ""
        Dim str1 As String = ""
        query = "truncate table tspl_gl_preview"
        RunSql(query)
        rdaccountpreview.AllowAddNewRow = True
        For i = 0 To rdaccountpreview.Rows.Count - 1
            If rdaccountpreview.Rows(i).Cells(0).Value <> "No" Then
               
                If rdaccountpreview.Rows(i).Cells(0).Value = "Yes" Then
                    str1 = "Y"
                End If
                Dim desc As String = rdaccountpreview.Rows(i).Cells(2).Value
                query1 = "Insert into tspl_gl_preview(create_acct,account_code,description,type,[normal balance],account_grp,status,control_account,GL_Main_Code,structure_desc)values('" + str1 + "','" + rdaccountpreview.Rows(i).Cells(1).Value + "','" + Replace(desc, "'", "''") + "','" + rdaccountpreview.Rows(i).Cells(3).Value + "','" + rdaccountpreview.Rows(i).Cells(4).Value + "','" + rdaccountpreview.Rows(i).Cells(5).Value + "','" + rdaccountpreview.Rows(i).Cells(6).Value + "','" + rdaccountpreview.Rows(i).Cells(7).Value + "','" + common.clsCommon.myCstr(rdaccountpreview.Rows(i).Cells("GL_Main_Code").Value) + "','" + rdaccountpreview.Rows(i).Cells(8).Value + "')"
                RunSql(query1)
            End If
        Next
        Me.Close()

    End Sub

    Private Sub rdaccountpreview_CellClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles rdaccountpreview.CellClick
        If e.ColumnIndex = 0 And e.RowIndex >= 0 Then
            If rdaccountpreview.CurrentRow.Cells(0).Value = "Yes" Then
                rdaccountpreview.CurrentRow.Cells(0).Value = "No"
            Else
                rdaccountpreview.CurrentRow.Cells(0).Value = "Yes"
            End If
        End If
    End Sub

   
End Class
