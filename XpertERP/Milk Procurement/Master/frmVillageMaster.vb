'--Created By Monika 22/05/2014
Imports common
Imports System.Data.SqlClient
Imports XpertERPCommanServices

'By balwinder on 02/08/2016 ticket no BM00000009356,BM00000009356
Public Class FrmVillageMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim isNewEntry As Boolean = True
    Dim Frm_Open As FrmMainTranScreen
    Private isAdditionInformation As Boolean = False
#End Region

    Sub Reset()
        fndcode.MyReadOnly = False
        fndcode.Value = ""
        txtDescription.Text = ""
        'txtadd1.Text = ""
        txtadd2.Text = ""
        txtcitycode.Value = ""
        txtcity.Text = ""
        txtstatecode.Value = ""
        txtstate.Text = ""
        txtcountrycode.Value = "INDIA"
        txtcountry.Text = "INDIA"
        txtpin.Text = ""
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        isNewEntry = True
        btndelete.Enabled = False
        txtSurveyorName.Text = ""
        txtSurveyDate.Value = clsCommon.GETSERVERDATE()
        txtSurveyDate.Checked = False
        txtTotalPopulation.Value = 0
        txtTehsil.Text = ""
        TxtTotalVoting.Value = 0
        txtPradhanName.Text = ""
        txtPradhanContactNo.Text = ""
        txtDistanceFromCenter.Value = 0
        txtIrrigationSource.Text = 0
        txtVillageArea.Text = ""
        txtDistanceFromMCC.Value = 0
        txtCowInMilk.Value = 0
        txtBuffaloInMilk.Value = 0
        txtCowDry.Value = 0
        txtBuffaloDry.Value = 0
        txtTotalAnimals.Text = "0"
        txtMilkProductionPerDayCow.Value = 0
        txtMilkProductionPerDayBuffalo.Value = 0
        txtMarketableSurplusPerDayCow.Value = 0
        txtMarketableSurplusPerDayBuffalo.Value = 0
        txtExpectedMilkPerDayCow.Value = 0
        txtExpectedMilkPerDayBuffalo.Value = 0
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmVillageMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        If Not (MyBase.isReadFlag) Then
            btnexport.Visibility = ElementVisibility.Collapsed
        End If
        If Not (MyBase.isModifyFlag) Then
            btnimport.Visibility = ElementVisibility.Collapsed
        End If
    End Sub
    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim qry As String = "select count(*) from tspl_village_master"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        Dim strAddInfo As String = ""
        Dim strBlankAddInfo As String = ""
        If isAdditionInformation Then
            strAddInfo = ",Surveyor_Name,case when Survey_Date is null then '' else REPLACE( convert(varchar, Survey_Date,106),' ','/') end as Survey_Date,Total_Population,Tehsil,Total_Voting,Pradhan_Name,Pradhan_Contact_No,Distance_From_Center,Irrigation_Source,Village_Area,Distance_From_MCC,Cow_In_Milk,Buffalo_In_Milk,Cow_Dry,Buffalo_Dry ,Milk_Production_Per_Day_Cow,Milk_Production_Per_Day_Buffalo,Marketable_Surplus_Per_Day_Cow,Marketable_Surplus_Per_Day_Buffalo,Expected_Milk_Per_Day_Cow,Expected_Milk_Per_Day_Buffalo "
            strBlankAddInfo = ",'' as Surveyor_Name,'' as Survey_Date,0 as Total_Population,'' as Tehsil,0 as Total_Voting,'' as Pradhan_Name,'' as Pradhan_Contact_No,0 as Distance_From_Center,'' as Irrigation_Source,'' as Village_Area,0 as Distance_From_MCC,0 as Cow_In_Milk,0 as Buffalo_In_Milk,0 as Cow_Dry,0 as Buffalo_Dry ,0 as Milk_Production_Per_Day_Cow,0 as Milk_Production_Per_Day_Buffalo,0 as Marketable_Surplus_Per_Day_Cow,0 as Marketable_Surplus_Per_Day_Buffalo,0 as Expected_Milk_Per_Day_Cow,0 as Expected_Milk_Per_Day_Buffalo "
            'ListImpExpColumns = New List(Of String)({"village_code", "village_name", "city_code", "city_name", "state_code", "state_name", "country_code", "country_name", "pin_no", "Surveyor_Name", "Survey_Date", "Total_Population", "Tehsil", "Total_Voting", "Pradhan_Name", "Pradhan_Contact_No", "Distance_From_Center", "Irrigation_Source", "Village_Area", "Distance_From_MCC", "Cow_In_Milk", "Buffalo_In_Milk", "Cow_Dry", "Buffalo_Dry", "Milk_Production_Per_Day_Cow", "Milk_Production_Per_Day_Buffalo", "Marketable_Surplus_Per_Day_Cow", "Marketable_Surplus_Per_Day_Buffalo", "Expected_Milk_Per_Day_Cow", "Expected_Milk_Per_Day_Buffalo"})
        Else
            'ListImpExpColumns = New List(Of String)({"village_code", "village_name", "city_code", "city_name", "state_code", "state_name", "country_code", "country_name", "pin_no"})
        End If

        If check > 0 Then
            qry = "select TSPL_VILLAGE_MASTER.village_code,TSPL_VILLAGE_MASTER.village_name,TSPL_VILLAGE_MASTER.city_code,tspl_city_master.city_name,TSPL_VILLAGE_MASTER.state_code,tspl_state_master.state_name,TSPL_VILLAGE_MASTER.country_code,tspl_country_master.country_name,TSPL_VILLAGE_MASTER.pin_no" + strAddInfo + " from TSPL_VILLAGE_MASTER left outer join tspl_city_master on tspl_city_master.city_code=TSPL_VILLAGE_MASTER.city_code left outer join tspl_state_master on tspl_state_master.state_code=TSPL_VILLAGE_MASTER.state_code left outer join tspl_country_master on tspl_country_master.country_code=TSPL_VILLAGE_MASTER.country_code"
        Else
            qry = "select '' as village_code,'' as village_name, '' as city_code,'' as city_name,'' as state_code,'' as state_name,'' as country_code,'' as country_name,'' as pin_no" + strBlankAddInfo
        End If
        ListImpExpColumnsMandatory = New List(Of String)({"village_name", "city_code", "state_code", "country_code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"village_code"})
        transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Function importprivate(ByRef gv As RadGridView) As Boolean
        If True Then

        End If

    End Function

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = clsCommon.GETSERVERDATE()
            Dim result As Boolean = False
            Dim ListImpExpColumns As List(Of String)
            If isAdditionInformation Then
                ListImpExpColumns = New List(Of String)({"village_code", "village_name", "city_code", "city_name", "state_code", "state_name", "country_code", "country_name", "pin_no", "Surveyor_Name", "Survey_Date", "Total_Population", "Tehsil", "Total_Voting", "Pradhan_Name", "Pradhan_Contact_No", "Distance_From_Center", "Irrigation_Source", "Village_Area", "Distance_From_MCC", "Cow_In_Milk", "Buffalo_In_Milk", "Cow_Dry", "Buffalo_Dry", "Milk_Production_Per_Day_Cow", "Milk_Production_Per_Day_Buffalo", "Marketable_Surplus_Per_Day_Cow", "Marketable_Surplus_Per_Day_Buffalo", "Expected_Milk_Per_Day_Cow", "Expected_Milk_Per_Day_Buffalo"})
                result = transportSql.importExcel(gv, ListImpExpColumns)
            Else
                ListImpExpColumns = New List(Of String)({"village_code", "village_name", "city_code", "city_name", "state_code", "state_name", "country_code", "country_name", "pin_no"})
                result = transportSql.importExcel(gv, ListImpExpColumns)
                'result = transportSql.importExcel(gv, "village_code", "village_name", "city_code", "city_name", "state_code", "state_name", "country_code", "country_name", "pin_no")
            End If
            If result Then
                Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim qry As String = ""
                    Dim counter As Integer = 1
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        If clsCommon.myLen(grow.Cells("village_name").Value) > 0 Then
                            Dim obj As New clsfrmVillageMaster
                            obj.villcode = clsCommon.myCstr(grow.Cells("village_code").Value)

                            obj.villname = clsCommon.myCstr(grow.Cells("village_name").Value)
                            If clsCommon.myLen(obj.villname) <= 0 Then
                                Throw New Exception("Please Fill Village Name At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If clsCommon.myLen(obj.villname) > 150 Then
                                Throw New Exception("Length Of Village Name Should Not Exceed 150 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            obj.citycode = clsCommon.myCstr(grow.Cells("city_code").Value)
                            If clsCommon.myLen(obj.citycode) > 0 Then
                                qry = "select city_code from tspl_city_master where city_code='" + obj.citycode + "'"
                                obj.citycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, tran))
                                If clsCommon.myLen(obj.citycode) <= 0 Then
                                    Throw New Exception("First Create City Master(" + obj.citycode + " Does Not Exist In Master) See Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If

                            obj.statecode = clsCommon.myCstr(grow.Cells("state_code").Value)
                            If clsCommon.myLen(obj.statecode) > 0 Then
                                qry = "select state_code from tspl_state_master where state_code='" + obj.statecode + "'"
                                obj.statecode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, tran))

                                If clsCommon.myLen(obj.statecode) <= 0 Then
                                    Throw New Exception("First Create State Master(" + obj.statecode + " Does Not Exist In Master) See Line No. " + clsCommon.myCstr(counter) + "")
                                Else
                                    qry = "select count(*) from tspl_city_master where city_code='" + obj.citycode + "' and state_code='" + obj.statecode + "'"
                                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, tran)
                                    If check <= 0 Then
                                        Throw New Exception("First Mapped State (" + obj.statecode + ") With City (" + obj.citycode + ") On City Master See At Line No. " + clsCommon.myCstr(counter) + "")
                                    End If
                                End If
                            End If

                            If clsCommon.myLen(obj.citycode) > 0 AndAlso clsCommon.myLen(obj.statecode) <= 0 Then
                                Throw New Exception("Please Fill State Along With City,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If

                            obj.countrycode = clsCommon.myCstr(grow.Cells("country_code").Value)


                            If clsCommon.myLen(obj.countrycode) > 0 Then
                                qry = "select country_code from tspl_country_master where country_code='" + obj.countrycode + "'"
                                obj.countrycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, tran))

                                If clsCommon.myLen(obj.countrycode) <= 0 Then
                                    Throw New Exception("Please First Create Country Master(" + obj.countrycode + " Does Not Exist In Master)")
                                Else
                                    qry = "select count(*) from tspl_state_master where country_code='" + obj.countrycode + "' and state_code='" + obj.statecode + "'"
                                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, tran)
                                    If check <= 0 Then
                                        Throw New Exception("First Mapped Country (" + obj.countrycode + ") With State (" + obj.statecode + ") In State Master See At Line No. " + clsCommon.myCstr(counter) + "")
                                    End If
                                End If
                            End If

                            If clsCommon.myLen(obj.countrycode) <= 0 AndAlso clsCommon.myLen(obj.statecode) > 0 Then
                                Throw New Exception("Please Fill Country Along With State,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            obj.pinno = clsCommon.myCstr(clsCommon.myCdbl(grow.Cells("pin_no").Value))

                            If isAdditionInformation Then
                                obj.Surveyor_Name = clsCommon.myCstr(grow.Cells("Surveyor_Name").Value)
                                If clsCommon.myLen(obj.Surveyor_Name) > 50 Then
                                    Throw New Exception("Length Of Surveyor Name Should Not Exceed 50 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                                If clsCommon.myLen(grow.Cells("Survey_Date").Value) > 0 Then
                                    obj.Survey_Date = clsCommon.myCDate(grow.Cells("Survey_Date").Value)
                                Else
                                    obj.Survey_Date = Nothing
                                End If
                                obj.Total_Population = clsCommon.myCdbl(grow.Cells("Total_Population").Value)
                                obj.Tehsil = clsCommon.myCstr(grow.Cells("Tehsil").Value)
                                If clsCommon.myLen(obj.Tehsil) > 50 Then
                                    Throw New Exception("Length Of Tehsil Should Not Exceed 50 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                                obj.Total_Voting = clsCommon.myCdbl(grow.Cells("Total_Voting").Value)
                                obj.Pradhan_Name = clsCommon.myCstr(grow.Cells("Pradhan_Name").Value)
                                If clsCommon.myLen(obj.Pradhan_Name) > 50 Then
                                    Throw New Exception("Length Of Pradhan_Name Should Not Exceed 50 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                                obj.Pradhan_Contact_No = clsCommon.myCstr(grow.Cells("Pradhan_Contact_No").Value)
                                If clsCommon.myLen(obj.Pradhan_Contact_No) > 50 Then
                                    Throw New Exception("Length Of Pradhan Contact No Should Not Exceed 50 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                                obj.Distance_From_Center = clsCommon.myCdbl(grow.Cells("Distance_From_Center").Value)
                                obj.Irrigation_Source = clsCommon.myCstr(grow.Cells("Irrigation_Source").Value)
                                If clsCommon.myLen(obj.Irrigation_Source) > 50 Then
                                    Throw New Exception("Length Of Irrigation Source Should Not Exceed 50 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                                obj.Village_Area = clsCommon.myCstr(grow.Cells("Village_Area").Value)
                                If clsCommon.myLen(obj.Village_Area) > 50 Then
                                    Throw New Exception("Length Of Village_Area Should Not Exceed 50 Characters At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                                obj.Distance_From_MCC = clsCommon.myCdbl(grow.Cells("Distance_From_MCC").Value)
                                obj.Cow_In_Milk = clsCommon.myCdbl(grow.Cells("Cow_In_Milk").Value)
                                obj.Buffalo_In_Milk = clsCommon.myCdbl(grow.Cells("Buffalo_In_Milk").Value)
                                obj.Cow_Dry = clsCommon.myCdbl(grow.Cells("Cow_Dry").Value)
                                obj.Buffalo_Dry = clsCommon.myCdbl(grow.Cells("Buffalo_Dry").Value)
                                obj.Milk_Production_Per_Day_Cow = clsCommon.myCdbl(grow.Cells("Milk_Production_Per_Day_Cow").Value)
                                obj.Milk_Production_Per_Day_Buffalo = clsCommon.myCdbl(grow.Cells("Milk_Production_Per_Day_Buffalo").Value)
                                obj.Marketable_Surplus_Per_Day_Cow = clsCommon.myCdbl(grow.Cells("Marketable_Surplus_Per_Day_Cow").Value)
                                obj.Marketable_Surplus_Per_Day_Buffalo = clsCommon.myCdbl(grow.Cells("Marketable_Surplus_Per_Day_Buffalo").Value)
                                obj.Expected_Milk_Per_Day_Cow = clsCommon.myCdbl(grow.Cells("Expected_Milk_Per_Day_Cow").Value)
                                obj.Expected_Milk_Per_Day_Buffalo = clsCommon.myCdbl(grow.Cells("Expected_Milk_Per_Day_Buffalo").Value)
                            End If
                            isNewEntry = True
                            If clsCommon.myLen(obj.villcode) > 0 Then
                                qry = "select count(*) from TSPL_VILLAGE_MASTER where village_code='" + obj.villcode + "'"
                                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry, tran)
                                If check1 > 0 Then
                                    isNewEntry = False
                                End If
                            End If
                            clsfrmVillageMaster.SaveData(obj, isNewEntry, tran)
                        End If
                        counter += 1
                        clsCommon.ProgressBarUpdate("Imported Receords  : " & counter & "/" & gv.Rows.Count)
                    Next
                    tran.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    tran.Rollback()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Sub LoadData(ByVal Strcode As String, ByVal Navtype As NavigatorType)
        Try
            Dim obj As clsfrmVillageMaster = clsfrmVillageMaster.GetData(Strcode, Navtype)
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.villcode) > 0 Then
                isNewEntry = False
                fndcode.Value = obj.villcode
                txtDescription.Text = obj.villname
                'txtadd1.Text = obj.add1
                'txtadd2.Text = obj.add2
                txtcitycode.Value = obj.citycode
                txtcity.Text = obj.cityname
                txtstatecode.Value = obj.statecode
                txtstate.Text = obj.statename
                txtcountrycode.Value = obj.countrycode
                txtcountry.Text = obj.countryname
                txtpin.Text = obj.pinno

                btnsave.Text = "Update"
                btndelete.Enabled = True
                fndcode.MyReadOnly = True
                UcAttachment1.LoadData(fndcode.Value)

                txtSurveyorName.Text = obj.Surveyor_Name
                If obj.Survey_Date Is Nothing Then
                    txtSurveyDate.Checked = False
                Else
                    txtSurveyDate.Checked = True
                    txtSurveyDate.Value = obj.Survey_Date
                End If
                txtTotalPopulation.Value = obj.Total_Population
                txtTehsil.Text = obj.Tehsil
                TxtTotalVoting.Value = obj.Total_Voting
                txtPradhanName.Text = obj.Pradhan_Name
                txtPradhanContactNo.Text = obj.Pradhan_Contact_No
                txtDistanceFromCenter.Value = obj.Distance_From_Center
                txtIrrigationSource.Text = obj.Irrigation_Source
                txtVillageArea.Text = obj.Village_Area
                txtDistanceFromMCC.Value = obj.Distance_From_MCC
                txtCowInMilk.Value = obj.Cow_In_Milk
                txtBuffaloInMilk.Value = obj.Buffalo_In_Milk
                txtCowDry.Value = obj.Cow_Dry
                txtBuffaloDry.Value = obj.Buffalo_Dry
                txtTotalAnimals.Text = obj.Total_Animals
                txtMilkProductionPerDayCow.Value = obj.Milk_Production_Per_Day_Cow
                txtMilkProductionPerDayBuffalo.Value = obj.Milk_Production_Per_Day_Buffalo
                txtMarketableSurplusPerDayCow.Value = obj.Marketable_Surplus_Per_Day_Cow
                txtMarketableSurplusPerDayBuffalo.Value = obj.Marketable_Surplus_Per_Day_Buffalo
                txtExpectedMilkPerDayCow.Value = obj.Expected_Milk_Per_Day_Cow
                txtExpectedMilkPerDayBuffalo.Value = obj.Expected_Milk_Per_Day_Buffalo

            Else
                fndcode.MyReadOnly = False
                Reset()
            End If

        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndcode._MYNavigator
        LoadData(fndcode.Value, NavType)
    End Sub

    Private Sub fndcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcode._MYValidating
        Dim str As String = "select count(*) from tspl_village_master where village_code ='" + fndcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndcode.MyReadOnly = False
        Else
            fndcode.MyReadOnly = True
        End If
        If fndcode.MyReadOnly OrElse isButtonClicked Then
            str = "select TSPL_VILLAGE_MASTER.village_code as villcode,TSPL_VILLAGE_MASTER.village_name as [Village Name],TSPL_VILLAGE_MASTER.add1 as Address,TSPL_VILLAGE_MASTER.city_code as [City code],tspl_city_master.city_name as [City Name],TSPL_VILLAGE_MASTER.state_code as [State Code],tspl_state_master.state_name as [State Name],TSPL_VILLAGE_MASTER.country_code as [Country Code],tspl_country_master.country_name as [Country Name],TSPL_VILLAGE_MASTER.pin_no as [Pin No] from TSPL_VILLAGE_MASTER left outer join tspl_city_master on tspl_city_master.city_code=TSPL_VILLAGE_MASTER.city_code left outer join tspl_state_master on tspl_state_master.state_code=TSPL_VILLAGE_MASTER.state_code left outer join tspl_country_master on tspl_country_master.country_code=TSPL_VILLAGE_MASTER.country_code"
            LoadData(clsCommon.ShowSelectForm("VILLFND", str, "villcode", "", fndcode.Value, "villcode", isButtonClicked), NavigatorType.Current)
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Private Sub txtcitycode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcitycode._MYValidating
        Try

            Dim qry As String = "select city_code as Code,city_name as City,comp_code as Company from TSPL_CITY_MASTER"
            txtcitycode.Value = clsCommon.ShowSelectForm("FNDCITY", qry, "Code", "", txtcitycode.Value, "City", isButtonClicked)

            If clsCommon.myLen(txtcitycode.Value) > 0 Then
                'txtcity.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select city_name from TSPL_CITY_MASTER where city_code='" + txtcitycode.Value + "' and state_code='" + txtstatecode.Value + "'"))
                txtcity.Text = clsCommon.myCstr(clsCityMaster.GetName(txtcitycode.Value))
                txtstatecode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_code from TSPL_CITY_MASTER where city_code='" + txtcitycode.Value + "' "))
                txtstate.Text = clsCommon.myCstr(clsStateMaster.GetName(txtstatecode.Value))
                txtcountrycode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_code from TSPL_state_MASTER where state_code='" + txtstatecode.Value + "' "))
                txtcountry.Text = clsCommon.myCstr(clsCountryMaster.GetName(txtcountrycode.Value, Nothing))
            Else
                txtcity.Text = ""
                txtstatecode.Value = ""
                txtstate.Text = ""
                txtcountrycode.Value = "INDIA"
                txtcountry.Text = "INDIA"

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Function AllowToSave() As Boolean
        Try

            If clsCommon.myLen(txtDescription.Text) <= 0 Then
                txtDescription.Focus()
                txtDescription.Select()
                ErrorControl.SetError(txtDescription, "Please Fill Village Name")
                Throw New Exception("Please Fill Village Name")
            Else
                ErrorControl.ResetError(txtDescription)
            End If

            If clsCommon.myLen(txtpin.Text) > 0 Then
                Try
                    Convert.ToDecimal(txtpin.Text)
                Catch exx As Exception
                    Throw New Exception("Pin Number Should Be Numeric")
                End Try
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Sub SaveData()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmVillageMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New clsfrmVillageMaster()
                obj.villcode = fndcode.Value
                obj.villname = txtDescription.Text.Replace("'", "`")
                obj.citycode = txtcitycode.Value
                obj.statecode = txtstatecode.Value
                obj.countrycode = txtcountrycode.Value
                obj.pinno = txtpin.Text
                If isAdditionInformation Then
                    obj.Surveyor_Name = txtSurveyorName.Text
                    If txtSurveyDate.Checked Then
                        obj.Survey_Date = txtSurveyDate.Value
                    Else
                        obj.Survey_Date = Nothing
                    End If
                    obj.Total_Population = txtTotalPopulation.Value
                    obj.Tehsil = txtTehsil.Text
                    obj.Total_Voting = TxtTotalVoting.Value
                    obj.Pradhan_Name = txtPradhanName.Text
                    obj.Pradhan_Contact_No = txtPradhanContactNo.Text
                    obj.Distance_From_Center = txtDistanceFromCenter.Value
                    obj.Irrigation_Source = txtIrrigationSource.Text
                    obj.Village_Area = txtVillageArea.Text
                    obj.Distance_From_MCC = txtDistanceFromMCC.Value
                    obj.Cow_In_Milk = txtCowInMilk.Value
                    obj.Buffalo_In_Milk = txtBuffaloInMilk.Value
                    obj.Cow_Dry = txtCowDry.Value
                    obj.Buffalo_Dry = txtBuffaloDry.Value
                    obj.Total_Animals = txtTotalAnimals.Text
                    obj.Milk_Production_Per_Day_Cow = txtMilkProductionPerDayCow.Value
                    obj.Milk_Production_Per_Day_Buffalo = txtMilkProductionPerDayBuffalo.Value
                    obj.Marketable_Surplus_Per_Day_Cow = txtMarketableSurplusPerDayCow.Value
                    obj.Marketable_Surplus_Per_Day_Buffalo = txtMarketableSurplusPerDayBuffalo.Value
                    obj.Expected_Milk_Per_Day_Cow = txtExpectedMilkPerDayCow.Value
                    obj.Expected_Milk_Per_Day_Buffalo = txtExpectedMilkPerDayBuffalo.Value
                End If
                If clsfrmVillageMaster.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(fndcode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Function DeleteData() As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select count(*) from TSPL_VILLAGE_MASTER where village_code='" + fndcode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If check <= 0 Then
                Throw New Exception("No Data Found For Deletion")
            End If
            qry = "select count(*) from tspl_vlc_master_detail where village_code='" + fndcode.Value + "'"
            check = clsDBFuncationality.getSingleValue(qry, trans)
            If check > 0 Then
                Throw New Exception("First Delete All Its References")
            End If
            If Not clsCommon.MyMessageBoxShow("Are You Sure,Want To Delete Village villcode " + fndcode.Value + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Return False
            End If
            qry = "delete from TSPL_VILLAGE_MASTER where village_code='" + fndcode.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If clsCommon.myLen(fndcode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Village For Deletion", Me.Text)
            fndcode.Focus()
            fndcode.Select()
            Return
        End If

        If DeleteData() Then
            clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
            Reset()
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub FrmVillageMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            btnnew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmVillageMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        Reset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S/U for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        isAdditionInformation = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsAdditionalInformationOnVillageMaster, clsFixedParameterCode.IsAdditionalInformationOnVillageMaster, Nothing)) = 1, True, False)
        setAdditionalInforPage()

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub txtpin_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtpin.Validating
        Try
            If clsCommon.myLen(txtpin) > 0 Then
                Convert.ToDecimal(txtpin.Text)
            End If
        Catch ex As Exception
            txtpin.Text = ""
            clsCommon.MyMessageBoxShow(Me, "Enter Numeric Value.", Me.Text)
        End Try
    End Sub

    Private Sub txtcitycode_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcitycode.Validated
        If clsCommon.myLen(txtcitycode.Value) > 0 Then
            txtstatecode.Enabled = False
            txtcountrycode.Enabled = False
        Else
            txtstatecode.Enabled = True
            txtcountrycode.Enabled = True
        End If
    End Sub

    Private Sub txtcitycode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcitycode._MYOpenMasterForm
        Frm_Open = New frmCityMaster(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        Frm_Open.SetUserMgmt(clsUserMgtCode.cityMaster)
        Frm_Open.Show()
    End Sub

    Sub setAdditionalInforPage()
        If isAdditionInformation Then
            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("RadPageViewPage3").Item.Visibility = ElementVisibility.Collapsed
        End If
    End Sub

    Private Sub txtCowInMilk_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtCowInMilk.Validating, txtBuffaloInMilk.Validating, txtBuffaloDry.Validating, txtCowDry.Validating
        txtTotalAnimals.Text = clsCommon.myCstr(txtCowInMilk.Value + txtBuffaloInMilk.Value + txtBuffaloDry.Value + txtCowDry.Value)
    End Sub
End Class
