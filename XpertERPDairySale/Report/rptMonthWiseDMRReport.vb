
Imports common
Imports System.IO


Public Class rptMonthWiseDMRReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim dtFreshItem As DataTable = New DataTable()
    Dim dtProductItem As DataTable = New DataTable()
#End Region
    Private Sub rptMonthWiseDMRReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add("Document_Date")
                For ii As Integer = 0 To dtFreshItem.Rows.Count - 1
                    view.ColumnGroups.Add(New GridViewColumnGroup(dtFreshItem.Rows(ii)("Short_Description")))
                    view.ColumnGroups(ii + 1).Rows.Add(New GridViewColumnGroupRow())
                    For col As Integer = 1 To gv1.Columns("Total_Milk_Qty").Index - 1
                        If clsCommon.CompairString(gv1.Columns(col).HeaderText, dtFreshItem.Rows(ii)("Short_Description")) = CompairStringResult.Equal OrElse clsCommon.CompairString(gv1.Columns(col).HeaderText, dtFreshItem.Rows(ii)("Item_Description")) = CompairStringResult.Equal Then
                            view.ColumnGroups(ii + 1).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
                        End If
                    Next
                Next

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

                For ii As Integer = 0 To dtProductItem.Rows.Count - 1
                    view.ColumnGroups.Add(New GridViewColumnGroup(dtProductItem.Rows(ii)("Short_Description")))
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                    For col As Integer = gv1.Columns("GrandTotalMilk").Index + 1 To gv1.Columns("Total_Prod_Amt").Index - 1
                        If clsCommon.CompairString(gv1.Columns(col).HeaderText, dtProductItem.Rows(ii)("Short_Description")) = CompairStringResult.Equal OrElse clsCommon.CompairString(gv1.Columns(col).HeaderText, dtProductItem.Rows(ii)("Item_Description")) = CompairStringResult.Equal Then
                            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
                        End If
                    Next
                Next

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

        If rbtnDemand.IsChecked Then
            VarID += "_DE"
        ElseIf rbtnDispatch.IsChecked Then
            VarID += "_DI"
        End If
        gv1.VarID = VarID

    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        txtRoute.Value = Nothing
        rbtnDemand.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadData()
        Try

            Dim qry As String = ""
            Dim whrcls As String = ""

            If clsCommon.myLen(txtRoute.Value) > 0 Then
                If rbtnDemand.IsChecked Then
                    whrcls += "  And TSPL_BOOKING_DETAIL.Route_No = '" + txtRoute.Value + "'"
                ElseIf rbtnDispatch.IsChecked Then
                    whrcls += " and TSPL_SD_SHIPMENT_HEAD.Route_No ='" + txtRoute.Value + "'"
                End If
            End If
            dtFreshItem = New DataTable()
            dtProductItem = New DataTable()
            If rbtnDispatch.IsChecked Then
                qry = " max(TSPL_ITEM_MASTER.Short_Description)Short_Description,max(TSPL_ITEM_MASTER.Short_Description) + '-Amt' as Item_Description,(TSPL_ITEM_MASTER.Sku_Seq)Sku_Seq
                FROM TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
                left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
                where  TSPL_SD_SHIPMENT_HEAD.Status = 1  AND convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=Convert(date,'" & txtFromDate.Value & "',103) 
                and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= Convert(date,'" & txtToDate.Value & "',103) " & whrcls & ""

                dtFreshItem = clsDBFuncationality.GetDataTable("select " & qry & " and  TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0   group by TSPL_ITEM_MASTER.Item_Code,Sku_Seq ORDER BY Sku_Seq ")
                dtProductItem = clsDBFuncationality.GetDataTable("select " & qry & "  and TSPL_ITEM_MASTER.Is_Ambient = 1 and TSPL_ITEM_MASTER.IsTaxable = 1  group by TSPL_ITEM_MASTER.Item_Code,Sku_Seq ORDER BY Sku_Seq ")
            End If
            Dim FreshItem As String = Nothing
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

            If dtFreshItem.Rows.Count <= 0 AndAlso dtProductItem.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
                Exit Sub
            End If
            If dtFreshItem.Rows.Count > 0 Then
                For i As Integer = 0 To dtFreshItem.Rows.Count - 1
                    FreshItemName += "Sum(IsNull([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "]" + ", Sum(IsNull([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "],0)) As [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "]" + ","
                    FreshItem += "[" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "] As [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "]" + ", [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "] As [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "]" + ","
                    If i = 0 Then
                        FreshItemsQty += "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "],0)"
                        FreshItemsAmt += "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "],0)"
                        FreshItemQtyPivot += "[" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "] "
                        FreshItemAmtPivot += "[" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "] "
                    Else
                        FreshItemsQty += "+" + "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "],0)"
                        FreshItemsAmt += "+" + "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "],0)"
                        FreshItemQtyPivot += ", [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "] "
                        FreshItemAmtPivot += ", [" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "] "

                    End If
                Next
            End If

            If dtProductItem.Rows.Count > 0 Then
                For i As Integer = 0 To dtProductItem.Rows.Count - 1
                    ProdItemName += "Sum(IsNull([" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "]" + ", Sum(IsNull([" + clsCommon.myCstr(dtProductItem.Rows(i)("Item_Description")) + "],0)) As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Item_Description")) + "]" + ","
                    ProdItem += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "] As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "]" + ",[" + clsCommon.myCstr(dtProductItem.Rows(i)("Item_Description")) + "] As [" + clsCommon.myCstr(dtProductItem.Rows(i)("Item_Description")) + "]" + ","
                    If i = 0 Then
                        ProdItemsQty += "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "],0)"
                        ProdItemsAmt += "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "],0)"
                        ProdItemQtyPivot += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "] "
                        ProdItemAmtPivot += "[" + clsCommon.myCstr(dtProductItem.Rows(i)("Item_Description")) + "] "
                    Else
                        ProdItemsQty += "+" + "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Short_Description")) + "],0)"
                        ProdItemsAmt += "+" + "ISNULL([" + clsCommon.myCstr(dtFreshItem.Rows(i)("Item_Description")) + "],0)"
                        ProdItemQtyPivot += ", [" + clsCommon.myCstr(dtProductItem.Rows(i)("Short_Description")) + "] "
                        ProdItemAmtPivot += ", [" + clsCommon.myCstr(dtProductItem.Rows(i)("Item_Description")) + "] "

                    End If
                Next
            End If
            Dim BaseQry As String = ""
            Dim FinalQuery As String = ""

            If rbtnDispatch.IsChecked Then
                BaseQry += " select Document_Date," & FreshItem & " Total_Milk_Qty,Total_Milk_Amt,TCS_Milk,Security,(Total_Milk_Amt +TCS_Milk+Security) as GrandTotalMilk, " & ProdItem & " Total_Prod_Amt,TCS_Prod,(Total_Prod_Amt+ TCS_Prod) as GrandTotalProd ,(Total_Milk_Amt +TCS_Milk+Security+ Total_Prod_Amt+ TCS_Prod ) as [Grand Total] from ( SELECT  Document_Date,  " & FreshItemName & " SUM(" & FreshItemsQty & ") As Total_Milk_Qty,SUM(" & FreshItemsAmt & ") As Total_Milk_Amt,CONVERT(DECIMAL(18,2),(SUM(" & FreshItemsAmt & ")* 0.1 ))  as TCS_Milk,CONVERT(DECIMAL(18,2),(SUM(" & FreshItemsQty & ")* 0.1)) as Security  , " & ProdItemName & "SUM(" & ProdItemsQty & ") As Total_Product_Qty, SUM(" & ProdItemsAmt & ") As Total_Prod_Amt ,CONVERT(DECIMAL(18,2),(SUM (" & ProdItemsAmt & ")* 0.1 ))  as TCS_Prod FROM( "
                BaseQry += " SELECT TSPL_ITEM_MASTER.Item_Code,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,  TSPL_SD_SHIPMENT_HEAD.Route_No, CONVERT(varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)Document_Date, case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 )  then  TSPL_ITEM_MASTER.Short_Description end as Fresh_Item ,
                case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 )  then  TSPL_ITEM_MASTER.Short_Description + '-Amt' end as Fresh_Item_Amt,TSPL_SD_SHIPMENT_DETAIL.Unit_Code AS UOM ,isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) AS Qty, case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 ) then
                isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0) end as Fresh_Qty, case when TSPL_ITEM_MASTER.Is_Ambient = 1 and IsTaxable = 1 then isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) end as Product_Qty,round((isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[KG],2) as KG_QTY  ,round((isnull(TSPL_SD_SHIPMENT_DETAIL.qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR],2) as LTR_QTY
                ,case when TSPL_ITEM_MASTER.Is_Ambient = 1 and IsTaxable = 1 then isnull(TSPL_SD_SHIPMENT_DETAIL.Amount,0)end as Product_Amount ,case when (TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 )  then isnull(TSPL_SD_SHIPMENT_DETAIL.Amount,0)end as Fresh_Amount,TSPL_SD_SHIPMENT_DETAIL.Amount, case when TSPL_ITEM_MASTER.Is_Ambient = 1 and IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description  end AS Product_Item
		        ,case when TSPL_ITEM_MASTER.Is_Ambient = 1 and IsTaxable = 1 then TSPL_ITEM_MASTER.Short_Description + '-Amt' end AS Product_Item_Amt From TSPL_SD_SHIPMENT_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SD_SHIPMENT_DETAIL.Unit_Code  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
	            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code  left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_SD_SHIPMENT_DETAIL.Item_Code = I.item_code
                where 2 = 2  And TSPL_SD_SHIPMENT_HEAD.Status = 1   And  convert(date,Document_Date,103) >= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103)  and   convert(date,Document_Date,103) <= CONVERT(DATE, '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103) " & whrcls & "  )  xx "
                BaseQry += "  PIVOT (SUM(LTR_QTY)  For Fresh_Item In (" & FreshItemQtyPivot & ") ) AS pivot_fresh PIVOT (SUM(KG_QTY)  FOR Product_Item IN (" & ProdItemQtyPivot & ") ) AS pivot_Product  "
                BaseQry += "  PIVOT (SUM(Fresh_Amount)  For Fresh_Item_Amt In (" & FreshItemAmtPivot & ") ) AS pivot_fresh_amt PIVOT (SUM(Product_Amount)  FOR Product_Item_Amt IN (" & ProdItemAmtPivot & ") ) AS pivot_Product_amt Group BY Document_Date)xxx order by convert(date,Document_Date,103)  "
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
                SetGridFormation()
                View()
                ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
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
        Next
        gv1.ShowGroupPanel = False
        'For ii As Integer = 1 To gv1.Columns("Total_Milk_Qty").Index - 1
        '    If gv1.Columns(ii).Name.Contains("Amt") Then
        '        gv1.Columns(ii).HeaderText = gv1.Columns(ii).Name.Remove(gv1.Columns(ii).Name.Length - 3, 3)
        '    End If
        '    gv1.Columns(ii).FormatString = "{0:n2}"
        'Next
        gv1.Columns("Document_Date").HeaderText = "Date"
        gv1.Columns("Document_Date").ReadOnly = True
        gv1.Columns("Document_Date").IsVisible = True


        gv1.Columns("Total_Milk_Qty").HeaderText = "Total Milk  Qty Ltr."
        gv1.Columns("Total_Milk_Amt").HeaderText = "Total  Milk  Amount"
        gv1.Columns("TCS_Milk").HeaderText = "TCS @.1 %"
        gv1.Columns("Security").HeaderText = "Security Money @10Paise Per Ltr. "

        gv1.Columns("Total_Prod_Amt").HeaderText = "Total Product Amout"
        gv1.Columns("TCS_Prod").HeaderText = "Product TCS"
        gv1.Columns("GrandTotalProd").HeaderText = "Grand Total Product Amount"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        For ii As Integer = 1 To gv1.Columns.Count - 1
            summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "{0:F2}", GridAggregateFunction.Sum))
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

