
'--------Created By Richa 18/11/2014 Against Ticket No BM00000003894
Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI

Public Class FrmTDSPayment
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean
    Public isInsideLoadData As Boolean = False
    Dim dtAllData As DataTable = Nothing
    Dim dtAllDataDetail As DataTable = Nothing
    Dim dtmain As DataTable = Nothing
    Dim isFlag As Boolean = False
    Public Const colSlNo As String = "SLNO"
    Public Const colDocType As String = "colDocType"
    Public Const colNatureDeductionCode As String = "colNatureDeductionCode"
    Public Const colNatureDeductionDesc As String = "colNatureDeductionDesc"
    Public Const colagainstDocNo As String = "colagainstDocNo"
    Public Const colVendorCode As String = "colVendorCode"
    Public Const colVendorDesc As String = "colVendorDesc"
    Public Const colPAN As String = "colPAN"
    Public Const colagainstDocDate As String = "colagainstDocDate"
    Public Const colTaxableAmt As String = "colTaxableAmt"
    Public Const colDocAmt As String = "colDocAmt"
    Public Const colIncometax As String = "colIncometax"
    Public Const ColTotalTDS As String = "ColTotalTDS"
    Public Const ColRateforTaxDeducted As String = "ColRateforTaxDeducted"
    Public Const colLocationCode As String = "colLocationCode"
    Public Const colLocationName As String = "colLocationName"
    Public Const colDeductCode As String = "colDeductCode"
    Public Const colDescription As String = "colDescription"
    Public Const colGLAccount As String = "colGLAccount"
    Dim arrLoc As String = Nothing
    Public Shared Alocation As String = Nothing

    Dim Qry As String = String.Empty

#End Region
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmCreateAutoInvoiceBS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub

    Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub
    Sub Reset()
        txtDocNo.Value = ""
        txtDocNo.MyReadOnly = False
        FndBankCode.Value = ""
        lblBankName.Text = ""
        fndLocation.Value = ""
        lblLocationName.Text = ""
        fndSectionCode.Value = ""
        lblSectionName.Text = ""
        txtBSRCode.Text = ""
        txtChallanNo.Text = ""
        TxtTotalAmount.Value = 0
        dtpChallanDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpFromdate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtptodate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        txtDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        loadBlankItemGrid()
        lblPending.Status = ERPTransactionStatus.Pending
        lblPayment_No.Text = ""
        txtPaymentMode.Value = ""
        chkPDC.Checked = False
        chkCheckPrint.Checked = False
        txtChequeNo.Text = ""
        dtpChequeDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        If DateTime = "1" Then
            txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpFromdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpChallanDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtptodate.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpChequeDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            txtDate.CustomFormat = "dd/MM/yyyy"
            dtpFromdate.CustomFormat = "dd/MM/yyyy"
            dtpChallanDate.CustomFormat = "dd/MM/yyyy"
            dtptodate.CustomFormat = "dd/MM/yyyy"
            dtpChequeDate.CustomFormat = "dd/MM/yyyy"
        End If

        txtNatureofDeduction.arrValueMember = Nothing
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnsave.Enabled = True
        isNewEntry = True
        LOCATIONRIGTHS()
        btnupdate.Visible = False
        btnupdate.Enabled = False
        fndLocation.Enabled = True
        ReStoreGridLayout()
    End Sub

    Private Sub txtPaymentMode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPaymentMode._MYValidating
        Dim strbankcode As String
        If Not String.IsNullOrEmpty(connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + FndBankCode.Value + "'")) Then
            strbankcode = connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + FndBankCode.Value + "'")
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
            HandleCheque()
        End If
    End Sub

    Public Sub HandleCheque()
        If clsCommon.CompairString(txtPaymentMode.Value, "Cheque") = CompairStringResult.Equal Or clsCommon.CompairString(txtPaymentMode.Value, "DD") = CompairStringResult.Equal Then
            pnlCheque.Visible = True
            txtChequeNo.Text = ""
            dtpChequeDate.Value = clsCommon.GETSERVERDATE
            txtChequeNo.Enabled = True
            dtpChequeDate.Enabled = True
        Else
            pnlCheque.Visible = False
            txtChequeNo.Text = ""
            dtpChequeDate.Value = Nothing
            txtChequeNo.Enabled = False
            dtpChequeDate.Enabled = False
        End If
    End Sub
    Private Sub chkCheckPrint_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCheckPrint.ToggleStateChanged
        If Me.chkCheckPrint.Checked Then
            Me.txtChequeNo.Enabled = False
        Else
            Me.txtChequeNo.Enabled = True
        End If
    End Sub
   
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                'lblLocationCode.Value = obj.Default_LocCode
                'LblLocationName.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
                Alocation = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.TDSPAYMENT)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub
    Private Sub FrmCreateAutoInvoiceBS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N New Transaction")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub RdbSavelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdbSavelayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID & "gv1"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                gv1.MasterTemplate.FilterDescriptors.Clear()
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj = Nothing

            ''richa agarwal regarding memory leakage
            obj = Nothing
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub RdDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RdDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gv1", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
    Sub loadBlankItemGrid()

        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing

        Dim lineNo As New GridViewCheckBoxColumn()
        lineNo.HeaderText = "SL. No."
        lineNo.Name = colSlNo
        lineNo.Width = 60
        lineNo.ReadOnly = False
        lineNo.WrapText = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(lineNo)


        Dim Doctype As New GridViewTextBoxColumn()
        Doctype.FormatString = ""
        Doctype.HeaderText = "DocType"
        Doctype.Name = colDocType
        Doctype.Width = 100
        Doctype.ReadOnly = True
        Doctype.WrapText = True
        Doctype.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(Doctype)

        Dim DeductionCode As New GridViewTextBoxColumn()
        DeductionCode.FormatString = ""
        DeductionCode.HeaderText = "Deduction Code"
        DeductionCode.Name = colNatureDeductionCode
        DeductionCode.Width = 100
        DeductionCode.ReadOnly = True
        DeductionCode.WrapText = True
        DeductionCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(DeductionCode)

        Dim DeductionDesc As New GridViewTextBoxColumn()
        DeductionDesc.FormatString = ""
        DeductionDesc.HeaderText = "Deduction Description"
        DeductionDesc.Name = colNatureDeductionDesc
        DeductionDesc.Width = 100
        DeductionDesc.ReadOnly = True
        DeductionDesc.WrapText = True
        DeductionDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(DeductionDesc)

        Dim VendorCode As New GridViewTextBoxColumn()
        VendorCode.FormatString = ""
        VendorCode.HeaderText = "vendor Code"
        VendorCode.Name = colVendorCode
        VendorCode.Width = 100
        VendorCode.ReadOnly = True
        VendorCode.WrapText = True
        VendorCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(VendorCode)

        Dim VendorName As New GridViewTextBoxColumn()
        VendorName.FormatString = ""
        VendorName.HeaderText = "Vendor Name"
        VendorName.Name = colVendorDesc
        VendorName.Width = 100
        VendorName.ReadOnly = True
        VendorName.WrapText = True
        VendorName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(VendorName)

        Dim PAN As New GridViewTextBoxColumn()
        PAN.FormatString = ""
        PAN.HeaderText = "PAN"
        PAN.Name = colPAN
        PAN.Width = 100
        PAN.ReadOnly = True
        PAN.WrapText = True
        PAN.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(PAN)

        Dim AgainstDocumentNo As New GridViewTextBoxColumn()
        AgainstDocumentNo.FormatString = ""
        AgainstDocumentNo.HeaderText = "Document No"
        AgainstDocumentNo.Name = colagainstDocNo
        AgainstDocumentNo.Width = 100
        AgainstDocumentNo.ReadOnly = True
        AgainstDocumentNo.WrapText = True
        AgainstDocumentNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(AgainstDocumentNo)

        Dim DocumentDate As New GridViewTextBoxColumn()
        DocumentDate.FormatString = ""
        DocumentDate.HeaderText = "Document Date"
        DocumentDate.Name = colagainstDocDate
        DocumentDate.Width = 100
        DocumentDate.ReadOnly = True
        DocumentDate.WrapText = True
        DocumentDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(DocumentDate)

        Dim taxableAmt As New GridViewDecimalColumn
        taxableAmt.FormatString = ""
        taxableAmt.HeaderText = "Taxable Amt"
        taxableAmt.Name = colTaxableAmt
        taxableAmt.Width = 100
        taxableAmt.ReadOnly = True
        taxableAmt.WrapText = True
        taxableAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(taxableAmt)

        Dim DocumentAmt As New GridViewDecimalColumn
        DocumentAmt.FormatString = ""
        DocumentAmt.HeaderText = "Document Amt"
        DocumentAmt.Name = colDocAmt
        DocumentAmt.Width = 100
        DocumentAmt.ReadOnly = True
        DocumentAmt.WrapText = True
        DocumentAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(DocumentAmt)

        Dim incometax As New GridViewDecimalColumn
        incometax.FormatString = ""
        incometax.HeaderText = "Income tax"
        incometax.Name = colIncometax
        incometax.Width = 100
        incometax.ReadOnly = True
        incometax.WrapText = True
        incometax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(incometax)


        Dim totaltds As New GridViewDecimalColumn
        totaltds.FormatString = ""
        totaltds.HeaderText = "Total TDS"
        totaltds.Name = ColTotalTDS
        totaltds.Width = 100
        totaltds.ReadOnly = True
        totaltds.WrapText = True
        totaltds.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(totaltds)


        Dim rateatwhichtax As New GridViewDecimalColumn
        rateatwhichtax.FormatString = ""
        rateatwhichtax.HeaderText = "Rate at which tax Deduct"
        rateatwhichtax.Name = ColRateforTaxDeducted
        rateatwhichtax.Width = 100
        rateatwhichtax.ReadOnly = True
        rateatwhichtax.WrapText = True
        rateatwhichtax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(rateatwhichtax)

        Dim LocationCode As New GridViewTextBoxColumn()
        LocationCode.FormatString = ""
        LocationCode.HeaderText = "Location Code"
        LocationCode.Name = colLocationCode
        LocationCode.Width = 100
        LocationCode.ReadOnly = True
        LocationCode.WrapText = True
        LocationCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(LocationCode)

        Dim LocationName As New GridViewTextBoxColumn()
        LocationName.FormatString = ""
        LocationName.HeaderText = "Location Name"
        LocationName.Name = colLocationName
        LocationName.Width = 250
        LocationName.ReadOnly = True
        LocationName.WrapText = True
        LocationName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(LocationName)

        Dim DeductCode As New GridViewTextBoxColumn()
        DeductCode.FormatString = ""
        DeductCode.HeaderText = "Deduct Code"
        DeductCode.Name = colDeductCode
        DeductCode.Width = 70
        DeductCode.ReadOnly = True
        DeductCode.WrapText = True
        DeductCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(DeductCode)

        Dim Description As New GridViewTextBoxColumn()
        Description.FormatString = ""
        Description.HeaderText = "Description"
        Description.Name = colDescription
        Description.Width = 70
        Description.ReadOnly = True
        Description.WrapText = True
        Description.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(Description)


        Dim glaccount As New GridViewTextBoxColumn()
        glaccount.FormatString = ""
        glaccount.HeaderText = "GL Account"
        glaccount.Name = colGLAccount
        glaccount.Width = 70
        glaccount.ReadOnly = True
        glaccount.WrapText = True
        glaccount.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(glaccount)
        ''----------------

        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = True
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnReorder = True
        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.Rows.AddNew()
        ReStoreGridLayout()
    End Sub
    Private Sub ReStoreGridLayout()
        Dim obj As clsGridLayout = New clsGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                obj = CType(obj.GetData(Form_ID & "gv1", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If

        Catch err As Exception
            MessageBox.Show(err.Message)
        Finally
            obj = Nothing
        End Try
    End Sub
    Public Sub LoadTDSDetail()
        Qry = "   select ISNULL(FINAL.Document_Type,'') AS Document_Type,final.Section_Description as [Deduction Code],TSPL_TDS_DEDUCTION_HEAD.Description as [Deduction Description]," & _
            " final.docnumber,final.Vendor_Code,TSPL_TDS_VENDOR_DETAILS.PAN as PANNo ,final.Vendor_Name,final.docdate ,final.baseamount,final.Document_Amount, Convert(Decimal(18,0),final.Credit) as Credit , (Convert(Decimal(18,0),final.Credit)+Actual_Surcharge+Actual_Edu_Cess+Actual_Sec_Educess) as total , final.TDS_Per,final.DeductCode ,  final.Description, Account_Seg_Code7 ,Location_Desc,TSPL_TDS_DEDUCTION_HEAD.Gl_Account ,convert(date,docdate,103) as OrderDate " & _
            " from (   select TSPL_REMITTANCE.Document_Type,0 as is_For_TDS,TSPL_REMITTANCE_ENTRY.Section_Code,TSPL_REMITTANCE_ENTRY.Section_Description ,TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code , " & _
            " TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Name ,TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code as docnumber,convert(varchar(12),Remittance_Date,103) as  docdate,TSPL_REMITTANCE_ENTRY_DETAIL.Document_No  as DN, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_TDS_Base as  baseamount, " & _
            " TSPL_REMITTANCE.Document_Amount,TSPL_GL_ACCOUNTS.Account_Seg_Code7,case when TSPL_REMITTANCE_ENTRY_DETAIL.Document_Type  <>'D' then TSPL_REMITTANCE_ENTRY_DETAIL.Actual_TDS  else null end as Debit,case when TSPL_REMITTANCE_ENTRY_DETAIL.Document_Type  = 'D' " & _
            " then TSPL_REMITTANCE_ENTRY_DETAIL.Actual_TDS  else null end as Credit ,TSPL_REMITTANCE_ENTRY_DETAIL.Deduction_Code ,  TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2,0 as  TDS_Per ,'' as DeductCode,TSPL_REMITTANCE . Actual_Surcharge ," & _
            " TSPL_REMITTANCE . Actual_Edu_Cess,TSPL_REMITTANCE . Actual_Sec_Educess, TSPL_REMITTANCE_ENTRY.Description,TSPL_GL_SEGMENT_CODE.Description as Location_Desc from TSPL_REMITTANCE_ENTRY   " & _
            " left outer join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY.Remittance_Code=TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code  " & _
            " left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_REMITTANCE_ENTRY.Comp_Code  " & _
            " left outer join  TSPL_REMITTANCE on TSPL_REMITTANCE .Remittance_Code =TSPL_REMITTANCE_ENTRY.Remittance_Code   " & _
            " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_REMITTANCE .Branch_GL_AC  " & _
            " left join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_GL_ACCOUNTS.Account_Seg_Code7 AND Segment_name='Locations' " & _
            " where 2=2  and (Posted is not null)  union all  select TSPL_REMITTANCE.Document_Type,isnull(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0) as is_For_TDS," & _
            " TSPL_REMITTANCE.Section_Code  as section ,TSPL_REMITTANCE.Deduction_Code as natureofdeduction ,TSPL_REMITTANCE.Vendor_Code  as vendor,TSPL_REMITTANCE.Vendor_Name as Name," & _
            " TSPL_REMITTANCE.Document_No as docnumber ,Document_Date  as docdate,TSPL_REMITTANCE.Document_No as DN, case when ISNULL(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0)<>1 Then " & _
            " Actual_TDS_Base WHEN  ISNULL(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='AP' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0)=1  THEN (SELECT ISNULL(Document_Total,0) AS Document_Total FROM TSPL_VENDOR_INVOICE_HEAD VH WHERE ISNULL(VH.Document_No ,'')=TSPL_VENDOR_INVOICE_HEAD.RefDocNo )  WHEN  ISNULL(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='A' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0)=1  THEN (SELECT ISNULL(Payment_Amount ,0) AS Payment_Amount FROM TSPL_PAYMENT_HEADER WHERE ISNULL(TSPL_PAYMENT_HEADER.Payment_No ,'')=(SELECT ISNULL(AgainstPayment_No,'') AS AgainstPayment_No FROM TSPL_VENDOR_INVOICE_DETAIL WHERE Document_No=TSPL_REMITTANCE.Document_No))  Else 0 End as baseamount,TSPL_REMITTANCE.Document_Amount,TSPL_GL_ACCOUNTS.Account_Seg_Code7, 0 as Debit, case when TSPL_REMITTANCE.Document_Type = 'D' then Actual_TDS* (case when TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 then 1 else  -1 end)when TSPL_REMITTANCE.Document_Type = 'C' then Actual_TDS* (case when TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 then -1 else 1 end) else Actual_TDS end as Credit, TSPL_REMITTANCE.Deduction_Code,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,   Case When ISNULL(TSPL_REMITTANCE.TDS_Per,0)<>0 Then TSPL_REMITTANCE.TDS_Per Else (Select Top(1) TSPL_VENDOR_INVOICE_DETAIL.TDS_Per From TSPL_VENDOR_INVOICE_DETAIL WHERE TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No) End as TDS_Per,  case when Select_By='C' then '01' else '02' end as DeductCode ,Actual_Surcharge ,Actual_Edu_Cess,Actual_Sec_Educess, TSPL_VENDOR_INVOICE_HEAD.Description,TSPL_GL_SEGMENT_CODE.Description as Location_Desc from TSPL_REMITTANCE   left outer join TSPL_COMPANY_MASTER on  TSPL_REMITTANCE.comp_code=TSPL_COMPANY_MASTER.Comp_Code  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_REMITTANCE .Branch_GL_AC  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_REMITTANCE.Document_No  left join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_GL_ACCOUNTS.Account_Seg_Code7 AND Segment_name='Locations'  where 2=2 and Remit_TDS in ('Y','N')  union all select TSPL_REMITTANCE.Document_Type,0 as is_For_TDS,TSPL_REMITTANCE.Section_Code  as section ,TSPL_REMITTANCE.Deduction_Code as natureofdeduction ,TSPL_REMITTANCE.Vendor_Code  as vendor,TSPL_REMITTANCE.Vendor_Name as Name,TSPL_REMITTANCE.Document_No as docnumber ,Document_Date  as docdate,TSPL_REMITTANCE.Document_No as DN,Actual_TDS_Base as baseamount,TSPL_REMITTANCE.Document_Amount,TSPL_GL_ACCOUNTS.Account_Seg_Code7,  0 AS Debit,   case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then -1*(TSPL_BANK_REVERSE.Amount- TSPL_PAYMENT_HEADER.Payment_Amount) else null end as credit,TSPL_REMITTANCE.Deduction_Code,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_REMITTANCE.TDS_Per ,case when Select_By='C' then '01' else '02' end as DeductCode ,Actual_Surcharge ,Actual_Edu_Cess,Actual_Sec_Educess, TSPL_PAYMENT_HEADER.Entry_Desc as Description,TSPL_GL_SEGMENT_CODE.Description as Location_Desc from TSPL_REMITTANCE   left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No    inner join  TSPL_BANK_REVERSE on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No  left outer join TSPL_COMPANY_MASTER on  TSPL_REMITTANCE.comp_code=TSPL_COMPANY_MASTER.Comp_Code     left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_REMITTANCE .Branch_GL_AC left join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_GL_ACCOUNTS.Account_Seg_Code7 AND Segment_name='Locations' where 2=2 and Remit_TDS in ('Y','N')   ) final left outer join TSPL_TDS_VENDOR_DETAILS on TSPL_TDS_VENDOR_DETAILS.Vendor_Code=final.Vendor_Code   left join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code =final.Deduction_Code   left join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section  " & _
            " where 2=2   and convert(date,docdate,103)>='" & clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MMM/yyyy") & "' and convert(date,docdate,103)<= '" & clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy") & "'  " & _
            " and  coalesce(TSPL_TDS_DEDUCTION_HEAD.TDS_Section,'')='" + clsCommon.myCstr(fndSectionCode.Value) + "'   "
        If txtNatureofDeduction.arrValueMember IsNot Nothing AndAlso txtNatureofDeduction.arrValueMember.Count > 0 Then
            Qry += " and final.Deduction_Code in (" + clsCommon.GetMulcallString(txtNatureofDeduction.arrValueMember) + ")   "
        End If
        ' Qry += "  and docnumber not in (Select Against_Document_No from TSPL_TDS_PAYMENT_DETAIL where Document_No <>'" & clsCommon.myCstr(txtDocNo.Value) & "') " & _
        Qry += " and docnumber not in (Select Against_Document_No from TSPL_TDS_PAYMENT_DETAIL where Document_No <>'" & clsCommon.myCstr(txtDocNo.Value) & "' " & _
        " and Document_No not in (Select Against_TDS_PAYMENT_No from TSPL_PAYMENT_HEADER where Payment_No in (select Document_No  from tspl_bank_reverse where Reverse_Document ='Payments') and isnull(TSPL_PAYMENT_HEADER.Against_TDS_PAYMENT_No,'')<>'' ))" & _
        " AND (Convert(Decimal(18,0),final.Credit)+Actual_Surcharge+Actual_Edu_Cess+Actual_Sec_Educess)<>0 order by OrderDate "

        dtAllDataDetail = clsDBFuncationality.GetDataTable(Qry)
        If dtAllDataDetail Is Nothing OrElse dtAllDataDetail.Rows.Count <= 0 Then
            loadBlankItemGrid()
            common.clsCommon.MyMessageBoxShow(Me, "No data found ", Me.Text)
        Else
            LoadDetailData()
        End If
    End Sub

    Sub LoadDetailData()
        loadBlankItemGrid()
        For Each dr As DataRow In dtAllDataDetail.Rows
            gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = True
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDocType).Value = clsCommon.myCstr(dr("Document_Type"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colNatureDeductionCode).Value = clsCommon.myCstr(dr("Deduction Code"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colNatureDeductionDesc).Value = clsCommon.myCstr(dr("Deduction Description"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colagainstDocNo).Value = clsCommon.myCstr(dr("docnumber"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colagainstDocDate).Value = clsCommon.myCstr(clsCommon.GetPrintDate(dr("docdate"), "dd/MM/yyyy"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colVendorCode).Value = clsCommon.myCstr(dr("Vendor_Code"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colVendorDesc).Value = clsCommon.myCstr(dr("Vendor_Name"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colPAN).Value = clsCommon.myCstr(dr("PANNo"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmt).Value = clsCommon.myCdbl(dr("baseamount"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDocAmt).Value = clsCommon.myCdbl(dr("Document_Amount"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colIncometax).Value = clsCommon.myCdbl(dr("Credit"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(ColTotalTDS).Value = clsCommon.myCdbl(dr("total"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(ColRateforTaxDeducted).Value = clsCommon.myCdbl(dr("TDS_Per"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDeductCode).Value = clsCommon.myCstr(dr("DeductCode"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colDescription).Value = clsCommon.myCstr(dr("Description"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = clsCommon.myCstr(dr("Account_Seg_Code7"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = clsCommon.myCstr(dr("Location_Desc"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colGLAccount).Value = clsCommon.myCstr(dr("Gl_Account"))
            gv1.Rows.AddNew()
        Next
        gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
        updatetotalAmount()
    End Sub

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myLen(fndSectionCode.Value) <= 0 Then
                Throw New Exception("Please select Section")
            End If
            'If txtNatureofDeduction.arrValueMember Is Nothing AndAlso txtNatureofDeduction.arrValueMember.Count <= 0 Then
            '    Throw New Exception("Please select Nature of deduction")
            'End If
            If clsCommon.myCDate(dtpFromdate.Value) > clsCommon.myCDate(dtptodate.Value) Then
                Throw New Exception("From date cannot be greater than To date")
            End If
           
            LoadTDSDetail()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Focus()
            Return False
        End If
        Dim count As Integer = 0

        If clsCommon.myCDate(dtpFromdate.Value) > clsCommon.myCDate(dtptodate.Value) Then
            Throw New Exception("From date cannot be greater than To date")
        End If
        'If txtNatureofDeduction.arrValueMember Is Nothing AndAlso txtNatureofDeduction.arrValueMember.Count <= 0 Then
        '    Throw New Exception("Please select Nature of deduction")
        'End If
        If clsCommon.myCDate(dtpFromdate.Value) > clsCommon.myCDate(dtptodate.Value) Then
            Throw New Exception("From date cannot be greater than To date")
        End If
        If clsCommon.myLen(txtPaymentMode.Value) <= 0 Then
            txtPaymentMode.Focus()
            Throw New Exception("Payment Mode can't be blank")
        End If

        '------Checks Whether ChequeNo is Blank Or Not, If CHeck ==Is Not Blank Then It Is Already Used Or Not---------
        Dim strcheckcode As String = connectSql.RunScalar("select Payment_Type  from TSPL_PAYMENT_CODE  where Payment_Code ='" + txtPaymentMode.Value + "'")
        If clsCommon.CompairString(strcheckcode, "Cheque") = CompairStringResult.Equal Or clsCommon.CompairString(txtPaymentMode.Value, "DD") = CompairStringResult.Equal Then
            If txtChequeNo.Text = "" Then
                If (chkCheckPrint.Visible And chkCheckPrint.Checked = False) Then
                    txtChequeNo.Focus()
                    Throw New Exception("Cheque No can't be blank")

                End If

            Else
                Dim check As String = "Select Payment_No from TSPL_PAYMENT_HEADER Where Cheque_No='" + txtChequeNo.Text + "' AND isnull(Against_TDS_PAYMENT_No,'') <> '" + txtDocNo.Value + "'"
                Dim chk As String = clsDBFuncationality.getSingleValue(check)
                If clsCommon.myLen(chk) > 0 Then
                    txtChequeNo.Text = ""
                    txtChequeNo.Focus()
                    Throw New Exception("This Cheque No '" + txtChequeNo.Text + "' is Already Exists Against Payment No '" + chk + "'")
                End If

                If clsCommon.myLen(txtChequeNo.Text) > 0 Then
                    If clsCommon.myLen(txtChequeNo.Text) < 6 Then
                        txtChequeNo.Focus()
                        Throw New Exception("Length of Cheque No should be of 6 digits.")
                    End If
                End If

            End If

        End If

        If gv1.Rows.Count <= 0 Then
            Throw New Exception("There is no document to create TDS payment")
        End If
        'For i As Integer = 0 To gv1.Rows.Count - 1

        '    If clsCommon.myLen(gv1.Rows(i).Cells(colDispatchNo).Value) >= 0 Then
        '        count = count + 1
        '    End If
        '    If count = 0 Then
        '        Throw New Exception("There is no dispatch to create invoice")
        '    End If
        'Next

        Return True
    End Function

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SaveData()
        Dim obj As New ClsTDSPayment()
        Dim objTr As New ClsTDSPaymentDetail
        Dim objND_TR As New ClsTDSPaymentDetailTDS_ND
        Try
            If AllowToSave() Then

                obj.Document_No = clsCommon.myCstr(txtDocNo.Value)
                obj.Document_Date = clsCommon.myCDate(txtDate.Value)
                obj.Bank_Code = clsCommon.myCstr(FndBankCode.Value)
                obj.TDS_Section_Code = clsCommon.myCstr(fndSectionCode.Value)
                obj.TDS_Deduction_Code = clsCommon.myCstr("" + clsCommon.GetMulcallString(txtNatureofDeduction.arrValueMember) + "")
                obj.Location_Code = clsCommon.myCstr(fndLocation.Value)
                obj.From_Date = clsCommon.myCDate(dtpFromdate.Value)
                obj.To_Date = clsCommon.myCDate(dtptodate.Value)

                obj.Challan_No = clsCommon.myCstr(txtChallanNo.Text)
                obj.Challan_Date = clsCommon.myCDate(dtpChallanDate.Value)
                obj.TotalPayment = clsCommon.myCdbl(TxtTotalAmount.Value)
                obj.BSR_Code = clsCommon.myCstr(txtBSRCode.Text)
                obj.Payment_Code = clsCommon.myCstr(txtPaymentMode.Value)
                If clsCommon.CompairString(obj.Payment_Code, "Cheque") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Payment_Code, "DD") = CompairStringResult.Equal Then
                    obj.Cheque_No = clsCommon.myCstr(txtChequeNo.Text)
                    obj.Cheque_Date = clsCommon.myCDate(dtpChequeDate.Value)
                    If chkPDC.Checked Then
                        obj.PDC_Cheque = "Y"
                    End If
                    If chkCheckPrint.Checked Then
                        obj.CHECK_PRINT = 1
                    Else
                        obj.CHECK_PRINT = 0
                    End If
                Else
                    obj.Cheque_No = ""
                    obj.Cheque_Date = Nothing
                End If
              

                obj.arrTDSPaymentDetail = New List(Of ClsTDSPaymentDetail)


                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCBool(grow.Cells(colSlNo).Value) = True Then
                        objTr = New ClsTDSPaymentDetail()
                        objTr.Document_No = clsCommon.myCstr(obj.Document_No)
                        objTr.Document_type = clsCommon.myCstr(grow.Cells(colDocType).Value)
                        objTr.TDS_Deduction_Code = clsCommon.myCstr(grow.Cells(colNatureDeductionCode).Value)
                        objTr.TDS_Deduction_Name = clsCommon.myCstr(grow.Cells(colNatureDeductionDesc).Value)
                        objTr.Against_Document_No = clsCommon.myCstr(grow.Cells(colagainstDocNo).Value)
                        objTr.Against_Document_Date = clsCommon.myCDate(grow.Cells(colagainstDocDate).Value)
                        objTr.Vendor_Code = clsCommon.myCstr(grow.Cells(colVendorCode).Value)
                        objTr.Vendor_Name = clsCommon.myCstr(grow.Cells(colVendorDesc).Value)
                        objTr.PAN = clsCommon.myCstr(grow.Cells(colPAN).Value)
                        objTr.Location_Code = clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                        objTr.Location_Name = clsCommon.myCstr(grow.Cells(colLocationName).Value)
                        objTr.Deduct_Code = clsCommon.myCstr(grow.Cells(colDeductCode).Value)
                        objTr.Description = clsCommon.myCstr(grow.Cells(colDescription).Value)
                        objTr.GL_Account = clsCommon.myCstr(grow.Cells(colGLAccount).Value)
                        objTr.Taxable_Amount = clsCommon.myCdbl(grow.Cells(colTaxableAmt).Value)
                        objTr.Document_amount = clsCommon.myCdbl(grow.Cells(colDocAmt).Value)
                        objTr.Income_tax = clsCommon.myCdbl(grow.Cells(colIncometax).Value)
                        objTr.TotalTdsAmount = clsCommon.myCdbl(grow.Cells(ColTotalTDS).Value)
                        objTr.RateForTdsDeduction = clsCommon.myCdbl(grow.Cells(ColRateforTaxDeducted).Value)
                        obj.arrTDSPaymentDetail.Add(objTr)
                    End If
                   
                Next

                ''----------------------- nature of deduction table
                obj.arrTDSPaymentDetail_TDS_ND = New List(Of ClsTDSPaymentDetailTDS_ND)

                If txtNatureofDeduction.arrValueMember IsNot Nothing AndAlso txtNatureofDeduction.arrValueMember.Count > 0 Then
                    Dim list As New ArrayList
                    list = txtNatureofDeduction.arrValueMember
                    For i As Integer = 0 To list.Count - 1
                        objND_TR = New ClsTDSPaymentDetailTDS_ND()
                        objND_TR.Document_No = clsCommon.myCstr(obj.Document_No)
                        objND_TR.TDS_Deduction_Code = TryCast(list.Item(i), String)
                        obj.arrTDSPaymentDetail_TDS_ND.Add(objND_TR)
                    Next
                End If

                ''------------------------------


            If (ClsTDSPayment.SaveData(obj, isNewEntry)) Then
                If Not isFlag Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                        LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
            objTr = Nothing
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim dt As DataTable = Nothing
        Dim obj As ClsTDSPayment = Nothing
        Try
            obj = ClsTDSPayment.GetData(strCode, arrLoc, NavTyep)

            isInsideLoadData = True
            Reset()
            If obj IsNot Nothing Then
                isNewEntry = False
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                FndBankCode.Value = obj.Bank_Code
                lblBankName.Text = obj.BankName
                fndLocation.Value = obj.Location_Code
                lblLocationName.Text = obj.Location_Name
                fndSectionCode.Value = obj.TDS_Section_Code
                lblSectionName.Text = obj.TDS_Section_Name
                'Dim arr As ArrayList = New ArrayList()

                'txtNatureofDeduction.arrValueMember = obj.TDS_Deduction_Code
                txtChallanNo.Text = obj.Challan_No
                dtpChallanDate.Value = obj.Challan_Date
                dtpFromdate.Value = obj.From_Date
                dtptodate.Value = obj.To_Date
                txtBSRCode.Text = obj.BSR_Code
                TxtTotalAmount.Value = obj.TotalPayment

                lblPayment_No.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Payment_No  from TSPL_PAYMENT_HEADER where Against_TDS_payment_No='" & clsCommon.myCstr(txtDocNo.Value) & "'"))


                txtPaymentMode.Value = obj.Payment_Code
                HandleCheque()
                txtChequeNo.Text = obj.Cheque_No
                chkCheckPrint.Checked = IIf(obj.CHECK_PRINT = 1, True, False)
                If clsCommon.myLen(obj.Cheque_Date) > 0 Then
                    dtpChequeDate.Value = obj.Cheque_Date
                    If clsCommon.CompairString(obj.PDC_Cheque, "Y") = CompairStringResult.Equal Then
                        chkPDC.Checked = True
                    Else
                        chkPDC.Checked = False
                    End If
                Else
                    chkPDC.Checked = False
                    dtpChequeDate.Value = Nothing
                End If

                If obj.arrTDSPaymentDetail IsNot Nothing AndAlso obj.arrTDSPaymentDetail.Count > 0 Then
                    For Each objTr As ClsTDSPaymentDetail In obj.arrTDSPaymentDetail
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = True
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDocType).Value = objTr.Document_type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNatureDeductionCode).Value = objTr.TDS_Deduction_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNatureDeductionDesc).Value = objTr.TDS_Deduction_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colagainstDocNo).Value = objTr.Against_Document_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colagainstDocDate).Value = objTr.Against_Document_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVendorCode).Value = objTr.Vendor_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVendorDesc).Value = objTr.Vendor_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPAN).Value = objTr.PAN
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxableAmt).Value = objTr.Taxable_Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDocAmt).Value = objTr.Document_amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIncometax).Value = objTr.Income_tax
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColTotalTDS).Value = objTr.TotalTdsAmount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColRateforTaxDeducted).Value = objTr.RateForTdsDeduction
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.Location_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDeductCode).Value = objTr.Deduct_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDescription).Value = objTr.Description
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGLAccount).Value = objTr.GL_Account
                        gv1.Rows.AddNew()
                    Next
                    gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                Else
                    gv1.DataSource = Nothing
                End If

                If obj.arrTDSPaymentDetail_TDS_ND IsNot Nothing AndAlso obj.arrTDSPaymentDetail_TDS_ND.Count > 0 Then
                    Dim list As New ArrayList
                    For Each objTr As ClsTDSPaymentDetailTDS_ND In obj.arrTDSPaymentDetail_TDS_ND
                        list.Add(objTr.TDS_Deduction_Code)
                    Next
                    txtNatureofDeduction.arrValueMember = list
                End If

                txtDocNo.MyReadOnly = True
                btnsave.Text = "Update"

                If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    lblPending.Status = ERPTransactionStatus.Approved

                    btnupdate.Visible = True
                    btnupdate.Enabled = True
                Else
                    btnPost.Enabled = True
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    lblPending.Status = ERPTransactionStatus.Pending

                    btnupdate.Visible = False
                    btnupdate.Enabled = False
                End If
            Else
                Reset()

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            dt = Nothing
            obj = Nothing
        End Try
    End Sub
    Sub SaveDatainvoice()

        'If dtmain Is Nothing OrElse dtmain.Rows.Count <= 0 Then
        '    Exit Sub
        'End If
        Dim strcountno As String = ""
        Dim trans As SqlTransaction = Nothing
        Dim objTr As ClsInvoiceDetailBulkSale = Nothing
        Dim obj As ClsInvoiceBulkSale = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()


            Dim DocuAmount As Double = 0
            Dim intCounter As Integer = 0

            For Each dr As DataRow In dtmain.Rows
                Dim intCurrInvNo As Integer = clsCommon.myCdbl(dr("InvoiceSrNo"))

                If clsCommon.CompairString(strcountno, clsCommon.myCstr(dr("InvoiceSrNo"))) <> CompairStringResult.Equal Then
                    obj = New ClsInvoiceBulkSale()
                    ' obj.Document_Date = TxtInvoiceDate.Value  ''clsCommon.GETSERVERDATE(trans)
                    obj.Customer_Code = clsCommon.myCstr(dr("Customer"))
                    obj.Location_Code = clsCommon.myCstr(dr("Location"))
                    obj.InvoiceAgainst = "Against Dispatch"
                    obj.fromdate = clsCommon.GETSERVERDATE(trans)
                    obj.todate = clsCommon.GETSERVERDATE(trans)

                    obj.arrInvoiceDetailBulkSale = New List(Of ClsInvoiceDetailBulkSale)
                    objTr = New ClsInvoiceDetailBulkSale()
                    objTr.Dispatch_Code = clsCommon.myCstr(dr("DispatchCode"))
                    objTr.Dispatch_Date = clsCommon.myCstr(dr("DispatchDate"))
                    objTr.Item_Code = clsCommon.myCstr(dr("ItemCode"))
                    objTr.Unit_code = clsCommon.myCstr(dr("UnitCode"))
                    objTr.Tanker_Code = clsCommon.myCstr(dr("TankerCode"))
                    objTr.DispatchQty = clsCommon.myCdbl(dr("DispatchQty"))
                    objTr.DispatchFatPer = clsCommon.myCdbl(dr("DispatchFatPer"))
                    objTr.DispatchSNFPer = clsCommon.myCdbl(dr("DispatchSNFPer"))
                    objTr.DispatchRate = clsCommon.myCdbl(dr("DispatchRate"))
                    objTr.DispatchAmount = clsCommon.myCdbl(dr("DispatchAmount"))
                    objTr.InvoiceQty = clsCommon.myCdbl(dr("InvoiceQty"))
                    objTr.InvoiceFatPer = clsCommon.myCdbl(dr("InvoiceFatPer"))
                    objTr.InvoiceSNFPer = clsCommon.myCdbl(dr("InvoiceSNFPer"))
                    objTr.InvoiceFatKG = clsCommon.myCdbl(dr("InvoiceFatKg"))
                    objTr.InvoiceSNFKG = clsCommon.myCdbl(dr("InvoiceSNFKg"))
                    objTr.InvoiceRate = clsCommon.myCdbl(dr("InvoiceRate"))
                    objTr.InvoiceAmount = clsCommon.myCdbl(dr("InvoiceAmount"))
                    DocuAmount = 0
                    DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(objTr.InvoiceAmount, 2)
                    obj.arrInvoiceDetailBulkSale.Add(objTr)
                Else
                    objTr = New ClsInvoiceDetailBulkSale()
                    objTr.Dispatch_Code = clsCommon.myCstr(dr("DispatchCode"))
                    objTr.Dispatch_Date = clsCommon.myCstr(dr("DispatchDate"))
                    objTr.Item_Code = clsCommon.myCstr(dr("ItemCode"))
                    objTr.Unit_code = clsCommon.myCstr(dr("UnitCode"))
                    objTr.Tanker_Code = clsCommon.myCstr(dr("TankerCode"))
                    objTr.DispatchQty = clsCommon.myCdbl(dr("DispatchQty"))
                    objTr.DispatchFatPer = clsCommon.myCdbl(dr("DispatchFatPer"))
                    objTr.DispatchSNFPer = clsCommon.myCdbl(dr("DispatchSNFPer"))
                    objTr.DispatchRate = clsCommon.myCdbl(dr("DispatchRate"))
                    objTr.DispatchAmount = clsCommon.myCdbl(dr("DispatchAmount"))
                    objTr.InvoiceQty = clsCommon.myCdbl(dr("InvoiceQty"))
                    objTr.InvoiceFatPer = clsCommon.myCdbl(dr("InvoiceFatPer"))
                    objTr.InvoiceSNFPer = clsCommon.myCdbl(dr("InvoiceSNFPer"))
                    objTr.InvoiceRate = clsCommon.myCdbl(dr("InvoiceRate"))
                    objTr.InvoiceAmount = clsCommon.myCdbl(dr("InvoiceAmount"))

                    DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(objTr.InvoiceAmount, 2)
                    obj.arrInvoiceDetailBulkSale.Add(objTr)
                End If
                strcountno = intCurrInvNo
                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < dtmain.Rows.Count Then
                    intNextInvNo = clsCommon.myCdbl(dtmain.Rows(intCounter + 1)("InvoiceSrNo"))
                End If

                If Not (intCurrInvNo = intNextInvNo) Then
                    If Math.Round(clsCommon.myCdbl(DocuAmount), 0) > clsCommon.myCdbl(DocuAmount) Then
                        obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount), 0) - clsCommon.myCdbl(DocuAmount), 2)
                        obj.Total_Amt = Math.Round(clsCommon.myCdbl(DocuAmount), 0)
                    Else
                        obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount)) - clsCommon.myCdbl(DocuAmount), 2)
                        obj.Total_Amt = Math.Round(clsCommon.myCdbl(DocuAmount), 0)
                    End If
                    ClsInvoiceBulkSale.SaveData(obj, True, trans)
                    ClsInvoiceBulkSale.PostData("", "'" + obj.Location_Code + "'", obj.Document_No, trans)
                End If
                intCounter += 1
            Next
            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Invoice created successfully", Me.Text)
            Reset()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            strcountno = Nothing
            obj = Nothing
            objTr = Nothing
        End Try

    End Sub


    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        updatetotalAmount()
    End Sub
    Sub updatetotalAmount()
        Dim totalQty As Double = 0
        Dim totalAmt As Double = 0

        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myCBool(grow.Cells(colSlNo).Value) Then
                totalAmt = totalAmt + clsCommon.myCdbl(grow.Cells(ColTotalTDS).Value)
            End If
        Next
        TxtTotalAmount.Value = totalAmt
    End Sub

    Private Sub FndBankCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndBankCode._MYValidating
        Try


            Dim strWhrcls As String = ""
            Qry = clsERPFuncationality.glbankqueryNew(strWhrcls)
            FndBankCode.Value = clsCommon.ShowSelectForm("TDSBanFilter", Qry, "Code", strWhrcls, FndBankCode.Value, "Code", isButtonClicked)

            Dim BankAcc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(FndBankCode.Value) + "'"))
            If (BankAcc.Length >= 3) Then
                BankAcc = BankAcc.Substring(BankAcc.Length - 3, 3)
                fndLocation.Value = BankAcc
                If clsCommon.myLen(clsCommon.myCstr(fndLocation.Value)) > 0 Then
                    lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(fndLocation.Value) & "'"))
                Else
                    lblLocationName.Text = ""
                End If
                fndLocation.Enabled = False
            Else
                Throw New Exception("Bank Master's Bank Account should be have location segment Type")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndSectionCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSectionCode._MYValidating
        Qry = "select tds_group as Code ,description as [Description] from TSPL_TDS_SECTION_MASTER"
        fndSectionCode.Value = clsCommon.ShowSelectForm("TDSpYSection", Qry, "Code", "", fndSectionCode.Value, "Code", isButtonClicked)
        lblSectionName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_TDS_SECTION_MASTER where tds_group ='" + fndSectionCode.Value + "'"))
    End Sub

    Private Sub txtNatureofDeduction__My_Click(sender As Object, e As EventArgs) Handles txtNatureofDeduction._My_Click
        Qry = "select deduction_code As Code,description  as [Name] from TSPL_TDS_DEDUCTION_HEAD WHERE TDS_Section ='" & fndSectionCode.Value & "'"
        txtNatureofDeduction.arrValueMember = clsCommon.ShowMultipleSelectForm("NDTDSMulSel", Qry, "Code", "Name", txtNatureofDeduction.arrValueMember, txtNatureofDeduction.arrDispalyMember)
        loadBlankItemGrid()
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            'Dim qry As String = "select distinct(Segment_code) as Code ,Description,TSPL_LOCATION_MASTER.Loc_Short_Name as [Short Name]  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
            'Dim WhrCls As String = "Seg_No = '7' AND GIT='N'"
            'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            '    WhrCls += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            'End If

            'fndLocation.Value = clsCommon.ShowSelectForm("TDSLoc", qry, "Code", WhrCls, fndLocation.Value, "Code", isButtonClicked)
            'If clsCommon.myLen(clsCommon.myCstr(fndLocation.Value)) > 0 Then
            '    lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(fndLocation.Value) & "'"))
            'Else
            '    lblLocationName.Text = ""
            'End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            isFlag = True
            If (myMessages.postConfirm()) Then
                SaveData()
                If (ClsTDSPayment.PostData(MyBase.Form_ID, arrLoc, txtDocNo.Value)) Then

                    common.clsCommon.MyMessageBoxShow(Me, "Successfully posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsTDSPayment.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_TDS_PAYMENT_HEADER where Document_No='" + txtDocNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = " Select TSPL_TDS_PAYMENT_HEADER.Document_No,CONVERT(VARCHAR,TSPL_TDS_PAYMENT_HEADER.Document_Date,103) AS Document_Date,TSPL_TDS_PAYMENT_HEADER.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as Bank_Name,TSPL_TDS_PAYMENT_HEADER.TDS_Section_Code,TSPL_TDS_SECTION_MASTER.Description as TDS_Section_Name,TSPL_TDS_PAYMENT_HEADER.TDS_Deduction_Code,CONVERT(VARCHAR,TSPL_TDS_PAYMENT_HEADER.From_Date,103) AS From_Date,CONVERT(VARCHAR,TSPL_TDS_PAYMENT_HEADER.To_Date,103) AS To_Date,TSPL_TDS_PAYMENT_HEADER.BSR_Code,TSPL_TDS_PAYMENT_HEADER.Challan_No," & _
        " CONVERT(VARCHAR,TSPL_TDS_PAYMENT_HEADER.Challan_Date,103) AS Challan_Date,TSPL_TDS_PAYMENT_HEADER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,case when TSPL_TDS_PAYMENT_HEADER.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_TDS_PAYMENT_HEADER Left Outer join TSPL_BANK_MASTER on TSPL_TDS_PAYMENT_HEADER.Bank_Code=TSPL_BANK_MASTER.BANK_CODE left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_PAYMENT_HEADER.TDS_Section_Code=TSPL_TDS_SECTION_MASTER.TDS_Group " & _
        " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_TDS_PAYMENT_HEADER.Location_Code"
        txtDocNo.Value = clsCommon.ShowSelectForm("TDSPayment", qry, "Document_No", "", txtDocNo.Value, "", isButtonClicked, "TSPL_TDS_PAYMENT_HEADER.Document_Date")
        LoadData(txtDocNo.Value, NavigatorType.Current)
        qry = Nothing
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Try
            If clsCommon.MyMessageBoxShow("Do you want to update BSR Code/Challan No/Challan Date after Posting.", "Update After Posting", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Qry = "update TSPL_TDS_PAYMENT_HEADER set TSPL_TDS_PAYMENT_HEADER.BSR_Code='" & clsCommon.myCstr(txtBSRCode.Text) & "',TSPL_TDS_PAYMENT_HEADER.Challan_No='" & clsCommon.myCstr(txtChallanNo.Text) & "',TSPL_TDS_PAYMENT_HEADER.Challan_Date='" & clsCommon.GetPrintDate(dtpChallanDate.Value, "dd/MMM/yyyy hh:mm tt") & "' where TSPL_TDS_PAYMENT_HEADER.Document_No='" & clsCommon.myCstr(txtDocNo.Value) & "' "
                clsDBFuncationality.ExecuteNonQuery(Qry)
                common.clsCommon.MyMessageBoxShow(Me, "Data updated Successfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
      
    End Sub
End Class
