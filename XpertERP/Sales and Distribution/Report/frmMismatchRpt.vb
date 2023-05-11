'-----26/06/2012--Updation By--[Pankaj Kumar]--Added Two Radio Buttons(Summary, Detail)--And Call the Report According with their check---Req by--Priti mam
Imports common
Imports System.Data.SqlClient

Public Class FrmMismatchRpt
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnMismatchReport)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
       
    End Sub
    Private Sub FrmMismatchRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        reset()
        LoadLocation()

        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")





    End Sub

    Sub LoadRoute()
        Dim qry As String = "select route_no,route_desc from TSPL_ROUTE_MASTER order by route_no"
        cbgroute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgroute.ValueMember = "route_no"
        cbgroute.DisplayMember = "route_desc"
    End Sub
    Sub LoadLocation()
        Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub


    Sub reset()
        chkSummary.IsChecked = True
        chkLocAll.IsChecked = True
        dtpFromdate.Value = clsCommon.GETSERVERDATE
        dtptodate.Value = clsCommon.GETSERVERDATE
        ddltype.Text = "SKU"
        ddlconversion.Text = "RAW"
        LoadRoute()
        chkAllRoute.IsChecked = True
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PrintReport()
    End Sub

    Sub PrintReport()
        Try
            Dim qry As String = ""
            Dim strSubQry1 As String = ""
            Dim strSubQry2 As String = ""
            Dim strSubQry3 As String = ""
            Dim strConverted As String = ""
            Dim head As String = ""
            Dim Group1 As String = ""
            Dim Group2 As String = ""
            'Dim conversion As String
            'Dim visifilter As String
            Dim StartDate As String = clsCommon.GetPrintDate(dtpFromdate.Value, "dd-MMM-yyyy")
            Dim EndDate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd-MMM-yyyy")
            Dim Rundate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd-MMM-yyyy")

            If chkSelectRoute.IsChecked AndAlso cbgroute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select Atleast Single Route Or Select All")
                Return
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Atleast Single Location Or select All ")
                Return
            End If

            If ddlconversion.Text = "Converted" Then
                strSubQry1 = "-(isnull(((select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='FB'and TSPL_SALE_INVOICE_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code) /  (select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='con'and TSPL_SALE_INVOICE_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code))* (case when TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0)) as Qty"
                strSubQry2 = "isnull(((select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='FB'and TSPL_TRANSFER_DETAIL .item_code =TSPL_ITEM_UOM_DETAIL.item_code) /  (select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='con'and TSPL_TRANSFER_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code))* (case when TSPL_TRANSFER_DETAIL.UOM ='FC' then (isnull(TSPL_TRANSFER_DETAIL.Item_Qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(TSPL_TRANSFER_DETAIL.Item_Qty ,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0)as Qty"
                strSubQry3 = "-(isnull(((select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='FB'and TSPL_TRANSFER_DETAIL .item_code =TSPL_ITEM_UOM_DETAIL.item_code) /  (select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='con'and TSPL_TRANSFER_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code))* (case when TSPL_TRANSFER_DETAIL.UOM ='FC' then (isnull(TSPL_TRANSFER_DETAIL.LoadIn_Qty,0)+ISNULL(TSPL_TRANSFER_DETAIL.Burst, 0)+ISNULL(TSPL_TRANSFER_DETAIL.Leak, 0)+ISNULL(TSPL_TRANSFER_DETAIL.Shortage, 0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  ((isnull(TSPL_TRANSFER_DETAIL.LoadIn_Qty ,0)+ISNULL(TSPL_TRANSFER_DETAIL.Burst, 0)+ISNULL(TSPL_TRANSFER_DETAIL.Leak, 0)+ISNULL(TSPL_TRANSFER_DETAIL.Shortage, 0)) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0)) as Qty"
                strConverted = "Converted"
            ElseIf ddlconversion.Text = "8oz" Then
                strSubQry1 = "-(isnull((( (select TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='fb'and TSPL_SALE_INVOICE_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code) / (select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='con'and TSPL_SALE_INVOICE_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code)   ) *(select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and TSPL_SALE_INVOICE_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code)   *(case when TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )),0))  as Qty"
                strSubQry2 = "isnull((( (select TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='fb'and TSPL_TRANSFER_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code) / (select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='con'and TSPL_TRANSFER_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code)   ) *(select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and TSPL_TRANSFER_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code)   *(case when TSPL_TRANSFER_DETAIL.Uom  ='FC' then (isnull(TSPL_TRANSFER_DETAIL.Item_Qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(TSPL_TRANSFER_DETAIL.Item_Qty,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )),0)  as Qty"
                strSubQry3 = "-(isnull((( (select TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='fb'and TSPL_TRANSFER_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code) / (select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='con'and TSPL_TRANSFER_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code)   ) *(select distinct TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and TSPL_TRANSFER_DETAIL.item_code =TSPL_ITEM_UOM_DETAIL.item_code)   *(case when TSPL_TRANSFER_DETAIL.Uom  ='FC' then (isnull(TSPL_TRANSFER_DETAIL.LoadIn_Qty,0)+ISNULL(TSPL_TRANSFER_DETAIL.Burst, 0)+ISNULL(TSPL_TRANSFER_DETAIL.Leak, 0)+ISNULL(TSPL_TRANSFER_DETAIL.Shortage, 0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  ((isnull(TSPL_TRANSFER_DETAIL.LoadIn_Qty,0)+ISNULL(TSPL_TRANSFER_DETAIL.Burst, 0)+ISNULL(TSPL_TRANSFER_DETAIL.Leak, 0)+ISNULL(TSPL_TRANSFER_DETAIL.Shortage, 0)) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )),0))  as Qty"
                strConverted = "8oz"
            ElseIf ddlconversion.Text = "RAW" Then
                strSubQry1 = "-((case when TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ))  as Qty "
                strSubQry2 = "(case when TSPL_TRANSFER_DETAIL.Uom  ='FC' then (isnull(TSPL_TRANSFER_DETAIL.Item_Qty ,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(TSPL_TRANSFER_DETAIL.Item_Qty ,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )  as Qty "
                strSubQry3 = "-((case when TSPL_TRANSFER_DETAIL.Uom  ='FC' then (isnull(TSPL_TRANSFER_DETAIL.LoadIn_Qty ,0)+ISNULL(TSPL_TRANSFER_DETAIL.Burst, 0)+ISNULL(TSPL_TRANSFER_DETAIL.Leak, 0)+ISNULL(TSPL_TRANSFER_DETAIL.Shortage, 0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  ((isnull(TSPL_TRANSFER_DETAIL.LoadIn_Qty ,0)+ISNULL(TSPL_TRANSFER_DETAIL.Burst, 0)+ISNULL(TSPL_TRANSFER_DETAIL.Leak, 0)+ISNULL(TSPL_TRANSFER_DETAIL.Shortage, 0)) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ))  as Qty "
                strConverted = "Raw"
            End If
             
            If ddltype.Text = "SKU" Then
                Group1 = " TSPL_TRANSFER_DETAIL.Item_Code as [Grouping] "
                Group2 = " TSPL_SALE_INVOICE_DETAIL.Item_Code as [Grouping] "
                head = "SKU Wise Missmatch report"
            ElseIf ddltype.Text = "Flavour" Then
                Group1 = " TSPL_ITEM_DETAILS.Class_Desc as [Grouping] "
                Group2 = " TSPL_ITEM_DETAILS.Class_Desc as [Grouping] "
                head = "Flavour Wise Missmatch report"
            ElseIf ddltype.Text = "Pack" Then
                Group1 = " TSPL_ITEM_DETAILS_1.Class_Desc as [Grouping] "
                Group2 = " TSPL_ITEM_DETAILS_1.Class_Desc as [Grouping] "
                head = "Pack Wise Missmatch report"
            End If



            qry = "Select '" + head + "' as Heading, '" + strConverted + "' as Conversion, '" + StartDate + "' as StartDate, '" + EndDate + "' as EndDate, '" + Rundate + "' as RunDate,  BBB.*, TSPL_EMPLOYEE_MASTER.Emp_Name as SalesMan, TSPL_COMPANY_MASTER.Comp_Name, (TSPL_COMPANY_MASTER.Add1+ CAse When ISNULL(TSPL_COMPANY_MASTER.Add2, '')='' Then '' Else ', '+TSPL_COMPANY_MASTER.Add2 End + CAse When ISNULL(TSPL_COMPANY_MASTER.Add3, '')='' Then '' else ', '+TSPL_COMPANY_MASTER.Add3 End+ Case When ISNULL(TSPL_CITY_MASTER.City_Name, '')='' Then '' else ', '+ TSPL_CITY_MASTER.City_Name End+ case When ISNULL(TSPL_COMPANY_MASTER.State,'')='' then '' else ', '+TSPL_COMPANY_MASTER.State End + case When ISNULL(TSPL_COMPANY_MASTER.Pincode,'')='' then '' Else '-'+TSPL_COMPANY_MASTER.Pincode End ) as CompAddress from (Select Route_No,Transfer_No, DocNo, (case when RI=1 then 'Settlement' else case when RI=2 then 'Memo' else 'Loadin' end end)  as Company, MAX( Salesmancode) as  Salesmancode, Grouping , SUM(Qty) as Qty, RI, MAX(Comp_Code ) as CompCode, (case when RI=1 Then DocNo else '' end) as Transfer, (case when RI=2 Then DocNo else '' end) as Memo, (case when RI=3 Then DocNo else '' end) as LoadIn from (" & _
    " Select TSPL_TRANSFER_HEAD.Route_No,TSPL_TRANSFER_HEAD.Transfer_No, TSPL_TRANSFER_HEAD.Transfer_No as DocNo, 'Settlement' as Company, " + Group1 + ", TSPL_ITEM_DETAILS.Class_Desc , TSPL_ITEM_DETAILS_1.Class_Desc as Class_Desc1 ,TSPL_TRANSFER_DETAIL.Item_Code, TSPL_TRANSFER_DETAIL.Item_Desc, TSPL_TRANSFER_DETAIL.Uom, " + strSubQry2 + ", TSPL_TRANSFER_HEAD.Salesmancode, TSPL_TRANSFER_HEAD.Comp_Code , 1 as RI  from TSPL_TRANSFER_HEAD " & _
    "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code = TSPL_TRANSFER_HEAD.From_Location " & _
    " Left Outer join TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No " & _
    " Left Outer Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_TRANSFER_DETAIL.Item_Code    and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TRANSFER_DETAIL.Uom  " & _
    " LEFT OUTER JOIN TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code    " & _
    " inner  JOIN TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code   Where (TSPL_ITEM_DETAILS.Class_Name = 'flavour') and (TSPL_ITEM_DETAILS_1.Class_Name = 'size') and  TSPL_TRANSFER_HEAD.Transfer_Type='LO' AND CONVERT(date, TSPL_TRANSFER_HEAD.Transfer_Date , 103)>=CONVERT(date, '" + dtpFromdate.Value + "', 103) AND CONVERT(date, TSPL_TRANSFER_HEAD.Transfer_Date, 103)<=CONVERT(date, '" + dtptodate.Value + "', 103) And 2 = 2"
            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
            qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
       End If
            qry += " Union All " & _
    " Select TSPL_TRANSFER_HEAD.Route_No, TSPL_TRANSFER_HEAD .Transfer_No as Transfer_No , TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as DocNo, 'MEMO' as Company, " + Group2 + ", TSPL_ITEM_DETAILS.Class_Desc , TSPL_ITEM_DETAILS_1.Class_Desc as Class_Desc1, TSPL_SALE_INVOICE_DETAIL.Item_Code , TSPL_SALE_INVOICE_DETAIL.Item_Desc, TSPL_SALE_INVOICE_DETAIL.Unit_code  , " & _
    " " + strSubQry1 + ", TSPL_SALE_INVOICE_HEAD.Salesman_Code  as SaleMan, TSPL_SALE_INVOICE_HEAD.Comp_Code, 2 as RI " & _
    " FROM TSPL_SALE_INVOICE_DETAIL " & _
    " LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
    " Left Outer Join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No " & _
    " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_SALE_INVOICE_DETAIL.Item_Code    and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code " & _
    " LEFT OUTER JOIN TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code   " & _
    " Left Outer Join TSPL_TRANSFER_HEAD on TSPL_SHIPMENT_MASTER.Transfer_No=TSPL_TRANSFER_HEAD.Transfer_No " & _
    "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code = TSPL_TRANSFER_HEAD.From_Location " & _
    " inner  JOIN TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code   " & _
    " Where TSPL_SHIPMENT_MASTER.Shipment_Type='Transfer' AND (TSPL_ITEM_DETAILS.Class_Name = 'flavour') and (TSPL_ITEM_DETAILS_1.Class_Name = 'size') AND  CONVERT(date, TSPL_TRANSFER_HEAD.Transfer_Date , 103)>=CONVERT(date, '" + dtpFromdate.Value + "', 103) AND CONVERT(date, TSPL_TRANSFER_HEAD.Transfer_Date, 103)<=CONVERT(date, '" + dtptodate.Value + "', 103) and 2=2 "
            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
                qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            qry += " Union All " & _
    " Select TSPL_TRANSFER_HEAD.Route_No,TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No, TSPL_TRANSFER_HEAD.Transfer_No as DocNo, 'Load In' as Company, " + Group1 + ", TSPL_ITEM_DETAILS.Class_Desc , TSPL_ITEM_DETAILS_1.Class_Desc as Class_Desc1 , TSPL_TRANSFER_DETAIL.Item_Code, TSPL_TRANSFER_DETAIL.Item_Desc, TSPL_TRANSFER_DETAIL.Uom, " + strSubQry3 + ", TSPL_TRANSFER_HEAD.Salesmancode, TSPL_TRANSFER_HEAD.Comp_Code , 3 as RI  from TSPL_TRANSFER_HEAD " & _
    " Left Outer join TSPL_TRANSFER_DETAIL on TSPL_TRANSFER_HEAD.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No " & _
    "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code = TSPL_TRANSFER_HEAD.From_Location " & _
    " Left Outer Join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_TRANSFER_DETAIL.Item_Code    and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TRANSFER_DETAIL.Uom " & _
    " LEFT OUTER JOIN TSPL_ITEM_DETAILS ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code   " & _
    " inner  JOIN TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code    Where TSPL_TRANSFER_HEAD.Transfer_Type='LI' AND CONVERT(date, TSPL_TRANSFER_HEAD.Transfer_Date , 103)>=CONVERT(date, '" + dtpFromdate.Value + "', 103) AND CONVERT(date, TSPL_TRANSFER_HEAD.Transfer_Date, 103)<=CONVERT(date, '" + dtptodate.Value + "', 103) AND (TSPL_ITEM_DETAILS.Class_Name = 'flavour')  and (TSPL_ITEM_DETAILS_1.Class_Name = 'size') and 2=2"
            If (chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count > 0) Then
            qry += " and TSPL_LOCATION_MASTER .Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            qry += " ) AAA Where Route_No<>''"

            If chkSelectRoute.IsChecked = True AndAlso cbgroute.CheckedValue.Count > 0 Then
                qry += " AND Route_No in (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ")"
            End If
          
            qry += " Group By Route_No,  Transfer_No,DocNo, RI, Grouping   " & _
    " )   BBB " & _
    " Left Outer Join TSPL_EMPLOYEE_MASTER on BBB.Salesmancode=TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
    " Left Outer Join TSPL_COMPANY_MASTER on BBB.CompCode=TSPL_COMPANY_MASTER.Comp_Code " & _
    " Left Outer Join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER.City_Code   Order by Route_No,  Transfer_No, RI,DocNo"


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found")
            Else
                If chkSummary.IsChecked = True Then
                    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptMismatchReportSummary", "Mismatch Report")
                Else
                    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "crptMismatchReportDetail", "Mismatch Report")
                End If

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmMismatchRpt_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
     
        If e.Alt And e.KeyCode = Keys.P Then
            PrintReport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If

    End Sub

    
    Private Sub chkAllRoute_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAllRoute.ToggleStateChanged
        cbgroute.Enabled = False
    End Sub

    Private Sub chkSelectRoute_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSelectRoute.ToggleStateChanged
        cbgroute.Enabled = True
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub
End Class
