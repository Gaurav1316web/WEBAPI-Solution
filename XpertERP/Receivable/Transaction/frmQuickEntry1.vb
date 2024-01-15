'26/12/2012-11:45AM---Updation By--Pankaj Kumar---Applied Validations
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports common
Imports System.IO
''Checkin richa 19/06/2020
Public Class FrmQuickEntry1
    Inherits FrmMainTranScreen
    Const ReportID As String = "QuickEntryGrid"
    Dim tran As SqlTransaction
    Dim userCode, companyCode, Qry As String
    Public IsLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public clicked As Boolean = False
    Public IsNewEntry As Boolean = True
    Dim arrDocNo As List(Of String)
    Dim ShowOutstandingAmtofCustomerOnQuickBookEntry As Boolean = False
    Public Sub New(ByVal user As String, ByVal company As String)
        IsLoadData = True
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub FrmQuickEntry1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IsLoadData = False
        ShowOutstandingAmtofCustomerOnQuickBookEntry = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowOutstandingAmtofCustomerOnQuickBookEntry, clsFixedParameterCode.ShowOutstandingAmtofCustomerOnQuickBookEntry, Nothing)) = 1, True, False)
        SetUserMgmtNew()
        dtDocDate.Value = System.DateTime.Now.Date
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")

        funReset()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            txtEntryNo.Value = clsCommon.myCstr(Me.Tag)
            fillData(clsCommon.myCstr(Me.Tag))
        End If
    End Sub

    Private Sub LoadBlankGrid()
        MasterTemplate.DataSource = Nothing
        MasterTemplate.Rows.Clear()
        MasterTemplate.Columns.Clear()

        MasterTemplate.AllowDeleteRow = True
        MasterTemplate.AllowAddNewRow = False

        Dim gvSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvSelect.FormatString = ""
        gvSelect.Name = "gvSelect"
        gvSelect.Width = 30
        gvSelect.ReadOnly = False
        gvSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        MasterTemplate.MasterTemplate.Columns.Add(gvSelect)

        Dim docType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docType.FormatString = ""
        docType.HeaderText = "Type"
        docType.Name = "gvType"
        docType.Width = 100
        docType.IsVisible = False
        docType.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(docType)

        Dim SourceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SourceCode.FormatString = ""
        SourceCode.HeaderText = "Code"
        SourceCode.Name = "gvSourceCode"
        SourceCode.Width = 120
        SourceCode.ReadOnly = False
        MasterTemplate.MasterTemplate.Columns.Add(SourceCode)

        Dim SourceName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SourceName.FormatString = ""
        SourceName.HeaderText = "Description"
        SourceName.Name = "gvSourceName"
        SourceName.Width = 200
        SourceName.ReadOnly = True
        MasterTemplate.MasterTemplate.Columns.Add(SourceName)

        ''richa agarwal VIJ/09/12/19-000111
        If ShowOutstandingAmtofCustomerOnQuickBookEntry = True AndAlso ddlType.Text = "Receipt" Then
            Dim originalOutstandingAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
            originalOutstandingAmt.FormatString = ""
            originalOutstandingAmt.HeaderText = "Outstanding Amount"
            originalOutstandingAmt.Name = "gvOutstandingAmount"
            originalOutstandingAmt.Width = 100
            originalOutstandingAmt.ReadOnly = True
            originalOutstandingAmt.IsVisible = True
            originalOutstandingAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            MasterTemplate.MasterTemplate.Columns.Add(originalOutstandingAmt)
        End If


        Dim docLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docLocation.FormatString = ""
        docLocation.HeaderText = "Location"
        docLocation.Name = "gvLocation"
        docLocation.Width = 100
        If ddlType.Text = "Receipt" Or ddlType.Text = "Payment" Then
            docLocation.IsVisible = True
        Else
            docLocation.IsVisible = False
        End If
        docLocation.ReadOnly = False
        MasterTemplate.MasterTemplate.Columns.Add(docLocation)

        Dim docLocation_Name As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docLocation_Name.FormatString = ""
        docLocation_Name.HeaderText = "Location Name"
        docLocation_Name.Name = "gvLocation_Name"
        docLocation_Name.Width = 100
        If ddlType.Text = "Receipt" Or ddlType.Text = "Payment" Then
            docLocation_Name.IsVisible = True
        Else
            docLocation_Name.IsVisible = False
        End If
        docLocation_Name.ReadOnly = False
        MasterTemplate.MasterTemplate.Columns.Add(docLocation_Name)

        Dim FromBank As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        FromBank.FormatString = ""
        FromBank.HeaderText = "From Bank"
        FromBank.Name = "gvFromBank"
        FromBank.Width = 200
        FromBank.ReadOnly = False
        MasterTemplate.MasterTemplate.Columns.Add(FromBank)

        Dim PaymentMode As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        PaymentMode.FormatString = ""
        PaymentMode.HeaderText = "Payment Mode"
        PaymentMode.Name = "gvPaymentMode"
        PaymentMode.DataSource = loadPaymentMode()
        PaymentMode.ValueMember = "Payment_Code"
        PaymentMode.DisplayMember = "Payment_Code"
        PaymentMode.Width = 100
        PaymentMode.ReadOnly = False
        MasterTemplate.MasterTemplate.Columns.Add(PaymentMode)

        Dim ChequeNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ChequeNo.FormatString = ""
        ChequeNo.HeaderText = "Cheque/UTR No"
        ChequeNo.Width = 100
        ChequeNo.Name = "gvCheckNo"
        ChequeNo.ReadOnly = False
        ChequeNo.IsVisible = True
        ChequeNo.MaxLength = 6
        MasterTemplate.MasterTemplate.Columns.Add(ChequeNo)

        Dim Chequedate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        Chequedate.FormatString = ""
        Chequedate.CustomFormat = "dd/MM/yyyy"
        Chequedate.HeaderText = "Cheque/UTR Date"
        Chequedate.Name = "gvCheckDate"
        Chequedate.Width = 100
        Chequedate.ReadOnly = False
        MasterTemplate.MasterTemplate.Columns.Add(Chequedate)

        Dim originalInvAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        originalInvAmt.FormatString = ""
        originalInvAmt.HeaderText = "Amount"
        originalInvAmt.Name = "gvAmount"
        originalInvAmt.Width = 100
        originalInvAmt.ReadOnly = False
        originalInvAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        MasterTemplate.MasterTemplate.Columns.Add(originalInvAmt)

        Dim gvPDCCheque As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvPDCCheque.FormatString = ""
        gvPDCCheque.Name = "gvPDCCheque"
        gvPDCCheque.HeaderText = "PDC Cheque"
        gvPDCCheque.Width = 100
        gvPDCCheque.ReadOnly = False
        If clsCommon.CompairString(ddlType.Text, "Payment") = CompairStringResult.Equal Then
            gvPDCCheque.IsVisible = True
        Else
            gvPDCCheque.IsVisible = False
        End If
        gvPDCCheque.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        MasterTemplate.MasterTemplate.Columns.Add(gvPDCCheque)

        Dim gvACPayee As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvACPayee.FormatString = ""
        gvACPayee.Name = "gvACPayee"
        gvACPayee.HeaderText = "A/C Payee"
        gvACPayee.Width = 100
        gvACPayee.ReadOnly = False
        If clsCommon.CompairString(ddlType.Text, "Payment") = CompairStringResult.Equal Then
            gvACPayee.IsVisible = True
        Else
            gvACPayee.IsVisible = False
        End If
        gvACPayee.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        MasterTemplate.MasterTemplate.Columns.Add(gvACPayee)

        Dim DocNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DocNo.FormatString = ""
        DocNo.HeaderText = "Document No"
        DocNo.Name = "gvDocNo"
        DocNo.Width = 100
        DocNo.ReadOnly = True
        DocNo.IsVisible = True
        MasterTemplate.MasterTemplate.Columns.Add(DocNo)


        Dim gvCForm As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvCForm.FormatString = ""
        gvCForm.Name = "gvCForm"
        gvCForm.HeaderText = "CForm"
        gvCForm.Width = 70

        If objCommonVar.IsDemoERP = True AndAlso (clsCommon.CompairString(ddlType.Text, "Receipt") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlType.Text, "Payment") = CompairStringResult.Equal) Then
            gvCForm.ReadOnly = False
        Else
            gvCForm.IsVisible = False
        End If
        gvCForm.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        MasterTemplate.MasterTemplate.Columns.Add(gvCForm)

        Dim CFormDoc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CFormDoc.FormatString = ""
        CFormDoc.HeaderText = "CForm Invoice"
        CFormDoc.Name = "gvCFormDoc"
        CFormDoc.Width = 100
        If objCommonVar.IsDemoERP = True AndAlso (clsCommon.CompairString(ddlType.Text, "Receipt") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlType.Text, "Payment") = CompairStringResult.Equal) Then
            CFormDoc.ReadOnly = False
        Else
            CFormDoc.IsVisible = False
        End If
        MasterTemplate.MasterTemplate.Columns.Add(CFormDoc)


        Dim status As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        status.FormatString = ""
        status.HeaderText = "Status"
        status.Name = "gvStatus"
        status.Width = 150
        status.ReadOnly = True
        status.IsVisible = True
        MasterTemplate.MasterTemplate.Columns.Add(status)

        Dim Narration As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Narration.FormatString = ""
        Narration.HeaderText = "Narration"
        Narration.Name = "gvNarration"
        Narration.Width = 150
        Narration.ReadOnly = False
        MasterTemplate.MasterTemplate.Columns.Add(Narration)

        MasterTemplate.ShowGroupPanel = False
        MasterTemplate.AllowColumnReorder = False
        MasterTemplate.AllowRowReorder = False
        MasterTemplate.EnableSorting = False
        MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        MasterTemplate.MasterTemplate.ShowRowHeaderColumn = False
        MasterTemplate.AllowAddNewRow = False
    End Sub

    Private Function loadPaymentMode() As DataTable
        Qry = "select TSPL_PAYMENT_CODE.Payment_Code   from TSPL_PAYMENT_CODE"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        Return dt
    End Function


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmQuickBook)
        '--preeti gupta ticket no-[BM00000003168]
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If btnSave.Visible = True Then
            rmiExport.Enabled = True
            rmiImport.Enabled = True
        Else
            rmiExport.Enabled = False
            rmiImport.Enabled = False
        End If
    End Sub

    Private Sub FillBankDetails()
        Dim Qry As String = "select DESCRIPTION , Substring(bankacc,len(bankacc)-2,3) as location  from TSPL_BANK_MASTER where BANK_CODE ='" + fndBankCode.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            txtBankName.Text = clsCommon.myCstr(dt.Rows(0).Item("DESCRIPTION"))
            txtLocation.Text = clsCommon.myCstr(dt.Rows(0).Item("location"))
        Else
            txtBankName.Text = ""
            txtLocation.Text = ""
        End If
    End Sub
    ''richa agarwal VIJ/28/11/19-000083
    Private Sub MasterTemplate_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles MasterTemplate.CellDoubleClick
        Try
            If e.RowIndex >= 0 Then
                If ShowOutstandingAmtofCustomerOnQuickBookEntry = True AndAlso ddlType.Text = "Receipt" Then
                    If e.Column.Name = "gvOutstandingAmount" Then
                        Dim strCode As String = MasterTemplate.CurrentRow.Cells("gvSourceCode").Value
                        If clsCommon.myLen(strCode) <= 0 Then
                            Throw New Exception("Plz Select Customer First")
                        Else
                            objCommonVar.ObjVar2 = dtDocDate.Value
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCustomerAgeing, strCode)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub MasterTemplate_CellEditorInitialized(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles MasterTemplate.CellEditorInitialized
        'If IsLoadData = False Then
        '    MasterTemplate.CurrentRow.Cells("gvSelect").Value = True
        '    MasterTemplate.CurrentRow.Cells("gvType").Value = "On-Account"
        'End If
    End Sub

    Private Sub funTypeLoad()
        If ddlType.Text = "Receipt" Then
            Dim cd1 As GridViewComboBoxColumn = TryCast(MasterTemplate.Columns("gvType"), GridViewComboBoxColumn)
            cd1.DataSource = New String() {"On-Account", "Advance"}
        Else
            Dim cd1 As GridViewComboBoxColumn = TryCast(MasterTemplate.Columns("gvType"), GridViewComboBoxColumn)
            cd1.DataSource = New String() {"On-Account"}
        End If
    End Sub

    Private Sub MasterTemplate_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles MasterTemplate.CellValueChanged
        If IsLoadData = False Then
            If e.Column.Name = "gvSourceCode" Then
                OpenICodeList(False)
                ''richa agarwal 23 Jan,2020
                If MasterTemplate.CurrentRow.Index > 0 Then
                    MasterTemplate.CurrentRow.Cells("gvPaymentMode").Value = clsCommon.myCstr(MasterTemplate.Rows(MasterTemplate.CurrentRow.Index - 1).Cells("gvPaymentMode").Value)
                Else
                    MasterTemplate.CurrentRow.Cells("gvPaymentMode").Value = "CHEQUE"
                End If

            End If

            If e.Column.Name = "gvLocation" Then
                Location_Finder()
                ' MasterTemplate.CurrentRow.Cells("gvPaymentMode").Value = "CHEQUE"
            End If

            If e.Column.Name = "gvPaymentMode" Then
                MasterTemplate.CurrentRow.Cells("gvCheckNo").Value = ""
                MasterTemplate.CurrentRow.Cells("gvCheckDate").Value = Nothing
                If clsCommon.CompairString(MasterTemplate.CurrentRow.Cells("gvPaymentMode").Value, "Cheque") = CompairStringResult.Equal Then
                    MasterTemplate.CurrentRow.Cells("gvCheckNo").ReadOnly = False
                    MasterTemplate.CurrentRow.Cells("gvCheckDate").ReadOnly = False
                Else
                    MasterTemplate.CurrentRow.Cells("gvCheckNo").ReadOnly = True
                    MasterTemplate.CurrentRow.Cells("gvCheckDate").ReadOnly = True
                End If
            End If
            If e.Column.Name = "gvCForm" Then
                MasterTemplate.CurrentRow.Cells("gvCFormDoc").Value = ""
            End If
            If e.Column.Name = "gvCFormDoc" AndAlso MasterTemplate.CurrentRow.Cells("gvCForm").Value = True Then
                OpenCformDocList(False)
            End If
            'End If
        End If
        If e.Column.Name = "gvStatus" Then
            If MasterTemplate.CurrentRow.Cells("gvStatus").Value = "Posted" Then
                MasterTemplate.CurrentRow.Cells("gvStatus").ReadOnly = True
                MasterTemplate.CurrentRow.Cells("gvSelect").ReadOnly = True
                MasterTemplate.CurrentRow.Cells("gvType").ReadOnly = True
                MasterTemplate.CurrentRow.Cells("gvSourceCode").ReadOnly = True
                MasterTemplate.CurrentRow.Cells("gvAmount").ReadOnly = True
                MasterTemplate.CurrentRow.Cells("gvNarration").ReadOnly = True
                MasterTemplate.CurrentRow.Cells("gvCheckNo").ReadOnly = True
                MasterTemplate.CurrentRow.Cells("gvCheckDate").ReadOnly = True
            Else
                MasterTemplate.CurrentRow.Cells("gvStatus").ReadOnly = False
                MasterTemplate.CurrentRow.Cells("gvSelect").ReadOnly = False
                MasterTemplate.CurrentRow.Cells("gvType").ReadOnly = False
                MasterTemplate.CurrentRow.Cells("gvSourceCode").ReadOnly = False
                MasterTemplate.CurrentRow.Cells("gvAmount").ReadOnly = False
                MasterTemplate.CurrentRow.Cells("gvNarration").ReadOnly = False
                MasterTemplate.CurrentRow.Cells("gvCheckNo").ReadOnly = False
                MasterTemplate.CurrentRow.Cells("gvCheckDate").ReadOnly = False
            End If
        End If
    End Sub

    Public Function funInsert(Optional ByVal tran1 As SqlTransaction = Nothing) As Boolean
        Dim EntryNo As String = ""
        Dim strQ As String = ""
        tran = clsDBFuncationality.GetTransactin()
        Try
            If ddlType.Text = "Receipt" Then
                EntryNo = fnAutoGenerateNo(dtDocDate.Value, tran)
                If MasterTemplate.Rows.Count <= 0 Then
                    Return False
                End If
                For Each row As GridViewRowInfo In MasterTemplate.Rows
                    If row.Cells("gvSelect").Value = False Then
                        Continue For
                    ElseIf clsCommon.myCstr(row.Cells("gvSourceCode").Value) = "" Then
                        Continue For
                    End If
                    Dim Amount As Decimal = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                    Dim Code As String = clsCommon.myCstr(row.Cells("gvSourceCode").Value)
                    Dim Name As String = clsCommon.myCstr(row.Cells("gvSourcename").Value)
                    Dim ChkNo As String = clsCommon.myCstr(row.Cells("gvCheckNo").Value)
                    Dim check As String = "Select top 1 1 from TSPL_RECEIPT_HEADER Where Cheque_No='" + ChkNo + "' "
                    Dim RcptNoFlag As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(check, tran))
                    If RcptNoFlag = 1 Then
                        common.clsCommon.MyMessageBoxShow(" '" + ChkNo + "' Cheque No. Is Already Used With Other Receipt Entry.")
                        tran.Rollback()
                        Return False
                    End If
                    Dim ChkDate As String = row.Cells("gvCheckDate").Value
                    Dim strnarration As String = clsCommon.myCstr(row.Cells("gvNarration").Value)
                    Dim strRcptType As String = ""
                    'If row.Cells("gvType").Value = "On-Account" Then
                    strRcptType = "O"
                    ' ElseIf row.Cells("gvType").Value = "Advance" Then
                    'strRcptType = "P"
                    ' End If
                    Dim qry As String = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + fndBankCode.Value + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Bank :" + fndBankCode.Value + ". Not found")
                    End If
                    Dim strDocumentNo As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                    If (strDocumentNo.Length >= 3) Then
                        strDocumentNo = strDocumentNo.Substring(strDocumentNo.Length - 3, 3)
                        If (IsNumeric(strDocumentNo)) Then
                            Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                        End If
                    Else
                        Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                    End If
                    Dim strRcptNo As String = ""
                    Dim strBankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
                    If clsCommon.CompairString(strBankType, "B") = CompairStringResult.Equal Then
                        strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.Bank, strDocumentNo, True)
                    ElseIf clsCommon.CompairString(strBankType, "C") = CompairStringResult.Equal Then
                        strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.Cash, strDocumentNo, True)
                    ElseIf clsCommon.CompairString(strBankType, "P") = CompairStringResult.Equal Then
                        strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.PettyCash, strDocumentNo, True)
                    ElseIf clsCommon.CompairString(strBankType, "O") = CompairStringResult.Equal Then
                        strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.Others, strDocumentNo, True)
                    Else
                        Throw New Exception("Plase set the Bank Type for Bank " + fndBankCode.Value)
                    End If

                    strQ = "select BANKACC,bank_type  from TSPL_BANK_MASTER where BANK_CODE='" + fndBankCode.Value + "'"
                    'Dim Drbank As SqlDataReader = connectSql.RunSqlReturnDR(strQ)
                    Dim BankType As String
                    Dim strBankAcc, strQuery As String
                    strBankAcc = ""
                    strQuery = ""
                    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strQ, tran)
                    If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                        For Each Drbank As DataRow In dt1.Rows
                            strBankAcc = Drbank("BANKACC").ToString()
                            BankType = Drbank("bank_type").ToString()
                        Next
                    End If
                    strQuery = " SELECT     TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN" &
                          " TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account" &
                          " where TSPL_CUSTOMER_MASTER.Cust_Code ='" + row.Cells(2).Value + "'"
                    Dim strRcvblAcc As String = connectSql.RunScalar(tran, strQuery)
                    Dim strAccSet As String
                    Dim Query As String = "select Cust_Account  from TSPL_CUSTOMER_MASTER  where Cust_Code  ='" + row.Cells(2).Value + "' "
                    strAccSet = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Query, tran))
                    clsDBFuncationality.SaveAStorePorcedure(tran, "sp_TSPL_RECEIPT_HEADER_INSERT", New SqlParameter("@Receipt_No", strRcptNo), New SqlParameter("@Receipt_Date", Format(dtDocDate.Value, "dd/MMM/yyy")), New SqlParameter("@Receipt_Post_Date", Format(dtDocDate.Value, "dd/MM/yyyy")), New SqlParameter("@Entry_Desc", strnarration), New SqlParameter("@Bank_Code", Me.fndBankCode.Value), New SqlParameter("@Receipt_Type", strRcptType), New SqlParameter("@Cust_Code", Code), New SqlParameter("@Customer_Name", Name), New SqlParameter("@Reference", ""), New SqlParameter("@Narration", strnarration), New SqlParameter("@Payment_Code", "CHEQUE"), New SqlParameter("@Cheque_No", ChkNo), New SqlParameter("@Cheque_Date", ChkDate), New SqlParameter("@Receipt_Amount", Amount), New SqlParameter("@Balance_Amt", Amount), New SqlParameter("@Document_No", ""), New SqlParameter("@UnApply_Amt", Amount), New SqlParameter("@Cust_Account", strAccSet), New SqlParameter("@Apply_By", ""), New SqlParameter("@Apply_To", ""), New SqlParameter("@Posted", "N"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(tran)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(tran)), New SqlParameter("@Level1_User_code", ""), New SqlParameter("@Level2_User_code", ""), New SqlParameter("@Level3_User_code", ""), New SqlParameter("@Level4_User_code", ""), New SqlParameter("@Level5_User_code", ""), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@Dr_Account", strBankAcc), New SqlParameter("@Cr_Account", strRcvblAcc), New SqlParameter("@UnApplied_balance", CDec(0)), New SqlParameter("@UnApplied_No", ""), New SqlParameter("@SecurityDeposit", "N"), New SqlParameter("@IsSalesmanType", "N"), New SqlParameter("@Salesman_Code", ""), New SqlParameter("@Salesman_Name", ""))
                    row.Cells("gvDocNo").Value = strRcptNo
                    strQ = "Update TSPL_RECEIPT_HEADER set QuickEntryNo='" + EntryNo + "' where Receipt_No='" + row.Cells("gvDocNo").Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(strQ, tran)

                Next

            ElseIf ddlType.Text = "Misc Receipt" Then

                EntryNo = fnAutoGenerateNo(dtDocDate.Value, tran)
                If MasterTemplate.Rows.Count <= 0 Then
                    Return False
                End If
                For Each row As GridViewRowInfo In MasterTemplate.Rows
                    If row.Cells("gvSelect").Value = False Then
                        Continue For
                    ElseIf clsCommon.myCstr(row.Cells("gvSourceCode").Value) = "" Then
                        Continue For
                    End If
                    Dim Amount As Decimal = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                    Dim AccCode As String = clsCommon.myCstr(row.Cells("gvSourceCode").Value)
                    Dim AccName As String = clsCommon.myCstr(row.Cells("gvSourcename").Value)
                    Dim ChkNo As String = clsCommon.myCstr(row.Cells("gvCheckNo").Value)
                    Dim check As String = "Select top 1 1 from TSPL_RECEIPT_HEADER Where Cheque_No='" + ChkNo + "' "
                    Dim RcptNoFlag As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(check, tran))
                    If RcptNoFlag = 1 Then
                        common.clsCommon.MyMessageBoxShow(" '" + ChkNo + "' Cheque No. Is Already Used With Other Receipt Entry.")
                        tran.Rollback()
                        Return False
                    End If
                    Dim ChkDate As String = row.Cells("gvCheckDate").Value
                    Dim strnarration As String = clsCommon.myCstr(row.Cells("gvNarration").Value)
                    Dim strRcptType As String = "M"

                    Dim qry As String = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + fndBankCode.Value + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Bank :" + fndBankCode.Value + ". Not found")
                    End If
                    Dim strDocumentNo As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                    Dim strBankAcc As String = strDocumentNo
                    If (strDocumentNo.Length >= 3) Then
                        strDocumentNo = strDocumentNo.Substring(strDocumentNo.Length - 3, 3)
                        If (IsNumeric(strDocumentNo)) Then
                            Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                        End If
                    Else
                        Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                    End If
                    Dim strRcptNo As String = ""
                    Dim strBankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
                    If clsCommon.CompairString(strBankType, "B") = CompairStringResult.Equal Then
                        strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.Bank, strDocumentNo, True)
                    ElseIf clsCommon.CompairString(strBankType, "C") = CompairStringResult.Equal Then
                        strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.Cash, strDocumentNo, True)
                    ElseIf clsCommon.CompairString(strBankType, "P") = CompairStringResult.Equal Then
                        strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.PettyCash, strDocumentNo, True)
                    ElseIf clsCommon.CompairString(strBankType, "O") = CompairStringResult.Equal Then
                        strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.Others, strDocumentNo, True)
                    Else
                        Throw New Exception("Plase set the Bank Type for Bank " + fndBankCode.Value)
                    End If


                    clsDBFuncationality.SaveAStorePorcedure(tran, "sp_TSPL_RECEIPT_HEADER_INSERT", New SqlParameter("@Receipt_No", strRcptNo), New SqlParameter("@Receipt_Date", Format(dtDocDate.Value, "dd/MMM/yyy")), New SqlParameter("@Receipt_Post_Date", Format(dtDocDate.Value, "dd/MMM/yyyy")), New SqlParameter("@Entry_Desc", strnarration), New SqlParameter("@Bank_Code", Me.fndBankCode.Value), New SqlParameter("@Receipt_Type", strRcptType), New SqlParameter("@Cust_Code", ""), New SqlParameter("@Customer_Name", ""), New SqlParameter("@Reference", ""), New SqlParameter("@Narration", strnarration), New SqlParameter("@Payment_Code", "CHEQUE"), New SqlParameter("@Cheque_No", ChkNo), New SqlParameter("@Cheque_Date", ChkDate), New SqlParameter("@Receipt_Amount", Amount), New SqlParameter("@Balance_Amt", Amount), New SqlParameter("@Document_No", ""), New SqlParameter("@UnApply_Amt", Amount), New SqlParameter("@Cust_Account", ""), New SqlParameter("@Apply_By", ""), New SqlParameter("@Apply_To", ""), New SqlParameter("@Posted", "N"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(tran)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(tran)), New SqlParameter("@Level1_User_code", ""), New SqlParameter("@Level2_User_code", ""), New SqlParameter("@Level3_User_code", ""), New SqlParameter("@Level4_User_code", ""), New SqlParameter("@Level5_User_code", ""), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@Dr_Account", strBankAcc), New SqlParameter("@Cr_Account", ""), New SqlParameter("@UnApplied_balance", CDec(0)), New SqlParameter("@UnApplied_No", ""), New SqlParameter("@SecurityDeposit", "N"), New SqlParameter("@IsSalesmanType", "N"), New SqlParameter("@Salesman_Code", ""), New SqlParameter("@Salesman_Name", ""))
                    row.Cells("gvDocNo").Value = strRcptNo
                    strQ = "Update TSPL_RECEIPT_HEADER set QuickEntryNo='" + EntryNo + "' where Receipt_No='" + row.Cells("gvDocNo").Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(strQ, tran)
                    clsDBFuncationality.SaveAStorePorcedure(tran, "sp_TSPL_RECEIPT_DETAIL_INSERT", New SqlParameter("@TagType", ""), New SqlParameter("@Receipt_No", strRcptNo), New SqlParameter("@Receipt_Line_No", 1), New SqlParameter("@Apply", "Y"), New SqlParameter("@Receipt_Type", "M"), New SqlParameter("@Document_No", ""), New SqlParameter("@Original_Amt", 0), New SqlParameter("@Pending_Balance", 0), New SqlParameter("@Applied_Amount", Amount), New SqlParameter("@Account_Code", AccCode), New SqlParameter("@Description", AccName), New SqlParameter("@Remarks", strnarration), New SqlParameter("@Comment", ""), New SqlParameter("@Adjustment_Account", ""), New SqlParameter("@Adjustment_Cost", 0), New SqlParameter("@Adjustment_No", 0))
                Next
            ElseIf ddlType.Text = "Payment" Then

                EntryNo = fnAutoGenerateNo(dtDocDate.Value, tran)
                If MasterTemplate.Rows.Count <= 0 Then
                    Return False
                End If
                For Each row As GridViewRowInfo In MasterTemplate.Rows
                    Dim STRPAYmentno As String = ""
                    Dim strvendoracctset As String = connectSql.RunScalar(tran, "select Vendor_Account  from TSPL_VENDOR_MASTER where Vendor_Code ='" + row.Cells("gvSourceCode").Value + "'")
                    Qry = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + fndBankCode.Value + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, tran)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Bank :" + fndBankCode.Value + ". Not found")
                    End If
                    Dim strDocumentNo As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                    If (strDocumentNo.Length >= 3) Then
                        strDocumentNo = strDocumentNo.Substring(strDocumentNo.Length - 3, 3)
                        If (IsNumeric(strDocumentNo)) Then
                            Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                        End If
                    Else
                        Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                    End If
                    Dim strBankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))

                    If clsCommon.CompairString(strBankType, "B") = CompairStringResult.Equal Then
                        STRPAYmentno = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Payment, clsDocTransactionType.Bank, strDocumentNo, True)
                    ElseIf clsCommon.CompairString(strBankType, "C") = CompairStringResult.Equal Then
                        STRPAYmentno = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Payment, clsDocTransactionType.Cash, strDocumentNo, True)
                    ElseIf clsCommon.CompairString(strBankType, "P") = CompairStringResult.Equal Then
                        STRPAYmentno = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Payment, clsDocTransactionType.PettyCash, strDocumentNo, True)
                    ElseIf clsCommon.CompairString(strBankType, "O") = CompairStringResult.Equal Then
                        STRPAYmentno = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Payment, clsDocTransactionType.Others, strDocumentNo, True)
                    Else
                        Throw New Exception("Please set the Bank Type for Bank " + fndBankCode.Value)
                    End If

                    If clsCommon.myLen(STRPAYmentno) <= 0 Then
                        Throw New Exception("Error in Genenrating Payment No")
                    End If
                    If row.Cells("gvSelect").Value = False Then
                        Continue For
                    ElseIf clsCommon.myCstr(row.Cells("gvSourceCode").Value) = "" Then
                        Continue For
                    End If
                    Dim Amount As Decimal = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                    Dim Code As String = clsCommon.myCstr(row.Cells("gvSourceCode").Value)
                    Dim Name As String = clsCommon.myCstr(row.Cells("gvSourcename").Value)
                    Dim ChkNo As String = clsCommon.myCstr(row.Cells("gvCheckNo").Value)
                    Dim check As String = "Select top 1 1 from TSPL_PAYMENT_HEADER Where Cheque_No='" + ChkNo + "' "
                    Dim RcptNoFlag As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(check, tran))
                    If RcptNoFlag = 1 Then
                        common.clsCommon.MyMessageBoxShow(" '" + ChkNo + "' Cheque No. Is Already Used With Other Payment Entry.")
                        tran.Rollback()
                        Return False
                    End If
                    Dim ChkDate As String = row.Cells("gvCheckDate").Value
                    Dim strnarration As String = clsCommon.myCstr(row.Cells("gvNarration").Value)
                    Dim strRcptType As String = ""
                    ' If row.Cells("gvType").Value = "On-Account" Then
                    strRcptType = "O"
                    'ElseIf row.Cells("gvType").Value = "Advance" Then
                    'strRcptType = "P"
                    ' End If
                    If strRcptType = "O" Then
                        Dim straccount As String = CStr(connectSql.RunScalar(tran, "select s.Advance_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + Code + "' "))
                        Dim strbankacct As String = CStr(connectSql.RunScalar(tran, "select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + fndBankCode.Value + "'"))
                        Dim tdsamount As Decimal = 0
                        'objRemittance.SaveData(objRemittance, STRPAYmentno, trans)
                        Dim totalprepayment As Decimal = CDec(Amount) '- tdsamounttotal
                        clsDBFuncationality.SaveAStorePorcedure(tran, "SP_TSPL_PAYMENT_HEADER_INSERT", New SqlParameter("@paymentno", STRPAYmentno), New SqlParameter("@debitacct", straccount), New SqlParameter("@creditacct", strbankacct), New SqlParameter("@paymentdate", clsCommon.GetPrintDate(dtDocDate.Value, "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@paymentpostdate", clsCommon.GetPrintDate(dtDocDate.Value, "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@bankcode", fndBankCode.Value), New SqlParameter("@paymenttype", "O"), New SqlParameter("@vendorcode", Code), New SqlParameter("@vendorname", Name), New SqlParameter("@remitto", ""), New SqlParameter("@entrydesc", strnarration), New SqlParameter("@reference", ""), New SqlParameter("@narration", ""), New SqlParameter("@paymentcode", "CHEQUE"), New SqlParameter("@chequeno", ChkNo), New SqlParameter("@chequedate", clsCommon.GetPrintDate(ChkDate, "dd/MMM/yyyy")), New SqlParameter("@paymentamount", Amount), New SqlParameter("@vendoraccountset", strvendoracctset), New SqlParameter("@applyby", ""), New SqlParameter("@applyto", ""), New SqlParameter("@post", "0"), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@compcode", companyCode))
                        row.Cells("gvDocNo").Value = STRPAYmentno
                        'Dim strQ As String = "update TSPL_PAYMENT_HEADER set fifo_balance=" + Amount + " where Payment_No ='" + STRPAYmentno + "'"
                        'clsDBFuncationality.ExecuteNonQuery(strQ, tran)
                    ElseIf strRcptType = "P" Then
                        Dim straccount As String = CStr(connectSql.RunScalar(tran, "select s.Advance_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + Code + "' "))
                        Dim strbankacct As String = CStr(connectSql.RunScalar(tran, "select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + fndBankCode.Value + "'"))
                        Dim tdsamount As Decimal = 0
                        'objRemittance.SaveData(objRemittance, STRPAYmentno, trans)
                        Dim totalprepayment As Decimal = Amount ' CDec(txtamt.Text) - tdsamounttotal
                        'connectSql.RunSpTransaction(trans, "SP_TSPL_PAYMENT_HEADER_INSERT", New SqlParameter("@paymentno", STRPAYmentno), New SqlParameter("@debitacct", straccount), New SqlParameter("@creditacct", strbankacct), New SqlParameter("@paymentdate", paymentdate), New SqlParameter("@paymentpostdate", "NULL"), New SqlParameter("@bankcode", fndBankCode.Value), New SqlParameter("@paymenttype", strgeneratecode), New SqlParameter("@vendorcode", fndvendorcode.Value), New SqlParameter("@vendorname", txtvendorname.Text), New SqlParameter("@remitto", txtremitto.Text), New SqlParameter("@entrydesc", txtdescription.Text), New SqlParameter("@reference", txtreference.Text), New SqlParameter("@narration", txtnarration.Text), New SqlParameter("@paymentcode", fndpaymentcode.txtValue.Text), New SqlParameter("@chequeno", txtchequeno.Text), New SqlParameter("@chequedate", chequedate), New SqlParameter("@paymentamount", txtamt.Text), New SqlParameter("@vendoraccountset", strvendoracctset), New SqlParameter("@applyby", ddlapplyby.Text), New SqlParameter("@applyto", fndapplyto.txtValue.Text), New SqlParameter("@post", "N"), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
                        'connectSql.RunSqlTransaction(trans, "update TSPL_PAYMENT_HEADER set tds_amount = '" + CStr(tdsamount) + "', total_prepayment = '" + CStr(txtamt.Text) + "', payment_amount = '" + CStr(totalprepayment) + "' where payment_no = '" + STRPAYmentno + "' ")
                        clsDBFuncationality.SaveAStorePorcedure(tran, "SP_TSPL_PAYMENT_HEADER_INSERT", New SqlParameter("@paymentno", STRPAYmentno), New SqlParameter("@debitacct", straccount), New SqlParameter("@creditacct", strbankacct), New SqlParameter("@paymentdate", clsCommon.GetPrintDate(dtDocDate.Value, "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@paymentpostdate", clsCommon.GetPrintDate(dtDocDate.Value, "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@bankcode", fndBankCode.Value), New SqlParameter("@paymenttype", "A"), New SqlParameter("@vendorcode", Code), New SqlParameter("@vendorname", Name), New SqlParameter("@remitto", ""), New SqlParameter("@entrydesc", strnarration), New SqlParameter("@reference", ""), New SqlParameter("@narration", ""), New SqlParameter("@paymentcode", "CHEQUE"), New SqlParameter("@chequeno", ChkNo), New SqlParameter("@chequedate", clsCommon.GetPrintDate(ChkDate, "dd/MMM/yyyy")), New SqlParameter("@paymentamount", Amount), New SqlParameter("@vendoraccountset", strvendoracctset), New SqlParameter("@applyby", ""), New SqlParameter("@applyto", ""), New SqlParameter("@post", "0"), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@compcode", companyCode))
                        clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set tds_amount = '" + CStr(0) + "', total_prepayment = '" + CStr(Amount) + "', payment_amount = '" + CStr(totalprepayment) + "', balance_amt = '" + CStr(totalprepayment) + "' where payment_no = '" + STRPAYmentno + "'", tran)
                        row.Cells("gvDocNo").Value = STRPAYmentno
                    End If
                    strQ = "Update TSPL_PAYMENT_HEADER set QuickEntryNo='" + EntryNo + "' where Payment_No='" + row.Cells("gvDocNo").Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(strQ, tran)
                Next
            End If
            txtEntryNo.Value = EntryNo
            tran.Commit()
            btnSave.Text = "Update"
            Return True
        Catch ex As Exception
            tran.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Receipt Entry", MessageBoxButtons.OK)
            Return False
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If btnSave.Text = "Save" And clsCommon.myLen(txtEntryNo.Value) <= 0 Then
                IsNewEntry = True
            Else
                IsNewEntry = False
            End If
            If SaveData(IsNewEntry) Then
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(fndBankCode.Value) <= 0 Then
                fndBankCode.Focus()
                Throw New Exception("Please select Bank.")
            ElseIf MasterTemplate.Rows.Count <= 0 Then
                Throw New Exception("Please insert atleast single account in grid.")
            Else
                'Dim BankType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Bank_type from TSPL_BANK_MASTER Where BANK_CODE='" + fndBankCode.Value + "'"))
                Dim Counter As Integer = 0
                arrDocNo = New List(Of String)
                For Each row As GridViewRowInfo In MasterTemplate.Rows
                    If (row.Cells("gvSelect").Value = 1 Or row.Cells("gvSelect").Value = True) And clsCommon.myLen(row.Cells("gvSelect").Value) And row.Cells("gvAmount").Value > 0 Then
                        Counter += 1
                        If clsCommon.CompairString(clsCommon.myCstr(row.Cells("gvPaymentMode").Value), "Cheque") = CompairStringResult.Equal Then
                            If clsCommon.myLen(row.Cells("gvCheckNo").Value) > 0 Then
                                If clsCommon.myLen(row.Cells("gvCheckDate").Value) <= 0 Then
                                    Throw New Exception("Please insert Cheque Date at line '" + clsCommon.myCstr(Counter) + "'.")
                                End If
                            Else
                                Throw New Exception("Please insert Cheque No at line '" + clsCommon.myCstr(Counter) + "'.")
                            End If
                        End If
                        If objCommonVar.IsDemoERP Then
                            If clsCommon.CompairString(ddlType.Text, "Receipt") = CompairStringResult.Equal OrElse clsCommon.CompairString(ddlType.Text, "Payment") = CompairStringResult.Equal Then
                                If row.Cells("gvCForm").Value = 1 Or row.Cells("gvCForm").Value = True Then
                                    If clsCommon.myLen(row.Cells("gvCFormDoc").Value) <= 0 Then
                                        Throw New Exception("Please enter CForm Invoice at line '" + clsCommon.myCstr(Counter) + "'.")
                                    End If
                                End If
                            End If
                        End If
                    End If
                    If Counter <= 0 Then
                        Throw New Exception("Please Select atleast single account in grid.")
                    End If
                    If clsCommon.myLen(row.Cells("gvDocNo").Value) > 0 Then
                        arrDocNo.Add(clsCommon.myCstr(row.Cells("gvDocNo").Value))
                    End If
                Next
            End If

            'If clsCommon.myLen(txtEntryNo.Value) > 0 Then
            '    Qry = "select   Posted   from TSPL_RECEIPT_HEADER where QuickEntryNo='" + txtEntryNo.Value + "'"
            '    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry)), "Y") = CompairStringResult.Equal Then
            '        Throw New Exception("Posted Transaction")
            '    End If
            'End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal IsNewEntry As Boolean) As Boolean
        Try
            If AllowToSave() Then
                tran = clsDBFuncationality.GetTransactin()
                Try
                    Dim strDocArr As String = clsCommon.GetMulcallString(arrDocNo)
                    Dim qry As String
                    Dim EntryNo As String
                    If IsNewEntry Then
                        EntryNo = fnAutoGenerateNo(dtDocDate.Value, tran)
                    Else
                        EntryNo = clsCommon.myCstr(txtEntryNo.Value)
                    End If
                    If clsCommon.myLen(EntryNo) <= 0 Then
                        Throw New Exception("Error in code generation.")
                    End If
                    '-----By balwinder on 24/04/2017
                    qry = "select Payment_No as DocNo,'Receipt' as DocType from TSPL_PAYMENT_HEADER where QuickEntryNo='" + EntryNo + "' and Posted='1'" + Environment.NewLine +
                    " union all " + Environment.NewLine +
                    "select Receipt_No as DocNo,'Payment' as DocType from TSPL_RECEIPT_HEADER where QuickEntryNo='" + EntryNo + "' and Posted='Y'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Throw New Exception("Please check your prefix.There is also Posted quick entry No " + EntryNo)
                    End If
                    '-----End By balwinder on 24/04/2017
                    If ddlType.Text = "Receipt" Then
                        If arrDocNo.Count > 0 Then
                            qry = "Delete From TSPL_RECEIPT_HEADER Where QuickEntryNo='" + EntryNo + "' AND Receipt_No Not in (" + strDocArr + ")"
                        Else
                            qry = "Delete From TSPL_RECEIPT_HEADER Where QuickEntryNo='" + EntryNo + "' "
                        End If
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)
                        For Each row As GridViewRowInfo In MasterTemplate.Rows
                            If (row.Cells("gvSelect").Value = 1 Or row.Cells("gvSelect").Value = True) And clsCommon.myLen(row.Cells("gvSelect").Value) And row.Cells("gvAmount").Value > 0 Then
                                If clsCommon.myLen(clsCommon.myCstr(row.Cells("gvDocNo").Value)) > 0 Then
                                    clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.ReceiptEntry, clsCommon.myCstr(row.Cells("gvDocNo").Value), tran)
                                End If

                                Dim obj As New clsRcptEntryHeader()
                                obj.Receipt_No = clsCommon.myCstr(row.Cells("gvDocNo").Value)
                                obj.Entry_Desc = clsCommon.myCstr(row.Cells("gvNarration").Value)
                                obj.Receipt_Date = dtDocDate.Value
                                obj.Receipt_Post_Date = dtDocDate.Value
                                obj.Bank_Code = clsCommon.myCstr(fndBankCode.Value)
                                obj.Receipt_Type = "O"
                                obj.Payment_Code = clsCommon.myCstr(row.Cells("gvPaymentMode").Value)
                                obj.Location_GL_Code = clsCommon.myCstr(row.Cells("gvLocation").Value)
                                obj.CFormRecd = IIf(row.Cells("gvCForm").Value, "1", "0")
                                obj.CForm_InvoiceNo = clsCommon.myCstr(row.Cells("gvCFormDoc").Value)
                                obj.Cheque_From = clsCommon.myCstr(row.Cells("gvFromBank").Value)
                                If clsCommon.CompairString(obj.Payment_Code, "Cheque") = CompairStringResult.Equal Then
                                    obj.Cheque_No = clsCommon.myCstr(row.Cells("gvCheckNo").Value)
                                    If clsCommon.myLen(obj.Cheque_No) > 0 Then
                                        'Dim check As String
                                        'If clsCommon.myLen(obj.Receipt_No) > 0 Then
                                        '    check = "Select top 1 1 from TSPL_RECEIPT_HEADER Where Cheque_No='" + obj.Cheque_No + "' And Receipt_No <> '" + obj.Receipt_No + "' "
                                        'Else
                                        '    check = "Select top 1 1 from TSPL_RECEIPT_HEADER Where Cheque_No='" + obj.Cheque_No + "' "
                                        'End If
                                        'Dim RcptNoFlag As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(check, tran))
                                        'If RcptNoFlag = 1 Then
                                        '    Throw New Exception(" '" + obj.Cheque_No + "' Cheque No. Is Already Used With Other Receipt Entry.")
                                        'End If
                                        obj.Cheque_Date = row.Cells("gvCheckDate").Value
                                    Else
                                        Throw New Exception("Enter Cheque no against customer '" + +"'")
                                    End If
                                Else
                                    obj.Cheque_No = ""
                                    obj.Cheque_Date = Nothing
                                End If
                                obj.Cust_Code = clsCommon.myCstr(row.Cells("gvSourceCode").Value)
                                obj.Receipt_Amount = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.Balance_Amt = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.UnApply_Amt = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.FIFO_Balance = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.RECEIVED_AMOUNT_BASE_CURRENCY = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.IsSalesmanType = "N"
                                obj.SecurityDeposit = "N"
                                obj.IsRecoCleared = "N"
                                obj.IsChkReverse = "N"
                                obj.ConvRate = 1
                                obj.ConvRateOld = 1
                                If clsCommon.myLen(obj.Cust_Code) > 0 Then
                                    Dim VenCurr As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select currency_code from TSPL_Customer_MASTER where Cust_CODE='" & clsCommon.myCstr(obj.Cust_Code) & "'", tran))
                                    If clsCommon.myLen(VenCurr) > 0 Then
                                        obj.CURRENCY_CODE = VenCurr
                                    End If

                                    ''richa agarwal 4 Dec,2017 -- regarding conversion code and applicable date in case of mullticurrency

                                    dt = Nothing
                                    If clsCommon.myLen(obj.CURRENCY_CODE) > 0 Then
                                        dt = clsModuleCurrencyMapping.GetLatestCurConvRateDT(obj.Receipt_Date, obj.CURRENCY_CODE, tran)
                                        If dt.Rows.Count = 0 Then
                                            If obj.CURRENCY_CODE = objCommonVar.BaseCurrencyCode Then
                                                obj.ConvRate = 1
                                                obj.ConvRateOld = 1
                                            Else
                                                clsCommon.MyMessageBoxShow(Me, "Conversion rate not entered for currency '" & obj.CURRENCY_CODE & "'")
                                                Exit Function
                                            End If
                                        Else
                                            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0).Item("Rate"))
                                            obj.ConvRateOld = clsCommon.myCdbl(dt.Rows(0).Item("Rate"))
                                            obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0).Item("FROM_DATE"), "dd/MMM/yyyy")
                                        End If
                                    End If
                                    ''-----------------end of multicurrency

                                End If
                                obj.Narration = obj.Entry_Desc
                                obj.QuickEntryNo = EntryNo
                                If arrDocNo.Contains(obj.Receipt_No) Then
                                    obj.SaveData(obj, False, tran)
                                Else
                                    obj.SaveData(obj, True, tran)
                                End If
                            End If
                        Next

                    ElseIf ddlType.Text = "Misc Receipt" Then
                        If arrDocNo.Count > 0 Then
                            qry = "Delete From TSPL_RECEIPT_DETAIL Where Receipt_No in (Select Receipt_No From TSPL_RECEIPT_HEADER Where QuickEntryNo='" + EntryNo + "' AND Receipt_No Not in (" + strDocArr + "))"
                        Else
                            qry = "Delete From TSPL_RECEIPT_DETAIL Where Receipt_No in (Select Receipt_No From TSPL_RECEIPT_HEADER Where QuickEntryNo='" + EntryNo + "')"
                        End If
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)
                        If arrDocNo.Count > 0 Then
                            qry = "Delete From TSPL_RECEIPT_HEADER Where QuickEntryNo='" + EntryNo + "' AND Receipt_No Not in (" + strDocArr + ")"
                        Else
                            qry = "Delete From TSPL_RECEIPT_HEADER Where QuickEntryNo='" + EntryNo + "'"
                        End If
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)

                        For Each row As GridViewRowInfo In MasterTemplate.Rows
                            If (row.Cells("gvSelect").Value = 1 Or row.Cells("gvSelect").Value = True) And clsCommon.myLen(row.Cells("gvSelect").Value) And row.Cells("gvAmount").Value > 0 Then
                                If clsCommon.myLen(clsCommon.myCstr(row.Cells("gvDocNo").Value)) > 0 Then
                                    clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.ReceiptEntry, clsCommon.myCstr(row.Cells("gvDocNo").Value), tran)
                                End If
                                Dim obj As New clsRcptEntryHeader()
                                obj.Receipt_No = clsCommon.myCstr(row.Cells("gvDocNo").Value)
                                obj.Entry_Desc = clsCommon.myCstr(row.Cells("gvNarration").Value)
                                obj.Receipt_Date = dtDocDate.Value
                                obj.Receipt_Post_Date = dtDocDate.Value
                                obj.Bank_Code = clsCommon.myCstr(fndBankCode.Value)
                                obj.Receipt_Type = "M"
                                obj.Payment_Code = clsCommon.myCstr(row.Cells("gvPaymentMode").Value)

                                If clsCommon.CompairString(obj.Payment_Code, "Cheque") = CompairStringResult.Equal Then
                                    obj.Cheque_No = clsCommon.myCstr(row.Cells("gvCheckNo").Value)
                                    If clsCommon.myLen(obj.Cheque_No) > 0 Then
                                        Dim check As String
                                        If clsCommon.myLen(obj.Receipt_No) > 0 Then
                                            check = "Select top 1 1 from TSPL_RECEIPT_HEADER Where Cheque_No='" + obj.Cheque_No + "' And Receipt_No <> '" + obj.Receipt_No + "' "
                                        Else
                                            check = "Select top 1 1 from TSPL_RECEIPT_HEADER Where Cheque_No='" + obj.Cheque_No + "' "
                                        End If

                                        Dim RcptNoFlag As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(check, tran))
                                        If RcptNoFlag = 1 Then
                                            Throw New Exception(" '" + obj.Cheque_No + "' Cheque No. Is Already Used With Other Receipt Entry.")
                                        End If
                                        obj.Cheque_Date = row.Cells("gvCheckDate").Value
                                    Else
                                        Throw New Exception("Enter Cheque no against customer '" + +"'")
                                    End If
                                Else
                                    obj.Cheque_No = ""
                                    obj.Cheque_Date = Nothing
                                End If
                                obj.Receipt_Amount = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.Balance_Amt = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.UnApply_Amt = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.FIFO_Balance = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.RECEIVED_AMOUNT_BASE_CURRENCY = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.IsSalesmanType = "N"
                                obj.SecurityDeposit = "N"
                                obj.IsRecoCleared = "N"
                                obj.IsChkReverse = "N"
                                obj.Narration = obj.Entry_Desc
                                obj.QuickEntryNo = EntryNo
                                obj.ConvRate = 1
                                obj.ConvRateOld = 1
                                If clsCommon.myLen(obj.Cust_Code) > 0 Then
                                    Dim VenCurr As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select currency_code from TSPL_Customer_MASTER where Cust_CODE='" & clsCommon.myCstr(obj.Cust_Code) & "'", tran))
                                    If clsCommon.myLen(VenCurr) > 0 Then
                                        obj.CURRENCY_CODE = VenCurr
                                    End If
                                End If
                                '------------Detail Data-------------------
                                obj.ArrTr = New List(Of clsReceiptDettail)
                                Dim objTr As New clsReceiptDettail()
                                objTr.Apply = "Y"
                                objTr.Receipt_Type = "M"
                                objTr.Applied_Amount = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                objTr.Account_Code = clsCommon.myCstr(row.Cells("gvSourceCode").Value)
                                objTr.Description = clsCommon.myCstr(row.Cells("gvSourcename").Value)
                                objTr.Remarks = clsCommon.myCstr(row.Cells("gvNarration").Value)
                                obj.ArrTr.Add(objTr)
                                '-------------------------------------------
                                If arrDocNo.Contains(obj.Receipt_No) Then
                                    obj.SaveData(obj, False, tran)
                                Else
                                    obj.SaveData(obj, True, tran)
                                End If
                            End If
                        Next

                    ElseIf ddlType.Text = "Payment" Then
                        If arrDocNo.Count > 0 Then
                            qry = "Delete From TSPL_PAYMENT_HEADER WHERE QuickEntryNo='" + EntryNo + "' AND Payment_No Not in (" + strDocArr + ")"
                        Else
                            qry = "Delete From TSPL_PAYMENT_HEADER WHERE QuickEntryNo='" + EntryNo + "' "
                        End If
                        clsDBFuncationality.ExecuteNonQuery(qry, tran)
                        For Each row As GridViewRowInfo In MasterTemplate.Rows
                            If (row.Cells("gvSelect").Value = 1 Or row.Cells("gvSelect").Value = True) And clsCommon.myLen(row.Cells("gvSelect").Value) And row.Cells("gvAmount").Value > 0 Then
                                If clsCommon.myLen(clsCommon.myCstr(row.Cells("gvDocNo").Value)) > 0 Then
                                    clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.PaymentEntryNew, clsCommon.myCstr(row.Cells("gvDocNo").Value), tran)
                                End If
                                Dim obj As New clsPaymentHeader()
                                obj.Payment_No = clsCommon.myCstr(row.Cells("gvDocNo").Value)
                                obj.Entry_Desc = clsCommon.myCstr(row.Cells("gvNarration").Value)
                                obj.Payment_Date = dtDocDate.Value
                                obj.Payment_Post_Date = dtDocDate.Value
                                obj.Bank_Code = clsCommon.myCstr(fndBankCode.Value)
                                obj.Payment_Type = "OA"
                                obj.Vendor_Code = clsCommon.myCstr(row.Cells("gvSourceCode").Value)
                                obj.Vendor_Name = clsCommon.myCstr(row.Cells("gvSourcename").Value)
                                obj.Location_GL_Code = clsCommon.myCstr(row.Cells("gvlocation").Value)
                                'obj.Payment_Code = "Cheque"
                                obj.Payment_Code = clsCommon.myCstr(row.Cells("gvPaymentMode").Value)
                                '' Anubhooti 07-Apr-2015 (Location not saving on payment screen)
                                ' obj.Location_GL_Code = clsCommon.myCstr(txtLocation.Text)
                                If clsCommon.myLen(obj.Location_Code) > 0 Then
                                    obj.Location_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Location_Desc,'') AS Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" + clsCommon.myCstr(obj.Location_Code) + "'", tran))
                                Else
                                    obj.Location_Description = ""
                                End If
                                If clsCommon.myLen(obj.Vendor_Code) > 0 Then
                                    Dim VenCurr As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select currency_code from TSPL_VENDOR_MASTER where VENDOR_CODE='" & clsCommon.myCstr(obj.Vendor_Code) & "'", tran))
                                    If clsCommon.myLen(VenCurr) > 0 Then
                                        obj.CURRENCY_CODE = VenCurr
                                    End If
                                End If
                                obj.ConvRate = 1
                                obj.ConvRateOld = 1
                                ''
                                If clsCommon.CompairString(obj.Payment_Code, "Cheque") = CompairStringResult.Equal Then
                                    obj.Cheque_No = clsCommon.myCstr(row.Cells("gvCheckNo").Value)
                                    If clsCommon.myLen(obj.Cheque_No) > 0 Then
                                        Dim check As String
                                        If clsCommon.myLen(obj.Payment_No) > 0 Then
                                            check = "Select top 1 1 from TSPL_PAYMENT_HEADER Where Cheque_No='" + obj.Cheque_No + "' And Payment_No <> '" + obj.Payment_No + "' "
                                        Else
                                            check = "Select top 1 1 from TSPL_PAYMENT_HEADER Where Cheque_No='" + obj.Cheque_No + "' "
                                        End If

                                        Dim RcptNoFlag As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(check, tran))
                                        If RcptNoFlag = 1 Then
                                            Throw New Exception(" '" + obj.Cheque_No + "' Cheque No. Is Already Used With Other Receipt Entry.")
                                        End If
                                        obj.Cheque_Date = row.Cells("gvCheckDate").Value
                                        obj.PDC_Cheque = IIf(clsCommon.myCstr(clsCommon.myCstr(row.Cells("gvPDCCheque").Value)) = "True", "Y", "N")
                                        If clsCommon.CompairString(obj.PDC_Cheque, "Y") = CompairStringResult.Equal Then
                                            obj.Payment_Date = obj.Cheque_Date
                                        End If
                                        obj.Account_Payee = IIf(clsCommon.myCstr(clsCommon.myCstr(row.Cells("gvACPayee").Value)) = "True", "1", "0")
                                    Else
                                        Throw New Exception("Enter Cheque no against customer '" + +"'")
                                    End If
                                Else
                                    obj.Cheque_No = ""
                                    obj.Cheque_Date = Nothing
                                End If

                                obj.Total_Prepayment = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.Payment_Amount = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.Balance_Amt = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.PAYMENT_AMOUNT_BASE_CURRENCY = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                                obj.IsChkReverse = "N"
                                obj.QuickEntryNo = EntryNo
                                If arrDocNo.Contains(obj.Payment_No) Then
                                    obj.SaveData1(obj, False, tran)
                                Else
                                    obj.SaveData1(obj, True, tran)
                                End If

                            End If
                        Next
                    End If
                    tran.Commit()
                    fillData(EntryNo)
                    DataSendForApproval()
                Catch ex As Exception
                    tran.Rollback()
                    Throw New Exception(ex.Message)
                End Try
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Function FunUpdate() As Boolean
        Try
            tran = clsDBFuncationality.GetTransactin()
            Dim strQ As String = ""
            If ddlType.Text = "Receipt" Then
                For Each row As GridViewRowInfo In MasterTemplate.Rows
                    If row.Cells("gvSelect").Value = False Then
                        strQ = "delete from TSPL_RECEIPT_HEADER where QuickEntryNo  ='" + clsCommon.myCstr(txtEntryNo.Value) + "' and Cheque_No ='" + clsCommon.myCstr(row.Cells("gvCheckNo").Value) + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQ, tran)
                        Continue For
                    ElseIf clsCommon.myCstr(row.Cells("gvSourceCode").Value) = "" Then
                        strQ = "delete from TSPL_RECEIPT_HEADER where QuickEntryNo  ='" + clsCommon.myCstr(txtEntryNo.Value) + "' and Cheque_No ='" + clsCommon.myCstr(row.Cells("gvCheckNo").Value) + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQ, tran)
                        Continue For
                    End If
                    Dim Amount As Decimal = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                    Dim Code As String = clsCommon.myCstr(row.Cells("gvSourceCode").Value)
                    Dim Name As String = clsCommon.myCstr(row.Cells("gvSourcename").Value)
                    Dim ChkNo As String = clsCommon.myCstr(row.Cells("gvCheckNo").Value)
                    Dim check As String = "Select top 1 1 from TSPL_RECEIPT_HEADER Where Cheque_No='" + ChkNo + "' AND QuickEntryNo <> '" + clsCommon.myCstr(txtEntryNo.Value) + "' "
                    Dim RcptNoFlag As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(check, tran))
                    If RcptNoFlag = 1 Then
                        common.clsCommon.MyMessageBoxShow(" '" + ChkNo + "' Cheque No. Is Already Used With Other Receipt Entry.")
                        tran.Rollback()
                        Return False
                    End If
                    Dim ChkDate As String = row.Cells("gvCheckDate").Value
                    Dim strnarration As String = clsCommon.myCstr(row.Cells("gvNarration").Value)
                    Dim DocNo As String = clsDBFuncationality.getSingleValue("select Receipt_No  from TSPL_RECEIPT_HEADER where QuickEntryNo ='" + clsCommon.myCstr(txtEntryNo.Value) + "' and Cheque_No ='" + clsCommon.myCstr(row.Cells("gvCheckNo").Value) + "'", tran)
                    Dim strRcptType As String = ""
                    'If row.Cells("gvType").Value = "On-Account" Then
                    strRcptType = "O"
                    ' ElseIf row.Cells("gvType").Value = "Advance" Then
                    ' strRcptType = "P"
                    'End If
                    strQ = "select BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + fndBankCode.Value + "'"
                    Dim strBankAcc As String = connectSql.RunScalar(tran, strQ)
                    Dim strQuery As String = " SELECT     TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct FROM  TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN" &
                                " TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account" &
                                " where TSPL_CUSTOMER_MASTER.Cust_Code ='" + Code + "'"
                    Dim strRcvblAcc As String = connectSql.RunScalar(tran, strQuery)
                    Dim strAccSet As String
                    Dim Query As String = "select Cust_Account  from TSPL_CUSTOMER_MASTER  where Cust_Code  ='" + Code + "' "
                    strAccSet = connectSql.RunScalar(tran, Query)

                    If DocNo = "" Or DocNo Is Nothing Then
                        Dim qry As String = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + fndBankCode.Value + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Bank :" + fndBankCode.Value + ". Not found")
                        End If
                        Dim strDocumentNo As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                        If (strDocumentNo.Length >= 3) Then
                            strDocumentNo = strDocumentNo.Substring(strDocumentNo.Length - 3, 3)
                            If (IsNumeric(strDocumentNo)) Then
                                Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                            End If
                        Else
                            Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                        End If
                        Dim strRcptNo As String = ""
                        Dim strBankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
                        If clsCommon.CompairString(strBankType, "B") = CompairStringResult.Equal Then
                            strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.Bank, strDocumentNo, True)
                        ElseIf clsCommon.CompairString(strBankType, "C") = CompairStringResult.Equal Then
                            strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.Cash, strDocumentNo, True)
                        ElseIf clsCommon.CompairString(strBankType, "P") = CompairStringResult.Equal Then
                            strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.PettyCash, strDocumentNo, True)
                        ElseIf clsCommon.CompairString(strBankType, "O") = CompairStringResult.Equal Then
                            strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.Others, strDocumentNo, True)
                        Else
                            Throw New Exception("Please set the Bank Type for Bank " + fndBankCode.Value)
                        End If
                        clsDBFuncationality.SaveAStorePorcedure(tran, "sp_TSPL_RECEIPT_HEADER_INSERT", New SqlParameter("@Receipt_No", strRcptNo), New SqlParameter("@Receipt_Date", Format(dtDocDate.Value, "dd/MM/yyy")), New SqlParameter("@Receipt_Post_Date", Format(dtDocDate.Value, "dd/MM/yyyy")), New SqlParameter("@Entry_Desc", strnarration), New SqlParameter("@Bank_Code", Me.fndBankCode.Value), New SqlParameter("@Receipt_Type", strRcptType), New SqlParameter("@Cust_Code", Code), New SqlParameter("@Customer_Name", Name), New SqlParameter("@Reference", ""), New SqlParameter("@Narration", strnarration), New SqlParameter("@Payment_Code", "CHEQUE"), New SqlParameter("@Cheque_No", ChkNo), New SqlParameter("@Cheque_Date", ChkDate), New SqlParameter("@Receipt_Amount", Amount), New SqlParameter("@Balance_Amt", Amount), New SqlParameter("@Document_No", ""), New SqlParameter("@UnApply_Amt", Amount), New SqlParameter("@Cust_Account", strAccSet), New SqlParameter("@Apply_By", ""), New SqlParameter("@Apply_To", ""), New SqlParameter("@Posted", "N"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(tran)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(tran)), New SqlParameter("@Level1_User_code", ""), New SqlParameter("@Level2_User_code", ""), New SqlParameter("@Level3_User_code", ""), New SqlParameter("@Level4_User_code", ""), New SqlParameter("@Level5_User_code", ""), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@Dr_Account", strBankAcc), New SqlParameter("@Cr_Account", strRcvblAcc), New SqlParameter("@UnApplied_balance", CDec(0)), New SqlParameter("@UnApplied_No", ""), New SqlParameter("@SecurityDeposit", "N"), New SqlParameter("@IsSalesmanType", "N"), New SqlParameter("@Salesman_Code", ""), New SqlParameter("@Salesman_Name", ""))
                        row.Cells("gvDocNo").Value = strRcptNo
                        strQ = "Update TSPL_RECEIPT_HEADER set QuickEntryNo='" + txtEntryNo.Value + "' where Receipt_No='" + row.Cells("gvDocNo").Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQ, tran)
                    Else
                        connectSql.RunSpTransaction(tran, "sp_TSPL_RECEIPT_HEADER_UPDATE", New SqlParameter("@Receipt_No", DocNo), New SqlParameter("@Receipt_Date", Format(Me.dtDocDate.Value, "dd/MM/yyy")), New SqlParameter("@Receipt_Post_Date", Format(Me.dtDocDate.Value, "dd/MM/yyyy")), New SqlParameter("@Entry_Desc", strnarration), New SqlParameter("@Bank_Code", Me.fndBankCode.Value), New SqlParameter("@Receipt_Type", strRcptType), New SqlParameter("@Cust_Code", Code), New SqlParameter("@Customer_Name", Name), New SqlParameter("@Reference", ""), New SqlParameter("@Narration", ""), New SqlParameter("@Payment_Code", "CHEQUE"), New SqlParameter("@Cheque_No", ChkNo), New SqlParameter("@Cheque_Date", ChkDate), New SqlParameter("@Receipt_Amount", Amount), New SqlParameter("@Balance_Amt", Convert.ToDecimal(Amount)), New SqlParameter("@Document_No", ""), New SqlParameter("@UnApply_Amt", Amount), New SqlParameter("@Cust_Account", strAccSet), New SqlParameter("@Apply_By", ""), New SqlParameter("@Apply_To", ""), New SqlParameter("@Posted", "N"), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(tran)), New SqlParameter("@Level1_User_code", ""), New SqlParameter("@Level2_User_code", ""), New SqlParameter("@Level3_User_code", ""), New SqlParameter("@Level4_User_code", ""), New SqlParameter("@Level5_User_code", ""), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@Dr_Account", strBankAcc), New SqlParameter("@Cr_Account", strRcvblAcc), New SqlParameter("@UnApplied_balance", CDec(0)), New SqlParameter("@UnApplied_No", ""), New SqlParameter("@SecurityDeposit", "N"), New SqlParameter("@IsSalesmanType", "N"), New SqlParameter("@Salesman_Code", ""), New SqlParameter("@Salesman_Name", ""))
                    End If
                Next
            ElseIf ddlType.Text = "Misc Receipt" Then
                For Each row As GridViewRowInfo In MasterTemplate.Rows
                    ' Dim ReceiptNo As String
                    Dim DeleteQry As String = "delete from TSPL_RECEIPT_HEADER where QuickEntryNo  ='" + clsCommon.myCstr(txtEntryNo.Value) + "' and Cheque_No ='" + clsCommon.myCstr(row.Cells("gvCheckNo").Value) + "'"
                    If row.Cells("gvSelect").Value = False Then
                        clsDBFuncationality.ExecuteNonQuery(strQ, tran)
                        Continue For
                    ElseIf clsCommon.myCstr(row.Cells("gvSourceCode").Value) = "" Then
                        clsDBFuncationality.ExecuteNonQuery(strQ, tran)
                        Continue For
                    End If
                    Dim Amount As Decimal = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                    Dim AccCode As String = clsCommon.myCstr(row.Cells("gvSourceCode").Value)
                    Dim AccName As String = clsCommon.myCstr(row.Cells("gvSourcename").Value)
                    Dim ChkNo As String = clsCommon.myCstr(row.Cells("gvCheckNo").Value)
                    Dim check As String = "Select top 1 1 from TSPL_RECEIPT_HEADER Where Cheque_No='" + ChkNo + "' "
                    Dim RcptNoFlag As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(check, tran))
                    If RcptNoFlag = 1 Then
                        common.clsCommon.MyMessageBoxShow(" '" + ChkNo + "' Cheque No. Is Already Used With Other Receipt Entry.")
                        tran.Rollback()
                        Return False
                    End If
                    Dim ChkDate As String = row.Cells("gvCheckDate").Value
                    Dim strnarration As String = clsCommon.myCstr(row.Cells("gvNarration").Value)
                    Dim DocNo As String = clsDBFuncationality.getSingleValue("select Receipt_No  from TSPL_RECEIPT_HEADER where QuickEntryNo ='" + clsCommon.myCstr(txtEntryNo.Value) + "' and Cheque_No ='" + clsCommon.myCstr(row.Cells("gvCheckNo").Value) + "'", tran)
                    Dim strRcptType As String = "M"
                    strQ = "select BANKACC  from TSPL_BANK_MASTER where BANK_CODE='" + fndBankCode.Value + "'"
                    Dim strBankAcc As String = connectSql.RunScalar(tran, strQ)

                    If DocNo = "" Or DocNo Is Nothing Then
                        Dim qry As String = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + fndBankCode.Value + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Bank :" + fndBankCode.Value + ". Not found")
                        End If
                        Dim strDocumentNo As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                        If (strDocumentNo.Length >= 3) Then
                            strDocumentNo = strDocumentNo.Substring(strDocumentNo.Length - 3, 3)
                            If (IsNumeric(strDocumentNo)) Then
                                Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                            End If
                        Else
                            Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                        End If
                        Dim strRcptNo As String = ""
                        Dim strBankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))
                        If clsCommon.CompairString(strBankType, "B") = CompairStringResult.Equal Then
                            strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.Bank, strDocumentNo, True)
                        ElseIf clsCommon.CompairString(strBankType, "C") = CompairStringResult.Equal Then
                            strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.Cash, strDocumentNo, True)
                        ElseIf clsCommon.CompairString(strBankType, "P") = CompairStringResult.Equal Then
                            strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.PettyCash, strDocumentNo, True)
                        ElseIf clsCommon.CompairString(strBankType, "O") = CompairStringResult.Equal Then
                            strRcptNo = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Receipt, clsDocTransactionType.Others, strDocumentNo, True)
                        Else
                            Throw New Exception("Please set the Bank Type for Bank " + fndBankCode.Value)
                        End If
                        clsDBFuncationality.SaveAStorePorcedure(tran, "sp_TSPL_RECEIPT_HEADER_INSERT", New SqlParameter("@Receipt_No", strRcptNo), New SqlParameter("@Receipt_Date", Format(dtDocDate.Value, "dd/MM/yyy")), New SqlParameter("@Receipt_Post_Date", Format(dtDocDate.Value, "dd/MM/yyyy")), New SqlParameter("@Entry_Desc", strnarration), New SqlParameter("@Bank_Code", Me.fndBankCode.Value), New SqlParameter("@Receipt_Type", strRcptType), New SqlParameter("@Cust_Code", ""), New SqlParameter("@Customer_Name", Name), New SqlParameter("@Reference", ""), New SqlParameter("@Narration", strnarration), New SqlParameter("@Payment_Code", "CHEQUE"), New SqlParameter("@Cheque_No", ChkNo), New SqlParameter("@Cheque_Date", ChkDate), New SqlParameter("@Receipt_Amount", Amount), New SqlParameter("@Balance_Amt", Amount), New SqlParameter("@Document_No", ""), New SqlParameter("@UnApply_Amt", Amount), New SqlParameter("@Cust_Account", ""), New SqlParameter("@Apply_By", ""), New SqlParameter("@Apply_To", ""), New SqlParameter("@Posted", "N"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(tran)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(tran)), New SqlParameter("@Level1_User_code", ""), New SqlParameter("@Level2_User_code", ""), New SqlParameter("@Level3_User_code", ""), New SqlParameter("@Level4_User_code", ""), New SqlParameter("@Level5_User_code", ""), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@Dr_Account", strBankAcc), New SqlParameter("@Cr_Account", ""), New SqlParameter("@UnApplied_balance", CDec(0)), New SqlParameter("@UnApplied_No", ""), New SqlParameter("@SecurityDeposit", "N"), New SqlParameter("@IsSalesmanType", "N"), New SqlParameter("@Salesman_Code", ""), New SqlParameter("@Salesman_Name", ""))
                        row.Cells("gvDocNo").Value = strRcptNo
                        strQ = "Update TSPL_RECEIPT_HEADER set QuickEntryNo='" + txtEntryNo.Value + "' where Receipt_No='" + row.Cells("gvDocNo").Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQ, tran)
                    Else
                        connectSql.RunSpTransaction(tran, "sp_TSPL_RECEIPT_HEADER_UPDATE", New SqlParameter("@Receipt_No", DocNo), New SqlParameter("@Receipt_Date", Format(Me.dtDocDate.Value, "dd/MM/yyy")), New SqlParameter("@Receipt_Post_Date", Format(Me.dtDocDate.Value, "dd/MM/yyyy")), New SqlParameter("@Entry_Desc", strnarration), New SqlParameter("@Bank_Code", Me.fndBankCode.Value), New SqlParameter("@Receipt_Type", strRcptType), New SqlParameter("@Cust_Code", ""), New SqlParameter("@Customer_Name", Name), New SqlParameter("@Reference", ""), New SqlParameter("@Narration", ""), New SqlParameter("@Payment_Code", "CHEQUE"), New SqlParameter("@Cheque_No", ChkNo), New SqlParameter("@Cheque_Date", ChkDate), New SqlParameter("@Receipt_Amount", Amount), New SqlParameter("@Balance_Amt", Convert.ToDecimal(Amount)), New SqlParameter("@Document_No", ""), New SqlParameter("@UnApply_Amt", Amount), New SqlParameter("@Cust_Account", ""), New SqlParameter("@Apply_By", ""), New SqlParameter("@Apply_To", ""), New SqlParameter("@Posted", "N"), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(tran)), New SqlParameter("@Level1_User_code", ""), New SqlParameter("@Level2_User_code", ""), New SqlParameter("@Level3_User_code", ""), New SqlParameter("@Level4_User_code", ""), New SqlParameter("@Level5_User_code", ""), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@Dr_Account", strBankAcc), New SqlParameter("@Cr_Account", ""), New SqlParameter("@UnApplied_balance", CDec(0)), New SqlParameter("@UnApplied_No", ""), New SqlParameter("@SecurityDeposit", "N"), New SqlParameter("@IsSalesmanType", "N"), New SqlParameter("@Salesman_Code", ""), New SqlParameter("@Salesman_Name", ""))
                    End If
                    clsDBFuncationality.SaveAStorePorcedure(tran, "sp_TSPL_RECEIPT_DETAIL_INSERT", New SqlParameter("@TagType", ""), New SqlParameter("@Receipt_No", DocNo), New SqlParameter("@Receipt_Line_No", 1), New SqlParameter("@Apply", "Y"), New SqlParameter("@Receipt_Type", "M"), New SqlParameter("@Document_No", ""), New SqlParameter("@Original_Amt", 0), New SqlParameter("@Pending_Balance", 0), New SqlParameter("@Applied_Amount", Amount), New SqlParameter("@Account_Code", AccCode), New SqlParameter("@Description", AccName), New SqlParameter("@Remarks", strnarration), New SqlParameter("@Comment", ""), New SqlParameter("@Adjustment_Account", ""), New SqlParameter("@Adjustment_Cost", 0), New SqlParameter("@Adjustment_No", 0))
                Next

            ElseIf ddlType.Text = "Payment" Then
                Dim paymentdate As String = clsCommon.GetPrintDate(dtDocDate.Value, "dd/MMM/yyyy hh:mm tt")
                For Each row As GridViewRowInfo In MasterTemplate.Rows
                    If row.Cells("gvSelect").Value = False Then
                        strQ = "delete from TSPL_PAYMENT_HEADER where QuickEntryNo  ='" + clsCommon.myCstr(txtEntryNo.Value) + "' and Cheque_No ='" + clsCommon.myCstr(row.Cells("gvCheckNo").Value) + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQ, tran)
                        Continue For
                    ElseIf clsCommon.myCstr(row.Cells("gvSourceCode").Value) = "" Then
                        strQ = "delete from TSPL_PAYMENT_HEADER where QuickEntryNo  ='" + clsCommon.myCstr(txtEntryNo.Value) + "' and Cheque_No ='" + clsCommon.myCstr(row.Cells("gvCheckNo").Value) + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQ, tran)
                        Continue For
                    End If
                    Dim Amount As Decimal = clsCommon.myCdbl(row.Cells("gvAmount").Value)
                    Dim Code As String = clsCommon.myCstr(row.Cells("gvSourceCode").Value)
                    Dim Name As String = clsCommon.myCstr(row.Cells("gvSourcename").Value)
                    Dim ChkNo As String = clsCommon.myCstr(row.Cells("gvCheckNo").Value)
                    Dim check As String = "Select top 1 1 from TSPL_PAYMENT_HEADER Where Cheque_No='" + ChkNo + "' AND QuickEntryNo <> '" + clsCommon.myCstr(txtEntryNo.Value) + "' "
                    Dim RcptNoFlag As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(check, tran))
                    If RcptNoFlag = 1 Then
                        common.clsCommon.MyMessageBoxShow(" '" + ChkNo + "' Cheque No. Is Already Used With Other Payment Entry.")
                        tran.Rollback()
                        Return False
                    End If
                    Dim ChkDate As String = row.Cells("gvCheckDate").Value
                    Dim strnarration As String = clsCommon.myCstr(row.Cells("gvNarration").Value)
                    Dim DocNo As String = clsDBFuncationality.getSingleValue("select Payment_No  from TSPL_PAYMENT_HEADER where QuickEntryNo ='" + clsCommon.myCstr(txtEntryNo.Value) + "' and Cheque_No ='" + clsCommon.myCstr(row.Cells("gvCheckNo").Value) + "'", tran)
                    Dim strRcptType As String = ""
                    'If row.Cells("gvType").Value = "On-Account" Then
                    strRcptType = "O"
                    ' ElseIf row.Cells("gvType").Value = "Advance" Then
                    'strRcptType = "P"
                    ' End If
                    Dim strvendoracctset As String = connectSql.RunScalar(tran, "select Vendor_Account  from TSPL_VENDOR_MASTER where Vendor_Code ='" + Code + "'")
                    If strRcptType = "O" Then
                        Dim straccount As String = CStr(connectSql.RunScalar(tran, "select s.Advance_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + Code + "' "))
                        Dim strbankacct As String = CStr(connectSql.RunScalar(tran, "select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + fndBankCode.Value + "'"))
                        Dim tdsamount As Decimal = 0
                        If DocNo = "" Or DocNo Is Nothing Then
                            Dim STRPAYmentno As String = ""
                            strvendoracctset = connectSql.RunScalar(tran, "select Vendor_Account  from TSPL_VENDOR_MASTER where Vendor_Code ='" + row.Cells("gvSourceCode").Value + "'")
                            Qry = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + fndBankCode.Value + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, tran)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("Bank :" + fndBankCode.Value + ". Not found")
                            End If
                            Dim strDocumentNo As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                            If (strDocumentNo.Length >= 3) Then
                                strDocumentNo = strDocumentNo.Substring(strDocumentNo.Length - 3, 3)
                                If (IsNumeric(strDocumentNo)) Then
                                    Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                                End If
                            Else
                                Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                            End If
                            Dim strBankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))

                            If clsCommon.CompairString(strBankType, "B") = CompairStringResult.Equal Then
                                STRPAYmentno = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Payment, clsDocTransactionType.Bank, strDocumentNo, True)
                            ElseIf clsCommon.CompairString(strBankType, "C") = CompairStringResult.Equal Then
                                STRPAYmentno = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Payment, clsDocTransactionType.Cash, strDocumentNo, True)
                            ElseIf clsCommon.CompairString(strBankType, "P") = CompairStringResult.Equal Then
                                STRPAYmentno = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Payment, clsDocTransactionType.PettyCash, strDocumentNo, True)
                            ElseIf clsCommon.CompairString(strBankType, "O") = CompairStringResult.Equal Then
                                STRPAYmentno = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Payment, clsDocTransactionType.Others, strDocumentNo, True)
                            Else
                                Throw New Exception("Plase set the Bank Type for Bank " + fndBankCode.Value)
                            End If

                            If clsCommon.myLen(STRPAYmentno) <= 0 Then
                                Throw New Exception("Error in Genenrating Payment No")
                            End If
                            straccount = CStr(connectSql.RunScalar(tran, "select s.Advance_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + Code + "' "))
                            strbankacct = CStr(connectSql.RunScalar(tran, "select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + fndBankCode.Value + "'"))
                            tdsamount = 0
                            'Dim totalprepayment As Decimal = CDec(Amount) '- tdsamounttotal
                            clsDBFuncationality.SaveAStorePorcedure(tran, "SP_TSPL_PAYMENT_HEADER_INSERT", New SqlParameter("@paymentno", STRPAYmentno), New SqlParameter("@debitacct", straccount), New SqlParameter("@creditacct", strbankacct), New SqlParameter("@paymentdate", paymentdate), New SqlParameter("@paymentpostdate", paymentdate), New SqlParameter("@bankcode", fndBankCode.Value), New SqlParameter("@paymenttype", "O"), New SqlParameter("@vendorcode", Code), New SqlParameter("@vendorname", Name), New SqlParameter("@remitto", ""), New SqlParameter("@entrydesc", strnarration), New SqlParameter("@reference", ""), New SqlParameter("@narration", ""), New SqlParameter("@paymentcode", "CHEQUE"), New SqlParameter("@chequeno", ChkNo), New SqlParameter("@chequedate", clsCommon.GetPrintDate(ChkDate, "dd/MMM/yyyy")), New SqlParameter("@paymentamount", Amount), New SqlParameter("@vendoraccountset", strvendoracctset), New SqlParameter("@applyby", ""), New SqlParameter("@applyto", ""), New SqlParameter("@post", "0"), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@compcode", companyCode))
                            row.Cells("gvDocNo").Value = STRPAYmentno
                            strQ = "Update TSPL_PAYMENT_HEADER set QuickEntryNo='" + txtEntryNo.Value + "' where Payment_No='" + row.Cells("gvDocNo").Value + "'"
                            clsDBFuncationality.ExecuteNonQuery(strQ, tran)
                        Else
                            Dim totalprepayment As Decimal = Amount '- tdsamount
                            clsDBFuncationality.SaveAStorePorcedure(tran, "SP_TSPL_PAYMENT_HEADER_UPDATE", New SqlParameter("@paymentno", DocNo), New SqlParameter("@debitacct", straccount), New SqlParameter("@creditacct", strbankacct), New SqlParameter("@paymentdate", paymentdate), New SqlParameter("@paymentpostdate", paymentdate), New SqlParameter("@bankcode", fndBankCode.Value), New SqlParameter("@paymenttype", "O"), New SqlParameter("@vendorcode", Code), New SqlParameter("@vendorname", Name), New SqlParameter("@remitto", ""), New SqlParameter("@entrydesc", strnarration), New SqlParameter("@reference", ""), New SqlParameter("@narration", ""), New SqlParameter("@paymentcode", "CHEQUE"), New SqlParameter("@chequeno", ChkNo), New SqlParameter("@chequedate", clsCommon.GetPrintDate(ChkDate, "dd/MMM/yyyy")), New SqlParameter("@paymentamount", Amount), New SqlParameter("@vendoraccountset", strvendoracctset), New SqlParameter("@applyby", ""), New SqlParameter("@applyto", ""), New SqlParameter("@post", "0"), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@compcode", companyCode))
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set tds_amount = '" + CStr(tdsamount) + "', total_prepayment = '" + CStr(Amount) + "', payment_amount = '" + CStr(totalprepayment) + "' where payment_no = '" + DocNo + "'", tran)
                        End If
                    ElseIf strRcptType = "P" Then
                        Dim straccount As String = CStr(connectSql.RunScalar(tran, "select s.Advance_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + Code + "' "))
                        Dim strbankacct As String = CStr(connectSql.RunScalar(tran, "select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + fndBankCode.Value + "'"))
                        Dim tdsamount As Decimal = 0
                        If DocNo = "" Or DocNo Is Nothing Then
                            Dim STRPAYmentno As String = ""
                            strvendoracctset = connectSql.RunScalar(tran, "select Vendor_Account  from TSPL_VENDOR_MASTER where Vendor_Code ='" + row.Cells("gvSourceCode").Value + "'")
                            Qry = "select Bank_type,BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + fndBankCode.Value + "'"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, tran)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                Throw New Exception("Bank :" + fndBankCode.Value + ". Not found")
                            End If
                            Dim strDocumentNo As String = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
                            If (strDocumentNo.Length >= 3) Then
                                strDocumentNo = strDocumentNo.Substring(strDocumentNo.Length - 3, 3)
                                If (IsNumeric(strDocumentNo)) Then
                                    Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                                End If
                            Else
                                Throw New Exception("Bank Master's Bank Account should be have location segment Type")
                            End If
                            Dim strBankType As String = clsCommon.myCstr(dt.Rows(0)("Bank_type"))

                            If clsCommon.CompairString(strBankType, "B") = CompairStringResult.Equal Then
                                STRPAYmentno = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Payment, clsDocTransactionType.Bank, strDocumentNo, True)
                            ElseIf clsCommon.CompairString(strBankType, "C") = CompairStringResult.Equal Then
                                STRPAYmentno = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Payment, clsDocTransactionType.Cash, strDocumentNo, True)
                            ElseIf clsCommon.CompairString(strBankType, "P") = CompairStringResult.Equal Then
                                STRPAYmentno = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Payment, clsDocTransactionType.PettyCash, strDocumentNo, True)
                            ElseIf clsCommon.CompairString(strBankType, "O") = CompairStringResult.Equal Then
                                STRPAYmentno = clsERPFuncationality.GetNextCode(tran, dtDocDate.Value, clsDocType.Payment, clsDocTransactionType.Others, strDocumentNo, True)
                            Else
                                Throw New Exception("Plase set the Bank Type for Bank " + fndBankCode.Value)
                            End If

                            If clsCommon.myLen(STRPAYmentno) <= 0 Then
                                Throw New Exception("Error in Genenrating Payment No")
                            End If
                            straccount = CStr(connectSql.RunScalar(tran, "select s.Advance_Account  from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code where m.Vendor_Code = '" + Code + "' "))
                            strbankacct = CStr(connectSql.RunScalar(tran, "select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + fndBankCode.Value + "'"))
                            tdsamount = 0
                            'Dim totalprepayment As Decimal = Amount ' CDec(txtamt.Text) - tdsamounttotal
                            clsDBFuncationality.SaveAStorePorcedure(tran, "SP_TSPL_PAYMENT_HEADER_INSERT", New SqlParameter("@paymentno", STRPAYmentno), New SqlParameter("@debitacct", straccount), New SqlParameter("@creditacct", strbankacct), New SqlParameter("@paymentdate", paymentdate), New SqlParameter("@paymentpostdate", paymentdate), New SqlParameter("@bankcode", fndBankCode.Value), New SqlParameter("@paymenttype", "A"), New SqlParameter("@vendorcode", Code), New SqlParameter("@vendorname", Name), New SqlParameter("@remitto", ""), New SqlParameter("@entrydesc", strnarration), New SqlParameter("@reference", ""), New SqlParameter("@narration", ""), New SqlParameter("@paymentcode", "CHEQUE"), New SqlParameter("@chequeno", ChkNo), New SqlParameter("@chequedate", clsCommon.GetPrintDate(ChkDate, "dd/MMM/yyyy")), New SqlParameter("@paymentamount", Amount), New SqlParameter("@vendoraccountset", strvendoracctset), New SqlParameter("@applyby", ""), New SqlParameter("@applyto", ""), New SqlParameter("@post", "0"), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@compcode", companyCode))
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set tds_amount = '" + CStr(0) + "', total_prepayment = '" + CStr(Amount) + "', payment_amount = '" + CStr(Amount) + "', balance_amt = '" + CStr(Amount) + "' where payment_no = '" + STRPAYmentno + "'", tran)
                            row.Cells("gvDocNo").Value = STRPAYmentno
                            strQ = "Update TSPL_PAYMENT_HEADER set QuickEntryNo='" + txtEntryNo.Value + "' where Payment_No='" + row.Cells("gvDocNo").Value + "'"
                            clsDBFuncationality.ExecuteNonQuery(strQ, tran)
                        Else
                            Dim totalprepayment As Decimal = Amount '- tdsamount
                            clsDBFuncationality.SaveAStorePorcedure(tran, "SP_TSPL_PAYMENT_HEADER_UPDATE", New SqlParameter("@paymentno", DocNo), New SqlParameter("@debitacct", straccount), New SqlParameter("@creditacct", strbankacct), New SqlParameter("@paymentdate", paymentdate), New SqlParameter("@paymentpostdate", paymentdate), New SqlParameter("@bankcode", fndBankCode.Value), New SqlParameter("@paymenttype", "A"), New SqlParameter("@vendorcode", Code), New SqlParameter("@vendorname", Name), New SqlParameter("@remitto", ""), New SqlParameter("@entrydesc", strnarration), New SqlParameter("@reference", ""), New SqlParameter("@narration", ""), New SqlParameter("@paymentcode", "CHEQUE"), New SqlParameter("@chequeno", ChkNo), New SqlParameter("@chequedate", clsCommon.GetPrintDate(ChkDate, "dd/MMM/yyyy")), New SqlParameter("@paymentamount", Amount), New SqlParameter("@vendoraccountset", strvendoracctset), New SqlParameter("@applyby", ""), New SqlParameter("@applyto", ""), New SqlParameter("@post", "0"), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt")), New SqlParameter("@compcode", companyCode))
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set tds_amount = '" + CStr(tdsamount) + "', total_prepayment = '" + CStr(Amount) + "', payment_amount = '" + CStr(totalprepayment) + "' where payment_no = '" + DocNo + "'", tran)
                        End If
                    End If
                Next
            End If
            tran.Commit()
            Return True
        Catch ex As Exception
            tran.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Receipt Entry", MessageBoxButtons.OK)
            Return False
        End Try
    End Function

    Public Function fnAutoGenerateNo(ByVal dtTransDate As Date, Optional ByVal tran1 As SqlTransaction = Nothing) As String
        Dim str As String = clsERPFuncationality.GetNextCode(tran1, dtTransDate, clsDocType.QuickBookEntry, "", "")
        If (clsCommon.myLen(str) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If
        Return str
    End Function
    Sub DataSendForApproval()
        Try
            Dim documentCounter As Integer = 0
            Dim xNewDesc As String = ""
            For Each row As GridViewRowInfo In MasterTemplate.Rows
                If clsCommon.myLen(clsCommon.myCstr(row.Cells("gvDocNo").Value)) > 0 Then
                    clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.ReceiptEntry, clsCommon.myCstr(row.Cells("gvDocNo").Value), tran)
                    xNewDesc = "Party Name : " + clsCommon.myCstr(row.Cells("gvSourceCode").Value)

                    xNewDesc = xNewDesc + Environment.NewLine + "Description : " + clsCommon.myCstr(row.Cells("gvDocNo").Value)

                    If ddlType.Text = "Receipt" OrElse ddlType.Text = "Misc Receipt" Then
                        clsApply_Approval.CheckApprovalRequired(clsUserMgtCode.ReceiptEntry, clsCommon.myCstr(row.Cells("gvDocNo").Value), dtDocDate.Value, clsCommon.myCstr(xNewDesc), clsCommon.myCstr(row.Cells("gvNarration").Value), clsCommon.myCdbl(row.Cells("gvAmount").Value), 0, "", tran, documentCounter)
                        documentCounter = documentCounter + 1
                    ElseIf ddlType.Text = "Payment" Then
                        clsApply_Approval.CheckApprovalRequired(clsUserMgtCode.PaymentEntryNew, clsCommon.myCstr(row.Cells("gvDocNo").Value), dtDocDate.Value, clsCommon.myCstr(xNewDesc), clsCommon.myCstr(row.Cells("gvNarration").Value), clsCommon.myCdbl(row.Cells("gvAmount").Value), 0, "", tran, documentCounter)
                        documentCounter = documentCounter + 1
                    End If
                End If
            Next


        Catch ex As Exception

        End Try
    End Sub

    'Public Function fnAutoGenerateNo(Optional ByVal tran1 As SqlTransaction = Nothing) As String
    '    Dim Maxvlu, strQuery As String
    '    Dim NxtMaxNo As Int32
    '    Dim myDs As DataSet
    '    strQuery = "select max(xxx.QuickEntryNo)  from (select Distinct QuickEntryNo ,'Receipt' as [Type] from TSPL_RECEIPT_HEADER Union All select Distinct QuickEntryNo ,'Payment' as [Type] from TSPL_PAYMENT_HEADER) as xxx   where xxx.QuickEntryNo like '%QCKN%' "
    '    myDs = connectSql.RunSQLReturnDS(tran, strQuery)
    '    If myDs.Tables(0).Rows.Count > 0 Then
    '        If myDs.Tables(0).Rows(0)(0).ToString <> "" Then
    '            Maxvlu = myDs.Tables(0).Rows(0)(0).ToString()
    '            Maxvlu = Maxvlu.Remove(0, 4)
    '            NxtMaxNo = Convert.ToInt32(Maxvlu.ToString())
    '            NxtMaxNo = NxtMaxNo + 1
    '            Dim strCount As String
    '            strCount = NxtMaxNo.ToString()
    '            If strCount.Length = 1 Then
    '                Maxvlu = "QCKN" & "000" & NxtMaxNo.ToString()
    '            ElseIf strCount.Length = 2 Then
    '                Maxvlu = "QCKN" & "00" & NxtMaxNo.ToString()
    '            ElseIf strCount.Length = 3 Then
    '                Maxvlu = "QCKN" & "0" & NxtMaxNo.ToString()
    '            ElseIf strCount.Length = 4 Then
    '                Maxvlu = "QCKN" & NxtMaxNo.ToString()
    '            End If
    '            Return Maxvlu
    '        Else
    '            Maxvlu = "QCKN0001"
    '            Return Maxvlu
    '        End If
    '    Else
    '        Maxvlu = "QCKN0001"
    '        Return Maxvlu
    '    End If
    '    Return Maxvlu
    'End Function
    Private Sub fndBank__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBankCode._MYValidating
        Dim strWhrcls As String = ""
        Dim Qry As String = clsERPFuncationality.glbankqueryNew(strWhrcls)
        fndBankCode.Value = clsCommon.ShowSelectForm("Bank Filter1", Qry, "Code", "Bank_type IN ('B','C') AND " & strWhrcls & "", fndBankCode.Value, "Code", isButtonClicked)
        FillBankDetails()
    End Sub
    Private Sub MasterTemplate_CreateRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCreateRowEventArgs) Handles MasterTemplate.CreateRow
        Try
            If IsLoadData = False Then
                MasterTemplate.CurrentRow.Cells("gvSelect").Value = True
                MasterTemplate.CurrentRow.Cells("gvType").Value = "On-Account"
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtEntryNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtEntryNo._MYNavigator

        Dim qry As String = " select * from( select 1 as byte,'Receipt' as [Type], QuickEntryNo ,Receipt_No ,Receipt_Date ,Bank_Code ,case when Receipt_Type='O' then 'On-Account' when Receipt_Type='P' then 'Advance' end as Receipt_Type ,Cust_Code ,Customer_Name ,Entry_Desc ,Cheque_No , CONVERT(DATE,Cheque_Date,103) as Cheque_Date , Payment_Code ,Receipt_Amount ,case when Posted='Y' then 'Posted' when Posted='N' then 'Open' end as Posted   from TSPL_RECEIPT_HEADER " &
              " union all  select 1 as byte,'Payment' as [Type], QuickEntryNo ,Payment_No  ,Payment_Date  ,Bank_Code ,case when Payment_Type ='OA' then 'On-Account' when Payment_Type='AV' then 'Advance' end as Receipt_Type ,Vendor_Code  ,Vendor_Name  ,Entry_Desc ,Cheque_No ,Cheque_Date , Payment_Code ,Payment_Amount  ,case when Posted='Y' then 'Posted' when Posted='N' then 'Open' end as Posted   from TSPL_PAYMENT_HEADER) as  xxx" &
              " where 2=2 "

        Dim Arrloc As New ArrayList
        Dim ArrAcc As New ArrayList
        clsERPFuncationality.GlLOCandACCArray(Arrloc, ArrAcc)
        Dim WhrCls As String = " "
        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
        Else
            WhrCls += "  AND (substring(TSPL_BANK_MASTER.BANKACC,(len(TSPL_BANK_MASTER.BANKACC)-2),3) IN (" + clsCommon.GetMulcallString(Arrloc) + ") OR TSPL_BANK_MASTER.BANKACC IN (" + clsCommon.GetMulcallString(ArrAcc) + "))"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and xxx.QuickEntryNo=(select min(QuickEntryNo) from ( select QuickEntryNo, Bank_Code from TSPL_RECEIPT_HEADER where QuickEntryNo<>'' union all select QuickEntryNo, Bank_Code from TSPL_PAYMENT_HEADER where QuickEntryNo<>'' ) as xx Left Outer Join TSPL_BANK_MASTER on xx.Bank_Code=TSPL_BANK_MASTER.BANK_CODE Where 1=1 " + WhrCls + ")"
            Case NavigatorType.Last
                qry += " and xxx.QuickEntryNo=(select max(QuickEntryNo) from ( select QuickEntryNo, Bank_Code from TSPL_RECEIPT_HEADER where QuickEntryNo<>'' union all select QuickEntryNo, Bank_Code from TSPL_PAYMENT_HEADER where QuickEntryNo<>'' ) as xx Left Outer Join TSPL_BANK_MASTER on xx.Bank_Code=TSPL_BANK_MASTER.BANK_CODE Where 1=1 " + WhrCls + ")"
            Case NavigatorType.Next
                qry += " and xxx.QuickEntryNo=(select MIN(QuickEntryNo) from ( select QuickEntryNo, Bank_Code from TSPL_RECEIPT_HEADER where QuickEntryNo<>'' union all select QuickEntryNo, Bank_Code from TSPL_PAYMENT_HEADER where QuickEntryNo<>'' ) as xx Left Outer Join TSPL_BANK_MASTER on xx.Bank_Code=TSPL_BANK_MASTER.BANK_CODE where xx.QuickEntryNo > '" + txtEntryNo.Value + "' " + WhrCls + ")"
            Case NavigatorType.Previous
                qry += " and xxx.QuickEntryNo=(select MAX(QuickEntryNo) from ( select QuickEntryNo, Bank_Code from TSPL_RECEIPT_HEADER where QuickEntryNo<>'' union all select QuickEntryNo, Bank_Code from TSPL_PAYMENT_HEADER where QuickEntryNo<>'' ) as xx Left Outer Join TSPL_BANK_MASTER on xx.Bank_Code=TSPL_BANK_MASTER.BANK_CODE where xx.QuickEntryNo < '" + txtEntryNo.Value + "' " + WhrCls + ")"
            Case NavigatorType.Current
                qry += " and xxx.QuickEntryNo='" + txtEntryNo.Value + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtEntryNo.Value = clsCommon.myCstr(dt.Rows(0)("QuickEntryNo"))
            fillData(txtEntryNo.Value)
        End If
    End Sub
    Private Sub txtEntryNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtEntryNo._MYValidating
        'Qry = "select Distinct QuickEntryNo ,'Receipt' as [Type],Receipt_Date from TSPL_RECEIPT_HEADER Union All select Distinct QuickEntryNo ,'Payment' as [Type],Payment_Date from TSPL_PAYMENT_HEADER "

        '----------Added By--Pankaj Kumar-----For GL Security----- ''richa agarwal 01 Apr,2019 show status in finder ERO/01/04/19-000536
        Qry = "Select QuickEntryNo, Type, Receipt_Date, TSPL_BANK_MASTER. BANK_CODE, DESCRIPTION,Status, ADD1, ADD2, ADD3, CITY, STATE, POSTAL, COUNTRY, CONTACT, PHONE, FAX, INACTIVE, BANKACCNUMBER, BANKACC, WRITEOFFACC, Bank_type from (select Distinct QuickEntryNo ,Case When Receipt_Type='O' Then 'Receipt' When Receipt_Type='M' Then 'Misc Receipt' End as [Type],Receipt_Date, Bank_Code,CASE WHEN ISNULL(Posted,'N')='N' THEN 'Pending' else 'Approved' end as Status  from TSPL_RECEIPT_HEADER Union select Distinct QuickEntryNo ,'Payment' as [Type], CONVERT(VARCHAR,Payment_Date,102) as Payment_Date, Bank_Code ,CASE WHEN ISNULL(Posted,'0')='0'  THEN 'Pending' else 'Approved' end as Status from TSPL_PAYMENT_HEADER ) AAA Left Outer Join TSPL_BANK_MASTER on AAA.Bank_Code=TSPL_BANK_MASTER.BANK_CODE"
        Dim Arrloc As New ArrayList
        Dim ArrAcc As New ArrayList
        clsERPFuncationality.GlLOCandACCArray(Arrloc, ArrAcc)
        Dim WhrCls As String = " ISNULL(QuickEntryNo,'')<>'' "
        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
        Else
            WhrCls += "  AND (substring(TSPL_BANK_MASTER.BANKACC,(len(TSPL_BANK_MASTER.BANKACC)-2),3) IN (" + clsCommon.GetMulcallString(Arrloc) + ") OR TSPL_BANK_MASTER.BANKACC IN (" + clsCommon.GetMulcallString(ArrAcc) + "))"
        End If
        txtEntryNo.Value = clsCommon.ShowSelectForm("Quick_Book_Entry", Qry, "QuickEntryNo", WhrCls, txtEntryNo.Value, "Receipt_Date desc", isButtonClicked)
        '-------------------------Code Ends Here------------------
        'txtEntryNo.Value = clsCommon.ShowSelectForm("Quick_Book_Entry", Qry, "QuickEntryNo", "(QuickEntryNo is not null) and ( QuickEntryNo<>'')", txtEntryNo.Value, "QuickEntryNo", isButtonClicked)
        If txtEntryNo.Value <> "" Then
            fillData(txtEntryNo.Value)
        End If
    End Sub
    Public Sub fillData(ByVal EntryNo As String)
        funReset()
        IsLoadData = True
        Dim i As Integer = 0
        Dim CountPost As Integer = 0
        Qry = " select * from( select 1 as byte, case when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'Receipt' when TSPL_RECEIPT_HEADER.Receipt_Type='M' then 'Misc Receipt' end as [Type], QuickEntryNo , TSPL_RECEIPT_HEADER.Receipt_No , Receipt_Date , Bank_Code , case when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'On-Account' when TSPL_RECEIPT_HEADER.Receipt_Type='M' then 'Misc Receipt' end as Receipt_Type , Cust_Code ,Customer_Name ,Entry_Desc ,Cheque_No ,Cheque_Date , Payment_Code ,Receipt_Amount ,case when TSPL_RECEIPT_HEADER.Posted='Y' then 'Posted' when TSPL_RECEIPT_HEADER.Posted='N' then 'Open' end as Posted, TSPL_RECEIPT_DETAIL.Account_Code, TSPL_RECEIPT_DETAIL.Description,CForm_InvoiceNo,CFormRecd,location_gl_code,'FALSE' as PDC_Cheque,'FALSE' as Account_Payee,TSPL_RECEIPT_HEADER.Cheque_From,TSPL_RECEIPT_HEADER.From_Branch      from TSPL_RECEIPT_HEADER Left Outer Join TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No " &
              " union all  select 1 as byte,'Payment' as [Type], QuickEntryNo , Payment_No, Payment_Date, Bank_Code, Case when Payment_Type ='OA' then 'On-Account' when Payment_Type='AD' then 'Advance' end as Receipt_Type ,Vendor_Code  ,Vendor_Name  ,Entry_Desc ,Cheque_No , CONVERT(VARCHAR,Cheque_Date,103) as Cheque_Date, Payment_Code ,Payment_Amount  ,case when Posted='1' then 'Posted' ELSE 'Open' end as Posted, '' AS Account_Code, '' as Description,'' as CForm_InvoiceNo,'' as CFormRecd,location_gl_code,cASE WHEN PDC_Cheque='Y' THEN 'TRUE' ELSE 'FALSE' END AS PDC_Cheque,CASE WHEN Account_Payee='1' THEN 'TRUE' ELSE 'FALSE' END AS Account_Payee,'' As Cheque_From,'' As From_Branch   from TSPL_PAYMENT_HEADER) as  xx" &
              " where xx.QuickEntryNo ='" + EntryNo + "' "
        Dim dt As DataTable
        Try
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                txtEntryNo.Value = clsCommon.myCstr(dt.Rows(0).Item("QuickEntryNo"))
                dtDocDate.Value = dt.Rows(0).Item("Receipt_Date").ToString()
                fndBankCode.Value = dt.Rows(0).Item("Bank_Code").ToString()
                FillBankDetails()
                ddlType.Text = dt.Rows(0).Item("Type").ToString()

                For Each dr As DataRow In dt.Rows
                    If clsCommon.myCstr(dr("byte")) = "1" Then
                        MasterTemplate.CurrentRow.Cells("gvSelect").Value = True
                    Else
                        MasterTemplate.CurrentRow.Cells("gvSelect").Value = False
                    End If
                    MasterTemplate.CurrentRow.Cells("gvType").Value = clsCommon.myCstr(dr("Receipt_Type"))
                    MasterTemplate.CurrentRow.Cells("gvLocation").Value = clsCommon.myCstr(dr("Location_GL_code"))
                    If clsCommon.myLen(clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("gvLocation").Value)) > 0 Then
                        MasterTemplate.CurrentRow.Cells("gvLocation_Name").Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("gvLocation").Value) & "'"))
                    Else
                        MasterTemplate.CurrentRow.Cells("gvLocation_Name").Value = ""
                    End If
                    If ddlType.Text = "Misc Receipt" Then
                        MasterTemplate.CurrentRow.Cells("gvSourceCode").Value = clsCommon.myCstr(dr("Account_Code"))
                        MasterTemplate.CurrentRow.Cells("gvSourcename").Value = clsCommon.myCstr(dr("Description"))
                    Else
                        MasterTemplate.CurrentRow.Cells("gvSourceCode").Value = clsCommon.myCstr(dr("Cust_Code"))
                        MasterTemplate.CurrentRow.Cells("gvSourcename").Value = clsCommon.myCstr(dr("Customer_Name"))
                    End If
                    If ddlType.Text = "Receipt" Or ddlType.Text = "Payment" Then
                        MasterTemplate.Columns("gvLocation").IsVisible = True
                        MasterTemplate.Columns("gvLocation_Name").IsVisible = True
                    Else
                        MasterTemplate.Columns("gvLocation").IsVisible = False
                        MasterTemplate.Columns("gvLocation_Name").IsVisible = False
                    End If
                    MasterTemplate.CurrentRow.Cells("gvPaymentMode").Value = clsCommon.myCstr(dr("Payment_Code"))
                    If clsCommon.myCstr(dr("Cheque_From")) <> "" Then
                        MasterTemplate.CurrentRow.Cells("gvFromBank").Value = clsCommon.myCstr(dr("Cheque_From"))
                    Else
                        MasterTemplate.CurrentRow.Cells("gvFromBank").Value = Nothing
                    End If
                    MasterTemplate.CurrentRow.Cells("gvcheckNo").Value = clsCommon.myCstr(dr("Cheque_No"))
                    If clsCommon.myCstr(dr("Cheque_No")) <> "" Then
                        MasterTemplate.CurrentRow.Cells("gvcheckDate").Value = clsCommon.myCstr(dr("Cheque_Date"))
                    Else
                        MasterTemplate.CurrentRow.Cells("gvcheckDate").Value = Nothing
                    End If
                    MasterTemplate.CurrentRow.Cells("gvPDCCheque").Value = clsCommon.myCstr(dr("PDC_Cheque")) ' IIf(clsCommon.myCstr(dr("PDC_Cheque")) = "1", True, False)
                    MasterTemplate.CurrentRow.Cells("gvAcPayee").Value = clsCommon.myCstr(dr("Account_Payee")) 'IIf(clsCommon.myCstr(dr("Account_Payee")) = "1", True, False)
                    If Not clsCommon.CompairString(clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("gvPaymentMode").Value), "Cheque") = CompairStringResult.Equal Then
                        MasterTemplate.CurrentRow.Cells("gvCheckNo").ReadOnly = True
                        MasterTemplate.CurrentRow.Cells("gvCheckDate").ReadOnly = True
                    End If
                    MasterTemplate.CurrentRow.Cells("gvAmount").Value = clsCommon.myCstr(dr("Receipt_Amount"))
                    MasterTemplate.CurrentRow.Cells("gvNarration").Value = clsCommon.myCstr(dr("Entry_Desc"))
                    MasterTemplate.CurrentRow.Cells("gvDocNo").Value = clsCommon.myCstr(dr("Receipt_No"))
                    MasterTemplate.CurrentRow.Cells("gvStatus").Value = clsCommon.myCstr(dr("Posted"))
                    MasterTemplate.CurrentRow.Cells("gvCFormDoc").Value = clsCommon.myCstr(dr("CForm_InvoiceNo"))
                    If clsCommon.myCstr(dr("CFormRecd")) = "1" Then
                        MasterTemplate.CurrentRow.Cells("gvCForm").Value = True
                    Else
                        MasterTemplate.CurrentRow.Cells("gvCForm").Value = False
                    End If
                    MasterTemplate.Rows.AddNew()
                Next

                For Each dr As GridViewRowInfo In MasterTemplate.Rows
                    If dr.Cells("gvStatus").Value = "Posted" Then
                        CountPost += 1
                    End If
                Next
                btnSave.Text = "Update"
                IsNewEntry = False
                ddlType.Enabled = False
                If MasterTemplate.Rows.Count = CountPost + 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    chkPrintCheque.Enabled = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            IsLoadData = False
        End Try
    End Sub
    Public Function funPost() As Boolean
        Try
            If ddlType.Text = "Receipt" Or ddlType.Text = "Misc Receipt" Then
                For Each row As GridViewRowInfo In MasterTemplate.Rows
                    If clsCommon.myCstr(row.Cells("gvDocNo").Value) <> "" Then
                        Dim DocNo As String = row.Cells("gvDocNo").Value
                        If Not clsRcptEntryHeader.isPosted(DocNo, Nothing) Then
                            clsRcptEntryHeader.funRcptPost(DocNo)
                        End If
                    End If
                Next
            ElseIf ddlType.Text = "Payment" Then
                For Each row As GridViewRowInfo In MasterTemplate.Rows
                    If clsCommon.myCstr(row.Cells("gvDocNo").Value) <> "" Then
                        Dim DocNo As String = row.Cells("gvDocNo").Value
                        If Not clsPaymentHeader.isPosted(DocNo, Nothing) Then
                            clsPaymentHeader.PostData(DocNo)
                        End If
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Quick Book Entry", MessageBoxButtons.OK)
            Return False
        End Try
    End Function
    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        clicked = True
        PostData()
    End Sub
    Public Sub PostData()
        Try
            SaveData(False)
            If txtEntryNo.Value <> "" Then

                If funPost() = True Then
                    fillData(txtEntryNo.Value)
                    myMessages.post()
                End If
            End If
            clicked = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        funReset()
    End Sub

    Public Sub funReset()
        chkPrintCheque.Checked = False
        txtEntryNo.Value = ""
        dtDocDate.Value = System.DateTime.Now.Date
        fndBankCode.Value = ""
        txtBankName.Text = ""
        txtLocation.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True
        ddlType.Enabled = True
        chkPrintCheque.Enabled = False
        LoadBlankGrid()
        MasterTemplate.Rows.AddNew()
        IsLoadData = False
        IsNewEntry = True
        txtLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtLocation.Text) > 0 Then
            Dim Qry As String = "select top 1 DESCRIPTION,BANK_CODE   from TSPL_BANK_MASTER where Substring(bankacc,len(bankacc)-2,3) ='" + txtLocation.Text + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                fndBankCode.Value = clsCommon.myCstr(dt.Rows(0).Item("BANK_CODE"))
                txtBankName.Text = clsCommon.myCstr(dt.Rows(0).Item("DESCRIPTION"))
            Else
                fndBankCode.Value = ""
                txtBankName.Text = ""
            End If
        End If

    End Sub

    Public Function funDelete() As Boolean
        Try
            Dim strVoucherNo As String
            Dim DocNo As String
            Dim srtQ As String
            tran = clsDBFuncationality.GetTransactin()
            If ddlType.Text = "Receipt" Or ddlType.Text = "Misc Receipt" Then
                For Each row As GridViewRowInfo In MasterTemplate.Rows
                    DocNo = clsCommon.myCstr(row.Cells("gvDocNo").Value)
                    If clsCommon.myLen(DocNo) > 0 Then
                        If Not clsRcptEntryHeader.isPosted(DocNo, tran) Then
                            strVoucherNo = clsDBFuncationality.getSingleValue("select Voucher_No  from TSPL_JOURNAL_MASTER where Source_Doc_No='" + DocNo + "'", tran)
                            If clsCommon.myLen(strVoucherNo) > 0 Then
                                srtQ = "delete  from TSPL_JOURNAL_DETAILS where Voucher_No='" + strVoucherNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(srtQ, tran)

                                srtQ = "delete  from TSPL_JOURNAL_MASTER where Voucher_No='" + strVoucherNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(srtQ, tran)
                            End If

                            srtQ = "delete from TSPL_RECEIPT_HEADER where QuickEntryNo='" + txtEntryNo.Value + "' and Receipt_No='" + DocNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(srtQ, tran)
                        End If
                    End If
                Next
            ElseIf ddlType.Text = "Payment" Then
                For Each row As GridViewRowInfo In MasterTemplate.Rows
                    DocNo = row.Cells("gvDocNo").Value
                    If clsCommon.myLen(DocNo) > 0 Then
                        If Not clsPaymentHeader.isPosted(DocNo, tran) Then
                            strVoucherNo = clsDBFuncationality.getSingleValue("select Voucher_No  from TSPL_JOURNAL_MASTER where Source_Doc_No='" + DocNo + "'", tran)
                            If clsCommon.myLen(strVoucherNo) > 0 Then
                                srtQ = "delete  from TSPL_JOURNAL_DETAILS where Voucher_No='" + strVoucherNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(srtQ, tran)

                                srtQ = "delete  from TSPL_JOURNAL_MASTER where Voucher_No='" + strVoucherNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(srtQ, tran)
                            End If
                            srtQ = "delete from TSPL_PAYMENT_HEADER where QuickEntryNo='" + txtEntryNo.Value + "' and Payment_No='" + DocNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(srtQ, tran)
                        End If
                    End If
                Next
            End If
            tran.Commit()
            Return True
        Catch ex As Exception
            tran.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Quick Book Entry", MessageBoxButtons.OK)
            Return False
        End Try
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Public Sub DeleteData()
        Try
            If txtEntryNo.Value <> "" Then
                If ddlType.Text = "Receipt" Or ddlType.Text = "Misc Receipt" Then
                    Dim Qry As String = "select top 1 Receipt_No from TSPL_RECEIPT_HEADER where QuickEntryNo='" + txtEntryNo.Value + "' and Posted='Y'"
                    Qry = clsDBFuncationality.getSingleValue(Qry)
                    If clsCommon.myLen(Qry) > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Receipt No:" + Qry + " Posted.Cannot Delete the quick book entry", Me.Text)
                        Exit Sub
                    End If
                ElseIf ddlType.Text = "Payment" Then
                    Dim Qry As String = "select top 1 Payment_No from TSPL_PAYMENT_HEADER where QuickEntryNo='" + txtEntryNo.Value + "' and Posted='1'"
                    Qry = clsDBFuncationality.getSingleValue(Qry)
                    If clsCommon.myLen(Qry) > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Payment No:" + Qry + " Posted.Cannot Delete the quick book entry", Me.Text)
                        Exit Sub
                    End If
                End If


                Dim Reason As String = ""
                If myMessages.deleteConfirm() Then
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
                    ''FOR THE DELETION OF aPPROVAL IF EXISTS
                    For Each row As GridViewRowInfo In MasterTemplate.Rows
                        If clsCommon.myLen(clsCommon.myCstr(row.Cells("gvDocNo").Value)) > 0 Then
                            If ddlType.Text = "Receipt" OrElse ddlType.Text = "Misc Receipt" Then
                                clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.ReceiptEntry, clsCommon.myCstr(row.Cells("gvDocNo").Value))
                            ElseIf ddlType.Text = "Payment" Then
                                clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.PaymentEntryNew, clsCommon.myCstr(row.Cells("gvDocNo").Value))
                            End If
                        End If
                    Next
                    If funDelete() = True Then
                        saveCancelLog(Reason, "Delete", Nothing)
                        myMessages.delete()
                        funReset()
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtEntryNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Public Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            MasterTemplate.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            MasterTemplate.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = MasterTemplate.Columns.Count()
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub FrmQuickEntry1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try

            If e.KeyCode = Keys.F2 AndAlso MasterTemplate.CurrentCell IsNot Nothing Then
                If MasterTemplate.CurrentColumn Is MasterTemplate.Columns("gvSourceCode") Then
                    MasterTemplate.CurrentColumn = MasterTemplate.Columns("gvSourceName")
                    OpenICodeList(True)

                    MasterTemplate.CurrentColumn = MasterTemplate.Columns("gvSourceCode")
                End If
            End If
            If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
                If SaveData(IsNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
                End If
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
                PostData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
                DeleteData()
            ElseIf e.Alt And e.KeyCode = Keys.C Then
                CloseForm()
            ElseIf e.Alt And e.KeyCode = Keys.N Then
                funReset()
            ElseIf e.Control And e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
                funprint(txtEntryNo.Value)
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                          "========Table Name=========" + Environment.NewLine +
                          "TSPL_RECEIPT_HEADER, TSPL_RECEIPT_DETAIL, TSPL_RECEIPT_DETAIL_GST( For Receipt & Misc Receipt)" + Environment.NewLine +
                          "TSPL_PAYMENT_HEADER ,TSPL_PAYMENT_DETAIL,TSPL_PAYMENT_BANK_CHARGES_TAX,TSPL_PJC_EXPENSE_HEADER,TSPL_REMITTANCE(for Payment) " + Environment.NewLine +
                          "TSPL_Customer_Invoice_Head & tspl_bank_transfer   (For Receipt Post) " + Environment.NewLine +
                          "tspl_BankReco_Head & tspl_BankReco_Detail(for Outstanding Entry)" + Environment.NewLine +
                          "TSPL_VENDOR_INVOICE_HEAD (For Payment POST)" + Environment.NewLine +
                          "=========Setting Name======" + Environment.NewLine +
                          "AllowToUseSubAccount " + Environment.NewLine +
                          "AllowtoSkipJournalEntryofPaymentandReceiptforAD" + Environment.NewLine +
                          "AllowUseApplyDocSeriesForReceipt" + Environment.NewLine +
                          "CustomerMasterFinderOnLocationwiseARReceipt" + Environment.NewLine +
                          "ApplyBrachAccounting" + Environment.NewLine +
                          "StopNegativeBankBalance" + Environment.NewLine +
                          "AutoRecieptPaymentMode")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        'Ticket No-MIL/23/07/19-000110,Sanjay, add Alies Name
        If ddlType.Text = "Receipt" Then
            Dim qry As String = "select Cust_Code ,Customer_Name,TSPL_CUSTOMER_MASTER.Alies_Name ,Cust_Group_Code ,(select case when Status ='N' then 'Active' when Status ='Y' then 'In-Active' end) as [Status] from TSPL_CUSTOMER_MASTER "
            Dim whrCls As String = "Status ='N' AND OnHold='N'"

            ''-------richa 19/06/2020 Customer permission--------
            Dim strwherecls As String = ""
            strwherecls = Xtra.CustomerPermission()
            If clsCommon.myLen(strwherecls) > 0 Then
                whrCls += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + strwherecls + ") "
            End If

            MasterTemplate.CurrentRow.Cells(2).Value = clsCommon.ShowSelectForm("CustomerCode", qry, "Cust_Code", whrCls, MasterTemplate.CurrentRow.Cells(2).Value, "Cust_Code", isButtonClick)
            MasterTemplate.CurrentRow.Cells(3).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(2).Value) + "'"))
            ''richa agarwal VIJ/09/12/19-000111
            If ShowOutstandingAmtofCustomerOnQuickBookEntry = True Then
                Dim CustomerOutstanding As DataTable = clsDBFuncationality.GetDataTable(clsCustomerMaster.getCustomerOutstandingAmtWithOPeningAndClosing("'" & MasterTemplate.CurrentRow.Cells(2).Value & "'", clsCommon.GetPrintDate(clsCommon.myCDate(dtDocDate.Value), "dd/MMM/yyyy"), clsCommon.GetPrintDate(clsCommon.myCDate(dtDocDate.Value), "dd/MMM/yyyy"), "ConvRate"))
                If CustomerOutstanding IsNot Nothing AndAlso CustomerOutstanding.Rows.Count > 0 Then
                    MasterTemplate.CurrentRow.Cells("gvOutstandingAmount").Value = clsCommon.myCdbl(CustomerOutstanding.Rows(0)("BalAmt"))
                End If
            End If
            ''richa 14 Jan,2019 to show location into grid
            If clsCommon.myLen(txtLocation.Text) > 0 Then
                MasterTemplate.CurrentRow.Cells("gvLocation").Value = txtLocation.Text
                IsLoadData = False
                If clsCommon.myLen(clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("gvLocation").Value)) > 0 Then
                    MasterTemplate.CurrentRow.Cells("gvLocation_Name").Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("gvLocation").Value) & "'"))
                Else
                    MasterTemplate.CurrentRow.Cells("gvLocation_Name").Value = ""
                End If
            End If
        ElseIf ddlType.Text = "Misc Receipt" Then
            If clsCommon.myLen(fndBankCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Bank Code ", Me.Text)
                fndBankCode.Focus()
                Exit Sub
            End If
            Dim bankseg As String = " select right(BANKACC,3) as segment,BANKACC,BANK_CODE from TSPL_BANK_MASTER where BANK_CODE='" + fndBankCode.Value + "'"
            Dim val As String = clsDBFuncationality.getSingleValue(bankseg)
            Dim qry As String = ""
            Dim whrCls As String = ""
            Dim arrlist As New ArrayList()
            arrlist = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arrlist.Item(0) + " inner join TSPL_GL_STRUCTURE on TSPL_GL_ACCOUNTS .Str_Code=TSPL_GL_STRUCTURE.Str_Code "
            whrCls = arrlist.Item(1)
            If whrCls = "" Then

            Else
                whrCls = "(" + whrCls + ")"
            End If
            If whrCls Is Nothing OrElse whrCls = "" Then
                whrCls = " 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
            Else
                whrCls = whrCls + " and 1<>(Seg_No1 +Seg_No2 +Seg_No3 +Seg_No4 +Seg_No5 +Seg_No6 +Seg_No7 +Seg_No8 +Seg_No9 +Seg_No10 )"
            End If
            whrCls += "   and TSPL_GL_ACCOUNTS.Account_Seg_Code7='" + val + "' AND ControlAccount<>'Y'"
            MasterTemplate.CurrentRow.Cells(2).Value = clsCommon.ShowSelectForm("QBEGLACFND1", qry, "Account_Code", whrCls, clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(2).Value), "Account_Code", isButtonClick)
            MasterTemplate.CurrentRow.Cells(3).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select  Description from TSPL_GL_ACCOUNTS WHERE Account_Code = '" + clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(2).Value) + "'"))
            ''richa 14 Jan,2019 to show location into grid
            If clsCommon.myLen(txtLocation.Text) > 0 Then
                MasterTemplate.CurrentRow.Cells("gvLocation").Value = txtLocation.Text
                IsLoadData = False
                If clsCommon.myLen(clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("gvLocation").Value)) > 0 Then
                    MasterTemplate.CurrentRow.Cells("gvLocation_Name").Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("gvLocation").Value) & "'"))
                Else
                    MasterTemplate.CurrentRow.Cells("gvLocation_Name").Value = ""
                End If
            End If
        ElseIf ddlType.Text = "Payment" Then
            Dim qry As String = "select Vendor_Code,Vendor_Name,TSPL_VENDOR_MASTER.Alies_Name,Vendor_Group_Code ,(select case when Status ='N' then 'Active' when Status ='Y' then 'In-Active' end) as [Status] from TSPL_VENDOR_MASTER"
            Dim whrCls As String = ""
            MasterTemplate.CurrentRow.Cells(2).Value = clsCommon.ShowSelectForm("VendorCode", qry, "Vendor_Code", whrCls, MasterTemplate.CurrentRow.Cells(2).Value, "Vendor_Code", isButtonClick)
            MasterTemplate.CurrentRow.Cells(3).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(2).Value) + "'"))
            ''richa 14 Jan,2019 to show location into grid
            If clsCommon.myLen(txtLocation.Text) > 0 Then
                MasterTemplate.CurrentRow.Cells("gvLocation").Value = txtLocation.Text
                IsLoadData = False
                If clsCommon.myLen(clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("gvLocation").Value)) > 0 Then
                    MasterTemplate.CurrentRow.Cells("gvLocation_Name").Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("gvLocation").Value) & "'"))
                Else
                    MasterTemplate.CurrentRow.Cells("gvLocation_Name").Value = ""
                End If
            End If
        End If
    End Sub
    Sub OpenCformDocList(ByVal isButtonClick As Boolean)
        If clsCommon.CompairString(ddlType.Text, "Receipt") = CompairStringResult.Equal Then
            Dim qry As String = "select Document_Code,Document_Date from TSPL_SD_SALE_INVOICE_HEAD "
            MasterTemplate.CurrentRow.Cells("gvCFormDoc").Value = clsCommon.ShowSelectForm("InvoiceNo", qry, "Document_Code", "Posting_Date is not null and Against_C_Form=1 and CFormRecd=0 and CFormApplied=0 and Customer_Code='" + clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(2).Value) + "' and Document_Code not in (select isnull(CForm_InvoiceNo,'')  from  TSPL_RECEIPT_HEADER) ", MasterTemplate.CurrentRow.Cells("gvCFormDoc").Value, "Document_Code", isButtonClick)
        ElseIf clsCommon.CompairString(ddlType.Text, "Payment") = CompairStringResult.Equal Then
            Dim qry As String = "select PI_No,PI_Date,* from TSPL_PI_HEAD "
            MasterTemplate.CurrentRow.Cells("gvCFormDoc").Value = clsCommon.ShowSelectForm("InvoiceNo", qry, "PI_No", "Posting_Date is not null and Against_C_Form=1 and CFormRecd=0 and CFormApplied=0 and Vendor_Code='" + clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(2).Value) + "' and PI_No not in (select isnull(CForm_InvoiceNo,'')  from  TSPL_PAYMENT_HEADER) ", MasterTemplate.CurrentRow.Cells("gvCFormDoc").Value, "PI_No", isButtonClick)
        End If
    End Sub

    Private Sub MasterTemplate_EditorRequired(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles MasterTemplate.EditorRequired
        If IsLoadData = False Then
            MasterTemplate.CurrentRow.Cells("gvSelect").Value = True
            'MasterTemplate.CurrentRow.Cells("gvType").Value = "On-Account"
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtEntryNo.Value) > 0 Then
            funprint(txtEntryNo.Value)
        Else
            common.clsCommon.MyMessageBoxShow(Me, "Select the Entry No. ", Me.Text)
        End If

    End Sub


    Public Sub funprint(ByVal StrCode As String)
        Try

            Dim qry As String = ""
            Dim qrySubReport As String = ""

            If ddlType.Text = "Receipt" Then
                qry = " SELECT TSPL_CUSTOMER_MASTER.Zone_Code,'Receipt' as rtype,TSPL_RECEIPT_HEADER.Payment_Code,TSPL_RECEIPT_HEADER.QuickEntryNo, TSPL_RECEIPT_HEADER.Receipt_No as DocNo, TSPL_RECEIPT_HEADER.Receipt_Date as DocDate, TSPL_RECEIPT_HEADER.Bank_Code, TSPL_BANK_MASTER.DESCRIPTION as BankDesc , " &
                       " (case when TSPL_RECEIPT_HEADER.Receipt_Type='o' then 'On Account' else case when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'Advance' end end )as Type, TSPL_RECEIPT_HEADER.Cust_Code as Code, TSPL_RECEIPT_HEADER.Customer_Name as Name, " &
                       "  TSPL_RECEIPT_HEADER.Narration, TSPL_RECEIPT_HEADER.Cheque_No, " &
                       "  TSPL_RECEIPT_HEADER.Cheque_Date, TSPL_RECEIPT_HEADER.Receipt_Amount as Amount,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_RECEIPT_HEADER.Cheque_From " &
                       "   FROM         TSPL_RECEIPT_HEADER LEFT OUTER JOIN  " &
                       "   TSPL_BANK_MASTER ON TSPL_RECEIPT_HEADER.Bank_Code = TSPL_BANK_MASTER.BANK_CODE " &
                       "   left Outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_RECEIPT_HEADER.Comp_Code " &
                       "   Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_RECEIPT_HEADER.Cust_Code " &
                       "   where TSPL_RECEIPT_HEADER.QuickEntryNo='" + StrCode + "' "
                qrySubReport = " Select Final.QuickEntryNo , Final.Payment_Code,Sum (Final.Amount) as Amount   from (  " + qry + "  ) Final group by Final.QuickEntryNo,Final.Payment_Code "

            ElseIf ddlType.Text = "Payment" Then

                qry = " SELECT 'Payment' as rtype,TSPL_PAYMENT_HEADER.Payment_Code ,TSPL_PAYMENT_HEADER.QuickEntryNo, TSPL_PAYMENT_HEADER.Payment_No as DocNo, TSPL_PAYMENT_HEADER.Payment_Date as DocDate, TSPL_PAYMENT_HEADER.Bank_Code , TSPL_BANK_MASTER.DESCRIPTION as BankDesc,  " &
                      "  (case when TSPL_PAYMENT_HEADER.Payment_Type='oa' then 'On Account' else case when TSPL_PAYMENT_HEADER.Payment_Type='av' then 'Advance' end end )as Type, TSPL_PAYMENT_HEADER.Vendor_Code as Code, TSPL_PAYMENT_HEADER.Vendor_Name as Name, " &
                      "  TSPL_PAYMENT_HEADER.Entry_Desc as Narration, TSPL_PAYMENT_HEADER.Cheque_No, TSPL_PAYMENT_HEADER.Cheque_Date,  " &
                      "   TSPL_PAYMENT_HEADER.Payment_Amount as Amount ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1" &
                      "   From  TSPL_PAYMENT_HEADER LEFT OUTER JOIN " &
                      " TSPL_BANK_MASTER ON TSPL_PAYMENT_HEADER.Bank_Code = TSPL_BANK_MASTER.BANK_CODE " &
                 "   left Outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_PAYMENT_HEADER.Comp_Code " &
                   " where TSPL_PAYMENT_HEADER.QuickEntryNo='" + StrCode + "' "
                qrySubReport = " Select Final.QuickEntryNo , Final.Payment_Code,Sum (Final.Amount) as Amount   from (  " + qry + "  ) Final group by Final.QuickEntryNo,Final.Payment_Code "
            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim dtSubReport As DataTable = clsDBFuncationality.GetDataTable(qrySubReport)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Print")
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                'frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "CrptQuickBook", "Quick Book Report")
                If chkPrintCheque.Enabled = True AndAlso chkPrintCheque.Checked Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.SalesReport, dt, dtSubReport, "CrptQuickBookCheque", "Quick Book Report", "rptQuickBookSubReport.rpt", Nothing)
                Else
                    frmCRV.funsubreportWithdt(CrystalReportFolder.SalesReport, dt, dtSubReport, "CrptQuickBook", "Quick Book Report", "rptQuickBookSubReport.rpt", Nothing)
                End If

                frmCRV = Nothing

            End If



        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "QUICKBOOK"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Enabled = False
    '        End If
    '        If strTemp(3) = "0" Then 'Grant Authorize access
    '            btnPost.Enabled = False
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception
    '        common.clsCommon.MyMessageBoxShow(er.Message.ToString())
    '    End Try
    'End Function

    Private Sub ddlType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlType.SelectedIndexChanged
        'If MasterTemplate.Rows.Count > 0 Then
        '    funTypeLoad()
        'End If
        LoadBlankGrid()
        MasterTemplate.Rows.AddNew()
    End Sub
    Public Sub Location_Finder()
        Try
            Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
            Dim WhrCls As String = "Seg_No = '7' AND GIT='N'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            IsLoadData = True
            MasterTemplate.CurrentRow.Cells("gvLocation").Value = clsCommon.ShowSelectForm("PELoc", qry, "Code", WhrCls, MasterTemplate.CurrentRow.Cells("gvLocation").Value, "Code", False)
            IsLoadData = False
            If clsCommon.myLen(clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("gvLocation").Value)) > 0 Then
                MasterTemplate.CurrentRow.Cells("gvLocation_Name").Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("gvLocation").Value) & "'"))
            Else
                MasterTemplate.CurrentRow.Cells("gvLocation_Name").Value = ""
            End If
            ' Return Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        'Dim StrQ As String = Nothing
        'If clsCommon.myLen(txtEntryNo.Value) > 0 Then
        '    If ddlType.Text = "Payment" Then
        '        StrQ = "delete from TSPL_PAYMENT_HEADER where QuickEntryNo  ='" + clsCommon.myCstr(txtEntryNo.Value) + "' and Cheque_No ='" + clsCommon.myCstr(gv1.CurrentRow.Cells("gvCheckNo").Value) + "'"
        '        clsDBFuncationality.ExecuteNonQuery(StrQ)
        '    ElseIf ddlType.Text = "Receipt" Then
        '        StrQ = "delete from TSPL_RECEIPT_HEADER where QuickEntryNo  ='" + clsCommon.myCstr(txtEntryNo.Value) + "' and Cheque_No ='" + clsCommon.myCstr(gv1.CurrentRow.Cells("gvCheckNo").Value) + "'"
        '        clsDBFuncationality.ExecuteNonQuery(StrQ)
        '    End If
        'End If
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        'If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
        '    e.Cancel = True
        'End If
    End Sub

    Private Sub MasterTemplate_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles MasterTemplate.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub MasterTemplate_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles MasterTemplate.CellFormatting
        Try

            Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
            If clsCommon.myCstr(grow.Cells("gvStatus").Value) = "Posted" Then
                grow.Cells("gvSelect").ReadOnly = True
                grow.Cells("gvType").ReadOnly = True
                grow.Cells("gvSourceCode").ReadOnly = True
                grow.Cells("gvSourcename").ReadOnly = True
                grow.Cells("gvcheckNo").ReadOnly = True
                grow.Cells("gvcheckDate").ReadOnly = True
                grow.Cells("gvAmount").ReadOnly = True
                grow.Cells("gvNarration").ReadOnly = True
                grow.Cells("gvDocNo").ReadOnly = True
                grow.Cells("gvStatus").ReadOnly = True
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub MasterTemplate_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles MasterTemplate.CurrentColumnChanged
        If MasterTemplate.RowCount > 0 Then
            Dim intCurrRow As Integer = MasterTemplate.CurrentRow.Index
            MasterTemplate.CurrentRow.Cells("gvSelect").Value = True
            If intCurrRow = MasterTemplate.Rows.Count - 1 Then
                MasterTemplate.Rows.AddNew()
                MasterTemplate.CurrentRow = MasterTemplate.Rows(intCurrRow)
            End If
        End If
    End Sub
    'done by stuti on 03/10/2016 Ticket No=BM00000009919'
    'Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
    '    Dim Qry As String = Nothing
    '    If ddlType.Text = "Receipt" Then
    '        Qry = " Select '' as [Quick Entry No], '' as [Date], '' as [Bank Code], '' as [Code], '' as [Cheque No], '' as [Cheque Date], 0 as [Amount], '' as [Narration],'' as [Location Code] "
    '    ElseIf ddlType.Text = "Payment" Then
    '        Qry = " Select '' as [Quick Entry No], '' as [Date], '' as [Bank Code], '' as [Code], '' as [Cheque No], '' as [Cheque Date], 0 as [Amount], '' as [Narration],'' as [Location Code],'' as [PDC Cheque],'' as [Acc Payee] "
    '    End If
    '    transportSql.ExporttoExcel(Qry, Me)
    'End Sub



    'Private Sub rmiImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImport.Click
    '    If ddlType.Text = "Receipt" Then
    '        funImportO()
    '    ElseIf ddlType.Text = "Payment" Then
    '        funImportOA()
    '    End If
    'End Sub

    Public Sub funImportO()

        Dim gv As New RadGridView()

        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Quick Entry No", "Date", "Bank Code", "Code", "Cheque No", "Cheque Date", "Amount", "Narration", "Location Code") Then
            Try
                tran = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim LineNo As String = ""
                Dim Qry As String = ""

                Dim EntryNo As String = ""
                Dim EntryDate As String = ""
                Dim BankCode As String = ""
                Dim Loc_Code As String = ""
                For Each grow As GridViewRowInfo In gv.Rows
                    LineNo = clsCommon.myCstr(grow.Index + 2)
                    If LineNo = "2" Then
                        '-----------Quick Entry No---------------------
                        If clsCommon.myLen(grow.Cells("Quick Entry No").Value) <= 0 Then
                            EntryNo = fnAutoGenerateNo(dtDocDate.Value, tran)
                        Else
                            EntryNo = clsCommon.myCstr(grow.Cells("Quick Entry No").Value)
                            If clsCommon.myLen(EntryNo) > 30 Then
                                Throw New Exception("The length of 'Quick Entry No' is greater than 30 at line " + LineNo + ". ")
                            End If
                        End If
                        Qry = "select 1 from TSPL_RECEIPT_HEADER where QuickEntryNo='" + EntryNo + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, tran)

                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Throw New Exception("Quick book no  " + EntryNo + " is in used.Please check your prefix ")
                        End If


                        Qry = "Delete From TSPL_RECEIPT_HEADER Where QuickEntryNo = '" + EntryNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, tran)
                        '----------------------------------------------

                        '--------------Date----------------------------
                        EntryDate = clsCommon.myCstr(grow.Cells("Date").Value)
                        If clsCommon.myLen(EntryDate) >= 10 Then
                        Else
                            Throw New Exception("Date at line " + LineNo + " has some incorrect values. ")
                        End If
                        '-----------------------------------------------

                        '--------------Bank Code------------------------
                        BankCode = clsCommon.myCstr(grow.Cells("Bank Code").Value)
                        If clsCommon.myLen(BankCode) > 0 Then
                            BankCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select BANK_CODE from TSPL_BANK_MASTER Where BANK_CODE='" + BankCode + "'", tran))
                            If BankCode = "" Then
                                Throw New Exception("Bank at line " + LineNo + " does not exist. ")
                            End If
                        Else
                            Throw New Exception("Insert Bank Code at line " + LineNo + ".")
                        End If
                        '-----------------------------------------------
                    End If

                    '-----------Customer Code--------------------------------
                    Dim CustCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(CustCode) > 0 Then
                        CustCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER Where Cust_Code = '" + CustCode + "'", tran))
                        If CustCode = "" Then
                            Throw New Exception("Customer Code at line " + LineNo + " does not exist. ")
                        End If
                    Else
                        Throw New Exception("Insert Customer Code at line " + LineNo + ".")
                    End If
                    '--------------------------------------------------------

                    '-----------Location Code--------------------------------
                    Loc_Code = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    If clsCommon.myLen(Loc_Code) > 0 Then
                        Loc_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left " _
                                   & " outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code  Where Seg_No = '7' AND GIT='N' and Segment_code = '" & Loc_Code & "'", tran))
                        If Loc_Code = "" Then
                            Throw New Exception("Location Code at line " + LineNo + " does not exist. ")
                        End If
                    Else
                        Throw New Exception("Insert Location Code at line " + LineNo + ".")
                    End If
                    '--------------------------------------------------------

                    Dim ChequeNo As String
                    Dim ChequeDate As String
                    Dim PaymentCode As String
                    '----------------Cheque No/ CHeque Date=------------------

                    Dim BankType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Bank_type from TSPL_BANK_MASTER Where BANK_CODE='" + BankCode + "'", tran))
                    If clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal Then
                        PaymentCode = "Cheque"
                        ChequeNo = clsCommon.myCstr(grow.Cells("Cheque No").Value)
                        '-------------------------
                        If clsCommon.myLen(ChequeNo) <= 0 Then
                            Throw New Exception("Insert Cheque No at line " + LineNo + ".")
                        ElseIf clsCommon.myLen(ChequeNo) > 6 Then
                            Throw New Exception("The length of Cheque No at line " + LineNo + " is greater than 6 .")
                        Else
                            Dim ChkNo As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) From TSPL_RECEIPT_HEADER Where Cheque_No='" + ChequeNo + "'", tran))
                            If ChkNo > 0 Then
                                Throw New Exception("The Cheque No at line " + LineNo + " is already in use .")
                            End If
                        End If
                        '--------------------------
                        ChequeDate = clsCommon.myCstr(grow.Cells("Cheque Date").Value)
                        If clsCommon.myLen(ChequeDate) <= 0 Then
                            Throw New Exception("Insert Cheque Date at line " + LineNo + " .")
                        ElseIf clsCommon.myLen(ChequeDate) < 10 Then
                            Throw New Exception("Cheque Date at line " + LineNo + " has some incorrect values .")
                        End If
                        '--------------------------
                    Else
                        PaymentCode = "Cash"
                        ChequeNo = ""
                        ChequeDate = Nothing
                    End If
                    '---------------------------------------------------------

                    '-----------------AMount----------------------------------
                    Dim Amount As Double = clsCommon.myCdbl(grow.Cells("Amount").Value)
                    If Amount <= 0 Then
                        Throw New Exception("Enter Amount at line " + LineNo + " .")
                    End If
                    '----------------------------------------------------------

                    '--------------------Narration----------------------------
                    Dim EntryDesc As String = clsCommon.myCstr(grow.Cells("Narration").Value)
                    If clsCommon.myLen(EntryDesc) > 200 Then
                        Throw New Exception("Length of Narration at line " + LineNo + " is greater than 200.")
                    End If
                    '---------------------------------------------------------

                    Dim obj As New clsRcptEntryHeader()
                    obj.Entry_Desc = EntryDesc
                    obj.Receipt_Date = EntryDate
                    obj.Receipt_Post_Date = EntryDate
                    obj.Bank_Code = BankCode
                    obj.Receipt_Type = "O"
                    obj.Payment_Code = PaymentCode
                    If clsCommon.CompairString(obj.Payment_Code, "Cheque") = CompairStringResult.Equal Then
                        obj.Cheque_No = ChequeNo
                        Dim check As String = "Select top 1 1 from TSPL_RECEIPT_HEADER Where Cheque_No='" + obj.Cheque_No + "' "
                        Dim RcptNoFlag As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(check, tran))
                        If RcptNoFlag = 1 Then
                            Throw New Exception(" '" + obj.Cheque_No + "' Cheque No. Is Already Used With Other Receipt Entry.")
                        End If
                        obj.Cheque_Date = ChequeDate
                    Else
                        obj.Cheque_No = ""
                        obj.Cheque_Date = Nothing
                    End If
                    obj.Cust_Code = CustCode
                    obj.Receipt_Amount = Amount
                    obj.Balance_Amt = Amount
                    obj.UnApply_Amt = Amount
                    obj.FIFO_Balance = Amount
                    obj.IsSalesmanType = "N"
                    obj.SecurityDeposit = "N"
                    obj.IsRecoCleared = "N"
                    obj.IsChkReverse = "N"
                    obj.Narration = obj.Entry_Desc
                    obj.QuickEntryNo = EntryNo
                    obj.Location_GL_Code = Loc_Code
                    obj.SaveData(obj, True, tran)
                Next
                tran.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                tran.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Public Sub funImportOA()

        Dim gv As New RadGridView()

        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Quick Entry No", "Date", "Bank Code", "Code", "Cheque No", "Cheque Date", "Amount", "Narration", "Location Code", "PDC Cheque", "Acc Payee") Then
            Try
                tran = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim LineNo As String = ""
                Dim Qry As String = ""

                Dim EntryNo As String = ""
                Dim EntryDate As String = ""
                Dim BankCode As String = ""
                Dim Loc_Code As String = ""
                For Each grow As GridViewRowInfo In gv.Rows
                    LineNo = clsCommon.myCstr(grow.Index + 2)
                    If LineNo = "2" Then
                        '-----------Quick Entry No---------------------
                        If clsCommon.myLen(grow.Cells("Quick Entry No").Value) <= 0 Then
                            EntryNo = fnAutoGenerateNo(dtDocDate.Value, tran)
                        Else
                            EntryNo = clsCommon.myCstr(grow.Cells("Quick Entry No").Value)
                            If clsCommon.myLen(EntryNo) > 30 Then
                                Throw New Exception("The length of 'Quick Entry No' is greater than 30 at line " + LineNo + ". ")
                            End If
                        End If

                        Qry = "select 1 from TSPL_RECEIPT_HEADER where QuickEntryNo='" + EntryNo + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, tran)

                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Throw New Exception("Quick book no  " + EntryNo + " is in used.Please check your prefix ")
                        End If

                        Qry = "Delete From TSPL_RECEIPT_HEADER Where Receipt_No = '" + EntryNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, tran)
                        '----------------------------------------------

                        '--------------Date----------------------------
                        EntryDate = clsCommon.myCstr(grow.Cells("Date").Value)
                        If clsCommon.myLen(EntryDate) >= 10 Then
                        Else
                            Throw New Exception("Date at line " + LineNo + " has some incorrect values. ")
                        End If
                        '-----------------------------------------------

                        '--------------Bank Code------------------------
                        BankCode = clsCommon.myCstr(grow.Cells("Bank Code").Value)
                        If clsCommon.myLen(BankCode) > 0 Then
                            BankCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select BANK_CODE from TSPL_BANK_MASTER Where BANK_CODE='" + BankCode + "'", tran))
                            If BankCode = "" Then
                                Throw New Exception("Bank at line " + LineNo + " does not exist. ")
                            End If
                        Else
                            Throw New Exception("Insert Bank Code at line " + LineNo + ".")
                        End If
                        '-----------------------------------------------
                    End If

                    '-----------Customer Code--------------------------------
                    Dim VendorCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(VendorCode) > 0 Then
                        VendorCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Code from TSPL_VENDOR_MASTER Where Vendor_Code = '" + VendorCode + "'", tran))
                        If VendorCode = "" Then
                            Throw New Exception("Vendor Code at line " + LineNo + " does not exist. ")
                        End If
                    Else
                        Throw New Exception("Insert Vendor Code at line " + LineNo + ".")
                    End If
                    '--------------------------------------------------------

                    '-----------Location Code--------------------------------
                    Loc_Code = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    If clsCommon.myLen(Loc_Code) > 0 Then
                        Loc_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left " _
                                   & " outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code  Where Seg_No = '7' AND GIT='N' and Segment_code = '" & Loc_Code & "'", tran))
                        If Loc_Code = "" Then
                            Throw New Exception("Location Code at line " + LineNo + " does not exist. ")
                        End If
                    Else
                        Throw New Exception("Insert Location Code at line " + LineNo + ".")
                    End If
                    '--------------------------------------------------------
                    Dim ChequeNo As String
                    Dim ChequeDate As String
                    Dim PaymentCode As String
                    Dim PDCCheque As String
                    Dim ACCPayee As String
                    '----------------Cheque No/ CHeque Date=------------------

                    Dim BankType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Bank_type from TSPL_BANK_MASTER Where BANK_CODE='" + BankCode + "'", tran))
                    If clsCommon.CompairString(BankType, "B") = CompairStringResult.Equal Then
                        PaymentCode = "Cheque"
                        ChequeNo = clsCommon.myCstr(grow.Cells("Cheque No").Value)
                        '-------------------------
                        If clsCommon.myLen(ChequeNo) <= 0 Then
                            Throw New Exception("Insert Cheque No at line " + LineNo + ".")
                        ElseIf clsCommon.myLen(ChequeNo) > 6 Then
                            Throw New Exception("The length of Cheque No at line " + LineNo + " is greater than 6 .")
                        Else
                            Dim ChkNo As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) From TSPL_PAYMENT_HEADER Where Cheque_No='" + ChequeNo + "'", tran))
                            If ChkNo > 0 Then
                                Throw New Exception("The Cheque No at line " + LineNo + " is already in use .")
                            End If
                        End If
                        '--------------------------
                        ChequeDate = clsCommon.myCstr(grow.Cells("Cheque Date").Value)
                        If clsCommon.myLen(ChequeDate) <= 0 Then
                            Throw New Exception("Insert Cheque Date at line " + LineNo + " .")
                        ElseIf clsCommon.myLen(ChequeDate) < 10 Then
                            Throw New Exception("Cheque Date at line " + LineNo + " has some incorrect values .")
                        End If
                        '--------------------------
                        '=============Added by Rohit on May 25,2015=======================
                        PDCCheque = clsCommon.myCstr(grow.Cells("PDC Cheque").Value)
                        If clsCommon.myCstr(PDCCheque) = "Y" Or clsCommon.myCstr(PDCCheque) = "N" Or clsCommon.myCstr(PDCCheque) = "" Then
                        Else
                            Throw New Exception("PDC Cheque at line " + LineNo + " has some incorrect values .It should be Y/N or blank.")
                        End If

                        ACCPayee = clsCommon.myCstr(grow.Cells("Acc Payee").Value)
                        If clsCommon.myCstr(ACCPayee) = "Y" Or clsCommon.myCstr(ACCPayee) = "N" Or clsCommon.myCstr(ACCPayee) = "" Then
                        Else
                            Throw New Exception("Acc Payee at line " + LineNo + " has some incorrect values .It should be Y/N or blank.")
                        End If
                        '==============================================================

                    Else
                        PaymentCode = "Cash"
                        ChequeNo = ""
                        ChequeDate = Nothing
                        PDCCheque = Nothing
                        ACCPayee = Nothing
                    End If
                    '---------------------------------------------------------

                    '-----------------AMount----------------------------------
                    Dim Amount As Double = clsCommon.myCdbl(grow.Cells("Amount").Value)
                    If Amount <= 0 Then
                        Throw New Exception("Enter Amount at line " + LineNo + " .")
                    End If
                    '----------------------------------------------------------

                    '--------------------Narration----------------------------
                    Dim EntryDesc As String = clsCommon.myCstr(grow.Cells("Narration").Value)
                    If clsCommon.myLen(EntryDesc) > 200 Then
                        Throw New Exception("Length of Narration at line " + LineNo + " is greater than 200.")
                    End If
                    '---------------------------------------------------------

                    Dim obj As New clsPaymentHeader()
                    obj.Entry_Desc = EntryDesc
                    obj.Payment_Date = EntryDate
                    obj.Payment_Post_Date = EntryDate
                    obj.Bank_Code = BankCode
                    obj.Payment_Type = "OA"
                    obj.Vendor_Code = VendorCode
                    obj.Vendor_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Vendor_Name From TSPL_VENDOR_MASTER WHERE Vendor_Code='" + obj.Vendor_Code + "'", tran))
                    obj.Payment_Code = "Cheque"
                    If clsCommon.CompairString(obj.Payment_Code, "Cheque") = CompairStringResult.Equal Then
                        obj.Cheque_No = ChequeNo
                        Dim check As String = "Select top 1 1 from TSPL_PAYMENT_HEADER Where Cheque_No='" + obj.Cheque_No + "' "
                        Dim RcptNoFlag As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(check, tran))
                        If RcptNoFlag = 1 Then
                            Throw New Exception(" '" + obj.Cheque_No + "' Cheque No. Is Already Used With Other Receipt Entry.")
                        End If
                        obj.Cheque_Date = ChequeDate
                        obj.PDC_Cheque = IIf(PDCCheque.Contains("Y"), "Y", "N")
                        obj.Account_Payee = IIf(ACCPayee.Contains("Y"), "1", "0")
                    Else
                        obj.Cheque_No = ""
                        obj.Cheque_Date = Nothing
                        obj.PDC_Cheque = Nothing
                        obj.Account_Payee = Nothing
                    End If

                    obj.Total_Prepayment = Amount
                    obj.Payment_Amount = Amount
                    obj.Balance_Amt = Amount

                    obj.IsChkReverse = "N"
                    obj.QuickEntryNo = EntryNo
                    obj.Location_GL_Code = Loc_Code
                    obj.SaveData1(obj, True, tran)
                Next
                tran.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                tran.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        IsLoadData = True
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In MasterTemplate.Rows
                grow.Cells("gvSelect").Value = False
            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In MasterTemplate.Rows
                grow.Cells("gvSelect").Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
        IsLoadData = False
    End Sub


    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        Dim Qry As String = Nothing
        If clsCommon.CompairString(ddlType.Text, "Receipt") = CompairStringResult.Equal Then
            Qry = "Select '' as Customer,'' as Location,'' as [Payment Mode],'' As [From Bank] ,'' as [Cheque/UTR No], '' as [Cheque/UTR Date], 0 as [Amount],'' as [CForm(Y/N)],'' as [CForm Invoice], '' as [Narration] "
        ElseIf clsCommon.CompairString(ddlType.Text, "Payment") = CompairStringResult.Equal Then
            Qry = "Select '' as Vendor,'' as Location,'' as [Payment Mode] ,'' as [Cheque/UTR No], '' as [Cheque/UTR Date], 0 as [Amount],'' as [PDC Cheque(Y/N)],'' as [Account Payee(Y/N)],'' as [CForm(Y/N)],'' as [CForm Invoice], '' as [Narration] "
        ElseIf clsCommon.CompairString(ddlType.Text, "Misc Receipt") = CompairStringResult.Equal Then
            Qry = "Select '' as Account,'' as [Payment Mode] ,'' as [Cheque/UTR No], '' as [Cheque/UTR Date], 0 as [Amount],'' as [Narration] "
        End If
        transportSql.ExporttoExcel(Qry, Me)
    End Sub

    Private Sub rmiImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImport.Click
        Dim gv As New RadGridView()
        Try
            Me.Controls.Add(gv)
            Try
                If clsCommon.myLen(MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvSourceCode").Value) <= 0 Then
                    MasterTemplate.Rows.RemoveAt(MasterTemplate.Rows.Count - 1)
                End If
            Catch ex As Exception
            End Try

            If clsCommon.CompairString(ddlType.Text, "Receipt") = CompairStringResult.Equal Then
                If transportSql.importExcel(gv, "Customer", "Location", "Payment Mode", "From Bank", "Cheque/UTR No", "Cheque/UTR Date", "Amount", "CForm(Y/N)", "CForm Invoice", "Narration") Then
                    For Each grow As GridViewRowInfo In gv.Rows
                        If clsCommon.myLen(grow.Cells("Customer").Value) > 0 Then
                            MasterTemplate.Rows.AddNew()
                            Try
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvSelect").Value = True
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvSourceCode").Value = clsCommon.myCstr(grow.Cells("Customer").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvLocation").Value = clsCommon.myCstr(grow.Cells("Location").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvPaymentMode").Value = clsCommon.myCstr(grow.Cells("Payment Mode").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvFromBank").Value = clsCommon.myCstr(grow.Cells("From Bank").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvCheckNo").Value = clsCommon.myCstr(grow.Cells("Cheque/UTR No").Value)
                                If clsCommon.myLen(grow.Cells("Cheque/UTR Date").Value) > 0 Then
                                    MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvCheckDate").Value = clsCommon.myCDate(grow.Cells("Cheque/UTR Date").Value)
                                End If
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvAmount").Value = clsCommon.myCstr(grow.Cells("Amount").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvCForm").Value = (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("CForm(Y/N)").Value), "Y") = CompairStringResult.Equal)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvCFormDoc").Value = clsCommon.myCstr(grow.Cells("CForm Invoice").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvNarration").Value = clsCommon.myCstr(grow.Cells("Narration").Value)
                            Catch ex As Exception
                                MasterTemplate.Rows.RemoveAt(MasterTemplate.Rows.Count - 1)
                                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                            End Try
                        End If
                    Next
                End If
            ElseIf clsCommon.CompairString(ddlType.Text, "Payment") = CompairStringResult.Equal Then
                If transportSql.importExcel(gv, "Vendor", "Location", "Payment Mode", "Cheque/UTR No", "Cheque/UTR Date", "Amount", "PDC Cheque(Y/N)", "Account Payee(Y/N)", "CForm(Y/N)", "CForm Invoice", "Narration") Then
                    For Each grow As GridViewRowInfo In gv.Rows
                        If clsCommon.myLen(grow.Cells("Vendor").Value) > 0 Then
                            MasterTemplate.Rows.AddNew()
                            Try
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvSelect").Value = True
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvSourceCode").Value = clsCommon.myCstr(grow.Cells("Vendor").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvLocation").Value = clsCommon.myCstr(grow.Cells("Location").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvPaymentMode").Value = clsCommon.myCstr(grow.Cells("Payment Mode").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvCheckNo").Value = clsCommon.myCstr(grow.Cells("Cheque/UTR No").Value)
                                If clsCommon.myLen(grow.Cells("Cheque/UTR Date").Value) > 0 Then
                                    MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvCheckDate").Value = clsCommon.myCDate(grow.Cells("Cheque/UTR Date").Value)
                                End If
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvAmount").Value = clsCommon.myCstr(grow.Cells("Amount").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvPDCCheque").Value = (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("PDC Cheque(Y/N)").Value), "Y") = CompairStringResult.Equal)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvACPayee").Value = (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Account Payee(Y/N)").Value), "Y") = CompairStringResult.Equal)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvCForm").Value = (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("CForm(Y/N)").Value), "Y") = CompairStringResult.Equal)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvCFormDoc").Value = clsCommon.myCstr(grow.Cells("CForm Invoice").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvNarration").Value = clsCommon.myCstr(grow.Cells("Narration").Value)
                            Catch ex As Exception
                                MasterTemplate.Rows.RemoveAt(MasterTemplate.Rows.Count - 1)
                                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                            End Try
                        End If
                    Next
                End If
            ElseIf clsCommon.CompairString(ddlType.Text, "Misc Receipt") = CompairStringResult.Equal Then
                If transportSql.importExcel(gv, "Account", "Payment Mode", "Cheque/UTR No", "Cheque/UTR Date", "Amount", "Narration") Then
                    For Each grow As GridViewRowInfo In gv.Rows
                        If clsCommon.myLen(grow.Cells("Account").Value) > 0 Then
                            MasterTemplate.Rows.AddNew()
                            Try
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvSelect").Value = True
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvSourceCode").Value = clsCommon.myCstr(grow.Cells("Account").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvPaymentMode").Value = clsCommon.myCstr(grow.Cells("Payment Mode").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvCheckNo").Value = clsCommon.myCstr(grow.Cells("Cheque/UTR No").Value)
                                If clsCommon.myLen(grow.Cells("Cheque/UTR Date").Value) > 0 Then
                                    MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvCheckDate").Value = clsCommon.myCDate(grow.Cells("Cheque/UTR Date").Value)
                                End If
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvAmount").Value = clsCommon.myCstr(grow.Cells("Amount").Value)
                                MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells("gvNarration").Value = clsCommon.myCstr(grow.Cells("Narration").Value)
                            Catch ex As Exception
                                MasterTemplate.Rows.RemoveAt(MasterTemplate.Rows.Count - 1)
                                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                            End Try
                        End If
                    Next
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub
End Class


