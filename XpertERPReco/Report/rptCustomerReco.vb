'' ticket no . UDL/25/04/18-000126,KDI/14/05/18-000313,KDI/06/06/18-000344
Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class rptCustomerReco
    Inherits FrmMainTranScreen
#Region "Varibales"
    Dim atchqry As String = ""
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    '' new varables 
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrItem As ArrayList
    Public arrTransaction As ArrayList
    Public arrCat As Dictionary(Of String, Object) = Nothing
    Public Unit_Code As String = Nothing
    Public arrLocation As ArrayList
    Public arrCustomer As ArrayList
    Public arrCustGroup As ArrayList
    Public arrItemGroup As ArrayList
    Public boolChecked As Boolean
    Public arrGLAccount As ArrayList
    Public Stocking_Uom As Boolean = False
    '' new filters
    Dim dtCategory As DataTable
    Dim strPivotForFinalOuterQuery As String
    Dim strPivotForAddChargeFinalOutersumQuery As String
    Dim MIS_Item_Group As String = ""
    Dim arrBack As List(Of String)
    Dim Document_No As String = ""
    Dim Document_No_Old As String = ""
    Dim ERPStartDate As Date
#End Region

    Private Sub rptVendReco_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MyLabel9.Visible = True
        cboType.Visible = True
        btnBack.Visible = True
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        GetMIS_ITem_GroupColumn()
        Try
            ERPStartDate = clsCommon.GetPrintDate(objCommonVar.ERPStartDate, "dd/MMM/yyyy")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow("Invalid ERP Start Date")
            Me.Close()
        End Try
        LoadReportTypes()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        Document_No = ""
        txtLocation.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtLocation.Enabled = True
        txtCustomer.Enabled = True
        Gv1.DataSource = Nothing
        cboType.SelectedValue = "Account Wise"
        RadPageViewPage2.Text = clsCommon.myCstr(cboType.SelectedValue)
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True
        Gv1.ShowGroupPanel = True
        If isDataLoad Then
            fromDate.Value = dtFrom
            ToDate.Value = dtTo
            txtLocation.arrValueMember = arrLocation
            txtCustomer.arrValueMember = arrCustomer
            cboType.SelectedValue = strType
            Print(True)
            Me.Visible = True
        End If
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
    End Sub

    Sub LoadReportTypes()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Account Wise")
        dt.Rows.Add("Customer And Account Wise")
        dt.Rows.Add("Detail")
        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"
    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.rptCustomerReco)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Public Function CustomerBaseQry(ByVal ItemWisecheck As Boolean, ByVal IsSecurity As Boolean, ByVal SecurityType As String, ByVal Docwise As Boolean, ByVal CurrencyType As String, ByVal strCustomer As String, ByVal isOpening As Boolean, ByVal strfromdate As String, ByVal strtodate As String, Optional ByVal IsCSA As Boolean = False, Optional ByVal IsMISDebtorReport As Boolean = False, Optional ByVal IsShowApplyDocument As Boolean = False) As String
        Dim strtempBaseQryforopening As String = ""
        Dim strRunQuery As String = ""

        ''richa to exclude exc for only apply documnets KDI/14/06/2018-000364
        Dim strExcludeEXcforApplyDocumnets As String = " and InnQuery.DocNo not in ( Select Receipt_No  from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Receipt_Type ='A' and   TSPL_RECEIPT_HEADER.Posted='Y' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT >0)   "
        If isOpening = True Then
            strExcludeEXcforApplyDocumnets += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
        Else
            strExcludeEXcforApplyDocumnets += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
        End If
        If clsCommon.myLen(strCustomer) > 0 Then
            strExcludeEXcforApplyDocumnets += " and TSPL_RECEIPT_HEADER.Cust_Code in (" & strCustomer & ")"
        End If
        strExcludeEXcforApplyDocumnets += Environment.NewLine & " Union All" & Environment.NewLine & _
        " Select Reverse_Code  from TSPL_BANK_REVERSE where Document_No in ( Select Receipt_No  from TSPL_RECEIPT_HEADER where TSPL_RECEIPT_HEADER.Receipt_Type ='A' and   TSPL_RECEIPT_HEADER.Posted='Y' and (TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT >0 or TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT >0)   "
        If isOpening = True Then
            strExcludeEXcforApplyDocumnets += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <'" + strfromdate + "'  " + Environment.NewLine
        Else
            strExcludeEXcforApplyDocumnets += "  and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  >='" + strfromdate + "' and  convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)  <='" + strtodate + "' " + Environment.NewLine
        End If
        If clsCommon.myLen(strCustomer) > 0 Then
            strExcludeEXcforApplyDocumnets += " and TSPL_RECEIPT_HEADER.Cust_Code in (" & strCustomer & ")"
        End If

        strExcludeEXcforApplyDocumnets += " ) ) "


        Dim strcustomerfilter As String = String.Empty
        Dim strcustomerGroupfilter As String = String.Empty
        Dim strLocationfilter As String = String.Empty
        Dim strAccountSetfilter As String = String.Empty
        Dim strAccountNofilter As String = String.Empty

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strLocationfilter = clsCommon.GetMulcallString(txtLocation.arrValueMember)
        End If
        If fndMultiAccSet.arrValueMember IsNot Nothing AndAlso fndMultiAccSet.arrValueMember.Count > 0 Then
            strAccountSetfilter = clsCommon.GetMulcallString(fndMultiAccSet.arrValueMember)
        End If
        If txtMultAccountNo.arrValueMember IsNot Nothing AndAlso txtMultAccountNo.arrValueMember.Count > 0 Then
            strAccountNofilter = clsCommon.GetMulcallString(txtMultAccountNo.arrValueMember)
        End If

        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            strcustomerfilter = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
        End If

        If fndMultiVendorGroup.arrValueMember IsNot Nothing AndAlso fndMultiVendorGroup.arrValueMember.Count > 0 Then
            strcustomerGroupfilter = clsCommon.GetMulcallString(fndMultiVendorGroup.arrValueMember)
        End If

        Dim BaseQry As String = clsCustomerMaster.GetCustomerBaseQryForOpeningForReco(ItemWisecheck, IsSecurity, "", False, CurrencyType, strCustomer, isOpening, strfromdate, strtodate, False, chkIncludeApplyDocument.Checked, Nothing)

        Dim strRunQueryforMainQuery = "Select  max(accountn) as accountn,InnQuery.HeaderData,InnQuery.DocNo as DocNo,max(InnQuery.DocDate) as DocDate ,max(InnQuery.DocType ) as DocType ,max(InnQuery.ACode) AS ACode, max(InnQuery.AName) AS AName,sum(convert(decimal(18,2),InnQuery.DrAmt)) as DrAmt,max(InnQuery.ConvRate) as ConvRate,sum(convert(decimal(18,2),InnQuery.CrAmt)) as CrAmt,max(InnQuery.Location) as Location, max(InnQuery.Cust_Type_Code) as Cust_Type_Code ,max(InnQuery.Cust_Type_Desc) as Cust_Type_Desc,max(InnQuery.Cust_Category_Code) as Cust_Category_Code ," & _
        " case when max(InnQuery.DocType) ='RV-TA' then (Select isnull(TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT,0) from TSPL_RECEIPT_HEADER where Receipt_No in (select Document_No from tspl_bank_reverse where Reverse_Code = InnQuery.DocNo)) else  max(EXCHANGE_GAIN_AMT) end as EXCHANGE_GAIN_AMT , case when max(InnQuery.DocType) ='RV-TA' then (Select isnull(TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT,0) from TSPL_RECEIPT_HEADER where Receipt_No in (select Document_No from tspl_bank_reverse where Reverse_Code = InnQuery.DocNo)) else  max(EXCHANGE_LOSS_AMT) end as EXCHANGE_LOSS_AMT ,max(SourceCode) as SourceCode  " & _
        " from (Select  case when Headerdata ='' and DocType ='IM' then substring(TSPL_RECEIPT_HEADER.Cr_Account,0,11)+Location  when Headerdata ='H' then substring(TSPL_RECEIPT_HEADER.Dr_Account,0,11)+Location end as accountn,  InnQuery.HeaderData,InnQuery.ACode AS ACode, InnQuery.AName AS AName,InnQuery.DocNo as DocNo,InnQuery.AgainstInvoiceNo AS AgainstInvoiceNo,InnQuery.DocDate ,InnQuery.DocType ,InnQuery.DocNarr ,InnQuery.ChequeDetails ,InnQuery.Currency_Code ,InnQuery.ConvRate,InnQuery.DrAmt ," + Environment.NewLine & _
        "case when len( (isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'')))<=0 then " + Environment.NewLine & _
        " case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=(InnQuery.Location) then " + Environment.NewLine & _
        " case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 AND (DocType)<>'EXC' and (DocType)<>'IM' AND  (InnQuery.Receipt_Type )<>'U'  then  (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") else " + Environment.NewLine & _
        " case when isnull((CrAmt),0)>0 AND (DocType)<>'IM' then (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") when isnull((CrAmt),0)>0  AND (DocType)='IM'  then (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") WHEN isnull((CrAmt),0)<0  AND (DocType)='IM'  then (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ")  else 0 end end else " + Environment.NewLine & _
        " case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ")  else (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ")  end end else " + Environment.NewLine & _
        " case when (RIGHT(TSPL_BANK_MASTER.BANKACC,3))=isnull(TSPL_RECEIPT_HEADER.Location_GL_Code,'') then " + Environment.NewLine & _
        " case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then   (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") +isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) else  (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ") -isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ),0) end else " + Environment.NewLine & _
        " case when isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0)>0 then  (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ")  else  (CrAmt* " & IIf(clsCommon.myCstr(CurrencyType) = "1", 1, " InnQuery." & CurrencyType & "") & ")  end end end as CrAmt, " + Environment.NewLine & _
        " InnQuery.SecurityDrAmt ,InnQuery.SecurityCrAmt ,InnQuery.Sales ,InnQuery.CollectionRefund,InnQuery.DrNote,InnQuery.CrNote,InnQuery.Location,InnQuery.SourceCode ,InnQuery.Item_Code,InnQuery.Item_Desc,InnQuery.Receipt_Type,InnQuery.Bank_Code,InnQuery.Cust_Type_Code ,InnQuery.Cust_Type_Desc,InnQuery.Cust_Category_Code ,InnQuery.CUST_CATEGORY_DESC,isnull(TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT,0) as EXCHANGE_LOSS_AMT,isnull(TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT ,0) as EXCHANGE_GAIN_AMT    from  (" & BaseQry & " ) InnQuery " + Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=InnQuery.Bank_Code left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =InnQuery.DocNo " & IIf(chkIncludeApplyDocument.Checked = False, " WHERE InnQuery.DocType<>'IM' " & strExcludeEXcforApplyDocumnets & " ", "") & " )InnQuery   where DocType <>'EXC'  group by DocNo,Location,Headerdata "


        strRunQuery = "select GL.Account_code,gl.Account_Desc,xx.DocNo,xx.DocDate,xx.DocType AS TransType,xx.ACode,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Cust_Account, TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc,xx.DrAmt,xx.CrAmt,xx.DrAmt-xx.CrAmt as NetAmount,GL.Voucher_No,GL.Voucher_Date,isnull( GL.DRAmount,0) as GLDrAmount,isnull(GL.CRAmount,0) as GLCrAmount,isnull(GL.DRAmount-GL.CRAmount,0) as GLNetAmount,isnull((xx.DrAmt-xx.CrAmt)-(GL.DRAmount-GL.CRAmount),0) as DiffAmount,xx.SourceCode  from " & Environment.NewLine & _
      " ( " & Environment.NewLine & _
      " Select finalCustomerBaseQuery.accountn,finalCustomerBaseQuery.Headerdata ,finalCustomerBaseQuery.DocNo,finalCustomerBaseQuery.DocDate as DocDate,finalCustomerBaseQuery.DocType as DocType,finalCustomerBaseQuery.ACode as ACode, finalCustomerBaseQuery.AName  as AName ,case when finalCustomerBaseQuery.DocType ='RV-TA' then (finalCustomerBaseQuery.DrAmt* finalCustomerBaseQuery.ConvRate)-finalCustomerBaseQuery.EXCHANGE_GAIN_AMT+finalCustomerBaseQuery.EXCHANGE_LOSS_AMT else finalCustomerBaseQuery.DrAmt* finalCustomerBaseQuery.ConvRate end  as DrAmt, " & Environment.NewLine & _
      " case when finalCustomerBaseQuery.DocType <>'RV-TA' then finalCustomerBaseQuery.CrAmt-finalCustomerBaseQuery.EXCHANGE_GAIN_AMT+finalCustomerBaseQuery.EXCHANGE_LOSS_AMT else finalCustomerBaseQuery.CrAmt end  as CrAmt,finalCustomerBaseQuery.Location as Location,finalCustomerBaseQuery.Cust_Type_Code as Cust_Type_Code ,finalCustomerBaseQuery.Cust_Type_Desc as Cust_Type_Desc,finalCustomerBaseQuery.Cust_Category_Code as  Cust_Category_Code, finalCustomerBaseQuery.EXCHANGE_GAIN_AMT ,finalCustomerBaseQuery.EXCHANGE_LOSS_AMT,finalCustomerBaseQuery.SourceCode from " & Environment.NewLine & _
      " ( " & Environment.NewLine & _
      " " & strRunQueryforMainQuery & " " & Environment.NewLine & _
      " )finalCustomerBaseQuery " & Environment.NewLine

        If clsCommon.myLen(strLocationfilter) > 0 Then
            strRunQuery += " where  finalCustomerBaseQuery.Location in (" & strLocationfilter & ") "
        End If


        strRunQuery += " )xx " & Environment.NewLine & _
       " left outer join ( " & Environment.NewLine & _
       " select TSPL_JOURNAL_MASTER.Voucher_No,max(TSPL_JOURNAL_MASTER.Voucher_Date) as Voucher_Date,TSPL_JOURNAL_MASTER.Source_Doc_No,max(TSPL_JOURNAL_MASTER.Segment_code) as Segment_code, TSPL_JOURNAL_DETAILS.Account_code,max(TSPL_GL_ACCOUNTS.Description) as Account_Desc,sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount>0  then 1 else 0 end   ) as DRAmount ,sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 else 0 end  ) as CRAmount,TSPL_JOURNAL_DETAILS.Account_Seg_Code7 AS JE_LOC_CODE " & Environment.NewLine & _
       " from TSPL_JOURNAL_DETAILS  " & Environment.NewLine & _
       " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_JOURNAL_DETAILS.Account_code " & Environment.NewLine & _
       " inner join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_DETAILS.Voucher_No=TSPL_JOURNAL_MASTER.Voucher_No  " & Environment.NewLine & _
       " where 2=2 and TSPL_JOURNAL_MASTER.Authorized='A' and TSPL_JOURNAL_DETAILS.Reco_Control_Account='C' and " & Environment.NewLine
        If isOpening = True Then
            strRunQuery += " CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) < '" & strfromdate & "' " & Environment.NewLine
        Else
            strRunQuery += " CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) BETWEEN '" & strfromdate & "' and '" & strtodate & "'  " & Environment.NewLine
        End If
        strRunQuery += " group by TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_MASTER.Source_Doc_No,TSPL_JOURNAL_DETAILS.Account_code,TSPL_JOURNAL_DETAILS.Account_Seg_Code7" & Environment.NewLine & _
        " )GL on GL.Source_Doc_No= xx.DocNo AND GL.JE_LOC_CODE=xx.Location and 2=(case when xx.DocType= 'IM' then (case when GL.Account_code=xx.accountn then 2 else 3 end) else 2 end ) " & Environment.NewLine & _
        " left outer join  TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code= xx.ACode " & Environment.NewLine & _
        " left outer join  TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code= TSPL_CUSTOMER_MASTER.Cust_Group_Code " & Environment.NewLine & _
        " where (xx.DrAmt>0 or xx.CrAmt>0 or GL.DRAmount>0 or GL.CRAmount>0) " & Environment.NewLine & _
        " and DocNo not in ( Select TSPL_BANK_REVERSE.Reverse_Code  from TSPL_BANK_REVERSE left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =TSPL_BANK_REVERSE.Document_No " & Environment.NewLine & _
        " where  TSPL_RECEIPT_HEADER.Receipt_Type ='M' and TSPL_BANK_REVERSE.Reverse_Code =xx.DocNo ) " & Environment.NewLine

            If clsCommon.myLen(strcustomerGroupfilter) > 0 Then
                strRunQuery += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (" & strcustomerGroupfilter & ") "
            End If

            If clsCommon.myLen(strAccountNofilter) > 0 Then
                strRunQuery += " and  GL.Account_code in (" & strAccountNofilter & ") "
            End If

            If clsCommon.myLen(strAccountSetfilter) > 0 Then
                strRunQuery += " and TSPL_CUSTOMER_MASTER.Cust_Account in (" & strAccountSetfilter & ") "
            End If

            Return strRunQuery
    End Function
    Sub OtherDataPrint(ByVal IsPrint As Exporter, Optional ByVal BulkExport As Integer = 0)
        Try
           
            Dim strAccountNofilter As String = String.Empty
            Gv2.DataSource = Nothing
            Gv2.Rows.Clear()

            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = ""

            If txtMultAccountNo.arrValueMember IsNot Nothing AndAlso txtMultAccountNo.arrValueMember.Count > 0 Then
                strAccountNofilter = clsCommon.GetMulcallString(txtMultAccountNo.arrValueMember)
            End If

            strRunQuery = " select TSPL_JOURNAL_MASTER.Voucher_No ,TSPL_JOURNAL_MASTER.Voucher_Date  ,TSPL_JOURNAL_MASTER.Source_Doc_No ,TSPL_JOURNAL_MASTER.Source_Doc_Date ,TSPL_JOURNAL_MASTER.CustVend_Code as [Customer/Vendor Code],TSPL_JOURNAL_MASTER.CustVend_Name as [Customer/Vendor Name]  ," + Environment.NewLine + _
            " TSPL_JOURNAL_DETAILS.Account_code,case when TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_JOURNAL_MASTER .CustVend_Code then 'Customer'  when TSPL_VENDOR_MASTER.Vendor_Code  =TSPL_JOURNAL_MASTER .CustVend_Code then 'Vendor'" + Environment.NewLine + _
            " when isnull(TSPL_JOURNAL_MASTER .CustVend_Code,'')='' then 'JE Other' end as [Journal Entry Type]," + Environment.NewLine + _
            "  (TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount>0  then 1 else 0 end   ) as DRAmount ," + Environment.NewLine + _
            " (TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 else 0 end  ) as CRAmount " + Environment.NewLine + _
            " from TSPL_JOURNAL_DETAILS " + Environment.NewLine + _
            " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " + Environment.NewLine + _
            "left outer join tspl_customer_master on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_JOURNAL_MASTER .CustVend_Code " + Environment.NewLine + _
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code  =TSPL_JOURNAL_MASTER .CustVend_Code " + Environment.NewLine + _
            " where isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'')<>'C' and TSPL_JOURNAL_MASTER.Authorized='A' "
                If Not chkOPAndClosing.Checked Then
                    strRunQuery += " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + " ' "
                End If
            strRunQuery += " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + " '  and TSPL_JOURNAL_DETAILS.Account_code in (" & strAccountNofilter & ") "

            dt = clsDBFuncationality.GetDataTable(strRunQuery)
            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
            Gv2.DataSource = Nothing
            Gv2.Columns.Clear()
            Gv2.Rows.Clear()
            Gv2.GroupDescriptors.Clear()
            Gv2.MasterTemplate.SummaryRowsBottom.Clear()
            Gv2.EnableFiltering = True
            Gv2.Tag = cboType.SelectedValue
            Gv2.BestFitColumns()
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                'common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                'Exit Sub
            Else
                Gv2.DataSource = dt
            End If
            Gv2.MasterTemplate.AllowAddNewRow = False

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item As New GridViewSummaryItem("DRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("CRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            Gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            RadPageView1.SelectedPage = RadPageViewPage3
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub Print(ByVal IsPrint As Exporter, Optional ByVal BulkExport As Integer = 0)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strcustomerfilter As String = String.Empty
            Dim strcustomerGroupfilter As String = String.Empty
            Dim strLocationfilter As String = String.Empty
            Dim strAccountSetfilter As String = String.Empty
            Dim strAccountNofilter As String = String.Empty
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If clsCommon.myLen(cboType.Text) > 0 Then
                arrHeader.Add("Report Type : " + cboType.Text)
            End If

            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("Customer Reco:" + cboType.SelectedValue, Gv1, arrHeader, Me.Text)
                Exit Sub
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Customer Reco" + cboType.SelectedValue, Gv1, arrHeader, "Purchase Register", True)
                Exit Sub
            End If
            clsCommon.ProgressBarShow()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = ""
            Dim strMain As String = Nothing
            Dim obj As New clsPurchaseReco

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                strLocationfilter = clsCommon.GetMulcallString(txtLocation.arrValueMember)
            End If
            If fndMultiAccSet.arrValueMember IsNot Nothing AndAlso fndMultiAccSet.arrValueMember.Count > 0 Then
                strAccountSetfilter = clsCommon.GetMulcallString(fndMultiAccSet.arrValueMember)
            End If
            If txtMultAccountNo.arrValueMember IsNot Nothing AndAlso txtMultAccountNo.arrValueMember.Count > 0 Then
                strAccountNofilter = clsCommon.GetMulcallString(txtMultAccountNo.arrValueMember)
            End If

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                strcustomerfilter = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
            End If

            If fndMultiVendorGroup.arrValueMember IsNot Nothing AndAlso fndMultiVendorGroup.arrValueMember.Count > 0 Then
                strcustomerGroupfilter = clsCommon.GetMulcallString(fndMultiVendorGroup.arrValueMember)
            End If

            Dim strOPTransQry As String = ""
            Dim strOPSystemQry As String = ""
            Dim ConvRate As String = "ConvRate"

            Dim strFromDate As String = clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")

            strRunQuery = "Select *,2 as RI from (" + CustomerBaseQry(False, False, "", False, ConvRate, strcustomerfilter, False, strFromDate, strToDate, False, False, chkIncludeApplyDocument.Checked) + ")aaa "
            If chkOPAndClosing.Checked Then
                If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Customer And Account Wise") = CompairStringResult.Equal Then
                    strFromDate = ERPStartDate
                    'strToDate = fromDate.Value.AddDays(-1)
                    strToDate = clsCommon.GetPrintDate(fromDate.Value.AddDays(-1), "dd/MMM/yyyy")
                    strOPTransQry = "Select *,1 as RI from (" + CustomerBaseQry(False, False, "", False, ConvRate, strcustomerfilter, False, strFromDate, strToDate, False, False, chkIncludeApplyDocument.Checked) + ")bbb "

                    ' obj.To_Date = ERPStartDate
                    strOPSystemQry = "Select *,1 as RI from (" + CustomerBaseQry(False, False, "", False, ConvRate, strcustomerfilter, True, strFromDate, strToDate, False, False, chkIncludeApplyDocument.Checked) + ")ccc "
                    strRunQuery += Environment.NewLine + Environment.NewLine +
                        "Union All" + Environment.NewLine + Environment.NewLine + strOPTransQry + Environment.NewLine + Environment.NewLine +
                        "Union All" + Environment.NewLine + Environment.NewLine + strOPSystemQry + Environment.NewLine + Environment.NewLine
                End If
            End If


            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
                strRunQuery = "select xxxxx.*,isnull( JEOther.DRAmount,0)  as OtherDRAmount, isnull( JEOther.CRAmount,0)  as OtherCRAmount,(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0)) as OtherNetAmount, (isnull(xxxxx.GLNetAmount ,0)+(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0))) as TotalAmount, (isnull( xxxxx.NetAmount,0)-(isnull(xxxxx.GLNetAmount ,0)+(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0)))) as CustomerNetTotalDiff from (" + Environment.NewLine +
                "select Account_code,max(Account_Desc) as Account_Desc," &
                 "sum(case when RI=1 then  DrAmt-CrAmt else 0 end) as OPBal" + Environment.NewLine +
                ",sum(case when RI=2 then  DrAmt else 0 end) as DRAmount" + Environment.NewLine +
                ",sum(case when RI=2  then CrAmt else 0 end ) as CRAmount" + Environment.NewLine +
                ",sum(case when RI=2 then NetAmount else 0 end ) as NetAmount" + Environment.NewLine +
                ",sum(case when RI=2 then NetAmount when RI=1 then  DrAmt-CrAmt else 0 end) as CLBal" + Environment.NewLine +
                ",sum(case when RI=1 then GLDRAmount-GLCRAmount else 0 end ) as GLOPBal" + Environment.NewLine +
                ",sum(case when RI=2 then GLDRAmount else 0 end ) as GLDRAmount" + Environment.NewLine +
                ",sum(case when RI=2 then GLCRAmount else 0 end ) as GLCRAmount" + Environment.NewLine +
                ",sum(case when RI=2 then GLNetAmount else 0 end ) as GLNetAmount" + Environment.NewLine +
                ",sum(case when RI=2 then GLNetAmount  when RI=1 then GLDRAmount-GLCRAmount else 0 end ) as GLCLBal" + Environment.NewLine +
                ",sum(case when RI=2 then DiffAmount else 0 end ) as DiffAmount " + Environment.NewLine +
                " from (" + strRunQuery + ")Final group by Account_code" + Environment.NewLine +
                 ")xxxxx  " + Environment.NewLine +
                 "left outer join (select TSPL_JOURNAL_DETAILS.Account_code,sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount>0  then 1 else 0 end   ) as DRAmount ," + Environment.NewLine +
                "sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 else 0 end  ) as CRAmount " + Environment.NewLine +
                " from TSPL_JOURNAL_DETAILS " + Environment.NewLine +
                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " + Environment.NewLine +
                " where isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'')<>'C' and TSPL_JOURNAL_MASTER.Authorized='A' "
                If Not chkOPAndClosing.Checked Then
                    strRunQuery += " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + " ' "
                End If
                strRunQuery += " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + " ' group by TSPL_JOURNAL_DETAILS.Account_code ) JEOther on  JEOther.Account_code=xxxxx.Account_code"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Customer And Account Wise") = CompairStringResult.Equal Then
                strRunQuery = "select  max(Cust_Group_Code) as Cust_Group_Code,max(Cust_Group_Desc) as Cust_Group_Desc,max(Cust_Account) as Cust_Account,ACode,max(Customer_Name) as Customer_Name, Account_code,max(Account_Desc) as Account_Desc " + Environment.NewLine +
                     ",sum(case when RI=1 then  DrAmt-CrAmt else 0 end) as OPBal" + Environment.NewLine +
                    ",sum(case when RI=2 then  DrAmt else 0 end) as DRAmount" + Environment.NewLine +
                    ",sum(case when RI=2  then CrAmt else 0 end ) as CRAmount" + Environment.NewLine +
                    ",sum(case when RI=2 then NetAmount else 0 end ) as NetAmount" + Environment.NewLine +
                    ",sum(case when RI=2 then NetAmount when RI=1 then  DrAmt-CrAmt else 0 end) as CLBal" + Environment.NewLine +
                    ",sum(case when RI=1 then GLDRAmount-GLCRAmount else 0 end ) as GLOPBal" + Environment.NewLine +
                    ",sum(case when RI=2 then GLDRAmount else 0 end ) as GLDRAmount" + Environment.NewLine +
                    ",sum(case when RI=2 then GLCRAmount else 0 end ) as GLCRAmount" + Environment.NewLine +
                    ",sum(case when RI=2 then GLNetAmount else 0 end ) as GLNetAmount" + Environment.NewLine +
                    ",sum(case when RI=2 then GLNetAmount  when RI=1 then GLDRAmount-GLCRAmount else 0 end ) as GLCLBal" + Environment.NewLine +
                    ",sum(case when RI=2 then DiffAmount else 0 end ) as DiffAmount " + Environment.NewLine +
                "from (" + strRunQuery + ")Final group by ACode,Account_code"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            Else
                Throw New Exception("Wrong Report type")
            End If

            dt = clsDBFuncationality.GetDataTable(strRunQuery)
            RadPageViewPage2.Text = cboType.Text
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True
            Gv1.Tag = cboType.SelectedValue
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                EnableDisableAllControl(False)
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If
            'FindAndRestoreGridLayout(Me)
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
            ReStoreGridLayout()
            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Sub EnableDisableAllControl(ByVal val As Boolean)
        txtLocation.Enabled = val
        txtCustomer.Enabled = val
        txtMultAccountNo.Enabled = val
        fndMultiAccSet.Enabled = val
        fndMultiVendorGroup.Enabled = val
        cboType.Enabled = val
        fromDate.Enabled = val
        ToDate.Enabled = val
        chkMismatchDoc.Enabled = val
        Gv2.DataSource = Nothing
        RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
    End Sub

    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
            Gv1.Columns(ii).BestFit()
        Next
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
            Gv1.Columns("Account_code").IsVisible = True
            Gv1.Columns("Account_code").Width = 120
            Gv1.Columns("Account_code").HeaderText = "Account code"

            Gv1.Columns("Account_Desc").IsVisible = True
            Gv1.Columns("Account_Desc").Width = 120
            Gv1.Columns("Account_Desc").HeaderText = "Account"

            Gv1.Columns("DRAmount").IsVisible = True
            Gv1.Columns("DRAmount").Width = 120
            Gv1.Columns("DRAmount").HeaderText = "Debit Amt"
            Gv1.Columns("DRAmount").FormatString = "{0:n2}"

            Gv1.Columns("CRAmount").IsVisible = True
            Gv1.Columns("CRAmount").Width = 120
            Gv1.Columns("CRAmount").HeaderText = "Credit Amt"

            Gv1.Columns("NetAmount").IsVisible = True
            Gv1.Columns("NetAmount").Width = 120
            Gv1.Columns("NetAmount").HeaderText = "Net Amt"

            Gv1.Columns("GLDRAmount").IsVisible = True
            Gv1.Columns("GLDRAmount").Width = 120
            Gv1.Columns("GLDRAmount").HeaderText = "Trial Debit Amt"

            Gv1.Columns("GLCRAmount").IsVisible = True
            Gv1.Columns("GLCRAmount").Width = 120
            Gv1.Columns("GLCRAmount").HeaderText = "Trial Credit Amt"

            Gv1.Columns("GLNetAmount").IsVisible = True
            Gv1.Columns("GLNetAmount").Width = 120
            Gv1.Columns("GLNetAmount").HeaderText = "Trial Net Amt"

            Gv1.Columns("OtherDRAmount").IsVisible = True
            Gv1.Columns("OtherDRAmount").Width = 120
            Gv1.Columns("OtherDRAmount").HeaderText = "Other Debit Amt"

            Gv1.Columns("OtherCRAmount").IsVisible = True
            Gv1.Columns("OtherCRAmount").Width = 120
            Gv1.Columns("OtherCRAmount").HeaderText = "Other Credit Amt"

            Gv1.Columns("OtherNetAmount").IsVisible = True
            Gv1.Columns("OtherNetAmount").Width = 120
            Gv1.Columns("OtherNetAmount").HeaderText = "Other Net Amt"

            Gv1.Columns("DiffAmount").IsVisible = True
            Gv1.Columns("DiffAmount").Width = 120
            Gv1.Columns("DiffAmount").HeaderText = "Diff Amount"

            Gv1.Columns("TotalAmount").IsVisible = True
            Gv1.Columns("TotalAmount").Width = 120
            Gv1.Columns("TotalAmount").HeaderText = "Total Amount"

            Gv1.Columns("CustomerNetTotalDiff").IsVisible = True
            Gv1.Columns("CustomerNetTotalDiff").Width = 120
            Gv1.Columns("CustomerNetTotalDiff").HeaderText = "Customer Net Total Diff"

            Gv1.Columns("OPBal").IsVisible = chkOPAndClosing.Checked
            Gv1.Columns("OPBal").Width = 120
            Gv1.Columns("OPBal").HeaderText = "Opening"

            Gv1.Columns("CLBal").IsVisible = chkOPAndClosing.Checked
            Gv1.Columns("CLBal").Width = 120
            Gv1.Columns("CLBal").HeaderText = "Closing"

            Gv1.Columns("GLOPBal").IsVisible = chkOPAndClosing.Checked
            Gv1.Columns("GLOPBal").Width = 120
            Gv1.Columns("GLOPBal").HeaderText = "GL Opening"

            Gv1.Columns("GLCLBal").IsVisible = chkOPAndClosing.Checked
            Gv1.Columns("GLCLBal").Width = 120
            Gv1.Columns("GLCLBal").HeaderText = "GL Closing"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item As New GridViewSummaryItem("DRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("CRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("NetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLDRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLCRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLNetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("OtherDRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("OtherCRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("OtherNetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("DiffAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("TotalAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("CustomerNetTotalDiff", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("OPBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("CLBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLOPBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLCLBal", "{0:F2}", GridAggregateFunction.Sum)


            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Customer And Account Wise") = CompairStringResult.Equal Then
            Gv1.Columns("Cust_Group_Code").IsVisible = True
            Gv1.Columns("Cust_Group_Code").Width = 120
            Gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"

            Gv1.Columns("Cust_Group_Desc").IsVisible = True
            Gv1.Columns("Cust_Group_Desc").Width = 120
            Gv1.Columns("Cust_Group_Desc").HeaderText = "Customer Group"

            Gv1.Columns("Cust_Account").IsVisible = True
            Gv1.Columns("Cust_Account").Width = 120
            Gv1.Columns("Cust_Account").HeaderText = "Customer Account"

            Gv1.Columns("ACode").IsVisible = True
            Gv1.Columns("ACode").Width = 120
            Gv1.Columns("ACode").HeaderText = "Customer Code"

            Gv1.Columns("Customer_Name").IsVisible = True
            Gv1.Columns("Customer_Name").Width = 120
            Gv1.Columns("Customer_Name").HeaderText = "Customer"

            Gv1.Columns("Account_code").IsVisible = True
            Gv1.Columns("Account_code").Width = 120
            Gv1.Columns("Account_code").HeaderText = "Account code"

            Gv1.Columns("Account_Desc").IsVisible = True
            Gv1.Columns("Account_Desc").Width = 120
            Gv1.Columns("Account_Desc").HeaderText = "Account"

            Gv1.Columns("DRAmount").IsVisible = True
            Gv1.Columns("DRAmount").Width = 120
            Gv1.Columns("DRAmount").HeaderText = "Debit Amt"
            Gv1.Columns("DRAmount").FormatString = "{0:n2}"

            Gv1.Columns("CRAmount").IsVisible = True
            Gv1.Columns("CRAmount").Width = 120
            Gv1.Columns("CRAmount").HeaderText = "Credit Amt"

            Gv1.Columns("NetAmount").IsVisible = True
            Gv1.Columns("NetAmount").Width = 120
            Gv1.Columns("NetAmount").HeaderText = "Net Amt"

            Gv1.Columns("GLDRAmount").IsVisible = True
            Gv1.Columns("GLDRAmount").Width = 120
            Gv1.Columns("GLDRAmount").HeaderText = "Trial Debit Amt"

            Gv1.Columns("GLCRAmount").IsVisible = True
            Gv1.Columns("GLCRAmount").Width = 120
            Gv1.Columns("GLCRAmount").HeaderText = "Trial Credit Amt"

            Gv1.Columns("GLNetAmount").IsVisible = True
            Gv1.Columns("GLNetAmount").Width = 120
            Gv1.Columns("GLNetAmount").HeaderText = "Trial Net Amt"

            Gv1.Columns("DiffAmount").IsVisible = True
            Gv1.Columns("DiffAmount").Width = 120
            Gv1.Columns("DiffAmount").HeaderText = "Diff Amount"

            Gv1.Columns("OPBal").IsVisible = chkOPAndClosing.Checked
            Gv1.Columns("OPBal").Width = 120
            Gv1.Columns("OPBal").HeaderText = "Opening"

            Gv1.Columns("CLBal").IsVisible = chkOPAndClosing.Checked
            Gv1.Columns("CLBal").Width = 120
            Gv1.Columns("CLBal").HeaderText = "Closing"

            Gv1.Columns("GLOPBal").IsVisible = chkOPAndClosing.Checked
            Gv1.Columns("GLOPBal").Width = 120
            Gv1.Columns("GLOPBal").HeaderText = "GL Opening"

            Gv1.Columns("GLCLBal").IsVisible = chkOPAndClosing.Checked
            Gv1.Columns("GLCLBal").Width = 120
            Gv1.Columns("GLCLBal").HeaderText = "GL Closing"
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item As New GridViewSummaryItem("DRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("CRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("NetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLDRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLCRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLNetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("DiffAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)

            item = New GridViewSummaryItem("OPBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("CLBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLOPBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLCLBal", "{0:F2}", GridAggregateFunction.Sum)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            Gv1.Columns("Cust_Group_Code").IsVisible = True
            Gv1.Columns("Cust_Group_Code").Width = 120
            Gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"

            Gv1.Columns("Cust_Group_Desc").IsVisible = True
            Gv1.Columns("Cust_Group_Desc").Width = 120
            Gv1.Columns("Cust_Group_Desc").HeaderText = "Customer Group"

            Gv1.Columns("Cust_Account").IsVisible = True
            Gv1.Columns("Cust_Account").Width = 120
            Gv1.Columns("Cust_Account").HeaderText = "Customer Account"

            Gv1.Columns("ACode").IsVisible = True
            Gv1.Columns("ACode").Width = 120
            Gv1.Columns("ACode").HeaderText = "Customer Code"

            Gv1.Columns("Customer_Name").IsVisible = True
            Gv1.Columns("Customer_Name").Width = 120
            Gv1.Columns("Customer_Name").HeaderText = "Customer"

            Gv1.Columns("Account_code").IsVisible = True
            Gv1.Columns("Account_code").Width = 120
            Gv1.Columns("Account_code").HeaderText = "Account code"

            Gv1.Columns("Account_Desc").IsVisible = True
            Gv1.Columns("Account_Desc").Width = 120
            Gv1.Columns("Account_Desc").HeaderText = "Account"

            Gv1.Columns("DrAmt").IsVisible = True
            Gv1.Columns("DrAmt").Width = 120
            Gv1.Columns("DrAmt").HeaderText = "Debit Amt"
            Gv1.Columns("DrAmt").FormatString = "{0:n2}"

            Gv1.Columns("CrAmt").IsVisible = True
            Gv1.Columns("CrAmt").Width = 120
            Gv1.Columns("CrAmt").HeaderText = "Credit Amt"

            Gv1.Columns("NetAmount").IsVisible = True
            Gv1.Columns("NetAmount").Width = 120
            Gv1.Columns("NetAmount").HeaderText = "Net Amt"

            Gv1.Columns("GLDRAmount").IsVisible = True
            Gv1.Columns("GLDRAmount").Width = 120
            Gv1.Columns("GLDRAmount").HeaderText = "Trial Debit Amt"

            Gv1.Columns("GLCRAmount").IsVisible = True
            Gv1.Columns("GLCRAmount").Width = 120
            Gv1.Columns("GLCRAmount").HeaderText = "Trial Credit Amt"

            Gv1.Columns("GLNetAmount").IsVisible = True
            Gv1.Columns("GLNetAmount").Width = 120
            Gv1.Columns("GLNetAmount").HeaderText = "Trial Net Amt"

            Gv1.Columns("DiffAmount").IsVisible = True
            Gv1.Columns("DiffAmount").Width = 120
            Gv1.Columns("DiffAmount").HeaderText = "Diff Amount"

            Gv1.Columns("DocNo").IsVisible = True
            Gv1.Columns("DocNo").Width = 120
            Gv1.Columns("DocNo").HeaderText = "Document No"

            Gv1.Columns("DocDate").IsVisible = True
            Gv1.Columns("DocDate").Width = 120
            Gv1.Columns("DocDate").HeaderText = "Document Date"

            Gv1.Columns("TransType").IsVisible = True
            Gv1.Columns("TransType").Width = 120
            Gv1.Columns("TransType").HeaderText = "Document Type"

            Gv1.Columns("Voucher_No").IsVisible = True
            Gv1.Columns("Voucher_No").Width = 120
            Gv1.Columns("Voucher_No").HeaderText = "Voucher No"

            Gv1.Columns("Voucher_Date").IsVisible = True
            Gv1.Columns("Voucher_Date").Width = 120
            Gv1.Columns("Voucher_Date").HeaderText = "Voucher Date"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("NetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLDRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLCRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLNetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("DiffAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub

    Sub Reset()
        EnableDisableAllControl(True)
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)

                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Customer Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If clsCommon.myLen(cboType.Text) > 0 Then
                arrHeader.Add("Report Type : " + cboType.Text)
            End If

            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmSetting_Click(sender As Object, e As EventArgs) Handles rmSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptFreshSaleRegister1
        frm.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rptVendReco_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        DrillDown()
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            If clsCommon.myLen(e.Row.Cells.Item("DocNo").Value) > 0 Then
                Dim SoucrCode As String = clsCommon.myCstr(Gv1.Rows(e.RowIndex).Cells.Item("SourceCode").Value)
                Dim DocNo As String = clsCommon.myCstr(e.Row.Cells.Item("DocNo").Value)


                If SoucrCode = "AR-PY" Or SoucrCode = "AR-RC" Or SoucrCode = "AR-UN" Or SoucrCode = "AR-OA" Or SoucrCode = "AR-RF" Or clsCommon.CompairString(SoucrCode, "AR-IM") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "AP-PY") = CompairStringResult.Equal Or clsCommon.CompairString(SoucrCode, "AP-MI") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "AP-IN") = CompairStringResult.Equal OrElse clsCommon.CompairString(SoucrCode, "AP-DN") = CompairStringResult.Equal OrElse clsCommon.CompairString(SoucrCode, "AP-CN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "SD-IN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "BK-TF") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.bankTransfer, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "IC-AD") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "PO-RC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "SD-LO") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.LoadOut, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "MM-TF") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "RV-TA") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.reverseTransaction, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "AR-AD") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptAdjustmentEntry, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "SD-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "AR-IN") = CompairStringResult.Equal Or clsCommon.CompairString(SoucrCode, "AR-CR") = CompairStringResult.Equal Or clsCommon.CompairString(SoucrCode, "AR-DN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, DocNo)
                ElseIf clsCommon.CompairString(SoucrCode, "VC-GL") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnVCGLEntry, DocNo)
                Else
                    Return
                End If

            End If
        End If
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = "select CM1.cust_code as Code, CM1.Customer_Name as Name, Case When ISNULL(CM2.Cust_Code,'')<>'' Then ISNULL(CM2.Cust_Code,'')+' - '+ISNULL(CM2.Customer_Name,'') Else '' End as [ParentCustomer]  from tspl_customer_master CM1 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM2 ON CM2.Cust_Code=CM1.Parent_Customer_No  where 1=1 and CM1.Status='N'"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustTypeMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtCustomer, "Customer_Name", "TSPL_CUSTOMER_MASTER", "cust_code")
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where location_type IN  ('Physical','Virtual')   "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    Private Sub Gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles Gv1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            DrillDown()
        End If
    End Sub


    Public Shared Function GetTaxQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select TAX" & intloop & " from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select TAX" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function

    Public Shared Function GetAddChargeQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select Add_Charge_Code" & intloop & "  from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select Add_Charge_Code" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function

    Public Shared Function GetAddChargeZeroQuery(ByVal lstTables As List(Of String)) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select 'AC_'+Add_Charge_Code" & intloop & " as Add_Charge_Code" & intloop & "  from " & TableName & ""
                    Else
                        qry = qry & " Union  " & "select 'AC_'+Add_Charge_Code" & intloop & " as Add_Charge_Code" & intloop & " from " & TableName & ""
                    End If
                Next
            Next
        Else
            Return qry
        End If
        If clsCommon.myLen(qry) > 0 Then
            qry = "select * from( " & qry & ") as t1 where Add_Charge_Code1 not in ('AC_')"
        End If
        Return qry
    End Function

    Private Sub txtMultAccountNo__My_Click(sender As Object, e As EventArgs) Handles txtMultAccountNo._My_Click
        Dim qry As String = " select  Account_Code AS Code,Description as [Name] from TSPL_GL_ACCOUNTS "
        txtMultAccountNo.arrValueMember = clsCommon.ShowMultipleSelectForm("GLMulSel", qry, "Code", "Name", txtMultAccountNo.arrValueMember, txtMultAccountNo.arrDispalyMember)
    End Sub

    Private Sub fndMultiAccSet__My_Click(sender As Object, e As EventArgs) Handles fndMultiAccSet._My_Click
        Dim qry As String = " select Cust_Account as [Code],Cust_Acct_Desc as Name,Receivable_Control_acct as [Receivable Account],Receipts_Discount_acct as [Discount Account],Advance_acct as [Advance Account],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],CURRENCY_CODE as [Currency Code],EXCHANGE_LOSS_ACCOUNT as [Exchange Loss Account],EXCHANGE_GAIN_ACCOUNT as [Exchange Gain Account] from TSPL_CUSTOMER_ACCOUNT_SET "
        fndMultiAccSet.arrValueMember = clsCommon.ShowMultipleSelectForm("CustAccMulSel", qry, "Code", "Name", fndMultiAccSet.arrValueMember, fndMultiAccSet.arrDispalyMember)
    End Sub

    Private Sub fndMultiVendorGroup__My_Click(sender As Object, e As EventArgs) Handles fndMultiVendorGroup._My_Click
        Dim qry As String = " select Cust_Group_Code as Code,Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER  "
        fndMultiVendorGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGrpMulSel", qry, "Code", "Name", fndMultiVendorGroup.arrValueMember, fndMultiVendorGroup.arrDispalyMember)
    End Sub

    Sub DrillDown()
        Try
            Gv2.DataSource = Nothing
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Account Wise") Then
                    arrBack.Add("Account Wise")
                End If
                cboType.SelectedValue = "Customer And Account Wise"
                arrGLAccount = New ArrayList()
                arrGLAccount = txtMultAccountNo.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Account_code").Value))
                txtMultAccountNo.arrValueMember = tmp
                If Gv1.CurrentColumn Is Gv1.Columns("DiffAmount") AndAlso clsCommon.myCdbl(Gv1.CurrentRow.Cells("DiffAmount").Value) <> 0 Then
                    boolChecked = chkMismatchDoc.Checked
                    chkMismatchDoc.Checked = True
                    chkMismatchDoc.Tag = "D"
                Else
                    chkMismatchDoc.Tag = Nothing
                End If

                Print(Exporter.Refresh)
                OtherDataPrint(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Customer And Account Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Customer And Account Wise") Then
                    arrBack.Add("Customer And Account Wise")
                End If
                cboType.SelectedValue = "Detail"
                arrCustomer = New ArrayList()
                arrCustomer = txtCustomer.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("ACode").Value))
                txtCustomer.arrValueMember = tmp
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
                ''Reached at last Node
            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
                ''Reached at First Node 
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Customer And Account Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Account Wise") Then
                arrBack.Remove("Account Wise")
                cboType.SelectedValue = "Account Wise"
                txtMultAccountNo.arrValueMember = arrGLAccount
                If clsCommon.CompairString(clsCommon.myCstr(chkMismatchDoc.Tag), "D") = CompairStringResult.Equal Then
                    chkMismatchDoc.Checked = boolChecked
                    chkMismatchDoc.Tag = Nothing
                End If
                Print(Exporter.Refresh)

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Customer And Account Wise") Then
                arrBack.Remove("Customer And Account Wise")
                cboType.SelectedValue = "Customer And Account Wise"
                txtCustomer.arrValueMember = arrCustomer
                Print(Exporter.Refresh)
            End If
            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Customer Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If clsCommon.myLen(cboType.Text) > 0 Then
                arrHeader.Add("Report Type : " + cboType.Text)
            End If

            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

       
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Customer Reco", Gv1, arrHeader, "Customer Reco", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class


