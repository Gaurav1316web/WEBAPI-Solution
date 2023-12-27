Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports common
'Imports XpertERPHRandPayroll
Public Class frmNewDCSScreen
    Inherits FrmMainTranScreen


#Region "Variables"
    Dim userCode, companyCode As String
    Private isNewEntry As Boolean = False
#End Region
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub fndSupervisorCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSupervisorCode._MYValidating
        Try
            Dim qry As String = " select EMP_CODE as Code, Emp_Name  as Name from TSPL_EMPLOYEE_MASTER  "
            'Emp_type = 'Salesman' and Emp_Status = 'Active'
            fndSupervisorCode.Value = clsCommon.ShowSelectForm("NDSSSupervisor", qry, "Code", "Emp_Status = 'Active'", fndSupervisorCode.Value, "Code", isButtonClicked)
            txtSupervisorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  Emp_Name   from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + fndSupervisorCode.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub fndDistrict__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDistrict._MYValidating
        Try
            Dim qry As String = "select TSPL_DISTRICT_MASTER.Code as Code,TSPL_DISTRICT_MASTER.Name as DistrictName,TSPL_State_MASTER.STATE_CODE as [State Code] ,TSPL_State_MASTER.STATE_NAME as [State] " &
            " from TSPL_DISTRICT_MASTER " &
            " left outer join TSPL_State_MASTER  on TSPL_State_MASTER.STATE_CODE=TSPL_DISTRICT_MASTER.State_Code " &
            " left outer join TSPL_State_MASTER_detail on TSPL_State_MASTER.state_code=TSPL_State_MASTER_detail.state_code "

            fndDistrict.Value = clsCommon.ShowSelectForm("DCS@Dis@Finder", qry, "Code", "", fndDistrict.Value, "", isButtonClicked)
            txtDistrictName.Text = clsDistrictMaster.GetName(fndDistrict.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub



    Private Sub fndRevenueVillage__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndRevenueVillage._MYValidating
        Try
            Dim qry As String = " Select REVENUE_VILLAGE_CODE as Code, REVENUE_VILLAGE_NAME as Name from TSPL_REVENUE_VILLAGE_MASTER  "
            fndRevenueVillage.Value = clsCommon.ShowSelectForm("DCS@RevenueVillage@Finder", qry, "Code", "", fndRevenueVillage.Value, "", isButtonClicked)
            txtRevenueVillageName.Text = clsRevenueVillageMaster.GetName(fndRevenueVillage.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub fndGramPanchayat__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndGramPanchayat._MYValidating
        Try
            Dim qry As String = "Select GRAMPANCHAYAT_CODE as Code, GRAMPANCHAYAT_NAME as Name from TSPL_GRAMPANCHAYAT_MASTER  "
            fndGramPanchayat.Value = clsCommon.ShowSelectForm("DCS@Grampanchayat@Finder", qry, "Code", "", fndGramPanchayat.Value, "", isButtonClicked)
            txtGramPanchayatName.Text = clsGrampanchayatMaster.GetName(fndGramPanchayat.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub fndPanchayatSamiti__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPanchayatSamiti._MYValidating
        Try
            Dim qry As String = " select PANCHAYAT_SAMITI_CODE as Code, PANCHAYAT_SAMITI_NAME as Name from TSPL_PANCHAYAT_SAMITI_MASTER  "
            fndPanchayatSamiti.Value = clsCommon.ShowSelectForm("DCS@PanchayatSamiti@Finder", qry, "Code", "", fndPanchayatSamiti.Value, "", isButtonClicked)
            txtPanchayatSamitiName.Text = clsPanchayatSamitiMaster.GetName(fndPanchayatSamiti.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub fndVidhanSabha__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndVidhanSabha._MYValidating
        Try
            Dim qry As String = " select VIDHAN_SABHA_CODE as Code, VIDHAN_SABHA_NAME as Name from TSPL_VIDHAN_SABHA_MASTER  "
            fndVidhanSabha.Value = clsCommon.ShowSelectForm("DCS@VidhanSabha@Finder", qry, "Code", "", fndVidhanSabha.Value, "", isButtonClicked)
            txtVidhanSabhaName.Text = clsVidhanSabhaMaster.GetName(fndVidhanSabha.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub fndBlock__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBlock._MYValidating
        Try
            Dim qry As String = "select Block_Code As Code, Block_Name As Name from TSPL_BLOCK_MASTER "
            fndBlock.Value = clsCommon.ShowSelectForm("NDSSBlock", qry, "Code", "", fndDistrict.Value, "Code", isButtonClicked)
            txtBlockName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Block_Name from TSPL_BLOCK_MASTER where  Block_Code = '" + fndBlock.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub fndZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndZone._MYValidating
        Try
            Dim qry As String = "select Zone_Code As Code, Description As ZoneName from TSPL_ZONE_MASTER "
            fndZone.Value = clsCommon.ShowSelectForm("NDSSBlock", qry, "Code", "", fndZone.Value, "Code", isButtonClicked)
            txtZoneName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description As ZoneName from TSPL_ZONE_MASTER  where  Zone_Code= '" + fndZone.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub fndCompanyBank__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCompanyBank._MYValidating
        Try
            fndCompanyBank.Value = clsBankMaster.getFinder("", fndCompanyBank.Value, isButtonClicked)
            txtCompanyBankName.Text = clsBankMaster.GetName(fndCompanyBank.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCompanyBank1__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCompanyBank1._MYValidating
        Try
            fndCompanyBank1.Value = clsBankMaster.getFinder("", fndCompanyBank1.Value, isButtonClicked)
            txtCompanyBank1.Text = clsBankMaster.GetName(fndCompanyBank1.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDCSCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDCSCode._MYValidating
        Try
            fndDCSCode.Value = clsVendorMaster.getFinder(" form_type='VSP'", fndDCSCode.Value, isButtonClicked)
            txtDCSName.Text = clsDBFuncationality.getSingleValue("select Vendor_Name As 'DCS Name' from TSPL_VENDOR_MASTER where  Vendor_Code='" + fndDCSCode.Value + "'")
            'Dim qry As String = "select Vendor_Code As 'Code', Vendor_Name As 'Name' from TSPL_VENDOR_MASTER"
            'fndDCSCode.Value = clsCommon.ShowSelectForm("NDSSBlock", qry, "Code", "", fndZone.Value, "Code", isButtonClicked)
            'txtDCSName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name As 'DCS Name' from TSPL_VENDOR_MASTER where  Vendor_Code= '" + fndDCSCode.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsNewDCSScreen()


                obj.VLCCode = fndDCSCode.Value
                obj.VLCName = txtDCSName.Text
                obj.VLCUploaderCode = txtDCSUploaderCode.Text
                If chkRegistered.Checked Then
                    obj.Registered = True
                End If
                obj.RegistrationNo = txtRegistrationNo.Text
                obj.RegistrationDate = clsCommon.GetPrintDate(txtRegistrationDate.Text)
                obj.DCSRouteCode = txtDCSRouteCode.Text
                If chkOwnBMC.Checked Then
                    obj.OwnBMC = True
                End If
                obj.OwnBMCDate = clsCommon.GetPrintDate(txtOwnBMCDate.Text)
                obj.HeadLoad = txtHeadLoad.Text
                obj.HeadLoadBasi = txtHeadLoadBasi.Text
                obj.HeadLoadRate = txtHeadLoadRate.Text
                If chkInActive.Checked Then
                    obj.Inactive = True
                End If
                If chkDefaultValue.Checked Then
                    obj.DefaultValue = True
                End If
                If chkPDCS.Checked Then
                    obj.PDCS = True
                End If
                obj.Gender = ddlGender.Text
                If chkApplyCowPrice.Checked Then
                    obj.ApplyCowPrice = True
                End If
                obj.StartDate = clsCommon.GetPrintDate(txtCowPriceStartDate.Text)
                obj.SupervisorCode = fndSupervisorCode.Value
                obj.District = fndDistrict.Value
                obj.Zone = fndZone.Value
                obj.Block = fndBlock.Value
                obj.RevenueVillage = fndRevenueVillage.Value
                obj.GramPanchayat = fndGramPanchayat.Value
                obj.PanchayatSamiti = fndPanchayatSamiti.Value
                obj.VidhanSabha = fndVidhanSabha.Value
                obj.CompanyBank1 = fndCompanyBank.Value
                obj.DCSCurrentBankDetails1 = txtDCSCurrentBankDetails.Text
                obj.BankName1 = txtBankName.Text
                obj.BankAccountNo1 = txtBankAccountNo.Text
                obj.BankBranch1 = txtBankBranchName.Text
                obj.BankIFSCCode1 = txtBankIFSCCode.Text
                obj.CompanyBank2 = fndCompanyBank1.Value
                obj.DCSCurrentBankDetails2 = txtDCSCurrentBankDetails1.Text
                obj.BankName2 = txtBankName1.Text
                obj.BankAccountNo2 = txtBankAccountNo1.Text
                obj.BankBranch2 = txtBankBranchName1.Text
                obj.BankIFSCCode2 = txtBankIFSCCode1.Text
                obj.PanNo = txtPanNo.Text
                obj.LoyaltyRate = txtLoyaltyRate.Text
                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    'LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub



    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(fndDCSCode.Value) <= 0 Then
                fndDCSCode.Focus()
                Throw New Exception("Select DCS Code")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


End Class