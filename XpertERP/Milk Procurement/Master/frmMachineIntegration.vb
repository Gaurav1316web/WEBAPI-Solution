Imports common
Imports System.Data.SqlClient
Public Class frmMachineIntegration
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
#End Region
    Sub Reset()

        '' Resetting Common Controls
        fndCode.Value = ""
        txtdesc.Text = ""
        chkEWS.IsChecked = True
        chkAnalyzer.IsChecked = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        fndCode.MyReadOnly = False
        isNewEntry = True
        txtInput.Text = ""
        ''Resetting Controls For EWS Tab
        chkIsContinuousReading.Checked = False
        txtStartStopSymbol.Text = ""
        txtStartStopSymbol.Enabled = False
        txtDataSample.Text = ""
        txtIntFromPos.Text = ""
        txtIntNoOfChar.Text = ""
        txtFracStartPos.Text = ""
        txtFracNoOfChar.Text = ""

    End Sub
    Private Sub FrmMachineIntegration_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            btnnew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub
    Private Sub frmMachineIntegration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Function AllowToSave() As Boolean
        Try
            

            If chkEWS.IsChecked Then

                If clsCommon.myLen(txtdesc.Text) <= 0 Then
                    txtdesc.Focus()
                    Throw New Exception("Please Fill Description")
                    Return False
                End If

                If clsCommon.myLen(txtDataSample.Text) <= 0 Then
                    txtDataSample.Focus()
                    Throw New Exception("Please Fill Data Sample")
                    Return False
                End If

                If chkIsContinuousReading.Checked Then
                    If clsCommon.myLen(txtStartStopSymbol.Text) <= 0 Then
                        txtStartStopSymbol.Focus()
                        Throw New Exception("Please Fill Start Stop Symbol")
                        Return False
                    End If

                    If Not txtDataSample.Text.Contains(txtStartStopSymbol.Text) Then
                        txtStartStopSymbol.Focus()
                        Throw New Exception("Please Fill Correct Start Stop Symbol, The Symbol you provided Coud not found in Sample")
                        Return False
                    End If
                End If
                If clsCommon.myLen(txtIntFromPos.Text) <= 0 Then
                    txtIntFromPos.Focus()
                    Throw New Exception("Please Fill Integer Part From Position")
                    Return False
                End If

                If Not IsNumeric(txtIntFromPos.Text) Then
                    txtIntFromPos.Focus()
                    Throw New Exception("'Integer Part From Position' Must be a number")
                    Return False
                End If

                If clsCommon.myCdbl(txtIntFromPos.Text) <= 0 Then
                    txtIntFromPos.Focus()
                    Throw New Exception("'Integer Part From Position' Must be a number Greater Than Zero")
                    Return False
                End If

                If clsCommon.myCdbl(txtIntFromPos.Text) > clsCommon.myLen(txtDataSample.Text) Then
                    txtIntFromPos.Focus()
                    Throw New Exception("'Integer Part From Position'  must not be greater than sample length")
                    Return False
                End If


                If clsCommon.myLen(txtIntNoOfChar.Text) <= 0 Then
                    txtIntFromPos.Focus()
                    Throw New Exception("Please Fill Integer Part No. Of Character")
                    Return False
                End If

                If Not IsNumeric(txtIntNoOfChar.Text) Then
                    txtIntNoOfChar.Focus()
                    Throw New Exception("'Integer Part No. Of Character' Must be a number")
                    Return False
                End If

                If clsCommon.myCdbl(txtIntNoOfChar.Text) <= 0 Then
                    txtIntNoOfChar.Focus()
                    Throw New Exception("'Integer Part No. Of Character' Must be a number Greater Than Zero")
                    Return False
                End If

                If clsCommon.myCdbl(txtIntNoOfChar.Text) > clsCommon.myLen(txtDataSample.Text) Then
                    txtIntNoOfChar.Focus()
                    Throw New Exception("'Integer Part No. Of Character' must not be greater than sample length")
                    Return False
                End If

                If (clsCommon.myCdbl(txtIntNoOfChar.Text) + clsCommon.myCdbl(txtIntFromPos.Text) - 1) > clsCommon.myLen(txtDataSample.Text) Then
                    txtIntNoOfChar.Focus()
                    Throw New Exception("Invalid Integer part Specification according to sample Data")
                    Return False
                End If

                If clsCommon.myLen(txtFracStartPos.Text) <= 0 Then
                    txtFracStartPos.Focus()
                    Throw New Exception("Please Fill Fractional Part From Position")
                    Return False
                End If

                If Not IsNumeric(txtFracStartPos.Text) Then
                    txtFracStartPos.Focus()
                    Throw New Exception("'Fractional Part From Position' Must be a number")
                    Return False
                End If

                If clsCommon.myCdbl(txtFracStartPos.Text) <= 0 Then
                    txtFracStartPos.Focus()
                    Throw New Exception("'Fractional Part From Position' Must be a number Greater Than Zero")
                    Return False
                End If

                If clsCommon.myCdbl(txtFracStartPos.Text) > clsCommon.myLen(txtDataSample.Text) Then
                    txtFracStartPos.Focus()
                    Throw New Exception("'Fractional Part From Position'  must not be greater than sample length")
                    Return False
                End If


                If clsCommon.myLen(txtFracNoOfChar.Text) <= 0 Then
                    txtFracStartPos.Focus()
                    Throw New Exception("Please Fill Fractional Part No. Of Character")
                    Return False
                End If

                If Not IsNumeric(txtFracStartPos.Text) Then
                    txtFracStartPos.Focus()
                    Throw New Exception("'Fractional Part No. Of Character' Must be a number")
                    Return False
                End If

                If clsCommon.myCdbl(txtFracStartPos.Text) <= 0 Then
                    txtFracStartPos.Focus()
                    Throw New Exception("'Fractional Part No. Of Character' Must be a number Greater Than Zero")
                    Return False
                End If

                If clsCommon.myCdbl(txtFracStartPos.Text) > clsCommon.myLen(txtDataSample.Text) Then
                    txtFracStartPos.Focus()
                    Throw New Exception("'Fractional Part No. Of Character' must not be greater than sample length")
                    Return False
                End If
                Dim PrevString As String = ""
                Dim intPart As String = Microsoft.VisualBasic.Mid(txtDataSample.Text, clsCommon.myCdbl(txtIntFromPos.Text), clsCommon.myCdbl(txtIntNoOfChar.Text))
                Dim fracPart As String = Microsoft.VisualBasic.Mid(txtDataSample.Text, clsCommon.myCdbl(txtFracStartPos.Text), clsCommon.myCdbl(txtFracNoOfChar.Text))
                PrevString = IIf(clsCommon.myCdbl(intPart) <= 9 And clsCommon.myCdbl(intPart) > 0, "0" & clsCommon.myCdbl(intPart), clsCommon.myCdbl(intPart)) & "." & IIf(clsCommon.myCdbl(fracPart) <= 9, clsCommon.myCdbl(fracPart) & "0", clsCommon.myCdbl(fracPart))
                If clsCommon.MyMessageBoxShow(Me, "Preview according to Your Data and Specification is : " & PrevString, Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    Return True
                Else
                    Return False
                End If
            Else
                Throw New Exception("This Part is Under-development Can't Save")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Sub SaveData()
        Dim trans As SqlTransaction = Nothing
        Try
            Dim obj As New clsMachineIntegration()
            obj.Code = clsCommon.myCstr(fndCode.Value)
            obj.Description = clsCommon.myCstr(txtdesc.Text)
            obj.Type = IIf(chkEWS.IsChecked, 0, 1)
            obj.isContinuousDataReading = IIf(chkIsContinuousReading.Checked, 1, 0)
            obj.StartStopCharacter = txtStartStopSymbol.Text
            obj.DataSample = txtDataSample.Text
            obj.IntFromPos = clsCommon.myCdbl(txtIntFromPos.Text)
            obj.IntNoOfChar = clsCommon.myCdbl(txtIntNoOfChar.Text)
            obj.FracFromPos = clsCommon.myCdbl(txtFracStartPos.Text)
            obj.FracNoOfChar = clsCommon.myCdbl(txtFracNoOfChar.Text)
            obj.Input_String = txtInput.Text
            obj.Check_For_Zero = chkCheckForZero.Checked
            trans = clsDBFuncationality.GetTransactin()
            If clsMachineIntegration.SaveData(obj, isNewEntry, trans) Then
                clsDBFuncationality.ExecuteNonQuery("update TSPL_MACHINE_INTEGRATION set DataSample = '" & txtDataSample.Text & "' where  Code='" & obj.Code & "' ", trans)
                trans.Commit()
                If isNewEntry Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If
                loadData(obj.Code, NavigatorType.Current)
            Else
                trans.Rollback()
                isNewEntry = True
                btnsave.Text = "Save"
                fndCode.MyReadOnly = False
                btndelete.Enabled = False
            End If
        Catch ex As Exception
            Try
                trans.Rollback()
            Catch ex1 As Exception
            End Try
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub loadData(strCode As String, nav As NavigatorType)
        Try
            Dim obj As New clsMachineIntegration
            obj = clsMachineIntegration.GetData(strCode, nav, Nothing)
            If obj IsNot Nothing Then
                isNewEntry = False
                fndCode.Value = obj.Code
                txtdesc.Text = obj.Description
                If obj.Type = 1 Then
                    chkAnalyzer.IsChecked = True
                Else
                    chkEWS.IsChecked = True
                End If
                txtDataSample.Text = obj.DataSample
                If obj.isContinuousDataReading = 1 Then
                    chkIsContinuousReading.Checked = True
                End If
                txtStartStopSymbol.Text = obj.StartStopCharacter
                txtIntFromPos.Text = obj.IntFromPos
                txtIntNoOfChar.Text = obj.IntNoOfChar
                txtFracStartPos.Text = obj.FracFromPos
                txtFracNoOfChar.Text = obj.FracNoOfChar
                txtInput.Text = obj.Input_String
                chkCheckForZero.Checked = obj.Check_For_Zero
                btnsave.Text = "Update"
                btndelete.Enabled = True
                fndCode.MyReadOnly = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub
    Private Sub chkEWS_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkEWS.ToggleStateChanged
        If chkEWS.IsChecked Then
            RadPageView1.SelectedPage = RadPageViewPage1
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
        End If
    End Sub

    Private Sub chkAnalyzer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAnalyzer.ToggleStateChanged
        If chkEWS.IsChecked Then
            RadPageView1.SelectedPage = RadPageViewPage1
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
        End If
    End Sub

    Private Sub chkIsContinuousReading_CheckStateChanged(sender As Object, e As EventArgs) Handles chkIsContinuousReading.CheckStateChanged
        If chkIsContinuousReading.Checked Then
            txtStartStopSymbol.Text = ""
            txtStartStopSymbol.Enabled = True
        Else
            txtStartStopSymbol.Text = ""
            txtStartStopSymbol.Enabled = False
        End If
    End Sub

    Private Sub fndCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndCode._MYNavigator
        loadData(fndCode.Value, NavType)
    End Sub

    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating
        fndCode.Value = clsMachineIntegration.getFinder("", fndCode.Value, isButtonClicked)
        loadData(fndCode.Value, NavigatorType.Current)
    End Sub
End Class