Imports common
Public Class frmRMProcessLoss
    Inherits FrmMainTranScreen
    Const colSno As String = "Sno"
    Const colItemType As String = "ItemType"
    Const colitemcode As String = "ItemCode"
    Const colitemname As String = "itemname"
    Const colitemcodeProduction As String = "ItemCodeProduction"
    Const colitemnameProduction As String = "itemnameProduction"
    Const colQtyProduction As String = "QtyProduction"
    Const coluom As String = "Uom"
    Const colopQty As String = "opQty"
    Const colopAmt As String = "OpAmt"
    Const colRecQty As String = "RecQty"
    Const colRecAmt As String = "RecAmt"
    Const colIssProdQty As String = "IssProdQty"
    Const colIssProdAmt As String = "IssProdAmt"
    Const colOtherIssQty As String = "OtherIssQty"
    Const colOtherIssAmt As String = "OtherIssAmt"
    Const colClQty As String = "ClQty"
    Const colClAmt As String = "ClAmt "
    Const colPLQty As String = "PLQty"
    Const colPLper As String = "PLper"
    Const colRate As String = "Rate"
    Const colFinalClStock As String = "FinalClStock "
    Const colStockTransferQty As String = "StockTransferQty"
    Const colStockTransferAmt As String = "StockTransferAmt"
    Const colProductioncode As String = "ProductionCode"
    Const colBomcodedetail As String = "Bomcodedetail"
    Dim isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Public Template_Status As String = Nothing
    Public colNature As String = "Nature"
    Dim StrPermission As String
    Dim Slot1FD As DateTime = Nothing
    Dim Slot1TD As DateTime = Nothing
    Dim Slot2FD As DateTime = Nothing
    Dim Slot2TD As DateTime = Nothing
    Dim Slot3FD As DateTime = Nothing
    Dim Slot3TD As DateTime = Nothing
    'Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Sub LoadBlankGridProduction()
        Gv2.Rows.Clear()
        Gv2.Columns.Clear()
        Dim repoStr As New GridViewTextBoxColumn()
        Dim repoInt As New GridViewDecimalColumn()
        Dim repoChk As New GridViewCheckBoxColumn()
        Dim repoDate As New GridViewDateTimeColumn()
        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Code"
        repoStr.Name = colitemcodeProduction
        repoStr.Width = 100
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        Gv2.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Name"
        repoStr.Name = colitemnameProduction
        repoStr.Width = 150
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        Gv2.MasterTemplate.Columns.Add(repoStr)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = "{0:F2}"
        repoInt.HeaderText = "Quantity(QTL)"
        repoInt.Name = colQtyProduction
        repoInt.Width = 70
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv2.MasterTemplate.Columns.Add(repoInt)

        Gv2.AllowAddNewRow = False
        Gv2.AllowDeleteRow = False
        Gv2.ShowGroupPanel = False
        Gv2.AllowColumnReorder = True
        Gv2.AllowRowReorder = False
        Gv2.EnableSorting = False
        Gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv2.MasterTemplate.ShowRowHeaderColumn = False
        Gv2.TableElement.TableHeaderHeight = 40
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        repoStr = Nothing
        repoInt = Nothing
        repoChk = Nothing
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoStr As New GridViewTextBoxColumn()
        Dim repoInt As New GridViewDecimalColumn()
        Dim repoChk As New GridViewCheckBoxColumn()
        Dim repoDate As New GridViewDateTimeColumn()


        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Code"
        repoStr.Name = colitemcode
        repoStr.Width = 70
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Type"
        repoStr.Name = colItemType
        repoStr.Width = 70
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Name"
        repoStr.Name = colitemname
        repoStr.Width = 150
        repoStr.IsVisible = True
        'repoStr.CustomFormat = True
        repoStr.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoStr)


        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "UOM"
        repoStr.Name = coluom
        repoStr.Width = 70
        repoStr.IsVisible = True
        repoStr.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = "{0:F2}"
        repoInt.HeaderText = "Quantity"
        repoInt.Name = colopQty
        repoInt.Width = 70
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Amount"
        repoInt.Name = colopAmt
        repoInt.Width = 90
        repoInt.ReadOnly = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = "{0:F2}"
        repoInt.HeaderText = "Quantity"
        repoInt.Name = colRecQty
        repoInt.Width = 70
        repoInt.ReadOnly = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Amount"
        repoInt.Name = colRecAmt
        repoInt.Width = 90
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = "{0:F2}"
        repoInt.HeaderText = "Quantity"
        repoInt.Name = colIssProdQty
        repoInt.Width = 70
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Amount"
        repoInt.Name = colIssProdAmt
        repoInt.Width = 90
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = "{0:F2}"
        repoInt.HeaderText = "Quantity"
        repoInt.Name = colOtherIssQty
        repoInt.Width = 70
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Amount"
        repoInt.Name = colOtherIssAmt
        repoInt.Width = 90
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = "{0:F2}"
        repoInt.HeaderText = "Quantity"
        repoInt.Name = colStockTransferQty
        repoInt.Width = 70
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Amount"
        repoInt.Name = colStockTransferAmt
        repoInt.Width = 90
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)


        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = "{0:F2}"
        repoInt.HeaderText = "Quantity"
        repoInt.Name = colClQty
        repoInt.Width = 70
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Amount"
        repoInt.Name = colClAmt
        repoInt.Width = 90
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = ""
        repoInt.HeaderText = "Quantity"
        repoInt.Name = colPLQty
        repoInt.Width = 70
        repoInt.ReadOnly = False
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = "{0:F2}"
        repoInt.HeaderText = "%AGE"
        repoInt.Name = colPLper
        repoInt.Width = 90
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = "{0:F2}"
        repoInt.HeaderText = "Rate"
        repoInt.Name = colRate
        repoInt.Width = 70
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)

        repoInt = New GridViewDecimalColumn()
        repoInt.FormatString = "{0:F2}"
        repoInt.HeaderText = "Final"
        repoInt.Name = colFinalClStock
        repoInt.Width = 90
        repoInt.ReadOnly = True
        repoInt.IsVisible = True
        repoInt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInt)



        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        repoStr = Nothing
        repoInt = Nothing
        repoChk = Nothing
        View()
    End Sub
    Sub View()

        If gv1.Rows.Count >= 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colitemname).Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(coluom).Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colItemType).Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Opening Stock"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colopQty).Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colopAmt).Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Received"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns(colRecQty).Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns(colRecAmt).Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Issue In Production"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns(colIssProdQty).Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns(colIssProdAmt).Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Other Issue"))
            view.ColumnGroups(6).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv1.Columns(colOtherIssQty).Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(gv1.Columns(colOtherIssAmt).Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Stock Transfer"))
            view.ColumnGroups(7).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(7).Rows(0).ColumnNames.Add(gv1.Columns(colStockTransferQty).Name)
            view.ColumnGroups(7).Rows(0).ColumnNames.Add(gv1.Columns(colStockTransferAmt).Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Closing Stock"))
            view.ColumnGroups(8).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(8).Rows(0).ColumnNames.Add(gv1.Columns(colClQty).Name)
            view.ColumnGroups(8).Rows(0).ColumnNames.Add(gv1.Columns(colClAmt).Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Process Loss"))
            view.ColumnGroups(9).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(9).Rows(0).ColumnNames.Add(gv1.Columns(colPLQty).Name)
            view.ColumnGroups(9).Rows(0).ColumnNames.Add(gv1.Columns(colPLper).Name)



            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(10).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(10).Rows(0).ColumnNames.Add(gv1.Columns(colRate).Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Closing Stock"))
            view.ColumnGroups(11).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(11).Rows(0).ColumnNames.Add(gv1.Columns(colFinalClStock).Name)

            gv1.ViewDefinition = view
        End If
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub txtLoc__MYValidating_1(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLoc._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLoc.Value = clsCommon.ShowSelectForm("ShipmentMasteidfndr", qry, "Code", WhrCls, txtLoc.Value, "Code", isButtonClicked)
        lblloc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLoc.Value + "'"))
    End Sub
    Private Sub frmRMProcessLoss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddNew()
        LoadBlankGrid()
        LoadBlankGridProduction()
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub txtFromDate_ValueChanged(sender As Object, e As EventArgs) Handles txtFromDate.ValueChanged
        Dim selectedMonth As Integer = txtFromDate.Value.Month
        Dim selectedYear As Integer = txtFromDate.Value.Year
        Dim currentDate As New DateTime(selectedYear, selectedMonth, 1)
        Slot1FD = clsCommon.GetPrintDate(currentDate, "dd/MMM/yyyy")
        Slot1TD = clsCommon.GetPrintDate(currentDate.AddDays(9), "dd/MMM/yyyy")
        Slot2FD = clsCommon.GetPrintDate(currentDate.AddDays(10), "dd/MMM/yyyy")
        Slot2TD = clsCommon.GetPrintDate(currentDate.AddDays(19), "dd/MMM/yyyy")
        Slot3FD = clsCommon.GetPrintDate(currentDate.AddDays(20), "dd/MMM/yyyy")
        txtTodate.Value = clsCommon.GetPrintDate(currentDate.AddMonths(1).AddDays(-1), "dd/MMM/yyyy")
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim dt As New DataTable
        Try
            If clsCommon.myLen(txtLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select location")
                Exit Sub
            End If
            Dim DocNo As String = clsDBFuncationality.getSingleValue("select Document_code from TSPL_RM_PROCESS_LOSS where convert(date,'" + txtFromDate.Value + "' ,103) between convert(date,From_date,103)  and convert(date,to_date,103) AND  LOCATION='" + txtLoc.Value + "'")
            Dim DocNo2 As String = clsDBFuncationality.getSingleValue("select Document_code from TSPL_RM_PROCESS_LOSS where convert(date,'" + txtTodate.Value + "' ,103) between convert(date,From_date,103)  and convert(date,to_date,103) AND LOCATION='" + txtLoc.Value + "'")
            If clsCommon.myLen(DocNo) > 0 And clsCommon.myLen(DocNo2) > 0 Then
                Throw New Exception("Please select correct period")
            Else
                gv1.Rows.Clear()
                Dim Qry As String = "select case when isnull(IssueQtyPro,0)=0 then 0 else  cast(isnull(IssueQtyProCost,0)/isnull(IssueQtyPro,0)as decimal(18,2)) end as Rate ,Left(Item_Code,2) as item_type,* from (
                        select Item_Code,xxx.Location_Code,maX(Item_Desc)Item_Desc,max(CONuom)CONuom
                        ,cast(sum(xxx.StockQTYY * RI * case when Punching_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end ) as decimal(18,2)) as OPQty
                        ,cast(sum(xxx.Avg_Cost * RI * case when Punching_Date<'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end )as decimal(18,0)) as OPCOST
                        ,cast(sum(xxx.StockQTYY * (case when RI=1 then 1 else 0 end) * case when Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end )as decimal(18,2)) as RecQty,
                        cast(sum(xxx.Avg_Cost * (case when RI=1 then 1 else 0 end) * case when Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end )as decimal(18,0)) as RecCost
                        ,cast(sum(xxx.StockQTYY * (case when RI=-1 then 1 else 0 end) * (case when Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when Trans_Type in ('STD_PRO_ENT') then 1 else 0 end)) as decimal(18,2)) as IssueQtyPro,
                        cast(sum(xxx.Avg_Cost * (case when RI=-1 then 1 else 0 end) * (case when Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when Trans_Type in ('STD_PRO_ENT') then 1 else 0 end)) as decimal(18,0)) as IssueQtyProCost
                        ,cast(sum(xxx.StockQTYY * (case when RI=-1 then 1 else 0 end) * (case when Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when Trans_Type in ('ScrapIn') then 1 else 0 end)) as decimal(18,2)) as IssueQtyScrap,
                        cast(sum(xxx.Avg_Cost * (case when RI=-1 then 1 else 0 end) * (case when Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when Trans_Type in ('ScrapIn') then 1 else 0 end)) as decimal(18,0)) as IssueQtyScrapCost
                        ,cast(sum(xxx.StockQTYY * (case when RI=-1 then 1 else 0 end) * (case when Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when Trans_Type  in ('STD_PRO_ENT','ScrapIn') then 0 else 1 end)) as decimal(18,2)) as IssueOthQty,
                        cast(sum(xxx.Avg_Cost * (case when RI=-1 then 1 else 0 end) * (case when Punching_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end) * (case when Trans_Type  in ('STD_PRO_ENT','ScrapIn') then 0 else 1 end)) as decimal(18,0)) as IssueOthQtyCost
                        ,cast(sum(xxx.StockQTYY * RI) as decimal(18,2)) as CLQty,
                        cast(sum(xxx.Avg_Cost * RI  ) as decimal(18,0)) as CLQtyCost
                        from (
                        select Avg_Cost,(case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then TSPL_INVENTORY_MOVEMENT.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then TSPL_INVENTORY_MOVEMENT.LIFO_Cost else TSPL_INVENTORY_MOVEMENT.Avg_Cost end end ) as Cost,Basic_Cost,TSPL_INVENTORY_MOVEMENT.Item_Desc,TSPL_INVENTORY_MOVEMENT.Item_Code,Trans_Type,Punching_Date,Location_Code,Stock_UOM,case when TSPL_INVENTORY_MOVEMENT.InOut='I' then 1 else -1 end as RI,(case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.UOM_Code else ConvertedUnits.UOM_Code end) as CONuom,
					  TSPL_INVENTORY_MOVEMENT.UOM,ConvertedUnit.Conversion_Factor,  ( Stock_Qty*ConvertedUnit.Conversion_Factor/(case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.Conversion_Factor else ConvertedUnits.Conversion_Factor end)) as StockQTYY from TSPL_INVENTORY_MOVEMENT
		              left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitP on ConvertedUnitp.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code and 
					 isnull(ConvertedUnitP.processLoss_Uom,0)=1
					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitS on ConvertedUnitS.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code and 
					 ConvertedUnitS.Stocking_Unit='Y'
					 					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit on ConvertedUnit.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code and ConvertedUnit.UOM_Code=TSPL_INVENTORY_MOVEMENT.Stock_UOM
										 left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code
										   left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                    where  Punching_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtTodate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                    and TSPL_INVENTORY_MOVEMENT.Item_Code in (  select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE 
                    from TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
                    left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                    left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE
                    left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
                    left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE
                    where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate((txtFromDate.Value), "dd/MMM/yyyy") + "',103) and  CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate((txtTodate.Value), "dd/MMM/yyyy") + "',103) and TSPL_SPP_PRODUCTION_ENTRY.LOCATION_CODE='" + txtLoc.Value + "' and TSPL_ITEM_MASTER.FG_for_CF_PL=1
                    UNION 
                    SELECT TSPL_ITEM_MASTER.Item_Code FROM TSPL_ITEM_MASTER 
                    LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code

                    WHERE TSPL_ITEM_UOM_DETAIL.Net_Weight>0) 
                    )xxx  where Location_Code='" + txtLoc.Value + "' group by xxx.Item_Code,xxx.Location_Code)YYY


  

 "

                dt = clsDBFuncationality.GetDataTable(Qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        gv1.Rows.AddNew()
                        ' gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = False
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemType).Value = clsCommon.myCstr(dr("Item_type"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colitemcode).Value = clsCommon.myCstr(dr("Item_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colitemname).Value = clsCommon.myCstr(dr("Item_Desc"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(coluom).Value = clsCommon.myCstr(dr("CONuom"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colopQty).Value = clsCommon.myCstr(dr("OPQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colopAmt).Value = clsCommon.myCstr(dr("OPCost"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRecQty).Value = clsCommon.myCstr(dr("RecQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRecAmt).Value = clsCommon.myCstr(dr("RecCost"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIssProdQty).Value = clsCommon.myCstr(dr("IssueQtyPro"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIssProdAmt).Value = clsCommon.myCstr(dr("IssueQtyProCost"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOtherIssQty).Value = clsCommon.myCstr(dr("IssueOthQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOtherIssAmt).Value = clsCommon.myCstr(dr("IssueOthQtyCost"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colClQty).Value = clsCommon.myCstr(dr("CLQty"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colClAmt).Value = clsCommon.myCstr(dr("CLQtyCost"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colStockTransferQty).Value = clsCommon.myCstr(dr("IssueQtyScrap"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colStockTransferAmt).Value = clsCommon.myCstr(dr("IssueQtyScrapCost"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCstr(dr("Rate"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFinalClStock).Value = clsCommon.myCstr(dr("CLQty"))
                    Next
                Else
                    clsCommon.MyMessageBoxShow(Me, "No data found to display ", Me.Text)
                End If
                Gv2.Rows.Clear()
                Dim Query As String = " SELECT TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE,Item_Desc,SUM(FINAL_PRODUCTION_QTY)/100 AS QTY FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
                                         LEFT OUTER JOIN TSPL_SPP_PRODUCTION_ENTRY ON TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                                         INNER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Item_Code AND FG_for_CF_PL=1 AND TSPL_SPP_PRODUCTION_ENTRY.POSTED=1
                                         WHERE convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' ,103) AND convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(txtTodate.Value) + "' ,103)
                                         AND TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE IN ('" + txtLoc.Value + "') GROUP BY  TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE,Item_Desc"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Query)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each drp As DataRow In dt1.Rows
                        Gv2.Rows.AddNew()
                        ' gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = False
                        Gv2.Rows(Gv2.Rows.Count - 1).Cells(colitemcodeProduction).Value = clsCommon.myCstr(drp("ITEM_CODE"))
                        Gv2.Rows(Gv2.Rows.Count - 1).Cells(colitemnameProduction).Value = clsCommon.myCstr(drp("Item_Desc"))
                        Gv2.Rows(Gv2.Rows.Count - 1).Cells(colQtyProduction).Value = clsCommon.myCstr(drp("QTY"))
                    Next
                End If
                CostofFeed()
                txtFromDate.Enabled = False
                txtTodate.Enabled = False

            End If
            ' End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            dt = Nothing
        End Try
    End Sub
    Sub CostofFeed()
        Dim arrpd As New List(Of String)
        Dim arr As New List(Of String)
        Dim icode As String = ""
        Dim status As Integer = 0
        Dim amtforFG1 As Double = 0
        Dim code1 As String = ""
        Dim Itemcode1 As String = ""
        Dim PLAvg As Decimal = 0
        Dim AvgPer As Double = 0
        Dim cost As Double = 0
        Dim ProdQty As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Itemcode1 = clsCommon.myCstr(gv1.Rows(ii).Cells(colitemcode).Value)
            code1 = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemType).Value)
            If clsCommon.CompairString(code1, "RM") = CompairStringResult.Equal OrElse clsCommon.CompairString(code1, "FG") = CompairStringResult.Equal Then
                If clsCommon.CompairString(code1, "FG") = CompairStringResult.Equal Then
                    amtforFG1 = clsDBFuncationality.getSingleValue(" select isnull(SFG_for_CF,0)SFG_for_CF from TSPL_ITEM_MASTER where Item_Code='" + Itemcode1 + "' ")
                End If
                If clsCommon.myCdbl(amtforFG1) > 0 Then
                    Dim PL As Decimal = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colIssProdAmt).Value)
                    If PL > 0 Then
                        PLAvg += clsCommon.myCDecimal(gv1.Rows(ii).Cells(colIssProdAmt).Value)
                        'PLAvg += PLAvg
                    End If
                Else
                    Dim PL As Decimal = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colIssProdAmt).Value)
                    If PL > 0 Then
                        PLAvg += clsCommon.myCDecimal(gv1.Rows(ii).Cells(colIssProdAmt).Value)
                        'PLAvg += PLAvg
                    End If
                End If
            End If
        Next
        For jj As Integer = 0 To Gv2.Rows.Count - 1
            cost = clsCommon.myCdbl(Gv2.Rows(jj).Cells(colQtyProduction).Value)
            If cost > 0 Then
                ProdQty += clsCommon.myCDecimal(Gv2.Rows(jj).Cells(colQtyProduction).Value)
            End If
        Next
        If ProdQty > 0 Then
            AvgPer = PLAvg / ProdQty
            If AvgPer > 0 Then
                txtCostofFeed.Text = Math.Round(AvgPer, 2)
            Else
                txtCostofFeed.Text = 0
            End If
        End If

        Dim code As String = ""
        Dim Itemcode As String = ""
        Dim status1 As Integer = 0
        Dim amtforFG As Double = 0
        Dim PLAvg1 As Decimal = 0
        Dim AvgPer1 As Decimal = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Itemcode = clsCommon.myCstr(gv1.Rows(ii).Cells(colitemcode).Value)
            code = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemType).Value)
            If clsCommon.CompairString(code, "RM") = CompairStringResult.Equal OrElse clsCommon.CompairString(code, "FG") = CompairStringResult.Equal Then
                If clsCommon.CompairString(code, "FG") = CompairStringResult.Equal Then
                    amtforFG = clsDBFuncationality.getSingleValue(" select isnull(SFG_for_CF,0)SFG_for_CF from TSPL_ITEM_MASTER where Item_Code='" + Itemcode + "' ")
                End If
                If clsCommon.myCdbl(amtforFG) > 0 Then
                    Dim PL As Decimal = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colIssProdAmt).Value)
                    If PL > 0 Then
                        PLAvg1 += clsCommon.myCDecimal(gv1.Rows(ii).Cells(colIssProdAmt).Value)
                        'PLAvg += PLAvg
                    End If
                Else
                    Dim PL As Decimal = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colIssProdAmt).Value)
                    If PL > 0 Then
                        PLAvg1 += clsCommon.myCDecimal(gv1.Rows(ii).Cells(colIssProdAmt).Value)
                        'PLAvg += PLAvg
                    End If
                End If
                End If
        Next
        If PLAvg1 > 0 Then
            txtITotalssQty.Text = PLAvg1
        End If
    End Sub
    Private Sub UpdateCurrentRow(ByVal grow As GridViewRowInfo)
        Try
            Dim arrTaxableAuth As New List(Of String)
            If clsCommon.myCDecimal(grow.Cells(colPLQty).Value) > 0 Then
                Dim dblFAmt As Double = 0
                Dim PLQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPLQty).Value)
                Dim Cl As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colClQty).Value)
                Dim IssProd As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colIssProdQty).Value)

                ' Check to avoid division by zero
                If PLQty <> 0 Then
                    isInsideLoadData = True
                    Dim PLPer As Double = (PLQty * 100) / IssProd
                    gv1.CurrentRow.Cells(colPLper).Value = PLPer
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                'If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gv1.Columns(colPLQty) Then
                    Dim PlQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPLQty).Value)
                    Dim IssProd As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colIssProdQty).Value)
                    If IssProd > 0 AndAlso PlQty > 0 Then
                        gv1.CurrentRow.Cells(colPLper).Value = (PlQty * 100) / (IssProd)
                    Else
                        gv1.CurrentRow.Cells(colPLper).Value = 0
                    End If
                    If clsCommon.myCdbl(gv1.CurrentRow.Cells(colPLper).Value) > 0 Then
                        gv1.CurrentRow.Cells(colFinalClStock).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colClQty).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colPLQty).Value)
                    Else
                        gv1.CurrentRow.Cells(colFinalClStock).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colClQty).Value)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Sub AddNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtTodate.Value = clsCommon.GETSERVERDATE()
        txtLoc.Value = ""
        lblloc.Text = ""
        isNewEntry = True
        txtFromDate.Enabled = True
        txtTodate.Enabled = True
        txtDocNo.Value = ""
        gv1.DataSource = Nothing
        LoadBlankGrid()
        LoadBlankGridProduction()
        btnReverse.Visible = False
        txtCostofFeed.Text = ""
        txtITotalssQty.Text = ""
        docDate.Value = clsCommon.GETSERVERDATE()
        gv1.MasterTemplate.FilterDescriptors.Clear()
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnDelete.Enabled = False
        btnSave.Text = "Save"
        txtComments.Text = ""
        gv1.MasterTemplate.FilterDescriptors.Clear()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtCostofFeed.Text = ""
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Private Sub SaveData(ByVal isPost As Boolean)
        Dim obj As New clsRMProcessLoss()
        Dim objpd As New ClsRmProcessLossDetail
        Try
            If AllowToSave() Then
                obj.document_code = clsCommon.myCstr(txtDocNo.Value)
                obj.document_date = clsCommon.myCDate(docDate.Value)
                obj.Location = clsCommon.myCstr(txtLoc.Value)
                obj.Comments = clsCommon.myCstr(txtComments.Text)
                obj.Fromdate = clsCommon.myCDate(txtFromDate.Value)
                obj.Todate = clsCommon.myCDate(txtTodate.Value)
                obj.Arr_Pd = New List(Of ClsRmProcessLossDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    objpd = New ClsRmProcessLossDetail()
                    objpd.item_code = (clsCommon.myCstr(grow.Cells(colitemcode).Value))
                    objpd.UOM = clsCommon.myCstr(grow.Cells(coluom).Value)
                    objpd.OP_Qty = clsCommon.myCDecimal(grow.Cells(colopQty).Value)
                    objpd.OP_Cost = clsCommon.myCdbl(grow.Cells(colopAmt).Value)
                    objpd.Rec_Qty = clsCommon.myCDecimal(grow.Cells(colRecQty).Value)
                    objpd.Rec_Cost = clsCommon.myCdbl(grow.Cells(colRecAmt).Value)
                    objpd.IssProd_Qty = clsCommon.myCDecimal(grow.Cells(colIssProdQty).Value)
                    objpd.IssProd_Cost = clsCommon.myCdbl(grow.Cells(colIssProdAmt).Value)
                    objpd.OtherIss_Qty = clsCommon.myCDecimal(grow.Cells(colOtherIssQty).Value)
                    objpd.OtherIss_Cost = clsCommon.myCdbl(grow.Cells(colOtherIssAmt).Value)
                    objpd.StkTrns_Qty = clsCommon.myCDecimal(grow.Cells(colStockTransferQty).Value)
                    objpd.StkTrns_Cost = clsCommon.myCdbl(grow.Cells(colStockTransferAmt).Value)
                    objpd.PL_Qty = clsCommon.myCDecimal(grow.Cells(colPLQty).Value)
                    objpd.PL_Per = clsCommon.myCDecimal(grow.Cells(colPLper).Value)
                    objpd.Rate = clsCommon.myCDecimal(grow.Cells(colRate).Value)
                    objpd.FnlStk_Qty = clsCommon.myCdbl(grow.Cells(colFinalClStock).Value)
                    objpd.CL_Qty = clsCommon.myCDecimal(grow.Cells(colClQty).Value)
                    objpd.Cl_Cost = clsCommon.myCdbl(grow.Cells(colClAmt).Value)
                    If clsCommon.myLen(objpd.item_code) > 0 Then
                        obj.Arr_Pd.Add(objpd)
                    End If
                Next
                If clsRMProcessLoss.SaveData(obj, isNewEntry) Then
                    If Not isPost Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
                    End If
                    LoadData(obj.document_code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            objpd = Nothing
            obj = Nothing
        End Try
    End Sub
    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsRMProcessLoss()
        Try
            AddNew()
            obj = clsRMProcessLoss.GetData(strCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.document_code) > 0 Then
                isInsideLoadData = True
                isNewEntry = False
                txtDocNo.Value = obj.document_code
                docDate.Value = obj.document_date
                txtLoc.Value = obj.Location
                lblloc.Text = obj.Locationdesc
                txtComments.Text = obj.Comments
                txtFromDate.Value = obj.Fromdate
                txtTodate.Value = obj.Todate
                UsLock1.Status = ERPTransactionStatus.Pending
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                btnPost.Enabled = True
                If obj.Status = 1 Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
                txtFromDate.Enabled = False
                txtTodate.Enabled = False
                If obj.Arr_Pd IsNot Nothing AndAlso obj.Arr_Pd.Count > 0 Then
                    For Each objtr As ClsRmProcessLossDetail In obj.Arr_Pd
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colitemcode).Value = objtr.item_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colitemname).Value = objtr.item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(coluom).Value = objtr.UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemType).Value = clsDBFuncationality.getSingleValue("select left(item_code,2)item_code from TSPL_ITEM_MASTER where Item_Code='" + objtr.item_code + "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colopQty).Value = objtr.OP_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colopAmt).Value = objtr.OP_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRecQty).Value = objtr.Rec_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRecAmt).Value = objtr.Rec_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIssProdQty).Value = objtr.IssProd_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIssProdAmt).Value = objtr.IssProd_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOtherIssQty).Value = objtr.OtherIss_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOtherIssAmt).Value = objtr.OtherIss_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colStockTransferQty).Value = objtr.StkTrns_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colStockTransferAmt).Value = objtr.StkTrns_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colClQty).Value = objtr.CL_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colClAmt).Value = objtr.Cl_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPLQty).Value = objtr.PL_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPLper).Value = objtr.PL_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objtr.Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFinalClStock).Value = objtr.FnlStk_Qty
                    Next
                End If
                Gv2.Rows.Clear()
                Dim Query As String = " SELECT TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE,Item_Desc,SUM(FINAL_PRODUCTION_QTY)/100 AS QTY FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
                                         LEFT OUTER JOIN TSPL_SPP_PRODUCTION_ENTRY ON TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                                         INNER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Item_Code AND FG_for_CF_PL=1 AND TSPL_SPP_PRODUCTION_ENTRY.POSTED=1
                                         WHERE convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(obj.Fromdate) + "' ,103) AND convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(obj.Todate) + "' ,103)
                                         AND TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE IN ('" + obj.Location + "') GROUP BY  TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE,Item_Desc"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Query)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For Each drp As DataRow In dt1.Rows
                        Gv2.Rows.AddNew()
                        ' gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = False
                        Gv2.Rows(Gv2.Rows.Count - 1).Cells(colitemcodeProduction).Value = clsCommon.myCstr(drp("ITEM_CODE"))
                        Gv2.Rows(Gv2.Rows.Count - 1).Cells(colitemnameProduction).Value = clsCommon.myCstr(drp("Item_Desc"))
                        Gv2.Rows(Gv2.Rows.Count - 1).Cells(colQtyProduction).Value = clsCommon.myCstr(drp("QTY"))
                    Next
                End If
                CostofFeed()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim whrClas As String = ""
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrClas = " Location in (" + objCommonVar.strCurrUserLocations + ") "
            End If
            Dim qry As String = "   select Document_Code as Code,convert(varchar,Document_Date,103)DocumentDate,Case when status=0 then 'Pending' else 'Approved' end as 'Status',convert(varchar,from_date,103)FromDate,convert(varchar,to_date,103)ToDate from TSPL_RM_PROCESS_LOSS "
            LoadData(clsCommon.ShowSelectForm("OutgoingQC", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_RM_PROCESS_LOSS where Document_Code ='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If myMessages.postConfirm() Then
                If (clsRMProcessLoss.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (clsRMProcessLoss.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Function AllowToSave() As Boolean 'ByVal isPost As Boolean
        Try
            Dim obj As New clsRMProcessLoss()
            If AllowFutureDateTransaction(docDate.Value, Nothing) = False Then
                docDate.Focus()
                Return False
            End If
            If AllowFutureDateTransaction(txtFromDate.Value, Nothing) = False Then
                txtFromDate.Focus()
                Return False
            End If
            If AllowFutureDateTransaction(txtTodate.Value, Nothing) = False Then
                txtTodate.Focus()
                Return False
            End If
            If clsCommon.myLen(txtLoc.Value) <= 0 Then
                txtLoc.Focus()
                txtLoc.Select()
                clsCommon.MyMessageBoxShow(Me, "Select Location", Me.Text)
                Return False
            End If
            Dim arrpd As New List(Of String)
            Dim arr As New List(Of String)
            Dim icode As String = ""
            Dim status As Integer = 0
            Dim PLAvg As Decimal = 0
            Dim AvgPer As Decimal = 0
            Dim per As Decimal
            For ii As Integer = 0 To gv1.Rows.Count - 1
                icode = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemType).Value)
                If clsCommon.CompairString(icode, "RM") = CompairStringResult.Equal Then
                    Dim PL As Decimal = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colPLper).Value)
                    If PL > 0 Then
                        status += 1
                        PLAvg += clsCommon.myCDecimal(gv1.Rows(ii).Cells(colPLper).Value)
                    End If
                End If
                arrpd.Add(icode)
            Next
            If PLAvg > 0 AndAlso status > 0 Then
                AvgPer = PLAvg / status
                per = Math.Round(AvgPer, 2)
            End If
            If AvgPer > 0.6 Then
                If clsCommon.MyMessageBoxShow(Me, "The average process loss %age of RM material is greater " + clsCommon.myCstr(per) + " than allowed %age 0.60" + Environment.NewLine + "Do you want to save ?", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnReverse.Visible = False
    End Sub
    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsRMProcessLoss.ReverseAndUnpost(txtDocNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub frmRMProcessLoss_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "sirc"
                frm.strCode = "sireversandcreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
            End If
        End If
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PrintView(True)
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtTodate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Location : " + clsCommon.myCstr(txtLoc.Value))

            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("C - Form Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Export(EnumExportTo.Excel)
    End Sub
    Sub PrintView(ByVal Detail As Boolean)
        Dim dt As DataTable = Nothing
        Dim dt1 As DataTable = Nothing
        Dim PLPERC As Decimal
        Dim dt2 As DataTable = Nothing
        Dim arrpd As New List(Of String)
        Dim arr As New List(Of String)

        Dim icode As String = ""
        Dim status As Integer = 0
        Dim PLAvg As Decimal = 0
        Dim AvgPer As Decimal = 0
        Try
            For ii As Integer = 0 To gv1.Rows.Count - 1
                icode = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemType).Value)
                If clsCommon.CompairString(icode, "RM") = CompairStringResult.Equal Then
                    Dim PL As Decimal = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colPLper).Value)
                    If PL > 0 Then
                        status += 1
                        PLAvg += clsCommon.myCDecimal(gv1.Rows(ii).Cells(colPLper).Value)
                    End If
                End If
                arrpd.Add(icode)
            Next
            If PLAvg > 0 AndAlso status > 0 Then
                AvgPer = PLAvg / status
                PLPERC = Math.Round(AvgPer, 2)
            End If
            Dim frmCRV As New frmCrystalReportViewer()
            Dim qry As String = "select '" + txtCostofFeed.Text + "' as CostOfRM,'" + clsCommon.myCstr(PLPERC) + "' as AvgPer,left(tspl_rm_process_loss_detail.item_code,2) as itemtype,from_date,to_Date,location,tspl_rm_process_loss.document_code,document_date,tspl_rm_process_loss_detail.item_code,item_desc,tspl_rm_process_loss_detail.uom,op_qty,op_cost,rec_qty,rec_cost,cl_qty,cl_cost,issprod_qty,issprod_cost,otheriss_qty,otherIss_cost,stktrns_qty,stktrns_cost,pl_qty,pl_per,tspl_rm_process_loss_detail.rate,fnlstk_qty from tspl_rm_process_loss_detail
            left outer join tspl_rm_process_loss on tspl_rm_process_loss.document_code=tspl_rm_process_loss_detail.document_code
            left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=tspl_rm_process_loss_detail.item_code where tspl_rm_process_loss.document_code='" + txtDocNo.Value + "' and tspl_rm_process_loss_detail.uom<>'NOS'"
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim query As String = "select left(tspl_rm_process_loss_detail.item_code,2) as itemtype,from_date,to_Date,location,tspl_rm_process_loss.document_code,document_date,tspl_rm_process_loss_detail.item_code,item_desc,tspl_rm_process_loss_detail.uom,op_qty,op_cost,rec_qty,rec_cost,cl_qty,cl_cost,issprod_qty,issprod_cost,otheriss_qty,otherIss_cost,stktrns_qty,stktrns_cost,pl_qty,pl_per,tspl_rm_process_loss_detail.rate,fnlstk_qty from tspl_rm_process_loss_detail
            left outer join tspl_rm_process_loss on tspl_rm_process_loss.document_code=tspl_rm_process_loss_detail.document_code
            left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=tspl_rm_process_loss_detail.item_code where tspl_rm_process_loss.document_code='" + txtDocNo.Value + "' and tspl_rm_process_loss_detail.uom='NOS'"
            dt1 = clsDBFuncationality.GetDataTable(query)
            Dim strqry As String = " SELECT 'Qtl' as UOM,TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE,Item_Desc,SUM(FINAL_PRODUCTION_QTY)/100 AS QTY FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
                                         LEFT OUTER JOIN TSPL_SPP_PRODUCTION_ENTRY ON TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                                         INNER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Item_Code AND FG_for_CF_PL=1 AND TSPL_SPP_PRODUCTION_ENTRY.POSTED=1
                                         WHERE convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)>=convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' ,103) AND convert(date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103)<=convert(date,'" + clsCommon.GetPrintDate(txtTodate.Value) + "' ,103)
                                         AND TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE IN ('" + txtLoc.Value + "') GROUP BY  TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE,Item_Desc"
            dt2 = clsDBFuncationality.GetDataTable(strqry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                If Detail = True Then
                    frmCRV.funsubreportWithdt(Nothing, CrystalReportFolder.SalesReport, dt, dt1, "rptRMProcessLossreport", "", Nothing, "PMstockConsumption.rpt", "Production.rpt", dt2)
                Else
                    frmCRV.funsubreportWithdt(Nothing, CrystalReportFolder.SalesReport, dt, dt1, "rptRMProcessLossreportDetail", "", Nothing, "PMstockConsumption.rpt", "Production.rpt", dt2)

                End If
            Else
                    common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrintDetail_Click(sender As Object, e As EventArgs) Handles btnPrintDetail.Click
        PrintView(False)
    End Sub
End Class