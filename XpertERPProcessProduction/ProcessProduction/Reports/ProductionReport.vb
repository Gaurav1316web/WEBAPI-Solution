Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class ProductionReport
    Inherits FrmMainTranScreen

#Region "Variables"

    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Dim arrLoc As String = Nothing
    Dim FORMTYPE As String = Nothing

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptItemConsumptionReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'rmExportToExcel.Visible = MyBase.isExport
        btnSplitExport.Visible = MyBase.isExport
    End Sub
#End Region
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtBillToLocation.Value = Nothing
        lblBillToLocation.Text = ""
        'TxtMultiLocation.arrValueMember = Nothing
        Gv1.DataSource = Nothing
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        'Dim DeleteTempDataFromTime As String = clsCommon.myCDate(clsFixedParameter.GetData(clsFixedParameterType.DeleteTempDataFromTime, clsFixedParameterCode.DeleteTempDataFromTime, Nothing))
        Dim DeleteTempDataFromTime As String
        If clsCommon.myLen(clsFixedParameter.GetData(clsFixedParameterType.DeleteTempDataFromTime, clsFixedParameterCode.DeleteTempDataFromTime, Nothing)) > 0 Then
            DeleteTempDataFromTime = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DeleteTempDataFromTime, clsFixedParameterCode.DeleteTempDataFromTime, Nothing))
            'DeleteTempDataToTime = clsCommon.myCDate(clsFixedParameter.GetData(clsFixedParameterType.DeleteTempDataToTime, clsFixedParameterCode.DeleteTempDataToTime, Nothing))
        End If

        Dim timeParts() As String = DeleteTempDataFromTime.Split("-"c)

        If timeParts.Length = 2 Then
            ' Trim the parts
            Dim fromTimeStr As String = timeParts(0).Trim()
            Dim toTimeStr As String = timeParts(1).Trim()

            ' Get string lengths
            Dim fromTimeLength As Integer = fromTimeStr.Length
            Dim toTimeLength As Integer = toTimeStr.Length

            ' Output
            Console.WriteLine("From Time: " & fromTimeStr & " | Length: " & fromTimeLength)
            Console.WriteLine("To Time: " & toTimeStr & " | Length: " & toTimeLength)

            ' Validate length and convert to DateTime
            If fromTimeLength = 11 AndAlso toTimeLength = 11 Then ' e.g., "07:00:00 AM" = 11 chars
                Dim fromTime As DateTime = clsCommon.myCDate(fromTimeStr)
                Dim toTime As DateTime = clsCommon.myCDate(toTimeStr)

                ' Now you can use fromTime and toTime
                If fromTime < toTime Then
                    Console.WriteLine("Valid time range.")
                End If
            Else
                Console.WriteLine("Invalid time format length.")
            End If
        Else
            Console.WriteLine("Time range format is invalid.")
        End If
        'Else
        '    Console.WriteLine("Time range string is empty or null.")
        'End If
        'Me.Close()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Load_Production_Report()
    End Sub

    Private Sub Load_Production_Report()
        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            Dim whr As String = ""
            Dim Status1 As String = ""
            Dim FG As String = ""
            Dim SFG As String = ""
            Dim FGSFG As String = ""
            If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                whr += " and LOCATION_CODE IN ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
            Else
                'whr += " and TSPL_LOCATION_MASTER.Location_Type='Physical'  "
                'If clsCommon.myLen(arrLoc) > 0 Then
                'whr += "  and  LOCATION_CODE IN (" + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrValueMember) + ") "
                'End If
            End If
            If rdbPosted.IsChecked = True Then
                Status1 = "  and TSPL_SPP_PRODUCTION_ENTRY.posted=1 "
            ElseIf rdbUnposted.IsChecked = True Then
                Status1 = " and TSPL_SPP_PRODUCTION_ENTRY.posted=0 "
            ElseIf rdbAll.IsChecked = True Then

            End If
            Dim Itemqry As String = ""
            Dim itemNames1 As String = Nothing
            If rdbFG.IsChecked = True Then
                FG = " and TSPL_Item_Master.FG_for_CF_RPT=1 "
                Itemqry = " Select Item_Code from TSPL_ITEM_MASTER WHERE FG_for_CF_RPT=1 "
            ElseIf rdbSFG.IsChecked = True Then
                SFG = " and TSPL_Item_Master.SFG_for_CF=1 "
                Itemqry = " Select Item_Code from TSPL_ITEM_MASTER WHERE SFG_for_CF=1 "
            ElseIf rdbfgsfg.IsChecked = True Then
                FGSFG = " and TSPL_Item_Master.FG_for_CF=1 "
                Itemqry = " Select Item_Code from TSPL_ITEM_MASTER WHERE FG_for_CF=1 "
            End If
            Dim dtitemName As DataTable = clsDBFuncationality.GetDataTable(Itemqry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    If i = 0 Then
                        itemNames1 += "'" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "' "
                    Else
                        itemNames1 += ", '" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "' "
                    End If
                Next
            End If
            qry = " Select * from (SELECT 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as ToDate,   LOCATION_CODE,Location_Desc as [Location Description],Add1,Add4,CONVERT(date, PROD_DATE,103) as PROD_DATE,[Item Code],ITEM_DESCRIPTION,isnull ([A-SHIFT], 0)  as [A-SHIFT],isnull ([B-SHIFT], 0) as [B-SHIFT], isnull ([C-SHIFT],0) as [C-SHIFT],
                          isnull ([WHOLEDAY],0) as [WHOLEDAY] , (  isnull ([A-SHIFT], 0)  + isnull ([B-SHIFT], 0) + isnull ([C-SHIFT],0) + isnull ([WHOLEDAY],0) ) AS [TOTAL BAG],   
                          (((  isnull ([A-SHIFT], 0)  + isnull ([B-SHIFT], 0) + isnull ([C-SHIFT],0) + isnull ([WHOLEDAY],0) )* 50)/1000) as [Total MT]
                          FROM ( Select max(Add1)Add1,max(Add4)Add4,max(Location_Desc)Location_Desc,[Item Code],max(ITEM_DESCRIPTION)ITEM_DESCRIPTION,
                               LOCATION_CODE,(PROD_DATE)PROD_DATE,shiftcode "
                        If Productionchk.IsChecked = True Then
                qry += " ,Sum(qty_bag1) as qty_bag "
            ElseIf RePrdntchk.IsChecked = True Then
                qry += " ,Sum(qty_bag3) as qty_bag  "
            ElseIf Prdncreallchk.IsChecked = True Then
                qry += " ,Sum(qty_bag1+qty_bag2) as qty_bag "
            End If
            qry += "  from (SELECT TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add4,TSPL_LOCATION_MASTER.Location_Desc,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE as [Item Code],
                                   TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_DESCRIPTION, "

            'If Productionchk.IsChecked = True Then
            '    qry += " (FINAL_PRODUCTION_QTY/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as qty_bag, "
            'ElseIf RePrdntchk.IsChecked = True Then
            '    qry += " ((TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Reprocess_Qty-(FINAL_PRODUCTION_QTY/TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) ) as qty_bag,  "
            'ElseIf Prdncreallchk.IsChecked = True Then
            '    qry += " (TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Reprocess_Qty) As qty_bag, "
            'End If

            qry += " (FINAL_PRODUCTION_QTY/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as qty_bag1,
					isnull(TSPL_SPP_PRODUCTION_ENTRY.Reprocess_H_Qty,0) as qty_bag2,
					((TSPL_SPP_PRODUCTION_ENTRY_DETAIL.Reprocess_Qty-(FINAL_PRODUCTION_QTY/TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) ) as qty_bag3,
                    TSPL_SPP_PRODUCTION_ENTRY.Shift_Code as shiftcode FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL
								   left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                                   left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE and TSPL_ITEM_UOM_DETAIL.UOM_Code='bag'
								   left join TSPL_ITEM_UOM_DETAIL AS MT_ITEM_UOM_DETAIL on MT_ITEM_UOM_DETAIL.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE and MT_ITEM_UOM_DETAIL.UOM_Code='MT'
								   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE
                                   LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE "

            qry += " where 2=2  " + Status1 + " " + FG + " " + SFG + " " + FGSFG + " and TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE >= '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' 
                     and TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE<= '" + clsCommon.GetPrintDate(txtToDate.Value) + "'"
            qry += " )Tab1 group by PROD_DATE,LOCATION_CODE,[Item Code],shiftcode)YY
                                    PIVOT(SUM(qty_bag) FOR shiftcode IN ([A-SHIFT],[B-SHIFT],[C-SHIFT],[WHOLEDAY])) AS Tab2 )tmp
									where [Item Code] IN (" & itemNames1 & ")  and tmp.PROD_DATE >= '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and tmp.PROD_DATE<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'" + whr + "  order by PROD_DATE "
            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.GroupDescriptors.Clear()
                Gv1.SummaryRowsBottom.Clear()
                Gv1.DataSource = dt
                'gv1.Columns("TransType").IsVisible = False
                'gv1.Columns("PROD_ENTRY_CODE").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                FormatGrid()
                summary()
                'View()
                'ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow("No data found to display.", "Production Report")
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
            Gv1.Columns(ii).IsVisible = False
        Next

        Gv1.Columns("Location_Code").Width = 100
        Gv1.Columns("Location_Code").IsVisible = True
        Gv1.Columns("Location_Code").HeaderText = "Location Code"

        'Dim Prod_Date As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        'Prod_Date.CustomFormat = "dd/MM/yyyy"
        'Prod_Date.FormatString = "{0:d}"
        ' Gv1.Columns("Prod_Date")
        Gv1.Columns("Prod_Date").Width = 100
        'Gv1.Columns("Prod_Date").FormatString = "dd/MM/yyyy"
        Gv1.Columns("Prod_Date").IsVisible = True
        Gv1.Columns("Prod_Date").HeaderText = "Production Date"

        Gv1.Columns("Item Code").Width = 100
        Gv1.Columns("Item Code").IsVisible = True
        Gv1.Columns("Item Code").HeaderText = "Item Code"

        Gv1.Columns("Item_Description").Width = 170
        Gv1.Columns("Item_Description").IsVisible = True
        Gv1.Columns("Item_Description").HeaderText = "Item Description"





        Gv1.Columns("A-SHIFT").Width = 100
        Gv1.Columns("A-SHIFT").IsVisible = True
        Gv1.Columns("A-SHIFT").HeaderText = "A-Shift(Bag)"
        Gv1.Columns("A-SHIFT").TextAlignment = System.Drawing.ContentAlignment.MiddleRight

        Gv1.Columns("B-SHIFT").Width = 100
        Gv1.Columns("B-SHIFT").IsVisible = True
        Gv1.Columns("B-SHIFT").HeaderText = "B-Shift(Bag)"
        Gv1.Columns("B-SHIFT").TextAlignment = System.Drawing.ContentAlignment.MiddleRight

        Gv1.Columns("C-SHIFT").Width = 100
        Gv1.Columns("C-SHIFT").IsVisible = True
        Gv1.Columns("C-SHIFT").HeaderText = "C-Shift(Bag)"
        Gv1.Columns("C-SHIFT").TextAlignment = System.Drawing.ContentAlignment.MiddleRight

        Gv1.Columns("WHOLEDAY").Width = 100
        Gv1.Columns("WHOLEDAY").IsVisible = True
        Gv1.Columns("WHOLEDAY").HeaderText = "WholeDay(Bag)"
        Gv1.Columns("WHOLEDAY").TextAlignment = System.Drawing.ContentAlignment.MiddleRight

        Gv1.Columns("TOTAL BAG").Width = 100
        Gv1.Columns("TOTAL BAG").IsVisible = True
        Gv1.Columns("TOTAL BAG").HeaderText = "Total Bag"

        Gv1.Columns("Total MT").Width = 100
        Gv1.Columns("Total MT").IsVisible = True
        Gv1.Columns("Total MT").HeaderText = "Total Mt"

        Gv1.Columns("Add1").Width = 100
        Gv1.Columns("Add1").IsVisible = False
        Gv1.Columns("Add1").HeaderText = "Add1"

        Gv1.Columns("Location Description").Width = 100
        Gv1.Columns("Location Description").IsVisible = False
        Gv1.Columns("Location Description").HeaderText = "Location Description"

        Gv1.Columns("FromDate").Width = 100
        Gv1.Columns("FromDate").IsVisible = False
        Gv1.Columns("FromDate").HeaderText = "FromDate"

        Gv1.Columns("ToDate").Width = 100
        Gv1.Columns("ToDate").IsVisible = False
        Gv1.Columns("ToDate").HeaderText = "ToDate"



    End Sub

    Sub summary()

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item1 As New GridViewSummaryItem("A-SHIFT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("B-SHIFT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("C-SHIFT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("WHOLEDAY", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("TOTAL BAG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        Dim item6 As New GridViewSummaryItem("Total MT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

    End Sub

    'Sub view()

    '    If Gv1.Rows.Count > 0 Then
    '        Dim view As New ColumnGroupsViewDefinition()

    '        view.ColumnGroups.Add(New GridViewColumnGroup(" RSDF "))
    '        view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
    '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Location_Code").Name)
    '        view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Prod_Date").Name)

    '        Gv1.ViewDefinition = view()

    '    End If


    'End Sub

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

                If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.myCstr(lblBillToLocation.Text))
                End If
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Production Report", Gv1, arrHeader, Me.Text)
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
                '
                If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.myCstr(lblBillToLocation.Text))
                End If
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Production Report", Gv1, arrHeader, "Production Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim Status1 As String = ""
            Dim FG As String = ""
            Dim SFG As String = ""
            Dim FGSFG As String = ""
            Dim whr As String = ""
            If txtBillToLocation.Value IsNot Nothing AndAlso txtBillToLocation.Value.Count > 0 Then
                whr += " and LOCATION_CODE IN ('" + clsCommon.myCstr(txtBillToLocation.Value) + "') "
            Else
                'whr += " and TSPL_LOCATION_MASTER.Location_Type='Physical'  "
                'If clsCommon.myLen(arrLoc) > 0 Then
                'whr += "  and  LOCATION_CODE IN (" + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrValueMember) + ") "
                'End If
            End If
            If rdbPosted.IsChecked = True Then
                Status1 = "  TSPL_SPP_PRODUCTION_ENTRY.posted=1 "
            ElseIf rdbUnposted.IsChecked = True Then
                Status1 = "  TSPL_SPP_PRODUCTION_ENTRY.posted=0 "
            ElseIf rdbAll.IsChecked = True Then

            End If
            Dim Itemqry As String = ""
            Dim itemNames1 As String = Nothing
            If rdbFG.IsChecked = True Then
                FG = " and TSPL_Item_Master.FG_for_CF_RPT=1 "
                Itemqry = " Select Item_Code from TSPL_ITEM_MASTER WHERE FG_for_CF_RPT=1 "
            ElseIf rdbSFG.IsChecked = True Then
                SFG = " and TSPL_Item_Master.SFG_for_CF=1 "
                Itemqry = " Select Item_Code from TSPL_ITEM_MASTER WHERE SFG_for_CF=1 "
            ElseIf rdbfgsfg.IsChecked = True Then
                FGSFG = " and TSPL_Item_Master.FG_for_CF=1 "
                Itemqry = " Select Item_Code from TSPL_ITEM_MASTER WHERE FG_for_CF=1 "
            End If
            Dim dtitemName As DataTable = clsDBFuncationality.GetDataTable(Itemqry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    If i = 0 Then
                        itemNames1 += "'" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "' "
                    Else
                        itemNames1 += ", '" + clsCommon.myCstr(dtitemName.Rows(i)("Item_Code")) + "' "
                    End If
                Next
            End If
            Dim qry As String = ""
            qry = " Select * from (SELECT 'RAJASTHAN CO-OPERATIVE DAIRY FEDERATION LIMITED' as HeadName, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as ToDate,   LOCATION_CODE,Location_Desc as [Location Description],Add1,Add4,CONVERT(date, PROD_DATE,103) as PROD_DATE,[Item Code],ITEM_DESCRIPTION,isnull ([A-SHIFT], 0)  as [A-SHIFT],isnull ([B-SHIFT], 0) as [B-SHIFT], isnull ([C-SHIFT],0) as [C-SHIFT],
                          isnull ([WHOLEDAY],0) as [WHOLEDAY] , (  isnull ([A-SHIFT], 0)  + isnull ([B-SHIFT], 0) + isnull ([C-SHIFT],0) + isnull ([WHOLEDAY],0) ) AS [TOTAL BAG],   
                          (((  isnull ([A-SHIFT], 0)  + isnull ([B-SHIFT], 0) + isnull ([C-SHIFT],0) + isnull ([WHOLEDAY],0) )* 50)/1000) as [Total MT]
                          FROM
                                   (SELECT TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Add4,TSPL_LOCATION_MASTER.Location_Desc,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE as [Item Code],
                                    TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_DESCRIPTION, "
                            If Productionchk.IsChecked = True Then
                qry += " (FINAL_PRODUCTION_QTY/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as qty_bag "
            ElseIf RePrdntchk.IsChecked = True Then
                qry += " ((FINAL_PRODUCTION_QTY-Reprocess_Qty)/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as qty_bag "
            ElseIf Prdncreallchk.IsChecked = True Then
                qry += " (Reprocess_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as qty_bag  "
            End If

            qry += "               ,TSPL_SPP_PRODUCTION_ENTRY.Shift_Code as shiftcode FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL
								   left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE
                                   left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE and TSPL_ITEM_UOM_DETAIL.UOM_Code='bag'
								   left join TSPL_ITEM_UOM_DETAIL AS MT_ITEM_UOM_DETAIL on MT_ITEM_UOM_DETAIL.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE and MT_ITEM_UOM_DETAIL.UOM_Code='MT'
								   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE
                                   LEFT JOIN TSPL_Item_Master ON TSPL_Item_Master.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE "

            qry += " where " + Status1 + " " + FG + " " + SFG + " " + FGSFG + ""
            qry += " )Tab1
                                    PIVOT(SUM(qty_bag) FOR shiftcode IN ([A-SHIFT],[B-SHIFT],[C-SHIFT],[WHOLEDAY])) AS Tab2 )tmp
									where [Item Code] IN (" & itemNames1 & ")  and tmp.PROD_DATE >= '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and tmp.PROD_DATE<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'" + whr + "  order by PROD_DATE "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.PRODUCTION, dt, "rptProductionReport", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBillToLocation._MYValidating

        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        txtBillToLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtBillToLocation.Value + "'"))


    End Sub

    Private Sub ProductionReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
            lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_Location_Master where Location_Code='" + txtBillToLocation.Value + "' "))
        End If
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()

    End Sub

End Class