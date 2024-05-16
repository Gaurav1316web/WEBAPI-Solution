'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------
'by vipin on 11/02/2013 for pdf work

Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports System.Data
Public Class frmRptPenetration
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptPenetration)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmRptPenetration_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print(False, 2)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub
    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        LoadType()
        LoadLocation()

        LoadRoute()
        SetDataBaseGrid()
        chkAllRoute.IsChecked = True
        'rbtnAllCompany.IsChecked = True
        rbtnSelectCompany.IsChecked = True
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompCode).Value), objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                gvDB.Rows(ii).Cells(colSelect).Value = 1
            End If
        Next


        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Sub LoadLocation()
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Private Sub LoadType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "HOS"
        dr("Name") = "HOS"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "TDM"
        dr("Name") = "TDM"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "ADC"
        dr("Name") = "ADC"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CE"
        dr("Name") = "CE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Salesman"
        dr("Name") = "Salesman"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

   

    '' ''Sub print()
    '' ''    Try
    '' ''        Dim strSalesmanColum As String = ""
    '' ''        If clsCommon.CompairString("HOS", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
    '' ''            strSalesmanColum = " TSPL_SALE_INVOICE_HEAD.Level2_User_code "
    '' ''        ElseIf clsCommon.CompairString("TDM", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
    '' ''            strSalesmanColum = " TSPL_SALE_INVOICE_HEAD.Level3_User_code "
    '' ''        ElseIf clsCommon.CompairString("ADC", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
    '' ''            strSalesmanColum = " TSPL_SALE_INVOICE_HEAD.Level4_User_code "
    '' ''        ElseIf clsCommon.CompairString("CE", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
    '' ''            strSalesmanColum = " TSPL_SALE_INVOICE_HEAD.Level5_User_code "
    '' ''        ElseIf clsCommon.CompairString("Salesman", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
    '' ''            strSalesmanColum = " TSPL_SALE_INVOICE_HEAD.Salesman_Code "
    '' ''        Else
    '' ''            RadMessageBox.Show("Please select Type")
    '' ''            cboType.Focus()
    '' ''            Return
    '' ''        End If
    '' ''        If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
    '' ''            RadMessageBox.Show("Please select at least one Location or select ALL")
    '' ''            Return
    '' ''        End If
    '' ''        Dim Baseqry As String = "select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,TSPL_SALE_INVOICE_HEAD.Cust_Code,TSPL_SALE_INVOICE_HEAD.Cust_Name, " + strSalesmanColum + " as SalesManCode,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesManName,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Route_Desc,TSPL_SALE_INVOICE_DETAIL.Item_Code as ICode,substring(TSPL_SALE_INVOICE_DETAIL.Item_Code,3,len(TSPL_SALE_INVOICE_DETAIL.Item_Code)) as ICodePack,(select top 1 Class_Desc from TSPL_ITEM_DETAILS where Class_Name='Flavour' and  Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code) as ICodeFlavour from TSPL_SALE_INVOICE_DETAIL"
    '' ''        Baseqry += " LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No"
    '' ''        Baseqry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=" + strSalesmanColum + ""
    '' ''        Baseqry += "  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_INVOICE_DETAIL.location"
    '' ''        Baseqry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "

    '' ''        If rbtnTransTypePosted.IsChecked Then
    '' ''            Baseqry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
    '' ''        End If

    '' ''        Dim qry As String = ""
    '' ''        Dim frm As New FrmSalerReport()

    '' ''        If rbtnSKUWise.IsChecked Then
    '' ''            qry = "select SalesManCode,SalesManName,Route_No,Route_Desc,ICode+' (%)' as ICode,NoOfCustCode,TotRouteCustomer,((Cast(NoOfCustCode as Decimal)*100)/TotRouteCustomer) as ItemPer,TSPL_COMPANY_MASTER.Comp_Name as FilterCompName,(TSPL_COMPANY_MASTER.Add1 +  case when len(TSPL_COMPANY_MASTER.Add2)>0 then ', '+TSPL_COMPANY_MASTER.Add2 else '' end + case when len(TSPL_COMPANY_MASTER.Add3)>0 then ', '+TSPL_COMPANY_MASTER.Add3 else '' end ) as FilterCompAdd ,TSPL_CITY_MASTER.City_Name,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FilterFromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as FilterToDate,'" + clsCommon.myCstr(cboType.SelectedValue) + "' as FilterReportType  "
    '' ''            qry += " from ( "
    '' ''            qry += " select SalesManCode,MAX(SalesManName) as SalesManName,Route_No,MAX(Route_Desc) as Route_Desc,ICode,COUNT(distinct Cust_Code) as NoOfCustCode,(select SUM(1)  from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Route_No=Final.Route_No) as TotRouteCustomer"
    '' ''            qry += " from( " + Baseqry + " "
    '' ''            '' -----------
    '' ''            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
    '' ''                qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    '' ''            End If
    '' ''            '' ------------
    '' ''            qry += ")Final group by SalesManCode,Route_No,ICode"

    '' ''            qry += " ) xxx left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
    '' ''            qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER .City_Code order by SalesManCode,Route_No,ICode"


    '' ''            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '' ''            frm.funreport(qry, "rptPenetrationSKU", Me.Text)
    '' ''        ElseIf rbtnPackWise.IsChecked Then
    '' ''            qry = "select SalesManCode,SalesManName,Route_No,Route_Desc,ICodePack+' (%)' as ICode,NoOfCustCode,TotRouteCustomer,((Cast(NoOfCustCode as Decimal)*100)/TotRouteCustomer) as ItemPer,TSPL_COMPANY_MASTER.Comp_Name as FilterCompName,(TSPL_COMPANY_MASTER.Add1 +  case when len(TSPL_COMPANY_MASTER.Add2)>0 then ', '+TSPL_COMPANY_MASTER.Add2 else '' end + case when len(TSPL_COMPANY_MASTER.Add3)>0 then ', '+TSPL_COMPANY_MASTER.Add3 else '' end ) as FilterCompAdd ,TSPL_CITY_MASTER.City_Name,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FilterFromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as FilterToDate,'" + clsCommon.myCstr(cboType.SelectedValue) + "' as FilterReportType  "
    '' ''            qry += " from ( "
    '' ''            qry += " select SalesManCode,MAX(SalesManName) as SalesManName,Route_No,MAX(Route_Desc) as Route_Desc,ICodePack,COUNT(distinct Cust_Code) as NoOfCustCode,(select SUM(1)  from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Route_No=Final.Route_No) as TotRouteCustomer"
    '' ''            qry += " from( " + Baseqry + ""
    '' ''            '---------
    '' ''            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
    '' ''                qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    '' ''            End If
    '' ''            '--------
    '' ''            qry += " )Final group by SalesManCode,Route_No,ICodePack"
    '' ''            qry += " ) xxx left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
    '' ''            qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER .City_Code order by SalesManCode,Route_No,ICodePack"



    '' ''            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '' ''            frm.funreport(qry, "rptPenetrationPack", Me.Text)
    '' ''        ElseIf rbtnFlavourWise.IsChecked Then
    '' ''            qry = "select SalesManCode,SalesManName,Route_No,Route_Desc,ICodeFlavour+' (%)' as ICode,NoOfCustCode,TotRouteCustomer,((Cast(NoOfCustCode as Decimal)*100)/TotRouteCustomer) as ItemPer,TSPL_COMPANY_MASTER.Comp_Name as FilterCompName,(TSPL_COMPANY_MASTER.Add1 +  case when len(TSPL_COMPANY_MASTER.Add2)>0 then ', '+TSPL_COMPANY_MASTER.Add2 else '' end + case when len(TSPL_COMPANY_MASTER.Add3)>0 then ', '+TSPL_COMPANY_MASTER.Add3 else '' end ) as FilterCompAdd ,TSPL_CITY_MASTER.City_Name,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FilterFromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as FilterToDate,'" + clsCommon.myCstr(cboType.SelectedValue) + "' as FilterReportType  "
    '' ''            qry += " from ( "
    '' ''            qry += " select SalesManCode,MAX(SalesManName) as SalesManName,Route_No,MAX(Route_Desc) as Route_Desc,ICodeFlavour,COUNT(distinct Cust_Code) as NoOfCustCode,(select SUM(1)  from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Route_No=Final.Route_No) as TotRouteCustomer"
    '' ''            qry += " from( " + Baseqry + ""
    '' ''            '------
    '' ''            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
    '' ''                qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
    '' ''            End If
    '' ''            '----------
    '' ''            qry += " )Final group by SalesManCode,Route_No,ICodeFlavour"
    '' ''            qry += " ) xxx left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
    '' ''            qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER .City_Code order by SalesManCode,Route_No,ICodeFlavour"


    '' ''            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '' ''            frm.funreport(qry, "rptPenetrationFlavour", Me.Text)
    '' ''        End If
    '' ''    Catch ex As Exception
    '' ''        RadMessageBox.Show(ex.Message)
    '' ''    End Try
    '' ''End Sub





    Sub print(byval chk As Boolean,ByVal exporter As EnumExportTo)
        Dim filterLocation As String = ""
        Dim filterRoute As String = ""
        Dim filterCompany As String
        Try
            Dim arrdatabase As New ArrayList
            arrdatabase = GetSelectedDatabasearrlist()
            If arrdatabase.Count = 1 Then
            Else
                RadMessageBox.Show("Select only one Company")
                Exit Sub
            End If




            Dim strSalesmanColum As String = ""
            If clsCommon.CompairString("HOS", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
                strSalesmanColum = " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Level2_User_code "
            ElseIf clsCommon.CompairString("TDM", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
                strSalesmanColum = " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Level3_User_code "
            ElseIf clsCommon.CompairString("ADC", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
                strSalesmanColum = " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Level4_User_code "
            ElseIf clsCommon.CompairString("CE", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
                strSalesmanColum = " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Level5_User_code "
            ElseIf clsCommon.CompairString("Salesman", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
                strSalesmanColum = " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code "
            Else
                RadMessageBox.Show("Please select Type")
                cboType.Focus()
                Return
            End If
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Location or select ALL")
                Return
            End If
            If chkSelectRoute.IsChecked = True AndAlso cbgroute.CheckedValue.Count > 0 Then
                filterRoute = "'" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + "'"
                filterRoute = filterRoute.Replace("'", "")
            End If
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                filterLocation = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
                filterLocation = filterLocation.Replace("'", "")
            End If
            filterCompany = clsCommon.GetMulcallString(arrdatabase)
            filterCompany = filterCompany.Replace("'", "")
            Dim Baseqry As String = "select " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name, " + strSalesmanColum + " as SalesManCode," + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name as SalesManName," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_Desc," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code as ICode,substring(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code,3,len(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code)) as ICodePack,(select top 1 Class_Desc from " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS where Class_Name='Flavour' and  Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code) as ICodeFlavour from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL"
            Baseqry += " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No"
            Baseqry += " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE=" + strSalesmanColum + ""
            Baseqry += "  left outer join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER .Location_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.location"
            Baseqry += "  left outer join  " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No"
            Baseqry += " where " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "

            If rbtnTransTypePosted.IsChecked Then
                Baseqry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
            End If

            Dim qry As String = ""

            If rbtnSKUWise.IsChecked Then
                qry = "select '" + filterLocation + "' as FLocation,  '" + filterRoute + "' as FRoute, SalesManCode,SalesManName,Route_No,Route_Desc,ICode+' (%)' as ICode,NoOfCustCode,TotRouteCustomer,((Cast(NoOfCustCode as Decimal)*100)/TotRouteCustomer) as ItemPer," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as FilterCompName,(" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add1 +  case when len(" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add2)>0 then ', '+" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add2 else '' end + case when len(" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add3)>0 then ', '+" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add3 else '' end ) as FilterCompAdd ," + clsCommon.ReplicateDBString + "TSPL_CITY_MASTER.City_Name,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FilterFromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as FilterToDate,'" + clsCommon.myCstr(cboType.SelectedValue) + "' as FilterReportType  "
                qry += " from ( "
                qry += " select SalesManCode,MAX(SalesManName) as SalesManName,Route_No,MAX(Route_Desc) as Route_Desc,ICode,COUNT(distinct Cust_Code) as NoOfCustCode,(select SUM(1)  from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Route_No=Final.Route_No) as TotRouteCustomer"
                qry += " from( " + Baseqry + " "
                '' -----------
                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If


                If chkSelectRoute.IsChecked = True Then
                    qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in  (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ")"
                End If



                '' ------------
                qry += ")Final group by SalesManCode,Route_No,ICode"

                qry += " ) xxxx left outer join " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
                qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_CITY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CITY_MASTER.City_Code=" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER .City_Code "


                ''Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                ''frm.funreport(qry, "rptPenetrationSKU", Me.Text)
            ElseIf rbtnPackWise.IsChecked Then
                qry = "select '" + filterLocation + "' as FLocation,  '" + filterRoute + "' as FRoute,  SalesManCode,SalesManName,Route_No,Route_Desc,ICodePack+' (%)' as ICode,NoOfCustCode,TotRouteCustomer,((Cast(NoOfCustCode as Decimal)*100)/TotRouteCustomer) as ItemPer," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as FilterCompName,(" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add1 +  case when len(" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add2)>0 then ', '+" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add2 else '' end + case when len(" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add3)>0 then ', '+" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add3 else '' end ) as FilterCompAdd ," + clsCommon.ReplicateDBString + "TSPL_CITY_MASTER.City_Name,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FilterFromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as FilterToDate,'" + clsCommon.myCstr(cboType.SelectedValue) + "' as FilterReportType  "
                qry += " from ( "
                qry += " select SalesManCode,MAX(SalesManName) as SalesManName,Route_No,MAX(Route_Desc) as Route_Desc,ICodePack,COUNT(distinct Cust_Code) as NoOfCustCode,(select SUM(1)  from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Route_No=Final.Route_No) as TotRouteCustomer"
                qry += " from( " + Baseqry + ""
                '---------
                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If


                If chkSelectRoute.IsChecked = True Then
                    qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in  (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ")"
                End If





                '--------
                qry += " )Final group by SalesManCode,Route_No,ICodePack"
                qry += " ) xxx left outer join " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
                qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_CITY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CITY_MASTER.City_Code=" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER .City_Code "



                'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                'frm.funreport(qry, "rptPenetrationPack", Me.Text)
            ElseIf rbtnFlavourWise.IsChecked Then
                qry = "select '" + filterLocation + "' as FLocation,  '" + filterRoute + "' as FRoute,  SalesManCode,SalesManName,Route_No,Route_Desc,ICodeFlavour+' (%)' as ICode,NoOfCustCode,TotRouteCustomer,((Cast(NoOfCustCode as Decimal)*100)/TotRouteCustomer) as ItemPer," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as FilterCompName,(" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add1 +  case when len(" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add2)>0 then ', '+" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add2 else '' end + case when len(" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add3)>0 then ', '+" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add3 else '' end ) as FilterCompAdd ," + clsCommon.ReplicateDBString + "TSPL_CITY_MASTER.City_Name,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FilterFromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as FilterToDate,'" + clsCommon.myCstr(cboType.SelectedValue) + "' as FilterReportType  "
                qry += " from ( "
                qry += " select SalesManCode,MAX(SalesManName) as SalesManName,Route_No,MAX(Route_Desc) as Route_Desc,ICodeFlavour,COUNT(distinct Cust_Code) as NoOfCustCode,(select SUM(1)  from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Route_No=Final.Route_No) as TotRouteCustomer"
                qry += " from( " + Baseqry + ""
                '------
                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If




                If chkSelectRoute.IsChecked = True Then
                    qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in  (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ")"
                End If

                '----------
                qry += " )Final group by SalesManCode,Route_No,ICodeFlavour"
                qry += " ) xxx left outer join " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
                qry += " left outer join " + clsCommon.ReplicateDBString + "TSPL_CITY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CITY_MASTER.City_Code=" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER .City_Code "


                'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                'frm.funreport(qry, "rptPenetrationFlavour", Me.Text)
            End If

            qry = clsCommon.GetQueryWithAllSelectedDataBase(qry, GetSelectedDatabase(), False)


            '----------------For grid work and Excel-------------------------


            Dim strItemCodestring, strItemCode, strMainItemCode, strmainItemCodeString, strsum As String
            Dim itemcount As String = ""

            strItemCodestring = ""
            strItemCode = ""
            strsum = ""
            strmainItemCodeString = ""

            If rbtnSKUWise.IsChecked Then
                itemcount = " select  distinct ICode  from (" + qry + ") abc  "

            ElseIf rbtnPackWise.IsChecked Then
                itemcount = " select  distinct ICode  from (" + qry + ") abc  "

            ElseIf rbtnFlavourWise.IsChecked Then
                itemcount = " select  distinct ICode  from (" + qry + ") abc  "
            End If




            ' Dim dr As SqlDataReader = connectSql.RunSqlReturnDR(itemcount)
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(itemcount)
            Dim arritem As New ArrayList
            ' While dr.Read

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    strItemCode = CStr(dr(0).ToString())
                    strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","
                    arritem.Add(strItemCode)
                    strMainItemCode = CStr(dr(0).ToString())
                    strmainItemCodeString = strmainItemCodeString & "  isnull(" & "[" & strItemCode & "]" & ",0)  " & "as  " & "[" & strItemCode & "]" & ","
                    'strsum = strItemCode + "+"

                    strsum = strsum & "  isnull(" & "[" & strItemCode & "]" & ",0)" & "+"
                Next
            End If
            'End While


            If strItemCode <> "" Then
                strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
                strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)
                strsum = strsum.Substring(0, strsum.Length - 1)
            Else
                RadMessageBox.Show("No Data Found to Display", Me.Text)
                Exit Sub
            End If

            Dim strmain As String = ""




            'If ddlprinttype.Text = "Detail" Then
            '    strmain = " select  comp_name as [Company Name],locdesc as [Depot], TDMName as Name,Route as [Route],routedesc as [Route Desc],invoiceno as [Invoice],InvoiceDate as [Invoice Date],cust as [Customer],custdesc as [Customer Desc] ," & strmainItemCodeString & " ,(" + strsum + ")as Total from(select Comp_Name,locDesc,TDMName,Route,routedesc,invoiceno,InvoiceDate,cust,custdesc,convert(decimal(18,2),round(sum(Qty),2)) as Qty,grouping from (" + qry + ")aaa group by aaa.grouping,aaa.Comp_Name,aaa.locDesc,aaa.TDMName,aaa.Route,aaa.routedesc,aaa.invoiceno,aaa.InvoiceDate,aaa.cust,aaa.custdesc) down pivot (SUM(qty) FOR grouping IN ( " & strItemCodestring & ")) AS pvt1 "
            'ElseIf ddlprinttype.Text = "Summary" Then
            '    strmain = " select  comp_name as [Company Name], TDMName as Name,Route as [Route],routedesc as [Route Desc] ," & strmainItemCodeString & " ,(" + strsum + ")as Total from(select Comp_Name,TDMName,Route,routedesc,convert(decimal(18,2),round(sum(Qty),2)) as Qty,grouping from (" + qry + ")aaa group by aaa.grouping,aaa.Comp_Name,aaa.TDMName,aaa.Route,aaa.routedesc) down pivot (SUM(qty) FOR grouping IN ( " & strItemCodestring & ")) AS pvt1 "
            'ElseIf ddlprinttype.Text = "Routewise" Then
            '    strmain = " select  comp_name as [Company Name],Route as [Route],routedesc as [Route Desc] ," & strmainItemCodeString & " ,(" + strsum + ")as Total from(select Comp_Name,Route,routedesc,convert(decimal(18,2),round(sum(Qty),2)) as Qty,grouping from (" + qry + ")aaa group by aaa.grouping,aaa.Comp_Name,aaa.Route,aaa.routedesc) down pivot (SUM(qty) FOR grouping IN ( " & strItemCodestring & ")) AS pvt1 "
            'ElseIf ddlprinttype.Text = "Hierarchywise" Then
            '    strmain = " select   TDMName as Name ," & strmainItemCodeString & " ,(" + strsum + ")as Total from(select TDMName,convert(decimal(18,2),round(sum(Qty),2)) as Qty,grouping from (" + qry + ")aaa group by aaa.grouping,aaa.TDMName) down pivot (SUM(qty) FOR grouping IN ( " & strItemCodestring & ")) AS pvt1 "

            'End If


            If rbtnSKUWise.IsChecked Then

                strmain = " select [SalesMan Name],[Route No],[Route Desc] ," & strmainItemCodeString & "  from(select SalesManCode as [SalesMan Code],SalesManName as [SalesMan Name],Route_No as [Route No],Route_Desc as [Route Desc],convert(decimal(18,2),round(sum(ItemPer),2)) as ItemPer,ICode from (" + qry + ")aaa group by aaa.icode,aaa.SalesManCode,aaa.SalesManName,aaa.Route_No,aaa.Route_Desc) down pivot (SUM(ItemPer) FOR ICode IN ( " & strItemCodestring & ")) AS pvt1 "

            ElseIf rbtnPackWise.IsChecked Then
                strmain = " select  [SalesMan Name],[Route No],[Route Desc] ," & strmainItemCodeString & "  from(select SalesManCode as [SalesMan Code],SalesManName as [SalesMan Name],Route_No as [Route No],Route_Desc as [Route Desc],convert(decimal(18,2),round(sum(ItemPer),2)) as ItemPer,ICode from (" + qry + ")aaa group by aaa.icode,aaa.SalesManCode,aaa.SalesManName,aaa.Route_No,aaa.Route_Desc) down pivot (SUM(ItemPer) FOR ICode IN ( " & strItemCodestring & ")) AS pvt1 "
            ElseIf rbtnFlavourWise.IsChecked Then
                strmain = " select [SalesMan Name],[Route No],[Route Desc] ," & strmainItemCodeString & "  from(select SalesManCode as [SalesMan Code],SalesManName as [SalesMan Name],Route_No as [Route No],Route_Desc as [Route Desc],convert(decimal(18,2),round(sum(ItemPer),2)) as ItemPer,ICode from (" + qry + ")aaa group by aaa.icode,aaa.SalesManCode,aaa.SalesManName,aaa.Route_No,aaa.Route_Desc) down pivot (SUM(ItemPer) FOR ICode IN ( " & strItemCodestring & ")) AS pvt1 "
            End If


            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(strmain)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
            End If
            gridformat(arritem)

            RadPageView1.SelectedPage = RadPageViewPage2



            '----------------------------------------------------------------

            If chk = True Then

                Dim head As String = ""
                Dim str As String = "Penetration Report"
                If rbtnSKUWise.IsChecked = True Then
                    head = "SKU Wise"
                ElseIf rbtnPackWise.IsChecked = True Then
                    head = "Pack Wise"
                ElseIf rbtnFlavourWise.IsChecked = True Then
                    head = "Flavour Wise"
                End If


                Dim arr As New List(Of String)()
                arr.Add(objCommonVar.CurrentCompanyName)
                arr.Add("Penetration Report ")
                arr.Add("" + cboType.Text + "-Wise")
                arr.Add(" Report Type:" + head + "")
                arr.Add("Start Date:-" + txtFromDate.Value + "")
                arr.Add("End Date:-" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "")
                arr.Add(" Location:-" + filterLocation + "")
                arr.Add(" Route:-" + filterRoute + "")
                ' clsCommon.MyExportToExcel(str, gv, arr, "PenetrationReport")

                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcel(str, gv, arr, "PenetrationReport")


                Else
                    clsCommon.MyExportToPDF(str, gv, arr, "PenetrationReport", True)
                End If
            Else
                'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If rbtnSKUWise.IsChecked Then
                    'qry += "order by SalesManCode,Route_No,ICode"
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(qry), "rptPenetrationSKU", Me.Text)
                ElseIf rbtnPackWise.IsChecked Then
                    ' qry += "order by SalesManCode,Route_No,ICodePack"
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(qry), "rptPenetrationPack", Me.Text)
                ElseIf rbtnFlavourWise.IsChecked Then
                    ' qry += "order by SalesManCode,Route_No,ICodeFlavour"
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(qry), "rptPenetrationFlavour", Me.Text)
                End If
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub gridformat(ByVal arritem As ArrayList)
        Try
            'gv.DataSource = Nothing
            'gv.Rows.Clear()
            'gv.Columns.Clear()

            gv.MasterTemplate.SummaryRowsBottom.Clear()

            Dim strItemCode As String
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            gv.AllowAddNewRow = False
           
            'If rbtnSKUWise.IsChecked Then

            gv.Columns("SalesMan Name").IsVisible = True
            gv.Columns("SalesMan Name").Width = 150
            gv.Columns("SalesMan Name").HeaderText = "SalesMan Name"


            gv.Columns("Route No").IsVisible = True
            gv.Columns("Route No").Width = 50
            gv.Columns("Route No").HeaderText = "Route No"

            gv.Columns("Route Desc").IsVisible = True
            gv.Columns("Route Desc").Width = 200
            gv.Columns("Route Desc").HeaderText = "Route Desc"



            For ii As Integer = 3 To gv.Columns.Count - 1
                intCount = intCount + 1
                strItemCode = gv.Columns(ii).FieldName
                Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Avg)
                summaryRowItem.Add(item16)
            Next

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            'For j As Integer = 0 To arritem.Count - 1
            '    gv.Columns(arritem.Item(j)).IsVisible = True
            '    gv.Columns(arritem.Item(j)).Width = 100
            '    gv.Columns(arritem.Item(j)).HeaderText = arritem.Item(j)

            'Next



            'End If

            For j As Integer = 0 To arritem.Count - 1
                gv.Columns(arritem.Item(j)).IsVisible = True
                gv.Columns(arritem.Item(j)).Width = 100
                gv.Columns(arritem.Item(j)).HeaderText = arritem.Item(j)

            Next
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)

        End Try

    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        reset()

    End Sub
    Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        cboType.SelectedIndex = 0
        chkLocatioAll.IsChecked = True
        LoadRoute()
        GetSelectedDatabase()
        chkAllRoute.IsChecked = True
        rbtnAllCompany.IsChecked = True

    End Sub
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SAL-PENET"
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

    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub chkLocatioAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = True
    End Sub

    Sub LoadRoute()
        Dim qry As String = "select route_no,route_desc from TSPL_ROUTE_MASTER order by route_no"
        cbgroute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgroute.ValueMember = "route_no"
        cbgroute.DisplayMember = "route_desc"

    End Sub


    '''''''''''''''''''''''''Fills The Data OF Filter 'Company''''
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
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub




    Private Sub chkAllRoute_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAllRoute.ToggleStateChanged
        cbgroute.Enabled = Not chkAllRoute.IsChecked
    End Sub

    Private Sub rbtnAllCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnAllCompany.ToggleStateChanged
        gvDB.Enabled = Not rbtnAllCompany.IsChecked
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

    Private Function GetSelectedDatabasearrlist() As ArrayList
        Dim arrDBList As New ArrayList
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If ((clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) OrElse rbtnAllCompany.IsChecked) Then
                arrDBList.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBList
    End Function

    Private Sub btnPrint_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print(False, 2)
    End Sub

    Private Sub chkLocatioAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocatioAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocatioAll.IsChecked
    End Sub

    Private Sub gv_ViewCellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub cbgroute_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbgroute.Load

    End Sub

    Private Sub RadGroupBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox4.Click

    End Sub

    Private Sub btnReset_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        print(True, EnumExportTo.Excel)
    End Sub

    Private Sub PDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        print(True, EnumExportTo.PDF)
    End Sub
End Class

