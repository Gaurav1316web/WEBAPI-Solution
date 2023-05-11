''Created By--Preeti Gupta-KDI/15/06/18-000372----on- 18/06/2018---------------------

Imports common
Imports System.Data.SqlClient



Public Class frmCancelledTransactions_MerchantSale
    Inherits FrmMainTranScreen

    Dim trnsLstCustomer As New List(Of String)
    Dim strCustomerCode As String = Nothing
    Dim dt1 As DataTable = New DataTable()
    Dim Isrefreshed As Boolean = False    '' Variable for Validate the btnPost(Enable/Disable) and GridView
    Dim IsSelected As Boolean = False     '' Variable for Validate the btnSelectAll(ChangeText)
    Dim qry As String
    Dim dt As DataTable
    Dim count As Integer = 0
    Dim strNoOfRecord As String
    Dim trnsLst As New List(Of String)
    Dim arrUser As New ArrayList()
    Dim arrSelectedUser As New ArrayList()
    Dim strDocNo As String = Nothing
    Dim countPostedDoc As Integer = 0
    Public IsPostBack As Boolean = False
    Dim DtError As DataTable
    Dim dr As DataRow
    Public fromdate As DateTime
    Public Todate As DateTime
    Public ModuleName As String = ""
    Public Transaction As String = ""
    Public IsOpenPsted As Boolean
    Dim ButtonToolTip As New ToolTip()
    Dim isInsideLoad As Boolean = False
    Dim dtAuthen As DataTable
    Dim StrQuery As String = Nothing
    Dim ChkAllowBulkPosting As Double
    Dim arrLoc As String = Nothing
    Dim IsInsideLoadData As Boolean = True
    Dim ShowDairySaleModuleOnBulkPosting As Integer
    Dim RecordCount As Integer = 0

    Private Sub FrmPendingAproval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for Closing The Window")
        LoadLocation()
        chkLocAll.CheckState = CheckState.Checked
        ChkUserAll.CheckState = CheckState.Checked
        LoadUsers()
        arrUser = FrmUserMaster.GetSubbordinateUsers(objCommonVar.CurrentUserCode)
        SetUserMgmtNew()
        LoadBlankGrid()
        LoadModuleType()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        LoadModuleSale()
        RadPageView1.SelectedPage = RadPageViewPage1
        If clsCommon.myLen(ModuleName) > 0 Then
            cboModule.SelectedValue = ModuleName
            cboTransaction.SelectedValue = Transaction
            dtpFromDate.Value = fromdate
            dtpToDate.Value = Todate
            ShowData()
        End If
    End Sub
    Public Sub LoadModuleType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        dr = dt.NewRow()
        dr("Code") = "Merchant Sale"
        dr("Name") = "Merchant Sale"
        dt.Rows.Add(dr)

        cboModule.DataSource = dt
        cboModule.DisplayMember = "Name"
        cboModule.ValueMember = "Code"

    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                Dim check As Integer = 0
                check = clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where type='Plant' and location_code='" + obj.Default_LocCode + "'")
                If check > 0 Then

                End If
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Sub LoadModuleSale()

        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Merchant Purchase order"
        dr("Name") = "Merchant Purchase order"
        dt1.Rows.Add(dr)


        dr = dt1.NewRow()
        dr("Code") = "Merchant Sales Order"
        dr("Name") = "Merchant Sales Order"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Merchant Proforma Invoice"
        dr("Name") = "Merchant Proforma Invoice"
        dt1.Rows.Add(dr)

       
        dr = dt1.NewRow()
        dr("Code") = "Merchant Commercial Invoice & Packing List"
        dr("Name") = "Merchant Commercial Invoice & Packing List"
        dt1.Rows.Add(dr)
        '=========Added by preeti gupta Against Ticket no[KDI/25/07/18-000411]

        dr = dt1.NewRow()
        dr("Code") = "Merchant SRN"
        dr("Name") = "Merchant SRN"
        dt1.Rows.Add(dr)


        dr = dt1.NewRow()
        dr("Code") = "Merchant Sales Invoice"
        dr("Name") = "Merchant Sales Invoice"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"

    End Sub

    Private Sub cboModule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboModule.SelectedIndexChanged
        LoadTrnsListOfSelectedModeule()
    End Sub

    Public Sub LoadTrnsListOfSelectedModeule()
        If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Merchant Sale") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LoadModuleSale()
        End If

    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.AllowAddNewRow = False

    End Sub
    Sub gv1Format()
        Me.gv1.MasterTemplate.Columns("Document Id").Width = 150      ''First Column
        Me.gv1.MasterTemplate.Columns("Document Date").Width = 150    ''Second Column
        Dim count As Integer = gv1.MasterTemplate.Columns.Count
        For i As Integer = 2 To count - 2
            Me.gv1.MasterTemplate.Columns(i).Width = 120
        Next i
        Me.gv1.MasterTemplate.Columns("Description").Width = 200    ''Last Column
        For j As Integer = 1 To count - 1
            Me.gv1.MasterTemplate.Columns(j).ReadOnly = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

#Region "Showing Details on GRID"
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        If dtpFromDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow("'From date' Can't Be Greater Than 'To Date'")
        Else
            qry = Nothing
            PageSetupReport_ID = MyBase.Form_ID
            ShowData()
        End If



    End Sub

    Sub ShowData()
        Try
            If cbgUser.CheckedValue.Count > 0 Then
                arrSelectedUser = cbgUser.CheckedValue
            Else
                arrSelectedUser = arrUser
            End If

            If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Merchant Sale") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillMerchantSale()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub FillMerchantSale()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Merchant Purchase order") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing
            qry = " Select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No  as[Document Id],convert(varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as [Document Date],  " & _
           " TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name], " & _
           " isnull(TSPL_PURCHASE_ORDER_HEAD.PO_Total_Amt,0) as [Amount],'' as Customer_Code,'' as [Customer],TSPL_PURCHASE_ORDER_HEAD.Created_By as [Created By],  " & _
           " convert(varchar,TSPL_PURCHASE_ORDER_HEAD.Created_Date,103) as [Created Date],'' as Description from TSPL_PURCHASE_ORDER_HEAD   " & _
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code " & _
           " where iscancel=1 and MT_Is_Merchant_Trade =1 " & _
           " and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_PURCHASE_ORDER_HEAD.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_PURCHASE_ORDER_HEAD.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += " ORDER BY TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date, TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Merchant Sales Order") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing

            qry = " Select TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Document_Code  as[Document Id],convert(varchar,TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Document_Date,103) as [Document Date],  " & _
            " TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name], " & _
            " isnull(TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Total_Amt,0) as [Amount],TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer],TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Created_By as [Created By],  " & _
            " convert(varchar,TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Created_Date,103) as [Created Date],'' as Description from TSPL_SD_SALES_ORDER_HEAD_Cancel_Data   " & _
            " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code " & _
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code " & _
            " where isnull(TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Trans_Type ,'')='EXP' and isnull(TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.SalesOrder_Type ,'')='MT' " & _
            " and convert(date,TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Customer_Code in  (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += " ORDER BY TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Document_Date, TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Document_Code "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Merchant Proforma Invoice") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing

            qry = " Select TSPL_EX_PI_HEAD_Cancel_Data.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_EX_PI_HEAD_Cancel_Data.Document_Code  as[Document Id],convert(varchar,TSPL_EX_PI_HEAD_Cancel_Data.Document_Date,103) as [Document Date],  " & _
            " isnull(TSPL_EX_PI_HEAD_Cancel_Data.Total_Amt,0) as [Amount],TSPL_EX_PI_HEAD_Cancel_Data.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer],TSPL_EX_PI_HEAD_Cancel_Data.Created_By as [Created By],  " & _
            " convert(varchar,TSPL_EX_PI_HEAD_Cancel_Data.Created_Date,103) as [Created Date],'' as Description from TSPL_EX_PI_HEAD_Cancel_Data  " & _
            " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_EX_PI_HEAD_Cancel_Data.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code " & _
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_EX_PI_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code " & _
            " where isnull(TSPL_EX_PI_HEAD_Cancel_Data.document_type ,'')='MT' " & _
            " and convert(date,TSPL_EX_PI_HEAD_Cancel_Data.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_EX_PI_HEAD_Cancel_Data.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_EX_PI_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_EX_PI_HEAD_Cancel_Data.Customer_Code in  (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_EX_PI_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_EX_PI_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += " ORDER BY TSPL_EX_PI_HEAD_Cancel_Data.Document_Date, TSPL_EX_PI_HEAD_Cancel_Data.Document_Code "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Merchant Commercial Invoice & Packing List") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing

            qry = " Select TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Document_Code  as[Document Id],convert(varchar,TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Document_Date,103) as [Document Date],  " & _
            " isnull(TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Total_Amt,0) as [Amount],TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer],TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Created_By as [Created By],  " & _
            " convert(varchar,TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Created_Date,103) as [Created Date],'' as Description from TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data  " & _
            " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code " & _
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code " & _
            " where isnull(TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.document_type ,'')='MT' " & _
            " and convert(date,TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Customer_Code in  (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If
            qry += " ORDER BY TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Document_Date, TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Document_Code "




        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Merchant SRN") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing

            qry = " Select TSPL_SRN_HEAD_cancel_data.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_SRN_HEAD_cancel_data.SRN_No  as[Document Id],convert(varchar,TSPL_SRN_HEAD_cancel_data.SRN_Date,103) as [Document Date],  " & _
            " isnull(TSPL_SRN_HEAD_cancel_data.SRN_Total_Amt,0) as [Amount],TSPL_SRN_HEAD_cancel_data.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor],TSPL_SRN_HEAD_cancel_data.Created_By as [Created By],  " & _
            " convert(varchar,TSPL_SRN_HEAD_cancel_data.Created_Date,103) as [Created Date],'' as Description from TSPL_SRN_HEAD_cancel_data  " & _
            "  Left Outer Join TSPL_VENDOR_MASTER on TSPL_SRN_HEAD_cancel_data.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code    " & _
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SRN_HEAD_cancel_data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code " & _
            " where isnull(TSPL_SRN_HEAD_cancel_data.document_type ,'')='MT' " & _
            " and convert(date,TSPL_SRN_HEAD_cancel_data.SRN_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SRN_HEAD_cancel_data.SRN_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_SRN_HEAD_cancel_data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            'If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_SRN_HEAD_cancel_data.Vendor_Code in  (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
            'End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_SRN_HEAD_cancel_data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_SRN_HEAD_cancel_data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If
            qry += " ORDER BY TSPL_SRN_HEAD_cancel_data.SRN_No, TSPL_SRN_HEAD_cancel_data.SRN_Date "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Merchant Sales Invoice") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing

            qry = " Select TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Code  as[Document Id],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Date,103) as [Document Date],  " & _
            " isnull(TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Total_Amt,0) as [Amount],TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer],TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Created_By as [Created By],  " & _
            " convert(varchar,TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Created_Date,103) as [Created Date],'' as Description from TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data  " & _
            " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code " & _
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code " & _
            " where isnull(TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.document_type ,'')='MT' " & _
            " and convert(date,TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Customer_Code in  (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If


            qry += " ORDER BY TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Date, TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Code "

        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = dt
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1Format()
            If dt.Rows.Count <= 0 Then
                lblNoOfRecords.Text = "No Record Found"
            Else
                strNoOfRecord = clsCommon.myCstr(dt.Rows.Count)
                lblNoOfRecords.Text = "" + strNoOfRecord + " Records Found"
            End If
        End If
    End Sub
   
#End Region


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm()
    End Sub


    Sub closeForm()
        Me.Close()
    End Sub
    Sub Validation()

    End Sub



   

    Private Sub FrmPendingAproval_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            closeForm()
        End If
    End Sub

    Private Sub chkLOcAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLOcSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = True
    End Sub
    Public Sub LoadLocation()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = " Select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where  Location_Code in (" + arrLoc + ")"
        End If
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub

    Public Sub LoadUsers()
        Dim qry As String = clsUserMaster.GetSubbordinateUsersQry(objCommonVar.CurrentUserCode)
        cbgUser.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgUser.ValueMember = "User_Code"
        cbgUser.DisplayMember = "User_Name"
    End Sub

    Private Sub chkLocAll_ToggleStateChanged_1(sender As Object, args As StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub ChkUserAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkUserAll.ToggleStateChanged
        cbgUser.Enabled = Not ChkUserAll.IsChecked
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim strQry As String = ""
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub


    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        chkLocAll.CheckState = CheckState.Checked
        ChkUserAll.CheckState = CheckState.Checked
        txtCustomer.arrValueMember = Nothing
        gv1.DataSource = Nothing
    End Sub


    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Merchant Purchase order") = CompairStringResult.Equal Then
            GV2.DataSource = Nothing

            qry = " SELECT TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No AS [Document Id],TSPL_PURCHASE_ORDER_DETAIL.Location,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_PURCHASE_ORDER_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc , " & _
            " TSPL_PURCHASE_ORDER_DETAIL.Unit_code ,TSPL_PURCHASE_ORDER_DETAIL.Row_Type,'' as Scheme_Applicable ," & _
            " '' as Scheme_Item ,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty ,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost ,TSPL_PURCHASE_ORDER_DETAIL.Amount ,TSPL_PURCHASE_ORDER_DETAIL.Disc_Per ,TSPL_PURCHASE_ORDER_DETAIL.Disc_Amt ,TSPL_PURCHASE_ORDER_DETAIL.Amt_Less_Discount ,TSPL_PURCHASE_ORDER_DETAIL.Total_Tax_Amt ,TSPL_PURCHASE_ORDER_DETAIL.Item_Net_Amt  FROM TSPL_PURCHASE_ORDER_DETAIL " & _
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_PURCHASE_ORDER_DETAIL.Location =TSPL_LOCATION_MASTER.Location_Code" & _
            " Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & _
            "  WHERE TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No ='" & clsCommon.myCstr(grow.Cells("Document Id").Value) & "' "


        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Merchant Sales Order") = CompairStringResult.Equal Then
            GV2.DataSource = Nothing

            qry = " SELECT TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Document_Code AS [Document Id],TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Location,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Item_Code ,TSPL_ITEM_MASTER.Item_Desc , " & _
            " TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Unit_code ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Row_Type,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Scheme_Applicable ," & _
            " TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Scheme_Item ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Qty  ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Item_Cost ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Amount ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Disc_Per ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Disc_Amt ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Amt_Less_Discount ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Total_Tax_Amt ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Item_Net_Amt FROM TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data " & _
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Location =TSPL_LOCATION_MASTER.Location_Code" & _
            " Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Item_Code " & _
            "  WHERE TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Document_Code ='" & clsCommon.myCstr(grow.Cells("Document Id").Value) & "' "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Merchant Proforma Invoice") = CompairStringResult.Equal Then
            GV2.DataSource = Nothing

            qry = " SELECT TSPL_EX_PI_DETAIL_Cancel_Data.Document_Code AS [Document Id],TSPL_EX_PI_DETAIL_Cancel_Data.Location,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_EX_PI_DETAIL_Cancel_Data.Item_Code ,TSPL_ITEM_MASTER.Item_Desc , " & _
              " TSPL_EX_PI_DETAIL_Cancel_Data.Unit_code ,TSPL_EX_PI_DETAIL_Cancel_Data.Row_Type,TSPL_EX_PI_DETAIL_Cancel_Data.Scheme_Applicable ," & _
              " TSPL_EX_PI_DETAIL_Cancel_Data.Scheme_Item ,TSPL_EX_PI_DETAIL_Cancel_Data.Qty  ,TSPL_EX_PI_DETAIL_Cancel_Data.Item_Cost ,TSPL_EX_PI_DETAIL_Cancel_Data.Amount ,TSPL_EX_PI_DETAIL_Cancel_Data.Disc_Per ,TSPL_EX_PI_DETAIL_Cancel_Data.Disc_Amt ,TSPL_EX_PI_DETAIL_Cancel_Data.Amt_Less_Discount ,TSPL_EX_PI_DETAIL_Cancel_Data.Total_Tax_Amt ,TSPL_EX_PI_DETAIL_Cancel_Data.Item_Net_Amt FROM TSPL_EX_PI_DETAIL_Cancel_Data " & _
              " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_EX_PI_DETAIL_Cancel_Data.Location =TSPL_LOCATION_MASTER.Location_Code" & _
              " Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_EX_PI_DETAIL_Cancel_Data.Item_Code " & _
              "  WHERE TSPL_EX_PI_DETAIL_Cancel_Data.Document_Code ='" & clsCommon.myCstr(grow.Cells("Document Id").Value) & "' "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Merchant Commercial Invoice & Packing List") = CompairStringResult.Equal Then
            GV2.DataSource = Nothing

            qry = " SELECT TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Document_Code AS [Document Id],TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Location,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Item_Code ,TSPL_ITEM_MASTER.Item_Desc , " & _
              " TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Unit_code ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Row_Type,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Scheme_Applicable ," & _
              " TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Scheme_Item ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Qty  ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Item_Cost ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Amount ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Disc_Per ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Disc_Amt ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Amt_Less_Discount ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Total_Tax_Amt ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Item_Net_Amt FROM TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data " & _
              " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Location =TSPL_LOCATION_MASTER.Location_Code" & _
              " Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Item_Code " & _
              "  WHERE TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Document_Code ='" & clsCommon.myCstr(grow.Cells("Document Id").Value) & "' "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Merchant SRN") = CompairStringResult.Equal Then
            GV2.DataSource = Nothing

            qry = " SELECT TSPL_SRN_DETAIL_Cancel_Data.SRN_No AS [Document Id],TSPL_SRN_DETAIL_Cancel_Data.Location,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_SRN_DETAIL_Cancel_Data.Item_Code ,TSPL_ITEM_MASTER.Item_Desc , " & _
                " TSPL_SRN_DETAIL_Cancel_Data.Unit_code ,TSPL_SRN_DETAIL_Cancel_Data.Row_Type,'' as Scheme_Applicable ," & _
                " '' as Scheme_Item ,TSPL_SRN_DETAIL_Cancel_Data.srn_qty as Qty  ,TSPL_SRN_DETAIL_Cancel_Data.Item_Cost ,TSPL_SRN_DETAIL_Cancel_Data.Amount ,TSPL_SRN_DETAIL_Cancel_Data.Disc_Per ,TSPL_SRN_DETAIL_Cancel_Data.Disc_Amt ,TSPL_SRN_DETAIL_Cancel_Data.Amt_Less_Discount ,TSPL_SRN_DETAIL_Cancel_Data.Total_Tax_Amt ,TSPL_SRN_DETAIL_Cancel_Data.Item_Net_Amt  FROM TSPL_SRN_DETAIL_Cancel_Data " & _
                " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SRN_DETAIL_Cancel_Data.Location =TSPL_LOCATION_MASTER.Location_Code" & _
                " Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SRN_DETAIL_Cancel_Data.Item_Code " & _
                "  WHERE TSPL_SRN_DETAIL_Cancel_Data.SRN_No ='" & clsCommon.myCstr(grow.Cells("Document Id").Value) & "' "



        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Merchant Sales Invoice") = CompairStringResult.Equal Then
            GV2.DataSource = Nothing

            qry = " SELECT TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Document_Code AS [Document Id],TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Location,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Item_Code ,TSPL_ITEM_MASTER.Item_Desc , " & _
                " TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Unit_code ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Row_Type,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Scheme_Applicable ," & _
                " TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Scheme_Item ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Qty  ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Item_Cost ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Amount ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Disc_Per ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Disc_Amt ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Amt_Less_Discount ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Total_Tax_Amt ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Item_Net_Amt  FROM TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data " & _
                " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Location =TSPL_LOCATION_MASTER.Location_Code" & _
                " Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Item_Code " & _
                "  WHERE TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Document_Code ='" & clsCommon.myCstr(grow.Cells("Document Id").Value) & "' "


        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            GV2.DataSource = dt
            GV2.MasterTemplate.SummaryRowsBottom.Clear()
            gv2Format()
            RadPageView1.SelectedPage = RadPageViewPage2
        End If

    End Sub
    Sub gv2Format()
        Me.GV2.MasterTemplate.Columns("Document Id").Width = 150      ''First Column
        Dim count As Integer = GV2.MasterTemplate.Columns.Count
        For i As Integer = 2 To count - 2
            Me.GV2.MasterTemplate.Columns(i).Width = 120
        Next i
        GV2.Columns("Qty").FormatString = "{0:F2}"
        GV2.Columns("Disc_Per").FormatString = "{0:F2}"
        For j As Integer = 0 To count - 1
            Me.GV2.MasterTemplate.Columns(j).ReadOnly = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Item_Cost", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("Disc_Per", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Disc_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Amt_Less_Discount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Total_Tax_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("Item_Net_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)


        GV2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmMTSalesCancelledTransation & "'"))


            If chkLocSelect.IsChecked Then
                Dim stLocName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(stLocName) > 0 Then
                        stLocName += ", "
                    End If
                    stLocName += StrName
                Next
                Dim strLocCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strLocCode) > 0 Then
                        strLocCode += ", "
                    End If
                    strLocCode += StrCode
                Next
                arrHeader.Add(("Location Name: " + stLocName + " "))
            End If


            If chkUserSelect.IsChecked Then
                Dim stUserName As String = ""
                For Each StrName As String In cbgUser.CheckedDisplayMember
                    If clsCommon.myLen(stUserName) > 0 Then
                        stUserName += ", "
                    End If
                    stUserName += StrName
                Next
                Dim strUserCode As String = ""
                For Each StrCode As String In cbgUser.CheckedValue
                    If clsCommon.myLen(strUserCode) > 0 Then
                        strUserCode += ", "
                    End If
                    strUserCode += StrCode
                Next
                arrHeader.Add(("User Name: " + strUserCode + " "))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrValueMember))
            Else
                arrHeader.Add(("Customer: All"))
            End If

            'If RadPageView1.SelectedPage = 1 Then

            'End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Merchant Sale Cancel Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Merchant Sale Cancel Report", gv1, arrHeader, "Merchant Sale Cancel Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportToExcel(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportToExcel(EnumExportTo.PDF)
    End Sub
End Class




