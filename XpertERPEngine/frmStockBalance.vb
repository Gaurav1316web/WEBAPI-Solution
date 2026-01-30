Imports System.IO
Imports Telerik.WinControls.UI.Export
Public Class frmStockBalance
    Public ReportID As String = "STCBAL"
    Public FilterItemCode As String
    Public FilterUOM As String
    Public FilterReqQty As Decimal
    Public FilterReqFATKg As Decimal
    Public FilterReqSNFKg As Decimal
    Public FilterLocationCode As String
    Public FilterDate As DateTime
    Public isForSFG As Boolean = False
    Public ArrRMIssue As List(Of clsProductionShiftMgmtProductionRMIssue) = Nothing
    Public ArrRMIssueSFG As List(Of clsProductionShiftMgmtSFGProductionRMIssue) = Nothing

    Public isOKClicked As Integer = 0
    Dim isProductTypeMI As Boolean = False

    Const ColLocationCode As String = "ColLocationCode"
    Const ColLocationName As String = "ColLocationName"
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
    Private Sub FrmFreeGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If clsCommon.myLen(ReportID) <= 0 Then
                Throw New Exception("Report ID Not found")
            End If
            Dim qry As String = "select Product_Type,Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + FilterItemCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Invalid Item [" + FilterItemCode + "]")
            End If
            Me.Text = "Available stock details for Item [" + FilterItemCode + "][" + clsCommon.myCstr(dt.Rows(0)("Item_Desc")) + "] in [" + FilterUOM + "]"
            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Product_Type")), "MI") = CompairStringResult.Equal Then
                isProductTypeMI = True
            End If
            lblReqFATKG.Visible = isProductTypeMI
            lblReqSNFKG.Visible = isProductTypeMI
            MyLabel4.Visible = isProductTypeMI
            MyLabel6.Visible = isProductTypeMI

            lblReqQty.Text = clsCommon.myFormat(FilterReqQty)
            lblReqFATKG.Text = clsCommon.myFormat(FilterReqFATKg)
            lblReqSNFKG.Text = clsCommon.myFormat(FilterReqSNFKg)
            If isProductTypeMI Then
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
where TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" + FilterItemCode + "' and TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM in ('LTR','KG') and TSPL_ITEM_MASTER.Product_Type='MI' and isnull(TSPL_LOCATION_MASTER.csa_type,'N')<>'Y' and isnull(TSPL_LOCATION_MASTER.Is_Section,'N')<>'Y'  and TSPL_LOCATION_MASTER.Location_Category<>'MCC' and  isnull(TSPL_LOCATION_MASTER.Is_Jobwork,0)=0 and isnull(TSPL_LOCATION_MASTER.GIT_Type,'N')='N' and  TSPL_LOCATION_MASTER.Main_Location_Code='" + FilterLocationCode + "' and Punching_Date<='" + clsCommon.GetPrintDate(FilterDate, "dd/MMM/yyyy hh:mm tt") + "' 
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
where TSPL_INVENTORY_MOVEMENT.Item_Code='" + FilterItemCode + "' and TSPL_INVENTORY_MOVEMENT.Stock_UOM='" + FilterUOM + "' and (TSPL_LOCATION_MASTER.Main_Location_Code='" + FilterLocationCode + "' or  TSPL_INVENTORY_MOVEMENT.Location_Code='" + FilterLocationCode + "' ) and Punching_Date<'" + clsCommon.GetPrintDate(FilterDate, "dd/MMM/yyyy hh:mm tt") + "'  
)xx group by Location_Code,Item_Code,Stock_UOM having sum(Stock_Qty*RI)>0"
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
            LoadBlankGrid()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.AutoGenerateColumns = False
                gv1.DataSource = dt
                gv1.Columns(ColLocationCode).FieldName = "Location_Code"
                gv1.Columns(ColLocationName).FieldName = "Location_Desc"
                gv1.Columns(ColAvailQty).FieldName = "Stock_Qty"
                If isProductTypeMI Then
                    gv1.Columns(ColFAT).FieldName = "FAT"
                    gv1.Columns(ColSNF).FieldName = "SNF"
                    gv1.Columns(ColFATKG).FieldName = "Fat_KG"
                    gv1.Columns(ColSNFKG).FieldName = "SNF_KG"
                End If

                gv1.ShowGroupPanel = False
                gv1.AllowAddNewRow = False
                gv1.ShowGroupPanel = False
                gv1.AllowColumnReorder = False
                gv1.AllowRowReorder = False
                gv1.EnableSorting = True
                gv1.ShowFilteringRow = True
                gv1.EnableFiltering = True
                gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
                gv1.MasterTemplate.ShowRowHeaderColumn = False
                If isForSFG Then
                    If ArrRMIssueSFG IsNot Nothing AndAlso ArrRMIssueSFG.Count > 0 Then
                        For Each obj As clsProductionShiftMgmtSFGProductionRMIssue In ArrRMIssueSFG
                            For ii As Integer = 0 To gv1.Rows.Count - 1
                                If clsCommon.CompairString(obj.Location_Code, clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationCode).Value)) = CompairStringResult.Equal Then
                                    gv1.Rows(ii).Cells(ColEnteredQty).Value = obj.Qty
                                    If isProductTypeMI Then
                                        gv1.Rows(ii).Cells(ColEnteredFATKG).Value = obj.FAT_KG
                                        gv1.Rows(ii).Cells(ColEnteredSNFKG).Value = obj.SNF_KG
                                    End If
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                Else
                    If ArrRMIssue IsNot Nothing AndAlso ArrRMIssue.Count > 0 Then
                        For Each obj As clsProductionShiftMgmtProductionRMIssue In ArrRMIssue
                            For ii As Integer = 0 To gv1.Rows.Count - 1
                                If clsCommon.CompairString(obj.Location_Code, clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationCode).Value)) = CompairStringResult.Equal Then
                                    gv1.Rows(ii).Cells(ColEnteredQty).Value = obj.Qty
                                    If isProductTypeMI Then
                                        gv1.Rows(ii).Cells(ColEnteredFATKG).Value = obj.FAT_KG
                                        gv1.Rows(ii).Cells(ColEnteredSNFKG).Value = obj.SNF_KG
                                    End If
                                    Exit For
                                End If
                            Next
                        Next
                    End If
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Me.Close()
        End Try
    End Sub
    Sub LoadBlankGrid()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()


        Dim repoTextBox As New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Location Code"
        repoTextBox.Name = ColLocationCode
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Location"
        repoTextBox.Name = ColLocationName
        repoTextBox.Width = 120
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)



        Dim repoNumBox As New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Balance Qty"
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
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = isProductTypeMI
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Avail FAT KG"
        repoNumBox.Name = ColFATKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.IsVisible = isProductTypeMI
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Entered FAT KG"
        repoNumBox.Name = ColEnteredFATKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = isProductTypeMI
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Avail SNF %"
        repoNumBox.Name = ColSNF
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 2
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = isProductTypeMI
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Avail SNF KG"
        repoNumBox.Name = ColSNFKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = isProductTypeMI
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Entered SNF KG"
        repoNumBox.Name = ColEnteredSNFKG
        repoNumBox.Width = 80
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoNumBox.IsVisible = isProductTypeMI
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
        If isForSFG Then
            ArrRMIssueSFG = New List(Of clsProductionShiftMgmtSFGProductionRMIssue)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredQty).Value) > 0 Then
                    Dim obj As New clsProductionShiftMgmtSFGProductionRMIssue
                    obj.Location_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationCode).Value)
                    obj.Item_Code = FilterItemCode
                    obj.Qty = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredQty).Value)
                    obj.UOM = FilterUOM
                    If isProductTypeMI Then
                        obj.FAT = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColFAT).Value)
                        obj.FAT_KG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredFATKG).Value)
                        obj.SNF = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColSNF).Value)
                        obj.SNF_KG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredSNFKG).Value)
                    End If
                    ArrRMIssueSFG.Add(obj)
                End If
            Next
        Else
            ArrRMIssue = New List(Of clsProductionShiftMgmtProductionRMIssue)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredQty).Value) > 0 Then
                    Dim obj As New clsProductionShiftMgmtProductionRMIssue
                    obj.Location_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationCode).Value)
                    obj.Item_Code = FilterItemCode
                    obj.Qty = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredQty).Value)
                    obj.UOM = FilterUOM
                    If isProductTypeMI Then
                        obj.FAT = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColFAT).Value)
                        obj.FAT_KG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredFATKG).Value)
                        obj.SNF = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColSNF).Value)
                        obj.SNF_KG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColEnteredSNFKG).Value)
                    End If
                    ArrRMIssue.Add(obj)
                End If
            Next
        End If

        Me.Close()
    End Sub
    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        isOKClicked = 2
        If isForSFG Then
            ArrRMIssueSFG = Nothing
        Else
            ArrRMIssue = Nothing
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
                    If e.Column Is gv1.Columns(ColEnteredQty) Then
                        If gv1.CurrentRow.Cells(ColEnteredQty).Value > gv1.CurrentRow.Cells(ColAvailQty).Value Then
                            gv1.CurrentRow.Cells(ColEnteredQty).Value = 0
                            Throw New Exception("Entered Qty Cant be more thant [" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColAvailQty).Value) + "] ")
                        End If
                        If isProductTypeMI Then
                            gv1.CurrentRow.Cells(ColEnteredFATKG).Value = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColFATKG).Value) * clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColEnteredQty).Value), clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColAvailQty).Value)), 3, MidpointRounding.ToEven)
                            gv1.CurrentRow.Cells(ColEnteredSNFKG).Value = Math.Round(clsCommon.myCDivide(clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColSNFKG).Value) * clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColEnteredQty).Value), clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColAvailQty).Value)), 3, MidpointRounding.ToEven)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
