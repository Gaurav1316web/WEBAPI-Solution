Imports common
Public Class frmDCSAdditionDeduction
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
#End Region

    Private Sub frmJWPriceCodeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadApplyType()
        LoadApplyOn()
        LoadRoundOFFDecimalPlaces()
        LoadRoundOFFIncreaseAfter()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P  for Post ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New")
        AddNew()
    End Sub

    Sub LoadRoundOFFDecimalPlaces()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "-1"
        dr("Name") = "NA"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "0 Decimal Places"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "1 Decimal Places"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "2"
        dr("Name") = "2 Decimal Places"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "3"
        dr("Name") = "3 Decimal Places"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "4"
        dr("Name") = "4 Decimal Places"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "5"
        dr("Name") = "5 Decimal Places"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "6"
        dr("Name") = "6 Decimal Places"
        dt.Rows.Add(dr)

        cboRoundOFFDecimalPlaces.DataSource = dt.Copy()
        cboRoundOFFDecimalPlaces.ValueMember = "Code"
        cboRoundOFFDecimalPlaces.DisplayMember = "Name"
    End Sub

    Sub LoadRoundOFFIncreaseAfter()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "-1"
        dr("Name") = "NA"
        dt.Rows.Add(dr)
        For ii As Integer = 0 To 9
            dr = dt.NewRow()
            dr("Code") = clsCommon.myCstr(ii)
            dr("Name") = clsCommon.myCstr(ii)
            dt.Rows.Add(dr)
        Next

        cboROIncreaseAfter.DataSource = dt.Copy()
        cboROIncreaseAfter.ValueMember = "Code"
        cboROIncreaseAfter.DisplayMember = "Name"
    End Sub
    Sub LoadApplyOn()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select..."
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "Quantity"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Amount"
        dt.Rows.Add(dr)

        cboApplyOn.DataSource = dt.Copy()
        cboApplyOn.ValueMember = "Code"
        cboApplyOn.DisplayMember = "Name"
    End Sub
    Sub LoadApplyType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select..."
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "Rate"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Percentage"
        dt.Rows.Add(dr)

        cboApplyType.DataSource = dt.Copy()
        cboApplyType.ValueMember = "Code"
        cboApplyType.DisplayMember = "Name"
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub
    Public Sub Save()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsDCSAdditionDeduction()
                obj.Code = txtCode.Value
                obj.Description = txtDescName.Text
                obj.Description_Hindi = txtDescNameHindi.Text
                obj.Start_Date = dtStartDate.Value
                If dtpEndDate.Checked Then
                    obj.End_Date = dtpEndDate.Value
                End If

                obj.SNo = txtSNo.Value
                If rbtnDCSTypeBoth.IsChecked Then
                    obj.Applicable_DCS_Type = 0
                ElseIf rbtnDCSTypeDCS.IsChecked Then
                    obj.Applicable_DCS_Type = 1
                ElseIf rbtnDCSTypePDCS.IsChecked Then
                    obj.Applicable_DCS_Type = 2
                ElseIf rbtnDCSTypeBMC.IsChecked Then
                    obj.Applicable_DCS_Type = 3
                ElseIf rbtnDCSTypeCluster.IsChecked Then
                    obj.Applicable_DCS_Type = 4
                ElseIf rbtnDCSTypeBMCTruckSheet.IsChecked Then
                    obj.Applicable_DCS_Type = 5
                ElseIf rbtnDCSTypeDCSTruckSheet.IsChecked Then
                    obj.Applicable_DCS_Type = 6
                ElseIf rbtnDCSTypeDCSTruckSheetMultipleDays.IsChecked Then
                    obj.Applicable_DCS_Type = 7
                End If
                obj.Conversion = txtConvertsion.Value
                obj.Saving = 0
                If rbtnNatureTypeAddition.IsChecked Then
                    obj.Nature_Type = 0
                    If rbtnAdditionTypeSaving.IsChecked Then
                        obj.Saving = 1
                    ElseIf rbtnAdditionTypeCompulsory.IsChecked Then
                        obj.Saving = 2
                    End If
                    obj.Apply_TDS = chkApplyTDS.Checked
                ElseIf rbtnNatureTypeDeduction.IsChecked Then
                    obj.Nature_Type = 1
                    obj.Apply_TDS = False
                End If
                obj.Applicable_Type = clsCommon.myCdbl(cboApplyType.SelectedValue)
                obj.Applicable_On = clsCommon.myCdbl(cboApplyOn.SelectedValue)
                obj.Include_Shortage_Own_BMC = chkIncludeShortageOwnBMC.Checked
                obj.Subtract = chkSubtract.Checked
                obj.Apply_Formula = chkApplyFormula.Checked
                obj.Dont_Generate_DR_CR_Note = chkDontGenerateDRCRNote.Checked
                If rbtnQtyUOMRec.IsChecked Then
                    obj.Qty_UOM = 0
                ElseIf rbtnQtyUOMLtr.IsChecked Then
                    obj.Qty_UOM = 1
                ElseIf rbtnQtyUOMKG.IsChecked Then
                    obj.Qty_UOM = 2
                End If
                If chkSavingAC.Checked Then
                    If rbtnACExists.IsChecked Then
                        obj.Check_Saving_AC = 1
                    ElseIf rbtnACNotExists.IsChecked Then
                        obj.Check_Saving_AC = 2
                    End If
                Else
                    obj.Check_Saving_AC = 0
                End If
                obj.Applicable_Value = txtApplyValue.Value
                obj.GL_Account = txtGLAccount.Value
                obj.MappingCode = txtMappingCode.Value
                obj.RO_Decimal_Places = clsCommon.myCDecimal(cboRoundOFFDecimalPlaces.SelectedValue)
                obj.RO_Increase_After = clsCommon.myCDecimal(cboROIncreaseAfter.SelectedValue)
                If txtMilkType.arrValueMember IsNot Nothing AndAlso txtMilkType.arrValueMember.Count > 0 Then
                    obj.Milk_Type = clsCommon.GetMulcallString(txtMilkType.arrValueMember)
                Else
                    obj.Milk_Type = ""
                End If
                obj.Arr = txtAddAmount.arrValueMember
                obj.ArrDCSExclude = txtExcludeDCS.arrValueMember
                If obj.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            AddNew()
            Dim obj As clsDCSAdditionDeduction = clsDCSAdditionDeduction.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                btnsave.Enabled = True
                btnPost.Enabled = True
                isNewEntry = False
                txtCode.Value = obj.Code
                txtDescName.Text = obj.Description
                txtDescNameHindi.Text = obj.Description_Hindi
                dtStartDate.Value = obj.Start_Date
                If obj.End_Date IsNot Nothing Then
                    dtpEndDate.Checked = True
                    dtpEndDate.Value = obj.End_Date
                Else
                    dtpEndDate.Checked = False
                End If

                UsLock1.Status = obj.Posted
                chkInactive.Checked = obj.Inactive

                If obj.Saving = 1 Then
                    rbtnAdditionTypeSaving.IsChecked = True
                ElseIf obj.Saving = 2 Then
                    rbtnAdditionTypeCompulsory.IsChecked = True
                Else
                    rbtnAdditionTypeNormal.IsChecked = True
                End If

                txtSNo.Value = obj.SNo
                If obj.Applicable_DCS_Type = 0 Then

                End If
                If obj.Applicable_DCS_Type = 0 Then
                    rbtnDCSTypeBoth.IsChecked = True
                ElseIf obj.Applicable_DCS_Type = 1 Then
                    rbtnDCSTypeDCS.IsChecked = True
                ElseIf obj.Applicable_DCS_Type = 2 Then
                    rbtnDCSTypePDCS.IsChecked = True
                ElseIf obj.Applicable_DCS_Type = 3 Then
                    rbtnDCSTypeBMC.IsChecked = True
                ElseIf obj.Applicable_DCS_Type = 4 Then
                    rbtnDCSTypeCluster.IsChecked = True
                ElseIf obj.Applicable_DCS_Type = 5 Then
                    rbtnDCSTypeBMCTruckSheet.IsChecked = True
                ElseIf obj.Applicable_DCS_Type = 6 Then
                    rbtnDCSTypeDCSTruckSheet.IsChecked = True
                ElseIf obj.Applicable_DCS_Type = 7 Then
                    rbtnDCSTypeDCSTruckSheetMultipleDays.IsChecked = True
                End If
                txtConvertsion.Value = obj.Conversion
                If obj.Nature_Type = 0 Then
                    rbtnNatureTypeAddition.IsChecked = True
                ElseIf obj.Nature_Type = 1 Then
                    rbtnNatureTypeDeduction.IsChecked = True
                End If
                chkApplyTDS.Checked = obj.Apply_TDS
                cboApplyType.SelectedValue = clsCommon.myCstr(obj.Applicable_Type)
                cboApplyOn.SelectedValue = clsCommon.myCstr(obj.Applicable_On)
                If obj.Qty_UOM = 0 Then
                    rbtnQtyUOMRec.IsChecked = True
                ElseIf obj.Qty_UOM = 1 Then
                    rbtnQtyUOMLtr.IsChecked = True
                ElseIf obj.Qty_UOM = 2 Then
                    rbtnQtyUOMKG.IsChecked = True
                End If
                chkIncludeShortageOwnBMC.Checked = obj.Include_Shortage_Own_BMC
                chkSubtract.Checked = obj.Subtract
                chkApplyFormula.Checked = obj.Apply_Formula
                chkDontGenerateDRCRNote.Checked = obj.Dont_Generate_DR_CR_Note
                If obj.Check_Saving_AC > 0 Then
                    chkSavingAC.Checked = True

                    If obj.Check_Saving_AC = 1 Then
                        rbtnACExists.IsChecked = True
                    ElseIf obj.Check_Saving_AC = 2 Then
                        rbtnACNotExists.IsChecked = True
                    End If
                Else
                    chkSavingAC.Checked = False

                End If

                txtApplyValue.Value = obj.Applicable_Value
                txtGLAccount.Value = obj.GL_Account
                lblGLAcctName.Text = clsGLAccount.GetName(obj.GL_Account)
                txtMappingCode.Value = obj.MappingCode
                lblMappingCodeDesc.Text = clsDCSAdditionDeduction.GetName(obj.MappingCode)

                cboRoundOFFDecimalPlaces.SelectedValue = clsCommon.myCstr(obj.RO_Decimal_Places)
                cboROIncreaseAfter.SelectedValue = clsCommon.myCstr(obj.RO_Increase_After)
                txtAddAmount.arrValueMember = obj.Arr
                Dim arr As ArrayList = Nothing
                Try
                    If clsCommon.myLen(obj.Milk_Type) > 0 Then
                        obj.Milk_Type = obj.Milk_Type.Replace("','", "#")
                        obj.Milk_Type = obj.Milk_Type.Replace("'", "")
                        Dim strTempBreak As String() = obj.Milk_Type.Split("#")
                        If strTempBreak.Length > 0 Then
                            arr = New ArrayList
                            For ii As Integer = 0 To strTempBreak.Length - 1
                                arr.Add(strTempBreak(ii))
                            Next
                        End If
                    End If
                Catch ex As Exception
                    Dim s As String = ex.Message
                    Dim y As Integer = 0
                End Try

                txtMilkType.arrValueMember = arr
                txtExcludeDCS.arrValueMember = obj.ArrDCSExclude
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    chkInactive.Enabled = Not obj.Inactive
                    If chkInactive.Enabled Then
                        chkInactive.Enabled = MyBase.isPostFlag
                    End If

                    btnEndDate.Enabled = Not dtpEndDate.Checked
                    If btnEndDate.Enabled Then
                        btnEndDate.Enabled = MyBase.isPostFlag
                    End If
                Else
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    btnsave.Enabled = True
                End If
                btnsave.Text = "Update"
                setNatureTypeAddition()
                setGrpQtyUOM()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtDescName.Text) <= 0 Then
            myMessages.blankValue(Me, txtDescName.MyLinkLable1.Text, Me.Text)
            txtDescName.Focus()
            Return False
        End If
        If clsCommon.myLen(cboApplyType.SelectedValue) <= 0 Then
            myMessages.blankValue(Me, cboApplyType.MyLinkLable1.Text, Me.Text)
            cboApplyType.Focus()
            Return False
        End If
        If clsCommon.myLen(cboApplyOn.SelectedValue) <= 0 Then
            myMessages.blankValue(Me, cboApplyOn.MyLinkLable1.Text, Me.Text)
            cboApplyOn.Focus()
            Return False
        End If
        If txtApplyValue.Value <= 0 Then
            myMessages.blankValue(Me, txtApplyValue.MyLinkLable1.Text, Me.Text)
            txtApplyValue.Focus()
            Return False
        End If
        If clsCommon.myLen(txtGLAccount.Value) <= 0 Then
            myMessages.blankValue(Me, txtGLAccount.MyLinkLable1.Text, Me.Text)
            txtGLAccount.Focus()
            Return False
        End If
        If rbtnDCSTypeBMCTruckSheet.IsChecked OrElse rbtnDCSTypeDCSTruckSheet.IsChecked OrElse rbtnDCSTypeDCSTruckSheetMultipleDays.IsChecked Then
            If clsCommon.CompairString(clsCommon.myCstr(cboApplyOn.SelectedValue), "0") = CompairStringResult.Equal Then
                If rbtnQtyUOMKG.IsChecked Then
                    clsCommon.MyMessageBoxShow(Me, "Application for DCS Type [BMC Truck Sheet] Apply on [Qty] not for UOM [KG]", Me.Text)
                    Return False
                End If
                If rbtnQtyUOMLtr.IsChecked Then
                    clsCommon.MyMessageBoxShow(Me, "Application for DCS Type [BMC Truck Sheet] Apply on [Qty] not for UOM [LTR]", Me.Text)
                    Return False
                End If
            End If
        End If
        Return True
    End Function
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsDCSAdditionDeduction.DeleteData(txtCode.Value)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_DCS_ADDITION_DEDUCTION where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsDCSAdditionDeduction.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                AddNew()
            End If
        End If
    End Sub
    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub AddNew()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Focus()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        chkInactive.Enabled = False
        chkInactive.Checked = False
        btnEndDate.Enabled = False
        cboApplyOn.SelectedValue = ""
        cboApplyType.SelectedValue = ""
        txtSNo.Text = ""
        txtCode.Value = Nothing
        txtDescName.Text = Nothing
        txtDescNameHindi.Text = Nothing
        txtGLAccount.Value = ""
        lblGLAcctName.Text = ""
        txtApplyValue.Text = ""
        dtpEndDate.Value = clsCommon.GETSERVERDATE()
        dtStartDate.Value = dtpEndDate.Value
        UsLock1.Status = ERPTransactionStatus.Pending
        rbtnDCSTypeBoth.IsChecked = True
        rbtnNatureTypeAddition.IsChecked = True
        txtMappingCode.Value = ""
        lblMappingCodeDesc.Text = ""
        setNatureTypeAddition()
        cboRoundOFFDecimalPlaces.SelectedValue = "-1"
        cboROIncreaseAfter.SelectedValue = "-1"
        txtAddAmount.arrValueMember = Nothing
        rbtnQtyUOMRec.IsChecked = True
        txtMilkType.arrValueMember = Nothing
        txtExcludeDCS.arrValueMember = Nothing
        chkApplyTDS.Checked = False
        chkIncludeShortageOwnBMC.Checked = False
        chkSubtract.Checked = False
        chkApplyFormula.Checked = False
        chkDontGenerateDRCRNote.Checked = False
        chkSavingAC.Checked = False
        rbtnACExists.Visible = False
        rbtnACNotExists.Visible = False
        txtConvertsion.Value = 1
    End Sub
    Private Sub frmHSNMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnreset.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub
    Private Sub rdbtnreset_Click(sender As Object, e As EventArgs) Handles rdbtnreset.Click
        AddNew()
    End Sub
    Private Sub chkInactive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInactive.ToggleStateChanged
        Try
            If Not isInsideLoadData Then
                If chkInactive.Checked Then
                    If clsCommon.myLen(txtCode.Value) > 0 Then
                        If clsCommon.MyMessageBoxShow(Me, "Current code [" + txtCode.Value + "] will be inactive" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            If (clsDCSAdditionDeduction.InactiveData(txtCode.Value)) Then
                                clsCommon.MyMessageBoxShow(Me, "Successfully Inactivated", Me.Text)
                            End If
                        End If
                    End If
                End If
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub PostData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Please select document no to post")
            End If
            If (myMessages.postConfirm()) Then
                If (clsDCSAdditionDeduction.PostData(txtCode.Value)) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Private Sub btnEndDate_Click(sender As Object, e As EventArgs) Handles btnEndDate.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Document No not found to update")
            End If

            Dim obj As New clsDCSAdditionDeduction()
            obj.Code = txtCode.Value
            obj.End_Date = dtpEndDate.Value
            obj.SaveEndDateData(obj)
            clsCommon.MyMessageBoxShow(Me, "Successfully Updated", Me.Text)
            LoadData(txtCode.Value, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            txtCode.Value = clsDCSAdditionDeduction.getFinder("", txtCode.Value, True)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
                UsLock1.Status = ERPTransactionStatus.Pending
                isNewEntry = True
                txtCode.Value = ""
                btnDelete.Enabled = False
                btnPost.Enabled = False
                btnsave.Enabled = True
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtGLAccount__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtGLAccount._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        txtGLAccount.Value = clsCommon.ShowSelectForm("FndGLDCSAD", Qry, "Account_Code", " ControlAccount ='Y' ", txtGLAccount.Value, "Account_Code", isButtonClicked)
        lblGLAcctName.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + txtGLAccount.Value + "' ")
    End Sub

    Private Sub txtMappingCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMappingCode._MYValidating
        Try
            txtMappingCode.Value = clsDCSAdditionDeduction.getFinder("", txtMappingCode.Value, isButtonClicked)
            If txtMappingCode.Value <> "" Then
                lblMappingCodeDesc.Text = clsDCSAdditionDeduction.GetName(txtMappingCode.Value)
            Else
                txtMappingCode.Value = ""
                lblMappingCodeDesc.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnNatureTypeAddition_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnNatureTypeAddition.ToggleStateChanged, rbtnNatureTypeDeduction.ToggleStateChanged
        setNatureTypeAddition()
    End Sub

    Sub setNatureTypeAddition()
        grpAdditionType.Visible = rbtnNatureTypeAddition.IsChecked
        chkApplyTDS.Visible = rbtnNatureTypeAddition.IsChecked
    End Sub

    Private Sub txtAddAmount__My_Click(sender As Object, e As EventArgs) Handles txtAddAmount._My_Click
        Dim qry As String = "select Code,Description from TSPL_DCS_ADDITION_DEDUCTION where code Not in ('" + txtCode.Value + "') and not exists(select 1 from TSPL_DCS_ADDITION_DEDUCTION_ADD_AMT where TSPL_DCS_ADDITION_DEDUCTION_ADD_AMT.Code=TSPL_DCS_ADDITION_DEDUCTION.Code)"
        txtAddAmount.arrValueMember = clsCommon.ShowMultipleSelectForm("MPISMCC", qry, "Code", "Description", txtAddAmount.arrValueMember, Nothing)
    End Sub

    Private Sub cboApplyOn_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cboApplyOn.Validating
        setGrpQtyUOM()
    End Sub

    Sub setGrpQtyUOM()
        grpQtyUOM.Visible = (clsCommon.CompairString(clsCommon.myCstr(cboApplyOn.SelectedValue), "0") = CompairStringResult.Equal)
    End Sub

    Private Sub txtMilkType__My_Click(sender As Object, e As EventArgs) Handles txtMilkType._My_Click
        txtMilkType.arrValueMember = clsCommon.ShowMultipleSelectForm("DCSADReT", clsMilkReceiptMCC.GetRejectQry(True), "Code", "Name", txtMilkType.arrValueMember, Nothing)
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub

    Private Sub chkSavingAC_CheckStateChanged(sender As Object, e As EventArgs) Handles chkSavingAC.CheckStateChanged
        If chkSavingAC.Checked Then
            rbtnACExists.Visible = True
            rbtnACNotExists.Visible = True
        Else
            rbtnACExists.Visible = False
            rbtnACNotExists.Visible = False

        End If

    End Sub

    Private Sub txtExcludeDCS__My_Click(sender As Object, e As EventArgs) Handles txtExcludeDCS._My_Click
        Try
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as DCSCode,VLC_Name as DCSName,VLC_Code_VLC_Uploader as UploaderCode,VSP_Code as VSPCode,TSPL_VENDOR_MASTER.Vendor_Name as VSPName
from TSPL_VLC_MASTER_HEAD
inner join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code"
            txtExcludeDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("DCS@addded", qry, "DCSCode", "DCSName", txtExcludeDCS.arrValueMember, Nothing)
            If UsLock1.Status = ERPTransactionStatus.Approved Then
                Dim obj As New clsDCSAdditionDeduction
                obj.SaveDCSExcludeData(txtCode.Value, txtExcludeDCS.arrValueMember)
                clsCommon.MyMessageBoxShow(Me, "DCS Exclude successfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class