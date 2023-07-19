Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class rptSalesReport
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
        Load_Sales_Report()
    End Sub

    Private Sub Load_Sales_Report()

        Dim qry As String = ""
        Dim dt As New DataTable()
        Try
            Dim whr As String = ""
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                whr += " and TSPL_SD_SALE_INVOICE_DETAIL.Location In  (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ") "
            Else
                'whr += " and TSPL_LOCATION_MASTER.Location_Type='Physical'  "
                'If clsCommon.myLen(arrLoc) > 0 Then
                'whr += "  and  LOCATION_CODE IN (" + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrValueMember) + ") "
                'End If
            End If
            qry = "   Select * from (SELECT  Location,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' As FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "'  As ToDate,Location_Desc as [Location Description],Add1,Document_Date,ISNULL ([MILKUNION], 0) as [MILKUNION] ,ISNULL ([GOSHALA], 0) as [GOSHALA] ,ISNULL ([DCS], 0) as [DCS],ISNULL ([GOVT], 0) as [GOVT],ISNULL ([KVSS], 0) as [KVSS], ISNULL ([OTHER], 0) as [OTHER],
  (ISNULL ([MILKUNION], 0) + ISNULL ([GOSHALA], 0) + ISNULL ([DCS], 0) + ISNULL ([GOVT], 0) + ISNULL ([KVSS], 0) + ISNULL ([OTHER], 0)) as [Total Sale]
  FROM
                                   (SELECT TSPL_SD_SALE_INVOICE_DETAIL.Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty) as Quantity,price_CodeNon								   
								   FROM TSPL_SD_SALE_INVOICE_DETAIL
								     left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
						      		 left outer join TSPL_SD_SALE_INVOICE_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_DETAIL.Location
	                                 WHERE  TSPL_SD_SALE_INVOICE_HEAD.Document_Date >= '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'  and  TSPL_SD_SALE_INVOICE_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(txtToDate.Value) + "'" + whr + "

									 group by TSPL_SD_SALE_INVOICE_HEAD.Document_Date,price_CodeNon,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,Location)Tab1
                                    PIVOT (SUM(Quantity) FOR price_CodeNon IN ([MILKUNION],[GOSHALA],[DCS],[GOVT],[KVSS],[OTHER]))AS Tab2)tmp  "

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
                clsCommon.MyMessageBoxShow("No data found to display.", "Sales Report")
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

        Gv1.Columns("Location Description").Width = 100
        Gv1.Columns("Location Description").IsVisible = False
        Gv1.Columns("Location Description").HeaderText = "Location Description"

        Gv1.Columns("FromDate").Width = 100
        Gv1.Columns("FromDate").IsVisible = False
        Gv1.Columns("FromDate").HeaderText = "FromDate"

        Gv1.Columns("ToDate").Width = 100
        Gv1.Columns("ToDate").IsVisible = False
        Gv1.Columns("ToDate").HeaderText = "ToDate"

        Gv1.Columns("Add1").Width = 100
        Gv1.Columns("Add1").IsVisible = False
        Gv1.Columns("Add1").HeaderText = "Add1"

        Gv1.Columns("Location").Width = 100
        Gv1.Columns("Location").IsVisible = True
        Gv1.Columns("Location").HeaderText = "Location"

        Gv1.Columns("Document_Date").Width = 100
        Gv1.Columns("Document_Date").IsVisible = True
        Gv1.Columns("Document_Date").HeaderText = "Document Date"

        Gv1.Columns("MILKUNION").Width = 100
        Gv1.Columns("MILKUNION").IsVisible = True
        Gv1.Columns("MILKUNION").HeaderText = "MILK UNION"

        Gv1.Columns("GOSHALA").Width = 100
        Gv1.Columns("GOSHALA").IsVisible = True
        Gv1.Columns("GOSHALA").HeaderText = "GOSHALA"

        Gv1.Columns("DCS").Width = 100
        Gv1.Columns("DCS").IsVisible = True
        Gv1.Columns("DCS").HeaderText = "DCS"

        Gv1.Columns("GOVT").Width = 100
        Gv1.Columns("GOVT").IsVisible = True
        Gv1.Columns("GOVT").HeaderText = "GOVT"

        Gv1.Columns("KVSS").Width = 100
        Gv1.Columns("KVSS").IsVisible = True
        Gv1.Columns("KVSS").HeaderText = "KVSS"

        Gv1.Columns("OTHER").Width = 100
        Gv1.Columns("OTHER").IsVisible = True
        Gv1.Columns("OTHER").HeaderText = "OTHER"

        Gv1.Columns("Total Sale").Width = 100
        Gv1.Columns("Total Sale").IsVisible = True
        Gv1.Columns("Total Sale").HeaderText = "Total Sale"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item1 As New GridViewSummaryItem("Total Sale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("OTHER", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("KVSS", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("GOVT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Dim item5 As New GridViewSummaryItem("DCS", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)

        Dim item6 As New GridViewSummaryItem("GOSHALA", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        Dim item7 As New GridViewSummaryItem("MILKUNION", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

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
                clsCommon.MyExportToExcelGrid("Sales Report", Gv1, arrHeader, Me.Text)
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
                clsCommon.MyExportToPDF("Sales Report", Gv1, arrHeader, "Sales Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
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
                whr += " and TSPL_SD_SALE_INVOICE_DETAIL.Location In   (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ") "
            Else
                'whr += " and TSPL_LOCATION_MASTER.Location_Type='Physical'  "
                'If clsCommon.myLen(arrLoc) > 0 Then
                'whr += "  and  LOCATION_CODE IN (" + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrValueMember) + ") "
                'End If
            End If

            Dim qry As String = "  Select * from (SELECT  Location,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' As FromDate, '" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "'  As ToDate,Location_Desc as [Location Description],Add1,Document_Date,ISNULL ([MILKUNION], 0) as [MILKUNION] ,ISNULL ([GOSHALA], 0) as [GOSHALA] ,ISNULL ([DCS], 0) as [DCS],ISNULL ([GOVT], 0) as [GOVT],ISNULL ([KVSS], 0) as [KVSS], ISNULL ([OTHER], 0) as [OTHER],
  (ISNULL ([MILKUNION], 0) + ISNULL ([GOSHALA], 0) + ISNULL ([DCS], 0) + ISNULL ([GOVT], 0) + ISNULL ([KVSS], 0) + ISNULL ([OTHER], 0)) as [Total Sale]
  FROM
                                   (SELECT TSPL_SD_SALE_INVOICE_DETAIL.Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,sum(TSPL_ITEM_UOM_DETAIL.Conversion_Factor*TSPL_SD_SALE_INVOICE_DETAIL.Qty) as Quantity,price_CodeNon								   
								   FROM TSPL_SD_SALE_INVOICE_DETAIL
								     left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE
                                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                                     LEFT OUTER JOIN TSPL_PRICE_COMPONENT_MAPPING ON TSPL_PRICE_COMPONENT_MAPPING.Price_Code=TSPL_CUSTOMER_MASTER.price_CodeNon
                                     left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                                     LEFT JOIN  TSPL_ITEM_UOM_DETAIL ON  TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                                     AND TSPL_ITEM_UOM_DETAIL.UOM_Code= TSPL_SD_SALE_INVOICE_DETAIL.Unit_code
						      		 left outer join TSPL_SD_SALE_INVOICE_HEAD as LO_SD_SALE_INVOICE_HEAD on LO_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE                            
									 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_DETAIL.Location
	                                 WHERE  TSPL_SD_SALE_INVOICE_HEAD.Document_Date >= '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'  and  TSPL_SD_SALE_INVOICE_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(txtToDate.Value) + "'" + whr + "

									 group by TSPL_SD_SALE_INVOICE_HEAD.Document_Date,price_CodeNon,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Add1,Location)Tab1
                                    PIVOT (SUM(Quantity) FOR price_CodeNon IN ([MILKUNION],[GOSHALA],[DCS],[GOVT],[KVSS],[OTHER]))AS Tab2)tmp  "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "rptSalesReport", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class