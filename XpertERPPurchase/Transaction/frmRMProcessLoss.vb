Imports common
Public Class frmRMProcessLoss
    Inherits FrmMainTranScreen
    Const colSno As String = "Sno"
    Const colItemType As String = "ItemType"
    Const colitemcode As String = "ItemCode"
    Const colitemname As String = "itemname"
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
    Dim isNewEntry As Boolean = True
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
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoStr As New GridViewTextBoxColumn()
        Dim repoInt As New GridViewDecimalColumn()
        Dim repoChk As New GridViewCheckBoxColumn()
        Dim repoDate As New GridViewDateTimeColumn()


        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "S.No."
        repoStr.Name = colSno
        repoStr.Width = 70
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoStr)

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

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = "{0:F2}"
        repoStr.HeaderText = "Quantity"
        repoStr.Name = colopQty
        repoStr.Width = 70
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Amount"
        repoStr.Name = colopAmt
        repoStr.Width = 90
        repoStr.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = "{0:F2}"
        repoStr.HeaderText = "Quantity"
        repoStr.Name = colRecQty
        repoStr.Width = 70
        repoStr.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Amount"
        repoStr.Name = colRecAmt
        repoStr.Width = 90
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = "{0:F2}"
        repoStr.HeaderText = "Quantity"
        repoStr.Name = colIssProdQty
        repoStr.Width = 70
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Amount"
        repoStr.Name = colIssProdAmt
        repoStr.Width = 90
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = "{0:F2}"
        repoStr.HeaderText = "Quantity"
        repoStr.Name = colOtherIssQty
        repoStr.Width = 70
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Amount"
        repoStr.Name = colOtherIssAmt
        repoStr.Width = 90
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = "{0:F2}"
        repoStr.HeaderText = "Quantity"
        repoStr.Name = colStockTransferQty
        repoStr.Width = 70
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Amount"
        repoStr.Name = colStockTransferAmt
        repoStr.Width = 90
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)


        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = "{0:F2}"
        repoStr.HeaderText = "Quantity"
        repoStr.Name = colClQty
        repoStr.Width = 70
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Amount"
        repoStr.Name = colClAmt
        repoStr.Width = 90
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = "{0:F2}"
        repoStr.HeaderText = "Quantity"
        repoStr.Name = colPLQty
        repoStr.Width = 70
        repoStr.ReadOnly = False
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = "{0:F2}"
        repoStr.HeaderText = "%AGE"
        repoStr.Name = colPLper
        repoStr.Width = 90
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = "{0:F2}"
        repoStr.HeaderText = "Rate"
        repoStr.Name = colRate
        repoStr.Width = 70
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = "{0:F2}"
        repoStr.HeaderText = "Final"
        repoStr.Name = colFinalClStock
        repoStr.Width = 90
        repoStr.ReadOnly = True
        repoStr.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoStr)



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
            'Else
            '    WhrCls += "  and  Location_Code in ('RCDF')"
        End If
        txtLoc.Value = clsCommon.ShowSelectForm("ShipmentMasteidfndr", qry, "Code", WhrCls, txtLoc.Value, "Code", isButtonClicked)
        lblloc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLoc.Value + "'"))
    End Sub

    Private Sub frmRMProcessLoss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddNew()
        LoadBlankGrid()
        SetUserMgmtNew()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)
        coll.Add("Document_Code", "varchar(30) NOT NULL Primary Key")
        coll.Add("Document_Date", "datetime  NULL")
        coll.Add("Comment", "varchar(30) NULL")
        coll.Add("Location", "varchar(12) null references TSPL_LOCATION_MASTER(Location_Code)")
        coll.Add("From_Date", "datetime  NULL")
        coll.Add("To_Date", "datetime  NULL")
        coll.Add("Created_By", "varchar(12)   NULL")
        coll.Add("Created_Date", "datetime   NULL")
        coll.Add("Modify_By", "varchar(12)   NULL")
        coll.Add("Modify_Date", "datetime   NULL")
        coll.Add("Posted_By", "varchar(12)   NULL")
        coll.Add("Posted_Date", "datetime   NULL")
        coll.Add("Status", "integer not null default 0")
        clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_RM_PROCESS_LOSS", coll, "", True)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "varchar(30) Not NULL References TSPL_RM_PROCESS_LOSS(Document_Code)")
        coll.Add("Item_Code", "Varchar(50) Not NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Uom", "varchar(30) null")
        coll.Add("OP_Qty", "decimal(18,2)  Null")
        coll.Add("OP_Cost", "decimal(18,2)  Null")
        coll.Add("Rec_Qty", "decimal(18,2)  Null")
        coll.Add("Rec_Cost", "decimal(18,2)  Null")
        coll.Add("IssProd_Qty", "decimal(18,2) Null")
        coll.Add("IssProd_Cost", "decimal(18,2)  Null")
        coll.Add("OtherIss_Qty", "decimal(18,2) Null")
        coll.Add("OtherIss_Cost", "decimal(18,2)  Null")
        coll.Add("StkTrns_Qty", "decimal(18,2)  Null")
        coll.Add("StkTrns_Cost", "decimal(18,2)  Null")
        coll.Add("CL_Qty", "decimal(18,2)  Null")
        coll.Add("Cl_Cost", "decimal(18,2)  Null")
        coll.Add("PL_Qty", "decimal(18,2)  Null")
        coll.Add("PL_Per", "decimal(18,2)  Null")
        coll.Add("Rate", "decimal(18,2)  Null")
        coll.Add("FnlStk_Qty", "decimal(18,2) Not Null")
        clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_RM_PROCESS_LOSS_DETAIL", coll, "", True)
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
        Slot3TD = clsCommon.GetPrintDate(currentDate.AddMonths(1).AddDays(-1), "dd/MMM/yyyy")

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim dt As New DataTable
        Try
            gv1.Rows.Clear()
            Dim Qry As String = "  select ROW_NUMBER() OVER (ORDER BY Item_Code) AS SNo,left(final1.Item_Code,2)Item_type,final1.Item_Code,final1.Item_Desc,max(final1.conuom)conuom,
                                                                                                                           CAST(
    CASE 
        WHEN max(final1.Stock_UOM) = 'KG' THEN sum(final1.OPQty) / 1000.0
        ELSE CASE 
            WHEN max(final1.Stock_UOM) = 'GM' THEN sum(final1.OPQty) / 10000.0
            ELSE sum(final1.OPQty) 
        END 
    END 
AS DECIMAL(18,2)) AS OPQty,

                                                sum(final1.OPRate)      as OPRate,cast(sum(final1.OPCost) as decimal(18,0)) as  OPCost,           
												cast(case when max(final1.Stock_UOM)='KG' THEN sum(final1.RecPurQty)/1000 ELSE CASE WHEN max(final1.Stock_UOM)='GM' THEN sum(final1.RecPurQty)/10000  ELSE sum(final1.RecPurQty)  END END as Decimal(18,2)) as RecPurQty,
                                                sum(final1.RecPurRate)  as RecPurRate,cast(sum(final1.RecPurCost) as decimal(18,0)) as RecPurCost,case when max(final1.Stock_UOM)='KG' THEN sum(final1.Issqty)/1000    ELSE CASE WHEN max(final1.Stock_UOM)='GM' THEN sum(final1.Issqty)/10000     ELSE sum(final1.Issqty)     END END as Issqty,
												cast(case when max(final1.Stock_UOM)='KG' THEN sum(final1.ProdIssQty)/1000    ELSE CASE WHEN max(final1.Stock_UOM)='GM' THEN sum(final1.ProdIssQty)/10000     ELSE sum(final1.ProdIssQty)     END END as decimal(18,2)) as ProdIssQty ,
                                                cast(sum(final1.IssRate) as decimal(18,2))     as IssRate,sum(final1.IssuCost) as IssuCost,      
												cast(case when max(final1.Stock_UOM)='KG' THEN Sum(final1.CLQty)/1000     ELSE CASE WHEN max(final1.Stock_UOM)='GM' THEN Sum(final1.CLQty)/10000      ELSE Sum(final1.CLQty)      END END as decimal(18,2)) as CLQty,
                                                sum(final1.CLRate)      as CLRate,cast(sum(final1.CLCost) as decimal(18,0)) as CLCost ,
                                              cast(sum(final1.IssRate)*sum(final1.ProdIssQty) as decimal(18,0)) as ProdIssCost,
												
												cast(case when max(final1.Stock_UOM)='KG' THEN sum(final1.otherIssQty)/1000    ELSE CASE WHEN max(final1.Stock_UOM)='GM' THEN sum(final1.otherIssQty)/10000     ELSE sum(final1.otherIssQty)     END END as decimal(18,2)) as OtherIssQty
												,cast(sum(OtherIssQty)*sum(IssRate) as decimal(18,0)) as OtherIssCost,
												cast(case when max(final1.Stock_UOM)='KG' THEN sum(final1.ScrabSaleQty)/1000    ELSE CASE WHEN max(final1.Stock_UOM)='GM' THEN sum(final1.ScrabSaleQty)/10000     ELSE sum(final1.ScrabSaleQty)     END END as decimal(18,2)) as ScrabSaleQty, cast(sum(final1.IssRate)*sum(final1.ScrabSaleQty) as decimal(18,0)) as ScrabSaleAmt from ( 
                                                select convert(varchar, Punching_Date,103) as Punching_Date  ,Item_Code ,Item_Desc,Stock_UOM, 
                                                case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and 0=0) ) then 0 else  Convert(decimal(18,3),(ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))) end as OPQty ,OPRate,OPCost ,
                                                0 as RecPurQty,0 as RecPurRate,0 as RecPurCost,0 as Issqty, 0 as IssRate, 0 as IssuCost,0 AS CLQty,0 AS CLRate,0 AS CLCost,0 as ProdIssQty,0 as OtherIssQty, 0 as ScrabSaleQty,0 as Stock_qty
                                                ,(FINAL.HeadName) AS HeadName,(FINAL.Location_Code) AS Location_Code,(FINAL.[Loc Desp]) AS [Loc Desp],(FINAL.Add1) AS Add1,(FINAL.Add2) AS Add2,(FINAL.Add3) AS Add3,(FINAL.Add4) AS Add4,conuom
                                                from (

                                                select * from 
                    (select 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName,xxxxxxx.Location_Code,[Loc Desp],Add1,Add2,Add3,Add4,convert(varchar, Punching_Date,103) as Punching_Date  ,Item_Code ,Item_Desc,Stock_UOM, case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else  Convert(decimal(18,3),(ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))) end as OPQty,
                    case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11) then 0 else  Convert(decimal(18,2),((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))))) end as OPRate ,case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else Convert(decimal(18,2),(isnull(CLCost,0)-isnull(RecCost,0)+ isnull(IssCost,0))) end as OPCost, RecPurQty,RecPurRate,RecPurCost ,RecProQty, RecProRate ,RecProCost,RecAdjQty,RecAdjRate ,RecAdjCost ,RecOthQty,RecOthRate ,RecOthCost,RecQty,RecRate,RecCost  ,IssTransferQty ,IssTransferRate ,IssTransferCost ,IssSaleQty ,IssSaleRate  ,IssSaleCost , IssIssAdjQty , IssIssAdjRate ,IssIssAdjCost , IssOthQty , IssOthRate ,IssOthCost ,IssQty,IssRate,IssCost ,case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or (ABS(isnull(cast(CLCost as decimal(18,2)),0))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else CLQty end as CLQty,
                    case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or ABS(isnull(cast(CLCost as decimal(18,2)),0))<0.11 ) then 0 else CLCost/CLQty end as CLRate, CLCost ,0 as ProdIssQty, 0  as OtherIssQty,0 as ScrabSaleQty,0 as Stock_qty,conuom
                     
                    from 
					( select  Location_Code,max([Loc Desp]) as [Loc Desp],Punching_Date as Punching_Date, Item_Code ,max(Item_Desc) as Item_Desc, (case when max(CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtTodate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN Stock_UOM ELSE '' end)<>'' then max(CASE WHEN PUNCHING_DAte >= '01/Dec/2023 12:00:00 AM'  AND PUNCHING_DAte <= '31/Dec/2023 11:59:59 PM' THEN Stock_UOM ELSE '' end) else MAX(Stock_UOM) end ) as Stock_UOM, sum(Balance_FAT) as Balance_FAT,sum(Balance_SNF) as Balance_SNF  ,sum(isnull(Balance_QTYKG,0)) as Balance_QTYKG ,sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end) as RecPurQty ,case when sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end)/sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end)) end as RecPurRate  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_FAT else 0 end) as RecPurFAT  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurFATPER  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_SNF else 0 end) as RecPurSNF  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurSNFPER  ,sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end) as RecPurCost  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end) as RecAdjQty   ,case when sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and Trans_Type='IC-AD' then Cost else 0 end)/sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end)) end as RecAdjRate  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Balance_FAT else 0 end) as RecAdjFAT  ,(case when sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end) end)  as RecAdjFATPER  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Balance_SNF else 0 end) as RecAdjSNF  ,(case when sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end) end)  as RecAdjSNFPER  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Cost else 0 end) as RecAdjCost , sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end) as RecProQty   ,case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Cost else 0 end)/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end)) end as RecProRate  ,sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Balance_FAT else 0 end) as RecProFAT  ,(case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end) end)  as RecProFATPER  ,sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Balance_SNF else 0 end) as RecProSNF  ,(case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end) end)  as RecProSNFPER  , sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Cost else 0 end) as RecProCost ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end) as RecOthQty  ,case when sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end)/sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end)) end as RecOthRate  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_FAT else 0 end) as RecOthFAT  ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthFATPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_SNF else 0 end) as RecOthSNF ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthSNFPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end) as RecOthCost ,sum(case when InOut='I' then Stock_Qty else 0 end) as RecQty  ,case when cast(sum(case when InOut='I' then Stock_Qty else 0 end)as  decimal(18,2))=0 then 0 else (sum(case when InOut='I' then Cost else 0 end)/sum(case when InOut='I' then Stock_Qty else 0 end)) end as RecRate  ,sum(case when InOut='I' then Balance_FAT else 0 end) as RecFAT,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecFATPER  ,sum(case when InOut='I' then Balance_SNF else 0 end) as RecSNF  ,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecSNFPER  ,sum(case when InOut='I' then Cost else 0 end) as RecCost
                    ,sum(case when InOut='I' then FATAmount else 0 end) as RecFATAmount 
                    ,sum(case when InOut='I' then SNFAmount else 0 end) as RecSNFAmount 
                      ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end) as IssSaleQty  ,case when sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Cost else 0 end)/sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end)) end as IssSaleRate  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Balance_FAT else 0 end) as IssSaleFAT  ,(case when sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end) end)  as IssSaleFATPER  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Balance_SNF else 0 end) as IssSaleSNF  ,(case when sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end) end)  as IssSaleSNFPER  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Cost else 0 end) as IssSaleCost  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end) as IssTransferQty  ,case when sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Cost else 0 end)/sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end)) end as IssTransferRate  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Balance_FAT else 0 end) as IssTransferFAT  ,(case when sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end) end)  as IssTransferFATPER  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Balance_SNF else 0 end) as IssTransferSNF  ,(case when sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end) end)  as IssTransferSNFPER  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Cost else 0 end) as IssTransferCost,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end) as IssIssAdjQty  ,case when sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Cost else 0 end)/sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end)) end as IssIssAdjRate  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Balance_FAT else 0 end) as IssIssAdjFAT  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjFATPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Balance_SNF else 0 end) as IssIssAdjSNF  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjSNFPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Cost else 0 end) as IssIssAdjCost ,sum(case when InOut=
					  'O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end) as IssOthQty  ,case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Cost else 0 end)/sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end)) end as IssOthRate  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Balance_FAT else 0 end) as IssOthFAT  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthFATPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Balance_SNF else 0 end) as IssOthSNF  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthSNFPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Cost else 0 end) as IssOthCost ,case when cast(sum(case when InOut='O' then -1.00*Stock_Qty else 0 end)as  decimal(18,2))=0 then 0 else (sum(case when InOut='O' then -1.00*Cost else 0 end)/sum(case when InOut='O' then -1.00*Stock_Qty else 0 end)) end as IssRate  ,sum(case when InOut='O' then -1.00*Balance_FAT else 0 end) as IssFAT  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssFATPER ,sum(case when InOut='O' then -1.00*Balance_SNF else 0 end) as IssSNF  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssSNFPER  ,sum(case when InOut='O' then -1.00*Cost else 0 end) as IssCost 
                    ,sum(case when InOut='O' then -1.00*FATAmount else 0 end) as IssFATAmount
                    ,sum(case when InOut='O' then -1.00*SNFAmount else 0 end) as IssSNFAmount
                     ,SUM(sum(Stock_Qty)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLQty   ,SUM(sum(Cost)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLCost,sum(case when InOut='O'   then -1.00*Stock_Qty else 0 end) as IssQty,SUM(sum(isnull(Balance_QTYKG,0))) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_QTYKG  ,SUM(sum(Balance_FAT)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_FAT ,SUM(sum(Balance_SNF)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLBalance_SNF 
                    ,SUM(sum(FATAmount)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLFATAmount 
                    ,SUM(sum(SNFAmount)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLSNFAmount ,max(Conuom) as conuom
                      from 
					  (select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,xxx.Location_Code ,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC],xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1.00 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(28,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF 
                    ,( sum((case when IsFromMilk=1 then Fat_Amt  else (Stock_Qty*FatPer*Fat_Rate) end) * case when InOut='I' then 1 else -1 end)) AS FATAmount
                    ,(sum((case when IsFromMilk=1 then SNF_Amt else (Stock_Qty*SNFPer*SNF_Rate) end ) * case when InOut='I' then 1 else -1 end)) AS SNFAmount 
                     ,   '' as In_Category,'' as Out_Category,max(Conuom)Conuom from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG,
					  InventroyMovement.Stock_UOM, 
					 (case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.UOM_Code else ConvertedUnits.UOM_Code end) as CONuom,InventroyMovement.UOM,ConvertedUnit.Conversion_Factor,  ( Stock_Qty*ConvertedUnit.Conversion_Factor/(case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.Conversion_Factor else ConvertedUnits.Conversion_Factor end)) as stock_qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
                    ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from 
					(  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor 
					from TSPL_INVENTORY_MOVEMENT 
                     union all 
                     select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW
                    ) InventroyMovement 
                     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
                     left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                     left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                     left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
                     left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) 
					 left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG'
					   left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitP on ConvertedUnitp.Item_Code=InventroyMovement.Item_Code and 
					 isnull(ConvertedUnitP.processLoss_Uom,0)=1
					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitS on ConvertedUnitS.Item_Code=InventroyMovement.Item_Code and 
					 ConvertedUnitS.Stocking_Unit='Y'
					 					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit on ConvertedUnit.Item_Code=InventroyMovement.Item_Code and ConvertedUnit.UOM_Code=InventroyMovement.Stock_UOM
				
					 left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No    left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC]  from (
                     select * from ( 
                     select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code 
                     ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc 
                     ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values 
                     ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc 
                     from  TSPL_ITEM_MASTER  
                     left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code 
                     left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values
                     where 2=2 
                     )xx
                     Pivot 
                     ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV])
                     ) Pivt
                     Pivot 
                     (
                     max(Category_Value_Desc) for Item_Category_CodeDesc in ([FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC])
                     ) Pivt1 
                     ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2    and Item_Code in (select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE
left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' ,103) and  CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate(txtTodate.Value) + "' ,103))    
                     and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtLoc.Value) + "') )) xxx 
                     where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' group by xxx.Item_Code,xxx.Location_Code 
                     union all  select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,cast(Punching_Date as date) as Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type, Item_Type_Name,Item_Group,Group_Description,[FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV],[FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC],Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code , ( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,  convert(decimal(28,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost, ( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,  ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF
                    ,(Fat_Amt * case when InOut='I' then 1 else -1 end) as FATAmount
                    ,(SNF_Amt * case when InOut='I' then 1 else -1 end) as SNFAmount
                     ,In_Category,Out_Category,(conuom) conuom   from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,  (case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.UOM_Code else ConvertedUnits.UOM_Code end) as CONuom,InventroyMovement.UOM,ConvertedUnit.Conversion_Factor,  ( Stock_Qty*ConvertedUnit.Conversion_Factor/(case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.Conversion_Factor else ConvertedUnits.Conversion_Factor end)) as stock_qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
                    ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from (  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT 
                     union all 
                     select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW
                    ) InventroyMovement 
                     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
                     left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                     left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                     left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
                     left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) 
					 left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG'
					   left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitP on ConvertedUnitp.Item_Code=InventroyMovement.Item_Code and 
					 isnull(ConvertedUnitP.processLoss_Uom,0)=1
					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitS on ConvertedUnitS.Item_Code=InventroyMovement.Item_Code and 
					 ConvertedUnitS.Stocking_Unit='Y'
					 					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit on ConvertedUnit.Item_Code=InventroyMovement.Item_Code and ConvertedUnit.UOM_Code=InventroyMovement.Stock_UOM


					 
					 left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No   left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC]  from (
                     select * from ( 
                     select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code 
                     ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc 
                     ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values 
                     ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc 
                     from  TSPL_ITEM_MASTER  
                     left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code 
                     left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values
                     where 2=2 
                     )xx
                     Pivot 
                     ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV])
                     ) Pivt
                     Pivot 
                     (
                     max(Category_Value_Desc) for Item_Category_CodeDesc in ([FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC])
                     ) Pivt1 
                     ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2    and Item_Code in (select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE
left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' ,103) and  CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate(txtTodate.Value) + "' ,103))   
                     and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtLoc.Value) + "') )) xxx 
                     where Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtTodate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                     union  all 
					 select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView,InOut,Location_Code,[Loc Desp],[LocAddress],SourceCode,SourceName,SourceType ,Item_Type, Item_Type_Name,Item_Group,Group_Description,null as [FINISHFOOD],null as [RAWMATERIAL],null as [OTHER],null as [PACKINGMAT],null as [FIXEDASST],null as [MAKE],null as [KV],null as [FINISHFOODDESC],null as [RAWMATERIALDESC],null as [OTHERDESC],null as [PACKINGMATDESC],null as [FIXEDASSTDESC],null as [MAKEDESC],null as [KVDESC],Items.Item_Code,Item_Desc, Item_Category_Struct_Code,  Items.Stock_UOM ,itf_code ,Stock_Qty,Balance_QTYKG,Rate,Cost,Balance_FAT, Balance_SNF 
                    ,0 as FATAmount
                    ,0 as SNFAmount
                     ,In_Category,Out_Category,(case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.UOM_Code else ConvertedUnits.UOM_Code end) as CONuom from (SELECT 0 as Trans_Id,null as Trans_Type,null as Trans_Type_Name, null as Source_Doc_No, thedate as Punching_Date,'In' as InOutView,'I' as InOut,TSPL_LOCATION_MASTER.Location_Code as Location_Code,TSPL_LOCATION_MASTER.Location_Desc as [Loc Desp],null as [LocAddress],null as SourceCode,null as SourceName,null as SourceType ,TSPL_ITEM_MASTER.Item_Type,null as Item_Type_Name,null as Item_Group,null as Group_Description,null as [FINISHFOOD],null as [RAWMATERIAL],null as [OTHER],null as [PACKINGMAT],null as [FIXEDASST],null as [MAKE],null as [KV],null as [FINISHFOODDESC],null as [RAWMATERIALDESC],null as [OTHERDESC],null as [PACKINGMATDESC],null as [FIXEDASSTDESC],null as [MAKEDESC],null as [KVDESC],TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,null as Item_Category_Struct_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code as Stock_UOM ,null as  itf_code ,0 as Stock_Qty,0 as Balance_QTYKG,0 as Rate,0 as Cost,0 as Balance_FAT, 0 as Balance_SNF ,null as In_Category,null as Out_Category,TSPL_ITEM_MASTER.Product_Type   FROM ExplodeDates( '" + clsCommon.GetPrintDate(txtFromDate.Value) + "','" + clsCommon.GetPrintDate(txtTodate.Value) + "') as d,TSPL_ITEM_MASTER,TSPL_LOCATION_MASTER,TSPL_ITEM_UOM_DETAIL where 2=2  and TSPL_ITEM_MASTER.Item_Code in (select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE
left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' ,103) and  CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate(txtTodate.Value) + "' ,103))   and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtLoc.Value) + "') )  and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y' and TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code) Items
left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitP on ConvertedUnitp.Item_Code=Items.Item_Code and 
					 isnull(ConvertedUnitP.processLoss_Uom,0)=1
					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitS on ConvertedUnitS.Item_Code=Items.Item_Code and 
					 ConvertedUnitS.Stocking_Unit='Y'
					 					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit on ConvertedUnit.Item_Code=Items.Item_Code and ConvertedUnit.UOM_Code=Items.Stock_UOM


left join (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as Fat_Per  from TSPL_ITEM_QC_PARAMETER_MASTER  left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='FAT') Fat on Items.Item_Code=Fat.Item_Code  left join  (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as SNF_Per  from TSPL_ITEM_QC_PARAMETER_MASTER  left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='SNF') as SNF on Items.Item_Code=SNF.Item_Code where 2=2  )xxxxxx Group by  Item_Code,Location_Code,Punching_Date )xxxxxxx left outer join tspl_location_master on tspl_location_master.Location_Code=xxxxxxx.Location_code where Punching_Date is not null )x )final where Location_Code= '" + clsCommon.myCstr(txtLoc.Value) + "'
                     AND Punching_Date= '" + clsCommon.GetPrintDate((txtFromDate.Value), "dd/MM/yyyy") + "'
					 
					 --Order by  convert(date,  Punching_Date,103),Location_Code
---PURCHASE RECEIVED AND ISSUE FOR PRODUCITON
UNION ALL
select max(convert(varchar, Punching_Date,103)) as Punching_Date,max(Item_Code) as item_cdoe ,max(Item_Desc) item_desc,MAX(Stock_UOM) AS Stock_UOM,0 AS OPQTY,0 AS OPRate,0 AS OPCost , sum(RecOthQty) as RecPurQty,sum((RecOthRate)*(RecOthQty))/case when sum(RecOthQty)=0 then 1 else sum(RecOthQty) end  as RecPurRate,sum((RecOthRate)*(RecOthQty)) as RecPurCost,sum(IssQty) as Issqty, (sum((IssRate)* (IssQty)) /case when sum(IssQty)=0 then 1 else sum(IssQty) end  ) as IssRate, sum((IssRate)* (IssQty)) as IssuCost,
0 AS CLQty,0 AS CLRate,0 AS CLCost,Sum(ProdIssQty) as ProdIssQty,sum(OtherIssQty)OtherIssQty,sum(ScrabSaleQty) as ScrabSaleQty,sum(stock_qty)stock_qty,MAX(FINAL.HeadName) AS HeadName,MAX(FINAL.Location_Code) AS Location_Code,max(FINAL.[Loc Desp]) AS [Loc Desp],MAX(FINAL.Add1) AS ADD1,MAX(FINAL.Add2) AS ADD2 ,MAX(FINAL.Add3) ADD3,MAX(FINAL.Add4) AS ADD4,max(conuom) as conuom
from (
select * from 
(select 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName,xxxxxxx.Location_Code,[Loc Desp],Add1,Add2,Add3,Add4,convert(varchar, Punching_Date,103) as Punching_Date  ,Item_Code ,Item_Desc,Stock_UOM, case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else  Convert(decimal(18,3),(ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))) end as OPQty,
                    case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11) then 0 else  Convert(decimal(18,2),((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))))) end as OPRate ,case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else Convert(decimal(18,2),(isnull(CLCost,0)-isnull(RecCost,0)+ isnull(IssCost,0))) end as OPCost, RecPurQty,RecPurRate,RecPurCost ,RecProQty, RecProRate ,RecProCost,RecAdjQty,RecAdjRate ,RecAdjCost ,RecOthQty,RecOthRate ,RecOthCost,RecQty,RecRate,RecCost  ,IssTransferQty ,IssTransferRate ,IssTransferCost ,IssSaleQty ,IssSaleRate  ,IssSaleCost , IssIssAdjQty , IssIssAdjRate ,IssIssAdjCost , IssOthQty , IssOthRate ,IssOthCost ,IssQty,IssRate,IssCost ,case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or (ABS(isnull(cast(CLCost as decimal(18,2)),0))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else CLQty end as CLQty,
                    case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or ABS(isnull(cast(CLCost as decimal(18,2)),0))<0.11 ) then 0 else CLCost/CLQty end as CLRate, CLCost ,ProdIssQty, OtherIssQty,ScrabSaleQty,stock_qty,conuom
                     
                    from 
					( select  Location_Code,max([Loc Desp]) as [Loc Desp],Punching_Date as Punching_Date, Item_Code ,max(Item_Desc) as Item_Desc, (case when max(CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtTodate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN Stock_UOM ELSE '' end)<>'' then max(CASE WHEN PUNCHING_DAte >= '01/Dec/2023 12:00:00 AM'  AND PUNCHING_DAte <= '31/Dec/2023 11:59:59 PM' THEN Stock_UOM ELSE '' end) else MAX(Stock_UOM) end ) as Stock_UOM, sum(Balance_FAT) as Balance_FAT,sum(Balance_SNF) as Balance_SNF  ,sum(isnull(Balance_QTYKG,0)) as Balance_QTYKG ,sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end) as RecPurQty ,case when sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end)/sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end)) end as RecPurRate  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_FAT else 0 end) as RecPurFAT  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurFATPER  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_SNF else 0 end) as RecPurSNF  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurSNFPER  ,sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end) as RecPurCost  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end) as RecAdjQty   ,case when sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and Trans_Type='IC-AD' then Cost else 0 end)/sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end)) end as RecAdjRate  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Balance_FAT else 0 end) as RecAdjFAT  ,(case when sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end) end)  as RecAdjFATPER  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Balance_SNF else 0 end) as RecAdjSNF  ,(case when sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end) end)  as RecAdjSNFPER  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Cost else 0 end) as RecAdjCost , sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end) as RecProQty   ,case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Cost else 0 end)/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end)) end as RecProRate  ,sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Balance_FAT else 0 end) as RecProFAT  ,(case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end) end)  as RecProFATPER  ,sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Balance_SNF else 0 end) as RecProSNF  ,(case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end) end)  as RecProSNFPER  , sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Cost else 0 end) as RecProCost ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end) as RecOthQty  ,case when sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end)/sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end)) end as RecOthRate  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_FAT else 0 end) as RecOthFAT  ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthFATPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_SNF else 0 end) as RecOthSNF ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthSNFPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end) as RecOthCost ,sum(case when InOut='I' then Stock_Qty else 0 end) as RecQty  ,case when cast(sum(case when InOut='I' then Stock_Qty else 0 end)as  decimal(18,2))=0 then 0 else (sum(case when InOut='I' then Cost else 0 end)/sum(case when InOut='I' then Stock_Qty else 0 end)) end as RecRate  ,sum(case when InOut='I' then Balance_FAT else 0 end) as RecFAT,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecFATPER  ,sum(case when InOut='I' then Balance_SNF else 0 end) as RecSNF  ,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecSNFPER  ,sum(case when InOut='I' then Cost else 0 end) as RecCost
                    ,sum(case when InOut='I' then FATAmount else 0 end) as RecFATAmount 
                    ,sum(case when InOut='I' then SNFAmount else 0 end) as RecSNFAmount 
                      ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end) as IssSaleQty  ,case when sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Cost else 0 end)/sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end)) end as IssSaleRate  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Balance_FAT else 0 end) as IssSaleFAT  ,(case when sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end) end)  as IssSaleFATPER  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Balance_SNF else 0 end) as IssSaleSNF  ,(case when sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end) end)  as IssSaleSNFPER  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Cost else 0 end) as IssSaleCost  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end) as IssTransferQty  ,case when sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Cost else 0 end)/sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end)) end as IssTransferRate  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Balance_FAT else 0 end) as IssTransferFAT  ,(case when sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end) end)  as IssTransferFATPER  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Balance_SNF else 0 end) as IssTransferSNF  ,(case when sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end) end)  as IssTransferSNFPER  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Cost else 0 end) as IssTransferCost,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end) as IssIssAdjQty  ,case when sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Cost else 0 end)/sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end)) end as IssIssAdjRate  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Balance_FAT else 0 end) as IssIssAdjFAT  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjFATPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Balance_SNF else 0 end) as IssIssAdjSNF  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjSNFPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Cost else 0 end) as IssIssAdjCost ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end) as IssOthQty  ,case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Cost else 0 end)/sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end)) end as IssOthRate  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Balance_FAT else 0 end) as IssOthFAT  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthFATPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Balance_SNF else 0 end) as IssOthSNF  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthSNFPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Cost else 0 end) as IssOthCost ,sum(case when InOut='O'   then -1.00*Stock_Qty else 0 end) as IssQty ,case when cast(sum(case when InOut='O' then -1.00*Stock_Qty else 0 end)as  decimal(18,2))=0 then 0 else (sum(case when InOut='O' then -1.00*Cost else 0 end)/sum(case when InOut='O' then -1.00*Stock_Qty else 0 end)) end as IssRate  ,sum(case when InOut='O' then -1.00*Balance_FAT else 0 end) as IssFAT  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssFATPER ,sum(case when InOut='O' then -1.00*Balance_SNF else 0 end) as IssSNF  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssSNFPER  ,sum(case when InOut='O' then -1.00*Cost else 0 end) as IssCost 
                    ,sum(case when InOut='O' then -1.00*FATAmount else 0 end) as IssFATAmount
                    ,sum(case when InOut='O' then -1.00*SNFAmount else 0 end) as IssSNFAmount
                     ,SUM(sum(Stock_Qty)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLQty   ,SUM(sum(Cost)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLCost,sum(case when InOut='O' and Trans_Type='STD_PRO_ENT' then -1.00*Stock_Qty else 0 end) as ProdIssQty,sum(case when InOut='O' and Trans_Type<>'STD_PRO_ENT' and Trans_Type<>'ScrapIn' then -1.00*Stock_Qty else 0 end) as OtherIssQty ,sum(case when InOut='O' and Trans_Type='ScrapIn' then -1.00*Stock_Qty else 0 end) as ScrabSaleQty ,sum(stock_qty)stock_qty,SUM(sum(isnull(Balance_QTYKG,0))) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_QTYKG  ,SUM(sum(Balance_FAT)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_FAT ,SUM(sum(Balance_SNF)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLBalance_SNF 
                    ,SUM(sum(FATAmount)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLFATAmount 
                    ,SUM(sum(SNFAmount)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLSNFAmount 
					,max(Conuom) as conuom
                      from 
					  (select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,xxx.Location_Code ,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC],xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1.00 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(28,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF 
                    ,( sum((case when IsFromMilk=1 then Fat_Amt  else (Stock_Qty*FatPer*Fat_Rate) end) * case when InOut='I' then 1 else -1 end)) AS FATAmount
                    ,(sum((case when IsFromMilk=1 then SNF_Amt else (Stock_Qty*SNFPer*SNF_Rate) end ) * case when InOut='I' then 1 else -1 end)) AS SNFAmount 
                     ,   '' as In_Category,'' as Out_Category,max(Conuom)Conuom from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM, 
					  (case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.UOM_Code else ConvertedUnits.UOM_Code end) as Conuom,InventroyMovement.UOM,ConvertedUnit.Conversion_Factor,  ( Stock_Qty*ConvertedUnit.Conversion_Factor/(case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.Conversion_Factor else ConvertedUnits.Conversion_Factor end)) as stock_qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
                    ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from 
					(  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor 
					from TSPL_INVENTORY_MOVEMENT 
                     union all 
                     select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW
                    ) InventroyMovement 
                     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
                     left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                     left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                     left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
                     left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' 
				  left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitP on ConvertedUnitp.Item_Code=InventroyMovement.Item_Code and 
					 isnull(ConvertedUnitP.processLoss_Uom,0)=1
					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitS on ConvertedUnitS.Item_Code=InventroyMovement.Item_Code and 
					 ConvertedUnitS.Stocking_Unit='Y'
					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit on ConvertedUnit.Item_Code=InventroyMovement.Item_Code and ConvertedUnit.UOM_Code=InventroyMovement.Stock_UOM
					 left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No    left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC]  from (
                     select * from ( 
                     select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code 
                     ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc 
                     ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values 
                     ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc 
                     from  TSPL_ITEM_MASTER  
                     left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code 
                     left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values
                     where 2=2 
                     )xx
                     Pivot 
                     ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV])
                     ) Pivt
                     Pivot 
                     (
                     max(Category_Value_Desc) for Item_Category_CodeDesc in ([FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC])
                     ) Pivt1 
                     ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2    and Item_Code in (select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE
left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' ,103) and  CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate(txtTodate.Value) + "' ,103))   
                     and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtLoc.Value) + "') )) xxx 
                     where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' group by xxx.Item_Code,xxx.Location_Code 
                     union all 
					 select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,cast(Punching_Date as date) as Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type, Item_Type_Name,Item_Group,Group_Description,[FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV],[FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC],Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code , ( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,  convert(decimal(28,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost, ( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,  ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF
                    ,(Fat_Amt * case when InOut='I' then 1 else -1 end) as FATAmount
                    ,(SNF_Amt * case when InOut='I' then 1 else -1 end) as SNFAmount
                     ,In_Category,Out_Category ,(conuom) conuom from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,
					  (case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.UOM_Code else ConvertedUnits.UOM_Code end) as CONuom,InventroyMovement.UOM,ConvertedUnit.Conversion_Factor,  ( Stock_Qty*ConvertedUnit.Conversion_Factor/(case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.Conversion_Factor else ConvertedUnits.Conversion_Factor end)) as stock_qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
                    ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from (  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT 
                     union all 
                     select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW
                    ) InventroyMovement 
                     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
                     left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                     left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                     left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
                     left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG'

					 					  left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitP on ConvertedUnitp.Item_Code=InventroyMovement.Item_Code and 
					 isnull(ConvertedUnitP.processLoss_Uom,0)=1
					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitS on ConvertedUnitS.Item_Code=InventroyMovement.Item_Code and 
					 ConvertedUnitS.Stocking_Unit='Y'
					 					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit on ConvertedUnit.Item_Code=InventroyMovement.Item_Code and ConvertedUnit.UOM_Code=InventroyMovement.Stock_UOM
					 left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No   left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC]  from (
                     select * from ( 
                     select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code 
                     ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc 
                     ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values 
                     ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc 
                     from  TSPL_ITEM_MASTER  
                     left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code 
                     left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values
                     where 2=2 
                     )xx
                     Pivot 
                     ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV])
                     ) Pivt
                     Pivot 
                     (
                     max(Category_Value_Desc) for Item_Category_CodeDesc in ([FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC])
                     ) Pivt1 
                     ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2    and Item_Code in (select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE
left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' ,103) and  CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate(txtTodate.Value) + "' ,103))    
                     and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtLoc.Value) + "') )) xxx 
                     where Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtTodate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                     union  
					 all select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView,InOut,Location_Code,[Loc Desp],[LocAddress],SourceCode,SourceName,SourceType ,Item_Type, Item_Type_Name,Item_Group,Group_Description,null as [FINISHFOOD],null as [RAWMATERIAL],null as [OTHER],null as [PACKINGMAT],null as [FIXEDASST],null as [MAKE],null as [KV],null as [FINISHFOODDESC],null as [RAWMATERIALDESC],null as [OTHERDESC],null as [PACKINGMATDESC],null as [FIXEDASSTDESC],null as [MAKEDESC],null as [KVDESC],Items.Item_Code,Item_Desc, Item_Category_Struct_Code,  Items.Stock_UOM ,itf_code ,Stock_Qty,Balance_QTYKG,Rate,Cost,Balance_FAT, Balance_SNF 
                    ,0 as FATAmount
                    ,0 as SNFAmount
                     ,In_Category,Out_Category,(case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.UOM_Code else ConvertedUnits.UOM_Code end) as CONuom  from (SELECT 0 as Trans_Id,null as Trans_Type,null as Trans_Type_Name, null as Source_Doc_No, thedate as Punching_Date,'In' as InOutView,'I' as InOut,TSPL_LOCATION_MASTER.Location_Code as Location_Code,TSPL_LOCATION_MASTER.Location_Desc as [Loc Desp],null as [LocAddress],null as SourceCode,null as SourceName,null as SourceType ,TSPL_ITEM_MASTER.Item_Type,null as Item_Type_Name,null as Item_Group,null as Group_Description,null as [FINISHFOOD],null as [RAWMATERIAL],null as [OTHER],null as [PACKINGMAT],null as [FIXEDASST],null as [MAKE],null as [KV],null as [FINISHFOODDESC],null as [RAWMATERIALDESC],null as [OTHERDESC],null as [PACKINGMATDESC],null as [FIXEDASSTDESC],null as [MAKEDESC],null as [KVDESC],TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,null as Item_Category_Struct_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code as Stock_UOM ,null as  itf_code ,0 as Stock_Qty,0 as Balance_QTYKG,0 as Rate,0 as Cost,0 as Balance_FAT, 0 as Balance_SNF ,null as In_Category,null as Out_Category,TSPL_ITEM_MASTER.Product_Type   FROM ExplodeDates(  '" + clsCommon.GetPrintDate(txtFromDate.Value) + "','" + clsCommon.GetPrintDate(txtTodate.Value) + "') as d,TSPL_ITEM_MASTER,TSPL_LOCATION_MASTER,TSPL_ITEM_UOM_DETAIL where 2=2  and TSPL_ITEM_MASTER.Item_Code in (select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE
left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' ,103) and  CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate(txtTodate.Value) + "' ,103))   and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtLoc.Value) + "') )  and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y' and TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code) Items 
left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitP on ConvertedUnitp.Item_Code=Items.Item_Code and 
					 isnull(ConvertedUnitP.processLoss_Uom,0)=1
					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitS on ConvertedUnitS.Item_Code=Items.Item_Code and 
					 ConvertedUnitS.Stocking_Unit='Y'
					 					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit on ConvertedUnit.Item_Code=Items.Item_Code and ConvertedUnit.UOM_Code=Items.Stock_UOM
				
left join (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as Fat_Per  from TSPL_ITEM_QC_PARAMETER_MASTER  left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='FAT') Fat on Items.Item_Code=Fat.Item_Code  left join  (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as SNF_Per  from TSPL_ITEM_QC_PARAMETER_MASTER  left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='SNF') as SNF on Items.Item_Code=SNF.Item_Code where 2=2  )xxxxxx Group by  Item_Code,Location_Code,Punching_Date )xxxxxxx left outer join tspl_location_master on tspl_location_master.Location_Code=xxxxxxx.Location_code where Punching_Date is not null )x )final  where Location_Code='" + clsCommon.myCstr(txtLoc.Value) + "'
					 group by Item_Code
					 --Order by  convert(date,  Punching_Date,103),Location_Code
					--CLOSING BALANCE
					 UNION ALL
select convert(varchar, Punching_Date,103) as Punching_Date  ,Item_Code ,Item_Desc,Stock_UOM, 
0 as OPQty ,0 AS OPRate,0 AS OPCost ,0 as RecPurQty,0 as RecPurRate,0 as RecPurCost,0 as Issqty, 0as IssRate, 0 as IssuCost, CLQty, CLRate, CLCost,0 as ProdIssQty,0 as OtherIssQty,0 as ScrabSaleQty,0 as Stock_qty,FINAL.HeadName,FINAL.Location_Code,FINAL.[Loc Desp],FINAL.Add1,FINAL.Add2,FINAL.Add3,FINAL.Add4,final.conuom
from (
select * from 
(select 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName,xxxxxxx.Location_Code,[Loc Desp],Add1,Add2,Add3,Add4,convert(varchar, Punching_Date,103) as Punching_Date  ,Item_Code ,Item_Desc,Stock_UOM, case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else  Convert(decimal(18,3),(ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))) end as OPQty,
                    case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11) then 0 else  Convert(decimal(18,2),((isnull(CLCost,0)-isnull(RecCost,0)+isnull(IssCost,0))/((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))))) end as OPRate ,case when ( abs(cast((ISNULL(CLQty,0) - isnull(RecQty,0)+isnull(IssQty,0))as  decimal(18,2)))<=0.11 or (abs(cast((ISNULL(CLCost,0) - isnull(RecCost,0)+isnull(IssCost,0))as  decimal(18,2)))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else Convert(decimal(18,2),(isnull(CLCost,0)-isnull(RecCost,0)+ isnull(IssCost,0))) end as OPCost, RecPurQty,RecPurRate,RecPurCost ,RecProQty, RecProRate ,RecProCost,RecAdjQty,RecAdjRate ,RecAdjCost ,RecOthQty,RecOthRate ,RecOthCost,RecQty,RecRate,RecCost  ,IssTransferQty ,IssTransferRate ,IssTransferCost ,IssSaleQty ,IssSaleRate  ,IssSaleCost , IssIssAdjQty , IssIssAdjRate ,IssIssAdjCost , IssOthQty , IssOthRate ,IssOthCost ,IssQty,IssRate,IssCost ,case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or (ABS(isnull(cast(CLCost as decimal(18,2)),0))<0.11 and tspl_location_master.Is_jobWork=0) ) then 0 else CLQty end as CLQty,
                    case when (ABS(isnull(cast(CLQty as decimal(18,2)),0))<=0.11 or ABS(isnull(cast(CLCost as decimal(18,2)),0))<0.11 ) then 0 else CLCost/CLQty end as CLRate, CLCost ,conuom
                     
                    from 
					( select  Location_Code,max([Loc Desp]) as [Loc Desp],Punching_Date as Punching_Date, Item_Code ,max(Item_Desc) as Item_Desc, (case when max(CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtTodate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN Stock_UOM ELSE '' end)<>'' then max(CASE WHEN PUNCHING_DAte >= '01/Dec/2023 12:00:00 AM'  AND PUNCHING_DAte <= '01/Dec/2023 11:59:59 PM' THEN Stock_UOM ELSE '' end) else MAX(Stock_UOM) end ) as Stock_UOM, sum(Balance_FAT) as Balance_FAT,sum(Balance_SNF) as Balance_SNF  ,sum(isnull(Balance_QTYKG,0)) as Balance_QTYKG ,sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end) as RecPurQty ,case when sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end)/sum(case when InOut='I' and In_Category in ('PU') then Stock_Qty else 0 end)) end as RecPurRate  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_FAT else 0 end) as RecPurFAT  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurFATPER  ,sum(case when InOut='I' and In_Category in ('PU') then Balance_SNF else 0 end) as RecPurSNF  ,(case when sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category in ('PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category in ('PU') then  Balance_QTYKG else 0 end) end)  as RecPurSNFPER  ,sum(case when InOut='I' and In_Category in ('PU') then Cost else 0 end) as RecPurCost  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end) as RecAdjQty   ,case when sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and Trans_Type='IC-AD' then Cost else 0 end)/sum(case when InOut='I' and Trans_Type='IC-AD' then Stock_Qty else 0 end)) end as RecAdjRate  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Balance_FAT else 0 end) as RecAdjFAT  ,(case when sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end) end)  as RecAdjFATPER  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Balance_SNF else 0 end) as RecAdjSNF  ,(case when sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type='IC-AD' then  Balance_QTYKG else 0 end) end)  as RecAdjSNFPER  ,sum(case when InOut='I' and Trans_Type='IC-AD' then Cost else 0 end) as RecAdjCost , sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end) as RecProQty   ,case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Cost else 0 end)/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Stock_Qty else 0 end)) end as RecProRate  ,sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Balance_FAT else 0 end) as RecProFAT  ,(case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end) end)  as RecProFATPER  ,sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Balance_SNF else 0 end) as RecProSNF  ,(case when sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then  Balance_QTYKG else 0 end) end)  as RecProSNFPER  , sum(case when InOut='I' and Trans_Type='PROD_ENTRY' then Cost else 0 end) as RecProCost ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end) as RecOthQty  ,case when sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end)/sum(case when InOut='I' and In_Category not in ('AD','PU') then Stock_Qty else 0 end)) end as RecOthRate  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_FAT else 0 end) as RecOthFAT  ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_FAT else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthFATPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Balance_SNF else 0 end) as RecOthSNF ,(case when sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_SNF else 0 end)*100/sum(case when InOut='I' and In_Category not in ('AD','PU') then  Balance_QTYKG else 0 end) end)  as RecOthSNFPER  ,sum(case when InOut='I' and In_Category not in ('AD','PU') then Cost else 0 end) as RecOthCost ,sum(case when InOut='I' then Stock_Qty else 0 end) as RecQty  ,case when cast(sum(case when InOut='I' then Stock_Qty else 0 end)as  decimal(18,2))=0 then 0 else (sum(case when InOut='I' then Cost else 0 end)/sum(case when InOut='I' then Stock_Qty else 0 end)) end as RecRate  ,sum(case when InOut='I' then Balance_FAT else 0 end) as RecFAT,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_FAT else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecFATPER  ,sum(case when InOut='I' then Balance_SNF else 0 end) as RecSNF  ,(case when sum(case when InOut='I' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='I' then  Balance_SNF else 0 end)*100/sum(case when InOut='I' then  Balance_QTYKG else 0 end) end)  as RecSNFPER  ,sum(case when InOut='I' then Cost else 0 end) as RecCost
                    ,sum(case when InOut='I' then FATAmount else 0 end) as RecFATAmount 
                    ,sum(case when InOut='I' then SNFAmount else 0 end) as RecSNFAmount 
                      ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end) as IssSaleQty  ,case when sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Cost else 0 end)/sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Stock_Qty else 0 end)) end as IssSaleRate  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Balance_FAT else 0 end) as IssSaleFAT  ,(case when sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end) end)  as IssSaleFATPER  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Balance_SNF else 0 end) as IssSaleSNF  ,(case when sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type='FS-SH' then  Balance_QTYKG else 0 end) end)  as IssSaleSNFPER  ,sum(case when InOut='O' and Trans_Type='FS-SH' then -1.00*Cost else 0 end) as IssSaleCost  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end) as IssTransferQty  ,case when sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Cost else 0 end)/sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Stock_Qty else 0 end)) end as IssTransferRate  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Balance_FAT else 0 end) as IssTransferFAT  ,(case when sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end) end)  as IssTransferFATPER  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Balance_SNF else 0 end) as IssTransferSNF  ,(case when sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Trans_Type='Transfer' then  Balance_QTYKG else 0 end) end)  as IssTransferSNFPER  ,sum(case when InOut='O' and Trans_Type='Transfer' then -1.00*Cost else 0 end) as IssTransferCost,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end) as IssIssAdjQty  ,case when sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Cost else 0 end)/sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Stock_Qty else 0 end)) end as IssIssAdjRate  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Balance_FAT else 0 end) as IssIssAdjFAT  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjFATPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Balance_SNF else 0 end) as IssIssAdjSNF  ,(case when sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category in ('IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category in ('IS') then  Balance_QTYKG else 0 end) end)  as IssIssAdjSNFPER  ,sum(case when InOut='O' and Out_Category in ('IS') then -1.00*Cost else 0 end) as IssIssAdjCost ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end) as IssOthQty  ,case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end)=0 then 0 else (sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Cost else 0 end)/sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Stock_Qty else 0 end)) end as IssOthRate  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Balance_FAT else 0 end) as IssOthFAT  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_FAT else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthFATPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Balance_SNF else 0 end) as IssOthSNF  ,(case when sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_SNF else 0 end)*100/sum(case when InOut='O' and Out_Category not in ('SA','IS') then  Balance_QTYKG else 0 end) end)  as IssOthSNFPER  ,sum(case when InOut='O' and Out_Category not in ('SA','IS') then -1.00*Cost else 0 end) as IssOthCost ,sum(case when InOut='O'  then -1.00*Stock_Qty else 0 end) as IssQty ,case when cast(sum(case when InOut='O' then -1.00*Stock_Qty else 0 end)as  decimal(18,2))=0 then 0 else (sum(case when InOut='O' then -1.00*Cost else 0 end)/sum(case when InOut='O' then -1.00*Stock_Qty else 0 end)) end as IssRate  ,sum(case when InOut='O' then -1.00*Balance_FAT else 0 end) as IssFAT  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_FAT else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssFATPER ,sum(case when InOut='O' then -1.00*Balance_SNF else 0 end) as IssSNF  ,(case when sum(case when InOut='O' then  Balance_QTYKG else 0 end)=0 then 0  else  sum(case when InOut='O' then  Balance_SNF else 0 end)*100/sum(case when InOut='O' then  Balance_QTYKG else 0 end) end)  as IssSNFPER  ,sum(case when InOut='O' then -1.00*Cost else 0 end) as IssCost 
                    ,sum(case when InOut='O' then -1.00*FATAmount else 0 end) as IssFATAmount
                    ,sum(case when InOut='O' then -1.00*SNFAmount else 0 end) as IssSNFAmount
                     ,SUM(sum(Stock_Qty)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLQty   ,SUM(sum(Cost)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLCost ,SUM(sum(isnull(Balance_QTYKG,0))) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_QTYKG  ,SUM(sum(Balance_FAT)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Punching_Date) as CLBalance_FAT ,SUM(sum(Balance_SNF)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code,Punching_Date) as CLBalance_SNF 
                    ,SUM(sum(FATAmount)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLFATAmount 
                    ,SUM(sum(SNFAmount)) OVER (Partition BY Item_Code,Location_Code ORDER BY Item_Code,Location_Code, Item_Code,Punching_Date) as CLSNFAmount ,max(conuom)conuom
                      from 
					  (select 0 as Trans_Id,'' as Trans_Type,'' as Trans_Type_Name,'Opening Balance' as Source_Doc_No,null as Punching_Date,'' as InOutView, '' as InOut,xxx.Location_Code ,'' as [Loc Desp],'' as [LocAddress],'' as SourceCode,'' as SourceName,'' as SourceType ,'' as Item_Type,'' as Item_Type_Name,'' as Item_Group,'' as Group_Description,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC],xxx.Item_Code as Item_Code ,max(xxx.Item_Desc) as Item_Desc,'' as Item_Category_Struct_Code,max(xxx.Stock_UOM) as Stock_UOM,'' as itf_code ,sum( Stock_Qty * case when InOut='I' then 1.00 else -1.00 end) as Stock_Qty,sum( QtyKG * case when InOut='I' then 1.00 else -1 end) as Balance_QTYKG, case when sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end))=0 then 0 else sum(convert(decimal(28,3),Cost* case when InOut='I' then 1 else -1 end))/sum(convert(decimal(28,3),Stock_Qty* case when InOut='I' then 1 else -1 end)) end as Rate,sum(Cost * case when InOut='I' then 1 else -1 end) as Cost,sum( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,sum(( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end)) as Balance_SNF 
                    ,( sum((case when IsFromMilk=1 then Fat_Amt  else (Stock_Qty*FatPer*Fat_Rate) end) * case when InOut='I' then 1 else -1 end)) AS FATAmount
                    ,(sum((case when IsFromMilk=1 then SNF_Amt else (Stock_Qty*SNFPer*SNF_Rate) end ) * case when InOut='I' then 1 else -1 end)) AS SNFAmount 
                     ,   '' as In_Category,'' as Out_Category,max(conuom)conuom from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG,InventroyMovement.Stock_UOM, 
					  (case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.UOM_Code else ConvertedUnits.UOM_Code end) as CONuom,InventroyMovement.UOM,ConvertedUnit.Conversion_Factor,  ( Stock_Qty*ConvertedUnit.Conversion_Factor/(case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.Conversion_Factor else ConvertedUnits.Conversion_Factor end)) as stock_qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
                    ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from 
					(  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor 
					from TSPL_INVENTORY_MOVEMENT 
                     union all 
                     select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW
                    ) InventroyMovement 
                     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
                     left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                     left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                     left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
                     left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' 
					  left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitP on ConvertedUnitp.Item_Code=InventroyMovement.Item_Code and 
					 isnull(ConvertedUnitP.processLoss_Uom,0)=1
					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitS on ConvertedUnitS.Item_Code=InventroyMovement.Item_Code and 
					 ConvertedUnitS.Stocking_Unit='Y'
					 					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit on ConvertedUnit.Item_Code=InventroyMovement.Item_Code and ConvertedUnit.UOM_Code=InventroyMovement.Stock_UOM
					 left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No    left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC]  from (
                     select * from ( 
                     select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code 
                     ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc 
                     ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values 
                     ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc 
                     from  TSPL_ITEM_MASTER  
                     left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code 
                     left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values
                     where 2=2 
                     )xx
                     Pivot 
                     ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV])
                     ) Pivt
                     Pivot 
                     (
                     max(Category_Value_Desc) for Item_Category_CodeDesc in ([FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC])
                     ) Pivt1 
                     ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2    and Item_Code in (select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE
left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' ,103) and  CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate(txtTodate.Value) + "' ,103))   
                     and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtLoc.Value) + "') )) xxx 
                     where Punching_Date < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' group by xxx.Item_Code,xxx.Location_Code 
                     union all  select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,cast(Punching_Date as date) as Punching_Date,InOutView, InOut,Location_Code,[Loc Desp], [LocAddress],SourceCode,SourceName,SourceType ,Item_Type, Item_Type_Name,Item_Group,Group_Description,[FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV],[FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC],Item_Code ,Item_Desc,Item_Category_Struct_Code,Stock_UOM,itf_code , ( Stock_Qty * case when InOut='I' then 1 else -1 end) as Stock_Qty,(QtyKG * case when InOut='I' then 1 else -1 end) as Balance_QTYKG,  convert(decimal(28,3),case when Stock_Qty=0 then 0 else Cost/Stock_Qty end) as Rate,(Cost * case when InOut='I' then 1 else -1 end) as Cost, ( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*FatPer) end) * case when InOut='I' then 1 else -1 end) as Balance_FAT,  ( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*SNFPer) end ) * case when InOut='I' then 1 else -1 end) as Balance_SNF
                    ,(Fat_Amt * case when InOut='I' then 1 else -1 end) as FATAmount
                    ,(SNF_Amt * case when InOut='I' then 1 else -1 end) as SNFAmount
                     ,In_Category,Out_Category ,CONuom  from (select * from ( select InventroyMovement.Fat_Amt,InventroyMovement.SNF_Amt,gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE  TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView', case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location, isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG, InventroyMovement.Stock_UOM,
					 (case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.UOM_Code else ConvertedUnits.UOM_Code end) as CONuom,InventroyMovement.UOM,ConvertedUnit.Conversion_Factor,  ( Stock_Qty*ConvertedUnit.Conversion_Factor/(case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.Conversion_Factor else ConvertedUnits.Conversion_Factor end)) as stock_qty, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer, isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code 
                    ,VirtualCategoryTabel.[FINISHFOOD],VirtualCategoryTabel.[RAWMATERIAL],VirtualCategoryTabel.[OTHER],VirtualCategoryTabel.[PACKINGMAT],VirtualCategoryTabel.[FIXEDASST],VirtualCategoryTabel.[MAKE],VirtualCategoryTabel.[KV],VirtualCategoryTabel.[FINISHFOODDESC],VirtualCategoryTabel.[RAWMATERIALDESC],VirtualCategoryTabel.[OTHERDESC],VirtualCategoryTabel.[PACKINGMATDESC],VirtualCategoryTabel.[FIXEDASSTDESC],VirtualCategoryTabel.[MAKEDESC],VirtualCategoryTabel.[KVDESC] ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation  from (  select Fat_Amt,SNF_Amt,0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,'' as Custom_UOM,0 as Custom_Coversion_Factor  from TSPL_INVENTORY_MOVEMENT 
                     union all 
                     select Fat_Amt,SNF_Amt,ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,isnull(Custom_UOM,'') as Custom_UOM,isnull(Custom_Coversion_Factor,0) as Custom_Coversion_Factor from TSPL_INVENTORY_MOVEMENT_NEW
                    ) InventroyMovement 
                     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code
                     left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                     left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code
                     left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code 
                     left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' 
								  left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitP on ConvertedUnitp.Item_Code=InventroyMovement.Item_Code and 
					 isnull(ConvertedUnitP.processLoss_Uom,0)=1
					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitS on ConvertedUnitS.Item_Code=InventroyMovement.Item_Code and 
					 ConvertedUnitS.Stocking_Unit='Y'
					 					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit on ConvertedUnit.Item_Code=InventroyMovement.Item_Code and ConvertedUnit.UOM_Code=InventroyMovement.Stock_UOM
					 left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No   left outer join (select Item_Code,max([FINISHFOOD]) as [FINISHFOOD],max([RAWMATERIAL]) as [RAWMATERIAL],max([OTHER]) as [OTHER],max([PACKINGMAT]) as [PACKINGMAT],max([FIXEDASST]) as [FIXEDASST],max([MAKE]) as [MAKE],max([KV]) as [KV],max([FINISHFOODDESC]) as [FINISHFOODDESC],max([RAWMATERIALDESC]) as [RAWMATERIALDESC],max([OTHERDESC]) as [OTHERDESC],max([PACKINGMATDESC]) as [PACKINGMATDESC],max([FIXEDASSTDESC]) as [FIXEDASSTDESC],max([MAKEDESC]) as [MAKEDESC],max([KVDESC]) as [KVDESC]  from (
                     select * from ( 
                     select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code 
                     ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc 
                     ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values 
                     ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc 
                     from  TSPL_ITEM_MASTER  
                     left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code 
                     left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values
                     where 2=2 
                     )xx
                     Pivot 
                     ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [FINISHFOOD],[RAWMATERIAL],[OTHER],[PACKINGMAT],[FIXEDASST],[MAKE],[KV])
                     ) Pivt
                     Pivot 
                     (
                     max(Category_Value_Desc) for Item_Category_CodeDesc in ([FINISHFOODDESC],[RAWMATERIALDESC],[OTHERDESC],[PACKINGMATDESC],[FIXEDASSTDESC],[MAKEDESC],[KVDESC])
                     ) Pivt1 
                     ) xxx group by Item_Code ) as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code left outer join ( select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join ( select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='ITEM-STRUCT'   and Custom_Field_Code='') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='') as StructDtl on Struct_Val.Value=StructDtl.Value ) as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code  left outer join ( SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER  ) as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2    and Item_Code in (select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE
left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' ,103) and  CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate(txtTodate.Value) + "' ,103))   
                     and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtLoc.Value) + "') )) xxx 
                     where Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Punching_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtTodate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'
                     union  all 
					 select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,InOutView,InOut,Location_Code,[Loc Desp],[LocAddress],SourceCode,SourceName,SourceType ,Item_Type, Item_Type_Name,Item_Group,Group_Description,null as [FINISHFOOD],null as [RAWMATERIAL],null as [OTHER],null as [PACKINGMAT],null as [FIXEDASST],null as [MAKE],null as [KV],null as [FINISHFOODDESC],null as [RAWMATERIALDESC],null as [OTHERDESC],null as [PACKINGMATDESC],null as [FIXEDASSTDESC],null as [MAKEDESC],null as [KVDESC],Items.Item_Code,Item_Desc, Item_Category_Struct_Code,  Items.Stock_UOM ,itf_code ,Stock_Qty,Balance_QTYKG,Rate,Cost,Balance_FAT, Balance_SNF 
                    ,0 as FATAmount
                    ,0 as SNFAmount
                     ,In_Category,Out_Category,(case when ConvertedUnitp.processLoss_Uom=1 then ConvertedUnitp.UOM_Code else ConvertedUnits.UOM_Code end) as CONuom   from (SELECT 0 as Trans_Id,null as Trans_Type,null as Trans_Type_Name, null as Source_Doc_No, thedate as Punching_Date,'In' as InOutView,'I' as InOut,TSPL_LOCATION_MASTER.Location_Code as Location_Code,TSPL_LOCATION_MASTER.Location_Desc as [Loc Desp],null as [LocAddress],null as SourceCode,null as SourceName,null as SourceType ,TSPL_ITEM_MASTER.Item_Type,null as Item_Type_Name,null as Item_Group,null as Group_Description,null as [FINISHFOOD],null as [RAWMATERIAL],null as [OTHER],null as [PACKINGMAT],null as [FIXEDASST],null as [MAKE],null as [KV],null as [FINISHFOODDESC],null as [RAWMATERIALDESC],null as [OTHERDESC],null as [PACKINGMATDESC],null as [FIXEDASSTDESC],null as [MAKEDESC],null as [KVDESC],TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,null as Item_Category_Struct_Code,TSPL_ITEM_UOM_DETAIL.UOM_Code as Stock_UOM ,null as  itf_code ,0 as Stock_Qty,0 as Balance_QTYKG,0 as Rate,0 as Cost,0 as Balance_FAT, 0 as Balance_SNF ,null as In_Category,null as Out_Category,TSPL_ITEM_MASTER.Product_Type   FROM ExplodeDates( '" + clsCommon.GetPrintDate(txtFromDate.Value) + "','" + clsCommon.GetPrintDate(txtTodate.Value) + "') as d,TSPL_ITEM_MASTER,TSPL_LOCATION_MASTER,TSPL_ITEM_UOM_DETAIL where 2=2  and TSPL_ITEM_MASTER.Item_Code in (select distinct TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE from TSPL_SPP_PRODUCTION_ENTRY_DETAIL 
left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
left outer join TSPL_MF_BOM_HEAD on TSPL_MF_BOM_HEAD.BOM_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.BOM_CODE
left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
where CONVERT(DATE,PROD_DATE,103)>= convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' ,103) and  CONVERT(DATE,PROD_DATE,103)<= convert(date,'" + clsCommon.GetPrintDate(txtTodate.Value) + "' ,103))   and ( ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(txtLoc.Value) + "') )  and TSPL_ITEM_UOM_DETAIL.Stocking_Unit='Y' and TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code) Items

left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitP on ConvertedUnitp.Item_Code=Items.Item_Code and 
					 isnull(ConvertedUnitP.processLoss_Uom,0)=1
					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnitS on ConvertedUnitS.Item_Code=Items.Item_Code and 
					 ConvertedUnitS.Stocking_Unit='Y'
					 					 left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUnit on ConvertedUnit.Item_Code=Items.Item_Code and ConvertedUnit.UOM_Code=Items.Stock_UOM
left join (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as Fat_Per  from TSPL_ITEM_QC_PARAMETER_MASTER  left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='FAT') Fat on Items.Item_Code=Fat.Item_Code  left join  (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range as SNF_Per  from TSPL_ITEM_QC_PARAMETER_MASTER  left join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where TSPL_PARAMETER_MASTER.Type='SNF') as SNF on Items.Item_Code=SNF.Item_Code where 2=2  )xxxxxx Group by  Item_Code,Location_Code,Punching_Date )xxxxxxx left outer join tspl_location_master on tspl_location_master.Location_Code=xxxxxxx.Location_code where Punching_Date is not null )x )final where Location_Code= '" + clsCommon.myCstr(txtLoc.Value) + "' 
                     AND Punching_Date= '" + clsCommon.GetPrintDate((txtFromDate.Value), "dd/MM/yyyy") + "'
					 )  final1 group by final1.Item_Code,final1.Item_Desc "

            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    ' gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = False
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSno).Value = clsCommon.myCstr(dr("SNo"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemType).Value = clsCommon.myCstr(dr("Item_type"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colitemcode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colitemname).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(coluom).Value = clsCommon.myCstr(dr("conuom"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colopQty).Value = clsCommon.myCstr(dr("OPQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colopAmt).Value = clsCommon.myCstr(dr("OPCost"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRecQty).Value = clsCommon.myCstr(dr("RecPurQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRecAmt).Value = clsCommon.myCstr(dr("RecPurCost"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIssProdQty).Value = clsCommon.myCstr(dr("ProdIssQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIssProdAmt).Value = clsCommon.myCstr(dr("ProdIssCost"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOtherIssQty).Value = clsCommon.myCstr(dr("OtherIssQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOtherIssAmt).Value = clsCommon.myCstr(dr("OtherIssCost"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colClQty).Value = clsCommon.myCstr(dr("CLQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colClAmt).Value = clsCommon.myCstr(dr("CLCost"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colStockTransferQty).Value = clsCommon.myCstr(dr("ScrabSaleQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colStockTransferAmt).Value = clsCommon.myCstr(dr("ScrabSaleAmt"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCstr(dr("IssRate"))
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display ", Me.Text)
            End If
            ' End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            dt = Nothing
        End Try
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
                    gv1.CurrentRow.Cells(colPLper).Value = (clsCommon.myCdbl(gv1.CurrentRow.Cells(colIssProdQty).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colPLQty).Value)) / 100
                    If clsCommon.myCdbl(gv1.CurrentRow.Cells(colPLper).Value) > 0 Then
                        gv1.CurrentRow.Cells(colFinalClStock).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colopQty).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colRecQty).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colIssProdQty).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colPLper).Value)
                    Else
                        gv1.CurrentRow.Cells(colFinalClStock).Value = 0
                    End If
                End If
                    'End If

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
        txtDocNo.Value = ""
        gv1.DataSource = Nothing
        LoadBlankGrid()
        btnReverse.Visible = False
        docDate.Value = clsCommon.GETSERVERDATE()
        gv1.MasterTemplate.FilterDescriptors.Clear()
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnDelete.Enabled = False
        btnSave.Text = "Save"
        txtComments.Text = ""
        gv1.MasterTemplate.FilterDescriptors.Clear()
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Private Sub MyLabel2_Click(sender As Object, e As EventArgs) Handles MyLabel2.Click

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Private Sub SaveData(ByVal isPost As Boolean)
        Dim obj As New clsRMProcessLoss()
        Dim objpd As New ClsRmProcessLossDetail
        'isInsideLoadData = True
        Try
            If AllowToSave() Then
                obj.document_code = clsCommon.myCstr(txtDocNo.Value)
                obj.document_date = clsCommon.myCDate(docDate.Text)
                obj.Location = clsCommon.myCstr(txtLoc.Value)
                obj.Comments = clsCommon.myCstr(txtComments.Text)
                obj.Fromdate = clsCommon.myCDate(txtFromDate.Value)
                obj.Todate = clsCommon.myCDate(txtTodate.Value)
                'obj.QC_Status = clsCommon.myCstr(txtAccept.Text)
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
                txtFromDate.Text = obj.Fromdate
                txtDate.Value = obj.Todate
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
                'If obj.Arr_Prod IsNot Nothing AndAlso obj.Arr_Prod.Count > 0 Then
                '    For Each objPRD As clsProductionEntry In obj.Arr_Prod
                '        For i As Integer = 0 To txtprodCode.arrValueMember.Count - 1
                '            ' Assuming Arr_Prod is a List(Of String)
                '            Arr_Prod.Add(txtprodCode.arrValueMember(i))
                '        Next
                '    Next

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
        Dim qry As String = "  select Document_Code as Code,Document_Date,Case when status=0 then 'Pending' else 'Approved' end as 'Status' from TSPL_RM_PROCESS_LOSS "
        LoadData(clsCommon.ShowSelectForm("OutgoingQC", qry, "Code", "", txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
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
            ' If QCEnddate.Value  QcStartdata.Value Then
            'If obj.QC_end_date > clsCommon.GETSERVERDATE Then
            '    QCEnddate.Focus()
            '    Throw New Exception("QC End date should be less than Server Date")
            'Else
            '    obj.QC_end_date = clsCommon.myCDate(QCEnddate.Value, "dd/MM/yyyy")
            'End If
            'If obj.QC_Start_date > clsCommon.GETSERVERDATE Then
            '    QcStartdata.Focus()
            '    Throw New Exception("QC End date should be less than Server Date")
            'Else
            '    obj.QC_Start_date = clsCommon.myCDate(QcStartdata.Value, "dd/MM/yyyy")
            'End If
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
            For ii As Integer = 0 To gv1.Rows.Count - 1
                icode = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemType).Value)
                If clsCommon.CompairString(icode, "RM") = CompairStringResult.Equal Then
                    status += 1
                    PLAvg += clsCommon.myCstr(gv1.Rows(ii).Cells(colPLper).Value)
                    'PLAvg += PLAvg
                End If
                'If clsCommon.myLen(icode) > 0 Then
                '    status += 1
                'End If
                arrpd.Add(icode)
            Next
            AvgPer = PLAvg / status
            If AvgPer > 0.6 Then
                If clsCommon.MyMessageBoxShow(Me, ",The average process loss %age of RM material is greater than allowed %age 0.60" + Environment.NewLine + "Do you want to save ?", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
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
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnMRN)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
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
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
End Class