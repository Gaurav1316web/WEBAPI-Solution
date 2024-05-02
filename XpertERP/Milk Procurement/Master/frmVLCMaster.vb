'--Created By Monika 26/05/2014
'=============BM00000003403======BM00000002679=====BM00000003414=
Imports common
Imports System.Data.SqlClient
Public Class FrmVLCMaster
    Inherits FrmMainTranScreen

#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colVillCode As String = "village_code"
    Const colVillName As String = "village_name"
    Const colRoutecode As String = "route_code"
    Const colRoutename As String = "route_name"
    Dim isLoadData As Boolean = False
    Dim isValueChanged As Boolean = True
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim isNewEntry As Boolean = True
    Dim arrLoc As String = Nothing
#End Region

    Private Sub MCCLOCATIONFINDER()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                If clsCommon.myLen(fndMcc.Value) <= 0 AndAlso Not obj.Default_HO Then
                    fndMcc.Value = IIf(obj.Default_LocName = "_", "", obj.Default_LocName)
                End If
                arrLoc = obj.arrLocCodes
            Else
                'cmbmcc.Enabled = False
                fndMcc.Enabled = False
                Throw New Exception("Please Set Default Location Of LogIn User")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        chkInActive.Checked = False
        txtVLCCodeVlcUploader.Text = ""
        txtvillcode.Value = ""
        txtvillname.Text = ""
        txtroutecode.Value = ""
        txtroutename.Text = ""
        isLoadData = False
        isValueChanged = True
        fndvlccode.MyReadOnly = False
        fndvlccode.Value = ""
        txtvlcname.Text = ""
        'txtvehicalname.Text = ""
        txtvsp.Text = ""
        txtvspcode.Value = ""
        fndMcc.Value = ""
        fndPriceCode.Value = Nothing
        fndPriceCode.Enabled = True
        gv.Rows.Clear()
        gv.Rows.AddNew()
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        isNewEntry = True
        btnsave.Text = "Save"
        btndelete.Enabled = False
        chkApplyPriceChartUploader.Checked = False
        chkSuspense.Checked = False
        txtShortDescription.Text = ""
        fndgroupcode.Value = ""
        txtGroupCodeDesc.Text = ""
        cboMilkReceiveUOM.SelectedValue = ""
        cboAutoFillMPOrder.SelectedValue = 0
        fndgroupcode.Enabled = False
        MCCLOCATIONFINDER()
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
            UcCustomFields1.SetDefaultValues()
        End If
        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AutoUpdateVLCUploaderCodeInVLCMaster, clsFixedParameterCode.AutoUpdateVLCUploaderCodeInVLCMaster, Nothing), "1") = CompairStringResult.Equal Then
            txtVLCCodeVlcUploader.Enabled = False
        Else
            txtVLCCodeVlcUploader.Enabled = True
        End If
        'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
        '    UcCustomFields1.SetDefaultValues()
        'End If
        txtCowPriceDate.Checked = False
        txtCowPriceDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub FrmVLCMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            btnnew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmVLCMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadMilkReceiveUOM()
        LoadAutoFillMPOrder()
        MCCLOCATIONFINDER()
        LoadBlankGrid()
        Reset()
        If objCommonVar.PricePlan = 4 Then
            chkApplyPriceChartUploader.Visible = True
        End If
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
    End Sub

    Sub LoadMilkReceiveUOM()
        Dim qry As String = " SELECT '' AS Code,'According To MCC' as Name union SELECT 'KG' AS Code, 'KG' as Name union SELECT 'LTR' AS Code, 'LTR' as Name "
        cboMilkReceiveUOM.DataSource = clsDBFuncationality.GetDataTable(qry)
        cboMilkReceiveUOM.ValueMember = "Code"
        cboMilkReceiveUOM.DisplayMember = "Name"
    End Sub

    Sub LoadAutoFillMPOrder()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = 0
        dr("Name") = "N/A"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 1
        dr("Name") = "Uploader No Asc"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 2
        dr("Name") = "Uploader No Desc"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 3
        dr("Name") = "Name Asc"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 4
        dr("Name") = "Name Desc"
        dt.Rows.Add(dr)

        cboAutoFillMPOrder.DataSource = dt
        cboAutoFillMPOrder.ValueMember = "Code"
        cboAutoFillMPOrder.DisplayMember = "Name"
        cboAutoFillMPOrder.SelectedValue = ""
    End Sub

    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repocode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode.FormatString = ""
        repocode.Name = colVillCode
        repocode.Width = 150
        repocode.HeaderText = "Village Code"
        repocode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repocode)

        Dim reponame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponame.FormatString = ""
        reponame.Name = colVillName
        reponame.Width = 320
        reponame.HeaderText = "Village Name"
        reponame.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reponame)

        'Dim repocode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repocode1.FormatString = ""
        'repocode1.Name = colRoutecode
        'repocode1.Width = 80
        'repocode1.HeaderText = "Route Code"
        'repocode1.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'repocode1.TextImageRelation = TextImageRelation.TextBeforeImage
        'gv.MasterTemplate.Columns.Add(repocode1)

        'Dim reponame1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'reponame1.FormatString = ""
        'reponame1.Name = colRoutename
        'reponame1.Width = 190
        'reponame1.HeaderText = "Route"
        'reponame1.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(reponame1)

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.Rows.AddNew()
    End Sub

    'Sub LoadCombobox()
    '    Dim qry As String = ""
    '    qry = "select mcc_code as Code,mcc_name as Name from tspl_mcc_master"
    '    If clsCommon.myLen(arrLoc) > 0 Then
    '        qry += " where mcc_code in (" + arrLoc + ")"
    '    End If
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        cmbmcc.DataSource = Nothing

    '        cmbmcc.DataSource = dt
    '        cmbmcc.DisplayMember = "Name"
    '        cmbmcc.ValueMember = "Code"
    '    Else
    '        clsCommon.MyMessageBoxShow("Before Doing VLC Master Entry,Make MCC Master", Me.Text)
    '        Reset()
    '        'Me.Close()
    '    End If
    'End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmVLCMaster)
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

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtvlcname.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Fill VLC Name", Me.Text)
                txtvlcname.Focus()
                txtvlcname.Select()
                Errorcontrol.SetError(txtvlcname, "Please Fill VLC Name")
                Return False
            Else
                Errorcontrol.ResetError(txtvlcname)
            End If
            If txtVLCCodeVlcUploader.Enabled = True Then
                If clsCommon.myLen(txtVLCCodeVlcUploader.Text) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Fill VLC Code For VLC Uploader ", Me.Text)
                    txtVLCCodeVlcUploader.Focus()
                    txtVLCCodeVlcUploader.Select()
                    Errorcontrol.SetError(txtVLCCodeVlcUploader, "Please Fill VLC Code For VLC Uploader")
                    Return False
                Else
                    Errorcontrol.ResetError(txtVLCCodeVlcUploader)
                End If
            End If
            
            If isDuplicateVLCCode(IIf(clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal, False, True)) Then

                clsCommon.MyMessageBoxShow(Me, " Duplicate VLC Code for VLC Uploader", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
                txtVLCCodeVlcUploader.Focus()
                Errorcontrol.SetError(txtVLCCodeVlcUploader, "Duplicate VLC Code for VLC Uploader")
                Return False
            Else
                Errorcontrol.SetError(txtVLCCodeVlcUploader, "")
            End If
            'If clsCommon.myLen(txtvehicalname.Text) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Fill Vehicle Name", Me.Text)
            '    txtvehicalname.Focus()
            '    txtvehicalname.Select()
            '    Errorcontrol.SetError(txtvehicalname, "Please Fill Vehicle Name")
            '    Return False
            'Else
            '    Errorcontrol.ResetError(txtvehicalname)
            'End If

            If clsCommon.myLen(txtvillcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Village Code/Name", Me.Text)
                txtvillcode.Focus()
                txtvillcode.Select()
                Errorcontrol.SetError(txtvillcode, "Please Select Village Code/Name")
                Return False
            Else
                Errorcontrol.ResetError(txtvillcode)
            End If

            'If clsCommon.myLen(txtroutecode.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Select Route Code/Name", Me.Text)
            '    txtroutecode.Focus()
            '    txtroutecode.Select()
            '    Errorcontrol.SetError(txtroutecode, "Please Select Route Code/Name")
            '    Return False
            'Else
            '    Errorcontrol.ResetError(txtroutecode)
            'End If

            If clsCommon.myLen(txtvspcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select VSP Code/Name", Me.Text)
                txtvspcode.Focus()
                txtvspcode.Select()
                Errorcontrol.SetError(txtvspcode, "Please Select VSP Code/Name")
                Return False
            Else
                Errorcontrol.ResetError(txtvspcode)
            End If

            If clsCommon.myLen(fndMcc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select MCC", Me.Text)
                fndMcc.Focus()
                fndMcc.Select()
                Errorcontrol.SetError(fndMcc, "Please Select MCC")
                Return False
            Else
                Errorcontrol.ResetError(fndMcc)
            End If

            Dim villcode As String = ""
            Dim routecode As String = ""
            Try
                villcode = clsCommon.myCstr(gv.Rows(0).Cells(colVillCode).Value)
            Catch exx As Exception
                villcode = ""
            End Try
            '-----------check whether the same VSP mapped earlier with the Other VLC----------------------------------------------------------------------------
            Dim check As String = ""
            check = clsDBFuncationality.getSingleValue("select VLC_NAME from TSPL_VLC_MASTER_HEAD where vsp_code='" + txtvspcode.Value + "' and vlc_code<>'" & fndvlccode.Value & "'")

            If clsCommon.myLen(check) > 0 AndAlso clsCommon.CompairString(check, txtvspcode.Value) <> CompairStringResult.Equal Then
                txtvspcode.Value = ""
                txtvspcode.Text = ""
                txtvspcode.Focus()
                txtvspcode.Select()
                Errorcontrol.SetError(txtvspcode, "Selected VSP Already Mapped With VLC " + check + "," + Environment.NewLine + "Please Select The Same VLC As Set Earlier Or" + Environment.NewLine + "Changed Mapping In Old Record First")
                Throw New Exception("Selected VSP Already Mapped With VLC " + check + "," + Environment.NewLine + "Please Select The Same VLC As Set Earlier Or" + Environment.NewLine + "Changed Mapping In Old Record First")
            Else
                Errorcontrol.ResetError(txtvspcode)
            End If
            '---------------------------------------------------------------------
            'If clsCommon.myLen(villcode) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please Fill Atleast One Row", Me.Text)
            '    Errorcontrol.SetError(gv, "Please Fill Atleast One Row")
            '    Return False
            'Else
            '    Errorcontrol.ResetError(gv)
            'End If

            If clsCommon.myLen(villcode) > 0 Then
                For ii As Integer = 0 To gv.Rows.Count - 1
                    villcode = ""
                    routecode = ""
                    Dim vill1 As String = ""
                    Dim route1 As String = ""

                    Try
                        villcode = clsCommon.myCstr(gv.Rows(ii).Cells(colVillCode).Value)
                    Catch exx As Exception
                        villcode = ""
                    End Try

                    If clsCommon.CompairString(villcode, txtvillcode.Value) = CompairStringResult.Equal Then
                        Throw New Exception("Grid Not Have Same Village As Selected In Header Part,Please Change The Village At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    End If
                    'Try
                    '    routecode = clsCommon.myCstr(gv.Rows(ii).Cells(colRoutecode).Value)
                    'Catch exx As Exception
                    '    routecode = ""
                    'End Try

                    'If clsCommon.myLen(villcode) > 0 AndAlso clsCommon.myLen(routecode) <= 0 Then
                    '    clsCommon.MyMessageBoxShow("Please Select Route At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "", Me.Text)
                    '    Return False
                    'End If

                    For jj As Integer = ii + 1 To gv.Rows.Count - 1 '-------for checking duplicate values
                        Try
                            vill1 = clsCommon.myCstr(gv.Rows(jj).Cells(colVillCode).Value)
                        Catch exx As Exception
                            vill1 = ""
                        End Try


                        Try
                            route1 = clsCommon.myCstr(gv.Rows(jj).Cells(colRoutecode).Value)
                        Catch exx As Exception
                            route1 = ""
                        End Try

                        If clsCommon.myLen(villcode) > 0 AndAlso (clsCommon.CompairString(villcode, vill1) = CompairStringResult.Equal) Then ' AndAlso clsCommon.CompairString(routecode, route1) = CompairStringResult.Equal)
                            clsCommon.MyMessageBoxShow("Same Village Name Can-Not Be Repeated More Than Once,Please Check Data At Row No. " + clsCommon.myCstr(CInt(jj) + 1) + "", Me.Text)
                            Errorcontrol.SetError(gv, "Same Village Name Can-Not Be Repeated More Than Once,Please Check Data At Row No. " + clsCommon.myCstr(CInt(jj) + 1) + "")
                            Return False
                        Else
                            Errorcontrol.ResetError(gv)
                        End If
                    Next
                Next
            End If
            UcCustomFields1.AllowToSave()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function
    Function isDuplicateVLCCode(ByVal isUpdate As Boolean) As Boolean

        Dim qry As String = "select COUNT(*) from TSPL_VLC_MASTER_HEAD where VLC_Code_vlc_uploader='" & txtVLCCodeVlcUploader.Text & "' and vlc_code<>'" & fndvlccode.Value & "' and mcc='" & fndMcc.Value & "'"
        Dim rvalue As Boolean = False
        Dim cnt As Integer = 0
        cnt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If cnt >= 1 Then
            rvalue = True
            'ElseIf (Not isUpdate) And cnt >= 1 Then
            '    rvalue = True
        Else
            rvalue = False
        End If
        Return rvalue
    End Function
    Sub SaveData()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmVLCMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            Dim obj As New clsfrmVLCMaster()

            obj.vlcCode = clsCommon.myCstr(fndvlccode.Value)
            obj.VLC_CODE_VLC_UPLOADER = clsCommon.myCstr(txtVLCCodeVlcUploader.Text)
            obj.vlcName = clsCommon.myCstr(txtvlcname.Text.Replace("'", "`"))
            'obj.vehical = clsCommon.myCstr(txtvehicalname.Text.Replace("'", "`"))
            obj.vspCode = clsCommon.myCstr(txtvspcode.Value)
            obj.MCCCOde = clsCommon.myCstr(fndMcc.Value)
            obj.mainvillcode = clsCommon.myCstr(txtvillcode.Value)
            obj.routecode = clsCommon.myCstr(txtroutecode.Value)
            obj.Active = IIf(Me.chkInActive.Checked, 0, 1)
            obj.Auto_Fill_MP_Order = clsCommon.myCdbl(cboAutoFillMPOrder.SelectedValue)
            obj.Price_Code = clsCommon.myCstr(fndPriceCode.Value)
            obj.Milk_Receive_UOM = clsCommon.myCstr(cboMilkReceiveUOM.SelectedValue)
            'If fndPriceCode.Enabled = True Then
            '    obj.IsavedPriceCode = False
            'Else
            '    obj.IsavedPriceCode = True
            'End If
            obj.Short_Description = txtShortDescription.Text
            obj.Apply_Price_Chart_Uploader = chkApplyPriceChartUploader.Checked
            obj.IsSuspense = chkSuspense.Checked
            If txtCowPriceDate.Checked Then
                obj.ApplyCowPriceDate = txtCowPriceDate.Value
            End If

            Dim arr As New List(Of clsfrmVLCMaster)

            For Each grow As GridViewRowInfo In gv.Rows
                Dim objtr As New clsfrmVLCMaster()

                objtr.villagecode = clsCommon.myCstr(grow.Cells(colVillCode).Value)
                'objtr.routecode = clsCommon.myCstr(grow.Cells(colRoutecode).Value)

                If clsCommon.myLen(objtr.villagecode) > 0 Then
                    arr.Add(objtr)
                End If
            Next
            ''For Custom Fields
            obj.Form_ID = MyBase.Form_ID
            obj.arrCustomFields = New List(Of clsCustomFieldValues)
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.GetData(obj.arrCustomFields)
            End If
            'If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
            '    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colICode)
            'End If
            ''End of For Custom Fields

            If clsfrmVLCMaster.SaveData(obj.vlcCode, isNewEntry, obj, arr, Nothing) Then
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If
                fndvlccode.Value = obj.vlcCode
                fndvlccode.MyReadOnly = True
                btnsave.Text = "Update"
                btndelete.Enabled = True
                UcAttachment1.SaveData(obj.vlcCode)
                LoadData(obj.vlcCode, NavigatorType.Current)
            Else
                fndvlccode.MyReadOnly = False
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(fndvlccode.Value) <= 0 Then
                Throw New Exception("Please Select VLC Code For Deletion")
            End If

            Dim qry As String = "select count(*) from TSPL_VLC_MASTER_HEAD where VLC_Code='" + fndvlccode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

            If check <= 0 Then
                Throw New Exception("No Data Found For Deletion")
            End If

            If Not clsCommon.MyMessageBoxShow("Are You Sure,Want To Delete The VLC Code " + fndvlccode.Value + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Return
            End If

            qry = "delete from TSPL_VLC_MASTER_DETAIL where VLC_Code='" & fndvlccode.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_FAT_SNF_UPLOADER_VLC where VLC_Code='" & fndvlccode.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_VLC_MASTER_HEAD where VLC_Code='" & fndvlccode.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
            Reset()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtvspcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtvspcode._MYValidating

        txtvspcode.Value = clsVendorMaster.getFinder(" form_type='VSP'", txtvspcode.Value, isButtonClicked)

        If clsCommon.myLen(txtvspcode.Value) > 0 Then
            txtvsp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + txtvspcode.Value + "' and form_type='VSP'"))
            fndgroupcode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Group_Code from tspl_vendor_master where vendor_code='" + txtvspcode.Value + "' and form_type='VSP'"))
            txtGroupCodeDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Group_Desc from TSPL_VENDOR_GROUP where Ven_Group_Code='" + fndgroupcode.Value + "' "))
        End If
    End Sub


    Private Sub gv_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isLoadData Then '------when on loaddata then it should not run
            If isValueChanged Then
                isValueChanged = False

                If gv.CurrentColumn Is gv.Columns(colVillCode) Then
                    OpenVillage()
                    isValueChanged = True
                    'ElseIf gv.CurrentColumn Is gv.Columns(colRoutecode) Then
                    '    OpenRoute()
                    '    isValueChanged = True
                End If
            End If
        End If
        'isValueChanged = True
    End Sub

    Sub OpenVillage()
        Dim qry As String = "select TSPL_VILLAGE_MASTER.Village_Code as Code,TSPL_VILLAGE_MASTER.Village_Name as [Village Name],(TSPL_VILLAGE_MASTER.Add1+' '+TSPL_VILLAGE_MASTER.Add2) as Address,TSPL_CITY_MASTER.City_Name as [City],TSPL_STATE_MASTER.STATE_NAME as [State],TSPL_VILLAGE_MASTER.PIN_NO as [Pin Code] from TSPL_VILLAGE_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_VILLAGE_MASTER.City_Code left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_VILLAGE_MASTER.State_Code where TSPL_VILLAGE_MASTER.Village_Code<>'" + txtvillcode.Value + "'"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("VILFND", qry)

        If dr IsNot Nothing Then
            gv.CurrentRow.Cells(colVillCode).Value = clsCommon.myCstr(dr("code"))
            gv.CurrentRow.Cells(colVillName).Value = clsCommon.myCstr(dr("village name"))
        Else
            gv.CurrentRow.Cells(colVillCode).Value = ""
            gv.CurrentRow.Cells(colVillName).Value = ""
        End If
    End Sub

    Sub OpenRoute()
        Dim qry As String = "select TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Description] from TSPL_MCC_ROUTE_MASTER"
        If clsCommon.myLen(arrLoc) > 0 Then
            qry += " where TSPL_MCC_ROUTE_MASTER.mcc_code in (" + arrLoc + ")"
        End If
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ROTFND", qry)

        If dr IsNot Nothing Then
            txtroutecode.Value = clsCommon.myCstr(dr("code")) 'gv.CurrentRow.Cells(colRoutecode).Value
            txtroutename.Text = clsCommon.myCstr(dr("Route Description")) 'gv.CurrentRow.Cells(colRoutename).Value
        Else
            txtroutecode.Value = ""
            txtroutename.Text = ""
            'gv.CurrentRow.Cells(colRoutecode).Value = ""
            'gv.CurrentRow.Cells(colRoutename).Value = ""
        End If
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If clsCommon.myLen(fndvlccode.Value) > 0 Then
            If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            Else
                Dim qry As String = " delete from TSPL_VLC_MASTER_DETAIL where village_code= '" + clsCommon.myCstr(gv.CurrentRow.Cells(colVillCode).Value) + "' and VLC_Code='" + fndvlccode.Value + "'" ' and route_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colRoutecode).Value) + "'
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            gv.Rows.Clear()

            Dim obj As clsfrmVLCMaster = clsfrmVLCMaster.GetData(strCode, arrLoc, NavType)
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.vlcCode) > 0 Then
                isNewEntry = False
                fndvlccode.Value = obj.vlcCode
                txtVLCCodeVlcUploader.Text = clsCommon.myCstr(obj.VLC_CODE_VLC_UPLOADER)
                txtvlcname.Text = obj.vlcName
                'txtvehicalname.Text = obj.vehical
                txtvspcode.Value = obj.vspCode
                txtvsp.Text = obj.VspName
                fndMcc.Value = obj.MCCCOde
                txtvillcode.Value = obj.mainvillcode
                txtvillname.Text = obj.mainvillname
                txtroutecode.Value = obj.routecode
                txtroutename.Text = obj.routename
                cboMilkReceiveUOM.SelectedValue = obj.Milk_Receive_UOM
                chkApplyPriceChartUploader.Checked = obj.Apply_Price_Chart_Uploader
                chkSuspense.Checked = obj.IsSuspense
                txtShortDescription.Text = obj.Short_Description

                cboAutoFillMPOrder.SelectedValue = obj.Auto_Fill_MP_Order
                'fndPriceCode.Value = clsfrmVLCMaster.getPriceCode(obj.vlcCode, obj.MCCCOde, False, Nothing)
                'If clsCommon.myLen(fndPriceCode.Value) > 0 Then
                '    fndPriceCode.Enabled = False
                'Else
                '    fndPriceCode.Enabled = True
                'End If
                If clsCommon.myLen(obj.vspCode) > 0 Then
                    fndgroupcode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Group_Code from tspl_vendor_master where vendor_code='" + txtvspcode.Value + "' and form_type='VSP'"))
                    txtGroupCodeDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Group_Desc from TSPL_VENDOR_GROUP where Ven_Group_Code='" + fndgroupcode.Value + "' "))
                End If
                fndPriceCode.Value = obj.Price_Code
                Me.chkInActive.Checked = IIf(obj.Active = 0, True, False)
                If obj.ApplyCowPriceDate.HasValue Then
                    txtCowPriceDate.Value = obj.ApplyCowPriceDate
                    txtCowPriceDate.Checked = True
                Else
                    txtCowPriceDate.Value = clsCommon.GETSERVERDATE()
                    txtCowPriceDate.Checked = False
                End If
                isLoadData = True
                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    For Each objtr As clsfrmVLCMaster In obj.arr
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colVillCode).Value = objtr.villagecode
                        gv.Rows(gv.Rows.Count - 1).Cells(colVillName).Value = objtr.villagename
                        'gv.Rows(gv.Rows.Count - 1).Cells(colRoutecode).Value = objtr.routecode
                        'gv.Rows(gv.Rows.Count - 1).Cells(colRoutename).Value = objtr.routename
                    Next
                Else
                    gv.Rows.AddNew()
                End If

                btnsave.Text = "Update"
                btndelete.Enabled = True
                fndvlccode.MyReadOnly = True
                UcAttachment1.LoadData(fndvlccode.Value)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.BlankAllControls()
                    UcCustomFields1.LoadData(obj.vlcCode)
                End If
            Else
                Reset()
            End If

            isLoadData = False
        Catch ex As Exception
            isNewEntry = True
            isLoadData = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndvlccode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndvlccode._MYNavigator
        LoadData(fndvlccode.Value, NavType)
    End Sub

    Private Sub fndvlccode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndvlccode._MYValidating
        Dim qry As String = "select TSPL_VLC_MASTER_HEAD.vlc_code as [Code],TSPL_VLC_MASTER_HEAD.vlc_name as [VLC Name],TSPL_VLC_MASTER_HEAD.vehical_name as [Vehicle Name],TSPL_VLC_MASTER_HEAD.village_code as [Village Code],tspl_village_master.village_name as [Village Name],TSPL_VLC_MASTER_HEAD.route_code as [Route Code],tspl_mcc_route_master.route_name as [Route Name],TSPL_VLC_MASTER_HEAD.vsp_code as [VSP Code],TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VENDOR_MASTER.Vendor_Group_Code as [Group Code], TSPL_VENDOR_GROUP.Group_Desc as [Group  Description],TSPL_VLC_MASTER_HEAD.mcc as [MCC Code],TSPL_MCC_MASTER.mcc_name as [MCC Name],TSPL_VLC_MASTER_HEAD.created_by as [Created By],TSPL_VLC_MASTER_HEAD.created_date as [Created Date],TSPL_VLC_MASTER_HEAD.modified_by as [Modified By],TSPL_VLC_MASTER_HEAD.modified_date as [Modified Date],TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader as [VLC Code For VLC Uploader] from TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code and TSPL_VENDOR_MASTER.Form_Type='VSP' left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc left outer join tspl_village_master on TSPL_VLC_MASTER_HEAD.village_code=tspl_village_master.village_code left outer join tspl_mcc_route_master on TSPL_VLC_MASTER_HEAD.route_code=tspl_mcc_route_master.route_code left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code "
        If clsCommon.myLen(arrLoc) > 0 Then
            fndvlccode.Value = clsCommon.ShowSelectForm("VLCFND2", qry, "Code", " TSPL_VLC_MASTER_HEAD.mcc in (" + arrLoc + ")", fndvlccode.Value, "Code", isButtonClicked)
        Else
            fndvlccode.Value = clsCommon.ShowSelectForm("VLCFND2", qry, "Code", " ", fndvlccode.Value, "Code", isButtonClicked)
        End If

        If clsCommon.myLen(fndvlccode.Value) > 0 Then
            txtvlcname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vlc_name from TSPL_VLC_MASTER_HEAD where vlc_code='" + fndvlccode.Value + "'"))
            LoadData(fndvlccode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub btnhead_ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhead_ex.Click
        Dim qry As String = "select count(*) from tspl_vlc_master_head"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select TSPL_VLC_MASTER_HEAD.vlc_code as [Code],TSPL_VLC_MASTER_HEAD.vlc_name as [VLC Name],TSPL_VLC_MASTER_HEAD.village_code as [Village Code],tspl_village_master.village_name as [Village Name],TSPL_VLC_MASTER_HEAD.route_code as [Route Code],tspl_mcc_route_master.route_name as [Route Name],TSPL_VLC_MASTER_HEAD.vsp_code as [VSP Code],TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VENDOR_MASTER.Vendor_Group_Code as [Group Code], TSPL_VENDOR_GROUP.Group_Desc as [Group Description],TSPL_VLC_MASTER_HEAD.mcc as [MCC Code],TSPL_MCC_MASTER.mcc_name as [MCC Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [VLC Uploader Code],TSPL_VLC_MASTER_HEAD.Price_Code as [Price Code],TSPL_VLC_MASTER_HEAD.Milk_Receive_UOM as [Milk Receive UOM]"
            If objCommonVar.PricePlan = 4 Then
                qry += ",case when Apply_Price_Chart_Uploader=1 then 'Y' else 'N' end as [Apply Price Chart Uploader(Y/N)]"
            End If
            qry += " from TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code and TSPL_VENDOR_MASTER.Form_Type='VSP' left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc left outer join tspl_village_master on TSPL_VLC_MASTER_HEAD.village_code=tspl_village_master.village_code left outer join tspl_mcc_route_master on TSPL_VLC_MASTER_HEAD.route_code=tspl_mcc_route_master.route_code left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code "
        Else
            qry = "select '' as [Code],'' as [VLC Name],'' as [Village Code],'' as [Village Name],'' as [Route Code],'' as [Route Name],'' as [VSP Code],'' as [VSP Name],'' as [Group Code] , '' as [Group Description],'' as [MCC Code],'' as [MCC Name],'' as [VLC Uploader Code],'' as Price Code,'' as [Milk Receive UOM]"
            If objCommonVar.PricePlan = 4 Then
                qry += ",'Y' as [Apply Price Chart Uploader(Y/N)]"
            End If
        End If
        ListImpExpColumnsMandatory = New List(Of String)({"Code", "VLC Name", "VSP Code", "VSP Name", "MCC Code", "MCC Name", "VLC Uploader Code", "village code", "village name", "Price Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
        transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "HEAD")
    End Sub

    Private Sub btngrid_ex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngrid_ex.Click
        Dim qry As String = "select count(*) from TSPL_VLC_MASTER_DETAIL"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select distinct TSPL_VLC_MASTER_DETAIL.vlc_code,TSPL_VLC_MASTER_DETAIL.village_code,TSPL_VILLAGE_MASTER.village_name from TSPL_VLC_MASTER_DETAIL left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.village_code=TSPL_VLC_MASTER_DETAIL.village_code"
        Else
            qry = "select distinct '' as vlc_code,'' as village_code,'' as village_name"
        End If
        ListImpExpColumnsMandatory = New List(Of String)({"vlc_code", "village_code", "village_name"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"vlc_code", "village_code"})
        transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "DETAIL")
    End Sub

    Private Sub btnhead_im_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhead_im.Click

        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim flag As Boolean = False
        If objCommonVar.PricePlan = 4 Then
            flag = transportSql.importExcel(gv, "Code", "VLC Name", "Village Code", "Village Name", "Route Code", "Route Name", "VSP Code", "VSP Name", "Group Code", "Group Description", "MCC Code", "MCC Name", "VLC Uploader Code", "Price Code", "Milk Receive UOM", "Apply Price Chart Uploader(Y/N)")
        Else
            flag = transportSql.importExcel(gv, "Code", "VLC Name", "Village Code", "Village Name", "Route Code", "Route Name", "VSP Code", "VSP Name", "Group Code", "Group Description", "MCC Code", "MCC Name", "VLC Uploader Code", "Price Code", "Milk Receive UOM")
        End If
        If flag Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try

                Dim code As String = "'"
                Dim name As String = ""
                'Dim vehical As String = ""
                Dim vspcode As String = ""
                Dim vspname As String = ""
                Dim mcccode As String = ""
                Dim mccname As String = ""
                Dim villcode As String = ""
                Dim villname As String = ""
                Dim routecode As String = ""
                Dim routename As String = ""
                Dim VlcUploaderCode As String = ""
                Dim strPriceCode As String = String.Empty

                clsCommon.ProgressBarShow()
                Dim counter As Integer = 1
                For Each grow As GridViewRowInfo In gv.Rows
                    code = clsCommon.myCstr(grow.Cells("Code").Value)
                    name = clsCommon.myCstr(grow.Cells("VLC Name").Value).Replace("'", "`")
                    If clsCommon.myLen(name) <= 0 Then
                        Throw New Exception("Fill VLC Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(name) > 150 Then
                        Throw New Exception("Length Of VLC Name Should Not Exceed 150 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    vspcode = clsCommon.myCstr(grow.Cells("VSP Code").Value)
                    vspname = clsCommon.myCstr(grow.Cells("VSP Name").Value)
                    If clsCommon.myLen(vspcode) <= 0 AndAlso clsCommon.myLen(vspname) <= 0 Then
                        Throw New Exception("Fill VSP Code/VSP Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(vspcode) > 12 Then
                        Throw New Exception("Length Of VSP Code Should Not Exceed 12 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim qry As String = ""
                    Dim check As Integer = 0
                    If clsCommon.myLen(vspcode) > 0 Then
                        qry = "select count(*) from tspl_vendor_master where form_type='VSP' and vendor_code='" + vspcode + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            Throw New Exception("VSP Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of VSP Master")
                        End If
                    End If
                    '-----------check whether the same VSP mapped earlier with the Other VLC----------------------------------------------------------------------------
                    If clsCommon.CompairString(objCommonVar.CurrentUserCode, "Rohit G") <> CompairStringResult.Equal Then
                        Dim checkVSP As String = ""
                        checkVSP = clsDBFuncationality.getSingleValue("select VLC_NAME from TSPL_VLC_MASTER_HEAD where vsp_code='" + vspcode + "' and vlc_code<>'" & code & "'", trans)

                        If clsCommon.myLen(checkVSP) > 0 AndAlso clsCommon.CompairString(checkVSP, name) <> CompairStringResult.Equal Then
                            Errorcontrol.SetError(txtvspcode, "Selected VSP Already Mapped With VLC " & checkVSP & "," & Environment.NewLine & "Please Select The Same VLC As Set Earlier Or" & Environment.NewLine & "Changed Mapping In Old Record First At Line No. " & clsCommon.myCstr(counter) & "")
                            Throw New Exception("Selected VSP Already Mapped With VLC " & checkVSP & "," & Environment.NewLine & "Please Select The Same VLC As Set Earlier Or" & Environment.NewLine & "Changed Mapping In Old Record First At Line No. " & clsCommon.myCstr(counter) & "")
                        Else
                            Errorcontrol.ResetError(txtvspcode)
                        End If
                    End If
                    '---------------------------------------------------------------------
                    '-------------------------------

                    mcccode = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                    mccname = clsCommon.myCstr(grow.Cells("MCC Name").Value)
                    VlcUploaderCode = clsCommon.myCstr(grow.Cells("VLC Uploader Code").Value)
                    If clsCommon.myLen(mcccode) <= 0 AndAlso clsCommon.myLen(mccname) <= 0 Then
                        Throw New Exception("Fill MCC Code/MCC Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(mcccode) > 30 Then
                        Throw New Exception("Length Of MCC Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(mcccode) > 0 Then
                        qry = "select count(*) from tspl_mcc_master where mcc_code='" + mcccode + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            Throw New Exception("MCC Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of MCC Master")
                        End If
                    End If

                    If clsCommon.myLen(VlcUploaderCode) <= 0 Then
                        Throw New Exception("Fill VLC Uploader Code At Line No. " + clsCommon.myCstr(counter))
                    End If
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_VLC_MASTER_HEAD where VLC_Code_vlc_uploader='" & VlcUploaderCode & "' and vlc_code<>'" & code & "'  and mcc='" & mcccode & "'", trans)) >= 1 Then
                        Throw New Exception("Duplicate VLC Uploader Code At Line No. " + clsCommon.myCstr(counter))
                    End If
                    '-------------------------------------------------

                    villcode = clsCommon.myCstr(grow.Cells("village code").Value).Replace("'", "`")
                    villname = clsCommon.myCstr(grow.Cells("village name").Value)
                    If clsCommon.myLen(villcode) <= 0 AndAlso clsCommon.myLen(villname) <= 0 Then
                        Throw New Exception("Fill Village Code/Village Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(villcode) > 30 Then
                        Throw New Exception("Length Of Village Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(villcode) > 0 Then
                        qry = "select count(*) from tspl_village_master where village_code='" + villcode + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            Throw New Exception("Village Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Village Master")
                        End If
                    End If


                    ''richa 
                    strPriceCode = clsCommon.myCstr(grow.Cells("Price Code").Value)
                    If clsCommon.myLen(strPriceCode) <= 0 Then '' must not be mandatory. it is mandatory on price creation
                        'Throw New Exception("Fill Price Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(strPriceCode) > 30 Then
                        Throw New Exception("Length Of Price Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(strPriceCode) > 0 Then
                        qry = "select count(*) from TSPL_FAT_SNF_UPLOADER_MASTER inner join  TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code='" & mcccode & "' and TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code where TSPL_FAT_SNF_UPLOADER_MASTER.Code='" & strPriceCode & "' and Isnull(TSPL_FAT_SNF_UPLOADER_MASTER.Is_InActive,0) =0"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            Throw New Exception("Price Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    ''---------------


                    Dim MilkReceiveUOM As String = clsCommon.myCstr(grow.Cells("Milk Receive UOM").Value).ToUpper()
                    If clsCommon.myLen(MilkReceiveUOM) > 0 Then
                        If Not (clsCommon.CompairString(MilkReceiveUOM, "KG") = CompairStringResult.Equal Or clsCommon.CompairString(MilkReceiveUOM, "LTR") = CompairStringResult.Equal) Then
                            Throw New Exception("Milk Receive UOM Should be KG or LTR At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If



                    Dim isSaved As Boolean = True
                    qry = "select count(*) from TSPL_VLC_MASTER_HEAD where vlc_code='" + code + "'"
                    check = clsDBFuncationality.getSingleValue(qry, trans)
                    If clsCommon.myLen(code) <= 0 Then
                        code = mcccode & "/" & clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), clsDocType.VLCMASTER, "", "")
                    End If
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "vlc_code", code)
                    clsCommon.AddColumnsForChange(coll, "vlc_name", name)
                    'clsCommon.AddColumnsForChange(coll, "vehical_name", vehical)
                    clsCommon.AddColumnsForChange(coll, "VSP_Code", vspcode)
                    clsCommon.AddColumnsForChange(coll, "village_code", villcode)
                    ' clsCommon.AddColumnsForChange(coll, "route_code", routecode)
                    clsCommon.AddColumnsForChange(coll, "MCC", mcccode)
                    clsCommon.AddColumnsForChange(coll, "Price_Code", strPriceCode, True)
                    clsCommon.AddColumnsForChange(coll, "Milk_Receive_UOM", MilkReceiveUOM, True)
                    If objCommonVar.PricePlan = 4 Then
                        clsCommon.AddColumnsForChange(coll, "Apply_Price_Chart_Uploader", IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Apply Price Chart Uploader(Y/N)").Value), "Y") = CompairStringResult.Equal, 1, 0))
                    End If
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                    If check <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                        '' done by Panch Raj against Ticket No:BM00000009815 on date 23/09/2016
                        If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AutoUpdateVLCUploaderCodeInVLCMaster, clsFixedParameterCode.AutoUpdateVLCUploaderCodeInVLCMaster, trans), "1") = CompairStringResult.Equal Then
                            VlcUploaderCode = clsfrmVLCMaster.GetCodeNumPart(code)
                            clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                        Else
                            clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                        End If
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommon.AddColumnsForChange(coll, "VLC_Code_VLC_Uploader", VlcUploaderCode)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Update, " TSPL_VLC_MASTER_HEAD.vlc_code='" + code + "'", trans)
                    End If
                    clsfrmVLCMaster.SaveVLCPriceCode(code, vspcode, mcccode, trans)
                    counter += 1
                    clsCommon.ProgressBarUpdate("Imported Receords  : " & counter & "/" & gv.Rows.Count)
                Next
                clsCommon.ProgressBarHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btngrid_im_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngrid_im.Click

        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "vlc_code", "village_code", "village_name") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try

                Dim code As String = "'"
                Dim villcode As String = ""
                Dim villname As String = ""
                Dim routecode As String = ""
                Dim routename As String = ""

                clsCommon.ProgressBarShow()
                Dim counter As Integer = 0
                Dim qry As String = ""
                Dim check As Integer = 0

                For Each grow As GridViewRowInfo In gv.Rows
                    counter += 1
                    code = clsCommon.myCstr(grow.Cells("vlc_code").Value)
                    If clsCommon.myLen(code) <= 0 Then
                        Throw New Exception("Fill VLC Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(code) > 30 Then
                        Throw New Exception("Length Of VLC Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(code) > 0 Then
                        qry = "select count(*) from TSPL_VLC_MASTER_HEAD where vlc_code='" + code + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            Throw New Exception("VLC Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of VLC Master(Head Part)")
                        End If
                    End If
                    '------------------------------------------------

                    villcode = clsCommon.myCstr(grow.Cells("village_code").Value).Replace("'", "`")
                    villname = clsCommon.myCstr(grow.Cells("village_name").Value)
                    If clsCommon.myLen(villcode) <= 0 AndAlso clsCommon.myLen(villname) <= 0 Then
                        Throw New Exception("Fill Village Code/Village Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(villcode) > 30 Then
                        Throw New Exception("Length Of Village Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(villcode) > 0 Then
                        qry = "select count(*) from tspl_village_master where village_code='" + villcode + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            Throw New Exception("Village Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Village Master")
                        End If
                    End If
                    If clsCommon.myLen(villname) > 0 Then
                        qry = "select village_code from tspl_village_master where village_name='" + villname + "'"
                        villcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                        If clsCommon.myLen(villcode) <= 0 Then
                            Throw New Exception("Vilage Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Village Master")
                        End If
                    End If

                    '-------------------------------

                    'routecode = clsCommon.myCstr(grow.Cells("route_code").Value)
                    'routename = clsCommon.myCstr(grow.Cells("route_name").Value)
                    'If clsCommon.myLen(routecode) <= 0 AndAlso clsCommon.myLen(routename) <= 0 Then
                    '    Throw New Exception("Fill Route Code/Route Name At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If
                    'If clsCommon.myLen(routecode) > 30 Then
                    '    Throw New Exception("Length Of Route Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If
                    'If clsCommon.myLen(routecode) > 0 Then
                    '    qry = "select count(*) from TSPL_MCC_ROUTE_MASTER where route_code='" + routecode + "' and village_code='" + villcode + "'"
                    '    check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                    '    If check <= 0 Then
                    '        Throw New Exception("Route Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Route Master And Mapped It With Correspondance State.")
                    '    End If
                    'End If
                    'If clsCommon.myLen(routename) > 0 Then
                    '    qry = "select route_code from TSPL_MCC_ROUTE_MASTER where route_name='" + routename + "' and village_code='" + villcode + "'"
                    '    routecode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                    '    If clsCommon.myLen(routecode) <= 0 Then
                    '        Throw New Exception("Route Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Route Master And Mapped It With Correspondance State.")
                    '    End If
                    'End If
                    '-------------------------------------------------
                    Dim isSaved As Boolean = True
                    qry = "delete from TSPL_VLC_MASTER_DETAIL where vlc_code='" + code + "' and village_code='" + villcode + "'" ' and route_code='" + routecode + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "vlc_code", code)
                    clsCommon.AddColumnsForChange(coll, "village_code", villcode)
                    'clsCommon.AddColumnsForChange(coll, "route_code", routecode)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))


                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_MASTER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    
                Next
                clsCommon.ProgressBarHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub txtvillcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtvillcode._MYValidating
        Dim qry As String = "select TSPL_VILLAGE_MASTER.Village_Code as Code,TSPL_VILLAGE_MASTER.Village_Name as [Village Name],(TSPL_VILLAGE_MASTER.Add1+' '+TSPL_VILLAGE_MASTER.Add2) as Address,TSPL_CITY_MASTER.City_Name as [City],TSPL_STATE_MASTER.STATE_NAME as [State],TSPL_VILLAGE_MASTER.PIN_NO as [Pin Code] from TSPL_VILLAGE_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_VILLAGE_MASTER.City_Code left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_VILLAGE_MASTER.State_Code"
        txtvillcode.Value = clsCommon.ShowSelectForm("VILFND", qry, "Code", "", txtvillcode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtvillcode.Value) > 0 Then
            txtvillname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select village_name from tspl_village_master where village_code='" + txtvillcode.Value + "'"))
        Else
            txtvillcode.Value = ""
            txtvillname.Text = ""
        End If
    End Sub

    Private Sub txtroutecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtroutecode._MYValidating
        OpenRoute()
    End Sub

    Private Sub fndPriceCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndPriceCode._MYValidating
        fndPriceCode.Value = clsfrmVLCMaster.getPriceCodeforVlc(fndvlccode.Value, fndMcc.Value, True, Nothing)
    End Sub

    Private Sub fndMcc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMcc._MYValidating
        Dim StrWhere As String = ""
        Dim qry As String = "select tspl_mcc_master.mcc_code as Code,tspl_mcc_master.mcc_name as Name,tspl_mcc_master.Plant_Code as [Plant Code],TSPL_LOCATION_MASTER.Location_Desc AS [Plant Name] from tspl_mcc_master left outer join tspl_location_master on tspl_location_master.location_code=tspl_mcc_master.plant_code"
        If clsCommon.myLen(arrLoc) > 0 Then
            StrWhere = " tspl_mcc_master.mcc_code in (" + arrLoc + ")"
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Before Doing VLC Master Entry,Make MCC Master", Me.Text)
            Reset()
        End If

        fndMcc.Value = clsCommon.ShowSelectForm("MCCFND", qry, "Code", StrWhere, fndMcc.Value, "Code", isButtonClicked)
    End Sub


End Class
