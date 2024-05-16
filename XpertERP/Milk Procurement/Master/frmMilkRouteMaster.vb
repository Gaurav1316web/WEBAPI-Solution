'----Created By Monika 12/06/2014--------------
'----------------BM00000003414
'' changed by Panch raj against Ticket:BM00000009821,BM00000009823
Imports common
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class FrmMilkRouteMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim arrLoc As String = Nothing
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim IsInsieLoadData As Boolean
    Dim No_of_Vlc As Integer = 0
    Dim Old_KM_Value As Double = 0.0
    Dim VLCTimeTableColumnShow As Boolean = False
    Dim VLCTimeTableColumnMandatory As Boolean = False
    Dim settApplyEffectiveStartDate As Boolean = False ''BHO/11/06/21-000003 by balwinder on 18/06/2021
    Dim settSeprateDistanceMorningEveningShift As Boolean = False
    Dim ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster As Boolean = False
    Const colSNO As String = "SNo"
    Const colDistance As String = "colDistance"
    Const colReachingTimeMorning As String = "colReachingTimeMorning"
    Const colReachingTimeEvening As String = "colReachingTimeEvening"
    Const colOutRoute As String = "colOutRoute"
    Const colOutRouteKM As String = "colOutRouteKM"
    Dim Prev As Integer = 0
#End Region

    Private Sub FrmMilkRouteMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S For Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        VLCTimeTableColumnShow = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VLCTimeTableColumnShow, clsFixedParameterCode.VLCTimeTableColumnShow, Nothing)) > 0, True, False)
        settSeprateDistanceMorningEveningShift = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SeprateDistanceMorningEveningShift, clsFixedParameterCode.SeprateDistanceMorningEveningShift, Nothing)) > 0, True, False)
        VLCTimeTableColumnMandatory = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VLCTimeTableColumnMandatory, clsFixedParameterCode.VLCTimeTableColumnMandatory, Nothing)) > 0, True, False)
        ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) > 0, True, False)
        settApplyEffectiveStartDate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyEffectiveStartDate, clsFixedParameterCode.ApplyEffectiveStartDate, Nothing)) > 0, True, False)
        txtEffectiveStartDate.Visible = settApplyEffectiveStartDate
        MyLabel5.Visible = settApplyEffectiveStartDate
        If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
            lblVehicle.Visible = True
            txtVehicle.Visible = True
        Else
            lblVehicle.Visible = False
            txtVehicle.Visible = False
        End If
        If settSeprateDistanceMorningEveningShift Then
            pnlMEDistance.Visible = True
        End If
        Reset()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMilkRouteMaster)
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

    Sub Reset()
        chkSelfRoute.Checked = False
        chkInActive.Checked = False
        txt_km.Text = ""
        txt_km_Morning.Text = ""
        txt_km_Evening.Text = ""
        fndcode.Value = ""
        txtdesc.Text = ""
        txtShortDescription.Text = ""
        txtmcccode.Value = ""
        txtmccname.Text = ""
        txtvehicleno.Value = ""
        txtvehiclename.Text = ""
        txtSuperVisorName.Text = ""
        fndSuperVisorCode.Value = ""
        txtcontactno.Text = ""
        IsInsieLoadData = False
        gvVLC.Rows.Clear()
        gvVLC.Columns.Clear()
        'txtadd1.Text = ""
        'txtadd2.Text = ""
        'txtadd3.Text = ""
        txtcountrycode.Value = ""
        txtcountryname.Text = ""
        txtstate.Value = ""
        txtstatename.Text = ""
        txtcity.Value = ""
        txtcityname.Text = ""
        txtemail.Text = ""

        txtReachingTimeM.Value = clsCommon.GETSERVERDATE()
        txtReachingTimeE.Value = txtReachingTimeM.Value
        txteffectivedate.Text = txtReachingTimeM.Value
        txtLastVLCToMCCDis.Value = 0
        txtVehicle.Text = ""
        txtEffectiveStartDate.Checked = False
        txtEffectiveStartDate.Value = txtReachingTimeM.Value

        btnsave.Text = "Save"
        btndelete.Enabled = False
        fndcode.MyReadOnly = False
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        isNewEntry = True
        LoadBlankVLC_Grid()
        MCCLOCATIONFINDER()
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
            UcCustomFields1.SetDefaultValues()
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub



    Function AllowToSave() As Boolean
        Try

            If clsCommon.myLen(txtdesc.Text) <= 0 Then
                txtdesc.Focus()
                txtdesc.Select()
                Errorcontrol.SetError(txtdesc, "Please Fill Route Name")
                Throw New Exception("Please Fill Route Name")
            Else
                Errorcontrol.ResetError(txtdesc)
            End If

            If clsCommon.myLen(txtvehicleno.Value) <= 0 Then
                txtvehicleno.Focus()
                txtvehicleno.Select()
                Errorcontrol.SetError(txtvehiclename, "Please Select Vehicle Detail")
                Throw New Exception("Please Select Vehicle Detail")
            Else
                Errorcontrol.ResetError(txtvehiclename)
            End If

            If clsCommon.myLen(txtmcccode.Value) <= 0 Then
                txtmcccode.Focus()
                txtmcccode.Select()
                Errorcontrol.SetError(txtmccname, "Please Select MCC Detail")
                Throw New Exception("Please Select MCC Detail")
            Else
                Errorcontrol.ResetError(txtmccname)
            End If

            If Not VLCTimeTableColumnMandatory Then
                If clsCommon.myLen(txt_km.Text) <= 0 Then
                    txt_km.Focus()
                    txt_km.Select()
                    Errorcontrol.SetError(txt_km, "Please Fill Distance(K.M.)")
                    Throw New Exception("Please Fill Distance(K.M.)")
                Else
                    Errorcontrol.ResetError(txt_km)
                End If
            End If
            If settSeprateDistanceMorningEveningShift Then
                If clsCommon.myLen(txt_km_Morning.Value) <= 0 Then
                    txt_km_Morning.Focus()
                    txt_km_Morning.Select()
                    Errorcontrol.SetError(txt_km_Morning, "Please Fill Morning Distance(K.M.)")
                    Throw New Exception("Please Fill Morning Distance(K.M.)")
                Else
                    Errorcontrol.ResetError(txt_km_Morning)
                End If
                If clsCommon.myLen(txt_km_Evening.Value) <= 0 Then
                    txt_km_Evening.Focus()
                    txt_km_Evening.Select()
                    Errorcontrol.SetError(txt_km_Evening, "Please Fill Evening Distance(K.M.)")
                    Throw New Exception("Please Fill Evening Distance(K.M.)")
                Else
                    Errorcontrol.ResetError(txt_km_Evening)
                End If
            End If


            If clsCommon.myLen(fndSuperVisorCode.Value) <= 0 Then
                fndSuperVisorCode.Focus()
                fndSuperVisorCode.Select()
                Errorcontrol.SetError(fndSuperVisorCode, "Please Select Supervisor ")
                Throw New Exception("Please Select Supervisor ")
            Else
                Errorcontrol.ResetError(fndSuperVisorCode)
            End If

            'If clsCommon.myLen(txtcountrycode.Value) > 0 AndAlso clsCommon.myLen(txtstate.Value) <= 0 Then
            '    txtstate.Focus()
            '    txtstate.Select()
            '    Errorcontrol.SetError(txtstatename, "Please Fill State")
            '    Throw New Exception("Please Fill State")
            'Else
            '    Errorcontrol.ResetError(txtstatename)
            'End If

            'If clsCommon.myLen(txtcountrycode.Value) > 0 AndAlso clsCommon.myLen(txtstate.Value) > 0 AndAlso clsCommon.myLen(txtcity.Value) <= 0 Then
            '    txtcity.Focus()
            '    txtcity.Select()
            '    Errorcontrol.SetError(txtcityname, "Please Fill City")
            '    Throw New Exception("Please Fill City")
            'Else
            '    Errorcontrol.ResetError(txtcityname)
            'End If

            Dim arrlst As New ArrayList
            For Each Row As GridViewRowInfo In gvVLC.Rows
                If arrlst.Contains(Row.Cells("ColVlc_Code").Value) Then
                    Throw New Exception("Repeated VLC [" + clsCommon.myCstr(Row.Cells("ColVlc_Code").Value) + "] at Row No [" + clsCommon.myCstr(Row.Index + 1) + "]")
                Else
                    arrlst.Add(clsCommon.myCstr(Row.Cells("ColVlc_Code").Value))
                End If
            Next
            If arrlst.Count <> gvVLC.Rows.Count Then
                Errorcontrol.SetError(gvVLC, "Please Fill Unique VLC")
                Throw New Exception("Please Fill Unique VLC")
            End If
            If clsCommon.myCdbl(gvVLC.Rows.Count) <> clsCommon.myCdbl(No_of_Vlc) Then
                If clsCommon.CompairString(Old_KM_Value, txt_km.Text) = CompairStringResult.Equal Then
                    'Errorcontrol.SetError(txt_km, "Please Update Distance.VLC Data Is Changed")
                    If clsCommon.MyMessageBoxShow(Me, "Please Update Distance.VLC Data Is Changed." & Environment.NewLine _
                                                   & "            Do you want To change", "Message", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        Errorcontrol.SetError(txt_km, "Please Update Distance.VLC Data Is Changed")
                        Return False
                    End If
                End If
            End If
            UcCustomFields1.AllowToSave()

            If settApplyEffectiveStartDate Then
                If Not txtEffectiveStartDate.Checked Then
                    txtEffectiveStartDate.Focus()
                    txtEffectiveStartDate.Select()
                    Errorcontrol.SetError(txtEffectiveStartDate, "Please Select Effective Start Date")
                    Throw New Exception("Please Select Effective Start Date")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    
    Sub SaveData()
        Try
            If AllowToSave() Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmMilkRouteMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsfrmMilkRouteMaster()
                obj.code = clsCommon.myCstr(fndcode.Value)
                obj.desc = clsCommon.myCstr(txtdesc.Text)
                obj.Short_Description = txtShortDescription.Text
                obj.vehiclecode = clsCommon.myCstr(txtvehicleno.Value)
                obj.vehiclename = clsCommon.myCstr(txtvehiclename.Text)
                obj.mcccode = clsCommon.myCstr(txtmcccode.Value)
                obj.mccname = clsCommon.myCstr(txtmccname.Text)
                obj.supervisorname = clsCommon.myCstr(fndSuperVisorCode.Value)
                obj.contactno = clsCommon.myCstr(txtcontactno.Text)
                obj.countrycode = clsCommon.myCstr(txtcountrycode.Value)
                obj.statecode = clsCommon.myCstr(txtstate.Value)
                obj.citycode = clsCommon.myCstr(txtcity.Value)
                obj.email = clsCommon.myCstr(txtemail.Text)
                obj.kilometer = clsCommon.myCdbl(txt_km.Text)

                obj.kilometer_Morning = clsCommon.myCdbl(txt_km_Morning.Text)
                obj.kilometer_Evening = clsCommon.myCdbl(txt_km_Evening.Text)

                obj.effectivedate = clsCommon.myCstr(txteffectivedate.Text)
                obj.Active = IIf(Me.chkInActive.Checked, 0, 1)
                obj.Self_Route = chkSelfRoute.Checked
                obj.MCC_Reaching_Time_M = txtReachingTimeM.Value
                obj.MCC_Reaching_Time_E = txtReachingTimeE.Value
                obj.Last_VLC_To_MCC_Distance = txtLastVLCToMCCDis.Value

                If clsCommon.myLen(obj.effectivedate) <= 0 Then
                    clsCommon.GETSERVERDATE().ToString("dd/MM/yyyy")
                End If
                If txtEffectiveStartDate.Checked Then
                    obj.Effective_Start_Date = txtEffectiveStartDate.Value
                End If
                Dim objgenset As New cls_VLC_Route_Detail
                clsfrmMilkRouteMaster.arr_VLC_Detail = New List(Of cls_VLC_Route_Detail)
                Dim dblTotalKM As Double = 0
                Dim arrSNO As New List(Of Integer)
                For irow As Integer = 0 To gvVLC.Rows.Count - 1

                    objgenset = New cls_VLC_Route_Detail
                    objgenset.VLC_CODE = clsCommon.myCstr(gvVLC.Rows(irow).Cells("COLVLC_Code").Value)
                    objgenset.Route_CODE = clsCommon.myCstr(fndcode.Value)

                    objgenset.SNo = clsCommon.myCdbl(gvVLC.Rows(irow).Cells(colSNO).Value)
                    objgenset.Distance = clsCommon.myCdbl(gvVLC.Rows(irow).Cells(colDistance).Value)
                    objgenset.Out_Route = clsCommon.myCBool(gvVLC.Rows(irow).Cells(colOutRoute).Value)
                    objgenset.Out_Route_KM = clsCommon.myCdbl(gvVLC.Rows(irow).Cells(colOutRouteKM).Value)
                    If VLCTimeTableColumnMandatory Then
                        If objgenset.SNo <= 0 Then
                            gvVLC.CurrentRow = gvVLC.Rows(irow)
                            gvVLC.CurrentColumn = gvVLC.Columns(colSNO)
                            Throw New Exception("Please provide Sequence No Of DCS " + objgenset.VLC_CODE)
                        End If
                        Dim flag As Boolean = (objgenset.Distance <= 0)
                        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHBA") = CompairStringResult.Equal Then
                            flag = (objgenset.Distance < 0)
                        End If
                        If flag Then
                            gvVLC.CurrentRow = gvVLC.Rows(irow)
                            gvVLC.CurrentColumn = gvVLC.Columns(colDistance)
                            Throw New Exception("Please provide Distance Of DCS " + objgenset.VLC_CODE)
                        End If
                        If arrSNO.Contains(objgenset.SNo) Then
                            Throw New Exception("Same Serial No Repeated Of DCS " + objgenset.VLC_CODE)
                        Else
                            arrSNO.Add(objgenset.SNo)
                        End If

                        dblTotalKM += objgenset.Distance

                    End If
                    If clsCommon.myLen(gvVLC.Rows(irow).Cells(colReachingTimeMorning).Value) > 0 Then
                        objgenset.Mor_Mik_Coll = clsCommon.myCDate(gvVLC.Rows(irow).Cells(colReachingTimeMorning).Value)
                    ElseIf VLCTimeTableColumnMandatory Then
                        gvVLC.CurrentRow = gvVLC.Rows(irow)
                        gvVLC.CurrentColumn = gvVLC.Columns(colReachingTimeMorning)
                        Throw New Exception("Please provide morning reaching time Of DCS " + objgenset.VLC_CODE)
                    End If
                    If clsCommon.myLen(gvVLC.Rows(irow).Cells(colReachingTimeEvening).Value) > 0 Then
                        objgenset.Eve_Milk_Coll = clsCommon.myCDate(gvVLC.Rows(irow).Cells(colReachingTimeEvening).Value)
                    ElseIf VLCTimeTableColumnMandatory Then
                        gvVLC.CurrentRow = gvVLC.Rows(irow)
                        gvVLC.CurrentColumn = gvVLC.Columns(colReachingTimeEvening)
                        Throw New Exception("Please provide evening reaching time Of DCS " + objgenset.VLC_CODE)
                    End If
                    clsfrmMilkRouteMaster.arr_VLC_Detail.Add(objgenset)
                Next
                If dblTotalKM > 0 Then
                    obj.kilometer = dblTotalKM + txtLastVLCToMCCDis.Value
                End If
                arrSNO = Nothing
                'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields
                If clsfrmMilkRouteMaster.SaveData(obj.code, obj, isNewEntry) Then
                    UcAttachment1.SaveData(fndcode.Value)
                    If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                    End If
                    LoadData(obj.code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If clsCommon.myLen(fndcode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Route Code For Deletion", Me.Text)
            fndcode.Focus()
            fndcode.Select()
            Errorcontrol.SetError(fndcode, "Please Select Route Code For Deletion")
            Return
        Else
            Errorcontrol.ResetError(fndcode)
        End If

        Dim qry As String = "Select count(*) from tspl_mcc_route_master where route_code='" + fndcode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found For Deletion", Me.Text)
            Return
        End If

        If Not clsCommon.MyMessageBoxShow(Me, "Are You Sure,Want To Delete The Route Master Of Route Code " + fndcode.Value + "?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            qry = "delete from tspl_mcc_route_master where route_code='" + fndcode.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
            trans.Commit()
            Reset()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            No_of_Vlc = 0
            IsInsieLoadData = True
            Dim obj As clsfrmMilkRouteMaster = clsfrmMilkRouteMaster.GetData(strCode, arrLoc, NavType)
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.code) > 0 Then
                isNewEntry = False
                fndcode.Value = obj.code
                txtdesc.Text = obj.desc
                txtShortDescription.Text = obj.Short_Description
                txtvehicleno.Value = obj.vehiclecode
                txtvehiclename.Text = obj.vehiclename
                txtVehicle.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vehicle from TSPL_Primary_Vehicle_Master where vehicle_Code='" & txtvehicleno.Value & "'"))
                txtmcccode.Value = obj.mcccode
                txtmccname.Text = obj.mccname
                fndSuperVisorCode.Value = obj.supervisorname
                txtSuperVisorName.Text = clsEmployeeMaster.GetName(fndSuperVisorCode.Value, Nothing)
                txtcontactno.Text = obj.contactno
                txtcountrycode.Value = obj.countrycode
                txtcountryname.Text = obj.countryname
                txtstate.Value = obj.statecode
                txtstatename.Text = obj.statename
                txtcity.Value = obj.citycode
                txtcityname.Text = obj.cityname
                txtemail.Text = obj.email
                txt_km.Text = obj.kilometer
                Old_KM_Value = txt_km.Text

                txt_km_Morning.Value = obj.kilometer_Morning
                txt_km_Evening.Value = obj.kilometer_Evening

                txteffectivedate.Text = obj.effectivedate
                If obj.MCC_Reaching_Time_M IsNot Nothing Then
                    txtReachingTimeM.Value = obj.MCC_Reaching_Time_M
                End If
                If obj.MCC_Reaching_Time_E IsNot Nothing Then
                    txtReachingTimeE.Value = obj.MCC_Reaching_Time_E
                End If
                txtLastVLCToMCCDis.Value = obj.Last_VLC_To_MCC_Distance
                If obj.Effective_Start_Date IsNot Nothing Then
                    txtEffectiveStartDate.Checked = True
                    txtEffectiveStartDate.Value = obj.Effective_Start_Date
                Else
                    txtEffectiveStartDate.Checked = False
                End If
                Me.chkInActive.Checked = IIf(obj.Active = 0, True, False)
                chkSelfRoute.Checked = obj.Self_Route
                btnsave.Text = "Update"
                btndelete.Enabled = True
                fndcode.MyReadOnly = True
                LoadBlankVLC_Grid()
                If clsfrmMilkRouteMaster.arr_VLC_Detail.Count > 0 Then
                    For i As Integer = 0 To clsfrmMilkRouteMaster.arr_VLC_Detail.Count - 1
                        gvVLC.Rows.Add(i + 1, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).VLC_Uploader_code, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).VLC_CODE, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).VLC_NAME, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).VSP_CODE, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).VSP_NAME, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).Status, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).Opening_Date, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).Closing_Date, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).VEHICLE_NAME, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).Distance, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).Mor_Mik_Coll, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).Eve_Milk_Coll, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).Out_Route, clsfrmMilkRouteMaster.arr_VLC_Detail.Item(i).Out_Route_KM)
                        No_of_Vlc += 1
                    Next
                End If
                UcAttachment1.LoadData(fndcode.Value)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.BlankAllControls()
                    UcCustomFields1.LoadData(obj.code)
                End If
            Else
                Reset()
            End If
            IsInsieLoadData = False
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankVLC_Grid()
        Try
            gvVLC.Rows.Clear()
            gvVLC.Columns.Clear()

            Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoLineNo.HeaderText = "SNo"
            repoLineNo.Name = colSNO
            repoLineNo.Width = 50
            repoLineNo.ShowUpDownButtons = False
            repoLineNo.Step = 0
            repoLineNo.Minimum = 0
            repoLineNo.FormatString = "{0:n0}"
            repoLineNo.DecimalPlaces = 0
            repoLineNo.ReadOnly = False
            repoLineNo.IsVisible = VLCTimeTableColumnShow
            repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gvVLC.MasterTemplate.Columns.Add(repoLineNo)
            gvVLC.Columns.Add("COLUploaderCode", "Uploader Code")
            gvVLC.Columns.Add("COLVLC_Code", "DCS Code")
            gvVLC.Columns.Add("COLVLC_NAME", "DCS Name")
            gvVLC.Columns.Add("COLVSP_Code", "Secretary Code")
            gvVLC.Columns.Add("COLVSP_NAME", "Secretary Name")
            gvVLC.Columns.Add("COLStatus", "Status")
            gvVLC.Columns.Add("COLOpeningDate", "Opening Date")
            gvVLC.Columns.Add("COLClosingDate", "Closing Date")
            ' gvVLC.Columns.Add("COLVehicle_Code", "Vehicle Code")
            gvVLC.Columns.Add("COLVehicle_NAME", "Vehicle Name")

            gvVLC.Columns("COLUploaderCode").Width = 100
            gvVLC.Columns("COLVLC_Code").Width = 150
            gvVLC.Columns("COLVLC_NAME").Width = 150
            gvVLC.Columns("COLVSP_Code").Width = 150
            gvVLC.Columns("COLVSP_NAME").Width = 150
            gvVLC.Columns("COLOpeningDate").Width = 100
            gvVLC.Columns("COLClosingDate").Width = 100
            'gvVLC.Columns("COLVehicle_Code").Width = 150
            gvVLC.Columns("COLVehicle_NAME").Width = 0
            gvVLC.Columns("COLStatus").Width = 100

            gvVLC.Columns("COLVLC_NAME").ReadOnly = True
            gvVLC.Columns("COLVSP_Code").ReadOnly = True
            gvVLC.Columns("COLVSP_NAME").ReadOnly = True
            'gvVLC.Columns("COLVehicle_Code").ReadOnly = True
            gvVLC.Columns("COLVehicle_NAME").ReadOnly = True
            gvVLC.Columns("COLVehicle_NAME").IsVisible = False
            gvVLC.Columns("COLStatus").ReadOnly = True
            gvVLC.Columns("COLOpeningDate").ReadOnly = True
            gvVLC.Columns("COLClosingDate").ReadOnly = True



            repoLineNo = New GridViewDecimalColumn()
            repoLineNo.HeaderText = "Distance"
            repoLineNo.Name = colDistance
            repoLineNo.Width = 50
            repoLineNo.ShowUpDownButtons = False
            repoLineNo.Step = 0
            repoLineNo.Minimum = 0
            repoLineNo.FormatString = "{0:n0}"
            repoLineNo.DecimalPlaces = 1
            repoLineNo.ReadOnly = False
            repoLineNo.IsVisible = VLCTimeTableColumnShow
            repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gvVLC.MasterTemplate.Columns.Add(repoLineNo)

            Dim repoDT As GridViewDateTimeColumn = New GridViewDateTimeColumn()
            repoDT.Format = DateTimePickerFormat.Custom
            repoDT.HeaderText = "Reaching Time Morning "
            repoDT.CustomFormat = "hh:mm tt"
            repoDT.FormatString = "{0:hh:mm tt}"
            repoDT.Name = colReachingTimeMorning
            repoDT.Width = 100
            repoDT.EditorType = GridViewDateTimeEditorType.TimePicker
            repoDT.ReadOnly = False
            repoDT.IsVisible = VLCTimeTableColumnShow
            gvVLC.MasterTemplate.Columns.Add(repoDT)

            repoDT = New GridViewDateTimeColumn()
            repoDT.Format = DateTimePickerFormat.Custom
            repoDT.HeaderText = "Reaching Time Evening"
            repoDT.CustomFormat = "hh:mm tt"
            repoDT.FormatString = "{0:hh:mm tt}"
            repoDT.Name = colReachingTimeEvening
            repoDT.Width = 100
            repoDT.EditorType = GridViewDateTimeEditorType.TimePicker
            repoDT.ReadOnly = False
            repoDT.IsVisible = VLCTimeTableColumnShow
            gvVLC.MasterTemplate.Columns.Add(repoDT)

            Dim repoCheckBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoCheckBox.HeaderText = "Out Route"
            repoCheckBox.Name = colOutRoute
            repoCheckBox.ReadOnly = False
            repoCheckBox.IsVisible = True
            repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gvVLC.MasterTemplate.Columns.Add(repoCheckBox)

            repoLineNo = New GridViewDecimalColumn()
            repoLineNo.HeaderText = "Out Route KM"
            repoLineNo.Name = colOutRouteKM
            repoLineNo.Width = 50
            repoLineNo.ShowUpDownButtons = False
            repoLineNo.Step = 0
            repoLineNo.Minimum = 0
            repoLineNo.FormatString = "{0:n0}"
            repoLineNo.DecimalPlaces = 1
            repoLineNo.ReadOnly = False
            repoLineNo.IsVisible = True
            repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gvVLC.MasterTemplate.Columns.Add(repoLineNo)

            gvVLC.AllowAddNewRow = True
            gvVLC.MasterTemplate.AddNewRowPosition = SystemRowPosition.Bottom
            gvVLC.AllowEditRow = True
            gvVLC.AllowDeleteRow = True
            gvVLC.AllowRowResize = False
            gvVLC.EnableSorting = False
            gvVLC.AllowRowReorder = False
            gvVLC.AllowColumnResize = True
            gvVLC.AllowColumnChooser = False
            gvVLC.AllowAutoSizeColumns = True
            gvVLC.ShowGroupPanel = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub fndcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndcode._MYNavigator
        LoadData(clsCommon.myCstr(fndcode.Value), NavType)
    End Sub

    Private Sub fndcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcode._MYValidating
        Dim whrclas As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrclas = " tspl_mcc_route_master.mcc_code in (" + arrLoc + ")"
        End If

        Dim qry As String = "select count(*) from tspl_mcc_route_master where route_code='" + fndcode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            fndcode.MyReadOnly = True
        Else
            fndcode.MyReadOnly = False
        End If

        If fndcode.MyReadOnly Or isButtonClicked Then
            fndcode.Value = clsfrmMilkRouteMaster.getFinder(whrclas, fndcode.Value, isButtonClicked)

            If clsCommon.myLen(fndcode.Value) > 0 Then
                LoadData(fndcode.Value, NavigatorType.Current)
            End If
        End If
    End Sub

    Private Sub txtmcccode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtmcccode._MYValidating

        'Dim qry As String = "select tspl_location_master.location_category from tspl_user_master left outer join tspl_location_master on tspl_location_master.location_code=tspl_user_master.default_location where tspl_user_master.user_code='" + objCommonVar.CurrentUserCode + "'"
        'Dim value As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        'If clsCommon.CompairString(value, "MCC") = CompairStringResult.Equal Then
        '    txtmcccode.Value = clsMccMaster.getFinder(" created_by='" + objCommonVar.CurrentUserCode + "'", txtmcccode.Value, isButtonClicked)
        'Else
        Dim whrCls As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrCls = " TSPL_MCC_MASTER.mcc_code in (" + arrLoc + ")"
        End If

        txtmcccode.Value = clsMccMaster.getFinder(whrCls, txtmcccode.Value, isButtonClicked)
        'End If

        If clsCommon.myLen(txtmcccode.Value) > 0 Then
            txtmccname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_code='" + txtmcccode.Value + "'"))
            txtvehiclename.Text = ""
            txtvehicleno.Value = ""
        Else
            txtmccname.Text = ""
            txtvehiclename.Text = ""
            txtvehicleno.Value = ""
        End If
    End Sub

    Private Sub txtvehicleno__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtvehicleno._MYValidating
        If clsCommon.myLen(txtmcccode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select MCC First", Me.Text)
            txtmcccode.Focus()
            txtmcccode.Select()
            Errorcontrol.SetError(txtmccname, "Please Select MCC First")
            Return
        Else
            Errorcontrol.ResetError(txtmccname)
        End If

        Dim qry As String = "select TSPL_Primary_Vehicle_Master.vehicle_code as Code,TSPL_Primary_Vehicle_Master.Description,TSPL_Primary_Vehicle_Master.vendor_code as [Primary Transporter Code],tspl_vendor_master.vendor_name as [Primary Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code as [MCC Code],tspl_mcc_master.mcc_name as [MCC Name],TSPL_Primary_Vehicle_Master.storage_capacity as [Storage Capacity],TSPL_Primary_Vehicle_Master.manufacturing_year as [Manufacturing Year],TSPL_Primary_Vehicle_Master.price_km as [Price Per KM],TSPL_Primary_Vehicle_Master.price_ltr as [Price Per Ltr] "
        If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
            qry += " ,TSPL_Primary_Vehicle_Master.Vehicle"
        End If
        qry += " from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code" ',TSPL_Primary_Vehicle_Master.price_head_load as [Price Head Load]
        txtvehicleno.Value = clsCommon.ShowSelectForm("PTVFND1", qry, "Code", " TSPL_Primary_Vehicle_Master.mcc_code='" + txtmcccode.Value + "'", txtvehicleno.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtvehicleno.Value) > 0 Then
            txtvehiclename.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_Primary_Vehicle_Master where vehicle_code='" + txtvehicleno.Value + "'"))
            txtVehicle.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vehicle from TSPL_Primary_Vehicle_Master where vehicle_Code='" & txtvehicleno.Value & "'"))
        Else
            txtvehiclename.Text = ""
            txtVehicle.Text = ""
        End If
    End Sub

    Private Sub txtcountrycode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcountrycode._MYValidating
        Dim qry As String = "Select country_code as Code,country_name as [Country Name],created_by as [Created By],created_date as [Created Date],modified_by as [Modified By],modified_date as [Modified Date] from tspl_country_master"
        txtcountrycode.Value = clsCommon.ShowSelectForm("CNTFND", qry, "Code", "", txtcountrycode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtcountrycode.Value) > 0 Then
            txtcountryname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select country_name from tspl_country_master where country_code='" + txtcountrycode.Value + "'"))
        Else
            txtcountryname.Text = ""
            txtstate.Value = ""
            txtstatename.Text = ""
            txtcity.Value = ""
            txtcityname.Text = ""
        End If
    End Sub

    Private Sub txtstate__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtstate._MYValidating
        If clsCommon.myLen(txtcountrycode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Country First", Me.Text)
            txtcountrycode.Focus()
            txtcountrycode.Select()
            Errorcontrol.SetError(txtcountryname, "Please Select Country First")
            Return
        Else
            Errorcontrol.ResetError(txtcountryname)
        End If

        Dim qry As String = "select tspl_state_master.state_code as Code,tspl_state_master.state_name as [State Name],tspl_state_master.country_code as [Country Code],tspl_country_master.country_name as [Country Name] from tspl_state_master left outer join tspl_country_master on tspl_country_master.country_code=tspl_state_master.country_code"
        txtstate.Value = clsCommon.ShowSelectForm("STAFND", qry, "Code", " tspl_state_master.country_code='" + txtcountrycode.Value + "'", txtstate.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtstate.Value) > 0 Then
            txtstatename.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select state_name from tspl_state_master where country_code='" + txtcountrycode.Value + "' and state_code='" + txtstate.Value + "'"))
        Else
            txtstatename.Text = ""
            txtcity.Value = ""
            txtcityname.Text = ""
        End If
    End Sub

    Private Sub txtcity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcity._MYValidating
        If clsCommon.myLen(txtstate.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select State First", Me.Text)
            txtstate.Focus()
            txtstate.Select()
            Errorcontrol.SetError(txtstatename, "Please Select State First")
            Return
        Else
            Errorcontrol.ResetError(txtstatename)
        End If

        Dim qry As String = "select TSPL_CITY_MASTER.City_Code as Code,TSPL_CITY_MASTER.City_Name as [City Name],TSPL_CITY_MASTER.STATE_CODE as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name] from TSPL_CITY_MASTER left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_CITY_MASTER.STATE_CODE and TSPL_STATE_MASTER.COUNTRY_CODE='" + txtcountrycode.Value + "'"
        txtcity.Value = clsCommon.ShowSelectForm("CITYFND", qry, "Code", " TSPL_CITY_MASTER.STATE_CODE='" + txtstate.Value + "'", txtcity.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtcity) > 0 Then
            txtcityname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select city_name from tspl_city_master where state_code='" + txtstate.Value + "'"))
        Else
            txtcityname.Text = ""
        End If
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        'Dim qry As String = "select count(*) from tspl_mcc_route_master"
        'Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        'If check > 0 Then
        '    qry = "select TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Name],TSPL_MCC_ROUTE_MASTER.vehicle_code as [Vehicle Code],TSPL_MCC_ROUTE_MASTER.mcc_code as [MCC Code],tspl_mcc_master.mcc_name as [MCC Name],TSPL_MCC_ROUTE_MASTER.KiloMeter,TSPL_MCC_ROUTE_MASTER.supervisor_name as [Supervisor Code],TSPL_MCC_ROUTE_MASTER.contact_no as [Contact No],TSPL_MCC_ROUTE_MASTER.email_id as [Email ID],TSPL_MCC_ROUTE_MASTER.effective_date as [Effective Date] from TSPL_MCC_ROUTE_MASTER left outer join tspl_mcc_master on Tspl_mcc_master.mcc_code=TSPL_MCC_ROUTE_MASTER.mcc_code left outer join tspl_country_master on TSPL_MCC_ROUTE_MASTER.country_code=tspl_country_master.country_code left outer join tspl_state_master on tspl_state_master.state_code=TSPL_MCC_ROUTE_MASTER.state_code left outer join tspl_city_master on tspl_city_master.city_code=TSPL_MCC_ROUTE_MASTER.city_code"
        '    ''Removed
        '    ''''',TSPL_MCC_ROUTE_MASTER.add1 as Address1,TSPL_MCC_ROUTE_MASTER.add2 as Address2,TSPL_MCC_ROUTE_MASTER.add3 as Address3
        '    ''''',TSPL_MCC_ROUTE_MASTER.country_code as [Country Code],tspl_country_master.country_name as [Country Name],TSPL_MCC_ROUTE_MASTER.state_code as [State Code],tspl_state_master.state_name as [State Name],TSPL_MCC_ROUTE_MASTER.city_code as [City Code],tspl_city_master.city_name as [City Name],
        'Else
        '    qry = "select '' as Code,'' as [Route Name],'' as [Vehicle Code],'' as [MCC Code],'' as [MCC Name],'' as KiloMeter,'' as [Supervisor Code],'' as [Contact No],'' as [Email ID],'' as [Effective Date]"
        '    '''' Removed
        '    '''''''' as Address1,'' as Address2,'' as Address3,
        '    '''''''' as [Country Code],'' as [Country Name],'' as [State Code],'' as [State Name],'' as [City Code],'' as [City Name],
        'End If

        'transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        'Dim gv As New RadGridView()
        'Me.Controls.Add(gv)
        'Dim currentdate As Date = Date.Today

        'If transportSql.importExcel(gv, "Code", "Route Name", "Vehicle Code", "MCC Code", "MCC Name", "KiloMeter", "Supervisor Code", "Contact No", "Email ID", "Effective Date") Then

        '    ''Removed
        '    ''''' "Address1", "Address2", "Address3",
        '    '''', "Country Code", "Country Name", "State Code", "State Name", "City Code", "City Name"
        '    Try
        '        clsCommon.ProgressBarShow()
        '        Dim obj As clsfrmMilkRouteMaster
        '        Dim counter As Integer = 1

        '        For Each grow As GridViewRowInfo In gv.Rows
        '            obj = New clsfrmMilkRouteMaster()

        '            obj.code = clsCommon.myCstr(grow.Cells("Code").Value)
        '            If clsCommon.myLen(obj.code) <= 0 Or clsCommon.myLen(obj.code) > 30 Then
        '                Throw New Exception("Please Fill Route Code(Max. 30 Characters) At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If
        '            '-----------------------------

        '            obj.desc = clsCommon.myCstr(grow.Cells("Route Name").Value)
        '            If clsCommon.myLen(obj.desc) <= 0 Or clsCommon.myLen(obj.desc) > 150 Then
        '                Throw New Exception("Please Fill Route Name(Max. 150 Characters) At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If
        '            '--------------------------------

        '            Dim qry As String = ""
        '            Dim check As Integer = 0

        '            obj.vehiclecode = clsCommon.myCstr(grow.Cells("vehicle code").Value)
        '            'obj.vehiclename = clsCommon.myCstr(grow.Cells("vehicle name").Value)
        '            If clsCommon.myLen(obj.vehiclecode) <= 0 Then
        '                Throw New Exception("Please Fill Vehicle Details At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If
        '            If clsCommon.myLen(obj.vehiclecode) > 0 Then
        '                qry = "select count(*) from tspl_primary_vehicle_master where vehicle_code='" + obj.vehiclecode + "'"
        '                check = clsDBFuncationality.getSingleValue(qry)

        '                If check <= 0 Then
        '                    '        qry = "select count(*) from tspl_primary_vehicle_master where description='" + obj.vehiclename + "'"
        '                    '        check = clsDBFuncationality.getSingleValue(qry)

        '                    '        If check <= 0 Then
        '                    '            obj.vehiclecode = ""
        '                    '            Throw New Exception("Filled Vehicle Details Is Invlaid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
        '                    '        End If
        '                    '    End If
        '                    'End If
        '                    'If clsCommon.myLen(obj.vehiclecode) <= 0 Then
        '                    '    qry = "select count(*) from tspl_primary_vehicle_master where description='" + obj.vehiclename + "'"
        '                    '    check = clsDBFuncationality.getSingleValue(qry)

        '                    '    If check <= 0 Then
        '                    Throw New Exception("Filled Vehicle Code Is Invlaid Or Does Not Exist in Master,See At Line No. " + clsCommon.myCstr(counter) + "")
        '                End If
        '            End If
        '            ''-----------------------------------

        '            obj.mcccode = clsCommon.myCstr(grow.Cells("MCC Code").Value)
        '            obj.mccname = clsCommon.myCstr(grow.Cells("MCC Name").Value)

        '            If clsCommon.myLen(obj.mcccode) <= 0 AndAlso clsCommon.myLen(obj.mccname) <= 0 Then
        '                Throw New Exception("Please Fill MCC Details At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If
        '            If clsCommon.myLen(obj.mcccode) > 0 Then
        '                qry = "select count(*) from tspl_mcc_master where mcc_code='" + obj.mcccode + "'"
        '                check = clsDBFuncationality.getSingleValue(qry)

        '                If check <= 0 Then
        '                    qry = "select count(*) from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
        '                    check = clsDBFuncationality.getSingleValue(qry)

        '                    If check <= 0 Then
        '                        obj.mcccode = ""
        '                        Throw New Exception("Filled MCC Details Is Invlaid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
        '                    End If
        '                End If
        '            End If
        '            If clsCommon.myLen(obj.mcccode) <= 0 Then
        '                qry = "select count(*) from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
        '                check = clsDBFuncationality.getSingleValue(qry)

        '                If check <= 0 Then
        '                    Throw New Exception("Filled MCC Details Is Invlaid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
        '                End If
        '            End If
        '            '-------------------------------------

        '            obj.kilometer = clsCommon.myCdbl(grow.Cells("KiloMeter").Value)
        '            If clsCommon.myLen(obj.kilometer) <= 0 Or clsCommon.myCdbl(obj.kilometer) <= 0 Then
        '                Throw New Exception("Please Fill KiloMeter Value And It Should Be Greater Than Zero(0) At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If
        '            '-----------------------------------------------------

        '            obj.supervisorname = clsCommon.myCstr(grow.Cells("Supervisor Code").Value)
        '            If clsCommon.myLen(obj.supervisorname) <= 0 Or clsCommon.myLen(obj.supervisorname) > 100 Then
        '                Throw New Exception("Please Fill Supervisor Code,See At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If
        '            '-------------------------

        '            '----------------------
        '            Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_employee_master where emp_code='" & obj.supervisorname & "'"))
        '            If cnt <= 0 Then
        '                Throw New Exception("Invalid Supervisor Code. Not Found in master,See At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If
        '            '--------------------
        '            obj.contactno = clsCommon.myCstr(grow.Cells("Contact No").Value)
        '            If clsCommon.myLen(obj.contactno) > 50 Then
        '                Throw New Exception("Length Of Contact No. Should Not Exceed Max. 50 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If
        '            '----------------------------------

        '            'obj.add1 = clsCommon.myCstr(grow.Cells("Address1").Value)
        '            'If clsCommon.myLen(obj.add1) > 100 Then
        '            '    Throw New Exception("Length Of Address1 Should Not Exceed Max. 100 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
        '            'End If
        '            ''------------------------------

        '            'obj.add2 = clsCommon.myCstr(grow.Cells("Address2").Value)
        '            'If clsCommon.myLen(obj.add2) > 100 Then
        '            '    Throw New Exception("Length Of Address2 Should Not Exceed Max. 100 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
        '            'End If
        '            ''------------------------------

        '            'obj.add3 = clsCommon.myCstr(grow.Cells("Address3").Value)
        '            'If clsCommon.myLen(obj.add3) > 100 Then
        '            '    Throw New Exception("Length Of Address3 Should Not Exceed Max. 100 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
        '            'End If
        '            '------------------------------

        '            'obj.countrycode = clsCommon.myCstr(grow.Cells("Country Code").Value)
        '            'obj.countryname = clsCommon.myCstr(grow.Cells("Country Name").Value)
        '            'obj.statecode = clsCommon.myCstr(grow.Cells("State Code").Value)
        '            'obj.statename = clsCommon.myCstr(grow.Cells("State Name").Value)
        '            'obj.citycode = clsCommon.myCstr(grow.Cells("City Code").Value)
        '            'obj.cityname = clsCommon.myCstr(grow.Cells("City Name").Value)
        '            'If clsCommon.myLen(obj.countrycode) <= 0 AndAlso clsCommon.myLen(obj.countryname) <= 0 Then
        '            '    Throw New Exception("Please Fill Country Code/Name,At Line No " + clsCommon.myCstr(counter) + "")
        '            'End If
        '            'If clsCommon.myLen(obj.statecode) <= 0 AndAlso clsCommon.myLen(obj.statename) <= 0 Then
        '            '    Throw New Exception("Please Fill State Code/Name,At Line No " + clsCommon.myCstr(counter) + "")
        '            'End If
        '            'If clsCommon.myLen(obj.citycode) <= 0 AndAlso clsCommon.myLen(obj.cityname) <= 0 Then
        '            '    Throw New Exception("Please Fill City Code/Name,At Line No " + clsCommon.myCstr(counter) + "")
        '            'End If

        '            'If clsCommon.myLen(obj.countrycode) > 0 Or clsCommon.myLen(obj.countryname) > 0 Then
        '            '    qry = "select count(*) from tspl_country_master where country_code='" + obj.countrycode + "'"
        '            '    check = clsDBFuncationality.getSingleValue(qry)
        '            '    If check <= 0 Then
        '            '        qry = "select count(*) from tspl_country_master where country_name='" + obj.countryname + "'"
        '            '        check = clsDBFuncationality.getSingleValue(qry)
        '            '        If check <= 0 Then
        '            '            Throw New Exception("Country Details Does Not Exist Or Invalid Entry,First Make Master,See At Line No. " + clsCommon.myCstr(counter) + "")
        '            '        End If
        '            '    End If
        '            'End If

        '            'If clsCommon.myLen(obj.statecode) > 0 Or clsCommon.myLen(obj.statename) > 0 Then
        '            '    qry = "select count(*) from tspl_state_master where country_code='" + obj.countrycode + "' and state_code='" + obj.statecode + "'"
        '            '    check = clsDBFuncationality.getSingleValue(qry)
        '            '    If check <= 0 Then
        '            '        qry = "select count(*) from tspl_state_master where country_code='" + obj.countrycode + "' and state_name='" + obj.statename + "'"
        '            '        check = clsDBFuncationality.getSingleValue(qry)
        '            '        If check <= 0 Then
        '            '            Throw New Exception("State Details Does Not Exist Or Invalid Entry,First Make Master,See At Line No. " + clsCommon.myCstr(counter) + "")
        '            '        End If
        '            '    End If
        '            'End If

        '            'If clsCommon.myLen(obj.citycode) > 0 Or clsCommon.myLen(obj.cityname) > 0 Then
        '            '    qry = "select count(*) from tspl_city_master where state_code='" + obj.statecode + "' and city_code='" + obj.citycode + "'"
        '            '    check = clsDBFuncationality.getSingleValue(qry)
        '            '    If check <= 0 Then
        '            '        qry = "select count(*) from tspl_city_master where state_code='" + obj.statecode + "' and city_name='" + obj.cityname + "'"
        '            '        check = clsDBFuncationality.getSingleValue(qry)
        '            '        If check <= 0 Then
        '            '            Throw New Exception("City Details Does Not Exist Or Invalid Entry,First Make Master,See At Line No. " + clsCommon.myCstr(counter) + "")
        '            '        End If
        '            '    End If
        '            'End If
        '            ''-------------------------------------------

        '            obj.email = clsCommon.myCstr(grow.Cells("Email ID").Value)
        '            If clsCommon.myLen(obj.email) > 100 Then
        '                Throw New Exception("Length Of Email ID Should Not Exceed Max. 100 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If
        '            '---------------------------------

        '            obj.effectivedate = clsCommon.myCstr(grow.Cells("Effective Date").Value)

        '            If clsCommon.myLen(obj.effectivedate) <= 0 Then
        '                obj.effectivedate = clsCommon.GETSERVERDATE().ToString("dd/MM/yyyy")
        '            End If

        '            qry = "select count(*) from tspl_mcc_route_master where route_code='" + obj.code + "'"
        '            check = clsDBFuncationality.getSingleValue(qry)

        '            If check <= 0 Then
        '                isNewEntry = True
        '            Else
        '                isNewEntry = False
        '            End If

        '            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        '            If clsfrmMilkRouteMaster.SaveData(obj.code, trans, obj, isNewEntry) Then
        '            Else
        '                Throw New Exception("No Data Transfer")
        '            End If

        '            counter += 1
        '        Next

        '        clsCommon.ProgressBarHide()
        '        clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
        '        Reset()
        '    Catch ex As Exception
        '        clsCommon.ProgressBarHide()
        '        clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        '    End Try
        'End If
        'Me.Controls.Remove(gv)
    End Sub

    Private Sub FrmMilkRouteMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    '------------BM00000003414-------------------
    Private Sub MCCLOCATIONFINDER()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                arrLoc = obj.arrLocCodes
            Else
                'fndMCCCode.Enabled = False
                'Throw New Exception("Please Set Default Location Of LogIn User")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    '------------------------------------------------


    Private Sub txtemail_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtemail.Leave
        If txtemail.Text = "" Then
        Else
            Dim check As Match = Regex.Match(txtemail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            If check.Success Then
                Errorcontrol.ResetError(txtemail)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter the proper format of e-mail address", Me.Text)
                txtemail.Text = ""
                txtemail.Focus()
                txtemail.Select()
                Errorcontrol.SetError(txtemail, "Please Enter the proper format of e-mail address")
            End If
        End If
    End Sub

    'Sub UpdateVLCSequence(ByVal CurrRow As GridViewRowInfo, ByVal prevVal As Integer)
    '    If Prev <= 0 AndAlso (CurrRow.Index = (gvVLC.RowCount - 1) Or CurrRow.Index = -1) Then
    '        Prev = gvVLC.RowCount + 1            
    '    ElseIf Prev <= 0 Then
    '        Exit Sub
    '    End If
    '    If prevVal > clsCommon.myCdbl(CurrRow.Cells(colSNO).Value) Then
    '        For row As Integer = (clsCommon.myCdbl(CurrRow.Cells(colSNO).Value)) - 1 To (prevVal - 1) - 1
    '            IsInsieLoadData = True
    '            gvVLC.Rows(row).Cells(colSNO).Value = clsCommon.myCdbl(gvVLC.Rows(row).Cells(colSNO).Value) + 1
    '        Next
    '    ElseIf prevVal < clsCommon.myCdbl(CurrRow.Cells(colSNO).Value) AndAlso CurrRow.Index <> ((CurrRow.Cells(colSNO).Value) - 1) Then
    '        IsInsieLoadData = True
    '        gvVLC.Rows(clsCommon.myCdbl(CurrRow.Cells(colSNO).Value) - 1).Cells(colSNO).Value = Prev
    '    End If
    '    'For row As Integer = 0 To gvVLC.RowCount - 1
    '    '    If clsCommon.myCdbl(gvVLC.Rows(row).Cells(colSNO).Value) = clsCommon.myCdbl(CurrRow.Cells(colSNO).Value) Then
    '    '        For intloop As Integer = row To gvVLC.RowCount - 1
    '    '            If gvVLC.Rows(intloop).Index < CurrRow.Index Then
    '    '                IsInsieLoadData = True
    '    '                gvVLC.Rows(intloop).Cells(colSNO).Value = (intloop + 1) + 1
    '    '            ElseIf gvVLC.Rows(intloop).Index > CurrRow.Index Then
    '    '                IsInsieLoadData = True
    '    '                gvVLC.Rows(intloop).Cells(colSNO).Value = clsCommon.myCdbl(gvVLC.Rows(intloop).Cells(colSNO).Value) - 1
    '    '            End If

    '    '        Next
    '    '        Exit Sub
    '    '    End If
    '    'Next
    'End Sub



    Private Sub fndSuperVisorCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndSuperVisorCode._MYValidating
        fndSuperVisorCode.Value = clsEmployeeMaster.getFinder(" emp_status='Active' ", fndSuperVisorCode.Value, isButtonClicked)
        If clsCommon.myLen(fndSuperVisorCode.Value) > 0 Then
            Dim qry As String = "select PRESENT_MOBILE_NO,PRESENT_CITY_CODE,City_Name ,PRESENT_STATE_CODE,STATE_NAME ,PRESENT_COUNTRY_CODE,COUNTRY_NAME  ,EMail_ID  from TSPL_EMPLOYEE_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_EMPLOYEE_MASTER.PRESENT_CITY_CODE left outer join TSPL_state_MASTER on TSPL_state_MASTER.STATE_CODE =TSPL_EMPLOYEE_MASTER.PRESENT_STATE_CODE left outer join TSPL_COUNTRY_MASTER  on TSPL_COUNTRY_MASTER .COUNTRY_CODE =TSPL_EMPLOYEE_MASTER.PRESENT_COUNTRY_CODE  where TSPL_EMPLOYEE_MASTER.EMP_CODE='" & fndSuperVisorCode.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtSuperVisorName.Text = clsEmployeeMaster.GetName(fndSuperVisorCode.Value, Nothing)
                txtcontactno.Text = clsCommon.myCstr(dt.Rows(0)("PRESENT_MOBILE_NO"))
                txtcity.Value = clsCommon.myCstr(dt.Rows(0)("PRESENT_CITY_CODE"))
                txtcityname.Text = clsCommon.myCstr(dt.Rows(0)("City_Name"))
                txtstatename.Text = clsCommon.myCstr(dt.Rows(0)("PRESENT_STATE_CODE"))
                txtstate.Value = clsCommon.myCstr(dt.Rows(0)("STATE_NAME"))
                txtcountrycode.Value = clsCommon.myCstr(dt.Rows(0)("PRESENT_COUNTRY_CODE"))
                txtcountryname.Text = clsCommon.myCstr(dt.Rows(0)("COUNTRY_NAME"))
                txtemail.Text = clsCommon.myCstr(dt.Rows(0)("EMail_ID"))
            Else
                txtSuperVisorName.Text = ""
                txtcontactno.Text = ""
                txtcity.Value = ""
                txtcityname.Text = ""
                txtstatename.Text = ""
                txtstate.Value = ""
                txtcountrycode.Value = ""
                txtcountryname.Text = ""
                txtemail.Text = ""
            End If
        Else
            txtcontactno.Text = ""
            txtcity.Value = ""
            txtcityname.Text = ""
            txtstatename.Text = ""
            txtstate.Value = ""
            txtcountrycode.Value = ""
            txtcountryname.Text = ""
            txtemail.Text = ""
        End If

    End Sub

    Private Sub rxMilkRouteDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rxMilkRouteDetails.Click
        Dim qry As String = "select count(*) from tspl_mcc_route_master"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If check > 0 Then
            qry = "select TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Name]," _
                & " TSPL_MCC_ROUTE_MASTER.vehicle_code as [Vehicle Code],TSPL_MCC_ROUTE_MASTER.mcc_code as [MCC Code],tspl_mcc_master.mcc_name as [MCC Name]," _
                & " TSPL_MCC_ROUTE_MASTER.KiloMeter,TSPL_MCC_ROUTE_MASTER.supervisor_name as [Supervisor Code],TSPL_MCC_ROUTE_MASTER.contact_no as [Contact No]," _
                & " TSPL_MCC_ROUTE_MASTER.email_id as [Email ID],convert (varchar,TSPL_MCC_ROUTE_MASTER.effective_date,103) as [Effective Date],case when active=1 then 'Active' else 'Inactive' end " _
                & " as [Status],substring(convert(varchar, MCC_Reaching_Time_E,108),1,5) as ReachingTimeE,substring(convert(varchar, MCC_Reaching_Time_M,108),1,5) as ReachingTimeM  "
            If settSeprateDistanceMorningEveningShift Then
                qry += " ,TSPL_MCC_ROUTE_MASTER.Kilometer_Morning as KilometerMorning,TSPL_MCC_ROUTE_MASTER.Kilometer_Evening as KilometerEvening"
            End If
            qry += " from TSPL_MCC_ROUTE_MASTER left outer join tspl_mcc_master on Tspl_mcc_master.mcc_code=TSPL_MCC_ROUTE_MASTER.mcc_code " _
                & " left outer join tspl_country_master on TSPL_MCC_ROUTE_MASTER.country_code=tspl_country_master.country_code left outer join " _
                & " tspl_state_master on tspl_state_master.state_code=TSPL_MCC_ROUTE_MASTER.state_code left outer join tspl_city_master on tspl_city_master.city_code=" _
                & " TSPL_MCC_ROUTE_MASTER.city_code"
        Else
            qry = "select '' as Code,'' as [Route Name],'' as [Vehicle Code],'' as [MCC Code],'' as [MCC Name],'' as KiloMeter,'' as [Supervisor Code],'' as [Contact No],'' as [Email ID],'' as [Effective Date],'' as [Status],'' as ReachingTimeE,'' as ReachingTimeM "
        End If
        ListImpExpColumnsMandatory = New List(Of String)({"Code", "Route Name", "vehicle code", "MCC Code", "KiloMeter", "Supervisor Code"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
        transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub rmMilkRouteDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmMilkRouteDetails.Click

        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim flag As Boolean = False
        If settSeprateDistanceMorningEveningShift Then
            flag = transportSql.importExcel(gv, "Code", "Route Name", "Vehicle Code", "MCC Code", "MCC Name", "KiloMeter", "Supervisor Code", "Contact No", "Email ID", "Effective Date", "Status", "ReachingTimeE", "ReachingTimeM", "KilometerMorning", "KilometerEvening")
        Else
            flag = transportSql.importExcel(gv, "Code", "Route Name", "Vehicle Code", "MCC Code", "MCC Name", "KiloMeter", "Supervisor Code", "Contact No", "Email ID", "Effective Date", "Status", "ReachingTimeE", "ReachingTimeM")
        End If

        If flag Then

            ''Removed
            ''''' "Address1", "Address2", "Address3",
            '''', "Country Code", "Country Name", "State Code", "State Name", "City Code", "City Name"
            Try
                clsCommon.ProgressBarShow()
                Dim obj As clsfrmMilkRouteMaster
                Dim counter As Integer = 1

                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsfrmMilkRouteMaster()
                    clsfrmMilkRouteMaster.arr_VLC_Detail = Nothing
                    obj.code = clsCommon.myCstr(grow.Cells("Code").Value)
                    obj = clsfrmMilkRouteMaster.GetData(obj.code, arrLoc, NavigatorType.Current)
                    If obj Is Nothing Then
                        obj = New clsfrmMilkRouteMaster
                    End If
                    obj.code = clsCommon.myCstr(grow.Cells("Code").Value)
                    'If clsCommon.myLen(obj.code) <= 0 Or clsCommon.myLen(obj.code) > 30 Then
                    '    Throw New Exception("Please Fill Route Code(Max. 30 Characters) At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If
                    '-----------------------------

                    obj.desc = clsCommon.myCstr(grow.Cells("Route Name").Value)
                    If clsCommon.myLen(obj.desc) <= 0 Or clsCommon.myLen(obj.desc) > 150 Then
                        Throw New Exception("Please Fill Route Name(Max. 150 Characters) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    '--------------------------------

                    Dim qry As String = ""
                    Dim check As Integer = 0

                    obj.vehiclecode = clsCommon.myCstr(grow.Cells("vehicle code").Value)
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Status").Value), "Active") = CompairStringResult.Equal Then
                        obj.Active = 1
                    Else
                        obj.Active = 0
                    End If
                    'obj.vehiclename = clsCommon.myCstr(grow.Cells("vehicle name").Value)
                    If clsCommon.myLen(obj.vehiclecode) <= 0 Then
                        Throw New Exception("Please Fill Vehicle Details At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(obj.vehiclecode) > 0 Then
                        qry = "select count(*) from tspl_primary_vehicle_master where vehicle_code='" + obj.vehiclecode + "'"
                        check = clsDBFuncationality.getSingleValue(qry)

                        If check <= 0 Then
                            '        qry = "select count(*) from tspl_primary_vehicle_master where description='" + obj.vehiclename + "'"
                            '        check = clsDBFuncationality.getSingleValue(qry)

                            '        If check <= 0 Then
                            '            obj.vehiclecode = ""
                            '            Throw New Exception("Filled Vehicle Details Is Invlaid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
                            '        End If
                            '    End If
                            'End If
                            'If clsCommon.myLen(obj.vehiclecode) <= 0 Then
                            '    qry = "select count(*) from tspl_primary_vehicle_master where description='" + obj.vehiclename + "'"
                            '    check = clsDBFuncationality.getSingleValue(qry)

                            '    If check <= 0 Then
                            Throw New Exception("Filled Vehicle Code Is Invlaid Or Does Not Exist in Master,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    ''-----------------------------------

                    obj.mcccode = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                    obj.mccname = clsCommon.myCstr(grow.Cells("MCC Name").Value)

                    If clsCommon.myLen(obj.mcccode) <= 0 AndAlso clsCommon.myLen(obj.mccname) <= 0 Then
                        Throw New Exception("Please Fill MCC Details At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(obj.mcccode) > 0 Then
                        qry = "select count(*) from tspl_mcc_master where mcc_code='" + obj.mcccode + "'"
                        check = clsDBFuncationality.getSingleValue(qry)

                        If check <= 0 Then
                            qry = "select count(*) from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                            check = clsDBFuncationality.getSingleValue(qry)

                            If check <= 0 Then
                                obj.mcccode = ""
                                Throw New Exception("Filled MCC Details Is Invlaid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                    End If
                    If clsCommon.myLen(obj.mcccode) <= 0 Then
                        qry = "select count(*) from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                        check = clsDBFuncationality.getSingleValue(qry)

                        If check <= 0 Then
                            Throw New Exception("Filled MCC Details Is Invlaid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    '-------------------------------------

                    obj.kilometer = clsCommon.myCdbl(grow.Cells("KiloMeter").Value)
                    If clsCommon.myLen(obj.kilometer) <= 0 Or clsCommon.myCdbl(obj.kilometer) <= 0 Then
                        Throw New Exception("Please Fill KiloMeter Value And It Should Be Greater Than Zero(0) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    '-----------------------------------------------------

                    obj.supervisorname = clsCommon.myCstr(grow.Cells("Supervisor Code").Value)
                    If clsCommon.myLen(obj.supervisorname) <= 0 Or clsCommon.myLen(obj.supervisorname) > 100 Then
                        Throw New Exception("Please Fill Supervisor Code,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    '-------------------------

                    '----------------------
                    Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_employee_master where emp_code='" & obj.supervisorname & "'"))
                    If cnt <= 0 Then
                        Throw New Exception("Invalid Supervisor Code. Not Found in master,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    '--------------------

                    qry = "select PRESENT_MOBILE_NO,PRESENT_CITY_CODE,City_Name ,PRESENT_STATE_CODE,STATE_NAME ,PRESENT_COUNTRY_CODE,COUNTRY_NAME  ,EMail_ID  from TSPL_EMPLOYEE_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_EMPLOYEE_MASTER.PRESENT_CITY_CODE left outer join TSPL_state_MASTER on TSPL_state_MASTER.STATE_CODE =TSPL_EMPLOYEE_MASTER.PRESENT_STATE_CODE left outer join TSPL_COUNTRY_MASTER  on TSPL_COUNTRY_MASTER .COUNTRY_CODE =TSPL_EMPLOYEE_MASTER.PRESENT_COUNTRY_CODE  where TSPL_EMPLOYEE_MASTER.EMP_CODE='" & obj.supervisorname & "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        obj.contactno = clsCommon.myCstr(dt.Rows(0)("PRESENT_MOBILE_NO"))
                        obj.email = clsCommon.myCstr(dt.Rows(0)("EMail_ID"))
                    End If


                    If clsCommon.myLen(grow.Cells("ReachingTimeM").Value) > 0 Then
                        Try
                            obj.MCC_Reaching_Time_M = clsCommon.myCDate(clsCommon.GetPrintDate(DateTime.Now, "dd/MMM/yyyy") + " " + clsCommon.myCstr(grow.Cells("ReachingTimeM").Value))
                        Catch ex As Exception
                        End Try
                    End If

                    'obj.MCC_Reaching_Time_E = Nothing
                    If clsCommon.myLen(grow.Cells("ReachingTimeE").Value) > 0 Then
                        Try
                            obj.MCC_Reaching_Time_E = clsCommon.myCDate(clsCommon.GetPrintDate(DateTime.Now, "dd/MMM/yyyy") + " " + clsCommon.myCstr(grow.Cells("ReachingTimeE").Value))
                        Catch ex As Exception
                        End Try
                    End If

                    obj.effectivedate = clsCommon.myCstr(grow.Cells("Effective Date").Value)

                    If clsCommon.myLen(obj.effectivedate) <= 0 Then
                        obj.effectivedate = clsCommon.GETSERVERDATE().ToString("dd/MM/yyyy")
                    End If

                    qry = "select count(*) from tspl_mcc_route_master where route_code='" + obj.code + "'"
                    check = clsDBFuncationality.getSingleValue(qry)

                    If check <= 0 Then
                        isNewEntry = True
                    Else
                        isNewEntry = False
                    End If
                    If settSeprateDistanceMorningEveningShift Then
                        obj.kilometer_Morning = clsCommon.myCdbl(grow.Cells("kilometerMorning").Value)
                        obj.kilometer_Evening = clsCommon.myCdbl(grow.Cells("kilometerEvening").Value)
                    End If
                    'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    If clsfrmMilkRouteMaster.SaveData(obj.code, obj, isNewEntry, True) Then
                    Else
                        Throw New Exception("No Data Transfer")
                    End If

                    counter += 1
                Next

                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                Reset()
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmVLCDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmVLCDetails.Click
        Try
            ImportVLCDetails()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rxVLCDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rxVLCDetails.Click
        Try
            Dim qry As String
            Dim extraColumn As String = ""
            If VLCTimeTableColumnShow Then
                extraColumn = " ,TSPL_MCC_ROUTE_VLC_MAPPING.SNo as  [Sequence No],TSPL_MCC_ROUTE_VLC_MAPPING.Distance,SUBSTRING( convert(varchar, TSPL_MCC_ROUTE_VLC_MAPPING.Mor_Mik_Coll ,108),0,6) as [Morning Reaching Time],SUBSTRING( convert(varchar, TSPL_MCC_ROUTE_VLC_MAPPING.Eve_Milk_Coll ,108),0,6) as [Evening Reaching Time] "
            End If
            qry = "select TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE AS [Route Code],TSPL_MCC_ROUTE_VLC_MAPPING.VLC_CODE AS [DCS Code],TSPL_VLC_MASTER_HEAD.VLC_Name As [DCS Name] ,VSP_Code AS [Secretary Code] ,vendor_name as [Secretary Name],case when coalesce(Is_Active,0)=1 then 'Open' else 'Close' end As [Status],case when TSPL_MCC_ROUTE_VLC_MAPPING.Out_Route=1 then 'Y' else 'N' end as [Out Route],TSPL_MCC_ROUTE_VLC_MAPPING.Out_Route as [Out Route KM] " + extraColumn + " From TSPL_MCC_ROUTE_VLC_MAPPING " &
                  " LEFT OUTER JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MCC_ROUTE_VLC_MAPPING.VLC_CODE " &
                  " left join TSPL_VENDOR_MASTER vm on vm.Vendor_Code=VSP_Code"
            ListImpExpColumnsMandatory = New List(Of String)({"Route Code", "DCS Code", "Sequence No", "Distance"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"Route Code", "DCS Code"})
            transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "VLCDetails")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "UOM")
        End Try
    End Sub

    Private Sub ImportVLCDetails()
        Dim gvCharges As New RadGridView()
        Me.Controls.Add(gvCharges)
        Dim countDefaultUOM As Integer = 0
        Dim boolresult As Boolean = False
        If VLCTimeTableColumnShow Then
            boolresult = transportSql.importExcel(gvCharges, "Route Code", "DCS Code", "DCS Name", "Secretary Code", "Secretary Name", "Status", "Out Route", "Out Route KM", "Sequence No", "Distance", "Morning Reaching Time", "Evening Reaching Time")
        Else
            boolresult = transportSql.importExcel(gvCharges, "Route Code", "VLC Code", "VLC Name", "VSP Code", "VSP Name", "Status", "Out Route", "Out Route KM")
        End If

        If boolresult Then
            Dim isSaved As Boolean = True
            Dim currentdate As Date = Date.Today
            Dim trans As SqlTransaction = Nothing
            clsCommon.ProgressBarShow()
            Dim arrRoute As New List(Of String)
            Dim arrVlcCode As New List(Of String)
            Try
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gvCharges.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim coll As New Hashtable()

                    Dim RouteCode As String = clsCommon.myCstr(grow.Cells("Route Code").Value)
                    If clsCommon.myLen(RouteCode) >= 0 Then
                        RouteCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Route_Code from TSPL_MCC_ROUTE_MASTER Where Route_Code ='" + RouteCode + "'", trans))
                        If clsCommon.myLen(RouteCode) <= 0 Then
                            Throw New Exception("Route Code '" + RouteCode + "' does not exist .Please make its master first at line no '" + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please insert route code at line no '" + LineNo + "' ")
                    End If
                    If Not arrRoute.Contains(RouteCode) Then
                        arrRoute.Add(RouteCode)
                    End If

                    Dim strVLCCode As String
                    Dim VLCCode As String = clsCommon.myCstr(grow.Cells("VLC Code").Value)
                    If clsCommon.myLen(VLCCode) > 0 Then
                        strVLCCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select VLC_Code from TSPL_VLC_MASTER_HEAD Where VLC_Code ='" + VLCCode + "'", trans))
                        If clsCommon.myLen(strVLCCode) <= 0 Then
                            Throw New Exception("DCS Code '" + VLCCode + "' does not exist .Please make its master first at line no '" + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please fill VLC code at line no '" + LineNo + "' ")
                    End If
                    Dim VLCName As String
                    ' Dim VehicleName As String
                    Dim VSPCode As String
                    Dim QueryVSPName As String

                    Dim MCCCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Code From TSPL_MCC_ROUTE_MASTER Where Route_Code ='" & RouteCode & "'", trans))
                    Dim sQuery As String = "select vlc_code as Code,vlc_name as Name,Vehical_name as [Vehicle Name],VSP_Code as [VSP Code],vendor_name as [VSP Name]" &
                                           " from TSPL_VLC_MASTER_HEAD inner join tspl_mcc_Master  on mcc=mcc_Code  left join TSPL_VENDOR_MASTER vm on vm.Vendor_Code=VSP_Code "
                    Dim whrcls As String = sQuery + " tspl_mcc_master.mcc_code in (" + arrLoc + ")"

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        VLCName = clsCommon.myCstr(dt.Rows(0)("Name"))
                        VSPCode = clsCommon.myCstr(dt.Rows(0)("VSP Code"))
                        QueryVSPName = clsCommon.myCstr(dt.Rows(0)("VSP Name"))
                    End If

                    Dim Status As String = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Status").Value), "Open") = CompairStringResult.Equal, 1, 0)

                    If clsCommon.CompairString(Status, "1") = CompairStringResult.Equal OrElse clsCommon.CompairString(Status, "0") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Status should be 1 at Line No '" + LineNo + "'")
                    End If
                    clsCommon.AddColumnsForChange(coll, "Route_CODE", RouteCode)
                    clsCommon.AddColumnsForChange(coll, "VLC_CODE", VLCCode)
                    clsCommon.AddColumnsForChange(coll, "Is_Active", Status)
                    clsCommon.AddColumnsForChange(coll, "Out_Route", IIf((clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Out Route").Value), "Y") = CompairStringResult.Equal), 1, 0))
                    clsCommon.AddColumnsForChange(coll, "Out_Route_KM", clsCommon.myCdbl(grow.Cells("Out Route KM").Value))
                    If VLCTimeTableColumnShow Then
                        clsCommon.AddColumnsForChange(coll, "SNo", clsCommon.myCdbl(grow.Cells("Sequence No").Value))
                        clsCommon.AddColumnsForChange(coll, "Distance", clsCommon.myCdbl(grow.Cells("Distance").Value))
                        If VLCTimeTableColumnMandatory Then
                            If clsCommon.myCdbl(clsCommon.myCdbl(grow.Cells("Sequence No").Value)) <= 0 Then
                                Throw New Exception("Please provide Sequence No of VLC " + VLCCode)
                            End If
                            Dim flag As Boolean = (clsCommon.myCdbl(grow.Cells("Distance").Value) <= 0)
                            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHBA") = CompairStringResult.Equal Then
                                flag = (clsCommon.myCdbl(grow.Cells("Distance").Value) < 0)
                            End If
                            If flag Then
                                Throw New Exception("Please provide Distance of VLC " + VLCCode)
                            End If
                        End If
                        If clsCommon.myLen(grow.Cells("Morning Reaching Time").Value) > 0 Then
                            clsCommon.AddColumnsForChange(coll, "Mor_Mik_Coll", clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells("Morning Reaching Time").Value), "dd/MMM/yyyy hh:mm tt"))
                        ElseIf VLCTimeTableColumnMandatory Then
                            Throw New Exception("Please provide morning reaching time of VLC " + VLCCode)
                        End If

                        If clsCommon.myLen(grow.Cells("Evening Reaching Time").Value) > 0 Then
                            clsCommon.AddColumnsForChange(coll, "Eve_Milk_Coll", clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells("Evening Reaching Time").Value), "dd/MMM/yyyy hh:mm tt"))
                        ElseIf VLCTimeTableColumnMandatory Then
                            Throw New Exception("Please provide evening reaching time of VLC " + VLCCode)
                        End If
                    End If

                    '*********************************************************************************************************
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_MCC_ROUTE_VLC_MAPPING  set Is_Active = 0  where VLC_CODE = '" & clsCommon.myCstr(grow.Cells("VLC Code").Value) & "' and Route_CODE <> '" & clsCommon.myCstr(grow.Cells("Route Code").Value) & "' ", trans)
                    clsDBFuncationality.ExecuteNonQuery("DELETE FROM TSPL_MCC_ROUTE_VLC_MAPPING where Route_CODE = '" & clsCommon.myCstr(grow.Cells("Route Code").Value) & "' and  VLC_CODE = '" & clsCommon.myCstr(grow.Cells("VLC Code").Value) & "'", trans)
                    clsDBFuncationality.ExecuteNonQuery("update tspl_vlc_master_head set Route_Code=null where Route_Code='" & clsCommon.myCstr(grow.Cells("Route Code").Value) & "'  and  VLC_CODE = '" & clsCommon.myCstr(grow.Cells("VLC Code").Value) & "' ", trans)
                    '*********************************************************************************************************

                    sQuery = "select count(*) from TSPL_MCC_ROUTE_VLC_MAPPING where route_code='" & RouteCode & "' and vlc_code='" & VLCCode & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(sQuery, trans)
                    sQuery = "update TSPL_MCC_ROUTE_VLC_MAPPING set is_active=0 where route_code<>'" & RouteCode & "' and vlc_code='" & VLCCode & "'"
                    clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    sQuery = "update TSPL_VLC_MASTER_HEAD set route_code='" & RouteCode & "' where vlc_code='" & VLCCode & "'"
                    clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    If check <= 0 Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_ROUTE_VLC_MAPPING", OMInsertOrUpdate.Update, " route_code='" & RouteCode & "' and vlc_code='" & VLCCode & "'", trans)
                    End If
                Next
                If isSaved Then
                    If VLCTimeTableColumnMandatory Then
                        If Not (arrRoute Is Nothing) AndAlso arrRoute.Count > 0 Then
                            For Each str As String In arrRoute
                                clsfrmMilkRouteMaster.CheckSequenceOfVLC(str, trans)
                                clsDBFuncationality.ExecuteNonQuery("update TSPL_MCC_ROUTE_MASTER set KiloMeter=(select sum(isnull(Distance,0)) as Distance  from TSPL_MCC_ROUTE_VLC_MAPPING where TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE=TSPL_MCC_ROUTE_MASTER.Route_CODE) where Route_CODE= '" + str + "'", trans)
                            Next
                        End If
                    End If
                    clsCommon.ProgressBarHide()
                    trans.Commit()
                    RadMessageBox.Show(Me, "Data Imported Successfully.", Me.Text)
                Else
                    Throw New Exception("Error in Import")
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                RadMessageBox.Show(Me, ex.Message, Me.Text)
            Finally
                Me.Controls.Remove(gvCharges)
                arrRoute = Nothing
            End Try
        End If
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Route Code", Me.Text)
                fndcode.Focus()
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndcode.Value, "Route_Code", "TSPL_MCC_ROUTE_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gvVLC_CellBeginEdit(sender As Object, e As GridViewCellCancelEventArgs) Handles gvVLC.CellBeginEdit
        Try
            If gvVLC.CurrentRow.Cells(colSNO).Value IsNot Nothing Then
                Prev = gvVLC.CurrentRow.Cells(colSNO).Value
            End If
            If gvVLC.CurrentRow.Cells(colSNO).Value Is Nothing Then
                Dim lastIndex = gvVLC.Rows.Count - 1
                Prev = lastIndex + 1
                If lastIndex >= 0 Then
                    gvVLC.Rows(lastIndex).Cells(colSNO).Value = Prev
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gvVLC_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvVLC.CellValueChanged
        Try
            If Not IsInsieLoadData Then
                IsInsieLoadData = True
                If e.Column Is gvVLC.Columns("COLVLC_Code") OrElse e.Column Is gvVLC.Columns("COLUploaderCode") Then
                    Dim sQuery As String = "select "
                    Dim strID As String = ""
                    If e.Column Is gvVLC.Columns("COLVLC_Code") Then
                        strID = "VLCFnd@RM"
                        sQuery += " vlc_code As Code,vlc_code_vlc_uploader as [Uploader Code],"
                    ElseIf e.Column Is gvVLC.Columns("COLUploaderCode") Then
                        strID = "VLCUFnd@RM"
                        sQuery += "vlc_code_vlc_uploader as Code, vlc_code As [VLC Code],"
                    Else
                        Throw New Exception("Wrong column")
                    End If
                    sQuery += " vlc_name As Name, Vehical_name As [Vehicle Name], VSP_Code As [VSP Code], vendor_name as [VSP Name] " _
                    & " from TSPL_VLC_MASTER_HEAD inner join tspl_mcc_Master  on mcc=mcc_Code  left join TSPL_VENDOR_MASTER vm on vm.Vendor_Code=VSP_Code "
                    Dim whrcls As String = " tspl_mcc_master.mcc_code in (" + arrLoc + ")  And coalesce(active,1)=1 And  Coalesce(route_Code,'')='' "
                    Dim str As String = clsCommon.ShowSelectForm(strID, sQuery, "Code", whrcls, gvVLC.CurrentRow.Cells(e.Column.Name).Value, "", False)
                    If str <> "" Then
                        Dim objvlc As New clsfrmVLCMaster
                        objvlc = clsfrmVLCMaster.GetData(str, "", NavigatorType.Current, (e.Column Is gvVLC.Columns("COLUploaderCode")))
                        If Not IsNothing(objvlc) Then
                            gvVLC.CurrentRow.Cells("ColVLC_Code").Value = objvlc.vlcCode
                            gvVLC.CurrentRow.Cells("ColVLC_Name").Value = objvlc.vlcName
                            gvVLC.CurrentRow.Cells("ColVsp_Code").Value = objvlc.vspCode
                            gvVLC.CurrentRow.Cells("ColVSP_Name").Value = objvlc.VspName
                            gvVLC.CurrentRow.Cells("ColVehicle_name").Value = objvlc.vehical
                            gvVLC.CurrentRow.Cells("COLOpeningDate").Value = objvlc.Created_Date
                            gvVLC.CurrentRow.Cells("COLUploaderCode").Value = objvlc.VLC_CODE_VLC_UPLOADER
                        End If
                    End If
                ElseIf e.Column Is gvVLC.Columns(colSNO) Then
                    If gvVLC.CurrentRow.Index < 0 Then
                        gvVLC.CurrentRow.Cells(colSNO).Value = gvVLC.RowCount + 1
                    Else
                        Dim CurrSNO As Integer = clsCommon.myCdbl(gvVLC.CurrentRow.Cells(colSNO).Value)
                        If CurrSNO > gvVLC.RowCount Then
                            gvVLC.CurrentRow.Cells(colSNO).Value = Prev
                        Else
                            For ii As Integer = 0 To gvVLC.RowCount - 1
                                Dim RunSNO As Integer = clsCommon.myCdbl(gvVLC.Rows(ii).Cells(colSNO).Value)
                                If gvVLC.CurrentRow.Index = ii Then
                                    Continue For
                                End If
                                If RunSNO >= CurrSNO AndAlso RunSNO <= Prev Then
                                    gvVLC.Rows(ii).Cells(colSNO).Value = clsCommon.myCdbl(gvVLC.Rows(ii).Cells(colSNO).Value) + 1
                                ElseIf RunSNO <= CurrSNO AndAlso RunSNO >= Prev Then
                                    gvVLC.Rows(ii).Cells(colSNO).Value = clsCommon.myCdbl(gvVLC.Rows(ii).Cells(colSNO).Value) - 1

                                End If
                            Next
                        End If
                    End If
                End If
                IsInsieLoadData = False
            End If
        Catch ex As Exception
            IsInsieLoadData = False
        End Try
    End Sub


End Class
