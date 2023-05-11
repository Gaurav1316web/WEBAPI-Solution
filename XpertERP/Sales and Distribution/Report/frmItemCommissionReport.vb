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
Public Class FrmItemCommissionReport
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery As String
    Dim dr As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        dr = clsDBFuncationality.GetDataTable(sql)
        For Each row As DataRow In dr.Rows
            l1User = row(0).ToString()
            l2User = row(1).ToString()
            l3User = row(2).ToString()
            l4User = row(3).ToString()
            l5User = row(4).ToString()
        Next
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt("ITM-COMM-RPT")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmItemCommissionReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
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


    Private Sub FrmItemCommissionReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print ")



    End Sub
    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged, chkItemSelect.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
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
    Sub LoadItem()
        ''Dim qry As String = "select distinct Item_Code as [Item Code],Scheme_Applicable as [Scheme Applicable] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Item_Code,Item_Desc from TSPL_ITEM_MASTER"
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item_Code"
        cbgItem.DisplayMember = "Scheme Applicable"
    End Sub

    '''' added by priti to add RefDoctype dropdown

    Sub LoadHRType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "HOS"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "2"
        dr("Name") = "TDM"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "3"
        dr("Name") = "ADC"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "4"
        dr("Name") = "CE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "5"
        dr("Name") = "SalesMan"
        dt.Rows.Add(dr)

        cmbType.DataSource = dt
        cmbType.ValueMember = "Code"
        cmbType.DisplayMember = "Name"

    End Sub
    '' ends here
    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged, rbtnCompanySelect.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()
    End Sub
    Sub print()
        If rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one company")
            Return
        End If

        If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least Item or select ALL")
            Return
        End If

        strQuery = "SELECT " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level1_User_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level2_User_Code, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level3_User_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level4_User_Code, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level5_User_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level1_User_Comm_Amount, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level2_User_Comm_Amount, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level3_User_Comm_Amount, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level4_User_Comm_Amount, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level5_User_Comm_Amount,  " & _
        "convert(varchar(10)," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) AS Sale_Invoice_Date, TSPL_EMPLOYEE_MASTER.Emp_Name AS Emp1," & _
        "ltrim(TSPL_EMPLOYEE_MASTER_1.Emp_Name) AS Emp2, TSPL_EMPLOYEE_MASTER_2.Emp_Name AS Emp3," & _
        "TSPL_EMPLOYEE_MASTER_3.Emp_Name AS Emp4, TSPL_EMPLOYEE_MASTER_4.Emp_Name AS Emp5 " & _
        "FROM " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL INNER JOIN " & _
        " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No INNER JOIN " & _
        " " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level1_User_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " & _
        " TSPL_EMPLOYEE_MASTER AS TSPL_EMPLOYEE_MASTER_1 ON  " & _
        " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level2_User_Code = TSPL_EMPLOYEE_MASTER_1.EMP_CODE INNER JOIN " & _
        " TSPL_EMPLOYEE_MASTER AS TSPL_EMPLOYEE_MASTER_2 ON " & _
        " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level3_User_Code =TSPL_EMPLOYEE_MASTER_2.EMP_CODE INNER JOIN " & _
        " TSPL_EMPLOYEE_MASTER AS TSPL_EMPLOYEE_MASTER_3 ON  " & _
        " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level4_User_Code = TSPL_EMPLOYEE_MASTER_3.EMP_CODE INNER JOIN " & _
        " TSPL_EMPLOYEE_MASTER AS TSPL_EMPLOYEE_MASTER_4 ON  " & _
        " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Level5_User_Code = TSPL_EMPLOYEE_MASTER_4.EMP_CODE  where " & _
        " convert(DATE," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) > =  CONVERT(DATE,'" & FromDate.Value & "',103) and  " & _
        " convert(DATE," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= CONVERT(DATE,'" & ToDate.Value & "',103) "
        Dim strItemAll As String
        If chkItemAll.IsChecked = True Then
            strItemAll = "Y"
        Else
            strItemAll = "N"
        End If

        If strItemAll = "Y" Then

        Else
            strQuery += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ") "
        End If
        Dim ArrDBName As ArrayList = Nothing
        If rbtnCompanyAll.IsChecked Then
            ArrDBName = cbgCompany.AllValue
        Else
            ArrDBName = cbgCompany.CheckedValue
        End If
        strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQuery, ArrDBName, False)

        If chkSummary.IsChecked = True Then
            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptItemCommissionSummary", "Item Commission Report")
        Else
            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptItemCommissionDetail", "Item Commission Report")
        End If

    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ITM-COMM-RPT"
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

    '''' Added by-----Pankaj Kumar----on----01/03/2012(Thursday)
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Public Sub Reset()
        FromDate.Value = clsCommon.GETSERVERDATE
        ToDate.Value = clsCommon.GETSERVERDATE
        chkDetail.IsChecked = True
        LoadItem()
        chkItemAll.IsChecked = True
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        LoadHRType()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub
    '-----------Code ends Here----------
End Class
