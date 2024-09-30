Imports common
Imports System.IO
Public Class rptProductionReport
    Inherits FrmMainTranScreen
#Region "Variables"
    Const ReportID As String = "ProductionReport"

#End Region
    Private Sub rptProductionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
    End Sub

    Sub funreset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        Dim ToDate As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        Dim FromDate As DateTime = ToDate.AddDays(-30)
        txtFromDate.Value = FromDate
        txtToDate.Value = ToDate
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Dim qry As String = ""
            Dim Baseqry As String = ""
            Dim dtFinal As DataTable = New DataTable()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found", Me.Text)
                gv1.DataSource = Nothing
                Exit Sub
            End If
            Dim arrUnion As New ArrayList()
            arrUnion.Add(objCommonVar.CurrComp_Code1)
            If objCommonVar.RCDFCFP Then
                dt = clsMilkUnion.UnionDBName()
            Else
                dt = clsMilkUnion.UnionDBName1(arrUnion)
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    Baseqry += " select SNo , [Union Name], sum(LTR_QTY)LTR_QTY , sum(KG_QTY)KG_QTY,sum(Fat_KG)Fat_KG,sum(SNF_KG)SNF_KG  from ( select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name], "
                    Baseqry += " convert(decimal(18,2),(isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_INVENTORY_MOVEMENT.Qty,0) * isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR]) as LTR_QTY,
                    convert(decimal(18,2),(isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_INVENTORY_MOVEMENT.Qty,0) * isnull([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) /I.[KG]) as KG_QTY , 
                     [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_INVENTORY_MOVEMENT.Fat_KG,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_INVENTORY_MOVEMENT.SNF_KG ,  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_INVENTORY_MOVEMENT.Qty
                    FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_INVENTORY_MOVEMENT  
                    left JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = [ASHOK].[dbo].TSPL_INVENTORY_MOVEMENT.Item_Code
                   LEFT JOIN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_INVENTORY_MOVEMENT.Item_Code and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL.UOM_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_INVENTORY_MOVEMENT.Stock_UOM
                      LEFT JOIN (  SELECT * FROM ( select item_code,uom_code,conversion_factor from  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_INVENTORY_MOVEMENT.Item_Code = I.item_code 
                  where Trans_Type in ('DRY-PRO-UPL' , 'PROD_ENTRY' , 'PRO-SFT-MGM') and InOut = 'I' and TSPL_ITEM_MASTER.Product_Type = 'MP'  and convert(date,Source_Doc_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,Source_Doc_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' ) xx group by  SNo , [Union Name] "
                    dt = clsDBFuncationality.GetDataTable(Baseqry)
                    dtFinal.Merge(dt)
                Next
            End If
            gv1.DataSource = Not
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dtFinal.Rows.Count > 0 Then
                gv1.DataSource = dtFinal
                gv1.BestFitColumns()
                SetGridFormatgv1(gv1)
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

    Sub SetGridFormatgv1(ByRef Gv1 As RadGridView)
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Gv1.Columns("LTR_QTY").HeaderText = "Qty Ltr"
        Gv1.Columns("KG_QTY").HeaderText = "Qty Kg"
        Gv1.Columns("Fat_KG").HeaderText = "Fat Kg"
        Gv1.Columns("SNF_KG").HeaderText = "Snf Kg"
        Gv1.Columns("SNo").IsVisible = False

        Gv1.AutoSizeRows = False
        Gv1.BestFitColumns()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        print(EnumExportTo.PDF)
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptProductionReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate.Value) + "  To " + clsCommon.myCDate(txtToDate.Value))
                arrHeader.Add("User : " + objCommonVar.CurrentUser)
                If exporter = EnumExportTo.Excel Then
                    transportSql.QuickExportToExcel(gv1, Me.Text, "", , arrHeader)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_DoubleClick(sender As Object, e As EventArgs) Handles gv1.DoubleClick

        If clsCommon.CompairString(gv1.CurrentColumn.Name, "Union Name") = CompairStringResult.Equal Then
            UnionWiseProductionDetail(gv1.CurrentCell.Value)
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Sub UnionWiseProductionDetail(LocName As String)
        Try
            Dim UnionName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select [TSPL_APP_LOCATION].DataBase_Name from [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] 
                              where [TSPL_APP_LOCATION].Location_Name=  '" + LocName + "' "))

            Dim query As String = ""
            query = " select  max([Union Name])[Union Name],Source_Doc_Date, sum(LTR_QTY)LTR_QTY , sum(KG_QTY)KG_QTY,sum(Fat_KG)Fat_KG,sum(SNF_KG)SNF_KG  from (  SELECT '" + LocName + "' as [Union Name]
           , Source_Doc_Date,convert(decimal(18,2),(isnull(TSPL_INVENTORY_MOVEMENT.Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[LTR]) as LTR_QTY, convert(decimal(18,2),(isnull(TSPL_INVENTORY_MOVEMENT.Qty,0) 
           *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))/I.[KG]) as KG_QTY , TSPL_INVENTORY_MOVEMENT.Qty,TSPL_INVENTORY_MOVEMENT.Stock_UOM,TSPL_INVENTORY_MOVEMENT.Fat_KG,TSPL_INVENTORY_MOVEMENT.SNF_KG 
           FROM [" + UnionName + "].[dbo].TSPL_INVENTORY_MOVEMENT 
           left JOIN [" + UnionName + "].[dbo].TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = [ASHOK].[dbo].TSPL_INVENTORY_MOVEMENT.Item_Code
           LEFT OUTER JOIN [" + UnionName + "].[dbo].TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_INVENTORY_MOVEMENT.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_INVENTORY_MOVEMENT.Stock_UOM
           left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from [" + UnionName + "].[dbo].TSPL_ITEM_UOM_DETAIL ) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_INVENTORY_MOVEMENT.Item_Code = I.item_code 
           where Trans_Type in ('DRY-PRO-UPL' , 'PROD_ENTRY' , 'PRO-SFT-MGM') and InOut = 'I' and TSPL_ITEM_MASTER.Product_Type = 'MP'  and convert(date,Source_Doc_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "'   and convert(date,Source_Doc_Date,103)  <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' ) xx group by  Source_Doc_Date "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gvfrmUnionProductionDetails.DataSource = Nothing
                gvfrmUnionProductionDetails.Rows.Clear()
                gvfrmUnionProductionDetails.Columns.Clear()
                gvfrmUnionProductionDetails.GroupDescriptors.Clear()
                gvfrmUnionProductionDetails.MasterTemplate.SummaryRowsBottom.Clear()
                gvfrmUnionProductionDetails.MasterView.Refresh()
                gvfrmUnionProductionDetails.DataSource = dt

                SetGridFormat()
                gvfrmUnionProductionDetails.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage3
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat()
        gvfrmUnionProductionDetails.AutoExpandGroups = True
        gvfrmUnionProductionDetails.ShowGroupPanel = False
        gvfrmUnionProductionDetails.ShowRowHeaderColumn = False
        gvfrmUnionProductionDetails.AllowAddNewRow = False
        gvfrmUnionProductionDetails.AllowDeleteRow = False
        gvfrmUnionProductionDetails.EnableFiltering = True
        gvfrmUnionProductionDetails.ShowFilteringRow = True

        For ii As Integer = 0 To gvfrmUnionProductionDetails.Columns.Count - 1
            gvfrmUnionProductionDetails.Columns(ii).ReadOnly = True
            gvfrmUnionProductionDetails.Columns(ii).BestFit()
            gvfrmUnionProductionDetails.Columns(ii).Width = 100
        Next

        gvfrmUnionProductionDetails.Columns("Source_Doc_Date").HeaderText = "Date"
        gvfrmUnionProductionDetails.Columns("LTR_QTY").HeaderText = "Qty Ltr"
        gvfrmUnionProductionDetails.Columns("KG_QTY").HeaderText = "Qty Kg"
        gvfrmUnionProductionDetails.Columns("Fat_KG").HeaderText = "Fat Kg"
        gvfrmUnionProductionDetails.Columns("SNF_KG").HeaderText = "Snf Kg"
    End Sub

End Class