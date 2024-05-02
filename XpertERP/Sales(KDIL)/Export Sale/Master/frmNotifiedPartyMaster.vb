'================BM00000003442
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports System.Text.RegularExpressions

Public Class FrmNotifiedPartyMaster
    Inherits FrmMainTranScreen

#Region "variables"
    Dim ButtonToolTip As New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim Errorcontrol As New clsErrorControl()
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmNotifiedPartyMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FunReset()
        txtMain_Cust_Code.Value = ""
        txtMain_Cust_Name.Text = ""
        txtMain_Cust_Code.Enabled = True
        txtcode.Value = ""
        txtname.Text = ""
        fndCustomer.Text = ""
        txtCustomerName.Text = ""
        txtShipToLocation.Text = ""
        txtShipToLocationDesc.Text = ""
        fndLocation.Value = ""
        lblLocationDesc.Text = ""
        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtAddress3.Text = ""
        txtcountrycode.Value = ""
        txtcountryname.Text = ""
        txtstatecode.Value = ""
        txtstatename.Text = ""
        txtcitycode.Value = ""
        txtcityname.Text = ""
        txtPostalCode.Text = ""
        txtTinNo.Text = ""
        txtCSTNo.Text = ""
        txtEmail.Text = ""
        txtTelephone.Text = ""
        fndCustomer.Value = ""
        fndCustomer.Enabled = True
        cbgLocation.UnCheckedAll()
        MasterTemplate.DataSource = Nothing
        MasterTemplate.Rows.Clear()

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        isNewEntry = True
        txtcode.MyReadOnly = False
        btnsave.Enabled = True
        btnsave.Text = "Save"
        btnDelete.Enabled = False
        txtstatus.Text = "New"

        RadPageView1.SelectedPage = Details

        txtname.Focus()
        txtname.Select()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub txtcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtcode._MYNavigator
        LoadData(clsCommon.myCstr(txtcode.Value), NavType)
    End Sub

    Private Sub txtcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcode._MYValidating
        Dim qry As String = "select count(*) from TSPL_NOTIFY_PARTY_HEAD where doc_no='" + clsCommon.myCstr(txtcode.Value) + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            txtcode.MyReadOnly = True
        Else
            txtcode.MyReadOnly = False
        End If

        If txtcode.MyReadOnly OrElse isButtonClicked Then
            txtcode.Value = clsNotifiedPartyMaster.GetFinder("", txtcode.Value, isButtonClicked)

            If clsCommon.myLen(txtcode.Value) > 0 Then
                LoadData(txtcode.Value, NavigatorType.Current)
            Else
                FunReset()
            End If
        End If
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsNotifiedPartyMaster = clsNotifiedPartyMaster.GetData(strCode, NavType)

            MasterTemplate.Rows.Clear()

            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.docno) > 0 Then
                isNewEntry = False
                txtcode.Value = obj.docno
                txtname.Text = obj.descrptn
                txtMain_Cust_Code.Value = obj.cust_code
                txtMain_Cust_Name.Text = obj.cust_name

                If obj.ArrMain IsNot Nothing AndAlso obj.ArrMain.Count > 0 Then
                    For Each objtr As clsNotifiedPartyMaster In obj.ArrMain
                        MasterTemplate.Rows.AddNew()

                        fndCustomer.Value = objtr.cust_code
                        txtCustomerName.Text = objtr.cust_name
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(0).Value = clsCommon.myCstr(objtr.cust_code)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(1).Value = clsCommon.myCstr(objtr.cust_name)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(2).Value = clsCommon.myCstr(objtr.ship_code)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(3).Value = clsCommon.myCstr(objtr.ship_name)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(4).Value = clsCommon.myCstr(objtr.add1)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(5).Value = clsCommon.myCstr(objtr.add2)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(6).Value = clsCommon.myCstr(objtr.add3)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(7).Value = clsCommon.myCstr(objtr.citycode)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(8).Value = clsCommon.myCstr(objtr.cityname)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(9).Value = clsCommon.myCstr(objtr.statecode)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(10).Value = clsCommon.myCstr(objtr.statename)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(11).Value = clsCommon.myCstr(objtr.postalcode)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(12).Value = clsCommon.myCstr(objtr.countrycode)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(13).Value = clsCommon.myCstr(objtr.countryname)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(14).Value = clsCommon.myCstr(objtr.tel_no)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(15).Value = clsCommon.myCstr(objtr.email)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(16).Value = clsCommon.myCstr(objtr.tin_no)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(17).Value = clsCommon.myCstr(objtr.cst_no)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Cells(18).Value = clsCommon.myCstr(objtr.Loc_Code)
                        MasterTemplate.Rows(MasterTemplate.Rows.Count - 1).Tag = objtr.GridArr
                        '======================
                        txtShipToLocation.Text = objtr.ship_code
                        txtShipToLocationDesc.Text = objtr.ship_name
                        fndLocation.Value = objtr.Loc_Code
                        lblLocationDesc.Text = objtr.Loc_Desc
                        txtAddress1.Text = objtr.add1
                        txtAddress2.Text = objtr.add2
                        txtAddress3.Text = objtr.add3
                        txtcountrycode.Value = objtr.countrycode
                        txtcountryname.Text = objtr.countryname
                        txtstatecode.Value = objtr.statecode
                        txtstatename.Text = objtr.statename
                        txtcitycode.Value = objtr.citycode
                        txtcityname.Text = objtr.cityname
                        txtPostalCode.Text = objtr.postalcode
                        txtTinNo.Text = objtr.tin_no
                        txtCSTNo.Text = objtr.cst_no
                        txtEmail.Text = objtr.email
                        txtTelephone.Text = objtr.tel_no

                        txtstatus.Text = "Old"
                    Next
                End If

                Dim arrList As New ArrayList()

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsNotifiedPartyMasterDetail In obj.Arr
                        arrList.Add(clsCommon.myCstr(objtr.loc_code))
                    Next
                End If
                cbgLocation.UnCheckedAll()
                If arrList IsNot Nothing AndAlso arrList.Count > 0 Then
                    cbgLocation.CheckedValue = arrList
                End If

                UcAttachment1.LoadData(txtcode.Value)
                txtcode.MyReadOnly = True
                btnsave.Text = "Update"
                btnDelete.Enabled = True

                fndCustomer.Enabled = False
                txtMain_Cust_Code.Enabled = False
            Else
                FunReset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLocation._MYValidating
        fndLocation.Value = clsCommon.myCstr(clsLocation.getFinder("Location_Type <>'Logical'", fndLocation.Value, isButtonClicked))

        If clsCommon.myLen(fndLocation.Value) > 0 Then
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from TSPL_LOCATION_MASTER where Location_Code='" & clsCommon.myCstr(fndLocation.Value) & "'"))
        Else
            lblLocationDesc.Text = ""
        End If

        If clsCommon.myLen(txtShipToLocation.Text) <= 0 Then
            txtShipToLocation.Text = fndLocation.Value
        End If
        If clsCommon.myLen(txtShipToLocationDesc.Text) <= 0 Then
            txtShipToLocationDesc.Text = lblLocationDesc.Text
        End If

    End Sub

    Private Sub txtcountrycode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcountrycode._MYValidating
        Try
            txtcountrycode.Value = clsCommon.myCstr(clsCountryMaster.getFinder("", txtcountrycode.Value, isButtonClicked))

            If clsCommon.myLen(txtcountrycode.Value) > 0 Then
                txtcountryname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + txtcountrycode.Value + "'"))
            Else
                txtcountrycode.Value = ""
                txtcountryname.Text = ""
                txtstatecode.Value = ""
                txtstatename.Text = ""
                txtcitycode.Value = ""
                txtcityname.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtstatecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtstatecode._MYValidating
        Try
            If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
                txtcountrycode.Focus()
                txtcountrycode.Select()
                ErrorControl.SetError(txtcountryname, "Select Country First.")
                Throw New Exception("Select Country First.")
            Else
                ErrorControl.ResetError(txtcountryname)
            End If

            txtstatecode.Value = clsCommon.myCstr(clsStateMaster.getFinder(" COUNTRY_CODE='" + txtcountrycode.Value + "'", txtstatecode.Value, isButtonClicked))

            If clsCommon.myLen(txtstatecode.Value) > 0 Then
                txtstatename.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where state_code='" + txtstatecode.Value + "' and country_code='" + txtcountrycode.Value + "'"))
            Else
                txtstatecode.Value = ""
                txtstatename.Text = ""
                txtcitycode.Value = ""
                txtcityname.Text = ""
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcitycode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcitycode._MYValidating
        Try
            If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
                txtcountrycode.Focus()
                txtcountrycode.Select()
                Errorcontrol.SetError(txtcountryname, "Select Country First.")
                Throw New Exception("Select Country First.")
            Else
                ErrorControl.ResetError(txtcountryname)
            End If

            If clsCommon.myLen(txtstatecode.Value) <= 0 Then
                txtstatecode.Focus()
                txtstatecode.Select()
                ErrorControl.SetError(txtstatename, "Select State First.")
                Throw New Exception("Select State First.")
            Else
                ErrorControl.ResetError(txtstatename)
            End If

            txtcitycode.Value = clsCommon.myCstr(clsCityMaster.getFinder(" tspl_city_master.state_code='" + txtstatecode.Value + "'", txtcitycode.Value, isButtonClicked))

            If clsCommon.myLen(txtcitycode.Value) > 0 Then
                txtcityname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select city_name from tspl_city_master where city_code='" + txtcitycode.Value + "'"))
            Else
                txtcitycode.Value = ""
                txtcityname.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtMain_Cust_Code.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Notify Party first.")
                txtMain_Cust_Code.Focus()
                txtMain_Cust_Code.Select()
                Errorcontrol.SetError(txtMain_Cust_Name, "Select Notify Party first.")
                Return False
            Else
                Errorcontrol.ResetError(txtMain_Cust_Name)
            End If

            If clsCommon.myLen(fndCustomer.Value) <= 0 Then
                fndCustomer.Focus()
                fndCustomer.Select()
                Errorcontrol.SetError(txtCustomerName, "Select customer detail.")
                Throw New Exception("Select customer detail.")
            Else
                Errorcontrol.ResetError(txtCustomerName)
            End If

            If clsCommon.myLen(txtShipToLocation.Text) <= 0 Then
                txtShipToLocation.Focus()
                txtShipToLocation.Select()
                Errorcontrol.SetError(txtShipToLocationDesc, "Fill ship to location detail.")
                Throw New Exception("Fill ship to location detail.")
            Else
                Errorcontrol.ResetError(txtShipToLocationDesc)
            End If

            Dim qry As String = "select count(*) from TSPL_NOTIFY_PARTY_SHIP_DETAIL where doc_no='" + txtcode.Value + "' and Ship_To_Location_Code='" + txtShipToLocation.Text + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 1 Then
                txtShipToLocation.Focus()
                txtShipToLocation.Select()
                Errorcontrol.SetError(txtShipToLocationDesc, "Filled ship to location detail is already exist.")
                Throw New Exception("Filled ship to location detail is already exist.")
            Else
                Errorcontrol.ResetError(txtShipToLocationDesc)
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub SaveData()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmNotifiedPartyMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            Dim obj As New clsNotifiedPartyMaster()
            obj.Arr = New List(Of clsNotifiedPartyMasterDetail)
            obj.ArrMain = New List(Of clsNotifiedPartyMaster)

            obj.docno = clsCommon.myCstr(txtcode.Value)
            obj.descrptn = clsCommon.myCstr(txtname.Text).Replace("'", "`")
            obj.cust_code = clsCommon.myCstr(txtMain_Cust_Code.Value)
            obj.cust_name = clsCommon.myCstr(txtMain_Cust_Name.Text)

            Dim objtr As New clsNotifiedPartyMaster()
            For Each grow As GridViewRowInfo In MasterTemplate.Rows
                objtr = New clsNotifiedPartyMaster()

                objtr.cust_code = clsCommon.myCstr(grow.Cells(0).Value)
                objtr.cust_name = clsCommon.myCstr(grow.Cells(1).Value)
                objtr.ship_code = clsCommon.myCstr(grow.Cells(2).Value).Replace("'", "`")
                objtr.ship_name = clsCommon.myCstr(grow.Cells(3).Value).Replace("'", "`")
                objtr.Loc_Code = clsCommon.myCstr(grow.Cells(18).Value)
                objtr.add1 = clsCommon.myCstr(grow.Cells(4).Value).Replace("'", "`")
                objtr.add2 = clsCommon.myCstr(grow.Cells(5).Value).Replace("'", "`")
                objtr.add3 = clsCommon.myCstr(grow.Cells(6).Value).Replace("'", "`")
                objtr.countrycode = clsCommon.myCstr(grow.Cells(12).Value)
                objtr.statecode = clsCommon.myCstr(grow.Cells(9).Value)
                objtr.citycode = clsCommon.myCstr(grow.Cells(7).Value)
                objtr.postalcode = clsCommon.myCstr(grow.Cells(11).Value)
                objtr.tin_no = clsCommon.myCstr(grow.Cells(16).Value)
                objtr.cst_no = clsCommon.myCstr(grow.Cells(17).Value)
                objtr.email = clsCommon.myCstr(grow.Cells(15).Value)
                objtr.tel_no = clsCommon.myCstr(grow.Cells(14).Value)
                objtr.GridArr = TryCast(grow.Tag, List(Of clsNotifiedPartyMasterDetail))

                If clsCommon.CompairString(objtr.cust_code, fndCustomer.Value) <> CompairStringResult.Equal Then
                    obj.ArrMain.Add(objtr)
                End If

            Next

            'If clsCommon.CompairString(txtstatus.Text, "New") = CompairStringResult.Equal Then
            objtr = New clsNotifiedPartyMaster()

            objtr.cust_code = clsCommon.myCstr(fndCustomer.Value)
            objtr.ship_code = clsCommon.myCstr(txtShipToLocation.Text).Replace("'", "`")
            objtr.ship_name = clsCommon.myCstr(txtShipToLocationDesc.Text).Replace("'", "`")
            objtr.Loc_Code = clsCommon.myCstr(fndLocation.Value)
            objtr.add1 = clsCommon.myCstr(txtAddress1.Text).Replace("'", "`")
            objtr.add2 = clsCommon.myCstr(txtAddress2.Text).Replace("'", "`")
            objtr.add3 = clsCommon.myCstr(txtAddress3.Text).Replace("'", "`")
            objtr.countrycode = clsCommon.myCstr(txtcountrycode.Value)
            objtr.statecode = clsCommon.myCstr(txtstatecode.Value)
            objtr.citycode = clsCommon.myCstr(txtcitycode.Value)
            objtr.postalcode = clsCommon.myCstr(txtPostalCode.Text)
            objtr.tin_no = clsCommon.myCstr(txtTinNo.Text)
            objtr.cst_no = clsCommon.myCstr(txtCSTNo.Text)
            objtr.email = clsCommon.myCstr(txtEmail.Text)
            objtr.tel_no = clsCommon.myCstr(txtTelephone.Text)

            obj.ArrMain.Add(objtr)
            'End If

            Dim objtr1 As New clsNotifiedPartyMasterDetail()

            Dim arrlist As ArrayList = cbgLocation.CheckedValue
            If arrlist IsNot Nothing AndAlso arrlist.Count > 0 Then
                For Each objloc As String In arrlist
                    objtr1 = New clsNotifiedPartyMasterDetail()

                    objtr1.ship_to_code = clsCommon.myCstr(txtShipToLocation.Text)
                    objtr1.loc_code = clsCommon.myCstr(objloc)
                    objtr1.custcode = clsCommon.myCstr(fndCustomer.Value)

                    obj.Arr.Add(objtr1)
                Next
            End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsNotifiedPartyMaster.SaveData(obj, isNewEntry, trans) Then
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If

                txtcode.Value = obj.docno
                UcAttachment1.SaveData(txtcode.Value)

                LoadData(txtcode.Value, NavigatorType.Current)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(txtcode.Value) <= 0 Then
                txtcode.Focus()
                txtcode.Select()
                Errorcontrol.SetError(txtcode, "Select notify party no. first for deletion")
                Throw New Exception("Select notify party no. first for deletion")
            Else
                Errorcontrol.ResetError(txtcode)
            End If

            If Not clsCommon.MyMessageBoxShow(Me, "Are you sure,want to delete notify party no. " + txtcode.Value + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Return
            End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsNotifiedPartyMaster.DeleteData(txtcode.Value, fndCustomer.Value, trans) Then
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub LoadLocation()
        Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location_Code"
        cbgLocation.DisplayMember = "Location_Desc"
    End Sub

    Private Sub FrmNotifiedPartyMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso Me.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso Me.isDeleteFlag AndAlso btnDelete.Enabled Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmNotifiedPartyMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadLocation()
        FunReset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N for refresh window.")

        txtcountrycode.Enabled = False
        txtstatecode.Enabled = False
        txtcitycode.Enabled = False
    End Sub

    Private Sub fndCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer._MYValidating
        If clsCommon.myLen(txtMain_Cust_Code.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select Notify Party first.", Me.Text)
            txtMain_Cust_Code.Focus()
            txtMain_Cust_Code.Select()
            Errorcontrol.SetError(txtMain_Cust_Name, "Select Notify Party first.")
            Return
        Else
            Errorcontrol.ResetError(txtMain_Cust_Name)
        End If

        fndCustomer.Value = clsCommon.myCstr(clsCustomerMaster.getFinder(" ", fndCustomer.Value, isButtonClicked))
        txtCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + fndCustomer.Value + "'"))
        txtAddress1.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select add1 from tspl_customer_master where cust_code='" + fndCustomer.Value + "'"))
        txtAddress2.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select add2 from tspl_customer_master where cust_code='" + fndCustomer.Value + "'"))
        txtAddress3.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select add3 from tspl_customer_master where cust_code='" + fndCustomer.Value + "'"))
        txtcitycode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select city_code from tspl_customer_master where cust_code='" + fndCustomer.Value + "'"))
        txtcityname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select city_name from tspl_city_master where city_code='" + txtcitycode.Value + "'"))
        txtstatecode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state from tspl_customer_master where cust_code='" + fndCustomer.Value + "'"))
        txtstatename.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from TSPL_STATE_MASTER where state_code='" + txtstatecode.Value + "'"))
        txtcountrycode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country from tspl_customer_master where cust_code='" + fndCustomer.Value + "'"))
        txtcountryname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from TSPL_COUNTRY_MASTER where country_code='" + txtcountrycode.Value + "'"))
        txtEmail.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select email from tspl_customer_master where cust_code='" + fndCustomer.Value + "'"))
        txtTinNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tin_no from tspl_customer_master where cust_code='" + fndCustomer.Value + "'"))
        txtCSTNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select cst from tspl_customer_master where cust_code='" + fndCustomer.Value + "'"))
        txtTelephone.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select phone1 from tspl_customer_master where cust_code='" + fndCustomer.Value + "'"))
    End Sub

    Private Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
        
    End Sub

    Private Sub MasterTemplate_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles MasterTemplate.CellDoubleClick
        Try
            If e.Column Is MasterTemplate.Columns(0) Then
                fndCustomer.Value = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(0).Value)
                txtCustomerName.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(1).Value)
                txtShipToLocation.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(2).Value)
                txtShipToLocationDesc.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(3).Value)
                fndLocation.Value = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(18).Value)
                lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + fndLocation.Value + "'"))
                txtAddress1.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(4).Value)
                txtAddress2.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(5).Value)
                txtAddress3.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(6).Value)
                txtcountrycode.Value = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(12).Value)
                txtcountryname.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(13).Value)
                txtstatecode.Value = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(9).Value)
                txtstatename.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(10).Value)
                txtcitycode.Value = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(7).Value)
                txtcityname.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(8).Value)
                txtPostalCode.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(11).Value)
                txtTinNo.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(16).Value)
                txtCSTNo.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(17).Value)
                txtEmail.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(15).Value)
                txtTelephone.Text = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(14).Value)

                txtstatus.Text = "Old"

                LoadComboLocation()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadComboLocation()
        Try
            Dim arrList As New ArrayList()

            Dim obj As New clsNotifiedPartyMaster()
            obj.Arr = New List(Of clsNotifiedPartyMasterDetail)
            obj.Arr = clsNotifiedPartyMaster.GetComboLocations(txtcode.Value, fndCustomer.Value, txtShipToLocation.Text)

            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objtr As clsNotifiedPartyMasterDetail In obj.Arr
                    arrList.Add(clsCommon.myCstr(objtr.loc_code))
                Next
            End If

            cbgLocation.UnCheckedAll()
            If arrList IsNot Nothing AndAlso arrList.Count > 0 Then
                cbgLocation.CheckedValue = arrList
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Try
            txtstatus.Text = "New"
            fndCustomer.Value = ""
            txtCustomerName.Text = ""
            fndCustomer.Enabled = True
            txtShipToLocation.Text = ""
            txtShipToLocationDesc.Text = ""
            fndLocation.Value = ""
            lblLocationDesc.Text = ""
            txtAddress1.Text = ""
            txtAddress2.Text = ""
            txtAddress3.Text = ""
            txtcountrycode.Value = ""
            txtcountryname.Text = ""
            txtstatecode.Value = ""
            txtstatename.Text = ""
            txtcitycode.Value = ""
            txtcityname.Text = ""
            txtPostalCode.Text = ""
            txtTinNo.Text = ""
            txtCSTNo.Text = ""
            txtEmail.Text = ""
            txtTelephone.Text = ""
            cbgLocation.UnCheckedAll()

            txtname.Focus()
            txtname.Select()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtEmail_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtEmail.Validating
        Try
            Dim check As Match = Regex.Match(txtEmail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")

            If check.Success = False And clsCommon.myLen(txtEmail.Text) > 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please insert the proper format of e-mail address", Me.Text)
                txtEmail.Text = ""
                txtEmail.Focus()
                txtEmail.Select()
                Errorcontrol.SetError(txtEmail, "Please insert the proper format of e-mail address")
            Else
                Errorcontrol.ResetError(txtEmail)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        
    End Sub

    Private Sub txtMain_Cust_Code__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtMain_Cust_Code._MYValidating
        txtMain_Cust_Code.Value = clsCommon.myCstr(clsCustomerMaster.getFinder("", txtMain_Cust_Code.Value, isButtonClicked))

        If clsCommon.myLen(txtMain_Cust_Code.Value) > 0 Then
            Dim qry As String = "select count(*) from TSPL_NOTIFY_PARTY_HEAD where Cust_Code='" + txtMain_Cust_Code.Value + "' and doc_no<>'" + clsCommon.myCstr(txtcode.Value) + "'"
            Dim count As Integer = clsDBFuncationality.getSingleValue(qry)

            If count > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Selected Party is already used," + Environment.NewLine + "for more changes edit previous record.")
                txtMain_Cust_Code.Value = ""
                txtMain_Cust_Name.Text = ""
                Return
            End If

            txtMain_Cust_Name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + txtMain_Cust_Code.Value + "'"))
        Else
            txtMain_Cust_Name.Text = ""
        End If
    End Sub
End Class
