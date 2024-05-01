
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI

Public Class frmCustomerLedgerVsAgeing
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isExportToExcel As Boolean = False
    Dim btnrefresh As Boolean = False
    Dim ReportID As String = String.Empty
    Dim dtCustGrp As DataTable
    Dim dtCustomer As DataTable
    Dim dtMain As DataTable
    Dim dtOpening As DataTable
    Dim dvTemp As DataView
    Dim VisibleGrid As Integer = 0
    Dim FormType As String = Nothing
    Dim strQry As String = ""
    Dim IsDrillDown As Boolean = False
    Dim BackProcess As Boolean = False
    Dim strtempBaseQryforopening As String = String.Empty
    Dim dvTemp1 As DataView = Nothing
    Dim isRunDoubleClick As Boolean = False
    Dim AllowtoSHOWParentChildCustomer As Boolean = False
#End Region

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FormType = formid
    End Sub

    Private Sub frmCustomerLedgerVsAgeing_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPrintFlag Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()

        End If
    End Sub

    Private Sub frmCustomerLedgerVsAgeing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadCurrencyType()
        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        chkActive.Checked = True
        ChkISParentCust.Checked = False
        GrpIsParent.Enabled = False
        chkCumulativeClosing.Checked = True
        ChkDocSumm.Enabled = False
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        gvCustomer.Dock = DockStyle.Fill

        gvCustomer.Visible = False
        TxtSecurity.Enabled = False


        ReportID = GetReportID()
        btnPrint.Enabled = False
        RadPageView1.SelectedPage = RadPageViewPage1
        isRunDoubleClick = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoubleClickOnVC, clsFixedParameterCode.DoubleClickOnVC, Nothing)) = 1, True, False)
        chkIncludeApplyDocument.Checked = False
        AllowtoSHOWParentChildCustomer = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSHOWParentChildCustomer, clsFixedParameterCode.AllowtoSHOWParentChildCustomer, Nothing)) = 1, True, False))
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnQExport.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        isExportToExcel = False
        GC.Collect()

        print()

        ' print()
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

    Sub print(Optional ByVal BulkExport As Integer = 0)
        Dim CompanyAdd As String = String.Empty
        Dim compname As String = String.Empty
        Dim qry As String = String.Empty
        Dim CheckCustomer As String = String.Empty
        Dim FilterForLevels As String = String.Empty
        Dim FilterForDetail As String = String.Empty
        Dim ACodeFilter As String = String.Empty
        Dim strcustomerfilter As String = String.Empty
        Dim strFIlterCheck As String = String.Empty
        Dim StrDocWiseFilter As String = String.Empty
        Dim BaseQry As String = String.Empty
        Dim BaseQryOPENINGINCASEOFMIS As String = String.Empty
        Dim strsecurity As String = String.Empty

        Dim strInnerQry As String = String.Empty
        Dim strAgeingQry As String = String.Empty
        Dim strFinalQry As String = String.Empty
        Dim isonduedate As String = "DocumentDate"
        Dim strLedgerQry As String = String.Empty
        Try
            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Customer", Me.Text)
                Return
            End If
            If chkCompanySelect.IsChecked AndAlso cbgCompany.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one company", Me.Text)
                Return
            End If
            If chkCustGrpSelect.IsChecked AndAlso cbgCustGrp.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Customer Group", Me.Text)
                Return
            End If
            '' Anubhooti 30-Sep-2014 BM00000003557
            If ChkISParentCust.Checked = True Then
                If ChkParentCustSelect.IsChecked AndAlso cbgParentCust.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select at least one parent customer", Me.Text)
                    Return
                End If
            End If

            ''
            If ChkCustTypeSelect.IsChecked AndAlso cbgcusttype.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Customer Type", Me.Text)
                Return
            End If
            If ChkCustCatSelect.IsChecked AndAlso cbgcustcat.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Customer Category", Me.Text)
                Return
            End If


            Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            compname = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER WHERE Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            Dim runDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy hh:mm tt")

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                CompanyAdd = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                qry = "select top(1) Add1+' '+Add2+' '+Add3  from TSPL_LOCATION_MASTER  where Location_Type ='Physical' and Loc_Segment_Code  in ( " + CompanyAdd + ") "
                CompanyAdd = clsDBFuncationality.getSingleValue(qry)
            Else
                qry = "select top(1) Add1+' '+Add2+' '+Add3  from TSPL_COMPANY_MASTER where Comp_Code in ( '" + objCommonVar.CurrentCompanyCode + "') "
                CompanyAdd = clsDBFuncationality.getSingleValue(qry)
            End If
            If chkActive.Checked Then
                CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='N'"
            ElseIf chkInactive.Checked Then
                CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='Y'"
            End If

            If btnrefresh = True Then
                If TxtSecurity.arrValueMember IsNot Nothing AndAlso TxtSecurity.arrValueMember.Count > 0 Then
                    strsecurity = clsCommon.GetMulcallString(TxtSecurity.arrValueMember)
                Else
                    strsecurity = ""
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    strcustomerfilter = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                End If

                If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                    BaseQry = clsCustomerMaster.GetCustomerBaseQry(chkItemWise.Checked, ChkSecurity.Checked, strsecurity, False, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), strcustomerfilter, False, strFromDate, strToDate, False, True, chkIncludeApplyDocument.Checked)
                    BaseQryOPENINGINCASEOFMIS = clsCustomerMaster.GetCustomerBaseQry(chkItemWise.Checked, ChkSecurity.Checked, strsecurity, False, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), strcustomerfilter, True, strFromDate, strToDate, False, False, chkIncludeApplyDocument.Checked)
                Else
                    BaseQry = clsCustomerMaster.GetCustomerBaseQry(chkItemWise.Checked, ChkSecurity.Checked, strsecurity, False, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), strcustomerfilter, False, strFromDate, strToDate, False, False, chkIncludeApplyDocument.Checked)
                    BaseQryOPENINGINCASEOFMIS = clsCustomerMaster.GetCustomerBaseQry(chkItemWise.Checked, ChkSecurity.Checked, strsecurity, False, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), strcustomerfilter, True, strFromDate, strToDate, False, False, chkIncludeApplyDocument.Checked)
                End If
                ''richa Agarwal 27 Oct,2017 
                Dim AllowTrasactionFilterOnCustomerLedger As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowTransactionFiltersOnCustomerlegder, clsFixedParameterCode.AllowTransactionFiltersOnCustomerlegder, Nothing)) = 1, True, False))
                If AllowTrasactionFilterOnCustomerLedger = True Then
                    BaseQry += " WHERE 1=1"
                    BaseQryOPENINGINCASEOFMIS += " WHERE 1=1"

                    Dim StrCONDITION As String = String.Empty
                    If clsCommon.myLen(StrCONDITION) > 0 Then
                        StrCONDITION += "'EXC'"

                        BaseQry += " AND InnQuery.DocType in (" & StrCONDITION & ") "
                        BaseQryOPENINGINCASEOFMIS += " AND InnQuery.DocType in (" & StrCONDITION & ") "
                    Else
                        BaseQry += " AND InnQuery.DocType in ('') "
                        BaseQryOPENINGINCASEOFMIS += " AND InnQuery.DocType in ('') "
                    End If
                End If
                ''-------------------------

                If Not ChkISParentCust.Checked = True Then
                    If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                        If AllowtoSHOWParentChildCustomer = True Then
                            strFIlterCheck += "and ( ACode in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") OR isnull(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'') in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + "))"
                        Else
                            strFIlterCheck += "and ACode in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                        End If
                        If ChkDocWise.Checked = True Then
                            StrDocWiseFilter += " AND CASE WHEN ISNULL([Child Cust Code],'')<>'' THEN [Child Cust Code] ELSE CustCode END IN (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                        End If
                    End If
                End If

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    If chkItemWise.Checked Then
                        strFIlterCheck += "and Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
                    Else
                        strFIlterCheck += "and Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
                    End If
                End If

                Dim dtCustGroup As DataTable = clsDBFuncationality.GetDataTable("select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "'")

                If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 And txtCustomerGroup.arrValueMember Is Nothing Then
                    strFIlterCheck += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "')"
                ElseIf txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")  "
                End If

                If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 And txtCustomer.arrValueMember Is Nothing Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Code in (select DISTINCT Cust_Code from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL WHERE User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "' "
                    If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                        strFIlterCheck += " and TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ") ) "
                    Else
                        strFIlterCheck += " ) "
                    End If
                End If

                If TxtZone.arrValueMember IsNot Nothing AndAlso TxtZone.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(TxtZone.arrValueMember) + ")  "
                End If

                If txtCustomerType.arrValueMember IsNot Nothing AndAlso txtCustomerType.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Type_Code in (" + clsCommon.GetMulcallString(txtCustomerType.arrValueMember) + ")  "
                End If
                If txtCustomerCategory.arrValueMember IsNot Nothing AndAlso txtCustomerCategory.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Category_Code in (" + clsCommon.GetMulcallString(txtCustomerCategory.arrValueMember) + ")  "
                End If

                If rbtnCustWise.Checked Then
                    dtCustomer = New DataTable
                    strLedgerQry = "Select  MAX(xxx.Cust_Group_Code) AS Cust_Group_Code,  ACode,  MAX(AName) as AName,MAX(xxx.Zone_code) as Zone_code, SUM(convert(decimal(18,2),OpngBal)) as OpngBal, SUM(convert(decimal(18,2),DrAmt)) as DrAmt, SUM(convert(decimal(18,2),CrAmt)) as CrAmt, ( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as LedgerAmt,0 as AgeingAmt From (" + Environment.NewLine &
                    " Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code,case when isnull(MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No),'')='' then ACode  else MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No)  end  as ParentCode,  max(Child) as Child, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName,"
                    strLedgerQry += " isnull(max(TSPL_CUSTOMER_MASTER.Zone_Code),'') as Zone_code,isnull(max(TSPL_CUSTOMER_MASTER.Route_No ),'') as Route_No, '' as CurrencyCode, null as ConvRate, SUM(DrAmt*" & ddlCurrencyType.SelectedValue & ")-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote, 0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from ( " + BaseQryOPENINGINCASEOFMIS + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY ACode" + Environment.NewLine &
                                        Environment.NewLine + " UNION ALL" + Environment.NewLine &
                                        " Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, case when isnull(MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No),'')='' then ACode  else MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No)  end  as ParentCode,  max(Child) as Child,ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, "

                    strLedgerQry += " isnull(max(TSPL_CUSTOMER_MASTER.Zone_Code),'') as Zone_code,isnull(max(TSPL_CUSTOMER_MASTER.Route_No ),'') as Route_No,MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate, 0 as OpngBal, SUM(convert(decimal(18,2),DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as DrAmt, " + Environment.NewLine &
                                                            "SUM(convert(decimal(18,2),CrAmt)) as CrAmt, SUM(convert(decimal(18,2),Sales* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as [Sales], SUM(convert(decimal(18,2),CollectionRefund* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as CollectionRefund, SUM(convert(decimal(18,2),DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as DrNote, SUM(convert(decimal(18,2),CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc FROM ( " + BaseQry + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine &
                                                            " Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine &
                                                            "where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' AND CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY ACode" + Environment.NewLine
                    strLedgerQry += " ) XXX left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=XXX.ParentCode GROUP BY ACode"

                    '' ageing query start
                    Dim ArryLst As New ArrayList
                    ArryLst.Add("IN")
                    ArryLst.Add("DB")
                    ArryLst.Add("CR")
                    ArryLst.Add("RC")
                    ArryLst.Add("UC")
                    ArryLst.Add("SR")
                    ArryLst.Add("AD")
                    ArryLst.Add("RF")
                    ArryLst.Add("AV")
                    ArryLst.Add("OA")
                    ArryLst.Add("VGCL")

                    strInnerQry = clsCustomerMasterNew.GetOutStandingQry(txtToDate.Value, txtToDate.Value, CheckCustomer, ArryLst, isonduedate, ddlCurrencyType.SelectedValue, IIf((txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0), txtCustomer.arrValueMember, Nothing), IIf((txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0), txtLocation.arrValueMember, Nothing), IIf((txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0), txtCustomerGroup.arrValueMember, Nothing), ChkISParentCust.Checked, IIf(ChkParentCustSelect.IsChecked, cbgParentCust.CheckedValue, Nothing), IIf(ChkSecurity.Checked, "", "AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'"))

                    strAgeingQry = " select max(Cust_Group_Code) as Cust_Group_Code,[Customer Id] ,max([Customer Name] ) as [Customer Name],max(Zone)  as [Zone],0 as Opng, 0 as cr,0 as dr,0 as LedderAmt,sum([Due Amount])+sum([Current]) as AgeingAmt from (  select 'Aged Trial Balance Report' as rptHeading, '0' AS First_Period, '15' AS Second_Period, '30' AS [Third Period], '45' AS [Fourth Period], '60' AS [Fifth Period],'' AS [Sixth Period], '' AS [Seventh Period],'' AS [Eight Period], '' AS [Nineth Period], '60' AS [Over],  Query.Comp_Code ,Query.[Customer Id],Query.[Parent Code] ,Query.ParentName ,Query.[Customer Name]
,TSPL_ZONE_MASTER.Zone_Code as [Zone],TSPL_ZONE_MASTER.Description as [Zone Name],Query.Cust_Group_Code ,Query.Cust_Group_Desc ,Query.[Document Id] ,Query.[Desc] as [Desc] ,Query.originalAmt as [Original Amount],  case when ( DATEDIFF (day,convert(date, Query.[Document Date],101),'" & strToDate & "')+1 )>0 then Case when Query.Document_Type IN ('IN','DB','RF') then CASE WHEN convert(decimal(18,2), Query.[Due Amount] )<0 THEN convert(decimal(18,2), Query.[Due Amount] ) ELSE  convert(decimal(18,2), Query.[Due Amount] ) END else 0 End else 0 end as [Due Amount], Query.Currency ,Query.CURRENCY_CODE ,Query.ConvRate ,case when Query.Document_Type IN ('IN','DB','RF') then convert(varchar,Query.[Due Date],103) else convert(varchar, Query.[Document Date],103) end as [Due Date], Query.type ,Query.[Document Date] ,  case when Query.Document_Type NOT IN ('IN','DB','RF') then  convert(decimal(18,2), Query.[Due Amount]) else 0 End  as [Current], 
 case when Query.Document_Type IN ('IN','DB','RF') then  DATEDIFF (day,convert(date, Query.[Document Date],101),'" & strToDate & "')+1  else 0 end  as [Ageing_Days],  Query.Document_Type ,Query.Location  , '' AS From_Vendor, '' AS To_Vendor,  'Aged Trial Balance By Document date' AS Report_Type, '" & strToDate & "' AS AgeofDate,'SMry' as [Summary], 'N' as [IsFifoBased], TSPL_COMPANY_MASTER.comp_name,TSPL_COMPANY_MASTER.Add1+case  when isnull(TSPL_COMPANY_MASTER.Add2,'')='' then '' else ', '+TSPL_COMPANY_MASTER.Add2 +case  when isnull(TSPL_COMPANY_MASTER.Add3,'')='' then '' else ', '+TSPL_COMPANY_MASTER.Add3 end end as comp_address  from ( " & strInnerQry &
                    ") Query  LEFT OUTER JOIN TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code = Query .Comp_Code  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =query.[Customer Id]  left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER .Zone_Code =TSPL_CUSTOMER_MASTER.Zone_Code  Where 1=1 " + strFIlterCheck + " AND Query.[Document Id] not in (Select  UnApplied_No  from TSPL_RECEIPT_HEADER ForUnapliedEntry_bankReverse_Exclude where ForUnapliedEntry_bankReverse_Exclude.UnApplied_No =Query .[Document Id]  and isnull(UnApplied_No,'')<>''  and ForUnapliedEntry_bankReverse_Exclude.Receipt_No  in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = ForUnapliedEntry_bankReverse_Exclude.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & strToDate & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P')   )  AND Query.[Document Id] not in ( Select distinct a.Document_No from TSPL_REVALUATION_DETAIL inner join (Select RefDocNo,Document_No   from TSPL_Customer_Invoice_Head where RefDocType ='REVALUATION ENTRY') a on a.RefDocNo =TSPL_REVALUATION_DETAIL.Document_No inner join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No =TSPL_REVALUATION_DETAIL.AR_Invoice_No  where  isnull(TSPL_REVALUATION_DETAIL.AR_Invoice_No ,'')<>'' union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and (ISNULL (Against_Sale_Return_No,'') IN (Select Document_Code from TSPL_SD_SALE_rETURN_HEAD WHERE Document_Code IN (Select Against_Sale_Return_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (Against_Sale_Return_No,'')<>'') AND ISNULL(Against_Invoice_No,'')<>'') )  union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (Against_Sale_Return_No,'')<>'' AND ISNULL(Trans_Type,'') ='BSR'  union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and (ISNULL (Against_MCC_Material_Sale_Return,'') IN (Select Document_Code from TSPL_SD_SALE_rETURN_HEAD WHERE Document_Code IN (Select Against_MCC_Material_Sale_Return  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (Against_MCC_Material_Sale_Return,'')<>'') AND ISNULL(Against_Invoice_No,'')<>'') )  union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and (ISNULL (AgainstScrapReturn,'') IN (Select Document_No from TSPL_SCRAPSALE_HEAD_RETURN WHERE Document_No in (Select AgainstScrapReturn  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (AgainstScrapReturn,'')<>'') ) )) ) xxx  group by [Customer Id]"

                    strFinalQry = "Select * from (select MAX(Cust_Group_Code) AS Cust_Group_Code,  ACode,  MAX(AName) as AName,MAX(Zone_code) as Zone_code, SUM(OpngBal) as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt,sum(LedgerAmt) as  LedgerAmt,sum(AgeingAmt) as AgeingAmt,sum(LedgerAmt) -sum(AgeingAmt) as Diff from ( " &
                        " " & strLedgerQry & "" & Environment.NewLine &
                        " Union " & Environment.NewLine &
                    " " & strAgeingQry & "" & Environment.NewLine &
                    ") final group by ACode) Qry "
                    If chkMismatchedData.Checked Then
                        strFinalQry += " Where Qry.Diff <>0"
                    End If

                    dtCustomer = clsDBFuncationality.GetDataTable(strFinalQry)
                End If

                If BulkExport = 1 Then
                    transportSql.BulkExport("Customer_Ledger", strQry, "ORDER BY CTETemp.ACode,  CTETemp.RowNo", "csv", "select ctetemp.rowno")
                    Exit Sub
                ElseIf BulkExport = 2 Then
                    transportSql.BulkExport("Customer_Ledger", strQry, "ORDER BY CTETemp.ACode,  CTETemp.RowNo", "xls", "select ctetemp.rowno")
                    Exit Sub
                End If

                If dtCustomer.Rows.Count <= 0 Then
                    btnPrint.Enabled = False
                    clsCommon.MyMessageBoxShow(Me, "Data not found", Me.Text)
                    Exit Sub
                Else
                    btnPrint.Enabled = True
                End If

                btnPrint.Enabled = True



                If rbtnCustWise.Checked Then
                    gvCustomer.DataSource = Nothing
                    gvCustomer.Columns.Clear()
                    gvCustomer.Rows.Clear()
                    gvCustomer.DataSource = dtCustomer
                    gvCustomer.Visible = True
                    FormatgvCustomer()
                End If

            End If

            If btnrefresh = False Then
                If isExportToExcel = True Then
                    Dim arrHeadrer As New List(Of String)
                    arrHeadrer.Add("From Date : " + strFromDate + "")
                    arrHeadrer.Add("To Date : " + strToDate + "")
                    If gvCustomer.Visible Then
                        clsCommon.MyExportToExcel("Customer Ledger VS Ageing (Customer Wise)", gvCustomer, arrHeadrer, "CustomerLedger")
                    End If

                End If
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            RadGroupBox1.Enabled = False
            ReStoreGridCust()
            GridSummaryRow()

            CompanyAdd = Nothing
            compname = Nothing
            qry = Nothing
            CheckCustomer = Nothing
            FilterForLevels = Nothing
            FilterForDetail = Nothing
            ACodeFilter = Nothing
            strcustomerfilter = Nothing
            strFIlterCheck = Nothing
            StrDocWiseFilter = Nothing

            BaseQry = Nothing
            BaseQryOPENINGINCASEOFMIS = Nothing
            strFromDate = Nothing

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridCust()
        Try
            Dim TempReportID As String = ""
            If clsCommon.CompairString(FormType, clsUserMgtCode.CustomerLedgerVsAgeing) = CompairStringResult.Equal Then
                TempReportID = "CustomerLedgerVsAgeing"
            End If
            If clsCommon.myLen(TempReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(TempReportID + "C", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvCustomer.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvCustomer.Columns.Count - 1 Step ii + 1
                        gvCustomer.Columns(ii).IsVisible = False
                        gvCustomer.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvCustomer.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub


    Private Sub FormatgvCustomer()
        Try
            gvCustomer.AllowAddNewRow = False
            gvCustomer.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 0 To gvCustomer.Columns.Count - 1
                gvCustomer.Columns(ii).ReadOnly = True
                gvCustomer.Columns(ii).IsVisible = False
            Next

            gvCustomer.Columns("Cust_Group_Code").IsVisible = False
            gvCustomer.Columns("Cust_Group_Code").Width = 101
            gvCustomer.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"

            gvCustomer.Columns("ACode").IsVisible = True
            gvCustomer.Columns("ACode").Width = 180
            gvCustomer.Columns("ACode").HeaderText = "Customer Code"

            gvCustomer.Columns("AName").IsVisible = True
            gvCustomer.Columns("AName").Width = 350
            gvCustomer.Columns("AName").HeaderText = "Name"

            gvCustomer.Columns("Zone_code").IsVisible = False
            gvCustomer.Columns("Zone_code").Width = 350
            gvCustomer.Columns("Zone_code").HeaderText = "Zone"

            gvCustomer.Columns("OpngBal").IsVisible = False
            gvCustomer.Columns("OpngBal").Width = 101
            gvCustomer.Columns("OpngBal").HeaderText = "OpngBal"
            gvCustomer.Columns("OpngBal").FormatString = "{0:f2}"

            gvCustomer.Columns("DrAmt").IsVisible = False
            gvCustomer.Columns("DrAmt").Width = 101
            gvCustomer.Columns("DrAmt").HeaderText = "DrAmt"
            gvCustomer.Columns("DrAmt").FormatString = "{0:f2}"


            gvCustomer.Columns("CrAmt").IsVisible = False
            gvCustomer.Columns("CrAmt").Width = 101
            gvCustomer.Columns("CrAmt").HeaderText = "CrAmt"
            gvCustomer.Columns("CrAmt").FormatString = "{0:f2}"

            gvCustomer.Columns("LedgerAmt").IsVisible = True
            gvCustomer.Columns("LedgerAmt").Width = 101
            gvCustomer.Columns("LedgerAmt").HeaderText = "LedgerAmt"
            gvCustomer.Columns("LedgerAmt").FormatString = "{0:f2}"

            gvCustomer.Columns("AgeingAmt").IsVisible = True
            gvCustomer.Columns("AgeingAmt").Width = 101
            gvCustomer.Columns("AgeingAmt").HeaderText = "AgeingAmt"
            gvCustomer.Columns("AgeingAmt").FormatString = "{0:f2}"

            gvCustomer.Columns("Diff").IsVisible = True
            gvCustomer.Columns("Diff").Width = 101
            gvCustomer.Columns("Diff").HeaderText = "Diff"
            gvCustomer.Columns("Diff").FormatString = "{0:f2}"

            ReStoreGridCust()
            GridSummaryRow()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
    End Sub
    Private Sub chkCompanyAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCompanyAll.ToggleStateChanged, chkCompanySelect.ToggleStateChanged
        cbgCompany.Enabled = Not chkCompanyAll.IsChecked
    End Sub

    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged, chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub

    Private Sub chkCusALL_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLOcALL.ToggleStateChanged
        cbgLocation.Enabled = Not chkLOcALL.IsChecked
    End Sub

    Private Sub chkCustGrpAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustGrpAll.ToggleStateChanged
        cbgCustGrp.Enabled = False
    End Sub

    Private Sub chkCustGrpSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustGrpSelect.ToggleStateChanged
        cbgCustGrp.Enabled = True
    End Sub

    Private Sub refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles refreshbtn.Click
        btnrefresh = True
        dtCustomer = Nothing
        GC.Collect()
        print()
        btnrefresh = False
        ReStoreGridCust()
        GridSummaryRow()
        ReportID = GetReportID()
        PageSetupReport_ID = GetReportID()
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            Dim obj As New clsGridLayout()
            If gvCustomer.Visible = True Then
                gvCustomer.MasterTemplate.FilterDescriptors.Clear()
                obj = New clsGridLayout()
                obj.ReportID = GetReportID() 'ReportID + "C"
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gvCustomer.SaveLayout(obj.GridLayout)
                obj.GridColumns = gvCustomer.ColumnCount
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
                End If
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()

            End If

        End If
    End Sub

    '-----------------Delete Layout---------------------
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        ReportID = GetReportID()
        If clsCommon.myLen(ReportID) > 0 Then
            If gvCustomer.Visible = True Then
                clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) 'ReportID & "C"
                FormatgvCustomer()
                ReStoreGridCust()
                GridSummaryRow()
            End If

            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
        End If
    End Sub
    Private Sub GridSummaryRow()
        If gvCustomer.Visible = True Then
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim TotalAmt As New GridViewSummaryItem("OpngBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("Sales", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CollectionRefund", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)

            If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                Dim TotalClosing As New GridViewSummaryItem()
                TotalClosing.FormatString = "{0:F2}"
                TotalClosing.Name = "BalAmt"
                TotalClosing.AggregateExpression = "sum(OpngBal)+sum(DrAmt)-sum(CrAmt)+sUM(Sales)-SUM(CollectionRefund)"
                summaryRowItem.Add(TotalClosing)
            Else
                Dim TotalClosing As New GridViewSummaryItem()
                TotalClosing.FormatString = "{0:F2}"
                TotalClosing.Name = "BalAmt"
                TotalClosing.AggregateExpression = "sum(OpngBal)+sum(DrAmt)-sum(CrAmt)"
                summaryRowItem.Add(TotalClosing)
            End If

            gvCustomer.MasterTemplate.SummaryRowsBottom.Clear()
            gvCustomer.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        End If
    End Sub
    Private Sub gvCustomer_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCustomer.CellDoubleClick
        Try
            If isRunDoubleClick Then
                If clsCommon.myLen(gvCustomer.CurrentRow.Cells("ACode").Value) > 0 Then

                    Dim arrCustomer As New ArrayList
                    arrCustomer.Add(gvCustomer.CurrentRow.Cells("ACode").Value)
                    txtCustomer.arrValueMember = arrCustomer
                    btnrefresh = True

                    print()
                    gvCustomer.Visible = False
                    GridSummaryRow()
                    IsDrillDown = True
                    BackProcess = False
                    btnrefresh = False
                    PageSetupReport_ID = GetReportID()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cbgParentCust__MyCheckChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbgParentCust._MyCheckChanged
        Try
            If cbgParentCust.Enabled = True AndAlso cbgParentCust.CheckedValue.Count > 0 Then
                strQry = clsCommon.GetMulcallString(cbgParentCust.CheckedValue)

                strQry = "Select Cust_Code As [Code],ISNULL(Customer_Name,'') As [Description] From TSPL_CUSTOMER_MASTER Where Parent_Customer_No In (" + strQry + ") "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    cbgCustomer.DataSource = dt
                    cbgCustomer.DisplayMember = "Description"
                    cbgCustomer.ValueMember = "Code"
                Else
                    cbgCustomer.DataSource = Nothing
                End If
            Else
                cbgCustomer.DataSource = Nothing
            End If
        Catch ex As Exception
            cbgCustomer.DataSource = Nothing
        End Try
    End Sub

    Private Sub ChkParentCustAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkParentCustAll.ToggleStateChanged
        cbgParentCust.Enabled = False
        'LoadCustomer()
    End Sub

    Private Sub ChkParentCustSelect_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkParentCustSelect.ToggleStateChanged
        cbgParentCust.Enabled = True
    End Sub

    Private Sub ChkDocWise_CheckStateChanged(sender As Object, e As EventArgs) Handles ChkDocWise.CheckStateChanged
        If ChkDocWise.Checked = True Then
            ChkDocSumm.Enabled = True
        Else
            ChkDocSumm.Enabled = False
        End If
    End Sub

    Private Sub ChkCustCatAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkCustCatAll.ToggleStateChanged
        cbgcustcat.Enabled = False
    End Sub

    Private Sub ChkCustCatSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkCustCatSelect.ToggleStateChanged
        cbgcustcat.Enabled = True
    End Sub

    Private Sub ChkCustTypeAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkCustTypeAll.ToggleStateChanged
        cbgcusttype.Enabled = False
    End Sub

    Private Sub ChkCustTypeSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkCustTypeSelect.ToggleStateChanged
        cbgcusttype.Enabled = True
    End Sub

    Private Sub txtCompany__My_Click(sender As Object, e As EventArgs) Handles txtCompany._My_Click
        strQry = "SELECT Comp_Code as Code,Comp_Name as Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        txtCompany.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCompany.arrValueMember, txtCompany.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name  from"
        strQry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
        strQry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'"
        strQry += " order by xxx.Loc_Segment_Code"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim frmpending As New FrmPendingRequisitionQty()
        frmpending.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomerGroup._My_Click
        strQry = "Select Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER where 1=1"
        Dim dtCustGroup As DataTable = clsDBFuncationality.GetDataTable("select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "'")

        If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 Then
            strQry += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "')"
        End If
        txtCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomerGroup.arrValueMember, txtCustomerGroup.arrDispalyMember)
    End Sub

    Sub LoadCustomerNew()
        strQry = "select CM1.cust_code as Code, CM1.Customer_Name as Name, Case When ISNULL(CM2.Cust_Code,'')<>'' Then ISNULL(CM2.Cust_Code,'')+' - '+ISNULL(CM2.Customer_Name,'') Else '' End as [ParentCustomer]  from tspl_customer_master CM1 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM2 ON CM2.Cust_Code=CM1.Parent_Customer_No  where 1=1"
        If chkActive.Checked Then
            strQry += " and CM1.Status='N'"
        ElseIf chkInactive.Checked Then
            strQry += " and CM1.Status='Y'"
        End If
        ''richa agarwal 14 Feb,2019 ERO/05/02/19-000485
        If AllowtoSHOWParentChildCustomer = True Then
            clsCommon.MyMessageBoxShow("Please select Parent/Child with their Child/Parent for better clarity of report..")
        End If

        Dim dtCustGroup As DataTable = clsDBFuncationality.GetDataTable("select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "'")

        If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 Then
            strQry += " AND CM1.cust_code in (select DISTINCT Cust_Code from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL WHERE User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "' "
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                strQry += " and TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ") ) "
            Else
                strQry += " ) "
            End If
        End If

        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        LoadCustomerNew()
    End Sub

    Private Sub txtParentCustomer__My_Click(sender As Object, e As EventArgs)
        strQry = "select cust_code as  Code, Customer_Name as  Name from tspl_customer_master "
        If ChkISParentCust.Checked Then
            strQry += " Where Parent_Customer_YN ='Y' "
        End If
    End Sub

    Private Sub txtCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles txtCustomerCategory._My_Click
        strQry = "SELECT Distinct TSPL_CUSTOMER_MASTER.Cust_Category_Code AS [Code],TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC  AS [Description] FROM TSPL_CUSTOMER_MASTER LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER  ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE  =  TSPL_CUSTOMER_MASTER.Cust_Category_Code"
        txtCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustomerCategorySelector@CustomerLedger", strQry, "Code", "Description", txtCustomerCategory.arrValueMember, txtCustomerCategory.arrDispalyMember)
    End Sub

    Private Sub txtCustomerType__My_Click(sender As Object, e As EventArgs) Handles txtCustomerType._My_Click
        strQry = "SELECT DISTINCT TSPL_CUSTOMER_MASTER.Cust_Type_Code AS [Code],TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc  AS [Description] FROM TSPL_CUSTOMER_MASTER LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code"
        txtCustomerType.arrValueMember = clsCommon.ShowMultipleSelectForm("CustomerTypeSelector@CustomerLedger", strQry, "Code", "Description", txtCustomerType.arrValueMember, txtCustomerType.arrDispalyMember)
    End Sub

    Private Sub btnReset_Click_1(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            gvCustomer.DataSource = Nothing
            RadGroupBox1.Enabled = True
            dvTemp1 = Nothing
            GC.Collect()
            RadPageView1.SelectedPage = RadPageViewPage1
            Dim AllowTrasactionFilterOnCustomerLedger As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowTransactionFiltersOnCustomerlegder, clsFixedParameterCode.AllowTransactionFiltersOnCustomerlegder, Nothing)) = 1, True, False))
            If AllowTrasactionFilterOnCustomerLedger = True Then
                chkIncludeApplyDocument.Checked = True
                chkIncludeApplyDocument.Enabled = False
                chkIncludeApplyDocument.Visible = False
            Else

                chkIncludeApplyDocument.Checked = False
                chkIncludeApplyDocument.Enabled = True
                chkIncludeApplyDocument.Visible = True
            End If
            Dim AllowtoMakeApplyDocOnbyDefault As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoMakeApplyDocOnbyDefault, clsFixedParameterCode.AllowtoMakeApplyDocOnbyDefault, Nothing)) = 1, True, False))
            If AllowtoMakeApplyDocOnbyDefault = True Then
                chkIncludeApplyDocument.Checked = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & FormType & "'"))

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If rbtnCustWise.Checked AndAlso gvCustomer.Visible = True Then ' gvCustomer.Visible 
                transportSql.QuickExportToExcel(gvCustomer, "", Me.Text, , arrHeader)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub TxtSecurity__My_Click(sender As Object, e As EventArgs) Handles TxtSecurity._My_Click
        strQry = "SELECT 'S' as Code,'Security' as Type union all select 'C' as Code,'Crate Security' as Type union all select 'R' as Code,'Refrigerator Security' as Type UNION ALL SELECT 'O' as Code,'Others' as Type"
        TxtSecurity.arrValueMember = clsCommon.ShowMultipleSelectForm("SecurityTypeMulSel", strQry, "Code", "Type", TxtSecurity.arrValueMember, TxtSecurity.arrDispalyMember)
    End Sub

    Private Sub ChkSecurity_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkSecurity.ToggleStateChanged
        If ChkSecurity.Checked Then
            TxtSecurity.Enabled = True
        Else
            TxtSecurity.Enabled = False
        End If
    End Sub

    Private Sub QExpExcel_Click(sender As Object, e As EventArgs) Handles QExpExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & FormType & "'"))

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If rbtnCustWise.Checked AndAlso gvCustomer.Visible = True Then ' gvCustomer.Visible 
                transportSql.applyExportTemplate(gvCustomer, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gvCustomer, "", Me.Text, , arrHeader)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Public Sub ExportCSV(ByVal sender As RadGridView, Optional ByVal AddHeader As Boolean = False)
        Try
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
            clsCommon.MyMessageBoxShow("Data Exported successfully")
            Process.Start(filePath)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub TxtZone__My_Click(sender As Object, e As EventArgs) Handles TxtZone._My_Click
        strQry = "select Zone_Code as Code ,Description as Name from TSPL_ZONE_MASTER where 1=1"
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            strQry += " and TSPL_ZONE_MASTER. Zone_Code in (Select TSPL_CUSTOMER_MASTER.Zone_Code from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") )"
        End If
        TxtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneMulSel", strQry, "Code", "Name", TxtZone.arrValueMember, TxtZone.arrDispalyMember)
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & FormType & "'"))


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            PageSetupReport_ID = GetReportID()

            If gvCustomer.Visible = True Then
                transportSql.applyExportTemplate(gvCustomer, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gvCustomer, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BulkCSV_Click(sender As Object, e As EventArgs) Handles BulkCSV.Click
        btnrefresh = True
        ' print()
        GC.Collect()

        print(1)

    End Sub

    Private Sub BulkExcel_Click(sender As Object, e As EventArgs) Handles BulkExcel.Click
        btnrefresh = True
        GC.Collect()
        print(2)

    End Sub

    Private Function GetReportID() As String
        If clsCommon.CompairString(FormType, clsUserMgtCode.CustomerLedgerVsAgeing) = CompairStringResult.Equal Then
            ReportID = "CustomerLedgerVSAgeing"
        End If
        If gvCustomer.Visible = True Then
            ReportID = ReportID + "C"
            TemplateGridview = gvCustomer
        End If
        Return ReportID
    End Function


    Private Sub ExcelGrid_Click(sender As Object, e As EventArgs) Handles ExcelGrid.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & FormType & "'"))

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If rbtnCustWise.Checked AndAlso gvCustomer.Visible = True Then ' gvCustomer.Visible 
                clsCommon.MyExportToExcelGrid(Me.Text, gvCustomer, arrHeader, Me.Text, True)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDFGrid_Click(sender As Object, e As EventArgs) Handles PDFGrid.Click
        Try

            Dim FilePath As String = "C:\\ERPTempFolder\\" + clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & FormType & "'")) + clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmss") + ".pdf"
            Dim pdfExporter As ExportToPDF = Nothing

            If gvCustomer.Visible = True Then
                pdfExporter = New ExportToPDF(gvCustomer)

            End If

            pdfExporter.Font = New System.Drawing.Font("Verdana", 6)
            pdfExporter.TableBorderThickness = 1
            pdfExporter.FitToPageWidth = True
            pdfExporter.ExportVisualSettings = True
            pdfExporter.ExportHierarchy = True
            pdfExporter.HiddenColumnOption = HiddenOption.DoNotExport
            pdfExporter.PageTitle = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & FormType & "'"))
            pdfExporter.RunExport(FilePath)
            System.Diagnostics.Process.Start(FilePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class
