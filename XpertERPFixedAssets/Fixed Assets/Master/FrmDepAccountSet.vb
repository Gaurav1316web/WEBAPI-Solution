'created by vipin on 25/01/2013
'--Updation By--[Pankaj Kumar CHaudhary] - against Ticket - []
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

Public Class FrmDepAccountSet
    Inherits FrmMainTranScreen
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.DepAccSets)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag

        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub fndcustNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndAssetControl._MYValidating

        Try
            'Dim qry As String = " select  Account_Code , Description  from TSPL_GL_ACCOUNTS"
            ''Dim qry As String = clsERPFuncationality.glaccountquery()
            'fndAssetControl.Value = clsCommon.ShowSelectForm("accfnd", qry, "Account_Code", "", fndAssetControl.Value, "Account_Code", isButtonClicked)
            'lblAssetControl.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Account_Code , Description  from TSPL_GL_ACCOUNTS where Acc

            Dim qry As String
            Dim arrlist As New ArrayList()
            Dim whrcls As String = ""
            arrlist = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arrlist.Item(0)
            whrcls = arrlist.Item(1)
            If whrcls <> "" Then
                whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='N' "
            Else
                whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='N' "
            End If
            fndAssetControl.Value = clsCommon.ShowSelectForm("accfnd", qry, "Account_Code", whrcls, fndAssetControl.Value, "Account_Code", isButtonClicked)
            lblAssetControl.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select    Description  from TSPL_GL_ACCOUNTS where Account_Code='" + fndAssetControl.Value + "'"))
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub fndAcCtrlYearDis__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTransferClearingAccount._MYValidating

        Try
            Dim qry As String
            Dim arrlist As New ArrayList()
            Dim whrcls As String = ""
            arrlist = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arrlist.Item(0)
            whrcls = arrlist.Item(1)
            If whrcls <> "" Then
                whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='N' "
            Else
                whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='N' "
            End If
            fndTransferClearingAccount.Value = clsCommon.ShowSelectForm("accfndyeardis", qry, "Account_Code", whrcls, fndTransferClearingAccount.Value, "Account_Code", isButtonClicked)
            lblTransferClearingAccount.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select   Description  from TSPL_GL_ACCOUNTS where Account_Code='" + fndTransferClearingAccount.Value + "'"))
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub fndAcCrtlYearAdj__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDisposalCostAccount._MYValidating
        Try
            Dim qry As String
            Dim arrlist As New ArrayList()
            Dim whrcls As String = ""
            arrlist = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arrlist.Item(0)
            whrcls = arrlist.Item(1)
            If whrcls <> "" Then
                whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='N' "
            Else
                whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='N' "
            End If
            txtDisposalCostAccount.Value = clsCommon.ShowSelectForm("AcFndYearAdj", qry, "Account_Code", whrcls, txtDisposalCostAccount.Value, "Account_Code", isButtonClicked)
            lblDisposalCostAccount.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select   Description  from TSPL_GL_ACCOUNTS where Account_Code='" + txtDisposalCostAccount.Value + "'"))
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub fndAccumDep__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndAccumDep._MYValidating
        Try
            Dim qry As String
            Dim arrlist As New ArrayList()
            Dim whrcls As String = ""
            arrlist = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arrlist.Item(0)
            whrcls = arrlist.Item(1)
            If whrcls <> "" Then
                whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='N' "
            Else
                whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='N' "
            End If
            fndAccumDep.Value = clsCommon.ShowSelectForm("fndAccdep", qry, "Account_Code", whrcls, fndAccumDep.Value, "Account_Code", isButtonClicked)
            lblAccumDep.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select   Description  from TSPL_GL_ACCOUNTS where Account_Code='" + fndAccumDep.Value + "'"))
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub fndAccumDeprYearDisp__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDisposalAccount._MYValidating
        Try
            Dim qry As String
            Dim arrlist As New ArrayList()
            Dim whrcls As String = ""
            arrlist = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arrlist.Item(0)
            whrcls = arrlist.Item(1)
            If whrcls <> "" Then
                whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='N' "
            Else
                whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='N' "
            End If
            txtDisposalAccount.Value = clsCommon.ShowSelectForm("fndAccDepDis", qry, "Account_Code", whrcls, txtDisposalAccount.Value, "Account_Code", isButtonClicked)
            lblDisposalAccount.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select    Description  from TSPL_GL_ACCOUNTS where Account_Code='" + txtDisposalAccount.Value + "'"))
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub fndAccumDeprYearAdjus__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDisposalProceedAccount._MYValidating
        Try
            Dim qry As String
            Dim arrlist As New ArrayList()
            Dim whrcls As String = ""
            arrlist = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arrlist.Item(0)
            whrcls = arrlist.Item(1)
            If whrcls <> "" Then
                whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='N' "
            Else
                whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='N' "
            End If
            txtDisposalProceedAccount.Value = clsCommon.ShowSelectForm("fndAccDepAdj", qry, "Account_Code", whrcls, txtDisposalProceedAccount.Value, "Account_Code", isButtonClicked)
            lblDisposalProceedAccount.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select    Description  from TSPL_GL_ACCOUNTS where Account_Code='" + txtDisposalProceedAccount.Value + "'"))
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating

        'Dim qrychk As String = "select count(*)  from TSPL_Dep_AccountSet where AcSet_Code='" + txtDocNo.Value + "' "
        'Dim no As Integer = clsDBFuncationality.getSingleValue(qrychk)
        'If no > 0 Then
        '    Dim qry As String = "select AcSet_Code as [Code] ,AcSet_Desc as [Description] from TSPL_Dep_AccountSet"
        '    Dim whrClas As String = ""
        '    LoadData(clsCommon.ShowSelectForm("txtdoc", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
        'Else
        '    txtDesc.Text = ""
        '    chkinactive.Checked = False
        '    fndAssetControl.Value = ""
        '    lblAssetControl.Text = ""
        '    fndAcCtrlYearDis.Value = ""
        '    lblAcCtrlYearDis.Text = ""
        '    fndAcCrtlYearAdj.Value = ""
        '    lblAcCrtlYearAdj.Text = ""
        '    fndwip.Value = ""
        '    lblwip.Text = ""
        '    fndAccumDep.Value = ""
        '    lblAccumDep.Text = ""
        '    fndAccumDeprYearDisp.Value = ""
        '    lblAccumDeprYearDisp.Text = ""
        '    fndAccumDeprYearAdjus.Value = ""
        '    lblAccumDeprYearAdjus.Text = ""
        '    Dim qry As String = "select AcSet_Code as [Code] ,AcSet_Desc as [Description] from TSPL_Dep_AccountSet"
        '    Dim whrClas As String = ""
        '    LoadData(clsCommon.ShowSelectForm("txtdoc", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
        'End If


        Try
            Dim Qry As String = "select count(*)  from TSPL_Dep_AccountSet where AcSet_Code='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            If txtDocNo.MyReadOnly OrElse isButtonClicked Then
                Qry = "Select AcSet_Code as [Code], AcSet_Desc as [Description] from TSPL_Dep_AccountSet "
                txtDocNo.Value = clsCommon.ShowSelectForm("AssetCategorySelector", Qry, "Code", "", txtDocNo.Value, "", isButtonClicked)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_Dep_AccountSet where AcSet_Code='" + txtDocNo.Value + "'"
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

        If clsCommon.myLen(fndAssetControl.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Asset Control Account")
            fndAssetControl.Focus()
            Return False
        End If
        If clsCommon.myLen(fndTransferClearingAccount.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Transfer Clearing Account")
            fndTransferClearingAccount.Focus()
            Return False
        End If

        If clsCommon.myLen(txtDisposalCostAccount.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Disposal cost account")
            txtDisposalCostAccount.Focus()
            Return False
        End If


        If clsCommon.myLen(fndAccumDep.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select  Accumulated Depreciation Account")
            fndAccumDep.Focus()
            Return False
        End If
        If clsCommon.myLen(txtDisposalAccount.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Disposal Account")
            txtDisposalAccount.Focus()
            Return False
        End If

        'If clsCommon.myLen(txtDisposalProceedAccount.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Please select Disposal proceed Account")
        '    txtDisposalProceedAccount.Focus()
        '    Return False
        'End If

        If clsCommon.myLen(fndWIP.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Work In Progress Account")
            fndWIP.Focus()
            Return False
        End If

        If clsCommon.myLen(fndProfit.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Profit Account")
            fndProfit.Focus()
            Return False
        End If

        If clsCommon.myLen(fndLoss.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Loss Account")
            fndLoss.Focus()
            Return False
        End If


        Return True
    End Function

    Public Sub SaveData()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.DepAccSets, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim Arr As New List(Of ClsDeprAccountSet)
                Dim obj As New ClsDeprAccountSet()
                obj.AcSet_Code = txtDocNo.Value
                obj.AcSet_Desc = txtDesc.Text

                If chkinactive.Checked = True Then
                    obj.Inactive = "1"
                ElseIf chkinactive.Checked = False Then
                    obj.Inactive = "0"
                End If
                obj.Ac_Control = fndAssetControl.Value
                obj.Ac_Accum_Dep = fndAccumDep.Value
                obj.Ac_Dep_Account = txtDepAccount.Value

                obj.Disposal_Account = txtDisposalAccount.Value
                obj.Disposal_Proceed_Account = txtDisposalProceedAccount.Value
                obj.Transfer_Clearing_Account = fndTransferClearingAccount.Value
                obj.Disposal_Cost_Account = txtDisposalCostAccount.Value

                obj.WIP_AC = fndWIP.Value
                obj.PROFIT_AC = fndProfit.Value
                obj.LOSS_AC = fndLoss.Value

                Arr.Add(obj)
                If (ClsDeprAccountSet.SaveData(Arr)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.AcSet_Code, NavigatorType.Current)
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

            chkinactive.Enabled = True
            'fndLocation.Enabled = False
            Dim obj As New ClsDeprAccountSet()
            obj = ClsDeprAccountSet.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.AcSet_Code) > 0) Then

                txtDocNo.Value = obj.AcSet_Code
                txtDesc.Text = obj.AcSet_Desc
                If obj.Inactive = "1" Then
                    chkinactive.Checked = True
                Else
                    chkinactive.Checked = False
                End If

                fndAssetControl.Value = obj.Ac_Control
                lblAssetControl.Text = clsGLAccount.GetName(obj.Ac_Control)

                txtDepAccount.Value = clsCommon.myCstr(obj.Ac_Dep_Account)
                lblDepAc.Text = clsGLAccount.GetName(obj.Ac_Dep_Account)

                fndAccumDep.Value = obj.Ac_Accum_Dep
                lblAccumDep.Text = clsGLAccount.GetName(obj.Ac_Accum_Dep)

                txtDisposalAccount.Value = obj.Disposal_Account
                lblDisposalAccount.Text = clsGLAccount.GetName(obj.Disposal_Account)

                txtDisposalProceedAccount.Value = obj.Disposal_Proceed_Account
                lblDisposalProceedAccount.Text = clsGLAccount.GetName(obj.Disposal_Proceed_Account)

                fndTransferClearingAccount.Value = obj.Transfer_Clearing_Account
                lblTransferClearingAccount.Text = clsGLAccount.GetName(obj.Transfer_Clearing_Account)

                txtDisposalCostAccount.Value = obj.Disposal_Cost_Account
                lblDisposalCostAccount.Text = clsGLAccount.GetName(obj.Disposal_Cost_Account)

                fndWIP.Value = obj.WIP_AC
                lblWIPDesc.Text = clsGLAccount.GetName(obj.WIP_AC)

                fndProfit.Value = obj.PROFIT_AC
                lblProfitDesc.Text = clsGLAccount.GetName(obj.PROFIT_AC)

                fndLoss.Value = obj.LOSS_AC
                lblLossDesc.Text = clsGLAccount.GetName(obj.LOSS_AC)
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
        fndAssetControl.Value = ""
        lblAssetControl.Text = ""
        fndTransferClearingAccount.Value = ""
        lblTransferClearingAccount.Text = ""
        txtDisposalCostAccount.Value = ""
        lblDisposalCostAccount.Text = ""

        fndAccumDep.Value = ""
        lblAccumDep.Text = ""
        txtDisposalAccount.Value = ""
        lblDisposalAccount.Text = ""
        txtDisposalProceedAccount.Value = ""
        lblDisposalProceedAccount.Text = ""
        txtDepAccount.Value = ""
        lblDepAc.Text = ""
        fndWIP.Value = ""
        lblWIPDesc.Text = ""
        fndProfit.Value = ""
        lblProfitDesc.Text = ""
        fndLoss.Value = ""
        lblLossDesc.Text = ""
    End Sub

    Sub AddNew()
        BlankAllControls()
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
            Dim obj As New ClsDeprAccountSet()
            If txtDocNo.Value = "" Then
                clsCommon.MyMessageBoxShow("Select the Account Set Code")
                Exit Sub
            End If
            If (myMessages.deleteConfirm()) Then
                If (ClsDeprAccountSet.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmDepAccountSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SetLength()
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S Save/Update")
    End Sub

    Private Sub Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        FunExport()
    End Sub

    Private Sub Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import.Click
        FunImport()
    End Sub

    Private Sub FunExport()
        Try
            ''"Account Set Code", "Description", "Not In Use", "Asset Control", "Asset Control-This Year Disposal", "Asset Control-This Year Adjustment", "Work In Progress", "Accumulated Depreciation", "Accumulation Depr-This Year Disposal", "Accumulation Depr-This Year Adjustment"
            Dim Qry As String = "Select AcSet_Code as [A/c Set Code], AcSet_Desc as [A/c Set Description], Ac_Control as [Asset Control A/c], Ac_Accum_Dep as [Accoumulated Dep A/c], Ac_Dep_Account as [Dep A/c], Disposal_Account as [Disposal A/c], Disposal_Proceed_Account as [Disposal  Proceed A/c], Disposal_Cost_Account as [DIsposal Cost A/c], Transfer_Clearing_Account as [Transfer Clearing A/c], Case When Inactive=1 Then 'Y' Else 'N' End as [Not In Use Y/N],WIP_AC as [WIP AC] ,PROFIT_AC as [PROFIT AC],LOSS_AC as [LOSS AC] from TSPL_Dep_AccountSet"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                Qry = "Select '' as [A/c Set Code], '' as [A/c Set Description], '' as [Asset Control A/c], '' as [Accoumulated Dep A/c], '' as [Dep A/c], '' as [Disposal A/c], '' as [Disposal  Proceed A/c], '' as [Disposal Cost A/c], '' as [Transfer Clearing A/c], '' as [Not In Use Y/N],'' as [WIP AC],'' as [PROFIT AC],'' as [LOSS AC]"
            End If
            transportSql.ExporttoExcel(Qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub FunImport()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "A/c Set Code", "A/c Set Description", "Asset Control A/c", "Accoumulated Dep A/c", "Dep A/c", "Disposal A/c", "Disposal  Proceed A/c", "DIsposal Cost A/c", "Transfer Clearing A/c", "Not In Use Y/N", "WIP AC", "PROFIT AC", "LOSS AC") Then
            Try
                clsCommon.ProgressBarShow()
                Dim chk As Boolean = True
                Dim Arr As New List(Of ClsDeprAccountSet)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim obj As New ClsDeprAccountSet()
                    obj.AcSet_Code = clsCommon.myCstr(grow.Cells("A/c Set Code").Value)
                    If clsCommon.myLen(obj.AcSet_Code) < 0 Then
                        Throw New Exception("Line No '" + LineNo + "' : - 'Account Set Code' can not be left BLANK.")
                    ElseIf clsCommon.myLen(obj.AcSet_Code) > 30 Then
                        Throw New Exception("Line No '" + LineNo + "' : - 'Account Set Code' can be of 30 character long")
                    End If

                    obj.AcSet_Desc = clsCommon.myCstr(grow.Cells("A/c Set Description").Value)
                    If clsCommon.myLen(obj.AcSet_Code) < 0 Then
                        Throw New Exception("Line No '" + LineNo + "' : - 'Account Set Description' can not be left BLANK.")
                    ElseIf clsCommon.myLen(obj.AcSet_Code) > 100 Then
                        Throw New Exception("Line No '" + LineNo + "' : - 'Account Set Descriptionj' can be of 100 character long")
                    End If

                    obj.Ac_Control = clsCommon.myCstr(grow.Cells("Asset Control A/c").Value)
                    If clsCommon.myLen(obj.Ac_Control) > 0 Then
                        obj.Ac_Control = clsDBFuncationality.getSingleValue("Select Account_Code from TSPL_GL_ACCOUNTS WHERE account_type <>'Retained Earnings'and ControlAccount='N' AND Account_Code='" + obj.Ac_Control + "'")
                        If clsCommon.myLen(obj.Ac_Control) <= 0 Then
                            Throw New Exception("Line No '" + LineNo + "' : - Asset Control Account does not exist in Master.")
                        End If
                    Else
                        Throw New Exception("Line No '" + LineNo + "' : - Asset Control Account can not be left BLANK.")
                    End If

                    obj.Ac_Accum_Dep = clsCommon.myCstr(grow.Cells("Accoumulated Dep A/c").Value)
                    If clsCommon.myLen(obj.Ac_Accum_Dep) > 0 Then
                        obj.Ac_Accum_Dep = clsDBFuncationality.getSingleValue("Select Account_Code from TSPL_GL_ACCOUNTS WHERE account_type <>'Retained Earnings'and ControlAccount='N' AND Account_Code='" + obj.Ac_Accum_Dep + "'")
                        If clsCommon.myLen(obj.Ac_Control) <= 0 Then
                            Throw New Exception("Line No '" + LineNo + "' : - Accumulated Depreciation Account does not exist in Master.")
                        End If
                    Else
                        Throw New Exception("Line No '" + LineNo + "' : - Accumulated Depreciation Account can not be left BLANK.")
                    End If

                    obj.Ac_Dep_Account = clsCommon.myCstr(grow.Cells("Dep A/c").Value)
                    If clsCommon.myLen(obj.Ac_Dep_Account) > 0 Then
                        obj.Ac_Dep_Account = clsDBFuncationality.getSingleValue("Select Account_Code from TSPL_GL_ACCOUNTS WHERE account_type <>'Retained Earnings'and ControlAccount='N' AND Account_Code='" + obj.Ac_Dep_Account + "'")
                        If clsCommon.myLen(obj.Ac_Control) <= 0 Then
                            Throw New Exception("Line No '" + LineNo + "' : - Depreciation Account does not exist in Master.")
                        End If
                    Else
                        Throw New Exception("Line No '" + LineNo + "' : - Depreciation Account can not be left BLANK.")
                    End If

                    obj.Disposal_Account = clsCommon.myCstr(grow.Cells("Disposal A/c").Value)
                    If clsCommon.myLen(obj.Disposal_Account) > 0 Then
                        obj.Disposal_Account = clsDBFuncationality.getSingleValue("Select Account_Code from TSPL_GL_ACCOUNTS WHERE account_type <>'Retained Earnings'and ControlAccount='N' AND Account_Code='" + obj.Disposal_Account + "'")
                        If clsCommon.myLen(obj.Ac_Control) <= 0 Then
                            Throw New Exception("Line No '" + LineNo + "' : - Disposal Account does not exist in Master.")
                        End If
                    Else
                        Throw New Exception("Line No '" + LineNo + "' : - Disposal Account can not be left BLANK.")
                    End If

                    obj.Disposal_Proceed_Account = clsCommon.myCstr(grow.Cells("Disposal  Proceed A/c").Value)
                    If clsCommon.myLen(obj.Disposal_Proceed_Account) > 0 Then
                        obj.Disposal_Proceed_Account = clsDBFuncationality.getSingleValue("Select Account_Code from TSPL_GL_ACCOUNTS WHERE account_type <>'Retained Earnings'and ControlAccount='N' AND Account_Code='" + obj.Disposal_Proceed_Account + "'")
                        If clsCommon.myLen(obj.Disposal_Proceed_Account) <= 0 Then
                            Throw New Exception("Line No '" + LineNo + "' : - Disposal Proceed Account does not exist in Master.")
                        End If
                    Else
                        Throw New Exception("Line No '" + LineNo + "' : - Disposal Proceed Account can not be left BLANK.")
                    End If

                    obj.Disposal_Cost_Account = clsCommon.myCstr(grow.Cells("Disposal Cost A/c").Value)
                    If clsCommon.myLen(obj.Disposal_Cost_Account) > 0 Then
                        obj.Disposal_Cost_Account = clsDBFuncationality.getSingleValue("Select Account_Code from TSPL_GL_ACCOUNTS WHERE account_type <>'Retained Earnings'and ControlAccount='N' AND Account_Code='" + obj.Disposal_Cost_Account + "'")
                        If clsCommon.myLen(obj.Disposal_Cost_Account) <= 0 Then
                            Throw New Exception("Line No '" + LineNo + "' : - Disposal Cost Account does not exist in Master.")
                        End If
                    Else
                        Throw New Exception("Line No '" + LineNo + "' : - Disposal Cost Account can not be left BLANK.")
                    End If

                    obj.Transfer_Clearing_Account = clsCommon.myCstr(grow.Cells("Transfer Clearing A/c").Value)
                    If clsCommon.myLen(obj.Transfer_Clearing_Account) > 0 Then
                        obj.Transfer_Clearing_Account = clsDBFuncationality.getSingleValue("Select Account_Code from TSPL_GL_ACCOUNTS WHERE account_type <>'Retained Earnings'and ControlAccount='N' AND Account_Code='" + obj.Transfer_Clearing_Account + "'")
                        If clsCommon.myLen(obj.Disposal_Cost_Account) <= 0 Then
                            Throw New Exception("Line No '" + LineNo + "' : - Transfer Clearing Account does not exist in Master.")
                        End If
                    Else
                        Throw New Exception("Line No '" + LineNo + "' : - Transfer Clearing Account can not be left BLANK.")
                    End If
                    '===============added by shivani===================================>
                    obj.WIP_AC = clsCommon.myCstr(grow.Cells("WIP AC").Value)
                    If clsCommon.myLen(obj.WIP_AC) > 0 Then
                        obj.WIP_AC = clsDBFuncationality.getSingleValue("Select Account_Code from TSPL_GL_ACCOUNTS WHERE account_type <>'Retained Earnings'and ControlAccount='N' AND Account_Code='" + obj.WIP_AC + "'")
                        If clsCommon.myLen(obj.WIP_AC) <= 0 Then
                            Throw New Exception("Line No '" + LineNo + "' : - Depreciation Account does not exist in Master.")
                        End If
                    Else
                        Throw New Exception("Line No '" + LineNo + "' : - Depreciation Account can not be left BLANK.")
                    End If

                    obj.LOSS_AC = clsCommon.myCstr(grow.Cells("LOSS AC").Value)
                    If clsCommon.myLen(obj.LOSS_AC) > 0 Then
                        obj.LOSS_AC = clsDBFuncationality.getSingleValue("Select Account_Code from TSPL_GL_ACCOUNTS WHERE account_type <>'Retained Earnings'and ControlAccount='N' AND Account_Code='" + obj.LOSS_AC + "'")
                        If clsCommon.myLen(obj.LOSS_AC) <= 0 Then
                            Throw New Exception("Line No '" + LineNo + "' : - Depreciation Account does not exist in Master.")
                        End If
                    Else
                        Throw New Exception("Line No '" + LineNo + "' : - Depreciation Account can not be left BLANK.")
                    End If

                    obj.PROFIT_AC = clsCommon.myCstr(grow.Cells("PROFIT AC").Value)
                    If clsCommon.myLen(obj.PROFIT_AC) > 0 Then
                        obj.PROFIT_AC = clsDBFuncationality.getSingleValue("Select Account_Code from TSPL_GL_ACCOUNTS WHERE account_type <>'Retained Earnings'and ControlAccount='N' AND Account_Code='" + obj.PROFIT_AC + "'")
                        If clsCommon.myLen(obj.PROFIT_AC) <= 0 Then
                            Throw New Exception("Line No '" + LineNo + "' : - Depreciation Account does not exist in Master.")
                        End If
                    Else
                        Throw New Exception("Line No '" + LineNo + "' : - Depreciation Account can not be left BLANK.")
                    End If



                    obj.Inactive = clsCommon.myCstr(grow.Cells("Not In Use Y/N").Value)
                    If clsCommon.myLen(obj.Inactive) > 0 Then
                        If clsCommon.CompairString(obj.Inactive, "Y") = CompairStringResult.Equal Then
                            obj.Inactive = "1"
                        ElseIf clsCommon.CompairString(obj.Inactive, "N") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Inactive, "") = CompairStringResult.Equal Then
                            obj.Inactive = "0"
                        Else
                            Throw New Exception("Line No '" + LineNo + "' : - 'Not In Use' can be like 'Y' or 'N' or left BLANK.")
                        End If
                    End If
                    Arr.Add(obj)
                Next

                If ClsDeprAccountSet.SaveData(Arr) Then
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

    Private Sub FrmDepAccountSet_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub txtDepAccount__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepAccount._MYValidating
        Try
            Dim qry As String
            Dim arrlist As New ArrayList()
            Dim whrcls As String = ""
            arrlist = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arrlist.Item(0)
            whrcls = arrlist.Item(1)
            If whrcls <> "" Then
                whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='N' "
            Else
                whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='N' "
            End If
            txtDepAccount.Value = clsCommon.ShowSelectForm("accfnd", qry, "Account_Code", whrcls, txtDepAccount.Value, "Account_Code", isButtonClicked)
            lblDepAc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_GL_ACCOUNTS where Account_Code='" + txtDepAccount.Value + "'"))
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub fndWIP__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndWIP._MYValidating
        Try
            Dim qry As String
            Dim arrlist As New ArrayList()
            Dim whrcls As String = ""
            arrlist = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arrlist.Item(0)
            whrcls = arrlist.Item(1)
            If whrcls <> "" Then
                whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='N' "
            Else
                whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='N' "
            End If
            fndWIP.Value = clsCommon.ShowSelectForm("fndWIP", qry, "Account_Code", whrcls, fndWIP.Value, "Account_Code", isButtonClicked)
            lblWIPDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select    Description  from TSPL_GL_ACCOUNTS where Account_Code='" + fndWIP.Value + "'"))
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub fndProfit__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProfit._MYValidating
        Try
            Dim qry As String
            Dim arrlist As New ArrayList()
            Dim whrcls As String = ""
            arrlist = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arrlist.Item(0)
            whrcls = arrlist.Item(1)
            If whrcls <> "" Then
                whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='N' "
            Else
                whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='N' "
            End If
            fndProfit.Value = clsCommon.ShowSelectForm("fndProfit", qry, "Account_Code", whrcls, fndProfit.Value, "Account_Code", isButtonClicked)
            lblProfitDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select    Description  from TSPL_GL_ACCOUNTS where Account_Code='" + fndProfit.Value + "'"))
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub fndLoss__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLoss._MYValidating
        Try
            Dim qry As String
            Dim arrlist As New ArrayList()
            Dim whrcls As String = ""
            arrlist = clsERPFuncationality.glaccountquery(objCommonVar.CurrentUserCode)
            qry = arrlist.Item(0)
            whrcls = arrlist.Item(1)
            If whrcls <> "" Then
                whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='N' "
            Else
                whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='N' "
            End If
            fndLoss.Value = clsCommon.ShowSelectForm("fndLoss", qry, "Account_Code", whrcls, fndLoss.Value, "Account_Code", isButtonClicked)
            lblLossDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select    Description  from TSPL_GL_ACCOUNTS where Account_Code='" + fndLoss.Value + "'"))
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Account Set", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtDocNo.Value, "AcSet_Code", "TSPL_DEP_ACCOUNTSET")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class








