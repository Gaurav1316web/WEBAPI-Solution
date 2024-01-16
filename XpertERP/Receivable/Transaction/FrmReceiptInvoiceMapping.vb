
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class FrmReceiptInvoiceMapping
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private isFormLoad As Boolean = False

    Const colLineNo As String = "COLLNO"
    Const colInvoiceNo As String = "colInvoiceNo"
    Const colIDate As String = "COLIDate"
    Const colCust As String = "COLCust"
    Const colCustName As String = "colCustName"
    Const ColLocation As String = "ColLocation"
    Const ColLocationName As String = "ColLocationName"
    Const colTaxAmt As String = "colTaxAmt"
    Const ColAmt As String = "ColAmt"
    Const colRemarks As String = "REMARKS"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public strDocumentNo As String
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        'btnReverse.Visible = MyBase.isReverse
    End Sub


    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtDesc.MaxLength = 200
    End Sub

    Private Sub FrmItemConversion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                         "========Table Name=========" + Environment.NewLine + _
                         "TSPL_Receipt_InvoiceMapping_Head" + Environment.NewLine + _
                         "TSPL_Receipt_InvoiceMapping_Detail" + Environment.NewLine + _
                         "tspl_sd_sale_invoice_head (update on POST) " + Environment.NewLine + _
                         "=========Setting Name======" + Environment.NewLine + _
                         "ApplyBrachAccounting (For Apply Branch Accounting)")
        End If

    End Sub

    Private Sub FrmItemConversion_Layout(ByVal sender As Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles Me.Layout

    End Sub
    Private Sub FrmItemConversion_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            clsDBFuncationality.ExecuteNonQuery("ALTER TABLE TSPL_Receipt_InvoiceMapping_Detail DROP CONSTRAINT FK__TSPL_RECE__Invoi__214F1607")
        Catch ex As Exception
        End Try

        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        SetLength()
        LoadBlankGrid()
        AddNew()

        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If

    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Invoice No"
        repoICode.Name = colInvoiceNo
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIDate.FormatString = ""
        repoIDate.HeaderText = "Invoice Date"
        repoIDate.Name = colIDate
        repoIDate.Width = 150
        repoIDate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIDate)


        Dim repoCust As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCust.FormatString = ""
        repoCust.HeaderText = "Customer Code"
        repoCust.Name = colCust
        repoCust.Width = 100
        repoCust.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCust)

        Dim repoCustDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustDesc.FormatString = ""
        repoCustDesc.HeaderText = "Customer Desc"
        repoCustDesc.Name = colCustName
        repoCustDesc.Width = 100
        repoCustDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustDesc)

        Dim repoLoc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLoc.FormatString = ""
        repoLoc.HeaderText = "Location Code"
        repoLoc.Name = ColLocation
        repoLoc.Width = 100
        repoLoc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLoc)

        Dim repoLocDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocDesc.FormatString = ""
        repoLocDesc.HeaderText = "Location Desc"
        repoLocDesc.Name = ColLocationName
        repoLocDesc.Width = 100
        repoLocDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLocDesc)

        Dim repTaxamt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repTaxamt.FormatString = ""
        repTaxamt.HeaderText = "Tax Amount"
        repTaxamt.Name = colTaxAmt
        repTaxamt.Width = 100
        repTaxamt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repTaxamt)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = ColAmt
        repoAmt.Width = 100
        repoAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmt)



        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remark"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 300
        repoRemarks.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRemarks)



        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = False
        fndReceiptNo.Enabled = True
        txtReceiptAMt.Text = ""
        fndReceiptNo.Value = ""
        txtReceiptTaxAmt.Text = ""
        lblBalanceRectTaxAmt.Text = ""
        gv1.Rows.AddNew()
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Sub BlankAllControls()
        txtCustomer.Text = ""
        txtCustName.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE
        txtDesc.Text = ""
        txtDocNo.Value = ""
        txtReceiptAMt.Text = ""
        txtReceiptTaxAmt.Text = ""
        fndReceiptNo.Value = ""
        txtTaxGroup.Text = ""
        txtLocDesc.Text = ""
        txtInvoiceAmt.Text = ""
        txtInvoiceTaxAmt.Text = ""
        txtLocCode.Text = ""
        isCellValueChangedOpen = True
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If isCellValueChangedOpen Then
                    isCellValueChangedOpen = False
                    If e.Column Is gv1.Columns(colInvoiceNo) Then
                        OpenICodeList(True)
                    ElseIf e.Column Is gv1.Columns(colTaxAmt) Then
                        CalculateTotal()
                    End If
                    isCellValueChangedOpen = True
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = True
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(fndReceiptNo.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Receipt No First.", Me.Text)
            Exit Sub
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colInvoiceNo).Value) <= 0 Then
            Exit Sub
        End If
        Dim CustPerm As String = Xtra.CustomerPermission()
        Dim strwherecls As String = ""
        Dim strICode As String = ""


        Dim qry As String = "Select * from (" + Environment.NewLine + _
            " select Code,Date,[Customer Code],Location, Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],Comments,[Tax Amount],Amount,[Supplementary Invoice No],xxx.[Status],[Shipment No],TransactionType from (" + Environment.NewLine + _
            " select Document_Code as Code,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,Customer_Code as [Customer Code],Bill_To_Location as Location,TSPL_SD_SALE_INVOICE_HEAD.Comments,Total_Tax_Amt as [Tax Amount],Total_Amt as Amount,Invoice_No_For_Supplementary as [Supplementary Invoice No],    'Approved'  as [Status],Against_Shipment_No as [Shipment No],case when Trans_Type='PS' then 'Product Sale' else 'MCC Material Sale' end as TransactionType from TSPL_SD_SALE_INVOICE_HEAD " + Environment.NewLine + _
            " where TSPL_SD_SALE_INVOICE_HEAD.Trans_Type in ('PS','MCC') and isnull(IsAdvanceTaxGlEntry,'0')=0 and isnull(IsAdvanceTaxGlEntry,'0')=0 and Customer_Code='" & txtCustomer.Text & "' and TSPL_SD_SALE_INVOICE_HEAD.Tax_Group='" & txtTaxGroup.Text & "' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") "
        End If
        If clsCommon.myLen(CustPerm) > 0 Then
            qry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + CustPerm + ")    "
        End If
        qry += Environment.NewLine + " union all" + Environment.NewLine + _
        " select invoice_No as Code,CONVERT(varchar(10), posting_Date,103)+' '+ CONVERT(varchar(5), posting_Date,114) as Date,cust_Code as [Customer Code],Loc_Code as Location,Description as Comments,Total_Tax_Amt as [Tax Amount],Doc_Amt as Amount,'' as [Supplementary Invoice No] ,'Approved'   as [Status] ,shipment_No as [Shipment No] ,'Misc Sale'  as TransactionType" + Environment.NewLine + _
        " from TSPL_SCRAPINVOICE_HEAD " + Environment.NewLine + _
        " where  TSPL_SCRAPINVOICE_HEAD.cust_Code='" + txtCustomer.Text + "' and TSPL_SCRAPINVOICE_HEAD.Tax_Group='" & txtTaxGroup.Text & "' and TSPL_SCRAPINVOICE_HEAD.ispost=1"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and Loc_Code in (" + objCommonVar.strCurrUserLocations + ") "
        End If
        If clsCommon.myLen(CustPerm) > 0 Then
            qry += " and TSPL_SCRAPINVOICE_HEAD.cust_Code  in (" + CustPerm + ")"
        End If
        qry += " )xxx left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=xxx.[Customer Code] " + Environment.NewLine + _
        " where not exists (select 1 from TSPL_RECEIPT_INVOICEMAPPING_DETAIL where xxx.Code = TSPL_RECEIPT_INVOICEMAPPING_DETAIL.InvoiceNo and TSPL_RECEIPT_INVOICEMAPPING_DETAIL.Doc_CODE not in ('" + txtDocNo.Value + "')) " + Environment.NewLine + _
        " ) xxxx "

        'and TSPL_SD_SALE_INVOICE_HEAD.Document_Code not in (select InvoiceNo from TSPL_RECEIPT_INVOICEMAPPING_DETAIL where TSPL_RECEIPT_INVOICEMAPPING_DETAIL.Doc_CODE not in ('" + txtDocNo.Value + "')) 
        gv1.CurrentRow.Cells(colInvoiceNo).Value = clsCommon.ShowSelectForm("ReceiptInvCode", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colInvoiceNo).Value), "Code", isButtonClick)

        If clsCommon.myLen(gv1.CurrentRow.Cells(colInvoiceNo).Value) > 0 Then
            FillInvoice(gv1.CurrentRow.Cells(colInvoiceNo).Value, gv1.CurrentRow.Index)
            CalculateTotal()
        Else
            SetBlankOfItemColumns()
        End If
    End Sub

    'Sub OpenICodeList(ByVal isButtonClick As Boolean)
    '    If clsCommon.myLen(fndReceiptNo.Value) = 0 Then
    '        clsCommon.MyMessageBoxShow("Please Select Receipt No First.", Me.Text)
    '        Exit Sub
    '    End If
    '    If clsCommon.myLen(gv1.CurrentRow.Cells(colInvoiceNo).Value) <= 0 Then
    '        Exit Sub
    '    End If

    '    Dim strwherecls As String = ""
    '    Dim strICode As String = ""
    '    Dim qry As String = "select Document_Code as Code,CONVERT(varchar(10), Document_Date,103)+' '+ CONVERT(varchar(5), Document_Date,114) as Date,Customer_Code as [Customer Code],Bill_To_Location as Location, Customer_Name as Customer,ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_SD_SALE_INVOICE_HEAD.Comments,Total_Tax_Amt as [Tax Amount],Total_Amt as Amount,Invoice_No_For_Supplementary as [Supplementary Invoice No], "
    '    qry += " case when TSPL_SD_SALE_INVOICE_HEAD.Status=0 then 'Pending' else 'Approved' end as [Status],Against_Shipment_No as [Shipment No] from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code "
    '    strwherecls = Xtra.CustomerPermission()
    '    Dim whrClas As String = " 2=2 "
    '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
    '        whrClas += " and Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")  and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ") "
    '    ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
    '        whrClas += " and Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")  and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' "
    '    ElseIf clsCommon.myLen(strwherecls) > 0 Then
    '        whrClas += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + strwherecls + ")  and TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' "
    '    Else
    '        whrClas += " and  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' "
    '    End If
    '    whrClas += " and isnull(IsAdvanceTaxGlEntry,'0')=0 and Customer_Code='" & txtCustomer.Text & "' and TSPL_SD_SALE_INVOICE_HEAD.Tax_Group='" & txtTaxGroup.Text & "' and TSPL_SD_SALE_INVOICE_HEAD.Document_Code not in (select InvoiceNo from TSPL_RECEIPT_INVOICEMAPPING_DETAIL where TSPL_RECEIPT_INVOICEMAPPING_DETAIL.Doc_CODE not in ('" + txtDocNo.Value + "')) "
    '    gv1.CurrentRow.Cells(colInvoiceNo).Value = clsCommon.ShowSelectForm("ReceiptInvCode", qry, "Code", whrClas, clsCommon.myCstr(gv1.CurrentRow.Cells(colInvoiceNo).Value), "Code", isButtonClick)

    '    If clsCommon.myLen(gv1.CurrentRow.Cells(colInvoiceNo).Value) > 0 Then
    '        FillInvoice(gv1.CurrentRow.Cells(colInvoiceNo).Value, gv1.CurrentRow.Index)
    '        CalculateTotal()
    '    Else
    '        SetBlankOfItemColumns()
    '    End If
    'End Sub

    Sub FillInvoice(ByVal strInvoiceNo As String, ByVal intRow As Integer)
        Dim qry As String = "Select Document_Date,Customer_Code,Customer_Name,Total_Tax_Amt,Bill_To_Location,Total_Amt from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where Document_Code='" & strInvoiceNo & "'" + Environment.NewLine + _
        " Union all " + Environment.NewLine + _
        " select TSPL_SCRAPINVOICE_HEAD.posting_Date as Document_Date,TSPL_SCRAPINVOICE_HEAD.cust_Code as Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,Total_Tax_Amt,TSPL_SCRAPINVOICE_HEAD.Loc_Code as Bill_To_Location,Doc_Amt as Total_Amt from TSPL_SCRAPINVOICE_HEAD  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SCRAPINVOICE_HEAD.cust_Code where  TSPL_SCRAPINVOICE_HEAD.invoice_No='" + strInvoiceNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            gv1.Rows(intRow).Cells(colIDate).Value = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            gv1.Rows(intRow).Cells(colCust).Value = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            gv1.Rows(intRow).Cells(colCustName).Value = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            gv1.Rows(intRow).Cells(ColLocation).Value = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
            gv1.Rows(intRow).Cells(ColLocationName).Value = clsLocation.GetName(clsCommon.myCstr(dt.Rows(0)("Bill_To_Location")), Nothing)
            gv1.Rows(intRow).Cells(colTaxAmt).Value = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            gv1.Rows(intRow).Cells(ColAmt).Value = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
        End If
    End Sub
    Sub CalculateTotal()
        Dim dblAmount As Double = 0
        Dim dblTaxAmount As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            dblAmount += gv1.Rows(ii).Cells(ColAmt).Value
            dblTaxAmount += gv1.Rows(ii).Cells(colTaxAmt).Value
        Next
        txtInvoiceAmt.Text = dblAmount
        txtInvoiceTaxAmt.Text = dblTaxAmount
    End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colInvoiceNo).Value = ""
        gv1.CurrentRow.Cells(colIDate).Value = ""
        gv1.CurrentRow.Cells(colCust).Value = ""
        gv1.CurrentRow.Cells(colCustName).Value = ""
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gv1_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserDeletedRow
        CalculateTotal()
    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        Else
            SetBlankOfItemColumns()
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If SaveData() Then
            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(fndReceiptNo.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Receipt No.", Me.Text)
            Return False
        End If
        If gv1.Rows.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please enter atleast One Invoice to Map.", Me.Text)
            Return False
        End If
        CalculateTotal()
        If clsCommon.myCdbl(txtInvoiceTaxAmt.Text) > clsCommon.myCdbl(lblBalanceRectTaxAmt.Text) Then
            clsCommon.MyMessageBoxShow(Me, "Invoice tax amount cant be more than balance tax amount", Me.Text)
            Return False
        End If

        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colInvoiceNo).Value)
            If (clsCommon.myLen(strICode)) > 0 Then
                For jj As Integer = 0 To gv1.Rows.Count - 1
                    If jj = ii Then
                        Continue For
                    End If
                    Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colInvoiceNo).Value)
                    If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal Then
                        Dim Msg As String = "Same Invoice Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                        common.clsCommon.MyMessageBoxShow(Msg)
                        Return False
                    End If
                Next
            End If
        Next
        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsReceiptInvoiceHead()
                obj.Doc_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Description = txtDesc.Text
                obj.Receipt_No = fndReceiptNo.Value
                obj.Receipt_Tax_Amt = txtReceiptTaxAmt.Text
                obj.Invoice_Tax_Amt = txtInvoiceTaxAmt.Text
                obj.Receipt_Location = txtLocCode.Text
                obj.Arr = New List(Of clsReceiptInvoiceDetails)()
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsReceiptInvoiceDetails()
                    objTr.InvoiceNo = clsCommon.myCstr(grow.Cells(colInvoiceNo).Value)
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    objTr.Line_No = clsCommon.myCstr(grow.Cells(colLineNo).Value)
                    objTr.InvoiceLocation = clsCommon.myCstr(grow.Cells(ColLocation).Value)
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt).Value)
                    objTr.InvoiceNo = clsCommon.myCstr(grow.Cells(colInvoiceNo).Value)
                    objTr.Doc_Code = txtDocNo.Value
                    If (clsCommon.myLen(objTr.InvoiceNo) > 0) Then
                        obj.Arr.Add(objTr)
                    End If

                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Invoice")
                End If

                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry, clsCommon.myCstr(txtDocNo.Value), Nothing)
                LoadData(obj.Doc_Code, NavigatorType.Current)
                Return isSaved
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New clsReceiptInvoiceHead()
            obj = clsReceiptInvoiceHead.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
                btnSave.Enabled = True
                btnDelete.Enabled = True
                isInsideLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                BlankAllControls()
                LoadBlankGrid()


                If obj.POSTED = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                Else
                    btnPost.Enabled = True
                End If
                fndReceiptNo.Enabled = False
                txtDocNo.Value = obj.Doc_Code
                txtDate.Value = obj.Document_Date
                fndReceiptNo.Value = obj.Receipt_No
                txtDesc.Text = obj.Description
                txtInvoiceTaxAmt.Text = obj.Invoice_Tax_Amt
                txtReceiptTaxAmt.Text = obj.Receipt_Tax_Amt
                UsLock1.Status = obj.POSTED

                FillReceiptDetails()
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsReceiptInvoiceDetails In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceNo).Value = objTr.InvoiceNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocation).Value = objTr.InvoiceLocation
                        FillInvoice(objTr.InvoiceNo, gv1.Rows.Count - 1)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt).Value = objTr.Total_Tax_Amt
                    Next
                    Dim dblAmount As Double = 0
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        dblAmount += gv1.Rows(ii).Cells(ColAmt).Value
                    Next
                    txtInvoiceAmt.Text = dblAmount
                    If obj.POSTED = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                    Else
                        'UsLock1.Status = ERPTransactionStatus.Approved
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub


    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (AllowToSave()) Then
                If (myMessages.postConfirm()) Then
                    If SaveData() Then
                        If (clsReceiptInvoiceHead.PostData(txtDocNo.Value, True)) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                            LoadData(txtDocNo.Value, NavigatorType.Current)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsReceiptInvoiceHead.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_Receipt_InvoiceMapping_Head where Doc_code='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select Doc_Code as [Code],Receipt_No as [Item Code] from TSPL_Receipt_InvoiceMapping_Head "
        txtDocNo.Value = clsCommon.ShowSelectForm("Receiptinv", qry, "Code", "", txtDocNo.Value, "Code", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
    End Sub


    Private Sub fndReceiptNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndReceiptNo._MYValidating
        Try
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(0).Cells(colInvoiceNo).Value) > 0 Then
                fndReceiptNo.Enabled = False
                Exit Sub
            End If
            Dim Qry As String = "select TSPL_RECEIPT_HEADER.Receipt_No as [ReceiptNo] ,convert(varchar,TSPL_RECEIPT_HEADER.Receipt_Date,103) as [Receipt Date] ,convert(varchar,TSPL_RECEIPT_HEADER.Receipt_Post_Date,103) as [Receipt Post Date] ,TSPL_RECEIPT_HEADER.Entry_Desc as [Entry Desc] ,TSPL_RECEIPT_HEADER.Bank_Code as [Bank Code] ,TSPL_RECEIPT_HEADER.Receipt_Type as [Receipt Type] ,TSPL_RECEIPT_HEADER.Cust_Code as [Cust Code] ,TSPL_RECEIPT_HEADER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name] ,TSPL_RECEIPT_HEADER.Reference as [Reference] ,TSPL_RECEIPT_HEADER.Narration as [Narration] ,TSPL_RECEIPT_HEADER.Payment_Code as [Payment Code] ,TSPL_RECEIPT_HEADER.Cheque_No as [Cheque No] ,TSPL_RECEIPT_HEADER.Cheque_Date as [Cheque Date] ,TSPL_RECEIPT_HEADER.Receipt_Amount as [Receipt Amount] ,TSPL_RECEIPT_HEADER.Cust_Account as [Cust Account] ,TSPL_RECEIPT_HEADER.Apply_By as [Apply By] ,TSPL_RECEIPT_HEADER.Apply_To as [Apply To] ,TSPL_RECEIPT_HEADER.Posted as [Posted] ,TSPL_RECEIPT_HEADER.Document_No as [Document No] ,TSPL_RECEIPT_HEADER.Payer as [Payer] ,TSPL_RECEIPT_HEADER.QuickEntryNo as [Quickentryno] ,TSPL_RECEIPT_HEADER.SecurityDeposit as [Securitydeposit] ,TSPL_RECEIPT_HEADER.Salesman_Code as [Salesman Code] ,TSPL_RECEIPT_HEADER.Salesman_Name as [Salesman Name] ,TSPL_RECEIPT_HEADER.Loadout_No as [Loadout No] ,TSPL_RECEIPT_HEADER.Cheque_From as [Cheque From] ,TSPL_RECEIPT_HEADER.CURRENCY_CODE as [Currency Code] ,TSPL_RECEIPT_HEADER.ConvRate as [Convrate] ,TSPL_RECEIPT_HEADER.ApplicableFrom as [Applicablefrom] ,TSPL_RECEIPT_HEADER.CFormRecd as [Cformrecd] ,TSPL_RECEIPT_HEADER.CForm_InvoiceNo as [Cform Invoiceno] ,TSPL_RECEIPT_HEADER.BASE_CURRENCY_CODE as [Base Currency Code] ,TSPL_RECEIPT_HEADER.RECEIVED_AMOUNT_BASE_CURRENCY as [Received Amount Base Currency] ,TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT as [Exchange Loss Amt] ,TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT as [Exchange Gain Amt] ,TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_ACCOUNT as [Exchange Loss Account] ,TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_ACCOUNT as [Exchange Gain Account] ,TSPL_RECEIPT_HEADER.ConvRateOld as [Convrateold] ,TSPL_RECEIPT_HEADER.PROJECT_ID as [Project Id] ,TSPL_RECEIPT_HEADER.IsParentCust as [Isparentcust] ,TSPL_RECEIPT_HEADER.CHECK_PRINT as [Check Print] ,TSPL_RECEIPT_HEADER.CHECK_CODE as [Check Code] ,TSPL_RECEIPT_HEADER.AUTO_GEN_BT_ENTRY as [Auto Gen Bt Entry] ,TSPL_RECEIPT_HEADER.TO_BANK_CODE as [To Bank Code] ,TSPL_RECEIPT_HEADER.Transfer_No as [Transfer No] ,TSPL_RECEIPT_HEADER.From_Branch as [From Branch] ,TSPL_RECEIPT_HEADER.memorandum_amt as [Memorandum Amt] ,TSPL_RECEIPT_HEADER.Applied_Receipt as [Applied Receipt],TSPL_RECEIPT_HEADER.SaleOrderNo,isnull(TSPL_RECEIPT_HEADER.Delivery_Code_PS,'') as [Delivery Order], " & _
                " TSPL_RECEIPT_HEADER.Tax_Amount_Advance,ISNULL(TSPL_INV_MAPPING_HEAD.Invoice_Tax_Amt,0) AS Invoice_Tax_Amt " & _
                " From TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_RECEIPT_HEADER.Bank_Code " & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_RECEIPT_HEADER.Cust_Code " & _
                "  LEFT OUTER JOIN (select TSPL_Receipt_InvoiceMapping_Head.Receipt_No,sum(isnull(TSPL_Receipt_InvoiceMapping_Head.Invoice_Tax_Amt,0)) as Invoice_Tax_Amt,MAX(ISNULL(TSPL_Receipt_InvoiceMapping_Head.Receipt_Tax_Amt,0)) AS Receipt_Tax_Amt     from  " & _
                 " TSPL_Receipt_InvoiceMapping_Head group by Receipt_No HAVING MAX(ISNULL(TSPL_Receipt_InvoiceMapping_Head.Receipt_Tax_Amt,0)) <> sum(isnull(TSPL_Receipt_InvoiceMapping_Head.Invoice_Tax_Amt,0))  ) as TSPL_INV_MAPPING_HEAD " & _
                 " on  TSPL_RECEIPT_HEADER.Receipt_No=TSPL_INV_MAPPING_HEAD.Receipt_No AND  ISNULL(TSPL_RECEIPT_HEADER.Tax_Amount_Advance,0)  <>ISNULL(TSPL_INV_MAPPING_HEAD.Invoice_Tax_Amt,0) "
            Dim WhrCls As String = "TSPL_RECEIPT_HEADER.receipt_type='P' and TSPL_RECEIPT_HEADER.tax_group <> '' and TSPL_RECEIPT_HEADER.Delivery_Code_PS='' AND TSPL_RECEIPT_HEADER.Posted='Y' and (ISNULL(TSPL_RECEIPT_HEADER.Tax_Amount_Advance,0)-ISNULL(TSPL_INV_MAPPING_HEAD.Invoice_Tax_Amt,0))>=0 " 'and Receipt_No not in (select Receipt_No from TSPL_Receipt_InvoiceMapping_Head)"
            fndReceiptNo.Value = clsCommon.ShowSelectForm("RecNo", Qry, "ReceiptNo", WhrCls, fndReceiptNo.Value, " Convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)", isButtonClicked)
            If clsCommon.myLen(fndReceiptNo.Value) > 0 Then
                FillReceiptDetails()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Sub FillReceiptDetails()
        Dim qry As String = "Select TSPL_RECEIPT_HEADER.* from TSPL_RECEIPT_HEADER WHERE TSPL_RECEIPT_HEADER.Receipt_No='" & fndReceiptNo.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtReceiptAMt.Text = clsCommon.myCdbl(dt.Rows(0)("receipt_amount"))
            txtReceiptTaxAmt.Text = clsCommon.myCdbl(dt.Rows(0)("Tax_Amount_Advance"))
            txtCustomer.Text = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            txtCustName.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            txtTaxGroup.Text = clsCommon.myCstr(dt.Rows(0)("tax_group"))
            txtLocCode.Text = clsCommon.myCstr(dt.Rows(0)("Location_GL_Code"))
            txtLocDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No = '7'  and Segment_code='" + txtLocCode.Text + "'"))
            lblBalanceRectTaxAmt.Text = clsReceiptInvoiceHead.GetReceiptBalanceTaxAmount(fndReceiptNo.Value, txtDocNo.Value, Nothing)
        End If
    End Sub


    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsReceiptInvoiceHead.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
