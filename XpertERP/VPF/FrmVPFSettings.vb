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
Imports Microsoft.VisualBasic.PowerPacks
Imports System.Text.RegularExpressions
Imports common

Public Class FrmVPFSettings
    Inherits FrmMainTranScreen
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmVPFSettings)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub FrmVPFSettings_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim Color2 As New ColorDialog
        'Dim color1 As Integer = clsDBFuncationality.getSingleValue("Select marks From test Where id=1")
        'Dim myColor As Color
        'myColor = Color.FromArgb(color1)
        'LblOvalColor.BackColor = myColor
        'LblOvalBlinkColor.BackColor = myColor
        funReset()
    End Sub

    Private Sub BtnOvalBlinkColor_Click(sender As Object, e As EventArgs)
        'Me.ColorDialog1.ShowDialog()
        Me.ColorDialog1.AllowFullOpen = True


        Dim Color As New ColorDialog
        'If the user actually selected a color Then
        If Color.ShowDialog() = DialogResult.OK Then
            'Set the background color of the picture box = color selected from the color dialog
            LblOvalBlinkColor.BackColor = Color.Color
        End If

        Dim myColor As Color = LblOvalBlinkColor.BackColor  ' ColorDialog1.Color

        '' Create the ColorConverter.
        'Dim converter As System.ComponentModel.TypeConverter = _
        'System.ComponentModel.TypeDescriptor.GetConverter(myColor)
        'clsDBFuncationality.ExecuteNonQuery("Update test Set marks ='" & myColor.ToArgb() & "'")
    End Sub
    Private Sub BtnOvalColor_Click(sender As Object, e As EventArgs) Handles BtnOvalColor.Click
        Me.ColorDialog1.AllowFullOpen = True


        Dim Color As New ColorDialog
        If Color.ShowDialog() = DialogResult.OK Then
            LblOvalColor.BackColor = Color.Color
        End If

        Dim myColor As Color = LblOvalColor.BackColor

        'Dim converter As System.ComponentModel.TypeConverter = _
        'System.ComponentModel.TypeDescriptor.GetConverter(myColor)
        'clsDBFuncationality.ExecuteNonQuery("Update test Set marks ='" & myColor.ToArgb() & "'")
    End Sub
    Function AllowToSave() As Boolean
        Try

            If clsCommon.myLen(TxtModuleCode.Value) <= 0 Then
                myMessages.blankValue("Module code")
                TxtModuleCode.Focus()
                TxtModuleCode.Select()
                Errorcontrol.SetError(TxtModuleCode, "Module code")
                Return False
            Else
                Errorcontrol.ResetError(TxtModuleCode)
            End If


            Return True
        Catch ex As Exception
            myMessages.myExceptions(ex)
            Return True
        End Try
    End Function
    Public Sub Save()

        Try
            If AllowToSave() Then

                Dim arr As New List(Of ClsVPFSettings)
                Dim obj As New ClsVPFSettings()

                Dim OvlColor As Color = LblOvalColor.BackColor
                Dim OvlBlinkColor As Color = LblOvalBlinkColor.BackColor

                obj.Program_Code = clsCommon.myCstr(TxtModuleCode.Value)
                obj.Oval_Color = clsCommon.myCdbl(OvlColor.ToArgb())
                obj.Oval_Blink_Color = clsCommon.myCdbl(OvlBlinkColor.ToArgb())

                If ChkApplicableForAll.IsChecked = True Then
                    obj.Is_ColorAppliedForAll = 1
                Else
                    obj.Is_ColorAppliedForAll = 0
                End If

                If ChkOvalUnderOval.IsChecked = True Then
                    obj.Oval_Under_Oval = 1
                Else
                    obj.Oval_Under_Oval = 0
                End If

                arr.Add(obj)
                If (ClsVPFSettings.SaveData(obj)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Program_Code, NavigatorType.Current)
                    btnsave.Text = "Update"
                Else
                    btnsave.Text = "Save"
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Try
            Dim OvalColor As Color
            Dim OvalBlinkColor As Color

            TxtModuleCode.MyReadOnly = True
            btnsave.Enabled = True
            Dim obj As New ClsVPFSettings()
            obj = ClsVPFSettings.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Program_Code) > 0) Then
                funReset()
                btnsave.Text = "Update"

                TxtModuleCode.Value = obj.Program_Code

                If obj.Is_ColorAppliedForAll = 1 Then
                    ChkApplicableForAll.Checked = True
                Else
                    ChkApplicableForAll.Checked = False
                End If
                If obj.Oval_Under_Oval = 1 Then
                    ChkOvalUnderOval.Checked = True
                Else
                    ChkOvalUnderOval.Checked = False
                End If

                OvalColor = Color.FromArgb(obj.Oval_Color)
                OvalBlinkColor = Color.FromArgb(obj.Oval_Blink_Color)
                LblOvalColor.BackColor = OvalColor
                LblOvalBlinkColor.BackColor = OvalBlinkColor
                TxtModuleCode.MyReadOnly = True
            Else
                funReset()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub funReset()
        TxtModuleCode.MyReadOnly = False
        TxtModuleCode.Value = Nothing
        TxtModuleCode.Focus()
        ChkApplicableForAll.Checked = False
        ChkOvalUnderOval.Checked = False
        LblOvalColor.Text = ""
        LblOvalBlinkColor.Text = ""
        'LblOvalColor.Size = New Size(12, 16)
        'LblOvalBlinkColor.Size = New Size(12, 16)
    End Sub

    Private Sub BtnOvalBlinkColor_Click1(sender As Object, e As EventArgs) Handles BtnOvalBlinkColor.Click
        Me.ColorDialog1.AllowFullOpen = True

        Dim Color As New ColorDialog
        If Color.ShowDialog() = DialogResult.OK Then
            LblOvalBlinkColor.BackColor = Color.Color
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub TxtModuleCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtModuleCode._MYValidating
        Dim qry As String = ""
        qry = "Select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name From TSPL_PROGRAM_MASTER"
        TxtModuleCode.Value = clsCommon.ShowSelectForm("VPFModCod", qry, "Code", " Type='M' AND Program_Code <>'MUtility' ", TxtModuleCode.Value, "Code", isButtonClicked)
        If clsCommon.myLen(TxtModuleCode.Value) > 0 Then
            LblModuleName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name END AS Name  From TSPL_PROGRAM_MASTER Where Program_Code='" + TxtModuleCode.Value + "'"))
            LoadData(TxtModuleCode.Value, NavigatorType.Current)
            LblVPFScreenCode.Text = LoadVPFScreenCodes(TxtModuleCode.Value)
        End If
    End Sub
    Function LoadVPFScreenCodes(ByVal Program_Code As String) As String
        Dim VPFCode As String = String.Empty
        If clsCommon.CompairString(Program_Code, "MGenLedger") = CompairStringResult.Equal Then
            VPFCode = clsUserMgtCode.FrmGLCycle
        ElseIf clsCommon.CompairString(Program_Code, "MCommSer") = CompairStringResult.Equal Then
            VPFCode = clsUserMgtCode.FrmCommonCycle
        ElseIf clsCommon.CompairString(Program_Code, "MPayable") = CompairStringResult.Equal Then
            VPFCode = clsUserMgtCode.FrmAPCycle
        ElseIf clsCommon.CompairString(Program_Code, "MReceivable") = CompairStringResult.Equal Then
            VPFCode = clsUserMgtCode.FrmARCycle
        ElseIf clsCommon.CompairString(Program_Code, "MBulkSale") = CompairStringResult.Equal Then
            VPFCode = clsUserMgtCode.FrmBulkSaleCycle
        ElseIf clsCommon.CompairString(Program_Code, "MFreshSale") = CompairStringResult.Equal Then
            VPFCode = clsUserMgtCode.FrmFreshSaleCycle
        ElseIf clsCommon.CompairString(Program_Code, "MProductSale") = CompairStringResult.Equal Then
            VPFCode = clsUserMgtCode.FrmProductSaleCycle
        ElseIf clsCommon.CompairString(Program_Code, "MCSASale") = CompairStringResult.Equal Then
            VPFCode = clsUserMgtCode.FrmCSASaleCycle
        ElseIf clsCommon.CompairString(Program_Code, "MMaterial") = CompairStringResult.Equal Then
            VPFCode = clsUserMgtCode.FrmMMCycle
        ElseIf clsCommon.CompairString(Program_Code, "MPurchase") = CompairStringResult.Equal Then
            VPFCode = clsUserMgtCode.FrmPurchaseCycle
        ElseIf clsCommon.CompairString(Program_Code, "MMMProc") = CompairStringResult.Equal Then
            VPFCode = clsUserMgtCode.FrmMCCProcurementCycle
        ElseIf clsCommon.CompairString(Program_Code, "MMBProc") = CompairStringResult.Equal Then
            VPFCode = clsUserMgtCode.FrmBulkProcurementCycle
        ElseIf clsCommon.CompairString(Program_Code, "MProdDairy") = CompairStringResult.Equal Then
            VPFCode = clsUserMgtCode.FrmDProductionCycle
        End If
        Return VPFCode
    End Function
End Class