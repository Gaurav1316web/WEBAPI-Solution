Imports common
Imports System.Windows.Forms

Public Class Excelform
    Inherits FrmMainTranScreen

    Public comboDataSource As DataTable
    Public collReturn As Dictionary(Of String, String)
    Const colSno As String = "colSNO"
    Const colValue As String = "colValue"
    Const colMatching As String = "colMatching"
    Private Sub Cancel()
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Cancel()
    End Sub

    Private Sub OkPressed()
        Try
            ''Check all value in matching column selected

            Dim matchingValues As New HashSet(Of String)()
            For ii As Integer = 0 To gvExcel.Rows.Count - 1
                Dim value As String = clsCommon.myCstr(gvExcel.Rows(ii).Cells(colMatching).Value)
                If String.IsNullOrEmpty(value) Then
                    Throw New Exception("Value is not filled in every row.")
                End If

                If matchingValues.Contains(value) Then
                    Throw New Exception("Duplicate Value Found.")
                End If

                matchingValues.Add(value)
            Next

            ''No Repeat

            collReturn = New Dictionary(Of String, String)
            For ii As Integer = 0 To gvExcel.Rows.Count - 1
                collReturn.Add(clsCommon.myCstr(gvExcel.Rows(ii).Cells(colValue).Value), clsCommon.myCstr(gvExcel.Rows(ii).Cells(colMatching).Value))
            Next
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        OkPressed()
    End Sub

    Sub ExcelGrid()
        gvExcel.Rows.Clear()
        gvExcel.Columns.Clear()
        gvExcel.DataSource = Nothing

        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "SNo"
        lineNo.Name = colSno
        lineNo.Width = 60
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvExcel.Columns.Add(lineNo)


        Dim farmercode As New GridViewTextBoxColumn()
        farmercode.FormatString = ""
        farmercode.HeaderText = "Value Column"
        farmercode.Name = colValue
        farmercode.Width = 100
        farmercode.ReadOnly = True
        farmercode.IsVisible = True
        farmercode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvExcel.Columns.Add(farmercode)


        Dim repoComboBox As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoComboBox.FormatString = ""
        repoComboBox.HeaderText = "Matching Column"
        repoComboBox.Name = colMatching
        repoComboBox.Width = 150
        repoComboBox.ReadOnly = False
        repoComboBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoComboBox.DataSource = comboDataSource
        repoComboBox.ValueMember = "Code"
        repoComboBox.DisplayMember = "Code"
        repoComboBox.IsVisible = True
        gvExcel.MasterTemplate.Columns.Add(repoComboBox)


        gvExcel.AllowAddNewRow = False
        gvExcel.AllowDeleteRow = False
        gvExcel.AllowRowReorder = False
        gvExcel.ShowGroupPanel = False
        gvExcel.EnableFiltering = False
        gvExcel.ShowFilteringRow = True
        gvExcel.EnableSorting = False
        gvExcel.EnableGrouping = False
        gvExcel.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom

        ''Create all five row

        gvExcel.Rows.AddNew()
        gvExcel.Rows(0).Cells(colSno).Value = 1
        gvExcel.Rows(0).Cells(colValue).Value = "DCS Code"

        gvExcel.Rows.AddNew()
        gvExcel.Rows(1).Cells(colSno).Value = 2
        gvExcel.Rows(1).Cells(colValue).Value = "Year"

        gvExcel.Rows.AddNew()
        gvExcel.Rows(2).Cells(colSno).Value = 3
        gvExcel.Rows(2).Cells(colValue).Value = "Month"

        gvExcel.Rows.AddNew()
        gvExcel.Rows(3).Cells(colSno).Value = 4
        gvExcel.Rows(3).Cells(colValue).Value = "Cycle No"

        gvExcel.Rows.AddNew()
        gvExcel.Rows(4).Cells(colSno).Value = 5
        gvExcel.Rows(4).Cells(colValue).Value = "Qty"


    End Sub

    Private Sub Excelform_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ExcelGrid()
    End Sub

    'Private Sub importExcel()
    '    'UcAttachment1.BlankAllControls()
    '    Dim dgv As New RadGridView
    '    Me.Controls.Add(dgv)
    '    Dim ii As Integer = 0

    '    'loadBlankGrid()
    '    Dim FileName As String = ""
    '    Dim SafeFileName As String = ""
    '    If transportSql.importExcel(FileName, SafeFileName, dgv, "DCS Code", "Year", "Month", "Cycle No", "Qty") Then
    '        'UcAttachment1.AddAttachment(FileName, SafeFileName)

    '        gvExcel.Columns.Add("DCS Code")
    '        gvExcel.Columns.Add("Year")
    '        gvExcel.Columns.Add("Month")
    '        gvExcel.Columns.Add("Cycle No")
    '        gvExcel.Columns.Add("Qty")




    '    End If

    '    common.clsCommon.MyMessageBoxShow("Imported Successfully", Me.Text)
    'End Sub

    'Private Sub btn_Import_Click(sender As Object, e As EventArgs)
    '    importExcel()
    'End Sub

End Class