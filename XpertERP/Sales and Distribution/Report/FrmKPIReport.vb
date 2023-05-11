Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML

Public Class FrmKPIReport
    Inherits FrmMainTranScreen
    Dim dt As DataTable

    Public Delegate Sub ProgressBarValueDelegate(ByVal value As Integer)
    Private Delegate Sub CustomDelegate(ByVal obj As Object, ByVal text As String)

    Dim ButtonToolTip As ToolTip = New ToolTip()


    Private Sub FrmKPIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value
        SetUserMgmtNew()
        Loadlocation()
        LoadDays()
        LoadEmployee()
        LoadRoute()
        cboDays.SelectedIndex = 0
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(RadButton3, "Press Alt+P Print")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub LoadEmployee()
        Dim qry As String = " select EMP_CODE,Emp_Name from TSPL_EMPLOYEE_MASTER where 2=2 " ''Emp_type='Salesman' and
        cbgEmployee.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgEmployee.ValueMember = "EMP_CODE"
        cbgEmployee.DisplayMember = "Emp_Name"
    End Sub

    Sub LoadRoute()
        Dim qry As String = " select Route_No,Route_Desc from TSPL_ROUTE_MASTER order by Route_No"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route_No"
        cbgRoute.DisplayMember = "Route_Desc"
    End Sub

    Private Sub LoadDays()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = 6
        dr("Name") = "7 Days"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 14
        dr("Name") = "15 Days"
        dt.Rows.Add(dr)

        cboDays.DataSource = dt
        cboDays.ValueMember = "Code"
        cboDays.DisplayMember = "Name"
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmKPIReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Loadlocation()
        'Dim qry As String = "select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Private Sub FrmKPIReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
         
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub

    Private Sub chkLocAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Try
            gv1.EnableFiltering = True
            LoadData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub LoadData()
        If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one Location Segment")
        End If
        If chkRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one Route")
        End If
        If chkEmployeeSelect.IsChecked AndAlso cbgEmployee.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one Employee")
        End If
        Dim strFilterDay As String = ""

        Dim strSchDayFilter As String = ""
        Dim strNonSchDayFilter As String = ""

        Dim strFilterEmployee As String = ""
        If chkEmployeeSelect.IsChecked Then
            strFilterEmployee += " and (TSPL_SALE_INVOICE_HEAD.Salesman_Code in (" + clsCommon.GetMulcallString(cbgEmployee.CheckedValue) + ") or TSPL_SALE_INVOICE_HEAD.Level2_User_code in (" + clsCommon.GetMulcallString(cbgEmployee.CheckedValue) + ") or TSPL_SALE_INVOICE_HEAD.Level3_User_code in (" + clsCommon.GetMulcallString(cbgEmployee.CheckedValue) + ") or TSPL_SALE_INVOICE_HEAD.Level4_User_code in (" + clsCommon.GetMulcallString(cbgEmployee.CheckedValue) + ") or TSPL_SALE_INVOICE_HEAD.Level5_User_code in (" + clsCommon.GetMulcallString(cbgEmployee.CheckedValue) + "))"
        End If


        Dim span As TimeSpan = txtToDate.Value.Subtract(txtFromDate.Value)

        Dim intDiffDays As Integer = span.Days
        If intDiffDays < 0 Then
            Throw New Exception("To Date can't be greater then From Date")
        End If
        Dim isFirstTime As Boolean = True
        For ii As Integer = 0 To intDiffDays
            Dim tempDt As DateTime = txtFromDate.Value.AddDays(ii)
            Select Case tempDt.DayOfWeek
                Case DayOfWeek.Monday
                    strFilterDay = "TSPL_ROUTE_GROUP_MASTER.Monday"
                Case DayOfWeek.Tuesday
                    strFilterDay = "TSPL_ROUTE_GROUP_MASTER.Tuesday"
                Case DayOfWeek.Wednesday
                    strFilterDay = "TSPL_ROUTE_GROUP_MASTER.Wednesday"
                Case DayOfWeek.Thursday
                    strFilterDay = "TSPL_ROUTE_GROUP_MASTER.Thursday"
                Case DayOfWeek.Friday
                    strFilterDay = "TSPL_ROUTE_GROUP_MASTER.Friday"
                Case DayOfWeek.Saturday
                    strFilterDay = "TSPL_ROUTE_GROUP_MASTER.Saturday"
                Case DayOfWeek.Sunday
                    strFilterDay = "TSPL_ROUTE_GROUP_MASTER.Sunday"
            End Select

            If Not isFirstTime Then
                strSchDayFilter += " + "
                strNonSchDayFilter += " + "
            End If

            strSchDayFilter += " (case when " + strFilterDay + " = 'T' then 1 else 0 end)"
            strNonSchDayFilter += "(case when " + strFilterDay + " ='F' then 1 else 0 end)"
            isFirstTime = False
        Next

        Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        Dim strDateSevenDays As String = clsCommon.GetPrintDate(txtToDate.Value.AddDays(-1 * clsCommon.myCdbl(cboDays.SelectedValue)), "dd/MMM/yyyy")
        Dim qry As String = "select xxxx.Route_No,xxxx.Route_Desc,xxxx.SchCall,xxxx.NonSchCall,xxxx.CallNotScheduled,xxxx.RouteJump,xxxx.StrikeCall," + Environment.NewLine
        qry += " CONVERT(decimal(18,2),(case when xxxx.SchCall=0 then 0 else (CONVERT(decimal(18,2), xxxx.StrikeCall)*100)/CONVERT(decimal(18,2),xxxx.SchCall) end)) as StrikeRate,CONVERT(decimal(18,2), ROUND( xxxx.DropSize,2)) as DropSize,xxxx.PPlus2,xxxx.PPlus3,CONVERT(decimal(18,2),(case when xxxx.SchCall=0 then 0 else (CONVERT(decimal(18,2), xxxx.PPlus2)*100)/CONVERT(decimal(18,2),xxxx.SchCall) end)) as PPlus2Per,CONVERT(decimal(18,2),(case when xxxx.SchCall=0 then 0 else (CONVERT(decimal(18,2), xxxx.PPlus3)*100)/CONVERT(decimal(18,2),xxxx.SchCall) end)) as PPlus3Per," + Environment.NewLine
        qry += " CONVERT(decimal(18,2),(case when xxxx.SchCall=0 then 0 else CONVERT(decimal(18,2), xxxx.P2PenCust) end)) as P2Pen," + Environment.NewLine
        qry += " CONVERT(decimal(18,2),(case when xxxx.SchCall=0 then 0 else CONVERT(decimal(18,2), xxxx.P3PenCust) end)) as P3Pen,P2Reach,P3Reach,MSReach,CONVERT(decimal(18,2),ROUND(LPSC,2))  as LPSC,CONVERT(decimal(18,2),ROUND(TotalSale,2)) as TotalSale" + Environment.NewLine
        qry += " from(" + Environment.NewLine

        qry += " select xxx.Route_No,xxx.Route_Desc ," + Environment.NewLine
        qry += " (select isnull( sum( " + strSchDayFilter + " ),0) from TSPL_CUSTOMER_MASTER" + Environment.NewLine
        qry += " left outer join TSPL_ROUTE_GROUP_MASTER on TSPL_ROUTE_GROUP_MASTER.Group_Id=TSPL_CUSTOMER_MASTER.Route_Group " + Environment.NewLine
        qry += " where TSPL_ROUTE_GROUP_MASTER .Route_Code=xxx.Route_No " + Environment.NewLine
        qry += " and  TSPL_CUSTOMER_MASTER.Status='N' and CONVERT(date, TSPL_CUSTOMER_MASTER.Created_Date,103)<='" + strToDate + "' "
        qry += " ) as SchCall," + Environment.NewLine

        qry += " (select isnull( sum(" + strNonSchDayFilter + "),0) from TSPL_CUSTOMER_MASTER" + Environment.NewLine
        qry += " left outer join TSPL_ROUTE_GROUP_MASTER on TSPL_ROUTE_GROUP_MASTER.Group_Id=TSPL_CUSTOMER_MASTER.Route_Group " + Environment.NewLine
        qry += " where TSPL_ROUTE_GROUP_MASTER .Route_Code=xxx.Route_No " + Environment.NewLine
        qry += "  and TSPL_CUSTOMER_MASTER.Status='N' and CONVERT(date, TSPL_CUSTOMER_MASTER.Created_Date,103)<='" + strToDate + "' ) as NonSchCall," + Environment.NewLine

        qry += " (select COUNT(Cust_Code) from (" + Environment.NewLine
        qry += " select   Cust_Code  from(" + Environment.NewLine
        qry += " Select TSPL_SALE_INVOICE_HEAD.Cust_Code" + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_HEAD " + Environment.NewLine
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >='" + strFromDate + "'  and  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <='" + strToDate + "' and TSPL_SALE_INVOICE_HEAD.Route_No= xxx.Route_No and TSPL_SALE_INVOICE_HEAD.Is_Scheduled='0'" + Environment.NewLine
        If clsCommon.myLen(strFilterEmployee) > 0 Then
            qry += strFilterEmployee
        End If
        If rbtnPosted.IsChecked Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        End If
        qry += " )AAA group by Cust_Code" + Environment.NewLine
        qry += " )AAAA" + Environment.NewLine
        qry += " ) as CallNotScheduled," + Environment.NewLine

        qry += " (select isnull( sum(is_Route_Jumped),0) from (" + Environment.NewLine
        qry += " select Cust_Code,is_Route_Jumped from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_Date >='" + strFromDate + "' and Sale_Invoice_Date <='" + strToDate + "' and Route_No= xxx.Route_No" + Environment.NewLine
        qry += " group by  Cust_Code,is_Route_Jumped) xx) as RouteJump," + Environment.NewLine


        ''qry += " (select isnull( sum(Is_Scheduled),0) from (" + Environment.NewLine
        ''qry += " select Cust_Code,Is_Scheduled from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_Date >='" + strFromDate + "' and Sale_Invoice_Date <='" + strToDate + "' and Route_No= xxx.Route_No" + Environment.NewLine
        ''qry += " group by  Cust_Code,Is_Scheduled) xx ) as StrikeCall,"

        qry += "(select ISNULL(  sum(StrikeCall),0) as StrikeCall from (" + Environment.NewLine
        qry += " select Cust_Code,Sale_Invoice_Date, min(1) as StrikeCall from (  " + Environment.NewLine
        qry += " select Cust_Code ,CONVERT(varchar(10), Sale_Invoice_Date,103) as Sale_Invoice_Date" + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_HEAD" + Environment.NewLine
        qry += " where Sale_Invoice_Date >='" + strFromDate + "' and Sale_Invoice_Date <='" + strToDate + "' and Route_No= xxx.Route_No and Is_Scheduled=1" + Environment.NewLine
        qry += " )xxxxxx " + Environment.NewLine
        qry += " group by  Cust_Code,Sale_Invoice_Date" + Environment.NewLine
        qry += " ) xx ) as StrikeCall," + Environment.NewLine

        qry += " ( select   case when xxx.NoOfInvoices=0 then 0 else SUM(FCQty)/xxx.NoOfInvoices end from(" + Environment.NewLine
        qry += " select TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_SALE_INVOICE_DETAIL.Unit_code,TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(Invoice_Qty/Conversion_Factor) as FCQty from TSPL_SALE_INVOICE_DETAIL" + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code" + Environment.NewLine
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >='" + strFromDate + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <='" + strToDate + "' and TSPL_SALE_INVOICE_HEAD.Route_No= xxx.Route_No" + Environment.NewLine
        If clsCommon.myLen(strFilterEmployee) > 0 Then
            qry += strFilterEmployee
        End If
        If rbtnPosted.IsChecked Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        End If
        qry += " )xx) as DropSize," + Environment.NewLine


        qry += " (select COUNT(Cust_Code) from (" + Environment.NewLine
        qry += " select   Cust_Code  from(" + Environment.NewLine
        qry += " select Cust_Code,Class_Code,max(ContainingPC) as ContainingPC   from(" + Environment.NewLine
        qry += " select TSPL_SALE_INVOICE_HEAD.Cust_Code, TSPL_SALE_INVOICE_HEAD.Cust_Name,TSPL_ITEM_DETAILS.Class_Code,case when TSPL_ITEM_DETAILS.Class_Code='PC' then 1 else 0 end as ContainingPC" + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL" + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_DETAILS on TSPL_ITEM_DETAILS.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_DETAILS.Class_Name='Flavour'" + Environment.NewLine
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >='" + strFromDate + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <='" + strToDate + "' and TSPL_SALE_INVOICE_HEAD.Route_No= xxx.Route_No " + Environment.NewLine
        qry += " and TSPL_SALE_INVOICE_HEAD.Is_Scheduled='1' and SUBSTRING( TSPL_SALE_INVOICE_DETAIL.Item_Code,0,3) not in ('ES','AQ') " + Environment.NewLine
        If clsCommon.myLen(strFilterEmployee) > 0 Then
            qry += strFilterEmployee
        End If
        If rbtnPosted.IsChecked Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        End If
        qry += " )AA group by Cust_Code,Class_Code " + Environment.NewLine
        qry += " )AAA group by Cust_Code" + Environment.NewLine
        qry += " having(SUM(ContainingPC) > 0 And count(Class_Code) >= 3)" + Environment.NewLine
        qry += " )AAAA) as PPlus2," + Environment.NewLine

        qry += " ( select COUNT(Cust_Code) from (" + Environment.NewLine
        qry += " select   Cust_Code  from(" + Environment.NewLine
        qry += " select Cust_Code,Class_Code,max(ContainingPC) as ContainingPC   from(" + Environment.NewLine
        qry += " select TSPL_SALE_INVOICE_HEAD.Cust_Code, TSPL_SALE_INVOICE_HEAD.Cust_Name,TSPL_ITEM_DETAILS.Class_Code,case when TSPL_ITEM_DETAILS.Class_Code='PC' then 1 else 0 end as ContainingPC" + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL" + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_DETAILS on TSPL_ITEM_DETAILS.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_DETAILS.Class_Name='Flavour'" + Environment.NewLine
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >='" + strFromDate + "'  and  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <='" + strToDate + "' and TSPL_SALE_INVOICE_HEAD.Route_No= xxx.Route_No " + Environment.NewLine
        qry += " and TSPL_SALE_INVOICE_HEAD.Is_Scheduled='1' and SUBSTRING( TSPL_SALE_INVOICE_DETAIL.Item_Code,0,3) not in ('AQ')" + Environment.NewLine
        If clsCommon.myLen(strFilterEmployee) > 0 Then
            qry += strFilterEmployee
        End If
        If rbtnPosted.IsChecked Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        End If
        qry += " )AA group by Cust_Code,Class_Code " + Environment.NewLine
        qry += " )AAA group by Cust_Code" + Environment.NewLine
        qry += " having(SUM(ContainingPC) > 0 And count(Class_Code)>= 4)" + Environment.NewLine
        qry += " )AAAA) as PPlus3,"

        qry += " (select COUNT(Cust_Code) from (" + Environment.NewLine
        qry += " select   Cust_Code  from(" + Environment.NewLine
        qry += " select Cust_Code,Class_Code,max(ContainingPC) as ContainingPC   from(" + Environment.NewLine
        qry += " select TSPL_SALE_INVOICE_HEAD.Cust_Code, TSPL_SALE_INVOICE_HEAD.Cust_Name,TSPL_ITEM_DETAILS.Class_Code,case when TSPL_ITEM_DETAILS.Class_Code='PC' then 1 else 0 end as ContainingPC" + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL" + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_DETAILS on TSPL_ITEM_DETAILS.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_DETAILS.Class_Name='Flavour'" + Environment.NewLine
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >='" + strFromDate + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <='" + strToDate + "' and TSPL_SALE_INVOICE_HEAD.Route_No= xxx.Route_No " + Environment.NewLine
        If clsCommon.myLen(strFilterEmployee) > 0 Then
            qry += strFilterEmployee
        End If
        If rbtnPosted.IsChecked Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        End If
        qry += " )AA group by Cust_Code,Class_Code " + Environment.NewLine
        qry += " )AAA group by Cust_Code" + Environment.NewLine
        qry += " having(SUM(ContainingPC) > 0 And count(Class_Code) = 3)" + Environment.NewLine
        qry += " )AAAA) as P2PenCust," + Environment.NewLine

        qry += " ( select COUNT(Cust_Code) from (" + Environment.NewLine
        qry += " select   Cust_Code  from(" + Environment.NewLine
        qry += " select Cust_Code,Class_Code,max(ContainingPC) as ContainingPC   from(" + Environment.NewLine
        qry += " select TSPL_SALE_INVOICE_HEAD.Cust_Code, TSPL_SALE_INVOICE_HEAD.Cust_Name,TSPL_ITEM_DETAILS.Class_Code,case when TSPL_ITEM_DETAILS.Class_Code='PC' then 1 else 0 end as ContainingPC" + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL" + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_DETAILS on TSPL_ITEM_DETAILS.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_DETAILS.Class_Name='Flavour'" + Environment.NewLine
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >='" + strFromDate + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <='" + strToDate + "' and TSPL_SALE_INVOICE_HEAD.Route_No= xxx.Route_No " + Environment.NewLine
        If clsCommon.myLen(strFilterEmployee) > 0 Then
            qry += strFilterEmployee
        End If
        If rbtnPosted.IsChecked Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        End If
        qry += " )AA group by Cust_Code,Class_Code " + Environment.NewLine
        qry += " )AAA group by Cust_Code" + Environment.NewLine
        qry += " having(SUM(ContainingPC) > 0 And count(Class_Code)>= 4)" + Environment.NewLine
        qry += " )AAAA) as P3PenCust,"

        qry += " (select COUNT(Cust_Code) from (" + Environment.NewLine
        qry += " select Cust_Code from (" + Environment.NewLine
        qry += " select   Sale_Invoice_No,Cust_Code  from(" + Environment.NewLine
        qry += " select Sale_Invoice_No,Class_Code,Cust_Code,max(ContainingPC) as ContainingPC   from(" + Environment.NewLine
        qry += " select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,Cust_Code, TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_DETAILS.Class_Code,case when TSPL_ITEM_DETAILS.Class_Code='PC' then 1 else 0 end as ContainingPC" + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_DETAILS on TSPL_ITEM_DETAILS.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_DETAILS.Class_Name='Flavour'" + Environment.NewLine
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >='" + strDateSevenDays + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <='" + strToDate + "' and TSPL_SALE_INVOICE_HEAD.Route_No= xxx.Route_No and TSPL_ITEM_DETAILS.Class_Code not in ('ES')" + Environment.NewLine
        If clsCommon.myLen(strFilterEmployee) > 0 Then
            qry += strFilterEmployee
        End If
        If rbtnPosted.IsChecked Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        End If
        qry += " )AA group by Sale_Invoice_No,Cust_Code, Class_Code " + Environment.NewLine
        qry += " )AAA group by Sale_Invoice_No,Cust_Code" + Environment.NewLine
        qry += " having(SUM(ContainingPC) > 0 And count(Class_Code) = 3)" + Environment.NewLine
        qry += " )AAAA group by Cust_Code" + Environment.NewLine
        qry += " )AAAAA) as P2Reach," + Environment.NewLine

        qry += " (select COUNT(Cust_Code) from (" + Environment.NewLine
        qry += " select Cust_Code from (" + Environment.NewLine
        qry += " select   Sale_Invoice_No,Cust_Code  from(" + Environment.NewLine
        qry += " select Sale_Invoice_No,Class_Code,Cust_Code,max(ContainingPC) as ContainingPC   from(" + Environment.NewLine
        qry += " select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,Cust_Code, TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_DETAILS.Class_Code,case when TSPL_ITEM_DETAILS.Class_Code='PC' then 1 else 0 end as ContainingPC" + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL" + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_DETAILS on TSPL_ITEM_DETAILS.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_DETAILS.Class_Name='Flavour'" + Environment.NewLine
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >='" + strDateSevenDays + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <='" + strToDate + "' and TSPL_SALE_INVOICE_HEAD.Route_No= xxx.Route_No " + Environment.NewLine
        If clsCommon.myLen(strFilterEmployee) > 0 Then
            qry += strFilterEmployee
        End If
        If rbtnPosted.IsChecked Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        End If
        qry += " )AA group by Sale_Invoice_No,Cust_Code, Class_Code " + Environment.NewLine
        qry += " )AAA group by Sale_Invoice_No,Cust_Code" + Environment.NewLine
        qry += " having(SUM(ContainingPC) > 0 And count(Class_Code) >= 4)" + Environment.NewLine
        qry += " )AAAA group by Cust_Code" + Environment.NewLine
        qry += " )AAAAA)  as P3Reach," + Environment.NewLine

        qry += " (select COUNT(Cust_Code) from (" + Environment.NewLine
        qry += " select Cust_Code from (" + Environment.NewLine
        qry += " select   Sale_Invoice_No,Cust_Code  from(" + Environment.NewLine
        qry += " select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,Cust_Code, TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Server_Type" + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL" + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code " + Environment.NewLine
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >='" + strDateSevenDays + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <='" + strToDate + "' and TSPL_SALE_INVOICE_HEAD.Route_No=xxx.Route_No   and TSPL_ITEM_MASTER.Server_Type='Multiple Serve'" + Environment.NewLine
        If clsCommon.myLen(strFilterEmployee) > 0 Then
            qry += strFilterEmployee
        End If
        If rbtnPosted.IsChecked Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        End If
        qry += " )AA group by Sale_Invoice_No,Cust_Code " + Environment.NewLine
        qry += " )AAAA group by Cust_Code" + Environment.NewLine
        qry += " )AAAAA) as MSReach," + Environment.NewLine

        qry += " (case when NoOfInvoices=0 then 0 else CONVERT(decimal(18,2), (select SUM(NoofItem) as NoofItem from(" + Environment.NewLine
        qry += " select Sale_Invoice_No,COUNT(Item_Code) as NoofItem from (" + Environment.NewLine
        qry += " select   Sale_Invoice_No,Item_Code  from(" + Environment.NewLine
        qry += " select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,Cust_Code,TSPL_SALE_INVOICE_DETAIL.Item_Code" + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL" + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >='" + strFromDate + "' and  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <='" + strToDate + "' and TSPL_SALE_INVOICE_HEAD.Route_No = xxx.Route_No" + Environment.NewLine
        If clsCommon.myLen(strFilterEmployee) > 0 Then
            qry += strFilterEmployee
        End If
        If rbtnPosted.IsChecked Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        End If
        qry += " )AA group by Sale_Invoice_No, Item_Code " + Environment.NewLine
        qry += " )AAA  group by Sale_Invoice_No" + Environment.NewLine
        qry += " )AAAA))/NoOfInvoices end) as LPSC," + Environment.NewLine

        qry += " (select  isnull( SUM(FCQty),0) from(" + Environment.NewLine
        qry += " select TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_SALE_INVOICE_DETAIL.Unit_code,TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(Invoice_Qty/Conversion_Factor) as FCQty from TSPL_SALE_INVOICE_DETAIL" + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        qry += "  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code " + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code" + Environment.NewLine
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >='" + strFromDate + "' and  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <='" + strToDate + "' and TSPL_SALE_INVOICE_HEAD.Route_No= xxx.Route_No" + Environment.NewLine
        If clsCommon.myLen(strFilterEmployee) > 0 Then
            qry += strFilterEmployee
        End If
        If rbtnPosted.IsChecked Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        End If
        qry += " and TSPL_CUSTOMER_MASTER.Cust_Type_Code  not in ('F','S') "
        qry += " )xx) as TotalSale" + Environment.NewLine
        qry += " from(" + Environment.NewLine
        qry += " --Base Qury" + Environment.NewLine
        qry += " select Route_No,max(Route_Desc) as Route_Desc,sum( NoOfInvoices)  as NoOfInvoices from (" + Environment.NewLine
        qry += " select   Route_No,  max(Route_Desc) as Route_Desc,COUNT(Sale_Invoice_No) as NoOfInvoices" + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_HEAD " + Environment.NewLine
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SALE_INVOICE_HEAD.Location"
        qry += " where Sale_Invoice_Date >='" + strFromDate + "'  and  Sale_Invoice_Date <='" + strToDate + "'  " + Environment.NewLine
        If chkLocSelect.IsChecked Then
            qry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        End If
        If chkRouteSelect.IsChecked Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
        End If
        If clsCommon.myLen(strFilterEmployee) > 0 Then
            qry += strFilterEmployee
        End If
        If rbtnPosted.IsChecked Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Is_Post='Y'"
        End If
        qry += " group by Route_No" + Environment.NewLine
        qry += " --End of Base Qury" + Environment.NewLine
        ''qry += " union all"
        ''qry += " select TSPL_ROUTE_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,0 as NoOfInvoices from TSPL_ROUTE_MASTER where 2=2 "
        ''If chkRouteSelect.IsChecked Then
        ''    qry += " and TSPL_ROUTE_MASTER.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
        ''End If
        qry += " )xx group by Route_No"
        qry += " )xxx  " + Environment.NewLine
        qry += " )xxxx " + Environment.NewLine
        qry += " order by Route_No" + Environment.NewLine
        dt = clsDBFuncationality.GetDataTable(qry)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("No Data Found to Display")
        End If
        gv1.DataSource = dt
        SetGridFormationOFGV1()
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        gv1.Columns("Route_No").IsVisible = True
        gv1.Columns("Route_No").Width = 50
        gv1.Columns("Route_No").HeaderText = "Route Code"
        ' gv1.MasterTableView.Columns[3].OrderIndex=5

        gv1.Columns("Route_Desc").IsVisible = True
        gv1.Columns("Route_Desc").Width = 150
        gv1.Columns("Route_Desc").HeaderText = "Route"

        gv1.Columns("SchCall").IsVisible = True
        gv1.Columns("SchCall").Width = 50
        gv1.Columns("SchCall").HeaderText = "Sch Calls"

        gv1.Columns("NonSchCall").IsVisible = True
        gv1.Columns("NonSchCall").Width = 70
        gv1.Columns("NonSchCall").HeaderText = "Non-Sch Call"

        gv1.Columns("CallNotScheduled").IsVisible = True
        gv1.Columns("CallNotScheduled").Width = 70
        gv1.Columns("CallNotScheduled").HeaderText = "Call Not Scheduled"

        gv1.Columns("RouteJump").IsVisible = True
        gv1.Columns("RouteJump").Width = 50
        gv1.Columns("RouteJump").HeaderText = "Route Jump"

        gv1.Columns("StrikeCall").IsVisible = True
        gv1.Columns("StrikeCall").Width = 70
        gv1.Columns("StrikeCall").HeaderText = "Strike Call"


        gv1.Columns("StrikeRate").IsVisible = True
        gv1.Columns("StrikeRate").Width = 70
        gv1.Columns("StrikeRate").HeaderText = "Strike Rate"

        gv1.Columns("DropSize").IsVisible = True
        gv1.Columns("DropSize").Width = 70
        gv1.Columns("DropSize").HeaderText = "Drop Size"

        gv1.Columns("PPlus2").IsVisible = True
        gv1.Columns("PPlus2").Width = 50
        gv1.Columns("PPlus2").HeaderText = "P+2"

        gv1.Columns("PPlus3").IsVisible = True
        gv1.Columns("PPlus3").Width = 50
        gv1.Columns("PPlus3").HeaderText = "P+3"

        gv1.Columns("PPlus2Per").IsVisible = True
        gv1.Columns("PPlus2Per").Width = 50
        gv1.Columns("PPlus2Per").HeaderText = "P+2 %"

        gv1.Columns("PPlus3Per").IsVisible = True
        gv1.Columns("PPlus3Per").Width = 80
        gv1.Columns("PPlus3Per").HeaderText = "P+3 %"

        gv1.Columns("P2Pen").IsVisible = True
        gv1.Columns("P2Pen").Width = 80
        gv1.Columns("P2Pen").HeaderText = "P2 % PEN"

        gv1.Columns("P3Pen").IsVisible = True
        gv1.Columns("P3Pen").Width = 80
        gv1.Columns("P3Pen").HeaderText = "P3 % PEN"

        gv1.Columns("P2Reach").IsVisible = True
        gv1.Columns("P2Reach").Width = 80
        gv1.Columns("P2Reach").HeaderText = "P2 Reach"

        gv1.Columns("P3Reach").IsVisible = True
        gv1.Columns("P3Reach").Width = 80
        gv1.Columns("P3Reach").HeaderText = "P3 Reach"

        gv1.Columns("MSReach").IsVisible = True
        gv1.Columns("MSReach").Width = 80
        gv1.Columns("MSReach").HeaderText = "MS Reach"

        gv1.Columns("LPSC").IsVisible = True
        gv1.Columns("LPSC").Width = 50
        gv1.Columns("LPSC").HeaderText = "LPSC"

        gv1.Columns("TotalSale").IsVisible = True
        gv1.Columns("TotalSale").Width = 80
        gv1.Columns("TotalSale").HeaderText = "Total Sale"

        ''gv1.GroupDescriptors.Add(New GridGroupByExpression("Transfer_No as Transfer_No format ""{0}: {1}"" Group By Transfer_No"))
        ''gv1.MasterTemplate.ExpandAllGroups()
        gv1.ShowGroupPanel = False
        ''gv1.MasterTemplate.AutoExpandGroups = True


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("SchCall", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("NonSchCall", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("RouteJump", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("StrikeCall", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("DropSize", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("PPlus2", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("PPlus3", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("PPlus2Per", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("PPlus3Per", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("P2Pen", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("P3Pen", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("P2Reach", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        Dim item13 As New GridViewSummaryItem("P3Reach", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("MSReach", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("LPSC", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)
        Dim item16 As New GridViewSummaryItem("TotalSale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item16)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    

    Sub print(ByVal exporter As EnumExportTo)
        Try
            LoadData()
            ''ExportToExcel()
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            If chkLocSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
                arrHeader.Add("Location Segment : " + strLoca)
            End If
            arrHeader.Add("Days : " + cboDays.Text)
            arrHeader.Add("Transaction Type : " + IIf(rbtnPosted.IsChecked, "Posted", "All"))

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("KPI Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("KPI Report", gv1, arrHeader, Me.Text, True)
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub ExportToExcel()
        Dim saveDialog1 As New SaveFileDialog()
        saveDialog1.Filter = "Excel (*.xls)|*.xls"
        If saveDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            ''Me.RadProgressBar1.Text = "Exporting to ExcelML..."
            ''Me.RadProgressBar1.Value1 = 0
            ''Me.RadProgressBar1.Visible = True

            Dim thread2 As New Thread(New ParameterizedThreadStart(AddressOf RunExportToExcelML))
            thread2.Start(saveDialog1.FileName)
        End If
    End Sub

    Private Sub RunExportToExcelML(ByVal fileName As Object)
        Try
            Dim exporter As New ExportToExcelML(gv1)
            exporter.ExportVisualSettings = True
            'If Me.radRadioButton1.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On Then
            exporter.SheetMaxRows = ExcelMaxRows._1048576
            'End If
            AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
            AddHandler exporter.ExcelTableCreated, AddressOf exporter_ExcelTableCreated
            exporter.RunExport(fileName.ToString())
            Dim text As String = "Export finished successfully!"
            ''Dim ev As CustomDelegate = AddressOf Me.MessageShow
            ''If Me.InvokeRequired Then
            ''    ev.Invoke(Me, text)
            ''Else
            ''    common.clsCommon.MyMessageBoxShow(Me, text)
            ''End If
            common.clsCommon.MyMessageBoxShow(text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
            ''If Me.InvokeRequired Then
            ''    Dim ev As CustomDelegate = AddressOf Me.MessageShowError
            ''    ev.Invoke(Me, ex.Message)
            ''Else
            ''    RadMessageBox.SetThemeName("Breeze")
            ''    common.clsCommon.MyMessageBoxShow(Me, ex.Message, "I/O Error", MessageBoxButtons.OK, RadMessageIcon.Error)
            ''End If

        End Try
    End Sub

    '''' <summary>        
    '''' '''' using ExcelTableCreated event for adding custom header row        
    '''' '''' </summary>        

    Private Sub exporter_ExcelTableCreated(ByVal sender As Object, ByVal e As ExcelTableCreatedEventArgs)

        If e.SheetIndex = 0 Then 'add header row only for the first excel sheet                
            Dim style As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 30, "KPI Report")
            style.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center
            style.AlignmentElement.VerticalAlignment = VerticalAlignmentType.Center
            style.InteriorStyle.Pattern = InteriorPatternType.Solid
            'style.InteriorStyle.Color = Color.Red
            style.FontStyle.Color = Color.Black
            style.FontStyle.Bold = True
            style.FontStyle.Size = 26

            Dim style1 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Date : " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            If chkLocSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
                Dim style2 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Location Segment : " + strLoca)

            End If

        End If
    End Sub

    '''' <summary>        
    '''' '''' using ExcelCellFormatting event for updating progress bar and applying custom format in excel file        
    '''' '''' </summary>        

    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewDataRowInfo) Then
            'update progress bar                
            ''Dim position As Integer = CInt(Fix(100 * CDbl(e.GridRowIndex) / CDbl(gv1.RowCount - 1)))
            ''Me.UpdateProgressBar(position)                'do some formatting                
            ''If e.GridColumnIndex = 3 AndAlso CInt(Fix(e.ExcelCellElement.Data.DataItem)) < 200 Then
            ''    e.ExcelStyleElement.InteriorStyle.Color = Color.Yellow
            ''End If
        End If
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged, chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = chkRouteSelect.IsChecked
    End Sub

    Private Sub chkEmployeeAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkEmployeeAll.ToggleStateChanged, chkEmployeeSelect.ToggleStateChanged
        cbgEmployee.Enabled = chkEmployeeSelect.IsChecked
    End Sub

     
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        print(EnumExportTo.Excel)

    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click

    End Sub
End Class
