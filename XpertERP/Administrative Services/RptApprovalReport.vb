'' New Report done against ticket no.UDL/18/06/18-000190
Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class RptApprovalReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing
    Dim qry As String
    Dim isInsideLoadData As Boolean = False
    Public arrLoc As Dictionary(Of String, Object) = Nothing
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport

    End Sub

    Private Sub RptApprovalReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        gv.AllowEditRow = False
        gv.AllowDragToGroup = False
        gv.AllowAddNewRow = False
        LoadModuleType()
        LoadCategory()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
        cboModule.SelectedIndex = 0
        cboTransaction.SelectedIndex = 0
        txtLocation.Value = ""
        ddl_category.SelectedIndex = 0

    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Private Sub Load_Report()
        'Ticket No-UDL/18/10/18-000233   sum(xxx.[Document Amount]) -> max(xxx.[Document Amount])
        Try
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If

            ' Dim StrQuery As String = " select distinct TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.No_of_level as [Level],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code as [Document No],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.amount as [Document Amount],tspl_user_master.user_name as [Approving Authority],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status as Status,case when isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status,'')='' then '' else cast(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.modified_date as varchar(12)) end as [Date of Approval]"
            'StrQuery += " ,tspl_purchase_order_head.category as [Type],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.remarks as [Remarks] from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL left outer join tspl_user_master on tspl_user_master.user_code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.user_code left outer join TSPL_APPROVAL_LEVEL_SCREEN on TSPL_APPROVAL_LEVEL_SCREEN.trans_code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.trans_code left outer join tspl_purchase_order_head on tspl_purchase_order_head.PurchaseOrder_No=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code "
            'StrQuery += " where 2=2 and is_reverse=0 and Convert(date,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date,103)>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and Convert(date,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
            Dim StrQuery As String = "select xxx.[Document No],max(xxx.[Document Date]) as [Document Date] ,max(xxx.[Vendor Code]) as [Vendor Code],max(xxx.[Vendor Name]) as [Vendor Name] ,max(xxx.[Date of Approval]) as [Date of Approval],max(xxx.[Document Amount]) as [Document Amount],max(xxx.[Approving Authority]) as [Approving Authority],max(xxx.Status) as Status,max(xxx.Type) as Type,max(xxx.Remarks) as Remarks from ("
            StrQuery += " select distinct TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.No_of_level as [Level],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code as [Document No],CONVERT(VARCHAR,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date ,103) AS [Document Date],tspl_purchase_order_head.Vendor_Code AS [Vendor Code],tspl_purchase_order_head.Vendor_Name as [Vendor Name],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.amount as [Document Amount],tspl_user_master.user_name as [Approving Authority],case when isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status,'Approved')='Approved' then 'Approved' end as Status,case when isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status,'')='' then '' else convert(varchar,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.modified_date,103) end as [Date of Approval] ,TSPL_APPROVAL_LEVEL_SCREEN.Capex_Category as [Type],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.remarks as [Remarks] from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL left outer join tspl_user_master on tspl_user_master.user_code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.user_code left outer join TSPL_APPROVAL_LEVEL_SCREEN on TSPL_APPROVAL_LEVEL_SCREEN.trans_code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.trans_code left outer join tspl_purchase_order_head on tspl_purchase_order_head.PurchaseOrder_No=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code  "
            StrQuery += " where 2=2 and is_reverse=0 and Convert(date,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date,103)>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and Convert(date,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

            If clsCommon.myLen(cboTransaction.SelectedValue) > 0 Then
                StrQuery += " and TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.trans_code='" & cboTransaction.SelectedValue & "'"
            End If
            'If clsCommon.myLen(txtLocation.Value) > 0 Then
            '    StrQuery += " and TSPL_APPROVAL_LEVEL_SCREEN.Loc_Code='" & txtLocation.Value & "'"
            'End If
            If clsCommon.CompairString(ddl_category.SelectedValue, "Capex") = CompairStringResult.Equal Then
                StrQuery += " and TSPL_APPROVAL_LEVEL_SCREEN.Capex_Category='" & ddl_category.SelectedValue & "'"
            ElseIf clsCommon.CompairString(ddl_category.SelectedValue, "Regular") = CompairStringResult.Equal Then
                StrQuery += " and TSPL_APPROVAL_LEVEL_SCREEN.Capex_Category='" & ddl_category.SelectedValue & "'"
            End If
            StrQuery += " and (TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.status='Approved' or (TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.status='Approved' and Is_Posted=1)) and is_reverse=0 "
            StrQuery += " union all"
            StrQuery += " select distinct TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.No_of_level as [Level],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code as [Document No],CONVERT(VARCHAR,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date ,103) AS [Document Date],tspl_purchase_order_head.Vendor_Code AS [Vendor Code],tspl_purchase_order_head.Vendor_Name as [Vendor Name],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.amount as [Document Amount],tspl_user_master.user_name as [Approving Authority],case when isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status,'Rejected')='Rejected' then 'Rejected' end as Status,case when isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status,'')='' then '' else convert(varchar,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.modified_date,103) end as [Date of Approval] ,TSPL_APPROVAL_LEVEL_SCREEN.Capex_Category as [Type],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.remarks as [Remarks] from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL left outer join tspl_user_master on tspl_user_master.user_code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.user_code left outer join TSPL_APPROVAL_LEVEL_SCREEN on TSPL_APPROVAL_LEVEL_SCREEN.trans_code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.trans_code left outer join tspl_purchase_order_head on tspl_purchase_order_head.PurchaseOrder_No=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code  "
            StrQuery += " where 2=2 and is_reverse=0 and Convert(date,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date,103)>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and Convert(date,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

            If clsCommon.myLen(cboTransaction.SelectedValue) > 0 Then
                StrQuery += " and TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.trans_code='" & cboTransaction.SelectedValue & "'"
            End If
            'If clsCommon.myLen(txtLocation.Value) > 0 Then
            '    StrQuery += " and TSPL_APPROVAL_LEVEL_SCREEN.Loc_Code='" & txtLocation.Value & "'"
            'End If
            If clsCommon.CompairString(ddl_category.SelectedValue, "Capex") = CompairStringResult.Equal Then
                StrQuery += " and TSPL_APPROVAL_LEVEL_SCREEN.Capex_Category='" & ddl_category.SelectedValue & "'"
            ElseIf clsCommon.CompairString(ddl_category.SelectedValue, "Regular") = CompairStringResult.Equal Then
                StrQuery += " and TSPL_APPROVAL_LEVEL_SCREEN.Capex_Category='" & ddl_category.SelectedValue & "'"
            End If
            StrQuery += " and (TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.status='Rejected' or (TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.status='Rejected' and Is_Posted=1)) and is_reverse=0 "
            StrQuery += " Union All"
            StrQuery += " select distinct TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.No_of_level as [Level],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code as [Document No],CONVERT(VARCHAR,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date ,103) AS [Document Date],tspl_purchase_order_head.Vendor_Code AS [Vendor Code],tspl_purchase_order_head.Vendor_Name as [Vendor Name],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.amount as [Document Amount],tspl_user_master.user_name as [Approving Authority],case when isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status,'Amend')='Amend' then 'Pending' end as Status,case when isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status,'')='' then '' else convert(varchar,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.modified_date,103) end as [Date of Approval] ,TSPL_APPROVAL_LEVEL_SCREEN.Capex_Category as [Type],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.remarks as [Remarks] from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL left outer join tspl_user_master on tspl_user_master.user_code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.user_code left outer join TSPL_APPROVAL_LEVEL_SCREEN on TSPL_APPROVAL_LEVEL_SCREEN.trans_code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.trans_code left outer join tspl_purchase_order_head on tspl_purchase_order_head.PurchaseOrder_No=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code  "
            StrQuery += " where 2=2 and is_reverse=0 and Convert(date,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date,103)>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and Convert(date,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

            If clsCommon.myLen(cboTransaction.SelectedValue) > 0 Then
                StrQuery += " and TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.trans_code='" & cboTransaction.SelectedValue & "'"
            End If
            'If clsCommon.myLen(txtLocation.Value) > 0 Then
            '    StrQuery += " and TSPL_APPROVAL_LEVEL_SCREEN.Loc_Code='" & txtLocation.Value & "'"
            'End If
            If clsCommon.CompairString(ddl_category.SelectedValue, "Capex") = CompairStringResult.Equal Then
                StrQuery += " and TSPL_APPROVAL_LEVEL_SCREEN.Capex_Category='" & ddl_category.SelectedValue & "'"
            ElseIf clsCommon.CompairString(ddl_category.SelectedValue, "Regular") = CompairStringResult.Equal Then
                StrQuery += " and TSPL_APPROVAL_LEVEL_SCREEN.Capex_Category='" & ddl_category.SelectedValue & "'"
            End If
            StrQuery += " and (TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.status='Amend' or (TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.status='Amend' and Is_Posted=1)) and is_reverse=0 "
            StrQuery += " and 2=(case when isnull(All_Level_Approval,0)=0 or tspl_approval_level_transaction_detail.No_Of_Level=1 then 2 else "
            StrQuery += " (case when exists(select 1 from tspl_approval_level_transaction_detail as inn where inn.TRANS_Code=tspl_approval_level_transaction_detail.TRANS_Code and inn.Document_Code=tspl_approval_level_transaction_detail.Document_Code and inn.Comp_Code=tspl_approval_level_transaction_detail.Comp_Code and inn.No_Of_Level=tspl_approval_level_transaction_detail.No_Of_Level-1  and inn.Status='Approved' ) then 2 else 3 end ) end) "
            StrQuery += " Union All"
            StrQuery += " select distinct TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.No_of_level as [Level],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code as [Document No],CONVERT(VARCHAR,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date,103) AS [Document Date],tspl_purchase_order_head.Vendor_Code AS [Vendor Code],tspl_purchase_order_head.Vendor_Name as [Vendor Name],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.amount as [Document Amount],tspl_user_master.user_name as [Approving Authority],case when isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status,'')='' then 'Pending' end as Status,case when isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Status,'')='' then '' else convert(varchar,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.modified_date,103) end as [Date of Approval] ,TSPL_APPROVAL_LEVEL_SCREEN.Capex_Category as [Type],TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.remarks as [Remarks] from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL left outer join tspl_user_master on tspl_user_master.user_code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.user_code left outer join TSPL_APPROVAL_LEVEL_SCREEN on TSPL_APPROVAL_LEVEL_SCREEN.trans_code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.trans_code left outer join tspl_purchase_order_head on tspl_purchase_order_head.PurchaseOrder_No=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Code  "
            StrQuery += " where 2=2 and is_reverse=0 and Convert(date,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date,103)>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and Convert(date,TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

            If clsCommon.myLen(cboTransaction.SelectedValue) > 0 Then
                StrQuery += " and TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.trans_code='" & cboTransaction.SelectedValue & "'"
            End If
            'If clsCommon.myLen(txtLocation.Value) > 0 Then
            '    StrQuery += " and TSPL_APPROVAL_LEVEL_SCREEN.Loc_Code='" & txtLocation.Value & "'"
            'End If
            If clsCommon.CompairString(ddl_category.SelectedValue, "Capex") = CompairStringResult.Equal Then
                StrQuery += " and TSPL_APPROVAL_LEVEL_SCREEN.Capex_Category='" & ddl_category.SelectedValue & "'"
            ElseIf clsCommon.CompairString(ddl_category.SelectedValue, "Regular") = CompairStringResult.Equal Then
                StrQuery += " and TSPL_APPROVAL_LEVEL_SCREEN.Capex_Category='" & ddl_category.SelectedValue & "'"
            End If
            StrQuery += " and ((isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.status,'')='' or isnull(TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.status,'')='Pending') and isnull(Is_Posted,'')=0) and is_reverse=0 "
            StrQuery += " and 2=(case when isnull(All_Level_Approval,0)=0 or tspl_approval_level_transaction_detail.No_Of_Level=1 then 2 else "
            StrQuery += " (case when exists(select 1 from tspl_approval_level_transaction_detail as inn where inn.TRANS_Code=tspl_approval_level_transaction_detail.TRANS_Code and inn.Document_Code=tspl_approval_level_transaction_detail.Document_Code and inn.Comp_Code=tspl_approval_level_transaction_detail.Comp_Code and inn.No_Of_Level=tspl_approval_level_transaction_detail.No_Of_Level-1  and inn.Status='Approved' ) then 2 else 3 end ) end) "
            StrQuery += "  and   not exists(select 1 from tspl_approval_level_transaction_detail as inn where inn.TRANS_Code=tspl_approval_level_transaction_detail.TRANS_Code and inn.Document_Code=tspl_approval_level_transaction_detail.Document_Code and inn.Comp_Code=tspl_approval_level_transaction_detail.Comp_Code and inn.No_Of_Level=tspl_approval_level_transaction_detail.No_Of_Level and inn.Status<>'') "
            StrQuery += " ) as xxx"
            StrQuery += " group by xxx.[Document No],Level "
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(StrQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                'gv.Columns("Level").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                gv.TableElement.TableHeaderHeight = 40
                gv.MasterTemplate.ShowRowHeaderColumn = False
                gv.EnableFiltering = True
                gv.ShowFilteringRow = True
                gv.BestFitColumns()
                ReStoreGridLayout()
            Else
                gv.DataSource = Nothing
                clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
                RadPageView1.SelectedPage = RadPageViewPage1
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RptPendingDocumentList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub



    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptApprovalReport & "'"))

            If clsCommon.myLen(cboModule.SelectedValue) > 0 Then
                arrHeader.Add("Module : " + cboModule.Text)
            End If
            If clsCommon.myLen(cboTransaction.SelectedValue) > 0 Then
                arrHeader.Add("Transaction : " + cboTransaction.Text)
            End If
            If clsCommon.myLen(ddl_category.SelectedValue) > 0 Then
                arrHeader.Add("Category : " + ddl_category.Text)
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
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Document Approval Status", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ExportToExcel_Click(sender As Object, e As EventArgs) Handles ExportToExcel.Click
        'If gv.DataSource Is Nothing Then
        '    Load_Report()
        'End If

        'Dim arrHeader As List(Of String) = New List(Of String)()
        'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
        'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptApprovalReport & "'"))
        'If clsCommon.myLen(cboModule.SelectedValue) > 0 Then
        '    arrHeader.Add("Module : " + cboModule.SelectedValue)
        'End If
        'clsCommon.MyExportToExcelGrid("Document Approval Status", gv, Nothing, Me.Text)
        ExportGrid(EnumExportTo.Excel)
    End Sub
    Public Sub LoadModuleType()
        isInsideLoadData = True
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleSalesNew
        dr("Name") = "Standard Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleProductSale
        dr("Name") = "Product Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleSaleDairy
        dr("Name") = "Dairy Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleCSASale
        dr("Name") = "CSA Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleFreshSale
        dr("Name") = "Fresh Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleBulkSale
        dr("Name") = "Bulk Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleMerchantTradeSale
        dr("Name") = "Merchant Trade Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleMCCMilkProcurement
        dr("Name") = "MCC Milk Procurement"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleBulkMilkProcurement
        dr("Name") = "Bulk Milk Procurement"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModulePurchase
        dr("Name") = "Purchase"
        dt.Rows.Add(dr)

     


        cboModule.DataSource = dt
        cboModule.DisplayMember = "Name"
        cboModule.ValueMember = "Code"
        isInsideLoadData = False
    End Sub
  
    Private Sub cboModule_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboModule.SelectedIndexChanged
        If Not isInsideLoadData Then
            LoadTrnsListOfSelectedModeule()
        End If
    End Sub

    Public Sub LoadTrnsListOfSelectedModeule()
        Dim dt As New DataTable
        Try
            Dim qry As String
            Dim whrCls As String = " and Program_Code in ('PO-ODR','PO-REQ','PO-INV','SALN-SO','SALE-ORDER','SALN-SP','DEL-NOTE-FS','DEL-ORD-PS','DISPATCH-BS','M-Material','VSP-Item','SHIPMENT-PS','CSA-INV-TRN','M-SRN-B','M-RECEIPT','M-QC','LC-CREATION','BOOK-DS','CSA-DO-TRN','SALE-RET-PS','RETRUN-DS')"



            If Not clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), clsUserMgtCode.ModuleSaleDairy) = CompairStringResult.Equal Then ''std. sales
                If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), clsUserMgtCode.ModuleBulkMilkProcurement) = CompairStringResult.Equal Then
                    qry = "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Bulk Transaction')) " + whrCls + " "

                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), clsUserMgtCode.ModuleMCCMilkProcurement) = CompairStringResult.Equal Then
                    qry = "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('MCC Transaction')) " + whrCls + " "
                Else
                    qry = "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                            "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Transaction')) " + whrCls + " "
                End If
            Else
                qry = "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                    "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Fresh Sale')) " + whrCls + "  " & _
                "union all " & _
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Bulk Sale')) " + whrCls + " " & _
                "union all " & _
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Product Sale','Product postdaturn')) " + whrCls + " " & _
                "union all " & _
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Export Sale')) " + whrCls + " " & _
                "union all " & _
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('CSA Sale')) " + whrCls + " " & _
                "union all " & _
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Transaction')) " + whrCls + " "
            End If
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)

            cboTransaction.DataSource = dt
            cboTransaction.DisplayMember = "Name"
            cboTransaction.ValueMember = "Code"

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            dt = Nothing
        End Try
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        txtLocation.Value = clsCommon.ShowSelectForm("BILLTOxxCPO", qry, "Code", "", txtLocation.Value, "Code", isButtonClicked)
    End Sub
    Sub LoadCategory()

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Both"
        dr("Name") = "Both"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Capex"
        dr("Name") = "Capex"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Regular"
        dr("Name") = "Regular"
        dt.Rows.Add(dr)

        ddl_category.DataSource = dt
        ddl_category.ValueMember = "Code"
        ddl_category.DisplayMember = "Name"
        ddl_category.SelectedValue = ""
    End Sub

    Private Sub ExportToPDF_Click(sender As Object, e As EventArgs) Handles ExportToPDF.Click
        ExportGrid(EnumExportTo.PDF)
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
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
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
