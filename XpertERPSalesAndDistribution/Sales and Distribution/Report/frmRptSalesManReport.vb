Imports common
Imports XpertERPEngine
Public Class frmRptSalesManReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub frmRptSalesManReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadCompany()
        LoadLocation()
        'LoadSalesman()
        chkLocAll.IsChecked = True
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        'AddHandler txtSalesman.txtValue.TextChanged, AddressOf FinderToCustomer_txtChanged
        'AddHandler txtSalesman.txtValue.Leave, AddressOf txtSalesman_Leave
        chkCompanyAll.IsChecked = True
        'chksalesmanAll.IsChecked = True
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")


    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnRptSalesManSalesReport)
        If Not (MyBase.isReadFlag) Then
            RadMessageBox.Show("Permission Denied")
            Me.Close()
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub

    'Private Sub txtSalesman_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If (clsCommon.myLen(txtSalesman.txtValue.Text) > 0) Then
    '        Dim qry As String = "select 1 from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + txtSalesman.txtValue.Text + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
    '            RadMessageBox.Show("Salesman Code Not Exist")
    '            txtSalesman.SelectedValue = ""
    '            txtSalesman.txtValue.Text = ""
    '        End If
    '    End If
    'End Sub

    'Private Sub FinderToCustomer_txtChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    lblSalesman.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + txtSalesman.txtValue.Text + "'"))
    'End Sub

    Sub LoadLocation()
        'Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' "
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub


    'Private Sub findToCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalesman.Load
    '    txtSalesman.ConnectionString = connectSql.SqlCon()
    '    txtSalesman.Query = " select EMP_CODE as Code,Emp_Name as Name,Designation,Add1 as Address1,Add2 as Address2 from TSPL_EMPLOYEE_MASTER  order by Code"
    '    txtSalesman.ValueToSelect = "Code"
    '    txtSalesman.Caption = "Code"
    '    txtSalesman.ValueToSelect1 = "Name"
    'End Sub

    'Public Sub LoadSalesman()
    '    Dim qry As String = " select EMP_CODE as Code ,Emp_Name as Name from TSPL_EMPLOYEE_MASTER  order by Code "
    '    dgvsalesman.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    dgvsalesman.ValueMember = "Code"
    '    dgvsalesman.DisplayMember = "Name"
    'End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()

    End Sub
    Sub print()
        Dim strQuery2 As String
        Dim subQry As String
        Dim FilterLocation As String = ""
        Dim FilterCompany As String
        Try

            If fndsalesman.Value = "" Then
                RadMessageBox.Show("Select Employee Code")
                Exit Sub
            End If

            Dim desc As String = clsDBFuncationality.getSingleValue("select Emp_Name  from TSPL_EMPLOYEE_MASTER  where EMP_CODE='" + fndsalesman.Value + "' ")
            Dim heading As String = fndsalesman.Value + " - " + desc


            'If chkSalesmanselect.IsChecked AndAlso dgvsalesman.CheckedValue.Count <= 0 Then
            '    RadMessageBox.Show("Please select atleast one Salesman")
            '    Return
            'End If
            If chkCompanySelect.IsChecked AndAlso cbgCompany.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select atleast one company")
                Return
            End If
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please Select atleast one Location ")
                Return
            End If
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                FilterLocation = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
                FilterLocation = FilterLocation.Replace("'", "")
            End If

            Dim ArrDBName As ArrayList = Nothing
            If chkCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            FilterCompany = clsCommon.GetMulcallString(ArrDBName)
            FilterCompany = FilterCompany.Replace("'", "")

            Dim fromdate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy")
            Dim todate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            Dim strQryForReceiptAmt As String = "(select isnull(SUM(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount),0) from " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL where Document_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)- (select isnull(sum( " + clsCommon.ReplicateDBString + "TSPL_BANK_REVERSE.Amount),0) from " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL left outer join " + clsCommon.ReplicateDBString + "TSPL_BANK_REVERSE on " + clsCommon.ReplicateDBString + "TSPL_BANK_REVERSE.Document_No=" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No where " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)   +  (select isnull(SUM(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost),0)+isnull(SUM(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Breakage_Cost),0) from " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL left outer join " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER on " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No=" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No where " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType='E' and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice' and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Document_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)"

            '            If chkSalesmanselect.IsChecked AndAlso dgvsalesman.CheckedValue.Count > 0 Then
            '                subQry = " and (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code IN (" + clsCommon.GetMulcallString(dgvsalesman.CheckedValue) + ") " & _
            '" or " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Level2_User_code IN (" + clsCommon.GetMulcallString(dgvsalesman.CheckedValue) + ") " & _
            '" or " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Level3_User_code IN (" + clsCommon.GetMulcallString(dgvsalesman.CheckedValue) + ") " & _
            '" or " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Level4_User_code IN (" + clsCommon.GetMulcallString(dgvsalesman.CheckedValue) + ") " & _
            '" or " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Level5_User_code IN (" + clsCommon.GetMulcallString(dgvsalesman.CheckedValue) + ")) "
            '            End If



            subQry = " and (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code IN ('" + fndsalesman.Value + "') " & _
                    " or " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Level2_User_code IN ('" + fndsalesman.Value + "') " & _
                    " or " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Level3_User_code IN ('" + fndsalesman.Value + "') " & _
                    " or " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Level4_User_code IN ('" + fndsalesman.Value + "') " & _
                    " or " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Level5_User_code IN ('" + fndsalesman.Value + "')) "


            Dim StrQuery1 As String = "select  '" + heading + "' as heading,'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as RunDate, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code,RTRIM(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code)+' - '+RTRIM(" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name) as Emp_Name," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Inv_Detail_Total_Amt+isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.TPT,0)+isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Inv_Tax_Amt,0) as  Inv_Detail_Total_Amt ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Empty_Value,(" + strQryForReceiptAmt + ") as ReceiptAmt ,Case When " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice='Y' Then 0 else ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Balance_Amt,0) End as Balance_Amt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt+ isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Empty_Value,0) as Total_Invoice_Amt,'" + fromdate + "' as FilterFromDate,'" + todate + "' as FilterToDate, (TSPL_EMPLOYEE_MASTER.EMP_CODE + ' - ' +TSPL_EMPLOYEE_MASTER.Emp_Name+ ' - ' +TSPL_EMPLOYEE_MASTER.Card_No) as FilterSalesman, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice as CreditParty from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code where convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date,'" + fromdate + "',103) and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date,'" + todate + "',103) and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post='Y' " + subQry + " "

            If chkLocSelect.IsChecked Then
                StrQuery1 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in  (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            End If

            Dim strUni As String = "Union All "
            strQuery2 = "select  '" + heading + "' as heading,'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as RunDate, " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE as Salesman_Code, "
            strQuery2 += " RTRIM(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE)+' - '+RTRIM(" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name) as Emp_Name, "
            strQuery2 += " " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No AS Sale_Invoice_No,'' AS Shipment_No,'' AS Sale_Invoice_Date, "
            strQuery2 += " 0 as  Inv_Detail_Total_Amt ,0 AS Empty_Value,(select SUM(isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.item_cost,0)) + sum(isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Breakage_Cost,0)) "
            strQuery2 += " from " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL  where  TSPL_ADJUSTMENT_DETAIL.Adjustment_No = TSPL_ADJUSTMENT_HEADER.Adjustment_No)  as ReceiptAmt , "
            strQuery2 += " 0 as Balance_Amt,'' as Cust_Code,'' as Cust_Name, 0 as Total_Invoice_Amt,"
            strQuery2 += " '" + fromdate + "' as FilterFromDate,'" + todate + "' as FilterToDate, (TSPL_EMPLOYEE_MASTER.EMP_CODE + '-' +TSPL_EMPLOYEE_MASTER.Emp_Name+ '-' +TSPL_EMPLOYEE_MASTER.Card_No) as FilterSalesman, 'N' as CreditParty "
            strQuery2 += "from " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER "
            strQuery2 += " left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on "
            strQuery2 += "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE=" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE  "
            strQuery2 += " where  convert(date," + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) >= convert(date,'" + fromdate + "',103) and "
            strQuery2 += "convert(date," + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <= convert(date,'" + todate + "',103) and "
            strQuery2 += "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Posted='Y' "

            'If chkSalesmanselect.IsChecked = True AndAlso dgvsalesman.CheckedValue.Count > 0 Then
            '    strQuery2 += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE IN (" + clsCommon.GetMulcallString(dgvsalesman.CheckedValue) + ")"
            'End If

            strQuery2 += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE IN ('" + fndsalesman.Value + "')"

            strQuery2 += " and  LEN(rtrim(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document))<=0 and  LEN(rtrim(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_CODE))<=0 and  LEN(rtrim(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE))>0  and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType='E' "
            If chkLocSelect.IsChecked Then
                'strQuery2 += "    and Exists(select 1 from " + clsCommon.ReplicateDBString + " TSPL_ADJUSTMENT_DETAIL where " + clsCommon.ReplicateDBString + " TSPL_ADJUSTMENT_DETAIL.Adjustment_No=" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No   and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Line_No='1'  and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Location_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") ) "
                strQuery2 += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code in  (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            End If
            strQuery2 = "select heading,RunDate,Salesman_Code,Emp_Name,Sale_Invoice_No,Shipment_No,Sale_Invoice_Date,Inv_Detail_Total_Amt, Empty_Value,ReceiptAmt ,  -1*ReceiptAmt as Balance_Amt, Cust_Code, Cust_Name,  Total_Invoice_Amt,  FilterFromDate, FilterToDate,  FilterSalesman,CreditParty  from (" + strQuery2 + ") xx "
            Dim strQuery = StrQuery1 & strUni & strQuery2
            strQuery = " Select '" + FilterCompany + "' as FCompany, '" + FilterLocation + "' as FLocation, * FRom ( " + strQuery + " ) Final "
            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQuery, ArrDBName, False)

            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "rptSalesman", "Salesman Sales Report")


        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        chkCompanyAll.IsChecked = True
        chkLocAll.IsChecked = True
        'chksalesmanAll.IsChecked = True
    End Sub
    Private Sub chkCompanyAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCompanyAll.ToggleStateChanged, chkCompanySelect.ToggleStateChanged
        cbgCompany.Enabled = Not chkCompanyAll.IsChecked
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SALM-SAL-RPT"
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

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    'Private Sub chksalesmanAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
    '    dgvsalesman.Enabled = False
    'End Sub

    'Private Sub chkSalesmanselect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
    '    dgvsalesman.Enabled = True
    'End Sub




    Private Sub fndsalesman__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndsalesman._MYValidating
        Dim qry As String = "select EMP_CODE as Code ,Emp_Name as Name from TSPL_EMPLOYEE_MASTER  "
        'Dim WhrCls As String = "Location_Type='Physical'"
        fndsalesman.Value = clsCommon.ShowSelectForm("Sales Man", qry, "Code", "", fndsalesman.Value, "Code", isButtonClicked)
        lblEmpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + fndsalesman.Value + "'"))
    End Sub


End Class
