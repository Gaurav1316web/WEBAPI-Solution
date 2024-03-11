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

Public Class frmRptDayWiseJournalBook
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim dt As DataTable = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub
    '------------------------------------------Ticket No:BM00000000870----------------------------------"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.mbtnJournalBook)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '' Anubhooti(3-July-2014) Added Export Permission Against BM00000003016 ''''''''
        RadSplitButton1.Visible = MyBase.isExport
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub GLTransReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        chkSummary.IsChecked = True
        SetUserMgmtNew()
        'txtFromDate.Value = clsCommon.GETSERVERDATE()

        'KUNAL > TICKET : BM00000009568 > DATE : 19-OCT-2016
        If System.DateTime.Now.Date.Month >= 1 AndAlso System.DateTime.Now.Date.Month <= 3 Then
            ' txtFromDate.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year - 1))
            txtFromDate.Value = clsCommon.myCDate(New DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, 1))
        Else
            'txtFromDate.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year))
            txtFromDate.Value = clsCommon.myCDate(New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1))
        End If


        txtToDate.Value = clsCommon.GETSERVERDATE()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        LoadVouchers()
        LoadLocation()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        chkVoucherAll.IsChecked = True
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")

    End Sub

    Private Sub LoadVouchers()
        '' richa agarwal 11 april,2016 changes in query because it takes too much time in processing
        'Dim strqry As String = "SELECT Distinct TSPL_JOURNAL_MASTER.Voucher_No as [Voucher No.], TSPL_JOURNAL_MASTER.Voucher_Desc as [Description], convert(varchar,TSPL_JOURNAL_MASTER.Voucher_Date,103) as [Voucher Date],TSPL_JOURNAL_MASTER.Source_Doc_No as [Source Doc No.],TSPL_JOURNAL_DETAILS.Account_Seg_Code7 as [Location] ,TSPL_JOURNAL_MASTER.Source_Code as [Source Code], TSPL_JOURNAL_MASTER.Source_Desc as [SC Description] ,(case when TSPL_JOURNAL_MASTER.Source_Type='C' then TSPL_JOURNAL_MASTER.CustVend_Code else '' end )as Customer, (case when TSPL_JOURNAL_MASTER.Source_Type='C' then TSPL_JOURNAL_MASTER.CustVend_Name else '' end )as CustomerName,(case when TSPL_JOURNAL_MASTER_1.Source_Type='v' then TSPL_JOURNAL_MASTER_1.CustVend_Code else '' end )as Vendor,(case when TSPL_JOURNAL_MASTER_1.Source_Type='v' then TSPL_JOURNAL_MASTER_1.CustVend_Name else '' end )as VendorName from TSPL_JOURNAL_MASTER  left Outer  join TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No   left Outer  join  TSPL_JOURNAL_MASTER as TSPL_JOURNAL_MASTER_1  on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_MASTER_1.Voucher_No	  where (convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)>=convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "',103)) AND (convert(date,TSPL_JOURNAL_MASTER.Voucher_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "',103)) "
        Dim strqry As String = "SELECT TSPL_JOURNAL_MASTER.Voucher_No as [Voucher No.], TSPL_JOURNAL_MASTER.Voucher_Desc as [Description]," & _
        " convert(varchar,TSPL_JOURNAL_MASTER.Voucher_Date,103) as [Voucher Date],TSPL_JOURNAL_MASTER.Source_Doc_No as [Source Doc No.]," & _
        " TSPL_JOURNAL_MASTER.Segment_code  as [Location] ,TSPL_JOURNAL_MASTER.Source_Code as [Source Code], TSPL_JOURNAL_MASTER.Source_Desc as [SC Description] ," & _
        " (case when TSPL_JOURNAL_MASTER.Source_Type='C' then TSPL_JOURNAL_MASTER.CustVend_Code else '' end )as Customer, " & _
        " (case when TSPL_JOURNAL_MASTER.Source_Type='C' then TSPL_JOURNAL_MASTER.CustVend_Name else '' end )as CustomerName," & _
        " (case when TSPL_JOURNAL_MASTER.Source_Type='V' then TSPL_JOURNAL_MASTER.CustVend_Code else '' end )as Vendor," & _
        " (case when TSPL_JOURNAL_MASTER.Source_Type='V' then TSPL_JOURNAL_MASTER.CustVend_Name else '' end )as VendorName from TSPL_JOURNAL_MASTER  " & _
        " where TSPL_JOURNAL_MASTER.Voucher_Date >= '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy hh:mm:ss tt") + "'  AND  TSPL_JOURNAL_MASTER.Voucher_Date <= '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy hh:mm:ss tt") + "' "
        cbgVoucher.DataSource = clsDBFuncationality.GetDataTable(strqry)
        cbgVoucher.ValueMember = "Voucher No."
        cbgVoucher.DisplayMember = "Description"
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Sub PrintData()
        Dim frmCRV As New frmCrystalReportViewer()
        If chkSummary.IsChecked = True Then
            frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "DayWiseJrnlBookSummary", "Journal Book Summary")
        Else
            frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "DayWiseJrnlBook", "Journal Book")
        End If
        frmCRV = Nothing
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub
    Sub funreset()
        Try
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            dt = Nothing
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            EnableDisableControls(True)
            GC.Collect()
            'KUNAL > TICKET : BM00000009568 > DATE : 19-OCT-2016
            txtFromDate.Value = clsCommon.myCDate(New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "GL-JB-RPT"
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

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(chkSummary.IsChecked = True, "S", "D")
        TemplateGridview = gv1
        LoadData()
    End Sub

    Private Sub EnableDisableControls(ByVal Val As Boolean)
        txtFromDate.Enabled = Val
        txtToDate.Enabled = Val
        grpVoucher.Enabled = Val
    End Sub

    Private Sub LoadData()
        Try
            Dim Address As String

            Dim location As String = ""
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                location = ("'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'")
                location = location.Replace("'", "")
            End If

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 1 Then
                Address = "(Select  MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_TDS_STATE_MASTER.State_Name) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end) from TSPL_LOCATION_MASTER LEFT OUTER  JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER.State=TSPL_TDS_STATE_MASTER.State_Code Where Loc_Segment_Code   in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            Else
                ''RICHA AGARWAL
                ' Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "
                Address = "(TSPL_COMPANY_MASTER.Add1 + case When ISNULL(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+ TSPL_COMPANY_MASTER.Add2 End + Case When ISNULL(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Add3 end + case When ISNULL(TSPL_COMPANY_MASTER.City_Code,'') ='' then '' else ', '+ TSPL_COMPANY_MASTER.City_Code end+ Case When ISNULL(TSPL_COMPANY_MASTER.State,'')='' Then '' else ', '+ TSPL_COMPANY_MASTER.State end +  Case When ISNULL(TSPL_COMPANY_MASTER.Pincode,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Pincode end) "
            End If
            If chkVouchertSelect.IsChecked AndAlso cbgVoucher.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Atleast Single Voucher Or select All ", Me.Text)
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Atleast Single Location Or select All ", Me.Text)
                Exit Sub
            End If
            Dim RunDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
            Dim DtFrm As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy")
            Dim DtTo As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            Dim qry As String
            If chkSummary.IsChecked = True Then
                'qry = " select '" + location + "' as location,'" + DtFrm + "' as FinderFromDate,'" + DtTo + "' as FinderToDate, '" + RunDate + "' as RunDate, TSPL_GL_ROLLUP.Account_Code as RollUpAccCode , TSPL_GL_ROLLUP.Description as RollUpAccDesc, BBB.Account_code , Account_Desc,  DrAmt,  CrAmt, Compname, CompAddress, Case When DrAmt>0 Then 0 else 1 end as [OrderCrCr]   from  ( " & _
                '    " Select  Account_code, MAX(Account_Desc) as Account_Desc, (case When SUM(DrAmt)>=SUM(CrAmt) Then SUM(DrAmt)-SUM(CrAmt) Else  0 End ) AS DrAmt, (case when SUM(CrAmt)>SUM(DrAmt) Then SUM(CrAmt)-SUM(DrAmt) else 0 End) as CrAmt, MAX(Comp_Name) as Compname, MAX(compaaddress) as CompAddress   from ( " & _
                '    " select   CAST( convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)as Varchar(11)) as OrderDate, convert(varchar, TSPL_JOURNAL_MASTER.Voucher_Date,103) as Voucher_Date,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_DETAILS.Account_code,Account_Desc, Voucher_Desc, " & _
                '    " case when Amount>0 then Amount else 0 end as DrAmt,case when Amount>0 then 0 else -1*Amount end as CrAmt ,TSPL_COMPANY_MASTER .Comp_Name, " & _
                '    " " + Address + " as compaaddress " & _
                '    " from TSPL_JOURNAL_DETAILS  " & _
                '    " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " & _
                '    " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "' " & _
                '    " where(2 = 2) "
                'If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                '    qry += " and TSPL_JOURNAL_DETAILS.Account_Seg_Code7 in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                'End If
                'If chkVouchertSelect.IsChecked Then
                '    qry += " and TSPL_JOURNAL_MASTER.Voucher_No in (" + clsCommon.GetMulcallString(cbgVoucher.CheckedValue) + ")"
                'End If
                'qry += " and convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)>=convert(date,'" + DtFrm + "',103) and convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)<=convert(date,'" + DtTo + "',103) and TSPL_JOURNAL_MASTER.Authorized='A' " & _
                '    " ) AAA Group By Account_code " & _
                '    " )BBB left outer join TSPL_GL_ROLLUP on TSPL_GL_ROLLUP.account=BBB.Account_code ORDER BY OrderCrCr"

                qry = " select '" + location + "' as location,'" + DtFrm + "' as FinderFromDate,'" + DtTo + "' as FinderToDate, '" + RunDate + "' as RunDate, TSPL_GL_ROLLUP.Account_Code as RollUpAccCode , TSPL_GL_ROLLUP.Description as RollUpAccDesc, BBB.Account_code , Account_Desc,  DrAmt,  CrAmt," & _
                    " TSPL_COMPANY_MASTER .Comp_Name,  " + Address + " as compaaddress ," & _
                    " Case When DrAmt>0 Then 0 else 1 end as [OrderCrCr]   from  ( " & _
                   " Select  Account_code, MAX(Account_Desc) as Account_Desc, (case When SUM(DrAmt)>=SUM(CrAmt) Then SUM(DrAmt)-SUM(CrAmt) Else  0 End ) AS DrAmt, (case when SUM(CrAmt)>SUM(DrAmt) Then SUM(CrAmt)-SUM(DrAmt) else 0 End) as CrAmt   from ( " & _
                   " select  TSPL_JOURNAL_MASTER.Voucher_Date  as OrderDate,  TSPL_JOURNAL_MASTER.Voucher_Date as Voucher_Date,TSPL_JOURNAL_MASTER.Voucher_No,TSPL_JOURNAL_DETAILS.Account_code,Account_Desc, Voucher_Desc, " & _
                   " case when Amount>0 then Amount else 0 end as DrAmt,case when Amount>0 then 0 else -1*Amount end as CrAmt " & _
                   " from TSPL_JOURNAL_DETAILS  " & _
                   " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No " & _
                   " where (2 = 2) "
                If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_JOURNAL_DETAILS.Account_Seg_Code7 in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
                If chkVouchertSelect.IsChecked Then
                    qry += " and TSPL_JOURNAL_MASTER.Voucher_No in (" + clsCommon.GetMulcallString(cbgVoucher.CheckedValue) + ")"
                End If
                qry += " and TSPL_JOURNAL_MASTER.Voucher_Date >='" + clsCommon.GetPrintDate(DtFrm, "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_JOURNAL_MASTER.Voucher_Date<='" + clsCommon.GetPrintDate(DtTo, "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_JOURNAL_MASTER.Authorized='A' " & _
                    " ) AAA Group By Account_code " & _
                    " )BBB left outer join TSPL_GL_ROLLUP on TSPL_GL_ROLLUP.account=BBB.Account_code " & _
                    " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "' ORDER BY OrderCrCr"

            Else
                'qry = " select  '" + location + "' as location,'" + DtFrm + "' as FinderFromDate,'" + DtTo + "' as FinderToDate, CAST( convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)as Varchar(11)) as OrderDate,convert(varchar,TSPL_JOURNAL_MASTER.Voucher_Date,103) as Voucher_Date,TSPL_JOURNAL_MASTER.Voucher_No,Account_code,Account_Desc, Voucher_Desc,case when Amount>0 then Amount else 0 end as DrAmt,case when Amount>0 then 0 else -1*Amount end as CrAmt ,TSPL_COMPANY_MASTER .Comp_Name,(TSPL_COMPANY_MASTER .Add1+TSPL_COMPANY_MASTER .Add2+TSPL_COMPANY_MASTER.Add3+','+TSPL_COMPANY_MASTER .State+'-'+TSPL_COMPANY_MASTER .Pincode)as compaaddress, Case When Amount>0 Then 0 else 1 end as [OrderDrCr]" + Environment.NewLine
                'qry += " from TSPL_JOURNAL_DETAILS " + Environment.NewLine
                'qry += "left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine
                'qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "'" + Environment.NewLine
                'qry += " where 2=2 " + Environment.NewLine
                'If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                '    qry += " and TSPL_JOURNAL_DETAILS.Account_Seg_Code7 in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                'End If
                'If chkVouchertSelect.IsChecked Then
                '    qry += " and TSPL_JOURNAL_MASTER.Voucher_No in (" + clsCommon.GetMulcallString(cbgVoucher.CheckedValue) + ")" + Environment.NewLine
                'Else
                '    qry += " and convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)>=convert(date,'" + DtFrm + "',103) and convert(date,TSPL_JOURNAL_MASTER.Voucher_Date ,103)<=convert(date,'" + DtTo + "',103) and TSPL_JOURNAL_MASTER.Authorized='A' " + Environment.NewLine

                'End If
                'qry += " order by OrderDrCr, OrderDate, Voucher_No"

                qry = " select  '" + location + "' as location,'" + DtFrm + "' as FinderFromDate,'" + DtTo + "' as FinderToDate, convert(VARCHAR,TSPL_JOURNAL_MASTER.Voucher_Date ,103) as OrderDate,convert(varchar,TSPL_JOURNAL_MASTER.Voucher_Date,103) as Voucher_Date,TSPL_JOURNAL_MASTER.Voucher_No,Account_code,Account_Desc, TSPL_JOURNAL_MASTER.Voucher_Desc,TSPL_JOURNAL_MASTER.Source_Narration,TSPL_JOURNAL_MASTER.CustVend_Code ,TSPL_JOURNAL_MASTER.CustVend_Name, TSPL_JOURNAL_DETAILS.Account_Seg_Code7,tspl_location_master.Location_Desc ,case when Amount>0 then Amount else 0 end as DrAmt,case when Amount>0 then 0 else -1*Amount end as CrAmt ,TSPL_COMPANY_MASTER .Comp_Name,(TSPL_COMPANY_MASTER .Add1+TSPL_COMPANY_MASTER .Add2+TSPL_COMPANY_MASTER.Add3+','+TSPL_COMPANY_MASTER .State+'-'+TSPL_COMPANY_MASTER .Pincode)as compaaddress, Case When Amount>0 Then 0 else 1 end as [OrderDrCr]" + Environment.NewLine & _
                 " from TSPL_JOURNAL_DETAILS " + Environment.NewLine & _
                " INNER join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Voucher_No=TSPL_JOURNAL_DETAILS.Voucher_No" + Environment.NewLine & _
 " left join tspl_location_master on tspl_location_master.location_code=TSPL_JOURNAL_DETAILS.Account_Seg_Code7" + Environment.NewLine & _
                " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "'" + Environment.NewLine & _
                " where 2=2 " + Environment.NewLine
                If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_JOURNAL_DETAILS.Account_Seg_Code7 in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
                If chkVouchertSelect.IsChecked Then
                    qry += " and TSPL_JOURNAL_MASTER.Voucher_No in (" + clsCommon.GetMulcallString(cbgVoucher.CheckedValue) + ")" + Environment.NewLine
                Else
                    qry += " and TSPL_JOURNAL_MASTER.Voucher_Date>='" + clsCommon.GetPrintDate(DtFrm, "dd/MMM/yyyy hh:mm:ss tt") + "'  and TSPL_JOURNAL_MASTER.Voucher_Date<='" + clsCommon.GetPrintDate(DtTo, "dd/MMM/yyyy hh:mm:ss tt") + "'  and TSPL_JOURNAL_MASTER.Authorized='A' " + Environment.NewLine

                End If
                qry += " order by  Voucher_No"

            End If

            dt = clsDBFuncationality.GetDataTable(qry)


            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            ''richa agarwal
            '   gv1.DataSource = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
          
            ''----------
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = False
            Next
            Dim idx As Integer = 0
            If chkSummary.IsChecked = True Then

                gv1.Columns("RollUpAccCode").IsVisible = True
                gv1.Columns("RollUpAccCode").Width = 80
                gv1.Columns("RollUpAccCode").HeaderText = "RollUp Account"

                gv1.Columns("RollUpAccDesc").IsVisible = True
                gv1.Columns("RollUpAccDesc").Width = 300
                gv1.Columns("RollUpAccDesc").HeaderText = "RollUp A/C Desc"


                gv1.Columns("Account_code").IsVisible = True
                gv1.Columns("Account_code").Width = 80
                gv1.Columns("Account_code").HeaderText = "Account Code"

                gv1.Columns("Account_Desc").IsVisible = True
                gv1.Columns("Account_Desc").Width = 300
                gv1.Columns("Account_Desc").HeaderText = "Account Desciption"

                gv1.Columns("DrAmt").IsVisible = True
                gv1.Columns("DrAmt").Width = 80
                gv1.Columns("DrAmt").HeaderText = "Debit"

                gv1.Columns("CrAmt").IsVisible = True
                gv1.Columns("CrAmt").Width = 80
                gv1.Columns("CrAmt").HeaderText = "Credit"

            Else
                gv1.Columns("OrderDate").IsVisible = False
                gv1.Columns("OrderDate").Width = 100
                gv1.Columns("OrderDate").HeaderText = "Date"

                gv1.Columns("Voucher_Date").IsVisible = True
                gv1.Columns("Voucher_Date").Width = 80
                gv1.Columns("Voucher_Date").HeaderText = "Date"

                gv1.Columns("Voucher_No").IsVisible = True
                gv1.Columns("Voucher_No").Width = 80
                gv1.Columns("Voucher_No").HeaderText = "Vr No"

                gv1.Columns("Source_Narration").IsVisible = True
                gv1.Columns("Source_Narration").Width = 80
                gv1.Columns("Source_Narration").HeaderText = "Reference"

                gv1.Columns("CustVend_Code").IsVisible = True
                gv1.Columns("CustVend_Code").Width = 80
                gv1.Columns("CustVend_Code").HeaderText = "Vendor/Customer Code"

                gv1.Columns("CustVend_Name").IsVisible = True
                gv1.Columns("CustVend_Name").Width = 80
                gv1.Columns("CustVend_Name").HeaderText = "Vendor/Customer Name"

                gv1.Columns("Account_code").IsVisible = True
                gv1.Columns("Account_code").Width = 80
                gv1.Columns("Account_code").HeaderText = "Code"

                gv1.Columns("Account_Desc").IsVisible = True
                gv1.Columns("Account_Desc").Width = 200
                gv1.Columns("Account_Desc").HeaderText = "Account Desciption"

                gv1.Columns("Voucher_Desc").IsVisible = True
                gv1.Columns("Voucher_Desc").Width = 300
                gv1.Columns("Voucher_Desc").HeaderText = "Particulars/Entry description"

                gv1.Columns("Account_Seg_Code7").IsVisible = True
                gv1.Columns("Account_Seg_Code7").Width = 100
                gv1.Columns("Account_Seg_Code7").HeaderText = "Segment Code"

                gv1.Columns("Location_Desc").IsVisible = True
                gv1.Columns("Location_Desc").Width = 300
                gv1.Columns("Location_Desc").HeaderText = "Segment Name"

                gv1.Columns("DrAmt").IsVisible = True
                gv1.Columns("DrAmt").Width = 80
                gv1.Columns("DrAmt").HeaderText = "Debit"

                gv1.Columns("CrAmt").IsVisible = True
                gv1.Columns("CrAmt").Width = 80
                gv1.Columns("CrAmt").HeaderText = "Credit"

                'gv1.GroupDescriptors.Add(New GridGroupByExpression("OrderDate as OrderDate  format ""{0}: {1}"" Group By OrderDate"))
            End If

            gv1.MasterTemplate.ExpandAllGroups()
            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True
            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim item1 As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            EnableDisableControls(False)
            ReStoreGridLayout()
            qry = String.Empty
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        Finally
            ' dt = Nothing
            GC.Collect()
        End Try
    End Sub

    Sub LoadLocation()
        ' Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    
    Private Sub txtFromDate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating, txtToDate.Validating
        LoadVouchers()
    End Sub

    Private Sub chkVouchertSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVouchertSelect.ToggleStateChanged, chkVoucherAll.ToggleStateChanged
        cbgVoucher.Enabled = chkVouchertSelect.IsChecked
    End Sub

    
    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    
    Private Sub frmRptDayWiseJournalBook_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick

        If chkSummary.IsChecked = True Then
            Dim strACode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Account_code").Value)
            If clsCommon.myLen(strACode) > 0 Then
                Dim frm As New GLTransReport(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
                frm.SetUserMgmt(clsUserMgtCode.frmGLTransReport)
                frm.strPrevFormACode = strACode
                frm.strPrevFormAName = clsCommon.myCstr(gv1.CurrentRow.Cells("Account_Desc").Value)
                frm.dTPrevFormFromDate = txtFromDate.Value
                frm.dTPrevFormToDate = txtToDate.Value
                'If clsCommon.CompairString(cbgSrcCode.Text, "Trial Balance") = CompairStringResult.Equal Then
                '    frm.dTPrevFormFromDate = "01/01/2012"
                '    frm.RadLabel7.Visible = False
                '    frm.txtFromDate.Visible = False
                '    'frm.RadLabel9.Text = "As On date"
                '    frm.MyLabel2.Text = "As On date"
                'Else
                '    frm.RadLabel7.Visible = True
                '    frm.txtFromDate.Visible = True
                '    'frm.RadLabel9.Text = "To Date"
                '    frm.MyLabel2.Text = "To Date"
                'End If
                Dim i As Integer = 0

                frm.arrLocSeg = New ArrayList()
                If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    frm.arrLocSeg = cbgLocation.CheckedValue
                End If
                frm.arrAcc = New ArrayList()
                frm.arrAcc.Add(strACode)

                'If chkAccSelect.IsChecked AndAlso cbgAccount.CheckedValue.Count > 0 Then
                '    frm.arrAcc = cbgAccount.CheckedValue
                'End If
                frm.arrvoucher = New ArrayList()
                If chkVouchertSelect.IsChecked AndAlso cbgVoucher.CheckedValue.Count > 0 Then
                    frm.arrvehicle = cbgVoucher.CheckedValue
                End If
                'frm.arrDept = New ArrayList()
                'If chkDeptSelect.IsChecked AndAlso cbgDept.CheckedValue.Count > 0 Then
                '    frm.arrDept = cbgDept.CheckedValue
                'End If
                'frm.arrEmp = New ArrayList
                'If chkEmpSelect.IsChecked AndAlso cbgEmployee.CheckedValue.Count > 0 Then
                '    frm.arrEmp = cbgEmployee.CheckedValue
                'End If
                'frm.arrMachine = New ArrayList
                'If chkMachineSelect.IsChecked AndAlso cbgmachine.CheckedValue.Count > 0 Then
                '    frm.arrMachine = cbgmachine.CheckedValue
                'End If
                'frm.arrVisi = New ArrayList
                'If chkVisiSelct.IsChecked AndAlso cbgVisi.CheckedValue.Count > 0 Then
                '    frm.arrVisi = cbgVisi.CheckedValue
                'End If
                frm.Show()
           

            End If
        End If


        If chkSummary.IsChecked = False Then
            If gv1.CurrentRow IsNot Nothing Then
                Dim strVoucherNo As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Voucher_No").Value)
                If clsCommon.myLen(strVoucherNo) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.journalEntry, strVoucherNo)
                End If
            End If
        End If
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.mbtnJournalBook & "'"))
            If chkVouchertSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgVoucher.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Voucher : " + strtemp)
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
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Journal Book", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
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

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
End Class
