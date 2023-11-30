'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
''updation by richa agarwal BM00000006641 show tds percentage in report 
'' updation by richa agarwal BM00000006828 do drilldown working
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class FrmrptTDSLedger
    Inherits FrmMainTranScreen
    Const ReportID As String = "TDS"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmrptTDSLedger)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Private Sub FrmrptTDSLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = dtpFromDate.Value
        LoadLocation()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Public Sub LoadLocation()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
        chkLocAll.IsChecked = True
    End Sub

    Private Sub fndSection__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSection._MYValidating
        Dim qry As String = "select tds_group as Code ,description as [Description] from TSPL_TDS_SECTION_MASTER"
        fndSection.Value = clsCommon.ShowSelectForm("rptTDSLedgerSection", qry, "Code", "", fndSection.Value, "Code", isButtonClicked)
        txtSection.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_TDS_SECTION_MASTER where tds_group ='" + fndSection.Value + "'"))
    End Sub

    Private Sub fndNatureofDeduction__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndNatureofDeduction._MYValidating
        Dim qry As String = "select deduction_code As Code,description  as [Description]from TSPL_TDS_DEDUCTION_HEAD"
        fndNatureofDeduction.Value = clsCommon.ShowSelectForm("rptTDSLedgerSection", qry, "Code", "TDS_Section ='" & fndSection.Value & "'", fndNatureofDeduction.Value, "Code", isButtonClicked)
        txtNatureofDeduction.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_TDS_DEDUCTION_HEAD where deduction_code ='" + fndNatureofDeduction.Value + "'"))
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferesh.Click
        PageSetupReport_ID = ReportID
        TemplateGridview = gvReport
        LoadData()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvReport.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvReport.Columns.Count - 1 Step ii + 1
                        gvReport.Columns(ii).IsVisible = False
                        gvReport.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gvReport.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If


            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub LoadData()
        gvReport.DataSource = Nothing
        gvReport.Columns.Clear()
        gvReport.Rows.Clear()
        gvReport.GroupDescriptors.Clear()
        gvReport.MasterTemplate.SummaryRowsBottom.Clear()
        gvReport.EnableFiltering = True

        Dim locationArr As ArrayList = cbgLocation.CheckedValue
        Dim FromdateFilter As String = Nothing
        Dim TodateFilter As String = Nothing
        Dim LocFilter As String = ""
        Dim NatureOfDeductionFilter As String = ""
        Dim SectionFilter As String = ""

        FromdateFilter = dtpFromDate.Value.ToString("dd/MM/yyyy")
        TodateFilter = dtpToDate.Value.ToString("dd/MM/yyyy")
        If fndSection.Value <> "" Then
            SectionFilter = fndSection.Value
        End If
        If fndNatureofDeduction.Value <> "" Then
            NatureOfDeductionFilter = fndNatureofDeduction.Value
        End If
        If cbgLocation.CheckedValue.Count > 0 Then
            LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
            LocFilter = LocFilter.Replace("'", "")
        End If
        '===============================update by richa agarwal 3 July,2018 ticket no. KDI/02/07/18-000389 pick vendor name from vendor master table instead of transaction table
        Dim qry As String
        qry = "  select ISNULL(FINAL.Document_Type,'') AS Document_Type, TSPL_TDS_DEDUCTION_HEAD.TDS_Section as [Section Code],TSPL_TDS_SECTION_MASTER.Description as [Section description],final.Section_Description as [Deduction Code],TSPL_TDS_DEDUCTION_HEAD.Description as [Deduction Description],final.docnumber,final.Vendor_Code,TSPL_TDS_VENDOR_DETAILS.PAN as PANNo ,TSPL_VENDOR_MASTER.Vendor_Name,final.docdate ,final.baseamount,final.Document_Amount, Convert(Decimal(18,0),final.Credit) as Credit ,Actual_Surcharge as [Surcharge] ,Actual_Edu_Cess as [Edu Cess],Actual_Sec_Educess as [Sec Edu Cess], (Convert(Decimal(18,0),final.Credit)+Actual_Surcharge+Actual_Edu_Cess+Actual_Sec_Educess) as total ,final.TDS_Per,final.DeductCode ,'" + FromdateFilter + "' as FromDate,'" + TodateFilter + "' as Todate,'" + LocFilter + "' as LocFilter,'" + SectionFilter + "' as SectionFilter,'" + NatureOfDeductionFilter + "' as Naturefilter, Case When Debit>0 Then 0 else 1 End as [OrderDrCr] ,convert(date,docdate,103) as OrderDate, final.Description,Account_Seg_Code7 ,Location_Desc,final.Document_Type,is_For_TDS, " &
            " isnull(TSPL_TDS_PAYMENT_HEADER .BSR_Code,'') as BSR_Code ,isnull(TSPL_TDS_PAYMENT_HEADER.Challan_No,'') as Challan_No ,convert(varchar,TSPL_TDS_PAYMENT_HEADER.Challan_Date,103) as Challan_Date ,case when isnull(TSPL_TDS_PAYMENT_HEADER.Challan_No,'')='' then 0 else (Convert(Decimal(18,0),final.Credit)+Actual_Surcharge+Actual_Edu_Cess+Actual_Sec_Educess) end  as Amount_Paid from ( "
        qry += "  select TSPL_REMITTANCE.Document_Type,0 as is_For_TDS,TSPL_REMITTANCE_ENTRY.Section_Code,TSPL_REMITTANCE_ENTRY.Section_Description ,TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code ,TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Name ,TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code as docnumber,convert(varchar(12),Remittance_Date,103) as  docdate,TSPL_REMITTANCE_ENTRY_DETAIL.Document_No  as DN, TSPL_REMITTANCE_ENTRY_DETAIL.Actual_TDS_Base as  baseamount,TSPL_REMITTANCE.Document_Amount,TSPL_GL_ACCOUNTS.Account_Seg_Code7,case when TSPL_REMITTANCE_ENTRY_DETAIL.Document_Type  <>'D' then TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Total_TDS  else null end as Debit,case when TSPL_REMITTANCE_ENTRY_DETAIL.Document_Type  = 'D' then TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Total_TDS  else null end as Credit ,TSPL_REMITTANCE_ENTRY_DETAIL.Deduction_Code ,  TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2,0 as  TDS_Per ,'' as DeductCode,TSPL_REMITTANCE . Actual_Surcharge ,TSPL_REMITTANCE . Actual_Edu_Cess,TSPL_REMITTANCE . Actual_Sec_Educess, TSPL_REMITTANCE_ENTRY.Description,TSPL_GL_SEGMENT_CODE.Description as Location_Desc from TSPL_REMITTANCE_ENTRY  " &
                " left outer join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY.Remittance_Code=TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code  " &
                " left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_REMITTANCE_ENTRY.Comp_Code  " &
                " left outer join  TSPL_REMITTANCE on TSPL_REMITTANCE .Remittance_Code =TSPL_REMITTANCE_ENTRY.Remittance_Code  " &
                 " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_REMITTANCE .Branch_GL_AC  " &
                " left join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_GL_ACCOUNTS.Account_Seg_Code7 AND Segment_name='Locations' " &
                " where 2=2  and (Posted is not null) "
        If chkLocSelect.IsChecked Then
            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select one location ", Me.Text)
                Return
            End If
            qry += "and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (" + clsCommon.GetMulcallString(locationArr) + ") "
        End If
        ' BM00000007835 Included CreditNote Entries as negative amount
        qry += " union all  select TSPL_REMITTANCE.Document_Type,isnull(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0) as is_For_TDS,TSPL_REMITTANCE.Section_Code  as section ,TSPL_REMITTANCE.Deduction_Code as natureofdeduction ,TSPL_REMITTANCE.Vendor_Code  as vendor,TSPL_REMITTANCE.Vendor_Name as Name,TSPL_REMITTANCE.Document_No as docnumber ,Document_Date  as docdate,TSPL_REMITTANCE.Document_No as DN, case when ISNULL(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0)<>1 Then Actual_TDS_Base" &
            " WHEN  ISNULL(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='AP' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0)=1  THEN (SELECT ISNULL(Document_Total,0) AS Document_Total FROM TSPL_VENDOR_INVOICE_HEAD VH WHERE ISNULL(VH.Document_No ,'')=TSPL_VENDOR_INVOICE_HEAD.RefDocNo ) " &
            " WHEN  ISNULL(TSPL_VENDOR_INVOICE_HEAD.RefDocType ,'')='A' AND ISNULL(TSPL_VENDOR_INVOICE_HEAD.is_For_TDS,0)=1  THEN (SELECT ISNULL(Payment_Amount ,0) AS Payment_Amount FROM TSPL_PAYMENT_HEADER WHERE ISNULL(TSPL_PAYMENT_HEADER.Payment_No ,'')=(SELECT ISNULL(AgainstPayment_No,'') AS AgainstPayment_No FROM TSPL_VENDOR_INVOICE_DETAIL WHERE Document_No=TSPL_REMITTANCE.Document_No)) " &
            " Else 0 End as baseamount,TSPL_REMITTANCE.Document_Amount,TSPL_GL_ACCOUNTS.Account_Seg_Code7, 0 as Debit, case when TSPL_REMITTANCE.Document_Type = 'D' then Actual_Total_TDS* (case when TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 then 1 else  -1 end)when TSPL_REMITTANCE.Document_Type = 'C' then Actual_Total_TDS* (case when TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=1 then -1 else 1 end) else Actual_Total_TDS end as Credit, TSPL_REMITTANCE.Deduction_Code,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2, " &
        "  Case When ISNULL(TSPL_REMITTANCE.TDS_Per,0)<>0 Then TSPL_REMITTANCE.TDS_Per Else (Select Top(1) TSPL_VENDOR_INVOICE_DETAIL.TDS_Per From TSPL_VENDOR_INVOICE_DETAIL WHERE TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No) End as TDS_Per, " &
        " case when Select_By='C' then '01' else '02' end as DeductCode ,Actual_Surcharge ,Actual_Edu_Cess,Actual_Sec_Educess,(isnull(TSPL_VENDOR_INVOICE_HEAD.Description,'')+isnull(TSPL_PAYMENT_HEADER.Entry_Desc,'')) as Description,TSPL_GL_SEGMENT_CODE.Description as Location_Desc from TSPL_REMITTANCE  " &
               " left outer join TSPL_COMPANY_MASTER on  TSPL_REMITTANCE.comp_code=TSPL_COMPANY_MASTER.Comp_Code " &
               " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_REMITTANCE .Branch_GL_AC " &
               " left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_REMITTANCE.Document_No " &
          " left join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_GL_ACCOUNTS.Account_Seg_Code7 AND Segment_name='Locations' " &
          "left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No and  TSPL_PAYMENT_HEADER.Payment_Type in ('OA','AV')" &
                " where 2=2 and Remit_TDS in ('Y','N') "
        qry += " union all select TSPL_REMITTANCE.Document_Type,0 as is_For_TDS,TSPL_REMITTANCE.Section_Code  as section ,TSPL_REMITTANCE.Deduction_Code as natureofdeduction ,TSPL_REMITTANCE.Vendor_Code  as vendor,TSPL_REMITTANCE.Vendor_Name as Name,TSPL_REMITTANCE.Document_No as docnumber ,Document_Date  as docdate,TSPL_REMITTANCE.Document_No as DN,Actual_TDS_Base as baseamount,TSPL_REMITTANCE.Document_Amount,TSPL_GL_ACCOUNTS.Account_Seg_Code7, " &
             " 0 AS Debit, " &
             "  case when (TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No) then -1*(TSPL_BANK_REVERSE.Amount- TSPL_PAYMENT_HEADER.Payment_Amount) else null end as credit,TSPL_REMITTANCE.Deduction_Code,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_REMITTANCE.TDS_Per ,case when Select_By='C' then '01' else '02' end as DeductCode ,Actual_Surcharge ,Actual_Edu_Cess,Actual_Sec_Educess, TSPL_PAYMENT_HEADER.Entry_Desc as Description,TSPL_GL_SEGMENT_CODE.Description as Location_Desc from TSPL_REMITTANCE  " &
             " left outer join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.Payment_No=TSPL_REMITTANCE.Document_No   " &
             " inner join  TSPL_BANK_REVERSE on TSPL_PAYMENT_HEADER.Payment_No=TSPL_BANK_REVERSE.Document_No " &
             " left outer join TSPL_COMPANY_MASTER on  TSPL_REMITTANCE.comp_code=TSPL_COMPANY_MASTER.Comp_Code   " &
             "  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_REMITTANCE .Branch_GL_AC left join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_GL_ACCOUNTS.Account_Seg_Code7 AND Segment_name='Locations' where 2=2 and Remit_TDS in ('Y','N') "

        qry += Environment.NewLine & " Union All " & Environment.NewLine &
        " select 'S' as Document_Type,1 as Is_For_TDS, MAX(TSPL_TDS_DEDUCTION_HEAD.TDS_Section) as Section,  MAX(TSPL_PAYHEAD_MASTER.Deduction_Code) AS natureofDeduction, 'Employee' as Vendor,null as Name,TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE as docnumber , convert(varchar,max(TSPL_PAYPERIOD_MASTER.DATE_To),103) as DocDate, " & Environment.NewLine &
        " null as DN,sum(TSPL_GENERATE_SALARY_PAYHEADS.PAYABLE_AMOUNT ) as baseamount,sum(TSPL_GENERATE_SALARY_PAYHEADS.PAYABLE_AMOUNT ) as DEDUCTION_AMOUNT,max(TSPL_GENERATE_SALARY.LOCATION_CODE ) as Loc_Code,0 as Debit, sum(TSPL_GENERATE_SALARY_PAYHEADS.PAYABLE_AMOUNT ) as credit,MAX(TSPL_PAYHEAD_MASTER.Deduction_Code) AS Deduction_Code,'" & objCommonVar.CurrentCompanyName & "'  as Comp,null as Logo_Img,null as Logo_img2 " & Environment.NewLine &
        " ,null as TDS_Per,'02' as DeductCode, 0 as Actual_surcharge,0 as Actual_EduCess,0 as Actual_Sec_EduCess,max(TSPL_GENERATE_SALARY.GENERATE_REMARKS ) as Description,max(TSPL_LOCATION_MASTER.location_desc) as Location_Desc " & Environment.NewLine &
        " from TSPL_GENERATE_SALARY" & Environment.NewLine &
        " left outer join TSPL_GENERATE_SALARY_PAYHEADS on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE =TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE " & Environment.NewLine &
        " left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE " & Environment.NewLine &
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERATE_SALARY.LOCATION_CODE " & Environment.NewLine &
        " LEFT OUTER JOIN TSPL_TDS_DEDUCTION_HEAD ON TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_PAYHEAD_MASTER.Deduction_Code " & Environment.NewLine &
        " left outer join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE=TSPL_GENERATE_SALARY.PAY_PERIOD_CODE  " & Environment.NewLine &
        " where TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE ='TDS' and TSPL_GENERATE_SALARY.POSTED =1 group by TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE "


        qry += "  ) final left outer join TSPL_TDS_VENDOR_DETAILS on TSPL_TDS_VENDOR_DETAILS.Vendor_Code=final.Vendor_Code   left join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code =final.Deduction_Code "
        qry += "  left join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section" &
        " left outer join TSPL_TDS_PAYMENT_DETAIL on TSPL_TDS_PAYMENT_DETAIL.Against_Document_No =final.docnumber " &
        " and TSPL_TDS_PAYMENT_DETAIL.Document_No  not in (Select Against_TDS_PAYMENT_No from TSPL_PAYMENT_HEADER where Payment_No in (select Document_No  from tspl_bank_reverse where Reverse_Document ='Payments') and isnull(TSPL_PAYMENT_HEADER.Against_TDS_PAYMENT_No,'')<>'' ) " &
        " left outer join TSPL_TDS_PAYMENT_HEADER on TSPL_TDS_PAYMENT_HEADER.Document_No  =TSPL_TDS_PAYMENT_DETAIL.Document_No " &
        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =final.Vendor_Code " &
        "   where 2=2   and convert(date,docdate,103)>=convert(date,'" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + "',103) and convert(date,docdate,103)<=  convert(date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "' ,103  )  "
        If fndSection.Value <> "" Then
            qry += " and  coalesce(TSPL_TDS_DEDUCTION_HEAD.TDS_Section,'')='" + fndSection.Value + "' "
        End If
        If fndNatureofDeduction.Value <> "" Then
            qry += " and final.Deduction_Code='" + fndNatureofDeduction.Value + "'"
        End If
        If chkLocSelect.IsChecked Then
            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select one location ", Me.Text)
                Return
            End If
            qry += " and final.Account_Seg_Code7 in (" + clsCommon.GetMulcallString(locationArr) + ") "
        End If
        qry += " AND (Convert(Decimal(18,0),final.Credit)+Actual_Surcharge+Actual_Edu_Cess+Actual_Sec_Educess)<>0 order by OrderDate"
        Try
            gvReport.DataSource = clsDBFuncationality.GetDataTable(qry)
            FormatGrid()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FormatGrid()
        gvReport.AllowAddNewRow = False
        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            gvReport.Columns(ii).IsVisible = False
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As GridViewSummaryItem

        gvReport.Columns("Section Code").IsVisible = True
        gvReport.Columns("Section Code").Width = 50
        gvReport.Columns("Section Code").HeaderText = "Section Code"

        gvReport.Columns("Section description").IsVisible = True
        gvReport.Columns("Section description").Width = 100
        gvReport.Columns("Section description").HeaderText = "Section description"

        gvReport.Columns("Deduction Code").IsVisible = True
        gvReport.Columns("Deduction Code").Width = 100
        gvReport.Columns("Deduction Code").HeaderText = "Deduction Code"

        gvReport.Columns("Deduction Description").IsVisible = True
        gvReport.Columns("Deduction Description").Width = 100
        gvReport.Columns("Deduction Description").HeaderText = "Deduction Description"

        gvReport.Columns("PANNo").IsVisible = True
        gvReport.Columns("PANNo").Width = 80
        gvReport.Columns("PANNo").HeaderText = "PAN"


        gvReport.Columns("Vendor_Code").IsVisible = True
        gvReport.Columns("Vendor_Code").Width = 50
        gvReport.Columns("Vendor_Code").HeaderText = "Vendor Code"

        gvReport.Columns("Vendor_Name").IsVisible = True
        gvReport.Columns("Vendor_Name").Width = 100
        gvReport.Columns("Vendor_Name").HeaderText = "Vendor"

        gvReport.Columns("docnumber").IsVisible = True
        gvReport.Columns("docnumber").Width = 80
        gvReport.Columns("docnumber").HeaderText = "Document No"


        gvReport.Columns("Account_Seg_Code7").IsVisible = True
        gvReport.Columns("Account_Seg_Code7").Width = 80
        gvReport.Columns("Account_Seg_Code7").HeaderText = "Location Code"

        gvReport.Columns("Location_Desc").IsVisible = True
        gvReport.Columns("Location_Desc").Width = 80
        gvReport.Columns("Location_Desc").HeaderText = "Location Name"

        gvReport.Columns("docdate").IsVisible = True
        gvReport.Columns("docdate").Width = 80
        gvReport.Columns("docdate").HeaderText = "Doc Date"

        gvReport.Columns("baseamount").Width = 100
        gvReport.Columns("baseamount").HeaderText = "Taxable Amount"
        gvReport.Columns("baseamount").IsVisible = True
        gvReport.Columns("baseamount").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("baseamount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("Document_Amount").Width = 100
        gvReport.Columns("Document_Amount").HeaderText = "Document Amount"
        gvReport.Columns("Document_Amount").IsVisible = True
        gvReport.Columns("Document_Amount").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("Document_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("Credit").Width = 100
        gvReport.Columns("Credit").HeaderText = "Income Tax"
        gvReport.Columns("Credit").IsVisible = True
        gvReport.Columns("Credit").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("Credit", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("TDS_Per").Width = 100
        gvReport.Columns("TDS_Per").HeaderText = "Rate At Which Tax Deduct"
        gvReport.Columns("TDS_Per").IsVisible = True
        gvReport.Columns("TDS_Per").FormatString = "{0:F2}"
        'item1 = New GridViewSummaryItem("TDS_Per", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)

        gvReport.Columns("DeductCode").IsVisible = True
        gvReport.Columns("DeductCode").Width = 80
        gvReport.Columns("DeductCode").HeaderText = "Deduct Code"

        gvReport.Columns("Surcharge").IsVisible = True
        gvReport.Columns("Surcharge").Width = 80
        gvReport.Columns("Surcharge").HeaderText = "Surcharge"
        gvReport.Columns("Surcharge").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("Surcharge", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("Edu Cess").IsVisible = True
        gvReport.Columns("Edu Cess").Width = 80
        gvReport.Columns("Edu Cess").HeaderText = "Edu Cess"
        gvReport.Columns("Edu Cess").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("Edu Cess", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("Sec Edu Cess").IsVisible = True
        gvReport.Columns("Sec Edu Cess").Width = 80
        gvReport.Columns("Sec Edu Cess").HeaderText = "Sec Edu Cess"
        gvReport.Columns("Sec Edu Cess").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("Sec Edu Cess", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("total").IsVisible = True
        gvReport.Columns("total").Width = 80
        gvReport.Columns("total").HeaderText = "Total TDS"
        gvReport.Columns("total").FormatString = "{0:F2}"
        item1 = New GridViewSummaryItem("total", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gvReport.Columns("Description").IsVisible = True
        gvReport.Columns("Description").Width = 150

        gvReport.Columns("Document_Type").IsVisible = True
        gvReport.Columns("Document_Type").Width = 100
        gvReport.Columns("Document_Type").HeaderText = "Document Type"


        gvReport.Columns("BSR_Code").IsVisible = True
        gvReport.Columns("BSR_Code").Width = 100
        gvReport.Columns("BSR_Code").HeaderText = "BSR Code"

        gvReport.Columns("Challan_No").IsVisible = True
        gvReport.Columns("Challan_No").Width = 100
        gvReport.Columns("Challan_No").HeaderText = "Challan No"

        gvReport.Columns("Challan_Date").IsVisible = True
        gvReport.Columns("Challan_Date").Width = 100
        gvReport.Columns("Challan_Date").HeaderText = "Challan Date"

        gvReport.Columns("Amount_Paid").IsVisible = True
        gvReport.Columns("Amount_Paid").Width = 80
        gvReport.Columns("Amount_Paid").HeaderText = "Amount Paid"
        gvReport.Columns("Amount_Paid").FormatString = "{0:F2}"

        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        RadPageView1.SelectedPage = RadPageViewPage2
        gvReport.BestFitColumns()
        ReStoreGridLayout()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub


    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        'LoadData()
        'printdata(EnumExportTo.PDF)
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        'LoadData()
        'printdata(EnumExportTo.Excel)
        Export(EnumExportTo.Excel)
    End Sub

    Public Sub printdata(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add(strtemp)
            If clsCommon.myLen(fndSection.Value) > 0 Then
                arrHeader.Add("Section - " + fndSection.Value + " (" + txtSection.Text + ")")
            End If
            If clsCommon.myLen(fndNatureofDeduction.Value) > 0 Then
                arrHeader.Add("Nature of Deduction - " + fndNatureofDeduction.Value + " (" + txtNatureofDeduction.Text + ")")
            End If

            If chkLocSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location : " + strtemp)
            End If


            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("TDS Ledger", gvReport, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("TDS Ledger ", gvReport, arrHeader, Me.Text, True)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub



    Private Sub RadMenuItem4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gvReport.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvReport.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gvReport.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj = Nothing
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub RadMenuItem5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem5.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub gvReport_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvReport.CellDoubleClick
        If (clsCommon.CompairString(clsCommon.myCstr(gvReport.CurrentRow.Cells("Document_Type").Value), "I") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(gvReport.CurrentRow.Cells("Document_Type").Value), "C") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(gvReport.CurrentRow.Cells("Document_Type").Value), "D") = CompairStringResult.Equal) AndAlso (clsCommon.CompairString(clsCommon.myCstr(gvReport.CurrentRow.Cells("is_For_TDS").Value), "0") = CompairStringResult.Equal) Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, clsCommon.myCstr(gvReport.CurrentRow.Cells("docnumber").Value))
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gvReport.CurrentRow.Cells("is_For_TDS").Value), "1") = CompairStringResult.Equal Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnAPInvoiceEntryTDS, clsCommon.myCstr(gvReport.CurrentRow.Cells("docnumber").Value))
        Else
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, clsCommon.myCstr(gvReport.CurrentRow.Cells("docnumber").Value))
        End If
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            Dim strtemp As String
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmrptTDSLedger & "'"))

            If clsCommon.myLen(fndSection.Value) > 0 Then
                arrHeader.Add("Section - " + fndSection.Value + " (" + txtSection.Text + ")")
            End If
            If clsCommon.myLen(fndNatureofDeduction.Value) > 0 Then
                arrHeader.Add("Nature of Deduction - " + fndNatureofDeduction.Value + " (" + txtNatureofDeduction.Text + ")")
            End If

            If chkLocSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location : " + strtemp)
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
                transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gvReport, "", Me.Text, , arrHeader)
                'transportSql.exportdata(gvReport, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
                clsCommon.MyExportToPDF("TDS Ledger", gvReport, arrHeader, "TDS Ledger", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gvReport.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvReport.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gvReport.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub
End Class
