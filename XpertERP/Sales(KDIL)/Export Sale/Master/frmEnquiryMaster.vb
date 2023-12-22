'===================created by monika=====BM00000003440===================
Imports common
Imports System.Data.SqlClient

Public Class FrmEnquiryMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As New ToolTip()
    Dim ErrorControl As New clsErrorControl()
    Dim isNewEntry As Boolean = True
    
#End Region

    Private Sub FunReset()
        isNewEntry = True
        txtcust_code.Text = ""
        txtcust_name.Text = ""
        txtCode.Value = ""
        dtpDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtCustomerName.Text = ""
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtAdd3.Text = ""
        fndCountry.Value = ""
        txtcountryName.Text = ""
        fndstate.Value = ""
        txtstateName.Text = ""
        fndCity.Value = ""
        txtcityName.Text = ""
        CmbTransaction.SelectedValue = ""

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        txtCode.MyReadOnly = False
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnsave.Text = "Save"

        txtCustomerName.Focus()
        txtCustomerName.Select()

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub LoadComboBox()
        Dim qry As String = "select * from (select '' as Code,'None' as Name union all select 'Local' as Code,'Local' as Name union all select 'Export' as Code,'Export' as Name)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        CmbTransaction.DataSource = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            CmbTransaction.DataSource = dt

            CmbTransaction.DisplayMember = "Name"
            CmbTransaction.ValueMember = "Code"
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmEnquiryMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
        End If
        btnSave.Visible = MyBase.isModifyFlag
        If btnsave.Visible = True Then
            '    RadMenuItem3.Enabled = True
            '    RadMenuItem4.Enabled = True
            'Else
            '    RadMenuItem3.Enabled = False
            '    RadMenuItem4.Enabled = False
        End If
        '--------------------------------------------------

        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtCustomerName.Text) <= 0 Then
                txtCustomerName.Focus()
                txtCustomerName.Select()
                ErrorControl.SetError(txtCustomerName, "Fill Name")
                Throw New Exception("Fill Name")
            Else
                ErrorControl.ResetError(txtCustomerName)
            End If

            If clsCommon.myLen(CmbTransaction.SelectedValue) <= 0 Then
                CmbTransaction.Select()
                ErrorControl.SetError(CmbTransaction, "Select transaction type.")
                Throw New Exception("Select transaction type.")
            Else
                ErrorControl.ResetError(CmbTransaction)
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub SaveData()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmEnquiryMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim obj As New clsEnquiryMaster()

            obj.code = clsCommon.myCstr(txtCode.Value)
            obj.docdate = clsCommon.myCDate(dtpDate.Text)
            obj.descrptn = clsCommon.myCstr(txtCustomerName.Text).Replace("'", "`")
            obj.add1 = clsCommon.myCstr(txtAdd1.Text).Replace("'", "`")
            obj.add2 = clsCommon.myCstr(txtAdd2.Text).Replace("'", "`")
            obj.add3 = clsCommon.myCstr(txtAdd3.Text).Replace("'", "`")
            obj.country_code = clsCommon.myCstr(fndCountry.Value)
            obj.state_code = clsCommon.myCstr(fndstate.Value)
            obj.city_code = clsCommon.myCstr(fndCity.Value)
            obj.trans_type = clsCommon.myCstr(CmbTransaction.SelectedValue)
            obj.cust_code = clsCommon.myCstr(txtcust_code.Text)

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsEnquiryMaster.SaveData(isNewEntry, obj, trans) Then
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If

                txtCode.Value = obj.code
                btnsave.Text = "Update"
                btndelete.Enabled = True

                UcAttachment1.SaveData(txtCode.Value)

                LoadData(txtCode.Value, NavigatorType.Current)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmEnquiryMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmEnquiryMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadComboBox()
        FunReset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+C for close window.")
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                ErrorControl.SetError(txtCode, "Select document code for deletion.")
                Throw New Exception("Select document code for deletion.")
            Else
                ErrorControl.ResetError(txtCode)
            End If

            If Not clsCommon.MyMessageBoxShow("Are you sure want to delete enquiry code " + txtCode.Value + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Return
            End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsEnquiryMaster.DeleteData(txtCode.Value, trans) Then
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub fndCountry__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCountry._MYValidating
        Try
            fndCountry.Value = clsCommon.myCstr(clsCountryMaster.getFinder("", fndCountry.Value, isButtonClicked))

            If clsCommon.myLen(fndCountry.Value) > 0 Then
                txtcountryName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + fndCountry.Value + "'"))
            Else
                fndCountry.Value = ""
                txtcountryName.Text = ""
                fndstate.Value = ""
                txtstateName.Text = ""
                fndCity.Value = ""
                txtcityName.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndstate__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndstate._MYValidating
        Try
            If clsCommon.myLen(fndCountry.Value) <= 0 Then
                fndCountry.Focus()
                fndCountry.Select()
                ErrorControl.SetError(txtcountryName, "Select Country First.")
                Throw New Exception("Select Country First.")
            Else
                ErrorControl.ResetError(txtcountryName)
            End If

            fndstate.Value = clsCommon.myCstr(clsStateMaster.getFinder(" tspl_state_master.country_code='" + fndCountry.Value + "'", fndstate.Value, isButtonClicked))

            If clsCommon.myLen(fndstate.Value) > 0 Then
                txtstateName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + fndstate.Value + "' and country_code='" + fndCountry.Value + "'"))
            Else
                fndstate.Value = ""
                txtstateName.Text = ""
                fndCity.Value = ""
                txtcityName.Text = ""
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCity._MYValidating
        Try
            If clsCommon.myLen(fndCountry.Value) <= 0 Then
                fndCountry.Focus()
                fndCountry.Select()
                ErrorControl.SetError(txtcountryName, "Select Country First.")
                Throw New Exception("Select Country First.")
            Else
                ErrorControl.ResetError(txtcountryName)
            End If

            If clsCommon.myLen(fndstate.Value) <= 0 Then
                fndstate.Focus()
                fndstate.Select()
                ErrorControl.SetError(txtstateName, "Select State First.")
                Throw New Exception("Select State First.")
            Else
                ErrorControl.ResetError(txtstateName)
            End If

            fndCity.Value = clsCommon.myCstr(clsCityMaster.getFinder(" tspl_city_master.state_code='" + fndstate.Value + "'", fndCity.Value, isButtonClicked))

            If clsCommon.myLen(fndCity.Value) > 0 Then
                txtcityName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select city_name from tspl_city_master where city_code='" + fndCity.Value + "'"))
            Else
                fndCity.Value = ""
                txtcityName.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsEnquiryMaster = clsEnquiryMaster.GetData(strCode, NavType)

            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.code) > 0 Then
                isNewEntry = False
                txtCode.Value = obj.code
                dtpDate.Text = obj.docdate
                txtCustomerName.Text = obj.descrptn
                txtAdd1.Text = obj.add1
                txtAdd2.Text = obj.add2
                txtAdd3.Text = obj.add3
                fndCountry.Value = obj.country_code
                txtcountryName.Text = obj.country_name
                fndstate.Value = obj.state_code
                txtstateName.Text = obj.state_name
                fndCity.Value = obj.city_code
                txtcityName.Text = obj.city_name
                CmbTransaction.SelectedValue = obj.trans_type
                txtcust_code.Text = obj.cust_code
                txtcust_name.Text = obj.cust_name

                txtCode.MyReadOnly = True
                btnsave.Text = "Update"
                btndelete.Enabled = True

                UcAttachment1.LoadData(txtCode.Value)
            Else
                FunReset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.myCstr(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select count(*) from tspl_enquiry_master where code='" + clsCommon.myCstr(txtCode.Value) + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsEnquiryMaster.GetFinder("", txtCode.Value, isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                FunReset()
            End If
        End If
    End Sub

    Private Sub btncreate_cust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncreate_cust.Click
        Try
            Dim qry As String = "create table Cust_info (cust_code varchar(30) NULL,cust_name varchar(200) NULL)"
            clsDBFuncationality.ExecuteNonQuery(qry)

            Dim frm As frmCustomer = New frmCustomer(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
            frm.fndCustomer.Value = clsCommon.myCstr(txtcust_code.Text)
            If clsCommon.myLen(txtcust_name.Text) > 0 Then
                frm.txtCustomerName.Text = clsCommon.myCstr(txtcust_name.Text)
            Else
                frm.txtCustomerName.Text = clsCommon.myCstr(txtCustomerName.Text)
            End If
            frm.txtAdd1.Text = clsCommon.myCstr(txtAdd1.Text)
            frm.txtAdd2.Text = clsCommon.myCstr(txtAdd2.Text)
            frm.txtAdd3.Text = clsCommon.myCstr(txtAdd3.Text)
            frm.fndCountry.Value = clsCommon.myCstr(fndCountry.Value)
            frm.TxtCountryName.Text = clsCommon.myCstr(txtcountryName.Text)
            frm.fndstate.Value = clsCommon.myCstr(fndstate.Value)
            frm.txtStateName.Text = clsCommon.myCstr(txtstateName.Text)
            frm.fndCity.Value = clsCommon.myCstr(fndCity.Value)
            frm.txtCity.Text = clsCommon.myCstr(txtcityName.Text)
            frm.DrillDown_FormName = clsUserMgtCode.frmEnquiryMaster
            frm.DrillDown_transType = clsCommon.myCstr(CmbTransaction.SelectedValue)
            frm.ShowDialog()

            txtcust_code.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select cust_code from cust_info"))
            txtcust_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select cust_name from cust_info"))

            qry = "drop table Cust_info"
            clsDBFuncationality.ExecuteNonQuery(qry)

            If AllowToSave() Then SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class
