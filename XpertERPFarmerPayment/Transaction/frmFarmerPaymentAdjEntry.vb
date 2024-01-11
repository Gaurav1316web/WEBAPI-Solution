'****** Created By: Richa Agarwal 06/01/2015,BM00000005317 **********

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Collections
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports common
Public Class frmFarmerPaymentAdjEntry
    Inherits FrmMainTranScreen
    Public isFromLoadout As Boolean = False
    Public isFromSettlement As Boolean = False
    Public strCustomerNo As String = ""
    Public strCustomerName As String = ""
    Public strDocumnentNo As String = ""
    Public dblDocumnentAmt As Double = 0
    Dim qry As String = ""
    Dim dt As DataTable
    Dim IsNewEntry As Boolean = True
    Dim IsLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()


#Region "Variable"
    Public strAdjNo As String = Nothing
    Dim inSideLoadData As Boolean = False
    Dim strQuery As String
    Dim myDs As DataSet
    Dim row As DataRow
    Dim myDr As SqlDataReader
    Dim myDataTable As DataTable
    Dim myCmd As New SqlCommand
    Dim trans As SqlTransaction
    Dim userCode, companyCode As String
    Dim i As Integer
    Dim btntooltip As ToolTip = New ToolTip()
    Dim x As Integer = 0
    Dim decApplyAmt As Decimal = 0
    Public dtLoadOut As Date
    Public clicked As Boolean = False
#End Region

#Region "Button_Click"
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If saveData() Then
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
  
    Private Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(dtAdj.Value, Nothing) = False Then
            dtAdj.Focus()
            Return False
        End If

        '====================Added by preeti gupta==================
        'clsLockMPPaymentCycle.LockMPTransaction(fndMCC_Code.Value, dtAdj.Value)


        '===========================================================

        If clsCommon.myLen(fndFarmerCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Farmer", Me.Text)
            fndFarmerCode.Focus()
            Return False
        ElseIf clsCommon.myLen(fndDocNo.Value) <= 0 Then
            'clsCommon.MyMessageBoxShow("Please select Document")
            'fndDocNo.Focus()
            'Return False
        End If
        Dim Post As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_Post from TSPL_MP_PAY_ADJ_HEAD where Adjustment_No='" + fndFnAdj.Value + "' "))
        If clsCommon.CompairString(Post, "Y") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow(Me, "Posted Transaction", Me.Text)
            Return False
        End If
        Dim amt As Decimal = 0
        'For Each grow As GridViewRowInfo In gv1.Rows
        '    If clsCommon.myLen(grow.Cells(1).Value) > 0 Then
        '        amt += clsCommon.myCdbl(grow.Cells(5).Value)
        '    End If
        'Next
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myLen(grow.Cells("DiscountCode").Value) > 0 Then
                amt += clsCommon.myCdbl(grow.Cells("amt").Value)
            End If
        Next
        'If amt > clsCommon.myCdbl(txtBalanceAmt.Text) Then
        '    clsCommon.MyMessageBoxShow("Adjustment can not be created more than Balance Amount.", Me.Text)
        '    Return False
        'End If

        If amt = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Adjustment can not be created with 0 Amount.", Me.Text)
            Return False
        End If

        If amt < 0 Then
            clsCommon.MyMessageBoxShow(Me, "Adjustment can not be created with -ve Amount.", Me.Text)
            Return False
        End If

        '' Anubhooti 13-Sep-2014 BM00000003735
        If FrmMainTranScreen.ValidateTransactionAccToFinYear("Adjustment Entry", dtAdj.Value) = False Then
            'Exit Function
            Return False
        End If
        ''
        Return True
    End Function

    Private Function saveData() As Boolean
        If AllowToSave() Then
            Try

                Dim obj As New clsFarmerPaymentAdjustmentEntry
                obj.Adjustment_No = clsCommon.myCstr(fndFnAdj.Value)
                obj.Description = clsCommon.myCstr(txtEntrDesc.Text)
                obj.Adjustment_Date = dtAdj.Value
                obj.Farmer_Code = clsCommon.myCstr(fndFarmerCode.Value)
                obj.Mcc_Code = clsCommon.myCstr(fndMCC_Code.Value)
                obj.Farmer_Name = clsCommon.myCstr(txtVendorName.Text)
                obj.Doc_No = clsCommon.myCstr(fndDocNo.Value)
                obj.Doc_Amount = clsCommon.myCdbl(txtDocAmt.Text)
                obj.Remarks = clsCommon.myCstr(txtRemarks.Text)
                obj.Adjustment_Amount = clsCommon.myCdbl(txtAdjAmt.Text)
                obj.Adjustment_Type = ddlAdjustType.SelectedValue
                obj.Arr = New List(Of clsFarmerPaymentAdjustmentEntryDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    'If clsCommon.myLen(grow.Cells(1).Value) > 0 Then
                    '    Dim objTr As New clsFarmerPaymentAdjustmentEntryDetail()
                    '    objTr.Discount_Code = clsCommon.myCstr(grow.Cells(1).Value)
                    '    objTr.Discount_Description = clsCommon.myCstr(grow.Cells(2).Value)
                    '    objTr.Account_No = clsCommon.myCstr(grow.Cells(3).Value)
                    '    objTr.Account_Description = clsCommon.myCstr(grow.Cells(4).Value)
                    '    objTr.Amount = clsCommon.myCdbl(grow.Cells(5).Value)
                    '    objTr.Remarks = clsCommon.myCstr(grow.Cells(6).Value)
                    '    obj.Arr.Add(objTr)
                    'End If
                    If clsCommon.myLen(grow.Cells("DiscountCode").Value) > 0 Then
                        Dim objTr As New clsFarmerPaymentAdjustmentEntryDetail()
                        objTr.Discount_Code = clsCommon.myCstr(grow.Cells("DiscountCode").Value)
                        objTr.Discount_Description = clsCommon.myCstr(grow.Cells("DiscountDescription").Value)
                        objTr.FarmerCode = clsCommon.myCstr(grow.Cells("FarmerCode").Value)
                        objTr.FarmerName = clsCommon.myCstr(grow.Cells("FarmerName").Value)
                        objTr.Account_No = clsCommon.myCstr(grow.Cells("accountcode").Value)
                        objTr.Account_Description = clsCommon.myCstr(grow.Cells("description").Value)
                        objTr.Amount = clsCommon.myCdbl(grow.Cells("amt").Value)
                        objTr.Remarks = clsCommon.myCstr(grow.Cells("Remarks").Value)
                        obj.Arr.Add(objTr)
                    End If
                Next
                If obj.SaveData(obj, IsNewEntry) Then
                    LoadData(obj.Adjustment_No, NavigatorType.Current)
                    Return True
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
                Return False
            End Try
        End If
        Return False
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        deleteData()



    End Sub

    Public Sub deleteData()

        If fndFnAdj.Value = "" Then
            myMessages.blankValue("ADjustment No.")
            Exit Sub
        End If

        Dim Reason As String = ""
        ' clsLockMPPaymentCycle.LockMPTransaction(fndMCC_Code.Value, dtAdj.Value)
        If myMessages.deleteConfirm() Then
            '' REASON FOR DELETE 
            If clsCancelLog.CheckForReasonOnDelete() Then
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Delete"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
            End If
            If saveCancelLog(Reason, Nothing) Then
                funDelete()
            End If
        End If
    End Sub

    Function saveCancelLog(ByVal Reason As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndFnAdj.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = "Delete"
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        clicked = True
        postData()
    End Sub

    Public Sub postData()
        Try
            If fndFnAdj.Value <> "" Then
                Dim Post As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_Post from TSPL_MP_PAY_ADJ_HEAD where Adjustment_No='" + fndFnAdj.Value + "' "))
                If clsCommon.CompairString(Post, "Y") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Already Posted Transaction", Me.Text)
                    Exit Sub
                End If
                If saveData() Then
                    funPOst()
                    LoadData(fndFnAdj.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
     
    End Sub

    Public Sub funPOst()
        Try
            Dim strDocNo As String = fndFnAdj.Value
            If myMessages.postConfirm() Then
                If clsFarmerPaymentAdjustmentEntry.FunPost(MyBase.Form_ID, strDocNo) Then
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    myMessages.post()
                Else
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnAdjClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjClose.Click
        strAdjNo = fndFnAdj.Value
        Me.Close()
    End Sub
#End Region

    Public Sub funDelete()
        Try
            If (clsFarmerPaymentAdjustmentEntry.DeleteData(fndFnAdj.Value)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data deleted successfully ", Me.Text)
                funReset()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub funReset()
        fndFnAdj.Value = ""
        txtEntrDesc.Text = ""
        dtAdj.Value = clsCommon.GETSERVERDATE()
        dtPost.Value = clsCommon.GETSERVERDATE()
        fndFarmerCode.Value = ""
        txtVendorName.Text = ""
        fndDocNo.Value = ""
        txtDocAmt.Text = "0.00"
        txtBalanceAmt.Text = "0.00"
        txtRemarks.Text = ""
        txtAdjAmt.Text = 0
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        btnSave.Text = "Save"
        btnDelete.Enabled = True
        btnSave.Enabled = True
        btnPost.Enabled = True
        fndFnAdj.Enabled = True
        decApplyAmt = 0
        lblInvisible.Text = 0.0
        gv1.Rows.AddNew()
        IsNewEntry = True
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.PaymentAdjustmentEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False

        End If
    End Sub

#Region "Page Load"
    Private Sub frmAdj_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnAdjClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        LoadAdjustType()
        If isFromSettlement = True Then
            dtAdj.Value = dtLoadOut
            dtPost.Value = dtLoadOut
        Else
            dtAdj.Value = clsCommon.GETSERVERDATE()
            dtPost.Value = clsCommon.GETSERVERDATE()
        End If

        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.isFarmerPaymentCycle, clsFixedParameterCode.isFarmerPaymentCycle, Nothing), "1") = CompairStringResult.Equal Then
            gv1.Columns("FarmerCode").IsVisible = True
            gv1.Columns("FarmerName").IsVisible = True
        Else
            gv1.Columns("FarmerCode").IsVisible = False
            gv1.Columns("FarmerName").IsVisible = False
        End If

        fndFnAdj.MyReadOnly = True

        If fndFnAdj.Value = "" Then
            gv1.Rows.AddNew()
        End If

        If clsCommon.myLen(strAdjNo) > 0 Then
            fndFnAdj.Value = strAdjNo
            LoadData(fndFnAdj.Value, NavigatorType.Current)
        End If
        If isFromLoadout Then
            fndFarmerCode.Value = strCustomerNo
            txtVendorName.Text = strCustomerName
            fndDocNo.Value = strDocumnentNo
            txtDocAmt.Text = clsCommon.myFormat(dblDocumnentAmt)
            lblInvisible.Text = clsCommon.myFormat(dblDocumnentAmt)
            dtAdj.Value = dtLoadOut
            dtPost.Value = dtLoadOut
            btnPost.Enabled = False
        End If
        If clsCommon.myLen(strDocumnentNo) > 0 Then
            fndFnAdj.Value = strDocumnentNo
            LoadData(fndFnAdj.Value, NavigatorType.Current)
        End If
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        ValidateLength()
        ApplyReadOptions()
        '' Drill Down BI
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub ValidateLength()
        fndFnAdj.MyMaxLength = 30
        txtEntrDesc.MaxLength = 100
        txtRemarks.MaxLength = 100
    End Sub
    Private Sub ApplyReadOptions()

    End Sub
#End Region
    Sub LoadAdjustType()
        ddlAdjustType.DataSource = GetAdjustType()
        ddlAdjustType.ValueMember = "Code"
        ddlAdjustType.DisplayMember = "Name"
    End Sub
    Public Shared Function GetAdjustType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Payment"
        dr("Name") = "Payment"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "Invoice"
        dr("Name") = "Invoice"
        dt.Rows.Add(dr)


        Return dt
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New clsFarmerPaymentAdjustmentEntry
            obj = clsFarmerPaymentAdjustmentEntry.GetData(strCode, NavTyep)
            funReset()
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Adjustment_No) > 0) Then
                IsLoadData = True
                IsNewEntry = False
                If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If

                fndFnAdj.Value = obj.Adjustment_No
                txtEntrDesc.Text = obj.Description
                dtAdj.Value = obj.Adjustment_Date
                fndFarmerCode.Value = obj.Farmer_Code
                txtVendorName.Text = obj.Farmer_Name
                '==========ADDED BY PREETI GUPTA=================
                fndMCC_Code.Value = obj.Mcc_Code
                lblMCCDesc.Text = obj.Mcc_Name
                '================================================
                fndDocNo.Value = obj.Doc_No
                txtDocAmt.Text = clsCommon.myCstr(obj.Doc_Amount)
                txtBalanceAmt.Text = clsCommon.myCstr(obj.Bal_Amount)
                txtRemarks.Text = obj.Remarks
                txtAdjAmt.Text = obj.Adjustment_Amount
                ddlAdjustType.SelectedValue = obj.Adjustment_Type
                Dim AdjAmt As Decimal = 0
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    'For Each objTr As clsFarmerPaymentAdjustmentEntryDetail In obj.Arr
                    '    gv1.CurrentRow.Cells(0).Value = objTr.Line_No
                    '    gv1.CurrentRow.Cells(1).Value = objTr.Discount_Code
                    '    gv1.CurrentRow.Cells(2).Value = objTr.Discount_Description
                    '    gv1.CurrentRow.Cells(3).Value = objTr.Account_No
                    '    gv1.CurrentRow.Cells(4).Value = objTr.Account_Description
                    '    gv1.CurrentRow.Cells(5).Value = objTr.Amount
                    '    AdjAmt += objTr.Amount
                    '    gv1.CurrentRow.Cells(6).Value = objTr.Remarks
                    '    gv1.Rows.AddNew()
                    'Next

                    For Each objTr As clsFarmerPaymentAdjustmentEntryDetail In obj.Arr
                        gv1.CurrentRow.Cells("lineno").Value = objTr.Line_No
                        gv1.CurrentRow.Cells("FarmerCode").Value = objTr.FarmerCode
                        gv1.CurrentRow.Cells("FarmerName").Value = objTr.FarmerName
                        gv1.CurrentRow.Cells("DiscountCode").Value = objTr.Discount_Code
                        gv1.CurrentRow.Cells("DiscountDescription").Value = objTr.Discount_Description
                        gv1.CurrentRow.Cells("accountcode").Value = objTr.Account_No
                        gv1.CurrentRow.Cells("description").Value = objTr.Account_Description
                        gv1.CurrentRow.Cells("amt").Value = objTr.Amount
                        AdjAmt += objTr.Amount
                        gv1.CurrentRow.Cells("Remarks").Value = objTr.Remarks
                        gv1.Rows.AddNew()
                    Next

                    txtAdjAmt.Text = clsCommon.myCstr(AdjAmt)
                End If
                fnDocAmt(fndDocNo.Value)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            IsLoadData = False
        End Try
    End Sub






#Region "Event"

    Public Sub funVendor_Data(ByVal isButtonClicked As Boolean)
        If clsCommon.myLen(fndMCC_Code.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Location first", Me.Text)
            Exit Sub
        End If
        Dim Qry As String = "select Vendor_Code as [Code],Farmer_Name as [Name],ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Vendor_Group_Code as [Group Code],(select case when Status ='N' then 'Active' when Status ='Y' then 'In-Active' end) as [Status] from TSPL_VENDOR_MASTER"
        'fndFarmerCode.Value = clsCommon.ShowSelectForm("Vendor Selector", Qry, "Code", "Status ='N' AND OnHold='N'", fndFarmerCode.Value, "Code", isButtonClicked)
        fndFarmerCode.Value = clsMpMaster.getFinder("VLCH.MCC='" & fndMCC_Code.Value & "'", fndFarmerCode.Value, isButtonClicked)
        Me.txtVendorName.Text = clsMpMaster.GetName(fndFarmerCode.Value, Nothing) 'fnVendor(Me.fndFarmerCode.Value)
    End Sub

    Private Function fnVendor(ByVal strCustId As String) As String
        Dim strName As String
        strName = ""
        Try
            strQuery = "select Vendor_Code as [Code],Farmer_Name as [Name],Vendor_Group_Code as [Group Code],(select case when Status ='N' then 'Active' when Status ='Y' then 'In-Active' end) as [Status] from TSPL_VENDOR_MASTER where Vendor_Code='" + strCustId + "'"
            dt = clsDBFuncationality.GetDataTable(strQuery)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each myDr As DataRow In dt.Rows
                    strName = Convert.ToString(myDr(1).ToString().Trim())
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Payment Entry", MessageBoxButtons.OK)
        End Try
        Return strName
    End Function

    Private Sub fndDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndDocNo._MYValidating
        funDocDataFill(isButtonClicked)
    End Sub

    Public Sub funDocDataFill(ByVal isButtonClicked As Boolean, Optional ByVal InvNo As String = Nothing)
        'Dim Qry As String = " select TSPL_Vendor_Invoice_Head.Document_No as [APInvoiceNo],TSPL_Vendor_Invoice_Head.Invoice_Entry_Date as [Date],TSPL_Vendor_Invoice_Head.Vendor_Code as [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name as [Name],TSPL_Vendor_Invoice_Head.Loc_Code as Location,TSPL_Vendor_Invoice_Head.Document_Total as [Invoice Total],isnull(TSPL_Vendor_Invoice_Head.Balance_Amt,0) as [Balance Amount],case when  TSPL_Vendor_Invoice_Head.Posting_Date is not null then 'Posted' else 'Pending' end as Status,TSPL_Vendor_Invoice_Head.Document_Type,Against_POInvoice_No as [InvoiceNo]" + Environment.NewLine + _
        '" from TSPL_Vendor_Invoice_Head " + Environment.NewLine + _
        '" LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_Vendor_Invoice_Head.Vendor_Code "
        'fndDocNo.Value = clsCommon.ShowSelectForm("DocuFilterID", Qry, "APInvoiceNo", "TSPL_Vendor_Invoice_Head.Posting_Date is not null and TSPL_Vendor_Invoice_Head.Vendor_Code='" + fndFarmerCode.Value + "' and isnull(TSPL_Vendor_Invoice_Head.Balance_Amt,0) > '0.00' and len( isnull(Against_PurchaseReturn_No,''))<=0", fndDocNo.Value, "APInvoiceNo", isButtonClicked)
        'fnDocAmt(fndDocNo.Value)
    End Sub

    Private Function fnDocAmt(ByVal strDocId As String) As Decimal
        Dim BalAmt As Double = 0
        'Try
        '    strQuery = "select TSPL_Vendor_Invoice_Head.Document_Total as DocAmt, TSPL_Vendor_Invoice_Head.Balance_Amt-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Vendor_Invoice_Head.Document_No)+(Select ISNULL(SUM(AH.Adjustment_Amount),0) from TSPL_MP_PAY_ADJ_HEAD AH WHere ISNULL(AH.Is_Post,'N')<>'Y' AND AH.Doc_No=TSPL_Vendor_Invoice_Head.Document_No AND AH.Adjustment_No <>'" + fndFnAdj.Value + "') + ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL Where TSPL_PAYMENT_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No AND Post NOT IN ('1','P')),0)) as BalAmt from" & _
        '    " TSPL_Vendor_Invoice_Head " & _
        '    " WHERE TSPL_Vendor_Invoice_Head.Document_No='" & strDocId & "' AND isnull(TSPL_Vendor_Invoice_Head.Posting_Date,'')<>''"
        '    dt = clsDBFuncationality.GetDataTable(strQuery)
        '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '        Me.txtDocAmt.Text = clsCommon.myCstr(dt.Rows(0)("DocAmt"))
        '        Me.lblInvisible.Text = clsCommon.myCstr(dt.Rows(0)("BalAmt"))
        '        Me.txtBalanceAmt.Text = clsCommon.myCstr(dt.Rows(0)("BalAmt"))
        '        BalAmt = clsCommon.myCdbl(dt.Rows(0)("BalAmt"))
        '    End If

        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message, "Receipt Entry", MessageBoxButtons.OK)
        'End Try
        Return BalAmt
    End Function

    Private Sub fndFnAdj__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndFnAdj._MYValidating
        '' finder query change by Panch Raj on 01-may-2018 against ticket : KDI/30/04/18-000281
        Dim Qry As String = " select TSPL_MP_PAY_ADJ_HEAD.Adjustment_no as [AdjustmentNo],TSPL_MP_PAY_ADJ_HEAD.Adjustment_date as [Date],TSPL_MP_PAY_ADJ_HEAD.Farmer_Code as [Farmer Code]," & _
                            " TSPL_MP_PAY_ADJ_HEAD.Farmer_Name as [Farmer Name],TSPL_MP_MASTER.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [VLC Name],TSPL_VLC_MASTER_HEAD.VSP_Code as [VSP Code]," & _
                            " TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_MP_PAY_ADJ_HEAD.Doc_No as [Against Invoice],TSPL_MP_PAY_ADJ_HEAD.Adjustment_Amount as [Adjustment Amount], " & _
                            " (case when TSPL_MP_PAY_ADJ_HEAD.is_post='N' OR TSPL_MP_PAY_ADJ_HEAD.Is_Post IS null then 'UnPosted' when TSPL_MP_PAY_ADJ_HEAD.is_post='Y' then 'Posted' end ) as [Status],TSPL_MP_PAY_ADJ_HEAD.Adjustment_Type as [Adjustment Type], " & _
                            " TSPL_MP_PAY_ADJ_HEAD.Is_Processed as [Is Processed],Description " & _
                            " from TSPL_MP_PAY_ADJ_HEAD " & _
                            " left join TSPL_MP_MASTER on TSPL_MP_PAY_ADJ_HEAD.Farmer_Code=TSPL_MP_MASTER.MP_Code " & _
                            " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code " & _
                            " left join TSPL_VENDOR_MASTER on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code "

        fndFnAdj.Value = clsCommon.ShowSelectForm("AdjustmFiltrFND", Qry, "AdjustmentNo", "", fndFnAdj.Value, "AdjustmentNo", isButtonClicked)
        LoadData(fndFnAdj.Value, NavigatorType.Current)
    End Sub

    Private Sub fndFnAdj__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndFnAdj._MYNavigator
        LoadData(fndFnAdj.Value, NavType)
    End Sub

    Dim isCellValueChagedOccored As Boolean = False
    Private Sub MasterTemplate_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        If Not IsLoadData Then
            If Not isCellValueChagedOccored Then
                isCellValueChagedOccored = True
                If e.Column Is gv1.Columns("DiscountCode") Then
                    qry = "Select Code, Description, Account_Code, Account_Description from TSPL_Discount_Master "
                    Dim Whr As String = "" ' "RIGHT(Account_Code,3)=(Select Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code=(Select Bill_To_Location from TSPL_SD_SALE_INVOICE_HEAD WHERE Document_Code='" & fndDocNo.Value & "'))"
                    'gv1.CurrentRow.Cells(1).Value = clsCommon.ShowSelectForm("frmAdjDSCTFND", qry, "Code", Whr, clsCommon.myCstr(gv1.CurrentRow.Cells(1).Value), "Code", False)
                    'qry = "Select Code, Description, Account_Code, Account_Description from TSPL_Discount_Master WHERE Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(1).Value) + "'"
                    'gv1.CurrentRow.Cells(2).Value = ""
                    'gv1.CurrentRow.Cells(3).Value = ""
                    'gv1.CurrentRow.Cells(4).Value = ""
                    'dt = clsDBFuncationality.GetDataTable(qry)
                    'If dt.Rows.Count > 0 Then
                    '    gv1.CurrentRow.Cells(2).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
                    '    gv1.CurrentRow.Cells(3).Value = clsCommon.myCstr(dt.Rows(0)("Account_Code"))
                    '    gv1.CurrentRow.Cells(4).Value = clsCommon.myCstr(dt.Rows(0)("Account_Description"))
                    'End If
                    gv1.CurrentRow.Cells("DiscountCode").Value = clsCommon.ShowSelectForm("frmAdjDSCTFND", qry, "Code", Whr, clsCommon.myCstr(gv1.CurrentRow.Cells("DiscountCode").Value), "Code", False)
                    qry = "Select Code, Description, Account_Code, Account_Description from TSPL_Discount_Master WHERE Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells("DiscountCode").Value) + "'"
                    gv1.CurrentRow.Cells("DiscountDescription").Value = ""
                    gv1.CurrentRow.Cells("accountcode").Value = ""
                    gv1.CurrentRow.Cells("description").Value = ""
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt.Rows.Count > 0 Then
                        gv1.CurrentRow.Cells("DiscountDescription").Value = clsCommon.myCstr(dt.Rows(0)("Description"))
                        gv1.CurrentRow.Cells("accountcode").Value = clsCommon.myCstr(dt.Rows(0)("Account_Code"))
                        gv1.CurrentRow.Cells("description").Value = clsCommon.myCstr(dt.Rows(0)("Account_Description"))
                    End If
                End If
                CalculateAdjAmt()
            End If
        End If
        isCellValueChagedOccored = False
    End Sub

    '-----This Subroutine-Fills Adjustment Amount in TextBox Calculated from Grid.
    Private Sub CalculateAdjAmt()
        Try
            Dim adjAmt As Decimal = 0.0
            For Each grow As GridViewRowInfo In gv1.Rows
                'If clsCommon.myLen(grow.Cells(1).Value) > 0 Then
                '    adjAmt += clsCommon.myCdbl(grow.Cells(5).Value)
                'End If
                If clsCommon.myLen(grow.Cells("DiscountCode").Value) > 0 Then
                    adjAmt += clsCommon.myCdbl(grow.Cells("amt").Value)
                End If
            Next
            txtAdjAmt.Text = clsCommon.myCstr(adjAmt)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

#End Region


    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Private Sub frmAdj_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            If saveData() Then
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            postData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            deleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Control And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIRC
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        End If
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.Rows.Count > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells("lineno").Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub PrintData()
        If clsCommon.myLen(fndDocNo.Value) > 0 Then
            Dim qry As String
            qry = "select finalQry .*,TSPL_COMPANY_MASTER.Comp_Name as compname, TSPL_COMPANY_MASTER.Logo_Img as Image1, TSPL_COMPANY_MASTER.Logo_Img2 as Image2,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from tspl_location_master where Location_Code in(select Location_Code from TSPL_LOCATION_MASTER where Loc_Segment_Code =(substring (finalqry.AcctNo ,LEN(finalqry.AcctNo)-2,5)))   )as address from (select case when xx.Is_Post='Y' then 'Y' else 'N' end as Is_Post  , xx.Adjustment_No,Convert(varchar,xx.Adjustment_Date,103) as Adjustment_Date ,xx.Farmer_Code ,xx.Farmer_Name ,xx.AcctNo ,xx.AcctDesc ,xx.DbtAmt ,xx.CrAmt ,xx.Comp_Code,xx.Doc_No  from " & _
                  " (SELECT TSPL_MP_PAY_ADJ_HEAD.Is_Post,  TSPL_MP_PAY_ADJ_DETAIL.Adjustment_No  ,Adjustment_Date,TSPL_MP_PAY_ADJ_HEAD.Farmer_Code ,(select Farmer_Name from TSPL_VENDOR_MASTER where Vendor_Code =TSPL_MP_PAY_ADJ_HEAD.Farmer_Code )as Farmer_Name,TSPL_MP_PAY_ADJ_DETAIL.Account_No as AcctNo,Account_Description as AcctDesc,Amount as DbtAmt,0 as CrAmt,TSPL_MP_PAY_ADJ_HEAD .Comp_Code,TSPL_MP_PAY_ADJ_HEAD .Doc_No  FROM TSPL_MP_PAY_ADJ_DETAIL left outer join TSPL_MP_PAY_ADJ_HEAD on  TSPL_MP_PAY_ADJ_DETAIL.Adjustment_No = TSPL_MP_PAY_ADJ_HEAD .Adjustment_No   where TSPL_MP_PAY_ADJ_DETAIL.Adjustment_No ='" & fndFnAdj.Value & "'" & _
                   " union all " & _
                   " select TSPL_MP_PAY_ADJ_HEAD.Is_Post, TSPL_MP_PAY_ADJ_HEAD.Adjustment_No ,TSPL_MP_PAY_ADJ_HEAD.Adjustment_Date  ,TSPL_MP_PAY_ADJ_HEAD.Farmer_Code,(select Farmer_Name from TSPL_VENDOR_MASTER where Vendor_Code =TSPL_MP_PAY_ADJ_HEAD.Farmer_Code )as Farmer_Name,(select TSPL_VENDOR_ACCOUNT_SET.Payable_Account from TSPL_VENDOR_ACCOUNT_SET where TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code  =TSPL_VENDOR_MASTER.Vendor_Account) as Acct,(select Description from TSPL_GL_ACCOUNTS where Account_Code =(select TSPL_VENDOR_ACCOUNT_SET.Payable_Account from TSPL_VENDOR_ACCOUNT_SET where TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Account))as AcctDesc,0 as DbtAmt,TSPL_MP_PAY_ADJ_HEAD.Adjustment_Amount as CrAmt ,TSPL_MP_PAY_ADJ_HEAD .Comp_Code,TSPL_MP_PAY_ADJ_HEAD .Doc_No from TSPL_MP_PAY_ADJ_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_MP_PAY_ADJ_HEAD.Farmer_Code =TSPL_VENDOR_MASTER.Vendor_Code where Adjustment_No ='" & fndFnAdj.Value & "' ) as xx )as finalQry left outer join TSPL_COMPANY_MASTER on finalQry .Comp_Code =TSPL_COMPANY_MASTER .Comp_Code "
            Try
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
                Else
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "ReceiptAdjustmentReport", "Receipt Settlement Report")
                    frmCRV = Nothing
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try


        End If
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "reverse and unpost the current document" + Environment.NewLine + "are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsFarmerPaymentAdjustmentEntry.ReverseAndUnpost(fndFnAdj.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "successfully reversed and recreated", Me.Text)
                    LoadData(fndFnAdj.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndVendorCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndFarmerCode._MYValidating
        funVendor_Data(isButtonClicked)
    End Sub
    Private Function MCCLOCATIONFINDER()
        Dim arrloc As String = ""
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                arrloc = obj.arrLocCodes
            Else
                'fndMCCCode.Enabled = False
                'Throw New Exception("Please Set Default Location Of LogIn User")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return arrloc
    End Function
    Private Sub fndMCC_Code__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMCC_Code._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = "  Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ") "
        End If
        WhrCls += "  and location_category='MCC' and  Location_Code in (" + MCCLOCATIONFINDER() + ")"

        fndMCC_Code.Value = clsCommon.ShowSelectForm("FarmerPayent", qry, "Code", WhrCls, fndMCC_Code.Value, "Code", isButtonClicked)
        lblMCCDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndMCC_Code.Value + "'"))

    End Sub
End Class
