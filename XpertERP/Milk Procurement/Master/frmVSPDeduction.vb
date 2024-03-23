Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Public Class frmVSPDeduction
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
        Try
            If AllowToSave() Then
                Dim obj As New clsVSPDeduction()
                obj.Deduction_Code = txtCode.Value
                obj.Deduction_Name = txtDescription.Text
                If rbtnRate.IsChecked Then
                    obj.Deduction_On = 0
                Else
                    obj.Deduction_On = 1
                End If
                obj.Deduction_Rate = txtDeductionRate.Value
                obj.Deduction_Minimum_FAT_Per = txtDeductionMinimumFATPer.Value
                obj.Deduction_Minimum_SNF_Per = txtDeductionMinimumSNFPer.Value
                obj.Deduction_No_Of_Payment_Cycle_For_New_VSP = txtDeductionNoOfPaymentCycleForNewVSP.Value
                If (clsVSPDeduction.SaveData(obj)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Deduction_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        rdbtndelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsVSPDeduction()
        obj = clsVSPDeduction.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Deduction_Code) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            rdbtndelete.Enabled = True
            txtCode.Value = obj.Deduction_Code
            txtDescription.Text = obj.Deduction_Name
            If obj.Deduction_On = 0 Then
                rbtnRate.IsChecked = True
            Else
                rbtnPercent.IsChecked = True
            End If
            SetText()
            txtDeductionRate.Value = obj.Deduction_Rate
            txtDeductionMinimumFATPer.Value = obj.Deduction_Minimum_FAT_Per
            txtDeductionMinimumSNFPer.Value = obj.Deduction_Minimum_SNF_Per
            txtDeductionNoOfPaymentCycleForNewVSP.Value = obj.Deduction_No_Of_Payment_Cycle_For_New_VSP
        End If
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Deduction_Code", Me.Text)
            txtCode.Focus()
            Return False
        End If
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue(Me, "Deduction_Name", Me.Text)
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
        ' Deduction_Code Ends 
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsVSPDeduction.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_VSP_DEDUCTION_MASTER where Deduction_Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsVSPDeduction.getFinder("", txtCode.Value, isButtonClicked)
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
        txtDeductionRate.Value = 0
        txtDeductionMinimumFATPer.Value = 0
        txtDeductionMinimumSNFPer.Value = 0
        txtDeductionNoOfPaymentCycleForNewVSP.Value = 0
        rbtnRate.IsChecked = True
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
        str = "select Deduction_Code as Code,Deduction_Name as Name,case when isnull(Deduction_On,0)=0 then 'Rate' else 'Percent' end as [Apply On] ,Deduction_Rate as Rate,Deduction_Minimum_FAT_Per as [Minimum FAT%],Deduction_Minimum_SNF_Per as [Minimum SNF%],Deduction_No_Of_Payment_Cycle_For_New_VSP as [No Of Payment Cycle For New VSP] from TSPL_VSP_DEDUCTION_MASTER"
        ListImpExpColumnsMandatory = New List(Of String)({"Code", "Name", "Rate"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Name", "Apply On", "Rate", "Minimum FAT%", "Minimum SNF%", "No Of Payment Cycle For New VSP") Then
            Try
                clsCommon.ProgressBarShow()
                Dim ii As Integer = 0
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii = ii + 1
                        Dim obj As New clsVSPDeduction()
                        obj.Deduction_Code = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(obj.Deduction_Code) <= 0 Then
                            Continue For
                        End If
                        If clsCommon.myLen(obj.Deduction_Code) > 10 Then
                            Throw New Exception("length greater then 10.")
                        End If
                        obj.Deduction_Name = clsCommon.myCstr(grow.Cells("Name").Value)
                        If clsCommon.myLen(obj.Deduction_Name) <= 0 Then
                            Throw New Exception("Deduction_Name can not be blank or incorrect.")
                        End If
                        If (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Apply On").Value), "Rate") = CompairStringResult.Equal) Then
                            obj.Deduction_On = 0
                        ElseIf (clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Apply On").Value), "Percent") = CompairStringResult.Equal) Then
                            obj.Deduction_On = 1
                        Else
                            Throw New Exception("Apply On Should be Rate/Percent")
                        End If
                        obj.Deduction_Rate = clsCommon.myCdbl(grow.Cells("Rate").Value)
                        If obj.Deduction_Rate <= 0 Then
                            Throw New Exception("Invalid Rate " + clsCommon.myCstr(obj.Deduction_Rate))
                        End If
                        obj.Deduction_Minimum_FAT_Per = clsCommon.myCdbl(grow.Cells("Minimum FAT%").Value)
                        obj.Deduction_Minimum_SNF_Per = clsCommon.myCdbl(grow.Cells("Minimum SNF%").Value)
                        obj.Deduction_No_Of_Payment_Cycle_For_New_VSP = clsCommon.myCdbl(grow.Cells("No Of Payment Cycle For New VSP").Value)
                        clsVSPDeduction.SaveData(obj)
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
                clsCommon.MyMessageBoxShow(Me, "Select Deduction_Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtCode.Value, "Deduction_Code", "TSPL_VSP_DEDUCTION_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub rbtnRate_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnRate.ToggleStateChanged

    End Sub

    Private Sub rbtnPercent_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnPercent.ToggleStateChanged
        SetText()
    End Sub

    Private Sub SetText()
        If rbtnRate.IsChecked Then
            MyLabel85.Text = "VSP Deduction Rate"
        Else
            MyLabel85.Text = "VSP Deduction %"
        End If
    End Sub
End Class