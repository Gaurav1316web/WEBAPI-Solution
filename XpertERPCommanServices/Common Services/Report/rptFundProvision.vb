Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Public Class rptFundProvision
    Inherits FrmMainTranScreen
    Private Sub rptFundProvision_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ftrdate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Private Sub FormatGrid()
        gv1BnkDetail.AllowAddNewRow = False
        gv1BnkDetail.TableElement.TableHeaderHeight = 40
        gv1BnkDetail.MasterTemplate.ShowRowHeaderColumn = False
        gv1BnkDetail.EnableFiltering = True
        For ii As Integer = 0 To gv1BnkDetail.Columns.Count - 1
            gv1BnkDetail.Columns(ii).ReadOnly = True
            gv1BnkDetail.Columns(ii).IsVisible = False
        Next

        gv1BnkDetail.Columns("BANK_CODE").IsVisible = True
        gv1BnkDetail.Columns("BANK_CODE").Width = 121
        gv1BnkDetail.Columns("BANK_CODE").HeaderText = "Bank Code"

        gv1BnkDetail.Columns("DESCRIPTION").IsVisible = True
        gv1BnkDetail.Columns("DESCRIPTION").Width = 251
        gv1BnkDetail.Columns("DESCRIPTION").HeaderText = "Bank Name"

        gv1BnkDetail.Columns("BankType").IsVisible = False
        gv1BnkDetail.Columns("BankType").Width = 121
        gv1BnkDetail.Columns("BankType").HeaderText = "Bank Type"

        gv1BnkDetail.Columns("Startdate").IsVisible = False
        gv1BnkDetail.Columns("Startdate").Width = 151
        gv1BnkDetail.Columns("Startdate").HeaderText = "Startdate"
        gv1BnkDetail.Columns("Startdate").FormatString = "{0:d}"

        gv1BnkDetail.Columns("EndDate").IsVisible = False
        gv1BnkDetail.Columns("EndDate").Width = 251
        gv1BnkDetail.Columns("EndDate").HeaderText = "EndDate"
        gv1BnkDetail.Columns("EndDate").FormatString = "{0:d}"

        gv1BnkDetail.Columns("RunDate").IsVisible = False
        gv1BnkDetail.Columns("RunDate").Width = 151
        gv1BnkDetail.Columns("RunDate").HeaderText = "Run Date"
        gv1BnkDetail.Columns("RunDate").FormatString = "{0:d}"

        gv1BnkDetail.Columns("BalAmt").IsVisible = True
        gv1BnkDetail.Columns("BalAmt").Width = 121
        gv1BnkDetail.Columns("BalAmt").HeaderText = "Bal Amt"

        gv1BnkDetail.Columns("Debit_Amount").IsVisible = True
        gv1BnkDetail.Columns("Debit_Amount").Width = 121
        gv1BnkDetail.Columns("Debit_Amount").HeaderText = "Debit Amount"

        gv1BnkDetail.Columns("Credit_Amount").IsVisible = True
        gv1BnkDetail.Columns("Credit_Amount").Width = 151
        gv1BnkDetail.Columns("Credit_Amount").HeaderText = "Credit Amount"


        gv1BnkDetail.Columns("Closing_Balance").IsVisible = True
        gv1BnkDetail.Columns("Closing_Balance").Width = 251
        gv1BnkDetail.Columns("Closing_Balance").HeaderText = "Closing Balance"

    End Sub
    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Try
            Dim dt As DataTable
            Dim qry As String = "Select final.*from ( SELECT BANK_CODE, MAX(DESCRIPTION) AS [DESCRIPTION], MAX(BankType) AS BankType,MAX(Startdate) AS Startdate,MAX(EndDate) AS EndDate,MAX(RunDate) AS RunDate,SUM(BalAmt) AS BalAmt,SUM(Debit_Amount) AS Debit_Amount ,SUM(Credit_Amount) AS Credit_Amount,(SUM(Debit_Amount)-SUM(Credit_Amount)+SUM(BalAmt)) AS Closing_Balance  FROM (Select  xxx.Reconciliation_Date, DocType,'Bank Book' as rptHeading, xxx.NARR_MASTER,(xxx.NARR_DETAIL + case when LEN(ISNULL(xxx.NARR_DETAIL,''))>0 and LEN(ISNULL(xxx.NARR_MASTER,''))>0  then '/' else '' end + xxx.NARR_MASTER) as NARR_DETAIL, '" + ftrdate.Value + "' as RunDate, '" + ftrdate.Value + "' as Startdate, '" + ftrdate.Value + "' as EndDate, TSPL_BANK_MASTER.BANK_CODE, TSPL_BANK_MASTER.Bank_type as BankType ,  TSPL_BANK_MASTER.DESCRIPTION , SOURCEDOC_NO as DocNo, Entry_Desc, SOURCEDOC_DATE as DocDate, ChequeNo as CHEQUE_NO , case when LEN(ISNULL(ChequeNo,''))>0 then ChequeDate else '' end as CHEQUE_DATE,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_CODE else GL_Account_Code end as CustVendorCode,  case when LEN(ISNULL( SOURCE_CODE,''))>0 then SOURCE_NAME else GL_Account_Name end as CustVendName, SOURCE_CODE, SOURCE_NAME, LOCATION_CODE, LOC_NAME , BANKGL_Account_Code , BANKGL_Account_Name, (TotDebAmt-TotCredAmt ) as BalAmt, Balance, Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else Debit_Amount end as Debit_Amount, Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount   ,  0.0 as CummulativeBal, Status, TSPL_COMPANY_MASTER.Logo_Img as Logo_Img , TSPL_COMPANY_MASTER.Logo_Img2 Logo_Img2,  (Select (TSPL_LOCATION_MASTER.Add1 + case When isnull(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+ TSPL_LOCATION_MASTER.Add2 End + Case When isnull(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+ TSPL_LOCATION_MASTER.Add3 end + case When isnull(TSPL_LOCATION_MASTER.City_Code,'') ='' then '' else ', '+ TSPL_LOCATION_MASTER.City_Code end+ Case When isnull(TSPL_LOCATION_MASTER.State,'')='' Then '' else ', '+ TSPL_STATE_MASTER.State_Name end +  Case When len(TSPL_LOCATION_MASTER.Pin_Code)<=0 Then '' Else ', '+ cast(TSPL_LOCATION_MASTER.Pin_Code as Varchar)  end) from TSPL_LOCATION_MASTER LEFT OUTER  JOIN TSPL_STATE_MASTER ON TSPL_LOCATION_MASTER.State=TSPL_STATE_MASTER.State_Code Where LEFT(TSPL_LOCATION_MASTER.Location_code,3) = (" + objCommonVar.strCurrUserLocations + "))   as Add1, TSPL_COMPANY_MASTER.Comp_Name as CompName,TransType,Payment_Code as [Payment Code],doctypefororder  From ( 
                             Select DISTINCT NULL as Reconciliation_Date,'' AS [Id], '' AS [SourceDoc_No], '' as Entry_Desc, NULL AS [SourceDoc_date], '' AS [Source_Code], '' AS [Source_Name], max(RIGHT(TSPL_BANK_MASTER.BANKACC,3)) AS [Loc_Code], '' as Payment_Code, XXX.BANK_CODE AS [Bank_Code],'' AS [Bank_Name], '' AS [Loc_Name], '' AS [BANKGL_account_Code], '' AS [BANKGL_Account_Name], '' AS [GL_Account_Code], '' AS [GL_Account_Name], '' AS [ChequeNo], '' AS [ChequeDate], '' AS [NARR_MASTER], '' AS [NARR_DETAIL], 0 AS [Debit_Amount], 0 AS [Credit_Amount],  SUM(TotCredAmt) as TotCredAmt, SUM(TotDebAmt) as  TotDebAmt, SUM(TotDebAmt)-SUM(TotCredAmt) As Balance, '' as DocType, '' as Status, 0 as orderColumn ,'' as TransType, '' as Type,max(doctypefororder) as doctypefororder From  (Select NULL as Reconciliation_Date,BANK_CODE,LOCATION_CODE, CONVERT(DECIMAL(18,2),isnull(Credit_Amount,0)* DocMaster.ConvRate-Case When Credit_Amount<>0 Then BankCharge Else 0 End) as TotCredAmt, CONVERT(DECIMAL(18,2),isnull(Debit_Amount,0)* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End) as TotDebAmt,Payment_Code,doctypefororder   from TSPL_BANK_BOOk 
							 LEFT OUTER JOIN TSPL_LOCATION_MASTER ON LEFT(TSPL_LOCATION_MASTER.Location_Code,3)=TSPL_BANK_BOOK.LOC_CODE
							 LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y' and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'0' as doctypefororder From TSPL_RECEIPT_HEADER Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'0' as doctypefororder From TSPL_PAYMENT_HEADER Union All  Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'0' as doctypefororder from TSPL_BANK_TRANSFER Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code,'0' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP' Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code,'0' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No where Source_Type='AR') as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType WHERE sourceDoc_Date < '" + ftrdate.Value + "'  AND DocMaster.Posted='Y'    ) XXX left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER .BANK_CODE =XXX.BANK_CODE  Group by XXX.BANK_CODE  UNION All 
                             Select  br.Reconciliation_Date,Id, SOURCEDOC_NO, Entry_Desc, SOURCEDOC_DATE, SOURCE_CODE, SOURCE_NAME, LOCATION_CODE,Payment_Code, TSPL_BANK_BOOK.BANK_CODE, TSPL_BANK_BOOK.BANK_NAME, LOC_NAME, BANKGL_Account_Code, BANKGL_Account_Name, GL_Account_Code, GL_Account_Name, TSPL_BANK_BOOK.CHEQUE_NO as ChequeNo, CONVERT(VARCHAR,TSPL_BANK_BOOK.CHEQUE_DATE,103) as ChequeDate, NARR_MASTER, NARR_DETAIL, CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate )-Case When Debit_Amount<>0 Then BankCharge Else 0 End as Debit_Amount, CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate )-Case When Credit_Amount<>0 Then BankCharge Else 0 End as Credit_Amount, 0 as TotCredAmt, 0 AS  TotDebAmt , (CONVERT(DECIMAL(18,2),Debit_Amount* DocMaster.ConvRate -Case When Debit_Amount<>0 Then BankCharge Else 0 End)-CONVERT(DECIMAL(18,2),Credit_Amount* DocMaster.ConvRate -Case When Credit_Amount<>0 Then BankCharge Else 0 End)) as Balance, (case when DocType='Reverse' then 'RV-TA' else 
                             case when DocType='BankTransfer' then 'BK-TF' else 
                             case when DocType='Payment' then (case when TransactionType IN('AV','OA','PY','RC') then  'AP-PY' else case when TransactionType IN('MI') then 'AP-MI' else '' end end) else  
                             case when DocType='Receipt' then (case when TransactionType ='F' then  'AR-RF' else case when TransactionType ='M' then 'AR-MI' else case when TransactionType ='O' then  'AR-OA' else case when TransactionType ='P' then  'AR-PI' else case when TransactionType ='R' then  'AR-PY' else '' end end  end end end) else '' end end end end ) as DocType, Case When BR.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status, Case When Debit_Amount>0 Then 1 Else 2 End as orderColumn,TSPL_BANK_BOOk.DocType as TransType,
                             (case when DocType='Reverse' then (Select Case When Reverse_Document='Receipts' Then 'Customer' Else 'Vendor' End from TSPL_BANK_REVERSE WHERE TSPL_BANK_REVERSE.Reverse_Code=TSPL_BANK_BOOK.SOURCEDOC_NO) when DocType='BankTransfer' then '' when DocType='Payment' then 'Vendor' when DocType='Receipt' then 'Customer' End) as [Type],doctypefororder from 
							 
							 TSPL_BANK_BOOk
							 LEFT OUTER JOIN TSPL_LOCATION_MASTER ON LEFT(TSPL_LOCATION_MASTER.Location_Code,3)=TSPL_BANK_BOOK.LOC_CODE
							 LEFT OUTER JOIN (Select Distinct tspl_BankReco_Detail.Reconciliation_Date,tspl_BankReco_Head.Bank_Code, tspl_BankReco_Detail.Document_No, tspl_BankReco_Detail.Document_Type, tspl_BankReco_Detail.Reconciliation_Status from tspl_BankReco_Detail LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id where tspl_BankReco_Head.Post='Y'  and tspl_BankReco_Detail.Reconciliation_Status='C') BR ON BR.Bank_Code=TSPL_BANK_BOOK.BANK_CODE AND BR.Document_No=TSPL_BANK_BOOK.SOURCEDOC_NO AND BR.Document_Type=TSPL_BANK_BOOK.DocType LEFT OUTER JOIN (Select Receipt_No as DocNo, Entry_Desc, Bank_Charges_Amt+Foreign_Bank_Charges_Amt*ConvRate as BankCharge, Posted, 'Receipt' as Doc_Type,TSPL_RECEIPT_HEADER.CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,TSPL_RECEIPT_HEADER.Payment_Code,'2' as doctypefororder From TSPL_RECEIPT_HEADER left outer join tspl_customer_master on tspl_customer_master.Cust_code=TSPL_RECEIPT_HEADER.cust_code where 1=1   Union All Select Payment_No  as DocNo, Entry_Desc, -Bank_Charges as BankCharge, Case When Posted=1 Then 'Y' Else 'N' End as Posted, 'Payment' as Doc_Type,CURRENCY_CODE as CURRENCY_CODE,ConvRate as ConvRate,Payment_Code,'5' as doctypefororder From TSPL_PAYMENT_HEADER
							 
							 
							 Union All  
                               -------- in query of tranfer ------ 
                             Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'1' as doctypefororder from TSPL_BANK_TRANSFER where to_bank_code in  (Select to_bank_code from TSPL_BANK_TRANSFER  ) and from_bank_code not  in  (Select from_bank_code from TSPL_BANK_TRANSFER  )  Union All 
                               -------- out query of tranfer --------- 
                             Select Transfer_No as DocNo, Description, 0 as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'BankTransfer' as Doc_Type,'INR' as CURRENCY_CODE,1 as ConvRate,Payment_Mode,'4' as doctypefororder from TSPL_BANK_TRANSFER where from_bank_code in  (Select from_bank_code from TSPL_BANK_TRANSFER  ) and to_bank_code in  (Select to_bank_code from TSPL_BANK_TRANSFER  )   Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, -PY.Bank_Charges as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,PY.CURRENCY_CODE as CURRENCY_CODE,PY.ConvRate as ConvRate,PY.Payment_Code,'3' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_PAYMENT_HEADER PY on TSPL_BANK_REVERSE.Document_No=PY.Payment_No where Source_Type='AP'   Union All  Select Reverse_Code as DocNo, Reason as Entry_Desc, (RC.Bank_Charges_Amt+RC.Foreign_Bank_Charges_Amt*RC.ConvRate) as BankCharge, Case When Post='P' Then 'Y' Else 'N' End as Posted, 'Reverse' as Doc_Type,RC.CURRENCY_CODE as CURRENCY_CODE,RC.ConvRate as ConvRate,RC.Payment_Code,'6' as doctypefororder from TSPL_BANK_REVERSE left join TSPL_RECEIPT_HEADER RC on TSPL_BANK_REVERSE.Document_No=RC.Receipt_No left outer join tspl_customer_master on tspl_customer_master.Cust_code=RC.cust_code where Source_Type='AR'  ) as DocMaster ON DocMaster.DocNo=TSPL_BANK_BOOK.SOURCEDOC_NO AND DocMaster.Doc_Type=TSPL_BANK_BOOK.DocType WHERE 1=1  AND SOURCEDOC_DATE >='" + ftrdate.Value + "'  AND  SOURCEDOC_DATE <='" + ftrdate.Value + "'  AND DocMaster.Posted='Y'   ) xxx  
							 Left Outer Join TSPL_BANK_MASTER on xxx.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE
							 LEFT OUTER JOIN TSPL_LOCATION_MASTER ON LEFT(TSPL_LOCATION_MASTER.Location_Code,3)=xxx.LOC_CODE

							 Left Outer Join TSPL_COMPANY_MASTER ON 'RCDFCF'=TSPL_COMPANY_MASTER.Comp_Code  Where 1=1  And Bank_type='B' And LOCATION_CODE = (" + objCommonVar.strCurrUserLocations + "))POP GROUP BY BANK_CODE )final Left Outer Join TSPL_COMPANY_MASTER ON 'RCDFCF'=TSPL_COMPANY_MASTER.Comp_Code
							
							 ORDER BY  BANK_CODE  

							 "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found ")
            Else '('" + objCommonVar.strCurrUserLocations + "')
                gv1BnkDetail.MasterTemplate.SummaryRowsBottom.Clear()
                gv1BnkDetail.DataSource = Nothing
                gv1BnkDetail.Rows.Clear()
                gv1BnkDetail.Columns.Clear()
                gv1BnkDetail.DataSource = dt.DefaultView
                gv1BnkDetail.EnableFiltering = True
                gv1BnkDetail.EnableSorting = True
                gv1BnkDetail.ShowFilteringRow = True
                FormatGrid()
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1BnkDetail.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1BnkDetail.Columns.Count - 1 Step ii + 1
                        gv1BnkDetail.Columns(ii).IsVisible = False
                        gv1BnkDetail.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1BnkDetail.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
End Class