Imports common
Imports XpertERPEngine
Imports XpertERPEngineFine
Imports Telerik.WinControls.UI
Public Class frmSheedGrowerMaster
    Dim isNewEntry As Boolean = True
    Dim ErrorControl As New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub frmSheedGrowerMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag

        If btnsave.Visible = True Then
            RadMenuItem1.Enabled = True
            RadMenuItem2.Enabled = True
        Else
            RadMenuItem1.Enabled = False
            RadMenuItem2.Enabled = False
        End If
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmSheedGrowerMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsSheedGrowerMaster()
                obj.Code = txtCode.Value
                obj.Name = txtname.Text.Replace("'", "`")
                obj.Father_Name = txtFatherName.Text
                obj.Village_Code = fndVillegeCode.Value
                obj.Tehsil = txtTehsil.Text
                obj.District_Code = txtDistrict.Value
                obj.Mobile_No = txtMobile.Text
                obj.Own_Land = txtOwnLand.Value
                obj.Family_Land = txtFamilyLand.Value
                obj.Lease_Land = txtLeaseLand.Value
                obj.Total_Land = txtTotal.Value

                If (obj.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean

        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Code", Me.Text)
            txtCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtname.Text) <= 0 Then
            myMessages.blankValue(Me, "Name", Me.Text)
            txtname.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim Sqlqry As String = "select Code,Name from TSPL_SHEED_GROWER_MASTER where code='" + txtCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
        If count = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim whrClas As String = ""
            Dim qry As String = "select Code,Name,Father_Name ,Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modified By],Modified_Date as [Modified Date] from TSPL_SHEED_GROWER_MASTER"
            txtCode.Value = clsCommon.ShowSelectForm("RTY", qry, "Code", whrClas, txtCode.Value, "TSPL_SHEED_GROWER_MASTER.Code asc", isButtonClicked, Nothing)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = True
            btndelete.Enabled = False
            btnsave.Enabled = True
            btnsave.Text = "Save"
            txtCode.MyReadOnly = False

            Dim obj As clsSheedGrowerMaster = clsSheedGrowerMaster.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                isNewEntry = False
                txtCode.Value = obj.Code
                txtname.Text = obj.Name
                txtFatherName.Text = obj.Father_Name
                fndVillegeCode.Value = obj.Village_Code
                txtTehsil.Text = obj.Tehsil
                txtDistrict.Value = obj.District_Code
                txtMobile.Text = obj.Mobile_No
                txtOwnLand.Value = obj.Own_Land
                txtFamilyLand.Value = obj.Family_Land
                txtLeaseLand.Value = obj.Lease_Land
                txtTotal.Value = obj.Total_Land
                lblVillage.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct village_name from tspl_village_master where village_code='" + obj.Village_Code + "'"))
                lblDistrict.Text = clsDistrictMaster.GetName(obj.District_Code)

                txtCode.MyReadOnly = True
                btnsave.Text = "Update"
                btndelete.Enabled = True
            Else
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim qry As String = "select count(*) from TSPL_SHEED_GROWER_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select Code,Name from TSPL_SHEED_GROWER_MASTER"
        Else
            qry = "select '' as Code,'' as Name"
        End If

        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Sub AddNew()
        txtCode.Value = ""
        txtname.Text = ""
        txtFatherName.Text = ""
        fndVillegeCode.Value = ""
        txtTehsil.Text = ""
        txtDistrict.Value = ""
        txtMobile.Text = ""
        txtOwnLand.Value = 0
        txtFamilyLand.Value = 0
        txtLeaseLand.Value = 0
        txtTotal.Value = 0
        lblVillage.Text = ""
        lblDistrict.Text = ""
        txtCode.MyReadOnly = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        isNewEntry = True
        txtname.Focus()
        txtname.Select()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                ErrorControl.SetError(txtCode, "Code not found to delete.")
                Throw New Exception("Code not found to delete")
            Else
                ErrorControl.ResetError(txtCode)
            End If

            If myMessages.deleteConfirm() Then
                If clsSheedGrowerMaster.DeleteData(txtCode.Value) Then
                    myMessages.delete()
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs)
        txtCode.Value = ""
        AddNewItem()
    End Sub
    Private Sub AddNewItem()
        txtCode.MyReadOnly = False
        txtname.Text = ""
        isNewEntry = True
    End Sub


    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim gv_Import As New RadGridView()
        Me.Controls.Add(gv_Import)
        Dim oldNewentry As Boolean = isNewEntry
        Dim counter As Integer = 0

        If transportSql.importExcel(gv_Import, "Code", "Name") Then
            Dim obj As New clsSheedGrowerMaster()

            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv_Import.Rows

                    obj.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(obj.Code) > 50 Then
                        Throw New Exception("Code has max. length 50 see at line no. " + clsCommon.myCstr(counter + 1) + "")
                    End If

                    obj.Name = clsCommon.myCstr(grow.Cells("Name").Value).Replace("'", "`")
                    If clsCommon.myLen(obj.Name) <= 0 Then
                        Throw New Exception("Fill Name  " + clsCommon.myCstr(counter + 1) + "")
                    End If
                    If clsCommon.myLen(obj.Name) > 200 Then
                        obj.Name = obj.Name.Substring(0, 200)
                    End If

                    Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_SHEED_GROWER_MASTER where code='" + obj.Code + "'")
                    isNewEntry = True
                    If qry > 0 Then
                        isNewEntry = False
                    End If

                    If (obj.SaveData(obj, isNewEntry)) Then

                    End If
                    counter += 1
                Next

                clsCommon.ProgressBarHide()

                If counter >= 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Data transfer successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No data found to transfer", Me.Text)
                End If


            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If

        isNewEntry = oldNewentry
        Me.Controls.Remove(gv_Import)
    End Sub

    Private Sub fndVillegeCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndVillegeCode._MYValidating
        Dim qry As String = " select TSPL_VILLAGE_MASTER.Village_Code as [Code] ,TSPL_VILLAGE_MASTER.Village_Name as [Village Name] ,TSPL_VILLAGE_MASTER.Add1 as [Address1] ,TSPL_VILLAGE_MASTER.Add2 as [Address2] ,TSPL_VILLAGE_MASTER.City_Code as [City Code] ,TSPL_VILLAGE_MASTER.State_Code as [State Code] ,TSPL_VILLAGE_MASTER.COUNTRY_CODE as [Country Code] ,TSPL_VILLAGE_MASTER.PIN_NO as [Pin No] ,TSPL_VILLAGE_MASTER.Created_By as [Created By] ,TSPL_VILLAGE_MASTER.Created_Date as [Created Date] ,TSPL_VILLAGE_MASTER.Modified_By as [Modified By] ,TSPL_VILLAGE_MASTER.Modified_Date as [Modified Date]  From TSPL_VILLAGE_MASTER "
        fndVillegeCode.Value = clsCommon.ShowSelectForm("VLGFND", qry, "Code", "", fndVillegeCode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(fndVillegeCode.Value) > 0 Then
            lblVillage.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct village_name from tspl_village_master where village_code='" & fndVillegeCode.Value & "'"))
        Else
            fndVillegeCode.Value = ""
            lblVillage.Text = ""
        End If
    End Sub

    Private Sub txtDistrict__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDistrict._MYValidating
        Try
            Dim qry As String = "select TSPL_DISTRICT_MASTER.Code as Code,TSPL_DISTRICT_MASTER.Name as DistrictName,TSPL_State_MASTER.STATE_CODE as [State Code] ,TSPL_State_MASTER.STATE_NAME as [State] " &
           " from TSPL_DISTRICT_MASTER " &
           " left outer join TSPL_State_MASTER  on TSPL_State_MASTER.STATE_CODE=TSPL_DISTRICT_MASTER.State_Code " &
           " left outer join TSPL_State_MASTER_detail on TSPL_State_MASTER.state_code=TSPL_State_MASTER_detail.state_code "
            txtDistrict.Value = clsCommon.ShowSelectForm("MP@District@Finder", qry, "Code", "", txtDistrict.Value, "", isButtonClicked)

            If clsCommon.myLen(txtDistrict.Value) > 0 Then
                lblDistrict.Text = clsDistrictMaster.GetName(txtDistrict.Value)
            Else
                txtDistrict.Value = ""
                lblDistrict.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try

    End Sub

    Private Sub txtOwnLand_TextChanged(sender As Object, e As EventArgs) Handles txtOwnLand.TextChanged
        UpdateTotalLand()
    End Sub

    Private Sub txtFamilyLand_TextChanged(sender As Object, e As EventArgs) Handles txtFamilyLand.TextChanged
        UpdateTotalLand()
    End Sub

    Private Sub txtLeaseLand_TextChanged(sender As Object, e As EventArgs) Handles txtLeaseLand.TextChanged
        UpdateTotalLand()
    End Sub
    Sub UpdateTotalLand()
        Dim OwnLand As Decimal = 0
        Dim FamilyLAnd As Decimal = 0
        Dim LeaseLand As Decimal = 0
        Dim TotalLand As Decimal = 0
        OwnLand = txtOwnLand.Value
        FamilyLAnd = txtFamilyLand.Value
        LeaseLand = txtLeaseLand.Value
        TotalLand = OwnLand + FamilyLAnd + LeaseLand
        txtTotal.Value = TotalLand

    End Sub

End Class