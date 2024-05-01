
Imports System.Data
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports common

Public Class FrmCustomerGroupReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim dt As Date = Date.Today
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim i As New Decimal
    Dim j As New Decimal
#End Region
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    'Private Sub fndToLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndToCustomerGroup.ConnectionString = connectSql.SqlCon()

    '    fndToCustomerGroup.Query = "select Cust_Group_Code as [Customer Group Code] ,Cust_Group_Desc as [Customer Group Description]from TSPL_CUSTOMER_GROUP_MASTER"
    '    fndToCustomerGroup.ValueToSelect = "Customer Group Code"
    '    fndToCustomerGroup.ValueToSelect1 = "Customer Group Description"
    '    fndToCustomerGroup.Caption = "Customer Group Details"
    'End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.CustomerGroupReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        funPrint()
    End Sub
    Private Sub funPrint()
        Dim f1 As String = fndFromCustomerGroup.Value
        Dim t1 As String = fndToCustomerGroup.Value
        funReport(f1, t1)
    End Sub

    Public Sub funReport(ByVal f1 As String, ByVal t1 As String)
        Try
            'f1 as [FCust],t1 as[TCust]
            If f1 = "" And t1 = "" Then
                ds = connectSql.RunSQLReturnDS("select m.Cust_Group_Code ,m.Tax_Group,m.Cust_Account,m.Terms_Code from TSPL_CUSTOMER_GROUP_MASTER m ")
            ElseIf f1 = "" And t1 <> "" Then
                common.clsCommon.MyMessageBoxShow(Me, "select From CustomerGroup Code", Me.Text)
                Exit Sub
            ElseIf f1 <> "" And t1 = "" Then
                common.clsCommon.MyMessageBoxShow(Me, "select To CustomerGroup Code", Me.Text)
                Exit Sub
            Else
                Dim qry As String = "select '" + f1 + "' as [FCG],'" + t1 + "' as [TCG],  m.Cust_Group_Code ,m.Tax_Group,m.Cust_Account,m.Terms_Code from TSPL_CUSTOMER_GROUP_MASTER m  where m.Cust_Group_Code between '" + f1 + "'and '" + t1 + "'"
                ds = connectSql.RunSQLReturnDS(qry)
            End If
            Dim dt As DataTable
            dt = ds.Tables(0)
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "CustomerGroupDetails", "Customer Group Details")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    'Private Sub fndFromLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndFromCustomerGroup.ConnectionString = connectSql.SqlCon()
    '    fndFromCustomerGroup.Query = "select Cust_Group_Code as [Customer Group Code] ,Cust_Group_Desc as [Customer Group Description]from TSPL_CUSTOMER_GROUP_MASTER"
    '    fndFromCustomerGroup.ValueToSelect = "Customer Group Code"
    '    fndFromCustomerGroup.ValueToSelect1 = "Customer Group Description"
    '    fndFromCustomerGroup.Caption = "Customer Group Details"
    'End Sub

    Private Sub FrmCustomerGroupReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            funPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub

    Private Sub FrmCustomerGroupReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'AddHandler fndFromCustomerGroup.txtValue.KeyPress, AddressOf FromCustomerGroup_KeyPress
        'AddHandler fndToCustomerGroup.txtValue.KeyPress, AddressOf ToCustomerGroup_KeyPress
        'fndFromCustomerGroup.txtValue.MaxLength = 12
        'fndToCustomerGroup.txtValue.MaxLength = 12
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Sub FromCustomerGroup_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    fndFromCustomerGroup.txtValue.CharacterCasing = CharacterCasing.Upper
    '    If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
    '        e.Handled = True
    '    End If
    'End Sub
    'Private Sub ToCustomerGroup_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    fndToCustomerGroup.txtValue.CharacterCasing = CharacterCasing.Upper
    '    If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub
    Sub funreset()
        fndFromCustomerGroup.Value = ""
        fndToCustomerGroup.Value = ""
    End Sub

    'Private Sub fndFromCustomerGroup_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndFromCustomerGroup.txtValue.Text <> "" Then
    '        'dr = connectSql.RunSqlReturnDR("select Cust_Group_Code from tspl_customer_group_master where Cust_Group_Code='" + fndFromCustomerGroup.txtValue.Text + "'")
    '        'Dim s As String
    '        'While dr.Read()
    '        '    s = dr(0).ToString()
    '        'End While

    '        sql = "select Cust_Group_Code from tspl_customer_group_master where Cust_Group_Code='" + fndFromCustomerGroup.txtValue.Text + "'"
    '        Dim s As String = clsDBFuncationality.getSingleValue(sql)

    '        If s <> fndFromCustomerGroup.txtValue.Text Then
    '            common.clsCommon.MyMessageBoxShow("Customer Group  doesn't exist")
    '            fndFromCustomerGroup.txtValue.Text = ""
    '            fndFromCustomerGroup.txtValue.Focus()
    '        Else

    '        End If
    '    Else

    '    End If
    'End Sub

    'Private Sub fndToCustomerGroup_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndToCustomerGroup.txtValue.Text <> "" Then
    '        'dr = connectSql.RunSqlReturnDR("select Cust_Group_Code from tspl_customer_group_master where Cust_Group_Code='" + fndToCustomerGroup.txtValue.Text + "'")
    '        'Dim s As String
    '        'While dr.Read()
    '        '    s = dr(0).ToString()
    '        'End While
    '        sql = "select Cust_Group_Code from tspl_customer_group_master where Cust_Group_Code='" + fndToCustomerGroup.txtValue.Text + "'"
    '        Dim s As String = clsDBFuncationality.getSingleValue(sql)

    '        If s <> fndToCustomerGroup.txtValue.Text Then
    '            common.clsCommon.MyMessageBoxShow("Customer Group  doesn't exist")
    '            fndToCustomerGroup.txtValue.Text = ""
    '            fndToCustomerGroup.txtValue.Focus()
    '        Else

    '        End If
    '    Else

    '    End If
    'End Sub


    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CUST-GRP-RPT"
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

    

    Private Sub fndFromCustomerGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndFromCustomerGroup._MYValidating
        Dim qry As String = "select Cust_Group_Code as [Customer Group Code] ,Cust_Group_Desc as [Customer Group Description]from TSPL_CUSTOMER_GROUP_MASTER"
        fndFromCustomerGroup.Value = clsCommon.ShowSelectForm("fndtrms", qry, "Customer Group Code", "", fndFromCustomerGroup.Value, "", isButtonClicked)
        lblFromCustomerGroup1.Text = clsDBFuncationality.getSingleValue("Select Cust_Group_Desc from TSPL_CUSTOMER_GROUP_MASTER where Cust_Group_Code='" + fndFromCustomerGroup.Value + "'")
    End Sub

    Private Sub fndToCustomerGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndToCustomerGroup._MYValidating
        Dim qry As String = "select Cust_Group_Code as [Customer Group Code] ,Cust_Group_Desc as [Customer Group Description]from TSPL_CUSTOMER_GROUP_MASTER"
        fndToCustomerGroup.Value = clsCommon.ShowSelectForm("fndtrms", qry, "Customer Group Code", "", fndToCustomerGroup.Value, "", isButtonClicked)
        lblToCustomerGroup1.Text = clsDBFuncationality.getSingleValue("Select Cust_Group_Desc from TSPL_CUSTOMER_GROUP_MASTER where Cust_Group_Code='" + fndToCustomerGroup.Value + "'")
    End Sub
End Class
