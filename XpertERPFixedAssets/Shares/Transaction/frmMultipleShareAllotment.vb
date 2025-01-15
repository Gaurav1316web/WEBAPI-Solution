Imports System.Data.SqlClient
Imports common
Imports Telerik
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class frmMultipleShareAllotment
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colSNo As String = "colSNo"
    Const colDCSUploaderNo As String = "colDCSUploaderNo"
    Const colVendorCode As String = "colVendorCode"
    Const colDCSName As String = "colDCSName"
    Const colShareCaptialOpeningAmount As String = "colShareCaptialOpeningAmount"
    Const colShareDeductedAmount As String = "colShareDeductedAmount"
    Const colBalanceAmt As String = "colBalanceAmt"
    Const colRatePerShare As String = "colRatePerShare"
    Const colNoOfShareToBeAllocated As String = "colNoOfShareToBeAllocated"
    Const colShareCertificateNoStartFrom As String = "colShareCertificateNoStartFrom"
    Const colShareCertificateNoTo As String = "colShareCertificateNoTo"
    Dim isLoadData As Boolean = False
    Dim isCopyData As Boolean = False
    Dim j As Integer = 0
    Dim obj As New clsMultipleShareAllotment()
    Dim objtr As New clsMultipleShareAllotmentDetail()
#End Region

    Private Sub frmMultipleShareAllotment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_No", "varchar(30) Not NULL Primary Key")
        coll.Add("Document_date", "DateTime Not NULL")
        coll.Add("From_Date", "DateTime Not NULL")
        coll.Add("To_Date", "DateTime Not NULL")
        coll.Add("Rate_Of_One_Share", "Decimal(18,2) NULL ")
        coll.Add("Status", "int not null default 0")
        coll.Add("Created_By", "varchar(12)  Not NULL")
        coll.Add("Created_Date", "DateTime  Not NULL")
        coll.Add("Modified_By", "varchar(12)  Not NULL")
        coll.Add("Modified_Date", "datetime  Not NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "datetime NULL")

        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD", coll, "", True, False, Nothing, Nothing, Nothing, False)

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "Integer Not NULL identity (1,1) primary key")
        coll.Add("Document_No", "VARCHAR(30) Not NULL REFERENCES TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD(Document_No)")
        coll.Add("Vendor_Code", "VARCHAR(12) Not NULL REFERENCES TSPL_VENDOR_MASTER(Vendor_Code)")
        coll.Add("Share_Opening_Amt", "Decimal(18,2) NULL")
        coll.Add("Share_Deducted_Amt", "Decimal(18,2) NULL")
        coll.Add("Balance_Amt", "Decimal(18,2) NULL")
        coll.Add("Rate_Per_Share", "Decimal(18,2) NULL")
        coll.Add("Allocated_Share", "Decimal(18,2) NULL")
        coll.Add("Share_Certificate_From", "varchar(20) NULL")
        coll.Add("Share_Certificate_To", "varchar(20) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL", coll, Nothing, True, False, "TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD", "Document_No", "")

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "Integer Not NULL identity (1,1) primary key")
        coll.Add("Ref_PK_ID", "integer not NULL references TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL (PK_Id) ")
        coll.Add("Document_No", "VARCHAR(30) Not NULL REFERENCES TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD(Document_No)")
        coll.Add("AP_Invoice_No", "Varchar(30) null References TSPL_VENDOR_INVOICE_HEAD(Document_No)")
        coll.Add("Used_Amt", "Decimal(18,2) NULL")
        coll.Add("Balance_Amt", "Decimal(18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL", coll, Nothing, True, False, "TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD", "Document_No", "")
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()

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
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNO.FormatString = ""
        repoSNO.HeaderText = "SNo"
        repoSNO.Name = colSNo
        repoSNO.Width = 40
        repoSNO.ReadOnly = True
        repoSNO.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoSNO)

        Dim repoDCSUploaderCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSUploaderCode.FormatString = ""
        repoDCSUploaderCode.HeaderText = "DCS Code"
        repoDCSUploaderCode.Name = colDCSUploaderNo
        repoDCSUploaderCode.Width = 70
        repoDCSUploaderCode.ReadOnly = True
        repoDCSUploaderCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoDCSUploaderCode)

        Dim repoDCSCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSCode.FormatString = ""
        repoDCSCode.HeaderText = "Vendor Code"
        repoDCSCode.Name = colVendorCode
        repoDCSCode.Width = 100
        repoDCSCode.ReadOnly = True
        repoDCSCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoDCSCode)

        Dim repoDCSName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSName.FormatString = ""
        repoDCSName.HeaderText = "DCS Name"
        repoDCSName.Name = colDCSName
        repoDCSName.Width = 150
        repoDCSName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDCSName)

        Dim repoShareCaptialOpeningAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShareCaptialOpeningAmount.FormatString = ""
        repoShareCaptialOpeningAmount.HeaderText = "Share Captial Opening Amount"
        repoShareCaptialOpeningAmount.Name = colShareCaptialOpeningAmount
        repoShareCaptialOpeningAmount.Width = 140
        repoShareCaptialOpeningAmount.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoShareCaptialOpeningAmount)

        Dim repoShareDeductedAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShareDeductedAmount.FormatString = ""
        repoShareDeductedAmount.HeaderText = "Total Share Captial Deducted Amount"
        repoShareDeductedAmount.Name = colShareDeductedAmount
        repoShareDeductedAmount.Width = 100
        repoShareDeductedAmount.ReadOnly = True
        repoShareDeductedAmount.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoShareDeductedAmount)

        Dim repoBalanceAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBalanceAmt.FormatString = ""
        repoBalanceAmt.HeaderText = "Balance Amt"
        repoBalanceAmt.Name = colBalanceAmt
        repoBalanceAmt.Width = 130
        repoBalanceAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBalanceAmt)

        Dim repoShareRatePerShare As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShareRatePerShare.FormatString = ""
        repoShareRatePerShare.HeaderText = "Share Rate Per Share"
        repoShareRatePerShare.Name = colRatePerShare
        repoShareRatePerShare.Width = 130
        repoShareRatePerShare.ReadOnly = True
        repoShareRatePerShare.ShowUpDownButtons = False

        gv1.MasterTemplate.Columns.Add(repoShareRatePerShare)

        Dim repoNoOfShareToBeAllocated As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNoOfShareToBeAllocated.FormatString = ""
        repoNoOfShareToBeAllocated.HeaderText = "No of Share to be allocated"
        repoNoOfShareToBeAllocated.Name = colNoOfShareToBeAllocated
        repoNoOfShareToBeAllocated.Width = 130
        repoNoOfShareToBeAllocated.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoNoOfShareToBeAllocated)

        Dim repoShareCertificateNoStartFrom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShareCertificateNoStartFrom.FormatString = ""
        repoShareCertificateNoStartFrom.HeaderText = "Share Certificate No Start From"
        repoShareCertificateNoStartFrom.Name = colShareCertificateNoStartFrom
        repoShareCertificateNoStartFrom.Width = 130
        repoShareCertificateNoStartFrom.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoShareCertificateNoStartFrom)

        Dim repoShareCertificateNoTo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShareCertificateNoTo.FormatString = ""
        repoShareCertificateNoTo.HeaderText = "Share Certificate No To"
        repoShareCertificateNoTo.Name = colShareCertificateNoTo
        repoShareCertificateNoTo.Width = 130
        repoShareCertificateNoTo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoShareCertificateNoTo)

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
        gv1.Rows.AddNew()
        gv1.BestFitColumns()
        ReStoreGridLayoutgv1()
    End Sub

    Private Sub ReStoreGridLayoutgv1()
        Try

            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub
    Function AllowToSave() As Boolean

        If txtRateOfOneShare.Value = 0 Then
            txtRateOfOneShare.Focus()
            Throw New Exception("Rate Of One Share can't be blank.")
        End If

        Dim isDocExist As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count(1) from TSPL_MULTIPLE_SHARE_ALLOTMENT_head where convert(date,from_date ,103)>=  convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) and convert(date,To_Date,103) >= convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)") > 0)
        If isDocExist Then
            Throw New Exception("Document already exist in this cycle")
        End If

        Return True
    End Function

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                obj = New clsMultipleShareAllotment()
                obj.Document_No = txtDocumentNo.Value
                obj.Rate_Of_One_Share = txtRateOfOneShare.Value
                obj.Document_date = clsCommon.myCDate(txtDocumentDate.Value)
                obj.From_Date = clsCommon.myCDate(txtFromDate.Value)
                obj.To_Date = clsCommon.myCDate(txtToDate.Value)
                obj.Arr = New List(Of clsMultipleShareAllotmentDetail)

                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsMultipleShareAllotmentDetail()
                    objTr.Vendor_Code = clsCommon.myCstr((grow.Cells(colVendorCode).Value))
                    objTr.Share_Opening_Amt = clsCommon.myCDecimal(grow.Cells(colShareCaptialOpeningAmount).Value)
                    objTr.Share_Deducted_Amt = clsCommon.myCDecimal((grow.Cells(colShareDeductedAmount).Value))
                    objTr.Balance_Amt = clsCommon.myCDecimal((grow.Cells(colBalanceAmt).Value))
                    objTr.Rate_Per_Share = clsCommon.myCDecimal((grow.Cells(colRatePerShare).Value))
                    objTr.Allocated_Share = clsCommon.myCDecimal((grow.Cells(colNoOfShareToBeAllocated).Value))
                    objTr.Share_Certificate_From = clsCommon.myCDecimal((grow.Cells(colShareCertificateNoStartFrom).Value))
                    objTr.Share_Certificate_To = clsCommon.myCDecimal((grow.Cells(colShareCertificateNoTo).Value))
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colDCSUploaderNo).Value)) > 0 Then
                        objTr.ArrAPInvoiceAllDetails = TryCast(grow.Cells(colDCSUploaderNo).Tag, List(Of clsMultipleShareAllotmentAPInvoiceDetail))
                        obj.Arr.Add(objTr)
                    End If
                Next


                If (obj.SaveData(obj, isNewEntry, Nothing, False)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Addnew()
        isNewEntry = True
        txtDocumentNo.Value = ""
        txtBMC.arrValueMember = Nothing
        btnSave.Enabled = True
        btnPost.Enabled = True
        txtDocumentDate.Value = clsCommon.GETSERVERDATE()
        btnSave.Text = "Save"
        txtRateOfOneShare.Value = 0
        txtRateOfOneShare.Enabled = True
        LoadBlankGrid()
        isInsideLoadData = False
        btnDelete.Enabled = True
        lblStatus.Status = ERPTransactionStatus.Pending
        ReStoreGridLayoutgv1()
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControls(True)
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
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtDocumentNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsMultipleShareAllotment.PostData(MyBase.Form_ID, txtDocumentNo.Value)
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
                If (clsMultipleShareAllotment.DeleteData(txtDocumentNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    btnAddNew.PerformClick()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub txtDocumentNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, isButtonClicked As System.Boolean) Handles txtDocumentNo._MYValidating
        If clsCommon.myLen(txtDocumentNo) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Document No can't be blank", Me.Text)
        End If
        txtDocumentNo.Value = clsMultipleShareAllotment.getFinder(txtDocumentNo.Value, isButtonClicked)
        LoadData(txtDocumentNo.Value, NavigatorType.Current)
    End Sub

    Private Sub txtDocumentNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD where Document_No='" + txtDocumentNo.Value + "' "
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
            LoadBlankGrid()
            obj = New clsMultipleShareAllotment()
            obj = clsMultipleShareAllotment.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Document_No)) > 0) Then
                isLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                If obj.Status = 1 Then
                    lblStatus.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnImport.Enabled = False
                Else
                    lblStatus.Status = ERPTransactionStatus.Pending
                    btnDelete.Enabled = True
                    btnImport.Enabled = True
                End If
                txtDocumentNo.Value = obj.Document_No
                txtDocumentDate.Value = obj.Document_date
                txtFromDate.Value = obj.From_Date
                txtToDate.Value = obj.To_Date
                txtRateOfOneShare.Value = obj.Rate_Of_One_Share

                If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                    For Each objtr As clsMultipleShareAllotmentDetail In obj.Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSUploaderNo).Value = objtr.VLC_Code_VLC_Uploader
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSUploaderNo).Tag = objtr.ArrAPInvoiceAllDetails
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVendorCode).Value = objtr.Vendor_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSName).Value = objtr.VLC_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShareCaptialOpeningAmount).Value = objtr.Share_Opening_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShareDeductedAmount).Value = objtr.Share_Deducted_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceAmt).Value = objtr.Balance_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRatePerShare).Value = objtr.Rate_Per_Share
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNoOfShareToBeAllocated).Value = objtr.Allocated_Share
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShareCertificateNoStartFrom).Value = objtr.Share_Certificate_From
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colShareCertificateNoTo).Value = objtr.Share_Certificate_To
                        gv1.Rows.AddNew()
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

    Private Sub frmMultipleShareAllotment_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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
            'Dim frm As New FrmPWD(Nothing)
            'frm.strType = "SIRC"
            'frm.strCode = "SIReversAndCreate"
            'frm.ShowDialog()
            'If frm.isPasswordCorrect Then
            '    '  btnReverseUnpost.Visible = True
            'End If
            Dim frm As frmMultipleShareAPInvoiceDetails = New frmMultipleShareAPInvoiceDetails()
            frm.strDCSCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCSUploaderNo).Value)
            frm.strDCSName = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCSName).Value)
            If isLoadData Then
                frm.arr = obj.ArrAPInvoiceDetails
            Else
                frm.arr = objtr.ArrAPInvoiceAllDetails
            End If
            frm.ShowDialog()
        End If
    End Sub


    Sub CancelPressed()
        Me.Close()
    End Sub


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If (txtRateOfOneShare.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Enter Rate of One Share", Me.Text)
            Exit Sub
        End If
        LoadBlankGrid()
        j = 0
        EnableDisableControls(False)
        isLoadData = False
        LoadGridData(isLoadData)
    End Sub

    Private Sub LoadGridData(ByVal isLoadData As Boolean)
        Try
            Dim qry As String = ""
            Dim Baseqry As String = ""
            Baseqry = " select TSPL_VENDOR_INVOICE_HEAD.Posting_Date as Date,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name, (TSPL_VENDOR_INVOICE_HEAD.Document_Total) as Deducted_Amt,1 as RI
						from TSPL_PAYMENT_PROCESS_DEDUCTION left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No 
						left outer join TSPL_DCS_ADDITION_DEDUCTION  on TSPL_DCS_ADDITION_DEDUCTION.Code = TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Code left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC
						where  TSPL_DCS_ADDITION_DEDUCTION.IsShare = 1 and TSPL_VENDOR_INVOICE_HEAD.Posting_Date >= convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103) < = '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' "

            If txtBMC.arrValueMember IsNot Nothing Then
                Baseqry += " and TSPL_VLC_MASTER_HEAD.MCC in ( " & clsCommon.GetMulcallString(txtBMC.arrValueMember) & " ) "
            End If
            qry = "select VLC_Code_VLC_Uploader,xxxx.Vendor_Code,VLC_Name,case when isnull(tab2.Opening_Amt,0) = 0 then 0 else tab2.Opening_Amt end as Opening_Amt,Deducted_Amt from ( select VLC_Code_VLC_Uploader,max(Vendor_CODE)Vendor_CODE,max(VLC_Name)VLC_Name,0 as Opening_Amt,sum(Deducted_Amt)Deducted_Amt,max(Date)Date from ( select max(Date)Date, AP_Invoice_No,max(VLC_Code_VLC_Uploader)VLC_Code_VLC_Uploader,max(Vendor_CODE)Vendor_CODE,max(VLC_Name)VLC_Name,sum(ri* Deducted_Amt)Deducted_Amt from ( " & Baseqry & " 
           " & Environment.NewLine & " union all  " & Environment.NewLine & " select null as Date,AP_Invoice_No,'' as VLC_Code_VLC_Uploader,'' as Vendor_CODE, '' as VLC_Name,Used_Amt as Deducted_Amt,-1 as RI
                       from TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL inner join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_MULTIPLE_SHARE_ALLOTMENT_AP_INVOICE_DETAIL.AP_Invoice_No where TSPL_VENDOR_INVOICE_HEAD.Posting_Date >= convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103) < = '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' )xx group by AP_Invoice_No ) xxx group by VLC_Code_VLC_Uploader "
            qry += " )xxxx outer APPLY ( SELECT TOP 1 TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Vendor_Code, TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.To_Date, TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Balance_Amt as Opening_Amt,TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Share_Deducted_Amt,TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Balance_Amt FROM TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD LEFT outer JOIN  
                TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL ON TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Document_No = TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No WHERE TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Vendor_Code = xxxx.Vendor_CODE  
			    and convert(date, TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.To_Date,103) <=  convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) ORDER BY TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.To_Date DESC  ) AS tab2 order  by Date, VLC_Code_VLC_Uploader "
            Baseqry = " select ROW_NUMBER( ) over( order by Date,VLC_Code_VLC_Uploader) as SNo, Date,AP_Invoice_No AS [AP Invoice No],VLC_Code_VLC_Uploader as [DCS Code],VLC_Name as [DCS Name],Deducted_Amt as [Balance Amount] from ( " & Baseqry & " )x order by date"

            Dim dtDetails As DataTable = clsDBFuncationality.GetDataTable(Baseqry)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt.Rows.Count > 0 Then
                obj = New clsMultipleShareAllotment()
                objtr.ArrAPInvoiceAllDetails = New List(Of clsMultipleShareAllotmentAPInvoiceDetail)

                For ii As Integer = 0 To dt.Rows.Count - 1
                    Dim Used_Amt As Decimal = 0
                    obj.ArrAPInvoiceDetails = New List(Of clsMultipleShareAllotmentAPInvoiceDetail)
                    Dim row As DataRow() = Nothing
                    row = dtDetails.Select("[DCS Code] ='" + clsCommon.myCstr(dt.Rows(ii)("VLC_Code_VLC_Uploader")) + "'")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = ii + 1
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSUploaderNo).Value = dt.Rows(ii)("VLC_Code_VLC_Uploader")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVendorCode).Value = dt.Rows(ii)("Vendor_CODE")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSName).Value = dt.Rows(ii)("VLC_Name")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colShareCaptialOpeningAmount).Value = dt.Rows(ii)("Opening_Amt")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colShareDeductedAmount).Value = dt.Rows(ii)("Deducted_Amt")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRatePerShare).Value = txtRateOfOneShare.Value
                    UpdateCurrentRow(ii)
                    Used_Amt = (txtRateOfOneShare.Value * gv1.Rows(gv1.Rows.Count - 1).Cells(colNoOfShareToBeAllocated).Value)

                    For j As Integer = 0 To row.Length - 1
                        Dim objInvoice As clsMultipleShareAllotmentAPInvoiceDetail = New clsMultipleShareAllotmentAPInvoiceDetail()
                        objInvoice.AP_Date = clsCommon.myCstr(row(j)("Date"))
                        objInvoice.VLC_Code_VLC_Uploader = clsCommon.myCstr(row(j)("DCS Code"))
                        objInvoice.VLC_Name = clsCommon.myCstr(row(j)("DCS Name"))
                        objInvoice.AP_Invoice_No = clsCommon.myCstr(row(j)("AP Invoice No"))
                        objInvoice.Balance_Amt = clsCommon.myCdbl(row(j)("Balance Amount"))
                        If Used_Amt > objInvoice.Balance_Amt Then
                            objInvoice.Used_Amt = clsCommon.myCdbl(row(j)("Balance Amount"))
                            Used_Amt = Used_Amt - objInvoice.Balance_Amt
                        Else
                            objInvoice.Used_Amt = Used_Amt
                        End If
                        obj.ArrAPInvoiceDetails.Add(objInvoice)
                        objtr.ArrAPInvoiceAllDetails.Add(objInvoice)
                    Next
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSUploaderNo).Tag = obj.ArrAPInvoiceDetails
                    gv1.Rows.AddNew()

                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No DCS Found", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim dblDeducted_Amt As Double = 0
        Dim dblBalance_Amt As Double = 0
        Dim dblRatePerShare As Double = 0
        Dim dblAllocatedShare As Double = 0
        Dim dblShareCertificate_From As Double = 0
        Dim dbldblShareCertificate_To As Double = 0

        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colDCSUploaderNo).Value)) > 0 Then
            dblDeducted_Amt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colShareDeductedAmount).Value)
            dblRatePerShare = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRatePerShare).Value)
            dblAllocatedShare = Math.Floor(dblDeducted_Amt / dblRatePerShare)
            If dblAllocatedShare > 0 Then
                dblBalance_Amt = (dblDeducted_Amt Mod dblRatePerShare)
            Else
                dblBalance_Amt = dblDeducted_Amt
            End If
            gv1.Rows(IntRowNo).Cells(colNoOfShareToBeAllocated).Value = dblAllocatedShare
            gv1.Rows(IntRowNo).Cells(colBalanceAmt).Value = dblBalance_Amt


            If gv1.Rows(IntRowNo).Cells(colNoOfShareToBeAllocated).Value > 0 Then
                gv1.Rows(IntRowNo).Cells(colShareCertificateNoStartFrom).Value = j + 1
                gv1.Rows(IntRowNo).Cells(colShareCertificateNoTo).Value = j + dblAllocatedShare
                j = gv1.Rows(IntRowNo).Cells(colShareCertificateNoTo).Value
            End If
        End If
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        clsCommon.MyExportToExcelGrid("", gv1, Nothing, Me.Text)
        clsCommon.MyMessageBoxShow(Me, "Exported Successfully", Me.Text)
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim gvImport As New RadGridView()

        Me.Controls.Add(gvImport)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gvImport, "DCS Code", "DCS Name", "Share Captial Opening Amount", "Total Share Captial Deducted Amount", "Balance Amt", "Share Rate Per Share", "No of Share to be allocated", "Share Certificate No Start From", "Share Certificate No To") Then
            Try
                clsCommon.ProgressBarPercentShow()
                For ii As Integer = 0 To gvImport.Rows.Count - 1
                    If clsCommon.myLen(gvImport.Rows(ii).Cells("DCS Code").Value) > 0 Then

                        clsCommon.ProgressBarPercentUpdate((gvImport.Rows(ii).Index + 1) * 100 / (gvImport.Rows.Count + 1), "Importing  : " & (gvImport.Rows(ii).Index + 1) & "/" & gvImport.Rows.Count & "")
                        Try

                            gv1.Rows(ii).Cells("DCS Code").Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("DCS Code").Value)
                            gv1.Rows(ii).Cells("DCS Name").Value = clsDBFuncationality.getSingleValue("Select VLC_NAME FROM TSPL_VLC_MASTER_HEAD VLC_Code_VLC_Uploader = '" & clsCommon.myCstr(gvImport.Rows(ii).Cells("DCS Code").Value) & "'")
                            gv1.Rows(ii).Cells("Vendor Code").Value = clsDBFuncationality.getSingleValue("Select VSP_CODE FROM TSPL_VLC_MASTER_HEAD VLC_Code_VLC_Uploader = '" & clsCommon.myCstr(gvImport.Rows(ii).Cells("DCS Code").Value) & "'")
                            If clsCommon.CompairString(gvImport.Rows(ii).Cells("Head Load Basis").Value, "Rate/Kg") = CompairStringResult.Equal OrElse clsCommon.CompairString(gvImport.Rows(ii).Cells("Head Load Basis").Value, "Rate/Ltr") = CompairStringResult.Equal OrElse clsCommon.CompairString(gvImport.Rows(ii).Cells("Head Load Basis").Value, "Cycle Wise Rate/Kg") = CompairStringResult.Equal OrElse clsCommon.CompairString(gvImport.Rows(ii).Cells("Head Load Basis").Value, "Cycle Wise Rate/Ltr") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(gvImport.Rows(ii).Cells("Head Load Basis").Value, "Rate/Kg") = CompairStringResult.Equal Then
                                    gv1.Rows(ii).Cells("Head Load Basis").Value = "K"
                                ElseIf clsCommon.CompairString(gvImport.Rows(ii).Cells("Head Load Basis").Value, "Rate/Ltr") = CompairStringResult.Equal Then
                                    gv1.Rows(ii).Cells("Head Load Basis").Value = "L"
                                ElseIf clsCommon.CompairString(gvImport.Rows(ii).Cells("Head Load Basis").Value, "Cycle Wise Rate/Kg") = CompairStringResult.Equal Then
                                    gv1.Rows(ii).Cells("Head Load Basis").Value = "CK"
                                ElseIf clsCommon.CompairString(gvImport.Rows(ii).Cells("Head Load Basis").Value, "Cycle Wise Rate/Ltr") = CompairStringResult.Equal Then
                                    gv1.Rows(ii).Cells("Head Load Basis").Value = "CL"
                                End If
                            Else
                                clsCommon.MyMessageBoxShow(Me, " This Head load basis does not exist in Head Load Master", Me.Text)
                                Exit Sub
                            End If
                            gv1.Rows(ii).Cells(colShareCaptialOpeningAmount).Value = Math.Round(clsCommon.myCDecimal(gvImport.Rows(ii).Cells("Share Captial Opening Amount").Value), 2)

                            gv1.Rows(ii).Cells(colShareDeductedAmount).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("Total Share Captial Deducted Amount").Value)
                            UpdateCurrentRow(ii)
                            'gv1.Rows(ii).Cells(colBalanceAmt).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("Balance Amt").Value)
                            'gv1.Rows(ii).Cells(colRatePerShare).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("Share Rate Per Share").Value)
                            'gv1.Rows(ii).Cells(colNoOfShareToBeAllocated).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("Total Share Captial Deducted Amount").Value)
                            txtRateOfOneShare.Value = gv1.Rows(ii).Cells(colRatePerShare).Value
                            If clsCommon.myLen(txtDocumentNo.Value) = 0 Then
                                If gv1.Rows.Count = gvImport.Rows.Count Then
                                Else
                                    gv1.Rows.AddNew()
                                End If
                            End If

                        Catch ex As Exception
                            gv1.Rows.RemoveAt(ii)
                            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                        End Try
                    End If
                Next

                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data imported successfully", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gvImport)
    End Sub

    Private Sub txtBMC__My_Click(sender As Object, e As EventArgs) Handles txtBMC._My_Click
        Try
            Dim qry As String = "select MCC_Code as [MCC Code],MCC_NAME as [MCC Name] from tspl_mcc_master where In_active = 0 "
            txtBMC.arrValueMember = clsCommon.ShowMultipleSelectForm("AllotmentBMC", qry, "MCC Code", "MCC Name", txtBMC.arrValueMember, txtBMC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExportBlank_Click(sender As Object, e As EventArgs) Handles btnExportBlank.Click
        Try
            Dim qry As String = "select '' as SNo,'' as [DCS Code],'' as [DCS Name] , '' as [Share Captial Opening Amount],'' as [Total Share Captial Deducted Amount] , '' as [Balance Amt],'' as [Share Rate Per Share],'' as	[No of Share to be allocated] , '' as	[Share Certificate No Start From] ,'' as [Share Certificate No To] "
            Dim Whrcls As String = " [DCS Code] "
            transportSql.ExporttoExcel(qry, "", Whrcls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserDeletedRow
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colSNo).Value = ii
        Next
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If gv1.CurrentCell.ColumnInfo.Name Is colDCSUploaderNo Then
            OpenAPInvoiceDetails()
        End If
    End Sub
    Sub OpenAPInvoiceDetails()
        Dim frm As frmMultipleShareAPInvoiceDetails = New frmMultipleShareAPInvoiceDetails()
        frm.strDCSCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCSUploaderNo).Value)
        frm.strDCSName = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCSName).Value)
        frm.arr = TryCast(gv1.CurrentRow.Cells(colDCSUploaderNo).Tag, List(Of clsMultipleShareAllotmentAPInvoiceDetail))
        frm.ShowDialog()
        If Not frm.isCencelButtonClicked Then
            gv1.CurrentRow.Cells(colDCSUploaderNo).Tag = frm.arr
        End If

    End Sub

End Class








