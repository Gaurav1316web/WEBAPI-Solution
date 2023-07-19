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
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        TxtMultiLocation.arrValueMember = Nothing
        Gv1.DataSource = Nothing
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
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
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                whr += " and LOCATION_CODE IN (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ") "
            Else
                'whr += " and TSPL_LOCATION_MASTER.Location_Type='Physical'  "
                'If clsCommon.myLen(arrLoc) > 0 Then
                'whr += "  and  LOCATION_CODE IN (" + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrValueMember) + ") "
                'End If
            End If
            qry = " Select * from (SELECT  '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as ToDate,   LOCATION_CODE,Location_Desc as [Location Description],Add1,CONVERT(date, PROD_DATE,103) as PROD_DATE,ITEM_DESCRIPTION,[Item Code],isnull ([A-SHIFT], 0)  as [A-SHIFT],isnull ([B-SHIFT], 0) as [B-SHIFT], isnull ([C-SHIFT],0) as [C-SHIFT],
                          isnull ([WHOLEDAY],0) as [WHOLEDAY] , (  isnull ([A-SHIFT], 0)  + isnull ([B-SHIFT], 0) + isnull ([C-SHIFT],0) + isnull ([WHOLEDAY],0) ) AS [TOTAL BAG],   
                          ((  isnull ([A-SHIFT], 0)  + isnull ([B-SHIFT], 0) + isnull ([C-SHIFT],0) + isnull ([WHOLEDAY],0) * 50)/1000) as [Total MT]
                          FROM
                                   (SELECT TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Location_Desc,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE as [Item Code],TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_DESCRIPTION,(FINAL_PRODUCTION_QTY/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as qty_bag,TSPL_SPP_PRODUCTION_ENTRY.Shift_Code as shiftcode FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL
								   left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE
                                   left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE and TSPL_ITEM_UOM_DETAIL.UOM_Code='bag'
								   left join TSPL_ITEM_UOM_DETAIL AS MT_ITEM_UOM_DETAIL on MT_ITEM_UOM_DETAIL.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE and MT_ITEM_UOM_DETAIL.UOM_Code='MT'
								   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE
                                    )Tab1
                                    PIVOT(SUM(qty_bag) FOR shiftcode IN ([A-SHIFT],[B-SHIFT],[C-SHIFT],[WHOLEDAY])) AS Tab2 )tmp
									where [Item Code] IN ('FG0001','FG0002','FG0003')  and tmp.PROD_DATE >= '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and tmp.PROD_DATE<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'" + whr
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

        Gv1.Columns("Prod_Date").Width = 100
        Gv1.Columns("Prod_Date").IsVisible = True
        Gv1.Columns("Prod_Date").HeaderText = "Prod Date"

        Gv1.Columns("Item_Description").Width = 100
        Gv1.Columns("Item_Description").IsVisible = True
        Gv1.Columns("Item_Description").HeaderText = "Item Description"

        Gv1.Columns("Item Code").Width = 100
        Gv1.Columns("Item Code").IsVisible = True
        Gv1.Columns("Item Code").HeaderText = "Item Code"

        Gv1.Columns("A-Shift").Width = 100
        Gv1.Columns("A-Shift").IsVisible = True
        Gv1.Columns("A-Shift").HeaderText = "A-Shift"

        Gv1.Columns("B-Shift").Width = 100
        Gv1.Columns("B-Shift").IsVisible = True
        Gv1.Columns("B-Shift").HeaderText = "B-Shift"

        Gv1.Columns("C-Shift").Width = 100
        Gv1.Columns("C-Shift").IsVisible = True
        Gv1.Columns("C-Shift").HeaderText = "C-Shift"

        Gv1.Columns("WholeDay").Width = 100
        Gv1.Columns("WholeDay").IsVisible = True
        Gv1.Columns("WholeDay").HeaderText = "WholeDay"

        Gv1.Columns("Total Bag").Width = 100
        Gv1.Columns("Total Bag").IsVisible = True
        Gv1.Columns("Total Bag").HeaderText = "Total Bag"

        Gv1.Columns("Total Mt").Width = 100
        Gv1.Columns("Total Mt").IsVisible = True
        Gv1.Columns("Total Mt").HeaderText = "Total Mt"

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

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


    End Sub

    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click

        Dim WhrCls As String = " And Location_Type ='Physical'  "
        If clsCommon.myLen(arrLoc) > 0 Then
            WhrCls += "  and  Location_Code in (" + arrLoc + ")"
        End If

        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER where 2=2 " + WhrCls + "  "

        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Pro", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)


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

                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
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
                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
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

            Dim whr As String = ""
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                whr += " and LOCATION_CODE IN (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ") "
            Else
                'whr += " and TSPL_LOCATION_MASTER.Location_Type='Physical'  "
                'If clsCommon.myLen(arrLoc) > 0 Then
                'whr += "  and  LOCATION_CODE IN (" + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrValueMember) + ") "
                'End If
            End If

            Dim qry As String = " Select * from (SELECT  '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as ToDate,   LOCATION_CODE,Location_Desc as [Location Description],Add1,CONVERT(date, PROD_DATE,103) as PROD_DATE,ITEM_DESCRIPTION,[Item Code],isnull ([A-SHIFT], 0)  as [A-SHIFT],isnull ([B-SHIFT], 0) as [B-SHIFT], isnull ([C-SHIFT],0) as [C-SHIFT],
                          isnull ([WHOLEDAY],0) as [WHOLEDAY] , (  isnull ([A-SHIFT], 0)  + isnull ([B-SHIFT], 0) + isnull ([C-SHIFT],0) + isnull ([WHOLEDAY],0) ) AS [TOTAL BAG],   
                          ((  isnull ([A-SHIFT], 0)  + isnull ([B-SHIFT], 0) + isnull ([C-SHIFT],0) + isnull ([WHOLEDAY],0) * 50)/1000) as [Total MT]
                          FROM
                                   (SELECT TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE,TSPL_LOCATION_MASTER.Add1,TSPL_LOCATION_MASTER.Location_Desc,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE as [Item Code],TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_DESCRIPTION,(FINAL_PRODUCTION_QTY/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as qty_bag,TSPL_SPP_PRODUCTION_ENTRY.Shift_Code as shiftcode FROM TSPL_SPP_PRODUCTION_ENTRY_DETAIL
								   left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE=TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE
                                   left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE and TSPL_ITEM_UOM_DETAIL.UOM_Code='bag'
								   left join TSPL_ITEM_UOM_DETAIL AS MT_ITEM_UOM_DETAIL on MT_ITEM_UOM_DETAIL.Item_Code=TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE and MT_ITEM_UOM_DETAIL.UOM_Code='MT'
								   left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_SPP_PRODUCTION_ENTRY_DETAIL.LOCATION_CODE
                                    )Tab1
                                    PIVOT(SUM(qty_bag) FOR shiftcode IN ([A-SHIFT],[B-SHIFT],[C-SHIFT],[WHOLEDAY])) AS Tab2 )tmp
									where [Item Code] IN ('FG0001','FG0002','FG0003')  and tmp.PROD_DATE >= '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and tmp.PROD_DATE<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'" + whr

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "rptProductionReport", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class