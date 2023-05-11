Imports common
Imports System.Data.SqlClient

Public Class FrmTemplateExport3
    Dim Arr As New List(Of String)
    Private Sub FrmTemplateExport3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadTemplates()
    End Sub

    Sub LoadTemplates()
        Try
            dgvTemplate.Rows.Clear()
            dgvTemplate.Columns.Clear()
            dgvTemplate.DataSource = Nothing
            Dim Qry As String = "Select Distinct CAST(0 as BIT ) as [Select], Tmplate_Id as Code, Description  from TSPL_CUSTOMER_TEMPLATE_MASTER "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            dgvTemplate.DataSource = dt
            FormatGrid()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub FormatGrid()
        Me.dgvTemplate.MasterTemplate.Columns("Select").ReadOnly = False
        Me.dgvTemplate.MasterTemplate.Columns("Code").Width = 171
        Me.dgvTemplate.MasterTemplate.Columns("Code").ReadOnly = True
        Me.dgvTemplate.MasterTemplate.Columns("Description").Width = 411
        Me.dgvTemplate.MasterTemplate.Columns("Description").ReadOnly = True
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Arr.Clear()
            Dim i As Integer
            For i = 0 To dgvTemplate.Rows.Count - 1
                If dgvTemplate.Rows(i).Cells("Select").Value = True Then
                    Dim TmplateId As String = clsCommon.myCstr(dgvTemplate.Rows(i).Cells("Code").Value)
                    Arr.Add(TmplateId)
                End If
            Next
            If (Arr Is Nothing OrElse Arr.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Template")
                Return
            Else
                ExportToExcel(Arr)
                Me.Close()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub ExportToExcel(ByVal Arr As List(Of String))
        Dim Qry As String = "select Tmplate_Id AS [Template Id], Description, REPLACE( Convert(varchar(11) ,Start_Date,102),'.','-') as [Start Date], Cust_Id as [Customer Id] , Created_By as [Created By], Created_Date as [Created Date], Modified_By as [Modify By], Modified_Date as [Modified date], Comp_Code as [Company]    from TSPL_CUSTOMER_TEMPLATE_MASTER "
        Dim WhrCls As String = " and Tmplate_Id in (" + clsCommon.GetMulcallString(Arr) + ") "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found To Export")
        Else
            transportSql.ExporttoExcel(Qry, WhrCls, Me)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

   
    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        If btnSelectAll.Text = "Select All" Then
            For i As Integer = 0 To dgvTemplate.Rows.Count - 1
                dgvTemplate.Rows(i).Cells("Select").Value = True
            Next
            btnSelectAll.Text = "UnSelect All"
        ElseIf btnSelectAll.Text = "UnSelect All" Then
            For i As Integer = 0 To dgvTemplate.Rows.Count - 1
                dgvTemplate.Rows(i).Cells("Select").Value = False
            Next
            btnSelectAll.Text = "Select All"
        End If
        
    End Sub
End Class
