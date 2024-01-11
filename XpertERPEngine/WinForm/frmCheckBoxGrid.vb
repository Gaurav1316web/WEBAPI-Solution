Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls.UI

Public Class FrmCheckBoxGrid
#Region "variables"
    Public btnOkClicked As Boolean = False
    Public Const colSelect As String = "colSelect"
    Public Const colValue As String = "SRNNO"
    Public IsForDate As Boolean = False
    Public DateType_Daily_Monthly_Weekly As String = Nothing 'M->Monthly,D->Daily,W->Weekly
    Public arrValue As List(Of String) = Nothing
    Public qry As String = String.Empty
    Public trans As SqlTransaction = Nothing
#End Region

    Private Sub FrmCheckBoxGrid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.F5 Then
            btnOkPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub

    Sub btnCancelPressed()
        btnOkClicked = False
        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub btnOkPressed()
        arrValue = New List(Of String)

        If IsForDate Then
            arrValue.Add(clsCommon.myCstr(dtpMonth.Text))

            If arrValue.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select date", Me.Text)
                Exit Sub
            End If
        Else
            For i As Integer = 0 To gv.Rows.Count - 1
                If gv.Rows(i).Cells(colSelect).Value = True Then
                    arrValue.Add(gv.Rows(i).Cells(colValue).Value)
                End If
            Next
            If arrValue.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Value selected" & Environment.NewLine & "Please select atleast one Value")
                Exit Sub
            End If
        End If

        btnOkClicked = True
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub FrmCheckBoxGrid_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Select Values "
        SplitContainer2.Panel2Collapsed = True
        SplitContainer2.Panel1Collapsed = False
        Me.Width = 391
        Me.Height = 328
        If IsForDate Then
            SplitContainer2.Panel2Collapsed = False
            SplitContainer2.Panel1Collapsed = True
            Me.Width = 391
            Me.Height = 112
            Me.Text = "Select Date "

            If clsCommon.CompairString(DateType_Daily_Monthly_Weekly, "M") = CompairStringResult.Equal Then ''month
                MyLabel1.Text = "Year"
            Else
                MyLabel1.Text = "Month"
            End If
        Else
            LoadGrid()
        End If
        dtpMonth.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "MMM yyyy")
    End Sub

    Sub LoadGrid()
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(qry) > 0 Then
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                loadBlankGrid()
                For i As Integer = 0 To dt.Rows.Count - 1
                    gv.Rows.AddNew()
                    gv.Rows(gv.Rows.Count - 1).Cells(colValue).Value = dt.Rows(i)("Value")
                    gv.Rows(gv.Rows.Count - 1).Cells(colSelect).Value = checkSelected(dt.Rows(i)("Value"))
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No Value Found", Me.Text)
                btnOkClicked = False
                Me.Close()
            End If
        End If
    End Sub

    Function checkSelected(ByVal strValue) As Boolean
        If arrValue IsNot Nothing AndAlso arrValue.Count > 0 Then
            For i As Integer = 0 To arrValue.Count - 1
                If clsCommon.CompairString(arrValue(i), strValue) = CompairStringResult.Equal Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Sub loadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = "Select "
        repoSelect.Name = colSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 100
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Value"
        repoCode.Name = colValue
        repoCode.Width = 280
        repoCode.ReadOnly = True

        gv.MasterTemplate.Columns.Add(repoCode)
        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.MasterTemplate.ShowColumnHeaders = True
        gv.EnableAlternatingRowColor = True
        gv.EnableFiltering = True
        gv.ShowFilteringRow = True
        gv.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        btnOkPressed()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancelPressed()
    End Sub

    Private Sub gv_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOkPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub

End Class
