

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO

Public Class rptCustomerAgeingDrillDown
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery, strType As String
    Dim ArrDBName As ArrayList = Nothing
    Dim strLocation As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    Dim SettingAllowtoShowCreditBalanceonCustomerAgeing As Boolean = False
    Dim ConsiderOpeningDocintoBucketsonCustomerAgeing As Boolean = False
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()

            Next
        End If
    End Sub

    Sub LoadLocation()
        strQuery = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
            strQuery += " and TSPL_GL_SEGMENT_CODE.Segment_code in (" + objCommonVar.strCurrUserLocationsSegment + ") "
        End If
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(strQuery)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub rptCustomerAgeingDrillDown_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SettingAllowtoShowCreditBalanceonCustomerAgeing = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoShowCreditBalanceonCustomerAgeing, clsFixedParameterCode.AllowtoShowCreditBalanceonCustomerAgeing, Nothing)) > 0, True, False)
        ConsiderOpeningDocintoBucketsonCustomerAgeing = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsiderOpeningDocintoBucketsonCustomerAgeing, clsFixedParameterCode.ConsiderOpeningDocintoBucketsonCustomerAgeing, Nothing)) = 1, True, False)
        SetUserMgmtNew()
        LoadLocationCode()
        LoadCustomerGroup()
        LoadCurrencyType()
        chkCGAll.IsChecked = True
        chkLocAll.IsChecked = True
        chkType.Checked = False
        dtpAgeof.Value = Date.Today
        dtpCutoffDate.Value = Date.Today
        chkCustomerAll.IsChecked = True
        cbgCustomer.Enabled = False
        ddlAgedRcvbl.Text = "Aged Trial Balance By Document date"
        chkActive.Checked = True
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        txt5.Text = ""
        txt6.Text = ""
        txt7.Text = ""
        txt8.Text = ""
        txtOvr.Text = ""
        If clsCommon.myLen(Me.Tag) > 0 Then
            Dim arrCustomer As New ArrayList
            arrCustomer.Add(clsCommon.myCstr(Me.Tag))
            txtCustomer.arrValueMember = arrCustomer

            dtpAgeof.Value = objCommonVar.ObjVar2
            gv1.EnableFiltering = True
            PageSetupReport_ID = MyBase.Form_ID
            chkType.Checked = True
            print(True)
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.frmCustomerAgeing)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Sub Reset()
        LoadLocation()
        chkLocAll.IsChecked = True
        LoadCustomer()
        LoadParentCustomer()
    End Sub
    Private Sub LoadCustomer()
        strQuery = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name],Cust_Group_Code as [Customer Group] from TSPL_CUSTOMER_MASTER WHERE 1=1 "
        If chkActive.Checked Then
            strQuery += " and Status='N'"
        ElseIf chkInactive.Checked Then
            strQuery += " and Status='Y'"
        ElseIf chkAll.Checked Then
            strQuery += ""
        End If
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
            strQuery += " and TSPL_CUSTOMER_MASTER.cust_code in (" + objCommonVar.strCurrUserCustomers + ") "
        End If
        strQuery += "  order by  Cust_Code "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strQuery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub LoadParentCustomer()
        strQuery = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where 2=2 "
        If ChkISParentCust.Checked Then
            strQuery += " and Parent_Customer_YN ='Y' "
        End If
        If chkActive.Checked Then
            strQuery += " and Status='N'"
        ElseIf chkInactive.Checked Then
            strQuery += " and Status='Y'"
        ElseIf chkAll.Checked Then
            strQuery += ""
        End If
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
            strQuery += " and TSPL_CUSTOMER_MASTER.cust_code in (" + objCommonVar.strCurrUserCustomers + ") "
        End If
        cbgParentCust.DataSource = clsDBFuncationality.GetDataTable(strQuery)
        cbgParentCust.ValueMember = "Customer Code"
        cbgParentCust.DisplayMember = "Customer Name"
        ChkParentCustAll.IsChecked = True
        cbgParentCust.Enabled = False
    End Sub

    Private Sub LoadCustomerGroup()
        strQuery = "Select Cust_Group_Code as Code, Cust_Group_Desc as Description from TSPL_CUSTOMER_GROUP_MASTER"
        cbgCustomerGroup.DataSource = clsDBFuncationality.GetDataTable(strQuery)
        cbgCustomerGroup.ValueMember = "Code"
        cbgCustomerGroup.DisplayMember = "Description"
    End Sub

    Private Sub LoadLocationCode()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Sub print(ByVal Isgrid As Boolean)
        Try
            Dim txt1 As String = Me.txt1.Text
            Dim txt2 As String = Me.txt2.Text
            Dim txt3 As String = Me.txt3.Text
            Dim txt4 As String = Me.txt4.Text
            Dim txt5 As String = Me.txt5.Text
            Dim txt6 As String = Me.txt6.Text
            Dim txt7 As String = Me.txt7.Text
            Dim txt8 As String = Me.txt8.Text
            Dim txtOvr As String
            Dim strNo As String
            Dim type As String = Me.ddlAgedRcvbl.Text
            Dim strTtpe As String = ""
            Dim IsFifoBased As String = "N"

            If chkType.Checked = True Then
                strTtpe = "SMry"
            End If

            If chkPrintCustomerPerPage.Checked = True Then
                If chkType.Checked = True Then
                    strTtpe = "SMryCusPerpage"
                Else
                    strTtpe = "CusPerpage"
                End If

            End If
            If chkFifo.Checked Then
                IsFifoBased = "Y"
            End If
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


            If ArryLst.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Transaction Type")
                Return
            End If
            If Me.txt1.Text = "" Then
                MsgBox("Select Atleast 1 Bucket!", MsgBoxStyle.Information, "Aged Trial Balance Report")
                Exit Sub
            ElseIf Me.txt1.Text <> "" And Me.txt2.Text <> "" And Me.txt3.Text <> "" And Me.txt4.Text = "" And Me.txt5.Text = "" And Me.txt6.Text = "" And Me.txt7.Text = "" And Me.txt8.Text = "" Then
                strNo = 0
                txtOvr = Me.txt3.Text
            ElseIf Me.txt1.Text <> "" And Me.txt2.Text <> "" And Me.txt3.Text <> "" And Me.txt4.Text <> "" And Me.txt5.Text = "" And Me.txt6.Text = "" And Me.txt7.Text = "" And Me.txt8.Text = "" Then
                strNo = 1
                txtOvr = Me.txt4.Text
            ElseIf Me.txt1.Text <> "" And Me.txt2.Text <> "" And Me.txt3.Text <> "" And Me.txt4.Text <> "" And Me.txt5.Text <> "" And Me.txt6.Text = "" And Me.txt7.Text = "" And Me.txt8.Text = "" Then
                strNo = 2
                txtOvr = Me.txt5.Text
            ElseIf Me.txt1.Text <> "" And Me.txt2.Text <> "" And Me.txt3.Text <> "" And Me.txt4.Text <> "" And Me.txt5.Text <> "" And Me.txt6.Text <> "" And Me.txt7.Text = "" And Me.txt8.Text = "" Then
                strNo = 3
                txtOvr = Me.txt6.Text
            ElseIf Me.txt1.Text <> "" And Me.txt2.Text <> "" And Me.txt3.Text <> "" And Me.txt4.Text <> "" And Me.txt5.Text <> "" And Me.txt6.Text <> "" And Me.txt7.Text <> "" And Me.txt8.Text = "" Then
                strNo = 4
                txtOvr = Me.txt7.Text
            ElseIf Me.txt1.Text <> "" And Me.txt2.Text <> "" And Me.txt3.Text <> "" And Me.txt4.Text <> "" And Me.txt5.Text <> "" And Me.txt6.Text <> "" And Me.txt7.Text <> "" And Me.txt8.Text <> "" Then
                strNo = ""
                txtOvr = Me.txtOvr.Text
            ElseIf Me.txt1.Text <> "" And Me.txt2.Text = "" And Me.txt3.Text = "" And Me.txt4.Text = "" And Me.txt5.Text = "" And Me.txt6.Text = "" And Me.txt7.Text = "" And Me.txt8.Text = "" Then
                strNo = 5
                txtOvr = Me.txt1.Text
            Else
                MsgBox("Selection Criteria Not In Order", MsgBoxStyle.Information, "Aged Trial Balance Report")
                Exit Sub
            End If

            Dim CheckCustomer As String = String.Empty
            If chkActive.Checked Then
                CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='N'"
            ElseIf chkInactive.Checked Then
                CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='Y'"
            End If

            Dim rptHeading As String
            rptHeading = "Aged Trial Balance Report"

            strQuery = ""
            Dim strEmptyQry As String = ""
            Dim strFilledQry As String = ""
            Dim strUpperQry As String = ""
            Dim strUpperQry1 As String = ""
            Dim strInnerQry As String = ""
            Dim strLowerQry As String = ""
            Dim strLowerQry1 As String = ""

            Dim isonduedate As String = String.Empty
            If clsCommon.CompairString(ddlAgedRcvbl.Text, "Aged Trial Balance By Due Date") = CompairStringResult.Equal Then
                isonduedate = "DueDate"
            ElseIf clsCommon.CompairString(ddlAgedRcvbl.Text, "Aged Trial Balance By Document date") = CompairStringResult.Equal Then
                isonduedate = "DocumentDate"
            End If
            '======================================================================================
            Dim arrMappCustCategory As New ArrayList
            Dim chkCustCategoryMappInUserMaster As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count ( distinct CUSTOMER_CATEGORY) as CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')"))
            If chkCustCategoryMappInUserMaster = True Then
                Dim qry As String = " select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "') "
                Dim dtMappCustCategory As DataTable = clsDBFuncationality.GetDataTable(qry)
                For Each dr As DataRow In dtMappCustCategory.Rows
                    arrMappCustCategory.Add(clsCommon.myCstr(dr.Item("CUSTOMER_CATEGORY")))
                Next
            End If
            '=========================================================================================
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 AndAlso (txtLocation.arrValueMember Is Nothing OrElse txtLocation.arrValueMember.Count <= 0) Then
                Dim strLoc As String = "select Segment_code from TSPL_GL_SEGMENT_CODE where Segment_code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
                Dim dtLoc As DataTable = clsDBFuncationality.GetDataTable(strLoc)
                Dim arrGLLocCode As New ArrayList
                If dtLoc IsNot Nothing AndAlso dtLoc.Rows.Count > 0 Then
                    For Each dr As DataRow In dtLoc.Rows
                        arrGLLocCode.Add(clsCommon.myCstr(dr("Segment_code")))
                    Next
                End If
                strInnerQry = clsCustomerMasterNew.GetOutStandingQry(dtpAgeof.Value, dtpCutoffDate.Value, CheckCustomer, ArryLst, isonduedate, ddlCurrencyType.SelectedValue, IIf((txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0), txtCustomer.arrValueMember, Nothing), arrGLLocCode, IIf((txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0), txtCustomerGroup.arrValueMember, Nothing), ChkISParentCust.Checked, IIf(ChkParentCustSelect.IsChecked, cbgParentCust.CheckedValue, Nothing), IIf(ChkSecurity.Checked, "", "AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'"), False, IIf((arrMappCustCategory IsNot Nothing AndAlso arrMappCustCategory.Count > 0), arrMappCustCategory, Nothing), IIf((TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0), TxtMultiCustomerCategory.arrValueMember, Nothing))
            Else
                strInnerQry = clsCustomerMasterNew.GetOutStandingQry(dtpAgeof.Value, dtpCutoffDate.Value, CheckCustomer, ArryLst, isonduedate, ddlCurrencyType.SelectedValue, IIf((txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0), txtCustomer.arrValueMember, Nothing), IIf((txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0), txtLocation.arrValueMember, Nothing), IIf((txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0), txtCustomerGroup.arrValueMember, Nothing), ChkISParentCust.Checked, IIf(ChkParentCustSelect.IsChecked, cbgParentCust.CheckedValue, Nothing), IIf(ChkSecurity.Checked, "", "AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'"), False, IIf((arrMappCustCategory IsNot Nothing AndAlso arrMappCustCategory.Count > 0), arrMappCustCategory, Nothing), IIf((TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0), TxtMultiCustomerCategory.arrValueMember, Nothing))
            End If

            strUpperQry = " select '" + rptHeading + "' as rptHeading, '" + txtCurr.Text + "' AS First_Period, '" + Me.txt1.Text + "' AS Second_Period, '" + Me.txt2.Text + "' AS [Third Period], '" + Me.txt3.Text + "' AS [Fourth Period], '" + Me.txt4.Text + "' AS [Fifth Period],'" + Me.txt5.Text + "' AS [Sixth Period]," & _
                                " '" + Me.txt6.Text + "' AS [Seventh Period],'" + Me.txt7.Text + "' AS [Eight Period], '" + Me.txt8.Text + "' AS [Nineth Period], '" + txtOvr + "' AS [Over], " & _
            " Query.Comp_Code ,Query.[Customer Id],Query.[Parent Code] ,Query.ParentName ,Query.[Customer Name] ,TSPL_ZONE_MASTER.Zone_Code as [Zone],TSPL_ZONE_MASTER.Description as [Zone Name],Query.Cust_Group_Code ,Query.Cust_Group_Desc ,Query.[Document Id] ,Query.[Desc] as [Desc] ,Query.originalAmt as [Original Amount], "


            If clsCommon.CompairString(ddlAgedRcvbl.Text, "Aged Trial Balance By Due Date") = CompairStringResult.Equal Then
                If ConsiderOpeningDocintoBucketsonCustomerAgeing = True Then
                    strUpperQry += " case when ( DATEDIFF (day,convert(date, Query.[Due Date],101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 or isnull(Query.[Due Date],'')='' then Case when Query.Document_Type IN ('IN','DB','RF') then CASE WHEN convert(decimal(18,2), Query.[Due Amount] )<0 THEN " & IIf(SettingAllowtoShowCreditBalanceonCustomerAgeing = True, 0, "convert(decimal(18,2), Query.[Due Amount] )") & " ELSE  convert(decimal(18,2), Query.[Due Amount] ) END else convert(decimal(18,2), Query.[Due Amount] )  End else 0 end as [Due Amount],"
                Else
                    strUpperQry += " case when ( DATEDIFF (day,convert(date, Query.[Due Date],101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 or isnull(Query.[Due Date],'')='' then Case when Query.Document_Type IN ('IN','DB','RF') then CASE WHEN convert(decimal(18,2), Query.[Due Amount] )<0 THEN " & IIf(SettingAllowtoShowCreditBalanceonCustomerAgeing = True, 0, "convert(decimal(18,2), Query.[Due Amount] )") & " ELSE  convert(decimal(18,2), Query.[Due Amount] ) END else 0 End else 0 end as [Due Amount],"
                End If

            ElseIf clsCommon.CompairString(ddlAgedRcvbl.Text, "Aged Trial Balance By Document date") = CompairStringResult.Equal Then
                If ConsiderOpeningDocintoBucketsonCustomerAgeing = True Then
                    strUpperQry += " case when ( DATEDIFF (day,convert(date, Query.[Document Date],101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Query.Document_Type IN ('IN','DB','RF') then CASE WHEN convert(decimal(18,2), Query.[Due Amount] )<0 THEN " & IIf(SettingAllowtoShowCreditBalanceonCustomerAgeing = True, 0, "convert(decimal(18,2), Query.[Due Amount] )") & " ELSE  convert(decimal(18,2), Query.[Due Amount] ) END else convert(decimal(18,2), Query.[Due Amount] )  End else 0 end as [Due Amount],"
                Else
                    strUpperQry += " case when ( DATEDIFF (day,convert(date, Query.[Document Date],101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 then Case when Query.Document_Type IN ('IN','DB','RF') then CASE WHEN convert(decimal(18,2), Query.[Due Amount] )<0 THEN " & IIf(SettingAllowtoShowCreditBalanceonCustomerAgeing = True, 0, "convert(decimal(18,2), Query.[Due Amount] )") & " ELSE  convert(decimal(18,2), Query.[Due Amount] ) END else 0 End else 0 end as [Due Amount],"
                End If


            End If


            strUpperQry += " Query.Currency ,Query.CURRENCY_CODE ,Query.ConvRate ,case when Query.Document_Type IN ('IN','DB','RF') then convert(varchar,Query.[Due Date],103) else convert(varchar, Query.[Document Date],103) end as [Due Date], Query.type ,Query.[Document Date] , "

            If clsCommon.CompairString(ddlAgedRcvbl.Text, "Aged Trial Balance By Due Date") = CompairStringResult.Equal Then
                If ConsiderOpeningDocintoBucketsonCustomerAgeing = True Then
                    strUpperQry += " 0 as [Current], " + Environment.NewLine &
                " case when isnull(Query.[Due Date],'')<>'' then  DATEDIFF (day,convert(date, Query.[Due Date],101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1  else 0 end  as [Ageing_Days], "
                Else
                    strUpperQry += " case when Query.Document_Type NOT IN ('IN','DB','RF') then  convert(decimal(18,2), Query.[Due Amount]) else case when ( DATEDIFF (day,convert(date, Query.[Due Date],101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )<=0 then Case when Query.Document_Type IN ('IN','DB','RF') then convert(decimal(18,2), Query.[Due Amount] ) else 0 End else 0 end End  as [Current], " + Environment.NewLine &
                " case when Query.Document_Type IN ('IN','DB','RF') and isnull(Query.[Due Date],'')<>'' then  DATEDIFF (day,convert(date, Query.[Due Date],101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1  else 0 end  as [Ageing_Days], "
                End If

            ElseIf clsCommon.CompairString(ddlAgedRcvbl.Text, "Aged Trial Balance By Document date") = CompairStringResult.Equal Then
                If ConsiderOpeningDocintoBucketsonCustomerAgeing = True Then
                    strUpperQry += " 0  as [Current], " + Environment.NewLine &
              "  DATEDIFF (day,convert(date, Query.[Document Date],101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1   as [Ageing_Days], "
                Else
                    strUpperQry += " case when Query.Document_Type NOT IN ('IN','DB','RF') then  convert(decimal(18,2), Query.[Due Amount]) else 0 End  as [Current], " + Environment.NewLine &
              " case when Query.Document_Type IN ('IN','DB','RF') then  DATEDIFF (day,convert(date, Query.[Document Date],101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1  else 0 end  as [Ageing_Days], "
                End If

            End If

            strUpperQry += " Query.Document_Type ,Query.Location  , '' AS From_Vendor, '' AS To_Vendor, " &
                                " '" + Me.ddlAgedRcvbl.Text + "' AS Report_Type,  '" + Me.dtpAgeof.Value + "' AS AgeofDate,'" + strTtpe + "' as [Summary], '" + IsFifoBased + "' as [IsFifoBased]," &
                                " TSPL_COMPANY_MASTER.comp_name,TSPL_COMPANY_MASTER.Add1+case  when isnull(TSPL_COMPANY_MASTER.Add2,'')='' then '' else ', '+TSPL_COMPANY_MASTER.Add2 +case  when isnull(TSPL_COMPANY_MASTER.Add3,'')='' then '' else ', '+TSPL_COMPANY_MASTER.Add3 end end as comp_address " &
                                " from ( "


            strLowerQry1 = " ) Query " & _
                        " LEFT OUTER JOIN TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code = Query .Comp_Code " & _
                        " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =query.[Customer Id] " & _
                        " left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER .Zone_Code =TSPL_CUSTOMER_MASTER.Zone_Code " & _
                        " Where 1=1 " & _
                        " AND Query.[Document Id] not in (Select  UnApplied_No  from TSPL_RECEIPT_HEADER ForUnapliedEntry_bankReverse_Exclude where ForUnapliedEntry_bankReverse_Exclude.UnApplied_No =Query .[Document Id]  and isnull(UnApplied_No,'')<>''  and ForUnapliedEntry_bankReverse_Exclude.Receipt_No  in (  Select Document_No from TSPL_BANK_REVERSE where  TSPL_BANK_REVERSE.Source_Type ='AR' and TSPL_BANK_REVERSE.Document_No = ForUnapliedEntry_bankReverse_Exclude.Receipt_No and CONVERT(DATE,TSPL_BANK_REVERSE.Reversal_Date,103)<=CONVERT(DATE,'" & dtpAgeof.Value & "',103) and isnull(TSPL_BANK_REVERSE.Post ,'')='P')   ) " & _
                         " AND Query.[Document Id] not in ( Select distinct a.Document_No from TSPL_REVALUATION_DETAIL inner join (Select RefDocNo,Document_No   from TSPL_Customer_Invoice_Head where RefDocType ='REVALUATION ENTRY') a on a.RefDocNo =TSPL_REVALUATION_DETAIL.Document_No inner join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No =TSPL_REVALUATION_DETAIL.AR_Invoice_No  where  isnull(TSPL_REVALUATION_DETAIL.AR_Invoice_No ,'')<>'' union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and (ISNULL (Against_Sale_Return_No,'') IN (Select Document_Code from TSPL_SD_SALE_rETURN_HEAD WHERE Document_Code IN (Select Against_Sale_Return_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (Against_Sale_Return_No,'')<>'') AND ISNULL(Against_Invoice_No,'')<>'') )  union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (Against_Sale_Return_No,'')<>'' AND ISNULL(Trans_Type,'') ='BSR'  union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and (ISNULL (Against_MCC_Material_Sale_Return,'') IN (Select Document_Code from TSPL_SD_SALE_rETURN_HEAD WHERE Document_Code IN (Select Against_MCC_Material_Sale_Return  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (Against_MCC_Material_Sale_Return,'')<>'') AND ISNULL(Against_Invoice_No,'')<>'') )  union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and (ISNULL (AgainstScrapReturn,'') IN (Select Document_No from TSPL_SCRAPSALE_HEAD_RETURN WHERE Document_No in (Select AgainstScrapReturn  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (AgainstScrapReturn,'')<>'') ) ))"




            Dim dt As New DataTable
            If chkFifo.Checked Then
                StrQuery = strUpperQry + strInnerQry + strLowerQry1 + "AND 1=2"
                dt = clsDBFuncationality.GetDataTable(StrQuery)
                Dim dtCustomer As DataTable = clsDBFuncationality.GetDataTable("Select Distinct [Customer Id] from ( " + strInnerQry + " ) Customer Where [Due Amount]<>0 ")
                For Each drCustomer As DataRow In dtCustomer.Rows
                    '--------------------FIFO(-ve balance)-------------------
                    Dim strFifoQry As String = strUpperQry + strInnerQry + strLowerQry1
                    strFifoQry += " and Query.[Customer Id] = '" + clsCommon.myCstr(drCustomer("Customer Id")) + "'"
                    strFifoQry += " AND [Due Amount] < 0 "

                    Dim dtFifo As DataTable = clsDBFuncationality.GetDataTable(strFifoQry)
                    '--------------------FIFO(+ve balance)-------------------
                    Dim strFifoQry1 As String = strUpperQry + strInnerQry + strLowerQry1
                    strFifoQry1 += " and Query.[Customer Id] = '" + clsCommon.myCstr(drCustomer("Customer Id")) + "'"
                    strFifoQry1 += " AND [Due Amount] > 0 "

                    Dim dtFifo1 As DataTable = clsDBFuncationality.GetDataTable(strFifoQry1)
                    If Me.chkShowInvOnly.Checked = False Then
                        If dtFifo1.Rows.Count <= 0 And dtFifo.Rows.Count > 0 Then
                            For Each dr As DataRow In dtFifo.Rows
                                Dim dRow As DataRow = dt.NewRow()
                                dRow.ItemArray = dr.ItemArray
                                dt.Rows.Add(dRow)
                            Next
                        End If
                    End If

                    If dtFifo1.Rows.Count > 0 And dtFifo.Rows.Count <= 0 Then
                        For Each dr As DataRow In dtFifo1.Rows
                            Dim dRow As DataRow = dt.NewRow()
                            dRow.ItemArray = dr.ItemArray
                            dt.Rows.Add(dRow)
                        Next
                    End If
                    If dtFifo1.Rows.Count > 0 And dtFifo.Rows.Count > 0 Then
                        Dim NegativeAmt As Double = Math.Round(clsCommon.myCdbl(dtFifo.Compute("Sum([Due Amount])", "") * -1), 0)
                        Dim PositiveAmt As Double = Math.Round(clsCommon.myCdbl(dtFifo1.Compute("Sum([Due Amount])", "")), 0)
                        If NegativeAmt > PositiveAmt Then
                            If chkShowInvOnly.Checked = False Then
                                Dim AppliedAmt As Double = clsCommon.myCdbl(dtFifo1.Compute("Sum([Due Amount])", ""))
                                For Each dr As DataRow In dtFifo.Rows
                                    If AppliedAmt > 0 Then
                                        If (clsCommon.myCdbl(dr("Due Amount")) * -1) <= AppliedAmt Then
                                            AppliedAmt = AppliedAmt + clsCommon.myCdbl(dr("Due Amount"))
                                        Else
                                            dr.Item("Due Amount") = clsCommon.myCdbl(dr("Due Amount")) + AppliedAmt
                                            AppliedAmt = 0
                                            Dim dRow As DataRow = dt.NewRow()
                                            dRow.ItemArray = dr.ItemArray
                                            dt.Rows.Add(dRow)
                                        End If
                                    Else
                                        Dim dRow As DataRow = dt.NewRow()
                                        dRow.ItemArray = dr.ItemArray
                                        dt.Rows.Add(dRow)
                                    End If
                                Next
                            End If
                        ElseIf NegativeAmt < PositiveAmt Then
                            Dim AppliedAmt As Double = clsCommon.myCdbl(dtFifo.Compute("Sum([Due Amount])", "") * -1)
                            For Each dr As DataRow In dtFifo1.Rows
                                If AppliedAmt > 0 Then
                                    If clsCommon.myCdbl(dr("Due Amount")) <= AppliedAmt Then
                                        AppliedAmt = AppliedAmt - clsCommon.myCdbl(dr("Due Amount"))
                                    Else
                                        dr.Item("Due Amount") = clsCommon.myCdbl(dr("Due Amount")) - AppliedAmt
                                        AppliedAmt = 0
                                        Dim dRow As DataRow = dt.NewRow()
                                        dRow.ItemArray = dr.ItemArray
                                        dt.Rows.Add(dRow)
                                    End If
                                Else
                                    Dim dRow As DataRow = dt.NewRow()
                                    dRow.ItemArray = dr.ItemArray
                                    dt.Rows.Add(dRow)
                                End If
                            Next
                        End If

                    End If
                Next
            Else
                strQuery = "select  rptHeading, First_Period, Second_Period, [Third Period], [Fourth Period], [Fifth Period],[Sixth Period], [Seventh Period],[Eight Period], [Nineth Period], [Over],  Comp_Code ,[Customer Id] as [Customer Code],[Parent Code] ,ParentName ,[Customer Name] ,Zone  as [Zone],[Zone Name]  as [Zone Name],Cust_Group_Code ,Cust_Group_Desc ,[Document Id] ,[Desc]"
                If SettingAllowtoShowCreditBalanceonCustomerAgeing Then
                    strQuery += ", case when abs( [Due Amount])>0.99 then [Due Amount] else 0 end as [Due Amount]"
                Else
                    strQuery += ",[Due Amount]"
                End If
                strQuery += ",Currency ,CURRENCY_CODE ,ConvRate ,[Due Date],type ,convert(varchar,[Document Date],103) as [Document Date],[Original Amount],[Current], [Ageing_Days],  Document_Type ,Location  , From_Vendor,  To_Vendor,  Report_Type,  AgeofDate,[Summary], [IsFifoBased], comp_name, comp_address " +
                " from ( " + strUpperQry + strInnerQry + strLowerQry1
                If clsCommon.CompairString(clsCommon.myCstr(ddlCurrencyType.SelectedValue), "1") = CompairStringResult.Equal Then
                    strQuery += "  and query.CURRENCY_CODE <>TSPL_COMPANY_MASTER .BaseCurrencyCode "
                End If
                strQuery += " ) xxx "
                If TxtZone.arrValueMember IsNot Nothing AndAlso TxtZone.arrValueMember.Count > 0 Then
                    strQuery += " where xxx.Zone  in (" + clsCommon.GetMulcallString(TxtZone.arrValueMember) + ")  "
                End If
                strQuery += " order by xxx.[Customer Id],xxx.[Document Date]"
                dt = clsDBFuncationality.GetDataTable(strQuery)
            End If

            If chkType.Checked AndAlso Not chkShowZeroBalance.Checked Then
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = dt.Rows.Count - 1 To 0 Step -1
                        If clsCommon.myCdbl(dt.Rows(ii)("Due Amount")) = 0 AndAlso clsCommon.myCdbl(dt.Rows(ii)("Current")) = 0 Then
                            dt.Rows.RemoveAt(ii)
                        End If
                    Next
                End If
            Else
                If chkShowZeroBalance.Checked = False Then
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For ii As Integer = dt.Rows.Count - 1 To 0 Step -1
                            If clsCommon.myCdbl(dt.Rows(ii)("Due Amount")) = 0 AndAlso clsCommon.myCdbl(dt.Rows(ii)("Current")) = 0 Then
                                dt.Rows.RemoveAt(ii)
                            End If
                        Next
                    End If
                End If
            End If

            If Isgrid = False Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "crptAgedTrialBalance" + strNo + "", "A/R Customer Ageing Report")
                frmCRV = Nothing
            Else
                gv1.DataSource = Nothing
                gv1.DataSource = GetGridDT(dt)
                SetGridFormationOFGV1()
                ReStoreGridLayout()
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub
    Function GetGridDT(ByVal dt As DataTable) As DataTable
        Dim dtGrid As New DataTable
        Dim Over As Integer = 0
        If chkFifo.Checked OrElse (Not chkFifo.Checked AndAlso Not chkType.Checked) Then
            dtGrid.Columns.Add(dt.Columns("Parent Code").ColumnName, dt.Columns("Parent Code").DataType)
            dtGrid.Columns.Add(dt.Columns("ParentName").ColumnName, dt.Columns("ParentName").DataType)
            dtGrid.Columns.Add(dt.Columns("Customer Code").ColumnName, dt.Columns("Customer Code").DataType)
            dtGrid.Columns.Add(dt.Columns("Customer Name").ColumnName, dt.Columns("Customer Name").DataType)
            dtGrid.Columns.Add(dt.Columns("Zone").ColumnName, dt.Columns("Zone").DataType)
            dtGrid.Columns.Add(dt.Columns("Zone Name").ColumnName, dt.Columns("Zone Name").DataType)
            dtGrid.Columns.Add(dt.Columns("Cust_Group_Code").ColumnName, dt.Columns("Cust_Group_Code").DataType)
            dtGrid.Columns.Add(dt.Columns("Cust_Group_Desc").ColumnName, dt.Columns("Cust_Group_Desc").DataType)
            dtGrid.Columns.Add(dt.Columns("Document Id").ColumnName, dt.Columns("Document Id").DataType)
            dtGrid.Columns.Add(dt.Columns("Document Date").ColumnName, dt.Columns("Document Date").DataType)
            dtGrid.Columns.Add("Document Type", dt.Columns("Document_Type").DataType)
            dtGrid.Columns.Add(dt.Columns("Location").ColumnName, dt.Columns("Location").DataType)
            dtGrid.Columns.Add(dt.Columns("Currency").ColumnName, dt.Columns("Currency").DataType)
            dtGrid.Columns.Add(dt.Columns("Original Amount").ColumnName, dt.Columns("Original Amount").DataType)
            dtGrid.Columns.Add(dt.Columns("Current").ColumnName, dt.Columns("Current").DataType)


            dtGrid.Columns.Add(dt.Columns("Due Date").ColumnName, dt.Columns("Due Date").DataType)

            dtGrid.Columns.Add(New DataColumn("" & Val(Me.txtCurr.Text) & "-" & Val(Me.txt1.Text) & " Days", dt.Columns("Due Amount").DataType))
            Over = Val(Me.txt1.Text)
            If clsCommon.myLen(txt1.Text) > 0 And clsCommon.myLen(txt2.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt1.Text) + 1 & "-" & Val(Me.txt2.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt2.Text)
            End If
            If clsCommon.myLen(txt2.Text) > 0 And clsCommon.myLen(txt3.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt2.Text) + 1 & "-" & Val(Me.txt3.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt3.Text)
            End If
            If clsCommon.myLen(txt3.Text) > 0 And clsCommon.myLen(txt4.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt3.Text) + 1 & "-" & Val(Me.txt4.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt4.Text)
            End If
            If clsCommon.myLen(txt4.Text) > 0 And clsCommon.myLen(txt5.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt4.Text) + 1 & "-" & Val(Me.txt5.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt5.Text)
            End If
            If clsCommon.myLen(txt5.Text) > 0 And clsCommon.myLen(txt6.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt5.Text) + 1 & "-" & Val(Me.txt6.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt6.Text)
            End If
            If clsCommon.myLen(txt6.Text) > 0 And clsCommon.myLen(txt7.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt6.Text) + 1 & "-" & Val(Me.txt7.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt7.Text)
            End If
            If clsCommon.myLen(txt7.Text) > 0 And clsCommon.myLen(txt8.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt7.Text) + 1 & "-" & Val(Me.txt8.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt8.Text)
            End If
            If clsCommon.myLen(txt8.Text) > 0 And clsCommon.myLen(txtOvr.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt8.Text) + 1 & "-" & Val(Me.txtOvr.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txtOvr.Text)
            End If
            dtGrid.Columns.Add(New DataColumn("Over " & Over & " Days", dt.Columns("Due Amount").DataType))
            dtGrid.Columns.Add(New DataColumn("Total Amount", dt.Columns("Due Amount").DataType))

            For Each dr As DataRow In dt.Rows
                dtGrid.Rows.Add()
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Parent Code") = dr.Item("Parent Code")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("ParentName") = dr.Item("ParentName")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Customer Code") = dr.Item("Customer Code")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Customer Name") = dr.Item("Customer Name")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Zone") = dr.Item("Zone")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Zone Name") = dr.Item("Zone Name")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Cust_Group_Code") = dr.Item("Cust_Group_Code")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Cust_Group_Desc") = dr.Item("Cust_Group_Desc")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Document Id") = dr.Item("Document Id")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Document Date") = dr.Item("Document Date")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Document Type") = dr.Item("Document_Type")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Location") = dr.Item("Location")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Currency") = dr.Item("Currency")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Due Date") = dr.Item("Due Date")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Original Amount") = dr.Item("Original Amount")
                dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Current") = dr.Item("Current")
                Dim total As Decimal = 0

                If dr.Item("Ageing_Days") <= Val(dr.Item("Second_Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txtCurr.Text) & "-" & Val(Me.txt1.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Third Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt1.Text) + 1 & "-" & Val(Me.txt2.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Fourth Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt2.Text) + 1 & "-" & Val(Me.txt3.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Fifth Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt3.Text) + 1 & "-" & Val(Me.txt4.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Sixth Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt4.Text) + 1 & "-" & Val(Me.txt5.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Seventh Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt5.Text) + 1 & "-" & Val(Me.txt6.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Eight Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt6.Text) + 1 & "-" & Val(Me.txt7.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Nineth Period")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt7.Text) + 1 & "-" & Val(Me.txt8.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Over")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt8.Text) + 1 & "-" & Val(Me.txtOvr.Text) & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                ElseIf dr.Item("Ageing_Days") > Val(dr.Item("Over")) Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Over " & Over & " Days") = dr.Item("Due Amount")
                    total = total + dr.Item("Due Amount")
                End If
                If SettingAllowtoShowCreditBalanceonCustomerAgeing Then
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = IIf(total < 0, 0.0, total)
                Else
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = IIf(total = 0, (dr.Item("Current")), total)
                End If
            Next
        Else
            dtGrid.Columns.Add(dt.Columns("Parent Code").ColumnName, dt.Columns("Parent Code").DataType)
            dtGrid.Columns.Add(dt.Columns("ParentName").ColumnName, dt.Columns("ParentName").DataType)
            dtGrid.Columns.Add(dt.Columns("Customer Code").ColumnName, dt.Columns("Customer Code").DataType)
            dtGrid.Columns.Add(dt.Columns("Customer Name").ColumnName, dt.Columns("Customer Name").DataType)
            dtGrid.Columns.Add(dt.Columns("Zone").ColumnName, dt.Columns("Zone").DataType)
            dtGrid.Columns.Add(dt.Columns("Zone Name").ColumnName, dt.Columns("Zone Name").DataType)
            dtGrid.Columns.Add(dt.Columns("Cust_Group_Code").ColumnName, dt.Columns("Cust_Group_Code").DataType)
            dtGrid.Columns.Add(dt.Columns("Cust_Group_Desc").ColumnName, dt.Columns("Cust_Group_Desc").DataType)
            dtGrid.Columns.Add(dt.Columns("Current").ColumnName, dt.Columns("Current").DataType)

            dtGrid.Columns.Add(New DataColumn("" & Val(Me.txtCurr.Text) & "-" & Val(Me.txt1.Text) & " Days", dt.Columns("Due Amount").DataType))
            Over = Val(Me.txt1.Text)
            If clsCommon.myLen(txt1.Text) > 0 And clsCommon.myLen(txt2.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt1.Text) + 1 & "-" & Val(Me.txt2.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt2.Text)
            End If
            If clsCommon.myLen(txt2.Text) > 0 And clsCommon.myLen(txt3.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt2.Text) + 1 & "-" & Val(Me.txt3.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt3.Text)
            End If
            If clsCommon.myLen(txt3.Text) > 0 And clsCommon.myLen(txt4.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt3.Text) + 1 & "-" & Val(Me.txt4.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt4.Text)
            End If
            If clsCommon.myLen(txt4.Text) > 0 And clsCommon.myLen(txt5.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt4.Text) + 1 & "-" & Val(Me.txt5.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt5.Text)
            End If
            If clsCommon.myLen(txt5.Text) > 0 And clsCommon.myLen(txt6.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt5.Text) + 1 & "-" & Val(Me.txt6.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt6.Text)
            End If
            If clsCommon.myLen(txt6.Text) > 0 And clsCommon.myLen(txt7.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt6.Text) + 1 & "-" & Val(Me.txt7.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt7.Text)
            End If
            If clsCommon.myLen(txt7.Text) > 0 And clsCommon.myLen(txt8.Text) > 0 Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt7.Text) + 1 & "-" & Val(Me.txt8.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txt8.Text)
            End If
            If clsCommon.myLen(txt8.Text) > 0 And clsCommon.myLen(txtOvr.Text) > 0 And clsCommon.CompairString(clsCommon.myCstr(txt8.Text), clsCommon.myCstr(txtOvr.Text)) <> CompairStringResult.Equal Then
                dtGrid.Columns.Add(New DataColumn("" & Val(Me.txt8.Text) + 1 & "-" & Val(Me.txtOvr.Text) & " Days", dt.Columns("Due Amount").DataType))
                Over = Val(Me.txtOvr.Text)
            End If
            dtGrid.Columns.Add(New DataColumn("Over " & Over & " Days", dt.Columns("Due Amount").DataType))
            dtGrid.Columns.Add(New DataColumn("Total Amount", dt.Columns("Due Amount").DataType))
            Dim xoldcustcode As String = ""
            Dim total As Decimal = 0
            Dim amt1 As Decimal = 0
            Dim amt2 As Decimal = 0
            Dim amt3 As Decimal = 0
            Dim amt4 As Decimal = 0
            Dim amt5 As Decimal = 0
            Dim amt6 As Decimal = 0
            Dim amt7 As Decimal = 0
            Dim amt8 As Decimal = 0
            Dim amt9 As Decimal = 0
            Dim amt10 As Decimal = 0
            If chkType.Checked Then
                Dim ROWCOUNTING As Integer = 0
                For Each dr As DataRow In dt.Rows
                    If clsCommon.CompairString(xoldcustcode, clsCommon.myCstr(dr.Item("Customer Code"))) <> CompairStringResult.Equal Then
                        total = 0
                        amt1 = 0
                        amt2 = 0
                        amt3 = 0
                        amt4 = 0
                        amt5 = 0
                        amt6 = 0
                        amt7 = 0
                        amt8 = 0
                        amt9 = 0
                        amt10 = 0
                        dtGrid.Rows.Add()
                    End If

                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Parent Code") = dr.Item("Parent Code")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("ParentName") = dr.Item("ParentName")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Customer Code") = dr.Item("Customer Code")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Customer Name") = dr.Item("Customer Name")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Zone") = dr.Item("Zone")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Zone Name") = dr.Item("Zone Name")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Cust_Group_Code") = dr.Item("Cust_Group_Code")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Cust_Group_Desc") = dr.Item("Cust_Group_Desc")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Current") = clsCommon.myCdbl(dtGrid.Rows(dtGrid.Rows.Count - 1)("Current")) + dr.Item("Current")
                    xoldcustcode = dr.Item("Customer Code")



                    If dr.Item("Ageing_Days") <= Val(dr.Item("Second_Period")) Then
                        amt1 += clsCommon.myCdbl(dr.Item("Due Amount"))
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txtCurr.Text) & "-" & Val(Me.txt1.Text) & " Days") = amt1
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Third Period")) Then
                        amt2 += clsCommon.myCdbl(dr.Item("Due Amount"))
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt1.Text) + 1 & "-" & Val(Me.txt2.Text) & " Days") = amt2
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Fourth Period")) Then
                        amt3 += clsCommon.myCdbl(dr.Item("Due Amount"))
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt2.Text) + 1 & "-" & Val(Me.txt3.Text) & " Days") = amt3
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Fifth Period")) Then
                        amt4 += clsCommon.myCdbl(dr.Item("Due Amount"))
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt3.Text) + 1 & "-" & Val(Me.txt4.Text) & " Days") = amt4
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Sixth Period")) Then
                        amt5 += clsCommon.myCdbl(dr.Item("Due Amount"))
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt4.Text) + 1 & "-" & Val(Me.txt5.Text) & " Days") = amt5
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Seventh Period")) Then
                        amt6 += clsCommon.myCdbl(dr.Item("Due Amount"))
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt5.Text) + 1 & "-" & Val(Me.txt6.Text) & " Days") = amt6
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Eight Period")) Then
                        amt7 += clsCommon.myCdbl(dr.Item("Due Amount"))
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt6.Text) + 1 & "-" & Val(Me.txt7.Text) & " Days") = amt7
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Nineth Period")) Then
                        amt8 += clsCommon.myCdbl(dr.Item("Due Amount"))
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt7.Text) + 1 & "-" & Val(Me.txt8.Text) & " Days") = amt8
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Over")) Then
                        amt9 += clsCommon.myCdbl(dr.Item("Due Amount"))
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt8.Text) + 1 & "-" & Val(Me.txtOvr.Text) & " Days") = amt9
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") > Val(dr.Item("Over")) Then
                        amt10 += clsCommon.myCdbl(dr.Item("Due Amount"))
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Over " & Over & " Days") = amt10
                        total = total + dr.Item("Due Amount")
                    End If

                    ROWCOUNTING = ROWCOUNTING + 1

                    If SettingAllowtoShowCreditBalanceonCustomerAgeing Then
                        If ROWCOUNTING = 1 Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = IIf(dr.Item("Current") < 0, 0, Math.Round((dr.Item("Current") + dr.Item("Due Amount")), 2))
                        Else
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = Math.Round((clsCommon.myCdbl(dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount")) + IIf(dr.Item("Current") < 0, 0, dr.Item("Current")) + IIf(dr.Item("Due Amount") < 0, 0, dr.Item("Due Amount"))), 2)
                        End If
                    Else
                        If ROWCOUNTING = 1 Then
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = Math.Round((dr.Item("Current") + dr.Item("Due Amount")), 2)
                        Else
                            dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = Math.Round((clsCommon.myCdbl(dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount")) + dr.Item("Current") + dr.Item("Due Amount")), 2)
                        End If
                    End If
                    total = dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount")

                Next
            Else
                For Each dr As DataRow In dt.Rows
                    dtGrid.Rows.Add()
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Parent Code") = dr.Item("Parent Code")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("ParentName") = dr.Item("ParentName")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Customer Code") = dr.Item("Customer Code")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Customer Name") = dr.Item("Customer Name")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Cust_Group_Code") = dr.Item("Cust_Group_Code")
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Cust_Group_Desc") = dr.Item("Cust_Group_Desc")
                    total = 0
                    If dr.Item("Ageing_Days") <= Val(dr.Item("Second_Period")) Then
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txtCurr.Text) & "-" & Val(Me.txt1.Text) & " Days") = dr.Item("Due Amount")
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Third Period")) Then
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt1.Text) + 1 & "-" & Val(Me.txt2.Text) & " Days") = dr.Item("Due Amount")
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Fourth Period")) Then
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt2.Text) + 1 & "-" & Val(Me.txt3.Text) & " Days") = dr.Item("Due Amount")
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Fifth Period")) Then
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt3.Text) + 1 & "-" & Val(Me.txt4.Text) & " Days") = dr.Item("Due Amount")
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Sixth Period")) Then
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt4.Text) + 1 & "-" & Val(Me.txt5.Text) & " Days") = dr.Item("Due Amount")
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Seventh Period")) Then
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt5.Text) + 1 & "-" & Val(Me.txt6.Text) & " Days") = dr.Item("Due Amount")
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Eight Period")) Then
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt6.Text) + 1 & "-" & Val(Me.txt7.Text) & " Days") = dr.Item("Due Amount")
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Nineth Period")) Then
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt7.Text) + 1 & "-" & Val(Me.txt8.Text) & " Days") = dr.Item("Due Amount")
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") <= Val(dr.Item("Over")) Then
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("" & Val(Me.txt8.Text) + 1 & "-" & Val(Me.txtOvr.Text) & " Days") = dr.Item("Due Amount")
                        total = total + dr.Item("Due Amount")
                    ElseIf dr.Item("Ageing_Days") > Val(dr.Item("Over")) Then
                        dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Over " & Over & " Days") = dr.Item("Due Amount")
                        total = total + dr.Item("Due Amount")
                    End If
                    dtGrid.Rows(dtGrid.Rows.Count - 1).Item("Total Amount") = total
                Next
            End If

        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Dim TotalAmt As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TotalAmt)
        If SettingAllowtoShowCreditBalanceonCustomerAgeing Then
            Dim current As New GridViewSummaryItem("Current", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(current)
            Dim i As Integer = 0
            If chkType.Checked Then
                For i = 7 To dtGrid.Columns.Count - 2
                    Dim bucketcolumn As New GridViewSummaryItem(dtGrid.Columns(i).ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(bucketcolumn)
                Next
            Else
                For i = 13 To dtGrid.Columns.Count - 2
                    Dim bucketcolumn As New GridViewSummaryItem(dtGrid.Columns(i).ColumnName, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(bucketcolumn)
                Next
            End If
        End If

        gv1.MasterTemplate.AutoExpandGroups = True
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        dtGrid.AcceptChanges()
        Return dtGrid
    End Function


    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False

        gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"
        gv1.Columns("Cust_Group_Desc").HeaderText = "Customer Group Description"

        If chkType.Checked = True Then
            gv1.Columns("Current").HeaderText = "Adv/On-Ac/Credit Note/UnApplied"
        End If

        If ConsiderOpeningDocintoBucketsonCustomerAgeing = True Then
            gv1.Columns("Current").IsVisible = False
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        RadPageView1.SelectedPage = RadPageViewPage2
        gv1.BestFitColumns()
    End Sub

    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewDataRowInfo) Then
        End If
    End Sub


    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub


    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        gv1.EnableFiltering = True
        PageSetupReport_ID = MyBase.Form_ID
        print(True)
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveLayout.Click
        If clsCommon.myLen(GetReportID()) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = GetReportID()
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Public Function GetReportID() As String
        Dim strSummary As String = String.Empty
        If chkType.Checked = True Then
            If clsCommon.CompairString(ddlAgedRcvbl.Text, "Aged Trial Balance By Document date") = CompairStringResult.Equal Then
                strSummary = "rptCustAgingSummaryDocDate"
            Else
                strSummary = "rptCustAgingSummaryDueDate"
            End If
        Else
            If clsCommon.CompairString(ddlAgedRcvbl.Text, "Aged Trial Balance By Document date") = CompairStringResult.Equal Then
                strSummary = "rptCustAgingDetailDocDate"
            Else
                strSummary = "rptCustAgingDetailDueDate"
            End If
        End If
        Return strSummary
    End Function

    Private Sub btnDeleteLayour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteLayour.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(GetReportID()) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(GetReportID(), "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rdbWoFOC_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)

    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv1.CellDoubleClick
        If chkType.Checked = False Then
            If gv1.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Document Type").Value)
                Dim strDoc = gv1.CurrentRow.Cells("Document Id").Value

                Select Case strTransType
                    Case "IN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, strDoc)
                    Case "VGCL"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnVCGLEntry, strDoc)
                    Case "RC"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, strDoc)
                    Case "OA"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, strDoc)
                    Case "AV"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, strDoc)
                    Case "UC"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, strDoc)
                    Case "SR"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, strDoc)
                    Case "CR"
                        'clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, strDoc)
                    Case "DR"
                        'clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.saleReturn, strDoc)
                End Select
            End If
        End If
    End Sub
    Private Sub chkVendorSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub
    Private Sub ChkParentCustSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkParentCustSelect.ToggleStateChanged
        cbgParentCust.Enabled = Not ChkParentCustAll.IsChecked
    End Sub

    Private Sub txt1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt1.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt2.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt3.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt4.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt5.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt6_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt6.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt7_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt7.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub
    Private Sub txt8_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt8.KeyPress
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            Return
        End If
    End Sub

    Private Sub txt8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt8.TextChanged
        Me.txtOvr.Text = Me.txt8.Text
    End Sub

    Private Sub chkCusSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLOcAll.IsChecked
    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        LoadParentCustomer()
        LoadCustomer()
    End Sub

    Private Sub chkInactive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInactive.CheckedChanged
        LoadParentCustomer()
        LoadCustomer()
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        LoadCustomer()
    End Sub


    Private Sub chkCGAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCGAll.ToggleStateChanged
        cbgCustomerGroup.Enabled = False
    End Sub

    Private Sub chkCGSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCGSelect.ToggleStateChanged
        cbgCustomerGroup.Enabled = True
    End Sub


    Private Sub chkType_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkType.ToggleStateChanged
        chkFifo.Checked = False
        If chkType.Checked Then
            chkFifo.Enabled = False
        Else
            chkFifo.Enabled = True
        End If
        chkShowZeroBalance.Visible = chkType.Checked
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        print(False)
    End Sub

    Private Sub cbgParentCust__MyCheckChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbgParentCust._MyCheckChanged
        Try
            If cbgParentCust.Enabled = True AndAlso cbgParentCust.CheckedValue.Count > 0 Then
                Dim values As String = clsCommon.GetMulcallString(cbgParentCust.CheckedValue)

                strQuery = "Select Cust_Code As [Code],ISNULL(Customer_Name,'') As [Description] From TSPL_CUSTOMER_MASTER Where Parent_Customer_No In (" + values + ") "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)

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

    Private Sub ChkISParentCust_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkISParentCust.CheckStateChanged
        LoadCustomer()
        chkCustomerAll.IsChecked = True
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQuery = "select xxx.Loc_Segment_Code as Code,TSPL_GL_SEGMENT_CODE.Description as Name  from"
        strQuery += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
        strQuery += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'"
        strQuery += " order by xxx.Loc_Segment_Code"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("MSAgeCus", strQuery, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub txtCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomerGroup._My_Click
        strQuery = "Select Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("MSAgeCusGrp", strQuery, "Code", "Name", txtCustomerGroup.arrValueMember, txtCustomerGroup.arrDispalyMember)
    End Sub
    Sub LoadCustomerNew()
        strQuery = "select CM1.cust_code as Code, CM1.Customer_Name as Name, Case When ISNULL(CM2.Cust_Code,'')<>'' Then ISNULL(CM2.Cust_Code,'')+' - '+ISNULL(CM2.Customer_Name,'') Else '' End as [ParentCustomer]  from tspl_customer_master CM1 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM2 ON CM2.Cust_Code=CM1.Parent_Customer_No  where 1=1"
        If chkActive.Checked Then
            strQuery += " and CM1.Status='N'"
        ElseIf chkInactive.Checked Then
            strQuery += " and CM1.Status='Y'"
        End If
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserCustomers) > 0 Then
            strQuery += " and CM1.cust_code in (" + objCommonVar.strCurrUserCustomers + ") "
        End If

        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQuery, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub TxtMultiCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCustomerCategory._My_Click
        Dim qry As String = " select cust_category_code as [Code], CUST_CATEGORY_DESC as [Desc] from TSPL_CUSTOMER_CATEGORY_MASTER "
        TxtMultiCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustCatMSel8", qry, "Code", "Desc", TxtMultiCustomerCategory.arrValueMember, TxtMultiCustomerCategory.arrDispalyMember)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        LoadCustomerNew()
    End Sub


    Private Sub LoadCurrencyType()
        dt = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("ConvRate", "Functional Currency")
        dt.Rows.Add("1", "Customer Currency")
        ddlCurrencyType.DataSource = dt
        ddlCurrencyType.ValueMember = "Code"
        ddlCurrencyType.DisplayMember = "Name"
    End Sub


    
    Private Sub dtpAgeof_ValueChanged(sender As Object, e As EventArgs) Handles dtpAgeof.ValueChanged
        dtpCutoffDate.Value = dtpAgeof.Value
    End Sub



    Private Sub mniExcel_Click(sender As Object, e As EventArgs) Handles mniExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmCustomerAgeing & "'"))
            arrHeader.Add("Age As Of : " + clsCommon.GetPrintDate(dtpAgeof.Value, "dd/MM/yyyy"))
            arrHeader.Add("Cut Off Date : " + clsCommon.GetPrintDate(dtpCutoffDate.Value, "dd/MM/yyyy"))

            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmCustomerAgeing & "'"))
            arrHeader.Add("Age As Of : " + clsCommon.GetPrintDate(dtpAgeof.Value, "dd/MM/yyyy"))
            arrHeader.Add("Cut Off Date : " + clsCommon.GetPrintDate(dtpCutoffDate.Value, "dd/MM/yyyy"))

            If clsCommon.myLen(ddlAgedRcvbl.Text) > 0 Then
                arrHeader.Add("Report Type : " + ddlAgedRcvbl.Text)
            End If

            If clsCommon.myLen(ddlCurrencyType.Text) > 0 Then
                arrHeader.Add("Currency : " + ddlCurrencyType.Text)
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember))
            End If

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Category : " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrValueMember))
            End If

            If TxtZone.arrValueMember IsNot Nothing AndAlso TxtZone.arrValueMember.Count > 0 Then
                arrHeader.Add("Zone : " + clsCommon.GetMulcallStringWithComma(TxtZone.arrDispalyMember))
            End If

            clsCommon.MyExportToPDF("Customer Ageing", gv1, arrHeader, "Customer Ageing", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

  
    Private Sub ExcelGrid_Click(sender As Object, e As EventArgs) Handles ExcelGrid.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmCustomerAgeing & "'"))
            arrHeader.Add("Age As Of : " + clsCommon.GetPrintDate(dtpAgeof.Value, "dd/MM/yyyy"))
            arrHeader.Add("Cut Off Date : " + clsCommon.GetPrintDate(dtpCutoffDate.Value, "dd/MM/yyyy"))
            If clsCommon.myLen(ddlAgedRcvbl.Text) > 0 Then
                arrHeader.Add("Report Type : " + ddlAgedRcvbl.Text)
            End If

            If clsCommon.myLen(ddlCurrencyType.Text) > 0 Then
                arrHeader.Add("Currency : " + ddlCurrencyType.Text)
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember))
            End If

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Category : " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrValueMember))
            End If
            If TxtZone.arrValueMember IsNot Nothing AndAlso TxtZone.arrValueMember.Count > 0 Then
                arrHeader.Add("Zone : " + clsCommon.GetMulcallStringWithComma(TxtZone.arrDispalyMember))
            End If

            clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDFGrid_Click(sender As Object, e As EventArgs) Handles PDFGrid.Click
        Try
            Dim FilePath As String = "C:\\ERPTempFolder\\" + Me.Text + clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmss") + ".pdf"
            Dim pdfExporter As New ExportToPDF(gv1)
            pdfExporter.Font = New System.Drawing.Font("Verdana", 6)
            pdfExporter.TableBorderThickness = 1
            pdfExporter.FitToPageWidth = True
            pdfExporter.ExportVisualSettings = True
            pdfExporter.ExportHierarchy = True
            pdfExporter.HiddenColumnOption = HiddenOption.DoNotExport
            pdfExporter.PageTitle = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmCustomerAgeing & "'"))
            pdfExporter.RunExport(FilePath)
            System.Diagnostics.Process.Start(FilePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub TxtZone__My_Click(sender As Object, e As EventArgs) Handles TxtZone._My_Click
        Dim strQry As String = "select Zone_Code as Code ,Description as Name from TSPL_ZONE_MASTER where 1=1"
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            strQry += " and TSPL_ZONE_MASTER. Zone_Code in (Select TSPL_CUSTOMER_MASTER.Zone_Code from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") )"
        End If
        TxtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneMulSel", strQry, "Code", "Name", TxtZone.arrValueMember, TxtZone.arrDispalyMember)

    End Sub
End Class
