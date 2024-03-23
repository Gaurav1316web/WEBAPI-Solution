' ------------------------- Created By Preeti Gupta  --------------------'
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports System.Text.RegularExpressions

Public Class FrmInvestmentType
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
#End Region

#Region "Functions"
    Public Function SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmInvestmentType)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Return False
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        RadMenuItem3.Enabled = MyBase.isModifyFlag
        Return True
    End Function
    Function AllowToSave() As Boolean
        Dim ITSec As Double = 0

        If clsCommon.myLen(txtcode.Value) <= 0 Or clsCommon.myLen(txtcode.Value) > 30 Then
            common.clsCommon.MyMessageBoxShow(Me, "Code can not be left blank", Me.Text)
            txtcode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtITSec.Value) <= 0 Then
            myMessages.blankValue(Me, "IT section code", Me.Text)
            txtITSec.Focus()
            Return False
        End If
        If clsCommon.myLen(txtITSec.Value) > 0 Then
            ITSec = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) From  TSPL_IT_SECTION Where IT_SECTION_CODE='" & clsCommon.myCstr(txtITSec.Value) & "'"))
            If ITSec = 0 Then
                common.clsCommon.MyMessageBoxShow("Please check ! IT section code (" & clsCommon.myCstr(txtITSec.Value) & ") does not exists.")
                txtITSec.Focus()
                Return False
            End If
        End If
        Return True
    End Function
    Sub funReset()
        isNewEntry = True
        txtcode.MyReadOnly = False
        txtcode.Value = Nothing
        txtcode.Focus()
        TxtDesp.Text = ""
        txtITSec.Value = ""
        lblITSectionName.Text = ""

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'txtCode.MyReadOnly = True
        btnsave.Enabled = True

        'isNewEntry = False
        Dim obj As New clsInvestmentType()
        obj = clsInvestmentType.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.CODE) > 0) Then
            funReset()
            isNewEntry = False
            btndelete.Enabled = True
            txtcode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
            txtcode.Value = obj.CODE
            TxtDesp.Text = obj.Description
            txtITSec.Value = obj.IT_SECTION_CODE
            lblITSectionName.Text = obj.IT_SECTION_Name
        End If
    End Sub
    Public Function Save()
        If AllowToSave() Then
            Dim obj As New clsInvestmentType()
            obj.CODE = txtcode.Value
            obj.Description = TxtDesp.Text
            obj.IT_SECTION_CODE = txtITSec.Value

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If (obj.SaveData(obj, isNewEntry, trans)) Then
                trans.Commit()
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.CODE, NavigatorType.Current)
                btnsave.Text = "Update"
                btndelete.Enabled = True
            Else
                btnsave.Text = "Save"
                btndelete.Enabled = False
                trans.Rollback()
            End If
        End If
        Return True
    End Function
    Sub DeleteData()
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Code not found to delete", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsInvestmentType.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
#End Region


    Private Sub FrmInvestmentType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        funReset()
    End Sub

    Private Sub FrmInvestmentType_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "select count(*) from TSPL_INVESTMENT_TYPE where CODE ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If

        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select CODE As [Code],Description As [Description],IT_SECTION_CODE As [IT SECTION CODE] from TSPL_INVESTMENT_TYPE"
            txtcode.Value = clsCommon.ShowSelectForm("TSPL_INVESTMENT_TYPE", qry, "Code", "", txtcode.Value, "TSPL_INVESTMENT_TYPE.CODE", isButtonClicked)
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As clsInvestmentType
                objOT = clsInvestmentType.GetData(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtITSec__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtITSec._MYValidating
        Dim qry As String = " Select IT_SECTION_CODE AS CODE ,DESCRIPTION  From TSPL_IT_SECTION "
        txtITSec.Value = clsCommon.ShowSelectForm("fmITSec", qry, "Code", "", txtITSec.Value, "CODE", isButtonClicked)

        If clsCommon.myLen(txtITSec.Value) > 0 Then
            lblITSectionName.Text = clsDBFuncationality.getSingleValue("Select DESCRIPTION  From TSPL_IT_SECTION  where IT_SECTION_CODE ='" + txtITSec.Value + "' ")
        Else
            lblITSectionName.Text = ""
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String
        str = "Select CODE As [Code],Description As [Description],IT_SECTION_CODE As [IT SECTION CODE] from TSPL_INVESTMENT_TYPE"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim IT_SEC_CODE As String = ""
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Code", "Description", "IT SECTION CODE") Then
            Dim trans As SqlTransaction
            trans = clsDBFuncationality.GetTransactin()
            Try

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsInvestmentType()
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect.")
                    End If
                    obj.CODE = strCode

                    Dim strDesp As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If strDesp.Length > 100 Then
                        Throw New Exception("Description can should be max. 100 character On Line No " & LineNo & ".")
                    End If
                    obj.Description = strDesp

                    Dim strITSec As String = clsCommon.myCstr(grow.Cells("IT SECTION CODE").Value)

                    If strITSec.Length > 30 Or (String.IsNullOrEmpty(strITSec)) Then
                        Throw New Exception("IT Section Code can not be blank or incorrect On Line No " & LineNo & ".")
                    Else
                        IT_SEC_CODE = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_IT_SECTION Where IT_SECTION_CODE ='" & strITSec & "'", trans)
                        If IT_SEC_CODE <= 0 Then
                            Throw New Exception("IT Section Code(" & strITSec & ") On Line No " & LineNo & " does not exist . Please make it entry first.")
                        End If
                    End If
                    obj.IT_SECTION_CODE = strITSec

                    obj.SaveData(obj, clsInvestmentType.CheckNewEntry(obj.CODE, trans), trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
                trans.Rollback()
            End Try

        End If
        Me.Controls.Remove(gv)

    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        funReset()
    End Sub
End Class
