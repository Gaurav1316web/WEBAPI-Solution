'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
Imports common
Imports System.IO
Public Class frmPurchaseOrderList
    Inherits FrmMainTranScreen

    Private Sub frmPurchaseOrderList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadVendor()
        LoadLocation()
        '' Anubhooti 22-Aug-2014
        LoadUser()
        Reset()
    End Sub
    Sub Reset()
        Try
            SetUserMgmtNew()
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER WHERE  Status='N'  order by Vendor_Code "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Vendor_Code"
        cbgVendor.DisplayMember = "Vendor_Name"
    End Sub

    Public Sub LoadLocation()
        'Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        Dim Qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
            Qry += "  and  Segment_code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
        End If
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub
    '' Anubhooti 21-Aug-2014
    Sub LoadUser()
        Dim qry As String = "select User_Code,User_Name from TSPL_USER_MASTER order by User_Code"
        cbguser.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbguser.ValueMember = "User_Code"
        cbguser.DisplayMember = "User_Name"
    End Sub
    ''
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPurchaseOrderList)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport

    End Sub

    Sub LoadData(Optional ByVal LoadPrint As Integer = 0)
        If chkVendor_select.IsChecked AndAlso cbgvendor.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
            Return
        End If
        If chkLocationSelect.IsChecked = CheckState.Checked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            Return
        End If
        '' Anubhooti 21-Aug-2014
        If chkUser_select.IsChecked AndAlso cbguser.CheckedValue.Count = 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one User")
            Return
        End If
        ''
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MMM/yyyy") 'change in date format Monika
        Dim todate As String = clsCommon.GetPrintDate(dtpTodate.Value, "dd/MMM/yyyy") 'change in date format Monika

        Dim qry As String
        Try
            '' Anubhooti 22-Aug-2014 (Fetches TSPL_PURCHASE_ORDER_HEAD.Created_By,TSPL_PURCHASE_ORDER_HEAD.Created_Date ,TSPL_PURCHASE_ORDER_HEAD.Modify_By ,TSPL_PURCHASE_ORDER_HEAD.Modify_Date To show on grid )
            'qry = "select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,convert(date,PurchaseOrder_Date,103) as PurchaseOrder_Date,Vendor_Code,Vendor_Name, " & _
            '"Description,Bill_To_Location,Against_Requisition,Item_Code,Item_Desc,PurchaseOrder_Qty,Unit_code, " & _
            '"Item_Cost,Item_Net_Amt,TSPL_PURCHASE_ORDER_HEAD.Created_By,convert(varchar,TSPL_PURCHASE_ORDER_HEAD.Created_Date,103) as Created_Date ,TSPL_PURCHASE_ORDER_HEAD.Modify_By ,convert(varchar,TSPL_PURCHASE_ORDER_HEAD.Modify_Date,103) as Modify_Date  from TSPL_PURCHASE_ORDER_HEAD left outer join TSPL_PURCHASE_ORDER_DETAIL on " & _
            '"TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No " & _
            '" where convert(date,PurchaseOrder_Date,103) > = '" & fromdate & "' and convert(date,PurchaseOrder_Date,103) <='" & todate & "'" 'changes in date format by Monika,because of selecting same todate and from date data would not be shown



            qry = "  select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,  case when tspl_purchase_order_head.Status = 1 then 'Close' else 'Open' end as  [Status],   convert(Date,PurchaseOrder_Date,103) as PurchaseOrder_Date,Vendor_Code,Vendor_Name,  Description,Bill_To_Location , (SELECT TSPL_LOCATION_MASTER.Location_Desc FROM TSPL_LOCATION_MASTER  WHERE Location_Code = Bill_To_Location) as [Location Detail], Against_Requisition,Item_Code,Item_Desc,PurchaseOrder_Qty,Unit_code,  Item_Cost,TSPL_PURCHASE_ORDER_DETAIL.Amount,TSPL_PURCHASE_ORDER_DETAIL.Disc_Per ,TSPL_PURCHASE_ORDER_DETAIL.Disc_Amt ,TSPL_PURCHASE_ORDER_DETAIL.Amt_Less_Discount,(TSPL_PURCHASE_ORDER_DETAIL.TAX1_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX2_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX3_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX4_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX5_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX6_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX7_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX8_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX9_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX10_Amt) as  Tax_Amt ,((TSPL_PURCHASE_ORDER_DETAIL.Amount - TSPL_PURCHASE_ORDER_DETAIL.Disc_Amt )+ (TSPL_PURCHASE_ORDER_DETAIL.TAX1_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX2_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX3_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX4_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX5_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX6_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX7_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX8_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX9_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX10_Amt)) as [IncludeTaxAmount] ,TSPL_PURCHASE_ORDER_DETAIL.Total_ItemAdd_Charge ,(((TSPL_PURCHASE_ORDER_DETAIL.Amount - TSPL_PURCHASE_ORDER_DETAIL.Disc_Amt )+ (TSPL_PURCHASE_ORDER_DETAIL.TAX1_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX2_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX3_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX4_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX5_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX6_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX7_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX8_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX9_Amt+TSPL_PURCHASE_ORDER_DETAIL.TAX10_Amt)) + TSPL_PURCHASE_ORDER_DETAIL.Total_ItemAdd_Charge ) as Document_Amount ,TSPL_PURCHASE_ORDER_HEAD.Created_By,convert(varchar,TSPL_PURCHASE_ORDER_HEAD.Created_Date,103) as Created_Date ,TSPL_PURCHASE_ORDER_HEAD.Modify_By ,convert(varchar,TSPL_PURCHASE_ORDER_HEAD.Modify_Date,103) as Modify_Date ,TSPL_PURCHASE_ORDER_HEAD.Delivery_date    from TSPL_PURCHASE_ORDER_HEAD left outer join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No "
            qry += " where convert(date,PurchaseOrder_Date,103) > = '" + fromdate + "' and convert(date,PurchaseOrder_Date,103) <='" + todate + "'" 'changes in date format by Monika,because of selecting same todate and from date data would not be shown

            If rdbagainstReq.IsChecked Then
                'qry += " and Requisition_Id <> '' or Requisition_Id is not null" 'commented by Monika beacuse it gives data out of selecting date range due to or
                qry += " and (TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id <> '' or TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id is not null)"

            ElseIf rdbwithoutReq.IsChecked Then
                'qry += " and Requisition_Id = '' or Requisition_Id is null"
                qry += " and (TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id = '' or TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id is null)"
            End If

            'KUNAL > TICKET : BM00000009696 > DATE : 25 - OCTOBER - 2016
            If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then

                qry += " and  TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location in ( Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            Else
                If clsCommon.myLen(objCommonVar.strCurrUserLocationsSegment) > 0 Then
                    qry += "  and  TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location in ( Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")) "
                End If
            End If

            If chkVendor_select.IsChecked = True Then
                'qry += " and  TSPL_PURCHASE_ORDER_HEAD.Vendor_Code in ( " + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") " 'commented by Monika 11/04/2014 in vendor location call
                qry += " and  TSPL_PURCHASE_ORDER_HEAD.Vendor_Code in ( " + clsCommon.GetMulcallString(cbgvendor.CheckedValue) + ") "
            End If

            '' Anubhooti 22-Aug-2014
            If chkUser_select.IsChecked = True Then
                qry += " and  TSPL_PURCHASE_ORDER_HEAD.Created_By in ( " + clsCommon.GetMulcallString(cbguser.CheckedValue) + ") "
            End If

            If rdbSummary.IsChecked Then
                '' Anubhooti 21-Aug-2014
                Dim SUMPayment As String = " ,(Select isnull(SUM(Payment_Amount),0) From TSPL_PAYMENT_HEADER Where TSPL_PAYMENT_HEADER.PurchaseOrder_No=a.PurchaseOrder_No AND TSPL_PAYMENT_HEADER.Posted =1 ) as Payment_Amount ,MAX(Created_By) As Created_By ,MAX(Created_Date ) As Created_Date,MAX(Modify_By ) As Modify_By ,MAX(Modify_Date ) As Modify_Date "
                ''
                'qry = "select PurchaseOrder_No, MAX([Status]) [Status], convert(varchar,max(PurchaseOrder_Date),103) as PurchaseOrder_Date,max(Vendor_Code) as Vendor_Code, " & _
                '"max(Vendor_Name) as Vendor_Name,max(Description) as Description,max(Bill_To_Location) as Bill_To_Location , MAX([Location Detail]) AS [Location Desc] " & _
                '",max(Against_Requisition) as Against_Requisition,sum(PurchaseOrder_Qty) as PurchaseOrder_Qty,max(Item_Net_Amt) as Item_Net_Amt " & SUMPayment & " from  ( " & qry & " ) a group by PurchaseOrder_No  ORDER BY convert(date,max(PurchaseOrder_Date),103)"

                ' Changed By Prabhakar 28/11/2016
                qry = " select PurchaseOrder_No, MAX([Status]) [Status], convert(varchar,max(PurchaseOrder_Date),103) as PurchaseOrder_Date,max(Vendor_Code) as Vendor_Code, max(Vendor_Name) as Vendor_Name,max(Description) as Description,max(Bill_To_Location) as Bill_To_Location , MAX([Location Detail]) as [Location Detail]  ,max(Against_Requisition) as Against_Requisition,sum(PurchaseOrder_Qty) as PurchaseOrder_Qty,sum(Amount) as Amount,  sum (Disc_Amt) as Disc_Amt , sum (Amt_Less_Discount) as Amt_Less_Discount , sum (Tax_Amt) as Tax_Amt, sum (Total_ItemAdd_Charge)  as Total_ItemAdd_Charge , sum (Total_ItemAdd_Charge)  as Total_ItemAdd_Charge, sum (IncludeTaxAmount)   as IncludeTaxAmount , sum (Document_Amount) as Document_Amount  " & SUMPayment & " from  ( " & qry & " ) a group by PurchaseOrder_No  ORDER BY convert(date,max(PurchaseOrder_Date),103)"

            End If

            If rdbAgainstSRN.IsChecked Then

                qry = "select max(ddd.Vendor_Name) as [SUPPLIER NAME],max(ddd.Against_Requisition) as [INDENT NO],  ddd.PurchaseOrder_No as [PO NO],max (convert(varchar,ddd.PurchaseOrder_Date,103)) as [PO DATE]  ,ddd.Item_Code as [ITEM CODE] , max(ddd.Item_Desc) as [PRODUCT DESCRIPTION],max(ddd.PurchaseOrder_Qty) as [QUANTITY ORDERED],max( ddd.Delivery_date) as [EXPECTED DELIVERY DATE] ,max(convert(varchar,TSPL_SRN_HEAD.SRN_Date,103)) as [ACTUAL DELIVERY DATE],sum(isnull(TSPL_SRN_DETAIL.SRN_Qty,0)) as [QUANTITY RECEIVED]  ,convert(decimal(10,2), max(ddd.PurchaseOrder_Qty))-convert(decimal(10,2), sum(isnull(TSPL_SRN_DETAIL.SRN_Qty,0))) as 'PENDING/ EXTRA QUANTITY'   from (  " + qry + " and TSPL_PURCHASE_ORDER_HEAD.Status=1  ) ddd left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.PO_ID = ddd.PurchaseOrder_No and TSPL_SRN_DETAIL.Item_Code=ddd.Item_Code  left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_NO = TSPL_SRN_DETAIL.SRN_NO  group by ddd.PurchaseOrder_No ,ddd.Item_Code  "
                Dim dtp As DataTable
                If LoadPrint = 1 Then
                    dtp = clsDBFuncationality.GetDataTable(qry)
                    If dtp Is Nothing OrElse dtp.Rows.Count <= 0 Then
                        common.clsCommon.MyMessageBoxShow("No data found between {" + dtpfromdate.Text + "- to -" + dtpTodate.Text + "}", Me.Text)
                        Exit Sub
                    End If
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.PurchaseOrder, dtp, clsERPFuncationality.CompanyAddresShowinHeader(), "rptPurchaseOrderListAgainstSRN", "Purchase Order List Against SRN Report", "Address.rpt")
                    frmCRV = Nothing
                    Exit Sub
                End If

            End If

            Dim dt As DataTable = Nothing
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, Nothing)
            gv.DataSource = Nothing
            gv.Columns.Clear()
            gv.Rows.Clear()
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No data found between {" + dtpfromdate.Text + "- to -" + dtpTodate.Text + "}", Me.Text)
                Exit Sub
            End If
            gv.DataSource = dt
            If rdbAgainstSRN.IsChecked = False Then
                SetGridFormationOFgv()
            End If
            RadPageView1.Visible = True
            RadPageView1.SelectedPage = RadPageViewPage2
            gv.BestFitColumns()
            ReStoreGridLayout()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub SetGridFormationOFgv()
        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("PurchaseOrder_No").IsVisible = True
        gv.Columns("PurchaseOrder_No").Width = 100
        gv.Columns("PurchaseOrder_No").HeaderText = "PO No"

        'KUNAL > TICKET : BM00000009696 > DATE : 25 - OCTOBER - 2016
        gv.Columns("Status").IsVisible = True
        gv.Columns("Status").Width = 100
        gv.Columns("Status").HeaderText = "Status"

        gv.Columns("PurchaseOrder_Date").IsVisible = True
        gv.Columns("PurchaseOrder_Date").Width = 100
        gv.Columns("PurchaseOrder_Date").HeaderText = "PO Date"

        gv.Columns("Vendor_Code").IsVisible = True
        gv.Columns("Vendor_Code").Width = 80
        gv.Columns("Vendor_Code").HeaderText = "Vendor Code"

        gv.Columns("Vendor_Name").IsVisible = True
        gv.Columns("Vendor_Name").Width = 100
        gv.Columns("Vendor_Name").HeaderText = "Vendor Name"

        gv.Columns("Description").IsVisible = True
        gv.Columns("Description").Width = 150
        gv.Columns("Description").HeaderText = "Description"

        gv.Columns("Bill_To_Location").IsVisible = True
        gv.Columns("Bill_To_Location").Width = 80
        gv.Columns("Bill_To_Location").HeaderText = "Location"

        'KUNAL > TICKET : BM00000009696 > DATE : 25 - OCTOBER - 2016

        gv.Columns("Location Detail").IsVisible = True
        gv.Columns("Location Detail").Width = 100
        gv.Columns("Location Detail").HeaderText = "Location Desc"

        gv.Columns("Against_Requisition").IsVisible = True
        gv.Columns("Against_Requisition").Width = 80
        gv.Columns("Against_Requisition").HeaderText = "Requisition Id"

        gv.Columns("PurchaseOrder_Qty").IsVisible = True
        gv.Columns("PurchaseOrder_Qty").Width = 80
        gv.Columns("PurchaseOrder_Qty").HeaderText = "PO Qty"

        'gv.Columns("Item_Net_Amt").IsVisible = True
        'gv.Columns("Item_Net_Amt").Width = 80
        'gv.Columns("Item_Net_Amt").HeaderText = "Net Amt"

        gv.Columns("Amount").IsVisible = True
        gv.Columns("Amount").Width = 120
        gv.Columns("Amount").HeaderText = "Extended Cost"

        'gv.Columns("Discount Amount").IsVisible = True
        'gv.Columns("Discount Amount").Width = 120
        'gv.Columns("Discount Amount").HeaderText = "Discount Amount"

      

        gv.Columns("Disc_Amt").IsVisible = True
        gv.Columns("Disc_Amt").Width = 120
        gv.Columns("Disc_Amt").HeaderText = "Discount Amount"

        gv.Columns("Amt_Less_Discount").IsVisible = True
        gv.Columns("Amt_Less_Discount").Width = 120
        gv.Columns("Amt_Less_Discount").HeaderText = "Amount After Discount"

        gv.Columns("Tax_Amt").IsVisible = True
        gv.Columns("Tax_Amt").Width = 120
        gv.Columns("Tax_Amt").HeaderText = "Tax Amount"

        'gv.Columns("Total_ItemAdd_Charge").IsVisible = True
        'gv.Columns("Total_ItemAdd_Charge").Width = 120
        'gv.Columns("Total_ItemAdd_Charge").HeaderText = "Addisnal Charge"

        gv.Columns("Total_ItemAdd_Charge").IsVisible = True
        gv.Columns("Total_ItemAdd_Charge").Width = 120
        gv.Columns("Total_ItemAdd_Charge").HeaderText = "Additional Charges"

        gv.Columns("IncludeTaxAmount").IsVisible = True
        gv.Columns("IncludeTaxAmount").Width = 120
        gv.Columns("IncludeTaxAmount").HeaderText = "Include Tax Amount"

        gv.Columns("Document_Amount").IsVisible = True
        gv.Columns("Document_Amount").Width = 120
        gv.Columns("Document_Amount").HeaderText = "Document Amount"

        '' Anubhooti 21-Aug-2014
        If rdbSummary.IsChecked = True Then
            'Document_Amount
            

            gv.Columns("Payment_Amount").IsVisible = True
            gv.Columns("Payment_Amount").Width = 120
            gv.Columns("Payment_Amount").HeaderText = "Payment Amount"



        End If

        gv.Columns("Created_By").IsVisible = True
        gv.Columns("Created_By").Width = 100
        gv.Columns("Created_By").HeaderText = "Created By"

        gv.Columns("Created_Date").IsVisible = True
        gv.Columns("Created_Date").Width = 100
        gv.Columns("Created_Date").HeaderText = "Created Date"

        gv.Columns("Modify_By").IsVisible = True
        gv.Columns("Modify_By").Width = 100
        gv.Columns("Modify_By").HeaderText = "Modify By"

        gv.Columns("Modify_Date").IsVisible = True
        gv.Columns("Modify_Date").Width = 100
        gv.Columns("Modify_Date").HeaderText = "Modify Date"
        ''
        If rdbDetail.IsChecked Then
            gv.Columns("Item_Code").IsVisible = True
            gv.Columns("Item_Code").Width = 80
            gv.Columns("Item_Code").HeaderText = "Item Code"

            gv.Columns("Item_Desc").IsVisible = True
            gv.Columns("Item_Desc").Width = 120
            gv.Columns("Item_Desc").HeaderText = "Item Desc"

            gv.Columns("Unit_code").IsVisible = True
            gv.Columns("Unit_code").Width = 50
            gv.Columns("Unit_code").HeaderText = "Unit"


            gv.Columns("Item_Cost").IsVisible = True
            gv.Columns("Item_Cost").Width = 80
            gv.Columns("Item_Cost").HeaderText = "Item Cost"

            gv.Columns("Disc_Per").IsVisible = True
            gv.Columns("Disc_Per").Width = 120
            gv.Columns("Disc_Per").HeaderText = "Discount(%)"

        End If


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("PurchaseOrder_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Item_Net_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Document_Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Total_ItemAdd_Charge", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("IncludeTaxAmount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Tax_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Amt_Less_Discount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("Disc_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Export.Click
        If gv.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PDF.Click
        If gv.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub
    Private Sub chkVendor_all_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendor_all.ToggleStateChanged
        cbgvendor.Enabled = Not chkVendor_all.IsChecked
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmPurchaseOrderList & "'"))
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If chkLocationSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location : " + strtemp)
            End If

            If chkVendor_select.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgvendor.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Vendor : " + strtemp)
            End If

            '' Anubhooti 22-Aug-2014
            If chkUser_select.IsChecked Then
                strtemp = ""
                For Each Str As String In cbguser.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("User : " + strtemp)
            End If
            ''

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
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Purchase Order List Report ", gv, arrHeader, "Purchase Order List", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Try
            PageSetupReport_ID = ReportId()
            TemplateGridview = gv
            LoadData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function ReportId()
        Dim Report_Id As String = ""
        Report_Id = MyBase.Form_ID
        If rdbSummary.IsChecked = True Then
            Report_Id += "S"
        ElseIf rdbDetail.IsChecked = True Then
            Report_Id += "D"
        End If

        If rdbAgainstSRN.IsChecked = True Then
            Report_Id += "SRN"
        End If

        Return Report_Id
    End Function

    'KUNAL > TICKET : BM00000009696 > DATE : 25 - OCTOBER - 2016
    Private Sub AddNew()
        Try
            cbgLocation.UnCheckedAll()
            cbgvendor.UnCheckedAll()
            cbguser.UnCheckedAll()
            dtpfromdate.Text = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            dtpTodate.Text = clsCommon.GETSERVERDATE
            rdbSummary.CheckState = CheckState.Checked
            rdbAll.CheckState = CheckState.Checked
            chkVendor_all.CheckState = CheckState.Checked
            chkuser_all.CheckState = CheckState.Checked
            chkLocationAll.CheckState = CheckState.Checked
            btnPrint.Enabled = False

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        Try
            'KUNAL > TICKET : BM00000009696 > DATE : 25 - OCTOBER - 2016
            AddNew()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        If gv.Rows.Count > 0 Then
            Dim strDoc
            If rdbAgainstSRN.IsChecked = True Then
                strDoc = gv.CurrentRow.Cells("PO NO").Value
            Else
                strDoc = gv.CurrentRow.Cells("PurchaseOrder_No").Value
            End If
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseOrder, strDoc)
        End If
    End Sub
    '' Anubhooti 22-Aug-2014
    Private Sub chkuser_all_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkuser_all.ToggleStateChanged
        cbguser.Enabled = Not chkuser_all.IsChecked
    End Sub

    'Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
    '    Try

    '        Dim arrHeader As List(Of String) = New List(Of String)()

    '        arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpfromdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy")) + " ")
    '        arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
    '        arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmPurchaseOrderList & "'"))
    '        If chkDoc_select1.IsChecked Then
    '            Dim strDocName As String = ""
    '            For Each StrName As String In cbgdoc.CheckedDisplayMember
    '                If clsCommon.myLen(strDocName) > 0 Then
    '                    strDocName += ", "
    '                End If
    '                strDocName += StrName
    '            Next
    '            Dim strDocCode As String = ""
    '            For Each StrCode As String In cbgdoc.CheckedValue
    '                If clsCommon.myLen(strDocCode) > 0 Then
    '                    strDocCode += ", "
    '                End If
    '                strDocCode += StrCode
    '            Next
    '            arrHeader.Add((" Document: " + strDocName + " "))
    '        End If
    '        If chkLocationSelect.IsChecked Then
    '            Dim strLocName As String = ""
    '            For Each StrName As String In cbgLocation.CheckedDisplayMember
    '                If clsCommon.myLen(strLocName) > 0 Then
    '                    strLocName += ", "
    '                End If
    '                strLocName += StrName
    '            Next
    '            Dim strLocCode As String = ""
    '            For Each StrCode As String In cbgLocation.CheckedValue
    '                If clsCommon.myLen(strLocCode) > 0 Then
    '                    strLocCode += ", "
    '                End If
    '                strLocCode += StrCode
    '            Next
    '            arrHeader.Add((" Location: " + strLocName + " "))
    '        End If
    '        If chkVendor_select.IsChecked Then
    '            Dim strVenName As String = ""
    '            For Each StrName As String In cbgvendor.CheckedDisplayMember
    '                If clsCommon.myLen(strVenName) > 0 Then
    '                    strVenName += ", "
    '                End If
    '                strVenName += StrName
    '            Next
    '            Dim strVenCode As String = ""
    '            For Each StrCode As String In cbgLocation.CheckedValue
    '                If clsCommon.myLen(strVenCode) > 0 Then
    '                    strVenCode += ", "
    '                End If
    '                strVenCode += StrCode
    '            Next
    '            arrHeader.Add((" Location: " + strVenName + " "))
    '        End If



    '        Dim sfd As SaveFileDialog = New SaveFileDialog()
    '        Dim filePath As String
    '        sfd.FileName = Me.Text
    '        sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
    '        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
    '            filePath = sfd.FileName
    '        Else
    '            Exit Sub
    '        End If
    '        transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
    '        common.clsCommon.MyMessageBoxShow("Exported Successfully.")
    '        Process.Start(filePath)
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
    '    End Try
    'End Sub

    Private Sub rdbSummary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbSummary.ToggleStateChanged
        rdbAgainstSRN.Enabled = False
        rdbAll.CheckState = CheckState.Checked
    End Sub

    Private Sub rdbDetail_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbDetail.ToggleStateChanged
        rdbAgainstSRN.Enabled = True
        rdbAll.CheckState = CheckState.Checked
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        LoadData(1)
    End Sub
    Private Sub rdbAgainstSRN_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbAgainstSRN.ToggleStateChanged
        If rdbAgainstSRN.IsChecked = True Then
            btnPrint.Enabled = True
        Else
            btnPrint.Enabled = False
        End If
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
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

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
