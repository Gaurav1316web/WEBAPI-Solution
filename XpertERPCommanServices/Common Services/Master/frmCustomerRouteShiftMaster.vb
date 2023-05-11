Imports common
Imports System.Data.SqlClient
Public Class frmCustomerRouteShiftMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Const colmcccode As String = "mcccode"
    Const colmccname As String = "mccname"
    Const colplantcode As String = "plantcode"
    Const colplantname As String = "plantname"
    Const colvlccode As String = "VLC_Code"
    Const colvlcname As String = "VLC_Name"
    Const colvillagecode As String = "Village_Code"
    Const colvillname As String = "Village_Name"
    Const colyesno As String = "Yes_No"
    Const colexvillcode As String = "Ex_vill_COde"
    Const colexvillname As String = "Ex_vill_name"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colsno As String = "SNO"
    Const colCustcode As String = "Cust_Code"
    Const colCustname As String = "Cust_Name"
    Const coldate As String = "Date"
    Const colroutecode As String = "Route_Code"
    Const colroutename As String = "Route_Name"
    Const colShift As String = "Shift"
    Const colexroutecode As String = "Ex_route_code"
    Const colexroutename As String = "Ex_route_name"
    Dim isloaddata As Boolean = False
    Dim isvaluecchanged As Boolean = True
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
#End Region

    Private Sub SetUserMgmtNew()
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

        'Dim repomcccode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repomcccode.FormatString = ""
        'repomcccode.Name = colmcccode
        'repomcccode.Width = 75
        'repomcccode.HeaderText = "MCC Code"
        'repomcccode.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repomcccode)

        'Dim repomccname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repomccname.FormatString = ""
        'repomccname.Name = colmccname
        'repomccname.Width = 150
        'repomccname.HeaderText = "MCC Description"
        'repomccname.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repomccname)

        'Dim repoPlantCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoPlantCode.FormatString = ""
        'repoPlantCode.Name = colplantcode
        'repoPlantCode.Width = 75
        'repoPlantCode.HeaderText = "Plant Code"
        'repoPlantCode.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repoPlantCode)

        'Dim repoPlantname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoPlantname.FormatString = ""
        'repoPlantname.Name = colplantname
        'repoPlantname.Width = 150
        'repoPlantname.HeaderText = "Plant Description"
        'repoPlantname.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(repoPlantname)

        Dim repocode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode.FormatString = ""
        repocode.Name = colCustcode
        repocode.Width = 80
        repocode.HeaderText = "Customer Code"
        repocode.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repocode)

        Dim reponame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponame.FormatString = ""
        reponame.Name = colCustname
        reponame.Width = 150
        reponame.HeaderText = "Customer Description"
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
        repostatus.Name = colShift
        repostatus.Width = 60
        repostatus.HeaderText = "Shift"
        repostatus.DataSource = GetShift()
        repostatus.DisplayMember = "Code"
        repostatus.ValueMember = "Code"
        repostatus.IsVisible = True
        gv.MasterTemplate.Columns.Add(repostatus)

        Dim reporoutecode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporoutecode.FormatString = ""
        reporoutecode.Name = colroutecode
        reporoutecode.Width = 80
        reporoutecode.HeaderText = "New Route Code"
        reporoutecode.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
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

    'Function GridCombobox() As DataTable
    '    Dim dt As New DataTable()
    '    dt.Columns.Add("Code", GetType(String))

    '    Dim dr As DataRow = Nothing

    '    dr = dt.NewRow()
    '    dr("Code") = "NO"
    '    dt.Rows.Add(dr)

    '    dr = dt.NewRow()
    '    dr("Code") = "YES"
    '    dt.Rows.Add(dr)

    '    Return dt
    'End Function

    Private Sub frmCustomerRouteShiftMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim coll As Dictionary(Of String, String)
        'coll = New Dictionary(Of String, String)()
        'coll.Add("Doc_No", "varchar(30) NOT null")
        'coll.Add("Description", "varchar(100) NOT NULL")
        'coll.Add("Doc_Date", "date NOT NULL")
        ''coll.Add("Status", "varchar(5) NOT NULL Default 'CLOSE'")
        'coll.Add("SNO", "Integer")
        'coll.Add("Cust_Code", "varchar(12) NOT NULL REFERENCES TSPL_CUSTOMER_MASTER(Cust_Code)")
        'coll.Add("Existing_Route_Code", "varchar(30) NOT NULL")
        ''coll.Add("Route_Status", "varchar(3) NOT NULL Default 'NO'")
        'coll.Add("Effective_Date", "date NULL")
        'coll.Add("New_Route_Code", "varchar(30) NOT NULL")
        'coll.Add("Route_Code", "varchar(30)  NULL")
        'coll.Add("SHIFT", "CHAR(1) NOT NULL")
        'coll.Add("Created_By", "varchar(12) not null")
        'coll.Add("Created_Date", "varchar(10) not null")
        'coll.Add("Modified_By", "varchar(12) not null")
        'coll.Add("Modified_Date", "varchar(10) not null")
        'coll.Add("comp_code", "varchar(8) NULL ")
        'clsCommonFunctionality.CreateOrAlterTable("TSPL_CUSTOMER_ROUTE_SHIFT_MASTER", coll)

        SetUserMgmtNew()
        Reset()
        LoadBlankGrid()
        'LoadCombobox()


        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
    End Sub

    Public Shared Function GetShift() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Desc", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "M"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dt.Rows.Add(dr)

        Return dt
    End Function


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

            'If clsCommon.CompairString(cmbstatus.SelectedValue, "") = CompairStringResult.Equal Then
            '    cmbstatus.Select()
            '    Errorcontrol.SetError(cmbstatus, "Please Select Route Status Shift/Close")
            '    Throw New Exception("Please Select Route Status Shift/Close")
            'Else
            '    Errorcontrol.ResetError(cmbstatus)
            'End If

            Dim custcode As String = ""
            'Dim vlccode As String = ""
            'Dim villcode As String = ""
            Dim routecode As String = ""
            Dim newroute As String = ""
            'Dim newvill As String = ""
            Dim shift As String = ""
            'Dim vlccode1 As String = ""
            Dim custcode1 As String = ""
            'Dim villcode1 As String = ""
            Dim routecode1 As String = ""
            Dim newroute1 As String = ""
            'Dim newvill1 As String = ""
            Dim effectivedate As String = ""

            custcode = clsCommon.myCstr(gv.Rows(0).Cells(colCustcode).Value)
            If clsCommon.myLen(custcode) <= 0 Then
                Throw New Exception("Please Fill Atleast One Row In Grid")
            End If

            For ii As Integer = 0 To gv.Rows.Count - 1
                custcode = clsCommon.myCstr(gv.Rows(ii).Cells(colCustcode).Value)
                routecode = clsCommon.myCstr(gv.Rows(ii).Cells(colexroutecode).Value)
                newroute = clsCommon.myCstr(gv.Rows(ii).Cells(colroutecode).Value)
                effectivedate = clsCommon.myCDate(gv.Rows(ii).Cells(coldate).Value)


                If clsCommon.myLen(custcode) > 0 AndAlso clsCommon.myLen(newroute) > 0 AndAlso clsCommon.myLen(effectivedate) <= 0 Then
                    Errorcontrol.SetError(gv, "Please Fill Effective Date For Route Changed At Line No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                    Throw New Exception("Please Fill Effective Date For Route Changed At Line No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                Else
                    Errorcontrol.ResetError(gv)
                End If


                For jj As Integer = ii + 1 To gv.Rows.Count - 1
                    custcode1 = clsCommon.myCstr(gv.Rows(jj).Cells(colCustcode).Value)
                    routecode1 = clsCommon.myCstr(gv.Rows(jj).Cells(colexroutecode).Value)
                    newroute1 = clsCommon.myCstr(gv.Rows(jj).Cells(colroutecode).Value)

                    If clsCommon.myLen(custcode) > 0 AndAlso (clsCommon.CompairString(custcode, custcode1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(routecode, routecode1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(newroute, newroute1) = CompairStringResult.Equal) AndAlso clsCommon.myLen(routecode) > 0 AndAlso clsCommon.myLen(newroute) > 0 Then
                        Throw New Exception("No Duplicate Rows Allowed,Please Do Changes As Required At Line No. " + clsCommon.myCstr(CInt(jj) + 1) + "")
                    End If
                    If clsCommon.myLen(custcode) > 0 AndAlso clsCommon.CompairString(custcode, custcode1) = CompairStringResult.Equal Then
                        Throw New Exception("No Duplicate Row Allowed,Please Do Changes As Re
quired At Line No. " + clsCommon.myCstr(CInt(jj) + 1) + "")
                    End If
                Next
            Next

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            'vlccode = clsCommon.myCstr(gv.Rows(0).Cells(colvlccode).Value)
            'If clsCommon.myLen(vlccode) <= 0 Then
            '    Throw New Exception("Please Fill Atleast One Row In Grid")
            'End If

            'For ii As Integer = 0 To gv.Rows.Count - 1
            '    vlccode = clsCommon.myCstr(gv.Rows(ii).Cells(colvlccode).Value)
            '    'villcode = clsCommon.myCstr(gv.Rows(ii).Cells(colexvillcode).Value)
            '    routecode = clsCommon.myCstr(gv.Rows(ii).Cells(colexroutecode).Value)
            '    newroute = clsCommon.myCstr(gv.Rows(ii).Cells(colroutecode).Value)
            '    'newvill = clsCommon.myCstr(gv.Rows(ii).Cells(colvillagecode).Value)
            '    'yesno = clsCommon.myCstr(gv.Rows(ii).Cells(colyesno).Value)
            '    effectivedate = clsCommon.myCDate(gv.Rows(ii).Cells(coldate).Value)


            '    If clsCommon.myLen(vlccode) > 0 AndAlso clsCommon.myLen(newroute) > 0 AndAlso clsCommon.myLen(effectivedate) <= 0 Then
            '        Errorcontrol.SetError(gv, "Please Fill Effective Date For Route Changed At Line No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
            '        Throw New Exception("Please Fill Effective Date For Route Changed At Line No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
            '    Else
            '        Errorcontrol.ResetError(gv)
            '    End If


            '    For jj As Integer = ii + 1 To gv.Rows.Count - 1
            '        vlccode1 = clsCommon.myCstr(gv.Rows(jj).Cells(colvlccode).Value)
            '        'villcode1 = clsCommon.myCstr(gv.Rows(jj).Cells(colexvillcode).Value)
            '        routecode1 = clsCommon.myCstr(gv.Rows(jj).Cells(colexroutecode).Value)
            '        newroute1 = clsCommon.myCstr(gv.Rows(jj).Cells(colroutecode).Value)
            '        'newvill1 = clsCommon.myCstr(gv.Rows(jj).Cells(colvillagecode).Value)

            '        If clsCommon.myLen(vlccode) > 0 AndAlso (clsCommon.CompairString(vlccode, vlccode1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(routecode, routecode1) = CompairStringResult.Equal AndAlso clsCommon.CompairString(newroute, newroute1) = CompairStringResult.Equal) AndAlso clsCommon.myLen(routecode) > 0 AndAlso clsCommon.myLen(newroute) > 0 Then
            '            Throw New Exception("No Duplicate Rows Allowed,Please Do Changes As Required At Line No. " + clsCommon.myCstr(CInt(jj) + 1) + "")
            '        End If
            '        If clsCommon.myLen(vlccode) > 0 AndAlso clsCommon.CompairString(vlccode, vlccode1) = CompairStringResult.Equal Then
            '            Throw New Exception("No Duplicate Row Allowed,Please Do Changes As Required At Line No. " + clsCommon.myCstr(CInt(jj) + 1) + "")
            '        End If
            '    Next
            'Next



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Sub SaveData()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmCustomerRouteShiftMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim arr As New List(Of clsfrmCustomerRouteShiftMaster)
            Dim obj As New clsfrmCustomerRouteShiftMaster()

            For Each grow As GridViewRowInfo In gv.Rows()
                obj = New clsfrmCustomerRouteShiftMaster()
                obj.docno = clsCommon.myCstr(fndcode.Value)
                obj.desc = clsCommon.myCstr(txtdesc.Text.Replace("'", "`"))
                obj.ddocdate = txtdate.Text
                'obj.status = clsCommon.myCstr(cmbstatus.SelectedValue)
                obj.sno = CInt(grow.Cells(colsno).Value)
                obj.Custcode = clsCommon.myCstr(grow.Cells(colCustcode).Value)
                obj.exroutecode = clsCommon.myCstr(grow.Cells(colexroutecode).Value)
                'obj.exvillcode = clsCommon.myCstr(grow.Cells(colexvillcode).Value)
                obj.shift = clsCommon.myCstr(grow.Cells(colShift).Value)
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
                obj.newroutename = clsCommon.myCstr(grow.Cells(colroutename).Value)

                'obj.newvillcode = clsCommon.myCstr(grow.Cells(colvillagecode).Value)

                If clsCommon.myLen(obj.Custcode) > 0 Then
                    arr.Add(obj)
                End If
            Next

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If clsfrmCustomerRouteShiftMaster.SaveData(obj.docno, arr, trans) Then
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
            clsCommon.MyMessageBoxShow("Please Select Doc Code For Deletion", Me.Text)
            fndcode.Focus()
            fndcode.Select()
            Errorcontrol.SetError(fndcode, "Please Select Doc Code For Deletion")
            Return
        Else
            Errorcontrol.ResetError(fndcode)
        End If

        If Not clsCommon.MyMessageBoxShow("Are You Sure,Want To Delete The Customer Route Shift Master " + clsCommon.myCstr(fndcode.Value) + "?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        Try
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsfrmCustomerRouteShiftMaster.DeleteData(clsCommon.myCstr(fndcode.Value), trans) Then
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
        Dim qry As String = "select count(*) from TSPL_CUSTOMER_ROUTE_SHIFT_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            qry = "select '' as [Doc No],'' as Description,'' as SNO,'' as [Customer Code],'' as [Customer Desc],'' as [Old Route Code],'' as [Old Route Desc],'' as Shift,'' as [Effective Date],'' as [New Route Code],'' as [New Route Desc] "
        Else
            qry = "select TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.Doc_NO as [Doc No],TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.Description,TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.SNO,TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.Cust_Code as [Customer Code],TSPL_Customer_MASTER.Customer_Name as [Customer Desc],TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.existing_route_code as [Old Route Code],tspl_route_master.route_desc as [Old Route Desc],TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.Shift,TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.effective_date as [Effective Date],TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.new_route_code as [New Route Code],a.Route_Desc as [New Route Desc] from TSPL_CUSTOMER_ROUTE_SHIFT_MASTER left outer join TSPL_Customer_MASTER on TSPL_Customer_MASTER.Cust_Code=TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.cust_code left outer join tspl_route_master on tspl_route_master.route_no=TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.existing_route_code left outer join tspl_route_master a on a.route_no=TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.new_route_code order by [Doc No],TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.SNO"
        End If
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv1, "Doc No", "Description", "SNO", "Customer Code", "Customer Desc", "Old Route Code", "Old Route Desc", "Shift", "Effective Date", "New Route Code", "New Route Desc") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim docno As String = Nothing
            Dim ddocdate As String = Nothing
            Dim desc As String = Nothing
            'Dim status As String = Nothing
            Dim custcode As String = Nothing
            Dim custname As String = Nothing
            Dim exroutecode As String = Nothing
            Dim exroutename As String = Nothing
            'Dim exvillcode As String = Nothing
            'Dim exvillname As String = Nothing
            Dim newroutecode As String = Nothing
            Dim newroutename As String = Nothing
            'Dim newvillcode As String = Nothing
            'Dim newvillname As String = Nothing
            'Dim yesno As String = Nothing
            Dim griddate As String = Nothing
            Dim sno As Integer = Nothing
            'Dim gridstatus As String = Nothing
            Dim shift As String = Nothing
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
                    'status = clsCommon.myCstr(grow.Cells("Status").Value)
                    'If clsCommon.myLen(status) <= 0 Then
                    '    Throw New Exception("Please Fill Status As SHIFT Or CLOSE At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If
                    'If clsCommon.CompairString(status, "Shift") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(status, "Close") <> CompairStringResult.Equal Then
                    '    Throw New Exception("Please Fill Status As SHIFT Or CLOSE At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If

                    '------------------------------
                    custcode = clsCommon.myCstr(grow.Cells("Customer Code").Value)
                    custname = clsCommon.myCstr(grow.Cells("Customer Desc").Value)
                    'If clsCommon.myLen(vlccode) <= 0 AndAlso clsCommon.myLen(vlcname) <= 0 Then
                    'Throw New Exception("Please Fill VLC Code/Description At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If
                    'If clsCommon.myLen(vlcname) > 0 Then
                    'qry = "select vlc_code from tspl_vlc_master_head where vlc_name='" + vlcname + "'"
                    'vlccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    'End If
                    If clsCommon.myLen(custcode) > 0 Then
                        qry = "select count(*) from TSPL_Customer_MASTER where cust_code='" + custcode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Customer Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    Else
                        Throw New Exception("Please Fill Customer Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    qry = "select vlc_name  from TSPL_Customer_MASTER where cust_code='" + custcode + "'"
                    custname = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
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
                        qry = "select count(*) from tspl_route_master where route_code='" + exroutecode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Old Route Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    Else
                        Throw New Exception("Please Fill Old Route Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    qry = "select route_name  from tspl_route_master where route_code='" + exroutecode + "'"
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
                        qry = "select count(*) from tspl_route_master where route_code='" + newroutecode + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("New Route Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                    Else
                        Throw New Exception("Please Fill New Route Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    qry = "select route_desc   from tspl_route_master where route_code='" + newroutecode + "'"
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

                    qry = "select count(*) from TSPL_CUSTOMER_ROUTE_SHIFT_MASTER where doc_no='" + docno + "' and cust_code='" + custcode + "' and existing_route_code='" + exroutecode + "' "
                    check = clsDBFuncationality.getSingleValue(qry, trans)

                    '-------------------------Save Done----------------------------
                    Dim coll As New Hashtable()
                    Dim isSaved As Boolean = True

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Doc_No", docno)
                    clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(ddocdate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Description", desc)
                    clsCommon.AddColumnsForChange(coll, "Shift", shift)
                    'clsCommon.AddColumnsForChange(coll, "Status", status)
                    clsCommon.AddColumnsForChange(coll, "SNO", sno)
                    clsCommon.AddColumnsForChange(coll, "Cust_Code", custcode)
                    clsCommon.AddColumnsForChange(coll, "Existing_Route_Code", exroutecode)
                    'clsCommon.AddColumnsForChange(coll, "Existing_Vill_Code", exvillcode)
                    'clsCommon.AddColumnsForChange(coll, "Route_Status", gridstatus)
                    clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(griddate, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "New_Route_Code", newroutecode)
                    'clsCommon.AddColumnsForChange(coll, "New_Vill_Code", newvillcode)

                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                    If check <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_ROUTE_SHIFT_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_ROUTE_SHIFT_MASTER", OMInsertOrUpdate.Update, " doc_no='" + docno + "'  and cust_code='" + custcode + "' and existing_route_code='" + exroutecode + "' ", trans)
                    End If
                    '--------------------------------------------------

                    '----------------new route updates in Customer master---------------------------------
                    'If clsCommon.myLen(newroutecode) > 0 Then
                    Dim coll1 As New Hashtable()
                        clsCommon.AddColumnsForChange(coll1, "route_no", newroutecode)
                        clsCommon.AddColumnsForChange(coll1, "route_desc", newroutename)
                        clsCommon.AddColumnsForChange(coll1, "Modified_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll1, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_Customer_MASTER", OMInsertOrUpdate.Update, " TSPL_Customer_MASTER.cust_Code='" + custcode + "' and TSPL_Customer_MASTER.route_no='" + exroutecode + "'", trans)

                    'End If

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

    Sub OpenCustomerMaster()
        Dim qry As String = "select TSPL_Customer_MASTER.cust_Code as [Customer Code],TSPL_Customer_MASTER.Customer_Name as [Customer Name],TSPL_Customer_MASTER.Route_No as [Route Code],tspl_route_master.route_desc as [Route Name] from TSPL_Customer_MASTER left outer join tspl_route_master on tspl_route_master.Route_No=TSPL_Customer_MASTER.Route_No  where TSPL_Customer_MASTER.status='N'"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("CUSTFND", qry)

        If dr IsNot Nothing Then
            gv.CurrentRow.Cells(colCustcode).Value = clsCommon.myCstr(dr("Customer Code"))
            gv.CurrentRow.Cells(colCustname).Value = clsCommon.myCstr(dr("Customer Name"))
            gv.CurrentRow.Cells(colexroutecode).Value = clsCommon.myCstr(dr("Route Code"))
            gv.CurrentRow.Cells(colexroutename).Value = clsCommon.myCstr(dr("Route Name"))
            'gv.CurrentRow.Cells(colexvillcode).Value = clsCommon.myCstr(dr("Village Code"))
            'gv.CurrentRow.Cells(colexvillname).Value = clsCommon.myCstr(dr("Village Name"))
        Else
            gv.CurrentRow.Cells(colCustcode).Value = ""
            gv.CurrentRow.Cells(colCustname).Value = ""
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
        If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(colCustcode).Value)) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select First Customer Code/Name", Me.Text)
            Return
        End If

        Dim qry As String = "select TSPL_ROUTE_MASTER.ROute_no as [Route Code],TSPL_ROUTE_MASTER.route_desc as [Route Name] from TSPL_ROUTE_MASTER  where status='A'"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("RTFND12", qry)

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

                If gv.CurrentColumn Is gv.Columns(colCustcode) Then
                    isvaluecchanged = False
                    'OpenVLCMaster()
                    OpenCustomerMaster()
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
                Dim qry As String = "delete from TSPL_CUSTOMER_ROUTE_SHIFT_MASTER where doc_no='" + clsCommon.myCstr(fndcode.Value) + "' and doc_date='" + clsCommon.myCstr(clsCommon.GetPrintDate(txtdate.Text, "dd/MMM/yyyy")) + "' and Cust_Code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colCustcode).Value) + "' and existing_route_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colexroutecode).Value) + "' and new_route_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colroutecode).Value) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsfrmCustomerRouteShiftMaster = clsfrmCustomerRouteShiftMaster.GetData(strCode, NavType)
            gv.Rows.Clear()

            If obj IsNot Nothing Then
                isloaddata = True
                If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                    For Each objtr As clsfrmCustomerRouteShiftMaster In obj.arr

                        fndcode.Value = objtr.docno
                        txtdesc.Text = objtr.desc
                        'cmbstatus.SelectedValue = objtr.status
                        fndRouteCode.Value = clsCommon.myCstr(objtr.Route_Code)
                        Try
                            txtdate.Text = Convert.ToDateTime(objtr.ddocdate)
                        Catch exx As Exception
                            txtdate.Text = clsCommon.GETSERVERDATE()
                        End Try
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colsno).Value = objtr.sno
                        gv.Rows(gv.Rows.Count - 1).Cells(colCustcode).Value = objtr.Custcode
                        gv.Rows(gv.Rows.Count - 1).Cells(colCustname).Value = objtr.Custname
                        gv.Rows(gv.Rows.Count - 1).Cells(colexroutecode).Value = objtr.exroutecode
                        gv.Rows(gv.Rows.Count - 1).Cells(colexroutename).Value = objtr.exroutename
                        'gv.Rows(gv.Rows.Count - 1).Cells(colexvillcode).Value = objtr.exvillcode
                        'gv.Rows(gv.Rows.Count - 1).Cells(colexvillname).Value = objtr.exvillname
                        gv.Rows(gv.Rows.Count - 1).Cells(colShift).Value = objtr.shift
                        Try
                            gv.Rows(gv.Rows.Count - 1).Cells(coldate).Value = Convert.ToDateTime(objtr.griddate)
                        Catch exx As Exception
                        End Try
                        gv.Rows(gv.Rows.Count - 1).Cells(colroutecode).Value = objtr.newroutecode
                        gv.Rows(gv.Rows.Count - 1).Cells(colroutename).Value = objtr.newroutename
                        'gv.Rows(gv.Rows.Count - 1).Cells(colvillagecode).Value = objtr.newvillcode
                        'gv.Rows(gv.Rows.Count - 1).Cells(colvillname).Value = objtr.newvillname
                        'gv.Rows(gv.Rows.Count - 1).Cells(colmcccode).Value = objtr.mcccode
                        'gv.Rows(gv.Rows.Count - 1).Cells(colmccname).Value = objtr.mccname
                        'gv.Rows(gv.Rows.Count - 1).Cells(colplantcode).Value = objtr.plantcode
                        'gv.Rows(gv.Rows.Count - 1).Cells(colplantname).Value = objtr.plantname
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
        Dim qry As String = "SELECT COUNT(*) FROM TSPL_CUSTOMER_ROUTE_SHIFT_MASTER where doc_no='" + clsCommon.myCstr(fndcode.Value) + "'"
        Dim check As String = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            fndcode.MyReadOnly = True
        Else
            fndcode.MyReadOnly = False
        End If

        If fndcode.MyReadOnly OrElse isButtonClicked Then
            qry = "select distinct TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.doc_no as [DocNo],TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.doc_date as [Date],TSPL_CUSTOMER_ROUTE_SHIFT_MASTER.Description from TSPL_CUSTOMER_ROUTE_SHIFT_MASTER"
            fndcode.Value = clsCommon.ShowSelectForm("CRSMFND3", qry, "DocNo", "", fndcode.Value, "DocNo", isButtonClicked)

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
        qry = "select tspl_route_master.Route_No as Route_Code,tspl_route_master.route_desc as Route_Name from tspl_route_master"
        fndRouteCode.Value = clsCommon.ShowSelectForm("ROUFND3", qry, "Route_Code", "status='A'", fndRouteCode.Value, "Route_Code", isButtonClicked)
        gv.Rows.Clear()
        gv.Rows.AddNew()
        isloaddata = True
        If clsCommon.myLen(fndRouteCode.Value) > 0 Then
            'qry = " select TSPL_MCC_ROUTE_MASTER.mcc_code,tspl_mcc_master.mcc_name,tspl_mcc_master.Plant_Code,TSPL_LOCATION_MASTER.Location_Desc AS Plant_Name,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name ,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name  from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code left outer join tspl_mcc_master on tspl_mcc_master.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC left outer join tspl_location_master on tspl_location_master.location_code=tspl_mcc_master.plant_code where TSPL_VLC_MASTER_HEAD.Route_Code='" & fndRouteCode.Value & "' and TSPL_VLC_MASTER_HEAD.active=1"
            qry = " select TSPL_Customer_MASTER.Cust_Code,TSPL_Customer_MASTER.Customer_Name ,TSPL_Customer_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc as Route_Name  from TSPL_Customer_MASTER left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_Customer_MASTER.Route_No 
                     where TSPL_Customer_MASTER.Route_No='" & fndRouteCode.Value & "' and TSPL_Customer_MASTER.status='N'"
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gv.Rows(i).Cells(colsno).Value = (i + 1)

                    'gv.Rows(i).Cells(colmcccode).Value = clsCommon.myCstr(dt.Rows(i)("mcc_code"))
                    'gv.Rows(i).Cells(colmccname).Value = clsCommon.myCstr(dt.Rows(i)("mcc_name"))
                    'gv.Rows(i).Cells(colplantcode).Value = clsCommon.myCstr(dt.Rows(i)("Plant_Code"))
                    'gv.Rows(i).Cells(colplantname).Value = clsCommon.myCstr(dt.Rows(i)("Plant_Name"))

                    gv.Rows(i).Cells(colCustcode).Value = clsCommon.myCstr(dt.Rows(i)("Cust_Code"))
                    gv.Rows(i).Cells(colCustname).Value = clsCommon.myCstr(dt.Rows(i)("Customer_Name"))
                    gv.Rows(i).Cells(colexroutecode).Value = clsCommon.myCstr(dt.Rows(i)("Route_No"))
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
