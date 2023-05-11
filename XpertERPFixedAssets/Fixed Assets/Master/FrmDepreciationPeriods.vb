Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports common
Imports System.IO
Imports XpertERPEngine

Public Class FrmDepreciationPeriods
    Inherits FrmMainTranScreen

#Region "variables"
    Dim Period1 As String = Nothing
    Dim PeriodRun1 As String = Nothing
    Dim Period2 As String = Nothing
    Dim PeriodRun2 As String = Nothing
    Dim Period3 As String = Nothing
    Dim PeriodRun3 As String = Nothing
    Dim Period4 As String = Nothing
    Dim PeriodRun4 As String = Nothing
    Dim Period5 As String = Nothing
    Dim PeriodRun5 As String = Nothing
    Dim Period6 As String = Nothing
    Dim PeriodRun6 As String = Nothing
    Dim Period7 As String = Nothing
    Dim PeriodRun7 As String = Nothing
    Dim Period8 As String = Nothing
    Dim PeriodRun8 As String = Nothing
    Dim Period9 As String = Nothing
    Dim PeriodRun9 As String = Nothing
    Dim Period10 As String = Nothing
    Dim PeriodRun10 As String = Nothing
    Dim Period11 As String = Nothing
    Dim PeriodRun11 As String = Nothing
    Dim Period12 As String = Nothing
    Dim PeriodRun12 As String = Nothing
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colfiscalperiod As String = "COLperiod"
    Const colfiscalperiodName As String = "colfiscalperiodName"
    Const colfiscalrun As String = "COLrum"
    Const COLFiscalRunPerm As String = "COLFiscalRunPerm"
#End Region




    Private Sub FrmDepAccountSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        loadgrid()
        SetLength()
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S Save/Update")
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim fiscalperiod As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        fiscalperiod.FormatString = ""
        fiscalperiod.HeaderText = "Period"
        fiscalperiod.Name = colfiscalperiod

        fiscalperiod.Width = 150
        fiscalperiod.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(fiscalperiod)

        Dim periodName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        periodName.FormatString = ""
        periodName.HeaderText = "Period Name"
        periodName.Name = colfiscalperiodName
        periodName.Width = 150
        periodName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(periodName)

        Dim fiscalrun As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        fiscalrun.FormatString = ""
        fiscalrun.HeaderText = "Run Depreciation"
        fiscalrun.Name = colfiscalrun
        fiscalrun.Width = 150
        fiscalrun.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(fiscalrun)

        Dim fiscalrunPerm As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        fiscalrunPerm.FormatString = ""
        fiscalrunPerm.HeaderText = "Run Permanent"
        fiscalrunPerm.Name = COLFiscalRunPerm
        fiscalrunPerm.Width = 150
        fiscalrunPerm.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(fiscalrunPerm)

        gv1.AllowAddNewRow = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub
    Private Sub loadgrid()
        Try
            SetUserMgmtNew()
            gv1.Rows.Clear()
            ' gv1.Columns.Clear()
            For ii As Integer = 0 To 11
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colfiscalperiod).Value = ii + 1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colfiscalperiodName).Value = MonthName(ii + 1)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colfiscalrun).Value = "NO"
                gv1.Rows(gv1.Rows.Count - 1).Cells(COLFiscalRunPerm).Value = "NO"
            Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.DepPeriod)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag

        btndelete.Visible = MyBase.isDeleteFlag
    End Sub



    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating



        Try
            Dim Qry As String = "select count(*)  from TSPL_DEPRECIATION_PERIODS where period_Code='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            If txtDocNo.MyReadOnly OrElse isButtonClicked Then
                Qry = "Select period_Code as [Code], period_Desc as [Description] from TSPL_DEPRECIATION_PERIODS "
                txtDocNo.Value = clsCommon.ShowSelectForm("DepreciationPeriod", Qry, "Code", "", txtDocNo.Value, "", isButtonClicked)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_DEPRECIATION_PERIODS where period_Code='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Function AllowToSave() As Boolean

        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Account Set Code can't be blank")
                txtDocNo.Focus()
                Return False
            End If
        End If

        If clsCommon.myLen(txtDesc.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Account Set Description can't be blank")
            txtDesc.Focus()
            Return False
        End If


        Return True
    End Function

    Public Sub SaveData()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.DepPeriod, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsDepreciationPeriods()
                obj.period_Code = txtDocNo.Value
                obj.period_Desc = txtDesc.Text

                If chkinactive.Checked = True Then
                    obj.Inactive = "1"
                ElseIf chkinactive.Checked = False Then
                    obj.Inactive = "0"
                End If

                obj.FiscalPeriod1 = clsCommon.myCstr(gv1.Rows(0).Cells(colfiscalperiod).Value)
                obj.FiscalPeriodRun1 = clsCommon.myCstr(gv1.Rows(0).Cells(colfiscalrun).Value)
                obj.FiscalPeriodRunPerm1 = clsCommon.myCstr(gv1.Rows(0).Cells(COLFiscalRunPerm).Value)

                obj.FiscalPeriod2 = clsCommon.myCstr(gv1.Rows(1).Cells(colfiscalperiod).Value)
                obj.FiscalPeriodRun2 = clsCommon.myCstr(gv1.Rows(1).Cells(colfiscalrun).Value)
                obj.FiscalPeriodRunPerm2 = clsCommon.myCstr(gv1.Rows(1).Cells(COLFiscalRunPerm).Value)

                obj.FiscalPeriod3 = clsCommon.myCstr(gv1.Rows(2).Cells(colfiscalperiod).Value)
                obj.FiscalPeriodRun3 = clsCommon.myCstr(gv1.Rows(2).Cells(colfiscalrun).Value)
                obj.FiscalPeriodRunPerm3 = clsCommon.myCstr(gv1.Rows(2).Cells(COLFiscalRunPerm).Value)

                obj.FiscalPeriod4 = clsCommon.myCstr(gv1.Rows(3).Cells(colfiscalperiod).Value)
                obj.FiscalPeriodRun4 = clsCommon.myCstr(gv1.Rows(3).Cells(colfiscalrun).Value)
                obj.FiscalPeriodRunPerm4 = clsCommon.myCstr(gv1.Rows(3).Cells(COLFiscalRunPerm).Value)

                obj.FiscalPeriod5 = clsCommon.myCstr(gv1.Rows(4).Cells(colfiscalperiod).Value)
                obj.FiscalPeriodRun5 = clsCommon.myCstr(gv1.Rows(4).Cells(colfiscalrun).Value)
                obj.FiscalPeriodRunPerm5 = clsCommon.myCstr(gv1.Rows(4).Cells(COLFiscalRunPerm).Value)

                obj.FiscalPeriod6 = clsCommon.myCstr(gv1.Rows(5).Cells(colfiscalperiod).Value)
                obj.FiscalPeriodRun6 = clsCommon.myCstr(gv1.Rows(5).Cells(colfiscalrun).Value)
                obj.FiscalPeriodRunPerm6 = clsCommon.myCstr(gv1.Rows(5).Cells(COLFiscalRunPerm).Value)

                obj.FiscalPeriod7 = clsCommon.myCstr(gv1.Rows(6).Cells(colfiscalperiod).Value)
                obj.FiscalPeriodRun7 = clsCommon.myCstr(gv1.Rows(6).Cells(colfiscalrun).Value)
                obj.FiscalPeriodRunPerm7 = clsCommon.myCstr(gv1.Rows(6).Cells(COLFiscalRunPerm).Value)

                obj.FiscalPeriod8 = clsCommon.myCstr(gv1.Rows(7).Cells(colfiscalperiod).Value)
                obj.FiscalPeriodRun8 = clsCommon.myCstr(gv1.Rows(7).Cells(colfiscalrun).Value)
                obj.FiscalPeriodRunPerm8 = clsCommon.myCstr(gv1.Rows(7).Cells(COLFiscalRunPerm).Value)

                obj.FiscalPeriod9 = clsCommon.myCstr(gv1.Rows(8).Cells(colfiscalperiod).Value)
                obj.FiscalPeriodRun9 = clsCommon.myCstr(gv1.Rows(8).Cells(colfiscalrun).Value)
                obj.FiscalPeriodRunPerm9 = clsCommon.myCstr(gv1.Rows(8).Cells(COLFiscalRunPerm).Value)

                obj.FiscalPeriod10 = clsCommon.myCstr(gv1.Rows(9).Cells(colfiscalperiod).Value)
                obj.FiscalPeriodRun10 = clsCommon.myCstr(gv1.Rows(9).Cells(colfiscalrun).Value)
                obj.FiscalPeriodRunPerm10 = clsCommon.myCstr(gv1.Rows(9).Cells(COLFiscalRunPerm).Value)

                obj.FiscalPeriod11 = clsCommon.myCstr(gv1.Rows(10).Cells(colfiscalperiod).Value)
                obj.FiscalPeriodRun11 = clsCommon.myCstr(gv1.Rows(10).Cells(colfiscalrun).Value)
                obj.FiscalPeriodRunPerm11 = clsCommon.myCstr(gv1.Rows(10).Cells(COLFiscalRunPerm).Value)

                obj.FiscalPeriod12 = clsCommon.myCstr(gv1.Rows(11).Cells(colfiscalperiod).Value)
                obj.FiscalPeriodRun12 = clsCommon.myCstr(gv1.Rows(11).Cells(colfiscalrun).Value)
                obj.FiscalPeriodRunPerm12 = clsCommon.myCstr(gv1.Rows(11).Cells(COLFiscalRunPerm).Value)



                If btnsave.Text = "Save" Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If

                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.period_Code, NavigatorType.Current)
                End If


            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub


    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnsave.Enabled = True

            btndelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnsave.Text = "Update"
            BlankAllControls()
            'gv1.Rows.Clear()
            'gv1.Columns.Clear()
            loadgrid()

            chkinactive.Enabled = True
            'fndLocation.Enabled = False
            Dim obj As New clsDepreciationPeriods()
            obj = clsDepreciationPeriods.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.period_Code) > 0) Then

                txtDocNo.Value = obj.period_Code
                txtDesc.Text = obj.period_Desc

                If obj.Inactive = "1" Then
                    chkinactive.Checked = True
                Else
                    chkinactive.Checked = False
                End If

                ' gv1.Rows.AddNew()
                ' gv1.Rows(0).Cells(colfiscalperiod).Value = obj.FiscalPeriod1
                gv1.Rows(0).Cells(colfiscalrun).Value = obj.FiscalPeriodRun1
                gv1.Rows(0).Cells(COLFiscalRunPerm).Value = obj.FiscalPeriodRunPerm1

                ' gv1.Rows.AddNew()
                ' gv1.Rows(1).Cells(colfiscalperiod).Value = obj.FiscalPeriod2
                gv1.Rows(1).Cells(colfiscalrun).Value = obj.FiscalPeriodRun2
                gv1.Rows(1).Cells(COLFiscalRunPerm).Value = obj.FiscalPeriodRunPerm2

                ' gv1.Rows.AddNew()
                ' gv1.Rows(2).Cells(colfiscalperiod).Value = obj.FiscalPeriod3
                gv1.Rows(2).Cells(colfiscalrun).Value = obj.FiscalPeriodRun3
                gv1.Rows(2).Cells(COLFiscalRunPerm).Value = obj.FiscalPeriodRunPerm3

                ' gv1.Rows.AddNew()
                ' gv1.Rows(3).Cells(colfiscalperiod).Value = obj.FiscalPeriod4
                gv1.Rows(3).Cells(colfiscalrun).Value = obj.FiscalPeriodRun4
                gv1.Rows(3).Cells(COLFiscalRunPerm).Value = obj.FiscalPeriodRunPerm4

                ' gv1.Rows.AddNew()
                ' gv1.Rows(4).Cells(colfiscalperiod).Value = obj.FiscalPeriod5
                gv1.Rows(4).Cells(colfiscalrun).Value = obj.FiscalPeriodRun5
                gv1.Rows(4).Cells(COLFiscalRunPerm).Value = obj.FiscalPeriodRunPerm5

                ' gv1.Rows.AddNew()
                ' gv1.Rows(5).Cells(colfiscalperiod).Value = obj.FiscalPeriod6
                gv1.Rows(5).Cells(colfiscalrun).Value = obj.FiscalPeriodRun6
                gv1.Rows(5).Cells(COLFiscalRunPerm).Value = obj.FiscalPeriodRunPerm6
                ' gv1.Rows.AddNew()
                ' gv1.Rows(6).Cells(colfiscalperiod).Value = obj.FiscalPeriod7
                gv1.Rows(6).Cells(colfiscalrun).Value = obj.FiscalPeriodRun7
                gv1.Rows(6).Cells(COLFiscalRunPerm).Value = obj.FiscalPeriodRunPerm7

                ' gv1.Rows.AddNew()
                ' gv1.Rows(7).Cells(colfiscalperiod).Value = obj.FiscalPeriod8
                gv1.Rows(7).Cells(colfiscalrun).Value = obj.FiscalPeriodRun8
                gv1.Rows(7).Cells(COLFiscalRunPerm).Value = obj.FiscalPeriodRunPerm8

                '  gv1.Rows.AddNew()
                ' gv1.Rows(8).Cells(colfiscalperiod).Value = obj.FiscalPeriod9
                gv1.Rows(8).Cells(colfiscalrun).Value = obj.FiscalPeriodRun9
                gv1.Rows(8).Cells(COLFiscalRunPerm).Value = obj.FiscalPeriodRunPerm9

                '  gv1.Rows.AddNew()
                ' gv1.Rows(9).Cells(colfiscalperiod).Value = obj.FiscalPeriod10
                gv1.Rows(9).Cells(colfiscalrun).Value = obj.FiscalPeriodRun10
                gv1.Rows(9).Cells(COLFiscalRunPerm).Value = obj.FiscalPeriodRunPerm10

                ' gv1.Rows.AddNew()
                ' gv1.Rows(10).Cells(colfiscalperiod).Value = obj.FiscalPeriod11
                gv1.Rows(10).Cells(colfiscalrun).Value = obj.FiscalPeriodRun11
                gv1.Rows(10).Cells(COLFiscalRunPerm).Value = obj.FiscalPeriodRunPerm11

                ' gv1.Rows.AddNew()
                'gv1.Rows(11).Cells(colfiscalperiod).Value = obj.FiscalPeriod12
                gv1.Rows(11).Cells(colfiscalrun).Value = obj.FiscalPeriodRun12
                gv1.Rows(11).Cells(COLFiscalRunPerm).Value = obj.FiscalPeriodRunPerm12

            Else
                AddNew()

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub


    Sub BlankAllControls()
        txtDocNo.Value = ""
        txtDesc.Text = ""
        chkinactive.Checked = False

    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        loadgrid()
        isNewEntry = True
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        txtDocNo.MyReadOnly = False
    End Sub
    Public Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtDesc.MaxLength = 100
    End Sub


    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        fundelete()
    End Sub
    Public Sub fundelete()
        Try
            Dim obj As New clsDepreciationPeriods()
            If txtDocNo.Value = "" Then
                clsCommon.MyMessageBoxShow("Select the Period Code")
                Exit Sub
            End If
            If (myMessages.deleteConfirm()) Then
                If (clsDepreciationPeriods.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    'Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
    '    FunExport()
    'End Sub

    'Private Sub Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import.Click
    '    FunImport()
    'End Sub

    Private Sub FunExport()
        Try
            'Dim Qry As String = "select period_Code as [Period Code],period_Desc as [Description],Inactive as [Not In Use],FiscalPeriod1 ,FiscalPeriodRun1 ,FiscalPeriod2 ,FiscalPeriodRun2 ,FiscalPeriod3 ,FiscalPeriodRun3 ,FiscalPeriod4 ,FiscalPeriodRun4 ,FiscalPeriod5 ,FiscalPeriodRun5 ,FiscalPeriod6 ,FiscalPeriodRun6 ,FiscalPeriod7 ,FiscalPeriodRun7 ,FiscalPeriod8 ,FiscalPeriodRun8 ,FiscalPeriod9 ,FiscalPeriodRun9 ,FiscalPeriod10 ,FiscalPeriodRun10 ,FiscalPeriod11 ,FiscalPeriodRun11 ,FiscalPeriod12 ,FiscalPeriodRun12  from TSPL_DEPRECIATION_PERIODS"

            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            'If dt.Rows.Count <= 0 Then
            '    Qry = "select '' as [Period Code],''  as [Description],'' as [Not In Use],'' as [FiscalPeriod1],'' as [FiscalPeriodRun1],'' as [FiscalPeriod2],'' as [FiscalPeriodRun2],'' as [FiscalPeriod3],'' as [FiscalPeriodRun3],'' as [FiscalPeriod4],'' as [FiscalPeriodRun4],'' as [FiscalPeriod5],'' as [FiscalPeriodRun5],'' as [FiscalPeriod6],'' as [FiscalPeriodRun6],'' as [FiscalPeriod7],'' as [FiscalPeriodRun7],'' as [FiscalPeriod8],'' as [FiscalPeriodRun8]'' as [FiscalPeriod9],'' as [FiscalPeriodRun9],'' as [FiscalPeriod10],'' as [FiscalPeriodRun10],'' as [FiscalPeriod11],'' as [FiscalPeriodRun11],'' as [FiscalPeriod12],'' as [FiscalPeriodRun12] "
            'End If


            Dim Qry As String = "select period_Code as [Period Code],period_Desc as [Description],Inactive as [Not In Use],FiscalPeriodRun1 ,FiscalPeriodRunPerm1 ,FiscalPeriodRun2 ,FiscalPeriodRunPerm2 ,FiscalPeriodRun3,FiscalPeriodRunPerm3  ,FiscalPeriodRun4,FiscalPeriodRunPerm4  ,FiscalPeriodRun5 ,FiscalPeriodRunPerm5 ,FiscalPeriodRun6 ,FiscalPeriodRunPerm6 ,FiscalPeriodRun7,FiscalPeriodRunPerm7  ,FiscalPeriodRun8,FiscalPeriodRunPerm8  ,FiscalPeriodRun9,FiscalPeriodRunPerm9  ,FiscalPeriodRun10,FiscalPeriodRunPerm10  ,FiscalPeriodRun11,FiscalPeriodRunPerm11 ,FiscalPeriodRun12 ,FiscalPeriodRunPerm12 from TSPL_DEPRECIATION_PERIODS"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                Qry = "select '' as [Period Code],''  as [Description],'' as [Not In Use],'' as [FiscalPeriodRun1],'' as [FiscalPeriodRunPerm1],'' as [FiscalPeriodRun2],'' as [FiscalPeriodRunPerm2],'' as [FiscalPeriodRun3],'' as [FiscalPeriodRunPerm3],'' as [FiscalPeriodRun4],'' as [FiscalPeriodRunPerm4],'' as [FiscalPeriodRun5],'' as [FiscalPeriodRunPerm5],'' as [FiscalPeriodRun6],'' as [FiscalPeriodRunPerm6],'' as [FiscalPeriodRun7],'' as [FiscalPeriodRunPerm7],'' as [FiscalPeriodRun8],'' as [FiscalPeriodRunPerm9],'' as [FiscalPeriodRun9],'' as [FiscalPeriodRun10] as [FiscalPeriodRunPerm10],'' as [FiscalPeriodRun11],'' as [FiscalPeriodRunPerm11],'' as [FiscalPeriodRun12],'' as [FiscalPeriodRunPerm12] "
            End If
            transportSql.ExporttoExcel(Qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub FunImport()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Period Code", "Description", "Not In Use", "FiscalPeriodRun1", "FiscalPeriodRunPerm1", "FiscalPeriodRun2", "FiscalPeriodRunPerm2", "FiscalPeriodRun3", "FiscalPeriodRunPerm3", "FiscalPeriodRun4", "FiscalPeriodRunPerm4", "FiscalPeriodRun5", "FiscalPeriodRunPerm5", "FiscalPeriodRun6", "FiscalPeriodRunPerm6", "FiscalPeriodRun7", "FiscalPeriodRunPerm7", "FiscalPeriodRun8", "FiscalPeriodRunPerm8", "FiscalPeriodRun9", "FiscalPeriodRunPerm9", "FiscalPeriodRun10", "FiscalPeriodRunPerm10", "FiscalPeriodRun11", "FiscalPeriodRunPerm11", "FiscalPeriodRun12", "FiscalPeriodRunPerm12") Then
            Try
                clsCommon.ProgressBarShow()
                Dim chk As Boolean = True
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim obj As New clsDepreciationPeriods()
                    obj.period_Code = clsCommon.myCstr(grow.Cells("Period Code").Value)
                    If clsCommon.myLen(obj.period_Code) < 0 Then
                        Throw New Exception("Period Code can't be blank on Line No '" + LineNo + "'")
                        Exit Sub
                    ElseIf clsCommon.myLen(obj.period_Code) > 30 Then
                        Throw New Exception("The Maximum Length of Period Code on Line No '" + LineNo + "' Is Greater Than 30")
                        Exit Sub
                    End If


                    obj.period_Desc = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(obj.period_Desc) < 0 Then
                        Throw New Exception("Description can't be blank on Line No '" + LineNo + "'")
                        Exit Sub
                    ElseIf clsCommon.myLen(obj.period_Desc) > 100 Then
                        Throw New Exception("The Maximum Length of  Description on Line No '" + LineNo + "' Is Greater Than 100")
                        Exit Sub
                    End If


                    If clsCommon.myCstr(grow.Cells("Not In Use").Value) = "1" Then
                        obj.Inactive = "1"
                    ElseIf clsCommon.myCstr(grow.Cells("Not In Use").Value) = "0" Then
                        obj.Inactive = "0"
                    Else
                        Throw New Exception("Value of Not In Use should be 1 or 0 on Line No '" + LineNo + "'")
                        Exit Sub
                    End If

                    obj.FiscalPeriod1 = "1"
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRun1").Value) = "YES" Then
                        obj.FiscalPeriodRun1 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRun1").Value) = "NO" Then
                        obj.FiscalPeriodRun1 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRun1 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm1").Value) = "YES" Then
                        obj.FiscalPeriodRunPerm1 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm1").Value) = "NO" Then
                        obj.FiscalPeriodRunPerm1 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRunPerm1 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    '=============================
                    obj.FiscalPeriod2 = "2"
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRun2").Value) = "YES" Then
                        obj.FiscalPeriodRun2 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRun2").Value) = "NO" Then
                        obj.FiscalPeriodRun2 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRun2 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm2").Value) = "YES" Then
                        obj.FiscalPeriodRunPerm2 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm2").Value) = "NO" Then
                        obj.FiscalPeriodRunPerm2 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRunPerm2 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    '===================================================================
                    obj.FiscalPeriod3 = "3"
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRun3").Value) = "YES" Then
                        obj.FiscalPeriodRun3 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRun3").Value) = "NO" Then
                        obj.FiscalPeriodRun3 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRun3 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm3").Value) = "YES" Then
                        obj.FiscalPeriodRunPerm3 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm3").Value) = "NO" Then
                        obj.FiscalPeriodRunPerm3 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRunPerm3 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    '=========================
                    obj.FiscalPeriod4 = "4"
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRun4").Value) = "YES" Then
                        obj.FiscalPeriodRun4 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRun4").Value) = "NO" Then
                        obj.FiscalPeriodRun4 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRun4 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm4").Value) = "YES" Then
                        obj.FiscalPeriodRunPerm4 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm4").Value) = "NO" Then
                        obj.FiscalPeriodRunPerm4 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRunPerm4 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If

                    '===================
                    obj.FiscalPeriod5 = "5"
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRun5").Value) = "YES" Then
                        obj.FiscalPeriodRun5 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRun5").Value) = "NO" Then
                        obj.FiscalPeriodRun5 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRun5 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm5").Value) = "YES" Then
                        obj.FiscalPeriodRunPerm5 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm5").Value) = "NO" Then
                        obj.FiscalPeriodRunPerm5 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRunPerm5 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    '===================
                    obj.FiscalPeriod6 = "6"
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRun6").Value) = "YES" Then
                        obj.FiscalPeriodRun6 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRun6").Value) = "NO" Then
                        obj.FiscalPeriodRun6 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRun6 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm6").Value) = "YES" Then
                        obj.FiscalPeriodRunPerm6 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm6").Value) = "NO" Then
                        obj.FiscalPeriodRunPerm6 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRunPerm6 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    '===================

                    obj.FiscalPeriod7 = "7"
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRun7").Value) = "YES" Then
                        obj.FiscalPeriodRun7 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRun7").Value) = "NO" Then
                        obj.FiscalPeriodRun7 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRun7 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm7").Value) = "YES" Then
                        obj.FiscalPeriodRunPerm7 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm7").Value) = "NO" Then
                        obj.FiscalPeriodRunPerm7 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRunPerm7 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    '===============================

                    obj.FiscalPeriod8 = "8"
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRun8").Value) = "YES" Then
                        obj.FiscalPeriodRun8 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRun8").Value) = "NO" Then
                        obj.FiscalPeriodRun8 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRun8 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm8").Value) = "YES" Then
                        obj.FiscalPeriodRunPerm8 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm8").Value) = "NO" Then
                        obj.FiscalPeriodRunPerm8 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRunPerm8 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    '=================================

                    obj.FiscalPeriod9 = "9"
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRun9").Value) = "YES" Then
                        obj.FiscalPeriodRun9 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRun9").Value) = "NO" Then
                        obj.FiscalPeriodRun9 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRun9 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm9").Value) = "YES" Then
                        obj.FiscalPeriodRunPerm9 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm9").Value) = "NO" Then
                        obj.FiscalPeriodRunPerm9 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRunPerm9 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    '=================================

                    obj.FiscalPeriod10 = "10"
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRun10").Value) = "YES" Then
                        obj.FiscalPeriodRun10 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRun10").Value) = "NO" Then
                        obj.FiscalPeriodRun10 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRun10 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm10").Value) = "YES" Then
                        obj.FiscalPeriodRunPerm10 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm10").Value) = "NO" Then
                        obj.FiscalPeriodRunPerm10 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRunPerm10 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    '============

                    obj.FiscalPeriod11 = "11"
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRun11").Value) = "YES" Then
                        obj.FiscalPeriodRun11 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRun11").Value) = "NO" Then
                        obj.FiscalPeriodRun11 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRun11 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm11").Value) = "YES" Then
                        obj.FiscalPeriodRunPerm11 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm11").Value) = "NO" Then
                        obj.FiscalPeriodRunPerm11 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRunPerm11 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    '=============================

                    obj.FiscalPeriod12 = "12"
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRun12").Value) = "YES" Then
                        obj.FiscalPeriodRun12 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRun12").Value) = "NO" Then
                        obj.FiscalPeriodRun12 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRun12 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    If clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm12").Value) = "YES" Then
                        obj.FiscalPeriodRunPerm12 = "YES"
                    ElseIf clsCommon.myCstr(grow.Cells("FiscalPeriodRunPerm12").Value) = "NO" Then
                        obj.FiscalPeriodRunPerm12 = "NO"
                    Else
                        Throw New Exception("FiscalPeriodRunPerm12 should be YES or NO on Line No '" + LineNo + "'")
                        Exit Sub
                    End If
                    '==========================

                    Dim Qry As String = "select count(*)  from TSPL_DEPRECIATION_PERIODS where period_Code='" + obj.period_Code + "' "
                    Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
                    If count = 0 Then
                        isNewEntry = True
                    Else
                        isNewEntry = False
                    End If
                    chk = obj.SaveData(obj, isNewEntry)

                Next

                If (chk) Then
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                End If

            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub FrmDepreciationPeriods_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            fundelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            Me.Close()
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colfiscalrun) Then

                If gv1.CurrentRow.Cells(colfiscalrun).Value = "YES" Then
                    gv1.CurrentRow.Cells(colfiscalrun).Value = "NO"
                ElseIf gv1.CurrentRow.Cells(colfiscalrun).Value = "NO" Then
                    gv1.CurrentRow.Cells(colfiscalrun).Value = "YES"
                End If

            End If
            If gv1.CurrentColumn Is gv1.Columns(COLFiscalRunPerm) Then

                If gv1.CurrentRow.Cells(COLFiscalRunPerm).Value = "YES" Then
                    gv1.CurrentRow.Cells(COLFiscalRunPerm).Value = "NO"
                ElseIf gv1.CurrentRow.Cells(COLFiscalRunPerm).Value = "NO" Then
                    gv1.CurrentRow.Cells(COLFiscalRunPerm).Value = "YES"
                ElseIf clsCommon.myLen(gv1.CurrentRow.Cells(COLFiscalRunPerm).Value) <= 0 Then
                    gv1.CurrentRow.Cells(COLFiscalRunPerm).Value = "NO"
                End If

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click

        FunExport()
    End Sub

    Private Sub Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import.Click
        FunImport()
    End Sub
End Class

