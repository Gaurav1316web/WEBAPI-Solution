Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI


Public Class FrmLICPolicyMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub FrmLICPolicyMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim coll As Dictionary(Of String, String)

        coll = New Dictionary(Of String, String)()
        coll.Add("LIC_CODE", "Varchar(50) not null PRIMARY KEY")
        coll.Add("LIC_NAME", "Varchar(100) not null")
        'coll.Add("SALARY_DEPENDENT_ON_ATTEN", "Varchar(30) NOT null")
        'coll.Add("OT_CODE", "Varchar(30)  null REFERENCES TSPL_OT_MASTER(OT_CODE)")
        'coll.Add("CALC_SAL_ON", "Varchar(30) NOT null")
        'coll.Add("ATTN_REGISTER_TYPE", "Varchar(30)  NOT null")
        'coll.Add("DESCRIPTION", "Varchar(100)  null")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        'clsCommonFunctionality.CreateOrAlterTable("TSPL_ATTENDANCE_MASTER", coll)
        clsCommonFunctionality.CreateOrAlterTable(False, False, "TSPL_LIC_POLICY_MASTER", coll, "", True, False, "", "", "", True)

        SetUserMgmtNew()
        isNewEntry = True

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Public Sub Save()
        Try
            Dim obj As New clsLICPolicyMaster()
            obj.Code = txtCode.Value
            obj.Description = txtName.Text

            If (obj.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.Code, NavigatorType.Current)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found.. ")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False
        Dim obj As New ClsLICPolicyMaster()
        obj = ClsLICPolicyMaster.GetData(strCode, NavTyep)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False
            btnSave.Text = "Update"
            txtCode.Value = obj.Code
            txtName.Text = obj.Name
            txtDescription.Text = obj.Description
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmAttendanceMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 01/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            MenuItemImport.Enabled = True
            MenuItemExport.Enabled = True
        Else
            MenuItemImport.Enabled = False
            MenuItemExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtName.Text = ""
        txtDescription.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsLICPolicyMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtCode.Value, "LIC_CODE", "TSPL_LIC_POLICY_MASTER")

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_LIC_POLICY_MASTER where LIC_CODE ='" + txtCode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))

            If no = 0 AndAlso isButtonClicked = False Then
                txtCode.MyReadOnly = False
                'txtCode.Value = ""
                '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
            Else
                txtCode.MyReadOnly = True
            End If

            If txtCode.MyReadOnly OrElse isButtonClicked Then
                txtCode.Value = ClsLICPolicyMaster.getFinder("", txtCode.Value, isButtonClicked)
                If txtCode.Value <> "" Then
                    LoadData(txtCode.Value, NavigatorType.Current)
                Else
                    funReset()
                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub MenuItemExport_Click(sender As Object, e As EventArgs) Handles MenuItemExport.Click
        Dim str As String
        str = "select LIC_CODE as Code, ATTENDANCE_NAME as Name, LIC_NAME as Description  from TSPL_LIC_POLICY_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub
End Class