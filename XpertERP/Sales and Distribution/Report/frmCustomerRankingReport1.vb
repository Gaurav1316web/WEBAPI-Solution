Imports common
Imports System.Data.SqlClient

Public Class FrmCustomerRankingReport1

    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnCustomerRanking)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmCustomerRankingReport1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub



    Private Sub FrmCustomerRankingReport1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")



        SetUserMgmtNew()
        Reset()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CUS-RAK-RPT"
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

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function


    Sub LoadRoute()
        Dim qry As String = "select Route_No as [Route],Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Sub Reset()
        fromDate.Value = clsCommon.GETSERVERDATE
        ToDate.Value = clsCommon.GETSERVERDATE
        LoadRoute()
        chkRouteAll.IsChecked = True
        txtVolume1.Text = 0
        txtVolume2.Text = 0
        ddlConvert.SelectedIndex = 0
        txtVolume1.Text = ""
        txtVolume2.Text = ""
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()

    End Sub
    Sub print()
        Dim strSubQry1 As String = ""
        ' Dim strSubQry2 As String
        Dim strConverted As String = ""
        Dim RunDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy")
        Dim StartDate As String = clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy")
        Dim EndDate As String = clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy")

        Dim Froute As String = ""


        If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count > 0 Then
            Froute = "'" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + "'"
            Froute = Froute.Replace("'", "")
        End If

        If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Atleast single Route Or Select All")
            Return
        End If

        Dim Vol1 As Double = clsCommon.myCdbl(txtVolume1.Text)
        Dim Vol2 As Double = clsCommon.myCdbl(txtVolume2.Text)
        If clsCommon.myLen(txtVolume1.Text) AndAlso clsCommon.myLen(txtVolume2.Text) Then
            If (Vol2 - Vol1) < 0 Then
                common.clsCommon.MyMessageBoxShow("please Provide A Valid Volume Range")
                Return
            End If
        End If
        Dim VolRange As String = clsCommon.myCstr(Vol1) + "---" + clsCommon.myCstr(Vol2)
        If ddlConvert.Text = "Converted" Then
            strSubQry1 = "isnull(((select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='FB'and TSPL_SALE_INVOICE_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code) /  (select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='con'and TSPL_SALE_INVOICE_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code))* (case when TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0)as Invoice_Qty"
            strConverted = "Converted"
        ElseIf ddlConvert.Text = "8oz" Then
            strSubQry1 = "isnull((( (select TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='fb'and TSPL_SALE_INVOICE_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code) / (select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='con'and TSPL_SALE_INVOICE_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code)   ) *(select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and TSPL_SALE_INVOICE_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code)   *(case when TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )),0)  as Invoice_Qty"
            strConverted = "8oz"
        ElseIf ddlConvert.Text = "Raw" Then
            strSubQry1 = "(case when TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )  as Invoice_Qty "
            strConverted = "Raw"
        End If
        Dim Qry As String = "Select '" + Froute + "' as Froute,'" + RunDate + "' as RunDate, '" + StartDate + "'  as StartDate, '" + EndDate + "' as EndDate, '" + VolRange + "' as VolRange , " & _
                " Cust_Code, MAX(Cust_Name) as Custname, Salesman_Code, MAX(Emp_Name) as SalesManDesc, MAX(Channel_Code) as ChannelCode, " & _
                " MAX(Channel_Desc) as ChannelDesc, Route_No as RouteNo, MAX(Route_Desc) as RouteDesc, SUM(Invoice_Qty) as Volume, MAX(Visi) as Visi, " & _
                " MAX(Comp_Code) as CompCode, MAX(Comp_Name) as CompName, MAX(Address  ) as Address, '" + strConverted + "' as ConversionType from ( " & _
                " Select TSPL_SALE_INVOICE_HEAD.Cust_Code, " & _
                " TSPL_SALE_INVOICE_HEAD.Cust_Name, TSPL_SALE_INVOICE_HEAD.Salesman_Code, TSPL_EMPLOYEE_MASTER.Emp_Name, TSPL_CUSTOMER_MASTER.Channel_Code, " & _
                " TSPL_CUSTOMER_MASTER.Channel_Desc, TSPL_SALE_INVOICE_HEAD.Route_No, TSPL_SALE_INVOICE_HEAD.Route_Desc, " + strSubQry1 + " , " & _
                " (Select Count(*) from TSPL_VISI_MASTER Where TSPL_VISI_MASTER.Customer_Id =TSPL_SALE_INVOICE_HEAD.Cust_Code  ) as Visi, " & _
                " TSPL_COMPANY_MASTER.Comp_Code, TSPL_COMPANY_MASTER.Comp_Name, " & _
                " (TSPL_COMPANY_MASTER.Add1+ Case When ISNULL(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.Add2 End+ Case When ISNULL(TSPL_COMPANY_MASTER.Add3, '')='' then '' else ', '+TSPL_COMPANY_MASTER.Add3 End+ Case When ISNULL(TSPL_CITY_MASTER.City_Name,'')='' then '' else ', '+TSPL_CITY_MASTER.City_Name  End+ Case When ISNULL(TSPL_COMPANY_MASTER.State,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.State End+ case When ISNULL(TSPL_COMPANY_MASTER.Pincode,'')='' Then '' else '- '+TSPL_COMPANY_MASTER.Pincode End ) as Address  " & _
                " from TSPL_SALE_INVOICE_HEAD " & _
                " Left Outer Join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
                " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_SALE_INVOICE_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
                " Left Outer Join TSPL_EMPLOYEE_MASTER on TSPL_SALE_INVOICE_HEAD.Salesman_Code=TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
                " Left outer Join TSPL_COMPANY_MASTER on TSPL_SALE_INVOICE_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
                " Left Outer Join TSPL_CITY_MASTER on TSPL_COMPANY_MASTER.City_Code=TSPL_CITY_MASTER.City_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_SALE_INVOICE_DETAIL.Item_Code    and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code " & _
                " Where Is_Post= 'Y' AND Convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103)>=CONVERT(date, '" + fromDate.Value + "', 103) AND Convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103)<=CONVERT(date, '" + ToDate.Value + "', 103)"

        If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count > 0 Then
            Qry += " AND TSPL_SALE_INVOICE_HEAD.Route_No  in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
        End If

        Dim Whr As String
        Whr = " Where 1=1 "
        If txtVolume1.Text <> "" Then
            Whr += " AND Invoice_Qty >=" + clsCommon.myCstr(Vol1) + "  "
        End If
        If txtVolume2.Text <> "" Then
            Whr += " AND Invoice_Qty<=" + clsCommon.myCstr(Vol2) + " "
        End If

        Qry += " ) xxx " + Whr + " Group By Route_No, Salesman_Code, Cust_Code  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found")
        Else
            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptCustomerRanking", " Customer Ranking Report")
        End If
    End Sub
    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged
        cbgRoute.Enabled = False
    End Sub

    Private Sub chkRouteSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = True
    End Sub
End Class
