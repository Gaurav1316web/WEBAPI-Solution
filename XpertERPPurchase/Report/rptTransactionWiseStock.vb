Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class rptTransactionWiseStock
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSplitExport.Visible = MyBase.isExport
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub Reset()
        txtBillToLocation.Value = Nothing
        lblBillToLocation.Text = ""
        TxtItem.Value = Nothing
        lblItem.Text = ""
        Gv1.DataSource = Nothing
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Load_Transaction_Wise_Stock()
    End Sub
    Private Sub Load_Transaction_Wise_Stock()
        Dim qry As String = " "
        Dim dt As New DataTable()
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Location.", Me.Text)
                txtBillToLocation.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(TxtItem.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Item.", Me.Text)
                TxtItem.Focus()
                Exit Sub
            End If
            qry = "  WITH my_cte AS (
                                    select ROW_NUMBER() over (Partition by 1 order by Punching_Date) as SNO , *,CASE WHEN RecQty <> 0 THEN RecCost / RecQty ELSE 0 END AS RecRate,CASE WHEN IssueQty <> 0 THEN IssueCost / IssueQty ELSE 0 END AS IssueRate  from (
                                    select Punching_Date as Punching_Date,max(Trans_Type)Trans_Type,Item_Code,Source_Doc_No,Location_Code,MAX(Stock_UOM)Stock_UOM,sum(Stock_Qty)Stock_Qty,max(InOut)InOut,
                                    sum(Stock_Qty * RI * case when Punching_Date< '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  then 1 else 0 end ) as OPQty,
                                    sum(Avg_Cost * RI * case when Punching_Date< '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  then 1 else 0 end ) as  OPCost,
                                    sum(Stock_Qty * (case when RI=1 then 1 else 0 end) * case when Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  then 1 else 0 end ) as RecQty
                                    ,sum(Avg_Cost * (case when RI=1 then 1 else 0 end) * case when Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  then 1 else 0 end ) as RecCost
                                    ,sum(Stock_Qty * (case when RI=-1 then 1 else 0 end) * (case when Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end)) as IssueQty
                                    ,sum(Avg_Cost * (case when RI=-1 then 1 else 0 end) * (case when Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end)) as IssueCost
                                    ,sum(Avg_Cost * RI  ) as CLCost,max(Cost_Code)Cost_Code,max(Add1)Add1,max(Add2)Add2,max(Add3)Add3,max(Comp_Name)Comp_Name,max(Item_Desc)Item_Desc
                                    from ( select TSPL_INVENTORY_MOVEMENT.Trans_Id,TSPL_INVENTORY_MOVEMENT.Trans_Type,TSPL_INVENTORY_MOVEMENT.InOut,
                                     TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code as Item_Code,TSPL_INVENTORY_MOVEMENT.Item_Desc,
                                    TSPL_INVENTORY_MOVEMENT.Source_Doc_No,TSPL_INVENTORY_MOVEMENT.Stock_Qty,TSPL_INVENTORY_MOVEMENT.Avg_Cost,
                                    TSPL_INVENTORY_MOVEMENT.Punching_Date,TSPL_INVENTORY_MOVEMENT.Stock_UOM,case when TSPL_INVENTORY_MOVEMENT.InOut='I' then 1 else -1 end as RI,
                                     TSPL_IssueReturn_DETAIL.Cost_Code,Add1,Add2,Add3,Comp_Name  from TSPL_INVENTORY_MOVEMENT
					                  left outer join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_DETAIL.Doc_No = TSPL_INVENTORY_MOVEMENT.Source_Doc_No and TSPL_IssueReturn_DETAIL.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code
					                  left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_INVENTORY_MOVEMENT.Comp_Code
                                    where  Punching_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 

                                      )xx where 2=2    and Location_Code = '" + clsCommon.myCstr(txtBillToLocation.Value) + "'  and Item_Code= '" + clsCommon.myCstr(TxtItem.Value) + "' 
                                      group by xx.Location_Code,xx.Punching_Date,xx.Source_Doc_No,xx.Item_Code 
                                      )xxx )
                                      select fromdate,ToDate,Location_Code,Format(Punching_Date,'dd/MMM/yyyy')Punching_Date,Source_Doc_No,Trans_Type,Item_Code,Item_Desc,Stock_UOM,
                                      Cost_Code,OPQty,OPCost,OPRate,RecQty,RecCost,RecRate,IssueQty,IssueCost,IssueRate,CL,CLCost,
                                      CASE WHEN CL <> 0 THEN CLCost / CL ELSE 0 END AS CLRate,Add1,Add2,Add3,Comp_Name from (
                                      select SNO,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,Location_Code,Punching_Date,Trans_Type,Item_Code,Source_Doc_No,OP as OPQty,OPCost,
					                  CASE WHEN OP <> 0 THEN OPCost / OP ELSE 0 END AS OPRate,RecQty,RecCost,RecRate,IssueQty, IssueCost,IssueRate,(OP+RecQty-IssueQty) as CL,
                                      CLCost,Cost_Code,Add1,Add2,Add3,Comp_Name,Item_Desc,Stock_UOM from  (
                                      select  (select (sum(Stock_Qty * (case when InOut='I' then 1 else -1 end ) * case when Punching_Date<=Punching_Date then 1 else 0 end ))  from my_cte as InnCTE 
                                      where InnCTE.Punching_Date<my_cte.Punching_Date) as OP,* from my_cte 
                                      where Punching_Date>= '" + clsCommon.GetPrintDate((txtFromDate.Value), "dd/MMM/yyyy") + "'  and Punching_Date<= '" + clsCommon.GetPrintDate((txtToDate.Value), "dd/MMM/yyyy") + "') xx)xxx
                                      order by xxx.Punching_Date asc "

            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.GroupDescriptors.Clear()
                Gv1.SummaryRowsBottom.Clear()
                Gv1.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage1
                Gv1.BestFitColumns()
                FormatGrid()
                'ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No data found to display.", "Transaction Wise Stock")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub FormatGrid()

        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
        Next
        Gv1.Columns("fromdate").IsVisible = False
        Gv1.Columns("ToDate").IsVisible = False
        Gv1.Columns("Add1").IsVisible = False
        Gv1.Columns("Add2").IsVisible = False
        Gv1.Columns("Add3").IsVisible = False
        Gv1.Columns("Comp_Name").IsVisible = False

        Gv1.Columns("Location_Code").Width = 100
        Gv1.Columns("Location_Code").IsVisible = True
        Gv1.Columns("Location_Code").HeaderText = "Location Code"
        Gv1.Columns("Location_Code").FormatString = "{0:n2}"

        Gv1.Columns("Punching_Date").Width = 100
        Gv1.Columns("Punching_Date").IsVisible = True
        Gv1.Columns("Punching_Date").HeaderText = "Punching Date"
        'Gv1.Columns("Punching_Date").FormatString = "{0:n2}"

        Gv1.Columns("Source_Doc_No").Width = 100
        Gv1.Columns("Source_Doc_No").IsVisible = True
        Gv1.Columns("Source_Doc_No").HeaderText = "Document No."
        Gv1.Columns("Source_Doc_No").FormatString = "{0:n2}"

        Gv1.Columns("Trans_Type").Width = 100
        Gv1.Columns("Trans_Type").IsVisible = True
        Gv1.Columns("Trans_Type").HeaderText = "Trans Type"
        Gv1.Columns("Trans_Type").FormatString = "{0:n2}"

        Gv1.Columns("Item_Code").Width = 100
        Gv1.Columns("Item_Code").IsVisible = True
        Gv1.Columns("Item_Code").HeaderText = "Item Code"
        Gv1.Columns("Item_Code").FormatString = "{0:n2}"

        Gv1.Columns("Stock_UOM").Width = 100
        Gv1.Columns("Stock_UOM").IsVisible = True
        Gv1.Columns("Stock_UOM").HeaderText = "Stock UOM"
        Gv1.Columns("Stock_UOM").FormatString = "{0:n2}"

        Gv1.Columns("Item_Desc").Width = 150
        Gv1.Columns("Item_Desc").IsVisible = True
        Gv1.Columns("Item_Desc").HeaderText = "Item Description"
        Gv1.Columns("Item_Desc").FormatString = "{0:n2}"

        Gv1.Columns("Cost_Code").Width = 150
        Gv1.Columns("Cost_Code").IsVisible = True
        Gv1.Columns("Cost_Code").HeaderText = "Cost Code"
        Gv1.Columns("Cost_Code").FormatString = "{0:n2}"

        Gv1.Columns("OPQty").Width = 100
        Gv1.Columns("OPQty").IsVisible = True
        Gv1.Columns("OPQty").HeaderText = "Opening Quantity"
        Gv1.Columns("OPQty").FormatString = "{0:n2}"

        Gv1.Columns("OPRate").Width = 100
        Gv1.Columns("OPRate").IsVisible = True
        Gv1.Columns("OPRate").HeaderText = "Opening Rate"
        Gv1.Columns("OPRate").FormatString = "{0:n2}"

        Gv1.Columns("OPCost").Width = 100
        Gv1.Columns("OPCost").IsVisible = True
        Gv1.Columns("OPCost").HeaderText = "Opening Cost"
        Gv1.Columns("OPCost").FormatString = "{0:n2}"

        Gv1.Columns("RecQty").Width = 100
        Gv1.Columns("RecQty").IsVisible = True
        Gv1.Columns("RecQty").HeaderText = "Received Quantity"
        Gv1.Columns("RecQty").FormatString = "{0:n2}"

        Gv1.Columns("RecRate").Width = 100
        Gv1.Columns("RecRate").IsVisible = True
        Gv1.Columns("RecRate").HeaderText = "Received Rate"
        Gv1.Columns("RecRate").FormatString = "{0:n2}"

        Gv1.Columns("RecCost").Width = 100
        Gv1.Columns("RecCost").IsVisible = True
        Gv1.Columns("RecCost").HeaderText = "Received Cost"
        Gv1.Columns("RecCost").FormatString = "{0:n2}"

        Gv1.Columns("IssueQty").Width = 100
        Gv1.Columns("IssueQty").IsVisible = True
        Gv1.Columns("IssueQty").HeaderText = "Issued Quantity"
        Gv1.Columns("IssueQty").FormatString = "{0:n2}"

        Gv1.Columns("IssueRate").Width = 100
        Gv1.Columns("IssueRate").IsVisible = True
        Gv1.Columns("IssueRate").HeaderText = "Issued Rate"
        Gv1.Columns("IssueRate").FormatString = "{0:n2}"

        Gv1.Columns("IssueCost").Width = 100
        Gv1.Columns("IssueCost").IsVisible = True
        Gv1.Columns("IssueCost").HeaderText = "Issued Cost"
        Gv1.Columns("IssueCost").FormatString = "{0:n2}"

        Gv1.Columns("CL").Width = 100
        Gv1.Columns("CL").IsVisible = True
        Gv1.Columns("CL").HeaderText = "Closing Quantity"
        Gv1.Columns("CL").FormatString = "{0:n2}"

        Gv1.Columns("CLCost").Width = 100
        Gv1.Columns("CLCost").IsVisible = True
        Gv1.Columns("CLCost").HeaderText = "Closing Cost"
        Gv1.Columns("CLCost").FormatString = "{0:n2}"

        Gv1.Columns("CLRate").Width = 100
        Gv1.Columns("CLRate").IsVisible = True
        Gv1.Columns("CLRate").HeaderText = "Closing Rate"
        Gv1.Columns("CLRate").FormatString = "{0:n2}"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item1 As New GridViewSummaryItem("OPQty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("OPRate", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("OPCost", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("RecQty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("RecRate", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        Dim item6 As New GridViewSummaryItem("RecCost", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item7 As New GridViewSummaryItem("IssueQty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim item8 As New GridViewSummaryItem("IssueRate", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)

        Dim item9 As New GridViewSummaryItem("IssueCost", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)

        Dim item0 As New GridViewSummaryItem("CL", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item0)

        Dim item As New GridViewSummaryItem("CLRate", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item)

        Dim items As New GridViewSummaryItem("CLCost", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(items)

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Private Sub txtBillToLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBillToLocation._MYValidating
        Dim qry As String = " Select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim whrcls As String = " Location_Type='Physical' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls += " and Location_Code in(" + objCommonVar.strCurrUserLocations + ") "
        End If
        txtBillToLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", whrcls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER Where Location_Code= '" + txtBillToLocation.Value + "'"))

    End Sub
    Private Sub TxtItem__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtItem._MYValidating
        Dim qry As String = " Select Item_Code as ItemCode,Item_Desc as ItemDescription from TSPL_ITEM_MASTER "
        'TxtItem.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", whrcls, TxtItem.Value, "ItemCode", isButtonClicked)
        TxtItem.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "ItemCode", Nothing, TxtItem.Value, "ItemCode", isButtonClicked)
        lblItem.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER Where Item_Code= '" + TxtItem.Value + "'"))
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If
                arrHeader.Add("Location:" + clsCommon.myCstr(lblBillToLocation.Text))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Transaction Wise Stock", Gv1, arrHeader, Me.Text)
                ' transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                'Process.Start(filePath)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If
                If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.myCstr(lblBillToLocation.Text))
                End If
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Transaction Wise Stock", Gv1, arrHeader, "Transaction Wise Stock", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rptTransactionWiseStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        Reset()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim qry As String = " "

        Try
            qry = "  WITH my_cte AS (
                                    select ROW_NUMBER() over (Partition by 1 order by Punching_Date) as SNO , *,CASE WHEN RecQty <> 0 THEN RecCost / RecQty ELSE 0 END AS RecRate,CASE WHEN IssueQty <> 0 THEN IssueCost / IssueQty ELSE 0 END AS IssueRate  from (
                                    select Punching_Date as Punching_Date,max(Trans_Type)Trans_Type,Item_Code,Source_Doc_No,Location_Code,MAX(Stock_UOM)Stock_UOM,sum(Stock_Qty)Stock_Qty,max(InOut)InOut,
                                    sum(Stock_Qty * RI * case when Punching_Date< '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  then 1 else 0 end ) as OPQty,
                                    sum(Avg_Cost * RI * case when Punching_Date< '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  then 1 else 0 end ) as  OPCost,
                                    sum(Stock_Qty * (case when RI=1 then 1 else 0 end) * case when Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  then 1 else 0 end ) as RecQty
                                    ,sum(Avg_Cost * (case when RI=1 then 1 else 0 end) * case when Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  then 1 else 0 end ) as RecCost
                                    ,sum(Stock_Qty * (case when RI=-1 then 1 else 0 end) * (case when Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end)) as IssueQty
                                    ,sum(Avg_Cost * (case when RI=-1 then 1 else 0 end) * (case when Punching_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end)) as IssueCost
                                    ,sum(Avg_Cost * RI  ) as CLCost,max(Cost_Code)Cost_Code,max(Add1)Add1,max(Add2)Add2,max(Add3)Add3,max(Comp_Name)Comp_Name,max(Item_Desc)Item_Desc
                                    from ( select TSPL_INVENTORY_MOVEMENT.Trans_Id,TSPL_INVENTORY_MOVEMENT.Trans_Type,TSPL_INVENTORY_MOVEMENT.InOut,
                                     TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_INVENTORY_MOVEMENT.Item_Code as Item_Code,TSPL_INVENTORY_MOVEMENT.Item_Desc,
                                    TSPL_INVENTORY_MOVEMENT.Source_Doc_No,TSPL_INVENTORY_MOVEMENT.Stock_Qty,TSPL_INVENTORY_MOVEMENT.Avg_Cost,
                                    TSPL_INVENTORY_MOVEMENT.Punching_Date,TSPL_INVENTORY_MOVEMENT.Stock_UOM,case when TSPL_INVENTORY_MOVEMENT.InOut='I' then 1 else -1 end as RI,
                                     TSPL_IssueReturn_DETAIL.Cost_Code,Add1,Add2,Add3,Comp_Name  from TSPL_INVENTORY_MOVEMENT
					                  left outer join TSPL_IssueReturn_DETAIL on TSPL_IssueReturn_DETAIL.Doc_No = TSPL_INVENTORY_MOVEMENT.Source_Doc_No and TSPL_IssueReturn_DETAIL.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code
					                  left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_INVENTORY_MOVEMENT.Comp_Code
                                    where  Punching_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 

                                      )xx where 2=2    and Location_Code = '" + clsCommon.myCstr(txtBillToLocation.Value) + "'  and Item_Code= '" + clsCommon.myCstr(TxtItem.Value) + "' 
                                      group by xx.Location_Code,xx.Punching_Date,xx.Source_Doc_No,xx.Item_Code 
                                      )xxx )
                                      select fromdate,ToDate,Location_Code,Format(Punching_Date,'dd/MMM/yyyy')Punching_Date,Source_Doc_No,Trans_Type,Item_Code,Item_Desc,Stock_UOM,
                                      Cost_Code,OPQty,OPCost,OPRate,RecQty,RecCost,RecRate,IssueQty,IssueCost,IssueRate,CL,CLCost,
                                      CASE WHEN CL <> 0 THEN CLCost / CL ELSE 0 END AS CLRate,Add1,Add2,Add3,Comp_Name from (
                                      select SNO,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' as fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' as ToDate,Location_Code,Punching_Date,Trans_Type,Item_Code,Source_Doc_No,OP as OPQty,OPCost,
					                  CASE WHEN OP <> 0 THEN OPCost / OP ELSE 0 END AS OPRate,RecQty,RecCost,RecRate,IssueQty, IssueCost,IssueRate,(OP+RecQty-IssueQty) as CL,
                                      CLCost,Cost_Code,Add1,Add2,Add3,Comp_Name,Item_Desc,Stock_UOM from  (
                                      select  (select (sum(Stock_Qty * (case when InOut='I' then 1 else -1 end ) * case when Punching_Date<=Punching_Date then 1 else 0 end ))  from my_cte as InnCTE 
                                      where InnCTE.Punching_Date<my_cte.Punching_Date) as OP,* from my_cte 
                                      where Punching_Date>= '" + clsCommon.GetPrintDate((txtFromDate.Value), "dd/MMM/yyyy") + "'  and Punching_Date<= '" + clsCommon.GetPrintDate((txtToDate.Value), "dd/MMM/yyyy") + "') xx)xxx
                                      order by xxx.Punching_Date asc  "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.Purchase, dt, "rptTransactionWiseStock", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class