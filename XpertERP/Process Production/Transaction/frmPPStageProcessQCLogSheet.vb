'-----------Panch Raj----02/09/2014--------------
Imports common
Imports System.Data.SqlClient



Public Class frmPPStageProcessQCLogSheet
    Inherits FrmMainTranScreen

#Region "Variables"
    Public isInsideLoadData As Boolean = False
    Public isCellValueChanged As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim Errorcontrol As New clsErrorControl()
    Public Log_Sheet_No As String = ""
    Public Stage_Code As String = ""
    Public Stage_Desc As String = ""
    Public ProductionCategoryCode As String = ""
    Public ProductionCategoryDesc As String = ""
    Public Sequence As Integer
    Public Stage_Type As String
    Public STAGE_PROCESS_CODE As String = ""
    Public Standardization_Code As String = ""
    Public PRODUCTION_ENTRY_CODE As String = ""
    Public Batch_Code As String = ""
    Dim dt As DataTable
    '' grid columns 
    'Const "S.No." As String = ""S.No.""
    'Const colStage_Code As String = "colStage_Code"
    'Const colStage_Desc As String = "colStage_Desc"
    'Const colLog_Sheet_No As String = "colcolLog_Sheet_No"
    'Const colParameterCode As String = "colParameterCode"
    'Const colParameterDesc As String = "colParameterDesc"
    'Const colStandardValue As String = "colStandardValue"
    'Const colActualValue As String = "colActualValue"
    'Const "Time" As String = ""Time""
    'Const colBatch_Code As String = "colBatch_Code"
    'Const colQCLM_Code As String = "colQCLM_Code"
    Public IsCancelled As Boolean = False

    Public objListSP As List(Of clsPPStageProcessLogSheetDetail) = New List(Of clsPPStageProcessLogSheetDetail)
    Public objListSTD As List(Of clsPPSTDLogSheetDetail) = New List(Of clsPPSTDLogSheetDetail)
    Public objListPE As List(Of clsPPPELogSheetDetail) = New List(Of clsPPPELogSheetDetail)
    Public objListUsers As List(Of clsSectionStageMapping_User) = New List(Of clsSectionStageMapping_User)
    Public arrXtime As List(Of String) = New List(Of String)
#End Region

    Private Sub frmPPStageProcessQCLogSheet_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            If clsCommon.CompairString(Stage_Type, "SP") = CompairStringResult.Equal Then
                If AllowToSave() Then GetQCDataSP()
                Me.Close()
            ElseIf clsCommon.CompairString(Stage_Type, "STD") = CompairStringResult.Equal Then
                If AllowToSave() Then GetQCDataSTD()
                Me.Close()
            ElseIf clsCommon.CompairString(Stage_Type, "PE") = CompairStringResult.Equal Then
                If AllowToSave() Then GetQCDataPE()
                Me.Close()
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If

        '--------------BM00000004866
        If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn.Index > 1 And isCellValueChanged = False Then
            isCellValueChanged = True
            Dim obj As clsPPLogSheetMaster = clsPPLogSheetMaster.GetData(dt.Rows(gv.CurrentColumn.Index - 2).Item("Parameter_Code"), NavigatorType.Current)
            If Not obj Is Nothing Then
                If clsCommon.CompairString(obj.nature, "A") = CompairStringResult.Equal Then
                    gv.Rows(gv.CurrentRow.Index).Cells(gv.CurrentColumn.Index).Value = clsParameterValueMaster.GetFinder("", clsCommon.myCstr(gv.Rows(gv.CurrentRow.Index).Cells(gv.CurrentColumn.Index).Value), False)

                ElseIf clsCommon.CompairString(obj.nature, "B") = CompairStringResult.Equal Then
                    gv.Rows(gv.CurrentRow.Index).Cells(gv.CurrentColumn.Index).Value = FinderForBoolean("", clsCommon.myCstr(gv.Rows(gv.CurrentRow.Index).Cells(gv.CurrentColumn.Index).Value), False)
                End If
            End If
            isCellValueChanged = False
        End If
    End Sub

    Private Sub FunReset()

        isNewEntry = True
        RadPageView1.Pages(1).Item.Visibility = ElementVisibility.Collapsed
        txtCode.Value = Log_Sheet_No
        dtpDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtcategorycode.Value = ProductionCategoryCode
        txtcategoryname.Text = ProductionCategoryDesc
        txtstagecode.Value = Stage_Code
        txtstagename.Text = Stage_Desc
        txtsequnce.Text = Sequence
        CreateColumns()

        LoadData()
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()

        btnsave.Enabled = True
        'btndelete.Enabled = False
        'btnsave.Text = "Save"
        txtCode.MyReadOnly = False
        RadPageView1.SelectedPage = RadPageViewPage1
        txtcategorycode.Focus()
        txtcategorycode.Select()
    End Sub

    Private Sub SetUserMgmtNew()
        'Dim Allowed As Boolean = False
        'For Each objTrUser As clsSectionStageMapping_User In objListUsers
        '    If clsCommon.CompairString(objTrUser.usercode, objCommonVar.CurrentUserCode) = CompairStringResult.Equal Then
        '        Allowed = True
        '        Exit For
        '    End If
        'Next
        'If Allowed = False Then
        '    IsCancelled = True
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPPStageProcessQCLogSheet)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
        'btnsave.Visible = True

    End Sub
    Sub LoadData()
        isInsideLoadData = True
        Me.txtCode.Value = Log_Sheet_No
        Me.txtcategorycode.Value = ProductionCategoryCode
        Me.txtcategoryname.Text = ProductionCategoryDesc

        Me.txtstagecode.Value = Stage_Code
        Me.txtstagename.Text = Stage_Desc
        Me.txtsequnce.Text = Sequence

        If clsCommon.CompairString(Stage_Type, "SP") = CompairStringResult.Equal Then
            If objListSP Is Nothing Then
                objListSP = clsPPStageProcessLogSheetDetail.GetPPStageProcessQCLogSheetDetail(STAGE_PROCESS_CODE, Stage_Code, Log_Sheet_No, Nothing)
            End If
            If objListSP IsNot Nothing AndAlso objListSP.Count > 0 Then
                'gv.RowCount = objListSTD.Count
                For Each objTr As clsPPStageProcessLogSheetDetail In objListSP
                    For Each grow As GridViewRowInfo In gv.Rows
                        For Each gcol As GridViewColumn In gv.Columns
                            If gcol.Index < 2 Then
                                Continue For
                            End If
                            If clsCommon.CompairString(grow.Cells("Time").Value, objTr.xtime) = CompairStringResult.Equal And clsCommon.CompairString(dt.Rows(gcol.Index - 2).Item("Parameter_Code"), objTr.QCLM_CODE) = CompairStringResult.Equal Then
                                If dt.Rows(gcol.Index - 2).Item("Pick_Batch_No") = 0 Then
                                    grow.Cells(gcol.Name).Value = objTr.Parameter_ACT_Value
                                Else
                                    grow.Cells(gcol.Name).Value = Batch_Code
                                End If

                            End If
                        Next
                    Next
                    'gv.Rows.AddNew()
                    'gv.Rows(gv.Rows.Count - 1).Cells("S.No.").Value = objTr.Sno
                    'gv.Rows(gv.Rows.Count - 1).Cells(colLog_Sheet_No).Value = objTr.Log_Sheet_No
                    'gv.Rows(gv.Rows.Count - 1).Cells(colParameterCode).Value = objTr.param_code
                    'gv.Rows(gv.Rows.Count - 1).Cells(colParameterDesc).Value = objTr.Parameter_Desc
                    'gv.Rows(gv.Rows.Count - 1).Cells(colQCLM_Code).Value = objTr.QCLM_CODE
                    'gv.Rows(gv.Rows.Count - 1).Cells(colStage_Code).Value = objTr.Stage_Code
                    'gv.Rows(gv.Rows.Count - 1).Cells(colStandardValue).Value = objTr.Parameter_STD_Value
                    'gv.Rows(gv.Rows.Count - 1).Cells(colActualValue).Value = objTr.Parameter_ACT_Value
                    'gv.Rows(gv.Rows.Count - 1).Cells("Time").Value = objTr.xtime
                    'gv.Rows(gv.Rows.Count - 1).Cells(colBatch_Code).Value = Batch_Code
                Next
            End If
        ElseIf clsCommon.CompairString(Stage_Type, "STD") = CompairStringResult.Equal Then
            If objListSTD Is Nothing Then
                objListSTD = clsPPSTDLogSheetDetail.GetPPSTDQCLogSheetDetail(Standardization_Code, Stage_Code, Log_Sheet_No, Nothing)
            End If
            If objListSTD IsNot Nothing AndAlso objListSTD.Count > 0 Then
                'gv.RowCount = objListSTD.Count
                For Each objTr As clsPPSTDLogSheetDetail In objListSTD
                    For Each grow As GridViewRowInfo In gv.Rows
                        For Each gcol As GridViewColumn In gv.Columns
                            If gcol.Index < 2 Then
                                Continue For
                            End If
                            If clsCommon.CompairString(grow.Cells("Time").Value, objTr.xtime) = CompairStringResult.Equal And clsCommon.CompairString(dt.Rows(gcol.Index - 2).Item("Parameter_Code"), objTr.QCLM_CODE) = CompairStringResult.Equal Then
                                If dt.Rows(gcol.Index - 2).Item("Pick_Batch_No") = 0 Then
                                    grow.Cells(gcol.Name).Value = objTr.Parameter_ACT_Value
                                Else
                                    grow.Cells(gcol.Name).Value = Batch_Code
                                End If
                            End If
                        Next
                    Next
                    'gv.Rows.AddNew()
                    'gv.Rows(gv.Rows.Count - 1).Cells("S.No.").Value = objTr.Sno
                    'gv.Rows(gv.Rows.Count - 1).Cells(colLog_Sheet_No).Value = objTr.Log_Sheet_No
                    'gv.Rows(gv.Rows.Count - 1).Cells(colParameterCode).Value = objTr.param_code
                    'gv.Rows(gv.Rows.Count - 1).Cells(colQCLM_Code).Value = objTr.QCLM_CODE
                    'gv.Rows(gv.Rows.Count - 1).Cells(colParameterDesc).Value = objTr.Parameter_Desc
                    'gv.Rows(gv.Rows.Count - 1).Cells(colStage_Code).Value = objTr.Stage_Code
                    'gv.Rows(gv.Rows.Count - 1).Cells(colStandardValue).Value = objTr.Parameter_STD_Value
                    'gv.Rows(gv.Rows.Count - 1).Cells(colActualValue).Value = objTr.Parameter_ACT_Value
                    'gv.Rows(gv.Rows.Count - 1).Cells("Time").Value = objTr.xtime
                    'gv.Rows(gv.Rows.Count - 1).Cells(colBatch_Code).Value = Batch_Code
                Next
            End If
        ElseIf clsCommon.CompairString(Stage_Type, "PE") = CompairStringResult.Equal Then
            If objListPE Is Nothing Then
                objListPE = clsPPPELogSheetDetail.GetPPPEQCLogSheetDetail(PRODUCTION_ENTRY_CODE, Stage_Code, Log_Sheet_No, Nothing)
            End If
            If objListPE IsNot Nothing AndAlso objListPE.Count > 0 Then
                For Each objTr As clsPPPELogSheetDetail In objListPE
                    For Each grow As GridViewRowInfo In gv.Rows
                        For Each gcol As GridViewColumn In gv.Columns
                            If gcol.Index < 2 Then
                                Continue For
                            End If
                            If clsCommon.CompairString(grow.Cells("Time").Value, objTr.xtime) = CompairStringResult.Equal And clsCommon.CompairString(dt.Rows(gcol.Index - 2).Item("Parameter_Code"), objTr.QCLM_CODE) = CompairStringResult.Equal Then
                                If dt.Rows(gcol.Index - 2).Item("Pick_Batch_No") = 0 Then
                                    grow.Cells(gcol.Name).Value = objTr.Parameter_ACT_Value
                                Else
                                    grow.Cells(gcol.Name).Value = Batch_Code
                                End If

                            End If
                        Next
                    Next

                Next
            End If
        End If
        isInsideLoadData = False
    End Sub

    Private Sub LoadBlankGrid1()
        'Try

        '    gv.Rows.Clear()
        '    gv.Columns.Clear()

        '    Dim repoARSno As GridViewDecimalColumn = New GridViewDecimalColumn()
        '    repoARSno.FormatString = ""
        '    repoARSno.Name = "S.No."
        '    repoARSno.Width = 60
        '    repoARSno.DecimalPlaces = 0
        '    repoARSno.HeaderText = "S.No."
        '    repoARSno.ReadOnly = True
        '    gv.MasterTemplate.Columns.Add(repoARSno)

        '    Dim StageCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        '    StageCode.FormatString = ""
        '    StageCode.Name = colStage_Code
        '    StageCode.Width = 100
        '    StageCode.HeaderText = "Stage Code"
        '    StageCode.ReadOnly = True
        '    gv.MasterTemplate.Columns.Add(StageCode)

        '    Dim StageDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        '    StageDesc.FormatString = ""
        '    StageDesc.Name = colStage_Desc
        '    StageDesc.Width = 100
        '    StageDesc.HeaderText = "Stage Description"
        '    StageDesc.ReadOnly = True
        '    StageDesc.IsVisible = False
        '    gv.MasterTemplate.Columns.Add(StageDesc)

        '    Dim Log_Sheet_No As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        '    Log_Sheet_No.FormatString = ""
        '    Log_Sheet_No.Name = colLog_Sheet_No
        '    Log_Sheet_No.Width = 120
        '    Log_Sheet_No.HeaderText = "Log Sheet No"
        '    Log_Sheet_No.ReadOnly = True
        '    gv.MasterTemplate.Columns.Add(Log_Sheet_No)

        '    Dim XTime As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        '    XTime.FormatString = ""
        '    XTime.Name = "Time"
        '    XTime.Width = 120
        '    XTime.HeaderText = "Time"
        '    XTime.ReadOnly = True
        '    gv.MasterTemplate.Columns.Add(XTime)


        '    Dim ParameterCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        '    ParameterCode.FormatString = ""
        '    ParameterCode.Name = colParameterCode
        '    ParameterCode.Width = 120
        '    ParameterCode.HeaderText = "Parameter Code"
        '    ParameterCode.ReadOnly = True
        '    gv.MasterTemplate.Columns.Add(ParameterCode)

        '    Dim ParameterDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        '    ParameterDesc.FormatString = ""
        '    ParameterDesc.Name = colParameterDesc
        '    ParameterDesc.Width = 120
        '    ParameterDesc.HeaderText = "Parameter Description"
        '    ParameterDesc.ReadOnly = True
        '    gv.MasterTemplate.Columns.Add(ParameterDesc)


        '    Dim stdValue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        '    stdValue.FormatString = ""
        '    stdValue.Name = colStandardValue
        '    stdValue.Width = 120
        '    stdValue.HeaderText = "Standard Value"
        '    stdValue.ReadOnly = True
        '    gv.MasterTemplate.Columns.Add(stdValue)

        '    Dim ActValue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        '    ActValue.FormatString = ""
        '    ActValue.Name = colActualValue
        '    ActValue.Width = 120
        '    ActValue.HeaderText = "Actual Value"
        '    ActValue.ReadOnly = False
        '    gv.MasterTemplate.Columns.Add(ActValue)

        '    Dim QCLM_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        '    QCLM_Code.FormatString = ""
        '    QCLM_Code.Name = colQCLM_Code
        '    QCLM_Code.Width = 100
        '    QCLM_Code.HeaderText = "QCLM Code"
        '    QCLM_Code.ReadOnly = True
        '    gv.MasterTemplate.Columns.Add(QCLM_Code)

        '    Dim Batch_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        '    Batch_Code.FormatString = ""
        '    Batch_Code.Name = colBatch_Code
        '    Batch_Code.Width = 100
        '    Batch_Code.HeaderText = "Batch Code"
        '    Batch_Code.ReadOnly = True
        '    gv.MasterTemplate.Columns.Add(Batch_Code)


        '    gv.AllowDeleteRow = True
        '    gv.AllowAddNewRow = False
        '    gv.ShowGroupPanel = False
        '    gv.AllowColumnReorder = False
        '    gv.AllowRowReorder = False
        '    gv.EnableSorting = False
        '    gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        '    gv.MasterTemplate.ShowRowHeaderColumn = False

        '    'gv.AutoSize = True
        '    gv.AllowDeleteRow = False
        '    gv.AllowAddNewRow = False
        '    gv.ShowGroupPanel = False
        '    gv.AllowColumnReorder = False
        '    gv.AllowRowReorder = False
        '    gv.EnableSorting = False
        '    gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        '    gv.MasterTemplate.ShowRowHeaderColumn = False

        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub
    Private Sub LoadBlankGrid(ByVal columns As String)
        Try


            gv.DataSource = Nothing

            Dim qry As String = ""
            If clsCommon.myLen(columns) > 0 Then
                columns = columns.Replace(",", ",'' as ")
            End If
           
            'qry = "select 0 as [S.No.],'' as [Time]" + columns + ""

            Dim isManual As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Is_Mannual from TSPL_PP_LOG_SHEET_HEAD where Doc_No='" & Log_Sheet_No & "'"))
            If Not arrXtime Is Nothing AndAlso arrXtime.Count > 0 Then
                For Each item As String In arrXtime
                    If arrXtime.IndexOf(item) = 0 Then
                        qry = "select " & (arrXtime.IndexOf(item) + 1) & " as [S.No.],'" & item & "' as [Time] " + columns + ""
                    Else
                        qry += " Union All " & "select " & (arrXtime.IndexOf(item) + 1) & " as [S.No.],'" & item & "' as [Time] " + columns + ""
                    End If
                Next
            Else
                qry = "select ROW_NUMBER() over (order by [Time]) as [S.No.],[Time] " + columns + "  from  (select distinct case when len(time_value)<=0 then '' when len(time_value)<=10 then time_value else substring(TSPL_PP_LOG_SHEET_DETAIL.Time_Value,11, len( TSPL_PP_LOG_SHEET_DETAIL.Time_Value)-10) end as [Time]" + columns + " from TSPL_PP_LOG_SHEET_DETAIL  " _
                      & " inner join TSPL_PP_LOG_SHEET_HEAD on TSPL_PP_LOG_SHEET_DETAIL.Doc_No=TSPL_PP_LOG_SHEET_HEAD.Doc_No " _
                      & " where TSPL_PP_LOG_SHEET_HEAD.Doc_No='" & Log_Sheet_No & "' and TSPL_PP_LOG_SHEET_HEAD.Stage_Code='" & Stage_Code & "') as tab "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv.DataSource = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = dt
                'gv.Rows.Clear()
                'gv.Rows(0).IsVisible = False

                For Each col As GridViewColumn In gv.Columns
                    col.Width = 150
                Next
                gv.Columns(0).Width = 60
                gv.Columns(0).ReadOnly = True

                gv.Columns(1).Width = 80
                gv.Columns(1).ReadOnly = IIf(isManual = 0, True, False)

                'gv.AutoSize = True
                gv.ReadOnly = False
                gv.AllowDeleteRow = False
                gv.AllowAddNewRow = False
                gv.ShowGroupPanel = False
                gv.AllowColumnReorder = False
                gv.AllowRowReorder = False
                gv.EnableSorting = False
                gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
                gv.MasterTemplate.ShowRowHeaderColumn = False
                'gv.Rows.NewRow()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub frmPPStageProcessQCLogSheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'LoadComboBox()
        FunReset()

        ButtonToolTip.SetToolTip(btnNew, "Alt+N for new window")
        ButtonToolTip.SetToolTip(btnNew, "Alt+S for save data")
        ButtonToolTip.SetToolTip(btnNew, "Alt+D for delete data")
        ButtonToolTip.SetToolTip(btnNew, "Alt+C for close window")
        ButtonToolTip.SetToolTip(btnNew, "Alt+G for fill QC grid")
    End Sub

    Private Function AllowToSave() As Boolean
        Try

            If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
                Return False
            End If
            If clsCommon.myLen(txtcategorycode.Value) <= 0 Then
                txtcategorycode.Focus()
                txtcategorycode.Select()
                Errorcontrol.SetError(txtcategoryname, "Select production category")
                Throw New Exception("Select production category")
            Else
                Errorcontrol.ResetError(txtcategoryname)
            End If

            If clsCommon.myLen(txtstagecode.Value) <= 0 Then
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

            'If CInt(txtsequnce.Text) <= 0 Then
            '    txtsequnce.Focus()
            '    txtsequnce.Select()
            '    Errorcontrol.SetError(txtsequnce, "Fill stage sequence no.")
            '    Throw New Exception("Fill stage sequence no.")
            'Else
            '    Errorcontrol.ResetError(txtsequnce)
            'End If



            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If clsCommon.CompairString(Stage_Type, "SP") = CompairStringResult.Equal Then
            If AllowToSave() Then GetQCDataSP()
            Me.Close()
        ElseIf clsCommon.CompairString(Stage_Type, "STD") = CompairStringResult.Equal Then
            If AllowToSave() Then GetQCDataSTD()
            Me.Close()
        ElseIf clsCommon.CompairString(Stage_Type, "PE") = CompairStringResult.Equal Then
            If AllowToSave() Then GetQCDataPE()
            Me.Close()
        End If

    End Sub

    Public Function GetQCDataSP() As List(Of clsPPStageProcessLogSheetDetail)
        objListSP = New List(Of clsPPStageProcessLogSheetDetail)
        arrXtime = New List(Of String)
        Try

            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.myLen(grow.Cells(1).Value) <= 0 Then
                    Continue For
                End If
                arrXtime.Add(grow.Cells(1).Value)
                For Each gcol As GridViewColumn In gv.Columns
                    If gcol.Index < 2 Then
                        Continue For
                    End If
                    Dim objtr As New clsPPStageProcessLogSheetDetail()
                    objtr.Sno = CInt(grow.Cells("S.No.").Value)
                    objtr.param_code = "" 'clsCommon.myCstr(grow.Cells(colParameterCode).Value)
                    objtr.Fill_Date = Me.dtpDate.Value
                    objtr.Log_Sheet_No = Log_Sheet_No 'clsCommon.myCstr(grow.Cells(colLog_Sheet_No).Value)
                    objtr.Parameter_ACT_Value = clsCommon.myCstr(grow.Cells(gcol.Name).Value) 'clsCommon.myCstr(grow.Cells(colActualValue).Value)
                    objtr.Parameter_STD_Value = 0 ''clsCommon.myCstr(grow.Cells(colStandardValue).Value)
                    objtr.Stage_Code = Stage_Code 'clsCommon.myCstr(grow.Cells(colStage_Code).Value)
                    objtr.STAGE_PROCESS_CODE = STAGE_PROCESS_CODE
                    objtr.xtime = clsCommon.myCstr(grow.Cells("Time").Value)
                    objtr.QCLM_CODE = dt.Rows(gcol.Index - 2).Item("Parameter_Code") 'clsCommon.myCstr(grow.Cells(colQCLM_Code).Value)
                    objtr.Batch_Code = Batch_Code 'clsCommon.myCstr(grow.Cells(colBatch_Code).Value)
                    'If objtr.Sno > 0 AndAlso clsCommon.myLen(objtr.QCLM_CODE) > 0 Then
                    '    objListSP.Add(objtr)
                    'End If
                    objListSP.Add(objtr)
                Next
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return objListSP
    End Function

    Public Function GetQCDataPE() As List(Of clsPPPELogSheetDetail)
        objListPE = New List(Of clsPPPELogSheetDetail)
        arrXtime = New List(Of String)
        Try

            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.myLen(grow.Cells(1).Value) <= 0 Then
                    Continue For
                End If
                arrXtime.Add(grow.Cells(1).Value)
                For Each gcol As GridViewColumn In gv.Columns
                    If gcol.Index < 2 Then
                        Continue For
                    End If
                    Dim objtr As New clsPPPELogSheetDetail()
                    objtr.Sno = CInt(grow.Cells("S.No.").Value)
                    objtr.param_code = "" 'clsCommon.myCstr(grow.Cells(colParameterCode).Value)
                    objtr.Fill_Date = Me.dtpDate.Value
                    objtr.Log_Sheet_No = Log_Sheet_No 'clsCommon.myCstr(grow.Cells(colLog_Sheet_No).Value)
                    objtr.Parameter_ACT_Value = clsCommon.myCstr(grow.Cells(gcol.Name).Value) 'clsCommon.myCstr(grow.Cells(colActualValue).Value)
                    objtr.Parameter_STD_Value = 0 ''clsCommon.myCstr(grow.Cells(colStandardValue).Value)
                    objtr.Stage_Code = Stage_Code 'clsCommon.myCstr(grow.Cells(colStage_Code).Value)
                    objtr.PRODUCTION_ENTRY_CODE = PRODUCTION_ENTRY_CODE
                    objtr.xtime = clsCommon.myCstr(grow.Cells("Time").Value)
                    objtr.QCLM_CODE = dt.Rows(gcol.Index - 2).Item("Parameter_Code") 'clsCommon.myCstr(grow.Cells(colQCLM_Code).Value)
                    objtr.Batch_Code = Batch_Code 'clsCommon.myCstr(grow.Cells(colBatch_Code).Value)
                    'If objtr.Sno > 0 AndAlso clsCommon.myLen(objtr.QCLM_CODE) > 0 Then
                    '    objListSP.Add(objtr)
                    'End If
                    objListPE.Add(objtr)
                Next
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return objListPE
    End Function

    Public Function GetQCDataSTD() As List(Of clsPPSTDLogSheetDetail)
        objListSTD = New List(Of clsPPSTDLogSheetDetail)
        arrXtime = New List(Of String)
        Try

            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.myLen(grow.Cells(1).Value) <= 0 Then
                    Continue For
                End If
                arrXtime.Add(grow.Cells(1).Value)
                For Each gcol As GridViewColumn In gv.Columns
                    If gcol.Index < 2 Then
                        Continue For
                    End If
                    Dim objtr As New clsPPSTDLogSheetDetail()
                    objtr.Sno = CInt(grow.Cells("S.No.").Value)
                    objtr.param_code = "" ' clsCommon.myCstr(grow.Cells(colParameterCode).Value)
                    objtr.Fill_Date = Me.dtpDate.Value
                    objtr.Log_Sheet_No = Log_Sheet_No 'clsCommon.myCstr(grow.Cells(colLog_Sheet_No).Value)
                    objtr.Parameter_ACT_Value = clsCommon.myCstr(grow.Cells(gcol.Name).Value) 'clsCommon.myCstr(grow.Cells(colActualValue).Value)
                    objtr.Parameter_STD_Value = 0 'clsCommon.myCstr(grow.Cells(colStandardValue).Value)
                    objtr.Stage_Code = Stage_Code 'clsCommon.myCstr(grow.Cells(colStage_Code).Value)
                    objtr.Standardization_Code = Standardization_Code
                    objtr.xtime = clsCommon.myCstr(grow.Cells("Time").Value)

                    objtr.QCLM_CODE = dt.Rows(gcol.Index - 2).Item("Parameter_Code") 'clsCommon.myCstr(grow.Cells(colQCLM_Code).Value)
                    objtr.Batch_Code = Batch_Code 'clsCommon.myCstr(grow.Cells(colBatch_Code).Value)
                    'If objtr.Sno > 0 AndAlso clsCommon.myLen(objtr.QCLM_CODE) > 0 Then
                    '    objListSTD.Add(objtr)
                    'End If
                    objListSTD.Add(objtr)
                Next

            Next



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return objListSTD
    End Function

    
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        IsCancelled = True
        Me.Close()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub
    Sub CreateColumns()
        '=====================get columns from grid selection--------------------
        isInsideLoadData = True
        Dim columns As String = ""

        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='QCLOGSHEET'")

        If check > 0 Then
            clsDBFuncationality.ExecuteNonQuery("drop table QCLOGSHEET")
        End If
        clsDBFuncationality.ExecuteNonQuery("Create table QCLOGSHEET(SeqNo float,Code varchar(50))")

        Dim qry As String = " select distinct TSPL_PP_LOG_SHEET_DETAIL.SNO,TSPL_PP_LOG_SHEET_DETAIL.Parameter_Code,TSPL_QC_LOG_SHEET_MASTER.Pick_Batch_No from TSPL_PP_LOG_SHEET_DETAIL  " _
                            & " inner join TSPL_PP_LOG_SHEET_HEAD on TSPL_PP_LOG_SHEET_DETAIL.Doc_No=TSPL_PP_LOG_SHEET_HEAD.Doc_No " _
                            & " left join TSPL_QC_LOG_SHEET_MASTER on TSPL_PP_LOG_SHEET_DETAIL.Parameter_Code=TSPL_QC_LOG_SHEET_MASTER.Code " _
                            & " where TSPL_PP_LOG_SHEET_HEAD.Doc_No='" & Log_Sheet_No & "' and TSPL_PP_LOG_SHEET_HEAD.Stage_Code='" & Stage_Code & "'"

        dt = clsDBFuncationality.GetDataTable(qry)
        For Each grow As DataRow In dt.Rows
            clsDBFuncationality.ExecuteNonQuery("Insert into QCLOGSHEET select '" + clsCommon.myCstr(grow.Item("SNO")) + "' as SeqNo,'" + clsCommon.myCstr(grow.Item("Parameter_Code")) + "' as Code")
        Next

        check = clsDBFuncationality.getSingleValue("select count(*) from QCLOGSHEET")


        If check > 0 Then
            qry = "select distinct (select ',['+Description+']' from TSPL_QC_LOG_SHEET_MASTER left outer join QCLOGSHEET on QCLOGSHEET.code=TSPL_QC_LOG_SHEET_MASTER.code where TSPL_QC_LOG_SHEET_MASTER.Code in (select Code from qclogsheet) and trans_id='PRODUCTION' order by QCLOGSHEET.SeqNo for xml path('')) as code"
            columns = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        ElseIf Not isInsideLoadData Then
            clsCommon.MyMessageBoxShow(Me, "No Parameters.", Me.Text)
            Exit Sub

        End If



        clsDBFuncationality.ExecuteNonQuery("drop table QCLOGSHEET")
        If clsCommon.myLen(columns) <= 0 AndAlso Not isInsideLoadData Then
            clsCommon.MyMessageBoxShow(Me, "No Parameters.", Me.Text)
            Exit Sub
        End If
        LoadBlankGrid(columns)
        isInsideLoadData = False
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isInsideLoadData Then
            If e.Column.Index > 1 And isCellValueChanged = False Then
                isCellValueChanged = True
                Dim obj As clsPPLogSheetMaster = clsPPLogSheetMaster.GetData(dt.Rows(e.Column.Index - 2).Item("Parameter_Code"), NavigatorType.Current)
                If Not obj Is Nothing Then
                    If clsCommon.CompairString(obj.nature, "A") = CompairStringResult.Equal Then
                        gv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = clsParameterValueMaster.GetFinder("", clsCommon.myCstr(gv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value), False)

                    ElseIf clsCommon.CompairString(obj.nature, "B") = CompairStringResult.Equal Then
                        gv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = FinderForBoolean("", clsCommon.myCstr(gv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value), False)

                    End If
                End If
                isCellValueChanged = False
            End If

        End If

    End Sub
    Function FinderForBoolean(ByVal whrCls As String, ByVal CurrCode As String, ByVal isButtonClicked As Boolean) As String
        Dim strcode As String = ""
        Dim qry As String = "select 'Yes' as Code union all select 'No' as Code"
        strcode = clsCommon.ShowSelectForm("PMTFND", qry, "Code", "", CurrCode, "Code", isButtonClicked)

        Return strcode
    End Function

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(0).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gv.Rows.Count - 1 And gv.Columns(1).ReadOnly = False Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub
End Class
