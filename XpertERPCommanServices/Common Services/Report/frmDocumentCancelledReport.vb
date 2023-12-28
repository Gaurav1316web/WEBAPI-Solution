''Created By-Sanjay----on- 31/Dec/2020---------------------
Imports common
Imports System.Data.SqlClient



Public Class frmDocumentCancelledReport
    Inherits FrmMainTranScreen
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
        'LOCATIONRIGTHS()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for Closing The Window")
        'LoadLocation()
        'chkLocAll.CheckState = CheckState.Checked
        'ChkUserAll.CheckState = CheckState.Checked
        'LoadUsers()
        'arrUser = GetSubbordinateUsers(objCommonVar.CurrentUserCode)
        SetUserMgmtNew()
        LoadBlankGrid()
        'LoadModuleType()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        LoadModuleTransaction()
        'If clsCommon.myLen(ModuleName) > 0 Then
        '    cboModule.SelectedValue = ModuleName
        '    cboTransaction.SelectedValue = Transaction
        '    dtpFromDate.Value = fromdate
        '    dtpToDate.Value = Todate
        '    ShowData()
        'End If
    End Sub
    Public Sub LoadModuleType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        dr = dt.NewRow()
        dr("Code") = "Dairy Sale"
        dr("Name") = "Dairy Sale"
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
    Sub LoadModuleTransaction()
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "Select"
        dr("Name") = "Select"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "AR Document"
        dr("Name") = "AR Document"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Dispatch"
        dr("Name") = "Dispatch"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Export Sale Invoice"
        dr("Name") = "Export Sale Invoice"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Sale Invoice"
        dr("Name") = "Sale Invoice"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Material Sale"
        dr("Name") = "Material Sale"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Transfer"
        dr("Name") = "Transfer"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "JobWork Billing"
        dr("Name") = "JobWork Billing"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Dairy Sale Return"
        dr("Name") = "Dairy Sale Return"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Export Sale Return"
        dr("Name") = "Export Sale Return"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Product Sale Return"
        dr("Name") = "Product Sale Return"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Material Sale Return"
        dr("Name") = "Material Sale Return"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Disposal Entry"
        dr("Name") = "Disposal Entry"
        dt1.Rows.Add(dr)

        cboTransaction.DataSource = dt1
        cboTransaction.DisplayMember = "Name"
        cboTransaction.ValueMember = "Code"
    End Sub

    Private Sub cboModule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboModule.SelectedIndexChanged
        LoadTrnsListOfSelectedModeule()
    End Sub

    Public Sub LoadTrnsListOfSelectedModeule()
        'If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Dairy Sale") = CompairStringResult.Equal Then
        '    cboTransaction.DataSource = Nothing
        '    LoadModuleProduction()
        'End If

    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.AllowAddNewRow = False

    End Sub
    Sub gv1Format()
        Me.gv1.MasterTemplate.Columns("Document Id").Width = 120      ''First Column
        Me.gv1.MasterTemplate.Columns("Document Date").Width = 150    ''Second Column
        Dim count As Integer = gv1.MasterTemplate.Columns.Count
        For i As Integer = 2 To count - 1
            Me.gv1.MasterTemplate.Columns(i).Width = 120
        Next i
        Me.gv1.MasterTemplate.Columns("Description").Width = 200    ''Last Column
        For j As Integer = 1 To count - 1
            Me.gv1.MasterTemplate.Columns(j).ReadOnly = True
        Next
        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

#Region "Showing Details on GRID"
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()

        If dtpFromDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow("'From date' Can't Be Greater Than 'To Date'")
            Exit Sub
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Select") = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow(Me, "Select Transaction Type", Me.Text)
            cboTransaction.Focus()
            Exit Sub
        End If
        qry = Nothing
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        ShowData()
    End Sub
    Function GetSubbordinateUsers(ByVal strUserCode As String) As ArrayList
        Dim arrUser As New ArrayList
        Try
            Dim qry As String
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
                qry = "Select User_Code from TSPL_User_MASTER Where 1=1"
            Else
                qry = "Select User_Code from TSPL_User_MASTER Where Level4_Code='" + strUserCode + "' OR User_Code='" + strUserCode + "'"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            For Each dr As DataRow In dt.Rows
                arrUser.Add(clsCommon.myCstr(dr("User_Code")))
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrUser
    End Function
    Sub ShowData()
        Try
            'If cbgUser.CheckedValue.Count > 0 Then
            '    arrSelectedUser = cbgUser.CheckedValue
            'Else
            '    arrSelectedUser = arrUser
            'End If

            'If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), "Dairy Sale") = CompairStringResult.Equal Then
            '    gv1.DataSource = Nothing
            FillData()
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub FillData()
        If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Dispatch") = CompairStringResult.Equal Then
            qry = "  Select TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_Code as [Document Id],convert(varchar,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_date ,103) as [Document Date]," &
                " TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Against_delivery_Code as [Against Delivery No] ,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Bill_To_Location as [Location Code]," &
                " TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Created_By as [Created By]," &
                " convert(varchar,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Created_Date,103) as [Created Date],'' as Description " &
                ",TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Cancel_By as [Cancelled By],convert(varchar,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Cancel_On,103) as [Cancelled Date]" &
                " from TSPL_SD_SHIPMENT_HEAD_Cancel_Data  " &
                " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code " &
            " WHERE  convert(date,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
            " and convert(date,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_date,103) <= convert(date,'" + dtpToDate.Value + "',103) "
            'and  TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Trans_Type IN ('FS', 'PS') and TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Screen_Type='DS'

            'If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
            '    qry += " and TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Bill_To_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If
            'If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
            '    qry += " and TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            'Else
            '    qry += " and TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            'End If
            qry += " ORDER BY TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_date,TSPL_SD_SHIPMENT_HEAD_Cancel_Data.Document_Code "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Sale Invoice") = CompairStringResult.Equal Then
            qry = "  Select TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_code  as [Document Id],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_date ,103) as [Document Date],  " &
                " TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Against_Shipment_No as [Against Shipment No],TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Bill_to_Location  as [Location Code], " &
                " TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Created_By as [Created By], " &
                " convert(varchar,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date],'' as Description " &
                ",TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date]" &
                " from TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA  " &
                " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Bill_to_Location  =TSPL_LOCATION_MASTER.Location_Code " &
                  " WHERE  convert(date,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
                  " and convert(date,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_date,103) <= convert(date,'" + dtpToDate.Value + "',103) "
            '" and TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Trans_Type IN ('FS','PS') and TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Screen_Type='DS' "


            'If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
            '    qry += " and TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Bill_to_Location  in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            'End If

            'If chkUserSelect.IsChecked AndAlso cbgUser.CheckedValue.Count > 0 Then
            '    qry += " and TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(cbgUser.CheckedValue) + ")"
            'Else
            '    qry += " and TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Created_By IN (" + clsCommon.GetMulcallString(arrSelectedUser) + ")"
            'End If

            qry += "  ORDER BY TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_date,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_code "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Export Sale Invoice") = CompairStringResult.Equal Then
            qry = "  Select TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_code  as [Document Id],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_date ,103) as [Document Date],  " &
                " TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Against_Shipment_No as [Against Shipment No],TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Bill_to_Location  as [Location Code], " &
                " TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Created_By as [Created By], " &
                " convert(varchar,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date],'' as Description,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA  " &
                " Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Bill_to_Location  =TSPL_LOCATION_MASTER.Location_Code " &
                " WHERE  convert(date,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103) " &
                " and convert(date,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_date,103) <= convert(date,'" + dtpToDate.Value + "',103) " &
                " and TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.document_type='EX' and TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.trans_type='EXP' " &
                " ORDER BY TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_date,TSPL_SD_SALE_INVOICE_HEAD_CANCEL_DATA.Document_code "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "AR Document") = CompairStringResult.Equal Then
            qry = "Select TSPL_Customer_Invoice_Head_CANCEL_DATA.Document_No  as [Document Id],convert(varchar,TSPL_Customer_Invoice_Head_CANCEL_DATA.Document_Date ,103) as [Document Date] " &
                  ",TSPL_Customer_Invoice_Head_CANCEL_DATA.Customer_Code as [Customer Code],TSPL_Customer_Invoice_Head_CANCEL_DATA.Customer_Name as [Customer Name] " &
                  ", TSPL_Customer_Invoice_Head_CANCEL_DATA.Document_Type as [Document Type] " &
                  ", TSPL_Customer_Invoice_Head_CANCEL_DATA.Document_Total as [Document Amount] " &
                  ",TSPL_Customer_Invoice_Head_CANCEL_DATA.Created_By as [Created By] " &
                  ", convert(varchar,TSPL_Customer_Invoice_Head_CANCEL_DATA.Created_Date,103) as [Created Date], Description,TSPL_Customer_Invoice_Head_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_Customer_Invoice_Head_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_Customer_Invoice_Head_CANCEL_DATA " &
                  " WHERE  convert(date,TSPL_Customer_Invoice_Head_CANCEL_DATA.Document_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103)  and convert(date,TSPL_Customer_Invoice_Head_CANCEL_DATA.Document_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) " &
                  " ORDER BY TSPL_Customer_Invoice_Head_CANCEL_DATA.Document_Date,TSPL_Customer_Invoice_Head_CANCEL_DATA.Document_No "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Material Sale") = CompairStringResult.Equal Then
            qry = "Select TSPL_SCRAPINVOICE_HEAD_CANCEL_DATA.invoice_No  as [Document Id],convert(varchar,TSPL_SCRAPINVOICE_HEAD_CANCEL_DATA.shipment_Date ,103) as [Document Date] " &
                  " ,TSPL_SCRAPINVOICE_HEAD_CANCEL_DATA.ToLoc_Code as [To Location Code] " &
                  " ,To_Location.Location_Desc as [To Location Name],TSPL_SCRAPINVOICE_HEAD_CANCEL_DATA.Created_By as [Created By] " &
                  " ,convert(varchar,TSPL_SCRAPINVOICE_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date], Description,TSPL_SCRAPINVOICE_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_SCRAPINVOICE_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_SCRAPINVOICE_HEAD_CANCEL_DATA   " &
                  " Left Outer Join TSPL_LOCATION_MASTER To_Location on TSPL_SCRAPINVOICE_HEAD_CANCEL_DATA.ToLoc_Code  =To_Location.Location_Code " &
                  " WHERE  Convert(Date, TSPL_SCRAPINVOICE_HEAD_CANCEL_DATA.shipment_Date,103) >= Convert(Date,'" + dtpFromDate.Value + "',103)  and convert(date,TSPL_SCRAPINVOICE_HEAD_CANCEL_DATA.shipment_Date,103) <= convert(date,'" + dtpToDate.Value + "',103)  " &
                  " ORDER BY TSPL_SCRAPINVOICE_HEAD_CANCEL_DATA.shipment_Date, TSPL_SCRAPINVOICE_HEAD_CANCEL_DATA.invoice_No "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Transfer") = CompairStringResult.Equal Then
            qry = "Select TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.Document_No  As [Document Id], Convert(varchar, TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.Document_date, 103) As [Document Date] " &
                  ", TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.From_Location As [From Location Code], From_Location.Location_Desc As [From Location Name] " &
                  ", TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.To_Location  As [To Location Code], To_Location.Location_Desc As [To Location Name], TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.Created_By As [Created By], Convert(varchar, TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.Created_Date, 103) As [Created Date], Description,TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA   " &
                  " Left Outer Join TSPL_LOCATION_MASTER From_Location On TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.From_Location  =From_Location.Location_Code " &
                  " Left Outer Join TSPL_LOCATION_MASTER To_Location On TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.To_Location  =To_Location.Location_Code " &
                  " WHERE  Convert(Date, TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.Document_date, 103) >= Convert( Date,'" + dtpFromDate.Value + "',103)  and convert(date,TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.Document_date,103) <= convert(date,'" + dtpToDate.Value + "',103)   " &
                  " ORDER BY TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.Document_date, TSPL_TRANSFER_ORDER_HEAD_CANCEL_DATA.Document_No "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "JobWork Billing") = CompairStringResult.Equal Then
            qry = "Select TSPL_JOBWORK_BILLING_HEAD_CANCEL_DATA.Document_Code as [Document Id],convert(varchar,TSPL_JOBWORK_BILLING_HEAD_CANCEL_DATA.Document_date ,103) as [Document Date],TSPL_JOBWORK_BILLING_HEAD_CANCEL_DATA.Loc_Code as [Location Code], TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_JOBWORK_BILLING_HEAD_CANCEL_DATA.Created_By as [Created By], convert(varchar,TSPL_JOBWORK_BILLING_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date],Reff as Reference, Description " &
                  ",TSPL_JOBWORK_BILLING_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_JOBWORK_BILLING_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] " &
                  " From TSPL_JOBWORK_BILLING_HEAD_CANCEL_DATA   Left Outer Join TSPL_LOCATION_MASTER  On TSPL_JOBWORK_BILLING_HEAD_CANCEL_DATA.Loc_Code  =TSPL_LOCATION_MASTER.Location_Code " &
                  " WHERE  convert(date,TSPL_JOBWORK_BILLING_HEAD_CANCEL_DATA.Document_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103)  and convert(date,TSPL_JOBWORK_BILLING_HEAD_CANCEL_DATA.Document_date,103) <= convert(date,'" + dtpToDate.Value + "',103) " &
                  " Order BY TSPL_JOBWORK_BILLING_HEAD_CANCEL_DATA.Document_date,TSPL_JOBWORK_BILLING_HEAD_CANCEL_DATA.Document_Code "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Dairy Sale Return") = CompairStringResult.Equal Then
            qry = "Select TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_Code as [Document Id],convert(varchar,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_date ,103) as  " &
                  " [Document Date],TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Against_Invoice_No as [Against Invoice No] " &
                  " ,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Bill_To_Location as [Bill To Location Code], TSPL_LOCATION_MASTER.Location_Desc as [Bill To Location Name] " &
                  " ,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.SHIP_TO_LOCATION as [Ship To Location Code], SHIP_LOC.Location_Desc as [Ship To Location Name] " &
                  " ,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Created_By as [Created By], convert(varchar,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date] " &
                  " ,Remarks,Description,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA   Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code  " &
                  " Left Outer Join TSPL_LOCATION_MASTER SHIP_LOC on TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.SHIP_TO_LOCATION =SHIP_LOC.Location_Code  " &
                  " WHERE ISNULL(Screen_Type,'') ='DS' and convert(date,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103)  and convert(date,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_date,103) <= convert(date,'" + dtpToDate.Value + "',103)   " &
                  " ORDER BY TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_date, TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_Code "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Product Sale Return") = CompairStringResult.Equal Then
            qry = "Select TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_Code as [Document Id],convert(varchar,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_date ,103) as  " &
                 " [Document Date],TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Against_Invoice_No as [Against Invoice No] " &
                 " ,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Bill_To_Location as [Bill To Location Code], TSPL_LOCATION_MASTER.Location_Desc as [Bill To Location Name] " &
                 " ,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.SHIP_TO_LOCATION as [Ship To Location Code], SHIP_LOC.Location_Desc as [Ship To Location Name] " &
                 " ,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Created_By as [Created By], convert(varchar,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date] " &
                 " ,Remarks,Description,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA   Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code  " &
                 " Left Outer Join TSPL_LOCATION_MASTER SHIP_LOC on TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.SHIP_TO_LOCATION =SHIP_LOC.Location_Code  " &
                 " WHERE ISNULL(Screen_Type,'')<>'DS' and ISNULL(Trans_Type,'')='PS' and convert(date,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103)  and convert(date,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_date,103) <= convert(date,'" + dtpToDate.Value + "',103)   " &
                 " ORDER BY TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_date, TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_Code "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Material Sale Return") = CompairStringResult.Equal Then
            qry = "  Select TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.Document_No as [Document Id],convert(varchar,TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.Return_ship_Date ,103) as [Document Date] " &
                  " , TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.shipment_No as [Against Shipment No] ,TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.invoice_No as [Against Invoice No]  " &
                  " ,TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.Loc_Code as [Location Code], TSPL_LOCATION_MASTER.Location_Desc as [Location Name] " &
                   " ,TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.ToLoc_Code as [To Location Code], TOLOC.Location_Desc as [To Location Name] " &
                  " ,TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.Created_By as [Created By], convert(varchar,TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.Created_Date,103) as [Created Date],'' as Description,TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.Cancel_On,103) as [Cancelled Date]  " &
                  " From TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA   Left Outer Join TSPL_LOCATION_MASTER  On TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.Loc_Code  =TSPL_LOCATION_MASTER.Location_Code  " &
                  " Left Outer Join TSPL_LOCATION_MASTER TOLOC on TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.ToLoc_Code  =TOLOC.Location_Code " &
                  " Where  convert(date,TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.Return_ship_Date ,103) >= convert(date,'" + dtpFromDate.Value + "',103)  and convert(date,TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.Return_ship_Date,103) <= convert(date,'" + dtpToDate.Value + "',103) " &
                  " ORDER BY TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.Return_ship_Date,TSPL_SCRAPSALE_HEAD_RETURN_CANCEL_DATA.Document_No "

        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Export Sale Return") = CompairStringResult.Equal Then
            qry = "Select TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_Code as [Document Id],convert(varchar,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_date ,103) as  " &
                 " [Document Date],TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Against_Invoice_No as [Against Invoice No] " &
                 " ,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Bill_To_Location as [Bill To Location Code], TSPL_LOCATION_MASTER.Location_Desc as [Bill To Location Name] " &
                 " ,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.SHIP_TO_LOCATION as [Ship To Location Code], SHIP_LOC.Location_Desc as [Ship To Location Name] " &
                 " ,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Created_By as [Created By], convert(varchar,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date] " &
                 " ,Remarks,Description,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date] from TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA   Left Outer Join TSPL_LOCATION_MASTER  on TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Bill_To_Location  =TSPL_LOCATION_MASTER.Location_Code  " &
                 " Left Outer Join TSPL_LOCATION_MASTER SHIP_LOC on TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.SHIP_TO_LOCATION =SHIP_LOC.Location_Code  " &
                 " WHERE TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.trans_type='EXP' and TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.document_type='EX' and convert(date,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_date ,103) >= convert(date,'" + dtpFromDate.Value + "',103)  and convert(date,TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_date,103) <= convert(date,'" + dtpToDate.Value + "',103)   " &
                 " ORDER BY TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_date, TSPL_SD_SALE_RETURN_HEAD_CANCEL_DATA.Document_Code "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "Disposal Entry") = CompairStringResult.Equal Then
            qry = "Select TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA.Document_No as [Document Id] " &
                 ",convert(varchar,TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA.Document_date ,103) as [Document Date],case when isnull(TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA.Against_Scrap,0)=1 then 'Yes' else 'No' end as [Against Scrap]  " &
                 ",TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA.Loc_Code as [Location Code], TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA.Created_By as [Created By] " &
                 ", convert(varchar,TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA.Created_Date,103) as [Created Date] ,Description,TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA.Cancel_By as [Cancelled By],convert(varchar,TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA.Cancel_On,103) as [Cancelled Date]  " &
                 " From TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA   Left Outer Join TSPL_LOCATION_MASTER  On TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA.Loc_Code =TSPL_LOCATION_MASTER.Location_Code " &
                 " Where Convert(Date, TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA.Document_date,103) >= Convert(Date,'" + dtpFromDate.Value + "',103)  and convert(date,TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA.Document_date,103) <= convert(date,'" + dtpToDate.Value + "',103)   " &
                 " ORDER BY TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA.Document_date, TSPL_ASSET_SCRAP_HEAD_CANCEL_DATA.Document_No "
        End If
        If clsCommon.CompairString(clsCommon.myCstr(qry), Nothing) <> CompairStringResult.Equal Then

            dt = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count <= 0 Then
                lblNoOfRecords.Text = "No Record Found"
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            Else
                gv1.DataSource = dt
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1Format()
                strNoOfRecord = clsCommon.myCstr(dt.Rows.Count)
                lblNoOfRecords.Text = "" + strNoOfRecord + " Records Found"
            End If
        End If
        '-------------------------------------Code Ends Here--------------------------------------------
    End Sub

#End Region


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm()
    End Sub


    Sub closeForm()
        Me.Close()
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
            qry = " Select Location_Code As Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical' and Location_Code in (" + arrLoc + ")"
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

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        'chkLocAll.CheckState = CheckState.Checked
        'ChkUserAll.CheckState = CheckState.Checked
        lblNoOfRecords.Text = ""
        cboTransaction.SelectedValue = "Select"
        gv1.DataSource = Nothing
    End Sub

    Private Sub btn_Excel_Click(sender As Object, e As EventArgs) Handles btn_Excel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Document Cancelled Report")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range : " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Transaction : " & clsCommon.myCstr(cboTransaction.SelectedValue))

            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btn_PDF_Click(sender As Object, e As EventArgs) Handles btn_PDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Document Cancelled Report")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range : " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Transaction : " & clsCommon.myCstr(cboTransaction.SelectedValue))

            clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class




