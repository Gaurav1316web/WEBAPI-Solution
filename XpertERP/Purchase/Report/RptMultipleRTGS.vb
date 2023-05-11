Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class RptMultipleRTGS
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    'Dim Refresh As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptMultipleRTGS)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnexport.Visible = MyBase.isExport
        BtnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        loadReport()
    End Sub
    Public Sub loadReport()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkBankSelect.IsChecked AndAlso cbgBank.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Bank or select all.")
            Exit Sub
        End If
        If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Vendor or select all.")
            Exit Sub
        End If
        Dim sQuery As String = String.Empty
        If chkNEFTFormat.Checked Then
            sQuery = "select RIGHT(TSPL_BANK_MASTER.BANKACC, 3) as [Loc Code],TSPL_LOCATION_MASTER .Location_Desc as [Loc Name],case when TSPL_Vendor_Master.Form_Type ='ALL' then 'Vendor' when TSPL_Vendor_Master.Form_Type ='VSP' then 'VSP' when TSPL_Vendor_Master.Form_Type in ('PTM','TTM')  then 'Transporter' when isnull(TSPL_Vendor_Master.Vendor_Type_CHA,'') ='CV' then 'Chilling Vendor' else '' end  as [Vendor Type],TSPL_PAYMENT_HEADER.Vendor_Code as [Vendor Code],TSPL_Vendor_Master.Vendor_Name as [Vendor Name],TSPL_PAYMENT_HEADER.Payment_Amount as [Amount],TSPL_PAYMENT_HEADER.Payment_Code as [Payment Mode],convert(varchar,Payment_Date,103)as [Payment Date],
TSPL_BANK_MASTER.IBAN_No  as [Sender IFSC],TSPL_BANK_MASTER.BANKACCNUMBER as [Sending Customer A/C No],TSPL_BANK_MASTER.DESCRIPTION as [Bank Name],TSPL_COMPANY_MASTER.Comp_Name as [Sending Customer A/C Name],TSPL_PAYMENT_HEADER.Vendor_IFSC_Code as [Beneficiary IFSC],TSPL_PAYMENT_HEADER.Vendor_Bank_ACNo as [Beneficiary A/C No],TSPL_Vendor_Master.Vendor_Name as [Beneficiary A/C Name],TSPL_PAYMENT_HEADER.Vendor_Bank_Name as [Beneficiary Bank Name],TSPL_PAYMENT_HEADER.Vendor_Branch_Name as [Beneficiary Bank Branch]
 from TSPL_PAYMENT_HEADER left join TSPL_BANK_MASTER on TSPL_BANK_MASTER.Bank_Code=TSPL_PAYMENT_HEADER.Bank_Code Left join TSPL_Vendor_Master on TSPL_Vendor_Master.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code 
LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=RIGHT(TSPL_BANK_MASTER.BANKACC, 3)
left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_PAYMENT_HEADER.Comp_Code
where TSPL_PAYMENT_HEADER.Payment_type in ('AV','OA','PY')"
            If txtPaymentMode.arrValueMember IsNot Nothing AndAlso txtPaymentMode.arrValueMember.Count > 0 Then
                sQuery += " and TSPL_PAYMENT_HEADER.Payment_Code in (" + clsCommon.GetMulcallString(txtPaymentMode.arrValueMember) + ") "
            End If
        Else
            sQuery = "select Cast(1 as BIT) as 'Check',Payment_No,convert(varchar,Payment_Date,103)as Payment_Date,TSPL_PAYMENT_HEADER.Bank_Code,Description as Bank_Name,TSPL_PAYMENT_HEADER.Vendor_Code,TSPL_VENDOR_MASTER.Cheque_In_Favour_Of as Vendor_Name,Payment_Amount from TSPL_PAYMENT_HEADER left join TSPL_BANK_MASTER on TSPL_BANK_MASTER.Bank_Code=TSPL_PAYMENT_HEADER.Bank_Code Left join TSPL_Vendor_Master on TSPL_Vendor_Master.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code where TSPL_PAYMENT_HEADER.Payment_Code='RTGS' "
        End If


        sQuery += " and convert(date,Payment_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Payment_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If chkBankSelect.IsChecked And cbgBank.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_BANK_MASTER. Bank_Code   IN (" + clsCommon.GetMulcallString(cbgBank.CheckedValue) + ") "
        End If
        If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_Vendor_Master. Vendor_Code   IN (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
        End If
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            If chkNEFTFormat.Checked = True Then
                gv.Columns(ii).IsVisible = True
            Else
                gv.Columns(ii).IsVisible = False
            End If

        Next

        If chkNEFTFormat.Checked = True Then
            gv.Columns("Payment Date").IsVisible = True
            gv.Columns("Payment Date").Width = 100
            gv.Columns("Payment Date").HeaderText = " Payment Date"
            gv.Columns("Payment Date").FormatString = "{0:d}"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            Dim item1 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv.BestFitColumns()
        Else
            gv.Columns("Check").IsVisible = True
            gv.Columns("Check").Width = 100
            gv.Columns("Check").HeaderText = " "
            gv.Columns("Check").ReadOnly = False

            gv.Columns("Payment_No").IsVisible = True
            gv.Columns("Payment_No").Width = 100
            gv.Columns("Payment_No").HeaderText = "Payment No "
            gv.Columns("Payment_No").ReadOnly = False



            gv.Columns("Payment_Date").IsVisible = True
            gv.Columns("Payment_Date").Width = 100
            gv.Columns("Payment_Date").HeaderText = " Date"
            gv.Columns("Payment_Date").FormatString = "{0:d}"

            gv.Columns("Bank_Code").IsVisible = True
            gv.Columns("Bank_Code").Width = 100
            gv.Columns("Bank_Code").HeaderText = "Bank Code"

            gv.Columns("Bank_Name").IsVisible = True
            gv.Columns("Bank_Name").Width = 150
            gv.Columns("Bank_Name").HeaderText = "Bank Name"

            gv.Columns("Vendor_Code").IsVisible = True
            gv.Columns("Vendor_Code").Width = 100
            gv.Columns("Vendor_Code").HeaderText = " Vendor Code"

            gv.Columns("Vendor_Name").IsVisible = True
            gv.Columns("Vendor_Name").Width = 100
            gv.Columns("Vendor_Name").HeaderText = "Vendor Name"

            gv.Columns("Payment_Amount").IsVisible = True
            gv.Columns("Payment_Amount").Width = 100
            gv.Columns("Payment_Amount").HeaderText = "Payment Amount"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            Dim item1 As New GridViewSummaryItem("Payment_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If


        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True



    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub LoadData()
        ArrInvoice_Arr = New ArrayList


        Dim PaymentNo As String = ""

        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                PaymentNo = PaymentNo + "','" + clsCommon.myCstr(grow.Cells("Payment_No").Value)
            End If
        Next

        If clsCommon.myLen(PaymentNo) > 0 AndAlso clsCommon.myCstr(PaymentNo).Substring(0, 3) = "','" Then
            PaymentNo = PaymentNo.Substring(3, PaymentNo.Length - 3)
        End If




        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkBankSelect.IsChecked AndAlso cbgBank.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Bank or select all.")
            Exit Sub
        End If
        If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single vendor or select all.")
            Exit Sub
        End If
        'Dim Qry As String = " select  Value ,TSPL_PAYMENT_HEADER.Payment_No as Doc_No,convert(varchar,Payment_Date,103)as Payment_Date,Payment_Amount,bank.BANKACCNUMBER as Debit_Account,TSPL_PAYMENT_HEADER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.bank_code as Vendor_Bank_Code,TSPL_VENDOR_MASTER.IFSC_Code,VendorBank.DESCRIPTION as Vendor_Bank_Name,TSPL_BANK_BRANCH_MASTER.Branch_code as Vendor_Branch_code,TSPL_BANK_BRANCH_MASTER.Branch_Name as Vendor_Branch_Name ,TSPL_PAYMENT_HEADER.Payment_Code,Account_No as Credit_Account,TSPL_PAYMENT_HEADER.Bank_Code,Bank.DESCRIPTION as Bank_Name,Bank.Add1,Bank.Add2,Bank.Add3,Bank.Add4 ,TSPL_COMPANY_MASTER.Comp_Name from TSPL_PAYMENT_HEADER left join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No  left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code  left outer join tspl_bank_master as VendorBank on VendorBank.bank_code=TSPL_VENDOR_MASTER.bank_code left join TSPL_BANK_BRANCH_MASTER on TSPL_BANK_BRANCH_MASTER.Bank_code=VendorBank.bank_code left join TSPL_bank_master as Bank on Bank.bank_code=TSPL_PAYMENT_HEADER.Bank_Code left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code=TSPL_PAYMENT_HEADER.comp_code left join (select Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES left join TSPL_CUSTOM_FIELD_HEAD on TSPL_CUSTOM_FIELD_HEAD.Code=TSPL_CUSTOM_FIELD_VALUES.Custom_Field_Code left join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=TSPL_CUSTOM_FIELD_VALUES.Program_Code where TSPL_CUSTOM_FIELD_VALUES.Program_Code='PYMT-NEW'  and "
        'Qry += " Name ='Purpose')tt on tt.transaction_Code=TSPL_PAYMENT_HEADER.Payment_No WHERE TSPL_PAYMENT_HEADER.Payment_Code='RTGS' and TSPL_PAYMENT_HEADER.Payment_No in ('" & PaymentNo & "')"
        Dim Qry As String = "select Value, TSPL_PAYMENT_HEADER.Payment_No as Doc_No,convert(varchar,Payment_Date,103)as Payment_Date,Payment_Amount,bank.BANKACCNUMBER as Debit_Account,TSPL_PAYMENT_HEADER.Vendor_Code,TSPL_VENDOR_MASTER.Cheque_In_Favour_Of as Vendor_Name,TSPL_VENDOR_MASTER.bank_code as Vendor_Bank_Code,TSPL_Vendor_Bank_Branch_Details.Bank_IFSC_Code as IFSC_Code,tspl_vendor_bank_Master.Bank_Name as Vendor_Bank_Name,TSPL_Vendor_Bank_Branch_Details.Branch_name as Vendor_Branch_Name,TSPL_PAYMENT_HEADER.Payment_Code,Account_No as Credit_Account,TSPL_PAYMENT_HEADER.Bank_Code,Bank.DESCRIPTION as Bank_Name,Bank.Add1,Bank.Add2,Bank.Add3,Bank.Add4 ,TSPL_COMPANY_MASTER.Comp_Name from TSPL_PAYMENT_HEADER left join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No  left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code"
        Qry += " left outer join tspl_vendor_bank_Master on  tspl_vendor_bank_Master.bank_code=TSPL_VENDOR_MASTER.bank_code "
        Qry += " left join  TSPL_Vendor_Bank_Branch_Details on TSPL_Vendor_Bank_Branch_Details.Bank_code=tspl_vendor_bank_Master.bank_code"
        Qry += " left join TSPL_bank_master as Bank on Bank.bank_code=TSPL_PAYMENT_HEADER.Bank_Code "
        Qry += " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code=TSPL_PAYMENT_HEADER.comp_code "
        Qry += " left join (select Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES left join TSPL_CUSTOM_FIELD_HEAD on TSPL_CUSTOM_FIELD_HEAD.Code=TSPL_CUSTOM_FIELD_VALUES.Custom_Field_Code left join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=TSPL_CUSTOM_FIELD_VALUES.Program_Code where TSPL_CUSTOM_FIELD_VALUES.Program_Code='PYMT-NEW'  and  Name ='Purpose')tt on tt.transaction_Code=TSPL_PAYMENT_HEADER.Payment_No WHERE TSPL_PAYMENT_HEADER.Payment_Code='RTGS' and TSPL_PAYMENT_HEADER.Payment_No in ('" & PaymentNo & "')"
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(Qry)
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funreport(CrystalReportFolder.Purchase, dt2, EnumTecxpertPaperSize.NA, "PaymentRTGS", "Payment Details")
        frmCRV = Nothing
    End Sub
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        LoadData()
    End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub chkBankAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkBankAll.ToggleStateChanged
        cbgBank.Enabled = Not chkBankAll.IsChecked
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code as [Code],Vendor_Name as [Name] from TSPL_VENDOR_MASTER where TSPL_VENDOR_MASTER.Status='N' "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"
    End Sub
    Sub LoadBank()
        Dim qry As String = "select Bank_Code as [Code],Description as [Name] from TSPL_BANK_MASTER  "
        cbgBank.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgBank.ValueMember = "Code"
        cbgBank.DisplayMember = "Name"

    End Sub
    Private Sub RptMultipleRTGS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        chkBankAll.CheckState = CheckState.Checked
        chkVendorAll.CheckState = CheckState.Checked
        chkNEFTFormat.Checked = True
        chkNEFTFormat.Checked = False
        txtPaymentMode.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.Rows.Clear()
        LoadVendor()
        LoadBank()
    End Sub
    Private Sub RptMultipleRTGS_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            loadReport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            LoadData()
        End If
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Private Sub FunExport(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("From Date: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To Date " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptMultipleRTGS & "'"))
                'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                'End If


                If chkBankSelect.IsChecked Then
                    Dim strBankName As String = ""
                    For Each StrName As String In cbgBank.CheckedDisplayMember
                        If clsCommon.myLen(strBankName) > 0 Then
                            strBankName += ", "
                        End If
                        strBankName += StrName
                    Next
                    Dim strBankCode As String = ""
                    For Each StrCode As String In cbgBank.CheckedValue
                        If clsCommon.myLen(strBankCode) > 0 Then
                            strBankCode += ", "
                        End If
                        strBankCode += StrCode
                    Next

                    arrHeader.Add((" Bank Name: " + strBankName + " "))

                End If
                If chkVendorSelect.IsChecked Then
                    Dim strVendorName As String = ""
                    For Each StrName As String In cbgVendor.CheckedDisplayMember
                        If clsCommon.myLen(strVendorName) > 0 Then
                            strVendorName += ", "
                        End If
                        strVendorName += StrName
                    Next
                    Dim strVendorCode As String = ""
                    For Each StrCode As String In cbgVendor.CheckedValue
                        If clsCommon.myLen(strVendorCode) > 0 Then
                            strVendorCode += ", "
                        End If
                        strVendorCode += StrCode
                    Next

                    arrHeader.Add(("Vendor Name: " + strVendorName + " "))

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
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Multiple RTGS Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        FunExport(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        FunExport(EnumExportTo.PDF)
    End Sub
    ''richa BHO/13/07/21-000039
    Private Sub txtPaymentMode__My_Click(sender As Object, e As EventArgs) Handles txtPaymentMode._My_Click
        Dim strQry As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
        txtPaymentMode.arrValueMember = clsCommon.ShowMultipleSelectForm("PaymentModeMulSel", strQry, "PaymentMode", "Description", txtPaymentMode.arrValueMember, txtPaymentMode.arrDispalyMember)
    End Sub

    Private Sub chkNEFTFormat_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkNEFTFormat.ToggleStateChanged
        If chkNEFTFormat.Checked Then
            txtPaymentMode.Enabled = True
        Else
            txtPaymentMode.Enabled = False
        End If
    End Sub
End Class