Imports System.Data.SqlClient
Imports System.IO
Imports common
Imports System.Globalization
Imports System.Text.RegularExpressions
Public Class frmDemandUploader
    Inherits FrmMainTranScreen
#Region "Variable"
    Dim isInsideLoadData As Boolean = False

    Const colSNo As String = "colSNo"
    Const colQtyIn As String = "colQtyIn"
    Const colRoute As String = "colRoute"
    Const colBooth As String = "colBooth"
    Const colTM500 As String = "colTM500"
    Const colTM1LT As String = "colTM1LT"
    Const colSM500 As String = "colSM500"
    Const colSM1LT As String = "colSM1LT"
    Const colGM500 As String = "colGM500"
    Const colGM1LT As String = "colGM1LT"
    Const colCHHACH As String = "colCHHACH"
    Const colTM6LT As String = "colTM6LT"
    Const colGM6LT As String = "colGM6LT"
    Const colSL400 As String = "colSL400"
    Const colSL6LT As String = "colSL6LT"
    Const colTotalAmount As String = "colTotalAmount"


#End Region
    Private Sub frmDemandUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        AddNew()

    End Sub
    Public Sub AddNew()
        txtDate.Value = clsCommon.GETSERVERDATE()
        rbtnMorning.IsChecked = True
        LoadBlankGrid()

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Public Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()


        gv1.DataSource = Nothing
        gv1.Rows.AddNew()

        Dim repoSNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNo = New GridViewTextBoxColumn()
        repoSNo.FormatString = ""
        repoSNo.HeaderText = "S.No."
        repoSNo.Name = colSNo
        repoSNo.Width = 50
        repoSNo.ReadOnly = True
        repoSNo.IsPinned = True
        repoSNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSNo)
        Dim repoQtyIn As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQtyIn = New GridViewTextBoxColumn()
        repoQtyIn.FormatString = ""
        repoQtyIn.HeaderText = "Qty In"
        repoQtyIn.Name = colQtyIn
        repoQtyIn.Width = 50
        repoQtyIn.ReadOnly = True
        repoQtyIn.IsPinned = True
        repoQtyIn.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQtyIn)
        Dim repoRoute As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRoute = New GridViewTextBoxColumn()
        repoRoute.FormatString = ""
        repoRoute.HeaderText = "Route"
        repoRoute.Name = colRoute
        repoRoute.Width = 50
        repoRoute.ReadOnly = True
        repoRoute.IsPinned = True
        repoRoute.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRoute)
        Dim repoBooth As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBooth = New GridViewTextBoxColumn()
        repoBooth.FormatString = ""
        repoBooth.HeaderText = "Booth"
        repoBooth.Name = colBooth
        repoBooth.Width = 50
        repoBooth.ReadOnly = True
        repoBooth.IsPinned = True
        repoBooth.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBooth)

        Dim repoTM500 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTM500 = New GridViewDecimalColumn()
        repoTM500.FormatString = ""
        repoTM500.HeaderText = "TM 500"
        repoTM500.Name = colTM500
        repoTM500.Width = 50
        repoTM500.ReadOnly = True
        repoTM500.IsPinned = True
        repoTM500.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTM500)
        Dim repoTM1LT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTM1LT = New GridViewDecimalColumn()
        repoTM1LT.FormatString = ""
        repoTM1LT.HeaderText = "TM 1LT"
        repoTM1LT.Name = colTM1LT
        repoTM1LT.Width = 50
        repoTM1LT.ReadOnly = True
        repoTM1LT.IsPinned = True
        repoTM1LT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTM1LT)
        Dim repoSM500 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSM500 = New GridViewDecimalColumn()
        repoSM500.FormatString = ""
        repoSM500.HeaderText = "SM 500"
        repoSM500.Name = colSM500
        repoSM500.Width = 50
        repoSM500.ReadOnly = True
        repoSM500.IsPinned = True
        repoSM500.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSM500)
        Dim repoSM1LT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSM1LT = New GridViewDecimalColumn()
        repoSM1LT.FormatString = ""
        repoSM1LT.HeaderText = "SM 1LT"
        repoSM1LT.Name = colSM1LT
        repoSM1LT.Width = 50
        repoSM1LT.ReadOnly = True
        repoSM1LT.IsPinned = True
        repoSM1LT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSM1LT)
        Dim repoGM500 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGM500 = New GridViewDecimalColumn()
        repoGM500.FormatString = ""
        repoGM500.HeaderText = "GM 500"
        repoGM500.Name = colGM500
        repoGM500.Width = 50
        repoGM500.ReadOnly = True
        repoGM500.IsPinned = True
        repoGM500.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGM500)
        Dim repoGM1LT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGM1LT = New GridViewDecimalColumn()
        repoGM1LT.FormatString = ""
        repoGM1LT.HeaderText = "GM 1LT"
        repoGM1LT.Name = colGM1LT
        repoGM1LT.Width = 50
        repoGM1LT.ReadOnly = True
        repoGM1LT.IsPinned = True
        repoGM1LT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGM1LT)
        Dim repoChhach As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoChhach = New GridViewDecimalColumn()
        repoChhach.FormatString = ""
        repoChhach.HeaderText = "CHHACH"
        repoChhach.Name = colCHHACH
        repoChhach.Width = 50
        repoChhach.ReadOnly = True
        repoChhach.IsPinned = True
        repoChhach.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoChhach)
        Dim repoTM6LT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTM6LT = New GridViewDecimalColumn()
        repoTM6LT.FormatString = ""
        repoTM6LT.HeaderText = "TM 6LT"
        repoTM6LT.Name = colTM6LT
        repoTM6LT.Width = 50
        repoTM6LT.ReadOnly = True
        repoTM6LT.IsPinned = True
        repoTM6LT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTM6LT)
        Dim repoGM6LT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGM6LT = New GridViewDecimalColumn()
        repoGM6LT.FormatString = ""
        repoGM6LT.HeaderText = "GM 6LT"
        repoGM6LT.Name = colGM6LT
        repoGM6LT.Width = 50
        repoGM6LT.ReadOnly = True
        repoGM6LT.IsPinned = True
        repoGM6LT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoGM6LT)
        Dim repoSL400 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSL400 = New GridViewDecimalColumn()
        repoSL400.FormatString = ""
        repoSL400.HeaderText = "SL 400"
        repoSL400.Name = colSL400
        repoSL400.Width = 50
        repoSL400.ReadOnly = True
        repoSL400.IsPinned = True
        repoSL400.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSL400)
        Dim repoSL6LT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSL6LT = New GridViewDecimalColumn()
        repoSL6LT.FormatString = ""
        repoSL6LT.HeaderText = "SL6 LT"
        repoSL6LT.Name = colSL6LT
        repoSL6LT.Width = 50
        repoSL6LT.ReadOnly = True
        repoSL6LT.IsPinned = True
        repoSL6LT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSL6LT)
        Dim repototamt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repototamt = New GridViewDecimalColumn()
        repototamt.FormatString = ""
        repototamt.HeaderText = "Total Amount"
        repototamt.Name = colTotalAmount
        repototamt.Width = 50
        repototamt.ReadOnly = True
        repototamt.IsPinned = True
        repototamt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repototamt)
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40


    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            Import()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Import()
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim obj As New List(Of clsDemandUploader)
            Dim currentdate As Date = Date.Today

            If transportSql.importExcel(gv, "S.No.", "Qty In", "Route", "Booth", "TM 500", "TM 1LT", "SM 500", "SM 1LT", "GM 500", "GM 1LT", "CHHACH", "TM 6LT", "GM 6LT", "SL400", "SL6 LT", "Total Amount") Then

                Dim linno As Integer = 0
                Dim TempNewRecord As Boolean = False
                Try

                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim Arr As New clsDemandUploader()
                        linno += 1
                        Arr.Sno = clsCommon.myCstr(grow.Cells("S.No.").Value)
                        Arr.Qty_In = clsCommon.myCstr(grow.Cells("Qty In").Value)
                        Arr.Route = clsCommon.myCstr(grow.Cells("Route").Value)
                        Arr.Booth = clsCommon.myCstr(grow.Cells("Booth").Value)
                        Arr.TM500 = clsCommon.myCdbl(grow.Cells("TM 500").Value)
                        Arr.TM1LT = clsCommon.myCdbl(grow.Cells("TM 1LT").Value)
                        Arr.SM500 = clsCommon.myCdbl(grow.Cells("SM 500").Value)
                        Arr.SM1LT = clsCommon.myCdbl(grow.Cells("SM 1LT").Value)
                        Arr.GM500 = clsCommon.myCdbl(grow.Cells("GM 500").Value)
                        Arr.GM1LT = clsCommon.myCdbl(grow.Cells("GM 1LT").Value)
                        Arr.CHHACH = clsCommon.myCdbl(grow.Cells("CHHACH").Value)
                        Arr.TM6LT = clsCommon.myCdbl(grow.Cells("TM 6LT").Value)
                        Arr.GM6LT = clsCommon.myCdbl(grow.Cells("GM 6LT").Value)
                        Arr.SL400 = clsCommon.myCdbl(grow.Cells("SL400").Value)
                        Arr.SL6LT = clsCommon.myCdbl(grow.Cells("SL6 LT").Value)
                        Arr.TotalAmount = clsCommon.myCdbl(grow.Cells("Total Amount").Value)

                        obj.Add(Arr)
                    Next
                    clsCommon.ProgressBarHide()


                    If obj IsNot Nothing AndAlso obj.Count > 0 Then
                        Dim introw As Integer = 0
                        LoadBlankGrid()
                        For Each item As clsDemandUploader In obj

                            gv1.Rows(introw).Cells(colSNo).Value = item.Sno
                            gv1.Rows(introw).Cells(colQtyIn).Value = item.Qty_In
                            gv1.Rows(introw).Cells(colRoute).Value = item.Route
                            gv1.Rows(introw).Cells(colBooth).Value = item.Booth
                            gv1.Rows(introw).Cells(colTM500).Value = item.TM500
                            gv1.Rows(introw).Cells(colTM1LT).Value = item.TM1LT
                            gv1.Rows(introw).Cells(colSM500).Value = item.SM500
                            gv1.Rows(introw).Cells(colSM1LT).Value = item.SM1LT
                            gv1.Rows(introw).Cells(colGM500).Value = item.GM500
                            gv1.Rows(introw).Cells(colGM1LT).Value = item.GM1LT
                            gv1.Rows(introw).Cells(colCHHACH).Value = item.CHHACH
                            gv1.Rows(introw).Cells(colTM6LT).Value = item.TM6LT
                            gv1.Rows(introw).Cells(colGM6LT).Value = item.GM6LT
                            gv1.Rows(introw).Cells(colSL400).Value = item.SL400
                            gv1.Rows(introw).Cells(colSL6LT).Value = item.SL6LT
                            gv1.Rows(introw).Cells(colTotalAmount).Value = item.TotalAmount
                            gv1.Rows.AddNew()
                            introw += 1
                        Next
                    End If
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    clsCommon.ProgressBarHide()
                Catch ex As Exception
                    clsCommon.ProgressBarHide()
                    Throw New Exception(ex.Message)
                End Try
            Else
                Throw New Exception("Excel Sheet is not in expected format")

            End If

            Me.Controls.Remove(gv)
        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        AddNew()
    End Sub

    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        Try
            ValidateGrid()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub ValidateGrid()
        Try
            Dim mess As String = ""
            Dim lineNo As Integer = 1
            For Each grow As GridViewRowInfo In gv1.Rows
                If (Not String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(colSNo).Value))) Then
                    If (ValidateBooth(grow.Cells(colRoute).Value, grow.Cells(colBooth).Value)) Then

                    Else
                        mess += " Error at Line No:" + clsCommon.myCstr(lineNo) + " Route:" + clsCommon.myCstr(grow.Cells(colRoute).Value) + " Booth:" + clsCommon.myCstr(grow.Cells(colRoute).Value) + " " & Environment.NewLine

                    End If
                    lineNo += 1
                End If

            Next
            If Not String.IsNullOrEmpty(mess) Then
                Throw New Exception(mess)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try

    End Sub
    Public Function ValidateBooth(ByVal Route As String, ByVal BoothCode As String) As Boolean
        Try
            Dim count As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Cust_Code) from TSPL_CUSTOMER_MASTER where Cust_Code='" + BoothCode + "' and Route_No='" + Route + "' and Status='N'"))
            If count = 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class