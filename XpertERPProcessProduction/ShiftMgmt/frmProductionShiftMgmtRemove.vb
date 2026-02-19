Imports common
Public Class frmProductionShiftMgmtRemove
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
    Const ColQty As String = "ColQty"
    Const ColFAT As String = "ColFAT"
    Const ColSNF As String = "ColSNF"
    Const ColFATKG As String = "ColFATKG"
    Const ColSNFKG As String = "ColSNFKG"

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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColQty).Value = obj.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFATKG).Value = obj.FAT_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNFKG).Value = obj.SNF_KG

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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColQty).Value = obj.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFATKG).Value = obj.FAT_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNFKG).Value = obj.SNF_KG

                        gv1.Rows.AddNew()
                    Next
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
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Entered Qty"
        repoNumBox.Name = ColQty
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
        repoNumBox.HeaderText = "FAT %"
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
        repoNumBox.HeaderText = "FAT KG"
        repoNumBox.Name = ColFATKG
        repoNumBox.Width = 100
        repoNumBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoNumBox.DecimalPlaces = 3
        repoNumBox.ReadOnly = False
        repoNumBox.IsVisible = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNF %"
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
        repoNumBox.HeaderText = "SNF KG"
        repoNumBox.Name = ColSNFKG
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
        Dim item1 As New GridViewSummaryItem(ColQty, "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColFATKG, "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        item1 = New GridViewSummaryItem(ColSNFKG, "{0:F3}", GridAggregateFunction.Sum)
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
                If clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColQty).Value) > 0 Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(ColItemCode).Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(ColLocationCode).Value) Then
                        Dim obj As New clsProductionShiftMgmtSFGProductionItemAddRemove
                        obj.Location_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationCode).Value)
                        obj.Location_Name = clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationName).Value)
                        obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(ColItemCode).Value)
                        obj.Item_Name = clsCommon.myCstr(gv1.Rows(ii).Cells(ColItemName).Value)
                        obj.ItemProductType = clsCommon.myCstr(gv1.Rows(ii).Cells(ColProductType).Value)
                        obj.Qty = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColQty).Value)
                        obj.UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(ColUOM).Value)
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(ColProductType).Value), "MI") = CompairStringResult.Equal Then
                            obj.FAT = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColFAT).Value)
                            obj.FAT_KG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColFATKG).Value)
                            obj.SNF = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColSNF).Value)
                            obj.SNF_KG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColSNFKG).Value)
                        End If
                        ArrSFG.Add(obj)
                    End If
                End If
            Next
        Else
            Arr = New List(Of clsProductionShiftMgmtProductionItemAddRemove)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColQty).Value) > 0 Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(ColItemCode).Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(ColLocationCode).Value) Then
                        Dim obj As New clsProductionShiftMgmtProductionItemAddRemove
                        obj.Location_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationCode).Value)
                        obj.Location_Name = clsCommon.myCstr(gv1.Rows(ii).Cells(ColLocationName).Value)
                        obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(ColItemCode).Value)
                        obj.Item_Name = clsCommon.myCstr(gv1.Rows(ii).Cells(ColItemName).Value)
                        obj.ItemProductType = clsCommon.myCstr(gv1.Rows(ii).Cells(ColProductType).Value)
                        obj.Qty = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColQty).Value)
                        obj.UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(ColUOM).Value)
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(ColProductType).Value), "MI") = CompairStringResult.Equal Then
                            obj.FAT = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColFAT).Value)
                            obj.FAT_KG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColFATKG).Value)
                            obj.SNF = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColSNF).Value)
                            obj.SNF_KG = clsCommon.myCDecimal(gv1.Rows(ii).Cells(ColSNFKG).Value)
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
                    ElseIf e.Column Is gv1.Columns(ColQty) Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColProductType).Value), "MI") = CompairStringResult.Equal Then
                            Dim dclQty As Decimal = clsItemMaster.Convert(clsCommon.myCstr(gv1.CurrentRow.Cells(ColItemCode).Value), clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColQty).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(ColUOM).Value), "KG")
                            gv1.CurrentRow.Cells(ColFATKG).Value = Math.Round(clsCommon.myCDivide(dclQty * clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColFAT).Value), 100), 3, MidpointRounding.ToEven)
                            gv1.CurrentRow.Cells(ColSNFKG).Value = Math.Round(clsCommon.myCDivide(dclQty * clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColSNF).Value), 100), 3, MidpointRounding.ToEven)
                        End If
                    ElseIf e.Column Is gv1.Columns(ColFATKG) Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColProductType).Value), "MI") = CompairStringResult.Equal Then
                            Dim dclQtyKG As Decimal = Math.Round(clsCommon.myCDivide((clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColFATKG).Value) * 100), clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColFAT).Value)), 2, MidpointRounding.ToEven)
                            gv1.CurrentRow.Cells(ColQty).Value = clsItemMaster.Convert(clsCommon.myCstr(gv1.CurrentRow.Cells(ColItemCode).Value), dclQtyKG, "KG", clsCommon.myCstr(gv1.CurrentRow.Cells(ColUOM).Value))
                            gv1.CurrentRow.Cells(ColSNFKG).Value = Math.Round(clsCommon.myCDivide(dclQtyKG * clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColSNF).Value), 100), 3, MidpointRounding.ToEven)
                        End If
                    ElseIf e.Column Is gv1.Columns(ColSNFKG) Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColProductType).Value), "MI") = CompairStringResult.Equal Then
                            Dim dclQtyKG As Decimal = Math.Round(clsCommon.myCDivide((clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColSNFKG).Value) * 100), clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColSNF).Value)), 2, MidpointRounding.ToEven)
                            gv1.CurrentRow.Cells(ColQty).Value = clsItemMaster.Convert(clsCommon.myCstr(gv1.CurrentRow.Cells(ColItemCode).Value), dclQtyKG, "KG", clsCommon.myCstr(gv1.CurrentRow.Cells(ColUOM).Value))
                            gv1.CurrentRow.Cells(ColFATKG).Value = Math.Round(clsCommon.myCDivide(dclQtyKG * clsCommon.myCDecimal(gv1.CurrentRow.Cells(ColFAT).Value), 100), 3, MidpointRounding.ToEven)
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
        gv1.CurrentRow.Cells(ColUOM).Value = clsItemMaster.GetStockUnit(clsCommon.myCstr(gv1.CurrentRow.Cells(ColItemCode).Value), Nothing)
        gv1.CurrentRow.Cells(ColProductType).Value = clsItemMaster.GetItemProductType(clsCommon.myCstr(gv1.CurrentRow.Cells(ColItemCode).Value), Nothing)
        SetFATSNFParameter(gv1.CurrentRow.Index)
    End Sub

    Private Sub SetFATSNFParameter(ByVal idx As Integer)
        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(idx).Cells(ColProductType).Value), "MI") = CompairStringResult.Equal Then
            Dim obj As MIlkComponentType = clsItemMaster.GetItemFatSNF(clsCommon.myCstr(gv1.Rows(idx).Cells(ColItemCode).Value), Nothing)
            If obj IsNot Nothing Then
                gv1.Rows(idx).Cells(ColFAT).Value = obj.FAT_Per
                gv1.Rows(idx).Cells(ColSNF).Value = obj.SNF_Per
            End If
        End If
    End Sub

    Sub OpenLocation()
        Dim whrCls As String = " (location_code in ('" + FilterLocationCode + "') or ( Main_Location_Code='" + FilterLocationCode + "' and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y' and isnull(Is_Sub_Location,'N')<>'N' and Location_Category<>'MCC'  ))"
        gv1.CurrentRow.Cells(ColLocationCode).Value = clsCommon.myCstr(clsLocation.getFinder(whrCls, gv1.CurrentRow.Cells(ColLocationCode).Value, False))
        gv1.CurrentRow.Cells(ColLocationName).Value = clsCommon.myCstr(clsLocation.GetName(clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationCode).Value), Nothing))
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
                    If e.Column Is gv1.Columns(ColFAT) OrElse e.Column Is gv1.Columns(ColFATKG) OrElse e.Column Is gv1.Columns(ColSNF) OrElse e.Column Is gv1.Columns(ColSNFKG) Then
                        gv1.CurrentRow.Cells(e.Column.Name).ReadOnly = Not (clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColProductType).Value), "MI") = CompairStringResult.Equal)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
