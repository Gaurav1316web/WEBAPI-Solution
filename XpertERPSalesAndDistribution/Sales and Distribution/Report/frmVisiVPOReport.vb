''16/07/2012---Updation by --[Pankaj kumar]-- Updated Report Qry Because of no of  Visi Outlets=No Of Costomer are not Corect----------By-Ranjana Mam
''26/07/2012---Updation by --[Pankaj kumar]-- Created Qury Again for both case (Visi/  NonVisi)----------By-Ranjana Mam
Imports System.Data.SqlClient
Imports common
Imports XpertERPEngine

Public Class FrmVisiVPOReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()



    Private Sub FrmVisiVPOReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            printdata()
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
        chkVisi.IsChecked = True
        Reset()

        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        SetUserMgmtNew()
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")




    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnVisiVPO1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String        
    '        Dim strTemp() As String
    '        Dim strProgCode = "VISI-VPO-RPT"
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


    Sub LoadLocation()
        Dim qry As String = "Select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        dgvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        dgvLocation.ValueMember = "Code"
        dgvLocation.DisplayMember = "Description"
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
        LoadLocation()
        chkLocAll.IsChecked = True
        ddlConvert.SelectedIndex = 0
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        printdata()
    End Sub
    Sub printdata()
        Dim strSubQry1 As String = ""
        Dim strConverted As String = ""
        Dim LocFilter As String = ""
        Dim RunDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy")
        Dim StartDate As String = clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy")
        Dim EndDate As String = clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy")

        If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Atleast single Route Or Select All")
            Return
        End If
        If chkLocSelect.IsChecked = True AndAlso dgvLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Atleast single Location Or Select All")
            Return
        Else
            LocFilter = clsCommon.GetMulcallString(dgvLocation.CheckedValue)
            LocFilter = LocFilter.Replace("'", "")
        End If

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
        Dim Qry As String

        If chkVisi.IsChecked = True Then    '26/07/2012-Pankaj Kuamr---In Case of Visi Checked it shows No of Visi on particular route in Range Columns
            Qry = "Select '" + LocFilter + "' as LocFilter, '" + RunDate + "' as RunDate, '" + StartDate + "'  as StartDate, '" + EndDate + "' as EndDate, '" + strConverted + "' as ConvertionType,  *, TSPL_COMPANY_MASTER.Comp_Name as CompName,  (TSPL_COMPANY_MASTER.Add1+ Case When ISNULL(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.Add2 End+ " & _
        " Case When ISNULL(TSPL_COMPANY_MASTER.Add3, '')='' then '' else ', '+TSPL_COMPANY_MASTER.Add3 End+ " & _
        " Case When ISNULL(TSPL_CITY_MASTER.City_Name,'')='' then '' else ', '+TSPL_CITY_MASTER.City_Name  End+ Case When ISNULL(TSPL_COMPANY_MASTER.State,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.State End+ case When ISNULL(TSPL_COMPANY_MASTER.Pincode,'')='' Then '' else '- '+TSPL_COMPANY_MASTER.Pincode End ) as Address from ( " & _
        " Select  Route_No, MAX(RouteDesc) as RouteDesc, SUM(V0) as V0, SUM(V1to5) as V1to5, SUM(V6to10) as V6to10, SUM(V11to20  ) as V11to20, " & _
        " SUM(V21to50) as V21to50, SUM(V51to100) as V51to100, SUM(V101to200) as V101to200, SUM(V201to500) as V201to500, SUM(V500Plus) as V500Plus, " & _
        " COUNT(DISTINCT Cust_Code) as VisiOutlet, (SUM(V0)+ SUM(V1to5)+SUM(V6to10)+SUM(V11to20  )+SUM(V21to50)+SUM(V51to100)+SUM(V101to200)+SUM(V201to500)+ SUM(V500Plus)) as TotalVisi, SUM(InvoiceQty) as Volume, MAX(CompCode) as CompCode  from( " & _
        " Select Route_No, MAX(RouteDesc) as RouteDesc, Cust_Code, (Case When InvoiceQty =0 Then SUM(Visi) else 0 End) as V0,  " & _
        " (Case When InvoiceQty>=1 AND InvoiceQty<=5 Then SUM(Visi) else 0 End) as V1to5,  (Case When InvoiceQty>=6 AND InvoiceQty<=10 Then SUM(Visi) else 0 End) as V6to10,  (Case When InvoiceQty>=11 AND InvoiceQty<=20 Then SUM(Visi) else 0 End) as V11to20,  (Case When InvoiceQty>=21 AND " & _
        " InvoiceQty<=50 Then SUM(Visi) else 0 End) as V21to50,  (Case When InvoiceQty>=51 AND InvoiceQty<=100 Then SUM(Visi) else 0 End) as V51to100, " & _
        " (Case When InvoiceQty>=101 AND InvoiceQty<=200 Then SUM(Visi) else 0 End) as V101to200,  (Case When InvoiceQty>201 AND InvoiceQty<=500 Then " & _
        " SUM(Visi) else 0 End) as V201to500,  (Case When InvoiceQty>500 Then SUM(Visi) else 0 End) as V500Plus, MAX(CompCode) as CompCode, SUM(Visi) as TotalVisi, InvoiceQty from ( " & _
        " Select Route_No, MAX(Route_Desc) as RouteDesc, Cust_Code, SUM(Invoice_Qty ) as InvoiceQty, (Select Count(*) from TSPL_VISI_MASTER Where TSPL_VISI_MASTER.Customer_Id =AAA.Cust_Code  ) as Visi, MAX(Comp_Code) as CompCode from ( "
        Else    '26/07/2012-Pankaj Kuamr---In Case of NonVisi Checked it shows No of Customer on particular route in Range Columns
            Qry = "Select '" + LocFilter + "' as LocFilter,'" + RunDate + "' as RunDate, '" + StartDate + "'  as StartDate, '" + EndDate + "' as EndDate, 'Converted' as ConvertionType, " & _
        " (TotalOutlet-(range1+Range2+Range3+Range4+Range5+Range6+Range7+Range8 ) ) as Range0, *, TSPL_COMPANY_MASTER.Comp_Name as CompName, " & _
        " (TSPL_COMPANY_MASTER.Add1+ Case When ISNULL(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.Add2 End+ Case When ISNULL(TSPL_COMPANY_MASTER.Add3, '')='' then '' else ', '+TSPL_COMPANY_MASTER.Add3 End+ Case When ISNULL(TSPL_CITY_MASTER.City_Name,'')='' then '' else ', '+TSPL_CITY_MASTER.City_Name  End+ " & _
        " Case When ISNULL(TSPL_COMPANY_MASTER.State,'')='' Then '' else ', '+TSPL_COMPANY_MASTER.State End+  case When ISNULL(TSPL_COMPANY_MASTER.Pincode,'')='' Then '' else '- '+TSPL_COMPANY_MASTER.Pincode End ) as Address from ( " & _
        " Select  Route_No, MAX(RouteDesc) as RoureDesc, SUM(V1to5) as Range1, SUM(V6to10) as Range2, SUM(V11to20  ) as Range3, SUM(V21to50) as Range4, " & _
        " SUM(V51to100) as Range5, SUM(V101to200) as Range6, SUM(V201to500) as Range7, SUM(V500Plus) as Range8, (Select COUNT(*) From TSPL_CUSTOMER_MASTER Where Route_No=CCC.Route_No) as TotalOutlet, SUM(InvoiceQty) as Volume, MAX(CompCode) as CompCode  from( " & _
        " Select Route_No, MAX(RouteDesc) as RouteDesc, Cust_Code, (Case When InvoiceQty>=1 AND InvoiceQty<=5 Then 1 else 0 End) as V1to5,  (Case When InvoiceQty>=6 AND InvoiceQty<=10 Then 1 else 0 End) as V6to10,  (Case When InvoiceQty>=11 AND InvoiceQty<=20 Then 1 else 0 End) as V11to20,  " & _
        " (Case When InvoiceQty>=21 AND InvoiceQty<=50 Then 1 else 0 End) as V21to50,  (Case When InvoiceQty>=51 AND InvoiceQty<=100 Then 1 else 0 End) as V51to100, (Case When InvoiceQty>=101 AND InvoiceQty<=200 Then 1 else 0 End) as V101to200,  " & _
        " (Case When InvoiceQty>201 AND InvoiceQty<=500 Then 1 else 0 End) as V201to500,  (Case When InvoiceQty>500 Then 1 else 0 End) as V500Plus, MAX(CompCode) as CompCode, InvoiceQty from ( " & _
        " Select Route_No, MAX(Route_Desc) as RouteDesc, Cust_Code, SUM(Invoice_Qty ) as InvoiceQty, MAX(Comp_Code) as CompCode from ("

        End If

        Qry += " Select  '" + LocFilter + "' as LocFilter,TSPL_SALE_INVOICE_HEAD.Cust_Code,  TSPL_EMPLOYEE_MASTER.Emp_Name, TSPL_SALE_INVOICE_HEAD.Route_No, TSPL_SALE_INVOICE_HEAD.Route_Desc, " & _
        " " + strSubQry1 + ", TSPL_SALE_INVOICE_HEAD.Comp_Code   from TSPL_SALE_INVOICE_HEAD   " & _
        " Left Outer Join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  " & _
        " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_SALE_INVOICE_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code " & _
        " Left Outer Join TSPL_EMPLOYEE_MASTER on TSPL_SALE_INVOICE_HEAD.Salesman_Code=TSPL_EMPLOYEE_MASTER.EMP_CODE   " & _
        " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_SALE_INVOICE_DETAIL.Item_Code " & _
        " and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code  Where Is_Post= 'Y' AND Convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103)>=CONVERT(date, '" + fromDate.Value + "', 103) AND Convert(date,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103)<=CONVERT(date, '" + ToDate.Value + "', 103) "

        If chkVisi.IsChecked = True Then
            Qry += " AND TSPL_SALE_INVOICE_HEAD.Cust_Code in (Select TSPL_VISI_MASTER.Customer_Id from TSPL_VISI_MASTER ) "
        Else
            Qry += " AND TSPL_SALE_INVOICE_HEAD.Cust_Code NOT in (Select TSPL_VISI_MASTER.Customer_Id from TSPL_VISI_MASTER ) "
        End If

        If chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count > 0 Then
            Qry += " AND TSPL_SALE_INVOICE_HEAD.Route_No  in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
        End If
        If chkLocSelect.IsChecked = True AndAlso dgvLocation.CheckedValue.Count > 0 Then
            Qry += " AND TSPL_SALE_INVOICE_HEAD.Location  in (" + clsCommon.GetMulcallString(dgvLocation.CheckedValue) + ")"
        End If

        Qry += " ) AAA Group by Route_No, Cust_Code " & _
        " ) BBB Group By Route_No, Cust_Code, InvoiceQty " & _
        " ) CCC Group By Route_No " & _
        " ) Final Left outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code= Final.CompCode  Left Outer Join TSPL_CITY_MASTER on TSPL_COMPANY_MASTER.City_Code=TSPL_CITY_MASTER.City_Code "
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found")
            Else
                If chkVisi.IsChecked = True Then
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "crptVisiVPOReport", " Visi VPO Report")
                Else
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "crptNonVisiVPOReport", " Visi Non VPO Report")
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged
        cbgRoute.Enabled = False
    End Sub

    Private Sub chkRouteSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = True
    End Sub

   
    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        dgvLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        dgvLocation.Enabled = True
    End Sub

 
End Class
