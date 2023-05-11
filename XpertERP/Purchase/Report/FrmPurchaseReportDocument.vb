Imports common
Imports System.Data.SqlClient


Public Class FrmPurchaseReportDocument
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub



    Private Sub FrmMRDAReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()

    End Sub



    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try

       
        gvData.DataSource = Nothing
        gvData.Rows.Clear()

        Dim strFromDate As String = clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy")
            Dim StrQuery As String = Nothing
            StrQuery = " select xx.[PO No],MAX(xx.[PO Date]) AS [PO Date],MAX(xx.[PO Amount]) AS [PO Amount],xx.[Vendor Code],MAX(xx.[Vendor Name]) AS [Vendor Name],xx.[Location Code],MAX(xx.[Location Name]) AS [Location Name] ,xx.[Purchase Order Type]  ,xx.[GRN No] " &
         ",MAX(xx.[GRN Date]) AS [GRN Date] ,MAX(xx.[GRN Amount]) AS [GRN Amount] ,xx.[Weighment No] ,xx.[MRN No],MAX(xx.[MRN Date]) AS [MRN Date],MAX(xx.[MRN Amount]) AS [MRN Amount] ,xx.[QC No] ,xx.[SRN No],MAX(xx.[SRN Date]) AS [SRN Date],MAX(xx.[SRN Amount] ) AS [SRN Amount],xx.[PI No],MAX(xx.[PI Date]) AS [PI Date],MAX(xx.[PI Amount]) AS[PI Amount] from ("
            StrQuery += "select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as [PO No],convert(varchar,PurchaseOrder_Date,103) as [PO Date],convert(decimal(18,2),TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty) as [PO Qty],PO_Total_Amt as [PO Amount]"
            StrQuery += " ,TSPL_PURCHASE_ORDER_HEAD.Vendor_code as [Vendor Code],tspl_vendor_master.Vendor_Name as [Vendor Name],TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name] "
        StrQuery += " ,case when TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type='J' then 'Job Work' else case when TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type='L' then 'Domestic' else case when TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type='I' then 'Import' end end end  as [Purchase Order Type] "
        StrQuery += " ,TSPL_GRN_HEAD.GRN_No as [GRN No],convert(varchar,TSPL_GRN_HEAD.GRN_Date,103) as [GRN Date],convert(decimal(18,2),TSPL_GRN_DETAIL.GRN_Qty) as [GRN Qty],TSPL_GRN_HEAD.GRN_Total_Amt as [GRN Amount]"
            StrQuery += " ,isnull(TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code,'') as [Weighment No]"
        StrQuery += " ,TSPL_MRN_head.MRN_No as [MRN No],convert(varchar,TSPL_MRN_head.MRN_Date,103) as [MRN Date],convert(decimal(18,2),TSPL_MRN_DETAIL.mrn_qty) as [MRN Qty],TSPL_MRN_head.MRN_Total_Amt as [MRN Amount]"
        StrQuery += " ,isnull(TSPL_QC_CHECK_MRN_DETAIL.Document_Code,'') as [QC No]"
        StrQuery += " ,TSPL_SRN_head.SRN_No as [SRN No],convert(varchar,TSPL_SRN_head.SRN_Date,103) as [SRN Date],convert(decimal(18,2),TSPL_SRN_DETAIL.SRN_Qty) as [SRN Qty],TSPL_SRN_head.SRN_Total_Amt as [SRN Amount]"
        StrQuery += " ,TSPL_PI_HEAD.PI_No as [PI No],TSPL_PI_HEAD.pi_date as [PI Date],convert(decimal(18,2),TSPL_PI_DETAIL.PI_Qty) as [PI Qty],TSPL_PI_HEAD.PI_Total_Amt as [PI Amount] "
        StrQuery += " from TSPL_PURCHASE_ORDER_HEAD"
        StrQuery += " left outer join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No"
            'StrQuery += " left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.Against_PO=tspl_purchase_order_head.PurchaseOrder_No"
            'StrQuery += " left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No and TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code "
            'StrQuery += " left outer join TSPL_MRN_head on TSPL_MRN_head.Against_GRN=TSPL_GRN_HEAD.GRN_No  and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_MRN_head.Against_PO "
            'StrQuery += " left outer join TSPL_MRN_DETAIL on TSPL_MRN_head.mrn_no =TSPL_MRN_DETAIL.mrn_no and  TSPL_MRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code "
            'StrQuery += " left outer join TSPL_SRN_head on TSPL_SRN_head.Against_GRN=TSPL_GRN_HEAD.GRN_No and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_SRN_HEAD.Against_PO"
            'StrQuery += "  and TSPL_GRN_HEAD.GRN_No=TSPL_SRN_HEAD.Against_GRN and TSPL_MRN_HEAD.MRN_No=TSPL_SRN_HEAD.Against_MRN "
            'StrQuery += " left outer join TSPL_SRN_DETAIL on TSPL_SRN_head.SRN_No=TSPL_SRN_DETAIL.SRN_No  and TSPL_SRN_DETAIL.Item_Code=  TSPL_MRN_DETAIL.Item_Code "
            'StrQuery += " left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.Against_SRN=TSPL_SRN_head.SRN_No  and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_HEAD.Against_PO"
            'StrQuery += "  and TSPL_GRN_HEAD.GRN_No=TSPL_PI_HEAD.Against_GRN and TSPL_MRN_HEAD.MRN_No=TSPL_PI_HEAD.Against_MRN "
            'StrQuery += " left outer join TSPL_PI_DETAIL on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No   and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID"
            'StrQuery += "  and TSPL_GRN_HEAD.GRN_No=TSPL_PI_DETAIL.GRN_ID and TSPL_MRN_HEAD.MRN_No=TSPL_PI_DETAIL.MRN_ID   and TSPL_PI_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code  "
            StrQuery += " left outer join TSPL_GRN_DETAIL on TSPL_GRN_DETAIL.po_id=tspl_purchase_order_head.PurchaseOrder_No and TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code"
            StrQuery += " left outer join TSPL_GRN_HEAD on TSPL_GRN_DETAIL.GRN_No=TSPL_GRN_HEAD.GRN_No "
            StrQuery += " left outer join TSPL_MRN_DETAIL on TSPL_GRN_HEAD.GRN_No =TSPL_MRN_DETAIL.GRN_Id and  TSPL_MRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_MRN_DETAIL.PO_ID and  TSPL_MRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code "
            StrQuery += " left outer join TSPL_MRN_head on TSPL_MRN_head.mrn_no =TSPL_MRN_DETAIL.mrn_no "
            StrQuery += " left outer join TSPL_SRN_DETAIL on TSPL_SRN_DETAIL.GRN_ID=TSPL_GRN_HEAD.GRN_No and TSPL_GRN_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code"
            StrQuery += "  AND TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_SRN_DETAIL.PO_ID  and  TSPL_PURCHASE_ORDER_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code  and TSPL_SRN_DETAIL.MRN_ID=TSPL_MRN_head.mrn_no and  TSPL_MRN_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code "
            StrQuery += "  left outer join TSPL_SRN_head on TSPL_SRN_head.SRN_No=TSPL_SRN_DETAIL.SRN_No "
            StrQuery += "  left outer join TSPL_PI_DETAIL on TSPL_SRN_head.SRN_No=TSPL_PI_DETAIL.SRN_ID and TSPL_PI_DETAIL.Item_Code=TSPL_SRN_DETAIL.Item_Code"
            StrQuery += "  and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PI_DETAIL.PO_ID  and TSPL_PI_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code and TSPL_GRN_HEAD.GRN_No=TSPL_PI_DETAIL.GRN_ID and TSPL_PI_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code and TSPL_MRN_HEAD.MRN_No=TSPL_PI_DETAIL.MRN_ID and TSPL_PI_DETAIL.Item_Code=TSPL_MRN_DETAIL.Item_Code "
            StrQuery += "  left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No "
            StrQuery += " left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_PURCHASE_ORDER_HEAD.vendor_code"
            StrQuery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.location_code=TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location "
        StrQuery += " left join TSPL_PO_WEIGHTMENT_HEAD on TSPL_PO_WEIGHTMENT_HEAD.Against_GRN_No=TSPL_GRN_HEAD.GRN_No left join TSPL_PO_WEIGHTMENT_DETAIL on TSPL_PO_WEIGHTMENT_DETAIL.Weighment_Code=TSPL_PO_WEIGHTMENT_HEAD.Weighment_Code and TSPL_PO_WEIGHTMENT_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code "
        StrQuery += " left join TSPL_QC_CHECK_MRN_DETAIL on TSPL_QC_CHECK_MRN_DETAIL.MRN_No=TSPL_MRN_DETAIL.MRN_No "
        StrQuery += " where 2=2 "
        StrQuery += " and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)>='" + strFromDate + "' and convert(date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103)<='" + strToDate + "'" + Environment.NewLine

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_PURCHASE_ORDER_HEAD.bill_to_location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
            Else
                If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    StrQuery += " and TSPL_PURCHASE_ORDER_HEAD.bill_to_location in (" + objCommonVar.strCurrUserLocations + ")"
                End If
            End If
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_PURCHASE_ORDER_HEAD.Vendor_Code in(" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")" + Environment.NewLine
            End If
            StrQuery += " )xx group by  xx.[PO No],xx.[Vendor Code],xx.[Location Code],xx.[Purchase Order Type]  ,xx.[GRN No],xx.[Weighment No] ,xx.[MRN No],xx.[QC No] ,xx.[SRN No],xx.[PI No] "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuery)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            RadPageView1.SelectedPage = RadPageViewPage2
            gvData.GroupDescriptors.Clear()
            gvData.MasterTemplate.SummaryRowsBottom.Clear()
            gvData.DataSource = dt
            
            gvData.AutoExpandGroups = True
            gvData.ShowGroupPanel = False
            gvData.ShowRowHeaderColumn = False
            gvData.AllowAddNewRow = False
            gvData.AllowDeleteRow = False
            gvData.EnableFiltering = True
            gvData.ReadOnly = True
            gvData.ShowFilteringRow = True
            gvData.BestFitColumns()

                'Dim obj As New ExpressionFormattingObject("MyCondition", "isnull([Weighment No],'') = '' and isnull([MRN No],'') = '' and isnull([QC No],'') = '' and isnull([SRN No],'') = '' and isnull([PI No],'')= ''", False)
                'obj.CellBackColor = Color.OrangeRed
                'gvData.Columns("Weighment No").ConditionalFormattingObjectList.Add(obj)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER where  Location_Type='Physical' "
        If objCommonVar.ApplyLocationFilterBasedOnPermission = True AndAlso clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            qry += " and TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select vendor_code as code,Vendor_Name as Name from tspl_vendor_master "
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("venMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        gvData.DataSource = Nothing
        gvData.Rows.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtLocation.arrValueMember = Nothing
        txtVendor.arrValueMember = Nothing

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub




    Private Sub FrmMRDAReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.Control And e.KeyCode = Keys.P Then

        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If


    End Sub


    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptPOAgainstDocument & "'"))

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gvData, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gvData, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class
