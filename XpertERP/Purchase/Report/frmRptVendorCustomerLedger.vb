''created by richa agarwal 24/07/2015 AGAINST TICKET NO BM00000005918,BM00000007774
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class frmRptVendorCustomerLedger
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim blnRefresh As Boolean = False
    Dim dtCustGrp As DataTable = Nothing
    Dim dtCustomer As DataTable = Nothing
    Dim dtMain As DataTable = Nothing
    Dim dtOpening As DataTable = Nothing
    Dim dvTemp As DataView
    Dim FormType As String = Nothing
    Dim strQry As String = Nothing
    Dim StrPermission As String

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FormType = formid
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnexport.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub frmRptVendorLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadCurrencyType()
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationSegment()
        rbPortrait.IsChecked = True

        dtptodate.Value = clsCommon.GETSERVERDATE()
        dtpFromdate.Value = dtptodate.Value.AddMonths(-1)
        gvVendorGroup.Dock = DockStyle.Fill
        gvVendor.Dock = DockStyle.Fill
        gv.Dock = DockStyle.Fill
        gvVendorGroup.Visible = False
        gvVendor.Visible = False
        ChkPDC.Visible = False
        chkNone.IsChecked = True
        rdbDetail.IsChecked = True
        chkIncludeApplyDocument.Checked = False
        SetMultiCurrencyVisibility()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset all Parameters")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
    End Sub


    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        RadGroupBox1.Enabled = True

      
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub Print()
        Dim fromdate As Date
        Dim todate As Date
        Dim IsPDCCheque As String = ""
        Dim CompanyAdd As String = ""
        Dim Comp_Name As String = ""
        Dim childvendrcode As String = ""

        fromdate = dtpFromdate.Text
        todate = dtptodate.Text
        Try
            Dim arrVendorType As New ArrayList
            If chkVendorTypeVSP.IsChecked OrElse chkVendorTypeAll.IsChecked Then
                arrVendorType.Add("VSP")
            End If
            If chkVendorTypePTM.IsChecked OrElse chkVendorTypeAll.IsChecked Then
                arrVendorType.Add("PTM")
            End If
            If chkVendorTypeTTM.IsChecked OrElse chkVendorTypeAll.IsChecked Then
                arrVendorType.Add("TTM")
            End If


            Comp_Name = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER WHERE Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            CompanyAdd = clsDBFuncationality.getSingleValue("select top(1) Add1+' '+Add2+' '+Add3  from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'")
            Dim objcust As FrmRptCustomerLedgerDemo = New FrmRptCustomerLedgerDemo(clsUserMgtCode.mbtnCustomerLedger)
            Dim objvendor As frmRptVendorLedger = New frmRptVendorLedger(clsUserMgtCode.VendorLedgerReport)
            Dim strFromDatefilter As String = clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MMM/yyyy")
            Dim strToDatefilter As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy")
            Dim strFilterCustomer As String = String.Empty
            If TxtCustomer.arrValueMember IsNot Nothing AndAlso TxtCustomer.arrValueMember.Count > 0 Then
                strFilterCustomer = clsCommon.GetMulcallString(TxtCustomer.arrValueMember)
            End If
            Dim strCustomerBaseqry As String = clsCustomerMaster.GetCustomerBaseQry(chkItemWise.Checked, False, "", False, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), strFilterCustomer, False, strFromDatefilter, strToDatefilter, False, False, chkIncludeApplyDocument.Checked)
            Dim strCustomerBaseqryopening As String = clsCustomerMaster.GetCustomerBaseQry(chkItemWise.Checked, False, "", False, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), strFilterCustomer, True, strFromDatefilter, strToDatefilter, False, False, chkIncludeApplyDocument.Checked)
            Dim strFilterVendor As String = String.Empty
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strFilterVendor += clsCommon.GetMulcallString(txtVendor.arrValueMember)
            End If
            Dim strVendorBaseqry As String = objvendor.GetVendorBaseQry(rbPortrait.IsChecked, rbLandScape.IsChecked, False, strFromDatefilter, strToDatefilter, strFilterVendor, False, chkIncludeApplyDocument.Checked)
            Dim strVendorBaseqryOpening As String = objvendor.GetVendorBaseQry(rbPortrait.IsChecked, rbLandScape.IsChecked, False, strFromDatefilter, strToDatefilter, strFilterVendor, True, chkIncludeApplyDocument.Checked)
            'strCustomerBaseqry = "(SELECT  case when  FINALCUSTOMER.ACode is null then Vendor_Code else FINALCUSTOMER.ACode end AS ACode,FINALCUSTOMER.AName  AS AName,FINALCUSTOMER.DocNo AS DocNo,FINALCUSTOMER.DocDate AS DocDate,FINALCUSTOMER.DocType AS DocType  ,FINALCUSTOMER.DocNarr AS DocNarr,FINALCUSTOMER.ChequeDetails, FINALCUSTOMER.Currency_Code as Currency_Code, FINALCUSTOMER.ConvRate as ConvRate, FINALCUSTOMER.DrAmt AS DrAmt, FINALCUSTOMER.CrAmt AS CrAmt, FINALCUSTOMER.Location AS Location,FINALCUSTOMER.SourceCode AS SourceCode,'' AS CUSTOMER,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,FINALCUSTOMER.AgainstInvoiceNo From (" & strCustomerBaseqry & ") FINALCUSTOMER LEFT OUTER jOIN TSPL_CUSTOMER_VENDOR_MAPPING ON TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=FINALCUSTOMER.ACode) "
            'strCustomerBaseqryopening = "(SELECT case when  FINALCUSTOMER.ACode is null then Vendor_Code else FINALCUSTOMER.ACode end AS ACode,FINALCUSTOMER.AName  AS AName,FINALCUSTOMER.DocNo AS DocNo,FINALCUSTOMER.DocDate AS DocDate,FINALCUSTOMER.DocType AS DocType  ,FINALCUSTOMER.DocNarr AS DocNarr,FINALCUSTOMER.ChequeDetails, FINALCUSTOMER.Currency_Code as Currency_Code, FINALCUSTOMER.ConvRate as ConvRate, FINALCUSTOMER.DrAmt AS DrAmt, FINALCUSTOMER.CrAmt AS CrAmt, FINALCUSTOMER.Location AS Location,FINALCUSTOMER.SourceCode AS SourceCode,'' AS CUSTOMER,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,FINALCUSTOMER.AgainstInvoiceNo From (" & strCustomerBaseqryopening & ") FINALCUSTOMER LEFT OUTER jOIN TSPL_CUSTOMER_VENDOR_MAPPING ON TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=FINALCUSTOMER.ACode) "

            strCustomerBaseqry = "(SELECT FINALCUSTOMER.ACode AS ACode,FINALCUSTOMER.AName  AS AName,FINALCUSTOMER.DocNo AS DocNo,FINALCUSTOMER.DocDate AS DocDate,FINALCUSTOMER.DocType AS DocType  ,FINALCUSTOMER.DocNarr AS DocNarr,FINALCUSTOMER.ChequeDetails, FINALCUSTOMER.Currency_Code as Currency_Code, FINALCUSTOMER.ConvRate as ConvRate, FINALCUSTOMER.DrAmt AS DrAmt, FINALCUSTOMER.CrAmt AS CrAmt, FINALCUSTOMER.Location AS Location,FINALCUSTOMER.SourceCode AS SourceCode,'' AS CUSTOMER,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,FINALCUSTOMER.AgainstInvoiceNo From (" & strCustomerBaseqry & ") FINALCUSTOMER inner jOIN TSPL_CUSTOMER_VENDOR_MAPPING ON TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=FINALCUSTOMER.ACode inner jOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code and TSPL_VENDOR_MASTER.Form_Type in (" + clsCommon.GetMulcallString(arrVendorType) + ")) "
            strCustomerBaseqryopening = "(SELECT FINALCUSTOMER.ACode AS ACode,FINALCUSTOMER.AName  AS AName,FINALCUSTOMER.DocNo AS DocNo,FINALCUSTOMER.DocDate AS DocDate,FINALCUSTOMER.DocType AS DocType  ,FINALCUSTOMER.DocNarr AS DocNarr,FINALCUSTOMER.ChequeDetails, FINALCUSTOMER.Currency_Code as Currency_Code, FINALCUSTOMER.ConvRate as ConvRate, FINALCUSTOMER.DrAmt AS DrAmt, FINALCUSTOMER.CrAmt AS CrAmt, FINALCUSTOMER.Location AS Location,FINALCUSTOMER.SourceCode AS SourceCode,'' AS CUSTOMER,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,FINALCUSTOMER.AgainstInvoiceNo From (" & strCustomerBaseqryopening & ") FINALCUSTOMER inner jOIN TSPL_CUSTOMER_VENDOR_MAPPING ON TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=FINALCUSTOMER.ACode inner jOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code and TSPL_VENDOR_MASTER.Form_Type in (" + clsCommon.GetMulcallString(arrVendorType) + ")) "

            'strVendorBaseqry = "(Select case when TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code is null then FinalVENDOR.VCode else TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code end AS ACode,FinalVENDOR.VName   AS AName,FinalVENDOR.DocNo AS DocNo,FinalVENDOR.DocDate AS DocDate,FinalVENDOR.DocType AS DocType ,FinalVENDOR.DocNarr AS DocNarr ,FinalVENDOR.ChequeDetails  ,FinalVENDOR.CURRENCY_CODE  as Currency_Code,FinalVENDOR.ConvRate  as ConvRate, FinalVENDOR.DrAmt AS DrAmt, FinalVENDOR.CrAmt AS CrAmt, RIGHT(FinalVENDOR.account,3)  AS Location,FinalVENDOR.GLDocType AS SourceCode,'' AS VENDOR ,FinalVENDOR.VCode AS Vendor_Code,FinalVENDOR.PO_SRN  as AgainstInvoiceNo     from (" & strVendorBaseqry & ") FinalVENDOR inner jOIN TSPL_CUSTOMER_VENDOR_MAPPING ON TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code =FinalVENDOR.VCode inner jOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=FinalVENDOR.VCode and TSPL_VENDOR_MASTER.Form_Type in (" + clsCommon.GetMulcallString(arrVendorType) + ")) "
            'strVendorBaseqryOpening = "(Select case when TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code is null then FinalVENDOR.VCode else TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code end ACode,FinalVENDOR.VName   AS AName,FinalVENDOR.DocNo AS DocNo,FinalVENDOR.DocDate AS DocDate,FinalVENDOR.DocType AS DocType ,FinalVENDOR.DocNarr AS DocNarr ,FinalVENDOR.ChequeDetails  ,FinalVENDOR.CURRENCY_CODE  as Currency_Code,FinalVENDOR.ConvRate  as ConvRate, FinalVENDOR.DrAmt AS DrAmt, FinalVENDOR.CrAmt AS CrAmt, RIGHT(FinalVENDOR.account,3)  AS Location,FinalVENDOR.GLDocType AS SourceCode,'' AS VENDOR ,FinalVENDOR.VCode AS Vendor_Code,FinalVENDOR.PO_SRN  as AgainstInvoiceNo     from (" & strVendorBaseqryOpening & ") FinalVENDOR inner jOIN TSPL_CUSTOMER_VENDOR_MAPPING ON TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code =FinalVENDOR.VCode inner jOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=FinalVENDOR.VCode and TSPL_VENDOR_MASTER.Form_Type in (" + clsCommon.GetMulcallString(arrVendorType) + ")) "

            strVendorBaseqry = strVendorBaseqry + "WHERE TSPL_VENDOR_MASTER.Form_Type in (" + clsCommon.GetMulcallString(arrVendorType) + ")"
            strVendorBaseqryOpening = strVendorBaseqryOpening + "WHERE TSPL_VENDOR_MASTER.Form_Type in (" + clsCommon.GetMulcallString(arrVendorType) + ")"

            strVendorBaseqry = "(Select case when TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code is null then FinalVENDOR.VCode else TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code end AS ACode,FinalVENDOR.VName   AS AName,FinalVENDOR.DocNo AS DocNo,FinalVENDOR.DocDate AS DocDate,FinalVENDOR.DocType AS DocType ,FinalVENDOR.DocNarr AS DocNarr ,FinalVENDOR.ChequeDetails  ,FinalVENDOR.CURRENCY_CODE  as Currency_Code,FinalVENDOR.ConvRate  as ConvRate, FinalVENDOR.DrAmt AS DrAmt, FinalVENDOR.CrAmt AS CrAmt, RIGHT(FinalVENDOR.account,3)  AS Location,FinalVENDOR.GLDocType AS SourceCode,'' AS VENDOR ,FinalVENDOR.VCode AS Vendor_Code,FinalVENDOR.PO_SRN  as AgainstInvoiceNo     from (" & strVendorBaseqry & ") FinalVENDOR left outer jOIN TSPL_CUSTOMER_VENDOR_MAPPING ON TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code =FinalVENDOR.VCode left outer jOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=FinalVENDOR.VCode and TSPL_VENDOR_MASTER.Form_Type in (" + clsCommon.GetMulcallString(arrVendorType) + ")) "
            strVendorBaseqryOpening = "(Select case when TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code is null then FinalVENDOR.VCode else TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code end ACode,FinalVENDOR.VName   AS AName,FinalVENDOR.DocNo AS DocNo,FinalVENDOR.DocDate AS DocDate,FinalVENDOR.DocType AS DocType ,FinalVENDOR.DocNarr AS DocNarr ,FinalVENDOR.ChequeDetails  ,FinalVENDOR.CURRENCY_CODE  as Currency_Code,FinalVENDOR.ConvRate  as ConvRate, FinalVENDOR.DrAmt AS DrAmt, FinalVENDOR.CrAmt AS CrAmt, RIGHT(FinalVENDOR.account,3)  AS Location,FinalVENDOR.GLDocType AS SourceCode,'' AS VENDOR ,FinalVENDOR.VCode AS Vendor_Code,FinalVENDOR.PO_SRN  as AgainstInvoiceNo     from (" & strVendorBaseqryOpening & ") FinalVENDOR left outer jOIN TSPL_CUSTOMER_VENDOR_MAPPING ON TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code =FinalVENDOR.VCode left outer jOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=FinalVENDOR.VCode and TSPL_VENDOR_MASTER.Form_Type in (" + clsCommon.GetMulcallString(arrVendorType) + ")) "

            Dim strFinalustomerVendorqryopening As String = " SELECT MAX(FINALCUSTOMERVENDOR.ACode) AS ACode,MAX(FINALCUSTOMERVENDOR.AName)  AS AName,'' AS DocNo,MAX(FINALCUSTOMERVENDOR.DocDate) AS DocDate,'OP' AS DocType  ,'' AS DocNarr,max(FINALCUSTOMERVENDOR.ChequeDetails)as ChequeDetails, MAX(FINALCUSTOMERVENDOR.Currency_Code) as Currency_Code, sUM(FINALCUSTOMERVENDOR.DrAmt) AS DrAmt, SUM(FINALCUSTOMERVENDOR.CrAmt) AS CrAmt, MAX(FINALCUSTOMERVENDOR.Location) AS Location,'' AS SourceCode,'' AS CUSTOMER,FINALCUSTOMERVENDOR.Vendor_Code,max(FINALCUSTOMERVENDOR.AgainstInvoiceNo) as AgainstInvoiceNo FROM ( " & strCustomerBaseqryopening & " Union All " & strVendorBaseqryOpening & " )FINALCUSTOMERVENDOR WHERE FINALCUSTOMERVENDOR.DocDate <'" & clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MMM/yyyy") & "' GROUP BY FINALCUSTOMERVENDOR.Vendor_Code "
            Dim strFinalustomerVendorqryopeningforSummary As String = " SELECT MAX(FINALCUSTOMERVENDOR.ACode) AS ACode,MAX(FINALCUSTOMERVENDOR.AName)  AS AName,'' AS DocNo,MAX(FINALCUSTOMERVENDOR.DocDate) AS DocDate,'OP' AS DocType  ,'' AS DocNarr,max(FINALCUSTOMERVENDOR.ChequeDetails)as ChequeDetails, MAX(FINALCUSTOMERVENDOR.Currency_Code) as Currency_Code, sUM(FINALCUSTOMERVENDOR.DrAmt) AS DrAmtOP, SUM(FINALCUSTOMERVENDOR.CrAmt) AS CrAmtOP, 0 as DrAmt, 0 as CrAmt , MAX(FINALCUSTOMERVENDOR.Location) AS Location,'' AS SourceCode,'' AS CUSTOMER,FINALCUSTOMERVENDOR.Vendor_Code,max(FINALCUSTOMERVENDOR.AgainstInvoiceNo) as AgainstInvoiceNo FROM ( " & strCustomerBaseqryopening & " Union All " & strVendorBaseqryOpening & " )FINALCUSTOMERVENDOR WHERE FINALCUSTOMERVENDOR.DocDate <'" & clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MMM/yyyy") & "' GROUP BY FINALCUSTOMERVENDOR.Vendor_Code "
            Dim strFinalustomerVendorqryclosing As String = Nothing
            If rdbDetail.IsChecked Then
                strFinalustomerVendorqryclosing = "With CTETemp as (Select ROW_NUMBER() OVER (PARTITION BY Vendor_Code ORDER BY Vendor_Code, DocDate) as RowNo, '13/Aug/2015 12:50 PM'  as RunDate,'" & fromdate & "' as fromdate,'" & todate & "' as todate,'" & Comp_Name & "' as Comp_Name ,'" & CompanyAdd & "' as CompanyAdd , CustomerVendorFinal.* from (" & strFinalustomerVendorqryopening & " " &
                 " Union All "
                '--############################################################################################BADA UNION#####################################
                strFinalustomerVendorqryclosing += " (SELECT FINALCUSTOMERVENDOR.ACode, FINALCUSTOMERVENDOR.AName, FINALCUSTOMERVENDOR.DocNo, FINALCUSTOMERVENDOR.DocDate, FINALCUSTOMERVENDOR.DocType, FINALCUSTOMERVENDOR.DocNarr,FINALCUSTOMERVENDOR.ChequeDetails, FINALCUSTOMERVENDOR.Currency_Code, FINALCUSTOMERVENDOR.DrAmt, FINALCUSTOMERVENDOR.CrAmt, FINALCUSTOMERVENDOR.Location, FINALCUSTOMERVENDOR.SourceCode, '' AS CUSTOMER, FINALCUSTOMERVENDOR.Vendor_Code,FINALCUSTOMERVENDOR.AgainstInvoiceNo FROM ( " & strCustomerBaseqry & " Union All " & strVendorBaseqry & " )FINALCUSTOMERVENDOR WHERE FINALCUSTOMERVENDOR.DocDate >='" & clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MMM/yyyy") & "'  and FINALCUSTOMERVENDOR.DocDate <='" & clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy") & "'  )" &
                                              " ) CustomerVendorFinal where 1=1  "
                If (TxtCustomer.arrValueMember IsNot Nothing AndAlso TxtCustomer.arrValueMember.Count > 0) AndAlso (txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0) Then
                    strFinalustomerVendorqryclosing += " and (CustomerVendorFinal.ACode in (" + clsCommon.GetMulcallString(TxtCustomer.arrValueMember) + ")  "
                    strFinalustomerVendorqryclosing += " or CustomerVendorFinal.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + "))  "
                Else
                    If TxtCustomer.arrValueMember IsNot Nothing AndAlso TxtCustomer.arrValueMember.Count > 0 Then
                        strFinalustomerVendorqryclosing += " and CustomerVendorFinal.ACode in (" + clsCommon.GetMulcallString(TxtCustomer.arrValueMember) + ")  "
                    End If

                    If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                        strFinalustomerVendorqryclosing += " and CustomerVendorFinal.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")  "
                    End If
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    strFinalustomerVendorqryclosing += " and CustomerVendorFinal.Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
                Else
                    strFinalustomerVendorqryclosing += " and CustomerVendorFinal.Location in (" + StrPermission + ")  "
                End If
                If clsCommon.myLen(txtCurrencyCode.Value) > 0 Then
                    strFinalustomerVendorqryclosing += " and CustomerVendorFinal.Currency_Code= '" + clsCommon.myCstr(txtCurrencyCode.Value) + "'  "
                End If
                strFinalustomerVendorqryclosing += ")Select CTETemp.RowNo, CTETemp.RunDate ,CTETemp.fromdate,CTETemp.todate,CTETemp.Comp_Name,CTETemp.CompanyAdd, CTETemp.ACode , CTETemp.AName , CTETemp.DocNo,CONVERT(VARCHAR,CTETemp.DocDate,103) as DocDate,CASE WHEN CTETemp.DocNarr LIKE '%Opening Bal%' then 'OP' else CTETemp.DocType end as DocType,  CTETemp.DocNarr,CTETemp.ChequeDetails, CTETemp.Currency_Code, CTETemp.DrAmt , CTETemp.CrAmt,SUM(DrAmt -CrAmt) Over (Partition by CTETemp.Vendor_Code ORDER BY RowNo) as [Closing], CTETemp.Location , CTETemp.SourceCode, CTETemp.Vendor_Code,CTETemp.AgainstInvoiceNo from CTETemp " &
                "left outer join TSPL_CUSTOMER_MASTER on CTETemp.ACode =TSPL_CUSTOMER_MASTER.Cust_Code " &
                " left outer join TSPL_CUSTOMER_GROUP_MASTER  on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code  =TSPL_CUSTOMER_MASTER.Cust_Group_Code left outer jOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code= CTETemp.Vendor_Code where TSPL_VENDOR_MASTER.Form_Type in (" + clsCommon.GetMulcallString(arrVendorType) + ") "
                If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    strFinalustomerVendorqryclosing += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")  "
                End If
                strFinalustomerVendorqryclosing += " ORDER BY CTETemp.Vendor_Code  , RowNo"
            ElseIf rdbSummary.IsChecked Then
                strFinalustomerVendorqryclosing = "  sELECT zz.* FROM ( select ACode as ACode,max(AName) as AName ,(sum(DrAmtOP)- sum(CrAmtOP)) as 'Opening',sum(DrAmt) as DrAmt ,sum(CrAmt) as CrAmt, ((sum(DrAmtOP)- sum(CrAmtOP)) + sum(DrAmt) -sum(CrAmt)) as 'Closing' ,Vendor_Code     from (Select ROW_NUMBER() OVER (PARTITION BY Vendor_Code ORDER BY Vendor_Code, DocDate) as RowNo, '13/Aug/2015 12:50 PM'  as RunDate,'" & fromdate & "' as fromdate,'" & todate & "' as todate,'" & Comp_Name & "' as Comp_Name ,'" & CompanyAdd & "' as CompanyAdd , CustomerVendorFinal.* from (" & strFinalustomerVendorqryopeningforSummary & " " &
            " Union All "
                '--############################################################################################BADA UNION#####################################
                strFinalustomerVendorqryclosing += " (SELECT FINALCUSTOMERVENDOR.ACode, FINALCUSTOMERVENDOR.AName, FINALCUSTOMERVENDOR.DocNo, FINALCUSTOMERVENDOR.DocDate, FINALCUSTOMERVENDOR.DocType, FINALCUSTOMERVENDOR.DocNarr,FINALCUSTOMERVENDOR.ChequeDetails, FINALCUSTOMERVENDOR.Currency_Code,  0 AS DrAmtOP, 0 AS CrAmtOP,  FINALCUSTOMERVENDOR.DrAmt, FINALCUSTOMERVENDOR.CrAmt, FINALCUSTOMERVENDOR.Location, FINALCUSTOMERVENDOR.SourceCode, '' AS CUSTOMER, FINALCUSTOMERVENDOR.Vendor_Code,FINALCUSTOMERVENDOR.AgainstInvoiceNo FROM ( " & strCustomerBaseqry & " Union All " & strVendorBaseqry & " )FINALCUSTOMERVENDOR WHERE FINALCUSTOMERVENDOR.DocDate >='" & clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MMM/yyyy") & "'  and FINALCUSTOMERVENDOR.DocDate <='" & clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy") & "'  )" &
                                          " ) CustomerVendorFinal where 1=1  "
                If (TxtCustomer.arrValueMember IsNot Nothing AndAlso TxtCustomer.arrValueMember.Count > 0) AndAlso (txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0) Then
                    strFinalustomerVendorqryclosing += " and (CustomerVendorFinal.ACode in (" + clsCommon.GetMulcallString(TxtCustomer.arrValueMember) + ")  "
                    strFinalustomerVendorqryclosing += " or CustomerVendorFinal.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + "))  "
                Else
                    If TxtCustomer.arrValueMember IsNot Nothing AndAlso TxtCustomer.arrValueMember.Count > 0 Then
                        strFinalustomerVendorqryclosing += " and CustomerVendorFinal.ACode in (" + clsCommon.GetMulcallString(TxtCustomer.arrValueMember) + ")  "
                    End If
                    If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                        strFinalustomerVendorqryclosing += " and CustomerVendorFinal.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")  "
                    End If
                End If
                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    strFinalustomerVendorqryclosing += " and CustomerVendorFinal.Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
                Else
                    strFinalustomerVendorqryclosing += " and CustomerVendorFinal.Location in (" + StrPermission + ")  "
                End If
                If clsCommon.myLen(txtCurrencyCode.Value) > 0 Then
                    strFinalustomerVendorqryclosing += " and CustomerVendorFinal.Currency_Code= '" + clsCommon.myCstr(txtCurrencyCode.Value) + "'  "
                End If
                strFinalustomerVendorqryclosing += ")final " &
                "left outer join TSPL_CUSTOMER_MASTER on final.ACode =TSPL_CUSTOMER_MASTER.Cust_Code " &
                    " left outer join TSPL_CUSTOMER_GROUP_MASTER  on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code  =TSPL_CUSTOMER_MASTER.Cust_Group_Code "
                If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    strFinalustomerVendorqryclosing += " WHERE TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")  "
                End If
                strFinalustomerVendorqryclosing += " group by ACode ,Vendor_Code )zz  left outer jOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code= zz.Vendor_Code where TSPL_VENDOR_MASTER.Form_Type in (" + clsCommon.GetMulcallString(arrVendorType) + ") ORDER BY ZZ.ACode   "
            End If
            dtOpening = clsDBFuncationality.GetDataTable(strFinalustomerVendorqryclosing)

            If dtOpening.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Data not found", Me.Text)
                gv.DataSource = Nothing
                Exit Sub
            Else
                If chkVendorWise.IsChecked Or chkNone.IsChecked Then
                    btnBack.Enabled = True
                Else
                    btnBack.Enabled = False
                End If
            End If

            gv.DataSource = Nothing
            gv.DataSource = dtOpening
            SetGridFormat(False)
            ReStoreGridLayout()
            gv.Visible = True
            RadPageView1.SelectedPage = RadPageViewPage2
            RadGroupBox1.Enabled = False
            If blnRefresh = False Then
                ''richa KDI/15/10/18-000438
                Dim frmCRV As New frmCrystalReportViewer()
                If rdbDetail.IsChecked Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtOpening, "CustomerVendorLedgerDetail", "Customer Vendor Ledger Report")
                Else
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtOpening, "CustomerVendorLedger", "Customer Vendor Ledger Report")
                End If
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub FormatgvVendorGroup()
        Try
            gvVendorGroup.AllowAddNewRow = False
            gvVendorGroup.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 0 To gvVendorGroup.Columns.Count - 1
                gvVendorGroup.Columns(ii).ReadOnly = True
                gvVendorGroup.Columns(ii).IsVisible = False
            Next
            gvVendorGroup.Columns("Vendor_Group_Code").IsVisible = True
            gvVendorGroup.Columns("Vendor_Group_Code").Width = 180
            gvVendorGroup.Columns("Vendor_Group_Code").HeaderText = "Vendor Group Code"

            gvVendorGroup.Columns("Vendor_Group_Desc").IsVisible = True
            gvVendorGroup.Columns("Vendor_Group_Desc").Width = 350
            gvVendorGroup.Columns("Vendor_Group_Desc").HeaderText = "Vendor Group Desc"

            gvVendorGroup.Columns("AccountSet").IsVisible = True
            gvVendorGroup.Columns("AccountSet").Width = 200

            gvVendorGroup.Columns("OpngBal").IsVisible = True
            gvVendorGroup.Columns("OpngBal").Width = 150
            gvVendorGroup.Columns("OpngBal").HeaderText = "Opening Balance"
            gvVendorGroup.Columns("OpngBal").FormatString = "{0:f2}"


            If FormType = clsUserMgtCode.MISCreditorReport Then
                gvVendorGroup.Columns("Purchase").IsVisible = True
                gvVendorGroup.Columns("Purchase").Width = 100
                gvVendorGroup.Columns("Purchase").FormatString = "{0:f2}"

                gvVendorGroup.Columns("Payments").IsVisible = True
                gvVendorGroup.Columns("Payments").Width = 100
                gvVendorGroup.Columns("Payments").FormatString = "{0:f2}"

                gvVendorGroup.Columns("DrNote").IsVisible = True
                gvVendorGroup.Columns("DrNote").Width = 100
                gvVendorGroup.Columns("DrNote").FormatString = "{0:f2}"

                gvVendorGroup.Columns("CrNote").IsVisible = True
                gvVendorGroup.Columns("CrNote").Width = 100
                gvVendorGroup.Columns("CrNote").FormatString = "{0:f2}"
            Else
                gvVendorGroup.Columns("CrAmt").IsVisible = True
                gvVendorGroup.Columns("CrAmt").Width = 100
                gvVendorGroup.Columns("CrAmt").HeaderText = "CrAmt"
                gvVendorGroup.Columns("CrAmt").FormatString = "{0:f2}"

                gvVendorGroup.Columns("DrAmt").IsVisible = True
                gvVendorGroup.Columns("DrAmt").Width = 100
                gvVendorGroup.Columns("DrAmt").HeaderText = "DrAmt"
                gvVendorGroup.Columns("DrAmt").FormatString = "{0:f2}"
            End If

            gvVendorGroup.Columns("Closing").IsVisible = True
            gvVendorGroup.Columns("Closing").Width = 100
            gvVendorGroup.Columns("Closing").FormatString = "{0:f2}"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim TotalAmt As New GridViewSummaryItem("OpngBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("Purchase", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("Payments", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            gvVendorGroup.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gvVendorGroup.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub FormatgvVendor()
        Try
            gvVendor.AllowAddNewRow = False
            gvVendor.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 0 To gvVendor.Columns.Count - 1
                gvVendor.Columns(ii).ReadOnly = True
                gvVendor.Columns(ii).IsVisible = False
            Next
            gvVendor.Columns("VCode").IsVisible = True
            gvVendor.Columns("VCode").Width = 180
            gvVendor.Columns("VCode").HeaderText = "Customer Code"

            gvVendor.Columns("VName").IsVisible = True
            gvVendor.Columns("VName").Width = 350
            gvVendor.Columns("VName").HeaderText = "Name"

            gvVendor.Columns("Vendor_Group_Desc").IsVisible = True
            gvVendor.Columns("Vendor_Group_Desc").Width = 350
            gvVendor.Columns("Vendor_Group_Desc").HeaderText = "Vendor Group"

            gvVendor.Columns("AccountSet").IsVisible = True
            gvVendor.Columns("AccountSet").Width = 200

            gvVendor.Columns("OpngBal").IsVisible = True
            gvVendor.Columns("OpngBal").Width = 150
            gvVendor.Columns("OpngBal").HeaderText = "Opening Balance"
            gvVendor.Columns("OpngBal").FormatString = "{0:f2}"

            If FormType = clsUserMgtCode.MISCreditorReport Then
                gvVendor.Columns("Purchase").IsVisible = True
                gvVendor.Columns("Purchase").Width = 100
                gvVendor.Columns("Purchase").FormatString = "{0:f2}"

                gvVendor.Columns("Payments").IsVisible = True
                gvVendor.Columns("Payments").Width = 100
                gvVendor.Columns("Payments").FormatString = "{0:f2}"

                gvVendor.Columns("DrNote").IsVisible = True
                gvVendor.Columns("DrNote").Width = 100
                gvVendor.Columns("DrNote").FormatString = "{0:f2}"

                gvVendor.Columns("CrNote").IsVisible = True
                gvVendor.Columns("CrNote").Width = 100
                gvVendor.Columns("CrNote").FormatString = "{0:f2}"
            Else
                gvVendor.Columns("CrAmt").IsVisible = True
                gvVendor.Columns("CrAmt").Width = 100
                gvVendor.Columns("CrAmt").HeaderText = "CrAmt"
                gvVendor.Columns("CrAmt").FormatString = "{0:f2}"

                gvVendor.Columns("DrAmt").IsVisible = True
                gvVendor.Columns("DrAmt").Width = 100
                gvVendor.Columns("DrAmt").HeaderText = "DrAmt"
                gvVendor.Columns("DrAmt").FormatString = "{0:f2}"
            End If

            gvVendor.Columns("Closing").IsVisible = True
            gvVendor.Columns("Closing").Width = 100
            gvVendor.Columns("Closing").FormatString = "{0:f2}"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim TotalAmt As New GridViewSummaryItem("OpngBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("Purchase", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("Payments", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            gvVendor.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gvVendor.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub SetGridFormat(ByVal IsFromDrillDown As Boolean)
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        gv.ShowGroupPanel = False
        Dim summaryRowItem As New GridViewSummaryRowItem()

        gv.AllowAddNewRow = False

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next
        If rdbDetail.IsChecked Then
            gv.Columns("Acode").IsVisible = True
            gv.Columns("Acode").Width = 50
            gv.Columns("Acode").HeaderText = "Customer Code"

            gv.Columns("AName").IsVisible = True
            gv.Columns("AName").Width = 100
            gv.Columns("AName").HeaderText = "Customer Name"

            gv.Columns("DocNo").IsVisible = True
            gv.Columns("DocNo").Width = 100
            gv.Columns("DocNo").HeaderText = "Document Number"

            gv.Columns("DocDate").IsVisible = True
            gv.Columns("DocDate").Width = 100
            gv.Columns("DocDate").HeaderText = "Document Date"
            gv.Columns("DocDate").FormatString = "{0:d}"

            gv.Columns("DocType").IsVisible = True
            gv.Columns("DocType").Width = 100
            gv.Columns("DocType").HeaderText = "Document Type"

            gv.Columns("DocNarr").IsVisible = True
            gv.Columns("DocNarr").Width = 250
            gv.Columns("DocNarr").HeaderText = "Document Narr"

            gv.Columns("Currency_Code").IsVisible = True
            gv.Columns("Currency_Code").Width = 80
            gv.Columns("Currency_Code").HeaderText = "Currency"

            gv.Columns("DrAmt").IsVisible = True
            gv.Columns("DrAmt").Width = 100
            gv.Columns("DrAmt").HeaderText = "DrAmt"
            gv.Columns("DrAmt").FormatString = "{0:f2}"

            gv.Columns("CrAmt").IsVisible = True
            gv.Columns("CrAmt").Width = 100
            gv.Columns("CrAmt").HeaderText = "CrAmt"
            gv.Columns("CrAmt").FormatString = "{0:f2}"

            gv.Columns("Closing").IsVisible = True
            gv.Columns("Closing").Width = 100
            gv.Columns("Closing").HeaderText = "Closing"
            gv.Columns("Closing").FormatString = "{0:f2}"

            gv.Columns("Location").IsVisible = True
            gv.Columns("Location").Width = 100
            gv.Columns("Location").HeaderText = "Location"

            gv.Columns("SourceCode").IsVisible = True
            gv.Columns("SourceCode").Width = 100
            gv.Columns("SourceCode").HeaderText = "Source Code"

            gv.Columns("Vendor_Code").IsVisible = True
            gv.Columns("Vendor_Code").Width = 100
            gv.Columns("Vendor_Code").HeaderText = "Vendor Code"

            'gv.GroupDescriptors.Add(New GridGroupByExpression("ACode as ACode  format ""{0}: {1}"" group by ACode"))
            'gv.AutoExpandGroups = True
            ''
            If ChkPDC.Enabled = True AndAlso ChkPDC.Checked = True Then
                Dim ttllAmt As New GridViewSummaryItem("DrAmt", "{0:F2}", "sum(DrPDC)")
                summaryRowItem.Add(ttllAmt)
                ttllAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", "sum(CrPDC)")
                summaryRowItem.Add(ttllAmt)
                ttllAmt = New GridViewSummaryItem("EffectiveAmt", "{0:F2}", "sum(EffPDC)")
                summaryRowItem.Add(ttllAmt)
            Else
                Dim ttllAmt As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(ttllAmt)
                ttllAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(ttllAmt)
                ttllAmt = New GridViewSummaryItem("EffectiveAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(ttllAmt)
            End If

            Dim TotalAmt As New GridViewSummaryItem("Purchase", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("Payments", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
        ElseIf rdbSummary.IsChecked Then
            gv.Columns("Acode").IsVisible = True
            gv.Columns("Acode").Width = 50
            gv.Columns("Acode").HeaderText = "Customer Code"

            gv.Columns("AName").IsVisible = True
            gv.Columns("AName").Width = 100
            gv.Columns("AName").HeaderText = "Customer Name"

            gv.Columns("Opening").IsVisible = True
            gv.Columns("Opening").Width = 100
            gv.Columns("Opening").HeaderText = "Opening"
            gv.Columns("Opening").FormatString = "{0:f2}"

            gv.Columns("DrAmt").IsVisible = True
            gv.Columns("DrAmt").Width = 100
            gv.Columns("DrAmt").HeaderText = "DrAmt"
            gv.Columns("DrAmt").FormatString = "{0:f2}"

            gv.Columns("CrAmt").IsVisible = True
            gv.Columns("CrAmt").Width = 100
            gv.Columns("CrAmt").HeaderText = "CrAmt"
            gv.Columns("CrAmt").FormatString = "{0:f2}"


            gv.Columns("Closing").IsVisible = True
            gv.Columns("Closing").Width = 100
            gv.Columns("Closing").HeaderText = "Closing"
            gv.Columns("Closing").FormatString = "{0:f2}"

            gv.Columns("Vendor_Code").IsVisible = True
            gv.Columns("Vendor_Code").Width = 100
            gv.Columns("Vendor_Code").HeaderText = "Vendor Code"


            ''
            If ChkPDC.Enabled = True AndAlso ChkPDC.Checked = True Then
                Dim ttllAmt As New GridViewSummaryItem("DrAmt", "{0:F2}", "sum(DrPDC)")
                summaryRowItem.Add(ttllAmt)
                ttllAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", "sum(CrPDC)")
                summaryRowItem.Add(ttllAmt)

            Else
                Dim ttllAmt As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(ttllAmt)
                ttllAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(ttllAmt)

                ttllAmt = New GridViewSummaryItem("Opening", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(ttllAmt)
                ttllAmt = New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(ttllAmt)
             

            End If


        End If


        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub
    ''
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmRptVendorLedger_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso btnPrint.Enabled Then
            Print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F12 Then
            If pnlAdminSetting.Visible Then
                pnlAdminSetting.Visible = False
            Else
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    pnlAdminSetting.Visible = True
                End If
            End If


        End If
    End Sub


    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        blnRefresh = True
        PageSetupReport_ID = MyBase.Form_ID + IIf(rdbSummary.IsChecked = True, "S", "D") + IIf(rbPortrait.IsChecked = True, "P", "L")
        TemplateGridview = gv
        Print()
    End Sub

    Private Sub btnPrint_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        blnRefresh = False
        Print()
    End Sub



    Private Sub gv_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs)
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
        Dim DocNo As String
        Dim DocType As String
        DocNo = clsCommon.myCstr(gv.CurrentRow.Cells("DocNo").Value)
        DocType = clsCommon.myCstr(gv.CurrentRow.Cells("GLDocType").Value)
        If clsCommon.CompairString(DocType, "AP-PY") = CompairStringResult.Equal Or clsCommon.CompairString(DocType, "AP-MI") = CompairStringResult.Equal Then
            Dim frm As New FrmPaymentNew
            frm.SetUserMgmt(clsUserMgtCode.PaymentEntryNew)
            frm.Show()
            frm.LoadData(DocNo, NavigatorType.Current)
        ElseIf clsCommon.CompairString(DocType, "AP-IN") = CompairStringResult.Equal Then
            Dim frm As New FrmAPInvoiceEntry
            frm.SetUserMgmt(clsUserMgtCode.mbtnAPInvoiceEntry)
            frm.Show()
            frm.LoadData(DocNo)
        ElseIf clsCommon.CompairString(DocType, "RV-TA") = CompairStringResult.Equal Then
            Dim frm As New frmReverseTransaction(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
            frm.SetUserMgmt(clsUserMgtCode.reverseTransaction)
            frm.Show()
            frm.fndreversecode.Value = DocNo
            frm.funFill4()
        ElseIf clsCommon.CompairString(DocType, "VC-GL") = CompairStringResult.Equal Then
            Dim frm As New frmVCGLEntry
            frm.SetUserMgmt(clsUserMgtCode.mbtnVCGLEntry)
            frm.Show()
            frm.LoadData(DocNo)
        End If

    End Sub

    Private Sub gridHideVisible()
        gv.Visible = False
        gvVendor.Visible = False
        gvVendorGroup.Visible = False
        If chkVendorGrupWise.IsChecked Then
            gvVendorGroup.Visible = True
            btnBack.Enabled = False
        ElseIf chkVendorWise.IsChecked Then
            gvVendor.Visible = True
            btnBack.Enabled = True
        Else
            gv.Visible = True
            btnBack.Enabled = True
        End If
    End Sub

    Private Sub gvVendorGroup_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvVendorGroup.CellDoubleClick
        Try
            If clsCommon.myLen(gvVendorGroup.CurrentRow.Cells("Vendor_Group_Code").Value) > 0 Then
                dvTemp = New DataView(dtCustomer)
                dvTemp.RowFilter = "Vendor_Group_Code='" + gvVendorGroup.CurrentRow.Cells("Vendor_Group_Code").Value + "'"
                gvVendor.DataSource = dvTemp.ToTable()
                FormatgvVendor()
                gvVendor.Visible = True
                gvVendorGroup.Visible = False
                gv.Visible = False
                btnBack.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvVendor_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvVendor.CellDoubleClick
        Try
            If clsCommon.myLen(gvVendor.CurrentRow.Cells("VCode").Value) > 0 Then
                If e.Column Is gvVendor.Columns("VCode") Or e.Column Is gvVendor.Columns("VName") Then
                    dvTemp = New DataView(dtMain)
                    dvTemp.RowFilter = "VCode='" + gvVendor.CurrentRow.Cells("VCode").Value + "'"
                    gv.DataSource = dvTemp.ToTable()
                    SetGridFormat(False)
                ElseIf e.Column Is gvVendor.Columns("OpngBal") Then
                    dvTemp = New DataView(dtOpening)
                    dvTemp.RowFilter = "VCode='" + gvVendor.CurrentRow.Cells("VCode").Value + "'"
                    gv.DataSource = dvTemp.ToTable()
                    SetGridFormat(True)
                ElseIf e.Column Is gvVendor.Columns("Purchase") Then
                    dvTemp = New DataView(dtMain)
                    dvTemp.RowFilter = "VCode='" + gvVendor.CurrentRow.Cells("VCode").Value + "' AND ISNULL(Purchase,0)<>0"
                    gv.DataSource = dvTemp.ToTable()
                    SetGridFormat(True)
                ElseIf e.Column Is gvVendor.Columns("Payments") Then
                    dvTemp = New DataView(dtMain)
                    dvTemp.RowFilter = "VCode='" + gvVendor.CurrentRow.Cells("VCode").Value + "' AND ISNULL(Payments,0)<>0"
                    gv.DataSource = dvTemp.ToTable()
                    SetGridFormat(True)
                ElseIf e.Column Is gvVendor.Columns("DrNote") Then
                    dvTemp = New DataView(dtMain)
                    dvTemp.RowFilter = "VCode='" + gvVendor.CurrentRow.Cells("VCode").Value + "' AND ISNULL(DrNote,0)<>0"
                    gv.DataSource = dvTemp.ToTable()
                    SetGridFormat(True)
                ElseIf e.Column Is gvVendor.Columns("CrNote") Then
                    dvTemp = New DataView(dtMain)
                    dvTemp.RowFilter = "VCode='" + gvVendor.CurrentRow.Cells("VCode").Value + "' AND ISNULL(CrNote,0)<>0"
                    gv.DataSource = dvTemp.ToTable()
                    SetGridFormat(True)
                End If
                gvVendorGroup.Visible = False
                gvVendor.Visible = False
                gv.Visible = True
                btnBack.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If gv.Visible = True Then
            gvVendor.Visible = True
            gvVendorGroup.Visible = False
            gv.Visible = False
        ElseIf gvVendor.Visible = True Then
            gvVendorGroup.Visible = True
            gvVendor.Visible = False
            gv.Visible = False
            btnBack.Enabled = False
        End If
    End Sub

    Private Sub gv_CellDoubleClick1(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        If rdbDetail.IsChecked Then
            If clsCommon.myLen(e.Row.Cells.Item("DocNo").Value) > 0 Then
                Dim SoucrCode As String = clsCommon.myCstr(gv.Rows(e.RowIndex).Cells.Item("DocType").Value)
                Dim DocNo As String = clsCommon.myCstr(e.Row.Cells.Item("DocNo").Value)
                'Pur.Invoice()            Vendor,TDS, ,
                If SoucrCode = "Credit Note" Or SoucrCode = "AP Invoice" Or SoucrCode = "Pur.Invoice" Then
                    If clsCommon.CompairString(clsDBFuncationality.getSingleValue("Select isnull(is_For_TDS,0) from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" & DocNo & "'"), "1") = CompairStringResult.Equal Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnAPInvoiceEntryTDS, DocNo)
                    Else
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, DocNo)
                    End If
                ElseIf SoucrCode = "Advance" Or SoucrCode = "Receipt" Or SoucrCode = "On Account" Or SoucrCode = "Payment" Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, DocNo)
                    'ElseIf SoucrCode = "Pur.Invoice" Then
                    '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, DocNo)
                ElseIf SoucrCode = "Vendor" Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnVCGLEntry, DocNo)
                ElseIf SoucrCode = "TDS" Or SoucrCode = "Debit Note" Then
                    If clsCommon.CompairString(clsDBFuncationality.getSingleValue("Select isnull(is_For_TDS,0) from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" & DocNo & "'"), "1") = CompairStringResult.Equal Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnAPInvoiceEntryTDS, DocNo)
                    Else
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, DocNo)
                    End If
                Else
                    Return
                End If

            End If


        End If
    
        'End If
    End Sub

    Private Sub txtCurrencyCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCurrencyCode._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        txtCurrencyCode.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
    End Sub

    Sub SetMultiCurrencyVisibility()
        Dim strq As String = ""
        If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
            txtCurrencyCode.Enabled = True
        Else
            txtCurrencyCode.Enabled = False
        End If
    End Sub

    Private Sub txtVendorGroup__My_Click(sender As Object, e As EventArgs) Handles txtVendorGroup._My_Click
        strQry = "select Ven_Group_Code as Code, Group_Desc as Name from TSPL_VENDOR_GROUP order by Ven_Group_Code "
        txtVendorGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorGrpSelector@VendorLedger", strQry, "Code", "Name", txtVendorGroup.arrValueMember, txtVendorGroup.arrDispalyMember)
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        strQry = "Select TSPL_VENDOR_MASTER.Vendor_Code as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name from TSPL_VENDOR_MASTER Left Outer join TSPL_CUSTOMER_VENDOR_MAPPING On TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code where 1=1 "
        If chkVendorTypeVSP.IsChecked Then
            strQry += "and TSPL_VENDOR_MASTER.Form_Type='VSP' "
        ElseIf chkVendorTypePTM.IsChecked Then
            strQry += "and TSPL_VENDOR_MASTER.Form_Type='PTM' "
        ElseIf chkVendorTypeTTM.IsChecked Then
            strQry += "and TSPL_VENDOR_MASTER.Form_Type='TTM' "
        Else
            strQry += "and TSPL_VENDOR_MASTER.Form_Type in ('VSP','PTM','TTM')"
        End If
        If TxtCustomer.arrValueMember IsNot Nothing AndAlso TxtCustomer.arrValueMember.Count > 0 Then
            strQry += " and TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code in (" + clsCommon.GetMulcallString(TxtCustomer.arrValueMember) + ") and TSPL_VENDOR_MASTER.Status='N' "
        End If
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorSelector@VendorLedger", strQry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)

    End Sub


    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name  from"
        'strQry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
        strQry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + StrPermission + "))xxx"
        strQry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'"
        strQry += " order by xxx.Loc_Segment_Code"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocationSelector@VendorLedge6", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtAccountSet__My_Click(sender As Object, e As EventArgs) Handles txtAccountSet._My_Click
        strQry = "select Acct_Set_Code as Code, Acct_Set_Desc as Description from TSPL_VENDOR_ACCOUNT_SET"
        txtAccountSet.arrValueMember = clsCommon.ShowMultipleSelectForm("AcSetSelector@VendorLedger", strQry, "Code", "Description", txtAccountSet.arrValueMember, txtAccountSet.arrDispalyMember)
    End Sub

    Private Sub LoadCurrencyType()
        dtOpening = New DataTable()
        dtOpening.Columns.Add("Code", GetType(String))
        dtOpening.Columns.Add("Name", GetType(String))
        dtOpening.Rows.Add("ConvRate", "Functional Currency")
        dtOpening.Rows.Add("1", "Customer Currency")
        ddlCurrencyType.DataSource = dtOpening
        ddlCurrencyType.ValueMember = "Code"
        ddlCurrencyType.DisplayMember = "Name"
    End Sub

    Private Sub FunExport(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & FormType & "'"))
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember))
            End If
            If TxtCustomer.arrValueMember IsNot Nothing AndAlso TxtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(TxtCustomer.arrDispalyMember))
            End If
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
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
                clsCommon.MyExportToPDF("Vendor Ledger Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub TxtCustomer__My_Click(sender As Object, e As EventArgs) Handles TxtCustomer._My_Click
        'strQry = "Select TSPL_CUSTOMER_MASTER.Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as Name,TSPL_VENDOR_MASTER.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name] from TSPL_CUSTOMER_VENDOR_MAPPING left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code =TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code  where Isnull(TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,'')<>''"
        strQry = "Select TSPL_CUSTOMER_MASTER.Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as Name,TSPL_VENDOR_MASTER.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name] from TSPL_CUSTOMER_VENDOR_MAPPING left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code =TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_CUSTOMER_GROUP_MASTER  on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code  =TSPL_CUSTOMER_MASTER.Cust_Group_Code Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code  where Isnull(TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,'')<>'' AND TSPL_CUSTOMER_GROUP_MASTER.ShowGroupOnReport =1"
        If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
            strQry += " AND TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")"
        End If
        TxtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("Customer1Selector@CusVendorLedger", strQry, "Code", "Name", TxtCustomer.arrValueMember, TxtCustomer.arrDispalyMember)

        strQry = "Select TSPL_VENDOR_MASTER.Vendor_Code as Code from TSPL_VENDOR_MASTER left outer join TSPL_CUSTOMER_VENDOR_MAPPING On TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code  =TSPL_VENDOR_MASTER.Vendor_Code where TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code in (" + clsCommon.GetMulcallString(TxtCustomer.arrValueMember) + ") "
        Dim arr As ArrayList = Nothing
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList
            For Each dr As DataRow In dt.Rows
                arr.Add(dr("Code"))
            Next
        End If
        txtVendor.arrValueMember = arr
    End Sub


    Private Sub txtCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomerGroup._My_Click
        strQry = "Select Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER where ShowGroupOnReport=1"
        txtCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomerGroup.arrValueMember, txtCustomerGroup.arrDispalyMember)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        FunExport(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        FunExport(EnumExportTo.PDF)
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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            gv.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
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
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class










