Imports common
Imports System
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net
Imports Telerik.WinControls
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient
Public Class FrmPromptMsgRelatedToPendency
#Region "Variables"
    Dim qry As String = Nothing
    Dim StrQuery As String = Nothing
    Dim dtAuthen As DataTable
    Dim arrUser As New ArrayList()
    Dim arrSelectedUser As New ArrayList()
    Dim dt As DataTable = Nothing
    Dim IsSelected As Boolean = False
    Public IsPostBack As Boolean = False
    Private isNewEntry As Boolean = True
    Public ChkOpenFromDate As Date? = Nothing
    Public ChkOpenToDate As Date? = Nothing
#End Region

    Private Sub FrmPromptMsgRelatedToPendency_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblMsgs.Text = ""
            btnSubmit.Text = "Submit"

            Dim promptPendingDocument As Integer = 0
            promptPendingDocument = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PromptMsgForPendingDocIntervel, clsFixedParameterType.PromptMsgForPendingDocIntervel, Nothing))

            arrUser = FrmUserMaster.GetSubbordinateUsers(objCommonVar.CurrentUserCode)
            ShowData()
            If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then
                dt = clsDBFuncationality.GetDataTable(qry)

                If dt.Rows.Count <= 0 Then
                    gv1.Text = "No data found to display "
                    chkDeclare.Checked = False
                    chkDeclare.Enabled = False
                    btnSubmit.Text = "Close"

                Else
                    btnSubmit.Text = "Submit"
                    gv1.DataSource = dt
                    gv1Format()

                End If

            End If


            chkDeclare.Checked = True
        Catch ex As Exception
            lblMsgs.Text = ex.Message.ToString()
        End Try

    End Sub
    Sub gv1Format()
        Try
            For Each col As GridViewColumn In gv1.Columns

                If col.Name = "IsChecked" Then
                    col.Width = 50
                    col.HeaderText = "Select"
                    col.ReadOnly = False
                Else
                    col.Width = 150
                    col.ReadOnly = True
                End If
            Next

        Catch ex As Exception
            lblMsgs.Text = ex.Message.ToString()
        End Try
    End Sub
    Private Sub LoadData_BySqlReader(ByVal mySQLQuery As String)
        Dim connetionString As String
        Dim sqlCnn As SqlConnection
        Dim sqlCmd As SqlCommand
        Dim sql As String

        If mySQLQuery.Contains("cast(0 as bit) as IsChecked ,") Then
            mySQLQuery = mySQLQuery.Replace("cast(0 as bit) as IsChecked ,", "cast(0 as bit) as IsChecked , ROW_NUMBER() OVER (ORDER BY Module) AS SNO,")
        End If
        sql = mySQLQuery

        connetionString = objCommonVar.ConnString
        sqlCnn = New SqlConnection(connetionString)
        Try
            sqlCnn.Open()
            sqlCmd = New SqlCommand(sql, sqlCnn)
            sqlCmd.CommandTimeout = 600
            Dim sqlReader As SqlDataReader = sqlCmd.ExecuteReader()
            Dim dt As DataTable = New DataTable("ListOfPendingDocs")
            dt.Columns.Add("IsChecked", GetType(Boolean))
            dt.Columns.Add("SNo", GetType(String))
            dt.Columns.Add("Module", GetType(String))
            dt.Columns.Add("Screen", GetType(String))
            dt.Columns.Add("Doc Code", GetType(String))
            dt.Columns.Add("Doc Date", GetType(Date))
            dt.Columns.Add("Status", GetType(String))
            dt.Columns.Add("Program Code", GetType(String))
            While sqlReader.Read()
                Dim dr As DataRow = dt.NewRow()
                dr("IsChecked") = sqlReader("IsChecked")
                dr("SNo") = sqlReader("SNO")
                dr("Module") = sqlReader("Module")
                dr("Screen") = sqlReader("Screen")
                dr("Doc Code") = sqlReader("Doc Code")
                dr("Doc Date") = sqlReader("Doc Date")
                dr("Status") = sqlReader("Status")
                dr("Program Code") = sqlReader("Program Code")
                dt.Rows.Add(dr)
            End While
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
            End If
            For Each col As GridViewColumn In gv1.Columns
                col.BestFit()
            Next
            sqlReader.Close()
            sqlCmd.Dispose()
            sqlCnn.Close()
        Catch ex As Exception
            lblMsgs.Text = ex.Message
        End Try
    End Sub
    Sub ShowData()
        Try
            arrSelectedUser = arrUser
            qry = ""
            gv1.DataSource = Nothing

            Dim obj As clsPendingDocsPopupHead = New clsPendingDocsPopupHead()
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "admin") = CompairStringResult.Equal Then
                LoadData_BySqlReader(obj.GeneratePendingDocumentsQuery(ChkOpenFromDate, ChkOpenToDate, arrUser, False))
            Else
                LoadData_BySqlReader(obj.GeneratePendingDocumentsQuery(ChkOpenFromDate, ChkOpenToDate, arrUser, True))
            End If

            If gv1.Rows.Count <= 0 Then
                'gv1.DataSource = Nothing
                'chkDeclare.Checked = False
                'chkDeclare.Enabled = False
                btnSubmit.Text = "Close"
                Me.Close()
            Else
                btnSubmit.Text = "Submit"
                'gv1.DataSource = dt
                gv1Format()
            End If

        Catch ex As Exception
            lblMsgs.Text = ex.Message.ToString()
        End Try
    End Sub
    Private Sub btnShow_Click(sender As Object, e As EventArgs)
        ShowData()
    End Sub
    Private Function UpdateTable(ByVal strTableName As String) As Boolean
        Dim isSavedSuccessfully As Boolean = False
        Try
            Dim qryUpdateFromDate As String = "update " + strTableName + " set Open_From_Date = convert(date,'" + ChkOpenFromDate + "',103) , Open_To_Date = convert(date,'" + ChkOpenToDate + "',103) where User_Code ='" + objCommonVar.CurrentUserCode + "'"
            isSavedSuccessfully = clsDBFuncationality.ExecuteNonQuery(qryUpdateFromDate)
        Catch ex As Exception
            lblMsgs.Text = ex.Message.ToString()
        End Try
        Return isSavedSuccessfully
    End Function
    Public Sub SaveData()
        Dim obj As clsPendingDocsPopupHead = Nothing
        Dim oHead As clsPendingDocsPopupHead = Nothing
        Dim objDtl As clsPendingDocsPopupDetail = Nothing
        Dim pCount As Integer = 0
        Try
            oHead = New clsPendingDocsPopupHead()
            oHead.Arr = New List(Of clsPendingDocsPopupDetail)
            '----------------------------------------------------------------------------

            For Each row As GridViewRowInfo In gv1.Rows
                If row.Cells("IsChecked").Value = True Then
                    objDtl = New clsPendingDocsPopupDetail()
                    oHead.Declare_Type = 1
                    pCount += 1
                    objDtl.ProgramCode = clsCommon.myCstr(row.Cells("Program Code").Value)
                    objDtl.Doc_Code = clsCommon.myCstr(row.Cells("Doc Code").Value)

                    If clsCommon.myCstr(row.Cells("Status").Value) = "Y" Then
                        objDtl.LAST_STATUS = "Posted"
                    Else
                        objDtl.LAST_STATUS = "Pending"
                    End If


                    If objDtl IsNot Nothing AndAlso clsCommon.myLen(objDtl) > 0 Then
                        oHead.Arr.Add(objDtl)
                    End If

                End If
            Next
            oHead.Pending_Count = pCount
            obj = New clsPendingDocsPopupHead()
            If obj IsNot Nothing Then
                If (obj.SaveData(oHead, True)) Then
                    If UpdateTable("tspl_user_master") Then
                        lblMsgs.Text = "Document(s) Submitted Successfully."
                        btnSubmit.Text = "Close"
                    End If
                    'Me.Close()
                End If
            Else
                lblMsgs.Text = "Nothing is selected to Submit."
            End If
        Catch ex As Exception
            lblMsgs.Text = ex.Message.ToString()
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            If btnSubmit.Text = "Close" Then
                Me.Close()
            ElseIf chkDeclare.Checked Then
                Dim unchecks As Integer = 0
                For Each r As GridViewRowInfo In gv1.Rows
                    If r.Cells("IsChecked").Value = False Then
                        unchecks += 1
                    End If
                Next
                If unchecks = gv1.Rows.Count Then
                    lblMsgs.Text = "You have not Selected any document to submit !"
                    Exit Sub
                Else
                    SaveData()
                End If

            ElseIf chkDeclare.Checked = False Then
                lblMsgs.Text = "You have to select declare checkbox"
                chkDeclare.Select()
                Exit Sub
            End If
        Catch ex As Exception
            lblMsgs.Text = ex.Message
        End Try
    End Sub
    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If e.Column.Name <> "IsChecked" Then
                If clsCommon.CompairString(e.Column.Name, "Doc Code") = CompairStringResult.Equal Then
                    Me.TopMost = False
                    'Me.WindowState = FormWindowState.Minimized
                    If clsCommon.CompairString(btnSubmit.Text, "Submit") = CompairStringResult.Equal Then
                        DrillDown_ShowDialog(clsCommon.myCstr(gv1.CurrentRow.Cells("Screen").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("Doc Code").Value))

                        If arrSelectedUser IsNot Nothing AndAlso arrSelectedUser.Count > 0 Then
                        Else
                            arrSelectedUser = FrmUserMaster.GetSubbordinateUsers(objCommonVar.CurrentUserCode)
                        End If
                        Dim tempQry As String = Nothing
                        If clsCommon.CompairString(objCommonVar.CurrentUserCode, "admin") = CompairStringResult.Equal Then
                            tempQry = clsPendingDocsPopupHead.GetPostStatus(ChkOpenFromDate, ChkOpenToDate, arrSelectedUser, clsCommon.myCstr(gv1.CurrentRow.Cells("Doc Code").Value), False)
                        Else
                            tempQry = clsPendingDocsPopupHead.GetPostStatus(ChkOpenFromDate, ChkOpenToDate, arrSelectedUser, clsCommon.myCstr(gv1.CurrentRow.Cells("Doc Code").Value), True)
                        End If


                        Dim tmpDt As DataTable = clsDBFuncationality.GetDataTable(tempQry)
                        If tmpDt IsNot Nothing AndAlso tmpDt.Rows.Count > 0 Then
                            For Each r As DataRow In tmpDt.Rows
                                gv1.CurrentRow.Cells("Status").Value = r("Status")
                            Next
                        End If

                        gv1.CurrentRow.Cells("IsChecked").Value = True
                    End If
                End If
            End If
            Me.TopMost = True
            'Me.WindowState = FormWindowState.Normal
        Catch ex As Exception
            lblMsgs.Text = ex.Message
        End Try
    End Sub

    Private Sub chkSelectAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSelectAll.ToggleStateChanged
        Try
            If args.ToggleState = ToggleState.On Then
                For Each row As GridViewRowInfo In gv1.Rows
                    row.Cells("IsChecked").Value = True
                Next
            Else
                For Each row As GridViewRowInfo In gv1.Rows
                    row.Cells("IsChecked").Value = False
                Next
            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Sub DrillDown_ShowDialog(ByVal strScreenCode As String, ByVal strDocumentCode As String)

        If clsCommon.myLen(strScreenCode) > 0 AndAlso clsCommon.myLen(strDocumentCode) > 0 Then
            Select Case strScreenCode
                Case "AP Invoice Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, strDocumentCode)
                Case "Payment Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, strDocumentCode)
                Case "AR Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, strDocumentCode)
                Case "Receipt Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, strDocumentCode)
                Case "Journal Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strDocumentCode)
                Case "VCGL"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnVCGLEntry, strDocumentCode)
                Case "Booking"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBookingProductSale, strDocumentCode)
                Case "Sale order Product Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesOrderProductSale, strDocumentCode)
                Case "Delivery order Product Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesOrderProductSale, strDocumentCode)
                Case "Dispatch Product Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strDocumentCode)
                Case "Booking Fresh Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBookingEntry, strDocumentCode)
                Case "Invoice Fresh Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmInvoiceFreshSale, strDocumentCode)
                Case "Purchase Requisition"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseRequistion, strDocumentCode)
                Case "Purchase Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseOrder, strDocumentCode)
                Case "Gate Received Note"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGRN, strDocumentCode)
                Case "Material Received Note"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnMRN, strDocumentCode)
                Case "Store Received Note"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strDocumentCode)
                Case "Purchase Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, strDocumentCode)
                Case "Purchase Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, strDocumentCode)
                Case "RGP/NRGP"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strDocumentCode)
                Case "Issue/Return Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, strDocumentCode)
                Case "Bulk Dispatch"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, strDocumentCode)
                Case "Milk SRN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, strDocumentCode)
                Case "Milk Purchase Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkPurchaseInvoice, strDocumentCode)
                Case "Milk Transfer In"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, strDocumentCode)
                Case "Store Adjustment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strDocumentCode)
                Case "Transfer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strDocumentCode)
                Case "Contra Vouchers"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.bankTransfer, strDocumentCode)
                Case "CSA Transfer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, strDocumentCode)
                Case "Sale Patti"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, strDocumentCode)
                Case "Production Planning"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionPlanningDairy, strDocumentCode)
                Case "Batch Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBatchOrderDairy, strDocumentCode)
                Case "Production Issue Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, strDocumentCode)
                Case "Production Standardization"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, strDocumentCode)
                Case "Stage Process"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, strDocumentCode)
                Case "Production Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, strDocumentCode)
                Case "Milk Sample"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSample, strDocumentCode)
                'Case "Milk Truck Sheet"
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.MilkTruckSheet, strDocumentCode)
                Case "Tanker Dispatch"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, strDocumentCode)
                'Case "Tanker Location Change"
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDispatchTransfer, strDocumentCode)
                'Case "VSP Asset Issue"
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPAssetIssue, strDocumentCode)
                Case "MCC Material Sale"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterial, strDocumentCode)
                Case "MCC Material Sale Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturn, strDocumentCode)
                'Case "VSP Item Issue"
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, strDocumentCode)
                Case "Export Sale Quotation"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesQuotation, strDocumentCode)
                Case "Export Sales Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesOrder, strDocumentCode)
                Case "Export Proforma Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXPorformaInvoice, strDocumentCode)
                Case "Export Commercial Invoice & Packing List"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXCommercialInvoice, strDocumentCode)
                Case "Export Sales Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesInvoice, strDocumentCode)
                Case "Export Sale Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesReturn, strDocumentCode)
                Case "Merchant Purchase Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmPurchaseOrderMT, strDocumentCode)
                Case "Merchant Sales Order"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesOrderMT, strDocumentCode)
                Case "Merchant Proforma Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProformaInvoiceMT, strDocumentCode)
                Case "LC Request"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmLCRequest, strDocumentCode)
                Case "LC Creation"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmLCCreation, strDocumentCode)
                Case "Document Acceptance"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDocumentAcceptance, strDocumentCode)
                Case "Fixed Deposit"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmFixedDeposit, strDocumentCode)
                Case "Merchant Commercial Invoice & Packing List"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCommercialInvoiceMT, strDocumentCode)
                Case "Merchant Sales Invoice"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, strDocumentCode)
                Case "Merchant Sale Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesReturn, strDocumentCode)
                Case "Milk RGP"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkJobWork, strDocumentCode)
                Case "Milk Gate Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkGateEntry, strDocumentCode)
                Case "Milk Weighment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkWeighment, strDocumentCode)
                Case "Milk Quality Check"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmJobMilkQualityCheck, strDocumentCode)
                Case "Milk Unloading"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkUnloading, strDocumentCode)
                Case "Milk SRN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmJobMilkSRN, strDocumentCode)
                Case "Complaint Detail Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmComplaintDetailEntry, strDocumentCode)
                Case "Service Dispatch"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAssetDistatch, strDocumentCode)
                Case "Cart Maintenance Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCartMaintenanceEntry, strDocumentCode)
                Case "Acquisition Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FAAcquisitionEntry, strDocumentCode)
                Case "Disposal Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FADisposalEntry, strDocumentCode)
                Case "Issue Items to Assemble Assset"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmIssueItemsToAsset, strDocumentCode)
                Case "Assset Work Expanses"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FAAssetWork, strDocumentCode)
                Case "Employee Salary"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEmpSalary, strDocumentCode)
                Case "Leave Application"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLeaveApplication, strDocumentCode)
                Case "Leave Adjustment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLeaveAdjustment, strDocumentCode)
                Case "Allowance Details"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAllowanceDetails, strDocumentCode)
                Case "Deduction Details"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDeductionDetails, strDocumentCode)
                Case "Employee Reimbursement"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmReimbursementDetails, strDocumentCode)
                Case "Generate Bonus"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmGenerateBonus, strDocumentCode)
                Case "Loan Generation"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLoanGeneration, strDocumentCode)
                Case "Loan Adjustment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLoanAdjustment, strDocumentCode)
                Case "Daily Attendance"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDailyAttendance, strDocumentCode)
                Case "Hourly Attendance"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmHourlyAttendance, strDocumentCode)
                Case "Monthly Attendance"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMonthlyAttendance, strDocumentCode)
                Case "Employee Adjustment Voucher"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAdjustmentVoucher, strDocumentCode)
                Case "Salary Generation"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalaryGeneration, strDocumentCode)
                Case "LTA Claim"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmLTAClaim, strDocumentCode)
                Case "Employee Mediclaim Entry"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMediclaimEntry, strDocumentCode)
                Case "Full And Final Settlement"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmFullAndFinalSettlement, strDocumentCode)
                Case "Employee Shift Change"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEmployeeShiftChange, strDocumentCode)
                Case "Employee Transfer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmEmployeeTransfer, strDocumentCode)
                Case "Employee Increment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmEmpIncrement, strDocumentCode)
                Case "Farmer Payment Adjustment"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmFarmerPaymentAdjustment, strDocumentCode)
                Case "MCC Material Sale Farmer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialFarmer, strDocumentCode)
                Case "MCC Material Sale Return Farmer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturnFarmer, strDocumentCode)
                Case "Reverse Transaction"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.reverseTransaction, strDocumentCode)
            End Select

            ''======================check status from table,that is posted or not

        End If
    End Sub
    Private Sub FrmPromptMsgRelatedToPendency_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If clsCommon.CompairString(btnSubmit.Text, "Close") = CompairStringResult.Equal Then
            e.Cancel = False
        Else
            e.Cancel = True
            Me.WindowState = FormWindowState.Normal
        End If

    End Sub
End Class
