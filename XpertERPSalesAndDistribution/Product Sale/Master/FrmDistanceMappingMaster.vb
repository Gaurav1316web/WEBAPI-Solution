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
Public Class FrmDistanceMappingMaster
    'Inherits FrmMainTranScreen
    'Dim ButtonToolTip As ToolTip = New ToolTip()
    'Dim isInsideLoadData As Boolean = False
    'Dim isnewentry As Boolean = False
    'Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    'Private Sub SetUserMgmtNew()
    '    'MyBase.SetUserMgmt(clsUserMgtCode.FrmExpiryDate)
    '    If Not (MyBase.isReadFlag) Then
    '        common.clsCommon.MyMessageBoxShow("Permission Denied")
    '        Me.Close()
    '        Exit Sub
    '    End If
    '    btnsave.Visible = MyBase.isModifyFlag
    '    btndelete.Visible = MyBase.isDeleteFlag
    'End Sub
    'Sub AddNew()

    '    fndCode.Value = ""
    '    fndFromLocation.Value = ""
    '    lblFromLocationName.Text = ""
    '    fndToCity.Value = ""
    '    lblToCityName.Text = ""
    '    txtVehicleCapacity.Text = ""
    '    txtFreight.Text = ""
    '    txtCalculation.Text = ""
    '    'txtDate.Text = clsCommon.GETSERVERDATE()
    '    'txtActiveDate.Text = ""
    '    btnsave.Text = "Save"
    '    fndCode.MyReadOnly = False
    'End Sub
    'Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click

    'End Sub

    'Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click

    'End Sub

    'Private Sub fndCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCode._MYNavigator

    'End Sub

    'Private Sub fndCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCode._MYValidating

    'End Sub

    'Private Sub fndFromLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndFromLocation._MYValidating
    '    Dim qry As String = "SELECT Location_Code ,Location_Desc  FROM TSPL_LOCATION_MASTER "
    '    fndFromLocation.Value = clsCommon.ShowSelectForm("Location", qry, "Location_Code", "", fndFromLocation.Value, "Location_Code", isButtonClicked)
    '    If clsCommon.myLen(fndFromLocation.Value) > 0 Then
    '        lblFromLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER Where Location_Code='" + fndFromLocation.Value + "'"))
    '    Else
    '        lblFromLocationName.Text = ""
    '    End If
    'End Sub

    'Private Sub fndToCity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndToCity._MYValidating
    '    Dim qry As String = "SELECT City_Code,City_Name  FROM TSPL_CITY_MASTER "
    '    fndToCity.Value = clsCommon.ShowSelectForm("City", qry, "City_Code", "", fndToCity.Value, "City_Code", isButtonClicked)
    '    If clsCommon.myLen(fndToCity.Value) > 0 Then
    '        lblToCityName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT City_Name  FROM TSPL_CITY_MASTER  Where City_Code='" + fndToCity.Value + "'"))
    '    Else
    '        lblToCityName.Text = ""
    '    End If
    'End Sub

    'Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

    'End Sub

    'Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

    'End Sub

    'Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

    'End Sub
End Class
