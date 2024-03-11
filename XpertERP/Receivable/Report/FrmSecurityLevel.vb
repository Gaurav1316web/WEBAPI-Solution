
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'================Created by Preeti Gupta ticket no[BM00000007965]================
'Ticket No-ERO/18/11/19-001117,ERO/19/11/19-001123
Public Class FrmSecurityLevel
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim dt As DataTable
    Dim strQry As String

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptSecurityLevel)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub

    Sub LoadSecurityType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "ALL"
        dr("Name") = "ALL"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "Security"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "Crate Security"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Refrigerator Security"
        dt.Rows.Add(dr)

        RdropSecurityType.DataSource = dt
        RdropSecurityType.DisplayMember = "Name"
        RdropSecurityType.ValueMember = "Code"
        RdropSecurityType.SelectedValue = "ALL"
    End Sub
    Sub LoadCustomer()
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strQry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub

    Sub LoadLocation()
        strQry = " select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER  where Location_Type ='Physical' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(strQry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Sub LoadCustomerGroup()
        strQry = " select Cust_Group_Code as [Code],Cust_Group_Desc as [Name] from TSPL_CUSTOMER_GROUP_MASTER "
        cbgCustomerGroup.DataSource = clsDBFuncationality.GetDataTable(strQry)
        cbgCustomerGroup.ValueMember = "Code"
        cbgCustomerGroup.DisplayMember = "Name"
    End Sub

    Sub Print(ByVal IsPrint As Exporter)
        Try
            If fromDate.Value > ToDate.Value Then
                Throw New Exception("From date can not be greater then to Date")
            End If
            If chkSelectLocation.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single Location or select all.")
            End If
            'If ChkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count = 0 Then
            '    Throw New Exception("Please select atleast single Customer or select all.")
            'End If
            If chkCustomerGroupSelect.IsChecked AndAlso cbgCustomerGroup.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single Customer Group or select all.")
            End If
            Gv1.Tag = IIf(rbtnSummary.IsChecked, "Summary", "Detail")
            'dt = clsDB.GetQueryCustomerSecurity(rbtnSummary.IsChecked, fromDate.Value, ToDate.Value, IIf(chkSelectLocation.IsChecked, cbgLocation.CheckedValue, Nothing), IIf(ChkCustomerSelect.IsChecked, cbgCustomer.CheckedValue, Nothing), IIf(chkCustomerGroupSelect.IsChecked, cbgCustomerGroup.CheckedValue, Nothing), rbtnPosted.IsChecked, rbtnUnPosted.IsChecked, clsCommon.myCstr(RdropSecurityType.SelectedValue), TxtMultiCustomerCategory.arrValueMember)
            dt = clsDB.GetQueryCustomerSecurity(rbtnSummary.IsChecked, fromDate.Value, ToDate.Value, IIf(chkSelectLocation.IsChecked, cbgLocation.CheckedValue, Nothing), IIf(txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0, txtCustomer.arrValueMember, Nothing), IIf(chkCustomerGroupSelect.IsChecked, cbgCustomerGroup.CheckedValue, Nothing), rbtnPosted.IsChecked, rbtnUnPosted.IsChecked, clsCommon.myCstr(RdropSecurityType.SelectedValue), TxtMultiCustomerCategory.arrValueMember, TxtZone.arrValueMember, chkSecurityTypeWise.Checked)


            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing

                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.DataSource = dt

                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()

                FormatGridDetails()

                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.MasterTemplate.AllowAddNewRow = False
                '===============added by shivani against ticket[BM00000008613]
                If btnReferesh = False Then
                    If rbtnDetail.IsChecked = True Then
                        Dim frmCRV As New frmCrystalReportViewer()
                        frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "rptCustomerSecurity", "Customer Security")
                        frmCRV = Nothing
                    End If

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub FormatGridDetails()
        Gv1.TableElement.TableHeaderHeight = 20
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next
        'Customer_Code, MAX(Customer_Name) as Customer_Name, SUM(Opening) as Opening, SUM(Debit) as Debit, SUM(Credit) as Credit, (SUM(Opening)+SUM(Debit)-SUM(Credit)) as Closing
        Gv1.Columns("Customer_Code").IsVisible = True
        Gv1.Columns("Customer_Code").HeaderText = "Customer Code"
        Gv1.Columns("Customer_Code").BestFit()

        Gv1.Columns("Customer_Name").IsVisible = True
        Gv1.Columns("Customer_Name").HeaderText = "Customer Name"
        Gv1.Columns("Customer_Name").BestFit()

        Gv1.Columns("Opening").IsVisible = True
        Gv1.Columns("Opening").BestFit()
        Gv1.Columns("Debit").IsVisible = True
        Gv1.Columns("Debit").BestFit()
        Gv1.Columns("Credit").IsVisible = True
        Gv1.Columns("Credit").BestFit()
        Gv1.Columns("Closing").IsVisible = True
        Gv1.Columns("Closing").BestFit()

        Gv1.Columns("Zone Code").IsVisible = True
        Gv1.Columns("Zone Code").BestFit()

        Gv1.Columns("Zone Desc").IsVisible = True
        Gv1.Columns("Zone Desc").BestFit()

        If rbtnDetail.IsChecked Then
            Gv1.Columns("Document_No").IsVisible = True
            Gv1.Columns("Document_No").HeaderText = "Document No"
            Gv1.Columns("Document_No").BestFit()

            Gv1.Columns("Document_Date").IsVisible = True
            Gv1.Columns("Document_Date").HeaderText = "Document Date"
            Gv1.Columns("Document_Date").FormatString = "{0:dd/MM/yyyy}"
            Gv1.Columns("Document_Date").BestFit()

            Gv1.Columns("DocType").IsVisible = True
            Gv1.Columns("DocType").HeaderText = "Document Type"
            Gv1.Columns("DocType").BestFit()

            Gv1.Columns("SecurityDepositType").IsVisible = True
            Gv1.Columns("SecurityDepositType").HeaderText = "Security Type"
            Gv1.Columns("SecurityDepositType").BestFit()

            Gv1.Columns("Loc_code").IsVisible = True
            Gv1.Columns("Loc_code").HeaderText = "Location Code"
            Gv1.Columns("Loc_code").BestFit()

            Gv1.Columns("Location_Desc").IsVisible = True
            Gv1.Columns("Location_Desc").HeaderText = "Location Name"
            Gv1.Columns("Location_Desc").BestFit()

            Gv1.Columns("Cust_Group_Code").IsVisible = True
            Gv1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"
            Gv1.Columns("Cust_Group_Code").BestFit()

            Gv1.Columns("Cust_Group_Desc").IsVisible = True
            Gv1.Columns("Cust_Group_Desc").HeaderText = "Customer Group Name"
            Gv1.Columns("Cust_Group_Desc").BestFit()

            If chkSecurityTypeWise.Checked = True Then
                Gv1.GroupDescriptors.Add(New GridGroupByExpression("SecurityDepositType as SecurityDepositType format ""{0}: {1}"" Group By SecurityDepositType"))
            End If

        End If


        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item1 As New GridViewSummaryItem("Opening", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem("Debit", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem("Credit", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        If rbtnSummary.IsChecked Then
            item1 = New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
        End If

        'gv.GroupDescriptors.Add(New GridGroupByExpression("DOC_DATE as Item format ""{0}: {1}"" Group By DOC_DATE"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_CODE as Item format ""{0}: {1}"" Group By VLC_CODE"))
        'Gv1.ShowGroupPanel = False
        'Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'ReStoreGridLayoutMain(Me)
        'FindAndRestoreGridLayout(Me)
        '==============Update by preeti Gupta against ticket no [BM00000007966]
        'FindAndRestoreGridLayout(Me)
        ReStoreGridLayout()
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        chkAllLocation.CheckState = CheckState.Checked
        chkCustomerGroupAll.CheckState = CheckState.Checked
        ChkCustomerAll.CheckState = CheckState.Checked
        rbtnSummary.IsChecked = True
        RdropSecurityType.SelectedValue = "ALL"
        Gv1.DataSource = Nothing
        ChkCustomerAll.IsChecked = True
        TxtMultiCustomerCategory.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub FrmSecurityLevel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadCustomer()
        LoadCustomerGroup()
        LoadLocation()
        LoadSecurityType()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N ")
        Reset()
        rbtnPosted.IsChecked = True
    End Sub

    Private Sub FrmSecurityLevel_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Private Sub ChkCustomerAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = Not ChkCustomerAll.IsChecked
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            btnReferesh = True
            PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnSummary.IsChecked = True, "S", "D")
            TemplateGridview = Gv1
            Print(Exporter.Refresh)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    'Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
    '    Try
    '        If (Gv1.Rows.Count <= 0) Then
    '            Throw New Exception("No Data found To Export")
    '        End If
    '        ExportExcel_PDF(Exporter.Excel)
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    'Private Sub ExportExcel_PDF(ByVal IsPrint As Exporter)
    '    Try
    '        Dim arrHeader As List(Of String) = New List(Of String)()
    '        Dim strTemp As String = ""
    '        arrHeader.Add("From Date : From " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
    '        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
    '        If IsPrint = Exporter.Excel Then
    '            clsCommon.MyExportToExcelGrid("Security Level Report" + IIf(rbtnDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, Me.Text)
    '        ElseIf IsPrint = Exporter.PDF Then
    '            clsCommon.MyExportToPDF("Security Level Report" + IIf(rbtnDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, "Security Level", True)
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptSecurityLevel & "'"))
            arrHeader.Add("Date Range: From " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If ChkCustomerSelect.IsChecked Then
                Dim strlocName As String = ""
                For Each StrName As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strlocName) > 0 Then
                        strlocName += ", "
                    End If
                    strlocName += StrName
                Next
                Dim strlocCode As String = ""
                For Each StrCode As String In cbgCustomer.CheckedValue
                    If clsCommon.myLen(strlocCode) > 0 Then
                        strlocCode += ", "
                    End If
                    strlocCode += StrCode
                Next
                arrHeader.Add(("Customer : " + strlocName + " "))

            End If

            If chkSelectLocation.IsChecked Then
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

            If chkCustomerGroupSelect.IsChecked Then
                Dim strlocName As String = ""
                For Each StrName As String In cbgCustomerGroup.CheckedDisplayMember
                    If clsCommon.myLen(strlocName) > 0 Then
                        strlocName += ", "
                    End If
                    strlocName += StrName
                Next
                Dim strlocCode As String = ""
                For Each StrCode As String In cbgCustomerGroup.CheckedValue
                    If clsCommon.myLen(strlocCode) > 0 Then
                        strlocCode += ", "
                    End If
                    strlocCode += StrCode
                Next
                arrHeader.Add(("Customer Group : " + strlocName + " "))

            End If

            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                arrHeader.Add(("Customer Category : " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrValueMember) + " "))
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
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Security Level Report" + IIf(rbtnDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, "Security Level", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Print(Exporter.Refresh)
    End Sub



    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        ''richa ERO/23/07/19-000960
        If rbtnDetail.IsChecked Then
            Dim documentNo As String = Gv1.CurrentRow.Cells("Document_No").Value
            Dim Type As String = Gv1.CurrentRow.Cells("Type").Value
            If clsCommon.myLen(documentNo) > 0 Then
                If clsCommon.CompairString(Type, "AR Invoice Entry") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnARInvoiceEntry, Gv1.CurrentRow.Cells("Document_No").Value)
                ElseIf clsCommon.CompairString(Type, "Receipt Entry") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ReceiptEntry, Gv1.CurrentRow.Cells("Document_No").Value)
                ElseIf clsCommon.CompairString(Type, "Bank Reverse Entry") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.reverseTransaction, Gv1.CurrentRow.Cells("Document_No").Value)
                End If
            End If
        Else
            Dim strCustomerCode As String = Gv1.CurrentRow.Cells("Customer_Code").Value
            If clsCommon.myLen(strCustomerCode) > 0 Then
                rbtnDetail.IsChecked = True
                ChkCustomerSelect.IsChecked = True
                Dim strcustomerarray As ArrayList = New ArrayList
                strcustomerarray.Add(strCustomerCode)
                cbgCustomer.CheckedValue = strcustomerarray
                Print(Exporter.Refresh)
            End If
        End If
    End Sub


    Private Sub chkCustomerGroupAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkCustomerGroupAll.ToggleStateChanged
        cbgCustomerGroup.Enabled = Not chkCustomerGroupAll.IsChecked
    End Sub

    Private Sub chkAllLocation_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAllLocation.ToggleStateChanged
        cbgLocation.Enabled = Not chkAllLocation.IsChecked
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub TxtMultiCustomerCategory__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCustomerCategory._My_Click
        Dim qry As String = " select cust_category_code as [Code], CUST_CATEGORY_DESC as [Desc] from TSPL_CUSTOMER_CATEGORY_MASTER "
        TxtMultiCustomerCategory.arrValueMember = clsCommon.ShowMultipleSelectForm("CustCatMSel2", qry, "Code", "Desc", TxtMultiCustomerCategory.arrValueMember, TxtMultiCustomerCategory.arrDispalyMember)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub TxtZone__My_Click(sender As Object, e As EventArgs) Handles TxtZone._My_Click
        strQry = "select Zone_Code as Code ,Description as Name from TSPL_ZONE_MASTER where 1=1"
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            strQry += " and TSPL_ZONE_MASTER. Zone_Code in (Select TSPL_CUSTOMER_MASTER.Zone_Code from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") )"
        End If
        TxtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneMulSelp", strQry, "Code", "Name", TxtZone.arrValueMember, TxtZone.arrDispalyMember)
    End Sub

    Private Sub RmiExcelGrid_Click(sender As Object, e As EventArgs) Handles rmiExcelGrid.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptSecurityLevel & "'"))
            arrHeader.Add("Date Range: From " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If ChkCustomerSelect.IsChecked Then
                Dim strlocName As String = ""
                For Each StrName As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strlocName) > 0 Then
                        strlocName += ", "
                    End If
                    strlocName += StrName
                Next
                Dim strlocCode As String = ""
                For Each StrCode As String In cbgCustomer.CheckedValue
                    If clsCommon.myLen(strlocCode) > 0 Then
                        strlocCode += ", "
                    End If
                    strlocCode += StrCode
                Next
                arrHeader.Add(("Customer : " + strlocName + " "))

            End If

            If chkSelectLocation.IsChecked Then
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

            If chkCustomerGroupSelect.IsChecked Then
                Dim strlocName As String = ""
                For Each StrName As String In cbgCustomerGroup.CheckedDisplayMember
                    If clsCommon.myLen(strlocName) > 0 Then
                        strlocName += ", "
                    End If
                    strlocName += StrName
                Next
                Dim strlocCode As String = ""
                For Each StrCode As String In cbgCustomerGroup.CheckedValue
                    If clsCommon.myLen(strlocCode) > 0 Then
                        strlocCode += ", "
                    End If
                    strlocCode += StrCode
                Next
                arrHeader.Add(("Customer Group : " + strlocName + " "))

            End If

            If TxtMultiCustomerCategory.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerCategory.arrValueMember.Count > 0 Then
                arrHeader.Add(("Customer Category : " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerCategory.arrValueMember) + " "))
            End If

            clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, Me.Text, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class