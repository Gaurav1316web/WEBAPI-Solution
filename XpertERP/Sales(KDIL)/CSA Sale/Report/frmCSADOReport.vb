Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmCSADOReport
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCSADOReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub btngo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            PrintData(EnumExportTo.Print)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PrintData(ByVal PrintForm As EnumExportTo)
        Try
            If txtFromDate.Text Is Nothing OrElse clsCommon.myLen(txtFromDate.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtFromDate.Focus()
                txtFromDate.Select()
                Throw New Exception("Select From Date.")
            End If

            If txtToDate.Text Is Nothing OrElse clsCommon.myLen(txtToDate.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtToDate.Focus()
                txtToDate.Select()
                Throw New Exception("Select To Date.")
            End If

            If rbtnCustselect.IsChecked AndAlso cbgCust.CheckedValue.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("Select atleast one customer.")
            End If

            If rbtnItemSelet.IsChecked AndAlso cbgItem.CheckedValue.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("Select atleast one item.")
            End If

            If rbtnDocSelect.IsChecked AndAlso cbgDoc.CheckedValue.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("Select atleast one document.")
            End If

            Dim qry As String = "select final.Doc_No,convert(varchar,final.doc_date,103)as doc_date,final.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,final.From_Location_Code,  (select m.Location_Desc  from TSPL_LOCATION_MASTER m where m.Location_Code = from_location_code) as [ From Location Desc], final.To_Location_Code, (SELECT  m.Location_Desc FROM TSPL_LOCATION_MASTER m WHERE m.Location_Code = To_Location_Code) AS [To Location Desc],final.Item_Code,final.Item_Desc,final.CSA_TYPE,(isnull(final.qty,0)) as qty,final.UOM,final.Unit_Rate,SUM(final.transfer_qty) as transfer_qty,max(final.Short_Close ) as Short_Close from ("
            qry += " select TSPL_CSA_DO_HEAD.Doc_No,TSPL_CSA_DO_HEAD.Doc_Date,TSPL_CSA_DO_HEAD.Cust_Code,TSPL_CSA_DO_HEAD.From_Location_Code,TSPL_CSA_DO_HEAD.To_Location_Code,TSPL_CSA_DO_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.CSA_TYPE,TSPL_CSA_DO_DETAIL.Qty,TSPL_CSA_DO_DETAIL.UOM,TSPL_CSA_DO_DETAIL.Unit_Rate,(case when isnull(uomcnvrsn.conversion_factor,0)=0 then 0 else (transfer.Qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/uomcnvrsn.conversion_factor end) as transfer_qty,TSPL_CSA_DO_HEAD.Short_Close from TSPL_CSA_DO_DETAIL left outer join TSPL_CSA_DO_HEAD on TSPL_CSA_DO_HEAD.Doc_No=TSPL_CSA_DO_DETAIL.Doc_No left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CSA_DO_DETAIL.Item_Code left outer join "
            qry += " (select foc,TSPL_CSA_TRANSFER_DETAIL.DELEVERY_ORDER_NO,TSPL_CSA_TRANSFER_DETAIL.Item_Code,TSPL_CSA_TRANSFER_DETAIL.Qty,TSPL_CSA_TRANSFER_DETAIL.Unit_code,TSPL_CSA_TRANSFER_DETAIL.Unit_Rate from TSPL_CSA_TRANSFER_DETAIL left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE=TSPL_CSA_TRANSFER_DETAIL.DOC_CODE where TSPL_CSA_TRANSFER_DETAIL.FOC <> 'Y') transfer on transfer.DELEVERY_ORDER_NO=TSPL_CSA_DO_DETAIL.Doc_No and transfer.Item_Code=TSPL_CSA_DO_DETAIL.Item_Code left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=transfer.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=transfer.Unit_code left outer join TSPL_ITEM_UOM_DETAIL uomcnvrsn on uomcnvrsn.Item_Code=transfer.Item_Code and uomcnvrsn.UOM_Code=TSPL_CSA_DO_DETAIL.UOM "
            If rbtnpartl.IsChecked Then
                qry += " where transfer.Item_Code=TSPL_CSA_DO_DETAIL.Item_Code"
            End If
            qry += " ) final left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Cust_Code "
            qry += " where final.doc_date between '" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "' "
            If cbgItem.CheckedValue.Count > 0 Then
                qry += " and final.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
            End If
            If cbgDoc.CheckedValue.Count > 0 Then
                qry += " and final.doc_no in (" + clsCommon.GetMulcallString(cbgDoc.CheckedValue) + ")"
            End If
            If cbgCust.CheckedValue.Count > 0 Then
                qry += " and final.cust_code in (" + clsCommon.GetMulcallString(cbgCust.CheckedValue) + ")"
            End If

            'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 > ADDED LOCATION FILTER
            If rbLocationsSelect.IsChecked And cbgMultiLocs.CheckedValue.Count > 0 Then
                qry += "AND final.From_Location_Code IN (" + clsCommon.GetMulcallString(cbgMultiLocs.CheckedValue) + ")"
            End If

            qry += " group by final.Doc_No,final.Doc_Date,final.Cust_Code,final.From_Location_Code,final.To_Location_Code,final.Item_Code,final.Item_Desc,final.CSA_TYPE,final.UOM,final.Unit_Rate,final.qty,TSPL_CUSTOMER_MASTER.Customer_Name"

            If rbtnComplt.IsChecked Then
                qry = "select doc_no,convert(varchar,doc_date,103) as doc_date,cust_code,customer_name,from_location_code,   final1.[ From Location Desc] as 'From Location Name', to_location_code,final1.[To Location Desc] as 'To Location Name',  item_code,item_desc,csa_type,case when  Short_Close = 'Y' then 'Short Close' when  Short_Close='N' then  'Dispatch' end  as DO_Status ,qty,uom,unit_rate,round(transfer_qty,2) as transfer_qty,round(qty-transfer_qty,2) as balance from (" + qry + ") final1 where (final1.qty-final1.transfer_qty)=0"
            ElseIf rbtnpartl.IsChecked Then
                qry = "select doc_no,convert(varchar,doc_date,103) as doc_date,cust_code,customer_name,from_location_code,   final1.[ From Location Desc] as 'From Location Name',   to_location_code, final1.[To Location Desc] as 'To Location Name',  final1.[To Location Desc] as 'To Location Name',  item_code,item_desc,csa_type,case when  Short_Close = 'Y' then 'Short Close' when  Short_Close='N' then  'Dispatch' end  as DO_Status ,qty,uom,unit_rate,round(transfer_qty,2) as transfer_qty,round(qty-transfer_qty,2) as balance from (" + qry + ") final1 where (final1.qty-final1.transfer_qty)>0"
            ElseIf rbtnpending.IsChecked Then
                qry = "select doc_no,convert(varchar,doc_date,103) as doc_date,cust_code,customer_name,from_location_code,  final1.[ From Location Desc] as 'From Location Name',   to_location_code,  final1.[To Location Desc] as 'To Location Name',   item_code,item_desc,csa_type,case when  Short_Close = 'Y' then 'Short Close' when  Short_Close='N' then  'Dispatch' end  as DO_Status ,qty,uom,unit_rate,round(transfer_qty,2) as transfer_qty,round(qty-transfer_qty,2) as balance from (" + qry + ") final1 where (final1.qty-final1.transfer_qty)<>0"
            End If
            qry += " order by convert(date,doc_date,103)"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = False
                gv1.EnableFiltering = True
                gv1.DataSource = dt
                SetFormatGV()
                ReStoreGridLayout()
                RadGroupBox1.Enabled = False
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                Throw New Exception("No data found.")
            End If

            '===============
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If rbtnCustselect.IsChecked Then
                Dim strLoca As String = ""
                For Each Str As String In cbgCust.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
                arrHeader.Add("Customer : " + strLoca)
            End If
            If rbtnItemSelet.IsChecked Then
                Dim strLoca As String = ""
                For Each Str As String In cbgItem.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
                arrHeader.Add("Item : " + strLoca)
            End If

            Dim strType As String = "Pending"
            If rbtnComplt.IsChecked Then
                strType = "Complete"
            ElseIf rbtnpartl.IsChecked Then
                strType = "Partial Complete"
            End If
            If PrintForm = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("DO " + strType + " Report", gv1, arrHeader, Me.Text)
            ElseIf PrintForm = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF("DO " + strType + " Report", gv1, arrHeader, Me.Text, True)
            End If

        Catch ex As Exception
            RadGroupBox1.Enabled = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetFormatGV()
        Try

            gv1.TableElement.TableHeaderHeight = 40
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = False
            Next

            gv1.Columns("doc_no").Width = 100
            gv1.Columns("doc_no").IsVisible = True
            gv1.Columns("doc_no").HeaderText = "Document Code"

            gv1.Columns("doc_date").Width = 100
            gv1.Columns("doc_date").IsVisible = True
            gv1.Columns("doc_date").HeaderText = "Document Date"
            'gv1.Columns("doc_date").FormatString = "{0:d}"

            gv1.Columns("cust_code").Width = 100
            gv1.Columns("cust_code").IsVisible = True
            gv1.Columns("cust_code").HeaderText = "Customer Code"

            gv1.Columns("customer_name").Width = 220
            gv1.Columns("customer_name").IsVisible = True
            gv1.Columns("customer_name").HeaderText = "Customer Name"

            gv1.Columns("from_location_code").Width = 120
            gv1.Columns("from_location_code").IsVisible = True
            gv1.Columns("from_location_code").HeaderText = "From Location"

            'KUNAL > TICKET : BM00000009581 
            gv1.Columns("From Location Name").Width = 120
            gv1.Columns("From Location Name").IsVisible = True
            gv1.Columns("From Location Name").HeaderText = "From Location Desc"

            gv1.Columns("to_location_code").Width = 120
            gv1.Columns("to_location_code").IsVisible = True
            gv1.Columns("to_location_code").HeaderText = "To Location"

            'KUNAL > TICKET : BM00000009581 
            gv1.Columns("To Location Name").Width = 120
            gv1.Columns("To Location Name").IsVisible = True
            gv1.Columns("To Location Name").HeaderText = "To Location Desc"

            gv1.Columns("item_code").Width = 110
            gv1.Columns("item_code").IsVisible = True
            gv1.Columns("item_code").HeaderText = "Item Code"

            gv1.Columns("item_desc").Width = 250
            gv1.Columns("item_desc").IsVisible = True
            gv1.Columns("item_desc").HeaderText = "Description"

            gv1.Columns("csa_type").Width = 120
            gv1.Columns("csa_type").IsVisible = True
            gv1.Columns("csa_type").HeaderText = "CSA Type"

            gv1.Columns("qty").Width = 100
            gv1.Columns("qty").IsVisible = True
            gv1.Columns("qty").HeaderText = "Quantity"
            gv1.Columns("qty").FormatString = "{0:F2}"

            gv1.Columns("uom").Width = 80
            gv1.Columns("uom").IsVisible = True
            gv1.Columns("uom").HeaderText = "Unit"


            gv1.Columns("DO_Status").Width = 100
            gv1.Columns("DO_Status").IsVisible = True
            gv1.Columns("DO_Status").HeaderText = "DO Status"

            gv1.Columns("unit_rate").Width = 80
            gv1.Columns("unit_rate").IsVisible = True
            gv1.Columns("unit_rate").HeaderText = "Unit Rate"

            gv1.Columns("transfer_qty").Width = 100
            gv1.Columns("transfer_qty").IsVisible = True
            gv1.Columns("transfer_qty").HeaderText = "Transfer Quantity"
            gv1.Columns("transfer_qty").FormatString = "{0:F2}"
            gv1.Columns.Move(11, 8)

            gv1.Columns("balance").Width = 100
            gv1.Columns("balance").IsVisible = True
            gv1.Columns("balance").HeaderText = "Balance Quantity"
            gv1.Columns("balance").FormatString = "{0:F2}"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("transfer_qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("balance", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item22 As New GridViewSummaryItem("qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item22)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Enum EnumExportTo
        Excel = 0
        PDF = 1
        Print = 2
    End Enum

    Private Sub btnpdf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        'PrintData(EnumExportTo.PDF)
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnexcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnexcel.Click
        'PrintData(EnumExportTo.Excel)
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        FunReset()
    End Sub

    Private Sub rbtnDocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnDocAll.ToggleStateChanged, rbtnDocSelect.ToggleStateChanged
        cbgDoc.Enabled = rbtnDocSelect.IsChecked
    End Sub

    Private Sub rbtnItemAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnItemAll.ToggleStateChanged, rbtnItemSelet.ToggleStateChanged
        cbgItem.Enabled = rbtnItemSelet.IsChecked
    End Sub

    Private Sub rbtnCustAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCustAll.ToggleStateChanged, rbtnCustselect.ToggleStateChanged
        cbgCust.Enabled = rbtnCustselect.IsChecked
    End Sub

    Private Sub FunReset()
        Try

            rbtnpending.IsChecked = True
            rbtnDocAll.IsChecked = True
            rbtnItemAll.IsChecked = True
            rbtnCustAll.IsChecked = True
            rbLocationsAll.IsChecked = True
            cbgCust.UnCheckedAll()
            cbgItem.UnCheckedAll()
            cbgDoc.UnCheckedAll()
            cbgMultiLocs.UnCheckedAll()
            RadGroupBox1.Enabled = True
            RadPageView1.SelectedPage = RadPageViewPage1
            txtFromDate.Text = clsCommon.GETSERVERDATE(Nothing)
            txtToDate.Text = clsCommon.GETSERVERDATE(Nothing)
            gv1.DataSource = Nothing

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadDoc()
        Dim qry As String = "select TSPL_CSA_DO_HEAD.doc_no as Code,TSPL_CSA_DO_HEAD.doc_date as [Date] from TSPL_CSA_DO_HEAD"
        qry += " where TSPL_CSA_DO_HEAD.doc_date between '" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "'"
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.DisplayMember = "Code"
        cbgDoc.ValueMember = "Code"
    End Sub

    Private Sub LoadItem()
        Dim qry As String = "select distinct tspl_csa_do_detail.Item_code as Code,Item_Desc as Description from tspl_csa_do_detail left outer join tspl_item_master on tspl_item_master.item_code=tspl_csa_do_detail.item_code"
        qry += " where tspl_csa_do_detail.doc_no in (select doc_no from tspl_csa_do_head where doc_date between '" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "')"
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.DisplayMember = "Description"
        cbgItem.ValueMember = "Code"
    End Sub

    Private Sub LoadCustomer()
        Dim qry As String = "select distinct TSPL_CSA_DO_HEAD.cust_code as Code,tspl_customer_master.customer_name as Description from TSPL_CSA_DO_HEAD left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_CSA_DO_HEAD.cust_code"
        qry += " where TSPL_CSA_DO_HEAD.doc_date between '" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "'"
        cbgCust.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCust.DisplayMember = "Description"
        cbgCust.ValueMember = "Code"
    End Sub
    'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 
    Private Sub LoadLocations()
        Try
            Dim qry As String = "select Location_Code Code , Location_Desc Name from TSPL_LOCATION_MASTER"
            cbgMultiLocs.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgMultiLocs.DisplayMember = "Name"
            cbgMultiLocs.ValueMember = "Code"

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub FrmCSADOReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            SetUserMgmtNew()
            LoadDoc()
            LoadItem()
            LoadCustomer()
            'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 
            LoadLocations()
            FunReset()
            rbtnpartl.Visible = False

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFromDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        If txtFromDate.Text IsNot Nothing AndAlso clsCommon.myLen(txtFromDate.Text) >= 10 Then
            LoadDoc()
            LoadCustomer()
            LoadItem()
        End If
    End Sub

    Private Sub txtToDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtToDate.Validating
        If txtToDate.Text IsNot Nothing AndAlso clsCommon.myLen(txtToDate.Text) >= 10 Then
            LoadDoc()
            LoadCustomer()
            LoadItem()
        End If
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmCSADOReport & "'"))
                If rbtnDocSelect.IsChecked Then
                    Dim strLoca As String = ""
                    For Each Str As String In cbgDoc.CheckedDisplayMember
                        If clsCommon.myLen(strLoca) > 0 Then
                            strLoca += ", "
                        End If
                        strLoca += Str
                    Next
                    arrHeader.Add("Document No : " + strLoca)
                End If
                If rbtnCustselect.IsChecked Then
                    Dim strLoca As String = ""
                    For Each Str As String In cbgCust.CheckedDisplayMember
                        If clsCommon.myLen(strLoca) > 0 Then
                            strLoca += ", "
                        End If
                        strLoca += Str
                    Next
                    arrHeader.Add("Customer : " + strLoca)
                End If
                If rbtnItemSelet.IsChecked Then
                    Dim strLoca As String = ""
                    For Each Str As String In cbgItem.CheckedDisplayMember
                        If clsCommon.myLen(strLoca) > 0 Then
                            strLoca += ", "
                        End If
                        strLoca += Str
                    Next
                    arrHeader.Add("Item : " + strLoca)
                End If
                If rbLocationsSelect.IsChecked Then
                    Dim strLoca As String = ""
                    For Each Str As String In cbgMultiLocs.CheckedDisplayMember
                        If clsCommon.myLen(strLoca) > 0 Then
                            strLoca += ", "
                        End If
                        strLoca += Str
                    Next
                    arrHeader.Add("Location : " + strLoca)
                End If
                Dim strType As String = "Pending"
                If rbtnComplt.IsChecked Then
                    strType = "Complete"
                ElseIf rbtnpartl.IsChecked Then
                    strType = "Partial Complete"
                End If
                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("DO " + strType + " Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
   
    'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 
    Private Sub rbLocationsAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbLocationsAll.ToggleStateChanged, rbLocationsSelect.ToggleStateChanged
        Try
            cbgMultiLocs.Enabled = rbLocationsSelect.IsChecked
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv1_CellEditorInitialized(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellEditorInitialized

    End Sub


    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
End Class
