Imports common
Imports XpertERPEngine
Imports System.Data.SqlClient
Public Class FrmRptCustomerRouteHistory
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub





    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.CustomerRouteHistoryReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
           
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmRptCustomerRouteHistory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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



    Private Sub FrmRptCustomerRouteHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'AddHandler fndCustomerCode.txtValue.TextChanged, AddressOf fndCustomerCode_TextChanged
        'AddHandler fndCustomerCode.txtValue.KeyPress, AddressOf fndCustomerCode_KeyPress
        'AddHandler fndCustomerCode.txtValue.Leave, AddressOf fndCustomerCode_TextLeave
        dtpFromDate.Value = Date.Today()
        dtpToDate.Value = Date.Today()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    'Private Sub fndCustomerCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    fndCustomerCode.ConnectionString = connectSql.SqlCon()
    '    fndCustomerCode.Query = "select distinct Cust_Code as [Customer Code],Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER_HISTORY"
    '    fndCustomerCode.ValueToSelect = "Customer Code"
    '    fndCustomerCode.Caption = "Customer Master History"
    '    fndCustomerCode.txtValue.MaxLength = 50
    '    fndCustomerCode.ValueToSelect1 = "Customer Name"
    'End Sub
    'Public Sub fndCustomerCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim strCust_Code As String = "select distinct Cust_Code  from TSPL_CUSTOMER_MASTER_HISTORY where Cust_Code='" + fndCustomerCode.txtValue.Text + "'"
    '        'Dim dr As SqlDataReader
    '        'dr = connectSql.RunSqlReturnDR(strCust_Code)
    '        'Dim strvalue As String
    '        'If dr.Read() Then
    '        '    strvalue = dr(0).ToString()
    '        'End If
    '        Dim strvalue As String = clsDBFuncationality.getSingleValue(strCust_Code)

    '        If (strvalue <> "") Then
    '            txtCustomerName.Text = clsDBFuncationality.getSingleValue("select distinct Customer_Name  from TSPL_CUSTOMER_MASTER_HISTORY where Cust_Code='" + fndCustomerCode.txtValue.Text + "'")
    '        Else
    '            txtCustomerName.Text = ""
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub
    'Private Sub fndCustomerCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    fndCustomerCode.txtValue.CharacterCasing = CharacterCasing.Upper
    '    If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
    '        e.Handled = True
    '    End If
    'End Sub
    'Private Sub fndCustomerCode_TextLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndCustomerCode.txtValue.Text = "" Then
    '    Else
    '        Dim strcust_Code As String = "select distinct Cust_Code  from TSPL_CUSTOMER_MASTER_HISTORY where Cust_Code='" + fndCustomerCode.txtValue.Text + "'"
    '        'Dim dr As SqlDataReader
    '        'dr = connectSql.RunSqlReturnDR(strcust_Code)
    '        'Dim strvalue As String
    '        'If dr.Read() Then
    '        '    strvalue = dr(0).ToString()
    '        'End If
    '        Dim strvalue As String = clsDBFuncationality.getSingleValue(strcust_Code)
    '        If strvalue <> "" Then
    '        Else : strcust_Code = ""
    '            common.clsCommon.MyMessageBoxShow("Customer Code does not exist in table")
    '            txtCustomerName.Text = ""
    '            fndCustomerCode.Value = ""
    '            fndCustomerCode.Focus()
    '        End If
    '    End If
    'End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()
    End Sub
    Sub print()
        Try
            'dtpToDate.MinDate = dtpFromDate.Value
            FrmProvionalSalesReport.proShowReport("Customer Route History", fndCustomerCode.Value, dtpFromDate.Value, dtpToDate.Value)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        dtpFromDate.Value = Date.Today()
        dtpToDate.Value = Date.Today()
        fndCustomerCode.Value = ""
        txtCustomerName.Text = ""
    End Sub


    ''This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CUST-RHIS"
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

   
    Private Sub fndCustomerCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustomerCode._MYValidating
        Dim qry As String = "select distinct Cust_Code as [Customer Code],Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER_HISTORY"
        fndCustomerCode.Value = clsCommon.ShowSelectForm("fndCustRouteHis", qry, "Customer Code", "", fndCustomerCode.Value, "", isButtonClicked)
        txtCustomerName.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER_HISTORY where Cust_Code='" + fndCustomerCode.Value + "'")
    End Sub
End Class
