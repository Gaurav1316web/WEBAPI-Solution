
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class frmDistributerLedgerReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ReportID As String = ""
    Dim arrBack As New List(Of String)
    Dim arrCust As New ArrayList()
    Dim arrDistr As New ArrayList()

#Region "Variable"
    Private isInsideLoadData As Boolean = False
#End Region

    Sub LoadData()
        Try
            If rbtnDetail.IsChecked = True Then
                Dim dt As DataTable = clsSecondaryCustomerMasterInfo.GetLedgerDetailDt(txtFromDate.Value, txtToDate.Value, TxtCustCode.arrValueMember, txtMultDistr.arrValueMember, Nothing)
                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.DataSource = dt
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.BestFitColumns()
                gv3.EnableFiltering = True
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                Dim dtgv As DataTable = clsSecondaryCustomerMasterInfo.GetLedgerSummaryDt(txtFromDate.Value, txtToDate.Value, TxtCustCode.arrValueMember, txtMultDistr.arrValueMember, Nothing)
                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.DataSource = dtgv
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.BestFitColumns()
                gv3.EnableFiltering = True
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
            ReStoreGridLayout()
            gv3.ReadOnly = True
            btnGenrate.Enabled = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            btnGenrate.Enabled = True
        End Try
    End Sub

    Private Sub frmDistributerLedgerReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = txtFromDate.Value
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmDistributerLedgerReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnExport.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport
        btnGenrate.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Sub funReset()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = txtFromDate.Value
        btnGenrate.Enabled = True
        rbtnSummary.IsChecked = True
        gv3.DataSource = Nothing
       
        txtLocation.arrValueMember = Nothing

        TxtCustCode.arrValueMember = Nothing
        txtMultDistr.arrValueMember = Nothing
        gv3.Rows.Clear()
        gv3.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub
    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmDistributerLedgerReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        PageSetupReport_ID = GetReportId()
        TemplateGridview = gv3
        LoadData()
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv3.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv3.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv3.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------

        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv3.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv3.Columns.Count - 1 Step ii + 1
                        gv3.Columns(ii).IsVisible = False
                        gv3.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv3.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Distributer Ledger Report (Detail)")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("Distributer Ledger Report", gv3, arr, "Salary Register")
        clsCommon.MyExportToExcelGrid("Distributer Ledger Report", gv3, arr, "Distributer Ledger Report", False)
    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Distributer Ledger Report (Detail)")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Distributer Ledger Report", gv3, arr, "Distributer Ledger Report", False)
    End Sub

#Region "grid operations"

    
#End Region

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)

        'arr.Add("Distributer Ledger Report (Detail)")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'If TxtCustCode.arrValueMember IsNot Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
        '    arr.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtCustCode.arrDispalyMember))
        'End If
        ''clsCommon.MyExportToExcel("Distributer Ledger Report", gv3, arr, "Salary Register")
        'If gv3.Rows.Count <= 0 Then
        '    gv3.Focus()
        '    clsCommon.MyMessageBoxShow("Data not found.")
        'Else
        '    clsCommon.MyExportToExcelGrid("Distributer Ledger Report", gv3, arr, "Distributer Ledger Report", False)
        'End If

        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Distributer Ledger Report")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If TxtCustCode.arrValueMember IsNot Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
                arrHeader.Add("CNF : " + clsCommon.GetMulcallStringWithComma(TxtCustCode.arrDispalyMember))
            End If
            If txtMultDistr.arrValueMember IsNot Nothing AndAlso txtMultDistr.arrValueMember.Count > 0 Then
                arrHeader.Add("Distributer : " + clsCommon.GetMulcallStringWithComma(txtMultDistr.arrDispalyMember))
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
            transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gv3, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(gv3, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)

        arr.Add("Distributer Ledger Report")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        If TxtCustCode.arrValueMember IsNot Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
            arr.Add(" Employee : " + clsCommon.GetMulcallStringWithComma(TxtCustCode.arrDispalyMember))
        End If
        If txtMultDistr.arrValueMember IsNot Nothing AndAlso txtMultDistr.arrValueMember.Count > 0 Then
            arr.Add(" Distributer : " + clsCommon.GetMulcallStringWithComma(txtMultDistr.arrDispalyMember))
        End If
        transportSql.applyExportTemplate(gv3, PageSetupReport_ID)
        clsCommon.MyExportToPDF("Distributer Ledger Report", gv3, arr, "Distributer Ledger Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
    End Sub
    ' ============= Addded by Preeti gupta============
  
    Private Sub gv3_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv3.CellDoubleClick

        If rbtnSummary.IsChecked Then
            If Not arrBack.Contains("Summary") Then
                arrBack.Add("Summary")
            End If
            rbtnDetail.IsChecked = True

            arrCust = TxtCustCode.arrValueMember
            Dim tmp As New ArrayList()
            tmp.Add(clsCommon.myCstr(gv3.CurrentRow.Cells("Distributor_Code").Value))
            txtMultDistr.arrValueMember = tmp
            arrDistr = txtMultDistr.arrValueMember
            tmp = New ArrayList
            tmp.Add(clsCommon.myCstr(gv3.CurrentRow.Cells("CNF_Code").Value))
            TxtCustCode.arrValueMember = tmp
            PageSetupReport_ID = GetReportId()
            LoadData()
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If rbtnDetail.IsChecked Then
                arrBack.Remove("Summary")
                ' MultVendor.arrValueMember = arrVendor
                rbtnSummary.IsChecked = True
                TxtCustCode.arrValueMember = arrCust
                txtMultDistr.arrValueMember = arrDistr
                LoadData()

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
    '    Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER"
    '    txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    'End Sub

    'Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
    '    txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    'End Sub

    Private Sub TxtMultiEmployee__My_Click(sender As Object, e As EventArgs) Handles TxtCustCode._My_Click
        Dim qry As String = " select Cust_Code as [Code],Customer_Name as [Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],Add1 ,Add2,Add3,City_Code as [City],Closing_Date as [Closing Date],Cust_Category_Code as [Customer Category Code],Cust_Group_Code as [Customer Group Code],Cust_Type_Code as [Customer Type Code],Route_No as [Route No],Route_Desc as [Route Description],Price_Code as [Price Code],CSA_Type as [CSA Type],Phone1,Phone2,Fax,Email,WebSite,Contact_Person_Name as [Contact Person Name],Contact_Person_Phone as [Contact Person Phone],Contact_Person_Fax as [Contact Person Fax],Contact_Person_Website as [Contact Person Website],Contact_Person_Email as [Contact Person Email],Terms_Code as [Terms Code],Cust_Account as [Customer Account]" _
                  & " ,Payment_Code as [Payment Code],Service_Tax_No as [Service Tax No],Tin_No as [Tin No],Lst_No as [LST No],Form_Type as [Form Type],Channel_Code as [Channel Code],Channel_Desc as [Channel Description],(select case when Status='N' then 'Active' else 'In Active' end ) as [Status],OnHold as [On Hold],Remarks1,Remarks2,Additional1,Additional2,Additional3,Salesman_Code as [Salesman Code],Salesman_Desc as [Salesman Description],Visi_Id as [Visi ID],Visi_Desc as [Visi Description],OutLet_Commossion as [Outlet Commission], Balance_ToDate as [Balance To Date],Credit_Limit as [Credit Limit],TempCreditLimit as [Temp Credit Limit],TempCreditLimitFrom as [Temp Credit Limit From],TempCreditLimitTo as [Temp Credit Limit To],Created_By as [Created By],Created_Date as [Created Date],Modify_By as[Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],Route_Group as [Route Group],CST,ECC,Range,Collectorate,PAN,Division,Parent_Customer_No as [Parent Customer No],Customer_Class as [Customer Class],Credit_Customer as [Credit Customer],LastInvoice_No as [Last Invoice No],LastInvoice_Date as [Last Invoice Date],price_CodeNon as [Price Code Non],Inter_Branch as [Inter Branch],TRANSACTION_TYPE as [Transaction Type],Credit_Limit_Alert_Type as [Credit Limit alert Type],PIN_Code as [Pin Code],Cust_DOB as [Customer DOB],Occation,Agg_Made_Date as [Agg Made Date],Agg_Close_Date as [Agg Close Date],CURRENCY_CODE as [Currency Code],Parent_Customer_YN as [Is Parent Customer],Service_Dealer_Code as [Service Dealer Code],TDM_Code as [TDM Code],Distributor_Code as [Distributor Code],IsDistributor as [Is Distributor],Price_Group_Code as [Price Group Code],Franchise_COde as [Franchise Code] from tspl_customer_master"
       
        TxtCustCode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivCustMulSel", qry, "Code", "Name", TxtCustCode.arrValueMember, TxtCustCode.arrDispalyMember)
    End Sub

    Private Sub txtMultLeaveCode__My_Click(sender As Object, e As EventArgs) Handles txtMultDistr._My_Click
        Dim qry As String = " select Cust_Code as [Code],Customer_Name as [Name],ISNULL(TSPL_SECONDARY_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],Add1 ,Add2,Add3 from TSPL_SECONDARY_CUSTOMER_MASTER where 2=2 "

        If Not TxtCustCode.arrValueMember Is Nothing AndAlso TxtCustCode.arrValueMember.Count > 0 Then
            qry = qry & " and TSPL_SECONDARY_CUSTOMER_MASTER.Parent_Customer_No IN (" & clsCommon.GetMulcallString(TxtCustCode.arrValueMember) & " )"
        End If
        txtMultDistr.arrValueMember = clsCommon.ShowMultipleSelectForm("DistrMulSel", qry, "Code", "Name", txtMultDistr.arrValueMember, txtMultDistr.arrDispalyMember)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funReset()
    End Sub

    Private Sub gv3_Click(sender As Object, e As EventArgs) Handles gv3.Click

    End Sub

    Private Function GetReportId()
        ReportID = MyBase.Form_ID + IIf(rbtnSummary.IsChecked = True, "S", "D")
        Return ReportID
    End Function
End Class
