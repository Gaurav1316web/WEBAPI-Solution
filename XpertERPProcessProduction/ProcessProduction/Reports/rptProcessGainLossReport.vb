Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

' Ticket No : BHA/07/12/18-000745 By Prabhakar Create New Report 

Public Class rptProcessGainLossReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim strCustomerCode As String = Nothing
    Dim strLocationCode As String = Nothing
    Dim strItemCode As String = Nothing
    'Dim isBackOn As Boolean = False

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        Reset()
    End Sub
    Sub Reset()

        'ToDate.Value = clsCommon.GETSERVERDATE()
        'fromDate.Value = ToDate.Value.AddMonths(-1)

        rdbSummary.Checked = True

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        strCustomerCode = ""
        strItemCode = ""
        strLocationCode = ""
        ' isBackOn = False
        btnGo.Enabled = True
        'btnExport.Enabled = True
        'btnBack.Enabled = False

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            GetReportGridID()

            PageSetupReport_ID = MyBase.Form_ID + IIf(rdbSummary.Checked = True, "S", "D")
            TemplateGridview = Gv1
            Dim BaseQry As String = ""
            Dim qry As String = ""
            Dim dt As New DataTable
            Dim whre As String = ""


            BaseQry = "  select TSPL_INVENTORY_SOURCE_CODE.Name as [Trans Name],InOut,Location_Code as [Location Code],Item_Code as [Item Code],Item_Desc as [Item Desc],Qty AS Qty,UOM , " &
                      "  Source_Doc_No as [Source Doc No],convert(date,Source_Doc_Date,103) as [Source Doc Date],Entry_Date as [Entry Date] ,  " &
                      "  cast(case when Basic_Cost=0 then ((case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end)/ (case when Qty=0 then 1 else Qty end) ) else  (case when InOut='O' THEN (-1 * Basic_Cost) ELSE Basic_Cost end) end as  decimal(16,2)) as [Basic Cost]  , " &
                      "  cast((case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end)/ (case when Stock_Qty=0 then 1 else Stock_Qty end) as  decimal(16,2)) as [Stock Cost],  " &
                      "  tspl_inventory_movement.Created_By as [Created By],tspl_inventory_movement.Comp_Code as [Comp Code],ItemType as [Item Type], " &
                      "  convert(date,Punching_Date,103) as [Punching Date],MRP,Batch_No as [Batch No] ,MFG_Date as [MFG Date],Expiry_Date as [Expiry Date],  " &
                      "  (case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end) AS [Avg Cost],Posting_Date as [Posting Date],  " &
                      "  Stock_UOM as [Stock UOM],(case when InOut='O' THEN (-1 * Stock_Qty) ELSE Stock_Qty end) as [Stock Qty],Item_Status as [Item Status],Assmbly_Status as [Assmbly Status],Fat_Per as [Fat %],SNF_Per as [SNF %],(case when InOut='O' THEN (-1 * Fat_KG) ELSE Fat_KG end) as[Fat KG],  " &
                      "  (case when InOut='O' THEN (-1 * SNF_KG) ELSE SNF_KG end) as [SNF KG] ,'' as [Main Location],  " &
                      "  IS_CONSUMPTION,Cust_Code as [Customer Code] ,Cust_Name as [Customer Name],Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Other_Location_Code as [Other Location Code],Other_Location_Desc as [Other Location Desc],convert(decimal(18,2),Fat_Rate) as [Fat Rate],convert(decimal(18,2),SNF_Rate) as [SNF Rate],(case when InOut='O' THEN (-1 * Fat_Amt) ELSE Fat_Amt end) as [Fat Amt],(case when InOut='O' THEN (-1 * SNF_Amt) ELSE SNF_Amt end) as [SNF Amt], " &
                      "  0 as [Standard Qty],0 as SYNC_STATUS  " &
                      "  from tspl_inventory_movement  left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=tspl_inventory_movement.Trans_Type " &
                      "  where Trans_Type in ('PROD_ENTRY','Disassembly') and convert(date,Source_Doc_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,Source_Doc_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "'  " &
                      "  union all " &
                      "  select TSPL_INVENTORY_SOURCE_CODE.Name as [Trans Name],tspl_inventory_movement_new.InOut,Location_Code as [Location Code] ,Item_Code as [Item Code],Item_Desc as [Item Desc],Qty as Qty,UOM," &
                      "  Source_Doc_No as [Source Doc No],convert(date,Source_Doc_Date,103) as [Source Doc Date],Entry_Date as [Entry Date] , " &
                      "  cast(case when Basic_Cost=0 then ((case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end)/ (case when Qty=0 then 1 else Qty end) ) else  (case when InOut='O' THEN (-1 * Basic_Cost) ELSE Basic_Cost end) end as  decimal(16,2)) as [Basic Cost]  , " &
                      "  cast((case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end)/ (case when Stock_Qty=0 then 1 else Stock_Qty end) as  decimal(16,2)) as [Stock Cost]  ,  " &
                      "  tspl_inventory_movement_new.Created_By as [Created By],tspl_inventory_movement_new.Comp_Code as [Comp Code] ,ItemType as [Item Type],  " &
                      "  convert(date,Punching_Date,103) as [Punching Date],MRP,Batch_No  as [Batch No],MFG_Date as [MFG Date],Expiry_Date as [Expiry Date] , " &
                      "  (case when InOut='O' THEN (-1 * Avg_Cost) ELSE Avg_Cost end) as [Avg Cost] ,Posting_Date as [Posting Date], " &
                      "  Stock_UOM as [Stock UOM],(case when InOut='O' THEN (-1 * Stock_Qty) ELSE Stock_Qty end) as [Stock Qty] ,Item_Status as [Item Status],Assmbly_Status as [Assmbly Status],Fat_Per as [Fat %],SNF_Per as [SNF %],(case when InOut='O' THEN (-1 * Fat_KG) ELSE Fat_KG end) as [Fat KG]," &
                      "  (case when InOut='O' THEN (-1 * SNF_KG) ELSE SNF_KG end) as [SNF KG], main_location as [Main Location]," &
                      "  IS_CONSUMPTION,Cust_Code as [Customer Code],Cust_Name as [Customer Name],Vendor_Code as [Vendor Code],Vendor_Name as [Vendor Name],Other_Location_Code as [Other Location Code],Other_Location_Desc as [Other Location Desc],convert(decimal(18,2),Fat_Rate) as [Fat Rate] ,convert(decimal(18,2),SNF_Rate) as [SNF Rate],(case when InOut='O' THEN (-1 * Fat_Amt) ELSE Fat_Amt end) as [Fat Amt],(case when InOut='O' THEN (-1 * SNF_Amt) ELSE SNF_Amt end) as [SNF Amt], " &
                      "  (case when InOut='O' THEN (-1 * Std_Qty) ELSE Std_Qty end) as [Standard Qty],SYNC_STATUS  " &
                      "  from tspl_inventory_movement_new left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type " &
                      "  where Trans_Type in ('PROD_ENTRY','Disassembly') and convert(date,Source_Doc_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' AND convert(date,Source_Doc_Date,103) <='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "'   "


            qry = " select Final.[Trans Name] as [Document Type], Final.[Source Doc No] as [Document No],convert (varchar, Final.[Source Doc Date],103) as [Document Date],Final.[Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Desc], Final.[Item Code] , Final.[Item Desc],Final.UOM ,Final.Qty, case when final.InOut = 'I' then  Final.[Avg Cost] end as [Debit amount],case when final.InOut = 'O' then  Final.[Avg Cost] * (-1) end as [Credit amount] from ( " &
                  " " + BaseQry + " " &
                  " ) Final left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = Final.[Location Code] "
            If rdbDetails.Checked = True Then
                qry = qry + " order by  Final.[Source Doc No] "
            End If
            If rdbSummary.Checked Then
                qry = " select max(SummaryFinal.[Document Type]) as [Document Type], SummaryFinal.[Document No]  ,max( SummaryFinal.[Document Date]) as [Document Date],Sum ( SummaryFinal.[Debit amount]) as [Debit amount] ,sum (SummaryFinal.[Credit amount]) as [Credit amount],( Sum ( SummaryFinal.[Debit amount]) - sum (SummaryFinal.[Credit amount]) ) as [Process Gain/Loss] from (  " &
                       " " + qry + " " &
                       " ) SummaryFinal group by [Document No] order by  SummaryFinal.[Document No] "

            End If


            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim Debitamount As New GridViewSummaryItem("Debit amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Debitamount)
                Dim CreditAmount As New GridViewSummaryItem("Credit amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(CreditAmount)
                If rdbSummary.Checked = True Then
                    Dim ProcessGainLoss As New GridViewSummaryItem("Process Gain/Loss", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(ProcessGainLoss)
                End If
                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If rdbSummary.Checked = True Then
            VarID += "_SU"
        Else
            rdbDetails.Checked = True
            VarID += "_DE"
        End If

        Gv1.VarID = VarID
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
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
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub

    'Private Sub txtLocation__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
    '    txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@JWBilling", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    'End Sub
    'Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
    '    Dim ReportID As String = MyBase.Form_ID
    '    If clsCommon.myLen(ReportID) > 0 Then
    '        Gv1.MasterTemplate.FilterDescriptors.Clear()
    '        Dim obj As New clsGridLayout()
    '        obj.ReportID = ReportID
    '        obj.UserID = objCommonVar.CurrentUserCode
    '        obj.GridLayout = New MemoryStream()
    '        Gv1.SaveLayout(obj.GridLayout)
    '        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
    '        obj.GridColumns = Gv1.ColumnCount
    '        If obj.SaveData() Then
    '            common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
    '        End If


    '        obj.GridLayout.Close()
    '        obj.GridLayout.Dispose()
    '        ''---------------
    '    End If
    'End Sub

    'Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
    '    clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
    '    common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    'End Sub


   

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        If e.Column Is Gv1.Columns("Document No") Then
            Dim strCode As String = Gv1.CurrentRow.Cells("Document No").Value
            Dim strTransType As String = Gv1.CurrentRow.Cells("Document Type").Value
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Item code Found.")
            Else
                If clsCommon.CompairString("Production Entry", strTransType) = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, strCode)
                ElseIf clsCommon.CompairString("Disassembly", strTransType) = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAssembDis, strCode)
                End If
            End If
        End If
    End Sub

   

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptProcessGainLossReport & "'"))

                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptProcessGainLossReport & "'"))

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
    End Sub
End Class
