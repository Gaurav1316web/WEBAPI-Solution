' ------------------------- Created By Preeti Gupta  --------------------'
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports XpertERPEngine
Imports common
Public Class FrmITSection
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
#End Region

#Region "Functions"
    Public Function SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmITSection)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied", Me.Text)
            Me.Close()
            Return False
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        'RadMenuItem3.Enabled = MyBase.isModifyFlag
        Return True
    End Function
    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Code can not be left blank", Me.Text)
            txtcode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtITAct.Text) <= 0 Then
            myMessages.blankValue("Income tax act")
            txtITAct.Focus()
            Return False

        ElseIf clsCommon.myLen(txtMinAmt.Text) > 0 AndAlso (clsCommon.myCdbl(txtMinAmt.Text) < 0 Or Not IsNumeric(txtMinAmt.Text)) < 0 Then
            clsCommon.MyMessageBoxShow("Minimum Amount can not be negative", Me.Text)
            txtMinAmt.Focus()
            Return False
        ElseIf clsCommon.myLen(txtMaxAmt.Text) > 0 AndAlso (clsCommon.myCdbl(txtMaxAmt.Text) < 0 Or Not IsNumeric(txtMaxAmt.Text)) Then
            clsCommon.MyMessageBoxShow("Maximum Amount can not be negative", Me.Text)
            txtMaxAmt.Focus()
            Return False
        End If
        Return True
    End Function
    Sub funReset()
        isNewEntry = True
        txtcode.MyReadOnly = False
        txtcode.Value = Nothing
        txtcode.Focus()
        TxtDesp.Text = ""
        txtITAct.Text = ""
        txtMinAmt.Text = 0
        txtMaxAmt.Text = 0

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        ' txtCode.MyReadOnly = True
        btnsave.Enabled = True

        'isNewEntry = False
        Dim obj As New ClsITSection()
        obj = clsITSection.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.IT_SECTION_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btndelete.Enabled = True
            txtcode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
            txtcode.Value = obj.IT_SECTION_CODE
            TxtDesp.Text = obj.Description
            txtITAct.Text = obj.INCOME_TAX_ACT
            txtMinAmt.Text = obj.MINIMUM_AMOUNT
            txtMaxAmt.Text = obj.MAXIMUM_AMOUNT
        End If
    End Sub
    Public Function Save()
        If AllowToSave() Then
            Dim obj As New ClsITSection()
            obj.IT_SECTION_CODE = txtcode.Value
            obj.Description = TxtDesp.Text
            obj.INCOME_TAX_ACT = txtITAct.Text

            If String.IsNullOrEmpty(txtMinAmt.Text) Then
                txtMinAmt.Text = 0
            End If
            If String.IsNullOrEmpty(txtMaxAmt.Text) Then
                txtMaxAmt.Text = 0
            End If

            obj.MINIMUM_AMOUNT = txtMinAmt.Text
            obj.MAXIMUM_AMOUNT = txtMaxAmt.Text

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If (obj.SaveData(obj, isNewEntry, trans)) Then
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                LoadData(obj.IT_SECTION_CODE, NavigatorType.Current)
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
            common.clsCommon.MyMessageBoxShow("Code could not found to delete", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsitsection.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
#End Region
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub FrmITSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        funReset()
    End Sub

    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "select count(*) from TSPL_IT_SECTION where IT_SECTION_CODE ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If

        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select IT_SECTION_CODE As [Code],Description As [Description],INCOME_TAX_ACT As [INCOME TAX ACT],MINIMUM_AMOUNT As [MINIMUM AMOUNT],MAXIMUM_AMOUNT As [MAXIMUM AMOUNT] from TSPL_IT_SECTION"
            txtcode.Value = clsCommon.ShowSelectForm("TSPL_IT_SECTION", qry, "Code", "", txtcode.Value, "TSPL_IT_SECTION.IT_SECTION_CODE", isButtonClicked)
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As clsITSection
                objOT = clsITSection.GetData(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description", "INCOME TAX ACT", "MINIMUM AMOUNT", "MAXIMUM AMOUNT") Then
            Dim trans As SqlTransaction
            trans = clsDBFuncationality.GetTransactin()
            Try

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsITSection()
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect.")
                    End If
                    obj.IT_SECTION_CODE = strCode

                    Dim strDesp As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If strDesp.Length > 100 Then
                        Throw New Exception("Description can should be max. 100 character On Line No " & LineNo & ".")
                    End If
                    obj.Description = strDesp

                    Dim strITAct As String = clsCommon.myCstr(grow.Cells("INCOME TAX ACT").Value)
                    If strITAct.Length > 50 Or (String.IsNullOrEmpty(strITAct)) Then
                        Throw New Exception("Income Tax Act can not be blank or incorrect on line no " & LineNo & ".")
                    End If
                    obj.INCOME_TAX_ACT = strITAct

                    'Dim StrMin As String = clsCommon.myCstr(grow.Cells("MINIMUM AMOUNT").Value)
                    'If String.IsNullOrEmpty(StrMin) Then
                    '    Throw New Exception("Income Tax Act can not be blank or incorrect.")
                    'End If
                    Dim Min As String = clsCommon.myCstr(grow.Cells("MINIMUM AMOUNT").Value)
                    If String.IsNullOrEmpty(Min) Then
                        Min = "0"
                    ElseIf Not IsNumeric(grow.Cells("MINIMUM AMOUNT").Value) Or clsCommon.myCdbl(grow.Cells("MINIMUM AMOUNT").Value) < 0 Then
                        Throw New Exception("Minimum amount should be numeric or in correct format on line no " & LineNo & ".")
                    ElseIf Min.Length > 10 Then
                        Throw New Exception("Please check ! length of minimum amount should be max 10 digits on line no " & LineNo & ".")
                    End If
                    obj.MINIMUM_AMOUNT = Min

                    Dim Max As String = clsCommon.myCstr(grow.Cells("MAXIMUM AMOUNT").Value)
                    If String.IsNullOrEmpty(Max) Then
                        Max = "0"
                    ElseIf Not IsNumeric(grow.Cells("MAXIMUM AMOUNT").Value) Or clsCommon.myCdbl(grow.Cells("MAXIMUM AMOUNT").Value) < 0 Then
                        Throw New Exception("Maximum Amount To Should be Numeric or in correct format On Line No " & LineNo & ".")
                    ElseIf Max.Length > 10 Then
                        Throw New Exception("Please check ! length of maximum amount should be max 10 digits on line no " & LineNo & ".")
                    End If
                    obj.MAXIMUM_AMOUNT = Max

                    obj.SaveData(obj, clsITSection.CheckNewEntry(obj.IT_SECTION_CODE, trans), trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
                trans.Rollback()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RmExport_Click(sender As Object, e As EventArgs) Handles RmExport.Click
        Dim str As String
        str = "Select IT_SECTION_CODE As [Code],Description As [Description],INCOME_TAX_ACT As [INCOME TAX ACT],MINIMUM_AMOUNT As [MINIMUM AMOUNT],MAXIMUM_AMOUNT As [MAXIMUM AMOUNT] from TSPL_IT_SECTION"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub FrmITSection_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
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
End Class
