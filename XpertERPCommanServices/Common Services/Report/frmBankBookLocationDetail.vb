
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
Public Class FrmBankBookLocationDetail
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBankBookLocationDetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
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

    End Sub

    
    Sub LoadBanks()
        Dim qry As String = ""

        If ddlBankType.Text = "Bank" Then
            qry = " select BANK_CODE  ,DESCRIPTION  from TSPL_BANK_MASTER Where Bank_type='B'"
        ElseIf ddlBankType.Text = "Cash" Then
            qry = " select BANK_CODE  ,DESCRIPTION  from TSPL_BANK_MASTER Where Bank_type ='C'"
        ElseIf ddlBankType.Text = "Petty Cash" Then
            qry = " select BANK_CODE  ,DESCRIPTION  from TSPL_BANK_MASTER Where Bank_type ='P'"
        ElseIf ddlBankType.Text = "Settlement" Then
            qry = " select BANK_CODE  ,DESCRIPTION  from TSPL_BANK_MASTER Where Bank_type ='S'"
        End If

        cbgBanks.ValueMember = "BANK_CODE"
        cbgBanks.DisplayMember = "DESCRIPTION"
        cbgBanks.DataSource = clsDBFuncationality.GetDataTable(qry)

    End Sub

    Sub LoadLocation()
        Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub
    Sub funreset()
        dtFrm.Value = System.DateTime.Now.Date
        dtTo.Value = System.DateTime.Now.Date
        LoadBanks()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        ddlBankType.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
        ddRecoStatus.SelectedIndex = 0
        gvReport.DataSource = Nothing
        gvReport.Rows.Clear()
        gvReport.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = reportid()
        TemplateGridview = gvReport
        RefreshData()
    End Sub

    Private Function reportid()
        Dim report_id As String = ""
        If chkSummary.IsChecked = True Then
            report_id = MyBase.Form_ID + "S"
            If chkDocWise.Checked = True Then
                report_id = report_id + "DOC"
            End If
        Else
            report_id = MyBase.Form_ID + "D"
        End If
        Return report_id
    End Function

    Public Sub RefreshData()
        Try
            If chkBanksSelect.IsChecked = True AndAlso cbgBanks.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select Atleast One Bank OR Select All")
            End If
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                Throw New Exception("Please Select Atleast One Location OR Select All")
            End If

            Dim strAddress As String
            If chkBanksSelect.IsChecked AndAlso cbgBanks.CheckedValue.Count = 1 Then
                strAddress = "(Select  MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_STATE_MASTER.State_Name) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end) from TSPL_LOCATION_MASTER LEFT OUTER  JOIN TSPL_STATE_MASTER ON TSPL_LOCATION_MASTER.State=TSPL_STATE_MASTER.State_Code LEFT OUTER JOIN TSPL_BANK_MASTER ON RIGHT(TSPL_BANK_MASTER.BANKACC,3)=TSPL_LOCATION_MASTER.Loc_Segment_Code Where TSPL_BANK_MASTER.Bank_Code in (" + clsCommon.GetMulcallString(cbgBanks.CheckedValue) + "))  "
            Else
                strAddress = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "
            End If

            Dim strSelectedBank As String = ""
            If chkBanksSelect.IsChecked = True AndAlso cbgBanks.CheckedValue.Count > 0 Then
                strSelectedBank = clsCommon.GetMulcallString(cbgBanks.CheckedValue)
            End If

            Dim strSelectedLocation As String = ""
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                strSelectedLocation = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
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

            Dim Qry As String = clsBankReco.GetBankBookLocationDetailQuery(dtFrm.Value, dtTo.Value, strSelectedBank, strSelectedLocation, strAddress, chrStatus, chrBankType, rptHead, rptRecoStatus, False)
            If chkSummary.IsChecked Then
                If chkDocWise.Checked Then
                    Qry = "Select BANK_CODE, MAX(DESCRIPTION) as DESCRIPTION, DocNo, MAX(DocDate) as DocDate, SUM(Debit_Amount ) as Debit_Amount, SUM(Credit_Amount ) as Credit_Amount,DocType, MAX(Status) as Status from ( " + Qry + " ) YYY Where ISNULL(DocNo, '')<>''  Group By BANK_CODE, DocNo,DocType Order By Convert(date,MAX(DocDate) , 103), DocNo "
                Else
                    Qry = "SELECT BANK_CODE, MAX(DESCRIPTION) AS [DESCRIPTION], MAX(BankType) AS BankType,MAX(Startdate) AS Startdate,MAX(EndDate) AS EndDate,MAX(RunDate) AS RunDate,SUM(BalAmt) AS BalAmt,SUM(Debit_Amount) AS Debit_Amount ,SUM(Credit_Amount) AS Credit_Amount,(SUM(Debit_Amount)-SUM(Credit_Amount)+SUM(BalAmt)) AS Closing_Balance   FROM (" + Qry + ")POP GROUP BY BANK_CODE ORDER BY  BANK_CODE "
                End If
            ElseIf chkDetail.IsChecked Then
                Qry = "WITH CTETemp as (" + Environment.NewLine & _
                    "Select YYY.DocType, YYY.rptHeading, YYY.NARR_MASTER, YYY.NARR_DETAIL, YYY.RunDate, YYY.Startdate, YYY.EndDate,convert(varchar,yyy.Reconciliation_Date,103) as [Reco Date], YYY.BANK_CODE, YYY.BankType, YYY.DESCRIPTION, YYY.DocNo, YYY.Entry_Desc, convert(varchar,YYY.DocDate,103)as DocDate, YYY.CHEQUE_NO, YYY.CHEQUE_DATE, YYY.CustVendorCode, YYY.CustVendName, YYY.Source_Code, YYY.Source_Name, YYY.Loc_Code, YYY.Loc_Name,YYY.[Payee Location Code] ,YYY.[Payee Location Name] , YYY.BANKGL_account_Code, YYY.BANKGL_Account_Name, YYY.BalAmt, YYY.Balance, YYY.Debit_Amount, YYY.Credit_Amount, YYY.CummulativeBal, YYY.Status, YYY.Logo_Img, YYY.Logo_Img2, YYY.Add1, YYY.CompName, YYY.TransType, YYY.Type, ROW_NUMBER() OVER (Partition By YYY.Bank_Code ORDER BY CONVERT(Date,YYY.DocDate,103),DocNo) as [RowNo] From (" + Environment.NewLine & _
                "" & Qry & "" + Environment.NewLine & _
                " ) YYY" + Environment.NewLine & _
                ")" + Environment.NewLine & _
            " Select DocType, rptHeading, NARR_MASTER, NARR_DETAIL, RunDate, Startdate, EndDate,[Reco Date], BANK_CODE, BankType, DESCRIPTION, DocNo, Entry_Desc, DocDate, CHEQUE_NO, CHEQUE_DATE, CustVendorCode, CustVendName, CustomerVendor_Master.Cust_Group_Desc, CustomerVendor_Master.Cust_Type_Desc, CustomerVendor_Master.CUST_CATEGORY_DESC, Source_Code, Source_Name, Loc_Code, Loc_Name,[Payee Location Code] , (SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code =isnull([Payee Location Code],'') and Seg_No =7  ) [Payee Location Name] , BANKGL_account_Code, BANKGL_Account_Name, Debit_Amount, Credit_Amount, BalAmt," + Environment.NewLine & _
            " Balance, (sum(Balance) over (partition by Bank_Code order by RowNo)) as CummulativeBal, Status, Logo_Img, Logo_Img2, Add1, CompName, TransType, RowNo" + Environment.NewLine & _
            "from CTETemp" + Environment.NewLine & _
                " LEFT OUTER JOIN (select TSPL_CUSTOMER_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Cust_Group_Code, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, TSPL_CUSTOMER_MASTER.Cust_Category_Code, TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC, TSPL_CUSTOMER_MASTER.Cust_Type_Code, TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, 'Customer' as [Type] from TSPL_CUSTOMER_MASTER" + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.Cust_Category_Code" + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER on TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code=TSPL_CUSTOMER_MASTER.Cust_Type_Code" + Environment.NewLine & _
                " UNION ALL" + Environment.NewLine & _
                " select TSPL_VENDOR_MASTER.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Group_Code, TSPL_VENDOR_GROUP.Group_Desc as Cust_Group_Desc, '' as Cust_Category_Code, '' as CUST_CATEGORY_DESC, '' as Cust_Type_Code, '' as Cust_Type_Desc, 'Vendor' as [Type] from TSPL_VENDOR_MASTER" + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.Ven_Group_Code=TSPL_VENDOR_MASTER.Vendor_Group_Code) CustomerVendor_Master on CustomerVendor_Master.Type=CTETemp.Type AND CustomerVendor_Master.Cust_Code=CTETemp.CustVendorCode" + Environment.NewLine & _
                "  ORDER BY RowNo"
                ''convert(date,docdate,103),DocNo"
                'Qry += " Order By Bank_Code, Convert(Date,SourceDoc_date, 103), DocNo"
            End If

            dt = clsDBFuncationality.GetDataTable(Qry)

      
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
            gvReport.Columns("CHEQUE_NO").HeaderText = "Cheque No"

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

            gvReport.Columns("LOC_Code").IsVisible = True
            gvReport.Columns("LOC_Code").Width = 121
            gvReport.Columns("LOC_Code").HeaderText = "Location Code"


            gvReport.Columns("LOC_NAME").IsVisible = True
            gvReport.Columns("LOC_NAME").Width = 121
            gvReport.Columns("LOC_NAME").HeaderText = "Location"

            gvReport.Columns("Payee Location Code").IsVisible = True
            gvReport.Columns("Payee Location Code").Width = 151
            gvReport.Columns("Payee Location Code").HeaderText = "Payee Location Code"
            gvReport.Columns("Payee Location Code").WrapText = True


            gvReport.Columns("Payee Location Name").IsVisible = True
            gvReport.Columns("Payee Location Name").Width = 151
            gvReport.Columns("Payee Location Name").HeaderText = "Payee Location Name"
            gvReport.Columns("Payee Location Name").WrapText = True

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
        gvReport.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
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
            If chkSummary.IsChecked = True Then
                ExportToExcelGV()
            Else
                Dim FRMcrys As New frmCrystalReportViewer
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


    Private Sub FrmBankBook_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
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

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        If (gvReport.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        'LoadExcel(Exporter.Excel)
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        If (gvReport.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        'LoadExcel(Exporter.PDF)
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmBankBookLocationDetail & "'"))
            arrHeader.Add("Report Type : " + clsCommon.myCstr(ddlBankType.Text))
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
                'transportSql.exportdataChilRows(gvReport, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Bank Cash Book" + IIf(chkDetail.IsChecked, "( Detail )", "( Summary )"), gvReport, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gvReport.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvReport.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvReport.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)

    End Sub
End Class
