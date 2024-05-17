'--31/10/2013--form Add By- Pradeep Sharma @ Ticket Id BM00000000526---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class frmProductionVariance
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ReportID As String = "frmProductionVariance"
    Dim formtype As String = Nothing
    Dim ApplyStandardProductionVariance As Boolean = False
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
#Region "Variable"
    Dim Qry As String
    Dim DT As DataTable
#End Region
    Sub LoadData()
        btnGenrate.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        If ApplyStandardProductionVariance = True Then
            Qry = " select  PP.PROD_ENTRY_CODE,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,TSPL_SPP_PRODUCTION_ENTRY.Shift_Code,PP.LOCATION_CODE,TSPL_LOCATION_MASTER.Location_Desc,PP.BOM_CODE,TSPL_MF_BOM_HEAD.DESCRIPTION AS BOM_Desc,PP.Main_ITEM_CODE,TSPL_ITEM_MASTER.ITEM_DESC AS Main_ITEM_Desc,PP.MAIN_UOM,TSPL_UNIT_MASTER.UNIT_DESC AS  MAIN_UOM_Desc,PP.CONSM_ITEM_CODE,Consm_Item.Item_Desc as Consm_Item_Desc,PP.UNIT_CODE as Consm_Unit_Code,PP.CONSM_QTY,PP.AVG_COST
                    , TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY ,( case when TSPL_MF_BOM_DETAIL.Percentage > 0 then  cast( ((TSPL_MF_BOM_DETAIL.Percentage /100) * TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY ) as decimal(18,2)) when TSPL_MF_BOM_DETAIL.CONSM_QUANTITY > 0 then  cast( ( TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY  *  ( ( Target_UOM.Conversion_Factor/ nullif (Source_UOM.Conversion_Factor,0) )) ) as decimal(18,2)) else 0 end )  as ByBOM_Consp_Item_Qty
                    , cast (( PP.CONSM_QTY -  ( case when TSPL_MF_BOM_DETAIL.Percentage > 0 then  cast( ((TSPL_MF_BOM_DETAIL.Percentage /100) * TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY ) as decimal(18,2)) when TSPL_MF_BOM_DETAIL.CONSM_QUANTITY > 0 then  cast( ( TSPL_SPP_PRODUCTION_ENTRY_DETAIL.FINAL_PRODUCTION_QTY  *  ( ( Target_UOM.Conversion_Factor/ nullif (Source_UOM.Conversion_Factor,0) )) ) as decimal(18,2)) else 0 end )) as Decimal(18,2)) as Diffrence
                    from TSPL_SPP_CONSUMPTION_WITHOUT_BATCH PP  LEFT JOIN TSPL_ITEM_MASTER ON PP.Main_ITEM_CODE=TSPL_ITEM_MASTER.ITEM_CODE  LEFT JOIN TSPL_MF_BOM_HEAD ON PP.BOM_CODE=TSPL_MF_BOM_HEAD.BOM_CODE
                    LEFT JOIN TSPL_UNIT_MASTER ON PP.MAIN_UOM=TSPL_UNIT_MASTER.UNIT_CODE  LEFT JOIN TSPL_ITEM_MASTER Consm_Item ON PP.CONSM_ITEM_CODE=Consm_Item.ITEM_CODE  left join TSPL_LOCATION_MASTER ON PP.LOCATION_CODE=TSPL_LOCATION_MASTER.LOCATION_CODE 
                    left outer join TSPL_SPP_PRODUCTION_ENTRY_DETAIL on TSPL_SPP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE = PP.PROD_ENTRY_CODE and TSPL_SPP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE = TSPL_MF_BOM_HEAD.PROD_ITEM_CODE
                    left outer join TSPL_MF_BOM_DETAIL on TSPL_MF_BOM_DETAIL.BOM_CODE = TSPL_MF_BOM_HEAD.BOM_CODE and TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE = PP.CONSM_ITEM_CODE
                    left outer join TSPL_ITEM_UOM_DETAIL as Source_UOM on Source_UOM.Item_Code = TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE and Source_UOM.UOM_Code = TSPL_MF_BOM_DETAIL.CONSM_ITEM_UNIT_CODE
                    left outer join TSPL_ITEM_UOM_DETAIL as Target_UOM on Target_UOM.Item_Code = TSPL_MF_BOM_DETAIL.CONSM_ITEM_CODE 
					and Target_UOM.UOM_Code = PP.UNIT_CODE
					left outer join TSPL_SPP_PRODUCTION_ENTRY on TSPL_SPP_PRODUCTION_ENTRY.PROD_ENTRY_CODE = PP.PROD_ENTRY_CODE 
                     where TSPL_SPP_PRODUCTION_ENTRY.POSTED = 1 and convert (date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103) >= convert (date,'" + clsCommon.GetPrintDate(dtpFrom.Value, "dd/MMM/yyyy") + "',103) and  convert (date,TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE,103) <= convert (date,'" + clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") + "',103)
                    "
        Else
            Qry = " select TSPL_MF_RECEIPT.RECEIPT_CODE ,TSPL_MF_RECEIPT.RECEIPT_DATE ,TSPL_MF_RECEIPT.DESCRIPTION ,TSPL_MF_RECEIPT.BO_CODE ," &
              " TSPL_MF_RECEIPT_DETAIL.PROD_PLAN_CODE ,TSPL_MF_RECEIPT_DETAIL.PRODUCTION_LINE_CODE ,TSPL_MF_RECEIPT_DETAIL.BOM_CODE ,TSPL_MF_RECEIPT_DETAIL.ITEM_CODE ," &
              " TSPL_MF_RECEIPT_DETAIL.ITEM_DESCRIPTION ,TSPL_MF_RECEIPT_DETAIL.BATCH_QTY ,TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY ," &
              " (TSPL_MF_RECEIPT_DETAIL.BATCH_QTY - TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY) AS [Varience], " &
              " (((TSPL_MF_RECEIPT_DETAIL.BATCH_QTY - TSPL_MF_RECEIPT_DETAIL.RECEIPT_QTY)*100)/TSPL_MF_RECEIPT_DETAIL.BATCH_QTY) AS [Varienc_Pre], " &
              " TSPL_MF_RECEIPT_DETAIL.REJ_QTY ,TSPL_MF_RECEIPT_DETAIL.BREAKAGE_QTY,TSPL_MF_RECEIPT_DETAIL.UNIT_CODE from TSPL_MF_RECEIPT " &
              " left outer join TSPL_MF_RECEIPT_DETAIL on TSPL_MF_RECEIPT_DETAIL.RECEIPT_CODE =  TSPL_MF_RECEIPT.RECEIPT_CODE " &
              " where 2=2 " &
              " and convert(date,TSPL_MF_RECEIPT.RECEIPT_DATE,103) >= '" + clsCommon.GetPrintDate(dtpFrom.Value, "dd/MMM/yyyy") + "' and " &
              " convert(date,TSPL_MF_RECEIPT.RECEIPT_DATE,103) <= '" + clsCommon.GetPrintDate(dtpTo.Value, "dd/MMM/yyyy") + "' "

        End If

        If chkLocationAll.IsChecked = False Then
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Location", Me.Text)
                Return
            ElseIf cbgLocation.CheckedValue.Count > 0 Then
                If ApplyStandardProductionVariance = True Then
                    Qry += " and PP.LOCATION_CODE in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                Else
                    Qry += " and TSPL_MF_RECEIPT.LOCATION_CODE in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If

            End If
        End If
        If chkItemAll.IsChecked = False Then
            If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one ItemCode", Me.Text)
                Return
            ElseIf cbgItem.CheckedValue.Count > 0 Then

                If ApplyStandardProductionVariance = True Then
                    Qry += "  and PP.Main_ITEM_CODE in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                Else
                    Qry += "  and TSPL_MF_RECEIPT_DETAIL.ITEM_CODE in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                End If

            End If
        End If
        If ApplyStandardProductionVariance = True Then
            Qry += " Order By TSPL_SPP_PRODUCTION_ENTRY.PROD_DATE "
        Else
            Qry += " Order By TSPL_MF_RECEIPT.RECEIPT_DATE "
        End If


        DT = clsDBFuncationality.GetDataTable(Qry)
        If (DT IsNot Nothing AndAlso DT.Rows.Count > 0) Then
            gv1.DataSource = DT
            If ApplyStandardProductionVariance = True Then
                gv1.Columns("PROD_ENTRY_CODE").HeaderText = "PROD ENTRY CODE"
                gv1.Columns("PROD_DATE").HeaderText = "PROD Date"
                gv1.Columns("PROD_DATE").FormatString = "{0:  dd/MMM/yyyy}"
                gv1.Columns("Shift_Code").HeaderText = "SHIFT CODE"
                gv1.Columns("LOCATION_CODE").HeaderText = "LOCATION CODE"
                gv1.Columns("Location_Desc").HeaderText = "LOCATION DESC"
                gv1.Columns("BOM_CODE").HeaderText = "BOM CODE"
                gv1.Columns("BOM_Desc").HeaderText = "BOM DESC"
                gv1.Columns("Main_ITEM_CODE").HeaderText = "MAIN ITEM CODE"
                gv1.Columns("Main_ITEM_Desc").HeaderText = "MAIN ITEM DESC"
                gv1.Columns("MAIN_UOM").HeaderText = "MAIN UOM"
                gv1.Columns("MAIN_UOM_Desc").HeaderText = "MAIN UOM"
                gv1.Columns("MAIN_UOM_Desc").IsVisible = False
                gv1.Columns("CONSM_ITEM_CODE").HeaderText = "CONSM ITEM CODE"
                gv1.Columns("Consm_Item_Desc").HeaderText = "CONSM ITEM DESC"
                gv1.Columns("Consm_Unit_Code").HeaderText = "CONSM UOM"
                gv1.Columns("CONSM_QTY").HeaderText = "PROD CONSM QTY"
                gv1.Columns("AVG_COST").HeaderText = "AVG COST"
                gv1.Columns("AVG_COST").IsVisible = False
                gv1.Columns("FINAL_PRODUCTION_QTY").HeaderText = "FINAL PRODUCTION QTY"
                gv1.Columns("FINAL_PRODUCTION_QTY").IsVisible = False
                gv1.Columns("ByBOM_Consp_Item_Qty").HeaderText = "BOM CONSM QTY"
                gv1.Columns("Diffrence").HeaderText = "DIFFRENCE"

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0

                Dim item1 As New GridViewSummaryItem("CONSM_QTY", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("ByBOM_Consp_Item_Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("Diffrence", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                gv1.MasterTemplate.ShowTotals = True
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            Else

                gv1.Columns("RECEIPT_CODE").Width = 100
                gv1.Columns("RECEIPT_CODE").HeaderText = "Receipt No."
                gv1.Columns("RECEIPT_DATE").Width = 100
                gv1.Columns("RECEIPT_DATE").HeaderText = "Receipt Date"
                gv1.Columns("RECEIPT_DATE").FormatString = "{0:  dd/MMM/yyyy}"
                gv1.Columns("DESCRIPTION").Width = 100
                gv1.Columns("DESCRIPTION").HeaderText = "Description"
                gv1.Columns("BO_CODE").Width = 100
                gv1.Columns("BO_CODE").HeaderText = "Batch Order"
                gv1.Columns("PROD_PLAN_CODE").Width = 150
                gv1.Columns("PROD_PLAN_CODE").HeaderText = "Production Plan"
                gv1.Columns("PRODUCTION_LINE_CODE").Width = 150
                gv1.Columns("PRODUCTION_LINE_CODE").HeaderText = "Production Line"
                gv1.Columns("BOM_CODE").Width = 150
                gv1.Columns("BOM_CODE").HeaderText = "BOM Code"
                gv1.Columns("ITEM_CODE").Width = 150
                gv1.Columns("ITEM_CODE").HeaderText = "Item Code"
                gv1.Columns("ITEM_DESCRIPTION").Width = 150
                gv1.Columns("ITEM_DESCRIPTION").HeaderText = "Item Description"
                gv1.Columns("BATCH_QTY").Width = 80
                gv1.Columns("BATCH_QTY").HeaderText = "Batch Qty"
                gv1.Columns("BATCH_QTY").FormatString = "{0:N2}"
                gv1.Columns("RECEIPT_QTY").Width = 80
                gv1.Columns("RECEIPT_QTY").HeaderText = "Receipt Qty"
                gv1.Columns("RECEIPT_QTY").FormatString = "{0:N2}"

                gv1.Columns("Varience").Width = 80
                gv1.Columns("Varience").FormatString = "{0:N2}"
                gv1.Columns("Varienc_Pre").Width = 80
                gv1.Columns("Varienc_Pre").HeaderText = "Varience %"
                gv1.Columns("Varienc_Pre").FormatString = "{0:N2}"


                gv1.Columns("REJ_QTY").Width = 80
                gv1.Columns("REJ_QTY").HeaderText = "Reject Qty"
                gv1.Columns("REJ_QTY").FormatString = "{0:N2}"
                gv1.Columns("BREAKAGE_QTY").Width = 80
                gv1.Columns("BREAKAGE_QTY").HeaderText = "Breakage Qty"
                gv1.Columns("BREAKAGE_QTY").FormatString = "{0:N2}"
                gv1.Columns("UNIT_CODE").Width = 80
                gv1.Columns("UNIT_CODE").HeaderText = "Unit"

            End If
            ReStoreGridLayout()
            RadPageView1.SelectedPage = RadPageViewPage2
            gv1.BestFitColumns()
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data to Show in Selected Dates.", Me.Text)
        End If

    End Sub

    Private Sub frmProductionVariance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        funReset()
        ItemLoad()
        LoadLocation()
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+R for Refresh ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        ApplyStandardProductionVariance = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyStandardProductionVariance, clsFixedParameterCode.ApplyStandardProductionVariance, Nothing)) > 0)
        If ApplyStandardProductionVariance = True Then
            ReportID = "Std_" + ReportID
        End If
    End Sub
    Public Sub ItemLoad()
        Qry = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER where Item_Type= 'F' "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
    End Sub

    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub


    Private Sub SetUserMgmtNew()
        If formtype = clsUserMgtCode.frmProductionVarianceSTD Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmProductionVarianceSTD)
        ElseIf formtype = clsUserMgtCode.frmProductionVariancePepsi Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmProductionVariancePepsi)
        End If
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnGenrate.Visible = MyBase.isModifyFlag

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        btnGenrate.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        dtpFrom.Value = (clsCommon.GETSERVERDATE().AddDays(-30))
        dtpTo.Value = clsCommon.GETSERVERDATE()
        chkItemAll.IsChecked = True
        chkLocationAll.IsChecked = True
        ItemLoad()
        LoadLocation()
        btnGenrate.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmProductionVariance_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        LoadData()
    End Sub

    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
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
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub
    Private Sub btnExcel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToExcel.Click
        PrintData1(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportToPDF.Click
        PrintData1(EnumExportTo.PDF)
    End Sub

    Sub PrintData1(ByVal exporter As EnumExportTo)
        If gv1.Rows.Count > 0 Then
            Dim fromdate As String = clsCommon.myCDate(dtpFrom.Value, "dd/MMM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpTo.Value, "dd/MMM/yyyy")
            Dim str As String = "Production Variance Report"

            Dim arr As New List(Of String)()
            arr.Add("Production Variance Report")
            arr.Add("  From Date:  " + fromdate + "  To Date: " + Todate + "   ")

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid(str, gv1, arr, "Production Variance Report")
            Else
                clsCommon.MyExportToPDF(str, gv1, arr, "Production Variance Report", True)
            End If
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Record Found to print.", Me.Text)
        End If
    End Sub

End Class
