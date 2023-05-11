Imports common
Public Class frmRptNoSales
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()






    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.NoSaleReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
           
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmRptNoSales_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        chkCustomerAll.IsChecked = True
        LoadCustomerCategory()
        chkcategoryall.IsChecked = True
        chkRouteAll.IsChecked = True
        LoadCustomerType()
        LoadCustomer()
        LoadRoute()
        ddlVisiType.SelectedIndex = 0
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        LoadTemplate()
        chktempall.IsChecked = True
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")

    End Sub
    Sub LoadCustomerCategory()
        Dim qry As String = "select Cust_Type_Code as [Code],Cust_Type_Desc as [Name] from TSPL_CUSTOMER_type_master"
        cbgcategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgcategory.ValueMember = "Code"
        cbgcategory.DisplayMember = "Name"
    End Sub

    Sub LoadCustomerType()

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "AGENCY"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "D"
        dr("Name") = "DIRECT ROUTE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "FRANCHISEE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "MODERN TRADE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "PRE-SALE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "SUPER DISTRIBUTOR"
        dt.Rows.Add(dr)

        'cboCustomerClass.DataSource = dt
        'cboCustomerClass.ValueMember = "Code"
        'cboCustomerClass.DisplayMember = "Name"

        'cboCustomerClass.SelectedIndex = 0
    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where 2=2 "
        If cbgcategory.CheckedValue.Count > 0 Then
            strquery += " and Customer_Class in (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")"
        End If

        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub

    Sub LoadRoute()
        Dim strquery As String = "select Route_No,Route_Desc from TSPL_ROUTE_MASTER order by Route_Desc"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgRoute.ValueMember = "Route_No"
        cbgRoute.DisplayMember = "Route_Desc"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()
    End Sub
    Sub print()
        Try
            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer")
                Return
            End If
            If chkcategoryselect.IsChecked AndAlso cbgcategory.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Category Type")
                Return
            End If
            Dim customername As String = ""
            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                For ii As Integer = 0 To cbgCustomer.CheckedDisplayMember.Count - 1
                    If clsCommon.myLen(customername) > 0 Then
                        customername += ", "
                    End If
                    customername += clsCommon.myCstr(cbgCustomer.CheckedDisplayMember(ii))
                Next
            End If
            Dim Routename As String = ""
            If chkRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count > 0 Then
                For ii As Integer = 0 To cbgRoute.CheckedDisplayMember.Count - 1
                    If clsCommon.myLen(customername) > 0 Then
                        Routename += ", "
                    End If
                    Routename += clsCommon.myCstr(cbgRoute.CheckedDisplayMember(ii))
                Next
            End If
            'Dim qry As String = "select TSPL_CUSTOMER_MASTER.Route_No,TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,(TSPL_CUSTOMER_MASTER.Add1+case when LEN(TSPL_CUSTOMER_MASTER.Add2)>0 then ', '+TSPL_CUSTOMER_MASTER.Add2 else '' end + case when LEN(TSPL_CUSTOMER_MASTER.Add3)>0 then ', '+TSPL_CUSTOMER_MASTER.Add3 else '' end) as Addres,TSPL_CUSTOMER_MASTER.Channel_Code,TSPL_CUSTOMER_MASTER.Channel_Desc," + Environment.NewLine
            'qry += " (select COUNT(*) from TSPL_VISI_MASTER where Customer_Id=TSPL_CUSTOMER_MASTER.Cust_Code) as NoOfVisi," + Environment.NewLine
            'qry += "(select Top 1 Sale_Invoice_No from TSPL_SALE_INVOICE_HEAD where TSPL_SALE_INVOICE_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code order by TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date desc,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) as LastInvNo," + Environment.NewLine
            'qry += "(select Top 1 Sale_Invoice_Date from TSPL_SALE_INVOICE_HEAD where TSPL_SALE_INVOICE_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code order by TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date desc,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)as LastInvDate" + Environment.NewLine
            'qry += "from TSPL_CUSTOMER_MASTER " + Environment.NewLine
            'qry += "where TSPL_CUSTOMER_MASTER.cust_code not in (select distinct Cust_Code from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_Date >='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and Sale_Invoice_Date <='" + clsCommon.GetPrintDate(txtToDate.Value) + "' )" + Environment.NewLine

            Dim qry As String = "select TSPL_CUSTOMER_MASTER.Route_No,TSPL_CUSTOMER_MASTER.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,(TSPL_CUSTOMER_MASTER.Add1+case when LEN(TSPL_CUSTOMER_MASTER.Add2)>0 then ', '+TSPL_CUSTOMER_MASTER.Add2 else '' end + case when LEN(TSPL_CUSTOMER_MASTER.Add3)>0 then ', '+TSPL_CUSTOMER_MASTER.Add3 else '' end) as Addres,TSPL_CUSTOMER_MASTER.Channel_Code,TSPL_CUSTOMER_MASTER.Channel_Desc," + Environment.NewLine
            qry += " (select COUNT(*) from TSPL_VISI_MASTER where Customer_Id=TSPL_CUSTOMER_MASTER.Cust_Code) as NoOfVisi," + Environment.NewLine
            qry += "(select Top 1 Sale_Invoice_No from TSPL_SALE_INVOICE_HEAD where TSPL_SALE_INVOICE_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code   and  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<'" + clsCommon.GetPrintDate(txtFromDate.Value, "yyyy-MM-dd") + "' order by TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date desc,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) as LastInvNo," + Environment.NewLine
            qry += "(select Top 1 Sale_Invoice_Date from TSPL_SALE_INVOICE_HEAD where TSPL_SALE_INVOICE_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  and  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<'" + clsCommon.GetPrintDate(txtFromDate.Value, "yyyy-MM-dd") + "' order by TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date desc,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)as LastInvDate" + Environment.NewLine
            qry += "from TSPL_CUSTOMER_MASTER " + Environment.NewLine
            qry += "where TSPL_CUSTOMER_MASTER.cust_code not in (select distinct Cust_Code from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_Date >='" + clsCommon.GetPrintDate(txtFromDate.Value, "yyyy-MM-dd") + "' and Sale_Invoice_Date <='" + clsCommon.GetPrintDate(txtToDate.Value, "yyyy-MM-dd") + "' )" + Environment.NewLine



            '----------Added by Pankaj Kumar-----On-22/03/2012---------
            If ddlVisiType.Text = "Both" Then
                qry += " "
            ElseIf ddlVisiType.Text = "With Visi" Then
                qry += " And  TSPL_CUSTOMER_MASTER.Visi_Id <> '' "
            ElseIf ddlVisiType.Text = "Without Visi" Then
                qry += " And  TSPL_CUSTOMER_MASTER.Visi_Id = '' "
            End If
            '----------------Code Ends Here----------------------------

            '----------------By Vipin for template on 05/07/2012

            If chktempall.IsChecked = True Then
                If chkCustomerSelect.IsChecked = True Then
                    qry += " and TSPL_CUSTOMER_MASTER.Cust_Code  in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            ElseIf chktempselect.IsChecked = True Then


                qry += " and  TSPL_CUSTOMER_MASTER.Cust_Code in  ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "


                If chkCustomerSelect.IsChecked = True Then
                    qry += " and  TSPL_CUSTOMER_MASTER.Cust_Code  in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            End If






            'If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
            '    qry += "and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")" + Environment.NewLine
            'End If
            If chkRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count > 0 Then
                qry += "and TSPL_CUSTOMER_MASTER.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
            End If
            If cbgcategory.CheckedValue.Count > 0 Then
                qry += "and TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")"
            End If

            Dim FinalQry As String = ""
            Dim dt As DataTable

            If rbtnDetail.IsChecked Then
                '--- IN Order By Condition Customer_No replaced by Order By Route_no Condition by abhishek kumar as on 4 july 2012
                FinalQry = "select Route_No, Cust_Code, Customer_Name,Addres,Channel_Desc,NoOfVisi,LastInvNo,(case when LEN(ISNULL(LastInvDate,''))>0 then CONVERT(varchar(10),LastInvDate,103) else '' end ) as LastInvDate,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FilterFromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as FilterToDate,'" + customername + "' as FilterCustomerName,'" + Routename + "' as FilterRouteName,(TSPL_COMPANY_MASTER.Add1+ case when LEN(TSPL_COMPANY_MASTER.Add2)>0 then  ' ,'+TSPL_COMPANY_MASTER.Add2 else '' end +case when LEN(TSPL_COMPANY_MASTER.Add3)>0 then ' ,'+TSPL_COMPANY_MASTER.Add3 else '' end )as comapnyAdd,TSPL_COMPANY_MASTER.Comp_Name,TSPL_CITY_MASTER.City_Name from(" + qry + ")Final left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER.City_Code order by Route_No"
                dt = clsDBFuncationality.GetDataTable(FinalQry)
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "rptNoSaleDetail", Me.Text)
            ElseIf rbtnSummary.IsChecked Then
                FinalQry = "select (ROW_NUMBER() over(order by Route_No)) as SNo, Route_No,TotalCustomer,NoSaleCust,((NoSaleCust*100)/TotalCustomer) as NoSalePer,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FilterFromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as FilterToDate,'" + customername + "' as FilterCustomerName,'" + Routename + "' as FilterRouteName,(TSPL_COMPANY_MASTER.Add1+ case when LEN(TSPL_COMPANY_MASTER.Add2)>0 then  ' ,'+TSPL_COMPANY_MASTER.Add2 else '' end +case when LEN(TSPL_COMPANY_MASTER.Add3)>0 then ' ,'+TSPL_COMPANY_MASTER.Add3 else '' end )as comapnyAdd,TSPL_COMPANY_MASTER.Comp_Name,TSPL_CITY_MASTER.City_Name from("
                FinalQry += " select Route_No,(select count(1) from TSPL_CUSTOMER_MASTER as NoOfTotalCust where NoOfTotalCust.Route_No=xxx.Route_No) as TotalCustomer,COUNT(Cust_Code) as NoSaleCust"
                FinalQry += " from(" + qry + ")xxx"
                FinalQry += " group by Route_No"
                FinalQry += " )Final "
                FinalQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
                FinalQry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER.City_Code "
                FinalQry += " order by Route_No"
                dt = clsDBFuncationality.GetDataTable(FinalQry)
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "rptNoSaleSummary", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        chkCustomerAll.IsChecked = True
        chkRouteAll.IsChecked = True
        chkcategoryall.IsChecked = True
        chktempall.IsChecked = True
    End Sub
    Sub LoadTemplate()
        Dim qry As String = " select distinct Tmplate_Id as [Template ID] , Description from TSPL_CUSTOMER_TEMPLATE_MASTER "
        cgvtemplate.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvtemplate.ValueMember = "Template ID"
        cgvtemplate.DisplayMember = "Description"
    End Sub

    Private Sub chktempall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktempall.ToggleStateChanged
        cgvtemplate.Enabled = Not chktempall.IsChecked
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "NO-SALE"
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

    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged, chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged, chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub

    Private Sub cboCustomerClass_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        LoadCustomer()
        chkCustomerAll.IsChecked = True
    End Sub

    Private Sub chkcategoryall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcategoryall.ToggleStateChanged, chkcategoryselect.ToggleStateChanged
        cbgcategory.Enabled = Not chkcategoryall.IsChecked
    End Sub

   
End Class
