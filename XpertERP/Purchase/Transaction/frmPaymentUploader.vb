'--Created By Anubhooti 05-Sep-2014 BM00000003755 ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Office.Interop

Public Class FrmPaymentUploader
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPaymentUploader)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnUploader.Visible = MyBase.isModifyFlag
    End Sub
    Sub LoadData()

        If isInsideLoadData Then
            clsCommon.MyMessageBoxShow("Work in Progress Please Wait...")
            Exit Sub
        End If

        btnUploader.Enabled = True
        isInsideLoadData = True
        btnUploader.Enabled = False

         UploaderQuery()
        isInsideLoadData = False
        btnUploader.Enabled = True
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myCDate(dtpFromDate.Value) > clsCommon.myCDate(dtpToDate.Value) Then
                dtpFromDate.Focus()
                Throw New Exception("From date can not be greater than from to date")
            End If
            If clsCommon.myLen(txtBankCode.Value) <= 0 Then
                txtBankCode.Focus()
                Throw New Exception("Please fill bank code")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Function
    Sub UploaderQuery()
        Try
            If AllowToSave() Then
                Dim qry As String = ""
                Dim WhrCond As String = ""
                'qry = "Select isnull(TSPL_COMPANY_MASTER.Comp_Name,'') AS [Company Name] , " & _
                '" CASE WHEN ISNULL(TSPL_VENDOR_MASTER.Form_Type,'') ='VSP' THEN 'VSP'  WHEN ISNULL(TSPL_VENDOR_MASTER.Form_Type,'') ='TTM' THEN 'S.T' WHEN ISNULL(TSPL_VENDOR_MASTER.Form_Type,'') ='PTM' THEN 'P.T' ELSE '' END  + ' ' + CASE WHEN ISNULL(TSPL_VENDOR_MASTER.vsp_payment ,'') ='Different' THEN ISNULL(TSPL_VENDOR_MASTER.VSP_Payee_Name,'') ELSE ISNULL(TSPL_VENDOR_MASTER.Vendor_Name,'') END + ' ' + CONVERT(VARCHAR, TSPL_PAYMENT_HEADER.Payment_Date,104) AS [Ref No], " & _
                '" isnull(TSPL_COMPANY_MASTER.Comp_Name,'') AS [Comp Name] ,isnull(Payment_Amount,0) As [Amt],CAST(TSPL_BANK_MASTER.BANKACCNUMBER As VARCHAR) As [Our A/C]  " & _
                '" ,CASE WHEN ISNULL(TSPL_PAYMENT_CODE.Payment_Type ,'') ='RTGS' THEN 'I'  WHEN ISNULL(TSPL_PAYMENT_CODE.Payment_Type ,'') ='NEFT' THEN 'I' ELSE 'M' END AS [Neft/Rtgs/trf] " & _
                '" ,CASE WHEN ISNULL(TSPL_PAYMENT_CODE.Payment_Type ,'') ='RTGS' THEN ISNULL(TSPL_VENDOR_MASTER.IFSC_Code ,'')  WHEN ISNULL(TSPL_PAYMENT_CODE.Payment_Type ,'') ='NEFT' THEN ISNULL(TSPL_VENDOR_MASTER.IFSC_Code ,'') ELSE '998' END AS [IFSC Code],  CASE WHEN ISNULL(TSPL_VENDOR_MASTER.vsp_payment ,'') ='Different' THEN ISNULL(TSPL_VENDOR_MASTER.VSP_Payee_Name,'') ELSE ISNULL(TSPL_VENDOR_MASTER.Vendor_Name,'') END AS [Party Name],'' AS A ,'' AS B,'' As C,'' AS D,'' AS E,'' AS F   " & _
                '" ,ISNULL(TSPL_VENDOR_MASTER.Account_No,'') AS [Party A/c No],'' As  J,'' As K,'' As L,'' As M,'' As N,'' As O,'' As P ,'' As Q,'' As R,'' As S,'' As T,CASE WHEN ISNULL(TSPL_VENDOR_MASTER.Form_Type,'') ='VSP' THEN 'VSP'  WHEN ISNULL(TSPL_VENDOR_MASTER.Form_Type,'') ='TTM' THEN 'S.T' WHEN ISNULL(TSPL_VENDOR_MASTER.Form_Type,'') ='PTM' THEN 'P.T' ELSE '' END  + ' ' + CASE WHEN ISNULL(TSPL_VENDOR_MASTER.vsp_payment ,'') ='Different' THEN ISNULL(TSPL_VENDOR_MASTER.VSP_Payee_Name,'') ELSE ISNULL(TSPL_VENDOR_MASTER.Vendor_Name,'') END + ' ' + CONVERT(VARCHAR, TSPL_PAYMENT_HEADER.Payment_Date,104) AS [Refrence No],'' As U,'' As V,'' As W,'' As X " & _
                '"  From TSPL_PAYMENT_HEADER " & _
                '" INNER JOIN TSPL_VENDOR_MASTER ON TSPL_PAYMENT_HEADER.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code " & _
                '" LEFT OUTER JOIN TSPL_COMPANY_MASTER  ON TSPL_PAYMENT_HEADER.Comp_Code  =TSPL_COMPANY_MASTER.Comp_Code " & _
                '" LEFT OUTER JOIN TSPL_BANK_MASTER  ON TSPL_PAYMENT_HEADER.BANK_CODE =TSPL_BANK_MASTER.BANK_CODE " & _
                '" LEFT OUTER JOIN TSPL_PAYMENT_CODE On TSPL_PAYMENT_HEADER.Payment_Code =TSPL_PAYMENT_CODE.Payment_Code "
                'qry += " Where CONVERT(date, TSPL_PAYMENT_HEADER.Payment_Date,103) >= '" & Format(dtpFromDate.Value, "dd/MMM/yyyy") & "' and CONVERT(date, TSPL_PAYMENT_HEADER.Payment_Date,103) <= '" & Format(dtpToDate.Value, "dd/MMM/yyyy") & "' AND TSPL_PAYMENT_HEADER.BANK_CODE ='" + clsCommon.myCstr(txtBankCode.Value) + "'"
                qry = "Select Case when isnull(TSPL_COMPANY_MASTER.Comp_Name,'') like '%KWALITY%' then 'KWALITY' else isnull(TSPL_COMPANY_MASTER.Comp_Name,'') end AS [Company Name] , " & _
               " DocRefNoForUploader AS [Ref No], " & _
               " Case when isnull(TSPL_COMPANY_MASTER.Comp_Name,'') like '%KWALITY%' then 'KWALITY' else isnull(TSPL_COMPANY_MASTER.Comp_Name,'') end AS [Comp Name] ,isnull(Payment_Amount,0) As [Amt],CAST(TSPL_BANK_MASTER.BANKACCNUMBER As VARCHAR) As [Our A/C]  " & _
               " ,CASE WHEN ISNULL(TSPL_PAYMENT_Header.bank_code,'') Like '%IDBI%' THEN 'M' WHEN ISNULL(TSPL_PAYMENT_CODE.Payment_Type ,'') ='RTGS' THEN 'I'  WHEN ISNULL(TSPL_PAYMENT_CODE.Payment_Type ,'') ='NEFT' THEN 'I' ELSE 'M' END AS [Neft/Rtgs/trf] " & _
               " ,CASE WHEN ISNULL(TSPL_PAYMENT_Header.bank_code,'') Like '%IDBI%' THEN '998'  WHEN ISNULL(TSPL_PAYMENT_CODE.Payment_Type ,'') ='RTGS' THEN ISNULL(TSPL_VENDOR_MASTER.IFSC_Code ,'')  WHEN ISNULL(TSPL_PAYMENT_CODE.Payment_Type ,'') ='NEFT' THEN ISNULL(TSPL_VENDOR_MASTER.IFSC_Code ,'') ELSE '998' END AS [IFSC Code],  CASE WHEN ISNULL(TSPL_VENDOR_MASTER.vsp_payment ,'') ='Different' THEN ISNULL(TSPL_VENDOR_MASTER.VSP_Payee_Name,'') ELSE ISNULL(TSPL_VENDOR_MASTER.Vendor_Name,'') END AS [Party Name],'' AS A ,'' AS B,'' As C,'' AS D,'' AS E,'' AS F   " & _
               " ,ISNULL(TSPL_VENDOR_MASTER.Account_No,'') AS [Party A/c No],'' As  J,'' As K,'' As L,'' As M,Case WHEN ISNULL(TSPL_PAYMENT_Header.bank_code,'') Like '%IDBI%' THEN 'IDBI Bank' else '' end As N,'' As O,'' As P ,'' As Q,'' As R,'' As S,'' As T,DocRefNoForUploader AS [Refrence No], CASE WHEN ISNULL(TSPL_VENDOR_MASTER.vsp_payment ,'') ='Different' THEN ISNULL(TSPL_VENDOR_MASTER.VSP_Payee_Name,'') ELSE ISNULL(TSPL_VENDOR_MASTER.Vendor_Name,'') END As U,'' As V,'' As W,'' As X " & _
               "  From TSPL_PAYMENT_HEADER " & _
               " INNER JOIN TSPL_VENDOR_MASTER ON TSPL_PAYMENT_HEADER.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code " & _
               " LEFT OUTER JOIN TSPL_COMPANY_MASTER  ON TSPL_PAYMENT_HEADER.Comp_Code  =TSPL_COMPANY_MASTER.Comp_Code " & _
               " LEFT OUTER JOIN TSPL_BANK_MASTER  ON TSPL_PAYMENT_HEADER.BANK_CODE =TSPL_BANK_MASTER.BANK_CODE " & _
               " LEFT OUTER JOIN TSPL_PAYMENT_CODE On TSPL_PAYMENT_HEADER.Payment_Code =TSPL_PAYMENT_CODE.Payment_Code  Left join (select Distinct Payment_No,DocRefNoForUploader,Description from TSPL_PAYMENT_DETAIL inner join TSPL_PAYMENT_PROCESS_INVOICE on TSPL_PAYMENT_PROCESS_INVOICE.Milk_Purchase_Invoice_No=TSPL_PAYMENT_DETAIL.Vendor_Invoice_No inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_INVOICE.Doc_No) TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.payment_No "
                qry += " Where CONVERT(date, TSPL_PAYMENT_HEADER.Payment_Date,103) >= '" & Format(dtpFromDate.Value, "dd/MMM/yyyy") & "' and CONVERT(date, TSPL_PAYMENT_HEADER.Payment_Date,103) <= '" & Format(dtpToDate.Value, "dd/MMM/yyyy") & "' AND TSPL_PAYMENT_HEADER.BANK_CODE ='" + clsCommon.myCstr(txtBankCode.Value) + "'"

                If Not IsNothing(TxtNeftUploader.arrValueMember) Then
                    If TxtNeftUploader.arrValueMember.Count > 0 Then
                        qry += " and DocRefNoForUploader in (" & clsCommon.GetMulcallString(TxtNeftUploader.arrValueMember) & ")"
                    End If
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    transportSql.ExporttoExcelWithoutFilter(qry, WhrCond, "", Me, True)
                Else
                    Throw New Exception("No data found to display")
                End If

                'Excel(qry)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub Excel(ByVal Qry As String)
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            'Make Connection ' Ammar
            ' Dim cnn As DataAccess = New DataAccess(CONNECTION_STRING)
            ' Variable ' Ammar
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            '  sfd.Filter = "Excel (*.xlsx;*.xls)|*.xlsx;*.xls"
            sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 *.xlsx|(*.xlsx);|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Return
            End If
            'If InStr(path, ".xlsx") <> -1 Then
            '    path = Replace(path, ".xlsx", ".xls")
            'End If
            If Not filePath.Equals(String.Empty) Then

                Dim i, j As Integer
                'Excel WorkBook object ' Ammar
                Dim xlApp As Microsoft.Office.Interop.Excel.Application
                Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
                Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
                Dim misValue As Object = System.Reflection.Missing.Value
                xlApp = New Microsoft.Office.Interop.Excel.Application
                xlWorkBook = xlApp.Workbooks.Add(misValue)
                ' Sheet Name or Number ' Ammar
                xlWorkSheet = xlWorkBook.Worksheets("sheet1")
                ' Sql QUery ' Ammar
                '  xlWorkBook.Sheets.Select("A1:A2")

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                    ' DataSet
                    'Dim ds As New DataSet
                    'dscmd.Fill(ds)
                    'COLUMN NAME ADD IN EXCEL SHEET OR HEADING 

                    'xlWorkSheet.Cells(1, 1).Value = clsCommon.myCstr(dt.Rows(0)("Company Name"))

                    clsCommon.ProgressBarShow()
                    Dim colIndex As Integer = 0
                    For j = 0 To dt.Columns.Count - 1
                        colIndex = colIndex + 1
                        xlWorkSheet.Cells(1, colIndex) = clsCommon.myCstr(dt.Columns(j))
                        xlWorkSheet.Cells(1, colIndex).Font.Bold = True
                    Next
                    ' SQL Table Transfer to Excel
                    For i = 0 To dt.Rows.Count - 1
                        'Column
                        For j = 0 To dt.Columns.Count - 1
                            ' this i change to header line cells >>>
                            xlWorkSheet.Cells(i + 3, j + 1) = _
                            dt.Rows(i).Item(j)
                        Next
                    Next
                    xlWorkSheet.Columns.AutoFit()
                    'clsCommon.MyMessageBoxShow("Please wait ! work in progress...")
                    'clsCommon.ProgressBarShow()
                    'HardCode in Excel sheet
                    ' this i change to footer line cells  >>>
                    'xlWorkSheet.Cells(i + 3, 7) = "Total"
                    'xlWorkSheet.Cells.Item(i + 3, 8) = "=SUM(H2:H18)"
                    ' Save as path of excel sheet
                    xlWorkBook.SaveAs(filePath)
                    xlWorkBook.Close()
                    xlApp.Quit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data transfer Completed!", "Export", MessageBoxButtons.OK)
                    System.Diagnostics.Process.Start(filePath)
                    trans.Commit()
                    'releaseObject(xlApp)
                    'releaseObject(xlWorkBook)
                    'releaseObject(xlWorkSheet)
                    'Msg Box of Excel Sheet Path
                    'MsgBox("You can find the file D:\vbexcel.xlsx")
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow("No data transfered." + Environment.NewLine + ex.Message, "Export Error", MessageBoxButtons.OK)
        End Try

    End Sub
    Sub funReset()
        btnUploader.Enabled = True
        txtBankCode.Value = ""
        lblBankDesc.Text = ""
    End Sub
    Private Sub FrmPaymentUploader_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt And e.KeyCode = Keys.U Then
            LoadData()
        End If
    End Sub

    Private Sub FrmPaymentUploader_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnUploader, "Press Alt+U for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        dtpFromDate.Value = clsCommon.GETSERVERDATE.AddMonths(-1)
        dtpToDate.Value = clsCommon.GETSERVERDATE
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()

    End Sub

    Private Sub btnUploader_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploader.Click
        LoadData()
    End Sub

    Private Sub txtBankCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtBankCode._MYValidating
        Dim strWhrclas As String = ""
        Dim Qry As String = clsERPFuncationality.glbankqueryNew(strWhrclas)
        txtBankCode.Value = clsCommon.ShowSelectForm("BUploader", Qry, "Code", strWhrclas, txtBankCode.Value, "Code", isButtonClicked)
        If clsCommon.myLen(txtBankCode.Value) > 0 Then
            lblBankDesc.Text = connectSql.RunScalar("select description from TSPL_BANK_MASTER where bank_code = '" + txtBankCode.Value + "'")
        End If
    End Sub

    Private Sub TxtNEFTUploader__My_Click(sender As Object, e As EventArgs) Handles TxtNeftUploader._My_Click
        Dim strWhrclas As String = ""
        Dim Qry As String = "select Distinct TSPL_PAYMENT_PROCESS_INVOICE.Doc_No as [Process No],DocRefNoForUploader as [Code] from TSPL_PAYMENT_DETAIL inner join TSPL_PAYMENT_PROCESS_INVOICE on TSPL_PAYMENT_PROCESS_INVOICE.Milk_Purchase_Invoice_No=TSPL_PAYMENT_DETAIL.Vendor_Invoice_No inner join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_INVOICE.Doc_No"
        'TxtNeftUploader.Value = clsCommon.ShowSelectForm("NEFUploader", Qry, "Code", strWhrclas, txtBankCode.Value, "Code", isButtonClicked)
        TxtNeftUploader.arrValueMember = clsCommon.ShowMultipleSelectForm("NEFUploader", Qry, "Code", "Process No", TxtNeftUploader.arrValueMember, TxtNeftUploader.arrDispalyMember)
    End Sub
End Class
