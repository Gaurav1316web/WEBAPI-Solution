Imports common
'---------update by vipin on 07/11/2012
Public Class FrmScrapSaleInvoice
    Inherits FrmMainTranScreen
    Dim strqry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub btnprint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint1.Click
        PrintData()
    End Sub
    Sub PrintData()
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy")
        funPrint(fromdate, todate)
    End Sub
    Sub funPrint(ByVal fromdate As String, ByVal todate As String)

        Dim CustomerFilter As String = ""
        Dim LocCodeFilter As String = ""
        Dim DocFilter As String = ""
        If chk_doc_select.IsChecked = True And cbgDoc.CheckedValue.Count > 0 Then
            DocFilter = clsCommon.GetMulcallString(cbgDoc.CheckedValue)
            DocFilter = DocFilter.Replace("'", "")
        End If
        If chkCustomerSelect.IsChecked = True And cbgCustomer.CheckedValue.Count > 0 Then
            CustomerFilter = clsCommon.GetMulcallString(cbgCustomer.CheckedValue)
            CustomerFilter = CustomerFilter.Replace("'", "")
        End If
        If chkLocationSelect.IsChecked = True And cbgLocation.CheckedValue.Count > 0 Then
            LocCodeFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
            LocCodeFilter = LocCodeFilter.Replace("'", "")
        End If

        strqry = "select '" + fromdate + "' as FromDate,'" + todate + "' as Todate,'" + DocFilter + "' as DocFilter,'" + CustomerFilter + "' as CustomerFilter,'" + LocCodeFilter + "' as LocFilter, tspl_company_master.logo_img,tspl_company_master.logo_img2,tspl_company_master.Comp_Name  as CompanyName,tspl_company_master.Tin_No as CompanyTin,Case when len(tspl_company_master.Add1)>0 then tspl_company_master.Add1 else '' end +" & _
                   "case when len(tspl_company_master.Add2)>0 then ','  else  '' end  +" & _
                   "case when len(tspl_company_master.Add2)>0 then tspl_company_master.Add2  else  '' end + " & _
                   "case when len(tspl_company_master.Add3)>0 then ','  else  '' end +" & _
                   "case when len(tspl_company_master.Add3)>0 then tspl_company_master.Add3  else  '' end as CompanyAddress,h.invoice_No as ScrapInvoice,h.shipment_Date  as ScrapInvoiceDate," & _
                   "h.cust_Name as CustomerName,h.cust_Code as CustomerCode,case when len(cm.Add1)>0 then cm.Add1 else '' end +" & _
                   "case when len(cm.Add2)>0 then ',' else '' end + " & _
                   "case when len(cm.Add2)>0 then cm.Add2 else ''end +" & _
                   "case when len(cm.Add3)>0 then ',' else '' end +" & _
                   "case when len(cm.Add3)>0 then cm.Add3 else '' end   as CustomerAddress," & _
                   "TSPL_SCRAPSALE_HEAD.NRG_No  as NrgNo,cm.CST as CstNo,TSPL_LOCATION_MASTER.Tin_No as TinNo,d.Item_Code as ItemCode," & _
                   "d.Item_Desc as Desciption,d.invoice_Qty as Quantity ,d.unit_code as Uom,d.price as Rate," & _
                   "d.ItemAmt as Amount ,h.TAX1 as TaxRateDesc1,h.TAX1_Amt as TaxRate1,h.TAX2 as TaxRateDesc2," & _
                    "h.TAX2_Amt as TaxRate2,h.TAX3 as TaxRateDesc3 ,h.TAX3_Amt as TaxRate3,h.TAX4 as TaxRateDesc4," & _
                    "h.TAX4_Amt as TaxRate4,h.TAX5 as TaxRateDesc5,h.TAX5_Amt as TaxRate5,h.TAX6 as TaxRateDesc6," & _
                   "h.TAX6_Amt as TaxRate6,h.TAX7 as TaxRateDesc7,h.TAX7_Amt as TaxRate7," & _
                   "h.TAX8 as  TaxRateDesc8,h.TAX8_Amt as TaxRate8,h.TAX9 as TaxRateDesc9," & _
                   "h.TAX9_Amt as TaxRate9,h.TAX10 as TaxRateDesc10,h.TAX10_Amt as  TaxRate10 " & _
                    "from TSPL_SCRAPINVOICE_HEAD h left outer join TSPL_SCRAPINVOICE_DETAIL d on h.invoice_No =d.invoice_No " & _
                   "left outer join TSPL_CUSTOMER_MASTER   cm on h.cust_Code =cm.Cust_Code  left outer join tspl_company_master   on tspl_company_master.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "'left outer join TSPL_SCRAPSALE_HEAD  on h.shipment_No =TSPL_SCRAPSALE_HEAD.shipment_No  left outer join TSPL_LOCATION_MASTER on  h.Loc_Code=TSPL_LOCATION_MASTER.Location_Code  where 2=2"





        If chk_doc_select.IsChecked = True And cbgDoc.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one document", Me.Text)
            Exit Sub
        ElseIf chk_doc_select.IsChecked = True And cbgDoc.CheckedValue.Count > 0 Then
            strqry += " and h.invoice_No in( " + clsCommon.GetMulcallString(cbgDoc.CheckedValue) + ")"
        End If
        If chkCustomerSelect.IsChecked = True And cbgCustomer.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atlease one customer", Me.Text)
            Exit Sub
        ElseIf chkCustomerSelect.IsChecked = True And cbgCustomer.CheckedValue.Count > 0 Then
            strqry += " and h.cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
        End If
        If chkLocationSelect.IsChecked = True And cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location", Me.Text)
            Exit Sub
        ElseIf chkLocationSelect.IsChecked = True And cbgLocation.CheckedValue.Count > 0 Then
            strqry += " and h.Loc_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        End If

        strqry += " and convert(date,h.shipment_Date ,103)>= convert(date,'" + fromdate + "',103) and convert(date,h.shipment_Date,103)<= convert(date,'" + todate + "',103)"
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "ScrapSaleInvoice", "ScrapSaleInvoiceRpt")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try

    End Sub


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmScrapSaleInvoice)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnprint1.Visible = MyBase.isPrintFlag

    End Sub



    Private Sub FrmScrapSaleInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        chk_All.IsChecked = True
        chkCustomerAll.IsChecked = True
        chkLocationAll.IsChecked = True
        LoadCustomer()
        LoadDocument()
        LoadLocation()
        ButtonToolTip.SetToolTip(btnclose1, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint1, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset1, "Press Alt+R Reset the Window")
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SRP-IN-RPT"
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
    Sub LoadDocument()
        strqry = "select invoice_No as ScrapSaleInvoice ,shipment_Date as ScrapSalaInvoiceDate from TSPL_SCRAPINVOICE_HEAD "
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(strqry)
        cbgDoc.ValueMember = "ScrapSaleInvoice"
        cbgDoc.DisplayMember = "ScrapSalaInvoiceDate"
    End Sub
    Sub LoadCustomer()
        strqry = "select Cust_Code as CustomerCode,Customer_Name as CustomerName from TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strqry)
        cbgCustomer.ValueMember = "CustomerCode"
        cbgCustomer.DisplayMember = "CustomerName"
    End Sub
    Sub LoadLocation()
        strqry = "select Location_Code as Code,Location_Desc as Description from TSPL_LOCATION_MASTER where location_type='Physical' "
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(strqry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub

    Private Sub chk_All_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chk_All.ToggleStateChanged, chk_doc_select.ToggleStateChanged
        cbgDoc.Enabled = Not chk_All.IsChecked = True

    End Sub

    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged, chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked = True
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged, chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked = True

    End Sub

    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        Reset()
    End Sub
    Sub Reset()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        chk_All.IsChecked = True
        chkCustomerAll.IsChecked = True
        chkLocationAll.IsChecked = True
        LoadCustomer()
        LoadDocument()
        LoadLocation()
    End Sub

    Private Sub btnclose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose1.Click
        Me.Close()
    End Sub

    Private Sub FrmScrapSaleInvoice_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If

    End Sub
End Class
