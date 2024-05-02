Imports common
Public Class FrmCustomerMaster
    Dim isNewEntry As Boolean = False
    Public strCustCode As String = ""
    Public strShipCode As String = ""

    Private Sub SplitContainer1_Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        strShipCode = txtShipCode.Value
        strCustCode = txtCustCode.Value
        Me.Close()
    End Sub

    Private Sub txtCustCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustCode._MYValidating
        Try
            Dim qry As String = "select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCustCode.MyReadOnly = False
            Else
                txtCustCode.MyReadOnly = True
            End If
            If txtCustCode.MyReadOnly OrElse isButtonClicked Then
                qry = "select Cust_Code,Customer_Name from TSPL_CUSTOMER_MASTER"
                Dim whrClas As String = ""
                LoadDataCustomer(clsCommon.ShowSelectForm("CMCode", qry, "Cust_Code", whrClas, txtCustCode.Value, "", isButtonClicked), NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCustCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCustCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCustCode.MyReadOnly = False
            Else
                txtCustCode.MyReadOnly = True
            End If
            LoadDataCustomer(txtCustCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadDataCustomer(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        BlankAllControl()
        isNewEntry = False
        Dim obj As clsCustomerMasterNew = clsCustomerMasterNew.GetData(strCode, NavTyep, Nothing)
        If obj IsNot Nothing Then
            txtCustCode.Value = obj.Cust_Code
            txtCustDescription.Text = obj.Customer_Name
            txtCustContactNo.Text = obj.Phone1
            txtCustAdd1.Text = obj.Add1
            txtCustAdd2.Text = obj.Add2
            txtCustCity.Value = obj.City_Code
            lblCustCity.Text = clsCityMaster.GetName(obj.City_Code)
            txtCustState.Value = obj.State
            lblCustState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select State_Name from TSPL_TDS_STATE_MASTER where State_Code='" + obj.State + "'"))
            txtCustCountry.Text = obj.Country
            txtCustPINCode.Text = obj.PIN_Code
            txtCustEmailID.Text = obj.Contact_Person_Email
            txtCustContactPerson.Text = obj.Contact_Person_Name

            If obj.Cust_DOB IsNot Nothing Then
                txtCustDOB.Checked = True
                txtCustDOB.Value = obj.Cust_DOB
            End If
            If obj.Cust_Spouse_DOB IsNot Nothing Then
                txtCustSpouseDOB.Checked = True
                txtCustSpouseDOB.Value = obj.Cust_Spouse_DOB
            End If
            If obj.Anniversary_Date IsNot Nothing Then
                txtCustAnniversaryDate.Checked = True
                txtCustAnniversaryDate.Value = obj.Anniversary_Date
            End If


            cboCustGender.SelectedValue = obj.Gender
            cboCustOccation.SelectedValue = obj.Occation
        End If


    End Sub

    Private Sub LoadDataShip(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim objShip As clsShipToLocation = clsShipToLocation.GetData(strCode, NavTyep)
        If objShip IsNot Nothing Then
            txtShipCode.Value = objShip.Ship_To_Code
            txtShipDescription.Text = objShip.Ship_To_Desc
            'objShip.Ship_To_Type = txtShipCode.Value
            'objShip.Ship_To_Type_Code = txtShipCode.Value
            'objShip.Ship_To_Type_Desc = txtShipCode.Value
            txtShipAdd1.Text = objShip.Add1
            txtShipAdd2.Text = objShip.Add2
            txtShipCity.Value = objShip.City_Code
            lblShipCity.Text = clsCityMaster.GetName(objShip.City_Code)
            txtShipState.Value = objShip.State
            lblShipState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select State_Name from TSPL_TDS_STATE_MASTER where State_Code='" + objShip.State + "'"))
            txtShipPINCode.Text = objShip.Pin_Code
            txtShipCountry.Text = objShip.Country
            txtShipContactNo.Text = objShip.Telphone
            txtShipEmailID.Text = objShip.Email
        End If
    End Sub

    Sub BlankAllControl()
        txtCustCode.Value = ""
        txtCustDescription.Text = ""
        txtCustContactNo.Text = ""
        txtCustAdd1.Text = ""
        txtCustAdd2.Text = ""
        txtCustCity.Value = ""
        lblCustCity.Text = ""
        txtCustState.Value = ""
        lblCustState.Text = ""
        txtCustCountry.Text = ""
        txtCustPINCode.Text = ""
        txtCustEmailID.Text = ""
        txtCustContactPerson.Text = ""
        txtCustDOB.Value = clsCommon.GETSERVERDATE()
        txtCustSpouseDOB.Value = txtCustDOB.Value
        txtCustAnniversaryDate.Value = txtCustDOB.Value
        cboCustGender.SelectedValue = ""
        cboCustOccation.SelectedValue = ""

        txtShipDescription.Text = ""
        txtShipContactNo.Text = ""
        txtShipAdd1.Text = ""
        txtShipAdd2.Text = ""
        txtShipCity.Value = ""
        lblShipCity.Text = ""
        txtShipState.Value = ""
        lblShipState.Text = ""
        txtShipCountry.Text = ""
        txtShipPINCode.Text = ""
        txtShipEmailID.Text = ""
        txtShipContactPerson.Text = ""

        txtCustDOB.Checked = False
        txtCustSpouseDOB.Checked = False
        txtCustAnniversaryDate.Checked = False
    End Sub

    Private Sub chkSameAddressForShipping_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSameAddressForShipping.ToggleStateChanged
        If chkSameAddressForShipping.Checked Then
            txtShipDescription.Text = txtCustDescription.Text
            txtShipContactNo.Text = txtCustContactNo.Text
            txtShipAdd1.Text = txtCustAdd1.Text
            txtShipAdd2.Text = txtCustAdd2.Text
            txtShipCity.Text = txtCustCity.Text
            lblShipCity.Text = lblCustCity.Text
            txtShipState.Text = txtCustState.Text
            lblShipState.Text = lblCustState.Text
            txtShipCountry.Text = txtCustCountry.Text
            txtShipPINCode.Text = txtCustPINCode.Text
            txtShipEmailID.Text = txtCustEmailID.Text
            txtShipContactPerson.Text = txtCustContactPerson.Text

            txtShipState.Value = txtCustState.Value
            txtShipCity.Value = txtCustCity.Value
        End If

    End Sub

    Private Sub txtShipEmailID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShipEmailID.TextChanged

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim obj As New clsCustomerMasterNew()
            obj.Cust_Code = txtCustCode.Value
            obj.Customer_Name = txtCustDescription.Text
            obj.Phone1 = txtCustContactNo.Text
            obj.Add1 = txtCustAdd1.Text
            obj.Add2 = txtCustAdd2.Text
            obj.City_Code = txtCustCity.Text
            'obj.City_name = lblCustCity.Text
            obj.State = txtCustState.Text
            'lblCustState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select State_Name from TSPL_TDS_STATE_MASTER where State_Code='" + obj.State + "'"))
            obj.Country = txtCustCountry.Text
            obj.PIN_Code = txtCustPINCode.Text
            obj.Contact_Person_Email = txtCustEmailID.Text
            obj.Contact_Person_Name = txtCustContactPerson.Text

            If txtCustDOB.Checked Then
                obj.Cust_DOB = txtCustDOB.Value
            End If
            If txtCustSpouseDOB.Checked Then
                obj.Cust_Spouse_DOB = txtCustSpouseDOB.Value
            End If
            If txtCustAnniversaryDate.Checked Then
                obj.Anniversary_Date = txtCustAnniversaryDate.Value
            End If
            cboCustGender.SelectedValue = obj.Gender
            cboCustOccation.SelectedValue = obj.Occation
            obj.OnHold = "N"

            Dim objShip As clsShipToLocation = New clsShipToLocation()
            objShip.Ship_To_Code = txtShipCode.Value
            objShip.Ship_To_Desc = txtShipDescription.Text
            'objShip.Ship_To_Type = txtShipCode.Value
            'objShip.Ship_To_Type_Code = txtShipCode.Value
            'objShip.Ship_To_Type_Desc = txtShipCode.Value
            objShip.Add1 = txtShipAdd1.Text
            objShip.Add2 = txtShipAdd2.Text
            objShip.City_Code = txtShipCity.Value
            objShip.State = txtShipState.Value
            objShip.Pin_Code = txtShipPINCode.Text
            objShip.Country = txtShipCountry.Text
            objShip.Telphone = txtShipContactNo.Text
            objShip.Email = txtShipEmailID.Text

            If obj.SaveDataPOS(obj, objShip, isNewEntry) Then
                RadMessageBox.Show("Data saved successfully", Me.Text)
                LoadDataCustomer(obj.Cust_Code, NavigatorType.Current)
                LoadDataShip(objShip.Ship_To_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        isNewEntry = True
        txtCustCode.MyReadOnly = False
        BlankAllControl()

    End Sub

    Private Sub FrmCustomerMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadOccation()
        LoadGender()
        If clsCommon.myLen(strCustCode) > 0 Then
            LoadDataCustomer(strCustCode, NavigatorType.Current)
        Else
            isNewEntry = True
        End If

        If clsCommon.myLen(strShipCode) > 0 Then
            LoadDataShip(strShipCode, NavigatorType.Current)
        End If

    End Sub

    Sub LoadOccation()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Others"
        dr("Name") = "Others"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Birtday"
        dr("Name") = "Birtday"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Anniversary"
        dr("Name") = "Anniversary"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Marriage"
        dr("Name") = "Marriage"
        dt.Rows.Add(dr)


        cboCustOccation.DataSource = dt
        cboCustOccation.ValueMember = "Code"
        cboCustOccation.DisplayMember = "Name"
    End Sub

    Sub LoadGender()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Male"
        dr("Name") = "Male"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Female"
        dr("Name") = "Female"
        dt.Rows.Add(dr)

        cboCustGender.DataSource = dt
        cboCustGender.ValueMember = "Code"
        cboCustGender.DisplayMember = "Name"
    End Sub

    Private Sub txtCustCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustCity._MYValidating
        Dim qry As String = "select City_Code as Code,City_Name as Name from TSPL_CITY_MASTER "
        txtCustCity.Value = clsCommon.ShowSelectForm("CSNCity", qry, "Code", "", txtCustCity.Value, "Code", isButtonClicked)
        lblCustCity.Text = clsCityMaster.GetName(txtCustCity.Value)
    End Sub

    Private Sub txtCustState__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustState._MYValidating
        Dim qry As String = "select State_Code as Code,State_Name as Name from TSPL_TDS_STATE_MASTER "
        txtCustState.Value = clsCommon.ShowSelectForm("CSNState", qry, "Code", "", txtCustState.Value, "Code", isButtonClicked)
        lblCustState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select State_Name as Name from TSPL_TDS_STATE_MASTER where State_Code ='" + txtCustState.Value + "'"))
    End Sub

    Private Sub txtShipCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipCity._MYValidating
        Dim qry As String = "select City_Code as Code,City_Name as Name from TSPL_CITY_MASTER "
        txtShipCity.Value = clsCommon.ShowSelectForm("CSNCity", qry, "Code", "", txtCustCity.Value, "Code", isButtonClicked)
        lblShipCity.Text = clsCityMaster.GetName(txtShipCity.Value)
    End Sub

    Private Sub txtShipState__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipState._MYValidating
        Dim qry As String = "select State_Code as Code,State_Name as Name from TSPL_TDS_STATE_MASTER "
        txtShipState.Value = clsCommon.ShowSelectForm("CSNState", qry, "Code", "", txtShipState.Value, "Code", isButtonClicked)
        lblShipState.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select State_Name as Name from TSPL_TDS_STATE_MASTER where State_Code ='" + txtShipState.Value + "'"))

    End Sub

    Private Sub txtShipCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtShipCode._MYNavigator
        LoadDataShip(txtShipCode.Value, NavType)
    End Sub

    Private Sub txtShipCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipCode._MYValidating
        Dim qry As String = "select Ship_To_Code,Ship_To_Desc,Ship_To_Type,Ship_To_Type_Code,Ship_To_Type_Desc,Add1,Add2,City_Code,State,Pin_Code,Country,Telphone,Email, Tin_No as [Tin No], CST_No as [CST No] from TSPL_SHIP_TO_LOCATION"

        LoadDataShip(clsCommon.ShowSelectForm("CMShipCode", qry, "Ship_To_Code", "", txtShipCode.Value, "", isButtonClicked), NavigatorType.Current)

    End Sub
End Class
