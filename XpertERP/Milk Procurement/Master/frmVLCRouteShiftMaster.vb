Imports common
Imports System.Data.SqlClient
Public Class FrmVLCRouteShiftMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colsno As String = "SNO"
    Const colmcccode As String = "mcccode"
    Const colmccname As String = "mccname"
    Const colplantcode As String = "plantcode"
    Const colplantname As String = "plantname"
    Const colvlccode As String = "VLC_Code"
    Const colvlcname As String = "VLC_Name"
    Const coldate As String = "Date"
    Const colroutecode As String = "Route_Code"
    Const colroutename As String = "Route_Name"
    Const colvillagecode As String = "Village_Code"
    Const colvillname As String = "Village_Name"
    Const colyesno As String = "Yes_No"
    Const colexroutecode As String = "Ex_route_code"
    Const colexroutename As String = "Ex_route_name"
    Const colexvillcode As String = "Ex_vill_COde"
    Const colexvillname As String = "Ex_vill_name"
    Dim isloaddata As Boolean = False
    Dim isvaluecchanged As Boolean = True
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmVLCRouteShiftMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Reset()
        fndcode.Value = ""
        fndRouteCode.Value = ""
        cmbstatus.SelectedIndex = 1
        fndcode.MyReadOnly = False
        txtdate.Text = clsCommon.GETSERVERDATE()
        txtdesc.Text = ""
        cmbstatus.SelectedValue = ""

        gv.Rows.Clear()
        gv.Rows.AddNew()
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        btnsave.Text = "&Save"
        btndelete.Enabled = False
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
        dr("Code") = "SHIFT"
        dr("Name") = "SHIFT"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "CLOSE"
        dr("Name") = "CLOSE"
        dt.Rows.Add(dr)

        'cmbstatus.DataSource = Nothing
        cmbstatus.DataSource = dt
        cmbstatus.DisplayMember = "Name"
        cmbstatus.ValueMember = "Code"
    End Sub

    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim reposno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposno.FormatString = ""
        reposno.Name = colsno
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        reposno.Width = 60
        gv.MasterTemplate.Columns.Add(reposno)

        Dim repomcccode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomcccode.FormatString = ""
        repomcccode.Name = colmcccode
        repomcccode.Width = 75
        repomcccode.HeaderText = "MCC Code"
        repomcccode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repomcccode)

        Dim repomccname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomccname.FormatString = ""
        repomccname.Name = colmccname
        repomccname.Width = 150
        repomccname.HeaderText = "MCC Description"
        repomccname.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repomccname)

        Dim repoPlantCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPlantCode.FormatString = ""
        repoPlantCode.Name = colplantcode
        repoPlantCode.Width = 75
        repoPlantCode.HeaderText = "Plant Code"
        repoPlantCode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoPlantCode)

        Dim repoPlantname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPlantname.FormatString = ""
        repoPlantname.Name = colplantname
        repoPlantname.Width = 150
        repoPlantname.HeaderText = "Plant Description"
        repoPlantname.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoPlantname)

        Dim repocode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode.FormatString = ""
        repocode.Name = colvlccode
        repocode.Width = 80
        repocode.HeaderText = "VLC Code"
        repocode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repocode)

        Dim reponame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponame.FormatString = ""
        reponame.Name = colvlcname
        reponame.Width = 150
        reponame.HeaderText = "VLC Description"
        reponame.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reponame)

        '================existing values column=====================================
        Dim repoexroutecode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoexroutecode.FormatString = ""
        repoexroutecode.Name = colexroutecode
        repoexroutecode.Width = 80
        repoexroutecode.HeaderText = "Existing Route Code"
        repoexroutecode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoexroutecode)

        Dim repoexroutename As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoexroutename.FormatString = ""
        repoexroutename.Name = colexroutename
        repoexroutename.Width = 120
        repoexroutename.HeaderText = "Existing Route Name"
        repoexroutename.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoexroutename)

        'Dim reporexvillcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'reporexvillcode.FormatString = ""
        'reporexvillcode.Name = colexvillcode
        'reporexvillcode.Width = 80
        'reporexvillcode.HeaderText = "Existing Village Code"
        'reporexvillcode.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(reporexvillcode)

        'Dim repoexvillname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoexvillname.FormatString = ""
        'repoexvillname.Name = colexvillname
        'repoexvillname.Width = 120
        'repoexvillname.HeaderText = "Existing Villge Name"
        'repoexvillname.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repoexvillname)
        '==============================================================================================

        Dim repostatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repostatus.FormatString = ""
        repostatus.Name = colyesno
        repostatus.Width = 60
        repostatus.HeaderText = "Yes/No"
        repostatus.DataSource = GridCombobox()
        repostatus.DisplayMember = "Code"
        repostatus.ValueMember = "Code"
        repostatus.IsVisible = False
        gv.MasterTemplate.Columns.Add(repostatus)

        Dim reporoutecode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporoutecode.FormatString = ""
        reporoutecode.Name = colroutecode
        reporoutecode.Width = 80
        reporoutecode.HeaderText = "New Route Code"
        reporoutecode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        reporoutecode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(reporoutecode)

        Dim reporoutename As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporoutename.FormatString = ""
        reporoutename.Name = colroutename
        reporoutename.Width = 120
        reporoutename.HeaderText = "New Route Name"
        reporoutename.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reporoutename)

        'Dim reporvillcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'reporvillcode.FormatString = ""
        'reporvillcode.Name = colvillagecode
        'reporvillcode.Width = 80
        'reporvillcode.HeaderText = "New Village Code"
        'reporvillcode.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(reporvillcode)

        'Dim repovillname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repovillname.FormatString = ""
        'repovillname.Name = colvillname
        'repovillname.Width = 120
        'repovillname.HeaderText = "New Villge Name"
        'repovillname.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repovillname)

        Dim repodate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repodate.FormatString = ""
        repodate.CustomFormat = "dd/MM/yyyy"
        repodate.Format = DateTimePickerFormat.Custom
        repodate.Name = coldate
        repodate.Width = 80
        repodate.HeaderText = "Effective Date"
        gv.MasterTemplate.Columns.Add(repodate)

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

    Function GridCombobox() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "NO"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "YES"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Sub FrmVLCRouteShiftMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        LoadBlankGrid()
        LoadCombobox()


        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
    End Sub

    Function AllowToSave() As Boolean
        Try
            'If clsCommon.myLen(fndcode.Value) <= 0 Then
            '    fndcode.Focus()
            '    fndcode.Select()
            '    Errorcontrol.SetError(fndcode, "Please Fill Document No")
            '    Throw New Exception("Please Fill Route Id")
            'Else
            '    Errorcontrol.ResetError(fndcode)
            'End If

            If clsCommon.myLen(txtdesc.Text) <= 0 Then
                txtdesc.Focus()
                txtdesc.Select()
                Errorcontrol.SetError(txtdesc, "Please Fill Document Description")
                Throw New Exception("Please Fill Route Description")
            Else
                Errorcontrol.ResetError(txtdesc)
            End If

            If clsCommon.myLen(fndRouteCode.Value) <= 0 Then
                Errorcontrol.SetError(fndRouteCode, "Please Fill Route Code")
                Throw New Exception("Please Fill Route Code")
            Else
                Errorcontrol.ResetError(fndRouteCode)
            End If


            If clsCommon.myLen(txtdate.Text) <= 0 Then
                txtdate.Text = clsCommon.GETSERVERDATE()
            End If

            If clsCommon.CompairString(cmbstatus.SelectedValue, "") = CompairStringResult.Equal Then
                cmbstatus.Select()
                Errorcontrol.SetError(cmbstatus, "Please Select Route Status Shift/Close")
                Throw New Exception("Please Select Route Status Shift/Close")
            Else
                Errorcontrol.ResetError(cmbstatus)
            End If

            Dim vlccode As String = ""
            Dim villcode As String = ""
            Dim routecode As String = ""
            Dim newroute As String = ""
            Dim newvill As String = ""
            Dim yesno As String = ""
            Dim vlccode1 As String = ""
            Dim villcode1 As String = ""
            Dim routecode1 As String = ""
            Dim newroute1 As String = ""
            Dim newvill1 As String = ""
            Dim effectivedate As String = ""

            vlccode = clsCommon.myCstr(gv.Rows(0).Cells(colvlccode).Value)
            If clsCommon.myLen(vlccode) <= 0 Then
                Throw New Exception("Please Fill Atleast One Row In Grid")
            End If

            For ii As Integer = 0 To gv.Rows.Count - 1
                vlccode = clsCommon.myCstr(gv.Rows(ii).Cells(colvlccode).Value)
                'villcode = clsCommon.myCstr(gv.Rows(ii).Cells(colexvillcode).Value)
                routecode = clsCommon.myCstr(gv.Rows(ii).Cells(colexroutecode).Value)
                newroute = clsCommon.myCstr(gv.Rows(ii).Cells(colroutecode).Value)
                'newvill = clsCommon.myCstr(gv.Rows(ii).Cells(colvillagecode).Value)
                'yesno = clsCommon.myCstr(gv.Rows(ii).Cells(colyesno).Value)
                effectivedate = clsCommon.myCDate(gv.Rows(ii).Cells(coldate).Value)

                'If clsCommon.myLen(vlccode) > 0 AndAlso clsCommon.myLen(newroute) <= 0 AndAlso clsCommon.CompairString(cmbstatus.SelectedValue, "SHIFT") = CompairStringResult.Equal Then
                '    Errorcontrol.SetError(gv, "Please Select New Route  At Line No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                '    Throw New Exception("Please Select New Route  At Line No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                'Else
                '    Errorcontrol.ResetError(gv)
                'End If

                'If clsCommon.myLen(vlccode) > 0 AndAlso clsCommon.myLen(yesno) <= 0 Then
                '    Errorcontrol.SetError(gv, "Please Select YES/NO For Route Changed At Line No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                '    Throw New Exception("Please Select YES/NO For Route Changed At Line No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                'Else
                '    Errorcontrol.ResetError(gv)
                'End If

                If clsCommon.myLen(vlccode) > 0 AndAlso clsCommon.myLen(newroute) > 0 AndAlso clsCommon.myLen(effectivedate) <= 0 Then
                    Errorcontrol.SetError(gv, "Please Fill Effective Date For Route Changed At Line No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    Throw New Exception("Please Fill Effective Date For Route Changed At Line No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                Else
                    Errorcontrol.ResetError(gv)
                End If


                For jj As Integer = ii + 1 To gv.Rows.Count - 1
                    vlccode1 = clsCommon.myCstr(gv.Rows(jj).Cells(colvlccode).Value)
                    'villcode1 = clsCommon.myCstr(gv.Rows(jj).Cells(colexvillcode).Value)
                    routecode1 = clsCommon.myCstr(gv.Rows(jj).Cells(colexroutecode).Value)
                    newroute1 = clsCommon.myCstr(gv.Rows(jj).Cells(colroutecode).Value)
                    'newvill1 = clsCommon.myCstr(gv.Rows(jj).Cells(colvillagecode).Value)

                    If clsCommon.myLen(vlccode) > 0 AndAlso (clsCommon.CompairString(vlccode, vlccode1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(routecode, routecode1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(newroute, newroute1) = CompairStringResult.Equal) AndAlso clsCommon.myLen(routecode) > 0 AndAlso clsCommon.myLen(newroute) > 0 Then
                        Throw New Exception("No Duplicate Rows Allowed,Please Do Changes As Required At Line No. " + clsCommon.myCstr(CInt(jj) + 1) + "")
                    End If
                    If clsCommon.myLen(vlccode) > 0 AndAlso clsCommon.CompairString(vlccode, vlccode1) = CompairStringResult.Equal Then
                        Throw New Exception("No Duplicate Row Allowed,Please Do Changes As Required At Line No. " + clsCommon.myCstr(CInt(jj) + 1) + "")
                    End If
                Next
            Next


            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub SaveData()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmVLCRouteShiftMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim arr As New List(Of clsfrmVLCRouteShiftMaster)
            Dim obj As New clsfrmVLCRouteShiftMaster()

            For Each grow As GridViewRowInfo In gv.Rows()
                obj = New clsfrmVLCRouteShiftMaster()
                obj.docno = clsCommon.myCstr(fndcode.Value)
                obj.desc = clsCommon.myCstr(txtdesc.Text.Replace("'", "`"))
                obj.ddocdate = txtdate.Text
                obj.status = clsCommon.myCstr(cmbstatus.SelectedValue)
                obj.sno = CInt(grow.Cells(colsno).Value)
                obj.vlccode = clsCommon.myCstr(grow.Cells(colvlccode).Value)
                obj.exroutecode = clsCommon.myCstr(grow.Cells(colexroutecode).Value)
                'obj.exvillcode = clsCommon.myCstr(grow.Cells(colexvillcode).Value)
                obj.gridstatus = clsCommon.myCstr(grow.Cells(colyesno).Value)
                obj.Route_Code = clsCommon.myCstr(fndRouteCode.Value)
                Try
                    obj.griddate = grow.Cells(coldate).Value
                Catch exx As Exception
                    obj.griddate = clsCommon.GETSERVERDATE()
                End Try

                If clsCommon.myLen(obj.griddate) <= 0 Then
                    obj.griddate = clsCommon.GETSERVERDATE()
                End If

                obj.newroutecode = clsCommon.myCstr(grow.Cells(colroutecode).Value)
                'obj.newvillcode = clsCommon.myCstr(grow.Cells(colvillagecode).Value)

                If clsCommon.myLen(obj.vlccode) > 0 Then
                    arr.Add(obj)
                End If
            Next

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If clsfrmVLCRouteShiftMaster.SaveData(obj.docno, arr, trans) Then
                    fndcode.Value = arr(0).docno
                    If clsCommon.CompairString(btnsave.Text, "&Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                    End If
                    btnsave.Text = "&Update"
                    btndelete.Enabled = True
                    fndcode.MyReadOnly = True
                    UcAttachment1.SaveData(fndcode.Value)

                    'If Not clsCommon.MyMessageBoxShow("Want To Maintained VLC Route Shift History?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    'Else
                    '    If clsfrmVLCRouteShiftMaster.SaveHistoryData(fndcode.Value, arr, trans) Then
                    '        If clsCommon.CompairString(btnsave.Text, "&Save") = CompairStringResult.Equal Then
                    '            clsCommon.MyMessageBoxShow("History Saved Successfully", Me.Text)
                    '        Else
                    '            clsCommon.MyMessageBoxShow("History Updated Successfully", Me.Text)
                    '        End If
                    '    End If
                    'End If
                Else
                    btnsave.Text = "&Save"
                    btndelete.Enabled = False
                    fndcode.MyReadOnly = False
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If clsCommon.myLen(fndcode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Route Id For Deletion", Me.Text)
            fndcode.Focus()
            fndcode.Select()
            Errorcontrol.SetError(fndcode, "Please Select Route Id For Deletion")
            Return
        Else
            Errorcontrol.ResetError(fndcode)
        End If

        If Not clsCommon.MyMessageBoxShow("Are You Sure,Want To Delete The VLC Route Shift Master Of Route Id " + clsCommon.myCstr(fndcode.Value) + "?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        Try
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsfrmVLCRouteShiftMaster.DeleteData(clsCommon.myCstr(fndcode.Value), trans) Then
                clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim qry As String = "select count(*) from TSPL_VLC_ROUTE_SHIFT_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            qry = "select '' as [Doc No],'' as Description,'' as Status,'' as SNO,'' as [VLC Code],'' as [VLC Desc],'' as [Old Route Code],'' as [Old Route Desc],'' as [Effective Date],'' as [New Route Code],'' as [New Route Desc] "
        Else
            qry = "select TSPL_VLC_ROUTE_SHIFT_MASTER.Doc_NO as [Doc No],TSPL_VLC_ROUTE_SHIFT_MASTER.Description,TSPL_VLC_ROUTE_SHIFT_MASTER.Status,TSPL_VLC_ROUTE_SHIFT_MASTER.SNO,TSPL_VLC_ROUTE_SHIFT_MASTER.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [VLC Desc],TSPL_VLC_ROUTE_SHIFT_MASTER.existing_route_code as [Old Route Code],tspl_mcc_route_master.route_name as [Old Route Desc],TSPL_VLC_ROUTE_SHIFT_MASTER.effective_date as [Effective Date],TSPL_VLC_ROUTE_SHIFT_MASTER.new_route_code as [New Route Code],a.route_name as [New Route Desc] from TSPL_VLC_ROUTE_SHIFT_MASTER left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_VLC_ROUTE_SHIFT_MASTER.vlc_code left outer join tspl_mcc_route_master on tspl_mcc_route_master.route_code=TSPL_VLC_ROUTE_SHIFT_MASTER.existing_route_code left outer join tspl_mcc_route_master a on a.route_code=TSPL_VLC_ROUTE_SHIFT_MASTER.new_route_code"
        End If
        ListImpExpColumnsMandatory = New List(Of String)({"Doc No", "Description", "Status", "VLC Code", "Old Route Code", "New Route Code", "Effective Date", "SNO"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Doc No", "VLC Code", "Old Route Code"})
        transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv1, "Doc No", "Description", "Status", "SNO", "VLC Code", "VLC Desc", "Old Route Code", "Old Route Desc", "Effective Date", "New Route Code", "New Route Desc") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim docno As String = Nothing
            Dim ddocdate As String = Nothing
            Dim desc As String = Nothing
            Dim status As String = Nothing
            Dim vlccode As String = Nothing
            Dim vlcname As String = Nothing
            Dim exroutecode As String = Nothing
            Dim exroutename As String = Nothing
            Dim exvillcode As String = Nothing
            Dim exvillname As String = Nothing
            Dim newroutecode As String = Nothing
            Dim newroutename As String = Nothing
            Dim newvillcode As String = Nothing
            Dim newvillname As String = Nothing
            Dim yesno As String = Nothing
            Dim griddate As String = Nothing
            Dim sno As Integer = Nothing
            Dim gridstatus As String = Nothing
            Dim counter As Integer = 1
            Dim qry As String = ""
            Dim check As Integer = 0

            clsCommon.ProgressBarShow()
            Try
                For Each grow As GridViewRowInfo In gv1.Rows()
                    docno = clsCommon.myCstr(grow.Cells("Doc No").Value)
                    If clsCommon.myLen(docno) <= 0 Then
                        Throw New Exception("Please Fill Doc No At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    desc = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(desc) <= 0 Then
                        Throw New Exception("Please Fill Description At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(desc) > 100 Then
                        Throw New Exception("Length Of Description Should Not Exceed 100 Characters At Line No." + clsCommon.myCstr(counter) + "")
                    End If

                    ddocdate = clsCommon.myCstr(clsCommon.GETSERVERDATE(trans))
                    status = clsCommon.myCstr(grow.Cells("Status").Value)
                    If clsCommon.myLen(status) <= 0 Then
                        Throw New Exception("Please Fill Status As SHIFT Or CLOSE At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.CompairString(status, "Shift") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(status, "Close") <> CompairStringResult.Equal Then
                        Throw New Exception("Please Fill Status As SHIFT Or CLOSE At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    '------------------------------
                    vlccode = clsCommon.myCstr(grow.Cells("VLC Code").Value)
                    vlcname = clsCommon.myCstr(grow.Cells("VLC Desc").Value)
                    'If clsCommon.myLen(vlccode) <= 0 AndAlso clsCommon.myLen(vlcname) <= 0 Then
                    'Throw New Exception("Please Fill VLC Code/Description At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If
                    'If clsCommon.myLen(vlcname) > 0 Then
                    'qry = "select vlc_code from tspl_vlc_master_head where vlc_name='" + vlcname + "'"
                    'vlccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    'End If
                    If clsCommon.myLen(vlccode) > 0 Then
                        qry = "select count(*) from tspl_vlc_master_head where vlc_code='" + vlccode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("VLC Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    Else
                        Throw New Exception("Please Fill VLC Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    qry = "select vlc_name  from tspl_vlc_master_head where vlc_code='" + vlccode + "'"
                    vlcname = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    '----------------------------------------------
                    exroutecode = clsCommon.myCstr(grow.Cells("Old Route Code").Value)
                    exroutename = clsCommon.myCstr(grow.Cells("Old Route Desc").Value)
                    If clsCommon.myLen(exroutecode) <= 0 AndAlso clsCommon.myLen(exroutename) <= 0 Then
                        Throw New Exception("Please Fill Old Route Code/Description At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    'If clsCommon.myLen(exroutename) > 0 Then
                    '    qry = "select route_code from tspl_mcc_route_master where route_name='" + exroutename + "'"
                    '    exroutecode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    'End If
                    If clsCommon.myLen(exroutecode) > 0 Then
                        qry = "select count(*) from tspl_mcc_route_master where route_code='" + exroutecode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Old Route Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    Else
                        Throw New Exception("Please Fill Old Route Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    qry = "select route_name  from tspl_mcc_route_master where route_code='" + exroutecode + "'"
                    exroutename = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    '-------------------------------------
                    'exvillcode = clsCommon.myCstr(grow.Cells("Old Village Code").Value)
                    'exvillname = clsCommon.myCstr(grow.Cells("Village Name").Value)
                    'If clsCommon.myLen(exvillcode) <= 0 AndAlso clsCommon.myLen(exvillname) <= 0 Then
                    '    Throw New Exception("Please Fill Old Village Code/Description At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If
                    'If clsCommon.myLen(exvillname) > 0 Then
                    '    qry = "select village_code from tspl_village_master where village_name='" + exvillname + "'"
                    '    exvillcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    'End If
                    'If clsCommon.myLen(exvillcode) > 0 Then
                    '    qry = "select count(*) from tspl_village_master where village_code='" + exvillcode + "'"
                    '    check = clsDBFuncationality.getSingleValue(qry, trans)
                    '    If check <= 0 Then
                    '        Throw New Exception("Old Village Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                    '    End If

                    '    'qry = "select count(*) from tspl_vlc_master_detail where vlc_code='" + vlccode + "' and village_code='" + exvillcode + "' and route_code='" + exroutecode + "'"
                    '    'check = clsDBFuncationality.getSingleValue(qry, trans)
                    '    'If check <= 0 Then
                    '    '    Throw New Exception("First Mapped Old Route Code And Old Village Code To Filled VLC Code In VLC Master Exist At Line No. " + clsCommon.myCstr(counter) + "")
                    '    'End If
                    'Else
                    '    Throw New Exception("Please Fill Old Village Code At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If
                    '-------------------------------------------

                    newroutecode = clsCommon.myCstr(grow.Cells("New Route Code").Value)
                    newroutename = clsCommon.myCstr(grow.Cells("New Route Desc").Value)
                    If clsCommon.myLen(newroutecode) <= 0 AndAlso clsCommon.myLen(newroutename) <= 0 Then
                        Throw New Exception("Please Fill New Route Code/Description At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    'If clsCommon.myLen(newroutename) > 0 Then
                    '    qry = "select route_code from tspl_mcc_route_master where route_name='" + newroutename + "'"
                    '    newroutecode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    'End If
                    If clsCommon.myLen(newroutecode) > 0 Then
                        qry = "select count(*) from tspl_mcc_route_master where route_code='" + newroutecode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("New Route Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    Else
                        Throw New Exception("Please Fill New Route Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    qry = "select route_name   from tspl_mcc_route_master where route_code='" + newroutecode + "'"
                    newroutename = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    '---------------------------------------------------------

                    'newvillcode = clsCommon.myCstr(grow.Cells("New Village Code").Value)
                    'newvillname = clsCommon.myCstr(grow.Cells("New Village Name").Value)
                    'If clsCommon.myLen(newvillcode) <= 0 AndAlso clsCommon.myLen(newvillname) <= 0 Then
                    '    Throw New Exception("Please Fill New Village Code/Description At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If
                    'If clsCommon.myLen(newvillname) > 0 Then
                    '    qry = "select village_code from tspl_village_master where village_name='" + newvillname + "'"
                    '    newvillcode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    'End If
                    'If clsCommon.myLen(newvillcode) > 0 Then
                    '    qry = "select count(*) from tspl_village_master where village_code='" + newvillcode + "'"
                    '    check = clsDBFuncationality.getSingleValue(qry, trans)
                    '    If check <= 0 Then
                    '        Throw New Exception("New Village Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                    '    End If

                    '    qry = "select count(*) from tspl_mcc_route_master where route_code='" + newroutecode + "' and village_code='" + newvillcode + "'"
                    '    check = clsDBFuncationality.getSingleValue(qry, trans)

                    '    If check <= 0 Then
                    '        Throw New Exception("First Mapped New Village Code With New Route Code In MCC Route Master Exist At Line No. " + clsCommon.myCstr(counter) + "")
                    '    End If
                    'Else
                    '    Throw New Exception("Please Fill New Village Code At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If
                    '-------------------------------------------------------

                    griddate = clsCommon.myCstr(grow.Cells("Effective Date").Value)
                    If clsCommon.myLen(griddate) <= 0 Then
                        Throw New Exception("Please Fill Effective Date At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    sno = CInt(grow.Cells("SNO").Value)
                    If clsCommon.myLen(sno) <= 0 Or CInt(sno) <= 0 Then
                        Throw New Exception("Please Fill SNO(Numeric Value Only) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    'gridstatus = clsCommon.myCstr(grow.Cells("Status Yes/No").Value)
                    'If clsCommon.myLen(gridstatus) <= 0 Or (clsCommon.CompairString(gridstatus, "YES") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(gridstatus, "NO") <> CompairStringResult.Equal) Then
                    '    Throw New Exception("Please Fill Route Shift Status As Yes/No At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If

                    qry = "select count(*) from TSPL_VLC_ROUTE_SHIFT_MASTER where doc_no='" + docno + "' and vlc_code='" + vlccode + "' and existing_route_code='" + exroutecode + "' "
                    check = clsDBFuncationality.getSingleValue(qry, trans)

                    '-------------------------Save Done----------------------------
                    Dim coll As New Hashtable()
                    Dim isSaved As Boolean = True

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Doc_No", docno)
                    clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(ddocdate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Description", desc)
                    clsCommon.AddColumnsForChange(coll, "Status", status)
                    clsCommon.AddColumnsForChange(coll, "SNO", sno)
                    clsCommon.AddColumnsForChange(coll, "VLC_Code", vlccode)
                    clsCommon.AddColumnsForChange(coll, "Existing_Route_Code", exroutecode)
                    'clsCommon.AddColumnsForChange(coll, "Existing_Vill_Code", exvillcode)
                    clsCommon.AddColumnsForChange(coll, "Route_Status", gridstatus)
                    clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(griddate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "New_Route_Code", newroutecode)
                    'clsCommon.AddColumnsForChange(coll, "New_Vill_Code", newvillcode)

                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                    If check <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_ROUTE_SHIFT_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VLC_ROUTE_SHIFT_MASTER", OMInsertOrUpdate.Update, " doc_no='" + docno + "'  and vlc_code='" + vlccode + "' and existing_route_code='" + exroutecode + "' ", trans)
                    End If
                    '--------------------------------------------------

                    '----------------new route and village updates in vlc master---------------------------------
                    Dim coll1 As New Hashtable()
                    'clsCommon.AddColumnsForChange(coll1, "village_code", newvillcode)
                    clsCommon.AddColumnsForChange(coll1, "route_code", newroutecode)
                    clsCommon.AddColumnsForChange(coll1, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll1, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_VLC_MASTER_HEAD", OMInsertOrUpdate.Update, " TSPL_VLC_MASTER_HEAD.VLC_Code='" + vlccode + "' and TSPL_VLC_MASTER_HEAD.route_code='" + exroutecode + "'", trans)

                    counter += 1
                Next

                clsCommon.ProgressBarHide()
                trans.Commit()
                Reset()
                clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv1)
    End Sub

    Sub OpenVLCMaster()
        Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as [VLC Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [Description],TSPL_VLC_MASTER_HEAD.Village_Code as [Village Code],TSPL_VILLAGE_MASTER.Village_Name as [Village Name],TSPL_VLC_MASTER_HEAD.Route_Code as [Route Code],tspl_mcc_route_master.route_name as [Route Name],TSPL_VLC_MASTER_HEAD.Vehical_Name as [Vehical Name] from TSPL_VLC_MASTER_HEAD left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code=TSPL_VLC_MASTER_HEAD.Village_Code left outer join tspl_mcc_route_master on tspl_mcc_route_master.route_CODE=TSPL_VLC_MASTER_HEAD.Route_Code  where TSPL_VLC_MASTER_HEAD.active=1"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("VLCFND", qry)

        If dr IsNot Nothing Then
            gv.CurrentRow.Cells(colvlccode).Value = clsCommon.myCstr(dr("VLC Code"))
            gv.CurrentRow.Cells(colvlcname).Value = clsCommon.myCstr(dr("Description"))
            gv.CurrentRow.Cells(colexroutecode).Value = clsCommon.myCstr(dr("Route Code"))
            gv.CurrentRow.Cells(colexroutename).Value = clsCommon.myCstr(dr("Route Name"))
            'gv.CurrentRow.Cells(colexvillcode).Value = clsCommon.myCstr(dr("Village Code"))
            'gv.CurrentRow.Cells(colexvillname).Value = clsCommon.myCstr(dr("Village Name"))
        Else
            gv.CurrentRow.Cells(colvlccode).Value = ""
            gv.CurrentRow.Cells(colvlcname).Value = ""
            gv.CurrentRow.Cells(colexroutecode).Value = ""
            gv.CurrentRow.Cells(colexroutename).Value = ""
            'gv.CurrentRow.Cells(colexvillcode).Value = ""
            'gv.CurrentRow.Cells(colexvillname).Value = ""
            gv.CurrentRow.Cells(colroutecode).Value = ""
            gv.CurrentRow.Cells(colroutename).Value = ""
            'gv.CurrentRow.Cells(colvillagecode).Value = ""
            'gv.CurrentRow.Cells(colvillname).Value = ""
        End If
    End Sub

    Sub OpenRouteMaster()
        If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(colvlccode).Value)) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select First VLC Code/Name", Me.Text)
            Return
        End If

        Dim qry As String = "select TSPL_MCC_ROUTE_MASTER.ROute_COde as [Route Code],TSPL_MCC_ROUTE_MASTER.route_name as [Route Name] from TSPL_MCC_ROUTE_MASTER  where active=1"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("RTFND", qry)

        If dr IsNot Nothing Then
            gv.CurrentRow.Cells(colroutecode).Value = clsCommon.myCstr(dr("Route Code"))
            gv.CurrentRow.Cells(colroutename).Value = clsCommon.myCstr(dr("Route Name"))
            'gv.CurrentRow.Cells(colvillagecode).Value = clsCommon.myCstr(dr("Village Code"))
            'gv.CurrentRow.Cells(colvillname).Value = clsCommon.myCstr(dr("Village Name"))

            If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colexroutecode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colroutecode).Value)) = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("New Route Can't Be Same As Old Route,Please Select Valid Route Code", Me.Text)
                gv.CurrentRow.Cells(colroutecode).Value = ""
                gv.CurrentRow.Cells(colroutename).Value = ""
                'gv.CurrentRow.Cells(colvillagecode).Value = ""
                'gv.CurrentRow.Cells(colvillname).Value = ""
            End If
        Else
            gv.CurrentRow.Cells(colroutecode).Value = ""
            gv.CurrentRow.Cells(colroutename).Value = ""
            'gv.CurrentRow.Cells(colvillagecode).Value = ""
            'gv.CurrentRow.Cells(colvillname).Value = ""
        End If
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isloaddata Then
            If isvaluecchanged Then

                If gv.CurrentColumn Is gv.Columns(colvlccode) Then
                    isvaluecchanged = False
                    OpenVLCMaster()
                    isvaluecchanged = True
                End If

                If gv.CurrentColumn Is gv.Columns(colroutecode) Then
                    isvaluecchanged = False
                    OpenRouteMaster()
                    isvaluecchanged = True
                End If
            End If
        End If
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.Rows(intCurrRow).Cells(colsno).Value = clsCommon.myCstr(intCurrRow + 1)
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If clsCommon.myLen(fndcode.Value) > 0 Then
            If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            Else
                Dim qry As String = "delete from TSPL_VLC_ROUTE_SHIFT_MASTER where doc_no='" + clsCommon.myCstr(fndcode.Value) + "' and doc_date='" + clsCommon.myCstr(clsCommon.GetPrintDate(txtdate.Text, "dd/MMM/yyyy")) + "' and vlc_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colvlccode).Value) + "' and existing_route_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colexroutecode).Value) + "' and new_route_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colroutecode).Value) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsfrmVLCRouteShiftMaster = clsfrmVLCRouteShiftMaster.GetData(strCode, NavType)
            gv.Rows.Clear()

            If obj IsNot Nothing Then
                isloaddata = True
                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    For Each objtr As clsfrmVLCRouteShiftMaster In obj.arr

                        fndcode.Value = objtr.docno
                        txtdesc.Text = objtr.desc
                        cmbstatus.SelectedValue = objtr.status
                        fndRouteCode.Value = clsCommon.myCstr(objtr.Route_Code)
                        Try
                            txtdate.Text = Convert.ToDateTime(objtr.ddocdate)
                        Catch exx As Exception
                            txtdate.Text = clsCommon.GETSERVERDATE()
                        End Try
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colsno).Value = objtr.sno
                        gv.Rows(gv.Rows.Count - 1).Cells(colvlccode).Value = objtr.vlccode
                        gv.Rows(gv.Rows.Count - 1).Cells(colvlcname).Value = objtr.vlcname
                        gv.Rows(gv.Rows.Count - 1).Cells(colexroutecode).Value = objtr.exroutecode
                        gv.Rows(gv.Rows.Count - 1).Cells(colexroutename).Value = objtr.exroutename
                        'gv.Rows(gv.Rows.Count - 1).Cells(colexvillcode).Value = objtr.exvillcode
                        'gv.Rows(gv.Rows.Count - 1).Cells(colexvillname).Value = objtr.exvillname
                        gv.Rows(gv.Rows.Count - 1).Cells(colyesno).Value = objtr.gridstatus
                        Try
                            gv.Rows(gv.Rows.Count - 1).Cells(coldate).Value = Convert.ToDateTime(objtr.griddate)
                        Catch exx As Exception
                        End Try
                        gv.Rows(gv.Rows.Count - 1).Cells(colroutecode).Value = objtr.newroutecode
                        gv.Rows(gv.Rows.Count - 1).Cells(colroutename).Value = objtr.newroutename
                        'gv.Rows(gv.Rows.Count - 1).Cells(colvillagecode).Value = objtr.newvillcode
                        'gv.Rows(gv.Rows.Count - 1).Cells(colvillname).Value = objtr.newvillname
                        gv.Rows(gv.Rows.Count - 1).Cells(colmcccode).Value = objtr.mcccode
                        gv.Rows(gv.Rows.Count - 1).Cells(colmccname).Value = objtr.mccname
                        gv.Rows(gv.Rows.Count - 1).Cells(colplantcode).Value = objtr.plantcode
                        gv.Rows(gv.Rows.Count - 1).Cells(colplantname).Value = objtr.plantname
                    Next
                End If
                isloaddata = False
                btnsave.Text = "&Update"
                btndelete.Enabled = True
                fndcode.MyReadOnly = True
                UcAttachment1.LoadData(fndcode.Value)
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndcode._MYNavigator
        LoadData(fndcode.Value, NavType)
    End Sub

    Private Sub fndcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcode._MYValidating
        Dim qry As String = "SELECT COUNT(*) FROM tspl_vlc_route_shift_master where doc_no='" + clsCommon.myCstr(fndcode.Value) + "'"
        Dim check As String = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            fndcode.MyReadOnly = True
        Else
            fndcode.MyReadOnly = False
        End If

        If fndcode.MyReadOnly OrElse isButtonClicked Then
            qry = "select distinct tspl_vlc_route_shift_master.doc_no as [DocNo],tspl_vlc_route_shift_master.doc_date as [Date],tspl_vlc_route_shift_master.Description,tspl_vlc_route_shift_master.Status from tspl_vlc_route_shift_master"
            fndcode.Value = clsCommon.ShowSelectForm("VLCFND3", qry, "DocNo", "", fndcode.Value, "DocNo", isButtonClicked)

            If clsCommon.myLen(fndcode) > 0 Then

                LoadData(fndcode.Value, NavigatorType.Current)
            Else
                Reset()
            End If
        End If
    End Sub

    Private Sub fndRouteCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndRouteCode._MYValidating
        Dim qry As String = String.Empty
        Dim dt As DataTable
        fndRouteCode.Value = clsfrmMilkRouteMaster.getFinder(" ACTIVE=1 ", fndRouteCode.Value, isButtonClicked)
        isloaddata = True
        If clsCommon.myLen(fndRouteCode.Value) > 0 Then
            qry = " select TSPL_MCC_ROUTE_MASTER.mcc_code,tspl_mcc_master.mcc_name,tspl_mcc_master.Plant_Code,TSPL_LOCATION_MASTER.Location_Desc AS Plant_Name,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name ,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name  from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code left outer join tspl_mcc_master on tspl_mcc_master.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC left outer join tspl_location_master on tspl_location_master.location_code=tspl_mcc_master.plant_code where TSPL_VLC_MASTER_HEAD.Route_Code='" & fndRouteCode.Value & "' and TSPL_VLC_MASTER_HEAD.active=1"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gv.Rows(i).Cells(colsno).Value = (i + 1)

                    gv.Rows(i).Cells(colmcccode).Value = clsCommon.myCstr(dt.Rows(i)("mcc_code"))
                    gv.Rows(i).Cells(colmccname).Value = clsCommon.myCstr(dt.Rows(i)("mcc_name"))
                    gv.Rows(i).Cells(colplantcode).Value = clsCommon.myCstr(dt.Rows(i)("Plant_Code"))
                    gv.Rows(i).Cells(colplantname).Value = clsCommon.myCstr(dt.Rows(i)("Plant_Name"))

                    gv.Rows(i).Cells(colvlccode).Value = clsCommon.myCstr(dt.Rows(i)("VLC_CODE"))
                    gv.Rows(i).Cells(colvlcname).Value = clsCommon.myCstr(dt.Rows(i)("VLC_Name"))
                    gv.Rows(i).Cells(colexroutecode).Value = clsCommon.myCstr(dt.Rows(i)("Route_Code"))
                    gv.Rows(i).Cells(colexroutename).Value = clsCommon.myCstr(dt.Rows(i)("Route_Name"))
                    gv.Rows.AddNew()
                Next
            Else
                gv.Rows.Clear()
                gv.Rows.AddNew()
            End If
        Else
            Reset()
        End If
        isloaddata = False
    End Sub

    Private Sub cmbstatus_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbstatus.SelectedValueChanged
        If clsCommon.CompairString(cmbstatus.SelectedValue.ToString, "SHIFT") = CompairStringResult.Equal OrElse clsCommon.CompairString(cmbstatus.SelectedValue.ToString, "CLOSE") = CompairStringResult.Equal Then
            gv.Columns(colroutecode).ReadOnly = False
        Else
            gv.Columns(colroutecode).ReadOnly = True
        End If
    End Sub
End Class
