Imports common
Public Class FrmShowAllDrNote
    Public strAllRemitcode As String = ""
    Private Sub FrmShowAllDrNote_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim qry As String = "select Remittance_Code,Vendor_Code,Vendor_Name,Document_No,Document_Date,Actual_TDS from Tspl_remittance where Remittance_Code in (" + strAllRemitcode + ")"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gv1.DataSource = dt
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).Width = 100
        Next
        gv1.Columns("Remittance_Code").HeaderText = "Remitance Code"
        'gv1.Columns("Remittance_Code").Width = 100

        gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"
        'gv1.Columns("Vendor_Code").Width = 100

        gv1.Columns("Vendor_Name").HeaderText = "Vendor"
        gv1.Columns("Vendor_Name").Width = 200

        gv1.Columns("Document_No").HeaderText = "Document No"
        'gv1.Columns("Document_No").Width = 100

        gv1.Columns("Document_Date").HeaderText = "Document Date"
        'gv1.Columns("Document_Date").Width = 100

        gv1.Columns("Actual_TDS").HeaderText = "TDS Amount"
        'gv1.Columns("Actual_TDS").Width = 100
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub
End Class
