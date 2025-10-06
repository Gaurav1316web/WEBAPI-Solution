
Imports common
Imports System.IO


Public Class rptMonthWiseDMRReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim dtFreshItem As DataTable = New DataTable()
    Dim dtProductItem As DataTable = New DataTable()
    Dim PickRatefromMaster As Boolean = False
#End Region
    Private Sub rptMonthWiseDMRReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        PickRatefromMaster = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickRatefromMaster, clsFixedParameterCode.PickRatefromMaster, Nothing)) = 1)
        'If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "TNK") = CompairStringResult.Equal Then
        '    rbtnDemand.Visible = False
        'Else
        'End If
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        rbtnDispatch.IsChecked = True
    End Sub

    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add("Document_Date")
                If dtFreshItem.Rows.Count > 0 Then
                    For ii As Integer = 0 To dtFreshItem.Rows.Count - 1
                        view.ColumnGroups.Add(New GridViewColumnGroup(dtFreshItem.Rows(ii)("Short_Description")))
                        view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                        For col As Integer = 1 To gv1.Columns("Total_Milk_Qty").Index - 1
                            If clsCommon.CompairString(gv1.Columns(col).HeaderText, dtFreshItem.Rows(ii)("Short_Description")) = CompairStringResult.Equal OrElse clsCommon.CompairString(gv1.Columns(col).HeaderText, dtFreshItem.Rows(ii)("Item_Description")) = CompairStringResult.Equal Then
                                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
                            End If
                        Next
                    Next
                End If

                'For ii As Integer = 0 To dtProductItem.Rows.Count - 1
                '    view.ColumnGroups.Add(New GridViewColumnGroup(dtProductItem.Rows(ii)("Short_Description")))
                '    For col As Integer = gv1.Columns("Total_Milk_Amt").Index To gv1.Columns("Total_Prod_Amt").Index - 1
                '        view.ColumnGroups(ii + 1).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
                '    Next
                'Next
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Total_Milk_Qty").Name)
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Total_Milk_Amt").Name)
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("TCS_Milk").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Security").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("GrandTotalMilk").Name)

                If dtProductItem.Rows.Count > 0 Then
                    For ii As Integer = 0 To dtProductItem.Rows.Count - 1
                        view.ColumnGroups.Add(New GridViewColumnGroup(dtProductItem.Rows(ii)("Short_Description")))
                        view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                        For col As Integer = gv1.Columns("GrandTotalMilk").Index + 1 To gv1.Columns("Total_Prod_Amt").Index - 1
                            If clsCommon.CompairString(gv1.Columns(col).HeaderText, dtProductItem.Rows(ii)("Short_Description")) = CompairStringResult.Equal OrElse clsCommon.CompairString(gv1.Columns(col).HeaderText, dtProductItem.Rows(ii)("Item_Description")) = CompairStringResult.Equal OrElse clsCommon.CompairString(gv1.Columns(col).HeaderText, dtProductItem.Rows(ii)("Product_Item_Rate")) = CompairStringResult.Equal OrElse clsCommon.CompairString(gv1.Columns(col).HeaderText, dtProductItem.Rows(ii)("Product_Item_Tax1")) = CompairStringResult.Equal OrElse clsCommon.CompairString(gv1.Columns(col).HeaderText, dtProductItem.Rows(ii)("Product_Item_Tax2")) = CompairStringResult.Equal OrElse clsCommon.CompairString(gv1.Columns(col).HeaderText, dtProductItem.Rows(ii)("Product_Item_Tax3")) = CompairStringResult.Equal OrElse clsCommon.CompairString(gv1.Columns(col).HeaderText, dtProductItem.Rows(ii)("Product_Item_Tax4")) = CompairStringResult.Equal OrElse clsCommon.CompairString(gv1.Columns(col).HeaderText, dtProductItem.Rows(ii)("Product_Item_Tax5")) = CompairStringResult.Equal OrElse clsCommon.CompairString(gv1.Columns(col).HeaderText, dtProductItem.Rows(ii)("Product_Item_TotalAmt")) = CompairStringResult.Equal Then
                                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
                            End If
                        Next
                    Next
                End If
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Total_Prod_Amt").Name)
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("TCS_Prod").Name)
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("GrandTotalProd").Name)
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Grand Total").Name)
                    gv1.ViewDefinition = view

                End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetReportID()
        LoadData()
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        VarID += "_DI"
        gv1.VarID = VarID
    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        rbtnDispatch.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadData()
        Try

            Dim qry As String = ""
            Dim whrcls As String = ""
            Dim whrclsReturn As String = ""
            whrclsReturn += "  And TSPL_SD_SALE_RETURN_head.Route_No = '" + txtRoute.Value + "'"
            Dim DateFilter As String = ""
            Dim DateFilterRetun As String = ""
            DateFilterRetun = "TSPL_SD_SALE_RETURN_head.Document_Date"
            If rbtnDocumentDate.IsChecked Then
                DateFilter = "Document_Date"
            ElseIf rbtnSupplyDate.IsChecked Then
                DateFilter = "Supply_Date"
            End If
            If clsCommon.myLen(txtRoute.Value) > 0 Then
                If rbtnDispatch.IsChecked Then
                    whrcls += " and TSPL_SD_SHIPMENT_HEAD.Route_No ='" + txtRoute.Value + "'"
                End If
            End If
            dtFreshItem = New DataTable()
            dtProductItem = New DataTable()
            If rbtnDispatch.IsChecked Then
                qry = " max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Short_Description) + '-Amt' as Item_Description,(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
                FROM TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                where  TSPL_SD_SHIPMENT_HEAD.Status = 1  AND convert(date,TSPL_SD_SHIPMENT_HEAD." + DateFilter + ",103) >=Convert(date,'" & txtFromDate.Value & "',103) 
                and convert(date,TSPL_SD_SHIPMENT_HEAD." + DateFilter + ",103) <= Convert(date,'" & txtToDate.Value & "',103) " & whrcls & ""

                dtFreshItem = clsDBFuncationality.GetDataTable("select " & qry & " and  TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0   group by TSPL_ITEM_MASTER.Item_Code,Sku_Seq ORDER BY Sku_Seq ")
                dtProductItem = clsDBFuncationality.GetDataTable(" select MAX(TSPL_SD_SHIPMENT_DETAIL.TAX1) + convert(varchar,convert(decimal(18,1),max(TSPL_SD_SHIPMENT_DETAIL.TAX1_Rate),103)) + '%' Tax1, MAX(TSPL_SD_SHIPMENT_DETAIL.TAX2) + convert(varchar,convert(decimal(18,1),max(TSPL_SD_SHIPMENT_DETAIL.TAX2_Rate),103)) + '%' Tax2,MAX(TSPL_SD_SHIPMENT_DETAIL.TAX3) + convert(varchar,convert(decimal(18,1),max(TSPL_SD_SHIPMENT_DETAIL.TAX3_Rate),103)) + '%' Tax3,MAX(TSPL_SD_SHIPMENT_DETAIL.TAX4) + convert(varchar,convert(decimal(18,1),max(TSPL_SD_SHIPMENT_DETAIL.TAX4_Rate),103)) + '%' Tax4,
                MAX(TSPL_SD_SHIPMENT_DETAIL.TAX5) + convert(varchar,convert(decimal(18,1),max(TSPL_SD_SHIPMENT_DETAIL.TAX5_Rate),103)) + '%' Tax5, TSPL_ITEM_MASTER.Short_Description + '-TAmt' AS Product_Item_TotalAmt, TSPL_ITEM_MASTER.Short_Description + 'Rate' as Product_Item_Rate
,case when max(TSPL_SD_SHIPMENT_DETAIL.TAX1_Rate) <> 0  then TSPL_ITEM_MASTER.Short_Description + ' ' + MAX(TSPL_SD_SHIPMENT_DETAIL.TAX1) + convert(varchar,convert(decimal(18,1),max(TSPL_SD_SHIPMENT_DETAIL.TAX1_Rate),103)) + '%' else TSPL_ITEM_MASTER.Short_Description+'00.00%'  end as Product_Item_Tax1 
,case when max(TSPL_SD_SHIPMENT_DETAIL.TAX2_Rate) <>  0 then TSPL_ITEM_MASTER.Short_Description + ' ' + max(TSPL_SD_SHIPMENT_DETAIL.TAX2) + convert(varchar,convert(decimal(18,1),max(TSPL_SD_SHIPMENT_DETAIL.TAX2_Rate),103)) + '%' else TSPL_ITEM_MASTER.Short_Description+'00.0%'  end as Product_Item_Tax2 
,case when max(TSPL_SD_SHIPMENT_DETAIL.TAX3_Rate) <>  0 then TSPL_ITEM_MASTER.Short_Description + ' ' + max(TSPL_SD_SHIPMENT_DETAIL.TAX3) + convert(varchar,convert(decimal(18,1),max(TSPL_SD_SHIPMENT_DETAIL.TAX3_Rate),103)) + '%' else TSPL_ITEM_MASTER.Short_Description+'0.00%'  end as Product_Item_Tax3 
,case when max(TSPL_SD_SHIPMENT_DETAIL.TAX4_Rate) <>  0 then TSPL_ITEM_MASTER.Short_Description + ' ' + max(TSPL_SD_SHIPMENT_DETAIL.TAX4) + convert(varchar,convert(decimal(18,1),max(TSPL_SD_SHIPMENT_DETAIL.TAX4_Rate),103)) + '%' else TSPL_ITEM_MASTER.Short_Description+'0.0%'  end as Product_Item_Tax4
,case when max(TSPL_SD_SHIPMENT_DETAIL.TAX5_Rate) <>  0 then  TSPL_ITEM_MASTER.Short_Description + ' ' + max(TSPL_SD_SHIPMENT_DETAIL.TAX5) + convert(varchar,convert(decimal(18,1),max(TSPL_SD_SHIPMENT_DETAIL.TAX5_Rate),103)) + '%' else TSPL_ITEM_MASTER.Short_Description+'0%'   end as Product_Item_Tax5
," & qry & " and TSPL_ITEM_MASTER.IsTaxable = 1 group by TSPL_ITEM_MASTER.Item_Code,Short_Description,Sku_Seq ORDER BY Sku_Seq ")
            End If
            Dim FreshItem As String = Nothing
            Dim ReturnFreshItem As String = Nothing

            Dim FreshItemName As String = Nothing
            Dim FreshItemQtyPivot As String = Nothing
            Dim FreshItemAmtPivot As String = Nothing
            Dim FreshItemsQty As String = Nothing
            Dim FreshItemsAmt As String = Nothing
            Dim ProdItem As String = Nothing
            Dim ProdItemName As String = Nothing
            Dim ProdItemQtyPivot As String = Nothing
            Dim ProdItemAmtPivot As String = Nothing
            Dim ProdItemsQty As String = Nothing
            Dim ProdItemsAmt As String = Nothing
            Dim ProdItemRatePivot As String = Nothing
            Dim ProdItemTax1Pivot As String = Nothing
            Dim ProdItemTax2Pivot As String = Nothing
            Dim ProdItemTax3Pivot As String = Nothing
            Dim ProdItemTax4Pivot As String = Nothing
            Dim ProdItemTax5Pivot As String = Nothing
            Dim ProdItemTotalAmtPivot As String = Nothing
            If dtFreshItem.Rows.Count <= 0 AndAlso dtProductItem.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
                Exit Sub
            End If
            If dtFreshItem.Rows.Count > 0 Then
                For i As Integer = 0 To dtFreshItem.Rows.Count - 1
                    FreshItemName += "Sum(IsNull([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "]" + ", Sum(IsNull([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "],0)) As [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "]" + ","
                    FreshItem += "[" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "] As [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "]" + ", [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "] As [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "]" + ","
                    'ReturnFreshItem += "-[" + -clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "] As [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "]" + ", -[" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "] As [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "]" + ","

                    If i = 0 Then
                        FreshItemsQty += "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "],0)"
                        FreshItemsAmt += "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "],0)"
                        FreshItemQtyPivot += "[" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "] "
                        FreshItemAmtPivot += "[" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "] "
                        ReturnFreshItem += "-ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "],0)"

                    Else
                        FreshItemsQty += "+" + "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "],0)"
                        FreshItemsAmt += "+" + "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "],0)"
                        FreshItemQtyPivot += ", [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "] "
                        FreshItemAmtPivot += ", [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "] "
                         ReturnFreshItem = "-ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "],0)"
                    End If
                Next
            End If

            If dtProductItem.Rows.Count > 0 Then
                For i As Integer = 0 To dtProductItem.Rows.Count - 1
                    ProdItemName += "Sum(IsNull([" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "]" + ", Sum(IsNull([" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Rate")) + "],0)) As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Rate")) + "]" + ", Sum(IsNull([" + clsCommon.myCstr(dtProductItem.Rows(i)("Item_Description")) + "],0)) As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Item_Description")) + "]" + ", Sum(IsNull([" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax1")) + "],0)) As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax1")) + "]"
                    ProdItemName += ", Sum(IsNull([" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax2")) + "],0)) As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax2")) + "]" + ", Sum(IsNull([" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax3")) + "],0)) As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax3")) + "]" + ", Sum(IsNull([" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax4")) + "],0)) As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax4")) + "]" + ", Sum(IsNull([" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax5")) + "],0)) As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax5")) + "]" + ", Sum(IsNull([" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_TotalAmt")) + "],0)) As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_TotalAmt")) + "]" + ","
                    ProdItem += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "] As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "]" + ",[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Rate")) + "] As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Rate")) + "]" + ",[" + clsCommon.myCstr(dtProductItem.Rows(i)("Item_Description")) + "] As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Item_Description")) + "]" + ",[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax1")) + "] As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax1")) + "]"
                    ProdItem += ",[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax2")) + "] As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax2")) + "]" + ",[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax3")) + "] As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax3")) + "]" + ",[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax4")) + "] As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax4")) + "]" + ",[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax5")) + "] As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax5")) + "]"
                    ProdItem += ",[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_TotalAmt")) + "] As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_TotalAmt")) + "]" + ","
                    If i = 0 Then
                        ProdItemsQty += "ISNULL([" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "],0)"
                        ProdItemsAmt += "ISNULL([" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_TotalAmt")) + "],0)"
                        ProdItemQtyPivot += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "] "
                        ProdItemRatePivot += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Rate")) + "] "
                        ProdItemTax1Pivot += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax1")) + "] "
                        ProdItemTax2Pivot += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax2")) + "] "
                        ProdItemTax3Pivot += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax3")) + "] "
                        ProdItemTax4Pivot += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax4")) + "] "
                        ProdItemTax5Pivot += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax5")) + "] "

                        ProdItemAmtPivot += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Item_Description")) + "] "
                        ProdItemTotalAmtPivot += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_TotalAmt")) + "] "
                    Else
                        ProdItemsQty += "+" + "ISNULL([" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "],0)"
                        ProdItemsAmt += "+" + "ISNULL([" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_TotalAmt")) + "],0)"
                        ProdItemQtyPivot += ", [" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "] "
                        ProdItemRatePivot += ", [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Rate")) + "] "
                        ProdItemTax1Pivot += ", [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax1")) + "] "
                        ProdItemTax2Pivot += ", [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax2")) + "] "
                        ProdItemTax3Pivot += ", [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax3")) + "] "
                        ProdItemTax4Pivot += ", [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax4")) + "] "
                        ProdItemTax5Pivot += ", [" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_Tax5")) + "] "
                        ProdItemAmtPivot += ", [" + clsCommon.myCstr(dtProductItem.Rows(i)("Item_Description")) + "] "
                        ProdItemTotalAmtPivot += ",[" + clsCommon.myCstr(dtProductItem.Rows(i)("Product_Item_TotalAmt")) + "] "

                    End If
                Next
            End If
            Dim BaseQry As String = ""
            Dim FinalQuery As String = ""
            Dim SecurityRate As String = ""
            If rbtnDispatch.IsChecked Then
                If dtProductItem.Rows.Count <= 0 Then
                    ProdItemsAmt = "0"
                End If
                If dtFreshItem.Rows.Count <= 0 Then
                    FreshItemsQty = "0"
                    FreshItemsAmt = "0"
                End If
                If PickRatefromMaster Then
                    SecurityRate = "tab2."
                Else
                    SecurityRate = "xxxx."
                End If
                BaseQry += " select CONVERT(varchar,xxxx.Document_Date,103)Document_Date," & FreshItem & " Total_Milk_Qty,Total_Milk_Amt,TCS_Milk," & SecurityRate & "Security,(Fresh_Net_Amount +" & SecurityRate & "Security) as GrandTotalMilk, " & ProdItem & " (Total_Prod_Amt - TCS_Prod)Total_Prod_Amt,TCS_Prod,Total_Prod_Amt as GrandTotalProd ,(Fresh_Net_Amount +" & SecurityRate & "Security + Total_Prod_Amt ) as [Grand Total] from ( SELECT max(Route_No)Route_No,  Document_Date,  " & FreshItemName & " SUM(" & FreshItemsQty & ") As Total_Milk_Qty,SUM(" & FreshItemsAmt & ") As Total_Milk_Amt,sum(Fresh_Net_Amount)Fresh_Net_Amount,sum(TCS_Milk)  as TCS_Milk,
                SUM(Security_Amt) as Security ,  " & ProdItemName & " SUM(" & ProdItemsAmt & ") As Total_Prod_Amt ,sum(TCS_Prod)TCS_Prod FROM ( select max(Fresh_Item_Amt)Fresh_Item_Amt,max(Route_No)Route_No,  Document_Date,  Item_Code, max(Cust_Code)Cust_Code,   max(Fresh_Item)Fresh_Item ,sum( Qty)Qty,sum(Fresh_Qty)Fresh_Qty, sum(Product_Qty)Product_Qty, sum(KG_QTY)KG_QTY,sum(LTR_QTY)LTR_QTY,  sum(Product_Amount) Product_Amount , sum(Fresh_Amount)Fresh_Amount,isnull(sum(Fresh_Net_Amount),0)Fresh_Net_Amount, sum(Amount)Amount,  max(Product_Item)Product_Item,max(Product_Item_Amt)Product_Item_Amt ,max(Product_Item_Rate)Product_Item_Rate, sum(Item_Cost)Item_Cost,
                max(Product_Item_Tax1)Product_Item_Tax1 ,  max(Product_Item_Tax2)Product_Item_Tax2   ,max(Product_Item_Tax3)Product_Item_Tax3 ,  max(Product_Item_Tax4)Product_Item_Tax4, max( Product_Item_Tax5)Product_Item_Tax5,  sum(Tax1_Amt)Tax1_Amt,  sum(Tax2_Amt)Tax2_Amt,  sum(Tax3_Amt)Tax3_Amt, sum(Tax4_Amt)Tax4_Amt, sum(Tax5_Amt)Tax5_Amt, isnull(sum(TCS_Prod),0)TCS_Prod,isnull(sum(TCS_Milk),0)TCS_Milk,sum(Product_Total_Amount )Product_Total_Amount, max(Product_Item_TotalAmt)Product_Item_TotalAmt,isnull(sum(Security_Amt),0)Security_Amt   FROM (			
             SELECT TSPL_SD_SHIPMENT_DETAIL.Security_Amt,  TSPL_ITEM_MASTER.Item_Code,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,  TSPL_SD_SHIPMENT_HEAD.Route_No, CONVERT(date,TSPL_SD_SHIPMENT_HEAD." + DateFilter + ",103)Document_Date, case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 )  then  TSPL_ITEM_MASTER.Short_Description end as Fresh_Item , case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 )  then  TSPL_ITEM_MASTER.Short_Description 
                + '-Amt' end as Fresh_Item_Amt,TSPL_SD_SHIPMENT_DETAIL.Unit_Code AS UOM ,isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) AS Qty, case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 ) then isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0) end as Fresh_Qty, case when  IsTaxable = 1 then isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) end as Product_Qty,case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 ) then round((isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) when (TSPL_ITEM_MASTER.Is_Ambient = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 ) then round((isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[KG],2) end as KG_QTY
                ,round((isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) as LTR_QTY ,case when  IsTaxable = 1 then isnull(TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount,0) end as Product_Amount ,case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 )  then isnull(TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount,0)end as Fresh_Amount,case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 )  then isnull(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,0)end as Fresh_Net_Amount,TSPL_SD_SHIPMENT_DETAIL.Amount, case when  IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description  end AS Product_Item,case when  IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description + '-Amt' end AS Product_Item_Amt ,case when  IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description + 'Rate' end as Product_Item_Rate, (TSPL_SD_SHIPMENT_DETAIL.Item_Cost/12) as Item_Cost,
                case when isnull(TSPL_SD_SHIPMENT_DETAIL.TAX1,'') <> '' then TSPL_ITEM_MASTER.Short_Description + ' ' +(TSPL_SD_SHIPMENT_DETAIL.TAX1) + convert(varchar,convert(decimal(18,1),(TSPL_SD_SHIPMENT_DETAIL.TAX1_Rate),103)) + '%' end as Product_Item_Tax1 , case when isnull(TSPL_SD_SHIPMENT_DETAIL.TAX2,'') <> '' then TSPL_ITEM_MASTER.Short_Description + ' ' + (TSPL_SD_SHIPMENT_DETAIL.TAX2) + convert(varchar,convert(decimal(18,1),(TSPL_SD_SHIPMENT_DETAIL.TAX2_Rate),103)) + '%' end as Product_Item_Tax2 , case
				when isnull(TSPL_SD_SHIPMENT_DETAIL.TAX3,'') <> ''  then TSPL_ITEM_MASTER.Short_Description + ' ' + (TSPL_SD_SHIPMENT_DETAIL.TAX3) + convert(varchar,convert(decimal(18,1),(TSPL_SD_SHIPMENT_DETAIL.TAX3_Rate),103)) + '%' end as Product_Item_Tax3 , case when isnull(TSPL_SD_SHIPMENT_DETAIL.TAX4,'') <> '' then  TSPL_ITEM_MASTER.Short_Description + ' ' + (TSPL_SD_SHIPMENT_DETAIL.TAX4) + convert(varchar,convert(decimal(18,1),(TSPL_SD_SHIPMENT_DETAIL.TAX4_Rate),103)) + '%' end as Product_Item_Tax4
               , case when isnull(TSPL_SD_SHIPMENT_DETAIL.TAX5,'') <> '' then  TSPL_ITEM_MASTER.Short_Description + ' ' + (TSPL_SD_SHIPMENT_DETAIL.TAX5) + convert(varchar,convert(decimal(18,1),(TSPL_SD_SHIPMENT_DETAIL.TAX5_Rate),103)) + '%'  end as Product_Item_Tax5, case when isnull(TSPL_SD_SHIPMENT_DETAIL.TAX1,'') <> '' then TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt end as Tax1_Amt, case when isnull(TSPL_SD_SHIPMENT_DETAIL.TAX2,'') <> '' then TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt end as Tax2_Amt, case  when isnull(TSPL_SD_SHIPMENT_DETAIL.TAX3,'') <> '' then TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt end as Tax3_Amt, case when isnull(TSPL_SD_SHIPMENT_DETAIL.TAX4,'') <>'' then  TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt end as Tax4_Amt, case when isnull(TSPL_SD_SHIPMENT_DETAIL.tax5,'') <> '' 
                then TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt end as Tax5_Amt,	case WHEN TSPL_ITEM_MASTER.IsTaxable = 0 then CASE WHEN tax1.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt, 0)  WHEN tax2.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt, 0) WHEN tax3.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt, 0) WHEN tax4.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt, 0)  WHEN tax5.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt, 0) WHEN tax6.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt, 0) WHEN tax7.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt, 0) WHEN tax8.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt, 0)  WHEN tax9.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt, 0)  WHEN tax10.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt, 0) ELSE 0 end  end as TCS_Milk
              ,case WHEN TSPL_ITEM_MASTER.IsTaxable = 1 then CASE WHEN tax1.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX1_Amt, 0)  WHEN tax2.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX2_Amt, 0) WHEN tax3.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX3_Amt, 0) WHEN tax4.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX4_Amt, 0)  WHEN tax5.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX5_Amt, 0) WHEN tax6.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX6_Amt, 0) WHEN tax7.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX7_Amt, 0) WHEN tax8.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX8_Amt, 0)  WHEN tax9.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX9_Amt, 0)  WHEN tax10.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SHIPMENT_DETAIL.TAX10_Amt, 0) ELSE 0 end end as TCS_Prod, case when  IsTaxable = 1 then isnull(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt,0)end as Product_Total_Amount,
              case when  IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description + '-TAmt' end AS Product_Item_TotalAmt  From TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_Code  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SHIPMENT_DETAIL.tax1   left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SHIPMENT_DETAIL.tax2   left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SHIPMENT_DETAIL .TAX3  
             left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SHIPMENT_DETAIL .tax4   left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SHIPMENT_DETAIL .tax5   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SHIPMENT_DETAIL .TAX6   left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SHIPMENT_DETAIL .TAX7   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SHIPMENT_DETAIL .TAX8   left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SHIPMENT_DETAIL .TAX9  left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SHIPMENT_DETAIL .TAX10 left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I 
                PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_SD_SHIPMENT_DETAIL.Item_Code = I.item_code where 2 = 2  And TSPL_SD_SHIPMENT_HEAD.Status = 1 And convert(date," + DateFilter + ",103) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103)  and   convert(date," + DateFilter + ",103) <= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103) " & whrcls & "  )  xx group by Document_Date,Item_Code ) xxx "
                If dtFreshItem.Rows.Count > 0 Then
                    BaseQry += "  PIVOT (SUM(LTR_QTY)  For Fresh_Item In (" & FreshItemQtyPivot & ") ) AS pivot_fresh PIVOT (SUM(Fresh_Amount)  For Fresh_Item_Amt In (" & FreshItemAmtPivot & ") ) AS pivot_fresh_amt "
                End If
                If dtProductItem.Rows.Count > 0 Then
                    BaseQry += " PIVOT (SUM(KG_QTY)  FOR Product_Item IN (" & ProdItemQtyPivot & ") ) AS pivot_Product  PIVOT (SUM(Product_Amount)  FOR Product_Item_Amt IN (" & ProdItemAmtPivot & ") ) AS pivot_Product_amt "
                    BaseQry += " PIVOT (SUM(Item_Cost)  FOR Product_Item_Rate IN (" & ProdItemRatePivot & ")) as pivot_Product_rate PIVOT (SUM(Tax1_Amt) FOR Product_Item_Tax1 IN (" & ProdItemTax1Pivot & ")) as pivot_Product_tax1 PIVOT (SUM(Tax2_Amt) FOR Product_Item_Tax2 IN (" & ProdItemTax2Pivot & ")) as pivot_Product_tax2
                    PIVOT (SUM(Tax3_Amt) FOR Product_Item_Tax3 IN (" & ProdItemTax3Pivot & ")) as pivot_Product_tax3 PIVOT (SUM(Tax4_Amt) FOR Product_Item_Tax4 IN (" & ProdItemTax4Pivot & ")) as pivot_Product_tax4  PIVOT (SUM(Tax5_Amt) FOR Product_Item_Tax5 IN (" & ProdItemTax5Pivot & ")) as pivot_Product_tax5 PIVOT (SUM(Product_Total_Amount)  FOR Product_Item_TotalAmt IN (" & ProdItemTotalAmtPivot & ")) as pivot_Product_Tamt"
                End If
                BaseQry += "  Group BY Document_Date )xxxx " & Environment.NewLine & "
	   WHERE CONVERT(DATE, xxxx.Document_Date, 103) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND CONVERT(DATE, xxxx.Document_Date, 103) <= CONVERT(DATE,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)"

                If clsCommon.myLen(txtRoute.Value) > 0 Then
                    BaseQry += " And xxxx.Route_No = '" & txtRoute.Value & "'   "
                End If
                'BaseQry += " order by xxxx.Document_Date ;"

                'varsa add return data


                BaseQry += "Union all ( select CONVERT(varchar,xxxx.Document_Date,103)Document_Date," & FreshItem & " Total_Milk_Qty,Total_Milk_Amt,TCS_Milk," & SecurityRate & "Security,(Fresh_Net_Amount +" & SecurityRate & "Security) as GrandTotalMilk, " & ProdItem & " (Total_Prod_Amt - TCS_Prod)Total_Prod_Amt,TCS_Prod,Total_Prod_Amt as GrandTotalProd ,(Fresh_Net_Amount +" & SecurityRate & "Security + Total_Prod_Amt ) as [Grand Total] from ( SELECT max(Route_No)Route_No,  Document_Date,  " & FreshItemName & " SUM(" & FreshItemsQty & ") As Total_Milk_Qty,SUM(" & FreshItemsAmt & ") As Total_Milk_Amt,sum(Fresh_Net_Amount*-1)Fresh_Net_Amount,sum(TCS_Milk)  as TCS_Milk,
                SUM(Security_Amt) as Security ,  " & ProdItemName & " SUM(" & ProdItemsAmt & ") As Total_Prod_Amt ,sum(TCS_Prod)TCS_Prod FROM ( select max(Fresh_Item_Amt)Fresh_Item_Amt,max(Route_No)Route_No,  Document_Date,  Item_Code, max(Cust_Code)Cust_Code,   max(Fresh_Item)Fresh_Item ,sum( Qty)Qty,sum(Fresh_Qty)Fresh_Qty, sum(Product_Qty)Product_Qty, sum(KG_QTY)KG_QTY,sum(LTR_QTY)LTR_QTY,  sum(Product_Amount) Product_Amount , sum(Fresh_Amount)Fresh_Amount,isnull(sum(Fresh_Net_Amount*-1),0)Fresh_Net_Amount, sum(Amount)Amount,  max(Product_Item)Product_Item,max(Product_Item_Amt)Product_Item_Amt ,max(Product_Item_Rate)Product_Item_Rate, sum(Item_Cost)Item_Cost,
                max(Product_Item_Tax1)Product_Item_Tax1 ,  max(Product_Item_Tax2)Product_Item_Tax2   ,max(Product_Item_Tax3)Product_Item_Tax3 ,  max(Product_Item_Tax4)Product_Item_Tax4, max( Product_Item_Tax5)Product_Item_Tax5,  sum(Tax1_Amt)Tax1_Amt,  sum(Tax2_Amt)Tax2_Amt,  sum(Tax3_Amt)Tax3_Amt, sum(Tax4_Amt)Tax4_Amt, sum(Tax5_Amt)Tax5_Amt, isnull(sum(TCS_Prod*-1),0)TCS_Prod,isnull(sum(TCS_Milk*-1),0)TCS_Milk,sum(Product_Total_Amount )Product_Total_Amount, max(Product_Item_TotalAmt)Product_Item_TotalAmt,isnull(sum(Security_Amt),0)Security_Amt   FROM (			
             SELECT TSPL_SD_SALE_RETURN_DETAIL.Security_Amt*-1 as Security_Amt, 
                TSPL_ITEM_MASTER.Item_Code,TSPL_SD_SALE_RETURN_head.Customer_Code as Cust_Code,  TSPL_SD_SALE_RETURN_head.Route_No, CONVERT(date," + DateFilterRetun + ",103)Document_Date, case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 )  then  TSPL_ITEM_MASTER.Short_Description end as Fresh_Item , case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 )  then  TSPL_ITEM_MASTER.Short_Description 
                + '-Amt' end as Fresh_Item_Amt,TSPL_SD_SALE_RETURN_DETAIL.Unit_Code AS UOM ,isnull(TSPL_SD_SALE_RETURN_DETAIL.qty,0) AS Qty, case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 ) then isnull(TSPL_SD_SALE_RETURN_DETAIL.Qty,0) end as Fresh_Qty, case when  IsTaxable = 1 then isnull(TSPL_SD_SALE_RETURN_DETAIL.qty,0)*-1 end as Product_Qty,
                case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 ) then round((isnull(TSPL_SD_SALE_RETURN_DETAIL.qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2)*-1 when (TSPL_ITEM_MASTER.Is_Ambient = 1 and TSPL_ITEM_MASTER.IsTaxable = 1 ) then round((isnull(TSPL_SD_SALE_RETURN_DETAIL.qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[KG],2)*-1 end as KG_QTY
                ,round((isnull(TSPL_SD_SALE_RETURN_DETAIL.qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2)*-1 as LTR_QTY ,case when  IsTaxable = 1 then isnull(TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount,0)*-1 end as Product_Amount ,case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 )  then isnull(TSPL_SD_SALE_RETURN_DETAIL.Amt_Less_Discount,0)*-1 end as Fresh_Amount,case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 )  then isnull(TSPL_SD_SALE_RETURN_detail.Item_Net_Amt,0)*-1 end as Fresh_Net_Amount,TSPL_SD_SALE_RETURN_DETAIL.Amount*-1 as Amount, case when  IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description  end AS Product_Item,case when  IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description + '-Amt' end AS Product_Item_Amt ,case when  IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description + 'Rate' end as Product_Item_Rate, (TSPL_SD_SALE_RETURN_DETAIL.Item_Cost/12)*-1 as Item_Cost,
                case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX1,'') <> '' then TSPL_ITEM_MASTER.Short_Description + ' ' +(TSPL_SD_SALE_RETURN_DETAIL.TAX1) + convert(varchar,convert(decimal(18,1),(TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate),103)) + '%' end as Product_Item_Tax1 , case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX2,'') <> '' then TSPL_ITEM_MASTER.Short_Description + ' ' + (TSPL_SD_SALE_RETURN_DETAIL.TAX2) + convert(varchar,convert(decimal(18,1),(TSPL_SD_SALE_RETURN_DETAIL.TAX2_Rate),103)) + '%' end as Product_Item_Tax2 , case
				when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'') <> ''  then TSPL_ITEM_MASTER.Short_Description + ' ' + (TSPL_SD_SALE_RETURN_DETAIL.TAX3) + convert(varchar,convert(decimal(18,1),(TSPL_SD_SALE_RETURN_DETAIL.TAX3_Rate),103)) + '%' end as Product_Item_Tax3 , case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4,'') <> '' then  TSPL_ITEM_MASTER.Short_Description + ' ' + (TSPL_SD_SALE_RETURN_DETAIL.TAX4) + convert(varchar,convert(decimal(18,1),(TSPL_SD_SALE_RETURN_DETAIL.TAX4_Rate),103)) + '%' end as Product_Item_Tax4
               , case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX5,'') <> '' then  TSPL_ITEM_MASTER.Short_Description + ' ' + (TSPL_SD_SALE_RETURN_DETAIL.TAX5) + convert(varchar,convert(decimal(18,1),(TSPL_SD_SALE_RETURN_DETAIL.TAX5_Rate),103)) + '%'  end as Product_Item_Tax5, case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX1,'') <> '' then TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt*-1 end as Tax1_Amt, case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX2,'') <> '' then TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt*-1 end as Tax2_Amt, case  when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX3,'') <> '' then TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt*-1 end as Tax3_Amt, case when isnull(TSPL_SD_SALE_RETURN_DETAIL.TAX4,'') <>'' then  TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt*-1 end as Tax4_Amt, case when isnull(TSPL_SD_SALE_RETURN_head.tax5,'') <> '' 
                then TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt*-1 end as Tax5_Amt,	case WHEN TSPL_ITEM_MASTER.IsTaxable = 0 then CASE WHEN tax1.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt, 0)  WHEN tax2.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt, 0) WHEN tax3.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt, 0) WHEN tax4.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt, 0)  WHEN tax5.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt, 0) WHEN tax6.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt, 0) WHEN tax7.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt, 0) WHEN tax8.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt, 0)  WHEN tax9.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_head.TAX9_Amt, 0)  WHEN tax10.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX10_Amt, 0) ELSE 0 end  end as TCS_Milk
              ,case WHEN TSPL_ITEM_MASTER.IsTaxable = 1 then CASE WHEN tax1.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt, 0)  WHEN tax2.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt, 0) WHEN tax3.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt, 0) WHEN tax4.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt, 0)  WHEN tax5.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt, 0) WHEN tax6.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt, 0) WHEN tax7.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt, 0) WHEN tax8.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt, 0)  WHEN tax9.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_DETAIL.TAX9_Amt, 0)  WHEN tax10.Is_TCS = 'Y' THEN ISNULL(TSPL_SD_SALE_RETURN_head.TAX10_Amt, 0) ELSE 0 end end as TCS_Prod, case when  IsTaxable = 1 then isnull(TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt,0)*-1 end as Product_Total_Amount,
              case when  IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description + '-TAmt' end AS Product_Item_TotalAmt  From TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SALE_RETURN_DETAIL.Unit_Code  
             
             left outer join TSPL_SD_SALE_RETURN_head on TSPL_SD_SALE_RETURN_head.Document_Code = TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE
             
             left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_head.Customer_Code left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_RETURN_DETAIL.tax1   left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_RETURN_DETAIL.tax2   left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_RETURN_DETAIL .TAX3  
             left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_RETURN_DETAIL .tax4   left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_RETURN_DETAIL .tax5   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_RETURN_DETAIL .TAX6   left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_RETURN_DETAIL .TAX7   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_RETURN_DETAIL .TAX8   left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_RETURN_DETAIL .TAX9  left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_RETURN_DETAIL .TAX10 left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I 
                PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_SD_SALE_RETURN_DETAIL.Item_Code = I.item_code where 2 = 2  And TSPL_SD_SALE_RETURN_head.Status = 1    And convert(date," + DateFilterRetun + ",103) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103)  and   convert(date," + DateFilterRetun + ",103) <= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103) " & whrclsReturn & "  )  xx group by Document_Date,Item_Code ) xxx "
                If dtFreshItem.Rows.Count > 0 Then
                    BaseQry += "  PIVOT (SUM(LTR_QTY)  For Fresh_Item In (" & FreshItemQtyPivot & ") ) AS pivot_fresh PIVOT (SUM(Fresh_Amount)  For Fresh_Item_Amt In (" & FreshItemAmtPivot & ") ) AS pivot_fresh_amt "
                End If
                If dtProductItem.Rows.Count > 0 Then
                    BaseQry += " PIVOT (SUM(KG_QTY)  FOR Product_Item IN (" & ProdItemQtyPivot & ") ) AS pivot_Product  PIVOT (SUM(Product_Amount)  FOR Product_Item_Amt IN (" & ProdItemAmtPivot & ") ) AS pivot_Product_amt "
                    BaseQry += " PIVOT (SUM(Item_Cost)  FOR Product_Item_Rate IN (" & ProdItemRatePivot & ")) as pivot_Product_rate PIVOT (SUM(Tax1_Amt) FOR Product_Item_Tax1 IN (" & ProdItemTax1Pivot & ")) as pivot_Product_tax1 PIVOT (SUM(Tax2_Amt) FOR Product_Item_Tax2 IN (" & ProdItemTax2Pivot & ")) as pivot_Product_tax2
                    PIVOT (SUM(Tax3_Amt) FOR Product_Item_Tax3 IN (" & ProdItemTax3Pivot & ")) as pivot_Product_tax3 PIVOT (SUM(Tax4_Amt) FOR Product_Item_Tax4 IN (" & ProdItemTax4Pivot & ")) as pivot_Product_tax4  PIVOT (SUM(Tax5_Amt) FOR Product_Item_Tax5 IN (" & ProdItemTax5Pivot & ")) as pivot_Product_tax5 PIVOT (SUM(Product_Total_Amount)  FOR Product_Item_TotalAmt IN (" & ProdItemTotalAmtPivot & ")) as pivot_Product_Tamt"
                End If
                BaseQry += "  Group BY Document_Date )xxxx " & Environment.NewLine & "
	   WHERE CONVERT(DATE, xxxx.Document_Date, 103) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) AND CONVERT(DATE, xxxx.Document_Date, 103) <= CONVERT(DATE,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)"

                If clsCommon.myLen(txtRoute.Value) > 0 Then
                    BaseQry += " And xxxx.Route_No = '" & txtRoute.Value & "')   "
                End If
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQry)

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                View()
                SetGridFormation()
                ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisableControls(False)
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 1 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).FormatString = "{0:n2}"
            If gv1.Columns(ii).Name.Contains("Rate") Then
                gv1.Columns(ii).HeaderText = "Basic Rate"
            End If
            If ii > gv1.Columns("GrandTotalMilk").Index Then
                If gv1.Columns(ii).Name.Contains("-Amt") Then
                    gv1.Columns(ii).HeaderText = "Basic Amt."
                End If
                If gv1.Columns(ii).Name.Contains("-TAmt") Then
                    gv1.Columns(ii).HeaderText = "Amount"
                End If
                If gv1.Columns(ii).Name.Contains("Rate") Then
                    gv1.Columns(ii).HeaderText = "Basic Rate"
                End If
                If gv1.Columns(ii).Name.Contains("%") Then
                    gv1.Columns(ii).FormatString = "{0:n6}"
                End If
                For j As Integer = 0 To dtProductItem.Rows.Count - 1
                        For k As Integer = 0 To 4
                            If gv1.Columns(ii).Name.Contains(dtProductItem.Rows(j)(k)) Then
                                gv1.Columns(ii).HeaderText = dtProductItem.Rows(j)(k)
                            End If
                        Next
                    Next

                End If

        Next
        gv1.ShowGroupPanel = False
        gv1.Columns("Document_Date").HeaderText = "Date"
        gv1.Columns("Document_Date").ReadOnly = True
        gv1.Columns("Document_Date").IsVisible = True
        gv1.Columns("Total_Milk_Qty").HeaderText = "Total Milk Qty Ltr."
        gv1.Columns("Total_Milk_Amt").HeaderText = "Total Milk Amount"
        gv1.Columns("TCS_Milk").HeaderText = "TCS @.1 %"
        gv1.Columns("TCS_Milk").FormatString = "{0:n6}"
        gv1.Columns("TCS_Prod").FormatString = "{0:n6}"
        gv1.BestFitColumns()
        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "TNK") = CompairStringResult.Equal Then
            gv1.Columns("Security").HeaderText = "Security Money @10Paise Per Ltr. "
        Else
            gv1.Columns("Security").HeaderText = "Commission/Security Rate "
            If PickRatefromMaster Then
                gv1.Columns("Security").FormatString = "{0:n4}"
            End If
        End If

        gv1.Columns("Total_Prod_Amt").HeaderText = "Total Product Amout"
        gv1.Columns("TCS_Prod").HeaderText = "Product TCS @.1% "
        gv1.Columns("GrandTotalProd").HeaderText = "Grand Total Product Amount"
        gv1.Columns("GrandTotalMilk").HeaderText = "Grand Total Milk Amount"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        For ii As Integer = 1 To gv1.Columns.Count - 1
            If ii > gv1.Columns("GrandTotalMilk").Index Then
                If gv1.Columns(ii).Name.Contains("%") Then
                    summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F6}", GridAggregateFunction.Sum))
                Else
                    summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
                End If
            Else
                summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer = 0
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
                obj = Nothing
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMonthWiseDMRReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate.Value) + "  To " + clsCommon.myCDate(txtToDate.Value))

                If clsCommon.myLen(txtRoute.Value) > 0 Then
                    arrHeader.Add("Route Code : " & clsCommon.myCstr(txtRoute.Value) & "")
                End If

                transportSql.exportdata(gv1, "", Me.Text, False, arrHeader, False, False, True)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy"))
                If clsCommon.myLen(txtRoute.Value) > 0 Then
                    arrHeader.Add("Route Code : " & txtRoute.Value & "")
                End If
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub

    Private Sub txtRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
        Try
            Dim qry As String = "Select Route_No as Code,Route_Desc as Description from TSPL_ROUTE_MASTER"
            txtRoute.Value = clsCommon.ShowSelectForm("EXRUTFND", qry, "Code", "", txtRoute.Value, "", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

