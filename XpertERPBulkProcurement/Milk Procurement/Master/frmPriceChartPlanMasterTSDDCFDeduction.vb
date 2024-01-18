Imports common
Imports System.IO
Imports System.ComponentModel
Imports Telerik.WinControls.UI.Export

Public Class frmPriceChartPlanMasterTSDDCFDeduction
#Region "Variables"
    Public Const colSNFPer As String = "colSNFPer"
    Public Const colDed As String = "colDed"

    Public SNFFrom As Decimal
    Public SNFTo As Decimal
    Public ArrDed As New Dictionary(Of Decimal, Decimal)
    Public isOK As Boolean = False
#End Region

    Private Sub FrmFreeGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        Dim flag As Boolean = False
        For ii As Decimal = SNFFrom To SNFTo Step 0.1
            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPer).Value = ii
            If ArrDed IsNot Nothing Then
                If ArrDed.ContainsKey(ii) Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDed).Value = Math.Abs(ArrDed(ii))
                    If Not flag Then
                        flag = True
                        If ArrDed(ii) < 0 Then
                            rbtnDeduction.IsChecked = True
                        Else
                            rbtnAddition.IsChecked = True
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Sub LoadBlankGrid()
        Try
            gv1.Rows.Clear()
            gv1.Columns.Clear()

            Dim repoDeciCol As GridViewDecimalColumn
            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colSNFPer
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 0
            repoDeciCol.Minimum = 0
            repoDeciCol.Maximum = 15
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "SNF %"
            repoDeciCol.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colDed
            repoDeciCol.Width = 100
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            'repoDeciCol.Maximum = 15
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Deduction"
            gv1.MasterTemplate.Columns.Add(repoDeciCol)

            gv1.AllowDeleteRow = True
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.TableElement.TableHeaderHeight = 40
            gv1.AutoSizeRows = False
            gv1.AllowRowReorder = True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        isOK = False
        Me.Close()
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        isOK = True
        ArrDed = New Dictionary(Of Decimal, Decimal)
        For ii As Integer = 0 To gv1.Rows.Count - 1
            ArrDed.Add(clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNFPer).Value), (IIf(rbtnDeduction.IsChecked, -1, 1)) * clsCommon.myCdbl(gv1.Rows(ii).Cells(colDed).Value))
        Next
        Me.Close()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Try
            If txtStart.Value < 0 Then
                txtStart.Focus()
                Throw New Exception("Value Should be +ver")
            End If

            If txtValue.Value < 0 Then
                txtValue.Focus()
                Throw New Exception("Value Should be +ver")
            End If
            Dim Value As Decimal = txtStart.Value
            If rbtnSame.IsChecked Then
                Value = txtValue.Value
            End If
            If rbtnBottomToTop.IsChecked Then
                For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                    gv1.Rows(ii).Cells(colDed).Value = Value
                    Value += txtValue.Value
                Next
            Else
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    gv1.Rows(ii).Cells(colDed).Value = Value
                    If Not rbtnSame.IsChecked Then
                        Value += txtValue.Value
                    End If
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
