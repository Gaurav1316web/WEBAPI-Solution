'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
Imports common
Imports XpertERPEngine
Imports System.IO
Public Class Cash_Register_Details4
    Inherits FrmMainTranScreen


    Sub loadsalesman()
        Dim query As String = "select distinct TSPL_SALE_INVOICE_HEAD.Salesman_Code  as 'Salesman Code',TSPL_EMPLOYEE_MASTER.Emp_Name  as 'Salesman Code'  from TSPL_RECEIPT_HEADER  inner   join TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_HEADER .Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No inner join TSPL_SALE_INVOICE_HEAD on TSPL_RECEIPT_DETAIL.Document_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No  left outer join TSPL_EMPLOYEE_MASTER on TSPL_SALE_INVOICE_HEAD.Salesman_Code =TSPL_EMPLOYEE_MASTER.EMP_CODE"
        cbgsalesman.DataSource = clsDBFuncationality.GetDataTable(query)
        cbgsalesman.ValueMember = "Salesman Code"
        cbgsalesman.DisplayMember = "Salesman Name"

    End Sub



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.cash_Register_Details4)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        'End If
        ' '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        'btnExportToExcel.Visible = MyBase.isExport
        'btnQuickxport.Visible = MyBase.isExport
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub Cash_Register_Details4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E Then
            print(True)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Sub loadlocation()
        ' Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Private Sub Cash_Register_Details4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpreceiptfromdate.Value = clsCommon.GETSERVERDATE()
        dtpreceiptTodate.Value = clsCommon.GETSERVERDATE()
        loadsalesman()
        loadlocation()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()

        chksalesmanAll.IsChecked = True
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Private Sub chksalesmanAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chksalesmanAll.ToggleStateChanged, chksalesmanselect.ToggleStateChanged
        cbgsalesman.Enabled = Not chksalesmanAll.IsChecked
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        dtpreceiptfromdate.Value = clsCommon.GETSERVERDATE()
        dtpreceiptTodate.Value = clsCommon.GETSERVERDATE()
        chksalesmanAll.IsChecked = True
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(chkSummary.Checked = True, "S", "D")
        TemplateGridview = gv1
        print(False)
    End Sub
    Sub print(ByVal excel As Boolean)
        Try
            Dim post As String
            If (rdbtnPosted.IsChecked) Then
                post = "Y"
            Else
                post = "N"
            End If
            Dim fromdate As String = clsCommon.GetPrintDate(dtpreceiptfromdate.Value, "dd/MM/yyyy")
            Dim todate As String = clsCommon.GetPrintDate(dtpreceiptTodate.Value, "dd/MM/yyyy")

            Dim SalesmanFilter As String = ""
            Dim LocationFilter As String = ""
            If chksalesmanselect.IsChecked AndAlso cbgsalesman.CheckedValue.Count > 0 Then
                SalesmanFilter = clsCommon.GetMulcallString(cbgsalesman.CheckedValue)
                SalesmanFilter = SalesmanFilter.Replace("'", "")
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                LocationFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
                LocationFilter = LocationFilter.Replace("'", "")
            End If


            Dim query As String = ""
            Dim pvtQry As String = ""
            Dim strPaymentMode As String = "case when TSPL_BANK_MASTER.Bank_type ='B' then 'BANK' when TSPL_BANK_MASTER.Bank_type ='C' then 'CASH'  when TSPL_BANK_MASTER.Bank_type ='P' then 'PETTY_CASH'  when TSPL_BANK_MASTER.Bank_type ='O' then 'OTHER' when TSPL_BANK_MASTER.Bank_type ='S' then 'SETTLEMENT'  end"

            query = "select * from ( "
            If chkSummary.Checked = True Then
                '   query = "select '" + fromdate + "' as Fromdate,'" + todate + "' as Todate,'" + SalesmanFilter + "' as SalesManfilter,'" + LocationFilter + "' as LocationFilter,  TSPL_RECEIPT_HEADER. Receipt_Date , Applied_Amount ,(select max(TSPL_CUSTOMER_MASTER.Customer_Name ) from  TSPL_RECEIPT_DETAIL left outer join TSPL_SALE_INVOICE_HEAD on TSPL_RECEIPT_DETAIL.Document_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No left outer join TSPL_CUSTOMER_MASTER  on TSPL_SALE_INVOICE_HEAD.Cust_Code  =TSPL_CUSTOMER_MASTER.Cust_Code   where TSPL_RECEIPT_HEADER .Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No) as Customer_Name , Payment_Code  from TSPL_RECEIPT_HEADER  left outer join TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_HEADER .Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No where 2=2"
                query += " select Fromdate,Todate,SalesManfilter,LocationFilter,Receipt_Date,Receipt_No,Document_No,Customer_Name,Payment_Code,Applied_Amount from "
                'query = "select  TSPL_RECEIPT_HEADER. Receipt_Date ,sum(Applied_Amount)as Applied_Amount  ,max(TSPL_RECEIPT_HEADER.Payment_Code)  as Payment_Code  from TSPL_RECEIPT_HEADER  inner   join TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_HEADER .Receipt_No =TSPL_RECEIPT_DETAIL.Receipt_No inner join TSPL_SALE_INVOICE_HEAD on TSPL_RECEIPT_DETAIL.Document_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No  left outer join TSPL_EMPLOYEE_MASTER on TSPL_SALE_INVOICE_HEAD.Salesman_Code =TSPL_EMPLOYEE_MASTER.EMP_CODE  where 2=2 "
            Else
                query += " select Fromdate,Todate,SalesManfilter,LocationFilter,Receipt_Date,Receipt_No,Document_No,Customer_Name,Payment_Code,Applied_Amount from "
            End If
            query += " (select '" + fromdate + "' as Fromdate,'" + todate + "' as Todate,'" + SalesmanFilter + "' as SalesManfilter,'" + LocationFilter + "' as LocationFilter,   TSPL_RECEIPT_HEADER. Receipt_Date ,TSPL_RECEIPT_HEADER .Receipt_No,TSPL_RECEIPT_HEADER.Document_No  as Document_No, Customer_Name  as Customer_Name," & IIf(chkBankWise.Checked = True, strPaymentMode, " TSPL_RECEIPT_HEADER.Payment_Code") & " as Payment_Code, TSPL_RECEIPT_HEADER.Posted ,(case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then (TSPL_RECEIPT_HEADER.Receipt_Amount * -1) else (TSPL_RECEIPT_HEADER.Receipt_Amount) end)as Applied_Amount,TSPL_RECEIPT_HEADER.Receipt_Type,TSPL_SALE_INVOICE_HEAD.Salesman_Code ,Account_Seg_Code7 from TSPL_RECEIPT_HEADER  left outer join TSPL_RECEIPT_DETAIL on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No left outer join TSPL_SALE_INVOICE_HEAD on TSPL_RECEIPT_DETAIL.Document_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No"
            query += " left outer join TSPL_GL_ACCOUNTS on TSPL_RECEIPT_DETAIL.Account_Code =TSPL_GL_ACCOUNTS.Account_Code left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE =TSPL_RECEIPT_HEADER.Bank_Code  where TSPL_RECEIPT_HEADER.Receipt_Type<>'R' and 2=2 "
            query += " union all "
            query += " select '" + fromdate + "' as Fromdate,'" + todate + "' as Todate,'" + SalesmanFilter + "' as SalesManfilter,'" + LocationFilter + "' as LocationFilter,  TSPL_RECEIPT_HEADER.Receipt_Date, TSPL_RECEIPT_HEADER.Receipt_No ,TSPL_RECEIPT_DETAIL.Document_No ,TSPL_RECEIPT_HEADER.Customer_Name  as Customer_Name," & IIf(chkBankWise.Checked = True, strPaymentMode, " TSPL_RECEIPT_HEADER.Payment_Code") & " as Payment_Code, TSPL_RECEIPT_HEADER.Posted,Applied_Amount,TSPL_RECEIPT_HEADER.Receipt_Type,TSPL_SALE_INVOICE_HEAD.Salesman_Code,Account_Seg_Code7 from TSPL_RECEIPT_DETAIL left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No left outer join TSPL_SALE_INVOICE_HEAD on TSPL_RECEIPT_DETAIL.Document_No =TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No left outer join TSPL_GL_ACCOUNTS on TSPL_RECEIPT_DETAIL.Account_Code =TSPL_GL_ACCOUNTS.Account_Code left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE =TSPL_RECEIPT_HEADER.Bank_Code where TSPL_RECEIPT_HEADER.Receipt_Type='R' )xxx where  2=2 "

            If chkBankWise.Checked = True Then

                If (excel = True) Then
                    If chkSummary.Checked = True Then
                        pvtQry = "SELECT Receipt_Date,SUM(BANK)as [BANK],SUM(CASH ) as [CASH],SUM(PETTY_CASH ) as [PETTY_CASH],SUM(OTHER) as [OTHER],SUM(SETTLEMENT)as [SETTLEMENT] FROM  ("
                    Else
                        pvtQry = ""
                    End If
                    pvtQry += " select Receipt_Date,Receipt_No ,Document_No ,Customer_Name, Case When ISNULL(BANK,'')='' Then 0  Else Applied_Amount end as [BANK],Case When ISNULL(CASH ,'')='' Then 0  Else Applied_Amount end as [CASH],Case When ISNULL(PETTY_CASH ,'')='' Then 0  Else Applied_Amount end as [PETTY_CASH],Case When ISNULL(OTHER ,'')='' Then 0  Else Applied_Amount end as [OTHER],Case When ISNULL(SETTLEMENT ,'')='' Then 0  Else Applied_Amount end as [SETTLEMENT]   from( " & _
                    "select Receipt_Date ,Receipt_No,max(Document_No) as [Document_No]  ,max(Customer_Name) as [Customer_Name] ,BANK=MAX([BANK]),CASH=MAX([CASH]),PETTY_CASH=MAX([PETTY_CASH]),OTHER=MAX([OTHER]),SETTLEMENT=MAX([SETTLEMENT]),sum(isnull(Applied_Amount,0)) as [Applied_Amount]   from ("

                End If

            Else
                If (excel = True) Then
                    If chkSummary.Checked = True Then
                        pvtQry = "SELECT Receipt_Date,SUM(CASH)as [CASH],SUM(CHEQUE ) as [CHEQUE],SUM(ONLINE ) as [ONLINE],SUM(RTGS) as [RTGS],SUM(SETTLEMENT)as [SETTLEMENT] FROM  ("
                    Else
                        pvtQry = ""
                    End If
                    pvtQry += " select Receipt_Date,Receipt_No ,Document_No ,Customer_Name, Case When ISNULL(CASH,'')='' Then 0  Else Applied_Amount end as [CASH],Case When ISNULL(CHEQUE ,'')='' Then 0  Else Applied_Amount end as [CHEQUE],Case When ISNULL(ONLINE ,'')='' Then 0  Else Applied_Amount end as [ONLINE],Case When ISNULL(RTGS ,'')='' Then 0  Else Applied_Amount end as [RTGS],Case When ISNULL(SETTLEMENT ,'')='' Then 0  Else Applied_Amount end as [SETTLEMENT]   from( " & _
                    "select Receipt_Date ,Receipt_No,max(Document_No) as [Document_No]  ,max(Customer_Name) as [Customer_Name] ,CASH=MAX([CASH]),CHEQUE=MAX([CHEQUE]),ONLINE=MAX([ONLINE]),RTGS=MAX([RTGS]),SETTLEMENT=MAX([SETTLEMENT]),sum(isnull(Applied_Amount,0)) as [Applied_Amount]   from ("

                End If

            End If

          

            Dim dt As DataTable
            If chksalesmanselect.IsChecked AndAlso cbgsalesman.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Salesman", Me.Text)
                Return

            ElseIf chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one location", Me.Text)
                Return

            End If
            If chksalesmanselect.IsChecked AndAlso cbgsalesman.CheckedValue.Count >= 0 Then
                query += "and Salesman_Code  in (" + clsCommon.GetMulcallString(cbgsalesman.CheckedValue) + ")"
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
                query += "and Account_Seg_Code7 in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            End If
            query += " and  convert(date,Receipt_Date,103) >= convert(date,'" + fromdate + "',103) and convert(date,Receipt_Date,103) <= convert(date,'" + todate + "',103) "
            query += " and Receipt_Type not in ('M','A')  and Posted='" + post + "' )xxx"

            If (excel = True) Then
                If chkBankWise.Checked = True Then
                    query += "  )xxx  PIVOT (MAX([Payment_Code])  FOR [Payment_Code] IN ([CASH],[PETTY_CASH],[BANK], [OTHER], [SETTLEMENT])) AS Pivot1 GROUP BY   Receipt_Date , Receipt_No,Document_No) xXx  "
                Else
                    query += "  )xxx  PIVOT (MAX([Payment_Code])  FOR [Payment_Code] IN ([CASH],[CHEQUE],[ONLINE], [RTGS], [SETTLEMENT])) AS Pivot1 GROUP BY   Receipt_Date , Receipt_No,Document_No) xXx  "
                End If

                If chkSummary.Checked = True Then
                    query += " )ABC GROUP BY   Receipt_Date   ORDER BY Receipt_Date"
                Else
                    query += "  ORDER BY Receipt_Date , Receipt_No"
                End If
                pvtQry = pvtQry + query

                dt = clsDBFuncationality.GetDataTable(pvtQry)
                gv1.DataSource = dt
                'ReStoreGridLayout()
                'Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'Dim strtemp As String = "From Date : " + clsCommon.GetPrintDate(dtpreceiptfromdate.Value, "dd/MM/yyyy") + " To Date : " + clsCommon.GetPrintDate(dtpreceiptTodate.Value, "dd/MM/yyyy")

                'If chkLocSelect.IsChecked Then
                '    Dim strlocName As String = ""
                '    For Each StrName As String In cbgLocation.CheckedDisplayMember
                '        If clsCommon.myLen(strlocName) > 0 Then
                '            strlocName += ", "
                '        End If
                '        strlocName += StrName
                '    Next
                '    Dim strlocCode As String = ""
                '    For Each StrCode As String In cbgLocation.CheckedValue
                '        If clsCommon.myLen(strlocCode) > 0 Then
                '            strlocCode += ", "
                '        End If
                '        strlocCode += StrCode
                '    Next
                '    arrHeader.Add(("Location : " + strlocName + " "))

                'End If
                'If chkSummary.Checked = True Then
                '    clsCommon.MyExportToExcelGrid("Cash Register Summary", gv1, arrHeader, "Cash Register Summary")
                'Else
                '    clsCommon.MyExportToExcelGrid("Cash Register", gv1, arrHeader, "Cash Register Detail")
                'End If

            Else
                If chkSummary.Checked = True Then
                    ' query += " group by TSPL_RECEIPT_HEADER. Receipt_Date"
                    dt = clsDBFuncationality.GetDataTable(query)
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "CashRegisterSummary", "Cash Register Details")
                Else

                    dt = clsDBFuncationality.GetDataTable(query)
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "CashRegisterNew", "Cash Register Details")
                End If
            End If


        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    '---------------------Added By -----Pankaj Kumar-------------on--29/03/2012-------------
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CSH-RGR-RPT"
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

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    '    '-----------------------------------Code Ends Here-------------------------------
    'End Function

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged, chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try


            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            Dim strtemp As String = "From Date : " + clsCommon.GetPrintDate(dtpreceiptfromdate.Value, "dd/MM/yyyy") + " To Date : " + clsCommon.GetPrintDate(dtpreceiptTodate.Value, "dd/MM/yyyy")
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.cash_Register_Details4 & "'"))
            If chksalesmanselect.IsChecked Then
                Dim strlocName As String = ""
                For Each StrName As String In cbgsalesman.CheckedDisplayMember
                    If clsCommon.myLen(strlocName) > 0 Then
                        strlocName += ", "
                    End If
                    strlocName += StrName
                Next
                Dim strlocCode As String = ""
                For Each StrCode As String In cbgsalesman.CheckedValue
                    If clsCommon.myLen(strlocCode) > 0 Then
                        strlocCode += ", "
                    End If
                    strlocCode += StrCode
                Next
                arrHeader.Add(("Salesman : " + strlocName + " "))

            End If
            If chkLocSelect.IsChecked Then
                Dim strlocName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strlocName) > 0 Then
                        strlocName += ", "
                    End If
                    strlocName += StrName
                Next
                Dim strlocCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strlocCode) > 0 Then
                        strlocCode += ", "
                    End If
                    strlocCode += StrCode
                Next
                arrHeader.Add(("Location : " + strlocName + " "))

            End If
            If exporter = EnumExportTo.Excel Then

                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 2007 (*.xlsx) |*.xlsx;|Excel 97-2003 (*.xls)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Cash Register Detail", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(True)
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(True)
        Export(EnumExportTo.PDF)
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
