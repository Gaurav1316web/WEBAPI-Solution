Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System
Imports System.IO
Imports Telerik.WinControls.UI.Export
Public Class FrmERPStatusTrackingReport
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim buttontooltip As ToolTip = New ToolTip()
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub FrmERPStatusTrackingReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnReport.Enabled Then
            fillGridReport()
            chkDBT.Visible = False
            chkDBT.Checked = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E AndAlso btnreset.Enabled Then
            reset()
            chkDBT.Visible = False
            chkDBT.Checked = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
            chkDBT.Visible = False
            chkDBT.Checked = False
        ElseIf e.Control AndAlso e.Shift AndAlso e.Alt AndAlso e.KeyCode = Keys.F12 Then
            If chkDBT.Visible = True Then
                chkDBT.Visible = False
                chkDBT.Checked = False
            Else
                chkDBT.Visible = True
            End If
        End If
    End Sub
    Private Sub FrmERPStatusTrackingReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        buttontooltip.SetToolTip(btnReport, "Press Alt+R for Summary ")
        buttontooltip.SetToolTip(btnreset, "Press Alt+E for Reset ")
        buttontooltip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        If objCommonVar.RCDFCFP = True Then
            Label1.Text = "ERP Status At Cattle Feed Plants"
        Else
            Label1.Text = "ERP Status At Milk Unions"
        End If
        chkDBT.Checked = False
        chkDBT.Visible = False
    End Sub
    Public Sub fillGridReport()
        Try
            Dim query As String
            gv1.DataSource = Nothing
            If objCommonVar.RCDFCFP = True Then
                query = "select ROW_NUMBER() OVER(ORDER BY TSPL_LOCATION_MASTER.Location_code ASC) as SNo
                ,TSPL_LOCATION_MASTER.location_code as [Location Code] ,TSPL_LOCATION_MASTER.Location_Desc AS [Location Name]
                ,TSPL_LOCATION_MASTER.Loc_Short_Name as [Location]
                ,convert(varchar, GRN.GRN_Date,103) AS [Last Gate In" + Environment.NewLine + "Date]
                ,convert(varchar, WEIGHTMENT.Weighment_Date,103) as [Last Weighment" + Environment.NewLine + "Date]
                ,convert(varchar,QC.Document_Date,103) as [Last QC" + Environment.NewLine + "Date]
                ,convert(varchar,SRN.SRN_Date,103) as [Last SRN" + Environment.NewLine + "Date]
                ,convert(varchar, PInvoice.PI_Date,103) as [Last Purchase Invoice" + Environment.NewLine + "Date]
                ,convert(varchar, RECEIPT.Receipt_Date,103) as [Last Advance Receipt" + Environment.NewLine + "Date]
                ,convert(varchar, SHIPMENT.Document_Date,103) as [Last Dispatch" + Environment.NewLine + "Date]
                ,convert(varchar, SInvoice.Document_Date,103) as [Last Sale Bill" + Environment.NewLine + "Date]
                ,convert(varchar, Production.PROD_DATE,103) as [Last Production Entry" + Environment.NewLine + "Date]
                FROM TSPL_LOCATION_MASTER LEFT OUTER JOIN
                (SELECT  max(TSPL_GRN_HEAD.GRN_Date) AS GRN_Date,TSPL_GRN_HEAD.Bill_To_Location FROM TSPL_GRN_HEAD
                GROUP BY TSPL_GRN_HEAD.Bill_To_Location) GRN
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =GRN.Bill_To_Location
                LEFT OUTER JOIN
                (SELECT  max(TSPL_PO_WEIGHTMENT_HEAD.Weighment_Date) AS Weighment_Date,TSPL_PO_WEIGHTMENT_HEAD.Location_code FROM TSPL_PO_WEIGHTMENT_HEAD where TSPL_PO_WEIGHTMENT_HEAD.Type='IN'
                GROUP BY TSPL_PO_WEIGHTMENT_HEAD.Location_code) WEIGHTMENT
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =WEIGHTMENT.Location_code
                LEFT OUTER JOIN
                (SELECT  max(TSPL_QC_CHECK_HEAD.Document_Date) AS Document_Date,TSPL_QC_CHECK_HEAD.Bill_To_Location FROM TSPL_QC_CHECK_HEAD
                GROUP BY TSPL_QC_CHECK_HEAD.Bill_To_Location) QC
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =QC.Bill_To_Location
                LEFT OUTER JOIN
                (SELECT  max(TSPL_SRN_HEAD.SRN_Date) AS SRN_Date,TSPL_SRN_HEAD.Bill_To_Location FROM TSPL_SRN_HEAD
                GROUP BY TSPL_SRN_HEAD.Bill_To_Location) SRN
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =SRN.Bill_To_Location
                LEFT OUTER JOIN
                (SELECT  max(TSPL_PI_HEAD.PI_Date) AS PI_Date,TSPL_PI_HEAD.Bill_To_Location FROM TSPL_PI_HEAD
                GROUP BY TSPL_PI_HEAD.Bill_To_Location) PInvoice
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =PInvoice.Bill_To_Location
                 LEFT OUTER JOIN
                (SELECT  max(TSPL_RECEIPT_HEADER.Receipt_Date) AS Receipt_Date,TSPL_RECEIPT_HEADER.Location_GL_Code FROM TSPL_RECEIPT_HEADER
                GROUP BY TSPL_RECEIPT_HEADER.Location_GL_Code) RECEIPT
                ON TSPL_LOCATION_MASTER.Loc_Segment_Code =RECEIPT.Location_GL_Code
                LEFT OUTER JOIN
                (SELECT  max(TSPL_SD_SHIPMENT_HEAD.Document_Date) AS Document_Date,TSPL_SD_SHIPMENT_HEAD.Bill_To_Location FROM TSPL_SD_SHIPMENT_HEAD
                GROUP BY TSPL_SD_SHIPMENT_HEAD.Bill_To_Location) SHIPMENT
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =SHIPMENT.Bill_To_Location
                LEFT OUTER JOIN
                (SELECT  max(tspl_sd_sale_invoice_head.Document_Date) AS Document_Date,tspl_sd_sale_invoice_head.Bill_To_Location FROM tspl_sd_sale_invoice_head
                GROUP BY tspl_sd_sale_invoice_head.Bill_To_Location) SInvoice
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =SInvoice.Bill_To_Location
                LEFT OUTER JOIN
                (select max(TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE) as PROD_DATE,TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE from TSPL_SPP_PRODUCTION_ENTRY
                group by TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE)Production
                ON TSPL_LOCATION_MASTER.LOCATION_CODE =Production.LOCATION_CODE
                where TSPL_LOCATION_MASTER.IsMainPlant='0'"
            Else
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
                If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                    gv1.DataSource = Nothing
                    Exit Sub
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT [TSPL_ERP_STATUS].Location_Name,[TSPL_ERP_STATUS].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_ERP_STATUS] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','RAJSAMAND','BANSWARA') ORDER BY [TSPL_ERP_STATUS].Location_Name")
                query = ""
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
                    'query += ",(select FORMAT(max(Indent_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_INDENT_HEAD where Post=1) as [Indent Date]"
                    query += ",(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date]"
                    query += ",(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SRN_HEAD where Status=1) as [Last Stock Received (SRN) Date]"
                    query += ",(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' and status=1) as [Last Stock Issue Date]"
                    query += ",(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ADJUSTMENT_HEADER where Posted='Y') as [Last Stock Adjustment Date]"
                    query += ",(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY where POSTED=1) as [Last Production Entry Date]"
                    query += ",(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER where Posted=1) as [Last Demand Date]"
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "RJS") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "BNS") = CompairStringResult.Equal Then
                        query += ",(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale where Posted=1) as [Last Dispatch Date]"
                    Else
                        query += ",(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD where Status=1) as [Last Dispatch Date]"

                    End If
                    query += ",(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PI_head where Status=1) as [Last Stock Voucher Date]"
                    query += ",(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD where Status=1) as [Last Sales Voucher Date]"
                    query += ",(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_RECEIPT_HEADER where Posted='Y') as [Last Receipt Date]"
                    If chkDBT.Checked Then
                        query += ",(select FORMAT(max(Document_Date),'MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD) as [Last DBT Entry]
                    ,(select FORMAT(max(Document_Date),'MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT) as [Last DBT Advice]
                    ,(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_MCC) as [Last BMC Truck Sheet]
                    ,(SELECT FORMAT(max(Document_Date),'dd/MMM/yyyy') FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS) as [Last DCS Truck Sheet Date]
                    ,(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PURCHASE_INVOICE_HEAD) as [Last Milk Bill Generation Date]"
                    End If
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                gv1.Visible = True
                gv1.DataSource = dt2
                gv1.ReadOnly = True
                SetGridFormat(gv1)
                ReStoreGridLayout()
                If objCommonVar.RCDFCFP = False Then
                    GridFormate()

                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                gv1.DataSource = Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub GridFormate()
        If objCommonVar.RCDFCFP = False Then
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup(" "))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("SNo").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Union Name").Name)
                view.ColumnGroups.Add(New GridViewColumnGroup("Purchase & Store"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                'view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Indent Date").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Last Purchase order Date").Name)
                'view.ColumnGroups.Add(New GridViewColumnGroup("Store"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Last Stock Received (SRN) Date").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Last Stock Issue Date").Name)
                view.ColumnGroups.Add(New GridViewColumnGroup("Production"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Last Stock Adjustment Date").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Last Production Entry Date").Name)
                view.ColumnGroups.Add(New GridViewColumnGroup("Sales & Marketing"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("Last Demand Date").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("Last Dispatch Date").Name)
                view.ColumnGroups.Add(New GridViewColumnGroup("Accounts"))
                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Last Stock Voucher Date").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Last Sales Voucher Date").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Last Receipt Date").Name)
                If chkDBT.Checked Then
                    view.ColumnGroups.Add(New GridViewColumnGroup("DBT Status"))
                    view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Last DBT Entry").Name)
                    view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Last DBT Advice").Name)
                    view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Last BMC Truck Sheet").Name)
                    view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Last DCS Truck Sheet Date").Name)
                    view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Last Milk Bill Generation Date").Name)
                End If
                gv1.ViewDefinition = view
            End If
        End If
    End Sub
    Sub SetGridFormat(ByRef Gv1 As RadGridView)
        Gv1.ShowGroupPanel = True
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.AutoSizeRows = False
        Gv1.BestFitColumns()
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub
    Sub reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.Visible = False
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnReport_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Try
            If objCommonVar.RCDFCFP = True Then
                PageSetupReport_ID = Me.Form_ID + "CFP"
            Else
                PageSetupReport_ID = Me.Form_ID + "D"
            End If
            TemplateGridview = gv1
            fillGridReport()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
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
    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
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
    Private Sub ReStoreGridLayoutDetails()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1
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
    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmERPStatusTrackingReport & "'"))
            arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt")) ' clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy HH:MM"))
            arrHeader.Add("User : " + objCommonVar.CurrentUser)
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Label1.Text, , arrHeader)
            Else
                'transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                'clsCommon.MyExportToPDF(Label1.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                Dim doc As New RadPrintDocument()
                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 100
                doc.Landscape = True
                doc.LeftFooter = "Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt")
                doc.RightFooter = "User : " + objCommonVar.CurrentUser
                doc.AssociatedObject = gv1
                Dim strHeader As String = Label1.Text 'Me.Text.Replace("/", "")
                doc.MiddleHeader = strHeader
                doc.HeaderFont = New Font("Verdana", 12, FontStyle.Bold)
                'doc.Print()
                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.ShowDialog()
                doc = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
    Private Sub chkDBT_CheckStateChanged(sender As Object, e As EventArgs) Handles chkDBT.CheckStateChanged
        If chkDBT.Checked Then
            Label1.Text = "Milk Procurement & DBT Status at Milk Unions"
        Else
            If objCommonVar.RCDFCFP = True Then
                Label1.Text = "ERP Status At Cattle Feed Plants"
            Else
                Label1.Text = "ERP Status At Milk Unions"
            End If
        End If
    End Sub
End Class
