'' Create new Screen for Amritha against ticket no. ERO/28/05/18-000328 
'' work done agaist ticket no. ERO/04/06/18-000334
Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared

Public Class FrmQuickDemandReport
    Inherits FrmMainTranScreen
    Dim SelFromDate As String = Nothing
    Dim SelToDate As String = Nothing
    Dim ReportID As String = ""
    Dim SeparateDemandMilkandProduct As Boolean = False
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gvData
        gvData.DataSource = Nothing
        gvData.Rows.Clear()
        Try
            Dim strFromDate As String = clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim lstItemDesc As New List(Of String)
            Dim strQry As String = "select * from (select 'Fresh' as FreshAmbient,tspl_item_master.Item_Code,tspl_item_master.Short_Description ,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit   from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate')
    union
    select 'Fresh' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit  from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
    and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate'))z order by RowNo,Sku_Seq,Item_Code"

            Dim dtitem As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dtitem IsNot Nothing AndAlso dtitem.Rows.Count > 0 Then

                For Each dr As DataRow In dtitem.Rows
                    Dim itemdesc As String = "[" + clsCommon.myCstr(dr("Short_Description")) + "]"
                    If Not lstItemDesc.Contains(itemdesc) Then
                        lstItemDesc.Add(itemdesc)
                    End If
                Next
            Else
                Throw New Exception("Item Not Found!")
            End If

            strQry = "WITH BaseQuery AS (
    SELECT 
        TSPL_DEMAND_BOOKING_MASTER.Document_Date, 
        TSPL_DEMAND_BOOKING_MASTER.ShiftType,
        TSPL_DEMAND_BOOKING_DETAIL.Cust_Code as Booth_Code,
        TSPL_CUSTOMER_MASTER.Customer_Name as Booth_Name,
        TSPL_DEMAND_BOOKING_MASTER.Route_No,
        TSPL_ITEM_MASTER.Short_Description,
        TSPL_DEMAND_BOOKING_DETAIL.Qty
    FROM TSPL_DEMAND_BOOKING_MASTER
    LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL ON TSPL_DEMAND_BOOKING_DETAIL.Document_No = TSPL_DEMAND_BOOKING_MASTER.Document_No
    LEFT JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code
    LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
    WHERE CONVERT(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 103) >= '" + strFromDate + "'
      AND CONVERT(date, TSPL_DEMAND_BOOKING_MASTER.Document_Date, 103) <= '" + strToDate + "'
" + IIf(txtMultiRoute.arrValueMember IsNot Nothing, " AND TSPL_DEMAND_BOOKING_MASTER.Route_No in(" + clsCommon.GetMulcallString(txtMultiRoute.arrValueMember) + ")", "") + ""
            If Not rbtnBoth.Checked Then

                strQry += " And TSPL_DEMAND_BOOKING_MASTER.ShiftType = '" + IIf(rbtnMorning.Checked, "Morning", "Evening") + "' "
            End If

            strQry += " And TSPL_DEMAND_BOOKING_DETAIL.REF_PK_ID Is Not NULL
)
Select *
From BaseQuery
PIVOT(
    SUM(Qty)
    For Short_Description IN (
        " + clsCommon.GetMulcallStringWithComma(lstItemDesc) + "
    )
) AS PivotTable;"


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
                Exit Sub
            Else
                RadPageView1.SelectedPage = RadPageViewPage2
                gvData.GroupDescriptors.Clear()
                gvData.MasterTemplate.SummaryRowsBottom.Clear()
                gvData.DataSource = dt

                SetGridFormationOFGV1()
                gvData.AutoExpandGroups = True
                gvData.ShowGroupPanel = False
                gvData.ShowRowHeaderColumn = False
                gvData.AllowAddNewRow = False
                gvData.AllowDeleteRow = False
                gvData.EnableFiltering = True
                gvData.ShowFilteringRow = True
                gvData.BestFitColumns()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Sub SetGridFormationOFGV1()
        gvData.TableElement.TableHeaderHeight = 40
        gvData.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvData.Columns.Count - 1
            gvData.Columns(ii).ReadOnly = True
        Next


        Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim item1 As New GridViewSummaryItem("Booking Qty", "{0F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem("DO Qty", "{0F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        'Dim item3 As New GridViewSummaryItem("Dispatch Qty", "{0F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)

        gvData.ShowGroupPanel = False
        gvData.MasterTemplate.AutoExpandGroups = True
        gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvData.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ReStoreGridLayout()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub Reset()
        txtfDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtMultiRoute.arrValueMember = Nothing

        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnMorning.Checked = True

    End Sub
    Private Sub FrmQuickDemandReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Private Sub FrmQuickDemandReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SeparateDemandMilkandProduct = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SeparateDemandMilkandProduct, clsFixedParameterCode.SeparateDemandMilkandProduct, Nothing)) = 1, True, False)

        Reset()
        ReportID = MyBase.Form_ID
    End Sub
    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiRoute._My_Click
        Dim qry As String = ""
        If SeparateDemandMilkandProduct Then
            qry = "Select TSPL_ROUTE_MASTER.Route_No as Code, Route_Desc As Description, Type, Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
                        Else
                        qry = "Select TSPL_ROUTE_MASTER.Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
        End If
            txtMultiRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Description", txtMultiRoute.arrValueMember, txtMultiRoute.arrDispalyMember)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvData.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvData.Columns.Count - 1 Step ii + 1
                        gvData.Columns(ii).IsVisible = False
                        gvData.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvData.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub rmexcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(Exporter.Excel)
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtMultiRoute.arrValueMember IsNot Nothing AndAlso txtMultiRoute.arrValueMember.Count > 0 Then
                Dim strRoute As String = clsCommon.GetMulcallStringWithComma(txtMultiRoute.arrValueMember)
                arrHeader.Add((" Route  : " + strRoute + " "))
            Else
                arrHeader.Add((" Route : All"))
            End If

            transportSql.applyExportTemplate(gvData, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Quick Demand Report", gvData, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Quick Demand Report", gvData, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gvData.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvData.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gvData.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------

        End If
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub



    Private Sub RadPageViewPage1_Paint(sender As Object, e As PaintEventArgs) Handles RadPageViewPage1.Paint

    End Sub
End Class
