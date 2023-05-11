'' =====Ticket No- BM00000009456 Fresh Price chart report
Imports common
Imports System.IO

Public Class frmRptPriceChartMaster
    Inherits FrmMainTranScreen
    '#Region "Varables"
    '    Private Const ReportID As String = "Qua_Sta"
    '#End Region


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptPriceChartFreshSalesReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub frmRptVendorLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            dtpFromdate.Value = dtptodate.Value.AddMonths(-1)
            dtptodate.Value = clsCommon.GETSERVERDATE()
            RadPageView1.SelectedPage = RadPageViewPage1
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                RadMenuItem2.Visibility = ElementVisibility.Collapsed
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Private Sub txtLocation__MY_click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulDev", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)

    End Sub
    Private Sub txtItem__MY_click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = "select CAST(item_code as varchar) as Code ,item_Desc as Name  from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulItem", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub
    Private Sub txtUOM__MY_click(sender As Object, e As EventArgs) Handles txtUOM._My_Click
        Dim qry As String = "select Unit_Code as Code, Unit_Desc as Name from TSPL_UNIT_MASTER "
        txtUOM.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulUOM", qry, "Code", "Name", txtUOM.arrValueMember, txtUOM.arrDispalyMember)

    End Sub


    Private Sub frmRptVendorLedger_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funReset()
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadReport()
    End Sub

    Sub LoadReport()
        '        Dim qry As String = "selectTSPL_ITEM_PRICE_MASTER.Item_Price_ID as [Price Id],TSPL_ITEM_PRICE_MASTER.Price_Code_Desc as [Price Code Desc] ,convert(varchar,TSPL_ITEM_PRICE_MASTER.Start_Date, 103) as [Start Date],convert(varchar,TSPL_ITEM_PRICE_MASTER.End_Date, 103) as [End Date],TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as [Item Description],TSPL_ITEM_PRICE_MASTER.Item_MRP as MRP,TSPL_ITEM_PRICE_MASTER.Item_Basic_Net as [Basic Net],TSPL_ITEM_PRICE_MASTER.Price_Category as [Price_Category], " & _
        '" TSPL_ITEM_PRICE_MASTER.Item_Baisc_Price as [Basic Price],TSPL_ITEM_PRICE_MASTER.Tax_group as [Tax Group],(case TSPL_TAX_MASTER.Type when 'V' then 'VAT' when 'C' then 'CST' when 'E' then 'EXCISE' when 'A' then 'ADDTAX' when 'O' then 'OTHER' when 'S' then 'SERVICE' when 'M' then 'MANDI TAX' when 'W' then 'WCT' end) as [Tax Type],TSPL_ITEM_PRICE_MASTER.Price_Code as [PriceCode],TSPL_ITEM_PRICE_MASTER.Price_Comp1 as [Price Component Code],TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc1 as [Component Description],TSPL_ITEM_PRICE_MASTER.Price_Rate1,TSPL_ITEM_PRICE_MASTER.Price_Amount1, " & _
        '" TSPL_ITEM_PRICE_MASTER.Price_Comp2,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc2,TSPL_ITEM_PRICE_MASTER.Price_Rate2,TSPL_ITEM_PRICE_MASTER.Price_Amount2,Price_Comp3,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc3,Price_Rate3,Price_Amount3,TSPL_ITEM_PRICE_MASTER.Price_Comp4,TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc4,TSPL_ITEM_PRICE_MASTER.Price_Rate4,TSPL_ITEM_PRICE_MASTER.Price_Amount4,TSPL_ITEM_PRICE_MASTER.UOM,TSPL_ITEM_PRICE_MASTER.Item_Price_Id_No as [Item Price Id No],Convert(decimal(18,2) ,TSPL_ITEM_PRICE_MASTER.Item_Basic_Price) AS [Item Selling Price], " & _
        '" TSPL_LOCATION_MASTER.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location],TSPL_ITEM_PRICE_MASTER.Basic_Price_On,TSPL_ITEM_PRICE_MASTER.Created_By as [Created By],convert(varchar,TSPL_ITEM_PRICE_MASTER.Created_Date,103) as [Created Date],TSPL_ITEM_PRICE_MASTER.Posted_By as [Approve By],convert(varchar,TSPL_ITEM_PRICE_MASTER.Posted_Date,103) as [Approve Date] , TSPL_ITEM_PRICE_MASTER.Remarks  " & _

        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        'KUNAL > TICKET : BM00000009456 > REQ : KLREQ000758 > DATE : 25-NOV-2016 > BUG OF - STUTI

        'Dim qry As String = " SELECT TSPL_ITEM_PRICE_MASTER.Price_Code AS [Price Code], TSPL_ITEM_PRICE_MASTER.Price_Code_Desc AS [Price Code Desc], Convert(varchar,TSPL_ITEM_PRICE_MASTER.Item_Price_ID) AS [Item_Price_ID],  TSPL_ITEM_PRICE_MASTER.Remarks, TSPL_ITEM_MASTER.Item_Code AS [Item Code], TSPL_ITEM_MASTER.Item_Desc AS [Item Desc], TSPL_ITEM_PRICE_MASTER.Basic_Price_On, TSPL_ITEM_PRICE_MASTER.UOM, TSPL_ITEM_PRICE_MASTER.Item_MRP AS [Item MRP], TSPL_ITEM_PRICE_MASTER.Item_Basic_Net AS [Basic Net],  TSPL_ITEM_PRICE_MASTER.Price_Category AS [Price Category], convert(decimal(18,2), TSPL_ITEM_PRICE_MASTER.Item_Selling_Price) AS [Item_Selling_Price], (CASE TSPL_TAX_MASTER.Type  WHEN 'V' THEN 'VAT' WHEN 'C' THEN 'CST'  WHEN 'E' THEN 'EXCISE' WHEN 'A' THEN 'ADDTAX' WHEN 'O' THEN 'OTHER' WHEN 'S' THEN 'SERVICE' WHEN 'M' THEN 'MANDI TAX'  WHEN 'W' THEN 'WCT' END) AS [Tax Type],  TSPL_ITEM_PRICE_MASTER.Tax_group AS [Tax Group], TSPL_TAX_GROUP_MASTER.Tax_Group_Desc [Tax Group Desc],  TSPL_ITEM_PRICE_MASTER.Price_Comp1 AS [Price Component Code], TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc1 AS [Price_Comp_Desc1], TSPL_ITEM_PRICE_MASTER.Price_Rate1, TSPL_ITEM_PRICE_MASTER.Price_Amount1, TSPL_ITEM_PRICE_MASTER.Price_Rate2, TSPL_ITEM_PRICE_MASTER.Price_Amount2, Price_Rate3, Price_Amount3, TSPL_ITEM_PRICE_MASTER.Price_Rate4, TSPL_ITEM_PRICE_MASTER.Price_Amount4,  TSPL_ITEM_PRICE_MASTER.Price_Comp2, TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc2, Price_Comp3, TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc3, TSPL_ITEM_PRICE_MASTER.Price_Comp4, TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc4, CONVERT(varchar,TSPL_ITEM_PRICE_MASTER.Item_Price_Id_No) AS [Item Price Id No],   CONVERT(decimal(18, 2), TSPL_ITEM_PRICE_MASTER.Item_Basic_Price) AS [Basic Rate], TSPL_LOCATION_MASTER.Location_Code AS [Location Code], TSPL_LOCATION_MASTER.Location_Desc AS[Location], TSPL_ITEM_PRICE_MASTER.Created_By AS [Created By],  CONVERT(varchar,  TSPL_ITEM_PRICE_MASTER.Created_Date, 103) AS [Created Date], CONVERT(varchar, TSPL_ITEM_PRICE_MASTER.Start_Date, 103) AS [Start Date], CONVERT(varchar, TSPL_ITEM_PRICE_MASTER.End_Date, 103) AS [End Date], CONVERT(varchar, TSPL_ITEM_PRICE_MASTER.Posted_Date, 103) AS [Approve Date], TSPL_ITEM_PRICE_MASTER.Posted_By AS [Approve By] " & _
        Dim qry As String = " SELECT tspl_item_price_master.price_code AS [Price Code],  tspl_item_price_master.price_code_desc AS [Price Code Desc],   CONVERT(VARCHAR, tspl_item_price_master.item_price_id) AS [Item_Price_ID]   ,tspl_item_price_master.remarks,tspl_item_master.item_code AS [Item Code]   ,tspl_item_master.item_desc AS [Item Desc],  tspl_item_price_master.basic_price_on,tspl_item_price_master.uom,  tspl_item_price_master.item_mrp AS [Item MRP],  tspl_item_price_master.item_basic_net AS [Basic Net],    tspl_item_price_master.price_category AS [Price Category],    CONVERT(DECIMAL(18, 2), tspl_item_price_master.item_selling_price)  AS [Item_Selling_Price],( CASE tspl_tax_master.type   WHEN 'V' THEN 'VAT'  WHEN 'C' THEN 'CST'   WHEN 'E' THEN 'EXCISE'  WHEN 'A' THEN 'ADDTAX'   WHEN 'O' THEN 'OTHER'   WHEN 'S' THEN 'SERVICE'  WHEN 'M' THEN 'MANDI TAX'   WHEN 'W' THEN 'WCT'  END ) AS [Tax Type],tspl_item_price_master.tax_group AS    [Tax Group],   tspl_tax_group_master.tax_group_desc [Tax Group Desc], tspl_item_price_master.price_comp1 AS [Price Component Code],  tspl_item_price_master.price_comp_desc1 AS [Price_Comp_Desc1],   tspl_item_price_master.price_rate1,tspl_item_price_master.price_amount1,  tspl_item_price_master.price_comp2,   tspl_item_price_master.price_comp_desc2,  tspl_item_price_master.price_amount2,  tspl_item_price_master.price_rate2,price_comp3,  tspl_item_price_master.price_comp_desc3,price_amount3,price_rate3,  tspl_item_price_master.price_comp4,  tspl_item_price_master.price_comp_desc4,  tspl_item_price_master.price_amount4,tspl_item_price_master.price_rate4,  CONVERT(VARCHAR, tspl_item_price_master.item_price_id_no) AS  [Item Price Id No],CONVERT(DECIMAL(18, 2),  tspl_item_price_master.item_basic_price) AS   [Basic Rate],   tspl_location_master.location_code AS [Location Code],  tspl_location_master.location_desc AS [Location],   tspl_item_price_master.created_by AS [Created By],   CONVERT(VARCHAR, tspl_item_price_master.created_date, 103) AS  [Created Date],CONVERT(VARCHAR, tspl_item_price_master.start_date, 103)  AS [Start Date],CONVERT(VARCHAR,  tspl_item_price_master.end_date, 103) AS [End Date],   CONVERT(VARCHAR, tspl_item_price_master.posted_date, 103) AS   [Approve Date],tspl_item_price_master.posted_by AS [Approve By] " & _
                            " from TSPL_ITEM_PRICE_MASTER " & _
                            " left outer join TSPL_TAX_MASTER ON TSPL_ITEM_PRICE_MASTER.Tax_group=TSPL_TAX_MASTER.Tax_Code" & _
                            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_code=TSPL_ITEM_PRICE_MASTER.Item_Code " & _
                            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_ITEM_PRICE_MASTER.Location_Code LEFT JOIN TSPL_TAX_GROUP_MASTER  ON TSPL_ITEM_PRICE_MASTER.Tax_group = TSPL_TAX_GROUP_MASTER.Tax_Group_Code and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' " & _
                            "  where 2=2 " & _
                            "and TSPL_ITEM_PRICE_MASTER.Start_Date >= CAST(convert(date,'" & dtpFromdate.Value & "',103) as date) and TSPL_ITEM_PRICE_MASTER.Start_Date <= CAST(convert(date,'" & dtptodate.Value & "',103) as date) "

        ' query in group format
        ' Dim qry As String = " SELECT TSPL_ITEM_PRICE_MASTER.Price_Code AS [Price Code], MAX(TSPL_ITEM_PRICE_MASTER.Price_Code_Desc) AS [Price Code Desc], MAX(Convert(varchar,TSPL_ITEM_PRICE_MASTER.Item_Price_ID)) AS [Item_Price_ID],  MAX(TSPL_ITEM_PRICE_MASTER.Remarks) AS [Remark], MAX(TSPL_ITEM_MASTER.Item_Code) AS [Item Code], MAX(TSPL_ITEM_MASTER.Item_Desc) AS [Item Desc], MAX(TSPL_ITEM_PRICE_MASTER.Basic_Price_On) [Basic_Price_On], max(TSPL_ITEM_PRICE_MASTER.UOM) AS UOM , max(TSPL_ITEM_PRICE_MASTER.Item_MRP) AS MRP,  MAX(TSPL_ITEM_PRICE_MASTER.Item_Basic_Net) AS [Item_Basic_Net], MAX(TSPL_ITEM_PRICE_MASTER.Price_Category) AS [Price_Category], MAX(TSPL_ITEM_PRICE_MASTER.Item_Baisc_Price) AS [Item_Baisc_Price], MAX((CASE TSPL_TAX_MASTER.Type  WHEN 'V' THEN 'VAT' WHEN 'C' THEN 'CST'  WHEN 'E' THEN 'EXCISE' WHEN 'A' THEN 'ADDTAX'  WHEN 'O' THEN 'OTHER' WHEN 'S' THEN 'SERVICE' WHEN 'M' THEN 'MANDI TAX' WHEN 'W' THEN 'WCT' END)) AS [Tax Type],  MAX(TSPL_ITEM_PRICE_MASTER.Tax_group) AS [Tax Group], MAX (TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) AS [Tax Group Desc], MAX(TSPL_ITEM_PRICE_MASTER.Price_Comp1) AS [Price Component Code], MAX(TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc1) AS [Component Description], MAX(TSPL_ITEM_PRICE_MASTER.Price_Rate1) AS Price_Rate1, MAX(TSPL_ITEM_PRICE_MASTER.Price_Amount1) AS Price_Amount1,  MAX(TSPL_ITEM_PRICE_MASTER.Price_Rate2) Price_Rate2, MAX(TSPL_ITEM_PRICE_MASTER.Price_Amount2) AS Price_Amount2,  MAX(Price_Rate3) as Price_Rate3, MAX(Price_Amount3)  as Price_Amount3 , MAX(TSPL_ITEM_PRICE_MASTER.Price_Rate4) as Price_Rate4,  MAX(TSPL_ITEM_PRICE_MASTER.Price_Amount4) as Price_Amount4, MAX(TSPL_ITEM_PRICE_MASTER.Price_Comp2) as Price_Comp2,  MAX(TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc2) as Price_Comp_Desc2, MAX(Price_Comp3) as Price_Rate4, MAX(TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc3) as Price_Comp_Desc3,  MAX(TSPL_ITEM_PRICE_MASTER.Price_Comp4) as Price_Comp4,  MAX(TSPL_ITEM_PRICE_MASTER.Price_Comp_Desc4) as Price_Comp_Desc4, MAX(CONVERT(varchar,TSPL_ITEM_PRICE_MASTER.Item_Price_Id_No)) AS [Item Price Id No],  MAX(CONVERT(decimal(18, 2), TSPL_ITEM_PRICE_MASTER.Item_Selling_Price)) AS [Item Selling Price], MAX(TSPL_LOCATION_MASTER.Location_Code) AS [Location Code],  MAX(TSPL_LOCATION_MASTER.Location_Desc) AS [Location], MAX(TSPL_ITEM_PRICE_MASTER.Created_By) AS [Created By],  MAX(CONVERT(varchar, TSPL_ITEM_PRICE_MASTER.Created_Date, 103)) AS [Created Date],  MAX(CONVERT(varchar, TSPL_ITEM_PRICE_MASTER.Start_Date, 103)) AS [Start Date],  MAX(CONVERT(varchar, TSPL_ITEM_PRICE_MASTER.End_Date, 103)) AS [End Date],  MAX(CONVERT(varchar, TSPL_ITEM_PRICE_MASTER.Posted_Date, 103)) AS [Approve Date], MAX(TSPL_ITEM_PRICE_MASTER.Posted_By) AS [Approve By] FROM TSPL_ITEM_PRICE_MASTER LEFT OUTER JOIN TSPL_TAX_MASTER ON TSPL_ITEM_PRICE_MASTER.Tax_group = TSPL_TAX_MASTER.Tax_Code LEFT OUTER JOIN TSPL_ITEM_MASTER  ON TSPL_ITEM_MASTER.Item_code = TSPL_ITEM_PRICE_MASTER.Item_Code LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =  TSPL_ITEM_PRICE_MASTER.Location_Code LEFT JOIN TSPL_TAX_GROUP_MASTER  ON TSPL_ITEM_PRICE_MASTER.Tax_group = TSPL_TAX_GROUP_MASTER.Tax_Group_Code AND TSPL_TAX_GROUP_MASTER.Tax_Group_Type = 'S' WHERE 2 = 2 " & _
        '"and TSPL_ITEM_PRICE_MASTER.Start_Date >= CAST(convert(date,'" & dtpFromdate.Value & "',103) as date) and TSPL_ITEM_PRICE_MASTER.Start_Date <= CAST(convert(date,'" & dtptodate.Value & "',103) as date) "

        If txtmultipricecode.arrValueMember IsNot Nothing AndAlso txtmultipricecode.arrValueMember.Count > 0 Then
            qry += " and Tspl_item_price_master.Price_Code  in (" + clsCommon.GetMulcallString(txtmultipricecode.arrValueMember) + ") "
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            qry += " and Tspl_item_price_master.Location_code  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") "
        End If
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            qry += " and TSPL_ITEM_PRICE_MASTER.Item_Code  in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") "
        End If
        If txtUOM.arrValueMember IsNot Nothing AndAlso txtUOM.arrValueMember.Count > 0 Then
            qry += " and Tspl_item_price_master.UOM  in (" + clsCommon.GetMulcallString(txtUOM.arrValueMember) + ") "
        End If

        ' qry += " GROUP BY Price_Code ORDER BY Price_Code , UOM ASC "

        ' qry += " ORDER BY Price_Code , UOM ASC "

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(qry)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                gv.Columns("Basic Net").IsVisible = False
                gv.Columns("Basic Net").VisibleInColumnChooser = False
                gv.Columns("Item Price Id No").IsVisible = False
                gv.Columns("Item Price Id No").VisibleInColumnChooser = False

            End If
            FormatGrid()
            ReStoreGridLayout()
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found..")
        End If
    End Sub

    Sub funReset()
        Try
            txtItem.arrValueMember = Nothing
            txtUOM.arrValueMember = Nothing
            txtLocation.arrValueMember = Nothing
            txtmultipricecode.arrValueMember = Nothing
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            dtpFromdate.Value = Today.AddMonths(-1)
            dtptodate.Value = clsCommon.GETSERVERDATE()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FormatGrid()
        gv.AllowAddNewRow = False
        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            'gv.Columns(ii).Width = 150
            gv.BestFitColumns()
        Next
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
    Private Sub RmSL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtnSaveLayout.Click
        gv.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = PageSetupReport_ID
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gv.SaveLayout(obj.GridLayout)
        obj.GridColumns = gv.ColumnCount
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
        End If
        ''stuti regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
    End Sub

    Private Sub RmDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtnDeleteLayout.Click
        If clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode) Then
            common.clsCommon.MyMessageBoxShow("Layout Deleted successfully", "Information")
        End If
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtmultipricecode__My_Click(sender As Object, e As EventArgs) Handles txtmultipricecode._My_Click
        Try
            Dim qry As String = " select Price_Code as Code , max(Price_Code_Desc) as Name  from TSPL_ITEM_PRICE_MASTER group by Price_Code "
            txtmultipricecode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulItemPrice", qry, "Code", "Name", txtmultipricecode.arrValueMember, txtmultipricecode.arrDispalyMember)

        Catch ex As Exception

        End Try
    End Sub


    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPriceChartFreshSalesReport & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("From Date : " & clsCommon.myCstr(dtpFromdate.Text))
            arrHeader.Add("To Date : " & clsCommon.myCstr(dtptodate.Text))
            ' kunal > kdil > date : 30-11-2016
            If txtmultipricecode.arrValueMember IsNot Nothing AndAlso txtmultipricecode.arrValueMember.Count > 0 Then
                arrHeader.Add(" Price Code : " + clsCommon.GetMulcallStringWithComma(txtmultipricecode.arrValueMember))
            End If

            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If

            If txtUOM.arrValueMember IsNot Nothing AndAlso txtUOM.arrValueMember.Count > 0 Then
                arrHeader.Add(" Unit : " + clsCommon.GetMulcallStringWithComma(txtUOM.arrDispalyMember))
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

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
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim arrHeader As List(Of String) = New List(Of String)()

        arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPriceChartFreshSalesReport & "'"))
        arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        'arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")
        arrHeader.Add("From Date : " & clsCommon.myCstr(dtpFromdate.Text))
        arrHeader.Add("To Date : " & clsCommon.myCstr(dtptodate.Text))

        If txtmultipricecode.arrValueMember IsNot Nothing AndAlso txtmultipricecode.arrValueMember.Count > 0 Then
            arrHeader.Add("Price Code : " + clsCommon.GetMulcallStringWithComma(txtmultipricecode.arrDispalyMember))
        End If

        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
        End If

        If txtUOM.arrValueMember IsNot Nothing AndAlso txtUOM.arrValueMember.Count > 0 Then
            arrHeader.Add("Unit : " + clsCommon.GetMulcallStringWithComma(txtUOM.arrDispalyMember))
        End If

        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
        End If

        If gv.Rows.Count <= 0 Then
            gv.Focus()
            clsCommon.MyMessageBoxShow("Data not found.")
        Else
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Fresh Price Chart Report", gv, arrHeader, "Fresh Price Chart Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If

    End Sub

    Private Sub RadGroupBox1_Click(sender As Object, e As EventArgs) Handles RadGroupBox1.Click

    End Sub
End Class










