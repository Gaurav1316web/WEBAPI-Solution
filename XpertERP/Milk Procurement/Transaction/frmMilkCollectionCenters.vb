Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.IO
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExportToExcelML
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports System.Threading
Imports common

Public Class FrmMilkCollectionCenters
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Code As String
    Public CreateNewTransaction As Boolean = False
    Public isNewEntry As Boolean = True

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
#Region "Variable"

    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region
    Function AllowToSave() As Boolean
        If txtDescription.Text = "" Then
            myMessages.blankValue(Me, "Description is blank", Me.Text)
            txtDescription.Focus()
            Return False
        ElseIf txtCollectionLevel.Value = "" Then
            myMessages.blankValue(Me, "collection blank", Me.Text)
            txtCollectionLevel.Focus()
            Return False




        End If
        Return True
    End Function




    Public Sub Save()

        Dim strvalue As String

        If AllowToSave() Then

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmMilkCollectionCenters, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            Dim obj As New clsMilkCollectionCenters()
            Dim qry = Nothing

            obj.COLLECTION_CENTER_CODE = txtCollectionCenters.Value
            obj.DESCRIPTION = txtDescription.Text
            obj.LEVEL_CODE = txtCollectionLevel.Value
            obj.ADDRESS1 = txtAdd1.Text
            obj.ADDRESS2 = txtAdd2.Text
            obj.ADDRESS3 = txtAdd3.Text
            obj.ADDRESS4 = txtAdd4.Text
            obj.City = txtCity.Text
            obj.State_Code = fndstate.Value
            obj.State_Name = txtstateprovince.Text
            obj.Zip = txtZipPostalCode.Text
            ' obj.InsuranceExpirydate = dtpInsuExpDate.Text
            obj.Country = txtCountry.Text
            obj.Telphone = txtTelephone.Text
            obj.EMAIL_ADDRESS = txtEmail.Text
            obj.Phone1 = txtPhone1.Text
            obj.Phone2 = txtPhone2.Text
            qry = "select COLLECTION_CENTER_CODE from TSPL_MilkCollectionCenter where COLLECTION_CENTER_CODE='" & obj.COLLECTION_CENTER_CODE & "'"
            strvalue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(strvalue) > 0 Then
                isNewEntry = False
            Else
                isNewEntry = True
            End If
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                If clsMilkCollectionCenters.SaveData(obj, isNewEntry, trans) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                    LoadData(obj.COLLECTION_CENTER_CODE, NavigatorType.Current)
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try

        End If
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMilkCollectionCenters)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag

        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmMilkCollectionCenters_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isNewEntry = True
        SetUserMgmtNew()
        If clsCommon.myLen(Code) > 0 Then
            LoadData(Code, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub fndstate__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndstate._MYValidating
        Dim qry As String = "select state_code as Code,state_name as [State Name] from TSPL_TDS_STATE_MASTER  "
        fndstate.Value = clsCommon.ShowSelectForm("LmSate", qry, "Code", "", fndstate.Value, "[State Name]", isButtonClicked)
        txtstateprovince.Text = clsDBFuncationality.getSingleValue("select state_name from TSPL_TDS_STATE_MASTER  where state_code='" + fndstate.Value + "'")
    End Sub

    Private Sub txtCollectionLevel__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCollectionLevel._MYValidating
        Dim qry As String = "select LEVEL_CODE as Code,DESCRIPTION  from TSPL_MilkCollectionLevels "
        Dim wherclause As String = "" '" LEVEL=(select MAX(level) from TSPL_Hierarchy_Master where cast(substring(Level,6,2) as integer)<substring('" & Me.cboLevel.SelectedValue.ToString & "',6,2))"
        txtCollectionLevel.Value = clsCommon.ShowSelectForm("TSPL_MilkCollectionLevels", qry, "Code", wherclause, txtCollectionLevel.Value, "", isButtonClicked)

        'qry = "select Description  from TSPL_MilkCollectionLevels  where LEVEL_CODE  ='" + txtCollectionLevel.Value + "'"
        'lblHigherLevelDesgName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)



        Dim obj As clsMilkCollectionCenters = clsMilkCollectionCenters.GetData(strCode, NavTyep)
        ' Dim qry As String
        If obj IsNot Nothing Then
            isNewEntry = False
            txtCollectionCenters.Value = obj.COLLECTION_CENTER_CODE
            txtDescription.Text = obj.DESCRIPTION
            txtCollectionLevel.Value = obj.LEVEL_CODE
            txtAdd1.Text = obj.ADDRESS1

            txtAdd2.Text = obj.ADDRESS2
            txtAdd3.Text = obj.ADDRESS3
            txtAdd4.Text = obj.ADDRESS4
            txtCity.Text = obj.City
            fndstate.Value = obj.State_Code
            txtstateprovince.Text = obj.State_Name
            txtZipPostalCode.Text = obj.Zip
            txtCountry.Text = obj.Country
            txtTelephone.Text = obj.Telphone
            txtPhone1.Text = obj.Phone1
            txtPhone2.Text = obj.Phone2
            txtEmail.Text = obj.EMAIL_ADDRESS



        End If
    End Sub

    Private Sub txtCollectionCenters__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCollectionCenters._MYNavigator
        LoadData(txtCollectionCenters.Value, NavType)
    End Sub

    Private Sub txtCollectionCenters__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCollectionCenters._MYValidating
        If txtCollectionCenters.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select COLLECTION_CENTER_CODE as Code,Description as Name from TSPL_MilkCollectionCenter  "
            LoadData(clsCommon.ShowSelectForm("TSPL_MilkCollectionLevels", qry, "Code", "", txtCollectionCenters.Value, "Code", isButtonClicked), NavigatorType.Current)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            If clsCommon.myLen(txtCollectionCenters.Value) <= 0 Then
                Throw New Exception("User Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Delete the Current User." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim qry As String = "delete from TSPL_MilkCollectionCenter where COLLECTION_CENTER_CODE='" + txtCollectionCenters.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
                Addnew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Addnew()
        txtCollectionCenters.Value = ""
        txtDescription.Text = ""
        txtCollectionLevel.Value = ""
        txtAdd1.Text = ""
        txtAdd2.Text = ""
        txtAdd3.Text = ""
        txtAdd4.Text = ""
        txtCity.Text = ""
        fndstate.Value = ""
        txtstateprovince.Text = ""
        txtZipPostalCode.Text = ""
        txtCountry.Text = ""
        txtTelephone.Text = ""
        txtPhone1.Text = ""
        txtPhone2.Text = ""
        txtEmail.Text = ""
    End Sub
    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Addnew()

    End Sub
End Class
