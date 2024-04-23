
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class frmBullMasters
    Inherits FrmMainTranScreen
    Dim isNewEntry As Boolean = True
    Dim ErrorControl As New clsErrorControl()
    Private Sub fndSpecies__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSpecies._MYValidating
        Try

            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SPECIES_MASTER  "
            'Emp_type = 'Salesman' and Emp_Status = 'Active'
            fndSpecies.Value = clsCommon.ShowSelectForm("Species", qry, "Code", "", fndSpecies.Value, "Code", isButtonClicked)
            ' txtSupervisorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  Emp_Name   from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + fndSupervisorCode.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndCategory__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCategory._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_CATEGORY_MASTER  "
            fndCategory.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndCategory.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndCountry__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub fndSubCategory__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSubCategory._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SUB_CATEGORY_MASTER  "
            fndSubCategory.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndSubCategory.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndSSCentre__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSSCentre._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SS_CENTRE_MASTER  "
            fndSSCentre.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndSSCentre.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndBreed__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBreed._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_BREED_MASTER  "
            fndBreed.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndBreed.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndShedId__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndShedId._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SHED_MASTER  "
            fndShedId.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndShedId.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndPenId__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPenId._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_PEN_ID_MASTER  "
            fndPenId.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndPenId.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndBullStatus__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBullStatus._MYValidating
        Try
            Dim qry As String = " select STSTUS_Code as Code, Name  as Name from TSPL_BULL_STATUS_MASTER  "
            fndBullStatus.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndBullStatus.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndSubStatus__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSubStatus._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SUB_STATUS_MASTER  "
            fndSubStatus.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndSubStatus.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating
        Try
            Dim qry As String = "select * from TSPL_BULL_MASTER "

            fndCode.Value = clsCommon.ShowSelectForm("Code@Finder", qry, "Code", "", fndCode.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    End Sub




    'Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
    '    Try
    '        isNewEntry = True
    '        btnDelete.Enabled = False
    '        btnSave.Enabled = True
    '        btnSave.Text = "Save"
    '        fndCode.MyReadOnly = False

    '        Dim obj As clsBullMasters = clsBullMasters.GetData(strCode, NavTyep)
    '        If obj IsNot Nothing Then
    '            isNewEntry = False
    '            fndCode.Value = obj.Code
    '            fndCategory.Value = obj.Category_Code
    '            fndSpecies.Value = obj.Species_Code
    '            'txtname.Text = obj.Name
    '            fndCode.MyReadOnly = True
    '            'btnsave.Text = "Update"
    '            btnDelete.Enabled = True
    '        Else
    '            'AddNew()
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
End Class