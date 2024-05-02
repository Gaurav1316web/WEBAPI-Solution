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
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
'KDI/20/02/18-000038 by balwinder on 04/05/2018,KDI/06/06/18-000343 by richa 7 June,2018,KDI/06/06/18-000343 12 june(richa)
Public Class rptVendReco
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
        'If clsCommon.myLen(MIS_Item_Group) <= 0 Then
        '    clsCommon.MyMessageBoxShow("MIS Item Group Custom field is not create in Item Structure.")
        'End If
        Try
            ERPStartDate = clsCommon.myCDate(objCommonVar.ERPStartDate)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow("Invalid ERP Start Date")
            Me.Close()
        End Try
        LoadReportTypes()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        Document_No = ""
        txtLocation.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtLocation.Enabled = True
        txtTransaction.Enabled = True
        txtCustomer.Enabled = True
        Gv1.DataSource = Nothing
        cboType.SelectedValue = "Account Wise"
        RadPageViewPage2.Text = clsCommon.myCstr(cboType.SelectedValue)
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True

        If isDataLoad Then
            fromDate.Value = dtFrom
            ToDate.Value = dtTo
            txtLocation.arrValueMember = arrLocation
            txtCustomer.arrValueMember = arrCustomer
            txtTransaction.arrValueMember = arrTransaction
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
        dt.Rows.Add("Vendor And Account Wise")
        dt.Rows.Add("Detail")
        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"
    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.rptVendorReco)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Sub Print(ByVal IsPrint As Exporter, Optional ByVal BulkExport As Integer = 0)
        Try


            Dim arrHeader As List(Of String) = New List(Of String)()
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
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid(" Purchase Register:" + cboType.SelectedValue, Gv1, arrHeader, Me.Text)
                Exit Sub
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Purchase Register" + cboType.SelectedValue, Gv1, arrHeader, "Purchase Register", True)
                Exit Sub
            End If
            clsCommon.ProgressBarShow()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            If clsCommon.GetDateWithStartTime(fromDate.Value) < clsCommon.GetDateWithStartTime(ERPStartDate) Then
                Throw New Exception("From Date [" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "] can't be less than ERP Start Date [" + clsCommon.GetPrintDate(ERPStartDate, "dd/MM/yyyy") + "]")
            End If
            If clsCommon.GetDateWithStartTime(fromDate.Value) > clsCommon.GetDateWithStartTime(ToDate.Value) Then
                Throw New Exception("From Date [" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "] can't be More than To Date [" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "]")
            End If
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = ""
            Dim strMain As String = Nothing
            Dim obj As New clsPurchaseReco
            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                obj.Transaction = txtTransaction.arrValueMember
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                obj.Location = txtLocation.arrValueMember
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                obj.Vendor_Code = txtCustomer.arrValueMember
            End If
            If txtMultAccountNo.arrValueMember IsNot Nothing AndAlso txtMultAccountNo.arrValueMember.Count > 0 Then
                obj.Acc_Code = txtMultAccountNo.arrValueMember
            End If
            obj.IncludeAllDoc = True
            obj.From_Date = fromDate.Value
            obj.To_Date = ToDate.Value
            obj.Account_Set = fndMultiAccSet.arrValueMember
            obj.Vendor_Group = fndMultiVendorGroup.arrValueMember
            obj.ShowMismatchDoc = chkMismatchDoc.Checked
            obj.IncludeApplyDocumentPayment = chkIncludeApplyDocument.Checked
            strRunQuery = "Select *,2 as RI from (" + clsPurchaseInvoiceHead.GetVendorRecoQry(obj) + ")aaa "
            Dim strOPTransQry As String = ""
            Dim strOPSystemQry As String = ""
            If chkOPAndClosing.Checked Then
                If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Vendor And Account Wise") = CompairStringResult.Equal Then
                    obj.From_Date = ERPStartDate
                    obj.To_Date = fromDate.Value.AddDays(-1)
                    strOPTransQry = "Select *,1 as RI from (" + clsPurchaseInvoiceHead.GetVendorRecoQry(obj) + ")bbb "

                    obj.To_Date = ERPStartDate
                    strOPSystemQry = "Select *,1 as RI from (" + clsPurchaseInvoiceHead.GetVendorRecoSytemOPQry(obj) + ")ccc "
                    strRunQuery += Environment.NewLine + Environment.NewLine +
                        "Union All" + Environment.NewLine + Environment.NewLine + strOPTransQry + Environment.NewLine + Environment.NewLine +
                        "Union All" + Environment.NewLine + Environment.NewLine + strOPSystemQry + Environment.NewLine + Environment.NewLine
                End If
            End If


            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
                ''RICHA TEC/02/11/18-000358 ON 18 Feb,2019
                'strRunQuery = "select xxxxx.*,isnull( JEOther.OpemingDRAmount,0)  as OpeningDRAmount,isnull( JEOther.OpemingCRAmount ,0)  as OpeningCRAmount,isnull( JEOther.DRAmount,0)  as OtherDRAmount, isnull( JEOther.CRAmount,0)  as OtherCRAmount,(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0)) as OtherNetAmount,(GLNetAmount+(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0))) as TotalTrialNet,(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0))+GLCLBal as TotalTrialCL, (isnull( xxxxx.NetAmount,0)-(isnull(xxxxx.GLNetAmount ,0)+(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0)))) as VendorNetTotalDiff from (" + Environment.NewLine + _
                '"select Account_code,max(Account_Desc) as Account_Desc" + Environment.NewLine + _
                '",sum(case when RI=1 then  DRAmount-CRAmount else 0 end) as OPBal" + Environment.NewLine + _
                '",sum(case when RI=2 then  DRAmount else 0 end) as DRAmount" + Environment.NewLine + _
                '",sum(case when RI=2  then CRAmount else 0 end ) as CRAmount" + Environment.NewLine + _
                '",sum(case when RI=2 then NetAmount else 0 end ) as NetAmount" + Environment.NewLine + _
                '",sum(case when RI=2 then NetAmount when RI=1 then  DRAmount-CRAmount else 0 end) as CLBal" + Environment.NewLine + _
                '",sum(case when RI=1 then GLDRAmount-GLCRAmount else 0 end ) as GLOPBal" + Environment.NewLine + _
                '",sum(case when RI=2 then GLDRAmount else 0 end ) as GLDRAmount" + Environment.NewLine + _
                '",sum(case when RI=2 then GLCRAmount else 0 end ) as GLCRAmount" + Environment.NewLine + _
                '",sum(case when RI=2 then GLNetAmount else 0 end ) as GLNetAmount" + Environment.NewLine + _
                '",sum(case when RI=2 then GLNetAmount  when RI=1 then GLDRAmount-GLCRAmount else 0 end ) as GLCLBal" + Environment.NewLine + _
                '",sum(case when RI=2 then DiffAmount else 0 end ) as DiffAmountNet " + Environment.NewLine + _
                '",(sum(case when RI=2 then NetAmount when RI=1 then  DRAmount-CRAmount else 0 end)-sum(case when RI=2 then GLNetAmount  when RI=1 then GLDRAmount-GLCRAmount else 0 end)) as DiffAmountCL " + Environment.NewLine + _
                '"from (" + strRunQuery + ")Final group by Account_code" + Environment.NewLine + _
                ' ")xxxxx  " + Environment.NewLine + _
                ' "left outer join (select TSPL_JOURNAL_DETAILS.Account_code,sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount>0  then 1 else 0 end   ) as DRAmount ," + Environment.NewLine + _
                '"sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 else 0 end  ) as CRAmount " + Environment.NewLine + _
                '" from TSPL_JOURNAL_DETAILS " + Environment.NewLine + _
                '" left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " + Environment.NewLine + _
                '" where (isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'')<>'V' "
                'If Not obj.IncludeApplyDocumentPayment Then
                '    strRunQuery += " or exists(select 1 from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_No=TSPL_JOURNAL_MASTER.Source_Doc_No and Payment_Type='AD') "
                'End If

                'strRunQuery += ") and TSPL_JOURNAL_MASTER.Authorized='A' "
                'If Not chkOPAndClosing.Checked Then
                '    strRunQuery += " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) >= '" + clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") + " ' "
                'End If
                'strRunQuery += " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) <= '" + clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") + " ' group by TSPL_JOURNAL_DETAILS.Account_code ) JEOther on  JEOther.Account_code=xxxxx.Account_code"


                strRunQuery = "select xxxxx.*,isnull( JEOther.OpeningDRAmount,0)-isnull( JEOther.OpeningCRAmount ,0)  as OtherOpeningAmount,isnull( JEOther.DRAmount,0)  as OtherDRAmount," + Environment.NewLine + _
                    " isnull( JEOther.CRAmount,0)  as OtherCRAmount,(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0)) as OtherNetAmount," + Environment.NewLine + _
                    " ((isnull( JEOther.OpeningDRAmount,0)-isnull( JEOther.OpeningCRAmount ,0) )+(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0))) as TotalTrialNet," + Environment.NewLine + _
                    " ((isnull( JEOther.OpeningDRAmount,0)-isnull( JEOther.OpeningCRAmount ,0) )+(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0)))+GLCLBal as TotalTrialCL," + Environment.NewLine + _
                    " ( xxxxx.CLBal -(((isnull( JEOther.OpeningDRAmount,0)-isnull( JEOther.OpeningCRAmount ,0) )+(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0)))+GLCLBal) ) as VendorNetTotalDiff from (" + Environment.NewLine + _
                "select Account_code,max(Account_Desc) as Account_Desc" + Environment.NewLine + _
                ",sum(case when RI=1 then  DRAmount-CRAmount else 0 end) as OPBal" + Environment.NewLine + _
                ",sum(case when RI=2 then  DRAmount else 0 end) as DRAmount" + Environment.NewLine + _
                ",sum(case when RI=2  then CRAmount else 0 end ) as CRAmount" + Environment.NewLine + _
                ",sum(case when RI=2 then NetAmount else 0 end ) as NetAmount" + Environment.NewLine + _
                ",sum(case when RI=2 then NetAmount when RI=1 then  DRAmount-CRAmount else 0 end) as CLBal" + Environment.NewLine + _
                ",sum(case when RI=1 then GLDRAmount-GLCRAmount else 0 end ) as GLOPBal" + Environment.NewLine + _
                ",sum(case when RI=2 then GLDRAmount else 0 end ) as GLDRAmount" + Environment.NewLine + _
                ",sum(case when RI=2 then GLCRAmount else 0 end ) as GLCRAmount" + Environment.NewLine + _
                ",sum(case when RI=2 then GLNetAmount else 0 end ) as GLNetAmount" + Environment.NewLine + _
                ",sum(case when RI=2 then GLNetAmount  when RI=1 then GLDRAmount-GLCRAmount else 0 end ) as GLCLBal" + Environment.NewLine + _
                ",sum(case when RI=2 then DiffAmount else 0 end ) as DiffAmountNet " + Environment.NewLine + _
                ",(sum(case when RI=2 then NetAmount when RI=1 then  DRAmount-CRAmount else 0 end)-sum(case when RI=2 then GLNetAmount  when RI=1 then GLDRAmount-GLCRAmount else 0 end)) as DiffAmountCL " + Environment.NewLine + _
                "from (" + strRunQuery + ")Final group by Account_code" + Environment.NewLine + _
                ")xxxxx  " + Environment.NewLine + _
                "left outer join (select z.Account_code,sum(z.DRAmount) as DRAmount,sum(z.CRAmount) as CRAmount,sum(z.OpeningDRAmount) as OpeningDRAmount,sum(z.OpeningCRAmount) as OpeningCRAmount from (select TSPL_JOURNAL_DETAILS.Account_code,sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount>0  then 1 else 0 end   ) as DRAmount ," + Environment.NewLine + _
                "sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 else 0 end  ) as CRAmount ,0 as OpeningDRAmount,0 as OpeningCRAmount" + Environment.NewLine + _
                " from TSPL_JOURNAL_DETAILS " + Environment.NewLine + _
                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " + Environment.NewLine + _
                " where (isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'')<>'V' "
                If Not obj.IncludeApplyDocumentPayment Then
                    strRunQuery += " or exists(select 1 from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_No=TSPL_JOURNAL_MASTER.Source_Doc_No and Payment_Type='AD') "
                End If
                strRunQuery += ") and TSPL_JOURNAL_MASTER.Authorized='A' " + Environment.NewLine + _
                    " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) >= '" + clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") + " ' " + Environment.NewLine + _
                    " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + " ' group by TSPL_JOURNAL_DETAILS.Account_code" + Environment.NewLine
                If chkOPAndClosing.Checked = True Then
                    strRunQuery += " Union All " & Environment.NewLine & _
                    "select TSPL_JOURNAL_DETAILS.Account_code,0 as DRAmount,0 as CRAmount,sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount>0  then 1 else 0 end   ) as OpeningDRAmount ," + Environment.NewLine + _
                    "sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 else 0 end  ) as OpeningCRAmount " + Environment.NewLine + _
                    " from TSPL_JOURNAL_DETAILS " + Environment.NewLine + _
                    " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " + Environment.NewLine + _
                    " where (isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'')<>'V' "

                    If Not obj.IncludeApplyDocumentPayment Then
                        strRunQuery += " or exists(select 1 from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_No=TSPL_JOURNAL_MASTER.Source_Doc_No and Payment_Type='AD') "
                    End If

                    strRunQuery += ") and TSPL_JOURNAL_MASTER.Authorized='A' " + Environment.NewLine + _
                    " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) < '" + clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") + " ' " + Environment.NewLine + _
                    " group by TSPL_JOURNAL_DETAILS.Account_code"
                End If
                strRunQuery += ")z group by z.Account_code) JEOther on  JEOther.Account_code=xxxxx.Account_code"




                '                strRunQuery = "select JEOther.Account_code,JEOther.Account_Desc  , ISNULL(xxxxx.OPBal,0) AS OPBal,ISNULL(xxxxx.DRAmount,0) AS DRAmount,ISNULL(xxxxx.CRAmount,0) AS CRAmount,ISNULL(xxxxx.NetAmount,0) AS NetAmount,ISNULL(xxxxx.CLBal,0) AS CLBal,ISNULL(xxxxx.GLOPBal,0) AS GLOPBal,ISNULL(xxxxx.GLDRAmount,0) AS GLDRAmount,ISNULL(xxxxx.GLCRAmount,0) AS GLCRAmount," + Environment.NewLine + _
                '" ISNULL(xxxxx.GLNetAmount,0) AS GLNetAmount,ISNULL(xxxxx.GLCLBal,0) AS GLCLBal,ISNULL(xxxxx.DiffAmountNet,0) AS DiffAmountNet,ISNULL(xxxxx.DiffAmountCL,0) AS DiffAmountCL," + Environment.NewLine + _
                '" isnull( JEOther.DRAmount,0)  as OtherDRAmount, isnull( JEOther.CRAmount,0)  as OtherCRAmount,(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0)) as OtherNetAmount,(GLNetAmount+(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0))) as TotalTrialNet,(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0))+GLCLBal as TotalTrialCL, (isnull( xxxxx.NetAmount,0)-(isnull(xxxxx.GLNetAmount ,0)+(isnull( JEOther.DRAmount,0)-isnull(JEOther.CRAmount,0)))) as VendorNetTotalDiff from (" + Environment.NewLine + _
                '                "select Account_code,max(Account_Desc) as Account_Desc" + Environment.NewLine + _
                '                ",sum(case when RI=1 then  DRAmount-CRAmount else 0 end) as OPBal" + Environment.NewLine + _
                '                ",sum(case when RI=2 then  DRAmount else 0 end) as DRAmount" + Environment.NewLine + _
                '                ",sum(case when RI=2  then CRAmount else 0 end ) as CRAmount" + Environment.NewLine + _
                '                ",sum(case when RI=2 then NetAmount else 0 end ) as NetAmount" + Environment.NewLine + _
                '                ",sum(case when RI=2 then NetAmount when RI=1 then  DRAmount-CRAmount else 0 end) as CLBal" + Environment.NewLine + _
                '                ",sum(case when RI=1 then GLDRAmount-GLCRAmount else 0 end ) as GLOPBal" + Environment.NewLine + _
                '                ",sum(case when RI=2 then GLDRAmount else 0 end ) as GLDRAmount" + Environment.NewLine + _
                '                ",sum(case when RI=2 then GLCRAmount else 0 end ) as GLCRAmount" + Environment.NewLine + _
                '                ",sum(case when RI=2 then GLNetAmount else 0 end ) as GLNetAmount" + Environment.NewLine + _
                '                ",sum(case when RI=2 then GLNetAmount  when RI=1 then GLDRAmount-GLCRAmount else 0 end ) as GLCLBal" + Environment.NewLine + _
                '                ",sum(case when RI=2 then DiffAmount else 0 end ) as DiffAmountNet " + Environment.NewLine + _
                '                ",(sum(case when RI=2 then NetAmount when RI=1 then  DRAmount-CRAmount else 0 end)-sum(case when RI=2 then GLNetAmount  when RI=1 then GLDRAmount-GLCRAmount else 0 end)) as DiffAmountCL " + Environment.NewLine + _
                '                "from (" + strRunQuery + ")Final group by Account_code" + Environment.NewLine + _
                '                 ")xxxxx  " + Environment.NewLine + _
                '                 "RIGHT outer join (select TSPL_JOURNAL_DETAILS.Account_code,max(TSPL_JOURNAL_DETAILS.Account_Desc) AS Account_Desc,sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount>0  then 1 else 0 end   ) as DRAmount ," + Environment.NewLine + _
                '                "sum(TSPL_JOURNAL_DETAILS.Amount * case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 else 0 end  ) as CRAmount " + Environment.NewLine + _
                '                " from TSPL_JOURNAL_DETAILS " + Environment.NewLine + _
                '                " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " + Environment.NewLine + _
                '                " where (isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'')<>'V' "
                '                If Not obj.IncludeApplyDocumentPayment Then
                '                    strRunQuery += " or exists(select 1 from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_No=TSPL_JOURNAL_MASTER.Source_Doc_No and Payment_Type='AD') "
                '                End If

                '                strRunQuery += ") and TSPL_JOURNAL_MASTER.Authorized='A' "
                '                If Not chkOPAndClosing.Checked Then
                '                    strRunQuery += " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) >= '" + clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") + " ' "
                '                End If
                '                strRunQuery += " and CAST(TSPL_JOURNAL_MASTER.Voucher_Date AS DATE) <'" + clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") + " ' group by TSPL_JOURNAL_DETAILS.Account_code ) JEOther on  JEOther.Account_code=xxxxx.Account_code"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Vendor And Account Wise") = CompairStringResult.Equal Then
                strRunQuery = "select  max(Vendor_Group_Code) as Vendor_Group_Code,max(Group_Desc) as Group_Desc,max(Vendor_Account) as Vendor_Account,Vendor_Code,max(Vendor_Name) as Vendor_Name, Account_code,max(Account_Desc) as Account_Desc" + Environment.NewLine + _
                    ",sum(case when RI=1 then  DRAmount-CRAmount else 0 end) as OPBal" + Environment.NewLine + _
                    ",sum(case when RI=2 then  DRAmount else 0 end) as DRAmount" + Environment.NewLine + _
                    ",sum(case when RI=2  then CRAmount else 0 end ) as CRAmount" + Environment.NewLine + _
                    ",sum(case when RI=2 then NetAmount else 0 end ) as NetAmount" + Environment.NewLine + _
                    ",sum(case when RI=2 then NetAmount when RI=1 then  DRAmount-CRAmount else 0 end) as CLBal" + Environment.NewLine + _
                    ",sum(case when RI=1 then GLDRAmount-GLCRAmount else 0 end ) as GLOPBal" + Environment.NewLine + _
                    ",sum(case when RI=2 then GLDRAmount else 0 end ) as GLDRAmount" + Environment.NewLine + _
                    ",sum(case when RI=2 then GLCRAmount else 0 end ) as GLCRAmount" + Environment.NewLine + _
                    ",sum(case when RI=2 then GLNetAmount else 0 end ) as GLNetAmount" + Environment.NewLine + _
                    ",sum(case when RI=2 then GLNetAmount  when RI=1 then GLDRAmount-GLCRAmount else 0 end ) as GLCLBal" + Environment.NewLine + _
                    ",sum(case when RI=2 then DiffAmount else 0 end ) as DiffAmountNet " + Environment.NewLine + _
                    ",(sum(case when RI=2 then NetAmount when RI=1 then  DRAmount-CRAmount else 0 end)-sum(case when RI=2 then GLNetAmount  when RI=1 then GLDRAmount-GLCRAmount else 0 end)) as DiffAmountCL " + Environment.NewLine + _
                    "from (" + strRunQuery + ")Final group by Vendor_Code,Account_code"
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
                Throw New Exception("No Data Found to Display")
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
            " where (isnull(TSPL_JOURNAL_DETAILS.Reco_Control_Account,'')<>'V' "
            If Not chkIncludeApplyDocument.Checked Then
                strRunQuery += " or exists(select 1 from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Payment_No=TSPL_JOURNAL_MASTER.Source_Doc_No and Payment_Type='AD') "
            End If
            strRunQuery += ") and TSPL_JOURNAL_MASTER.Authorized='A' "

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
            RadPageViewPage3.Tag = cboType.SelectedValue
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
    Sub EnableDisableAllControl(ByVal val As Boolean)
        txtTransaction.Enabled = val
        txtLocation.Enabled = val
        txtCustomer.Enabled = val
        txtMultAccountNo.Enabled = val
        fndMultiAccSet.Enabled = val
        fndMultiVendorGroup.Enabled = val
        cboType.Enabled = val
        fromDate.Enabled = val
        ToDate.Enabled = val
        chkMismatchDoc.Enabled = val
        chkIncludeApplyDocument.Enabled = val
        chkOPAndClosing.Enabled = val
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

            Gv1.Columns("OtherOpeningAmount").IsVisible = True
            Gv1.Columns("OtherOpeningAmount").Width = 120
            Gv1.Columns("OtherOpeningAmount").HeaderText = " Other Opening Amt"

            Gv1.Columns("OtherDRAmount").IsVisible = True
            Gv1.Columns("OtherDRAmount").Width = 120
            Gv1.Columns("OtherDRAmount").HeaderText = "Other Debit Amt"

            Gv1.Columns("OtherCRAmount").IsVisible = True
            Gv1.Columns("OtherCRAmount").Width = 120
            Gv1.Columns("OtherCRAmount").HeaderText = "Other Credit Amt"

            Gv1.Columns("OtherNetAmount").IsVisible = True
            Gv1.Columns("OtherNetAmount").Width = 120
            Gv1.Columns("OtherNetAmount").HeaderText = "Other Net Amt"


            Gv1.Columns("DiffAmountNet").IsVisible = True
            Gv1.Columns("DiffAmountNet").Width = 120
            Gv1.Columns("DiffAmountNet").HeaderText = "Diff Amount Net"

            Gv1.Columns("DiffAmountCL").IsVisible = chkOPAndClosing.Checked
            Gv1.Columns("DiffAmountCL").Width = 120
            Gv1.Columns("DiffAmountCL").HeaderText = "Diff Amount Closing"

            Gv1.Columns("TotalTrialNet").IsVisible = True
            Gv1.Columns("TotalTrialNet").Width = 120
            Gv1.Columns("TotalTrialNet").HeaderText = "Total Trial Net"

            Gv1.Columns("TotalTrialCL").IsVisible = chkOPAndClosing.Checked
            Gv1.Columns("TotalTrialCL").Width = 120
            Gv1.Columns("TotalTrialCL").HeaderText = "Total Trial Closing"

            Gv1.Columns("VendorNetTotalDiff").IsVisible = True
            Gv1.Columns("VendorNetTotalDiff").Width = 120
            Gv1.Columns("VendorNetTotalDiff").HeaderText = "Vendor Net Total Diff"

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
            item = New GridViewSummaryItem("OtherOpeningAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("OtherDRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("OtherCRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("OtherNetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("DiffAmountNet", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("DiffAmountCL", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("TotalTrialNet", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("TotalTrialCL", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("VendorNetTotalDiff", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("OPBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("CLBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLOPBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLCLBal", "{0:F2}", GridAggregateFunction.Sum)

            summaryRowItem.Add(item)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Vendor And Account Wise") = CompairStringResult.Equal Then
            Gv1.Columns("Vendor_Group_Code").IsVisible = True
            Gv1.Columns("Vendor_Group_Code").Width = 120
            Gv1.Columns("Vendor_Group_Code").HeaderText = "Vendor Group Code"

            Gv1.Columns("Group_Desc").IsVisible = True
            Gv1.Columns("Group_Desc").Width = 120
            Gv1.Columns("Group_Desc").HeaderText = "Vendor Group"

            Gv1.Columns("Vendor_Account").IsVisible = True
            Gv1.Columns("Vendor_Account").Width = 120
            Gv1.Columns("Vendor_Account").HeaderText = "Vendor Accountset"

            Gv1.Columns("Vendor_Code").IsVisible = True
            Gv1.Columns("Vendor_Code").Width = 120
            Gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

            Gv1.Columns("Vendor_Name").IsVisible = True
            Gv1.Columns("Vendor_Name").Width = 120
            Gv1.Columns("Vendor_Name").HeaderText = "Vendor"

            Gv1.Columns("Account_code").IsVisible = True
            Gv1.Columns("Account_code").Width = 120
            Gv1.Columns("Account_code").HeaderText = "Account code"

            Gv1.Columns("Account_Desc").IsVisible = True
            Gv1.Columns("Account_Desc").Width = 120
            Gv1.Columns("Account_Desc").HeaderText = "Account"

            Gv1.Columns("DRAmount").IsVisible = True
            Gv1.Columns("DRAmount").Width = 120
            Gv1.Columns("DRAmount").HeaderText = "Debit Amt"

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

            Gv1.Columns("DiffAmountNet").IsVisible = True
            Gv1.Columns("DiffAmountNet").Width = 120
            Gv1.Columns("DiffAmountNet").HeaderText = "Diff Amount Net"

            Gv1.Columns("DiffAmountCL").IsVisible = chkOPAndClosing.Checked
            Gv1.Columns("DiffAmountCL").Width = 120
            Gv1.Columns("DiffAmountCL").HeaderText = "Diff Amount Closing"

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
            item = New GridViewSummaryItem("DiffAmountNet", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("DiffAmountCL", "{0:F2}", GridAggregateFunction.Sum)
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
            Gv1.Columns("Vendor_Group_Code").IsVisible = True
            Gv1.Columns("Vendor_Group_Code").Width = 120
            Gv1.Columns("Vendor_Group_Code").HeaderText = "Vendor Group Code"

            Gv1.Columns("Group_Desc").IsVisible = True
            Gv1.Columns("Group_Desc").Width = 120
            Gv1.Columns("Group_Desc").HeaderText = "Vendor Group"

            Gv1.Columns("Vendor_Account").IsVisible = True
            Gv1.Columns("Vendor_Account").Width = 120
            Gv1.Columns("Vendor_Account").HeaderText = "Vendor Accountset"

            Gv1.Columns("Vendor_Code").IsVisible = True
            Gv1.Columns("Vendor_Code").Width = 120
            Gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

            Gv1.Columns("Vendor_Name").IsVisible = True
            Gv1.Columns("Vendor_Name").Width = 120
            Gv1.Columns("Vendor_Name").HeaderText = "Vendor"

            Gv1.Columns("Account_code").IsVisible = True
            Gv1.Columns("Account_code").Width = 120
            Gv1.Columns("Account_code").HeaderText = "Account code"

            Gv1.Columns("Account_Desc").IsVisible = True
            Gv1.Columns("Account_Desc").Width = 120
            Gv1.Columns("Account_Desc").HeaderText = "Account"

            Gv1.Columns("DRAmount").IsVisible = True
            Gv1.Columns("DRAmount").Width = 120
            Gv1.Columns("DRAmount").HeaderText = "Debit Amt"

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

            Gv1.Columns("DocNo").IsVisible = True
            Gv1.Columns("DocNo").Width = 120
            Gv1.Columns("DocNo").HeaderText = "Document No"

            Gv1.Columns("Document Date").IsVisible = True
            Gv1.Columns("Document Date").Width = 120
            Gv1.Columns("Document Date").HeaderText = "Document Date"

            Gv1.Columns("RefDocNo").IsVisible = True
            Gv1.Columns("RefDocNo").Width = 120
            Gv1.Columns("RefDocNo").HeaderText = "Reference Document No"


            Gv1.Columns("TransType").IsVisible = True
            Gv1.Columns("TransType").Width = 120
            Gv1.Columns("TransType").HeaderText = "Trans Type"

            Gv1.Columns("DocumentType").IsVisible = True
            Gv1.Columns("DocumentType").Width = 120
            Gv1.Columns("DocumentType").HeaderText = "Document Type"

            Gv1.Columns("Voucher_No").IsVisible = True
            Gv1.Columns("Voucher_No").Width = 120
            Gv1.Columns("Voucher_No").HeaderText = "Voucher No"

            Gv1.Columns("Voucher_Date").IsVisible = True
            Gv1.Columns("Voucher_Date").Width = 120
            Gv1.Columns("Voucher_Date").HeaderText = "Voucher Date"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item As New GridViewSummaryItem("DRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("CRAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("NetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLDrAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLCrAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("GLNetAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            item = New GridViewSummaryItem("DiffAmount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
        RadPageView1.SelectedPage = RadPageViewPage2

        Gv1.AllowAddNewRow = False
        Gv1.BestFitColumns()
        Gv1.ShowGroupPanel = False
    End Sub

    Sub Reset()
        Try
            EnableDisableAllControl(True)
            RadPageView1.SelectedPage = RadPageViewPage1
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
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
            arrHeader.Add("Name : Vendor Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If clsCommon.myLen(cboType.Text) > 0 Then
                arrHeader.Add("Report Type : " + cboType.Text)
            End If

            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 2007 (*.xlsx) |*.xlsx;|Excel 97-2003 (*.xls)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSetting_Click(sender As Object, e As EventArgs) Handles rmSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.RptFreshSaleRegister1
        frm.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Dim qry As String
        Try
            qry = "CREATE NONCLUSTERED INDEX [TSPL_JOURNAL_DETAILS_Reco_Control_Account]" + _
            "ON [dbo].[TSPL_JOURNAL_DETAILS] ([Reco_Control_Account]) " + _
            "INCLUDE ([Voucher_No],[Account_code],[Amount])"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
        End Try

        Try
            qry = "CREATE NONCLUSTERED INDEX [TSPL_JOURNAL_MASTER_Authorized_Voucher_Date]" + _
            "ON [dbo].[TSPL_JOURNAL_MASTER] ([Authorized],[Voucher_Date])" + _
            "INCLUDE ([Voucher_No],[Source_Doc_No])"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
        End Try

        Try
            qry = "CREATE NONCLUSTERED INDEX [TSPL_VENDOR_INVOICE_HEAD_Posting_Date]" + _
            "ON [dbo].[TSPL_VENDOR_INVOICE_HEAD] ([Posting_Date])" + _
            "INCLUDE ([Vendor_Code],[Document_No],[Document_Type],[Document_Total],[Total_Tax],[TDS_Actual_Amount],[GSTRegistered],[RCM])"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
        End Try
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
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_name as Name from TSPL_VENDOR_master  WHERE  Status='N'  "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("VenMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtCustomer, "Vendor_name", "TSPL_VENDOR_master", "Vendor_Code")
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER  where location_type IN  ('Physical','Virtual')   "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim FrmR As New FrmPendingRequisitionQty
        FrmR.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " Select xxx.Code,  xxx.Name From (" & _
                                 " Select distinct 'PI' As Code,    'Purchase Invoice' As Name from TSPL_PI_HEAD " & _
                                 " Union  Select distinct 'MCC' As Code,    'Milk Receipt' As Name from TSPL_MILK_RECEIPT_HEAD " & _
                                 " Union  Select distinct 'Bulk' As Code,    'Bulk Purchase' As Name from tspl_Bulk_milk_purchase_Invoice_head " & _
                                  " Union  Select distinct 'Bulk Purchase Return' As Code,    'Bulk Purchase Return' As Name from TSPL_BULK_MILK_PURCHASE_RETURN_HEAD " & _
                                 " Union  Select distinct 'MCC Transfer' As Code,    'MCC Transfer' As Name from TSPL_MILK_TRANSFER_IN " & _
                                 " Union  Select distinct 'Transfer' As Code,    'Transfer' As Name from TSPL_TRANSFER_ORDER_HEAD " & _
                                  " Union  Select distinct 'Transfer Return' As Code,    'Transfer Return' As Name from TSPL_TRANSFER_RETURN " & _
                                 " Union  Select distinct 'Return' As Code,    'Purchase Return' As Name from TSPL_PR_HEAD " & _
                                 " union Select distinct 'MT' As Code,    'Merchant Trade' As Name from TSPL_PI_HEAD " & _
                                 " ) xxx"
        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulPur", qry, "Name", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
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
        Dim qry As String = " select Acct_Set_Code as [Code],Acct_Set_Desc as Name,Payable_Account as [Payable Account],Discount_Account as [Discount Account],Advance_Account as [Advance Account],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],CURRENCY_CODE as [Currency Code],EXCHANGE_LOSS_ACCOUNT as [Exchange Loss Account],EXCHANGE_GAIN_ACCOUNT as [Exchange Gain Account] from TSPL_VENDOR_ACCOUNT_SET "
        fndMultiAccSet.arrValueMember = clsCommon.ShowMultipleSelectForm("VenAccMulSel", qry, "Code", "Name", fndMultiAccSet.arrValueMember, fndMultiAccSet.arrDispalyMember)
    End Sub

    Private Sub fndMultiVendorGroup__My_Click(sender As Object, e As EventArgs) Handles fndMultiVendorGroup._My_Click
        Dim qry As String = " select Ven_Group_Code as Code,Group_Desc as Name from TSPL_VENDOR_GROUP  "
        fndMultiVendorGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VenGrpMulSel", qry, "Code", "Name", fndMultiVendorGroup.arrValueMember, fndMultiVendorGroup.arrDispalyMember)
    End Sub

    Sub DrillDown()
        Try
            Gv2.DataSource = Nothing
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Account Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Account Wise") Then
                    arrBack.Add("Account Wise")
                End If
                cboType.SelectedValue = "Vendor And Account Wise"
                arrGLAccount = New ArrayList()
                arrGLAccount = txtMultAccountNo.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Account_code").Value))
                txtMultAccountNo.arrValueMember = tmp
                If Gv1.CurrentColumn Is Gv1.Columns("DiffAmountNet") AndAlso clsCommon.myCdbl(Gv1.CurrentRow.Cells("DiffAmountNet").Value) <> 0 Then
                    boolChecked = chkMismatchDoc.Checked
                    chkMismatchDoc.Checked = True
                    chkMismatchDoc.Tag = "D"
                Else
                    chkMismatchDoc.Tag = Nothing
                End If

                Print(Exporter.Refresh)
                OtherDataPrint(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Vendor And Account Wise") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Vendor And Account Wise") Then
                    arrBack.Add("Vendor And Account Wise")
                End If
                cboType.SelectedValue = "Detail"
                arrCustomer = New ArrayList()
                arrCustomer = txtCustomer.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(Gv1.CurrentRow.Cells("Vendor_Code").Value))
                txtCustomer.arrValueMember = tmp
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
                ''Reached at last Node
                'System Memory 24/04/2018
                If Gv1.CurrentRow.Index >= 0 Then
                    'If Gv1.CurrentColumn Is Gv1.Columns("Voucher_No") AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells("Voucher_No").Value) > 0 Then
                    '    'MDI.ShowForm(clsUserMgtCode.journalEntry, "", True, clsCommon.myCstr(Gv1.CurrentRow.Cells("Voucher_No").Value), True)
                    'ElseIf clsCommon.myLen(Gv1.CurrentRow.Cells("DocNo").Value) > 0 Then
                    '    If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "Payment") = CompairStringResult.Equal Then
                    '        '  MDI.ShowForm(clsUserMgtCode.PaymentEntryNew, "", True, clsCommon.myCstr(Gv1.CurrentRow.Cells("DocNo").Value), True)
                    '    ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "Bank Reverse") = CompairStringResult.Equal Then
                    '        ' MDI.ShowForm(clsUserMgtCode.FrmBankReverse, "", True, clsCommon.myCstr(Gv1.CurrentRow.Cells("DocNo").Value), True)
                    '    ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "AP Adjustment") = CompairStringResult.Equal Then
                    '        'MDI.ShowForm(clsUserMgtCode.PaymentAdjustmentEntry, "", True, clsCommon.myCstr(Gv1.CurrentRow.Cells("DocNo").Value), True)
                    '    Else
                    '        'MDI.ShowForm(clsUserMgtCode.mbtnAPInvoiceEntry, "", True, clsCommon.myCstr(Gv1.CurrentRow.Cells("DocNo").Value), True)
                    '    End If
                    'End If

                    If Gv1.CurrentColumn Is Gv1.Columns("Voucher_No") AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells("Voucher_No").Value) > 0 Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("Voucher_No").Value))
                    ElseIf clsCommon.myLen(Gv1.CurrentRow.Cells("DocNo").Value) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "Payment") = CompairStringResult.Equal Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, clsCommon.myCstr(Gv1.CurrentRow.Cells("DocNo").Value))
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "Bank Reverse") = CompairStringResult.Equal Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.reverseTransaction, clsCommon.myCstr(Gv1.CurrentRow.Cells("DocNo").Value))
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("TransType").Value), "AP Adjustment") = CompairStringResult.Equal Then
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentAdjustmentEntry, clsCommon.myCstr(Gv1.CurrentRow.Cells("DocNo").Value))
                        Else
                            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, clsCommon.myCstr(Gv1.CurrentRow.Cells("DocNo").Value))
                        End If
                    End If
                End If
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
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Vendor And Account Wise") = CompairStringResult.Equal AndAlso arrBack.Contains("Account Wise") Then
                arrBack.Remove("Account Wise")
                cboType.SelectedValue = "Account Wise"
                txtMultAccountNo.arrValueMember = arrGLAccount
                If clsCommon.CompairString(clsCommon.myCstr(chkMismatchDoc.Tag), "D") = CompairStringResult.Equal Then
                    chkMismatchDoc.Checked = boolChecked
                    chkMismatchDoc.Tag = Nothing
                End If
                Print(Exporter.Refresh)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Vendor And Account Wise") Then
                arrBack.Remove("Vendor And Account Wise")
                cboType.SelectedValue = "Vendor And Account Wise"
                txtCustomer.arrValueMember = arrCustomer
                Print(Exporter.Refresh)
            End If
            PageSetupReport_ID = clsERPFuncationality.GetReportID(MyBase.Form_ID, cboType.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cboType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboType.SelectedValueChanged
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            chkOPAndClosing.Checked = False
            chkOPAndClosing.Visible = False
        Else
            chkOPAndClosing.Visible = True
        End If
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Vendor Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            If clsCommon.myLen(cboType.Text) > 0 Then
                arrHeader.Add("Report Type : " + cboType.Text)
            End If

            If Not IsNothing(txtLocation.arrValueMember) Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If Not IsNothing(txtCustomer.arrValueMember) Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Vendor Reco", Gv1, arrHeader, "Vendor Reco", PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class


