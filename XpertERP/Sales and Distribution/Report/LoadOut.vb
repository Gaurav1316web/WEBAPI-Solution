'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 07/06/2012-------------------------------------
'--------------------------------Last modify Time - 11:00 AM -------------------------------------

Imports common
Public Class LoadOut
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.LoadOutReport1)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub LoadOut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            printdata()
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


    Private Sub LoadOut_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpstart.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE())
        dtpend.Value = clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE())
        ' ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print Preview")



        chklocAll.IsChecked = True
        'chkcustAll.IsChecked = True
        chktypeAll.IsChecked = True
        chkClassAll.IsChecked = True
        ddlReportType.Text = "All"
        ddlReceived.Text = "Both"
        ddlType.Text = "Quantity"
        Loadlocation()
        LoadType()
        LoadCustomerClass()
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "LD-RPT"
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

    Sub Loadlocation()
        'Dim qry As String = "select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbglocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbglocation.ValueMember = "Code"
        cbglocation.DisplayMember = "Description"
    End Sub

    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt = GetItemType()
        ' Dim qry As String = "select CUST_CATEGORY_CODE,CUST_CATEGORY_DESC from TSPL_CUSTOMER_CATEGORY_MASTER order by CUST_CATEGORY_CODE "
        'cbgtype.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgtype.DataSource = dt
        cbgtype.ValueMember = "Code"
        cbgtype.DisplayMember = "Code"
    End Sub

    'Sub LoadCustomerCatgory()
    '    Dim qry As String = "select CUST_CATEGORY_CODE,CUST_CATEGORY_DESC from TSPL_CUSTOMER_CATEGORY_MASTER order by CUST_CATEGORY_CODE "
    '    cbgcustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgcustomer.ValueMember = "CUST_CATEGORY_CODE"
    '    cbgcustomer.DisplayMember = "CUST_CATEGORY_DESC"
    'End Sub




    Private Sub chklocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocAll.ToggleStateChanged
        cbglocation.Enabled = Not chklocAll.IsChecked
    End Sub



    Private Sub RadButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub LoadCustomerClass()
        Dim qry As String = "select Cust_Type_Code as [Code],Cust_Type_Desc as [Name] from TSPL_CUSTOMER_type_master"
        chkCustomerClass.DataSource = clsDBFuncationality.GetDataTable(qry)
        chkCustomerClass.ValueMember = "Code"
        chkCustomerClass.DisplayMember = "Name"
    End Sub
    Function LoadClass() As DataTable

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        'dr("Code") = ""
        'dr("Name") = "Select"
        'dt.Rows.Add(dr)

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


        Return dt
    End Function

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        printdata()
    End Sub
    Sub printdata()
        Dim strFromDateTime As String = clsCommon.GetPrintDate(dtpstart.Value, "dd/MMM/yyyy hh:mm tt")
        Dim strToDateTime As String = clsCommon.GetPrintDate(dtpend.Value, "dd/MMM/yyyy hh:mm tt")

        GetItemType()
        If chklocSelect.IsChecked = True AndAlso cbglocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            Return
        ElseIf chktypeSelect.IsChecked = True AndAlso cbgtype.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Item or select ALL")
            Return
        ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomerClass.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one customer class or select ALL")
            Return

        End If

        Dim strCustClass, strCust, strLocAll, strItemAll, strReceorIssue, strValue As String
        strReceorIssue = ""
        If chklocAll.IsChecked = True Then
            strLocAll = "Y"
        Else
            strLocAll = "N"
        End If
        If chktypeAll.IsChecked = True Then
            strItemAll = "Y"
        Else
            strItemAll = "N"
        End If
        If chkClassAll.IsChecked = True Then
            strCustClass = "Y"
        Else
            strCustClass = "N"
        End If

        If ddlReceived.Text = "Both" Then
            strReceorIssue = ""
        ElseIf ddlReceived.Text = "Issued" Then
            strReceorIssue = "I"
        ElseIf ddlReceived.Text = "Received" Then
            strReceorIssue = "R"
        End If

        If ddlType.Text = "Value" Then
            strValue = "V"
        Else
            strValue = "Q"
        End If
        Dim strSql1 As String = "SELECT CONVERT(date, TSPL_ADJUSTMENT_HEADER.Adjustment_Date, 103) AS date, TSPL_ADJUSTMENT_DETAIL.Item_Code, " & _
                     "TSPL_ADJUSTMENT_DETAIL.Item_Description, TSPL_ADJUSTMENT_DETAIL.Location_Code AS Location, 0 AS LoadOutQty, " & _
                    " 0 AS LoadInQty,0 AS LoadOutAmt, ISNULL(TSPL_ADJUSTMENT_DETAIL.Item_Cost, 0) AS LoadInamt,  " & _
                    "  TSPL_ADJUSTMENT_HEADER.Comp_Code, 'A' AS Type, '" + strFromDateTime + "' AS Fdate, '" + strToDateTime + "' AS Tdate, " & _
                    "TSPL_LOCATION_MASTER.Location_Desc,'' as strLoc,'" & strReceorIssue & "' as RecdorIssue,Customer_Class,Cust_Type_Desc,'" & strValue & "' as Value " & _
                    "FROM TSPL_ADJUSTMENT_HEADER INNER JOIN " & _
                    "TSPL_ADJUSTMENT_DETAIL ON TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No INNER JOIN " & _
                    "TSPL_LOCATION_MASTER ON TSPL_ADJUSTMENT_DETAIL.Location_Code = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                     "TSPL_ITEM_UOM_DETAIL ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                     "TSPL_ADJUSTMENT_DETAIL.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " & _
                     "TSPL_CUSTOMER_MASTER ON TSPL_ADJUSTMENT_HEADER.Customer_CODE = TSPL_CUSTOMER_MASTER.Cust_Code inner join TSPL_CUSTOMER_TYPE_MASTER on TSPL_CUSTOMER_MASTER.Customer_Class=TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code " & _
                    "WHERE (TSPL_ADJUSTMENT_HEADER.ItemType = 'E') AND  Location_Type <> 'Logical' and " & _
                    "(TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice' or TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '') AND (TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI') " & _
                    " and  TSPL_ADJUSTMENT_HEADER.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
                    "  TSPL_ADJUSTMENT_HEADER.EntryDateTime <=  '" & strToDateTime & "' and Posted='Y'"
        Dim Un1 As String = "Union All "
        'Dim strSql2 As String = "SELECT TSPL_TRANSFER_HEAD.Transfer_Date as date, TSPL_TRANSFER_DETAIL.Item_Code , " & _
        '            "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_TRANSFER_HEAD.From_Location as location, " & _
        '            "TSPL_TRANSFER_DETAIL.Item_Qty   as LoadOutQty,0 as LoadInQty,( Item_Qty * mrp) as amt, " & _
        '            "TSPL_TRANSFER_HEAD.Comp_Code, 'A' AS Type,convert(date, '" & dtpstart.Value & "',103) AS Fdate, convert(date, '" & dtpend.Value & "',103) AS Tdate,TSPL_LOCATION_MASTER.Location_Desc, " & _
        '            "'' as strLoc FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
        '            "TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
        '            "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER.Location_Code where " & _
        '            "Item_Type='Empty' and  Location_Type <> 'Logical' AND Transfer_Type='LO' " & _
        '            " and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date, '" & dtpend.Value & "',103) "
        'Dim Un2 As String = "Union All "
        'Dim strSql3 As String = " SELECT TSPL_TRANSFER_HEAD.Transfer_Date as date, TSPL_TRANSFER_DETAIL.Item_Code , " & _
        '            "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_TRANSFER_HEAD.To_Location as location,  0   as LoadOutQty, " & _
        '            "TSPL_TRANSFER_DETAIL.Item_Qty as LoadInQty,( Item_Qty * mrp) as amt, TSPL_TRANSFER_HEAD.Comp_Code, " & _
        '            "'A' AS Type,convert(date, '" & dtpstart.Value & "',103) AS Fdate, convert(date, '" & dtpend.Value & "',103) AS Tdate,TSPL_LOCATION_MASTER.Location_Desc,'' as strLoc " & _
        '            "FROM TSPL_TRANSFER_HEAD INNER JOIN " & _
        '            "TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
        '            "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER.Location_Code " & _
        '            "where  Item_Type='Empty' and  Location_Type <> 'Logical' AND Transfer_Type='LI' " & _
        '            " and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date, '" & dtpend.Value & "',103) "
        'Dim Un3 As String = "Union All "
        Dim strSql4 As String = "SELECT TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date AS date, " & _
                    "TSPL_SALE_INVOICE_DETAIL.Item_Code, TSPL_SALE_INVOICE_DETAIL.Item_Desc, " & _
                    "TSPL_SALE_INVOICE_DETAIL.Location,  (TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) AS LoadOutQty,0 AS LoadInQty, " & _
                    "(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt + isnull(TSPL_SALE_INVOICE_DETAIL.Empty_Value,0)) AS LoadOutamt,0 as LoadInamt, TSPL_SALE_INVOICE_HEAD.Comp_Code, 'B' AS Type, " & _
                    " '" + strFromDateTime + "' AS Fdate, '" + strToDateTime + "' AS Tdate," & _
                    "TSPL_LOCATION_MASTER.Location_Desc,'' as strLoc,'" & strReceorIssue & "' as RecdorIssue,Customer_Class,Cust_Type_Desc,'" & strValue & "' as Value " & _
                    "FROM TSPL_SALE_INVOICE_HEAD INNER JOIN " & _
                    "TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                    "TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_DETAIL.Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                      "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                      "TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " & _
                      "TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code inner join TSPL_CUSTOMER_TYPE_MASTER on TSPL_CUSTOMER_MASTER.Customer_Class=TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code " & _
                    " where " & _
                    " TSPL_SALE_INVOICE_HEAD.Date_Time_Removal >=  '" & strFromDateTime & "' AND " & _
                    " TSPL_SALE_INVOICE_HEAD.Date_Time_Removal <=  '" & strToDateTime & "'  and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        Dim Un4 As String = "Union All "
        Dim strSql5 As String = "SELECT distinct TSPL_TRANSFER_HEAD.Transfer_Date AS date, TSPL_TRANSFER_DETAIL.Item_Code, " & _
                        "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_TRANSFER_HEAD.To_Location AS location, 0 AS LoadOutQty, " & _
                        "(TSPL_TRANSFER_DETAIL.LoadIn_Qty/Conversion_Factor) + Burst/Conversion_Factor + Leak/Conversion_Factor  +  shortage/Conversion_Factor  AS LoadInQty,0 as LoadOutamt, " & _
                      "(TSPL_TRANSFER_DETAIL.LoadIn_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value)) AS LoadInamt, " & _
                      "TSPL_TRANSFER_HEAD.Comp_Code, 'B' AS Type, '" + strFromDateTime + "' AS Fdate," & _
                      " '" + strToDateTime + "' AS Tdate, TSPL_LOCATION_MASTER_1.Location_Desc, " & _
                      "'' AS strLoc,'" & strReceorIssue & "' as RecdorIssue,tspl_customer_master.Customer_Class,TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc,'" & strValue & "' as Value " & _
                      "FROM  TSPL_CUSTOMER_TYPE_MASTER INNER JOIN " & _
                      "TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = TSPL_CUSTOMER_MASTER.Cust_Type_Code INNER JOIN " & _
                      "TSPL_LOCATION_MASTER INNER JOIN " & _
                      "TSPL_TRANSFER_DETAIL INNER JOIN " & _
                      "TSPL_TRANSFER_HEAD ON TSPL_TRANSFER_DETAIL.Transfer_No = TSPL_TRANSFER_HEAD.Transfer_No ON " & _
                      "TSPL_LOCATION_MASTER.Location_Code = TSPL_TRANSFER_HEAD.From_Location INNER JOIN " & _
                      "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER_1.Location_Code INNER JOIN " & _
                      "TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                      "TSPL_TRANSFER_DETAIL.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " & _
                      "TSPL_SHIPMENT_MASTER ON TSPL_TRANSFER_HEAD.Load_Out_No = TSPL_SHIPMENT_MASTER.Transfer_No INNER JOIN " & _
                      "TSPL_SALE_INVOICE_HEAD ON TSPL_SHIPMENT_MASTER.Shipment_No = TSPL_SALE_INVOICE_HEAD.Shipment_No ON " & _
                      "TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
                        "WHERE (TSPL_LOCATION_MASTER.Location_Type = 'logical') AND (TSPL_TRANSFER_HEAD.Transfer_Type = 'LI') AND " & _
                        " TSPL_TRANSFER_HEAD.EntryDateTime  >=  '" & strFromDateTime & "' AND " & _
                        " TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "' and post='Y'"

        'Dim strSql5 As String = "SELECT TSPL_TRANSFER_HEAD.Transfer_Date AS date, TSPL_TRANSFER_DETAIL.Item_Code, " & _
        '                "TSPL_TRANSFER_DETAIL.Item_Desc,TSPL_TRANSFER_HEAD.To_Location AS location, 0 AS LoadOutQty, " & _
        '                "(TSPL_TRANSFER_DETAIL.Item_Qty/Conversion_Factor) AS LoadInQty,0 as LoadOutamt, " & _
        '              "(TSPL_TRANSFER_DETAIL.LoadIn_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value)) + TSPL_TRANSFER_DETAIL.Empty_Value AS LoadInamt, " & _
        '              "TSPL_TRANSFER_HEAD.Comp_Code, 'B' AS Type, CONVERT(date, '" & dtpstart.Value & "', 103) AS Fdate," & _
        '              "CONVERT(date, '" & dtpend.Value & "', 103) AS Tdate, TSPL_LOCATION_MASTER_1.Location_Desc, " & _
        '              "'' AS strLoc,'" & strReceorIssue & "' as RecdorIssue,'S' as Customer_Class,'SUPER DISTRIBUTOR' as Cust_Type_Desc,'" & strValue & "' as Value FROM         TSPL_LOCATION_MASTER INNER JOIN " & _
        '              "TSPL_TRANSFER_DETAIL INNER JOIN " & _
        '              "TSPL_TRANSFER_HEAD ON TSPL_TRANSFER_DETAIL.Transfer_No = TSPL_TRANSFER_HEAD.Transfer_No ON " & _
        '              "TSPL_LOCATION_MASTER.Location_Code = TSPL_TRANSFER_HEAD.From_Location INNER JOIN " & _
        '              "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON " & _
        '              "TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER_1.Location_Code INNER JOIN " & _
        '              "TSPL_ITEM_UOM_DETAIL ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
        '                "TSPL_TRANSFER_DETAIL.Uom = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
        '                "WHERE (TSPL_LOCATION_MASTER.Location_Type = 'logical') AND (TSPL_TRANSFER_HEAD.Transfer_Type = 'LI') AND " & _
        '                "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
        '                "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date, '" & dtpend.Value & "',103)  and " & _
        '                "convert(time,Date_Time_Removal,103) > = CONVERT(time,'" & dtpStarttime.Value & "' ,103) and " & _
        '                "convert(time,Date_Time_Removal,103) < = CONVERT(time,'" & dtpendtime.Value & "' ,103) "
        'Dim Un5 As String = "Union All "
        Dim strSql6 As String = "SELECT TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date AS date, " & _
                    "TSPL_SALE_INVOICE_DETAIL.Item_Code, TSPL_SALE_INVOICE_DETAIL.Item_Desc, " & _
                    "TSPL_SALE_INVOICE_DETAIL.Location,  0 AS LoadOutQty,(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) AS LoadInQty, " & _
                    "0 AS LoadOutamt,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt + isnull(TSPL_SALE_INVOICE_DETAIL.Empty_Value,0)) as LoadInamt, TSPL_SALE_INVOICE_HEAD.Comp_Code, 'B' AS Type, " & _
                    " '" + strFromDateTime + "' AS Fdate, '" + strToDateTime + "' AS Tdate," & _
                    "TSPL_LOCATION_MASTER.Location_Desc,'' as strLoc,'" & strReceorIssue & "' as RecdorIssue,Customer_Class,Cust_Type_Desc,'" & strValue & "' as Value " & _
                    "FROM TSPL_SALE_INVOICE_HEAD INNER JOIN " & _
                    "TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                    "TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_DETAIL.Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                      "TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                      "TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " & _
                      "TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code inner join TSPL_CUSTOMER_TYPE_MASTER on " & _
                      "TSPL_CUSTOMER_MASTER.Customer_Class=TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code INNER JOIN " & _
                      "TSPL_SALE_RETURN_HEAD ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_RETURN_HEAD.Cust_Code AND " & _
                      "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_RETURN_HEAD.Invoice_No INNER JOIN " & _
                      "TSPL_SALE_RETURN_DETAIL ON TSPL_SALE_RETURN_HEAD.Sale_Return_No = TSPL_SALE_RETURN_DETAIL.Sale_Return_No AND  " & _
                      "TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_SALE_RETURN_DETAIL.Item_Code  where " & _
                    " TSPL_SALE_INVOICE_HEAD.Date_Time_Removal >=  '" & strFromDateTime & "' AND " & _
                    " TSPL_SALE_INVOICE_HEAD.Date_Time_Removal <=  '" & strToDateTime & "' and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"

        If strLocAll = "N" Then
            strSql1 += " and TSPL_ADJUSTMENT_DETAIL.Location_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            'strSql2 += " and TSPL_TRANSFER_HEAD.From_Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
            'strSql3 += " and TSPL_TRANSFER_HEAD.To_Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
            strSql4 += " and TSPL_SALE_INVOICE_DETAIL.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            strSql5 += " and TSPL_TRANSFER_HEAD.To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            strSql6 += " and TSPL_SALE_INVOICE_DETAIL.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
        End If
        If strItemAll = "N" Then
            strSql1 += " and TSPL_ADJUSTMENT_DETAIL.Unit_Code in (" + clsCommon.GetMulcallString(cbgtype.CheckedValue) + ") "
            strSql4 += " and TSPL_SALE_INVOICE_DETAIL.Unit_Code in (" + clsCommon.GetMulcallString(cbgtype.CheckedValue) + ") "
            strSql5 += " and TSPL_TRANSFER_detail.UOM in (" + clsCommon.GetMulcallString(cbgtype.CheckedValue) + ") "
            strSql6 += " and TSPL_SALE_INVOICE_DETAIL.Unit_Code in (" + clsCommon.GetMulcallString(cbgtype.CheckedValue) + ") "
        End If
        Dim strquery As String
        If strCustClass = "N" Then
            strCust = "" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ""
            strSql1 += " and TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ")"
            strSql4 += " and TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ")"
            strSql5 += " and TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ")"

        End If

        strquery = strSql1 & Un1 & strSql4 & Un4 & strSql5 & Un4 & strSql6

        frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strquery), "crptLoadOutReport", "LoadOut Report")


        'If strCustClass = "N" Then
        '    If strCust.Contains("S") = True Then
        '        strquery = strSql1 & Un1 & strSql4 & Un4 & strSql5
        '    Else
        '        strquery = strSql1 & Un1 & strSql4
        '    End If
        'Else
        'End If
        'If cboCustomerClass.Text <> "Select" Then
        '    strSql1 += " and TSPL_CUSTOMER_MASTER.Customer_Class='" & cboCustomerClass.SelectedValue & "'"
        '    strSql4 += " and TSPL_CUSTOMER_MASTER.Customer_Class='" & cboCustomerClass.SelectedValue & "'"
        '    strquery = strSql1 & Un1 & strSql4
        'Else
        '    strquery = strSql1 & Un1 & strSql4 & Un4 & strSql5
        'End If

        'Dim strquery = strSql1 & Un1 & strSql2 & Un2 & strSql3 & Un3 & strSql4 & Un4 & strSql5 & Un5 & strSql6
        'strquery = clsCommon.GetQueryWithAllSelectedDataBase(strquery, ArrDBName, False)


    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click

        reset()
    End Sub
    Sub reset()

        chklocAll.IsChecked = True
        chkClassAll.IsChecked = True
        chktypeAll.IsChecked = True

        ddlReportType.Text = "All"
        ddlReceived.Text = "Both"
        ddlType.Text = "Quantity"


        Loadlocation()
        LoadType()
    End Sub
    Public Shared Function GetItemType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))


        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "EC"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "FC"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "EB"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "FB"
        dt.Rows.Add(dr)


        Return dt
    End Function


    Private Sub chktypeAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktypeAll.ToggleStateChanged
        cbgtype.Enabled = Not chktypeAll.IsChecked
    End Sub

    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkClassAll.ToggleStateChanged
        chkCustomerClass.Enabled = Not chkClassAll.IsChecked
    End Sub
End Class
