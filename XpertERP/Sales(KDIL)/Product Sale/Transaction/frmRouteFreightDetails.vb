Imports common
Imports System.Data.SqlClient
'Ticket No-UDL/18/02/19-000268 Sanjay
Public Class FrmRouteFreightDetails
    Inherits FrmMainTranScreen
#Region "Variables"
    Const ColLocationCode As String = "Location Code"
    Const ColLocationName As String = "Location Name"

    Const ColToLocationCode As String = "To Location Code"
    Const ColToLocationName As String = "To Location Name"

    Const ColCityCode As String = "City Code"
    Const ColCityName As String = "City Name"
    Const ColTransportCode As String = "Transport Id"
    Const ColTransportName As String = "Transport Name"
    Const ColCapacityMT As String = "Capacity(MT)"
    Const ColFreight As String = "Freight"
    Const ColFixedAmt As String = "FixedAmt"
    Const colRowType As String = "colRowType"
    Const ColEffectiveDate As String = "Effective Date"
    Const colType As String = "Type"
    Public Const RowTypeFixedAmt As String = "FixedAmt"
    Public Const RowTypeEmpty As String = "Empty"
    Public Const RowTypeBoth As String = "Both"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim IsLoadData As Boolean = False
    Dim FormLoadData As Boolean = False
#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmRouteFreightDetails)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub
    Function AllowToSave() As Boolean
        Dim GridRowQual As Integer = 0
        Dim HighQual As Integer = 0
        Dim LocCode As String = ""
        Dim ToLocCode As String = ""
        Dim Capacity As Double = 0
        Dim CityCode As String = ""
        Dim TransportCode As String = ""
        Dim Freight As Double = 0
        Dim EffectiveDate As String = ""
        Dim Type As String = ""
        Dim TransType As String = ""
        Dim lineno As Integer = 0
        Dim trans As SqlTransaction = Nothing
        btnSave.Focus()
        If clsCommon.myCstr(ddltype.SelectedValue) = "Select" Then
            myMessages.blankValue(Me, "Select Type", Me.Text)
            Return False
        End If
        For Each grow As GridViewRowInfo In gv.Rows
            lineno += 1
            LocCode = clsCommon.myCstr(grow.Cells(ColLocationCode).Value)
            ToLocCode = clsCommon.myCstr(grow.Cells(ColToLocationCode).Value)
            CityCode = clsCommon.myCstr(grow.Cells(ColCityCode).Value)
            TransportCode = clsCommon.myCstr(grow.Cells(ColTransportCode).Value)
            Capacity = clsCommon.myCdbl(grow.Cells(ColCapacityMT).Value)
            Freight = clsCommon.myCdbl(grow.Cells(ColFreight).Value)
            EffectiveDate = clsCommon.myCstr(grow.Cells(ColEffectiveDate).Value)
            Type = clsCommon.myCstr(grow.Cells(colType).Value)
            'If clsCommon.myLen(LocCode) > 0 Then
            '    Dim qry As String = "Select Count(*) As Row From TSPL_LOCATION_MASTER where Location_Code = '" + LocCode + "'"
            '    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            '    If check <= 0 Then
            '        clsCommon.MyMessageBoxShow("Location code '" & LocCode & "' does not exists at line no. " + clsCommon.myCstr(lineno) + ".")
            '        Return False
            '    End If
            'End If
            'If clsCommon.myLen(ToLocCode) > 0 Then
            '    Dim qry As String = "Select Count(*) As Row From TSPL_LOCATION_MASTER where Location_Code = '" + ToLocCode + "'"
            '    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            '    If check <= 0 Then
            '        clsCommon.MyMessageBoxShow("To Location code '" & ToLocCode & "' does not exists at line no. " + clsCommon.myCstr(lineno) + ".")
            '        Return False
            '    End If
            'End If
            'If clsCommon.myLen(CityCode) > 0 Then
            '    Dim qry As String = "Select Count(*) As Row From TSPL_CITY_MASTER where City_Code = '" + CityCode + "'"
            '    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            '    If check <= 0 Then
            '        clsCommon.MyMessageBoxShow("City code '" & CityCode & "' does not exists at line no. " + clsCommon.myCstr(lineno) + ".")
            '        Return False
            '    End If
            'End If
            'If clsCommon.myLen(TransportCode) > 0 Then
            '    Dim qry As String = "Select Count(*) As Row From TSPL_TRANSPORT_MASTER where Transport_Id = '" + TransportCode + "'"
            '    Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            '    If check <= 0 Then
            '        clsCommon.MyMessageBoxShow("Transport id '" & TransportCode & "' does not exists at line no. " + clsCommon.myCstr(lineno) + ".")
            '        Return False
            '    End If
            'End If
            If clsCommon.myLen(LocCode) > 0 Then
                If clsCommon.CompairString(ddl_transtype.SelectedValue, "P") = CompairStringResult.Equal Then
                    If clsCommon.myLen(CityCode) <= 0 Then
                        clsCommon.MyMessageBoxShow("City code can not be left blank at line no. " + clsCommon.myCstr(lineno) + ".")
                        Return False
                    End If
                Else
                    If clsCommon.myLen(ToLocCode) <= 0 Then
                        clsCommon.MyMessageBoxShow("To Location code can not be left blank at line no. " + clsCommon.myCstr(lineno) + ".")
                        Return False
                    End If
                End If
                If clsCommon.myLen(TransportCode) <= 0 Then
                    clsCommon.MyMessageBoxShow("Transport id can not be left blank at line no. " + clsCommon.myCstr(lineno) + ".")
                    Return False
                End If
                If clsCommon.myLen(EffectiveDate) <= 0 Then
                    clsCommon.MyMessageBoxShow("Effective date can not be left blank at line no. " + clsCommon.myCstr(lineno) + ".")
                    Return False
                End If
            End If
        Next

        Return True
    End Function

    Sub funClose()
        Me.Close()
        GC.Collect()
    End Sub

    Sub OpenToLocCode(ByVal isButtonClick As Boolean)
        Dim WhrCls As String = " "
        Dim qry As String = "select Location_Code As Code,ISNULL(Location_Desc,'') As Location_Desc from TSPL_LOCATION_MASTER "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "   TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        gv.CurrentRow.Cells(ColToLocationCode).Value = clsCommon.ShowSelectForm("RFDLoc", qry, "Code", WhrCls, gv.CurrentRow.Cells(ColToLocationCode).Value.ToString().Replace("'", ""), "Code", isButtonClick)
        If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColToLocationCode).Value)) > 0 Then
            gv.CurrentRow.Cells(ColToLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Location_Desc,'') As Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" & clsCommon.myCstr(gv.CurrentRow.Cells(ColToLocationCode).Value) & "'"))
        Else
            gv.CurrentRow.Cells(ColToLocationName).Value = ""
        End If
    End Sub
    Sub OpenLocCode(ByVal isButtonClick As Boolean)
        Dim WhrCls As String = " "
        Dim qry As String = "select Location_Code As Code,ISNULL(Location_Desc,'') As Location_Desc from TSPL_LOCATION_MASTER "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "   TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        gv.CurrentRow.Cells(ColLocationCode).Value = clsCommon.ShowSelectForm("RFDLoc", qry, "Code", WhrCls, gv.CurrentRow.Cells(ColLocationCode).Value.ToString().Replace("'", ""), "Code", isButtonClick)
        If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColLocationCode).Value)) > 0 Then
            gv.CurrentRow.Cells(ColLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Location_Desc,'') As Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" & clsCommon.myCstr(gv.CurrentRow.Cells(ColLocationCode).Value) & "'"))
        Else
            gv.CurrentRow.Cells(ColLocationName).Value = ""
        End If
    End Sub
    Sub OpenCityCode(ByVal isButtonClick As Boolean)
        Dim WhrCls As String = ""
        Dim qry As String = "select City_Code As Code,ISNULL(City_Name,'') As City_Name from TSPL_CITY_MASTER "

        gv.CurrentRow.Cells(ColCityCode).Value = clsCommon.ShowSelectForm("RFDCity", qry, "Code", WhrCls, gv.CurrentRow.Cells(ColCityCode).Value, "Code", isButtonClick)
        If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColCityCode).Value)) > 0 Then
            gv.CurrentRow.Cells(ColCityName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(City_Name,'') As City_Name FROM TSPL_CITY_MASTER WHERE City_Code ='" & clsCommon.myCstr(gv.CurrentRow.Cells(ColCityCode).Value) & "'"))
        Else
            gv.CurrentRow.Cells(ColCityName).Value = ""
        End If
    End Sub
    Sub OpenTransportCode(ByVal isButtonClick As Boolean)
        Dim WhrCls As String = ""
        Dim qry As String = "SELECT Transport_Id AS Code,ISNULL(Transporter_Name,'') As [Transportor Name],ISNULL(city,'') AS City,ISNULL(state,'') As State,ISNULL(panno,'') AS [PAN No],ISNULL(Add1,'') AS [Address1],ISNULL(Add2,'') AS [Address2],ISNULL(Email,'') AS [Email] FROM TSPL_TRANSPORT_MASTER "

        gv.CurrentRow.Cells(ColTransportCode).Value = clsCommon.ShowSelectForm("RFDCity", qry, "Code", WhrCls, gv.CurrentRow.Cells(ColTransportCode).Value, "Code", isButtonClick)
        If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColTransportCode).Value)) > 0 Then
            gv.CurrentRow.Cells(ColTransportName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Transporter_Name,'') As Transporter_Name FROM TSPL_TRANSPORT_MASTER WHERE Transport_Id ='" & clsCommon.myCstr(gv.CurrentRow.Cells(ColTransportCode).Value) & "'"))
        Else
            gv.CurrentRow.Cells(ColTransportName).Value = ""
        End If
    End Sub

    Public Sub SaveData()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmRouteFreightDetails, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim lineno As Integer = 0
                Dim arr As New List(Of ClsRouteFreightDetails)
                Dim obj As ClsRouteFreightDetails = Nothing

                For Each grow As GridViewRowInfo In gv.Rows
                    lineno += 1
                    If clsCommon.myLen(grow.Cells(ColLocationCode).Value) > 0 Then
                        obj = New ClsRouteFreightDetails()
                        obj.Location_Code = clsCommon.myCstr(grow.Cells(ColLocationCode).Value)
                        If clsCommon.CompairString(clsCommon.myCstr(ddl_transtype.SelectedValue), "P") = CompairStringResult.Equal Then
                            obj.City_Code = clsCommon.myCstr(grow.Cells(ColCityCode).Value)
                        Else
                            obj.ToLocation_Code = clsCommon.myCstr(grow.Cells(ColToLocationCode).Value)
                        End If
                        obj.Transport_Id = clsCommon.myCstr(grow.Cells(ColTransportCode).Value)
                        obj.CapacityMT = clsCommon.myCdbl(grow.Cells(ColCapacityMT).Value)
                        obj.Freight = clsCommon.myCstr(grow.Cells(ColFreight).Value)
                        obj.Fixed = clsCommon.myCstr(grow.Cells(ColFixedAmt).Value)
                        obj.EffectiveDate = clsCommon.myCstr(grow.Cells(ColEffectiveDate).Value)
                        obj.Type = clsCommon.myCstr(ddltype.SelectedValue)
                        obj.TransType = clsCommon.myCstr(ddl_transtype.SelectedValue)
                        arr.Add(obj)
                    End If
                Next
                Dim DocType As String = ddltype.SelectedValue
                Dim TransType As String = clsCommon.myCstr(ddl_transtype.SelectedValue)
                If obj Is Nothing Then
                    clsCommon.MyMessageBoxShow("No data found.")
                Else
                    If ClsRouteFreightDetails.SaveData(DocType, TransType, arr) Then
                        clsCommon.MyMessageBoxShow("Data saved successfully.")
                    End If
                End If
                LoadData()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
        gv.AllowAddNewRow = False
        LoadBlankGrid()

        LoadTransType()
        LoadType()
        FormLoadData = False
        ddltype.Text = "MT"
        If ddltype.Text = "MT" Then
            LoadData()
            gv.Columns(ColFreight).ReadOnly = False
            gv.Columns(ColFixedAmt).ReadOnly = True

        End If
        ShowLocation()
        IsLoadData = False
        FormLoadData = False
        btnSave.Visible = False

    End Sub
    Sub LoadBlankGrid()
        Try
            gv.Rows.Clear()
        Catch ex As Exception
        End Try

        Try
            gv.Columns.Clear()
        Catch ex As Exception
        End Try

        Try
            gv.DataSource = Nothing
        Catch ex As Exception
        End Try

        Dim repoLoc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLoc.FormatString = ""
        repoLoc.HeaderText = "Location Code"
        repoLoc.Name = ColLocationCode
        repoLoc.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoLoc.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLoc.Width = 130
        gv.MasterTemplate.Columns.Add(repoLoc)

        Dim repoLocName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocName.FormatString = ""
        repoLocName.HeaderText = "Location Name"
        repoLocName.Name = ColLocationName
        repoLocName.Width = 200
        repoLocName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoLocName)

        Dim repoToLoc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoToLoc.FormatString = ""
        repoToLoc.HeaderText = "To Location Code"
        repoToLoc.Name = ColToLocationCode
        repoToLoc.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoToLoc.TextImageRelation = TextImageRelation.TextBeforeImage
        repoToLoc.Width = 130
        gv.MasterTemplate.Columns.Add(repoToLoc)

        Dim repoToLocName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoToLocName.FormatString = ""
        repoToLocName.HeaderText = "To Location Name"
        repoToLocName.Name = ColToLocationName
        repoToLocName.Width = 200
        repoToLocName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoToLocName)

        Dim repoCityCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCityCode.FormatString = ""
        repoCityCode.HeaderText = "City Code"
        repoCityCode.Name = ColCityCode
        repoCityCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoCityCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCityCode.Width = 130
        'repoToLocation.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoCityCode)

        Dim repoCityName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCityName.FormatString = ""
        repoCityName.HeaderText = "City Name"
        repoCityName.Name = ColCityName
        repoCityName.Width = 200
        repoCityName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoCityName)

        Dim repoTransportCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTransportCode.FormatString = ""
        repoTransportCode.HeaderText = "Transport ID"
        repoTransportCode.Name = ColTransportCode
        repoTransportCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTransportCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTransportCode.Width = 130
        gv.MasterTemplate.Columns.Add(repoTransportCode)

        Dim repoTransportName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTransportName.FormatString = ""
        repoTransportName.HeaderText = "Transport Name"
        repoTransportName.Name = ColTransportName
        repoTransportName.Width = 200
        repoTransportName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoTransportName)

        Dim repoCapacity As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCapacity.FormatString = ""
        repoCapacity.HeaderText = "Capacity(MT)"
        repoCapacity.Name = ColCapacityMT
        repoCapacity.Width = 130
        repoCapacity.FormatString = "{0:n2}"
        repoCapacity.DecimalPlaces = 2
        repoCapacity.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoCapacity)

        Dim repoFreight As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFreight.FormatString = ""
        repoFreight.HeaderText = "Freight"
        repoFreight.Name = ColFreight
        repoFreight.Width = 100
        'repoFreight.ReadOnly = True
        repoFreight.FormatString = "{0:n2}"
        repoFreight.DecimalPlaces = 2
        repoFreight.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoFreight)

        Dim repoEffDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoEffDate.Format = DateTimePickerFormat.Custom
        repoEffDate.FormatString = "{0:dd/MMM/yyyy}"
        repoEffDate.CustomFormat = "dd/MMM/yyyy"
        repoEffDate.HeaderText = "Effective Date"
        repoEffDate.Name = ColEffectiveDate
        repoEffDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoEffDate.Width = 100
        gv.MasterTemplate.Columns.Add(repoEffDate)

        Dim repoFixedAmtDate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFixedAmtDate.FormatString = ""
        repoFixedAmtDate.HeaderText = "Fixed Amount"
        repoFixedAmtDate.Name = ColFixedAmt
        repoFixedAmtDate.Width = 100
        'repoFreight.ReadOnly = True
        repoFixedAmtDate.FormatString = "{0:n2}"
        repoFixedAmtDate.DecimalPlaces = 2
        repoFixedAmtDate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoFixedAmtDate)

        Dim repoType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoType.FormatString = ""
        repoType.HeaderText = "Type"
        repoType.Name = colType
        repoType.Width = 100
        repoType.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoType)

        gv.ReadOnly = True
        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
    End Sub
    Private Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = RowTypeFixedAmt
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeEmpty
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = RowTypeBoth
        dt.Rows.Add(dr)

        Return dt
    End Function

    Public Sub LoadData()
        If FormLoadData Then
            Exit Sub
        End If
        Try
            IsLoadData = True
            Try
                gv.DataSource = Nothing
            Catch ex As Exception
            End Try
            Try
                gv.Rows.Clear()
            Catch ex As Exception
            End Try
            Try
                gv.Columns.Clear()
            Catch ex As Exception
            End Try
          

            Dim DocType As String = clsCommon.myCstr(ddltype.Text)
            Dim TransType As String = clsCommon.myCstr(ddl_transtype.SelectedValue)
            Dim dt As DataTable = ClsRouteFreightDetails.GetDataTable(DocType, TransType, Nothing)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = dt

                gv.Columns(ColLocationCode).Width = 130
                gv.Columns(ColLocationName).Width = 200
                gv.Columns(ColLocationName).ReadOnly = True
                gv.Columns(ColToLocationCode).Width = 130
                gv.Columns(ColToLocationName).Width = 200
                gv.Columns(ColToLocationName).ReadOnly = True
                gv.Columns(ColCityCode).Width = 130
                gv.Columns(ColCityName).Width = 200
                gv.Columns(ColCityName).ReadOnly = True
                gv.Columns(ColTransportCode).Width = 130
                gv.Columns(ColTransportName).Width = 200
                gv.Columns(ColTransportName).ReadOnly = True
                gv.Columns(ColCapacityMT).Width = 130
                gv.Columns(ColCapacityMT).FormatString = "{0:n2}"
                gv.Columns(ColFreight).FormatString = "{0:n2}"
                gv.Columns(ColFreight).Width = 130

                gv.Columns(ColEffectiveDate).Width = 130
                gv.Columns(ColEffectiveDate).FormatString = "{0:dd/MMM/yyyy}"

                gv.Columns(ColFixedAmt).Width = 130
                gv.Columns(ColFixedAmt).FormatString = "{0:n2}"

                gv.Rows.AddNew()


                gv.AllowDeleteRow = True
                gv.AllowAddNewRow = False
                gv.ShowGroupPanel = False
                gv.AllowColumnReorder = False
                gv.AllowRowReorder = False
                gv.EnableSorting = False
                gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
                gv.MasterTemplate.ShowRowHeaderColumn = False
                gv.TableElement.TableHeaderHeight = 40
            Else
                LoadBlankGrid()
                gv.Rows.AddNew()
            End If
            Validation()
            ShowLocation()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            IsLoadData = False
        End Try
    End Sub

    Public Sub LoadDataOLD()
        If FormLoadData Then
            Exit Sub
        End If
        Try
            LoadBlankGrid()
            IsLoadData = True
            Dim arr As New List(Of ClsRouteFreightDetails)
            Dim DocType As String = clsCommon.myCstr(ddltype.Text)
            Dim TransType As String = clsCommon.myCstr(ddl_transtype.SelectedValue)
            arr = ClsRouteFreightDetails.GetData(DocType, TransType, Nothing)
            gv.Rows.AddNew()
            For Each obj As ClsRouteFreightDetails In arr
                gv.CurrentRow.Cells(ColLocationCode).Value = clsCommon.myCstr(obj.Location_Code)
                gv.CurrentRow.Cells(ColLocationName).Value = clsCommon.myCstr(obj.Location_Name)
                'If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColLocationCode).Value)) > 0 Then
                '    gv.CurrentRow.Cells(ColLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Location_Desc,'') As Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" & clsCommon.myCstr(gv.CurrentRow.Cells(ColLocationCode).Value) & "'"))
                'Else
                '    gv.CurrentRow.Cells(ColLocationName).Value = ""
                'End If
                ''done by stuti on 21/10/2016 againts ticket no BM00000007524
                gv.CurrentRow.Cells(ColToLocationCode).Value = clsCommon.myCstr(obj.ToLocation_Code)
                gv.CurrentRow.Cells(ColToLocationName).Value = clsCommon.myCstr(obj.ToLocation_Name)
                'If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColToLocationCode).Value)) > 0 Then
                '    gv.CurrentRow.Cells(ColToLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Location_Desc,'') As Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" & clsCommon.myCstr(gv.CurrentRow.Cells(ColToLocationCode).Value) & "'"))
                'Else
                '    gv.CurrentRow.Cells(ColToLocationName).Value = ""
                'End If

                gv.CurrentRow.Cells(ColCityCode).Value = clsCommon.myCstr(obj.City_Code)
                gv.CurrentRow.Cells(ColCityName).Value = clsCommon.myCstr(obj.City_Name)
                'If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColCityCode).Value)) > 0 Then
                '    gv.CurrentRow.Cells(ColCityName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(City_Name,'') As City_Name FROM TSPL_CITY_MASTER WHERE City_Code ='" & clsCommon.myCstr(gv.CurrentRow.Cells(ColCityCode).Value) & "'"))
                'Else
                '    gv.CurrentRow.Cells(ColCityName).Value = ""
                'End If
                gv.CurrentRow.Cells(ColTransportCode).Value = clsCommon.myCstr(obj.Transport_Id)
                gv.CurrentRow.Cells(ColTransportName).Value = clsCommon.myCstr(obj.Transport_Name)
                'If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(ColTransportCode).Value)) > 0 Then
                '    gv.CurrentRow.Cells(ColTransportName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Transporter_Name,'') As Transporter_Name FROM TSPL_TRANSPORT_MASTER WHERE Transport_Id ='" & clsCommon.myCstr(gv.CurrentRow.Cells(ColTransportCode).Value) & "'"))
                'Else
                '    gv.CurrentRow.Cells(ColTransportName).Value = ""
                'End If
                gv.CurrentRow.Cells(ColCapacityMT).Value = clsCommon.myCdbl(obj.CapacityMT)
                gv.CurrentRow.Cells(ColFreight).Value = clsCommon.myCdbl(obj.Freight)
                gv.CurrentRow.Cells(ColFixedAmt).Value = clsCommon.myCdbl(obj.Fixed)
                gv.CurrentRow.Cells(ColEffectiveDate).Value = clsCommon.myCstr(obj.EffectiveDate)
                gv.CurrentRow.Cells(colType).Value = clsCommon.myCstr(obj.Type)
                gv.Rows.AddNew()
            Next
            Validation()
            ShowLocation()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            IsLoadData = False
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If gv.Rows.Count > 0 Then
            SaveData()
        Else
            clsCommon.MyMessageBoxShow("Select Type", Me.Text)
        End If
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            If gv.Rows.Count > 0 Then
                Dim CityCode As String = clsCommon.myCstr(gv.CurrentRow.Cells(ColCityCode).Value)
                Dim Locationcode As String = clsCommon.myCstr(gv.CurrentRow.Cells(ColLocationCode).Value)
                Dim Type As String = clsCommon.myCstr(ddltype.SelectedValue)
                Dim TransType As String = clsCommon.myCstr(ddl_transtype.SelectedValue)
                Dim Transpotercode As String = clsCommon.myCstr(gv.CurrentRow.Cells(ColTransportCode).Value)
                Dim fromLocationcode As String = clsCommon.myCstr(gv.CurrentRow.Cells(ColToLocationCode).Value)
                Dim CapacityMT As String = clsCommon.myCstr(gv.CurrentRow.Cells(ColCapacityMT).Value)
                Dim frm As New frmchildRouteFreight()
                frm.CityCode = CityCode
                frm.Type = Type
                frm.Locationcode = Locationcode
                frm.Transpotercode = Transpotercode
                frm.TransType = TransType
                frm.ToLocation = fromLocationcode
                frm.CapacityMT = CapacityMT
                frm.WindowState = FormWindowState.Normal
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
                frm.ShowDialog()

                LoadData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message())
        End Try
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not IsLoadData AndAlso e.RowIndex >= 0 Then
            If e.Column Is gv.Columns(ColLocationCode) Then
                OpenLocCode(False)
            ElseIf e.Column Is gv.Columns(ColToLocationCode) Then
                OpenToLocCode(False)
            ElseIf e.Column Is gv.Columns(ColCityCode) Then
                OpenCityCode(False)
            ElseIf e.Column Is gv.Columns(ColTransportCode) Then
                OpenTransportCode(False)
            End If
        End If
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    'updated by preeti gupta ticket no[BM00000004761]
    'Ticket No-UDL/18/02/19-000268 sanjay add type
    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim qry As String = "select count(*) from TSPL_ROUTE_FREIGHT_DETAILS where TransType='" + clsCommon.myCstr(ddl_transtype.SelectedValue) + "' and Type='" + clsCommon.myCstr(ddltype.SelectedValue) + "'"
        Dim whrcls As String = String.Empty
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If clsCommon.CompairString(clsCommon.myCstr(ddl_transtype.SelectedValue), "P") = CompairStringResult.Equal Then
            If check <= 0 Then
                qry = "select '' as [Location Code],'' as [City Code],'' As [Transport Id],'' as [Capacity(MT)],'' as [Freight],'' as [Effective Date],'' as [Fixed Amount],'' as [Type]"
            Else
                qry = "select Location_Code as [Location Code] ,City_Code as [City Code],Transport_Id AS [Transport Id],CapacityMT as [Capacity(MT)],Freight as [Freight] ,Effective_Date as [Effective Date],Fixed as [Fixed Amount],Type as [Type] from TSPL_ROUTE_FREIGHT_DETAILS "
                whrcls += " and TSPL_ROUTE_FREIGHT_DETAILS.TransType='P' and Type='" + clsCommon.myCstr(ddltype.SelectedValue) + "'"
            End If
        Else
            If check <= 0 Then
                qry = "select '' as [Location Code],'' as [To Location Code],'' As [Transport Id],'' as [Capacity(MT)],'' as [Freight],'' as [Effective Date],'' as [Fixed Amount],'' as [Type]"
            Else
                qry = "select Location_Code as [Location Code] ,ToLocation_Code as [To Location Code],Transport_Id AS [Transport Id],CapacityMT as [Capacity(MT)],Freight as [Freight] ,Effective_Date as [Effective Date],Fixed as [Fixed Amount],Type as [Type] from TSPL_ROUTE_FREIGHT_DETAILS "
                whrcls = " and TSPL_ROUTE_FREIGHT_DETAILS.TransType='T' and Type='" + clsCommon.myCstr(ddltype.SelectedValue) + "'"
            End If
        End If
        
        transportSql.ExporttoExcel(qry, whrcls, Me)
    End Sub
    'updated by preeti gupta ticket no[BM00000004761]
    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        'Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim gvbool As Boolean = False
        Dim TransType As String = clsCommon.myCstr(ddl_transtype.SelectedValue)
        If clsCommon.CompairString(TransType, "P") = CompairStringResult.Equal Then
            gvbool = transportSql.importExcel(gv, "Location Code", "City Code", "Transport Id", "Capacity(MT)", "Freight", "Effective Date", "Fixed Amount", "Type")
        Else
            gvbool = transportSql.importExcel(gv, "Location Code", "To Location Code", "Transport Id", "Capacity(MT)", "Freight", "Effective Date", "Fixed Amount", "Type")
        End If
        If gvbool Then
            Dim linno As Integer = 1
            Try
                Dim arr As New List(Of ClsRouteFreightDetails)
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New ClsRouteFreightDetails()

                    Dim strLocCode As String = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    Dim strCityCode As String = Nothing
                    Dim strToLocCode As String = Nothing
                    If clsCommon.CompairString(TransType, "P") = CompairStringResult.Equal Then
                        strCityCode = clsCommon.myCstr(grow.Cells("City Code").Value)
                    Else
                        strToLocCode = clsCommon.myCstr(grow.Cells("To Location Code").Value)
                    End If

                    Dim strTransportCode As String = clsCommon.myCstr(grow.Cells("Transport Id").Value)
                    Dim strCapacity As String = clsCommon.myCstr(grow.Cells("Capacity(MT)").Value)
                    Dim strFreight As String = clsCommon.myCstr(grow.Cells("Freight").Value)
                    Dim strdate As String = clsCommon.myCstr(grow.Cells("Effective Date").Value)
                    Dim strFixedAmt As String = clsCommon.myCstr(grow.Cells("Fixed Amount").Value)
                    Dim strType As String = clsCommon.myCstr(grow.Cells("Type").Value)
                    linno += 1


                    If clsCommon.myLen(strLocCode) <= 0 Then
                        Throw New Exception("Location Code should not be left blank ")
                    End If

                    If clsDBFuncationality.getSingleValue("select count(*) from TSPL_LOCATION_MASTER where Location_Code='" & strLocCode & "'", trans) = 0 Then
                        Throw New Exception(" Location Code could not found in Location Master")
                    End If

                    If clsCommon.CompairString(TransType, "P") = CompairStringResult.Equal Then
                        If clsDBFuncationality.getSingleValue("select count(*) from TSPL_CITY_MASTER where City_Code='" & strCityCode & "'", trans) = 0 Then
                            Throw New Exception(" City Code could not found in City Master")
                        End If
                    Else
                        If clsDBFuncationality.getSingleValue("select count(*) from TSPL_LOCATION_MASTER where Location_Code='" & strToLocCode & "'", trans) = 0 Then
                            Throw New Exception(" To Location Code could not found in Location Master")
                        End If
                    End If
                    

                    If clsCommon.myLen(strTransportCode) <= 0 Then
                        Throw New Exception("Transport Id should not be left blank")
                    End If

                    If clsDBFuncationality.getSingleValue("select count(*) from TSPL_TRANSPORT_MASTER where Transport_Id='" & strTransportCode & "'", trans) = 0 Then
                        Throw New Exception(" Transport Id could not found in Transport Master")
                    End If

                    If Not clsCommon.CompairString(clsCommon.myCstr(ddltype.SelectedValue), "KG") = CompairStringResult.Equal Then

                        If clsCommon.myCdbl(strCapacity) = 0 Then
                            Throw New Exception("Capacity can not be left blank or zero ")

                        End If
                    End If

                    If clsCommon.myCdbl(strCapacity) < 0 Then
                        Throw New Exception("Capacity can not be negative at line no")

                    End If

                    If clsCommon.CompairString(strType, "Fixed") <> CompairStringResult.Equal Then
                        If clsCommon.myCdbl(strFreight) = 0 Then
                            Throw New Exception("Freight can not be left blank or zero")

                        End If
                    End If

                    If clsCommon.myCdbl(strFreight) < 0 Then
                        Throw New Exception("Freight can not be negative at line no. ")

                    End If

                    If Not clsCommon.CompairString(clsCommon.myCstr(ddltype.SelectedValue), "KG") = CompairStringResult.Equal Then

                        If clsCommon.CompairString(strType, "MT") = CompairStringResult.Equal Then
                        Else
                            If clsCommon.myCdbl(strFixedAmt) = 0 Then
                                Throw New Exception("FixedAmt can not be left blank or zero")

                            End If
                        End If
                    End If

                    If clsCommon.myCdbl(strFixedAmt) < 0 Then
                        Throw New Exception("FixedAmt can not be negative at line no. ")

                    End If
                    If clsCommon.myLen(strdate) <= 0 Then
                        Throw New Exception("Effective date can not be left blank at line no.")

                    End If
                    If clsCommon.myLen(strType) <= 0 Then
                        Throw New Exception("Type can not be left blank at line no.")
                    End If

                    If clsCommon.CompairString(TransType, "T") = CompairStringResult.Equal Then
                        If Not clsCommon.CompairString(strType, "MT") = CompairStringResult.Equal Then
                            Throw New Exception("Type must be 'MT' for transaction type='T'")
                        End If
                    End If

                    'Ticket No-UDL/18/02/19-000268 sanjay
                    If Not clsCommon.CompairString(clsCommon.myCstr(ddltype.SelectedValue), "KG") = CompairStringResult.Equal Then
                        obj.CapacityMT = strCapacity
                        obj.Fixed = strFixedAmt
                    Else
                        obj.CapacityMT = "0"
                        obj.Fixed = "0"
                    End If

                    obj.City_Code = strCityCode
                    obj.ToLocation_Code = strToLocCode
                    obj.Location_Code = strLocCode

                    obj.Freight = strFreight
                    obj.EffectiveDate = strdate

                    obj.Transport_Id = strTransportCode
                    obj.Type = strType
                    obj.TransType = TransType
                    arr.Add(obj)

                Next
                trans.Commit()
                ClsRouteFreightDetails.SaveData(ddltype.SelectedValue, TransType, arr)
                LoadData()

                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)

            Catch ex As Exception
                clsCommon.MyMessageBoxShow(ex.Message & " At Line No : " & linno)
                trans.Rollback()

            End Try
        End If
    End Sub

    Private Sub ddltype_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddltype.SelectedIndexChanged
        'LoadData()
        'ShowLocation()
        'Validation()

    End Sub
    Public Sub Validation()
        Try
            'LoadData()
            If clsCommon.CompairString(clsCommon.myCstr(ddltype.SelectedValue), "Both") = CompairStringResult.Equal Then
                gv.Columns(ColFreight).ReadOnly = False
                gv.Columns(ColFixedAmt).ReadOnly = False
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddltype.SelectedValue), "MT") = CompairStringResult.Equal Then
                gv.Columns(ColFreight).ReadOnly = False
                gv.Columns(ColFixedAmt).ReadOnly = True
            ElseIf clsCommon.CompairString(clsCommon.myCstr(ddltype.SelectedValue), "Fixed") = CompairStringResult.Equal Then
                gv.Columns(ColFreight).ReadOnly = True
                gv.Columns(ColFixedAmt).ReadOnly = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

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

        '===========Added by preeti gupta [14/01./2019]
        dr = dt.NewRow()
        dr("Code") = "KG"
        dr("Name") = "KG"
        dt.Rows.Add(dr)
        '==============================================

        dr = dt.NewRow()
        dr("Code") = "Both"
        dr("Name") = "Both"
        dt.Rows.Add(dr)

        ddltype.DataSource = dt
        ddltype.ValueMember = "Code"
        ddltype.DisplayMember = "Name"

    End Sub

    Private Sub ddl_transtype_SelectedValueChanged(sender As Object, e As EventArgs) Handles ddl_transtype.SelectedValueChanged
        Try
            'IsLoadData = True
            LoadData()
            'IsLoadData = False
        Catch ex As Exception
            IsLoadData = False

        End Try
        
    End Sub

    Public Sub ShowLocation()
        If ddltype.Items.Count > 0 AndAlso ddl_transtype.Items.Count > 0 Then
            If clsCommon.CompairString(clsCommon.myCstr(ddl_transtype.SelectedValue), "T") = CompairStringResult.Equal Then
                ddltype.SelectedValue = "MT"
            End If
            If Not clsCommon.CompairString(clsCommon.myCstr(ddltype.SelectedValue), "MT") = CompairStringResult.Equal Then
                ddl_transtype.SelectedValue = "P"
            End If
            If clsCommon.CompairString(clsCommon.myCstr(ddl_transtype.SelectedValue), "T") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(ddltype.SelectedValue), "MT") = CompairStringResult.Equal Then
                gv.Columns(ColCityCode).IsVisible = False
                gv.Columns(ColCityName).IsVisible = False
                gv.Columns(ColCityCode).VisibleInColumnChooser = False
                gv.Columns(ColCityName).VisibleInColumnChooser = False
                gv.Columns(ColToLocationCode).IsVisible = True
                gv.Columns(ColToLocationName).IsVisible = True
            Else
                gv.Columns(ColCityCode).IsVisible = True
                gv.Columns(ColCityName).IsVisible = True
                gv.Columns(ColToLocationCode).IsVisible = False
                gv.Columns(ColToLocationName).IsVisible = False
                gv.Columns(ColToLocationCode).VisibleInColumnChooser = False
                gv.Columns(ColToLocationName).VisibleInColumnChooser = False
            End If
        End If
    End Sub

    Private Sub ddltype_SelectedValueChanged(sender As Object, e As EventArgs) Handles ddltype.SelectedValueChanged
        LoadData()
    End Sub
End Class
