Imports common

Public Class RptBankReconcilliation
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptBankReconcilliation)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnprint.Visible = MyBase.isPostFlag

    End Sub

    Private Sub RptBankReconcilliation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadFromBank()

        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        chkfrombankAll.IsChecked = True

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P for Print ")
    End Sub
    Sub LoadFromBank()
        Dim qry As String = " select bank_code As Code,description  as [Description]from TSPL_Bank_MASTER"
        cbgfrombank.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgfrombank.ValueMember = "Code"
        cbgfrombank.DisplayMember = "Description"
    End Sub
    

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        printdata()
    End Sub
    Sub printdata()
        Try
            If chkfrombankSelect.IsChecked = True AndAlso cbgfrombank.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one Bank")
            End If
            
       
            Dim qry As String = Nothing
            qry = "select '" + clsCommon.GetPrintDate(dtpfromdate.Value, "dd-MMM-yyyy") + "' as StratDate, '" + clsCommon.GetPrintDate(dtptodate.Value, "dd-MMM-yyyy") + "' as EndDate, "
            qry += "(SELECT  Logo_Img FROM TSPL_COMPANY_MASTER WHERE      (Comp_Code = 'DEMO')) AS logo1,"
            qry += "(SELECT  Logo_Img2 FROM  TSPL_COMPANY_MASTER WHERE (Comp_Code = 'DEMO')) AS logo2, "
            qry += "(SELECT     Comp_Name FROM TSPL_COMPANY_MASTER WHERE      (Comp_Code = 'DEMO')) AS CompanyName, "
            qry += "*  from ("
            qry += " select  CONVERT(VARCHAR,Cheque_Date,103)  as [Date],Cheque_No as [Number],Payment_No as [DocNo],Payment_Amount as [Withdrawal],0.00 as [Deposit],Payment_Amount as [Transaction Amt],Bank_Code , CONVERT(Date,Payment_Post_Date,103) as[PostDate] ,'Withdrawal' as DocType ,case when IsRecoCleared ='N' then 'OS' else 'CL' end as 'Reconcilliation Status','PY' as Src "
            qry += " from TSPL_PAYMENT_HEADER where LEN(Cheque_No)>0 and Posted ='1'"
            qry += " union"
            qry += "  select Cheque_Date  ,Cheque_No ,Receipt_No ,0.0,Receipt_Amount as [Deposit],Receipt_Amount  ,Bank_Code,"
            qry += " Receipt_Post_Date  ,'Deposit' as DocType,case when IsRecoCleared ='N' then 'OS' else 'CL' end as 'Reconcilliation  "
            qry += " Status', 'RC' as Src from TSPL_RECEIPT_HEADER  where LEN(Cheque_No)>0   and Posted='Y' "
            qry += " union "
            qry += " select convert(varchar(12),cheque_date),cheque_no as [ChequeNo],Transfer_No, Transfer_Amount,0.00 ,Transfer_Amount ,"
            qry += " From_Bank_Code,Transfer_Posting_Date ,'Withdrawal' as DocType,case when IsRecoCleared ='N' then 'OS' else 'CL' end"
            qry += " as 'Reconcilliation Status','BT' as Src  from TSPL_BANK_TRANSFER  where Post ='P' and LEN(cheque_no)>0 " 'and IsRecoCleared ='N'  "
            qry += " union "
            qry += " select convert(varchar(12),cheque_date),cheque_no as [ChequeNo],Transfer_No,0.0, Deposit_Amount,Deposit_Amount"
            qry += "  ,To_Bank_Code ,Transfer_Posting_Date ,'Deposit' as DocType,case when IsRecoCleared ='N' then 'OS' else 'CL' end "
            qry += " as 'Reconcilliation Status','BT' as Src  from TSPL_BANK_TRANSFER  where Post ='P' and LEN(cheque_no)>0 " 'and IsRecoCleared ='N'"
            qry += " ) as xxx   where 2=2"
            qry += " and CONVERT(date, xxx.PostDate,103) >= convert(date,'" + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + "',103) and CONVERT(date, xxx.PostDate,103) <= convert(date,'" + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy") + "',103)"

            If chkfrombankSelect.IsChecked = True AndAlso cbgfrombank.CheckedValue.Count > 0 Then
                qry += " And  xxx.Bank_Code  in (" + clsCommon.GetMulcallString(cbgfrombank.CheckedValue) + ")"
            End If

            qry += " order by xxx.DocNo "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.CommonServices, dt, "crptBankReconcilliation", "Bank Reconciliation Report")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try

    End Sub

    Private Sub chkfrombankAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkfrombankAll.ToggleStateChanged
        cbgfrombank.Enabled = False
    End Sub

    Private Sub chkfrombankSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkfrombankSelect.ToggleStateChanged
        cbgfrombank.Enabled = True
    End Sub


    Private Sub RptBankReconcilliation_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            printdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
End Class
