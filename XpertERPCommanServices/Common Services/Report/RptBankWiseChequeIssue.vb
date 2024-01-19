'=================created by shivani tyagi
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class RptBankWiseChequeIssue
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptBankWiseChequeIssue)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Private Sub txtBank__My_Click(sender As Object, e As EventArgs) Handles txtBank._My_Click
        Dim qry As String = "select BANK_CODE as [Code], DESCRIPTION as [Name]  from TSPL_BANK_MASTER"
        txtBank.arrValueMember = clsCommon.ShowMultipleSelectForm("bank", qry, "Code", "Name", txtBank.arrValueMember, txtBank.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Dim FrmP As New FrmPendingRequisitionQty
        FrmP.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtPaymentMode__My_Click(sender As Object, e As EventArgs) Handles txtPaymentMode._My_Click
        Dim qry As String = " select distinct Payment_Type as [Code] , Payment_Type as [Name] from TSPL_PAYMENT_CODE"
        txtPaymentMode.arrValueMember = clsCommon.ShowMultipleSelectForm("payment", qry, "Code", "Name", txtPaymentMode.arrValueMember, txtPaymentMode.arrDispalyMember)
    End Sub

    Private Sub btnReferesh_Click(sender As Object, e As EventArgs) Handles btnReferesh.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData()
    End Sub
    Sub LoadData()
        Dim qry As String = "Select TSPL_PAYMENT_HEADER.Payment_No, convert(varchar,TSPL_PAYMENT_HEADER.Payment_Date,103)as Payment_Date, TSPL_PAYMENT_HEADER.Entry_Desc, TSPL_PAYMENT_HEADER.Payment_Code,TSPL_PAYMENT_HEADER.Bank_Code,TSPL_BANK_MASTER.Description as Bank_Name,TSPL_PAYMENT_HEADER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,Location_Gl_Code ,GL.Location_Desc, TSPL_PAYMENT_HEADER.Cheque_No, convert(varchar,TSPL_PAYMENT_HEADER.Cheque_Date,103)as Cheque_Date, Case When ISNULL(tspl_BankReco_Detail.Reconciliation_Status,'')='C' Then 'Yes' Else 'No' End As RecoStatus, TSPL_PAYMENT_HEADER.Payment_Amount from TSPL_PAYMENT_HEADER left join TSPL_BANK_MASTER on TSPL_BANK_MASTER.Bank_Code=TSPL_PAYMENT_HEADER.Bank_Code left join TSPL_PAYMENT_CODE on TSPL_PAYMENT_CODE.Payment_Code=TSPL_PAYMENT_HEADER.Payment_Code left join TSPL_LOCATION_MASTER LM on LM.Location_Code=TSPL_PAYMENT_HEADER.Location_Code left join TSPL_LOCATION_MASTER Gl on GL.Location_Code=TSPL_PAYMENT_HEADER.Location_Gl_Code"
        qry += " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_HEADER.Vendor_Code"
        qry += " LEFT OUTER JOIN tspl_BankReco_Detail ON tspl_BankReco_Detail.Document_No=TSPL_PAYMENT_HEADER.Payment_No"
        qry += " LEFT OUTER JOIN tspl_BankReco_Head ON tspl_BankReco_Detail.Reconciliation_Id=tspl_BankReco_Head.Reconciliation_Id AND tspl_BankReco_Head.Post='Y'"
        qry += " WHERE TSPL_PAYMENT_HEADER.Posted='1' and convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)>=convert(date,'" + dtpfromdate.Value + "',103) and convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103) <=convert(date,'" + dtpTodate.Value + "' ,103)  "
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_PAYMENT_HEADER.Location_Gl_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtBank.arrValueMember IsNot Nothing AndAlso txtBank.arrValueMember.Count > 0 Then
            qry += " and TSPL_BANK_MASTER.Bank_Code in (" + clsCommon.GetMulcallString(txtBank.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtPaymentMode.arrValueMember IsNot Nothing AndAlso txtPaymentMode.arrValueMember.Count > 0 Then
            qry += " and TSPL_PAYMENT_CODE.Payment_Code in (" + clsCommon.GetMulcallString(txtPaymentMode.arrValueMember) + ") " + Environment.NewLine
        End If
        qry += " order by convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)"
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(qry)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
            RadGroupBox7.Enabled = False
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
       
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next
       
        gv.Columns("Payment_No").IsVisible = True
        gv.Columns("Payment_No").Width = 100
        gv.Columns("Payment_No").HeaderText = "Payment No"

        gv.Columns("Payment_Date").IsVisible = True
        gv.Columns("Payment_Date").Width = 100
        gv.Columns("Payment_Date").HeaderText = "Payment_Date"
        gv.Columns("Payment_Date").FormatString = "{0:d}"

        gv.Columns("Entry_Desc").IsVisible = True
        gv.Columns("Entry_Desc").Width = 100
        gv.Columns("Entry_Desc").HeaderText = "Description"

        gv.Columns("Payment_Code").IsVisible = True
        gv.Columns("Payment_Code").Width = 100
        gv.Columns("Payment_Code").HeaderText = "Payment Mode"

        gv.Columns("Bank_Code").IsVisible = True
        gv.Columns("Bank_Code").Width = 100
        gv.Columns("Bank_Code").HeaderText = "Bank Code"

        gv.Columns("Bank_Name").IsVisible = True
        gv.Columns("Bank_Name").Width = 100
        gv.Columns("Bank_Name").HeaderText = " Bank Name"


        gv.Columns("Vendor_Code").IsVisible = True
        gv.Columns("Vendor_Code").Width = 100
        gv.Columns("Vendor_Code").HeaderText = "Vendor Code"

        gv.Columns("Vendor_Name").IsVisible = True
        gv.Columns("Vendor_Name").Width = 150
        gv.Columns("Vendor_Name").HeaderText = "Vendor Name"

        gv.Columns("Location_Gl_Code").IsVisible = True
        gv.Columns("Location_Gl_Code").Width = 100
        gv.Columns("Location_Gl_Code").HeaderText = "Payment For Code"

        gv.Columns("Location_Desc").IsVisible = True
        gv.Columns("Location_Desc").Width = 100
        gv.Columns("Location_Desc").HeaderText = "Payment For Name"

        gv.Columns("Cheque_No").IsVisible = True
        gv.Columns("Cheque_No").Width = 100
        gv.Columns("Cheque_No").HeaderText = "Cheque No"

        gv.Columns("Cheque_Date").IsVisible = True
        gv.Columns("Cheque_Date").Width = 100
        gv.Columns("Cheque_Date").HeaderText = "Cheque Date"

        gv.Columns("RecoStatus").IsVisible = True
        gv.Columns("RecoStatus").Width = 100
        gv.Columns("RecoStatus").HeaderText = "Reco Status"

        gv.Columns("Payment_Amount").IsVisible = True
        gv.Columns("Payment_Amount").Width = 100
        gv.Columns("Payment_Amount").HeaderText = "Payment Amount"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Payment_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        'gv.GroupDescriptors.Add(New GridGroupByExpression("Loc_Code as Item format ""{0}: {1}"" Group By Loc_Code"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        ' print(EnumExportTo.Excel)
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        'print(EnumExportTo.PDF)
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub RptBankWiseChequeIssue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReferesh, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Adding New")
        dtpTodate.Value = clsCommon.GETSERVERDATE()
        dtpfromdate.Value = dtpTodate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub Reset()
        RadGroupBox7.Enabled = True
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtBank.arrValueMember IsNot Nothing AndAlso txtBank.arrValueMember.Count > 0 Then
                arrHeader.Add("Bank : " + clsCommon.GetMulcallStringWithComma(txtBank.arrDispalyMember))
            End If

            If txtPaymentMode.arrValueMember IsNot Nothing AndAlso txtBank.arrValueMember.Count > 0 Then
                arrHeader.Add("Payment Mode : " + clsCommon.GetMulcallStringWithComma(txtBank.arrDispalyMember))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Bank Wise Cheque Issue Report", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Bank Wise Cheque Issue Report", gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub RptBankWiseChequeIssue_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptBankWiseChequeIssue & "'"))


                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If

                If txtBank.arrValueMember IsNot Nothing AndAlso txtBank.arrValueMember.Count > 0 Then
                    arrHeader.Add("Bank : " + clsCommon.GetMulcallStringWithComma(txtBank.arrDispalyMember))
                End If

                If txtPaymentMode.arrValueMember IsNot Nothing AndAlso txtBank.arrValueMember.Count > 0 Then
                    arrHeader.Add("Payment Mode : " + clsCommon.GetMulcallStringWithComma(txtBank.arrDispalyMember))
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
                    clsCommon.MyExportToPDF("Bank Wise Cheque Issue Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub
End Class