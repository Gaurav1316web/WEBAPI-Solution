'****** Created By: Manoj Chauhan **********
'****** Start Date: 24/11/2011 1:00 PM    **********
'****** Table: tspl_receipt_adjustment_header  , tspl_receipt_adjustment_detail   ******** 
'--19/11/2012--12:15PM--Updation By-[Pankaj Kumar]--Accounts Should not be displayed Which Contains [ControlAccount<>'Y']----Fwd By--Ranjana Mam
'26/12/2012-12:45PM---Updation By--Pankaj Kumar---Applied Validations
'on 04/02/2013 for check the posting status
'---preeti Gupta---Ticket No.-BM00000003015--01/07/2014
'' updation by richa agarwal against ticket no BM00000006176 on 14/04/2015
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
Public Class frmAdj
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
        If clsCommon.myLen(fndCusCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Customer", Me.Text)
            fndCusCode.Focus()
            Return False
        ElseIf clsCommon.myLen(fndDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Document", Me.Text)
            fndDocNo.Focus()
            Return False
        End If
      
        Dim Post As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_Post from TSPL_Receipt_Adjustment_Header where Adjustment_No='" + fndFnAdj.Value + "' "))
        If clsCommon.CompairString(Post, "Y") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow(Me, "Posted Transaction", Me.Text)
            Return False
        End If
        Dim amt As Decimal = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myLen(grow.Cells(1).Value) > 0 Then
                amt += clsCommon.myCdbl(grow.Cells(5).Value)
            End If
        Next

        If amt > clsCommon.myCdbl(txtBalanceAmt.Text) Then
            clsCommon.MyMessageBoxShow(Me, "Adjustment can not be created more than Balance Amount.", Me.Text)
            Return False
        End If
        '' Anubhooti 13-Sep-2014 BM00000003735
        If FrmMainTranScreen.ValidateTransactionAccToFinYear("Adjustment Entry", dtAdj.Value) = False Then
            Exit Function
        End If
        ''
        Return True
    End Function

    Private Function saveData() As Boolean
        If AllowToSave() Then
            Try
                Dim obj As New clsAdjustmentEntryReceivables
                obj.Adjustment_No = clsCommon.myCstr(fndFnAdj.Value)
                obj.Description = clsCommon.myCstr(txtEntrDesc.Text)
                obj.Adjustment_Date = dtAdj.Value
                obj.Customer_No = clsCommon.myCstr(fndCusCode.Value)
                obj.Customer_Name = clsCommon.myCstr(txtCusName.Text)
                obj.ARInvoiceNo = clsCommon.myCstr(fndDocNo.Value)
                obj.Doc_No = clsCommon.myCstr(TxtSaleInvoiceNo.Text)
                obj.Doc_Amount = clsCommon.myCdbl(txtDocAmt.Text)
                obj.Remarks = clsCommon.myCstr(txtRemarks.Text)
                obj.Adjustment_Amount = clsCommon.myCdbl(txtAdjAmt.Text)
                obj.Arr = New List(Of clsAdjustmentEntryReceivablesDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(1).Value) > 0 Then
                        Dim objTr As New clsAdjustmentEntryReceivablesDetail()
                        objTr.Discount_Code = clsCommon.myCstr(grow.Cells(1).Value)
                        objTr.Discount_Description = clsCommon.myCstr(grow.Cells(2).Value)
                        objTr.Account_No = clsCommon.myCstr(grow.Cells(3).Value)
                        objTr.Account_Description = clsCommon.myCstr(grow.Cells(4).Value)
                        objTr.Amount = clsCommon.myCdbl(grow.Cells(5).Value)
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(6).Value)
                        obj.Arr.Add(objTr)
                    End If
                Next
                If obj.SaveData(obj, IsNewEntry) Then
                    LoadData(obj.Adjustment_No, NavigatorType.Current)
                    Return True
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
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
        If fndFnAdj.Value <> "" Then
            Dim Post As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_Post from TSPL_Receipt_Adjustment_Header where Adjustment_No='" + fndFnAdj.Value + "' "))
            If clsCommon.CompairString(Post, "Y") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Already Posted Transaction", Me.Text)
                Exit Sub
            End If
            If saveData() Then
                funPOst()
                LoadData(fndFnAdj.Value, NavigatorType.Current)
            End If
        End If
    End Sub

    Public Sub funPOst()
        Try
            Dim strDocNo As String = fndFnAdj.Value
            If myMessages.postConfirm() Then
                If clsAdjustmentEntryReceivables.FunPost(strDocNo) Then
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    myMessages.post()
                Else
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnAdjClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdjClose.Click
        strAdjNo = fndFnAdj.Value
        Me.Close()
    End Sub
#End Region

    Public Sub funDelete()
        Try
            connectSql.RunSp("sp_TSPL_Receipt_Adjustment_Detail_Delete", New SqlParameter("@Adjustment_No", fndFnAdj.Value))
            connectSql.RunSp("sp_TSPL_Receipt_Adjustment_Header_Delete", New SqlParameter("@Adjustment_No", fndFnAdj.Value))
            myMessages.delete()
            funReset()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub funReset()
        fndFnAdj.Value = ""
        txtEntrDesc.Text = ""
        dtAdj.Value = clsCommon.GETSERVERDATE()
        dtPost.Value = clsCommon.GETSERVERDATE()
        fndCusCode.Value = ""
        txtCusName.Text = ""
        fndDocNo.Value = ""
        txtDocAmt.Text = "0.00"
        txtBalanceAmt.Text = "0.00"
        txtRemarks.Text = ""
        txtAdjAmt.Text = ""
        TxtSaleInvoiceNo.Text = ""
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        btnSave.Text = "Save"
        btnDelete.Enabled = True
        btnSave.Enabled = True
        btnPost.Enabled = True
        fndFnAdj.MyReadOnly = False
        decApplyAmt = 0
        lblInvisible.Text = 0.0
        gv1.Rows.AddNew()
        IsNewEntry = True
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ReceiptAdjustmentEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
    End Sub

#Region "Page Load"
    Private Sub frmAdj_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Xtra.UpdateSaleInvoiceBalanceAmt()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnAdjClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")

        If isFromSettlement = True Then
            dtAdj.Value = dtLoadOut
            dtPost.Value = dtLoadOut
        Else
            dtAdj.Value = clsCommon.GETSERVERDATE()
            dtPost.Value = clsCommon.GETSERVERDATE()
        End If


        fndFnAdj.MyReadOnly = False

        If fndFnAdj.Value = "" Then
            gv1.Rows.AddNew()
        End If

        If clsCommon.myLen(strAdjNo) > 0 Then
            fndFnAdj.Value = strAdjNo
            LoadData(fndFnAdj.Value, NavigatorType.Current)
        End If
        If isFromLoadout Then
            fndCusCode.Value = strCustomerNo
            txtCusName.Text = strCustomerName
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

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New clsAdjustmentEntryReceivables
            obj = clsAdjustmentEntryReceivables.GetData(strCode, NavTyep)
            funReset()
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Adjustment_No) > 0) Then
                IsLoadData = True
                IsNewEntry = False
                fndFnAdj.MyReadOnly = True
                If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If

                fndFnAdj.Value = obj.Adjustment_No
                txtEntrDesc.Text = obj.Description
                dtAdj.Value = obj.Adjustment_Date
                fndCusCode.Value = obj.Customer_No
                txtCusName.Text = obj.Customer_Name
                fndDocNo.Value = obj.ARInvoiceNo
                TxtSaleInvoiceNo.Text = obj.Doc_No
                txtDocAmt.Text = clsCommon.myCstr(obj.Doc_Amount)
                txtBalanceAmt.Text = clsCommon.myCstr(obj.Bal_Amount)
                txtRemarks.Text = obj.Remarks
                txtAdjAmt.Text = obj.Adjustment_Amount
                Dim AdjAmt As Decimal = 0
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsAdjustmentEntryReceivablesDetail In obj.Arr
                        gv1.CurrentRow.Cells(0).Value = objTr.Line_No
                        gv1.CurrentRow.Cells(1).Value = objTr.Discount_Code
                        gv1.CurrentRow.Cells(2).Value = objTr.Discount_Description
                        gv1.CurrentRow.Cells(3).Value = objTr.Account_No
                        gv1.CurrentRow.Cells(4).Value = objTr.Account_Description
                        gv1.CurrentRow.Cells(5).Value = objTr.Amount
                        AdjAmt += objTr.Amount
                        gv1.CurrentRow.Cells(6).Value = objTr.Remarks
                        gv1.Rows.AddNew()
                    Next
                    txtAdjAmt.Text = clsCommon.myCstr(AdjAmt)
                End If
                ''RICHA AGARWAL 14/04/2015
                fnDocAmt(fndDocNo.Value)
                'If (clsCommon.myCdbl(txtBalanceAmt.Text) + clsCommon.myCdbl(txtAdjAmt.Text)) > clsCommon.myCdbl(txtBalanceAmt.Text) Then
                '    txtBalanceAmt.Text = clsCommon.myCstr(clsCommon.myCdbl(txtAdjAmt.Text))
                'Else
                txtBalanceAmt.Text = clsCommon.myCstr(clsCommon.myCdbl(txtBalanceAmt.Text) + clsCommon.myCdbl(txtAdjAmt.Text))
                ' End If

                ''--------------
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            IsLoadData = False
        End Try
    End Sub






#Region "Event"

    Private Sub fndCusCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCusCode._MYValidating
        funCus_Data(isButtonClicked)
    End Sub
    Public Sub funCus_Data(ByVal isButtonClicked As Boolean)
        Dim Qry As String = "select Cust_Code as [Code],Customer_Name as [Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],Cust_Group_Code as [Group Code],(select case when Status ='N' then 'Active' when Status ='Y' then 'In-Active' end) as [Status] from TSPL_CUSTOMER_MASTER "
        fndCusCode.Value = clsCommon.ShowSelectForm("Customer Selector", Qry, "Code", "Status ='N' AND OnHold='N'", fndCusCode.Value, "Code", isButtonClicked)
        Me.txtCusName.Text = fnCustomer(Me.fndCusCode.Value)
    End Sub
    
    Private Function fnCustomer(ByVal strCustId As String) As String
        Dim strName As String
        strName = ""
        Try
            strQuery = "select Cust_Code as [Customer No],Customer_Name as [Name],Cust_Group_Code as [Group Code],(select case when Status ='N' then 'Active' when Status ='Y' then 'In-Active' end) as [Status] from TSPL_CUSTOMER_MASTER where cust_code='" + strCustId + "'"
            dt = clsDBFuncationality.GetDataTable(strQuery)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each myDr As DataRow In dt.Rows
                    strName = Convert.ToString(myDr(1).ToString().Trim())
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Receipt Entry", MessageBoxButtons.OK)
        End Try
        Return strName
    End Function
    
    Private Sub fndDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndDocNo._MYValidating
        funDocDataFill(isButtonClicked)
    End Sub

    Public Sub funDocDataFill(ByVal isButtonClicked As Boolean, Optional ByVal InvNo As String = Nothing)
        ''richa agarwal 20/03/2015 against ticket no BM00000005810 show data from bulk invoice also
        'Dim Qry As String = "select Document_Code as [InvoiceNo], TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Date], TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name as [Name], Bill_To_Location as Location, Total_Amt as [Invoice Total]," & _
        '" isnull(TSPL_Customer_Invoice_Head.Balance_Amt,0) as [Balance Amount], TSPL_SD_SALE_INVOICE_HEAD.Status from TSPL_SD_SALE_INVOICE_HEAD" & _
        '" LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & _
        '" LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code"
        If clsCommon.myLen(fndCusCode.Value) > 0 Then
            'Dim Qry As String = " Select Final.InvoiceNo ,Final.Date ,Final.[Customer Code] ,Final.Name ,Final.Location ,Final.[Invoice Total] ,Final.[Balance Amount] ,Final.Status  from (select Document_Code as [InvoiceNo], TSPL_SD_SALE_INVOICE_HEAD.Document_Date as [Date], TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name as [Name], Bill_To_Location as Location, Total_Amt as [Invoice Total], isnull(TSPL_Customer_Invoice_Head.Balance_Amt,0) as [Balance Amount], TSPL_SD_SALE_INVOICE_HEAD.Status as Status from TSPL_SD_SALE_INVOICE_HEAD LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code where TSPL_SD_SALE_INVOICE_HEAD.Status=1 and isnull(TSPL_Customer_Invoice_Head.Balance_Amt,0) > '0.00' " & _
            '               " Union All" & _
            '               " select TSPL_INVOICE_MASTER_BULKSALE.Document_No  as [InvoiceNo], TSPL_INVOICE_MASTER_BULKSALE.Document_Date as [Date], TSPL_INVOICE_MASTER_BULKSALE.Customer_Code as [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name as [Name],TSPL_INVOICE_MASTER_BULKSALE.Location_Code as Location, Total_Amt as [Invoice Total], isnull(TSPL_Customer_Invoice_Head.Balance_Amt,0) as [Balance Amount], TSPL_INVOICE_MASTER_BULKSALE.Posted as Status from TSPL_INVOICE_MASTER_BULKSALE LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_INVOICE_MASTER_BULKSALE.Customer_Code  where TSPL_INVOICE_MASTER_BULKSALE.Posted=1 and isnull(TSPL_Customer_Invoice_Head.Balance_Amt,0) > '0.00' )Final "
            Dim Qry As String = "Select TSPL_Customer_Invoice_Head.Document_No as [InvoiceNo] ,TSPL_Customer_Invoice_Head.Document_Date as [Date],TSPL_Customer_Invoice_Head.Against_Sale_No as [Against Invoice No], TSPL_Customer_Invoice_Head.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Name],TSPL_Customer_Invoice_Head.Loc_Code ,TSPL_Customer_Invoice_Head.Document_Total as [Invoice Total] ,isnull(TSPL_Customer_Invoice_Head.Balance_Amt,0) as [Balance Amount], TSPL_Customer_Invoice_Head.Status from TSPL_Customer_Invoice_Head LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Customer_Invoice_Head.Customer_Code"

            Dim strwhrcls As String = " TSPL_Customer_Invoice_Head.Status=1 and isnull(TSPL_Customer_Invoice_Head.Balance_Amt,0) > '0.00' and TSPL_Customer_Invoice_Head.Document_Type in ('I','D') and TSPL_Customer_Invoice_Head.Customer_Code='" & fndCusCode.Value & "'"

            fndDocNo.Value = clsCommon.ShowSelectForm("DocFilterID", Qry, "InvoiceNo", strwhrcls, fndDocNo.Value, "InvoiceNo", isButtonClicked)

            fnDocAmt(fndDocNo.Value)
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select Customer first", Me.Text)
        End If
       
    End Sub
    
    Private Function fnDocAmt(ByVal strDocId As String) As Decimal
        Dim BalAmt As Double = 0
        Try
            ''richa agarwal 20/03/2015 against ticket no BM00000005810 show data from bulk invoice also
            '    strQuery = "select TSPL_Customer_Invoice_Head.Document_Total as DocAmt, TSPL_Customer_Invoice_Head.Balance_Amt-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No)+(Select ISNULL(SUM(AH.Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER AH WHere ISNULL(AH.Is_Post,'N')<>'Y' AND AH.Doc_No=TSPL_Customer_Invoice_Head.Against_Sale_No AND AH.Adjustment_No <>'" + fndFnAdj.Value + "')) as BalAmt from TSPL_SD_SALE_INVOICE_HEAD" & _
            '" LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code" & _
            '" WHERE TSPL_SD_SALE_INVOICE_HEAD.Document_Code='" & strDocId & "' AND TSPL_SD_SALE_INVOICE_HEAD.Status =1"
            'strQuery = "Select Final.DocAmt,Final.BalAmt,Final.InvoiceNo from (select TSPL_Customer_Invoice_Head.Document_Total as DocAmt, TSPL_Customer_Invoice_Head.Balance_Amt-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No)+(Select ISNULL(SUM(AH.Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER AH WHere ISNULL(AH.Is_Post,'N')<>'Y' AND AH.Doc_No=TSPL_Customer_Invoice_Head.Against_Sale_No AND AH.Adjustment_No <>'')) as BalAmt,TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo from TSPL_SD_SALE_INVOICE_HEAD LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code WHERE TSPL_SD_SALE_INVOICE_HEAD.Status =1 " & _
            '          " Union All " & _
            '          " select TSPL_Customer_Invoice_Head.Document_Total as DocAmt, TSPL_Customer_Invoice_Head.Balance_Amt-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No)+(Select ISNULL(SUM(AH.Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER AH WHere ISNULL(AH.Is_Post,'N')<>'Y' AND AH.Doc_No=TSPL_Customer_Invoice_Head.Against_Sale_No AND AH.Adjustment_No <>'')) as BalAmt,TSPL_INVOICE_MASTER_BULKSALE.Document_No as InvoiceNo from TSPL_INVOICE_MASTER_BULKSALE LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No WHERE TSPL_INVOICE_MASTER_BULKSALE.Posted  =1) Final where Final.InvoiceNo ='" & strDocId & "' "
            'strQuery = "select TSPL_Customer_Invoice_Head.Document_Total as DocAmt, TSPL_Customer_Invoice_Head.Balance_Amt-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No)+(Select ISNULL(SUM(AH.Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER AH WHere ISNULL(AH.Is_Post,'N')<>'Y' AND AH.Doc_No=TSPL_Customer_Invoice_Head.Against_Sale_No AND AH.Adjustment_No <>'')) as BalAmt,TSPL_Customer_Invoice_Head.Against_Sale_No  from  TSPL_Customer_Invoice_Head where TSPL_Customer_Invoice_Head.Document_No ='" & strDocId & "'"
            'strQuery = "select TSPL_Customer_Invoice_Head.Document_Total as DocAmt, TSPL_Customer_Invoice_Head.Balance_Amt-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere Posted<>'Y' AND TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No)+(Select ISNULL(SUM(AH.Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER AH WHere ISNULL(AH.Is_Post,'N')<>'Y' AND  AH.ARInvoiceNo =TSPL_Customer_Invoice_Head.Document_No AND AH.Adjustment_No <>'')) as BalAmt,TSPL_Customer_Invoice_Head.Against_Sale_No  from  TSPL_Customer_Invoice_Head where TSPL_Customer_Invoice_Head.Document_No ='" & strDocId & "'"
            strQuery = "select TSPL_Customer_Invoice_Head.Document_Total as DocAmt, TSPL_Customer_Invoice_Head.Document_Total-((Select ISNULL(SUM(Applied_Amount),0) from TSPL_RECEIPT_DETAIL WHere TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Document_No)+(Select ISNULL(SUM(AH.Adjustment_Amount),0) from TSPL_RECEIPT_ADJUSTMENT_HEADER AH WHere  AH.ARInvoiceNo =TSPL_Customer_Invoice_Head.Document_No AND AH.Adjustment_No <>'')) as BalAmt,TSPL_Customer_Invoice_Head.Against_Sale_No  from  TSPL_Customer_Invoice_Head where TSPL_Customer_Invoice_Head.Document_No ='" & strDocId & "'"

            dt = clsDBFuncationality.GetDataTable(strQuery)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Me.txtDocAmt.Text = clsCommon.myCstr(dt.Rows(0)("DocAmt"))
                Me.lblInvisible.Text = clsCommon.myCstr(dt.Rows(0)("BalAmt"))
                Me.txtBalanceAmt.Text = clsCommon.myCstr(dt.Rows(0)("BalAmt"))
                BalAmt = clsCommon.myCdbl(dt.Rows(0)("BalAmt"))
                TxtSaleInvoiceNo.Text = clsCommon.myCstr(dt.Rows(0)("Against_Sale_No"))
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Receipt Entry", MessageBoxButtons.OK)
        End Try
        Return BalAmt
    End Function

    Private Sub fndFnAdj__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndFnAdj._MYValidating
        Dim Qry As String = " select adjustment_no as [AdjustmentNo],tspl_receipt_adjustment_header.Description ,Adjustment_date as [Date],Customer_No as [Customer No],tspl_receipt_adjustment_header.CUSTOMER_Name as [Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],(select case when is_post='N' OR Is_Post IS null then 'UnPosted' when is_post='Y' then 'Posted' end ) as [Status],convert (varchar, tspl_receipt_adjustment_header.Post_Date,103) as [Post Date] " & _
                            " , tspl_receipt_adjustment_header.ARInvoiceNo  as [Document No], tspl_receipt_adjustment_header.Doc_Amount as [Document Amount] ,tspl_receipt_adjustment_header.Adjustment_Amount  as [Adjustment Amount]  ,tspl_receipt_adjustment_header.Created_By  as [Created By], convert(varchar, tspl_receipt_adjustment_header.Created_Date, 103) as [Created Date]  ,tspl_receipt_adjustment_header.Modified_By as [Modified By], convert (varchar, tspl_receipt_adjustment_header.Modified_Date ,103) as [Modified Date] ,tspl_receipt_adjustment_header.Doc_No as [Sale Invoice No] from tspl_receipt_adjustment_header "
        '' Anubhooti 13-Mar-2015 (Fetch Alies Name On Vendor Finder) Ticket No : GKD/24/09/18-000164 By Prabhakar 
        Qry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = tspl_receipt_adjustment_header.Customer_No "
        fndFnAdj.Value = clsCommon.ShowSelectForm("AdjustmFiltrFND", Qry, "AdjustmentNo", "", fndFnAdj.Value, "Adjustment_date desc", isButtonClicked)
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
                    gv1.CurrentRow.Cells(1).Value = clsCommon.ShowSelectForm("frmAdjDSCTFND", qry, "Code", Whr, clsCommon.myCstr(gv1.CurrentRow.Cells(1).Value), "Code", False)
                    qry = "Select Code, Description, Account_Code, Account_Description from TSPL_Discount_Master WHERE Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(1).Value) + "'"
                    gv1.CurrentRow.Cells(2).Value = ""
                    gv1.CurrentRow.Cells(3).Value = ""
                    gv1.CurrentRow.Cells(4).Value = ""
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt.Rows.Count > 0 Then
                        gv1.CurrentRow.Cells(2).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
                        gv1.CurrentRow.Cells(3).Value = clsCommon.myCstr(dt.Rows(0)("Account_Code"))
                        gv1.CurrentRow.Cells(4).Value = clsCommon.myCstr(dt.Rows(0)("Account_Description"))
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
                If clsCommon.myLen(grow.Cells(1).Value) > 0 Then
                    adjAmt += clsCommon.myCdbl(grow.Cells(5).Value)
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
            PrintData(fndFnAdj.Value)
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIRC
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                              "TSPL_Receipt_Adjustment_Header " + Environment.NewLine +
                              "TSPL_Receipt_Adjustment_Detail" + Environment.NewLine +
                              "TSPL_Receipt_Adjustment_Header" + Environment.NewLine +
                              "Journal Entry (On Post Button)")
            End If
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.Rows.Count > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(0).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(fndFnAdj.Value) > 0 Then
            PrintData(fndFnAdj.Value)
        Else
            clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
        End If

    End Sub
    Sub PrintData(ByVal StrCode As String)
        If clsCommon.myLen(fndDocNo.Value) > 0 OrElse clsCommon.myLen(StrCode) > 0 Then
            Dim qry As String
            qry = "select case when Adjustment_Header.Is_Post='Y' then 'Y' else 'N' end as Is_Post, finalQry .*,TSPL_COMPANY_MASTER.Comp_Name as compname, TSPL_COMPANY_MASTER.Logo_Img as Image1, TSPL_COMPANY_MASTER.Logo_Img2 as Image2,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from tspl_location_master where Location_Code in(select Location_Code from TSPL_LOCATION_MASTER where Loc_Segment_Code =(substring (finalqry.AcctNo ,LEN(finalqry.AcctNo)-2,5)))   )as address from (select xx.Adjustment_No,Convert(varchar,xx.Adjustment_Date,103) as Adjustment_Date ,xx.Customer_No ,xx.Customer_Name ,xx.AcctNo ,xx.AcctDesc ,xx.DbtAmt ,xx.CrAmt ,xx.Comp_Code,xx.Doc_No  from " & _
                  " (SELECT  TSPL_Receipt_Adjustment_Detail.Adjustment_No  ,Adjustment_Date,TSPL_Receipt_Adjustment_Header.Customer_No ,  (select Customer_Name from TSPL_CUSTOMER_MASTER where cust_Code =TSPL_Receipt_Adjustment_Header .Customer_No )as Customer_Name,TSPL_Receipt_Adjustment_Detail.Account_No as AcctNo,Account_Description as AcctDesc,Amount as DbtAmt,0 as CrAmt,TSPL_Receipt_Adjustment_Header .Comp_Code," & _
                  " case when isnull(TSPL_Receipt_Adjustment_Header.Doc_No ,'')='' then TSPL_Receipt_Adjustment_Header.ARInvoiceNo else TSPL_Receipt_Adjustment_Header.Doc_No end as Doc_No " & _
                  " FROM TSPL_Receipt_Adjustment_Detail left outer join TSPL_Receipt_Adjustment_Header on  TSPL_Receipt_Adjustment_Detail.Adjustment_No = TSPL_Receipt_Adjustment_Header .Adjustment_No   where TSPL_Receipt_Adjustment_Detail.Adjustment_No ='" & StrCode & "'" & _
                   " union all " & _
                   " select TSPL_Receipt_Adjustment_Header.Adjustment_No ,TSPL_Receipt_Adjustment_Header .Adjustment_Date  ,TSPL_Receipt_Adjustment_Header .Customer_No,(select Customer_Name from TSPL_CUSTOMER_MASTER where cust_Code =TSPL_Receipt_Adjustment_Header .Customer_No )as Customer_Name,(select TSPL_CUSTOMER_ACCOUNT_SET .Receivable_Control_acct from TSPL_CUSTOMER_ACCOUNT_SET where TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account =TSPL_CUSTOMER_master.Cust_Account) as Acct,(select Description from TSPL_GL_ACCOUNTS where Account_Code =(select TSPL_CUSTOMER_ACCOUNT_SET .Receivable_Control_acct  from TSPL_CUSTOMER_ACCOUNT_SET where TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account =TSPL_CUSTOMER_master.Cust_Account))as AcctDesc,0 as DbtAmt,TSPL_Receipt_Adjustment_Header.Adjustment_Amount as CrAmt ,TSPL_Receipt_Adjustment_Header .Comp_Code, " & _
                   " case when isnull(TSPL_Receipt_Adjustment_Header.Doc_No ,'')='' then TSPL_Receipt_Adjustment_Header.ARInvoiceNo else TSPL_Receipt_Adjustment_Header.Doc_No end as Doc_No " & _
                   " from TSPL_Receipt_Adjustment_Header left outer join TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header .Customer_No =tspl_customer_master.Cust_Code  where Adjustment_No ='" & StrCode & "')as xx )as finalQry left outer join TSPL_COMPANY_MASTER on finalQry .Comp_Code =TSPL_COMPANY_MASTER .Comp_Code left join TSPL_Receipt_Adjustment_Header as Adjustment_Header on Adjustment_Header.Adjustment_No =finalQry.Adjustment_No "
            Try
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
                Else
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "AdjustmentReport", "Settlement Report")
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
                If clsAdjustmentEntryReceivables.ReverseAndUnpost(fndFnAdj.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "successfully reversed and recreated", Me.Text)
                    LoadData(fndFnAdj.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
