Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports common
Imports System.IO

Public Class FrmViewLoadOutDetail

    Const ReportID As String = "ViewLoadoutDetails"

    Const colSettlementCode As String = "SettleMentCode"
    Const colDescription As String = "Description"
    Const colAmount As String = "Amount1"
    Const colRemarks As String = "Remarks"

    Const colItemCode As String = "colItemCode"
    Const colItemName As String = "colItemName"
    Const colLoadOutQty As String = "colLoadQty"
    Const colLoadInQtyFC As String = "colLoadInQtyFC"
    Const colLoadInQtyFB As String = "colLoadInQtyFB"
    Const colTotalLoadInQtyFC As String = "colTotalLoadInQtyFC"
    Const colProvisionalSale As String = "colProvisionalSale"
    Const colRetailerPrice As String = "colRetailerPrice"
    Const colAmountLoadOut As String = "colAmountLoadOut"
    Const colMRP As String = "colMRP"
    Const colFOCinFC As String = "colFOCinFC"
    Const colFOCinFB As String = "colFOCinFB"
    Const colTotalFOCinFC As String = "colTotalFOCinFC"
    Const colNetSaleAmt As String = "colNetSaleAmt"
    Const colSchemeAmt As String = "colSchemeAmt"
    Const colNetLoadQty As String = "colNetLoadQty"



    Public ProvisionalSale_Amount As Decimal = 0
    Public NetSale_Amount As Decimal = 0
    Public Scheme_Amount As Decimal = 0
    'Public Net_LoadInQty As Decimal = 0
    'Public Net_LoadOutQty As Decimal = 0


    Dim userCode, companyCode As String
    Public LoadOutNo As String = Nothing
    Public strQuickSettlementId As String = Nothing

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Public Sub New(ByVal TransferNo As String, ByVal QuickSettlementId As String, ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        LoadOutNo = TransferNo
        strQuickSettlementId = QuickSettlementId
    End Sub

    Sub LoadBlankGrid()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCode = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.HeaderText = "Item Code"
        repoItemCode.Name = colItemCode
        repoItemCode.Width = 120
        repoItemCode.ReadOnly = True
        repoItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoItemCode)


        Dim repoItemName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemName = New GridViewTextBoxColumn()
        repoItemName.FormatString = ""
        repoItemName.HeaderText = "Item Name"
        repoItemName.Name = colItemName
        repoItemName.Width = 300
        repoItemName.ReadOnly = True
        repoItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoItemName)

        Dim repoMRP As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMRP = New GridViewTextBoxColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 100
        repoMRP.ReadOnly = True
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoLoadOutQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLoadOutQty = New GridViewDecimalColumn()
        repoLoadOutQty.FormatString = ""
        repoLoadOutQty.HeaderText = "LoadOut Qty"
        repoLoadOutQty.Name = colLoadOutQty
        repoLoadOutQty.Width = 100
        repoLoadOutQty.ReadOnly = True
        repoLoadOutQty.FormatString = "{0:F2}"
        repoLoadOutQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLoadOutQty)


        Dim repoLoadInFC As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLoadInFC = New GridViewDecimalColumn()
        repoLoadInFC.FormatString = ""
        repoLoadInFC.HeaderText = "LoadIn Qty(FC)"
        repoLoadInFC.Name = colLoadInQtyFC
        repoLoadInFC.Width = 100
        repoLoadInFC.ReadOnly = True
        repoLoadInFC.FormatString = "{0:F2}"
        repoLoadInFC.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLoadInFC)


        Dim repoLoadInFB As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLoadInFB = New GridViewDecimalColumn()
        repoLoadInFB.FormatString = ""
        repoLoadInFB.HeaderText = "LoadIn Qty(FB)"
        repoLoadInFB.Name = colLoadInQtyFB
        repoLoadInFB.Width = 100
        repoLoadInFB.ReadOnly = True
        repoLoadInFB.FormatString = "{0:F2}"
        repoLoadInFB.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLoadInFB)


        Dim repoTotLoadInFC As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotLoadInFC = New GridViewDecimalColumn()
        repoTotLoadInFC.FormatString = ""
        repoTotLoadInFC.HeaderText = "Total LoadIn Qty(FC)"
        repoTotLoadInFC.Name = colTotalLoadInQtyFC
        repoTotLoadInFC.Width = 100
        repoTotLoadInFC.ReadOnly = True
        repoTotLoadInFC.FormatString = "{0:F2}"
        repoTotLoadInFC.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotLoadInFC)

        Dim repoPSQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPSQty = New GridViewDecimalColumn()
        repoPSQty.FormatString = ""
        repoPSQty.HeaderText = "Provisional Sale Qty(FC)"
        repoPSQty.Name = colProvisionalSale
        repoPSQty.Width = 130
        repoPSQty.ReadOnly = True
        repoPSQty.FormatString = "{0:F2}"
        repoPSQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPSQty)

        Dim repoRetailerPrice As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRetailerPrice = New GridViewDecimalColumn()
        repoRetailerPrice.FormatString = ""
        repoRetailerPrice.HeaderText = "Retailer Price"
        repoRetailerPrice.Name = colRetailerPrice
        repoRetailerPrice.Width = 100
        repoRetailerPrice.ReadOnly = True
        repoRetailerPrice.FormatString = "{0:F2}"
        repoRetailerPrice.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRetailerPrice)

        Dim repoLoadOutAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLoadOutAmt = New GridViewDecimalColumn()
        repoLoadOutAmt.FormatString = ""
        repoLoadOutAmt.HeaderText = "Provisional Sale Amount"
        repoLoadOutAmt.Name = colAmountLoadOut
        repoLoadOutAmt.Width = 100
        repoLoadOutAmt.ReadOnly = True
        repoLoadOutAmt.FormatString = "{0:F2}"
        repoLoadOutAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLoadOutAmt)


        Dim repoFOcInFC As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFOcInFC = New GridViewDecimalColumn()
        repoFOcInFC.FormatString = ""
        repoFOcInFC.HeaderText = "FOC Qty(FC)"
        repoFOcInFC.Name = colFOCinFC
        repoFOcInFC.Width = 100
        repoFOcInFC.ReadOnly = False
        repoFOcInFC.FormatString = "{0:F2}"
        repoFOcInFC.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFOcInFC)

        Dim repoFOcInFB As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFOcInFB = New GridViewDecimalColumn()
        repoFOcInFB.FormatString = ""
        repoFOcInFB.HeaderText = "FOC Qty(FB)"
        repoFOcInFB.Name = colFOCinFB
        repoFOcInFB.Width = 100
        repoFOcInFB.ReadOnly = False
        repoFOcInFB.FormatString = "{0:F2}"
        repoFOcInFB.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFOcInFB)


        Dim repoTotalFOCQtyInFC As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalFOCQtyInFC = New GridViewDecimalColumn()
        repoTotalFOCQtyInFC.FormatString = ""
        repoTotalFOCQtyInFC.HeaderText = "Total FOC Qty(FC)"
        repoTotalFOCQtyInFC.Name = colTotalFOCinFC
        repoTotalFOCQtyInFC.Width = 100
        repoTotalFOCQtyInFC.ReadOnly = True
        repoTotalFOCQtyInFC.FormatString = "{0:F2}"
        repoTotalFOCQtyInFC.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotalFOCQtyInFC)

        Dim repoNetLoadQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNetLoadQty = New GridViewDecimalColumn()
        repoNetLoadQty.FormatString = ""
        repoNetLoadQty.HeaderText = "Net LoadQty(FC)"
        repoNetLoadQty.Name = colNetLoadQty
        repoNetLoadQty.Width = 100
        repoNetLoadQty.ReadOnly = True
        repoNetLoadQty.FormatString = "{0:F2}"
        repoNetLoadQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNetLoadQty)

        Dim repoSchemeAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSchemeAmt = New GridViewDecimalColumn()
        repoSchemeAmt.FormatString = ""
        repoSchemeAmt.HeaderText = "Scheme Amount"
        repoSchemeAmt.Name = colSchemeAmt
        repoSchemeAmt.Width = 100
        repoSchemeAmt.ReadOnly = True
        repoSchemeAmt.FormatString = "{0:F2}"
        repoSchemeAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSchemeAmt)



        Dim repoNetSaleAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNetSaleAmt = New GridViewDecimalColumn()
        repoNetSaleAmt.FormatString = ""
        repoNetSaleAmt.HeaderText = "Net Sale Amount"
        repoNetSaleAmt.Name = colNetSaleAmt
        repoNetSaleAmt.Width = 100
        repoNetSaleAmt.ReadOnly = True
        repoNetSaleAmt.FormatString = "{0:F2}"
        repoNetSaleAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNetSaleAmt)




        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = True
        gv1.EnableFiltering = True
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem(colLoadOutQty, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem(colLoadInQtyFC, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem(colLoadInQtyFB, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem(colTotalLoadInQtyFC, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem(colProvisionalSale, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem(colRetailerPrice, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem(colAmountLoadOut, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem(colFOCinFC, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem(colFOCinFB, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem(colTotalFOCinFC, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem(colNetSaleAmt, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem(colNetLoadQty, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        Dim item13 As New GridViewSummaryItem(colSchemeAmt, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Sub BindLoadOutDetails()
        'Dim Qry As String = " select 0 AS [FOCQtyFC],0 as [FOCQtyFB],0 as [TotalFOCQtyInFC] ,0 as [NetSaleAmount], xxx.MRP , xxx.Item_Code,xxx.Item_Desc,xxx.LoadOutQty,ISNULL(xxx.LoadOInFC,0) AS LoadOInFC ,ISNULL(xxx.LoadInFB,0) AS LoadInFB ,xxx.TotQtyFC , xxx.LoadOutQty-xxx.TotQtyFC as [PSQty] ,xxx.[Retailer Price] as [Retailer Price],(xxx.[Retailer Price]*(xxx.LoadOutQty-xxx.TotQtyFC)) as [Amount] from (select TSPL_TRANSFER_DETAIL.MRP , TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc ,(LoadIn_Qty+Leak+Burst+Shortage ) as [LoadOInFC],(select isnull(Item_Qty,0)  from TSPL_TRANSFER_DETAIL L where l.Transfer_No =TSPL_TRANSFER_HEAD.Load_Out_No and L.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code AND L.MRP=TSPL_TRANSFER_DETAIL.MRP  ) as [LoadOutQty],( select isnull(LoadIn_Qty+Leak+Burst+Shortage ,0)  from TSPL_TRANSFER_DETAIL L1  where L1.Uom='FB' and L1.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No and L1.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code AND L1.MRP=TSPL_TRANSFER_DETAIL.MRP/(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =L1.Item_Code  and UOM_Code ='FB')) as [LoadInFB],(select isnull(sum(Total_QtyInCase),0)  from TSPL_TRANSFER_DETAIL L1  where  L1.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No and L1.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code AND L1.MRP=TSPL_TRANSFER_DETAIL.MRP/(" & _
        '                    " select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where  L1.Uom='FB' AND Item_Code =L1.Item_Code  and UOM_Code ='FB')) +isnull " & _
        '                    " ((LoadIn_Qty+Leak+Burst+Shortage ),0) as [TotQtyFC] ,(BasicPrice_WithTax+TPT_Value  ) as [Retailer Price] from TSPL_TRANSFER_DETAIL inner join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_DETAIL.Transfer_No =TSPL_TRANSFER_HEAD.Transfer_No where TSPL_TRANSFER_HEAD.Load_Out_No='" + LoadOutNo + "' and (TSPL_TRANSFER_DETAIL.Uom='FC'  OR TSPL_TRANSFER_DETAIL.Uom='SH') " & _
        '                    " ) as xxx inner join TSPL_ITEM_MASTER on xxx.Item_Code =TSPL_ITEM_MASTER.Item_Code order by TSPL_ITEM_MASTER.Sku_Seq "
        Dim Qry As String = "    select case when max(xxx.Uom)='SH' then 0 else   ( sum(isnull(xxx.LoadOutQty,0))- sum(isnull(xxx.TotQtyFC,0))-sum(isnull(xxx.Total_FOC_FCQty,0)) ) end as NetLoad_Qty , (sum(isnull(xxx.Total_FOC_FCQty,0)) * max(xxx.[Retailer Price]))   as FOC_Amount,  sum(xxx.FOC_FCQty) as FOCQtyFC ,sum(xxx.FOC_FBQty)  as [FOCQtyFB],sum(xxx.Total_FOC_FCQty)  as [TotalFOCQtyInFC] ,((sum(isnull(xxx.LoadOutQty,0))- sum(isnull(xxx.TotQtyFC,0))-sum(isnull(xxx.Total_FOC_FCQty,0)) )* max(xxx.[Retailer Price]) )   as [NetSaleAmount],   xxx.MRP , xxx.Item_Code,max(xxx.Item_Desc) as Item_Desc,sum(isnull(xxx.LoadOutQty,0)) as [LoadOutQty],sum(ISNULL(xxx.LoadOInFC,0)) AS LoadOInFC ,sum(ISNULL(xxx.LoadInFB,0)) AS LoadInFB ,sum(xxx.TotQtyFC) as TotQtyFC ,case when max(xxx.Uom) ='SH' then 0 else ( sum(isnull(xxx.LoadOutQty,0))- sum(isnull(xxx.TotQtyFC,0))) end as [PSQty] ,max(xxx.[Retailer Price]) as [Retailer Price],(max(xxx.[Retailer Price])*(sum(isnull(xxx.LoadOutQty,0))-sum(isnull(xxx.TotQtyFC,0)))) as [Amount]  from (  select 0 as  FOC_FCQty, 0 as FOC_FBQty,0 as  Total_FOC_FCQty,0 as  NetSale_Amount  , TSPL_TRANSFER_DETAIL.MRP , TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Item_Desc ,(LoadIn_Qty+Leak+Burst+Shortage ) as [LoadOInFC],(select isnull(Item_Qty,0)  from TSPL_TRANSFER_DETAIL L where l.Transfer_No =TSPL_TRANSFER_HEAD.Load_Out_No and L.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code AND L.MRP=TSPL_TRANSFER_DETAIL.MRP  ) as [LoadOutQty],( select isnull(LoadIn_Qty+Leak+Burst+Shortage ,0)  from TSPL_TRANSFER_DETAIL L1  where L1.Uom='FB' and L1.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No and L1.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code AND L1.MRP=TSPL_TRANSFER_DETAIL.MRP/(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code =L1.Item_Code  and UOM_Code ='FB')) as [LoadInFB],(select isnull(sum(Total_QtyInCase),0)  from TSPL_TRANSFER_DETAIL L1  where  L1.Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No and L1.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code AND L1.MRP=TSPL_TRANSFER_DETAIL.MRP/( select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where  L1.Uom='FB' AND Item_Code =L1.Item_Code  and UOM_Code ='FB')) +isnull  ((LoadIn_Qty+Leak+Burst+Shortage ),0) as [TotQtyFC] ,(BasicPrice_WithTax+TPT_Value  ) as [Retailer Price]  ,TSPL_TRANSFER_DETAIL.Uom from TSPL_TRANSFER_DETAIL inner join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_DETAIL.Transfer_No =TSPL_TRANSFER_HEAD.Transfer_No where TSPL_TRANSFER_HEAD.Load_Out_No='" + LoadOutNo + "' and (TSPL_TRANSFER_DETAIL.Uom='FC'  OR TSPL_TRANSFER_DETAIL.Uom='SH')  union all" & _
                           "  SELECT    FOC_FCQty, FOC_FBQty, Total_FOC_FCQty, NetSale_Amount ,mrp,item_code,item_name as [item_desc], 0 as loadinfoc,0 as loadoutqty,0 as loadinfb,0 as totqtyfc,Retailer_Price as retailerprice ,''            " & _
                           "  FROM tspl_QuickSettleMent_Item_Detail WHERE Transfer_Number = '" + LoadOutNo + "') as xxx inner join TSPL_ITEM_MASTER on xxx.Item_Code =TSPL_ITEM_MASTER.Item_Code  group by xxx.Item_Code ,xxx.MRP  " & _
                           "  order by max(TSPL_ITEM_MASTER.Sku_Seq )"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        gv1.DataSource = Nothing
        gv1.AutoGenerateColumns = False
        'gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.DataSource = dt
        gv1.Columns(colItemCode).FieldName = "Item_Code"
        gv1.Columns(colItemName).FieldName = "Item_Desc"
        gv1.Columns(colMRP).FieldName = "MRP"
        gv1.Columns(colLoadOutQty).FieldName = "LoadOutQty"
        gv1.Columns(colLoadInQtyFC).FieldName = "LoadOInFC"
        gv1.Columns(colLoadInQtyFB).FieldName = "LoadInFB"
        gv1.Columns(colTotalLoadInQtyFC).FieldName = "TotQtyFC"
        gv1.Columns(colProvisionalSale).FieldName = "PSQty"
        gv1.Columns(colRetailerPrice).FieldName = "Retailer Price"
        gv1.Columns(colAmountLoadOut).FieldName = "Amount"
        gv1.Columns(colFOCinFC).FieldName = "FOCQtyFC"
        gv1.Columns(colFOCinFB).FieldName = "FOCQtyFB"
        gv1.Columns(colTotalFOCinFC).FieldName = "TotalFOCQtyInFC"
        gv1.Columns(colNetSaleAmt).FieldName = "NetSaleAmount"
        gv1.Columns(colNetLoadQty).FieldName = "NetLoad_Qty"
        gv1.Columns(colSchemeAmt).FieldName = "FOC_Amount"
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Dim UOM As String = Nothing
        If e.Column Is gv1.Columns(colFOCinFB) Or e.Column Is gv1.Columns(colFOCinFC) Then
            Dim UOMCF As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), "FB", Nothing)
            If UOMCF > 1 Then
                ''''added by priti on 17/09/12
                Dim dblNetLoadqty, dblpendingqtyInbottle As Double
                dblpendingqtyInbottle = clsDBFuncationality.getSingleValue("select Pending_Balance_In_Bottle from TSPL_TRANSFER_DETAIL where Transfer_No='" & LoadOutNo & "' and MRP='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colMRP).Value) & "' and Item_Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value) & "' and Uom='fc'")
                dblNetLoadqty = (dblpendingqtyInbottle - ((clsCommon.myCdbl(gv1.CurrentRow.Cells(colFOCinFC).Value) * UOMCF) + (clsCommon.myCdbl(gv1.CurrentRow.Cells(colFOCinFB).Value)))) / UOMCF

                gv1.CurrentRow.Cells(colTotalFOCinFC).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colFOCinFC).Value) + (clsCommon.myCdbl(gv1.CurrentRow.Cells(colFOCinFB).Value) / UOMCF)
                gv1.CurrentRow.Cells(colNetLoadQty).Value = dblNetLoadqty
                gv1.CurrentRow.Cells(colNetSaleAmt).Value = dblNetLoadqty * clsCommon.myCdbl(gv1.CurrentRow.Cells(colRetailerPrice).Value)
                gv1.CurrentRow.Cells(colSchemeAmt).Value = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotalFOCinFC).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colRetailerPrice).Value), 2)

                'gv1.CurrentRow.Cells(colTotalFOCinFC).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colFOCinFC).Value) + (clsCommon.myCdbl(gv1.CurrentRow.Cells(colFOCinFB).Value) / UOMCF)
                'gv1.CurrentRow.Cells(colNetSaleAmt).Value = Math.Round((clsCommon.myCdbl(gv1.CurrentRow.Cells(colProvisionalSale).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotalFOCinFC).Value)) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colRetailerPrice).Value), 2)
                'gv1.CurrentRow.Cells(colNetLoadQty).Value = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colProvisionalSale).Value) - clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotalFOCinFC).Value), 2)
                'gv1.CurrentRow.Cells(colSchemeAmt).Value = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotalFOCinFC).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colRetailerPrice).Value), 2)
            End If
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        ''Dim isOKApplicable As Boolean = False
        ''For ii As Integer = 0 To gv1.Columns.Count - 1
        ''    If gv1.Columns(ii) Is gv1.CurrentColumn Then
        ''        isOKApplicable = True
        ''        Exit For
        ''    End If
        ''Next
        ''If isOKApplicable Then
        ''    If gv1.Rows.Count > 0 Then
        ''        Dim LoadInNo As String = clsDBFuncationality.getSingleValue("select Transfer_No  from TSPL_TRANSFER_HEAD where Load_Out_No='" + LoadOutNo + "'")
        ''        Dim ItemCOde As String = gv1.CurrentRow.Cells(colItemCode).Value
        ''        If clsCommon.myLen(LoadInNo) > 0 Then
        ''            Dim frm As New frmTransfer(LoadInNo, ItemCOde, userCode, companyCode)
        ''            frm.ShowDialog()
        ''            Dim frmSettlement As New FrmQuickSettlement(userCode, companyCode)
        ''            frmSettlement.fndTransferNumber_TextChanged()
        ''            BindLoadOutDetails()
        ''        End If
        ''    End If
        ''End If
    End Sub

    Private Sub btnUpdateLoadIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateLoadIn.Click
        If gv1.Rows.Count > 0 Then
            Dim LoadInNo As String = clsDBFuncationality.getSingleValue("select Transfer_No  from TSPL_TRANSFER_HEAD where Load_Out_No='" + LoadOutNo + "'")
            If clsCommon.myLen(LoadInNo) > 0 Then
                Dim frm As New frmTransferNew(LoadInNo, Nothing, userCode, companyCode)
                frm.ShowDialog()
                Dim frmSettlement As New FrmQuickSettlement(userCode, companyCode)
                frmSettlement.fndTransferNumber_TextChanged()
                BindLoadOutDetails()
                ReStoreGridLayout()
            End If
        End If
    End Sub

    Private Sub FrmViewLoadOutDetail_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If gv1.Rows.Count > 0 Then
            For Each grow As GridViewRowInfo In gv1.Rows
                ProvisionalSale_Amount = ProvisionalSale_Amount + clsCommon.myCdbl(grow.Cells(colAmountLoadOut).Value)
                NetSale_Amount = NetSale_Amount + clsCommon.myCdbl(grow.Cells(colNetSaleAmt).Value)
                Scheme_Amount = Scheme_Amount + clsCommon.myCdbl(grow.Cells(colSchemeAmt).Value)
            Next
        End If
    End Sub

    Private Sub FrmViewLoadOutDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadBlankGrid()
        BindLoadOutDetails()
        Me.WindowState = FormWindowState.Maximized
        ReStoreGridLayout()

    End Sub

    


    Private Sub gv1_ViewCellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If gv1.Rows.Count > 0 Then
                funSave()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalFOCinFC).Value), 2, MidpointRounding.ToEven) > Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colProvisionalSale).Value), 2, MidpointRounding.ToEven) Then
                clsCommon.MyMessageBoxShow(Me, "Total Provision Sale " + clsCommon.myCstr(Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colProvisionalSale).Value), 2, MidpointRounding.ToEven)) + " and Total FOC Qty " + clsCommon.myCstr(Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalFOCinFC).Value), 2, MidpointRounding.ToEven)) + " At Row No" + clsCommon.myCstr(ii + 1))
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub funSave()
        If AllowToSave() Then
            Dim Arr As New List(Of clsQuickSettlementItemDetails)
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New clsQuickSettlementItemDetails()
                objTr.Quick_SettleMent_Id = strQuickSettlementId
                objTr.Transfer_Number = LoadOutNo
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objTr.Item_Name = clsCommon.myCstr(grow.Cells(colItemName).Value)
                objTr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                objTr.LoadOut_Qty = clsCommon.myCdbl(grow.Cells(colLoadOutQty).Value)
                objTr.LoadInFC_Qty = clsCommon.myCdbl(grow.Cells(colLoadInQtyFC).Value)
                objTr.LoadInFB_Qty = clsCommon.myCdbl(grow.Cells(colLoadInQtyFB).Value)
                objTr.Total_LoadInFC_Qty = clsCommon.myCdbl(grow.Cells(colTotalLoadInQtyFC).Value)
                objTr.Provisional_SaleQty = clsCommon.myCdbl(grow.Cells(colProvisionalSale).Value)
                objTr.Retailer_Price = clsCommon.myCdbl(grow.Cells(colRetailerPrice).Value)
                objTr.Provisional_Sale_Amount = clsCommon.myCdbl(grow.Cells(colAmountLoadOut).Value)
                objTr.FOC_FCQty = clsCommon.myCdbl(grow.Cells(colFOCinFC).Value)
                objTr.FOC_FBQty = clsCommon.myCdbl(grow.Cells(colFOCinFB).Value)
                objTr.Total_FOC_FCQty = clsCommon.myCdbl(grow.Cells(colTotalFOCinFC).Value)
                objTr.NetSale_Amount = clsCommon.myCdbl(grow.Cells(colNetSaleAmt).Value)
                objTr.NetLoad_Qty = clsCommon.myCdbl(grow.Cells(colNetLoadQty).Value)
                objTr.FOC_Amount = clsCommon.myCdbl(grow.Cells(colSchemeAmt).Value)
                Arr.Add(objTr)
            Next
            If (clsQuickSettlementItemDetails.funSave(Arr, strQuickSettlementId, LoadOutNo)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                funFIll(strQuickSettlementId, LoadOutNo)
                ReStoreGridLayout()
            End If
        End If
    End Sub
    'Private Function UpdateTotal() As Boolean
    '    If gv1.Rows.Count > 0 Then
    '        For Each grow As GridViewRowInfo In gv1.Rows
    '            Net_LoadOutQty = Net_LoadOutQty + clsCommon.myCdbl(grow.Cells(colLoadOutQty).Value)
    '            Net_LoadInQty = Net_LoadInQty + clsCommon.myCdbl(grow.Cells(colTotalLoadInQtyFC).Value)
    '            Net_ProvisionalQty = Net_ProvisionalQty + clsCommon.myCdbl(grow.Cells(colProvisionalSale).Value)
    '            Net_FOCQty = Net_FOCQty + clsCommon.myCdbl(grow.Cells(colTotalFOCinFC).Value)
    '            Net_SalesQty = Net_SalesQty + clsCommon.myCdbl(grow.Cells(colNetLoadQty).Value)
    '        Next

    '    End If
    'End Function
    Private Sub funFIll(ByVal strDocNo As String, ByVal strTransferNo As String)
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        LoadBlankGrid()
        Dim Arr As New List(Of clsQuickSettlementItemDetails)
        Arr = clsQuickSettlementItemDetails.GetData(strDocNo, LoadOutNo)
        For Each objTr As clsQuickSettlementItemDetails In Arr
            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = objTr.Item_Name
            gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.MRP
            gv1.Rows(gv1.Rows.Count - 1).Cells(colLoadOutQty).Value = objTr.LoadOut_Qty
            gv1.Rows(gv1.Rows.Count - 1).Cells(colLoadInQtyFC).Value = objTr.LoadInFC_Qty
            gv1.Rows(gv1.Rows.Count - 1).Cells(colLoadInQtyFB).Value = objTr.LoadInFB_Qty
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalLoadInQtyFC).Value = objTr.Total_LoadInFC_Qty
            gv1.Rows(gv1.Rows.Count - 1).Cells(colProvisionalSale).Value = objTr.Provisional_SaleQty
            gv1.Rows(gv1.Rows.Count - 1).Cells(colRetailerPrice).Value = objTr.Retailer_Price
            gv1.Rows(gv1.Rows.Count - 1).Cells(colAmountLoadOut).Value = objTr.Provisional_Sale_Amount
            gv1.Rows(gv1.Rows.Count - 1).Cells(colFOCinFC).Value = objTr.FOC_FCQty
            gv1.Rows(gv1.Rows.Count - 1).Cells(colFOCinFB).Value = objTr.FOC_FBQty
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalFOCinFC).Value = objTr.Total_FOC_FCQty
            gv1.Rows(gv1.Rows.Count - 1).Cells(colNetSaleAmt).Value = objTr.NetSale_Amount
            gv1.Rows(gv1.Rows.Count - 1).Cells(colNetLoadQty).Value = objTr.NetLoad_Qty
            gv1.Rows(gv1.Rows.Count - 1).Cells(colSchemeAmt).Value = objTr.FOC_Amount
        Next
    End Sub

    Private Sub mbtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnSaveLayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub mbtnDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnDeleteLayout.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If gv1.Rows.Count > 0 Then
            Dim LoadInNo As String = clsDBFuncationality.getSingleValue("select Transfer_No  from TSPL_TRANSFER_HEAD where Load_Out_No='" + LoadOutNo + "'")
            Dim ItemCOde As String = gv1.CurrentRow.Cells(colItemCode).Value
            If clsCommon.myLen(LoadInNo) > 0 Then
                Dim frm As New frmTransferNew(LoadInNo, ItemCOde, userCode, companyCode)
                frm.ShowDialog()
                Dim frmSettlement As New FrmQuickSettlement(userCode, companyCode)
                frmSettlement.SetUserMgmt(clsUserMgtCode.frmQuickSettlement)
                frmSettlement.fndTransferNumber_TextChanged()
                BindLoadOutDetails()
            End If
        End If

    End Sub
    Public Sub closeform()
        Me.Close()
    End Sub
    Private Sub FrmViewLoadOutDetail_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S Then
            If gv1.Rows.Count > 0 Then
                funSave()
            End If
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        End If
    End Sub
End Class
