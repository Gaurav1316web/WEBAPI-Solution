''''              Modified by = Priti (28/11/2012) 11:33 AM
''''              Modified by = Priti (13/12/2012) 09:30 AM
''''              Modified by = Priti (17/12/2012) 12:30 PM
''''              Modified by = Priti (20/12/2012) 1:30 PM
''''              Modified by = Priti (20/12/2012) 4:00 PM
'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------
'by vipin for pdf work on 07/02/2013


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
Public Class frmDailySettlement
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    Dim dr As SqlDataReader
    Public strReportType As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strOpening, strFinalOpening, strEmp As String



    Public Sub New(ByVal user As String, ByVal company As String, Optional ByVal ReportType As String = Nothing)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        'dr = connectSql.RunSqlReturnDR(sql)
        'dr.Read()


        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()
                strReportType = ReportType
            Next
        End If
    End Sub

    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        '' Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Sub LoadCustomerClass()
        Dim qry As String = "select Cust_Type_Code as [Code],Cust_Type_Desc as [Name] from TSPL_CUSTOMER_type_master"
        chkCustomerClass.DataSource = clsDBFuncationality.GetDataTable(qry)
        chkCustomerClass.ValueMember = "Code"
        chkCustomerClass.DisplayMember = "Name"
    End Sub
    Sub LoadRoute()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Route_No as [Route],Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub





    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.DailySettlement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmDailySettlement_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()

        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Sub LoadSalesPerson()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "Select EMP_CODE as [SalesPerson Code],Emp_Name as [SalesPerson Name] from TSPL_EMPLOYEE_MASTER"
        cbgSalesPerson.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSalesPerson.ValueMember = "SalesPerson Code"
        cbgSalesPerson.DisplayMember = "SalesPerson Name"
    End Sub
    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkClassAll.ToggleStateChanged
        chkCustomerClass.Enabled = Not chkClassAll.IsChecked
    End Sub
    Private Sub frmDailySettlement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rdbBoth.IsChecked = True
        SetUserMgmtNew()
        LoadLocation()
        chkLocatioAll.IsChecked = True
        LoadRoute()
        LoadCustomerClass()
        chkClassAll.IsChecked = True
        LoadSalesPerson()
        ChkSalesAll.IsChecked = True
        chkRouteAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        LoadCompany()
        'rbtnCompanyAll.IsChecked = True
        rbtnCompanySelect.IsChecked = True
        Dim arrDBName As New ArrayList()
        arrDBName.Add(objCommonVar.CurrDatabase)
        cbgCompany.CheckedValue = arrDBName


        rdbSalesmanSummary.IsChecked = True
        'If strReportType = "Provisional" Then
        '    rdbDetail.Visible = False
        '    rdbSummary.IsChecked = True
        'Else
        '    rdbDetail.Visible = True
        '    rdbSummary.IsChecked = False
        'End If
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


    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "DAILYSET-RPT"
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


    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        LoadLocation()
        chkLocatioAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        rdbSummary.IsChecked = True
        LoadSalesPerson()
        ChkSalesAll.IsChecked = True
    End Sub
    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged, chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub
    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub

    Private Sub chkLocatioAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocatioAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocatioAll.IsChecked
    End Sub
    Sub PrintActualReport()
        Dim strLocAll, strRouteAll, strSalesAll As String
        If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one route Category or select ALL ")
            Return
        ElseIf chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one location or select ALL ")
            Return
        ElseIf chlSalesSelect.IsChecked = True AndAlso cbgSalesPerson.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Salesperson or select ALL ")
            Return
        ElseIf rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one company or select ALL ")
            Return

        End If
        If chkLocatioAll.IsChecked = True Then
            strLocAll = "Y"
        Else
            strLocAll = "N"
        End If
        If chkRouteAll.IsChecked = True Then
            strRouteAll = "Y"
        Else
            strRouteAll = "N"
        End If
        If ChkSalesAll.IsChecked = True Then
            strSalesAll = "Y"
        Else
            strSalesAll = "N"
        End If
        '' '' If rdbSummary.IsChecked = True Then
        '' ''     Dim strSql As String = "SELECT " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Docno,Sale_Invoice_Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type, " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
        '' ''     "(SELECT  ISNULL(SUM(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount), 0) AS Expr1 FROM  " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL INNER JOIN " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No " & _
        '' ''     " inner join " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE on " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code=" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
        '' ''     "WHERE (" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND (" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque') and TSPL_RECEIPT_HEADER.Posted='Y' and IsChkReverse='N') + " & _
        '' '' " (SELECT isnull(Adjustment_Amount,0) FROM TSPL_Receipt_Adjustment_Header WHERE Doc_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and  Is_Post='Y')  AS OPCashAmt, " & _
        '' ''     "(SELECT ISNULL(SUM(TSPL_RECEIPT_DETAIL_1.Applied_Amount), 0) AS Expr1 FROM " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL AS TSPL_RECEIPT_DETAIL_1 INNER JOIN " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER AS TSPL_RECEIPT_HEADER_1 ON TSPL_RECEIPT_DETAIL_1.Receipt_No = TSPL_RECEIPT_HEADER_1.Receipt_No " & _
        '' ''     " inner join " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE on TSPL_RECEIPT_HEADER_1.Payment_Code=" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
        '' ''     "WHERE (TSPL_RECEIPT_DETAIL_1.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND (" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque') and TSPL_RECEIPT_HEADER_1.Posted='Y' and IsChkReverse='N') " & _
        '' ''      " - ISNULL((SELECT  sum(TSPL_RECEIPT_DETAIL.Applied_Amount) FROM  " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL INNER JOIN " & _
        '' ''      "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_DETAIL.Receipt_No = TSPL_RECEIPT_HEADER.Receipt_No INNER JOIN " & _
        '' ''      "" + clsCommon.ReplicateDBString + "TSPL_BANK_REVERSE ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No " & _
        '' ''      "WHERE (TSPL_BANK_REVERSE.Source_Type = 'AR') AND (TSPL_RECEIPT_DETAIL.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)),0) AS OPCheckAmt, TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt  AS OPInvAmtwoEmty, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_head.Empty_Value) as OPEmptySalesValue, " & _
        '' ''     "(SELECT  ISNULL(SUM(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost), 0) AS Expr1 FROM  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No WHERE (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND " & _
        '' ''     "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = 'Sale Invoice') AND (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType = 'E') AND (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI') and TSPL_ADJUSTMENT_HEADER.Posted='Y') AS OPadjustEmpty, " & _
        '' ''     "0 as PECashAmt,0 as PECheckAmt,0 as PEInvAmtwoEmty,0 as PEEmptySalesValue,0 as PEadjustEmpty, '" & fromDate.Value & "' AS FDate, '" & ToDate.Value & "' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc,(Shell_Qty *100) as Shellamt,Credit_Invoice,(Balance_Amt) as OPBalamt,0 as PerbalAmt " & _
        '' ''     "FROM  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code  " & _
        '' ''     "where convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) < convert(date, '" & fromDate.Value & "',103) and  (select Invoice_No  from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD WHERE TSPL_SALE_RETURN_HEAD.Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) IS NULL  and Is_Post='Y' "
        '' ''     Dim un1 As String = " union all "
        '' ''     Dim strSql1 As String = "SELECT " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as Docno,Sale_Invoice_Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, 0 as OPCashAmt,0 as OPCheckAmt,0 as OPInvAmtwoEmty,0 as OPEmptySalesValue,0 as OPadjustEmpty, " & _
        '' ''     "(SELECT  ISNULL(SUM(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount), 0) AS Expr1 FROM  " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL INNER JOIN " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No " & _
        '' ''     " inner join " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE on " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code=" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
        '' ''     "WHERE (" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND  " & _
        '' ''     "(" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque') and TSPL_RECEIPT_HEADER.Posted='Y' and IsChkReverse='N') + " & _
        '' '' " (SELECT isnull(Adjustment_Amount,0) FROM TSPL_Receipt_Adjustment_Header WHERE Doc_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and  Is_Post='Y') AS PECashAmt,(SELECT ISNULL(SUM(TSPL_RECEIPT_DETAIL_1.Applied_Amount), 0) AS Expr1 " & _
        '' ''     "FROM " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL AS TSPL_RECEIPT_DETAIL_1 INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER AS TSPL_RECEIPT_HEADER_1 ON " & _
        '' ''     "TSPL_RECEIPT_DETAIL_1.Receipt_No = TSPL_RECEIPT_HEADER_1.Receipt_No  " & _
        '' ''     " inner join " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE on TSPL_RECEIPT_HEADER_1.Payment_Code=" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
        '' ''     "WHERE (TSPL_RECEIPT_DETAIL_1.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND (" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque') and TSPL_RECEIPT_HEADER_1.Posted='Y' and IsChkReverse='N') " & _
        '' ''      " - ISNULL((SELECT  sum(TSPL_RECEIPT_DETAIL.Applied_Amount) FROM  " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL INNER JOIN " & _
        '' ''    "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_DETAIL.Receipt_No = TSPL_RECEIPT_HEADER.Receipt_No INNER JOIN " & _
        '' ''    "" + clsCommon.ReplicateDBString + "TSPL_BANK_REVERSE ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No " & _
        '' ''    "WHERE (TSPL_BANK_REVERSE.Source_Type = 'AR') AND (TSPL_RECEIPT_DETAIL.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)),0) AS PECheckAmt, " & _
        '' ''     "TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt AS PEInvAmtwoEmty, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_Head.Empty_Value) as PEEmptySalesValue, " & _
        '' ''     "(SELECT  ISNULL(SUM(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost), 0) AS Expr1 FROM  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No WHERE " & _
        '' ''     "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = 'Sale Invoice') AND " & _
        '' ''     "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType = 'E') AND (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI') and TSPL_ADJUSTMENT_HEADER.Posted='Y') AS PEadjustEmpty, '" & fromDate.Value & "' AS FDate, '" & ToDate.Value & "' AS TDate, " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc,(Shell_Qty *100) as Shellamt,Credit_Invoice,0 as OPBalamt,(Balance_Amt) as PerbalAmt FROM  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code where " & _
        '' ''     "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND " & _
        '' ''     "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and Is_Post='Y' and  (select Invoice_No  from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD WHERE TSPL_SALE_RETURN_HEAD.Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) IS NULL "

        '' ''     If strLocAll = "N" Then
        '' ''         strSql += " and TSPL_SALE_INVOICE_DETAIL.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        '' ''         strSql1 += " and TSPL_SALE_INVOICE_DETAIL.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "

        '' ''     End If
        '' ''     If strRouteAll = "N" Then
        '' ''         strSql += " and TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        '' ''         strSql1 += " and TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "

        '' ''     End If
        '' ''     If strSalesAll = "N" Then
        '' ''         strSql += " and TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        '' ''         strSql1 += " and TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        '' ''     End If

        '' ''     strSql += " group by " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, " & _
        '' ''     "Sale_Invoice_Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name,Location_Desc,TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt,TSPL_SALE_INVOICE_HEAD.Empty_Value,Shell_Qty,Credit_Invoice,Balance_Amt"

        '' ''     strSql1 += "   group by " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, " & _
        '' ''     "Sale_Invoice_Date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " & _
        '' ''     "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name,Location_Desc,TSPL_SALE_INVOICE_HEAD.Empty_Value,TSPL_SALE_INVOICE_HEAD.Total_Invoice_Amt,Shell_Qty,Credit_Invoice,Balance_Amt"
        '' ''     strQuery = strSql & un1 & strSql1
        '' '' Else
        '' ''     strQuery = "SELECT  " + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Inv_Detail_Total_Amt, " & _
        '' '' "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
        '' '' "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'sale' THEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ELSE " & _
        '' '' "(SELECT Transfer_No FROM " + clsCommon.ReplicateDBString + "tspl_shipment_master WHERE " + clsCommon.ReplicateDBString + "tspl_shipment_master.shipment_no = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.shipment_no) END AS DocNo, " & _
        '' '' "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName,CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'Sale' THEN (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value)  Else " & _
        '' '' "(SELECT sum(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty * (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TPT_Value + " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Empty_Value)) " & _
        '' '' "FROM " + clsCommon.ReplicateDBString + "tspl_shipment_master, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL WHERE " + clsCommon.ReplicateDBString + "tspl_shipment_master.shipment_no = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.shipment_no AND " & _
        '' '' "" + clsCommon.ReplicateDBString + "TSPL_SHIPMENT_MASTER.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No) END AS NetLoad, " & _
        '' '' "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN " & _
        '' '' "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value)  WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') " & _
        '' '' "THEN (TSPL_SALE_INVOICE_DETAIL.Empty_Value) ELSE 0 END AS NetSale, " & _
        '' '' "CASE WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') " & _
        '' '' "THEN ((Basic_Rate * Invoice_Qty))  " & _
        '' '' "WHEN (Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N') THEN (Invoice_Qty * (Cust_Discount + Disc_Amt)) ELSE 0 END AS Discount, " & _
        '' '' "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice = 'Y' THEN (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value) " & _
        '' '' "ELSE 0 END AS CreditSale,(SELECT  ISNULL(SUM(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount), 0) AS Expr1 FROM  " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL INNER JOIN " & _
        '' '' "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No " & _
        '' '' " inner join " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE on " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code=" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
        '' '' " WHERE (" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND (" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque') and TSPL_RECEIPT_HEADER.Posted='Y' and IsChkReverse='N') + " & _
        '' '' " ISNULL((SELECT isnull(Adjustment_Amount,0) FROM TSPL_Receipt_Adjustment_Header WHERE Doc_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No and  Is_Post='Y'),0) AS CashAmt, " & _
        '' '' "(SELECT  ISNULL(SUM(TSPL_RECEIPT_DETAIL_1.Applied_Amount), 0) AS Expr1 FROM " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL AS TSPL_RECEIPT_DETAIL_1 INNER JOIN " & _
        '' '' " " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER AS TSPL_RECEIPT_HEADER_1 ON TSPL_RECEIPT_DETAIL_1.Receipt_No = TSPL_RECEIPT_HEADER_1.Receipt_No " & _
        '' '' " inner join " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE on TSPL_RECEIPT_HEADER_1.Payment_Code=" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
        '' '' "WHERE (TSPL_RECEIPT_DETAIL_1.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND (" + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque') and TSPL_RECEIPT_HEADER_1.Posted='Y' and IsChkReverse='N') " & _
        '' ''  " - ISNULL((SELECT  sum(TSPL_RECEIPT_DETAIL.Applied_Amount) FROM  " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL INNER JOIN " & _
        '' '' "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_DETAIL.Receipt_No = TSPL_RECEIPT_HEADER.Receipt_No INNER JOIN " & _
        '' ''"" + clsCommon.ReplicateDBString + "TSPL_BANK_REVERSE ON TSPL_RECEIPT_HEADER.Receipt_No = TSPL_BANK_REVERSE.Document_No " & _
        '' '' "WHERE (TSPL_BANK_REVERSE.Source_Type = 'AR') AND (TSPL_RECEIPT_DETAIL.Document_No = TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No)),0) AS CheckAmt, " & _
        '' '' "case WHEN (Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N') THEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt else 0 end AS InvAmtwoEmty, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value, " & _
        '' '' "(SELECT  ISNULL(SUM(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost), 0) AS Expr1 FROM " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER INNER JOIN " & _
        '' '' "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No " & _
        '' '' "WHERE (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AND " & _
        '' '' "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = 'Sale Invoice') AND (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType = 'E') AND " & _
        '' '' "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI' ) and TSPL_ADJUSTMENT_HEADER.Posted='Y') AS adjustEmpty, '" & fromDate.Value & "' AS FDate, '" & ToDate.Value & "' AS TDate, TSPL_LOCATION_MASTER.Location_Desc,(Shell_Qty *100) as Shellamt, " & _
        '' '' "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice,Balance_Amt " & _
        '' '' "FROM  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " & _
        '' ''               " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
        '' ''               " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No LEFT OUTER JOIN " & _
        '' ''               " " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
        '' '' "where convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        '' '' "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and Is_Post='Y' and  (select Invoice_No  from " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD WHERE TSPL_SALE_RETURN_HEAD.Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) IS NULL  "

        '' ''     If strLocAll = "N" Then
        '' ''         strQuery += " and TSPL_SALE_INVOICE_DETAIL.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        '' ''     End If

        '' ''     If strRouteAll = "N" Then
        '' ''         strQuery += " and TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        '' ''     End If

        '' ''     If strSalesAll = "N" Then
        '' ''         strQuery += " and TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        '' ''     End If
        '' '' End If




        '' '' Dim ArrDBName As ArrayList = Nothing
        '' '' Dim dt As New DataTable

        '' '' If rbtnCompanyAll.IsChecked Then
        '' ''     ArrDBName = cbgCompany.AllValue
        '' '' Else
        '' ''     ArrDBName = cbgCompany.CheckedValue
        '' '' End If
        '' '' strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQuery, ArrDBName, False)
        '' '' dt = clsDBFuncationality.GetDataTable(strQuery)



        '' '' If rdbSummary.IsChecked = True Then
        '' ''     Dim frmDailySettlementSummary As New FrmSalerReport()
        '' ''     frmDailySettlementSummary.funreport(strQuery, "crptDailySettlementSummary", "Daily Settlement Report Summary")
        '' '' Else
        '' ''     Dim frmDailySettlementDetail As New FrmSalerReport()
        '' ''     frmDailySettlementDetail.funreport(strQuery, "crptDailySettlementDetail", "Daily Settlement Report Detail")
        '' '' End If

        Dim strQuery, strMain, strRoute, strNonRoute, strMainopening, strRouteOpening, strNonRouteOpening As String
        strQuery = ""
        strMain = "SELECT   Cust_Type_Code,DocNo,DocDate,PartyName, " & _
        " SalesName,Salesman_Code as PayrollCode," & _
        "case when TransType='Sale' then  sum(NetLoad) + SUM(Discount) + MAX(PeriodShellamt) - MAX(PeriodRetShell) else MAX(NetLoad) end   as NetLoad, " & _
        "case when TransType='Sale' then sum(PeriodNetSale) + MAX(PeriodShellamt) - MAX(PeriodRetShell) else sum(PeriodNetSale)  end as NetSale," & _
        "convert(decimal(18,2),sum(Discount)) as Discount," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then case when sum(PeriodNetSale) <> 0 then " & _
        "SUM(CreditSale) + MAX(PeriodShellamt)  - MAX(PeriodRetShell) - sum(periodadjustEmpty) else SUM(CreditSale) end  else 0 end " & _
        "else SUM(creditSale)  end  as CreditSale," & _
        "SUM(PeriodSettlement) as Settlement, " & _
        "SUM(PeriodDEP) + SUM(PeriodRefund) as PeriodDepRefund," & _
        "SUM(OPDEP) + SUM(OPRefund) as OPDepRefund," & _
        "case when TransType='Sale' then max(periodCashAmt) else SUM(PeriodCashAmt) end as CashAmt," & _
        "case when TransType='Sale' then max(PeriodCheckAmt) else SUM(PeriodCheckAmt) end as CheckAmt," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then sum(periodadjustEmpty)  else  sum(periodadjustEmpty)  end else sum(periodadjustEmpty) end  as EmptyIn," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else " & _
        "   sum(OPNetSale)   - ( sum(OPCashAmt) + sum(OPCheckAmt) + sum(OPEmpty_Value) + sum(OPSettlement) + SUM(OPRefund) ) end else " & _
        "   case when sum(OPCashSortage) <> 0 then sum(OPCashSortage) else sum(OPNetSale) - SUM(OPDiscount) - SUM(OPcreditSale) - sum(OPCashAmt) - sum(OPCheckAmt) end end as OPcashShortage," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else " & _
        "   sum(periodNetSale) - ( sum(periodCashAmt) + sum(periodCheckAmt) + sum(periodEmpty_Value) + SUM(PeriodSettlement) + SUM(PeriodRefund)) end else " & _
        "  case when sum(PeriodCashSortage) <> 0 then sum(PeriodCashSortage) else sum(PeriodNetSale) - SUM(Discount) - SUM(creditSale) - sum(periodCashAmt) - sum(periodCheckAmt) end end as PercashShortage, " & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else " & _
        "   sum(OPEmpty_Value) + max(OPShellamt)  - MAX(OPRetShell) - (sum(OPadjustEmpty) + SUM(OPDEP) ) end else " & _
        "   Max(OPNetLoad) - sum(OPNetSale)- SUM(OPDEP) - SUM(OPRefund) - sum(OPadjustEmpty) end  as OPEmptyShortage," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else " & _
        "   sum(periodEmpty_Value) + max(periodShellamt) - MAX(PeriodRetShell) - (sum(periodadjustEmpty) + SUM(PeriodDEP) ) end else " & _
        "   Max(NetLoad) - sum(PeriodNetSale)- SUM(PeriodDEP) - SUM(PeriodRefund)- sum(periodadjustEmpty) end  as PerEmptyShortage," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else   max(OPBalance_Amt) end else sum(OPCashSortage) + sum(OPEmptyShortage) end  as OPBalance," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else   max(periodBalance_Amt) end else sum(PeriodCashSortage) + sum(PeriodEmptyShortage) end  as PerBalance," & _
        "Location,Location_Desc,max(FDate) as FDate,max(TDate) as TDate,max(Comments) as Comments from("

        strNonRoute= "SELECT  Cust_Type_Code,Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No AS DocNo, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName," & _
        "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value)  WHEN " & _
        "(Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value) ELSE 0 END AS PeriodNetSale,0 as OPNetSale," & _
        "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value)  WHEN " & _
        "(Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value) ELSE 0 END  AS NetLoad, " & _
        "CASE WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
                " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount4 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount8 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount9)) " & _
        "WHEN (Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N') THEN " & _
        "(Invoice_Qty * (Cust_Discount + Disc_Amt)) ELSE 0 END AS Discount, " & _
        "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice = 'Y' THEN " & _
        "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value)  WHEN " & _
        "(Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value) ELSE 0 END ELSE 0 END AS CreditSale," & _
        "0 AS PeriodCashAmt, " & _
        "0 AS OPCashAmt, " & _
        "0  AS PeriodCheckAmt, " & _
        "0  AS OPCheckAmt, " & _
        "case when Credit_Invoice='Y' then 0 else  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value end as PeriodEmpty_Value, " & _
        "0 as OPEmpty_Value, " & _
        "0 AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
        "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc,(Shell_Qty *100) as PeriodShellamt, " & _
        "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
        "case when Credit_Invoice='Y' then 0 else Balance_Amt end as PeriodBalance_Amt," & _
        "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage,0 as OPCashSortage, " & _
        "0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP,0 as OPDEP,0 as PeriodRefund,0 as OPRefund, " & _
        "0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
        "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
        "FROM " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code AND  " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code ON  " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE  " & _
        " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
        "where  " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' and Shipment_Type='sale' " & _
        "and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "

        If strLocAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        End If

        If strRouteAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If

        If strSalesAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If

        strNonRoute += "Union All "

        ' ''--for cash and cheque recceiving  Period wise

        strNonRoute += " SELECT   Cust_Type_Code,Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName, " + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No AS DocNo, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
        "0 AS PeriodNetSale,0 as OPNetSale, " & _
        "0 AS NetLoad, " & _
        "0 AS Discount, 0 AS CreditSale, " & _
        "case  when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque' then case when Credit_Invoice='Y' then 0 " & _
        "else isnull( " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount,0)  end  else 0 end AS PeriodCashAmt, " & _
        "0 AS OPCashAmt," & _
        "case  when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque' then case when Credit_Invoice='Y' then 0 " & _
        "else  isnull(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount,0)  end  else 0 end  AS PeriodCheckAmt, " & _
        "0  AS OPCheckAmt, " & _
        "0 as PeriodEmpty_Value, " & _
        "0 as OPEmpty_Value, " & _
        "0 AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
        "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
        "0 as PeriodShellamt, " & _
        "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
        "0 as PeriodBalance_Amt, " & _
        "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage, " & _
        "0 as PeriodEmptyShortage,0 as OPCashSortage,0 as OPEmptyShortage, " & _
        "'All' as ReportType, 0 as PeriodDEP,0 as OPDEP,0 as PeriodRefund, " & _
        "0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
        "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
        "FROM " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE ON " & _
        "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON  " & _
        "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No " & _
        " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
        "where IsChkReverse='N' and " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' and Shipment_Type='sale' " & _
        " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "

        If strLocAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        End If

        If strRouteAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If

        If strSalesAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If

        strNonRoute += "Union All "


        ' ''--for cash and cheque recceiving of Unappled Type entry  Period wise

        strNonRoute += " SELECT   Cust_Type_Code,Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName, " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.UnApplied_No AS DocNo, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
        "0 AS PeriodNetSale,0 as OPNetSale, " & _
        "0 AS NetLoad, " & _
        "0 AS Discount, 0 AS CreditSale, " & _
        "case  when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque' then case when Credit_Invoice='Y' then 0 " & _
        "else isnull( " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.UnApplied_Balance,0)  end  else 0 end AS PeriodCashAmt, " & _
        "0 AS OPCashAmt," & _
        "case  when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque' then case when Credit_Invoice='Y' then 0 " & _
        "else  isnull(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.UnApplied_Balance,0)  end  else 0 end  AS PeriodCheckAmt, " & _
        "0  AS OPCheckAmt, " & _
        "0 as PeriodEmpty_Value, " & _
        "0 as OPEmpty_Value, " & _
        "0 AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
        "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
        "0 as PeriodShellamt, " & _
        "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
        "0 as PeriodBalance_Amt, " & _
        "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage, " & _
        "0 as PeriodEmptyShortage,0 as OPCashSortage,0 as OPEmptyShortage, " & _
        "'All' as ReportType, 0 as PeriodDEP,0 as OPDEP,0 as PeriodRefund, " & _
        "0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
        "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
        "FROM " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE ON " & _
        "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON  " & _
        "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No " & _
        " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
        "where IsChkReverse='N' and " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' and Shipment_Type='sale' " & _
        " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') and UnApplied_No <> '' and TSPL_Receipt_Detail.Receipt_Line_No=1 "

        If strLocAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        End If

        If strRouteAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If

        If strSalesAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If

        strNonRoute += "Union All "

        ' ''--for cash and cheque recceiving salesman  entry without customer  Period wise

        strNonRoute += "SELECT '' AS Cust_Type_Code, convert(date,Receipt_Date,103) AS DocDate, " & _
        "Customer_Name AS PartyName, " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No AS DocNo, " & _
        "RIGHT(TSPL_BANK_MASTER.BANKACC, 3) AS Location, '' AS Route_No, " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Salesman_Code, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Salesman_Name AS SalesName, " & _
        "0 AS PeriodNetSale, 0 AS OPNetSale, 0 AS NetLoad, 0 AS Discount, 0 AS CreditSale, " & _
        "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque' THEN " & _
        "isnull(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_Amount, 0) ELSE 0 END AS PeriodCashAmt, " & _
        "0 AS OPCashAmt, " & _
        "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque' THEN " & _
        "isnull(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_Amount, 0) ELSE 0 END AS PeriodCheckAmt, " & _
        "0 AS OPCheckAmt, 0 AS PeriodEmpty_Value, 0 AS OPEmpty_Value, " & _
        "0 AS PeriodadjustEmpty, 0 AS OPadjustEmpty, " & _
        "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_GL_SEGMENT_CODE.Description as Location_Desc,  " & _
        "0 AS PeriodShellamt, 0 AS OPShellamt, '' AS Credit_Invoice, " & _
        "0 AS PeriodBalance_Amt, 0 AS OPBalance_Amt, 'Sale' AS TransType, " & _
        "0 AS PeriodCashSortage,0 AS PeriodEmptyShortage, 0 AS OPCashSortage,  " & _
        "0 AS OPEmptyShortage, 'All' AS ReportType, 0 AS PeriodDEP, " & _
        "0 AS OPDEP, 0 AS PeriodRefund,0 AS OPRefund, 0 AS PeriodSettlement, " & _
        "0 AS OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
        "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
        "FROM " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_GL_SEGMENT_CODE ON RIGHT(" + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER.BANKACC, 3) = " + clsCommon.ReplicateDBString + "TSPL_GL_SEGMENT_CODE.Segment_code ON " & _
        "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Bank_Code = " + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE ON " & _
        "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code = " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
        "WHERE (" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.IsChkReverse = 'N') and Receipt_Type='M'  and " & _
        " Salesman_Code <> '' and " & _
        "convert(date,Receipt_Date,103) >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
        "convert(date,Receipt_Date,103) <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' "

        If strLocAll = "N" Then
            strNonRoute += " and RIGHT(" + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER.BANKACC, 3) in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
        End If

        'If strRouteAll = "N" Then
        '    strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        'End If

        If strSalesAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If

        strNonRoute += "Union All "



        '--for adjustment empty  of sale invoice in Period wise

        strNonRoute += "SELECT    Cust_Type_Code,Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No AS DocNo, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
        "TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName," & _
        "0 AS PeriodNetSale,0 as OPNetSale, " & _
        "0 AS NetLoad, " & _
        "0 AS Discount, CASE WHEN Credit_Invoice = 'N' THEN 0 ELSE case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   END    AS CreditSale, " & _
        "0 AS PeriodCashAmt, " & _
        "0 AS OPCashAmt, " & _
        "0  AS PeriodCheckAmt, " & _
        "0  AS OPCheckAmt, " & _
        "0 as PeriodEmpty_Value, " & _
        "0 as OPEmpty_Value, " & _
        " CASE WHEN Credit_Invoice = 'Y' THEN 0 ELSE case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   END   AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
        "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, " & _
        "0 as PeriodShellamt, " & _
        "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
        "0 as PeriodBalance_Amt, " & _
        "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
        "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
        "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
        "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
        "FROM  " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Document_No LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No " & _
        " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
        "WHERE (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = 'Sale Invoice') and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType='E' " & _
        "and Adjustment_Type='BI' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' and Shipment_Type='sale' " & _
        " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "

        If strLocAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        End If

        If strRouteAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If

        If strSalesAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If

        strNonRoute += "Union All "

        '--for adjustment empty  of salesman without sale invoice and without customer in Period wise

        strNonRoute += "SELECT  '' as   Cust_Type_Code,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DocDate, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_NAME as PartyName, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No AS DocNo, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code as Location, " & _
        "'' as Route_No, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE as Salesman_Code," & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_NAME AS SalesName," & _
        "0 AS PeriodNetSale,0 as OPNetSale, " & _
        "0 AS NetLoad, " & _
        "0 AS Discount, 0 AS CreditSale, " & _
        "0 AS PeriodCashAmt, " & _
        "0 AS OPCashAmt, " & _
        "0  AS PeriodCheckAmt, " & _
        "0  AS OPCheckAmt, " & _
        "0 as PeriodEmpty_Value, " & _
        "0 as OPEmpty_Value, " & _
        " case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end  AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
        "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, " & _
        "0 as PeriodShellamt, " & _
        "0 as OPShellamt,'N' as Credit_Invoice, " & _
        "0 as PeriodBalance_Amt, " & _
        "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
        "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
        "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
        "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
        "FROM  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON  " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No " & _
        "WHERE  (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = '') and Customer_CODE='' AND " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType = 'E') AND  (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI') and EMP_CODE <> ''   " & _
        "and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'"


        If strLocAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        End If

        'If strRouteAll = "N" Then
        '    strOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        'End If

        If strSalesAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If


        strNonRoute += "Union All "

        '--for adjustment empty  of salesman without sale invoice and with customer in Period wise

        strNonRoute += "SELECT    Cust_Type_Code,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DocDate, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_NAME as PartyName, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No AS DocNo, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code as Location, " & _
        "'' as Route_No, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE as Salesman_Code," & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_NAME AS SalesName," & _
        "0 AS PeriodNetSale,0 as OPNetSale, " & _
        "0 AS NetLoad, " & _
        "0 AS Discount, case when Credit_Customer='Y' then case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   else 0 end AS CreditSale, " & _
        "0 AS PeriodCashAmt, " & _
        "0 AS OPCashAmt, " & _
        "0  AS PeriodCheckAmt, " & _
        "0  AS OPCheckAmt, " & _
        "0 as PeriodEmpty_Value, " & _
        "0 as OPEmpty_Value, " & _
        "case when Credit_Customer='N' then  case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end else 0 end   AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
        "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, " & _
        "0 as PeriodShellamt, " & _
        "0 as OPShellamt,case when Credit_Customer='Y' then 'Y' else 'N' end as Credit_Invoice, " & _
        "0 as PeriodBalance_Amt, " & _
        "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
        "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
        "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
        "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
        "FROM  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON  " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No " & _
        "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_CODE=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
        "WHERE  (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = '') and Customer_CODE <> '' AND " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType = 'E') AND  (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI') and EMP_CODE <> ''   " & _
        "and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' and  Cust_Type_Code not in ( 'F','S')"


        If strLocAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        End If

        'If strRouteAll = "N" Then
        '    strOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        'End If

        If strSalesAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If


        strNonRoute += "Union All "

        ''--for settlement Period wise

        strNonRoute += "SELECT    Cust_Type_Code,Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AS DocNo, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
        "0 AS PeriodNetSale, 0 AS OPNetSale, 0 AS NetLoad, " & _
        "0 AS Discount, 0 AS CreditSale, 0 AS PeriodCashAmt, " & _
        "0 AS OPCashAmt, 0 AS PeriodCheckAmt, 0 AS OPCheckAmt, " & _
        "0 AS PeriodEmpty_Value, 0 AS OPEmpty_Value,  " & _
        "0 AS PeriodadjustEmpty, 0 AS OPadjustEmpty, " & _
        "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, 0 AS PeriodShellamt, " & _
        "0 AS OPShellamt, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
        "0 AS PeriodBalance_Amt, 0 AS OPBalance_Amt, " & _
        "'Sale' AS TransType, 0 AS PeriodCashSortage, " & _
        "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, " & _
        "0 AS OPEmptyShortage, 'All' AS ReportType, " & _
        "case when SettleMent_Type='DEP' then isnull(Amount,0) else 0 end AS PeriodDEP, 0 AS OPDEP, " & _
        "case when SettleMent_Type='REF' then isnull(Amount,0) else 0 end AS PeriodRefund, 0 AS OPRefund,  " & _
        "CASE WHEN Credit_Invoice = 'Y' THEN 0 ELSE case when (SettleMent_Type <> 'DEP' and SettleMent_Type <> 'REF') then " & _
        "isnull(Amount,0) else 0 end end AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
        "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
        "FROM  " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master INNER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail ON " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode = " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail.Discount_Code RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header ON " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Adjustment_No RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Doc_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
        " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
        "WHERE  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' AND  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'sale') AND SettleMent_Type <> '' " & _
        " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "

        If strLocAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        End If

        If strRouteAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If

        If strSalesAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If

        strNonRoute += "Union All "

        '''''  Sale return for period wise

        strNonRoute += "SELECT  Cust_Type_Code,Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No AS DocNo, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
        "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) " & _
        "WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value)  ELSE 0 END  * -1 AS PeriodNetSale, " & _
        " 0 AS OPNetSale, " & _
        "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value)  WHEN " & _
        "(Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) ELSE 0 END *  -1 AS NetLoad , " & _
        "CASE WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
                " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount4 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount8 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount9)) " & _
        "WHEN (Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N') THEN " & _
        "(Invoice_Qty * (Cust_Discount + Disc_Amt)) ELSE 0 END  * -1 AS Discount, " & _
        "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice = 'Y' THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_Item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) ELSE 0 END * -1 AS CreditSale, " & _
        " 0 AS PeriodCashAmt, 0 AS OPCashAmt, " & _
        "0 AS PeriodCheckAmt, 0 AS OPCheckAmt, " & _
        "case when Credit_Invoice='Y' then 0 else -( " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) end AS PeriodEmpty_Value, 0 AS OPEmpty_Value,  " & _
        " 0 AS PeriodadjustEmpty, 0 AS OPadjustEmpty, " & _
        "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
        "0 AS PeriodShellamt, " & _
        "0 AS OPShellamt, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice,  " & _
        "0 AS PeriodBalance_Amt, 0 AS OPBalance_Amt, 'Sale' AS TransType, 0 AS PeriodCashSortage,  " & _
        "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, 0 AS OPEmptyShortage, 'All' AS ReportType, " & _
        "0 AS PeriodDEP, 0 AS OPDEP, 0 AS PeriodRefund, 0 AS OPRefund,  " & _
        " 0 AS PeriodSettlement, 0 AS OPSettlement,TSPL_SALE_RETURN_HEAD.Shell_Qty * 100 as PeriodRetShell,0 as OPRetShell, " & _
        "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
        "FROM  " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
        " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No ON  " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No ON " & _
        "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
        "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
        "left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
        " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
        "WHERE " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' AND " & _
        " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'sale') and  Cust_Type_Code not in ( 'F','S') and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

        If strLocAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        End If

        If strRouteAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If

        If strSalesAll = "N" Then
            strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If


        ''''''''SALE OPENING BALANCE CODE START HERE

        If rdbSummary.IsChecked = True Then

            '--for actual sale main query for opening balance
            strNonRoute += "Union All "


            strNonRoute += "SELECT  Cust_Type_Code,Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale, " & _
            "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value)  " & _
            "WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value) ELSE 0 END AS OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, " & _
            "0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "case when Credit_Invoice='Y' then 0 else  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value end as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty, " & _
            "0 as OPadjustEmpty, '22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "0 as PeriodShellamt, (Shell_Qty *100) as OPShellamt, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "case when Credit_Invoice='Y' then 0 else Balance_Amt end as OPBalance_Amt, " & _
            "'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType,0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "where " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and  " & _
           " Shipment_Type='sale'  and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "

            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRoute += "Union All "

            ''--for cash receiving for opening balance

            strNonRoute += "SELECT    Cust_Type_Code,Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "case when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque' then case when  Credit_Invoice='Y' then 0 " & _
            "else isnull( " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount,0) end else 0 end AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "case when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque' then case when  Credit_Invoice='Y' then 0  " & _
            "else isnull(" + clsCommon.ReplicateDBString + " TSPL_RECEIPT_DETAIL.Applied_Amount,0) end else 0 end  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No " & _
            " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            " where " & _
            " IsChkReverse='N' and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' " & _
            " and Shipment_Type='sale' and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "

            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRoute += "Union All "

            ''--for cash receiving for unapllied entry for opening balance

            strNonRoute += "SELECT    Cust_Type_Code,Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.UnApplied_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "case when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque' then case when  Credit_Invoice='Y' then 0 " & _
            "else isnull( " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.UnApplied_Balance,0) end else 0 end AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "case when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque' then case when  Credit_Invoice='Y' then 0  " & _
            "else isnull(" + clsCommon.ReplicateDBString + " TSPL_RECEIPT_HEADER.UnApplied_Balance,0) end else 0 end  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No " & _
            " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            " where " & _
            " IsChkReverse='N' and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' " & _
            " and Shipment_Type='sale' and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S')  and UnApplied_No <> '' and TSPL_Receipt_Detail.Receipt_Line_No=1 "

            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRoute += "Union All "


            ' ''--for cash and cheque recceiving salesman  entry without customer  Opening wise

            strNonRoute += "SELECT '' AS Cust_Type_Code, convert(date,Receipt_Date,103) AS DocDate, " & _
            "Customer_Name AS PartyName, " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No AS DocNo, " & _
            "BANKACC AS Location, '' AS Route_No, " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Salesman_Name AS SalesName, " & _
            "0 AS PeriodNetSale, 0 AS OPNetSale, 0 AS NetLoad, 0 AS Discount, 0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque' THEN " & _
            "isnull(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_Amount, 0) ELSE 0 END AS OPCashAmt, " & _
            "0 AS PeriodCheckAmt, " & _
            "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque' THEN " & _
            "isnull(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_Amount, 0) ELSE 0 END AS OPCheckAmt, 0 AS PeriodEmpty_Value, 0 AS OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty, 0 AS OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_GL_SEGMENT_CODE.Description as Location_Desc,  " & _
            "0 AS PeriodShellamt, 0 AS OPShellamt, '' AS Credit_Invoice, " & _
            "0 AS PeriodBalance_Amt, 0 AS OPBalance_Amt, 'Sale' AS TransType, " & _
            "0 AS PeriodCashSortage,0 AS PeriodEmptyShortage, 0 AS OPCashSortage,  " & _
            "0 AS OPEmptyShortage, 'All' AS ReportType, 0 AS PeriodDEP, " & _
            "0 AS OPDEP, 0 AS PeriodRefund,0 AS OPRefund, 0 AS PeriodSettlement, " & _
            "0 AS OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_GL_SEGMENT_CODE ON RIGHT(" + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER.BANKACC, 3) = " + clsCommon.ReplicateDBString + "TSPL_GL_SEGMENT_CODE.Segment_code ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Bank_Code = " + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code = " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
            "WHERE (" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.IsChkReverse = 'N') and Receipt_Type='M'  and " & _
            " Salesman_Code <> '' and " & _
            "convert(date,Receipt_Date,103) <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' "

            If strLocAll = "N" Then
                strNonRoute += " and RIGHT(" + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER.BANKACC, 3) in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            'If strRouteAll = "N" Then
            '    strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            'End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRoute += "Union All "



            ''--for adjustment  empty of sale invoice in for opening balance


            strNonRoute += "SELECT    Cust_Type_Code,Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, CASE WHEN Credit_Invoice = 'N' THEN 0 ELSE case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end  END AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,CASE WHEN Credit_Invoice = 'Y' THEN 0 ELSE case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end  END as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, TSPL_ADJUSTMENT_HEADER.Loc_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType , 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Document_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "WHERE (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = 'Sale Invoice') and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType='E' and Adjustment_Type='BI' and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' " & _
            " and Shipment_Type='sale' and  Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "


            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If


            strNonRoute += "Union All "

            '--for adjustment empty  of salesman without sale invoice and without customer in Opening balance

            strNonRoute += "SELECT  '' as   Cust_Type_Code,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DocDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_NAME as PartyName, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code as Location, " & _
            "'' as Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE as Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_NAME AS SalesName," & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt,'N' as Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No " & _
            "WHERE (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = '') and Customer_CODE = '' AND " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType = 'E') AND  (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI') and EMP_CODE <> '' " & _
            " and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' "


            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            'If strRouteAll = "N" Then
            '    strOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            'End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If


            strNonRoute += "Union All "


            '--for adjustment empty  of salesman without sale invoice and with customer in Opening balance

            strNonRoute += "SELECT    Cust_Type_Code,convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DocDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_NAME as PartyName, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code as Location, " & _
            "'' as Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE as Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_NAME AS SalesName," & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, case when Credit_Customer='Y' then case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   else 0 end  AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,case when Credit_Customer='N' then case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   else 0 end    as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt,'N' as Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No " & _
            " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_CODE=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "WHERE (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = '') AND " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType = 'E') and Customer_CODE <> '' AND  (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI') and EMP_CODE <> '' " & _
            " and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and  Cust_Type_Code not in ( 'F','S')"


            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            'If strRouteAll = "N" Then
            '    strOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            'End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If


            strNonRoute += "Union All "
            '--for settlement Opening wise

            strNonRoute += "SELECT Cust_Type_Code,Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AS DocNo, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale, 0 AS OPNetSale, 0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, 0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, 0 AS PeriodCheckAmt, 0 AS OPCheckAmt, " & _
            "0 AS PeriodEmpty_Value, 0 AS OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty, 0 AS OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, 0 AS PeriodShellamt, " & _
            "0 AS OPShellamt, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 AS PeriodBalance_Amt, 0 AS OPBalance_Amt, " & _
            "'Sale' AS TransType, 0 AS PeriodCashSortage, " & _
            "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, " & _
            "0 AS OPEmptyShortage, 'All' AS ReportType, " & _
            "0 AS PeriodDEP, case when SettleMent_Type='DEP' then isnull(Amount,0) else 0 end AS OPDEP, " & _
            "0 AS PeriodRefund, case when SettleMent_Type='REF' then isnull(Amount,0) else 0 end AS OPRefund,  " & _
            "0 AS PeriodSettlement,CASE WHEN Credit_Invoice = 'Y' THEN 0 ELSE case when  " & _
            "(SettleMent_Type <> 'DEP' and SettleMent_Type <> 'REF') then isnull(Amount,0) else 0 end end as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master INNER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail ON " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode = " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail.Discount_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header ON " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Adjustment_No RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Doc_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "WHERE  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'sale') and  Cust_Type_Code not in ( 'F','S') AND SettleMent_Type <> '' and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S'"


            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRoute += "Union All "

            '''''  Sale return for OPENING wise

            strNonRoute += "SELECT Cust_Type_Code,Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No AS DocNo, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale, " & _
            " CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) " & _
            "WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value)  ELSE 0 END  * -1 AS OPNetSale, " & _
            "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value)  WHEN " & _
        "(Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) ELSE 0 END  *  -1 AS NetLoad , " & _
            "CASE WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
                " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount4 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount8 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount9)) " & _
        "WHEN (Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N') THEN " & _
        "(Invoice_Qty * (Cust_Discount + Disc_Amt)) ELSE 0 END  * -1 AS Discount, " & _
            "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice = 'Y' THEN  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_Item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) ELSE 0 END * -1 AS CreditSale, " & _
            " 0 AS PeriodCashAmt, 0 AS OPCashAmt, " & _
            "0 AS PeriodCheckAmt, 0 AS OPCheckAmt, " & _
            "0 AS PeriodEmpty_Value, 0 AS OPEmpty_Value,  " & _
            " 0 AS PeriodadjustEmpty, 0 AS OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "0 AS PeriodShellamt, " & _
            "0 AS OPShellamt, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice,  " & _
            "0 AS PeriodBalance_Amt, 0 AS OPBalance_Amt, 'Sale' AS TransType, 0 AS PeriodCashSortage,  " & _
            "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, 0 AS OPEmptyShortage, 'All' AS ReportType, " & _
            "0 AS PeriodDEP, 0 AS OPDEP, 0 AS PeriodRefund, 0 AS OPRefund,  " & _
            " 0 AS PeriodSettlement, 0 AS OPSettlement,0 as PeriodRetShell,TSPL_SALE_RETURN_HEAD.Shell_Qty * 100  as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
            " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
        " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            "WHERE " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'sale') and  Cust_Type_Code not in ( 'F','S') and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            'strNonRoute += "Union All "

        End If

        ''''''''SALE OPENING BALANCE CODE ENDS HERE

        ''--for quick settlement Period wise

        strRoute = "SELECT    '' as Cust_Type_Code,Quick_Settlement_Date as DocDate,RouteDescription as PartyName, " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location as Location, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode as Salesman_Code, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
        "0 AS PeriodNetSale, " & _
        "0 as OPNetSale, " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Amount - " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Load_In_Amount AS NetLoad, " & _
        "CASE WHEN SettleMent_Type = 'DSC' THEN isnull(Amount,0) ELSE 0 END AS Discount, " & _
        "CASE WHEN SettleMent_Type = 'CRS' THEN isnull(Amount,0) ELSE 0 END AS CreditSale,  " & _
        "CASE WHEN SettleMent_Type = 'CSH' THEN isnull(Amount,0) ELSE 0 END AS PeriodCashAmt,0 as OPCashAmt, " & _
        "CASE WHEN SettleMent_Type = 'CHQ' THEN isnull(Amount,0) ELSE 0 END AS PeriodCheckAmt,0 as OPCheckAmt, " & _
        "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Empty_Load_In AS PeriodEmpty_Value,0 as OPEmpty_Value, " & _
        "0 AS PeriodadjustEmpty,0 AS OPadjustEmpty, " & _
        "'01/09/2012' AS FDate, '26/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.FromLoc_Desc as Location_desc, " & _
        "0 AS PeriodShellamt,0 as OPShellamt, " & _
        "'N' AS Credit_Invoice, " & _
        "0 AS PeriodBalance_Amt,0 as OPBalance_Amt, " & _
        "'Transfer' AS TransType, " & _
        "CASE WHEN SettleMent_Type = 'CSE' THEN case  when Calculate='A' then Amount * (-1) else Amount end ELSE 0 END AS PeriodCashSortage,  " & _
        "CASE WHEN SettleMent_Type = 'ESE' THEN case  when Calculate='A' then Amount * (-1) else Amount end ELSE 0 END AS PeriodEmptyShortage, " & _
        "0 as OPCashSortage,0 as OPEmptyShortage, " & _
        " 'All' AS ReportType,case when (SettleMent_Type='DEP') then isnull(Amount,0) else  0 end as PeriodDEP, " & _
        "0 as OPDEP,case when (SettleMent_Type='REF') then - isnull(Amount,0) else  0 end as PeriodRefund, " & _
        "0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
        "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,tspl_QuickSettleMent.Comments " & _
        "FROM " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_SettleMent_Id = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.Quick_SettleMent_Id LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "tspl_SettleMent_Master ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.SettleMent_Code = " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No " & _
        "left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
        "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
        " " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' " & _
        " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

        If strLocAll = "N" Then
            strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        End If

        If strRouteAll = "N" Then
            strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If

        If strSalesAll = "N" Then
            strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If

        strRoute += "Union All "

        ''--for loadout Period wise

        strRoute += "SELECT '' AS Cust_Type_Code, " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_Settlement_Date AS DocDate, " & _
        "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.RouteDescription AS PartyName," & _
        "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location AS Location, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode AS Salesman_Code, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
        "case when uom <> 'SH' then  (BasicPrice_WithTax + TPT_Value) * Item_Qty else 0 end   AS PeriodNetSale, " & _
        "0 AS OPNetSale, 0 AS NetLoad, 0 AS Discount, 0 AS CreditSale, 0 AS PeriodCashAmt, " & _
        "0 AS OPCashAmt, 0 AS PeriodCheckAmt, 0 AS OPCheckAmt, 0 AS PeriodEmpty_Value, " & _
        "0 AS OPEmpty_Value, 0 AS PeriodadjustEmpty, " & _
        "0 AS OPadjustEmpty, '01/09/2012' AS FDate, '26/09/2012' AS TDate, " & _
        "TSPL_TRANSFER_HEAD.FromLoc_Desc AS Location_desc, 0 AS PeriodShellamt, " & _
        "0 AS OPShellamt, 'N' AS Credit_Invoice, 0 AS PeriodBalance_Amt, " & _
        "0 AS OPBalance_Amt, 'Transfer'AS TransType, 0 AS PeriodCashSortage, " & _
        "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, 0 AS OPEmptyShortage, " & _
        "'All' AS ReportType, 0 AS PeriodDEP, 0 AS OPDEP, 0 AS PeriodRefund, " & _
        "0 AS OPRefund, 0 AS PeriodSettlement, 0 AS OPSettlement, 0 AS PeriodRetShell, 0 AS OPRetShell, " & _
         "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
        "FROM  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent ON " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number " & _
        "left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
        "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
        " " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' " & _
        " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

        If strLocAll = "N" Then
            strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        End If

        If strRouteAll = "N" Then
            strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If

        If strSalesAll = "N" Then
            strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If

        strRoute += "Union All "


        ''--for loadIn Period wise

        strRoute += "SELECT '' AS Cust_Type_Code, " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_Settlement_Date AS DocDate, " & _
        "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.RouteDescription AS PartyName," & _
        "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location AS Location, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode AS Salesman_Code, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
        "case when uom <> 'SH' then -(BasicPrice_WithTax + TPT_Value ) * (LoadIn_Qty + Leak+Breakage+burst+Shortage) else 0 end   AS PeriodNetSale, " & _
        "0 AS OPNetSale, 0 AS NetLoad, 0 AS Discount, 0 AS CreditSale, 0 AS PeriodCashAmt, " & _
        "0 AS OPCashAmt, 0 AS PeriodCheckAmt, 0 AS OPCheckAmt, 0 AS PeriodEmpty_Value, " & _
        "0 AS OPEmpty_Value, 0 AS PeriodadjustEmpty, " & _
        "0 AS OPadjustEmpty, '01/09/2012' AS FDate, '26/09/2012' AS TDate, " & _
        "TSPL_TRANSFER_HEAD.ToLoc_Desc AS Location_desc, 0 AS PeriodShellamt, " & _
        "0 AS OPShellamt, 'N' AS Credit_Invoice, 0 AS PeriodBalance_Amt, " & _
        "0 AS OPBalance_Amt, 'Transfer'AS TransType, 0 AS PeriodCashSortage, " & _
        "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, 0 AS OPEmptyShortage, " & _
        "'All' AS ReportType, 0 AS PeriodDEP, 0 AS OPDEP, 0 AS PeriodRefund, " & _
        "0 AS OPRefund, 0 AS PeriodSettlement, 0 AS OPSettlement, 0 AS PeriodRetShell, 0 AS OPRetShell, " & _
         "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
        "FROM  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No RIGHT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent ON " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Load_Out_No = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number " & _
        "left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
        "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
        " " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' " & _
        " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

        If strLocAll = "N" Then
            strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        End If

        If strRouteAll = "N" Then
            strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If

        If strSalesAll = "N" Then
            strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If

        strRoute += "Union All "


        ''--for quick settlement Adjustment In Period wise

        strRoute += "SELECT  '' as   Cust_Type_Code,Transfer_Date as DocDate, " & _
        "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Route_Desc as PartyName," + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_No AS DocNo," & _
        "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.From_Location as Location, " + clsCommon.ReplicateDBString + "tspl_Transfer_head.Route_No, " & _
        "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Salesmancode as Salesman_Code," + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
        "0 AS PeriodNetSale,0 as OPNetSale, 0 AS NetLoad, " & _
        "0 AS Discount, 0 AS CreditSale, " & _
        "0 AS PeriodCashAmt, 0 AS OPCashAmt, 0  AS PeriodCheckAmt, " & _
        "0  AS OPCheckAmt, 0 as PeriodEmpty_Value, 0 as OPEmpty_Value, " & _
        "case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end AS PeriodadjustEmpty, " & _
        "0 as OPadjustEmpty, '22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, 0 as PeriodShellamt, 0 as OPShellamt, " & _
        "'' as Credit_Invoice, 0 as PeriodBalance_Amt, 0 as OPBalance_Amt, " & _
        "'Transfer' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
        "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
        "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
         "0 AS OPDiscount, " & _
        "0 AS OPCreditSale,0 AS OPNetLoad,'' as  Comments " & _
        "FROM " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "tspl_Transfer_head ON " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "tspl_Transfer_head.Salesmancode RIGHT OUTER JOIN  " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER ON " + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Document_No LEFT OUTER JOIN " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No WHERE  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = 'Load out/Transfer') and " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType='E' and (Adjustment_Type='BI' or Adjustment_Type='BD') and " & _
        "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
        "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  and " & _
        "Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "


        If strLocAll = "N" Then
            strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
        End If

        If strRouteAll = "N" Then
            strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If

        If strSalesAll = "N" Then
            strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
        End If


        ''''''''QUICK SETTLEMENT OPENING BALANCE CODE START HERE

        If rdbSummary.IsChecked = True Then

            strRoute += "Union All "

            strRoute += "SELECT  '' as Cust_Type_Code,Quick_Settlement_Date as DocDate,RouteDescription as PartyName," + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location as Location, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode as Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName," & _
            "0 AS PeriodNetSale, " & _
            "0 as OPNetSale, " & _
            "0 AS NetLoad,  " & _
            "0 AS Discount, " & _
            "0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "CASE WHEN SettleMent_Type = 'CSH' THEN isnull(Amount,0) ELSE 0 END as OPCashAmt, " & _
            "0 AS PeriodCheckAmt, " & _
            "CASE WHEN SettleMent_Type = 'CHQ' THEN isnull(Amount,0) ELSE 0 END  as OPCheckAmt," & _
            "0 AS PeriodEmpty_Value, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Empty_Load_In as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,0 AS OPadjustEmpty, " & _
            "'01/09/2012' AS FDate, '26/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.FromLoc_Desc as Location_desc, " & _
            "0 AS PeriodShellamt,0 as OPShellamt, " & _
            "'N' AS Credit_Invoice, " & _
            "0 AS PeriodBalance_Amt,0 as OPBalance_Amt, " & _
            "'Transfer' AS TransType, " & _
            "0 AS PeriodCashSortage,  " & _
            "0 AS PeriodEmptyShortage, " & _
            "CASE WHEN SettleMent_Type = 'CSE' THEN case  when Calculate='A' then Amount * (-1) else Amount end ELSE 0 END as OPCashSortage, " & _
            "CASE WHEN SettleMent_Type = 'ESE' THEN case  when Calculate='A' then Amount * (-1) else Amount end ELSE 0 END as OPEmptyShortage, " & _
            "'All' AS ReportType,0 as PeriodDEP, " & _
            " case when (SettleMent_Type='DEP') then isnull(Amount,0) else  0 end as OPDEP,0 as PeriodRefund, " & _
            " case when (SettleMent_Type='REF') then - Amount else  0 end as OPRefund,0 AS PeriodSettlement,0 as OPSettlement, " & _
            "0 as PeriodRetShell,0 as OPRetShell, " & _
            "CASE WHEN SettleMent_Type = 'DSC' THEN isnull(Amount,0) ELSE 0 END AS OPDiscount, " & _
            "CASE WHEN SettleMent_Type = 'CRS' THEN isnull(Amount,0) ELSE 0 END AS OPCreditSale, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Amount - " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Load_In_Amount AS OPNetLoad, tspl_QuickSettleMent.Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_SettleMent_Id = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.Quick_SettleMent_Id LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "tspl_SettleMent_Master ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.SettleMent_Code = " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "WHERE  " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'" & _
            " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

            If strLocAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If


            strRoute += "Union All "

            ''--for loadout Opening wise

            strRoute += "SELECT '' AS Cust_Type_Code, " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_Settlement_Date AS DocDate, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.RouteDescription AS PartyName," & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location AS Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode AS Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale, " & _
            "case when uom <> 'SH' then (BasicPrice_WithTax + TPT_Value) * Item_Qty  else 0 end AS OPNetSale, 0 AS NetLoad, 0 AS Discount, 0 AS CreditSale, 0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, 0 AS PeriodCheckAmt, 0 AS OPCheckAmt, 0 AS PeriodEmpty_Value, " & _
            "0 AS OPEmpty_Value, 0 AS PeriodadjustEmpty, " & _
            "0 AS OPadjustEmpty, '01/09/2012' AS FDate, '26/09/2012' AS TDate, " & _
            "TSPL_TRANSFER_HEAD.FromLoc_Desc AS Location_desc, 0 AS PeriodShellamt, " & _
            "0 AS OPShellamt, 'N' AS Credit_Invoice, 0 AS PeriodBalance_Amt, " & _
            "0 AS OPBalance_Amt, 'Transfer'AS TransType, 0 AS PeriodCashSortage, " & _
            "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, 0 AS OPEmptyShortage, " & _
            "'All' AS ReportType, 0 AS PeriodDEP, 0 AS OPDEP, 0 AS PeriodRefund, " & _
            "0 AS OPRefund, 0 AS PeriodSettlement, 0 AS OPSettlement, 0 AS PeriodRetShell, 0 AS OPRetShell, " & _
             "0 AS OPDiscount, " & _
            "0 AS OPCreditSale, " & _
            "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date < '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'  " & _
            " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

            If strLocAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strRoute += "Union All "


            ''--for loadIn Opening wise

            strRoute += "SELECT '' AS Cust_Type_Code, " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_Settlement_Date AS DocDate, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.RouteDescription AS PartyName," & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location AS Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode AS Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale, " & _
            "case when uom <> 'SH' then  -(BasicPrice_WithTax + TPT_Value ) * (LoadIn_Qty + Leak+Breakage+burst+Shortage) else 0 end AS OPNetSale, 0 AS NetLoad, 0 AS Discount, 0 AS CreditSale, 0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, 0 AS PeriodCheckAmt, 0 AS OPCheckAmt, 0 AS PeriodEmpty_Value, " & _
            "0 AS OPEmpty_Value, 0 AS PeriodadjustEmpty, " & _
            "0 AS OPadjustEmpty, '01/09/2012' AS FDate, '26/09/2012' AS TDate, " & _
            "TSPL_TRANSFER_HEAD.ToLoc_Desc AS Location_desc, 0 AS PeriodShellamt, " & _
            "0 AS OPShellamt, 'N' AS Credit_Invoice, 0 AS PeriodBalance_Amt, " & _
            "0 AS OPBalance_Amt, 'Transfer'AS TransType, 0 AS PeriodCashSortage, " & _
            "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, 0 AS OPEmptyShortage, " & _
            "'All' AS ReportType, 0 AS PeriodDEP, 0 AS OPDEP, 0 AS PeriodRefund, " & _
            "0 AS OPRefund, 0 AS PeriodSettlement, 0 AS OPSettlement, 0 AS PeriodRetShell, 0 AS OPRetShell, " & _
             "0 AS OPDiscount, " & _
            "0 AS OPCreditSale, " & _
            "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Load_Out_No = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date < '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'  " & _
            " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

            If strLocAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strRoute += "Union All "

            ''--for quick settlement Adjustment In Opening wise

            strRoute += "SELECT  '' as   Cust_Type_Code,Transfer_Date as DocDate, " & _
            "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Route_Desc as PartyName," + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_No AS DocNo," & _
            "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.From_Location as Location, " + clsCommon.ReplicateDBString + "tspl_Transfer_head.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Salesmancode as Salesman_Code," + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale,0 as OPNetSale, 0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, 0 AS OPCashAmt, 0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, 0 as PeriodEmpty_Value, 0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty, " & _
            "case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) end  as OPadjustEmpty, '22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, 0 as PeriodShellamt, 0 as OPShellamt, " & _
            "'' as Credit_Invoice, 0 as PeriodBalance_Amt, 0 as OPBalance_Amt, " & _
            "'Transfer' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell," & _
             "0 AS OPDiscount, " & _
           "0 AS OPCreditSale, " & _
           "0 AS OPNetLoad,'' as  Comments  FROM " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "tspl_Transfer_head ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "tspl_Transfer_head.Salesmancode RIGHT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER ON " + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Document_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No WHERE  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = 'Load out/Transfer') and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType='E' and (Adjustment_Type='BI'  or " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BD') and " & _
            "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'  and " & _
            "Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "


            If strLocAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If



        End If


        ''''''''QUICK SETTLEMENT OPENING BALANCE CODE ENDS HERE

        If rdbBoth.IsChecked Then
            strQuery = strMain & strNonRoute & " Union All " & strRoute & " ) a group by Cust_Type_Code,Route_No,TransType,SalesName,Location,Location_Desc,Salesman_Code,DocNo,PartyName,DocDate"
        ElseIf rdbRouteWise.IsChecked Then
            strQuery = strMain & strRoute & " ) a group by Cust_Type_Code,Route_No,TransType,SalesName,Location,Location_Desc,Salesman_Code,DocNo,PartyName,DocDate"
        ElseIf rdnNonRoutewise.IsChecked Then
            strQuery = strMain & strNonRoute & " ) a group by Cust_Type_Code,Route_No,TransType,SalesName,Location,Location_Desc,Salesman_Code,DocNo,PartyName,DocDate"
        End If

        Dim strSummary As String = ""
        If rdbSummary.IsChecked = True Then
            ' '' ''strEmp = "select '' as Cust_Type_Code,'' as DocDate,'' as PartyName,'' as DocNo,Emp_Name as Salesname,EMP_CODE as PayrollCode,0 as NetLoad, " & _
            ' '' ''"0 as NetSale,0 as Discount,0 as CreditSale,0 as Settlement,0 as PeriodDepRefund, " & _
            ' '' ''"0 as OPDepRefund,0 as CashAmt,0 as CheckAmt,0 as EmptyIn, " & _
            ' '' ''"cash as OPCashShortage,0 as PercashShortage,Empty_Ex as OpEmptyShortage, " & _
            ' '' ''"0 as PerEmptyShortage,(Cash +  Empty_Ex) as OPBalance,0 as PerBalance,'' as location, " & _
            ' '' ''"'' as Location_Desc,'' as Fdate,'' as Tdate,'' as Comments from " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER where (Cash <> 0 or Empty_Ex <> 0) "

            strEmp = "select '' as Cust_Type_Code,'' as DocDate,'' as PartyName,'' as DocNo,Emp_Name as Salesname,EMP_CODE as PayrollCode,0 as NetLoad, " & _
           "0 as NetSale,0 as Discount,0 as CreditSale,0 as Settlement,0 as PeriodDepRefund, " & _
           "0 as OPDepRefund,0 as CashAmt,0 as CheckAmt,0 as EmptyIn, " & _
           "cash as OPCashShortage,0 as PercashShortage,Empty_Ex as OpEmptyShortage, " & _
           "0 as PerEmptyShortage,(Cash +  Empty_Ex) as OPBalance,0 as PerBalance,'' as location, " & _
           "isnull((select top 1 Location_Desc from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD left outer join " & _
           "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
           "where " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE) , " & _
           "(select top 1 FromLoc_Desc from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD where " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE and " & _
           "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type='LO') )  as Location_Desc, " & _
           "'' as Fdate,'' as Tdate,'' as Comments from " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER where (Cash <> 0 or Empty_Ex <> 0) "

            If strSalesAll = "N" Then
                strEmp += " and " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If


            strSummary += "select SalesName,PayrollCode,Location_Desc as Location, " & _
            "SUM(OPcashShortage) as OPcashShortage, " & _
            "SUM(OPEmptyShortage) as OPEmptyShortage, " & _
            "SUM(OPcashShortage) + SUM(OPEmptyShortage) AS OPBalance, " & _
            "SUM(PercashShortage) as PercashShortage, " & _
            "SUM(PerEmptyShortage) as PerEmptyShortage, " & _
            "SUM(PercashShortage) + SUM(PerEmptyShortage) as PerBalance, " & _
            "SUM(OPcashShortage) + SUM(PercashShortage) as ClosingCashShortage, " & _
            "SUM(OPEmptyShortage) + SUM(PerEmptyShortage) as ClosingEmptyShortage, " & _
            "SUM(OPcashShortage) + SUM(OPEmptyShortage) + SUM(PercashShortage) + SUM(PerEmptyShortage) as ClosingBalance from " & _
            "(  " & strQuery & " " & _
            " union all " & _
            "" & strEmp & ")xxx  group by SalesName,PayrollCode,Location_Desc"
            strQuery = strSummary

        ElseIf rdbDetail.IsChecked Then
            strSummary = "select DocNo,DocDate,PartyName,SalesName,Cust_Type_Code as CustClass,Location_Desc as Location, " & _
            "PayrollCode,NetLoad,NetSale,Discount,CreditSale,Settlement, " & _
            "PeriodDepRefund,CashAmt,CheckAmt,EmptyIn,PercashShortage,PerEmptyShortage, " & _
            "(PercashShortage + PerEmptyShortage)PerBalance,Comments from (  " & strQuery & "  ) b "
            strQuery = strSummary
        ElseIf rdbSalesmanSummary.IsChecked = True Then










            strMain = "SELECT   DocNo,DocDate,PartyName, " & _
        " SalesName,Salesman_Code as PayrollCode," & _
        "case when TransType='Sale' then  sum(NetLoad) + SUM(Discount) else MAX(NetLoad) end   as NetLoad, " & _
        "case when TransType='Sale' then sum(PeriodNetSale) + MAX(PeriodShellamt)  - MAX(PeriodRetShell) else sum(PeriodNetSale)  end as NetSale," & _
        "convert(decimal(18,2),sum(Discount)) as Discount," & _
       "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then case when sum(PeriodNetSale) <> 0 then " & _
        "SUM(CreditSale) + MAX(PeriodShellamt)  - MAX(PeriodRetShell) - sum(periodadjustEmpty) else SUM(CreditSale) end  else 0 end " & _
        "else SUM(creditSale)  end  as CreditSale," & _
        "SUM(PeriodSettlement) as Settlement, " & _
        "SUM(PeriodDEP) + SUM(PeriodRefund) as PeriodDepRefund," & _
        "SUM(OPDEP) + SUM(OPRefund) as OPDepRefund," & _
        "case when TransType='Sale' then max(periodCashAmt) else SUM(PeriodCashAmt) end as CashAmt," & _
        "case when TransType='Sale' then max(PeriodCheckAmt) else SUM(PeriodCheckAmt) end as CheckAmt," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else " & _
        "   sum(OPNetSale)   - ( sum(OPCashAmt) + sum(OPCheckAmt) + sum(OPEmpty_Value) + sum(OPSettlement) + SUM(OPRefund) ) end else " & _
        "   case when sum(OPCashSortage) <> 0 then sum(OPCashSortage) else sum(OPNetSale) - SUM(OPDiscount) - SUM(OPcreditSale) - sum(OPCashAmt) - sum(OPCheckAmt) end end as OPcashShortage," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else " & _
        "   sum(periodNetSale) - ( sum(periodCashAmt) + sum(periodCheckAmt) + sum(periodEmpty_Value) + SUM(PeriodSettlement) + SUM(PeriodRefund)) end else " & _
        "  case when sum(PeriodCashSortage) <> 0 then sum(PeriodCashSortage) else sum(PeriodNetSale) - SUM(Discount) - SUM(creditSale) - sum(periodCashAmt) - sum(periodCheckAmt) end end as PercashShortage, " & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else " & _
        "   sum(OPEmpty_Value) + max(OPShellamt)  - MAX(OPRetShell) - (sum(OPadjustEmpty) + SUM(OPDEP) ) end else " & _
        "   Max(OPNetLoad) - sum(OPNetSale)- SUM(OPDEP) - SUM(OPRefund) - sum(OPadjustEmpty) end  as OPEmptyShortage," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else " & _
        "   sum(periodEmpty_Value) + max(periodShellamt) - MAX(PeriodRetShell) - (sum(periodadjustEmpty) + SUM(PeriodDEP) ) end else " & _
        "   Max(NetLoad) - sum(PeriodNetSale)- SUM(PeriodDEP) - SUM(PeriodRefund) - sum(periodadjustEmpty) end  as PerEmptyShortage," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else   max(OPBalance_Amt) end else sum(OPCashSortage) + sum(OPEmptyShortage) end  as OPBalance," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else   max(periodBalance_Amt) end else sum(PeriodCashSortage) + sum(PeriodEmptyShortage) end  as PerBalance," & _
        "Location,Location_Desc,max(FDate) as FDate,max(TDate) as TDate from("

            strNonRoute = "SELECT  Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName," & _
            "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value)  WHEN " & _
            "(Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value) ELSE 0 END AS PeriodNetSale,0 as OPNetSale," & _
            "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value)  WHEN " & _
            "(Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value) ELSE 0 END AS NetLoad, " & _
            "CASE WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN ((Basic_Rate * Invoice_Qty)) " & _
            "WHEN (Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N') THEN " & _
            "(Invoice_Qty * (Cust_Discount + Disc_Amt)) ELSE 0 END AS Discount, " & _
            "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice = 'Y' THEN " & _
            "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value)  WHEN " & _
            "(Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value) ELSE 0 END ELSE 0 END AS CreditSale," & _
            "0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "case when Credit_Invoice='Y' then 0 else  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value end as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc,(Shell_Qty *100) as PeriodShellamt, " & _
            "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "case when Credit_Invoice='Y' then 0 else Balance_Amt end as PeriodBalance_Amt," & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage,0 as OPCashSortage, " & _
            "0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP,0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
             "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "where  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' and Shipment_Type='sale' " & _
            "and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "

            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRoute += "Union All "

            ' ''--for cash and cheque recceiving  Period wise

            strNonRoute += " SELECT   Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName, " + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, " & _
            "case  when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque' then case when Credit_Invoice='Y' then 0 " & _
            "else isnull( " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount,0)  end  else 0 end AS PeriodCashAmt, " & _
            "0 AS OPCashAmt," & _
            "case  when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque' then case when Credit_Invoice='Y' then 0 " & _
            "else  isnull(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount,0)  end  else 0 end  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage, " & _
            "0 as PeriodEmptyShortage,0 as OPCashSortage,0 as OPEmptyShortage, " & _
            "'All' as ReportType, 0 as PeriodDEP,0 as OPDEP,0 as PeriodRefund, " & _
            "0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
             "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "where IsChkReverse='N' and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' and Shipment_Type='sale' " & _
            " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "

            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRoute += "Union All "



            ' ''--for cash and cheque recceiving  for unapplied entry Period wise

            strNonRoute += " SELECT   Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName, " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.UnApplied_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, " & _
            "case  when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque' then case when Credit_Invoice='Y' then 0 " & _
            "else isnull( " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.UnApplied_Balance,0)  end  else 0 end AS PeriodCashAmt, " & _
            "0 AS OPCashAmt," & _
            "case  when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque' then case when Credit_Invoice='Y' then 0 " & _
            "else  isnull(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.UnApplied_Balance,0)  end  else 0 end  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage, " & _
            "0 as PeriodEmptyShortage,0 as OPCashSortage,0 as OPEmptyShortage, " & _
            "'All' as ReportType, 0 as PeriodDEP,0 as OPDEP,0 as PeriodRefund, " & _
            "0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
             "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "where IsChkReverse='N' and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' and Shipment_Type='sale' " & _
            " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S')  and UnApplied_No <> '' and TSPL_Receipt_Detail.Receipt_Line_No=1 "

            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRoute += "Union All "


            ' ''--for cash and cheque recceiving salesman  entry without customer  Period wise

            strNonRoute += "SELECT convert(date,Receipt_Date,103) AS DocDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Customer_Name AS PartyName, " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No AS DocNo, " & _
            "BANKACC AS Location, '' AS Route_No, " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Salesman_Name AS SalesName, " & _
            "0 AS PeriodNetSale, 0 AS OPNetSale, 0 AS NetLoad, 0 AS Discount, 0 AS CreditSale, " & _
            "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque' THEN " & _
            "isnull(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_Amount, 0) ELSE 0 END AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, " & _
            "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque' THEN " & _
            "isnull(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_Amount, 0) ELSE 0 END AS PeriodCheckAmt, " & _
            "0 AS OPCheckAmt, 0 AS PeriodEmpty_Value, 0 AS OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty, 0 AS OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_GL_SEGMENT_CODE.Description as Location_Desc,  " & _
            "0 AS PeriodShellamt, 0 AS OPShellamt, '' AS Credit_Invoice, " & _
            "0 AS PeriodBalance_Amt, 0 AS OPBalance_Amt, 'Sale' AS TransType, " & _
            "0 AS PeriodCashSortage,0 AS PeriodEmptyShortage, 0 AS OPCashSortage,  " & _
            "0 AS OPEmptyShortage, 'All' AS ReportType, 0 AS PeriodDEP, " & _
            "0 AS OPDEP, 0 AS PeriodRefund,0 AS OPRefund, 0 AS PeriodSettlement, " & _
            "0 AS OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
             "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_GL_SEGMENT_CODE ON RIGHT(" + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER.BANKACC, 3) = " + clsCommon.ReplicateDBString + "TSPL_GL_SEGMENT_CODE.Segment_code ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Bank_Code = " + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code = " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
            "WHERE (" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.IsChkReverse = 'N') and Receipt_Type='M'  and " & _
            " Salesman_Code <> '' and " & _
            "convert(date,Receipt_Date,103) >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "convert(date,Receipt_Date,103) <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' "

            If strLocAll = "N" Then
                strNonRoute += " and RIGHT(" + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER.BANKACC, 3) in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            'If strRouteAll = "N" Then
            '    strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            'End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRoute += "Union All "


            '--for adjustment empty  of saleinvoice in Period wise

            strNonRoute += "SELECT    Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
            "TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName," & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, CASE WHEN Credit_Invoice = 'N' THEN 0 ELSE case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end  END AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "CASE WHEN Credit_Invoice = 'Y' THEN 0 ELSE case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   END AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Document_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "WHERE (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = 'Sale Invoice') and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType='E' " & _
            "and Adjustment_Type='BI' and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' and Shipment_Type='sale' " & _
            " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "


            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If




            strNonRoute += "Union All "

            '--for adjustment empty  of salesman without sale invoice and without customer in Period wise

            strNonRoute += "SELECT    convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DocDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_NAME as PartyName, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code as Location, " & _
            "'' as Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE as Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_NAME AS SalesName," & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt,'N' as  Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No " & _
            "WHERE (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = '') AND " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType = 'E') and Customer_CODE = '' AND  (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI') and EMP_CODE <> '' " & _
            "and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'"



            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            'If strRouteAll = "N" Then
            '    strOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            'End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If


            strNonRoute += "Union All "


            '--for adjustment empty  of salesman without sale invoice and with customer in Period wise

            strNonRoute += "SELECT    convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DocDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_NAME as PartyName, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code as Location, " & _
            "'' as Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE as Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_NAME AS SalesName," & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, case when Credit_Customer='Y' then case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   else 0 end AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            " case when Credit_Customer='N' then case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end  else 0 end  AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt,case when Credit_Customer='Y' then 'Y' else 'N' end as  Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No " & _
            " left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_CODE=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "WHERE (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = '') AND " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType = 'E') and Customer_CODE <> '' AND  (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI') and EMP_CODE <> '' " & _
            "and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and " & _
            "convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' and  Cust_Type_Code not in ( 'F','S')"



            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            'If strRouteAll = "N" Then
            '    strOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            'End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If


            strNonRoute += "Union All "

            ''--for settlement Period wise

            strNonRoute += "SELECT    Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AS DocNo, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale, 0 AS OPNetSale, 0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, 0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, 0 AS PeriodCheckAmt, 0 AS OPCheckAmt, " & _
            "0 AS PeriodEmpty_Value, 0 AS OPEmpty_Value,  " & _
            "0 AS PeriodadjustEmpty, 0 AS OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, 0 AS PeriodShellamt, " & _
            "0 AS OPShellamt, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 AS PeriodBalance_Amt, 0 AS OPBalance_Amt, " & _
            "'Sale' AS TransType, 0 AS PeriodCashSortage, " & _
            "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, " & _
            "0 AS OPEmptyShortage, 'All' AS ReportType, " & _
            "case when SettleMent_Type='DEP' then isnull(Amount,0) else 0 end AS PeriodDEP, 0 AS OPDEP, " & _
            "case when SettleMent_Type='REF' then isnull(Amount,0) else 0 end AS PeriodRefund, 0 AS OPRefund,  " & _
            "CASE WHEN Credit_Invoice = 'Y' THEN 0 ELSE case when (SettleMent_Type <> 'DEP' and SettleMent_Type <> 'REF') then " & _
            "isnull(Amount,0) else 0 end end AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master INNER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail ON " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode = " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail.Discount_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header ON " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Adjustment_No RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Doc_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "WHERE  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' AND  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'sale') AND SettleMent_Type <> '' " & _
            " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "

            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRoute += "Union All "

            '''''  Sale return for period wise

            strNonRoute += "SELECT  Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No AS DocNo, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) " & _
            "WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value)  ELSE 0 END  * -1 AS PeriodNetSale, " & _
            " 0 AS OPNetSale, " & _
            "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value)  WHEN " & _
        "(Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) ELSE 0 END  *  -1 AS NetLoad , " & _
            "CASE WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
                " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount4 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount8 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount9)) " & _
        "WHEN (Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N') THEN " & _
        "(Invoice_Qty * (Cust_Discount + Disc_Amt)) ELSE 0 END  * -1 AS Discount, " & _
            "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice = 'Y' THEN  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_Item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) ELSE 0 END * -1 AS CreditSale, " & _
            " 0 AS PeriodCashAmt, 0 AS OPCashAmt, " & _
            "0 AS PeriodCheckAmt, 0 AS OPCheckAmt, " & _
            "case when Credit_Invoice='Y' then 0 else -( " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) end AS PeriodEmpty_Value, 0 AS OPEmpty_Value,  " & _
            " 0 AS PeriodadjustEmpty, 0 AS OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "0 AS PeriodShellamt, " & _
            "0 AS OPShellamt, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice,  " & _
            "0 AS PeriodBalance_Amt, 0 AS OPBalance_Amt, 'Sale' AS TransType, 0 AS PeriodCashSortage,  " & _
            "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, 0 AS OPEmptyShortage, 'All' AS ReportType, " & _
            "0 AS PeriodDEP, 0 AS OPDEP, 0 AS PeriodRefund, 0 AS OPRefund,  " & _
            " 0 AS PeriodSettlement, 0 AS OPSettlement,TSPL_SALE_RETURN_HEAD.Shell_Qty * 100 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
            " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
        " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            "WHERE " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' AND " & _
            " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'sale') and  Cust_Type_Code not in ( 'F','S') and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

            If strLocAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRoute += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            'strNonRoute += "Union All "

            ''''''''SALE OPENING BALANCE CODE START HERE

            'If rdbSummary.IsChecked = True Then

            '--for actual sale main query for opening balance

            strMainopening = "SELECT   DocNo,DocDate,PartyName, " & _
       " SalesName,Salesman_Code as PayrollCode," & _
       "case when TransType='Sale' then  sum(NetLoad) + SUM(Discount) + MAX(PeriodShellamt) - MAX(PeriodRetShell) else MAX(NetLoad) end   as NetLoad, " & _
       "case when TransType='Sale' then sum(PeriodNetSale) + MAX(PeriodShellamt) - MAX(PeriodRetShell) else sum(PeriodNetSale)  end as NetSale," & _
       "convert(decimal(18,2),sum(Discount)) as Discount," & _
       "case when TransType='Sale' then SUM(CreditSale) else SUM(creditSale) end  as CreditSale," & _
       "SUM(PeriodSettlement) as Settlement, " & _
       "SUM(PeriodDEP) + SUM(PeriodRefund) as PeriodDepRefund," & _
       "SUM(OPDEP) + SUM(OPRefund) as OPDepRefund," & _
       "case when TransType='Sale' then max(periodCashAmt) else SUM(PeriodCashAmt) end as CashAmt," & _
       "case when TransType='Sale' then max(PeriodCheckAmt) else SUM(PeriodCheckAmt) end as CheckAmt," & _
       "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else  sum(periodadjustEmpty)  end else 0 end  as EmptyIn," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else " & _
        "   sum(OPNetSale)   - ( sum(OPCashAmt) + sum(OPCheckAmt) + sum(OPEmpty_Value) + sum(OPSettlement) + SUM(OPRefund) ) end else " & _
        "   case when sum(OPCashSortage) <> 0 then sum(OPCashSortage) else sum(OPNetSale) - SUM(OPDiscount) - SUM(OPcreditSale) - sum(OPCashAmt) - sum(OPCheckAmt) end end as OPcashShortage," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else " & _
        "   sum(periodNetSale) - ( sum(periodCashAmt) + sum(periodCheckAmt) + sum(periodEmpty_Value) + SUM(PeriodSettlement) + SUM(PeriodRefund)) end else " & _
        "  case when sum(PeriodCashSortage) <> 0 then sum(PeriodCashSortage) else sum(PeriodNetSale) - SUM(Discount) - SUM(creditSale) - sum(periodCashAmt) - sum(periodCheckAmt) end end as PercashShortage, " & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else " & _
        "   sum(OPEmpty_Value) + max(OPShellamt)  - MAX(OPRetShell) - (sum(OPadjustEmpty) + SUM(OPDEP) ) end else " & _
        "   Max(OPNetLoad) - sum(OPNetSale)- SUM(OPDEP) - SUM(OPRefund) - sum(OPadjustEmpty) end  as OPEmptyShortage," & _
        "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else " & _
        "   sum(periodEmpty_Value) + max(periodShellamt) - MAX(PeriodRetShell) - (sum(periodadjustEmpty) + SUM(PeriodDEP) ) end else " & _
        "   Max(NetLoad) - sum(PeriodNetSale)- SUM(PeriodDEP) - SUM(PeriodRefund) - sum(periodadjustEmpty) end  as PerEmptyShortage," & _
       "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else   max(OPBalance_Amt) end else sum(OPCashSortage) + sum(OPEmptyShortage) end  as OPBalance," & _
       "case when TransType='Sale' then case when max(Credit_Invoice)='Y' then 0 else   max(periodBalance_Amt) end else sum(PeriodCashSortage) + sum(PeriodEmptyShortage) end  as PerBalance," & _
       "Location,Location_Desc,max(FDate) as FDate,max(TDate) as TDate from("

            strNonRouteOpening = "SELECT  Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale, " & _
            "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value)  " & _
            "WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value) ELSE 0 END AS OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, " & _
            "0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "case when Credit_Invoice='Y' then 0 else  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Empty_Value end as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty, " & _
            "0 as OPadjustEmpty, '22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "0 as PeriodShellamt, (Shell_Qty *100) as OPShellamt, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "case when Credit_Invoice='Y' then 0 else Balance_Amt end as OPBalance_Amt, " & _
            "'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType,0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad ,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No LEFT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "where " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and  " & _
           " Shipment_Type='sale'  and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "

            If strLocAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRouteOpening += "Union All "

            ''--for cash receiving for opening balance

            strNonRouteOpening += "SELECT    Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "case when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque' then case when  Credit_Invoice='Y' then 0 " & _
            "else isnull( " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Applied_Amount,0) end else 0 end AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "case when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque' then case when  Credit_Invoice='Y' then 0  " & _
            "else isnull(" + clsCommon.ReplicateDBString + " TSPL_RECEIPT_DETAIL.Applied_Amount,0) end else 0 end  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "where " & _
            " IsChkReverse='N' and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' " & _
            " and Shipment_Type='sale' and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "


            If strLocAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRouteOpening += "Union All "


            ''--for cash receiving for unapplied entry opening balance

            strNonRouteOpening += "SELECT    Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.UnApplied_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "case when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque' then case when  Credit_Invoice='Y' then 0 " & _
            "else isnull( " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.UnApplied_Balance,0) end else 0 end AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "case when " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque' then case when  Credit_Invoice='Y' then 0  " & _
            "else isnull(" + clsCommon.ReplicateDBString + " TSPL_RECEIPT_HEADER.UnApplied_Balance,0) end else 0 end  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,0 as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No = " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_DETAIL.Receipt_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "where " & _
            " IsChkReverse='N' and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' " & _
            " and Shipment_Type='sale' and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') and UnApplied_No <> '' and TSPL_Receipt_Detail.Receipt_Line_No=1  "


            If strLocAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRouteOpening += "Union All "


            ' ''--for cash and cheque recceiving salesman  entry without customer  Opening balance wise

            strNonRouteOpening += "SELECT  convert(date,Receipt_Date,103) AS DocDate, " & _
            "Customer_Name AS PartyName, " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_No AS DocNo, " & _
            "BANKACC AS Location, '' AS Route_No, " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Salesman_Name AS SalesName, " & _
            "0 AS PeriodNetSale, 0 AS OPNetSale, 0 AS NetLoad, 0 AS Discount, 0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type <> 'Cheque' THEN " & _
            "isnull(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_Amount, 0) ELSE 0 END AS OPCashAmt, " & _
            "0 AS PeriodCheckAmt, " & _
            "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Type = 'Cheque' THEN " & _
            "isnull(" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Receipt_Amount, 0) ELSE 0 END AS OPCheckAmt, 0 AS PeriodEmpty_Value, 0 AS OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty, 0 AS OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_GL_SEGMENT_CODE.Description as Location_Desc,  " & _
            "0 AS PeriodShellamt, 0 AS OPShellamt, '' AS Credit_Invoice, " & _
            "0 AS PeriodBalance_Amt, 0 AS OPBalance_Amt, 'Sale' AS TransType, " & _
            "0 AS PeriodCashSortage,0 AS PeriodEmptyShortage, 0 AS OPCashSortage,  " & _
            "0 AS OPEmptyShortage, 'All' AS ReportType, 0 AS PeriodDEP, " & _
            "0 AS OPDEP, 0 AS PeriodRefund,0 AS OPRefund, 0 AS PeriodSettlement, " & _
            "0 AS OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_GL_SEGMENT_CODE ON RIGHT(" + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER.BANKACC, 3) = " + clsCommon.ReplicateDBString + "TSPL_GL_SEGMENT_CODE.Segment_code ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Bank_Code = " + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER.BANK_CODE LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Payment_Code = " + clsCommon.ReplicateDBString + "TSPL_PAYMENT_CODE.Payment_Code " & _
            "WHERE (" + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.IsChkReverse = 'N') and Receipt_Type='M'  and " & _
            "Salesman_Code <> '' and " & _
            "convert(date,Receipt_Date,103) <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'  "

            If strLocAll = "N" Then
                strNonRouteOpening += " and RIGHT(" + clsCommon.ReplicateDBString + "TSPL_BANK_MASTER.BANKACC, 3) in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            'If strRouteAll = "N" Then
            '    strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            'End If

            If strSalesAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_RECEIPT_HEADER.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRouteOpening += "Union All "



            ''--for adjustment  empty of sale invoice  for opening balance


            strNonRouteOpening += "SELECT Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Sale_Invoice_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, CASE WHEN Credit_Invoice = 'N' THEN 0 ELSE case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   END AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,CASE WHEN Credit_Invoice = 'Y' THEN 0 ELSE case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   END as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, TSPL_ADJUSTMENT_HEADER.Loc_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType , 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Document_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "WHERE (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = 'Sale Invoice') and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType='E' and Adjustment_Type='BI' and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' " & _
            " and Shipment_Type='sale' and  Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' and  Cust_Type_Code not in ( 'F','S') "

            If strLocAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRouteOpening += "Union All "


            '--for adjustment empty  of salesman without sale invoice and without customer in Opening balance

            strNonRouteOpening += "SELECT    convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DocDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_NAME as PartyName, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code as Location, " & _
            "'' as Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE as Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_NAME AS SalesName," & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt,'N' as Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No " & _
            "WHERE (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = '') AND " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType = 'E') and Customer_CODE = '' AND  (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI') and EMP_CODE <> '' " & _
            " and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' "


            If strLocAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            'If strRouteAll = "N" Then
            '    strOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            'End If

            If strSalesAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRouteOpening += "Union All "


            '--for adjustment empty  of salesman without sale invoice and with customer in Opening balance

            strNonRouteOpening += "SELECT    convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) as DocDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_NAME as PartyName, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code as Location, " & _
            "'' as Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE as Salesman_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_NAME AS SalesName," & _
            "0 AS PeriodNetSale,0 as OPNetSale, " & _
            "0 AS NetLoad, " & _
            "0 AS Discount,  case when Credit_Customer='Y' then case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end else 0 end   AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, " & _
            "0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, " & _
            "0 as PeriodEmpty_Value, " & _
            "0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty, case when Credit_Customer='N' then case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end else 0 end  as OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, " & _
            "0 as PeriodShellamt, " & _
            "0 as OPShellamt,'N' as Credit_Invoice, " & _
            "0 as PeriodBalance_Amt, " & _
            "0 as OPBalance_Amt,'Sale' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Customer_CODE=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "WHERE (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = '') AND " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType = 'E') and Customer_CODE <> '' AND  (" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BI') and EMP_CODE <> '' " & _
            " and convert(date,TSPL_ADJUSTMENT_HEADER.Adjustment_Date,103) <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' and  Cust_Type_Code not in ( 'F','S') "


            If strLocAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Code in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            'If strRouteAll = "N" Then
            '    strOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            'End If

            If strSalesAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.EMP_CODE in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRouteOpening += "Union All "


            '--for settlement Opening wise

            strNonRouteOpening += "SELECT Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AS DocNo, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale, 0 AS OPNetSale, 0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, 0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, 0 AS PeriodCheckAmt, 0 AS OPCheckAmt, " & _
            "0 AS PeriodEmpty_Value, 0 AS OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty, 0 AS OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, 0 AS PeriodShellamt, " & _
            "0 AS OPShellamt, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice, " & _
            "0 AS PeriodBalance_Amt, 0 AS OPBalance_Amt, " & _
            "'Sale' AS TransType, 0 AS PeriodCashSortage, " & _
            "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, " & _
            "0 AS OPEmptyShortage, 'All' AS ReportType, " & _
            "0 AS PeriodDEP, case when SettleMent_Type='DEP' then isnull(Amount,0) else 0 end AS OPDEP, " & _
            "0 AS PeriodRefund, case when SettleMent_Type='REF' then isnull(Amount,0) else 0 end AS OPRefund,  " & _
            "0 AS PeriodSettlement,CASE WHEN Credit_Invoice = 'Y' THEN 0 ELSE case when  " & _
            "(SettleMent_Type <> 'DEP' and SettleMent_Type <> 'REF') then isnull(Amount,0) else 0 end end as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master INNER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail ON " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode = " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail.Discount_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header ON " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Detail.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Adjustment_No RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_Receipt_Adjustment_Header.Doc_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "WHERE  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'sale') and  Cust_Type_Code not in ( 'F','S') AND SettleMent_Type <> '' and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S'"

            If strLocAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strNonRouteOpening += "Union All "

            '''''  Sale return for OPENING wise

            strNonRouteOpening += "SELECT Sale_Invoice_Date as DocDate," + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Cust_Name as PartyName," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No AS DocNo, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale, " & _
            " CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) " & _
            "WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value)  ELSE 0 END  * -1 AS OPNetSale, " & _
            "CASE WHEN Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N' THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value)  WHEN " & _
        "(Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN  " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) ELSE 0 END  *  -1 AS NetLoad , " & _
            "CASE WHEN (Scheme_Item = 'Y' OR Promo_Scheme_Item = 'Y' OR Sampling_Item = 'Y') THEN (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (ISNULL(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 0) " & _
                " - (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount1 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount2 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount3 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount4 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount5 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount6 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount7 + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount8 " & _
                " + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount9)) " & _
        "WHEN (Scheme_Item = 'N' AND Promo_Scheme_Item = 'N' AND Sampling_Item = 'N') THEN " & _
        "(Invoice_Qty * (Cust_Discount + Disc_Amt)) ELSE 0 END  * -1 AS Discount, " & _
            "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice = 'Y' THEN  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Total_Item_Amt + " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Empty_Value) ELSE 0 END * -1 AS CreditSale, " & _
            " 0 AS PeriodCashAmt, 0 AS OPCashAmt, " & _
            "0 AS PeriodCheckAmt, 0 AS OPCheckAmt, " & _
            "0 AS PeriodEmpty_Value, 0 AS OPEmpty_Value,  " & _
            " 0 AS PeriodadjustEmpty, 0 AS OPadjustEmpty, " & _
            "'22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "0 AS PeriodShellamt, " & _
            "0 AS OPShellamt, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Credit_Invoice,  " & _
            "0 AS PeriodBalance_Amt, 0 AS OPBalance_Amt, 'Sale' AS TransType, 0 AS PeriodCashSortage,  " & _
            "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, 0 AS OPEmptyShortage, 'All' AS ReportType, " & _
            "0 AS PeriodDEP, 0 AS OPDEP, 0 AS PeriodRefund, 0 AS OPRefund,  " & _
            " 0 AS PeriodSettlement, 0 AS OPSettlement,0 as PeriodRetShell,TSPL_SALE_RETURN_HEAD.Shell_Qty * 100 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
            " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
        " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            "WHERE " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type = 'sale') and  Cust_Type_Code not in ( 'F','S') and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "


            If strLocAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strNonRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            'strNonRouteOpening += "Union All "


            ''''''''SALE OPENING BALANCE CODE ENDS HERE

            ''--for quick settlement Period wise

            strRoute = "SELECT    Quick_Settlement_Date as DocDate,RouteDescription as PartyName, " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location as Location, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode as Salesman_Code, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale, " & _
            "0 as OPNetSale, " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Amount - " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Load_In_Amount AS NetLoad, " & _
            "CASE WHEN SettleMent_Type = 'DSC' THEN isnull(Amount,0) ELSE 0 END AS Discount, " & _
            "CASE WHEN SettleMent_Type = 'CRS' THEN isnull(Amount,0) ELSE 0 END AS CreditSale,  " & _
            "CASE WHEN SettleMent_Type = 'CSH' THEN isnull(Amount,0) ELSE 0 END AS PeriodCashAmt,0 as OPCashAmt, " & _
            "CASE WHEN SettleMent_Type = 'CHQ' THEN isnull(Amount,0) ELSE 0 END AS PeriodCheckAmt,0 as OPCheckAmt, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Empty_Load_In AS PeriodEmpty_Value,0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,0 AS OPadjustEmpty, " & _
            "'01/09/2012' AS FDate, '26/09/2012' AS TDate, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.FromLoc_Desc as Location_desc, " & _
            "0 AS PeriodShellamt,0 as OPShellamt, " & _
            "'N' AS Credit_Invoice, " & _
            "0 AS PeriodBalance_Amt,0 as OPBalance_Amt, " & _
            "'Transfer' AS TransType, " & _
            "CASE WHEN SettleMent_Type = 'CSE' THEN case  when Calculate='A' then Amount * (-1) else Amount end ELSE 0 END AS PeriodCashSortage,  " & _
            "CASE WHEN SettleMent_Type = 'ESE' THEN case  when Calculate='A' then Amount * (-1) else Amount end ELSE 0 END AS PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage, " & _
            " 'All' AS ReportType,case when (SettleMent_Type='DEP') then isnull(Amount,0) else  0 end as PeriodDEP, " & _
            "0 as OPDEP,case when (SettleMent_Type='REF') then - isnull(Amount,0) else  0 end as PeriodRefund, " & _
            "0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
            "0 AS OPCreditSale, " & _
            "0 AS OPNetLoad, tspl_QuickSettleMent.Comments " & _
            "FROM " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_SettleMent_Id = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.Quick_SettleMent_Id LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "tspl_SettleMent_Master ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.SettleMent_Code = " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            " " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' " & _
            " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

            If strLocAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strRoute += "Union All "


            ''--for loadout Period wise

            strRoute += "SELECT  " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_Settlement_Date AS DocDate, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.RouteDescription AS PartyName," & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location AS Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode AS Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "case when uom <> 'SH' then  (BasicPrice_WithTax + TPT_Value) * Item_Qty else 0 end   AS PeriodNetSale, " & _
            "0 AS OPNetSale, 0 AS NetLoad, 0 AS Discount, 0 AS CreditSale, 0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, 0 AS PeriodCheckAmt, 0 AS OPCheckAmt, 0 AS PeriodEmpty_Value, " & _
            "0 AS OPEmpty_Value, 0 AS PeriodadjustEmpty, " & _
            "0 AS OPadjustEmpty, '01/09/2012' AS FDate, '26/09/2012' AS TDate, " & _
            "TSPL_TRANSFER_HEAD.FromLoc_Desc AS Location_desc, 0 AS PeriodShellamt, " & _
            "0 AS OPShellamt, 'N' AS Credit_Invoice, 0 AS PeriodBalance_Amt, " & _
            "0 AS OPBalance_Amt, 'Transfer'AS TransType, 0 AS PeriodCashSortage, " & _
            "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, 0 AS OPEmptyShortage, " & _
            "'All' AS ReportType, 0 AS PeriodDEP, 0 AS OPDEP, 0 AS PeriodRefund, " & _
            "0 AS OPRefund, 0 AS PeriodSettlement, 0 AS OPSettlement, 0 AS PeriodRetShell, 0 AS OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            " " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' " & _
            " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

            If strLocAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strRoute += "Union All "


            ''--for loadIn Period wise

            strRoute += "SELECT " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_Settlement_Date AS DocDate, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.RouteDescription AS PartyName," & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location AS Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode AS Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "case when uom <> 'SH' then  -(BasicPrice_WithTax + TPT_Value ) * (LoadIn_Qty + Leak+Breakage+burst+Shortage) else 0 end   AS PeriodNetSale, " & _
            "0 AS OPNetSale, 0 AS NetLoad, 0 AS Discount, 0 AS CreditSale, 0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, 0 AS PeriodCheckAmt, 0 AS OPCheckAmt, 0 AS PeriodEmpty_Value, " & _
            "0 AS OPEmpty_Value, 0 AS PeriodadjustEmpty, " & _
            "0 AS OPadjustEmpty, '01/09/2012' AS FDate, '26/09/2012' AS TDate, " & _
            "TSPL_TRANSFER_HEAD.ToLoc_Desc AS Location_desc, 0 AS PeriodShellamt, " & _
            "0 AS OPShellamt, 'N' AS Credit_Invoice, 0 AS PeriodBalance_Amt, " & _
            "0 AS OPBalance_Amt, 'Transfer'AS TransType, 0 AS PeriodCashSortage, " & _
            "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, 0 AS OPEmptyShortage, " & _
            "'All' AS ReportType, 0 AS PeriodDEP, 0 AS OPDEP, 0 AS PeriodRefund, " & _
            "0 AS OPRefund, 0 AS PeriodSettlement, 0 AS OPSettlement, 0 AS PeriodRetShell, 0 AS OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Load_Out_No = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date >= '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            " " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <= '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "' " & _
            " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

            If strLocAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strRoute += "Union All "


            ''--for quick settlement Adjustment In Period wise

            strRoute += "SELECT  Transfer_Date as DocDate, " & _
            "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Route_Desc as PartyName," + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_No AS DocNo," & _
            "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.From_Location as Location, " + clsCommon.ReplicateDBString + "tspl_Transfer_head.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Salesmancode as Salesman_Code," + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale,0 as OPNetSale, 0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, 0 AS OPCashAmt, 0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, 0 as PeriodEmpty_Value, 0 as OPEmpty_Value, " & _
            "case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   AS PeriodadjustEmpty, " & _
            "0 as OPadjustEmpty, '22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, 0 as PeriodShellamt, 0 as OPShellamt, " & _
            "'' as Credit_Invoice, 0 as PeriodBalance_Amt, 0 as OPBalance_Amt, " & _
            "'Transfer' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "tspl_Transfer_head ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "tspl_Transfer_head.Salesmancode RIGHT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER ON " + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Document_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No WHERE  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = 'Load out/Transfer') and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType='E' and (Adjustment_Type='BI' or " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BD') and " & _
            "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_Date >=  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "' AND " & _
            "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_Date <=  '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  and " & _
            "Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "


            If strLocAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strRoute += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If


            ''''''''QUICK SETTLEMENT OPENING BALANCE CODE START HERE



            'strQuery += "Union All "

            strRouteOpening = "SELECT  Quick_Settlement_Date as DocDate,RouteDescription as PartyName," + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location as Location, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode as Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName," & _
            "0 AS PeriodNetSale, " & _
            "0 as OPNetSale, " & _
            "0 AS NetLoad,  " & _
            "0 AS Discount, " & _
            "0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, " & _
            "CASE WHEN SettleMent_Type = 'CSH' THEN isnull(Amount,0) ELSE 0 END as OPCashAmt, " & _
            "0 AS PeriodCheckAmt, " & _
            "CASE WHEN SettleMent_Type = 'CHQ' THEN isnull(Amount,0) ELSE 0 END  as OPCheckAmt," & _
            "0 AS PeriodEmpty_Value, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Empty_Load_In as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty,0 AS OPadjustEmpty, " & _
            "'01/09/2012' AS FDate, '26/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.FromLoc_Desc as Location_desc, " & _
            "0 AS PeriodShellamt,0 as OPShellamt, " & _
            "'N' AS Credit_Invoice, " & _
            "0 AS PeriodBalance_Amt,0 as OPBalance_Amt, " & _
            "'Transfer' AS TransType, " & _
            "0 AS PeriodCashSortage,  " & _
            "0 AS PeriodEmptyShortage, " & _
            "CASE WHEN SettleMent_Type = 'CSE' THEN case  when Calculate='A' then Amount * (-1) else Amount end ELSE 0 END as OPCashSortage, " & _
            "CASE WHEN SettleMent_Type = 'ESE' THEN case  when Calculate='A' then Amount * (-1) else Amount end ELSE 0 END as OPEmptyShortage, " & _
            "'All' AS ReportType,0 as PeriodDEP, " & _
            " case when (SettleMent_Type='DEP') then isnull(Amount,0) else  0 end as OPDEP,0 as PeriodRefund, " & _
            " case when (SettleMent_Type='REF') then - Amount else  0 end as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "CASE WHEN SettleMent_Type = 'DSC' THEN isnull(Amount,0) ELSE 0 END AS OPDiscount, " & _
            "CASE WHEN SettleMent_Type = 'CRS' THEN isnull(Amount,0) ELSE 0 END AS OPCreditSale, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Amount - " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Load_In_Amount AS OPNetLoad, tspl_QuickSettleMent.Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_SettleMent_Id = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.Quick_SettleMent_Id LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "tspl_SettleMent_Master ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.SettleMent_Code = " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "WHERE  " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'" & _
            " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "


            If strLocAll = "N" Then
                strRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strRouteOpening += "Union All "

            ''--for loadout Opening wise

            strRouteOpening += "SELECT " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_Settlement_Date AS DocDate, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.RouteDescription AS PartyName," & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location AS Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode AS Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale, " & _
            "case when uom <> 'SH' then (BasicPrice_WithTax + TPT_Value) * Item_Qty else 0 end AS OPNetSale, 0 AS NetLoad, 0 AS Discount, 0 AS CreditSale, 0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, 0 AS PeriodCheckAmt, 0 AS OPCheckAmt, 0 AS PeriodEmpty_Value, " & _
            "0 AS OPEmpty_Value, 0 AS PeriodadjustEmpty, " & _
            "0 AS OPadjustEmpty, '01/09/2012' AS FDate, '26/09/2012' AS TDate, " & _
            "TSPL_TRANSFER_HEAD.FromLoc_Desc AS Location_desc, 0 AS PeriodShellamt, " & _
            "0 AS OPShellamt, 'N' AS Credit_Invoice, 0 AS PeriodBalance_Amt, " & _
            "0 AS OPBalance_Amt, 'Transfer'AS TransType, 0 AS PeriodCashSortage, " & _
            "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, 0 AS OPEmptyShortage, " & _
            "'All' AS ReportType, 0 AS PeriodDEP, 0 AS OPDEP, 0 AS PeriodRefund, " & _
            "0 AS OPRefund, 0 AS PeriodSettlement, 0 AS OPSettlement, 0 AS PeriodRetShell, 0 AS OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date < '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'  " & _
            " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

            If strLocAll = "N" Then
                strRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strRouteOpening += "Union All "


            ''--for loadIn Opening wise

            strRouteOpening += "SELECT  " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Quick_Settlement_Date AS DocDate, " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.RouteDescription AS PartyName," & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number AS DocNo, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location AS Location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode AS Salesman_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale, " & _
            "case when uom <> 'SH' then  -(BasicPrice_WithTax + TPT_Value ) * (LoadIn_Qty + Leak+Breakage+burst+Shortage) else 0 end AS OPNetSale, 0 AS NetLoad, 0 AS Discount, 0 AS CreditSale, 0 AS PeriodCashAmt, " & _
            "0 AS OPCashAmt, 0 AS PeriodCheckAmt, 0 AS OPCheckAmt, 0 AS PeriodEmpty_Value, " & _
            "0 AS OPEmpty_Value, 0 AS PeriodadjustEmpty, " & _
            "0 AS OPadjustEmpty, '01/09/2012' AS FDate, '26/09/2012' AS TDate, " & _
            "TSPL_TRANSFER_HEAD.ToLoc_Desc AS Location_desc, 0 AS PeriodShellamt, " & _
            "0 AS OPShellamt, 'N' AS Credit_Invoice, 0 AS PeriodBalance_Amt, " & _
            "0 AS OPBalance_Amt, 'Transfer'AS TransType, 0 AS PeriodCashSortage, " & _
            "0 AS PeriodEmptyShortage, 0 AS OPCashSortage, 0 AS OPEmptyShortage, " & _
            "'All' AS ReportType, 0 AS PeriodDEP, 0 AS OPDEP, 0 AS PeriodRefund, " & _
            "0 AS OPRefund, 0 AS PeriodSettlement, 0 AS OPSettlement, 0 AS PeriodRetShell, 0 AS OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "tspl_QuickSettleMent ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Load_Out_No = " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Number " & _
            "left outer join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode=" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "WHERE   " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent.Transfer_Date < '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'  " & _
            " and Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "

            If strLocAll = "N" Then
                strRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            strRouteOpening += "Union All "


            ''--for quick settlement Adjustment In Opening wise

            strRouteOpening += "SELECT  Transfer_Date as DocDate, " & _
            "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Route_Desc as PartyName," + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_No AS DocNo," & _
            "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.From_Location as Location, " + clsCommon.ReplicateDBString + "tspl_Transfer_head.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Salesmancode as Salesman_Code," + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS SalesName, " & _
            "0 AS PeriodNetSale,0 as OPNetSale, 0 AS NetLoad, " & _
            "0 AS Discount, 0 AS CreditSale, " & _
            "0 AS PeriodCashAmt, 0 AS OPCashAmt, 0  AS PeriodCheckAmt, " & _
            "0  AS OPCheckAmt, 0 as PeriodEmpty_Value, 0 as OPEmpty_Value, " & _
            "0 AS PeriodadjustEmpty, " & _
            "case when " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.trans_type='In' then isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0) else - isnull(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Item_Cost,0)  end   as OPadjustEmpty, '22/09/2012' AS FDate, '25/09/2012' AS TDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Loc_Desc, 0 as PeriodShellamt, 0 as OPShellamt, " & _
            "'' as Credit_Invoice, 0 as PeriodBalance_Amt, 0 as OPBalance_Amt, " & _
            "'Transfer' as TransType,0 as PeriodCashSortage,0 as PeriodEmptyShortage, " & _
            "0 as OPCashSortage,0 as OPEmptyShortage,'All' as ReportType, 0 as PeriodDEP, " & _
            "0 as OPDEP,0 as PeriodRefund,0 as OPRefund,0 AS PeriodSettlement,0 as OPSettlement,0 as PeriodRetShell,0 as OPRetShell, " & _
            "0 AS OPDiscount, " & _
        "0 AS OPCreditSale, " & _
        "0 AS OPNetLoad,'' as  Comments " & _
            "FROM " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "tspl_Transfer_head ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "tspl_Transfer_head.Salesmancode RIGHT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER ON " + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Document_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Adjustment_No = " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_No WHERE  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.Reference_Document = 'Load out/Transfer') and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_HEADER.ItemType='E' and (Adjustment_Type='BI' or " + clsCommon.ReplicateDBString + "TSPL_ADJUSTMENT_DETAIL.Adjustment_Type = 'BD' ) and " & _
            "" + clsCommon.ReplicateDBString + "tspl_Transfer_head.Transfer_Date <  '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") & "'  and " & _
            "Route_Type_Id <> 'F' AND Route_Type_Id <> 'S' "


            If strLocAll = "N" Then
                strRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If strRouteAll = "N" Then
                strRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strSalesAll = "N" Then
                strRouteOpening += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ") "
            End If

            ''''''''QUICK SETTLEMENT OPENING BALANCE CODE ENDS HERE

            If rdbBoth.IsChecked Then
                strQuery = strMain & strNonRoute & " Union All " & strRoute & ") a group by Route_No,TransType,SalesName,Location,Location_Desc,Salesman_Code,DocNo,PartyName,DocDate"
                strOpening = strMainopening & strNonRouteOpening & " Union All " & strRouteOpening & " ) a group by Route_No,TransType,SalesName,Location,Location_Desc,Salesman_Code,DocNo,PartyName,DocDate"
            ElseIf rdbRouteWise.IsChecked Then
                strQuery = strMain & strRoute & " ) a group by Route_No,TransType,SalesName,Location,Location_Desc,Salesman_Code,DocNo,PartyName,DocDate"
                strOpening = strMainopening & strRouteOpening & " ) a group by Route_No,TransType,SalesName,Location,Location_Desc,Salesman_Code,DocNo,PartyName,DocDate"
            ElseIf rdnNonRoutewise.IsChecked Then
                strQuery = strMain & strNonRoute & " ) a group by Route_No,TransType,SalesName,Location,Location_Desc,Salesman_Code,DocNo,PartyName,DocDate"
                strOpening = strMainopening & strNonRouteOpening & " ) a group by Route_No,TransType,SalesName,Location,Location_Desc,Salesman_Code,DocNo,PartyName,DocDate"
            End If
           

            Dim strEmp As String = "select (Cash +  Empty_Ex) as OPBalance,EMP_CODE as payrollcode from " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER  where (Cash <> 0 or Empty_Ex <> 0) "

            If strSalesAll = "N" Then
                strEmp += " and " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE in (" + clsCommon.GetMulcallString(cbgSalesPerson.CheckedValue) + ")  "

            End If

            strFinalOpening = "Select  SUM(OPEmptyShortage) + SUM(OPcashShortage) as OPBalance,PayrollCode from " & _
             "(  " & strOpening & "  ) A group by PayrollCode " & _
             " union all " & _
             " " & strEmp & " "


            strOpening = " select SUM (OPBalance) as OPBalance,PayrollCode  as Salesman_Code from ( " & strFinalOpening & "  ) C group by payrollcode"


            If chkLocation.Checked = True Then
                strSummary += "select DocNo,DocDate,PartyName,Location_Desc as Location, SalesName,PayrollCode, " & _
                "SUM(OPcashShortage) as OPcashShortage, " & _
              "SUM(OPEmptyShortage) as OPEmptyShortage, " & _
              "SUM(OPEmptyShortage) + SUM(OPcashShortage) AS OPBalance, " & _
              "SUM(PercashShortage) as PercashShortage, " & _
              "SUM(PerEmptyShortage) as PerEmptyShortage, " & _
              "SUM(PercashShortage) + SUM(PerEmptyShortage) as PerBalance, " & _
              "SUM(OPcashShortage) + SUM(PercashShortage) as ClosingCashShortage, " & _
              "SUM(OPEmptyShortage) + SUM(PerEmptyShortage) as ClosingEmptyShortage, " & _
              "SUM(OPcashShortage) + SUM(PercashShortage) + SUM(OPEmptyShortage) + SUM(PerEmptyShortage) as ClosingBalance from " & _
              "(  " & strQuery & "  )xxx  group by SalesName,PayrollCode,DocNo,DocDate,PartyName,Location_Desc "
            Else
                strSummary += "select DocNo,DocDate,PartyName, SalesName,PayrollCode, " & _
                "SUM(OPcashShortage) as OPcashShortage, " & _
              "SUM(OPEmptyShortage) as OPEmptyShortage, " & _
              "SUM(OPEmptyShortage) + SUM(OPcashShortage) AS OPBalance, " & _
              "SUM(PercashShortage) as PercashShortage, " & _
              "SUM(PerEmptyShortage) as PerEmptyShortage, " & _
              "SUM(PercashShortage) + SUM(PerEmptyShortage) as PerBalance, " & _
              "SUM(OPcashShortage) + SUM(PercashShortage) as ClosingCashShortage, " & _
              "SUM(OPEmptyShortage) + SUM(PerEmptyShortage) as ClosingEmptyShortage, " & _
              "SUM(OPcashShortage) + SUM(PercashShortage) + SUM(OPEmptyShortage) + SUM(PerEmptyShortage) as ClosingBalance from " & _
              "(  " & strQuery & "  )xxx  group by SalesName,PayrollCode,DocNo,DocDate,PartyName "
            End If
            strQuery = strSummary

        End If

        Dim ArrDBName As ArrayList = Nothing
        Dim ArrOPBal As New Dictionary(Of String, Double)
        Dim dt, dt1 As New DataTable
        Dim dtFinal As DataTable = Nothing
        If rbtnCompanyAll.IsChecked Then
            ArrDBName = cbgCompany.AllValue
        Else
            ArrDBName = cbgCompany.CheckedValue
        End If
        strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQuery, ArrDBName, False)


        If rdbSalesmanSummary.IsChecked Then
            dt = clsDBFuncationality.GetDataTable(strQuery + " order by PayrollCode,DocDate ")

            dt.Columns.Remove("OPBalance")
            'dt.Columns.Add("OPBalance", GetType(Double))
            dt.Columns.Add("OPBalance", GetType(Double)).SetOrdinal(5)
            strOpening = clsCommon.GetQueryWithAllSelectedDataBase(strOpening, ArrDBName, False)

            strOpening = "select sum(OPBalance) as OPBalance, Salesman_Code from ( " & strOpening & " ) D group by Salesman_Code"
            dt1 = clsDBFuncationality.GetDataTable(strOpening)

            For Each dr As DataRow In dt1.Rows
                'Dim str As String = clsCommon.myCstr(dr("Salesman_Code"))
                ArrOPBal.Add(clsCommon.myCstr(dr("Salesman_Code")), clsCommon.myCdbl(dr("OPBalance")))
            Next
            dtFinal = dt.Clone()
            Dim strSaleManCode As String = ""
            Dim dblOPBal As Double = 0
            Dim strSalesname As String

            If dt.Rows.Count <> 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim drFianl As DataRow = dtFinal.NewRow()
                    If Not clsCommon.CompairString(strSaleManCode, clsCommon.myCstr(dr("PayrollCode"))) = CompairStringResult.Equal Then
                        dblOPBal = 0
                        strSaleManCode = clsCommon.myCstr(dr("PayrollCode"))
                        If ArrOPBal.ContainsKey(strSaleManCode) Then
                            dblOPBal = ArrOPBal(strSaleManCode)
                        End If
                    End If
                    drFianl("DocNo") = dr("DocNo")
                    drFianl("DocDate") = dr("DocDate")
                    drFianl("PartyName") = dr("PartyName")
                    If chkLocation.Checked = True Then
                        drFianl("Location") = dr("Location")
                    End If
                    drFianl("SalesName") = dr("SalesName")
                    drFianl("PayrollCode") = dr("PayrollCode")
                    drFianl("OPcashShortage") = dr("OPcashShortage")
                    drFianl("OPEmptyShortage") = dr("OPEmptyShortage")
                    drFianl("OPBalance") = dblOPBal
                    drFianl("PercashShortage") = dr("PercashShortage")
                    drFianl("PerEmptyShortage") = dr("PerEmptyShortage")
                    drFianl("PerBalance") = dr("PerBalance")
                    drFianl("ClosingCashShortage") = dr("ClosingCashShortage")
                    drFianl("ClosingEmptyShortage") = dr("ClosingEmptyShortage")
                    dblOPBal += clsCommon.myCdbl(dr("PerEmptyShortage")) + clsCommon.myCdbl(dr("PercashShortage"))
                    drFianl("ClosingBalance") = dblOPBal
                    dtFinal.Rows.Add(drFianl)

                    strSaleManCode = clsCommon.myCstr(dr("PayrollCode"))
                Next
            Else
                If dt1.Rows.Count > 0 Then

                    For Each dr As DataRow In dt1.Rows
                        Dim drFianl As DataRow = dtFinal.NewRow()
                        dblOPBal = 0
                        strSaleManCode = clsCommon.myCstr(dr("Salesman_Code"))
                        If ArrOPBal.ContainsKey(strSaleManCode) Then
                            dblOPBal = ArrOPBal(strSaleManCode)
                        End If
                        drFianl("PayrollCode") = strSaleManCode
                        strSalesname = clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" & strSaleManCode & "'")
                        drFianl("SalesName") = strSalesname
                        drFianl("OPBalance") = dblOPBal
                        drFianl("PercashShortage") = 0
                        drFianl("PerEmptyShortage") = 0

                        drFianl("OPBalance") = dblOPBal
                        'dblOPBal += clsCommon.myCdbl(dr("PerEmptyShortage")) + clsCommon.myCdbl(dr("PercashShortage"))
                        drFianl("ClosingBalance") = dblOPBal
                        dtFinal.Rows.Add(drFianl)

                        strSaleManCode = clsCommon.myCstr(dr("Salesman_Code"))
                    Next
                End If
            End If
        ElseIf rdbSummary.IsChecked Then

            Dim strSql As String

            If chkLocation.Checked = True Then
                strSql = "select SalesName,PayrollCode,Location, " & _
                "SUM(OPcashShortage) as OPcashShortage, " & _
                "SUM(OPEmptyShortage) as OPEmptyShortage, " & _
                "SUM(OPBalance) AS OPBalance, " & _
                "SUM(PercashShortage) as PercashShortage, " & _
                "SUM(PerEmptyShortage) as PerEmptyShortage, " & _
                "SUM(PerBalance) as PerBalance, " & _
                "SUM(ClosingCashShortage) as ClosingCashShortage, " & _
                "SUM(ClosingEmptyShortage) as ClosingEmptyShortage, " & _
                "SUM(ClosingBalance) as ClosingBalance from " & _
                "(  " & strQuery & ")xxx  group by SalesName,PayrollCode,Location"
            Else
                strSql = "select SalesName,PayrollCode, " & _
                                "SUM(OPcashShortage) as OPcashShortage, " & _
                                "SUM(OPEmptyShortage) as OPEmptyShortage, " & _
                                "SUM(OPBalance) AS OPBalance, " & _
                                "SUM(PercashShortage) as PercashShortage, " & _
                                "SUM(PerEmptyShortage) as PerEmptyShortage, " & _
                                "SUM(PerBalance) as PerBalance, " & _
                                "SUM(ClosingCashShortage) as ClosingCashShortage, " & _
                                "SUM(ClosingEmptyShortage) as ClosingEmptyShortage, " & _
                                "SUM(ClosingBalance) as ClosingBalance from " & _
                                "(  " & strQuery & ")xxx  group by SalesName,PayrollCode"
            End If
            strQuery = strSql

            dtFinal = clsDBFuncationality.GetDataTable(strQuery)

        ElseIf rdbDetail.IsChecked Then
            dtFinal = clsDBFuncationality.GetDataTable(strQuery)
        End If

        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.EnableFiltering = True

        If dtFinal Is Nothing OrElse dtFinal.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            gv1.DataSource = dtFinal
            SetGridFormationOFGV1()
        End If

        gv1.MasterTemplate.AllowAddNewRow = False
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Sub SetGridFormationOFGV1()
        gv1.ShowGroupPanel = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            If rdbDetail.IsChecked Then
                gv1.Columns(ii).IsVisible = False
            Else
                gv1.Columns(ii).Width = 100
            End If
        Next
        If rdbDetail.IsChecked Then

            gv1.Columns("DocNo").IsVisible = True
            gv1.Columns("DocNo").Width = 100
            gv1.Columns("DocNo").HeaderText = "Document No"

            gv1.Columns("DocDate").IsVisible = True
            gv1.Columns("DocDate").Width = 100
            gv1.Columns("DocDate").HeaderText = "Date"

            gv1.Columns("PartyName").IsVisible = True
            gv1.Columns("PartyName").Width = 100
            gv1.Columns("PartyName").HeaderText = "Party"

            gv1.Columns("SalesName").IsVisible = True
            gv1.Columns("SalesName").Width = 100
            gv1.Columns("SalesName").HeaderText = "SalesName"

            gv1.Columns("CustClass").IsVisible = True
            gv1.Columns("CustClass").Width = 100
            gv1.Columns("CustClass").HeaderText = "Customer Class"


            gv1.Columns("Location").IsVisible = True
            gv1.Columns("Location").Width = 100
            gv1.Columns("Location").HeaderText = "Location"


            gv1.Columns("PayrollCode").IsVisible = True
            gv1.Columns("PayrollCode").Width = 100
            gv1.Columns("PayrollCode").HeaderText = "Payroll Code"

            gv1.Columns("NetLoad").IsVisible = True
            gv1.Columns("NetLoad").Width = 100
            gv1.Columns("NetLoad").HeaderText = "NetLoad"

            gv1.Columns("NetSale").IsVisible = True
            gv1.Columns("NetSale").Width = 100
            gv1.Columns("NetSale").HeaderText = "NetSale"

            gv1.Columns("Discount").IsVisible = True
            gv1.Columns("Discount").Width = 100
            gv1.Columns("Discount").HeaderText = "Discount"

            gv1.Columns("CreditSale").IsVisible = True
            gv1.Columns("CreditSale").Width = 100
            gv1.Columns("CreditSale").HeaderText = "Credit Sale"

            gv1.Columns("Settlement").IsVisible = True
            gv1.Columns("Settlement").Width = 100
            gv1.Columns("Settlement").HeaderText = "Settlement"

            gv1.Columns("PeriodDepRefund").IsVisible = True
            gv1.Columns("PeriodDepRefund").Width = 100
            gv1.Columns("PeriodDepRefund").HeaderText = "DEP/REF"

            gv1.Columns("CashAmt").IsVisible = True
            gv1.Columns("CashAmt").Width = 100
            gv1.Columns("CashAmt").HeaderText = "CashAmt"

            gv1.Columns("CheckAmt").IsVisible = True
            gv1.Columns("CheckAmt").Width = 100
            gv1.Columns("CheckAmt").HeaderText = "CheckAmt"

            gv1.Columns("CheckAmt").IsVisible = True
            gv1.Columns("CheckAmt").Width = 100
            gv1.Columns("CheckAmt").HeaderText = "CheckAmt"

            gv1.Columns("EmptyIn").IsVisible = True
            gv1.Columns("EmptyIn").Width = 100
            gv1.Columns("EmptyIn").HeaderText = "EmptyIn"

            gv1.Columns("PercashShortage").IsVisible = True
            gv1.Columns("PercashShortage").Width = 100
            gv1.Columns("PercashShortage").HeaderText = "Cash Sh/Ex"

            gv1.Columns("PerEmptyShortage").IsVisible = True
            gv1.Columns("PerEmptyShortage").Width = 100
            gv1.Columns("PerEmptyShortage").HeaderText = "Empty Sh/Ex"

            gv1.Columns("PerBalance").IsVisible = True
            gv1.Columns("PerBalance").Width = 100
            gv1.Columns("PerBalance").HeaderText = "Total Sh/Ex"

            gv1.Columns("Comments").IsVisible = True
            gv1.Columns("Comments").Width = 100
            gv1.Columns("Comments").HeaderText = "Comments"

            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim item1 As New GridViewSummaryItem("NetLoad", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("NetSale", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Discount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("CreditSale", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("CashAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("CheckAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("PercashShortage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("PerEmptyShortage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            Dim item9 As New GridViewSummaryItem("PerBalance", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)
            Dim item10 As New GridViewSummaryItem("Settlement", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)
            Dim item11 As New GridViewSummaryItem("PeriodDepRefund", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item11)
            Dim item12 As New GridViewSummaryItem("EmptyIn", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item12)

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf rdbSummary.IsChecked Then

            gv1.Columns("SalesName").IsVisible = True
            gv1.Columns("SalesName").Width = 100
            gv1.Columns("SalesName").HeaderText = "SalesName"


            gv1.Columns("PayrollCode").IsVisible = True
            gv1.Columns("PayrollCode").Width = 100
            gv1.Columns("PayrollCode").HeaderText = "Payroll Code"

            If chkLocation.Checked = True Then
                gv1.Columns("Location").IsVisible = True
                gv1.Columns("Location").Width = 100
                gv1.Columns("Location").HeaderText = "Location"
            End If

            gv1.Columns("OPcashShortage").IsVisible = True
            gv1.Columns("OPcashShortage").Width = 100
            gv1.Columns("OPcashShortage").HeaderText = "Opening Cash Sh/Ex"

            gv1.Columns("OPEmptyShortage").IsVisible = True
            gv1.Columns("OPEmptyShortage").Width = 100
            gv1.Columns("OPEmptyShortage").HeaderText = "Opening Empty Sh/Ex"

            gv1.Columns("OPBalance").IsVisible = True
            gv1.Columns("OPBalance").Width = 100
            gv1.Columns("OPBalance").HeaderText = "Opening Total Sh/Ex"

            gv1.Columns("PercashShortage").IsVisible = True
            gv1.Columns("PercashShortage").Width = 100
            gv1.Columns("PercashShortage").HeaderText = "Period Cash Sh/Ex"

            gv1.Columns("PerEmptyShortage").IsVisible = True
            gv1.Columns("PerEmptyShortage").Width = 100
            gv1.Columns("PerEmptyShortage").HeaderText = "Period Empty Sh/Ex"

            gv1.Columns("PerBalance").IsVisible = True
            gv1.Columns("PerBalance").Width = 100
            gv1.Columns("PerBalance").HeaderText = "Period Total Sh/Ex"

            gv1.Columns("ClosingCashShortage").IsVisible = True
            gv1.Columns("ClosingCashShortage").Width = 100
            gv1.Columns("ClosingCashShortage").HeaderText = "Closing Cash Sh/Ex"

            gv1.Columns("ClosingEmptyShortage").IsVisible = True
            gv1.Columns("ClosingEmptyShortage").Width = 100
            gv1.Columns("ClosingEmptyShortage").HeaderText = "Closing Empty Sh/Ex"

            gv1.Columns("ClosingBalance").IsVisible = True
            gv1.Columns("ClosingBalance").Width = 100
            gv1.Columns("ClosingBalance").HeaderText = "Closing Total Sh/Ex"


            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim item1 As New GridViewSummaryItem("OPcashShortage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("OPEmptyShortage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("OPBalance", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("PercashShortage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("PerEmptyShortage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("PerBalance", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("ClosingCashShortage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("ClosingEmptyShortage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            Dim item9 As New GridViewSummaryItem("ClosingBalance", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item9)


            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        ElseIf rdbSalesmanSummary.IsChecked Then


            gv1.Columns("DocNo").IsVisible = True
            gv1.Columns("DocNo").Width = 100
            gv1.Columns("DocNo").HeaderText = "Document No"

            gv1.Columns("DocDate").IsVisible = True
            gv1.Columns("DocDate").Width = 100
            gv1.Columns("DocDate").HeaderText = "Date"

            gv1.Columns("PartyName").IsVisible = True
            gv1.Columns("PartyName").Width = 100
            gv1.Columns("PartyName").HeaderText = "Party"

            If chkLocation.Checked = True Then
                gv1.Columns("Location").IsVisible = True
                gv1.Columns("Location").Width = 100
                gv1.Columns("Location").HeaderText = "Location"
            End If

            gv1.Columns("SalesName").IsVisible = True
            gv1.Columns("SalesName").Width = 100
            gv1.Columns("SalesName").HeaderText = "SalesName"


            gv1.Columns("OPcashShortage").IsVisible = False
            gv1.Columns("OPcashShortage").Width = 100
            gv1.Columns("OPcashShortage").HeaderText = "Opening Cash Sh/Ex"

            gv1.Columns("OPEmptyShortage").IsVisible = False
            gv1.Columns("OPEmptyShortage").Width = 100
            gv1.Columns("OPEmptyShortage").HeaderText = "Opening Empty Sh/Ex"

            gv1.Columns("OPBalance").IsVisible = True
            gv1.Columns("OPBalance").Width = 100
            gv1.Columns("OPBalance").HeaderText = "Opening Total Sh/Ex"

            gv1.Columns("PercashShortage").IsVisible = True
            gv1.Columns("PercashShortage").Width = 100
            gv1.Columns("PercashShortage").HeaderText = "Period Cash Sh/Ex"

            gv1.Columns("PerEmptyShortage").IsVisible = True
            gv1.Columns("PerEmptyShortage").Width = 100
            gv1.Columns("PerEmptyShortage").HeaderText = "Period Empty Sh/Ex"

            gv1.Columns("PerBalance").IsVisible = True
            gv1.Columns("PerBalance").Width = 100
            gv1.Columns("PerBalance").HeaderText = "Period Total Sh/Ex"

            gv1.Columns("ClosingCashShortage").IsVisible = False
            gv1.Columns("ClosingCashShortage").Width = 100
            gv1.Columns("ClosingCashShortage").HeaderText = "Closing Cash Sh/Ex"

            gv1.Columns("ClosingEmptyShortage").IsVisible = False
            gv1.Columns("ClosingEmptyShortage").Width = 100
            gv1.Columns("ClosingEmptyShortage").HeaderText = "Closing Empty Sh/Ex"

            gv1.Columns("ClosingBalance").IsVisible = True
            gv1.Columns("ClosingBalance").Width = 100
            gv1.Columns("ClosingBalance").HeaderText = "Cumulative Sh/Ex"


            Dim summaryRowItem As New GridViewSummaryRowItem()

            'Dim item1 As New GridViewSummaryItem("OPcashShortage", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item1)
            'Dim item2 As New GridViewSummaryItem("OPEmptyShortage", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item2)
            'Dim item3 As New GridViewSummaryItem("OPBalance", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("PercashShortage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("PerEmptyShortage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("PerBalance", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("ClosingCashShortage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item8 As New GridViewSummaryItem("ClosingEmptyShortage", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)
            'Dim item9 As New GridViewSummaryItem("ClosingBalance", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item9)

            gv1.GroupDescriptors.Add(New GridGroupByExpression("PayrollCode as SalesName format ""{0}: {1}"" Group By PayrollCode"))
            gv1.MasterTemplate.ExpandAllGroups()
            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If
    End Sub

    Sub PrintProvisionalReport()
        Dim StrQ As String = " SELECT  M.RouteNo, M.Quick_SettleMent_Id,M.Transfer_Date as [Settlement Date], M.Salesman, M.Transfer_Amount as [LoadOutAmt], M.Load_In_Amount as [LoadInAmt], M.Empty_Load_In  as [EmptyAmt], M.Transfer_Amount - M.Load_In_Amount - M.Empty_Load_In AS NetLoad, isnull((select SUM( Amount)  from " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail  INNER JOIN" & _
                             " " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.SettleMent_Code = " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode where " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMent_Type='DSC' and " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.Quick_SettleMent_Id=M.Quick_SettleMent_Id  ),0) as [Discount], isnull((select SUM( Amount)  from " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail  INNER JOIN " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.SettleMent_Code = " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode where " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMent_Type='CRS' and " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.Quick_SettleMent_Id=M.Quick_SettleMent_Id ),0) as [CreditSale], isnull((select SUM( Amount)  from " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail  INNER JOIN " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.SettleMent_Code = " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode where " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMent_Type='' and " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.Quick_SettleMent_Id=M.Quick_SettleMent_Id ),0) as [DEP/REF], isnull((select SUM( Amount)  from " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail  INNER JOIN " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.SettleMent_Code = " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode where " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMent_Type='CSH' and " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.Quick_SettleMent_Id=M.Quick_SettleMent_Id ),0) as [Cash], isnull((select SUM( Amount)  from " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail  INNER JOIN" & _
                             " " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master ON " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.SettleMent_Code = " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMentCode where " + clsCommon.ReplicateDBString + "tspl_SettleMent_Master.SettleMent_Type='CHQ' and " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail.Quick_SettleMent_Id=M.Quick_SettleMent_Id ),0) as [CHEQUE],M.Transfer_Number as [LoadOutNo] " & _
                             " FROM  " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent AS M left outer JOIN " + clsCommon.ReplicateDBString + "tspl_QuickSettleMent_Detail AS L ON M.Quick_SettleMent_Id = L.Quick_SettleMent_Id where  (M.Transfer_Date >=convert(date,'" + fromDate.Value + "',103) and M.Transfer_Date <= convert(date,'" + ToDate.Value + "',103))  "

        If chkRouteSelect.IsChecked Then
            If cbgRoute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One Route Code.")
                Return
            End If
            StrQ += " and M.RouteNo in   (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")  "
        End If

        If chkLocationSelect.IsChecked Then
            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One Location.")
                Return
            End If
            StrQ += " and (select Loc_Segment_Code  from " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER as LM where LM.Location_Code=(select T.From_Location  from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD as T where T.Transfer_No=M.Transfer_Number   )) in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  "
        End If


        Dim ArrDBName As ArrayList = Nothing
        Dim dt As New DataTable

        If rbtnCompanySelect.IsChecked Then
            If cbgCompany.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast One Company.")
                Return
            End If
            ArrDBName = cbgCompany.CheckedValue
        ElseIf rbtnCompanyAll.IsChecked Then
            ArrDBName = cbgCompany.AllValue
        End If
        StrQ = clsCommon.GetQueryWithAllSelectedDataBase(StrQ, ArrDBName, False)

        Dim FinalQuery As String = "select MAX(Query.RouteNo) as [RouteNo],MAX(Query.Salesman )as [SalesMan], MAX(Query.Quick_SettleMent_Id  ) as [Settlement Id],MAX(Query.[Settlement Date] ) as [Settlement Date],LoadOutNo as [Loadout No],MAX(Query.LoadOutAmt) as [LoadOut Amount],MAX(Query.EmptyAmt )as [Empty Amount],MAX(Query.LoadInAmt ) as [LoadIn Amount],MAX(Query.NetLoad ) as [NetLoad],MAX(Query.Discount  ) as [Discount],MAX(Query.CreditSale  ) as [Credit Sale],MAX(Query.[DEP/REF] ) as [DEP/REF],MAX(Query.Cash  ) as [Cash],MAX(Query.CHEQUE  ) as [Cheque],0 as [Total SH/EX],(MAX(Query.NetLoad )-MAX(Query.Discount  )-MAX(Query.CreditSale  )-MAX(Query.Cash  )-MAX(Query.CHEQUE  ))as [Balance] from ( " + StrQ + " ) as Query  group by Query.LoadOutNo "

        transportSql.OpenExporttoExcel(FinalQuery, Me)

    End Sub
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Print()
    End Sub
    Sub Print()
        If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            Return
        ElseIf chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Route or select ALL")
            Return
        End If
        If strReportType = "Provisional" Then
            PrintProvisionalReport()
        Else
            PrintActualReport()
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ChkSalesAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkSalesAll.ToggleStateChanged
        cbgSalesPerson.Enabled = Not ChkSalesAll.IsChecked
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'Try
        '    PrintActualReport()
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
        '    arrHeader.Add(strtemp)

        '    If chkRouteSelect.IsChecked Then
        '        strtemp = ""
        '        For Each Str As String In cbgRoute.CheckedDisplayMember
        '            If clsCommon.myLen(strtemp) > 0 Then
        '                strtemp += ", "
        '            End If
        '            strtemp += Str
        '        Next
        '        arrHeader.Add("Route : " + strtemp)
        '    End If
        '    If chlSalesSelect.IsChecked Then
        '        strtemp = ""
        '        For Each Str As String In cbgSalesPerson.CheckedDisplayMember
        '            If clsCommon.myLen(strtemp) > 0 Then
        '                strtemp += ", "
        '            End If
        '            strtemp += Str
        '        Next
        '        arrHeader.Add("Salesname : " + strtemp)
        '    End If

        '    If chkLocationSelect.IsChecked Then
        '        strtemp = ""
        '        For Each Str As String In cbgLocation.CheckedDisplayMember
        '            If clsCommon.myLen(strtemp) > 0 Then
        '                strtemp += ", "
        '            End If
        '            strtemp += Str
        '        Next
        '        arrHeader.Add("Location Segment : " + strtemp)
        '    End If


        '    'If rbtnCompanySelect.IsChecked Then
        '    '    strtemp = ""
        '    '    For Each Str As String In cbgCompany.CheckedDisplayMember
        '    '        If clsCommon.myLen(strtemp) > 0 Then
        '    '            strtemp += ", "
        '    '        End If
        '    '        strtemp += Str
        '    '    Next
        '    '    arrHeader.Add("Company : " + strtemp)
        '    'End If
        '    clsCommon.MyExportToExcel("Salesman Shortage" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), gv1, arrHeader, Me.Text)
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        'End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        PrintActualReport()
    End Sub

    Public Sub printdata(ByVal exporter As EnumExportTo)
        Try
            PrintActualReport()
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)

            If chkRouteSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgRoute.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Route : " + strtemp)
            End If
            If chlSalesSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgSalesPerson.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Salesname : " + strtemp)
            End If

            If chkLocationSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location Segment : " + strtemp)
            End If


            'If rbtnCompanySelect.IsChecked Then
            '    strtemp = ""
            '    For Each Str As String In cbgCompany.CheckedDisplayMember
            '        If clsCommon.myLen(strtemp) > 0 Then
            '            strtemp += ", "
            '        End If
            '        strtemp += Str
            '    Next
            '    arrHeader.Add("Company : " + strtemp)
            'End If
           ' clsCommon.MyExportToExcel("Salesman Shortage" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), gv1, arrHeader, Me.Text)

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Salesman Shortage" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Salesman Shortage" + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), gv1, arrHeader, "Salesman Shortage Report", True)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        printdata(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        printdata(EnumExportTo.PDF)
    End Sub
End Class
