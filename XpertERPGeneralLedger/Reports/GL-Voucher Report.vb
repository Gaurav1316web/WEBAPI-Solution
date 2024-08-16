'****** Created By: Manoj Chauhan **********
'****** Start Date: 29/05/2011 11:30 AM    **********
'****** Table: TSPL_Journal_MASTER  & TSPL_Journal_DETAILS  ******** 
'Update by Vipin  '12-06-2012' for Source Code and Transection Type Filter
''16/06/2012---Updation by --[Pankaj kumar]-- Created New Report [Journal Voucher(Summary)] by Adding a ChekBox[Summary]---Required By---Rakesh Sir
''22/06/2012---Updation by --[Pankaj kumar]-- implement Location Filter in case of Summary---Req By---Rakesh Sir
''30/06/2012 by vipin for Addintion of Grid TAb to display report data with drill down facility


Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Collections
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports common
Imports System.IO

Public Class JrnlVoucherReport
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideMinData As Boolean = False
    Dim dtmain As New DataTable

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

#Region "Finder"
    Private Sub fndVoucherFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndVoucherFrm.Query = "SELECT TSPL_JOURNAL_MASTER.Voucher_No as [Voucher No.], TSPL_JOURNAL_MASTER.Voucher_Desc as [Description], TSPL_JOURNAL_MASTER.Voucher_Date as [Voucher Date] from TSPL_JOURNAL_MASTER "
        'fndVoucherFrm.ConnectionString = connectSql.SqlCon()
        'fndVoucherFrm.Caption = "Journal Voucher"
        'fndVoucherFrm.ValueToSelect = "Voucher No."
        'fndVoucherFrm.ValueToSelect1 = "Description"
    End Sub

    Private Sub fndVoucherTo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndVoucherTo.Query = "SELECT TSPL_JOURNAL_MASTER.Voucher_No as [Voucher No.], TSPL_JOURNAL_MASTER.Voucher_Desc as [Description], TSPL_JOURNAL_MASTER.Voucher_Date as [Voucher Date] from TSPL_JOURNAL_MASTER "
        'fndVoucherTo.ConnectionString = connectSql.SqlCon()
        'fndVoucherTo.Caption = "Journal Voucher"
        'fndVoucherTo.ValueToSelect = "Voucher No."
        'fndVoucherTo.ValueToSelect1 = "Description"
    End Sub
#End Region

#Region "Button Click"
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'LoadVoucher()
        'funPrint()
        printreport()

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub
    Sub funreset()
        'fndVoucherFrm.txtValue.Text = ""
        'fndVoucherTo.txtValue.Text = ""
        'dtFrm.Value = System.DateTime.Now.Date
        'dtTo.Value = System.DateTime.Now.Date
        'chkVoucherAll.IsChecked = True
        'LoadVoucher()


        chkVoucherAll.IsChecked = True
        'dtFrm.Value = System.DateTime.Now.Date

        If System.DateTime.Now.Date.Month >= 1 AndAlso System.DateTime.Now.Date.Month <= 3 Then
            '   dtFrm.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year - 1))
            'KUNAL > TICKET : BM00000009568 > DATE : 19-OCT-2016
            dtFrm.Value = clsCommon.myCDate(New DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, 1))
        Else
            ' dtFrm.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year))
            'KUNAL > TICKET : BM00000009568 > DATE : 19-OCT-2016
            dtFrm.Value = clsCommon.myCDate(New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1))
        End If

        dtTo.Value = System.DateTime.Now.Date


        LoadVoucher()
        LoadTransType()
        LoadSourceCode()
        chkallcode.IsChecked = True
        chktransAll.IsChecked = True
        gridvoucher.DataSource = Nothing
        gridvoucher.Columns.Clear()
        gridvoucher.Rows.Clear()
        gridvoucher.GroupDescriptors.Clear()
        gridvoucher.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
       
        Me.Close()
    End Sub
#End Region


    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmJrnlVoucher)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        'End If
        ' '' Anubhooti(3-July-2014) Added Export Permission Against BM00000003016 ''''''''
        'btnExport.Visible = MyBase.isExport
        ''      btnSave.Visible = MyBase.isModifyFlag
        ''       btnAuth.Visible = MyBase.isPostFlag
        ''        btnDelete.Visible = MyBase.isDeleteFlag
        'btnPrint.Visible = MyBase.isPrintFlag
    End Sub
#Region "Page Load"

    Private Sub JrnlVoucherReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            printreport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub

    Private Sub GL_Trans_Report_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        chkSummary.Checked = False
        lblTransType.Visible = False
        fndtransType.Visible = False
        SetUserMgmtNew()
        chkVoucherAll.IsChecked = True
        'dtFrm.Value = System.DateTime.Now.Date
        'If System.DateTime.Now.Date.Month >= 1 AndAlso System.DateTime.Now.Date.Month <= 3 Then
        '    dtFrm.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year - 1))
        'Else
        '    dtFrm.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year))
        'End If
        'dtFrm.Value = objCommonVar.CurrFiscalStartDate()
        '  dtFrm.Value = clsCommon.GETSERVERDATE.AddDays(-10).ToString()

        'KUNAL > TICKET : BM00000009568 > DATE : 19-OCT-2016
        dtFrm.Value = clsCommon.myCDate(New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1))
        dtTo.Value = clsCommon.GETSERVERDATE()
        ' dtTo.Value = System.DateTime.Now.Date.ToString()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        LoadLocation()
        'cbgLocSeg.DataSource = clsLocation.GetLocationSegments()
        chkLocAll.IsChecked = True
        isInsideMinData = True
        LoadVoucher()
        cgvtrans.DataSource = Nothing
        LoadTransType()
        LoadSourceCode()
        chkallcode.IsChecked = True
        chktransAll.IsChecked = True
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub
#End Region

#Region "Function"
    Public Sub funPrint()
        'Dim strFrmDate As String = dtFrm.Value.Date
        'Dim strToDate As String = dtTo.Value.Date
        'Dim strFrmVoucher As String = fndVoucherFrm.txtValue.Text
        'Dim strToVoucher As String = fndVoucherTo.txtValue.Text

        'funPrintReport(strFrmDate, strToDate)
        funPrintReport()

    End Sub


    ' ''Public Sub funPrintReport(ByVal FrmDate As String, ByVal ToDate As String)
    ' ''    Try
    ' ''        If fndtransType.Value = Nothing AndAlso chkSummary.Checked = True Then
    ' ''            common.clsCommon.MyMessageBoxShow("Please Select trancation Type ")
    ' ''            fndtransType.Focus()
    ' ''            Exit Sub
    ' ''        End If
    ' ''        Dim strFyear As String = FrmDate.Substring(6, 4)
    ' ''        Dim strFMnth As String = FrmDate.Substring(0, 2)
    ' ''        Dim strFDate As String = FrmDate.Substring(3, 2)
    ' ''        Dim strFrmDate As String = strFDate & "/" & strFMnth & "/" & strFyear


    ' ''        Dim strTyear As String = ToDate.Substring(6, 4)
    ' ''        Dim strTMnth As String = ToDate.Substring(0, 2)
    ' ''        Dim strTDate As String = ToDate.Substring(3, 2)
    ' ''        Dim strToDate As String = strTDate & "/" & strTMnth & "/" & strTyear

    ' ''        Dim strQuery As String
    ' ''        If chkSummary.Checked = True Then
    ' ''            strQuery = "Select '" + fndtransType.Value + "' as transType, '" + clsCommon.GetPrintDate(dtFrm.Value.Date) + "' as StartDate, '" + clsCommon.GetPrintDate(dtTo.Value.Date) + "' as Enddate, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as Printdate, Account_code, AccDesc, DAmt, CAmt, location, " & _
    ' ''    " (TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) as CompAddress, " & _
    ' ''    " TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, TSPL_COMPANY_MASTER.Comp_Name as CompName, b.Created_By, b.Modify_By   from (select Account_code,MAX(a.Account_Desc  ) as AccDesc, SUM(Debit ) as DAmt,  " & _
    ' ''    " SUM(Credit) as CAmt, MAX(a.Account_Seg_Code7) as location, MAX(Comp_Code) as Comp_Code, MAX(Created_By) as Created_By, Max(Modify_By) as Modify_By  from (select a.Account_code,a.Account_Desc, " & _
    ' ''    " case when a.Amount < 0 then (a.Amount)*(-1) else 0 end as Credit,case when a.Amount > 0 then a.Amount else 0 end as Debit,b.Posting_Date,b.Comp_Code,a.Account_Seg_Code7,  " & _
    ' ''    " b.Created_By, b.Modify_By " & _
    ' ''    " from TSPL_JOURNAL_DETAILS a, TSPL_JOURNAL_MASTER b where a.Journal_No=b.Journal_No and b.Authorized='A' AND Type='" + fndtransType.Value + "' And " & _
    ' ''    " CONVERT(Date, b.Posting_Date, 103) >=CONVERT(Date, '" + dtFrm.Value.Date + "', 103) And CONVERT(Date, b.Posting_Date, 103) <=CONVERT(Date, '" + dtTo.Value.Date + "', 103) "

    ' ''            If chkLocSelect.IsChecked = True AndAlso cbgLocSeg.CheckedValue.Count > 0 Then
    ' ''                strQuery += " AND Account_Seg_Code7 in ( " + clsCommon.GetMulcallString(cbgLocSeg.CheckedValue) + ")"
    ' ''            End If

    ' ''            If chkVouchertSelect.IsChecked = True AndAlso cbgVoucher.CheckedValue.Count > 0 Then
    ' ''                strQuery += "  and (b.Voucher_No in (" + clsCommon.GetMulcallString(cbgVoucher.CheckedValue) + "))"
    ' ''            End If

    ' ''            strQuery += " ) a   group by a.Account_code) b Left Outer Join TSPL_COMPANY_MASTER on b.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code"

    ' ''        Else
    ' ''            strQuery = " SELECT TSPL_JOURNAL_MASTER.Voucher_No, TSPL_JOURNAL_MASTER.Voucher_Date, (case when TSPL_JOURNAL_MASTER.Source_Code ='IC-AD' then TSPL_JOURNAL_MASTER.Voucher_Desc+ (case when len(TSPL_JOURNAL_MASTER .CustVend_Name)>0 then +'/'+TSPL_JOURNAL_MASTER .CustVend_Name else ''end) else TSPL_JOURNAL_MASTER .Voucher_Desc end )as Voucher_Desc, " & _
    ' ''                       "   TSPL_JOURNAL_MASTER.Source_Code, TSPL_JOURNAL_MASTER.Posting_Date, TSPL_JOURNAL_MASTER.Total_Debit_Amt, " & _
    ' ''                       "   TSPL_JOURNAL_MASTER.Total_Credit_Amt, TSPL_JOURNAL_DETAILS.Account_code, TSPL_JOURNAL_DETAILS.Account_Desc,  " & _
    ' ''                       "   TSPL_JOURNAL_DETAILS.Amount, TSPL_JOURNAL_DETAILS.Description, TSPL_JOURNAL_DETAILS.Reference,  " & _
    ' ''                       "   TSPL_JOURNAL_DETAILS.Posting_Date AS [Dtline PostDt], TSPL_JOURNAL_DETAILS.Detail_Line_No, " & _
    ' ''                       " TSPL_JOURNAL_MASTER.Created_By,TSPL_JOURNAL_MASTER.Modify_By,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,(SELECT     CASE WHEN TSPL_JOURNAL_MASTER.Authorized = 'N' THEN 'OPEN' WHEN TSPL_JOURNAL_MASTER.Authorized = 'A' THEN 'Post' END" & _
    ' ''                        "  AS Expr1) AS Status, TSPL_JOURNAL_MASTER.Remarks" & _
    ' ''                       "   FROM         TSPL_JOURNAL_MASTER INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No  " & _
    ' ''                       "  AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =TSPL_JOURNAL_MASTER.Comp_Code  where 2=2 "


    ' ''            If chkVouchertSelect.IsChecked = True Then
    ' ''                strQuery = strQuery & "  and      (TSPL_JOURNAL_MASTER.Voucher_No in (" + clsCommon.GetMulcallString(cbgVoucher.CheckedValue) + "))"

    ' ''                'If FrmVoucher <> "" And ToVoucher <> "" Then
    ' ''                '    strQuery = strQuery & "  WHERE     (TSPL_JOURNAL_MASTER.Voucher_No >= '" + FrmVoucher + "') AND (TSPL_JOURNAL_MASTER.Voucher_No <= '" + ToVoucher + "')"
    ' ''            End If

    ' ''            strQuery = strQuery & "  and     (convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)>=convert(date,'" + FrmDate + "',103)) AND (convert(date,TSPL_JOURNAL_MASTER.Voucher_Date,103)<=convert(date,'" + ToDate + "',103))"

    ' ''            If chkcodeselect.IsChecked = True Then
    ' ''                If cbgSource.CheckedValue.Count > 0 Then
    ' ''                    strQuery += " and TSPL_JOURNAL_MASTER.Source_Code in (" + clsCommon.GetMulcallString(cbgSource.CheckedValue) + ")"
    ' ''                End If
    ' ''            End If


    ' ''            If chkTypeSelect.IsChecked = True Then
    ' ''                If cgvtrans.CheckedValue.Count > 0 Then
    ' ''                    strQuery += " and TSPL_JOURNAL_MASTER.Type in (" + clsCommon.GetMulcallString(cgvtrans.CheckedValue) + ")"
    ' ''                End If
    ' ''            End If

    ' ''            strQuery = strQuery + " Order by TSPL_JOURNAL_DETAILS.Detail_Line_No"
    ' ''        End If

    ' ''        If chkSummary.Checked = True Then
    ' ''            PrintJrnlVoucher.funreport(clsDBFuncationality.GetDataTable(strQuery), "crptGLVoucherSummary", "Journal Voucher Summary")
    ' ''        Else
    ' ''            PrintJrnlVoucher.funreport(clsDBFuncationality.GetDataTable(strQuery), "crptGLVoucher", "Journal Voucher Report")
    ' ''        End If




    ' ''    Catch ex As Exception
    ' ''        common.clsCommon.MyMessageBoxShow("No Data Found", "Journal Voucher Report", MessageBoxButtons.OK)
    ' ''    End Try


    ' ''End Sub



    Public Function funPrintReport() As DataTable
        Dim dt As DataTable = Nothing
        Try
            Dim FrmDate As String = dtFrm.Value.Date
            Dim ToDate As String = dtTo.Value.Date

            Dim strFyear As String = FrmDate.Substring(6, 4)
            Dim strFMnth As String = FrmDate.Substring(0, 2)
            Dim strFDate As String = FrmDate.Substring(3, 2)
            Dim strFrmDate As String = strFDate & "/" & strFMnth & "/" & strFyear


            Dim strTyear As String = ToDate.Substring(6, 4)
            Dim strTMnth As String = ToDate.Substring(0, 2)
            Dim strTDate As String = ToDate.Substring(3, 2)
            Dim strToDate As String = strTDate & "/" & strTMnth & "/" & strTyear

            Dim strQuery As String
            Dim img As Byte() = DirectCast(clsDBFuncationality.getSingleValue("select  Logo_Img  from tspl_company_master where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'"), Byte())
            Dim img1 As Byte() = DirectCast(clsDBFuncationality.getSingleValue("select  Logo_Img2  from tspl_company_master where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'"), Byte())
            If chkSummary.Checked = True Then
                ''        strQuery = "Select Type as transType, '" + clsCommon.GetPrintDate(dtFrm.Value.Date) + "' as StartDate, '" + clsCommon.GetPrintDate(dtTo.Value.Date) + "' as Enddate, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as Printdate, Account_code, AccDesc, DAmt, CAmt, location, " & _
                ''" (TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) as CompAddress, " & _
                ''" TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, TSPL_COMPANY_MASTER.Comp_Name as CompName, b.Created_By, b.Modify_By   from (select Account_code,MAX(a.Account_Desc  ) as AccDesc, SUM(Debit ) as DAmt,  " & _
                ''" SUM(Credit) as CAmt, MAX(a.Account_Seg_Code7) as location, MAX(Comp_Code) as Comp_Code, MAX(Created_By) as Created_By, Max(Modify_By) as Modify_By ,type from (select a.Account_code,a.Account_Desc, " & _
                ''" case when a.Amount < 0 then (a.Amount)*(-1) else 0 end as Credit,case when a.Amount > 0 then a.Amount else 0 end as Debit,b.Posting_Date,b.Comp_Code,a.Account_Seg_Code7,  " & _
                ''" b.Created_By, b.Modify_By,b.Type  " & _
                ''" from TSPL_JOURNAL_DETAILS a, TSPL_JOURNAL_MASTER b where a.Journal_No=b.Journal_No and b.Authorized='A'  And " & _
                ''" CONVERT(Date, b.Posting_Date, 103) >=CONVERT(Date, '" + dtFrm.Value.Date + "', 103) And CONVERT(Date, b.Posting_Date, 103) <=CONVERT(Date, '" + dtTo.Value.Date + "', 103) "


                strQuery = "Select  '" + clsCommon.GetPrintDate(dtFrm.Value.Date) + "' as StartDate, '" + clsCommon.GetPrintDate(dtTo.Value.Date) + "' as Enddate, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as Printdate, Account_code, AccDesc, DAmt, CAmt, location, " & _
" (TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) as CompAddress, " & _
"  TSPL_COMPANY_MASTER.Comp_Name as CompName, b.Created_By, b.Modify_By   from (select Account_code,MAX(a.Account_Desc  ) as AccDesc, SUM(Debit ) as DAmt,  " & _
" SUM(Credit) as CAmt, MAX(a.Account_Seg_Code7) as location, MAX(Comp_Code) as Comp_Code, MAX(Created_By) as Created_By, Max(Modify_By) as Modify_By  from (select a.Account_code,a.Account_Desc, " & _
" case when a.Amount < 0 then (a.Amount)*(-1) else 0 end as Credit,case when a.Amount > 0 then a.Amount else 0 end as Debit,b.Posting_Date,b.Comp_Code,a.Account_Seg_Code7,  " & _
" b.Created_By, b.Modify_By  " & _
" from TSPL_JOURNAL_DETAILS a, TSPL_JOURNAL_MASTER b where a.Journal_No=b.Journal_No and b.Authorized='A'  And " & _
" CONVERT(Date, b.Posting_Date, 103) >=CONVERT(Date, '" + dtFrm.Value.Date + "', 103) And CONVERT(Date, b.Posting_Date, 103) <=CONVERT(Date, '" + dtTo.Value.Date + "', 103) "



                If chkTypeSelect.IsChecked = True AndAlso RadGroupBox2.Enabled = True Then
                    If cgvtrans.CheckedValue.Count > 0 Then
                        strQuery += " and Type in (" + clsCommon.GetMulcallString(cgvtrans.CheckedValue) + ")"
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Select atleast one Transection type", Me.Text)
                        Return Nothing
                        Exit Function

                    End If
                End If

                If chkLocSelect.IsChecked = True Then
                    If cbgLocSeg.CheckedValue.Count > 0 Then
                        strQuery += " AND Account_Seg_Code7 in ( " + clsCommon.GetMulcallString(cbgLocSeg.CheckedValue) + ")"
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Select atleast one Location Segment Code", Me.Text)
                        Return Nothing
                        Exit Function
                    End If
                End If

                If chkVouchertSelect.IsChecked = True Then
                    If cbgVoucher.CheckedValue.Count > 0 Then
                        strQuery += "  and (b.Voucher_No in (" + clsCommon.GetMulcallString(cbgVoucher.CheckedValue) + "))"
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Select atleast one Voucher", Me.Text)
                        Return Nothing
                        Exit Function
                    End If
                End If



                'If chkcodeselect.IsChecked = True Then
                '    If cbgSource.CheckedValue.Count > 0 Then
                '        strQuery += "  and (b.Voucher_No in (" + clsCommon.GetMulcallString(cbgVoucher.CheckedValue) + "))"
                '    Else
                '        common.clsCommon.MyMessageBoxShow("Select atleast one Voucher")
                '        Exit Function
                '    End If
                'End If

                strQuery += " ) a   group by a.Account_code ) b Left Outer Join TSPL_COMPANY_MASTER on b.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code"

            Else
                strQuery = " SELECT TSPL_JOURNAL_DETAILS.Detail_Line_No as [S.No], TSPL_JOURNAL_MASTER.Voucher_No,convert(varchar, TSPL_JOURNAL_MASTER.Voucher_Date,103) as Voucher_Date, (case when TSPL_JOURNAL_MASTER.Source_Code ='IC-AD' then TSPL_JOURNAL_MASTER.Voucher_Desc+ (case when len(TSPL_JOURNAL_MASTER .CustVend_Name)>0 then +'/'+TSPL_JOURNAL_MASTER .CustVend_Name else ''end) else TSPL_JOURNAL_MASTER .Voucher_Desc end )as Voucher_Desc, " & _
                         "   TSPL_JOURNAL_MASTER.Source_Code, TSPL_JOURNAL_MASTER.Posting_Date,TSPL_JOURNAL_DETAILS.Account_code, TSPL_JOURNAL_DETAILS.Account_Desc, TSPL_JOURNAL_MASTER.Total_Debit_Amt, " & _
                          "   TSPL_JOURNAL_MASTER.Total_Credit_Amt,   " & _
                          "   TSPL_JOURNAL_DETAILS.Amount,   (case when  TSPL_JOURNAL_DETAILS.Amount >=0 then TSPL_JOURNAL_DETAILS.Amount else 0 end)as DrAmt ,-1 *(case when  TSPL_JOURNAL_DETAILS.Amount <0 then TSPL_JOURNAL_DETAILS.Amount else 0 end)as CrAmt , TSPL_JOURNAL_DETAILS.Description, TSPL_JOURNAL_DETAILS.Reference,  " & _
                          "   TSPL_JOURNAL_DETAILS.Posting_Date AS [Dtline PostDt],  " & _
                          " TSPL_JOURNAL_MASTER.Created_By,TSPL_JOURNAL_MASTER.Modify_By,TSPL_COMPANY_MASTER.Comp_Name  , CASE WHEN TSPL_JOURNAL_MASTER.Authorized = 'N' THEN 'OPEN' WHEN TSPL_JOURNAL_MASTER.Authorized = 'A' THEN 'Post' END" & _
                           "   AS Status, TSPL_JOURNAL_MASTER.Remarks" & _
                          "   FROM         TSPL_JOURNAL_MASTER INNER JOIN TSPL_JOURNAL_DETAILS ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No  " & _
                          "  AND  TSPL_JOURNAL_MASTER.Voucher_No = TSPL_JOURNAL_DETAILS.Voucher_No  left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =TSPL_JOURNAL_MASTER.Comp_Code  where 2=2 "



                If chkVouchertSelect.IsChecked = True Then
                    strQuery = strQuery & "  and      (TSPL_JOURNAL_MASTER.Voucher_No in (" + clsCommon.GetMulcallString(cbgVoucher.CheckedValue) + "))"


                End If

                strQuery = strQuery & "  and     (convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)>=convert(date,'" + FrmDate + "',103)) AND (convert(date,TSPL_JOURNAL_MASTER.Voucher_Date,103)<=convert(date,'" + ToDate + "',103))"

                If chkcodeselect.IsChecked = True Then
                    If cbgSource.CheckedValue.Count > 0 Then
                        strQuery += " and TSPL_JOURNAL_MASTER.Source_Code in (" + clsCommon.GetMulcallString(cbgSource.CheckedValue) + ")"

                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Select atleast one Source Code", Me.Text)
                        Return Nothing
                        Exit Function

                    End If
                End If


                If chkTypeSelect.IsChecked = True Then
                    If cgvtrans.CheckedValue.Count > 0 Then
                        strQuery += " and TSPL_JOURNAL_MASTER.Type in (" + clsCommon.GetMulcallString(cgvtrans.CheckedValue) + ")"
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Select atleast one Transection Type", Me.Text)
                        Return Nothing
                        Exit Function
                    End If
                End If

                strQuery = "Select *, Case When Amount>=0  Then 0 else 1 END as [OrderDrCr] FROM (" + strQuery + ") XXX ORDER BY OrderDrCr, [S.No] "
            End If
            dt = clsDBFuncationality.GetDataTable(strQuery)
            dt.Columns.Add("Logo_Img", GetType(Byte()))
            dt.Columns.Add("Logo_Img2", GetType(Byte()))
            ''dt.Rows(0).Item("Logo_Img")
            'dt.Rows(0)("Logo_Img") = img
            'dt.Rows(0)("Logo_Img2") = img1

            For Each dr As DataRow In dt.Rows
                dr("Logo_Img") = img
                dr("Logo_Img2") = img1
            Next

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Journal Voucher Report", MessageBoxButtons.OK)
        End Try
        Return dt
    End Function

    Public Sub printreport()

        Try

            'Dim dt As New DataTable
            'dt = funPrintReport()

            'If dt IsNot Nothing And dt.Rows.Count >= 0 Then
            '    If chkSummary.Checked = True Then
            '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "crptGLVoucherSummary", "Journal Voucher Summary")
            '    Else
            '        frmCrystalReportViewer.funreport(CrystalReportFolder.GeneralLedger, dt, "crptGLVoucher", "Journal Voucher Report")
            '    End If
            '    refreshRecords()

            'End If




            If dtmain IsNot Nothing And dtmain.Rows.Count >= 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If chkSummary.Checked = True Then
                    frmCRV.funreport(CrystalReportFolder.GeneralLedger, dtmain, "crptGLVoucherSummary", "Journal Voucher Summary")
                Else
                    frmCRV.funreport(CrystalReportFolder.GeneralLedger, dtmain, "crptGLVoucher", "Journal Voucher Report")
                End If
                frmCRV = Nothing
                'refreshRecords()

            End If

        Catch ex As Exception

        End Try

    End Sub




#End Region
    Sub LoadVoucher()
        'Dim strqry As String = "SELECT TSPL_JOURNAL_MASTER.Voucher_No as [Voucher No.], TSPL_JOURNAL_MASTER.Voucher_Desc as [Description], TSPL_JOURNAL_MASTER.Voucher_Date as [Voucher Date] from TSPL_JOURNAL_MASTER "
        'Dim strqry As String = "SELECT TSPL_JOURNAL_MASTER.Voucher_No as [Voucher No.], TSPL_JOURNAL_MASTER.Voucher_Desc as [Description], TSPL_JOURNAL_MASTER.Voucher_Date as [Voucher Date],TSPL_JOURNAL_DETAILS.Account_Seg_Code7 as [Location] ,TSPL_JOURNAL_MASTER.Source_Code as [Source Code], TSPL_JOURNAL_MASTER.Source_Desc as [SC Description]  from TSPL_JOURNAL_MASTER  left Outer  join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No	 "

        Dim strqry As String = "SELECT Distinct TSPL_JOURNAL_MASTER.Voucher_No as [Voucher No.], TSPL_JOURNAL_MASTER.Voucher_Desc as [Description],convert(varchar, TSPL_JOURNAL_MASTER.Voucher_Date,103) as [Voucher Date],TSPL_JOURNAL_MASTER.Source_Doc_No as [Source Doc No.],TSPL_JOURNAL_DETAILS.Account_Seg_Code7 as [Location] ,TSPL_JOURNAL_MASTER.Source_Code as [Source Code], TSPL_JOURNAL_MASTER.Source_Desc as [SC Description] ,(case when TSPL_JOURNAL_MASTER.Source_Type='C' then TSPL_JOURNAL_MASTER.CustVend_Code else '' end )as Customer, (case when TSPL_JOURNAL_MASTER.Source_Type='C' then TSPL_JOURNAL_MASTER.CustVend_Name else '' end )as CustomerName,(case when TSPL_JOURNAL_MASTER.Source_Type='v' then TSPL_JOURNAL_MASTER.CustVend_Code else '' end )as Vendor,(case when TSPL_JOURNAL_MASTER.Source_Type='v' then TSPL_JOURNAL_MASTER.CustVend_Name else '' end )as VendorName ,TSPL_JOURNAL_MASTER.Type as [Transaction Type] from TSPL_JOURNAL_MASTER  left Outer  join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No  	  where TSPL_JOURNAL_MASTER.Voucher_Date >='" + clsCommon.GetPrintDate(dtFrm.Value, "dd/MMM/yyyy hh:mm tt") + "' AND TSPL_JOURNAL_MASTER.Voucher_Date<='" + clsCommon.GetPrintDate(dtTo.Value, "dd/MMM/yyyy hh:mm tt") + "' "
        ' Dim strqry As String = "SELECT Distinct TSPL_JOURNAL_MASTER.Voucher_No as [Voucher No.], TSPL_JOURNAL_MASTER.Voucher_Desc as [Description], TSPL_JOURNAL_MASTER.Voucher_Date as [Voucher Date],TSPL_JOURNAL_MASTER.Source_Doc_No as [Source Doc No.],TSPL_JOURNAL_DETAILS.Account_Seg_Code7 as [Location] , TSPL_JOURNAL_MASTER.Source_Desc as [SC Description] ,(case when TSPL_JOURNAL_MASTER.Source_Type='C' then TSPL_JOURNAL_MASTER.CustVend_Code else '' end )as Customer, (case when TSPL_JOURNAL_MASTER.Source_Type='C' then TSPL_JOURNAL_MASTER.CustVend_Name else '' end )as CustomerName,(case when TSPL_JOURNAL_MASTER_1.Source_Type='v' then TSPL_JOURNAL_MASTER_1.CustVend_Code else '' end )as Vendor,(case when TSPL_JOURNAL_MASTER_1.Source_Type='v' then TSPL_JOURNAL_MASTER_1.CustVend_Name else '' end )as VendorName  from TSPL_JOURNAL_MASTER  left Outer  join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No   left Outer  join  TSPL_JOURNAL_MASTER as TSPL_JOURNAL_MASTER_1  on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_MASTER_1.Voucher_No	  where (convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)>=convert(date,'" + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + "',103)) AND (convert(date,TSPL_JOURNAL_MASTER.Voucher_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy") + "',103)) "
        If chkcodeselect.IsChecked = True Then
            If cbgSource.CheckedValue.Count > 0 Then
                strqry += " and TSPL_JOURNAL_MASTER.Source_Code in (" + clsCommon.GetMulcallString(cbgSource.CheckedValue) + ")"
            End If
        End If

        'If chkSummary.Checked = True AndAlso RadGroupBox2.Enabled = False Then
        '    strqry += " and TSPL_JOURNAL_MASTER.Type = '" + fndtransType.Value + "'"
        'End If
        If chkTypeSelect.IsChecked = True Then
            If cgvtrans.CheckedValue.Count > 0 Then
                strqry += " and TSPL_JOURNAL_MASTER.Type in (" + clsCommon.GetMulcallString(cgvtrans.CheckedValue) + ")"
            End If
        End If

        If chkLocSelect.IsChecked = True Then
            If cbgLocSeg.CheckedValue.Count > 0 Then
                strqry += " and TSPL_JOURNAL_DETAILS.Account_Seg_Code7 in (" + clsCommon.GetMulcallString(cbgLocSeg.CheckedValue) + ")"
            End If
        End If

        cbgVoucher.DataSource = clsDBFuncationality.GetDataTable(strqry)
        cbgVoucher.ValueMember = "Voucher No."
        cbgVoucher.DisplayMember = "Description"

    End Sub


    Public Sub refreshRecords()
        Try

            Dim dt As New DataTable
            dt = funPrintReport()
            dtmain = dt
            gridvoucher.DataSource = Nothing
            gridvoucher.Columns.Clear()
            gridvoucher.Rows.Clear()
            gridvoucher.GroupDescriptors.Clear()
            gridvoucher.MasterTemplate.SummaryRowsBottom.Clear()
            If dt IsNot Nothing And dt.Rows.Count > 0 Then


                gridvoucher.DataSource = dt
                fromatgrid()
                ReStoreGridLayout()
            End If


        Catch ex As Exception
            ' common.clsCommon.MyMessageBoxShow("No Data Found", "Journal Voucher Report", MessageBoxButtons.OK)
        End Try
    End Sub

    Public Sub fromatgrid()
        Try

            gridvoucher.AllowAddNewRow = False

            If chkSummary.Checked = False Then

                gridvoucher.Columns("S.No").IsVisible = True
                gridvoucher.Columns("S.No").Width = 40
                gridvoucher.Columns("S.No").HeaderText = "S.No"
                gridvoucher.Columns("S.No").ReadOnly = True

                gridvoucher.Columns("Voucher_No").IsVisible = True
                gridvoucher.Columns("Voucher_No").Width = 100
                gridvoucher.Columns("Voucher_No").HeaderText = "Voucher No."
                gridvoucher.Columns("Voucher_No").ReadOnly = True

                gridvoucher.Columns("Voucher_Date").IsVisible = True
                gridvoucher.Columns("Voucher_Date").Width = 100
                gridvoucher.Columns("Voucher_Date").HeaderText = "Voucher Date."
                gridvoucher.Columns("Voucher_Date").ReadOnly = True

                gridvoucher.Columns("Voucher_Desc").IsVisible = False
                gridvoucher.Columns("Source_Code").IsVisible = False

                gridvoucher.Columns("Posting_Date").IsVisible = False

                gridvoucher.Columns("Total_Debit_Amt").IsVisible = False
                gridvoucher.Columns("Total_Debit_Amt").Width = 100
                gridvoucher.Columns("Total_Debit_Amt").HeaderText = "Debit Amt"

                gridvoucher.Columns("Total_Credit_Amt").IsVisible = False
                gridvoucher.Columns("Total_Credit_Amt").Width = 100
                gridvoucher.Columns("Total_Credit_Amt").HeaderText = "Credit Amt"


                gridvoucher.Columns("DrAmt").IsVisible = True
                gridvoucher.Columns("DrAmt").Width = 100
                gridvoucher.Columns("DrAmt").HeaderText = "Debit Amt"
                gridvoucher.Columns("DrAmt").ReadOnly = True


                gridvoucher.Columns("CrAmt").IsVisible = True
                gridvoucher.Columns("CrAmt").Width = 100
                gridvoucher.Columns("CrAmt").HeaderText = "Credit Amt"
                gridvoucher.Columns("CrAmt").ReadOnly = True

                gridvoucher.Columns("Account_code").IsVisible = True
                gridvoucher.Columns("Account_code").Width = 130
                gridvoucher.Columns("Account_code").HeaderText = "Account Code"
                gridvoucher.Columns("Account_code").ReadOnly = True

                gridvoucher.Columns("Account_Desc").IsVisible = True
                gridvoucher.Columns("Account_Desc").Width = 300
                gridvoucher.Columns("Account_Desc").HeaderText = "Account Description"
                gridvoucher.Columns("Account_Desc").ReadOnly = True

                gridvoucher.Columns("Amount").IsVisible = False

                gridvoucher.Columns("Description").IsVisible = False
                gridvoucher.Columns("Reference").IsVisible = False
                gridvoucher.Columns("Dtline PostDt").IsVisible = False

                gridvoucher.Columns("Created_By").IsVisible = False
                gridvoucher.Columns("Modify_By").IsVisible = False
                gridvoucher.Columns("Comp_Name").IsVisible = False
                gridvoucher.Columns("Logo_Img").IsVisible = False
                gridvoucher.Columns("Logo_Img2").IsVisible = False
                gridvoucher.Columns("Status").IsVisible = False
                gridvoucher.Columns("Remarks").IsVisible = False


                gridvoucher.GroupDescriptors.Add(New GridGroupByExpression("Voucher_No as Voucher_No format ""{0}: {1}"" Group By Voucher_No"))
                gridvoucher.MasterTemplate.ExpandAllGroups()
                gridvoucher.ShowGroupPanel = False
                gridvoucher.MasterTemplate.AutoExpandGroups = True



                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)

                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)

                gridvoucher.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gridvoucher.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            Else
                'gridvoucher.Columns("transType").IsVisible = False
                'gridvoucher.Columns("transType").Width = 100
                'gridvoucher.Columns("transType").HeaderText = "Transection Type"
                'gridvoucher.Columns("transType").ReadOnly = True

                gridvoucher.Columns("StartDate").IsVisible = False


                gridvoucher.Columns("Enddate").IsVisible = False
                gridvoucher.Columns("Printdate").IsVisible = False

                gridvoucher.Columns("Account_code").IsVisible = True
                gridvoucher.Columns("Account_code").Width = 130
                gridvoucher.Columns("Account_code").HeaderText = "Account Code"
                gridvoucher.Columns("Account_code").ReadOnly = True

                gridvoucher.Columns("AccDesc").IsVisible = True
                gridvoucher.Columns("AccDesc").Width = 300
                gridvoucher.Columns("AccDesc").HeaderText = "Account Description"
                gridvoucher.Columns("AccDesc").ReadOnly = True



                gridvoucher.Columns("DAmt").IsVisible = True
                gridvoucher.Columns("DAmt").Width = 100
                gridvoucher.Columns("DAmt").HeaderText = "Debit Amt"
                gridvoucher.Columns("DAmt").ReadOnly = True



                gridvoucher.Columns("CAmt").IsVisible = True
                gridvoucher.Columns("CAmt").Width = 100
                gridvoucher.Columns("CAmt").HeaderText = "Credit Amt"
                gridvoucher.Columns("CAmt").ReadOnly = True




                gridvoucher.Columns("location").IsVisible = False

                gridvoucher.Columns("CompAddress").IsVisible = False
                gridvoucher.Columns("Logo_Img").IsVisible = False
                gridvoucher.Columns("Logo_Img2").IsVisible = False
                gridvoucher.Columns("CompName").IsVisible = False
                gridvoucher.Columns("Created_By").IsVisible = False
                gridvoucher.Columns("Modify_By").IsVisible = False



                'gridvoucher.GroupDescriptors.Add(New GridGroupByExpression("transType as transType format ""{0}: {1}"" Group By transType"))
                'gridvoucher.MasterTemplate.ExpandAllGroups()
                'gridvoucher.ShowGroupPanel = False
                'gridvoucher.MasterTemplate.AutoExpandGroups = True



                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("DAmt", "{0:F2}", GridAggregateFunction.Sum)

                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("CAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)

                gridvoucher.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gridvoucher.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If



        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub



    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "JRN-VOUCHER"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function


    Private Sub chkVoucherAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVoucherAll.ToggleStateChanged, chkVouchertSelect.ToggleStateChanged
        cbgVoucher.Enabled = chkVouchertSelect.IsChecked
    End Sub

    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cbgVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbgVoucher.Load

    End Sub

    Private Sub dtFrm_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtFrm.ValueChanged
        If isInsideMinData = True Then
            LoadVoucher()
        End If
    End Sub

    Private Sub dtTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtTo.ValueChanged
        If isInsideMinData = True Then
            LoadVoucher()
        End If
    End Sub

    Sub LoadSourceCode()

        Dim strquery As String = "select distinct Source_Code,Source_Desc as Description  from TSPL_JOURNAL_MASTER"
        cbgSource.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgSource.ValueMember = "Source_Code"
        cbgSource.DisplayMember = "Description"

    End Sub



    Sub LoadTransTypeLeave()

        cgvtrans.DataSource = Nothing
        Dim strquery As String = "select distinct Type  from TSPL_JOURNAL_MASTER where Source_Code in (" + clsCommon.GetMulcallString(cbgSource.CheckedValue) + ")  and Type <> ''"
        cgvtrans.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cgvtrans.ValueMember = "Type"
        cgvtrans.DisplayMember = "Type"

    End Sub



    Sub LoadTransType()
        cgvtrans.DataSource = Nothing
        Dim strquery As String = "select distinct Type  from TSPL_JOURNAL_MASTER where Type <>''"

        'If chkTypeSelect.IsChecked = True Then
        '    If cgvtrans.CheckedValue.Count > 0 Then
        '        strquery += " and Type in (" + clsCommon.GetMulcallString(cgvtrans.CheckedValue) + ")"
        '    End If
        'End If


        If chkcodeselect.IsChecked = True Then
            strquery += " and Source_Code in (" + clsCommon.GetMulcallString(cbgSource.CheckedValue) + ")"
        End If


        cgvtrans.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cgvtrans.ValueMember = "Type"
        cgvtrans.DisplayMember = "Type"

    End Sub

    Private Sub cbgSource_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbgSource.Click

    End Sub

    Private Sub cbgSource_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbgSource.Leave

        LoadTransType()
        LoadVoucher()
        chktransAll.IsChecked = True
        chkVoucherAll.IsChecked = True
    End Sub

    Private Sub cgvtrans_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cgvtrans.Leave
        LoadVoucher()
        chkVoucherAll.IsChecked = True
        'chktransAll.IsChecked = True
    End Sub

    Private Sub chkallcode_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkallcode.ToggleStateChanged
        cbgSource.Enabled = Not chkallcode.IsChecked
    End Sub

    Private Sub chktransAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktransAll.ToggleStateChanged
        cgvtrans.Enabled = Not chktransAll.IsChecked
    End Sub

    Sub LoadLocation()
        Dim qry As String = "select Segment_code as Code, Description from TSPL_GL_SEGMENT_CODE where Seg_No='7'"
        cbgLocSeg.DataSource = clsLocation.GetLocationSegments()
        cbgLocSeg.ValueMember = "Code"
        cbgLocSeg.DisplayMember = "Name"
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocSeg.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocSeg.Enabled = True
    End Sub

    'Private Sub fndtransType__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndtransType._MYValidating
    '    Dim strquery As String = "select distinct Type  from TSPL_JOURNAL_MASTER "
    '    Dim strwhr As String
    '    If chkcodeselect.IsChecked = True Then
    '        strwhr = "where Type <> '' and Source_Code in (" + clsCommon.GetMulcallString(cbgSource.CheckedValue) + ")"
    '    End If
    '    fndtransType.Value = clsCommon.ShowSelectForm("Trans Type", strquery, "Type", strwhr, fndtransType.Value, "Type", isButtonClicked)
    '    LoadVoucher()
    'End Sub

    'Private Sub chkSummary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSummary.ToggleStateChanged
    '    If chkSummary.Checked = True Then
    '        RadGroupBox2.Enabled = False
    '        fndtransType.Visible = True
    '        lblTransType.Visible = True
    '    Else
    '        RadGroupBox2.Enabled = True
    '        fndtransType.Visible = False
    '        lblTransType.Visible = False
    '    End If
    '    LoadVoucher()
    'End Sub

    Private Sub cbgSource_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbgSource.Load

    End Sub



    Private Sub Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Refreshbtn.Click
        GetReportGridID()
        gridvoucher.EnableFiltering = True
        PageSetupReport_ID = MyBase.Form_ID + IIf(chkSummary.Checked = True, "S", "")
        TemplateGridview = gridvoucher
        refreshRecords()
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If chkSummary.Checked = True Then
            VarID += "_S"
        Else
            VarID += "_SH"
        End If

        gridvoucher.VarID = VarID
    End Sub
    Private Sub gridvoucher_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gridvoucher.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub gridvoucher_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridvoucher.DoubleClick
        Try


            If gridvoucher.CurrentRow IsNot Nothing AndAlso chkSummary.Checked = False Then
                Dim strcode As String = clsCommon.myCstr(gridvoucher.CurrentRow.Cells("Voucher_No").Value)

                If clsCommon.myLen(strcode) > 0 Then
                    Dim frm As New frmJournalEntry(userCode, companyCode, strcode)
                    frm.SetUserMgmt(clsUserMgtCode.journalEntry)
                    frm.Show()
                End If


            Else
                Dim AccCode As String = clsCommon.myCstr(gridvoucher.CurrentRow.Cells("Account_code").Value)
                If clsCommon.myLen(AccCode) > 0 Then
                    Dim frm As New GLTransReport(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                    frm.SetUserMgmt(clsUserMgtCode.frmGLTransReport)
                    frm.strPrevFormACode = AccCode
                    frm.dTPrevFormFromDate = dtFrm.Value
                    frm.dTPrevFormToDate = dtTo.Value
                    frm.arrLocSeg = New ArrayList()
                    frm.arrAcc = New ArrayList()
                    frm.arrAcc.Add(AccCode)
                    frm.arrvoucher = New ArrayList()
                    If chkVouchertSelect.IsChecked = True And cbgVoucher.CheckedValue.Count > 0 Then
                        frm.arrvoucher = cbgVoucher.CheckedValue
                    Else
                        frm.arrvoucher = cbgVoucher.AllValue
                    End If
                    frm.arrvehicle = New ArrayList()
                    frm.arrDept = New ArrayList()
                    frm.arrEmp = New ArrayList()
                    frm.arrMachine = New ArrayList()
                    frm.arrVisi = New ArrayList()
                    frm.Show()
                End If

            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub cbgLocSeg_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbgLocSeg.Leave
        LoadVoucher()
        chkVoucherAll.IsChecked = True
    End Sub



    Private Sub gridvoucher_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gridvoucher.CellDoubleClick
        If chkSummary.Checked = False Then
            If clsCommon.myLen(gridvoucher.CurrentRow.Cells("Voucher_No").Value) > 0 Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, gridvoucher.CurrentRow.Cells("Voucher_No").Value)
            End If

        End If

    End Sub


    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmJrnlVoucher & "'"))
         
            If chkcodeselect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgSource.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Source Code : " + strtemp)
            End If

            If chkTypeSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cgvtrans.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Transection Type : " + strtemp)
            End If

            If chkLocSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocSeg.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location : " + strtemp)
            End If

            If chkVouchertSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgVoucher.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("select Voucher : " + strtemp)
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
            'transportSql.exportdataChilRows(gridvoucher, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gridvoucher, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub Export(ByVal IsPrint As EnumExportTo)
        Try
            refreshRecords()
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtFrm.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtTo.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If chkcodeselect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgSource.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Source Code : " + strtemp)
            End If

            If chkTypeSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cgvtrans.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Transection Type : " + strtemp)
            End If

            If chkLocSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocSeg.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location : " + strtemp)
            End If

            If chkVouchertSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgVoucher.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("select Voucher : " + strtemp)
            End If

            If IsPrint = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gridvoucher, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Journal Voucher", gridvoucher, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(gridvoucher, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Journal Voucher", gridvoucher, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gridvoucher.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gridvoucher.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gridvoucher.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gridvoucher.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gridvoucher.Columns.Count - 1 Step ii + 1
                        gridvoucher.Columns(ii).IsVisible = False
                        gridvoucher.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gridvoucher.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
End Class
