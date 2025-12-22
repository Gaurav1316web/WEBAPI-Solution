Imports System.Data.SqlClient
Imports common
Imports Telerik
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class frmQuickPaymentBySingleCheque
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim colStartIndex As Integer = 7
    Dim colEndIndex As Integer = 6
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colSNo As String = "colSNo"
    Const colVendorCode As String = "colVendorCode"
    Const colVendorName As String = "colVendorName"
    Public ColVendorBankCode As String = "ColVendorBankCode"
    Public ColVendorBankName As String = "ColVendorBankName"
    Const colACNo As String = "colACNo"
    Const colIFSCCode As String = "colIFSCCode"
    Const colChequeAmt As String = "colChequeAmt"
    Dim isLoadData As Boolean = False
    Dim obj As New clsQuickPaymentBySingleCheque()
    Dim objtr As New clsQuickPaymentBySingleChequeDetail()
    Dim dblTotalAmt As Decimal = 0
#End Region
    Private Sub frmQuickPaymentBySingleCheque_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Addnew()
        btnPost.Visible = True
        btnPost.Enabled = False
        If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
            LoadData(clsCommon.myCstr(txtDocumentNo.Value), NavigatorType.Current)
        End If
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub LoadBlankGrid()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Dim repoText As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "SNo"
        repoText.Name = colSNo
        repoText.Width = 40
        repoText.ReadOnly = True
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Vendor Code"
        repoText.Name = colVendorCode
        repoText.Width = 80
        repoText.ReadOnly = False
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Vendor Name"
        repoText.Name = colVendorName
        repoText.Width = 120
        repoText.ReadOnly = True
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Vendor Bank "
        repoText.Name = ColVendorBankCode
        repoText.Width = 120
        repoText.ReadOnly = True
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Vendor Bank Name"
        repoText.Name = ColVendorBankName
        repoText.Width = 120
        repoText.ReadOnly = True
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "A/C No"
        repoText.Name = colACNo
        repoText.Width = 70
        repoText.ReadOnly = True
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "IFSC Code"
        repoText.Name = colIFSCCode
        repoText.Width = 70
        repoText.ReadOnly = True
        repoText.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoText)

        Dim repoChequeAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoChequeAmt.FormatString = "{0:n2}"
        repoChequeAmt.HeaderText = "Amount"
        repoChequeAmt.Name = colChequeAmt
        repoChequeAmt.Width = 100
        repoChequeAmt.ReadOnly = False
        repoChequeAmt.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoChequeAmt)

        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = True
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.AutoSizeRows = False
        Gv1.AllowColumnChooser = True
        Gv1.Rows.AddNew()
        ReStoreGridLayoutgv1()
    End Sub

    Public Shared Function GetSaleType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Cash"
        dr("Name") = "Cash"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Credit"
        dr("Name") = "Credit"
        dt.Rows.Add(dr)
        Return dt
    End Function
    Private Sub ReStoreGridLayoutgv1()
        Try
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To Gv1.Columns.Count - 1 Step ii & 1
                    Gv1.Columns(ii).IsVisible = False
                    Gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                Gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Try
            Xtra.TransactionValidity(txtDocumentDate.Value)
            If clsCommon.myLen(clsCommon.myCstr(txtPaymentMode.Value)) <= 0 Then
                Throw New Exception("Please select Payment Mode")
            End If
            If Gv1.Rows.Count = 1 Then
                Throw New Exception("Please fill atleast one vendor")
            End If
            UpdateAllTotals()
            Dim arrVendorCode As New List(Of String)
            For ii As Integer = 0 To Gv1.Rows.Count - 1
                If Not arrVendorCode.Contains(Gv1.Rows(ii).Cells(colVendorCode).Value) Then
                    arrVendorCode.Add(Gv1.Rows(ii).Cells(colVendorCode).Value)
                Else
                    Throw New Exception("Duplicate vendor code in Line No [" + clsCommon.myCstr(ii + 1) + "]")
                End If
            Next
            If txtChequeAmt.Text > dblTotalAmt Then
                Throw New Exception("Cheque Amount cannot be greater than Total Amount of all vendors")
            ElseIf txtChequeAmt.Text < dblTotalAmt Then
                Throw New Exception("Cheque Amount cannot be less than Total Amount of all vendors")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Sub SaveData()
        Try
            If (AllowToSave()) Then
                obj = New clsQuickPaymentBySingleCheque()
                obj.Document_No = txtDocumentNo.Value
                obj.Document_date = clsCommon.myCDate(txtDocumentDate.Value)
                obj.Bank_Code = txtBankCode.Value
                obj.Bank_Name = lblBankName.Text
                obj.Cheque_No = txtChequeNo.Text
                obj.Cheque_Date = txtChequeDate.Value
                obj.Payment_Code = txtPaymentMode.Value
                obj.Cheque_Amount = txtChequeAmt.Text
                If ChkAccPayee.Checked Then
                    obj.Account_Payee = 1
                Else
                    obj.Account_Payee = 0
                End If
                obj.Remarks = txtRemarks.Text
                obj.Arr = New List(Of clsQuickPaymentBySingleChequeDetail)

                For Each grow As GridViewRowInfo In Gv1.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colVendorCode).Value)) > 0 Then
                        Dim objTr As New clsQuickPaymentBySingleChequeDetail()
                        objTr.Vendor_Code = clsCommon.myCstr((grow.Cells(colVendorCode).Value))
                        objTr.Vendor_Bank_Code = clsCommon.myCstr((grow.Cells(ColVendorBankCode).Value))
                        objTr.Vendor_Bank_Name = clsCommon.myCstr((grow.Cells(ColVendorBankName).Value))
                        objTr.Vendor_Bank_ACNo = clsCommon.myCstr((grow.Cells(colACNo).Value))
                        objTr.Vendor_IFSC_Code = clsCommon.myCstr((grow.Cells(colIFSCCode).Value))
                        objTr.Amount = clsCommon.myCstr((grow.Cells(colChequeAmt).Value))
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.SaveData(obj, isNewEntry, Nothing, False)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Addnew()
        isNewEntry = True
        ChkAccPayee.Checked = False
        txtDocumentNo.Value = ""
        txtBankCode.Value = ""
        lblBankName.Text = ""
        txtPaymentMode.Value = ""
        txtChequeNo.Text = ""
        txtChequeDate.Value = clsCommon.GETSERVERDATE()
        txtChequeAmt.Text = "0"
        btnSave.Enabled = True
        btnPost.Enabled = True
        txtDocumentDate.Value = clsCommon.GETSERVERDATE()
        btnSave.Text = "Save"
        txtRemarks.Text = ""
        LoadBlankGrid()
        isInsideLoadData = False
        btnDelete.Enabled = True
        lblStatus.Status = ERPTransactionStatus.Pending
        ReStoreGridLayoutgv1()
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click

        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" & txtDocumentNo.Value & "]" & Environment.NewLine & "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                obj.PostData(MyBase.Form_ID, txtDocumentNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(txtDocumentNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If (myMessages.deleteConfirm()) Then
                If (obj.DeleteData(txtDocumentNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    btnAddNew.PerformClick()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtDocumentNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, isButtonClicked As System.Boolean) Handles txtDocumentNo._MYValidating
        Try
            If clsCommon.myLen(txtDocumentNo) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Document No can't be blank", Me.Text)
            End If
            obj = New clsQuickPaymentBySingleCheque()
            txtDocumentNo.Value = obj.getFinder(txtDocumentNo.Value, isButtonClicked)
            LoadData(txtDocumentNo.Value, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocumentNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE where Document_No='" & txtDocumentNo.Value & "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocumentNo.MyReadOnly = False
            Else
                txtDocumentNo.MyReadOnly = True
            End If
            LoadData(txtDocumentNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            Addnew()
            obj = New clsQuickPaymentBySingleCheque()
            obj = obj.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Document_No)) > 0) Then
                isLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                If obj.Status = 1 Then
                    lblStatus.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                Else
                    lblStatus.Status = ERPTransactionStatus.Pending
                    btnDelete.Enabled = True
                End If
                txtDocumentNo.Value = obj.Document_No
                txtDocumentDate.Value = obj.Document_date
                txtBankCode.Value = obj.Bank_Code
                lblBankName.Text = obj.Bank_Name
                txtPaymentMode.Value = obj.Payment_Code
                txtChequeNo.Text = obj.Cheque_No
                txtChequeDate.Value = obj.Cheque_Date
                txtChequeAmt.Text = obj.Cheque_Amount
                txtRemarks.Text = obj.Remarks
                ChkAccPayee.Checked = IIf(obj.Account_Payee = 1, True, False)
                If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                    For Each objtr As clsQuickPaymentBySingleChequeDetail In obj.Arr
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSNo).Value = Gv1.Rows.Count
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVendorCode).Value = objtr.Vendor_Code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVendorName).Value = objtr.Vendor_Name
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColVendorBankCode).Value = objtr.Vendor_Bank_Code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(ColVendorBankName).Value = objtr.Vendor_Bank_Name
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colACNo).Value = objtr.Vendor_Bank_ACNo
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colIFSCCode).Value = objtr.Vendor_IFSC_Code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colChequeAmt).Value = objtr.Amount
                        Gv1.Rows.AddNew()
                    Next
                End If

            End If
            isInsideLoadData = True
            isInsideLoadData = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Private Sub frmQuickPaymentBySingleCheque_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            btnAddNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnSave.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIR
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverseUnpost.Visible = True
            End If
        End If
    End Sub

    Private Sub btnReverseUnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseUnpost.Click
        Try
            obj = New clsQuickPaymentBySingleCheque()
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" & Environment.NewLine & "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If obj.ReverseAndUnpost(txtDocumentNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub
    Sub UpdateAllTotals()
        dblTotalAmt = 0
        For j As Integer = 0 To Gv1.Rows.Count - 1
            If clsCommon.myLen(clsCommon.myCstr(Gv1.Rows(j).Cells(colVendorCode).Value)) > 0 Then
                dblTotalAmt += Gv1.Rows(j).Cells(colChequeAmt).Value
            End If
        Next
    End Sub
    Private Sub txtBankCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBankCode._MYValidating
        Try
            Dim strWhrclas As String = ""
            Dim strWhereClause As String = ""
            Dim Qry As String = clsERPFuncationality.glbankqueryNew(strWhrclas)
            If isNewEntry = False Then
                strWhrclas += "   Bank_type=(Select Bank_type from TSPL_BANK_MASTER WHERE BANK_CODE=(Select Bank_Code from TSPL_PAYMENT_HEADER WHERE Payment_No='" & txtDocumentNo.Value & "')) AND RIGHT(BANKACC,3)=(Select RIGHT(BANKACC,3) from TSPL_BANK_MASTER WHERE BANK_CODE=(Select Bank_Code from TSPL_PAYMENT_HEADER WHERE Payment_No='" & txtDocumentNo.Value & "'))"
            End If

            If objCommonVar.RCDFCFP = True Then
            Else
                strWhereClause += " User_Code='" + objCommonVar.CurrentUserCode + "' and "
            End If
            strWhrclas += " and  TSPL_bank_master.INACTIVE ='Active' "
            strWhereClause += " TSPL_bank_master.INACTIVE ='Active' "

            Dim query As String = Qry & "where 1=1 and " & strWhereClause
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt.Rows.Count > 0 Then
                txtBankCode.Value = clsCommon.ShowSelectForm("BankSlctr@Payment", Qry, "Code", strWhereClause, txtBankCode.Value, "Code", isButtonClicked)
                lblBankName.Text = clsDBFuncationality.getSingleValue("select description from TSPL_BANK_MASTER where bank_code = '" + txtBankCode.Value + "'")

            Else
                Dim qrys As String = "select BANK_CODE as [Code], DESCRIPTION,BANKACCNUMBER as [Bank Account No]  from TSPL_BANK_MASTER "
                txtBankCode.Value = clsCommon.ShowSelectForm("BankSlctr@Payment", qrys, "Code", strWhrclas, txtBankCode.Value, "Code", isButtonClicked)
                lblBankName.Text = clsDBFuncationality.getSingleValue("select description from TSPL_BANK_MASTER where bank_code = '" + txtBankCode.Value + "'")

            End If
            txtPaymentMode.Value = clsDBFuncationality.getSingleValue("select TSPL_PAYMENT_CODE.Payment_Code   from TSPL_PAYMENT_CODE Where TSPL_PAYMENT_CODE.Payment_Code=  (select DISTINCT (case when Bank_type = 'C' THEN 'CASH' WHEN BANK_TYPE = 'B' THEN 'CHEQUE' WHEN BANK_TYPE = 'O' THEN 'OTHER' WHEN Bank_type = 'P' THEN 'PETTYCASH' END ) AS [Paymet Type] from TSPL_BANK_MASTER Where BANK_CODE='" + txtBankCode.Value + "' )")

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtPaymentMode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPaymentMode._MYValidating
        Try
            Dim strbankcode As String
            If Not String.IsNullOrEmpty(connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + txtBankCode.Value + "'")) Then
                strbankcode = connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + txtBankCode.Value + "'")
                If strbankcode.Trim() = "C" Then
                    Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                    txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector1", Qry1, "PaymentMode", "PAYMENT_TYPE = 'CASH'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
                ElseIf strbankcode.Trim() = "P" Then
                    Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                    txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector2", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Petty Cash'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
                ElseIf strbankcode = "B" Then
                    Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                    txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector3", Qry1, "PaymentMode", "PAYMENT_TYPE IN ('Cheque', 'Other','NEFT','RTGS')", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
                Else
                    Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                    txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector4", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Other'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is Gv1.Columns(colVendorCode) Then
                        OpenVendorCode(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub
    Sub OpenVendorCode(ByVal isButtonClick As Boolean)
        Dim qry As String
        qry = " select Vendor_Code as Code, Vendor_Name as Name,Bank_Code as [Bank Code],Bank_Name as [Bank Name] ,Account_No as [Account No],IFSC_Code as [IFSC Code] from TSPL_VENDOR_MASTER  "
        Gv1.CurrentRow.Cells(colVendorCode).Value = clsCommon.ShowSelectForm("DCSFND", qry, "Code", "", Gv1.CurrentRow.Cells(colVendorCode).Value, "Code", isButtonClick)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim dr As DataRow() = clsCommon.MyDTSelect(dt, "Code='" + Gv1.CurrentRow.Cells(colVendorCode).Value + "'")
        If dr.Length > 0 Then
            If clsCommon.myLen(clsCommon.myCstr(dr(0).Item("Code"))) > 0 Then
                Gv1.CurrentRow.Cells(colVendorCode).Value = clsCommon.myCstr(dr(0).Item("Code"))
                Gv1.CurrentRow.Cells(colVendorName).Value = clsCommon.myCstr(dr(0).Item("Name"))
                Gv1.CurrentRow.Cells(ColVendorBankCode).Value = clsCommon.myCstr(dr(0).Item("Bank Code"))
                Gv1.CurrentRow.Cells(ColVendorBankName).Value = clsCommon.myCstr(dr(0).Item("Bank Name"))
                Gv1.CurrentRow.Cells(colACNo).Value = clsCommon.myCstr(dr(0).Item("Account No"))
                Gv1.CurrentRow.Cells(colIFSCCode).Value = clsCommon.myCstr(dr(0).Item("IFSC Code"))
            End If
        Else
            SetBlankOfVendorColumns()
        End If
    End Sub
    Sub SetBlankOfVendorColumns()
        Gv1.CurrentRow.Cells(colVendorCode).Value = ""
        Gv1.CurrentRow.Cells(colVendorName).Value = ""
        Gv1.CurrentRow.Cells(ColVendorBankCode).Value = ""
        Gv1.CurrentRow.Cells(ColVendorBankName).Value = ""
        Gv1.CurrentRow.Cells(colACNo).Value = ""
        Gv1.CurrentRow.Cells(colIFSCCode).Value = ""
    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles Gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles Gv1.UserDeletedRow
        RefreshSNO()
    End Sub
    Sub RefreshSNO()
        For ii As Integer = 1 To Gv1.Rows.Count
            Gv1.Rows(ii - 1).Cells(colSNo).Value = ii
        Next
    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles Gv1.CurrentColumnChanged
        Try
            If Gv1.RowCount > 0 Then
                Dim intCurrRow As Integer = Gv1.CurrentRow.Index
                Gv1.CurrentRow.Cells(colSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                If intCurrRow = Gv1.Rows.Count - 1 Then
                    Gv1.Rows.AddNew()
                    Gv1.CurrentRow = Gv1.Rows(intCurrRow)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrintCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintCheck.Click
        obj = New clsQuickPaymentBySingleCheque()
        obj = obj.GetData(txtDocumentNo.Value, NavigatorType.Current)
        Dim frm As New frmPrintCheck
        frmPrintCheck.Manual_Print = 0
        'frm.Manual_Print = 0
        frmPrintCheck.BankCode = obj.Bank_Code
        frmPrintCheck.CheckCode = obj.CHECK_CODE
        frmPrintCheck.lblCheckDesc.Text = connectSql.RunScalar("select description from TSPL_BANK_CHECK_PRINTING where CHECK_CODE = '" + obj.CHECK_CODE + "'")
        frmPrintCheck.DocumentType = "Quick Payment By Single Cheque"
        frmPrintCheck.DocumentCode = obj.Document_No
        If clsCommon.myLen(obj.CHECK_CODE) > 0 Then
            frm.btnPrint.Text = "RePrint"
        End If
        frm.chkNotForHighValue.Checked = False
        If obj.Account_Payee = 1 Then
            frm.chkAccPayee.Checked = True
        Else
            frm.chkAccPayee.Checked = False
        End If
        frm.Show()

    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim gvimport As New UserControls.MyRadGridView
        Me.Controls.Add(gvimport)
        LoadBlankGrid()
        Dim arr As New List(Of String)
        Dim check As Integer = 0
        Dim VendorCode As String = ""
        Dim lineno As Integer = 1
        If transportSql.importExcel(gvimport, "Vendor Code", "Vendor Name", "Vendor Bank", "Vendor Bank Name", "A/C No", "IFSC Code", "Amount") Then
            Dim index As Integer = 0
            Dim dtError As New DataTable
            dtError.Columns.Add("RowNo", GetType(Integer))
            dtError.Columns.Add("Error", GetType(String))
            Try
                If gvimport IsNot Nothing AndAlso gvimport.Rows.Count > 0 Then
                    clsCommon.ProgressBarPercentShow()
                    For Each grow As GridViewRowInfo In gvimport.Rows
                        Try
                            index += 1
                            VendorCode = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                            clsCommon.ProgressBarPercentUpdate(index, Gv1.Rows.Count, "Validating Data...")

                            If clsCommon.myLen(VendorCode) <= 0 Then
                                Throw New Exception("Vendor Code can't be blank !")
                            ElseIf clsCommon.myLen(VendorCode) > 0 Then
                                check = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_VENDOR_MASTER where VENDOR_CODE = '" & VendorCode & "'"))
                                If check <= 0 Then
                                    Throw New Exception("Vendor Code not found in master at line no. " + clsCommon.myCstr(lineno) + "")
                                End If
                            End If
                            arr.Add(VendorCode)
                        Catch ex As Exception
                            Dim dr As DataRow = dtError.NewRow()
                            dr("RowNo") = index
                            dr("Error") = ex.Message
                            dtError.Rows.Add(dr)
                        End Try
                    Next
                    clsCommon.ProgressBarPercentHide()
                End If

                Try
                    If dtError.Rows.Count > 0 Then
                        Dim ff As New FrmFreeGrid
                        ff.ReportID = MyBase.Form_ID
                        ff.Text = Me.Text
                        ff.dt = dtError
                        ff.ShowDialog()
                    ElseIf arr IsNot Nothing AndAlso arr.Count > 0 Then
                        Try
                            Dim qry As String = "Valid Row [" + clsCommon.myCstr(arr.Count) + "]" + Environment.NewLine + " Do You want to Proceed"
                            If clsCommon.MyMessageBoxShow(Me, qry, Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                clsCommon.ProgressBarPercentShow()
                                For ii As Integer = 0 To gvimport.Rows.Count - 1
                                    clsCommon.ProgressBarPercentUpdate((gvimport.Rows(ii).Index + 1) * 100 / (gvimport.Rows.Count + 1), "Importing  : " & (gvimport.Rows(ii).Index + 1) & "/" & gvimport.Rows.Count & "")
                                    Gv1.Rows(ii).Cells(colSNo).Value = ii + 1
                                    Gv1.Rows(ii).Cells(colVendorCode).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Vendor Code").Value)
                                    Gv1.Rows(ii).Cells(colChequeAmt).Value = clsCommon.myCstr(gvimport.Rows(ii).Cells("Amount").Value)
                                    Gv1.Rows.AddNew()
                                Next
                                clsCommon.ProgressBarPercentHide()
                                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                            End If
                        Catch ex As Exception
                            Throw New Exception(ex.Message)
                        End Try
                    End If
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception(ex.Message)
                End Try
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

            End Try
        End If
        Me.Controls.Remove(gvimport)
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim whrcls As String = ""
        Dim qry As String = "select '' as [Vendor Code],'' as [Vendor Name], '' as [Vendor Bank], '' as [Vendor Bank Name], '' as [A/C No] , '' as [IFSC Code] , 0 as Amount"
        transportSql.ExporttoExcel(qry, "", "", Me)
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim qry As String = " select  '" & objCommonVar.CurrentUser & "' as User_Code, Comp_Name,TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE.Bank_Code,TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE.Bank_Name,TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE.Document_date,TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE.Cheque_No,convert(varchar,TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE.Cheque_Date,103) as Cheque_Date,TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE.Cheque_Amount, ROW_NUMBER( ) over( order by TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL.Vendor_Code) as SNo,TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL.Vendor_Bank_Code,TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL.Vendor_Bank_Name,TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL.Vendor_Bank_ACNo,TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL.Vendor_IFSC_Code,TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL.Amount from TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL
        LEFT JOIN TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE ON TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE.Document_No = TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL.Document_No LEFT JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code= TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL.Vendor_Code left join TSPL_COMPANY_MASTER on 1=1 where TSPL_QUICK_PAYMENT_BY_SINGLE_CHEQUE_DETAIL.Document_No='" & txtDocumentNo.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(MyBase.Form_ID, False, CrystalReportFolder.SalesReport, dt, "rptQuickPaymentBySingleCheque", "Quick Payment By Single Cheque")
            frmCRV = Nothing
        End If
    End Sub
End Class


