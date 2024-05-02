''Created By--richa agarwal-KDI/15/06/18-000373----on- 18/06/2018---------------------

Imports common
Imports System.Data.SqlClient



Public Class frmCancelledTransactions_Exportsale
    Inherits FrmMainTranScreen
    Dim trnsLstCustomer As New List(Of String)
    Dim strCustomerCode As String = Nothing
    Dim arrUser As New ArrayList()
    Dim ButtonToolTip As New ToolTip()
    Public ModuleName As String = ""
    Public Transaction As String = ""
    Public fromdate As DateTime
    Public Todate As DateTime
    Dim dr As DataRow
    Dim dt As DataTable
    Dim strNoOfRecord As String
    Dim qry As String
    Dim arrSelectedUser As New ArrayList()
    Dim arrLoc As String = Nothing
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
        LoadModuleExportSale()
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
        dr("Code") = "Export Sale"
        dr("Name") = "Export Sale"
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Sub LoadModuleExportSale()

        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))


        dr = dt1.NewRow()
        dr("Code") = "Export Sales Order"
        dr("Name") = "Export Sales Order"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Export Proforma Invoice"
        dr("Name") = "Export Proforma Invoice"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Export Commercial Invoice & Packing List"
        dr("Name") = "Export Commercial Invoice & Packing List"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Export Sales Invoice"
        dr("Name") = "Export Sales Invoice"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"

    End Sub
  
    Private Sub cboModule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboModule.SelectedIndexChanged
        LoadTrnsListOfSelectedModeule()
    End Sub

    Public Sub LoadTrnsListOfSelectedModeule()
        If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Export Sale") = CompairStringResult.Equal Then
            cboTransaction.DataSource = Nothing
            LoadModuleExportSale()
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
        For j As Integer = 0 To count - 1
            Me.gv1.MasterTemplate.Columns(j).ReadOnly = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Sub gv2Format()
        Me.GV2.MasterTemplate.Columns("Document Id").Width = 150      ''First Column
        Dim count As Integer = GV2.MasterTemplate.Columns.Count
        For i As Integer = 2 To count - 2
            Me.GV2.MasterTemplate.Columns(i).Width = 120
        Next i
        For j As Integer = 0 To count - 1
            Me.GV2.MasterTemplate.Columns(j).ReadOnly = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        GV2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

#Region "Showing Details on GRID"
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        If dtpFromDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow("'From date' Can't Be Greater Than 'To Date'")
        Else
            qry = Nothing
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

            If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Export Sale") = CompairStringResult.Equal Then
                gv1.DataSource = Nothing
                FillExportSale()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub FillExportSale()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Export Sales Order") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing

            qry = " Select TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Document_Code  as[Document Id],convert(varchar,TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Document_Date,103) as [Document Date],  " & _
            " TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name], " & _
            " isnull(TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Total_Amt,0) as [Amount],TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Total_Tax_Amt  as [Total Tax Amt],TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer],TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Created_By as [Created By],  " & _
            " convert(varchar,TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Created_Date,103) as [Created Date],'' as Description from TSPL_SD_SALES_ORDER_HEAD_Cancel_Data   " & _
            " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code " & _
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code " & _
            " where isnull(TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Trans_Type ,'')='EXP' and isnull(TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.SalesOrder_Type ,'')='EX' " & _
            " and convert(date,TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += " ORDER BY TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Document_Date, TSPL_SD_SALES_ORDER_HEAD_Cancel_Data.Document_Code "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Export Proforma Invoice") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing

            qry = " Select TSPL_EX_PI_HEAD_Cancel_Data.Document_Code  as[Document Id],convert(varchar,TSPL_EX_PI_HEAD_Cancel_Data.Document_Date,103) as [Document Date],  " & _
                " TSPL_EX_PI_HEAD_Cancel_Data.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name], " & _
            " isnull(TSPL_EX_PI_HEAD_Cancel_Data.Total_Amt,0) as [Amount],TSPL_EX_PI_HEAD_Cancel_Data.Total_Tax_Amt  as [Total Tax Amt],TSPL_EX_PI_HEAD_Cancel_Data.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer],TSPL_EX_PI_HEAD_Cancel_Data.Created_By as [Created By],  " & _
            " convert(varchar,TSPL_EX_PI_HEAD_Cancel_Data.Created_Date,103) as [Created Date],'' as Description from TSPL_EX_PI_HEAD_Cancel_Data  " & _
            " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_EX_PI_HEAD_Cancel_Data.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code " & _
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_EX_PI_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code " & _
            " where isnull(TSPL_EX_PI_HEAD_Cancel_Data.document_type ,'')='EX' " & _
            " and convert(date,TSPL_EX_PI_HEAD_Cancel_Data.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_EX_PI_HEAD_Cancel_Data.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_EX_PI_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_EX_PI_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_EX_PI_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If

            qry += " ORDER BY TSPL_EX_PI_HEAD_Cancel_Data.Document_Date, TSPL_EX_PI_HEAD_Cancel_Data.Document_Code "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Export Commercial Invoice & Packing List") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing

            qry = " Select TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Document_Code  as[Document Id],convert(varchar,TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Document_Date,103) as [Document Date],  " & _
                " TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name], " & _
            " isnull(TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Total_Amt,0) as [Amount],TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Total_Tax_Amt  as [Total Tax Amt],TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer],TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Created_By as [Created By],  " & _
            " convert(varchar,TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Created_Date,103) as [Created Date],'' as Description from TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data  " & _
            " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code " & _
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code " & _
            " where isnull(TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.document_type ,'')='EX' " & _
            " and convert(date,TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If
            If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then

                qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            Else
                qry += " and TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            End If
            qry += " ORDER BY TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Document_Date, TSPL_EX_COMMERCIAL_INVOICE_HEAD_Cancel_Data.Document_Code "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Export Sales Invoice") = CompairStringResult.Equal Then
            gv1.DataSource = Nothing

            qry = " Select TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Code  as[Document Id],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Date,103) as [Document Date],  " & _
                " TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name], " & _
            " isnull(TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Total_Amt,0) as [Amount],TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Total_Tax_Amt  as [Total Tax Amt],TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer],TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Created_By as [Created By],  " & _
            " convert(varchar,TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Created_Date,103) as [Created Date],'' as Description from TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data  " & _
            " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code " & _
            " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code " & _
            " where isnull(TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.document_type ,'')='EX' " & _
            " and convert(date,TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                qry += " and TSPL_SD_SALE_INVOICE_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
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
            qry = " Select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical' and Location_Code in (" + arrLoc + ")"
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
        RadPageView1.SelectedPage = RadPageViewPage1
        GV2.DataSource = Nothing
    End Sub

    Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
            Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
            If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Export Sales Order") = CompairStringResult.Equal Then
                GV2.DataSource = Nothing

                qry = " SELECT TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Document_Code AS [Document Id],TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Location,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Item_Code ,TSPL_ITEM_MASTER.Item_Desc , " & _
                " TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Qty,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Unit_code ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Row_Type,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Scheme_Applicable ," & _
                " TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Scheme_Item ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Item_Cost ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Item_Net_Amt ,TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Amount," & _
                " TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Amt_Less_Discount FROM TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data " & _
                " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Location =TSPL_LOCATION_MASTER.Location_Code" & _
                " Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Item_Code " & _
                "  WHERE TSPL_SD_SALES_ORDER_DETAIL_Cancel_Data.Document_Code ='" & clsCommon.myCstr(grow.Cells("Document Id").Value) & "' "

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Export Proforma Invoice") = CompairStringResult.Equal Then
                GV2.DataSource = Nothing

                qry = " SELECT TSPL_EX_PI_DETAIL_Cancel_Data.Document_Code AS [Document Id],TSPL_EX_PI_DETAIL_Cancel_Data.Location,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_EX_PI_DETAIL_Cancel_Data.Item_Code ,TSPL_ITEM_MASTER.Item_Desc , " & _
                  " TSPL_EX_PI_DETAIL_Cancel_Data.Qty,TSPL_EX_PI_DETAIL_Cancel_Data.Unit_code ,TSPL_EX_PI_DETAIL_Cancel_Data.Row_Type,TSPL_EX_PI_DETAIL_Cancel_Data.Scheme_Applicable ," & _
                  " TSPL_EX_PI_DETAIL_Cancel_Data.Scheme_Item ,TSPL_EX_PI_DETAIL_Cancel_Data.Item_Cost ,TSPL_EX_PI_DETAIL_Cancel_Data.Item_Net_Amt ,TSPL_EX_PI_DETAIL_Cancel_Data.Amount," & _
                  " TSPL_EX_PI_DETAIL_Cancel_Data.Amt_Less_Discount FROM TSPL_EX_PI_DETAIL_Cancel_Data " & _
                  " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_EX_PI_DETAIL_Cancel_Data.Location =TSPL_LOCATION_MASTER.Location_Code" & _
                  " Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_EX_PI_DETAIL_Cancel_Data.Item_Code " & _
                  "  WHERE TSPL_EX_PI_DETAIL_Cancel_Data.Document_Code ='" & clsCommon.myCstr(grow.Cells("Document Id").Value) & "' "

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Export Commercial Invoice & Packing List") = CompairStringResult.Equal Then
                GV2.DataSource = Nothing

                qry = " SELECT TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Document_Code AS [Document Id],TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Location,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Item_Code ,TSPL_ITEM_MASTER.Item_Desc , " & _
                  " TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Qty,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Unit_code ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Row_Type,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Scheme_Applicable ," & _
                  " TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Scheme_Item ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Item_Cost ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Item_Net_Amt ,TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Amount," & _
                  " TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Amt_Less_Discount FROM TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data " & _
                  " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Location =TSPL_LOCATION_MASTER.Location_Code" & _
                  " Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Item_Code " & _
                  "  WHERE TSPL_EX_COMMERCIAL_INVOICE_DETAIL_Cancel_Data.Document_Code ='" & clsCommon.myCstr(grow.Cells("Document Id").Value) & "' "


            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Export Sales Invoice") = CompairStringResult.Equal Then
                GV2.DataSource = Nothing

                qry = " SELECT TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Document_Code AS [Document Id],TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Location,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Item_Code ,TSPL_ITEM_MASTER.Item_Desc , " & _
                    " TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Qty,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Unit_code ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Row_Type,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Scheme_Applicable ," & _
                    " TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Scheme_Item ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Item_Cost ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Item_Net_Amt ,TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Amount," & _
                    " TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data.Amt_Less_Discount FROM TSPL_SD_SALE_INVOICE_DETAIL_Cancel_Data " & _
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


End Class




