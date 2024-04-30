Imports common
Imports Telerik.WinControls.UI.Export
' Update BY abhishek as on 29 oct 2012 5:20 pm For Excel
Public Class Parti_VS_Rejected
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False

    Private Sub btnprint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint1.Click
        btnReferesh = False
        PrintData()
    End Sub
    Sub PrintData()

        If chk_doc_select.IsChecked AndAlso cbgDoc.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Documnet Number")
            Return
        End If
        If chk_vendor_select.IsChecked AndAlso cbgVendor1.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
            Return
        End If
        If itemselect.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
            Return
        End If

        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy")
        printdata(fromdate, todate, chk_doc_select.IsChecked, cbgDoc.CheckedValue, chk_vendor_select.IsChecked, cbgVendor1.CheckedValue, itemselect.IsChecked, cbgItem.CheckedValue, chkLocationSelect.IsChecked, cbgLocation.CheckedValue)

    End Sub
    Sub printdata(ByVal FromDate As String, ByVal ToDate As String, ByVal isDocSelect As Boolean, ByVal ArrDoc As ArrayList, ByVal isVendorSelect As Boolean, ByVal ArrVendor As ArrayList, ByVal isitemselect As Boolean, ByVal ArrItem As ArrayList, ByVal isLocationSelect As Boolean, ByVal ArrLoc As ArrayList)
        Dim DocNo As String = ""
        Dim Vendor As String = ""
        Dim item As String = ""
        Dim location As String = ""
        Dim StrDocno As String = ""
        Dim StrVendor As String = ""
        Dim StrItem As String = ""
        Dim StrLocation As String = ""
        Dim CompanyQry As String = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
        Dim dtCompany As DataTable = clsDBFuncationality.GetDataTable(CompanyQry)

        If isDocSelect AndAlso ArrDoc.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Document")
            Return
        ElseIf isVendorSelect AndAlso ArrVendor.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Vendor For Print")
            Return
        ElseIf isLocationSelect AndAlso ArrLoc.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location For Print")
            Return
        End If

        If isDocSelect AndAlso ArrDoc.Count > 0 Then
            DocNo = "'" + clsCommon.GetMulcallString(ArrDoc) + "'"
            StrDocno = DocNo.Replace("'", "")
        End If
        If isVendorSelect AndAlso ArrVendor.Count > 0 Then
            Vendor = "'" + clsCommon.GetMulcallString(ArrVendor) + "'"
            StrVendor = Vendor.Replace("'", "")
        End If
        If isitemselect AndAlso cbgItem.CheckedValue.Count > 0 Then
            item = "'" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + "'"
            StrItem = item.Replace("'", "")
        End If
        If isLocationSelect AndAlso ArrLoc.Count > 0 Then
            location = "'" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + "'"
            StrLocation = location.Replace("'", "")
        End If
        Dim Address As String
        If isLocationSelect And ArrLoc.Count = 1 Then
            Address = "(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(TSPL_TDS_STATE_MASTER .State_Name)> 0 then ',' else '' end  +(TSPL_TDS_STATE_MASTER .State_Name )) from tspl_location_master LEFT OUTER JOIN TSPL_TDS_STATE_MASTER ON TSPL_LOCATION_MASTER .State =TSPL_TDS_STATE_MASTER .State_Code  where Location_Code = MAX(Location )  )"

        Else
            Address = "'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "'"
        End If

        Dim qry As String = "select'" + FromDate + "' as FromDate,'" + ToDate + "'as ToDate,'" + StrDocno + "' as StrDocNo,'" + StrVendor + "' as StrVendor, '" + StrItem + "' as StrItem,'" + StrLocation + "' as StrLocation, code,ICode,MAX(Bill_No )as Bill_No,max(IName) as IName,max(Unit)as Unit,max(Location) as Location,MAX(TSPL_LOCATION_MASTER.Location_Desc) as LocationName,Max(MrnNo)as MrnNo,isnull( MAX(ChalanQty ),0)as ChalanQty ,SUM(Qty* case when RI=1 then 1 else 0 end) as SRNQty, SUM(Qty* case when RI=-1 then 1 else 0 end) as InvoiceQty, SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty ,MAX(Rate) as Rate, max(Final.MRP)as MRP,max(Final.Batch_No) as Batch_No,max(Final.MFG_Date) as MFG_Date,max(Final.Expiry_Date) as Expiry_Date ,max(Disc_Per) as Disc_Per,Convert(varchar(12),max(TransDate),103) as TransDate,MAX(Vendor) as Vendor,MAX(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName ,Max(ReqStatus) as ReqStatus," + Address + "as CompaAddress,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(City_Code_Desc )> 0 then ',' else '' end +City_Code_Desc+case when len(state)> 0 then ',' else '' end +state ) from TSPL_VENDOR_MASTER  where Vendor_Code  = max(Vendor ))as vendorAddress,(select TIN_No from TSPL_VENDOR_MASTER where Vendor_Code = max(vendor) ) as tin_no,'" + FromDate + "' as FilterFromDate,'" + ToDate + "' as FilterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img,(select Logo_Img2  from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img2,max(final.RejectedQty)  as RejectedQty,sum(final.ReceiptQty) as ReceiptQty" &
                            " from (select TSPL_PI_HEAD .Vendor_Invoice_No   as Bill_No,Tspl_Srn_head.Against_MRN as MrnNo,TSPL_SRN_DETAIL .Grn_Qty as ChalanQty, TSPL_SRN_DETAIL.SRN_No as Code,TSPL_SRN_HEAD.SRN_Date as srndate,TSPL_SRN_HEAD.Vendor_Code as Vendor,TSPL_SRN_DETAIL.Item_Code as ICode,TSPL_SRN_DETAIL.Item_Desc as IName,TSPL_SRN_DETAIL.SRN_Qty as Qty,0 as Unapproved,TSPL_SRN_DETAIL.Unit_Code as Unit,TSPL_SRN_DETAIL.Location as Location,1 as RI,TSPL_SRN_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_SRN_DETAIL.MRP,TSPL_SRN_DETAIL.Batch_No,TSPL_SRN_DETAIL.MFG_Date,TSPL_SRN_DETAIL.Expiry_Date,TSPL_SRN_DETAIL.Disc_Per,TSPL_SRN_HEAD.SRN_Date as TransDate,TSPL_SRN_DETAIL.Status as ReqStatus,TSPL_SRN_HEAD .Bill_To_Location as bill_to_address,Rejected_Qty as RejectedQty ,isnull( ( select  mrn_qty  from TSPL_MRN_DETAIL where TSPL_MRN_DETAIL .MRN_No =TSPL_SRN_DETAIL .MRN_Id and TSPL_SRN_DETAIL .Item_Code =TSPL_MRN_DETAIL .Item_Code ),0 ) as ReceiptQty  from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No inner join TSPL_PI_HEAD On TSPL_SRN_HEAD .SRN_No =TSPL_PI_HEAD .Against_SRN  where TSPL_SRN_DETAIL.Status=0 and TSPL_SRN_HEAD.Status=1  "
        'If isDocSelect Then
        '    qry += " and TSPL_SRN_HEAD.SRN_No in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
        'End If
        'If isVendorSelect Then
        '    qry += " and TSPL_SRN_HEAD.Vendor_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
        'End If
        'If itemselect Then
        '    qry += ""
        'End If




        qry += "union all  select TSPL_PI_HEAD.Vendor_Invoice_No  as Bill_No, '' as MrnNo,0 as ChalanQty,TSPL_PI_DETAIL.SRN_ID as Code,'' as srndate,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,TSPL_PI_DETAIL.PI_Qty as Qty" &
                 ",0 as Unapproved,'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,'' as ReqStatus,''as bill_to_address,0 as RejectedQty,0 as ReceiptQty " &
                 " from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=1 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0 " &
                 " union all select TSPL_PI_HEAD.Vendor_Invoice_No  as Bill_No, '' as MrnNo,0 as ChalanQty, TSPL_PI_DETAIL.SRN_ID as Code,'' as srndate,TSPL_PI_HEAD.Vendor_Code as Vendor,TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.Item_Desc as IName,0  as Qty,TSPL_PI_DETAIL.PI_Qty as Unapproved" &
                 ",'' as Unit,'' as Location,-1 as RI,0 as Rate,0 as Chk,0 as MRP,'' as Batch_No,null as MFG_Date,null as Expiry_Date,0 as Disc_Per,null as TransDate,'' as ReqStatus,''as bill_to_address,0 as RejectedQty,0 as ReceiptQty" &
                 " from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_HEAD.Status=0 and len(isnull(TSPL_PI_DETAIL.SRN_Id,''))>0 and TSPL_PI_DETAIL.PI_No not in ('')  )Final left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=final.Vendor left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=Final.Location left outer join TSPL_LOCATION_MASTER as loc_master on final.bill_to_address = loc_master.Location_Code group by Code,Vendor,ICode,srndate having 2=2 "
        If isDocSelect Then
            qry += " and  code in (" + clsCommon.GetMulcallString(ArrDoc) + ") "
        End If
        If isVendorSelect Then
            qry += " and Vendor in (" + clsCommon.GetMulcallString(ArrVendor) + ") "
        End If
        If isitemselect Then
            qry += " and ICode in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
        End If
        '' Added By Abhishek as on 20/03/2012
        If isLocationSelect Then
            qry += " and max(final.bill_to_address) in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        End If
        qry += " and convert(date,srndate ,103)>= convert(date,'" + FromDate + "',103) and convert(date,srndate,103)<= convert(date,'" + ToDate + "',103)"
        ' qry += " "
        'If Not rdobtnAll.IsChecked Then


        '    If rdoInvoicenever.IsChecked = True Then
        '        qry += "and SUM(Qty* case when RI=-1 then 1 else 0 end)=0 and sum(Unapproved)=0 "
        '    End If
        '    If rdoinvoicePartial.IsChecked = True Then
        '        'qry += "and (SUM(Unapproved) >0 and  SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0 )"
        '        qry += "and(SUM(Unapproved) >0 and  SUM((Qty *RI)- Unapproved)>0 or SUM(Unapproved) >0 or(SUM(Unapproved) =0 and SUM((Qty *RI)- Unapproved) < SUM(Qty* case when RI=1 then 1 else 0 end) and SUM(Unapproved)<> SUM((Qty *RI)- Unapproved) ))"
        '    End If
        '    If rdobtnCompleted.IsChecked = True Then
        '        qry += "and ((SUM((Qty *RI)- Unapproved)=0 and SUM(Unapproved)=0) or MAX(ReqStatus)=1)"
        '    End If
        'End If
        ' Added By Abhishek kumar
        Dim qry1 As String
        If chkExcel.Checked = True Then
            Dim str As String = "Party vs Rejected"


            qry1 = "Select ForExcel.Vendor as Party,ForExcel.VendorName as [Party Name],ForExcel .ICode as Item,ForExcel .IName as [NameOf Item],Convert(Varchar(12),ForExcel .TransDate,106) as Date,ForExcel .Code as [MRN No],forexcel.Bill_No as [Bill No],forexcel.ChalanQty as [Chalan Qty],ForExcel .ReceiptQty as [Recd Qty],ForExcel .RejectedQty as [Rejected Qty],forexcel.SRNQty as AcceptedQty   from (" + qry + ") as Forexcel"
            Dim arr As New List(Of String)()

            arr.Add("  From Date:  " + FromDate + "  To Date: " + ToDate + "  ")
            If StrDocno <> "" Then
                arr.Add(" Vendor:  " + StrDocno + "")
            End If
            If StrItem <> "" Then
                arr.Add(" Vendor:  " + StrItem + "")
            End If
            If StrLocation <> "" Then
                arr.Add(" Location:   " + StrLocation + "")
            End If
            If StrDocno <> "" Then
                arr.Add(" DocumentNo:  " + StrDocno + "")
            End If
            If StrVendor <> "" Then
                arr.Add(" Vendor:  " + StrVendor + "")
            End If
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry1)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv

            End If
            clsCommon.MyExportToExcel(str, gv, arr, "Party vs Rejected")

            'ExporttoMyExcel(qry1, Me)
        Else
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gridformat()
            End If
            If btnReferesh = False Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "PartyVsRejected", "Rejected SRN Qty")
                frmCRV = Nothing
            End If
        End If

    End Sub
    Public Sub FillGridView(ByVal sql As String, ByVal gv As RadGridView)
        Dim ds As New DataSet
        Dim bs As New BindingSource()
        ds = connectSql.RunSQLReturnDS(sql)
        bs.DataSource = ds.Tables(0)
        gv.DataSource = bs
    End Sub
    Public Sub ExporttoMyExcel(ByVal sql As String, ByVal frm As RadForm)
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        Dim Fullpath As String
        sfd.FileName = frm.Text
        Dim path As String
        sfd.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            path = sfd.FileName
            Fullpath = path
        Else
            Exit Sub
        End If



        If Not path.Equals(String.Empty) Then
            Dim gv As New RadGridView()
            Try
                ''''' Dim exporter As New RadGridViewExcelExporter()
                gv.Name = "gTax"
                frm.Controls.Add(gv)
                FillGridView(sql, gv)
                If gv.Rows.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("There is no data for Show Excel Report.")
                    Exit Sub
                End If
                Dim i As Integer = 0
                For i = 0 To gv.ColumnCount - 1
                    Dim grow As GridViewRowInfo = TryCast(gv.Rows(0), GridViewRowInfo)
                    If TypeOf grow.Cells(i).Value Is DateTime Then
                        Dim datecol As GridViewDateTimeColumn = TryCast(gv.Columns(i), GridViewDateTimeColumn)
                        datecol.ExcelExportType = DisplayFormatType.ShortDate
                    End If
                Next i
                '    exporter.Export(gv, path, frm.Text)

                Dim exporter As New ExportToExcelML(gv)
                AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting

                exporter.ExportHierarchy = True
                'exporter.ExportVisualSettings = True
                exporter.SheetMaxRows = ExcelMaxRows._65536

                exporter.SheetName = frm.Text
                exporter.RunExport(Fullpath)

                frm.Controls.Remove(gv)
                '' Added By Abhishek For Show Excel Without save.
                Dim xlsApp As Microsoft.Office.Interop.Excel.Application
                Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
                'Dim xlssheet As Microsoft.Office.Interop.Excel.Worksheet
                'Dim selectedrange As Microsoft.Office.Interop.Excel.Range
                'selectedrange = xlssheet.Range("A1:a1")
                'selectedrange.EntireColumn.Columns.AutoFit()
                xlsApp = New Microsoft.Office.Interop.Excel.Application
                xlsApp.Visible = True
                xlsWB = xlsApp.Workbooks.Open(Fullpath)
                'common.clsCommon.MyMessageBoxShow("Excel Report Created!", "Export", MessageBoxButtons.OK)

            Catch ex As Exception
                frm.Controls.Remove(gv)
                common.clsCommon.MyMessageBoxShow("No Report Created.", "Export Error", MessageBoxButtons.OK)

            End Try
        End If
    End Sub

    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelML.ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewTableHeaderRowInfo) Then
            e.ExcelStyleElement.FontStyle.Bold = False
            e.ExcelStyleElement.FontStyle.Size = 8

        End If

        e.ExcelStyleElement.FontStyle.Bold = False
        e.ExcelStyleElement.FontStyle.Size = 8
        e.ExcelStyleElement.FontStyle.FontName = "Verdana"
    End Sub


    Sub LoadDocuemntNo()
        qry = "select SRN_No as 'Code',Description as [Description] from TSPL_SRN_HEAD "
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.ValueMember = "Code"
        cbgDoc.DisplayMember = "Invoice_Entry_Date"
        'cbgDocument.CheckedValue

    End Sub
    Sub LoadVendor()
        qry = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER order by Vendor_Code"
        cbgVendor1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor1.ValueMember = "Vendor_Code"
        cbgVendor1.DisplayMember = "Vendor_Name"
    End Sub

    Sub LoadItem()
        Dim qry As String = "select Item_Code as[Item Code],item_desc as [Description]  from TSPL_ITEM_MASTER "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item Code"
        cbgItem.DisplayMember = "Description"
    End Sub
    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.Parti_VS_Rejected)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub

    Private Sub Parti_VS_Rejected_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        LoadDocuemntNo()
        LoadVendor()
        LoadItem()
        LoadLocation()
        chk_All.IsChecked = True
        chkvendor_All1.IsChecked = True
        itemall.IsChecked = True
        chkLocationAll.IsChecked = True
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        chkExcel.Checked = False
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If


        ButtonToolTip.SetToolTip(btnclose1, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnprint1, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset1, "Press Alt+R Reset the Window")


    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PARTY-REJ"
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

    Private Sub chk_All_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chk_All.ToggleStateChanged, chk_doc_select.ToggleStateChanged
        cbgDoc.Enabled = Not chk_All.IsChecked

    End Sub

    Private Sub chkvendor_All1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkvendor_All1.ToggleStateChanged
        cbgVendor1.Enabled = Not chkvendor_All1.IsChecked
    End Sub

    Private Sub itemall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles itemall.ToggleStateChanged
        cbgItem.Enabled = Not itemall.IsChecked
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub


    Private Sub btnclose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose1.Click
        Me.Close()
    End Sub

    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        reset()
    End Sub
    Sub reset()
        dtpfromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        chk_All.IsChecked = True
        chkvendor_All1.IsChecked = True
        itemall.IsChecked = True
        chkLocationAll.IsChecked = True
        chkExcel.Checked = False
    End Sub


    Private Sub Parti_VS_Rejected_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            reset()

        End If

    End Sub
    Public Sub gridformat()
        Try

            gv.MasterTemplate.SummaryRowsBottom.Clear()


            Dim summaryRowItem As New GridViewSummaryRowItem()

            gv.AllowAddNewRow = False

            gv.Columns("FromDate").IsVisible = True
            gv.Columns("FromDate").Width = 100
            gv.Columns("FromDate").HeaderText = "FromDate"

            gv.Columns("ToDate").IsVisible = True
            gv.Columns("ToDate").Width = 100
            gv.Columns("ToDate").HeaderText = "ToDate"

            gv.Columns("StrDocNo").IsVisible = False
            gv.Columns("StrDocNo").Width = 200
            gv.Columns("StrDocNo").HeaderText = "StrDocNo"

            gv.Columns("StrVendor").IsVisible = False
            gv.Columns("StrVendor").Width = 100
            gv.Columns("StrVendor").HeaderText = "StrVendor"

            gv.Columns("StrItem").IsVisible = False
            gv.Columns("StrItem").Width = 100
            gv.Columns("StrItem").HeaderText = "StrItem"

            gv.Columns("StrLocation").IsVisible = False
            gv.Columns("StrLocation").Width = 100
            gv.Columns("StrLocation").HeaderText = "StrLocation"


            gv.Columns("code").IsVisible = True
            gv.Columns("code").Width = 100
            gv.Columns("code").HeaderText = "code"


            gv.Columns("ICode").IsVisible = True
            gv.Columns("ICode").Width = 150
            gv.Columns("ICode").HeaderText = "ICode"

            gv.Columns("Bill_No").IsVisible = True
            gv.Columns("Bill_No").Width = 250
            gv.Columns("Bill_No").HeaderText = "Bill_No"



            gv.Columns("IName").IsVisible = True
            gv.Columns("IName").Width = 150
            gv.Columns("IName").HeaderText = "IName"


            gv.Columns("Unit").IsVisible = True
            gv.Columns("Unit").Width = 150
            gv.Columns("Unit").HeaderText = "Unit"



            gv.Columns("Location").IsVisible = True
            gv.Columns("Location").Width = 100
            gv.Columns("Location").HeaderText = "Location"

            gv.Columns("LocationName").IsVisible = True
            gv.Columns("LocationName").Width = 100
            gv.Columns("LocationName").HeaderText = "LocationName"

            gv.Columns("MrnNo").IsVisible = True
            gv.Columns("MrnNo").Width = 100
            gv.Columns("MrnNo").HeaderText = "MrnNo"

            gv.Columns("ChalanQty").IsVisible = True
            gv.Columns("ChalanQty").Width = 100
            gv.Columns("ChalanQty").HeaderText = "ChalanQty"

            gv.Columns("SRNQty").IsVisible = True
            gv.Columns("SRNQty").Width = 100
            gv.Columns("SRNQty").HeaderText = "SRNQty"

            gv.Columns("InvoiceQty").IsVisible = True
            gv.Columns("InvoiceQty").Width = 100
            gv.Columns("InvoiceQty").HeaderText = "InvoiceQty"

            gv.Columns("UnapprovedQty").IsVisible = True
            gv.Columns("UnapprovedQty").Width = 100
            gv.Columns("UnapprovedQty").HeaderText = "UnapprovedQty"

            gv.Columns("Rate").IsVisible = True
            gv.Columns("Rate").Width = 100
            gv.Columns("Rate").HeaderText = "Rate"

            gv.Columns("Batch_No").IsVisible = True
            gv.Columns("Batch_No").Width = 100
            gv.Columns("Batch_No").HeaderText = "Batch_No"

            gv.Columns("MFG_Date").IsVisible = True
            gv.Columns("MFG_Date").Width = 100
            gv.Columns("MFG_Date").HeaderText = "MFG_Date"

            gv.Columns("Expiry_Date").IsVisible = True
            gv.Columns("Expiry_Date").Width = 100
            gv.Columns("Expiry_Date").HeaderText = "Expiry_Date"

            gv.Columns("Disc_Per").IsVisible = True
            gv.Columns("Disc_Per").Width = 100
            gv.Columns("Disc_Per").HeaderText = "Disc_Per"

            gv.Columns("TransDate").IsVisible = True
            gv.Columns("TransDate").Width = 100
            gv.Columns("TransDate").HeaderText = "TransDate"

            gv.Columns("Vendor").IsVisible = True
            gv.Columns("Vendor").Width = 100
            gv.Columns("Vendor").HeaderText = "Vendor"

            gv.Columns("VendorName").IsVisible = True
            gv.Columns("VendorName").Width = 100
            gv.Columns("VendorName").HeaderText = "VendorName"

            gv.Columns("ReqStatus").IsVisible = True
            gv.Columns("ReqStatus").Width = 100
            gv.Columns("ReqStatus").HeaderText = "ReqStatus"

            gv.Columns("CompaAddress").IsVisible = False
            gv.Columns("CompaAddress").Width = 100
            gv.Columns("CompaAddress").HeaderText = "CompaAddress"

            gv.Columns("logo_img").IsVisible = False
            gv.Columns("logo_img").Width = 100
            gv.Columns("logo_img").HeaderText = "logo_img"

            gv.Columns("logo_img2").IsVisible = False
            gv.Columns("logo_img2").Width = 100
            gv.Columns("logo_img2").HeaderText = "logo_img2"





        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PrintData()
    End Sub
End Class
