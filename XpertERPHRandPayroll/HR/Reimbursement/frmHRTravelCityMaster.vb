'' Created By Anubhooti BM00000006153 
Imports common
Imports System.Data.SqlClient
Imports System
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class FrmHRTravelCityMaster
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ComboLoad As Boolean = False
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmHRTravelCityMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub Reset()
        txtCode.Value = ""
        txtDesc.Text = ""
        txtcountrycode.Value = ""
        txtCountry.Text = ""
        txtState.Value = ""
        LblState.Text = ""
        fndCity.Value = ""
        txtCity.Text = ""
        ChkIsApplicable.Checked = False

        Me.CmbTrType.DataSource = ClsHRTravelCityMaster.GetTT
        Me.CmbTrType.DisplayMember = "Name"
        Me.CmbTrType.ValueMember = "Code"
        ComboLoad = True

        If clsCommon.CompairString(CmbTrType.SelectedValue, "D") = CompairStringResult.Equal Then
            Dim CountCM As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) AS Row From TSPL_COUNTRY_MASTER WHERE COUNTRY_CODE='INDIA'"))
            If CountCM > 0 Then
                txtcountrycode.Value = "INDIA"
                txtCountry.Text = "INDIA"
                LblState.Text = ""
                txtState.Value = ""
                txtCity.Text = ""
                fndCity.Value = ""
            Else
                txtcountrycode.Value = ""
                txtCountry.Text = ""
                LblState.Text = ""
                txtState.Value = ""
                txtCity.Text = ""
                fndCity.Value = ""
            End If
        Else
            txtcountrycode.Value = ""
            txtCountry.Text = ""
            LblState.Text = ""
            txtState.Value = ""
            txtCity.Text = ""
            fndCity.Value = ""
        End If
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        txtCode.MyReadOnly = False

        txtCode.Focus()
        txtCode.Select()
    End Sub
    Function AllowToSave() As Boolean
        Try
            btnSave.Focus()
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill Code", Me.Text)
                txtCode.Focus()
                txtCode.Select()
                Return False
            End If
            If clsCommon.myLen(txtDesc.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill description", Me.Text)
                txtDesc.Focus()
                txtDesc.Select()
                Return False
            End If
            If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill country", Me.Text)
                txtcountrycode.Focus()
                txtcountrycode.Select()
                Return False
            End If
            If clsCommon.myLen(txtState.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill state", Me.Text)
                txtState.Focus()
                txtState.Select()
                Return False
            End If
            If clsCommon.myLen(fndCity.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please fill city", Me.Text)
                fndCity.Focus()
                fndCity.Select()
                Return False
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function
    Sub SaveData()
        Try
            Dim obj As New ClsHRTravelCityMaster()

            obj.Travel_City_Code = clsCommon.myCstr(txtCode.Value)
            obj.Desp = clsCommon.myCstr(txtDesc.Text).Replace("'", "`")
            obj.Country_Code = clsCommon.myCstr(txtcountrycode.Value)
            obj.State_Code = clsCommon.myCstr(txtState.Value)
            obj.City_Code = clsCommon.myCstr(fndCity.Value)
            obj.Travel_Type = clsCommon.myCstr(CmbTrType.SelectedValue)
            If ChkIsApplicable.Checked = True Then
                obj.Is_Applicable = 1
            Else
                obj.Is_Applicable = 0
            End If
            If ClsHRTravelCityMaster.SaveData(obj, txtCode.Value) Then
                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                LoadData(obj.Travel_City_Code, NavigatorType.Current)
                txtCode.MyReadOnly = True
            Else
                txtCode.MyReadOnly = False
                btnSave.Text = "Save"
                btnDelete.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New ClsHRTravelCityMaster()
            txtCode.Value = strCode
            obj = ClsHRTravelCityMaster.GetData(strCode, NavTyep)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Travel_City_Code) > 0 Then
                txtCode.Value = clsCommon.myCstr(obj.Travel_City_Code)
                txtDesc.Text = clsCommon.myCstr(obj.Desp)
                CmbTrType.SelectedValue = clsCommon.myCstr(obj.Travel_Type)
                txtcountrycode.Value = clsCommon.myCstr(obj.Country_Code)
                txtState.Value = clsCommon.myCstr(obj.State_Code)
                fndCity.Value = clsCommon.myCstr(obj.City_Code)

                If clsCommon.myLen(clsCommon.myCstr(obj.Country_Code)) > 0 Then
                    txtCountry.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(COUNTRY_NAME,'') AS [COUNTRY NAME] FROM TSPL_COUNTRY_MASTER Where COUNTRY_CODE ='" + clsCommon.myCstr(obj.Country_Code) + "'")
                Else
                    txtCountry.Text = ""
                End If
                If clsCommon.myLen(clsCommon.myCstr(obj.State_Code)) > 0 Then
                    LblState.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(STATE_NAME,'') AS [STATE NAME] FROM TSPL_STATE_MASTER Where STATE_CODE ='" + clsCommon.myCstr(txtState.Value) + "'")
                Else
                    LblState.Text = ""
                End If
                If clsCommon.myLen(clsCommon.myCstr(obj.City_Code)) > 0 Then
                    txtCity.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(CITY_NAME,'') AS [CITY NAME] FROM TSPL_CITY_MASTER Where CITY_CODE ='" + clsCommon.myCstr(fndCity.Value) + "'")
                Else
                    txtCity.Text = ""
                End If
                If clsCommon.myCdbl(obj.Is_Applicable) = 1 Then
                    ChkIsApplicable.Checked = True
                Else
                    ChkIsApplicable.Checked = False
                End If

                txtCode.MyReadOnly = True
                btnSave.Text = "Update"
                btnDelete.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Code not found to delete")
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsHRTravelCityMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully.")
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
#End Region
#Region "Events"
    Private Sub txtcountrycode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcountrycode._MYValidating
        Try
            Dim WhrCls As String = String.Empty
            If clsCommon.CompairString(CmbTrType.SelectedValue, "D") = CompairStringResult.Equal Then
                WhrCls = "  ISNULL(tspl_country_master.COUNTRY_CODE,'')= 'INDIA' "
            ElseIf clsCommon.CompairString(CmbTrType.SelectedValue, "I") = CompairStringResult.Equal Then
                WhrCls = " ISNULL(tspl_country_master.COUNTRY_CODE,'')<> 'INDIA' "
            End If
            Dim qry As String = "select country_code as Code,country_name as Country from tspl_country_master"
            txtcountrycode.Value = clsCommon.ShowSelectForm("HRCNTFND", qry, "Code", WhrCls, txtcountrycode.Value.Replace("'", ""), "Code", isButtonClicked)

            If clsCommon.myLen(txtcountrycode.Value) > 0 Then
                txtCountry.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + txtcountrycode.Value + "'"))
                LblState.Text = ""
                txtState.Value = ""
                txtCity.Text = ""
                fndCity.Value = ""
            Else
                txtCountry.Text = ""
                LblState.Text = ""
                txtState.Value = ""
                txtCity.Text = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtState__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtState._MYValidating
        If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Country First", Me.Text)
            txtcountrycode.Focus()
            txtcountrycode.Select()
            Return
        End If

        Try
            Dim qry As String = "select State_code as Code,state_name as State from tspl_state_master"
            txtState.Value = clsCommon.ShowSelectForm("HRSTAFND", qry, "Code", " country_code='" + txtcountrycode.Value + "'", txtState.Value.Replace("'", ""), "Code", isButtonClicked)

            If clsCommon.myLen(txtState.Value) > 0 Then
                LblState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + txtState.Value + "'"))
                txtCity.Text = ""
                fndCity.Value = ""
            Else
                LblState.Text = ""
                txtState.Value = ""
                txtCity.Text = ""
                fndCity.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub fndCity__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCity._MYValidating
        If clsCommon.myLen(txtState.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select State Code First", Me.Text)
            txtState.Focus()
            txtState.Select()
            Return
        End If
        fndCity.Value = clsCityMaster.getFinder(" state_code='" + txtState.Value + "'", fndCity.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(fndCity.Value) > 0 Then
            txtCity.Text = clsDBFuncationality.getSingleValue("Select City_Name from TSPL_CITY_MASTER where City_Code='" + fndCity.Value + "' and state_code='" + txtState.Value + "'")
        Else
            txtCity.Text = ""
            fndCity.Value = ""
        End If
    End Sub
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Reset()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If AllowToSave() Then
            SaveData()
        End If
    End Sub
    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String
        str = "SELECT TSPL_HR_TRAVEL_CITY_MASTER.Travel_City_Code as [Code], TSPL_HR_TRAVEL_CITY_MASTER.Travel_Type AS [Travel Type],TSPL_HR_TRAVEL_CITY_MASTER.Description as [Description],TSPL_HR_TRAVEL_CITY_MASTER.Country_Code AS [Country Code],ISNULL(TSPL_COUNTRY_MASTER.COUNTRY_NAME,'') AS [Country Name],TSPL_HR_TRAVEL_CITY_MASTER.State_Code AS [State Code],ISNULL(TSPL_STATE_MASTER.STATE_NAME,'') AS [State Name],TSPL_HR_TRAVEL_CITY_MASTER.City_Code AS [City Code],ISNULL(TSPL_CITY_MASTER.CITY_NAME,'') AS [City Name],TSPL_HR_TRAVEL_CITY_MASTER.Is_Applicable AS [Is Applicable] From TSPL_HR_TRAVEL_CITY_MASTER  " & _
        " LEFT OUTER JOIN TSPL_COUNTRY_MASTER ON TSPL_COUNTRY_MASTER.COUNTRY_CODE=TSPL_HR_TRAVEL_CITY_MASTER.COUNTRY_CODE" & _
        " LEFT OUTER JOIN TSPL_STATE_MASTER ON TSPL_STATE_MASTER.STATE_CODE=TSPL_HR_TRAVEL_CITY_MASTER.STATE_CODE" & _
        " LEFT OUTER JOIN TSPL_CITY_MASTER ON TSPL_CITY_MASTER.CITY_CODE=TSPL_HR_TRAVEL_CITY_MASTER.CITY_CODE"
        transportSql.ExporttoExcel(str, Me)
    End Sub
    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim linno As Integer = 0

        If transportSql.importExcel(gv, "Code", "Travel Type", "Description", "Country Code", "Country Name", "State Code", "State Name", "City Code", "City Name", "Is Applicable") Then
            Try
                clsCommon.ProgressBarPercentShow()

                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New ClsHRTravelCityMaster()
                    linno += 1
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value.ToString().Replace("'", ""))
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Travel_City_Code = strCode

                    Dim strTravelType As String = clsCommon.myCstr(grow.Cells("Travel Type").Value)
                    If clsCommon.myLen(strTravelType) > 0 Then
                        If (clsCommon.CompairString(strTravelType, "D") <> CompairStringResult.Equal) AndAlso (clsCommon.CompairString(strTravelType, "I") <> CompairStringResult.Equal) Then
                            Throw New Exception("Travel Type should be 'D','I' at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        Throw New Exception("Travel Type can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Travel_Type = strTravelType.ToUpper()

                    Dim strRemarks As String = clsCommon.myCstr(grow.Cells("Description").Value.ToString().Replace("'", ""))
                    If (String.IsNullOrEmpty(strRemarks)) Then
                        Throw New Exception("Description can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf strRemarks.Length > 150 Then
                        Throw New Exception("Please check ! Description lenght should be 150 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Desp = strRemarks

                    Dim strCountryCode As String = clsCommon.myCstr(grow.Cells("Country Code").Value.ToString().Replace("'", ""))
                    If (String.IsNullOrEmpty(strCountryCode)) Then
                        Throw New Exception("Country code can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim sQuery As String = "select * from  TSPL_COUNTRY_MASTER where COUNTRY_CODE='" + strCountryCode + "'"
                    Dim DTEmp As DataTable = clsDBFuncationality.GetDataTable(sQuery)
                    If DTEmp.Rows.Count <= 0 Then
                        Throw New Exception("Please check country code '" + strCountryCode + "' .It dose not exits in country master at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.CompairString(strTravelType, "D") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCountryCode, "India") <> CompairStringResult.Equal Then
                            Throw New Exception("Please check country code should be 'India' at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    ElseIf clsCommon.CompairString(strTravelType, "I") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strCountryCode, "India") = CompairStringResult.Equal Then
                            Throw New Exception("Please check country code should not be 'India' at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.Country_Code = strCountryCode.ToUpper()

                    Dim strStateCode As String = clsCommon.myCstr(grow.Cells("State Code").Value.ToString().Replace("'", ""))
                    If (String.IsNullOrEmpty(strStateCode)) Then
                        Throw New Exception("State code can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim StateQry As String = "select * from  TSPL_State_MASTER where State_CODE='" + strStateCode + "'"
                    Dim DTState As DataTable = clsDBFuncationality.GetDataTable(StateQry)
                    If DTState.Rows.Count <= 0 Then
                        Throw New Exception("Please check state code '" + strStateCode + "' .It dose not exits in state master.")
                    End If
                    Dim SMWithCM As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_STATE_MASTER WHERE COUNTRY_CODE ='" & strCountryCode & "' AND STATE_CODE ='" & strStateCode & "'"))
                    If clsCommon.myCdbl(SMWithCM) <= 0 Then
                        Throw New Exception("Please check mapped state code '" + strStateCode + "' does not lie under country code '" + strCountryCode + "'.")
                    End If
                    obj.State_Code = strStateCode.ToUpper()

                    Dim strCityCode As String = clsCommon.myCstr(grow.Cells("City Code").Value.ToString().Replace("'", ""))
                    If (String.IsNullOrEmpty(strCityCode)) Then
                        Throw New Exception("City code can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim CityQry As String = "select * from  TSPL_City_MASTER where City_CODE='" + strCityCode + "'"
                    Dim DTCity As DataTable = clsDBFuncationality.GetDataTable(CityQry)
                    If DTCity.Rows.Count <= 0 Then
                        Throw New Exception("Please check city code '" + strCityCode + "' .It dose not exits in city master.")
                    End If
                    Dim CMWithSM As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_CITY_MASTER WHERE CITY_CODE ='" & strCityCode & "' AND STATE_CODE ='" & strStateCode & "'"))
                    If clsCommon.myCdbl(CMWithSM) <= 0 Then
                        Throw New Exception("Please check mapped city code '" + strCityCode + "' does not lie under state code '" + strStateCode + "'.")
                    End If
                    obj.City_Code = strCityCode.ToUpper()

                    Dim strIsApplicable As String = clsCommon.myCstr(grow.Cells("Is Applicable").Value)
                    If clsCommon.myLen(strIsApplicable) > 0 Then
                        If (clsCommon.CompairString(strIsApplicable, "1") <> CompairStringResult.Equal) AndAlso (clsCommon.CompairString(strIsApplicable, "0") <> CompairStringResult.Equal) Then
                            Throw New Exception("Is Applicable should be '1','0' at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        Throw New Exception("Is Applicable can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Is_Applicable = strIsApplicable.ToUpper()

                    ClsHRTravelCityMaster.SaveData(obj, obj.Travel_City_Code)
                Next
                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarPercentHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
    Private Sub FrmHRTravelCityMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            If AllowToSave() Then
                SaveData()
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso BtnClose.Enabled Then
            Me.Close()
            GC.Collect()
        End If
    End Sub
    Private Sub FrmHRTravelCityMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(BtnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_HR_TRAVEL_CITY_MASTER where Travel_City_Code='" + txtCode.Value + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtCode.MyReadOnly = True
            ElseIf check <= 0 Then
                txtCode.MyReadOnly = False
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_HR_TRAVEL_CITY_MASTER where Travel_City_Code ='" + txtCode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then

                txtCode.Value = ClsHRTravelCityMaster.GetFinder("", txtCode.Value, isButtonClicked)
                If clsCommon.myLen(txtCode.Value) > 0 Then
                    btnDelete.Enabled = True
                    btnSave.Text = "Update"
                    txtCode.MyReadOnly = True
                Else
                    btnSave.Text = "Save"
                    btnDelete.Enabled = False
                    txtCode.MyReadOnly = False
                End If
            End If
            LoadData(txtCode.Value, NavigatorType.Current)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub CmbTrType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles CmbTrType.SelectedIndexChanged
        If ComboLoad = True Then
            If clsCommon.CompairString(CmbTrType.SelectedValue, "D") = CompairStringResult.Equal Then
                Dim CountCM As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) AS Row From TSPL_COUNTRY_MASTER WHERE COUNTRY_CODE='INDIA'"))
                If CountCM > 0 Then
                    txtcountrycode.Value = "INDIA"
                    txtCountry.Text = "INDIA"
                    LblState.Text = ""
                    txtState.Value = ""
                    txtCity.Text = ""
                    fndCity.Value = ""
                Else
                    txtcountrycode.Value = ""
                    txtCountry.Text = ""
                    LblState.Text = ""
                    txtState.Value = ""
                    txtCity.Text = ""
                    fndCity.Value = ""
                End If
            Else
                txtcountrycode.Value = ""
                txtCountry.Text = ""
                LblState.Text = ""
                txtState.Value = ""
                txtCity.Text = ""
                fndCity.Value = ""
            End If
        End If
    End Sub

    Private Sub txtcountrycode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcountrycode.KeyDown
        If clsCommon.myCdbl(e.KeyCode) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtcountrycode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcountrycode.KeyPress
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndCity_KeyDown(sender As Object, e As KeyEventArgs) Handles fndCity.KeyDown
        If clsCommon.myCdbl(e.KeyCode) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub fndCity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles fndCity.KeyPress
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtState_KeyDown(sender As Object, e As KeyEventArgs) Handles txtState.KeyDown
        If clsCommon.myCdbl(e.KeyCode) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtState_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtState.KeyPress
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        End If
    End Sub
#End Region
End Class
