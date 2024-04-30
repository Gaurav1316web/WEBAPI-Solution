Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common

Public Class FrmCostCenter
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False

    Dim userCode, companyCode As String
    'Private isInsideLoadData As Boolean = False
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.CostCenter)
        If Not (MyBase.isReadFlag) Then
            '--------------richa 15/07/2014 Ticket No BM00000003124---------
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            rmExport.Enabled = True
            rmImport.Enabled = True
        Else
            rmExport.Enabled = False
            rmImport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmCostCenter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        AddNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Trasnaction")
        ' globalFunc.mandatoryText(fnddesig.txtValue, txtdes)
        ToolTipdesig.SetToolTip(btnnew, "New")
        'fnddesig.txtValue.MaxLength = 12
        'AddHandler fnddesig.txtValue.TextChanged, AddressOf text_changed
        'AddHandler fnddesig.txtValue.KeyPress, AddressOf key_press
        'fnddesig.txtValue.CharacterCasing = CharacterCasing.Upper

        'btndelete.Enabled = False
        'btnsave.Enabled = True
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Private Sub FrmCostCenter_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsCostCenter = ClsCostCenter.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            btnsave.Text = "Update"
            txtCode.Value = obj.Cost_Code
            txtdes.Text = obj.Cost_name
            txtcostcenter.Value = obj.CostCenter_Code
            If clsCommon.myLen(txtcostcenter.Value) > 0 Then
                lblCostDesp.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_COST_CENTRE_GROUP_MASTER where CostCenter_Code='" + clsCommon.myCstr(txtcostcenter.Value) + "'")
            Else
                lblCostDesp.Text = ""
            End If

            txtGLAcc.Value = obj.GL_Account_Code
            If clsCommon.myLen(txtGLAcc.Value) > 0 Then
                lblGLAcc.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(txtGLAcc.Value) + "'")
            Else
                lblGLAcc.Text = ""
            End If
            '' Anubhooti 26-Sep-2014 BM00000003930
            btnsave.Enabled = True
            btndelete.Enabled = True
            txtCode.MyReadOnly = True
        End If
    End Sub
    Sub SaveData()
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.CostCenter, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New ClsCostCenter()
                obj.Cost_Code = txtCode.Value
                obj.Cost_name = txtdes.Text
                obj.CostCenter_Code = txtcostcenter.Value
                obj.GL_Account_Code = txtGLAcc.Value
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Cost_Code) from TSPL_CostCenter_MASTER where Cost_Code='" + obj.Cost_Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsCostCenter.SaveData(obj, isNewEntry)) Then
                    'trans.Commit()
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Cost_Code, NavigatorType.Current)

                End If
            End If
        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Then
                txtCode.Focus()
                Throw New Exception("Please Fill Cost Code")
            End If
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtdes.Text)) <= 0 Then
            txtdes.Focus()
            Throw New Exception("Please Fill Description")
        End If
        '' Anubhooti 09-Sep-2104 BM00000003439
        If clsCommon.myLen(txtcostcenter.Value) > 0 Then
            Dim qry As String = "select count(*) As Row from TSPL_COST_CENTRE_GROUP_MASTER where CostCenter_Code='" + txtcostcenter.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check <= 0 Then
                txtcostcenter.Focus()
                Throw New Exception("'" & clsCommon.myCstr(txtcostcenter.Value) & "' code does not exists.First create cost centre group stores entry")
            End If
        End If
        If clsCommon.myLen(txtGLAcc.Value) > 0 Then
            Dim qry As String = "select count(*) As Row from TSPL_GL_ACCOUNTS where Account_Code='" + txtGLAcc.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check <= 0 Then
                txtGLAcc.Focus()
                Throw New Exception("'" & clsCommon.myCstr(txtGLAcc.Value) & "' code does not exists.First create GL account entry")
            End If
        End If
        Return True
    End Function

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception(" City Code not found to delete")

            End If
            If clsCommon.MyMessageBoxShow("Do you want to delete Cost Code '" + txtCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "delete from TSPL_CostCenter_MASTER where Cost_Code='" + txtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Successfully Deleted", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Cost Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Cost Code is in use")

            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
    Sub AddNew()
        txtCode.Value = ""
        txtdes.Text = ""
        txtcostcenter.Value = ""
        txtGLAcc.Value = ""
        lblCostDesp.Text = ""
        lblGLAcc.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        txtCode.MyReadOnly = False
        btndelete.Enabled = False
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub fnddesig__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub fnddesig__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select Cost_Code as Code,Cost_name as Name from  TSPL_CostCenter_MASTER"
            'txtCode.Value = clsCommon.ShowSelectForm("TSPL_CostCenter_MASTER", qry, "Code", "", txtCode.Value, "", isButtonClicked)
            txtCode.Value = ClsCostCenter.getFinder("", txtCode.Value, isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub
    '------------------------------Preeti gupta -------BM00000002845------
    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Cost Code", "Cost Name", "Cost Center Code", "GL Account Code") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try

                connectSql.OpenConnection()
                clsCommon.ProgressBarShow()


                Dim obj As New ClsCostCenter()


                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows

                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                        Throw New Exception("Length of cost code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(clsCommon.myCstr(strcode)) <= 0 Then

                        Throw New Exception("Please Fill Cost Code")

                    End If
                    obj.Cost_Code = strcode

                    Dim strName As String = clsCommon.myCstr(grow.Cells(1).Value)
                    If (String.IsNullOrEmpty(strName)) Or clsCommon.myLen(strName) > 100 Then
                        Throw New Exception("Length of cost name should be max. 100 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Cost_name = strName
                    '' Anubhooti 09-Sep-2014 
                    Dim Cost_Centre_code As String = ""
                    Cost_Centre_code = clsCommon.myCstr(grow.Cells("Cost Center Code").Value)

                    If clsCommon.myLen(Cost_Centre_code) > 0 Then
                        Dim qry As String = "select count(*) As Row from TSPL_COST_CENTRE_GROUP_MASTER where CostCenter_Code='" + Cost_Centre_code + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("'" & clsCommon.myCstr(Cost_Centre_code) & "' code does not exists at line no. " + clsCommon.myCstr(linno) + ".First create cost centre group stores entry")
                        End If
                    End If
                    obj.CostCenter_Code = Cost_Centre_code.ToUpper()

                    Dim GL_Account_Code As String = ""
                    GL_Account_Code = clsCommon.myCstr(grow.Cells("GL Account Code").Value)

                    If clsCommon.myLen(GL_Account_Code) > 0 Then
                        Dim qry As String = "select count(*) As Row from TSPL_GL_ACCOUNTS where Account_Code='" + GL_Account_Code + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("'" & clsCommon.myCstr(GL_Account_Code) & "' code does not exists at line no. " + clsCommon.myCstr(linno) + ".First create GL account entry")
                        End If
                    End If
                    obj.GL_Account_Code = GL_Account_Code.ToUpper()
                    ''
                    If clsCommon.myLen(strcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_CostCenter_MASTER where Cost_Code='" + strcode + "' ", trans) > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True

                    End If
                    ClsCostCenter.SaveData(obj, isNewEntry, trans)

                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        '' Anubhooti 09-Sep-2014 BM00000003439 (Fetches Two more fields)
        str = "select Cost_Code as [Cost Code] ,Cost_name as [Cost Name],CostCenter_Code As [Cost Center Code],GL_Account_Code As [GL Account Code] from TSPL_CostCenter_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    '' Anubhooti 08-Sep-2014 BM00000003439
    Private Sub txtGLAcc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtGLAcc._MYValidating
        Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
        txtGLAcc.Value = clsCommon.ShowSelectForm("fndWIPAcc", Qry, "Account_Code", "", txtGLAcc.Value, "Account_Code", isButtonClicked)
        If clsCommon.myLen(txtGLAcc.Value) > 0 Then
            lblGLAcc.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + txtGLAcc.Value + "' ")
        Else
            lblGLAcc.Text = ""
        End If
    End Sub

    Private Sub txtcostcenter__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcostcenter._MYValidating
        Dim Qry As String = "select  CostCenter_Code , Description  from TSPL_COST_CENTRE_GROUP_MASTER "
        txtcostcenter.Value = clsCommon.ShowSelectForm("ccgrp", Qry, "CostCenter_Code", "", txtcostcenter.Value, "CostCenter_Code", isButtonClicked)
        If clsCommon.myLen(txtcostcenter.Value) > 0 Then
            lblCostDesp.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_COST_CENTRE_GROUP_MASTER Where CostCenter_Code='" + txtcostcenter.Value + "' ")
        Else
            lblCostDesp.Text = ""
        End If
    End Sub
End Class
