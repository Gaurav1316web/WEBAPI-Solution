'''' last change by priti on 30.08.2012 on 07:30 pm
'''' 20/10/2012-1:29PM---Updation By--Pankaj Kumar in Report Qry for Viewing the [TransferNo (L/O LoadIn No)] Whether transtype if LoadIn---Fwd By---Amit Sir
''''By vipin for pdf on 08/02/2013 
Imports XpertERPEngine
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Public Class FrmOtherPartySale1
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String


    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        ' dr = connectSql.RunSqlReturnDR(sql)
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                ' dr.Read()
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()
            Next
        End If
    End Sub



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.OtherPartySale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmOtherPartySale1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print(False, 0)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub



    Private Sub FrmOtherPartySale1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE())
        txtToDate.Value = clsCommon.GetDateWithEndTime(clsCommon.GETSERVERDATE())
        rdbSku.IsChecked = True
        chklocAll.IsChecked = True
        chkCustAll.IsChecked = True
        ChkVehicleAll.IsChecked = True
        chkClassAll.IsChecked = True

        LoadCustomer()
        LoadCustomerClass()
        Loadlocation()
        LoadVehicle()
        ddlType.Text = "Both"
        ddlUnitType.Text = "Both"
        ddlConvert.Text = "Converted"
        rdbWithoutVehicle.IsChecked = True
        grpVehicle.Visible = False
        LoadTemplate()
        chktempall.IsChecked = True
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")

        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "OT-PRT-SL"
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
        ' Dim qry As String = "select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbglocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbglocation.ValueMember = "Code"
        'cbglocation.DisplayMember = "Description"
        Dim qry As String = "Select  LM.Location_Code as Code,LM.Location_Desc as Description,Location_type as 'Location Type',(case LM.Excisable when 'T'then 'Excisable'else 'Non-Excisable'end) as 'Excisable'  from TSPL_LOCATION_MASTER as LM where    Location_Type = 'Physical' "
        cbglocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbglocation.ValueMember = "Code"
        cbglocation.DisplayMember = "Name"
    End Sub

    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name],Customer_Class as [Customer Class] from TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Code"
    End Sub
    Sub LoadVehicle()
        Dim qry As String = "Select Vehicle_Id as [Vehicle Code],Number as [Vehicle No] from TSPL_VEHICLE_MASTER ORDER BY Number"
        cbgVehicle.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVehicle.ValueMember = "Vehicle Code"
        cbgVehicle.DisplayMember = "Vehicle Code"
    End Sub
    Private Sub chklocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocAll.ToggleStateChanged
        cbglocation.Enabled = Not chklocAll.IsChecked
    End Sub


    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
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
    Private Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub
    Sub reset()
        chkexcel.Checked = False
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        rdbSku.IsChecked = True
        chklocAll.IsChecked = True
        chkCustAll.IsChecked = True
        ChkVehicleAll.IsChecked = True
        chkClassAll.IsChecked = True
        'ddlType.Text = "Quantity"
        LoadCustomerClass()
        Loadlocation()
        LoadCustomer()
        ddlType.Text = "Both"
        ddlUnitType.Tag = "Both"
        ddlConvert.Text = "Converted"
        rdbWithoutVehicle.IsChecked = True
        grpVehicle.Visible = False
        LoadVehicle()
        LoadTemplate()
        chktempall.IsChecked = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()


    End Sub
    Private Sub btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprint.Click
        print(False, 3)


    End Sub
    Sub print(ByVal chk As Boolean, ByVal exporter As EnumExportTo)
        Dim strFromDateTime As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy hh:mm tt")
        Dim strToDateTime As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy hh:mm tt")

        Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")

        Dim strFromTime As String = clsCommon.GetPrintDate(txtFromDate.Value, "hh:mm tt")
        Dim strToTime As String = clsCommon.GetPrintDate(txtToDate.Value, "hh:mm tt")



        If chklocSelect.IsChecked = True AndAlso cbglocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            Return
        ElseIf chkChkSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Customer or select ALL")
            Return
        ElseIf chkVehicleSelect.IsChecked = True AndAlso cbgVehicle.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Vehicle or select ALL")
            Return
        ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomerClass.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Customer Type or select ALL")
            Return
        End If
        Dim strUnit, strTempAll, strCust, strCustAll, strLocAll, strValue, strVehicle, strCustClass, strSQL5Group, strSQL1Group, strSQL2Group, strSQL3Group, strSQL4Group, strOrderColumn, strOrderBy As String
        strSQL4Group = ""
        strOrderBy = ""
        strCust = ""
        strValue = ""
        strOrderColumn = ""
        strSQL2Group = ""
        strSQL3Group = ""
        strSQL1Group = ""
        strUnit = ""
        If rdbSku.IsChecked = True Then
            strSQL1Group = "TSPL_TRANSFER_DETAIL.Item_Code"
            strSQL2Group = "TSPL_SALE_INVOICE_DETAIL.Item_Code"
            strSQL3Group = "TSPL_SRN_DETAIL.Item_Code"
            strSQL4Group = "TSPL_ADJUSTMENT_DETAIL.Item_Code"
            strSQL5Group = "TSPL_SALE_RETURN_DETAIL.Item_Code"
            strOrderColumn = "TSPL_ITEM_MASTER.Sku_Seq"
            strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
        ElseIf rdbPack.IsChecked = True Then
            strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc"
            strSQL2Group = " TSPL_ITEM_DETAILS.Class_Desc"
            strSQL3Group = " TSPL_ITEM_DETAILS.Class_Desc"
            strSQL4Group = " TSPL_ITEM_DETAILS.Class_Desc"
            strSQL5Group = " TSPL_ITEM_DETAILS.Class_Desc"
            strOrderColumn = "TSPL_ITEM_MASTER.Pack_Seq"
            strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
        ElseIf rdbFlavour.IsChecked = True Then
            strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
            strSQL2Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
            strSQL3Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
            strSQL4Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
            strSQL5Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
            strOrderColumn = "TSPL_ITEM_MASTER.Flavour_Seq"
            strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"
        End If

        If chklocAll.IsChecked = True Then
            strLocAll = "Y"
        Else
            strLocAll = "N"
        End If
        If ChkVehicleAll.IsChecked = True Then
            strVehicle = "Y"
        Else
            strVehicle = "N"
        End If
        If chkCustAll.IsChecked = True Then
            strCustAll = "Y"
        Else
            strCustAll = "N"
        End If

        If ddlType.Text = "Quantity" Then
            strValue = "Q"
        ElseIf ddlType.Text = "Value" Then
            strValue = "V"
        ElseIf ddlType.Text = "Both" Then
            strValue = "B"
        End If

        If ddlUnitType.Text = "Filled" Then
            strUnit = "F"
        ElseIf ddlUnitType.Text = "Empty" Then
            strUnit = "E"
        ElseIf ddlUnitType.Text = "Both" Then
            strUnit = "B"
        End If

        If chkClassAll.IsChecked = True Then
            strCustClass = "Y"
        Else
            strCustClass = "N"
        End If
        If chktempall.IsChecked = True Then
            strTempAll = "Y"
        Else
            strTempAll = "N"
        End If

        Dim strSubQry1, strSubQry2, strSubQry3, strSubQry4 As String
        strSubQry4 = ""
        strSubQry3 = ""
        strSubQry1 = ""
        strSubQry2 = ""
        If ddlConvert.Text = "Converted" Then
            strSubQry1 = "1"
            strSubQry2 = "1"
            strSubQry3 = "1"
            strSubQry4 = "1"
        ElseIf ddlConvert.Text = "8oz" Then
            strSubQry1 = "1"
            strSubQry2 = "1"
            strSubQry3 = "1"
            strSubQry4 = "1"
        ElseIf ddlConvert.Text = "Raw" Then
            strSubQry1 = "1"
            strSubQry2 = "1"
            strSubQry3 = "1"
            strSubQry4 = "1"
        End If


        '' 1 . Sale Invoice detail Qty as Delivered qty
        Dim strSql1 As String = "SELECT TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo, TSPL_SALE_INVOICE_HEAD.Cust_Code, CONVERT(DATE,CONVERT(VARCHAR,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103),103) AS Date, " & _
        " " & strSQL2Group & " as Item_Code, TSPL_SALE_INVOICE_DETAIL.Item_Desc, " & _
        "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty * " & strSubQry1 & " AS Deliverqty,0 as RecdQty, TSPL_SALE_INVOICE_DETAIL.Unit_code, " & _
        "TSPL_CUSTOMER_MASTER.Customer_Name,(TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt + isnull(TSPL_SALE_INVOICE_DETAIL.Empty_Value,0)) * " & strSubQry1 & " as Saleprice, " & _
        "0 as SRNPrice,TSPL_SALE_INVOICE_DETAIL.MRP_Amt as MRP, CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate, " & _
        "CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate, TSPL_LOCATION_MASTER.Location_Desc,'" & strValue & "' as value " & _
        ",'" + strFromDateTime + "' as startTime,'" + strToDateTime + "' as endtime, " & _
        "Vehicle_Code,Vehicle_No," & strOrderColumn & " as OrderBy,convert(time,Date_Time_Removal,103) as Doctime FROM TSPL_SALE_INVOICE_HEAD INNER JOIN " & _
        "TSPL_SALE_INVOICE_DETAIL ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
        "TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code INNER JOIN " & _
        "TSPL_LOCATION_MASTER ON TSPL_SALE_INVOICE_DETAIL.Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
            "TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
            "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
            "inner join TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
        "WHERE (TSPL_ITEM_DETAILS.Class_Name = 'size') AND " & _
        "(TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
        " Date_Time_Removal > =  '" & strFromDateTime & "' and " & _
        " Date_Time_Removal < =  '" & strToDateTime & "' and Is_Post='Y' "

        ''9 . Sale Return  Qty as Receipt qty
        Dim strSql9 As String = "SELECT TSPL_SALE_RETURN_HEAD.Sale_Return_No AS DocNo, TSPL_SALE_RETURN_HEAD.Cust_Code, " & _
        "CONVERT(DATE,CONVERT(VARCHAR,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103),103) AS Date, TSPL_SALE_RETURN_DETAIL.Item_Code, " & _
        "TSPL_SALE_RETURN_DETAIL.Item_Desc, 0 AS Deliverqty, TSPL_SALE_RETURN_DETAIL.Return_Qty * 1 AS RecdQty, " & _
        "TSPL_SALE_RETURN_DETAIL.Unit_code, TSPL_CUSTOMER_MASTER.Customer_Name, " & _
        "0 AS Saleprice, " & _
        "(TSPL_SALE_RETURN_DETAIL.Total_Item_Amt + ISNULL(TSPL_SALE_RETURN_DETAIL.Empty_Value,0)) * 1 AS SRNPrice, TSPL_SALE_RETURN_DETAIL.MRP_Amt AS MRP, CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate, " & _
        "CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate, TSPL_LOCATION_MASTER.Location_Desc,'" & strValue & "' as value " & _
        ",'" + strFromDateTime + "' as startTime,'" + strToDateTime + "' as endtime, TSPL_SALE_RETURN_HEAD.Vehicle_Code, " & _
        "TSPL_SALE_RETURN_HEAD.Vehicle_No,TSPL_ITEM_MASTER.Sku_Seq AS OrderBy, " & _
        "CONVERT(time, TSPL_SALE_RETURN_HEAD.Sale_Return_Date, 103) AS Doctime " & _
        "FROM  TSPL_SALE_RETURN_HEAD LEFT OUTER JOIN TSPL_SALE_RETURN_DETAIL ON " & _
        "TSPL_SALE_RETURN_HEAD.Sale_Return_No = TSPL_SALE_RETURN_DETAIL.Sale_Return_No LEFT OUTER JOIN " & _
        "TSPL_ITEM_DETAILS ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
        "TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
        "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code LEFT OUTER JOIN " & _
        "TSPL_ITEM_MASTER ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " & _
        "TSPL_LOCATION_MASTER ON TSPL_SALE_RETURN_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code " & _
        "WHERE (TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') AND  " & _
        "(TSPL_SALE_RETURN_HEAD.Sale_Return_Date >= '" & strFromDateTime & "') AND  " & _
        "(TSPL_SALE_RETURN_HEAD.Sale_Return_Date <= '" & strToDateTime & "') AND " & _
        "(TSPL_SALE_RETURN_HEAD.Is_Post = 'Y')"

        Dim strUn1 As String = "Union All "

        '''' 2. SRN Details Qty as Recd. qty
        Dim strSql2 As String = "select TSPL_SRN_HEAD.SRN_No as DOcno ,TSPL_CUSTOMER_MASTER.Cust_Code, CONVERT(DATE,CONVERT(VARCHAR,TSPL_SRN_HEAD.SRN_Date,103),103) as date, " & _
        " " & strSQL3Group & " as Item_Code,TSPL_SRN_DETAIL.Item_Desc,0 as Deliverqty,TSPL_SRN_DETAIL.srn_qty * " & strSubQry2 & " as RecdQty, " & _
        "TSPL_SRN_DETAIL.Unit_code,TSPL_CUSTOMER_MASTER.Customer_Name,0 as Saleprice, " & _
        "(TSPL_SRN_DETAIL.Item_Net_Amt) * " & strSubQry2 & " as SRNPrice,TSPL_SRN_DETAIL.MRP as MRP,CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate, " & _
        "CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate,TSPL_LOCATION_MASTER.Location_Desc,'" & strValue & "' as value, " & _
        "'" + strFromDateTime + "' as startTime,'" + strToDateTime + "' as endtime, " & _
        "TSPL_SRN_HEAD.Vendor_Code,VehicleNo as Vehicle_No," & strOrderColumn & " as OrderBy,convert(time,SRN_Date,103) as Doctime from TSPL_SRN_HEAD inner join " & _
        "TSPL_SRN_DETAIL on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No inner join tspl_customer_vendor_mapping on " & _
        "TSPL_SRN_HEAD.Vendor_Code=tspl_customer_vendor_mapping.vendor_code inner join TSPL_CUSTOMER_MASTER ON " & _
        "tspl_customer_vendor_mapping.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code INNER JOIN " & _
        "TSPL_LOCATION_MASTER ON TSPL_SRN_DETAIL.Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
            "TSPL_ITEM_DETAILS ON TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
            "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
            "inner join TSPL_ITEM_MASTER on TSPL_SRN_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
        "WHERE (TSPL_ITEM_DETAILS.Class_Name = 'size') AND " & _
        "(TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
        " SRN_Date  > = '" + strFromDateTime + "' and " & _
        " SRN_Date  < = '" + strToDateTime + "' and Posting_Date <> '' "
        Dim strUn2 As String = "Union All "

        '''' 10. SaleReturnInterCompany as Recd. qty

        Dim strSql10 As String = "SELECT TSPL_SALE_RETURN_INTER_HEAD.Document_No as DocNo, TSPL_SALE_RETURN_INTER_HEAD.Cust_Code, " & _
        "CONVERT(DATE,CONVERT(VARCHAR,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103),103) AS Date,  TSPL_SALE_RETURN_INTER_DETAIL.Item_Code as Item_Code,  " & _
        "TSPL_SALE_RETURN_INTER_DETAIL.Item_Desc, 0 AS Deliverqty,TSPL_SALE_RETURN_INTER_DETAIL.Qty * 1 as RecdQty,  " & _
        "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code, TSPL_CUSTOMER_MASTER.Customer_Name,  " & _
        "0 as Saleprice,  " & _
        "(TSPL_SALE_RETURN_INTER_DETAIL.Total_Item_Amt + isnull(TSPL_SALE_RETURN_INTER_DETAIL.Empty_Value,0)) * 1 as SRNPrice,TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt as MRP, CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate,  " & _
        "CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate, TSPL_LOCATION_MASTER.Location_Desc,'B' as value ,  " & _
        "'" + strFromDateTime + "' as startTime,'" + strToDateTime + "' as endtime, Vehicle_Code,Vehicle_No," & strOrderColumn & " as OrderBy, " & _
        "convert(time,Document_Date,103) as Doctime FROM TSPL_SALE_RETURN_INTER_HEAD INNER JOIN TSPL_SALE_RETURN_INTER_DETAIL ON  " & _
        "TSPL_SALE_RETURN_INTER_HEAD.Document_No = TSPL_SALE_RETURN_INTER_DETAIL.Document_No INNER JOIN TSPL_CUSTOMER_MASTER ON  " & _
        "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code INNER JOIN TSPL_LOCATION_MASTER ON  " & _
        "TSPL_SALE_RETURN_INTER_HEAD.Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN TSPL_ITEM_DETAILS ON  " & _
        "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN TSPL_ITEM_DETAILS AS  " & _
        "TSPL_ITEM_DETAILS_1 ON TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code inner join  " & _
        "TSPL_ITEM_MASTER on TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code WHERE  " & _
        "(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and   " & _
        "Document_Date > =  '" + strFromDateTime + "' and  Document_Date < =  '" + strToDateTime + "' and Is_Post=1 "
        Dim strSql3, strSql4, strSql5, strSql6 As String

        '''' 3. Transfer Load Out detail where from location and To location both are physical and from location qty is Delivered QTy
        If strLocAll = "N" Then
            strSql3 = "SELECT TSPL_TRANSFER_HEAD.Transfer_No as DocNo ,TSPL_TRANSFER_HEAD.To_Location as Cust_Code,CONVERT(DATE,CONVERT(VARCHAR,TSPL_TRANSFER_HEAD.Transfer_Date,103),103) as Date, " & _
        " " & strSQL1Group & " as Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc,( case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then  TSPL_TRANSFER_DETAIL.Item_Qty else 0 end) * " & strSubQry3 & " as DeliverQty, " & _
        "0 as RecdQty,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_LOCATION_MASTER_1.Location_Desc as Customer_Name, " & _
        "(TSPL_TRANSFER_DETAIL.Item_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value ) ) * " & strSubQry3 & " as SalesPrice,0 as SRNPrice,TSPL_TRANSFER_DETAIL.MRP as MRP, " & _
        "CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate, CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate, " & _
        "TSPL_LOCATION_MASTER.Location_Desc,'" & strValue & "' as value ,'" + strFromDateTime + "' as startTime, " & _
        "'" + strToDateTime + "' as endtime,Vehicle_Code,Vehicle_No," & strOrderColumn & " as OrderBy,convert(time,EntryDateTime,103) as DocTime FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
        "TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
        "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
        "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER_1.Location_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                     "inner join TSPL_ITEM_MASTER on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
        "where (TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
        "TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER_1.Location_Type='Physical' and " & _
        "TSPL_TRANSFER_HEAD.Transfer_Type='LO' and " & _
        " TSPL_TRANSFER_HEAD.Transfer_Date  >=  '" & strFromDateTime & "' AND " & _
        " TSPL_TRANSFER_HEAD.Transfer_Date  <=  '" & strToDateTime & "'  and Post='Y'"
            ''"convert(time,Date_Time_Removal,103) > = CONVERT(time,'" & dtpStarttime.Value & "' ,103) and " & _
            '    '"convert(time,Date_Time_Removal,103) < = CONVERT(time,'" & dtpendtime.Value & "' ,103) "
        Else
            strSql3 = "SELECT TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_TRANSFER_HEAD.From_Location as Cust_Code,CONVERT(DATE,CONVERT(VARCHAR,TSPL_TRANSFER_HEAD.Transfer_Date,103),103) as Date, " & _
        " " & strSQL1Group & " as Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc,( case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then  TSPL_TRANSFER_DETAIL.Item_Qty else 0 end) * " & strSubQry3 & " as DeliverQty, " & _
        "0 as RecdQty,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_LOCATION_MASTER.Location_Desc as Customer_Name, " & _
        "(TSPL_TRANSFER_DETAIL.Item_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value ) ) * " & strSubQry3 & " as SalesPrice,0 as SRNPrice,TSPL_TRANSFER_DETAIL.MRP as MRP, " & _
        "CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate, CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate, " & _
        "TSPL_LOCATION_MASTER.Location_Desc,'" & strValue & "' as value ,'" + strFromDateTime + "' as startTime, " & _
        "'" + strToDateTime + "' as endtime,Vehicle_Code,Vehicle_No," & strOrderColumn & " as OrderBy,convert(time,EntryDateTime,103)  as Doctime FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
        "TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
        "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
        "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER_1.Location_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                     "inner join TSPL_ITEM_MASTER on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
        "where (TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
        "TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER_1.Location_Type='Physical' and " & _
        "TSPL_TRANSFER_HEAD.Transfer_Type='LO' and " & _
        " TSPL_TRANSFER_HEAD.EntryDateTime >=  '" & strFromDateTime & "'  AND " & _
        "  TSPL_TRANSFER_HEAD.EntryDateTime  <=  '" & strToDateTime & "' and Post='Y'"
            ''"convert(time,Date_Time_Removal,103) > = CONVERT(time,'" & dtpStarttime.Value & "' ,103) and " & _
            '    '"convert(time,Date_Time_Removal,103) < = CONVERT(time,'" & dtpendtime.Value & "' ,103) "
        End If


        Dim strUn3 As String = "Union All "

        '' 4 Transfer Load Out detail where from location and To location both are physical and from location qty is Received QTy
        If strLocAll = "N" Then

            strSql4 = " SELECT TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_TRANSFER_HEAD.From_Location as Cust_Code, " & _
                   "CONVERT(DATE,CONVERT(VARCHAR,TSPL_TRANSFER_HEAD.Transfer_Date,103),103) as Date," & strSQL1Group & " as Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc, " & _
                   "0 as DeliverQty,( case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then  TSPL_TRANSFER_DETAIL.Item_Qty else 0 end) * " & strSubQry3 & " as RecdQty,TSPL_TRANSFER_DETAIL.Uom as Unit_Code, " & _
                   "TSPL_LOCATION_MASTER.Location_Desc as Customer_Name,0 as SalesPrice,(TSPL_TRANSFER_DETAIL.Item_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value ) ) * " & strSubQry3 & " as SRNPrice, " & _
                   "TSPL_TRANSFER_DETAIL.MRP as MRP,CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate, " & _
                   "CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate, TSPL_LOCATION_MASTER_1.Location_Desc, " & _
                   "'" & strValue & "' as value ,'" + strFromDateTime + "' as startTime, " & _
                   "'" + strToDateTime + "' as endtime,Vehicle_Code,Vehicle_No," & strOrderColumn & " as OrderBy,convert(time,EntryDateTime,103)  as DocTime " & _
                   "FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
                   "TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
                   "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                   "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER_1.Location_Code INNER JOIN " & _
                               "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                               "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                               "inner join TSPL_ITEM_MASTER on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  where " & _
                   "(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
                   "TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER_1.Location_Type='Physical' and " & _
                   "TSPL_TRANSFER_HEAD.Transfer_Type='LO' and " & _
                   " TSPL_TRANSFER_HEAD.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
                   " TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "' and Post='Y'"
            '       '"convert(time,Date_Time_Removal,103) > = CONVERT(time,'" & dtpStarttime.Value & "' ,103) and " & _
            ''"convert(time,Date_Time_Removal,103) < = CONVERT(time,'" & dtpendtime.Value & "' ,103) "
        Else
            strSql4 = " SELECT TSPL_TRANSFER_HEAD.Transfer_No as DocNo,TSPL_TRANSFER_HEAD.To_Location as Cust_Code, " & _
        "CONVERT(DATE,CONVERT(VARCHAR,TSPL_TRANSFER_HEAD.Transfer_Date,103),103) as Date," & strSQL1Group & " as Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc, " & _
        "0 as DeliverQty,( case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then  TSPL_TRANSFER_DETAIL.Item_Qty else 0 end) * " & strSubQry3 & " as RecdQty,TSPL_TRANSFER_DETAIL.Uom as Unit_Code, " & _
        "TSPL_LOCATION_MASTER_1.Location_Desc as Customer_Name,0 as SalesPrice,(TSPL_TRANSFER_DETAIL.Item_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value ) ) * " & strSubQry3 & " as SRNPrice, " & _
        "TSPL_TRANSFER_DETAIL.MRP as MRP,CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate, " & _
        "CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate, TSPL_LOCATION_MASTER.Location_Desc, " & _
        "'" & strValue & "' as value ,'" + strFromDateTime + "' as startTime, " & _
        "'" + strToDateTime + "' as endtime,Vehicle_Code,Vehicle_No," & strOrderColumn & " as OrderBy,convert(time,EntryDateTime,103)  as DocTime " & _
        "FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
        "TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
        "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
        "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER_1.Location_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                    "inner join TSPL_ITEM_MASTER on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  where " & _
        "(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
        "TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER_1.Location_Type='Physical' and " & _
        "TSPL_TRANSFER_HEAD.Transfer_Type='LO' and " & _
        " TSPL_TRANSFER_HEAD.EntryDateTime >=  '" + strFromDateTime + "' AND " & _
        " TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "' and Post='Y'"
            ''"convert(time,Date_Time_Removal,103) > = CONVERT(time,'" & dtpStarttime.Value & "' ,103) and " & _
            '    '"convert(time,Date_Time_Removal,103) < = CONVERT(time,'" & dtpendtime.Value & "' ,103) "
        End If

        Dim strUn4 As String = "Union All "

        ''''5. Transfer Load In detail where from location and To location both are physical and from location qty is Delivered QTy
        If strLocAll = "N" Then
            strSql5 = "SELECT tspl_transfer_head.Transfer_No+' (L/O '+tspl_transfer_head.Load_Out_No+ ')' as DocNo,TSPL_TRANSFER_HEAD.To_Location as Cust_Code, CONVERT(DATE,CONVERT(VARCHAR,TSPL_TRANSFER_HEAD.Transfer_Date,103),103) as Date, " & _
        " " & strSQL1Group & " as Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc,( case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then  TSPL_TRANSFER_DETAIL.LoadIn_Qty else 0 end) * " & strSubQry3 & " as DeliverQty, " & _
        "0 as RecdQty,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_LOCATION_MASTER_1.Location_Desc as Customer_Name, " & _
        "(TSPL_TRANSFER_DETAIL.LoadIn_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value)) * " & strSubQry3 & " as SalesPrice,0 as SRNPrice,TSPL_TRANSFER_DETAIL.MRP as MRP, " & _
        "CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate, CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate, " & _
        "TSPL_LOCATION_MASTER.Location_Desc,'" & strValue & "' as value ,'" + strFromDateTime + "' as startTime, " & _
        " '" + strToDateTime + "' as endtime,Vehicle_Code,Vehicle_No," & strOrderColumn & " as OrderBy,convert(time,EntryDateTime,103)  as DocTime FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
        "TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
        "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
        "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER_1.Location_Code  INNER JOIN " & _
                    "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                     "inner join TSPL_ITEM_MASTER on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
        "where (TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
        "TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER_1.Location_Type='Physical' and " & _
        "TSPL_TRANSFER_HEAD.Transfer_Type='LI' and " & _
        " TSPL_TRANSFER_HEAD.EntryDateTime >=  '" & strFromDateTime & "' AND " & _
        " TSPL_TRANSFER_HEAD.EntryDateTime <=  '" & strToDateTime & "'  and Post='Y'"
            ''"convert(time,Date_Time_Removal,103) > = CONVERT(time,'" & dtpStarttime.Value & "' ,103) and " & _
            '    '"convert(time,Date_Time_Removal,103) < = CONVERT(time,'" & dtpendtime.Value & "' ,103) "
        Else
            strSql5 = "SELECT tspl_transfer_head.Transfer_No+' (L/O '+tspl_transfer_head.Load_Out_No+ ')' as DocNo,TSPL_TRANSFER_HEAD.From_Location as Cust_Code,CONVERT(DATE,CONVERT(VARCHAR,TSPL_TRANSFER_HEAD.Transfer_Date,103),103) as Date, " & _
        " " & strSQL1Group & " as Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc,( case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then  TSPL_TRANSFER_DETAIL.LoadIn_Qty else 0 end) * " & strSubQry3 & " as DeliverQty, " & _
        "0 as RecdQty,TSPL_TRANSFER_DETAIL.Uom as Unit_Code,TSPL_LOCATION_MASTER.Location_Desc as Customer_Name, " & _
        "(TSPL_TRANSFER_DETAIL.LoadIn_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value)) * " & strSubQry3 & " as SalesPrice,0 as SRNPrice,TSPL_TRANSFER_DETAIL.MRP as MRP, " & _
        "CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate, CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate, " & _
        "TSPL_LOCATION_MASTER.Location_Desc,'" & strValue & "' as value ,'" + strFromDateTime + "' as startTime, " & _
        " '" + strToDateTime + "' as endtime,Vehicle_Code,Vehicle_No," & strOrderColumn & " as OrderBy,convert(time,EntryDateTime,103)  as DocTime FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
        "TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
        "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
        "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER_1.Location_Code  INNER JOIN " & _
                    "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
        "inner join TSPL_ITEM_MASTER on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
        "where (TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
        "TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER_1.Location_Type='Physical' and " & _
        "TSPL_TRANSFER_HEAD.Transfer_Type='LI' and " & _
        " TSPL_TRANSFER_HEAD.EntryDateTime  >=  '" & strFromDateTime & "' AND " & _
        " TSPL_TRANSFER_HEAD.EntryDateTime  <=  '" & strToDateTime & "'  and Post='Y'"
            ''"convert(time,Date_Time_Removal,103) > = CONVERT(time,'" & dtpStarttime.Value & "' ,103) and " & _
            '    '"convert(time,Date_Time_Removal,103) < = CONVERT(time,'" & dtpendtime.Value & "' ,103) "
        End If

        Dim strUn5 As String = "Union All "

        ''6.	Transfer Load In detail where from location and To location both are physical and from location qty is Received QTy
        If strLocAll = "N" Then
            strSql6 = " SELECT tspl_transfer_head.Transfer_No+' (L/O '+tspl_transfer_head.Load_Out_No+ ')' as DocNo,TSPL_TRANSFER_HEAD.From_Location as Cust_Code, " & _
           "CONVERT(DATE,CONVERT(VARCHAR,TSPL_TRANSFER_HEAD.Transfer_Date,103),103) as Date," & strSQL1Group & " as Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc, " & _
           "0 as DeliverQty,( case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then  TSPL_TRANSFER_DETAIL.LoadIn_Qty else 0 end) * " & strSubQry3 & " as RecdQty,TSPL_TRANSFER_DETAIL.Uom as Unit_Code, " & _
           "TSPL_LOCATION_MASTER.Location_Desc as Customer_Name,0  as SalesPrice,(TSPL_TRANSFER_DETAIL.LoadIn_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value)) * " & strSubQry3 & " as SRNPrice, " & _
           "TSPL_TRANSFER_DETAIL.MRP as MRP,CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate, " & _
           "CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate, TSPL_LOCATION_MASTER_1.Location_Desc, " & _
           " '" & strValue & "' as value ,'" + strFromDateTime + "' as startTime, " & _
           " '" + strToDateTime + "' as endtime,Vehicle_Code,Vehicle_No," & strOrderColumn & " as OrderBy,convert(time,EntryDateTime,103)  as DocTime " & _
           "FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
           "TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
           "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
           "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER_1.Location_Code  INNER JOIN " & _
                       "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                       "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                        "inner join TSPL_ITEM_MASTER on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code where " & _
           "(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
           "TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER_1.Location_Type='Physical' and " & _
           "TSPL_TRANSFER_HEAD.Transfer_Type='LI' and " & _
           " TSPL_TRANSFER_HEAD.EntryDateTime >= '" + strFromDateTime + "' AND  TSPL_TRANSFER_HEAD.EntryDateTime <= '" + strToDateTime + "' and Post='Y'"
        Else
            strSql6 = " SELECT tspl_transfer_head.Transfer_No+' (L/O '+tspl_transfer_head.Load_Out_No+ ')' as DocNo,TSPL_TRANSFER_HEAD.To_Location as Cust_Code, " & _
        "CONVERT(DATE,CONVERT(VARCHAR,TSPL_TRANSFER_HEAD.Transfer_Date,103),103) as Date," & strSQL1Group & " as Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc, " & _
        "0 as DeliverQty,(case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then  TSPL_TRANSFER_DETAIL.LoadIn_Qty else 0 end) * " & strSubQry3 & " as RecdQty,TSPL_TRANSFER_DETAIL.Uom as Unit_Code, " & _
        "TSPL_LOCATION_MASTER_1.Location_Desc as Customer_Name,0 as SalesPrice,(TSPL_TRANSFER_DETAIL.LoadIn_Qty * (TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + TSPL_TRANSFER_DETAIL.TPT_Value + TSPL_TRANSFER_DETAIL.Empty_Value)) * " & strSubQry3 & " as SRNPrice, " & _
        "TSPL_TRANSFER_DETAIL.MRP as MRP,CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate, " & _
        "CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate, TSPL_LOCATION_MASTER.Location_Desc, " & _
        "'" & strValue & "' as value ,'" + strFromDateTime + "' as startTime, " & _
        " '" + strToDateTime + "' as endtime,Vehicle_Code,Vehicle_No," & strOrderColumn & " as OrderBy,convert(time,EntryDateTime,103) as DocTime " & _
        "FROM  TSPL_TRANSFER_HEAD INNER JOIN " & _
        "TSPL_TRANSFER_DETAIL ON TSPL_TRANSFER_HEAD.Transfer_No = TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
        "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.From_Location = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
        "TSPL_LOCATION_MASTER AS TSPL_LOCATION_MASTER_1 ON TSPL_TRANSFER_HEAD.To_Location = TSPL_LOCATION_MASTER_1.Location_Code  INNER JOIN " & _
                    "TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                     "inner join TSPL_ITEM_MASTER on TSPL_TRANSFER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code where " & _
        "(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
        "TSPL_LOCATION_MASTER.Location_Type='Physical' and TSPL_LOCATION_MASTER_1.Location_Type='Physical' and " & _
        "TSPL_TRANSFER_HEAD.Transfer_Type='LI' and " & _
        " TSPL_TRANSFER_HEAD.EntryDateTime >= '" & strFromDateTime & "' AND " & _
        " TSPL_TRANSFER_HEAD.EntryDateTime <= '" & strToDateTime & "'  and Post='Y'"
            ''"convert(time,Date_Time_Removal,103) > = CONVERT(time,'" & dtpStarttime.Value & "' ,103) and " & _
            '    '"convert(time,Date_Time_Removal,103) < = CONVERT(time,'" & dtpendtime.Value & "' ,103) "
        End If

        Dim strUn6 As String = "Union All "
        Dim strSql7 As String = "SELECT TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo ,TSPL_ADJUSTMENT_HEADER.Customer_CODE, CONVERT(DATE,CONVERT(VARCHAR,TSPL_ADJUSTMENT_HEADER.Adjustment_Date, 103),103)  AS date, " & _
        "" & strSQL4Group & " as Item_Code, TSPL_ADJUSTMENT_DETAIL.Item_Description, " & _
        "TSPL_ADJUSTMENT_DETAIL.Item_Quantity  * " & strSubQry4 & " AS  Deliverqty,0 AS RecdQty, TSPL_ADJUSTMENT_DETAIL.Unit_Code," & _
        "TSPL_ADJUSTMENT_HEADER.Customer_NAME, TSPL_ADJUSTMENT_DETAIL.Item_Cost * " & strSubQry4 & " AS Saleprice, 0 AS SRNPrice, " & _
        "TSPL_ADJUSTMENT_DETAIL.mrp, CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate, " & _
        "CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate, TSPL_LOCATION_MASTER.Location_Desc," & _
        "'" & strValue & "' AS value, '" + strFromDateTime + "' as startTime, " & _
        "  '" + strToDateTime + "'  AS endtime,Vehicle_Code,Vehicle_No," & strOrderColumn & " as OrderBy,convert(time,EntryDateTime,103) as Doctime FROM  " & _
        "TSPL_ADJUSTMENT_HEADER INNER JOIN TSPL_ADJUSTMENT_DETAIL ON " & _
        "TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No INNER JOIN " & _
        "TSPL_LOCATION_MASTER ON TSPL_ADJUSTMENT_DETAIL.Location_Code = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
        "TSPL_CUSTOMER_MASTER ON TSPL_ADJUSTMENT_HEADER.Customer_CODE = TSPL_CUSTOMER_MASTER.Cust_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code  " & _
                    "inner join TSPL_ITEM_MASTER on TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code WHERE " & _
        "(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
        "(TSPL_ADJUSTMENT_HEADER.ItemType = 'E') AND  Location_Type <> 'Logical' and  " & _
        "(TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice' or TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '') AND " & _
        "(TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BD')  and " & _
        " TSPL_ADJUSTMENT_HEADER.EntryDateTime >= '" & strFromDateTime & "' AND " & _
        " TSPL_ADJUSTMENT_HEADER.EntryDateTime <= '" & strToDateTime & "'  and Posted='Y'"
        ''" convert(time,Modified_Time,103) > = CONVERT(time,'" & dtpStarttime.Value & "' ,103) and " & _
        ''" convert(time,Modified_Time,103) < = CONVERT(time,'" & dtpendtime.Value & "' ,103) "
        Dim strUn7 As String = "Union All "
        Dim strSql8 As String = "SELECT TSPL_ADJUSTMENT_HEADER.Adjustment_No as DocNo,TSPL_ADJUSTMENT_HEADER.Customer_CODE, CONVERT(DATE,CONVERT(VARCHAR,TSPL_ADJUSTMENT_HEADER.Adjustment_Date, 103),103)  AS date, " & _
        " " & strSQL4Group & " as Item_Code, TSPL_ADJUSTMENT_DETAIL.Item_Description, " & _
        "0 AS Deliverqty, TSPL_ADJUSTMENT_DETAIL.Item_Quantity  * " & strSubQry4 & " AS RecdQty, TSPL_ADJUSTMENT_DETAIL.Unit_Code, " & _
        "TSPL_ADJUSTMENT_HEADER.Customer_NAME, 0 AS Saleprice, TSPL_ADJUSTMENT_DETAIL.Item_Cost * " & strSubQry4 & " AS SRNPrice, " & _
        "TSPL_ADJUSTMENT_DETAIL.mrp, CONVERT(date, '" & txtFromDate.Value & "', 103) AS Fdate, " & _
        "CONVERT(date, '" & txtToDate.Value & "', 103) AS tdate, TSPL_LOCATION_MASTER.Location_Desc," & _
        "'" & strValue & "' AS value, '" + strFromDateTime + "' as startTime, " & _
        " '" + strToDateTime + "'  AS endtime,Vehicle_Code,Vehicle_No," & strOrderColumn & " as OrderBy,convert(time,EntryDateTime,103) as Doctime FROM  " & _
        "TSPL_ADJUSTMENT_HEADER INNER JOIN TSPL_ADJUSTMENT_DETAIL ON " & _
        "TSPL_ADJUSTMENT_HEADER.Adjustment_No = TSPL_ADJUSTMENT_DETAIL.Adjustment_No INNER JOIN " & _
        "TSPL_LOCATION_MASTER ON TSPL_ADJUSTMENT_DETAIL.Location_Code = TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
        "TSPL_CUSTOMER_MASTER ON TSPL_ADJUSTMENT_HEADER.Customer_CODE = TSPL_CUSTOMER_MASTER.Cust_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                    "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_ADJUSTMENT_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                    "inner join TSPL_ITEM_MASTER on TSPL_ADJUSTMENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code WHERE " & _
        "(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
        "(TSPL_ADJUSTMENT_HEADER.ItemType = 'E') AND  Location_Type <> 'Logical' and  " & _
        "(TSPL_ADJUSTMENT_HEADER.Reference_Document='Sale Invoice' or TSPL_ADJUSTMENT_HEADER.Customer_CODE <> '') AND " & _
        "(TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI')  and " & _
        " TSPL_ADJUSTMENT_HEADER.EntryDateTime >= '" & strFromDateTime & "' AND " & _
        " TSPL_ADJUSTMENT_HEADER.EntryDateTime <= '" & strToDateTime & "' and Posted='Y'"
        ''" convert(time,Modified_Time,103) > = CONVERT(time,'" & dtpStarttime.Value & "' ,103) and " & _
        ''" convert(time,Modified_Time,103) < = CONVERT(time,'" & dtpendtime.Value & "' ,103) "

        If strLocAll = "N" Then

            strSql1 += " and TSPL_SALE_INVOICE_DETAIL.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            strSql2 += " and TSPL_SRN_DETAIL.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            strSql3 += " and TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            strSql4 += " and TSPL_TRANSFER_HEAD.To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            strSql5 += " and TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            strSql6 += " and TSPL_TRANSFER_HEAD.To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            strSql7 += " and TSPL_ADJUSTMENT_DETAIL.Location_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            strSql8 += " and TSPL_ADJUSTMENT_DETAIL.Location_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            strSql9 += " and TSPL_SALE_RETURN_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
            strSql10 += " and TSPL_SALE_RETURN_INTER_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "


        End If
        If strCustAll = "N" And strTempAll = "Y" Then
            strSql1 += " and TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            strSql2 += " and tspl_customer_vendor_mapping.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            strSql7 += " and TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            strSql8 += " and TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            strSql9 += " and TSPL_SALE_RETURN_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            strSql10 += " and TSPL_SALE_RETURN_INTER_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "

        End If
        'If strCustAll = "N" Then
        '    strSql1 += " and TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '    strSql2 += " and tspl_customer_vendor_mapping.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '    strSql7 += " and TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '    strSql8 += " and TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '    strSql9 += " and TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "

        'End If
        If strTempAll = "N" Then
            strSql1 += " and  TSPL_SALE_INVOICE_HEAD.cust_code in  ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
            strSql2 += " and tspl_customer_vendor_mapping.Cust_Code in ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
            strSql7 += " and TSPL_ADJUSTMENT_HEADER.Customer_CODE in ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
            strSql8 += " and TSPL_ADJUSTMENT_HEADER.Customer_CODE in ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
            strSql9 += " and TSPL_SALE_RETURN_HEAD.Cust_Code in ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
            strSql10 += " and TSPL_SALE_RETURN_INTER_HEAD.Cust_Code in ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "

        End If
        If strCustClass = "N" Then
            strCust = "" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ""
            strSql1 += " and TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
            strSql2 += " and TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
            strSql7 += " and TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
            strSql8 += " and TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
            strSql9 += " and TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
            strSql10 += " and TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "

        End If
        If strVehicle = "N" Then
            strSql1 += " and TSPL_SALE_INVOICE_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
            strSql2 += " and TSPL_SRN_HEAD.VehicleNo in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
            strSql3 += " and TSPL_TRANSFER_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
            strSql4 += " and TSPL_TRANSFER_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
            strSql5 += " and TSPL_TRANSFER_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
            strSql6 += " and TSPL_TRANSFER_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
            strSql7 += " and TSPL_ADJUSTMENT_HEADER.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
            strSql8 += " and TSPL_ADJUSTMENT_HEADER.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
            strSql9 += " and TSPL_SALE_RETURN_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
            strSql10 += " and TSPL_SALE_RETURN_INTER_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "

        End If

        If strUnit = "E" Then
            strSql1 += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='EC' or TSPL_SALE_INVOICE_Detail.Unit_code ='EB' or TSPL_SALE_INVOICE_Detail.Unit_code ='SH') "
            strSql2 += " and (TSPL_SRN_DETAIL.Unit_code='EC' or TSPL_SRN_DETAIL.Unit_code='EB' or TSPL_SRN_DETAIL.Unit_code='SH') "
            strSql3 += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
            strSql4 += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
            strSql5 += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
            strSql6 += " and (TSPL_TRANSFER_DETAIL.UOM ='EC' or TSPL_TRANSFER_DETAIL.UOM ='EB' or TSPL_TRANSFER_DETAIL.UOM ='SH') "
            strSql7 += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='EC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='EB' or TSPL_ADJUSTMENT_DETAIL.Unit_code='SH')"
            strSql8 += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='EC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='EB' or TSPL_ADJUSTMENT_DETAIL.Unit_code='SH')"
            strSql10 += " and (TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='EC' or TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='EB' or TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='SH')"
            strSql9 += " and (TSPL_SALE_RETURN_DETAIL.Unit_code ='EC' or TSPL_SALE_RETURN_DETAIL.Unit_code ='EB' or TSPL_SALE_RETURN_DETAIL.Unit_code ='SH') "
        End If
        If strUnit = "F" Then
            strSql1 += " and (TSPL_SALE_INVOICE_Detail.Unit_code ='FC' or TSPL_SALE_INVOICE_Detail.Unit_code ='fB') "
            strSql2 += " and (TSPL_SRN_DETAIL.Unit_code='FC' or TSPL_SRN_DETAIL.Unit_code='FB') "
            strSql3 += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB') "
            strSql4 += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB') "
            strSql5 += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB') "
            strSql6 += " and (TSPL_TRANSFER_DETAIL.UOM ='FC' or TSPL_TRANSFER_DETAIL.UOM ='FB') "
            strSql7 += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='FC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='FB')"
            strSql8 += " and (TSPL_ADJUSTMENT_DETAIL.Unit_code='FC' or TSPL_ADJUSTMENT_DETAIL.Unit_code='FB')"
            strSql10 += " and (TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FC' or TSPL_SALE_RETURN_INTER_DETAIL.Unit_code='FB')"
            strSql9 += " and (TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' or TSPL_SALE_RETURN_DETAIL.Unit_code ='fB') "
        End If

        Dim strSql, strReport As String
        strReport = ""

        If rdbWithoutVehicle.IsChecked = True Then
            If strLocAll = "Y" Then
                strReport = "crptOtherPartySale"
            Else
                strReport = "crptOtherPartySaleLocationwise"
            End If
        ElseIf rdbVehicle.IsChecked = True Then
            If strLocAll = "Y" Then
                strReport = "crptOtherPartySaleVehicleWise"
            Else
                strReport = "crptOtherPartySaleVehicleLocationWise"
            End If
        ElseIf rdbInvoice.IsChecked = True Then
            If strLocAll = "Y" Then
                strReport = "crptOtherPartySaleVehicleWiseSummary"
            Else
                strReport = "crptOtherPartySaleVehicleLocationWiseSummary"
            End If
        End If
        strSql = strSql1 & strUn1 & strSql2 & strUn2 & strSql3 & strUn3 & strSql4 & strUn4 & strSql5 & strUn5 & strSql6 & strUn6 & strSql7 & strUn7 & strSql8 & strUn1 & strSql9 & strUn1 & strSql10 & strOrderBy

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(strSql)
        dtgv.DefaultView.Sort = "Date"
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.DataSource = dtgv.DefaultView
            gridformat()
        End If

        If chk = True Then
            ExportToExcelGV(exporter)
        Else
            If rdbWithoutVehicle.IsChecked = True Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strSql), "" & strReport & "", "Other Party Sales Report")
                ''FrmSalerReport.funreport(strSql, "" & strReport & "", "Other Party Sales Report")
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strSql), "" & strReport & "", "Other Party Sales Report")
                ''FrmSalerReport.funreport(strSql, "" & strReport & "", "Other Party Sales Report")
            End If

        End If


        'If strLocAll = "Y" Then
        '    If strCustAll = "Y" Then
        '    Else
        '        strSql1 += " and TSPL_SALE_INVOICE_DETAIL.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '        strSql2 += " and tspl_customer_vendor_mapping.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '        strSql7 += " and TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '        strSql8 += " and TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '    End If

        'Else
        '    If strCustAll = "Y" Then
        '        strSql1 += " and TSPL_SALE_INVOICE_DETAIL.Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
        '        strSql2 += " and TSPL_SRN_DETAIL.Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
        '        strSql3 += " and TSPL_TRANSFER_HEAD.From_Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
        '        strSql4 += " and TSPL_TRANSFER_HEAD.To_Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
        '        strSql5 += " and TSPL_TRANSFER_HEAD.From_Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
        '        strSql6 += " and TSPL_TRANSFER_HEAD.To_Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
        '        strSql7 += " and TSPL_ADJUSTMENT_DETAIL.Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
        '        strSql8 += " and TSPL_ADJUSTMENT_DETAIL.Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
        '    Else
        '        strSql1 += " and TSPL_SALE_INVOICE_DETAIL.Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") and TSPL_SALE_INVOICE_DETAIL.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '        strSql2 += " and TSPL_SRN_DETAIL.Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") and tspl_customer_vendor_mapping.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '        strSql3 += " and TSPL_TRANSFER_HEAD.From_Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
        '        strSql4 += " and TSPL_TRANSFER_HEAD.To_Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
        '        strSql5 += " and TSPL_TRANSFER_HEAD.From_Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
        '        strSql6 += " and TSPL_TRANSFER_HEAD.To_Location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ") "
        '        strSql7 += " and TSPL_ADJUSTMENT_DETAIL.Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")  and TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '        strSql8 += " and TSPL_ADJUSTMENT_DETAIL.Location_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")  and TSPL_ADJUSTMENT_HEADER.Customer_CODE in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '    End If

        'End If
        'If strVehicle = "N" Then
        '    strSql1 += " and TSPL_SALE_INVOICE_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
        '    strSql2 += " and TSPL_SRN_HEAD.VehicleNo in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
        '    strSql3 += " and TSPL_TRANSFER_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
        '    strSql4 += " and TSPL_TRANSFER_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
        '    strSql5 += " and TSPL_TRANSFER_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
        '    strSql6 += " and TSPL_TRANSFER_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
        '    strSql7 += " and TSPL_ADJUSTMENT_HEADER.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
        '    strSql8 += " and TSPL_ADJUSTMENT_HEADER.Vehicle_Code in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ") "
        'End If
    End Sub

    Public Sub gridformat()
        Try

            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False
            Dim summaryRowItem As New GridViewSummaryRowItem()
            gv1.AllowAddNewRow = False

            gv1.Columns("DocNo").IsVisible = True
            gv1.Columns("DocNo").Width = 150
            gv1.Columns("DocNo").HeaderText = "Document No"

            gv1.Columns("Cust_Code").IsVisible = True
            gv1.Columns("Cust_Code").Width = 70
            gv1.Columns("Cust_Code").HeaderText = "Customer"


            gv1.Columns("Date").IsVisible = True
            gv1.Columns("Date").Width = 101
            gv1.Columns("Date").FormatString = "{0:d}"
            gv1.Columns("Date").HeaderText = "Date"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 101
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Item_Desc").IsVisible = True
            gv1.Columns("Item_Desc").Width = 200
            gv1.Columns("Item_Desc").HeaderText = "Description"

            gv1.Columns("Deliverqty").IsVisible = True
            gv1.Columns("Deliverqty").Width = 101
            gv1.Columns("Deliverqty").HeaderText = "Dilivered Qty"

            gv1.Columns("RecdQty").IsVisible = True
            gv1.Columns("RecdQty").Width = 101
            gv1.Columns("RecdQty").HeaderText = "Received Qty"

            gv1.Columns("Unit_code").IsVisible = True
            gv1.Columns("Unit_code").Width = 81
            gv1.Columns("Unit_code").HeaderText = "UOM"

            gv1.Columns("Customer_Name").IsVisible = True
            gv1.Columns("Customer_Name").Width = 200
            gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            gv1.Columns("Saleprice").IsVisible = True
            gv1.Columns("Saleprice").Width = 90
            gv1.Columns("Saleprice").HeaderText = "Sale Price"

            gv1.Columns("SRNPrice").IsVisible = True
            gv1.Columns("SRNPrice").Width = 100
            gv1.Columns("SRNPrice").HeaderText = "SRN Price"

            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 91

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 200
            gv1.Columns("Location_Desc").HeaderText = "Location"

            gv1.Columns("value").IsVisible = True
            gv1.Columns("value").Width = 71
            gv1.Columns("value").HeaderText = "Value"

            gv1.Columns("Vehicle_Code").IsVisible = True
            gv1.Columns("Vehicle_Code").Width = 100
            gv1.Columns("Vehicle_Code").HeaderText = "Vehicle Code"

            gv1.Columns("Vehicle_No").IsVisible = True
            gv1.Columns("Vehicle_No").Width = 100
            gv1.Columns("Vehicle_No").HeaderText = "Vehicle No"

            gv1.Columns("Doctime").IsVisible = True
            gv1.Columns("Doctime").Width = 150
            gv1.Columns("Doctime").HeaderText = "Doc Time"


            'gv1.GroupDescriptors.Add(New GridGroupByExpression("Location as Location format ""{0}: {1}"" Group By Location"))
            'gv1.GroupDescriptors.Add(New GridGroupByExpression("CustomerClass as CustomerClass format ""{0}: {1}"" Group By CustomerClass"))
            'gv1.GroupDescriptors.Add(New GridGroupByExpression("CustomerName as Customer format ""{0}: {1}"" Group By CustomerName"))
            'gv1.MasterTemplate.ExpandAllGroups()

            'gv1.ShowGroupPanel = False
            'gv1.MasterTemplate.AutoExpandGroups = True


            Dim DlvQty As New GridViewSummaryItem("Deliverqty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(DlvQty)
            Dim RcvdQty As New GridViewSummaryItem("RecdQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(RcvdQty)

            'gv1.MasterTemplate.ExpandAllGroups()
            'gv1.ShowGroupPanel = False
            'gv1.MasterTemplate.AutoExpandGroups = True
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub ExportToExcelGV(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""


            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")
            'If chkLocSelect.IsChecked Then
            '    strTemp = ""
            '    For Each Str As String In cbgLocation.CheckedValue
            '        If clsCommon.myLen(strTemp) > 0 Then
            '            strTemp += ", "
            '        End If
            '        strTemp += Str
            '    Next
            '    arrHeader.Add("Location : " + strTemp)
            'End If
            arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy") + " ")

            If chklocSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbglocation.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Location Segment : " + strTemp)
            End If
            If chkClassSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In chkCustomerClass.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Customer Class Segment : " + strTemp)
            End If
            If chkChkSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Customer Segment : " + strTemp)
            End If
            If chktempselect.IsChecked Then
                strTemp = ""
                For Each Str As String In cgvtemplate.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Template Segment : " + strTemp)
            End If
            If chkVehicleSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgVehicle.CheckedDisplayMember
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Vehicle Segment : " + strTemp)
            End If
            ' clsCommon.MyExportToExcel("Other Party Sale", gv1, arrHeader, "OtherPartySale")

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Other Party Sale", gv1, arrHeader, "OtherPartySale")
            Else
                clsCommon.MyExportToPDF("Other Party Sale", gv1, arrHeader, "OtherPartySale", True)
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Private Sub chkCustAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked
    End Sub

    Private Sub ChkVehicleAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkVehicleAll.ToggleStateChanged
        cbgVehicle.Enabled = Not ChkVehicleAll.IsChecked
    End Sub
    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkClassAll.ToggleStateChanged
        chkCustomerClass.Enabled = Not chkClassAll.IsChecked
    End Sub

    Private Sub RadGroupBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox5.Click

    End Sub

    Private Sub rdbVehicle_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbVehicle.ToggleStateChanged
        grpVehicle.Visible = True
    End Sub

    Private Sub rdbWithoutVehicle_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbWithoutVehicle.ToggleStateChanged
        grpVehicle.Visible = False
    End Sub

    Private Sub chktempall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktempall.ToggleStateChanged
        cgvtemplate.Enabled = Not chktempall.IsChecked
    End Sub
    Sub LoadTemplate()
        Dim qry As String = " select distinct Tmplate_Id as [Template ID] , Description from TSPL_CUSTOMER_TEMPLATE_MASTER "
        cgvtemplate.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvtemplate.ValueMember = "Template ID"
        cgvtemplate.DisplayMember = "Description"
    End Sub

    
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        print(True, EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        print(True, EnumExportTo.PDF)
    End Sub
End Class
