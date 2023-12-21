'-25/07/2012-Updation by-Pankaj Kumar ---- added location filter-------------------------by---Ranjana Mam
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common


Public Class frmRouteListReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RouteListReport11)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub frmRouteListReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        funreset()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")

        
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub


    Sub LoadVendor()
        Dim qry As String = "select route_no,route_desc from TSPL_ROUTE_MASTER order by route_no"
        cbgroute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgroute.ValueMember = "route_no"
        cbgroute.DisplayMember = "route_desc"

    End Sub

    Sub LoadCustomerType()
        Dim qry As String = "select cust_type_code,cust_type_desc from TSPL_CUSTOMER_TYPE_MASTER "
        cbgcustomertype.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgcustomertype.ValueMember = "cust_type_code"
        cbgcustomertype.DisplayMember = "cust_type_desc"

    End Sub

    Private Sub chkAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAll.ToggleStateChanged
        cbgroute.Enabled = Not chkAll.IsChecked
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
       
    End Sub
    Sub funreset()
        LoadVendor()
        chkAll.IsChecked = True
        LoadLocation()
        chkLocAll.IsChecked = True
        LoadCustomerType()
        chkcustall.IsChecked = True
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Print()
    End Sub
    Sub Print()
        Try

            Dim LocationFilter As String
            Dim RouteCodeFilter As String
            Dim CustCodeFilter As String

            ' Dim qry As String = " SELECT TSPL_ROUTE_MASTER.Route_No, TSPL_ROUTE_MASTER.Route_Desc, TSPL_CUSTOMER_MASTER.Cust_Code, " & _
            '          " TSPL_CUSTOMER_MASTER.Customer_Name, ((TSPL_CUSTOMER_MASTER.Add1) + (case when TSPL_CUSTOMER_MASTER.Add1='' then '' else ' , 'end) )+( TSPL_CUSTOMER_MASTER.Add2)+(case when TSPL_CUSTOMER_MASTER.Add2='' then '' else ' , 'end)+( TSPL_CUSTOMER_MASTER.Add3 )+((TSPL_CITY_MASTER.City_Name)+(case when TSPL_CUSTOMER_MASTER.Add3='' then '' else ' , 'end)) as Address, " & _
            '          " TSPL_CUSTOMER_MASTER.City_Code, TSPL_CUSTOMER_MASTER.Cust_Group_Code, TSPL_CUSTOMER_MASTER.Cust_Category_Code, " & _
            '          " TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC, TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Logo_Img, " & _
            '" TSPL_COMPANY_MASTER.Logo_Img2, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, TSPL_CITY_MASTER.City_Name " & _
            '" FROM         TSPL_CITY_MASTER INNER JOIN " & _
            '         "  TSPL_CUSTOMER_GROUP_MASTER INNER JOIN " & _
            '          " TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = TSPL_CUSTOMER_MASTER.Cust_Group_Code INNER JOIN " & _
            '         "  TSPL_CUSTOMER_CATEGORY_MASTER ON " & _
            '         "  TSPL_CUSTOMER_MASTER.Cust_Category_Code = TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE ON " & _
            '          " TSPL_CITY_MASTER.City_Code = TSPL_CUSTOMER_MASTER.City_Code RIGHT OUTER JOIN " & _
            '          " TSPL_COMPANY_MASTER INNER JOIN " & _
            '          " TSPL_ROUTE_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_ROUTE_MASTER.Comp_Code ON " & _
            '         "  TSPL_CUSTOMER_MASTER.Route_No = TSPL_ROUTE_MASTER.Route_No"

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            'Dim qry As String = "SELECT TSPL_ROUTE_MASTER.Route_No,  TSPL_ROUTE_MASTER.Route_Desc,  TSPL_CUSTOMER_MASTER.Cust_Code, " & _
            '"TSPL_CUSTOMER_MASTER.Customer_Name, " & _
            '"(Case When TSPL_CUSTOMER_MASTER.Add1='' Then '' else convert(Varchar,TSPL_CUSTOMER_MASTER.Add1, 103)  + case when TSPL_CUSTOMER_MASTER.Add2='' then '' else ', '+ Convert(Varchar, TSPL_CUSTOMER_MASTER.Add2, 103)+ case when TSPL_CUSTOMER_MASTER.Add3='' then '' else ', '+ Convert(varchar, TSPL_CUSTOMER_MASTER.Add3, 103)+  case When TSPL_CITY_MASTER.City_Name='' then '' else ', '+ Convert(Varchar, TSPL_CITY_MASTER.City_Name, 103) end end end end ) as Address,   " & _
            '"TSPL_CUSTOMER_MASTER.City_Code,  TSPL_CUSTOMER_MASTER.Cust_Group_Code,   TSPL_CUSTOMER_MASTER.Cust_Category_Code,   " & _
            '"TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC,  TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Logo_Img,  TSPL_COMPANY_MASTER.Logo_Img2, " & _
            '"TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, TSPL_CITY_MASTER.City_Name  FROM TSPL_ROUTE_MASTER " & _
            '"Left Outer  Join TSPL_CUSTOMER_MASTER on TSPL_ROUTE_MASTER.Route_No=  TSPL_CUSTOMER_MASTER.Route_No " & _
            '"Left Outer Join  TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.Cust_Category_Code  " & _
            '"Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_ROUTE_MASTER.Comp_Code " & _
            '"Left Outer Join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code = TSPL_CUSTOMER_MASTER.City_Code " & _
            '"Inner Join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = TSPL_CUSTOMER_MASTER.Cust_Group_Code"
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            If chkSelect.IsChecked = True AndAlso cbgroute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast Single Route", Me.Text)
                Return
            Else
                RouteCodeFilter = clsCommon.GetMulcallString(cbgroute.CheckedValue)
                RouteCodeFilter = RouteCodeFilter.Replace("'", "")
            End If
            If chkLocSelect.IsChecked = True AndAlso dgvLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast single Location Or Select All", Me.Text)
                Return
            Else
                LocationFilter = clsCommon.GetMulcallString(dgvLocation.CheckedValue)
                LocationFilter = LocationFilter.Replace("'", "")
            End If
            If chkcustselect.IsChecked = True AndAlso cbgcustomertype.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast single CustomerType Or Select All", Me.Text)
                Return
            Else
                CustCodeFilter = clsCommon.GetMulcallString(cbgcustomertype.CheckedValue)
                CustCodeFilter = CustCodeFilter.Replace("'", "")
            End If
            Dim qry As String = " Select '" + CustCodeFilter + "' as CustCodeFilter,'" + LocationFilter + "' as LocFilter,'" + RouteCodeFilter + "' as RouteCodeFilter, *,  (select top 1 Sale_Invoice_No+'-'+ CONVERT(varchar(10), Sale_Invoice_Date,103) from TSPL_SALE_INVOICE_HEAD where TSPL_SALE_INVOICE_HEAD.Cust_Code= xxx.Cust_Code  order by Date_Time_Removal desc) as LastMemoAndDate from ( SELECT TSPL_ROUTE_MASTER.Route_No, (TSPL_ROUTE_MASTER.Route_No+' - '+ Convert( varchar, TSPL_ROUTE_MASTER.Route_Desc, 102)) as Route_NoNDesc,  TSPL_ROUTE_MASTER.Route_Desc,  TSPL_CUSTOMER_MASTER.Cust_Code, " & _
            " TSPL_CUSTOMER_MASTER.Customer_Name, " & _
            " (Case When TSPL_CUSTOMER_MASTER.Add1='' Then '' else convert(Varchar,TSPL_CUSTOMER_MASTER.Add1, 103)  + case when TSPL_CUSTOMER_MASTER.Add2='' then '' else ', '+ Convert(Varchar, TSPL_CUSTOMER_MASTER.Add2, 103)+ case when TSPL_CUSTOMER_MASTER.Add3='' then '' else ', '+ Convert(varchar, TSPL_CUSTOMER_MASTER.Add3, 103)+  case When TSPL_CITY_MASTER.City_Name='' then '' else ', '+ Convert(Varchar, TSPL_CITY_MASTER.City_Name, 103) end end end end ) as Address, " & _
            " TSPL_CUSTOMER_MASTER.City_Code,  TSPL_CUSTOMER_MASTER.Cust_Group_Code, TSPL_CITY_MASTER.City_Name,   TSPL_CUSTOMER_MASTER.Cust_Category_Code,   TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_DESC,  " & _
            " TSPL_COMPANY_MASTER.Comp_Name, TSPL_COMPANY_MASTER.Logo_Img,  TSPL_COMPANY_MASTER.Logo_Img2, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, " & _
            " TSPL_CUSTOMER_MASTER.Channel_Code, " & _
            " (Case when TSPL_ROUTE_GROUP_MASTER.Monday='T' Then 'M'+', ' else '' end + Case when TSPL_ROUTE_GROUP_MASTER.Tuesday='T' then  'T'+',' else '' end +Case When TSPL_ROUTE_GROUP_MASTER.Wednesday ='T' Then 'W'+',' else '' end + Case When TSPL_ROUTE_GROUP_MASTER.Thursday ='T' then 'Th'+',' else '' end + case When TSPL_ROUTE_GROUP_MASTER.Friday='T' Then 'F'+',' else'' End + case When TSPL_ROUTE_GROUP_MASTER.Saturday='T' then 'S'+',' else '' end+ Case when TSPL_ROUTE_GROUP_MASTER.Sunday='T' then 'Sun' else '' end ) as [DaysOfOperation], ( Select Count(Customer_Id ) as NoOfVisi from TSPL_VISI_MASTER Where Customer_Id = TSPL_CUSTOMER_MASTER.Cust_Code ) as NoOfVisi,  " & _
            " TSPL_ROUTE_GROUP_MASTER.Group_Id, (TSPL_ROUTE_GROUP_MASTER.Group_Id+' - '+CONVERT(Varchar,TSPL_ROUTE_GROUP_MASTER.Description, 102)) as [Group_IdnDesc] " & _
            " From TSPL_ROUTE_MASTER Left Outer  Join TSPL_CUSTOMER_MASTER on TSPL_ROUTE_MASTER.Route_No=  TSPL_CUSTOMER_MASTER.Route_No " & _
            " Left Outer Join  TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.CUST_CATEGORY_CODE=TSPL_CUSTOMER_MASTER.Cust_Category_Code  " & _
            " Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_ROUTE_MASTER.Comp_Code " & _
            " Left Outer Join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code = TSPL_CUSTOMER_MASTER.City_Code " & _
            " Inner Join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = TSPL_CUSTOMER_MASTER.Cust_Group_Code " & _
            " Left Outer Join  TSPL_ROUTE_GROUP_MASTER on TSPL_ROUTE_GROUP_MASTER.Route_Code=TSPL_CUSTOMER_MASTER.Route_No AND TSPL_ROUTE_GROUP_MASTER.Group_Id=TSPL_CUSTOMER_MASTER.Route_Group " & _
            "left outer join TSPL_CUSTOMER_TYPE_MASTER on TSPL_CUSTOMER_MASTER.Cust_Type_Code=TSPL_CUSTOMER_TYPE_MASTER.cust_type_code  Where 1=1 "
            If chkSelect.IsChecked = True AndAlso cbgroute.CheckedValue.Count > 0 Then
                qry += " AND TSPL_ROUTE_MASTER.Route_No  in (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ")"
            End If
            If chkLocSelect.IsChecked = True AndAlso dgvLocation.CheckedValue.Count > 0 Then
                qry += " AND TSPL_ROUTE_MASTER.Depot_Id  in (" + clsCommon.GetMulcallString(dgvLocation.CheckedValue) + ")"
            End If
            If chkcustselect.IsChecked = True AndAlso cbgcustomertype.CheckedValue.Count > 0 Then
                qry += " AND TSPL_CUSTOMER_TYPE_MASTER.cust_type_code  in (" + clsCommon.GetMulcallString(cbgcustomertype.CheckedValue) + ")"
            End If
            qry += " ) as xxx Order By xxx.Group_Id "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.Purchase, dt, "rptRouteList", "Route List Report")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "RT-LIST-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            'btnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            'btndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Private Sub frmRouteListReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub

    '-25/07/2012-Added by-Pankaj Kumar while added location filter-------------------------by---Ranjana Mam
    Sub LoadLocation()
        Dim qry As String = "Select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        dgvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        dgvLocation.ValueMember = "Code"
        dgvLocation.DisplayMember = "Description"
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        dgvLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        dgvLocation.Enabled = True
    End Sub
    '------------------------------------------Code Ends Here-------------------------------------------------

    
    Private Sub chkcustall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcustall.ToggleStateChanged
        cbgcustomertype.Enabled = False
    End Sub

    Private Sub chkcustselect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcustselect.ToggleStateChanged
        cbgcustomertype.Enabled = True
    End Sub

End Class
