Imports common
Imports System.Data.SqlClient
'=====update by preeti Against ticket no[ERO/03/04/19-000540]
Public Class FrmBankBookDayWise
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim StrPermission As String
    Private Sub FrmBankBookDayWise_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ddlBankType.SelectedIndex = 0
        LoadBanks()
        SetUserMgmtNew()
        LoadLocation()

        chkBanksAll.IsChecked = True
        chkLocAll.IsChecked = True
        dtFrm.Value = System.DateTime.Now.Date
        dtTo.Value = System.DateTime.Now.Date

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        chkExcludeProvisionBank.Visible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowCheckExcludeProvisionBank, clsFixedParameterCode.ShowCheckExcludeProvisionBank, Nothing)) = 1, True, False)
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmBankBookDayWise)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Sub LoadBanks()
        Dim qry As String = ""
        If ddlBankType.Text = "Bank" Then
            qry = " select BANK_CODE as [Bank Code],DESCRIPTION  from TSPL_BANK_MASTER Where Bank_type='B'"
        ElseIf ddlBankType.Text = "Cash" Then
            qry = " select BANK_CODE as [Bank Code],DESCRIPTION  from TSPL_BANK_MASTER Where Bank_type ='C'"
        ElseIf ddlBankType.Text = "Petty Cash" Then
            qry = " select BANK_CODE as [Bank Code],DESCRIPTION  from TSPL_BANK_MASTER Where Bank_type ='P'"
        ElseIf ddlBankType.Text = "Settlement" Then
            qry = " select BANK_CODE as [Bank Code],DESCRIPTION  from TSPL_BANK_MASTER Where Bank_type ='S'"
        End If
        cbgBanks.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgBanks.ValueMember = "Bank Code"
        cbgBanks.DisplayMember = "DESCRIPTION"
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

        If System.DateTime.Now.Date.Month >= 1 AndAlso System.DateTime.Now.Date.Month <= 3 Then
            dtFrm.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year - 1))
        Else
            dtFrm.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year))
        End If
        dtTo.Value = System.DateTime.Now.Date
        LoadBanks()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        LoadLocation()
        ddlBankType.SelectedIndex = 0
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        funPrint()
    End Sub

    Public Sub funPrint()
        Dim Address As String

        If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 1 Then
            Address = "(Select  MAX(TSPL_LOCATION_MASTER.Add1 + case When TSPL_LOCATION_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Add2, 103) End + Case When TSPL_LOCATION_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_LOCATION_MASTER.Add3,103) end + case When TSPL_LOCATION_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.City_Code, 103) end+ Case When TSPL_LOCATION_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_TDS_STATE_MASTER.State_Name) end +  Case When TSPL_LOCATION_MASTER.Pin_Code='' Then '' Else ', '+ Convert(Varchar,TSPL_LOCATION_MASTER.Pin_Code, 103)  end) from TSPL_LOCATION_MASTER LEFT OUTER  JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER.State=TSPL_TDS_STATE_MASTER.State_Code Where Loc_Segment_Code   in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
        Else
            Address = "(TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end) "
        End If
        If chkBanksSelect.IsChecked = True AndAlso cbgBanks.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast One Bank OR Select All", Me.Text)
            Exit Sub
        End If
        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast One Location OR Select All", Me.Text)
            Exit Sub
        End If
        Dim rptHead As String = ""
        '--- Create Heading 0f Bank According to Bank Type----
        If ddlBankType.Text = "Bank" Then
            rptHead = "Bank Book"
        ElseIf ddlBankType.Text = "Cash" Then
            rptHead = "Cash Book"
        ElseIf ddlBankType.Text = "Petty Cash" Then
            rptHead = "Petty Cash Book"
        ElseIf ddlBankType.Text = "Settlement" Then
            rptHead = " Settlement Book "
        End If

        Dim Bank As String = ""
        Dim Location As String = ""

        If chkBanksSelect.IsChecked = True AndAlso cbgBanks.CheckedValue.Count > 0 Then
            Bank = ("'" + clsCommon.GetMulcallString(cbgBanks.CheckedValue) + "'")
            Bank = Bank.Replace("'", "")
        End If

        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            Location = ("'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'")
            Location = Location.Replace("'", "")
        Else
            Location = StrPermission
            Location = Location.Replace("'", "")
        End If




        '-------- Codes End Here -----------------------------
        'Dim Qry As String = "Select * from (Select '" + rptHead + "' as rptHeading, xxx.NARR_MASTER,(xxx.NARR_DETAIL + case when ISNULL(xxx.NARR_DETAIL,'')<>'' and "
        'Qry += " ISNULL(xxx.NARR_MASTER,'')<>''  then '/' else '' end + xxx.NARR_MASTER) as NARR_DETAIL, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy") + "' as RunDate, '" + clsCommon.GetPrintDate(dtFrm.Value) + "' as Startdate, "
        'Qry += " '" + clsCommon.GetPrintDate(dtTo.Value) + "' as EndDate, TSPL_BANK_MASTER.BANK_CODE, TSPL_BANK_MASTER.Bank_type as BankType ,  TSPL_BANK_MASTER.DESCRIPTION , SOURCEDOC_NO as DocNo, SOURCEDOC_DATE as DocDate, CHEQUE_NO , case when LEN(ISNULL(CHEQUE_NO,''))>0 then CHEQUE_DATE else '' end as CHEQUE_DATE,  "
        'Qry += " case when ISNULL( SOURCE_CODE,'')<>'' then SOURCE_CODE else GL_Account_Code end as CustVendorCode,  case when ISNULL( SOURCE_CODE,'')<>'' then SOURCE_NAME else GL_Account_Name end as CustVendName, LOC_CODE, LOC_NAME , BANKGL_Account_Code , BANKGL_Account_Name, "
        'Qry += " Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else Debit_Amount end as Debit_Amount, Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount  ,  (TotDebAmt-TotCredAmt ) as BalAmt, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2," + Address + " as Add1, TSPL_COMPANY_MASTER.Comp_Name as CompName  From ("
        'Qry += " Select DISTINCT '' AS [Id], '' AS [SourceDoc_No], SOURCEDOC_DATE AS [SourceDoc_date], '' AS [Source_Code], '' AS [Source_Name], "
        'Qry += " BANK_CODE AS [Bank_Code], '' AS [Bank_Name], '' AS [Loc_Code], '' AS [Loc_Name], '' AS [BANKGL_account_Code], '' AS [BANKGL_Account_Name], "
        'Qry += " '' AS [GL_Account_Code], '' AS [GL_Account_Name], '' AS [CHEQUE_NO], '' AS [CHEQUE_DATE], '' AS [NARR_MASTER], '' AS [NARR_DETAIL], 0 AS [Debit_Amount], 0 AS [Credit_Amount], MAX(TotDebAmt ) as TotDebAmt, MAX(TotCredAmt ) as TotCredAmt  from (Select TSPL_BANK_BOOk.BANK_CODE, TSPL_BANK_BOOk.SOURCEDOC_DATE ,  (select isnull(SUM(Credit_Amount),0) from TSPL_BANK_BOOK a where  a.BANKGL_Account_Code=TSPL_BANK_BOOK.BANKGL_Account_Code and   CONVERT(date, a.sourceDoc_Date,103) < CONVERT(date,TSPL_BANK_BOOK.SOURCEDOC_DATE ,103)) as TotCredAmt, "
        'Qry += " (select isnull(SUM(Debit_Amount),0) from TSPL_BANK_BOOK b where  b.BANKGL_Account_Code=TSPL_BANK_BOOK.BANKGL_Account_Code and "
        'Qry += " CONVERT(date, b.sourceDoc_Date,103) < CONVERT(date,TSPL_BANK_BOOK.SOURCEDOC_DATE ,103)) as TotDebAmt  from TSPL_BANK_BOOk) AAA Group By AAA.BANK_CODE, AAA.SOURCEDOC_DATE "
        'Qry += " UNION All "
        'Qry += " Select Id, SOURCEDOC_NO, SOURCEDOC_DATE, SOURCE_CODE, SOURCE_NAME, BANK_CODE, BANK_NAME, LOC_CODE, LOC_NAME, BANKGL_Account_Code, BANKGL_Account_Name, GL_Account_Code, GL_Account_Name, CHEQUE_NO, CHEQUE_DATE, NARR_MASTER, NARR_DETAIL, Debit_Amount, Credit_Amount, "
        'Qry += " 0 as TotCredAmt, 0 AS  TotDebAmt  from TSPL_BANK_BOOk WHERE CONVERT(Date, SOURCEDOC_DATE, 103)>=CONVERT(date, '" + dtFrm.Value.Date + "', 103) AND  "
        'Qry += " CONVERT(date,SOURCEDOC_DATE, 103)<=CONVERT(date, '" + dtTo.Value.Date + "', 103)  "
        'Qry += " ) xxx  Left Outer Join TSPL_BANK_MASTER on xxx.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE "
        'Qry += " Left Outer Join TSPL_COMPANY_MASTER ON 'DEMO'=TSPL_COMPANY_MASTER.Comp_Code  Where 1=1 "

        Dim Qry As String = "Select '" + Bank + "' as Bank,'" + Location + "' as Location,Row_Number() OVER (PARTITION BY final.Bank_Code ORDER BY Convert(Date,DocDate, 103)) as BalRow,  * from (Select '" + rptHead + "' as rptHeading, xxx.NARR_MASTER,(xxx.NARR_DETAIL + case when ISNULL(xxx.NARR_DETAIL,'')<>'' and "
        Qry += " ISNULL(xxx.NARR_MASTER,'')<>''  then '/' else '' end + xxx.NARR_MASTER) as NARR_DETAIL, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy") + "' as RunDate, '" + clsCommon.GetPrintDate(dtFrm.Value) + "' as Startdate, "
        Qry += " '" + clsCommon.GetPrintDate(dtTo.Value) + "' as EndDate, TSPL_BANK_MASTER.BANK_CODE, TSPL_BANK_MASTER.Bank_type as BankType ,  TSPL_BANK_MASTER.DESCRIPTION , SOURCEDOC_NO as DocNo, SOURCEDOC_DATE as DocDate, CHEQUE_NO , case when LEN(ISNULL(CHEQUE_NO,''))>0 then CHEQUE_DATE else '' end as CHEQUE_DATE,  "
        Qry += " case when ISNULL( SOURCE_CODE,'')<>'' then SOURCE_CODE else GL_Account_Code end as CustVendorCode,  Case When Row=0 Then 'Openning Balance On '+' - '+CONVERT(VARCHAR,SOURCEDOC_DATE,103) Else case when ISNULL( SOURCE_CODE,'')<>'' then SOURCE_NAME else GL_Account_Name end END  as CustVendName, LOC_CODE, LOC_NAME , BANKGL_Account_Code , BANKGL_Account_Name, "
        Qry += " Case When Debit_Amount=0 AND Credit_Amount<0 Then (Credit_Amount)*-1 else Debit_Amount end as Debit_Amount, Case When Credit_Amount<0 Then 0 else Credit_Amount end Credit_Amount, Row  , TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2," + Address + " as Add1, TSPL_COMPANY_MASTER.Comp_Name as CompName, Case When Row=0 Then 0 When Row=1 AND Debit_Amount>0 Then 1 Else 2 End As [OrderDrCr], Status From ("
        Qry += " Select '' AS [Id], '' AS [SourceDoc_No], SOURCEDOC_DATE AS [SourceDoc_date], '' AS [Source_Code], '' AS [Source_Name],  "
        Qry += " BANK_CODE AS [Bank_Code], '' AS [Bank_Name], '' AS [Loc_Code], '' AS [Loc_Name], '' AS [BANKGL_account_Code], '' AS [BANKGL_Account_Name],  "
        Qry += " '' AS [GL_Account_Code], '' AS [GL_Account_Name], '' AS [CHEQUE_NO], '' AS [CHEQUE_DATE], '' AS [NARR_MASTER], '' AS [NARR_DETAIL],"
        Qry += " Case When TotDebAmt>TotCredAmt Then TotDebAmt-TotCredAmt Else 0 END AS [Debit_Amount],"
        Qry += " Case When TotCredAmt>TotDebAmt Then TotCredAmt-TotDebAmt Else 0 END AS [Credit_Amount], '' as Status, "
        Qry += " 0 as Row  from ("
        Dim NoOfDays As Integer = DateDiff(DateInterval.Day, dtFrm.Value, dtTo.Value)
        If NoOfDays >= 0 Then
            Dim StartDate As Date = clsCommon.myCDate(dtFrm.Value)
            For ii As Integer = 0 To NoOfDays
                Qry += " Select Convert(Date,'" + StartDate + "',103) as SOURCEDOC_DATE, TSPL_BANK_BOOk.BANK_CODE, SUM(Credit_Amount) as TotCredAmt, SUM(Debit_Amount) as TotDebAmt from TSPL_BANK_BOOk WHERE SOURCEDOC_DATE<'" + clsCommon.GetPrintDate(StartDate, "dd/MMM/yyyy") + "' GROUP BY BANK_CODE "
                StartDate = StartDate.AddDays(1)
                If ii <> NoOfDays Then
                    Qry += " UNION ALL "
                End If
            Next
        Else
            clsCommon.MyMessageBoxShow(Me, "To Date can not be greater than Start date", Me.Text)
        End If
        Qry += " ) AAA "
        Qry += " UNION All "
        Qry += " Select Id, SOURCEDOC_NO, SOURCEDOC_DATE, SOURCE_CODE, SOURCE_NAME, TSPL_BANK_BOOk.BANK_CODE, TSPL_BANK_BOOk.BANK_NAME, LOC_CODE, LOC_NAME, BANKGL_Account_Code, BANKGL_Account_Name, GL_Account_Code, GL_Account_Name, TSPL_BANK_BOOk.CHEQUE_NO, TSPL_BANK_BOOk.CHEQUE_DATE, NARR_MASTER, NARR_DETAIL, Debit_Amount, Credit_Amount, Case When tspl_BankReco_Detail.Reconciliation_Status='C' Then 'CLR' Else 'OS' End as Status, "
        Qry += " 1 as Row  from TSPL_BANK_BOOk LEFT OUTER JOIN tspl_BankReco_Detail ON tspl_BankReco_Detail.Document_No=TSPL_BANK_BOOk.SourceDoc_No LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id AND tspl_BankReco_Head.Post='Y' WHERE CONVERT(Date, SOURCEDOC_DATE, 103)>=CONVERT(date, '" + dtFrm.Value.Date + "', 103) AND  "
        Qry += " CONVERT(date,SOURCEDOC_DATE, 103)<=CONVERT(date, '" + dtTo.Value.Date + "', 103)  "
        Qry += " ) xxx  Left Outer Join TSPL_BANK_MASTER on xxx.BANK_CODE=TSPL_BANK_MASTER.BANK_CODE "
        Qry += " Left Outer Join TSPL_COMPANY_MASTER ON TSPL_BANK_MASTER.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  Where 1=1 "

        If ddlBankType.Text = "Bank" Then
            Qry += " And Bank_type='B' "
        ElseIf ddlBankType.Text = "Cash" Then
            Qry += " And Bank_type ='C' "
        ElseIf ddlBankType.Text = "Petty Cash" Then
            Qry += " And Bank_type ='P'"
        ElseIf ddlBankType.Text = "Settlement" Then
            Qry += " And Bank_type ='S'"
        End If

        If chkExcludeProvisionBank.Checked = True Then
            Qry += " And TSPL_BANK_MASTER.IsProvisionBank ='0'"
        End If

        If chkBanksSelect.IsChecked = True AndAlso cbgBanks.CheckedValue.Count > 0 Then
            Qry += " AND xxx.BANK_CODE in (" + clsCommon.GetMulcallString(cbgBanks.CheckedValue) + ")"
        End If
        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
            Qry += " And LOC_CODE in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        Else
            Qry += " And LOC_CODE in (" + "''," + StrPermission + ")"
        End If
        Qry += " ) Final Where CONVERT(Date, Final.DocDate , 103)>=CONVERT(date, '" + dtFrm.Value.Date + "', 103) AND  CONVERT(date,Final.DocDate , 103)<=CONVERT(date, '" + dtTo.Value.Date + "', 103) "
        Qry += " Order By Convert(Date,DocDate, 103), Row, OrderDrCr"
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            If dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                If chkSummary.Checked Then
                    frmCRV.funreport(CrystalReportFolder.CommonServices, dt, "crptBankBookDayWiseSummary", Me.Text)
                Else
                    frmCRV.funreport(CrystalReportFolder.CommonServices, dt, "crptBankBookDayWise", Me.Text)
                End If
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

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
            funPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()

        End If
    End Sub

    Private Sub ddlBankType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlBankType.SelectedIndexChanged
        LoadBanks()
    End Sub

    
 
End Class
