Imports common
Imports XpertERPEngine
Imports XpertERPEngineFine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class frmSeedSelectionEntry
    Dim isNewEntry As Boolean = True
    Dim ErrorControl As New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub frmSeedSelectionEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New ")
        Addnew()
        LoadFlag()
        LoadSeason()
        btnPost.Visible = True
        btnPost.Enabled = False
    End Sub
    Private Sub frmBullInsurance_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            btnAddNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnsave.Enabled AndAlso MyBase.isDeleteFlag Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverseUnpost.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
    Sub LoadFlag()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Y"
        dr("Name") = "Yes"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "No"
        dt.Rows.Add(dr)

        cmbSelectedFlag.DataSource = dt
        cmbSelectedFlag.ValueMember = "Code"
        cmbSelectedFlag.DisplayMember = "Name"
    End Sub
    Sub LoadSeason()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "RABI"
        dr("Name") = "RABI"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "KHARIF"
        dr("Name") = "KHARIF"
        dt.Rows.Add(dr)

        cmbSeason.DataSource = dt
        cmbSeason.ValueMember = "Code"
        cmbSeason.DisplayMember = "Name"
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        RadSplitButton1.Visible = MyBase.isExport
        btnPost.Visible = MyBase.isPostFlag
        If MyBase.isExport = True Then
            rmExport.Enabled = True
            rmimport.Enabled = True
        Else
            rmExport.Enabled = False
            rmimport.Enabled = False
        End If

    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmSeedSelectionEntry, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsSeedSelectionEntry
                obj.Document_No = clsCommon.myCstr(txtDocumentNo.Value)
                obj.Document_Date = txtDate.Value
                obj.PO_Code = txtPONo.Value
                obj.Grower_Code = txtGrower.Value
                obj.Crop_Reg_Code = txtCropRegCode.Text
                obj.Crop_Location = txtCropLocation.Text
                obj.Area = txtArea.Text
                obj.Item_Code = txtItemCode.Value
                obj.Khasra_No = txtKhasraNo.Text
                obj.Season = clsCommon.myCstr(cmbSeason.SelectedValue)
                obj.Village_Code = txtVillage.Value
                obj.DISTRICT_Code = txtDistrict.Value
                obj.Selected_Flag = clsCommon.myCstr(cmbSelectedFlag.SelectedValue)
                obj.Own_Land = txtOwnLand.Value
                obj.Family_Land = txtFamilyLand.Value
                obj.Lease_Land = txtLeaseLand.Value
                obj.Sowing_Week_Month = txtSowingWeekMonth.Text
                If (obj.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtGrower.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Grower", Me.Text)
                txtGrower.Focus()
                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Private Sub Addnew()
        LoadFlag()
        LoadSeason()
        txtDate.Value = clsCommon.GETSERVERDATE()
        isNewEntry = True
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnPost.Enabled = True
        btndelete.Enabled = True
        UsLock.Status = ERPTransactionStatus.Pending
        txtDocumentNo.Value = ""
        txtGrower.Value = ""
        lblGrower.Text = ""
        txtPONo.Value = ""
        txtVillage.Value = ""
        lblVillage.Text = ""
        txtDistrict.Value = ""
        lblDistrict.Text = ""
        txtItemCode.Value = ""
        txtArea.Text = ""
        txtCropRegCode.Text = ""
        txtCropLocation.Text = ""
        txtKhasraNo.Text = ""
        txtSowingWeekMonth.Text = ""
        txtOwnLand.Text = ""
        txtFamilyLand.Text = ""
        txtLeaseLand.Text = ""
    End Sub

    Private Sub txtDocumentNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        Dim Sqlqry As String = "select Document_No,Document_Date,PO_Code,Grower_Code,Crop_Reg_Code,Crop_Location,Area,Item_Code,Khasra_No,Season,Village_Code,DISTRICT_Code,Status from TSPL_SEED_GROWER_SELECTION_ENTRY where Document_No='" + txtDocumentNo.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
        If count = 0 Then
            txtDocumentNo.MyReadOnly = False
        Else
            txtDocumentNo.MyReadOnly = True
        End If
        If txtDocumentNo.MyReadOnly OrElse isButtonClicked Then
            Dim whrClas As String = ""
            Dim qry As String = "select Document_No as [Document No] ,Created_By as [Created By],Created_Date as [Created Date],Modified_By as [Modified By],Modified_Date as [Modified Date] from TSPL_SEED_GROWER_SELECTION_ENTRY"
            txtDocumentNo.Value = clsCommon.ShowSelectForm("RTY", qry, "Document No", whrClas, txtDocumentNo.Value, "TSPL_SEED_GROWER_SELECTION_ENTRY.Document_No asc", isButtonClicked, Nothing)
            LoadData(txtDocumentNo.Value, NavigatorType.Current)
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = True
            btndelete.Enabled = False
            btnsave.Enabled = True
            btnsave.Text = "Save"
            btnPost.Enabled = True
            txtDocumentNo.MyReadOnly = False

            Dim obj As clsSeedSelectionEntry = clsSeedSelectionEntry.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                isNewEntry = False
                txtDocumentNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtPONo.Value = obj.PO_Code
                txtGrower.Value = obj.Grower_Code
                txtCropRegCode.Text = obj.Crop_Reg_Code
                txtCropLocation.Text = obj.Crop_Location
                txtDistrict.Value = obj.DISTRICT_Code
                txtArea.Text = obj.Area
                txtItemCode.Value = obj.Item_Code
                txtKhasraNo.Text = obj.Khasra_No
                txtVillage.Value = obj.Village_Code
                txtOwnLand.Value = obj.Own_Land
                txtFamilyLand.Value = obj.Family_Land
                txtLeaseLand.Value = obj.Lease_Land
                txtSowingWeekMonth.Text = obj.Sowing_Week_Month
                cmbSeason.SelectedValue = obj.Season
                'If clsCommon.CompairString(clsCommon.myCstr(obj.Selected_Flag), "Y") = CompairStringResult.Equal Then
                '    cmbSelectedFlag.SelectedValue = clsCommon.myCstr("Yes")
                'Else
                '    cmbSelectedFlag.SelectedValue = clsCommon.myCstr("No")
                'End If
                cmbSelectedFlag.SelectedValue = obj.Selected_Flag
                lblVillage.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct village_name from tspl_village_master where village_code='" + obj.Village_Code + "'"))
                lblDistrict.Text = clsDistrictMaster.GetName(obj.DISTRICT_Code)
                lblGrower.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct Code from TSPL_SHEED_GROWER_MASTER where Code='" + obj.Grower_Code + "'"))
                txtDocumentNo.MyReadOnly = True
                btnsave.Text = "Update"
                btndelete.Enabled = True
                If obj.Status = 1 Then
                    UsLock.Status = ERPTransactionStatus.Approved
                    btndelete.Enabled = False
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                Else
                    UsLock.Status = ERPTransactionStatus.Pending
                    btndelete.Enabled = True
                    btnsave.Enabled = True
                    btnPost.Enabled = True
                End If

            Else
                Addnew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVillage__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVillage._MYValidating
        Dim qry As String = " select TSPL_VILLAGE_MASTER.Village_Code as [Code] ,TSPL_VILLAGE_MASTER.Village_Name as [Village Name] ,TSPL_VILLAGE_MASTER.Add1 as [Address1] ,TSPL_VILLAGE_MASTER.Add2 as [Address2] ,TSPL_VILLAGE_MASTER.City_Code as [City Code] ,TSPL_VILLAGE_MASTER.State_Code as [State Code] ,TSPL_VILLAGE_MASTER.COUNTRY_CODE as [Country Code] ,TSPL_VILLAGE_MASTER.PIN_NO as [Pin No] ,TSPL_VILLAGE_MASTER.Created_By as [Created By] ,TSPL_VILLAGE_MASTER.Created_Date as [Created Date] ,TSPL_VILLAGE_MASTER.Modified_By as [Modified By] ,TSPL_VILLAGE_MASTER.Modified_Date as [Modified Date]  From TSPL_VILLAGE_MASTER "
        txtVillage.Value = clsCommon.ShowSelectForm("VLGFND", qry, "Code", "", txtVillage.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtVillage.Value) > 0 Then
            lblVillage.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct village_name from tspl_village_master where village_code='" & txtVillage.Value & "'"))
        Else
            txtVillage.Value = ""
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

    Private Sub txtPONo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPONo._MYValidating
        Dim qry As String = "select * from TSPL_PURCHASE_ORDER_HEAD"
        txtPONo.Value = clsCommon.ShowSelectForm("POFinder", qry, "PurchaseOrder_No", "", txtPONo.Value, "", isButtonClicked)
    End Sub

    Private Sub txtGrower__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGrower._MYValidating
        Try
            Dim qry As String = "select Code, Name, Father_Name as [Father Name],Village_Code as [Village Code],Tehsil,DISTRICT_Code as [District] from TSPL_SHEED_GROWER_MASTER "
            txtGrower.Value = clsCommon.ShowSelectForm("GROWERFIND", qry, "Code", "", txtGrower.Value, "", isButtonClicked)
            If clsCommon.myLen(txtGrower.Value) > 0 Then
                lblGrower.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct Code from TSPL_SHEED_GROWER_MASTER where Code='" + txtGrower.Value + "'"))
            Else
                txtGrower.Value = ""
                lblGrower.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try

    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Addnew()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            If (myMessages.postConfirm()) Then
                SaveData()
                If (clsSeedSelectionEntry.PostData(txtDocumentNo.Value)) Then
                    msg = "Successfully Posted"

                End If
                If clsCommon.myLen(msg) > 0 Then
                    common.clsCommon.MyMessageBoxShow(msg)
                End If
                LoadData(txtDocumentNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnReverseUnpost_Click(sender As Object, e As EventArgs) Handles btnReverseUnpost.Click
        ReverseUnpost()
    End Sub
    Sub ReverseUnpost()
        Try
            If clsCommon.MyMessageBoxShow(Me, "Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                If clsSeedSelectionEntry.ReverseAndUnpost(txtDocumentNo.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtItemCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItemCode._MYValidating
        Dim qry As String = "select Item_Code as Code , Item_Desc from TSPL_ITEM_MASTER"
        txtItemCode.Value = clsCommon.ShowSelectForm("ITEMFinder", qry, "Code", "", txtItemCode.Value, "", isButtonClicked)
    End Sub
End Class