'--02/02/2018--form Add By- Sanjeet Kumar ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class FrmCostCetreTypeMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.DistrictMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        If btnSave.Visible = True Then
            MenuItemImport.Enabled = True
            MenuItemExport.Enabled = True
        Else
            MenuItemImport.Enabled = False
            MenuItemExport.Enabled = False
        End If
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmCostCetreTypeMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True

        funReset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Code", "varchar(30) not null primary Key")
        coll.Add("Description", "varchar(100) null")
        coll.Add("Department", "varchar(30) null References TSPL_DEPARTMENT_MASTER(DEPARTMENT_CODE)")
        coll.Add("Created_By", "varchar(12) null")
        coll.Add("Created_Date", "datetime null")
        coll.Add("Modify_By", "varchar(12) null")
        coll.Add("Modify_Date", "datetime null")
        coll.Add("Unit_Code", "varchar(30) null")
        coll.Add("Cost_Code", "varchar(30) null")
        coll.Add("Department_Cost", "integer null")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_COST_CENTER_TYPE_MASTER", coll)
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_COST_CENTER_TYPE_MASTER where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsCostCenterTypeMaster.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmCostCetreTypeMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            FunClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Save()
    End Sub
    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtName.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        Txtdepartmentcost.Value = ""
        Txtdepartmentcost.Enabled = True
        txtUnitCode.Value = ""
        txtCostCenterType.Value = ""
        lblDepartmentDes.Text = ""
        lblUnitDesc.Text = ""
        txtdes.Text = ""
        Txtdepartmentcost.Value = ""
        Labdepartmentcost.Text = ""
    End Sub
    Public Sub Save()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.DistrictMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New clsCostCenterTypeMaster()
                obj.Code = txtCode.Value
                obj.Description = txtName.Text
                obj.Department_Cost = Txtdepartmentcost.Value
                obj.Unit_Code = txtUnitCode.Value
                obj.Cost_Code = txtCostCenterType.Value
                'obj.Department_Cost = Txtdepartmentcost.Value

                Dim checkEntry As String = "Select 1 from TSPL_COST_CENTER_TYPE_MASTER where Code='" + txtCode.Value + "' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(checkEntry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    isNewEntry = False
                Else
                    isNewEntry = True
                End If

                If (clsCostCenterTypeMaster.SaveData(obj, isNewEntry, Nothing)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                Else
                    btnSave.Text = "Save"
                    btnDelete.Enabled = False
                End If

            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Code", Me.Text)
            txtCode.Focus()
            Return False
            'End If
        ElseIf clsCommon.myLen(txtCode.Value) > 30 Then
            clsCommon.MyMessageBoxShow(Me, "Code Max Length should be 30", Me.Text)
            txtCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtName.Text) <= 0 Then
            myMessages.blankValue(Me, "Description", Me.Text)
            txtName.Focus()
            Return False
        ElseIf clsCommon.myLen(Txtdepartmentcost.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Department", Me.Text)
            Txtdepartmentcost.Focus()
            Return False
        End If

        Dim checkEntry As String = "Select 1 from TSPL_COST_CENTER_TYPE_MASTER where Department_Cost='" + Txtdepartmentcost.Value + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(checkEntry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            clsCommon.MyMessageBoxShow(Me, "Department already exist", Me.Text)
            Return False
        End If

        Return True
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsCostCenterTypeMaster()
        obj = clsCostCenterTypeMaster.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then

            'funReset()
            isNewEntry = False
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            txtCode.Value = obj.Code
            txtName.Text = obj.Description
            Txtdepartmentcost.Value = obj.Department_Cost
            Labdepartmentcost.Text = obj.Department_Cost
            If clsCommon.myLen(Txtdepartmentcost.Value) > 0 Then
                Txtdepartmentcost.Enabled = False
                'Txtdepartmentcost.Text = obj.Department_Cost
            End If

            'txtlblDepartmentDes.Text = obj.Department
            txtUnitCode.Value = obj.Unit_Code
                txtCostCenterType.Value = obj.Cost_Code
            If clsCommon.myLen(Txtdepartmentcost.Value) > 0 Then
                Labdepartmentcost.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEPARTMENT_NAME from TSPL_DEPARTMENT_MASTER where DEPARTMENT_CODE='" + Txtdepartmentcost.Value + "'"))
            Else
                Labdepartmentcost.Text = ""
            End If
            If clsCommon.myLen(Labdepartmentcost.Text) > 0 Then
                Txtdepartmentcost.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Segment_code from TSPL_GL_SEGMENT_CODE where Description='" + Labdepartmentcost.Text + "'"))

            End If
            If clsCommon.myLen(txtUnitCode.Value) > 0 Then
                    lblUnitDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_COST_CENTER_UNIT_MASTER where Code='" + txtUnitCode.Value + "'"))
                Else
                    lblUnitDesc.Text = ""
                End If

                If clsCommon.myLen(txtCostCenterType.Value) > 0 Then
                    txtdes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cost_name from TSPL_CostCenter_MASTER where Cost_Code='" + txtCostCenterType.Value + "'"))
                Else
                    txtdes.Text = ""
                End If
            End If
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        ' Code Ends 
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsCostCenterTypeMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Sub FunClose()
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub MenuItemImport_Click(sender As Object, e As EventArgs) Handles MenuItemImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description") Then
            Try
                clsCommon.ProgressBarShow()
                Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim ii As Integer = 0
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii = ii + 1
                        Dim obj As New clsCostCenterTypeMaster()
                        obj.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Code can not be blank or incorrect.")
                        End If

                        obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        If clsCommon.myLen(obj.Description) <= 0 Then
                            Throw New Exception("Description can not be blank or incorrect.")
                        End If

                        clsCostCenterTypeMaster.SaveData(obj, clsCostCenterTypeMaster.CheckNewEntry(obj.Code, tran), tran)
                    Next
                Catch ex As Exception
                    tran.Rollback()
                    Throw New Exception("At Row No" + clsCommon.myCstr(ii) + ex.Message)
                End Try
                tran.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub MenuItemExport_Click(sender As Object, e As EventArgs) Handles MenuItemExport.Click
        Dim str As String
        str = "select Code, Description from TSPL_COST_CENTER_TYPE_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub txtCode_KeyPress(sender As Object, e As KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Private Sub txtDepartment__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDepartment._MYValidating
        'Try
        '    'Dim Qry As String = "select  Segment_code , Segment_name  from TSPL_GL_SEGMENT_CODE "
        '    Dim qry As String = "select DEPARTMENT_CODE as Code,DEPARTMENT_NAME as Name from TSPL_DEPARTMENT_MASTER"

        '    'Dim qry As String = "select Segment_code as Code,Description as Name from TSPL_GL_SEGMENT_CODE "
        '    txtDepartment.Value = clsCommon.ShowSelectForm("fndDepartment", qry, "Code", "", txtDepartment.Value, "Code", isButtonClicked)
        '    If clsCommon.myLen(txtDepartment.Value) > 0 Then
        '        lblDepartmentDes.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_SEGMENT_CODE Where Segment_code='" + txtDepartment.Value + "' ")
        '    Else
        '        lblDepartmentDes.Text = ""
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        'End Try
    End Sub

    Private Sub txtUnitCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUnitCode._MYValidating
        Try
            Dim obj As clsUnitMaster = clsUnitMaster.Finder(txtUnitCode.Value, isButtonClicked)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtUnitCode.Value = obj.Code
                lblUnitDesc.Text = obj.Description
            Else
                txtUnitCode.Value = ""
                lblUnitDesc.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtCostCenterType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCostCenterType._MYValidating
        'Try
        '    Dim Qry As String = "select  DEPARTMENT_CODE , DEPARTMENT_NAME  from TSPL_DEPARTMENT_MASTER "
        '    txtCostCenterType.Value = clsCommon.ShowSelectForm("fndDepartment", Qry, "DEPARTMENT_CODE", "", txtCostCenterType.Value, "DEPARTMENT_CODE", isButtonClicked)
        '    If clsCommon.myLen(txtCostCenterType.Value) > 0 Then
        '        txtdes.Text = clsDBFuncationality.getSingleValue("Select DEPARTMENT_NAME from TSPL_DEPARTMENT_MASTER Where DEPARTMENT_CODE='" + txtCostCenterType.Value + "' ")
        '    Else
        '        txtdes.Text = ""
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        'End Try
        If txtCostCenterType.MyReadOnly OrElse isButtonClicked Then
            txtCostCenterType.Value = ClsCostCenter.getFinder("", txtCostCenterType.Value, isButtonClicked)
            txtdes.Text = clsDBFuncationality.getSingleValue("Select TSPL_CostCenter_MASTER.Cost_name as [Cost Name] from TSPL_CostCenter_MASTER Where Cost_Code='" + txtCostCenterType.Value + "' ")

            LoadData(txtCostCenterType.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub Txtdepartmentcost__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles Txtdepartmentcost._MYValidating
        Try
            'Dim Qry As String = "select  Segment_code , Segment_name  from TSPL_GL_SEGMENT_CODE "
            Dim qry As String = "select Segment_code as Code,Description as Name from TSPL_GL_SEGMENT_CODE "
            Txtdepartmentcost.Value = clsCommon.ShowSelectForm("fndDepartment", qry, "Code", "Seg_No=3", Txtdepartmentcost.Value, "Code", isButtonClicked)
            If clsCommon.myLen(Txtdepartmentcost.Value) > 0 Then
                Labdepartmentcost.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_SEGMENT_CODE Where Segment_code='" + Txtdepartmentcost.Value + "' ")
            Else
                Labdepartmentcost.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class
