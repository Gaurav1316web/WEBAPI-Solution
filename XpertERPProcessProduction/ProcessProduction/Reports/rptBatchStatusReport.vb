Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'================Created By Preeti Gupta-==============
'' changes by shivani(added location filter and date)against ticket no[BM00000008609]
Public Class RptBatchStatusReport
    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptBatchStatus)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Sub Print(ByVal IsPrint As Exporter)

        Try
            '' tested by panch raj against ticket no : KDI/12/04/18-000225
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")

            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If txtBatchNoMult.arrDispalyMember IsNot Nothing AndAlso txtBatchNoMult.arrDispalyMember.Count > 0 Then

                arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNoMult.arrDispalyMember))
            End If
            If txtItemMult.arrDispalyMember IsNot Nothing AndAlso txtItemMult.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
            End If
            Dim qry As String = Nothing

            '======================================================================================================================
            ' KUNAL > TICKET : BM00000009508 > DATE : 10-NOV-2016 > Client Remark : Pending…item name is not coming, production qty is showing incorrect data along with pending & status column
            ' TASK : Existing query was showing incorrect result, so whole query was changed in this ticket written below.
            '======================================================================================================================
            ' Ticket No : BHA/16/07/18-000167 By Prabhakar - Manual Batch No add 
            Dim Baseqry As String = " SELECT [Prd Entry Code],[Batch Code],ManualBatchNo,[BATCH_DATE],[Location], [Location Desc],[Section Code],[Batch Item],[Item Desc], "

            'Sanjay Start Add Return Quantity
            'If chk_stockingunit.Checked Then
            '    Baseqry += " stockunitconv.Uom_Code as [Batch UOM],((isnull(unitconvbatch.Conversion_Factor,1)*[Batch Qty])/isnull(stockunitconv.Conversion_Factor,1))  as [Batch Qty],((isnull(unitconv.Conversion_Factor,1)*[Produced Qty])/isnull(stockunitconv.Conversion_Factor,1)) as [Produced Qty],stockunitconv.Uom_Code as [Prd Item UOM],((isnull(unitconv.Conversion_Factor,1)*[Pending Qty])/isnull(stockunitconv.Conversion_Factor,1)) as [Pending Qty],"
            'Else
            '    Baseqry += " [Batch UOM],[Batch Qty],[Produced Qty],[Prd Item UOM],[Pending Qty],"
            'End If

            'Baseqry += " ((CASE WHEN [Pending Qty] < 0 THEN 'Close'  WHEN [Pending Qty] > 0 THEN 'WIP'  WHEN [Pending Qty] = 0 THEN 'Close'  WHEN [Produced Qty] = 0 THEN 'Open'  END)) AS Status FROM (SELECT  MAX(TBL0.[Prd Entry Code]) [Prd Entry Code], TBL0.[Batch Code],  MAX(TBL0.[BATCH_DATE]) [BATCH_DATE], MAX(TBL0.[Location]) [Location],  MAX(TBL0.[Location Desc]) [Location Desc],  MAX(TBL0.[Section Code]) [Section Code], TBL0.[Batch Item],  MAX(TBL0.[Item Desc]) [Item Desc], MAX(TBL0.[Batch UOM]) [Batch UOM], MAX(TBL0.[Batch Qty]) [Batch Qty], SUM(TBL0.[Produced Qty]) [Produced Qty], MAX(TBL0.[Prd Item UOM]) [Prd Item UOM], CASE WHEN ((MAX(TBL0.[Batch Qty]) - SUM(TBL0.[Produced Qty]))) < 0 THEN 0  ELSE ((MAX(TBL0.[Batch Qty]) - SUM(TBL0.[Produced Qty]))) END AS [Pending Qty] FROM (SELECT  PED.PROD_ENTRY_CODE AS [Prd Entry Code], PEH.Batch_Code AS [Batch Code], PEH.BATCH_DATE AS [BATCH_DATE], PED.LOCATION_CODE AS [Location], LM.Location_Desc AS [Location Desc], PED.Section_Code AS [Section Code], PED.ITEM_CODE AS [Batch Item],  PED.ITEM_DESCRIPTION AS [Item Desc], PED.UNIT_CODE AS [Batch UOM], PED.BATCH_QTY AS [Batch Qty], PED.FINAL_PRODUCTION_QTY AS [Produced Qty], PED.UNIT_CODE AS [Prd Item UOM] FROM TSPL_PP_PRODUCTION_ENTRY PEH LEFT JOIN TSPL_PP_PRODUCTION_ENTRY_DETAIL PED ON PEH.PROD_ENTRY_CODE = PED.PROD_ENTRY_CODE LEFT JOIN TSPL_LOCATION_MASTER LM ON  PED.LOCATION_CODE = LM.Location_Code) AS TBL0 GROUP BY [Batch Code], [Batch Item] UNION ALL SELECT MAX(TBL0.[Prd Entry Code]) [Prd Entry Code], TBL0.[Batch Code], MAX(TBL0.[BATCH_DATE]) [BATCH_DATE], MAX(TBL0.[Location]) [Location], MAX(TBL0.[Location Desc]) [Location Desc],  MAX(TBL0.[Section Code]) [Section Code], TBL0.[Batch Item], MAX(TBL0.[Item Desc]) [Item Desc], MAX(TBL0.[Batch UOM]) [Batch UOM], MAX(TBL0.[Batch Qty]) [Batch Qty], SUM(TBL0.[Produced Qty]) [Produced Qty],  MAX(TBL0.[Prd Item UOM]) [Prd Item UOM], CASE WHEN ((MAX(TBL0.[Batch Qty]) - SUM(TBL0.[Produced Qty]))) < 0 THEN 0 ELSE ((MAX(TBL0.[Batch Qty]) - SUM(TBL0.[Produced Qty]))) END AS [Pending Qty] FROM (SELECT PED.Standardization_Code AS [Prd Entry Code], PEH.Child_Batch_Code AS [Batch Code], PEH.Standardization_Date AS [BATCH_DATE], PED.STD_Loaction_Code AS [Location], LM.Location_Desc AS [Location Desc],  PED.Section_Code AS [Section Code],  PED.ITEM_CODE AS [Batch Item], IM.Item_Desc AS [Item Desc], PED.UNIT_CODE AS [Batch UOM], PED.Quantity AS [Batch Qty],  PED.Produced_Qty AS [Produced Qty], PED.UNIT_CODE AS [Prd Item UOM] FROM TSPL_PP_STANDARDIZATION_HEAD PEH LEFT JOIN  TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL PED ON PEH.Standardization_Code = PED.Standardization_Code LEFT JOIN TSPL_LOCATION_MASTER LM ON PED.STD_Loaction_Code = LM.Location_Code LEFT JOIN TSPL_ITEM_MASTER IM ON IM.Item_Code = PED.Item_Code) AS TBL0 GROUP BY [Batch Code], [Batch Item]) AS FINAL "


            If chk_stockingunit.Checked Then
                Baseqry += " stockunitconv.Uom_Code as [Batch UOM],((isnull(unitconvbatch.Conversion_Factor,1)*[Batch Qty])/isnull(stockunitconv.Conversion_Factor,1))  as [Batch Qty],((isnull(unitconv.Conversion_Factor,1)*[Produced Qty])/isnull(stockunitconv.Conversion_Factor,1)) as [Produced Qty],((isnull(unitconv.Conversion_Factor,1)*[Return Qty])/isnull(stockunitconv.Conversion_Factor,1)) as [Return Qty],stockunitconv.Uom_Code as [Prd Item UOM],((isnull(unitconv.Conversion_Factor,1)*[Pending Qty])/isnull(stockunitconv.Conversion_Factor,1)) as [Pending Qty],"
            Else
                Baseqry += " [Batch UOM],[Batch Qty],[Produced Qty],[Return Qty],[Prd Item UOM],[Pending Qty],"
            End If

            Baseqry += " ((CASE WHEN [Pending Qty] < 0 THEN 'Close'  WHEN [Pending Qty] > 0 THEN 'WIP'  WHEN [Pending Qty] = 0 THEN 'Close'  WHEN [Produced Qty] = 0 THEN 'Open'  END)) AS Status FROM (SELECT  MAX(TBL0.[Prd Entry Code]) [Prd Entry Code], TBL0.[Batch Code],  MAX(TBL0.[BATCH_DATE]) [BATCH_DATE], MAX(TBL0.[Location]) [Location],  MAX(TBL0.[Location Desc]) [Location Desc],  MAX(TBL0.[Section Code]) [Section Code], TBL0.[Batch Item],  MAX(TBL0.[Item Desc]) [Item Desc], MAX(TBL0.[Batch UOM]) [Batch UOM], MAX(TBL0.[Batch Qty]) [Batch Qty], SUM(TBL0.[Produced Qty]) [Produced Qty], MAX(TBL0.[Prd Item UOM]) [Prd Item UOM], CASE WHEN ((MAX(TBL0.[Batch Qty]) - SUM(TBL0.[Produced Qty]) + sum(TBL0.[Return Qty]) )) < 0 THEN 0  ELSE ((MAX(TBL0.[Batch Qty]) - SUM(TBL0.[Produced Qty]) + sum(TBL0.[Return Qty]))) END AS [Pending Qty],sum(TBL0.[Return Qty]) as [Return Qty],max(TBL0.ManualBatchNo ) ManualBatchNo FROM (SELECT  PED.PROD_ENTRY_CODE AS [Prd Entry Code], PEH.Batch_Code AS [Batch Code], PEH.BATCH_DATE AS [BATCH_DATE], PED.LOCATION_CODE AS [Location], LM.Location_Desc AS [Location Desc], PED.Section_Code AS [Section Code], PED.ITEM_CODE AS [Batch Item],  PED.ITEM_DESCRIPTION AS [Item Desc], PED.UNIT_CODE AS [Batch UOM], PED.BATCH_QTY AS [Batch Qty], PED.FINAL_PRODUCTION_QTY AS [Produced Qty], PED.UNIT_CODE AS [Prd Item UOM],(CASE WHEN PER.PROD_RETURN_CODE IS NOT NULL THEN PED.FINAL_PRODUCTION_QTY ELSE 0 END) as [Return Qty],isnull(PEH.ManualBatchNo ,'') as ManualBatchNo FROM TSPL_PP_PRODUCTION_ENTRY PEH LEFT JOIN TSPL_PP_PRODUCTION_ENTRY_DETAIL PED ON PEH.PROD_ENTRY_CODE = PED.PROD_ENTRY_CODE LEFT JOIN TSPL_LOCATION_MASTER LM ON  PED.LOCATION_CODE = LM.Location_Code "

            Baseqry += " LEFT JOIN TSPL_PP_PRODUCTION_RETURN PER ON PEH.PROD_ENTRY_CODE=PER.PROD_ENTRY_CODE"

            Baseqry += " ) AS TBL0 GROUP BY [Batch Code], [Batch Item] UNION ALL SELECT MAX(TBL0.[Prd Entry Code]) [Prd Entry Code], TBL0.[Batch Code], MAX(TBL0.[BATCH_DATE]) [BATCH_DATE], MAX(TBL0.[Location]) [Location], MAX(TBL0.[Location Desc]) [Location Desc],  MAX(TBL0.[Section Code]) [Section Code], TBL0.[Batch Item], MAX(TBL0.[Item Desc]) [Item Desc], MAX(TBL0.[Batch UOM]) [Batch UOM], MAX(TBL0.[Batch Qty]) [Batch Qty], SUM(TBL0.[Produced Qty]) [Produced Qty],  MAX(TBL0.[Prd Item UOM]) [Prd Item UOM], CASE WHEN ((MAX(TBL0.[Batch Qty]) - SUM(TBL0.[Produced Qty]))) < 0 THEN 0 ELSE ((MAX(TBL0.[Batch Qty]) - SUM(TBL0.[Produced Qty]))) END AS [Pending Qty],0 as [Return Qty], max(TBL0.ManualBatchNo ) ManualBatchNo FROM (SELECT PED.Standardization_Code AS [Prd Entry Code], PEH.Child_Batch_Code AS [Batch Code], PEH.Standardization_Date AS [BATCH_DATE], PED.STD_Loaction_Code AS [Location], LM.Location_Desc AS [Location Desc],  PED.Section_Code AS [Section Code],  PED.ITEM_CODE AS [Batch Item], IM.Item_Desc AS [Item Desc], PED.UNIT_CODE AS [Batch UOM], PED.Quantity AS [Batch Qty],  PED.Produced_Qty AS [Produced Qty], PED.UNIT_CODE AS [Prd Item UOM],isnull(PEH.ManualBatchNo ,'') as ManualBatchNo FROM TSPL_PP_STANDARDIZATION_HEAD PEH LEFT JOIN  TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL PED ON PEH.Standardization_Code = PED.Standardization_Code LEFT JOIN TSPL_LOCATION_MASTER LM ON PED.STD_Loaction_Code = LM.Location_Code LEFT JOIN TSPL_ITEM_MASTER IM ON IM.Item_Code = PED.Item_Code) AS TBL0 GROUP BY [Batch Code], [Batch Item]) AS FINAL "

            'Sanjay End Add Return Quantity

            If chk_stockingunit.Checked Then
                Baseqry += " left join (select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code) as stockunitconv on stockunitconv.Item_Code=final.[Batch Item]  "
                Baseqry += " left join TSPL_ITEM_UOM_DETAIL unitconv on unitconv.Item_Code=final.[Batch Item] and unitconv.UOM_Code=final.[Prd Item UOM]"
                Baseqry += " left join TSPL_ITEM_UOM_DETAIL unitconvbatch on unitconvbatch.Item_Code=final.[Batch Item] and unitconvbatch.UOM_Code=[Batch UOM]"
            End If
            Baseqry += " where 2=2 and convert(date,FINAL.[BATCH_DATE],103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,FINAL.[BATCH_DATE],103) <=convert(date,'" + txtToDate.Value + "' ,103)"

            If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
                Baseqry += " and  FINAL.[Batch Code] in (" + clsCommon.GetMulcallString(txtBatchNoMult.arrValueMember) + ")  "
            End If
            If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                Baseqry += " and  FINAL.[Batch Item] in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
            End If
            If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                Baseqry += " and  FINAL.[Section Code] in (" + clsCommon.GetMulcallString(txtSectionMult.arrValueMember) + ")  "
            End If
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                Baseqry += " and FINAL.Location in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
            End If
            ' KUNAL > TICKET : BM00000009508 > DATE : 4 OCT 2016
            Baseqry += "ORDER BY FINAL.[Batch Code] ASC"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Baseqry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = False

                gv1.EnableFiltering = True

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If

            gv1.DataSource = dt
            SetGridFormationOFGV1()
            gv1.BestFitColumns()
            chk_stockingunit.Enabled = False
            FindAndRestoreGridLayout(Me)
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        Try
            ' KUNAL > TICKET : BM00000009508 > DATE : 10-NOV-2016

            gv1.TableElement.TableHeaderHeight = 40
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = False
            Next

            gv1.Columns("Prd Entry Code").Width = 50
            gv1.Columns("Prd Entry Code").IsVisible = True
            gv1.Columns("Prd Entry Code").HeaderText = "Prd Entry Code"

            gv1.Columns("Batch Code").Width = 50
            gv1.Columns("Batch Code").IsVisible = True
            gv1.Columns("Batch Code").HeaderText = "Batch Code"

            gv1.Columns("BATCH_DATE").Width = 100
            gv1.Columns("BATCH_DATE").IsVisible = True
            gv1.Columns("BATCH_DATE").HeaderText = "Batch Date"

            gv1.Columns("ManualBatchNo").Width = 50
            gv1.Columns("ManualBatchNo").IsVisible = True
            gv1.Columns("ManualBatchNo").HeaderText = "Manual Batch No"

            gv1.Columns("Location").Width = 100
            gv1.Columns("Location").IsVisible = True
            gv1.Columns("Location").HeaderText = "Location Code"

            gv1.Columns("Location Desc").Width = 100
            gv1.Columns("Location Desc").IsVisible = True
            gv1.Columns("Location Desc").HeaderText = "Location Desc"

            gv1.Columns("Section Code").Width = 100
            gv1.Columns("Section Code").IsVisible = True
            gv1.Columns("Section Code").HeaderText = "Section Code"

            gv1.Columns("Batch Item").Width = 150
            gv1.Columns("Batch Item").IsVisible = True
            gv1.Columns("Batch Item").HeaderText = "Batch Item"

            gv1.Columns("Item Desc").Width = 100
            gv1.Columns("Item Desc").IsVisible = True
            gv1.Columns("Item Desc").HeaderText = "Item Desc"

            gv1.Columns("Batch UOM").Width = 100
            gv1.Columns("Batch UOM").IsVisible = True
            gv1.Columns("Batch UOM").HeaderText = "Batch UOM"

            gv1.Columns("Batch Qty").Width = 100
            gv1.Columns("Batch Qty").IsVisible = True
            gv1.Columns("Batch Qty").HeaderText = "Batch Qty"

            gv1.Columns("Produced Qty").Width = 100
            gv1.Columns("Produced Qty").IsVisible = True
            gv1.Columns("Produced Qty").HeaderText = "Produced Qty"

            gv1.Columns("Return Qty").Width = 100
            gv1.Columns("Return Qty").IsVisible = True
            gv1.Columns("Return Qty").HeaderText = "Return Qty"

            gv1.Columns("Prd Item UOM").Width = 100
            gv1.Columns("Prd Item UOM").IsVisible = True
            gv1.Columns("Prd Item UOM").HeaderText = "Prd Item UOM"

            gv1.Columns("Pending Qty").Width = 100
            gv1.Columns("Pending Qty").IsVisible = True
            gv1.Columns("Pending Qty").HeaderText = "Pending Qty"

            gv1.Columns("Status").Width = 100
            gv1.Columns("Status").IsVisible = True
            gv1.Columns("Status").HeaderText = "Status"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            'Dim item1 As New GridViewSummaryItem("Stock_Qty", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item1)
            'Dim item2 As New GridViewSummaryItem("Cost", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item2)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


            'ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        Try
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = txtToDate.Value.AddMonths(-1)
            gv1.DataSource = Nothing
            TxtMultiLocation.arrValueMember = Nothing
            txtSectionMult.arrValueMember = Nothing
            txtBatchNoMult.arrValueMember = Nothing
            txtItemMult.arrValueMember = Nothing
            chk_stockingunit.Checked = False
            chk_stockingunit.Enabled = True
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
       
    End Sub
    Private Sub RptBatchStatusReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
            ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RptBatchStatusReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub txtItemMult__My_Click(sender As Object, e As EventArgs) Handles txtItemMult._My_Click
        Dim qry As String = " select TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name from TSPL_ITEM_MASTER "
        txtItemMult.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultItemNo", qry, "Code", "Name", txtItemMult.arrValueMember, txtItemMult.arrDispalyMember)
    End Sub

    Private Sub txtSectionMult__My_Click(sender As Object, e As EventArgs) Handles txtSectionMult._My_Click
        Dim qry As String = " select distinct Section_Code as Code , Section_Code as Name from TSPL_PP_BATCH_ORDER_HEAD where Section_Code is not null "
        txtSectionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("txtSectionMult", qry, "Code", "Name", txtSectionMult.arrValueMember, txtSectionMult.arrDispalyMember)
    End Sub

    Private Sub txtBatchNoMult__My_Click(sender As Object, e As EventArgs) Handles txtBatchNoMult._My_Click
        Dim qry As String = " select TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as Code,TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as Name  from TSPL_PP_BATCH_ORDER_HEAD "
        txtBatchNoMult.arrValueMember = clsCommon.ShowMultipleSelectForm("multBatchNo", qry, "Code", "Name", txtBatchNoMult.arrValueMember, txtBatchNoMult.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER  "
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Pro", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub


    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptItemConsumptionReport & "'"))


            If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNoMult.arrDispalyMember))
            End If
            If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
            End If
            If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Section : " + clsCommon.GetMulcallStringWithComma(txtSectionMult.arrDispalyMember))
            End If
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
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
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptItemConsumptionReport & "'"))


            If txtBatchNoMult.arrValueMember IsNot Nothing AndAlso txtBatchNoMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtBatchNoMult.arrDispalyMember))
            End If
            If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
            End If
            If txtSectionMult.arrValueMember IsNot Nothing AndAlso txtSectionMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Section : " + clsCommon.GetMulcallStringWithComma(txtSectionMult.arrDispalyMember))
            End If
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
            End If

            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
