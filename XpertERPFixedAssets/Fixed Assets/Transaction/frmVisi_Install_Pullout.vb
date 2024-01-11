'-Creted By--[Pankaj Kumar Chaudhary]
'-Updation By-[Pankaj Kumar Chaudhary]--Against Ticket No-[BM00000001773]
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class FrmVisi_Install_Pullout
    Inherits FrmMainTranScreen
    Dim Qry As String
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ArrDb As New List(Of String)
    Const colLineNo As String = "LineNo"
    Const colSelect As String = "Select"
    Const colVisiNo As String = "VisiNo"
    Const colVisiMake As String = "VisiMake"
    Const colAssetNo As String = "AssetNo"
    Const colModelNo As String = "ModelNo"
    Const colVisiSize As String = "VisiSize"
    Const colLoc As String = "LocCode"
    Const colLocDesc As String = "LocName"
    Const colRoute As String = "RouteCode"
    Const colROuteDesc As String = "RouteName"
    Const coLTtype As String = "Type"
    Const colTransDate As String = "Trans_Date"
    Dim IsInsideLoadData As Boolean = True

    Private Sub FrmVisi_Install_Pullout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkInstall.IsChecked = True
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        ArrDb.Add(objCommonVar.CurrDatabase)

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadCustomer(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmVisi_Install_Pullout)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        ' btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadBlankGrid()
        dgvVisi.Rows.Clear()
        dgvVisi.Columns.Clear()

        Dim LineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        LineNo = New GridViewDecimalColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 61
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        dgvVisi.MasterTemplate.Columns.Add(LineNo)

        Dim CollSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        CollSelect.FormatString = ""
        CollSelect.HeaderText = "Select"
        CollSelect.Name = colSelect
        CollSelect.Width = 61
        CollSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        dgvVisi.MasterTemplate.Columns.Add(CollSelect)

        Dim VisiNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        VisiNo.FormatString = ""
        VisiNo.HeaderText = "Visi No"
        VisiNo.Name = colVisiNo
        VisiNo.Width = 121
        VisiNo.MaxLength = 50
        VisiNo.ReadOnly = True
        VisiNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(VisiNo)

        Dim VisiMake As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        VisiMake.FormatString = ""
        VisiMake.HeaderText = "Visi Make"
        VisiMake.Name = colVisiMake
        VisiMake.Width = 121
        VisiMake.MaxLength = 50
        VisiMake.IsVisible = False
        VisiMake.ReadOnly = True
        VisiMake.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(VisiMake)

        Dim AssetNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        AssetNo.FormatString = ""
        AssetNo.HeaderText = "Asset No"
        AssetNo.Name = colAssetNo
        AssetNo.Width = 121
        AssetNo.MaxLength = 50
        'AssetNo.IsVisible = False
        AssetNo.ReadOnly = True
        AssetNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(AssetNo)

        Dim ModelNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ModelNo.FormatString = ""
        ModelNo.HeaderText = "Model No"
        ModelNo.Name = colModelNo
        ModelNo.Width = 121
        ModelNo.MaxLength = 50
        ModelNo.ReadOnly = True
        ModelNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(ModelNo)


        Dim VisiSize As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        VisiSize.FormatString = ""
        VisiSize.HeaderText = "Visi Size"
        VisiSize.Name = colVisiSize
        VisiSize.Width = 121
        VisiSize.MaxLength = 50
        VisiSize.IsVisible = False
        VisiSize.ReadOnly = True
        VisiSize.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(VisiSize)

        Dim Location As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Location.FormatString = ""
        Location.HeaderText = "Location"
        Location.Name = colLoc
        Location.Width = 101
        Location.MaxLength = 50
        Location.ReadOnly = False
        Location.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(Location)

        Dim Route As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Route.FormatString = ""
        Route.HeaderText = "Route"
        Route.Name = colRoute
        Route.Width = 101
        Route.MaxLength = 50
        Route.ReadOnly = False
        Route.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(Route)


        Dim Type As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        Type.FormatString = ""
        Type.HeaderText = "Type"
        Type.Name = coLTtype
        Type.Width = 101
        Type.ReadOnly = False
        Type.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Type.DataSource = GetItemType()
        Type.ValueMember = "Code"
        Type.DisplayMember = "Code"
        dgvVisi.MasterTemplate.Columns.Add(Type)

        Dim transDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        transDate.FormatString = ""
        transDate.Format = DateTimePickerFormat.Custom
        transDate.CustomFormat = "dd/MMM/yyyy"
        If chkInstall.IsChecked Then
            transDate.HeaderText = "Installation Date"
        Else
            transDate.HeaderText = "Pullout Date"
        End If
        transDate.Name = colTransDate
        transDate.Width = 101
        transDate.ReadOnly = False
        transDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvVisi.MasterTemplate.Columns.Add(transDate)

        dgvVisi.EnableFiltering = True
        dgvVisi.AllowDeleteRow = False
        dgvVisi.ShowGroupPanel = False
        dgvVisi.AllowColumnReorder = False
        dgvVisi.AllowRowReorder = False
        dgvVisi.EnableSorting = False
        dgvVisi.AllowAddNewRow = False
        dgvVisi.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvVisi.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "New"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Refurnished"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Sub Reset()
        fndCustomer.Value = ""
        lblCustomerName.Text = ""
        lblRoute.Text = ""
        LoadBlankGrid()
    End Sub

    Private Sub fndCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomer._MYValidating
        Qry = "select Cust_Code as [CustomerCode],Customer_Name as [Name],Cust_Group_Code as [Customer Group],(select case when Status='N' then 'Active' else 'Inactive' end ) as [Status] from TSPL_CUSTOMER_MASTER  "
        fndCustomer.Value = clsCommon.ShowSelectForm("CustFinderInVisiPullout", Qry, "CustomerCode", "", fndCustomer.Value, "", isButtonClicked)
        LoadCustomer(fndCustomer.Value, NavigatorType.Current)
    End Sub

    Private Sub fndCustomer__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCustomer._MYNavigator
        LoadCustomer(fndCustomer.Value, NavType)
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If AllowToSave() Then
                Dim Arr As New List(Of clsVisiInstallPullout)
                For Each grow As GridViewRowInfo In dgvVisi.Rows
                    Dim objTr As New clsVisiInstallPullout()
                    If grow.Cells(colSelect).Value = True Then
                        objTr.Customer_Id = clsCommon.myCstr(fndCustomer.Value)
                        objTr.Visi_Id = clsCommon.myCstr(grow.Cells(colVisiNo).Value)
                        objTr.VisiMake = clsCommon.myCstr(grow.Cells(colVisiMake).Value)
                        objTr.Location = clsCommon.myCstr(grow.Cells(colLoc).Value)
                        objTr.Route = clsCommon.myCstr(grow.Cells(colRoute).Value)
                        If chkInstall.IsChecked Then
                            objTr.Trans_Type = "Installed"
                        Else
                            objTr.Trans_Type = "Pulled Out"
                        End If
                        objTr.Type = clsCommon.myCstr(grow.Cells(coLTtype).Value)
                        objTr.Trans_Date = clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells(colTransDate).Value), "dd/MMM/yyyy")
                        Arr.Add(objTr)
                    End If
                Next

                If (clsVisiInstallPullout.SaveData(Arr, ArrDb)) Then
                    RadMessageBox.Show("Data Saved Successfully")
                    LoadCustomer(fndCustomer.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(fndCustomer.Value) > 0 Then
            Dim Counter As Integer = 0
            For i As Integer = 0 To dgvVisi.Rows.Count - 1
                If dgvVisi.Rows(i).Cells(colSelect).Value = True Then
                    Counter += 1
                    If clsCommon.myLen(dgvVisi.Rows(i).Cells(colTransDate).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Please select Date At Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                        Return False
                    ElseIf clsCommon.myLen(dgvVisi.Rows(i).Cells(coLTtype).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Please Select Type At Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                        Return False
                    ElseIf clsCommon.myLen(dgvVisi.Rows(i).Cells(colLoc).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Please Select Location At Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                        Return False
                    ElseIf clsCommon.myLen(dgvVisi.Rows(i).Cells(colRoute).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Please Select Route At Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                        Return False
                    End If
                    Qry = "Select Visi_Installation_date from TSPL_VISI_MASTER Where Visi_Id='" + dgvVisi.Rows(i).Cells(colVisiNo).Value + "' AND Customer_Id='" + fndCustomer.Value + "'"
                    Dim InstallationDate As Date = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue(Qry), "dd/MMM/yyyy")
                    Dim PulloutDate As Date = clsCommon.GetPrintDate(dgvVisi.Rows(i).Cells(colTransDate).Value, "dd/MMM/yyyy")
                    Dim ts As TimeSpan = PulloutDate - InstallationDate
                    If CInt(ts.TotalDays) < 0 Then
                        clsCommon.MyMessageBoxShow(Me, "You cann't pullout VISI - " + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colVisiNo).Value) + " Before date - " + InstallationDate + " at Line '" + clsCommon.myCstr(dgvVisi.Rows(i).Cells(colLineNo).Value) + "'")
                        Return False
                    End If
                End If
            Next
            If Counter > 0 Then
                Return True
            Else
                clsCommon.MyMessageBoxShow(Me, "Please Select Atleast Single Row", Me.Text)
                Return False
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "Please Select Customer", Me.Text)
            fndCustomer.Focus()
            Return False
        End If
        Return True
    End Function


    Private Sub LoadCustomer(ByVal strCustCode As String, ByVal navType As common.NavigatorType)
        Try
            Reset()
            IsInsideLoadData = True
            dt = clsVisiMaster.GetDataForVisiInstallPullout(strCustCode, navType)
            If dt.Rows.Count > 0 Then
                fndCustomer.Value = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
                lblCustomerName.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
                lblRoute.Text = clsCommon.myCstr(dt.Rows(0)("Route_No")) + " - " + clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            End If
            If chkInstall.IsChecked Then
                LoadDetails(fndCustomer.Value, "Install")
            Else
                LoadDetails(fndCustomer.Value, "Pullout")
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        Finally
            IsInsideLoadData = False
        End Try
    End Sub

    Private Sub LoadDetails(ByVal strCustCode As String, ByVal StrTransType As String)
        Try
            LoadBlankGrid()
            If clsCommon.CompairString(StrTransType, "Install") = CompairStringResult.Equal Then
                Qry = "Select Case When (ISNULL(Visi_Installation_date,'')<>'' AND ISNULL(Customer_Id,'')<>'') Then CAST(1 AS bit) Else CAST(0 as Bit) End As [Select], "
                Qry += " Visi_Id, VisiMake, Asset_No, Model_No, Visi_Size, Visi_Installation_date, Pull_Out_Date, CAse When TSPL_VISI_MASTER.Type='N' Then 'New' When TSPL_VISI_MASTER.Type='R' Then 'Refurnished' Else '' End as Visi_Type,"
                Qry += " TSPL_VISI_MASTER.Location, TSPL_LOCATION_MASTER.Location_Desc, TSPL_VISI_MASTER.Route, TSPL_ROUTE_MASTER.Route_Desc from TSPL_VISI_MASTER "
                Qry += " LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_VISI_MASTER.Route"
                Qry += " LEFT OUTER JOIN TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_VISI_MASTER.Location"
                Qry += " WHERE Customer_Id='" + strCustCode + "' OR ISNULL(Customer_Id,'')=''"
            Else
                Qry = "Select CAST(0 as Bit) As [Select], "
                Qry += " Visi_Id, VisiMake, Asset_No, Model_No, Visi_Size, Visi_Installation_date, Pull_Out_Date, CAse When TSPL_VISI_MASTER.Type='N' Then 'New' When TSPL_VISI_MASTER.Type='R' Then 'Refurnished' Else '' End as Visi_Type,"
                Qry += " TSPL_VISI_MASTER.Location, TSPL_LOCATION_MASTER.Location_Desc, TSPL_VISI_MASTER.Route, TSPL_ROUTE_MASTER.Route_Desc from TSPL_VISI_MASTER "
                Qry += " LEFT OUTER JOIN TSPL_ROUTE_MASTER ON TSPL_ROUTE_MASTER.Route_No=TSPL_VISI_MASTER.Route"
                Qry += " LEFT OUTER JOIN TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_VISI_MASTER.Location"
                Qry += " WHERE Customer_Id='" + strCustCode + "'"
            End If
            dt = clsDBFuncationality.GetDataTable(Qry)
            Dim ii As Integer = 0
            For Each dr As DataRow In dt.Rows
                dgvVisi.Rows.AddNew()
                ii += 1
                dgvVisi.CurrentRow.Cells(colLineNo).Value = ii
                dgvVisi.CurrentRow.Cells(colSelect).Value = dr("Select")
                dgvVisi.CurrentRow.Cells(colVisiNo).Value = clsCommon.myCstr(dr("Visi_Id"))
                dgvVisi.CurrentRow.Cells(colVisiMake).Value = clsCommon.myCstr(dr("VisiMake"))
                dgvVisi.CurrentRow.Cells(colAssetNo).Value = clsCommon.myCstr(dr("Asset_No"))
                dgvVisi.CurrentRow.Cells(colModelNo).Value = clsCommon.myCstr(dr("Model_No"))
                dgvVisi.CurrentRow.Cells(colVisiSize).Value = clsCommon.myCstr(dr("Visi_Size"))
                dgvVisi.CurrentRow.Cells(coLTtype).Value = clsCommon.myCstr(dr("Visi_Type"))
                dgvVisi.CurrentRow.Cells(colLoc).Value = clsCommon.myCstr(dr("Location"))
                dgvVisi.CurrentRow.Cells(colRoute).Value = clsCommon.myCstr(dr("Route"))

                If dgvVisi.CurrentRow.Cells(colSelect).Value = True Then
                    dgvVisi.CurrentRow.Cells(colTransDate).Value = clsCommon.myCstr(dr("Visi_Installation_date"))
                    dgvVisi.CurrentRow.Cells(colSelect).ReadOnly = True
                    dgvVisi.CurrentRow.Cells(colTransDate).ReadOnly = True
                Else
                    dgvVisi.CurrentRow.Cells(colVisiSize).Value = Nothing
                    dgvVisi.CurrentRow.Cells(colSelect).ReadOnly = False
                    dgvVisi.CurrentRow.Cells(colTransDate).ReadOnly = False
                End If
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Reset()
    End Sub


    Private Sub OpenLocation(ByVal StrCode As String, ByVal IsButtonCLicked As Boolean)
        Qry = "Select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER "
        dgvVisi.CurrentRow.Cells(colLoc).Value = clsCommon.ShowSelectForm("LocFilter", Qry, "Code", "Location_Type='Physical'", StrCode, "Code", IsButtonCLicked)
    End Sub
    Private Sub OpenRoute(ByVal StrCode As String, ByVal IsButtonCLicked As Boolean)
        Qry = "Select Route_No as Code, Route_Desc  as Description from TSPL_ROUTE_MASTER"
        dgvVisi.CurrentRow.Cells(colRoute).Value = clsCommon.ShowSelectForm("RouteFilter", Qry, "Code", "", StrCode, "Code", IsButtonCLicked)
    End Sub

    Private Sub dgvVisi_CellValueChanged_1(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvVisi.CellValueChanged
        If IsInsideLoadData = False Then
            Dim strCode As String
            If e.Column Is dgvVisi.Columns(colLoc) Then
                strCode = dgvVisi.CurrentRow.Cells(colLoc).Value
                OpenLocation(strCode, False)
            ElseIf e.Column Is dgvVisi.Columns(colRoute) Then
                strCode = dgvVisi.CurrentRow.Cells(colRoute).Value
                OpenRoute(strCode, False)
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkInstall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkInstall.ToggleStateChanged, chkPullOut.ToggleStateChanged
        If clsCommon.myLen(fndCustomer.Value) > 0 Then
            If chkInstall.IsChecked Then
                LoadDetails(fndCustomer.Value, "Install")
            Else
                LoadDetails(fndCustomer.Value, "Pulout")
            End If
        End If
    End Sub

End Class
