'===================BM00000007864,BM00000007337,BM00000007744===================
Imports common
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Management

Public Class FrmPaymentProcessFarmer
    Inherits FrmMainTranScreen

#Region "Variables"
    Public isEmpOnAmtOnly As Boolean = False
    Public Const colSlno As String = "colSlno"
    Public Const colPurchaseInvoiceNo As String = "colPurchaseInvoiceNo"
    Public Const colAPInvoiceNo As String = "colAPInvoiceNo"
    Public Const colARInvoiceNo As String = "colARInvoiceNo"
    Public Const colPurchaseInvoiceDate As String = "colPurchaseInvoiceDate"
    Public Const colAPInvoiceDate As String = "colAPInvoiceDate"
    Public Const colARInvoiceDate As String = "colARInvoiceDate"
    Public Const colInvAmt As String = "colInvAmt"
    Public Const colItemCode As String = "colItemCode"
    Public Const colItemDesc As String = "colItemDesc"
    Public Const colLocCode As String = "colLocCode"
    Public Const colLocDesc As String = "colLocDesc"
    Public Const colTotalEmp As String = "colTotalEmp"
    Public Const colDeductionCode As String = "colDeductionCode"
    Public Const colDeductionDesc As String = "colDeductionDesc"
    Public Const colVLCCode As String = "colVLCCode"
    Public Const colVLCName As String = "colVLCName"
    Public Const colVLCUploaderCode As String = "colVLCUploaderCode"
    Public Const colVendorCode As String = "colVendorCode"
    Public Const colVendorDesc As String = "colVendorDesc"
    Public Const colCustomerCode As String = "colCustomerCode"
    Public Const colCustomerName As String = "colCustomerName"
    Public Const colPayeeJointName As String = "colPayeeJointName"
    Public Const colPayeeJointAcNo As String = "colPayeeJointAcNo"
    Public Const colPayeeJointIFSC As String = "colPayeeJointIFSC"
    Public Const colPayeeJointBankCode As String = "colPayeeJointBankCode"
    Public Const colPayeeJointBankDesc As String = "colPayeeJointBankDesc"
    Public Const colPayeeJointBranchCode As String = "colPayeeJointBranchCode"
    Public Const colPayeeJointBranchDesc As String = "colPayeeJointBranchDesc"
    Public Const colMilkQty As String = "colMilkQty"
    Public Const colItemAmt As String = "colItemAmt"
    Public Const colEmpAmt As String = "colEmpAmt"
    Public Const colInvAndEmpAmt As String = "colInvAndEmpAmt"
    Public Const colIncenAmt As String = "colIncenAmt"
    Public Const colIncenEmpAmt As String = "colIncenEmpAmt"
    Public Const colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt As String = "colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt"
    Public Const colVSPOwnSystemAmt As String = "colVSPOwnSystemAmt"
    Public Const colHeadLoadAmt As String = "colHeadLoadAmt"
    Public Const colInvDeduc As String = "colInvDeduc"
    Public Const colReduceDeduc As String = "colReduceDeduc"
    Public Const colPaybleAmt As String = "colPaybleAmt"
    Public Const colShipmentNo As String = "colShipmentNo"
    Public Const colShipmentDate As String = "colShipmentDate"
    Public Const colSaleInvNo As String = "colSaleInvNo"
    Public Const colSaleInvDate As String = "colSaleInvDate"
    Public Const colVspItemIssueNo As String = "colVspItemIssueNo"
    Public Const colVspItemIssueReturnNo As String = "colVspItemIssueReturnNo"
    Public Const colVspItemIssueDate As String = "colVspItemIssueDate"
    Public Const colSelect As String = "colSelect"
    Public Const colReturnDocNo As String = "colReturnDocNo"
    Public Const colReturnDocDate As String = "colReturnDocDate"
    Public Const colReturnDocType As String = "colReturnDocType"

    Public Const colVSPAmount As String = "colVSPAmount"
    Public Const colMPAmount As String = "colMPAmount"
    Public Const colMPVSPDiffAmount As String = "colMPVSPDiffAmount"
    Public Const colServiceChargeAmt As String = "colServiceChargeAmt"

    Public Const colAPSelect As String = "colAPSelect"
    Public Const colAPSNo As String = "colAPSNo"
    Public Const colAPVendorCode As String = "colAPVendorCode"
    Public Const colAPVendorName As String = "colAPVendorName"
    Public Const colAPPaymentCode As String = "colAPPaymentCode"
    Public Const colAPPaymentDate As String = "colAPPaymentDate"
    Public Const colAPPaymentAmt As String = "colAPPaymentAmt"
    Public Const colAPPaymentAmtBalance As String = "colAPPaymentAmtBalance"

    Dim colChkBox As GridViewCheckBoxColumn = Nothing
    Dim colTextBox As GridViewTextBoxColumn = Nothing
    Dim colDate As GridViewDateTimeColumn = Nothing
    Dim colDecimal As GridViewDecimalColumn = Nothing
    Dim arrStrMccSaleItemCode As List(Of String) = Nothing
    Dim arrStrMccSaleItemDesc As List(Of String) = Nothing
    Dim arrStrIssueItemCode As List(Of String) = Nothing
    Dim arrStrIssueItemDesc As List(Of String) = Nothing
    Dim arrStrDedCode As List(Of String) = Nothing
    Dim arrStrDedDesc As List(Of String) = Nothing
    Dim arrAgainstMilkPurchaseInvoiceNo As String = ""
    Dim arrVendorInvoiceNo As String = ""
    Dim strVendorCode As String = ""
    Dim strCustomerCode As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isLoad As Boolean = True
    Dim isNewEntry As Boolean = False
    Dim frm As FrmPaymentDetail = New FrmPaymentDetail()
    Public Const colMccSaleTotalAmount As String = "colMccSaleTotalAmount"
    Public Const colItemIssueTotalAmount As String = "colItemIssueTotalAmount"
    Public Const colItemIssueReturnTotalAmount As String = "colItemIssueReturnTotalAmount"
    Public Const colDeductionTotalAmount As String = "colDeductionTotalAmount"
    Public Const colTotalCreditNoteAmount As String = "colTotalCreditNoteAmount"
    Public Const colAdvanceAmount As String = "colAdvanceTotal"
    Public Const colAdvanceKnockOffAmount As String = "colAdvanceKnockOffAmount"

    Public colBankCode As String = "colBankCode"
    Public colBankDesc As String = "colBankDesc"
    Public colPayMode As String = "colPayMode"
    Public colChequeNo As String = "colChequeNo"
    Public colChequeDate As String = "colChequeDate"
    Public isCellValueChanged = False
    Public PayProcessDocNo As String = ""
    Public colActualVSPCode As String = "colActualVSPCode"
    Public colActualVSPName As String = "colActualVSPName"

    '============Added By Rohit,========================
    Public Const colMccSaleReturnTotalAmount As String = "colMccSaleReturnTotalAmount"

    Private isConsiderAdvancePayment As Boolean = False
    Private PayableAmountZeroForMCCSale As Boolean = False
    Dim isPickPendingMilkSRNinNextPaymentCycle As Boolean = False
#End Region

    Private Sub FrmProvisionEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isConsiderAdvancePayment = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsiderAdvancePayment, clsFixedParameterType.ConsiderAdvancePayment, Nothing)) = 1, True, False)
        PayableAmountZeroForMCCSale = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PayableAmountZeroForMCCSale, clsFixedParameterType.PayableAmountZeroForMCCSale, Nothing)) = 1, True, False)
        RadPageView1.Pages("RadPageViewPage7").Item.Visibility = IIf(isConsiderAdvancePayment, Telerik.WinControls.ElementVisibility.Visible, Telerik.WinControls.ElementVisibility.Collapsed)
        RadPageView1.SelectedPage = RadPageViewPage1
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        'ButtonToolTip.SetToolTip(btnProcess, "Press Alt+P to Post the Transaction")
        isPickPendingMilkSRNinNextPaymentCycle = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickPendingMilkSRNinNextPaymentCycle, clsFixedParameterCode.PickPendingMilkSRNinNextPaymentCycle, Nothing)) = 1
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Public Sub LoadDataFromOtherForm()
        Try
            If clsCommon.myLen(PayProcessDocNo) > 0 Then
                LoadData(PayProcessDocNo, NavigatorType.Current)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Sub LoadBlankGridInvoice()
        gvInvoice.Rows.Clear()
        gvInvoice.Columns.Clear()

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.ReadOnly = True
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvInvoice.MasterTemplate.Columns.Add(colChkBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Purchase Invoice No"
        colTextBox.Name = colPurchaseInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Purchase Invoice Date"
        colTextBox.Name = colPurchaseInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = False
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Name"
        colTextBox.Name = colVLCName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VSP Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VSP Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Main VSP Code"
        colTextBox.Name = colActualVSPCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Main VSP Name"
        colTextBox.Name = colActualVSPName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Name"
        colTextBox.Name = colPayeeJointName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Bank Code"
        colTextBox.Name = colPayeeJointBankCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Bank Name"
        colTextBox.Name = colPayeeJointBankDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Branch Code"
        colTextBox.Name = colPayeeJointBranchCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = False
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Branch Name"
        colTextBox.Name = colPayeeJointBranchDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Account No"
        colTextBox.Name = colPayeeJointAcNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint IFSC Code"
        colTextBox.Name = colPayeeJointIFSC
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Qty"
        colDecimal.Name = colMilkQty
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colInvAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "EMP"
        colDecimal.Name = colEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Total"
        colDecimal.Name = colInvAndEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Incentive"
        colDecimal.Name = colIncenAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Incentive EMP"
        colDecimal.Name = colIncenEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Service Charge Amt"
        colDecimal.Name = colServiceChargeAmt
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Total Amount"
        colDecimal.Name = colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "VSP Own System"
        colDecimal.Name = colVSPOwnSystemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Head Load"
        colDecimal.Name = colHeadLoadAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Deduction"
        colDecimal.Name = colInvDeduc
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Reduce Deduction"
        colDecimal.Name = colReduceDeduc
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = False
        gvInvoice.MasterTemplate.Columns.Add(colDecimal)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Bank code"
        colTextBox.Name = colBankCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Bank Desc"
        colTextBox.Name = colBankDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payment Mode"
        colTextBox.Name = colPayMode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Cheque No"
        colTextBox.Name = colChequeNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = True
        gvInvoice.MasterTemplate.Columns.Add(colTextBox)

        gvInvoice.AllowAddNewRow = False
        gvInvoice.AllowDeleteRow = False
        gvInvoice.ShowGroupPanel = False
        gvInvoice.AllowColumnReorder = True
        gvInvoice.AllowRowReorder = False
        gvInvoice.EnableSorting = True
        gvInvoice.EnableFiltering = True
        gvInvoice.TableElement.TableHeaderHeight = 40
        gvInvoice.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub getVendors()
        strVendorCode = ""
        If gvInvoice.Rows.Count > 0 Then
            For i As Integer = 0 To gvInvoice.Rows.Count - 1
                If gvInvoice.Rows(i).Cells(colSelect).Value = True Then
                    strVendorCode = strVendorCode & "'" & gvInvoice.Rows(i).Cells(colVendorCode).Value & "',"
                End If
            Next
            If clsCommon.myLen(strVendorCode) > 0 Then
                strVendorCode = Microsoft.VisualBasic.Left(strVendorCode, Microsoft.VisualBasic.Len(strVendorCode) - 1)
            End If
        End If
    End Sub

    Function getTotalDeductionReduceDeduSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvDeduction IsNot Nothing AndAlso gvDeduction.Rows.Count > 0 Then
                For i As Integer = 0 To gvDeduction.Rows.Count - 1
                    If gvDeduction.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvDeduction.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colReduceDeduc).Value) > 0 Then
                        rValue = rValue + clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colReduceDeduc).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalDeductionSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvDeduction IsNot Nothing AndAlso gvDeduction.Rows.Count > 0 Then
                For i As Integer = 0 To gvDeduction.Rows.Count - 1
                    If gvDeduction.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvDeduction.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then

                        rValue = rValue + clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalCreditNoteSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvCreditNote IsNot Nothing AndAlso gvCreditNote.Rows.Count > 0 Then
                For i As Integer = 0 To gvCreditNote.Rows.Count - 1
                    If gvCreditNote.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvCreditNote.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then

                        rValue = rValue + clsCommon.myCdbl(gvCreditNote.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalAdvancePayment(ByVal strVendorCode As String) As Double
        Dim rValue As Double = 0
        Try
            If isConsiderAdvancePayment Then
                If clsCommon.myLen(strVendorCode) > 0 AndAlso gvAdvancePayment IsNot Nothing AndAlso gvAdvancePayment.Rows.Count > 0 Then
                    For i As Integer = 0 To gvAdvancePayment.Rows.Count - 1
                        If gvAdvancePayment.Rows(i).Cells(colAPSelect).Value = True AndAlso clsCommon.CompairString(gvAdvancePayment.Rows(i).Cells(colAPVendorCode).Value, strVendorCode) = CompairStringResult.Equal Then
                            rValue = rValue + clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPPaymentAmtBalance).Value)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Math.Round(rValue, 2, MidpointRounding.ToEven)
    End Function

    Function getTotalItemIssueSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvItemIssue IsNot Nothing AndAlso gvItemIssue.Rows.Count > 0 Then
                For i As Integer = 0 To gvItemIssue.Rows.Count - 1
                    If gvItemIssue.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvItemIssue.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then

                        rValue = rValue + clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalItemIssueReturnSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvItemIssueReturn IsNot Nothing AndAlso gvItemIssueReturn.Rows.Count > 0 Then
                For i As Integer = 0 To gvItemIssueReturn.Rows.Count - 1
                    If gvItemIssueReturn.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvItemIssueReturn.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then
                        rValue = rValue + clsCommon.myCdbl(gvItemIssueReturn.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalItemIssueReduceDeduSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvItemIssue IsNot Nothing AndAlso gvItemIssue.Rows.Count > 0 Then
                For i As Integer = 0 To gvItemIssue.Rows.Count - 1
                    If gvItemIssue.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvItemIssue.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colReduceDeduc).Value) > 0 Then
                        rValue = rValue + clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colReduceDeduc).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    'Function getTotalItemIssueReturnReduceDeduSum(ByVal vsp As String) As Double
    '    Dim rValue As Double = 0
    '    Try
    '        If clsCommon.myLen(vsp) > 0 AndAlso gvItemIssueReturn IsNot Nothing AndAlso gvItemIssueReturn.Rows.Count > 0 Then
    '            For i As Integer = 0 To gvItemIssueReturn.Rows.Count - 1
    '                If gvItemIssueReturn.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvItemIssueReturn.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gvItemIssueReturn.Rows(i).Cells(colReduceDeduc).Value) > 0 Then
    '                    rValue = rValue + clsCommon.myCdbl(gvItemIssueReturn.Rows(i).Cells(colReduceDeduc).Value)
    '                End If
    '            Next
    '        End If

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return rValue
    'End Function
    Function getTotalMccSaleSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvMccSale IsNot Nothing AndAlso gvMccSale.Rows.Count > 0 Then
                For i As Integer = 0 To gvMccSale.Rows.Count - 1
                    If gvMccSale.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvMccSale.Rows(i).Cells(colCustomerCode).Value, vsp) = CompairStringResult.Equal Then

                        rValue = rValue + clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalMccSaleReturnSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso GvMccSaleReturn IsNot Nothing AndAlso GvMccSaleReturn.Rows.Count > 0 Then
                For i As Integer = 0 To GvMccSaleReturn.Rows.Count - 1
                    If GvMccSaleReturn.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(GvMccSaleReturn.Rows(i).Cells(colCustomerCode).Value, vsp) = CompairStringResult.Equal Then

                        rValue = rValue + clsCommon.myCdbl(GvMccSaleReturn.Rows(i).Cells(colItemAmt).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Private Function GetDataTable(ByVal dtg As RadGridView, ByVal columnName As String, ByVal ConditionValue As String) As DataTable
        Dim dt As New DataTable()
        ' add the columns to the datatable            
        If dtg IsNot Nothing Then
            For i As Integer = 0 To dtg.Columns.Count - 1
                dt.Columns.Add(dtg.Columns(i).HeaderText)
            Next
        End If
        '  add each of the data rows to the table
        For j As Integer = 0 To dtg.Rows.Count - 1
            If clsCommon.CompairString(dtg.Rows(j).Cells(columnName).Value, ConditionValue) = CompairStringResult.Equal Then
                Dim dr As DataRow
                dr = dt.NewRow()
                For k As Integer = 0 To dtg.Columns.Count - 1
                    dr(k) = dtg.Rows(j).Cells(k).Value
                Next
                dt.Rows.Add(dr)
            End If
        Next
        Return dt
    End Function

    Function getMccSaleList(ByVal vsp As String) As DataTable
        Dim rValue As DataTable = Nothing
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvMccSale IsNot Nothing AndAlso gvMccSale.Rows.Count > 0 Then
                rValue = GetDataTable(gvMccSale, colCustomerCode, vsp)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getMccSaleReturnList(ByVal vsp As String) As DataTable
        Dim rValue As DataTable = Nothing
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso GvMccSaleReturn IsNot Nothing AndAlso GvMccSaleReturn.Rows.Count > 0 Then
                rValue = GetDataTable(GvMccSaleReturn, colCustomerCode, vsp)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getCreditNoteList(ByVal vsp As String) As DataTable
        Dim rValue As DataTable = Nothing
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvCreditNote IsNot Nothing AndAlso gvCreditNote.Rows.Count > 0 Then
                rValue = GetDataTable(gvCreditNote, colVendorCode, vsp)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getItemIssueList(ByVal vsp As String) As DataTable
        Dim rValue As DataTable = Nothing
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvItemIssue IsNot Nothing AndAlso gvItemIssue.Rows.Count > 0 Then
                rValue = GetDataTable(gvItemIssue, colVendorCode, vsp)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getDeductionList(ByVal vsp As String) As DataTable
        Dim rValue As DataTable = Nothing
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvDeduction IsNot Nothing AndAlso gvDeduction.Rows.Count > 0 Then
                rValue = GetDataTable(gvDeduction, colVendorCode, vsp)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function getTotalMccSaleReduceDeduSum(ByVal vsp As String) As Double
        Dim rValue As Double = 0
        Try
            If clsCommon.myLen(vsp) > 0 AndAlso gvMccSale IsNot Nothing AndAlso gvMccSale.Rows.Count > 0 Then
                For i As Integer = 0 To gvMccSale.Rows.Count - 1
                    If gvMccSale.Rows(i).Cells(colSelect).Value = True AndAlso clsCommon.CompairString(gvMccSale.Rows(i).Cells(colCustomerCode).Value, vsp) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colReduceDeduc).Value) > 0 Then

                        rValue = rValue + clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colReduceDeduc).Value)
                    End If
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Sub LoadBlankGridGV()
        gv.Rows.Clear()
        gv.Columns.Clear()

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(colChkBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 80
        colTextBox.ReadOnly = False
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Purchase Invoice No"
        colTextBox.Name = colPurchaseInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Purchase Invoice Date"
        colTextBox.Name = colPurchaseInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Code Uploader"
        colTextBox.Name = colVLCUploaderCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC Name"
        colTextBox.Name = colVLCName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VSP Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VSP Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Main VSP Code"
        colTextBox.Name = colActualVSPCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Main VSP Name"
        colTextBox.Name = colActualVSPName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Name"
        colTextBox.Name = colPayeeJointName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Bank Code"
        colTextBox.Name = colPayeeJointBankCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Bank Name"
        colTextBox.Name = colPayeeJointBankDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Branch Code"
        colTextBox.Name = colPayeeJointBranchCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = False
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Branch Name"
        colTextBox.Name = colPayeeJointBranchDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint Account No"
        colTextBox.Name = colPayeeJointAcNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee/Joint IFSC Code"
        colTextBox.Name = colPayeeJointIFSC
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        Dim colTextBox1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        colTextBox1.FormatString = ""
        colTextBox1.HeaderText = "Bank code"
        colTextBox1.Name = colBankCode
        colTextBox1.Width = 200
        colTextBox1.ReadOnly = False

        'colTextBox.IsVisible = False
        gv.MasterTemplate.Columns.Add(colTextBox1)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Bank Desc"
        colTextBox.Name = colBankDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        'colTextBox.IsVisible = False
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payment Mode"
        colTextBox.Name = colPayMode
        colTextBox.Width = 200
        colTextBox.ReadOnly = False
        'colTextBox.IsVisible = False
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Cheque No"
        colTextBox.Name = colChequeNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = False
        colTextBox.IsVisible = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colDate = New GridViewDateTimeColumn
        colDate.FormatString = ""
        colDate.HeaderText = "Cheque Date"
        colDate.Name = colChequeDate

        colDate.CustomFormat = "dd/MM/yyyy"
        colDate.FormatString = "{0:dd/MM/yyyy}"

        colDate.Width = 100
        colDate.ReadOnly = False
        colDate.IsVisible = True
        gv.MasterTemplate.Columns.Add(colDate)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Qty"
        colDecimal.Name = colMilkQty
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Amount(VSP)"
        colDecimal.Name = colVSPAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Amount(Farmer)"
        colDecimal.Name = colMPAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Diff. Amount(Farmer-VSP)"
        colDecimal.Name = colMPVSPDiffAmount
        colDecimal.Width = 100
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Milk Amount"
        colDecimal.Name = colInvAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Incentive"
        colDecimal.Name = colIncenAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "EMP"
        colDecimal.Name = colEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = False
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Incentive EMP"
        colDecimal.Name = colIncenEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = False
        gv.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Total EMP"
        colDecimal.Name = colTotalEmp
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Total"
        colDecimal.Name = colInvAndEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = False
        gv.MasterTemplate.Columns.Add(colDecimal)



        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Total Invoice Amount"
        colDecimal.Name = colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "VSP Own System"
        colDecimal.Name = colVSPOwnSystemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Head Load"
        colDecimal.Name = colHeadLoadAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Inv Deduction"
        colDecimal.Name = colInvDeduc
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Reduce Deduction"
        colDecimal.Name = colReduceDeduc
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Service Charge Amt"
        colDecimal.Name = colServiceChargeAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "MCC Sale Amount"
        colDecimal.Name = colMccSaleTotalAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "MCC Sale Return Amount"
        colDecimal.Name = colMccSaleReturnTotalAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Item Issue Amount"
        colDecimal.Name = colItemIssueTotalAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Item Issue Return Amount"
        colDecimal.Name = colItemIssueReturnTotalAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Deduction Amount"
        colDecimal.Name = colDeductionTotalAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Advance Payment"
        colDecimal.Name = colAdvanceAmount
        colDecimal.Width = 150
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = isConsiderAdvancePayment
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Advance Knock Off"
        colDecimal.Name = colAdvanceKnockOffAmount
        colDecimal.Width = 150
        colDecimal.ReadOnly = True
        colDecimal.IsVisible = isConsiderAdvancePayment
        gv.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Credit Note Amount"
        colDecimal.Name = colTotalCreditNoteAmount
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Payble Amount"
        ''richa agarwal
        colDecimal.FormatString = "{0:n2}"
        ''----------------
        colDecimal.Name = colPaybleAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        gv.Columns(colBankCode).HeaderImage = Global.ERP.My.Resources.Resources.search4
        gv.Columns(colBankCode).TextImageRelation = TextImageRelation.TextBeforeImage

        gv.Columns(colPayMode).HeaderImage = Global.ERP.My.Resources.Resources.search4
        gv.Columns(colPayMode).TextImageRelation = TextImageRelation.TextBeforeImage

        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = True
        gv.EnableFiltering = True
        gv.TableElement.TableHeaderHeight = 40
        gv.BestFitColumns(BestFitColumnMode.AllCells)
        gv.MasterTemplate.AllowEditRow = True

    End Sub

    Function isMultipleDocumentForSameVendor() As Boolean
        For i As Integer = 0 To gvInvoice.Rows.Count - 1
            For j As Integer = 0 To gvInvoice.Rows.Count - 1
                If i <> j AndAlso clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, gvInvoice.Rows(j).Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                    Return True
                End If
            Next
        Next
        Return False
    End Function

    Function getAllDeductionSum(ByVal RowNo) As Double
        Dim rValue As Double = 0
        If arrStrDedCode IsNot Nothing AndAlso arrStrDedCode.Count > 0 AndAlso RowNo <= gv.Rows.Count - 1 Then
            For i As Integer = 0 To arrStrDedCode.Count - 1
                rValue = rValue + clsCommon.myCdbl(gv.Rows(RowNo).Cells(arrStrDedCode.Item(i) & "D").Value)
            Next
        End If
        Return rValue
    End Function

    Function getAllItemIssueSum(ByVal RowNo) As Double
        Dim rValue As Double = 0
        If arrStrIssueItemCode IsNot Nothing AndAlso arrStrIssueItemCode.Count > 0 AndAlso RowNo <= gv.Rows.Count - 1 Then
            For i As Integer = 0 To arrStrIssueItemCode.Count - 1
                rValue = rValue + clsCommon.myCdbl(gv.Rows(RowNo).Cells(arrStrIssueItemCode.Item(i) & "I").Value)
            Next
        End If
        Return rValue
    End Function

    Function getAllMccSaleItemSum(ByVal RowNo) As Double
        Dim rValue As Double = 0
        If arrStrMccSaleItemCode IsNot Nothing AndAlso arrStrMccSaleItemCode.Count > 0 AndAlso RowNo <= gv.Rows.Count - 1 Then
            For i As Integer = 0 To arrStrMccSaleItemCode.Count - 1
                rValue = rValue + clsCommon.myCdbl(gv.Rows(RowNo).Cells(arrStrMccSaleItemCode.Item(i) & "S").Value)
            Next
        End If
        Return rValue
    End Function

    Function getMultipleDocumentForSameVendor() As String
        Dim rValue As String = ""
        For i As Integer = 0 To gvInvoice.Rows.Count - 1
            For j As Integer = 0 To gvInvoice.Rows.Count - 1
                If i <> j AndAlso clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, gvInvoice.Rows(j).Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                    rValue = rValue & "Vendor " & gvInvoice.Rows(i).Cells(colVendorCode).Value & " ( " & gvInvoice.Rows(i).Cells(colVendorDesc).Value & " ) " & " Invoice No. " & gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value & " Invoice Date " & gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value & Environment.NewLine
                End If
            Next
        Next
        Return rValue
    End Function

    Sub LoadInvoiceGridData()
        Try
            LoadBlankGridInvoice()
            Dim qry As String = "    select   MAX( xxx.[AP Invoice Doc No]) as [AP Invoice Doc No] ,max(xxx.[Ap Invoice Doc Date]) as [Ap Invoice Doc Date] ,xxx.[Milk Purchase Invoice Doc No] as [Milk Purchase Invoice Doc No],max(xxx.[Milk Purchase Invoice Doc Date]) as [Milk Purchase Invoice Doc Date],max(VLC_Code) as VLC_Code,max(xxx.VLC_Name) as VLC_Name,max(xxx.Vendor_Code)  as Vendor_Code,max(xxx.Vendor_Name) as Vendor_Name,max(xxx.[Payee/Joint Name]) as [Payee/Joint Name],max(xxx.[Bank Code]) as [Bank Code],max(xxx.[Bank Name]) as  [Bank Name] , max(xxx.[Branch Code]) as [Branch Code],max(xxx.[Branch Name]) as [Branch Name],max(xxx.[IFSC Code]) as  [IFSC Code],SUM(xxx.qty) as [Total Qty]   ,max(xxx.TOTAL_basic_amount) as TOTAL_basic_amount,max(xxx.TOTAL_AMOUNT ) as TOTAL_AMOUNT,MAX(xxx.TOTAL_PaymentCOMMISSION) as TOTAL_PaymentCOMMISSION,MAX(xxx.Incentive_Head ) as Incentive_Head, MAX(xxx .IncentiveEMP_Head ) as  IncentiveEMP_Head,sum(Service_Charge_Amount) as Service_Charge_Amount,max(xxx.TOTAL_AMOUNT_Acc  ) as TOTAL_AMOUNT_Acc,max(xxx.MCC_CODE) as  MCC_CODE,max(xxx.AccountNo) as AccountNo " + Environment.NewLine +
                " from ( 	   select  TSPL_VENDOR_INVOICE_HEAD.Document_No as [AP Invoice Doc No], TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date as [Ap Invoice Doc Date], TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as [Milk Purchase Invoice Doc No],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE as [Milk Purchase Invoice Doc Date],coalesce(TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader,mp_vlc.vlc_code_vlc_uploader) as VLC_Code ,coalesce(TSPL_VLC_MASTER_HEAD.VLC_Name ,mp_vlc.vlc_name) as vlc_name,coalesce(TSPL_VENDOR_MASTER.Vendor_Code,mp_v.vendor_Code) as vendor_Code,coalesce(TSPL_VENDOR_MASTER.Vendor_Name,mp_v.Vendor_name) as Vendor_name , coalesce(TSPL_VENDOR_MASTER.VSP_Payee_Name,Mp_V.VSP_Payee_Name) as [Payee/Joint Name], case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then ''  else ''   end as [Branch Code],case when isnull (coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(SelfBank .Bank_Name,selfBank_mp.bank_name)  else coalesce(jointBank .Bank_Name,jointBank_MP .Bank_Code)   end as [Bank Name],case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(SelfBank .Bank_Code,SelfBank_MP .Bank_Code)   else coalesce(jointBank .Bank_Code,jointBank_Mp .Bank_Code)    end as [Bank Code],case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(TSPL_VENDOR_MASTER .Branch_Name,MP_V .Branch_Name )   else coalesce(TSPL_VENDOR_MASTER .Joint_Branch_Name,Mp_V.Joint_Branch_Name)   end as [Branch Name],case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(TSPL_VENDOR_MASTER .IFSC_Code,mp_V.IFSC_Code)   else coalesce(TSPL_VENDOR_MASTER .Joint_IFSC_Code,mp_v.Joint_IFSC_Code)   end as [IFSC Code],case when isnull(coalesce(TSPL_VENDOR_MASTER.vsp_payment,Mp_V.vsp_payment),'')='Self' then coalesce(TSPL_VENDOR_MASTER .Account_No,mp_V .Account_No)    else coalesce(TSPL_VENDOR_MASTER.Joint_Account_No,mp_V.Joint_Account_No)    end as [AccountNo],TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty,TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_basic_amount,TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_AMOUNT  , TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION, TSPL_MILK_PURCHASE_INVOICE_HEAD.Incentive_Head,TSPL_MILK_PURCHASE_INVOICE_HEAD.IncentiveEMP_Head, TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_AMOUNT_Acc ,  TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Service_Charge_Amount     " + Environment.NewLine +
                " from TSPL_VENDOR_INVOICE_HEAD  " + Environment.NewLine +
                " left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE= TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No " + Environment.NewLine +
                " left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE  " + Environment.NewLine +
                " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE" + Environment.NewLine +
                " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE " + Environment.NewLine +
                "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  " + Environment.NewLine +
                " left outer join TSPL_Vendor_Bank_MASTER as jointBank on jointBank.Bank_Code =TSPL_VENDOR_MASTER .Joint_Bank_Code  " + Environment.NewLine +
                " left outer join TSPL_Vendor_Bank_MASTER as SelfBank on SelfBank .Bank_Code =TSPL_VENDOR_MASTER.Bank_Name " _
                & " left outer join TSPL_MP_MASTER mp on mp.MP_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_Code left outer join TSPL_VLC_MASTER_HEAD mp_vlc on mp_vlc.Vlc_Code=mp.VLC_Code Left join tspl_vendor_master Mp_V on mp_V.Vendor_Code=mp.MP_Code  left outer join TSPL_Vendor_Bank_MASTER as jointBank_Mp on jointBank_Mp.Bank_Code =Mp_V .Joint_Bank_Code   left outer join TSPL_Vendor_Bank_MASTER as SelfBank_Mp on SelfBank .Bank_Code =Mp_V.Bank_Name   " _
                & "  where TSPL_VENDOR_INVOICE_HEAD.Balance_Amt>0 and TSPL_VENDOR_INVOICE_HEAD.document_type='I' and TSPL_VENDOR_INVOICE_HEAD.Invoice_Type='AP' and TSPL_VENDOR_INVOICE_HEAD.REFDocType='MI-PI' and ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_MillkPurchaseInvoice_No,'')<>''    ) xxx where  1=1 "
            Dim whrCls As String = ""
            If clsCommon.myLen(fndLoc.Value) > 0 Then
                whrCls = " And MCC_Code in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "')"
            End If

            qry = qry & whrCls

            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                fndLoc.Focus()
                Throw New Exception("Please select Location")
            End If

            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                txtVSP.Focus()
                Throw New Exception("Please select at lease one VSP ")
            End If
            whrCls = " And vendor_code in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"

            qry = qry & whrCls

            whrCls = " and [Milk Purchase Invoice Doc Date] between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'   group by xxx.[Milk Purchase Invoice Doc No]"
            qry = qry & whrCls

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvInvoice.Rows.AddNew()
                    gvInvoice.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvInvoice.Rows(i).Cells(colSelect).Value = True
                    gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value = dt.Rows(i)("AP Invoice Doc No")
                    gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Ap Invoice Doc Date"), "dd/MMM/yyyy")
                    gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value = dt.Rows(i)("Milk Purchase Invoice Doc No")
                    gvInvoice.Rows(i).Cells(colPurchaseInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Milk Purchase Invoice Doc Date"), "dd/MMM/yyyy")
                    gvInvoice.Rows(i).Cells(colVLCCode).Value = dt.Rows(i)("VLC_Code")
                    gvInvoice.Rows(i).Cells(colVLCName).Value = dt.Rows(i)("VLC_Name")
                    gvInvoice.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("Vendor_Code")
                    gvInvoice.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("Vendor_Name")
                    gvInvoice.Rows(i).Cells(colPayeeJointName).Value = dt.Rows(i)("Payee/Joint Name")
                    gvInvoice.Rows(i).Cells(colPayeeJointBankCode).Value = dt.Rows(i)("Bank Code")
                    gvInvoice.Rows(i).Cells(colPayeeJointBankDesc).Value = dt.Rows(i)("Bank Name")
                    gvInvoice.Rows(i).Cells(colPayeeJointBranchCode).Value = dt.Rows(i)("Branch Code")
                    gvInvoice.Rows(i).Cells(colPayeeJointBranchDesc).Value = dt.Rows(i)("Branch Name")
                    gvInvoice.Rows(i).Cells(colPayeeJointIFSC).Value = dt.Rows(i)("IFSC Code")
                    gvInvoice.Rows(i).Cells(colPayeeJointAcNo).Value = dt.Rows(i)("AccountNo")
                    gvInvoice.Rows(i).Cells(colMilkQty).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Total Qty")))
                    gvInvoice.Rows(i).Cells(colInvAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("TOTAL_basic_amount")))
                    gvInvoice.Rows(i).Cells(colEmpAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("TOTAL_PaymentCOMMISSION")))
                    gvInvoice.Rows(i).Cells(colInvAndEmpAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("TOTAL_AMOUNT"))) '+ clsCommon.myCdbl(dt.Rows(i)("TOTAL_PaymentCOMMISSION"))
                    gvInvoice.Rows(i).Cells(colIncenAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Incentive_Head")))
                    gvInvoice.Rows(i).Cells(colIncenEmpAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("IncentiveEMP_Head")))
                    gvInvoice.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("TOTAL_AMOUNT_Acc")))
                    gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Value = clsCommon.myFormat(getVspOwnSystemAmount(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value))
                    gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Tag = getVspOwnSystemINV(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                    gvInvoice.Rows(i).Cells(colHeadLoadAmt).Value = clsCommon.myFormat(clsCommon.myFormat(getHeadLoadAmount(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value)))
                    gvInvoice.Rows(i).Cells(colHeadLoadAmt).Tag = getHeadLoadINV(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                    gvInvoice.Rows(i).Cells(colInvDeduc).Value = clsCommon.myFormat(getDeductionAmount(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value))
                    gvInvoice.Rows(i).Cells(colInvDeduc).Tag = getDeductionINV(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                    gvInvoice.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myFormat(0)
                    gvInvoice.Rows(i).Cells(colBankCode).Value = ""
                    gvInvoice.Rows(i).Cells(colBankDesc).Value = ""
                    gvInvoice.Rows(i).Cells(colPayMode).Value = ""
                    gvInvoice.Rows(i).Cells(colChequeNo).Value = ""
                    gvInvoice.Rows(i).Cells(colServiceChargeAmt).Value = clsCommon.myCdbl(dt.Rows(i)("Service_Charge_Amount"))
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Function getVspOwnSystemAmount(ByVal strMilkPurchaseInvoiceNo As String) As Double
        Dim rValue As Double = 0
        Dim qry As String = " select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD where Document_Type='C' and RefDocType='Milk_OW' and Against_MillkPurchaseInvoice_No='" & strMilkPurchaseInvoiceNo & "' and Balance_Amt<>0 "
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Return rValue
    End Function

    Function getVspOwnSystemINV(ByVal strMilkPurchaseInvoiceNo As String) As String
        Dim rValue As String = ""
        Dim qry As String = " select Document_No from TSPL_VENDOR_INVOICE_HEAD where Document_Type='C' and RefDocType='Milk_OW' and Against_MillkPurchaseInvoice_No='" & strMilkPurchaseInvoiceNo & "' and Balance_Amt<>0 "
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return rValue
    End Function

    Function getHeadLoadINV(ByVal strMilkPurchaseInvoiceNo As String) As String
        Dim rValue As String = ""
        Dim qry As String = " select Document_No from TSPL_VENDOR_INVOICE_HEAD where Document_Type='C' and RefDocType='Milk_HE' and Against_MillkPurchaseInvoice_No='" & strMilkPurchaseInvoiceNo & "' and Balance_Amt<>0 "
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return rValue
    End Function

    Function getDeductionINV(ByVal strMilkPurchaseInvoiceNo As String) As String
        Dim rValue As String = ""
        Dim qry As String = " select Document_No from TSPL_VENDOR_INVOICE_HEAD where Document_Type='D' and RefDocType='Milk_DE' and Against_MillkPurchaseInvoice_No='" & strMilkPurchaseInvoiceNo & "' and Balance_Amt<>0 "
        rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return rValue
    End Function

    Function getHeadLoadAmount(ByVal strMilkPurchaseInvoiceNo As String) As Double
        Dim rValue As Double = 0
        Dim qry As String = " select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD where Document_Type='C' and RefDocType='Milk_HE' and Against_MillkPurchaseInvoice_No='" & strMilkPurchaseInvoiceNo & "' and Balance_Amt<>0 "
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Return rValue
    End Function

    Function getDeductionAmount(ByVal strMilkPurchaseInvoiceNo As String) As Double
        Dim rValue As Double = 0
        Dim qry As String = " select Balance_Amt from TSPL_VENDOR_INVOICE_HEAD where Document_Type='D' and RefDocType='Milk_DE' and Against_MillkPurchaseInvoice_No='" & strMilkPurchaseInvoiceNo & "' and Balance_Amt<>0 "
        rValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Return rValue
    End Function

    Sub LoadBlankGridMccSale()
        gvMccSale.Rows.Clear()
        gvMccSale.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvMccSale.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Shipment Doc No"
        colTextBox.Name = colShipmentNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Shipment Doc Date"
        colTextBox.Name = colShipmentDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Sale Doc No"
        colTextBox.Name = colSaleInvNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Sale Doc Date"
        colTextBox.Name = colSaleInvDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AR Invoice No"
        colTextBox.Name = colARInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AR Invoice Date"
        colTextBox.Name = colARInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colCustomerCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colCustomerName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colTextBox)



        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Code"
        'colTextBox.Name = colItemCode
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvMccSale.MasterTemplate.Columns.Add(colTextBox)



        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Desc"
        'colTextBox.Name = colItemDesc
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvMccSale.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvMccSale.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Reduce" & Environment.NewLine & "Deduction"
        colDecimal.Name = colReduceDeduc
        colDecimal.Width = 100
        colDecimal.ReadOnly = False
        colDecimal.WrapText = True
        gvMccSale.MasterTemplate.Columns.Add(colDecimal)


        gvMccSale.AllowAddNewRow = False
        gvMccSale.AllowDeleteRow = False
        gvMccSale.ShowGroupPanel = False
        gvMccSale.AllowColumnReorder = True
        gvMccSale.AllowRowReorder = False
        gvMccSale.EnableSorting = True
        gvMccSale.EnableFiltering = True
        gvMccSale.TableElement.TableHeaderHeight = 40
        gvMccSale.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub LoadBlankGridMccSaleReturn()
        GvMccSaleReturn.Rows.Clear()
        GvMccSaleReturn.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        GvMccSaleReturn.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Return Doc No"
        colTextBox.Name = colReturnDocNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Return Doc Date"
        colTextBox.Name = colReturnDocDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Return Doc Type"
        colTextBox.Name = colReturnDocType
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AR Invoice No"
        colTextBox.Name = colARInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AR Invoice Date"
        colTextBox.Name = colARInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Shipment Doc No"
        colTextBox.Name = colShipmentNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Shipment Doc Date"
        colTextBox.Name = colShipmentDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Sale Doc No"
        colTextBox.Name = colSaleInvNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Sale Doc Date"
        colTextBox.Name = colSaleInvDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colCustomerCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colCustomerName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colTextBox)



        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Code"
        'colTextBox.Name = colItemCode
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvMccsaleReturn.MasterTemplate.Columns.Add(colTextBox)



        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Desc"
        'colTextBox.Name = colItemDesc
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvMccsaleReturn.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        GvMccSaleReturn.MasterTemplate.Columns.Add(colDecimal)

        'colDecimal = New GridViewDecimalColumn()
        'colDecimal.FormatString = ""
        'colDecimal.HeaderText = "Reduce" & Environment.NewLine & "Deduction"
        'colDecimal.Name = colReduceDeduc
        'colDecimal.Width = 100
        'colDecimal.ReadOnly = False
        'colDecimal.WrapText = True
        'GvMccSaleReturn.MasterTemplate.Columns.Add(colDecimal)


        GvMccSaleReturn.AllowAddNewRow = False
        GvMccSaleReturn.AllowDeleteRow = False
        GvMccSaleReturn.ShowGroupPanel = False
        GvMccSaleReturn.AllowColumnReorder = True
        GvMccSaleReturn.AllowRowReorder = False
        GvMccSaleReturn.EnableSorting = True
        GvMccSaleReturn.EnableFiltering = True
        GvMccSaleReturn.TableElement.TableHeaderHeight = 40
        GvMccSaleReturn.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub LoadBlankGridDeduction()
        gvDeduction.Rows.Clear()
        gvDeduction.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvDeduction.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Deduction Code"
        colTextBox.Name = colDeductionCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Deduction Desc"
        colTextBox.Name = colDeductionDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvDeduction.MasterTemplate.Columns.Add(colDecimal)


        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Reduce Deduction"
        colDecimal.Name = colReduceDeduc
        colDecimal.Width = 200
        colDecimal.ReadOnly = False
        gvDeduction.MasterTemplate.Columns.Add(colDecimal)

        gvDeduction.AllowAddNewRow = False
        gvDeduction.AllowDeleteRow = False
        gvDeduction.ShowGroupPanel = False
        gvDeduction.AllowColumnReorder = True
        gvDeduction.AllowRowReorder = False
        gvDeduction.EnableSorting = True
        gvDeduction.EnableFiltering = True
        gvDeduction.TableElement.TableHeaderHeight = 40
        gvDeduction.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub LoadBlankGridAdvancePayment()
        gvAdvancePayment.Rows.Clear()
        gvAdvancePayment.Columns.Clear()

        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = " "
        colChkBox.Name = colAPSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 30
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvAdvancePayment.MasterTemplate.Columns.Add(colChkBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SNo"
        colTextBox.Name = colAPSNo
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colAPVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colAPVendorName
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payment No"
        colTextBox.Name = colAPPaymentCode
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payment Date"
        colTextBox.Name = colAPPaymentDate
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colAPPaymentAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Balance Amount"
        colDecimal.Name = colAPPaymentAmtBalance
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvAdvancePayment.MasterTemplate.Columns.Add(colDecimal)

        gvAdvancePayment.AllowAddNewRow = False
        gvAdvancePayment.AllowDeleteRow = False
        gvAdvancePayment.ShowGroupPanel = False
        gvAdvancePayment.AllowColumnReorder = True
        gvAdvancePayment.AllowRowReorder = False
        gvAdvancePayment.EnableSorting = True
        gvAdvancePayment.EnableFiltering = True
        gvAdvancePayment.TableElement.TableHeaderHeight = 40
        gvAdvancePayment.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub LoadBlankGridCreditNote()
        gvCreditNote.Rows.Clear()
        gvCreditNote.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select "
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvCreditNote.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvCreditNote.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvCreditNote.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvCreditNote.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvCreditNote.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvCreditNote.MasterTemplate.Columns.Add(colTextBox)





        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvCreditNote.MasterTemplate.Columns.Add(colDecimal)




        gvCreditNote.AllowAddNewRow = False
        gvCreditNote.AllowDeleteRow = False
        gvCreditNote.ShowGroupPanel = False
        gvCreditNote.AllowColumnReorder = True
        gvCreditNote.AllowRowReorder = False
        gvCreditNote.EnableSorting = True
        gvCreditNote.EnableFiltering = True
        gvCreditNote.TableElement.TableHeaderHeight = 40
        gvCreditNote.BestFitColumns(BestFitColumnMode.AllCells)
    End Sub

    Sub LoadDeductionGridData()
        '' Updation by pankaj jha in query  Against Ticket No : BM00000008003
        'gvDeduction.Rows.Clear()
        LoadBlankGridDeduction()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "   select TSPL_VENDOR_INVOICE_DETAIL.Document_No,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code, " & _
                " TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,TSPL_VENDOR_INVOICE_DETAIL.Total_Amount " & _
                " from TSPL_VENDOR_INVOICE_DETAIL left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No =TSPL_VENDOR_INVOICE_DETAIL.Document_No " & _
                " where  Document_Type='D' " & _
                " and ((TSPL_VENDOR_INVOICE_HEAD.isDeduction='1' " & _
                " and ISNULL(TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,'')<>'') or  len(coalesce(TSPL_VENDOR_INVOICE_HEAD.Against_VCGL,''))>0)  " & _
                " and TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 "
            Dim whrCls As String = ""


            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  in ( " & strVendorCode & ")  and  coalesce(Posting_Date,'')<>'' "
            End If

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                whrCls += "and 2=(case when TSPL_VENDOR_INVOICE_HEAD.isDeduction='1' and TSPL_VENDOR_INVOICE_DETAIL.DeductionCode='SECURITY DED' then" + Environment.NewLine + _
                  " case when convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >= '" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") + "' then 2 " + Environment.NewLine + _
                  " else 0 end else 2 end )"
            End If

            qry = qry & whrCls & "  and TSPL_VENDOR_INVOICE_HEAD.Loc_Code ='" & fndLoc.Value & "' and " & IIf(chkSkipPrevDeduction.Checked, " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ")
            'in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code



            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvDeduction.Rows.AddNew()
                    gvDeduction.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvDeduction.Rows(i).Cells(colSelect).Value = True
                    gvDeduction.Rows(i).Cells(colAPInvoiceNo).Value = dt.Rows(i)("Document_No")
                    gvDeduction.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Invoice_Entry_Date"), "dd/MMM/yyyy")
                    gvDeduction.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("Vendor_Code")
                    gvDeduction.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("Vendor_Name")
                    gvDeduction.Rows(i).Cells(colDeductionCode).Value = dt.Rows(i)("DeductionCode")
                    gvDeduction.Rows(i).Cells(colDeductionDesc).Value = dt.Rows(i)("Deduction_Desc")
                    gvDeduction.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Total_Amount")))
                    gvDeduction.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myFormat(0)
                Next
            End If
        End If
    End Sub

    Sub LoadCreditNoteGridData()
        'gvDeduction.Rows.Clear()
        LoadBlankGridCreditNote()
        If clsCommon.myLen(strVendorCode) > 0 Then
            '' Update By pankaj jha For picking up amount from Head in place of details 
            Dim qry As String = "   select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_HEAD.document_total   as Total_Amount   from TSPL_VENDOR_INVOICE_head   where   TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 and coalesce(refDocType,'') not in ('Milk_HE','Milk_OW','V_I_Issue_Return')  "
            Dim whrCls As String = ""


            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  in ( " & strVendorCode & ")   and  coalesce(Posting_Date,'')<>''"
            End If
            qry = qry & whrCls & "  and TSPL_VENDOR_INVOICE_HEAD.Loc_Code ='" & fndLoc.Value & "' and " & IIf(chkSkipPrevCreditNote.Checked, " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' ")
            'in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvCreditNote.Rows.AddNew()
                    gvCreditNote.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvCreditNote.Rows(i).Cells(colSelect).Value = True
                    gvCreditNote.Rows(i).Cells(colAPInvoiceNo).Value = dt.Rows(i)("Document_No")
                    gvCreditNote.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Invoice_Entry_Date"), "dd/MMM/yyyy")
                    gvCreditNote.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("Vendor_Code")
                    gvCreditNote.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("Vendor_Name")
                    gvCreditNote.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Total_Amount")))
                Next
            End If
        End If
    End Sub

    Sub LoadAdvancePaymentGridData()
        If isConsiderAdvancePayment Then
            LoadBlankGridAdvancePayment()
            If clsCommon.myLen(strVendorCode) > 0 Then
                Dim qry As String = "Select * from (" & _
               " Select TSPL_PAYMENT_HEADER.Vendor_Code,TSPL_PAYMENT_HEADER.Vendor_Name,TSPL_PAYMENT_HEADER.Payment_No,TSPL_PAYMENT_HEADER.Payment_Date,TSPL_PAYMENT_HEADER.Payment_Amount+isnull(TDS_Amount ,0) as Payment_Amount," & _
               " Balance_Amt+isnull(TDS_Amount ,0)-ISNULL((Select SUM(Payment_Amount)+sum(isnull(TDS_Amount ,0)) from TSPL_PAYMENT_HEADER PH WHERE PH.Posted<>'1' AND PH.Payment_Type='AD' AND PH.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code AND PH.Applied_Payment=TSPL_PAYMENT_HEADER.Payment_No),0) as Balance_Amt  from TSPL_PAYMENT_HEADER WHERE Posted='1' "
                If clsCommon.myLen(strVendorCode) <= 0 Then
                Else
                    qry += " AND Vendor_Code in  (" + strVendorCode + ") "
                End If
                qry += " AND   IsChkReverse='N' and Payment_Type IN ('AV','OA') and TSPL_PAYMENT_HEADER.Payment_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE()), "dd/MMM/yyyy hh:mm tt") + "' " & _
               " ) Final where Balance_Amt>0 order by Payment_Date"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        gvAdvancePayment.Rows.AddNew()
                        gvAdvancePayment.Rows(i).Cells(colAPSelect).Value = True
                        gvAdvancePayment.Rows(i).Cells(colAPSNo).Value = (i + 1)
                        gvAdvancePayment.Rows(i).Cells(colAPVendorCode).Value = clsCommon.myCstr(dt.Rows(i)("Vendor_Code"))
                        gvAdvancePayment.Rows(i).Cells(colAPVendorName).Value = clsCommon.myCstr(dt.Rows(i)("Vendor_Name"))
                        gvAdvancePayment.Rows(i).Cells(colAPPaymentCode).Value = clsCommon.myCstr(dt.Rows(i)("Payment_No"))
                        gvAdvancePayment.Rows(i).Cells(colAPPaymentDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Payment_Date"), "dd/MM/yyyy")
                        gvAdvancePayment.Rows(i).Cells(colAPPaymentAmt).Value = clsCommon.myCdbl(dt.Rows(i)("Payment_Amount"))
                        gvAdvancePayment.Rows(i).Cells(colAPPaymentAmtBalance).Value = clsCommon.myCdbl(dt.Rows(i)("Balance_Amt"))
                    Next
                End If
            End If
        End If
    End Sub

    Sub LoadMccSaleGridData()
        'gvMccSale.Rows.Clear()
        LoadBlankGridMccSale()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "   select TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE as [Shipment_No],TSPL_SD_SHIPMENT_HEAD.Document_Date as [Shipment_Date] ,TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code [Vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name as [Vendor_Name] , TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Sale_Inoivce_Date] ,TSPL_Customer_Invoice_Head.Document_No as [AR_Invoice_No] ,   TSPL_Customer_Invoice_Head.Document_Date as [AR_Invoice_Date], TSPL_Customer_Invoice_Head.Balance_Amt   from TSPL_SD_SHIPMENT_HEAD  left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code     inner join TSPL_Customer_Invoice_Head on  TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No  and coalesce(TSPL_Customer_Invoice_Head.Against_Sale_No,'')<>''   left outer join TSPL_VENDOR_MASTER   on  TSPL_VENDOR_MASTER .Vendor_code  =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code   left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No   where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC' and " & IIf(chkSkipPrevMccSale.Checked, " convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & " and TSPL_Customer_Invoice_Head.Loc_Code  ='" & fndLoc.Value & "'  and tspl_customer_invoice_head.Balance_Amt<>0 "
            Dim whrCls As String = ""


            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code  in (  " & strVendorCode & " )"
            End If
            qry = qry & whrCls

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvMccSale.Rows.AddNew()
                    gvMccSale.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvMccSale.Rows(i).Cells(colSelect).Value = True
                    gvMccSale.Rows(i).Cells(colShipmentNo).Value = dt.Rows(i)("Shipment_No")
                    gvMccSale.Rows(i).Cells(colShipmentDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Shipment_Date"), "dd/MMM/yyyy")
                    gvMccSale.Rows(i).Cells(colSaleInvNo).Value = dt.Rows(i)("Sale_Invoice_No")
                    gvMccSale.Rows(i).Cells(colSaleInvDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Sale_Inoivce_Date"), "dd/MMM/yyyy")
                    gvMccSale.Rows(i).Cells(colARInvoiceNo).Value = dt.Rows(i)("AR_Invoice_No")
                    gvMccSale.Rows(i).Cells(colARInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("AR_Invoice_Date"), "dd/MMM/yyyy")
                    gvMccSale.Rows(i).Cells(colCustomerCode).Value = dt.Rows(i)("Vendor_Code")
                    gvMccSale.Rows(i).Cells(colCustomerName).Value = dt.Rows(i)("Vendor_Name")
                    gvMccSale.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Balance_Amt")))
                    'gvMccSale.Rows(i).Cells(colReduceDeduc).Value = 0
                Next
            End If
        End If
    End Sub

    Sub LoadMccSaleReturnGridData()
        'GvMccSaleReturn.Rows.Clear()
        LoadBlankGridMccSaleReturn()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "   select TSPL_SD_SALE_RETURN_HEAD.Document_Code,case when coalesce(TSPL_SD_SALE_RETURN_HEAD.Return_Type,'')='D' then 'Damaged Goods' when coalesce(TSPL_SD_SALE_RETURN_HEAD.Return_Type,'')='P' then 'Price Only' when coalesce(TSPL_SD_SALE_RETURN_HEAD.Return_Type,'')='I' then 'Inventory Type' end as Document_Type,TSPL_SD_SALE_RETURN_HEAD.Document_Date,TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE as [Shipment_No]" _
                & " ,TSPL_SD_SHIPMENT_HEAD.Document_Date as [Shipment_Date] ,TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code [Vendor_Code] ," _
                & " TSPL_VENDOR_MASTER.Vendor_Name as [Vendor_Name] , TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No,TSPL_SD_SALE_INVOICE_HEAD.Document_Date as " _
                & " [Sale_Inoivce_Date] ,TSPL_Customer_Invoice_Head.Document_No as [AR_Invoice_No] ,   TSPL_Customer_Invoice_Head.Document_Date as " _
                & " [AR_Invoice_Date], TSPL_Customer_Invoice_Head.Balance_Amt   from TSPL_SD_SALE_RETURN_HEAD left join TSPL_SD_SHIPMENT_HEAD on " _
                & " TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No  left outer join  TSPL_CUSTOMER_VENDOR_MAPPING on " _
                & " TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code     inner join TSPL_Customer_Invoice_Head on  " _
                & " TSPL_Customer_Invoice_Head.against_Mcc_Material_Sale_return=TSPL_SD_SALE_RETURN_HEAD.Document_Code  and coalesce(TSPL_Customer_Invoice_Head.against_Mcc_Material_Sale_return,'')<>''  " _
                & " left outer join TSPL_VENDOR_MASTER   on  TSPL_VENDOR_MASTER .Vendor_code  =TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code   left outer join " _
                & " TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No   where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC' " _
                & " and " & IIf(ChkSkipMccSaleReturn.Checked, " convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and " _
                & " '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= " _
                & " '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & " and TSPL_Customer_Invoice_Head.Loc_Code  ='" & fndLoc.Value & "'  and " _
                & " tspl_customer_invoice_head.Balance_Amt<>0 "
            Dim whrCls As String = ""


            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_SD_SALE_RETURN_HEAD.Customer_code  in (  " & strVendorCode & " )"
            End If
            qry = qry & whrCls

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    GvMccSaleReturn.Rows.AddNew()
                    GvMccSaleReturn.Rows(i).Cells(colSlno).Value = (i + 1)
                    GvMccSaleReturn.Rows(i).Cells(colSelect).Value = True
                    GvMccSaleReturn.Rows(i).Cells(colReturnDocNo).Value = dt.Rows(i)("Document_Code")
                    GvMccSaleReturn.Rows(i).Cells(colReturnDocDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Document_Date"), "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colReturnDocType).Value = dt.Rows(i)("Document_Type")
                    GvMccSaleReturn.Rows(i).Cells(colShipmentNo).Value = dt.Rows(i)("Shipment_No")
                    GvMccSaleReturn.Rows(i).Cells(colShipmentDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Shipment_Date"), "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colSaleInvNo).Value = dt.Rows(i)("Sale_Invoice_No")
                    GvMccSaleReturn.Rows(i).Cells(colSaleInvDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Sale_Inoivce_Date"), "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colARInvoiceNo).Value = dt.Rows(i)("AR_Invoice_No")
                    GvMccSaleReturn.Rows(i).Cells(colARInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("AR_Invoice_Date"), "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colCustomerCode).Value = dt.Rows(i)("Vendor_Code")
                    GvMccSaleReturn.Rows(i).Cells(colCustomerName).Value = dt.Rows(i)("Vendor_Name")
                    GvMccSaleReturn.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Balance_Amt")))
                    'gvMccsaleReturn.Rows(i).Cells(colReduceDeduc).Value = 0
                Next
            End If
        End If
    End Sub

    Sub LoadBlankGridItemIssue()
        gvItemIssue.Rows.Clear()
        gvItemIssue.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select"
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItemIssue.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Item Issue Doc No"
        colTextBox.Name = colVspItemIssueNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Item Issue Doc Date"
        colTextBox.Name = colVspItemIssueDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colTextBox)




        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Code"
        'colTextBox.Name = colItemCode
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvItemIssue.MasterTemplate.Columns.Add(colTextBox)
        'gvItemIssue.BestFitColumns(BestFitColumnMode.AllCells)


        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Item Desc"
        'colTextBox.Name = colItemDesc
        'colTextBox.Width = 200
        'colTextBox.ReadOnly = True
        'gvItemIssue.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvItemIssue.MasterTemplate.Columns.Add(colDecimal)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Reduce " & Environment.NewLine & " Deduction"
        colDecimal.Name = colReduceDeduc
        colDecimal.Width = 100
        colDecimal.ReadOnly = False
        colDecimal.WrapText = True
        colDecimal.ShowUpDownButtons = False
        colDecimal.Minimum = 0
        gvItemIssue.MasterTemplate.Columns.Add(colDecimal)

        gvItemIssue.AllowAddNewRow = False
        gvItemIssue.AllowDeleteRow = False
        gvItemIssue.ShowGroupPanel = False
        gvItemIssue.AllowColumnReorder = True
        gvItemIssue.AllowRowReorder = False
        gvItemIssue.EnableSorting = True
        gvItemIssue.EnableFiltering = True
        gvItemIssue.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadBlankGridItemIssueReturn()
        gvItemIssueReturn.Rows.Clear()
        gvItemIssueReturn.Columns.Clear()


        colChkBox = New GridViewCheckBoxColumn()
        colChkBox.HeaderText = "Select"
        colChkBox.Name = colSelect
        colChkBox.ReadOnly = False
        colChkBox.Width = 50
        colChkBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvItemIssueReturn.MasterTemplate.Columns.Add(colChkBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SL. No."
        colTextBox.Name = colSlno
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Item Issue Return No"
        colTextBox.Name = colVspItemIssueReturnNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Item Issue Doc No"
        colTextBox.Name = colVspItemIssueNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Item Issue Return Date"
        colTextBox.Name = colVspItemIssueDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice No"
        colTextBox.Name = colAPInvoiceNo
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "AP Invoice Date"
        colTextBox.Name = colAPInvoiceDate
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Code"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Vendor Name"
        colTextBox.Name = colVendorDesc
        colTextBox.Width = 200
        colTextBox.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = ""
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = colItemAmt
        colDecimal.Width = 200
        colDecimal.ReadOnly = True
        gvItemIssueReturn.MasterTemplate.Columns.Add(colDecimal)

        'colDecimal = New GridViewDecimalColumn()
        'colDecimal.FormatString = ""
        'colDecimal.HeaderText = "Reduce " & Environment.NewLine & " Deduction"
        'colDecimal.Name = colReduceDeduc
        'colDecimal.Width = 100
        'colDecimal.ReadOnly = False
        'colDecimal.WrapText = True
        'colDecimal.ShowUpDownButtons = False
        'colDecimal.Minimum = 0
        'gvItemIssueReturn.MasterTemplate.Columns.Add(colDecimal)

        gvItemIssueReturn.AllowAddNewRow = False
        gvItemIssueReturn.AllowDeleteRow = False
        gvItemIssueReturn.ShowGroupPanel = False
        gvItemIssueReturn.AllowColumnReorder = True
        gvItemIssueReturn.AllowRowReorder = False
        gvItemIssueReturn.EnableSorting = True
        gvItemIssueReturn.EnableFiltering = True
        gvItemIssueReturn.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadItemIssueGridData()
        'gvItemIssue.Rows.Clear()
        LoadBlankGridItemIssue()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "   select TSPL_VSPItem_HEAD.Doc_No  as [Item_Issue_Doc_No],TSPL_VSPItem_HEAD.Doc_Date  as [Item_Issue_Doc_Date] ,TSPL_VSPItem_HEAD.From_Location , " & _
                " TSPL_VSPItem_HEAD.Issue_To as [vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [AP_Invoice_No] , " & _
                " TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  as [AP_Invoice_Date],TSPL_VENDOR_INVOICE_HEAD.Balance_Amt   from TSPL_VENDOR_INVOICE_HEAD  " & _
                " inner join TSPL_VSPItem_HEAD on TSPL_VSPItem_HEAD .Doc_No=TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No    " & _
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSPItem_HEAD.Issue_To  " & _
                " where TSPL_VSPItem_HEAD.Doc_Type='Issue' and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 and " & IIf(chkSkipPrevItemIssue.Checked, " convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & " " & _
                " and TSPL_VSPItem_HEAD.From_Location in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code   ='" & fndLoc.Value & "')  "
            Dim whrCls As String = ""
            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_VSPItem_HEAD.Issue_To  in ( " & strVendorCode & " )"
            End If
            qry = qry & whrCls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvItemIssue.Rows.AddNew()
                    gvItemIssue.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvItemIssue.Rows(i).Cells(colSelect).Value = True
                    gvItemIssue.Rows(i).Cells(colVspItemIssueNo).Value = dt.Rows(i)("Item_Issue_Doc_No")
                    gvItemIssue.Rows(i).Cells(colVspItemIssueDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Item_Issue_Doc_Date"), "dd/MMM/yyyy")
                    gvItemIssue.Rows(i).Cells(colAPInvoiceNo).Value = dt.Rows(i)("AP_Invoice_No")
                    gvItemIssue.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("AP_Invoice_Date"), "dd/MMM/yyyy")
                    gvItemIssue.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("vendor_Code")
                    gvItemIssue.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("Vendor_Name")
                    gvItemIssue.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Balance_Amt")))
                    gvItemIssue.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myFormat(0)
                Next
            End If
        End If
    End Sub

    Sub LoadItemIssueReturnGridData()
        'gvItemIssue.Rows.Clear()
        LoadBlankGridItemIssueReturn()
        If clsCommon.myLen(strVendorCode) > 0 Then
            Dim qry As String = "   select TSPL_VSPItem_HEAD.Doc_No  as [Item_Issue_Return_No],Issue_No as [Item_Issue_Doc_No],TSPL_VSPItem_HEAD.Doc_Date  as [Item_Issue_Return_Date] , " & _
                " TSPL_VSPItem_HEAD.From_Location ,TSPL_VSPItem_HEAD.Issue_To as [vendor_Code] ,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_VENDOR_INVOICE_HEAD.Document_No as [AP_Invoice_No] , " & _
                " TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date  as [AP_Invoice_Date],TSPL_VENDOR_INVOICE_HEAD.Balance_Amt   from TSPL_VENDOR_INVOICE_HEAD  " & _
                " inner join TSPL_VSPItem_HEAD on TSPL_VSPItem_HEAD .Doc_No=TSPL_VENDOR_INVOICE_HEAD.Against_VSPItemIssue_No    " & _
                " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VSPItem_HEAD.Issue_To  " & _
                " where TSPL_VSPItem_HEAD.Doc_Type='Return' and  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt<>0 and " & IIf(chkSkipPrevItemIssueReturn.Checked, " convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) between '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'", " convert(date,TSPL_VSPItem_HEAD.Doc_Date,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'") & " " & _
                " and TSPL_VSPItem_HEAD.From_Location in  (  select location_code from TSPL_LOCATION_MASTER where Loc_Segment_Code   ='" & fndLoc.Value & "')  "
            Dim whrCls As String = ""
            If clsCommon.myLen(strVendorCode) <= 0 Then
            Else
                whrCls = " and TSPL_VSPItem_HEAD.Issue_To  in ( " & strVendorCode & " )"
            End If
            qry = qry & whrCls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gvItemIssueReturn.Rows.AddNew()
                    gvItemIssueReturn.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvItemIssueReturn.Rows(i).Cells(colSelect).Value = True
                    gvItemIssueReturn.Rows(i).Cells(colVspItemIssueReturnNo).Value = clsCommon.myCstr(dt.Rows(i)("Item_Issue_Return_No"))
                    gvItemIssueReturn.Rows(i).Cells(colVspItemIssueNo).Value = clsCommon.myCstr(dt.Rows(i)("Item_Issue_Doc_No"))
                    gvItemIssueReturn.Rows(i).Cells(colVspItemIssueDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("Item_Issue_Return_Date"), "dd/MMM/yyyy")
                    gvItemIssueReturn.Rows(i).Cells(colAPInvoiceNo).Value = dt.Rows(i)("AP_Invoice_No")
                    gvItemIssueReturn.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(dt.Rows(i)("AP_Invoice_Date"), "dd/MMM/yyyy")
                    gvItemIssueReturn.Rows(i).Cells(colVendorCode).Value = dt.Rows(i)("vendor_Code")
                    gvItemIssueReturn.Rows(i).Cells(colVendorDesc).Value = dt.Rows(i)("Vendor_Name")
                    gvItemIssueReturn.Rows(i).Cells(colItemAmt).Value = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(i)("Balance_Amt")))
                    'gvItemIssueReturn.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myFormat(0)
                Next
            End If
        End If
    End Sub

    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Sub Reset()
        isLoad = True
        isNewEntry = True
        Dim dt As Date = clsCommon.GETSERVERDATE()
        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        dtpDate.Value = dt
        fndDocNo.Value = ""
        txtNEFTUploaderREFNo.Text = ""
        fndDocNo.MyReadOnly = False
        arrStrIssueItemCode = Nothing
        arrStrIssueItemDesc = Nothing
        arrStrMccSaleItemCode = Nothing
        arrStrMccSaleItemDesc = Nothing
        arrStrDedCode = Nothing
        arrStrDedDesc = Nothing
        strVendorCode = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        fndLoc.Value = ""
        txtLocName.Text = ""
        btnProcess.Enabled = False
        btnDelete.Enabled = False
        lblPending.Status = ERPTransactionStatus.Pending
        'btnSave.Visible = False
        'btnProcess.Enabled = True
        'btnDelete.Visible = False

        LoadBlankGridInvoice()
        LoadBlankGridItemIssue()
        LoadBlankGridItemIssueReturn()
        LoadBlankGridMccSale()
        LoadBlankGridMccSaleReturn()
        LoadBlankGridDeduction()
        LoadBlankGridCreditNote()
        LoadBlankGridAdvancePayment()
        LoadBlankGridGV()
        frm.desc = "Against Bulk Payment Process. "
        fndLoc.Enabled = True
        txtLocName.Enabled = True
        dtpToDate.Enabled = True
        dtpToDate.ReadOnly = True
        dtpFromDate.Enabled = True
        isLoad = False
        GroupBox2.Visible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowSkippingPrevDocumentsOnPaymentProcess, Nothing)) = 1, True, False)
    End Sub

    Private Sub FrmProvisionEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose.PerformClick()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.frmPaymentProcess)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")

            End If
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Function AllowToSave() As Boolean

        Try
            ' KUNAL > TICKET : BM00000009575 =======
            'If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
            '    dtpDate.Focus()
            '    Return False
            'End If

            If dtpFromDate.Value > dtpToDate.Value Then
                Throw New Exception(" 'From Date' can't be larger than 'To Date'")
            End If
            If clsCommon.GetDateWithEndTime(dtpDate.Value) < clsCommon.GetDateWithStartTime(dtpToDate.Value) Then
                Throw New Exception("Payment process date can't be less than to date")
            End If
            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                Throw New Exception(" Please select atleast one Vendor")
            End If

            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                Throw New Exception("Please select Location segment")
                fndLoc.Focus()
            End If

            If gv.Rows.Count <= 0 OrElse gv Is Nothing Then
                Throw New Exception("Please select atleast one document")
            End If
            '' done by Panch Raj against ticket no:BM00000008937
            '' unselect mcc sale trans for unseleceted vendor 
            Dim IsInvalidVendor As Boolean

            For Each grow As GridViewRowInfo In gvMccSale.Rows
                IsInvalidVendor = True
                If grow.Cells(colSelect).Value = True Then
                    For i As Integer = 0 To gv.Rows.Count - 1
                        If gv.Rows(i).Cells(colSelect).Value = True Then
                            If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                                IsInvalidVendor = False
                            End If
                        End If
                    Next
                    If IsInvalidVendor = True Then
                        Throw New Exception("MCC Sale : Invalid Vendor No-" & grow.Cells(colCustomerCode).Value & " at Line No-" & (grow.Index + 1) & ". Please unselect it")
                    End If
                End If
            Next

            '' unselect mcc sale return trans for unseleceted vendor 
            For Each grow As GridViewRowInfo In GvMccSaleReturn.Rows
                IsInvalidVendor = True
                If grow.Cells(colSelect).Value = True Then
                    For i As Integer = 0 To gv.Rows.Count - 1
                        If gv.Rows(i).Cells(colSelect).Value = True Then
                            If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                                IsInvalidVendor = False
                            End If
                        End If
                    Next
                    If IsInvalidVendor = True Then
                        Throw New Exception("MCC Sale Return : Invalid Vendor No-" & grow.Cells(colCustomerCode).Value & " at Line No-" & (grow.Index + 1) & ". Please unselect it")
                    End If
                End If
            Next

            '' unselect mcc Item Issue trans for unseleceted vendor 
            For Each grow As GridViewRowInfo In gvItemIssue.Rows
                IsInvalidVendor = True
                If grow.Cells(colSelect).Value = True Then
                    For i As Integer = 0 To gv.Rows.Count - 1
                        If gv.Rows(i).Cells(colSelect).Value = True Then
                            If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                                IsInvalidVendor = False
                            End If
                        End If
                    Next
                    If IsInvalidVendor = True Then
                        Throw New Exception("Item Issue : Invalid Vendor No-" & grow.Cells(colVendorCode).Value & " at Line No-" & (grow.Index + 1) & ". Please unselect it")
                    End If
                End If
            Next

            '' unselect mcc Item Issue trans for unseleceted vendor 
            For Each grow As GridViewRowInfo In gvItemIssueReturn.Rows
                IsInvalidVendor = True
                If grow.Cells(colSelect).Value = True Then
                    For i As Integer = 0 To gv.Rows.Count - 1
                        If gv.Rows(i).Cells(colSelect).Value = True Then
                            If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                                IsInvalidVendor = False
                            End If
                        End If
                    Next
                    If IsInvalidVendor = True Then
                        Throw New Exception("Item Issue Return : Invalid Vendor No-" & grow.Cells(colVendorCode).Value & " at Line No-" & (grow.Index + 1) & ". Please unselect it")
                    End If
                End If
            Next

            '' unselect mcc Deduction trans for unseleceted vendor 
            For Each grow As GridViewRowInfo In gvDeduction.Rows
                IsInvalidVendor = True
                If grow.Cells(colSelect).Value = True Then
                    For i As Integer = 0 To gv.Rows.Count - 1
                        If gv.Rows(i).Cells(colSelect).Value = True Then
                            If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                                IsInvalidVendor = False
                            End If
                        End If
                    Next
                    If IsInvalidVendor = True Then
                        Throw New Exception("Deduction : Invalid Vendor No-" & grow.Cells(colVendorCode).Value & " at Line No-" & (grow.Index + 1) & ". Please unselect it")
                    End If
                End If
            Next
            '' unselect mcc Credit trans for unseleceted vendor 
            For Each grow As GridViewRowInfo In gvCreditNote.Rows
                IsInvalidVendor = True
                If grow.Cells(colSelect).Value = True Then
                    For i As Integer = 0 To gv.Rows.Count - 1
                        If gv.Rows(i).Cells(colSelect).Value = True Then
                            If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                                IsInvalidVendor = False
                            End If
                        End If
                    Next
                    If IsInvalidVendor = True Then
                        Throw New Exception("Credit Note : Invalid Vendor No-" & grow.Cells(colVendorCode).Value & " at Line No-" & (grow.Index + 1) & ". Please unselect it")
                    End If
                End If
            Next

            ''richa agarwal changes in 08/08/2016
            'For i As Integer = 0 To gv.Rows.Count - 1
            '    If gv.Rows(i).Cells(colSelect).Value = True Then
            '        If gv.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value < (getTotalMccSaleSum(gv.Rows(i).Cells(colVendorCode).Value) - getTotalMccSaleReduceDeduSum(gv.Rows(i).Cells(colVendorCode).Value)) Then
            '            Throw New Exception(" Please Unselect some MCC Sale documents  " & Environment.NewLine & " Sale Amount can not be more than Milk Purchase Invoice amount At line no " & (i + 1))
            '        End If
            '    End If
            'Next

            'For i As Integer = 0 To gv.Rows.Count - 1
            '    If gv.Rows(i).Cells(colSelect).Value = True Then
            '        If gv.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value < (getTotalMccSaleSum(gv.Rows(i).Cells(colVendorCode).Value) - getTotalMccSaleReturnSum(gv.Rows(i).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(i).Cells(colVendorCode).Value) - getTotalItemIssueReturnSum(gv.Rows(i).Cells(colVendorCode).Value) + getTotalDeductionReduceDeduSum(gv.Rows(i).Cells(colVendorCode).Value) - getTotalCreditNoteSum(gv.Rows(i).Cells(colVendorCode).Value) - getTotalMccSaleReduceDeduSum(gv.Rows(i).Cells(colVendorCode).Value)) Then
            '            Throw New Exception(" Please Unselect some MCC Sale documents  " & Environment.NewLine & " Sale Amount can not be more than Milk Purchase Invoice amount At line no " & (i + 1))
            '        End If
            '    End If
            'Next

            For i As Integer = 0 To gv.Rows.Count - 1
                If gv.Rows(i).Cells(colSelect).Value = True Then
                    'If gv.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value < clsCommon.myCdbl(gv.Rows(i).Cells(colPaybleAmt).Value) Then
                    '    Throw New Exception("Payable Amount cannot be more than invoice Amount At line no " & (i + 1))
                    'End If
                    gv.Rows(i).Cells(colPaybleAmt).Value = Math.Round(clsCommon.myCdbl(gv.Rows(i).Cells(colPaybleAmt).Value), 2)
                    If clsCommon.myCdbl(gv.Rows(i).Cells(colPaybleAmt).Value) < 0 Then
                        Throw New Exception("Payable Amount cannot be in negative At line no " & (i + 1))
                    End If

                    If clsCommon.myCdbl(gv.Rows(i).Cells(colPaybleAmt).Value) = 0 Then
                        If Not (isConsiderAdvancePayment OrElse PayableAmountZeroForMCCSale) Then
                            Throw New Exception("Payable Amount cannot be zero At line no " & (i + 1))
                        End If
                    End If

                    If gv.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value < (getTotalMccSaleSum(gv.Rows(i).Cells(colVendorCode).Value) - getTotalMccSaleReturnSum(gv.Rows(i).Cells(colVendorCode).Value) - getTotalMccSaleReduceDeduSum(gv.Rows(i).Cells(colVendorCode).Value)) Then
                        Throw New Exception(" Please Unselect some MCC Sale documents  " & Environment.NewLine & " Sale Amount can not be more than Milk Purchase Invoice amount At line no " & (i + 1))
                    End If
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        ''----------------------
        Return True
    End Function

    Sub deleteData()
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Want To Delete The Doc No : " & fndDocNo.Value & " ?", "Confirm", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    If clsPaymentProcessHead.deleteData(fndDocNo.Value) Then
                        clsCommon.MyMessageBoxShow("Deleted successFully")
                        Reset()
                    End If
                End If
            Else
                Throw New Exception("Doc No not Found to delete")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deleteData()
    End Sub

    Sub SaveData(Optional ByVal isPostbtnClick As Boolean = False)

        Dim i As Integer = 0
        Try
            AllowToSave()

            For i = 0 To gv.Rows.Count - 1
                If clsCommon.myLen(gv.Rows(i).Cells(colBankCode).Value) <= 0 Then
                    frm.StartPosition = FormStartPosition.CenterScreen
                    frm.desc = "Against Payment Process "
                    frm.ShowDialog()
                    If frm.btnOkClicked Then
                        Exit For
                    Else
                        Exit Sub
                    End If
                End If
            Next
            isCellValueChanged = True
            For i = 0 To gvInvoice.Rows.Count - 1
                If clsCommon.myLen(gvInvoice.Rows(i).Cells(colBankCode).Value) <= 0 AndAlso clsCommon.myLen(frm.bankCode) > 0 Then
                    gvInvoice.Rows(i).Cells(colBankCode).Value = frm.bankCode
                    gvInvoice.Rows(i).Cells(colBankDesc).Value = frm.bankDesc
                    gvInvoice.Rows(i).Cells(colPayMode).Value = frm.paymentMode
                    gvInvoice.Rows(i).Cells(colChequeNo).Value = ""
                End If
            Next
            For i = 0 To gv.Rows.Count - 1
                If clsCommon.myLen(gv.Rows(i).Cells(colBankCode).Value) <= 0 AndAlso clsCommon.myLen(frm.bankCode) > 0 Then
                    gv.Rows(i).Cells(colBankCode).Value = frm.bankCode
                    gv.Rows(i).Cells(colBankDesc).Value = frm.bankDesc
                    gv.Rows(i).Cells(colPayMode).Value = frm.paymentMode
                    If clsCommon.myCBool(gv.Rows(i).Cells(colSelect).Value) = True Then
                        If clsCommon.CompairString(gv.Rows(i).Cells(colPayMode).Value, "Cheque") = CompairStringResult.Equal Then
                            gv.Rows(i).Cells(colChequeNo).ReadOnly = False
                            gv.Rows(i).Cells(colChequeDate).ReadOnly = False

                            For j As Integer = 0 To gv.Rows.Count - 1
                                gv.Rows(j).Cells(colBankCode).Value = frm.bankCode
                                gv.Rows(j).Cells(colBankDesc).Value = frm.bankDesc
                                gv.Rows(j).Cells(colPayMode).Value = frm.paymentMode
                            Next
                            If clsCommon.myLen(gv.Rows(i).Cells(colChequeNo).Value) <= 0 Then
                                Throw New Exception("Please Enter Cheque No at line no: " & i + 1 & ". Cheque No is mandatory in case of Payment Mode:Cheque.")
                            End If
                            If clsCommon.myLen(gv.Rows(i).Cells(colChequeDate).Value) <= 0 Then
                                Throw New Exception("Please Enter Cheque Date at line no: " & i + 1 & ". Cheque Date is mandatory in case of Payment Mode:Cheque.")
                            End If
                        Else
                            gv.Rows(i).Cells(colChequeNo).ReadOnly = True
                            gv.Rows(i).Cells(colChequeNo).Value = ""

                            gv.Rows(i).Cells(colChequeDate).ReadOnly = True
                            gv.Rows(i).Cells(colChequeDate).Value = Nothing
                        End If
                    End If

                Else
                    If clsCommon.myCBool(gv.Rows(i).Cells(colSelect).Value) = True Then
                        gv.Rows(i).Cells(colBankCode).Value = frm.bankCode
                        gv.Rows(i).Cells(colBankDesc).Value = frm.bankDesc
                        gv.Rows(i).Cells(colPayMode).Value = frm.paymentMode
                        If clsCommon.CompairString(gv.Rows(i).Cells(colPayMode).Value, "Cheque") = CompairStringResult.Equal Then
                            gv.Rows(i).Cells(colChequeNo).ReadOnly = False
                            gv.Rows(i).Cells(colChequeDate).ReadOnly = False
                            For j As Integer = 0 To gv.Rows.Count - 1
                                gv.Rows(j).Cells(colBankCode).Value = frm.bankCode
                                gv.Rows(j).Cells(colBankDesc).Value = frm.bankDesc
                                gv.Rows(j).Cells(colPayMode).Value = frm.paymentMode
                            Next
                            If clsCommon.myLen(gv.Rows(i).Cells(colChequeNo).Value) <= 0 Then
                                Throw New Exception("Please Enter Cheque No at line no: " & i + 1 & ". Cheque No is mandatory in case of Payment Mode:Cheque.")
                            End If
                            If clsCommon.myLen(gv.Rows(i).Cells(colChequeDate).Value) <= 0 Then
                                Throw New Exception("Please Enter Cheque Date at line no: " & i + 1 & ". Cheque Date is mandatory in case of Payment Mode:Cheque.")
                            End If
                        Else
                            gv.Rows(i).Cells(colChequeNo).ReadOnly = True
                            gv.Rows(i).Cells(colChequeNo).Value = ""

                            gv.Rows(i).Cells(colChequeDate).ReadOnly = True
                            gv.Rows(i).Cells(colChequeDate).Value = Nothing
                        End If
                    End If

                End If
            Next
            isCellValueChanged = False
            Dim obj As clsPaymentProcessHead = New clsPaymentProcessHead()
            obj.Doc_No = fndDocNo.Value
            obj.DocRefNoForUploader = clsCommon.myCstr(txtNEFTUploaderREFNo.Text)
            obj.Doc_Date = clsCommon.GetPrintDate(dtpDate.Value, "dd/MMM/yyyy")
            obj.From_Date = clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy")
            obj.To_Date = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy")
            obj.Loc_Seg_Code = clsCommon.myCstr(fndLoc.Value)
            ''richa agarwal 07-jan-2016
            If btnSave.Text = "Update" Then
                frm.desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(paymentDesc,'') as paymentDesc from tspl_payment_process_head where doc_no='" & clsCommon.myCstr(fndDocNo.Value) & "'"))
            End If
            ''-------------------
            obj.PaymentDesc = frm.desc
            If gvInvoice IsNot Nothing AndAlso gvInvoice.Rows.Count > 0 Then
                obj.arrClsPaymentProcessInvoices = New List(Of clsPaymentProcessInvoices)
                Dim objPayProInv As clsPaymentProcessInvoices = Nothing
                For i = 0 To gvInvoice.Rows.Count - 1
                    If gvInvoice.Rows(i).Cells(colSelect).Value = True Then
                        objPayProInv = New clsPaymentProcessInvoices
                        objPayProInv.SLNO = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colSlno).Value)
                        objPayProInv.Milk_Purchase_Invoice_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                        objPayProInv.Milk_Purchase_Invoice_Date = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPurchaseInvoiceDate).Value)
                        objPayProInv.AP_Invoice_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value)
                        objPayProInv.AP_Invoice_Date = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value)
                        objPayProInv.VLC_CODE = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colVLCName).Value)
                        objPayProInv.VSP_CODE = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colVendorCode).Value)
                        objPayProInv.VSP_NAME = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colVendorDesc).Value)
                        objPayProInv.Payee_Joint_Name = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointName).Value)
                        objPayProInv.Payee_Joint_Bank_Code = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointBankCode).Value)
                        objPayProInv.Payee_Joint_Bank_Name = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointBankDesc).Value)
                        objPayProInv.Payee_Joint_Branch_Code = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointBranchCode).Value)
                        objPayProInv.Payee_Joint_Branch_Name = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointBranchDesc).Value)
                        objPayProInv.Payee_Joint_IFSC_Code = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointIFSC).Value)
                        objPayProInv.Payee_Joint_Ac_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayeeJointAcNo).Value)
                        objPayProInv.Milk_Qty = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colMilkQty).Value)
                        objPayProInv.Inv_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colInvAmt).Value)
                        objPayProInv.Inv_EMP_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colEmpAmt).Value)
                        objPayProInv.Inv_Amt_EMP_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colInvAndEmpAmt).Value)
                        objPayProInv.Inv_Incentive_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colIncenAmt).Value)
                        objPayProInv.Inv_Incentive_EMP_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colIncenEmpAmt).Value)
                        objPayProInv.Gross_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value)
                        objPayProInv.Vsp_Own_System_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Value)
                        objPayProInv.Head_Load_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colHeadLoadAmt).Value)
                        objPayProInv.Deduction_Amount = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colInvDeduc).Value)
                        objPayProInv.Reduce_Deduc_Amt = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colReduceDeduc).Value)
                        objPayProInv.Vsp_Own_System_Doc_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Tag)
                        objPayProInv.Head_Load_Doc_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colHeadLoadAmt).Tag)
                        objPayProInv.Deduction_Doc_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colInvDeduc).Tag)
                        objPayProInv.Bank_Code = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colBankCode).Value)
                        objPayProInv.Bank_Desc = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colBankDesc).Value)
                        objPayProInv.Payment_Mode = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colPayMode).Value)
                        objPayProInv.Cheque_No = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colChequeNo).Value)
                        objPayProInv.Service_Charge_Amt = clsCommon.myCstr(gvInvoice.Rows(i).Cells(colServiceChargeAmt).Value)
                        obj.arrClsPaymentProcessInvoices.Add(objPayProInv)
                    End If
                Next
            End If

            If gvMccSale IsNot Nothing AndAlso gvMccSale.Rows.Count > 0 Then
                obj.arrClsPaymentProcessMccSale = New List(Of clsPaymentProcessMCCSale)
                Dim objPayProMccSale As clsPaymentProcessMCCSale = Nothing
                For i = 0 To gvMccSale.Rows.Count - 1
                    If gvMccSale.Rows(i).Cells(colSelect).Value = True Then
                        objPayProMccSale = New clsPaymentProcessMCCSale
                        objPayProMccSale.SLNO = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colSlno).Value)
                        objPayProMccSale.Shipment_Doc_No = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colShipmentNo).Value)
                        objPayProMccSale.Shipment_Doc_Date = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colShipmentDate).Value)
                        objPayProMccSale.Sale_Doc_No = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colSaleInvNo).Value)
                        objPayProMccSale.Sale_Doc_Date = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colSaleInvDate).Value)
                        objPayProMccSale.AR_Invoice_No = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colARInvoiceNo).Value)
                        objPayProMccSale.AR_Invoice_Date = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colARInvoiceDate).Value)
                        objPayProMccSale.Customer_CODE = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colCustomerCode).Value)
                        objPayProMccSale.Customer_NAME = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colCustomerName).Value)
                        'objPayProMccSale.Item_Code = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colItemCode).Value)
                        'objPayProMccSale.Item_Desc = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colItemDesc).Value)
                        objPayProMccSale.Amount = clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colItemAmt).Value)
                        objPayProMccSale.Reduce_Deduc_Amt = clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colReduceDeduc).Value)
                        obj.arrClsPaymentProcessMccSale.Add(objPayProMccSale)
                    End If
                Next
            End If

            If GvMccSaleReturn IsNot Nothing AndAlso GvMccSaleReturn.Rows.Count > 0 Then
                obj.arrClsPaymentProcessMccSaleReturn = New List(Of clsPaymentProcessMCCSaleReturn)
                Dim objPayProMccSale As clsPaymentProcessMCCSaleReturn = Nothing
                For i = 0 To GvMccSaleReturn.Rows.Count - 1
                    If GvMccSaleReturn.Rows(i).Cells(colSelect).Value = True Then
                        objPayProMccSale = New clsPaymentProcessMCCSaleReturn
                        objPayProMccSale.SLNO = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colSlno).Value)
                        objPayProMccSale.Shipment_Doc_No = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colShipmentNo).Value)
                        objPayProMccSale.Shipment_Doc_Date = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colShipmentDate).Value)
                        objPayProMccSale.Sale_Doc_No = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colSaleInvNo).Value)
                        objPayProMccSale.Sale_Doc_Date = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colSaleInvDate).Value)
                        objPayProMccSale.AR_Invoice_No = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colARInvoiceNo).Value)
                        objPayProMccSale.AR_Invoice_Date = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colARInvoiceDate).Value)
                        objPayProMccSale.Customer_CODE = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colCustomerCode).Value)
                        objPayProMccSale.Customer_NAME = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colCustomerName).Value)
                        objPayProMccSale.Amount = clsCommon.myCdbl(GvMccSaleReturn.Rows(i).Cells(colItemAmt).Value)
                        objPayProMccSale.Return_Doc_No = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colReturnDocNo).Value)
                        objPayProMccSale.Return_Doc_Date = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colReturnDocDate).Value)
                        objPayProMccSale.Return_Doc_Type = clsCommon.myCstr(GvMccSaleReturn.Rows(i).Cells(colReturnDocType).Value)
                        obj.arrClsPaymentProcessMccSaleReturn.Add(objPayProMccSale)
                    End If
                Next
            End If

            If gvItemIssue IsNot Nothing AndAlso gvItemIssue.Rows.Count > 0 Then
                obj.arrClsPaymentProcessItemIssue = New List(Of clsPaymentProcessItemIssue)
                Dim objPayProItemIssue As clsPaymentProcessItemIssue = Nothing
                For i = 0 To gvItemIssue.Rows.Count - 1
                    If gvItemIssue.Rows(i).Cells(colSelect).Value = True Then
                        objPayProItemIssue = New clsPaymentProcessItemIssue
                        objPayProItemIssue.SLNO = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colSlno).Value)
                        objPayProItemIssue.Item_Issue_Doc_No = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVspItemIssueNo).Value)
                        objPayProItemIssue.Item_Issue_Doc_Date = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVspItemIssueDate).Value)
                        objPayProItemIssue.AP_Invoice_No = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colAPInvoiceNo).Value)
                        objPayProItemIssue.AP_Invoice_Date = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colAPInvoiceDate).Value)
                        objPayProItemIssue.Vendor_CODE = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVendorCode).Value)
                        objPayProItemIssue.Vendor_NAME = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colVendorDesc).Value)
                        objPayProItemIssue.Amount = clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colItemAmt).Value)
                        objPayProItemIssue.Reduce_Deduc_Amt = clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colReduceDeduc).Value)
                        obj.arrClsPaymentProcessItemIssue.Add(objPayProItemIssue)
                    End If
                Next
            End If

            If gvItemIssueReturn IsNot Nothing AndAlso gvItemIssueReturn.Rows.Count > 0 Then
                obj.arrClsPaymentProcessItemIssueReturn = New List(Of clsPaymentProcessItemIssueReturn)
                Dim objPayProItemIssueReturn As clsPaymentProcessItemIssueReturn = Nothing
                For i = 0 To gvItemIssueReturn.Rows.Count - 1
                    If gvItemIssueReturn.Rows(i).Cells(colSelect).Value = True Then
                        objPayProItemIssueReturn = New clsPaymentProcessItemIssueReturn
                        objPayProItemIssueReturn.SLNO = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colSlno).Value)
                        objPayProItemIssueReturn.Item_Issue_Return_No = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colVspItemIssueReturnNo).Value)
                        objPayProItemIssueReturn.Item_Issue_Doc_No = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colVspItemIssueNo).Value)
                        objPayProItemIssueReturn.Item_Issue_Return_Date = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colVspItemIssueDate).Value)
                        objPayProItemIssueReturn.AP_Invoice_No = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colAPInvoiceNo).Value)
                        objPayProItemIssueReturn.AP_Invoice_Date = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colAPInvoiceDate).Value)
                        objPayProItemIssueReturn.Vendor_CODE = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colVendorCode).Value)
                        objPayProItemIssueReturn.Vendor_NAME = clsCommon.myCstr(gvItemIssueReturn.Rows(i).Cells(colVendorDesc).Value)
                        objPayProItemIssueReturn.Amount = clsCommon.myCdbl(gvItemIssueReturn.Rows(i).Cells(colItemAmt).Value)
                        'objPayProItemIssueReturn.Reduce_Deduc_Amt = clsCommon.myCdbl(gvItemIssueReturn.Rows(i).Cells(colReduceDeduc).Value)
                        obj.arrClsPaymentProcessItemIssueReturn.Add(objPayProItemIssueReturn)
                    End If
                Next
            End If

            If gvDeduction IsNot Nothing AndAlso gvDeduction.Rows.Count > 0 Then
                obj.arrClsPaymentProcessDeductions = New List(Of clsPaymentProcessDeduction)
                Dim objPayProDeduction As clsPaymentProcessDeduction = Nothing
                For i = 0 To gvDeduction.Rows.Count - 1
                    If gvDeduction.Rows(i).Cells(colSelect).Value = True Then
                        objPayProDeduction = New clsPaymentProcessDeduction
                        objPayProDeduction.SLNO = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colSlno).Value)
                        objPayProDeduction.AP_Invoice_No = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colAPInvoiceNo).Value)
                        objPayProDeduction.AP_Invoice_Date = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colAPInvoiceDate).Value)
                        objPayProDeduction.Vendor_CODE = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colVendorCode).Value)
                        objPayProDeduction.Vendor_NAME = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colVendorDesc).Value)
                        objPayProDeduction.Ded_Code = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colDeductionCode).Value)
                        objPayProDeduction.Ded_Desc = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colDeductionDesc).Value)
                        objPayProDeduction.Amount = clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colItemAmt).Value)
                        objPayProDeduction.Reduce_Deduc_Amt = clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colReduceDeduc).Value)
                        obj.arrClsPaymentProcessDeductions.Add(objPayProDeduction)
                    End If
                Next
            End If

            If gvCreditNote IsNot Nothing AndAlso gvCreditNote.Rows.Count > 0 Then
                obj.arrClsPaymentProcessCreditNote = New List(Of clsPaymentProcessCreditNote)
                Dim objPayProCreditNote As clsPaymentProcessCreditNote = Nothing
                For i = 0 To gvCreditNote.Rows.Count - 1
                    If gvCreditNote.Rows(i).Cells(colSelect).Value = True Then
                        objPayProCreditNote = New clsPaymentProcessCreditNote
                        objPayProCreditNote.SLNO = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colSlno).Value)
                        objPayProCreditNote.AP_Invoice_No = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colAPInvoiceNo).Value)
                        objPayProCreditNote.AP_Invoice_Date = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colAPInvoiceDate).Value)
                        objPayProCreditNote.Vendor_CODE = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colVendorCode).Value)
                        objPayProCreditNote.Vendor_NAME = clsCommon.myCstr(gvCreditNote.Rows(i).Cells(colVendorDesc).Value)
                        objPayProCreditNote.Amount = clsCommon.myCdbl(gvCreditNote.Rows(i).Cells(colItemAmt).Value)
                        obj.arrClsPaymentProcessCreditNote.Add(objPayProCreditNote)
                    End If
                Next
            End If

            If gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
                obj.ArrPPDetail = New List(Of clsPaymentProcessDetail)
                Dim objPPDetail As clsPaymentProcessDetail = Nothing
                For i = 0 To gv.Rows.Count - 1
                    If clsCommon.myCBool(gv.Rows(i).Cells(colSelect).Value) Then
                        objPPDetail = New clsPaymentProcessDetail()
                        objPPDetail.Is_select = clsCommon.myCBool(gv.Rows(i).Cells(colSelect).Value)
                        objPPDetail.SNo = clsCommon.myCstr(gv.Rows(i).Cells(colSlno).Value)
                        objPPDetail.Milk_Purchase_Invoice_No = clsCommon.myCstr(gv.Rows(i).Cells(colPurchaseInvoiceNo).Value)
                        objPPDetail.Milk_Purchase_Invoice_Date = clsCommon.myCDate(gv.Rows(i).Cells(colPurchaseInvoiceDate).Value)
                        objPPDetail.AP_Invoice_No = clsCommon.myCstr(gv.Rows(i).Cells(colAPInvoiceNo).Value)
                        objPPDetail.AP_Invoice_Date = clsCommon.myCDate(gv.Rows(i).Cells(colAPInvoiceDate).Value)
                        objPPDetail.VLC_CODE_Uploader = clsCommon.myCstr(gv.Rows(i).Cells(colVLCUploaderCode).Value)
                        objPPDetail.VLC_Name = clsCommon.myCstr(gv.Rows(i).Cells(colVLCName).Value)
                        objPPDetail.VSP_CODE = clsCommon.myCstr(gv.Rows(i).Cells(colVendorCode).Value)
                        objPPDetail.VSP_NAME = clsCommon.myCstr(gv.Rows(i).Cells(colVendorDesc).Value)
                        objPPDetail.Main_VSP_CODE = clsCommon.myCstr(gv.Rows(i).Cells(colActualVSPCode).Value)
                        objPPDetail.Main_VSP_NAME = clsCommon.myCstr(gv.Rows(i).Cells(colActualVSPName).Value)
                        objPPDetail.Payee_Joint_Name = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointName).Value)
                        objPPDetail.Payee_Joint_Bank_Code = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointBankCode).Value)
                        objPPDetail.Payee_Joint_Bank_Name = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointBankDesc).Value)
                        objPPDetail.Payee_Joint_Branch_Code = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointBranchCode).Value)
                        objPPDetail.Payee_Joint_Branch_Name = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointBranchDesc).Value)
                        objPPDetail.Payee_Joint_Account_No = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointAcNo).Value)
                        objPPDetail.Payee_Joint_IFSC_Code = clsCommon.myCstr(gv.Rows(i).Cells(colPayeeJointIFSC).Value)
                        objPPDetail.Bank_Code = clsCommon.myCstr(gv.Rows(i).Cells(colBankCode).Value)
                        objPPDetail.Bank_Desc = clsCommon.myCstr(gv.Rows(i).Cells(colBankDesc).Value)
                        objPPDetail.Payment_Mode = clsCommon.myCstr(gv.Rows(i).Cells(colPayMode).Value)
                        objPPDetail.Cheque_No = clsCommon.myCstr(gv.Rows(i).Cells(colChequeNo).Value)
                        If clsCommon.CompairString(objPPDetail.Payment_Mode, "Cheque") = CompairStringResult.Equal Then
                            objPPDetail.Cheque_Dated = clsCommon.myCstr(gv.Rows(i).Cells(colChequeDate).Value)
                        Else
                            objPPDetail.Cheque_Dated = Nothing
                        End If

                        objPPDetail.Milk_Qty = clsCommon.myCdbl(gv.Rows(i).Cells(colMilkQty).Value)
                        objPPDetail.VSP_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colVSPAmount).Value)
                        objPPDetail.MP_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colMPAmount).Value)
                        objPPDetail.MP_VSP_Diff_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colMPVSPDiffAmount).Value)
                        objPPDetail.Milk_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colInvAmt).Value)
                        objPPDetail.Incentive_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colIncenAmt).Value)
                        objPPDetail.EMP_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colEmpAmt).Value)
                        objPPDetail.Incentive_EMP_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colIncenEmpAmt).Value)
                        objPPDetail.Total_EMP_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colTotalEmp).Value)
                        objPPDetail.Total = clsCommon.myCdbl(gv.Rows(i).Cells(colInvAndEmpAmt).Value)
                        objPPDetail.Total_Invoice_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value)
                        objPPDetail.Vsp_Own_System_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colVSPOwnSystemAmt).Value)
                        objPPDetail.Head_Load_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colHeadLoadAmt).Value)
                        objPPDetail.Invoice_Deduction_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colInvDeduc).Value)
                        objPPDetail.Reduce_Deduc_Amt = clsCommon.myCdbl(gv.Rows(i).Cells(colReduceDeduc).Value)
                        objPPDetail.MCC_Sale_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colMccSaleTotalAmount).Value)
                        objPPDetail.MCC_Sale_Return_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colMccSaleReturnTotalAmount).Value)
                        objPPDetail.Item_Issue_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colItemIssueTotalAmount).Value)
                        objPPDetail.Item_Issue_Return_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colItemIssueReturnTotalAmount).Value)
                        objPPDetail.Deduction_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colDeductionTotalAmount).Value)
                        objPPDetail.Credit_Note_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colTotalCreditNoteAmount).Value)
                        objPPDetail.Payable_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colPaybleAmt).Value)
                        objPPDetail.Service_Charge_Amt = clsCommon.myCdbl(gv.Rows(i).Cells(colServiceChargeAmt).Value)

                        objPPDetail.Advance_Payment_Amount = clsCommon.myCdbl(gv.Rows(i).Cells(colAdvanceAmount).Value)
                        objPPDetail.Advance_Payment_Amount_Knock_Off = clsCommon.myCdbl(gv.Rows(i).Cells(colAdvanceKnockOffAmount).Value)
                        obj.ArrPPDetail.Add(objPPDetail)
                    End If
                Next
            End If

            If gvAdvancePayment IsNot Nothing AndAlso gvAdvancePayment.Rows.Count > 0 Then
                obj.ArrPPAdvancePayment = New List(Of clsPaymentProcessAdvancePayment)
                Dim objPPAdvancePayment As clsPaymentProcessAdvancePayment = Nothing
                For i = 0 To gvAdvancePayment.Rows.Count - 1
                    If clsCommon.myCBool(gvAdvancePayment.Rows(i).Cells(colAPSelect).Value) Then
                        objPPAdvancePayment = New clsPaymentProcessAdvancePayment
                        objPPAdvancePayment.SNo = clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPSNo).Value)
                        objPPAdvancePayment.Vendor_Code = clsCommon.myCstr(gvAdvancePayment.Rows(i).Cells(colAPVendorCode).Value)
                        objPPAdvancePayment.Payment_No = clsCommon.myCstr(gvAdvancePayment.Rows(i).Cells(colAPPaymentCode).Value)
                        objPPAdvancePayment.Payment_Amount = clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPPaymentAmt).Value)
                        objPPAdvancePayment.Payment_Balance = clsCommon.myCdbl(gvAdvancePayment.Rows(i).Cells(colAPPaymentAmtBalance).Value)
                        obj.ArrPPAdvancePayment.Add(objPPAdvancePayment)
                    End If
                Next
            End If

            If clsPaymentProcessHead.SaveData(obj, isNewEntry) Then
                If Not isPostbtnClick Then
                    If isNewEntry Then
                        myMessages.insert()
                    Else
                        myMessages.update()
                    End If
                End If
                LoadData(obj.Doc_No, NavigatorType.Current)
            End If
        Catch ex As Exception
            If isPostbtnClick Then
                Throw New Exception(ex.Message)
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal navType As NavigatorType)
        Dim obj As clsPaymentProcessHead = clsPaymentProcessHead.getData(strCode, navType)
        If obj IsNot Nothing Then
            Reset()
            fndLoc.Enabled = False
            isNewEntry = False
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            btnProcess.Enabled = True
            fndDocNo.MyReadOnly = True
            fndDocNo.Value = obj.Doc_No
            fndLoc.Value = obj.Loc_Seg_Code
            txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndLoc.Value & "' "))
            dtpFromDate.Value = obj.From_Date
            dtpToDate.Value = obj.To_Date
            dtpDate.Value = obj.Doc_Date
            txtNEFTUploaderREFNo.Text = obj.DocRefNoForUploader
            txtNEFTUploaderREFNo.Tag = obj.PaymentDesc
            If obj.isPosted = 0 Then
                lblPending.Status = ERPTransactionStatus.Pending
            Else
                btnSave.Enabled = False
                btnProcess.Enabled = False
                btnDelete.Enabled = False
                lblPending.Status = ERPTransactionStatus.Approved
            End If
            isLoad = True
            Dim i As Integer = 0
            If obj.arrClsPaymentProcessInvoices IsNot Nothing AndAlso obj.arrClsPaymentProcessInvoices.Count > 0 Then
                gvInvoice.Rows.Clear()
                For i = 0 To obj.arrClsPaymentProcessInvoices.Count - 1
                    gvInvoice.Rows.AddNew()
                    gvInvoice.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvInvoice.Rows(i).Cells(colSelect).Value = True
                    gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value = obj.arrClsPaymentProcessInvoices.Item(i).AP_Invoice_No
                    gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessInvoices.Item(i).AP_Invoice_Date, "dd/MMM/yyyy")
                    gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value = obj.arrClsPaymentProcessInvoices.Item(i).Milk_Purchase_Invoice_No
                    gvInvoice.Rows(i).Cells(colPurchaseInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessInvoices.Item(i).Milk_Purchase_Invoice_Date, "dd/MMM/yyyy")
                    gvInvoice.Rows(i).Cells(colVLCCode).Value = getVLCUploaderCode(obj.arrClsPaymentProcessInvoices.Item(i).VSP_CODE)
                    gvInvoice.Rows(i).Cells(colVLCName).Value = obj.arrClsPaymentProcessInvoices.Item(i).VLC_CODE
                    gvInvoice.Rows(i).Cells(colVendorCode).Value = obj.arrClsPaymentProcessInvoices.Item(i).VSP_CODE
                    gvInvoice.Rows(i).Cells(colVendorDesc).Value = obj.arrClsPaymentProcessInvoices.Item(i).VSP_NAME
                    gvInvoice.Rows(i).Cells(colPayeeJointName).Value = obj.arrClsPaymentProcessInvoices.Item(i).Payee_Joint_Name
                    gvInvoice.Rows(i).Cells(colPayeeJointBankCode).Value = obj.arrClsPaymentProcessInvoices.Item(i).Payee_Joint_Bank_Code
                    gvInvoice.Rows(i).Cells(colPayeeJointBankDesc).Value = obj.arrClsPaymentProcessInvoices.Item(i).Payee_Joint_Bank_Name
                    gvInvoice.Rows(i).Cells(colPayeeJointBranchCode).Value = obj.arrClsPaymentProcessInvoices.Item(i).Payee_Joint_Branch_Code
                    gvInvoice.Rows(i).Cells(colPayeeJointBranchDesc).Value = obj.arrClsPaymentProcessInvoices.Item(i).Payee_Joint_Branch_Name
                    gvInvoice.Rows(i).Cells(colPayeeJointIFSC).Value = obj.arrClsPaymentProcessInvoices.Item(i).Payee_Joint_IFSC_Code
                    gvInvoice.Rows(i).Cells(colPayeeJointAcNo).Value = obj.arrClsPaymentProcessInvoices.Item(i).Payee_Joint_Ac_No
                    gvInvoice.Rows(i).Cells(colMilkQty).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessInvoices.Item(i).Milk_Qty)
                    gvInvoice.Rows(i).Cells(colInvAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessInvoices.Item(i).Inv_Amount)
                    gvInvoice.Rows(i).Cells(colEmpAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessInvoices.Item(i).Inv_EMP_Amount)
                    gvInvoice.Rows(i).Cells(colInvAndEmpAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessInvoices.Item(i).Inv_Amt_EMP_Amount)
                    gvInvoice.Rows(i).Cells(colIncenAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessInvoices.Item(i).Inv_Incentive_Amount)
                    gvInvoice.Rows(i).Cells(colIncenEmpAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessInvoices.Item(i).Inv_Incentive_EMP_Amount)
                    gvInvoice.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessInvoices.Item(i).Gross_Amount)
                    gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Value = obj.arrClsPaymentProcessInvoices.Item(i).Vsp_Own_System_Amount
                    gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Tag = obj.arrClsPaymentProcessInvoices.Item(i).Vsp_Own_System_Doc_No
                    gvInvoice.Rows(i).Cells(colHeadLoadAmt).Value = obj.arrClsPaymentProcessInvoices.Item(i).Head_Load_Amount
                    gvInvoice.Rows(i).Cells(colHeadLoadAmt).Tag = obj.arrClsPaymentProcessInvoices.Item(i).Head_Load_Doc_No
                    gvInvoice.Rows(i).Cells(colInvDeduc).Value = obj.arrClsPaymentProcessInvoices.Item(i).Deduction_Amount
                    gvInvoice.Rows(i).Cells(colInvDeduc).Tag = obj.arrClsPaymentProcessInvoices.Item(i).Deduction_Doc_No
                    gvInvoice.Rows(i).Cells(colReduceDeduc).Value = obj.arrClsPaymentProcessInvoices.Item(i).Reduce_Deduc_Amt
                    gvInvoice.Rows(i).Cells(colReduceDeduc).Value = obj.arrClsPaymentProcessInvoices.Item(i).Reduce_Deduc_Amt
                    gvInvoice.Rows(i).Cells(colBankCode).Value = obj.arrClsPaymentProcessInvoices.Item(i).Bank_Code
                    gvInvoice.Rows(i).Cells(colBankDesc).Value = obj.arrClsPaymentProcessInvoices.Item(i).Bank_Desc
                    gvInvoice.Rows(i).Cells(colPayMode).Value = obj.arrClsPaymentProcessInvoices.Item(i).Payment_Mode
                    gvInvoice.Rows(i).Cells(colChequeNo).Value = obj.arrClsPaymentProcessInvoices.Item(i).Cheque_No
                    gvInvoice.Rows(i).Cells(colServiceChargeAmt).Value = obj.arrClsPaymentProcessInvoices.Item(i).Service_Charge_Amt
                    gvInvoice.Rows(i).Cells(colActualVSPCode).Value = obj.arrClsPaymentProcessInvoices.Item(i).ActualVSPCode
                    gvInvoice.Rows(i).Cells(colActualVSPName).Value = obj.arrClsPaymentProcessInvoices.Item(i).ActualVSPName
                Next

            End If
            If obj.arrClsPaymentProcessMccSale IsNot Nothing AndAlso obj.arrClsPaymentProcessMccSale.Count > 0 Then
                gvMccSale.Rows.Clear()
                For i = 0 To obj.arrClsPaymentProcessMccSale.Count - 1
                    gvMccSale.Rows.AddNew()
                    gvMccSale.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvMccSale.Rows(i).Cells(colSelect).Value = True
                    gvMccSale.Rows(i).Cells(colShipmentNo).Value = obj.arrClsPaymentProcessMccSale.Item(i).Shipment_Doc_No
                    gvMccSale.Rows(i).Cells(colShipmentDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSale.Item(i).Shipment_Doc_Date, "dd/MMM/yyyy")
                    gvMccSale.Rows(i).Cells(colSaleInvNo).Value = obj.arrClsPaymentProcessMccSale.Item(i).Sale_Doc_No
                    gvMccSale.Rows(i).Cells(colSaleInvDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSale.Item(i).Sale_Doc_Date, "dd/MMM/yyyy")
                    gvMccSale.Rows(i).Cells(colARInvoiceNo).Value = obj.arrClsPaymentProcessMccSale.Item(i).AR_Invoice_No
                    gvMccSale.Rows(i).Cells(colARInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSale.Item(i).AR_Invoice_Date, "dd/MMM/yyyy")
                    gvMccSale.Rows(i).Cells(colCustomerCode).Value = obj.arrClsPaymentProcessMccSale.Item(i).Customer_CODE
                    gvMccSale.Rows(i).Cells(colCustomerName).Value = obj.arrClsPaymentProcessMccSale.Item(i).Customer_NAME
                    'gvMccSale.Rows(i).Cells(colItemCode).Value = obj.arrClsPaymentProcessMccSale.Item(i).Item_Code
                    'gvMccSale.Rows(i).Cells(colItemDesc).Value = obj.arrClsPaymentProcessMccSale.Item(i).Item_Desc
                    gvMccSale.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSale.Item(i).Amount)
                    gvMccSale.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSale.Item(i).Reduce_Deduc_Amt)
                Next
            End If
            If obj.arrClsPaymentProcessItemIssue IsNot Nothing AndAlso obj.arrClsPaymentProcessItemIssue.Count > 0 Then
                gvItemIssue.Rows.Clear()
                For i = 0 To obj.arrClsPaymentProcessItemIssue.Count - 1
                    gvItemIssue.Rows.AddNew()
                    gvItemIssue.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvItemIssue.Rows(i).Cells(colSelect).Value = True
                    gvItemIssue.Rows(i).Cells(colVspItemIssueNo).Value = obj.arrClsPaymentProcessItemIssue.Item(i).Item_Issue_Doc_No
                    gvItemIssue.Rows(i).Cells(colVspItemIssueDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessItemIssue.Item(i).Item_Issue_Doc_Date, "dd/MMM/yyyy")
                    gvItemIssue.Rows(i).Cells(colAPInvoiceNo).Value = obj.arrClsPaymentProcessItemIssue.Item(i).AP_Invoice_No
                    gvItemIssue.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessItemIssue.Item(i).AP_Invoice_Date, "dd/MMM/yyyy")
                    gvItemIssue.Rows(i).Cells(colVendorCode).Value = obj.arrClsPaymentProcessItemIssue.Item(i).Vendor_CODE
                    gvItemIssue.Rows(i).Cells(colVendorDesc).Value = obj.arrClsPaymentProcessItemIssue.Item(i).Vendor_NAME
                    'gvItemIssue.Rows(i).Cells(colItemCode).Value = obj.arrClsPaymentProcessItemIssue.Item(i).Item_Code
                    'gvItemIssue.Rows(i).Cells(colItemDesc).Value = obj.arrClsPaymentProcessItemIssue.Item(i).Item_Desc
                    gvItemIssue.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessItemIssue.Item(i).Amount)
                    gvItemIssue.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessItemIssue.Item(i).Reduce_Deduc_Amt)
                Next
            End If

            If obj.arrClsPaymentProcessItemIssueReturn IsNot Nothing AndAlso obj.arrClsPaymentProcessItemIssueReturn.Count > 0 Then
                gvItemIssueReturn.Rows.Clear()
                For i = 0 To obj.arrClsPaymentProcessItemIssueReturn.Count - 1
                    gvItemIssueReturn.Rows.AddNew()
                    gvItemIssueReturn.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvItemIssueReturn.Rows(i).Cells(colSelect).Value = True
                    gvItemIssueReturn.Rows(i).Cells(colVspItemIssueReturnNo).Value = obj.arrClsPaymentProcessItemIssueReturn.Item(i).Item_Issue_Return_No
                    gvItemIssueReturn.Rows(i).Cells(colVspItemIssueNo).Value = obj.arrClsPaymentProcessItemIssueReturn.Item(i).Item_Issue_Doc_No
                    gvItemIssueReturn.Rows(i).Cells(colVspItemIssueDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessItemIssueReturn.Item(i).Item_Issue_Return_Date, "dd/MMM/yyyy")
                    gvItemIssueReturn.Rows(i).Cells(colAPInvoiceNo).Value = obj.arrClsPaymentProcessItemIssueReturn.Item(i).AP_Invoice_No
                    gvItemIssueReturn.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessItemIssueReturn.Item(i).AP_Invoice_Date, "dd/MMM/yyyy")
                    gvItemIssueReturn.Rows(i).Cells(colVendorCode).Value = obj.arrClsPaymentProcessItemIssueReturn.Item(i).Vendor_CODE
                    gvItemIssueReturn.Rows(i).Cells(colVendorDesc).Value = obj.arrClsPaymentProcessItemIssueReturn.Item(i).Vendor_NAME
                    gvItemIssueReturn.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessItemIssueReturn.Item(i).Amount)
                    'gvItemIssueReturn.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessItemIssueReturn.Item(i).Reduce_Deduc_Amt)
                Next
            End If

            If obj.arrClsPaymentProcessDeductions IsNot Nothing AndAlso obj.arrClsPaymentProcessDeductions.Count > 0 Then
                gvDeduction.Rows.Clear()
                For i = 0 To obj.arrClsPaymentProcessDeductions.Count - 1
                    gvDeduction.Rows.AddNew()
                    gvDeduction.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvDeduction.Rows(i).Cells(colSelect).Value = True
                    gvDeduction.Rows(i).Cells(colAPInvoiceNo).Value = obj.arrClsPaymentProcessDeductions.Item(i).AP_Invoice_No
                    gvDeduction.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessDeductions.Item(i).AP_Invoice_Date, "dd/MMM/yyyy")
                    gvDeduction.Rows(i).Cells(colVendorCode).Value = obj.arrClsPaymentProcessDeductions.Item(i).Vendor_CODE
                    gvDeduction.Rows(i).Cells(colVendorDesc).Value = obj.arrClsPaymentProcessDeductions.Item(i).Vendor_NAME
                    gvDeduction.Rows(i).Cells(colDeductionCode).Value = obj.arrClsPaymentProcessDeductions.Item(i).Ded_Code
                    gvDeduction.Rows(i).Cells(colDeductionDesc).Value = obj.arrClsPaymentProcessDeductions.Item(i).Ded_Desc
                    gvDeduction.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessDeductions.Item(i).Amount)
                    gvDeduction.Rows(i).Cells(colReduceDeduc).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessDeductions.Item(i).Reduce_Deduc_Amt)
                Next
            End If

            If obj.arrClsPaymentProcessCreditNote IsNot Nothing AndAlso obj.arrClsPaymentProcessCreditNote.Count > 0 Then
                gvCreditNote.Rows.Clear()
                For i = 0 To obj.arrClsPaymentProcessCreditNote.Count - 1
                    gvCreditNote.Rows.AddNew()
                    gvCreditNote.Rows(i).Cells(colSlno).Value = (i + 1)
                    gvCreditNote.Rows(i).Cells(colSelect).Value = True
                    gvCreditNote.Rows(i).Cells(colAPInvoiceNo).Value = obj.arrClsPaymentProcessCreditNote.Item(i).AP_Invoice_No
                    gvCreditNote.Rows(i).Cells(colAPInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessCreditNote.Item(i).AP_Invoice_Date, "dd/MMM/yyyy")
                    gvCreditNote.Rows(i).Cells(colVendorCode).Value = obj.arrClsPaymentProcessCreditNote.Item(i).Vendor_CODE
                    gvCreditNote.Rows(i).Cells(colVendorDesc).Value = obj.arrClsPaymentProcessCreditNote.Item(i).Vendor_NAME
                    gvCreditNote.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessCreditNote.Item(i).Amount)

                Next
            End If
            If obj.arrClsPaymentProcessMccSaleReturn IsNot Nothing AndAlso obj.arrClsPaymentProcessMccSaleReturn.Count > 0 Then
                GvMccSaleReturn.Rows.Clear()
                For i = 0 To obj.arrClsPaymentProcessMccSaleReturn.Count - 1
                    GvMccSaleReturn.Rows.AddNew()
                    GvMccSaleReturn.Rows(i).Cells(colSlno).Value = (i + 1)
                    GvMccSaleReturn.Rows(i).Cells(colSelect).Value = True
                    GvMccSaleReturn.Rows(i).Cells(colReturnDocNo).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Return_Doc_No
                    GvMccSaleReturn.Rows(i).Cells(colReturnDocType).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Return_Doc_Type
                    GvMccSaleReturn.Rows(i).Cells(colReturnDocDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSaleReturn.Item(i).Return_Doc_Date, "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colShipmentNo).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Shipment_Doc_No
                    GvMccSaleReturn.Rows(i).Cells(colShipmentDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSaleReturn.Item(i).Shipment_Doc_Date, "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colSaleInvNo).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Sale_Doc_No
                    GvMccSaleReturn.Rows(i).Cells(colSaleInvDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSaleReturn.Item(i).Sale_Doc_Date, "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colARInvoiceNo).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).AR_Invoice_No
                    GvMccSaleReturn.Rows(i).Cells(colARInvoiceDate).Value = clsCommon.GetPrintDate(obj.arrClsPaymentProcessMccSaleReturn.Item(i).AR_Invoice_Date, "dd/MMM/yyyy")
                    GvMccSaleReturn.Rows(i).Cells(colCustomerCode).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Customer_CODE
                    GvMccSaleReturn.Rows(i).Cells(colCustomerName).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Customer_NAME
                    'gvMccSaleReturn.Rows(i).Cells(colItemCode).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Item_Code
                    'gvMccSaleReturn.Rows(i).Cells(colItemDesc).Value = obj.arrClsPaymentProcessMccSaleReturn.Item(i).Item_Desc
                    GvMccSaleReturn.Rows(i).Cells(colItemAmt).Value = clsCommon.myCdbl(obj.arrClsPaymentProcessMccSaleReturn.Item(i).Amount)
                Next
            End If
            LoadBlankGridGV()


            If obj.ArrPPAdvancePayment IsNot Nothing AndAlso obj.ArrPPAdvancePayment.Count > 0 Then
                gvAdvancePayment.Rows.Clear()
                For i = 0 To obj.ArrPPAdvancePayment.Count - 1
                    gvAdvancePayment.Rows.AddNew()
                    gvAdvancePayment.Rows(i).Cells(colAPSNo).Value = (i + 1)
                    gvAdvancePayment.Rows(i).Cells(colAPSelect).Value = True
                    gvAdvancePayment.Rows(i).Cells(colAPVendorCode).Value = obj.ArrPPAdvancePayment.Item(i).Vendor_Code
                    gvAdvancePayment.Rows(i).Cells(colAPVendorName).Value = obj.ArrPPAdvancePayment.Item(i).Vendor_Name
                    gvAdvancePayment.Rows(i).Cells(colAPPaymentCode).Value = obj.ArrPPAdvancePayment.Item(i).Payment_No
                    gvAdvancePayment.Rows(i).Cells(colAPPaymentDate).Value = obj.ArrPPAdvancePayment.Item(i).Payment_Date
                    gvAdvancePayment.Rows(i).Cells(colAPPaymentAmt).Value = obj.ArrPPAdvancePayment.Item(i).Payment_Amount
                    gvAdvancePayment.Rows(i).Cells(colAPPaymentAmtBalance).Value = obj.ArrPPAdvancePayment.Item(i).Payment_Balance
                Next
            End If

            If obj.ArrPPDetail IsNot Nothing AndAlso obj.ArrPPDetail.Count > 0 Then
                Dim arr As New ArrayList()
                For i = 0 To obj.ArrPPDetail.Count - 1
                    If Not arr.Contains(obj.ArrPPDetail.Item(i).VSP_CODE) Then
                        arr.Add(obj.ArrPPDetail.Item(i).VSP_CODE)
                    End If

                    gv.Rows.AddNew()
                    gv.Rows(i).Cells(colSlno).Value = obj.ArrPPDetail.Item(i).SNo
                    gv.Rows(i).Cells(colSelect).Value = obj.ArrPPDetail.Item(i).Is_select
                    gv.Rows(i).Cells(colPurchaseInvoiceNo).Value = obj.ArrPPDetail.Item(i).Milk_Purchase_Invoice_No
                    gv.Rows(i).Cells(colPurchaseInvoiceDate).Value = obj.ArrPPDetail.Item(i).Milk_Purchase_Invoice_Date
                    gv.Rows(i).Cells(colAPInvoiceNo).Value = obj.ArrPPDetail.Item(i).AP_Invoice_No
                    gv.Rows(i).Cells(colAPInvoiceDate).Value = obj.ArrPPDetail.Item(i).AP_Invoice_Date
                    gv.Rows(i).Cells(colVLCUploaderCode).Value = obj.ArrPPDetail.Item(i).VLC_CODE_Uploader
                    gv.Rows(i).Cells(colVLCName).Value = obj.ArrPPDetail.Item(i).VLC_Name
                    gv.Rows(i).Cells(colVendorCode).Value = obj.ArrPPDetail.Item(i).VSP_CODE
                    gv.Rows(i).Cells(colVendorDesc).Value = obj.ArrPPDetail.Item(i).VSP_NAME
                    gv.Rows(i).Cells(colActualVSPCode).Value = obj.ArrPPDetail.Item(i).Main_VSP_CODE
                    gv.Rows(i).Cells(colActualVSPName).Value = obj.ArrPPDetail.Item(i).Main_VSP_NAME
                    gv.Rows(i).Cells(colPayeeJointName).Value = obj.ArrPPDetail.Item(i).Payee_Joint_Name
                    gv.Rows(i).Cells(colPayeeJointBankCode).Value = obj.ArrPPDetail.Item(i).Payee_Joint_Bank_Code
                    gv.Rows(i).Cells(colPayeeJointBankDesc).Value = obj.ArrPPDetail.Item(i).Payee_Joint_Bank_Name
                    gv.Rows(i).Cells(colPayeeJointBranchCode).Value = obj.ArrPPDetail.Item(i).Payee_Joint_Branch_Code
                    gv.Rows(i).Cells(colPayeeJointBranchDesc).Value = obj.ArrPPDetail.Item(i).Payee_Joint_Branch_Name
                    gv.Rows(i).Cells(colPayeeJointAcNo).Value = obj.ArrPPDetail.Item(i).Payee_Joint_Account_No
                    gv.Rows(i).Cells(colPayeeJointIFSC).Value = obj.ArrPPDetail.Item(i).Payee_Joint_IFSC_Code
                    gv.Rows(i).Cells(colBankCode).Value = obj.ArrPPDetail.Item(i).Bank_Code
                    gv.Rows(i).Cells(colBankDesc).Value = obj.ArrPPDetail.Item(i).Bank_Desc
                    gv.Rows(i).Cells(colPayMode).Value = obj.ArrPPDetail.Item(i).Payment_Mode
                    gv.Rows(i).Cells(colChequeNo).Value = obj.ArrPPDetail.Item(i).Cheque_No
                    If clsCommon.CompairString(obj.ArrPPDetail.Item(i).Payment_Mode, "Cheque") = CompairStringResult.Equal Then
                        gv.Rows(i).Cells(colChequeDate).Value = obj.ArrPPDetail.Item(i).Cheque_Dated
                    Else
                        gv.Rows(i).Cells(colChequeDate).Value = Nothing
                    End If

                    gv.Rows(i).Cells(colMilkQty).Value = obj.ArrPPDetail.Item(i).Milk_Qty
                    gv.Rows(i).Cells(colVSPAmount).Value = obj.ArrPPDetail.Item(i).VSP_Amount
                    gv.Rows(i).Cells(colMPAmount).Value = obj.ArrPPDetail.Item(i).MP_Amount
                    gv.Rows(i).Cells(colMPVSPDiffAmount).Value = obj.ArrPPDetail.Item(i).MP_VSP_Diff_Amount
                    gv.Rows(i).Cells(colIncenAmt).Value = obj.ArrPPDetail.Item(i).Incentive_Amount
                    gv.Rows(i).Cells(colEmpAmt).Value = obj.ArrPPDetail.Item(i).EMP_Amount
                    gv.Rows(i).Cells(colIncenEmpAmt).Value = obj.ArrPPDetail.Item(i).Incentive_EMP_Amount
                    gv.Rows(i).Cells(colTotalEmp).Value = obj.ArrPPDetail.Item(i).Total_EMP_Amount
                    gv.Rows(i).Cells(colInvAmt).Value = obj.ArrPPDetail.Item(i).Milk_Amount
                    gv.Rows(i).Cells(colInvAndEmpAmt).Value = obj.ArrPPDetail.Item(i).Incentive_EMP_Amount
                    gv.Rows(i).Cells(colInvAndEmpAmt).Value = obj.ArrPPDetail.Item(i).Total
                    gv.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value = obj.ArrPPDetail.Item(i).Total_Invoice_Amount
                    gv.Rows(i).Cells(colVSPOwnSystemAmt).Value = obj.ArrPPDetail.Item(i).Vsp_Own_System_Amount
                    gv.Rows(i).Cells(colHeadLoadAmt).Value = obj.ArrPPDetail.Item(i).Head_Load_Amount
                    gv.Rows(i).Cells(colInvDeduc).Value = obj.ArrPPDetail.Item(i).Invoice_Deduction_Amount
                    gv.Rows(i).Cells(colReduceDeduc).Value = obj.ArrPPDetail.Item(i).Reduce_Deduc_Amt
                    gv.Rows(i).Cells(colMccSaleTotalAmount).Value = obj.ArrPPDetail.Item(i).MCC_Sale_Amount
                    gv.Rows(i).Cells(colMccSaleReturnTotalAmount).Value = obj.ArrPPDetail.Item(i).MCC_Sale_Return_Amount
                    gv.Rows(i).Cells(colItemIssueTotalAmount).Value = obj.ArrPPDetail.Item(i).Item_Issue_Amount
                    gv.Rows(i).Cells(colItemIssueReturnTotalAmount).Value = obj.ArrPPDetail.Item(i).Item_Issue_Return_Amount
                    gv.Rows(i).Cells(colDeductionTotalAmount).Value = obj.ArrPPDetail.Item(i).Deduction_Amount
                    gv.Rows(i).Cells(colTotalCreditNoteAmount).Value = obj.ArrPPDetail.Item(i).Credit_Note_Amount
                    gv.Rows(i).Cells(colPaybleAmt).Value = obj.ArrPPDetail.Item(i).Payable_Amount
                    gv.Rows(i).Cells(colServiceChargeAmt).Value = obj.ArrPPDetail.Item(i).Service_Charge_Amt

                    gv.Rows(i).Cells(colAdvanceAmount).Value = obj.ArrPPDetail.Item(i).Advance_Payment_Amount
                    gv.Rows(i).Cells(colAdvanceKnockOffAmount).Value = obj.ArrPPDetail.Item(i).Advance_Payment_Amount_Knock_Off
                Next
                txtVSP.arrValueMember = arr

                ReStoreGridLayout()
            Else
                loadGvData()
            End If
            isLoad = False
        Else
            Reset()
        End If
    End Sub

    Private Sub fndDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNo._MYNavigator
        LoadData(fndDocNo.Value, NavType)
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Try
        '    If AllowToSave() Then
        SaveData(False)
        '    End If
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    Sub PaymentProcess()

        Try
            'frm.StartPosition = FormStartPosition.CenterScreen
            'frm.desc = "Against Payment Process "
            'frm.ShowDialog()
            ' If frm.btnOkClicked Then
            If Not AllowToSave() Then
                Exit Sub
            End If
            GC.Collect()
            GC.WaitForPendingFinalizers()
            SaveData(True)
            If clsCommon.MyMessageBoxShow("Continue to Process the payment ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                clsPaymentProcessHead.ProcessData(fndDocNo.Value, IIf(clsCommon.myLen(txtNEFTUploaderREFNo.Tag) > 0, txtNEFTUploaderREFNo.Tag, frm.desc))
                clsCommon.MyMessageBoxShow("Payment Processed")
                LoadData(fndDocNo.Value, NavigatorType.Current)
            End If
            ' End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function isItemFoundInList(ByVal arrStr As List(Of String), ByVal str As String) As Boolean

        If arrStr Is Nothing OrElse arrStr.Count <= 0 Then
            Return False
        Else
            For i As Integer = 0 To arrStr.Count - 1
                If clsCommon.CompairString(arrStr.Item(i), str) = CompairStringResult.Equal Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function

    Function isDedFoundInList(ByVal arrStr As List(Of String), ByVal str As String) As Boolean

        If arrStr Is Nothing OrElse arrStr.Count <= 0 Then
            Return False
        Else
            For i As Integer = 0 To arrStr.Count - 1
                If clsCommon.CompairString(arrStr.Item(i), str) = CompairStringResult.Equal Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function

    Sub getDistinctMccSaleItem()
        arrStrMccSaleItemCode = New List(Of String)
        arrStrMccSaleItemDesc = New List(Of String)
        Dim itemCode As String = ""
        Dim itemDes As String = ""
        For i As Integer = 0 To gvMccSale.Rows.Count - 1
            itemCode = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colItemCode).Value)
            itemDes = clsCommon.myCstr(gvMccSale.Rows(i).Cells(colItemDesc).Value)
            If isItemFoundInList(arrStrMccSaleItemCode, itemCode) = False Then
                arrStrMccSaleItemCode.Add(itemCode)
                arrStrMccSaleItemDesc.Add(itemDes)
            End If
        Next

    End Sub

    Sub getDistinctDeduction()
        arrStrDedCode = New List(Of String)
        arrStrDedDesc = New List(Of String)
        Dim dedCode As String = ""
        Dim dedDes As String = ""
        For i As Integer = 0 To gvDeduction.Rows.Count - 1
            dedCode = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colDeductionCode).Value)
            dedDes = clsCommon.myCstr(gvDeduction.Rows(i).Cells(colDeductionDesc).Value)
            If isItemFoundInList(arrStrDedCode, dedCode) = False Then
                arrStrDedCode.Add(dedCode)
                arrStrDedDesc.Add(dedDes)
            End If
        Next

    End Sub

    Sub getDistinctVSPIssueItem()
        arrStrIssueItemCode = New List(Of String)
        arrStrIssueItemDesc = New List(Of String)
        Dim itemCode As String = ""
        Dim itemDes As String = ""
        For i As Integer = 0 To gvItemIssue.Rows.Count - 1
            itemCode = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colItemCode).Value)
            itemDes = clsCommon.myCstr(gvItemIssue.Rows(i).Cells(colItemDesc).Value)
            If isItemFoundInList(arrStrIssueItemCode, itemCode) = False Then
                arrStrIssueItemCode.Add(itemCode)
                arrStrIssueItemDesc.Add(itemDes)
            End If
        Next

    End Sub

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        isLoad = True
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select Location segment")
                Exit Sub
            End If

            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                txtVSP.Focus()
                Throw New Exception("Please select at lease one VSP ")
            End If
            LoadInvoiceGridData()
            If isMultipleDocumentForSameVendor() Then
                gvInvoice.Rows.Clear()
                clsCommon.MyMessageBoxShow("Multiple Invoices For Same vendor Found in selected date range" & Environment.NewLine & "Please select another Date range and continue " & Environment.NewLine & getMultipleDocumentForSameVendor())
                Exit Sub
            End If
            getVendors()
            LoadMccSaleGridData()
            LoadMccSaleReturnGridData()
            LoadItemIssueGridData()
            LoadItemIssueReturnGridData()
            LoadDeductionGridData()
            LoadCreditNoteGridData()
            LoadAdvancePaymentGridData()
            LoadBlankGridGV()
            loadGvData()
            If PayableAmountZeroForMCCSale Then
                For ii As Integer = 0 To gv.Rows.Count - 1
                    Dim strVSPCode As String = clsCommon.myCstr(gv.Rows(ii).Cells(colVendorCode).Value)
                    Dim dblAmt As Double = Math.Round(clsCommon.myCdbl(gv.Rows(ii).Cells(colPaybleAmt).Value), 2, MidpointRounding.AwayFromZero)
                    If dblAmt < 0 Then ''IF amount negative
                        dblAmt = Math.Abs(dblAmt)
                        For jj As Integer = 0 To gvMccSale.Rows.Count - 1
                            If clsCommon.CompairString(strVSPCode, clsCommon.myCstr(gvMccSale.Rows(jj).Cells(colCustomerCode).Value)) = CompairStringResult.Equal Then
                                gvMccSale.CurrentColumn = gvMccSale.Columns(colReduceDeduc)
                                gvMccSale.CurrentRow = gvMccSale.Rows(jj)
                                If dblAmt > clsCommon.myCdbl(gvMccSale.Rows(jj).Cells(colItemAmt).Value) Then
                                    gvMccSale.Rows(jj).Cells(colReduceDeduc).Value = Math.Round(clsCommon.myCdbl(gvMccSale.Rows(jj).Cells(colItemAmt).Value))
                                    dblAmt -= Math.Round(clsCommon.myCdbl(gvMccSale.Rows(jj).Cells(colItemAmt).Value), 2, MidpointRounding.AwayFromZero)
                                Else
                                    gvMccSale.Rows(jj).Cells(colReduceDeduc).Value = dblAmt
                                    dblAmt = 0
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            isLoad = False
        End Try
    End Sub

    Sub loadSaleData(ByVal qry As String, ByVal arrStr As List(Of String))
        Dim qry1 As String = " select amount    from(  " & qry & " ) yyy where 1=1 "
        Dim dblAmt As Double = 0
        Dim whrCls As String = ""
        If arrStr IsNot Nothing AndAlso arrStr.Count > 0 Then
            For i As Integer = 0 To gv.Rows.Count - 1
                For j As Integer = 0 To arrStr.Count - 1
                    whrCls = " and [Item Code] ='" & arrStr.Item(j).ToString & "' and Location='" & gv.Rows(i).Cells("MCC_CODE").Value & "' and vendor_code='" & gv.Rows(i).Cells("Vendor_Code").Value & "' "
                    dblAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry1 & whrCls))
                    gv.Rows(i).Cells(arrStr.Item(j).ToString).Value = dblAmt
                Next
            Next
        End If
    End Sub

    Function getItemSumMccSale(ByVal itemCode As String, ByVal vendorCode As String) As Double
        Dim rValue As Double = 0
        If gvMccSale.Rows.Count > 0 Then
            For i As Integer = 0 To gvMccSale.Rows.Count - 1
                If clsCommon.CompairString(gvMccSale.Rows(i).Cells(colCustomerCode).Value, vendorCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvMccSale.Rows(i).Cells(colItemCode).Value, itemCode) = CompairStringResult.Equal Then
                    rValue = rValue + clsCommon.myCdbl(gvMccSale.Rows(i).Cells(colItemAmt).Value)
                End If
            Next
        End If
        Return rValue
    End Function

    Function getItemSumItemIssue(ByVal itemCode As String, ByVal vendorCode As String) As Double
        Dim rValue As Double = 0
        If gvItemIssue.Rows.Count > 0 Then
            For i As Integer = 0 To gvItemIssue.Rows.Count - 1
                If clsCommon.CompairString(gvItemIssue.Rows(i).Cells(colVendorCode).Value, vendorCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvItemIssue.Rows(i).Cells(colItemCode).Value, itemCode) = CompairStringResult.Equal Then
                    rValue = rValue + clsCommon.myCdbl(gvItemIssue.Rows(i).Cells(colItemAmt).Value)
                End If
            Next
        End If
        Return rValue
    End Function

    Function getDedSum(ByVal DedCode As String, ByVal vendorCode As String) As Double
        Dim rValue As Double = 0
        If gvDeduction.Rows.Count > 0 Then
            For i As Integer = 0 To gvDeduction.Rows.Count - 1
                If clsCommon.CompairString(gvDeduction.Rows(i).Cells(colVendorCode).Value, vendorCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvDeduction.Rows(i).Cells(colDeductionCode).Value, DedCode) = CompairStringResult.Equal Then
                    rValue = rValue + clsCommon.myCdbl(gvDeduction.Rows(i).Cells(colItemAmt).Value)
                End If
            Next
        End If
        Return rValue
    End Function

    Sub loadGvData()
        Dim k As Integer = -1
        Dim VendCustCode As String = ""
        getVendors()
        isEmpOnAmtOnly = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select isnull(TSPL_MCC_MASTER.empOnAmountOnly,0) as empOnAmountOnly  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code =(select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")) = 0, False, True)
        If gvInvoice.Rows.Count > 0 Then
            For i As Integer = 0 To gvInvoice.Rows.Count - 1
                'If gvInvoice.Rows(i).Cells(colSelect).Value = True Then
                gv.Rows.AddNew()
                k = k + 1
                'For j As Integer = 1 To gvInvoice.Columns.Count - 1
                gv.Rows(k).Cells(colSelect).Value = gvInvoice.Rows(i).Cells(colSelect).Value
                gv.Rows(k).Cells(colSlno).Value = gvInvoice.Rows(i).Cells(colSlno).Value
                gv.Rows(k).Cells(colPurchaseInvoiceNo).Value = gvInvoice.Rows(i).Cells(colPurchaseInvoiceNo).Value
                gv.Rows(k).Cells(colPurchaseInvoiceDate).Value = gvInvoice.Rows(i).Cells(colPurchaseInvoiceDate).Value
                gv.Rows(k).Cells(colAPInvoiceNo).Value = gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value
                gv.Rows(k).Cells(colAPInvoiceDate).Value = gvInvoice.Rows(i).Cells(colAPInvoiceDate).Value
                gv.Rows(k).Cells(colVLCUploaderCode).Value = gvInvoice.Rows(i).Cells(colVLCCode).Value
                gv.Rows(k).Cells(colVLCName).Value = gvInvoice.Rows(i).Cells(colVLCName).Value
                gv.Rows(k).Cells(colVLCUploaderCode).Value = gvInvoice.Rows(i).Cells(colVLCCode).Value
                gv.Rows(k).Cells(colVendorCode).Value = gvInvoice.Rows(i).Cells(colVendorCode).Value
                gv.Rows(k).Cells(colVendorDesc).Value = gvInvoice.Rows(i).Cells(colVendorDesc).Value
                gv.Rows(k).Cells(colPayeeJointName).Value = gvInvoice.Rows(i).Cells(colPayeeJointName).Value
                gv.Rows(k).Cells(colPayeeJointBankCode).Value = gvInvoice.Rows(i).Cells(colPayeeJointBankCode).Value
                gv.Rows(k).Cells(colPayeeJointBankDesc).Value = gvInvoice.Rows(i).Cells(colPayeeJointBankDesc).Value
                gv.Rows(k).Cells(colPayeeJointBranchCode).Value = gvInvoice.Rows(i).Cells(colPayeeJointBranchCode).Value
                gv.Rows(k).Cells(colPayeeJointBranchDesc).Value = gvInvoice.Rows(i).Cells(colPayeeJointBranchDesc).Value
                gv.Rows(k).Cells(colPayeeJointAcNo).Value = gvInvoice.Rows(i).Cells(colPayeeJointAcNo).Value
                gv.Rows(k).Cells(colPayeeJointIFSC).Value = gvInvoice.Rows(i).Cells(colPayeeJointIFSC).Value
                gv.Rows(k).Cells(colBankCode).Value = gvInvoice.Rows(i).Cells(colBankCode).Value
                gv.Rows(k).Cells(colBankDesc).Value = gvInvoice.Rows(i).Cells(colBankDesc).Value
                gv.Rows(k).Cells(colPayMode).Value = gvInvoice.Rows(i).Cells(colPayMode).Value
                gv.Rows(k).Cells(colChequeNo).Value = gvInvoice.Rows(i).Cells(colChequeNo).Value

                'Next
                gv.Rows(k).Cells(colMilkQty).Value = gvInvoice.Rows(i).Cells(colMilkQty).Value


                gv.Rows(k).Cells(colVSPAmount).Value = gvInvoice.Rows(i).Cells(colInvAmt).Value
                gv.Rows(k).Cells(colMPAmount).Value = calculateMPAmount(clsCommon.myCstr(gv.Rows(k).Cells(colVendorCode).Value), dtpFromDate.Value, dtpToDate.Value, fndLoc.Value)
                gv.Rows(k).Cells(colIncenAmt).Value = gvInvoice.Rows(i).Cells(colIncenAmt).Value
                gv.Rows(k).Cells(colIncenEmpAmt).Value = gvInvoice.Rows(i).Cells(colIncenEmpAmt).Value

                If clsCommon.myCdbl(gv.Rows(k).Cells(colMPAmount).Value) > 0 AndAlso clsCommon.myCdbl(gv.Rows(k).Cells(colMPAmount).Value) < clsCommon.myCdbl(gv.Rows(k).Cells(colVSPAmount).Value) Then
                    gv.Rows(k).Cells(colInvAmt).Value = gv.Rows(k).Cells(colMPAmount).Value
                    Dim dblVSPCharges As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Actual_charges from TSPL_VENDOR_MASTER where Form_Type='VSP' and Vendor_Code='" + clsCommon.myCstr(gv.Rows(k).Cells(colVendorCode).Value) + "'"))
                    gv.Rows(k).Cells(colEmpAmt).Value = Math.Round(clsCommon.myCdbl(gv.Rows(k).Cells(colMPAmount).Value) * dblVSPCharges / 100, 2, MidpointRounding.ToEven)
                    If Not isEmpOnAmtOnly Then
                        gv.Rows(k).Cells(colTotalEmp).Value = clsCommon.myCdbl(gv.Rows(k).Cells(colIncenEmpAmt).Value) + clsCommon.myCdbl(gv.Rows(k).Cells(colEmpAmt).Value)
                    Else
                        gv.Rows(k).Cells(colTotalEmp).Value = clsCommon.myCdbl(gv.Rows(k).Cells(colEmpAmt).Value)
                    End If
                    gv.Rows(k).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value = clsCommon.myCdbl(gv.Rows(k).Cells(colInvAmt).Value) + clsCommon.myCdbl(gv.Rows(k).Cells(colTotalEmp).Value)
                Else
                    gv.Rows(k).Cells(colInvAmt).Value = gvInvoice.Rows(i).Cells(colInvAmt).Value
                    gv.Rows(k).Cells(colEmpAmt).Value = gvInvoice.Rows(i).Cells(colEmpAmt).Value
                    If Not isEmpOnAmtOnly Then
                        gv.Rows(k).Cells(colTotalEmp).Value = clsCommon.myCdbl(gv.Rows(k).Cells(colIncenEmpAmt).Value) + clsCommon.myCdbl(gv.Rows(k).Cells(colEmpAmt).Value)
                    Else
                        gv.Rows(k).Cells(colTotalEmp).Value = clsCommon.myCdbl(gv.Rows(k).Cells(colEmpAmt).Value)
                    End If
                    gv.Rows(k).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value = gvInvoice.Rows(i).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value
                End If
                If clsCommon.myCdbl(gv.Rows(k).Cells(colMPAmount).Value) > 0 Then
                    gv.Rows(k).Cells(colMPVSPDiffAmount).Value = Math.Round(clsCommon.myCdbl(gv.Rows(k).Cells(colMPAmount).Value) - clsCommon.myCdbl(gv.Rows(k).Cells(colVSPAmount).Value), 2, MidpointRounding.AwayFromZero)
                End If
                gv.Rows(k).Cells(colActualVSPCode).Value = gvInvoice.Rows(i).Cells(colActualVSPCode).Value
                gv.Rows(k).Cells(colActualVSPName).Value = gvInvoice.Rows(i).Cells(colActualVSPName).Value
                gv.Rows(k).Cells(colVSPOwnSystemAmt).Value = gvInvoice.Rows(i).Cells(colVSPOwnSystemAmt).Value
                gv.Rows(k).Cells(colHeadLoadAmt).Value = gvInvoice.Rows(i).Cells(colHeadLoadAmt).Value
                gv.Rows(k).Cells(colInvDeduc).Value = gvInvoice.Rows(i).Cells(colInvDeduc).Value
                gvInvoice.Rows(i).Cells(colReduceDeduc).Value = getTotalDeductionReduceDeduSum(gvInvoice.Rows(i).Cells(colVendorCode).Value) + getTotalMccSaleReduceDeduSum(gvInvoice.Rows(i).Cells(colVendorCode).Value) + getTotalItemIssueReduceDeduSum(gvInvoice.Rows(i).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colReduceDeduc).Value = gvInvoice.Rows(i).Cells(colReduceDeduc).Value
                gv.Rows(k).Cells(colMccSaleTotalAmount).Value = getTotalMccSaleSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colMccSaleReturnTotalAmount).Value = getTotalMccSaleReturnSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colTotalCreditNoteAmount).Value = getTotalCreditNoteSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colItemIssueTotalAmount).Value = getTotalItemIssueSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colItemIssueReturnTotalAmount).Value = getTotalItemIssueReturnSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colDeductionTotalAmount).Value = getTotalDeductionSum(gv.Rows(k).Cells(colVendorCode).Value)
                gv.Rows(k).Cells(colServiceChargeAmt).Value = clsCommon.myCdbl(gvInvoice.Rows(i).Cells(colServiceChargeAmt).Value)
                gv.Rows(k).Cells(colPaybleAmt).Value = (((gv.Rows(k).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.Rows(k).Cells(colTotalCreditNoteAmount).Value + gv.Rows(k).Cells(colVSPOwnSystemAmt).Value + gv.Rows(k).Cells(colHeadLoadAmt).Value) - gv.Rows(k).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(k).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(k).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(k).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(k).Cells(colReduceDeduc).Value) + getTotalMccSaleReturnSum(gv.Rows(k).Cells(colVendorCode).Value) + getTotalItemIssueReturnSum(gv.Rows(k).Cells(colVendorCode).Value)

                If isConsiderAdvancePayment Then
                    Dim totalPayableAmt As Double = getTotalAdvancePayment(clsCommon.myCstr(gv.Rows(k).Cells(colVendorCode).Value))
                    If totalPayableAmt > 0 Then
                        gv.Rows(k).Cells(colAdvanceAmount).Value = totalPayableAmt
                        If clsCommon.myCdbl(gv.Rows(k).Cells(colAdvanceAmount).Value) > clsCommon.myCdbl(gv.Rows(k).Cells(colPaybleAmt).Value) Then
                            gv.Rows(k).Cells(colAdvanceKnockOffAmount).Value = Math.Round(clsCommon.myCdbl(gv.Rows(k).Cells(colPaybleAmt).Value), 2)
                            gv.Rows(k).Cells(colPaybleAmt).Value = 0
                        ElseIf clsCommon.myCdbl(gv.Rows(k).Cells(colAdvanceAmount).Value) < clsCommon.myCdbl(gv.Rows(k).Cells(colPaybleAmt).Value) Then
                            gv.Rows(k).Cells(colPaybleAmt).Value = Math.Round(clsCommon.myCdbl(gv.Rows(k).Cells(colPaybleAmt).Value) - clsCommon.myCdbl(gv.Rows(k).Cells(colAdvanceAmount).Value), 2, MidpointRounding.ToEven)
                            gv.Rows(k).Cells(colAdvanceKnockOffAmount).Value = Math.Round(clsCommon.myCdbl(gv.Rows(k).Cells(colAdvanceAmount).Value), MidpointRounding.ToEven)
                        ElseIf clsCommon.myCdbl(gv.Rows(k).Cells(colAdvanceAmount).Value) = clsCommon.myCdbl(gv.Rows(k).Cells(colPaybleAmt).Value) Then
                            gv.Rows(k).Cells(colPaybleAmt).Value = 0
                            gv.Rows(k).Cells(colAdvanceKnockOffAmount).Value = Math.Round(clsCommon.myCdbl(gv.Rows(k).Cells(colAdvanceAmount).Value), 2, MidpointRounding.ToEven)
                        End If
                    End If
                End If
            Next
        End If
        ReStoreGridLayout()
        gv.BestFitColumns(BestFitColumnMode.AllCells)
        gv.AllowDeleteRow = False
        gv.AllowRowResize = False
        gv.AllowEditRow = True
        gv.AllowAddNewRow = False
        gv.ShowFilteringRow = True
        gv.ShowGroupPanel = False


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        gv.SummaryRowsBottom.Clear()

        For iii As Integer = 0 To gv.Columns.Count - 1
            If TypeOf (gv.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gv.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        'Dim item1 As New GridViewSummaryItem(colMilkQty, "{0:F2}", GridAggregateFunction.Sum)

        'summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem(colInvAmt, "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        'Dim item3 As New GridViewSummaryItem(colInvAndEmpAmt, "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)
        'Dim item4 As New GridViewSummaryItem(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt, "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)
        'Dim item5 As New GridViewSummaryItem(colVSPOwnSystemAmt, "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item5)
        'Dim item6 As New GridViewSummaryItem(colHeadLoadAmt, "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item6)
        'Dim item7 As New GridViewSummaryItem(colReduceDeduc, "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item7)
        'Dim item8 As New GridViewSummaryItem(colMccSaleTotalAmount, "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item8)
        'Dim item9 As New GridViewSummaryItem(colItemIssueTotalAmount, "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item9)
        'Dim item10 As New GridViewSummaryItem(colDeductionTotalAmount, "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item10)
        'Dim item11 As New GridViewSummaryItem(colPaybleAmt, "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item11)


        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        isCellValueChanged = False
    End Sub

    Private Function calculateMPAmount(ByVal strVSPCode As String, ByVal dtFrom As DateTime, ByVal dtTo As DateTime, ByVal strMCC As String) As Double
        Dim retVAL As Double = 0
        Dim qry As String = "select 1 from TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_VLC_MAPPING_FOR_MP_MILK_AMOUNT.VLC_Code where TSPL_VLC_MASTER_HEAD.VSP_Code='" + strVSPCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            qry = "select sum(Amount) as Amount from (" + Environment.NewLine + _
            " Select TSPL_VLC_DATA_UPLOADER.Amount " + Environment.NewLine + _
            " from TSPL_VLC_DATA_UPLOADER " + Environment.NewLine + _
            " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE and TSPL_VLC_MASTER_HEAD.MCC=TSPL_VLC_DATA_UPLOADER.MCC_Code" + Environment.NewLine + _
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VLC_MASTER_HEAD.MCC " + Environment.NewLine + _
            " where File_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm tt") + "' and File_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_LOCATION_MASTER.Loc_Segment_Code='" + strMCC + "' and TSPL_VLC_MASTER_HEAD.VSP_Code='" + strVSPCode + "' " + Environment.NewLine + _
            " union all" + Environment.NewLine + _
            " select  TSPL_VLC_DATA_UPLOADER_DETAIL.Amount from TSPL_VLC_DATA_UPLOADER_DETAIL" + Environment.NewLine + _
            " left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code=TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code" + Environment.NewLine + _
            " left Outer Join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code= TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code" + Environment.NewLine + _
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VLC_MASTER_HEAD.MCC" + Environment.NewLine + _
            " where TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtFrom), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtTo), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_LOCATION_MASTER.Loc_Segment_Code='" + strMCC + "'" + Environment.NewLine + _
            " and TSPL_VLC_MASTER_HEAD.VSP_Code='" + strVSPCode + "'" + Environment.NewLine + _
            " )xxx"
            retVAL = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)), 2, MidpointRounding.ToEven)
        End If
        Return retVAL
    End Function


    Private Sub fndLoc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Dim whrCls As String = " 1=1 "
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
            End If
        End If

        whrCls = whrCls & " and   Rejected_Type='N' and Location_Category='MCC'"
        fndLoc.Value = clsLocation.getLocSegFinder(whrCls, fndLoc.Value, isButtonClicked)
        txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndLoc.Value & "' "))
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            ' fndLoc.Enabled = False
            ' txtLocName.Enabled = False

            If Not isLoad Then
                Dim PaymentCycleType As String = ""
                Dim PaymentCycleValue As Integer = 0
                ' If Not isLoad Then
                If clsCommon.myLen(fndLoc.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Please select the Location first")
                    Exit Sub
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select isnull(TSPL_MCC_MASTER.empOnAmountOnly,0) as empOnAmountOnly,TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code =(select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No Payment Cycle found on current MCC/Location")
                    Exit Sub
                End If
                PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
                PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
                isEmpOnAmtOnly = IIf(clsCommon.myCdbl(dt.Rows(0)("empOnAmountOnly")) = 0, False, True)
                If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then

                    If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                        clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        Exit Sub
                    End If
                    dtpToDate.Value = DateAdd(DateInterval.Day, PaymentCycleValue - 1, dtpFromDate.Value)
                    If DatePart(DateInterval.Month, dtpFromDate.Value) <> DatePart(DateInterval.Month, dtpToDate.Value) Then
                        dtpToDate.Value = DateAdd(DateInterval.Month, 1, clsCommon.myCDate("01/" & DatePart(DateInterval.Month, dtpFromDate.Value) & "/" & DatePart(DateInterval.Year, dtpFromDate.Value)))
                        dtpToDate.Value = DateAdd(DateInterval.Day, -1, dtpToDate.Value)

                    End If
                ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                        clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        Exit Sub
                    End If
                    dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
                ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                        clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                        Exit Sub
                    End If
                    dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
                End If
                ' End If
            End If
        End If
    End Sub

    Private Sub gvInvoice_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvInvoice.CellValueChanged
        If Not isLoad AndAlso e.Column Is gvInvoice.Columns(colSelect) Then
            isLoad = True
            getVendors()
            LoadDeductionGridData()
            LoadMccSaleGridData()
            LoadMccSaleReturnGridData()
            LoadItemIssueGridData()
            LoadCreditNoteGridData()
            'LoadBlankGridGV()
            'loadGvData()
            isLoad = False
        End If
    End Sub

    Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        PaymentProcess()
    End Sub

    Private Sub fndDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNo._MYValidating

        fndDocNo.Value = clsPaymentProcessHead.getFinder("", fndDocNo.Value, isButtonClicked)
        If clsCommon.myLen(fndDocNo.Value) > 0 Then
            LoadData(fndDocNo.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub dtpFromDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpFromDate.ValueChanged
    End Sub

    Function getInvoiceRowNo(ByVal vsp As String) As Integer
        Dim rvalue As Integer = 0
        Try
            If gvInvoice IsNot Nothing AndAlso gvInvoice.Rows.Count > 0 And clsCommon.myLen(vsp) > 0 Then
                For i As Integer = 0 To gvInvoice.Rows.Count - 1
                    If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then
                        rvalue = i
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            rvalue = -1
        End Try
        Return rvalue
    End Function

    Function getMainRowNo(ByVal vsp As String) As Integer
        Dim rvalue As Integer = 0
        Try
            If gv IsNot Nothing AndAlso gv.Rows.Count > 0 And clsCommon.myLen(vsp) > 0 Then
                For i As Integer = 0 To gv.Rows.Count - 1
                    If clsCommon.CompairString(gv.Rows(i).Cells(colVendorCode).Value, vsp) = CompairStringResult.Equal Then
                        rvalue = i
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            rvalue = -1
        End Try
        Return rvalue
    End Function

    Private Sub gvDeduction_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvDeduction.CellValueChanged
        If e.Column Is gvDeduction.Columns(colReduceDeduc) Then
            Dim Rownum As Integer = 0
            Dim rownummain As Integer = 0
            Try
                If gvDeduction.CurrentRow.Cells(colReduceDeduc).Value > gvDeduction.CurrentRow.Cells(colItemAmt).Value Then
                    clsCommon.MyMessageBoxShow("Reduce Deduction can not be more than invoice amount ")
                    gvDeduction.CurrentRow.Cells(colReduceDeduc).Value = 0
                    Exit Sub
                End If
                Rownum = getInvoiceRowNo(gvDeduction.CurrentRow.Cells(colVendorCode).Value)
                rownummain = getMainRowNo(gvDeduction.CurrentRow.Cells(colVendorCode).Value)
                If Rownum <> -1 Then
                    gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value = getTotalDeductionReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalMccSaleReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalItemIssueReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value)

                    If rownummain <> -1 Then
                        gv.Rows(rownummain).Cells(colReduceDeduc).Value = gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value
                        gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value)
                    End If
                End If
            Catch ex As Exception
            End Try
        ElseIf e.Column Is gvDeduction.Columns(colSelect) Then
            If Not isLoad Then
                isLoad = True
                loadGvData()
                isLoad = False
            End If
        End If
    End Sub

    Private Sub gvCreditNote_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCreditNote.CellValueChanged
        If e.Column Is gvCreditNote.Columns(colSelect) Then
            If Not isLoad Then
                isLoad = True
                loadGvData()
                isLoad = False
            End If
        End If
    End Sub

    Private Sub gvMCCSale_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvMccSale.CellValueChanged
        If e.Column Is gvMccSale.Columns(colReduceDeduc) Then
            Dim Rownum As Integer = 0
            Dim rownummain As Integer = 0
            Try
                If gvMccSale.CurrentRow.Cells(colReduceDeduc).Value > gvMccSale.CurrentRow.Cells(colItemAmt).Value Then
                    clsCommon.MyMessageBoxShow("Reduce Deduction can not be more than invoice amount ")
                    gvMccSale.CurrentRow.Cells(colReduceDeduc).Value = 0
                    Exit Sub
                End If
                Rownum = getInvoiceRowNo(gvMccSale.CurrentRow.Cells(colCustomerCode).Value)
                rownummain = getMainRowNo(gvMccSale.CurrentRow.Cells(colCustomerCode).Value)
                If Rownum <> -1 Then
                    gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value = getTotalDeductionReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalMccSaleReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalItemIssueReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value)
                    If rownummain <> -1 Then
                        gv.Rows(rownummain).Cells(colReduceDeduc).Value = gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value
                        gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value)
                    End If
                End If
            Catch ex As Exception
            End Try
        ElseIf e.Column Is gvMccSale.Columns(colSelect) Then
            If Not isLoad Then
                isLoad = True
                loadGvData()
                isLoad = False
            End If
        End If
    End Sub

    Private Sub gvItemIssue_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItemIssue.CellValueChanged
        If e.Column Is gvItemIssue.Columns(colReduceDeduc) Then
            Dim Rownum As Integer = 0
            Dim rownummain As Integer = 0
            Try
                If Not isCellValueChanged Then
                    isCellValueChanged = True
                    If gvItemIssue.CurrentRow.Cells(colReduceDeduc).Value > gvItemIssue.CurrentRow.Cells(colItemAmt).Value Then
                        clsCommon.MyMessageBoxShow("Item Issue : Reduce Deduction can not be more than invoice amount at line no: " & (e.RowIndex + 1) & "")
                        gvItemIssue.CurrentRow.Cells(colReduceDeduc).Value = 0
                        isCellValueChanged = False
                        Exit Sub
                    End If
                    Rownum = getInvoiceRowNo(gvItemIssue.CurrentRow.Cells(colVendorCode).Value)
                    rownummain = getMainRowNo(gvItemIssue.CurrentRow.Cells(colVendorCode).Value)
                    If Rownum <> -1 Then
                        gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value = getTotalDeductionReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalMccSaleReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalItemIssueReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value)
                        If rownummain <> -1 Then
                            gv.Rows(rownummain).Cells(colReduceDeduc).Value = gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value
                            gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value)
                        End If
                    End If
                    isCellValueChanged = False
                End If

            Catch ex As Exception
            End Try
        ElseIf e.Column Is gvItemIssue.Columns(colSelect) Then
            If Not isLoad Then
                isLoad = True
                loadGvData()
                isLoad = False
            End If
        End If
        isCellValueChanged = False
    End Sub

    Private Sub gvMccsaleReturn_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles GvMccSaleReturn.CellValueChanged
        If e.Column Is GvMccSaleReturn.Columns(colReduceDeduc) Then
            Dim Rownum As Integer = 0
            Dim rownummain As Integer = 0
            Try
                'If GvMccSaleReturn.CurrentRow.Cells(colReduceDeduc).Value > GvMccSaleReturn.CurrentRow.Cells(colItemAmt).Value Then
                '    clsCommon.MyMessageBoxShow("Reduce Deduction can not be more than invoice amount ")
                '    GvMccSaleReturn.CurrentRow.Cells(colReduceDeduc).Value = 0
                '    Exit Sub
                'End If
                Rownum = getInvoiceRowNo(GvMccSaleReturn.CurrentRow.Cells(colCustomerCode).Value)
                rownummain = getMainRowNo(GvMccSaleReturn.CurrentRow.Cells(colCustomerCode).Value)
                If Rownum <> -1 Then
                    gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value = getTotalDeductionReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalMccSaleReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalItemIssueReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value)
                    If rownummain <> -1 Then
                        gv.Rows(rownummain).Cells(colReduceDeduc).Value = gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value
                        gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value)
                    End If
                End If
            Catch ex As Exception
            End Try
        ElseIf e.Column Is GvMccSaleReturn.Columns(colSelect) Then
            If Not isLoad Then
                isLoad = True
                loadGvData()
                isLoad = False
            End If
        End If
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        If gv IsNot Nothing AndAlso gv.Rows.Count > 0 AndAlso gv.CurrentRow.Index >= 0 Then
            If e.Column Is gv.Columns(colMccSaleTotalAmount) Then
                Dim frm As New FrmFreeGrid
                frm.dt = getMccSaleList(gv.CurrentRow.Cells(colVendorCode).Value)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No data Found")
                    Exit Sub
                End If
                frm.strFormName = "MCC Sale List For VSP: " & gv.CurrentRow.Cells(colVendorCode).Value
                frm.ReportID = "MCCSaleDetail"
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If

            If e.Column Is gv.Columns(colMccSaleReturnTotalAmount) Then
                Dim frm As New FrmFreeGrid
                frm.dt = getMccSalereturnList(gv.CurrentRow.Cells(colVendorCode).Value)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No data Found")
                    Exit Sub
                End If
                frm.strFormName = "MCC Sale Return List For VSP: " & gv.CurrentRow.Cells(colVendorCode).Value
                frm.ReportID = "MCCSaleReturnDetail"
                frm.WindowState = FormWindowState.Maximized
                frm.Show()
            End If

            If e.Column Is gv.Columns(colItemIssueTotalAmount) Then
                Dim frm As New FrmFreeGrid
                frm.dt = getItemIssueList(gv.CurrentRow.Cells(colVendorCode).Value)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No data Found")
                    Exit Sub
                End If
                frm.strFormName = "Item Issue List For VSP: " & gv.CurrentRow.Cells(colVendorCode).Value
                frm.WindowState = FormWindowState.Maximized
                frm.ReportID = "ItemIssueDetail"
                frm.Show()
            End If
            If e.Column Is gv.Columns(colItemIssueReturnTotalAmount) Then
                Dim frm As New FrmFreeGrid
                frm.dt = getItemIssueList(gv.CurrentRow.Cells(colVendorCode).Value)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No data Found")
                    Exit Sub
                End If
                frm.strFormName = "Item Issue Return List For VSP: " & gv.CurrentRow.Cells(colVendorCode).Value
                frm.WindowState = FormWindowState.Maximized
                frm.ReportID = "ItemIssueDetail"
                frm.Show()
            End If

            If e.Column Is gv.Columns(colDeductionTotalAmount) Then
                Dim frm As New FrmFreeGrid
                frm.dt = getDeductionList(gv.CurrentRow.Cells(colVendorCode).Value)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No data Found")
                    Exit Sub
                End If
                frm.strFormName = "Deduction List For VSP: " & gv.CurrentRow.Cells(colVendorCode).Value
                frm.WindowState = FormWindowState.Maximized
                frm.ReportID = "DeductionDetail"
                frm.Show()
            End If

            If e.Column Is gv.Columns(colTotalCreditNoteAmount) Then
                Dim frm As New FrmFreeGrid
                frm.dt = getCreditNoteList(gv.CurrentRow.Cells(colVendorCode).Value)
                If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No data Found")
                    Exit Sub
                End If
                frm.strFormName = "Credit Note List For VSP: " & gv.CurrentRow.Cells(colVendorCode).Value
                frm.WindowState = FormWindowState.Maximized
                frm.ReportID = "CreditNoteDetail"
                frm.Show()
            End If

            If e.Column Is gv.Columns(colAPInvoiceNo) Then
                If clsCommon.myLen(gv.CurrentRow.Cells(colAPInvoiceNo).Value) > 0 Then
                    Dim frm As New FrmAPInvoiceEntry
                    frm.strAPInvoice = gv.CurrentRow.Cells(colAPInvoiceNo).Value
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()
                End If
            End If
            If e.Column Is gv.Columns(colPurchaseInvoiceNo) Then
                If clsCommon.myLen(gv.CurrentRow.Cells(colPurchaseInvoiceNo).Value) > 0 Then
                    Dim frm As New frmMilkPurchaseInvoiceMCC
                    frmMilkPurchaseInvoiceMCC.strDocumentNo = gv.CurrentRow.Cells(colPurchaseInvoiceNo).Value
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()
                End If
            End If

        End If
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isLoad Then
            isLoad = True
            If (e.Column Is gv.Columns(colReduceDeduc) OrElse e.Column Is gv.Columns(colMccSaleTotalAmount) OrElse e.Column Is gv.Columns(colMccSaleReturnTotalAmount) OrElse e.Column Is gv.Columns(colItemIssueTotalAmount) OrElse e.Column Is gv.Columns(colItemIssueReturnTotalAmount) OrElse e.Column Is gv.Columns(colDeductionTotalAmount)) AndAlso gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
                gv.CurrentRow.Cells(colPaybleAmt).Value = (((gv.CurrentRow.Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.CurrentRow.Cells(colTotalCreditNoteAmount).Value + gv.CurrentRow.Cells(colVSPOwnSystemAmt).Value + gv.CurrentRow.Cells(colHeadLoadAmt).Value) - gv.CurrentRow.Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.CurrentRow.Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.CurrentRow.Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.CurrentRow.Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.CurrentRow.Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.CurrentRow.Cells(colVendorCode).Value)
            End If
            isLoad = False
            isLoad = True
            If e.Column Is gv.Columns(colSelect) Then
                If gv.CurrentRow.Cells(colSelect).Value = True Then
                    CheckInvoice(gv.CurrentRow.Cells(colAPInvoiceNo).Value)
                Else
                    UncheckInvoice(gv.CurrentRow.Cells(colAPInvoiceNo).Value)
                End If
            End If
            isLoad = False
        End If
        If Not isLoad Then
            ' isCellValueChanged = False
            If Not isCellValueChanged Then
                isCellValueChanged = True
                If e.Column Is gv.Columns(colBankCode) And e.RowIndex >= 0 Then
                    isCellValueChanged = True
                    openBankcode()
                    isCellValueChanged = False
                End If
                If e.Column Is gv.Columns(colPayMode) And e.RowIndex >= 0 Then
                    isCellValueChanged = True
                    openPaymentMode()
                    isCellValueChanged = False
                End If
            End If
            'isCellValueChanged = False
            Dim Rownum As Integer = getInvoiceRowNo(gv.CurrentRow.Cells(colVendorCode).Value)
            If Rownum <> -1 Then
                If e.Column Is gv.Columns(colBankCode) Then
                    gvInvoice.Rows(Rownum).Cells(colBankCode).Value = gv.CurrentRow.Cells(colBankCode).Value
                End If
                If e.Column Is gv.Columns(colBankDesc) Then
                    gvInvoice.Rows(Rownum).Cells(colBankDesc).Value = gv.CurrentRow.Cells(colBankDesc).Value
                End If
                If e.Column Is gv.Columns(colPayMode) Then
                    gvInvoice.Rows(Rownum).Cells(colPayMode).Value = gv.CurrentRow.Cells(colPayMode).Value
                End If
                If e.Column Is gv.Columns(colChequeNo) Then
                    gvInvoice.Rows(Rownum).Cells(colChequeNo).Value = gv.CurrentRow.Cells(colChequeNo).Value
                End If
            End If
        End If

    End Sub

    Sub openBankcode()
        Dim strWhrclas As String = ""
        Dim Qry As String = clsERPFuncationality.glbankqueryNew(strWhrclas)
        gv.CurrentRow.Cells(colBankCode).Value = clsCommon.ShowSelectForm("BankSlctr@Payment", Qry, "Code", strWhrclas, gv.CurrentRow.Cells(colBankCode).Value, "Code", True)
        gv.CurrentRow.Cells(colBankDesc).Value = connectSql.RunScalar("select description from TSPL_BANK_MASTER where bank_code = '" + gv.CurrentRow.Cells(colBankCode).Value + "'")
        gv.CurrentRow.Cells(colPayMode).Value = ""
        gv.CurrentRow.Cells(colChequeNo).Value = ""
        gv.CurrentRow.Cells(colChequeDate).Value = Nothing


    End Sub

    Sub openPaymentMode()
        Dim strbankcode As String
        If Not String.IsNullOrEmpty(connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + gv.CurrentRow.Cells(colBankCode).Value + "'")) Then
            strbankcode = connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + gv.CurrentRow.Cells(colBankCode).Value + "'")
            If strbankcode.Trim() = "C" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                gv.CurrentRow.Cells(colPayMode).Value = clsCommon.ShowSelectForm("PaymentCode Selector1", Qry1, "PaymentMode", "PAYMENT_TYPE = 'CASH'", gv.CurrentRow.Cells(colPayMode).Value, "PaymentMode", True)
            ElseIf strbankcode.Trim() = "P" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                gv.CurrentRow.Cells(colPayMode).Value = clsCommon.ShowSelectForm("PaymentCode Selector2", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Petty Cash'", gv.CurrentRow.Cells(colPayMode).Value, "PaymentMode", True)
            ElseIf strbankcode = "B" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                gv.CurrentRow.Cells(colPayMode).Value = clsCommon.ShowSelectForm("PaymentCode Selector3", Qry1, "PaymentMode", "PAYMENT_TYPE IN ('Cheque', 'Other','NEFT','RTGS')", gv.CurrentRow.Cells(colPayMode).Value, "PaymentMode", True)
            Else
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                gv.CurrentRow.Cells(colPayMode).Value = clsCommon.ShowSelectForm("PaymentCode Selector4", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Other'", gv.CurrentRow.Cells(colPayMode).Value, "PaymentMode", True)
            End If
            If clsCommon.myLen(gv.CurrentRow.Cells(colPayMode).Value) > 0 Then
                If clsCommon.CompairString(gv.CurrentRow.Cells(colPayMode).Value, "Cheque") = CompairStringResult.Equal Then
                    gv.CurrentRow.Cells(colChequeNo).ReadOnly = False
                Else
                    gv.CurrentRow.Cells(colChequeNo).ReadOnly = True
                End If
                gv.CurrentRow.Cells(colChequeNo).Value = ""
                gv.CurrentRow.Cells(colChequeDate).Value = Nothing
            End If
        End If
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        CheckAll()
    End Sub

    Sub UnCheckAll()
        If gv IsNot Nothing AndAlso gv.ChildRows.Count > 0 Then
            For i As Integer = 0 To gv.ChildRows.Count - 1
                gv.ChildRows(i).Cells(colSelect).Value = False
                UncheckInvoice(gv.ChildRows(i).Cells(colAPInvoiceNo).Value)
            Next
        End If
    End Sub

    Sub CheckAll()
        If gv IsNot Nothing AndAlso gv.ChildRows.Count > 0 Then
            For i As Integer = 0 To gv.ChildRows.Count - 1
                gv.ChildRows(i).Cells(colSelect).Value = True
                CheckInvoice(gv.ChildRows(i).Cells(colAPInvoiceNo).Value)
            Next
        End If
    End Sub

    Private Sub btnUnselectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectAll.Click
        UnCheckAll()
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        If gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
            gv.Columns(colSelect).IsVisible = False
            Dim arr As List(Of String) = New List(Of String)
            arr.Add("Location  : " & fndLoc.Value & " ( " & txtLocName.Text & " ) ")
            arr.Add("Date Range : " & dtpFromDate.Value & " To " & dtpToDate.Value)
            arr.Add("VSP :" & clsCommon.GetMulcallStringWithComma(txtVSP.arrValueMember))
            clsCommon.MyExportToExcelGrid("Payment Process Details", gv, arr, "Payment Process")
            gv.Columns(colSelect).IsVisible = True
        Else
            clsCommon.MyMessageBoxShow("No Data Found to export")
        End If
    End Sub

    Function getVLCUploaderCode(vsp_code As String, Optional trans As SqlTransaction = Nothing) As String
        Dim rValue As String = String.Empty
        Dim qry As String = String.Empty
        Try
            qry = "select VLC_Code_VLC_Uploader  from TSPL_VLC_MASTER_HEAD  where VSP_Code ='" & vsp_code & "'"
            rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Sub UncheckInvoice(ByVal strApInvoiceNo As String)
        If gvInvoice IsNot Nothing AndAlso gvInvoice.Rows.Count > 0 AndAlso clsCommon.myLen(strApInvoiceNo) > 0 Then
            For i As Integer = 0 To gvInvoice.Rows.Count - 1
                If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value, strApInvoiceNo) = CompairStringResult.Equal Then
                    gvInvoice.Rows(i).Cells(colSelect).Value = False
                    '' done by Panch Raj against ticket no:BM00000008937
                    '' unselect mcc sale trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvMccSale.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next
                    '' unselect mcc sale return trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In GvMccSaleReturn.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next

                    '' unselect mcc Item Issue trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvItemIssue.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next

                    '' unselect mcc Item Issue trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvItemIssueReturn.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next

                    '' unselect mcc Deduction trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvDeduction.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next
                    '' unselect mcc Credit trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvCreditNote.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = False
                        End If
                    Next

                End If
            Next
        End If
    End Sub

    Sub CheckInvoice(ByVal strApInvoiceNo As String)
        If gvInvoice IsNot Nothing AndAlso gvInvoice.Rows.Count > 0 AndAlso clsCommon.myLen(strApInvoiceNo) > 0 Then
            For i As Integer = 0 To gvInvoice.Rows.Count - 1
                If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colAPInvoiceNo).Value, strApInvoiceNo) = CompairStringResult.Equal Then
                    gvInvoice.Rows(i).Cells(colSelect).Value = True
                    '' done by Panch Raj against ticket no:BM00000008937
                    '' unselect mcc sale trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvMccSale.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next
                    '' unselect mcc sale return trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In GvMccSaleReturn.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colCustomerCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next

                    '' unselect mcc Item Issue trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvItemIssue.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next
                    '' unselect mcc Item Issue trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvItemIssueReturn.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next
                    '' unselect mcc Deduction trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvDeduction.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next
                    '' unselect mcc Credit trans for unseleceted vendor 
                    For Each grow As GridViewRowInfo In gvCreditNote.Rows
                        If clsCommon.CompairString(gvInvoice.Rows(i).Cells(colVendorCode).Value, grow.Cells(colVendorCode).Value) = CompairStringResult.Equal Then
                            grow.Cells(colSelect).Value = True
                        End If
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub gvItemIssueReturnReturn_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvItemIssueReturn.CellValueChanged
        'If e.Column Is gvItemIssueReturn.Columns(colReduceDeduc) Then
        '    Dim Rownum As Integer = 0
        '    Dim rownummain As Integer = 0
        '    Try
        '        If Not isCellValueChanged Then
        '            isCellValueChanged = True
        '            If gvItemIssueReturn.CurrentRow.Cells(colReduceDeduc).Value > gvItemIssueReturn.CurrentRow.Cells(colItemAmt).Value Then
        '                clsCommon.MyMessageBoxShow("Item Issue Return : Reduce Deduction can not be more than invoice amount at line no: " & (e.RowIndex + 1) & "")
        '                gvItemIssueReturn.CurrentRow.Cells(colReduceDeduc).Value = 0
        '                isCellValueChanged = False
        '                Exit Sub
        '            End If
        '            Rownum = getInvoiceRowNo(gvItemIssueReturn.CurrentRow.Cells(colVendorCode).Value)
        '            rownummain = getMainRowNo(gvItemIssueReturn.CurrentRow.Cells(colVendorCode).Value)
        '            If Rownum <> -1 Then
        '                gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value = getTotalDeductionReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalMccSaleReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value) + getTotalItemIssueReduceDeduSum(gvInvoice.Rows(Rownum).Cells(colVendorCode).Value)
        '                If rownummain <> -1 Then
        '                    gv.Rows(rownummain).Cells(colReduceDeduc).Value = gvInvoice.Rows(Rownum).Cells(colReduceDeduc).Value
        '                    gv.Rows(rownummain).Cells(colPaybleAmt).Value = (((gv.Rows(rownummain).Cells(colInvAndEMPAmtAndIncenAmtAndIncenEmpAmt).Value + gv.Rows(rownummain).Cells(colTotalCreditNoteAmount).Value + gv.Rows(rownummain).Cells(colVSPOwnSystemAmt).Value + gv.Rows(rownummain).Cells(colHeadLoadAmt).Value) - gv.Rows(rownummain).Cells(colInvDeduc).Value) - (getTotalDeductionSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalItemIssueSum(gv.Rows(rownummain).Cells(colVendorCode).Value) + getTotalMccSaleSum(gv.Rows(rownummain).Cells(colVendorCode).Value))) + clsCommon.myCdbl(gv.Rows(rownummain).Cells(colReduceDeduc).Value) + getTotalItemIssueReturnSum(gv.Rows(rownummain).Cells(colVendorCode).Value)
        '                End If
        '            End If
        '            isCellValueChanged = False
        '        End If

        '    Catch ex As Exception
        '    End Try
        'Else
        If e.Column Is gvItemIssueReturn.Columns(colSelect) Then
            If Not isLoad Then
                isLoad = True
                loadGvData()
                isLoad = False
            End If
        End If
        isCellValueChanged = False
    End Sub

    Sub SetToDate()
        If Not isLoad Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select the Location first")
                Exit Sub
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code =(select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Payment Cycle found on current MCC/Location")
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If dtpFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    dtpFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    dtpToDate.Value = dtpFromDate.Value
                    Exit Sub
                End If
                dtpToDate.Value = dtpFromDate.Value.AddDays(PaymentCycleValue - 1)

                If dtpFromDate.Value.Month <> dtpToDate.Value.Month Then
                    dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = dtpToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If dtpFromDate.Value.Month <> dtNxtPay.Month Then
                    dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
            End If
            ' End If
        End If
    End Sub

    Private Sub dtpFromDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpFromDate.Validating
        SetToDate()
    End Sub

    Private Sub dtpFromDate_Leave(sender As Object, e As EventArgs) Handles dtpFromDate.Leave
        SetToDate()
    End Sub

    Private Sub txtVSP__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                fndLoc.Focus()
                Throw New Exception("Please select MCC")
            End If

            'Dim qry As String = "select xx.VSP_CODE as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,xx.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name from (" + Environment.NewLine +
            '" select VSP_CODE,max(VLC_CODE)as VLC_CODE from (" + Environment.NewLine + _
            '" select VSP_CODE,VLC_CODE from TSPL_MILK_SRN_Head left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_SRN_Head.MCC_CODE where TSPL_LOCATION_MASTER.Loc_Segment_Code ='" + fndLoc.Value + "'"
            'If Not isPickPendingMilkSRNinNextPaymentCycle Then
            '    qry += " and DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "
            'End If
            'qry += " and DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine + _
            '" union all " + Environment.NewLine + _
            '" select VSP_CODE,VLC_CODE as VLC_CODE from TSPL_MILK_REJECT_DETAIL left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_REJECT_HEAD.MCC_CODE where TSPL_MILK_REJECT_HEAD.Posted=1 and TSPL_LOCATION_MASTER.Loc_Segment_Code ='" + fndLoc.Value + "'"
            'If Not isPickPendingMilkSRNinNextPaymentCycle Then
            '    qry += " and TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
            'End If
            'qry += " and TSPL_MILK_REJECT_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine + _
            '" )xxx group by VSP_CODE " + Environment.NewLine + _
            '" )xx " + Environment.NewLine +
            '" left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.VSP_CODE " + Environment.NewLine +
            '" left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=xx.VLC_CODE " + Environment.NewLine +
            '" where TSPL_VENDOR_MASTER.is_Hold_Payment_Process=0 " + Environment.NewLine +
            '" order by xx.VSP_CODE"


            Dim qry As String = "select xx.VSP_CODE as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,xx.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name, xx.MCC_CODE  from (" + Environment.NewLine +
         " select VSP_CODE,max(VLC_CODE)as VLC_CODE,max(MCC_CODE ) MCC_CODE from (" + Environment.NewLine + _
         " select VSP_CODE,VLC_CODE,MCC_CODE from TSPL_MILK_SRN_Head left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_SRN_Head.MCC_CODE where TSPL_LOCATION_MASTER.Loc_Segment_Code ='" + fndLoc.Value + "'"
            If Not isPickPendingMilkSRNinNextPaymentCycle Then
                qry += " and DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "
            End If
            qry += " and DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine + _
            " union all " + Environment.NewLine + _
            " select VSP_CODE,VLC_CODE as VLC_CODE,MCC_CODE  from TSPL_MILK_REJECT_DETAIL left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MILK_REJECT_HEAD.MCC_CODE where TSPL_MILK_REJECT_HEAD.Posted=1 and TSPL_LOCATION_MASTER.Loc_Segment_Code ='" + fndLoc.Value + "'"
            If Not isPickPendingMilkSRNinNextPaymentCycle Then
                qry += " and TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
            End If
            qry += " and TSPL_MILK_REJECT_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine + _
            " )xxx group by VSP_CODE " + Environment.NewLine + _
            " )xx " + Environment.NewLine +
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.VSP_CODE " + Environment.NewLine +
            " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=xx.VLC_CODE " + Environment.NewLine +
            " left outer join TSPL_VLC_MAPPING_FOR_MP_PAYMENT_PROCESS on TSPL_VLC_MAPPING_FOR_MP_PAYMENT_PROCESS.MCC_Code =xx.MCC_CODE and TSPL_VLC_MAPPING_FOR_MP_PAYMENT_PROCESS.VLC_Code =xx.VLC_CODE " + Environment.NewLine +
            " where TSPL_VENDOR_MASTER.is_Hold_Payment_Process=0  " + Environment.NewLine +
            " and TSPL_VLC_MAPPING_FOR_MP_PAYMENT_PROCESS.MCC_Code =xx.MCC_CODE and TSPL_VLC_MAPPING_FOR_MP_PAYMENT_PROCESS.VLC_Code =xx.VLC_CODE " + Environment.NewLine +
            " order by xx.VSP_CODE"

            txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm("PPfPVLF", qry, "Code", "", txtVSP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Function Load_Report(ByVal Location As String, ByVal dFromDate As Date, ByVal dToDate As Date, ByVal ListOfVSPs As ArrayList, ByVal otherCls As Boolean, ByVal PrintOpen As Boolean) As DataTable
        '======================preeti gupta ticket no []
        Dim sQuery As String
        Dim dtgv As New DataTable
        Dim companyADD, CompName, CompCode As String

        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        companyADD = dt1.Rows(0).Item("comp_address")

        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompName = dt2.Rows(0).Item("Comp_Name")


        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompCode = dt5.Rows(0).Item("Comp_Code")

        Dim fromDate As String = dtpFromDate.Value
        Dim Todate As String = dtpToDate.Value


        Dim whrcls As String = " where 2=2 "
        Dim whrcls1 As String = " where 2=2 "
        Dim whrclsItemWise As String = " where 2=2 "
        If otherCls Then

            whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dFromDate + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dToDate + "'),103) "
            whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(ListOfVSPs) & ")"
            If clsCommon.myLen(Location) > 0 Then
                whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN (" + Location + ") "
            End If

            whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dFromDate + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dToDate + "'),103) "
            whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(ListOfVSPs) & ")"
            If clsCommon.myLen(Location) > 0 Then
                whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN (" + Location + ") "
            End If
            whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dFromDate + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dToDate + "'),103) "

            whrclsItemWise += " and final.Customer_CODE in ( " & clsCommon.GetMulcallString(ListOfVSPs) & ")"
            whrclsItemWise += " and TSPL_PAYMENT_PROCESS_head.Loc_Seg_Code IN (" + Location + ")"


        Else
            whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
            whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
            If clsCommon.myLen(fndLoc.Value) > 0 Then
                whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN (" + fndLoc.Value + ") "
            End If

            whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
            whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
            whrcls1 += "  and TSPL_PAYMENT_PROCESS_Head.doc_no='" + fndDocNo.Value + "'"
            If clsCommon.myLen(fndLoc.Value) > 0 Then
                whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN (" + fndLoc.Value + ") "
            End If
            whrclsItemWise += " and final.doc_no in ('" + fndDocNo.Value + "')"
            whrclsItemWise += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
            'whrclsItemWise += " and final.Customer_CODE in  ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
            'whrclsItemWise += " and TSPL_PAYMENT_PROCESS_head.Loc_Seg_Code IN (" + fndLoc.Value + ")"
        End If
        sQuery = ""

        'sQuery += " select  '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate ,''  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,convert(varchar,DOC_DATE,103) as DOC_DATE,SHIFT,TYPE,SAMPLE_NO,convert(decimal(18,2),NewQty)as NewQty ,convert(decimal(18,1),FAT_PER) as FAT_PER ,convert(decimal(18,1),SNF_PER) as SNF_PER  ,convert(decimal(18,1),CLR)as CLR ,convert(decimal(18,2),(FAT_PER * NewQty)/100 )as TFAT,convert(decimal(18,2),(SNF_PER * NewQty)/100) as TSNF,convert(decimal(18,2),Amount/NewQty) as RATE  ,convert(decimal(18,2),Amount)as Amount ,MCC_CODE,VLC_Code ,VLC_Code_VLC_Uploader,emp,incentive,HEDAmt,AstAMT,DedAmt,coalesce(sale_AMt,0) as sale_AMt,VSP_CODE, ISNULL(Deduction_Debit_Amount,0) as Deduction_Debit_Amount, ISNULL(Deduction_MCC_Sale_Amount,0) as Deduction_MCC_Sale_Amount, ISNULL(Deduction_MCC_Sale_Return_Amount,0) as Deduction_MCC_Sale_Return_Amount, ISNULL(Deduction_Item_Issue_Amount,0) as Deduction_Item_Issue_Amount, ISNULL(Deduction_CREDIT_Amount,0) as Deduction_CREDIT_Amount,ISNULL(Issue_Return_Amount,0) as Issue_Return_Amount,Total_Basic_AMOUNT from( "
        'sQuery += " select addd,DOC_DATE,UOM_Code,Qty as NewQty, Qty,RATE,Net_AMOUNT as Amount ,MCC_CODE+' -'+MCC_NAME+'  QtyMode :'+UOM_Code    as MCC_CODE ,VSP_CODE ,SHIFT,ROUTE_CODE+' -'+Route_Name as ROUTE_CODE  ,Vendor_Name ,MCC_NAME,SAMPLE_NO ,TYPE ,CLR,FAT_PER ,SNF_PER ,VLC_Code_VLC_Uploader+' - '+VLC_Name as VLC_Code_VLC_Uploader, VLC_Code,emp,incentive,HEDAmt,AstAMT,DedAmt,sale_AMt,Deduction_Debit_Amount, Deduction_MCC_Sale_Amount, Deduction_MCC_Sale_Return_Amount, Deduction_Item_Issue_Amount, Deduction_CREDIT_Amount,Issue_Return_Amount,Total_Basic_AMOUNT from "
        sQuery += "select TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as MPD ,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as MPI_Date,   TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, "
        If otherCls Then
            sQuery += "    '" & dFromDate & "'  as fromDate ,'" & dToDate & "'  as Todate"
        Else
            sQuery += "    '" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate"
        End If
        sQuery += " ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,coalesce(PaymentProcess.Total_EMP_Amount,0) as Total_EMP_Amount,coalesce(PaymentProcess.Incentive_Amount,0) as Incentive_Amount ,coalesce(PaymentProcess.Incentive_EMP_Amount,0) as Incentive_EMP_Amount ,coalesce(PaymentProcess.EMP_Amount,0) as EMP_Amount ,coalesce(PaymentProcess.Vsp_Own_System_Amount,0) as Vsp_Own_System_Amount ,coalesce(PaymentProcess.Head_Load_Amount,0) as Head_Load_Amount ,coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0)*(-1) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_RECEIPT_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty  ,FAT_PER ,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as FATQTY,SNF_PER,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as SNFQTY "
        sQuery += " ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.RATE as RATE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT, TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,TSPL_MILK_SAMPLE_HEAD.SHIFT,"
        sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,TSPL_MILK_SAMPLE_DETAIL.TYPE ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
        sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt ,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT"
        sQuery += "  from TSPL_MILK_PURCHASE_INVOICE_DETAIL  Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE"
        sQuery += "   Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE    Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  "
        sQuery += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE   left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE Left Outer Join TSPL_VENDOR_MASTER On"
        sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'   Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_RECEIPT_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MCC_MASTER.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join TSPL_VLC_MASTER_HEAD on"
        sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  "
        sQuery += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code "
        sQuery += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code "
        sQuery += "  left join  (select VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount from (select TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL"
        sQuery += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
        sQuery += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
        sQuery += " ) as pp group by VSP_CODE,VLC_Code"
        sQuery += " ) as PaymentProcess on "
        sQuery += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_Code"
        sQuery += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code"

        sQuery += "  " & whrcls & " "
        sQuery += "order by vsp_code,convert(datetime,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)"

        '================================================= END ALL TYPE DEDUCTION=================================================
        'sQuery += " ) as yy left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_name='" & CompName & "'"
        'sQuery += "  order by convert(date,DOC_DATE,103)"
        Dim dt As New DataTable

        dt = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "  select Customer_CODE as VSP_Code,trans_type,Item_Code as VLC_Code_VLC_Uploader ,Item_Desc ,coalesce(Amount,0) as Amount  from  (select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Amount*(-1) as Amount  from TSPL_PAYMENT_PROCESS_MCC_SALE"
        sQuery += " left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no"
        sQuery += " left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code  "
        sQuery += "  union all"
        sQuery += " select 'MCC Sale Return' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.Doc_No ,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no,TSPL_SD_SALE_RETURN_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE_Return.amount as Paymnet_Amount ,TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt"
        sQuery += " from TSPL_PAYMENT_PROCESS_MCC_SALE_Return"
        sQuery += "  left join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE_Return.return_doc_no"
        sQuery += " left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SALE_RETURN_DETAIL.item_code "

        sQuery += "  union all"
        sQuery += " select 'Item Issue' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt*(-1) as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE "
        sQuery += " left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE.item_issue_doc_no"
        sQuery += " left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code "
        sQuery += "   union all"

        sQuery += " select 'Item Issue Return' as trans_type,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No ,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no,TSPL_VSPItem_DETAIL.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Vendor_code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.amount as Paymnet_Amount ,TSPL_VSPItem_DETAIL.Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN "
        sQuery += " left join TSPL_VSPItem_DETAIL on TSPL_VSPItem_DETAIL.doc_no=TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.item_issue_return_no"
        sQuery += " left join tspl_item_master on tspl_item_master.item_code=TSPL_VSPItem_DETAIL.item_code "

        sQuery += "   union all"

        sQuery += " select 'Deduction' as trans_type,TSPL_PAYMENT_PROCESS_DEDUCTION.doc_no,TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE,TSPL_PAYMENT_PROCESS_DEDUCTION.Ded_Desc ,TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE ,0 as Paymnet_Amount ,TSPL_PAYMENT_PROCESS_DEDUCTION.Amount*(-1) as Item_Net_AMount  from TSPL_PAYMENT_PROCESS_DEDUCTION  "

        sQuery += " union all"

        sQuery += "  select 'Credit' as trans_type,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.doc_no,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No   ,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE ,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_NAME,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE,0 as Paymnet_Amount ,TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount as Item_Net_AMount  from TSPL_PAYMENT_PROCESS_CREDIT_NOTE  "

        sQuery += " ) as final "
        sQuery += " left join TSPL_PAYMENT_PROCESS_head on TSPL_PAYMENT_PROCESS_head.doc_no=final.doc_no"
        sQuery += " " & whrclsItemWise & " "


        dtgv = clsDBFuncationality.GetDataTable(sQuery)



        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            If PrintOpen = True Then
                frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dtgv, "crptMilkPurchaseBillPaymentProcess", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")
            End If
            Return dt
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
            Return Nothing
        End If

    End Function

    Public Sub Load_Report_Paymnet_UDL()
        '======================preeti gupta ticket no [] update 29/11/2016
        Dim sQuery As String
        Dim dtgv As New DataTable
        Dim dt As New DataTable
        Dim companyADD, CompName, CompCode As String

        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        companyADD = dt1.Rows(0).Item("comp_address")

        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompName = dt2.Rows(0).Item("Comp_Name")


        sQuery = ""
        sQuery += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CompCode = dt5.Rows(0).Item("Comp_Code")

        Dim whrcls As String = " where 2=2 "
        whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN (" + fndLoc.Value + ") "
        End If
        whrcls += " and  PaymentProcess.doc_no='" + fndDocNo.Value + "'"
        Dim whrcls1 As String = " where 2=2 "
        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN (" + fndLoc.Value + ") "
        End If

        Dim whrclsDeduction As String = " where 2=2 "
        whrclsDeduction += "  and convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and  convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrclsDeduction += "  and TSPL_MILK_REJECT_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")   "
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrclsDeduction += " and TSPL_LOCATION_MASTER.Loc_Segment_Code    IN (" + fndLoc.Value + ") "
        End If
        sQuery = ""


        sQuery += "select *,Net_AMOUNT-Reject_Deduction_Amount as GrossAmt from (select  TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE,'' as Reject_Type,0 as Reject_Deduction_Amount ,TSPL_MILK_SRN_DETAIL .FAT_KG,TSPL_MILK_SRN_DETAIL .SNF_KG , (-1)*coalesce(TSPL_VENDOR_MASTER.Security_Deduction_Amount,0) as Security_Deduction_Amount,"
        sQuery += " PaymentProcess.doc_no as paymnet_DOC_No,PaymentProcess.Advance_Payment_Amount,PaymentProcess.Advance_Payment_Amount_Knock_Off,PaymentProcess.doc_date as paymnet_doc_date,convert(varchar,PaymentProcess.from_date,103) as Paymnet_From_Date,convert(varchar,PaymentProcess.to_date,103) as Payment_To_Date, (-1)*PaymentProcess.Service_Charge_Amt as Service_Charge_Amt  ,  TSPL_MCC_MASTER.add1 +case when len(TSPL_MCC_MASTER.add2)>0 then ', '+TSPL_MCC_MASTER.add2 else '' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+MCC_City.City_Name  else ' ' end + case when len(TSPL_MCC_MASTER.State_Code )>0 then MCC_State.STATE_NAME else '' end  as MCC_address, '" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2,PaymentProcess.Total_EMP_Amount,PaymentProcess.Incentive_Amount ,PaymentProcess.Incentive_EMP_Amount ,PaymentProcess.EMP_Amount ,PaymentProcess.Vsp_Own_System_Amount ,PaymentProcess.Head_Load_Amount ,(PaymentProcess.Payable_Amount) as Payable_Amount,(PaymentProcess.Credit_Note_Amount)as Credit_Note_Amount,(PaymentProcess.Deduction_Amount)*(-1) as Deduction_Amount,(PaymentProcess.Item_Issue_Amount)*(-1) as Item_Issue_Amount,(PaymentProcess.Item_Issue_Return_Amount) as Item_Issue_Return_Amount,(PaymentProcess.MCC_Sale_Amount)*(-1) as MCC_Sale_Amount,(PaymentProcess.Reduce_Deduc_Amt) as Reduce_Deduc_Amt ,(PaymentProcess.MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_RECEIPT_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty  ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER ,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as FATQTY,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER,(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100) as SNFQTY "
        sQuery += " ,Price_Chart.milk_rate as RATE,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT, TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,TSPL_MILK_SAMPLE_HEAD.SHIFT,"
        sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,TSPL_MILK_SAMPLE_DETAIL.TYPE ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
        sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt ,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT,isnull(PaymentProcess.Deduction_Amount,0)*(-1)  as Total_Deduction "
        sQuery += " from TSPL_MILK_PURCHASE_INVOICE_DETAIL  Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE  "
        sQuery += " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE    Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  "
        sQuery += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE   left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE Left Outer Join TSPL_VENDOR_MASTER On"
        sQuery += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'   Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_RECEIPT_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MCC_MASTER.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join TSPL_VLC_MASTER_HEAD on"
        sQuery += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  "
        sQuery += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code "
        sQuery += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code "
        sQuery += " left join  (select sum(Advance_Payment_Amount) as Advance_Payment_Amount,sum(Advance_Payment_Amount_Knock_Off)as Advance_Payment_Amount_Knock_Off, max(doc_no) as doc_no,max(convert(varchar,doc_date,103)) as doc_date,max(from_date) as from_date,max(to_date) as to_date,sum(Service_Charge_Amt ) as Service_Charge_Amt, VLC_Code, VSP_CODE,sum(Total_EMP_Amount) as Total_EMP_Amount,sum(Incentive_Amount) as Incentive_Amount,sum(Incentive_EMP_Amount) as Incentive_EMP_Amount,sum(EMP_Amount) as EMP_Amount,sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount,sum(Deduction_Amount) as Deduction_Amount,sum(Item_Issue_Amount) as Item_Issue_Amount,sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount,sum(MCC_Sale_Amount) as MCC_Sale_Amount ,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount,sum(Reduce_Deduc_Amt) as Reduce_Deduc_Amt from (select TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount_Knock_Off, TSPL_PAYMENT_PROCESS_HEAD.doc_no,TSPL_PAYMENT_PROCESS_HEAD.doc_date,TSPL_PAYMENT_PROCESS_HEAD.from_date,TSPL_PAYMENT_PROCESS_HEAD.to_date,tspl_payment_process_detail.milk_purchase_invoice_no as Doc_Code,TSPL_PAYMENT_PROCESS_DETAIL.Service_Charge_Amt ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Reduce_Deduc_Amt  from TSPL_PAYMENT_PROCESS_DETAIL"
        sQuery += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
        sQuery += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
        sQuery += " ) as pp group by Doc_No,VSP_CODE,VLC_Code"
        sQuery += " ) as PaymentProcess on "

        sQuery += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_Code"
        sQuery += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code"
        sQuery += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code "
        sQuery += "   from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart "
        sQuery += "   on TSPL_MILK_SRN_DETAIL.Price_Code=Price_Chart.Code "

        sQuery += "  " & whrcls & " "


        sQuery += "  union all"

        sQuery += " select '' as DOC_CODE,case when  TSPL_MILK_REJECT_DETAIL.Defaulter='VSP' then  TSPL_MILK_REJECT_DETAIL .Reject_Type else '' end Reject_Type, case when  TSPL_MILK_REJECT_DETAIL.Defaulter='VSP' then  isnull(TSPL_MILK_REJECT_DETAIL.snf_deduction_amount,0)+isnull(TSPL_MILK_REJECT_DETAIL.Fat_deduction_amount,0) else 0 end as Reject_Deduction_Amount, isnull(TSPL_MILK_REJECT_DETAIL.FAT,0) * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100 as FAT_KG,isnull(TSPL_MILK_REJECT_DETAIL.SNF,0)  * isnull(TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT,0) /100 as SNF_KG ,"
        sQuery += " 0 as Security_Deduction_Amount,'' as paymnet_DOC_No,0 as Advance_Payment_Amount,0 as Advance_Payment_Amount_Knock_Off,"
        sQuery += " NULL as paymnet_doc_date,NULL as Paymnet_From_Date,NULL as Payment_To_Date,"
        sQuery += " 0 as Service_Charge_Amt , ''  as MCC_address,'' as companyADD, ''  as CompName,''  as CompCode,TSPL_COMPANY_MASTER .Logo_Img   as compLogo1 ,TSPL_COMPANY_MASTER .Logo_Img2 as compLogo2"
        sQuery += ",0 as Total_EMP_Amount,0 as Incentive_Amount ,0 as Incentive_EMP_Amount ,0 as EMP_Amount ,0 as Vsp_Own_System_Amount ,0 as Head_Load_Amount ,0  as Payable_Amount,0 as Credit_Note_Amount,0 as Deduction_Amount,0 as Item_Issue_Amount,0 as Item_Issue_Return_Amount,0 as MCC_Sale_Amount ,0 as Reduce_Deduc_Amt,0 as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_REJECT_DETAIL.UOM_Code as UOM_Code,TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT  as Qty  ,TSPL_MILK_REJECT_DETAIL.FAT  as FAT_PER ,0 as FATQTY,TSPL_MILK_REJECT_DETAIL.SNF  as SNF_PER,0 as SNFQTY  ,Price_Chart.milk_rate   as RATE,case when TSPL_MILK_REJECT_DETAIL.Is_Return in ('1','2') then 0 else TSPL_MILK_REJECT_DETAIL.Amount end as Net_AMOUNT, TSPL_MILK_REJECT_HEAD.MCC_CODE  as MCC_CODE , "
        sQuery += " convert(varchar,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) as DOC_DATE,"
        sQuery += "TSPL_MILK_REJECT_DETAIL.VSP_CODE  as VSP_CODE ,TSPL_MILK_REJECT_HEAD.SHIFT  as SHIFT, TSPL_MILK_REJECT_DETAIL.ROUTE_CODE  as ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,"
        sQuery += " '' as TYPE ,0 as CLR,'' as SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VLC_Name "
        sQuery += " ,0 as [EMP],0 as Incentive,0 as HEDAmt,0 as AstAMT,0 as DedAmt ,0 as Total_Basic_AMOUNT,0 as Total_Deduction "
        sQuery += " from TSPL_MILK_REJECT_HEAD left join TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE =TSPL_MILK_REJECT_HEAD.DOC_CODE  left join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code  =TSPL_MILK_REJECT_HEAD.MCC_CODE "
        sQuery += " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_MILK_REJECT_HEAD.MCC_CODE"
        sQuery += " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =TSPL_MILK_REJECT_HEAD.Comp_Code "
        sQuery += " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_MILK_REJECT_DETAIL .VSP_CODE "
        sQuery += " left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code =TSPL_MILK_REJECT_DETAIL.ROUTE_CODE "
        sQuery += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_REJECT_DETAIL.VLC_CODE "
        sQuery += " left join (select distinct FAT_Pers,SNF_Pers,Ratio as Fat_ratio,SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code    from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code=TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code) as  Price_Chart    on TSPL_MILK_REJECT_DETAIL.Price_Code=Price_Chart.Code  "
        sQuery += " " & whrclsDeduction & ") as final "
        sQuery += " order by vsp_code,convert(date,doc_date,103) ,Reject_Type "



        dt = clsDBFuncationality.GetDataTable(sQuery)

        sQuery = "select Customer_CODE,Item_Code,max(Item_Desc)  as Item_Desc,sum(Qty) as Qty from ( select  TSPL_SD_SHIPMENT_detail.qty,'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Amount*(-1) as Amount  from TSPL_PAYMENT_PROCESS_MCC_SALE"
        sQuery += " left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no"
        sQuery += " left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code where TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no='" + fndDocNo.Value + "' )xx group by Customer_CODE,item_code "

        dtgv = clsDBFuncationality.GetDataTable(sQuery)

        If dt IsNot Nothing And dt.Rows.Count > 0 Then

            frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dtgv, "crptMilkPurchaseBillPaymentProcess", "SubMilkPurchaseBill.rpt", "SubMilkPurchaseBill.rpt", "Address.rpt")

        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click


        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            Load_Report_Paymnet_UDL()
        Else
            Load_Report(Nothing, Nothing, Nothing, Nothing, False, True)

        End If


    End Sub

    Private Sub btnDocPrint_Click(sender As Object, e As EventArgs) Handles btnDocPrint.Click
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            Doc_Print()
        End If
    End Sub

    Public Sub Doc_Print()

        Dim Query As String = Nothing

        Dim sQuery As String = Nothing

        Dim companyADD, CompName, CompCode As String

        sQuery = ""
        sQuery = " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address " & _
            " ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.comp_code from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        companyADD = dt1.Rows(0).Item("comp_address")

        CompName = dt1.Rows(0).Item("Comp_Name")

        CompCode = dt1.Rows(0).Item("Comp_Code")

        Dim whrcls As String = " where 2=2 "
        whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN (" + fndLoc.Value + ") "
        End If
        whrcls += " and  PaymentProcess.doc_no='" + fndDocNo.Value + "'"
        Dim whrcls1 As String = " where 2=2 "
        whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN (" + fndLoc.Value + ") "
        End If

        Dim whrclsDeduction As String = " where 2=2 "
        whrclsDeduction += "  and convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103)>=convert(date,('" + dtpFromDate.Value + "'),103) and  convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) <=convert(date,('" + dtpToDate.Value + "'),103) "
        whrclsDeduction += "  and TSPL_MILK_REJECT_DETAIL.VSP_CODE  in ( " & clsCommon.GetMulcallString(txtVSP.arrValueMember) & ")"
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            whrclsDeduction += " and TSPL_LOCATION_MASTER.Loc_Segment_Code    IN (" + fndLoc.Value + ") "
        End If

        Query = "select final.fdate, tdate, FAT_KG, SNF_KG, Advance_Payment_Amount, companyADD, CompName, CompCode, Payable_Amount, Qty, Rate, Net_AMOUNT, ROUTE_CODE, Route_Name, final.VLC_Code, final.VSP_CODE, final.VLC_Name " & _
         ", GHEE, CATTLEFEED , OTHERS " & _
        "from (select      " & _
        " convert(date,('" + dtpFromDate.Value + "'),103) as fdate,convert(date,('" + dtpDate.Value + "'),103) as tdate,sum(TSPL_MILK_SRN_DETAIL .FAT_KG) as FAT_KG, " & _
        " sum(TSPL_MILK_SRN_DETAIL .SNF_KG) as SNF_KG,sum(ISNULL(MCCSALE.GHEE,0)) AS GHEE,sum(ISNULL(MCCSALE.CATTLEFEED,0)) AS CATTLEFEED ,sum(ISNULL(MCCSALE.OTHERS,0)) AS OTHERS, " & _
         "sum(PaymentProcess.Advance_Payment_Amount) as Advance_Payment_Amount,   max(TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE) as VSP_CODE, " & _
        "'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode," & _
        "sum(PaymentProcess.Payable_Amount) as Payable_Amount,sum(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty)as Qty ,sum( Price_Chart.milk_rate) as RATE, sum(TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT) as Net_AMOUNT, max(TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE) as ROUTE_CODE , max(TSPL_MCC_ROUTE_MASTER .Route_Name) as Route_Name , max(TSPL_VLC_MASTER_HEAD.VLC_Code) as VLC_Code,max(TSPL_VLC_MASTER_HEAD.VLC_Name) as VLC_Name " & _
        "from TSPL_MILK_PURCHASE_INVOICE_DETAIL Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD  On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " & _
        " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD .DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE " & _
        " left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL .DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " & _
        "Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE = TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE " & _
         "Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE " & _
        "left outer join TSPL_MILK_RECEIPT_HEAD  on TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE " & _
         "left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE and TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE " & _
         "Left Outer Join TSPL_VENDOR_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE = TSPL_VENDOR_MASTER.Vendor_Code  And TSPL_VENDOR_MASTER.Form_Type = 'VSP' " & _
         "Left Outer Join TSPL_MCC_MASTER  On TSPL_MILK_RECEIPT_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code " & _
         "left join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code = TSPL_MCC_MASTER.MCC_Code " & _
         "Left Outer Join TSPL_MCC_ROUTE_MASTER  On TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code " & _
         "left outer join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO " & _
         "left join TSPL_CITY_MASTER as MCC_City  on MCC_City.city_code = TSPL_MCC_MASTER.City_code " & _
         "left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE = TSPL_MCC_MASTER.State_Code " & _
         "left join ( select sum(Advance_Payment_Amount) as Advance_Payment_Amount,sum(Advance_Payment_Amount_Knock_Off)as Advance_Payment_Amount_Knock_Off, max(doc_no) as doc_no," & _
          "max(convert(varchar, doc_date, 103)) as doc_date, max(from_date) as from_date, max(to_date) as to_date, sum(Service_Charge_Amt ) as Service_Charge_Amt, VLC_Code, VSP_CODE," & _
            "sum(Total_EMP_Amount) as Total_EMP_Amount, sum(Incentive_Amount) as Incentive_Amount, sum(Incentive_EMP_Amount) as Incentive_EMP_Amount, sum(EMP_Amount) as EMP_Amount, sum(Vsp_Own_System_Amount) as Vsp_Own_System_Amount,sum(Head_Load_Amount) as Head_Load_Amount, sum(Payable_Amount) as Payable_Amount,sum(Credit_Note_Amount)as Credit_Note_Amount," & _
            "sum(Deduction_Amount) as Deduction_Amount, sum(Item_Issue_Amount) as Item_Issue_Amount, sum(Item_Issue_Return_Amount) as Item_Issue_Return_Amount, sum(MCC_Sale_Amount) as MCC_Sale_Amount,sum(MCC_Sale_Return_Amount) as MCC_Sale_Return_Amount " & _
               "from (select TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Advance_Payment_Amount_Knock_Off, TSPL_PAYMENT_PROCESS_HEAD.doc_no," & _
          "TSPL_PAYMENT_PROCESS_HEAD.doc_date,TSPL_PAYMENT_PROCESS_HEAD.from_date, TSPL_PAYMENT_PROCESS_HEAD.to_date,tspl_payment_process_detail.milk_purchase_invoice_no as Doc_Code," & _
                        "TSPL_PAYMENT_PROCESS_DETAIL.Service_Charge_Amt,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount, TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount," & _
               "TSPL_VLC_MASTER_HEAD.VLC_Code, TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount," & _
              "TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount, TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from  TSPL_PAYMENT_PROCESS_DETAIL " & _
                       "left join TSPL_PAYMENT_PROCESS_HEAD  on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DETAIL.Doc_No " & _
                        "left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader = TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & _
        "" & whrcls1 & "" & _
                        " )  as pp group by Doc_No,VSP_CODE,VLC_Code ) as PaymentProcess  on PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code  And PaymentProcess.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_Code " & _
         "left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_MILK_PURCHASE_INVOICE_Head.Comp_Code " & _
         "left join  ( select distinct FAT_Pers, SNF_Pers, Ratio as Fat_ratio, SNF_Ratio, Milk_Rate,TSPL_MILK_PRICE_MASTER.Price_Code, TSPL_FAT_SNF_UPLOADER_MASTER.code " & _
              " from TSPL_FAT_SNF_UPLOADER_MASTER  inner join TSPL_MILK_PRICE_MASTER  on TSPL_MILK_PRICE_MASTER.Price_Code = TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code)" & _
            "as Price_Chart  on TSPL_MILK_SRN_DETAIL.Price_Code = Price_Chart.Code " & _
         " left join(" & _
       " select 'MCC Sale' as trans_type,TSPL_PAYMENT_PROCESS_MCC_SALE.doc_no,TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no,TSPL_SD_SHIPMENT_detail.item_code,tspl_item_master.item_desc,TSPL_PAYMENT_PROCESS_MCC_SALE.customer_code,TSPL_PAYMENT_PROCESS_MCC_SALE.amount as Paymnet_Amount,TSPL_SD_SHIPMENT_detail.Amount*(-1) as Amount,(CASE WHEN TSPL_ITEM_MASTER.ITEM_DESC LIKE '%GHEE%' THEN TSPL_SD_SHIPMENT_detail.Amount ELSE 0 END ) AS [GHEE],(CASE when TSPL_ITEM_MASTER.ITEM_DESC LIKE '%CATTLE%' THEN  TSPL_SD_SHIPMENT_detail.Amount ELSE 0 END) AS [CATTLEFEED],(CASE when TSPL_ITEM_MASTER.ITEM_DESC NOT LIKE '%CATTLE%' OR TSPL_ITEM_MASTER.ITEM_DESC NOT LIKE '%GHEE%'  THEN  TSPL_SD_SHIPMENT_detail.Amount ELSE 0 END) AS [OTHERS]   from TSPL_PAYMENT_PROCESS_MCC_SALE " & _
 " LEFT JOIN TSPL_PAYMENT_PROCESS_HEAD ON TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_MCC_SALE.Doc_No " & _
   "left join TSPL_SD_SHIPMENT_detail on TSPL_SD_SHIPMENT_detail.document_code=TSPL_PAYMENT_PROCESS_MCC_SALE.shipment_doc_no " & _
 "left join tspl_item_master on tspl_item_master.item_code=TSPL_SD_SHIPMENT_detail.item_code   " & _
 "WHERE convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + dtpFromDate.Value + "'),103) AND convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + dtpToDate.Value + "'),103)) AS MCCSALE " & _
" on MCCSALE.customer_code=TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE " & _
 "" & whrcls & " " & _
        " group by TSPL_VLC_MASTER_HEAD.VLC_Code )  as final "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Query)
        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, "rptMilkProBulkPmtProcess_VLCWise", "VLC WISE DOC REPORT")
        End If

    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID + "M"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New System.IO.MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(MyBase.Form_ID + "M", objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID + "M", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

   
End Class
