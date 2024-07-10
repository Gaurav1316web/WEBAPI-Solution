'' KDI/07/05/18-000301,KDI/09/04/18-000202
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI

Public Class FrmRptCustomerLedgerDemo
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
    Dim ShowCustomerLedgerasPerRightsForLocation As Boolean = True
    Dim StrPermission As String
    Dim strCusMappingByLocForCurUser As String = ""

#End Region

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FormType = formid
    End Sub

    Private Sub frmRptCustomerLedger_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPrintFlag Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
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

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadCurrencyType()
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
            StrPermission = objCommonVar.strCurrUserLocationsSegment
        Else
            StrPermission = clsERPFuncationality.UserWiseAvailableLocationSegment()
        End If

        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        chkActive.Checked = True
        rbtnNone.Checked = True
        ChkISParentCust.Checked = False
        GrpIsParent.Enabled = False
        chkCumulativeClosing.Checked = True
        ChkDocSumm.Enabled = False
        lblParentCustomer.Enabled = False
        txtParentCustomer.Enabled = False
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        gvCustomerGroup.Dock = DockStyle.Fill
        gvCustomer.Dock = DockStyle.Fill
        gvDetails.Dock = DockStyle.Fill

        gvCustomerGroup.Visible = False
        gvCustomer.Visible = False
        btnBack.Enabled = False
        TxtSecurity.Enabled = False
        ''richa ERO/08/11/19-001095 18 Nov,2019
        'If FormType = clsUserMgtCode.MISDebtorReport Then
        '    rbtnCustGroupWise.Checked = True
        '    pnlActiveInActiveCustomer.Visible = False
        '    chkAll.Checked = True
        'End If
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Description FROM TSPL_FIXED_PARAMETER WHERE TYPE='Industry Type'")), "D") = CompairStringResult.Equal Then
            TxtSecurity.Visible = True
            LblSecurity.Visible = True
        Else
            TxtSecurity.Visible = False
            LblSecurity.Visible = False
        End If

        'If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
        '    ReportID = "MISDebtorReport"
        'Else
        '    ReportID = "CustomerLedgerReport"
        'End If
        ReportID = GetReportID()
        rbtnDocWise.Visible = False
        btnPrint.Enabled = False
        RadPageView1.SelectedPage = RadPageViewPage1
        isRunDoubleClick = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoubleClickOnVC, clsFixedParameterCode.DoubleClickOnVC, Nothing)) = 1, True, False)
        Dim AllowTrasactionFilterOnCustomerLedger As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowTransactionFiltersOnCustomerlegder, clsFixedParameterCode.AllowTransactionFiltersOnCustomerlegder, Nothing)) = 1, True, False))
        If AllowTrasactionFilterOnCustomerLedger = True Then
            GroupBox2.Visible = True
            chkAdjustment.Visible = True
            chkDebitNote.Visible = True
            chkCreditNote.Visible = True
            chkApplyDocument.Visible = True
            ChkInvoice.Visible = True
            chkOnAccount.Visible = True
            chkAdvance.Visible = True
            chkreceipt.Visible = True
            chkrefund.Visible = True
            chkBankReverse.Visible = True
            ChkUnapplied.Visible = True

            FilterOFDocumnetType()
            chkIncludeApplyDocument.Enabled = True
            chkIncludeApplyDocument.Visible = False
            chkIncludeApplyDocument.Checked = True
        Else
            GroupBox2.Visible = False
            chkAdjustment.Visible = False
            chkDebitNote.Visible = False
            chkCreditNote.Visible = False
            chkApplyDocument.Visible = False
            ChkInvoice.Visible = False
            chkOnAccount.Visible = False
            chkAdvance.Visible = False
            chkreceipt.Visible = False
            chkrefund.Visible = False
            chkBankReverse.Visible = False
            ChkUnapplied.Visible = False

            chkAdjustment.Checked = False
            chkDebitNote.Checked = False
            chkCreditNote.Checked = False
            chkApplyDocument.Checked = False
            ChkInvoice.Checked = False
            chkOnAccount.Checked = False
            chkAdvance.Checked = False
            chkreceipt.Checked = False
            chkrefund.Checked = False
            chkBankReverse.Checked = False
            ChkUnapplied.Checked = False

            chkIncludeApplyDocument.Enabled = True
            chkIncludeApplyDocument.Visible = True
            chkIncludeApplyDocument.Checked = False
        End If
        ShowCustomerLedgerasPerRightsForLocation = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCustomerLedgerasPerRightsForLocation, clsFixedParameterCode.ShowCustomerLedgerasPerRightsForLocation, Nothing)) = 1, True, False))

        '-- done by richa agarwal related to ticket no. KDI/12/03/18-000107 on 13 Mar,2018
        Dim AllowtoMakeApplyDocOnbyDefault As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoMakeApplyDocOnbyDefault, clsFixedParameterCode.AllowtoMakeApplyDocOnbyDefault, Nothing)) = 1, True, False))
        If AllowtoMakeApplyDocOnbyDefault = True Then
            chkIncludeApplyDocument.Checked = True
        End If
        chkDateWise.Visible = False
        chkMonthWise.Visible = False
        chkIncludeCardIndent.Checked = True
        AllowtoSHOWParentChildCustomer = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSHOWParentChildCustomer, clsFixedParameterCode.AllowtoSHOWParentChildCustomer, Nothing)) = 1, True, False))
        strCusMappingByLocForCurUser = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'''' + ( Customer_Code ) +''''  as Alies_Name FROM TSPL_CUSTOMER_LOCATION_MAPPING where Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Loc_Segment_Code   in ( " + objCommonVar.strCurrUserLocationsSegment + " ) )  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnExportToExcel.Visible = MyBase.isExport
        'btnQExport.Visible = MyBase.isQuickExportFlag
        'radbtnBulkExp.Visible = MyBase.isQuickExportFlag
        btnQExport.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        isExportToExcel = False
        GC.Collect()
        If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
            PrintForCustomerCurrency()
        Else
            print()
        End If
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

    Sub print(Optional ByVal BulkExport As Integer = 0, Optional ByVal CustomerLedgerPrintConfirmation As Boolean = False)
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
        Dim strCustomerDaywise As String = String.Empty
        Try
            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer")
                Return
            End If
            If chkCompanySelect.IsChecked AndAlso cbgCompany.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one company")
                Return
            End If
            If chkCustGrpSelect.IsChecked AndAlso cbgCustGrp.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer Group")
                Return
            End If
            '' Anubhooti 30-Sep-2014 BM00000003557
            If ChkISParentCust.Checked = True Then
                If ChkParentCustSelect.IsChecked AndAlso cbgParentCust.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select at least one parent customer")
                    Return
                End If
            End If

            ''
            If ChkCustTypeSelect.IsChecked AndAlso cbgcusttype.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer Type")
                Return
            End If
            If ChkCustCatSelect.IsChecked AndAlso cbgcustcat.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer Category")
                Return
            End If

            Dim strCustCatforCurrentUser As String = ""

            strCustCatforCurrentUser = Xtra.CustomerCategory()

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

            If btnrefresh = True OrElse CustomerLedgerPrintConfirmation = True Then
                If TxtSecurity.arrValueMember IsNot Nothing AndAlso TxtSecurity.arrValueMember.Count > 0 Then
                    strsecurity = clsCommon.GetMulcallString(TxtSecurity.arrValueMember)
                Else
                    strsecurity = ""
                End If
                If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                    strcustomerfilter = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                Else
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
                        strcustomerfilter = strCusMappingByLocForCurUser
                    End If
                End If

                If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                    BaseQry = clsCustomerMaster.GetCustomerBaseQry(chkItemWise.Checked, ChkSecurity.Checked, strsecurity, rbtnDocWise.Checked, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), strcustomerfilter, False, strFromDate, strToDate, False, True, chkIncludeApplyDocument.Checked, Nothing, chkIncludeCardIndent.Checked)
                    BaseQryOPENINGINCASEOFMIS = clsCustomerMaster.GetCustomerBaseQry(chkItemWise.Checked, ChkSecurity.Checked, strsecurity, rbtnDocWise.Checked, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), strcustomerfilter, True, strFromDate, strToDate, False, False, chkIncludeApplyDocument.Checked, Nothing, chkIncludeCardIndent.Checked)
                Else
                    BaseQry = clsCustomerMaster.GetCustomerBaseQry(chkItemWise.Checked, ChkSecurity.Checked, strsecurity, rbtnDocWise.Checked, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), strcustomerfilter, False, strFromDate, strToDate, False, False, chkIncludeApplyDocument.Checked, Nothing, chkIncludeCardIndent.Checked)
                    BaseQryOPENINGINCASEOFMIS = clsCustomerMaster.GetCustomerBaseQry(chkItemWise.Checked, ChkSecurity.Checked, strsecurity, rbtnDocWise.Checked, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), strcustomerfilter, True, strFromDate, strToDate, False, False, chkIncludeApplyDocument.Checked, Nothing, chkIncludeCardIndent.Checked)
                End If
                BaseQry += " WHERE 1=1"
                BaseQryOPENINGINCASEOFMIS += " WHERE 1=1"

                '-------richa fetch data according to customer category rights of user---------
                If clsCommon.myLen(strCustCatforCurrentUser) > 0 Then
                    BaseQry += " and isnull(TSPL_CUSTOMER_MASTER.customer_category,'') in (" + strCustCatforCurrentUser + ")"
                    BaseQryOPENINGINCASEOFMIS += " and isnull(TSPL_CUSTOMER_MASTER.customer_category,'') in (" + strCustCatforCurrentUser + ")"
                End If
                ''richa Agarwal 27 Oct,2017 
                Dim AllowTrasactionFilterOnCustomerLedger As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowTransactionFiltersOnCustomerlegder, clsFixedParameterCode.AllowTransactionFiltersOnCustomerlegder, Nothing)) = 1, True, False))
                If AllowTrasactionFilterOnCustomerLedger = True Then
                    'BaseQry += " WHERE 1=1"
                    'BaseQryOPENINGINCASEOFMIS += " WHERE 1=1"

                    Dim StrCONDITION As String = String.Empty
                    If chkAdjustment.Checked = True Then
                        StrCONDITION += "'Adjustment',"
                    End If
                    If chkApplyDocument.Checked = True Then
                        StrCONDITION += "'IM',"
                    End If
                    If chkCreditNote.Checked = True Then
                        StrCONDITION += "'CR',"
                    End If
                    If chkDebitNote.Checked = True Then
                        StrCONDITION += "'DR',"
                    End If
                    If ChkInvoice.Checked = True Then
                        StrCONDITION += "'IN',"
                    End If
                    If chkOnAccount.Checked = True Then
                        StrCONDITION += "'OA',"
                    End If
                    If chkAdvance.Checked = True Then
                        StrCONDITION += "'PR',"
                    End If
                    If chkreceipt.Checked = True Then
                        StrCONDITION += "'RC',"
                    End If
                    If chkrefund.Checked = True Then
                        StrCONDITION += "'RF',"
                    End If
                    If chkBankReverse.Checked = True Then
                        StrCONDITION += "'RV-TA',"
                    End If
                    If ChkUnapplied.Checked = True Then
                        StrCONDITION += "'UA',"
                    End If
                    If clsCommon.myLen(StrCONDITION) > 0 Then
                        StrCONDITION += "'EXC'"

                        BaseQry += " AND InnQuery.DocType in (" & StrCONDITION & ") "
                        BaseQryOPENINGINCASEOFMIS += " AND InnQuery.DocType in (" & StrCONDITION & ") "
                    Else
                        BaseQry += " AND InnQuery.DocType in ('') "
                        BaseQryOPENINGINCASEOFMIS += " AND InnQuery.DocType in ('') "
                    End If
                Else
                    ''added y richa agarwal on 27 Nov,2020
                    If chkTurnOver.Checked = True Then
                        'BaseQry += " WHERE 1=1"
                        'BaseQryOPENINGINCASEOFMIS += " WHERE 1=1"

                        Dim StrCONDITION As String = String.Empty
                        StrCONDITION = "'DR','IN','CR'"

                        BaseQry += " AND InnQuery.DocType in (" & StrCONDITION & ") "
                        BaseQryOPENINGINCASEOFMIS += " AND InnQuery.DocType in (" & StrCONDITION & ") "
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
                    Else
                        If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
                            StrDocWiseFilter += " and and ACode in ( " + strCusMappingByLocForCurUser + " ) "
                        End If
                    End If
                End If

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    If chkItemWise.Checked Then
                        strFIlterCheck += "and Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
                    Else
                        strFIlterCheck += "and Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
                    End If
                Else
                    ''richa UDL/08/07/21-001041
                    If ShowCustomerLedgerasPerRightsForLocation = True OrElse objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
                        If chkItemWise.Checked Then
                            strFIlterCheck += "and Loc_Code in (" + StrPermission + ")  "
                        Else
                            strFIlterCheck += "and Location in (" + StrPermission + ")  "
                        End If
                    End If
                End If
                'If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                '    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")  "
                'End If

                Dim dtCustGroup As DataTable = clsDBFuncationality.GetDataTable("select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "'")

                If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 And txtCustomerGroup.arrValueMember Is Nothing Then
                    strFIlterCheck += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "')"
                ElseIf txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")  "
                End If

                If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 And txtCustomer.arrValueMember Is Nothing Then
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
                        strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Code in ( " + strCusMappingByLocForCurUser + " "

                    Else
                        strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Code in (select DISTINCT Cust_Code from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL WHERE User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "' "

                    End If

                    If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                        strFIlterCheck += " and TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ") ) "
                    Else
                        strFIlterCheck += " ) "
                    End If
                End If

                '' added by richa agarwal ERO/22/05/18-000323
                If TxtZone.arrValueMember IsNot Nothing AndAlso TxtZone.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(TxtZone.arrValueMember) + ")  "
                End If
                ''--------------

                If txtCustomerType.arrValueMember IsNot Nothing AndAlso txtCustomerType.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Type_Code in (" + clsCommon.GetMulcallString(txtCustomerType.arrValueMember) + ")  "
                End If
                If txtCustomerCategory.arrValueMember IsNot Nothing AndAlso txtCustomerCategory.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Category_Code in (" + clsCommon.GetMulcallString(txtCustomerCategory.arrValueMember) + ")  "
                End If

                If (rbtnCustGroupWise.Checked OrElse rbtnCustGroupWiseDrCr.Checked) Then
                    If chkExcludeOpening.Checked = True Then
                        BaseQryOPENINGINCASEOFMIS = " Select '' as ACode ,'' as AName,'' as DocNo,'' as AgainstInvoiceNo,null as DocDate ,'' as DocType ,'' as DocNarr ,'' as ChequeDetails ,'' as Currency_Code ,1 as ConvRate, 0 as DrAmt,0 as CrAmt,0 as Sales,0 as CollectionRefund , 0 as SecurityDrAmt ,0 as SecurityCrAmt ,0 as DrNote,0 as CrNote,'' as Location,'' as SourceCode ,'' as Item_Code,'' as Item_Desc,'' as Receipt_Type,'' as Bank_Code,'' as Cust_Type_Code ,'' as Cust_Type_Desc,'' as Cust_Category_Code ,'' as CUST_CATEGORY_DESC,0 as EXCHANGE_GAIN_AMT ,0 as EXCHANGE_LOSS_AMT  where 1=2"
                    End If
                    dtCustGrp = New DataTable
                    strQry = "Select 'CustomerGroup' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, Cust_Group_Code, MAX(Cust_Group_Desc) as Cust_Group_Desc, '' as ACode, '' as AName, SUM(convert(decimal(18,2),OpngBal)) as OpngBal, SUM(convert(decimal(18,2),DrAmt)) as DrAmt, SUM(convert(decimal(18,2),CrAmt)) as CrAmt, SUM(convert(decimal(18,2),Sales)) as [Sales], SUM(convert(decimal(18,2),CollectionRefund)) as CollectionRefund, SUM(convert(decimal(18,2),DrNote)) as DrNote,-1* SUM(convert(decimal(18,2),CrNote)) as CrNote, "

                    If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                        strQry += "( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) + SUM(convert(decimal(18,2),Sales)) ) -(SUM(convert(decimal(18,2),CrAmt))+SUM(convert(decimal(18,2),CollectionRefund)))  as BalAmt, "
                    Else
                        strQry += "( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt, "
                    End If
                    strQry += " MAX(xxx.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(xxx.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc  From (" + Environment.NewLine &
                    " Select TSPL_CUSTOMER_MASTER.Cust_Group_Code, MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc, '' as CurrencyCode, null as ConvRate, SUM(DrAmt*" & ddlCurrencyType.SelectedValue & ")-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote, 0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from ( " + BaseQryOPENINGINCASEOFMIS + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine &
                    Environment.NewLine + " UNION ALL-----------------------------------BADA UNION--------------------" + Environment.NewLine &
                    " Select TSPL_CUSTOMER_MASTER.Cust_Group_Code, MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc, MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate, 0 as OpngBal, SUM(convert(decimal(18,2),DrAmt*" & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & "))  -  case when DocType <>'EXC' then sum(isnull(final.EXCHANGE_GAIN_AMT,0) ) else 0 end +  case when DocType <>'EXC' then sum(isnull(final.EXCHANGE_LOSS_AMT ,0) ) else 0 end as DrAmt, " + Environment.NewLine &
                    " sum(convert(decimal(18,2),CrAmt)) CrAmt, SUM(convert(decimal(18,2),Sales* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as [Sales], SUM(convert(decimal(18,2),CollectionRefund* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as CollectionRefund, SUM(convert(decimal(18,2),DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as DrNote, SUM(convert(decimal(18,2),CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc FROM ( " + BaseQry + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine &
                    " Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine &
                    " where CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' AND CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY TSPL_CUSTOMER_MASTER.Cust_Group_Code,DocType" + Environment.NewLine &
                    " ) XXX GROUP BY Cust_Group_Code "
                    If rbtnCustGroupWiseDrCr.Checked Then
                        strQry = " select strType,RunDate,CompanyAddress ,CompanyName, FilterFromDate,FilterToDate, Cust_Group_Code, Cust_Group_Desc, ACode, AName,case when OpngBal>0 then  OpngBal else 0 end as OpngBalDR,case when OpngBal>0 then  0 else -1*OpngBal end as OpngBalCR,DrAmt,CrAmt,case when BalAmt>0 then BalAmt else 0 end BalAmtDR,case when BalAmt>0 then 0 else -1*BalAmt end BalAmtCR,  Sales , CollectionRefund,DrNote,CrNote, Cust_Category_Code,Cust_Category_Desc,Cust_Type_Code,Cust_Type_Desc from (" + strQry + ")XXXX "
                    End If
                    strQry += " ORDER BY Cust_Group_Code "
                    dtCustGrp = clsDBFuncationality.GetDataTable(strQry)
                End If
                If (rbtnCustomerCategory.Checked OrElse rbtnCustomerType.Checked) Then
                    If chkExcludeOpening.Checked = True Then
                        BaseQryOPENINGINCASEOFMIS = " Select '' as ACode ,'' as AName,'' as DocNo,'' as AgainstInvoiceNo,null as DocDate ,'' as DocType ,'' as DocNarr ,'' as ChequeDetails ,'' as Currency_Code ,1 as ConvRate, 0 as DrAmt,0 as CrAmt,0 as Sales,0 as CollectionRefund , 0 as SecurityDrAmt ,0 as SecurityCrAmt ,0 as DrNote,0 as CrNote,'' as Location,'' as SourceCode ,'' as Item_Code,'' as Item_Desc,'' as Receipt_Type,'' as Bank_Code,'' as Cust_Type_Code ,'' as Cust_Type_Desc,'' as Cust_Category_Code ,'' as CUST_CATEGORY_DESC,0 as EXCHANGE_GAIN_AMT ,0 as EXCHANGE_LOSS_AMT  where 1=2"
                    End If
                    dtCustGrp = New DataTable
                    strQry = "Select '" & IIf(rbtnCustomerCategory.Checked = True, "CustomerCategory", "CustomerType") & "' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,max(Cust_Group_Code) as  Cust_Group_Code, MAX(Cust_Group_Desc) as Cust_Group_Desc, '' as ACode, '' as AName, SUM(convert(decimal(18,2),OpngBal)) as OpngBal, SUM(convert(decimal(18,2),DrAmt)) as DrAmt, SUM(convert(decimal(18,2),CrAmt)) as CrAmt, SUM(convert(decimal(18,2),Sales)) as [Sales], SUM(convert(decimal(18,2),CollectionRefund)) as CollectionRefund, SUM(convert(decimal(18,2),DrNote)) as DrNote,-1* SUM(convert(decimal(18,2),CrNote)) as CrNote, "

                    If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                        strQry += "( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) + SUM(convert(decimal(18,2),Sales)) ) -(SUM(convert(decimal(18,2),CrAmt))+SUM(convert(decimal(18,2),CollectionRefund)))  as BalAmt, "
                    Else
                        strQry += "( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt, "
                    End If
                    strQry += "  " & IIf(rbtnCustomerCategory.Checked = True, "xxx.Cust_Category_Code", "MAX(xxx.Cust_Category_Code)") & " as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc," & IIf(rbtnCustomerCategory.Checked = True, "MAX(xxx.Cust_Type_Code)", "xxx.Cust_Type_Code") & " As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc From (" + Environment.NewLine &
                    " Select " & IIf(rbtnCustomerCategory.Checked = True, "tspl_customer_master.Cust_Category_Code", "MAX(tspl_customer_master.Cust_Category_Code) ") & "  as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc," & IIf(rbtnCustomerCategory.Checked = True, "MAX(tspl_customer_master.Cust_Type_Code)", "tspl_customer_master.Cust_Type_Code") & " As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc, '' as CurrencyCode, null as ConvRate, SUM(DrAmt*" & ddlCurrencyType.SelectedValue & ")-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote, 0 as CrNote, max(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc from ( " + BaseQryOPENINGINCASEOFMIS + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY " & IIf(rbtnCustomerCategory.Checked = True, "TSPL_CUSTOMER_MASTER.Cust_Category_Code", "TSPL_CUSTOMER_MASTER.Cust_Type_Code") & " " + Environment.NewLine &
                    Environment.NewLine + " UNION ALL-----------------------------------BADA UNION--------------------" + Environment.NewLine &
                    " Select " & IIf(rbtnCustomerCategory.Checked = True, "tspl_customer_master.Cust_Category_Code ", " MAX(tspl_customer_master.Cust_Category_Code) ") & "as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc," & IIf(rbtnCustomerCategory.Checked = True, "MAX(tspl_customer_master.Cust_Type_Code)", "tspl_customer_master.Cust_Type_Code") & " As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc , MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate, 0 as OpngBal, SUM(convert(decimal(18,2),DrAmt*" & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as DrAmt, " + Environment.NewLine &
                    " sum(convert(decimal(18,2),CrAmt)) CrAmt, SUM(convert(decimal(18,2),Sales* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as [Sales], SUM(convert(decimal(18,2),CollectionRefund* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as CollectionRefund, SUM(convert(decimal(18,2),DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as DrNote, SUM(convert(decimal(18,2),CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as CrNote, max(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc FROM ( " + BaseQry + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine &
                    " Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine &
                    " where CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' AND CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY  " & IIf(rbtnCustomerCategory.Checked = True, "TSPL_CUSTOMER_MASTER.Cust_Category_Code", "TSPL_CUSTOMER_MASTER.Cust_Type_Code") & " " + Environment.NewLine &
                    " ) XXX GROUP BY " & IIf(rbtnCustomerCategory.Checked = True, "Cust_Category_Code", "Cust_Type_Code") & " "

                    strQry += " ORDER BY " & IIf(rbtnCustomerCategory.Checked = True, "Cust_Category_Code", "Cust_Type_Code") & " "
                    dtCustGrp = clsDBFuncationality.GetDataTable(strQry)
                End If


                If rbtnCustWise.Checked OrElse rbtnCustWiseDrCr.Checked Then
                    dtCustomer = New DataTable
                    If chkExcludeOpening.Checked = True Then
                        BaseQryOPENINGINCASEOFMIS = " Select '' as ParentCode,'' as Child,'' as ACode ,'' as AName,'' as DocNo,'' as AgainstInvoiceNo,null as DocDate ,'' as DocType ,'' as DocNarr ,'' as ChequeDetails ,'' as Currency_Code ,1 as ConvRate, 0 as DrAmt,0 as CrAmt,0 as Sales,0 as CollectionRefund , 0 as SecurityDrAmt ,0 as SecurityCrAmt ,0 as DrNote,0 as CrNote,'' as Location,'' as SourceCode ,'' as Item_Code,'' as Item_Desc,'' as Receipt_Type,'' as Bank_Code,'' as Cust_Type_Code ,'' as Cust_Type_Desc,'' as Cust_Category_Code ,'' as CUST_CATEGORY_DESC,0 as EXCHANGE_GAIN_AMT ,0 as EXCHANGE_LOSS_AMT  where 1=2"
                    End If
                    If chkDateWise.Checked = True Then
                        strQry = "Select convert(date,DocDate ,103) DocDateForSorting, ROW_NUMBER() OVER (order BY ACode , convert(date,DocDate,103)) as RowNo,"
                    ElseIf chkMonthWise.Checked = True Then
                        strQry = "Select convert(date,max(DocDate) ,103) DocDateForSorting, ROW_NUMBER() OVER (order BY ACode , convert(date,max(DocDate),103)) as RowNo,"
                    Else
                        strQry = "Select  ROW_NUMBER() OVER (order BY ACode)   as RowNo,"
                    End If

                    strQry += " 'Customer' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, MAX(xxx.Cust_Group_Code) AS Cust_Group_Code, '' as Cust_Group_Desc,max(ParentCode) as [ParentCode], max(Parent_Master.Customer_Name) as ParentName, max(Child) as Child, ACode, "
                    ''BHA/25/09/18-000570 richa 
                    If chkDateWise.Checked Then
                        strQry += " DocDate, "
                    End If
                    If chkMonthWise.Checked Then
                        strQry += " datename(month,convert(date,max(DocDate) ,103))+' - '+cast(YEAR(convert(date,max(DocDate) ,103)) as varchar) as DocDate, "
                    End If
                    strQry += " MAX(AName) as AName,MAX(xxx.Zone_code) as Zone_code,MAX(xxx.Route_No) as Route_No, SUM(convert(decimal(18,2),OpngBal)) as OpngBal, SUM(convert(decimal(18,2),DrAmt)) as DrAmt, SUM(convert(decimal(18,2),CrAmt)) as CrAmt, SUM(convert(decimal(18,2),Sales)) as [Sales], SUM(convert(decimal(18,2),CollectionRefund)) as CollectionRefund, SUM(convert(decimal(18,2),DrNote)) as DrNote, -1* SUM(convert(decimal(18,2),CrNote)) as CrNote, "

                    If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                        strQry += "( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) + SUM(convert(decimal(18,2),Sales)) ) -(SUM(convert(decimal(18,2),CrAmt))+SUM(convert(decimal(18,2),CollectionRefund)))  as BalAmt, "
                    Else
                        strQry += "( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt))  as BalAmt, "
                    End If
                    strQry += " MAX(xxx.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(xxx.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc From (" + Environment.NewLine &
                    " Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code,case when isnull(MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No),'')='' then ACode  else MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No)  end  as ParentCode,  max(Child) as Child, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName,"
                    'If chkMonthWise.Checked Then
                    '    strQry += " max(convert(date,final.DocDate ,103)) DocDate,"
                    'End If
                    If chkDateWise.Checked Or chkMonthWise.Checked Then
                        If clsCommon.myCDate(txtFromDate.Value) < clsCommon.myCDate(txtToDate.Value, "dd/MM/yyyy") Then
                            strQry += " convert(date,max(dateadd(day,1,final.DocDate)),103) as DocDate,"
                        Else
                            strQry += " convert(date,max('" & strFromDate & "'),103) as DocDate,"
                        End If
                    End If
                    strQry += " isnull(max(TSPL_CUSTOMER_MASTER.Zone_Code),'') as Zone_code,isnull(max(TSPL_CUSTOMER_MASTER.Route_No ),'') as Route_No, '' as CurrencyCode, null as ConvRate, SUM(DrAmt*" & ddlCurrencyType.SelectedValue & ")-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote, 0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from ( " + BaseQryOPENINGINCASEOFMIS + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY ACode" + Environment.NewLine &
                                        Environment.NewLine + " UNION ALL" + Environment.NewLine &
                                        " Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, case when isnull(MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No),'')='' then ACode  else MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No)  end  as ParentCode,  max(Child) as Child,ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, "
                    If chkDateWise.Checked Or chkMonthWise.Checked Then
                        strQry += " max(convert(date,final.DocDate ,103)) DocDate,"
                    End If
                    strQry += " isnull(max(TSPL_CUSTOMER_MASTER.Zone_Code),'') as Zone_code,isnull(max(TSPL_CUSTOMER_MASTER.Route_No ),'') as Route_No,MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate, 0 as OpngBal, SUM(convert(decimal(18,2),DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) -  case when DocType <>'EXC' then sum(isnull(final.EXCHANGE_GAIN_AMT,0) ) else 0 end +  case when DocType <>'EXC' then sum(isnull(final.EXCHANGE_LOSS_AMT ,0) ) else 0 end as DrAmt, " + Environment.NewLine &
                                                            "SUM(convert(decimal(18,2),CrAmt)) as CrAmt, SUM(convert(decimal(18,2),Sales* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as [Sales], SUM(convert(decimal(18,2),CollectionRefund* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as CollectionRefund, SUM(convert(decimal(18,2),DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as DrNote, SUM(convert(decimal(18,2),CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc FROM ( " + BaseQry + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine &
                                                            " Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine &
                                                            "where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' AND CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY ACode,DocType" + Environment.NewLine
                    If chkDateWise.Checked Then
                        strQry += " ,convert(date,final.DocDate ,103)"
                    End If
                    ''richa ERO/23/12/19-001161
                    If chkMonthWise.Checked Then
                        strQry += " ,datename(month,convert(date,final.DocDate ,103))+cast(YEAR(convert(date,final.DocDate ,103)) as varchar) "
                    End If
                    strQry += " ) XXX left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=XXX.ParentCode GROUP BY ACode"
                    If chkDateWise.Checked Then
                        strQry += " ,convert(date,DocDate ,103)"
                    End If
                    ''richa ERO/23/12/19-001161
                    If chkMonthWise.Checked Then
                        strQry += " ,datename(month,convert(date,DocDate ,103))+' - '+cast(YEAR(convert(date,DocDate ,103)) as varchar) "
                    End If
                    ''richa TEC/05/06/19-000517 add ParentCode, ParentName, Child into select Query,TEC/05/06/19-000515
                    If rbtnCustWiseDrCr.Checked Then
                        strQry = " select strType,RunDate,CompanyAddress ,CompanyName, FilterFromDate,FilterToDate, Cust_Group_Code, Cust_Group_Desc, ParentCode, ParentName, Child,ACode, AName,Zone_Code,Route_No,case when OpngBal>0 then  OpngBal else 0 end as OpngBalDR,case when OpngBal>0 then  0 else -1*OpngBal end as OpngBalCR,DrAmt,CrAmt,case when BalAmt>0 then BalAmt else 0 end BalAmtDR,case when BalAmt>0 then 0 else -1*BalAmt end BalAmtCR,  Sales , CollectionRefund,DrNote,CrNote, Cust_Category_Code,Cust_Category_Desc,Cust_Type_Code,Cust_Type_Desc from (" + strQry + ")XXXX "
                    End If
                    ''richa agarwal 27/dec/2019
                    If AllowtoSHOWParentChildCustomer = False And chkDateWise.Checked = False And chkMonthWise.Checked = False Then
                        strQry += " ORDER BY ACode"
                    End If

                    'If chkDateWise.Checked Then
                    '    strQry += " ,convert(date,DocDate ,103)"
                    'End If

                    'If chkMonthWise.Checked AndAlso AllowtoSHOWParentChildCustomer = False Then
                    '    strQry += "  ,max(convert(date,DocDate ,103))"
                    'End If


                    ''richa agarwal ERO/05/02/19-000485 11 Feb,2019
                    If AllowtoSHOWParentChildCustomer = True Then
                        Dim strCustomerQryCHILDParent As String = " select strType, RunDate,CompanyAddress ,CompanyName, FilterFromDate,FilterToDate,Cust_Group_Code,ParentCode, ParentName,Child, ACode, "
                        If chkDateWise.Checked OrElse chkMonthWise.Checked Then
                            strCustomerQryCHILDParent += " DocDate, "
                        End If

                        strCustomerQryCHILDParent += " AName,Zone_code,Route_No,OpngBal,DrAmt, CrAmt, [Sales],CollectionRefund,DrNote,CrNote,"
                        If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                            strCustomerQryCHILDParent += "BalAmt, "
                        Else
                            strCustomerQryCHILDParent += "SUM(convert(decimal(18,2),OpngBal) + convert(decimal(18,2),DrAmt)-convert(decimal(18,2),CrAmt)) Over (Partition by ParentCode ORDER BY RowNo) as BalAmt, "
                        End If
                        strCustomerQryCHILDParent += " Cust_Category_Code,Cust_Category_Desc,Cust_Type_Code,Cust_Type_Desc from (" & strQry & ") z order by z.Acode"
                        strQry = strCustomerQryCHILDParent
                    End If

                    '' strCustomerDaywise = strQry

                    If chkDateWise.Checked = True OrElse chkMonthWise.Checked = True Then
                        strQry = " Select z.RowNo ,z.strType,z.RunDate,z.CompanyAddress,z.CompanyName,z.FilterFromDate,z.FilterToDate ,z.Cust_Group_Code ,z.Cust_Group_Desc ,z.ParentCode,z.ParentName,z.Child,z.ACode,z.DocDate,z.AName,z.Zone_code,z.Route_No,z.OpngBal,z.DrAmt,z.CrAmt,z.Sales,z.CollectionRefund,z.DrNote,z.CrNote ,SUM(convert(decimal(18,2),OpngBal ) +convert(decimal(18,2),DrAmt)-convert(decimal(18,2),CrAmt)) Over (Partition by Acode ORDER BY RowNo) as BalAmt,z.Cust_Category_Code,z.Cust_Category_Desc ,z.Cust_Type_Code ,z.Cust_Type_Desc from ( " & strQry & " ) z   ORDER BY ACode  ,convert(date,DocDateForSorting ,103)"
                    End If

                    dtCustomer = clsDBFuncationality.GetDataTable(strQry)
                End If
                '------------------Detail Level Data------------------- 

                If chkItemWise.Checked Then
                    strQry = "WITH CTETemp as ("
                    strQry += "Select ROW_NUMBER() OVER (PARTITION BY ACode ORDER BY ACode, OrderDate, OrderDocType) as RowNo, 'Detail' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,Parent_Master.Customer_Name as ParentName, * from ("
                    strQry += " Select MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No) as ParentCode, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as DocNo,'' as AgainstInvoiceNo, NULL as DocDate, '' as DocType, 'Opening Balance' as DocNarr, '' as ChequeDetails, '' as Location, NULL as OrderDate, '' as LocDesc, '' as Item_Code, '' as Item_Desc, Case WHEN SUM(DrAmt)>=SUM(CrAmt) Then SUM(DrAmt)-SUM(CrAmt) Else 0 End as DrAmt, Case WHEN SUM(DrAmt)< SUM(CrAmt) Then SUM(CrAmt)-SUM(DrAmt) Else 0 End as CrAmt, SUM(Sales) as [Sales], SUM(CollectionRefund) as CollectionRefund, SUM(DrNote) as DrNote, SUM(CrNote) as CrNote, SUM(DrAmt)-SUM(CrAmt) as BalAmt, '' as SourceCode, 0 as OrderDocType, '' as [Reconciliation_Date] "
                    strQry += ",MAX(Receipt_Type) AS Receipt_Type "
                    strQry += " from ( " + BaseQry + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code "
                    strQry += " where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' AND TSPL_CUSTOMER_MASTER.Status='N'  and LEN(ACode)>0 " + strFIlterCheck + " " + FilterForDetail + " " + CheckCustomer + " "
                    strQry += "  GROUP BY ACode"
                    strQry += " UNION ALL"
                    strQry += " Select TSPL_CUSTOMER_MASTER.Parent_Customer_No as ParentCode, ACode, TSPL_CUSTOMER_MASTER.Customer_Name as AName, DocNo,AgainstInvoiceNo,DocDate,DocType,isnull(  tspl_BankReco_Head.Description,'') as DocNarr,ChequeDetails, Loc_Code,convert(date,final.DocDate,103) as OrderDate, (Select MAX( TSPL_LOCATION_MASTER.Location_Desc) from  TSPL_LOCATION_MASTER where Loc_Segment_Code=Location_Code) as LocDesc, Item_Code, Item_Desc, DrAmt, CrAmt, [Sales], CollectionRefund, DrNote, CrNote, dramt-cramt as BalAmt,SourceCode, CASE  WHEN DocType  = 'IN' THEN 1  WHEN DocType = 'RC' THEN 2 WHEN DocType = 'SR' THEN 3 WHEN DocType = 'AD' THEN 4 ELSE 5 END  as OrderDocType, Case When tspl_BankReco_Head.Post='Y' AND tspl_BankReco_Detail.Reconciliation_Status='C' Then CONVERT(VARCHAR,tspl_BankReco_Detail.Reconciliation_Date,103) Else 'Pending' End as [Reconciliation_Date] "
                    strQry += ",Receipt_Type AS Receipt_Type "
                    strQry += " from ( " + BaseQry + " ) Final LEFT OUTER JOIN tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' and   CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForDetail + " " + CheckCustomer + ""
                    strQry += " ) XXX   left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=XXX.ParentCode"
                    strQry += ") Select CTETemp.RowNo, CTETemp.strType, CTETemp.RunDate, CTETemp.CompanyAddress, CTETemp.CompanyName, CTETemp.FilterFromDate, CTETemp.FilterToDate,CTETemp.ParentCode,CTETemp.ParentName ,CTETemp.ACode, CTETemp.AName, CTETemp.DocNo, CTETemp.AgainstInvoiceNo, convert(varchar,CTETemp.DocDate) as DocDate, CTETemp.DocType, CTETemp.DocNarr, CTETemp.ChequeDetails, CTETemp.Location, CTETemp.OrderDate, CTETemp.LocDesc, Item_Code, Item_Desc, CTETemp.DrAmt, CTETemp.CrAmt, CTETemp.BalAmt, SUM(BalAmt) OVER (PARTITION BY ACode ORDER BY ACode, RowNo) as [Closing], CTETemp.SourceCode, CTETemp.OrderDocType, CTETemp.Reconciliation_Date "
                    strQry += ",CTETemp.Receipt_Type "
                    strQry += " from CTETemp ORDER BY CTETemp.OrderDate,CTETemp.ACode,  CTETemp.OrderDocType"
                Else
                    If rbtnDocWise.Checked = True Then '' 10-Aug-2015  BM00000008356,BM00000008357 
                        strQry = "WITH CTETemp as (" + Environment.NewLine &
                        " Select ROW_NUMBER() OVER (PARTITION BY ACode ORDER BY ACode, OrderDate, OrderDocType) as RowNo, 'Detail' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,Parent_Master.Customer_Name as ParentName, XXX.* from (" + Environment.NewLine &
                        " Select MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No) as ParentCode, max(ACode) as ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, DocNo, MAX(AgainstInvoiceNo) as AgainstInvoiceNo, MAX(DocDate) as DocDate, MAX(DocType) as DocType,isnull(MAX(DocNarr),'') as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Location) as Location, convert(date,MAX(final.DocDate),103) as OrderDate," + Environment.NewLine
                        strQry += "MAX( TSPL_LOCATION_MASTER.Location_Desc) as LocDesc,'' as Item_Code, '' as Item_Desc, MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate,  " + Environment.NewLine &
                        " CASE WHEN max(DocType) ='RV-TA' THEN SUM(convert(decimal(18,2),DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & "))+ (Select isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) from TSPL_RECEIPT_HEADER where Receipt_No =(Select Document_No  from TSPL_BANK_REVERSE where Reverse_Code =DocNo)) - (Select isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT),0) from TSPL_RECEIPT_HEADER where Receipt_No =(Select Document_No  from TSPL_BANK_REVERSE where Reverse_Code =DocNo)) ELSE SUM(convert(decimal(18,2),DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & "))  END AS DrAmt, " + Environment.NewLine
                        strQry += " Sum(convert(decimal(18,2),CrAmt)) as CrAmt, SUM(convert(decimal(18,2),Sales* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as [Sales], SUM(convert(decimal(18,2),CollectionRefund* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as CollectionRefund, SUM(convert(decimal(18,2),DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as DrNote, SUM(convert(decimal(18,2),CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as CrNote, " + Environment.NewLine
                        strQry += " CASE WHEN max(DocType) ='RV-TA' THEN SUM(convert(decimal(18,2),DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & "))+ (Select isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) from TSPL_RECEIPT_HEADER where Receipt_No =(Select Document_No  from TSPL_BANK_REVERSE where Reverse_Code =DocNo)) - (Select isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT),0) from TSPL_RECEIPT_HEADER where Receipt_No =(Select Document_No  from TSPL_BANK_REVERSE where Reverse_Code =DocNo)) ELSE SUM(convert(decimal(18,2),DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & "))  END -Sum(convert(decimal(18,2),CrAmt))  as BalAmt,  MAX(SourceCode) as SourceCode, CASE  WHEN MAX(DocType) = 'IN' THEN 1  WHEN MAX(DocType) = 'RC' THEN 2 WHEN MAX(DocType) = 'SR' THEN 3 WHEN MAX(DocType) = 'AD' THEN 4 ELSE 5 END  as OrderDocType," + Environment.NewLine &
                         " Case When Max(TSPL_BANK_MASTER.Bank_type)='B' Then (Case When MAX(tspl_BankReco_Head.Post)='Y' AND MAX(tspl_BankReco_Detail.Reconciliation_Status)='C' Then CONVERT(VARCHAR,MAX(tspl_BankReco_Detail.Reconciliation_Date),103) Else 'Pending' End) Else '' End as [Reconciliation_Date]" + Environment.NewLine &
                         " ,MAX(Final.Receipt_Type) AS Receipt_Type, MAX(TSPL_CUSTOMER_MASTER.Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(TSPL_CUSTOMER_MASTER.Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC " + Environment.NewLine &
                         " ,MAX(TSPL_JOURNAL_MASTER.Voucher_No) AS Voucher_No   " + Environment.NewLine &
                         " ,MAX(TSPL_CUSTOMER_MASTER.Cust_Account) AS Cust_Account" + Environment.NewLine &
                         " ,( SELECT MAX(TSPL_JOURNAL_DETAILS.Account_code) AS Account_code  FROM TSPL_JOURNAL_DETAILS " + Environment.NewLine &
                         " LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account  = MAX(TSPL_CUSTOMER_MASTER.Cust_Account)" + Environment.NewLine &
                         " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " + Environment.NewLine &
                         " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') = ISNULL(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct  ,'')  " + Environment.NewLine &
                         " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=MAX(TSPL_JOURNAL_MASTER.Voucher_No )) AS JEAccount_code" + Environment.NewLine &
                         " ,( SELECT SUM( TSPL_JOURNAL_DETAILS.Amount) AS Amount  FROM TSPL_JOURNAL_DETAILS " + Environment.NewLine &
                         " LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account  =MAX(TSPL_CUSTOMER_MASTER.Cust_Account)    " + Environment.NewLine &
                         " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " + Environment.NewLine &
                         " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') = ISNULL(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct  ,'')  " + Environment.NewLine &
                         " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=MAX(TSPL_JOURNAL_MASTER.Voucher_No )	) AS JEAmount " + Environment.NewLine
                        strQry += " from ( " + BaseQry + " ) Final LEFT OUTER JOIN tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No AND tspl_BankReco_Detail.Reconciliation_Status='C' LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account " + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=Final.DocNo " + Environment.NewLine
                        strQry += " Left Outer Join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo " + Environment.NewLine &
                         "  LEFT OUTER JOIN   TSPL_LOCATION_MASTER on  TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine
                        strQry += " where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' and   CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForDetail + " " + CheckCustomer + " " + Environment.NewLine
                        strQry += " GROUP By  DocNo" + Environment.NewLine &
                        " ) XXX   left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=XXX.ParentCode" + Environment.NewLine &
                        ") Select CTETemp.RowNo, CTETemp.strType, CTETemp.RunDate, CTETemp.CompanyAddress, CTETemp.CompanyName, CTETemp.FilterFromDate, CTETemp.FilterToDate, CTETemp.ParentCode, CTETemp.ParentName, CTETemp.ACode, CTETemp.AName, CTETemp.DocNo, CTETemp.AgainstInvoiceNo, convert(varchar,CTETemp.DocDate,103) as DocDate, CTETemp.DocType, CTETemp.DocNarr, CTETemp.ChequeDetails, CTETemp.Location, CTETemp.OrderDate, CTETemp.LocDesc, " & IIf(ddlCurrencyType.SelectedValue = "1", "CTETemp.Currency_Code", "'INR'") & " as Currency_Code, CTETemp.DrAmt, CTETemp.CrAmt, CTETemp.Sales, CTETemp.CollectionRefund, CTETemp.DrNote, CTETemp.CrNote, CTETemp.BalAmt, SUM(BalAmt) OVER (PARTITION BY ACode ORDER BY ACode, RowNo) as [Closing], CTETemp.SourceCode, CTETemp.OrderDocType, CTETemp.Reconciliation_Date " + Environment.NewLine &
                        ",CTETemp.Receipt_Type,CTETemp.Cust_Type_Code ,CTETemp.Cust_Type_Desc ,CTETemp.Cust_Category_Code ,CTETemp.CUST_CATEGORY_DESC ,CTETemp.Voucher_No As [JE No],CTETemp.JEAccount_Code  AS [JE Account],CTETemp.JEAmount  AS [JE Amount],CTETemp.BalAmt - CTETemp.JEAmount AS DiffAmt   " + Environment.NewLine &
                        " from CTETemp ORDER BY CTETemp.OrderDate,CTETemp.ACode,  CTETemp.OrderDocType"
                    Else
                        ''richa ERO/20/11/19-001125
                        dtMain = New DataTable
                        ' strQry = "WITH CTETemp as (" + Environment.NewLine &
                        strQry = " Select " & IIf(AllowtoSHOWParentChildCustomer = True, " ROW_NUMBER() OVER (order BY ACode) ", "ROW_NUMBER() OVER (PARTITION BY ACode ORDER BY ACode, DocDate)") & "  as RowNo, 'Detail' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,Parent_Master.Customer_Name as ParentName, " + Environment.NewLine &
                        " XXX.* from (" + Environment.NewLine
                        If chkExcludeOpening.IsChecked = False Then
                            strQry += " Select isnull(max(TSPL_CUSTOMER_MASTER.Zone_Code),'') as Zone_code,case when isnull(MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No),'')='' then ACode  else MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No)  end  as ParentCode,  max(Child) as Child, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as DocNo,'' as AgainstInvoiceNo, NULL as DocDate, 'OP' as DocType, 'Opening Balance' as DocNarr, '' as ChequeDetails, '' as Location, NULL as OrderDate, '' as LocDesc, '' as Item_Code, '' as Item_Desc, '' as Currency_Code, NULL as ConvRate, Case WHEN SUM(DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")>=SUM(CrAmt) Then SUM(DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")-SUM(CrAmt) Else 0 End as DrAmt, Case WHEN SUM(DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")< SUM(CrAmt) Then SUM(CrAmt)-SUM(DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") Else 0 End as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote, 0 as CrNote, SUM(DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")-SUM(CrAmt) as BalAmt, '' as SourceCode, 0 as OrderDocType, '' as [Reconciliation_Date]" + Environment.NewLine &
                        " ,MAX(Receipt_Type) AS Receipt_Type ,MAX(TSPL_CUSTOMER_MASTER.Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(TSPL_CUSTOMER_MASTER.Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC,MAX(TSPL_JOURNAL_MASTER.Voucher_No) AS Voucher_No ,MAX(TSPL_CUSTOMER_MASTER.Cust_Account ) AS Cust_Account 	,'' AS JEAccount_Code ,0 AS JEAmount" + Environment.NewLine &
                        " from ( " + BaseQryOPENINGINCASEOFMIS + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No = final.docno " + Environment.NewLine &
                        " where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "'  and LEN(ACode)>0 " + strFIlterCheck + " " + FilterForDetail + " " + CheckCustomer + "" + Environment.NewLine &
                        " GROUP BY ACode" + Environment.NewLine &
                        Environment.NewLine + " UNION ALL----------------------------------------------Bada UNION---------------" + Environment.NewLine
                        End If
                        strQry += " Select isnull(max(TSPL_CUSTOMER_MASTER.Zone_Code),'') as Zone_code,case when isnull(MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No),'')='' then ACode else MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No) end as ParentCode,  max(Child) as Child, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, DocNo, MAX(AgainstInvoiceNo) as AgainstInvoiceNo, MAX(DocDate) as DocDate, MAX(DocType) as DocType,isnull(MAX(DocNarr),'') as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Location) as Location, convert(date,MAX(final.DocDate),103) as OrderDate," + Environment.NewLine &
                       "MAX( TSPL_LOCATION_MASTER.Location_Desc)  as LocDesc, '' as Item_Code, '' as Item_Desc, MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate, SUM(convert(decimal(18,2),DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & "))  -  case when DocType <>'EXC' then sum(isnull(final.EXCHANGE_GAIN_AMT,0) ) else 0 end +  case when DocType <>'EXC' then sum(isnull(final.EXCHANGE_LOSS_AMT ,0) ) else 0 end as DrAmt," + Environment.NewLine &
                        " SUM(convert(decimal(18,2),CrAmt)) as CrAmt, SUM(convert(decimal(18,2),Sales* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as [Sales], SUM(convert(decimal(18,2),CollectionRefund* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as CollectionRefund, SUM(convert(decimal(18,2),DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as DrNote, SUM(convert(decimal(18,2),CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) as CrNote, " + Environment.NewLine &
                       "SUM(convert(decimal(18,2),dramt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")) - SUM(convert(decimal(18,2),CrAmt))  as BalAmt, " + Environment.NewLine &
                        " MAX(SourceCode) as SourceCode, CASE  WHEN MAX(DocType) = 'IN' THEN 1  WHEN MAX(DocType) = 'RC' THEN 2 WHEN MAX(DocType) = 'SR' THEN 3 WHEN MAX(DocType) = 'AD' THEN 4 ELSE 5 END  as OrderDocType," + Environment.NewLine &
                        " Case When Max(TSPL_BANK_MASTER.Bank_type)='B' Then (Case When MAX(tspl_BankReco_Head.Post)='Y' AND MAX(tspl_BankReco_Detail.Reconciliation_Status)='C' Then CONVERT(VARCHAR,MAX(tspl_BankReco_Detail.Reconciliation_Date),103) Else 'Pending' End) Else '' End as [Reconciliation_Date]" + Environment.NewLine &
                        ",MAX(Final.Receipt_Type) AS Receipt_Type, MAX(TSPL_CUSTOMER_MASTER.Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(TSPL_CUSTOMER_MASTER.Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  ,MAX(TSPL_JOURNAL_MASTER.Voucher_No) AS Voucher_No   " + Environment.NewLine &
                        " ,MAX(TSPL_CUSTOMER_MASTER.Cust_Account) AS Cust_Account" + Environment.NewLine &
                        " ,( SELECT MAX(TSPL_JOURNAL_DETAILS.Account_code) AS Account_code  FROM TSPL_JOURNAL_DETAILS " + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account  = MAX(TSPL_CUSTOMER_MASTER.Cust_Account)" + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') = ISNULL(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct  ,'')  " + Environment.NewLine &
                        " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=MAX(TSPL_JOURNAL_MASTER.Voucher_No )) AS JEAccount_code" + Environment.NewLine &
                        " ,( SELECT SUM( TSPL_JOURNAL_DETAILS.Amount) AS Amount  FROM TSPL_JOURNAL_DETAILS " + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account  =MAX(TSPL_CUSTOMER_MASTER.Cust_Account)    " + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') = ISNULL(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct  ,'')  " + Environment.NewLine &
                        " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=MAX(TSPL_JOURNAL_MASTER.Voucher_No )	) AS JEAmount " + Environment.NewLine &
                        " from ( " + BaseQry + " ) Final LEFT OUTER JOIN tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No AND tspl_BankReco_Detail.Reconciliation_Status='C' LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account " + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=Final.DocNo " + Environment.NewLine +
                        " and 2=(case when final.DocType='Adjustment' then (case when TSPL_JOURNAL_MASTER.Source_Code='AR-AD' then 2 else 3 end ) else 2 end) " + Environment.NewLine &
                        " left outer join TSPL_LOCATION_MASTER on  TSPL_LOCATION_MASTER.Location_Code=final.Location" + Environment.NewLine &
                       " left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =final.DocNo " + Environment.NewLine &
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') = ISNULL(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct  ,'')" + Environment.NewLine &
                        " where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' and   CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForDetail + " " + CheckCustomer + " "  ' ' AND 2= (CASE WHEN DOCTYPE IN ('OA','PR','RC','RF','RV-TA') AND tspl_BankReco_Detail.Reconciliation_Status='C' THEN 2 WHEN DOCTYPE NOT IN ('OA','PR','RC','RF','RV-TA') THEN 2 ELSE 1 END) " + Environment.NewLine & _
                        strQry += " GROUP By ACode, DocNo,Location,DocType  " + Environment.NewLine &
                        " ) XXX   left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=XXX.ParentCode " + Environment.NewLine

                        strCustomerDaywise = strQry

                        strQry = "WITH CTETemp as (" & strQry & " ) " + Environment.NewLine &
                      " Select CTETemp.RowNo, CTETemp.strType, CTETemp.RunDate, CTETemp.CompanyAddress, CTETemp.CompanyName, CTETemp.FilterFromDate, CTETemp.FilterToDate, CTETemp.ParentCode, CTETemp.ParentName,CTETemp.Child ,CTETemp.ACode, CTETemp.AName,CTETemp.Zone_Code, CTETemp.DocNo, CTETemp.AgainstInvoiceNo, convert(varchar,CTETemp.DocDate,103) as DocDate, CTETemp.DocType, CTETemp.DocNarr, CTETemp.ChequeDetails, CTETemp.Location, CTETemp.OrderDate, CTETemp.LocDesc, " & IIf(ddlCurrencyType.SelectedValue = "1", "CTETemp.Currency_Code", "'INR'") & " as Currency_Code, convert(decimal(18,2),CTETemp.DrAmt) as DrAmt, convert(decimal(18,2),CTETemp.CrAmt) as CrAmt, convert(decimal(18,2), CTETemp.Sales) as Sales, convert(decimal(18,2),CTETemp.CollectionRefund) as CollectionRefund, convert(decimal(18,2), CTETemp.DrNote) as DrNote,-1 * (case when CTETemp.DocType='IM' then  0 else  convert(decimal(18,2), CTETemp.CrNote) end)  as CrNote, " + Environment.NewLine

                        If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                            strQry += " SUM(convert(decimal(18,2),DrAmt)-convert(decimal(18,2),CrAmt)+convert(decimal(18,2),Sales)-convert(decimal(18,2),CollectionRefund)) Over (Partition by Acode ORDER BY RowNo) as [Closing], " + Environment.NewLine
                        Else
                            ''richa agarwal ERO/05/02/19-000485 7 Feb,2019
                            If AllowtoSHOWParentChildCustomer = True Then
                                strQry += " SUM(convert(decimal(18,2),DrAmt)-convert(decimal(18,2),CrAmt)) Over (Partition by ParentCode ORDER BY RowNo) as [Closing], " + Environment.NewLine
                            Else
                                strQry += " SUM(convert(decimal(18,2),DrAmt)-convert(decimal(18,2),CrAmt)) Over (Partition by Acode ORDER BY RowNo) as [Closing], " + Environment.NewLine
                            End If
                        End If

                        strQry += "CTETemp.SourceCode, CTETemp.OrderDocType, CTETemp.Reconciliation_Date " + Environment.NewLine &
                        ",CTETemp.Receipt_Type,CTETemp.Cust_Type_Code ,CTETemp.Cust_Type_Desc ,CTETemp.Cust_Category_Code ,CTETemp.CUST_CATEGORY_DESC ,CTETemp.Voucher_No As [JE No],CTETemp.JEAccount_Code  AS [JE Account],CTETemp.JEAmount  AS [JE Amount],CTETemp.BalAmt - CTETemp.JEAmount AS DiffAmt   " + Environment.NewLine &
                        "  ,case when CTETemp.DocType='Adjustment' then 'Adjustment' when CTETemp.DocType='IM' then 'Apply Document' when CTETemp.DocType='CR' then 'Credit Note' when CTETemp.DocType='DR' then 'Debit Note' when CTETemp.DocType='IN' then 'Invoice' when CTETemp.DocType='OA' then 'On Account' when CTETemp.DocType='PR' then 'Advance' when CTETemp.DocType='RC' then 'Receipt' " &
                        " when CTETemp.DocType='RF' then 'Refund' when CTETemp.DocType='RV-TA' then 'Bank Reverse' when CTETemp.DocType='UA' then 'Unapplied' when CTETemp.DocType='KN' then 'Knock Off' else CTETemp.DocType end as DocumentType " &
                        " from CTETemp ORDER BY CTETemp.ACode,  CTETemp.RowNo"
                    End If
                End If


                If ChkDocWise.Checked = True Then
                    If ChkDocSumm.Checked = True Then
                        strQry = "WITH CTETEMP as ( SELECT '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,ROW_NUMBER() over (PARTITION By DocNo ORDER By MAX(RefDocDate)) AS RowNo, MAX(CustCode) AS [CustomerCode],MAX(CUSTNAME) AS [Customer Name],DocNo AS [DocumentNo] ,MAX(doctype) As [Doc Type],MAX(DocTypeCode) AS [Doc Type Code],MAX(xxx.Cust_Group_Code) AS [Cust Group Code],MAX(xxx.Route_No) AS [Route No],MAX(xxx.Zone_Code) AS [Zone Code],MAX ([Order Number]) AS [Order Number],case when  MAX(CONVERT(VARCHAR,[Due Date],103))='01/01/1900' then null else MAX(CONVERT(VARCHAR,[Due Date],103)) end AS [Due Date],MAX([Against Sale No]) AS [Against Sale No],MAX([Against Sale Return No]) As [Against Sale Return No],MAX([Against MCC Material Sale Return]) AS [Against MCC Material Sale Return],MAX(AgainstScrap) AS [AgainstScrap],MAX([Against VCGL]) AS [Against VCGL],MAX(Description) AS [Description],MAX(Remarks) As [Remarks],MAX([Child Cust Code]) As [Child Cust Code],CASE WHEN MAX(ISNULL([Child Cust Code],''))<>'' THEN MAX([Child Cust Code]) ELSE MAX(CustCode) END AS MainCustCode,CASE WHEN MAX(ISNULL([Child Cust Name],''))<>'' THEN MAX([Child Cust Name])  ELSE MAX(CustName)  END AS MainCustName,MAX([Source Doc No]) AS [Source Doc No] ,MAX([Loc Code]) AS  [Loc Code],MAX([Loc Desp]) AS  [Loc Desp],MAX(CONVERT(VARCHAR,DOCDATE,103)) AS [Document Date],MAX(RefDocNo) AS [Ref Doc No],MAX(CONVERT (VARCHAR,RefDocDate,101)) AS [Ref Doc Date],MAX(SubDocType) AS [Sub Doc Type],SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,CASE WHEN SUM(CrAmt) =0 THEN SUM(DrAmt) ELSE SUM(CrAmt) * -1 END AS [Trans Amt],MAX(BalAmt) AS BalAmt,CASE WHEN MAX(ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')) <> '' THEN MAX(ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')) ELSE MAX(CUSTCODE) END AS ParentCode   From ("
                    Else
                        strQry = " WITH CTETEMP as ( SELECT '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,ROW_NUMBER() over (PARTITION By DocNo ORDER By CONVERT(DATE,RefDocDate,103)) AS RowNo, CustCode As [CustomerCode],CustName AS [Customer Name],DocNo As [DocumentNo],DocType AS [Doc Type],DocTypeCode AS [Doc Type Code],xxx.Cust_Group_Code AS [Cust Group Code],xxx.Route_No AS [Route No],xxx.Zone_Code AS [Zone Code],[Order Number] AS [Order Number],case when  CONVERT(VARCHAR,[Due Date],103)='01/01/1900' then null else CONVERT(VARCHAR,[Due Date],103) end AS [Due Date],[Against Sale No] AS [Against Sale No],[Against Sale Return No] As [Against Sale Return No],[Against MCC Material Sale Return] AS [Against MCC Material Sale Return],[AgainstScrap] AS [AgainstScrap],[Against VCGL] AS [Against VCGL],[Description] AS [Description],[Source Doc No] ,[Remarks] As [Remarks],[Child Cust Code],CASE WHEN ISNULL([Child Cust Code],'')<>'' THEN [Child Cust Code] ELSE CustCode END AS MainCustCode,CASE WHEN ISNULL([Child Cust Name],'')<>'' THEN [Child Cust Name]  ELSE CustName  END AS MainCustName,[Loc Code] AS  [Loc Code],[Loc Desp] AS  [Loc Desp],CONVERT(VARCHAR,DocDate,103) AS [Document Date],RefDocNo AS [Ref Doc No],CONVERT (VARCHAR,RefDocDate,101) AS [Ref Doc Date],SubDocType AS [Sub Doc Type],DrAmt AS DrAmt,CrAmt ,CASE WHEN CrAmt =0 THEN DrAmt ELSE CrAmt * -1 END AS [Trans Amt] ,BalAmt,CASE WHEN ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')<>''  THEN ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'') ELSE CustCode END AS ParentCode FROM ("
                    End If
                    strQry += Environment.NewLine + " -- AR INVOICE " + Environment.NewLine
                    strQry += " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,TSPL_Customer_Invoice_Head.Document_No AS DocNo,'AR Invoice' AS DocType,'IN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, '' AS RefDocNo, Null AS RefDocDate,''  As SubDocType,TSPL_Customer_Invoice_Head.Document_Total " &
                              " AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  From TSPL_Customer_Invoice_Head " &
                              " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code  WHERE TSPL_Customer_Invoice_Head.Status=1 " &
                              " AND TSPL_Customer_Invoice_Head.Document_Type='I' "

                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += Environment.NewLine + " --  CREDIT NOTE AGAINST INVOICE " + Environment.NewLine '' CHANGED TSPL_Customer_Invoice_Head.Document_Total AS CR_AMT TO TSPL_Customer_Invoice_Head.Document_Total - COALESCE(TSPL_Customer_Invoice_Head.BALANCE_AMOUNT,0) AS CrAmt
                    strQry += " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo,'Credit Note' AS DocType,'CN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_No ELSE '' END AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_Date ELSE NULL END AS RefDocDate,'Credit Note'  As SubDocType,0 AS DrAmt,ISNULL(TSPL_Customer_Invoice_Head.Document_Total ,0) AS CrAmt,0 AS TransAmt,0 AS BalAmt From TSPL_Customer_Invoice_Head " &
                              " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_Customer_Invoice_Head.Status=1 " &
                              " AND TSPL_Customer_Invoice_Head.Document_Type='C' AND ISNULL(RefDocNo,'')<>'' "

                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += Environment.NewLine + " -- CREDIT NOTE SEPEARTED " + Environment.NewLine
                    strQry += " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo,'Credit Note' AS DocType,'CN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_No ELSE '' END AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_Date ELSE NULL END AS RefDocDate,'Credit Note'  As SubDocType,0 AS DrAmt,TSPL_Customer_Invoice_Head.Document_Total  AS CrAmt,0 AS TransAmt,0 AS BalAmt From TSPL_Customer_Invoice_Head  LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_Customer_Invoice_Head.Status=1  AND TSPL_Customer_Invoice_Head.Document_Type='C' AND ISNULL(RefDocNo,'')='' "

                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += Environment.NewLine + " -- DEBIT NOTE SEPERATED " + Environment.NewLine
                    strQry += " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo,'Debit Note' AS DocType,'DN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate,'' AS RefDocNo, NULL AS RefDocDate,''  As SubDocType,TSPL_Customer_Invoice_Head.Document_Total AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  From TSPL_Customer_Invoice_Head " &
                              " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code  WHERE TSPL_Customer_Invoice_Head.Status=1 " &
                              " AND TSPL_Customer_Invoice_Head.Document_Type='D' AND ISNULL(RefDocNo,'')='' "

                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += Environment.NewLine + " -- DEBIT NOTE AGAINST INVOICE " + Environment.NewLine
                    strQry += " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo,'Debit Note' AS DocType,'DN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, TSPL_Customer_Invoice_Head.Document_No AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_Customer_Invoice_Head.Document_Date ELSE NULL END AS RefDocDate,'Debit Note'  As SubDocType,TSPL_Customer_Invoice_Head.Document_Total AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  From TSPL_Customer_Invoice_Head " &
                              " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code  WHERE TSPL_Customer_Invoice_Head.Status=1 " &
                              " AND TSPL_Customer_Invoice_Head.Document_Type='D' AND ISNULL(RefDocNo,'')<>'' "

                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += Environment.NewLine + " -- RECEIPT ENTRIES (APPLY DOCUMENT/RECEIPT) " + Environment.NewLine
                    strQry += " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode, TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_DETAIL.Document_No AS DocNo,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' ELSE NULL END AS DocType,ISNULL(TSPL_RECEIPT_HEADER.Receipt_Type,'') AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name], '' AS [Source Doc No],reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],TSPL_RECEIPT_DETAIL.Document_Date AS DocDate,TSPL_RECEIPT_HEADER.Receipt_No AS RefDocNo,CONVERT(DATE, TSPL_RECEIPT_HEADER.Receipt_Date  ,103) AS RefDocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' END  AS SubDocType, 0 as DrAmt, TSPL_RECEIPT_DETAIL.Applied_Amount  AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_RECEIPT_DETAIL   " &
                              " LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No  = TSPL_RECEIPT_DETAIL.Receipt_No  " &
                              " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " &
                              " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_DETAIL.Child_Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code  " &
                              " WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') AND ISNULL(SecurityDeposit,'')='N' "

                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += Environment.NewLine + " -- REFUND " + Environment.NewLine
                    strQry += " SELECT TSPL_RECEIPT_HEADER.Cust_Code as CustCode,TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_HEADER.Receipt_No AS DocNo,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' ELSE NULL END AS DocType,ISNULL(TSPL_RECEIPT_HEADER.Receipt_Type,'') AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) As [Loc Code] ,ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],TSPL_RECEIPT_HEADER.Receipt_Date AS DocDate,'' as RefDocNo,NULL AS RefDocDate,'' As SubDocType,TSPL_RECEIPT_HEADER.Receipt_Amount as DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM  TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('F') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  "

                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += Environment.NewLine + " -- ADVANCE/ON-ACCOUNT " + Environment.NewLine
                    strQry += " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode,TSPL_RECEIPT_HEADER.Customer_Name AS CustName,Receipt_No AS DocNo,CASE WHEN Receipt_Type='P' THEN 'Advance' WHEN Receipt_Type='O' THEN 'On Account' ELSE NULL END AS DocType,ISNULL(Receipt_Type,'') AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],Receipt_Date AS DocDate,'' as RefDocNo,NULL AS RefDocDate,''  AS SubDocType, 0  AS DrAmt, Receipt_Amount AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM  TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('P','O') AND ISNULL(SecurityDeposit,'')='N' "

                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += Environment.NewLine + " -- APPLY DOCUMENT AGAINST ADVANCE/ON-ACCOUNT " + Environment.NewLine
                    strQry += " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode, TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_HEADER.Applied_Receipt  AS DocNo,CASE WHEN (SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt)= 'P' THEN 'Advance' WHEN (SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt)= 'O' THEN 'On Account' END AS DocType,(SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt) AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code ,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name], '' AS [Source Doc No],reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],TSPL_RECEIPT_HEADER.Receipt_Date AS DocDate,TSPL_RECEIPT_HEADER.Receipt_No  AS RefDocNo,CONVERT(DATE, TSPL_RECEIPT_HEADER.Receipt_Date  ,103) AS RefDocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' END  AS SubDocType, 0 as DrAmt, TSPL_RECEIPT_DETAIL.Applied_Amount   AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_RECEIPT_HEADER " &
                              " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No  = TSPL_RECEIPT_DETAIL.Receipt_No  " &
                              " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " &
                              " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_DETAIL.Child_Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code  " &
                              " WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') AND ISNULL(SecurityDeposit,'')='N' AND ISNULL(Applied_Receipt,'')<>''"

                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += Environment.NewLine + " -- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT-BULK) " + Environment.NewLine
                    strQry += " SELECT TSPL_BANK_REVERSE.Cust_Code AS CustCode, TSPL_BANK_REVERSE.Cust_Name AS CustName,TSPL_Customer_Invoice_Head.Document_No  AS DocNo, 'Bank Reverse' AS DocType,'RV' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name],'' AS [Source Doc No],ISNULL (TSPL_INVOICE_MASTER_BULKSALE.Location_Code,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] , TSPL_Customer_Invoice_Head.Document_Date AS DocDate ,RHM.Receipt_No AS RefDocNo, RHM.Receipt_Date AS RefDocDate,CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' ELSE '' END  AS SubDocType, TSPL_BANK_REVERSE.Amount AS DrAmt, 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_INVOICE_MASTER_BULKSALE " &
                              " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No  " &
                              " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_DETAIL.Document_No =TSPL_Customer_Invoice_Head.Document_No " &
                              " LEFT OUTER JOIN TSPL_RECEIPT_HEADER RHM ON RHM.Receipt_No = TSPL_RECEIPT_DETAIL.Receipt_No   " &
                              " LEFT OUTER JOIN  TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE .Document_No = RHM.Receipt_No " &
                              " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_INVOICE_MASTER_BULKSALE.Location_Code " &
                              " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_DETAIL.Child_Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " &
                              " WHERE TSPL_BANK_REVERSE.Source_Type='AR' AND TSPL_BANK_REVERSE.post='P' AND RHM.Receipt_Type IN ('A','R')  AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' "

                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += Environment.NewLine + " -- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT) " + Environment.NewLine
                    strQry += " SELECT TSPL_BANK_REVERSE.Cust_Code AS CustCode, TSPL_BANK_REVERSE.Cust_Name AS CustName,RHM.Receipt_No  AS DocNo, 'Bank Reverse' AS DocType,'RV' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name],'' AS [Source Doc No],Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] , RHM.Receipt_Date AS DocDate ,RHM.Receipt_No AS RefDocNo, RHM.Receipt_Date AS RefDocDate,CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' ELSE '' END  AS SubDocType,  TSPL_BANK_REVERSE.Amount AS  DrAmt, 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_BANK_REVERSE " &
                              " LEFT OUTER JOIN  TSPL_RECEIPT_HEADER RHM ON TSPL_BANK_REVERSE.Document_No = RHM.Receipt_No  " &
                              " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) " &
                              " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code " &
                              " WHERE TSPL_BANK_REVERSE.Source_Type='AR' AND TSPL_BANK_REVERSE.post='P' AND RHM.Receipt_Type IN ('P','O')  AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' "

                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += Environment.NewLine + " -- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT) " + Environment.NewLine
                    strQry += " SELECT TSPL_BANK_REVERSE.Cust_Code AS CustCode, TSPL_BANK_REVERSE.Cust_Name AS CustName,TSPL_RECEIPT_DETAIL.Document_No AS DocNo, 'Bank Reverse' AS DocType,'RV' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(TSPL_CUSTOMER_MASTER.Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,ISNULL(TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order ,'') AS [Order Number],NULL  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name],'' AS [Source Doc No],Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] , RHM.Receipt_Date   AS DocDate ,TSPL_BANK_REVERSE.Reverse_Code AS RefDocNo, TSPL_BANK_REVERSE.Reversal_Date  AS RefDocDate,CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' ELSE '' END  AS SubDocType,  TSPL_BANK_REVERSE.Amount AS  DrAmt, 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_BANK_REVERSE " &
                              " LEFT OUTER JOIN TSPL_RECEIPT_HEADER RHM ON RHM.Receipt_No = TSPL_BANK_REVERSE.Document_No " &
                              " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No = RHM.Receipt_No " &
                              " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = RHM.Cust_Code " &
                              " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) " &
                              " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No = TSPL_RECEIPT_DETAIL.Document_No " &
                              " LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_Customer_Invoice_Head.Against_Sale_No =TSPL_SD_SHIPMENT_HEAD.Document_Code " &
                              " WHERE TSPL_BANK_REVERSE.Source_Type='AR' AND TSPL_BANK_REVERSE.post='P' AND RHM.Receipt_Type IN ('A','R') AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' "

                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += Environment.NewLine + " --  VCGL DATA(FIRST QUERY) " + Environment.NewLine
                    strQry += " SELECT TSPL_VCGL_Head.VC_Code AS CustCode, TSPL_VCGL_Head.VC_Name AS CustName, TSPL_VCGL_Head.Document_No AS DocNo,'VCGL' AS DocType,'AD' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name],'' AS [Source Doc No],ISNULL(TSPL_VCGL_Head.Location_Segment ,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp], CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) AS DocDate,'' AS RefDocNo,Null AS RefDocDate,''  As SubDocType,CASE WHEN  TSPL_VCGL_Head.Amount_Type='Cr' THEN  TSPL_VCGL_Head.Amount ELSE 0 END  AS DrAmt ,CASE WHEN  TSPL_VCGL_Head.Amount_Type='Dr' THEN  TSPL_VCGL_Head.Amount ELSE 0 END AS CrAmt,0 AS TransAmt,0 AS BalAmt  FROM TSPL_VCGL_Head LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_VCGL_Head.Location_Segment LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code= TSPL_CUSTOMER_MASTER.Cust_Code  WHERE TSPL_VCGL_Head.Document_Type='C' and  TSPL_VCGL_Head.Status=1 AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') ='' "

                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += Environment.NewLine + " -- -- VCGL DATA(SECOND QUERY) " + Environment.NewLine
                    strQry += " SELECT TSPL_VCGL_Detail.VCGL_Code AS CustCode, TSPL_VCGL_Detail.VCGL_Name AS CustName, TSPL_VCGL_Head.Document_No AS DocNo,'VCGL' AS DocType,'AD' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name],'' AS [Source Doc No],ISNULL(TSPL_VCGL_Head.Location_Segment ,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp], CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) AS DocDate, '' AS RefDocNo,NULL AS RefDocDate,''  As SubDocType, TSPL_VCGL_Detail.Dr_Amount AS DrAmt , TSPL_VCGL_Detail.Cr_Amount AS CrAmt ,0 AS TransAmt,0 AS BalAmt FROM  TSPL_VCGL_Detail LEFT OUTER JOIN  TSPL_VCGL_Head ON  TSPL_VCGL_Head .Document_No= TSPL_VCGL_Detail.Document_No LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_VCGL_Head.Location_Segment LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code= TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_VCGL_Head.Status=1 AND  TSPL_VCGL_Detail.Row_Type='Customer' AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') ='' " &
                              " )xxx " &
                     " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code =xxx.CustCode  " &
                     " WHERE CONVERT (date, DocDate,103) >= '" & strFromDate & "' And CONVERT (date,DocDate,103) <='" & strToDate & "'" + StrDocWiseFilter + ""
                    If ChkDocSumm.Checked = True Then
                        strQry += " Group By DocNo "
                    End If
                    strQry += " ) " '' Commented Old Query (Because it is not considering seperated query of credit note which has greater amount from invoice balance amount)
                    strQry += " Select * from (" &
                              " Select *, Case When CumAmt<0 Then 0 Else CumAmt End as CumAmt1 from (" &
                              " Select *, Case When (Select SUM(DrAmt-CrAmt) from CTETEMP CTET WHERE CTET.[DocumentNo]=CTETEMP.[DocumentNo] AND CTET.RowNo<=CTETEMP.RowNo)<0 Then (Select SUM(DrAmt-CrAmt) from CTETEMP CTET WHERE CTET.[DocumentNo]=CTETEMP.[DocumentNo] AND CTET.RowNo<CTETEMP.RowNo) Else [Trans Amt] End as [TransAmt1], (Select SUM(DrAmt-CrAmt) from CTETEMP CTET WHERE CTET.[DocumentNo]=CTETEMP.[DocumentNo] AND CTET.RowNo<=CTETEMP.RowNo) AS CumAmt FROM CTETEMP) YYY "
                    strQry += Environment.NewLine + " UNION ALL " + Environment.NewLine
                    strQry += " Select YYY.RunDate,YYY.CompanyAddress,YYY.CompanyName,YYY.FilterFromDate,YYY.FilterToDate,YYY.RowNo, YYY.CustomerCode AS CustomerCode,YYY.[Customer Name]  AS [Customer Name],YYY.[Ref Doc No] as [Document No],'Credit Note' AS [Doc Type],'CN' AS [Doc Type Code],YYY.[Cust Group Code] AS [Cust Group Code] ,YYY.[Route No]  AS Route_No,YYY.[Zone Code] AS Zone_Code,YYY.[Order Number] AS [Order Number],YYY.[Due Date] AS [Due Date],yyy.[Against Sale No]  AS [Against Sale No],YYY.[Against Sale Return No]  As [Against Sale Return No],YYY.[Against MCC Material Sale Return] AS [Against MCC Material Sale Return],YYY.AgainstScrap  AS [AgainstScrap],YYY.[Against VCGL]  AS [Against VCGL],YYY.Description  AS [Description],YYY.[Source Doc No] AS [Source Doc No],YYY.Remarks  As [Remarks],YYY.[Child Cust Code]  AS [Child Cust Code],YYY.MainCustCode AS MainCustCode,YYY.MainCustName AS MainCustName, " &
                              " YYY.[Loc Code] As [Loc Code],YYY.[Loc Desp] AS [Loc Desp], YYY.[Document Date] AS [Document Date],YYY.DocumentNo AS [Ref Doc No],YYY.[Ref Doc Date] AS [Ref Doc Date],'AR Invoice'  As [Sub Doc Type],YYY.DrAmt AS DrAmt,YYY.CrAmt  AS CrAmt,YYY.[Trans Amt] AS [Trans Amt],YYY.BalAmt AS BalAmt,YYY.ParentCode AS ParentCode, CumAmt As TransAmt1,CumAmt AS CumAmt,CumAmt AS CumAmt1 FROM (Select *, (Select SUM(DrAmt-CrAmt) from CTETEMP CTET WHERE CTET.[DocumentNo]=CTETEMP.[DocumentNo] AND CTET.RowNo<=CTETEMP.RowNo) AS CumAmt FROM CTETEMP) YYY WHERE [Doc Type Code]='CN' AND CumAmt<0) ZZZ ORDER BY RowNo "
                End If

                If BulkExport = 1 Then
                    transportSql.BulkExport("Customer_Ledger", strQry, "ORDER BY CTETemp.ACode,  CTETemp.RowNo", "csv", "select ctetemp.rowno")
                    Exit Sub
                ElseIf BulkExport = 2 Then
                    transportSql.BulkExport("Customer_Ledger", strQry, "ORDER BY CTETemp.ACode,  CTETemp.RowNo", "xls", "select ctetemp.rowno")
                    Exit Sub
                End If
                ''
                If rbtnNone.Checked Then
                    dtMain = clsDBFuncationality.GetDataTable(strQry)
                End If
                If rbtnNone.Checked = True AndAlso dtMain.Rows.Count <= 0 Then
                    btnPrint.Enabled = False
                    gvDetails.DataSource = Nothing
                    gvDetails.Columns.Clear()
                    gvDetails.Rows.Clear()
                    clsCommon.MyMessageBoxShow("Data not found")
                    Exit Sub
                Else
                    If rbtnCustWise.Checked Or rbtnNone.Checked Then
                        btnBack.Enabled = True
                    Else
                        btnBack.Enabled = False
                    End If
                    btnPrint.Enabled = True
                    gvDetails.DataSource = Nothing
                    gvDetails.Columns.Clear()
                    gvDetails.Rows.Clear()
                End If


                If ChkDocWise.Checked = False Then
                    If rbtnCustGroupWise.Checked OrElse rbtnCustGroupWiseDrCr.Checked Then
                        gvCustomerGroup.DataSource = Nothing
                        gvCustomerGroup.Columns.Clear()
                        gvCustomerGroup.Rows.Clear()
                        gvCustomerGroup.DataSource = dtCustGrp
                        gvCustomerGroup.Tag = IIf(rbtnCustGroupWiseDrCr.Checked, "DRCR", "")
                        FormatgvCustGroup()
                    End If
                    If rbtnCustomerCategory.Checked OrElse rbtnCustomerType.Checked Then
                        gvCustomerGroup.DataSource = Nothing
                        gvCustomerGroup.Columns.Clear()
                        gvCustomerGroup.Rows.Clear()
                        gvCustomerGroup.DataSource = dtCustGrp
                        gvCustomerGroup.Tag = IIf(rbtnCustomerCategory.Checked, "CC", "CT")
                        FormatgvCustGroup()
                    End If

                    If rbtnCustWise.Checked OrElse rbtnCustWiseDrCr.Checked Then
                        gvCustomer.DataSource = Nothing
                        gvCustomer.Columns.Clear()
                        gvCustomer.Rows.Clear()
                        gvCustomer.DataSource = dtCustomer
                        gvCustomer.Tag = IIf(rbtnCustWiseDrCr.Checked, "DRCR", "")
                        FormatgvCustomer()
                    End If
                End If

                If rbtnNone.Checked = True Then
                    gvDetails.DataSource = dtMain
                    gvDetails.AllowRowReorder = False
                    FormatGrid(False)
                End If
                If ChkDocWise.Checked = True Then
                    FormatGridDocWise()
                End If
                If ChkDocWise.Checked = False Then
                    gridHideVisible(IsDrillDown)
                End If
            End If
            If BackProcess = False Then
                If (rbtnCustGroupWise.Checked = True OrElse rbtnCustGroupWiseDrCr.Checked) AndAlso gvCustomer.Visible = True Then
                    FilterForLevels += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in ('" + clsCommon.myCstr(gvCustomerGroup.CurrentRow.Cells("Cust_Group_Code").Value) + "')  "
                End If
                If ((rbtnCustGroupWise.Checked = True OrElse rbtnCustGroupWiseDrCr.Checked) AndAlso gvDetails.Visible = True) OrElse (rbtnCustWise.Checked = True AndAlso gvDetails.Visible = True) Then
                    ACodeFilter = clsCommon.myCstr(gvCustomer.CurrentRow.Cells("ACode").Value)
                End If
                If ((rbtnCustWise.Checked = True OrElse rbtnCustWiseDrCr.Checked) AndAlso gvDetails.Visible = True) OrElse ((rbtnCustGroupWise.Checked = True OrElse rbtnCustGroupWiseDrCr.Checked) AndAlso gvDetails.Visible = True) Then
                    dvTemp1 = New DataView(dtMain)
                    dvTemp1.RowFilter = "ACode = '" + ACodeFilter + "'"
                    gvDetails.DataSource = Nothing
                    gvDetails.DataSource = dvTemp1.ToTable()
                    FormatGrid(False)
                End If
            End If
            BackProcess = False
            IsDrillDown = False

            If btnrefresh = False Then
                If isExportToExcel = True Then
                    Dim arrHeadrer As New List(Of String)
                    arrHeadrer.Add("From Date : " + strFromDate + "")
                    arrHeadrer.Add("To Date : " + strToDate + "")
                    If gvCustomerGroup.Visible Then
                        clsCommon.MyExportToExcel("Customer Ledger (Customer Group Wise)", gvCustomerGroup, arrHeadrer, "CustomerLedger")
                    ElseIf gvCustomer.Visible Then
                        clsCommon.MyExportToExcel("Customer Ledger (Customer Wise)", gvCustomer, arrHeadrer, "CustomerLedger")
                    Else
                        clsCommon.MyExportToExcel("Customer Ledger", gvDetails, arrHeadrer, "CustomerLedger")
                    End If
                Else
                    If ChkDocWise.Checked = False Then
                        Dim frmcrystal As New frmCrystalReportViewer()
                        If rbtnCustGroupWiseDrCr.Checked Then
                            If gvCustomerGroup.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustGrp, "rptCustomerLedgerSummaryDRCR_DEMO", "Customer Ledger")
                            ElseIf gvCustomer.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustomer, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                            ElseIf gvDetails.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dvTemp1.ToTable(), "rptCustomerLedger_DEMO", "Customer Ledger")
                            End If
                        ElseIf rbtnCustGroupWise.Checked Then
                            If gvCustomerGroup.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustGrp, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                            ElseIf gvCustomer.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustomer, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                            ElseIf gvDetails.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dvTemp1.ToTable(), "rptCustomerLedger_DEMO", "Customer Ledger")
                            End If
                        ElseIf rbtnCustWiseDrCr.Checked Then
                            'Balwinder
                            If gvCustomerGroup.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustGrp, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                            ElseIf gvCustomer.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustomer, "rptCustomerWiseDRCR", "Customer Ledger")
                            ElseIf gvDetails.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dvTemp1.ToTable(), "rptCustomerLedger_DEMO", "Customer Ledger")
                            End If
                        ElseIf rbtnCustWise.Checked Then
                            If gvCustomerGroup.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustGrp, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                            ElseIf gvCustomer.Visible And chkDateWise.Checked = True Then  '' BHA/04/10/18-000601
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustomer, "rptCustomerLedgerCustomerWiseWithDateWise", "Customer Ledger")

                                'Dim strCustomerDaywiseDr As String = " Select z.ACode as Cust_Code,format(z.DocDate ,'dd/MMM/yyyy') as DocDate,z.AName as description,z.DrAmt,case when z.OpngBal>0 then z.OpngBal else 0 end OpngBal  from ( " & strCustomerDaywise & " ) z where  z.DrAmt<>0 or z.OpngBal<>0 ORDER BY ACode  ,convert(date,DocDateForSorting ,103)"
                                'Dim strCustomerDaywiseCr As String = " Select z.ACode as Cust_Code,format(z.DocDate ,'dd/MMM/yyyy') as DocDate,z.AName as description,z.CrAmt,case when z.OpngBal<0 then z.OpngBal else 0 end OpngBal from ( " & strCustomerDaywise & " ) z where  z.CrAmt<>0 or z.OpngBal<>0 ORDER BY ACode  ,convert(date,DocDateForSorting ,103)"

                                'Dim strCustomerDaywiseDr As String = " Select z.ACode as Cust_Code,case when z.DocType ='OP' then format(" & clsCommon.myCDate(txtFromDate.Value).AddDays(-1) & " ,'dd/MMM/yyyy') else  format(z.DocDate ,'dd/MMM/yyyy') end  as DocDate,case when DocType ='IN' then z.AgainstInvoiceNo when  z.DocType ='OP' then z.DocNarr else z.DocNo end as description,case when z.DocType <>'OP' then z.DrAmt else 0 end DrAmt,case when z.DocType ='OP' and z.DrAmt >0 then z.DrAmt else 0 end OpngBal from ( " & strCustomerDaywise & " ) z where  z.DrAmt<>0 ORDER BY ACode  ,convert(date,DocDate ,103)"
                                'Dim strCustomerDaywiseCr As String = " Select z.ACode as Cust_Code,case when z.DocType ='OP' then format(" & clsCommon.myCDate(txtFromDate.Value).AddDays(-1) & " ,'dd/MMM/yyyy') else  format(z.DocDate ,'dd/MMM/yyyy') end  as DocDate,case when DocType ='IN' then z.AgainstInvoiceNo when  z.DocType ='OP' then z.DocNarr else z.DocNo end as description,case when z.DocType <>'OP' then z.CrAmt else 0 end CrAmt,case when z.DocType ='OP' and z.CrAmt >0 then z.CrAmt else 0 end OpngBal from ( " & strCustomerDaywise & " ) z where  z.cramt <>0 ORDER BY ACode  ,convert(date,DocDate ,103)"


                                'Dim strmainquery As String = getQueryCustomerMonthiwisereport(strCustomerDaywise)
                                'frmcrystal.funSubreport(CrystalReportFolder.SalesReport, strmainquery, strCustomerDaywiseDr, strCustomerDaywiseCr, "", "", "rptCustomerDayWise", "Customer Ledger Date Wise", "rptCustomerDayWiseDebit.rpt", "rptCustomerDayWiseCredit.rpt")
                            ElseIf gvCustomer.Visible And chkDateWise.Checked = False Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustomer, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")

                            ElseIf gvDetails.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dvTemp1.ToTable(), "rptCustomerLedger_DEMO", "Customer Ledger")
                            End If
                        ElseIf rbtnNone.Checked Then

                            If CustomerLedgerPrintConfirmation = True Then
                                Dim strFromDateprint As String = clsCommon.GetPrintDate(txtFromDate.Value.AddDays(-1), "dd/MMM/yyyy")
                                Dim strCustomerDaywiseDr As String = " Select z.ACode as Cust_Code,case when z.DocType ='OP' then '" & strFromDateprint & "' else  format(z.DocDate ,'dd/MMM/yyyy') end  as DocDate,case when DocType ='IN' then z.AgainstInvoiceNo when  z.DocType ='OP' then z.DocNarr else z.DocNo end as description,case when z.DocType <>'OP' then z.DrAmt else 0 end DrAmt,case when z.DocType ='OP' and z.DrAmt >0 then z.DrAmt else 0 end OpngBal from ( " & strCustomerDaywise & " ) z where  z.DrAmt<>0 ORDER BY ACode  ,convert(date,DocDate ,103)"
                                Dim strCustomerDaywiseCr As String = " Select z.ACode as Cust_Code,case when z.DocType ='OP' then '" & strFromDateprint & "' else  format(z.DocDate ,'dd/MMM/yyyy') end  as DocDate,case when DocType ='IN' then z.AgainstInvoiceNo when  z.DocType ='OP' then z.DocNarr else z.DocNo end as description,case when z.DocType <>'OP' then z.CrAmt else 0 end CrAmt,case when z.DocType ='OP' and z.CrAmt >0 then z.CrAmt else 0 end OpngBal from ( " & strCustomerDaywise & " ) z where  z.cramt <>0 ORDER BY ACode  ,convert(date,DocDate ,103)"


                                Dim strmainquery As String = getQueryCustomerMonthiwisereport(strCustomerDaywise)
                                frmcrystal.funSubreport(CrystalReportFolder.SalesReport, strmainquery, strCustomerDaywiseDr, strCustomerDaywiseCr, "", "", "rptCustomerDayWise", "Customer Ledger Date Wise", "rptCustomerDayWiseDebit.rpt", "rptCustomerDayWiseCredit.rpt")
                            Else
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtMain, "rptCustomerLedger_DEMO", "Customer Ledger")
                            End If

                        ElseIf rbtnDocWise.Checked Then
                            frmcrystal.funreport(CrystalReportFolder.SalesReport, dtMain, "rptCustomerLedger_DEMO", "Customer Ledger")
                        End If



                    Else
                        '' Anubhooti 11-Mar-2015 (Doc Wise Rpt)
                        If ChkDocSumm.Checked = True Then
                            Dim frmcrystal As New frmCrystalReportViewer()
                            frmcrystal.funreport(CrystalReportFolder.SalesReport, dtMain, "rptCustomerLedgerDocSummWise", "Customer Ledger")
                        Else
                            Dim frmcrystal As New frmCrystalReportViewer()
                            frmcrystal.funreport(CrystalReportFolder.SalesReport, dtMain, "rptCustomerLedgerDocWise", "Customer Ledger")
                        End If

                    End If
                End If
            End If
            gvDetails.EnableFiltering = True
            gvDetails.ShowFilteringRow = True
            RadPageView1.SelectedPage = RadPageViewPage2
            RadGroupBox1.Enabled = False
            ReStoreGridCust()
            ReStoreGridCustGrp()
            ReStoreGridDetail()
            GridSummaryRow()
            gvDetails.MasterTemplate.SortDescriptors.Clear()

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
    Function getQueryCustomerMonthiwisereport(ByVal strCustomerdayWiseqry As String) As String
        Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy")
        Dim strfromtodate As String = strFromDate + " to " + strToDate

        Dim qry As String = "select '" & strfromtodate & "' as strfromtodate ,format(getdate(),'dd/MMM/yyyy') as PrintDate,tspl_customer_master.Cust_Code , tspl_customer_master.Customer_Name,tspl_customer_master.Add1 AS CustAdd ,'Ph: '+replace(tspl_customer_master.Phone1,'(+91)__________','')  + case when len( replace(tspl_customer_master.Phone2,'(+91)__________',''))>0 then  +', ' +replace(tspl_customer_master.Phone2,'(+91)__________','') else '' end as CustPhone,'TIN NO : ' + 
tspl_customer_master.Tin_No AS Cust_TINNO , 'GSTIN : ' + tspl_customer_master.GSTNO AS Cust_GSTINNO ,tspl_company_master.Comp_Name ,tspl_company_master.Add1 + ' '+tspl_company_master.Add2  AS CompAdd ,'Ph: '+replace(tspl_company_master.Phone1,'(+91)__________','')  + case when len( replace(tspl_company_master.Phone2,'(+91)__________',''))>0 then  +', ' +replace(tspl_company_master.Phone2,'(+91)__________','') else '' end as CompPhone,'CIN : ' + tspl_company_master.CINNo  AS Comp_CINNO , 'GSTIN : ' + tspl_company_master.GSTReg_No AS Comp_GSTINNO ,tspl_company_master.pincode as CompPincode,'Contact   : '+replace(tspl_customer_master.Phone1,'(+91)__________','')  + case when len( replace(tspl_customer_master.Phone2,'(+91)__________',''))>0 then  +', ' +replace(tspl_customer_master.Phone2,'(+91)__________','') else '' end as Contact ,Final.* from (
select z.acode,sum(z.DrAmt) as DrAmt,sum(z.CrAmt) as CrAmt,( SUM(convert(decimal(18,2),OpngBal)) + SUM(convert(decimal(18,2),DrAmt)) ) -SUM(convert(decimal(18,2),CrAmt)) as BalAmt,case when sum(z.OpngBal)>0 then sum(z.OpngBal) else 0 end OpngBalDr,case when sum(z.OpngBal)<0 then sum(z.OpngBal) else 0 end OpngBalCr from( Select xy.ACode ,case when xy.DocType ='OP' then '' else  format(xy.DocDate ,'dd/MMM/yyyy') end  as DocDate,case when DocType ='IN' then xy.AgainstInvoiceNo when  xy.DocType ='OP' then xy.DocNarr else xy.DocNo end as description,case when xy.DocType <>'OP' then xy.CrAmt else 0 end CrAmt,case when xy.DocType <>'OP' then xy.DrAmt else 0 end DrAmt,case when xy.DocType ='OP' then xy.DrAmt -xy.CrAmt else 0 end OpngBal from ( " & strCustomerdayWiseqry & " )xy )z   GROUP BY ACode
)Final 
left outer join tspl_customer_master on tspl_customer_master.cust_code=Final.acode
left outer join tspl_company_master on tspl_company_master.Comp_Code =tspl_customer_master.Comp_Code "

        Return qry
    End Function
    Private Sub ReStoreGridDetail()
        Try
            Dim TempReportID As String = ""
            If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                TempReportID = "MISDebtorReport"
            Else
                TempReportID = "CustomerLedgerReport"
            End If
            If clsCommon.myLen(TempReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(TempReportID + "D", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvDetails.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvDetails.Columns.Count - 1 Step ii + 1
                        gvDetails.Columns(ii).IsVisible = False
                        gvDetails.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvDetails.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub ReStoreGridCust()
        Try
            Dim TempReportID As String = ""
            If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                TempReportID = "MISDebtorReport"
            Else
                TempReportID = "CustomerLedgerReport"
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

    Private Sub ReStoreGridCustGrp()
        Try
            Dim TempReportID As String = ""
            If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                TempReportID = "MISDebtorReport"
            Else
                TempReportID = "CustomerLedgerReport"
            End If
            If clsCommon.myLen(TempReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(TempReportID + "CG", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvCustomerGroup.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvCustomerGroup.Columns.Count - 1 Step ii + 1
                        gvCustomerGroup.Columns(ii).IsVisible = False
                        gvCustomerGroup.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvCustomerGroup.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub FormatGrid(ByVal IsFromDrillDown As Boolean)
        gvDetails.AllowAddNewRow = False
        gvDetails.TableElement.TableHeaderHeight = 40
        gvDetails.MasterTemplate.ShowRowHeaderColumn = False
        gvDetails.MasterTemplate.SummaryRowsBottom.Clear()
        For ii As Integer = 0 To gvDetails.Columns.Count - 1
            gvDetails.Columns(ii).ReadOnly = True
            gvDetails.Columns(ii).IsVisible = False
        Next
        If rbtnCustWise.Checked Then
            gvDetails.Columns("ParentCode").IsVisible = True
            gvDetails.Columns("ParentCode").Width = 101
            gvDetails.Columns("ParentCode").HeaderText = "Parent Customer Code"

            gvDetails.Columns("ParentName").IsVisible = True
            gvDetails.Columns("ParentName").Width = 221
            gvDetails.Columns("ParentName").HeaderText = "Parent Name"

            gvDetails.Columns("ACode").IsVisible = True
            gvDetails.Columns("ACode").Width = 101
            gvDetails.Columns("ACode").HeaderText = "Customer Code"

            gvDetails.Columns("AName").IsVisible = True
            gvDetails.Columns("AName").Width = 221
            gvDetails.Columns("AName").HeaderText = "Customer Name"



            If FormType = clsUserMgtCode.MISDebtorReport Then
                gvCustomer.Columns("Sales").IsVisible = True
                gvCustomer.Columns("Sales").Width = 150
                gvCustomer.Columns("Sales").HeaderText = "Sales"
                gvCustomer.Columns("Sales").FormatString = "{0:f2}"

                gvCustomer.Columns("CollectionRefund").IsVisible = True
                gvCustomer.Columns("CollectionRefund").Width = 150
                gvCustomer.Columns("CollectionRefund").HeaderText = "Collections"
                gvCustomer.Columns("CollectionRefund").FormatString = "{0:f2}"

                gvCustomer.Columns("DrNote").IsVisible = True
                gvCustomer.Columns("DrNote").Width = 150
                gvCustomer.Columns("DrNote").HeaderText = "DR Note"
                gvCustomer.Columns("DrNote").FormatString = "{0:f2}"

                gvCustomer.Columns("CrNote").IsVisible = True
                gvCustomer.Columns("CrNote").Width = 150
                gvCustomer.Columns("CrNote").HeaderText = "CR Note"
                gvCustomer.Columns("CrNote").FormatString = "{0:f2}"

            Else
                gvDetails.Columns("DrAmt").IsVisible = True
                gvDetails.Columns("DrAmt").Width = 101
                gvDetails.Columns("DrAmt").HeaderText = "Debit Amt"
                gvDetails.Columns("DrAmt").FormatString = "{0:f2}"

                gvDetails.Columns("CrAmt").IsVisible = True
                gvDetails.Columns("CrAmt").Width = 101
                gvDetails.Columns("CrAmt").HeaderText = "Credit Amt"
                gvDetails.Columns("CrAmt").FormatString = "{0:f2}"
            End If
            gvDetails.Columns("Closing").IsVisible = True
            gvDetails.Columns("Closing").Width = 101
            gvDetails.Columns("Closing").HeaderText = "Closing"
            gvDetails.Columns("Closing").FormatString = "{0:f2}"

        Else
            gvDetails.Columns("ParentCode").IsVisible = True
            gvDetails.Columns("ParentCode").Width = 101
            gvDetails.Columns("ParentCode").HeaderText = "Parent Customer Code"

            gvDetails.Columns("ParentName").IsVisible = True
            gvDetails.Columns("ParentName").Width = 221
            gvDetails.Columns("ParentName").HeaderText = "Parent Name"

            If AllowtoSHOWParentChildCustomer = True Then
                gvDetails.Columns("Child").IsVisible = True
                gvDetails.Columns("Child").Width = 101
                gvDetails.Columns("Child").HeaderText = "Child"
            End If

            gvDetails.Columns("ACode").IsVisible = True
            gvDetails.Columns("ACode").Width = 101
            gvDetails.Columns("ACode").HeaderText = "Customer Code"

            gvDetails.Columns("AName").IsVisible = True
            gvDetails.Columns("AName").Width = 221
            gvDetails.Columns("AName").HeaderText = "Customer Name"

            If clsCommon.CompairString(clsCommon.myCstr(ddlCurrencyType.SelectedValue), "1") <> CompairStringResult.Equal Then
                gvDetails.Columns("Zone_code").IsVisible = True
                gvDetails.Columns("Zone_code").Width = 101
                gvDetails.Columns("Zone_code").HeaderText = "Zone"
            End If

            gvDetails.Columns("DocDate").IsVisible = True
            gvDetails.Columns("DocDate").Width = 81
            gvDetails.Columns("DocDate").HeaderText = "Date"
            gvDetails.Columns("DocDate").FormatString = "{0:d}"

            gvDetails.Columns("DocType").IsVisible = True
            gvDetails.Columns("DocType").Width = 51
            gvDetails.Columns("DocType").HeaderText = "From"

            gvDetails.Columns("DocumentType").IsVisible = False
            gvDetails.Columns("DocumentType").Width = 51
            gvDetails.Columns("DocumentType").HeaderText = "Document Type"

            gvDetails.Columns("Location").IsVisible = True
            gvDetails.Columns("Location").Width = 71
            gvDetails.Columns("Location").HeaderText = "Unit"

            '' Anubhooti 23-Dec-2014 (Show Unit Desc)
            gvDetails.Columns("LocDesc").IsVisible = True
            gvDetails.Columns("LocDesc").Width = 101
            gvDetails.Columns("LocDesc").HeaderText = "Unit Description"
            ''

            gvDetails.Columns("DocNo").IsVisible = True
            gvDetails.Columns("DocNo").Width = 151
            gvDetails.Columns("DocNo").HeaderText = "Doc No"

            gvDetails.Columns("AgainstInvoiceNo").IsVisible = True
            gvDetails.Columns("AgainstInvoiceNo").Width = 151
            gvDetails.Columns("AgainstInvoiceNo").HeaderText = "Against Invoice No"

            gvDetails.Columns("DocNarr").IsVisible = True
            gvDetails.Columns("DocNarr").Width = 171
            gvDetails.Columns("DocNarr").HeaderText = "Narration"

            gvDetails.Columns("chequedetails").IsVisible = True
            gvDetails.Columns("chequedetails").Width = 171
            gvDetails.Columns("chequedetails").HeaderText = "Cheque Details"

            gvDetails.Columns("Currency_Code").IsVisible = True
            gvDetails.Columns("Currency_Code").Width = 70
            gvDetails.Columns("Currency_Code").HeaderText = "Currency"

            If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
                gvDetails.Columns("ConvRate").IsVisible = True
                gvDetails.Columns("ConvRate").Width = 100
                gvDetails.Columns("ConvRate").HeaderText = "Conv Rate"
                gvDetails.Columns("ConvRate").FormatString = "{0:f2}"
            End If

            If chkItemWise.Checked Then
                gvDetails.Columns("Item_Code").IsVisible = True
                gvDetails.Columns("Item_Code").Width = 70
                gvDetails.Columns("Item_Code").HeaderText = "Item Code"

                gvDetails.Columns("Item_Desc").IsVisible = True
                gvDetails.Columns("Item_Desc").Width = 101
                gvDetails.Columns("Item_Desc").HeaderText = "Item Desc"
            End If

            If FormType = clsUserMgtCode.MISDebtorReport Then
                gvDetails.Columns("Sales").IsVisible = True
                gvDetails.Columns("Sales").Width = 150
                gvDetails.Columns("Sales").HeaderText = "Sales"
                gvDetails.Columns("Sales").FormatString = "{0:f2}"

                gvDetails.Columns("CollectionRefund").IsVisible = True
                gvDetails.Columns("CollectionRefund").Width = 150
                gvDetails.Columns("CollectionRefund").HeaderText = "Collections"
                gvDetails.Columns("CollectionRefund").FormatString = "{0:f2}"

                gvDetails.Columns("DrAmt").IsVisible = True
                gvDetails.Columns("DrAmt").Width = 150
                gvDetails.Columns("DrAmt").HeaderText = "Debit Note"
                gvDetails.Columns("DrAmt").FormatString = "{0:f2}"

                gvDetails.Columns("CrAmt").IsVisible = True
                gvDetails.Columns("CrAmt").Width = 150
                gvDetails.Columns("CrAmt").HeaderText = "Credit Note"
                gvDetails.Columns("CrAmt").FormatString = "{0:f2}"
            Else
                gvDetails.Columns("DrAmt").IsVisible = True
                gvDetails.Columns("DrAmt").Width = 101
                gvDetails.Columns("DrAmt").HeaderText = "Total Dr"
                gvDetails.Columns("DrAmt").FormatString = "{0:f2}"

                gvDetails.Columns("CrAmt").IsVisible = True
                gvDetails.Columns("CrAmt").Width = 101
                gvDetails.Columns("CrAmt").HeaderText = "Total Cr"
                gvDetails.Columns("CrAmt").FormatString = "{0:f2}"
            End If

            If chkCumulativeClosing.Checked Then
                gvDetails.Columns("Closing").IsVisible = True
                gvDetails.Columns("Closing").Width = 101
                '' Anubhooti 23-Dec-2014 (Bal Amt is irrelevant in case of cumulative)
                gvDetails.Columns("Closing").HeaderText = "Closing"
                gvDetails.Columns("Closing").FormatString = "{0:f2}"
                ''
            End If

            gvDetails.Columns("SourceCode").IsVisible = False
            gvDetails.Columns("SourceCode").Width = 101
            gvDetails.Columns("SourceCode").HeaderText = "Source Code"

            gvDetails.Columns("Reconciliation_Date").IsVisible = True
            gvDetails.Columns("Reconciliation_Date").Width = 120
            gvDetails.Columns("Reconciliation_Date").HeaderText = "Reco Date" '

            gvDetails.Columns("Cust_Type_Code").IsVisible = True
            gvDetails.Columns("Cust_Type_Code").Width = 101
            gvDetails.Columns("Cust_Type_Code").HeaderText = "Cust Type Code"

            gvDetails.Columns("Cust_Type_Desc").IsVisible = True
            gvDetails.Columns("Cust_Type_Desc").Width = 120
            gvDetails.Columns("Cust_Type_Desc").HeaderText = "Cust Type Desc" '

            gvDetails.Columns("Cust_Category_Code").IsVisible = True
            gvDetails.Columns("Cust_Category_Code").Width = 101
            gvDetails.Columns("Cust_Category_Code").HeaderText = "Cust Category Code"

            gvDetails.Columns("CUST_CATEGORY_DESC").IsVisible = True
            gvDetails.Columns("CUST_CATEGORY_DESC").Width = 120
            gvDetails.Columns("CUST_CATEGORY_DESC").HeaderText = "Cust Category Desc" '
        End If
        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim ColumnTotal As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(ColumnTotal)
        'ColumnTotal = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(ColumnTotal)
        'ColumnTotal = New GridViewSummaryItem("Sales", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(ColumnTotal)
        'ColumnTotal = New GridViewSummaryItem("CollectionRefund", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(ColumnTotal)
        'ColumnTotal = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(ColumnTotal)
        'ColumnTotal = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(ColumnTotal)
        'If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
        '    Dim TotalClosing As New GridViewSummaryItem()
        '    TotalClosing.FormatString = "{0:F2}"
        '    TotalClosing.Name = "Closing"
        '    TotalClosing.AggregateExpression = "sum(DrAmt)-sum(CrAmt)+sUM(Sales)-SUM(CollectionRefund)"
        '    summaryRowItem.Add(TotalClosing)
        'Else
        '    Dim TotalClosing As New GridViewSummaryItem()
        '    TotalClosing.FormatString = "{0:F2}"
        '    TotalClosing.Name = "Closing"
        '    TotalClosing.AggregateExpression = "sum(DrAmt)-sum(CrAmt)"
        '    summaryRowItem.Add(TotalClosing)
        'End If

        'gvDetails.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        ReStoreGridDetail()
        ReStoreGridCust()
        ReStoreGridCustGrp()
        GridSummaryRow()
    End Sub

    Private Sub FormatgvCustGroup()
        Try
            gvCustomerGroup.AllowAddNewRow = False
            gvCustomerGroup.MasterTemplate.SummaryRowsBottom.Clear()
            For ii As Integer = 0 To gvCustomerGroup.Columns.Count - 1
                gvCustomerGroup.Columns(ii).ReadOnly = True
                gvCustomerGroup.Columns(ii).IsVisible = False
            Next

            gvCustomerGroup.Columns("Cust_Group_Code").IsVisible = True
            gvCustomerGroup.Columns("Cust_Group_Code").Width = 180
            gvCustomerGroup.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"

            gvCustomerGroup.Columns("Cust_Group_Desc").IsVisible = True
            gvCustomerGroup.Columns("Cust_Group_Desc").Width = 350
            gvCustomerGroup.Columns("Cust_Group_Desc").HeaderText = "Description"



            If FormType = clsUserMgtCode.MISDebtorReport Then
                gvCustomerGroup.Columns("Sales").IsVisible = True
                gvCustomerGroup.Columns("Sales").Width = 150
                gvCustomerGroup.Columns("Sales").HeaderText = "Sales"
                gvCustomerGroup.Columns("Sales").FormatString = "{0:f2}"

                gvCustomerGroup.Columns("CollectionRefund").IsVisible = True
                gvCustomerGroup.Columns("CollectionRefund").Width = 150
                gvCustomerGroup.Columns("CollectionRefund").HeaderText = "Collections"
                gvCustomerGroup.Columns("CollectionRefund").FormatString = "{0:f2}"

                gvCustomerGroup.Columns("DrAmt").IsVisible = True
                gvCustomerGroup.Columns("DrAmt").Width = 150
                gvCustomerGroup.Columns("DrAmt").HeaderText = "Debit Note"
                gvCustomerGroup.Columns("DrAmt").FormatString = "{0:f2}"

                gvCustomerGroup.Columns("CrAmt").IsVisible = True
                gvCustomerGroup.Columns("CrAmt").Width = 150
                gvCustomerGroup.Columns("CrAmt").HeaderText = "Credit Note"
                gvCustomerGroup.Columns("CrAmt").FormatString = "{0:f2}"
            Else
                gvCustomerGroup.Columns("DrAmt").IsVisible = True
                gvCustomerGroup.Columns("DrAmt").Width = 150
                gvCustomerGroup.Columns("DrAmt").HeaderText = "Debit Amt"
                gvCustomerGroup.Columns("DrAmt").FormatString = "{0:f2}"

                gvCustomerGroup.Columns("CrAmt").IsVisible = True
                gvCustomerGroup.Columns("CrAmt").Width = 150
                gvCustomerGroup.Columns("CrAmt").HeaderText = "Credit Amt"
                gvCustomerGroup.Columns("CrAmt").FormatString = "{0:f2}"
            End If

            If rbtnCustGroupWiseDrCr.Checked Then
                gvCustomerGroup.Columns("OpngBalDR").IsVisible = True
                gvCustomerGroup.Columns("OpngBalDR").Width = 150
                gvCustomerGroup.Columns("OpngBalDR").HeaderText = "Debit Opening"
                gvCustomerGroup.Columns("OpngBalDR").FormatString = "{0:f2}"

                gvCustomerGroup.Columns("OpngBalCR").IsVisible = True
                gvCustomerGroup.Columns("OpngBalCR").Width = 150
                gvCustomerGroup.Columns("OpngBalCR").HeaderText = "Credit Opening"
                gvCustomerGroup.Columns("OpngBalCR").FormatString = "{0:f2}"

                gvCustomerGroup.Columns("BalAmtDR").IsVisible = True
                gvCustomerGroup.Columns("BalAmtDR").Width = 150
                gvCustomerGroup.Columns("BalAmtDR").HeaderText = "Debit Balance"
                gvCustomerGroup.Columns("BalAmtDR").FormatString = "{0:f2}"

                gvCustomerGroup.Columns("BalAmtCR").IsVisible = True
                gvCustomerGroup.Columns("BalAmtCR").Width = 150
                gvCustomerGroup.Columns("BalAmtCR").HeaderText = "Credit Balance"
                gvCustomerGroup.Columns("BalAmtCR").FormatString = "{0:f2}"

            Else
                gvCustomerGroup.Columns("OpngBal").IsVisible = True
                gvCustomerGroup.Columns("OpngBal").Width = 150
                gvCustomerGroup.Columns("OpngBal").HeaderText = "Opening Balance"
                gvCustomerGroup.Columns("OpngBal").FormatString = "{0:f2}"

                gvCustomerGroup.Columns("BalAmt").IsVisible = True
                gvCustomerGroup.Columns("BalAmt").Width = 150
                gvCustomerGroup.Columns("BalAmt").HeaderText = "Balance Amount"
                gvCustomerGroup.Columns("BalAmt").FormatString = "{0:f2}"
            End If




            gvCustomerGroup.Columns("Cust_Type_Code").IsVisible = True
            gvCustomerGroup.Columns("Cust_Type_Code").Width = 101
            gvCustomerGroup.Columns("Cust_Type_Code").HeaderText = "Cust Type Code"

            gvCustomerGroup.Columns("Cust_Type_Desc").IsVisible = True
            gvCustomerGroup.Columns("Cust_Type_Desc").Width = 120
            gvCustomerGroup.Columns("Cust_Type_Desc").HeaderText = "Cust Type Desc" '

            gvCustomerGroup.Columns("Cust_Category_Code").IsVisible = True
            gvCustomerGroup.Columns("Cust_Category_Code").Width = 101
            gvCustomerGroup.Columns("Cust_Category_Code").HeaderText = "Cust Category Code"

            gvCustomerGroup.Columns("CUST_CATEGORY_DESC").IsVisible = True
            gvCustomerGroup.Columns("CUST_CATEGORY_DESC").Width = 120
            gvCustomerGroup.Columns("CUST_CATEGORY_DESC").HeaderText = "Cust Category Desc" '

            'Dim summaryRowItem As New GridViewSummaryRowItem()

            'Dim TotalAmt As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)

            'If rbtnCustGroupWiseDrCr.Checked Then
            '    TotalAmt = New GridViewSummaryItem("OpngBalDR", "{0:F2}", GridAggregateFunction.Sum)
            '    summaryRowItem.Add(TotalAmt)
            '    TotalAmt = New GridViewSummaryItem("OpngBalCR", "{0:F2}", GridAggregateFunction.Sum)
            '    summaryRowItem.Add(TotalAmt)
            '    TotalAmt = New GridViewSummaryItem("BalAmtDR", "{0:F2}", GridAggregateFunction.Sum)
            '    summaryRowItem.Add(TotalAmt)
            '    TotalAmt = New GridViewSummaryItem("BalAmtCR", "{0:F2}", GridAggregateFunction.Sum)
            '    summaryRowItem.Add(TotalAmt)
            'Else
            '    TotalAmt = New GridViewSummaryItem("OpngBal", "{0:F2}", GridAggregateFunction.Sum)
            '    summaryRowItem.Add(TotalAmt)
            '    If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
            '        Dim TotalClosing As New GridViewSummaryItem()
            '        TotalClosing.FormatString = "{0:F2}"
            '        TotalClosing.Name = "BalAmt"
            '        TotalClosing.AggregateExpression = "sum(OpngBal)+sum(DrAmt)-sum(CrAmt)+sUM(Sales)-SUM(CollectionRefund)"
            '        summaryRowItem.Add(TotalClosing)
            '    Else
            '        Dim TotalClosing As New GridViewSummaryItem()
            '        TotalClosing.FormatString = "{0:F2}"
            '        TotalClosing.Name = "BalAmt"
            '        TotalClosing.AggregateExpression = "sum(OpngBal)+sum(DrAmt)-sum(CrAmt)"
            '        summaryRowItem.Add(TotalClosing)
            '    End If
            'End If

            'TotalAmt = New GridViewSummaryItem("Sales", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("CollectionRefund", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)

            'gvCustomerGroup.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            ReStoreGridCustGrp()
            GridSummaryRow()
        Catch ex As Exception
            Throw New Exception(ex.Message)
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


            gvCustomer.Columns("ParentCode").IsVisible = True
            gvCustomer.Columns("ParentCode").Width = 101
            gvCustomer.Columns("ParentCode").HeaderText = "Parent Customer Code"

            gvCustomer.Columns("ParentName").IsVisible = True
            gvCustomer.Columns("ParentName").Width = 221
            gvCustomer.Columns("ParentName").HeaderText = "Parent Name"

            If AllowtoSHOWParentChildCustomer = True Then
                gvCustomer.Columns("Child").IsVisible = True
                gvCustomer.Columns("Child").Width = 101
                gvCustomer.Columns("Child").HeaderText = "Child"
            End If


            gvCustomer.Columns("ACode").IsVisible = True
            gvCustomer.Columns("ACode").Width = 180
            gvCustomer.Columns("ACode").HeaderText = "Customer Code"

            gvCustomer.Columns("AName").IsVisible = True
            gvCustomer.Columns("AName").Width = 350
            gvCustomer.Columns("AName").HeaderText = "Name"

            If chkDateWise.Checked Then
                gvCustomer.Columns("DocDate").IsVisible = True
                gvCustomer.Columns("DocDate").Width = 350
                gvCustomer.Columns("DocDate").HeaderText = "Date"
                gvCustomer.Columns("DocDate").FormatString = "{0:d}"
            ElseIf chkMonthWise.Checked = True Then
                gvCustomer.Columns("DocDate").IsVisible = True
                gvCustomer.Columns("DocDate").Width = 350
                gvCustomer.Columns("DocDate").HeaderText = "Month"
            End If

            If clsCommon.CompairString(clsCommon.myCstr(ddlCurrencyType.SelectedValue), "1") <> CompairStringResult.Equal Then
                gvCustomer.Columns("Zone_code").IsVisible = True
                gvCustomer.Columns("Zone_code").Width = 350
                gvCustomer.Columns("Zone_code").HeaderText = "Zone"

                gvCustomer.Columns("Route_No").IsVisible = True
                gvCustomer.Columns("Route_No").Width = 350
                gvCustomer.Columns("Route_No").HeaderText = "Route"
            End If
            If FormType = clsUserMgtCode.MISDebtorReport Then
                gvCustomer.Columns("Sales").IsVisible = True
                gvCustomer.Columns("Sales").Width = 150
                gvCustomer.Columns("Sales").HeaderText = "Sales"
                gvCustomer.Columns("Sales").FormatString = "{0:f2}"

                gvCustomer.Columns("CollectionRefund").IsVisible = True
                gvCustomer.Columns("CollectionRefund").Width = 150
                gvCustomer.Columns("CollectionRefund").HeaderText = "Collections"
                gvCustomer.Columns("CollectionRefund").FormatString = "{0:f2}"

                gvCustomer.Columns("DrAmt").IsVisible = True
                gvCustomer.Columns("DrAmt").Width = 150
                gvCustomer.Columns("DrAmt").HeaderText = "Debit Note"
                gvCustomer.Columns("DrAmt").FormatString = "{0:f2}"

                gvCustomer.Columns("CrAmt").IsVisible = True
                gvCustomer.Columns("CrAmt").Width = 150
                gvCustomer.Columns("CrAmt").HeaderText = "Credit Note"
                gvCustomer.Columns("CrAmt").FormatString = "{0:f2}"
            Else
                gvCustomer.Columns("DrAmt").IsVisible = True
                gvCustomer.Columns("DrAmt").Width = 150
                gvCustomer.Columns("DrAmt").HeaderText = "Debit Amt"
                gvCustomer.Columns("DrAmt").FormatString = "{0:f2}"

                gvCustomer.Columns("CrAmt").IsVisible = True
                gvCustomer.Columns("CrAmt").Width = 150
                gvCustomer.Columns("CrAmt").HeaderText = "Credit Amt"
                gvCustomer.Columns("CrAmt").FormatString = "{0:f2}"
            End If

            If rbtnCustWiseDrCr.Checked Then
                gvCustomer.Columns("OpngBalDR").IsVisible = True
                gvCustomer.Columns("OpngBalDR").Width = 150
                gvCustomer.Columns("OpngBalDR").HeaderText = "Debit Opening"
                gvCustomer.Columns("OpngBalDR").FormatString = "{0:f2}"

                gvCustomer.Columns("OpngBalCR").IsVisible = True
                gvCustomer.Columns("OpngBalCR").Width = 150
                gvCustomer.Columns("OpngBalCR").HeaderText = "Credit Opening"
                gvCustomer.Columns("OpngBalCR").FormatString = "{0:f2}"

                gvCustomer.Columns("BalAmtDR").IsVisible = True
                gvCustomer.Columns("BalAmtDR").Width = 150
                gvCustomer.Columns("BalAmtDR").HeaderText = "Debit Balance"
                gvCustomer.Columns("BalAmtDR").FormatString = "{0:f2}"

                gvCustomer.Columns("BalAmtCR").IsVisible = True
                gvCustomer.Columns("BalAmtCR").Width = 150
                gvCustomer.Columns("BalAmtCR").HeaderText = "Credit Balance"
                gvCustomer.Columns("BalAmtCR").FormatString = "{0:f2}"
            Else
                gvCustomer.Columns("OpngBal").IsVisible = True
                gvCustomer.Columns("OpngBal").Width = 150
                gvCustomer.Columns("OpngBal").HeaderText = "Opening Balance"
                gvCustomer.Columns("OpngBal").FormatString = "{0:f2}"

                gvCustomer.Columns("BalAmt").IsVisible = True
                gvCustomer.Columns("BalAmt").Width = 150
                gvCustomer.Columns("BalAmt").HeaderText = "Balance Amount"
                gvCustomer.Columns("BalAmt").FormatString = "{0:f2}"

            End If

            gvCustomer.Columns("Cust_Type_Code").IsVisible = True
            gvCustomer.Columns("Cust_Type_Code").Width = 101
            gvCustomer.Columns("Cust_Type_Code").HeaderText = "Cust Type Code"

            gvCustomer.Columns("Cust_Type_Desc").IsVisible = True
            gvCustomer.Columns("Cust_Type_Desc").Width = 120
            gvCustomer.Columns("Cust_Type_Desc").HeaderText = "Cust Type Desc" '

            gvCustomer.Columns("Cust_Category_Code").IsVisible = True
            gvCustomer.Columns("Cust_Category_Code").Width = 101
            gvCustomer.Columns("Cust_Category_Code").HeaderText = "Cust Category Code"

            gvCustomer.Columns("CUST_CATEGORY_DESC").IsVisible = True
            gvCustomer.Columns("CUST_CATEGORY_DESC").Width = 120
            gvCustomer.Columns("CUST_CATEGORY_DESC").HeaderText = "Cust Category Desc" '

            'Dim summaryRowItem As New GridViewSummaryRowItem()
            'Dim TotalAmt As New GridViewSummaryItem("OpngBal", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("Sales", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("CollectionRefund", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)
            'TotalAmt = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(TotalAmt)

            'If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
            '    Dim TotalClosing As New GridViewSummaryItem()
            '    TotalClosing.FormatString = "{0:F2}"
            '    TotalClosing.Name = "BalAmt"
            '    TotalClosing.AggregateExpression = "sum(OpngBal)+sum(DrAmt)-sum(CrAmt)+sUM(Sales)-SUM(CollectionRefund)"
            '    summaryRowItem.Add(TotalClosing)
            'Else
            '    Dim TotalClosing As New GridViewSummaryItem()
            '    TotalClosing.FormatString = "{0:F2}"
            '    TotalClosing.Name = "BalAmt"
            '    TotalClosing.AggregateExpression = "sum(OpngBal)+sum(DrAmt)-sum(CrAmt)"
            '    summaryRowItem.Add(TotalClosing)
            'End If
            'If rbtnCustWiseDrCr.Checked Then
            '    TotalAmt = New GridViewSummaryItem("OpngBalDR", "{0:F2}", GridAggregateFunction.Sum)
            '    summaryRowItem.Add(TotalAmt)
            '    TotalAmt = New GridViewSummaryItem("OpngBalCR", "{0:F2}", GridAggregateFunction.Sum)
            '    summaryRowItem.Add(TotalAmt)
            '    TotalAmt = New GridViewSummaryItem("BalAmtDR", "{0:F2}", GridAggregateFunction.Sum)
            '    summaryRowItem.Add(TotalAmt)
            '    TotalAmt = New GridViewSummaryItem("BalAmtCR", "{0:F2}", GridAggregateFunction.Sum)
            '    summaryRowItem.Add(TotalAmt)
            'End If
            'gvCustomer.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            ReStoreGridCust()
            GridSummaryRow()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    '' Anubhooti 02-March-2015
    Private Sub FormatGridDocWise()
        gvDetails.Visible = True
        gvCustomer.Visible = False
        gvCustomerGroup.Visible = False
        gvDetails.AllowAddNewRow = False
        gvDetails.TableElement.TableHeaderHeight = 40
        gvDetails.MasterTemplate.ShowRowHeaderColumn = False
        gvDetails.MasterTemplate.SummaryRowsBottom.Clear()
        gvDetails.GroupDescriptors.Clear()
        For ii As Integer = 0 To gvDetails.Columns.Count - 1
            gvDetails.Columns(ii).ReadOnly = True
            gvDetails.Columns(ii).IsVisible = False
        Next
        If ChkDocWise.Checked Then
            gvDetails.Columns("CustomerCode").IsVisible = True
            gvDetails.Columns("CustomerCode").Width = 150
            gvDetails.Columns("CustomerCode").HeaderText = "Customer Code"

            gvDetails.Columns("Customer Name").IsVisible = True
            gvDetails.Columns("Customer Name").Width = 220
            gvDetails.Columns("Customer Name").HeaderText = "Customer Name"

            gvDetails.Columns("MainCustCode").IsVisible = True
            gvDetails.Columns("MainCustCode").Width = 150
            gvDetails.Columns("MainCustCode").HeaderText = "MainCustCode"

            gvDetails.Columns("MainCustName").IsVisible = True
            gvDetails.Columns("MainCustName").Width = 220
            gvDetails.Columns("MainCustName").HeaderText = "MainCustName"

            gvDetails.Columns("DocumentNo").IsVisible = True
            gvDetails.Columns("DocumentNo").Width = 100
            gvDetails.Columns("DocumentNo").HeaderText = "Document No"

            gvDetails.Columns("Doc Type").IsVisible = True
            gvDetails.Columns("Doc Type").Width = 130
            gvDetails.Columns("Doc Type").HeaderText = "Document Type"

            gvDetails.Columns("Document Date").IsVisible = True
            gvDetails.Columns("Document Date").Width = 101
            gvDetails.Columns("Document Date").HeaderText = "Document Date"

            gvDetails.Columns("Ref Doc No").IsVisible = True
            gvDetails.Columns("Ref Doc No").Width = 101
            gvDetails.Columns("Ref Doc No").HeaderText = "Ref Doc No"

            gvDetails.Columns("Ref Doc Date").IsVisible = True
            gvDetails.Columns("Ref Doc Date").Width = 101
            gvDetails.Columns("Ref Doc Date").HeaderText = "Ref Doc Date"

            gvDetails.Columns("Sub Doc Type").IsVisible = True
            gvDetails.Columns("Sub Doc Type").Width = 130
            gvDetails.Columns("Sub Doc Type").HeaderText = "Sub Doc Type"

            gvDetails.Columns("DrAmt").IsVisible = False
            gvDetails.Columns("DrAmt").Width = 150
            gvDetails.Columns("DrAmt").HeaderText = "Debit Amt"
            gvDetails.Columns("DrAmt").FormatString = "{0:f2}"

            gvDetails.Columns("CrAmt").IsVisible = False
            gvDetails.Columns("CrAmt").Width = 150
            gvDetails.Columns("CrAmt").HeaderText = "Credit Amt"
            gvDetails.Columns("CrAmt").FormatString = "{0:f2}"
            If ChkDocSumm.Checked Then
                gvDetails.Columns("Trans Amt").IsVisible = False
                gvDetails.Columns("Trans Amt").Width = 101
                gvDetails.Columns("Trans Amt").HeaderText = "Old Trans Amt"
                gvDetails.Columns("Trans Amt").FormatString = "{0:f2}"

                gvDetails.Columns("TransAmt1").IsVisible = False
                gvDetails.Columns("TransAmt1").Width = 101
                gvDetails.Columns("TransAmt1").HeaderText = "Transaction Amount"
                gvDetails.Columns("TransAmt1").FormatString = "{0:f2}"
            Else
                gvDetails.Columns("Trans Amt").IsVisible = False
                gvDetails.Columns("Trans Amt").Width = 101
                gvDetails.Columns("Trans Amt").HeaderText = "Old Trans Amt"
                gvDetails.Columns("Trans Amt").FormatString = "{0:f2}"

                gvDetails.Columns("TransAmt1").IsVisible = True
                gvDetails.Columns("TransAmt1").Width = 101
                gvDetails.Columns("TransAmt1").HeaderText = "Transaction Amount"
                gvDetails.Columns("TransAmt1").FormatString = "{0:f2}"
            End If

            gvDetails.Columns("BalAmt").IsVisible = False
            gvDetails.Columns("BalAmt").Width = 101
            gvDetails.Columns("BalAmt").HeaderText = "Bal Amount"
            gvDetails.Columns("BalAmt").FormatString = "{0:f2}"

            gvDetails.Columns("CumAmt").IsVisible = False
            gvDetails.Columns("CumAmt").Width = 150
            gvDetails.Columns("CumAmt").HeaderText = "CumAmt"
            gvDetails.Columns("CumAmt").FormatString = "{0:f2}"

            gvDetails.Columns("CumAmt1").IsVisible = True
            gvDetails.Columns("CumAmt1").Width = 150
            gvDetails.Columns("CumAmt1").HeaderText = "Balance"
            gvDetails.Columns("CumAmt1").FormatString = "{0:f2}"
        End If
        gvDetails.ShowGroupPanel = True
        gvDetails.GroupDescriptors.Add(New GridGroupByExpression("DocumentNo As DocumentNo format ""{0}: {1}"" group by DocumentNo"))
        gvDetails.MasterTemplate.ExpandAllGroups()
        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim dramt As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(dramt)
        'If ChkDocSumm.Checked = True Then
        '    Dim CumAmt1 As New GridViewSummaryItem("CumAmt1", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(CumAmt1)
        'Else
        '    Dim summaryItem As New GridViewSummaryItem()
        '    summaryItem.FormatString = "{0:F2}"
        '    summaryItem.Name = "CumAmt1"
        '    summaryItem.AggregateExpression = "SUM([TransAmt1])"
        '    summaryRowItem.Add(summaryItem)
        '    gvDetails.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'End If
        gvDetails.BestFitColumns()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
    End Sub

    Sub FilterOFDocumnetType()
        Dim dt As DataTable = Nothing
        dt = clsDBFuncationality.GetDataTable("Select * from TSPL_CustomerLedger_filter where Created_By='" & objCommonVar.CurrentUserCode & "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            chkAdjustment.Checked = IIf(dt.Rows(0)("Adjustment") = 1, True, False)
            chkCreditNote.Checked = IIf(dt.Rows(0)("CreditNote") = 1, True, False)
            chkDebitNote.Checked = IIf(dt.Rows(0)("DebitNote") = 1, True, False)
            chkApplyDocument.Checked = IIf(dt.Rows(0)("ApplyDocument") = 1, True, False)
            ChkInvoice.Checked = IIf(dt.Rows(0)("Invoice") = 1, True, False)
            chkOnAccount.Checked = IIf(dt.Rows(0)("OnAccount") = 1, True, False)
            chkAdvance.Checked = IIf(dt.Rows(0)("Advance") = 1, True, False)
            chkreceipt.Checked = IIf(dt.Rows(0)("Receipt") = 1, True, False)
            chkrefund.Checked = IIf(dt.Rows(0)("Refund") = 1, True, False)
            chkBankReverse.Checked = IIf(dt.Rows(0)("BankReverse") = 1, True, False)
            ChkUnapplied.Checked = IIf(dt.Rows(0)("UnApplied") = 1, True, False)
        Else
            chkAdjustment.Checked = False
            chkCreditNote.Checked = False
            chkDebitNote.Checked = False
            chkApplyDocument.Checked = False
            ChkInvoice.Checked = False
            chkOnAccount.Checked = False
            chkAdvance.Checked = False
            chkreceipt.Checked = False
            chkrefund.Checked = False
            chkBankReverse.Checked = False
            ChkUnapplied.Checked = False
        End If
        dt = Nothing
    End Sub

    Sub inSertOrUpdateFilterData()
        Dim count As Integer = 0
        Dim qry As String = String.Empty
        count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_CustomerLedger_filter where Created_By ='" & objCommonVar.CurrentUserCode & "'"))
        If count > 0 Then
            qry = "Update TSPL_CustomerLedger_filter set Adjustment=" & IIf(chkAdjustment.Checked = True, 1, 0) & ",CreditNote=" & IIf(chkCreditNote.Checked = True, 1, 0) & ",DebitNote=" & IIf(chkDebitNote.Checked = True, 1, 0) & ",ApplyDocument=" & IIf(chkApplyDocument.Checked = True, 1, 0) & ",Invoice=" & IIf(ChkInvoice.Checked = True, 1, 0) & ",OnAccount=" & IIf(chkOnAccount.Checked = True, 1, 0) & ", " & _
                " Advance=" & IIf(chkAdvance.Checked = True, 1, 0) & ",Receipt=" & IIf(chkreceipt.Checked = True, 1, 0) & ",Refund=" & IIf(chkrefund.Checked = True, 1, 0) & ",BankReverse=" & IIf(chkBankReverse.Checked = True, 1, 0) & ",UnApplied=" & IIf(ChkUnapplied.Checked = True, 1, 0) & ",Modified_By='" & objCommonVar.CurrentUserCode & "',Modified_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "' " & _
                     " where Created_By='" & objCommonVar.CurrentUserCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Else
            qry = "Insert into TSPL_CustomerLedger_filter(Adjustment,CreditNote,DebitNote,ApplyDocument,Invoice,OnAccount,Advance,Receipt,Refund,BankReverse,UnApplied,Created_By,Created_Date,Modified_By,Modified_Date) values  " & _
                " (" & IIf(chkAdjustment.Checked = True, 1, 0) & "," & IIf(chkCreditNote.Checked = True, 1, 0) & "," & IIf(chkDebitNote.Checked = True, 1, 0) & "," & IIf(chkApplyDocument.Checked = True, 1, 0) & "," & IIf(ChkInvoice.Checked = True, 1, 0) & "," & IIf(chkOnAccount.Checked = True, 1, 0) & ", " & _
                " " & IIf(chkAdvance.Checked = True, 1, 0) & "," & IIf(chkreceipt.Checked = True, 1, 0) & "," & IIf(chkrefund.Checked = True, 1, 0) & "," & IIf(chkBankReverse.Checked = True, 1, 0) & "," & IIf(ChkUnapplied.Checked = True, 1, 0) & ",'" & objCommonVar.CurrentUserCode & "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "','" & objCommonVar.CurrentUserCode & "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "')  "
            clsDBFuncationality.ExecuteNonQuery(qry)
        End If
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
        dtMain = Nothing
        dtCustGrp = Nothing
        dtCustomer = Nothing
        dtOpening = Nothing
        GC.Collect()
        If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
            PrintForCustomerCurrency()
        Else
            print()
        End If
        inSertOrUpdateFilterData()
        btnrefresh = False
        ReStoreGridCust()
        ReStoreGridCustGrp()
        ReStoreGridDetail()
        GridSummaryRow()
        gvDetails.MasterTemplate.SortDescriptors.Clear()
        ReportID = GetReportID()
        PageSetupReport_ID = GetReportID()
    End Sub

    Sub PrintForCustomerCurrency(Optional ByVal BulkExport As Integer = 0)
        Dim CompanyAdd As String = ""
        Dim compname As String = ""
        Try
            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer")
                Return
            End If
            If chkCompanySelect.IsChecked AndAlso cbgCompany.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one company")
                Return
            End If
            If chkCustGrpSelect.IsChecked AndAlso cbgCustGrp.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer Group")
                Return
            End If
            If ChkISParentCust.Checked = True Then
                If ChkParentCustSelect.IsChecked AndAlso cbgParentCust.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select at least one parent customer")
                    Return
                End If
            End If

            If ChkCustTypeSelect.IsChecked AndAlso cbgcusttype.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer Type")
                Return
            End If
            If ChkCustCatSelect.IsChecked AndAlso cbgcustcat.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer Category")
                Return
            End If

            Dim qry As String
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

            Dim CheckCustomer As String = ""
            If chkActive.Checked Then
                CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='N'"
            ElseIf chkInactive.Checked Then
                CheckCustomer = " AND TSPL_CUSTOMER_MASTER.Status='Y'"
            End If

            Dim FilterForLevels As String = String.Empty
            Dim FilterForDetail As String = String.Empty
            Dim ACodeFilter As String = String.Empty
            If btnrefresh = True Then

                Dim BaseQry As String = ""
                Dim BaseQryOPENINGINCASEOFMIS As String = ""

                Dim strsecurity As String = String.Empty
                If TxtSecurity.arrValueMember IsNot Nothing AndAlso TxtSecurity.arrValueMember.Count > 0 Then
                    strsecurity = clsCommon.GetMulcallString(TxtSecurity.arrValueMember)
                Else
                    strsecurity = ""
                End If
                If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                    BaseQry = clsCustomerMaster.GetCustomerBaseQryforCustomerCurrency(chkItemWise.Checked, ChkSecurity.Checked, strsecurity, rbtnDocWise.Checked, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), False, True, chkIncludeApplyDocument.Checked)
                    BaseQryOPENINGINCASEOFMIS = clsCustomerMaster.GetCustomerBaseQryforCustomerCurrency(chkItemWise.Checked, ChkSecurity.Checked, strsecurity, rbtnDocWise.Checked, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), False, False, chkIncludeApplyDocument.Checked)
                Else
                    BaseQry = clsCustomerMaster.GetCustomerBaseQryforCustomerCurrency(chkItemWise.Checked, ChkSecurity.Checked, strsecurity, rbtnDocWise.Checked, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " " & ddlCurrencyType.SelectedValue & ""), False, False, chkIncludeApplyDocument.Checked)
                    BaseQryOPENINGINCASEOFMIS = BaseQry
                End If

                strtempBaseQryforopening = clsCustomerMaster.GetCustomerBaseQryForOpeningforCustomerCurrency(chkItemWise.Checked, ChkSecurity.Checked, strsecurity, rbtnDocWise.Checked, IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, "" & ddlCurrencyType.SelectedValue & ""), False, chkIncludeApplyDocument.Checked)


                ''richa Agarwal 27 Oct,2017 
                Dim AllowTrasactionFilterOnCustomerLedger As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowTransactionFiltersOnCustomerlegder, clsFixedParameterCode.AllowTransactionFiltersOnCustomerlegder, Nothing)) = 1, True, False))
                If AllowTrasactionFilterOnCustomerLedger = True Then
                    ' ''RICHA 6 OCT,2017
                    BaseQry += " WHERE 1=1"
                    BaseQryOPENINGINCASEOFMIS += " WHERE 1=1"
                    Dim StrCONDITION As String = String.Empty
                    If chkAdjustment.Checked = True Then
                        StrCONDITION += "'Adjustment',"
                    End If
                    If chkApplyDocument.Checked = True Then
                        StrCONDITION += "'IM',"
                    End If
                    If chkCreditNote.Checked = True Then
                        StrCONDITION += "'CR',"
                    End If
                    If chkDebitNote.Checked = True Then
                        StrCONDITION += "'DR',"
                    End If
                    If ChkInvoice.Checked = True Then
                        StrCONDITION += "'IN',"
                    End If
                    If chkOnAccount.Checked = True Then
                        StrCONDITION += "'OA',"
                    End If
                    If chkAdvance.Checked = True Then
                        StrCONDITION += "'PR',"
                    End If
                    If chkreceipt.Checked = True Then
                        StrCONDITION += "'RC',"
                    End If
                    If chkrefund.Checked = True Then
                        StrCONDITION += "'RF',"
                    End If
                    If chkBankReverse.Checked = True Then
                        StrCONDITION += "'RV-TA',"
                    End If
                    If ChkUnapplied.Checked = True Then
                        StrCONDITION += "'UA',"
                    End If

                    If clsCommon.myLen(StrCONDITION) > 0 Then
                        StrCONDITION += "'EXC'"

                        BaseQry += " AND InnQuery.DocType in (" & StrCONDITION & ") "
                        BaseQryOPENINGINCASEOFMIS += " AND InnQuery.DocType in (" & StrCONDITION & ") "
                    Else
                        BaseQry += " AND InnQuery.DocType in ('') "
                        BaseQryOPENINGINCASEOFMIS += " AND InnQuery.DocType in ('') "
                    End If
                End If
                ''-----------------


                Dim strFIlterCheck As String = ""
                Dim StrDocWiseFilter As String = String.Empty
                If ChkISParentCust.Checked = True Then
                Else
                    If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                        strFIlterCheck += "and ACode in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                        If ChkDocWise.Checked = True Then
                            StrDocWiseFilter += " AND CASE WHEN ISNULL([Child Cust Code],'')<>'' THEN [Child Cust Code] ELSE CustCode END IN (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
                        End If
                    Else
                        strFIlterCheck += " and ACode in ( " + strCusMappingByLocForCurUser + " )  "
                    End If
                End If

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    If chkItemWise.Checked Then
                        strFIlterCheck += "and Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
                    Else
                        strFIlterCheck += "and Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
                    End If
                Else
                    If chkItemWise.Checked Then
                        strFIlterCheck += "and Loc_Code in (" + StrPermission + ")  "
                    Else
                        strFIlterCheck += "and Location in (" + StrPermission + ")  "
                    End If
                End If


                'If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                '    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")  "
                'End If

                Dim dtCustGroup As DataTable = clsDBFuncationality.GetDataTable("select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "'")

                If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 And txtCustomerGroup.arrValueMember Is Nothing Then
                    strFIlterCheck += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code in (select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "')"
                ElseIf txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")  "
                End If

                If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 And txtCustomer.arrValueMember Is Nothing Then
                    If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
                        strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Code in ( " + strCusMappingByLocForCurUser + " "
                    Else
                        strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Code in (select DISTINCT Cust_Code from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL WHERE User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "' "
                    End If

                    If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                        strFIlterCheck += " and TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ") ) "
                    Else
                        strFIlterCheck += " ) "
                    End If
                End If
                '' added by richa agarwal ERO/22/05/18-000323
                If TxtZone.arrValueMember IsNot Nothing AndAlso TxtZone.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(TxtZone.arrValueMember) + ")  "
                End If
                ''--------------

                If txtCustomerType.arrValueMember IsNot Nothing AndAlso txtCustomerType.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Type_Code in (" + clsCommon.GetMulcallString(txtCustomerType.arrValueMember) + ")  "
                End If
                If txtCustomerCategory.arrValueMember IsNot Nothing AndAlso txtCustomerCategory.arrValueMember.Count > 0 Then
                    strFIlterCheck += " and TSPL_CUSTOMER_MASTER.Cust_Category_Code in (" + clsCommon.GetMulcallString(txtCustomerCategory.arrValueMember) + ")  "
                End If

                If chkExcludeOpening.Checked = True Then
                    BaseQryOPENINGINCASEOFMIS = " Select '' as ACode ,'' as AName,'' as DocNo,'' as AgainstInvoiceNo,null as DocDate ,'' as DocType ,'' as DocNarr ,'' as ChequeDetails ,'' as Currency_Code ,1 as ConvRate, 0 as DrAmt,0 as CrAmt,0 as Sales,0 as CollectionRefund , 0 as SecurityDrAmt ,0 as SecurityCrAmt ,0 as DrNote,0 as CrNote,'' as Location,'' as SourceCode ,'' as Item_Code,'' as Item_Desc,'' as Receipt_Type,'' as Bank_Code,'' as Cust_Type_Code ,'' as Cust_Type_Desc,'' as Cust_Category_Code ,'' as CUST_CATEGORY_DESC,0 as EXCHANGE_GAIN_AMT ,0 as EXCHANGE_LOSS_AMT  where 1=2"
                End If

                '---------------Customer Group Wise Data----------- BM00000008411
                strQry = "Select 'CustomerGroup' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, Cust_Group_Code, MAX(Cust_Group_Desc) as Cust_Group_Desc, '' as ACode, '' as AName, SUM(OpngBal) as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(Sales) as [Sales], SUM(CollectionRefund) as CollectionRefund, SUM(DrNote) as DrNote,-1* SUM(CrNote) as CrNote, "

                If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                    strQry += "( SUM(OpngBal) + SUM(DrAmt) + SUM(Sales) ) -(SUM(CrAmt)+SUM(CollectionRefund))  as BalAmt, "
                Else
                    strQry += "( SUM(OpngBal) + SUM(DrAmt) ) -SUM(CrAmt)  as BalAmt, "
                End If

                strQry += " MAX(xxx.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(xxx.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc  From (" + Environment.NewLine & _
                " Select TSPL_CUSTOMER_MASTER.Cust_Group_Code, MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc, '' as CurrencyCode, null as ConvRate, SUM(DrAmt*" & ddlCurrencyType.SelectedValue & ")-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote, 0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from ( " + BaseQryOPENINGINCASEOFMIS + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine & _
                Environment.NewLine + " UNION ALL-----------------------------------BADA UNION--------------------" + Environment.NewLine & _
                " Select TSPL_CUSTOMER_MASTER.Cust_Group_Code, MAX(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc, MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate, 0 as OpngBal, SUM(DrAmt*" & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as DrAmt, " + Environment.NewLine & _
                " sum(CrAmt) CrAmt, SUM(Sales* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as [Sales], SUM(CollectionRefund* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as CollectionRefund, SUM(DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as DrNote, SUM(CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc FROM ( " + BaseQry + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine & _
                " Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine & _
                " where CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' AND CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY TSPL_CUSTOMER_MASTER.Cust_Group_Code" + Environment.NewLine & _
                " ) XXX GROUP BY Cust_Group_Code "
                If rbtnCustGroupWiseDrCr.Checked Then
                    strQry = " select strType,RunDate,CompanyAddress ,CompanyName, FilterFromDate,FilterToDate, Cust_Group_Code, Cust_Group_Desc, ACode, AName,case when OpngBal>0 then  OpngBal else 0 end as OpngBalDR,case when OpngBal>0 then  0 else -1*OpngBal end as OpngBalCR,DrAmt,CrAmt,case when BalAmt>0 then BalAmt else 0 end BalAmtDR,case when BalAmt>0 then 0 else -1*BalAmt end BalAmtCR,  Sales , CollectionRefund,DrNote,CrNote, Cust_Category_Code,Cust_Category_Desc,Cust_Type_Code,Cust_Type_Desc from (" + strQry + ")XXXX "
                End If
                strQry += " ORDER BY Cust_Group_Code "
                dtCustGrp = clsDBFuncationality.GetDataTable(strQry)

                '---------------Customer Wise Data----------------- BM00000008411

                'strQry = "Select 'Customer' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, MAX(Cust_Group_Code) AS Cust_Group_Code, '' as Cust_Group_Desc, ACode, MAX(AName) as AName, SUM(OpngBal) as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(Sales) as [Sales], SUM(CollectionRefund) as CollectionRefund, SUM(DrNote) as DrNote, -1* SUM(CrNote) as CrNote, "

                'If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                '    strQry += "( SUM(OpngBal) + SUM(DrAmt) + SUM(Sales) ) -(SUM(CrAmt)+SUM(CollectionRefund))  as BalAmt, "
                'Else
                '    strQry += "( SUM(OpngBal) + SUM(DrAmt) ) -SUM(CrAmt)  as BalAmt, "
                'End If

                'strQry += " MAX(Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(xxx.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc From (" + Environment.NewLine & _
                '" Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as CurrencyCode, null as ConvRate, SUM(DrAmt*" & ddlCurrencyType.SelectedValue & ")-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote, 0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from ( " + BaseQryOPENINGINCASEOFMIS + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY ACode" + Environment.NewLine & _
                'Environment.NewLine + " UNION ALL" + Environment.NewLine & _
                '" Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate, 0 as OpngBal, SUM(DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as DrAmt, " + Environment.NewLine & _
                '" SUM(CrAmt) as CrAmt, SUM(Sales* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as [Sales], SUM(CollectionRefund* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as CollectionRefund, SUM(DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as DrNote, SUM(CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc FROM ( " + BaseQry + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine & _
                '" Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine & _
                '"where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' AND CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY ACode" + Environment.NewLine & _
                '" ) XXX GROUP BY ACode "
                'If rbtnCustWiseDrCr.Checked Then
                '    strQry = " select strType,RunDate,CompanyAddress ,CompanyName, FilterFromDate,FilterToDate, Cust_Group_Code, Cust_Group_Desc, ACode, AName,case when OpngBal>0 then  OpngBal else 0 end as OpngBalDR,case when OpngBal>0 then  0 else -1*OpngBal end as OpngBalCR,DrAmt,CrAmt,case when BalAmt>0 then BalAmt else 0 end BalAmtDR,case when BalAmt>0 then 0 else -1*BalAmt end BalAmtCR,  Sales , CollectionRefund,DrNote,CrNote, Cust_Category_Code,Cust_Category_Desc,Cust_Type_Code,Cust_Type_Desc from (" + strQry + ")XXXX "
                'End If
                'strQry += " ORDER BY ACode"
                'dtCustomer = clsDBFuncationality.GetDataTable(strQry)

                strQry = "Select 'Customer' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate, MAX(Cust_Group_Code) AS Cust_Group_Code, '' as Cust_Group_Desc, ACode,"
                ''BHA/25/09/18-000570 richa 
                If chkDateWise.Checked Then
                    strQry += " DocDate, "
                End If
                strQry += " MAX(AName) as AName, SUM(OpngBal) as OpngBal, SUM(DrAmt) as DrAmt, SUM(CrAmt) as CrAmt, SUM(Sales) as [Sales], SUM(CollectionRefund) as CollectionRefund, SUM(DrNote) as DrNote, -1* SUM(CrNote) as CrNote, "

                If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                    strQry += "( SUM(OpngBal) + SUM(DrAmt) + SUM(Sales) ) -(SUM(CrAmt)+SUM(CollectionRefund))  as BalAmt, "
                Else
                    strQry += "( SUM(OpngBal) + SUM(DrAmt) ) -SUM(CrAmt)  as BalAmt, "
                End If

                strQry += " MAX(Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(xxx.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc From (" + Environment.NewLine & _
                " Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, "
                If chkDateWise.Checked Then
                    strQry += " max(convert(date,final.DocDate ,103)) DocDate,"
                End If
                strQry += " '' as CurrencyCode, null as ConvRate, SUM(DrAmt*" & ddlCurrencyType.SelectedValue & ")-SUM(CrAmt) as OpngBal, 0 as DrAmt, 0 as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote, 0 as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc from ( " + BaseQryOPENINGINCASEOFMIS + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY ACode" + Environment.NewLine & _
                               Environment.NewLine + " UNION ALL" + Environment.NewLine & _
                               " Select MAX(TSPL_CUSTOMER_MASTER.Cust_Group_Code) as Cust_Group_Code, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, "
                If chkDateWise.Checked Then
                    strQry += " max(convert(date,final.DocDate ,103)) DocDate,"
                End If
                strQry += " MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate, 0 as OpngBal, SUM(DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as DrAmt, " + Environment.NewLine & _
                                               " SUM(CrAmt) as CrAmt, SUM(Sales* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as [Sales], SUM(CollectionRefund* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as CollectionRefund, SUM(DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as DrNote, SUM(CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as CrNote, MAX(tspl_customer_master.Cust_Category_Code) as Cust_Category_Code,MAX(CUST_CATEGORY_DESC) as Cust_Category_Desc,MAX(tspl_customer_master.Cust_Type_Code) As Cust_Type_Code,MAX(Cust_Type_Desc) As Cust_Type_Desc FROM ( " + BaseQry + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine & _
                                               " Left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo  LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine & _
                                               "where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' AND CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForLevels + " " + CheckCustomer + " GROUP BY ACode" + Environment.NewLine
                If chkDateWise.Checked Then
                    strQry += " ,convert(date,final.DocDate ,103)"
                End If
                strQry += " ) XXX GROUP BY ACode "
                If chkDateWise.Checked Then
                    strQry += " ,convert(date,DocDate ,103)"
                End If
                If rbtnCustWiseDrCr.Checked Then
                    strQry = " select strType,RunDate,CompanyAddress ,CompanyName, FilterFromDate,FilterToDate, Cust_Group_Code, Cust_Group_Desc, ACode, AName,case when OpngBal>0 then  OpngBal else 0 end as OpngBalDR,case when OpngBal>0 then  0 else -1*OpngBal end as OpngBalCR,DrAmt,CrAmt,case when BalAmt>0 then BalAmt else 0 end BalAmtDR,case when BalAmt>0 then 0 else -1*BalAmt end BalAmtCR,  Sales , CollectionRefund,DrNote,CrNote, Cust_Category_Code,Cust_Category_Desc,Cust_Type_Code,Cust_Type_Desc from (" + strQry + ")XXXX "
                End If
                strQry += " ORDER BY ACode"
                dtCustomer = clsDBFuncationality.GetDataTable(strQry)


                '---------------Opening Data-----------------------
                strQry = "With CTETemp as (" + Environment.NewLine & _
                " Select ROW_NUMBER() OVER (PARTITION BY ACode ORDER BY ACode, OrderDate, OrderDocType) as RowNo, 'Detail' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,Parent_Master.Customer_Name as ParentName, XXX.* from (" + Environment.NewLine & _
                " Select MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No) as ParentCode, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, DocNo, MAX(AgainstInvoiceNo) as AgainstInvoiceNo, MAX(DocDate) as DocDate, MAX(DocType) as DocType,isnull(MAX(DocNarr),'') as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Location) as Location, convert(date,MAX(final.DocDate),103) as OrderDate, (Select MAX( TSPL_LOCATION_MASTER.Location_Desc) from  TSPL_LOCATION_MASTER where Location_Code=MAX(Location)) as LocDesc, '' as Item_Code, '' as Item_Desc, MAX(Final.Currency_Code) as Currency_Code, MAX(ConvRate) as ConvRate, SUM(DrAmt*" & ddlCurrencyType.SelectedValue & ") as DrAmt, SUM(CrAmt*" & ddlCurrencyType.SelectedValue & ") as CrAmt, SUM(Sales*" & ddlCurrencyType.SelectedValue & ") as [Sales], SUM(CollectionRefund*" & ddlCurrencyType.SelectedValue & ") as CollectionRefund, SUM(DrNote*" & ddlCurrencyType.SelectedValue & ") as DrNote, SUM(CrNote*" & ddlCurrencyType.SelectedValue & ") as CrNote, SUM(dramt*" & ddlCurrencyType.SelectedValue & ")-SUM(cramt*" & ddlCurrencyType.SelectedValue & ") as BalAmt, MAX(SourceCode) as SourceCode, CASE  WHEN MAX(DocType) = 'IN' THEN 1  WHEN MAX(DocType) = 'RC' THEN 2 WHEN MAX(DocType) = 'SR' THEN 3 WHEN MAX(DocType) = 'AD' THEN 4 ELSE 5 END  as OrderDocType," + Environment.NewLine & _
                " Case When Max(TSPL_BANK_MASTER.Bank_type)='B' Then (Case When MAX(tspl_BankReco_Head.Post)='Y' AND MAX(tspl_BankReco_Detail.Reconciliation_Status)='C' Then CONVERT(VARCHAR,MAX(tspl_BankReco_Detail.Reconciliation_Date),103) Else 'Pending' End) Else '' End as [Reconciliation_Date]" + Environment.NewLine & _
                " ,MAX(Receipt_Type) AS Receipt_Type, MAX(TSPL_CUSTOMER_MASTER.Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(TSPL_CUSTOMER_MASTER.Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC,'' AS Voucher_No , '' as JEAccount_Code, 0 as JEAMT " + Environment.NewLine & _
                " from ( " + strtempBaseQryforopening + " ) Final LEFT OUTER JOIN tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine & _
                " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine & _
                " where  CONVERT(DATE,final.DocDate,103) < '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForDetail + " " + CheckCustomer + "" + Environment.NewLine & _
                " GROUP By ACode, DocNo,Location" + Environment.NewLine & _
                " ) XXX left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=XXX.ParentCode" + Environment.NewLine & _
                " ) Select CTETemp.RowNo, CTETemp.strType, CTETemp.RunDate, CTETemp.CompanyAddress, CTETemp.CompanyName, CTETemp.FilterFromDate, CTETemp.FilterToDate, CTETemp.ParentCode, CTETemp.ParentName, CTETemp.ACode, CTETemp.AName, CTETemp.DocNo, CTETemp.AgainstInvoiceNo, convert(varchar,CTETemp.DocDate,103) as DocDate, CTETemp.DocType, CTETemp.DocNarr, CTETemp.ChequeDetails, CTETemp.Location, CTETemp.OrderDate, CTETemp.LocDesc, " & IIf(ddlCurrencyType.SelectedValue = "1", "CTETemp.Currency_Code", "'INR'") & " as Currency_Code, CTETemp.DrAmt, CTETemp.CrAmt, CTETemp.Sales, CTETemp.CollectionRefund, CTETemp.DrNote, CTETemp.CrNote, CTETemp.BalAmt, SUM(BalAmt) OVER (PARTITION BY ACode ORDER BY ACode, RowNo) as [Closing], CTETemp.SourceCode, CTETemp.OrderDocType, CTETemp.Reconciliation_Date " + Environment.NewLine & _
                " ,CTETemp.Receipt_Type, CTETemp.Cust_Type_Code, CTETemp.Cust_Type_Desc, CTETemp.Cust_Category_Code, CTETemp.CUST_CATEGORY_DESC ,CTETemp.Voucher_No As [JE No],CTETemp.JEAccount_Code  AS [JE Account] ,CTETemp.JEAMT AS [JE Amount]  ,CTETemp.BalAmt - CTETemp.JEAMT AS DiffAmt   " + Environment.NewLine & _
                " from CTETemp ORDER BY  CTETemp.OrderDate, CTETemp.ACode, CTETemp.OrderDocType"
                dtOpening = clsDBFuncationality.GetDataTable(strQry)

                '------------------Detail Level Data------------------- 

                If chkItemWise.Checked Then
                    strQry = "WITH CTETemp as ("
                    strQry += "Select ROW_NUMBER() OVER (PARTITION BY ACode ORDER BY ACode, OrderDate, OrderDocType) as RowNo, 'Detail' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,Parent_Master.Customer_Name as ParentName, * from ("
                    strQry += " Select MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No) as ParentCode, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as DocNo,'' as AgainstInvoiceNo, NULL as DocDate, '' as DocType, 'Opening Balance' as DocNarr, '' as ChequeDetails, '' as Location, NULL as OrderDate, '' as LocDesc, '' as Item_Code, '' as Item_Desc, Case WHEN SUM(DrAmt)>=SUM(CrAmt) Then SUM(DrAmt)-SUM(CrAmt) Else 0 End as DrAmt, Case WHEN SUM(DrAmt)< SUM(CrAmt) Then SUM(CrAmt)-SUM(DrAmt) Else 0 End as CrAmt, SUM(Sales) as [Sales], SUM(CollectionRefund) as CollectionRefund, SUM(DrNote) as DrNote, SUM(CrNote) as CrNote, SUM(DrAmt)-SUM(CrAmt) as BalAmt, '' as SourceCode, 0 as OrderDocType, '' as [Reconciliation_Date] "
                    strQry += ",MAX(Receipt_Type) AS Receipt_Type "
                    strQry += " from ( " + BaseQry + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code "
                    strQry += " where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' AND TSPL_CUSTOMER_MASTER.Status='N'  and LEN(ACode)>0 " + strFIlterCheck + " " + FilterForDetail + " " + CheckCustomer + " "
                    strQry += "  GROUP BY ACode"
                    strQry += " UNION ALL"
                    strQry += " Select TSPL_CUSTOMER_MASTER.Parent_Customer_No as ParentCode, ACode, TSPL_CUSTOMER_MASTER.Customer_Name as AName, DocNo,AgainstInvoiceNo,DocDate,DocType,isnull(  tspl_BankReco_Head.Description,'') as DocNarr,ChequeDetails, Loc_Code,convert(date,final.DocDate,103) as OrderDate, (Select MAX( TSPL_LOCATION_MASTER.Location_Desc) from  TSPL_LOCATION_MASTER where Loc_Segment_Code=Location_Code) as LocDesc, Item_Code, Item_Desc, DrAmt, CrAmt, [Sales], CollectionRefund, DrNote, CrNote, dramt-cramt as BalAmt,SourceCode, CASE  WHEN DocType  = 'IN' THEN 1  WHEN DocType = 'RC' THEN 2 WHEN DocType = 'SR' THEN 3 WHEN DocType = 'AD' THEN 4 ELSE 5 END  as OrderDocType, Case When tspl_BankReco_Head.Post='Y' AND tspl_BankReco_Detail.Reconciliation_Status='C' Then CONVERT(VARCHAR,tspl_BankReco_Detail.Reconciliation_Date,103) Else 'Pending' End as [Reconciliation_Date] "
                    strQry += ",Receipt_Type AS Receipt_Type "
                    strQry += " from ( " + BaseQry + " ) Final LEFT OUTER JOIN tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' and   CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForDetail + " " + CheckCustomer + ""
                    strQry += " ) XXX   left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=XXX.ParentCode"
                    strQry += ") Select CTETemp.RowNo, CTETemp.strType, CTETemp.RunDate, CTETemp.CompanyAddress, CTETemp.CompanyName, CTETemp.FilterFromDate, CTETemp.FilterToDate,CTETemp.ParentCode,CTETemp.ParentName ,CTETemp.ACode, CTETemp.AName, CTETemp.DocNo, CTETemp.AgainstInvoiceNo, convert(varchar,CTETemp.DocDate) as DocDate, CTETemp.DocType, CTETemp.DocNarr, CTETemp.ChequeDetails, CTETemp.Location, CTETemp.OrderDate, CTETemp.LocDesc, Item_Code, Item_Desc, CTETemp.DrAmt, CTETemp.CrAmt, CTETemp.BalAmt, SUM(BalAmt) OVER (PARTITION BY ACode ORDER BY ACode, RowNo) as [Closing], CTETemp.SourceCode, CTETemp.OrderDocType, CTETemp.Reconciliation_Date "
                    strQry += ",CTETemp.Receipt_Type "
                    strQry += " from CTETemp ORDER BY CTETemp.OrderDate,CTETemp.ACode,  CTETemp.OrderDocType"
                Else
                    If rbtnDocWise.Checked = True Then '' 10-Aug-2015  BM00000008356,BM00000008357 
                        strQry = "WITH CTETemp as (" + Environment.NewLine & _
                        " Select ROW_NUMBER() OVER (PARTITION BY ACode ORDER BY ACode, OrderDate, OrderDocType) as RowNo, 'Detail' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,Parent_Master.Customer_Name as ParentName, XXX.* from (" + Environment.NewLine & _
                        " Select MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No) as ParentCode, max(ACode) as ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, DocNo, MAX(AgainstInvoiceNo) as AgainstInvoiceNo, MAX(DocDate) as DocDate, MAX(DocType) as DocType,isnull(MAX(DocNarr),'') as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Location) as Location, convert(date,MAX(final.DocDate),103) as OrderDate," + Environment.NewLine & _
                       "MAX( TSPL_LOCATION_MASTER.Location_Desc) as LocDesc,'' as Item_Code, '' as Item_Desc, MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate,  " + Environment.NewLine & _
                        " CASE WHEN max(DocType) ='RV-TA' THEN SUM(DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")+ (Select isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) from TSPL_RECEIPT_HEADER where Receipt_No =(Select Document_No  from TSPL_BANK_REVERSE where Reverse_Code =DocNo)) - (Select isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT),0) from TSPL_RECEIPT_HEADER where Receipt_No =(Select Document_No  from TSPL_BANK_REVERSE where Reverse_Code =DocNo)) ELSE SUM(DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")  END AS DrAmt, " + Environment.NewLine & _
                        " Sum(CrAmt) as CrAmt, SUM(Sales* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as [Sales], SUM(CollectionRefund* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as CollectionRefund, SUM(DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as DrNote, SUM(CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as CrNote, " + Environment.NewLine & _
                        " CASE WHEN max(DocType) ='RV-TA' THEN SUM(DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")+ (Select isnull((TSPL_RECEIPT_HEADER.EXCHANGE_LOSS_AMT),0) from TSPL_RECEIPT_HEADER where Receipt_No =(Select Document_No  from TSPL_BANK_REVERSE where Reverse_Code =DocNo)) - (Select isnull((TSPL_RECEIPT_HEADER.EXCHANGE_GAIN_AMT),0) from TSPL_RECEIPT_HEADER where Receipt_No =(Select Document_No  from TSPL_BANK_REVERSE where Reverse_Code =DocNo)) ELSE SUM(DrAmt*  " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")  END -Sum(CrAmt)  as BalAmt,  MAX(SourceCode) as SourceCode, CASE  WHEN MAX(DocType) = 'IN' THEN 1  WHEN MAX(DocType) = 'RC' THEN 2 WHEN MAX(DocType) = 'SR' THEN 3 WHEN MAX(DocType) = 'AD' THEN 4 ELSE 5 END  as OrderDocType," + Environment.NewLine & _
                        " Case When Max(TSPL_BANK_MASTER.Bank_type)='B' Then (Case When MAX(tspl_BankReco_Head.Post)='Y' AND MAX(tspl_BankReco_Detail.Reconciliation_Status)='C' Then CONVERT(VARCHAR,MAX(tspl_BankReco_Detail.Reconciliation_Date),103) Else 'Pending' End) Else '' End as [Reconciliation_Date]" + Environment.NewLine & _
                        " ,MAX(Final.Receipt_Type) AS Receipt_Type, MAX(TSPL_CUSTOMER_MASTER.Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(TSPL_CUSTOMER_MASTER.Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC " + Environment.NewLine & _
                        " ,MAX(TSPL_JOURNAL_MASTER.Voucher_No) AS Voucher_No   " + Environment.NewLine & _
                        " ,MAX(TSPL_CUSTOMER_MASTER.Cust_Account) AS Cust_Account" + Environment.NewLine & _
                        " ,( SELECT MAX(TSPL_JOURNAL_DETAILS.Account_code) AS Account_code  FROM TSPL_JOURNAL_DETAILS " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account  = MAX(TSPL_CUSTOMER_MASTER.Cust_Account)" + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') = ISNULL(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct  ,'')  " + Environment.NewLine & _
                        " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=MAX(TSPL_JOURNAL_MASTER.Voucher_No )) AS JEAccount_code" + Environment.NewLine & _
                        " ,( SELECT SUM( TSPL_JOURNAL_DETAILS.Amount) AS Amount  FROM TSPL_JOURNAL_DETAILS " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account  =MAX(TSPL_CUSTOMER_MASTER.Cust_Account)    " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') = ISNULL(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct  ,'')  " + Environment.NewLine & _
                        " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=MAX(TSPL_JOURNAL_MASTER.Voucher_No )	) AS JEAmount " + Environment.NewLine & _
                        " from ( " + BaseQry + " ) Final LEFT OUTER JOIN tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No AND tspl_BankReco_Detail.Reconciliation_Status='C' LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=Final.DocNo " + Environment.NewLine & _
                        " Left Outer Join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =Final.DocNo " + Environment.NewLine & _
                        "  LEFT OUTER JOIN   TSPL_LOCATION_MASTER on  TSPL_LOCATION_MASTER.Location_Code=Final.Location " + Environment.NewLine & _
                        " where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' and   CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForDetail + " " + CheckCustomer + " " + Environment.NewLine & _
                        " GROUP By  DocNo" + Environment.NewLine & _
                        " ) XXX   left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=XXX.ParentCode" + Environment.NewLine & _
                        ") Select CTETemp.RowNo, CTETemp.strType, CTETemp.RunDate, CTETemp.CompanyAddress, CTETemp.CompanyName, CTETemp.FilterFromDate, CTETemp.FilterToDate, CTETemp.ParentCode, CTETemp.ParentName, CTETemp.ACode, CTETemp.AName, CTETemp.DocNo, CTETemp.AgainstInvoiceNo, convert(varchar,CTETemp.DocDate,103) as DocDate, CTETemp.DocType, CTETemp.DocNarr, CTETemp.ChequeDetails, CTETemp.Location, CTETemp.OrderDate, CTETemp.LocDesc, " & IIf(ddlCurrencyType.SelectedValue = "1", "CTETemp.Currency_Code", "'INR'") & " as Currency_Code, CTETemp.DrAmt, CTETemp.CrAmt, CTETemp.Sales, CTETemp.CollectionRefund, CTETemp.DrNote, CTETemp.CrNote, CTETemp.BalAmt, SUM(BalAmt) OVER (PARTITION BY ACode ORDER BY ACode, RowNo) as [Closing], CTETemp.SourceCode, CTETemp.OrderDocType, CTETemp.Reconciliation_Date " + Environment.NewLine & _
                        ",CTETemp.Receipt_Type,CTETemp.Cust_Type_Code ,CTETemp.Cust_Type_Desc ,CTETemp.Cust_Category_Code ,CTETemp.CUST_CATEGORY_DESC ,CTETemp.Voucher_No As [JE No],CTETemp.JEAccount_Code  AS [JE Account],CTETemp.JEAmount  AS [JE Amount],CTETemp.BalAmt - CTETemp.JEAmount AS DiffAmt   " + Environment.NewLine & _
                        " from CTETemp ORDER BY CTETemp.OrderDate,CTETemp.ACode,  CTETemp.OrderDocType"
                    Else
                        strQry = "WITH CTETemp as (" + Environment.NewLine & _
                        " Select ROW_NUMBER() OVER (PARTITION BY ACode ORDER BY ACode, DocDate) as RowNo, 'Detail' as strType, '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,Parent_Master.Customer_Name as ParentName, " + Environment.NewLine & _
                        " XXX.* from (" + Environment.NewLine & _
                         " Select MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No) as ParentCode, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, '' as DocNo,'' as AgainstInvoiceNo, NULL as DocDate, 'OP' as DocType, 'Opening Balance' as DocNarr, '' as ChequeDetails, '' as Location, NULL as OrderDate, '' as LocDesc, '' as Item_Code, '' as Item_Desc, '' as Currency_Code, NULL as ConvRate, Case WHEN SUM(DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")>=SUM(CrAmt) Then SUM(DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")-SUM(CrAmt) Else 0 End as DrAmt, Case WHEN SUM(DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")< SUM(CrAmt) Then SUM(CrAmt)-SUM(DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") Else 0 End as CrAmt, 0 as [Sales], 0 as CollectionRefund, 0 as DrNote, 0 as CrNote, SUM(DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ")-SUM(CrAmt) as BalAmt, '' as SourceCode, 0 as OrderDocType, '' as [Reconciliation_Date]" + Environment.NewLine & _
                        " ,MAX(Receipt_Type) AS Receipt_Type ,MAX(TSPL_CUSTOMER_MASTER.Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(TSPL_CUSTOMER_MASTER.Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC,MAX(TSPL_JOURNAL_MASTER.Voucher_No) AS Voucher_No ,MAX(TSPL_CUSTOMER_MASTER.Cust_Account ) AS Cust_Account 	,'' AS JEAccount_Code ,0 AS JEAmount" + Environment.NewLine & _
                        " from ( " + BaseQryOPENINGINCASEOFMIS + " ) Final left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No = final.docno " + Environment.NewLine & _
                        " where  CONVERT(DATE,final.DocDate,103) < '" + strFromDate + "' and LEN(ACode)>0 " + strFIlterCheck + " " + FilterForDetail + " " + CheckCustomer + "" + Environment.NewLine & _
                        " GROUP BY ACode" + Environment.NewLine & _
                        Environment.NewLine + " UNION ALL----------------------------------------------Bada UNION---------------" + Environment.NewLine & _
                        " Select MAX(TSPL_CUSTOMER_MASTER.Parent_Customer_No) as ParentCode, ACode, MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as AName, DocNo, MAX(AgainstInvoiceNo) as AgainstInvoiceNo, MAX(DocDate) as DocDate, MAX(DocType) as V,isnull(MAX(DocNarr),'') as DocNarr, MAX(ChequeDetails) as ChequeDetails, MAX(Location) as Location, convert(date,MAX(final.DocDate),103) as OrderDate," + Environment.NewLine & _
                        "MAX( TSPL_LOCATION_MASTER.Location_Desc)  as LocDesc, '' as Item_Code, '' as Item_Desc, MAX(Final.Currency_Code) as Currency_Code, MAX(Final.ConvRate) as ConvRate, SUM(DrAmt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as DrAmt," + Environment.NewLine & _
                        " SUM(CrAmt) as CrAmt, SUM(Sales* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as [Sales], SUM(CollectionRefund* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as CollectionRefund, SUM(DrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as DrNote, SUM(CrNote* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") as CrNote, " + Environment.NewLine & _
                        "SUM(dramt* " & IIf(clsCommon.myCstr(ddlCurrencyType.SelectedValue) = "1", 1, " Final." & ddlCurrencyType.SelectedValue & "") & ") - SUM(CrAmt)  as BalAmt, " + Environment.NewLine & _
                        " MAX(SourceCode) as SourceCode, CASE  WHEN MAX(DocType) = 'IN' THEN 1  WHEN MAX(DocType) = 'RC' THEN 2 WHEN MAX(DocType) = 'SR' THEN 3 WHEN MAX(DocType) = 'AD' THEN 4 ELSE 5 END  as OrderDocType," + Environment.NewLine & _
                        " Case When Max(TSPL_BANK_MASTER.Bank_type)='B' Then (Case When MAX(tspl_BankReco_Head.Post)='Y' AND MAX(tspl_BankReco_Detail.Reconciliation_Status)='C' Then CONVERT(VARCHAR,MAX(tspl_BankReco_Detail.Reconciliation_Date),103) Else 'Pending' End) Else '' End as [Reconciliation_Date]" + Environment.NewLine & _
                        ",MAX(Final.Receipt_Type) AS Receipt_Type, MAX(TSPL_CUSTOMER_MASTER.Cust_Type_Code) AS Cust_Type_Code,MAX(Cust_Type_Desc) AS Cust_Type_Desc,MAX(TSPL_CUSTOMER_MASTER.Cust_Category_Code) AS Cust_Category_Code,MAX(CUST_CATEGORY_DESC) AS CUST_CATEGORY_DESC  ,MAX(TSPL_JOURNAL_MASTER.Voucher_No) AS Voucher_No   " + Environment.NewLine & _
                        " ,MAX(TSPL_CUSTOMER_MASTER.Cust_Account) AS Cust_Account" + Environment.NewLine & _
                        " ,( SELECT MAX(TSPL_JOURNAL_DETAILS.Account_code) AS Account_code  FROM TSPL_JOURNAL_DETAILS " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account  = MAX(TSPL_CUSTOMER_MASTER.Cust_Account)" + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') = ISNULL(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct  ,'')  " + Environment.NewLine & _
                        " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=MAX(TSPL_JOURNAL_MASTER.Voucher_No )) AS JEAccount_code" + Environment.NewLine & _
                        " ,( SELECT SUM( TSPL_JOURNAL_DETAILS.Amount) AS Amount  FROM TSPL_JOURNAL_DETAILS " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account  =MAX(TSPL_CUSTOMER_MASTER.Cust_Account)    " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL1 ON ISNULL(GL1.Account_Code,'') = ISNULL(TSPL_JOURNAL_DETAILS.Account_code ,'')  " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') = ISNULL(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct  ,'')  " + Environment.NewLine & _
                        " WHERE GL1.Account_Seg_Code1 = GL2.Account_Seg_Code1 AND TSPL_JOURNAL_DETAILS.Voucher_No=MAX(TSPL_JOURNAL_MASTER.Voucher_No )	) AS JEAmount " + Environment.NewLine & _
                        " from ( " + BaseQry + " ) Final LEFT OUTER JOIN tspl_BankReco_Detail on final.DocNo=tspl_BankReco_Detail.Document_No AND tspl_BankReco_Detail.Reconciliation_Status='C' LEFT OUTER JOIN tspl_BankReco_Head on tspl_BankReco_Detail.Reconciliation_Id =tspl_BankReco_Head.Reconciliation_Id left outer join TSPL_CUSTOMER_MASTER on final.ACode=TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN TSPL_CUSTOMER_GROUP_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=Final.Bank_Code " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Source_Doc_No=Final.DocNo " + Environment.NewLine & _
                        " left outer join TSPL_LOCATION_MASTER on  TSPL_LOCATION_MASTER.Location_Code=final.Location" + Environment.NewLine & _
                        " left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No =final.DocNo " + Environment.NewLine & _
                        " LEFT OUTER JOIN TSPL_GL_ACCOUNTS GL2 ON ISNULL(GL2.Account_Code,'') = ISNULL(TSPL_CUSTOMER_ACCOUNT_SET.Receivable_Control_acct  ,'')" + Environment.NewLine & _
                        " where  CONVERT(DATE,final.DocDate,103) >= '" + strFromDate + "' and   CONVERT(DATE,final.DocDate,103) <= '" + strToDate + "' AND LEN(ACode)>0 " + strFIlterCheck + " " + FilterForDetail + " " + CheckCustomer + " " + Environment.NewLine & _
                        " GROUP By ACode, DocNo,Location,DocType" + Environment.NewLine & _
                        " ) XXX   left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=XXX.ParentCode)" + Environment.NewLine & _
                        " Select CTETemp.RowNo, CTETemp.strType, CTETemp.RunDate, CTETemp.CompanyAddress, CTETemp.CompanyName, CTETemp.FilterFromDate, CTETemp.FilterToDate, CTETemp.ParentCode, CTETemp.ParentName, CTETemp.ACode, CTETemp.AName, CTETemp.DocNo, CTETemp.AgainstInvoiceNo, convert(varchar,CTETemp.DocDate,103) as DocDate, CTETemp.DocType, CTETemp.DocNarr, CTETemp.ChequeDetails, CTETemp.Location, CTETemp.OrderDate, CTETemp.LocDesc, " & IIf(ddlCurrencyType.SelectedValue = "1", "CTETemp.Currency_Code", "'INR'") & " as Currency_Code,CTETemp.ConvRate , CTETemp.DrAmt, CTETemp.CrAmt, CTETemp.Sales,CTETemp.CollectionRefund ,  CTETemp.DrNote,-1 * (case when CTETemp.DocType='IM' then  0 else   CTETemp.CrNote end)  as CrNote, " + Environment.NewLine

                        If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                            strQry += " SUM(DrAmt-CrAmt+Sales-CollectionRefund) Over (Partition by Acode ORDER BY RowNo) as [Closing], " + Environment.NewLine
                        Else
                            strQry += " SUM(DrAmt-CrAmt) Over (Partition by Acode ORDER BY RowNo) as [Closing], " + Environment.NewLine
                        End If

                        strQry += "CTETemp.SourceCode, CTETemp.OrderDocType, CTETemp.Reconciliation_Date " + Environment.NewLine & _
                        ",CTETemp.Receipt_Type,CTETemp.Cust_Type_Code ,CTETemp.Cust_Type_Desc ,CTETemp.Cust_Category_Code ,CTETemp.CUST_CATEGORY_DESC ,CTETemp.Voucher_No As [JE No],CTETemp.JEAccount_Code  AS [JE Account],CTETemp.JEAmount  AS [JE Amount],CTETemp.BalAmt - CTETemp.JEAmount AS DiffAmt   " + Environment.NewLine & _
                        "  ,case when CTETemp.DocType='Adjustment' then 'Adjustment' when CTETemp.DocType='IM' then 'Apply Document' when CTETemp.DocType='CR' then 'Credit Note' when CTETemp.DocType='DR' then 'Debit Note' when CTETemp.DocType='IN' then 'Invoice' when CTETemp.DocType='OA' then 'On Account' when CTETemp.DocType='PR' then 'Advance' when CTETemp.DocType='RC' then 'Receipt' " & _
                        " when CTETemp.DocType='RF' then 'Refund' when CTETemp.DocType='RV-TA' then 'Bank Reverse' when CTETemp.DocType='UA' then 'Unapplied' else CTETemp.DocType end as DocumentType " & _
                         " from CTETemp ORDER BY CTETemp.ACode,  CTETemp.RowNo"

                    End If
                End If

                If ChkDocWise.Checked = True Then
                    If ChkDocSumm.Checked = True Then
                        strQry = "WITH CTETEMP as ( SELECT '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,ROW_NUMBER() over (PARTITION By DocNo ORDER By MAX(RefDocDate)) AS RowNo, MAX(CustCode) AS [CustomerCode],MAX(CUSTNAME) AS [Customer Name],DocNo AS [DocumentNo] ,MAX(doctype) As [Doc Type],MAX(DocTypeCode) AS [Doc Type Code],MAX(xxx.Cust_Group_Code) AS [Cust Group Code],MAX(xxx.Route_No) AS [Route No],MAX(xxx.Zone_Code) AS [Zone Code],MAX ([Order Number]) AS [Order Number],case when  MAX(CONVERT(VARCHAR,[Due Date],103))='01/01/1900' then null else MAX(CONVERT(VARCHAR,[Due Date],103)) end AS [Due Date],MAX([Against Sale No]) AS [Against Sale No],MAX([Against Sale Return No]) As [Against Sale Return No],MAX([Against MCC Material Sale Return]) AS [Against MCC Material Sale Return],MAX(AgainstScrap) AS [AgainstScrap],MAX([Against VCGL]) AS [Against VCGL],MAX(Description) AS [Description],MAX(Remarks) As [Remarks],MAX([Child Cust Code]) As [Child Cust Code],CASE WHEN MAX(ISNULL([Child Cust Code],''))<>'' THEN MAX([Child Cust Code]) ELSE MAX(CustCode) END AS MainCustCode,CASE WHEN MAX(ISNULL([Child Cust Name],''))<>'' THEN MAX([Child Cust Name])  ELSE MAX(CustName)  END AS MainCustName,MAX([Source Doc No]) AS [Source Doc No] ,MAX([Loc Code]) AS  [Loc Code],MAX([Loc Desp]) AS  [Loc Desp],MAX(CONVERT(VARCHAR,DOCDATE,103)) AS [Document Date],MAX(RefDocNo) AS [Ref Doc No],MAX(CONVERT (VARCHAR,RefDocDate,101)) AS [Ref Doc Date],MAX(SubDocType) AS [Sub Doc Type],SUM(DrAmt) AS DrAmt,SUM(CrAmt) AS CrAmt,CASE WHEN SUM(CrAmt) =0 THEN SUM(DrAmt) ELSE SUM(CrAmt) * -1 END AS [Trans Amt],MAX(BalAmt) AS BalAmt,CASE WHEN MAX(ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')) <> '' THEN MAX(ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')) ELSE MAX(CUSTCODE) END AS ParentCode   From ("
                    Else
                        strQry = " WITH CTETEMP as ( SELECT '" + runDate + "' as RunDate,'" + CompanyAdd + "' as CompanyAddress ,'" + compname + "' as CompanyName, '" + strFromDate + "' as FilterFromDate,'" + strToDate + "' as FilterToDate,ROW_NUMBER() over (PARTITION By DocNo ORDER By CONVERT(DATE,RefDocDate,103)) AS RowNo, CustCode As [CustomerCode],CustName AS [Customer Name],DocNo As [DocumentNo],DocType AS [Doc Type],DocTypeCode AS [Doc Type Code],xxx.Cust_Group_Code AS [Cust Group Code],xxx.Route_No AS [Route No],xxx.Zone_Code AS [Zone Code],[Order Number] AS [Order Number],case when  CONVERT(VARCHAR,[Due Date],103)='01/01/1900' then null else CONVERT(VARCHAR,[Due Date],103) end AS [Due Date],[Against Sale No] AS [Against Sale No],[Against Sale Return No] As [Against Sale Return No],[Against MCC Material Sale Return] AS [Against MCC Material Sale Return],[AgainstScrap] AS [AgainstScrap],[Against VCGL] AS [Against VCGL],[Description] AS [Description],[Source Doc No] ,[Remarks] As [Remarks],[Child Cust Code],CASE WHEN ISNULL([Child Cust Code],'')<>'' THEN [Child Cust Code] ELSE CustCode END AS MainCustCode,CASE WHEN ISNULL([Child Cust Name],'')<>'' THEN [Child Cust Name]  ELSE CustName  END AS MainCustName,[Loc Code] AS  [Loc Code],[Loc Desp] AS  [Loc Desp],CONVERT(VARCHAR,DocDate,103) AS [Document Date],RefDocNo AS [Ref Doc No],CONVERT (VARCHAR,RefDocDate,101) AS [Ref Doc Date],SubDocType AS [Sub Doc Type],DrAmt AS DrAmt,CrAmt ,CASE WHEN CrAmt =0 THEN DrAmt ELSE CrAmt * -1 END AS [Trans Amt] ,BalAmt,CASE WHEN ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'')<>''  THEN ISNULL(TSPL_CUSTOMER_MASTER.Parent_Customer_No,'') ELSE CustCode END AS ParentCode FROM ("
                    End If
                    strQry += Environment.NewLine + " ------- AR INVOICE --------  " + Environment.NewLine & _
                    " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,TSPL_Customer_Invoice_Head.Document_No AS DocNo,'AR Invoice' AS DocType,'IN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, '' AS RefDocNo, Null AS RefDocDate,''  As SubDocType,TSPL_Customer_Invoice_Head.Document_Total " & _
                    " AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  From TSPL_Customer_Invoice_Head " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code  WHERE TSPL_Customer_Invoice_Head.Status=1 " & _
                    " AND TSPL_Customer_Invoice_Head.Document_Type='I' " + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " ---------  CREDIT NOTE AGAINST INVOICE ------- " + Environment.NewLine & _
                    " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo,'Credit Note' AS DocType,'CN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_No ELSE '' END AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_Date ELSE NULL END AS RefDocDate,'Credit Note'  As SubDocType,0 AS DrAmt,ISNULL(TSPL_Customer_Invoice_Head.Document_Total ,0) AS CrAmt,0 AS TransAmt,0 AS BalAmt From TSPL_Customer_Invoice_Head " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_Customer_Invoice_Head.Status=1 " & _
                    " AND TSPL_Customer_Invoice_Head.Document_Type='C' AND ISNULL(RefDocNo,'')<>'' " + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " ------- CREDIT NOTE SEPEARTED ---------------- " + Environment.NewLine & _
                    " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo,'Credit Note' AS DocType,'CN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_No ELSE '' END AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.Document_Date ELSE NULL END AS RefDocDate,'Credit Note'  As SubDocType,0 AS DrAmt,TSPL_Customer_Invoice_Head.Document_Total  AS CrAmt,0 AS TransAmt,0 AS BalAmt From TSPL_Customer_Invoice_Head  LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_Customer_Invoice_Head.Status=1  AND TSPL_Customer_Invoice_Head.Document_Type='C' AND ISNULL(RefDocNo,'')='' " + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " --------- DEBIT NOTE SEPERATED ---------------- " + Environment.NewLine & _
                    " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo,'Debit Note' AS DocType,'DN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate,'' AS RefDocNo, NULL AS RefDocDate,''  As SubDocType,TSPL_Customer_Invoice_Head.Document_Total AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  From TSPL_Customer_Invoice_Head " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code  WHERE TSPL_Customer_Invoice_Head.Status=1 " & _
                    " AND TSPL_Customer_Invoice_Head.Document_Type='D' AND ISNULL(RefDocNo,'')='' " + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " ------------ DEBIT NOTE AGAINST INVOICE ----------- " + Environment.NewLine & _
                    " SELECT TSPL_Customer_Invoice_Head.Customer_Code AS CustCode,TSPL_Customer_Invoice_Head.Customer_Name AS CustName,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN TSPL_Customer_Invoice_Head.RefDocNo ELSE TSPL_Customer_Invoice_Head.Document_No END AS DocNo,'Debit Note' AS DocType,'DN' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],NULL AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],ISNULL (TSPL_Customer_Invoice_Head.Loc_Code ,'') As [Loc Code],ISNULL(TSPL_LOCATION_MASTER.Location_Desc ,'') AS [Loc Desp],  TSPL_Customer_Invoice_Head.Document_Date  AS DocDate, TSPL_Customer_Invoice_Head.Document_No AS RefDocNo,CASE WHEN ISNULL(RefDocNo,'')<>'' THEN  TSPL_Customer_Invoice_Head.Document_Date ELSE NULL END AS RefDocDate,'Debit Note'  As SubDocType,TSPL_Customer_Invoice_Head.Document_Total AS DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt  From TSPL_Customer_Invoice_Head " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Customer_Invoice_Head.Loc_Code  LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_Customer_Invoice_Head.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code  WHERE TSPL_Customer_Invoice_Head.Status=1 " & _
                    " AND TSPL_Customer_Invoice_Head.Document_Type='D' AND ISNULL(RefDocNo,'')<>'' " + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " ----------- RECEIPT ENTRIES (APPLY DOCUMENT/RECEIPT)-------------- " + Environment.NewLine & _
                    " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode, TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_DETAIL.Document_No AS DocNo,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' ELSE NULL END AS DocType,ISNULL(TSPL_RECEIPT_HEADER.Receipt_Type,'') AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name], '' AS [Source Doc No],reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],TSPL_RECEIPT_DETAIL.Document_Date AS DocDate,TSPL_RECEIPT_HEADER.Receipt_No AS RefDocNo,CONVERT(DATE, TSPL_RECEIPT_HEADER.Receipt_Date  ,103) AS RefDocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' END  AS SubDocType, 0 as DrAmt, TSPL_RECEIPT_DETAIL.Applied_Amount  AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_RECEIPT_DETAIL   " & _
                    " LEFT OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No  = TSPL_RECEIPT_DETAIL.Receipt_No  " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " & _
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_DETAIL.Child_Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code  " & _
                    " WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('R','A') AND ISNULL(SecurityDeposit,'')='N' " + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " ---------- REFUND " + Environment.NewLine & _
                    " SELECT TSPL_RECEIPT_HEADER.Cust_Code as CustCode,TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_HEADER.Receipt_No AS DocNo,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' ELSE NULL END AS DocType,ISNULL(TSPL_RECEIPT_HEADER.Receipt_Type,'') AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) As [Loc Code] ,ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],TSPL_RECEIPT_HEADER.Receipt_Date AS DocDate,'' as RefDocNo,NULL AS RefDocDate,'' As SubDocType,TSPL_RECEIPT_HEADER.Receipt_Amount as DrAmt,0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM  TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('F') AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'  " + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " --------- ADVANCE/ON-ACCOUNT ------------- " + Environment.NewLine & _
                    " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode,TSPL_RECEIPT_HEADER.Customer_Name AS CustName,Receipt_No AS DocNo,CASE WHEN Receipt_Type='P' THEN 'Advance' WHEN Receipt_Type='O' THEN 'On Account' ELSE NULL END AS DocType,ISNULL(Receipt_Type,'') AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name], '' AS [Source Doc No],Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],Receipt_Date AS DocDate,'' as RefDocNo,NULL AS RefDocDate,''  AS SubDocType, 0  AS DrAmt, Receipt_Amount AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM  TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('P','O') AND ISNULL(SecurityDeposit,'')='N' " + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " -------------- APPLY DOCUMENT AGAINST ADVANCE/ON-ACCOUNT------------- " + Environment.NewLine & _
                    " SELECT TSPL_RECEIPT_HEADER.Cust_Code AS CustCode, TSPL_RECEIPT_HEADER.Customer_Name AS CustName,TSPL_RECEIPT_HEADER.Applied_Receipt  AS DocNo,CASE WHEN (SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt)= 'P' THEN 'Advance' WHEN (SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt)= 'O' THEN 'On Account' END AS DocType,(SELECT RHI.Receipt_Type FROM TSPL_RECEIPT_HEADER RHI WHERE RHI.Receipt_No=TSPL_RECEIPT_HEADER.Applied_Receipt) AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],CASE WHEN ISNULL(TSPL_RECEIPT_HEADER.Payment_Code  ,'CHEQUE')='' THEN ISNULL(TSPL_RECEIPT_HEADER.Cheque_No ,'') END AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code ,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name], '' AS [Source Doc No],reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3))  AS [Loc Code],ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'') AS [Loc Desp],TSPL_RECEIPT_HEADER.Receipt_Date AS DocDate,TSPL_RECEIPT_HEADER.Receipt_No  AS RefDocNo,CONVERT(DATE, TSPL_RECEIPT_HEADER.Receipt_Date  ,103) AS RefDocDate,CASE WHEN TSPL_RECEIPT_HEADER.Receipt_Type='R' THEN 'Receipt' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='P' THEN 'Advance' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='O' THEN 'On Account' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='A' THEN 'Apply Document' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='U' THEN 'UnApplied' WHEN TSPL_RECEIPT_HEADER.Receipt_Type='F' THEN 'Refund' END  AS SubDocType, 0 as DrAmt, TSPL_RECEIPT_DETAIL.Applied_Amount   AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_RECEIPT_HEADER " & _
                    " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No  = TSPL_RECEIPT_DETAIL.Receipt_No  " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =Reverse(substring(reverse(ISNULL(TSPL_RECEIPT_HEADER.Dr_Account,'')),1,3)) " & _
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_DETAIL.Child_Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code  " & _
                    " WHERE  TSPL_RECEIPT_HEADER.Posted='Y' AND  TSPL_RECEIPT_HEADER.Receipt_Type IN ('A') AND ISNULL(SecurityDeposit,'')='N' AND ISNULL(Applied_Receipt,'')<>''" + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " ----------- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT-BULK)------------ " + Environment.NewLine & _
                    " SELECT TSPL_BANK_REVERSE.Cust_Code AS CustCode, TSPL_BANK_REVERSE.Cust_Name AS CustName,TSPL_Customer_Invoice_Head.Document_No  AS DocNo, 'Bank Reverse' AS DocType,'RV' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name],'' AS [Source Doc No],ISNULL (TSPL_INVOICE_MASTER_BULKSALE.Location_Code,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] , TSPL_Customer_Invoice_Head.Document_Date AS DocDate ,RHM.Receipt_No AS RefDocNo, RHM.Receipt_Date AS RefDocDate,CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' ELSE '' END  AS SubDocType, TSPL_BANK_REVERSE.Amount AS DrAmt, 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_INVOICE_MASTER_BULKSALE " & _
                    " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No  " & _
                    " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_DETAIL.Document_No =TSPL_Customer_Invoice_Head.Document_No " & _
                    " LEFT OUTER JOIN TSPL_RECEIPT_HEADER RHM ON RHM.Receipt_No = TSPL_RECEIPT_DETAIL.Receipt_No   " & _
                    " LEFT OUTER JOIN  TSPL_BANK_REVERSE ON TSPL_BANK_REVERSE .Document_No = RHM.Receipt_No " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_INVOICE_MASTER_BULKSALE.Location_Code " & _
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_RECEIPT_DETAIL.Child_Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code " & _
                    " WHERE TSPL_BANK_REVERSE.Source_Type='AR' AND TSPL_BANK_REVERSE.post='P' AND RHM.Receipt_Type IN ('A','R')  AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' " + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " -------------- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT)------------- " + Environment.NewLine & _
                    " SELECT TSPL_BANK_REVERSE.Cust_Code AS CustCode, TSPL_BANK_REVERSE.Cust_Name AS CustName,RHM.Receipt_No  AS DocNo, 'Bank Reverse' AS DocType,'RV' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name],'' AS [Source Doc No],Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] , RHM.Receipt_Date AS DocDate ,RHM.Receipt_No AS RefDocNo, RHM.Receipt_Date AS RefDocDate,CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' ELSE '' END  AS SubDocType,  TSPL_BANK_REVERSE.Amount AS  DrAmt, 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_BANK_REVERSE " & _
                    " LEFT OUTER JOIN  TSPL_RECEIPT_HEADER RHM ON TSPL_BANK_REVERSE.Document_No = RHM.Receipt_No  " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) " & _
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_BANK_REVERSE.Cust_Code  = TSPL_CUSTOMER_MASTER.Cust_Code " & _
                    " WHERE TSPL_BANK_REVERSE.Source_Type='AR' AND TSPL_BANK_REVERSE.post='P' AND RHM.Receipt_Type IN ('P','O')  AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' " + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " ------------- BANK REVERSE (FOR APPLY DOCUMENT/RECEIPT) ------------ " + Environment.NewLine & _
                    " SELECT TSPL_BANK_REVERSE.Cust_Code AS CustCode, TSPL_BANK_REVERSE.Cust_Name AS CustName,TSPL_RECEIPT_DETAIL.Document_No AS DocNo, 'Bank Reverse' AS DocType,'RV' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(TSPL_CUSTOMER_MASTER.Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,ISNULL(TSPL_SD_SHIPMENT_HEAD.Against_Sales_Order ,'') AS [Order Number],NULL  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],ISNULL(TSPL_RECEIPT_DETAIL.Child_Cust_Code,'') AS [Child Cust Code],ISNULL(TSPL_CUSTOMER_MASTER.Customer_Name,'') As [Child Cust Name],'' AS [Source Doc No],Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp] , RHM.Receipt_Date   AS DocDate ,TSPL_BANK_REVERSE.Reverse_Code AS RefDocNo, TSPL_BANK_REVERSE.Reversal_Date  AS RefDocDate,CASE WHEN RHM.Receipt_Type='R' THEN 'Receipt' ELSE '' END  AS SubDocType,  TSPL_BANK_REVERSE.Amount AS  DrAmt, 0 AS CrAmt,0 AS TransAmt,0 AS BalAmt FROM TSPL_BANK_REVERSE " & _
                    " LEFT OUTER JOIN TSPL_RECEIPT_HEADER RHM ON RHM.Receipt_No = TSPL_BANK_REVERSE.Document_No " & _
                    " LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_DETAIL.Receipt_No = RHM.Receipt_No " & _
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = RHM.Cust_Code " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = Reverse(substring(reverse(ISNULL(RHM.Dr_Account,'')),1,3)) " & _
                    " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No = TSPL_RECEIPT_DETAIL.Document_No " & _
                    " LEFT OUTER JOIN TSPL_SD_SHIPMENT_HEAD ON TSPL_Customer_Invoice_Head.Against_Sale_No =TSPL_SD_SHIPMENT_HEAD.Document_Code " & _
                    " WHERE TSPL_BANK_REVERSE.Source_Type='AR' AND TSPL_BANK_REVERSE.post='P' AND RHM.Receipt_Type IN ('A','R') AND  ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit ,'')='N' " + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " -----------  VCGL DATA(FIRST QUERY)------------ " + Environment.NewLine & _
                    " SELECT TSPL_VCGL_Head.VC_Code AS CustCode, TSPL_VCGL_Head.VC_Name AS CustName, TSPL_VCGL_Head.Document_No AS DocNo,'VCGL' AS DocType,'AD' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name],'' AS [Source Doc No],ISNULL(TSPL_VCGL_Head.Location_Segment ,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp], CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) AS DocDate,'' AS RefDocNo,Null AS RefDocDate,''  As SubDocType,CASE WHEN  TSPL_VCGL_Head.Amount_Type='Cr' THEN  TSPL_VCGL_Head.Amount ELSE 0 END  AS DrAmt ,CASE WHEN  TSPL_VCGL_Head.Amount_Type='Dr' THEN  TSPL_VCGL_Head.Amount ELSE 0 END AS CrAmt,0 AS TransAmt,0 AS BalAmt  FROM TSPL_VCGL_Head LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_VCGL_Head.Location_Segment LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Head.VC_Code= TSPL_CUSTOMER_MASTER.Cust_Code  WHERE TSPL_VCGL_Head.Document_Type='C' and  TSPL_VCGL_Head.Status=1 AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') ='' " + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " --------- VCGL DATA(SECOND QUERY)----------- " + Environment.NewLine & _
                    " SELECT TSPL_VCGL_Detail.VCGL_Code AS CustCode, TSPL_VCGL_Detail.VCGL_Name AS CustName, TSPL_VCGL_Head.Document_No AS DocNo,'VCGL' AS DocType,'AD' AS DocTypeCode,ISNULL(Cust_Group_Code,'') AS Cust_Group_Code,ISNULL(Route_No,'') AS Route_No,ISNULL(Zone_Code,'') AS Zone_Code,'' AS [Order Number],''  AS [Due Date],'' AS [Against Sale No],'' As [Against Sale Return No],'' AS [Against MCC Material Sale Return],'' AS [AgainstScrap],'' AS [Against VCGL],'' AS [Description],'' As [Remarks],'' AS [Child Cust Code],'' As [Child Cust Name],'' AS [Source Doc No],ISNULL(TSPL_VCGL_Head.Location_Segment ,'') AS [Loc Code], ISNULL (TSPL_LOCATION_MASTER.Location_Desc,'' ) AS [Loc Desp], CONVERT(DATE, TSPL_VCGL_Head.Document_Date,103) AS DocDate, '' AS RefDocNo,NULL AS RefDocDate,''  As SubDocType, TSPL_VCGL_Detail.Dr_Amount AS DrAmt , TSPL_VCGL_Detail.Cr_Amount AS CrAmt ,0 AS TransAmt,0 AS BalAmt FROM  TSPL_VCGL_Detail LEFT OUTER JOIN  TSPL_VCGL_Head ON  TSPL_VCGL_Head .Document_No= TSPL_VCGL_Detail.Document_No LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code  =TSPL_VCGL_Head.Location_Segment LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_VCGL = TSPL_VCGL_Head.Document_No LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_VCGL_Detail.VCGL_Code= TSPL_CUSTOMER_MASTER.Cust_Code WHERE TSPL_VCGL_Head.Status=1 AND  TSPL_VCGL_Detail.Row_Type='Customer' AND ISNULL(TSPL_Customer_Invoice_Head.Against_VCGL,'') ='' " & _
                    " )xxx " & _
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code =xxx.CustCode  " & _
                    " WHERE CONVERT (date, DocDate,103) >= '" & strFromDate & "' And CONVERT (date,DocDate,103) <='" & strToDate & "'" + StrDocWiseFilter + ""
                    If ChkDocSumm.Checked = True Then
                        strQry += " Group By DocNo "
                    End If
                    strQry += " ) " + Environment.NewLine & _
                    " Select * from (" & _
                    " Select *, Case When CumAmt<0 Then 0 Else CumAmt End as CumAmt1 from (" & _
                    " Select *, Case When (Select SUM(DrAmt-CrAmt) from CTETEMP CTET WHERE CTET.[DocumentNo]=CTETEMP.[DocumentNo] AND CTET.RowNo<=CTETEMP.RowNo)<0 Then (Select SUM(DrAmt-CrAmt) from CTETEMP CTET WHERE CTET.[DocumentNo]=CTETEMP.[DocumentNo] AND CTET.RowNo<CTETEMP.RowNo) Else [Trans Amt] End as [TransAmt1], (Select SUM(DrAmt-CrAmt) from CTETEMP CTET WHERE CTET.[DocumentNo]=CTETEMP.[DocumentNo] AND CTET.RowNo<=CTETEMP.RowNo) AS CumAmt FROM CTETEMP) YYY " + Environment.NewLine & _
                    " UNION ALL " + Environment.NewLine & _
                    " Select YYY.RunDate,YYY.CompanyAddress,YYY.CompanyName,YYY.FilterFromDate,YYY.FilterToDate,YYY.RowNo, YYY.CustomerCode AS CustomerCode,YYY.[Customer Name]  AS [Customer Name],YYY.[Ref Doc No] as [Document No],'Credit Note' AS [Doc Type],'CN' AS [Doc Type Code],YYY.[Cust Group Code] AS [Cust Group Code] ,YYY.[Route No]  AS Route_No,YYY.[Zone Code] AS Zone_Code,YYY.[Order Number] AS [Order Number],YYY.[Due Date] AS [Due Date],yyy.[Against Sale No]  AS [Against Sale No],YYY.[Against Sale Return No]  As [Against Sale Return No],YYY.[Against MCC Material Sale Return] AS [Against MCC Material Sale Return],YYY.AgainstScrap  AS [AgainstScrap],YYY.[Against VCGL]  AS [Against VCGL],YYY.Description  AS [Description],YYY.[Source Doc No] AS [Source Doc No],YYY.Remarks  As [Remarks],YYY.[Child Cust Code]  AS [Child Cust Code],YYY.MainCustCode AS MainCustCode,YYY.MainCustName AS MainCustName, " & _
                    " YYY.[Loc Code] As [Loc Code],YYY.[Loc Desp] AS [Loc Desp], YYY.[Document Date] AS [Document Date],YYY.DocumentNo AS [Ref Doc No],YYY.[Ref Doc Date] AS [Ref Doc Date],'AR Invoice'  As [Sub Doc Type],YYY.DrAmt AS DrAmt,YYY.CrAmt  AS CrAmt,YYY.[Trans Amt] AS [Trans Amt],YYY.BalAmt AS BalAmt,YYY.ParentCode AS ParentCode, CumAmt As TransAmt1,CumAmt AS CumAmt,CumAmt AS CumAmt1 FROM (Select *, (Select SUM(DrAmt-CrAmt) from CTETEMP CTET WHERE CTET.[DocumentNo]=CTETEMP.[DocumentNo] AND CTET.RowNo<=CTETEMP.RowNo) AS CumAmt FROM CTETEMP) YYY WHERE [Doc Type Code]='CN' AND CumAmt<0) ZZZ ORDER BY RowNo "
                End If

                '' bulk export
                If BulkExport = 1 Then
                    transportSql.BulkExport("Customer_Ledger", strQry, "ORDER BY CTETemp.ACode,  CTETemp.RowNo", "csv", "select ctetemp.rowno")
                    Exit Sub
                ElseIf BulkExport = 2 Then
                    transportSql.BulkExport("Customer_Ledger", strQry, "ORDER BY CTETemp.ACode,  CTETemp.RowNo", "xls", "select ctetemp.rowno")
                    Exit Sub
                End If
                dtMain = clsDBFuncationality.GetDataTable(strQry)

                If dtMain.Rows.Count <= 0 Then
                    btnPrint.Enabled = False
                    clsCommon.MyMessageBoxShow("Data not found")
                    gvDetails.DataSource = Nothing
                    gvDetails.Rows.Clear()
                    gvDetails.Columns.Clear()
                    Exit Sub
                Else
                    If rbtnCustWise.Checked Or rbtnNone.Checked Then
                        btnBack.Enabled = True
                    Else
                        btnBack.Enabled = False
                    End If
                    btnPrint.Enabled = True
                    gvDetails.DataSource = Nothing
                    gvDetails.Rows.Clear()
                    gvDetails.Columns.Clear()
                End If
                If ChkDocWise.Checked = False Then
                    gvCustomerGroup.DataSource = dtCustGrp
                    gvCustomerGroup.Tag = IIf(rbtnCustGroupWiseDrCr.Checked, "DRCR", "")
                    FormatgvCustGroup()

                    gvCustomer.DataSource = dtCustomer
                    gvCustomerGroup.Tag = IIf(rbtnCustWiseDrCr.Checked, "DRCR", "")
                    FormatgvCustomer()
                End If
                gvDetails.DataSource = dtMain
                gvDetails.AllowRowReorder = False

                If ChkDocWise.Checked = True Then
                    FormatGridDocWise()
                Else
                    FormatGrid(False)
                End If

                If ChkDocWise.Checked = False Then
                    gridHideVisible(IsDrillDown)
                End If
            End If


            If BackProcess = False Then
                If rbtnCustGroupWise.Checked = True AndAlso gvCustomer.Visible = True Then
                    FilterForLevels += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in ('" + clsCommon.myCstr(gvCustomerGroup.CurrentRow.Cells("Cust_Group_Code").Value) + "')  "
                End If
                If (rbtnCustGroupWise.Checked = True AndAlso gvDetails.Visible = True) OrElse (rbtnCustWise.Checked = True AndAlso gvDetails.Visible = True) Then
                    ACodeFilter = clsCommon.myCstr(gvCustomer.CurrentRow.Cells("ACode").Value)
                End If
                If (rbtnCustWise.Checked = True AndAlso gvDetails.Visible = True) OrElse (rbtnCustGroupWise.Checked = True AndAlso gvDetails.Visible = True) Then
                    dvTemp1 = New DataView(dtMain)
                    dvTemp1.RowFilter = "ACode = '" + ACodeFilter + "'"
                    gvDetails.DataSource = Nothing
                    gvDetails.DataSource = dvTemp1.ToTable()
                    FormatGrid(False)
                End If
            End If
            BackProcess = False
            IsDrillDown = False

            If btnrefresh = False Then
                If isExportToExcel = True Then
                    Dim arrHeadrer As New List(Of String)
                    arrHeadrer.Add("From Date : " + strFromDate + "")
                    arrHeadrer.Add("To Date : " + strToDate + "")
                    If gvCustomerGroup.Visible Then
                        clsCommon.MyExportToExcel("Customer Ledger (Customer Group Wise)", gvCustomerGroup, arrHeadrer, "CustomerLedger")
                    ElseIf gvCustomer.Visible Then
                        clsCommon.MyExportToExcel("Customer Ledger (Customer Wise)", gvCustomer, arrHeadrer, "CustomerLedger")
                    Else
                        clsCommon.MyExportToExcel("Customer Ledger", gvDetails, arrHeadrer, "CustomerLedger")
                    End If
                Else
                    If ChkDocWise.Checked = False Then
                        Dim frmcrystal As New frmCrystalReportViewer()
                        If rbtnCustGroupWiseDrCr.Checked Then
                            If gvCustomerGroup.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustGrp, "rptCustomerLedgerSummaryDRCR_DEMO", "Customer Ledger")
                            ElseIf gvCustomer.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustomer, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                            ElseIf gvDetails.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dvTemp1.ToTable(), "rptCustomerLedger_DEMO", "Customer Ledger")
                            End If
                        ElseIf rbtnCustGroupWise.Checked Then
                            If gvCustomerGroup.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustGrp, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                            ElseIf gvCustomer.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustomer, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                            ElseIf gvDetails.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dvTemp1.ToTable(), "rptCustomerLedger_DEMO", "Customer Ledger")
                            End If
                        ElseIf rbtnCustWiseDrCr.Checked Then
                            'Balwinder
                            If gvCustomerGroup.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustGrp, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                            ElseIf gvCustomer.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustomer, "rptCustomerWiseDRCR", "Customer Ledger")
                            ElseIf gvDetails.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dvTemp1.ToTable(), "rptCustomerLedger_DEMO", "Customer Ledger")
                            End If
                        ElseIf rbtnCustWise.Checked Then
                            If gvCustomerGroup.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustGrp, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                            ElseIf gvCustomer.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dtCustomer, "rptCustomerLedgerSummary_DEMO", "Customer Ledger")
                            ElseIf gvDetails.Visible Then
                                frmcrystal.funreport(CrystalReportFolder.SalesReport, dvTemp1.ToTable(), "rptCustomerLedger_DEMO", "Customer Ledger")
                            End If
                        ElseIf rbtnNone.Checked Then
                            frmcrystal.funreport(CrystalReportFolder.SalesReport, dtMain, "rptCustomerLedger_DEMO", "Customer Ledger")
                        ElseIf rbtnDocWise.Checked Then
                            frmcrystal.funreport(CrystalReportFolder.SalesReport, dtMain, "rptCustomerLedger_DEMO", "Customer Ledger")
                        End If
                    Else
                        Dim frmcrystal As New frmCrystalReportViewer()
                        If ChkDocSumm.Checked = True Then
                            frmcrystal.funreport(CrystalReportFolder.SalesReport, dtMain, "rptCustomerLedgerDocSummWise", "Customer Ledger")
                        Else
                            frmcrystal.funreport(CrystalReportFolder.SalesReport, dtMain, "rptCustomerLedgerDocWise", "Customer Ledger")
                        End If

                    End If
                End If
            End If
            gvDetails.EnableFiltering = True
            gvDetails.ShowFilteringRow = True
            RadPageView1.SelectedPage = RadPageViewPage2
            RadGroupBox1.Enabled = False
            ReStoreGridCust()
            ReStoreGridCustGrp()
            ReStoreGridDetail()
            GridSummaryRow()
            gvDetails.MasterTemplate.SortDescriptors.Clear()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkReconcile_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkReconcile.ToggleStateChanged
        If chkReconcile.Checked Then
            rbtnCustWise.Checked = True
        End If
    End Sub

    Private Sub chkActive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkActive.CheckedChanged
        'LoadCustomer()
    End Sub

    Private Sub chkInactive_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInactive.CheckedChanged
        'LoadCustomer()
    End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        'LoadCustomer()
    End Sub

    '-----------------Save Layout---------------------- BM00000007862
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
                    common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
                End If
                ''richa agarwal regarding memory leakage
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
                ''---------------

            ElseIf gvCustomerGroup.Visible = True Then
                gvCustomerGroup.MasterTemplate.FilterDescriptors.Clear()
                obj = New clsGridLayout()
                obj.ReportID = GetReportID() 'ReportID + "CG"
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gvCustomerGroup.SaveLayout(obj.GridLayout)
                obj.GridColumns = gvCustomerGroup.ColumnCount
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
                End If

                ''richa agarwal regarding memory leakage
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
                ''---------------
            ElseIf gvDetails.Visible = True Then
                gvDetails.MasterTemplate.FilterDescriptors.Clear()
                obj = New clsGridLayout()
                obj.ReportID = GetReportID() 'ReportID + "D"
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                gvDetails.SaveLayout(obj.GridLayout)
                obj.GridColumns = gvDetails.ColumnCount
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
                End If

                ''richa agarwal regarding memory leakage
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
                ''---------------
            End If

        End If
    End Sub

    '-----------------Delete Layout---------------------
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        '  clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
        ReportID = GetReportID()
        If clsCommon.myLen(ReportID) > 0 Then
            If gvCustomer.Visible = True Then
                clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) 'ReportID & "C"
                FormatgvCustomer()
                ReStoreGridCust()
                GridSummaryRow()
            ElseIf gvCustomerGroup.Visible = True Then
                clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) 'ReportID & "CG"
                FormatgvCustGroup()
                ReStoreGridCustGrp()
                GridSummaryRow()
            ElseIf gvDetails.Visible = True Then
                clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) 'ReportID & "D"
                FormatGrid(False)
                ReStoreGridDetail()
                GridSummaryRow()
            End If

            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        End If
    End Sub
    'Ticket No-ERO/18/11/19-001116
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
            If rbtnCustWiseDrCr.Checked Then
                TotalAmt = New GridViewSummaryItem("OpngBalDR", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TotalAmt)
                TotalAmt = New GridViewSummaryItem("OpngBalCR", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TotalAmt)
                TotalAmt = New GridViewSummaryItem("BalAmtDR", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TotalAmt)
                TotalAmt = New GridViewSummaryItem("BalAmtCR", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TotalAmt)
            End If
            gvCustomer.MasterTemplate.SummaryRowsBottom.Clear()
            gvCustomer.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gvCustomer.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ElseIf gvCustomerGroup.Visible = True Then

            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim TotalAmt As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)

            If rbtnCustGroupWiseDrCr.Checked Then
                TotalAmt = New GridViewSummaryItem("OpngBalDR", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TotalAmt)
                TotalAmt = New GridViewSummaryItem("OpngBalCR", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TotalAmt)
                TotalAmt = New GridViewSummaryItem("BalAmtDR", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TotalAmt)
                TotalAmt = New GridViewSummaryItem("BalAmtCR", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TotalAmt)
            Else
                TotalAmt = New GridViewSummaryItem("OpngBal", "{0:F2}", GridAggregateFunction.Sum)
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
            End If

            TotalAmt = New GridViewSummaryItem("Sales", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CollectionRefund", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            TotalAmt = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalAmt)
            gvCustomerGroup.MasterTemplate.SummaryRowsBottom.Clear()
            gvCustomerGroup.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gvCustomerGroup.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ElseIf gvDetails.Visible = True AndAlso ChkDocWise.Checked = False Then
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim ColumnTotal As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(ColumnTotal)
            ColumnTotal = New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(ColumnTotal)
            ColumnTotal = New GridViewSummaryItem("Sales", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(ColumnTotal)
            ColumnTotal = New GridViewSummaryItem("CollectionRefund", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(ColumnTotal)
            ColumnTotal = New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(ColumnTotal)
            ColumnTotal = New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(ColumnTotal)
            If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
                Dim TotalClosing As New GridViewSummaryItem()
                TotalClosing.FormatString = "{0:F2}"
                TotalClosing.Name = "Closing"
                TotalClosing.AggregateExpression = "sum(DrAmt)-sum(CrAmt)+sUM(Sales)-SUM(CollectionRefund)"
                summaryRowItem.Add(TotalClosing)
            Else
                Dim TotalClosing As New GridViewSummaryItem()
                TotalClosing.FormatString = "{0:F2}"
                TotalClosing.Name = "Closing"
                TotalClosing.AggregateExpression = "sum(DrAmt)-sum(CrAmt)"
                summaryRowItem.Add(TotalClosing)
            End If
            gvDetails.MasterTemplate.SummaryRowsBottom.Clear()
            gvDetails.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gvDetails.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ElseIf gvDetails.Visible = True AndAlso ChkDocWise.Checked = True Then
            Dim summaryRowItem1 As New GridViewSummaryRowItem()
            Dim dramt As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem1.Add(dramt)
            If ChkDocSumm.Checked = True Then
                Dim CumAmt1 As New GridViewSummaryItem("CumAmt1", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem1.Add(CumAmt1)
            Else
                Dim summaryItem As New GridViewSummaryItem()
                summaryItem.FormatString = "{0:F2}"
                summaryItem.Name = "CumAmt1"
                summaryItem.AggregateExpression = "SUM([TransAmt1])"
                summaryRowItem1.Add(summaryItem)
                gvDetails.MasterTemplate.SummaryRowsBottom.Clear()
                gvDetails.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem1)
                gvDetails.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If
        End If
    End Sub

    Private Sub gridHideVisible(IsDrillDown)
        If IsDrillDown = False Then
            gvDetails.Visible = False
            gvCustomer.Visible = False
            gvCustomerGroup.Visible = False
            '' Old 
            If rbtnCustGroupWise.Checked OrElse rbtnCustGroupWiseDrCr.Checked Then
                gvCustomerGroup.Visible = True
            ElseIf rbtnCustomerCategory.Checked OrElse rbtnCustomerType.Checked Then
                gvCustomerGroup.Visible = True
            ElseIf rbtnCustWise.Checked OrElse rbtnCustWiseDrCr.Checked Then
                gvCustomer.Visible = True
                btnBack.Enabled = False
            Else
                gvDetails.Visible = True
            End If
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click

        If gvDetails.Visible = True Then
            If clsCommon.CompairString(clsCommon.myCstr(gvCustomer.Tag), "DRCR") = CompairStringResult.Equal Then
                rbtnCustWiseDrCr.Checked = True
            End If
            gvCustomer.Visible = True
            gvCustomerGroup.Visible = False
            gvDetails.Visible = False
            txtCustomer.arrValueMember = Nothing
            'rbtnCustWise.Checked = True
        ElseIf gvCustomer.Visible = True Then
            If clsCommon.CompairString(clsCommon.myCstr(gvCustomerGroup.Tag), "DRCR") = CompairStringResult.Equal Then
                rbtnCustGroupWiseDrCr.Checked = True
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gvCustomerGroup.Tag), "CC") = CompairStringResult.Equal Then
                rbtnCustomerCategory.Checked = True
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gvCustomerGroup.Tag), "CT") = CompairStringResult.Equal Then
                rbtnCustomerType.Checked = True
            End If
            gvCustomerGroup.Visible = True
            gvCustomer.Visible = False
            gvDetails.Visible = False
            txtCustomerGroup.arrValueMember = Nothing
        End If
        IsDrillDown = True
        BackProcess = True
        PageSetupReport_ID = GetReportID()
    End Sub

    Private Sub gvCustomerGroup_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCustomerGroup.CellDoubleClick
        Try
            If isRunDoubleClick Then
                Dim IsCustomerCategoryOrType As String = String.Empty
                If clsCommon.myLen(gvCustomerGroup.CurrentRow.Cells("Cust_Group_Code").Value) > 0 Then
                    If rbtnCustGroupWiseDrCr.Checked Then
                        rbtnCustWiseDrCr.Checked = True
                    ElseIf rbtnCustomerCategory.Checked Then
                        rbtnCustWise.Checked = True
                        IsCustomerCategoryOrType = "CustomerCategory"
                    ElseIf rbtnCustomerType.Checked Then
                        rbtnCustWise.Checked = True
                        IsCustomerCategoryOrType = "CustomerType"
                    Else
                        rbtnCustWise.Checked = True
                    End If
                    Dim arrCustomerGroup As New ArrayList
                    If clsCommon.CompairString(IsCustomerCategoryOrType, "CustomerCategory") = CompairStringResult.Equal Then
                        arrCustomerGroup.Add(gvCustomerGroup.CurrentRow.Cells("Cust_Category_Code").Value)
                        txtCustomerCategory.arrValueMember = arrCustomerGroup
                    ElseIf clsCommon.CompairString(IsCustomerCategoryOrType, "CustomerType") = CompairStringResult.Equal Then
                        arrCustomerGroup.Add(gvCustomerGroup.CurrentRow.Cells("Cust_Type_Code").Value)
                        txtCustomerType.arrValueMember = arrCustomerGroup
                    Else
                        arrCustomerGroup.Add(gvCustomerGroup.CurrentRow.Cells("Cust_Group_Code").Value)
                        txtCustomerGroup.arrValueMember = arrCustomerGroup
                    End If


                    btnrefresh = True
                    If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
                        PrintForCustomerCurrency()
                    Else
                        print()
                    End If


                    gvCustomer.Visible = True
                    gvCustomerGroup.Visible = False
                    gvDetails.Visible = False
                    FormatgvCustomer()
                    btnBack.Enabled = True
                    '        rbtnCustWise.Checked = True
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

    Private Sub gvCustomer_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvCustomer.CellDoubleClick
        Try
            If isRunDoubleClick Then


                If clsCommon.myLen(gvCustomer.CurrentRow.Cells("ACode").Value) > 0 Then
                    gvDetails.DataSource = Nothing
                    gvDetails.Rows.Clear()
                    gvDetails.Columns.Clear()
                    rbtnNone.Checked = True
                    Dim arrCustomer As New ArrayList
                    arrCustomer.Add(gvCustomer.CurrentRow.Cells("ACode").Value)
                    txtCustomer.arrValueMember = arrCustomer
                    btnrefresh = True
                    If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
                        PrintForCustomerCurrency()
                    Else
                        print()
                    End If


                    gvDetails.Visible = True
                    gvCustomerGroup.Visible = False
                    gvCustomer.Visible = False
                    GridSummaryRow()
                    btnBack.Enabled = True
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

    Private Sub gvReport_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvDetails.CellDoubleClick
        If isRunDoubleClick Then
            If clsCommon.myLen(e.Row.Cells.Item("DocNo").Value) > 0 Then
                Dim SoucrCode As String = clsCommon.myCstr(gvDetails.Rows(e.RowIndex).Cells.Item("SourceCode").Value)
                Dim DocNo As String = clsCommon.myCstr(e.Row.Cells.Item("DocNo").Value)


                If SoucrCode = "AR-PY" Or SoucrCode = "AR-RC" Or SoucrCode = "AR-UN" Or SoucrCode = "AR-OA" Or SoucrCode = "AR-RF" Or clsCommon.CompairString(SoucrCode, "AR-IM") = CompairStringResult.Equal Or clsCommon.CompairString(SoucrCode, "AR-KN") = CompairStringResult.Equal Then
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
            'End If
        End If
    End Sub

    Private Sub ChkISParentCust_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkISParentCust.CheckStateChanged
        txtParentCustomer.Enabled = ChkISParentCust.Checked
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
        'strQry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + "))xxx"
        strQry += " (select Loc_Segment_Code  from TSPL_LOCATION_MASTER where LEN(isnull(Loc_Segment_Code,''))>0 group by Loc_Segment_Code having Loc_Segment_Code in (" + StrPermission + "))xxx"
        strQry += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=xxx.Loc_Segment_Code and TSPL_GL_SEGMENT_CODE.Seg_No='7'"
        strQry += " order by xxx.Loc_Segment_Code"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSe5", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
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
        strQry = "select CM1.cust_code as Code, CM1.Customer_Name as Name, Case When ISNULL(CM2.Cust_Code,'')<>'' Then ISNULL(CM2.Cust_Code,'')+' - '+ISNULL(CM2.Customer_Name,'') Else '' End as [ParentCustomer]  from tspl_customer_master CM1 LEFT OUTER JOIN TSPL_CUSTOMER_MASTER CM2 ON CM2.Cust_Code=CM1.Parent_Customer_No where 1=1 "
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
            strQry += " and CM1.cust_code in  ( " + strCusMappingByLocForCurUser + " )   "
        End If

        If chkActive.Checked Then
            strQry += " and CM1.Status='N'"
        ElseIf chkInactive.Checked Then
            strQry += " and CM1.Status='Y'"
        End If

        Dim strCustCatforCurrentUser As String = ""
        strCustCatforCurrentUser = Xtra.CustomerCategory()
        If clsCommon.myLen(strCustCatforCurrentUser) > 0 Then

            strQry += " and isnull(CM1.CUSTOMER_CATEGORY,'') in (" + strCustCatforCurrentUser + ")"
        End If

        ''richa agarwal 14 Feb,2019 ERO/05/02/19-000485
        If AllowtoSHOWParentChildCustomer = True Then
            clsCommon.MyMessageBoxShow("Please select Parent/Child with their Child/Parent for better clarity of report..")
        End If

        Dim dtCustGroup As DataTable = clsDBFuncationality.GetDataTable("select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "'")

        If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 Then
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
                strQry += " AND CM1.cust_code in (" + clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'''' + ( Customer_Code ) +''''  as Alies_Name FROM TSPL_CUSTOMER_LOCATION_MAPPING where Location_Code in (select Location_Code from TSPL_LOCATION_MASTER where Loc_Segment_Code   in ( " + objCommonVar.strCurrUserLocationsSegment + " ) )  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ") + " )"
            Else
                strQry += " AND CM1.cust_code in (select DISTINCT Cust_Code from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL WHERE User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "' "
            End If

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

    Private Sub txtParentCustomer__My_Click(sender As Object, e As EventArgs) Handles txtParentCustomer._My_Click
        strQry = "select cust_code as  Code, Customer_Name as  Name from tspl_customer_master "
        If ChkISParentCust.Checked Then
            strQry += " Where Parent_Customer_YN ='Y' "
        End If
        txtParentCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtParentCustomer.arrValueMember, txtParentCustomer.arrDispalyMember)
    End Sub

    Private Sub txtCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles txtCustomerCategory._My_Click
        strQry = "SELECT Distinct TSPL_CUSTOMER_MASTER.Cust_Category_Code AS [Code],TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC  AS [Description] FROM TSPL_CUSTOMER_MASTER LEFT OUTER JOIN TSPL_CUSTOMER_CATEGORY_MASTER  ON TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE  =  TSPL_CUSTOMER_MASTER.Cust_Category_Code"
        Dim strCustCatforCurrentUser As String = ""
        strCustCatforCurrentUser = Xtra.CustomerCategory()
        If clsCommon.myLen(strCustCatforCurrentUser) > 0 Then
            strQry += " where isnull(TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY,'') in (" + strCustCatforCurrentUser + ")"
        End If


        txtCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustomerCategorySelector@CustomerLedger", strQry, "Code", "Description", txtCustomerCategory.arrValueMember, txtCustomerCategory.arrDispalyMember)
    End Sub

    Private Sub txtCustomerType__My_Click(sender As Object, e As EventArgs) Handles txtCustomerType._My_Click
        strQry = "SELECT DISTINCT TSPL_CUSTOMER_MASTER.Cust_Type_Code AS [Code],TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc  AS [Description] FROM TSPL_CUSTOMER_MASTER LEFT OUTER JOIN TSPL_CUSTOMER_TYPE_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code"
        Dim strCustCatforCurrentUser As String = ""
        strCustCatforCurrentUser = Xtra.CustomerCategory()
        If clsCommon.myLen(strCustCatforCurrentUser) > 0 Then
            strQry += " where isnull(TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY,'') in (" + strCustCatforCurrentUser + ")"
        End If

        txtCustomerType.arrValueMember = clsCommon.ShowMultipleSelectForm("CustomerTypeSelector@CustomerLedger", strQry, "Code", "Description", txtCustomerType.arrValueMember, txtCustomerType.arrDispalyMember)
    End Sub

    Private Sub btnReset_Click_1(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            gvCustomerGroup.DataSource = Nothing
            gvCustomerGroup.Tag = Nothing
            gvCustomer.DataSource = Nothing
            gvDetails.DataSource = Nothing
            gvDetails.Columns.Clear()
            gvDetails.Rows.Clear()
            RadGroupBox1.Enabled = True
            dvTemp1 = Nothing
            GC.Collect()
            RadPageView1.SelectedPage = RadPageViewPage1
            'txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
            Dim AllowTrasactionFilterOnCustomerLedger As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowTransactionFiltersOnCustomerlegder, clsFixedParameterCode.AllowTransactionFiltersOnCustomerlegder, Nothing)) = 1, True, False))
            If AllowTrasactionFilterOnCustomerLedger = True Then
                chkIncludeApplyDocument.Checked = True
                chkIncludeApplyDocument.Enabled = False
                chkIncludeApplyDocument.Visible = False
                GroupBox2.Visible = True
                chkAdjustment.Visible = True
                chkDebitNote.Visible = True
                chkCreditNote.Visible = True
                chkApplyDocument.Visible = True
                ChkInvoice.Visible = True
                chkOnAccount.Visible = True
                chkAdvance.Visible = True
                chkreceipt.Visible = True
                chkrefund.Visible = True
                chkBankReverse.Visible = True
                ChkUnapplied.Visible = True
                FilterOFDocumnetType()
            Else
                GroupBox2.Visible = False
                chkAdjustment.Visible = False
                chkDebitNote.Visible = False
                chkCreditNote.Visible = False
                chkApplyDocument.Visible = False
                ChkInvoice.Visible = False
                chkOnAccount.Visible = False
                chkAdvance.Visible = False
                chkreceipt.Visible = False
                chkrefund.Visible = False
                chkBankReverse.Visible = False
                ChkUnapplied.Visible = False

                chkAdjustment.Checked = False
                chkDebitNote.Checked = False
                chkCreditNote.Checked = False
                chkApplyDocument.Checked = False
                ChkInvoice.Checked = False
                chkOnAccount.Checked = False
                chkAdvance.Checked = False
                chkreceipt.Checked = False
                chkrefund.Checked = False
                chkBankReverse.Checked = False
                ChkUnapplied.Checked = False

                chkIncludeApplyDocument.Checked = False
                chkIncludeApplyDocument.Enabled = True
                chkIncludeApplyDocument.Visible = True
            End If
            '-- done by richa agarwal related to ticket no. KDI/12/03/18-000107 on 13 Mar,2018
            Dim AllowtoMakeApplyDocOnbyDefault As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoMakeApplyDocOnbyDefault, clsFixedParameterCode.AllowtoMakeApplyDocOnbyDefault, Nothing)) = 1, True, False))
            If AllowtoMakeApplyDocOnbyDefault = True Then
                chkIncludeApplyDocument.Checked = True
            End If
            chkExcludeOpening.Checked = False
            chkDateWise.Checked = False
            chkIncludeCardIndent.Checked = True
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


            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If

            If rbtnCustGroupWise.Checked AndAlso gvCustomerGroup.Visible = True Then ' gvCustomerGroup.Visible 
                'transportSql.exportdataChilRows(gvCustomerGroup, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvCustomerGroup, "", Me.Text, , arrHeader)
            ElseIf rbtnCustGroupWise.Checked AndAlso gvCustomer.Visible = True Then
                'transportSql.exportdataChilRows(gvCustomer, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvCustomer, "", Me.Text, , arrHeader)
            ElseIf rbtnCustGroupWise.Checked AndAlso gvDetails.Visible = True Then
                'transportSql.exportdataChilRows(gvDetails, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
            End If
            If rbtnCustWise.Checked AndAlso gvCustomer.Visible = True Then ' gvCustomer.Visible 
                'transportSql.exportdataChilRows(gvCustomer, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvCustomer, "", Me.Text, , arrHeader)
            ElseIf rbtnCustWise.Checked AndAlso gvDetails.Visible = True Then
                'transportSql.exportdataChilRows(gvDetails, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
            End If
            If rbtnNone.Checked Then ' gvDetails.Visible 

                'transportSql.exportdataChilRows(gvDetails, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
            End If
            If rbtnDocWise.Checked Then ' gvDetails.Visible 
                'transportSql.exportdataChilRows(gvDetails, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
            End If

            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
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


            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If

            If rbtnCustGroupWise.Checked AndAlso gvCustomerGroup.Visible = True Then ' gvCustomerGroup.Visible 
                transportSql.applyExportTemplate(gvCustomerGroup, PageSetupReport_ID)
                'transportSql.exportdataChilRows(gvCustomerGroup, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvCustomerGroup, "", Me.Text, , arrHeader)
            ElseIf rbtnCustGroupWise.Checked AndAlso gvCustomer.Visible = True Then
                transportSql.applyExportTemplate(gvCustomer, PageSetupReport_ID)
                'transportSql.exportdataChilRows(gvCustomer, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvCustomer, "", Me.Text, , arrHeader)
            ElseIf rbtnCustGroupWise.Checked AndAlso gvDetails.Visible = True Then
                transportSql.applyExportTemplate(gvDetails, PageSetupReport_ID)
                'transportSql.exportdataChilRows(gvDetails, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
            End If
            If rbtnCustWise.Checked AndAlso gvCustomer.Visible = True Then ' gvCustomer.Visible 
                transportSql.applyExportTemplate(gvCustomer, PageSetupReport_ID)
                'transportSql.exportdataChilRows(gvCustomer, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvCustomer, "", Me.Text, , arrHeader)
            ElseIf rbtnCustWise.Checked AndAlso gvDetails.Visible = True Then
                transportSql.applyExportTemplate(gvDetails, PageSetupReport_ID)
                'transportSql.exportdataChilRows(gvDetails, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
            End If
            If (rbtnCustomerCategory.Checked OrElse rbtnCustomerType.Checked) AndAlso gvCustomerGroup.Visible = True Then ' gvCustomerGroup.Visible 
                transportSql.applyExportTemplate(gvCustomerGroup, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gvCustomerGroup, "", Me.Text, , arrHeader)
            End If

            If rbtnNone.Checked Then ' gvDetails.Visible 
                transportSql.applyExportTemplate(gvDetails, PageSetupReport_ID)
                'transportSql.exportdataChilRows(gvDetails, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
            End If
            If rbtnDocWise.Checked Then ' gvDetails.Visible 
                transportSql.applyExportTemplate(gvDetails, PageSetupReport_ID)
                'transportSql.exportdataChilRows(gvDetails, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
            End If

            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub QExpCSV_Click(sender As Object, e As EventArgs) Handles QExpCSV.Click
        Try
            If gvDetails Is Nothing OrElse gvDetails.RowCount <= 0 Then
                MsgBox("Grid is empty!!!")
                Exit Sub
            End If
            ExportCSV(gvDetails, True)
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
            clsCommon.MyMessageBoxShow("Data Exported successfully")
            Process.Start(filePath)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    ''ERO/22/05/18-000323 added by richa agarwal
    Private Sub TxtZone__My_Click(sender As Object, e As EventArgs) Handles TxtZone._My_Click
        strQry = "select Zone_Code as Code ,Description as Name from TSPL_ZONE_MASTER where 1=1"
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            strQry += " and TSPL_ZONE_MASTER. Zone_Code in (Select TSPL_CUSTOMER_MASTER.Zone_Code from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") )"
        Else
            If objCommonVar.ApplyLocationFilterBasedOnPermission = True Then
                strQry += " and TSPL_ZONE_MASTER. Zone_Code in (Select TSPL_CUSTOMER_MASTER.Zone_Code from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code in (" + strCusMappingByLocForCurUser + ") ) "
            End If
        End If

        TxtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneMulSel", strQry, "Code", "Name", TxtZone.arrValueMember, TxtZone.arrDispalyMember)
    End Sub

    Private Sub rbtnCustWise_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnCustWise.CheckedChanged
        Try
            If rbtnCustWise.Checked = True Then
                chkDateWise.Visible = True
                chkDateWise.Checked = False

                chkMonthWise.Visible = True
                chkMonthWise.Checked = False

            Else
                chkDateWise.Visible = False
                chkDateWise.Checked = False

                chkMonthWise.Visible = False
                chkMonthWise.Checked = False

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
            ElseIf gvCustomerGroup.Visible = True Then
                transportSql.applyExportTemplate(gvCustomerGroup, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gvCustomerGroup, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            ElseIf gvDetails.Visible = True Then
                transportSql.applyExportTemplate(gvDetails, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gvDetails, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BulkCSV_Click(sender As Object, e As EventArgs) Handles BulkCSV.Click
        btnrefresh = True
        ' print()
        GC.Collect()
        If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
            PrintForCustomerCurrency(1)
        Else
            print(1)
        End If
    End Sub

    Private Sub BulkExcel_Click(sender As Object, e As EventArgs) Handles BulkExcel.Click
        btnrefresh = True
        ' print()
        GC.Collect()
        If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
            PrintForCustomerCurrency(2)
        Else
            print(2)
        End If
    End Sub

    Private Function GetReportID() As String
        'If clsCommon.myLen(ReportID) > 0 Then
        If clsCommon.CompairString(FormType, clsUserMgtCode.MISDebtorReport) = CompairStringResult.Equal Then
            ReportID = "MISDebtorReport"
        Else
            ReportID = "CustomerLedgerReport"
        End If
        If gvCustomer.Visible = True Then
            ReportID = ReportID + "C"
            TemplateGridview = gvCustomer
        ElseIf gvCustomerGroup.Visible = True Then
            If rbtnCustomerCategory.Checked = True Then
                ReportID = ReportID + "CC"
            ElseIf rbtnCustomerType.Checked = True Then
                ReportID = ReportID + "CT"
            Else
                ReportID = ReportID + "CG"
            End If

            TemplateGridview = gvCustomerGroup
        ElseIf gvDetails.Visible = True Then
            ReportID = ReportID + "D"
            TemplateGridview = gvDetails
        End If
        'End If
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

            If rbtnCustGroupWise.Checked AndAlso gvCustomerGroup.Visible = True Then ' gvCustomerGroup.Visible 
                clsCommon.MyExportToExcelGrid(Me.Text, gvCustomerGroup, arrHeader, Me.Text, True)
                'transportSql.QuickExportToExcel(gvCustomerGroup, "", Me.Text, , arrHeader)
            ElseIf rbtnCustGroupWise.Checked AndAlso gvCustomer.Visible = True Then
                clsCommon.MyExportToExcelGrid(Me.Text, gvCustomer, arrHeader, Me.Text, True)
                ' transportSql.QuickExportToExcel(gvCustomer, "", Me.Text, , arrHeader)
            ElseIf rbtnCustGroupWise.Checked AndAlso gvDetails.Visible = True Then
                clsCommon.MyExportToExcelGrid(Me.Text, gvDetails, arrHeader, Me.Text, True)
                'transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
            End If
            If rbtnCustWise.Checked AndAlso gvCustomer.Visible = True Then ' gvCustomer.Visible 
                clsCommon.MyExportToExcelGrid(Me.Text, gvCustomer, arrHeader, Me.Text, True)
                'transportSql.QuickExportToExcel(gvCustomer, "", Me.Text, , arrHeader)
            ElseIf rbtnCustWise.Checked AndAlso gvDetails.Visible = True Then
                clsCommon.MyExportToExcelGrid(Me.Text, gvDetails, arrHeader, Me.Text, True)
                ' transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
            End If
            If rbtnNone.Checked Then ' gvDetails.Visible 
                clsCommon.MyExportToExcelGrid(Me.Text, gvDetails, arrHeader, Me.Text, True)
                'transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
            End If
            If rbtnDocWise.Checked Then ' gvDetails.Visible 
                clsCommon.MyExportToExcelGrid(Me.Text, gvDetails, arrHeader, Me.Text, True)
                'transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
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
            ElseIf gvCustomerGroup.Visible = True Then
                pdfExporter = New ExportToPDF(gvCustomerGroup)
            ElseIf gvDetails.Visible = True Then
                pdfExporter = New ExportToPDF(gvDetails)
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
    ''richa ERO/23/12/19-001161
    Private Sub chkDateWise_CheckStateChanged(sender As Object, e As EventArgs) Handles chkDateWise.CheckStateChanged
        Try
            If rbtnCustWise.Checked = True Then
                If chkDateWise.Checked = True Then
                    chkMonthWise.Checked = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkMonthWise_CheckStateChanged(sender As Object, e As EventArgs) Handles chkMonthWise.CheckStateChanged
        Try
            If rbtnCustWise.Checked = True Then
                If chkMonthWise.Checked = True Then
                    chkDateWise.Checked = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkTurnOver_CheckStateChanged(sender As Object, e As EventArgs) Handles chkTurnOver.CheckStateChanged
        Try
            If chkTurnOver.Checked = True Then
                Dim FinancialYear As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT CASE WHEN DatePart(Month, GETDATE ()) >= 4 THEN DatePart(Year, GETDATE ()) + 1 ELSE DatePart(Year,GETDATE ()) END AS Fiscal_Year"))
                Dim strStartDate As Date = "01/Apr/" + clsCommon.myCstr(clsCommon.myCdbl(FinancialYear - 1))

                txtFromDate.Value = clsCommon.myCDate(strStartDate, "dd/MMM/yyyy")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrintConfirmation_Click(sender As Object, e As EventArgs) Handles btnPrintConfirmation.Click
        isExportToExcel = False
        GC.Collect()
        If rbtnNone.Checked Then
            If clsCommon.CompairString(ddlCurrencyType.SelectedValue, "1") = CompairStringResult.Equal Then
                PrintForCustomerCurrency()
            Else
                print(0, True)
            End If
        Else
            clsCommon.MyMessageBoxShow("Type should be Document Wise", Me.Text)
        End If

        ' print()
    End Sub
End Class
