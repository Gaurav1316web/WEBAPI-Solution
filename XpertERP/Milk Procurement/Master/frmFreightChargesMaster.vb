Imports common
Imports System.Data.SqlClient
Public Class frmFreightChargesMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim arrLoc As String = Nothing
    Public Const colSlabUpto As String = "colSlabUpto"
    Public Const colSlabRate As String = "colSlabRate"
    Dim isNewEntry As Boolean = True
    Dim Frm_Open As FrmMainTranScreen
#End Region

    Private Sub FrmPrimaryTransporterVehicalMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        Reset()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
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

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmPrimaryTransporterVehicalMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Reset()
        fndcode.Value = Nothing
        chkIsAdditional.Checked = False
        chkDiesel.Checked = False
        chkRateKm.Checked = False
        ChkRateLtr.Checked = False
        chkRentalBasis.Checked = False
        ChkKMRange.Checked = False
        txtchrg.Text = ""
        txtavgkm.Text = ""
        txtdiesel.Text = ""
        txt_km.Text = ""
        txt_ltr.Text = ""
        btnsave.Text = "Save"
        btndelete.Enabled = False
        fndcode.MyReadOnly = False
        isNewEntry = True
        txtRentalAmt.Text = ""
        cmbRentalType.Enabled = False
        cmbRentalType.SelectedIndex = 0
        cmbLtrKG.SelectedIndex = 0
        txt_ltr.Text = ""
        txtDesc.Text = ""
        loadBlankgv()
        gv.Rows.AddNew()
        ClickMethod()
    End Sub

    Function isDuplicateVechiele(ByVal VNO As String) As Boolean
        Dim rValue As Boolean = False
        Dim qry As String = " select count(xx.V_No)  from (select Tanker_No  as V_No from TSPL_TANKER_MASTER union all  select Vehicle_Code as V_No  from TSPL_FREIGHT_CHARGES_MASTER )xx  where xx.V_No ='" & VNO & "'"
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
            If Not chkDiesel.Checked AndAlso Not chkRateKm.Checked AndAlso Not ChkRateLtr.Checked AndAlso Not chkRentalBasis.Checked AndAlso Not ChkKMRange.Checked AndAlso Not chkMonthlyRentPlusDiesel.Checked Then
                RadGroupBox1.Focus()
                RadGroupBox1.Focus()
                Errorcontrol.SetError(chkDiesel, "Please Select Any One From Basis of Freight Payments.")
                Throw New Exception("Please Select Any One From Basis of Freight Payments.")
            Else
                Errorcontrol.ResetError(chkDiesel)
            End If

            If chkDiesel.Checked AndAlso ((clsCommon.myLen(txtchrg.Text) <= 0 Or clsCommon.myCdbl(txtchrg.Text) <= 0) Or (clsCommon.myLen(txtavgkm.Text) <= 0 Or clsCommon.myCdbl(txtavgkm.Text) <= 0) Or (clsCommon.myLen(txtdiesel.Text) <= 0 Or clsCommon.myCdbl(txtavgkm.Text) <= 0)) Then
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

            If chkRateKm.Checked AndAlso (clsCommon.myLen(txt_km.Text) <= 0 Or clsCommon.myCdbl(txt_km.Text) <= 0) Then
                txt_km.Focus()
                txt_km.Select()
                Errorcontrol.SetError(txt_km, "Please Fill Rate per K.M")
                Throw New Exception("Please Fill Rate per K.M")
            Else
                Errorcontrol.ResetError(txt_km)
            End If

            If chkRentalBasis.Checked AndAlso clsCommon.myLen(cmbRentalType.Text) <= 0 Then
                Errorcontrol.SetError(cmbRentalType, "Please Fill any one from rental per Day/ Month/ Year")
                Throw New Exception("Please Select Any rental type as Day/Month/Year")
            Else
                Errorcontrol.ResetError(cmbRentalType)
            End If

            If chkRentalBasis.Checked AndAlso clsCommon.myLen(cmbRentalType.Text) > 0 AndAlso clsCommon.myCdbl(txtRentalAmt.Text) <= 0 Then
                Errorcontrol.SetError(txtRentalAmt, "Please Fill Rental amount")
                Throw New Exception("Please Fill Rental amount")
            Else
                Errorcontrol.ResetError(txtRentalAmt)
            End If
            If ChkRateLtr.Checked AndAlso (clsCommon.myLen(txt_ltr.Text) <= 0 Or clsCommon.myCdbl(txt_ltr.Text) <= 0 Or clsCommon.myLen(cmbLtrKG.Text) <= 0) Then
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

            If ChkKMRange.Checked AndAlso isBlankGV() Then
                Throw New Exception("Please Fill Slab Value")
            End If

            If ChkKMRange.Checked AndAlso (Not isBlankGV()) AndAlso (Not isValidateGridValue()) Then
                Throw New Exception("Slab Value Should be in increasing Order")
            End If


            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
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

            Dim obj As New clsFreightChargesMaster()
            obj.Freight_Code = fndcode.Value
            obj.Freight_Description = txtDesc.Text
            obj.Price_Km = txt_km.Value
            obj.Rate_Type = clsCommon.myCstr(cmbLtrKG.Text)
            obj.Price_Ltr_KG = txt_ltr.Value
            obj.Shift_Charges = txtchrg.Value
            obj.Avg_Km_Ltr = txtavgkm.Value
            obj.Diesel_Rate = txtdiesel.Value
            obj.Rental_Type = clsCommon.myCstr(cmbRentalType.Text)
            obj.Rental_Amount = txtRentalAmt.Value
            obj.Status = ""
            If chkDiesel.IsChecked Then
                obj.Status = "Day/Diesel"
            ElseIf chkRateKm.IsChecked Then
                obj.Status = "Rate/K.M"
            ElseIf ChkRateLtr.IsChecked Then
                obj.Status = "Rate/Ltr"
            ElseIf chkRentalBasis.IsChecked Then
                obj.Status = "Rental"
            ElseIf ChkKMRange.IsChecked Then
                obj.Status = "KM_Range"
                obj.Is_Additional = chkIsAdditional.Checked
            ElseIf chkMonthlyRentPlusDiesel.Checked Then
                obj.Status = "Rental/Diesel"
                obj.Rental_Amount = txtMRPDRent.Value
                obj.Avg_Km_Ltr = txtMRPDAverage.Value
                obj.Diesel_Rate = txtMRPDDieselRate.Value
            End If

            If ChkKMRange.Checked Then
                obj.Is_Additional = chkIsAdditional.Checked
                obj.ArrSlab = New List(Of clsFreightChargesSlab)
                For i As Integer = 0 To gv.Rows.Count - 1
                    If clsCommon.myCdbl(gv.Rows(i).Cells(colSlabUpto).Value) > 0 AndAlso clsCommon.myCdbl(gv.Rows(i).Cells(colSlabRate).Value) > 0 Then
                        Dim objSlab As New clsFreightChargesSlab()
                        objSlab.Slab_Upto = clsCommon.myCdbl(gv.Rows(i).Cells(colSlabUpto).Value)
                        objSlab.Slab_Rate = clsCommon.myCdbl(gv.Rows(i).Cells(colSlabRate).Value)
                        obj.ArrSlab.Add(objSlab)
                    End If
                Next
            End If
            If clsFreightChargesMaster.SaveData(obj) Then
                fndcode.Value = obj.Freight_Code
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
            btndelete.Enabled = True
            fndcode.MyReadOnly = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then
            SaveData()
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If clsCommon.myLen(fndcode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Vehicle No. For Deletion", Me.Text)
            Errorcontrol.SetError(fndcode, "Please Select Vehicle No. For Deletion")
            Return
        Else
            Errorcontrol.ResetError(fndcode)
        End If

        Dim qry As String = "select count(*) from TSPL_FREIGHT_CHARGES_MASTER where vehicle_code='" + fndcode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found For Deletion", Me.Text)
            Return
        End If

        If Not clsCommon.MyMessageBoxShow(Me, "Are You Sure,Want To Delete Primary Transporter Vehicle Master of Vehicle No. " + fndcode.Value + "?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            qry = "delete from TSPL_FREIGHT_CHARGES_MASTER where vehicle_code='" + fndcode.Value + "'"
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

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsFreightChargesMaster = clsFreightChargesMaster.GetData(strCode, arrLoc, NavType)
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Freight_Code) > 0 Then
                Reset()
                isNewEntry = False
                fndcode.Value = obj.Freight_Code
                txtDesc.Text = obj.Freight_Description
                txt_km.Text = obj.Price_Km
                cmbLtrKG.Text = obj.Rate_Type
                txt_ltr.Text = obj.Price_Ltr_KG
                txtchrg.Text = obj.Shift_Charges
                txtdiesel.Text = obj.Diesel_Rate
                txtavgkm.Text = obj.Avg_Km_Ltr
                cmbRentalType.Text = clsCommon.myCstr(obj.Rental_Type)
                txtRentalAmt.Text = clsCommon.myCdbl(obj.Rental_Amount)
                fndcode.MyReadOnly = True
                If clsCommon.CompairString(obj.Status, "Day/Diesel") = CompairStringResult.Equal Then
                    chkDiesel.IsChecked = True
                ElseIf clsCommon.CompairString(obj.Status, "Rate/K.M") = CompairStringResult.Equal Then
                    chkRateKm.IsChecked = True
                ElseIf clsCommon.CompairString(obj.Status, "Rate/Ltr") = CompairStringResult.Equal Then
                    ChkRateLtr.IsChecked = True
                ElseIf clsCommon.CompairString(obj.Status, "Rental") = CompairStringResult.Equal Then
                    chkRentalBasis.IsChecked = True
                ElseIf clsCommon.CompairString(obj.Status, "KM_Range") = CompairStringResult.Equal Then
                    ChkKMRange.IsChecked = True
                    If obj.ArrSlab IsNot Nothing AndAlso obj.ArrSlab.Count > 0 Then
                        gv.Rows.Clear()
                        For i As Integer = 0 To obj.ArrSlab.Count - 1
                            gv.Rows.AddNew()
                            gv.Rows(i).Cells(colSlabUpto).Value = obj.ArrSlab.Item(i).Slab_Upto
                            gv.Rows(i).Cells(colSlabRate).Value = obj.ArrSlab.Item(i).Slab_Rate
                        Next
                    End If
                    chkIsAdditional.Checked = obj.Is_Additional
                ElseIf clsCommon.CompairString(obj.Status, "Rental/Diesel") = CompairStringResult.Equal Then
                    chkMonthlyRentPlusDiesel.IsChecked = True
                    txtMRPDRent.Value = obj.Rental_Amount
                    txtMRPDAverage.Value = obj.Avg_Km_Ltr
                    txtMRPDDieselRate.Value = obj.Diesel_Rate
                    txtRentalAmt.Text = ""
                    txtavgkm.Text = ""
                    txtdiesel.Text = ""
                End If
                btndelete.Enabled = True
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
        Try
            Dim qry As String = "select count(*) from TSPL_FREIGHT_CHARGES_MASTER where Freight_Code='" + fndcode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                fndcode.MyReadOnly = True
            Else
                fndcode.MyReadOnly = False
            End If
            Dim whrcls As String = ""
            If fndcode.MyReadOnly Or isButtonClicked Then
                qry = "select TSPL_FREIGHT_CHARGES_MASTER.Freight_Code as Code,Freight_Description  from TSPL_FREIGHT_CHARGES_MASTER "
                fndcode.Value = clsCommon.ShowSelectForm("FCMFND", qry, "Code", whrcls, fndcode.Value, "Code", isButtonClicked)
                LoadData(fndcode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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

    Private Sub rbtndiesel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDiesel.Click
        ClickMethod()
    End Sub

    Private Sub rbtndiesel_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDiesel.ToggleStateChanged
        If chkDiesel.IsChecked = True Then
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

    Private Sub rbtnrental_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRentalBasis.Click
        ClickMethod()
    End Sub

    Private Sub rbtnrental_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRentalBasis.ToggleStateChanged
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
        If chkRentalBasis.Checked = True Then
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

    Private Sub rbtnrateltr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkRateLtr.Click
        ClickMethod()
    End Sub

    Sub ClickMethod()
        chkDiesel.IsChecked = False
        chkRateKm.IsChecked = False
        ChkRateLtr.IsChecked = False
        chkRentalBasis.IsChecked = False
        ChkKMRange.Checked = False
        chkMonthlyRentPlusDiesel.Checked = False
    End Sub

    Private Sub rbtnrateltr_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkRateLtr.ToggleStateChanged
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

        If ChkRateLtr.Checked = True Then
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

    Private Sub rbtnratekm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRateKm.Click
        ClickMethod()
    End Sub

    Private Sub rbtnratekm_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRateKm.ToggleStateChanged
        If chkRateKm.Checked = True Then
            txt_km.Enabled = True
            txt_km.MendatroryField = True
        Else
            txt_km.Enabled = False
            txt_km.MendatroryField = False
            txt_km.Text = ""
        End If
    End Sub

    Private Sub rbtKmrange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkKMRange.Click
        ClickMethod()
    End Sub

    Private Sub rbtKmrange_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkKMRange.ToggleStateChanged
        If ChkKMRange.Checked = True Then
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

    Private Sub txtTankerNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Asc(e.KeyChar) >= 48 AndAlso Asc(e.KeyChar) <= 57) OrElse (Asc(e.KeyChar) >= 65 AndAlso Asc(e.KeyChar) <= 90) OrElse (Asc(e.KeyChar) >= 97 AndAlso Asc(e.KeyChar) <= 122) OrElse Asc(e.KeyChar) = Keys.Back OrElse Asc(e.KeyChar) = Keys.Delete Then
        Else

            e.Handled = True
        End If
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub

    Private Sub txtprimarycode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        Frm_Open = New FrmPrimaryTransporterMaster(objCommonVar.CurrentUser, objCommonVar.CurrentCompanyName)
        Frm_Open.SetUserMgmt(clsUserMgtCode.frmPrimaryTransporterMaster)
        Frm_Open.Show()
    End Sub

    Private Sub txtmcccode__MYOpenMasterForm(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
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

    Private Sub btnimport_Click(sender As Object, e As EventArgs) Handles btnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Freight_Code", "Freight_Description", "Payment Method(Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel,KM_Range)", "Charges per Day", "Average KM per Ltr", "Rate of Diesel", "Rental Type", "Rental Amount", "Rate per KM", "Rate Type", "Price Ltr/KG", "Additional(Y/N)", "Slab Upto 1", "Slab Rate 1", "Slab Upto 2", "Slab Rate 2", "Slab Upto 3", "Slab Rate 3", "Slab Upto 4", "Slab Rate 4", "Slab Upto 5", "Slab Rate 5") Then
            Dim counter As Integer = 1
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim arr As New List(Of clsFreightChargesMaster)
                Dim obj As clsFreightChargesMaster
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsFreightChargesMaster()
                    Dim index As Integer = 0

                    obj.Freight_Code = clsCommon.myCstr(grow.Cells("Freight_Code").Value)
                    If clsCommon.myLen(obj.Freight_Code) > 30 Then
                        Throw New Exception("Length of Freight_Code Should Not Exceed 30 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    obj.Freight_Description = clsCommon.myCstr(grow.Cells("Freight_Description").Value).Replace("'", "`")
                    If clsCommon.myLen(obj.Freight_Description) <= 0 Or clsCommon.myLen(obj.Freight_Description) > 200 Then
                        Throw New Exception("Please Fill Freight Description And Length Should Not Exceed Max.200 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Dim qry As String = ""
                    obj.Shift_Charges = clsCommon.myCdbl(grow.Cells("Charges per Day").Value)
                    obj.Avg_Km_Ltr = clsCommon.myCdbl(grow.Cells("Average KM per Ltr").Value)
                    obj.Diesel_Rate = clsCommon.myCdbl(grow.Cells("Rate of Diesel").Value)
                    obj.Rental_Type = clsCommon.myCstr(grow.Cells("Rental Type").Value)
                    obj.Rental_Amount = clsCommon.myCdbl(grow.Cells("Rental Amount").Value)
                    obj.Price_Km = clsCommon.myCdbl(grow.Cells("Rate per KM").Value)
                    obj.Rate_Type = clsCommon.myCstr(grow.Cells("Rate Type").Value)
                    obj.Price_Ltr_KG = clsCommon.myCdbl(grow.Cells("Price Ltr/KG").Value)
                    obj.Status = clsCommon.myCstr(grow.Cells("Payment Method(Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel,KM_Range)").Value)
                    If clsCommon.CompairString(obj.Status, "Day/Diesel") = CompairStringResult.Equal Then
                        If obj.Shift_Charges <= 0 Then
                            Throw New Exception("Please Fill Charges per Day At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If obj.Avg_Km_Ltr <= 0 Then
                            Throw New Exception("Please Fill Average KM per Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If obj.Diesel_Rate <= 0 Then
                            Throw New Exception("Please Fill Rate of Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(obj.Status, "Rental") = CompairStringResult.Equal Then
                        If obj.Rental_Amount <= 0 Then
                            Throw New Exception("Please Fill Rental Amount At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If Not (clsCommon.CompairString(obj.Rental_Type, "Day") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Rental_Type, "Month") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Rental_Type, "Year") = CompairStringResult.Equal) Then
                            Throw New Exception("Rental Type should be Day,Month,Year  At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(obj.Status, "Rate/Ltr") = CompairStringResult.Equal Then
                        If obj.Price_Ltr_KG <= 0 Then
                            Throw New Exception("Please Fill Price Ltr/KG At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If Not (clsCommon.CompairString(obj.Rate_Type, "LTR") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Rate_Type, "KG") = CompairStringResult.Equal) Then
                            Throw New Exception("Rate Type should be LTR,KG  At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(obj.Status, "Rate/K.M") = CompairStringResult.Equal Then
                        If obj.Price_Km <= 0 Then
                            Throw New Exception("Please Fill Rate per KM At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(obj.Status, "Rental/Diesel") = CompairStringResult.Equal Then
                        If obj.Rental_Amount <= 0 Then
                            Throw New Exception("Please Fill Rental Amount At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If obj.Avg_Km_Ltr <= 0 Then
                            Throw New Exception("Please Fill Average KM per Ltr At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If obj.Diesel_Rate <= 0 Then
                            Throw New Exception("Please Fill Rate of Diesel At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    ElseIf clsCommon.CompairString(obj.Status, "KM_Range") = CompairStringResult.Equal Then
                        obj.Is_Additional = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Additional(Y/N)").Value), "Y") = CompairStringResult.Equal, True, False)

                        obj.ArrSlab = New List(Of clsFreightChargesSlab)
                        Dim objtr As New clsFreightChargesSlab
                        objtr.Slab_Upto = clsCommon.myCdbl(grow.Cells("Slab Upto 1").Value)
                        objtr.Slab_Rate = clsCommon.myCdbl(grow.Cells("Slab Rate 1").Value)
                        obj.ArrSlab.Add(objtr)

                        objtr = New clsFreightChargesSlab
                        objtr.Slab_Upto = clsCommon.myCdbl(grow.Cells("Slab Upto 2").Value)
                        objtr.Slab_Rate = clsCommon.myCdbl(grow.Cells("Slab Rate 2").Value)
                        obj.ArrSlab.Add(objtr)

                        objtr = New clsFreightChargesSlab
                        objtr.Slab_Upto = clsCommon.myCdbl(grow.Cells("Slab Upto 3").Value)
                        objtr.Slab_Rate = clsCommon.myCdbl(grow.Cells("Slab Rate 3").Value)
                        obj.ArrSlab.Add(objtr)

                        objtr = New clsFreightChargesSlab
                        objtr.Slab_Upto = clsCommon.myCdbl(grow.Cells("Slab Upto 4").Value)
                        objtr.Slab_Rate = clsCommon.myCdbl(grow.Cells("Slab Rate 4").Value)
                        obj.ArrSlab.Add(objtr)

                        objtr = New clsFreightChargesSlab
                        objtr.Slab_Upto = clsCommon.myCdbl(grow.Cells("Slab Upto 5").Value)
                        objtr.Slab_Rate = clsCommon.myCdbl(grow.Cells("Slab Rate 5").Value)
                        obj.ArrSlab.Add(objtr)

                    ElseIf clsCommon.myLen(obj.Status) > 0 Then
                        Throw New Exception("Payment method should be Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel,KM_Range At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    clsFreightChargesMaster.SaveData(obj, trans)
                    counter += 1
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnexport_Click(sender As Object, e As EventArgs) Handles btnexport.Click
        Try
            Dim qry As String = "select count(*) from TSPL_FREIGHT_CHARGES_MASTER"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                qry = "select Freight_Code,Freight_Description,STATUS as [Payment Method(Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel,KM_Range)],Shift_Charges as [Charges per Day],Avg_Km_Ltr as [Average KM per Ltr],Diesel_Rate as [Rate of Diesel],Rental_Type as [Rental Type],Rental_Amount as [Rental Amount],Price_KM as [Rate per KM],Rate_Type as [Rate Type],Price_Ltr_KG as [Price Ltr/KG],case when Is_Additional=1 then 'Y' else 'N' end as [Additional(Y/N)]" + _
                ",(select Slab_Upto from TSPL_FREIGHT_CHARGES_SLAB where SNo=1 and TSPL_FREIGHT_CHARGES_SLAB.Freight_Code=TSPL_FREIGHT_CHARGES_MASTER.Freight_Code) as [Slab Upto 1]" + _
                ",(select Slab_Rate from TSPL_FREIGHT_CHARGES_SLAB where SNo=1  and TSPL_FREIGHT_CHARGES_SLAB.Freight_Code=TSPL_FREIGHT_CHARGES_MASTER.Freight_Code) as [Slab Rate 1] " + _
                " ,(select Slab_Upto from TSPL_FREIGHT_CHARGES_SLAB where SNo=2 and TSPL_FREIGHT_CHARGES_SLAB.Freight_Code=TSPL_FREIGHT_CHARGES_MASTER.Freight_Code) as [Slab Upto 2] " + _
                ",(select Slab_Rate from TSPL_FREIGHT_CHARGES_SLAB where SNo=2 and TSPL_FREIGHT_CHARGES_SLAB.Freight_Code=TSPL_FREIGHT_CHARGES_MASTER.Freight_Code) as [Slab Rate 2]" + _
                ",(select Slab_Upto from TSPL_FREIGHT_CHARGES_SLAB where SNo=3 and TSPL_FREIGHT_CHARGES_SLAB.Freight_Code=TSPL_FREIGHT_CHARGES_MASTER.Freight_Code) as [Slab Upto 3] " + _
                " ,(select Slab_Rate from TSPL_FREIGHT_CHARGES_SLAB where SNo=3 and TSPL_FREIGHT_CHARGES_SLAB.Freight_Code=TSPL_FREIGHT_CHARGES_MASTER.Freight_Code) as [Slab Rate 3]" + _
                " ,(select Slab_Upto from TSPL_FREIGHT_CHARGES_SLAB where SNo=4 and TSPL_FREIGHT_CHARGES_SLAB.Freight_Code=TSPL_FREIGHT_CHARGES_MASTER.Freight_Code) as [Slab Upto 4] " + _
                " ,(select Slab_Rate from TSPL_FREIGHT_CHARGES_SLAB where SNo=4 and TSPL_FREIGHT_CHARGES_SLAB.Freight_Code=TSPL_FREIGHT_CHARGES_MASTER.Freight_Code) as [Slab Rate 4] " + _
                ",(select Slab_Upto from TSPL_FREIGHT_CHARGES_SLAB where SNo=5 and TSPL_FREIGHT_CHARGES_SLAB.Freight_Code=TSPL_FREIGHT_CHARGES_MASTER.Freight_Code) as [Slab Upto 5] " + _
                ",(select Slab_Rate from TSPL_FREIGHT_CHARGES_SLAB where SNo=5 and TSPL_FREIGHT_CHARGES_SLAB.Freight_Code=TSPL_FREIGHT_CHARGES_MASTER.Freight_Code) as [Slab Rate 5] from TSPL_FREIGHT_CHARGES_MASTER  "
            Else
                qry = "select '' as Freight_Code,'' as Freight_Description'' as [Payment Method(Day/Diesel,Rate/K.M,Rate/Ltr,Rental,Rental/Diesel,KM_Range)],0 as [Charges per Day],0 as [Average KM per Ltr],0 as [Rate of Diesel],'' as [Rental Type],0 as [Rental Amount],0 as [Rate per KM],'' as [Rate Type],0 as [Price Ltr/KG],0 as [Additional(Y/N)],0 as [Slab Upto 1],0 as [Slab Rate 1],0 as [Slab Upto 2],0 as [Slab Rate 2],0 as [Slab Upto 3],0 as [Slab Rate 3],0 as [Slab Upto 4],0 as [Slab Rate 4],0 as [Slab Upto 5],0 as [Slab Rate 5] "
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
