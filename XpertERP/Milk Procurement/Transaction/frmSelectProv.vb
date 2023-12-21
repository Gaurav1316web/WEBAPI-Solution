''richa agarwal 12/06/2015 AGAINST TICKET NO. BM00000007008 (Add Select All and Unselect All button and its working)
Imports common
Imports System.Data.SqlClient
Public Class FrmSelectProv
    Public btnOkClicked As Boolean = False
    Public Const colSelect As String = "colSelect"
    Public Const colDocNo As String = "colDocNo"
    Public Const colDocDATE As String = "colDocDATE"
    Public Const colVendorCode As String = "colVendorCode"
    Public Const colVendorDesc As String = "colVendorDesc"
    Public Const colVendorType As String = "colVendorType"
    Public Const colStatus As String = "colStatus"
    Public Const colRefDocNo As String = "colRefDocNo"
    Public Const colProvType As String = "colProvType"
    Public Const colAmount As String = "colAmount"
    Public Const colLocationCode As String = "colLocationCode"
    Public Const colLocationDesc As String = "colLocationDesc"
    Public Const colProvMonth As String = "colProvMonth"
    Public Const colProvYear As String = "colProvYear"
    Public ProvAmount As Double = 0
    Public qry As String = String.Empty
    Public arrProvDocNo As List(Of String) = Nothing
    Public arrInvoiceNo As List(Of String) = Nothing

    Private Sub FrmSelectProv_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOkPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub
    Sub btnCancelPressed()
        btnOkClicked = False
        Me.Close()
    End Sub
    Sub btnOkPressed()
        Try
            arrProvDocNo = New List(Of String)
            arrInvoiceNo = New List(Of String)
            ProvAmount = 0
            For i As Integer = 0 To gv.Rows.Count - 1

                If gv.Rows(i).Cells(colSelect).Value = True Then
                    arrProvDocNo.Add(clsCommon.myCstr(gv.Rows(i).Cells(colDocNo).Value))
                    arrInvoiceNo.Add(clsCommon.myCstr(gv.Rows(i).Cells(colRefDocNo).Value))
                    ProvAmount = ProvAmount + clsCommon.myCdbl(gv.Rows(i).Cells(colAmount).Value)
                End If
            Next
            If ProvAmount <= 0 Then
                clsCommon.MyMessageBoxShow("No Provision selected" & Environment.NewLine & "Please select atleast one Provision")
                Exit Sub
            End If
            btnOkClicked = True
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmSelectProv_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Select Provision "
        LoadGrid()
    End Sub
    Sub LoadGrid()
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(qry) > 0 Then
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                loadBlankGrid()
                For i As Integer = 0 To dt.Rows.Count - 1
                    gv.Rows.AddNew()
                    gv.Rows(gv.Rows.Count - 1).Cells(colSelect).Value = True
                    gv.Rows(gv.Rows.Count - 1).Cells(colDocNo).Value = clsCommon.myCstr(dt.Rows(i)("Doc_No"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colDocDATE).Value = clsCommon.myCDate(dt.Rows(i)("Doc_Date"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colVendorCode).Value = clsCommon.myCstr(dt.Rows(i)("Vendor_Code"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colVendorDesc).Value = clsCommon.myCstr(dt.Rows(i)("Vendor_Desc"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colVendorType).Value = clsCommon.myCstr(dt.Rows(i)("Vendor_Type"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colRefDocNo).Value = clsCommon.myCstr(dt.Rows(i)("Ref_Doc_No"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colProvType).Value = clsCommon.myCstr(dt.Rows(i)("Prov_type"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colStatus).Value = clsCommon.myCstr(dt.Rows(i)("Status"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colLocationCode).Value = clsCommon.myCstr(dt.Rows(i)("Loc_code"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colLocationDesc).Value = clsCommon.myCstr(dt.Rows(i)("Loc_Desc"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colProvMonth).Value = clsCommon.myCstr(dt.Rows(i)("Prov_Month"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colProvYear).Value = clsCommon.myCstr(dt.Rows(i)("Prov_Year"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colAmount).Value = clsCommon.myCdbl(dt.Rows(i)("Amount"))

                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No Provision Found", Me.Text)
                btnOkClicked = False
                Me.Close()
            End If
        End If
    End Sub
    Sub loadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Document No"
        repoCode.Name = colDocNo
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoCode)

        Dim repoSRNDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoSRNDate.FormatString = ""
        repoSRNDate.HeaderText = "Document Date"
        repoSRNDate.Name = colDocDATE
        repoSRNDate.Width = 180
        repoSRNDate.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSRNDate)


        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Vendor Code"
        repoICode.Name = colVendorCode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Vendor Desc"
        repoIName.Name = colVendorDesc
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIName)


        Dim repoVCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVCode.FormatString = ""
        repoVCode.HeaderText = "Vendor Type"
        repoVCode.Name = colVendorType
        repoVCode.Width = 100
        repoVCode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoVCode)

        Dim repoVName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVName.FormatString = ""
        repoVName.HeaderText = "Status"
        repoVName.Name = colStatus
        repoVName.Width = 180
        repoVName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoVName)



        Dim repoLCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLCode.FormatString = ""
        repoLCode.HeaderText = "Location Code"
        repoLCode.Name = colLocationCode
        repoLCode.Width = 100
        repoLCode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoLCode)

        Dim repoLName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLName.FormatString = ""
        repoLName.HeaderText = "Location Desc"
        repoLName.Name = colLocationDesc
        repoLName.Width = 180
        repoLName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoLName)

        Dim repoOrderQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "Ref Doc No"
        repoOrderQty.Name = colRefDocNo
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Provision Type"
        repoUnit.Name = colProvType
        repoUnit.Width = 60
        repoUnit.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoUnit)

        Dim repoInvoiceQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoInvoiceQty.FormatString = ""
        repoInvoiceQty.HeaderText = "Amount"
        repoInvoiceQty.Name = colAmount
        repoInvoiceQty.ReadOnly = True
        repoInvoiceQty.Width = 100
        repoInvoiceQty.WrapText = True
        repoInvoiceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoInvoiceQty)


        Dim repoRemainingQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemainingQty.FormatString = ""
        repoRemainingQty.HeaderText = "provision Month"
        repoRemainingQty.Name = colProvMonth
        repoRemainingQty.ReadOnly = True
        repoRemainingQty.Width = 100
        repoRemainingQty.WrapText = True
        repoRemainingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoRemainingQty)

        repoRemainingQty = New GridViewTextBoxColumn()
        repoRemainingQty.FormatString = ""
        repoRemainingQty.HeaderText = "provision Year"
        repoRemainingQty.Name = colProvYear
        repoRemainingQty.ReadOnly = True
        repoRemainingQty.Width = 100
        repoRemainingQty.WrapText = True
        repoRemainingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoRemainingQty)


        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = True
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
    ''richa agarwal 12/06/2015
    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        If gv.Rows.Count > 0 Then
            For i As Integer = 0 To gv.Rows.Count - 1
                gv.Rows(i).Cells(colSelect).Value = True
            Next
        End If
    End Sub

    Private Sub btnUnSelectAll_Click(sender As Object, e As EventArgs) Handles btnUnSelectAll.Click
        If gv.Rows.Count > 0 Then
            For i As Integer = 0 To gv.Rows.Count - 1
                gv.Rows(i).Cells(colSelect).Value = False
            Next
        End If
    End Sub
    '---------------Code Ends Here--------------
End Class
