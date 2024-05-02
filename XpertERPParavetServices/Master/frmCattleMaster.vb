Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common

Public Class FrmCattleMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt("CattleMaster")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadGenderType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("Male", "Male")
        dt.Rows.Add("Female", "Female")
        cboGender.DataSource = dt
        cboGender.DisplayMember = "Name"
        cboGender.ValueMember = "Code"
    End Sub
    Sub LoadCattleStatus()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("Active", "Active")
        dt.Rows.Add("Inactive", "Inactive")
        cboCattleStatus.DataSource = dt
        cboCattleStatus.DisplayMember = "Name"
        cboCattleStatus.ValueMember = "Code"
    End Sub

    'Sub LoadCattleInType()
    '    Dim dt As New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Columns.Add("Name", GetType(String))
    '    dt.Rows.Add("Days", "Days")
    '    dt.Rows.Add("Month", "Month")
    '    dt.Rows.Add("Years", "Years")
    '    cmbInType.DataSource = dt
    '    cmbInType.DisplayMember = "Name"
    '    cmbInType.ValueMember = "Code"
    'End Sub

    Private Sub FrmCattleMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGenderType()
        LoadCattleStatus()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        AddNew()

    End Sub
    Private Sub AddNew()

        txtRegistrationNo.Value = ""
        txtDescription.Text = ""
        txtRegistrationDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtRegistrationCharge.Text = 0
        txtTagId.Text = ""
        txtCattleCode.Text = ""
        cboCattleStatus.Text = ""
        dtpDOB.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtCattleInAge.Text = ""
        cboGender.Text = ""
        rdbParentType.Checked = False
        rdbChildCattleType.Checked = True
        txtNDDBCode.Value = ""
        lblNDDBCode.Text = ""
        txtMother.Value = ""
        lblMother.Text = ""
        txtFather.Value = ""
        lblFather.Text = ""
        txtFarmer.Value = ""
        lblFarmar.Text = ""
        txtCattleColor.Value = ""
        lblCattleColor.Text = ""
        dtpInsuranceTo.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        dtpInsuranceDateFrom.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtMilkFat.Text = ""
        txtMilkQty.Text = ""
        txtCattleType.Value = ""
        lblCattleType.Text = ""
        txtBreed.Value = ""
        lblBreed.Text = ""
        txtPMCCode.Value = ""
        lblPMCCode.Text = ""
        txtMCC.Value = ""
        lblMCC.Text = ""
        txtHeadOffice.Text = ""
        txtZone.Value = ""
        lblZone.Text = ""
        txtRegion.Value = ""
        lblRegion.Text = ""
        txtArea.Value = ""
        lblArea.Text = ""
        txtBranch.Value = ""
        lblBranch.Text = ""
        txtInsuranceNo.Text = ""

        txtRegistrationNo.MyReadOnly = False
        txtCattleCode.ReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        txtRegistrationNo.Focus()

        '**********************************************************************************************
        '**********************************************************************************************

        'txtCode.Value = ""
        'txtDesc.Text = ""
        'txtCattleColor.Value = ""
        ''lblParent.Text = ""
        'txtCattleType.Value = ""
        'lblCattleType.Text = ""
        'txtBreed.Value = ""
        'lblBreed.Text = ""
        ''txtHeadOffice.Value = ""
        ''lblHeadOffice.Text = ""
        'txtZone.Value = ""
        'lblZone.Text = ""
        'txtRegion.Value = ""
        'lblRegion.Text = ""
        'txtArea.Value = ""
        'lblArea.Text = ""
        'txtBranch.Value = ""
        'lblBranch.Text = ""
        'txtMCC.Value = ""
        'lblMCC.Text = ""
        'txtFarmer.Value = ""
        'lblFarmar.Text = ""
        'rdbChildCattleType.Checked = True
        ''txtInsurnceRemark.Text = ""
        'txtMilkFat.Text = ""
        'txtMilkQty.Text = ""
        'txtCode.MyReadOnly = False
        'btnsave.Text = "Save"
        'btndelete.Enabled = False
        'txtCode.Focus()

    End Sub


    Private Sub txtBranch__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBranch._MYValidating
        Dim query As String = " select Branch_Code as [Code],Branch_Name as [Branch Name] from TSPL_BRANCH_MASTER "
        txtBranch.Value = clsCommon.ShowSelectForm("BranchCodevald", query, "Code", "", txtBranch.Value, "Code", isButtonClicked)
        Dim desc As String = "select  Branch_Name from  TSPL_BRANCH_MASTER where Branch_Code='" & txtBranch.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblBranch.Text = clsCommon.myCstr(dt.Rows(0)("Branch_Name"))
        Else
            lblBranch.Text = ""
        End If
    End Sub

    Private Sub txtArea__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtArea._MYValidating
        If clsCommon.myLen(txtZone.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Zone First", Me.Text)
            txtZone.Focus()
            txtZone.Select()
            Return
        End If
        Try
            Dim qry As String = " select Code,Name  from TSPL_AREA_MASTER "
            txtArea.Value = clsCommon.ShowSelectForm("AREAFND", qry, "Code", " Zone_Code='" + txtZone.Value + "'", txtArea.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtArea.Value) > 0 Then
                lblArea.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Name from TSPL_AREA_MASTER where Code='" + txtArea.Value + "'"))
            Else
                lblArea.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtZone._MYValidating
        Dim query As String = "  select Zone_Code as [Code],Description as [Zone Name] from TSPL_ZONE_MASTER "
        txtZone.Value = clsCommon.ShowSelectForm("ZoneCodevald", query, "Code", "", txtZone.Value, "Code", isButtonClicked)
        Dim desc As String = "select  Description from  TSPL_ZONE_MASTER where Zone_Code='" & txtZone.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblZone.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            lblZone.Text = ""
        End If
    End Sub

    Private Sub txtCattle__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCattleType._MYValidating
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

    Private Sub txtBreed__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBreed._MYValidating
        Dim query As String = "  select Bred_Type_Code as [Code], Bred_Type_Name as [Description] from TSPL_BRED_TYPE_MASTER "
        txtBreed.Value = clsCommon.ShowSelectForm("BREDVald", query, "Code", "", txtBreed.Value, "Code", isButtonClicked)
        Dim desc As String = "select  Bred_Type_Name from  TSPL_BRED_TYPE_MASTER where Bred_Type_Code='" & txtBreed.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblBreed.Text = clsCommon.myCstr(dt.Rows(0)("Bred_Type_Name"))
        Else
            lblBreed.Text = ""
        End If
    End Sub
    Private Sub txtCattleColor__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCattleColor._MYValidating
        Dim query As String = "  select Cattle_Color_Code as [Code], Cattle_Color_Name as [Description] from TSPL_CATTLE_COLOR_MASTER "
        txtCattleColor.Value = clsCommon.ShowSelectForm("CattleTypevald", query, "Code", "", txtCattleColor.Value, "Code", isButtonClicked)
        Dim desc As String = "select  Cattle_Color_Name from  TSPL_CATTLE_COLOR_MASTER where Cattle_Color_Code='" & txtCattleColor.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblCattleColor.Text = clsCommon.myCstr(dt.Rows(0)("Cattle_Color_Name"))
        Else
            lblCattleColor.Text = ""
        End If
    End Sub

    Private Sub txtRegion__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRegion._MYValidating
        'select REGION_CODE,REGION_NAME from TSPL_REGION_MASTER
        Dim query As String = "  select REGION_CODE as [Code], REGION_NAME as [Description] from TSPL_REGION_MASTER "
        txtRegion.Value = clsCommon.ShowSelectForm("RegionVald", query, "Code", "", txtRegion.Value, "Code", isButtonClicked)
        Dim desc As String = "select  REGION_NAME from  TSPL_REGION_MASTER where REGION_CODE='" & txtRegion.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblRegion.Text = clsCommon.myCstr(dt.Rows(0)("REGION_NAME"))
        Else
            lblRegion.Text = ""
        End If
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        'Dim query As String = " select MCC_Code as [Code], MCC_NAME as [Description] from TSPL_MCC_MASTER "
        'txtMCC.Value = clsCommon.ShowSelectForm("MCCVald", query, "Code", "", txtMCC.Value, "Code", isButtonClicked)
        'Dim desc As String = "select  MCC_NAME from  TSPL_MCC_MASTER where MCC_Code='" & txtMCC.Value & "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '    lblMCC.Text = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
        'Else
        '    lblMCC.Text = ""
        'End If
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
        GC.Collect()
    End Sub
    'clsCommon.GetPrintDate(obj.Insurance_Date_To, "dd/MM/yyyy")
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        AddNew()
        Dim obj As clsCattleMaster = clsCattleMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtRegistrationNo.Value = obj.Registration_No
            txtDescription.Text = obj.Registration_Desc
            txtRegistrationDate.Value = clsCommon.GetPrintDate(obj.Registration_Date, "dd/MM/yyyy")
            txtRegistrationCharge.Text = obj.Registration_Charge
            txtTagId.Text = obj.Tag_Id
            txtCattleCode.Text = obj.Cattle_Code
            cboCattleStatus.Text = obj.Cattle_Status
            dtpDOB.Value = clsCommon.GetPrintDate(obj.DOB, "dd/MM/yyyy")
            txtCattleInAge.Text = obj.Cattle_In_Age
            cboGender.Text = obj.Gender
            If obj.Type = "Parent Cattle" Then
                rdbParentType.Checked = True
            Else
                rdbChildCattleType.Checked = True
            End If
            txtNDDBCode.Value = obj.NDDB_Code
            lblNDDBCode.Text = ""
            txtMother.Value = obj.Mother_Code
            lblMother.Text = ""
            txtFather.Value = obj.Father_Code
            lblFather.Text = ""
            txtFarmer.Value = obj.Farmer_Id
            If clsCommon.myLen(obj.Farmer_Id) > 0 Then
                lblFarmar.Text = clsDBFuncationality.getSingleValue("select  MP_Name from  TSPL_MP_MASTER where MP_Code='" & obj.Farmer_Id & "'")
            End If

            txtCattleColor.Value = obj.Cattle_Color_Code
            If clsCommon.myLen(obj.Cattle_Color_Code) > 0 Then
                lblCattleColor.Text = clsDBFuncationality.getSingleValue("select  Cattle_Color_Name from  TSPL_CATTLE_COLOR_MASTER where Cattle_Color_Code='" & obj.Cattle_Color_Code & "'")
            End If

            txtCattleType.Value = obj.Cattle_Type_Code
            If clsCommon.myLen(obj.Cattle_Type_Code) > 0 Then
                lblCattleType.Text = clsDBFuncationality.getSingleValue("select  Cattle_Type_Name from  TSPL_CATTLE_TYPE_MASTER where Cattle_Type_Code='" & obj.Cattle_Type_Code & "'")
            End If
            txtBreed.Value = obj.Bred_Type_Code
            If clsCommon.myLen(obj.Bred_Type_Code) > 0 Then
                lblBreed.Text = clsDBFuncationality.getSingleValue("select  Bred_Type_Name from  TSPL_BRED_TYPE_MASTER where Bred_Type_Code='" & obj.Bred_Type_Code & "'")
            End If
            txtPMCCode.Value = obj.PMC_Code
            If clsCommon.myLen(obj.PMC_Code) > 0 Then
                lblPMCCode.Text = clsDBFuncationality.getSingleValue("select  VLC_Name from  TSPL_VLC_MASTER_HEAD where VLC_Code='" & obj.PMC_Code & "'")
            End If

            txtMCC.Value = obj.MCC_Code
            If clsCommon.myLen(obj.MCC_Code) > 0 Then
                lblMCC.Text = clsDBFuncationality.getSingleValue("select  MCC_NAME from  TSPL_MCC_MASTER where MCC_Code='" & obj.MCC_Code & "'")
            End If


            txtHeadOffice.Text = obj.Head_Office
            txtZone.Value = obj.Zone_Code
            If clsCommon.myLen(obj.Zone_Code) > 0 Then
                lblZone.Text = clsDBFuncationality.getSingleValue("select  Description from  TSPL_ZONE_MASTER where Zone_Code='" & obj.Zone_Code & "'")
            End If
            txtRegion.Value = obj.Region_Code
            If clsCommon.myLen(obj.Region_Code) > 0 Then
                lblRegion.Text = clsDBFuncationality.getSingleValue("select  REGION_NAME from  TSPL_REGION_MASTER where REGION_CODE='" & obj.Region_Code & "'")
            End If

            txtArea.Value = obj.Area_Code
            If clsCommon.myLen(obj.Area_Code) > 0 Then
                lblArea.Text = clsDBFuncationality.getSingleValue("select Name from TSPL_AREA_MASTER where Code='" & obj.Area_Code & "'")
            End If
            txtBranch.Value = obj.Branch_Code
            If clsCommon.myLen(obj.Branch_Code) > 0 Then
                lblBranch.Text = clsDBFuncationality.getSingleValue("select  Branch_Name from  TSPL_BRANCH_MASTER where Branch_Code='" & obj.Branch_Code & "'")
            End If
            txtInsuranceNo.Text = obj.Insurance_No
            dtpInsuranceTo.Value = clsCommon.GetPrintDate(obj.Insurance_Date_To, "dd/MM/yyyy")
            dtpInsuranceDateFrom.Value = clsCommon.GetPrintDate(obj.Insurance_Date_From, "dd/MM/yyyy")
            txtMilkFat.Text = obj.Milk_Fat_Percentage
            txtMilkQty.Text = obj.Milk_Qty_Per_Day
            'calculateAge(dtpDOB.Value)
            AgeCalculate()
            txtRegistrationNo.MyReadOnly = True
            txtCattleCode.ReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
        End If
    End Sub
    Private Sub txtPMCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPMCCode._MYValidating
        'Dim query As String = " select VLC_Code as [Code], VLC_Name as [Description] from TSPL_VLC_MASTER_HEAD "
        'txtPMCCode.Value = clsCommon.ShowSelectForm("PMCVald", query, "Code", "", txtPMCCode.Value, "Code", isButtonClicked)
        'Dim desc As String = "select  VLC_Name from  TSPL_VLC_MASTER_HEAD where VLC_Code='" & txtPMCCode.Value & "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '    lblPMCCode.Text = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
        'Else
        '    lblPMCCode.Text = ""
        'End If

        Dim query As String = " select  TSPL_VLC_MASTER_HEAD.VLC_Code as VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.MCC,TSPL_MCC_MASTER.MCC_NAME from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC "
        txtPMCCode.Value = clsCommon.ShowSelectForm("PMCVald", query, "VLC_Code", "", txtPMCCode.Value, "VLC_Code", isButtonClicked)
        Dim desc As String = "select  TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name as VLC_Name,TSPL_VLC_MASTER_HEAD.MCC as MCC,TSPL_MCC_MASTER.MCC_NAME from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC where VLC_Code='" & txtPMCCode.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblPMCCode.Text = clsCommon.myCstr(dt.Rows(0)("VLC_Name"))
            txtMCC.Value = clsCommon.myCstr(dt.Rows(0)("MCC"))
            lblMCC.Text = clsCommon.myCstr(dt.Rows(0)("MCC_NAME"))
        Else
            lblPMCCode.Text = ""
            txtMCC.Value = ""
            lblMCC.Text = ""
        End If
    End Sub

    Private Sub txtFarmer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFarmer._MYValidating
        Dim query As String = " select MP_Code as [Code], MP_Name as [Description] from TSPL_MP_MASTER "
        txtFarmer.Value = clsCommon.ShowSelectForm("FarmerVald", query, "Code", "", txtFarmer.Value, "Code", isButtonClicked)
        Dim desc As String = "select  MP_Name from  TSPL_MP_MASTER where MP_Code='" & txtFarmer.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblFarmar.Text = clsCommon.myCstr(dt.Rows(0)("MP_Name"))
        Else
            lblFarmar.Text = ""
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtRegistrationNo.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtRegistrationNo.Value)) > 30 Then
                myMessages.blankValue(Me, "Registration No", Me.Text)

                txtRegistrationNo.Focus()
                txtRegistrationNo.Select()
                Errorcontrol.SetError(txtRegistrationNo, "Registration No")
                Return False
            Else
                Errorcontrol.ResetError(txtRegistrationNo)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtCattleCode.Text)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtCattleCode.Text)) > 30 Then
                myMessages.blankValue(Me, "Cattle Code", Me.Text)
                txtCattleCode.Focus()
                txtCattleCode.Select()
                Errorcontrol.SetError(txtCattleCode, "Cattle Code")
                Return False
            Else
                Errorcontrol.ResetError(txtCattleCode)
            End If
            If (DateTime.Compare(DateTime.Now, dtpDOB.Value) < 0) Then
                common.clsCommon.MyMessageBoxShow("DOB Date not greter then Current Date", Me.Text, MessageBoxButtons.OK)
                dtpDOB.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                dtpDOB.Focus()
                txtCattleInAge.Text = ""
                Return False
            End If

            If (DateTime.Compare(dtpInsuranceTo.Value, dtpInsuranceDateFrom.Value) < 0) Then
                common.clsCommon.MyMessageBoxShow("Insurance From Date not greter then Insurance To Date", Me.Text, MessageBoxButtons.OK)
                dtpInsuranceTo.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                dtpInsuranceDateFrom.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                dtpInsuranceDateFrom.Focus()
                Return False
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtDescription.Text)) <= 0 Then
                myMessages.blankValue(Me, "Description", Me.Text)
                txtDescription.Focus()
                txtDescription.Select()
                Errorcontrol.SetError(txtDescription, "Description")
                Return False
            Else
                Errorcontrol.ResetError(txtDescription)
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
                Dim obj As New clsCattleMaster()
                obj.Registration_No = txtRegistrationNo.Value
                obj.Registration_Desc = txtDescription.Text
                obj.Registration_Date = txtRegistrationDate.Value
                obj.Registration_Charge = txtRegistrationCharge.Text
                obj.Tag_Id = txtTagId.Text
                obj.Cattle_Code = txtCattleCode.Text
                obj.Cattle_Status = cboCattleStatus.Text
                obj.DOB = dtpDOB.Value
                obj.Cattle_In_Age = txtCattleInAge.Text
                obj.Gender = cboGender.Text

                If rdbParentType.Checked = True Then
                    obj.Type = rdbParentType.Text
                Else
                    obj.Type = rdbChildCattleType.Text
                End If
                obj.NDDB_Code = txtNDDBCode.Value
                obj.Mother_Code = txtMother.Value
                obj.Father_Code = txtFather.Value
                obj.Farmer_Id = txtFarmer.Value
                obj.Cattle_Color_Code = txtCattleColor.Value
                obj.Cattle_Type_Code = txtCattleType.Value
                obj.Bred_Type_Code = txtBreed.Value
                obj.PMC_Code = txtPMCCode.Value
                obj.MCC_Code = txtMCC.Value
                obj.Head_Office = txtHeadOffice.Text
                obj.Zone_Code = txtZone.Value
                obj.Region_Code = txtRegion.Value
                obj.Area_Code = txtArea.Value
                obj.Branch_Code = txtBranch.Value
                obj.Insurance_No = txtInsuranceNo.Text
                obj.Insurance_Date_To = dtpInsuranceTo.Value
                obj.Insurance_Date_From = dtpInsuranceDateFrom.Value
                obj.Milk_Fat_Percentage = txtMilkFat.Value
                obj.Milk_Qty_Per_Day = txtMilkQty.Text

                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Registration_No) from TSPL_Cattle_Master WHERE Registration_No='" + obj.Registration_No + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (clsCattleMaster.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Registration_No, NavigatorType.Current)
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
            If clsCommon.myLen(txtRegistrationNo.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure? do you want to delete this Code ('" + txtRegistrationNo.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_Cattle_Master WHERE Registration_No='" + txtRegistrationNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub

    Private Sub txtRegistrationNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtRegistrationNo._MYNavigator
        Try
            LoadData(txtRegistrationNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtRegistrationNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRegistrationNo._MYValidating
        Dim str As String = "select count(*) from TSPL_Cattle_Master where Registration_No ='" + txtRegistrationNo.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtRegistrationNo.MyReadOnly = False
        Else
            txtRegistrationNo.MyReadOnly = True
        End If

        If txtRegistrationNo.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select Registration_No As [Code],Registration_Desc As [Name] from TSPL_Cattle_Master "
            txtRegistrationNo.Value = clsCommon.ShowSelectForm("TSPL_Cattle_Master", qry, "Code", "", txtRegistrationNo.Value, "TSPL_Cattle_Master.Registration_No", isButtonClicked)
            If clsCommon.myLen(txtRegistrationNo.Value) > 0 Then
                Dim objOT As clsCattleMaster
                objOT = clsCattleMaster.GetData(txtRegistrationNo.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtRegistrationNo.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub

    'Export
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim str As String
        str = "select Registration_No as [Registration No],Registration_Desc as [Registration Desc] , Convert(varchar, Registration_Date,103) as [Registration Date] ,isnull (Registration_Charge,0) as [Registration Charge],Tag_Id as [Tag Id],Cattle_Code as [Cattle Code],Cattle_Status as [Cattle Status],convert(varchar,DOB,103) as DOB ,Cattle_In_Age as [Cattle In Age],Gender,Type ,NDDB_Code as [NDDB Code], Mother_Code as [Mother Code],Father_Code as [Father Code], Farmer_Id as [Farmer Id] ,Cattle_Color_Code as [Cattle Color Code] ,Cattle_Type_Code as [Cattle Type Code],Bred_Type_Code as [Breed Type Code],PMC_Code as [PMC Code],MCC_Code as [MCC Code],Head_Office as [Head Office],Zone_Code as [Zone Code],Region_Code as [Region Code],Area_Code as [Area Code],Branch_Code as [Branch Code],Insurance_No as [Insurance No],convert(varchar,Insurance_Date_To,103) as [Insurance Date To] ,convert(varchar,Insurance_Date_From,103) as [Insurance Date From],Milk_Qty_Per_Day as [Milk Qty Per Day],Milk_Fat_Percentage as [Milk Fat Percentage]   from TSPL_Cattle_Master "
        transportSql.ExporttoExcel(str, Me)
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

    Private Sub FrmCattleMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Registration No", "Registration Desc", "Registration Date", "Registration Charge", "Tag Id", "Cattle Code", "Cattle Status", "DOB", "Cattle In Age", "Gender", "Type", "NDDB Code", "Mother Code", "Father Code", "Farmer Id", "Cattle Color Code", "Cattle Type Code", "Breed Type Code", "PMC Code", "MCC Code", "Head Office", "Zone Code", "Region Code", "Area Code", "Branch Code", "Insurance No", "Insurance Date To", "Insurance Date From", "Milk Qty Per Day", "Milk Fat Percentage") Then
            Dim linno As Integer = 1
            Try
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New clsCattleMaster()
                    Dim strRegistration_No As String = clsCommon.myCstr(grow.Cells("Registration No").Value)
                    Dim strRegistration_Desc As String = clsCommon.myCstr(grow.Cells("Registration Desc").Value)
                    Dim strRegistration_Date As String = clsCommon.myCstr(grow.Cells("Registration Date").Value)
                    Dim strRegistration_Charge As String = clsCommon.myCdbl(grow.Cells("Registration Charge").Value)
                    Dim strTag_Id As String = clsCommon.myCstr(grow.Cells("Tag Id").Value)
                    Dim strCattleCode As String = clsCommon.myCstr(grow.Cells("Cattle Code").Value)
                    Dim strCattle_Status As String = clsCommon.myCstr(grow.Cells("Cattle Status").Value)
                    Dim strDOB As String = clsCommon.myCstr(grow.Cells("DOB").Value)
                    Dim strCattle_In_Age As String = clsCommon.myCstr(grow.Cells("Cattle In Age").Value)
                    Dim strGender As String = clsCommon.myCstr(grow.Cells("Gender").Value)
                    Dim strType As String = clsCommon.myCstr(grow.Cells("Type").Value)
                    Dim strNDDB_Code As String = clsCommon.myCstr(grow.Cells("NDDB Code").Value)
                    Dim strMother_Code As String = clsCommon.myCstr(grow.Cells("Mother Code").Value)
                    Dim strFather_Code As String = clsCommon.myCstr(grow.Cells("Father Code").Value)
                    Dim strFarmer_Id As String = clsCommon.myCstr(grow.Cells("Farmer Id").Value)
                    Dim strCattle_Color_Code As String = clsCommon.myCstr(grow.Cells("Cattle Color Code").Value)
                    Dim strCattle_Type_Code As String = clsCommon.myCstr(grow.Cells("Cattle Type Code").Value)
                    Dim strBreed_Type_Code As String = clsCommon.myCstr(grow.Cells("Breed Type Code").Value)
                    Dim strPMC_Code As String = clsCommon.myCstr(grow.Cells("PMC Code").Value)
                    Dim strMCC_Code As String = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                    Dim strHead_Office As String = clsCommon.myCstr(grow.Cells("Head Office").Value)
                    Dim strZone_Code As String = clsCommon.myCstr(grow.Cells("Zone Code").Value)
                    Dim strRegion_Code As String = clsCommon.myCstr(grow.Cells("Region Code").Value)
                    Dim strArea_Code As String = clsCommon.myCstr(grow.Cells("Area Code").Value)
                    Dim strBranch_Code As String = clsCommon.myCstr(grow.Cells("Branch Code").Value)
                    Dim strInsurance_No As String = clsCommon.myCstr(grow.Cells("Insurance No").Value)
                    Dim strInsurance_Date_To As String = clsCommon.myCstr(grow.Cells("Insurance Date To").Value)
                    Dim strInsurance_Date_From As String = clsCommon.myCstr(grow.Cells("Insurance Date From").Value)
                    Dim strMilk_Qty_Per_Day As String = clsCommon.myCstr(grow.Cells("Milk Qty Per Day").Value)
                    Dim strMilk_Fat_Percentage As String = clsCommon.myCstr(grow.Cells("Milk Fat Percentage").Value)
                    linno += 1


                    Dim ChkRegistration_No As String = ""
                    Dim ChkRegistration_Desc As String = ""
                    Dim ChkRegistration_Date As String = ""
                    Dim ChkTag_Id As String = ""
                    Dim ChkCattle_Status As String = ""
                    Dim ChkDOB As String = ""
                    Dim ChkGender As String = ""
                    Dim ChkType As String = ""
                    Dim ChkNDDB_Code As String = ""
                    Dim ChkMother_Code As String = ""
                    Dim ChkFather_Code As String = ""
                    Dim ChkFarmer_Id As String = ""
                    Dim ChkCattle_Color_Code As String = ""
                    Dim ChkCattle_Type_Code As String = ""
                    Dim ChkBred_Type_Code As String = ""
                    Dim ChkPMC_Code As String = ""
                    Dim ChkMCC_Code As String = ""
                    Dim ChkHead_Office As String = ""
                    Dim ChkZone_Code As String = ""
                    Dim ChkRegion_Code As String = ""
                    Dim ChkArea_Code As String = ""
                    Dim ChkBranch_Code As String = ""
                    Dim ChkInsurance_No As String = ""
                    Dim ChkInsurance_Date_From As String = ""
                    Dim ChkInsurance_Date_To As String = ""
                    Dim ChkMilk_Qty_Per_Day As String = ""
                    Dim ChkMilk_Fat_Percentage As String = ""
                    Dim ChkCattle_Code As String = ""
                    Dim ChkRegistration_Charge As String = ""
                    Dim ChkData As String = " Select Registration_No,Registration_Desc,Registration_Date,Tag_Id,Cattle_Status,DOB,Gender,Type,NDDB_Code,Mother_Code,Father_Code,Farmer_Id, Cattle_Color_Code,Cattle_Type_Code,Bred_Type_Code,PMC_Code,MCC_Code,Head_Office,Zone_Code,Region_Code,Area_Code, Branch_Code,Insurance_No,Insurance_Date_From,Insurance_Date_To,Milk_Qty_Per_Day,Milk_Fat_Percentage, Cattle_Code, Registration_Charge from TSPL_Cattle_Master where Registration_No = '" + strRegistration_No + "' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(ChkData)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        ChkRegistration_No = clsCommon.myCstr(dt.Rows(0)("Registration_No"))
                        ChkRegistration_Desc = clsCommon.myCstr(dt.Rows(0)("Registration_Desc"))
                        ChkRegistration_Date = clsCommon.myCstr(dt.Rows(0)("Registration_Date"))
                        ChkTag_Id = clsCommon.myCdbl(dt.Rows(0)("Tag_Id"))
                        ChkCattle_Status = clsCommon.myCstr(dt.Rows(0)("Cattle_Status"))
                        ChkDOB = clsCommon.myCstr(dt.Rows(0)("DOB"))
                        ChkGender = clsCommon.myCstr(dt.Rows(0)("Gender"))
                        ChkType = clsCommon.myCstr(dt.Rows(0)("Type"))
                        ChkNDDB_Code = clsCommon.myCstr(dt.Rows(0)("NDDB_Code"))
                        ChkMother_Code = clsCommon.myCstr(dt.Rows(0)("Mother_Code"))
                        ChkFather_Code = clsCommon.myCstr(dt.Rows(0)("Father_Code"))
                        ChkFarmer_Id = clsCommon.myCstr(dt.Rows(0)("Farmer_Id"))
                        ChkCattle_Color_Code = clsCommon.myCstr(dt.Rows(0)("Cattle_Color_Code"))
                        ChkCattle_Type_Code = clsCommon.myCstr(dt.Rows(0)("Cattle_Type_Code"))
                        ChkBred_Type_Code = clsCommon.myCstr(dt.Rows(0)("Bred_Type_Code"))
                        ChkPMC_Code = clsCommon.myCstr(dt.Rows(0)("PMC_Code"))
                        ChkMCC_Code = clsCommon.myCstr(grow.Cells("MCC_Code"))
                        ChkHead_Office = clsCommon.myCstr(dt.Rows(0)("Head_Office"))
                        ChkZone_Code = clsCommon.myCstr(dt.Rows(0)("Zone_Code"))
                        ChkRegion_Code = clsCommon.myCstr(dt.Rows(0)("Region_Code"))
                        ChkArea_Code = clsCommon.myCstr(dt.Rows(0)("Area_Code"))
                        ChkBranch_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
                        ChkInsurance_No = clsCommon.myCstr(dt.Rows(0)("Insurance_No"))
                        ChkInsurance_Date_From = clsCommon.myCstr(dt.Rows(0)("Insurance_Date_From"))
                        ChkInsurance_Date_To = clsCommon.myCstr(dt.Rows(0)("Insurance_Date_To"))
                        ChkMilk_Qty_Per_Day = clsCommon.myCstr(dt.Rows(0)("Milk_Qty_Per_Day"))
                        ChkMilk_Fat_Percentage = clsCommon.myCstr(dt.Rows(0)("Milk_Fat_Percentage"))
                        ChkCattle_Code = clsCommon.myCstr(dt.Rows(0)("Cattle_Code"))
                        ChkRegistration_Charge = clsCommon.myCstr(dt.Rows(0)("Registration_Charge"))
                        
                    End If

                    If (clsCommon.myLen(ChkRegistration_No) > 0) Then
                        ' Update Record in Cattle Master 

                    Else
                        ' Insert New Record in Cattle Master
                        If clsCommon.myLen(strRegistration_No) <= 0 Then
                            Throw New Exception("Registration No should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                        ElseIf clsCommon.myLen(strRegistration_No) > 30 Then
                            Throw New Exception("Please check ! length of strRegistration No should be 30 at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    
                    If (clsCommon.myLen(ChkRegistration_Date) > 0) Then
                        ' Update Record in Cattle Master 
                        If (DateTime.Compare(DateTime.Now, strRegistration_Date) < 0) Then
                            Throw New Exception("Registration Date should not be greter then Current Date at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        ' Insert New Record in Cattle Master
                        If clsCommon.myLen(strRegistration_Date) <= 0 Then
                            Throw New Exception("Registration Date should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        If (DateTime.Compare(DateTime.Now, strRegistration_Date) < 0) Then
                            Throw New Exception("Registration Date should not be greter then Current Date at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If (clsCommon.myLen(ChkDOB) > 0) Then
                        ' Update Record in Cattle Master 
                        If (DateTime.Compare(DateTime.Now, strDOB) < 0) Then
                            Throw New Exception("DOB Date not greter then Current Date at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        ' Insert New Record in Cattle Master
                        If (DateTime.Compare(DateTime.Now, strDOB) < 0) Then
                            Throw New Exception("DOB Date not greter then Current Date at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If clsCommon.myLen(ChkGender) > 0 Then
                        ' Update Record in Cattle Master 
                        ' (clsCommon.CompairString(strGender.ToUpper(), "MALE") <> CompairStringResult.Equal) or (clsCommon.CompairString(strGender.ToUpper(), "FEMALE") <> CompairStringResult.Equal)
                        'If strGender <> "Male" Or strGender <> "Female" Then

                        If (clsCommon.CompairString(strGender.ToUpper(), "MALE") <> CompairStringResult.Equal) Then
                            If (clsCommon.CompairString(strGender.ToUpper(), "FEMALE") <> CompairStringResult.Equal) Then
                                Throw New Exception("Gender Should be Male or Female at line no. " + clsCommon.myCstr(linno) + ".")
                            End If

                        End If
                    Else
                        ' Insert New Record in Cattle Master
                        If clsCommon.myLen(strGender) <= 0 Then
                            Throw New Exception("Gender should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        If (clsCommon.CompairString(strGender.ToUpper(), "MALE") <> CompairStringResult.Equal) Then
                            If (clsCommon.CompairString(strGender.ToUpper(), "FEMALE") <> CompairStringResult.Equal) Then
                                Throw New Exception("Gender Should be Male or Female at line no. " + clsCommon.myCstr(linno) + ".")
                            End If

                        End If
                    End If
                    If clsCommon.myLen(ChkCattle_Status) > 0 Then
                        ' Update Record in Cattle Master 
                       
                        If (clsCommon.CompairString(strCattle_Status.ToUpper(), "ACTIVE") <> CompairStringResult.Equal) Then
                            If (clsCommon.CompairString(strCattle_Status.ToUpper(), "INACTVE") <> CompairStringResult.Equal) Then
                                Throw New Exception("Cattle Status Should be Active or Inactive at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                    Else
                        ' Insert New Record in Cattle Master
                        If clsCommon.myLen(strCattle_Status) <= 0 Then
                            Throw New Exception("Cattle Status should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        If (clsCommon.CompairString(strCattle_Status.ToUpper(), "ACTIVE") <> CompairStringResult.Equal) Then
                            If (clsCommon.CompairString(strCattle_Status.ToUpper(), "INACTVE") <> CompairStringResult.Equal) Then
                                Throw New Exception("Cattle Status Should be Active or Inactive at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                    End If

                    If clsCommon.myLen(ChkType) > 0 Then
                        ' Update Record in Cattle Master 
                        If clsCommon.CompairString(strType.ToUpper(), "PARENT CATTLE") <> CompairStringResult.Equal Then
                            If clsCommon.CompairString(strType.ToUpper(), "CHILD CATTLE") <> CompairStringResult.Equal Then
                                Throw New Exception("Type Should be 'Parent Cattle' or 'Child Cattle' at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                    Else
                        ' Insert New Record in Cattle Master
                        If clsCommon.myLen(strType) <= 0 Then
                            Throw New Exception("Type should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        If clsCommon.CompairString(strType.ToUpper(), "PARENT CATTLE") <> CompairStringResult.Equal Then
                            If clsCommon.CompairString(strType.ToUpper(), "CHILD CATTLE") <> CompairStringResult.Equal Then
                                Throw New Exception("Type Should be 'Parent Cattle' or 'Child Cattle' at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                    End If

                    If clsCommon.myLen(ChkNDDB_Code) > 0 Then
                        ' Update Record in Cattle Master 
                        'ChkNDDB_Code
                        Dim desc As String = "select NDDB_Code from TSPL_CATTLE_MASTER where NDDB_Code='" + strNDDB_Code + "' and Registration_No <>'" & strRegistration_No & "'  "
                        Dim dtt As DataTable = clsDBFuncationality.GetDataTable(desc)
                        If (dtt IsNot Nothing AndAlso dtt.Rows.Count > 0) Then
                            Throw New Exception("Invalid NDDB No,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        ' Insert New Record in Cattle Master
                        If clsCommon.myLen(strNDDB_Code) > 0 Then
                            Dim desc As String = "select TSPL_Paravet_NDDB_Master.NDDB_NO as NDDB_NO ,TSPL_Paravet_NDDB_Master.NDDB_Desc from TSPL_Paravet_NDDB_Master left outer join TSPL_CATTLE_MASTER on TSPL_Paravet_NDDB_Master.NDDB_No=TSPL_CATTLE_MASTER.NDDB_Code where TSPL_CATTLE_MASTER.NDDB_Code  is null  and TSPL_Paravet_NDDB_Master.NDDB_No ='" + strNDDB_Code + "' "
                            Dim dtt As DataTable = clsDBFuncationality.GetDataTable(desc)
                            If (dtt IsNot Nothing AndAlso dt.Rows.Count < 0) Then
                                Throw New Exception("Invalid NDDB No,Line No " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                    End If

                    If clsCommon.myLen(strNDDB_Code) > 0 Then
                        Dim TagId As String = clsDBFuncationality.getSingleValue(" select Tag_Prefix+'-'+Tag_SNO as 'Tag Id'  from TSPL_PARAVET_NDDB_MASTER where NDDB_No='" & strNDDB_Code & "'")

                        If (clsCommon.myLen(TagId) < 0) Then
                            Throw New Exception("Please Check Prefix and SNo  against NDDB No ,Line No " + clsCommon.myCstr(linno) + ".")
                        Else
                            strTag_Id = TagId
                        End If
                    End If

                    If clsCommon.myLen(ChkCattle_Code) > 0 Then
                        ' Update Record in Cattle Master                         
                        Dim CattleCode As String = clsDBFuncationality.getSingleValue(" select count (*) from TSPL_CATTLE_MASTER where Cattle_Code='" & strCattleCode & "' and Registration_No <>'" & strRegistration_No & "'")
                        If CattleCode > 0 Then
                            Throw New Exception("Invalid Cattle Code, Cattle Code already used another Cattle  ,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        ' Insert New Record in Cattle Master
                        If clsCommon.myLen(strCattleCode) <= 0 Then
                            Throw New Exception("Cattle Code should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim CattleCode As String = clsDBFuncationality.getSingleValue(" select count (*) from TSPL_CATTLE_MASTER where Cattle_Code='" & strCattleCode & "' and Registration_No <>'" & strRegistration_No & "'")
                        If CattleCode > 0 Then
                            Throw New Exception("Invalid Cattle Code, Cattle Code already used another Cattle  ,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If clsCommon.myLen(ChkMother_Code) > 0 Then
                        If strType.ToUpper() = "CHILD CATTLE" Then
                            If clsCommon.myLen(strMother_Code) > 0 Then

                                Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_CATTLE_MASTER where Gender='Female' and Type = 'Parent Cattle' and Cattle_Code <> '" & strCattleCode & "' and Cattle_Code = '" & strMother_Code & "' ")
                                If Values <= 0 Then
                                    Throw New Exception("Invalid Mother Name,Line No " + clsCommon.myCstr(linno) + ".")
                                End If

                            End If
                        Else
                            strMother_Code = ""
                        End If

                    Else
                        If strType.ToUpper() = "CHILD CATTLE" Then
                            If clsCommon.myLen(strMother_Code) > 0 Then
                                Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_CATTLE_MASTER where Gender='Female' and Type = 'Parent Cattle' and Cattle_Code <> '" & strCattleCode & "' and Cattle_Code = '" & strMother_Code & "' ")
                                If Values <= 0 Then
                                    Throw New Exception("Invalid Mother Mother,Line No " + clsCommon.myCstr(linno) + ".")
                                End If

                            End If
                        Else
                            strMother_Code = ""
                        End If

                    End If

                    If clsCommon.myLen(ChkFather_Code) > 0 Then
                        If strType.ToUpper() = "CHILD CATTLE" Then
                            If clsCommon.myLen(strFather_Code) > 0 Then
                                Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_PARAVET_BULL_MASTER where bull_No='" & strFather_Code & "' and Status= 'Active' ")
                                If Values <= 0 Then
                                    Throw New Exception("Invalid Father Code,Line No " + clsCommon.myCstr(linno) + ".")
                                End If

                            End If
                        Else
                            strFather_Code = ""
                        End If

                    Else
                        If strType.ToUpper() = "CHILD CATTLE" Then
                            If clsCommon.myLen(strFather_Code) > 0 Then
                                Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_PARAVET_BULL_MASTER where bull_No='" & strFather_Code & "' and Status= 'Active' ")
                                If Values <= 0 Then
                                    Throw New Exception("Invalid Father Code,Line No " + clsCommon.myCstr(linno) + ".")
                                End If

                            End If
                        Else
                            strFather_Code = ""
                        End If
                    End If

                    If clsCommon.myLen(ChkFarmer_Id) > 0 Then
                        If clsCommon.myLen(strFarmer_Id) > 0 Then
                            Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_MP_MASTER where MP_Code= '" & strFarmer_Id & "'  ")
                            If Values <= 0 Then
                                Throw New Exception("Invalid Farmer ID Code,Line No " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                    Else
                        If clsCommon.myLen(strFarmer_Id) <= 0 Then
                            Throw New Exception("Farmer Id should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        If clsCommon.myLen(strFarmer_Id) > 0 Then
                            Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_MP_MASTER where MP_Code= '" & strFarmer_Id & "'  ")
                            If Values <= 0 Then
                                Throw New Exception("Invalid Farmer ID Code,Line No " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                    End If

                    If clsCommon.myLen(strCattle_Color_Code) <= 0 Then
                        Throw New Exception("Cattle Color Code should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strCattle_Color_Code) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue(" select count (*) from TSPL_CATTLE_COLOR_MASTER where Cattle_Color_Code = '" & strCattle_Color_Code & "'  ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid Cattle Color Code,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If clsCommon.myLen(strCattle_Type_Code) <= 0 Then
                        Throw New Exception("Cattle Type Code should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strCattle_Type_Code) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_CATTLE_TYPE_MASTER where Cattle_Type_Code=  '" & strCattle_Type_Code & "'  ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid Cattle Type Code,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If clsCommon.myLen(strBreed_Type_Code) <= 0 Then
                        Throw New Exception("Breed Type Code should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strBreed_Type_Code) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_BRED_TYPE_MASTER where Bred_Type_Code='" & strBreed_Type_Code & "'  ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid Breed Type Code,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If clsCommon.myLen(strPMC_Code) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue("  select count(*) from TSPL_VLC_MASTER_HEAD where VLC_Code= '" & strPMC_Code & "'  ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid PMC Code,Line No " + clsCommon.myCstr(linno) + ".")
                        Else
                            strMCC_Code = clsDBFuncationality.getSingleValue("  select MCC from TSPL_VLC_MASTER_HEAD where VLC_Code= '" & strPMC_Code & "'  ")
                        End If
                    End If

                    If clsCommon.myLen(strZone_Code) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue(" select count (*) from TSPL_ZONE_MASTER where Zone_Code ='" & strZone_Code & "'  ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid Zone Code,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If clsCommon.myLen(strRegion_Code) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_REGION_MASTER where REGION_CODE='" & strRegion_Code & "'  ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid Region Code,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If clsCommon.myLen(strArea_Code) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*)  from TSPL_AREA_MASTER where Code='" & strArea_Code & "'  ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid Area Code,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If


                    If clsCommon.myLen(strBranch_Code) > 0 Then
                        Dim Values As String = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_BRANCH_MASTER where Branch_Code='" & strBranch_Code & "'  ")
                        If Values <= 0 Then
                            Throw New Exception("Invalid Branch Code,Line No " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    If clsCommon.myLen(strInsurance_No) > 0 Then

                    End If
                    If (DateTime.Compare(strInsurance_Date_To, strInsurance_Date_From) < 0) Then
                        Throw New Exception("Insurance From Date not greter then Insurance To Date at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strRegistration_No) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Cattle_Master where Registration_No='" + strRegistration_No + "' ") > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If

                    obj.Registration_No = strRegistration_No
                    obj.Registration_Desc = strRegistration_Desc
                    obj.Registration_Date = strRegistration_Date
                    obj.Registration_Charge = strRegistration_Charge
                    obj.Tag_Id = strTag_Id
                    obj.Cattle_Code = strCattleCode
                    obj.Cattle_Status = strCattle_Status
                    obj.DOB = strDOB
                    obj.Cattle_In_Age = strCattle_In_Age
                    obj.Gender = strGender
                    obj.Type = strType
                    obj.NDDB_Code = strNDDB_Code
                    obj.Mother_Code = strMother_Code
                    obj.Father_Code = strFather_Code
                    obj.Farmer_Id = strFarmer_Id
                    obj.Cattle_Color_Code = strCattle_Color_Code
                    obj.Cattle_Type_Code = strCattle_Type_Code
                    obj.Bred_Type_Code = strBreed_Type_Code
                    obj.PMC_Code = strPMC_Code
                    obj.MCC_Code = strMCC_Code
                    obj.Head_Office = strHead_Office
                    obj.Zone_Code = strZone_Code
                    obj.Region_Code = strRegion_Code
                    obj.Area_Code = strArea_Code
                    obj.Branch_Code = strBranch_Code
                    obj.Insurance_No = strInsurance_No
                    obj.Insurance_Date_To = strInsurance_Date_To
                    obj.Insurance_Date_From = strInsurance_Date_From
                    obj.Milk_Fat_Percentage = strMilk_Fat_Percentage
                    obj.Milk_Qty_Per_Day = strMilk_Qty_Per_Day

                    clsCattleMaster.SaveData(obj, IsNewEntry)
                    'LoadData(obj.Registration_No, NavigatorType.Current)

                Next
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Me.Close()
        GC.Collect()
    End Sub

    'Public Sub CalculateAge()
    '    Dim a As String
    '    Dim b As String
    '    Dim c As String
    '    Dim birth As DateTime
    '    birth = dtpDOB.Value
    '    a = birth.Year
    '    b = birth.Month
    '    c = birth.Date
    '    Dim DOB As New DateTime(a, b, c)
    '    Dim Years As Integer = DateDiff(DateInterval.Year, DOB, Now) - 1
    '    Dim Months As Integer = DateDiff(DateInterval.Month, DOB, Now) Mod 12
    '    Dim days As Integer = DateDiff(DateInterval.Day, DOB, Now) Mod 30 - 10
    '    txtCattleInAge.Text = Years & " Years, " & Months & " Months " & days & "Days"

    'End Sub

    Sub calculateAge(ByVal birthDate As Date)
        Dim mySpan As Int16 = CInt(Date.Now.Subtract(birthDate).TotalDays)
        Dim nowYear As Int16 = Date.Now.Year
        Dim nowMonth As Int16 = Date.Now.Month
        Dim birthYear As Int16 = birthDate.Year
        Dim birthMonth As Int16 = birthDate.Month

        Dim yearCount As Int16
        For yearCount = Date.Now.Year To birthYear Step -1
            If yearCount Mod 4 = 0 Then
                Select Case True
                    Case yearCount = nowYear And nowMonth < 3
                        'This is a leap year but we're not past Feb yet
                        'Do nothing
                    Case yearCount = birthYear And birthMonth > 2
                        'They were born after Feb in a leap year
                        'Do nothing
                    Case Else
                        'This was a full leap year, subtract a day to make it 365
                        mySpan -= 1
                End Select
            End If
        Next

        Dim myYears As Int16 = mySpan / 365
        Dim myDays As Int16 = mySpan - (myYears * 365)
        If myDays < 0 Then
            myYears -= 1
            myDays = 365 + myDays ' myDays is negative so this will subtract
        End If
        txtCattleInAge.Text = "You are " & myYears & " years and " & myDays & " days"
        ' MessageBox.Show("You are " & myYears & " years and " & myDays & " days .")
    End Sub

    Public Sub AgeCalculate()

        Dim dob As DateTime
        dob = New DateTime(dtpDOB.Value.Year, dtpDOB.Value.Month, dtpDOB.Value.Day)
        Dim tday As TimeSpan = DateTime.Now.Subtract(dob)
        Dim years As Integer, months As Integer, days As Integer
        months = 12 * (DateTime.Now.Year - dob.Year) + (DateTime.Now.Month - dob.Month)

        If DateTime.Now.Day < dob.Day Then
            months -= 1
            days = DateTime.DaysInMonth(dob.Year, dob.Month) - dob.Day + DateTime.Now.Day
        Else
            days = DateTime.Now.Day - dob.Day
        End If
        years = Math.Floor(months / 12)
        months -= years * 12
        txtCattleInAge.Text = " " & years & " Years " & months & " Months " & days & " Days"
        
    End Sub

    Private Sub txtNDDBCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtNDDBCode._MYValidating
        Dim query As String = " select TSPL_Paravet_NDDB_Master.NDDB_NO as NDDB_NO ,TSPL_Paravet_NDDB_Master.NDDB_Desc from TSPL_Paravet_NDDB_Master left outer join TSPL_CATTLE_MASTER on TSPL_Paravet_NDDB_Master.NDDB_No=TSPL_CATTLE_MASTER.NDDB_Code  "
        txtNDDBCode.Value = clsCommon.ShowSelectForm("NDDBVald", query, "NDDB_NO", " (TSPL_CATTLE_MASTER.NDDB_Code is null  or TSPL_CATTLE_MASTER.NDDB_Code ='" & txtNDDBCode.Value & "')", txtNDDBCode.Value, "NDDB_NO", isButtonClicked)
        Dim desc As String = "select NDDB_Desc from TSPL_Paravet_NDDB_Master where NDDB_No='" & txtNDDBCode.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblNDDBCode.Text = clsCommon.myCstr(dt.Rows(0)("NDDB_Desc"))
        Else
            lblNDDBCode.Text = ""
        End If
        If clsCommon.myLen(txtNDDBCode.Value) > 0 Then
            txtTagId.Text = clsDBFuncationality.getSingleValue(" select Tag_Prefix +'-' +Tag_SNO as Tag_No  from TSPL_Paravet_NDDB_Master where TSPL_Paravet_NDDB_Master.NDDB_No='" & txtNDDBCode.Value & "'")
        Else
            txtTagId.Text = ""
        End If
    End Sub

    Private Sub txtFather__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFather._MYValidating
        If rdbChildCattleType.Checked Then
            Dim query As String = " select Bull_No,Bull_Desc,Bull_Profile_Id,Status from TSPL_Paravet_Bull_Master "
            txtFather.Value = clsCommon.ShowSelectForm("FatherVald", query, "Bull_No", "Status='Active' ", txtFather.Value, "Bull_No", isButtonClicked)
            Dim desc As String = "select Bull_Desc from TSPL_Paravet_Bull_Master where Bull_No='" & txtFather.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblFather.Text = clsCommon.myCstr(dt.Rows(0)("Bull_Desc"))
            Else
                lblFather.Text = ""
            End If
        End If
       
    End Sub

    Private Sub txtMother__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMother._MYValidating
        If rdbChildCattleType.Checked Then
            Dim wher As String = ""
            wher = " Gender='Female' and Type = 'Parent Cattle'  "
            Dim CurrentCattleCode As String = ""
            CurrentCattleCode = txtCattleCode.Text
            If clsCommon.myLen(CurrentCattleCode) > 0 Then
                wher = wher + " and Cattle_Code <> '" & CurrentCattleCode & "'  "
            End If
            Dim query As String = " select Cattle_Code as Mother_Code,Registration_Desc as Mother_Desc from TSPL_CATTLE_MASTER  "
            txtMother.Value = clsCommon.ShowSelectForm("MotherVald", query, "Mother_Code", wher, txtMother.Value, "Mother_Code", isButtonClicked)
            Dim desc As String = "select Registration_Desc from TSPL_CATTLE_MASTER where Cattle_Code ='" & txtMother.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(desc)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblMother.Text = clsCommon.myCstr(dt.Rows(0)("Registration_Desc"))
            Else
                lblMother.Text = ""
            End If
        End If
       

    End Sub

    Private Sub rdbParentType_CheckedChanged(sender As Object, e As EventArgs) Handles rdbParentType.CheckedChanged
        If rdbParentType.Checked Then
            txtMother.MyReadOnly = True
            txtFather.MyReadOnly = True
        End If
    End Sub

    Private Sub rdbChildCattleType_CheckedChanged(sender As Object, e As EventArgs) Handles rdbChildCattleType.CheckedChanged
        If rdbChildCattleType.Checked Then
            txtMother.MyReadOnly = False
            txtFather.MyReadOnly = False
        End If
    End Sub

    Private Sub txtCattleCode_Leave(sender As Object, e As EventArgs) Handles txtCattleCode.Leave
        Dim strCattleCode As Integer = clsDBFuncationality.getSingleValue(" select count (*) from TSPL_CATTLE_MASTER where Cattle_Code='" & txtCattleCode.Text & "'")
        If strCattleCode > 0 Then
            clsCommon.MyMessageBoxShow("Duplicate Cattle Code.")
            txtCattleCode.Text = ""
            txtCattleCode.Focus()
        End If

    End Sub

    Private Sub dtpDOB_ValueChanged(sender As Object, e As EventArgs) Handles dtpDOB.ValueChanged
        AgeCalculate()
    End Sub
End Class
