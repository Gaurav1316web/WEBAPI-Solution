Imports common
Imports System.Data.SqlClient
''Created by Preeti gupta Ticket no[BM00000004258]
Public Class FrmCreditLimitApprovalMaster
    Inherits FrmMainTranScreen
    Dim isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isnewentry As Boolean
    Dim ModuleName As String = ""
    Dim formtype As String = Nothing
    Dim strCLAPass As String = ""

#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub SetUserMgmtNew()
        '=====shivani
        If formtype = clsUserMgtCode.FrmCreditLimitApproval Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmCreditLimitApproval)
            strCLAPass = clsUserMgtCode.FrmCreditLimitApproval
        ElseIf formtype = clsUserMgtCode.FrmBulkCreditLimitApproval Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmBulkCreditLimitApproval)
            strCLAPass = clsUserMgtCode.FrmBulkCreditLimitApproval
        End If
        '================

        'MyBase.SetUserMgmt(clsUserMgtCode.FrmCreditLimitApproval)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If

    End Sub
    Public Sub UserCode()
        Dim qry As String = "select User_Code as Code ,User_Name  as Name from TSPL_USER_MASTER   "
        cbgUser.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgUser.ValueMember = "Code"
        cbgUser.DisplayMember = "Name"
    End Sub
  

    Sub GetModule()
        Dim dt As New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "FS"
        dr("Name") = "Fresh Sale"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "PS"
        dr("Name") = "Product Sale"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "BS"
        dr("Name") = "Bulk Sale"
        dt.Rows.Add(dr)

        isInsideLoadData = True

        ddmodule.DataSource = dt
        ddmodule.ValueMember = "Name"
        ddmodule.DisplayMember = "Name"
        isInsideLoadData = False
    End Sub
    Sub SaveData()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(strCLAPass, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            btnSave.Focus()
            Dim obj As clsCreditLimitApprovalDeatils = Nothing
            Dim arr As New List(Of clsCreditLimitApprovalDeatils)
            Dim entry As String
            Dim count As Integer = 0
            Dim i As Integer = 0
            Dim qry As String = "select count(*) from TSPL_CREDIT_LIMIT_APPROVAL_HEAD  where Module_Name ='" + ddmodule.Text + "'"
            count = clsDBFuncationality.getSingleValue(qry)
            If count = 0 Then
                isnewentry = True
            Else
                isnewentry = False

            End If
            If ((cbgUser.CheckedValue.Count) <= 0) Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one user ")
                rgBank.Focus()
            ElseIf clsCommon.myCstr(ddmodule.Text) = "" Then
                common.clsCommon.MyMessageBoxShow("Please fill Module Name ")
                ddmodule.Focus()
            End If
            Dim Name As New clsCreditLimitApprovalHead
            Name.ModuleName = clsCommon.myCstr(ddmodule.Text)

            If cbgUser.CheckedValue.Count > 0 Then

                For i = 0 To cbgUser.CheckedValue.Count - 1
                    obj = New clsCreditLimitApprovalDeatils()
                    obj.UserCode = clsCommon.myCstr(cbgUser.CheckedValue(i))
                    arr.Add(obj)
                Next
                If clsCreditLimitApprovalHead.SaveData(Name, isnewentry, arr) Then
                    clsCommon.MyMessageBoxShow("Saved successfully")
                    entry = obj.ModuleName
                    getdata(Name.ModuleName)
                    btnDelete.Enabled = True
                End If

            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
    Sub getdata(ByVal ModuleName As String)
        Try
            cbgUser.UnCheckedAll()
            Dim obj As clsCreditLimitApprovalHead = clsCreditLimitApprovalHead.getdata(ModuleName)
            If obj IsNot Nothing Then

                ddmodule.Text = obj.ModuleName
                cbgUser.CheckedValue = obj.arrUser
            End If
            btnDelete.Enabled = True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub resetdata()
        ddmodule.Text = "Fresh Sale"
        cbgUser.UnCheckedAll()
        getdata(ddmodule.Text)
        'btnDelete.Enabled = False
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsCreditLimitApprovalHead.DeleteData(ddmodule.Text)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    resetdata()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Sub DeleteData()
        If ((cbgUser.CheckedValue.Count) <= 0) Then
            common.clsCommon.MyMessageBoxShow(" No data found to delete")
            Exit Sub
        End If
        funDelete()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        resetdata()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmCreditLimitApprovalMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        btnDelete.Enabled = False
        UserCode()
        GetModule()
        getdata(ddmodule.Text)
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D for Delete")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R for Reset")
    End Sub

    Private Sub ddmodule_SelectedIndexChanged1(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddmodule.SelectedIndexChanged
        If isInsideLoadData = False Then
            Dim ModuleName As String = ddmodule.SelectedValue
            getdata(ModuleName)
        End If
    End Sub

    'Private Sub ddmodule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddmodule.SelectedValueChanged
    '    If isInsideLoadData = False Then
    '        getdata(ModuleName)
    '    End If
    'End Sub

    Private Sub FrmCreditLimitApprovalMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            resetdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub
End Class
