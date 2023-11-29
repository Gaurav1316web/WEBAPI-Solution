'-----------Created By-----------Monika------11/06/2014-------------
'--------BM00000003414----------------------------------
'==========Updated By Rohit on Sep,04================
''BM00000009402 by balwinder on 10/08/2016
Imports common
Imports System.Data.SqlClient
Public Class FrmPrimaryTransporterVehicalMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim arrLoc As String = Nothing
    Public Const colSlabUpto As String = "colSlabUpto"
    Public Const colSlabRate As String = "colSlabRate"
    Dim isNewEntry As Boolean = True
    Dim Frm_Open As FrmMainTranScreen
    Dim settApplyEffectiveStartDate As Boolean = False ''by balwinder on 22/06/2021
    Dim ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster As Boolean = False
#End Region
    Private Sub FrmPrimaryTransporterVehicalMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
            ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) > 0, True, False)
            settApplyEffectiveStartDate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyEffectiveStartDate, clsFixedParameterCode.ApplyEffectiveStartDate, Nothing)) > 0, True, False)
            txtEffectiveStartDate.Visible = settApplyEffectiveStartDate
            MyLabel11.Visible = settApplyEffectiveStartDate
            If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
                lblVehicle.Visible = True
                txtVehicle.Visible = True
            Else
                lblVehicle.Visible = False
                txtVehicle.Visible = False
            End If
            Reset()

            'DATE : 24-01-2017 > CLIENT : SAHAYOG DAIRY > TICKET/REQUEST : SCMPLREQ000002
            If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VehicleFitnessAndInsuranceFields,
                                                              clsFixedParameterType.VehicleFitnessAndInsuranceFields, Nothing)) = 1, True, False) Then
                RadPageView1.Pages(pageVehicleFitness.Name.ToString()).Item.Visibility = ElementVisibility.Visible
            Else
                RadPageView1.Pages(pageVehicleFitness.Name.ToString()).Item.Visibility = ElementVisibility.Collapsed
            End If
        Catch ex As Exception
        End Try

    End Sub

    Sub loadBlankgv()
        Try
            gv.Rows.Clear()
            gv.Columns.Clear()
            Dim repoDeciCol As GridViewDecimalColumn
            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colSlabUpto
            repoDeciCol.Width = 120
            repoDeciCol.DecimalPlaces = 0
            repoDeciCol.Minimum = 1
            repoDeciCol.HeaderText = "Slab Upto"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colSlabRate
            repoDeciCol.Width = 200
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 1
            repoDeciCol.HeaderText = "Slab Rate"
            gv.MasterTemplate.Columns.Add(repoDeciCol)

            gv.AllowAddNewRow = True
            gv.AllowEditRow = True
            gv.AllowDeleteRow = True
            gv.AllowRowResize = False
            gv.AllowRowReorder = False
            gv.AllowColumnResize = True
            gv.AllowColumnChooser = False
            gv.AllowAutoSizeColumns = False
            gv.ShowGroupPanel = False
            gv.AddNewRowPosition = SystemRowPosition.Bottom
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Sub Reset()
        txtTankerNo.Text = ""
        chkIsAdditional.Checked = False
        'fndcode.Value = ""
        txtdesc.Text = ""
        txtprimarycode.Value = ""
        txtprimaryname.Text = ""
        txtmcccode.Value = ""
        txtmccname.Text = ""
        txtcapcity.Text = ""
        txtyear.Text = clsCommon.GETSERVERDATE().ToString("yyyy")
        FndRoute.Value = Nothing
        LblRoute.Text = Nothing
        txtVehicleWeight.Value = 0
        rbtndiesel.Checked = False
        rbtnratekm.Checked = False
        rbtnrateltr.Checked = False
        rbtnrental.Checked = False
        rbtKmrange.Checked = False
        chkTwoWay.Checked = False
        txtchrg.Text = ""
        txtavgkm.Text = ""
        txtdiesel.Text = ""

        txt_km.Text = ""
        txt_ltr.Text = ""

        'Txt1to10Km.Text = ""
        'Txt10to20Km.Text = ""
        'TxtAbove20Km.Text = ""

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        MCCLOCATIONFINDER()
        btnsave.Text = "Save"
        btndelete.Enabled = False
        fndcode.MyReadOnly = False
        txtTankerNo.ReadOnly = False
        isNewEntry = True
        txtTankerNo.Focus()
        txtTankerNo.Select()
        txtRentalAmt.Text = ""
        cmbRentalType.Enabled = False
        cmbRentalType.SelectedIndex = 0
        'fndcode.Focus()
        'fndcode.Select()
        cmbLtrKG.SelectedIndex = 0
        txt_ltr.Text = ""
        loadBlankgv()
        gv.Rows.AddNew()
        ClickMethod()
        txtVehInsuranceNo.Text = String.Empty
        dtpInsurance.Value = clsCommon.GETSERVERDATE()
        txtVehFitnessNo.Text = String.Empty
        dtpVehFitness.Value = dtpInsurance.Value
        txtEffectiveStartDate.Checked = False
        txtEffectiveStartDate.Value = dtpInsurance.Value
        txtVehicle.Text = ""
    End Sub

    Private Sub MCCLOCATIONFINDER()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                If clsCommon.myLen(txtmcccode.Value) <= 0 AndAlso Not obj.Default_HO Then
                    txtmcccode.Value = obj.Default_LocCode
                    txtmccname.Text = obj.Default_LocName
                End If
                arrLoc = obj.arrLocCodes
            Else
                txtmcccode.Enabled = False
                Throw New Exception("Please Set Default Location Of LogIn User")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmPrimaryTransporterVehicalMaster)
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

    Function isDuplicateVechiele(ByVal VNO As String) As Boolean
        Dim rValue As Boolean = False
        Dim qry As String = " select count(xx.V_No)  from (select Tanker_No  as V_No from TSPL_TANKER_MASTER union all  select Vehicle_Code as V_No  from TSPL_Primary_Vehicle_Master )xx  where xx.V_No ='" & VNO & "'"
        Dim intCnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        If intCnt > 0 Then
            rValue = True
        Else
            rValue = False
        End If
        Return rValue
    End Function
    Function isBlankGV() As Boolean
        Dim rValue As Boolean = True
        If gv.Rows.Count = 0 Then
            rValue = False
            Return rValue
        End If
        If clsCommon.myCdbl(gv.Rows(0).Cells(colSlabUpto).Value) <= 0 Or clsCommon.myCdbl(gv.Rows(0).Cells(colSlabRate).Value) <= 0 Then
            rValue = False
            Return rValue
        End If
    End Function
    Function isValidateGridValue() As Boolean
        Dim rValue As Boolean = True
        If gv.Rows.Count > 1 Then
            For i As Integer = 1 To gv.Rows.Count - 1
                If clsCommon.myCdbl(gv.Rows(i).Cells(colSlabUpto).Value) <= clsCommon.myCdbl(gv.Rows(i - 1).Cells(colSlabUpto).Value) AndAlso clsCommon.myCdbl(gv.Rows(i).Cells(colSlabUpto).Value) > 0 AndAlso clsCommon.myLen(gv.Rows(i).Cells(colSlabUpto).Value) > 0 Then
                    rValue = False
                End If
            Next
        End If
        Return rValue
    End Function
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(fndcode.Value) <= 0 Then
                'fndcode.Focus()
                'fndcode.Select()
                txtTankerNo.Focus()
                txtTankerNo.Select()
                Errorcontrol.SetError(fndcode, "Please Fill Vehicle No.")
                Throw New Exception("Please Fill Vehicle No.")
            Else
                Errorcontrol.ResetError(fndcode)
            End If
            If isDuplicateVechiele(fndcode.Value) AndAlso clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                'fndcode.Focus()
                'fndcode.Select()
                txtTankerNo.Focus()
                txtTankerNo.Select()
                Errorcontrol.SetError(fndcode, "Duplicate Vehicle No.")
                Throw New Exception("Duplicate Vehicle No.")
            Else
                Errorcontrol.ResetError(fndcode)
            End If
            If clsCommon.myLen(txtdesc.Text) <= 0 Then
                txtdesc.Focus()
                txtdesc.Select()
                Errorcontrol.SetError(txtdesc, "Please Fill Vehicle Description")
                Throw New Exception("Please Fill Vehicle Description")
            Else
                Errorcontrol.ResetError(txtdesc)
            End If

            If clsCommon.myLen(txtprimarycode.Value) <= 0 Then
                txtprimarycode.Focus()
                txtprimarycode.Select()
                Errorcontrol.SetError(txtprimaryname, "Please Select Primary Transporter Code/Name")
                Throw New Exception("Please Select Primary Transporter Code/Name")
            Else
                Errorcontrol.ResetError(txtprimaryname)
            End If

            If clsCommon.myLen(txtmcccode.Value) <= 0 Then
                txtmcccode.Focus()
                txtmcccode.Select()
                Errorcontrol.SetError(txtmccname, "Please Select MCC Code/Name")
                Throw New Exception("Please Select MCC Code/Name")
            Else
                Errorcontrol.ResetError(txtmccname)
            End If

            If clsCommon.myLen(txtyear.Text) <= 0 Then
                txtyear.Focus()
                txtyear.Select()
                Errorcontrol.SetError(txtyear, "Please Fill Year of Manufacturing")
                Throw New Exception("Please Fill Year of Manufacturing")
            Else
                Errorcontrol.ResetError(txtyear)
            End If

            If Not rbtndiesel.Checked AndAlso Not rbtnratekm.Checked AndAlso Not rbtnrateltr.Checked AndAlso Not rbtnrental.Checked AndAlso Not rbtKmrange.Checked AndAlso Not chkMonthlyRentPlusDiesel.Checked Then
                RadGroupBox1.Focus()
                RadGroupBox1.Focus()
                Errorcontrol.SetError(rbtndiesel, "Please Select Any One From Basis of Freight Payments.")
                Throw New Exception("Please Select Any One From Basis of Freight Payments.")
            Else
                Errorcontrol.ResetError(rbtndiesel)
            End If

            If rbtndiesel.Checked AndAlso ((clsCommon.myLen(txtchrg.Text) <= 0 Or clsCommon.myCdbl(txtchrg.Text) <= 0) Or (clsCommon.myLen(txtavgkm.Text) <= 0 Or clsCommon.myCdbl(txtavgkm.Text) <= 0) Or (clsCommon.myLen(txtdiesel.Text) <= 0 Or clsCommon.myCdbl(txtavgkm.Text) <= 0)) Then
                If clsCommon.myCdbl(txtchrg.Text) <= 0 Then
                    txtchrg.Focus()
                    txtchrg.Select()
                    Errorcontrol.SetError(txtchrg, "Please Fill Charges Per Shift")
                ElseIf clsCommon.myCdbl(txtavgkm.Text) <= 0 Then
                    txtavgkm.Focus()
                    txtavgkm.Select()
                    Errorcontrol.SetError(txtchrg, "Please Fill Average KM per Ltr.")
                ElseIf clsCommon.myCdbl(txtdiesel.Text) <= 0 Then
                    txtdiesel.Focus()
                    txtdiesel.Select()
                    Errorcontrol.SetError(txtchrg, "Please Fill Rate of Diesel")
                End If
                Throw New Exception("Please Fill Charge per Shift/ Avg. K.M. per Ltr./ Rate of Diesel")
            Else
                Errorcontrol.ResetError(txtchrg)
                Errorcontrol.ResetError(txtavgkm)
                Errorcontrol.ResetError(txtdiesel)
            End If

            If rbtnratekm.Checked AndAlso (clsCommon.myLen(txt_km.Text) <= 0 Or clsCommon.myCdbl(txt_km.Text) <= 0) Then
                txt_km.Focus()
                txt_km.Select()
                Errorcontrol.SetError(txt_km, "Please Fill Rate per K.M")
                Throw New Exception("Please Fill Rate per K.M")
            Else
                Errorcontrol.ResetError(txt_km)
            End If


            If rbtnrental.Checked AndAlso clsCommon.myLen(cmbRentalType.Text) <= 0 Then
                Errorcontrol.SetError(cmbRentalType, "Please Fill any one from rental per Day/ Month/ Year")
                Throw New Exception("Please Select Any rental type as Day/Month/Year")
            Else
                Errorcontrol.ResetError(cmbRentalType)
            End If

            If rbtnrental.Checked AndAlso clsCommon.myLen(cmbRentalType.Text) > 0 AndAlso clsCommon.myCdbl(txtRentalAmt.Text) <= 0 Then
                Errorcontrol.SetError(txtRentalAmt, "Please Fill Rental amount")
                Throw New Exception("Please Fill Rental amount")
            Else
                Errorcontrol.ResetError(txtRentalAmt)
            End If
            If rbtnrateltr.Checked AndAlso (clsCommon.myLen(txt_ltr.Text) <= 0 Or clsCommon.myCdbl(txt_ltr.Text) <= 0 Or clsCommon.myLen(cmbLtrKG.Text) <= 0) Then
                If clsCommon.myCdbl(txt_ltr.Text) <= 0 Then
                    txt_ltr.Focus()
                    txt_ltr.Select()
                    Errorcontrol.SetError(txt_ltr, "Please Fill Rate per Ltr/KG")
                ElseIf clsCommon.myLen(cmbLtrKG.Text) <= 0 Then
                    cmbLtrKG.Focus()
                    Errorcontrol.SetError(cmbLtrKG, "Please select  Ltr/KG")
                End If
                Throw New Exception("Please Fill Rate per Ltr./Kg And  select Ltr./Kg")
            Else
                Errorcontrol.ResetError(txt_ltr)
                Errorcontrol.ResetError(cmbLtrKG)
            End If
            If chkMonthlyRentPlusDiesel.Checked Then
                If txtMRPDRent.Value <= 0 Then
                    Errorcontrol.SetError(txtMRPDRent, "Please provide monthly rent")
                End If
                If txtMRPDAverage.Value <= 0 Then
                    Errorcontrol.SetError(txtMRPDAverage, "Please enter average KM per ltr ")
                End If
                If txtMRPDDieselRate.Value <= 0 Then
                    Errorcontrol.SetError(txtMRPDDieselRate, "Please enter diesel rate")
                End If
            End If

            If rbtKmrange.Checked AndAlso isBlankGV() Then
                Throw New Exception("Please Fill Slab Value")
            End If

            If rbtKmrange.Checked AndAlso (Not isBlankGV()) AndAlso (Not isValidateGridValue()) Then
                Throw New Exception("Slab Value Should be in increasing Order")
            End If

            If settApplyEffectiveStartDate Then
                If Not txtEffectiveStartDate.Checked Then
                    txtEffectiveStartDate.Focus()
                    txtEffectiveStartDate.Select()
                    Errorcontrol.SetError(txtEffectiveStartDate, "Please Select Effective Start Date")
                    Throw New Exception("Please Select Effective Start Date")
                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Function

    Sub SaveData()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmPrimaryTransporterVehicalMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim obj As New clsfrmPrimaryTransporterVehicalMaster()
            obj.docno = clsCommon.myCstr(fndcode.Value)
            obj.desc = clsCommon.myCstr(txtdesc.Text.Replace("'", "`"))
            obj.primarycode = clsCommon.myCstr(txtprimarycode.Value)
            obj.primaryname = clsCommon.myCstr(txtprimaryname.Text)
            obj.mcccode = clsCommon.myCstr(txtmcccode.Value)
            obj.mccname = clsCommon.myCstr(txtmccname.Text)
            obj.capacity = clsCommon.myCdbl(txtcapcity.Text)
            obj.year = clsCommon.myCstr(txtyear.Text)
            obj.pricekm = clsCommon.myCdbl(txt_km.Text)
            obj.Rate_Type = clsCommon.myCstr(cmbLtrKG.Text)
            obj.Price_Ltr_KG = clsCommon.myCdbl(txt_ltr.Text)
            obj.chagrshift = clsCommon.myCdbl(txtchrg.Text)
            obj.avgrate = clsCommon.myCdbl(txtavgkm.Text)
            obj.dieselrate = clsCommon.myCdbl(txtdiesel.Text)
            obj.RentalType = clsCommon.myCstr(cmbRentalType.Text)
            obj.RentalAmount = clsCommon.myCdbl(txtRentalAmt.Text)
            obj.Vehicle_Weight = txtVehicleWeight.Value
            obj.Two_Way = chkTwoWay.Checked
            If txtEffectiveStartDate.Checked Then
                obj.Effective_Start_Date = txtEffectiveStartDate.Value
            End If
            obj.Vehicle = clsCommon.myCstr(txtVehicle.Text)
            'DATE : 24-01-2017 > CLIENT : SAHAYOG DAIRY > TICKET/REQUEST : SCMPLREQ000002
            Try
                If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VehicleFitnessAndInsuranceFields,
                                                                  clsFixedParameterType.VehicleFitnessAndInsuranceFields,
                                                                                        Nothing)) = 1, True, False) Then

                    If txtVehInsuranceNo.Text IsNot Nothing AndAlso clsCommon.myLen(txtVehInsuranceNo.Text) > 0 Then
                        obj.Veh_Insurance_No = txtVehInsuranceNo.Text
                    End If
                    If clsCommon.myLen(dtpInsurance.Value) > 0 Then
                        obj.Veh_Insurance_Date = dtpInsurance.Value.ToShortDateString()
                    End If
                    If txtVehFitnessNo.Text IsNot Nothing AndAlso clsCommon.myLen(txtVehFitnessNo.Text) > 0 Then
                        obj.Veh_Fitness_No = txtVehFitnessNo.Text
                    End If
                    If clsCommon.myLen(dtpVehFitness.Value) > 0 Then
                        obj.Veh_Fitness_Date = dtpVehFitness.Value.ToShortDateString()
                    End If

                End If
            Catch ex As Exception
            End Try

            obj.status = ""
            If rbtndiesel.IsChecked Then
                obj.status = "Day/Diesel"
            ElseIf rbtnratekm.IsChecked Then
                obj.status = "Rate/K.M"
            ElseIf rbtnrateltr.IsChecked Then
                obj.status = "Rate/Ltr"
            ElseIf rbtnrental.IsChecked Then
                obj.status = "Rental"
            ElseIf rbtKmrange.IsChecked Then
                obj.status = "KM_Range"
                obj.Is_Additional = chkIsAdditional.Checked
            ElseIf chkMonthlyRentPlusDiesel.Checked Then
                obj.status = "Rental/Diesel"
                obj.RentalAmount = txtMRPDRent.Value
                obj.avgrate = txtMRPDAverage.Value
                obj.dieselrate = txtMRPDDieselRate.Value
            End If


            If clsCommon.myLen(obj.desc) > 0 Then
                'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If clsfrmPrimaryTransporterVehicalMaster.SaveData(obj.docno, isNewEntry, obj) Then
                    Dim objSlab As clsSlabRangeDetail = Nothing
                    Dim arrObjSlab As List(Of clsSlabRangeDetail) = Nothing
                    clsSlabRangeDetail.deleteData(Me.Form_ID, obj.docno, Nothing)
                    If rbtKmrange.Checked Then
                        arrObjSlab = New List(Of clsSlabRangeDetail)
                        For i As Integer = 0 To gv.Rows.Count - 1
                            If clsCommon.myCdbl(gv.Rows(i).Cells(colSlabUpto).Value) > 0 AndAlso clsCommon.myCdbl(gv.Rows(i).Cells(colSlabRate).Value) > 0 Then
                                objSlab = New clsSlabRangeDetail()
                                objSlab.Form_ID = Me.Form_ID
                                objSlab.Trans_ID = obj.docno
                                objSlab.Slab_Upto = clsCommon.myCdbl(gv.Rows(i).Cells(colSlabUpto).Value)
                                objSlab.Slab_Rate = clsCommon.myCdbl(gv.Rows(i).Cells(colSlabRate).Value)
                                arrObjSlab.Add(objSlab)
                            End If
                        Next
                    End If

                    If arrObjSlab IsNot Nothing AndAlso arrObjSlab.Count > 0 Then
                        clsSlabRangeDetail.SaveData(arrObjSlab, Nothing)
                    End If
                    If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                    End If

                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                    txtTankerNo.Text = obj.docno
                    'fndcode.Value = obj.docno
                    fndcode.MyReadOnly = True
                    txtTankerNo.ReadOnly = True
                    UcAttachment1.SaveData(fndcode.Value)
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                    fndcode.MyReadOnly = False
                    txtTankerNo.ReadOnly = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If clsCommon.myLen(fndcode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Vehicle No. For Deletion", Me.Text)
            'fndcode.Focus()
            'fndcode.Select()
            txtTankerNo.Focus()
            txtTankerNo.Select()
            Errorcontrol.SetError(fndcode, "Please Select Vehicle No. For Deletion")
            Return
        Else
            Errorcontrol.ResetError(fndcode)
        End If

        Dim qry As String = "select count(*) from TSPL_Primary_Vehicle_Master where vehicle_code='" + fndcode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found For Deletion", Me.Text)
            Return
        End If

        If Not clsCommon.MyMessageBoxShow("Are You Sure,Want To Delete Primary Transporter Vehicle Master of Vehicle No. " + fndcode.Value + "?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            qry = "delete from TSPL_Primary_Vehicle_Master where vehicle_code='" + fndcode.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsSlabRangeDetail.deleteData(Me.Form_ID, fndcode.Value, trans)
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

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub



    Private Sub txtprimarycode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtprimarycode._MYValidating
        txtprimarycode.Value = clsVendorMaster.getFinder(" Form_type='PTM'", txtprimarycode.Value, isButtonClicked)

        If clsCommon.myLen(txtprimarycode.Value) > 0 Then
            txtprimaryname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select vendor_name from tspl_vendor_master where vendor_code='" + txtprimarycode.Value + "' and form_type='PTM'"))
        Else
            txtprimaryname.Text = ""
        End If
    End Sub

    Private Sub txtmcccode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtmcccode._MYValidating
        Dim whrcls As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrcls = " TSPL_MCC_MASTER.mcc_code in (" + arrLoc + ")"
        End If

        txtmcccode.Value = clsMccMaster.getFinder(whrcls, txtmcccode.Value, isButtonClicked)


        If clsCommon.myLen(txtmcccode.Value) > 0 Then
            txtmccname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_code='" + txtmcccode.Value + "'"))
        Else
            txtmccname.Text = ""
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsfrmPrimaryTransporterVehicalMaster = clsfrmPrimaryTransporterVehicalMaster.GetData(strCode, arrLoc, NavType)
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.docno) > 0 Then
                Reset()
                isNewEntry = False
                txtTankerNo.Text = obj.docno
                txtdesc.Text = obj.desc
                txtprimarycode.Value = obj.primarycode
                txtprimaryname.Text = obj.primaryname
                txtmcccode.Value = obj.mcccode
                txtmccname.Text = obj.mccname
                txtcapcity.Text = obj.capacity
                txtyear.Text = obj.year
                txt_km.Text = obj.pricekm
                cmbLtrKG.Text = obj.Rate_Type
                txt_ltr.Text = obj.Price_Ltr_KG
                txtchrg.Text = obj.chagrshift
                txtdiesel.Text = obj.dieselrate
                txtavgkm.Text = obj.avgrate
                cmbRentalType.Text = clsCommon.myCstr(obj.RentalType)
                txtRentalAmt.Text = clsCommon.myCdbl(obj.RentalAmount)
                txtVehicleWeight.Value = obj.Vehicle_Weight
                chkTwoWay.Checked = obj.Two_Way
                fndcode.MyReadOnly = True
                txtTankerNo.ReadOnly = True
                If obj.Effective_Start_Date IsNot Nothing Then
                    txtEffectiveStartDate.Checked = True
                    txtEffectiveStartDate.Value = obj.Effective_Start_Date
                Else
                    txtEffectiveStartDate.Checked = False
                End If
                If clsCommon.CompairString(obj.status, "Day/Diesel") = CompairStringResult.Equal Then
                    rbtndiesel.IsChecked = True
                ElseIf clsCommon.CompairString(obj.status, "Rate/K.M") = CompairStringResult.Equal Then
                    rbtnratekm.IsChecked = True
                ElseIf clsCommon.CompairString(obj.status, "Rate/Ltr") = CompairStringResult.Equal Then
                    rbtnrateltr.IsChecked = True
                ElseIf clsCommon.CompairString(obj.status, "Rental") = CompairStringResult.Equal Then
                    rbtnrental.IsChecked = True
                ElseIf clsCommon.CompairString(obj.status, "KM_Range") = CompairStringResult.Equal Then
                    rbtKmrange.IsChecked = True
                ElseIf clsCommon.CompairString(obj.status, "Rental/Diesel") = CompairStringResult.Equal Then
                    chkMonthlyRentPlusDiesel.IsChecked = True
                    txtMRPDRent.Value = obj.RentalAmount
                    txtMRPDAverage.Value = obj.avgrate
                    txtMRPDDieselRate.Value = obj.dieselrate

                    txtRentalAmt.Text = ""
                    txtavgkm.Text = ""
                    txtdiesel.Text = ""
                End If


                obj.status = "Rental/Diesel"



                If rbtKmrange.IsChecked Then
                    Dim arrObjSlab As List(Of clsSlabRangeDetail) = clsSlabRangeDetail.getData(Me.Form_ID, obj.docno)
                    If arrObjSlab IsNot Nothing AndAlso arrObjSlab.Count > 0 Then
                        gv.Rows.Clear()
                        For i As Integer = 0 To arrObjSlab.Count - 1
                            gv.Rows.AddNew()
                            gv.Rows(i).Cells(colSlabUpto).Value = arrObjSlab.Item(i).Slab_Upto
                            gv.Rows(i).Cells(colSlabRate).Value = arrObjSlab.Item(i).Slab_Rate
                        Next
                    End If
                    chkIsAdditional.Checked = obj.Is_Additional
                End If


                btnsave.Text = "Update"
                btndelete.Enabled = True
                FndRoute.Value = obj.Route_Code
                LblRoute.Text = obj.Route_Code
                UcAttachment1.LoadData(fndcode.Value)

                'DATE : 24-01-2017 > CLIENT : SAHAYOG DAIRY > TICKET/REQUEST : SCMPLREQ000002
                txtVehInsuranceNo.Text = obj.Veh_Insurance_No
                dtpInsurance.Text = obj.Veh_Insurance_Date
                txtVehFitnessNo.Text = obj.Veh_Fitness_No
                dtpVehFitness.Text = obj.Veh_Fitness_Date
                txtVehicle.Text = obj.Vehicle
            Else
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
        Dim qry As String = "select count(*) from TSPL_Primary_Vehicle_Master where vehicle_code='" + fndcode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If check > 0 Then
            fndcode.MyReadOnly = True
            txtTankerNo.ReadOnly = True
        Else
            fndcode.MyReadOnly = False
            txtTankerNo.ReadOnly = False
        End If
        Dim whrcls As String = ""
        If fndcode.MyReadOnly Or isButtonClicked Then
            qry = "select TSPL_Primary_Vehicle_Master.vehicle_code as Code,TSPL_Primary_Vehicle_Master.Description,TSPL_Primary_Vehicle_Master.vendor_code as [Primary Transporter Code],tspl_vendor_master.vendor_name as [Primary Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code as [MCC Code],tspl_mcc_master.mcc_name as [MCC Name],TSPL_Primary_Vehicle_Master.storage_capacity as [Storage Capacity],TSPL_Primary_Vehicle_Master.manufacturing_year as [Manufacturing Year],Shift_Charges as [Charges per Day],Avg_Km_Ltr as [Average K.M per Ltr],Diesel_Rate as [Rate of Diesel],Rental_type as [Rental Type],Rental_amount as [Rental Amount],TSPL_Primary_Vehicle_Master.price_km as [Rate per KM],Rate_type as [Rate Type],TSPL_Primary_Vehicle_Master.price_ltr_Kg as [Price Per Ltr/KG] "
            If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
                qry += " ,TSPL_Primary_Vehicle_Master.Vehicle "
            End If
            qry += " from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code"
            If clsCommon.myLen(arrLoc) > 0 Then
                whrcls = " tspl_primary_vehicle_master.mcc_code in (" + arrLoc + ")"
            End If
            fndcode.Value = clsCommon.ShowSelectForm("PTVFND", qry, "Code", whrcls, fndcode.Value, "Code", isButtonClicked)
            LoadData(fndcode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub Export_Vehical_Details_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export_Vehical_Details.Click 'btnexport.Click
        Dim qry As String = "select count(*) from TSPL_Primary_Vehicle_Master"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If check > 0 Then
            qry = "select TSPL_Primary_Vehicle_Master.vehicle_code as Code,TSPL_Primary_Vehicle_Master.Description,TSPL_Primary_Vehicle_Master.vendor_code as [Primary Transporter Code],tspl_vendor_master.vendor_name as [Primary Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code as [MCC Code],tspl_mcc_master.mcc_name as [MCC Name],TSPL_Primary_Vehicle_Master.storage_capacity as [Storage Capacity],TSPL_Primary_Vehicle_Master.manufacturing_year as [Manufacturing Year],TSPL_Primary_Vehicle_Master.STATUS as [Payment Method(Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel)],Shift_Charges as [Charges per Day],Avg_Km_Ltr as [Average KM per Ltr],Diesel_Rate as [Rate of Diesel],Rental_type as [Rental Type],Rental_Amount as [Rental Amount],TSPL_Primary_Vehicle_Master.price_km as [Rate per KM],Rate_type as [Rate Type],Price_ltr_kg as [Price Ltr/KG],TSPL_Primary_Vehicle_Master.Vehicle_Weight as [Vehicle Weight],TSPL_Primary_Vehicle_Master.Veh_Fitness_No , TSPL_Primary_Vehicle_Master.Veh_Fitness_Date , TSPL_Primary_Vehicle_Master.Veh_Insurance_No,TSPL_Primary_Vehicle_Master.Veh_Insurance_Date,(case when TSPL_Primary_Vehicle_Master.Two_Way=1 then 'Y' else 'N' end) as [Two Way(Y/N)] "
            If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
                qry += ",TSPL_Primary_Vehicle_Master.Vehicle "
            End If
            qry += " from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code"
        Else
            qry = "select '' as Code,'' as Description,'' as [Primary Transporter Code],'' as [Primary Transporter Name],'' as [MCC Code],'' as [MCC Name],'' as [Storage Capacity],'' as [Manufacturing Year],'' as [Payment Method(Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel)],0 as [Charges per Day],0 as [Average KM per Ltr],0 as [Rate of Diesel],'' as [Rental Type],0 as [Rental Amount],0 as [Rate per KM],'' as [Rate Type],0 as [Price Ltr/KG],0 as [Vehicle Weight] , '' as Veh_Fitness_No , '' as Veh_Fitness_Date , '' as Veh_Insurance_No,'' as Veh_Insurance_Date,'' as [Two Way(Y/N)]  "
            If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
                qry += ",'' as Vehicle "
            End If
        End If
        ListImpExpColumnsMandatory = New List(Of String)({"Code", "Description", "Primary Transporter Code", "Primary Transporter Name", "MCC Code", "MCC Name", "Manufacturing Year", "Charges per Day", "Average KM per Ltr", "Rate of Diesel", "Rental Amount"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
        transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub Export_Slab_Details_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export_Slab_Details.Click 'btnexport.Click
        Dim qry As String = ""
        qry = "select TSPL_Primary_Vehicle_Master.vehicle_code as Code,TSPL_Primary_Vehicle_Master.Description,Slab_Upto as [Slab Upto],Slab_Rate as [Slab Rate],Is_Additional as [Is Additional(T/F)] from TSPL_Primary_Vehicle_Master " _
        & "  left outer join tspl_slab_range_detail on tspl_slab_range_detail.Trans_Id=TSPL_Primary_Vehicle_Master.Vehicle_Code and " _
        & " tspl_slab_range_detail.form_Id='PTV-MST'"
        ListImpExpColumnsMandatory = New List(Of String)({"Code", "Slab Upto", "Slab Rate"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
        transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "Slab_Details")
    End Sub

    Private Sub Import_Vehical_Details_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import_Vehical_Details.Click 'btnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim inputs() As String = {}
        If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
            inputs = {"Code", "Description", "Primary Transporter Code", "Primary Transporter Name", "MCC Code", "MCC Name", "Storage Capacity", "Manufacturing Year", "Payment Method(Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel)", "Charges per Day", "Average KM per Ltr", "Rate of Diesel", "Rental Type", "Rental Amount", "Rate per KM", "Rate Type", "Price Ltr/KG", "Vehicle Weight", "Veh_Fitness_No", "Veh_Fitness_Date", "Veh_Insurance_No", "Veh_Insurance_Date", "Two Way(Y/N)", "Vehicle"}
        Else
            inputs = {"Code", "Description", "Primary Transporter Code", "Primary Transporter Name", "MCC Code", "MCC Name", "Storage Capacity", "Manufacturing Year", "Payment Method(Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel)", "Charges per Day", "Average KM per Ltr", "Rate of Diesel", "Rental Type", "Rental Amount", "Rate per KM", "Rate Type", "Price Ltr/KG", "Vehicle Weight", "Veh_Fitness_No", "Veh_Fitness_Date", "Veh_Insurance_No", "Veh_Insurance_Date", "Two Way(Y/N)"}
        End If
        Dim Strs As List(Of String) = New List(Of String)(inputs)
        If transportSql.importExcel(gv, Strs.ToArray()) Then
            Dim counter As Integer = 1
            Try
                Dim obj As clsfrmPrimaryTransporterVehicalMaster
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsfrmPrimaryTransporterVehicalMaster()
                    Dim index As Integer = 0

                    obj.docno = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(obj.docno) <= 0 Then
                        Throw New Exception("Please Fill Vehicle No.(Code) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(obj.docno) > 30 Then
                        Throw New Exception("Length of Vehicle No.(Code) Should Not Exceed 30 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(obj.docno) > 0 Then
                        index = obj.docno.IndexOf(" ")

                        If index > 0 AndAlso index < clsCommon.myLen(obj.docno) Then
                            Throw New Exception("There Should Be No white Space Between Vehicle No. At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    '-----------------------------

                    obj.desc = clsCommon.myCstr(grow.Cells("Description").Value).Replace("'", "`")
                    If clsCommon.myLen(obj.desc) <= 0 Or clsCommon.myLen(obj.desc) > 150 Then
                        Throw New Exception("Please Fill Description And Length Should Not Exceed Max.150 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim qry As String = ""

                    obj.primarycode = clsCommon.myCstr(grow.Cells("Primary Transporter Code").Value)
                    obj.primaryname = clsCommon.myCstr(grow.Cells("Primary Transporter Name").Value).Replace("'", "`")
                    If clsCommon.myLen(obj.primarycode) <= 0 AndAlso clsCommon.myLen(obj.primaryname) <= 0 Then
                        Throw New Exception("Please Fill Primary Transporter Code/Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(obj.primarycode) > 0 Then
                        qry = "select count(*) from tspl_vendor_master where vendor_code='" + obj.primarycode + "' and form_type='PTM'"
                        index = clsDBFuncationality.getSingleValue(qry)

                        If index <= 0 Then
                            qry = "select vendor_code from tspl_vendor_master where vendor_name='" + obj.primaryname + "' and form_type='PTM'"
                            obj.primarycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                            If obj.primarycode IsNot Nothing AndAlso clsCommon.myLen(obj.primarycode) <= 0 Then
                                Throw New Exception("Filled Primary Transporter Code Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                    ElseIf clsCommon.myLen(obj.primaryname) > 0 Then
                        qry = "select vendor_code from tspl_vendor_master where vendor_name='" + obj.primaryname + "' and form_type='PTM'"
                        obj.primarycode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                        If obj.primarycode IsNot Nothing AndAlso clsCommon.myLen(obj.primarycode) <= 0 Then
                            Throw New Exception("Filled Primary Transporter Code/Name Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    '-------------------------------------------------------------

                    obj.mcccode = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                    obj.mccname = clsCommon.myCstr(grow.Cells("MCC Name").Value).Replace("'", "`")
                    If clsCommon.myLen(obj.mcccode) <= 0 AndAlso clsCommon.myLen(obj.mccname) <= 0 Then
                        Throw New Exception("Please Fill MCC Code/Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(obj.mcccode) > 0 Then
                        qry = "select count(*) from tspl_mcc_master where mcc_code='" + obj.mcccode + "'"
                        index = clsDBFuncationality.getSingleValue(qry)

                        If index <= 0 Then
                            qry = "select mcc_code from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                            obj.mcccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                            If obj.mcccode IsNot Nothing AndAlso clsCommon.myLen(obj.mcccode) <= 0 Then
                                Throw New Exception("Filled MCC Code Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                    ElseIf clsCommon.myLen(obj.mccname) > 0 Then
                        qry = "select mcc_code from tspl_mcc_master where mcc_name='" + obj.mccname + "'"
                        obj.mcccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                        If obj.mcccode IsNot Nothing AndAlso clsCommon.myLen(obj.mcccode) <= 0 Then
                            Throw New Exception("Filled MCC Code/Name Is Invalid Or Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If
                    '---------------------

                    '------------check primary transporter mapped with other mcc-----------------
                    Dim checkmcccode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_code from tspl_primary_vehicle_master where vendor_code='" + obj.primarycode + "'"))
                    If clsCommon.myLen(checkmcccode) > 0 AndAlso clsCommon.CompairString(checkmcccode, obj.mcccode) <> CompairStringResult.Equal Then
                        Throw New Exception("Filled MCC Code/Name Is Invalid" + Environment.NewLine + "Primary Transporter Code Is Mapped With Other MCC Code i.e (" + checkmcccode + ") At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    '------------------------------------------------------------------------

                    obj.capacity = clsCommon.myCdbl(grow.Cells("Storage Capacity").Value)
                    obj.year = clsCommon.myCstr(grow.Cells("Manufacturing Year").Value)
                    If clsCommon.myLen(obj.year) <= 0 Then
                        Throw New Exception("Please Fill Manufacturing Year At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    '--------------------
                    obj.Vehicle_Weight = clsCommon.myCdbl(grow.Cells("Vehicle Weight").Value)
                    obj.chagrshift = clsCommon.myCdbl(grow.Cells("Charges per Day").Value)
                    obj.avgrate = clsCommon.myCdbl(grow.Cells("Average KM per Ltr").Value)
                    obj.dieselrate = clsCommon.myCdbl(grow.Cells("Rate of Diesel").Value)
                    obj.RentalType = clsCommon.myCstr(grow.Cells("Rental Type").Value)
                    obj.RentalAmount = clsCommon.myCdbl(grow.Cells("Rental Amount").Value)
                    obj.pricekm = clsCommon.myCdbl(grow.Cells("Rate per KM").Value)
                    obj.Rate_Type = clsCommon.myCstr(grow.Cells("Rate Type").Value)
                    obj.Price_Ltr_KG = clsCommon.myCdbl(grow.Cells("Price Ltr/KG").Value)

                    'DATE:25-JAN-2017 >  CLIENT : SAHAYOG DAIRY > TICKET/REQUEST : SCMPLREQ000002
                    obj.Veh_Fitness_No = clsCommon.myCstr(grow.Cells("Veh_Fitness_No").Value)
                    obj.Veh_Fitness_Date = clsCommon.myCstr(grow.Cells("Veh_Fitness_Date").Value)
                    obj.Veh_Insurance_No = clsCommon.myCstr(grow.Cells("Veh_Insurance_No").Value)
                    obj.Veh_Insurance_Date = clsCommon.myCstr(grow.Cells("Veh_Insurance_Date").Value)

                    obj.Two_Way = (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Two Way(Y/N)").Value), "Y") = CompairStringResult.Equal)
                    If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
                        obj.Vehicle = clsCommon.myCstr(grow.Cells("Vehicle").Value)
                    End If
                    obj.status = clsCommon.myCstr(grow.Cells("Payment Method(Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel)").Value)
                    If clsCommon.CompairString(obj.status, "Day/Diesel") = CompairStringResult.Equal Then
                        If obj.chagrshift <= 0 Then
                            Throw New Exception("Please Fill Charges per Day At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If obj.avgrate <= 0 Then
                            Throw New Exception("Please Fill Average KM per Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If obj.dieselrate <= 0 Then
                            Throw New Exception("Please Fill Rate of Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(obj.status, "Rental") = CompairStringResult.Equal Then
                        If obj.RentalAmount <= 0 Then
                            Throw New Exception("Please Fill Rental Amount At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If Not (clsCommon.CompairString(obj.RentalType, "Day") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "Month") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "Year") = CompairStringResult.Equal) Then
                            Throw New Exception("Rental Type should be Day,Month,Year  At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(obj.status, "Rate/Ltr") = CompairStringResult.Equal Then
                        If obj.Price_Ltr_KG <= 0 Then
                            Throw New Exception("Please Fill Price Ltr/KG At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If Not (clsCommon.CompairString(obj.Rate_Type, "LTR") = CompairStringResult.Equal Or clsCommon.CompairString(obj.RentalType, "KG") = CompairStringResult.Equal) Then
                            Throw New Exception("Rate Type should be LTR,KG  At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(obj.status, "Rate/K.M") = CompairStringResult.Equal Then
                        If obj.pricekm <= 0 Then
                            Throw New Exception("Please Fill Rate per KM At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(obj.status, "Rental/Diesel") = CompairStringResult.Equal Then
                        If obj.RentalAmount <= 0 Then
                            Throw New Exception("Please Fill Rental Amount At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If obj.avgrate <= 0 Then
                            Throw New Exception("Please Fill Average KM per Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If obj.dieselrate <= 0 Then
                            Throw New Exception("Please Fill Rate of Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(obj.status, "KM_Range") = CompairStringResult.Equal Then
                    ElseIf clsCommon.myLen(obj.status) > 0 Then
                        Throw New Exception("Payment method should be Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    qry = "select count(*) from TSPL_Primary_Vehicle_Master where vehicle_code='" + obj.docno + "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                    If check > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True
                    End If

                    If clsCommon.myLen(obj.docno) > 0 Then
                        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        If clsfrmPrimaryTransporterVehicalMaster.SaveData(obj.docno, isNewEntry, obj) Then
                        Else
                            Throw New Exception("No Data Transfer")
                        End If
                    End If
                    counter += 1
                Next
                clsCommon.ProgressBarHide()
                'trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
            Catch ex As Exception
                'trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub Import_Slab_Details_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import_Slab_Details.Click 'btnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Code", "Description", "Slab Upto", "Slab Rate", "Is Additional(T/F)") Then
            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim counter As Integer = 1

            Try
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim arrSlab As New List(Of clsSlabRangeDetail)
                Dim arrDistinct As New List(Of clsSlabRangeDetail)
                Dim NewValue As String = String.Empty
                Dim OldVale As String = String.Empty
                Dim obj As New clsSlabRangeDetail
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsSlabRangeDetail()
                    Dim qry As String = ""
                    Dim index As Integer = 0

                    obj.Trans_ID = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(obj.Trans_ID) <= 0 Then
                        Throw New Exception("Please Fill Vehicle No.(Code) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(obj.Trans_ID) > 0 Then
                        qry = "select count(*) from TSPL_Primary_Vehicle_Master where vehicle_code='" + obj.Trans_ID + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Please Fill Vehicle No.(Code) At Line No. " + clsCommon.myCstr(counter) + ".Filled Code dose not exits.")
                        End If
                    End If
                    Dim is_additional As String = clsCommon.myCstr(grow.Cells("Is Additional(T/F)").Value)

                    obj.Slab_Upto = clsCommon.myCdbl(grow.Cells("Slab Upto").Value)
                    If clsCommon.myLen(obj.Slab_Upto) <= 0 Then
                        Throw New Exception("Please Fill Slab Upto At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    obj.Slab_Rate = clsCommon.myCdbl(grow.Cells("Slab Rate").Value)
                    If clsCommon.myLen(obj.Slab_Rate) <= 0 Then
                        Throw New Exception("Please Fill Slab Rate At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    '-------------------------------------------------------------
                    obj.Form_ID = clsUserMgtCode.frmPrimaryTransporterVehicalMaster

                    NewValue = obj.Trans_ID
                    '=================UPdate By Preeti Gupta=======================
                    If clsCommon.myLen(obj.Trans_ID) > 0 Then
                        arrSlab.Add(obj)

                        If clsCommon.CompairString(OldVale, NewValue) <> CompairStringResult.Equal Then
                            arrDistinct.Add(obj)
                        End If ''
                        OldVale = obj.Trans_ID
                    End If
                    ' ======================================================================

                    Dim squery As String = "update TSPL_Primary_Vehicle_Master set status='KM_Range',is_additional='" & is_additional & "' where vehicle_code='" & obj.Trans_ID & "'"
                    clsDBFuncationality.ExecuteNonQuery(squery, trans)
                    counter += 1

                Next

                If clsCommon.myLen(obj.Trans_ID) > 0 Then
                    If clsSlabRangeDetail.SaveBulkData(arrDistinct, arrSlab, trans) Then
                        trans.Commit()
                    Else
                        Throw New Exception("No Data Transfer")
                    End If
                End If
                clsCommon.ProgressBarHide()
                'trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
            Catch ex As Exception
                'trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub FrmPrimaryTransporterVehicalMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub fndcode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Asc(e.KeyChar) >= 48 AndAlso Asc(e.KeyChar) <= 57) OrElse (Asc(e.KeyChar) >= 65 AndAlso Asc(e.KeyChar) <= 90) OrElse (Asc(e.KeyChar) >= 97 AndAlso Asc(e.KeyChar) <= 122) OrElse Asc(e.KeyChar) = Keys.Back OrElse Asc(e.KeyChar) = Keys.Delete Then
        Else

            e.Handled = True
        End If
    End Sub


    Private Sub fndcode_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            If clsCommon.myLen(fndcode.Value) > 0 Then
                Dim index As Integer = fndcode.Value.IndexOf(" ")

                If index > 0 AndAlso index < CInt(fndcode.Value.Length) Then
                    'fndcode.Value = ""
                    'fndcode.Focus()
                    'fndcode.Select()
                    txtTankerNo.Text = ""
                    txtTankerNo.Focus()
                    txtTankerNo.Select()
                    Errorcontrol.SetError(fndcode, "No White Space Allowed In Vehicle No.,Please Remvoe All White Space")
                    Throw New Exception("No White Space Allowed In Vehicle No.,Please Remvoe All White Space")
                Else
                    Errorcontrol.ResetError(fndcode)
                End If
            Else
                Errorcontrol.ResetError(fndcode)
            End If
        Catch ex As Exception
            Errorcontrol.SetError(fndcode, "No White Space Allowed In Vehicle No.,Please Remvoe All White Space")
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtndiesel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtndiesel.Click
        ClickMethod()
    End Sub

    Private Sub rbtndiesel_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtndiesel.ToggleStateChanged
        If rbtndiesel.IsChecked = True Then
            txtchrg.Enabled = True
            txtavgkm.Enabled = True
            txtdiesel.Enabled = True
            txtchrg.MendatroryField = True
            txtavgkm.MendatroryField = True
            txtdiesel.MendatroryField = True
        Else
            txtchrg.Text = ""
            txtavgkm.Text = ""
            txtdiesel.Text = ""
            txtchrg.Enabled = False
            txtavgkm.Enabled = False
            txtdiesel.Enabled = False
            txtchrg.MendatroryField = False
            txtavgkm.MendatroryField = False
            txtdiesel.MendatroryField = False
        End If
    End Sub

    Private Sub rbtnrental_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnrental.Click
        ClickMethod()
    End Sub

    Private Sub rbtnrental_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnrental.ToggleStateChanged
        'If rbtnrental.Checked = True Then
        '    txtrental_day.Enabled = True
        '    txtrental_month.Enabled = True
        '    txtrental_week.Enabled = True
        '    txtrental_day.MendatroryField = True
        '    txtrental_month.MendatroryField = True
        '    txtrental_week.MendatroryField = True
        'Else
        '    txtrental_day.Text = ""
        '    txtrental_month.Text = ""
        '    txtrental_week.Text = ""
        '    txtrental_day.Enabled = False
        '    txtrental_month.Enabled = False
        '    txtrental_week.Enabled = False
        '    txtrental_day.MendatroryField = False
        '    txtrental_month.MendatroryField = False
        '    txtrental_week.MendatroryField = False
        'End If
        If rbtnrental.Checked = True Then
            cmbRentalType.Enabled = True
            'cmbRentalType.SelectedIndex = 0
            cmbRentalType.MendatroryField = True
            txtRentalAmt.Enabled = True
            '            txtRentalAmt.Text = "0"
            txtRentalAmt.MendatroryField = True
            'txtrental_day.Enabled = True
            'txtrental_month.Enabled = True
            'txtrental_week.Enabled = True
            'txtrental_day.MendatroryField = True
            'txtrental_month.MendatroryField = True
            'txtrental_week.MendatroryField = True
        Else
            cmbRentalType.Enabled = False
            cmbRentalType.Text = ""
            cmbRentalType.MendatroryField = False
            txtRentalAmt.Enabled = False
            txtRentalAmt.Text = Nothing
            txtRentalAmt.MendatroryField = False

            'txtrental_day.Text = Nothing
            'txtrental_month.Text = Nothing
            'txtrental_week.Text = Nothing
            'txtrental_day.Enabled = False
            'txtrental_month.Enabled = False
            'txtrental_week.Enabled = False
            'txtrental_day.MendatroryField = False
            'txtrental_month.MendatroryField = False
            'txtrental_week.MendatroryField = False
        End If
    End Sub

    Private Sub rbtnrateltr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnrateltr.Click
        ClickMethod()
    End Sub

    Sub ClickMethod()
        rbtndiesel.IsChecked = False
        rbtnratekm.IsChecked = False
        rbtnrateltr.IsChecked = False
        rbtnrental.IsChecked = False
        rbtKmrange.Checked = False
        chkMonthlyRentPlusDiesel.Checked = False
    End Sub

    Private Sub rbtnrateltr_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnrateltr.ToggleStateChanged
        'If rbtnrateltr.Checked = True Then
        '    txt_ltr.Enabled = True
        '    txt_ltr_kg.Enabled = True
        '    txt_ltr.MendatroryField = True
        '    txt_ltr_kg.MendatroryField = True
        'Else
        '    txt_ltr.Text = ""
        '    txt_ltr_kg.Text = ""
        '    txt_ltr.Enabled = False
        '    txt_ltr_kg.Enabled = False
        '    txt_ltr.MendatroryField = False
        '    txt_ltr_kg.MendatroryField = False
        'End If

        If rbtnrateltr.Checked = True Then
            cmbLtrKG.Enabled = True
            'cmbRentalType.SelectedIndex = 0
            cmbLtrKG.MendatroryField = True
            txt_ltr.Enabled = True
            '            txtRentalAmt.Text = "0"
            txt_ltr.MendatroryField = True
            'txtrental_day.Enabled = True
            'txtrental_month.Enabled = True
            'txtrental_week.Enabled = True
            'txtrental_day.MendatroryField = True
            'txtrental_month.MendatroryField = True
            'txtrental_week.MendatroryField = True
        Else
            cmbLtrKG.Enabled = False
            cmbLtrKG.Text = ""
            cmbLtrKG.MendatroryField = False
            txt_ltr.Enabled = False
            txt_ltr.Text = Nothing
            txt_ltr.MendatroryField = False

            'txtrental_day.Text = Nothing
            'txtrental_month.Text = Nothing
            'txtrental_week.Text = Nothing
            'txtrental_day.Enabled = False
            'txtrental_month.Enabled = False
            'txtrental_week.Enabled = False
            'txtrental_day.MendatroryField = False
            'txtrental_month.MendatroryField = False
            'txtrental_week.MendatroryField = False
        End If
    End Sub

    Private Sub rbtnratekm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnratekm.Click
        ClickMethod()
    End Sub

    Private Sub rbtnratekm_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnratekm.ToggleStateChanged
        If rbtnratekm.Checked = True Then
            txt_km.Enabled = True
            txt_km.MendatroryField = True
        Else
            txt_km.Enabled = False
            txt_km.MendatroryField = False
            txt_km.Text = ""
        End If
    End Sub

    Private Sub rbtKmrange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtKmrange.Click
        ClickMethod()
    End Sub

    Private Sub rbtKmrange_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtKmrange.ToggleStateChanged
        If rbtKmrange.Checked = True Then
            'Txt1to10Km.Enabled = True
            'Txt10to20Km.Enabled = True
            'TxtAbove20Km.Enabled = True
            'Txt1to10Km.MendatroryField = True
            'Txt10to20Km.MendatroryField = True
            'TxtAbove20Km.MendatroryField = True
            gv.Enabled = True
        Else
            'Txt1to10Km.Text = ""
            'Txt10to20Km.Text = ""
            'TxtAbove20Km.Text = ""
            'Txt1to10Km.Enabled = False
            'Txt10to20Km.Enabled = False
            'TxtAbove20Km.Enabled = False
            'Txt1to10Km.MendatroryField = False
            'Txt10to20Km.MendatroryField = False
            'TxtAbove20Km.MendatroryField = False
            gv.Enabled = False
        End If
    End Sub

    Private Sub txtTankerNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTankerNo.KeyPress
        If (Asc(e.KeyChar) >= 48 AndAlso Asc(e.KeyChar) <= 57) OrElse (Asc(e.KeyChar) >= 65 AndAlso Asc(e.KeyChar) <= 90) OrElse (Asc(e.KeyChar) >= 97 AndAlso Asc(e.KeyChar) <= 122) OrElse Asc(e.KeyChar) = Keys.Back OrElse Asc(e.KeyChar) = Keys.Delete Then
        Else

            e.Handled = True
        End If
    End Sub

    Private Sub txtTankerNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTankerNo.TextChanged
        fndcode.Value = txtTankerNo.Text
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub

    Private Sub txtprimarycode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtprimarycode._MYOpenMasterForm
        Frm_Open = New FrmPrimaryTransporterMaster(objCommonVar.CurrentUser, objCommonVar.CurrentCompanyName)
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmPrimaryTransporterMaster)
        Frm_Open.Show()
    End Sub

    Private Sub txtmcccode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtmcccode._MYOpenMasterForm
        Frm_Open = New FrmMCCMaster(objCommonVar.CurrentUser, objCommonVar.CurrentCompanyName)
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmMCCMaster)
        Frm_Open.Show()
    End Sub

    Private Sub chkMonthlyRentPlusDiesel_Click(sender As Object, e As EventArgs) Handles chkMonthlyRentPlusDiesel.Click
        ClickMethod()
    End Sub

    Private Sub chkMonthlyRentPlusDiesel_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMonthlyRentPlusDiesel.ToggleStateChanged
        If chkMonthlyRentPlusDiesel.Checked Then
            txtMRPDRent.Enabled = True
            txtMRPDAverage.Enabled = True
            txtMRPDDieselRate.Enabled = True
            txtMRPDRent.MendatroryField = True
            txtMRPDAverage.MendatroryField = True
            txtMRPDDieselRate.MendatroryField = True
        Else
            txtMRPDRent.Enabled = False
            txtMRPDAverage.Enabled = False
            txtMRPDDieselRate.Enabled = False
            txtMRPDRent.MendatroryField = False
            txtMRPDAverage.MendatroryField = False
            txtMRPDDieselRate.MendatroryField = False
            txtMRPDRent.Text = ""
            txtMRPDAverage.Text = ""
            txtMRPDDieselRate.Text = ""
        End If
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Vehicle Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndcode.Value, "Vehicle_Code", "TSPL_Primary_Vehicle_Master")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
End Class
