'' Developped by Panch Raj on date:15-02-2018
Imports common
Imports System.IO
Imports Microsoft.Office.Interop
Public Class rptPlantWiseAvailableStockQtyReport
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dtLocation As DataTable = New DataTable()
#End Region
    Private Sub rptPlantWiseAvailableStockQtyReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        reset()
        txtDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub rptPlantWiseAvailableStockQtyReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            gv1.EnableFiltering = True
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            LoadData(False)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadData(ByVal isPrint As Boolean)
        Try
            Dim Location As String = Nothing
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                If txtLocation.arrValueMember.Count > 1 Then
                    Location = "'" + clsDBFuncationality.getSingleValue(" select Location_Code from tspl_location_master where   IsMainPlant=1 ") + "'"
                Else
                    Location = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                End If
            End If

            Dim qry As String = " select xx.Location_Code, xx.Item_Desc,MT.UOM_Code AS unit,isnull((XX.Stock_Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(mt.Conversion_Factor),0) As STOCK_QTY,isnull((XX.Stock_Qty * isnull((TSPL_ITEM_UOM_DETAIL.Conversion_Factor),1)) /(mt.Conversion_Factor),0) As Total_Stock_Qty,cast(xx.QTY_FOR_DAYS as integer)  as QTY_FOR_DAYS,cast(xx.QTY_FOR_DAYS as integer) as Total_QTY_FOR_Days,Location_Code as Location_Code_Qty , Location_Code + ' Days' as Location_Code_Days from (
                SELECT RM_STOCK_DAYS.Location_Code, RM_STOCK_DAYS.ITEM_CODE,max(RM_STOCK_DAYS.Item_Desc) as Item_Desc,max(RM_STOCK_DAYS.UOM)  AS 'UOM',SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))  AS 'STOCK_QTY',SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) AS REQ_STOCK,SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) AS MIN_LEVEL,
	            CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.REQ_STOCK,0)) ELSE CASE WHEN SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0))<>0 THEN SUM(ISNULL(RM_STOCK_DAYS.STOCK_QTY,0))/SUM(ISNULL(RM_STOCK_DAYS.MIN_LEVEL,0)) ELSE 0 END END AS 'QTY_FOR_DAYS'  FROM  (
	            SELECT RM_STOCK.ITEM_CODE,RM_STOCK.Location_Code,max(RM_STOCK.Item_Desc) as Item_Desc,MAX(RM_STOCK.UOM) AS 'UOM',SUM(ISNULL(RM_STOCK.IN_STOCK_QTY,0))-SUM(ISNULL(RM_STOCK.OUT_STOCK_QTY,0)) AS 'STOCK_QTY',0 AS 'REQ_STOCK', 0 AS 'MIN_LEVEL' FROM (
	            SELECT TSPL_INVENTORY_MOVEMENT.Item_Code AS 'ITEM_CODE',TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',TSPL_ITEM_MASTER.Unit_Code AS 'UOM', CASE WHEN INOUT='I' THEN STOCK_QTY END AS 'IN_STOCK_QTY',CASE WHEN INOUT='O' THEN STOCK_QTY END AS 'OUT_STOCK_QTY' ,TSPL_INVENTORY_MOVEMENT.Location_Code FROM TSPL_INVENTORY_MOVEMENT
	            LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code WHERE 2=2  AND TSPL_ITEM_MASTER.Structure_Code IN ('RM','PM')  and Punching_Date <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'
				) RM_STOCK  GROUP BY RM_STOCK.Location_Code, RM_STOCK.Item_Code
	            UNION ALL 
	            SELECT  TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE,TSPL_MF_BOM_HEAD.Location_Code,max(TSPL_ITEM_MASTER.Short_Description) as Item_Desc,max(TSPL_ITEM_MASTER.Unit_Code) as 'UOM',0 AS 'STOCK_QTY',AVG(CASE WHEN TSPL_MF_BOM_DETAIL.Percentage>0 THEN  (TSPL_MF_BOM_DETAIL.Percentage*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/100 ELSE CASE WHEN TSPL_MF_BOM_DETAIL.CONSM_QUANTITY>0 THEN ((TSPL_MF_BOM_DETAIL.CONSM_QUANTITY*TSPL_LOCATION_MASTER.Silo_Capacity*1000)/TSPL_MF_BOM_HEAD.PROD_QUANTITY) ELSE 0 END END)  AS 'REQ_STOCK',
	            0 AS 'MIN_LEVEL' FROM TSPL_MF_BOM_HEAD  LEFT OUTER JOIN TSPL_MF_BOM_DETAIL ON TSPL_MF_BOM_DETAIL.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_MF_BOM_HEAD.LOCATION_CODE INNER join (select PROD_ITEM_CODE,MAX(BOM_CODE) AS 'BOM_CODE',MAX(REVISION_NO) AS 'REVISION_NO' from TSPL_MF_BOM_HEAD WHERE 2=2   GROUP BY PROD_ITEM_CODE ) BOM_LATEST ON BOM_LATEST.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE AND BOM_LATEST.REVISION_NO=TSPL_MF_BOM_HEAD.REVISION_NO
            	WHERE TSPL_ITEM_MASTER.Structure_Code IN ('RM','PM') and BOM_DATE <= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' GROUP BY  TSPL_MF_BOM_HEAD.LOCATION_CODE, TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE
                UNION ALL
                select TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code,TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code ,TSPL_ITEM_MASTER.Short_Description AS 'ITEM_DESC',TSPL_ITEM_MASTER.Unit_Code AS 'UOM',0 AS 'STOCK_QTY', 0 AS 'REQ_STOCK',TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level AS 'MIN_LEVEL' from TSPL_ITEM_REORDER_LEVEL_NEW  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code where TSPL_ITEM_MASTER.Structure_Code IN ('RM','PM') and Apply='Y' ) RM_STOCK_DAYS GROUP BY  RM_STOCK_DAYS.Location_Code, RM_STOCK_DAYS.ITEM_CODE ) xx 
                left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ITEM_CODE and TSPL_ITEM_UOM_DETAIL.UOM_Code = xx.UOM left join ( select item_code,uom_code,conversion_factor,UOM_Description from  TSPL_ITEM_UOM_DETAIL where UOM_Code = 'MT' ) as  MT ON xx.Item_Code = MT.item_code  WHERE XX.ITEM_CODE NOT IN ('PM0001','PM0002') and xx.STOCK_QTY>0 "

            If txtItem.arrValueMember IsNot Nothing Then
                qry += " and xx.ITEM_CODE in  (" & clsCommon.GetMulcallString(txtItem.arrValueMember) & ") "
            End If
            If txtLocation.arrValueMember IsNot Nothing Then
                qry += " and xx.Location_Code in  (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ") "
            End If

            qry += " ) XXX "

            dtLocation = clsDBFuncationality.GetDataTable(" select max(Location_Code_Qty)Location_Code_Qty , max(Location_Code_Days)Location_Code_Days from (" & qry & " group by Location_Code ")
            Dim LocationCodesQty As String = Nothing
            Dim strLocationQty As String = Nothing
            Dim LocationCodesDays As String = Nothing
            Dim strLocationPrint As String = Nothing
            If dtLocation IsNot Nothing AndAlso dtLocation.Rows.Count > 0 Then
                For i As Integer = 0 To dtLocation.Rows.Count - 1
                    If i = 0 Then
                        strLocationQty += "Sum(IsNull([" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Qty")) + "],0)) As [" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Qty")) + "],Sum(IsNull([" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Days")) + "],0)) As [" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Days")) + "]"
                        LocationCodesQty += "[" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Qty")) + "] "
                        LocationCodesDays += "[" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Days")) + "] "
                        strLocationPrint += " '" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Qty")) + "' as  Location_Code_" & clsCommon.myCstr(i + 1) & "_Name, SUM(case when Location_Code = '" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Qty")) + "' then STOCK_QTY else 0 end) as Location_" & clsCommon.myCstr(i + 1) & "_Qty, SUM(case when Location_Code = '" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Qty")) + "' then QTY_FOR_DAYS  else 0 end) as Location_" & clsCommon.myCstr(i + 1) & "_Days"
                    Else
                        strLocationQty += ", Sum(IsNull([" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Qty")) + "],0)) As [" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Qty")) & "],Sum(IsNull([" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Days")) + "],0)) As [" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Days")) + "]"
                        LocationCodesQty += ", [" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Qty")) + "] "
                        LocationCodesDays += ", [" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Days")) + "] "
                        strLocationPrint += " ,'" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Qty")) + "' as Location_Code_" & clsCommon.myCstr(i + 1) & "_Name,SUM(case when Location_Code = '" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Qty")) + "' then STOCK_QTY else 0 end) as Location_" & clsCommon.myCstr(i + 1) & "_Qty, SUM(case when Location_Code = '" + clsCommon.myCstr(dtLocation.Rows(i)("Location_Code_Qty")) + "' then QTY_FOR_DAYS  else 0 end) as Location_" & clsCommon.myCstr(i + 1) & "_Days"
                    End If
                Next
            Else
                gv1.DataSource = Nothing
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.SummaryRowsBottom.Clear()
                Exit Sub
            End If
            Dim dt As DataTable = New DataTable()
            Dim dtPrint As DataTable = New DataTable()
            If isPrint Then
                dtPrint = clsDBFuncationality.GetDataTable("select Comp_Name,'" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' as Date, xxxxx.* from ( SELECT Item_Desc,sum(Total_Stock_Qty)Total_Stock_Qty," & strLocationPrint & " ,sum(Total_QTY_FOR_Days)Total_QTY_FOR_Days  FROM (  " & qry & " Group by Item_Desc ) xxxxx left join TSPL_COMPANY_MASTER on 1= 1 ORDER BY  ITEM_DESC  ")
            End If
            qry += " PIVOT(SUM(STOCK_QTY) For Location_Code_Qty In ( " & LocationCodesQty & ")) As  PIVOTQTY  PIVOT(SUM(QTY_FOR_DAYS) For Location_Code_Days In ( " & LocationCodesDays & ")) As  PIVOTDays  Group by Item_Desc ORDER BY  ITEM_DESC "
            dt = clsDBFuncationality.GetDataTable("SELECT Item_Desc,sum(Total_Stock_Qty)Total_Stock_Qty," & strLocationQty & " ,sum(Total_QTY_FOR_Days)Total_QTY_FOR_Days  FROM (  " & qry & " ")

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.BestFitColumns()
                gv1.EnableFiltering = True
                gv1.AllowAddNewRow = False
                gv1.AllowDeleteRow = False
                RadPageView1.SelectedPage = RadPageViewPage2
                View()
                SetGridFormation()
                ReStoreGridLayout()
                EnableDisableCtrl(False)
                If isPrint Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(Form_ID, CrystalReportFolder.PurchaseOrder, dtPrint, "rptPlantWiseStockAvailable", "Plant Wise Stock Available")
                    frmCRV = Nothing
                End If
            Else
                If isPrint Then
                    clsCommon.MyMessageBoxShow(Me, "No Data found to print", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data found to Display", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            btnGo.Enabled = True
        End Try
    End Sub
    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).Width = 120
            If gv1.Columns(ii).Name.Contains("Days") Then
                gv1.Columns(ii).FormatString = ""
                gv1.Columns(ii).HeaderText = "Days"
            Else
                gv1.Columns(ii).HeaderText = "Stock Qty"
                gv1.Columns(ii).FormatString = "{0:n2}"
            End If
        Next
        gv1.ShowGroupPanel = False

        gv1.Columns("Item_Desc").HeaderText = "Item"
        gv1.Columns("Total_Stock_Qty").HeaderText = "Total Stock Available"
        gv1.Columns("Total_QTY_FOR_Days").HeaderText = "Total Available Stock" + Environment.NewLine + "In Days"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        For ii As Integer = 1 To gv1.Columns.Count - 1
            summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, IIf(gv1.Columns(ii).Name.Contains("Days"), "{0:F0}", "{0:F2}"), GridAggregateFunction.Sum))
        Next

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

    End Sub
    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Item_Desc").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Total_Stock_Qty").Name)

                For ii As Integer = 0 To dtLocation.Rows.Count - 1
                    view.ColumnGroups.Add(New GridViewColumnGroup(dtLocation.Rows(ii)("Location_Code_Qty")))
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                    For col As Integer = 2 To gv1.Columns.Count - 1
                        If clsCommon.CompairString(gv1.Columns(col).HeaderText, dtLocation.Rows(ii)("Location_Code_Qty")) = CompairStringResult.Equal OrElse clsCommon.CompairString(gv1.Columns(col).HeaderText, dtLocation.Rows(ii)("Location_Code_Days")) = CompairStringResult.Equal Then
                            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns(col).Name)
                        End If
                    Next
                Next
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(gv1.Columns("Total_QTY_FOR_Days").Name)
                gv1.ViewDefinition = view

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = PageSetupReport_ID
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            common.clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_code='" & clsUserMgtCode.rptPlantWiseAvailableStockQtyReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtDate.Value))

                If txtLocation.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Location : " & clsCommon.GetMulcallString(txtLocation.arrValueMember) & "  Location Name :" & clsCommon.GetMulcallString(txtLocation.arrDispalyMember) & "")
                End If

                If txtItem.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Item : " & clsCommon.GetMulcallString(txtItem.arrValueMember) & "   Item Name :" & clsCommon.GetMulcallString(txtItem.arrDispalyMember) & "")
                End If
                clsCommon.MyExportToExcel(Me.Text, gv1, arrHeader, Me.Text)
                clsCommon.MyMessageBoxShow(Me, "Export Successfully", Me.Text)
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
                arrHeader.Add("Date : " & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                If txtLocation.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Location : " & clsCommon.GetMulcallString(txtLocation.arrValueMember) & "   Location Name :" & clsCommon.GetMulcallString(txtLocation.arrDispalyMember) & "")
                End If

                If txtItem.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Item : " & clsCommon.GetMulcallString(txtItem.arrValueMember) & "   Item Name :" & clsCommon.GetMulcallString(txtItem.arrDispalyMember) & "")
                End If
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub EnableDisableCtrl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub
    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub rmsaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = PageSetupReport_ID
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Try
            Dim qry As String = " select Location_Code as Code , Location_Desc AS Name from tspl_location_master "
            txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocFinder", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = " SELECT Item_Code AS Code,Item_Desc as [Item Description],Short_Description as [Short Description] FROM TSPL_ITEM_MASTER WHERE ITEM_TYPE = 'R' "
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemFnd", qry, "Code", "Short Description", txtItem.arrValueMember, txtItem.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Sub reset()
        EnableDisableCtrl(True)
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        LoadData(True)
    End Sub
End Class
