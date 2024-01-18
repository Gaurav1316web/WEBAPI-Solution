'****** Created By: Pankaj Kumar Chaudhary

'--Updation By --[Pankaj Kumar Chaudhary]--Against Ticket No-[BM00000001236]
'---------update by Preeti Gupta against ticket no[BM00000008020]
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
Public Class FrmBankBook
    Inherits FrmMainTranScreen

    Public FilterON As Boolean = False
    Public FilterfromDate As Date
    Public FilterToDate As Date
    Public FilterBankCode As String


    Dim userCode, companyCode As String
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim wherDocumentCode As String = " where 2 = 2 "
    Dim StrPermission As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
        RadSplitButton1.Visible = MyBase.isExport 'MyBase.isQuickExportFlag
    End Sub
    Private Sub FrmBankBook_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ddlBankType.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
        ddRecoStatus.SelectedIndex = 0
        LoadBanks()
        SetUserMgmtNew()
        LoadLocation()
        chkDetail.IsChecked = True
        chkBanksAll.IsChecked = True
        chkLocAll.IsChecked = True
        dtFrm.Value = System.DateTime.Now.Date
        dtTo.Value = System.DateTime.Now.Date
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        chkExcludeProvisionBank.Visible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowCheckExcludeProvisionBank, clsFixedParameterCode.ShowCheckExcludeProvisionBank, Nothing)) = 1, True, False)
        pnlDocument.Visible = False
        lblDocumentNo.Text = ""
        If clsCommon.myLen(Me.Tag) > 0 Then
            lblDocumentNo.Text = clsCommon.myCstr(Me.Tag)
            Dim qry As String = " select * from ( " &
                                " select  TSPL_PAYMENT_HEADER.Payment_No as Document_No,convert ( varchar, TSPL_PAYMENT_HEADER.Payment_Date,103) as Document_Date ,TSPL_PAYMENT_HEADER.Bank_Code,Case when  TSPL_BANK_MASTER.Bank_type ='B' then 'Bank'  When  TSPL_BANK_MASTER.Bank_type ='C' then 'Cash' When  TSPL_BANK_MASTER.Bank_type ='P' then 'Petty Cash' When  TSPL_BANK_MASTER.Bank_type ='S' then 'Settlement'  end as Bank_type  from TSPL_PAYMENT_HEADER left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE = TSPL_PAYMENT_HEADER.Bank_Code " &
                                " union all " &
                                " select  TSPL_RECEIPT_HEADER.Receipt_No as Document_No,convert ( varchar, TSPL_RECEIPT_HEADER.Receipt_Date,103) as Document_Date ,TSPL_RECEIPT_HEADER.Bank_Code,Case when  TSPL_BANK_MASTER.Bank_type ='B' then 'Bank'  When  TSPL_BANK_MASTER.Bank_type ='C' then 'Cash' When  TSPL_BANK_MASTER.Bank_type ='P' then 'Petty Cash' When  TSPL_BANK_MASTER.Bank_type ='S' then 'Settlement'  end as Bank_type  from TSPL_RECEIPT_HEADER left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE = TSPL_RECEIPT_HEADER.Bank_Code " &
                                " union all " &
                                " select  TSPL_BANK_TRANSFER.Transfer_No as Document_No,convert ( varchar, TSPL_BANK_TRANSFER.Transfer_Date,103) as Document_Date ,TSPL_BANK_TRANSFER.To_Bank_Code as Bank_Code,Case when  TSPL_BANK_MASTER.Bank_type ='B' then 'Bank'  When  TSPL_BANK_MASTER.Bank_type ='C' then 'Cash' When  TSPL_BANK_MASTER.Bank_type ='P' then 'Petty Cash' When  TSPL_BANK_MASTER.Bank_type ='S' then 'Settlement'  end as Bank_type  from TSPL_BANK_TRANSFER left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE = TSPL_BANK_TRANSFER.TO_BANK_CODE  " &
                                " union all " &
                                " select  TSPL_BANK_REVERSE.Reverse_Code as Document_No,convert ( varchar, TSPL_BANK_REVERSE.Reversal_Date,103) as Document_Date ,TSPL_BANK_REVERSE.Bank_Code as Bank_Code,Case when  TSPL_BANK_MASTER.Bank_type ='B' then 'Bank'  When  TSPL_BANK_MASTER.Bank_type ='C' then 'Cash' When  TSPL_BANK_MASTER.Bank_type ='P' then 'Petty Cash' When  TSPL_BANK_MASTER.Bank_type ='S' then 'Settlement'  end as Bank_type  from TSPL_BANK_REVERSE left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE = TSPL_BANK_REVERSE.BANK_CODE  " &
                                "  ) Final where Final.Document_No = '" + clsCommon.myCstr(Me.Tag) + "' "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                dtFrm.Value = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                dtTo.Value = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                chkDetail.IsChecked = True
                ddlStatus.Text = "All"
                ddRecoStatus.Text = "All"
                wherDocumentCode = wherDocumentCode + " and CTETemp.DocNo = '" + lblDocumentNo.Text + "'"
            End If
            btnRefresh.PerformClick()
            pnlDocument.Visible = True
        ElseIf FilterON Then
            dtFrm.Value = FilterfromDate
            dtTo.Value = FilterToDate

            Dim qry As String = "select (case when Bank_type='B' then 'Bank' else ((case when Bank_type='C' then 'Cash' else ((case when Bank_type='P' then 'Petty Cash' else ((case when Bank_type='S' then 'Settlement' else '' end)) end)) end)) end) from tspl_bank_master where BANK_CODE='" + FilterBankCode + "'"
            ddlBankType.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            LoadBanks()
            chkBanksSelect.IsChecked = True
            Dim arr As New ArrayList
            arr.Add(FilterBankCode)
            cbgBanks.CheckedValue = arr
            chkDetail.IsChecked = True
            ddlStatus.Text = "All"
            ddRecoStatus.Text = "All"
            btnRefresh.PerformClick()
        End If
    End Sub

    Sub LoadBanks()
        Dim qry As String = ""
        '--- Added By abhishek As on 23 Aug 2012 For Multiple Bank Type According to Amit sir

        If ddlBankType.Text = "Bank" Then
            qry = " select BANK_CODE  ,DESCRIPTION  from TSPL_BANK_MASTER Where Bank_type='B'"
        ElseIf ddlBankType.Text = "Cash" Then
            qry = " select BANK_CODE  ,DESCRIPTION  from TSPL_BANK_MASTER Where Bank_type ='C'"
        ElseIf ddlBankType.Text = "Petty Cash" Then
            qry = " select BANK_CODE  ,DESCRIPTION  from TSPL_BANK_MASTER Where Bank_type ='P'"
        ElseIf ddlBankType.Text = "Settlement" Then
            qry = " select BANK_CODE  ,DESCRIPTION  from TSPL_BANK_MASTER Where Bank_type ='S'"
        End If
        '' Codes ends here ------------
        cbgBanks.ValueMember = "BANK_CODE"
        cbgBanks.DisplayMember = "DESCRIPTION"
        cbgBanks.DataSource = clsDBFuncationality.GetDataTable(qry)
        
    End Sub

    Sub LoadLocation()
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationSegment()
        Dim qry As String = " select Segment_code as Code, Description as Name from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        qry += " and Segment_code in (" & StrPermission & ")"
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub
    Sub funreset()
        'fndBank.txtValue.Text = ""
        dtFrm.Value = System.DateTime.Now.Date
        dtTo.Value = System.DateTime.Now.Date
        chkExcludeProvisionBank.Checked = False
        LoadBanks()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        LoadLocation()
        ddlBankType.SelectedIndex = 0 '--- Added By abhishek As on 23 Aug 2012 For Multiple Bank Type According to Amit sir
        ddlStatus.SelectedIndex = 0
        ddRecoStatus.SelectedIndex = 0
        gvReport.DataSource = Nothing
        gvReport.Rows.Clear()
        gvReport.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        'wherDocumentCode = ""
        'pnlDocument.Visible = False
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gvReport
        RefreshData()
    End Sub

    
    Public Sub RefreshData()
        '======Update By Preeti Gupta Against Ticket no[BM00000008103]
        '' changes by shivani [BM00000008458]
        Try
            If chkBanksSelect.IsChecked = True AndAlso cbgBanks.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select Atleast One Bank OR Select All")
            End If
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select Atleast One Location OR Select All")
            End If

            Dim strAddress As String
            If chkBanksSelect.IsChecked AndAlso cbgBanks.CheckedValue.Count = 1 Then
                ' strAddress = "(Select  MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_STATE_MASTER.State_Name) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end) from TSPL_LOCATION_MASTER LEFT OUTER  JOIN TSPL_STATE_MASTER ON TSPL_LOCATION_MASTER.State=TSPL_STATE_MASTER.State_Code LEFT OUTER JOIN TSPL_BANK_MASTER ON RIGHT(TSPL_BANK_MASTER.BANKACC,3)=TSPL_LOCATION_MASTER.Loc_Segment_Code Where TSPL_BANK_MASTER.Bank_Code in (" + clsCommon.GetMulcallString(cbgBanks.CheckedValue) + "))  "
                strAddress = " (Select (TSPL_LOCATION_MASTER.Add1 + case When isnull(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+ TSPL_LOCATION_MASTER.Add2 End + Case When isnull(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+ TSPL_LOCATION_MASTER.Add3 end + case When isnull(TSPL_LOCATION_MASTER.City_Code,'') ='' then '' else ', '+ TSPL_LOCATION_MASTER.City_Code end + Case When isnull(TSPL_LOCATION_MASTER.State,'')='' Then '' else ', '+TSPL_STATE_MASTER.State_Name end +  Case When len(TSPL_LOCATION_MASTER.Pin_Code)<=0 Then '' Else ', '+ cast (TSPL_LOCATION_MASTER.Pin_Code as varchar) end) from TSPL_LOCATION_MASTER LEFT OUTER  JOIN TSPL_STATE_MASTER ON TSPL_LOCATION_MASTER.State=TSPL_STATE_MASTER.State_Code LEFT OUTER JOIN TSPL_BANK_MASTER ON RIGHT(TSPL_BANK_MASTER.BANKACC,3)=TSPL_LOCATION_MASTER.Loc_Segment_Code Where TSPL_BANK_MASTER.Bank_Code in (" + clsCommon.GetMulcallString(cbgBanks.CheckedValue) + ") and  TSPL_LOCATION_MASTER.Location_code =TSPL_LOCATION_MASTER.Loc_Segment_Code) "
            ElseIf chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 1 AndAlso chkBanksSelect.IsChecked = False Then
                strAddress = " (Select (TSPL_LOCATION_MASTER.Add1 + case When isnull(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+ TSPL_LOCATION_MASTER.Add2 End + Case When isnull(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+ TSPL_LOCATION_MASTER.Add3 end + case When isnull(TSPL_LOCATION_MASTER.City_Code,'') ='' then '' else ', '+ TSPL_LOCATION_MASTER.City_Code end+ Case When isnull(TSPL_LOCATION_MASTER.State,'')='' Then '' else ', '+ TSPL_STATE_MASTER.State_Name end +  Case When len(TSPL_LOCATION_MASTER.Pin_Code)<=0 Then '' Else ', '+ cast(TSPL_LOCATION_MASTER.Pin_Code as Varchar)  end) from TSPL_LOCATION_MASTER LEFT OUTER  JOIN TSPL_STATE_MASTER ON TSPL_LOCATION_MASTER.State=TSPL_STATE_MASTER.State_Code Where TSPL_LOCATION_MASTER.Location_code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            Else
                strAddress = " (TSPL_COMPANY_MASTER.Add1 + case When isnull(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+ TSPL_COMPANY_MASTER.Add2 End + Case When isnull(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Add3 end + case When isnull(TSPL_COMPANY_MASTER.City_Code,'') ='' then '' else ', '+ TSPL_COMPANY_MASTER.City_Code end+ Case When isnull(TSPL_COMPANY_MASTER.State,'')='' Then '' else ', '+ TSPL_COMPANY_MASTER.State end +  Case When isnull(TSPL_COMPANY_MASTER.Pincode,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Pincode  end) "
            End If

            Dim strSelectedBank As String = ""
            If chkBanksSelect.IsChecked = True AndAlso cbgBanks.CheckedValue.Count > 0 Then
                strSelectedBank = clsCommon.GetMulcallString(cbgBanks.CheckedValue)
            End If

            Dim strSelectedLocation As String = ""
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                strSelectedLocation = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
            Else
                strSelectedLocation = StrPermission
            End If

            Dim chrStatus As String = ""
            If ddlStatus.Text = "Posted" Then
                chrStatus = "Y"
            ElseIf ddlStatus.Text = "Unposted" Then
                chrStatus = "N"
            End If

            Dim chrBankType As Char = ""
            If ddlBankType.Text = "Bank" Then
                chrBankType = "B"
            ElseIf ddlBankType.Text = "Cash" Then
                chrBankType = "C"
            ElseIf ddlBankType.Text = "Petty Cash" Then
                chrBankType = "P"
            ElseIf ddlBankType.Text = "Settlement" Then
                chrBankType = "S"
            End If

            Dim rptHead As String = ""
            If ddlBankType.Text = "Bank" Then
                rptHead = "Bank Book"
            ElseIf ddlBankType.Text = "Cash" Then
                rptHead = "Cash Book"
            ElseIf ddlBankType.Text = "Petty Cash" Then
                rptHead = "Petty Cash Book"
            ElseIf ddlBankType.Text = "Settlement" Then
                rptHead = " Settlement Book "
            End If


            Dim rptRecoStatus As String = ""
            If ddRecoStatus.Text = "CLR" Then
                rptRecoStatus = "AND ISNULL(BR.Reconciliation_Status,'')='C' "
            ElseIf ddRecoStatus.Text = "OS" Then
                rptRecoStatus = "AND ISNULL(BR.Reconciliation_Status,'')=' ' "
            End If

            Dim Qry As String = clsBankReco.GetQueryForTransactionOFBB(True, dtFrm.Value, dtTo.Value, strSelectedBank, strSelectedLocation, strAddress, chrStatus, chrBankType, rptHead, rptRecoStatus, False, Nothing, IIf(chkExcludeProvisionBank.Checked, True, False))
            If chkSummary.IsChecked Then
                'If chkDocWise.Checked Then
                '    Qry = "Select BANK_CODE, MAX(DESCRIPTION) as DESCRIPTION, DocNo, MAX(DocDate) as DocDate, SUM(Debit_Amount ) as Debit_Amount, SUM(Credit_Amount ) as Credit_Amount,DocType, MAX(Status) as Status from ( " + Qry + " ) YYY Where ISNULL(DocNo, '')<>''  Group By BANK_CODE, DocNo,DocType Order By Convert(date,MAX(DocDate) , 103), DocNo "
                'Else
                '    Qry = "SELECT BANK_CODE, MAX(DESCRIPTION) AS [DESCRIPTION], MAX(BankType) AS BankType,MAX(Startdate) AS Startdate,MAX(EndDate) AS EndDate,MAX(RunDate) AS RunDate,SUM(BalAmt) AS BalAmt,SUM(Debit_Amount) AS Debit_Amount ,SUM(Credit_Amount) AS Credit_Amount,(SUM(Debit_Amount)-SUM(Credit_Amount)+SUM(BalAmt)) AS Closing_Balance   FROM (" + Qry + ")POP GROUP BY BANK_CODE ORDER BY  BANK_CODE "
                'End If

                If chkDocWise.Checked Then
                    Qry = "Select final.*,TSPL_COMPANY_MASTER.Logo_Img as Logo_Img , TSPL_COMPANY_MASTER.Logo_Img2 Logo_Img2,TSPL_COMPANY_MASTER.Comp_Name as CompName from ( Select BANK_CODE, MAX(DESCRIPTION) as DESCRIPTION, DocNo, MAX(DocDate) as DocDate, SUM(Debit_Amount ) as Debit_Amount, SUM(Credit_Amount ) as Credit_Amount,DocType, MAX(Status) as Status,max(Add1) as Add1,max(Startdate ) as Startdate,max(EndDate ) as EndDate,max(RunDate ) as RunDate,max([Payment Code]) as [Payment Mode] from ( " + Qry + " ) YYY Where ISNULL(DocNo, '')<>''  Group By BANK_CODE, DocNo,DocType ) final Left Outer Join TSPL_COMPANY_MASTER ON '" & objCommonVar.CurrentCompanyCode & "'=TSPL_COMPANY_MASTER.Comp_Code Order By Convert(date,DocDate, 103), DocNo "
                Else
                    Qry = "Select final.*,TSPL_COMPANY_MASTER.Logo_Img as Logo_Img , TSPL_COMPANY_MASTER.Logo_Img2 Logo_Img2,TSPL_COMPANY_MASTER.Comp_Name as CompName from ( SELECT BANK_CODE, MAX(DESCRIPTION) AS [DESCRIPTION], MAX(BankType) AS BankType,MAX(Startdate) AS Startdate,MAX(EndDate) AS EndDate,MAX(RunDate) AS RunDate,SUM(BalAmt) AS BalAmt,SUM(Debit_Amount) AS Debit_Amount ,SUM(Credit_Amount) AS Credit_Amount,(SUM(Debit_Amount)-SUM(Credit_Amount)+SUM(BalAmt)) AS Closing_Balance,max(POP.Add1) as Add1,max([Payment Code]) as [Payment Mode]   FROM (" + Qry + ")POP GROUP BY BANK_CODE )final Left Outer Join TSPL_COMPANY_MASTER ON '" & objCommonVar.CurrentCompanyCode & "'=TSPL_COMPANY_MASTER.Comp_Code ORDER BY  BANK_CODE "
                End If
            ElseIf chkDetail.IsChecked Then
                ''richa MIL/29/07/19-000113
                Qry = "WITH CTETemp as (" + Environment.NewLine & _
                    "Select YYY.DocType, YYY.rptHeading, YYY.NARR_MASTER, YYY.NARR_DETAIL, YYY.RunDate, YYY.Startdate, YYY.EndDate,convert(varchar,yyy.Reconciliation_Date,103) as [Reco Date], YYY.BANK_CODE, YYY.BankType, YYY.DESCRIPTION, YYY.DocNo, YYY.Entry_Desc, convert(varchar,YYY.DocDate,103)as DocDate, YYY.CHEQUE_NO, YYY.CHEQUE_DATE, YYY.CustVendorCode, YYY.CustVendName, YYY.Source_Code, YYY.Source_Name, YYY.Loc_Code, YYY.Loc_Name, YYY.BANKGL_account_Code, YYY.BANKGL_Account_Name, YYY.BalAmt, YYY.Balance, YYY.Debit_Amount, YYY.Credit_Amount, YYY.CummulativeBal, YYY.Status, YYY.Logo_Img, YYY.Logo_Img2, YYY.Add1, YYY.CompName, YYY.TransType, YYY.Type, ROW_NUMBER() OVER (Partition By YYY.Bank_Code ORDER BY CONVERT(Date,YYY.DocDate,103),DocNo) as [RowNo],doctypefororder,[Payment Code] From (" + Environment.NewLine & _
                "" & Qry & "" + Environment.NewLine & _
                " ) YYY" + Environment.NewLine & _
                ")" + Environment.NewLine & _
            " Select DocType, rptHeading, NARR_MASTER, NARR_DETAIL, RunDate, Startdate, EndDate,[Reco Date], BANK_CODE, BankType, DESCRIPTION, DocNo, Entry_Desc, DocDate, CHEQUE_NO, CHEQUE_DATE, CustVendorCode, CustVendName, CustomerVendor_Master.Cust_Group_Desc, CustomerVendor_Master.Cust_Type_Desc, CustomerVendor_Master.CUST_CATEGORY_DESC, Source_Code, Source_Name, Loc_Code, Loc_Name, BANKGL_account_Code, BANKGL_Account_Name, Debit_Amount, Credit_Amount, BalAmt," + Environment.NewLine & _
            " Balance, (sum(Balance) over (partition by Bank_Code order by convert(date,DocDate,103) ,doctypefororder,rowno)) as CummulativeBal, Status,  Logo_Img,  Logo_Img2 , Add1, CompName, TransType, RowNo,doctypefororder" + Environment.NewLine & _
            " ,[Payment Code] as [Payment Mode] from CTETemp" + Environment.NewLine & _
                " LEFT OUTER JOIN (select TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Cust_Group_Code, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, TSPL_CUSTOMER_MASTER.Cust_Category_Code, TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC, TSPL_CUSTOMER_MASTER.Cust_Type_Code, TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, 'Customer' as [Type] from TSPL_CUSTOMER_MASTER" + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.Cust_Category_Code" + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER on TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code=TSPL_CUSTOMER_MASTER.Cust_Type_Code" + Environment.NewLine & _
                " UNION ALL" + Environment.NewLine & _
                " select TSPL_VENDOR_MASTER.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Group_Code, TSPL_VENDOR_GROUP.Group_Desc as Cust_Group_Desc, '' as Cust_Category_Code, '' as CUST_CATEGORY_DESC, '' as Cust_Type_Code, '' as Cust_Type_Desc, 'Vendor' as [Type] from TSPL_VENDOR_MASTER" + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=TSPL_VENDOR_MASTER.Vendor_Group_Code) CustomerVendor_Master on CustomerVendor_Master.Type=CTETemp.Type AND CustomerVendor_Master.Cust_Code=CTETemp.CustVendorCode" + Environment.NewLine & _
                " " + wherDocumentCode + " " & IIf(chkWithoutOpening.Checked = True, " and isnull(CTETemp.DocType,'')<>'' ", "") & " " + Environment.NewLine & _
                "  ORDER BY convert(date,DocDate,103) ,doctypefororder,rowno "
                ''convert(date,docdate,103),DocNo"
                'Qry += " Order By Bank_Code, Convert(Date,SourceDoc_date, 103), DocNo"
            End If

            dt = clsDBFuncationality.GetDataTable(Qry)

            'Ticket No - BM00000002813 by PURAN
            If Not (chkSummary.IsChecked) Then
                'Dim openingBal As Double = 0.0
                'Dim lstBank As New List(Of String)
                'For Each dr As DataRow In dt.Rows
                '    If lstBank.Contains(dr("Bank_Code").ToString()) Then
                '        dr("CummulativeBal") = openingBal + clsCommon.myCdbl(dr("Debit_Amount")) - clsCommon.myCdbl(dr("Credit_Amount"))
                '        openingBal = clsCommon.myCdbl(dr("CummulativeBal"))
                '    Else
                '        lstBank.Add(dr("Bank_Code").ToString())
                '        openingBal = 0
                '        openingBal = clsCommon.myCdbl(dr("BalAmt"))
                '        dr("CummulativeBal") = openingBal
                '    End If
                'Next
            End If

            If chkSummary.IsChecked AndAlso clsCommon.CompairString(ddlBankType.Text, "Bank") = CompairStringResult.Equal AndAlso pnlAdminSetting.Visible AndAlso chkReconcile.Checked Then
                Dim dtAS As DataTable = clsReconciliationSetting.GetAccounts(clsRecoSettingReportName.Bank, clsRecoSettingReportComponent.BankAccount, dtFrm.Value, dtTo.Value)
                Dim arr As Dictionary(Of String, clsTempDrCrAmt) = Nothing
                dt.Columns.Add("SubledgerDrAmt", GetType(Double))
                dt.Columns.Add("SubledgerCrAmt", GetType(Double))
                If dtAS IsNot Nothing AndAlso dtAS.Rows.Count > 0 Then
                    arr = New Dictionary(Of String, clsTempDrCrAmt)
                    For Each dr As DataRow In dtAS.Rows
                        Dim obj As clsTempDrCrAmt = New clsTempDrCrAmt()
                        obj.DrAmt = clsCommon.myCdbl(dr("SubledgerDrAmt"))
                        obj.CrAmt = clsCommon.myCdbl(dr("SubledgerCrAmt"))
                        arr.Add(clsCommon.myCstr(clsCommon.myCstr(dr("docNo")) + clsCommon.myCstr(dr("DocType"))).ToUpper(), obj)
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        Dim strSourceDocNo As String = clsCommon.myCstr(clsCommon.myCstr(dt.Rows(ii)("DocNo")) + clsCommon.myCstr(dt.Rows(ii)("DocType"))).ToUpper()
                        If arr.ContainsKey(strSourceDocNo) Then
                            dt.Rows(ii)("SubledgerDrAmt") = clsCommon.myCdbl(arr.Item(strSourceDocNo).DrAmt)
                            dt.Rows(ii)("SubledgerCrAmt") = clsCommon.myCdbl(arr.Item(strSourceDocNo).CrAmt)
                        End If
                    Next
                End If


                If chkMismatch.Checked Then
                    Dim dtView As DataView = dt.DefaultView
                    dtView.RowFilter = "  (SubledgerDrAmt<>Debit_Amount or SubledgerCrAmt<>Credit_Amount)"
                End If
            End If

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("No Data found ")
            Else
                gvReport.MasterTemplate.SummaryRowsBottom.Clear()
                gvReport.DataSource = Nothing
                gvReport.Rows.Clear()
                gvReport.Columns.Clear()
                gvReport.DataSource = dt.DefaultView
                gvReport.EnableFiltering = True
                gvReport.EnableSorting = True
                gvReport.ShowFilteringRow = True
                FormatGrid()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub LoadExcel(ByVal IsPrint As Exporter)
        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy") + " ")
        If chkBanksSelect.IsChecked Then
            Dim stVSPName As String = ""
            For Each StrName As String In cbgBanks.CheckedDisplayMember
                If clsCommon.myLen(stVSPName) > 0 Then
                    stVSPName += ", "
                End If
                stVSPName += StrName
            Next
            Dim strVSPCode As String = ""
            For Each StrCode As String In cbgBanks.CheckedValue
                If clsCommon.myLen(strVSPCode) > 0 Then
                    strVSPCode += ", "
                End If
                strVSPCode += StrCode
            Next
            arrHeader.Add(("Bank: " + stVSPName + " "))
        End If
        If chkLocSelect.IsChecked Then
            Dim stVSPName As String = ""
            For Each StrName As String In cbgLocation.CheckedDisplayMember
                If clsCommon.myLen(stVSPName) > 0 Then
                    stVSPName += ", "
                End If
                stVSPName += StrName
            Next
            Dim strVSPCode As String = ""
            For Each StrCode As String In cbgLocation.CheckedValue
                If clsCommon.myLen(strVSPCode) > 0 Then
                    strVSPCode += ", "
                End If
                strVSPCode += StrCode
            Next
            arrHeader.Add(("Location: " + stVSPName + " "))
        End If
        If IsPrint = Exporter.Excel Then
            clsCommon.MyExportToExcelGrid("Bank Cash Book" + IIf(chkDetail.IsChecked, "( Detail )", "( Summary )"), gvReport, arrHeader, Me.Text)
        ElseIf IsPrint = Exporter.PDF Then
            clsCommon.MyExportToPDF("Bank Cash Book" + IIf(chkDetail.IsChecked, "( Detail )", "( Summary )"), gvReport, arrHeader, "Bank Cash Book", True)
        End If
    End Sub
    Private Sub FormatGrid()
        gvReport.AllowAddNewRow = False
        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        gvReport.EnableFiltering = True
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            gvReport.Columns(ii).IsVisible = False
        Next


        gvReport.Columns("BANK_CODE").IsVisible = True
        gvReport.Columns("BANK_CODE").Width = 121
        gvReport.Columns("BANK_CODE").HeaderText = "Bank Code"

        gvReport.Columns("DESCRIPTION").IsVisible = True
        gvReport.Columns("DESCRIPTION").Width = 251
        gvReport.Columns("DESCRIPTION").HeaderText = "Bank Name"

        If chkDetail.IsChecked = True Then
            gvReport.Columns("Reco Date").IsVisible = True
            gvReport.Columns("Reco Date").Width = 121
            gvReport.Columns("Reco Date").HeaderText = "Reco Date"

            gvReport.Columns("DocNo").IsVisible = True
            gvReport.Columns("DocNo").Width = 151
            gvReport.Columns("DocNo").HeaderText = "Document"

            gvReport.Columns("Entry_Desc").IsVisible = True
            gvReport.Columns("Entry_Desc").Width = 251
            gvReport.Columns("Entry_Desc").HeaderText = "Document Description"

            gvReport.Columns("DocDate").IsVisible = True
            gvReport.Columns("DocDate").Width = 151
            gvReport.Columns("DocDate").HeaderText = "Doc Date"
            gvReport.Columns("DocDate").FormatString = "{0:d}"

            gvReport.Columns("Status").IsVisible = True
            gvReport.Columns("Status").Width = 121
            gvReport.Columns("Status").HeaderText = "Reco.Status"

            gvReport.Columns("CHEQUE_NO").IsVisible = True
            gvReport.Columns("CHEQUE_NO").Width = 121
            gvReport.Columns("CHEQUE_NO").HeaderText = "Cheque/DD No"

            gvReport.Columns("CHEQUE_DATE").IsVisible = False
            gvReport.Columns("CHEQUE_DATE").Width = 151
            gvReport.Columns("CHEQUE_DATE").HeaderText = "Cheque/DD Date"
            gvReport.Columns("CHEQUE_DATE").FormatString = "{0:d}"

            gvReport.Columns("CustVendName").IsVisible = True
            gvReport.Columns("CustVendName").Width = 251
            gvReport.Columns("CustVendName").HeaderText = "Customer/Vendor"

            gvReport.Columns("Cust_Group_Desc").IsVisible = True
            gvReport.Columns("Cust_Group_Desc").Width = 251
            gvReport.Columns("Cust_Group_Desc").HeaderText = "Customer/Vendor Group"

            gvReport.Columns("CUST_CATEGORY_DESC").IsVisible = True
            gvReport.Columns("CUST_CATEGORY_DESC").Width = 251
            gvReport.Columns("CUST_CATEGORY_DESC").HeaderText = "Customer/Vendor Category"

            gvReport.Columns("Cust_Type_Desc").IsVisible = True
            gvReport.Columns("Cust_Type_Desc").Width = 251
            gvReport.Columns("Cust_Type_Desc").HeaderText = "Customer/Vendor Type"

            gvReport.Columns("LOC_NAME").IsVisible = True
            gvReport.Columns("LOC_NAME").Width = 121
            gvReport.Columns("LOC_NAME").HeaderText = "Location"

            gvReport.Columns("BANKGL_Account_Code").IsVisible = True
            gvReport.Columns("BANKGL_Account_Code").Width = 151
            gvReport.Columns("BANKGL_Account_Code").HeaderText = "Bank GL Account"

            gvReport.Columns("BANKGL_Account_Name").IsVisible = True
            gvReport.Columns("BANKGL_Account_Name").Width = 151
            gvReport.Columns("BANKGL_Account_Name").HeaderText = "Bank GL Account Name"

            gvReport.Columns("Status").IsVisible = True
            gvReport.Columns("Status").Width = 50
            gvReport.Columns("Status").HeaderText = "Status"

            gvReport.Columns("TransType").IsVisible = False
            gvReport.Columns("TransType").Width = 50
            gvReport.Columns("TransType").HeaderText = "Transaction Type"

            If gvReport.Columns.Contains("Payment Mode") Then
                gvReport.Columns("Payment Mode").Width = 100
                gvReport.Columns("Payment Mode").IsVisible = True
            End If
        End If

        If Not (chkSummary.IsChecked And chkDocWise.Checked) Then
            gvReport.Columns("BankType").IsVisible = True
            gvReport.Columns("BankType").Width = 121
            gvReport.Columns("BankType").HeaderText = "Bank Type"

            gvReport.Columns("BalAmt").IsVisible = True
            gvReport.Columns("BalAmt").Width = 121
            gvReport.Columns("BalAmt").HeaderText = "Opening Balance"
        End If

        If chkSummary.IsChecked Then
            If chkDocWise.Checked Then
                gvReport.Columns("DocNo").IsVisible = True
                gvReport.Columns("DocNo").Width = 151
                gvReport.Columns("DocNo").HeaderText = "Document"

                gvReport.Columns("DocDate").IsVisible = True
                gvReport.Columns("DocDate").Width = 151
                gvReport.Columns("DocDate").HeaderText = "Doc Date"
                gvReport.Columns("DocDate").FormatString = "{0:d}"

                gvReport.Columns("Status").IsVisible = True
                gvReport.Columns("Status").Width = 50
                gvReport.Columns("Status").HeaderText = "Status"

                If gvReport.Columns.Contains("Payment Mode") Then
                    gvReport.Columns("Payment Mode").Width = 100
                    gvReport.Columns("Payment Mode").IsVisible = True
                End If
            Else
                gvReport.Columns("Closing_Balance").IsVisible = True
                gvReport.Columns("Closing_Balance").Width = 121
                gvReport.Columns("Closing_Balance").HeaderText = "Closing Balance"
            End If
        End If

        gvReport.Columns("Debit_Amount").IsVisible = True
        gvReport.Columns("Debit_Amount").Width = 151
        gvReport.Columns("Debit_Amount").HeaderText = "Dr Amount"
        gvReport.Columns("Debit_Amount").FormatString = "{0:f2}"

        gvReport.Columns("Credit_Amount").IsVisible = True
        gvReport.Columns("Credit_Amount").Width = 151
        gvReport.Columns("Credit_Amount").HeaderText = "Cr Amount"
        gvReport.Columns("Credit_Amount").FormatString = "{0:f2}"


        If Not chkSummary.IsChecked Then
            gvReport.Columns("CummulativeBal").IsVisible = True
            gvReport.Columns("CummulativeBal").Width = 151
            gvReport.Columns("CummulativeBal").HeaderText = "Cummulative Balance"

        End If
        '---------------Total of Debit Amount ANd Credit Amount----- 
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim SUMDrAmt As New GridViewSummaryItem("Debit_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SUMDrAmt)
        Dim SUMCrAmt As New GridViewSummaryItem("Credit_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SUMCrAmt)

        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        '--------------------------------------------------------------------------------------------
        If chkSummary.IsChecked AndAlso clsCommon.CompairString(ddlBankType.Text, "Bank") = CompairStringResult.Equal AndAlso pnlAdminSetting.Visible AndAlso chkReconcile.Checked Then
            gvReport.Columns("SubledgerDrAmt").IsVisible = True
            gvReport.Columns("SubledgerDrAmt").Width = 100
            gvReport.Columns("SubledgerDrAmt").HeaderText = "Subledger Dr Amt"

            gvReport.Columns("SubledgerCrAmt").IsVisible = True
            gvReport.Columns("SubledgerCrAmt").Width = 100
            gvReport.Columns("SubledgerCrAmt").HeaderText = "Subledger Cr Amt"

            Dim emptydr As New GridViewSummaryItem("SubledgerDrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(emptydr)
            Dim emptycr As New GridViewSummaryItem("SubledgerCrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(emptycr)
        End If

    End Sub



    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        FunPrint()
    End Sub

    Public Sub FunPrint()
        RefreshData()
        If dt.Rows.Count > 0 Then
            Dim FRMcrys As New frmCrystalReportViewer
            If chkSummary.IsChecked = True Then
                ''richa agarwal 4 Aapril,2017 addd new report
                '   ExportToExcelGV()

                If chkDocWise.Checked Then

                    FRMcrys.funreport(CrystalReportFolder.CommonServices, dt, "crptBankBookSummaryDocWise", Me.Text)
                Else
                    FRMcrys.funreport(CrystalReportFolder.CommonServices, dt, "crptBankBookSummary", Me.Text)
                End If

            Else
                FRMcrys.funreport(CrystalReportFolder.CommonServices, dt, "crptBankBook", Me.Text)
            End If
        End If
    End Sub

    Public Sub ExportToExcelGV()
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""

            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If chkLocSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Location : " + strTemp)
            End If

            If chkBanksSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgBanks.CheckedValue
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Bank : " + strTemp)
            End If

            clsCommon.MyExportToExcelGrid("Bank Book Summary", gvReport, arrHeader, "Bank-Cash Book")

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub




    'Public Sub funShowReport(ByVal Bank As String, ByVal BankDesc As String, ByVal banktype As String, ByVal frmDate As String, ByVal toDate As String)
    '***************** Old Bank Book *********************************************
    '    Dim strQue As String = "select isnull(sum(amt),0) from (SELECT SUM(Receipt_Amount) as [Amt] FROM TSPL_RECEIPT_HEADER INNER JOIN TSPL_BANK_MASTER ON TSPL_RECEIPT_HEADER.Bank_Code = TSPL_BANK_MASTER.BANK_CODE where Posted='Y'   and TSPL_RECEIPT_HEADER.Bank_Code ='" + Bank + "' and (convert(datetime,Receipt_Post_Date,103) < convert(datetime,'" + frmDate + "',103)) " & _
    '                         " union all" & _
    '                         " SELECT     sum(Payment_Amount)as [Amt] FROM  TSPL_PAYMENT_HEADER INNER JOIN TSPL_BANK_MASTER ON TSPL_PAYMENT_HEADER.Bank_Code = TSPL_BANK_MASTER.BANK_CODE where Posted='P' and TSPL_PAYMENT_HEADER.Bank_Code ='" + Bank + "' and (convert(datetime,Payment_Post_Date,103)  < convert(datetime,'" + frmDate + "',103))" & _
    '                         " union all" & _
    '                         " SELECT sum(Amount)as [Amt] FROM TSPL_BANK_REVERSE WHERE TSPL_BANK_REVERSE.Post ='P' AND TSPL_BANK_REVERSE.Bank_Code ='" + Bank + "' AND" & _
    '                         " (convert(datetime,TSPL_BANK_REVERSE.Reversal_Date ,103) < convert(datetime,'" + frmDate + "',103)))que"
    '    Dim strAmt As String = connectSql.RunScalar(strQue)

    '    Dim strQ As String = " select *,'" + strAmt + "' as [Opening Bal] from (SELECT     Cust_Code, Customer_Name, Entry_Desc, Receipt_Post_Date, Receipt_Amount, Receipt_No, TSPL_RECEIPT_HEADER.Bank_Code ,'C' as [Type],Bank_type ,'" + frmDate + "' as frmDate,'" + toDate + "' as todate,TSPL_BANK_MASTER.DESCRIPTION " & _
    '                         " FROM TSPL_RECEIPT_HEADER INNER JOIN TSPL_BANK_MASTER ON TSPL_RECEIPT_HEADER.Bank_Code = TSPL_BANK_MASTER.BANK_CODE where Posted='Y'  " & _
    '                         " and TSPL_RECEIPT_HEADER.Bank_Code ='" + Bank + "' and (convert(datetime,Receipt_Post_Date,103) >=convert(datetime,'" + frmDate + "',103) and convert(datetime,Receipt_Post_Date,103) <= convert(datetime,'" + toDate + "',103)) " & _
    '                         " union all " & _
    '                         " SELECT     Vendor_Code, Vendor_Name, Entry_Desc, Payment_Post_Date, Payment_Amount, Payment_No,TSPL_PAYMENT_HEADER.Bank_Code ,'V' as [Type],Bank_type ,'" + frmDate + "' as frmDate,'" + toDate + "' as todate,TSPL_BANK_MASTER.DESCRIPTION " & _
    '                        "  FROM  TSPL_PAYMENT_HEADER INNER JOIN TSPL_BANK_MASTER ON TSPL_PAYMENT_HEADER.Bank_Code = TSPL_BANK_MASTER.BANK_CODE where Posted='P'  " & _
    '                        "  and TSPL_PAYMENT_HEADER.Bank_Code ='" + Bank + "' and (convert(datetime,Payment_Post_Date,103) >=convert(datetime,'" + frmDate + "',103) and convert(datetime,Payment_Post_Date,103) <= convert(datetime,'" + toDate + "',103))" & _
    '                        " union all " & _
    '                        " SELECT (SELECT CASE WHEN  Reverse_Document ='Receipts' THEN CUST_CODE ELSE VENDOR_CODE END) AS [SOUCE CODE],(SELECT CASE WHEN  Reverse_Document ='Receipts' THEN CUST_NAME ELSE Vendor_Name  END) AS [SOUCE NAME],Reason AS [DESC],Reversal_Date AS [POST DATE]," & _
    '                        " Amount ,Reverse_Code ,Bank_Code ,(SELECT CASE WHEN  Reverse_Document ='Receipts' THEN 'V' ELSE 'C' END) AS [SOUCE CODE],  " & _
    '                        " (SELECT Bank_type  FROM TSPL_BANK_MASTER WHERE TSPL_BANK_MASTER.BANK_CODE  =TSPL_BANK_REVERSE.Bank_Code) AS [BANK TYPE],'" + frmDate + "' as frmDate,'" + toDate + "' as todate," & _
    '                        " (SELECT DESCRIPTION   FROM TSPL_BANK_MASTER WHERE TSPL_BANK_MASTER.BANK_CODE  =TSPL_BANK_REVERSE.Bank_Code) AS [DESCRIPTION] FROM TSPL_BANK_REVERSE WHERE TSPL_BANK_REVERSE.Post ='P' AND TSPL_BANK_REVERSE.Bank_Code ='AXIS' AND" & _
    '                        " (convert(datetime,TSPL_BANK_REVERSE.Reversal_Date ,103) >=convert(datetime,'" + frmDate + "',103) and convert(datetime,TSPL_BANK_REVERSE.Reversal_Date ,103) <= convert(datetime,'" + toDate + "',103))" & _
    '                       "  union All select '' as [Cust_Code],'Bank Transfer' as [Customer_Name],Description ,Transfer_Posting_Date ,Deposit_Amount ,Transfer_No ,From_Bank_Code ,'V' " & _
    '                       " ,(select bank_type from tspl_bank_master where tspl_bank_master.BANK_CODE =TSPL_BANK_TRANSFER.From_Bank_Code  ) as [Type],'" + frmDate + "' as frmDate,'" + toDate + "' as todate," & _
    '                       " (select tspl_bank_master.DESCRIPTION   from tspl_bank_master where tspl_bank_master.BANK_CODE =TSPL_BANK_TRANSFER.From_Bank_Code  )" & _
    '                       "  as [Description] from tspl_bank_transfer where From_Bank_Code ='AXIS' and Post ='P' AND (convert(datetime,tspl_bank_transfer.Transfer_Posting_Date ,103) >=convert(datetime,'" + frmDate + "',103) and convert(datetime,tspl_bank_transfer.Transfer_Posting_Date ,103)" & _
    '                       "  <= convert(datetime,'" + toDate + "',103)) union All select '' as [Cust_Code],'Bank Transfer' as [Customer_Name],Description ,Transfer_Posting_Date ,Deposit_Amount ,Transfer_No ,To_Bank_Code  ,'C' " & _
    '                       " ,(select bank_type from tspl_bank_master where tspl_bank_master.BANK_CODE =TSPL_BANK_TRANSFER.To_Bank_Code   ) as [Type],'" + frmDate + "' as frmDate,'" + toDate + "' as todate,(select tspl_bank_master.DESCRIPTION " & _
    '                       "  from tspl_bank_master where tspl_bank_master.BANK_CODE =TSPL_BANK_TRANSFER.To_Bank_Code   ) as [Description] from tspl_bank_transfer where To_Bank_Code  ='AXIS' and Post ='P' AND (convert(datetime,tspl_bank_transfer.Transfer_Posting_Date ,103)" & _
    '                       "  >=convert(datetime,'" + frmDate + "',103) and convert(datetime,tspl_bank_transfer.Transfer_Posting_Date ,103) <= convert(datetime,'" + toDate + "',103)) ) Query"
    '************************ End Old Bank Book
    '    Dim bankWithOutLoc As String = Bank.Substring(0, 4)
    '    Dim bankWithOutLoc As String = Bank
    '    Dim strQ As String = "select '" + BankDesc + "' as [BankDesc], '" + Bank + "' as [grp], xxx.*,'" + frmDate + "'as dtfrom,'" + toDate + "'as dttodate,isnull((select SUM(ISNULL(subJD.Amount,0))from TSPL_JOURNAL_DETAILS as subJD  where convert(date,subJD.Voucher_Date ,103)< convert(date,'" + frmDate + "',103)  and subJD.Account_code = xxx.Account_code),0) as opening_balance,case when xxx.Source_Code IN ('AP-MI','AP-PI','AP-PY')then (select max(Cheque_No)  from TSPL_PAYMENT_HEADER  where TSPL_PAYMENT_HEADER .Payment_No =xxx.Source_Doc_No ) else case when xxx.Source_Code IN ('AR-PY','AR-PI','AR-MI')then (select max(Cheque_No) from TSPL_RECEIPT_HEADER   where TSPL_RECEIPT_HEADER  .Receipt_No  =xxx .Source_Doc_No )else '' end end as Cheque_num,case when xxx.Source_Code IN ('AP-MI','AP-PI','AP-PY')then (select max(Cheque_Date)  from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER .Payment_No =xxx.Source_Doc_No) else case when xxx.Source_Code IN ('AR-PY','AR-MI','AR-MI')then (select max(Cheque_Date)  from TSPL_RECEIPT_HEADER   where TSPL_RECEIPT_HEADER  .Receipt_No  =xxx.Source_Doc_No  )else '' end end as Cheque_Date from (SELECT TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date,tspl_journal_master.CustVend_Code,TSPL_JOURNAL_MASTER .CustVend_name,(TSPL_JOURNAL_MASTER.Voucher_Desc+'/'+TSPL_JOURNAL_MASTER.Remarks+'/'+TSPL_JOURNAL_MASTER .Comments) as Description1,TSPL_JOURNAL_MASTER.Source_Code,TSPL_JOURNAL_MASTER.Source_doc_No ,TSPL_JOURNAL_MASTER.Posting_Date,'" + banktype + "' as [BankType] ,TSPL_JOURNAL_MASTER.Total_Debit_Amt,TSPL_JOURNAL_MASTER.Total_Credit_Amt, TSPL_JOURNAL_DETAILS.Account_code,TSPL_JOURNAL_DETAILS.Account_Desc, TSPL_JOURNAL_DETAILS.Amount,case when TSPL_JOURNAL_DETAILS.Amount<0 then -1 * TSPL_JOURNAL_DETAILS.Amount else 0 end as CrAmt,case when TSPL_JOURNAL_DETAILS.Amount<0 then 0 else  TSPL_JOURNAL_DETAILS.Amount end as DrAmt , TSPL_JOURNAL_DETAILS.Description, TSPL_JOURNAL_DETAILS.Reference,TSPL_JOURNAL_DETAILS.Posting_Date AS [Dtline PostDt], TSPL_JOURNAL_DETAILS.Detail_Line_No,   TSPL_GL_ACCOUNTS.Account_Group_Code,TSPL_COMPANY_MASTER .Comp_Name,(TSPL_COMPANY_MASTER .Add1+TSPL_COMPANY_MASTER .Add2+TSPL_COMPANY_MASTER.Add3+','+TSPL_COMPANY_MASTER .State+'-'+TSPL_COMPANY_MASTER .Pincode)as compaaddress,TSPL_COMPANY_MASTER .logo_img,TSPL_COMPANY_MASTER .logo_img2 FROM TSPL_JOURNAL_MASTER  INNER JOIN    TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No INNER JOIN    TSPL_GL_ACCOUNTS ON TSPL_JOURNAL_DETAILS.Account_code = TSPL_GL_ACCOUNTS.Account_Code  left join TSPL_COMPANY_MASTER on TSPL_JOURNAL_MASTER .Comp_Code =TSPL_COMPANY_MASTER .Comp_Code    WHERE  TSPL_JOURNAL_MASTER.Authorized ='A' and  convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)>=convert(date,'" + frmDate + "',103) and convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)<=convert(date,'" + toDate + "',103) and  TSPL_JOURNAL_MASTER.Source_Code not in ('AR-MI','AP-MI'  )  and  TSPL_JOURNAL_DETAILS.Account_code = '" + bankWithOutLoc + "')as xxx order by convert(date,Voucher_Date ,103),Voucher_No"
    '    Dim strQMisc As String = "SELECT '" + BankDesc + "' as [BankDesc], '" + Bank + "' as [grp],(select 0.00) as opening_balance,(case when TSPL_JOURNAL_MASTER.Source_Code='AP-MI' then (select Cheque_No  from TSPL_PAYMENT_HEADER where Payment_No =TSPL_JOURNAL_MASTER.Source_doc_No) when TSPL_JOURNAL_MASTER.Source_Code='AR-MI' then (select Cheque_No from TSPL_RECEIPT_HEADER  where Receipt_No =TSPL_JOURNAL_MASTER.Source_doc_No)  end) as Cheque_num , (case when TSPL_JOURNAL_MASTER.Source_Code='AP-MI' then (select Cheque_Date  from TSPL_PAYMENT_HEADER where Payment_No =TSPL_JOURNAL_MASTER.Source_doc_No) when TSPL_JOURNAL_MASTER.Source_Code='AR-MI' then (select Cheque_Date from TSPL_RECEIPT_HEADER  where Receipt_No =TSPL_JOURNAL_MASTER.Source_doc_No)  end) as Cheque_Date, TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date,isnull(TSPL_JOURNAL_DETAILS.Description,'') as Description1,TSPL_JOURNAL_MASTER.Source_Code,TSPL_JOURNAL_MASTER.Source_doc_No ,TSPL_JOURNAL_MASTER.Posting_Date,'" + banktype + "' as [BankType] ,TSPL_JOURNAL_MASTER.Total_Debit_Amt,TSPL_JOURNAL_MASTER.Total_Credit_Amt, TSPL_JOURNAL_DETAILS.Account_code as CustVend_Code,TSPL_JOURNAL_DETAILS.Account_Desc as CustVend_name,TSPL_JOURNAL_DETAILS.Account_Desc,case when Source_Code='AP-MI' then TSPL_JOURNAL_DETAILS.Amount*-1 when Source_Code='AR-MI' then TSPL_JOURNAL_DETAILS.Amount*-1 else TSPL_JOURNAL_DETAILS.Amount end as Amount  ,case when TSPL_JOURNAL_DETAILS.Amount>0 then -1 * TSPL_JOURNAL_DETAILS.Amount else 0 end as CrAmt,case when TSPL_JOURNAL_DETAILS.Amount>0 then 0 else  TSPL_JOURNAL_DETAILS.Amount end as DrAmt , TSPL_JOURNAL_DETAILS.Description, TSPL_JOURNAL_DETAILS.Reference,TSPL_JOURNAL_DETAILS.Posting_Date AS [Dtline PostDt], TSPL_JOURNAL_DETAILS.Detail_Line_No,   TSPL_GL_ACCOUNTS.Account_Group_Code,TSPL_COMPANY_MASTER .Comp_Name,(TSPL_COMPANY_MASTER .Add1+TSPL_COMPANY_MASTER .Add2+TSPL_COMPANY_MASTER.Add3+','+TSPL_COMPANY_MASTER .State+'-'+TSPL_COMPANY_MASTER .Pincode)as compaaddress,TSPL_COMPANY_MASTER .logo_img,TSPL_COMPANY_MASTER .logo_img2,'" + frmDate + "'as dtfrom,'" + toDate + "'as dttodate FROM TSPL_JOURNAL_MASTER  INNER JOIN    TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No INNER JOIN    TSPL_GL_ACCOUNTS ON TSPL_JOURNAL_DETAILS.Account_code = TSPL_GL_ACCOUNTS.Account_Code  left join TSPL_COMPANY_MASTER on TSPL_JOURNAL_MASTER .Comp_Code =TSPL_COMPANY_MASTER .Comp_Code    WHERE  TSPL_JOURNAL_MASTER.Authorized ='A' and  convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)>=convert(date,'" + frmDate + "',103) and convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)<=convert(date,'" + toDate + "',103) and  TSPL_JOURNAL_DETAILS.Account_code  <> '" + bankWithOutLoc + "' and (Source_Code='AP-MI' or Source_Code='AR-MI') and TSPL_JOURNAL_MASTER.Voucher_No in (select distinct Voucher_No  from TSPL_JOURNAL_DETAILS  where  TSPL_JOURNAL_DETAILS.Account_code = '" + bankWithOutLoc + "') order by convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103),Voucher_No"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQ)
    '    Dim dtMisc As DataTable = clsDBFuncationality.GetDataTable(strQMisc)
    '    dt.Merge(dtMisc)
    '    Try
    '        CommonServicesViewer.funreport(dt, "crptBankBook", Me.Text)
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try


    'End Sub
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    Private Function funSetUserAccess() As Boolean
        Try

            Dim strRights As String
            Dim strTemp() As String
            Dim strProgCode = "BANK-BK-RPT"
            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
            strTemp = Split(strRights, ",")
            If strTemp(0) = "0" Then
                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
                funSetUserAccess = False
                blnRead = False
                Me.Close()
                Exit Function
            Else
                blnRead = True
            End If
            If strTemp(1) = "0" Then 'Grant modify access

            End If
            If strTemp(2) = "0" Then 'Grant modify access

            End If

            funSetUserAccess = True
        Catch er As Exception
            myMessages.myExceptions(er)
            Return False
        End Try
        Return True
    End Function

    Private Sub chkBanksAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBanksAll.ToggleStateChanged
        cbgBanks.Enabled = False
    End Sub

    Private Sub chkBanksSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBanksSelect.ToggleStateChanged
        cbgBanks.Enabled = True
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub


    'Private Sub chkBank_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBank.ToggleStateChanged
    '    LoadBanks()
    'End Sub

    'Private Sub chkCash_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCash.ToggleStateChanged
    '    LoadBanks()
    'End Sub

    Private Sub FrmBankBook_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPrintFlag Then
            FunPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
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
    '--- Added By abhishek As on 23 Aug 2012 For Multiple Bank Type According to Amit sir
    Private Sub ddlBankType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlBankType.SelectedIndexChanged
        LoadBanks()
    End Sub

    Private Sub chkReconcile_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkReconcile.ToggleStateChanged
        If chkReconcile.Checked Then
            ddlBankType.Text = "Bank"
            chkSummary.IsChecked = True
        End If
    End Sub

    Private Sub chkSummary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSummary.ToggleStateChanged
        chkDocWise.Checked = False
        chkDocWise.Visible = chkSummary.IsChecked
    End Sub

    Private Sub gvReport_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvReport.CellDoubleClick
        If chkDetail.IsChecked Or chkDocWise.Checked Then
            If clsCommon.CompairString(gvReport.CurrentRow.Cells("TransType").Value, "Receipt") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, gvReport.CurrentRow.Cells("DocNo").Value)
            ElseIf clsCommon.CompairString(gvReport.CurrentRow.Cells("TransType").Value, "BankTransfer") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.bankTransfer, gvReport.CurrentRow.Cells("DocNo").Value)
            ElseIf clsCommon.CompairString(gvReport.CurrentRow.Cells("TransType").Value, "Payment") = CompairStringResult.Equal Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, gvReport.CurrentRow.Cells("DocNo").Value)
            End If
        End If

    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmBankBook & "'"))
            If chkBanksSelect.IsChecked Then
                Dim stVSPName As String = ""
                For Each StrName As String In cbgBanks.CheckedDisplayMember
                    If clsCommon.myLen(stVSPName) > 0 Then
                        stVSPName += ", "
                    End If
                    stVSPName += StrName
                Next
                Dim strVSPCode As String = ""
                For Each StrCode As String In cbgBanks.CheckedValue
                    If clsCommon.myLen(strVSPCode) > 0 Then
                        strVSPCode += ", "
                    End If
                    strVSPCode += StrCode
                Next
                arrHeader.Add(("Bank: " + stVSPName + " "))
            End If
            If chkLocSelect.IsChecked Then
                Dim stVSPName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(stVSPName) > 0 Then
                        stVSPName += ", "
                    End If
                    stVSPName += StrName
                Next
                Dim strVSPCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strVSPCode) > 0 Then
                        strVSPCode += ", "
                    End If
                    strVSPCode += StrCode
                Next
                arrHeader.Add(("Location: " + stVSPName + " "))
            End If
            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gvReport, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gvReport, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    '================updated by Preeti gupta Against ticket No [BM00000007967]
    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvReport.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvReport.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvReport.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
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
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)

    End Sub

    Private Sub QExpExcel_Click(sender As Object, e As EventArgs) Handles QExpExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmBankBook & "'"))
            If chkBanksSelect.IsChecked Then
                Dim stVSPName As String = ""
                For Each StrName As String In cbgBanks.CheckedDisplayMember
                    If clsCommon.myLen(stVSPName) > 0 Then
                        stVSPName += ", "
                    End If
                    stVSPName += StrName
                Next
                Dim strVSPCode As String = ""
                For Each StrCode As String In cbgBanks.CheckedValue
                    If clsCommon.myLen(strVSPCode) > 0 Then
                        strVSPCode += ", "
                    End If
                    strVSPCode += StrCode
                Next
                arrHeader.Add(("Bank: " + stVSPName + " "))
            End If
            If chkLocSelect.IsChecked Then
                Dim stVSPName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(stVSPName) > 0 Then
                        stVSPName += ", "
                    End If
                    stVSPName += StrName
                Next
                Dim strVSPCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strVSPCode) > 0 Then
                        strVSPCode += ", "
                    End If
                    strVSPCode += StrCode
                Next
                arrHeader.Add(("Location: " + stVSPName + " "))
            End If
            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = "Bank Cash Book" + IIf(chkDetail.IsChecked, "( Detail )", "( Summary )")
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gvReport, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(gvReport, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub QExpCSV_Click(sender As Object, e As EventArgs) Handles QExpCSV.Click
        Try
            If gvReport Is Nothing OrElse gvReport.RowCount <= 0 Then
                MsgBox("Grid is empty!!!")
                Exit Sub
            End If
            ExportCSV(gvReport, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub ExportCSV(ByVal sender As RadGridView, Optional ByVal AddHeader As Boolean = False)
        Try
            '', ByVal FileName As String, 

            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            clsCommon.ProgressBarShow()
            IO.File.WriteAllLines(filePath, transportSql.ExportCSV(sender, AddHeader))
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
            Process.Start(filePath)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmBankBook & "'"))
            If chkBanksSelect.IsChecked Then
                Dim stVSPName As String = ""
                For Each StrName As String In cbgBanks.CheckedDisplayMember
                    If clsCommon.myLen(stVSPName) > 0 Then
                        stVSPName += ", "
                    End If
                    stVSPName += StrName
                Next
                Dim strVSPCode As String = ""
                For Each StrCode As String In cbgBanks.CheckedValue
                    If clsCommon.myLen(strVSPCode) > 0 Then
                        strVSPCode += ", "
                    End If
                    strVSPCode += StrCode
                Next
                arrHeader.Add(("Bank: " + stVSPName + " "))
            End If
            If chkLocSelect.IsChecked Then
                Dim stVSPName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(stVSPName) > 0 Then
                        stVSPName += ", "
                    End If
                    stVSPName += StrName
                Next
                Dim strVSPCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strVSPCode) > 0 Then
                        strVSPCode += ", "
                    End If
                    strVSPCode += StrCode
                Next
                arrHeader.Add(("Location: " + stVSPName + " "))
            End If
            
            transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Bank Cash Book" + IIf(chkDetail.IsChecked, "( Detail )", "( Summary )"), gvReport, arrHeader, "Bank Cash Book", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
