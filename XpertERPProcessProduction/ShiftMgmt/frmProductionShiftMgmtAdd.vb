Imports common
Public Class frmProductionShiftMgmtAdd
#Region "Variables"
    Public ReportID As String = "AddF"
    Public FilterLocationCode As String
    Public FilterDate As DateTime
    Public isSFG As Boolean = False
    Public ArrSFG As List(Of clsProductionShiftMgmtSFGProductionItemAddRemove) = Nothing
    Public Arr As List(Of clsProductionShiftMgmtProductionItemAddRemove) = Nothing
    Public isOKClicked As Integer = 0


    Const ColLocationCode As String = "ColLocationCode"
    Const ColLocationName As String = "ColLocationName"
    Const ColItemCode As String = "ColItemCode"
    Const ColItemName As String = "ColItemName"
    Const ColProductType As String = "ColProductType"
    Const ColUOM As String = "ColUOM"
    Const ColAvailQty As String = "ColAvailQty"
    Const ColEnteredQty As String = "ColEnteredQty"
    Const ColFAT As String = "ColFAT"
    Const ColSNF As String = "ColSNF"
    Const ColFATKG As String = "ColFATKG"
    Const ColEnteredFATKG As String = "ColEnteredFATKG"
    Const ColSNFKG As String = "ColSNFKG"
    Const ColEnteredSNFKG As String = "ColEnteredSNFKG"

    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
#End Region
    Private Sub FrmFreeGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If clsCommon.myLen(ReportID) <= 0 Then
                Throw New Exception("Report ID Not found")
            End If
            LoadBlankGrid()
            If isSFG Then
                If ArrSFG IsNot Nothing AndAlso ArrSFG.Count > 0 Then
                    For Each obj As clsProductionShiftMgmtSFGProductionItemAddRemove In ArrSFG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationCode).Value = obj.Location_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationName).Value = obj.Location_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColItemCode).Value = obj.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColItemName).Value = obj.Item_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColProductType).Value = obj.ItemProductType
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColUOM).Value = obj.UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColEnteredQty).Value = obj.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColEnteredFATKG).Value = obj.FAT_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColEnteredSNFKG).Value = obj.SNF_KG
                        SetBalance(gv1.Rows.Count - 1)
                        gv1.Rows.AddNew()
                    Next
                End If
            Else
                If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                    For Each obj As clsProductionShiftMgmtProductionItemAddRemove In Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationCode).Value = obj.Location_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationName).Value = obj.Location_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColItemCode).Value = obj.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColItemName).Value = obj.Item_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColProductType).Value = obj.ItemProductType
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColUOM).Value = obj.UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColEnteredQty).Value = obj.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColEnteredFATKG).Value = obj.FAT_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColEnteredSNFKG).Value = obj.SNF_KG
                        SetBalance(gv1.Rows.Count - 1)
                        gv1.Rows.AddNew()
                    Next
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Me.Close()
        End Try
    End Sub
    Sub SetBalance(idx As Integer)
        If clsCommon.myLen(gv1.Rows(idx).Cells(ColLocationCode).Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(idx).Cells(ColItemCode).Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(idx).Cells(ColUOM).Value) > 0 Then
            Dim qry As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(idx).Cells(ColProductType).Value), "MI") = CompairStringResult.Equal Then
                qry = "select xxxx.Location_Code,xxxx.Location_Desc,xxxx.Stock_Qty,case when Stock_Qty_KG>0 then cast((Fat_KG*100/Stock_Qty_KG) as decimal(18,2)) else 0 end FAT,xxxx.Fat_KG,case when Stock_Qty_KG>0 then cast((SNF_KG*100/Stock_Qty_KG) as decimal(18,2)) else 0 end SNF,xxxx.SNF_KG from (
        select xxx.Location_Code,xxx.Location_Desc,xxx.Item_Code,xxx.Item_Desc,xxx.Stock_Qty 
        ,case when xxx.Stock_UOM='KG' then xxx.Stock_Qty else xxx.Stock_Qty/TabUOMKG.Conversion_Factor end as Stock_Qty_KG
        ,xxx.Fat_KG,xxx.SNF_KG  from (
        select Location_Code,max(Location_Desc) as Location_Desc,Item_Code,max(Item_Desc) as Item_Desc,sum(Stock_Qty*RI) as Stock_Qty,Stock_UOM,sum(Fat_KG*RI) as Fat_KG,sum(SNF_KG*RI) as SNF_KG from (
        select TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_ITEM_MASTER.Item_Desc,case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' then 1 else -1 end as RI,TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost,TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM,
        cast( TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG as decimal(18,3)) as Fat_KG ,cast(TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG as decimal(18,3)) as SNF_KG
         from TSPL_INVENTORY_MOVEMENT_NEW 
        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code
        left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT_NEW.Location_Code
        where TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" + clsCommon.myCstr(gv1.Rows(idx).Cells(ColItemCode).Value) + "' and TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM in ('LTR','KG') and TSPL_ITEM_MASTER.Product_Type='MI' and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + clsCommon.myCstr(gv1.Rows(idx).Cells(ColLocationCode).Value) + "' and Punching_Date<='" + clsCommon.GetPrintDate(FilterDate, "dd/MMM/yyyy hh:mm tt") + "' 
        )xx group by Location_Code,Item_Code,Stock_UOM
        )xxx 
        left outer join TSPL_ITEM_UOM_DETAIL as TabUOMKG on TabUOMKG.Item_Code=xxx.Item_Code and TabUOMKG.UOM_Code='KG'
        where (xxx.Stock_Qty>0 and (xxx.Fat_KG>0 or xxx.SNF_KG>0))
        )xxxx"
            Else
                qry = " select Location_Code,max(Location_Desc) as Location_Desc,sum(Stock_Qty*RI) as Stock_Qty from (
        select TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_ITEM_MASTER.Item_Desc,case when TSPL_INVENTORY_MOVEMENT.InOut='I' then 1 else -1 end as RI,TSPL_INVENTORY_MOVEMENT.Stock_Qty,TSPL_INVENTORY_MOVEMENT.Stock_UOM
        from TSPL_INVENTORY_MOVEMENT 
        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code
        left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT.Location_Code
        where TSPL_INVENTORY_MOVEMENT.Item_Code='" + clsCommon.myCstr(gv1.Rows(idx).Cells(ColItemCode).Value) + "' and TSPL_INVENTORY_MOVEMENT.Stock_UOM='" + clsCommon.myCstr(gv1.Rows(idx).Cells(ColUOM).Value) + "' and   TSPL_INVENTORY_MOVEMENT.Location_Code='" + clsCommon.myCstr(gv1.Rows(idx).Cells(ColLocationCode).Value) + "'   and Punching_Date<='" + clsCommon.GetPrintDate(FilterDate, "dd/MMM/yyyy hh:mm tt") + "'  
        )xx group by Location_Code,Item_Code,Stock_UOM having sum(Stock_Qty*RI)>0"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.Rows(idx).Cells(ColAvailQty).Value = clsCommon.myCDecimal(dt.Rows(0)("Stock_Qty"))
                If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(idx).Cells(ColProductType).Value), "MI") = CompairStringResult.Equal Then
                    gv1.Rows(idx).Cells(ColFAT).Value = clsCommon.myCDecimal(dt.Rows(0)("FAT"))
                    gv1.Rows(idx).Cells(ColSNF).Value = clsCommon.myCDecimal(dt.Rows(0)("SNF"))
                    gv1.Rows(idx).Cells(ColFATKG).Value = clsCommon.myCDecimal(dt.Rows(0)("Fat_KG"))
                    gv1.Rows(idx).Cells(ColSNFKG).Value = clsCommon.myCDecimal(dt.Rows(0)("SNF_KG"))
                End If
            End If
        End If
    End Sub
    Sub LoadBlankGrid()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()

        Dim repoTextBox As New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Location Code"
        repoTextBox.Name = ColLocationCode
        repoTextBox.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 120
        repoTextBox.ReadOnly = False
        repoTextBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Location"
        repoTextBox.Name = ColLocationName
        repoTextBox.Width = 150
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Code"
        repoTextBox.Name = ColItemCode
        repoTextBox.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = False
        repoTextBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item"
        repoTextBox.Name = ColItemName
        repoTextBox.Width = 120
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Product Type"
        repoTextBox.Name = ColProductType
        repoTextBox.Width = 120
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "UOM"
        repoTextBox.Name = ColUOM
        repoTextBox.Width = 120
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoNumBox As New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Availa Qty"
        repoNumBox.Name = ColAvailQty
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Entered Qty"
        repoNumBox.Name = ColEnteredQty
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)


        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Avail FAT %"
        repoNumBox.Name = ColFAT
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Avail FAT KG"
        repoNumBox.Name = ColFATKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Entered FAT KG"
        repoNumBox.Name = ColEnteredFATKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Avail SNF %"
        repoNumBox.Name = ColSNF
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Avail SNF KG"
        repoNumBox.Name = ColSNFKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Entered SNF KG"
        repoNumBox.Name = ColEnteredSNFKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = False
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoNumBox)



        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AllowDeleteRow = False
        gv1.ShowFilteringRow = False
        gv1.EnableFiltering = False
        gv1.MasterTemplate.SummaryRowsBottom.Clear()




        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem(ColAvailQty, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColEnteredQty, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColFATKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColSNFKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColEnteredFATKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColEnteredSNFKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        gv1.Rows.AddNew()
    End Sub
    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        Try
            If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
                e.CellElement.TextAlignment = ContentAlignment.MiddleRight
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnopen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnopen.Click
        isOKClicked = 1
        If isSFG Then
            ArrSFG = New List(Of clsProductionShiftMgmtSFGProductionItemAddRemove)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredQty).Value) > 0 Then
                    If clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredQty).Value) <= clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColAvailQty).Value) Then
                        Dim obj As New clsProductionShiftMgmtSFGProductionItemAddRemove
                        obj.Location_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationCode).Value)
                        obj.Location_Name = clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationName).Value)
                        obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(ColItemCode).Value)
                        obj.Item_Name = clsCommon.myCstr(gv1.Rows(ii).Cells(ColItemName).Value)
                        obj.ItemProductType = clsCommon.myCstr(gv1.Rows(ii).Cells(ColProductType).Value)
                        obj.Qty = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredQty).Value)
                        obj.UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(ColUOM).Value)
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(ColProductType).Value), "MI") = CompairStringResult.Equal Then
                            obj.FAT = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColFAT).Value)
                            obj.FAT_KG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredFATKG).Value)
                            obj.SNF = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColSNF).Value)
                            obj.SNF_KG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredSNFKG).Value)
                        End If
                        ArrSFG.Add(obj)
                    End If
                End If
            Next
        Else
            Arr = New List(Of clsProductionShiftMgmtProductionItemAddRemove)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredQty).Value) > 0 Then
                    If clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredQty).Value) <= clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColAvailQty).Value) Then
                        Dim obj As New clsProductionShiftMgmtProductionItemAddRemove
                        obj.Location_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationCode).Value)
                        obj.Location_Name = clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationName).Value)
                        obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(ColItemCode).Value)
                        obj.Item_Name = clsCommon.myCstr(gv1.Rows(ii).Cells(ColItemName).Value)
                        obj.ItemProductType = clsCommon.myCstr(gv1.Rows(ii).Cells(ColProductType).Value)
                        obj.Qty = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredQty).Value)
                        obj.UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(ColUOM).Value)
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(ColProductType).Value), "MI") = CompairStringResult.Equal Then
                            obj.FAT = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColFAT).Value)
                            obj.FAT_KG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredFATKG).Value)
                            obj.SNF = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColSNF).Value)
                            obj.SNF_KG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredSNFKG).Value)
                        End If
                        Arr.Add(obj)
                    End If
                End If
            Next
        End If

        Me.Close()
    End Sub
    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        isOKClicked = 2
        If isSFG Then
            ArrSFG = Nothing
        Else
            Arr = Nothing
        End If
        Me.Close()
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        isOKClicked = 3
        Me.Close()
    End Sub
    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(ColLocationCode) Then
                        OpenLocation()
                    ElseIf e.Column Is gv1.Columns(ColItemCode) Then
                        OpenItem(False)
                    ElseIf e.Column Is gv1.Columns(ColEnteredQty) Then
                        If gv1.CurrentRow.Cells(ColEnteredQty).Value > gv1.CurrentRow.Cells(ColAvailQty).Value Then
                            gv1.CurrentRow.Cells(ColEnteredQty).Value = 0
                            Throw New Exception("Entered Qty Can't be more than [" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColAvailQty).Value) + "] ")
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColProductType).Value), "MI") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(ColEnteredFATKG).Value = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColFATKG).Value) * clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColEnteredQty).Value), clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColAvailQty).Value)), 3, MidpointRounding.ToEven)
                            gv1.CurrentRow.Cells(ColEnteredSNFKG).Value = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColSNFKG).Value) * clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColEnteredQty).Value), clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColAvailQty).Value)), 3, MidpointRounding.ToEven)
                        End If
                    ElseIf e.Column Is gv1.Columns(ColEnteredFATKG) Then
                        If gv1.CurrentRow.Cells(ColEnteredFATKG).Value > gv1.CurrentRow.Cells(ColFATKG).Value Then
                            gv1.CurrentRow.Cells(ColEnteredQty).Value = 0
                            Throw New Exception("Entered FAT KG Can't be more than [" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColFATKG).Value) + "] ")
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColProductType).Value), "MI") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(ColEnteredQty).Value = Math.Round(clsCommon.myCDivide((clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColEnteredFATKG).Value) * 100), clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColFAT).Value)), 2, MidpointRounding.ToEven)
                            gv1.CurrentRow.Cells(ColEnteredSNFKG).Value = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColSNFKG).Value) * clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColEnteredQty).Value), clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColAvailQty).Value)), 3, MidpointRounding.ToEven)
                        End If
                    ElseIf e.Column Is gv1.Columns(ColEnteredSNFKG) Then
                        If gv1.CurrentRow.Cells(ColEnteredSNFKG).Value > gv1.CurrentRow.Cells(ColSNFKG).Value Then
                            gv1.CurrentRow.Cells(ColEnteredQty).Value = 0
                            Throw New Exception("Entered SNF KG Can't be more than [" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColSNFKG).Value) + "] ")
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColProductType).Value), "MI") = CompairStringResult.Equal Then
                            gv1.CurrentRow.Cells(ColEnteredQty).Value = Math.Round(clsCommon.myCDivide((clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColEnteredSNFKG).Value) * 100), clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColSNF).Value)), 2, MidpointRounding.ToEven)
                            gv1.CurrentRow.Cells(ColEnteredFATKG).Value = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColFATKG).Value) * clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColEnteredQty).Value), clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColAvailQty).Value)), 3, MidpointRounding.ToEven)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub
    Sub OpenItem(ByVal isButtonClicked As Boolean)
        gv1.CurrentRow.Cells(ColItemCode).Value = clsItemMaster.getFinder(" tspl_item_master.Active='1' ", clsCommon.myCstr(gv1.CurrentRow.Cells(ColItemCode).Value), isButtonClicked)
        gv1.CurrentRow.Cells(ColItemName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(gv1.CurrentRow.Cells(ColItemCode).Value), Nothing)
        gv1.CurrentRow.Cells(ColProductType).Value = clsItemMaster.GetItemProductType(clsCommon.myCstr(gv1.CurrentRow.Cells(ColItemCode).Value), Nothing)
        gv1.CurrentRow.Cells(ColUOM).Value = clsItemMaster.GetStockUnit(clsCommon.myCstr(gv1.CurrentRow.Cells(ColItemCode).Value), Nothing)
        SetBalance(gv1.CurrentRow.Index)
    End Sub
    Sub OpenLocation()
        Dim whrCls As String = " (location_code in ('" + FilterLocationCode + "') or ( Main_Location_Code='" + FilterLocationCode + "' and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y' and isnull(Is_Sub_Location,'N')<>'N' and Location_Category<>'MCC'  ))"
        gv1.CurrentRow.Cells(ColLocationCode).Value = clsCommon.myCstr(clsLocation.getFinder(whrCls, gv1.CurrentRow.Cells(ColLocationCode).Value, False))
        gv1.CurrentRow.Cells(ColLocationName).Value = clsCommon.myCstr(clsLocation.GetName(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationCode).Value), Nothing))
        SetBalance(gv1.CurrentRow.Index)
    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If Not isInsideLoadData Then
                If e.RowIndex >= 0 Then
                    If e.Column Is gv1.Columns(ColEnteredFATKG) OrElse e.Column Is gv1.Columns(ColEnteredSNFKG) Then
                        gv1.CurrentRow.Cells(e.Column.Name).ReadOnly = Not (clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColProductType).Value), "MI") = CompairStringResult.Equal)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
