Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmEInvoiceHead
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isNewEntry As Boolean = False

#End Region


    Private Sub frmEInvoiceHead_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub
    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        LoadData(txtCode.Value, NavigatorType.Current)
    End Sub

    Private Sub btnViewDocument_Click(sender As Object, e As EventArgs) Handles btnViewDocument.Click
        LoadData(txtCode.Value, NavigatorType.Current)
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal nav As NavigatorType)
        Dim qry As String
        Dim dr As DataRow
        If strCode = "" Then
            qry = "select url, username, password, ip_address , client_id, client_secret, GSTin, RequiredFor, VendorName, Location_Code, code from TSPL_EINVOICEHEADER_INFO"
            dr = clsCommon.ShowSelectFormForRow("EINVOICE", qry)
            If dr IsNot Nothing Then
                txtUrl.Text = clsCommon.myCstr(dr("url"))
                txtUserName.Text = clsCommon.myCstr(dr("username"))
                txtPassword.Text = clsCommon.myCstr(dr("password"))
                txtIPAddr.Text = clsCommon.myCstr(dr("ip_address"))
                txtClientID.Text = clsCommon.myCstr(dr("client_id"))
                txtClientSecret.Text = clsCommon.myCstr(dr("client_secret"))
                txtGSTin.Text = clsCommon.myCstr(dr("GSTin"))
                txtReqFor.Text = clsCommon.myCstr(dr("RequiredFor"))
                txtVendorName.Text = clsCommon.myCstr(dr("VendorName"))
                txtLocation.Value = clsCommon.myCstr(dr("Location_Code"))
                txtCode.Value = dr("code")
                txtLocDes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc As Name from TSPL_LOCATION_MASTER where Location_Code = '" + txtLocation.Value + "' "))

            End If
        Else
            qry = "select url, username, password, ip_address , client_id, client_secret, GSTin, RequiredFor, VendorName, Location_Code, code from TSPL_EINVOICEHEADER_INFO where code = " & strCode
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtUrl.Text = clsCommon.myCstr(dt.Rows(0)("url"))
                txtUserName.Text = clsCommon.myCstr(dt.Rows(0)("username"))
                txtPassword.Text = clsCommon.myCstr(dt.Rows(0)("password"))
                txtIPAddr.Text = clsCommon.myCstr(dt.Rows(0)("ip_address"))
                txtClientID.Text = clsCommon.myCstr(dt.Rows(0)("client_id"))
                txtClientSecret.Text = clsCommon.myCstr(dt.Rows(0)("client_secret"))
                txtGSTin.Text = clsCommon.myCstr(dt.Rows(0)("GSTin"))
                txtReqFor.Text = clsCommon.myCstr(dt.Rows(0)("RequiredFor"))
                txtVendorName.Text = clsCommon.myCstr(dt.Rows(0)("VendorName"))
                txtLocation.Value = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                txtLocDes.Text = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            End If
        End If


    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Addnew()
    End Sub

    Private Sub Addnew()
        txtUrl.Text = ""
        txtUserName.Text = ""
        txtLocDes.Text = ""
        txtPassword.Text = ""
        txtIPAddr.Text = ""
        txtClientID.Text = ""
        txtClientSecret.Text = ""
        txtGSTin.Text = ""
        txtReqFor.Text = ""
        txtVendorName.Text = ""
        txtLocation.Value = ""
        isNewEntry = True
        txtCode.Value = ""
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim qst As String = "select count(1) from TSPL_EINVOICEHEADER_INFO where code= " + txtCode.Value + ""
                Dim DataExist = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
                If DataExist = 0 Then
                    isNewEntry = True
                Else
                    isNewEntry = False

                End If
                Dim obj As New clsEInvoiceHead()
                obj.url = txtUrl.Text
                obj.username = txtUserName.Text
                obj.password = txtPassword.Text
                obj.ip_address = txtIPAddr.Text
                obj.client_id = txtClientID.Text
                obj.client_secret = txtClientSecret.Text
                obj.GSTin = txtGSTin.Text
                obj.RequiredFor = txtReqFor.Text
                obj.VendorName = txtVendorName.Text
                obj.Location_Code = txtLocation.Value
                If (obj.SaveData(obj, isNewEntry, txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Dim MaxCode = clsDBFuncationality.getSingleValue("select max(code) as code from TSPL_EINVOICEHEADER_INFO")
                    txtCode.Value = MaxCode + 1
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function AllowToSave() As Boolean

        If txtUrl.Text = "" Then
            txtUrl.Focus()
            Throw New Exception("Please enter Url")
        End If
        If clsCommon.myCstr(txtUserName.Text) = "" Then
            txtUserName.Focus()
            Throw New Exception("Please enter User Name")
        End If
        If clsCommon.myCstr(txtPassword.Text) = "" Then
            txtPassword.Focus()
            Throw New Exception("Please enter Password")
        End If

        If clsCommon.myCstr(txtIPAddr.Text) = "" Then
            txtIPAddr.Focus()
            Throw New Exception("Please enter IP Address")
        End If
        If clsCommon.myCstr(txtClientID.Text) = "" Then
            txtClientID.Focus()
            Throw New Exception("Please enter Client ID")
        End If
        If clsCommon.myCstr(txtClientSecret.Text) = "" Then
            txtClientSecret.Focus()
            Throw New Exception("Please enter Client Secret")
        End If
        If clsCommon.myCstr(txtGSTin.Text) = "" Then
            txtGSTin.Focus()
            Throw New Exception("Please enter GST")
        End If
        If clsCommon.myCstr(txtReqFor.Text) = "" Then
            txtReqFor.Focus()
            Throw New Exception("Please enter Required For")
        End If
        If clsCommon.myCstr(txtVendorName.Text) = "" Then
            txtVendorName.Focus()
            Throw New Exception("Please enter Vendor Name ")
        End If
        If clsCommon.myCstr(txtLocation.Value) = "" Then
            Mylabel.Focus()
            Throw New Exception("Please enter Location Code")
        End If
        Return True
    End Function

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "Select Location_Code As Code,Location_Desc As Name from TSPL_LOCATION_MASTER "
            txtLocation.Value = clsCommon.ShowSelectForm("EInvoice", qry, "Code", "", txtLocation.Value, "Code", isButtonClicked)
            txtLocDes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc As Name from TSPL_LOCATION_MASTER where Location_Code = '" + txtLocation.Value + "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select code as code from TSPL_EINVOICEHEADER_INFO where 2=2"
            Select Case NavType
                Case NavigatorType.Current
                    qst += " and TSPL_EINVOICEHEADER_INFO.code in ('" + txtCode.Value + "')"
                Case NavigatorType.Next
                    qst += " and TSPL_EINVOICEHEADER_INFO.code in (select min(code ) from TSPL_EINVOICEHEADER_INFO where code  >'" + txtCode.Value + "')"
                Case NavigatorType.First
                    qst += " and TSPL_EINVOICEHEADER_INFO.code in (select MIN(code ) from TSPL_EINVOICEHEADER_INFO)"
                Case NavigatorType.Last
                    qst += " and TSPL_EINVOICEHEADER_INFO.code in (select Max(code ) from TSPL_EINVOICEHEADER_INFO)"
                Case NavigatorType.Previous
                    qst += " and TSPL_EINVOICEHEADER_INFO.code in (select Max(code ) from TSPL_EINVOICEHEADER_INFO where code  <'" + txtCode.Value + "')"
            End Select
            txtCode.Value = clsDBFuncationality.getSingleValue(qst)
            LoadData(txtCode.Value, NavigatorType.Current)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                btnSave.Text = "Update"
                txtCode.MyReadOnly = True

            Else
                btnSave.Text = "Save"
                txtCode.MyReadOnly = False
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


End Class