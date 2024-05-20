'==============created by Monika
'================BM00000003353===================BM00000003535======
'========BM00000006845============='================added by preeti gupta against ticket no [BM00000009854]
Imports System.Data.SqlClient
Imports common

Public Class FrmParameterMaster
    Inherits FrmMainTranScreen


#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim isNewEntry As Boolean = True

    Public Const colSLNO As String = "colSLNO"
    Public Const colParamCode As String = "colParamCode"
    Public Const colParamDesc As String = "colParamDesc"
    Public Const colNature As String = "colNature"
    Public Const colMandatory As String = "colMandatory"
    Public Const colDispatchPrint As String = "colDispatchPrint"
    Public Const colQCPrint As String = "colQCPrint"
    Public Const colMMCollection As String = "colMMCollection"
    Public Const colApplicableFor As String = "colApplicableFor"
    Public isLoadData As Boolean = False

#End Region


    Sub Reset()
        fndNo.Value = ""
        txtdesc.Text = ""
        cbonature.Text = "None"
        cmbtype.SelectedValue = ""
        ddlParametereType.SelectedValue = ""
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        ''richa Against Ticket No. BM00000003713 on 03/09/2014
        chkIsmandatory.Checked = False
        chkIsCanType.Checked = False
        chkBulkSale.Checked = False
        ChkMccQC.Checked = False
        ChkProduction.Checked = False
        ChkMandatoryProdction.Checked = False
        ChkMandatoryProdction.Enabled = False
        ''----------------------------------------------------
        cmbParamFor.Text = ""
        btnsave.Text = "Save"
        btndelete.Enabled = False
        fndNo.MyReadOnly = False
        isNewEntry = True
        loadParameters()
    End Sub

    Sub loadBlankParameterGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()
        Dim repoComboColumn As GridViewComboBoxColumn
        Dim repoTextColumn As GridViewTextBoxColumn

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colSLNO
        repoTextColumn.Width = 60
        repoTextColumn.HeaderText = "SL. NO"
        repoTextColumn.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextColumn)

        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colParamCode
        repoTextColumn.Width = 120
        repoTextColumn.HeaderText = "Parameter Code"
        repoTextColumn.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextColumn)



        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colParamDesc
        repoTextColumn.Width = 180
        repoTextColumn.HeaderText = "Parameter Desc"
        repoTextColumn.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextColumn)



        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colNature
        repoTextColumn.Width = 120
        repoTextColumn.HeaderText = "Nature"
        repoTextColumn.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextColumn)


        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colMandatory
        repoTextColumn.Width = 80
        repoTextColumn.HeaderText = "Mandatory"
        repoTextColumn.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextColumn)


        repoTextColumn = New GridViewTextBoxColumn()
        repoTextColumn.Name = colApplicableFor
        repoTextColumn.Width = 80
        repoTextColumn.HeaderText = "Applicable For"
        repoTextColumn.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTextColumn)

        repoComboColumn = New GridViewComboBoxColumn()
        repoComboColumn.Name = colDispatchPrint
        repoComboColumn.Width = 180
        repoComboColumn.HeaderText = "Show on Dispatch Print"
        repoComboColumn.DataSource = FillYesNoValue()
        repoComboColumn.DisplayMember = "Value"
        repoComboColumn.ValueMember = "Value"
        repoComboColumn.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoComboColumn)

        repoComboColumn = New GridViewComboBoxColumn()
        repoComboColumn.Name = colQCPrint
        repoComboColumn.Width = 180
        repoComboColumn.HeaderText = "Show on QC Print"
        repoComboColumn.DataSource = FillYesNoValue()
        repoComboColumn.DisplayMember = "Value"
        repoComboColumn.ValueMember = "Value"
        repoComboColumn.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoComboColumn)

        repoComboColumn = New GridViewComboBoxColumn()
        repoComboColumn.Name = colMMCollection
        repoComboColumn.Width = 180
        repoComboColumn.HeaderText = "Show in Mcc Milk Collection"
        repoComboColumn.DataSource = FillYesNoValue()
        repoComboColumn.DisplayMember = "Value"
        repoComboColumn.ValueMember = "Value"
        repoComboColumn.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoComboColumn)

        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.AllowRowReorder = False
        gv.ShowGroupPanel = False
        gv.EnableFiltering = True
        gv.EnableSorting = True
        gv.EnableGrouping = False
        gv.AllowColumnChooser = True
        gv.AllowColumnReorder = True

    End Sub
    Function FillYesNoValue() As DataTable
        Dim dt As DataTable

        Dim qry As String = " select '' as value union all select 'Yes' as value union all select 'No' as value "

        dt = clsDBFuncationality.GetDataTable(qry)


        Return dt
    End Function
    Sub loadParameters()
        'Code,Description,Type,Nature,IsMandatory,Param_For,IsForPrintOnDispatch,IsForPrintOnQC
        isLoadData = True
        loadBlankParameterGrid()
        Dim dt As DataTable = Nothing
        Dim qry As String = " select * from TSPL_PARAMETER_MASTER  "
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                gv.Rows.AddNew()
                gv.Rows(i).Cells(colSLNO).Value = (i + 1)
                gv.Rows(i).Cells(colParamCode).Value = dt.Rows(i)("Code")
                gv.Rows(i).Cells(colParamDesc).Value = dt.Rows(i)("Description")
                gv.Rows(i).Cells(colNature).Value = dt.Rows(i)("Nature")
                gv.Rows(i).Cells(colMandatory).Value = dt.Rows(i)("IsMandatory")
                gv.Rows(i).Cells(colApplicableFor).Value = dt.Rows(i)("Param_For")
                gv.Rows(i).Cells(colDispatchPrint).Value = IIf(dt.Rows(i)("IsForPrintOnDispatch") = "1", "Yes", "No")
                gv.Rows(i).Cells(colQCPrint).Value = IIf(dt.Rows(i)("IsForPrintOnQC") = "1", "Yes", "No")
                gv.Rows(i).Cells(colMMCollection).Value = IIf(dt.Rows(i)("IsShowInMMC") = "1", "Yes", "No")
            Next
        End If
        isLoadData = False
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmParameterMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmParameterMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            btnnew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmParameterMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        LoadCombobox()
        LoadparameterType()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        RadPageView1.SelectedPage = RadPageViewPage1
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "IPIL") = CompairStringResult.Equal Then
            ChkMccQC.Visible = True
        Else
            ChkMccQC.Visible = False
        End If
    End Sub

    Sub LoadCombobox()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "FAT"
        dr("Name") = "FAT"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SNF"
        dr("Name") = "SNF"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CLR"
        dr("Name") = "CLR"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CF"
        dr("Name") = "CORRECTION FACTOR"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CHANNA"
        dr("Name") = "CHANNA"
        dt.Rows.Add(dr)

        '=============Added by preeti Gupta=============
        dr = dt.NewRow()
        dr("Code") = "TIME"
        dr("Name") = "TIME"
        dt.Rows.Add(dr)
        '===============================================


        dr = dt.NewRow()
        dr("Code") = "OTHERS"
        dr("Name") = "OTHERS"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "BWRM"
        dr("Name") = "BWRM"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "AWRM"
        dr("Name") = "AWRM"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "DIFFRM"
        dr("Name") = "DIFFRM"
        dt.Rows.Add(dr)

        ''richa 22 Sep,2016 BM00000009810
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") = CompairStringResult.Equal Then
            dr = dt.NewRow()
            dr("Code") = "ABB"
            dr("Name") = "Acid Before Boiling"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "AAB"
            dr("Name") = "Acid After Boiling"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "ADIFF"
            dr("Name") = "Acid Difference"
            dt.Rows.Add(dr)
        End If
        ''--------------------------------

        cmbtype.DataSource = Nothing
        cmbtype.DataSource = dt
        cmbtype.DisplayMember = "Name"
        cmbtype.ValueMember = "Code"
    End Sub
    '==============added by preeti gupta==============
    Sub LoadparameterType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()

        dr("Code") = "None"
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Purchase"
        dr("Name") = "Purchase"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Procurement"
        dr("Name") = "Procurement"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Job Work"
        dr("Name") = "Job Work"
        dt.Rows.Add(dr)


        ddlParametereType.DataSource = Nothing
        ddlParametereType.DataSource = dt
        ddlParametereType.DisplayMember = "Name"
        ddlParametereType.ValueMember = "Code"
    End Sub

    '===================================================
    Function AllowToSave() As Boolean
        Try
            'If clsCommon.myLen(fndNo.Value) <= 0 Then
            '    fndNo.Focus()
            '    fndNo.Select()
            '    ErrorControl.SetError(fndNo, "Fill The Parameter Code.")
            '    Throw New Exception("Please Fill Code")
            '    Return False
            'Else
            '    ErrorControl.ResetError(fndNo)
            'End If

            If clsCommon.myLen(txtdesc.Text) <= 0 Then
                txtdesc.Focus()
                txtdesc.Select()
                ErrorControl.SetError(txtdesc, "Fill The Description Of Parameter")
                Throw New Exception("Please Fill Description")
                Return False
            Else
                ErrorControl.ResetError(txtdesc)
            End If

            If cmbtype.SelectedValue = "" Then
                cmbtype.Select()
                ErrorControl.SetError(cmbtype, "Please Select Type For Parameter")
                Throw New Exception("Please Select Type Values")
                Return False
            Else
                ErrorControl.ResetError(cmbtype)
            End If

            If clsCommon.myLen(cbonature.Text) <= 0 Or clsCommon.CompairString(cbonature.Text, "None") = CompairStringResult.Equal Then
                cbonature.Select()
                ErrorControl.SetError(cbonature, "Please select nature for parameter")
                Throw New Exception("Please select nature for parameter")
            Else
                ErrorControl.ResetError(cbonature)
            End If

            ''richa 22 Sep,2016 BM00000009810
            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") = CompairStringResult.Equal Then
                If clsCommon.CompairString(cmbtype.SelectedValue, "AAB") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbtype.SelectedValue, "ABB") = CompairStringResult.Equal Then
                    If clsCommon.myLen(cbonature.Text) <= 0 Or clsCommon.CompairString(cbonature.Text, "Range") <> CompairStringResult.Equal Then
                        cbonature.Select()
                        ErrorControl.SetError(cbonature, "Please select Range Type of parameter")
                        Throw New Exception("Please select Range Type of parameter")
                    Else
                        ErrorControl.ResetError(cbonature)
                    End If
                End If
            End If
            ''--------------------------------

            If clsCommon.CompairString(cmbtype.SelectedValue, "OTHERS") <> CompairStringResult.Equal Then
                Dim qry As String = "select count(*) from TSPL_PARAMETER_MASTER where comp_code='" + objCommonVar.CurrentCompanyCode + "' and type='" + clsCommon.myCstr(cmbtype.SelectedValue) + "' and code <> '" + clsCommon.myCstr(fndNo.Value) + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                'Dim value As Integer = 0
                'If clsCommon.CompairString(btnsave.Text, "&Save") = CompairStringResult.Equal Then
                '    value = 0
                'Else
                '    value = 1
                'End If
                If check > 0 Then
                    ErrorControl.SetError(cmbtype, "This Type (" + clsCommon.myCstr(cmbtype.SelectedValue) + ") Is Already Exist,Please Change The Type")
                    Throw New Exception("This Type (" + clsCommon.myCstr(cmbtype.SelectedValue) + ") Is Already Exist,Please Change The Type")
                    Return False
                Else
                    ErrorControl.ResetError(cmbtype)
                End If
            End If

            If clsCommon.myLen(cmbParamFor.Text) <= 0 Then
                cmbParamFor.Select()
                ErrorControl.SetError(cmbParamFor, "Please select   Applicable For field Either MCC/PLANT/BOTH")
                Throw New Exception("Please select Applicable For field Either MCC/PLANT/BOTH")
            Else
                ErrorControl.ResetError(cmbParamFor)
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub SaveData()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmParameterMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim obj As New clsfrmParameterMaster()

            obj.code = clsCommon.myCstr(fndNo.Value)
            obj.desc = clsCommon.myCstr(txtdesc.Text.Replace("'", "`"))
            obj.type = clsCommon.myCstr(cmbtype.SelectedValue)
            obj.Parametertype = clsCommon.myCstr(ddlParametereType.SelectedValue)
            obj.nature = clsCommon.myCstr(cbonature.Text)
            obj.Param_For = clsCommon.myCstr(cmbParamFor.Text)

            ''richa Against Ticket No. BM00000003713 on 03/09/2014
            If chkIsmandatory.Checked = True Then
                obj.IsMandatory = 1
            Else
                obj.IsMandatory = 0
            End If
            If chkBulkSale.Checked = True Then
                obj.IsBulkSale = 1
            Else
                obj.IsBulkSale = 0
            End If
            If ChkMccQC.Checked = True Then
                obj.IsMCC_Qc = 1
            Else
                obj.IsMCC_Qc = 0
            End If
            If chkIsCanType.Checked = True Then
                obj.IsCanType = 1
            Else
                obj.IsCanType = 0
            End If
            '' Work done by Parteek on 09/08/2018 ticket no.BHA/09/08/18-000404
            If ChkProduction.Checked = True Then
                obj.IsProduction = 1
            Else
                obj.IsProduction = 0
            End If
            If ChkMandatoryProdction.Checked = True Then
                obj.IsProductionMandatory = 1
            Else
                obj.IsProductionMandatory = 0
            End If
            ''----------------------------------------------------

            If obj.nature = "Range" Then
                obj.nature = "R"
            ElseIf obj.nature = "Alphanumeric" Then
                obj.nature = "A"
            ElseIf obj.nature = "Boolean" Then
                obj.nature = "B"
            End If
            obj.Is_Milk_Sample = chkMilkSample.Checked
            obj.IsForMilkGrade = IIf(chkMilkGrade.Checked = True, 1, 0)
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsfrmParameterMaster.SaveData(obj.code, isNewEntry, trans, obj) Then
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If
                btnsave.Text = "Update"
                btndelete.Enabled = True
                fndNo.MyReadOnly = True
                fndNo.Value = obj.code
                UcAttachment1.SaveData(obj.code)

                LoadData(fndNo.Value, NavigatorType.Current)
            Else
                btnsave.Text = "Save"
                fndNo.MyReadOnly = False
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
        Dim qry As String = ""

        Try
            If clsCommon.myLen(fndNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Code For Deletion", Me.Text)
                fndNo.Focus()
                fndNo.Select()
                Return
            Else
                qry = "select count(*) from TSPL_PARAMETER_MASTER where comp_code='" + objCommonVar.CurrentCompanyCode + "' and code='" + clsCommon.myCstr(fndNo.Value) + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

                If check <= 0 Then
                    fndNo.Focus()
                    fndNo.Select()
                    Throw New Exception("No Data Found For Deletion")
                End If
            End If

            If Not clsCommon.MyMessageBoxShow("Are You Sure,Want To Delete The Parameter Master Of Code " + clsCommon.myCstr(fndNo.Value) + "?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                trans.Rollback()
                Return
            End If

            qry = "delete from TSPL_PARAMETER_MASTER where comp_code='" + objCommonVar.CurrentCompanyCode + "' and code='" + clsCommon.myCstr(fndNo.Value) + "'"
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

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            isNewEntry = True
            Dim obj As clsfrmParameterMaster = clsfrmParameterMaster.GetData(strCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.code) > 0 Then
                isNewEntry = False
                fndNo.Value = obj.code
                txtdesc.Text = obj.desc
                cmbtype.SelectedValue = obj.type
                ddlParametereType.SelectedValue = obj.Parametertype
                If obj.nature = "R" Then
                    obj.nature = "Range"
                ElseIf obj.nature = "A" Then
                    obj.nature = "Alphanumeric"
                ElseIf obj.nature = "B" Then
                    obj.nature = "Boolean"
                End If
                ''richa Against Ticket No. BM00000003713 on 03/09/2014
                If obj.IsMandatory = 1 Then
                    chkIsmandatory.Checked = True
                Else
                    chkIsmandatory.Checked = False
                End If
                If obj.IsCanType = 1 Then
                    chkIsCanType.Checked = True
                Else
                    chkIsCanType.Checked = False
                End If
                If obj.IsBulkSale = 1 Then
                    chkBulkSale.Checked = True
                Else
                    chkBulkSale.Checked = False
                End If
                If obj.IsMCC_Qc = 1 Then
                    ChkMccQC.Checked = True
                Else
                    ChkMccQC.Checked = False
                End If

                '' Work done by Parteek on 09/08/2018 ticket no.BHA/09/08/18-000404
                If obj.IsProduction = 1 Then
                    ChkProduction.Checked = True
                Else
                    ChkProduction.Checked = False
                End If
                If obj.IsProductionMandatory = 1 Then
                    ChkMandatoryProdction.Checked = True
                Else
                    ChkMandatoryProdction.Checked = False
                End If
                '' End
                chkMilkSample.Checked = obj.Is_Milk_Sample
                chkMilkGrade.Checked = IIf(obj.IsForMilkGrade = 1, True, False)
                ''----------------------------------------------------
                cbonature.Text = obj.nature
                cmbParamFor.Text = obj.Param_For
                UcAttachment1.LoadData(fndNo.Value)

                btnsave.Text = "Update"
                btndelete.Enabled = True
                fndNo.MyReadOnly = True
                UcAttachment1.LoadData(fndNo.Value)
                loadParameters()
            Else
                Reset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndNo._MYNavigator
        LoadData(clsCommon.myCstr(fndNo.Value), NavType)
    End Sub

    Private Sub fndNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndNo._MYValidating
        Dim str As String = "select count(*) from tspl_parameter_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and code ='" + fndNo.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndNo.MyReadOnly = False
        Else
            fndNo.MyReadOnly = True
        End If
        If fndNo.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "Select Code,Description,Type,Nature,Param_For as [Applicable For],Case when IsMandatory=0 then CAST(0 as BIT) else CAST(1 as BIT) end as IsMandatory,Case when IsBulkSale=0 then CAST(0 as BIT) else CAST(1 as BIT) end as [Bulk Sale],created_by as [Created By],created_date as [Created Date],modified_by as [Modified By],modified_date as [Modified Date],Parametertype as [Parameter Type] from TSPL_PARAMETER_MASTER"
            fndNo.Value = clsCommon.ShowSelectForm("PMTFND", qry, "Code", " comp_code='" + objCommonVar.CurrentCompanyCode + "'", fndNo.Value, "Code", isButtonClicked, "TSPL_PARAMETER_MASTER.created_date")

            If clsCommon.myLen(fndNo.Value) > 0 Then
                LoadData(fndNo.Value, NavigatorType.Current)
            Else
                Reset()
            End If
        End If
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim qry As String = "select count(*) from tspl_parameter_master"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        Dim WHRCLS As String = ""
        If check <= 0 Then
            qry = "select '' as Code,'' as Description,'' as Type,'' as Nature,'' as IsMandatory,'' as [Applicable For],'' as [For Bulk Sale],'' as [For Milk Grade],'' as [Parameter Type],'' as [Production],'' as [Mandatory in Production] "
        Else
            qry = "select Code,Description,(case when Type='CF' then 'CORRECTION FACTOR' else type end) as Type,(case when Nature='A' then 'Alphanumeric' else case" _
                & " when nature='B' then 'Boolean' else 'Range' end end) as Nature,Case when IsMandatory=0 then 'No' else 'Yes' end as IsMandatory,Param_for as [Applicable For]" _
                & " ,case when coalesce(IsBulkSale,0)=1 then 'True' else 'False' end as [For Bulk Sale] ,case when coalesce(IsForMilkGrade,0)=1 then 'True' else 'False' end as [For Milk Grade],tspl_parameter_master.parametertype as [Parameter Type],case when coalesce(IsProduction,0)=1 then 'True' else 'False' end as [Production],case when coalesce(IsProductionMandatory,0)=1 then 'True' else 'False' end as [Mandatory in Production],case when Is_Milk_Sample=1 then 'True' else 'False' end as [Milk Sample] from tspl_parameter_master"
            ''richa 22 Sep,2016 BM00000009810
            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") <> CompairStringResult.Equal Then
                WHRCLS += " AND Type not in ('ABB','AAB') "
            End If
            ''---------------------
        End If
        ListImpExpColumnsMandatory = New List(Of String)({"Code", "Description", "Type", "Applicable For", "Nature", "IsMandatory"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
        transportSql.ExporttoExcel(qry, WHRCLS, "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today


        If transportSql.importExcel(gv, "Code", "Description", "Type", "Nature", "IsMandatory", "Applicable For", "For Bulk Sale", "For Milk Grade", "Parameter Type", "Production", "Mandatory in Production", "Milk Sample") Then
            Try
                Dim counter As Integer = 1
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim code As String = ""
                    Dim desc As String = ""
                    Dim type As String = ""
                    Dim ParamFor As String = ""
                    Dim ParameterType As String = ""
                    Dim ForBulksale As Integer = 0
                    Dim Production As Integer = 0
                    Dim ProductionMandatory As Integer = 0
                    Dim ForMilkGrade As Integer = 0
                    code = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(code) <= 0 Then
                        Throw New Exception("Please Fill Code At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If
                    If clsCommon.myLen(code) > 30 Then
                        Throw New Exception("Length of Code Should Not Exceed Max.30 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If

                    desc = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(desc) <= 0 Then
                        Throw New Exception("Please Fill Description At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If
                    If clsCommon.myLen(desc) > 150 Then
                        Throw New Exception("Length of Description Should Not Exceed Max.150 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If

                    type = clsCommon.myCstr(grow.Cells("Type").Value)
                    ForBulksale = IIf(clsCommon.CompairString(clsCommon.myCstr(clsCommon.myCstr(grow.Cells("For Bulk Sale").Value)), "True") = CompairStringResult.Equal, 1, 0)
                    ForMilkGrade = IIf(clsCommon.CompairString(clsCommon.myCstr(clsCommon.myCstr(grow.Cells("For Milk Grade").Value)), "True") = CompairStringResult.Equal, 1, 0)
                    Production = IIf(clsCommon.CompairString(clsCommon.myCstr(clsCommon.myCstr(grow.Cells("Production").Value)), "True") = CompairStringResult.Equal, 1, 0)
                    ProductionMandatory = IIf(clsCommon.CompairString(clsCommon.myCstr(clsCommon.myCstr(grow.Cells("Mandatory in Production").Value)), "True") = CompairStringResult.Equal, 1, 0)
                    If clsCommon.myLen(type) <= 0 Then
                        Throw New Exception("Please Fill Parameter Type At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If

                    ParamFor = clsCommon.myCstr(grow.Cells("Applicable For").Value)
                    If clsCommon.myLen(ParamFor) <= 0 Then
                        Throw New Exception("Please Fill Applicable For At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If
                    If clsCommon.CompairString(ParamFor, "BOTH") = CompairStringResult.Equal Or clsCommon.CompairString(ParamFor, "MCC") = CompairStringResult.Equal Or clsCommon.CompairString(ParamFor, "PLANT") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("' Applicable For ' should be either MCC/PLANT/BOTH At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If
                    ''==============added by preeti gupta======
                    ParameterType = clsCommon.myCstr(grow.Cells("Parameter Type").Value)
                    If clsCommon.CompairString(ParameterType, "None") = CompairStringResult.Equal Then
                        ParameterType = ""
                    End If

                    If clsCommon.CompairString(ParameterType, "Purchase") = CompairStringResult.Equal Or clsCommon.CompairString(ParameterType, "Procurement") = CompairStringResult.Equal Or clsCommon.CompairString(ParameterType, "Job Work") = CompairStringResult.Equal Or clsCommon.CompairString(ParameterType, "") = CompairStringResult.Equal OrElse clsCommon.CompairString(ParameterType, "None") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("[Parameter Type] should be either Purchase/Procurement/Job Work At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If
                    ''='===============

                    ''richa 22 Sep,2016 BM00000009810
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") <> CompairStringResult.Equal Then
                        If clsCommon.CompairString(type, "FAT") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "SNF") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CLR") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CORRECTION FACTOR") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "TIME") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "OTHERS") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CHANNA") <> CompairStringResult.Equal Then
                            Throw New Exception("Please Fill Parameter Type Any One From " + Environment.NewLine + "FAT/SNF/CLR/CORRECTION FACTOR/TIME/OTHERS/Channa At Line No. " + clsCommon.myCstr(counter) + "")
                            Return
                        End If
                    Else
                        If clsCommon.CompairString(type, "FAT") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "SNF") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CLR") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CORRECTION FACTOR") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "TIME") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "OTHERS") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "ABB") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "AAB") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CHANNA") <> CompairStringResult.Equal Then
                            Throw New Exception("Please Fill Parameter Type Any One From " + Environment.NewLine + "FAT/SNF/CLR/CORRECTION FACTOR/TIME/OTHERS/ABB/AAB/Channa At Line No. " + clsCommon.myCstr(counter) + "")
                            Return
                        End If
                    End If
                    ''---------------------



                    If clsCommon.CompairString(type, "CORRECTION FACTOR") = CompairStringResult.Equal Then
                        type = "CF"
                    End If

                    Dim qry As String = "select count(*) from tspl_parameter_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and code='" + code + "'"
                    Dim chk As Integer = clsDBFuncationality.getSingleValue(qry)

                    If clsCommon.CompairString(type, "OTHERS") <> CompairStringResult.Equal Then
                        qry = "select count(*) from TSPL_PARAMETER_MASTER where comp_code='" + objCommonVar.CurrentCompanyCode + "' and type='" + clsCommon.myCstr(type) + "' and code<>'" + code + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                        If check > 0 Then
                            Throw New Exception("This Type (" + clsCommon.myCstr(type) + ") Is Already Exist,Please Change The Type At Line No. " + clsCommon.myCstr(counter) + "")
                            Return
                        End If
                    End If

                    Dim nature As String = ""
                    nature = clsCommon.myCstr(grow.Cells("Nature").Value)

                    If clsCommon.myLen(nature) <= 0 Then
                        Throw New Exception("Fill nature of parameter as Range/Alphanumeric/Boolean At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.CompairString(nature, "Range") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(nature, "Alphanumeric") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(nature, "Boolean") <> CompairStringResult.Equal Then
                        Throw New Exception("Fill nature of parameter as Range/Alphanumeric/Boolean At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    ''richa 22 Sep,2016 BM00000009810
                    If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowUseBoilingParameteronParameterMaster, clsFixedParameterCode.AllowUseBoilingParameteronParameterMaster, Nothing)), "1") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(type, "ABB") = CompairStringResult.Equal OrElse clsCommon.CompairString(type, "AAB") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(nature, "Range") <> CompairStringResult.Equal Then
                                Throw New Exception("Fill nature of parameter as Range At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                    End If
                    ''---------------------

                    ''richa Against Ticket No. BM00000003713 on 03/09/2014
                    Dim ismandatoryvalue As Integer = 0
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("IsMandatory").Value).ToUpper(), "YES") = CompairStringResult.Equal Then
                        ismandatoryvalue = 1
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells("IsMandatory").Value).ToUpper(), "NO") = CompairStringResult.Equal Then
                        ismandatoryvalue = 0
                    ElseIf clsCommon.myLen(grow.Cells("IsMandatory").Value) <= 0 Then
                        Throw New Exception("IsMandatory cannot be left blank At Line No. " + clsCommon.myCstr(counter) + "")
                    Else
                        Throw New Exception("IsMandatory should be Yes/No At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    ''----------------------------------------------------

                    'Dim isSaved As Boolean = True
                    'Dim coll As New Hashtable()

                    'clsCommon.AddColumnsForChange(coll, "Code", code)
                    'clsCommon.AddColumnsForChange(coll, "description", desc)
                    'clsCommon.AddColumnsForChange(coll, "type", type)

                    'clsCommon.AddColumnsForChange(coll, "modified_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                    'clsCommon.AddColumnsForChange(coll, "modified_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))

                    'If chk <= 0 Then
                    '    clsCommon.AddColumnsForChange(coll, "created_by", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                    '    clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans).ToString("dd/MM/yyyy")))
                    '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PARAMETER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    'Else
                    '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PARAMETER_MASTER", OMInsertOrUpdate.Update, " TSPL_PARAMETER_MASTER.code='" + code + "'", trans)
                    'End If
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    Dim obj As clsfrmParameterMaster = New clsfrmParameterMaster()
                    obj.code = code
                    obj.desc = desc
                    obj.type = type.ToUpper()
                    obj.nature = nature
                    obj.Param_For = ParamFor
                    obj.IsMandatory = ismandatoryvalue
                    obj.IsBulkSale = ForBulksale
                    obj.IsForMilkGrade = ForMilkGrade
                    obj.Is_Milk_Sample = (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Milk Sample").Value), "True") = CompairStringResult.Equal)
                    '' work agaist ticket no.BHA/09/08/18-000404
                    obj.IsProduction = Production
                    obj.IsProductionMandatory = ProductionMandatory
                    '' end

                    If clsCommon.myCstr(ParameterType) = "" Then
                        obj.Parametertype = "None"
                    Else
                        obj.Parametertype = ParameterType
                    End If


                    If clsCommon.CompairString(obj.nature, "Range") = CompairStringResult.Equal Then
                        obj.nature = "R"
                    ElseIf clsCommon.CompairString(obj.nature, "Alphanumeric") = CompairStringResult.Equal Then
                        obj.nature = "A"
                    ElseIf clsCommon.CompairString(obj.nature, "Boolean") = CompairStringResult.Equal Then
                        obj.nature = "B"
                    End If

                    'If chk > 0 Then
                    '    isNewEntry = True
                    'Else
                    '    isNewEntry = False
                    'End If
                    If chk > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True
                    End If
                    If clsfrmParameterMaster.SaveData(code, isNewEntry, trans, obj) Then
                        counter += 1
                    End If

                Next

                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)

                'trans.Commit()
                Reset()
            Catch ex As Exception
                'trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If

        Me.Controls.Remove(gv)
    End Sub

    Private Sub gv_CellValidated(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellValidatedEventArgs) Handles gv.CellValidated
        Try
            Dim MccOrPalnt As String = ""
            Dim ParamCode As String = ""
            Dim isDispatchPrint As Integer = 0
            Dim isQCPrint As Integer = 0
            Dim isMMCollection As Integer = 0
            If Not isLoadData Then
                If e.Column Is gv.Columns(colDispatchPrint) OrElse e.Column Is gv.Columns(colQCPrint) OrElse e.Column Is gv.Columns(colMMCollection) Then
                    isLoadData = True
                    MccOrPalnt = clsCommon.myCstr(gv.CurrentRow.Cells(colApplicableFor).Value)
                    ParamCode = clsCommon.myCstr(gv.CurrentRow.Cells(colParamCode).Value)
                    isDispatchPrint = IIf(clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colDispatchPrint).Value), "YES") = CompairStringResult.Equal, 1, 0)
                    isQCPrint = IIf(clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colQCPrint).Value), "YES") = CompairStringResult.Equal, 1, 0)
                    isMMCollection = IIf(clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colMMCollection).Value), "YES") = CompairStringResult.Equal, 1, 0)

                    If clsCommon.CompairString(MccOrPalnt, "PLANT") = CompairStringResult.Equal AndAlso isDispatchPrint = 1 Then
                        clsCommon.MyMessageBoxShow(Me, " Parameter Applicable for Plant can not be printed on dispatch Challan ", Me.Text)
                        gv.CurrentRow.Cells(colDispatchPrint).Value = "No"
                        isLoadData = False
                        Exit Sub
                    End If
                    Dim DispCount = 0
                    For i As Integer = 0 To gv.Rows.Count - 1
                        If clsCommon.CompairString(gv.Rows(i).Cells(colDispatchPrint).Value, "Yes") = CompairStringResult.Equal Then
                            DispCount = DispCount + 1
                        End If
                    Next

                    If DispCount >= 11 Then
                        clsCommon.MyMessageBoxShow(Me, " Maximum 10 Parameters Are allowed on Dispatch print ", Me.Text)
                        gv.CurrentRow.Cells(colDispatchPrint).Value = "No"
                        isLoadData = False
                        Exit Sub
                    Else
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_PARAMETER_MASTER set IsForPrintOnDispatch=" & isDispatchPrint & " where code='" & ParamCode & "'")
                    End If

                    Dim QCCount = 0
                    For j As Integer = 0 To gv.Rows.Count - 1
                        If clsCommon.CompairString(gv.Rows(j).Cells(colQCPrint).Value, "Yes") = CompairStringResult.Equal Then
                            QCCount = QCCount + 1
                        End If
                    Next

                    If (Not clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal) AndAlso (QCCount >= 11) Then
                        clsCommon.MyMessageBoxShow(Me, " Maximum 10 Parameters Are allowed on QC print ", Me.Text)
                        gv.CurrentRow.Cells(colQCPrint).Value = "No"
                        isLoadData = False
                        Exit Sub
                    Else
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_PARAMETER_MASTER set IsForPrintOnQC=" & isQCPrint & " where code='" & ParamCode & "'")
                    End If

                    If e.Column Is gv.Columns(colMMCollection) Then
                        clsDBFuncationality.ExecuteNonQuery(" update TSPL_PARAMETER_MASTER set IsShowInMMC=" & isMMCollection & " where code='" & ParamCode & "'")
                    End If

                    isLoadData = False
                    ' loadParameters()
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ChkProduction_CheckedChanged(sender As Object, e As EventArgs) Handles ChkProduction.CheckedChanged
        If ChkProduction.Checked Then
            ChkMandatoryProdction.Enabled = True
        Else
            ChkMandatoryProdction.Enabled = False
        End If
    End Sub

    Private Sub ChkMandatoryProdction_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMandatoryProdction.CheckedChanged
        If ChkMandatoryProdction.Checked Then
            ChkProduction.Enabled = False
        Else
            ChkProduction.Enabled = True
        End If
    End Sub
End Class
