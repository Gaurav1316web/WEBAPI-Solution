Imports common
Imports System.Data.SqlClient
'Ticket No-UDL/18/02/19-000268 Sanjay add delete button
Public Class frmchildRouteFreight
    Inherits FrmMainTranScreen
#Region "Variables"
   
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = True
    Public Locationcode As String = Nothing
    Public CityCode As String = Nothing
    Public Type As String = Nothing
    Public Transpotercode As String = Nothing
    Public TransType As String = Nothing
    Public ToLocation As String = Nothing
    Public CapacityMT As String = Nothing
    Dim IsLoadData As Boolean = False
    Dim FormLoadData As Boolean = False
#End Region
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmRouteFreightDetails)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        'End If
        'btnSave.Visible = MyBase.isModifyFlag
    End Sub


    Sub funClose()
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub FrmRouteFreightDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        End If
    End Sub

    Private Sub FrmRouteFreightDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        IsLoadData = True
        FormLoadData = True
        SetUserMgmtNew()
        LoadTransType()
        LoadType()
        IsLoadData = False
        FormLoadData = False
        txtDate.Value = clsCommon.GETSERVERDATE()
        ddltype.Enabled = False
        ddl_transtype.Enabled = False
        btnDelete.Enabled = False
        ddltype.SelectedValue = Type
        ddl_transtype.SelectedValue = TransType

        '===============Added by preeti gupta[14/01/2019]=============
        If clsCommon.CompairString(Type, "KG") = CompairStringResult.Equal Then
            MyTextBox1.Enabled = False
            txtFixedAmt.Enabled = False
        Else
            MyTextBox1.Enabled = True
            txtFixedAmt.Enabled = True
        End If
        '=============================================================

        If clsCommon.CompairString(TransType, "T") = CompairStringResult.Equal Then
            If clsCommon.myLen(ToLocation) > 0 AndAlso clsCommon.myLen(Transpotercode) > 0 AndAlso clsCommon.myLen(Locationcode) AndAlso clsCommon.myLen(TransType) > 0 AndAlso clsCommon.myLen(Type) > 0 Then
                LoadData(CityCode, Transpotercode, Type, Locationcode, TransType)
            Else
                ValidTransaction()
            End If
        Else
            If clsCommon.myLen(CityCode) > 0 AndAlso clsCommon.myLen(Transpotercode) > 0 AndAlso clsCommon.myLen(Locationcode) AndAlso clsCommon.myLen(TransType) > 0 AndAlso clsCommon.myLen(Type) > 0 Then
                LoadData(CityCode, Transpotercode, Type, Locationcode, TransType)
            Else
                ValidTransaction()
            End If

        End If

    End Sub

    Private Sub LoadData(ByVal CityCode As String, ByVal Transpotercode As String, ByVal Typed As String, ByVal LocationCode As String, ByVal TransType As String)
        Dim dt As New DataTable()
        Dim qry As String = ""
        Try
            If clsCommon.CompairString(TransType, "T") = CompairStringResult.Equal Then
                qry = "select TSPL_ROUTE_FREIGHT_DETAILS.Location_Code ,FromLoc.Location_Desc ,TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code ,ToLoc.Location_Desc,TSPL_ROUTE_FREIGHT_DETAILS.City_Code ,TSPL_CITY_MASTER.City_Name,TSPL_ROUTE_FREIGHT_DETAILS.CapacityMT,TSPL_ROUTE_FREIGHT_DETAILS.Freight,TSPL_ROUTE_FREIGHT_DETAILS.Fixed, TSPL_ROUTE_FREIGHT_DETAILS.Effective_Date  ,TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id ,TSPL_TRANSPORT_MASTER.Transporter_Name ,TSPL_ROUTE_FREIGHT_DETAILS.Type,TSPL_ROUTE_FREIGHT_DETAILS.TransType,TSPL_ROUTE_FREIGHT_DETAILS.tolocation_code from TSPL_ROUTE_FREIGHT_DETAILS" & _
                          " left join TSPL_LOCATION_MASTER as FromLoc on FromLoc.Location_Code =TSPL_ROUTE_FREIGHT_DETAILS.Location_Code" & _
                          " left join TSPL_LOCATION_MASTER as ToLoc on ToLoc.Location_Code =TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code" & _
                          " left join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_ROUTE_FREIGHT_DETAILS.City_Code" & _
                          " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id where 2=2 and TSPL_ROUTE_FREIGHT_DETAILS.Type='" & Typed & "' and TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code='" & ToLocation & "' and TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id='" & Transpotercode & "' and TSPL_ROUTE_FREIGHT_DETAILS.Location_Code='" & LocationCode & "' and TSPL_ROUTE_FREIGHT_DETAILS.TransType='" & TransType & "'" & _
                          " and TSPL_ROUTE_FREIGHT_DETAILS.CapacityMT='" & CapacityMT & "'" & _
                          " order by convert(date,TSPL_ROUTE_FREIGHT_DETAILS.Effective_Date,103) "
            Else
                qry = "select TSPL_ROUTE_FREIGHT_DETAILS.Location_Code ,FromLoc.Location_Desc ,TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code ,ToLoc.Location_Desc,TSPL_ROUTE_FREIGHT_DETAILS.City_Code ,TSPL_CITY_MASTER.City_Name,TSPL_ROUTE_FREIGHT_DETAILS.CapacityMT,TSPL_ROUTE_FREIGHT_DETAILS.Freight,TSPL_ROUTE_FREIGHT_DETAILS.Fixed, TSPL_ROUTE_FREIGHT_DETAILS.Effective_Date  ,TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id ,TSPL_TRANSPORT_MASTER.Transporter_Name ,TSPL_ROUTE_FREIGHT_DETAILS.Type,TSPL_ROUTE_FREIGHT_DETAILS.TransType,TSPL_ROUTE_FREIGHT_DETAILS.tolocation_code from TSPL_ROUTE_FREIGHT_DETAILS" & _
                          " left join TSPL_LOCATION_MASTER as FromLoc on FromLoc.Location_Code =TSPL_ROUTE_FREIGHT_DETAILS.Location_Code" & _
                          " left join TSPL_LOCATION_MASTER as ToLoc on ToLoc.Location_Code =TSPL_ROUTE_FREIGHT_DETAILS.ToLocation_Code" & _
                          " left join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_ROUTE_FREIGHT_DETAILS.City_Code" & _
                          " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id where 2=2 and TSPL_ROUTE_FREIGHT_DETAILS.Type='" & Typed & "' and TSPL_ROUTE_FREIGHT_DETAILS.City_Code='" & CityCode & "' and TSPL_ROUTE_FREIGHT_DETAILS.Transport_Id='" & Transpotercode & "' and TSPL_ROUTE_FREIGHT_DETAILS.Location_Code='" & LocationCode & "' and TSPL_ROUTE_FREIGHT_DETAILS.TransType='" & TransType & "'" & _
                          " and TSPL_ROUTE_FREIGHT_DETAILS.CapacityMT='" & CapacityMT & "'" & _
                          " order by convert(date,TSPL_ROUTE_FREIGHT_DETAILS.Effective_Date,103) "
            End If
           
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                fndCity.Value = clsCommon.myCstr(dt.Rows(0)("City_Code"))
                fndCity.Tag = clsCommon.myCstr(dt.Rows(0)("City_Code"))
                lblCityName.Text = clsDBFuncationality.getSingleValue("select  City_Name  from TSPL_CITY_MASTER where City_Code='" & fndCity.Value & "'")
                fndLocation.Value = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                fndLocation.Tag = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                lblLocationName.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'")
                fndTranspoter.Value = clsCommon.myCstr(dt.Rows(0)("Transport_Id"))
                fndTranspoter.Tag = clsCommon.myCstr(dt.Rows(0)("Transport_Id"))
                lbltranspoterName.Text = clsDBFuncationality.getSingleValue("select  Transporter_Name  from TSPL_TRANSPORT_MASTER where Transport_Id='" & fndTranspoter.Value & "'")
                txtFixedAmt.Text = clsCommon.myCdbl(dt.Rows(0)("Fixed"))
                txtFreight.Text = clsCommon.myCdbl(dt.Rows(0)("Freight"))
                txtDate.Text = clsCommon.myCstr(dt.Rows(0)("Effective_Date"))
                MyTextBox1.Text = clsCommon.myCdbl(dt.Rows(0)("CapacityMT"))
                ddltype.SelectedValue = clsCommon.myCstr(dt.Rows(0)("Type"))
                ddl_transtype.SelectedValue = clsCommon.myCstr(dt.Rows(0)("TransType"))
                txtfromLocation.Value = clsCommon.myCstr(dt.Rows(0)("tolocation_code"))
                txtfromLocation.Tag = clsCommon.myCstr(dt.Rows(0)("tolocation_code"))
                lblFromLocation.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & txtfromLocation.Value & "'")

                btnSave.Text = "Update"
                btnDelete.Enabled = True
                ddltype.Enabled = False
                ddl_transtype.Enabled = False
                isNewEntry = False
                ValidTransaction()
                MyTextBox1.Enabled = False
            Else
                btnSave.Text = "Save"
                btnDelete.Enabled = False
                ValidTransaction()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Public Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Select"
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Fixed"
        dr("Name") = "Fixed"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MT"
        dr("Name") = "MT"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "KG"
        dr("Name") = "KG"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Both"
        dr("Name") = "Both"
        dt.Rows.Add(dr)

        ddltype.DataSource = dt
        ddltype.ValueMember = "Code"
        ddltype.DisplayMember = "Name"

    End Sub

    Private Sub fndCity__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCity._MYValidating
        Try
            Dim qry As String = "select City_Code As Code,ISNULL(City_Name,'') As City_Name from TSPL_CITY_MASTER "
            fndCity.Value = clsCommon.ShowSelectForm("City1", qry, "Code", "", fndCity.Value, "Code", isButtonClicked)
            lblCityName.Text = clsDBFuncationality.getSingleValue("select  City_Name  from TSPL_CITY_MASTER where City_Code='" & fndCity.Value & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message())
        End Try
    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Try
            Dim WhrCls As String = " "
            Dim qry As String = "select Location_Code As Code,ISNULL(Location_Desc,'') As Location_Desc from TSPL_LOCATION_MASTER "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "   TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            fndLocation.Value = clsCommon.ShowSelectForm("Location1", qry, "Code", WhrCls, fndLocation.Value, "Code", isButtonClicked)
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message())
        End Try
    End Sub

    Private Sub txtfromLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtfromLocation._MYValidating
        Try
            Dim WhrCls As String = " "
            Dim qry As String = "select Location_Code As Code,ISNULL(Location_Desc,'') As Location_Desc from TSPL_LOCATION_MASTER "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "   TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtfromLocation.Value = clsCommon.ShowSelectForm("Location1", qry, "Code", WhrCls, txtfromLocation.Value, "Code", isButtonClicked)
            lblFromLocation.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & txtfromLocation.Value & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message())
        End Try
    End Sub

    Private Sub fndTranspoter__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTranspoter._MYValidating
        Try
            Dim WhrCls As String = ""
            Dim qry As String = "SELECT Transport_Id AS Code,ISNULL(Transporter_Name,'') As [Transportor Name],ISNULL(city,'') AS City,ISNULL(state,'') As State,ISNULL(panno,'') AS [PAN No],ISNULL(Add1,'') AS [Address1],ISNULL(Add2,'') AS [Address2],ISNULL(Email,'') AS [Email] FROM TSPL_TRANSPORT_MASTER "

            fndTranspoter.Value = clsCommon.ShowSelectForm("Transporter1", qry, "Code", "", fndTranspoter.Value, "Code", isButtonClicked)
            lbltranspoterName.Text = clsDBFuncationality.getSingleValue("select  Transporter_Name  from TSPL_TRANSPORT_MASTER where Transport_Id='" & fndTranspoter.Value & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message())
        End Try
    End Sub
    Private Sub ddltype_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddltype.SelectedIndexChanged
        Validation()
    End Sub
    Public Sub Validation()
        Try
            'LoadData()
            If clsCommon.CompairString(clsCommon.myCstr(ddltype.SelectedValue), "Both") = CompairStringResult.Equal Then
                txtFreight.ReadOnly = False
                txtFixedAmt.ReadOnly = False

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddltype.SelectedValue), "MT") = CompairStringResult.Equal Then
                txtFreight.ReadOnly = False
                txtFixedAmt.ReadOnly = True

            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddltype.SelectedValue), "Fixed") = CompairStringResult.Equal Then
                txtFreight.ReadOnly = True
                txtFixedAmt.ReadOnly = False

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub SaveData()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmChildRouteFreight, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New ClsRouteFreightDetails()

                obj.Location_Code = fndLocation.Value
                obj.Location_Name = lblLocationName.Text
                obj.City_Code = fndCity.Value
                obj.UpdateCity = fndCity.Tag
                obj.UpdateLocation = fndLocation.Tag
                obj.UpdateTransporter = fndTranspoter.Tag
                obj.City_Name = lblCityName.Text
                obj.Transport_Id = fndTranspoter.Value
                obj.Transport_Name = lbltranspoterName.Text
                obj.Freight = txtFreight.Text
                obj.Fixed = txtFixedAmt.Text
                obj.EffectiveDate = txtDate.Value
                obj.CapacityMT = MyTextBox1.Text
                obj.Type = clsCommon.myCstr(ddltype.SelectedValue)
                obj.UpdateToLocation = txtfromLocation.Tag

                Dim DocType As String = ddltype.SelectedValue
                obj.TransType = clsCommon.myCstr(ddl_transtype.SelectedValue)
                obj.ToLocation_Code = txtfromLocation.Value
                obj.ToLocation_Name = lblFromLocation.Text
                If obj Is Nothing Then
                    clsCommon.MyMessageBoxShow("No data found.")
                Else
                    If ClsRouteFreightDetails.SaveDataChild(obj, isNewEntry) Then
                        clsCommon.MyMessageBoxShow("Data saved successfully.")
                        btnSave.Text = "Update"
                    End If
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Function AllowToSave() As Boolean

        If clsCommon.CompairString(ddl_transtype.SelectedValue, "P") = CompairStringResult.Equal Then
            If clsCommon.myLen(fndCity.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("City code can not be left blank ")
                fndCity.Focus()
                Return False
            End If
        Else
            If clsCommon.myLen(txtfromLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("From Location can not be left blank ")
                txtfromLocation.Focus()
                Return False
            End If
        End If

        If clsCommon.CompairString(ddl_transtype.SelectedValue, "T") = CompairStringResult.Equal Then
            If clsCommon.myCstr(txtfromLocation.Value) = clsCommon.myCstr(fndLocation.Value) Then
                clsCommon.MyMessageBoxShow("From Location and To Location can not be Same ")
                txtfromLocation.Focus()
                Return False
            End If
        End If

        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("To Location code can not be left blank ")
            fndLocation.Focus()
            Return False
        End If

        If clsCommon.myLen(fndTranspoter.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Transport id can not be left blank ")
            fndTranspoter.Focus()
            Return False
        End If
        If clsCommon.myLen(txtDate.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Effective date can not be left blank ")
            txtDate.Focus()
            Return False
        End If

        If clsCommon.myCstr(ddltype.SelectedValue) = "Select" Then
            myMessages.blankValue("Select Type")
            Return False
        End If
        If clsCommon.myCstr(ddl_transtype.SelectedValue) = "Select" Then
            myMessages.blankValue("Select Transaction Type")
            ddl_transtype.Focus()
            Return False
        End If


        Return True
    End Function

    Public Sub LoadTransType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "Sale"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "T"
        dr("Name") = "Transfer"
        dt.Rows.Add(dr)

        ddl_transtype.DataSource = dt
        ddl_transtype.ValueMember = "Code"
        ddl_transtype.DisplayMember = "Name"
        ddl_transtype.SelectedValue = "P"
    End Sub

    'Private Sub ddl_transtype_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddl_transtype.SelectedValueChanged
    '    Try
    '        ValidTransaction()
    '    Catch ex As Exception
    '        IsLoadData = False
    '    End Try

    'End Sub
    Public Sub ValidTransaction()
        Try
            If clsCommon.CompairString(ddl_transtype.SelectedValue, "T") = CompairStringResult.Equal Then
                txtfromLocation.Enabled = True
                fndCity.Enabled = False
            Else
                txtfromLocation.Enabled = False
                fndCity.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message())
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim obj As New ClsRouteFreightDetails()
        If ClsRouteFreightDetails.DeleteData(ddltype.SelectedValue, ddl_transtype.SelectedValue, fndLocation.Value, txtfromLocation.Value, fndCity.Value, fndTranspoter.Value, MyTextBox1.Value) Then
            clsCommon.MyMessageBoxShow("Data delete successfully.")
            funClose()
        End If
    End Sub
End Class
