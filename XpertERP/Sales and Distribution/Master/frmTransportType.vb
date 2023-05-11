'Developed By -BibhuPrasad Parida
'Database - TSPLERP
'Table - tspl_customer_category_master
'Start Date -
'End Date -'--preeti gupta-ticket no.[BM00000003133]
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports common

Public Class FrmTransportType
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()


#Region "Constructor"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
#End Region
#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dr As DataTable
    Dim tableName As String = "TSPL_TRANSPORT_TYPE"
    Dim dt As Date = Date.Today
    Dim userCode, companyCode As String
#End Region

    Private Sub fndTransportTypeId_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndTransportTypeId.ConnectionString = connectSql.SqlCon()
        'fndTransportTypeId.Query = " select TransType_Code as [Transport Type],TransType_Desc as [Description] from TSPL_TRANSPORT_TYPE order by TransType_Code"
        'fndTransportTypeId.ValueToSelect = "Transport Type"
        'fndTransportTypeId.ValueToSelect1 = "Description"
        'fndTransportTypeId.Caption = "TransportType Details"
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub
    Private Sub funReset()
        fndTransportTypeId.MyReadOnly = False
        fndTransportTypeId.Value = ""
        fndTransportTypeId.Focus()
        txtDescription.Text = ""
        btnDelete.Enabled = False
        btnSave.Text = "Save"
    End Sub

    Private Sub FrmTransportType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fndTransportTypeId.MyMaxLength = 12
        txtDescription.MaxLength = 30
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        SetUserMgmtNew()
        fndTransportTypeId_TextChanged()
        '     globalFunc.mandatoryText(fndTransportTypeId.Value)
        '  AddHandler fndTransportTypeId.ValueChanged, AddressOf fndTransportTypeId_TextChanged


        btnDelete.Enabled = False

        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.transportType)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
#Region "KeyPress Event"
    Private Sub fndTransportTypeId_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
#End Region
#Region "TextChanged"
    Sub fndTransportTypeId_TextChanged()
        dr = clsDBFuncationality.GetDataTable("select TransType_Code from TSPL_TRANSPORT_TYPE where TransType_Code='" + fndTransportTypeId.Value + "'")
        Dim s As String = ""

        If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
            s = dr.Rows(0)(0).ToString()
        End If

        If s <> fndTransportTypeId.Value Then
            txtDescription.Text = ""
            btnSave.Text = "Save"
            btnDelete.Enabled = False
        Else
            funFill()
        End If
    End Sub
#End Region
#Region "Methods"
    'fill Customer Type Details
    Private Sub funFill()
        If fndTransportTypeId.Value <> "" Then
            dr = clsDBFuncationality.GetDataTable("select * from TSPL_TRANSPORT_TYPE where TransType_Code='" + fndTransportTypeId.Value + "'")

            If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                txtDescription.Text = dr.Rows(0)(1).ToString()
            End If

            btnSave.Enabled = True
            btnSave.Text = "Update"
            btnDelete.Enabled = True

        Else
            btnSave.Enabled = True
            btnSave.Text = "Save"

        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub
    'priti added on 01-06-2011 --- To implement the access control
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "TRANS-TYPE"
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
    '            btnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    'Code ends here
    'insert Customer Type Details
    Private Sub funInsert()
        Try
            connectSql.RunSp("sp_TSPL_TRANSPORT_TYPE_insert", New SqlParameter("@TransportTypeCode", fndTransportTypeId.Value), New SqlParameter("@TransportTypeDesc", txtDescription.Text), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate()), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode))
            myMessages.insert()

            btnSave.Text = "Update"
            btnDelete.Enabled = True
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'update Customer Type Details
    Private Sub funUpdate()
        If fndTransportTypeId.Value = "" Then
            myMessages.blankValue("Transport Type")
        Else
            Try
                connectSql.RunSp("sp_TSPL_TRANSPORT_TYPE_update", New SqlParameter("@TransportTypeCode", fndTransportTypeId.Value), New SqlParameter("@TransportTypeDesc", txtDescription.Text), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate()), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode))
                myMessages.update()
                btnSave.Text = "Update"
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub


    'delete Customer Type Details
    Private Sub funDelete()
        Try
            connectSql.RunSp("sp_TSPL_TRANSPORT_TYPE_delete", New SqlParameter("@TransportTypeCode", fndTransportTypeId.Value))
            myMessages.delete()
            btnSave.Text = "Save"
            btnDelete.Enabled = False
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'priti added on 01-06-2011 --- To implement the access control
    '  Private Function funSetUserAccess() As Boolean
    ' Try
    'If funCheckLoginStatus() = False Then Exit Function
    'Dim strRights As String
    'Dim strTemp() As String
    'Dim strProgCode = "CUST-TYPE"
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
    '            btnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    'Code ends here
#End Region
#Region "ButtonClick Event"
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If fndTransportTypeId.Value = "" Then
            ' common.clsCommon.MyMessageBoxShow("Please enter Customer Type.")
            myMessages.blankValue("Transport Type")
        Else
            If btnSave.Text = "Save" Then
                funInsert()
            Else
                funUpdate()
            End If
        End If
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If btnDelete.Enabled = True Then
            If fndTransportTypeId.Value <> "" Then
                If myMessages.deleteConfirm() Then
                    funDelete()
                Else

                End If
            End If
        Else
            common.clsCommon.MyMessageBoxShow("You Can Not Delete record")
        End If
    End Sub
    Private Sub MenuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuClose.Click
        Me.Close()
    End Sub
#End Region
#Region "Finder Load"

#End Region
#Region "Finder Leave Event"
    Private Sub fndTransportTypeId_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If fndTransportTypeId.txtValue.Text <> "" Then
        '    'txtDescription.Text = ""
        '    btnSave.Enabled = True
        'Else
        '    btnClose.Focus()
        'End If
    End Sub
#End Region
#Region "Import/Export"
    Private Sub MenuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExport.Click
        sql = "select TransType_Code,TransType_Desc from TSPL_TRANSPORT_TYPE "
        transportSql.ExporttoExcel(sql, Me)
    End Sub

    Private Sub MenuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "TransType_Code", "TransType_Desc") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim strTransportType As String = ""
                    Dim strTransportTypeDesc As String

                    If String.IsNullOrEmpty(grow.Cells(0).Value.ToString()) Then
                        'common.clsCommon.MyMessageBoxShow("Customer Type has some incorrect values")
                        myMessages.blankValue("Transport Type")

                    ElseIf clsCommon.myLen(grow.Cells(0).Value.ToString()) > 12 Then
                        Throw New Exception("Transport Type cannot be greater than 12 length.")
                    Else
                        strTransportType = clsCommon.myCstr(grow.Cells(0).Value.ToString()).ToUpper()
                    End If

                    If grow.Cells(1).Value.ToString().Length > 30 Then
                        Throw New Exception("Transport Type Description cannot be greater than 30 length")
                    Else
                        strTransportTypeDesc = clsCommon.myCstr(grow.Cells(1).Value)
                    End If

                    Dim sql1 As String = "select COUNT(*) from TSPL_TRANSPORT_TYPE  where TransType_Code='" + strTransportType + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        ' connectSql.RunSpTransaction(trans, "@sp_tspl_Customer_Type_Master_insert", New SqlParameter("@CustTypeCode", str), New SqlParameter("@CustTypeDesc", str1), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate()), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate()), New SqlParameter("@CompCode", companyCode))
                        connectSql.RunSpTransaction(trans, "sp_TSPL_TRANSPORT_TYPE_insert", New SqlParameter("@TransportTypeCode", strTransportType), New SqlParameter("@TransportTypeDesc", strTransportTypeDesc), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate(trans)), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate(trans)), New SqlParameter("@CompCode", companyCode))
                    Else
                        connectSql.RunSpTransaction(trans, "sp_TSPL_TRANSPORT_TYPE_update", New SqlParameter("@TransportTypeCode", strTransportType), New SqlParameter("@TransportTypeDesc", strTransportTypeDesc), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", connectSql.serverDate(trans)), New SqlParameter("@ModifiedBy", userCode), New SqlParameter("@ModifiedDate", connectSql.serverDate(trans)), New SqlParameter("@CompCode", companyCode))
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
#End Region


    Private Sub FrmTransportType_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub fndTransportTypeId__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTransportTypeId._MYValidating

        Dim str As String = "select count(*) from TSPL_TRANSPORT_TYPE where TransType_Code ='" + fndTransportTypeId.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndTransportTypeId.MyReadOnly = False
        Else
            fndTransportTypeId.MyReadOnly = True
        End If
        If fndTransportTypeId.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select TransType_Code as [TransportType],TransType_Desc as [Description] from TSPL_TRANSPORT_TYPE   "
            fndTransportTypeId.Value = clsCommon.ShowSelectForm("POProjectID", qry, "TransportType", "", fndTransportTypeId.Value, "TransType_Code", isButtonClicked)

            'fndTransportTypeId_TextChanged()
            funFill()
        End If

    End Sub

    Private Sub fndTransportTypeId__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndTransportTypeId._MYNavigator

        Dim qst As String = "select TransType_Code as [Transport Type],TransType_Desc as [Description] from TSPL_TRANSPORT_TYPE  where  2=2 "
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and TransType_Code in (select min(TransType_Code) from TSPL_TRANSPORT_TYPE where TransType_Code>'" + fndTransportTypeId.Value + "' ) "
            Case NavigatorType.First
                qst += "and TransType_Code in (select MIN(TransType_Code) from TSPL_TRANSPORT_TYPE  )"
            Case NavigatorType.Last
                qst += "and TransType_Code in (select Max(TransType_Code) from TSPL_TRANSPORT_TYPE )"
            Case NavigatorType.Previous
                qst += "and TransType_Code in (select max(TransType_Code) from TSPL_TRANSPORT_TYPE where TransType_Code<'" + fndTransportTypeId.Value + "'  )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndTransportTypeId.Value = clsCommon.myCstr(dt.Rows(0)("Transport Type"))
        End If
        'TextChanged()
        If fndTransportTypeId.Value IsNot Nothing Then
            btnDelete.Enabled = True
        Else
            btnDelete.Enabled = False

        End If
        'fndTransportTypeId_TextChanged()
        funFill()
    End Sub
End Class
