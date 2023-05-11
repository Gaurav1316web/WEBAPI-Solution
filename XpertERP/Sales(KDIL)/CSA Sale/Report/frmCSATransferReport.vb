Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmCSATransferReport
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCSATransferReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnExport.Visible = MyBase.isExport
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

    Private Sub rbtnDocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnDocAll.ToggleStateChanged, rbtnDocSelect.ToggleStateChanged
        cbgDoc.Enabled = rbtnDocSelect.IsChecked
    End Sub

    Private Sub rbtnItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnItemAll.ToggleStateChanged, rbtnItemSelet.ToggleStateChanged
        cbgItem.Enabled = rbtnItemSelet.IsChecked
    End Sub

    Private Sub rbtnCustAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCustAll.ToggleStateChanged, rbtnCustselect.ToggleStateChanged
        cbgCust.Enabled = rbtnCustselect.IsChecked
    End Sub

    Private Enum EnumExportTo
        Excel = 0
        PDF = 1
        Print = 2
    End Enum

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

            If rbLocationsSelect.IsChecked AndAlso cbgLocations.CheckedValue.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("Select atleast one location.")
            End If

            Dim qry As String = "select final.secondary_doc_code,final.Doc_No,convert(varchar,final.doc_date,103) as Doc_Date,final.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,final.From_Location_Code,   (select m.Location_Desc  from TSPL_LOCATION_MASTER m where m.Location_Code = from_location_code) as [ From Location Desc],  final.To_Location_Code,  (SELECT  m.Location_Desc FROM TSPL_LOCATION_MASTER m WHERE m.Location_Code = To_Location_Code) AS [To Location Desc], final.Item_Code,final.Item_Desc,final.CSA_TYPE,final.Against_Form,convert(decimal(18,2),isnull(final.qty,0)) as qty,convert(decimal(18,2),sum(transfer_value )) as [Transfer value],final.UOM,final.Unit_Rate,SUM(convert(decimal(18,2),isnull(final.transfer_qty,0))) as transfer_qty,convert(decimal(18,2),sum(sale_value )) as [Sale_value],sum(convert(decimal(18,2),isnull(Return_qty,0))) as Return_qty,sum(Return_value ) as Return_value from ("
            qry += " select TSPL_CSA_TRANSFER_HEAD.doc_code as secondary_doc_code,(case when isnull(TSPL_CSA_TRANSFER_HEAD.secondary_doc_code,'')<>'' then TSPL_CSA_TRANSFER_HEAD.secondary_doc_code else TSPL_CSA_TRANSFER_HEAD.Doc_Code end) as Doc_No,TSPL_CSA_TRANSFER_HEAD.transfer_date as Doc_Date,TSPL_CSA_TRANSFER_HEAD.Cust_Code,TSPL_CSA_TRANSFER_HEAD.From_Location_Code,TSPL_CSA_TRANSFER_HEAD.To_Location_Code,TSPL_CSA_TRANSFER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.CSA_TYPE,case when isnull(TSPL_CSA_TRANSFER_HEAD.Against_Form,'')='F' then 'F-Form' else '' end as Against_Form,TSPL_CSA_TRANSFER_DETAIL.Qty,(convert(decimal(18,2),isnull(TSPL_CSA_TRANSFER_DETAIL.Transfer_Rate,0)) * convert(decimal(18,2),isnull(TSPL_CSA_TRANSFER_DETAIL.Qty,0))) as transfer_value,TSPL_CSA_TRANSFER_DETAIL.unit_code as UOM,TSPL_CSA_TRANSFER_DETAIL.transfer_rate as Unit_Rate,convert(decimal(18,2),isnull(case when isnull(uomcnvrsn.conversion_factor,0)=0 then 0 else (transfer.Qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/uomcnvrsn.conversion_factor end,0)) as transfer_qty,isnull(transfer.sale_value,0) as sale_value ,convert(decimal(18,2),isnull((case when isnull(uomcnvrsn1.conversion_factor,0)=0 then 0 else (TransferReturn .Qty * Return_Item_uom_detail.Conversion_Factor)/uomcnvrsn1.conversion_factor end),0)) as Return_qty,isnull(TransferReturn.Amount,0) as Return_value from TSPL_CSA_TRANSFER_DETAIL left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.Doc_code=TSPL_CSA_TRANSFER_DETAIL.Doc_code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CSA_TRANSFER_DETAIL.Item_Code  "

            qry += " left join (select FOC_Item ,TSPL_SD_SALE_RETURN_DETAIL.Transfer_No,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,convert(decimal(18,2),TSPL_SD_SALE_RETURN_DETAIL.Qty) as Qty ,TSPL_SD_SALE_RETURN_DETAIL.Amount,TSPL_SD_SALE_RETURN_DETAIL.Unit_code   from TSPL_SD_SALE_RETURN_DETAIL"
            qry += " left join  TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE where Trans_Type ='CSA') TransferReturn on  TransferReturn.Transfer_No =TSPL_CSA_transfer_DETAIL.Doc_code and TransferReturn.Item_Code=TSPL_CSA_transfer_DETAIL.Item_Code "
            qry += " and case when TransferReturn.FOC_Item =1 then 'Y' else 'N' end  =isnull(TSPL_CSA_TRANSFER_DETAIL.foc,'N')"
            qry += " left outer join TSPL_ITEM_UOM_DETAIL as Return_Item_uom_detail on Return_Item_uom_detail.Item_Code=TransferReturn.Item_Code and Return_Item_uom_detail.UOM_Code=TransferReturn.unit_code"
            'If rbtnpartl.IsChecked Then
            '    qry += " where transfer.Item_Code=TSPL_CSA_transfer_DETAIL.Item_Code "
            'End If
            qry += " left outer join (select TSPL_CSA_SALE_TRANSFER_DETAIL.Transfer_Line_No , foc,TSPL_CSA_SALE_TRANSFER_DETAIL.against_transfer_code as DELEVERY_ORDER_NO,TSPL_CSA_SALE_TRANSFER_DETAIL.Item_Code,convert(decimal(18,2),TSPL_CSA_SALE_TRANSFER_DETAIL.qty) as Qty,TSPL_CSA_SALE_TRANSFER_DETAIL.sale_uom as Unit_code,TSPL_CSA_SALE_TRANSFER_DETAIL.transfer_rate as Unit_Rate,convert(decimal(18,2),case when Conv_Factor>1 then (transfer_rate*qty)/Conv_Factor else transfer_rate*Alt_Qty end) as sale_value from TSPL_CSA_SALE_TRANSFER_DETAIL ) transfer on transfer.DELEVERY_ORDER_NO=TSPL_CSA_transfer_DETAIL.Doc_code and transfer.Item_Code=TSPL_CSA_transfer_DETAIL.Item_Code  and transfer.Transfer_Line_No =TSPL_CSA_TRANSFER_DETAIL.Line_No  and isnull(transfer.foc,'N')=isnull(TSPL_CSA_TRANSFER_DETAIL.foc,'N') left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=transfer.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=transfer.unit_code left outer join TSPL_ITEM_UOM_DETAIL uomcnvrsn on uomcnvrsn.Item_Code=transfer.Item_Code and uomcnvrsn.UOM_Code=TSPL_CSA_TRANSFER_DETAIL.unit_code LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL uomcnvrsn1 ON uomcnvrsn1.item_code = transferreturn.item_code AND uomcnvrsn1.uom_code = TSPL_CSA_TRANSFER_DETAIL.unit_code "
            qry += " where 2=2 "
            '----- Changes by Parteek on 24/03/2017 Client UDL
            'If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            '    If rbtnpartl.IsChecked Then
            '        qry += " where transfer.Item_Code=TSPL_CSA_transfer_DETAIL.Item_Code and TSPL_CSA_TRANSFER_HEAD.Excisable<>1 "
            '    Else
            '        qry += " where 2=2  and TSPL_CSA_TRANSFER_HEAD.Excisable<>1 "
            '    End If

            'Else
            '    If rbtnpartl.IsChecked Then
            '        qry += " where transfer.Item_Code=TSPL_CSA_transfer_DETAIL.Item_Code "
            '    End If
            'End If
            If rbtnpartl.IsChecked Then
                qry += " and transfer.Item_Code=TSPL_CSA_transfer_DETAIL.Item_Code  "
            End If
            If rbtnExcisable.IsChecked Then
                qry += " and  TSPL_CSA_TRANSFER_HEAD.Excisable=1 "
            ElseIf rbtnNonExcisable.IsChecked Then
                qry += " and  TSPL_CSA_TRANSFER_HEAD.Excisable<>1 "
            End If
            '--------End


            qry += " ) final left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=final.Cust_Code "
            qry += " where convert(date,final.doc_date,103) between '" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "' "
            If cbgItem.CheckedValue.Count > 0 Then
                qry += " and final.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
            End If
            If cbgDoc.CheckedValue.Count > 0 Then
                qry += " and (case when isnull(final.secondary_doc_code,'')<>'' then final.secondary_doc_code else final.doc_no end) in (" + clsCommon.GetMulcallString(cbgDoc.CheckedValue) + ")"
            End If
            If cbgCust.CheckedValue.Count > 0 Then
                qry += " and final.cust_code in (" + clsCommon.GetMulcallString(cbgCust.CheckedValue) + ")"
            End If

            'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 > ADDED LOCATION FILTER
            If rbLocationsSelect.IsChecked And cbgLocations.CheckedValue.Count > 0 Then
                qry += "AND final.From_Location_Code IN (" + clsCommon.GetMulcallString(cbgLocations.CheckedValue) + ")"
            End If

            qry += " group by final.Doc_No,final.secondary_doc_code,final.Doc_Date,final.Cust_Code,final.From_Location_Code,final.To_Location_Code,final.Item_Code,final.Item_Desc,final.CSA_TYPE,Final.Against_Form,final.UOM,final.Unit_Rate,final.qty,TSPL_CUSTOMER_MASTER.Customer_Name"

            If rbtnComplt.IsChecked Then
                qry = "select doc_no,doc_date,cust_code,customer_name,from_location_code, final1.[ From Location Desc] as 'From Location Name' ,   to_location_code, final1.[To Location Desc] as 'To Location Name', item_code,item_desc,csa_type,Against_Form,qty,[Transfer value],uom,unit_rate,round(transfer_qty,2) as transfer_qty,[Sale_value],Return_qty,Return_value,round(qty-transfer_qty-Return_qty,2) as balance from (" + qry + ") final1 where (final1.qty-final1.transfer_qty)=0"
            ElseIf rbtnpartl.IsChecked Then
                qry = "select doc_no,doc_date,cust_code,customer_name,from_location_code, final1.[ From Location Desc] as 'From Location Name' ,  to_location_code, final1.[To Location Desc] as 'To Location Name', item_code,item_desc,csa_type,Against_Form,qty,[Transfer value],uom,unit_rate,round(transfer_qty,2) as transfer_qty,[Sale_value],Return_qty,Return_value,round(qty-transfer_qty-Return_qty,2) as balance from (" + qry + ") final1 where (final1.qty-final1.transfer_qty)>0"
            ElseIf rbtnpending.IsChecked Then
                qry = "select doc_no,doc_date,cust_code,customer_name,from_location_code, final1.[ From Location Desc] as 'From Location Name' ,   to_location_code, final1.[To Location Desc] as 'To Location Name', item_code,item_desc,csa_type,Against_Form,qty,[Transfer value],uom,unit_rate,round(transfer_qty,2) as transfer_qty,[Sale_value],Return_qty,Return_value,round(qty-transfer_qty-Return_qty,2) as balance from (" + qry + ") final1 where (final1.qty-final1.transfer_qty)=final1.qty and final1.Return_qty<=0"

            ElseIf chkAll.IsChecked Then
                qry = "select doc_no,doc_date,cust_code,customer_name,from_location_code, final1.[ From Location Desc] as 'From Location Name',   to_location_code, final1.[To Location Desc] as 'To Location Name', item_code,item_desc,csa_type,Against_Form,qty,[Transfer value],uom,unit_rate,round(transfer_qty,2) as transfer_qty,[Sale_value],Return_qty,Return_value,round(qty-transfer_qty-Return_qty,2) as balance from (" + qry + ") final1 "
            End If
            qry += " order by convert(date,doc_date,103) "
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
                RadGroupBox1.Enabled = False
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                Throw New Exception("No data found.")
            End If
            ReStoreGridLayout()
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
                clsCommon.MyExportToExcelGrid("Transfer " + strType + " Report", gv1, arrHeader, Me.Text)
            ElseIf PrintForm = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF("Transfer " + strType + " Report", gv1, arrHeader, Me.Text, True)
            End If

        Catch ex As Exception
            RadGroupBox1.Enabled = True
            clsCommon.MyMessageBoxShow(ex.Message)
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

            'KUNAL > TICKET : BM00000009581 > ADDED LOCATION FILTER 
            gv1.Columns("From Location Name").Width = 120
            gv1.Columns("From Location Name").IsVisible = True
            gv1.Columns("From Location Name").HeaderText = "From Location Desc"

            gv1.Columns("to_location_code").Width = 120
            gv1.Columns("to_location_code").IsVisible = True
            gv1.Columns("to_location_code").HeaderText = "To Location"

            'KUNAL > TICKET : BM00000009581 > ADDED LOCATION FILTER 
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

            gv1.Columns("Against_Form").Width = 80
            gv1.Columns("Against_Form").IsVisible = True
            gv1.Columns("Against_Form").HeaderText = "Against Form"

            gv1.Columns("qty").Width = 100
            gv1.Columns("qty").IsVisible = True
            gv1.Columns("qty").HeaderText = "Transfer Quantity"
            gv1.Columns("qty").FormatString = "{0:F2}"

            gv1.Columns("uom").Width = 80
            gv1.Columns("uom").IsVisible = True
            gv1.Columns("uom").HeaderText = "Unit"

            gv1.Columns("unit_rate").Width = 80
            gv1.Columns("unit_rate").IsVisible = True
            gv1.Columns("unit_rate").HeaderText = "Unit Rate"

            gv1.Columns("transfer_qty").Width = 100
            gv1.Columns("transfer_qty").IsVisible = True
            gv1.Columns("transfer_qty").HeaderText = "Sale Quantity"
            gv1.Columns("transfer_qty").FormatString = "{0:F2}"
            gv1.Columns.Move(11, 8)

            gv1.Columns("balance").Width = 100
            gv1.Columns("balance").IsVisible = True
            gv1.Columns("balance").HeaderText = "Balance Quantity"
            gv1.Columns("balance").FormatString = "{0:F2}"

            gv1.Columns("Transfer value").Width = 100
            gv1.Columns("Transfer value").IsVisible = True
            gv1.Columns("Transfer value").HeaderText = "Transfer Value"
            gv1.Columns("Transfer value").FormatString = "{0:F2}"

            gv1.Columns("Sale_value").Width = 100
            gv1.Columns("Sale_value").IsVisible = True
            gv1.Columns("Sale_value").HeaderText = "Sale Value"
            gv1.Columns("Sale_value").FormatString = "{0:F2}"

            gv1.Columns("Return_qty").Width = 100
            gv1.Columns("Return_qty").IsVisible = True
            gv1.Columns("Return_qty").HeaderText = "Return Qty"
            gv1.Columns("Return_qty").FormatString = "{0:F2}"

            gv1.Columns("Return_value").Width = 100
            gv1.Columns("Return_value").IsVisible = True
            gv1.Columns("Return_value").HeaderText = "Return value"
            gv1.Columns("Return_value").FormatString = "{0:F2}"


            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("transfer_qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("balance", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item22 As New GridViewSummaryItem("qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item22)
            Dim item3 As New GridViewSummaryItem("Transfer value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Sale_value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("Return_qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("Return_value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FunReset()
        Try
            rbtnBoth.IsChecked = True
            chkAll.IsChecked = True
            rbtnDocAll.IsChecked = True
            rbtnItemAll.IsChecked = True
            rbtnCustAll.IsChecked = True
            'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 
            rbLocationsAll.IsChecked = True
            cbgCust.UnCheckedAll()
            cbgItem.UnCheckedAll()
            cbgDoc.UnCheckedAll()
            'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 > LOCATION FILTER 
            cbgLocations.UnCheckedAll()
            gv1.DataSource = Nothing
            RadGroupBox1.Enabled = True
            RadPageView1.SelectedPage = RadPageViewPage1
            '= KUNAL > TICKET : BM00000009466 ==========
            txtFromDate.Text = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
            txtToDate.Text = clsCommon.GETSERVERDATE
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub LoadDoc()
        Dim qry As String = "select TSPL_CSA_TRANSFER_HEAD.doc_code as Code,TSPL_CSA_TRANSFER_HEAD.transfer_date as [Date] from TSPL_CSA_TRANSFER_HEAD"
        qry += " where convert(date,TSPL_CSA_TRANSFER_HEAD.transfer_date,103) between '" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "'"
        cbgDoc.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDoc.DisplayMember = "Code"
        cbgDoc.ValueMember = "Code"
    End Sub

    Private Sub LoadItem()
        Dim qry As String = "select distinct TSPL_CSA_TRANSFER_DETAIL.Item_code as Code,tspl_item_master.Item_Desc as Description from TSPL_CSA_TRANSFER_DETAIL left outer join tspl_item_master on tspl_item_master.item_code=TSPL_CSA_TRANSFER_DETAIL.item_code"
        qry += " where TSPL_CSA_TRANSFER_DETAIL.doc_code in (select doc_code from TSPL_CSA_TRANSFER_HEAD where convert(date,transfer_date,103) between '" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "')"
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.DisplayMember = "Description"
        cbgItem.ValueMember = "Code"
    End Sub

    Private Sub LoadCustomer()
        Dim qry As String = "select distinct TSPL_CSA_TRANSFER_HEAD.cust_code as Code,tspl_customer_master.customer_name as Description from TSPL_CSA_TRANSFER_HEAD left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_CSA_TRANSFER_HEAD.cust_code"
        qry += " where convert(date,TSPL_CSA_TRANSFER_HEAD.transfer_date,103) between '" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "'"
        cbgCust.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCust.DisplayMember = "Description"
        cbgCust.ValueMember = "Code"
    End Sub

    Private Sub btngo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            PrintData(EnumExportTo.Print)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        FunReset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 
    Private Sub LoadLocations()
        Try
            cbgLocations.DataSource = Nothing
            'Dim qry As String = "select Location_Code Code , Location_Desc Name from TSPL_LOCATION_MASTER"
            Dim qry As String = "select final.Loc_Code as Code,TSPL_LOCATION_MASTER.Location_Desc as Name from (select From_Location_Code as Loc_Code from TSPL_CSA_TRANSFER_HEAD where convert(date,transfer_date,103) between '" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "' union select To_Location_Code as Loc_Code from TSPL_CSA_TRANSFER_HEAD where convert(date,transfer_date,103) between '" + clsCommon.GetPrintDate(txtFromDate.Text, "dd/MMM/yyyy") + "' and '" + clsCommon.GetPrintDate(txtToDate.Text, "dd/MMM/yyyy") + "')final left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=final.Loc_Code "
            cbgLocations.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgLocations.DisplayMember = "Name"
            cbgLocations.ValueMember = "Code"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmCSATransferReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            LoadDoc()
            LoadCustomer()
            LoadItem()
            'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 > LOCATION FILTER
            LoadLocations()
            FunReset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnexportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnexportExcel.Click
        'PrintData(EnumExportTo.Excel)
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub btnexportPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnexportPDF.Click
        'PrintData(EnumExportTo.PDF)
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub txtFromDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        If txtFromDate.Text IsNot Nothing AndAlso clsCommon.myLen(txtFromDate.Text) >= 10 Then
            LoadDoc()
            LoadItem()
            LoadCustomer()
            LoadLocations()
        End If
    End Sub

    Private Sub txtToDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtToDate.Validating
        If txtToDate.Text IsNot Nothing AndAlso clsCommon.myLen(txtToDate.Text) >= 10 Then
            LoadDoc()
            LoadItem()
            LoadCustomer()
            LoadLocations()
        End If
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmCSATransferReport & "'"))
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
                    For Each Str As String In cbgLocations.CheckedDisplayMember
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
                    clsCommon.MyExportToPDF("Transfer " + strType + " Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    'KUNAL > TICKET : BM00000009581 > DATE 27-SEP-2016 > ADDED LOCATION FILTER
    Private Sub rbLocationsAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbLocationsAll.ToggleStateChanged, rbLocationsSelect.ToggleStateChanged
        Try
            cbgLocations.Enabled = rbLocationsSelect.IsChecked
        Catch ex As Exception
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class
