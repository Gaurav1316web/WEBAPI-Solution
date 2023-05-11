Imports Microsoft.VisualBasic
Imports System
Imports XpertERPEngine
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

Public Class FrmItemDiscountReport
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    Dim dr As SqlDataReader

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"

        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strQuery)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                l1User = dt.Rows(i)("User_Type").ToString()
                l2User = dt.Rows(i)("Level1_Code").ToString()
                l3User = dt.Rows(i)("Level2_Code").ToString()
                l4User = dt.Rows(i)("Level3_Code").ToString()
                l5User = dt.Rows(i)("Level4_Code").ToString()
            Next
        End If


        'dr = connectSql.RunSqlReturnDR(sql)
        'dr.Read()
        'l1User = dr(0).ToString()
        'l2User = dr(1).ToString()
        'l3User = dr(2).ToString()
        'l4User = dr(3).ToString()
        'l5User = dr(4).ToString()
    End Sub
    Sub LoadCustomer()
        Dim dt As DataTable = New DataTable()
        dt = LoadClass()

        cbgCustomer.DataSource = dt
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub
    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Location,TSPL_LOCATION_MASTER.Location_Desc as[Location Description] from (select distinct Location from TSPL_SALE_INVOICE_DETAIL)Final left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location"
        cbgLocation.DisplayMember = "Location Description"
    End Sub
    Sub LoadItem()
        ''Dim qry As String = "select distinct Item_Code as [Item Code],Scheme_Applicable as [Scheme Applicable] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Item_Code,Item_Desc from TSPL_ITEM_MASTER"
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item_Code"
        cbgItem.DisplayMember = "Scheme Applicable"
    End Sub
    Public Shared Function LoadClass() As DataTable

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





    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt("ITM-DIS-RPT")
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmItemDiscountReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub FrmItemDiscountReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        LoadCustomer()
        chkCustomerAll.IsChecked = True
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadItem()
        chkItemAll.IsChecked = True
        LoadCompany()
        rbtnCompanyAll.IsChecked = True

        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        LoadCustomer()
        chkCustomerAll.IsChecked = True
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadItem()
        chkItemAll.IsChecked = True
    End Sub

    Private Sub chkDocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged, chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged, chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged, chkItemSelect.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Print()


    End Sub
    Sub print()
        If rbtnCompanySelect.IsChecked AndAlso cbgCompany.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one company")
            Return
        End If

        Dim strCustAll, strLocAll, strItemAll, strReporrTitle As String
        Dim strGroup1, strGroup2, strGroup3 As String
        Dim stGroupDesc1, stGroupDesc2, stGroupDesc3 As String
        strCustAll = ""
        strLocAll = ""
        strItemAll = ""
        strReporrTitle = ""
        strGroup1 = ""
        strGroup2 = ""
        strGroup3 = ""
        stGroupDesc1 = ""
        stGroupDesc2 = ""
        stGroupDesc3 = ""
        If chkCustomerSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Customer Category or select ALL ")
            Return
        ElseIf chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            Return
        ElseIf chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least Item or select ALL")
            Return
        End If
        If chkCust.IsChecked = True Then
            strGroup1 = "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class"
            strGroup2 = "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code"
            strGroup3 = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code"
            stGroupDesc1 = "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc"
            stGroupDesc2 = "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc"
            stGroupDesc3 = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc"
            strReporrTitle = "Customer Category Wise Sales/Discount Details"
        End If
        If chkLoc.IsChecked = True Then
            strGroup1 = "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code"
            strGroup2 = "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class"
            strGroup3 = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code"
            stGroupDesc1 = "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc"
            stGroupDesc2 = "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc"
            stGroupDesc3 = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc"
            strReporrTitle = "Location Wise Sales/Discount Details"
        End If
        If chkItem.IsChecked = True Then
            strGroup1 = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code"
            strGroup2 = "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code"
            strGroup3 = "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class"
            stGroupDesc1 = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc"
            stGroupDesc2 = "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc"
            stGroupDesc3 = "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc"
            strReporrTitle = "Item Wise Sales/Discount Details"
        End If

        If chkCustomerAll.IsChecked = True Then
            strCustAll = "Y"
        Else
            strCustAll = "N"
        End If
        If chkLocationAll.IsChecked = True Then
            strLocAll = "Y"
        Else
            strLocAll = "N"
        End If
        If chkItemAll.IsChecked = True Then
            strItemAll = "Y"
        Else
            strItemAll = "N"
        End If

        strQuery = "select " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Basic_Rate," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc, " & _
                    "CASE WHEN (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                    "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN 0 " & _
                    "ELSE " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty END AS SaleQty, CASE WHEN " & _
                    "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                    "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y' ) THEN " & _
                    "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty ELSE 0 END AS FOCqty, " & _
                    "CASE WHEN (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                    "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN 0 " & _
                    "ELSE " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty * " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Basic_Rate END AS SaleAmt," & _
                    "isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Disc_Amt,0) as Disc_Amt, " & _
                    "CASE WHEN (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y' OR " & _
                    "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item = 'Y' OR  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sampling_Item = 'Y') THEN  " & _
                    " case when ((select " + clsCommon.ReplicateDBString + "tspl_location_master.excisable from " + clsCommon.ReplicateDBString + "tspl_location_master where " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location)='T' ) then (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty * (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Basic_Rate + (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Basic_Rate * (10.3/100)))) else (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty * " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Basic_Rate) end ELSE 0 END AS FOCAmt, " & _
                    "isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Cust_Discount,0) as Cust_Discount," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code, " & _
                    "" & strGroup1 & " as Group1," & strGroup2 & " as Group2," & strGroup3 & " as Group3, " & _
                    "" & stGroupDesc1 & " as Group1_Desc," & stGroupDesc2 & " as Group2_Desc, " & _
                    "" & stGroupDesc3 & " as Group3_Desc,'" & strReporrTitle & "' as ReportTitle, " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor " & _
                    "from TSPL_CUSTOMER_TYPE_MASTER INNER JOIN " & _
                    "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location AND " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code "



        If strCustAll = "N" Then
            strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class  in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        End If
        If strLocAll = "N" Then
            strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
        End If
        If strItemAll = "N" Then
            strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        End If
        Dim ArrDBName As ArrayList = Nothing
        If rbtnCompanyAll.IsChecked Then
            ArrDBName = cbgCompany.AllValue
        Else
            ArrDBName = cbgCompany.CheckedValue
        End If
        strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQuery, ArrDBName, False)

        Dim frmcrystal As New frmCrystalReportViewer()
        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptItemDiscountReport", "Item Discount Report")



        'If strCustAll = "Y" Then
        '    If strLocAll = "Y" Then
        '        If strItemAll = "Y" Then

        '        Else
        '            strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        '        End If
        '    Else
        '        If strItemAll = "Y" Then
        '            strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
        '        Else
        '            strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        '        End If
        '    End If

        'Else
        '    If strLocAll = "Y" Then
        '        If strItemAll = "Y" Then
        '            strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class  in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '        Else
        '            strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        '        End If
        '    Else
        '        If strItemAll = "Y" Then
        '            strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        '        Else
        '            strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class  in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        '        End If
        '    End If

        'End If

    End Sub


    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged, rbtnCompanySelect.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub


    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ITM-DIS-RPT"
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

 
End Class
