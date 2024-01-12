'-Created By --[Pankaj Kumar Chaudhary]--against Ticket-[]
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class FrmSecondaryCustomer
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim dt As DataTable

    Private Sub FrmSecondaryCustomer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData(fndCustomer.Value)
        End If
    End Sub

    Private Sub FrmSecondaryCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ButtonToolTip As ToolTip = New ToolTip()
        SetUserMgmtNew()
        ValidateLength()
        '----------For Custom Fields----------
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        '---------End of For Custom Fields----
    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmSecondaryCustomer)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            rmiExport.Enabled = True
            rmiImport.Enabled = True
        Else
            rmiExport.Enabled = False
            rmiImport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub ValidateLength()
        fndCustomer.MyMaxLength = 12
        txtCustomerName.MaxLength = 200
        txtAdd1.MaxLength = 50
        txtAdd2.MaxLength = 50
        txtAdd3.MaxLength = 50
        txtCountry.MaxLength = 50
        txtfax.MaxLength = 12
        txtPhone1.MaxLength = 20
        txtPhone2.MaxLength = 20
        txtEmail.MaxLength = 50
        txtWeb.MaxLength = 50
    End Sub

    Private Sub fndCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer._MYValidating

        'qry = "select Cust_Code as [CustomerCode],Customer_Name as [Name],[Status] from TSPL_SECONDARY_CUSTOMER_MASTER  "
        'fndCustomer.Value = clsCommon.ShowSelectForm("SecCUSTOMEFND", qry, "CustomerCode", "", fndCustomer.Value, "", isButtonClicked)
        fndCustomer.Value = clsSecondaryCustomer.getFinder("", fndCustomer.Value, isButtonClicked)
        LoadData(fndCustomer.Value, NavigatorType.Current)
    End Sub

    Private Sub fndCustomer__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCustomer._MYNavigator
        Try
            qry = "select count(*) from TSPL_SECONDARY_CUSTOMER_MASTER where Cust_Code='" + fndCustomer.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                fndCustomer.MyReadOnly = False
            Else
                fndCustomer.MyReadOnly = True
            End If
            LoadData(fndCustomer.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strSecCustomerCode As String, ByVal NavType As NavigatorType)
        Try
            Reset()
            Dim obj As New clsSecondaryCustomer
            obj = clsSecondaryCustomer.GetData(strSecCustomerCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Cust_Code) > 0 Then
                fndCustomer.Value = obj.Cust_Code
                txtCustomerName.Text = obj.Customer_Name
                txtDistributor.Value = obj.Distributor
                lblDistributorName.Text = clsSecondaryCustomer.getDistributorName(txtDistributor.Value, Nothing)
                txtAdd1.Text = obj.Add1
                txtAdd2.Text = obj.Add2
                txtAdd3.Text = obj.Add3
                fndCity.Value = obj.City_Code
                lblCityName.Text = clsSecondaryCustomer.getCityName(fndCity.Value, Nothing)
                fndstate.Value = obj.State
                lblStateName.Text = clsSecondaryCustomer.getStateName(fndstate.Value, Nothing)
                txtCountry.Text = obj.Country
                txtPhone1.Text = obj.Phone1
                txtPhone2.Text = obj.Phone2
                txtfax.Text = obj.Fax
                txtEmail.Text = obj.Email
                txtWeb.Text = obj.WebSite
                fndCustCurrency.Value = obj.CURRENCY_CODE
                lblCurrencyName.Text = clsCurrencyMaster.GetCurrencyName(fndCustCurrency.Value, Nothing)
                chkInActive.Checked = IIf(clsCommon.CompairString("Inactive", obj.Status) = CompairStringResult.Equal, True, False)
                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields
                btnSave.Text = "Update"
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCustomerName.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please enter Customer Name", Me.Text)
            txtCustomerName.Focus()
            Return False
        ElseIf clsCommon.myLen(txtDistributor.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Distributor.", Me.Text)
            txtDistributor.Focus()
            Return False
        End If
        Return True
    End Function

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim Arr As New List(Of clsSecondaryCustomer)
                Dim obj As New clsSecondaryCustomer()
                obj.Cust_Code = clsCommon.myCstr(fndCustomer.Value)
                obj.Customer_Name = clsCommon.myCstr(txtCustomerName.Text)
                obj.Distributor = clsCommon.myCstr(txtDistributor.Value)
                obj.Add1 = clsCommon.myCstr(txtAdd1.Text)
                obj.Add2 = clsCommon.myCstr(txtAdd2.Text)
                obj.Add3 = clsCommon.myCstr(txtAdd3.Text)
                obj.City_Code = clsCommon.myCstr(fndCity.Value)
                obj.State = clsCommon.myCstr(fndstate.Value)
                obj.Country = clsCommon.myCstr(txtCountry.Text)
                obj.Phone1 = clsCommon.myCstr(txtPhone1.Text)
                obj.Phone2 = clsCommon.myCstr(txtPhone2.Text)
                obj.Fax = clsCommon.myCstr(txtfax.Text)
                obj.Email = clsCommon.myCstr(txtEmail.Text)
                obj.WebSite = clsCommon.myCstr(txtWeb.Text)
                obj.CURRENCY_CODE = clsCommon.myCstr(fndCustCurrency.Value)
                obj.Status = IIf(chkInActive.Checked, "InActive", "Active")
                Arr.Add(obj)
                If (clsSecondaryCustomer.SaveData(Arr)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Cust_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData(fndCustomer.Value)
    End Sub

    Private Sub DeleteData(ByVal strCustCode As String)
        Try
            If clsCommon.myLen(strCustCode) > 0 Then
                If clsSecondaryCustomer.DeleteData(strCustCode) Then
                    clsCommon.MyMessageBoxShow(Me, "Data deleted successfully.", Me.Text)
                    Reset()
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Customer found to delete.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Reset()
    End Sub

    Private Sub Reset()
        Me.fndCustomer.Value = ""
        Me.fndCity.Value = ""
        fndCustCurrency.Value = ""
        Me.txtCustomerName.Text = ""
        Me.txtAdd1.Text = ""
        Me.txtAdd2.Text = ""
        Me.txtAdd3.Text = ""
        chkInActive.Checked = False
        Me.fndstate.Value = ""
        Me.txtCountry.Text = ""
        Me.txtPhone1.Text = ""
        Me.txtPhone2.Text = ""
        Me.txtfax.Text = ""
        Me.txtEmail.Text = ""
        Me.txtWeb.Text = ""
        txtDistributor.Value = ""
        lblDistributorName.Text = ""
        lblStateName.Text = ""
        lblCityName.Text = ""
        lblCurrencyName.Text = ""
        btnSave.Text = "Save"
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
    End Sub

    Private Sub txtDistributor__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDistributor._MYValidating
        'qry = "Select Cust_Code as Code, Customer_Name as Name from TSPL_CUSTOMER_MASTER"
        'txtDistributor.Value = clsCommon.ShowSelectForm("fndDisctr@sc", qry, "Code", "Cust_Type_Code='D'", txtDistributor.Value, "", isButtonClicked)
        txtDistributor.Value = clsCustomerMaster.getFinder("IsDistributor='Y'", txtDistributor.Value, isButtonClicked)
        lblDistributorName.Text = clsSecondaryCustomer.getDistributorName(txtDistributor.Value, Nothing)
    End Sub

    Private Sub fndCity__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCity._MYValidating
        'qry = "SELECT [City_Code] as Code,[City_Name] as [Name] FROM [TSPL_CITY_MASTER]"
        'fndCity.Value = clsCommon.ShowSelectForm("SFNDCITY_CODE", qry, "Code", "", fndCity.Value, "", isButtonClicked)
        fndCity.Value = clsCityMaster.getFinder("", fndCity.Value, isButtonClicked)
        lblCityName.Text = clsSecondaryCustomer.getCityName(fndCity.Value, Nothing)
    End Sub

    Private Sub fndstate__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndstate._MYValidating
        'qry = "SELECT [State_Code] as Code,[State_Name] as [Name] FROM [TSPL_State_MASTER]"
        'fndstate.Value = clsCommon.ShowSelectForm("fndState@sc", qry, "Code", "", fndstate.Value, "", isButtonClicked)
        fndstate.Value = clsStateMaster.getFinder("", fndstate.Value, isButtonClicked)
        lblStateName.Text = clsSecondaryCustomer.getStateName(fndstate.Value, Nothing)
    End Sub

    Private Sub fndCustCurrency__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCustCurrency._MYValidating
        'qry = "Select CURRENCY_CODE As Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        'fndCustCurrency.Value = clsCommon.ShowSelectForm("fndCurrency@sc", qry, "Code", "", fndCustCurrency.Value, "", isButtonClicked)
        fndCustCurrency.Value = clsCurrencyMaster.getFinder("", fndCustCurrency.Value, isButtonClicked)
        lblCurrencyName.Text = clsCurrencyMaster.GetCurrencyName(fndCustCurrency.Value, Nothing)
    End Sub

    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        Try
            qry = "Select TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code as [Customer Code], TSPL_SECONDARY_CUSTOMER_MASTER.Customer_Name as [Customer Name]," & _
            " TSPL_SECONDARY_CUSTOMER_MASTER.Distributor as [Distributor Code], TSPL_CUSTOMER_MASTER.Customer_Name as [Distributor Name]," & _
            " TSPL_SECONDARY_CUSTOMER_MASTER.Add1 as [Address1], TSPL_SECONDARY_CUSTOMER_MASTER.Add2 as [Address2], TSPL_SECONDARY_CUSTOMER_MASTER.Add3 as [Address3]," & _
            " TSPL_SECONDARY_CUSTOMER_MASTER.City_Code as [City Code], TSPL_CITY_MASTER.City_Name as [City Name], TSPL_SECONDARY_CUSTOMER_MASTER.State as [State Code]," & _
            " TSPL_STATE_MASTER.STATE_NAME as [State Name], TSPL_SECONDARY_CUSTOMER_MASTER.[Country], TSPL_SECONDARY_CUSTOMER_MASTER.Phone1," & _
            " TSPL_SECONDARY_CUSTOMER_MASTER.Phone2, TSPL_SECONDARY_CUSTOMER_MASTER.Fax, TSPL_SECONDARY_CUSTOMER_MASTER.Email, TSPL_SECONDARY_CUSTOMER_MASTER.WebSite," & _
            " TSPL_SECONDARY_CUSTOMER_MASTER.CURRENCY_CODE as [Currency Code], TSPL_CURRENCY_MASTER.CURRENCY_NAME as [Currency Name], TSPL_SECONDARY_CUSTOMER_MASTER.Status from TSPL_SECONDARY_CUSTOMER_MASTER " & _
            " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SECONDARY_CUSTOMER_MASTER.Distributor" & _
            " Left Outer Join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_SECONDARY_CUSTOMER_MASTER.City_Code" & _
            " LEFT OUTER JOIN TSPL_STATE_MASTER On TSPL_STATE_MASTER.STATE_CODE=TSPL_SECONDARY_CUSTOMER_MASTER.State" & _
            " LEFT OUTER JOIN TSPL_CURRENCY_MASTER On TSPL_CURRENCY_MASTER.CURRENCY_CODE=TSPL_SECONDARY_CUSTOMER_MASTER.CURRENCY_CODE"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count <= 0 Then
                qry = "Select '' As [Customer Code], '' as [Customer Name], '' As [Distributor Code], 0 as [Distributor Name], '' as [Address1], '' as [Address2], '' as [Address3]," & _
                " '' as [Address1], '' as [City Code], '' as [City Name], '' as [State Code], '' as [State Name], '' as [Country], '' as Phone1, '' as Phone2, '' as Fax," & _
                " '' as Email, '' as WebSite, '' as [Currency Code], '' as [Currency Name], '' as Status"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImport.Click
        Try
            ImportCustomers()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ImportCustomers()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Customer Code", "Customer Name", "Distributor Code", "Distributor Name", "Address1", "Address2", "Address3", "City Code", "City Name", "State Code", "State Name", "Country", "Phone1", "Phone2", "Fax", "Email", "WebSite", "Currency Code", "Currency Name", "Status") Then
            clsCommon.ProgressBarShow()
            ' Dim trans As SqlTransaction
            Try
                Dim temp As String = ""
                Dim Arr As New List(Of clsSecondaryCustomer)
                Dim LineNo As String
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsSecondaryCustomer()
                    LineNo = clsCommon.myCstr(grow.Index + 2)

                    obj.Cust_Code = clsCommon.myCstr(grow.Cells("Customer Code").Value)

                    obj.Customer_Name = clsCommon.myCstr(grow.Cells("Customer Name").Value)
                    If clsCommon.myLen(obj.Customer_Name) > 200 Then
                        Throw New Exception("line '" + LineNo + "' : Length of Customer Name can not be greater than 200.")
                    End If

                    obj.Distributor = clsCommon.myCstr(grow.Cells("Distributor Code").Value)
                    If clsCommon.myLen(obj.Distributor) > 0 Then
                        temp = clsDBFuncationality.getSingleValue("Select Cust_Code from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Distributor + "'")
                        If Not clsCommon.CompairString(obj.Distributor, temp) = CompairStringResult.Equal Then
                            Throw New Exception("line '" + LineNo + "' : Distributor does not exist.")
                        End If
                    End If

                    obj.Add1 = clsCommon.myCstr(grow.Cells("Address1").Value)
                    If clsCommon.myLen(obj.Add1) > 50 Then
                        Throw New Exception("line '" + LineNo + "' : Length of Address1 can not be greater than 50.")
                    End If

                    obj.Add2 = clsCommon.myCstr(grow.Cells("Address2").Value)
                    If clsCommon.myLen(obj.Add2) > 50 Then
                        Throw New Exception("line '" + LineNo + "' : Length of Address2 can not be greater than 50.")
                    End If

                    obj.Add3 = clsCommon.myCstr(grow.Cells("Address3").Value)
                    If clsCommon.myLen(obj.Add3) > 50 Then
                        Throw New Exception("line '" + LineNo + "' : Length of Address3 can not be greater than 50.")
                    End If

                    obj.City_Code = clsCommon.myCstr(grow.Cells("City Code").Value)
                    If clsCommon.myLen(obj.City_Code) > 0 Then
                        temp = clsDBFuncationality.getSingleValue("Select City_Code from TSPL_City_MASTER WHERE City_Code='" + obj.City_Code + "'")
                        If Not clsCommon.CompairString(obj.City_Code, temp) = CompairStringResult.Equal Then
                            Throw New Exception("line '" + LineNo + "' : City Code does not exist.")
                        End If
                    End If

                    obj.State = clsCommon.myCstr(grow.Cells("State Code").Value)
                    If clsCommon.myLen(obj.State) > 0 Then
                        temp = clsDBFuncationality.getSingleValue("Select State_Code from TSPL_State_MASTER WHERE State_Code='" + obj.State + "'")
                        If Not clsCommon.CompairString(obj.State, temp) = CompairStringResult.Equal Then
                            Throw New Exception("line '" + LineNo + "' : State Code does not exist.")
                        End If
                    End If

                    obj.Country = clsCommon.myCstr(grow.Cells("Country").Value)
                    If clsCommon.myLen(obj.Country) > 50 Then
                        Throw New Exception("line '" + LineNo + "' : Length of Country can not be greater than 50.")
                    End If

                    obj.Phone1 = clsCommon.myCstr(grow.Cells("Phone1").Value)
                    If clsCommon.myLen(obj.Phone1) > 20 Then
                        Throw New Exception("line '" + LineNo + "' : Length of Phone1 can not be greater than 50.")
                    End If

                    obj.Phone2 = clsCommon.myCstr(grow.Cells("Phone2").Value)
                    If clsCommon.myLen(obj.Phone2) > 20 Then
                        Throw New Exception("line '" + LineNo + "' : Length of Phone2 can not be greater than 50.")
                    End If

                    obj.Fax = clsCommon.myCstr(grow.Cells("Fax").Value)
                    If clsCommon.myLen(obj.Fax) > 12 Then
                        Throw New Exception("line '" + LineNo + "' : Length of Fax can not be greater than 12.")
                    End If

                    obj.Email = clsCommon.myCstr(grow.Cells("Email").Value)
                    If clsCommon.myLen(obj.Email) > 12 Then
                        Throw New Exception("line '" + LineNo + "' : Length of Email can not be greater than 50.")
                    End If

                    obj.WebSite = clsCommon.myCstr(grow.Cells("WebSite").Value)
                    If clsCommon.myLen(obj.WebSite) > 12 Then
                        Throw New Exception("line '" + LineNo + "' : Length of WebSite can not be greater than 12.")
                    End If

                    obj.CURRENCY_CODE = clsCommon.myCstr(grow.Cells("Currency Code").Value)
                    If clsCommon.myLen(obj.CURRENCY_CODE) > 0 Then
                        temp = clsDBFuncationality.getSingleValue("Select CURRENCY_CODE from TSPL_CURRENCY_MASTER WHERE CURRENCY_CODE='" + obj.CURRENCY_CODE + "'")
                        If Not clsCommon.CompairString(obj.CURRENCY_CODE, temp) = CompairStringResult.Equal Then
                            Throw New Exception("line '" + LineNo + "' : Currency Code does not exist.")
                        End If
                    End If
                    obj.Status = clsCommon.myCstr(grow.Cells("Status").Value)
                    If Not (clsCommon.CompairString(obj.Status, "InActive") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Status, "Active") = CompairStringResult.Equal Or obj.State = "") Then
                        Throw New Exception("line '" + LineNo + "' : Status shouled be 'Active' or 'InActive' or 'Blank'.")
                    End If
                    Arr.Add(obj)
                Next
                If Arr.Count > 0 And (clsSecondaryCustomer.SaveData(Arr)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text)
                Else
                    Throw New Exception("No row found to Import.")
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                Throw New Exception(ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmiClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiClose.Click
        Me.Close()
    End Sub
End Class
