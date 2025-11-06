Imports System.Data.SqlClient
Imports common
Public Class frmCancelDCSSale
#Region "Variables"
    Dim IsInsideLoadData As Boolean = False
    Const colDSelect As String = "SELECT"
    Const colInvoiceNo As String = "colInvoiceNo"
    Const colInvoiceDate As String = "colInvoiceDate"
    Const colDCSUploaderNo As String = "colDCSUploaderNo"
    Const colDCSCode As String = "colDCSCode"
    Const colDCSName As String = "colDCSName"
    Const colDedCode As String = "colDedCode"
    Const colDedName As String = "colDedName"
    Const colAmount As String = "colAmount"
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadBlankGrid()
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colDSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoSelect)


        Dim repoInvoiceNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoInvoiceNo.FormatString = ""
        repoInvoiceNo.HeaderText = "Invoice No"
        repoInvoiceNo.Name = colInvoiceNo
        repoInvoiceNo.Width = 180
        repoInvoiceNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoInvoiceNo)

        Dim repoInvDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoInvDate.Format = DateTimePickerFormat.Custom
        repoInvDate.CustomFormat = "dd-MM-yyyy"
        repoInvDate.HeaderText = "Invoice Date"
        repoInvDate.FormatString = "{0:d}"
        repoInvDate.Name = colInvoiceDate
        repoInvDate.WrapText = True
        repoInvDate.ReadOnly = True
        repoInvDate.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoInvDate)

        Dim repoDCSUploader As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSUploader.FormatString = ""
        repoDCSUploader.HeaderText = "DCS Uploader No"
        repoDCSUploader.Name = colDCSUploaderNo
        repoDCSUploader.Width = 80
        repoDCSUploader.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDCSUploader)

        Dim repoDCSCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSCode.FormatString = ""
        repoDCSCode.HeaderText = "DCS Code"
        repoDCSCode.Name = colDCSCode
        repoDCSCode.Width = 120
        repoDCSCode.ReadOnly = True
        repoDCSCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoDCSCode)

        Dim repoDCSName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSName.FormatString = ""
        repoDCSName.HeaderText = "DCS Name"
        repoDCSName.Name = colDCSName
        repoDCSName.Width = 180
        repoDCSName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDCSName)

        Dim repoDedCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDedCode.FormatString = ""
        repoDedCode.HeaderText = "Deduction Code"
        repoDedCode.Name = colDedCode
        repoDedCode.Width = 80
        repoDedCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDedCode)

        Dim repoDedName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDedName.FormatString = ""
        repoDedName.HeaderText = "Deduction Name"
        repoDedName.Name = colDedName
        repoDedName.Width = 120
        repoDedName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDedName)

        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colAmount
        repoAmount.ReadOnly = True
        repoAmount.Width = 100
        repoAmount.WrapText = True
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmount)

        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Sub setGridPropery()
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        ''gv1.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        btnClosePressed()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        CancelData()
    End Sub

    Sub btnClosePressed()
        Me.Close()
    End Sub

    Sub CancelData()
        Dim ArrCheck As New ArrayList()
        If clsCommon.MyMessageBoxShow(Me, "Are you sure to Cancel the Record?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            For ii As Integer = 0 To gv1.RowCount - 1
                If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                    ArrCheck.Add(gv1.Rows(ii).Cells(colInvoiceNo).Value)
                    clsPSInvoiceHead.CancelData(clsCommon.myCstr(gv1.Rows(ii).Cells(colInvoiceNo).Value), trans)
                    clsCommon.MyMessageBoxShow(Me, "Successfully Cancelled", Me.Text)
                End If
            Next
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

        If ArrCheck.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Invoice", Me.Text)
        Else
            Me.Close()
        End If
    End Sub

    Private Sub frmCancelDCSSale_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            '  btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnClosePressed()
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        If gv1.CurrentColumn Is gv1.Columns(colDSelect) Then
            Dim strPONO As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colInvoiceNo).Value)
            Dim SelectStatus As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colDSelect).Value)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(strPONO, clsCommon.myCstr(gv1.Rows(ii).Cells(colInvoiceNo).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colDSelect).Value = Not SelectStatus
                End If
            Next
        End If
    End Sub

    'Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs)
    '    If Not IsInsideLoadData Then
    '        If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
    '            Dim strVendorCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorCode).Value)
    '            Dim strVendorName As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHVendorName).Value)
    '            If clsCommon.myLen(VendorCode) <= 0 Then
    '                VendorCode = strVendorCode
    '                VendorName = strVendorName
    '            End If
    '            If clsCommon.CompairString(strVendorCode, VendorCode) = CompairStringResult.Equal Then
    '                Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
    '                If clsCommon.myLen(strCode) > 0 Then
    '                    LoadDetailData(e.NewValue, strCode)
    '                End If
    '            Else
    '                common.clsCommon.MyMessageBoxShow(Me, "Invoice's Customer should be `" + VendorName)
    '                e.Cancel = True
    '            End If
    '        End If
    '    End If
    'End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Sub LoadData()
        Try
            If clsCommon.myLen(txtFromDate.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "From Date Can't be Blank. ", Me.Text)
                txtFromDate.Focus()
                Return
            End If
            If clsCommon.myLen(txtToDate.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "To Date Can't be Blank.", Me.Text)
                txtToDate.Focus()
                Return
            End If
            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then To Date", Me.Text)
                txtFromDate.Focus()
                Return
            End If

            setGridPropery()

            Dim strwherecls As String = ""
            Dim qry As String = ""

            qry += "select  Document_Code as Invoice_No,Document_Date as Invoice_Date, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,Deduction,TSPL_DEDUCTION_MASTER.Description as Deduction_Name,tspl_sd_sale_invoice_head.Total_Amt, * from tspl_sd_sale_invoice_head
            left join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code = tspl_sd_sale_invoice_head.Customer_Code left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code  left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = tspl_sd_sale_invoice_head.Deduction
            where Status = 1 and Is_Taxable = 0 and Trans_Type = 'MCC' and  convert(date,Document_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,Document_Date,103) <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' "

            If txtDeduction.arrValueMember IsNot Nothing AndAlso txtDeduction.arrValueMember.Count > 0 Then
                qry += " And Deduction in (" & clsCommon.GetMulcallString(txtDeduction.arrValueMember) & ") "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            LoadBlankGrid()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myLen(clsCommon.myCstr(dr("Invoice_No"))) Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceNo).Value = clsCommon.myCstr(dr("Invoice_No"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceDate).Value = clsCommon.myCstr(dr("Invoice_Date"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSUploaderNo).Value = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSCode).Value = clsCommon.myCstr(dr("VLC_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSName).Value = clsCommon.myCstr(dr("VLC_Name"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDedCode).Value = clsCommon.myCdbl(dr("Deduction"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDedName).Value = clsCommon.myCdbl(dr("Deduction_Name"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = clsCommon.myCdbl(dr("Total_Amt"))
                    End If
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDeduction__My_Click(sender As Object, e As EventArgs) Handles txtDeduction._My_Click
        Try
            Dim qry As String = " Select  Distinct Deduction as Code,max(Description) as Name  from TSPL_ITEM_MASTER left outer join TSPL_DEDUCTION_MASTER On TSPL_DEDUCTION_MASTER.Code=TSPL_ITEM_MASTER.Deduction
                             where 2=2 and Deduction is not null group by Deduction "

            txtDeduction.arrValueMember = clsCommon.ShowMultipleSelectForm("DedMulSel", qry, "Code", "Name", txtDeduction.arrValueMember, txtDeduction.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

