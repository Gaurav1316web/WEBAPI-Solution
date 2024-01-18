Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class RptBranchAccountMapping
    Inherits FrmMainTranScreen
#Region "Variables"
    Const ColFromLocation As String = "From Location"
    Const ColToLocation As String = "To Location"
    Const ColBranchAccount As String = "Branch Account"
    Const ColBranchAccountName As String = "Branch Account Name"
    Const colTrans As String = "Transaction"
    Const colDoc As String = "Doc"
    Const colDocNo As String = "Doc No"
    Const colBranchAmt As String = "Branch Amt"
    Const colBAmt As String = "Amt"
    Const ColBranchNo As String = "Branch Account No"
    Const ColBranchDesc As String = "Branch Account Desc"




    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim IsLoadData As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmBranchAccountMapping)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnGo.Visible = MyBase.isModifyFlag
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtTransaction.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        fromDate.Enabled = True
        ToDate.Enabled = True
        txtTransaction.Enabled = True
        txtLocation.Enabled = True
    End Sub
    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoTrans As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTrans.FormatString = ""
        repoTrans.HeaderText = "Transaction Type"
        repoTrans.Name = colTrans
        repoTrans.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoTrans.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTrans.Width = 130
        gv.MasterTemplate.Columns.Add(repoTrans)

        Dim repoDocNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocNo.FormatString = ""
        repoDocNo.HeaderText = "Document No"
        repoDocNo.Name = colDoc
        repoDocNo.Width = 200
        repoDocNo.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoDocNo)

        Dim repoFromLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromLocation.FormatString = ""
        repoFromLocation.HeaderText = "From Location"
        repoFromLocation.Name = ColFromLocation
        repoFromLocation.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoFromLocation.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFromLocation.Width = 130
        gv.MasterTemplate.Columns.Add(repoFromLocation)

        Dim repoBAccountName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBAccountName.FormatString = ""
        repoBAccountName.HeaderText = "Branch Accounting Code"
        repoBAccountName.Name = ColBranchAccount
        repoBAccountName.Width = 200
        repoBAccountName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoBAccountName)

        Dim repoBAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBAccount.FormatString = ""
        repoBAccount.HeaderText = "Branch Account Name"
        repoBAccount.Name = ColBranchAccountName
        repoBAccount.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoBAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        repoBAccount.Width = 130
        gv.MasterTemplate.Columns.Add(repoBAccount)

        Dim repoBAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBAmt.FormatString = ""
        repoBAmt.HeaderText = "Amount"
        repoBAmt.Name = colBAmt
        repoBAmt.Width = 240
        repoBAmt.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoBAmt)

        Dim repoDocName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocName.FormatString = ""
        repoDocName.HeaderText = "Document No"
        repoDocName.Name = colDocNo
        repoDocName.Width = 200
        repoDocName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoDocName)

        Dim repoToLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoToLocation.FormatString = ""
        repoToLocation.HeaderText = "To Location"
        repoToLocation.Name = ColToLocation
        repoToLocation.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoToLocation.TextImageRelation = TextImageRelation.TextBeforeImage
        repoToLocation.Width = 130
        'repoToLocation.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoToLocation)

        Dim repoBranchCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBranchCode.FormatString = ""
        repoBranchCode.HeaderText = "Branch Accounting Code"
        repoBranchCode.Name = ColBranchNo
        repoBranchCode.Width = 200
        repoBranchCode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoBranchCode)

        Dim repoBranchName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBranchName.FormatString = ""
        repoBranchName.HeaderText = "Branch Account Name"
        repoBranchName.Name = ColBranchDesc
        repoBranchName.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoBranchName.TextImageRelation = TextImageRelation.TextBeforeImage
        repoBranchName.Width = 130
        gv.MasterTemplate.Columns.Add(repoBranchName)

        Dim repoBranchAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBranchAmt.FormatString = ""
        repoBranchAmt.HeaderText = "Amount"
        repoBranchAmt.Name = colBranchAmt
        repoBranchAmt.Width = 240
        repoBranchAmt.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoBranchAmt)

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

   
    Private Sub RptBranchAccountMapping_Load(sender As Object, e As EventArgs) Handles Me.Load
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        SetUserMgmtNew()
        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv
            'LoadBlankGrid()
            loadReport()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub loadReport()
        Try
            Dim dt As DataTable = Nothing
            Dim qry As String = ""
            gv.DataSource = Nothing
            gv.Columns.Clear()
            gv.Rows.Clear()
            '' Bank Transfer Branch Accounting Report
            'qry = "select TSPL_JOURNAL_MASTER.Source_Desc as [TransName],TSPL_JOURNAL_MASTER.Source_Doc_No as DocNo,TSPL_JOURNAL_MASTER.Segment_code as Fromloc,TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account as BranchCode,TSPL_JOURNAL_DETAILS.Account_Desc as BranchDesc,TSPL_JOURNAL_DETAILS.Amount as [BranchAmt]"
            'qry += " ,Transfer_No,Account_code1,Account_Desc1,Amount1 from TSPL_BRANCH_ACCOUNT_MAPPING"
            'qry += " left outer join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_DETAILS.Account_code=TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account "
            'qry += " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No"
            'qry += " left outer join (select Transfer_No,Against_Withdrawal_No,TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account as Account_code1,TSPL_JOURNAL_DETAILS.Account_Desc as Account_Desc1,TSPL_JOURNAL_DETAILS.Amount as Amount1 from TSPL_BANK_TRANSFER left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_BANK_TRANSFER.Transfer_No left outer join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No"
            'qry += " left outer join TSPL_BRANCH_ACCOUNT_MAPPING on TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account=TSPL_JOURNAL_DETAILS.Account_code"
            'qry += " ) as second1 on second1.Against_Withdrawal_No=TSPL_JOURNAL_MASTER.Source_Doc_No where 2=2 "


            'If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_JOURNAL_MASTER.Source_Desc in (" & clsCommon.GetMulcallString(txtTransaction.arrValueMember) & ")"
            'Else
            '    qry += " and TSPL_JOURNAL_MASTER.Source_Desc='Bank Transfer' "
            'End If
            'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_JOURNAL_MASTER.Segment_code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            'End If
            'qry += " and  Account_code1 is not null "

            ' Tanker Dispatch 
            qry = "  Select   XFinal.[Transaction Type],XFinal.[Document NO],XFinal.[From Location] , XFinal.[Branch Accounting code],XFinal.[Branch Accounting description],case when XFinal.SNO = 1 then  XFinal.Amount else Null end Amount, XFinal.[Transation Type Ref] as [Ref Transaction Type],XFinal.[Document No Ref] as [Ref Document No] , XFinal.[To Location], XFinal.[Branch Accounting code Ref] as [Ref Branch Accounting code] ,XFinal.[Branch Accounting description Ref] as [Ref Branch Accounting description], XFinal.[Amount Ref] as [Ref Amount], XFinal.[Diff]    " & _
                  "  From ( " & _
                  "  select row_number() over(partition by tspl_mcc_dispatch_challan.Chalan_NO  order by tspl_mcc_dispatch_challan.Chalan_NO) as SNO,   'Tanker Dispatch' as [Transaction Type], tspl_mcc_dispatch_challan.Chalan_NO as [Document NO], tspl_mcc_dispatch_challan.MCC_Code as [From Location],TBL_JOURNAL.Account_code as [Branch Accounting code] , TBL_JOURNAL.Account_Desc as [Branch Accounting description] , TBL_JOURNAL.Amount, 'Milk Transfer In' as [Transation Type Ref], TSPL_MILK_TRANSFER_IN.Receipt_Challan_No as [Document No Ref],TSPL_MILK_TRANSFER_IN.location_code as [To Location] , TBL_MILK_TRANSFER_IN.Account_code as [Branch Accounting code Ref],TBL_MILK_TRANSFER_IN.Account_Desc as [Branch Accounting description Ref],TBL_MILK_TRANSFER_IN.Amount as [Amount Ref], isnull (TBL_JOURNAL.Amount,0) + isnull (TBL_Diff.Amount,0)  as [Diff]  from tspl_mcc_dispatch_challan " & _
                  "  Left Outer Join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No = tspl_mcc_dispatch_challan.Chalan_NO " & _
                  "  Left Outer Join  (Select TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_Details.Account_code,TSPL_JOURNAL_Details.Account_Desc, TSPL_JOURNAL_Details.Amount from TSPL_JOURNAL_Details left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_Details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No  inner Join (Select distinct TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING) TSPL_BRANCH_ACCOUNT_MAPPING on  TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account  = TSPL_JOURNAL_Details.Account_code)  TBL_JOURNAL on TBL_JOURNAL.Source_Doc_No = tspl_mcc_dispatch_challan.Chalan_NO " & _
                  "  left outer join  (Select TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_Details.Account_code,TSPL_JOURNAL_Details.Account_Desc, TSPL_JOURNAL_Details.Amount from TSPL_JOURNAL_Details left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_Details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No  inner Join (Select distinct TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING) TSPL_BRANCH_ACCOUNT_MAPPING on  TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account  = TSPL_JOURNAL_Details.Account_code )  TBL_MILK_TRANSFER_IN on TBL_MILK_TRANSFER_IN.Source_Doc_No = TSPL_MILK_TRANSFER_IN.Receipt_Challan_No " & _
                  "  left outer join  (Select TSPL_JOURNAL_MASTER.Source_Doc_No, sum( TSPL_JOURNAL_Details.Amount) as Amount  from TSPL_JOURNAL_Details left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_Details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No  inner Join (Select distinct TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING) TSPL_BRANCH_ACCOUNT_MAPPING on  TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account  = TSPL_JOURNAL_Details.Account_code  group by TSPL_JOURNAL_MASTER.Source_Doc_No )  TBL_Diff on TBL_Diff.Source_Doc_No = TSPL_MILK_TRANSFER_IN.Receipt_Challan_No " & _
                  "  where tspl_mcc_dispatch_challan.isPosted = 1 and convert (date,tspl_mcc_dispatch_challan.Dispatch_Date,103) > = convert (date, '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "',103)  and convert (date,tspl_mcc_dispatch_challan.Dispatch_Date,103) <= convert (date, '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "',103)   " & _
                  "  ) XFinal where 2=2 "
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                qry += " and XFinal.[Transaction Type] in (" & clsCommon.GetMulcallString(txtTransaction.arrValueMember) & ")"
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and XFinal.[From Location] in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If
            ' Transfer Out
            qry += "  Union all "

            qry += "  Select XFinal.[Transaction Type] ,XFinal.[Document NO] , XFinal.[From Location], XFinal.[Branch Accounting code], XFinal.[Branch Accounting description] , XFinal.Amount ,XFinal.[Transation Type Ref] as [Ref Transaction Type],XFinal.[Document No Ref] as [Ref Document No], XFinal.[To Location], XFinal.[Branch Accounting code]  as [Ref Branch Accounting code], XFinal.[Branch Accounting description Ref] as [Ref Branch Accounting description], XFinal.[Amount Ref] as [Ref Amount], XFinal.[Diff] from ( " & _
                   "  select   'Transfer Out' as [Transaction Type], TSPL_TRANSFER_ORDER_HEAD.Document_No as [Document NO], TSPL_TRANSFER_ORDER_HEAD.From_Location as [From Location],TBL_JOURNAL.Account_code as [Branch Accounting code] , TBL_JOURNAL.Account_Desc as [Branch Accounting description] , TBL_JOURNAL.Amount, 'Transfer In' as [Transation Type Ref], TBL_Transfer_IN.Document_No as [Document No Ref],TBL_Transfer_IN.To_Location as [To Location] , TBL_TRANSFER_IN_JE.Account_code as [Branch Accounting code Ref],TBL_TRANSFER_IN_JE.Account_Desc as [Branch Accounting description Ref],TBL_TRANSFER_IN_JE.Amount as [Amount Ref] , isnull (TBL_JOURNAL.Amount,0) + isnull (TBL_Diff.Amount,0)  as [Diff] from TSPL_TRANSFER_ORDER_HEAD  " & _
                   "  Left Outer Join  (Select TSPL_TRANSFER_ORDER_HEAD.Document_No, TransferOutNo,To_Location from TSPL_TRANSFER_ORDER_HEAD  where TSPL_TRANSFER_ORDER_HEAD.Transfer_Type = 'I' and TSPL_TRANSFER_ORDER_HEAD.Status = 1 )  TBL_Transfer_IN on TBL_Transfer_IN.TransferOutNo = TSPL_TRANSFER_ORDER_HEAD.Document_No " & _
                   "  Left Outer Join  (Select TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_Details.Account_code,max(TSPL_JOURNAL_Details.Account_Desc) as Account_Desc, sum(TSPL_JOURNAL_Details.Amount) as Amount from TSPL_JOURNAL_Details left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_Details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No  inner Join(Select  distinct TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING) TSPL_BRANCH_ACCOUNT_MAPPING on  TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account  = TSPL_JOURNAL_Details.Account_code  group by TSPL_JOURNAL_MASTER.Source_Doc_No , TSPL_JOURNAL_Details.Account_code    )  TBL_JOURNAL on TBL_JOURNAL.Source_Doc_No = TSPL_TRANSFER_ORDER_HEAD.Document_No " & _
                   "  left outer join  (Select TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_Details.Account_code,max(TSPL_JOURNAL_Details.Account_Desc) as Account_Desc, sum(TSPL_JOURNAL_Details.Amount ) as Amount from TSPL_JOURNAL_Details left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_Details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No  inner Join ( Select distinct TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING) TSPL_BRANCH_ACCOUNT_MAPPING on  TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account  = TSPL_JOURNAL_Details.Account_code  group by TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_Details.Account_code   ) TBL_TRANSFER_IN_JE on TBL_TRANSFER_IN_JE.Source_Doc_No = TBL_Transfer_IN.Document_No " & _
                   "  left outer join  (Select TSPL_JOURNAL_MASTER.Source_Doc_No, sum( TSPL_JOURNAL_Details.Amount) as Amount  from TSPL_JOURNAL_Details left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_Details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No  inner Join (Select distinct TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING) TSPL_BRANCH_ACCOUNT_MAPPING on  TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account  = TSPL_JOURNAL_Details.Account_code  group by TSPL_JOURNAL_MASTER.Source_Doc_No )  TBL_Diff on TBL_Diff.Source_Doc_No = TBL_Transfer_IN.Document_No " & _
                   "  where TSPL_TRANSFER_ORDER_HEAD.Transfer_Type = 'O' and TSPL_TRANSFER_ORDER_HEAD.Status = 1  and convert (date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) > = convert (date, '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "',103)  and convert (date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) <= convert (date, '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "',103)  " & _
                   "  ) XFinal where 2=2   "
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                qry += " and  XFinal.[Transaction Type] in (" & clsCommon.GetMulcallString(txtTransaction.arrValueMember) & ")"
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and XFinal.[From Location] in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If

            ' 
            qry += " Union All "
            qry += " Select XFinal.[Transaction Type],XFinal.[Document NO],XFinal.[From Location] , XFinal.[Branch Accounting code],XFinal.[Branch Accounting description], XFinal.Amount as Amount,'' as [Ref Transaction Type],'' as [Ref Document No] , '' as [To Location], '' as  [Ref Branch Accounting code] ,'' as [Ref Branch Accounting description], Null as [Ref Amount], Null as [Diff] from (Select 'GENERAL ENTRY' as [Transaction Type], TSPL_JOURNAL_MASTER.Voucher_No as [Document NO], max(TSPL_JOURNAL_MASTER.Segment_code) as [From Location] , TSPL_JOURNAL_Details.Account_code as [Branch Accounting code],max(TSPL_JOURNAL_Details.Account_Desc) as [Branch Accounting description], sum(TSPL_JOURNAL_Details.Amount) as Amount from TSPL_JOURNAL_Details left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_Details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No  inner Join(Select  distinct TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING) TSPL_BRANCH_ACCOUNT_MAPPING on  TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account  = TSPL_JOURNAL_Details.Account_code  where TSPL_JOURNAL_MASTER.Source_Code = 'GL-JE'  and convert (date,TSPL_JOURNAL_MASTER.Voucher_Date,103) > = convert (date, '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "',103)  and convert (date,TSPL_JOURNAL_MASTER.Voucher_Date,103) <= convert (date, '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "',103)   " & _
                   " group by TSPL_JOURNAL_MASTER.Voucher_No , TSPL_JOURNAL_Details.Account_code " & _
                   " ) XFinal where 2=2  "
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                qry += " and XFinal.[Transaction Type] in (" & clsCommon.GetMulcallString(txtTransaction.arrValueMember) & ")"
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and XFinal.[From Location] in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If

            ' Payment 

            qry += " Union All "
            qry += " Select   XFinal.[Transaction Type],XFinal.[Document NO],XFinal.[From Location] , XFinal.[Branch Accounting code],XFinal.[Branch Accounting description]  ,XFinal.Amount  , 'Payment' as [Ref Transaction Type],TBL_JOURNAL2.Source_Doc_No as [Ref Document No] , TBL_JOURNAL2.[To Location], TBL_JOURNAL2.Account_code as [Ref Branch Accounting code] ,TBL_JOURNAL2.Account_Desc as [Ref Branch Accounting description], TBL_JOURNAL2.Amount as [Ref Amount], 0 as [Diff]    " & _
                   " From ( " & _
                   " select row_number() over(partition by TSPL_PAYMENT_HEADER.payment_no  order by TSPL_PAYMENT_HEADER.payment_no) as SNO,   'PAYMENT' as [Transaction Type] , TSPL_PAYMENT_HEADER.payment_no as [Document NO], TBL_JOURNAL.[From Location] as [From Location],TBL_JOURNAL.Account_code as [Branch Accounting code] , TBL_JOURNAL.Account_Desc as [Branch Accounting description] , TBL_JOURNAL.Amount from TSPL_PAYMENT_HEADER  " & _
                   " inner Join  (Select TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_Details.Account_code,max(TSPL_JOURNAL_Details.Account_Desc) as Account_Desc,max(TSPL_JOURNAL_Details.Account_Seg_Code7) as [From Location] ,sum (TSPL_JOURNAL_Details.Amount) as Amount from TSPL_JOURNAL_Details left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_Details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No   inner Join (Select distinct TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING) TSPL_BRANCH_ACCOUNT_MAPPING on  TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account  = TSPL_JOURNAL_Details.Account_code  where  TSPL_JOURNAL_DETAILS.Amount >=0  group by TSPL_JOURNAL_MASTER.Source_Doc_No ,TSPL_JOURNAL_Details.Account_code)  TBL_JOURNAL on TBL_JOURNAL.Source_Doc_No = TSPL_PAYMENT_HEADER.payment_no   where TSPL_PAYMENT_HEADER.Posted = 1  and convert (date,TSPL_PAYMENT_HEADER.Payment_Date,103) > = convert (date, '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "',103)  and convert (date,TSPL_PAYMENT_HEADER.Payment_Date,103) <= convert (date, '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "',103)  " & _
                   "  ) XFinal " & _
                   " left outer Join (Select TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_Details.Account_code,max(TSPL_JOURNAL_Details.Account_Desc) as Account_Desc,max(TSPL_JOURNAL_Details.Account_Seg_Code7) as [To Location] ,sum(TSPL_JOURNAL_Details.Amount) as Amount from TSPL_JOURNAL_Details left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_Details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No   inner Join (Select distinct TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING) TSPL_BRANCH_ACCOUNT_MAPPING on  TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account  = TSPL_JOURNAL_Details.Account_code  where  TSPL_JOURNAL_DETAILS.Amount < 0  group by TSPL_JOURNAL_MASTER.Source_Doc_No ,TSPL_JOURNAL_Details.Account_code)  TBL_JOURNAL2 on TBL_JOURNAL2.Source_Doc_No = XFinal.[Document NO] and XFinal.Amount = -1*TBL_JOURNAL2.Amount " & _
                   " where 2=2 "

            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                qry += " and XFinal.[Transaction Type] in (" & clsCommon.GetMulcallString(txtTransaction.arrValueMember) & ")"
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and XFinal.[From Location] in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If

            'RECEIPT
            qry += " Union all "
            qry += " Select   XFinal.[Transaction Type],XFinal.[Document NO],XFinal.[From Location] , XFinal.[Branch Accounting code],XFinal.[Branch Accounting description]  ,XFinal.Amount  , 'RECEIPT' as [Ref Transaction Type],TBL_JOURNAL2.Source_Doc_No as [Ref Document No] , TBL_JOURNAL2.[To Location], TBL_JOURNAL2.Account_code as [Ref Branch Accounting code] ,TBL_JOURNAL2.Account_Desc as [Ref Branch Accounting description], TBL_JOURNAL2.Amount as [Ref Amount], 0 as [Diff]    " & _
                   " From ( " & _
                   " select row_number() over(partition by TSPL_RECEIPT_HEADER.receipt_no  order by TSPL_RECEIPT_HEADER.receipt_no) as SNO,   'RECEIPT' as [Transaction Type] , TSPL_RECEIPT_HEADER.receipt_no as [Document NO], TBL_JOURNAL.[From Location] as [From Location], TBL_JOURNAL.Account_code as [Branch Accounting code] , TBL_JOURNAL.Account_Desc as [Branch Accounting description] , TBL_JOURNAL.Amount from TSPL_RECEIPT_HEADER " & _
                   " inner Join  (Select TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_Details.Account_code,max(TSPL_JOURNAL_Details.Account_Desc) as Account_Desc,max(TSPL_JOURNAL_Details.Account_Seg_Code7) as [From Location] ,sum (TSPL_JOURNAL_Details.Amount) as Amount from TSPL_JOURNAL_Details left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_Details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No   inner Join (Select distinct TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING) TSPL_BRANCH_ACCOUNT_MAPPING on  TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account  = TSPL_JOURNAL_Details.Account_code  where  TSPL_JOURNAL_DETAILS.Amount >=0  group by TSPL_JOURNAL_MASTER.Source_Doc_No ,TSPL_JOURNAL_Details.Account_code)  TBL_JOURNAL on TBL_JOURNAL.Source_Doc_No = TSPL_RECEIPT_HEADER.receipt_no  " & _
                   " where TSPL_RECEIPT_HEADER.Posted = 'Y'  and convert (date,TSPL_RECEIPT_HEADER.Receipt_Date,103) > = convert (date, '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "',103)  and convert (date,TSPL_RECEIPT_HEADER.Receipt_Date,103) <= convert (date, '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "',103)   " & _
                   " ) XFinal " & _
                   " left outer Join (Select TSPL_JOURNAL_MASTER.Source_Doc_No, TSPL_JOURNAL_Details.Account_code,max(TSPL_JOURNAL_Details.Account_Desc) as Account_Desc,max(TSPL_JOURNAL_Details.Account_Seg_Code7) as [To Location] ,sum(TSPL_JOURNAL_Details.Amount) as Amount from TSPL_JOURNAL_Details left Outer Join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_Details.Voucher_No = TSPL_JOURNAL_MASTER.Voucher_No   inner Join (Select distinct TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING) TSPL_BRANCH_ACCOUNT_MAPPING on  TSPL_BRANCH_ACCOUNT_MAPPING.Branch_Account  = TSPL_JOURNAL_Details.Account_code  where  TSPL_JOURNAL_DETAILS.Amount < 0  group by TSPL_JOURNAL_MASTER.Source_Doc_No ,TSPL_JOURNAL_Details.Account_code)  TBL_JOURNAL2 on TBL_JOURNAL2.Source_Doc_No = XFinal.[Document NO] and XFinal.Amount = -1*TBL_JOURNAL2.Amount " & _
                   " where 2=2 "
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                qry += " and XFinal.[Transaction Type] in (" & clsCommon.GetMulcallString(txtTransaction.arrValueMember) & ")"
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and XFinal.[From Location] in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ")"
            End If

            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Columns.Clear()
                gv.Rows.Clear()
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                gv.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2
                gv.AllowAddNewRow = False
                gv.EnableFiltering = True
                gv.ReadOnly = True
                    'formatGrid()
                gv.Columns("Transaction Type").IsPinned = True

                gv.Columns("Ref Amount").IsPinned = True
                gv.Columns("Ref Amount").PinPosition = PinnedColumnPosition.Right

                gv.Columns("Diff").IsPinned = True
                gv.Columns("Diff").PinPosition = PinnedColumnPosition.Right

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim amount As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(amount)
                Dim Refamount As New GridViewSummaryItem("Ref Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Refamount)

                Dim diff As New GridViewSummaryItem("Diff", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(diff)

                    'gv.MasterTemplate.SummaryRowsBottom.p()

                    'GRIDVIEWBOTTOMPINNEDROWSMODE.FIXED()

                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                    'gv.MasterTemplate.bo = GridViewBottomPinnedRowsMode.Fixed



                gv.BestFitColumns()
                ReStoreGridLayout()
                fromDate.Enabled = False
                ToDate.Enabled = False
                txtTransaction.Enabled = False
                txtLocation.Enabled = False
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)

            End If



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub formatGrid()
        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False

        Next
        gv.Columns("TransName").IsVisible = True
        gv.Columns("TransName").Width = 100
        gv.Columns("TransName").HeaderText = "Transaction Type"

        gv.Columns("DocNo").IsVisible = True
        gv.Columns("DocNo").Width = 100
        gv.Columns("DocNo").HeaderText = "Document No"

        gv.Columns("Fromloc").IsVisible = True
        gv.Columns("Fromloc").Width = 80
        gv.Columns("Fromloc").HeaderText = "From Location"

        gv.Columns("BranchCode").IsVisible = True
        gv.Columns("BranchCode").Width = 120
        gv.Columns("BranchCode").HeaderText = "Branch Accounting Code"

        gv.Columns("BranchDesc").IsVisible = True
        gv.Columns("BranchDesc").Width = 120
        gv.Columns("BranchDesc").HeaderText = "Branch Accounting Desc"

        gv.Columns("BranchAmt").IsVisible = True
        gv.Columns("BranchAmt").Width = 100
        gv.Columns("BranchAmt").HeaderText = "Amount"

        gv.Columns("Transfer_No").IsVisible = True
        gv.Columns("Transfer_No").Width = 120
        gv.Columns("Transfer_No").HeaderText = "Document"

        gv.Columns("Account_code1").IsVisible = True
        gv.Columns("Account_code1").Width = 120
        gv.Columns("Account_code1").HeaderText = "Branch Accounting Code"

        gv.Columns("Account_Desc1").IsVisible = True
        gv.Columns("Account_Desc1").Width = 120
        gv.Columns("Account_Desc1").HeaderText = "Branch Accounting Desc"

        gv.Columns("Amount1").IsVisible = True
        gv.Columns("Amount1").Width = 100
        gv.Columns("Amount1").HeaderText = "Amount"
        gv.BestFitColumns()

    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " Select xxx.Code,  xxx.Name From (" & _
                                 " Select distinct 'Bank Transfer' As Code,    'Bank Transfer' As Name from TSPL_BANK_TRANSFER " & _
                                 " Union All Select distinct 'Tanker Dispatch' As Code,    'Tanker Dispatch' As Name from TSPL_BANK_TRANSFER " & _
                                 " Union All Select distinct 'Transfer Out' As Code,    'Transfer Out' As Name from TSPL_BANK_TRANSFER " & _
                                 " Union All Select distinct 'GENERAL ENTRY' As Code,    'GENERAL ENTRY' As Name from TSPL_BANK_TRANSFER  " & _
                                 " Union All Select distinct 'PAYMENT' As Code,    'PAYMENT' As Name from TSPL_BANK_TRANSFER  " & _
                                 " Union All Select distinct 'RECEIPT' As Code,    'RECEIPT' As Name from TSPL_BANK_TRANSFER  " & _
                                 " ) xxx"
        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulPur", qry, "Name", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim stateCond As String = ""
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where location_type IN  ('Physical','Virtual') " & stateCond & "  "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptBranchAccountMapping & "'"))

                If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                    arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
                End If

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If

                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            'If e.Column Is gv.Columns("Item Code") OrElse e.Column Is Gv1.Columns("Item Desc") Then
            '    Dim itemcode As String = ""
            '    itemcode = clsCommon.myCstr(Gv1.CurrentRow.Cells("Item Code").Value)
            '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmItemMasterRMOther, itemcode)
            If e.Column Is gv.Columns("Document NO") Then
                DrillDown()
            ElseIf e.Column Is gv.Columns("Ref Document NO") Then
                DrillDown2()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ' Tanker Dispatch,Transfer Out, GENERAL ENTRY, PAYMENT, RECEIPT, Bank Transfer
    Sub DrillDown()
        Try
            If gv.CurrentRow.Index >= 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells("Transaction Type").Value), "Tanker Dispatch") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, clsCommon.myCstr(gv.CurrentRow.Cells("Document NO").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells("Transaction Type").Value), "Transfer Out") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, clsCommon.myCstr(gv.CurrentRow.Cells("Document NO").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells("Transaction Type").Value), "GENERAL ENTRY") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, clsCommon.myCstr(gv.CurrentRow.Cells("Document NO").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells("Transaction Type").Value), "PAYMENT") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, clsCommon.myCstr(gv.CurrentRow.Cells("Document NO").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells("Transaction Type").Value), "RECEIPT") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, clsCommon.myCstr(gv.CurrentRow.Cells("Document NO").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells("Transaction Type").Value), "Bank Transfer") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.bankTransfer, clsCommon.myCstr(gv.CurrentRow.Cells("Document NO").Value))
                
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub DrillDown2()
        Try
            If gv.CurrentRow.Index >= 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells("Ref Transaction Type").Value), "Tanker Dispatch") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, clsCommon.myCstr(gv.CurrentRow.Cells("Ref Document NO").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells("Ref Transaction Type").Value), "Transfer In") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, clsCommon.myCstr(gv.CurrentRow.Cells("Ref Document NO").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells("Ref Transaction Type").Value), "GENERAL ENTRY") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, clsCommon.myCstr(gv.CurrentRow.Cells("Ref Document NO").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells("Ref Transaction Type").Value), "PAYMENT") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, clsCommon.myCstr(gv.CurrentRow.Cells("Ref Document NO").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells("Ref Transaction Type").Value), "RECEIPT") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, clsCommon.myCstr(gv.CurrentRow.Cells("Ref Document NO").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells("Ref Transaction Type").Value), "Bank Transfer") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.bankTransfer, clsCommon.myCstr(gv.CurrentRow.Cells("Ref Document NO").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells("Ref Transaction Type").Value), "Milk Transfer In") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, clsCommon.myCstr(gv.CurrentRow.Cells("Ref Document NO").Value))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class
