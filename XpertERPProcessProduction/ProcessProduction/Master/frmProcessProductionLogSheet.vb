'---BM00000003351--------Monika----13/08/2014---------BM00000003759--BM00000004406---
Imports common
Imports System.Data.SqlClient

Public Class FrmProcessProductionLogSheet
    Inherits FrmMainTranScreen

#Region "Variables"
    Public StrLogSheetNo As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim Errorcontrol As New clsErrorControl()
    Dim isInsideLoadData As Boolean = False
    Dim isCellvaluechanged As Boolean = False

    Const colLineno As String = "Line No"
    Const colstatus As String = "Status"
    Const colSeqno As String = "Seqno"
    Const colParamcode As String = "paramcode"
    Const colParamdesc As String = "Paramdesc"
    Const colParamType As String = "ParamType"
#End Region

    Private Sub FrmProcessProductionLogSheet_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.Focus()
            btnsave.Select()
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.G AndAlso btngo.Enabled Then
            btngo.PerformClick()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                                 "TSPL_PP_LOG_SHEET_HEAD " + Environment.NewLine + _
                                                 "TSPL_PP_LOG_SHEET_DETAIL ")
        End If
    End Sub

    Private Sub LoadParamGrid()
        gv_Param.Rows.Clear()
        gv_Param.Columns.Clear()

        Dim repolineno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno.HeaderText = "S.No."
        repolineno.Name = colLineno
        repolineno.FormatString = ""
        repolineno.Width = 60
        repolineno.ReadOnly = True
        gv_Param.MasterTemplate.Columns.Add(repolineno)

        Dim repolineno1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repolineno1.HeaderText = "Status"
        repolineno1.Name = colstatus
        repolineno1.FormatString = ""
        repolineno1.Width = 60
        gv_Param.MasterTemplate.Columns.Add(repolineno1)

        Dim repolineno2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolineno2.HeaderText = "Sequence No."
        repolineno2.Name = colSeqno
        repolineno2.FormatString = ""
        repolineno2.Width = 60
        repolineno2.DecimalPlaces = 0
        gv_Param.MasterTemplate.Columns.Add(repolineno2)

        Dim repolineno31 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno31.HeaderText = "Description"
        repolineno31.Name = colParamdesc
        repolineno31.FormatString = ""
        repolineno31.Width = 250
        repolineno31.ReadOnly = True
        gv_Param.MasterTemplate.Columns.Add(repolineno31)

        Dim repolineno31Type As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno31Type.HeaderText = "Type"
        repolineno31Type.Name = colParamType
        repolineno31Type.FormatString = ""
        repolineno31Type.Width = 100
        repolineno31Type.ReadOnly = True
        gv_Param.MasterTemplate.Columns.Add(repolineno31Type)

        Dim repolineno3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolineno3.HeaderText = "Parameter"
        repolineno3.Name = colParamcode
        repolineno3.FormatString = ""
        repolineno3.Width = 100
        repolineno3.ReadOnly = True
        gv_Param.MasterTemplate.Columns.Add(repolineno3)

        gv_Param.AllowDeleteRow = True
        gv_Param.AllowAddNewRow = False
        gv_Param.ShowGroupPanel = False
        gv_Param.AllowColumnReorder = True
        gv_Param.AllowRowReorder = False
        gv_Param.EnableSorting = False
        gv_Param.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_Param.MasterTemplate.ShowRowHeaderColumn = False
        gv_Param.EnableFiltering = False
        gv_Param.Rows.AddNew()

        LoadParameters()
    End Sub

    Private Sub FunReset()
        isNewEntry = True
        txtdesc.Text = ""
        txtCode.Value = ""
        dtpDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtcategorycode.Value = ""
        txtcategoryname.Text = ""
        txtstagecode.Value = ""
        txtstagename.Text = ""
        txtsequnce.Text = Nothing
        txtstart_time.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MM/yyyy hh:mm tt")
        txtend_time.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MM/yyyy hh:mm tt")
        txtdiff.Text = Nothing
        cbodiff.SelectedValue = ""
        LoadParamGrid()
        LoadBlankGrid(Nothing)


        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        btnsave.Enabled = True
        btndelete.Enabled = False
        btnsave.Text = "Save"
        txtCode.MyReadOnly = False
        btngo.Enabled = True

        chkMannual.Enabled = True
        chkMannual.Checked = False
        txtstart_time.Enabled = True
        txtend_time.Enabled = True
        txtdiff.Enabled = True
        cbodiff.Enabled = True

        RadPageView1.SelectedPage = RadPageViewPage1
        txtdesc.Focus()
        txtdesc.Select()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmProcessProductionLogSheet)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        'btnPrint.Enabled = MyBase.isPainting
    End Sub

    Private Sub LoadBlankGrid(ByVal columns As String)
        Try


            gv.DataSource = Nothing

            Dim qry As String '= "select distinct (select distinct ',['+description+']' from TSPL_PARAMETER_MASTER for xml path('')) as x"
            'Dim pivotheader As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

            'If clsCommon.myLen(pivotheader) <= 0 Then
            '    Throw New Exception("Do entry of parameter master first")
            'End If

            'If clsCommon.myLen(pivotheader) > 0 AndAlso pivotheader.Substring(0, 1) = "," Then
            '    pivotheader = pivotheader.Substring(1, pivotheader.Length - 1)
            'End If

            'qry = "select * from (select 0 as [S.No.],'' as [Time],Code,Description from tspl_parameter_master ) as s pivot(max(code) for description in (" + pivotheader + "))t"

            If clsCommon.myLen(columns) > 0 Then
                columns = columns.Replace(",", ",'' as ")
            End If

            qry = "select 0 as [S.No.],'' as [Time]" + columns + ""
            If chkMannual.Checked Then
                qry = "select 0 as [S.No.],CONVERT(varchar,getdate(),101)+ ' ' + SUBSTRING(CONVERT(varchar,getdate(),108),1,5) + ' '+ SUBSTRING(CONVERT(varchar,getdate(),109),25,2) as [Time]" + columns + ""
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gv.DataSource = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = dt
                'gv.Rows.Clear()
                gv.Rows(0).IsVisible = False

                For Each col As GridViewColumn In gv.Columns
                    col.Width = 150
                    If chkMannual.Checked AndAlso col.Index = 1 Then
                        col.ReadOnly = False 'time column editable when mannual
                    Else
                        col.ReadOnly = True
                    End If
                Next
                gv.Columns(0).Width = 60
                'gv.Columns(0).ReadOnly = True

                gv.Columns(1).Width = 120
                'gv.Columns(1).ReadOnly = True
                

                'gv.AutoSize = True
                'gv.ReadOnly = True
                gv.AllowDeleteRow = True
                gv.AllowAddNewRow = False
                gv.ShowGroupPanel = False
                gv.AllowColumnReorder = False
                gv.AllowRowReorder = False
                gv.EnableSorting = False
                gv.EnableFiltering = False
                gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
                gv.MasterTemplate.ShowRowHeaderColumn = False
                gv.Rows.NewRow()
                If chkMannual.Checked Then
                    gv.Columns(1).ReadOnly = False
                    gv.Columns(1).FormatString = "" '{0:dd/MM/yyyy hh:mm tt}
                    gv.Rows.AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadComboBox()
        Dim qry As String = "select * from (select '' as Code,'None' as Name union all select 'Sec' as Code,'Second' as Name union all select 'Min' as Code,'Minute' as Name union all select 'Hour' as Code,'Hour' as Name)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cbodiff.DataSource = Nothing

        cbodiff.DataSource = dt
        cbodiff.DisplayMember = "Name"
        cbodiff.ValueMember = "Code"
    End Sub

    Private Sub FrmProcessProductionLogSheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadComboBox()
        FunReset()

        ButtonToolTip.SetToolTip(btnNew, "Alt+N for new window")
        ButtonToolTip.SetToolTip(btnNew, "Alt+S for save data")
        ButtonToolTip.SetToolTip(btnNew, "Alt+D for delete data")
        ButtonToolTip.SetToolTip(btnNew, "Alt+C for close window")
        ButtonToolTip.SetToolTip(btnNew, "Alt+G for fill QC grid")

        If clsCommon.myLen(StrLogSheetNo) > 0 Then
            LoadData(StrLogSheetNo, NavigatorType.Current)
            btnsave.Enabled = False
            btndelete.Enabled = False
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub LoadParameters()
        Dim qry As String = "select Code,[Description] as Descrptn,type from TSPL_QC_LOG_SHEET_MASTER where trans_id='PRODUCTION'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        gv_Param.Rows.Clear()
        gv_Param.EnableFiltering = True
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gv_Param.Rows.AddNew()

                gv_Param.Rows(gv_Param.Rows.Count - 1).Cells(colLineno).Value = CInt(gv_Param.Rows.Count)
                gv_Param.Rows(gv_Param.Rows.Count - 1).Cells(colstatus).Value = False
                gv_Param.Rows(gv_Param.Rows.Count - 1).Cells(colSeqno).Value = Nothing
                gv_Param.Rows(gv_Param.Rows.Count - 1).Cells(colParamcode).Value = clsCommon.myCstr(dr("Code"))
                gv_Param.Rows(gv_Param.Rows.Count - 1).Cells(colParamdesc).Value = clsCommon.myCstr(dr("Descrptn"))
                gv_Param.Rows(gv_Param.Rows.Count - 1).Cells(colParamType).Value = clsCommon.myCstr(dr("type"))
            Next
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            'If clsCommon.myLen(txtcategorycode.Value) <= 0 Then
            '    txtcategorycode.Focus()
            '    txtcategorycode.Select()
            '    Errorcontrol.SetError(txtcategoryname, "Select production category")
            '    Throw New Exception("Select production category")
            'Else
            '    Errorcontrol.ResetError(txtcategoryname)
            'End If

            If clsCommon.myLen(txtstagecode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtstagecode.Focus()
                txtstagecode.Select()
                Errorcontrol.SetError(txtstagename, "Select stage detail")
                Throw New Exception("Select stage detail")
            Else
                Errorcontrol.ResetError(txtstagename)
            End If

            If clsCommon.myLen(txtsequnce.Text) <= 0 Then
                txtsequnce.Text = 0
            End If
            If clsCommon.myLen(txtdiff.Text) <= 0 Then
                txtdiff.Text = 0
            End If

            'If CInt(txtsequnce.Text) <= 0 Then
            '    txtsequnce.Focus()
            '    txtsequnce.Select()
            '    Errorcontrol.SetError(txtsequnce, "Fill stage sequence no.")
            '    Throw New Exception("Fill stage sequence no.")
            'Else
            '    Errorcontrol.ResetError(txtsequnce)
            'End If

            '-----------------check sequence no--------------------------------
            'Dim qry As String = "select count(*) from TSPL_PP_LOG_SHEET_HEAD where structure_code='" + txtcategorycode.Value + "' and sequence_no='" + txtsequnce.Text + "' and doc_no<>'" + clsCommon.myCstr(txtCode.Value) + "'"
            'Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            'If check > 0 Then
            '    txtsequnce.Focus()
            '    txtsequnce.Select()
            '    Errorcontrol.SetError(txtsequnce, "Filled sequence no. is already in used.")
            '    Throw New Exception("Filled sequence no. is already in used.")
            'Else
            '    Errorcontrol.ResetError(txtsequnce)
            'End If
            '--------------------------------------------------------------

            If Not chkMannual.Checked Then
                If clsCommon.myLen(txtstart_time.Text) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtstart_time.Focus()
                    txtstart_time.Select()
                    Errorcontrol.SetError(txtstart_time, "Fill start time")
                    Throw New Exception("Fill start time")
                Else
                    Errorcontrol.ResetError(txtstart_time)
                End If

                If clsCommon.myLen(txtend_time.Text) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtend_time.Focus()
                    txtend_time.Select()
                    Errorcontrol.SetError(txtend_time, "Fill end time")
                    Throw New Exception("Fill end time")
                Else
                    Errorcontrol.ResetError(txtend_time)
                End If

                If CInt(txtdiff.Text) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtdiff.Focus()
                    txtdiff.Select()
                    Errorcontrol.SetError(txtdiff, "Fill difference of time")
                    Throw New Exception("Fill difference of time")
                Else
                    Errorcontrol.ResetError(txtdiff)
                End If

                If clsCommon.CompairString(cbodiff.SelectedValue, "") = CompairStringResult.Equal Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    cbodiff.Select()
                    Errorcontrol.SetError(cbodiff, "Select diffrence status(Second/Minute/Hour)")
                    Throw New Exception("Select diffrence status(Second/Minute/Hour)")
                Else
                    Errorcontrol.ResetError(cbodiff)
                End If

                If clsCommon.myCDate(txtend_time.Text) <= clsCommon.myCDate(txtstart_time.Text) Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    txtend_time.Focus()
                    txtend_time.Select()
                    Errorcontrol.SetError(txtend_time, "End time should be greater than Start time")
                    Throw New Exception("End time should be greater than Start time")
                Else
                    Errorcontrol.ResetError(txtend_time)
                End If
            End If

            If gv.Columns.Count <= 2 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                btngo.Focus()
                btngo.Select()
                Errorcontrol.SetError(btngo, "Click on go button for filling grid data.")
                Throw New Exception("Click on go button for filling grid data.")
            Else
                Errorcontrol.ResetError(btngo)
            End If

            If chkMannual.Checked Then
                For Each grow As GridViewRowInfo In gv.Rows
                    Try
                        If clsCommon.myCdbl(grow.Cells(0).Value) > 0 Then
                            Convert.ToDateTime(clsCommon.myCstr(grow.Cells(1).Value))

                            If clsCommon.myLen(grow.Cells(1).Value) < 19 Then
                                RadPageView1.SelectedPage = RadPageViewPage1
                                gv.CurrentRow = gv.Rows(grow.Index)
                                Throw New Exception("Fill time in [dd/MM/yyyy hh:mm tt] format only at row no. " + clsCommon.myCstr(grow.Index) + ".")
                            End If
                        End If
                    Catch ex As Exception
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv.CurrentRow = gv.Rows(grow.Index)
                        Throw New Exception("Fill time in [dd/MM/yyyy hh:mm tt] format only at row no. " + clsCommon.myCstr(grow.Index) + ".")
                    End Try
                Next
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub SaveData()
        Try
            Dim obj As New clsProcessProductionLogSheet()

            obj.Doc_no = clsCommon.myCstr(txtCode.Value)
            obj.Doc_Date = clsCommon.myCDate(dtpDate.Text)
            obj.Description = clsCommon.myCstr(txtdesc.Text)
            obj.Cate_Code = clsCommon.myCstr(txtcategorycode.Value)
            obj.Stage_code = clsCommon.myCstr(txtstagecode.Value)
            obj.start_time = clsCommon.myCDate(txtstart_time.Text)
            obj.end_time = clsCommon.myCDate(txtend_time.Text)
            obj.diff_time = CInt(txtdiff.Text)
            obj.Combo_Value = clsCommon.myCstr(cbodiff.SelectedValue)
            obj.sequnce = CInt(txtsequnce.Text)
            obj.Is_Mannual = CInt(IIf(chkMannual.Checked = True, 1, 0))

            obj.Arr = New List(Of clsProcessProductionLogSheetDetail)

            For Each grow As GridViewColumn In gv.Columns
                If clsCommon.CompairString(clsCommon.myCstr(grow.HeaderText), "S.No.") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.HeaderText), "Time") <> CompairStringResult.Equal Then
                    obj.columns_name = obj.columns_name + ",[" + clsCommon.myCstr(grow.HeaderText) + "]"
                End If
            Next

            Dim cmprI As Integer = 1
            For Each grow As GridViewRowInfo In gv_Param.Rows
                Dim objtr As New clsProcessProductionLogSheetDetail()
                If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(colstatus).Value), True) = CompairStringResult.Equal Then
                    For Each grow_time As GridViewRowInfo In gv.Rows
                        objtr = New clsProcessProductionLogSheetDetail()

                        objtr.Sno = CInt(grow.Cells(colSeqno).Value)
                        objtr.param_code = clsCommon.myCstr(grow.Cells(colParamcode).Value)
                        objtr.xtime = clsCommon.myCstr(grow_time.Cells(1).Value)

                        If clsCommon.myLen(objtr.param_code) > 0 AndAlso clsCommon.myLen(objtr.xtime) > 0 AndAlso clsCommon.myCdbl(grow_time.Cells(0).Value) > 0 Then
                            obj.Arr.Add(objtr)
                        ElseIf chkMannual.Checked AndAlso cmprI <= gv.Rows.Count - 1 AndAlso clsCommon.myCdbl(grow_time.Cells(0).Value) <= 0 AndAlso grow_time.Index > 0 AndAlso grow_time.Index < 2 Then
                            objtr.xtime = ""
                            obj.Arr.Add(objtr)
                            cmprI += 1
                        End If
                    Next
                    cmprI = 1
                End If
            Next
            'For ii As Integer = 2 To gv.Columns.Count - 1 '---------lopp for getting columns value(columns in pivot)

            '    For Each grow As GridViewRowInfo In gv.Rows
            '        Dim objtr As New clsProcessProductionLogSheetDetail()

            '        If grow.Cells(0).Value Is DBNull.Value Then
            '            grow.Cells(0).Value = 0
            '        End If

            '        objtr.Sno = CInt(grow.Cells(0).Value)
            '        objtr.xtime = clsCommon.myCstr(grow.Cells(1).Value)
            '        objtr.param_code = clsCommon.myCstr(gv.Rows(0).Cells(ii).Value)
            '        objtr.param_value = clsCommon.myCstr(grow.Cells(ii).Value)

            '        If clsCommon.myLen(objtr.param_value) > 100 Then
            '            objtr.param_value = objtr.param_value.Substring(0, 100)
            '        End If

            '        If objtr.Sno > 0 AndAlso clsCommon.myLen(objtr.param_code) > 0 Then
            '            obj.Arr.Add(objtr)
            '        End If
            '    Next
            'Next

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsProcessProductionLogSheet.SaveData(txtCode.Value, obj, isNewEntry, trans) Then
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If

                txtCode.Value = obj.Doc_no
                UcAttachment1.SaveData(txtCode.Value)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCode.Focus()
                txtCode.Select()
                Errorcontrol.SetError(txtCode, "Select first log sheet no.")
                Throw New Exception("Select first log sheet no.")
            Else
                Errorcontrol.ResetError(txtCode)
            End If


            If myMessages.deleteConfirm() Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If clsProcessProductionLogSheet.DeleteData(txtCode.Value, trans) Then
                    myMessages.delete()
                    FunReset()
                End If
            End If
            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsProcessProductionLogSheet = clsProcessProductionLogSheet.GetData(strCode, NavType)

            isNewEntry = True
            isInsideLoadData = False
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_no) > 0 Then
                isInsideLoadData = True
                isNewEntry = False
                txtCode.Value = obj.Doc_no
                dtpDate.Text = obj.Doc_Date
                txtdesc.Text = obj.Description
                txtcategorycode.Value = obj.Cate_Code
                txtcategoryname.Text = obj.Cate_Name
                txtstagecode.Value = obj.Stage_code
                txtstagename.Text = obj.stage_name
                txtsequnce.Text = obj.sequnce
                chkMannual.Checked = clsCommon.myCBool(IIf(obj.Is_Mannual = 1, True, False))
                txtstart_time.Text = obj.start_time
                txtend_time.Text = obj.end_time
                txtdiff.Text = obj.diff_time
                cbodiff.SelectedValue = obj.Combo_Value

                LoadParamGrid()

                Dim columnName As String = ""

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each grow As GridViewRowInfo In gv_Param.Rows
                        For Each objtr As clsProcessProductionLogSheetDetail In obj.Arr
                            If clsCommon.CompairString(objtr.param_code, clsCommon.myCstr(grow.Cells(colParamcode).Value)) = CompairStringResult.Equal Then
                                grow.Cells(colstatus).Value = True
                                grow.Cells(colSeqno).Value = objtr.Sno
                                columnName = columnName + ",[" + clsProcessProductionLogSheet.GetParam_Name(clsCommon.myCstr(grow.Cells(colParamcode).Value)) + "]"
                            End If
                        Next
                    Next
                End If

                'FillTime(columnName)
                btngo.PerformClick()
                
                UcAttachment1.LoadData(obj.Doc_no)

                If gv.Rows.Count < 2 Then
                    gv.Rows.AddNew()
                End If

                If chkMannual.Checked Then
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select distinct time_value from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + txtCode.Value + "' order by Time_Value")
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            If clsCommon.myLen(dr("time_value")) > 0 Then
                                gv.Rows(gv.Rows.Count - 1).Cells(0).Value = gv.Rows.Count - 1
                                gv.Rows(gv.Rows.Count - 1).Cells(1).Value = clsCommon.myCstr(dr("time_value"))
                                gv.Rows(gv.Rows.Count - 1).Cells(1).ReadOnly = False
                                gv.Rows.AddNew()
                            End If
                        Next
                    End If
                    
                End If

                '================check if log-sheet use in section stage mapping then no change done here.
                Dim check As Integer = 0
                check = clsDBFuncationality.getSingleValue("select count(*) from TSPL_SECTION_STAGE_MAPPING where log_sheet_no='" + txtCode.Value + "'")

                gv_Param.Columns(colstatus).ReadOnly = False
                gv_Param.Columns(colSeqno).ReadOnly = False
                If check > 0 Then
                    gv_Param.Columns(colstatus).ReadOnly = True
                    gv_Param.Columns(colSeqno).ReadOnly = True
                End If
                '====================================================================

                btnsave.Enabled = True
                btndelete.Enabled = True
                btnsave.Text = "Update"
                txtCode.MyReadOnly = True
                'btngo.Enabled = False
                txtstart_time.Enabled = False
                txtend_time.Enabled = False
                txtdiff.Enabled = False
                cbodiff.Enabled = False
                chkMannual.Enabled = False
            Else
                FunReset()
            End If
            isInsideLoadData = False
        Catch ex As Exception
            isNewEntry = True
            isInsideLoadData = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btngo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngo.Click
        Try
            btngo.Focus()
            btngo.Select()
            If clsCommon.CompairString(clsCommon.myCstr(cbodiff.SelectedValue), "") = CompairStringResult.Equal AndAlso isInsideLoadData = False AndAlso Not chkMannual.Checked Then
                RadPageView1.SelectedPage = RadPageViewPage1
                cbodiff.Select()
                Throw New Exception("Select first interval type.")
            End If
            '=====================get columns from grid selection--------------------
            Dim columns As String = ""

            Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='QCLOGSHEET'")

            If check > 0 Then
                clsDBFuncationality.ExecuteNonQuery("drop table QCLOGSHEET")
            End If
            clsDBFuncationality.ExecuteNonQuery("Create table QCLOGSHEET(SeqNo float,Code varchar(50))")

            For Each grow As GridViewRowInfo In gv_Param.Rows
                If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(colstatus).Value), True) = CompairStringResult.Equal Then
                    clsDBFuncationality.ExecuteNonQuery("Insert into QCLOGSHEET select '" + clsCommon.myCstr(grow.Cells(colSeqno).Value) + "' as SeqNo,'" + clsCommon.myCstr(grow.Cells(colParamcode).Value) + "' as Code")
                End If
            Next

            check = clsDBFuncationality.getSingleValue("select count(*) from QCLOGSHEET")


            If check > 0 Then
                columns = clsDBFuncationality.getSingleValue("select distinct (select ',['+Description+']' from TSPL_QC_LOG_SHEET_MASTER left outer join QCLOGSHEET on QCLOGSHEET.code=TSPL_QC_LOG_SHEET_MASTER.code where TSPL_QC_LOG_SHEET_MASTER.Code in (select Code from qclogsheet) and trans_id='PRODUCTION' order by QCLOGSHEET.SeqNo for xml path('')) as code")
            ElseIf Not isInsideLoadData Then
                RadPageView1.SelectedPage = RadPageViewPage3
                clsCommon.MyMessageBoxShow(Me, "Select atleast one parameter option.", Me.Text)
                Exit Sub
            End If



            clsDBFuncationality.ExecuteNonQuery("drop table QCLOGSHEET")
            If clsCommon.myLen(columns) <= 0 AndAlso Not isInsideLoadData Then
                RadPageView1.SelectedPage = RadPageViewPage3
                clsCommon.MyMessageBoxShow(Me, "Select atleast one parameter option.", Me.Text)
                Exit Sub
            End If


            '===========================================================================
            FillTime(columns)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FillTime(ByVal Columns_name As String)
        Try
            LoadBlankGrid(Columns_name)
            If chkMannual.Checked Then
                Exit Sub
            End If

            If gv.DataSource IsNot Nothing Then
                Dim xtime As Date = Nothing
                Dim oldvalue As Date = Nothing
                Dim xstarttime As Date = clsCommon.myCDate(txtstart_time.Text)
                Dim xendtime As Date = clsCommon.myCDate(txtend_time.Text)
                oldvalue = clsCommon.myCDate(txtstart_time.Text)

                '-----------fill very first start time in grid---------------------
                gv.Rows.AddNew()
                gv.Rows(gv.Rows.Count - 1).Cells(0).Value = CInt(gv.Rows.Count - 1)
                gv.Rows(gv.Rows.Count - 1).Cells(1).Value = clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.myCDate(txtstart_time.Text), "dd/MM/yyyy hh:mm tt"))
                '------------------------------------------------------------------
                If Not xendtime > xstarttime Then
                    Throw New Exception("Start time and End time should be in selected day (" + clsCommon.myCstr(dtpDate.Text) + ") hours.")
                End If
                While (xendtime > xstarttime) 'clsCommon.myCDate(txtend_time.Text) > clsCommon.myCDate(txtstart_time.Text)
                    If clsCommon.CompairString(cbodiff.SelectedValue, "Sec") = CompairStringResult.Equal Then
                        xtime = clsCommon.myCDate(txtstart_time.Text).AddSeconds(clsCommon.myCdbl(txtdiff.Text))
                    ElseIf clsCommon.CompairString(cbodiff.SelectedValue, "Min") = CompairStringResult.Equal Then
                        xtime = clsCommon.myCDate(txtstart_time.Text).AddMinutes(clsCommon.myCdbl(txtdiff.Text))
                    ElseIf clsCommon.CompairString(cbodiff.SelectedValue, "Hour") = CompairStringResult.Equal Then
                        xtime = clsCommon.myCDate(txtstart_time.Text).AddHours(clsCommon.myCdbl(txtdiff.Text))
                    End If

                    If clsCommon.myCDate(xtime) > clsCommon.myCDate(txtend_time.Text) Then
                        xtime = txtend_time.Text
                    End If
                    txtstart_time.Text = xtime
                    xstarttime = clsCommon.myCDate(txtstart_time.Text)
                    gv.Rows.AddNew()
                    gv.Rows(gv.Rows.Count - 1).Cells(0).Value = CInt(gv.Rows.Count - 1)
                    gv.Rows(gv.Rows.Count - 1).Cells(1).Value = clsCommon.myCstr(clsCommon.GetPrintDate(xtime, "dd/MM/yyyy hh:mm tt"))
                End While

                txtstart_time.Text = oldvalue
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select count(*) from TSPL_PP_LOG_SHEET_HEAD where doc_no='" + clsCommon.myCstr(txtCode.Value) + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly Or isButtonClicked Then
            txtCode.Value = clsProcessProductionLogSheet.GetFinder("", txtCode.Value, isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                FunReset()
            End If
        Else
            FunReset()
        End If

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub txtcategorycode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcategorycode._MYValidating
        Try
            Dim qry As String = "select Structure_Code as Code,Structure_Descq as Description,Item_Structure as Structure,Total_Length as [Length],Default_Struct as [Default Structure] from TSPL_STRUCTURE_MASTER "
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("CATFND1", qry)
            If dr IsNot Nothing Then
                txtcategorycode.Value = clsCommon.myCstr(dr("code"))
                txtcategoryname.Text = clsCommon.myCstr(dr("Description"))
            Else
                txtcategorycode.Value = ""
                txtcategoryname.Text = ""
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtstagecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtstagecode._MYValidating
        Dim qry As String = "select Stage_Code as Code,Description from TSPL_STAGE_MASTER "
        txtstagecode.Value = clsCommon.ShowSelectForm("STGFND", qry, "Code", " ", txtstagecode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtstagecode.Value) > 0 Then
            txtstagename.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_STAGE_MASTER where Stage_Code='" + txtstagecode.Value + "' "))
        Else
            txtstagename.Text = ""
        End If
    End Sub

    Private Sub gv_Param_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_Param.CellValueChanged
        Try
            Dim oldseq As Decimal = 0
            Dim seqno As Decimal = 0

            If Not isInsideLoadData Then
                If Not isCellvaluechanged Then
                    isCellvaluechanged = True
                    For ii As Integer = 0 To gv_Param.Rows.Count - 1
                        seqno = clsCommon.myCdbl(gv_Param.Rows(ii).Cells(colSeqno).Value)

                        If seqno > 0 Then
                            For jj As Integer = ii + 1 To gv_Param.Rows.Count - 1
                                oldseq = clsCommon.myCdbl(gv_Param.Rows(jj).Cells(colSeqno).Value)

                                If seqno = oldseq Then
                                    Throw New Exception("Duplicate sequence no. is not allowed")
                                End If
                            Next
                        End If
                    Next
                    isCellvaluechanged = False
                End If
            End If
        Catch ex As Exception
            isCellvaluechanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkMannual_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMannual.ToggleStateChanged
        If Not chkMannual.Checked Then
            txtstart_time.Enabled = True
            txtend_time.Enabled = True
            txtdiff.Enabled = True
            cbodiff.Enabled = True
            btngo.Enabled = True
            LoadBlankGrid(Nothing)
        Else
            txtstart_time.Enabled = False
            txtend_time.Enabled = False
            txtdiff.Enabled = False
            cbodiff.Enabled = False
            btngo.Enabled = True
            LoadBlankGrid(Nothing)
        End If
    End Sub

    Private Sub gv_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv.CellFormatting
        Try
            If gv.Columns IsNot Nothing AndAlso gv.Columns Is gv.Columns(1) Then
                If chkMannual.Checked Then
                    gv.Columns(1).ReadOnly = False
                Else
                    gv.Columns(1).ReadOnly = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If Not chkMannual.Checked Then
            Exit Sub
        End If
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(0).Value = clsCommon.myCdbl(intCurrRow)
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub
End Class
