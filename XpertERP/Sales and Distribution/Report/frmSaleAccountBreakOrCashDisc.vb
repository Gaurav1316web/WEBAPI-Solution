'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 17/07/2012-------------------------------------
'--------------------------------Last modify Time - 03:30 pm -------------------------------------
'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 29/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------
'by vipin for pdf on 11/02/2013


'''' for bug no BM00000000605 ,BM00000000593,BM00000001142

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
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Public Class FrmSaleAccountBreakOrCashDisc
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    Dim dr As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    Dim blnRefresh As Boolean = False
    Dim strPost, strFOC, strSale, strTarget As String
    Dim blnVisualEffect As Boolean = False



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.SaleAccountBreakDetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()

            Next
        End If
    End Sub

    Private Sub FrmSaleAccountBreakOrCashDisc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub FrmSaleAccountBreakOrCashDisc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        dtpStarttime.Value = DateTime.Now
        dtpendtime.Value = DateTime.Now
        rdbSummary.IsChecked = True


        chklocAll.IsChecked = True
        chkCustAll.IsChecked = True
        chkClassAll.IsChecked = True
        chkItemAll1.IsChecked = True
        chkGroupAll.IsChecked = True
        LoadItem()
        LoadCustomer()
        LoadCustomerClass()
        Loadlocation()
        LoadCustomerGroup()
        LoadCompany()
        'rbtnCompanyAll.IsChecked = True
        rbtnCompanySelect.IsChecked = True
        Dim arrDBName As New ArrayList()
        arrDBName.Add(objCommonVar.CurrDatabase)
        cbgCompany.CheckedValue = arrDBName
        rdbSku.IsChecked = True

        'grpSelect.Visible = True
        'grpSku.Visible = True
        ddlType.Text = "Quantity"
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")

        rdbAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
        rdbTradeDisc.IsChecked = True
        rdbRoute.Checked = False
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SL-AC-BRKG"
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
        '  Dim qry As String = "select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbglocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbglocation.ValueMember = "Code"
        'cbglocation.DisplayMember = "Description"
        cbglocation.DataSource = clsLocation.GetLocationSegments()
        cbglocation.ValueMember = "Code"
        cbglocation.DisplayMember = "Name"
    End Sub

    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name],Customer_Class as [Customer Class] from TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)

        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Code"
    End Sub

    Sub LoadCustomerGroup()
        Dim qry As String = "select Cust_Group_Code as [Customer Group Code],Cust_Group_Desc as [Description]from TSPL_CUSTOMER_GROUP_MASTER"
        cbgCustGroup.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustGroup.ValueMember = "Customer Group Code"
        cbgCustGroup.DisplayMember = "Customer Group Code"
    End Sub
    Sub LoadItem()
        ''Dim qry As String = "select distinct Item_Code as [Item Code],Scheme_Applicable as [Scheme Applicable] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Item_Code as [Item Code],Item_Desc as [Item Description] from TSPL_ITEM_MASTER"
        cbgItem1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem1.ValueMember = "Item Code"
        cbgItem1.DisplayMember = "Item Description"
    End Sub
    Private Sub chklocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbglocation.Enabled = Not chklocAll.IsChecked
    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
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
        GV1.DataSource = Nothing
        GV1.Columns.Clear()
        GV1.Rows.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub reset()
        rdbRoute.Checked = False
        dtpstart.Value = clsCommon.GETSERVERDATE()
        dtpend.Value = clsCommon.GETSERVERDATE()
        dtpStarttime.Value = DateTime.Now
        dtpendtime.Value = DateTime.Now
        rdbSummary.IsChecked = True
        chklocAll.IsChecked = True
        chkCustAll.IsChecked = True
        chkClassAll.IsChecked = True
        chkItemAll1.IsChecked = True
        chkGroupAll.IsChecked = True
        'LoadItem()
        'LoadCustomer()
        'LoadCustomerClass()
        'Loadlocation()
        'LoadCustomerGroup()
        ddlType.Text = "Quantity"
        'grpSelect.Visible = True
        'LoadCompany()
        rbtnCompanyAll.IsChecked = True
        rdbSku.IsChecked = True
        rdbAll.IsChecked = True
        rdbTradeDisc.IsChecked = True
    End Sub
    Private Sub chkCustAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgCustomer.Enabled = Not chkCustAll.IsChecked
    End Sub

    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        chkCustomerClass.Enabled = Not chkClassAll.IsChecked
    End Sub

    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub
    Private Sub btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()
    End Sub

    Sub print()
        Try
            Dim strFOCRet, strSaleRet, strFOCInter, strSaleInter, strPostInter As String
            strSaleRet = ""
            strFOCRet = ""
            strSaleInter = ""
            strFOCInter = ""
            Dim strItem, strTotal, strTotalSale As String
            strItem = ""
            strTotal = ""
            strTotalSale = ""
            If rdbAll.IsChecked = True Then
                strPost = ""
                strPostInter = ""
            Else
                strPost = " and Is_Post='Y' "
                strPostInter = " and Is_Post=1 "
            End If

            If rdbTradeDisc.IsChecked = True Then
                strFOC = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y') and ( TSPL_SALE_INVOICE_DETAIL.Discount_Code = '') "
                strSale = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'N')  "
                strFOCRet = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Scheme_Item = 'Y') and ( TSPL_SALE_return_DETAIL.Discount_Code = '') "
                strSaleRet = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Scheme_Item = 'N')  "
                strFOCInter = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Scheme_Item = 'Y')  "
                strSaleInter = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Scheme_Item = 'N')  "
            ElseIf rdbFoc.IsChecked Then
                strFOC = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y') "
                strSale = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'N')"
                strFOCRet = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Scheme_Item = 'Y') "
                strSaleRet = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Scheme_Item = 'N')"
                strFOCInter = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Scheme_Item = 'Y') "
                strSaleInter = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Scheme_Item = 'N')"
            ElseIf rdbtarget.IsChecked Then
                strFOC = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'Y') and ( TSPL_SALE_INVOICE_DETAIL.Discount_Code <>  '') "
                strSale = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Scheme_Item = 'N')"
                strFOCRet = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Scheme_Item = 'Y') and ( TSPL_SALE_return_DETAIL.Discount_Code <>  '') "
                strSaleRet = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Scheme_Item = 'N')"
                strFOCInter = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Scheme_Item = 'Y')  "
                strSaleInter = " ( " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Scheme_Item = 'N')"
            End If


            If chklocSelect.IsChecked = True AndAlso cbglocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                Return
            ElseIf chkChkSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer or select ALL")
                Return
            ElseIf chkItemSelect1.IsChecked = True AndAlso cbgItem1.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Item or select ALL")
                Return
            ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomerClass.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer Type or select ALL")
                Return
            ElseIf chkGroupSelect.IsChecked = True AndAlso cbgCustGroup.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer Group or select ALL")
                Return
            ElseIf rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Company or select ALL")
                Return
            End If
            Dim strInter, strSql, strSql2, strCustAll, strLocAll, strItemAll, strClassAll, strCustGroup, strSQL1Group, strReportTitle, strOrderColumn, strOrderBy, strsum, strsumTradeQty, strsumTradeAmt, strsumAmt As String
            strOrderColumn = ""
            strOrderBy = ""
            strsum = ""
            strsumAmt = ""
            strsumTradeQty = ""
            strsumTradeAmt = ""
            strSQL1Group = ""
            strInter = ""
            If chklocAll.IsChecked = True Then
                strLocAll = "Y"
            Else
                strLocAll = "N"
            End If
            If chkItemAll1.IsChecked = True Then
                strItemAll = "Y"
            Else
                strItemAll = "N"
            End If
            If chkClassAll.IsChecked = True Then
                strClassAll = "Y"
            Else
                strClassAll = "N"
            End If
            If chkCustAll.IsChecked = True Then
                strCustAll = "Y"
            Else
                strCustAll = "N"
            End If

            If chkGroupAll.IsChecked = True Then
                strCustGroup = "Y"
            Else
                strCustGroup = "N"
            End If


            If rdbSku.IsChecked = True Then
                strSQL1Group = clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code"
                strReportTitle = "Cash Discount Summary Sku wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Sku_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
            ElseIf rdbPack.IsChecked = True Then
                strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc + ' ( ' +  convert(varchar(20),TSPL_ITEM_MASTER.Pack_Seq) +  ' ) '"
                strReportTitle = "Cash Discount Summary Pack wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Pack_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
            ElseIf rdbFlavour.IsChecked = True Then
                strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc + ' ( ' +  convert(varchar(20),TSPL_ITEM_MASTER.Flavour_Seq) +  ' ) '"
                strReportTitle = "Cash Discount Summary Flavour wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Flavour_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"
            End If
            Dim strLoca As String = ""
            Dim strClass As String = ""
            If chklocSelect.IsChecked Then
                For Each Str As String In cbglocation.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
            End If

            If chkClassSelect.IsChecked Then
                For Each Str As String In chkCustomerClass.CheckedDisplayMember
                    If clsCommon.myLen(strClass) > 0 Then
                        strClass += ", "
                    End If
                    strClass += Str
                Next
            End If
            Dim strItemCodestring, strTradeItemQty, strTradeItemAmt, strTradeItemstringQty, strTradeItemstringAMt, strItemCodestringAMt, strItemCode, strItemCodeAmt, strTradeMainItem, strMainItemCode, strMainItemCodeAmt, strmainItemCodeString, strmainItemCodeStringamt, strPivot, strPivotamt, str1, str2 As String
            strItemCodestring = ""
            strTradeItemQty = ""
            strTradeItemAmt = ""
            strTradeItemstringQty = ""
            strTradeItemstringAMt = ""
            strItemCodestringAMt = ""
            strItemCode = ""
            strItemCodeAmt = ""
            strTradeMainItem = ""
            strMainItemCode = ""
            strMainItemCodeAmt = ""
            strmainItemCodeString = ""
            strmainItemCodeStringamt = ""
            strPivot = ""
            strPivotamt = ""
            str1 = ""
            str2 = ""
            If rdbSku.IsChecked = True Then
                strPivot = "TSPL_SALE_INVOICE_DETAIL.Item_Code"
            ElseIf rdbPack.IsChecked = True Then
                strPivot = "TSPL_ITEM_DETAILS.Class_Desc + ' ( ' +  convert(varchar(20),TSPL_ITEM_MASTER.Pack_Seq) +  ' ) '"
            ElseIf rdbFlavour.IsChecked = True Then
                strPivot = "TSPL_ITEM_DETAILS_1.Class_Desc + ' ( ' +  convert(varchar(20),TSPL_ITEM_MASTER.Flavour_Seq) +  ' ) '"
            End If
            Dim strBothqty, strbothamt, strBothqtymain, strbothamtmain As String
            strBothqtymain = ""
            strBothqty = ""
            strbothamtmain = ""
            strbothamt = ""
            If rdbSummary.IsChecked = True OrElse rdbSummaryDoc.IsChecked = True OrElse rdbSalewithTargetQty.IsChecked = True Then

                dt = clsDBFuncationality.GetDataTable("SELECT distinct " & strPivot & "," & strOrderColumn & " FROM TSPL_ITEM_DETAILS INNER JOIN " & _
                              "TSPL_SALE_INVOICE_DETAIL ON TSPL_ITEM_DETAILS.Item_Code = TSPL_SALE_INVOICE_DETAIL.Item_Code LEFT OUTER JOIN " & _
                              "TSPL_ITEM_MASTER ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code RIGHT OUTER JOIN " & _
                              "TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No LEFT OUTER JOIN " & _
                              "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code WHERE " & _
                              "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
                "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103)  and " & _
                    "TSPL_ITEM_DETAILS.Class_Name='Size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour' " & strOrderBy & "   ")

                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows

                        If rdbSalewithTargetQty.IsChecked = False Then
                            strItemCode = CStr(dr(0).ToString()) + "_Qty"
                            strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","

                            strItemCodeAmt = CStr(dr(0).ToString()) + "_Amt"
                            strItemCodestringAMt = strItemCodestringAMt & "[" & strItemCodeAmt & "]" & ","

                            If (ddlType.Text = "Both" Or ddlType.Text = "Quantity") Then
                                strMainItemCode = CStr(dr(0).ToString()) + "_Qty"
                                strmainItemCodeString = strmainItemCodeString & "  isnull(" & "Sum(" & "[" & strMainItemCode & "]" & " ) " & ",0)  " & "as  " & "[" & strMainItemCode & "]" & ","
                                strBothqtymain = strBothqtymain & "  isnull(" & "Sum(" & "[" & strMainItemCode & "]" & " ) " & ",0)  " & "as  " & "[" & strMainItemCode & "]" & ","

                                strBothqty = strBothqty & "0" & "as  " & "[" & strMainItemCode & "]" & ","

                            End If

                            If (ddlType.Text = "Both" Or ddlType.Text = "Value") Then
                                strMainItemCodeAmt = CStr(dr(0).ToString()) + "_Amt"
                                strmainItemCodeString = strmainItemCodeString & "  isnull(" & "Sum(" & "[" & strMainItemCodeAmt & "]" & " ) " & ",0)  " & "as  " & "[" & strMainItemCodeAmt & "]" & ","

                                strbothamtmain = strbothamtmain & "  isnull(" & "Sum(" & "[" & strMainItemCodeAmt & "]" & " ) " & ",0)  " & "as  " & "[" & strMainItemCodeAmt & "]" & ","
                                strbothamt = strbothamt & "0" & "as  " & "[" & strMainItemCodeAmt & "]" & ","
                            End If

                            strsum = strsum & " isnull(" & "Sum(" & "[" & strMainItemCode & "]" & " ) " & ",0)" & "+"
                            strsumAmt = strsumAmt & " isnull(" & "Sum(" & "[" & strMainItemCodeAmt & "]" & " ) " & ",0)" & "+"

                        Else
                            If (ddlType.Text = "Both") Then
                                strItemCode = CStr(dr(0).ToString()) + "_Qty"
                                strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","
                                strmainItemCodeString = strmainItemCodeString & "  isnull(" & "Sum(" & "[" & strItemCode & "]" & " ) " & ",0)  " & "as  " & "[" & strItemCode & "]" & ","

                                strTradeItemQty = CStr(dr(0).ToString()) + "_TradeQty"
                                strTradeItemstringQty = strTradeItemstringQty & "[" & strTradeItemQty & "]" & ","
                                strTradeMainItem = strTradeMainItem & "  isnull(" & "Sum(" & "[" & strTradeItemQty & "]" & " ) " & ",0)  " & "as  " & "[" & strTradeItemQty & "]" & ","

                                strTradeItemAmt = CStr(dr(0).ToString()) + "_TradeAmt"
                                strTradeItemstringAMt = strTradeItemstringAMt & "[" & strTradeItemAmt & "]" & ","
                                strTradeMainItem = strTradeMainItem & "  isnull(" & "Sum(" & "[" & strTradeItemAmt & "]" & " ) " & ",0)  " & "as  " & "[" & strTradeItemAmt & "]" & ","




                                strsum = strsum & " isnull(" & "Sum(" & "[" & strItemCode & "]" & " ) " & ",0)" & "+"
                                strsumTradeQty = strsumTradeQty & " isnull(" & "Sum(" & "[" & strTradeItemQty & "]" & " ) " & ",0)" & "+"
                                strsumTradeAmt = strsumTradeAmt & " isnull(" & "Sum(" & "[" & strTradeItemAmt & "]" & " ) " & ",0)" & "+"
                            End If
                        End If
                    Next
                End If

                If strItemCode <> "" Then

                    If rdbSalewithTargetQty.IsChecked Then
                        strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
                        strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)

                        strTradeItemstringQty = strTradeItemstringQty.Substring(0, strTradeItemstringQty.Length - 1)
                        strTradeItemstringAMt = strTradeItemstringAMt.Substring(0, strTradeItemstringAMt.Length - 1)

                        strTradeMainItem = strTradeMainItem.Substring(0, strTradeMainItem.Length - 1)
                        strsum = strsum.Substring(0, strsum.Length - 1)
                        strsumTradeQty = strsumTradeQty.Substring(0, strsumTradeQty.Length - 1)
                        strsumTradeAmt = strsumTradeAmt.Substring(0, strsumTradeAmt.Length - 1)
                    Else
                        strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
                        strItemCodestringAMt = strItemCodestringAMt.Substring(0, strItemCodestringAMt.Length - 1)
                        strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)
                        strsum = strsum.Substring(0, strsum.Length - 1)
                        strsumAmt = strsumAmt.Substring(0, strsumAmt.Length - 1)

                        If ddlType.Text = "Both" Then
                            strBothqty = strBothqty.Substring(0, strBothqty.Length - 1)
                            strbothamt = strbothamt.Substring(0, strbothamt.Length - 1)

                            strBothqtymain = strBothqtymain.Substring(0, strBothqtymain.Length - 1)
                            strbothamtmain = strbothamtmain.Substring(0, strbothamtmain.Length - 1)
                        End If
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                    Exit Sub
                End If

                If ddlType.Text = "Both" Then
                    If rdbSalewithTargetQty.IsChecked = False Then
                        strItem = "(" & strSQL1Group & " + '_Amt') as Itemamt ,(" & strSQL1Group & " + '_Qty')  as Item_Code"
                        strTotal = "(" + strsum + ")as TotalQty,(" + strsumAmt + ")as TotalAmt"
                    Else
                        strItem = "(" & strSQL1Group & " + '_Qty') as Item_Code , (" & strSQL1Group & " + '_TradeAmt') as TradeItemamt ,(" & strSQL1Group & " + '_TradeQty')  as TradeItem_Code"
                        strTotal = "(" + strsumTradeQty + ")as TotalQty,(" + strsumTradeAmt + ")as TotalAmt"
                        strTotalSale = "(" + strsum + ")as TotalSaleQty "
                    End If
                ElseIf ddlType.Text = "Value" Then
                    strItem = "(" & strSQL1Group & " + '_Amt') as Itemamt "
                    strTotal = "(" + strsumAmt + ")as TotalAmt"
                Else
                    strItem = "(" & strSQL1Group & " + '_Qty')  as Item_Code"
                    strTotal = "(" + strsum + ")as TotalQty"
                End If

                strSql = "SELECT  TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id as ID, " & _
                "case when scheme_item='Y' and Discount_Code='' then CONVERT(DECIMAL(18,2), " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  else 0 end as TradeQty, " & _
                "case when scheme_item='Y' and Discount_Code <> '' then CONVERT(DECIMAL(18,2), " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  else 0 end as TargetQty, " & _
                "case when scheme_item='Y' and Discount_Code='' then CONVERT(DECIMAL(18,2),(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
                " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount4 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount8 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount9)))  else 0 end as TradeAmt, " & _
                "case when scheme_item='Y' and Discount_Code <> '' then CONVERT(DECIMAL(18,2),(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
                " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount4 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount8 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount9)))  else 0 end as TargetAmt, " & _
                "CONVERT(DECIMAL(18,2),(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
                " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount4 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount8 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount9)))  as GrossAmt, " & _
                "CONVERT(DECIMAL(18,2), " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  as GrossQty, " & _
                "TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,Sale_Invoice_Date," & strItem & ", " & _
                " case when " & strSale & " then CONVERT(DECIMAL(18,2), " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end AS Saleqty,  " & _
                " case when " & strSale & " then Total_Item_Amt else 0 end AS SaleAmt,  " & _
                " case when " & strFOC & " then CONVERT(DECIMAL(18,2), " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end AS Disc_qty, " & _
                " case when " & strFOC & " then  CONVERT(DECIMAL(18,2),(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
                " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount4 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount8 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount9))) else 0 end AS DiscAmt, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code AS Company, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name " & _
                "FROM  " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS RIGHT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER INNER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code ON  " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code ON " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code ON " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
                "left outer join " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No=" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No " & _
                "WHERE  ( " & strFOC & "  or   " & strSale & ") " & strPost & " and " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103) and " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'"

                strSql2 = "SELECT  TSPL_SALE_return_DETAIL.Sale_Return_Id as ID,  " & _
                 "-case when scheme_item='Y' and Discount_Code='' then CONVERT(DECIMAL(18,2), " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  else 0 end as TradeQty, " & _
                "-case when scheme_item='Y' and Discount_Code <> '' then CONVERT(DECIMAL(18,2), " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  else 0 end as TargetQty, " & _
                 "-case when scheme_item='Y' and Discount_Code='' then  CONVERT(DECIMAL(18,2),(" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Return_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
            " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount4 " & _
            " + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount8 " & _
            " + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount9)))  else 0 end as TradeAmt, " & _
                "-case when scheme_item='Y' and Discount_Code <> '' then  CONVERT(DECIMAL(18,2),(" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Return_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
            " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount4 " & _
            " + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount8 " & _
            " + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount9)))  else 0 end as TargetAmt, " & _
                "-  (CONVERT(DECIMAL(18,2),(" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Return_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
            " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount4 " & _
            " + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount8 " & _
            " + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount9))) )   as GrossAmt, " & _
                "- ( CONVERT(DECIMAL(18,2), " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) )  as GrossQty, " & _
                "TSPL_SALE_RETURN_HEAD.Route_No,TSPL_ROUTE_MASTER.Route_Desc,TSPL_SALE_RETURN_HEAD.Sale_Return_No as Sale_Invoice_No, " & _
                "Sale_Return_Date as Sale_Invoice_Date," & strItem & ", " & _
                "- (case when " & strSaleRet & " then CONVERT(DECIMAL(18,2), " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0  end) AS Saleqty,  " & _
                "- ( case when " & strSaleRet & " then Total_Item_Amt else 0 end) AS SaleAmt,  " & _
            " - (case when " & strFOCRet & " then CONVERT(DECIMAL(18,2), " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end) AS Disc_qty, " & _
            " -(case when " & strFOCRet & " then  CONVERT(DECIMAL(18,2),(" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Return_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
            " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount4 " & _
            " + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount8 " & _
            " + " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Price_Amount9))) else 0 end) AS DiscAmt, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code AS Company, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER INNER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Item_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Item_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No=" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No " & _
            "WHERE  ( " & strFOCRet & "  or   " & strSaleRet & ")  " & strPost & " and " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & dtpend.Value & "',103) and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'"


                strInter = " Union all SELECT  TSPL_SALE_RETURN_INTER_DETAIL.Line_No as ID, " & _
                "0 as TradeQty,0 as TargetQty,0 as TradeAmt,0 as TargetAmt, " & _
                "-  (CONVERT(DECIMAL(18,2),(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0)  - (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount4  + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount8  + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount9))) )  as Grossamt, " & _
                "- (CONVERT(DECIMAL(18,2), " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ) as GrossQty, " & _
                "TSPL_SALE_RETURN_INTER_HEAD.Route_No,TSPL_ROUTE_MASTER.Route_Desc, " & _
                "TSPL_SALE_RETURN_INTER_HEAD.Document_No as Sale_Invoice_No,Document_Date as Sale_Invoice_Date, " & _
                "" & strItem & ", " & _
                "- (case when  " & strSaleInter & " then CONVERT(DECIMAL(18,2), " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end) AS Saleqty,  " & _
                "- ( case when " & strSaleInter & " then Total_Item_Amt else 0 end) AS SaleAmt,  " & _
                "- (case when  " & strFOCInter & "  then CONVERT(DECIMAL(18,2), " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end) AS Disc_qty, " & _
                " -(case when  " & strFOCInter & "  then  CONVERT(DECIMAL(18,2),(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0)  - (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount4  + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount8  + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount9))) else 0 end) AS DiscAmt,  " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code AS Company, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name FROM  " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER INNER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code " & _
                "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code ON " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD ON " & _
                "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER ON " & _
                "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code ON " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code ON " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No left outer join " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_No=" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No WHERE " & _
                " ( " & strFOCInter & "  or   " & strSaleInter & ")  " & strPostInter & "  and  " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) >=  convert(date, '" & dtpstart.Value & "',103)  AND " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) <=  convert(date, '" & dtpend.Value & "',103)  and " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour' "
            Else
                strSql = " SELECT  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No, convert(varchar(12)," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as Sale_Invoice_Date, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty AS Disc_qty," & _
                "TSPL_SALE_INVOICE_DETAIL_1.Invoice_Qty AS Main_Qty, " & _
                "TSPL_SALE_INVOICE_DETAIL_1.Item_Code AS Main_Item, " & _
                "TSPL_SALE_INVOICE_DETAIL_1.Unit_code AS Main_Unit, " & _
                "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * Conversion_Factor, 0) - (TSPL_SALE_INVOICE_DETAIL.Price_Amount1) AS Ret_price,(ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * Conversion_Factor, 0) - (TSPL_SALE_INVOICE_DETAIL.Price_Amount1 + TSPL_SALE_INVOICE_DETAIL.Price_Amount2 + TSPL_SALE_INVOICE_DETAIL.Price_Amount3 + TSPL_SALE_INVOICE_DETAIL.Price_Amount4 + TSPL_SALE_INVOICE_DETAIL.Price_Amount5 + TSPL_SALE_INVOICE_DETAIL.Price_Amount6 + TSPL_SALE_INVOICE_DETAIL.Price_Amount7 + TSPL_SALE_INVOICE_DETAIL.Price_Amount8 + TSPL_SALE_INVOICE_DETAIL.Price_Amount9)) AS CalcRet_price, " & _
                "case when TSPL_SALE_INVOICE_DETAIL.Discount_Code <> '' then (select  top 1  Description from " + clsCommon.ReplicateDBString + "TSPL_Discount_Master where Code=TSPL_SALE_INVOICE_DETAIL.Discount_Code) else 'Trade Scheme' end as Scheme_Desc," & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Remarks, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code, '" & dtpstart.Value & "' AS Fdate, '" & dtpend.Value & "' AS ToDate, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) AS ConvetedQty, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,Salesman_Desc,'" & strLoca & "' as SelectLoc,'" & strClass & "' as SelectClass,0 as DiscAmount " & _
                "FROM  " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL RIGHT OUTER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 RIGHT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL  ON TSPL_ITEM_DETAILS_1.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code AND  " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
                 "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER LEFT OUTER JOIN " & _
                 "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
                 "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code ON  " & _
                 "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No " & _
                 "left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL as TSPL_SALE_INVOICE_DETAIL_1 on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL_1.Sale_Invoice_No and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Main_Item=TSPL_SALE_INVOICE_DETAIL_1.Item_Code " & _
                 "WHERE  " & strFOC & "  " & strPost & " and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND  " & _
                 "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103) and " & _
                 " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour'  and TSPL_SALE_INVOICE_DETAIL_1.Scheme_Item='N'"

                strSql2 = "  SELECT  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No as Sale_Invoice_No, convert(varchar(12)," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_Date,103) as Invoice_Date, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No,  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code, - (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty) AS Disc_qty," & _
                "TSPL_SALE_RETURN_DETAIL_1.Return_Qty AS Main_Qty, " & _
                "TSPL_SALE_RETURN_DETAIL_1.Item_Code AS Main_Item, " & _
                "TSPL_SALE_RETURN_DETAIL_1.Unit_code AS Main_Unit, " & _
                "ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * Conversion_Factor, 0) - (TSPL_SALE_RETURN_DETAIL.Price_Amount1) AS Ret_price,(ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * Conversion_Factor, 0) - (TSPL_SALE_RETURN_DETAIL.Price_Amount1 + TSPL_SALE_RETURN_DETAIL.Price_Amount2 + TSPL_SALE_RETURN_DETAIL.Price_Amount3 + TSPL_SALE_RETURN_DETAIL.Price_Amount4 + TSPL_SALE_RETURN_DETAIL.Price_Amount5 + TSPL_SALE_RETURN_DETAIL.Price_Amount6 + TSPL_SALE_RETURN_DETAIL.Price_Amount7 + TSPL_SALE_RETURN_DETAIL.Price_Amount8 + TSPL_SALE_RETURN_DETAIL.Price_Amount9)) AS CalcRet_price, " & _
                " case when TSPL_SALE_RETURN_DETAIL.Discount_Code <> '' then (select  top 1  Description from " + clsCommon.ReplicateDBString + "TSPL_Discount_Master where Code=TSPL_SALE_RETURN_DETAIL.Discount_Code) else 'Trade Scheme' end as Scheme_Desc," & _
                "'' as Remarks, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code, '" & dtpstart.Value & "' AS Fdate, '" & dtpend.Value & "' AS ToDate, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, -(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) AS ConvetedQty, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,Salesman_Desc,'" & strLoca & "' as SelectLoc,'" & strClass & "' as SelectClass,0 as DiscAmount " & _
                "FROM  " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL RIGHT OUTER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 RIGHT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL  ON TSPL_ITEM_DETAILS_1.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code AND  " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location LEFT OUTER JOIN " & _
                "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
                 "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER LEFT OUTER JOIN " & _
                 "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
                 "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code ON  " & _
                 "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No " & _
                 "left outer join " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL as TSPL_SALE_RETURN_DETAIL_1 on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=TSPL_SALE_RETURN_DETAIL_1.Sale_Return_No and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Main_Item=TSPL_SALE_RETURN_DETAIL_1.Item_Code  " & _
                 "WHERE  " & strFOCRet & "  " & strPost & " and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND  " & _
                 "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & dtpend.Value & "',103) and " & _
                 " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name='size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour' and TSPL_SALE_RETURN_DETAIL_1.Scheme_Item='N'"
            End If

            If strLocAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_head.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
                strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
                strSaleInter += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "

            End If
            If strCustAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_head.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                strSaleInter += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "

            End If

            If strClassAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                strSaleInter += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "

            End If
            If strItemAll = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
                strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_return_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
                strSaleInter += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "

            End If
            If strCustGroup = "N" Then
                strSql += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGroup.CheckedValue) + ") "
                strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGroup.CheckedValue) + ") "
                strSaleInter += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGroup.CheckedValue) + ") "

            End If
            Dim strquery As String = strSql & "  Union all " & strSql2 & strInter
            Dim stralias As String
            If ddlType.Text = "Both" Then
                stralias = "aaa"
            Else
                stralias = "pvt1"
            End If
            Dim strSaleQTy As String = ""
            If rdbSummary.IsChecked Then
                strSaleQTy = "convert(decimal(18,2), ( isnull((select SUM(Invoice_Qty/Conversion_Factor) from TSPL_SALE_INVOICE_HEAD left outer join  " & _
                "TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer join " & _
                "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
                "TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code where TSPL_SALE_INVOICE_HEAD.Comp_Code=Company and " & _
                "TSPL_SALE_INVOICE_HEAD.Location=" & stralias & ".Location and TSPL_SALE_INVOICE_HEAD.Route_No=" & stralias & ".Route_No  and TSPL_SALE_INVOICE_HEAD.Cust_Code= " & stralias & ".Cust_Code " & strPost & "  and " & _
                "scheme_item='N' and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND  " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103)),0) " & _
                "- isnull((select SUM(Return_Qty/Conversion_Factor) from TSPL_SALE_RETURN_HEAD left outer join TSPL_SALE_RETURN_DETAIL on " & _
                "TSPL_SALE_return_HEAD.Sale_Return_No=TSPL_SALE_return_DETAIL.Sale_Return_No left outer join TSPL_ITEM_UOM_DETAIL on  " & _
                "TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_SALE_RETURN_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code where  " & _
                "TSPL_SALE_RETURN_HEAD.Comp_Code=Company and TSPL_SALE_RETURN_HEAD.Location=" & stralias & ".Location and TSPL_SALE_RETURN_HEAD.Route_No=" & stralias & ".Route_No   and TSPL_SALE_RETURN_HEAD.Cust_Code=" & stralias & ".Cust_Code " & _
                "" & strPost & " and scheme_item='N' and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND  " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & dtpend.Value & "',103)  ),0) " & _
                " - isnull((select SUM(Qty/Conversion_Factor) from TSPL_SALE_RETURN_INTER_HEAD left outer join " & _
                "TSPL_SALE_RETURN_INTER_DETAIL on TSPL_SALE_RETURN_INTER_HEAD.Document_No=TSPL_SALE_RETURN_INTER_DETAIL.Document_No left outer join " & _
                "TSPL_ITEM_UOM_DETAIL on  TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
                "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code where  TSPL_SALE_RETURN_INTER_HEAD.Comp_Code=Company and  " & _
                "TSPL_SALE_RETURN_INTER_HEAD.Location=" & stralias & ".Location and TSPL_SALE_RETURN_INTER_HEAD.Route_No=" & stralias & ".Route_No " & _
                "and TSPL_SALE_RETURN_INTER_HEAD.Cust_Code=" & stralias & ".Cust_Code " & _
                "and Is_Post=1  and scheme_item='N' and " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) >= convert(date, '" & dtpstart.Value & "',103)  AND  " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) <= convert(date, '" & dtpend.Value & "',103)   ),0)    )) "
            ElseIf rdbSummaryDoc.IsChecked Then
                strSaleQTy = "convert(decimal(18,2), ( isnull((select SUM(Invoice_Qty/Conversion_Factor) from TSPL_SALE_INVOICE_HEAD left outer join  " & _
                "TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer join " & _
                "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
                "TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code where TSPL_SALE_INVOICE_HEAD.Comp_Code=Company and " & _
                "TSPL_SALE_INVOICE_HEAD.Location=" & stralias & ".Location and TSPL_SALE_INVOICE_HEAD.Cust_Code= " & stralias & ".Cust_Code " & strPost & "  and " & _
                " TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" & stralias & ".Sale_Invoice_No  and scheme_item='N' and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND  " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103)),0) " & _
                "- isnull((select SUM(Return_Qty/Conversion_Factor) from TSPL_SALE_RETURN_HEAD left outer join TSPL_SALE_RETURN_DETAIL on " & _
                "TSPL_SALE_return_HEAD.Sale_Return_No=TSPL_SALE_return_DETAIL.Sale_Return_No left outer join TSPL_ITEM_UOM_DETAIL on  " & _
                "TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and TSPL_SALE_RETURN_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code where  " & _
                "TSPL_SALE_RETURN_HEAD.Comp_Code=Company and TSPL_SALE_RETURN_HEAD.Location=" & stralias & ".Location and TSPL_SALE_RETURN_HEAD.Cust_Code=" & stralias & ".Cust_Code " & _
                "" & strPost & " and scheme_item='N' and TSPL_SALE_RETURN_HEAD.Sale_Return_No=" & stralias & ".Sale_Invoice_No and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND  " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & dtpend.Value & "',103)  ),0)  " & _
                " - isnull((select SUM(Qty/Conversion_Factor) from TSPL_SALE_RETURN_INTER_HEAD left outer join " & _
                "TSPL_SALE_RETURN_INTER_DETAIL on TSPL_SALE_RETURN_INTER_HEAD.Document_No=TSPL_SALE_RETURN_INTER_DETAIL.Document_No left outer join " & _
                "TSPL_ITEM_UOM_DETAIL on  TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
                "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code where  TSPL_SALE_RETURN_INTER_HEAD.Comp_Code=Company and  " & _
                "TSPL_SALE_RETURN_INTER_HEAD.Location=" & stralias & ".Location  " & _
                "and TSPL_SALE_RETURN_INTER_HEAD.Cust_Code=" & stralias & ".Cust_Code and TSPL_SALE_RETURN_INTER_HEAD.Document_No=" & stralias & ".Sale_Invoice_No " & _
                "and Is_Post=1  and scheme_item='N' and " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) >= convert(date, '" & dtpstart.Value & "',103)  AND  " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) <= convert(date, '" & dtpend.Value & "',103)   ),0)    )) "
            End If
            If rdbSummary.IsChecked = True Then
                If ddlType.Text = "Quantity" Then
                    str2 = "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                    "max(Customer_Name) as Customer_Name,Route_No,Route_Desc,SUM(Saleqty) as SaleQty, " & _
                    "" & strTotal & "," & strmainItemCodeString & "  from  " & _
                    "( " & strquery & "   ) down  pivot " & _
                    "(SUM(Disc_qty) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1 " & _
                    "group by company,Location,Cust_Code,Route_No,Route_Desc"
                ElseIf ddlType.Text = "Value" Then
                    str2 = "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                    "max(Customer_Name) as Customer_Name,Route_No,Route_Desc,SUM(Saleqty) as SaleQty, " & _
                    "" & strTotal & "," & strmainItemCodeString & "  from  " & _
                    "( " & strquery & "  ) down  pivot " & _
                    "(SUM(DiscAmt) FOR Itemamt IN ( " & strItemCodestringAMt & ")) AS pvt1  " & _
                    "group by company,Location,Cust_Code,Route_No,Route_Desc"
                ElseIf ddlType.Text = "Both" Then
                    'str2 = "select company,max(Comp_Name) as Comp_Name,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                    '"max(Customer_Name) as Customer_Name,sum(Saleqty) as SaleQty, " & _
                    '"" & strTotal & "," & strmainItemCodeString & "  from  " & _
                    '"( " & strquery & "   ) down  pivot " & _
                    '"(SUM(disc_qty) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1  pivot " & _
                    '"(SUM(DiscAmt) FOR Itemamt IN ( " & strItemCodestringAMt & ")) AS pvt2 " & _
                    '"group by company,Location,Cust_Code"
                    str2 = "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                    "max(Customer_Name) as Customer_Name,Route_No,Route_Desc,sum(Saleqty) as SaleQty, " & _
                    "" + strsum + " as TotalQty,0 as TotalAmt," & strBothqtymain & "  ," & strbothamt & "  from  " & _
                    "( " & strquery & "   ) down  pivot " & _
                    "(SUM(Disc_qty) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1 " & _
                    "group by company,Location,Cust_Code,Route_No,Route_Desc " & _
                    "union all " & _
                    "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                    "max(Customer_Name) as Customer_Name,Route_No,Route_Desc,0 as SaleQty, " & _
                    " 0 as TotalQty," + strsumAmt + " as TotalAmt ," & strBothqty & "," & strbothamtmain & "  from  " & _
                    "( " & strquery & "  ) down  pivot " & _
                    "(SUM(DiscAmt) FOR Itemamt IN ( " & strItemCodestringAMt & ")) AS pvt1  " & _
                    "group by company,Location,Cust_Code,Route_No,Route_Desc"

                    str2 = "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                    "max(Customer_Name) as Customer_Name,Route_No,Route_Desc," & strSaleQTy & " as SaleQty,sum(TotalQty) as TotalQty, " & _
                    "sum(TotalAmt) as TotalAmt," & strBothqtymain & " ," & strbothamtmain & " from ( " & str2 & " ) aaa group by company,Location,Cust_Code,Route_No,Route_Desc "
                End If
                strSql = str2

            ElseIf rdbSummaryDoc.IsChecked Then
                If rdbSummaryDoc.IsChecked AndAlso rdbGross.Checked = True AndAlso rdbFoc.IsChecked = True Then
                    If ddlType.Text = "Quantity" Then
                        str2 = "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                        "max(Customer_Name) as Customer_Name,max(Route_No) as Route_No,max(Route_Desc) as Route_Desc,Sale_Invoice_No,Sale_Invoice_Date, " & _
                        "SUM(Disc_qty) as DiscQty,sum(tradeQty) as TradeQty,sum(TargetQty) as TargetQty,SUM(Saleqty) as SaleQty, " & _
                        "" & strTotal & "," & strmainItemCodeString & "  from  " & _
                        "( " & strquery & "   ) down  pivot " & _
                        "(SUM(GrossQty) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1 " & _
                        "group by company,Location,Cust_Code,Sale_Invoice_No,Sale_Invoice_Date"
                    ElseIf ddlType.Text = "Value" Then
                        str2 = "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                        "max(Customer_Name) as Customer_Name,max(Route_No) as Route_No,max(Route_Desc) as Route_Desc,Sale_Invoice_No, " & _
                        "Sale_Invoice_Date,SUM(Discamt) as DiscAmt,sum(tradeAmt) as TradeAmt,sum(TargetAmt) as TargetAmt,SUM(Saleqty) as SaleQty, " & _
                        "" & strTotal & "," & strmainItemCodeString & "  from  " & _
                        "( " & strquery & "  ) down  pivot " & _
                        "(SUM(Grossamt) FOR Itemamt IN ( " & strItemCodestringAMt & ")) AS pvt1  " & _
                        "group by company,Location,Cust_Code,Sale_Invoice_No,Sale_Invoice_Date"
                    ElseIf ddlType.Text = "Both" Then
                        str2 = "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                        "max(Customer_Name) as Customer_Name,max(Route_No) as Route_No,max(Route_Desc) as Route_Desc,Sale_Invoice_No, " & _
                        "Sale_Invoice_Date,SUM(Disc_qty) as DiscQty,0 as DiscAmt,sum(tradeQty) as TradeQty,sum(tradeAmt) as TradeAmt,sum(TargetQty) as TargetQty,sum(TargetAmt) as TargetAmt,sum(Saleqty) as SaleQty, " & _
                        "" + strsum + " as TotalQty,0 as TotalAmt," & strBothqtymain & "  ," & strbothamt & "  from  " & _
                        "( " & strquery & "   ) down  pivot " & _
                        "(SUM(Grossqty) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1 " & _
                        "group by company,Location,Cust_Code,Sale_Invoice_No,Sale_Invoice_Date " & _
                        "union all " & _
                        "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                        "max(Customer_Name) as Customer_Name,max(Route_No) as Route_No,max(Route_Desc) as Route_Desc,Sale_Invoice_No, " & _
                        "Sale_Invoice_Date,0 as DiscQty,SUM(Discamt) as DiscAmt,0 as TradeQty,0 as TradeAmt,0 as TargetQty,0 as TargetAmt,0 as SaleQty, " & _
                        " 0 as TotalQty," + strsumAmt + " as TotalAmt ," & strBothqty & "," & strbothamtmain & "  from  " & _
                        "( " & strquery & "  ) down  pivot " & _
                        "(SUM(GrossAmt) FOR Itemamt IN ( " & strItemCodestringAMt & ")) AS pvt1  " & _
                        "group by company,Location,Cust_Code,Sale_Invoice_No,Sale_Invoice_Date"

                        str2 = "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                        "max(Customer_Name) as Customer_Name,max(Route_No) as Route_No,max(Route_Desc) as Route_Desc,Sale_Invoice_No, " & _
                        "Sale_Invoice_Date,SUM(Discqty) as DiscQty,SUM(Discamt) as DiscAmt,sum(tradeQty) as TradeQty,sum(tradeAmt) as TradeAmt,sum(TargetQty) as TargetQty,sum(TargetAmt) as TargetAmt,sum(Saleqty) as SaleQty,sum(TotalQty) as TotalQty, " & _
                        "sum(TotalAmt) as TotalAmt," & strBothqtymain & " ," & strbothamtmain & " from ( " & str2 & " ) aaa group by company,Location,Cust_Code,Sale_Invoice_No,Sale_Invoice_Date "
                    End If
                Else

                    If ddlType.Text = "Quantity" Then
                        str2 = "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                        "max(Customer_Name) as Customer_Name,max(Route_No) as Route_No,max(Route_Desc) as Route_Desc,Sale_Invoice_No,Sale_Invoice_Date, " & _
                        "SUM(Saleqty) as SaleQty, " & _
                        "" & strTotal & "," & strmainItemCodeString & "  from  " & _
                        "( " & strquery & "   ) down  pivot " & _
                        "(SUM(Disc_qty) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1 " & _
                        "group by company,Location,Cust_Code,Sale_Invoice_No,Sale_Invoice_Date"
                    ElseIf ddlType.Text = "Value" Then
                        str2 = "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                        "max(Customer_Name) as Customer_Name,max(Route_No) as Route_No,max(Route_Desc) as Route_Desc,Sale_Invoice_No, " & _
                        "Sale_Invoice_Date,SUM(Saleqty) as SaleQty, " & _
                        "" & strTotal & "," & strmainItemCodeString & "  from  " & _
                        "( " & strquery & "  ) down  pivot " & _
                        "(SUM(DiscAmt) FOR Itemamt IN ( " & strItemCodestringAMt & ")) AS pvt1  " & _
                        "group by company,Location,Cust_Code,Sale_Invoice_No,Sale_Invoice_Date"
                    ElseIf ddlType.Text = "Both" Then
                        str2 = "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                        "max(Customer_Name) as Customer_Name,max(Route_No) as Route_No,max(Route_Desc) as Route_Desc,Sale_Invoice_No, " & _
                        "Sale_Invoice_Date,sum(Saleqty) as SaleQty, " & _
                        "" + strsum + " as TotalQty,0 as TotalAmt," & strBothqtymain & "  ," & strbothamt & "  from  " & _
                        "( " & strquery & "   ) down  pivot " & _
                        "(SUM(Disc_qty) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1 " & _
                        "group by company,Location,Cust_Code,Sale_Invoice_No,Sale_Invoice_Date " & _
                        "union all " & _
                        "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                        "max(Customer_Name) as Customer_Name,max(Route_No) as Route_No,max(Route_Desc) as Route_Desc,Sale_Invoice_No,Sale_Invoice_Date,0 as SaleQty, " & _
                        " 0 as TotalQty," + strsumAmt + " as TotalAmt ," & strBothqty & "," & strbothamtmain & "  from  " & _
                        "( " & strquery & "  ) down  pivot " & _
                        "(SUM(DiscAmt) FOR Itemamt IN ( " & strItemCodestringAMt & ")) AS pvt1  " & _
                        "group by company,Location,Cust_Code,Sale_Invoice_No,Sale_Invoice_Date"

                        str2 = "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                        "max(Customer_Name) as Customer_Name,max(Route_No) as Route_No,max(Route_Desc) as Route_Desc,Sale_Invoice_No,Sale_Invoice_Date," & strSaleQTy & " as SaleQty,sum(TotalQty) as TotalQty, " & _
                        "sum(TotalAmt) as TotalAmt," & strBothqtymain & " ," & strbothamtmain & " from ( " & str2 & " ) aaa group by company,Location,Cust_Code,Sale_Invoice_No,Sale_Invoice_Date "
                    End If

                End If
                strSql = str2
            ElseIf rdbSalewithTargetQty.IsChecked Then
                strSql = " select ID,Saleqty + TargetQty as SaletargetQty,TradeQty,TargetQty,TargetAmt,TradeAmt,GrossQty,GrossAmt,Saleqty,Route_No, " & _
                "Route_Desc,Sale_Invoice_No,Sale_Invoice_Date,Item_Code,TradeItem_code,TradeItemamt,Disc_qty,DiscAmt,Location,Location_Desc,Cust_Code,Customer_Name,Company,Comp_Name from ( " & strquery & " ) aa "
                str2 = "select company,Location,max(Location_Desc) as Location_Desc,Cust_Code, " & _
                       "max(Customer_Name) as Customer_Name,max(Route_No) as Route_No,max(Route_Desc) as Route_Desc,Sale_Invoice_No, " & _
                       "Sale_Invoice_Date, " & _
                       "" & strmainItemCodeString & "," & strTotalSale & "," & strTradeMainItem & " ," & strTotal & " from  " & _
                       "( " & strSql & "  ) down   " & _
                       " pivot (SUM(SaletargetQty) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1  " & _
                       " pivot (SUM(TradeQty) FOR TradeItem_code IN ( " & strTradeItemstringQty & ")) AS pvt2  " & _
                       " pivot (SUM(TradeAmt) FOR TradeItemamt IN ( " & strTradeItemstringAMt & ")) AS pvt3  " & _
                       "group by company,Location,Cust_Code,Sale_Invoice_No,Sale_Invoice_Date"
                strSql = str2
            ElseIf rdbDetail.IsChecked Then
                strSql = "select Location_Code as Location ,Location_Desc,Cust_Group_Code,Cust_Group_Desc,Cust_Type_Code, " & _
                "Cust_Type_Desc,Cust_Code,Customer_Name,Sale_Invoice_No,Invoicedate,Route_No,Route_Desc,Salesname,FOCItem, " & _
                "FOCUnit,convert(decimal(18,2),FOCQty) as FOCQty,mainItem,mainUnit,convert(decimal(18,2),mainqty) as mainqty,Ret_price,DP, " & _
                "convert(decimal(18,2),Discamt) as Discamt,Scheme_Desc,convert(decimal(18,2),FOCConvQty) as FOCConvQty  from ( " & _
                "select Sale_Invoice_No,Invoicedate,FOCItem,FOCUnit,FOCQty,mainItem,mainUnit,mainqty,Discount_Code,Ret_price, " & _
                "CalcRet_price as DP,(CalcRet_price * FOCConvQty) as Discamt,FOCConvQty,TSPL_ROUTE_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc, " & _
                "TSPL_EMPLOYEE_MASTER.EMP_CODE as SalesCode,TSPL_EMPLOYEE_MASTER.EMP_CODE as Salesname,TSPL_CUSTOMER_MASTER.Cust_Code, " & _
                "TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Cust_Group_Code,Cust_Group_Desc,TSPL_CUSTOMER_MASTER.Cust_Type_Code, " & _
                "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc,Location_Code,Location_Desc,CASE WHEN Discount_Code='' THEN 'TRADE Scheme' else " & _
                "TSPL_Discount_Master.Description end as Scheme_Desc from( " & _
                "select b.Item_Code as FOCItem,b.FOCUnit,b.FOCQty,c.Item_Code AS mainItem,c.Unit_code as mainUnit,c.mainqty,b.Sale_Invoice_No, " & _
                "b.Discount_Code,b.Sale_Invoice_Date as Invoicedate,Ret_price,CalcRet_price,FOCConvQty,Cust_Code,Salesman_Code,Route_No,Location  from " & _
                "(SELECT Cust_Code,Salesman_Code,Route_No,TSPL_SALE_INVOICE_HEAD.Location,Sale_Invoice_Date,Discount_Code, " & _
                "TSPL_SALE_INVOICE_DETAIL.Item_Code ,Unit_code as FOCUnit,Main_Item,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, " & _
                "Sale_Invoice_Date as Invoicedate,0 as mainqty,Invoice_Qty  as FOCQty,Invoice_Qty/Conversion_Factor  as FOCConvQty, " & _
                "ISNULL(MRP_Amt * Conversion_Factor, 0) - (Price_Amount1) AS Ret_price,(ISNULL(MRP_Amt * Conversion_Factor, 0) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)) AS CalcRet_price  " & _
                "FROM TSPL_SALE_INVOICE_HEAD left outer join TSPL_SALE_INVOICE_DETAIL on " & _
                "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer join " & _
                "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code  and " & _
                "TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code WHERE " & strFOC & " and " & _
                "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND  " & _
                 "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103)  " & strPost & "   ) b  " & _
                "left outer join (select Item_Code , case when count(Unit_code) >  1 then 'FC' else MAX(Unit_code) end as Unit_code , " & _
                "Sale_Invoice_No,case when COUNT(unit_code) > 1 then (select Invoice_Qty from TSPL_SALE_INVOICE_DETAIL where " & _
                "Sale_Invoice_No=a.Sale_Invoice_No and Item_Code=a.Item_Code and Unit_code='FC' and Scheme_Item='N') else MAX(Invoice_Qty)  end as mainqty, " & _
                "0 as FOCQty   from (SELECT Item_Code,Unit_code,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,Invoice_Qty FROM TSPL_SALE_INVOICE_HEAD left outer join " & _
                "TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No WHERE Scheme_Item='N' and " & _
                "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND  " & _
                 "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103)  " & strPost & "   ) a  " & _
                "group by Sale_Invoice_No ,item_Code ) as c  on b.Main_Item=c.Item_Code and b.Sale_Invoice_No=c.Sale_Invoice_No  " & _
                "union all " & _
                "select b.Item_Code  as FOCItem,b.FOCUnit,- b.FOCQty,c.Item_Code AS mainItem,c.Unit_code as mainUnit,- c.mainqty,b.Sale_Return_No, " & _
                "b.Discount_Code,b.Sale_Return_Date as Invoicedate,Ret_price,CalcRet_price,-FOCConvQty,Cust_Code,Salesman_Code,Route_No,Location   from  " & _
                "(SELECT Cust_Code,Salesman_Code,Route_No,TSPL_SALE_RETURN_HEAD.Location,Sale_Return_Date,Discount_Code,TSPL_SALE_RETURN_DETAIL.Item_Code , " & _
                "Unit_code as FOCUnit,Main_Item,TSPL_SALE_RETURN_HEAD.Invoice_No,0 as mainqty,Return_Qty  as FOCQty,Return_Qty/Conversion_Factor as FOCConvQty, " & _
                "TSPL_SALE_RETURN_HEAD.Sale_Return_No,ISNULL(MRP_Amt * Conversion_Factor, 0) - (Price_Amount1) AS Ret_price, " & _
                "(ISNULL(MRP_Amt * Conversion_Factor, 0) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)) AS CalcRet_price " & _
                "FROM TSPL_SALE_RETURN_HEAD left outer join TSPL_SALE_RETURN_DETAIL on " & _
                "TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_RETURN_DETAIL.Sale_Return_No left outer join TSPL_ITEM_UOM_DETAIL on " & _
                "TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code  and TSPL_SALE_RETURN_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "WHERE " & strFOCRet & " And " & _
                "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND  " & _
                 "convert(date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & dtpend.Value & "',103)   " & strPost & " ) b " & _
                "left outer join (select Item_Code , case when count(Unit_code) >  1 then 'FC' else MAX(Unit_code) end as Unit_code ,Sale_Invoice_No, " & _
                "case when COUNT(unit_code) > 1 then (select Invoice_Qty from TSPL_SALE_INVOICE_DETAIL where Sale_Invoice_No=a.Sale_Invoice_No and " & _
                "Item_Code=a.Item_Code and Unit_code='FC' and Scheme_Item='N') else MAX(Invoice_Qty)  end as mainqty,0 as FOCQty   from " & _
                "(SELECT Item_Code,Unit_code,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,Invoice_Qty FROM TSPL_SALE_INVOICE_HEAD left outer join " & _
                "TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No WHERE Scheme_Item='N' and " & _
                "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & dtpstart.Value & "',103) AND  " & _
                 "convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & dtpend.Value & "',103)  " & strPost & ") a " & _
                "group by Sale_Invoice_No ,item_Code ) as c  on b.Main_Item=c.Item_Code and b.Invoice_No=c.Sale_Invoice_No  ) semifinal  " & _
                "LEFT OUTER JOIN TSPL_Discount_Master ON semifinal.Discount_Code=TSPL_Discount_Master.Code left outer join TSPL_CUSTOMER_MASTER on " & _
                "semifinal.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_ROUTE_MASTER on semifinal.Route_No=TSPL_ROUTE_MASTER.Route_No left outer join " & _
                "TSPL_LOCATION_MASTER on semifinal.Location=TSPL_LOCATION_MASTER.Location_Code left outer join " & _
                "TSPL_EMPLOYEE_MASTER on semifinal.Salesman_Code=TSPL_EMPLOYEE_MASTER.EMP_CODE left outer join " & _
                "TSPL_CUSTOMER_TYPE_MASTER on TSPL_CUSTOMER_MASTER.Cust_Type_Code=TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code left outer join " & _
                "TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_MASTER.Cust_Group_Code=TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code ) final  where 2=2"


                If strLocAll = "N" Then
                    strSql += " and Location_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")) "
                End If
                If strCustAll = "N" Then
                    strSql += " and  Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                End If
                If strClassAll = "N" Then
                    strSql += " and Cust_Type_Code in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                End If
                If strItemAll = "N" Then
                    strSql += " and FOCItem in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ") "
                End If
                If strCustGroup = "N" Then
                    strSql += " and Cust_Group_Code in (" + clsCommon.GetMulcallString(cbgCustGroup.CheckedValue) + ") "
                End If
            End If
            Dim ArrDBName As ArrayList = Nothing
            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            strquery = clsCommon.GetQueryWithAllSelectedDataBase(strSql, ArrDBName, False)

            If rdbSummaryDoc.IsChecked Then
                strquery += " Order By Sale_Invoice_Date"
            ElseIf rdbDetail.IsChecked Then
                strquery += " Order By Invoicedate "
            End If
            'If blnRefresh = True Then
            dt = clsDBFuncationality.GetDataTable(strquery)
            GV1.DataSource = Nothing
            GV1.Columns.Clear()
            GV1.Rows.Clear()
            GV1.GroupDescriptors.Clear()
            GV1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
            GV1.DataSource = dt
            SetGridFormationOFGV1()

            'If rdbDetail.IsChecked = True Then
            '    FrmSalerReport.funreport(strQuery, "crptTradeDiscInvoicewise", "Trade Discount report")
            'End If


        Catch ex As Exception
            If ex.Message.Substring(0, 10) = "The column" Then
                If rdbPack.IsChecked = True Then
                    MsgBox("Please check Pack Seq no ")
                ElseIf rdbFlavour.IsChecked = True Then
                    MsgBox("Please check Flavour Seq no ")
                End If

            Else
                common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        End Try
    End Sub


    Sub SetGridFormationOFGV1()
        Dim strItemCode As String = ""

        GV1.TableElement.TableHeaderHeight = 40
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).IsVisible = False
        Next

        If rdbSummary.IsChecked = True Then
            GV1.Columns("company").IsVisible = True
            GV1.Columns("company").Width = 50
            GV1.Columns("company").HeaderText = "company"

            GV1.Columns("Location").IsVisible = True
            GV1.Columns("Location").Width = 70
            GV1.Columns("Location").HeaderText = "Location"

            GV1.Columns("Location_Desc").IsVisible = True
            GV1.Columns("Location_Desc").Width = 100
            GV1.Columns("Location_Desc").HeaderText = "Loc Desc"

            GV1.Columns("Cust_Code").IsVisible = True
            GV1.Columns("Cust_Code").Width = 100
            GV1.Columns("Cust_Code").HeaderText = "Customer"

            GV1.Columns("Customer_Name").IsVisible = True
            GV1.Columns("Customer_Name").Width = 100
            GV1.Columns("Customer_Name").HeaderText = "Customer Name"

            GV1.Columns("Route_No").IsVisible = True
            GV1.Columns("Route_No").Width = 100
            GV1.Columns("Route_No").HeaderText = "Route"

            GV1.Columns("Route_Desc").IsVisible = True
            GV1.Columns("Route_Desc").Width = 100
            GV1.Columns("Route_Desc").HeaderText = "Route Desc"

            GV1.Columns("SaleQty").IsVisible = True
            GV1.Columns("SaleQty").Width = 100
            GV1.Columns("SaleQty").HeaderText = "Sale Qty"


            Dim intType As Integer
            If (ddlType.Text = "Both") Then

                GV1.Columns("TotalQty").IsVisible = True
                GV1.Columns("TotalQty").Width = 80
                GV1.Columns("TotalQty").HeaderText = "Total Qty"

                GV1.Columns("TotalAmt").IsVisible = True
                GV1.Columns("TotalAmt").Width = 80
                GV1.Columns("TotalAmt").HeaderText = "Total Amount"
                intType = 10
            ElseIf (ddlType.Text = "Value") Then
                GV1.Columns("TotalAmt").IsVisible = True
                GV1.Columns("TotalAmt").Width = 80
                GV1.Columns("TotalAmt").HeaderText = "Total Amount"
                intType = 9
            ElseIf (ddlType.Text = "Quantity") Then
                GV1.Columns("TotalQty").IsVisible = True
                GV1.Columns("TotalQty").Width = 80
                GV1.Columns("TotalQty").HeaderText = "Total Qty"
                intType = 9
            End If
            For ii As Integer = intType To GV1.Columns.Count - 1
                strItemCode = GV1.Columns(ii).FieldName
                GV1.Columns("" & strItemCode & "").IsVisible = True
                GV1.Columns("" & strItemCode & "").Width = 80
                GV1.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
            Next


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            If (ddlType.Text = "Both") Then
                Dim item1 As New GridViewSummaryItem("TotalQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("TotalAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("SaleQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
            ElseIf ddlType.Text = "Value" Then
                Dim item2 As New GridViewSummaryItem("TotalAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("SaleQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
            ElseIf ddlType.Text = "Quantity" Then
                Dim item1 As New GridViewSummaryItem("TotalQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item3 As New GridViewSummaryItem("SaleQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
            End If
            For ii As Integer = intType To GV1.Columns.Count - 1
                intCount = intCount + 1
                strItemCode = GV1.Columns(ii).FieldName
                Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item16)
            Next
            GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf rdbSummaryDoc.IsChecked Then
            GV1.Columns("company").IsVisible = True
            GV1.Columns("company").Width = 50
            GV1.Columns("company").HeaderText = "company"


            GV1.Columns("Location").IsVisible = True
            GV1.Columns("Location").Width = 70
            GV1.Columns("Location").HeaderText = "Location"

            GV1.Columns("Location_Desc").IsVisible = True
            GV1.Columns("Location_Desc").Width = 100
            GV1.Columns("Location_Desc").HeaderText = "Loc Desc"

            GV1.Columns("Cust_Code").IsVisible = True
            GV1.Columns("Cust_Code").Width = 100
            GV1.Columns("Cust_Code").HeaderText = "Customer"

            GV1.Columns("Customer_Name").IsVisible = True
            GV1.Columns("Customer_Name").Width = 100
            GV1.Columns("Customer_Name").HeaderText = "Customer Name"

            GV1.Columns("Route_No").IsVisible = True
            GV1.Columns("Route_No").Width = 100
            GV1.Columns("Route_No").HeaderText = "Route"

            GV1.Columns("Route_Desc").IsVisible = True
            GV1.Columns("Route_Desc").Width = 100
            GV1.Columns("Route_Desc").HeaderText = "Route Desc"

            GV1.Columns("Sale_Invoice_No").IsVisible = True
            GV1.Columns("Sale_Invoice_No").Width = 100
            GV1.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

            GV1.Columns("Sale_Invoice_Date").IsVisible = True
            GV1.Columns("Sale_Invoice_Date").Width = 100
            GV1.Columns("Sale_Invoice_Date").HeaderText = "Sale Invoice Date"

            GV1.Columns("SaleQty").IsVisible = True
            GV1.Columns("SaleQty").Width = 100
            GV1.Columns("SaleQty").HeaderText = "Sale Qty"


            Dim intType As Integer
            If (ddlType.Text = "Both") Then
                GV1.Columns("TotalQty").IsVisible = True
                GV1.Columns("TotalQty").Width = 80
                GV1.Columns("TotalQty").HeaderText = "Total Qty"

                GV1.Columns("TotalAmt").IsVisible = True
                GV1.Columns("TotalAmt").Width = 80
                GV1.Columns("TotalAmt").HeaderText = "Total Amount"
                If rdbGross.Checked = True And rdbFoc.IsChecked Then
                    GV1.Columns("DiscQty").IsVisible = True
                    GV1.Columns("DiscQty").Width = 100
                    GV1.Columns("DiscQty").HeaderText = "Disc Qty"

                    GV1.Columns("DiscAmt").IsVisible = True
                    GV1.Columns("DiscAmt").Width = 100
                    GV1.Columns("DiscAmt").HeaderText = "Disc Amount"

                    GV1.Columns("TradeQty").IsVisible = True
                    GV1.Columns("TradeQty").Width = 100
                    GV1.Columns("TradeQty").HeaderText = "Trade Qty"

                    GV1.Columns("TargetQty").IsVisible = True
                    GV1.Columns("TargetQty").Width = 100
                    GV1.Columns("TargetQty").HeaderText = "Target Qty"

                    GV1.Columns("TradeAmt").IsVisible = True
                    GV1.Columns("TradeAmt").Width = 100
                    GV1.Columns("TradeAmt").HeaderText = "Trade Amount"

                    GV1.Columns("TargetAmt").IsVisible = True
                    GV1.Columns("TargetAmt").Width = 100
                    GV1.Columns("TargetAmt").HeaderText = "Target Amount"


                    intType = 18
                Else
                    intType = 12
                End If

            ElseIf (ddlType.Text = "Value") Then
                GV1.Columns("TotalAmt").IsVisible = True
                GV1.Columns("TotalAmt").Width = 80
                GV1.Columns("TotalAmt").HeaderText = "Total Amount"
                If rdbGross.Checked = True And rdbFoc.IsChecked Then
                    GV1.Columns("DiscAmt").IsVisible = True
                    GV1.Columns("DiscAmt").Width = 100
                    GV1.Columns("DiscAmt").HeaderText = "Disc Amount"

                    GV1.Columns("TradeAmt").IsVisible = True
                    GV1.Columns("TradeAmt").Width = 100
                    GV1.Columns("TradeAmt").HeaderText = "Trade Amount"

                    GV1.Columns("TargetAmt").IsVisible = True
                    GV1.Columns("TargetAmt").Width = 100
                    GV1.Columns("TargetAmt").HeaderText = "Target Amount"

                    intType = 14
                Else
                    intType = 11
                End If
            ElseIf (ddlType.Text = "Quantity") Then
                GV1.Columns("TotalQty").IsVisible = True
                GV1.Columns("TotalQty").Width = 80
                GV1.Columns("TotalQty").HeaderText = "Total Qty"

                If rdbGross.Checked = True And rdbFoc.IsChecked Then
                    GV1.Columns("DiscQty").IsVisible = True
                    GV1.Columns("DiscQty").Width = 100
                    GV1.Columns("DiscQty").HeaderText = "Disc Qty"

                    GV1.Columns("TargetQty").IsVisible = True
                    GV1.Columns("TargetQty").Width = 100
                    GV1.Columns("TargetQty").HeaderText = "Target Qty"

                    GV1.Columns("TradeQty").IsVisible = True
                    GV1.Columns("TradeQty").Width = 100
                    GV1.Columns("TradeQty").HeaderText = "Trade Qty"

                    intType = 14
                Else
                    intType = 11
                End If
            End If
            For ii As Integer = intType To GV1.Columns.Count - 1
                strItemCode = GV1.Columns(ii).FieldName
                GV1.Columns("" & strItemCode & "").IsVisible = True
                GV1.Columns("" & strItemCode & "").Width = 80
                GV1.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
            Next


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            If (ddlType.Text = "Both") Then
                Dim item1 As New GridViewSummaryItem("TotalQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("TotalAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("SaleQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                If rdbGross.Checked = True And rdbFoc.IsChecked Then
                    Dim item4 As New GridViewSummaryItem("DiscQty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item4)
                    Dim item5 As New GridViewSummaryItem("DiscAmt", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item5)
                    Dim item6 As New GridViewSummaryItem("TargetQty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item6)
                    Dim item7 As New GridViewSummaryItem("TradeQty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item7)
                    Dim item8 As New GridViewSummaryItem("TargetAmt", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item8)
                    Dim item9 As New GridViewSummaryItem("TradeAmt", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item9)
                End If
            ElseIf ddlType.Text = "Value" Then
                Dim item2 As New GridViewSummaryItem("TotalAmt", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("SaleQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                If rdbGross.Checked = True And rdbFoc.IsChecked Then
                    Dim item1 As New GridViewSummaryItem("DiscAmt", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                    Dim item4 As New GridViewSummaryItem("TargetAmt", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item4)
                    Dim item5 As New GridViewSummaryItem("TradeAmt", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item5)
                End If
            ElseIf ddlType.Text = "Quantity" Then
                Dim item1 As New GridViewSummaryItem("TotalQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item3 As New GridViewSummaryItem("SaleQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                If rdbGross.Checked = True And rdbFoc.IsChecked Then
                    Dim item2 As New GridViewSummaryItem("DiscQty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item2)
                    Dim item4 As New GridViewSummaryItem("TargetQty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item4)
                    Dim item5 As New GridViewSummaryItem("TradeQty", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item5)
                End If

            End If
            For ii As Integer = intType To GV1.Columns.Count - 1
                intCount = intCount + 1
                strItemCode = GV1.Columns(ii).FieldName
                Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item16)
            Next
            GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        ElseIf rdbSalewithTargetQty.IsChecked Then


            GV1.Columns("company").IsVisible = True
            GV1.Columns("company").Width = 50
            GV1.Columns("company").HeaderText = "company"


            GV1.Columns("Location").IsVisible = True
            GV1.Columns("Location").Width = 70
            GV1.Columns("Location").HeaderText = "Location"

            GV1.Columns("Location_Desc").IsVisible = True
            GV1.Columns("Location_Desc").Width = 100
            GV1.Columns("Location_Desc").HeaderText = "Loc Desc"

            GV1.Columns("Cust_Code").IsVisible = True
            GV1.Columns("Cust_Code").Width = 100
            GV1.Columns("Cust_Code").HeaderText = "Customer"

            GV1.Columns("Customer_Name").IsVisible = True
            GV1.Columns("Customer_Name").Width = 100
            GV1.Columns("Customer_Name").HeaderText = "Customer Name"

            GV1.Columns("Route_No").IsVisible = True
            GV1.Columns("Route_No").Width = 100
            GV1.Columns("Route_No").HeaderText = "Route"

            GV1.Columns("Route_Desc").IsVisible = True
            GV1.Columns("Route_Desc").Width = 100
            GV1.Columns("Route_Desc").HeaderText = "Route Desc"

            GV1.Columns("Sale_Invoice_No").IsVisible = True
            GV1.Columns("Sale_Invoice_No").Width = 100
            GV1.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

            GV1.Columns("Sale_Invoice_Date").IsVisible = True
            GV1.Columns("Sale_Invoice_Date").Width = 100
            GV1.Columns("Sale_Invoice_Date").HeaderText = "Sale Invoice Date"


            Dim intType As Integer
            intType = 9
            For ii As Integer = intType To GV1.Columns.Count - 1
                strItemCode = GV1.Columns(ii).FieldName
                GV1.Columns("" & strItemCode & "").IsVisible = True
                GV1.Columns("" & strItemCode & "").Width = 80
                GV1.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""

            Next


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            For ii As Integer = intType To GV1.Columns.Count - 1
                intCount = intCount + 1
                strItemCode = GV1.Columns(ii).FieldName
                Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item16)
            Next
            GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


        ElseIf rdbDetail.IsChecked Then
            'GV1.Columns("Comp_Code").IsVisible = True
            'GV1.Columns("Comp_Code").Width = 50
            'GV1.Columns("Comp_Code").HeaderText = "company"
            'GV1.Columns("Comp_Code").PinPosition = PinnedColumnPosition.Left

            'GV1.Columns("Comp_Name").IsVisible = False
            'GV1.Columns("Comp_Name").Width = 100
            'GV1.Columns("Comp_Name").HeaderText = "Company Name"

            GV1.Columns("Location").IsVisible = True
            GV1.Columns("Location").Width = 70
            GV1.Columns("Location").HeaderText = "Location"

            GV1.Columns("Location_Desc").IsVisible = True
            GV1.Columns("Location_Desc").Width = 100
            GV1.Columns("Location_Desc").HeaderText = "Loc Desc"

            GV1.Columns("Cust_Group_Code").IsVisible = True
            GV1.Columns("Cust_Group_Code").Width = 100
            GV1.Columns("Cust_Group_Code").HeaderText = "Customer Group Code"

            GV1.Columns("Cust_Group_Desc").IsVisible = True
            GV1.Columns("Cust_Group_Desc").Width = 100
            GV1.Columns("Cust_Group_Desc").HeaderText = "Customer Group Desc"

            GV1.Columns("Cust_Type_Code").IsVisible = True
            GV1.Columns("Cust_Type_Code").Width = 100
            GV1.Columns("Cust_Type_Code").HeaderText = "Customer Type Code"

            GV1.Columns("Cust_Type_Desc").IsVisible = True
            GV1.Columns("Cust_Type_Desc").Width = 100
            GV1.Columns("Cust_Type_Desc").HeaderText = "Customer Type Desc"

            GV1.Columns("Cust_Code").IsVisible = True
            GV1.Columns("Cust_Code").Width = 100
            GV1.Columns("Cust_Code").HeaderText = "Customer"

            GV1.Columns("Customer_Name").IsVisible = True
            GV1.Columns("Customer_Name").Width = 100
            GV1.Columns("Customer_Name").HeaderText = "Customer Name"

            GV1.Columns("Sale_Invoice_No").IsVisible = True
            GV1.Columns("Sale_Invoice_No").Width = 100
            GV1.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

            GV1.Columns("Invoicedate").IsVisible = True
            GV1.Columns("Invoicedate").Width = 100
            GV1.Columns("Invoicedate").HeaderText = "Sale Invoice Date"

            GV1.Columns("Route_No").IsVisible = True
            GV1.Columns("Route_No").Width = 100
            GV1.Columns("Route_No").HeaderText = "Route"

            GV1.Columns("Route_Desc").IsVisible = True
            GV1.Columns("Route_Desc").Width = 100
            GV1.Columns("Route_Desc").HeaderText = "Route Desc"

            GV1.Columns("Salesname").IsVisible = True
            GV1.Columns("Salesname").Width = 100
            GV1.Columns("Salesname").HeaderText = "Salesman"

            GV1.Columns("FOCItem").IsVisible = True
            GV1.Columns("FOCItem").Width = 100
            GV1.Columns("FOCItem").HeaderText = "Item Code"

            GV1.Columns("FOCUnit").IsVisible = True
            GV1.Columns("FOCUnit").Width = 40
            GV1.Columns("FOCUnit").HeaderText = "FOC Unit"

            GV1.Columns("FOCQty").IsVisible = True
            GV1.Columns("FOCQty").Width = 100
            GV1.Columns("FOCQty").HeaderText = "FOC Qty"

            GV1.Columns("mainItem").IsVisible = True
            GV1.Columns("mainItem").Width = 100
            GV1.Columns("mainItem").HeaderText = "Main Item"

            GV1.Columns("mainUnit").IsVisible = True
            GV1.Columns("mainUnit").Width = 40
            GV1.Columns("mainUnit").HeaderText = "Main Unit"

            GV1.Columns("mainqty").IsVisible = True
            GV1.Columns("mainqty").Width = 100
            GV1.Columns("mainqty").HeaderText = "Main Item Qty"

            GV1.Columns("Ret_price").IsVisible = True
            GV1.Columns("Ret_price").Width = 100
            GV1.Columns("Ret_price").HeaderText = "Trade Price"

            GV1.Columns("DP").IsVisible = True
            GV1.Columns("DP").Width = 100
            GV1.Columns("DP").HeaderText = "Distributor Price"

            GV1.Columns("Discamt").IsVisible = True
            GV1.Columns("Discamt").Width = 100
            GV1.Columns("Discamt").HeaderText = "Disc Amount"

            GV1.Columns("Scheme_Desc").IsVisible = True
            GV1.Columns("Scheme_Desc").Width = 100
            GV1.Columns("Scheme_Desc").HeaderText = "Scheme Desc"

            GV1.Columns("FOCConvQty").IsVisible = True
            GV1.Columns("FOCConvQty").Width = 100
            GV1.Columns("FOCConvQty").HeaderText = "Converted Disc Qty"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            Dim item1 As New GridViewSummaryItem("FOCQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("mainqty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Discamt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FOCConvQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            'Dim item5 As New GridViewSummaryItem("DiscAmount", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item5)

            GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        End If

        If rdbRoute.Checked Then
            GV1.GroupDescriptors.Add(New GridGroupByExpression("Route_No as Route format ""{0}: {1}"" Group By Route_No"))
            GV1.GroupDescriptors.Add(New GridGroupByExpression("Route_Desc as Route format ""{0}: {1}"" Group By Route_Desc"))
            GV1.ShowGroupPanel = False
            GV1.MasterTemplate.AutoExpandGroups = True
        Else

        End If

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo, ByVal blnEffect As Boolean)
        Try


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpstart.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpend.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)

            If chkGroupSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgCustGroup.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Customer Group : " + strtemp)
            End If
            If chkClassSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In chkCustomerClass.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Customer Type : " + strtemp)
            End If

            If chklocSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbglocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location Segment : " + strtemp)
            End If



            '  clsCommon.MyExportToExcel("Trade Discount Report " + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, Me.Text)
            If exporter = EnumExportTo.Excel Then
                If rdbRoute.Checked Then
                    clsCommon.MyExportToExcelGrid("Trade Discount Report " + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, Me.Text)
                Else
                    If blnEffect = True Then
                        clsCommon.MyExportToExcelGrid("Trade Discount Report " + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, Me.Text, True)
                    Else
                        clsCommon.MyExportToExcel("Trade Discount Report " + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, Me.Text)

                    End If
                End If

            Else
                clsCommon.MyExportToPDF("Trade Discount Report " + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, "Trade Discount Report", True)
            End If

            ' '' ''Dim strReportTitle As String
            ' '' ''strReportTitle = "Trade Discount Invoice"
            ' '' ''Dim saveDialog1 As New SaveFileDialog()
            ' '' ''saveDialog1.FileName = strReportTitle
            ' '' ''saveDialog1.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"

            ' '' ''Dim Fullpath As String

            ' '' ''Dim path = "C:\\ERPTempFolder"
            ' '' ''Dim IsExists As Boolean = System.IO.Directory.Exists(path)
            ' '' ''If IsExists = False Then
            ' '' ''    System.IO.Directory.CreateDirectory(path)
            ' '' ''End If

            ' '' ''Fullpath = path + "\" + saveDialog1.FileName
            ' '' ''Dim i As Integer = 0
            '' '' ''For i = 0 To GV1.ColumnCount - 1
            '' '' ''    Dim grow As GridViewRowInfo = TryCast(GV1.Rows(0), GridViewRowInfo)
            '' '' ''    If TypeOf grow.Cells(i).Value Is DateTime Then
            '' '' ''        Dim datecol As GridViewDateTimeColumn = TryCast(GV1.Columns(i), GridViewDateTimeColumn)
            '' '' ''        datecol.ExcelExportType = DisplayFormatType.ShortDate
            '' '' ''    End If
            '' '' ''Next i
            ' '' ''Dim exporter As New ExportToExcelML(GV1)
            ' '' ''exporter.SummariesExportOption = SummariesOption.ExportAll
            ' '' ''If rdbSummary.IsChecked = True Then
            ' '' ''    exporter.ExportVisualSettings = True
            ' '' ''End If
            ' '' ''exporter.ExportHierarchy = True
            ' '' ''exporter.HiddenColumnOption = HiddenOption.DoNotExport
            ' '' ''exporter.SheetMaxRows = ExcelMaxRows._1048576
            ' '' ''AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
            ' '' ''AddHandler exporter.ExcelTableCreated, AddressOf exporter_ExcelTableCreated
            ' '' ''exporter.SheetName = strReportTitle
            ' '' ''exporter.RunExport(Fullpath)
            ' '' ''Me.Controls.Remove(GV1)
            ' '' ''Dim xlsApp As Microsoft.Office.Interop.Excel.ApplicationClass
            ' '' ''Dim xlsWB As Microsoft.Office.Interop.Excel.WorkbookClass
            ' '' ''xlsApp = New Microsoft.Office.Interop.Excel.ApplicationClass
            ' '' ''xlsApp.Visible = True
            ' '' ''xlsWB = xlsApp.Workbooks.Open(Fullpath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RunExportToExcelML(ByVal fileName As Object)
        Try
            'Dim exporter As New ExportToExcelML(GV1)
            'exporter.SummariesExportOption = SummariesOption.ExportAll
            ''If rdbSummary.IsChecked = True Then
            ''    exporter.ExportVisualSetting = True
            ''End If
            'exporter.ExportHierarchy = True
            'exporter.HiddenColumnOption = HiddenOption.DoNotExport
            'exporter.SheetMaxRows = ExcelMaxRows._1048576
            'AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
            'AddHandler exporter.ExcelTableCreated, AddressOf exporter_ExcelTableCreated
            'exporter.RunExport(fileName.ToString())
            'Dim text As String = "Export finished successfully!"
            'Dim xlApp As Excel.Application
            'xlApp = New Excel.ApplicationClass
            'Process.Start(fileName.ToString())

            'common.clsCommon.MyMessageBoxShow(text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    '''' <summary>        
    '''' '''' using ExcelCellFormatting event for updating progress bar and applying custom format in excel file        
    '''' '''' </summary>        

    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewDataRowInfo) Then
        End If
    End Sub

    '''' <summary>        
    '''' '''' using ExcelTableCreated event for adding custom header row        
    '''' '''' </summary>        

    Private Sub exporter_ExcelTableCreated(ByVal sender As Object, ByVal e As ExcelTableCreatedEventArgs)
        'Dim strReportTitle, strConverted, strOrderby, head2, strSummary, strQty As String
        Dim strReportTitle As String = ""
        If rdbSku.IsChecked = True Then
            strReportTitle = "Sku wise"
        ElseIf rdbPack.IsChecked = True Then
            strReportTitle = "pack wise"
        ElseIf rdbFlavour.IsChecked = True Then
            strReportTitle = "Flavour wise"

        End If

        If e.SheetIndex = 0 Then 'add header row only for the first excel sheet                

            Dim style1 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Trade Discount Report : " + strReportTitle)
            Dim style3 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Start Date : = " + clsCommon.GetPrintDate(dtpstart.Value, "dd/MM/yyyy"))
            Dim style4 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "End Date : = " + clsCommon.GetPrintDate(dtpend.Value, "dd/MM/yyyy"))

            If chklocSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each Str As String In cbglocation.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
                Dim style6 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Location : " + strLoca)
                Dim style7 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "")

            End If

        End If
    End Sub
    Private Sub chkItemAll1_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgItem1.Enabled = Not chkItemAll1.IsChecked
    End Sub


    Private Sub chkGroupAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgCustGroup.Enabled = Not chkGroupAll.IsChecked
    End Sub



    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        'If GV1.Rows.Count > 0 Then
        '    ExportToExcel()
        'Else
        '    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        'End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        blnRefresh = True
        print()
    End Sub


    Private Sub chklocAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocAll.ToggleStateChanged
        cbglocation.Enabled = Not chklocAll.IsChecked
    End Sub

    Private Sub chkCustAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked

    End Sub

    Private Sub chkClassAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkClassAll.ToggleStateChanged
        chkCustomerClass.Enabled = Not chkClassAll.IsChecked

    End Sub

    Private Sub chkGroupAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkGroupAll.ToggleStateChanged
        cbgCustGroup.Enabled = Not chkGroupAll.IsChecked
    End Sub

    Private Sub rbtnCompanyAll_ToggleStateChanged1(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked

    End Sub

    Private Sub FrmSaleAccountBreakOrCashDisc_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LocationChanged

    End Sub

    Private Sub Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Item.Click

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If GV1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel, False)
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        If GV1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF, False)
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub


    Private Sub GV1_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles GV1.CellFormatting
        Try
            If TypeOf e.CellElement.RowInfo Is GridViewDataRowInfo Then
                If e.Column.Name.Contains("_TradeAmt") OrElse e.Column.Name.Contains("TotalAmt") Then
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = GradientStyles.Solid
                    e.CellElement.ForeColor = Color.Black
                    e.CellElement.BackColor = Color.LightGreen
                ElseIf e.Column.Name.Contains("_TradeQty") OrElse e.Column.Name.Contains("TotalQty") Then
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = GradientStyles.Solid
                    e.CellElement.ForeColor = Color.Black
                    e.CellElement.BackColor = Color.Yellow
                ElseIf e.Column.Name.Contains("_Qty") OrElse e.Column.Name.Contains("TotalSaleQty") Then
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = GradientStyles.Solid
                    e.CellElement.ForeColor = Color.Black
                    e.CellElement.BackColor = Color.Violet
                Else
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = GradientStyles.Solid
                    e.CellElement.ForeColor = Color.Black
                    e.CellElement.BackColor = Color.White
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub rdbExcelVisalEffect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbExcelVisalEffect.Click
        blnVisualEffect = True
        If GV1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel, True)
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub
End Class
