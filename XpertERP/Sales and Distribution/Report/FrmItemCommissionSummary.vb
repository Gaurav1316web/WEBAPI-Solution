''----08/06/2012--updation by shipra------on location's filter locations are being displayed from Segment Table
''----22/08/2012--updation by Pankaj Kumar-----if commission does not exists in FB then it Shows commission according to FC--Forwarded by---Ranjana Mam
'by vipin for retun qry on 1/11/12
'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------

Imports common
Public Class FrmItemCommissionSummary
    Inherits FrmMainTranScreen
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"

    Dim ButtonToolTip As ToolTip = New ToolTip()



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ItemCommissionSummary)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub



    Private Sub FrmItemCommissionSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        reset()
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")

        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ITM-CM-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
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

    '    End Try
    'End Function
    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
    End Sub

    Private Function GetSelectedDatabase() As List(Of String)
        Dim arrDBName As New List(Of String)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If ((clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) OrElse rbtnAllCompany.IsChecked) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function


    Sub Loademployee()
        Dim strquery As String = "select EMP_CODE as [Employee Code], Emp_Name as [Employee Name] from TSPL_EMPLOYEE_MASTER  "
        cgvemployee.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cgvemployee.ValueMember = "Employee Code"
        cgvemployee.DisplayMember = "Employee Name"
    End Sub

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



    Sub LoadRoute()
        Dim qry As String = " select Route_No,Route_Desc from TSPL_ROUTE_MASTER"
        cgvRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvRoute.ValueMember = "Route_No"
        cgvRoute.DisplayMember = "Route_Desc"
    End Sub

    Sub LoadCustomerCategory()
        Dim qry As String = "select Cust_Type_Code as [Code],Cust_Type_Desc as [Name] from TSPL_CUSTOMER_type_master"
        cbgcategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgcategory.ValueMember = "Code"
        cbgcategory.DisplayMember = "Name"
    End Sub



    Private Sub chkempall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkempall.ToggleStateChanged
        cgvemployee.Enabled = Not chkempall.IsChecked
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub rbtnAllCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnAllCompany.ToggleStateChanged
        gvDB.Enabled = Not rbtnAllCompany.IsChecked
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub


    Public Sub reset()
        LoadCustomerCategory()
        LoadLocation()
        Loademployee()
        SetDataBaseGrid()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        chkLocAll.IsChecked = True
        chkempall.IsChecked = True
        'rbtnAllCompany.IsChecked = True
        rbtnSelectCompany.IsChecked = True
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompCode).Value), objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                gvDB.Rows(ii).Cells(colSelect).Value = 1
            End If
        Next

        LoadRoute()
        chkAllroute.IsChecked = True
        rdoSummary.IsChecked = True
        chkcategoryall.IsChecked = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If chkcategoryselect.IsChecked = True AndAlso cbgcategory.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer Category or select ALL ")
                Return
            ElseIf chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                Return
            ElseIf chkempselect.IsChecked = True AndAlso cgvemployee.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least employee or select ALL")
                Return
            ElseIf chkrotesel.IsChecked = True AndAlso cgvRoute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least route or select ALL")
                Return
         
            End If
            ' Dim qtyReturn, qryqty, qryconqty As String
            'Dim strReturnQty As String

            Dim level As String = ""
            Dim Hierarchy As String = ""
            If clsCommon.CompairString(ddlhier.Text, "HOS") = CompairStringResult.Equal Then
                level = "Level2_User_Code"
                Hierarchy = "HOS"
            ElseIf clsCommon.CompairString(ddlhier.Text, "TDM") = CompairStringResult.Equal Then
                level = "Level3_User_Code"
                Hierarchy = "TDM"
            ElseIf clsCommon.CompairString(ddlhier.Text, "ADC") = CompairStringResult.Equal Then
                level = "Level4_User_Code"
                Hierarchy = "ADC"
            ElseIf clsCommon.CompairString(ddlhier.Text, "CE") = CompairStringResult.Equal Then
                level = "Level5_User_Code"
                Hierarchy = "CE"
            ElseIf clsCommon.CompairString(ddlhier.Text, "Sales Man") = CompairStringResult.Equal Then
                level = "Salesman_Code"
                Hierarchy = "RA"
            End If



            Dim qry As String = " select " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD." + level + " as Emp_Code, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc , " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code,(isnull((case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code in ('FC','EC')  then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0) ) AS QTY, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ,(select top 1 Commission  from " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History where " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_Commission_Master_History.UOM IN ('FC', 'EC') and " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.Cust_Group=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>= " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.Start_Date and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<= " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.End_Date AND TSPL_Commission_Master_History .Hierarchy='" + Hierarchy + "' ) as CommosionRateHist, " & _
                      " (select top 1 Commission  from " + clsCommon.ReplicateDBString + "TSPL_Commission_Master " & _
                      " where " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code " & _
                      " and " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.UOM IN ('FC', 'EC') and " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.Cust_Group= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>= " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.Start_Date AND TSPL_Commission_Master .Hierarchy='" + Hierarchy + "' ) as CommosionRate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code , " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc , " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code , " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name " & _
                      " from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL " & _
                      " left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
                      " left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD." + level + " " & _
                      " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
                      " Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
                      " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code  and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code " & _
                      " Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code " & _
                      " where Convert(Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103)>=Convert(Date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "', 103) and COnvert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103)<= convert(Date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "', 103) and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post='Y'  "

            If chkLocSelect.IsChecked Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in  (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            End If
            If chkempselect.IsChecked Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE in  (" + clsCommon.GetMulcallString(cgvemployee.CheckedValue) + ")  "
            End If
            If chkcategoryselect.IsChecked Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code in  (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")  "
            End If
            If chkrotesel.IsChecked Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in  (" + clsCommon.GetMulcallString(cgvRoute.CheckedValue) + ")  "
            End If


            '---for return ----------------


            Dim qry1 As String = " select " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code ," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No as Sale_Invoice_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_Date as Sale_Invoice_Date, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Name, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD." + level + " as Emp_Code, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_Desc , " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code,-1 *(isnull((case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code in ('FC','EC')  then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0) ) AS QTY, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ,(select top 1 Commission  from " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History where " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code and TSPL_Commission_Master_History.UOM IN ('FC', 'EC') and " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.Cust_Group=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_Date>= " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.Start_Date and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_Date<= " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.End_Date AND TSPL_Commission_Master_History .Hierarchy='" + Hierarchy + "' ) as CommosionRateHist, " & _
                     " (select top 1 Commission  from " + clsCommon.ReplicateDBString + "TSPL_Commission_Master " & _
                     " where " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code " & _
                     " and " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.UOM IN ('FC', 'EC') and " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.Cust_Group= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_Date>= " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.Start_Date AND TSPL_Commission_Master .Hierarchy='" + Hierarchy + "' ) as CommosionRate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code , " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc , " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code , " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name " & _
                     " from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL " & _
                     " left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No " & _
                     " left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD." + level + " " & _
                     " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code " & _
                     " Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
                     " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code  and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code " & _
                     " Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code " & _
                     " where Convert(Date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_Date, 103)>=Convert(Date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "', 103) and COnvert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_Date, 103)<= convert(Date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "', 103) and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Is_Post='Y'  "

            If chkLocSelect.IsChecked Then
                qry1 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location in  (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
            End If
            If chkempselect.IsChecked Then
                qry1 += " and " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE in  (" + clsCommon.GetMulcallString(cgvemployee.CheckedValue) + ")  "
            End If
            If chkcategoryselect.IsChecked Then
                qry1 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code in  (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")  "
            End If
            If chkrotesel.IsChecked Then
                qry1 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No in  (" + clsCommon.GetMulcallString(cgvRoute.CheckedValue) + ")  "
            End If



            Dim baseqry As String = qry + "  Union all   " + qry1






            If rdoSummary.IsChecked = True Then
                'qry = "select Max(fDate) as fDate, Max(tDAte) as tDate, Max(Name) as Name, Emp_Code, Max(Emp_Name) as Emp_Name,Sum(qty) as Qty , Sum(CommissionAmt) as Commission ,  Comp_name as Comp_Name  from ( " + qry + "  )xxxxx ) yyy Group By Emp_Code, Route , Item, commission, Location_Code  )xxx Group  By Comp_name, Emp_Code  "
                qry = "select Max(fDate) as fDate, Max(tDAte) as tDate, Max(Name) as Name, Emp_Code, Max(Emp_Name) as Emp_Name,Sum(qty) as Qty , Sum(CommissionAmt) as Commission ,  Comp_name as Comp_Name  from ( Select '" + txtFromDate.Value + "' as  fDate, '" + txtToDate.Value + "' as tDAte, '" + ddlhier.Text + "' as Name, Emp_Code, MAX(Emp_Name) as Emp_Name, Route, MAX(RouteDesc ) as RouteDesc, Item , SUM(qty) as QTY, Commission, (SUM(qty)* Commission)as CommissionAmt,MAX(Comp_Code ) as CompCode, Max(Comp_Name ) as Comp_name, Location_Code    From ( " & _
                     " select  Sale_Invoice_No, Sale_Invoice_Date, Cust_Group_Code, Cust_Code, Cust_Name, Emp_Code , Emp_Name, Route_No as Route,Route_Desc as RouteDesc,Item_Code as Item, QTY, Unit_code as UOM, Location_Code, Location_Desc  , " & _
                     " (case when isnull(CommosionRateHist,0)<>0 then CommosionRateHist else isnull(CommosionRate,0) end) as commission, Comp_Code, Comp_Name " & _
                     " from(" + baseqry + " )asd )xxxxx Group By Emp_Code, Route , Item, commission, Location_Code) yyy Group By Emp_Code,Comp_name    "
            Else
                'qry += "   )xxxxx ) yyy Group By Emp_Code, Route , Item, commission, Location_Code "

                qry = "Select '" + txtFromDate.Value + "' as  fDate, '" + txtToDate.Value + "' as tDAte, '" + ddlhier.Text + "' as Name, Emp_Code, MAX(Emp_Name) as Emp_Name, Route, MAX(RouteDesc ) as RouteDesc, Item , SUM(qty) as QTY, Commission, (SUM(qty)* Commission)as CommissionAmt,MAX(Comp_Code ) as CompCode, Max(Comp_Name ) as Comp_name, Location_Code as Location   From (" & _
                     " select  Sale_Invoice_No, Sale_Invoice_Date, Cust_Group_Code, Cust_Code, Cust_Name, Emp_Code , Emp_Name, Route_No as Route,Route_Desc as RouteDesc,Item_Code as Item, QTY, Unit_code as UOM, Location_Code, Location_Desc  , " & _
                     " (case when isnull(CommosionRateHist,0)<>0 then CommosionRateHist else isnull(CommosionRate,0) end) as commission, Comp_Code, Comp_Name " & _
                     " from (  " + baseqry + "  )xxxxx ) yyy Group By Emp_Code, Route , Item, commission, Location_Code "
            End If
            qry = clsCommon.GetQueryWithAllSelectedDataBase(qry, GetSelectedDatabase(), False)


            Dim dt As DataTable
            If rdoSummary.IsChecked Then
                dt = clsDBFuncationality.GetDataTable(qry)
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptItemCommissionSummaryReport", "Item Commission Summary")
            ElseIf rdoDetail.IsChecked Then
                dt = clsDBFuncationality.GetDataTable(qry)
                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptItemCommissionDetailsReport", "Item Commission Summary")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    ' ''Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
    ' ''    Try
    ' ''        If chkcategoryselect.IsChecked = True AndAlso cbgcategory.CheckedValue.Count <= 0 Then
    ' ''            common.clsCommon.MyMessageBoxShow("Please select at least one Customer Category or select ALL ")
    ' ''            Return
    ' ''        ElseIf chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
    ' ''            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
    ' ''            Return
    ' ''        ElseIf chkempselect.IsChecked = True AndAlso cgvemployee.CheckedValue.Count <= 0 Then
    ' ''            common.clsCommon.MyMessageBoxShow("Please select at least employee or select ALL")
    ' ''            Return
    ' ''        ElseIf chkrotesel.IsChecked = True AndAlso cgvRoute.CheckedValue.Count <= 0 Then
    ' ''            common.clsCommon.MyMessageBoxShow("Please select at least route or select ALL")
    ' ''            Return

    ' ''        End If
    ' ''        Dim qtyReturn, qryqty, qryconqty As String
    ' ''        Dim strReturnQty As String

    ' ''        Dim level As String
    ' ''        Dim Hierarchy As String
    ' ''        If clsCommon.CompairString(ddlhier.Text, "HOS") = CompairStringResult.Equal Then
    ' ''            level = "Level2_User_Code"
    ' ''            Hierarchy = "HOS"
    ' ''        ElseIf clsCommon.CompairString(ddlhier.Text, "TDM") = CompairStringResult.Equal Then
    ' ''            level = "Level3_User_Code"
    ' ''            Hierarchy = "TDM"
    ' ''        ElseIf clsCommon.CompairString(ddlhier.Text, "ADC") = CompairStringResult.Equal Then
    ' ''            level = "Level4_User_Code"
    ' ''            Hierarchy = "ADC"
    ' ''        ElseIf clsCommon.CompairString(ddlhier.Text, "CE") = CompairStringResult.Equal Then
    ' ''            level = "Level5_User_Code"
    ' ''            Hierarchy = "CE"
    ' ''        ElseIf clsCommon.CompairString(ddlhier.Text, "Sales Man") = CompairStringResult.Equal Then
    ' ''            level = "Salesman_Code"
    ' ''            Hierarchy = "RA"
    ' ''        End If
    ' ''        'Dim qry As String = "   Select '" + txtFromDate.Value + "' as  fDate, '" + txtToDate.Value + "' as tDAte, '" + ddlhier.Text + "' as Name, Emp_Code, MAX(Emp_Name) as Emp_Name, Route, MAX(RouteDesc ) as RouteDesc, Item , SUM(qty) as QTY, Commission, (SUM(qty)* Commission)as CommissionAmt,MAX(Comp_Code ) as CompCode, Max(Comp_Name ) as Comp_name, Location_Code as Location   From (" & _
    ' ''        '          " select  Sale_Invoice_No, Sale_Invoice_Date, Cust_Group_Code, Cust_Code, Cust_Name, Emp_Code , Emp_Name, Route_No as Route,Route_Desc as RouteDesc,Item_Code as Item, QTY, Unit_code as UOM, Location_Code, Location_Desc  , " & _
    ' ''        '          " (case when isnull(CommosionRateHist,0)<>0 then CommosionRateHist else isnull(CommosionRate,0) end) as commission, Comp_Code, Comp_Name " & _
    ' ''        '          " from ( " & _
    ' ''        '          " select " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD." + level + " as Emp_Code, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc , " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code,(isnull((case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code in ('FC','EC')  then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0) ) AS QTY, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ,(select top 1 Commission  from " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History where " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_Commission_Master_History.UOM IN ('FC', 'EC') and " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.Cust_Group=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>= " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.Start_Date and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<= " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.End_Date AND TSPL_Commission_Master_History .Hierarchy='" + Hierarchy + "' ) as CommosionRateHist, " & _
    ' ''        '          " (select top 1 Commission  from " + clsCommon.ReplicateDBString + "TSPL_Commission_Master " & _
    ' ''        '          " where " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code " & _
    ' ''        '          " and " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.UOM IN ('FC', 'EC') and " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.Cust_Group= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>= " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.Start_Date AND TSPL_Commission_Master .Hierarchy='" + Hierarchy + "' ) as CommosionRate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code , " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc , " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code , " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name " & _
    ' ''        '          " from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL " & _
    ' ''        '          " left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
    ' ''        '          " left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD." + level + " " & _
    ' ''        '          " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
    ' ''        '          " Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
    ' ''        '          " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code  and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code " & _
    ' ''        '          " Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code " & _
    ' ''        '          " where Convert(Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103)>=Convert(Date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "', 103) and COnvert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103)<= convert(Date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "', 103) and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post='Y'  "



    ' ''        qryconqty = "(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) end  )  "
    ' ''        qtyReturn = "(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) end  )  "
    ' ''        qryqty = "( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=(select distinct " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Scheme_Item=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item  ) then  (" + qryconqty + "-(select top 1  " + qtyReturn + " as Return_Qty from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.unit_code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code)  )  else  " + qryconqty + "    end )AS QTY "



    ' ''        Dim qry As String = "   Select '" + txtFromDate.Value + "' as  fDate, '" + txtToDate.Value + "' as tDAte, '" + ddlhier.Text + "' as Name, Emp_Code, MAX(Emp_Name) as Emp_Name, Route, MAX(RouteDesc ) as RouteDesc, Item , SUM(qty) as QTY, Commission, (SUM(qty)* Commission)as CommissionAmt,MAX(Comp_Code ) as CompCode, Max(Comp_Name ) as Comp_name, Location_Code as Location   From (" & _
    ' ''                " select  Sale_Invoice_No, Sale_Invoice_Date, Cust_Group_Code, Cust_Code, Cust_Name, Emp_Code , Emp_Name, Route_No as Route,Route_Desc as RouteDesc,Item_Code as Item, QTY, Unit_code as UOM, Location_Code, Location_Desc  , " & _
    ' ''                " (case when isnull(CommosionRateHist,0)<>0 then CommosionRateHist else isnull(CommosionRate,0) end) as commission, Comp_Code, Comp_Name " & _
    ' ''                " from ( " & _
    ' ''                " select " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code ," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD." + level + " as Emp_Code, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc , " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code," + qryqty + ", " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ,(select top 1 Commission  from " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History where " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_Commission_Master_History.UOM IN ('FC', 'EC') and " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.Cust_Group=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>= " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.Start_Date and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<= " + clsCommon.ReplicateDBString + "TSPL_Commission_Master_History.End_Date AND TSPL_Commission_Master_History .Hierarchy='" + Hierarchy + "' ) as CommosionRateHist, " & _
    ' ''                " (select top 1 Commission  from " + clsCommon.ReplicateDBString + "TSPL_Commission_Master " & _
    ' ''                " where " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code " & _
    ' ''                " and " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.UOM IN ('FC', 'EC') and " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.Cust_Group= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>= " + clsCommon.ReplicateDBString + "TSPL_Commission_Master.Start_Date AND TSPL_Commission_Master .Hierarchy='" + Hierarchy + "' ) as CommosionRate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code , " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc , " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code , " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name " & _
    ' ''                " from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL " & _
    ' ''                " left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
    ' ''                " left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD." + level + " " & _
    ' ''                " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
    ' ''                " Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
    ' ''                " left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code  and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code " & _
    ' ''                " Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code " & _
    ' ''                " where Convert(Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103)>=Convert(Date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "', 103) and COnvert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103)<= convert(Date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "', 103) and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post='Y'  "





    ' ''        If chkLocSelect.IsChecked Then
    ' ''            qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in  (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))  "
    ' ''        End If
    ' ''        If chkempselect.IsChecked Then
    ' ''            qry += " and " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE in  (" + clsCommon.GetMulcallString(cgvemployee.CheckedValue) + ")  "
    ' ''        End If
    ' ''        If chkcategoryselect.IsChecked Then
    ' ''            qry += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code in  (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")  "
    ' ''        End If
    ' ''        If chkrotesel.IsChecked Then
    ' ''            qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in  (" + clsCommon.GetMulcallString(cgvRoute.CheckedValue) + ")  "
    ' ''        End If
    ' ''        If rdoSummary.IsChecked = True Then
    ' ''            qry = "select Max(fDate) as fDate, Max(tDAte) as tDate, Max(Name) as Name, Emp_Code, Max(Emp_Name) as Emp_Name,Sum(qty) as Qty , Sum(CommissionAmt) as Commission ,  Comp_name as Comp_Name  from ( " + qry + "  )xxxxx ) yyy Group By Emp_Code, Route , Item, commission, Location_Code  )xxx Group  By Comp_name, Emp_Code  "
    ' ''        Else
    ' ''            qry += "   )xxxxx ) yyy Group By Emp_Code, Route , Item, commission, Location_Code "
    ' ''        End If
    ' ''        qry = clsCommon.GetQueryWithAllSelectedDataBase(qry, GetSelectedDatabase(), False)


    ' ''        Dim dt As DataTable
    ' ''        If rdoSummary.IsChecked Then
    ' ''            Dim frm As New SalesReportViewer()
    ' ''            dt = clsDBFuncationality.GetDataTable(qry)
    ' ''            frm.funreport(dt, "crptItemCommissionSummaryReport", "Item Commission Summary")
    ' ''        ElseIf rdoDetail.IsChecked Then
    ' ''            Dim frm As New SalesReportViewer()
    ' ''            dt = clsDBFuncationality.GetDataTable(qry)
    ' ''            frm.funreport(dt, "crptItemCommissionDetailsReport", "Item Commission Summary")
    ' ''        End If
    ' ''    Catch ex As Exception
    ' ''        common.clsCommon.MyMessageBoxShow(ex.Message)
    ' ''    End Try
    ' ''End Sub





    Private Sub chkAllroute_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAllroute.ToggleStateChanged
        cgvRoute.Enabled = Not chkAllroute.IsChecked
    End Sub

    Private Sub chkcategoryall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcategoryall.ToggleStateChanged
        cbgcategory.Enabled = Not chkcategoryall.IsChecked
    End Sub

   
    Private Sub FrmItemCommissionSummary_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            'printreport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub
End Class
