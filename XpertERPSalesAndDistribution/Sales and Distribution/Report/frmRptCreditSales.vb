Imports common
Imports XpertERPEngine

Public Class frmRptCreditSales
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()


    '-----by vipin on 22/11/2012 for sale return

    '-----by vipin on 23/11/2012 for Vat
    '-----by vipin for Route and Non Route on 28/11/2012

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptCreditSaleReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmRptCreditSales_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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


    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")
        SetUserMgmtNew()
        chkCustomerAll.IsChecked = True
        LoadCustomer()
        chkLocationAll.IsChecked = True
        LoadLocation()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        LoadTemplate()
        chktempall.IsChecked = True
        btnall.IsChecked = True

    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where Credit_Customer='Y'"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub

    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        'Dim qry As String = "select Location_code as [Location],TSPL_LOCATION_MASTER.Location_Desc as[Location Description] from TSPL_LOCATION_MASTER where location_type='physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Sub LoadTemplate()
        Dim qry As String = " select distinct Tmplate_Id as [Template ID] , Description from TSPL_CUSTOMER_TEMPLATE_MASTER "
        cgvtemplate.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvtemplate.ValueMember = "Template ID"
        cgvtemplate.DisplayMember = "Description"
    End Sub

    Private Sub chktempall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktempall.ToggleStateChanged
        cgvtemplate.Enabled = Not chktempall.IsChecked
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()
    End Sub
    Sub print()
        Try

            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer")
            ElseIf chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location")
            Else


                Dim customername As String = ""
                If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                    For ii As Integer = 0 To cbgCustomer.CheckedDisplayMember.Count - 1
                        If clsCommon.myLen(customername) > 0 Then
                            customername += ", "
                        End If
                        customername += clsCommon.myCstr(cbgCustomer.CheckedDisplayMember(ii))
                    Next
                End If

                Dim Routestatus As String = ""
                If btnall.IsChecked = True Then
                    Routestatus = " ('Sale','Transfer')"
                ElseIf btnroute.IsChecked = True Then
                    Routestatus = " ('Transfer')"
                ElseIf btnnonroute.IsChecked = True Then
                    Routestatus = " ('Sale')"
                End If


                Dim qry As String = "select * from("

                qry += "select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, TSPL_SALE_INVOICE_HEAD.Cust_PONo AS CustPONo, CONVERT(varchar(10), TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as Sale_Invoice_Date,"
                qry += " TSPL_SALE_INVOICE_HEAD.Cust_Code,TSPL_SALE_INVOICE_HEAD.Cust_Name,(TSPL_CUSTOMER_MASTER.Add1+ case when LEN(TSPL_CUSTOMER_MASTER.Add2)>0 then ' ,'+TSPL_CUSTOMER_MASTER.Add2 else '' end + case when LEN(TSPL_CUSTOMER_MASTER.Add3)>0 then ' ,'+TSPL_CUSTOMER_MASTER.Add3 else '' end) as Cust_Address,TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_SALE_INVOICE_DETAIL.Item_Desc, TSPL_SALE_INVOICE_DETAIL.MRP_Amt AS mrp, TSPL_SALE_INVOICE_DETAIL.Basic_Rate, TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,TSPL_SALE_INVOICE_DETAIL.Total_Basic_Amt,TSPL_SALE_INVOICE_DETAIL.Total_Disc_Amt,TSPL_SALE_INVOICE_DETAIL.Total_Item_Amt,TSPL_SALE_INVOICE_DETAIL.TAX1_Rate,TSPL_SALE_INVOICE_DETAIL.Total_Tax_Amt,TSPL_SALE_INVOICE_DETAIL.Total_TPT, Location_Code, Location_Desc "
                qry += " from TSPL_SALE_INVOICE_DETAIL"
                qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No"
                qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code"
                qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_INVOICE_HEAD.Location "
                qry += " where TSPL_SALE_INVOICE_HEAD.Credit_invoice='Y' and TSPL_SALE_INVOICE_HEAD.Shipment_Type in " + Routestatus + " and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "'"

                If chktempall.IsChecked = True Then
                    If chkCustomerSelect.IsChecked = True Then
                        qry += " and  TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                    End If
                ElseIf chktempselect.IsChecked = True Then


                    qry += " and  TSPL_SALE_INVOICE_HEAD.cust_code in  ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "


                    If chkCustomerSelect.IsChecked = True Then
                        qry += " and  TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                    End If
                End If

                If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))"
                End If


                '----------Sale Return Impact-------
                qry += "    union all "

                qry += "select TSPL_SALE_RETURN_HEAD.Sale_Return_No as Sale_Invoice_No, TSPL_SALE_RETURN_HEAD.Cust_PONo AS CustPONo, CONVERT(varchar(10), TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) as Sale_Invoice_Date,"
                qry += " TSPL_SALE_RETURN_HEAD.Cust_Code,TSPL_SALE_RETURN_HEAD.Cust_Name,(TSPL_CUSTOMER_MASTER.Add1+ case when LEN(TSPL_CUSTOMER_MASTER.Add2)>0 then ' ,'+TSPL_CUSTOMER_MASTER.Add2 else '' end + case when LEN(TSPL_CUSTOMER_MASTER.Add3)>0 then ' ,'+TSPL_CUSTOMER_MASTER.Add3 else '' end) as Cust_Address,TSPL_SALE_RETURN_DETAIL.Item_Code,TSPL_SALE_RETURN_DETAIL.Item_Desc, TSPL_SALE_RETURN_DETAIL.MRP_Amt AS mrp, TSPL_SALE_RETURN_DETAIL.Basic_Rate, -1 * TSPL_SALE_RETURN_DETAIL.Return_Qty as Invoice_Qty, -1 * TSPL_SALE_RETURN_DETAIL.Total_Basic_Amt,-1 *  TSPL_SALE_RETURN_DETAIL.Total_Disc_Amt,-1 *  TSPL_SALE_RETURN_DETAIL.Total_Item_Amt, TSPL_SALE_RETURN_DETAIL.TAX1_Rate,-1 *  TSPL_SALE_RETURN_DETAIL.Total_Tax_Amt,-1 *  TSPL_SALE_RETURN_DETAIL.Total_TPT, Location_Code, Location_Desc "
                qry += " from TSPL_SALE_RETURN_DETAIL"
                qry += " left outer join TSPL_SALE_RETURN_HEAD on TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_RETURN_DETAIL.Sale_Return_No"
                qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_RETURN_HEAD.Invoice_No "
                qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_HEAD.Cust_Code "
                qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_RETURN_HEAD.Location "
                qry += " where TSPL_SALE_INVOICE_HEAD.Credit_invoice='Y'  and TSPL_SALE_RETURN_HEAD.Shipment_Type in " + Routestatus + "  and convert(date,TSPL_SALE_RETURN_HEAD.Invoice_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") + "' and convert(date,TSPL_SALE_RETURN_HEAD.Invoice_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "'"

                If chktempall.IsChecked = True Then
                    If chkCustomerSelect.IsChecked = True Then
                        qry += " and  TSPL_SALE_RETURN_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                    End If
                ElseIf chktempselect.IsChecked = True Then


                    qry += " and  TSPL_SALE_RETURN_HEAD.cust_code in  ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "


                    If chkCustomerSelect.IsChecked = True Then
                        qry += " and  TSPL_SALE_RETURN_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                    End If
                End If

                If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_SALE_RETURN_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))"
                End If

                If chkIC.Checked Then
                    qry += "    union all "
                    qry += "select TSPL_SALE_RETURN_INTER_HEAD.Document_No as Sale_Invoice_No, '' AS CustPONo,"
                    qry += "CONVERT(varchar(10), TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) as Sale_Invoice_Date, "
                    qry += "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code,TSPL_SALE_RETURN_INTER_HEAD.Cust_Name,"
                    qry += "(TSPL_CUSTOMER_MASTER.Add1+ case when LEN(TSPL_CUSTOMER_MASTER.Add2)>0 then ' ,'+TSPL_CUSTOMER_MASTER.Add2 else '' end + case when LEN(TSPL_CUSTOMER_MASTER.Add3)>0 then ' ,'+TSPL_CUSTOMER_MASTER.Add3 else '' end) as Cust_Address, "
                    qry += "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code,TSPL_SALE_RETURN_INTER_DETAIL.Item_Desc, TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt AS mrp,"
                    qry += " TSPL_SALE_RETURN_INTER_DETAIL.Basic_Rate, -1 * TSPL_SALE_RETURN_INTER_DETAIL.Qty as Invoice_Qty, "
                    qry += " -1 * TSPL_SALE_RETURN_INTER_DETAIL.Total_Basic_Amt,-1 *  TSPL_SALE_RETURN_INTER_DETAIL.Total_Disc_Amt,-1 *  TSPL_SALE_RETURN_INTER_DETAIL.Total_Item_Amt,"
                    qry += " TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Rate,-1 *  TSPL_SALE_RETURN_INTER_DETAIL.Total_Tax_Amt, "
                    qry += "-1 *  TSPL_SALE_RETURN_INTER_DETAIL.Total_TPT, Location_Code, Location_Desc  from TSPL_SALE_RETURN_INTER_DETAIL left outer join "
                    qry += "TSPL_SALE_RETURN_INTER_HEAD on TSPL_SALE_RETURN_INTER_HEAD.Document_No=TSPL_SALE_RETURN_INTER_DETAIL.Document_No left outer join "
                    qry += "TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_RETURN_INTER_HEAD.Cust_Code  left outer join TSPL_LOCATION_MASTER on "
                    qry += "TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_RETURN_INTER_HEAD.Location  where TSPL_CUSTOMER_MASTER.Credit_Customer='Y'   and  "
                    qry += "convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") + "'  and "
                    qry += "convert(date,TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "'  "

                    If chktempall.IsChecked = True Then
                        If chkCustomerSelect.IsChecked = True Then
                            qry += " and  TSPL_SALE_RETURN_INTER_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                        End If
                    ElseIf chktempselect.IsChecked = True Then


                        qry += " and  TSPL_SALE_RETURN_INTER_HEAD.cust_code in  ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "


                        If chkCustomerSelect.IsChecked = True Then
                            qry += " and  TSPL_SALE_RETURN_INTER_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                        End If
                    End If

                    If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                        qry += " and TSPL_SALE_RETURN_INTER_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))"
                    End If

                End If
                qry += " )base "

                '-----------------------------------




                Dim FinalQry As String = ""
                Dim dt As DataTable

                If rbtnDetail.IsChecked Then
                    FinalQry = "select Sale_Invoice_No, CustPONo, Sale_Invoice_Date,Item_Code,Item_Desc,Invoice_Qty,  mrp, Basic_Rate,Cust_Code,Cust_Name,Cust_Address, mrp, Total_Basic_Amt,Total_Disc_Amt,Total_Item_Amt,TAX1_Rate,Total_Tax_Amt,Total_TPT,(TSPL_COMPANY_MASTER.Add1+ case when LEN(TSPL_COMPANY_MASTER.Add2)>0 then  ' ,'+TSPL_COMPANY_MASTER.Add2 else '' end +case when LEN(TSPL_COMPANY_MASTER.Add3)>0 then ' ,'+TSPL_COMPANY_MASTER.Add3 else '' end )as comapnyAdd,TSPL_COMPANY_MASTER.Comp_Name,TSPL_CITY_MASTER.City_Name,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as ToDate,'" + customername + "' as Cusomer, Location_Code as Location, Location_Desc as LocDesc  from(" + qry + ""
                    FinalQry += ")Final"
                    FinalQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
                    FinalQry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER.City_Code"
                    FinalQry += " order by Final.Sale_Invoice_Date, Cust_Name"
                    dt = clsDBFuncationality.GetDataTable(FinalQry)
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "rptCreditSaleDetail", Me.Text)
                ElseIf rbtnSummary.IsChecked Then
                    FinalQry = "select Cust_Code, Cust_Name,Cust_Address,Total_Basic_Amt,Total_Disc_Amt,Total_Item_Amt,(TSPL_COMPANY_MASTER.Add1+ case when LEN(TSPL_COMPANY_MASTER.Add2)>0 then  ' ,'+TSPL_COMPANY_MASTER.Add2 else '' end +case when LEN(TSPL_COMPANY_MASTER.Add3)>0 then ' ,'+TSPL_COMPANY_MASTER.Add3 else '' end )as comapnyAdd,TSPL_COMPANY_MASTER.Comp_Name,TSPL_CITY_MASTER.City_Name,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as ToDate,'" + customername + "' as Cusomer, Location, LocDesc from("
                    FinalQry += " select Cust_Code, max(Cust_Name) as Cust_Name,MAX(Cust_Address) as Cust_Address,SUM(Total_Basic_Amt) as Total_Basic_Amt,SUM(Total_Disc_Amt) as Total_Disc_Amt,SUM(Total_Item_Amt) as Total_Item_Amt, MAX(Location_Code) as Location, MAX(Location_Desc) as LocDesc "
                    FinalQry += " from (" + qry + ")xxx"
                    FinalQry += " group by Cust_Code"
                    FinalQry += " )Final"
                    FinalQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
                    FinalQry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER.City_Code"
                    FinalQry += " order by Cust_Name"
                    dt = clsDBFuncationality.GetDataTable(FinalQry)
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "rptCreditSaleSummary", Me.Text)
                End If
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    'Sub print()
    '    Try

    '        If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
    '            common.clsCommon.MyMessageBoxShow("Please select at least one Customer")
    '        ElseIf chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
    '            common.clsCommon.MyMessageBoxShow("Please select at least one Location")
    '        Else


    '            Dim customername As String = ""
    '            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
    '                For ii As Integer = 0 To cbgCustomer.CheckedDisplayMember.Count - 1
    '                    If clsCommon.myLen(customername) > 0 Then
    '                        customername += ", "
    '                    End If
    '                    customername += clsCommon.myCstr(cbgCustomer.CheckedDisplayMember(ii))
    '                Next
    '            End If
    '            Dim qry As String = "select TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, TSPL_SALE_INVOICE_HEAD.Cust_PONo AS CustPONo, CONVERT(varchar(10), TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as Sale_Invoice_Date,"
    '            qry += " TSPL_SALE_INVOICE_HEAD.Cust_Code,TSPL_SALE_INVOICE_HEAD.Cust_Name,(TSPL_CUSTOMER_MASTER.Add1+ case when LEN(TSPL_CUSTOMER_MASTER.Add2)>0 then ' ,'+TSPL_CUSTOMER_MASTER.Add2 else '' end + case when LEN(TSPL_CUSTOMER_MASTER.Add3)>0 then ' ,'+TSPL_CUSTOMER_MASTER.Add3 else '' end) as Cust_Address,TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_SALE_INVOICE_DETAIL.Item_Desc, TSPL_SALE_INVOICE_DETAIL.MRP_Amt AS mrp, TSPL_SALE_INVOICE_DETAIL.Basic_Rate,TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,TSPL_SALE_INVOICE_DETAIL.Total_Basic_Amt,TSPL_SALE_INVOICE_DETAIL.Total_Disc_Amt,TSPL_SALE_INVOICE_DETAIL.Total_net_Amt, Location_Code, Location_Desc "
    '            qry += " from TSPL_SALE_INVOICE_DETAIL"
    '            qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No"
    '            qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code"
    '            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SALE_INVOICE_HEAD.Location "
    '            qry += " where TSPL_SALE_INVOICE_HEAD.Credit_invoice='Y' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "'"

    '            If chktempall.IsChecked = True Then
    '                If chkCustomerSelect.IsChecked = True Then
    '                    qry += " and  TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
    '                End If
    '            ElseIf chktempselect.IsChecked = True Then


    '                qry += " and  TSPL_SALE_INVOICE_HEAD.cust_code in  ( select distinct  TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from TSPL_CUSTOMER_TEMPLATE_MASTER where TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "


    '                If chkCustomerSelect.IsChecked = True Then
    '                    qry += " and  TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
    '                End If
    '            End If







    '            'If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
    '            '    qry += " and TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
    '            'End If

    '            If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
    '                qry += " and TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "))"
    '            End If

    '            Dim FinalQry As String = ""
    '            Dim dt As DataTable
    '            Dim frm As New FrmSalerReport()

    '            If rbtnDetail.IsChecked Then
    '                FinalQry = "select Sale_Invoice_No, CustPONo, Sale_Invoice_Date,Item_Code,Item_Desc,Invoice_Qty,  mrp, Basic_Rate,Cust_Code,Cust_Name,Cust_Address, mrp, Total_Basic_Amt,Total_Disc_Amt,Total_net_Amt,(TSPL_COMPANY_MASTER.Add1+ case when LEN(TSPL_COMPANY_MASTER.Add2)>0 then  ' ,'+TSPL_COMPANY_MASTER.Add2 else '' end +case when LEN(TSPL_COMPANY_MASTER.Add3)>0 then ' ,'+TSPL_COMPANY_MASTER.Add3 else '' end )as comapnyAdd,TSPL_COMPANY_MASTER.Comp_Name,TSPL_CITY_MASTER.City_Name,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as ToDate,'" + customername + "' as Cusomer, Location_Code as Location, Location_Desc as LocDesc  from(" + qry + ""
    '                FinalQry += ")Final"
    '                FinalQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
    '                FinalQry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER.City_Code"
    '                FinalQry += " order by Final.Sale_Invoice_Date, Cust_Name"
    '                dt = clsDBFuncationality.GetDataTable(FinalQry)
    '                frm.funreport(dt, "rptCreditSaleDetail", Me.Text)
    '            ElseIf rbtnSummary.IsChecked Then
    '                FinalQry = "select Cust_Code, Cust_Name,Cust_Address,Total_Basic_Amt,Total_Disc_Amt,Total_net_Amt,(TSPL_COMPANY_MASTER.Add1+ case when LEN(TSPL_COMPANY_MASTER.Add2)>0 then  ' ,'+TSPL_COMPANY_MASTER.Add2 else '' end +case when LEN(TSPL_COMPANY_MASTER.Add3)>0 then ' ,'+TSPL_COMPANY_MASTER.Add3 else '' end )as comapnyAdd,TSPL_COMPANY_MASTER.Comp_Name,TSPL_CITY_MASTER.City_Name,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as ToDate,'" + customername + "' as Cusomer, Location, LocDesc from("
    '                FinalQry += " select Cust_Code, max(Cust_Name) as Cust_Name,MAX(Cust_Address) as Cust_Address,SUM(Total_Basic_Amt) as Total_Basic_Amt,SUM(Total_Disc_Amt) as Total_Disc_Amt,SUM(Total_net_Amt) as Total_net_Amt, MAX(Location_Code) as Location, MAX(Location_Desc) as LocDesc "
    '                FinalQry += " from (" + qry + ")xxx"
    '                FinalQry += " group by Cust_Code"
    '                FinalQry += " )Final"
    '                FinalQry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
    '                FinalQry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_COMPANY_MASTER.City_Code"
    '                FinalQry += " order by Cust_Name"
    '                dt = clsDBFuncationality.GetDataTable(FinalQry)
    '                frm.funreport(dt, "rptCreditSaleSummary", Me.Text)
    '            End If
    '        End If


    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '    End Try
    'End Sub




    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        LoadLocation()
        LoadCustomer()
        LoadTemplate()
        chkCustomerAll.IsChecked = True

        chkLocationAll.IsChecked = True

        btnall.IsChecked = True
        chktempall.IsChecked = True
        chkIC.Checked = False
    End Sub
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CREDIT-SALE"
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

    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged, chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox1.Click

    End Sub
End Class
