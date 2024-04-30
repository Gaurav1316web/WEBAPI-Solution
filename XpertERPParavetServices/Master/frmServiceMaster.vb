Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common

Public Class FrmServiceMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt("SERVMST")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub AddNew()
        txtServiceCode.Value = ""
        txtDesc.Text = ""
        txtGroup.Value = ""
        txtServiceName.Value = ""
        txtCattleType.Value = ""
        txtBreedType.Value = ""
        txtServiceCharge.Text = ""
        txtReminder.Text = ""
        lblBreedType.Text = ""
        lblCattleType.Text = ""
        txtServiceCode.MyReadOnly = False
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        txtServiceCode.Focus()
    End Sub
    Private Sub FrmServiceMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        AddNew()
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsServiceMaster = clsServiceMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtServiceCode.Value = obj.Service_Code
            txtDesc.Text = obj.Service_Desc
            txtGroup.Value = obj.Service_Group_Name
            txtServiceName.Value = obj.Service_Group_Desc
            txtCattleType.Value = obj.Cattle_Type_Code
            txtBreedType.Value = obj.Breed_Code
            txtServiceCharge.Text = obj.Service_Charge
            txtReminder.Text = obj.Reminder_Days
            lblBreedType.Text = clsDBFuncationality.getSingleValue("select  Bred_Type_Name from  TSPL_BRED_TYPE_MASTER where Bred_Type_Code='" & txtBreedType.Value & "'")
            lblCattleType.Text = clsDBFuncationality.getSingleValue("select  Cattle_Type_Name from  TSPL_CATTLE_TYPE_MASTER where Cattle_Type_Code='" & txtCattleType.Value & "'")
            txtServiceCode.MyReadOnly = True
            btnSave.Text = "Update"
            btndelete.Enabled = True
        End If
    End Sub

    Private Sub txtCattleType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCattleType._MYValidating
        Dim query As String = "  select Cattle_Type_Code as [Code], Cattle_Type_Name as [Description] from TSPL_CATTLE_TYPE_MASTER "
        txtCattleType.Value = clsCommon.ShowSelectForm("CattleTypevald", query, "Code", "", txtCattleType.Value, "Code", isButtonClicked)
        Dim desc As String = "select  Cattle_Type_Name from  TSPL_CATTLE_TYPE_MASTER where Cattle_Type_Code='" & txtCattleType.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblCattleType.Text = clsCommon.myCstr(dt.Rows(0)("Cattle_Type_Name"))
        Else
            lblCattleType.Text = ""
        End If
    End Sub
    Private Sub txtBreedType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBreedType._MYValidating
        Dim query As String = "  select Bred_Type_Code as [Code], Bred_Type_Name as [Description] from TSPL_BRED_TYPE_MASTER "
        txtBreedType.Value = clsCommon.ShowSelectForm("BREDVald", query, "Code", "", txtBreedType.Value, "Code", isButtonClicked)
        Dim desc As String = "select  Bred_Type_Name from  TSPL_BRED_TYPE_MASTER where Bred_Type_Code='" & txtBreedType.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblBreedType.Text = clsCommon.myCstr(dt.Rows(0)("Bred_Type_Name"))
        Else
            lblBreedType.Text = ""
        End If
    End Sub



    Private Sub txtServiceName__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtServiceName._MYValidating
        If clsCommon.myLen(txtGroup.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Group Name First", Me.Text)
            txtGroup.Focus()
            txtGroup.Select()
            Return
        End If
        Try
            Dim qry As String = " select Service_Group_Desc as [Service_Name],Service_Group_Name as [Service Group Name] from TSPL_Paravet_Service_Group "
            txtServiceName.Value = clsCommon.ShowSelectForm("ServiceFND", qry, "Service_Name", " Service_Group_Name='" + txtGroup.Value + "'", txtServiceName.Value, "Service_Name", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGroup._MYValidating
        Dim query As String = "  select Service_Group_Code as Code,Service_Group_Name as [Name] from TSPL_Paravet_Service_Group "
        txtGroup.Value = clsCommon.ShowSelectForm("BREDVald", query, "Name", "", txtGroup.Value, "Name", isButtonClicked)
        txtServiceName.Value = ""
    End Sub

    Private Function AllowToSave() As Boolean
       
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtServiceCode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtServiceCode.Value)) > 30 Then
                myMessages.blankValue(Me, "Service Code", Me.Text)

                txtServiceCode.Focus()
                txtServiceCode.Select()
                Errorcontrol.SetError(txtServiceCode, "Service Code")
                Return False
            Else
                Errorcontrol.ResetError(txtServiceCode)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtDesc.Text)) <= 0 Then
                myMessages.blankValue(Me, "Description", Me.Text)

                txtDesc.Focus()
                txtDesc.Select()
                Errorcontrol.SetError(txtDesc, "Description")
                Return False
            Else
                Errorcontrol.ResetError(txtDesc)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtGroup.Value)) <= 0 Then
                myMessages.blankValue(Me, "Group Name", Me.Text)
                txtGroup.Focus()
                txtGroup.Select()
                Errorcontrol.SetError(txtGroup, "Group Name")
                Return False
            Else
                Errorcontrol.ResetError(txtGroup)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtServiceName.Value)) <= 0 Then
                myMessages.blankValue(Me, "Service Name", Me.Text)
                txtServiceName.Focus()
                txtServiceName.Select()
                Errorcontrol.SetError(txtServiceName, "Service Name")
                Return False
            Else
                Errorcontrol.ResetError(txtServiceName)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtCattleType.Value)) <= 0 Then
                myMessages.blankValue(Me, "Cattle Type", Me.Text)
                txtCattleType.Focus()
                txtCattleType.Select()
                Errorcontrol.SetError(txtCattleType, "Cattle Type")
                Return False
            Else
                Errorcontrol.ResetError(txtCattleType)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtBreedType.Value)) <= 0 Then
                myMessages.blankValue(Me, "Breed Type", Me.Text)
                txtBreedType.Focus()
                txtBreedType.Select()
                Errorcontrol.SetError(txtBreedType, "Breed Type")
                Return False
            Else
                Errorcontrol.ResetError(txtBreedType)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtServiceCharge.Text)) <= 0 Then
                myMessages.blankValue(Me, "Service Charge", Me.Text)
                txtServiceCharge.Focus()
                txtServiceCharge.Select()
                Errorcontrol.SetError(txtServiceCharge, "Service Charge")
                Return False
            Else
                Errorcontrol.ResetError(txtServiceCharge)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtReminder.Text)) <= 0 Then
                myMessages.blankValue(Me, "Reminder", Me.Text)
                txtReminder.Focus()
                txtReminder.Select()
                Errorcontrol.SetError(txtReminder, "Reminder")
                Return False
            Else
                Errorcontrol.ResetError(txtReminder)
            End If

           
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Sub SaveData()
       
        Try
            If AllowToSave() Then
                Dim obj As New clsServiceMaster()
                obj.Service_Code = txtServiceCode.Value
                obj.Service_Desc = txtDesc.Text
                obj.Service_Group_Name = txtGroup.Value
                obj.Service_Group_Desc = txtServiceName.Value
                obj.Cattle_Type_Code = txtCattleType.Value
                obj.Breed_Code = txtBreedType.Value
                obj.Service_Charge = txtServiceCharge.Text
                obj.Reminder_Days = txtReminder.Text
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Service_Code) from TSPL_Paravet_Service_Master WHERE Service_Code ='" + obj.Service_Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (clsServiceMaster.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Service_Code, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtServiceCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure? do you want to delete this Service Code ('" + txtServiceCode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_Paravet_Service_Master WHERE Service_Code='" + txtServiceCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Service Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Service Code is in use")
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub


    Private Sub txtServiceCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtServiceCode._MYNavigator
        Try
            LoadData(txtServiceCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ' select count(Service_Code) from TSPL_Paravet_Service_Master WHERE Service_Code

    Private Sub txtServiceCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtServiceCode._MYValidating
        Dim str As String = "select count(*) from TSPL_Paravet_Service_Master where Service_Code ='" + txtServiceCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtServiceCode.MyReadOnly = False
        Else
            txtServiceCode.MyReadOnly = True
        End If

        If txtServiceCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = " select Service_Code as [Code],Service_Desc as [Service Description] ,Service_Group_Name AS [Service Group Name],Service_Group_Desc as [Group Description],Cattle_Type_Code as [Cattle Type Code],Created_By as [Created By] ,Convert(varchar,Created_Date,103) as [Created Date] ,Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date]  from TSPL_Paravet_Service_Master "
            txtServiceCode.Value = clsCommon.ShowSelectForm("TSPL_Paravet_Service_Master", qry, "Code", "", txtServiceCode.Value, "TSPL_Paravet_Service_Master.Service_Code", isButtonClicked)
            If clsCommon.myLen(txtServiceCode.Value) > 0 Then
                Dim objOT As clsServiceMaster
                objOT = clsServiceMaster.GetData(txtServiceCode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtServiceCode.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub


    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Private Sub FrmServiceMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
            GC.Collect()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim str As String
        str = " select  Service_Code as [Service Code],Service_Desc as [Service Desc],Service_Group_Name as [Service Group Name],Service_Group_Desc as [Service Name],Cattle_Type_Code as [Cattle Type Code],Breed_Code as [Breed Code],Service_Charge as [Service Charge],Reminder_Days as [Reminder Days] from TSPL_Paravet_Service_Master "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Service Code", "Service Desc", "Service Group Name", "Service Name", "Cattle Type Code", "Breed Code", "Service Charge", "Reminder Days") Then
            Dim linno As Integer = 1
            Try
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New clsServiceMaster()
                    Dim strServiceCode As String = clsCommon.myCstr(grow.Cells("Service Code").Value)
                    Dim strServiceDesc As String = clsCommon.myCstr(grow.Cells("Service Desc").Value)
                    Dim strServiceGroupName As String = clsCommon.myCstr(grow.Cells("Service Group Name").Value)
                    Dim strServiceName As String = clsCommon.myCstr(grow.Cells("Service Name").Value)
                    Dim strCattleTypeCode As String = clsCommon.myCstr(grow.Cells("Cattle Type Code").Value)
                    Dim strBreedCode As String = clsCommon.myCstr(grow.Cells("Breed Code").Value)
                    Dim strServiceCharge As String = clsCommon.myCstr(grow.Cells("Service Charge").Value)
                    Dim strReminderDays As String = clsCommon.myCstr(grow.Cells("Reminder Days").Value)

                    linno += 1
                    If clsCommon.myLen(strServiceCode) <= 0 Then
                        Throw New Exception("Service Code should not be left blankat line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strServiceCode) > 30 Then
                        Throw New Exception("Please check ! length of Service Code not greter then 30 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strServiceGroupName) <= 0 Then
                        Throw New Exception("Group Name should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strServiceGroupName) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_Paravet_Service_Group where Service_Group_Name ='" & strServiceGroupName & "'  ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid Group Name,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    If clsCommon.myLen(strServiceName) <= 0 Then
                        Throw New Exception("Service Name should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strServiceName) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_Paravet_Service_Group where Service_Group_Name='" + strServiceGroupName + "' and Service_Group_Desc = '" + strServiceName + "' ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid Service Name,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    If clsCommon.myLen(strCattleTypeCode) <= 0 Then
                        Throw New Exception("Cattle Type Code should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strCattleTypeCode) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue("  select count(*) from TSPL_CATTLE_TYPE_MASTER where Cattle_Type_Code = '" + strCattleTypeCode + "' ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid Cattle Type Code,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    If clsCommon.myLen(strBreedCode) <= 0 Then
                        Throw New Exception("Breed Code should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strBreedCode) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue("  select count(*) from TSPL_BRED_TYPE_MASTER where Bred_Type_Code = '" + strBreedCode + "' ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid Breed Code,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If clsCommon.myLen(strServiceCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Paravet_Service_Master where Service_Code='" + strServiceCode + "' ") > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If

                    obj.Service_Code = strServiceCode
                    obj.Service_Desc = strServiceDesc
                    obj.Service_Group_Name = strServiceGroupName
                    obj.Service_Group_Desc = strServiceName
                    obj.Cattle_Type_Code = strCattleTypeCode
                    obj.Breed_Code = strBreedCode
                    obj.Service_Charge = strServiceCharge
                    obj.Reminder_Days = strReminderDays

                    clsServiceMaster.SaveData(obj, IsNewEntry)
                    'LoadData(obj.Service_Code, NavigatorType.Current)
                Next
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)


    End Sub
End Class
