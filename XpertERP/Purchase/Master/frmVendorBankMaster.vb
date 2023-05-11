''richa agarwal Against ticket no BM00000005874,VendorBankMaster,BM00000006369
Imports common
Imports System.Data.SqlClient
Public Class FrmVendorBankMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As New ToolTip()
    Dim ErrorControl As New clsErrorControl()
    Dim isNewEntry As Boolean = True
    Public Const colBankIFSCCode As String = "colBankIFSCCode"
    Public Const colBankBranch As String = "colBankBranch"
    Public Const colBankSwiftCode As String = "colBankSwiftCode"
    Public BankCode As String = String.Empty
    Public BAnkCodeValue As String = ""
#End Region

    Private Sub FunReset()
        isNewEntry = True
        fndBankCode.Value = ""
        txtBankName.Text = ""
        txtBranchCode.Text = ""
        txtBranchName.Text = ""
        txtIFSCCode.Text = ""
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtAdd3.Text = ""
        fndCountry.Value = ""
        txtcountryName.Text = ""
        fndstate.Value = ""
        txtstateName.Text = ""
        fndCity.Value = ""
        txtcityName.Text = ""
        ''richa agarwal 24/03/2014
        loadBlankItemGrid()
        ''------------------

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        fndBankCode.MyReadOnly = False
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnsave.Text = "Save"

        txtBankName.Focus()
        txtBankName.Select()

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.VendorBankMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
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
            If clsCommon.myLen(txtBankName.Text) <= 0 Then
                txtBankName.Focus()
                txtBankName.Select()
                ErrorControl.SetError(txtBankName, "Fill Bank Name")
                Throw New Exception("Fill Bank Name")
            Else
                ErrorControl.ResetError(txtBankName)
            End If

            If clsCommon.myLen(txtBankName.Text) <= 0 Then
                txtBankName.Focus()
                txtBankName.Select()
                ErrorControl.SetError(txtBankName, "Fill Bank Name")
                Throw New Exception("Fill Bank Name")
            Else
                ErrorControl.ResetError(txtBankName)
            End If

            'If clsCommon.myLen(txtBranchCode.Text) <= 0 Then
            '    txtBranchCode.Focus()
            '    txtBranchCode.Select()
            '    ErrorControl.SetError(txtBranchCode, "Fill Branch Code")
            '    Throw New Exception("Fill Branch Code")
            'Else
            '    ErrorControl.ResetError(txtBranchCode)
            'End If

            'If clsCommon.myLen(txtBranchName.Text) <= 0 Then
            '    txtBranchName.Focus()
            '    txtBranchName.Select()
            '    ErrorControl.SetError(txtBranchName, "Fill Branch Name")
            '    Throw New Exception("Fill Branch Name")
            'Else
            '    ErrorControl.ResetError(txtBranchName)
            'End If

            'If clsCommon.myLen(txtIFSCCode.Text) <= 0 Then
            '    txtIFSCCode.Focus()
            '    txtIFSCCode.Select()
            '    ErrorControl.SetError(txtIFSCCode, "Fill IFSC Code")
            '    Throw New Exception("Fill IFSC Code")
            'Else
            '    ErrorControl.ResetError(txtIFSCCode)
            'End If
            ''richa agarwal 24/03/2015
            Dim count As Integer = 0 ''IFSC Code CAN Be Same 
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strOuterIFSCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colBankIFSCCode).Value)
                Dim strOuterSwiftCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colBankSwiftCode).Value)
                Dim strOuterBranchCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colBankBranch).Value)

                If clsCommon.myLen(strOuterIFSCode) <= 0 Then
                    Dim Msg As String = "Fill IFSC Code at Row No " + clsCommon.myCstr(ii + 1)
                    common.clsCommon.MyMessageBoxShow(Msg)
                    Return False
                End If
                If clsCommon.myLen(strOuterBranchCode) <= 0 Then
                    Dim Msg As String = "Fill Branch Name at Row No " + clsCommon.myCstr(ii + 1)
                    common.clsCommon.MyMessageBoxShow(Msg)
                    Return False
                End If

                If clsCommon.myLen(strOuterIFSCode) > 0 Then
                    count = count + 1
                End If
                'For jj As Integer = 0 To gv1.Rows.Count - 1
                '    If jj = ii Then
                '        Continue For
                '    End If
                '    Dim strInnerIFScCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colBankIFSCCode).Value)
                '    Dim strInnerSwiftCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colBankSwiftCode).Value)

                '    If clsCommon.CompairString(strOuterIFSCode, strInnerIFScCode) = CompairStringResult.Equal Then
                '        Dim Msg As String = "Same IFSC Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                '        common.clsCommon.MyMessageBoxShow(Msg)
                '        Return False
                '    End If

                '    If clsCommon.CompairString(strOuterSwiftCode, strInnerSwiftCode) = CompairStringResult.Equal Then
                '        Dim Msg As String = "Same Swift Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                '        common.clsCommon.MyMessageBoxShow(Msg)
                '        Return False
                '    End If
                'Next
            Next
            If count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please fill atleast 1 IFSC code of Branch")
                Return False
            End If
            ''---------------------
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Sub SaveData()
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.VendorBankMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        Dim trans As SqlTransaction = Nothing
        Try
            Dim obj As New clsVendorBankMaster()
            Dim objTr As New clsVendorBankBranchDetail()
            obj.Bank_code = clsCommon.myCstr(fndBankCode.Value)
            obj.Bank_Name = clsCommon.myCstr(txtBankName.Text).Replace("'", "`")
            ''richa agarwal 25/03/2015
            'obj.Branch_Code = clsCommon.myCstr(txtBranchCode.Text).Replace("'", "`")
            'obj.Branch_Name = clsCommon.myCstr(txtBranchName.Text).Replace("'", "`")
            'obj.IFSC_Code = clsCommon.myCstr(txtIFSCCode.Text).Replace("'", "`")
            ''-------------------
            obj.add1 = clsCommon.myCstr(txtAdd1.Text).Replace("'", "`")
            obj.add2 = clsCommon.myCstr(txtAdd2.Text).Replace("'", "`")
            obj.add3 = clsCommon.myCstr(txtAdd3.Text).Replace("'", "`")
            obj.country_code = clsCommon.myCstr(fndCountry.Value)
            obj.state_code = clsCommon.myCstr(fndstate.Value)
            obj.city_code = clsCommon.myCstr(fndCity.Value)
            ''richa agarwal 24/03/2015
            obj.arrVendorBranchDetail = New List(Of clsVendorBankBranchDetail)

            For Each grow As GridViewRowInfo In gv1.Rows
                objTr = New clsVendorBankBranchDetail()
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colBankIFSCCode).Value)) > 0 Then
                    objTr.Bank_Code = clsCommon.myCstr(obj.Bank_code)
                    objTr.Bank_IFSC_Code = clsCommon.myCstr(grow.Cells(colBankIFSCCode).Value)
                    objTr.Branch_Name = clsCommon.myCstr(grow.Cells(colBankBranch).Value)
                    objTr.Bank_Swift_Code = clsCommon.myCstr(grow.Cells(colBankSwiftCode).Value)
                    obj.arrVendorBranchDetail.Add(objTr)
                End If
            Next

            ''---------------------------
            trans = clsDBFuncationality.GetTransactin()
            If clsVendorBankMaster.SaveData(isNewEntry, obj, trans) Then
                trans.Commit()
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                End If
                fndBankCode.Value = obj.Bank_code
                btnsave.Text = "Update"
                btndelete.Enabled = True
                UcAttachment1.SaveData(fndBankCode.Value)
                LoadData(fndBankCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            Try
                trans.Rollback()
            Catch xxx As Exception
            End Try
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmVendorBankMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmVendorBankMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        FunReset()
        If clsCommon.myLen(BankCode) > 0 Then
            LoadData(BankCode, NavigatorType.Current)
        End If
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+C for close window.")
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Dim trans As SqlTransaction = Nothing
        Try
            If clsCommon.myLen(fndBankCode.Value) <= 0 Then
                fndBankCode.Focus()
                fndBankCode.Select()
                ErrorControl.SetError(fndBankCode, "Select Bank Code for deletion.")
                Throw New Exception("Select Bank Code for deletion.")
            Else
                ErrorControl.ResetError(fndBankCode)
            End If

            If Not clsCommon.MyMessageBoxShow("Are you sure want to delete Bank code " + fndBankCode.Value + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Return
            End If

            trans = clsDBFuncationality.GetTransactin()
            If clsVendorBankMaster.DeleteData(fndBankCode.Value, trans) Then
                trans.Commit()
                clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
                FunReset()
            End If
        Catch ex As Exception
            Try
                trans.Rollback()
            Catch xxx As Exception
            End Try
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
        BAnkCodeValue = fndBankCode.Value
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
            clsCommon.MyMessageBoxShow(ex.Message)
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
            clsCommon.MyMessageBoxShow(ex.Message)
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
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsVendorBankMaster = clsVendorBankMaster.GetData(strCode, NavType)

            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Bank_code) > 0 Then
                isNewEntry = False
                fndBankCode.Value = obj.Bank_code
                txtBankName.Text = obj.Bank_Name
                ''richa agarwal 25/03/2015
                'txtBranchCode.Text = obj.Branch_Code
                'txtBranchName.Text = obj.Branch_Name
                'txtIFSCCode.Text = obj.IFSC_Code
                ''-----------
                txtAdd1.Text = obj.add1
                txtAdd2.Text = obj.add2
                txtAdd3.Text = obj.add3
                fndCountry.Value = obj.country_code
                txtcountryName.Text = obj.country_name
                fndstate.Value = obj.state_code
                txtstateName.Text = obj.state_name
                fndCity.Value = obj.city_code
                txtcityName.Text = obj.city_name
                ''richa agarwal 24/0/2015
                loadBlankItemGrid()
                If obj.arrVendorBranchDetail IsNot Nothing AndAlso obj.arrVendorBranchDetail.Count > 0 Then
                    For Each objTr As clsVendorBankBranchDetail In obj.arrVendorBranchDetail
                        ' gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBankBranch).Value = objTr.Branch_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBankIFSCCode).Value = objTr.Bank_IFSC_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBankSwiftCode).Value = objTr.Bank_Swift_Code
                        gv1.Rows.AddNew()
                    Next
                    gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                End If

                ''----------------------------------
                fndBankCode.MyReadOnly = True
                btnsave.Text = "Update"
                btndelete.Enabled = True

                UcAttachment1.LoadData(fndBankCode.Value)
            Else
                FunReset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.myCstr(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndBankCode._MYNavigator
        LoadData(fndBankCode.Value, NavType)
    End Sub

    Private Sub fndBankCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndBankCode._MYValidating
        Dim qry As String = "select count(*) from tspl_vendor_Bank_Master where Bank_code='" + clsCommon.myCstr(fndBankCode.Value) + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            fndBankCode.MyReadOnly = True
        Else
            fndBankCode.MyReadOnly = False
        End If

        If fndBankCode.MyReadOnly OrElse isButtonClicked Then
            fndBankCode.Value = clsVendorBankMaster.GetFinder("", fndBankCode.Value, isButtonClicked)

            If clsCommon.myLen(fndBankCode.Value) > 0 Then
                LoadData(fndBankCode.Value, NavigatorType.Current)
            Else
                FunReset()
            End If
        End If
    End Sub


    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDImportBankDetail.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim trans As SqlTransaction = Nothing

        ' If transportSql.importExcel(gv, "Bank Code", "Bank Name", "Branch Code", "Branch_Name", "IFSC Code", "Add1", "Add2", "Add3", "Country Code", "State Code", "City Code") Then
        If transportSql.importExcel(gv, "Bank Code", "Bank Name", "Add1", "Add2", "Add3", "Country Code", "State Code", "City Code") Then
            Dim linno As Integer = 0
            trans = clsDBFuncationality.GetTransactin()
            Try
                'connectSql.OpenConnection()
                clsCommon.ProgressBarShow()
                linno = 0
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsVendorBankMaster()
                    linno += 1
                    Dim strBankcode As String = clsCommon.myCstr(grow.Cells("Bank Code").Value)
                    If clsCommon.myLen(strBankcode) > 30 Then
                        Throw New Exception("Length of Bank Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Bank_code = strBankcode

                    Dim strBankName As String = clsCommon.myCstr(grow.Cells("Bank Name").Value)
                    If (String.IsNullOrEmpty(strBankName)) Or clsCommon.myLen(strBankName) > 200 Then
                        Throw New Exception("Length of Bank Name should be max. 200 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Bank_Name = strBankName

                    'Dim strBranchCode As String = clsCommon.myCstr(grow.Cells("Branch Code").Value)
                    'If (String.IsNullOrEmpty(strBranchCode)) Or clsCommon.myLen(strBranchCode) > 200 Then
                    '    Throw New Exception("Length of Branch Code should be max. 200 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    'End If
                    'obj.Branch_Code = strBranchCode

                    'Dim strBranchName As String = clsCommon.myCstr(grow.Cells("Branch_Name").Value)
                    'If (String.IsNullOrEmpty(strBranchName)) Or clsCommon.myLen(strBranchName) > 200 Then
                    '    Throw New Exception("Length of Branch Name should be max. 200 character ")
                    'End If

                    'obj.Branch_Name = strBranchName

                    'Dim strIFSC As String = clsCommon.myCstr(grow.Cells("IFSC Code").Value)
                    'If (String.IsNullOrEmpty(strIFSC)) Or clsCommon.myLen(strIFSC) > 200 Then
                    '    Throw New Exception("Length of IFSC Code should be max. 200 character ")
                    'End If
                    'obj.IFSC_Code = strIFSC

                    Dim strAdd1 As String = clsCommon.myCstr(grow.Cells("Add1").Value)
                    If clsCommon.myLen(strAdd1) > 150 Then
                        Throw New Exception("Length of Add1 should be max. 150 character ")
                    End If
                    obj.add1 = strAdd1

                    Dim strAdd2 As String = clsCommon.myCstr(grow.Cells("Add2").Value)
                    If clsCommon.myLen(strAdd2) > 75 Then
                        Throw New Exception("Length of Add2 should be max. 75 character ")
                    End If
                    obj.add2 = strAdd2

                    Dim strAdd3 As String = clsCommon.myCstr(grow.Cells("Add3").Value)
                    If clsCommon.myLen(strAdd3) > 75 Then
                        Throw New Exception("Length of Add3 should be max. 75 character ")
                    End If
                    obj.add3 = strAdd3

                    Dim strCountry As String = clsCommon.myCstr(grow.Cells("Country Code").Value)
                    If clsCommon.myLen(strCountry) > 30 Then
                        Throw New Exception("Length of Country code should be max. 30 character ")
                    End If
                    obj.country_code = strCountry

                    If clsCommon.myLen(strCountry) > 0 Then
                        ChkCountry(strCountry, trans)
                    End If
                    Dim strstate As String = clsCommon.myCstr(grow.Cells("State Code").Value)
                    If clsCommon.myLen(strstate) > 30 Then
                        Throw New Exception("Length of State Code should be max. 30 character ")
                    End If
                    obj.state_code = strstate
                    If clsCommon.myLen(strstate) > 0 Then
                        ChkStateCode(strstate, trans)
                    End If
                    Dim strcity As String = clsCommon.myCstr(grow.Cells("City Code").Value)
                    If clsCommon.myLen(strcity) > 30 Then
                        Throw New Exception("Length of City Code should be max. 30 character ")
                    End If
                    obj.city_code = strcity
                    If clsCommon.myLen(strcity) > 0 Then
                        ChkCityCode(strcity, trans)
                    End If
                    If clsCommon.myLen(strBankcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Master where Bank_Code ='" + strBankcode + "' ", trans) > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True

                    End If

                    clsVendorBankMaster.SaveDataImport(isNewEntry, obj, trans)

                Next

                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message & " At Line No. " & clsCommon.myCstr(linno) & "")
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click, RDExportBankDetails.Click
        Dim str As String
        str = "select Bank_Code as 'Bank Code' ,Bank_Name as 'Bank Name',Add1 as 'Add1',Add2 as 'Add2' ,Add3 as 'Add3' ,Country_Code as 'Country Code' ,State_Code as 'State Code' ,City_Code as 'City Code' from TSPL_Vendor_Bank_Master"
        ListImpExpColumnsMandatory = New List(Of String)({"Bank Code", "Bank Name"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Bank Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Function ChkCountry(ByVal CountryCode As String, ByVal trans As SqlTransaction) As Boolean

        Try
            If clsDBFuncationality.getSingleValue("Select count(*) from tspl_country_master where country_code='" + CountryCode + "' ", trans) > 0 Then
                Return True
            Else
                Throw New Exception("Country code is invalid,It could not found in Country Master")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Private Function ChkStateCode(ByVal StateCode As String, ByVal trans As SqlTransaction) As Boolean

        Try
            If clsDBFuncationality.getSingleValue("select count(*) from tspl_state_master where state_code='" + StateCode + "' ", trans) > 0 Then
                Return True
            Else
                Throw New Exception("State code is invalid,It could not found in State Master")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function ChkCityCode(ByVal CityCode As String, ByVal trans As SqlTransaction) As Boolean

        Try
            If clsCommon.myLen(CityCode) > 0 AndAlso clsDBFuncationality.getSingleValue("select count(*) from tspl_city_master where city_code='" + CityCode + "' ", trans) > 0 Then
                Return True
            Else
                Throw New Exception("City code is invalid,It could not found in City Master")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

  
    Private Sub RadMenu1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    ''richa agarwal 24/03/2014
    Sub loadBlankItemGrid()

        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing


        Dim ifsccode As New GridViewTextBoxColumn()
        ifsccode.FormatString = ""
        ifsccode.HeaderText = "IFSC Code"
        ifsccode.Name = colBankIFSCCode
        ifsccode.Width = 100
        ifsccode.ReadOnly = False
        ifsccode.WrapText = True
        ifsccode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(ifsccode)

        Dim Swiftcode As New GridViewTextBoxColumn()
        Swiftcode.FormatString = ""
        Swiftcode.HeaderText = "Swift Code"
        Swiftcode.Name = colBankSwiftCode
        Swiftcode.Width = 100
        Swiftcode.ReadOnly = False
        Swiftcode.WrapText = True
        Swiftcode.MaxLength = 20
        Swiftcode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(Swiftcode)

        Dim branchname As New GridViewTextBoxColumn()
        branchname.FormatString = ""
        branchname.HeaderText = "Branch Name"
        branchname.Name = colBankBranch
        branchname.Width = 320
        branchname.ReadOnly = False
        branchname.WrapText = True
        branchname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(branchname)


        'gv1.Rows.AddNew()
        'gv1.AllowAddNewRow = True
        'gv1.AllowDeleteRow = False
        'gv1.AllowRowReorder = False
        'gv1.ShowGroupPanel = False
        'gv1.EnableFiltering = False
        'gv1.EnableSorting = False
        'gv1.EnableGrouping = False
        'gv1.AllowColumnReorder = True
        'gv1.ShowGroupPanel = False
        'gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        'gv1.MasterTemplate.ShowRowHeaderColumn = False
        'gv1.TableElement.TableHeaderHeight = 40

        gv1.Rows.AddNew()
        gv1.AllowDeleteRow = True
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnReorder = True
        gv1.AllowAddNewRow = True
        gv1.ShowGroupPanel = False

        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        ifsccode = Nothing
        branchname = Nothing
    End Sub

    Private Sub RDExportBranchDetails_Click(sender As Object, e As EventArgs) Handles RDExportBranchDetails.Click
        Dim str As String
        str = "Select Bank_Code as [Bank Code],Branch_Name as [Branch],Bank_IFSC_Code as [IFSC Code],Bank_Swift_Code as [Swift Code] from TSPL_Vendor_Bank_Branch_Details "
        ListImpExpColumnsMandatory = New List(Of String)({"Bank Code", "Branch", "IFSC Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Bank Code", "IFSC Code"})
        transportSql.ExporttoExcel(str, "Branch", "IFSC Code", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "BranchDetails")
    End Sub

    Private Sub RDImportBranchDetail_Click(sender As Object, e As EventArgs) Handles RDImportBranchDetail.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim trans As SqlTransaction = Nothing

        If transportSql.importExcel(gv, "Bank Code", "Branch", "IFSC Code", "Swift Code") Then
            Dim linno As Integer = 0
            trans = clsDBFuncationality.GetTransactin()
            Try
                'connectSql.OpenConnection()
                clsCommon.ProgressBarShow()
                linno = 0
                Dim strBankcode As String = Nothing
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsVendorBankBranchDetail()
                    linno += 1
                    strBankcode = clsCommon.myCstr(grow.Cells("Bank Code").Value)
                    If clsCommon.myLen(strBankcode) > 30 Then
                        Throw New Exception("Length of Bank Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Bank_Code = strBankcode


                    Dim strBranchName As String = clsCommon.myCstr(grow.Cells("Branch").Value)
                    If (String.IsNullOrEmpty(strBranchName)) Or clsCommon.myLen(strBranchName) > 100 Then
                        Throw New Exception("Length of Branch should be max. 100 character ")
                    End If

                    obj.Branch_Name = strBranchName

                    Dim strIFSC As String = clsCommon.myCstr(grow.Cells("IFSC Code").Value)
                    If (String.IsNullOrEmpty(strIFSC)) Or clsCommon.myLen(strIFSC) > 100 Then
                        Throw New Exception("Length of IFSC Code should be max. 100 character ")
                    End If
                    obj.Bank_IFSC_Code = strIFSC

                    Dim strSwift As String = clsCommon.myCstr(grow.Cells("Swift Code").Value)
                    If clsCommon.myLen(strSwift) > 20 Then
                        Throw New Exception("Length of Swift Code should be max. 20 character ")
                    End If
                    obj.Bank_Swift_Code = strSwift


                    Dim coll As Hashtable

                    If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Master where Bank_Code ='" + strBankcode + "'  ", trans) <= 0 Then
                        Throw New Exception("Bank Code Does Not Exist : " + strBankcode + ".Please make entry in vendor bank master.")
                    End If

                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Bank_Code", strBankcode)
                    clsCommon.AddColumnsForChange(coll, "Branch_Name", obj.Branch_Name)
                    clsCommon.AddColumnsForChange(coll, "Bank_IFSC_Code", obj.Bank_IFSC_Code)
                    clsCommon.AddColumnsForChange(coll, "Bank_Swift_Code", obj.Bank_Swift_Code)
                    If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_IFSC_Code ='" + strIFSC + "'  and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & obj.Bank_Code & "'", trans) <= 0 Then
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Vendor_Bank_Branch_Details", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Vendor_Bank_Branch_Details", OMInsertOrUpdate.Update, " TSPL_Vendor_Bank_Branch_Details.Bank_IFSC_Code='" & obj.Bank_IFSC_Code & "' and TSPL_Vendor_Bank_Branch_Details.Bank_Code='" & obj.Bank_Code & "'", trans)
                    End If
                Next

                'Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select * from TSPL_Vendor_Bank_Branch_Details where Bank_Code ='" & strBankcode & "'", trans)

                'If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_Code ='" & strBankcode & "' group by Bank_IFSC_Code having count(Bank_IFSC_Code)>1  ", trans) > 1 Then
                '    Throw New Exception("Same IFSC Code Does Not Exist more than one time.")
                'End If

                'If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Vendor_Bank_Branch_Details where Bank_Code ='" & strBankcode & "' group by Bank_Swift_Code having count(Bank_Swift_Code)>1  ", trans) > 1 Then
                '    Throw New Exception("Same Swift Code Does Not Exist more than one time")
                'End If

                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message & " At Line No. " & clsCommon.myCstr(linno) & "")
            End Try
        End If
        Me.Controls.Remove(gv)

    End Sub

    
End Class
