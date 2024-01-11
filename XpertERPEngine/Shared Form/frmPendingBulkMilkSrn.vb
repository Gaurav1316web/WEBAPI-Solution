Imports common
Imports System.Data.SqlClient

Public Class FrmPendingBulkMilkSrn
    Public btnOkClicked As Boolean = False
    Public Const colSelect As String = "colSelect"
    Public Const colSRNNo As String = "SRNNO"
    Public Const colSRNDATE As String = "SRNDate"
    Public Const colItemCode As String = "colItemCode"
    Public Const colItemDesc As String = "colItemDesc"
    Public Const colNetWeight As String = "colNetWeight"
    Public Const colInvoiceQty As String = "colInvoiceQty"
    Public Const colRemainingQty As String = "colRemainingQty"
    Public Const colVendorCode As String = "colVendorCode"
    Public Const colVendorName As String = "colVendorName"
    Public Const colLocationCode As String = "colLocationCode"
    Public Const colLocationDesc As String = "colLocationDesc"
    Public Const colUOM As String = "colUOM"
    Public Const colXtraRate As String = "colXtraRate"
    Public arrSrnNo As List(Of BulkMilkSRNXtraRate) = Nothing
    Public qry As String = String.Empty
    'Public XtraRate As Decimal = 0
    Dim settIncludeInceAndDedInFATSNFRate As Boolean = True

    Private Sub FrmPendingBulkMilkSrn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        settIncludeInceAndDedInFATSNFRate = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IncludeInceAndDedInFATSNFRate, clsFixedParameterCode.IncludeInceAndDedInFATSNFRate, Nothing)) = 1)
        Me.Text = "Pending SRN "
        Panel1.Visible = settIncludeInceAndDedInFATSNFRate
        LoadGrid()
    End Sub

    Private Sub FrmPendingBulkMilkSrn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            arrSrnNo = New List(Of BulkMilkSRNXtraRate)
            Dim LocCode As String = String.Empty
            Dim Vencode As String = String.Empty
            For i As Integer = 0 To gv.Rows.Count - 1
                Dim obj As New BulkMilkSRNXtraRate
                If gv.Rows(i).Cells(colSelect).Value = True Then
                    If Not clsCommon.CompairString(LocCode, clsCommon.myCstr(gv.Rows(i).Cells(colLocationCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.myLen(LocCode) > 0 Then
                        Throw New Exception(" Selected SRN must be of same location")
                    End If
                    If Not clsCommon.CompairString(Vencode, clsCommon.myCstr(gv.Rows(i).Cells(colVendorCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.myLen(Vencode) > 0 Then
                        Throw New Exception(" Selected SRN must be of same Vendor")
                    End If
                    LocCode = clsCommon.myCstr(gv.Rows(i).Cells(colLocationCode).Value)
                    Vencode = clsCommon.myCstr(gv.Rows(i).Cells(colVendorCode).Value)
                    obj.SRNCode = clsCommon.myCstr(gv.Rows(i).Cells(colSRNNo).Value)
                    obj.XtraRate = clsCommon.myCdbl(gv.Rows(i).Cells(colXtraRate).Value)
                    arrSrnNo.Add(obj)
                End If
            Next
            If arrSrnNo.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No SRN selected" & Environment.NewLine & "Please select atleast one SRN")
                Exit Sub
            End If
            'XtraRate = txtExtraRate.Value
            btnOkClicked = True
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Sub LoadGrid()
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(qry) > 0 Then
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsCommon.ProgressBarPercentShow()
                loadBlankGrid()
                For i As Integer = 0 To dt.Rows.Count - 1
                    clsCommon.ProgressBarPercentUpdate(((i + 1) * 100) / dt.Rows.Count, "Wait Loading total " & (i + 1) & " Records of " & dt.Rows.Count)
                    gv.Rows.AddNew()
                    gv.Rows(gv.Rows.Count - 1).Cells(colSRNNo).Value = dt.Rows(i)("SRN_No")
                    gv.Rows(gv.Rows.Count - 1).Cells(colSRNDATE).Value = dt.Rows(i)("SRN_Date")
                    gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = dt.Rows(i)("Item_Code")
                    gv.Rows(gv.Rows.Count - 1).Cells(colItemDesc).Value = clsItemMaster.GetItemName(dt.Rows(i)("Item_Code"), Nothing)
                    gv.Rows(gv.Rows.Count - 1).Cells(colUOM).Value = dt.Rows(i)("UOM")
                    gv.Rows(gv.Rows.Count - 1).Cells(colNetWeight).Value = dt.Rows(i)("Net_Weight")
                    gv.Rows(gv.Rows.Count - 1).Cells(colInvoiceQty).Value = dt.Rows(i)("invoice_qty")
                    gv.Rows(gv.Rows.Count - 1).Cells(colRemainingQty).Value = dt.Rows(i)("Remaining_qty")
                    gv.Rows(gv.Rows.Count - 1).Cells(colLocationCode).Value = dt.Rows(i)("Loc_code")
                    gv.Rows(gv.Rows.Count - 1).Cells(colLocationDesc).Value = clsLocation.GetName(dt.Rows(i)("Loc_code"), Nothing)
                    gv.Rows(gv.Rows.Count - 1).Cells(colVendorCode).Value = dt.Rows(i)("Vendor_code")
                    gv.Rows(gv.Rows.Count - 1).Cells(colVendorName).Value = clsVendorMaster.GetName(dt.Rows(i)("Vendor_code"), Nothing)
                    gv.Rows(gv.Rows.Count - 1).Cells(colXtraRate).Value = Nothing
                Next
                clsCommon.ProgressBarPercentHide()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Pending SRN Found", Me.Text)
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
        repoCode.HeaderText = "SRN No"
        repoCode.Name = colSRNNo
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoCode)

        Dim repoSRNDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoSRNDate.FormatString = ""
        repoSRNDate.HeaderText = "SRN Date"
        repoSRNDate.Name = colSRNDATE
        repoSRNDate.Width = 180
        repoSRNDate.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSRNDate)


        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colItemCode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Desc"
        repoIName.Name = colItemDesc
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIName)


        Dim repoVCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVCode.FormatString = ""
        repoVCode.HeaderText = "Vendor Code"
        repoVCode.Name = colVendorCode
        repoVCode.Width = 100
        repoVCode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoVCode)

        Dim repoVName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVName.FormatString = ""
        repoVName.HeaderText = "Vendor Desc"
        repoVName.Name = colVendorName
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

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "SRN Qty"
        repoOrderQty.Name = colNetWeight
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoOrderQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colUOM
        repoUnit.Width = 60
        repoUnit.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoUnit)



        Dim repoInvoiceQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoInvoiceQty.FormatString = ""
        repoInvoiceQty.HeaderText = "Invoice Qty (Used)"
        repoInvoiceQty.Name = colInvoiceQty
        repoInvoiceQty.ReadOnly = True
        repoInvoiceQty.Width = 100
        repoInvoiceQty.WrapText = True
        repoInvoiceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoInvoiceQty)


        Dim repoRemainingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRemainingQty.FormatString = ""
        repoRemainingQty.HeaderText = "Remaining Qty"
        repoRemainingQty.Name = colRemainingQty
        repoRemainingQty.ReadOnly = True
        repoRemainingQty.Width = 100
        repoRemainingQty.WrapText = True
        repoRemainingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoRemainingQty)


        repoInvoiceQty = New GridViewDecimalColumn()
        repoInvoiceQty.FormatString = ""
        repoInvoiceQty.HeaderText = "Extra Rate"
        repoInvoiceQty.Name = colXtraRate
        repoInvoiceQty.ReadOnly = True
        repoInvoiceQty.IsVisible = settIncludeInceAndDedInFATSNFRate
        If settIncludeInceAndDedInFATSNFRate Then
            repoInvoiceQty.Width = 100
        End If
        repoInvoiceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoInvoiceQty)

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

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            For i As Integer = 0 To gv.Rows.Count - 1
                If gv.Rows(i).Cells(colSelect).Value = True Then
                    If gv.Rows(i).Cells(colXtraRate).Value Is Nothing Then
                        gv.Rows(i).Cells(colXtraRate).Value = txtExtraRate.Value
                    End If
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            For i As Integer = 0 To gv.Rows.Count - 1
                gv.Rows(i).Cells(colXtraRate).Value = Nothing
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

Public Class BulkMilkSRNXtraRate
    Public SRNCode As String = Nothing
    Public XtraRate As Decimal = Nothing
End Class
