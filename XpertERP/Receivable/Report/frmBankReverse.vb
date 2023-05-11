'--Updation by--[Pankaj Kumar Chaudhary]--Against Ticket no--[]
Imports common
Imports System.Data.SqlClient
Imports System.IO
'' Created By Abhishek Kumar as on 3:05 pm 28 Nov 2012 
Public Class FrmBankReverse
    Inherits FrmMainTranScreen
    Private Sub FrmBankReverse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Funreset()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Public Sub BankCode()
        Dim qry As String = "select BANK_CODE as Code ,DESCRIPTION  from TSPL_BANK_MASTER   "
        cbgBankCode.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgBankCode.ValueMember = "Code"
        cbgBankCode.DisplayMember = "DESCRIPTION"
    End Sub
    Public Sub Funreset()
        dtpFrmDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        chkbankAll.IsChecked = True
        rdobtnAll.IsChecked = True

        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        BankCode()

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub SetUserMgmtNew()
        '--preeti gupta--ticket no-[BM00000003148]
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmBankReverse)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim qry As String
            Dim dtCompany As DataTable
            Dim fromdate As String = clsCommon.myCDate(dtpFrmDate.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            Dim bankcodefilter As String = ""
            Dim status As String = ""
            Dim BankCode As ArrayList = cbgBankCode.CheckedValue
            If rdobtnPayments.IsChecked Then
                status = "Payments"
            End If
            If rdobtnReceipts.IsChecked Then
                status = "Receipts"
            End If
            If rdobtnAll.IsChecked Then
                status = "All"
            End If
            If (cbgBankCode.CheckedValue.Count > 0) Then
                bankcodefilter = clsCommon.GetMulcallString(cbgBankCode.CheckedValue)
                bankcodefilter = bankcodefilter.Replace("'", "")
            End If
            Dim address As String
            Dim CompanyQry As String = "select  TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            dtCompany = clsDBFuncationality.GetDataTable(CompanyQry)
            address = clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress"))
            qry = "  Select '" + bankcodefilter + "' as Bankfilter, '" + fromdate + "' as FilterFromDate,'" + Todate + "' as FilterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,TSPL_BANK_REVERSE.Bank_Code as [Bank Code],(TSPL_BANK_MASTER .DESCRIPTION )as BankName, Reverse_Code as [Reverse Code],convert(varchar,Reversal_Date,103) as [Reverse Date],Cheque_No as CheckNo,Amount,Reverse_Document as Type,Document_No as [Doc No], CONVERT(VARCHAR,Pay_Rec_Date,103) as [Doc Date],(case when Source_Type ='AP' then Vendor_Name else Cust_Name end)as CustVendorName,(case when Source_Type ='AP' then Vendor_Code  else Cust_Code  end)as CustVendorCode,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img,(select Logo_Img2  from TSPL_COMPANY_MASTER  where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img2 ,Reason,(case when Post ='P' then 'posted' else 'pending' end)as Status  from TSPL_BANK_REVERSE inner join TSPL_BANK_MASTER on TSPL_BANK_MASTER .BANK_CODE =TSPL_BANK_REVERSE .Bank_Code where Convert(date,Reversal_Date,103) >=Convert(Date,'" + dtpFrmDate.Value + "',103) and Convert(date,Reversal_Date,103) <=Convert(Date,'" + dtpToDate.Value + "',103)"
            If rdobtnPayments.IsChecked Then
                qry += " and Reverse_Document  in ('Payments')"
            End If
            If rdobtnReceipts.IsChecked Then
                qry += " and Reverse_Document  in ('Receipts')"
            End If
       
            If chkBankSelect.IsChecked Then
                If cbgBankCode.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast One Bank")
                    Return

                End If
                qry += " and TSPL_BANK_REVERSE.Bank_Code  in (" + clsCommon.GetMulcallString(cbgBankCode.CheckedValue) + ") "
            End If
            qry += " order by convert(date,Reversal_Date,103)"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            Else
                dt = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "BankReverseReport", "Bank Reverse Report")
                frmCRV = Nothing
            End If

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Funreset()
    End Sub

    

    Private Sub chkbankAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkbankAll.ToggleStateChanged
        cbgBankCode.Enabled = False
    End Sub

    Private Sub chkBankSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBankSelect.ToggleStateChanged
        cbgBankCode.Enabled = True
    End Sub

    Private Sub btnReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferesh.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            Dim qry As String

            Dim dtCompany As DataTable
            Dim fromdate As String = clsCommon.myCDate(dtpFrmDate.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            Dim bankcodefilter As String
            Dim status As String
            Dim BankCode As ArrayList = cbgBankCode.CheckedValue
            If rdobtnPayments.IsChecked Then
                status = "Payments"
            End If
            If rdobtnReceipts.IsChecked Then
                status = "Receipts"
            End If
            If rdobtnAll.IsChecked Then
                status = "All"
            End If
            If (cbgBankCode.CheckedValue.Count > 0) Then
                bankcodefilter = clsCommon.GetMulcallString(cbgBankCode.CheckedValue)
                bankcodefilter = bankcodefilter.Replace("'", "")
            End If
            'Dim address As String
            'Dim CompanyQry As String = "select  TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            'dtCompany = clsDBFuncationality.GetDataTable(CompanyQry)
            'address = clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress"))
            qry = "    Select Reverse_Code as [Reverse Code],convert(varchar,Reversal_Date,103)as [Reverse Date],TSPL_BANK_REVERSE.Bank_Code as [Bank Code],(TSPL_BANK_MASTER .DESCRIPTION )as [Bank Name],Reverse_Document as [Reverse Document],Document_No as [Receipt/Payment No.], CONVERT(VARCHAR,Pay_Rec_Date,103) as [Receipt/Payment Dt ],Amount as [Receipt/Payment Amt],(case when Source_Type ='AP' then Vendor_Code  else Cust_Code  end)as [Vendor/Customer Code],(case when Source_Type ='AP' then Vendor_Name else Cust_Name end)as [Vendor/Customer/Misc Name] ,Reason as [Reason For Reversal] ,(case when Post ='P' then 'posted' else 'pending' end)as [Status]   from TSPL_BANK_REVERSE inner join TSPL_BANK_MASTER on TSPL_BANK_MASTER .BANK_CODE =TSPL_BANK_REVERSE .Bank_Code where Convert(date,Reversal_Date,103) >=Convert(Date,'" + dtpFrmDate.Value + "',103) and Convert(date,Reversal_Date,103) <=Convert(Date,'" + dtpToDate.Value + "',103)"
            If rdobtnPayments.IsChecked Then
                qry += " and Reverse_Document  in ('Payments')"
            End If
            If rdobtnReceipts.IsChecked Then
                qry += " and Reverse_Document  in ('Receipts')"
            End If

            If chkBankSelect.IsChecked Then
                If cbgBankCode.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please select atleast One Bank")
                    Return

                End If
                qry += " and TSPL_BANK_REVERSE.Bank_Code  in (" + clsCommon.GetMulcallString(cbgBankCode.CheckedValue) + ")  "
            End If
            qry += " order by convert(date,Reversal_Date,103)"
            dtCompany = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.ReadOnly = True
            If dtCompany Is Nothing OrElse dtCompany.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
            gv1.DataSource = dtCompany
            RadPageView1.SelectedPage = RadPageViewPage2
            FormatGrid()
            ReStoreGridLayout()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If clsCommon.myLen(gv1.CurrentRow.Cells("Reverse Code").Value) > 0 Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.reverseTransaction, gv1.CurrentRow.Cells("Reverse Code").Value)

        End If
    End Sub
    Sub FormatGrid()
        gv1.Columns("Reverse Code").Width = 100
        gv1.Columns("Reverse Date").Width = 100
        gv1.Columns("Bank Code").Width = 100
        gv1.Columns("Bank Name").Width = 100
        gv1.Columns("Reverse Document").Width = 100
        gv1.Columns("Receipt/Payment No.").Width = 100
        gv1.Columns("Receipt/Payment Dt ").Width = 100
        gv1.Columns("Receipt/Payment Amt").Width = 100
        gv1.Columns("Vendor/Customer Code").Width = 100
        gv1.Columns("Vendor/Customer/Misc Name").Width = 100
        gv1.Columns("Reason For Reversal").Width = 100
        gv1.Columns("Status").Width = 100
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        Export(EnumExportTo.Excel)
        'Try
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    Dim strTemp As String = ""

        '    arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + " ")
        '    arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
        '    If chkBankSelect.IsChecked Then
        '        strTemp = ""
        '        For Each Str As String In cbgBankCode.CheckedValue
        '            If clsCommon.myLen(strTemp) > 0 Then
        '                strTemp += ", "
        '            End If
        '            strTemp += Str
        '        Next
        '        arrHeader.Add(" Bank : " + strTemp)
        '    End If


        '    clsCommon.MyExportToExcelGrid("Bank Reverse", gv1, arrHeader, "Bank Reverse")


        'Catch ex As Exception
        '    clsCommon.ProgressBarHide()
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'Finally
        '    clsCommon.ProgressBarHide()
        'End Try
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmBankReverse & "'"))
            If chkBankSelect.IsChecked Then
                Dim stVSPName As String = ""
                For Each StrName As String In cbgBankCode.CheckedDisplayMember
                    If clsCommon.myLen(stVSPName) > 0 Then
                        stVSPName += ", "
                    End If
                    stVSPName += StrName
                Next
                Dim strVSPCode As String = ""
                For Each StrCode As String In cbgBankCode.CheckedValue
                    If clsCommon.myLen(strVSPCode) > 0 Then
                        strVSPCode += ", "
                    End If
                    strVSPCode += StrCode
                Next
                arrHeader.Add(("Bank: " + stVSPName + " "))
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
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
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
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        Export(EnumExportTo.PDF)
    End Sub
End Class
