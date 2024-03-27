Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Public Class frmVSPDayWiseIncentive
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub frmJWPriceCodeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update")
        ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D  for Delete")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New")
        funReset()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub Save()
        If AllowToSave() Then
            Dim obj As New clsVSSDayWiseIncentive()
            obj.Day_Wise_Incentive_Code = txtCode.Value
            obj.Day_Wise_Incentive_Name = txtDescription.Text

            obj.Day_Wise_Incentive_From_1 = txtDWIFrom1.Value
            obj.Day_Wise_Incentive_From_2 = txtDWIFrom2.Value
            obj.Day_Wise_Incentive_From_3 = txtDWIFrom3.Value
            obj.Day_Wise_Incentive_From_4 = txtDWIFrom4.Value
            obj.Day_Wise_Incentive_From_5 = txtDWIFrom5.Value


            obj.Day_Wise_Incentive_To_1 = txtDWITo1.Value
            obj.Day_Wise_Incentive_To_2 = txtDWITo2.Value
            obj.Day_Wise_Incentive_To_3 = txtDWITo3.Value
            obj.Day_Wise_Incentive_To_4 = txtDWITo4.Value
            obj.Day_Wise_Incentive_To_5 = txtDWITo5.Value

            obj.Day_Wise_Incentive_Rate_1 = txtDWIRate1.Value
            obj.Day_Wise_Incentive_Rate_2 = txtDWIRate2.Value
            obj.Day_Wise_Incentive_Rate_3 = txtDWIRate3.Value
            obj.Day_Wise_Incentive_Rate_4 = txtDWIRate4.Value
            obj.Day_Wise_Incentive_Rate_5 = txtDWIRate5.Value


            If (clsVSSDayWiseIncentive.SaveData(obj)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.Day_Wise_Incentive_Code, NavigatorType.Current)
            End If
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        rdbtndelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsVSSDayWiseIncentive()
        obj = clsVSSDayWiseIncentive.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Day_Wise_Incentive_Code) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            rdbtndelete.Enabled = True
            txtCode.Value = obj.Day_Wise_Incentive_Code
            txtDescription.Text = obj.Day_Wise_Incentive_Name
            txtDWIFrom1.Value = obj.Day_Wise_Incentive_From_1
            txtDWIFrom2.Value = obj.Day_Wise_Incentive_From_2
            txtDWIFrom3.Value = obj.Day_Wise_Incentive_From_3
            txtDWIFrom4.Value = obj.Day_Wise_Incentive_From_4
            txtDWIFrom5.Value = obj.Day_Wise_Incentive_From_5
            txtDWITo1.Value = obj.Day_Wise_Incentive_To_1
            txtDWITo2.Value = obj.Day_Wise_Incentive_To_2
            txtDWITo3.Value = obj.Day_Wise_Incentive_To_3
            txtDWITo4.Value = obj.Day_Wise_Incentive_To_4
            txtDWITo5.Value = obj.Day_Wise_Incentive_To_5
            txtDWIRate1.Value = obj.Day_Wise_Incentive_Rate_1
            txtDWIRate2.Value = obj.Day_Wise_Incentive_Rate_2
            txtDWIRate3.Value = obj.Day_Wise_Incentive_Rate_3
            txtDWIRate4.Value = obj.Day_Wise_Incentive_Rate_4
            txtDWIRate5.Value = obj.Day_Wise_Incentive_Rate_5
        End If
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Day_Wise_Incentive_Code", Me.Text)
            txtCode.Focus()
            Return False
        End If
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue(Me, "Day_Wise_Incentive_Name", Me.Text)
            txtDescription.Focus()
            Return False
        End If
        If clsCommon.myLen(txtCode.Value) > 20 Then
            clsCommon.MyMessageBoxShow(Me, "Length is greater then 20.", Me.Text)
            txtCode.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        ' Day_Wise_Incentive_Code Ends 
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsVSSDayWiseIncentive.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER where Day_Wise_Incentive_Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsVSSDayWiseIncentive.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtDescription.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        rdbtndelete.Enabled = False
        txtDWIFrom1.Value = 0
        txtDWIFrom2.Value = 0
        txtDWIFrom3.Value = 0
        txtDWIFrom4.Value = 0
        txtDWIFrom5.Value = 0
        txtDWITo1.Value = 0
        txtDWITo2.Value = 0
        txtDWITo3.Value = 0
        txtDWITo4.Value = 0
        txtDWITo5.Value = 0
        txtDWIRate1.Value = 0
        txtDWIRate2.Value = 0
        txtDWIRate3.Value = 0
        txtDWIRate4.Value = 0
        txtDWIRate5.Value = 0
    End Sub

    Private Sub frmHSNMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnreset.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        Import.Enabled = MyBase.isModifyFlag
        Export.Enabled = MyBase.isModifyFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnreset_Click(sender As Object, e As EventArgs) Handles rdbtnreset.Click
        funReset()
    End Sub

    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        Dim str As String
        str = "select Day_Wise_Incentive_Code as Code,Day_Wise_Incentive_Name as Name," + Environment.NewLine +
            "Day_Wise_Incentive_From_1 as [From 1],Day_Wise_Incentive_To_1 as [To 1],Day_Wise_Incentive_Rate_1 as [Rate 1]," + Environment.NewLine +
            "Day_Wise_Incentive_From_2 as [From 2],Day_Wise_Incentive_To_2 as [To 2],Day_Wise_Incentive_Rate_2 as [Rate 2]," + Environment.NewLine +
            "Day_Wise_Incentive_From_3 as [From 3],Day_Wise_Incentive_To_3 as [To 3],Day_Wise_Incentive_Rate_3 as [Rate 3]," + Environment.NewLine +
            "Day_Wise_Incentive_From_4 as [From 4],Day_Wise_Incentive_To_4 as [To 4],Day_Wise_Incentive_Rate_4 as [Rate 4]," + Environment.NewLine +
            "Day_Wise_Incentive_From_5 as [From 5],Day_Wise_Incentive_To_5 as [To 5],Day_Wise_Incentive_Rate_5 as [Rate 5] from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Name", "Rate", "From 1", "To 1", "Rate 1", "From 2", "To 2", "Rate 2", "From 3", "To 3", "Rate 3", "From 4", "To 4", "Rate 4", "From 5", "To 5", "Rate 5") Then
            Try
                clsCommon.ProgressBarShow()
                Dim ii As Integer = 0
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii = ii + 1
                        Dim obj As New clsVSSDayWiseIncentive()
                        obj.Day_Wise_Incentive_Code = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(obj.Day_Wise_Incentive_Code) <= 0 Then
                            Continue For
                        End If
                        If clsCommon.myLen(obj.Day_Wise_Incentive_Code) > 10 Then
                            Throw New Exception("length greater then 10.")
                        End If
                        obj.Day_Wise_Incentive_Name = clsCommon.myCstr(grow.Cells("Name").Value)
                        If clsCommon.myLen(obj.Day_Wise_Incentive_Name) <= 0 Then
                            Throw New Exception("Day_Wise_Incentive_Name can not be blank or incorrect.")
                        End If
                        obj.Day_Wise_Incentive_From_1 = clsCommon.myCdbl(grow.Cells("From 1").Value)
                        obj.Day_Wise_Incentive_To_1 = clsCommon.myCdbl(grow.Cells("To 1").Value)
                        obj.Day_Wise_Incentive_Rate_1 = clsCommon.myCdbl(grow.Cells("Rate 1").Value)

                        obj.Day_Wise_Incentive_From_2 = clsCommon.myCdbl(grow.Cells("From 2").Value)
                        obj.Day_Wise_Incentive_To_2 = clsCommon.myCdbl(grow.Cells("To 2").Value)
                        obj.Day_Wise_Incentive_Rate_2 = clsCommon.myCdbl(grow.Cells("Rate 2").Value)

                        obj.Day_Wise_Incentive_From_3 = clsCommon.myCdbl(grow.Cells("From 3").Value)
                        obj.Day_Wise_Incentive_To_3 = clsCommon.myCdbl(grow.Cells("To 3").Value)
                        obj.Day_Wise_Incentive_Rate_3 = clsCommon.myCdbl(grow.Cells("Rate 3").Value)

                        obj.Day_Wise_Incentive_From_4 = clsCommon.myCdbl(grow.Cells("From 4").Value)
                        obj.Day_Wise_Incentive_To_4 = clsCommon.myCdbl(grow.Cells("To 4").Value)
                        obj.Day_Wise_Incentive_Rate_4 = clsCommon.myCdbl(grow.Cells("Rate 4").Value)

                        obj.Day_Wise_Incentive_From_5 = clsCommon.myCdbl(grow.Cells("From 5").Value)
                        obj.Day_Wise_Incentive_To_5 = clsCommon.myCdbl(grow.Cells("To 5").Value)
                        obj.Day_Wise_Incentive_Rate_5 = clsCommon.myCdbl(grow.Cells("Rate 5").Value)

                        clsVSSDayWiseIncentive.SaveData(obj)
                    Next
                Catch ex As Exception
                    Throw New Exception("At Row No" + clsCommon.myCstr(ii) + " " + ex.Message)
                End Try
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Day_Wise_Incentive_Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtCode.Value, "Day_Wise_Incentive_Code", "TSPL_VSP_DAY_WISE_INCENTIVE_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class