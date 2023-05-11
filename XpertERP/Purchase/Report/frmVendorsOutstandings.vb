Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports System.Net.Mail
Imports System.Net


Public Class FrmVendorsOutstandings
    Inherits FrmMainTranScreen

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        dtpFromDate.Value = clsCommon.GETSERVERDATE
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Sub LoadVendor()

        Dim strqry As String = "select Vendor_Code as [Vendor Number],Vendor_Name as [Vendor Name] from TSPL_VENDOR_MASTER where Email <> ''"
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(strqry)
        cbgVendor.ValueMember = "Vendor Number"
        cbgVendor.DisplayMember = "Vendor Name"
    End Sub

    Private Sub FrmVendorsOutstandings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        dtpFromDate.Value = clsCommon.GETSERVERDATE
        LoadVendor()
        btnPrint.Visible = False
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmVendorsOutstandings)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag

    End Sub
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'If cbgVendor.CheckedValue.Count <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast one vendor")
        '    Exit Sub
        'Else
        '    Print()
        'End If

    End Sub
    Private Function GetQuery(ByVal FrmDate As String, ByVal ToDate As String, ByVal isforOpBal As Boolean) As String
        Dim qry As String = ""
        Dim qrydetail As String

        qrydetail = " select vcode,TSPL_VENDOR_MASTER.Vendor_Name as VName,DocNo,DocType,DocDate,DocNarr,ChequeDetails,CrAmt,DrAmt, (CrAmt-DrAmt) as EffectiveAmt, Document_Type,'" + FrmDate + "' as FilterFromDate,'" + ToDate + "' as FilterToDate,Balance_Amount ,Comp_Name ,Logo_Img ,Logo_Img2, substring(account,LEN(account)-2,3) as account,Posting_Date,GLDocType ,"

        qrydetail += "(Select TOP(1) Case when (len(TSPL_COMPANY_MASTER .Add1)>0) then convert(varchar(30),TSPL_COMPANY_MASTER.Add1,103) else '' end +  case when (len(TSPL_COMPANY_MASTER.Add2)>0) then ', '+ convert(varchar(30),TSPL_COMPANY_MASTER.Add2,103)  else  '' end +  case when (len(TSPL_COMPANY_MASTER.Add3)>0) then ', '+convert(varchar(30),TSPL_COMPANY_MASTER.Add3,103)  else  '' end +  case when (len(TSPL_COMPANY_MASTER.City_Code )>0) then ', '+convert(varchar(20),TSPL_COMPANY_MASTER.City_Code,103) else  ''  end +  case when (len(TSPL_COMPANY_MASTER.State )>0) then ', '+convert(varchar(20),TSPL_COMPANY_MASTER.State,103)  else  ''  end +  case when (len(TSPL_COMPANY_MASTER.Pincode )>0) then ', '+convert(varchar(10),TSPL_COMPANY_MASTER.Pincode,103)  else  ''  end  as Address  from TSPL_COMPANY_MASTER where Comp_Code =('" + objCommonVar.CurrentCompanyCode + "')) as Address, TSPL_VENDOR_MASTER.Vendor_Group_Code, TSPL_VENDOR_GROUP.Group_Desc"


        '---------------------By Vipin for Location code---------------
        qrydetail += " from( select vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD .Document_No as DocNo ,case when Document_Type='D' then 'Debit Note' else case when Document_Type='C' then 'Credit Note' else 'AP Invoice' end  end as DocType,convert(date,Invoice_Entry_Date,103) as DocDate,((Description) + (case when Description='' then '' else ' - 'end) +(RefDocNo)+Vendor_Invoice_No ) as DocNarr,'' as ChequeDetails,((case when Document_Type IN('I','C') AND TAX1_Amt<0 then (-1*TAX1_Amt) else 0 end + case when Document_Type IN('I','C') AND TAX2_Amt<0 then (-1*TAX2_Amt)  else 0   end + case when Document_Type IN('I','C') AND TAX3_Amt<0 then (-1*TAX3_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX4_Amt<0 then (-1*TAX4_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX5_Amt<0 then (-1*TAX5_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX6_Amt<0 then (-1*TAX6_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX7_Amt<0 then (-1*TAX7_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX8_Amt<0 then (-1*TAX8_Amt) else 0  end + case when Document_Type IN('I','C') AND TAX9_Amt<0 then (-1*TAX9_Amt) else  0  end + case when Document_Type IN('I','C') AND TAX10_Amt<0 then (-1*TAX10_Amt) else 0 end)+case when Document_Type IN('I','C') then document_total else 0 end  ) as CrAmt, case when Document_Type IN('D') then Document_Total else 0 end as DrAmt,Document_Type,balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No ) as account, convert(date,tspl_vendor_invoice_head.Posting_Date,103) as Posting_Date ,'AP-IN' as GLDocType    from tspl_vendor_invoice_head    left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>''  and LEN(ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,''))<=0 " + Environment.NewLine
        For i As Integer = 1 To 10
            qrydetail += " Union All select vendor_code as VCode,vendor_name as VName,TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo ,TAX" + clsCommon.myCstr(i) + " as DocType,convert(date,Invoice_Entry_Date, 103) as DocDate,TSPL_TAX_MASTER.Tax_Code_Desc as DocNarr,'' as ChequeDetails, 0 as CrAmt, (-1* TAX" + clsCommon.myCstr(i) + "_Amt) as DrAmt, Document_Type,balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select   GL_Account_Code from TSPL_VENDOR_INVOICE_DETAIL where Detail_Line_No='1' and Document_No=tspl_vendor_invoice_head.Document_No ) as account, tspl_vendor_invoice_head.Posting_Date,'AP-IN' as GLDocType  from tspl_vendor_invoice_head   left outer join TSPL_COMPANY_MASTER on tspl_vendor_invoice_head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  Left Outer Join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=tspl_vendor_invoice_head.TAX" + clsCommon.myCstr(i) + "  where ISNULL(tspl_vendor_invoice_head.Posting_Date,'')<>'' AND TAX" + clsCommon.myCstr(i) + "_Amt<0  " + Environment.NewLine
        Next

        'qrydetail += " union all select TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' - '+'SRN No-'+TSPL_PI_HEAD.Against_SRN+ ' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No ) as DocNarr,'' as ChequeDetails, Pi_total_amt+ISNULL( Tot_Empty_Amount,0)  as CrAmt, 0  as DrAmt,'I' as Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date  from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No  left outer join TSPL_COMPANY_MASTER on TSPL_PI_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1 " + Environment.NewLine

        qrydetail += " union all select TSPL_PI_HEAD.Vendor_Code as VCode,TSPL_PI_HEAD.Vendor_Name as VName, TSPL_VENDOR_INVOICE_HEAD.Document_No as DocNo,'Pur.Invoice' as DocType,convert(date,TSPL_PI_HEAD.PI_Date,103) as DocDate,(TSPL_PJV_HEAD.PJV_No+' - '+'SRN No-'+TSPL_PI_HEAD.Against_SRN+ ' ,Vendor Invoice No-'+ TSPL_PI_HEAD.Vendor_Invoice_No ) as DocNarr,'' as ChequeDetails, TSPL_VENDOR_INVOICE_HEAD.Document_Total as CrAmt, 0  as DrAmt,'I' as Document_Type, TSPL_VENDOR_INVOICE_HEAD.Balance_Amt as Balance_Amount , TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,( select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code=TSPL_PI_HEAD.Bill_To_Location) as account, convert(date,TSPL_PI_HEAD.Posting_Date,103) as Posting_Date ,'AP-IN' as GLDocType from TSPL_PI_HEAD  left outer join TSPL_PJV_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PJV_HEAD.Invoice_No  left outer join TSPL_COMPANY_MASTER on TSPL_PI_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No=TSPL_PI_HEAD.Vendor_Invoice_No and TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No=TSPL_PI_HEAD.PI_No where TSPL_PI_HEAD.Status=1 " + Environment.NewLine

        qrydetail += " union all select TSPL_REMITTANCE.Vendor_Code , TSPL_REMITTANCE.Vendor_Name , TSPL_REMITTANCE.Document_No ,'TDS' as [DocType],convert(date,Document_Date,103)as Document_Date,'', '',case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then 0 else Actual_Total_TDS END, case when TSPL_REMITTANCE.Document_Type IN('I','C','OA','AV') AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No,'')='' then Actual_Total_TDS else 0 END  ,'TDS',0,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2, right( Branch_GL_AC,3) as account,'' as Posting_Date,case when TSPL_REMITTANCE.Document_Type in ('D','C','I') then 'AP-IN' else case when TSPL_REMITTANCE.Document_Type in ('AV','OA','RC') then 'AP-PY' else '' end end as GLDocType from TSPL_REMITTANCE Left Outer Join TSPL_VENDOR_INVOICE_HEAD ON TSPL_REMITTANCE.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No left outer join TSPL_COMPANY_MASTER on TSPL_REMITTANCE.Comp_Code  =TSPL_COMPANY_MASTER.Comp_Code where Remit_TDS is not null   and   Branch_GL_AC  is not null  and Actual_Total_TDS<>0 " + Environment.NewLine


        qrydetail += " union all select TSPL_REMITTANCE.Vendor_Code ,TSPL_REMITTANCE.Vendor_Name ,TSPL_REMITTANCE.Document_No ,'TDS REVERSE' as [DocType],convert(date,TSPL_BANK_REVERSE.Reversal_Date,103)as Document_Date,'', '',0 AS CrAmt,   case when (TSPL_PAYMENT_HEADER.Cheque_No=TSPL_BANK_REVERSE.Document_No) then Actual_Total_TDS else null end  as DrAmt ,'TDS',0,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2, right( Branch_GL_AC,3) as account,'' as Posting_Date,'RV-TA' as GLDocType from TSPL_REMITTANCE left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Cheque_No=TSPL_REMITTANCE.Document_No    inner join  TSPL_BANK_REVERSE on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No  left outer join TSPL_COMPANY_MASTER on  TSPL_REMITTANCE.comp_code=TSPL_COMPANY_MASTER.Comp_Code where Remit_TDS is not null and TSPL_BANK_REVERSE.Post ='P'  and   Branch_GL_AC  is not null and Actual_Total_TDS<>0 " + Environment.NewLine


        'for the condition of RC type by vipin 10/12/2012

        qrydetail += " union all select TSPL_BANK_REVERSE.vendor_code as VCode,TSPL_BANK_REVERSE.vendor_name as VName,Reverse_Code as DocNo, 'Reverse Payment' as [DocType],convert(date,Reversal_Date, 103) as DocDate,TSPL_BANK_REVERSE.Document_No as DocNarr,(TSPL_BANK_REVERSE.cheque_No+'-'+CONVERT(VARCHAR,Pay_Rec_Date, 103) )as ChequeDetails, case when   TSPL_PAYMENT_HEADER.Payment_Type='RC' then 0 ELSE TSPL_BANK_REVERSE.Amount end as CrAmt, case when TSPL_PAYMENT_HEADER.Payment_Type='RC' then TSPL_BANK_REVERSE.Amount else 0 end  as DrAmt,'RV'as Document_Type, amount as Balance_Amount,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,right(BANKACC,3) account,'' as Posting_Date,'RV-TA' as GLDocType  from TSPL_BANK_REVERSE  LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No = TSPL_BANK_REVERSE.Document_No left outer join TSPL_COMPANY_MASTER on TSPL_BANK_REVERSE .Comp_Code  =TSPL_COMPANY_MASTER.Comp_Code  left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_REVERSE.Bank_Code where Source_Type ='AP' And Post='P' AND TSPL_BANK_REVERSE.Vendor_Code<> '' AND TSPL_PAYMENT_HEADER.IsChkReverse='Y'  " + Environment.NewLine

        qrydetail += " union all select  VC_Code as VCode,VC_Name as VName,Document_No as DocNo,case when Document_Type='V' then 'Vendor' else '' end as DocType,CONVERT(Date,Document_Date,103) as DocDate,Remarks as DocNarr,'' as ChequeDetails,(case when Amount_Type='Dr' then Amount else 0 end) as CrAmt,(case when Amount_Type='Cr' then Amount else 0 end) as DrAmt,(case when  Document_Type='V' then 'Vendor' else '' end ) as Document_Type ,0 as Balance_Amount, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,(TSPL_VCGL_Head.Location_Segment) as account,'' as Posting_Date,'VC-GL' as GLDocType      from TSPL_VCGL_Head   left outer join TSPL_COMPANY_MASTER on TSPL_VCGL_Head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code where Document_Type='v' and TSPL_VCGL_Head.Status='1' " + Environment.NewLine

        qrydetail += " union all select TSPL_VCGL_Detail.VCGL_Code as VCode,TSPL_VCGL_Detail.VCGL_Name as VName,TSPL_VCGL_Detail.Document_No as DocNo, TSPL_VCGL_Detail.Row_Type as DocType,CONVERT(date,TSPL_VCGL_Head.Document_Date,103) as DocDate,TSPL_VCGL_Head.Remarks as DocNarr,'' as ChequeDetails,TSPL_VCGL_Detail.Cr_Amount as CrAmt,TSPL_VCGL_Detail.Dr_Amount as DrAmt, TSPL_VCGL_Detail.Row_Type as Document_Type,0 as Balance_Amount, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,(TSPL_VCGL_Head.Location_Segment) as account,'' as Posting_Date ,'VC-GL' as GLDocType  from  TSPL_VCGL_Detail left outer join  TSPL_VCGL_Head on TSPL_VCGL_Head.Document_No=TSPL_VCGL_Detail.Document_No   left outer join TSPL_COMPANY_MASTER on TSPL_VCGL_Head.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code    where Row_Type='Vendor'  and TSPL_VCGL_Head.Status='1' " + Environment.NewLine

        qrydetail += " union all select vendor_code as VCode,vendor_name as VName,payment_no as DocNo ,case when   Payment_Type='AV' then 'Advance' else case when Payment_Type='OA' then 'On Account' else case  when Payment_Type='PY' then 'Payment' else case when Payment_Type='RC' then 'Receipt' else 'Mislleneous' end end end   end as DocType,COnvert(date,payment_date, 103) as DocDate,((entry_desc)+(case when entry_desc='' then '' else ' - 'end)+(reference)+(case when reference='' then '' else ' - 'end)+  ( narration)) as DocNarr,((Cheque_No) + (case when Cheque_No='' then '' else ' - ' end) +CONVERT(Varchar, Cheque_Date, 103)) as ChequeDetails,case when payment_type in ('RC') then Payment_Amount else 0 end as CrAmt ,case when payment_type IN('PY','OA','AV','MI') then Payment_Amount  else 0 end as DrAmt, Payment_Type as Document_Type,Payment_Amount as Balance_Amount, TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,right(TSPL_BANK_MASTER.BANKACC,3) account,'' as Posting_Date,case when Payment_Type in ('PY','AV','OA','RC') then 'AP-PY' else case when Payment_Type in ('MI') then 'AP-MI' else '' end  end as GLDocType    from tspl_payment_header  left outer join TSPL_COMPANY_MASTER on tspl_payment_header.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_BANK_MASTER on tspl_payment_header.Bank_Code=TSPL_BANK_MASTER.BANK_CODE Where (Posted='P' or Posted='1'))final LEFT OUTER JOIN TSPL_VENDOR_MASTER on final.VCode=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_VENDOR_GROUP ON TSPL_VENDOR_MASTER.Vendor_Group_Code=TSPL_VENDOR_GROUP.Ven_Group_Code " + Environment.NewLine

        qry += qrydetail

        qry += " where final.Document_Type in ('TDS','I','C','D','AV','OA','PY','MI','RV','Vendor','RC') and CONVERT(date,DocDate ,103)  <=CONVERT(date,'" + dtpFromDate.Value + "',103) "




        qry += " and vcode in(" + (clsCommon.GetMulcallString(cbgVendor.CheckedValue)) + ")"

        qry += " and DocType <>'Mislleneous'"


        qry = "select VCode,VName, (CrAmt-DrAmt) as EffectiveAmt from( select  VCode,MAX(VName) as VName,SUM(CrAmt) as CrAmt,SUM(DrAmt) as DrAmt,MAX(FilterFromDate) as FilterFromDate,MAX(FilterToDate) as FilterToDate,MAX(Document_Type) as Document_Type,MAX(Address) as Address,MAX(Comp_Name) as CompName,MAX(Vendor_Group_Code) as Vendor_Group_Code, MAX(Group_Desc) as Vendor_Group_Desc from(" + qry + "  )xxx group by VCode )xxxxx"



        Return qry
    End Function

    'Sub Print()
    '    Dim strEmail As String
    '    Dim strVendor, strVendorName As String
    '    Dim decAmt As Decimal
    '    Dim dt As DataTable
    '    Try
    '        Dim FrmDate As String = clsCommon.myCstr(clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy"))
    '        Dim filterFromDate As String = clsCommon.myCstr(clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy"))
    '        Dim qry = GetQuery(FrmDate, "", True)
    '        dt = New DataTable

    '        dt = clsDBFuncationality.GetDataTable(qry)

    '        If Process.GetProcessesByName("OutLook").Length < 1 Then
    '            'restarts the Process
    '            Process.Start("OutLook.exe")
    '        End If
    '        Dim oApp As New Outlook.Application()
    '        Dim oMsg As Outlook.MailItem
    '        Dim blnmail As Boolean = False

    '        For Each dr As DataRow In dt.Rows
    '            decAmt = 0
    '            strVendor = clsCommon.myCstr(dr("Vcode"))
    '            strVendorName = clsCommon.myCstr(dr("VName"))
    '            decAmt = clsCommon.myCdbl(dr("EffectiveAmt"))
    '            If decAmt > 0 Then
    '                oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
    '                strEmail = clsDBFuncationality.getSingleValue("select Email,Vendor_Code from TSPL_VENDOR_MASTER where Vendor_Code ='" + strVendor + "' ")
    '                oMsg.Body = "Hello  " & strVendorName & "  your Rs. " & decAmt & "  outstanding amount is due as on  " & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy")
    '                oMsg.Subject = "Outdtanding Amount"
    '                'Dim ArrReceivers As New List(Of String)
    '                'ArrReceivers.Add(strEmail)
    '                oMsg.Recipients.Add(strEmail)
    '                'For ii As Integer = 0 To ArrReceivers.Count - 1
    '                '    oMsg.Recipients.Add(ArrReceivers(ii))
    '                'Next

    '                oMsg.Send()
    '                blnmail = True
    '            End If
    '        Next
    '        If blnmail = True Then
    '            RadMessageBox.Show("Mail sent successfully")
    '        End If
    '        oMsg = Nothing
    '        oApp = Nothing

    '    Catch ex As Exception
    '        RadMessageBox.Show(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnClose_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
