Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Public Class frmDockMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub frmJWPriceCodeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        clsPortSetting.GetMachineType(cboSampleMachine1)
        clsPortSetting.GetMachineType(cboSampleMachine2)
        clsPortSetting.GetMachineType(cboSampleMachine3)
        clsPortSetting.GetMachineType(cboSampleMachine4)

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
            Dim obj As New clsDockMaster()
            obj.Code = txtCode.Value
            obj.Description = txtDescription.Text
            obj.MCC_Code = fndMCCMaster.Value

            obj.Default_Sample_Machine_1 = clsCommon.myCstr(cboSampleMachine1.SelectedValue)
            obj.Default_Sample_Machine_2 = clsCommon.myCstr(cboSampleMachine2.SelectedValue)
            obj.Default_Sample_Machine_3 = clsCommon.myCstr(cboSampleMachine3.SelectedValue)
            obj.Default_Sample_Machine_4 = clsCommon.myCstr(cboSampleMachine4.SelectedValue)

            obj.Default_Sample_Comport_1 = clsCommon.myCstr(CmbSampleComport1.Text)
            obj.Default_Sample_Comport_2 = clsCommon.myCstr(CmbSampleComport2.Text)
            obj.Default_Sample_Comport_3 = clsCommon.myCstr(CmbSampleComport3.Text)
            obj.Default_Sample_Comport_4 = clsCommon.myCstr(CmbSampleComport4.Text)


            If (clsDockMaster.SaveData(obj)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.Code, NavigatorType.Current)
            End If
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        rdbtndelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsDockMaster()
        obj = clsDockMaster.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False
            btnSave.Text = "Update"
            rdbtndelete.Enabled = True
            txtCode.Value = obj.Code
            txtDescription.Text = obj.Description
            fndMCCMaster.Value = obj.MCC_Code
            LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + fndMCCMaster.Value + "' "))

            CmbSampleComport1.Text = obj.Default_Sample_Comport_1
            cboSampleMachine1.SelectedValue = IIf(obj.Default_Sample_Machine_1 = "", "E", clsCommon.myCstr(obj.Default_Sample_Machine_1))

            CmbSampleComport2.Text = obj.Default_Sample_Comport_2
            cboSampleMachine2.SelectedValue = IIf(obj.Default_Sample_Machine_2 = "", "E", clsCommon.myCstr(obj.Default_Sample_Machine_2))

            CmbSampleComport3.Text = obj.Default_Sample_Comport_3
            cboSampleMachine3.SelectedValue = IIf(obj.Default_Sample_Machine_3 = "", "E", clsCommon.myCstr(obj.Default_Sample_Machine_3))

            CmbSampleComport4.Text = obj.Default_Sample_Comport_4
            cboSampleMachine4.SelectedValue = IIf(obj.Default_Sample_Machine_4 = "", "E", clsCommon.myCstr(obj.Default_Sample_Machine_4))
        End If
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Code")
            txtCode.Focus()
            Return False
        End If
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtDescription.Focus()
            Return False
        End If
        If clsCommon.myLen(txtCode.Value) > 20 Then
            clsCommon.MyMessageBoxShow("Length is greater then 20.")
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
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        ' Code Ends 
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsDockMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_DOCK_MASTER where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsDockMaster.getFinder("", txtCode.Value, isButtonClicked)
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtDescription.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        rdbtndelete.Enabled = False
        fndMCCMaster.Value = ""
        LblMccName.Text = ""

        cboSampleMachine1.SelectedValue = "E"
        cboSampleMachine2.SelectedValue = "E"
        cboSampleMachine3.SelectedValue = "E"
        cboSampleMachine4.SelectedValue = "E"
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
        str = "select Code,Description,MCC_Code as [MCC Code] from TSPL_DOCK_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description", "MCC Code") Then
            Try
                clsCommon.ProgressBarShow()
                Dim ii As Integer = 0
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii = ii + 1
                        Dim obj As New clsDockMaster()
                        obj.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Continue For
                        End If
                        If clsCommon.myLen(obj.Code) > 10 Then
                            Throw New Exception("length greater then 10.")
                        End If
                        obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        obj.MCC_Code = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                        If clsCommon.myLen(obj.MCC_Code) <= 0 Then
                            Throw New Exception("Please Enter MCC Code")
                        End If
                        obj.MCC_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Code from TSPL_MCC_MASTER where MCC_Code='" + obj.MCC_Code + "'"))
                        If clsCommon.myLen(obj.MCC_Code) <= 0 Then
                            Throw New Exception("Invalid MCC Code " + clsCommon.myCstr(grow.Cells("MCC Code").Value))
                        End If

                        If clsCommon.myLen(obj.Description) <= 0 Then
                            Throw New Exception("Description can not be blank or incorrect.")
                        End If
                        clsDockMaster.SaveData(obj)
                    Next
                Catch ex As Exception
                    Throw New Exception("At Row No" + clsCommon.myCstr(ii) + " " + ex.Message)
                End Try
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
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
                clsCommon.MyMessageBoxShow("Select code")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtCode.Value, "Code", "TSPL_DOCK_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub fndMCCMaster__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMCCMaster._MYValidating
        Try
            fndMCCMaster.Value = clsMccMaster.getFinder("", fndMCCMaster.Value, isButtonClicked)
            LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + fndMCCMaster.Value + "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
