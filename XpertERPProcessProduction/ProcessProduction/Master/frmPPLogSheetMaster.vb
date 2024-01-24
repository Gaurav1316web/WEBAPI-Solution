'===========BM00000004405===created by Monika
'===================BM00000007847=======================
'==========BM00000007848=============================

Imports System.Data.SqlClient
Imports common

Public Class frmPPLogSheetMaster
    Inherits FrmMainTranScreen


#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim isNewEntry As Boolean = True
    Dim FORMTYPE As String = Nothing
    Dim trans_id As String = "PRODUCTION"
#End Region

#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Sub Reset()
        txtAliasName.Text = ""
        chk_batch_no.Checked = False
        fndNo.Value = ""
        txtdesc.Text = ""
        cbonature.SelectedValue = "None"
        cmbtype.SelectedValue = ""
        chkReq_Para_Mst.Checked = False
        chkIsmandatory.Checked = False
        fndDepart_code.Value = ""
        TxtDepart_desc.Text = ""
        chkAll.IsChecked = True
        cbgUsers.CheckedAll()
        txtISRef.Text = ""
        txtClsRef.Text = ""
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        btnsave.Text = "Save"
        btndelete.Enabled = False
        fndNo.MyReadOnly = False
        isNewEntry = True
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(FORMTYPE) 'clsUserMgtCode.frmPPLogSheetMaster_QC

        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag

        trans_id = "PRODUCTION"
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.frmPPLogSheetMaster_QC) = CompairStringResult.Equal Then
            trans_id = "STANDARD"
        End If
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
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                                            "TSPL_QC_LOG_SHEET_MASTER " + Environment.NewLine +
                                                            "TSPL_QC_LOG_SHEET_USER_MASTER " + Environment.NewLine +
                                                            "TSPL_PARAMETER_MASTER ")
        End If
    End Sub

    Private Sub LoadNature()
        cbonature.DataSource = Nothing
        Dim qry As String = ""
        qry = "select 'None' as Code,'None' as Name union all select 'R' as Code,'Range' as Name union all select 'A' as Code,'Alphanumeric' as Name union all select 'B' as Code,'Boolean' as Name"
        ''======================================================Manual not seen
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.frmPPLogSheetMaster_QC) <> CompairStringResult.Equal Then 'standard qc parameter master
            qry += " union all select 'M' as Code,'Manual Input' as Name "
        End If
        ''=============================================================

        cbonature.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbonature.ValueMember = "Code"
        cbonature.DisplayMember = "Name"
    End Sub

    Private Sub FrmParameterMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadCombobox()
        LoadUsers()
        LoadNature()
        Reset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        chkReq_Para_Mst.Visible = False
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.frmPPLogSheetMaster_QC) = CompairStringResult.Equal Then 'standard qc parameter master
            chk_batch_no.Visible = False
            cmbtype.Visible = False
            MyLabel3.Visible = False
            btnEx_detail.Visibility = ElementVisibility.Collapsed
            btnEx_User.Visibility = ElementVisibility.Collapsed
            btnIm_Detail.Visibility = ElementVisibility.Collapsed
            btnIm_Users.Visibility = ElementVisibility.Collapsed
        End If
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub LoadUsers()
        Dim qry As String = "select user_code as Code,user_name as [User Name] from TSPL_USER_MASTER"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cbgUsers.DataSource = Nothing
        cbgUsers.DataSource = dt

        cbgUsers.ValueMember = "Code"
        cbgUsers.DisplayMember = "User Name"

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
        dr("Code") = "TIME"
        dr("Name") = "TIME"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "OTHERS"
        dr("Name") = "OTHERS"
        dt.Rows.Add(dr)

        cmbtype.DataSource = Nothing
        cmbtype.DataSource = dt
        cmbtype.DisplayMember = "Name"
        cmbtype.ValueMember = "Code"
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtdesc.Text) <= 0 Then
                txtdesc.Focus()
                txtdesc.Select()
                ErrorControl.SetError(txtdesc, "Fill The Description Of Parameter")
                Throw New Exception("Please Fill Description")
                Return False
            Else
                ErrorControl.ResetError(txtdesc)
            End If

            If clsCommon.CompairString(cmbtype.SelectedValue, "") = CompairStringResult.Equal AndAlso clsCommon.CompairString(FORMTYPE, clsUserMgtCode.frmPPLogSheetMaster) = CompairStringResult.Equal Then
                cmbtype.Select()
                ErrorControl.SetError(cmbtype, "Please Select Type of Parameter")
                Throw New Exception("Please Select Type Values")
                Return False
            Else
                ErrorControl.ResetError(cmbtype)
            End If

            If clsCommon.myLen(cbonature.SelectedValue) <= 0 Or clsCommon.CompairString(cbonature.SelectedValue, "None") = CompairStringResult.Equal Then
                cbonature.Select()
                ErrorControl.SetError(cbonature, "Please select nature of parameter")
                Throw New Exception("Please select nature of parameter")
            Else
                ErrorControl.ResetError(cbonature)
            End If

            If clsCommon.CompairString(cbonature.SelectedValue, "M") = CompairStringResult.Equal AndAlso chkReq_Para_Mst.Checked = True Then
                chkReq_Para_Mst.Focus()
                chkReq_Para_Mst.Select()
                ErrorControl.SetError(chkReq_Para_Mst, "Required in Parameter master cannot go with Manual Input Nature.")
                Throw New Exception("Required in Parameter master cannot go with Manual Input Nature.")
            Else
                ErrorControl.ResetError(chkReq_Para_Mst)
            End If

            If clsCommon.myLen(fndDepart_code.Value) <= 0 Then
                fndDepart_code.Focus()
                fndDepart_code.Select()
                ErrorControl.SetError(TxtDepart_desc, "Select Department")
                Throw New Exception("Select Department")
            Else
                ErrorControl.ResetError(TxtDepart_desc)
            End If

            If clsCommon.CompairString(cmbtype.SelectedValue, "OTHERS") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(FORMTYPE, clsUserMgtCode.frmPPLogSheetMaster) = CompairStringResult.Equal Then
                Dim qry As String = "select count(*) from TSPL_QC_LOG_SHEET_MASTER where trans_id='" + trans_id + "' and type='" + clsCommon.myCstr(cmbtype.SelectedValue) + "' and code <> '" + clsCommon.myCstr(fndNo.Value) + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                If check > 0 Then
                    ErrorControl.SetError(cmbtype, "This Type (" + clsCommon.myCstr(cmbtype.SelectedValue) + ") Is Already Exist,Please Change The Type")
                    Throw New Exception("This Type (" + clsCommon.myCstr(cmbtype.SelectedValue) + ") Is Already Exist,Please Change The Type")
                    Return False
                Else
                    ErrorControl.ResetError(cmbtype)
                End If
            End If

            If cbgUsers.CheckedValue.Count <= 0 AndAlso chkSelect.IsChecked Then
                cbgUsers.Focus()
                Throw New Exception("Select atleast one user.")
            End If
            If chkAll.IsChecked Then
                cbgUsers.CheckedAll()
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Sub SaveData()
        Dim obj As New clsPPLogSheetMaster()
        Dim objtr As New clsPPLogSheetUserMaster()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmPPLogSheetMaster_QC, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        Try
            Dim check As Integer = 0
            chkReq_Para_Mst.Checked = False
            If chkReq_Para_Mst.Checked Then
                Dim qry As String = ""
                If clsCommon.CompairString(clsCommon.myCstr(cmbtype.SelectedValue), "OTHERS") = CompairStringResult.Equal Then
                    qry = "select count(*) from tspl_parameter_master where description='" + txtdesc.Text + "' and type='" + clsCommon.myCstr(cmbtype.SelectedValue) + "'"
                Else
                    qry = "select count(*) from tspl_parameter_master where type='" + clsCommon.myCstr(cmbtype.SelectedValue) + "'"
                End If
                check = clsDBFuncationality.getSingleValue(qry)
            End If

            obj.code = clsCommon.myCstr(fndNo.Value)
            obj.desc = clsCommon.myCstr(txtdesc.Text.Replace("'", "`"))
            obj.AliasName = clsCommon.myCstr(txtAliasName.Text.Replace("'", "`"))
            obj.type = clsCommon.myCstr(cmbtype.SelectedValue)
            If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.frmPPLogSheetMaster_QC) = CompairStringResult.Equal Then
                obj.type = "OTHERS"
            End If
            obj.nature = clsCommon.myCstr(cbonature.SelectedValue)
            obj.IsMandatory = CInt(IIf(chkIsmandatory.Checked = True, 1, 0))
            obj.IsReq_Parameter_Master = CInt(IIf(chkReq_Para_Mst.Checked = True, 1, 0))
            obj.Pick_BO = CInt(IIf(chk_batch_no.Checked = True, 1, 0))
            obj.Trans_Id = trans_id

            obj.Department_COde = clsCommon.myCstr(fndDepart_code.Value)
            obj.ClauseRef = clsCommon.myCstr(txtClsRef.Text)
            obj.ISRef = clsCommon.myCstr(txtISRef.Text)
            obj.Arr = New List(Of clsPPLogSheetUserMaster)

            If cbgUsers.CheckedValue.Count > 0 Then
                For Each usercode As String In cbgUsers.CheckedValue
                    objtr = New clsPPLogSheetUserMaster()

                    objtr.UserCode = usercode

                    If clsCommon.myLen(objtr.UserCode) > 0 Then
                        obj.Arr.Add(objtr)
                    End If
                Next
            End If

            If clsPPLogSheetMaster.SaveData(obj.code, isNewEntry, obj) Then
                If chkReq_Para_Mst.Checked AndAlso check <= 0 AndAlso clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Parameter is auto created in Parameter Master", Me.Text)
                ElseIf clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
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
        Finally
            objtr = Nothing
            obj = Nothing
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Dim qry As String = ""

        Try
            If clsCommon.myLen(fndNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Code For Deletion", Me.Text)
                fndNo.Focus()
                fndNo.Select()
                Return
            Else
                qry = "select count(*) from TSPL_QC_LOG_SHEET_MASTER where trans_id='" + trans_id + "' and code='" + clsCommon.myCstr(fndNo.Value) + "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                If check <= 0 Then
                    fndNo.Focus()
                    fndNo.Select()
                    Throw New Exception("No Data Found For Deletion")
                End If
            End If

            If myMessages.deleteConfirm() Then
                If clsPPLogSheetMaster.DeleteData(fndNo.Value, trans_id) Then
                    myMessages.delete()
                    Reset()
                End If
            End If
        Catch ex As Exception
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
        Dim obj As New clsPPLogSheetMaster()
        Try
            isNewEntry = True
            Reset()
            obj = clsPPLogSheetMaster.GetData(strCode, NavType, trans_id)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.code) > 0 Then
                isNewEntry = False
                fndNo.Value = obj.code
                txtdesc.Text = obj.desc
                txtAliasName.Text = obj.AliasName
                cmbtype.SelectedValue = obj.type
                chkIsmandatory.Checked = clsCommon.myCBool(IIf(obj.IsMandatory = 1, True, False))
                chkReq_Para_Mst.Checked = clsCommon.myCBool(IIf(obj.IsReq_Parameter_Master = 1, True, False))
                chk_batch_no.Checked = clsCommon.myCBool(IIf(obj.Pick_BO = 1, True, False))
                fndDepart_code.Value = obj.Department_COde
                TxtDepart_desc.Text = obj.Department_Name
                cbonature.SelectedValue = obj.nature
                txtClsRef.Text = obj.ClauseRef
                txtISRef.Text = obj.ISRef
                chkAll.IsChecked = True
                cbgUsers.CheckedAll()

                Dim ArrayList1 As New ArrayList()

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsPPLogSheetUserMaster In obj.Arr
                        ArrayList1.Add(objtr.UserCode)
                    Next

                    chkSelect.IsChecked = True
                    cbgUsers.CheckedValue = ArrayList1
                End If

                UcAttachment1.LoadData(fndNo.Value)

                btnsave.Text = "Update"
                btndelete.Enabled = True
                fndNo.MyReadOnly = True

                UcAttachment1.LoadData(fndNo.Value)
            Else
                Reset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub fndNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndNo._MYNavigator
        LoadData(clsCommon.myCstr(fndNo.Value), NavType)
    End Sub

    Private Sub fndNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndNo._MYValidating
        Dim str As String = "select count(*) from TSPL_QC_LOG_SHEET_MASTER where trans_id='" + trans_id + "' and code ='" + fndNo.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fndNo.MyReadOnly = False
        Else
            fndNo.MyReadOnly = True
        End If
        If fndNo.MyReadOnly OrElse isButtonClicked Then
            fndNo.Value = clsPPLogSheetMaster.GetFinder(" trans_id='" + trans_id + "' ", fndNo.Value, isButtonClicked)

            If clsCommon.myLen(fndNo.Value) > 0 Then
                LoadData(fndNo.Value, NavigatorType.Current)
            Else
                Reset()
            End If
        End If
    End Sub

    Private Sub fndDepart_code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndDepart_code._MYValidating
        fndDepart_code.Value = clsDepartmentMaster.getFinder("", fndDepart_code.Value, isButtonClicked)
        TxtDepart_desc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEPARTMENT_NAME from TSPL_DEPARTMENT_MASTER where department_code='" + fndDepart_code.Value + "'"))
    End Sub

    Private Sub chkAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAll.ToggleStateChanged, chkSelect.ToggleStateChanged
        cbgUsers.Enabled = chkSelect.IsChecked
        If chkAll.IsChecked Then
            cbgUsers.CheckedAll()
        Else
            cbgUsers.UnCheckedAll()
        End If
    End Sub

    Private Sub btnEx_detail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEx_detail.Click
        Dim qry As String = "select count(*) from TSPL_QC_LOG_SHEET_MASTER where trans_id='" + trans_id + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        Dim Whr As String = String.Empty
        If check <= 0 Then
            qry = "select '' as Code,'' as Description,'' as Type,'' as Nature,0 as IsMandatory,0 as Pick_Batch_No,'' as Department_Code" ',0 as IsRequired_InParameter_Master
            Whr = ""
        Else
            qry = "select Code,Description,(case when Type='CF' then 'CORRECTION FACTOR' else type end) as Type,(case when Nature='A' then 'Alphanumeric' else case when nature='B' then 'Boolean' else case when nature='M' then 'Manual Input' else 'Range' end end end) as Nature,IsMandatory,Pick_Batch_No,Department_Code from TSPL_QC_LOG_SHEET_MASTER "
            Whr = " and trans_id='" + trans_id + "'"
        End If
        transportSql.ExporttoExcel(qry, Whr, Me)
    End Sub

    Private Sub btnEx_User_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEx_User.Click
        Dim qry As String = "select count(*) from TSPL_QC_LOG_SHEET_USER_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            qry = "select '' as Code,'' as [User Code]"
        Else
            qry = "select Code,emp_code as [User Code] from TSPL_QC_LOG_SHEET_USER_MASTER"
        End If
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnIm_Detail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIm_Detail.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsPPLogSheetMaster = New clsPPLogSheetMaster()

        If transportSql.importExcel(gv, "Code", "Description", "Type", "Nature", "IsMandatory", "Pick_Batch_No", "Department_Code") Then
            Try
                Dim counter As Integer = 1
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim code As String = ""
                    Dim desc As String = ""
                    Dim type As String = ""
                    Dim ParamFor As String = ""
                    Dim qry As String = ""

                    code = clsCommon.myCstr(grow.Cells("Code").Value).Replace("'", "`")
                    If clsCommon.myLen(code) <= 0 Then
                        'Throw New Exception("Please Fill Code At Line No. " + clsCommon.myCstr(counter) + "")
                        'Return
                    End If
                    If clsCommon.myLen(code) > 30 Then
                        Throw New Exception("Length of Code Should Not Exceed Max.30 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If

                    desc = clsCommon.myCstr(grow.Cells("Description").Value).Replace("'", "`")
                    If clsCommon.myLen(desc) <= 0 Then
                        Throw New Exception("Please Fill Description At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If
                    If clsCommon.myLen(desc) > 150 Then
                        Throw New Exception("Length of Description Should Not Exceed Max.150 Characters,See At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If

                    type = clsCommon.myCstr(grow.Cells("Type").Value).Replace("'", "`")

                    If clsCommon.myLen(type) <= 0 Then
                        Throw New Exception("Please Fill Parameter Type At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If

                    If clsCommon.CompairString(type, "FAT") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "SNF") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CLR") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "CORRECTION FACTOR") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "TIME") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(type, "OTHERS") <> CompairStringResult.Equal Then
                        Throw New Exception("Please Fill Parameter Type Any One From " + Environment.NewLine + "FAT/SNF/CLR/CORRECTION FACTOR/TIME/OTHERS At Line No. " + clsCommon.myCstr(counter) + "")
                        Return
                    End If

                    If clsCommon.CompairString(type, "CORRECTION FACTOR") = CompairStringResult.Equal Then
                        type = "CF"
                    End If

                    '======================================
                    If clsCommon.CompairString(type, "OTHERS") <> CompairStringResult.Equal Then
                        qry = "select count(*) from TSPL_QC_LOG_SHEET_MASTER where type='" + clsCommon.myCstr(type) + "'"
                        Dim chk As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If chk > 0 Then
                            isNewEntry = False
                            If clsCommon.CompairString(type, "OTHERS") <> CompairStringResult.Equal Then
                                qry = "select code from TSPL_QC_LOG_SHEET_MASTER where type='" + clsCommon.myCstr(type) + "'"
                            End If
                            code = clsDBFuncationality.getSingleValue(qry, trans)
                            If clsCommon.myLen(code) <= 0 Then
                                Throw New Exception("Please Fill Code At Line No. " + clsCommon.myCstr(counter) + "")
                                Return
                            End If
                        Else
                            isNewEntry = True
                        End If
                    End If


                    '=================================================

                    'If clsCommon.CompairString(type, "OTHERS") <> CompairStringResult.Equal Then
                    '    qry = "select count(*) from TSPL_QC_LOG_SHEET_MASTER where type='" + clsCommon.myCstr(type) + "' and code<>'" + code + "'"
                    '    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

                    '    If check > 0 Then
                    '        Throw New Exception("This Type (" + clsCommon.myCstr(type) + ") Is Already Exist,Please Change The Type At Line No. " + clsCommon.myCstr(counter) + "")
                    '        Return
                    '    End If
                    'End If

                    Dim nature As String = ""
                    nature = clsCommon.myCstr(grow.Cells("Nature").Value).Replace("'", "`")

                    If clsCommon.myLen(nature) <= 0 Then
                        Throw New Exception("Fill nature of paramter as Range/Alphanumeric/Boolean/Manual Input At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.CompairString(nature, "Range") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(nature, "Alphanumeric") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(nature, "Boolean") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(nature, "Manual Input") <> CompairStringResult.Equal Then
                        Throw New Exception("Fill nature of paramter as Range/Alphanumeric/Boolean/Manual Input At Line No. " + clsCommon.myCstr(counter) + "")
                    End If


                    Dim ismandatoryvalue As String = clsCommon.myCstr(grow.Cells("IsMandatory").Value).Replace("'", "`")
                    If clsCommon.myLen(ismandatoryvalue) <= 0 Then
                        Throw New Exception("Fill Ismandatory(0 or 1) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.CompairString(ismandatoryvalue, "1") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(ismandatoryvalue, "0") <> CompairStringResult.Equal Then
                        Throw New Exception("Fill Ismandatory(0 or 1) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim IsRequired_InParameter_Master As String = "0" ' clsCommon.myCstr(grow.Cells("IsRequired_InParameter_Master").Value)
                    If clsCommon.myLen(IsRequired_InParameter_Master) <= 0 Then
                        Throw New Exception("Fill IsRequired_InParameter_Master(0 or 1) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.CompairString(IsRequired_InParameter_Master, "1") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(IsRequired_InParameter_Master, "0") <> CompairStringResult.Equal Then
                        Throw New Exception("Fill IsRequired_InParameter_Master(0 or 1) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If


                    If clsCommon.CompairString(nature, "Manual Input") = CompairStringResult.Equal AndAlso IsRequired_InParameter_Master = "1" Then
                        Throw New Exception("IsRequired_InParameter_Master cannot go with Manual Input Nature At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim pick_bo As String = clsCommon.myCstr(grow.Cells("Pick_Batch_No").Value).Replace("'", "`")
                    If clsCommon.myLen(pick_bo) <= 0 Then
                        Throw New Exception("Fill Pick_Batch_No(0 or 1) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.CompairString(pick_bo, "1") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(pick_bo, "0") <> CompairStringResult.Equal Then
                        Throw New Exception("Fill Pick_Batch_No(0 or 1) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    Dim department_code As String = clsCommon.myCstr(grow.Cells("Department_Code").Value).Replace("'", "`")
                    If clsCommon.myLen(department_code) <= 0 Then
                        Throw New Exception("Fill department code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(department_code) > 0 Then
                        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from tspl_department_master where department_code='" + department_code + "'", trans)
                        If check <= 0 Then
                            Throw New Exception("Filled department code is not exist in master see at line no. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    obj = New clsPPLogSheetMaster()
                    obj.code = code
                    obj.desc = desc
                    obj.type = type.ToUpper()
                    obj.nature = nature
                    obj.IsMandatory = ismandatoryvalue
                    obj.IsReq_Parameter_Master = IsRequired_InParameter_Master
                    obj.Department_COde = department_code
                    obj.Pick_BO = pick_bo

                    If clsCommon.CompairString(obj.nature, "Range") = CompairStringResult.Equal Then
                        obj.nature = "R"
                    ElseIf clsCommon.CompairString(obj.nature, "Alphanumeric") = CompairStringResult.Equal Then
                        obj.nature = "A"
                    ElseIf clsCommon.CompairString(obj.nature, "Boolean") = CompairStringResult.Equal Then
                        obj.nature = "B"
                    ElseIf clsCommon.CompairString(obj.nature, "Manual Input") = CompairStringResult.Equal Then
                        obj.nature = "M"
                    End If
                    obj.Import = False

                    obj.Arr = New List(Of clsPPLogSheetUserMaster)

                    obj.Trans_Id = trans_id

                    If clsPPLogSheetMaster.SaveIMPORTData(code, isNewEntry, trans, obj) Then
                        counter += 1
                    End If

                Next

                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)

                trans.Commit()
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                trans.Rollback()
            Finally
                clsCommon.ProgressBarHide()
                Reset()
                obj = Nothing
            End Try
        End If

        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnIm_Users_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIm_Users.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        If transportSql.importExcel(gv, "Code", "User Code") Then
            Try
                Dim code As String = ""
                Dim usercode As String = ""
                Dim check As Integer = 0

                For Each grow As GridViewRowInfo In gv.Rows
                    code = clsCommon.myCstr(grow.Cells("code").Value)
                    usercode = clsCommon.myCstr(grow.Cells("user code").Value)

                    If clsCommon.myLen(code) <= 0 Then
                        Throw New Exception("Fill code at line no. " + clsCommon.myCstr(grow.Index) + "")
                    End If
                    If clsCommon.myLen(code) > 0 Then
                        check = clsDBFuncationality.getSingleValue("select count(*) from TSPL_QC_LOG_SHEET_MASTER where code='" + code + "'", trans)

                        If check <= 0 Then
                            Throw New Exception("Filled code is not exist at line no. " + clsCommon.myCstr(grow.Index) + "")
                        End If
                    End If

                    If clsCommon.myLen(usercode) <= 0 Then
                        Throw New Exception("Fill User code at line no. " + clsCommon.myCstr(grow.Index) + "")
                    End If
                    If clsCommon.myLen(usercode) > 0 Then
                        check = clsDBFuncationality.getSingleValue("select count(*) from tspl_user_master where user_code='" + usercode + "'", trans)

                        If check <= 0 Then
                            Throw New Exception("Filled User code is not exist at line no. " + clsCommon.myCstr(grow.Index) + "")
                        End If
                    End If

                    Dim coll As New Hashtable()
                    Dim isSaved As Boolean = True
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery("delete from TSPL_QC_LOG_SHEET_USER_MASTER where code='" + code + "' and Emp_Code='" + usercode + "'", trans)

                    clsCommon.AddColumnsForChange(coll, "code", code)
                    clsCommon.AddColumnsForChange(coll, "Emp_Code", usercode)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_QC_LOG_SHEET_USER_MASTER", OMInsertOrUpdate.Insert, "", trans)
                Next

                clsCommon.ProgressBarHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)

            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Finally
                clsCommon.ProgressBarHide()
                Reset()
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnexport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnexport.Click
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.frmPPLogSheetMaster) = CompairStringResult.Equal Then
            Exit Sub
        End If
        Try
            Dim Whr As String = String.Empty
            Dim qry As String = "select count(*) from TSPL_QC_LOG_SHEET_MASTER where trans_id='" + trans_id + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                qry = "select TSPL_QC_LOG_SHEET_MASTER.Code,TSPL_QC_LOG_SHEET_MASTER.Description,(case when TSPL_QC_LOG_SHEET_MASTER.Nature='A' then 'Alphanumeric' else case when TSPL_QC_LOG_SHEET_MASTER.nature='B' then 'Boolean' else case when TSPL_QC_LOG_SHEET_MASTER.nature='M' then 'Manual Input' else 'Range' end end end) as Nature,TSPL_QC_LOG_SHEET_MASTER.IsMandatory,TSPL_QC_LOG_SHEET_MASTER.Department_Code,TSPL_QC_LOG_SHEET_USER_MASTER.Emp_Code as [User Code], TSPL_QC_LOG_SHEET_MASTER.AliasName from TSPL_QC_LOG_SHEET_MASTER left outer join TSPL_QC_LOG_SHEET_USER_MASTER on TSPL_QC_LOG_SHEET_MASTER.Code=TSPL_QC_LOG_SHEET_USER_MASTER.Code "
                Whr = " and trans_id='" + trans_id + "'"
            Else
                qry = "select '' as Code,'' as Description,'Alphanumeric' as Nature,'0' as IsMandatory,'' as Department_Code,'' as [User Code],'' as AliasName"
                Whr = ""
            End If

            transportSql.ExporttoExcel(qry, Whr, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnimport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnimport.Click
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.frmPPLogSheetMaster) = CompairStringResult.Equal Then
            Exit Sub
        End If
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim obj As New clsPPLogSheetMaster()
        Dim objtr As New clsPPLogSheetUserMaster()
        Dim oldnewentry As Boolean = isNewEntry
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            clsCommon.ProgressBarShow()
            Dim qry As String = ""
            Dim check As Integer = 0
            Dim madatory As String = ""
            Dim counter As Integer = 0
            If transportSql.importExcel(gv, "Code", "Description", "Nature", "IsMandatory", "Department_Code", "User Code", "AliasName") Then
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsPPLogSheetMaster()

                    obj.code = clsCommon.myCstr(grow.Cells("code").Value)
                    obj.Trans_Id = trans_id
                    obj.desc = clsCommon.myCstr(grow.Cells("description").Value).Replace("'", "`")
                    If clsCommon.myLen(obj.desc) <= 0 Then
                        Throw New Exception("Fill description at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    If clsCommon.myLen(obj.desc) > 150 Then
                        Throw New Exception("Description having max.150 length at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    obj.AliasName = clsCommon.myCstr(grow.Cells("AliasName").Value).Replace("'", "`")
                    obj.type = "OTHERS"
                    obj.nature = clsCommon.myCstr(grow.Cells("nature").Value)
                    If clsCommon.myLen(obj.nature) <= 0 Then
                        Throw New Exception("Fill nature at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    If clsCommon.CompairString(obj.nature, "Alphanumeric") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.nature, "Boolean") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.nature, "Manual Input") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.nature, "Range") <> CompairStringResult.Equal Then
                        Throw New Exception("Nature should be any one from Range,Boolean,Alphanumeric or Manual Input at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    If clsCommon.CompairString(obj.nature, "Alphanumeric") = CompairStringResult.Equal Then
                        obj.nature = "A"
                    End If
                    If clsCommon.CompairString(obj.nature, "Boolean") = CompairStringResult.Equal Then
                        obj.nature = "B"
                    End If
                    If clsCommon.CompairString(obj.nature, "Manual Input") = CompairStringResult.Equal Then
                        obj.nature = "M"
                    End If
                    If clsCommon.CompairString(obj.nature, "Range") = CompairStringResult.Equal Then
                        obj.nature = "R"
                    End If

                    madatory = clsCommon.myCstr(grow.Cells("IsMandatory").Value)
                    If clsCommon.myLen(madatory) <= 0 Then
                        Throw New Exception("Fill IsMandatory(0 or 1) at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    If clsCommon.CompairString(madatory, "0") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(madatory, "1") <> CompairStringResult.Equal Then
                        Throw New Exception("Fill IsMandatory(0 or 1) at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    obj.IsMandatory = CInt(madatory)

                    obj.IsReq_Parameter_Master = 0
                    obj.Pick_BO = 0
                    obj.Department_COde = clsCommon.myCstr(grow.Cells("Department_Code").Value)
                    If clsCommon.myLen(obj.Department_COde) <= 0 Then
                        Throw New Exception("Fill department code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    qry = "select count(*) from TSPL_DEPARTMENT_MASTER where department_code='" + obj.Department_COde + "'"
                    check = clsDBFuncationality.getSingleValue(qry, trans)
                    If check <= 0 Then
                        Throw New Exception("Filled department code not found see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    obj.Import = True

                    obj.Arr = New List(Of clsPPLogSheetUserMaster)
                    objtr = New clsPPLogSheetUserMaster()

                    objtr.UserCode = clsCommon.myCstr(grow.Cells("User Code").Value)
                    If clsCommon.myLen(objtr.UserCode) <= 0 Then
                        Throw New Exception("Fill user code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    qry = "select count(*) from TSPL_USER_MASTER where user_code='" + objtr.UserCode + "'"
                    check = clsDBFuncationality.getSingleValue(qry, trans)
                    If check <= 0 Then
                        Throw New Exception("Filled user code not found see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    obj.Arr.Add(objtr)

                    isNewEntry = True
                    If clsCommon.myLen(obj.code) > 0 Then
                        qry = "select count(*) from TSPL_QC_LOG_SHEET_MASTER where code='" + obj.code + "' and trans_id='" + obj.Trans_Id + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            isNewEntry = True
                        Else
                            isNewEntry = False
                        End If
                    End If

                    If clsPPLogSheetMaster.SaveIMPORTData(obj.code, isNewEntry, trans, obj) Then
                        counter += 1
                    End If
                Next
            End If

            clsCommon.ProgressBarHide()
            trans.Commit()

            If counter > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Data transfer successfully.", Me.Text)
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
            Me.Controls.Remove(gv)
            isNewEntry = oldnewentry
            objtr = Nothing
            obj = Nothing
        End Try
    End Sub

End Class
