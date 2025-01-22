Imports common
Public Class rptUnionStock
    Inherits FrmMainTranScreen


    Private Sub txtBillToLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBillToLocation._MYValidating
        Dim qry As String = " Select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim whrcls As String = " Location_Type='Physical' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls += " and Location_Code in(" + objCommonVar.strCurrUserLocations + ") "
        End If
        txtBillToLocation.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", whrcls, txtBillToLocation.Value, "Code", isButtonClicked)
        lblBillToLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER Where Location_Code= '" + txtBillToLocation.Value + "'"))
    End Sub
    Private Sub TxtItem__My_Click(sender As Object, e As EventArgs) Handles TxtItem._My_Click
        Dim whr As String = ""
        If clsCommon.myLen(TxtItemType.Value) > 0 Then
            whr = "Where Item_Type ='" + TxtItemType.Value + "'"
        End If
        Dim qry As String = " Select Item_Code as ItemCode,Item_Desc as ItemDescription from TSPL_ITEM_MASTER " + whr + " order by Item_Code "
        TxtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "ItemCode", "ItemDescription", TxtItem.arrValueMember, TxtItem.arrDispalyMember)
    End Sub

    Private Sub TxtItemType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtItemType._MYValidating
        Try
            Dim qry As String = " SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER "
            TxtItemType.Value = clsCommon.ShowSelectForm("VendorMafnd", qry, "Code", "", TxtItemType.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtBillToLocation.Value = ""
        TxtItem.arrValueMember = Nothing
        lblBillToLocation.Text = ""
        TxtItemType.Value = ""
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        rbtnDetail.IsChecked = False
        rbtnSummary.IsChecked = True
    End Sub
    Private Sub rptUnionStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        reset()
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(False)
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        reset()
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
    Sub Print(ByVal Print As Boolean)
        Try
            Dim dt As DataTable = Nothing
            Dim Qry As String = ""
            Dim Whr As String = ""
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select location")
                Exit Sub
            End If
            If clsCommon.myLen(txtBillToLocation.Value) > 0 Then
                Whr = "Where Location_Code='" + txtBillToLocation.Value + "'"
            End If
            If clsCommon.myLen(TxtItemType.Value) > 0 Then
                Whr += "and Item_Type='" + TxtItemType.Value + "'"
            End If
            If TxtItem.arrValueMember IsNot Nothing AndAlso TxtItem.arrValueMember.Count > 0 Then
                Whr += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(TxtItem.arrValueMember) + ")"
            End If
            If rbtnSummary.IsChecked Then
                Qry = " select TSPL_ITEM_MASTER.Item_Code,max(TSPL_ITEM_MASTER.Item_Desc)Item_Desc,TSPL_ITEM_MASTER.Structure_Code,TSPL_ITEM_MASTER.Item_Type, case when  max(Report_UOM) = 1 then MAX(I.UOM_Code) else max(TSPL_INVENTORY_MOVEMENT.stock_uom)  end as UOM,
 CASE 
    WHEN MAX(Report_UOM) = 1 THEN 
        CONVERT(decimal(18,2), 
            SUM(
                ISNULL((Stock_Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.Conversion_Factor, 0) 
                * CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 END 
                * CASE WHEN InOut = 'I' THEN 1.00 ELSE -1.00 END
            )
        )
    ELSE 
        CONVERT(decimal(18,2), 
            SUM(
                Stock_Qty 
                * CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 END 
                * CASE WHEN InOut = 'I' THEN 1.00 ELSE -1.00 END
            )
        )
END AS [OPBal]
                 , CASE 
    WHEN MAX(Report_UOM) = 1 THEN 
        CONVERT(decimal(18,2), 
            SUM(
                ISNULL((Stock_Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.Conversion_Factor, 0) 
                * CASE 
                    WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'
                         AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                    THEN 1.00 
                    ELSE 0 
                  END 
                * CASE 
                    WHEN InOut = 'I' THEN 1.00 
                    ELSE 0 
                  END
            )
        )
    ELSE 
        SUM(
            Stock_Qty 
            * CASE 
                WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                     AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                THEN 1.00 
                ELSE 0 
              END 
            * CASE 
                WHEN InOut = 'I' THEN 1.00 
                ELSE 0 
              END
        )
END AS Received_Qty
                , CASE 
    WHEN MAX(Report_UOM) = 1 THEN 
        CONVERT(decimal(18,2), 
            SUM(
                ISNULL((Stock_Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.Conversion_Factor, 0) 
                * CASE 
                    WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                         AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                    THEN 1.00 
                    ELSE 0 
                  END 
                * CASE 
                    WHEN InOut = 'I' THEN 0 
                    ELSE 1.00 
                  END
            )
        )
    ELSE 
        SUM(
            Stock_Qty 
            * CASE 
                WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                     AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                THEN 1.00 
                ELSE 0 
              END 
            * CASE 
                WHEN InOut = 'I' THEN 0 
                ELSE 1.00 
              END
        )
END AS Issued_Qty
				, CASE 
    WHEN MAX(Report_UOM) = 1 THEN 
        CONVERT(decimal(18,2),

            SUM(
                ISNULL((Stock_Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.Conversion_Factor, 0) 
                * CASE 
                    WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 
                    ELSE 0 
                  END 
                * CASE 
                    WHEN InOut = 'I' THEN 1.00 
                    ELSE -1.00 
                  END
            )
        )
    ELSE 
        SUM(
            Stock_Qty 
            * CASE 
                WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 
                ELSE 0 
              END 
            * CASE 
                WHEN InOut = 'I' THEN 1.00 
                ELSE -1.00 
              END
        )
END AS [Balance_Qty]

				,max(TSPL_COMPANY_MASTER.Comp_Name)Comp_Name,max(TSPL_COMPANY_MASTER.City_Code)City_Code,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' as fromDate,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' as Todate
                 from TSPL_INVENTORY_MOVEMENT
                 left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code
                 left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                 left outer join  TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_INVENTORY_MOVEMENT.Stock_UOM
                 left join ( select item_code,uom_code,conversion_factor,UOM_Description from  TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) as  I ON TSPL_INVENTORY_MOVEMENT.Item_Code = I.item_code
                 left outer join TSPL_COMPANY_MASTER on 2=2 " + Whr + "
                Group by TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_MASTER.Structure_Code
				order by Structure_Code "
            ElseIf rbtnDetail.IsChecked Then
                Qry = " select Source_Doc_Date as Document_Date,TSPL_ITEM_MASTER.Item_Code,(TSPL_ITEM_MASTER.Item_Desc)Item_Desc,TSPL_ITEM_MASTER.Structure_Code,TSPL_ITEM_MASTER.Item_Type, case when  (Report_UOM) = 1 then (I.UOM_Code) else (TSPL_INVENTORY_MOVEMENT.stock_uom)  end as UOM,
 CASE 
    WHEN (Report_UOM) = 1 THEN 
        CONVERT(decimal(18,2), 
            (
                ISNULL((Stock_Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.Conversion_Factor, 0) 
                * CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 END 
                * CASE WHEN InOut = 'I' THEN 1.00 ELSE -1.00 END
            )
        )
    ELSE 
        CONVERT(decimal(18,2), 
            (
                Stock_Qty 
                * CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 ELSE 0 END 
                * CASE WHEN InOut = 'I' THEN 1.00 ELSE -1.00 END
            )
        )
END AS [OPBal]
                 , CASE 
    WHEN (Report_UOM) = 1 THEN 
        CONVERT(decimal(18,2), 
            (
                ISNULL((Stock_Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.Conversion_Factor, 0) 
                * CASE 
                    WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                         AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  
                    THEN 1.00 
                    ELSE 0 
                  END 
                * CASE 
                    WHEN InOut = 'I' THEN 1.00 
                    ELSE 0 
                  END
            )
        )
    ELSE 
        (
            Stock_Qty 
            * CASE 
                WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                     AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  
                THEN 1.00 
                ELSE 0 
              END 
            * CASE 
                WHEN InOut = 'I' THEN 1.00 
                ELSE 0 
              END
        )
END AS Received_Qty
                , CASE 
    WHEN (Report_UOM) = 1 THEN 
        CONVERT(decimal(18,2), 
            (
                ISNULL((Stock_Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.Conversion_Factor, 0) 
                * CASE 
                    WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                         AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  
                    THEN 1.00 
                    ELSE 0 
                  END 
                * CASE 
                    WHEN InOut = 'I' THEN 0 
                    ELSE 1.00 
                  END
            )
        )
    ELSE 
        (
            Stock_Qty 
            * CASE 
                WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                     AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  
                THEN 1.00 
                ELSE 0 
              END 
            * CASE 
                WHEN InOut = 'I' THEN 0 
                ELSE 1.00 
              END
        )
END AS Issued_Qty

				
				, CASE 
    WHEN (Report_UOM) = 1 THEN 
        CONVERT(decimal(18,2), 
            (
                ISNULL((Stock_Qty * ISNULL(TSPL_ITEM_UOM_DETAIL.Conversion_Factor, 1)) / I.Conversion_Factor, 0) 
                * CASE 
                    WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1.00 
                    ELSE 0 
                  END 
                * CASE 
                    WHEN InOut = 'I' THEN 1.00 
                    ELSE -1.00 
                  END
            )
        )
    ELSE 
        (
            Stock_Qty 
            * CASE 
                WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  THEN 1.00 
                ELSE 0 
              END 
            * CASE 
                WHEN InOut = 'I' THEN 1.00 
                ELSE -1.00 
              END
        )
END AS [Balance_Qty]

				,(TSPL_COMPANY_MASTER.Comp_Name)Comp_Name,(TSPL_COMPANY_MASTER.City_Code)City_Code,'" + txtFromDate.Value + "' as fromDate,'" + txtToDate.Value + "' as Todate
                 from TSPL_INVENTORY_MOVEMENT
                 left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code
                 left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code
                 left outer join  TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_INVENTORY_MOVEMENT.Stock_UOM
                 left join ( select item_code,uom_code,conversion_factor,UOM_Description from  TSPL_ITEM_UOM_DETAIL where Report_UOM = 1 ) as  I ON TSPL_INVENTORY_MOVEMENT.Item_Code = I.item_code
                 left outer join TSPL_COMPANY_MASTER on 2=2 " + Whr + "
                 order by Structure_Code ,TSPL_ITEM_MASTER.Sku_Seq "
            End If
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                If Print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If rbtnSummary.IsChecked Then
                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "UnionStockSummary", "")
                    ElseIf rbtnDetail.IsChecked Then
                        frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "UnionStockDetail", "")
                    End If
                    frmCRV = Nothing
                Else
                    Gv1.DataSource = Nothing
                    Gv1.Rows.Clear()
                    Gv1.Columns.Clear()
                    Gv1.GroupDescriptors.Clear()
                    Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    Gv1.MasterView.Refresh()
                    Gv1.DataSource = dt
                    For ii As Integer = 0 To Gv1.Columns.Count - 1
                        Gv1.Columns(ii).ReadOnly = True
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    Gv1.EnableFiltering = True
                    SetGridFormat()
                    Gv1.BestFitColumns()
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Sub SetGridFormat()
        Try
            Gv1.AutoExpandGroups = True
            Gv1.ShowGroupPanel = True
            Gv1.ShowRowHeaderColumn = False
            Gv1.AllowAddNewRow = False
            Gv1.AllowDeleteRow = False
            Gv1.EnableFiltering = True
            Gv1.ShowFilteringRow = True
            For ii As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(ii).ReadOnly = True
                Gv1.Columns(ii).BestFit()
            Next
            If rbtnDetail.IsChecked Then
                Gv1.Columns("Document_Date").HeaderText = "Date"
                Gv1.Columns("Document_Date").Width = 250
                Gv1.Columns("Document_Date").FormatString = "{0:n2}"
                Gv1.Columns("Document_Date").IsVisible = True
            End If
            Gv1.Columns("Item_Code").HeaderText = "Item Code"
            Gv1.Columns("Item_Code").Width = 250
            Gv1.Columns("Item_Code").FormatString = "{0:n2}"
            Gv1.Columns("Item_Code").IsVisible = True

            Gv1.Columns("Item_Desc").HeaderText = "Item Name"
            Gv1.Columns("Item_Desc").Width = 500
            Gv1.Columns("Item_Desc").IsVisible = True

            Gv1.Columns("Structure_Code").HeaderText = "Structure Code"
            Gv1.Columns("Structure_Code").Width = 500
            Gv1.Columns("Structure_Code").IsVisible = False

            'gv1.Columns("Item_Code").Name = "Item Code"
            'gv1.Columns("Item_Code").IsVisible = True

            Gv1.Columns("Item_Type").HeaderText = "Item Type"
            Gv1.Columns("Item_Type").Width = 250
            Gv1.Columns("Item_Type").IsVisible = False

            'gv1.Columns("VLC_Name").FormatString = "{0:n2}"
            Gv1.Columns("UOM").HeaderText = "UOM"
            Gv1.Columns("UOM").Width = 500

            Gv1.Columns("OPBal").HeaderText = "Opening Qty"
            Gv1.Columns("OPBal").Width = 250
            Gv1.Columns("OPBal").FormatString = "{0:n2}"
            Gv1.Columns("OPBal").IsVisible = True

            Gv1.Columns("Received_Qty").HeaderText = "Received Qty"
            Gv1.Columns("Received_Qty").Width = 250
            Gv1.Columns("Received_Qty").FormatString = "{0:n2}"
            Gv1.Columns("Received_Qty").IsVisible = True

            Gv1.Columns("Issued_Qty").HeaderText = "Issued Qty"
            Gv1.Columns("Issued_Qty").Width = 250
            Gv1.Columns("Issued_Qty").IsVisible = True

            Gv1.Columns("Balance_Qty").HeaderText = "Balance Qty"
            Gv1.Columns("Balance_Qty").Width = 250
            Gv1.Columns("Balance_Qty").IsVisible = True

            Gv1.Columns("Comp_Name").HeaderText = "Comp Name"
            Gv1.Columns("Comp_Name").Width = 250
            Gv1.Columns("Comp_Name").IsVisible = False

            Gv1.Columns("City_Code").HeaderText = "City Code"
            Gv1.Columns("City_Code").Width = 250
            Gv1.Columns("City_Code").IsVisible = False

            Gv1.Columns("fromDate").HeaderText = "From Date"
            Gv1.Columns("fromDate").Width = 250
            Gv1.Columns("fromDate").IsVisible = False

            Gv1.Columns("Todate").HeaderText = "To Date"
            Gv1.Columns("Todate").Width = 250
            Gv1.Columns("Todate").IsVisible = False

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim item1 As New GridViewSummaryItem("OPBal", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("Received_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Issued_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Balance_Qty", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Print(True)
    End Sub
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.UnionStockReport & "'"))
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Name : " + strHeading)
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text,, arrHeader)
                transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, True)
            Else
                clsCommon.MyExportToPDF(strHeading, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub
End Class