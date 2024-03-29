Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls
Public Class RptRouteSaleRegister
    Inherits FrmMainTranScreen

    Sub Reset()
        Gv1.DataSource = Nothing
        txtRoute.arrValueMember = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        ControlEnableDisable(True)
    End Sub

    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        fromDate.Enabled = isEnable
        dtpToDate.Enabled = isEnable
    End Sub


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Public Sub LoadData()

        Try
            Dim dt As New DataTable
            Dim strQry As String
            strQry = "SELECT TSPL_ROUTE_MASTER.Route_No as Route_No,max(TSPL_ROUTE_MASTER.Route_Desc) as Route_Name,(TSPL_DEMAND_BOOKING_DETAIL.Item_Code) as Item_Code,max(tspl_item_master.Item_Desc) as Iteam_Name,sum(TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise) as CRATE,(sum(TSPL_DEMAND_BOOKING_DETAIL.Qty) - sum(TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise)) as POUCH,sum(TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise) as LTR
                                    FROM TSPL_DEMAND_BOOKING_DETAIL 
                                    left outer join tspl_item_master on  TSPL_DEMAND_BOOKING_DETAIL.item_code=tspl_item_master.item_code
                                    left outer join TSPL_DEMAND_BOOKING_MASTER on  TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_DEMAND_BOOKING_MASTER.Document_No
                                    left outer join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
                                    where TSPL_DEMAND_BOOKING_MASTER.Document_Date >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.Document_Date<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'
                                    group by TSPL_ROUTE_MASTER.Route_No,TSPL_DEMAND_BOOKING_DETAIL.Item_Code"

            If btnproduct.IsChecked =true Then
                strQry = "SELECT TSPL_ROUTE_MASTER.Route_No as Route_No,max(TSPL_ROUTE_MASTER.Route_Desc) as Route_Name,(TSPL_DEMAND_BOOKING_DETAIL.Item_Code) as Item_Code,max(tspl_item_master.Item_Desc) as Iteam_Name,max(TSPL_DEMAND_BOOKING_DETAIL.Unit_code) UOM,sum(TSPL_DEMAND_BOOKING_DETAIL.Qty) as Qty
                                    FROM TSPL_DEMAND_BOOKING_DETAIL 
                                    left outer join tspl_item_master on  TSPL_DEMAND_BOOKING_DETAIL.item_code=tspl_item_master.item_code
                                    left outer join TSPL_DEMAND_BOOKING_MASTER on  TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_DEMAND_BOOKING_MASTER.Document_No
                                    left outer join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
                                    where TSPL_DEMAND_BOOKING_MASTER.Document_Date >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.Document_Date<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.ItemType='Ambient'
                                    group by TSPL_ROUTE_MASTER.Route_No,TSPL_DEMAND_BOOKING_DETAIL.Item_Code"

            ElseIf rbtnmilk.IsChecked =true Then 
                strQry="SELECT TSPL_ROUTE_MASTER.Route_No as Route_No,max(TSPL_ROUTE_MASTER.Route_Desc) as Route_Name,(TSPL_DEMAND_BOOKING_DETAIL.Item_Code) as Item_Code,max(tspl_item_master.Item_Desc) as Iteam_Name,sum(TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise) as CRATE,(sum(TSPL_DEMAND_BOOKING_DETAIL.Qty) - sum(TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise)) as POUCH,sum(TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise) as LTR
                                    FROM TSPL_DEMAND_BOOKING_DETAIL 
                                    left outer join tspl_item_master on  TSPL_DEMAND_BOOKING_DETAIL.item_code=tspl_item_master.item_code
                                    left outer join TSPL_DEMAND_BOOKING_MASTER on  TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_DEMAND_BOOKING_MASTER.Document_No
                                    left outer join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
                                    where TSPL_DEMAND_BOOKING_MASTER.Document_Date >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.Document_Date<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.ItemType='Fresh'
                                    group by TSPL_ROUTE_MASTER.Route_No,TSPL_DEMAND_BOOKING_DETAIL.Item_Code"
                ElseIf btnBoth.IsChecked =true Then
                strQry = "SELECT TSPL_ROUTE_MASTER.Route_No as Route_No,max(TSPL_ROUTE_MASTER.Route_Desc) as Route_Name,(TSPL_DEMAND_BOOKING_DETAIL.Item_Code) as Item_Code,max(tspl_item_master.Item_Desc) as Iteam_Name,sum(TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise) as CRATE,(sum(TSPL_DEMAND_BOOKING_DETAIL.Qty) - sum(TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise)) as POUCH,sum(TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise) as LTR
                                    FROM TSPL_DEMAND_BOOKING_DETAIL 
                                    left outer join tspl_item_master on  TSPL_DEMAND_BOOKING_DETAIL.item_code=tspl_item_master.item_code
                                    left outer join TSPL_DEMAND_BOOKING_MASTER on  TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_DEMAND_BOOKING_MASTER.Document_No
                                    left outer join TSPL_ROUTE_MASTER on TSPL_DEMAND_BOOKING_MASTER.Route_No=TSPL_ROUTE_MASTER.Route_No
                                    where TSPL_DEMAND_BOOKING_MASTER.Document_Date >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_DEMAND_BOOKING_MASTER.Document_Date<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'
                                    group by TSPL_ROUTE_MASTER.Route_No,TSPL_DEMAND_BOOKING_DETAIL.Item_Code"

            End If

            'Dim strQry As String = "Select TSPL_ROUTE_MASTER.Route_No as Code,Route_Desc as Description from TSPL_ROUTE_MASTER"
            
            'group by TSPL_DEMAND_BOOKING_DETAIL.Item_Code
            'order by TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_DEMAND_BOOKING_DETAIL.ShiftType asc"

            dt = clsDBFuncationality.GetDataTable(strQry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt

                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.EnableFiltering = True
                'FormatGrid()
                ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If

            Gv1.BestFitColumns()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMobileAppMilkCollection & "'"))
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, Me.Text)
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMobileAppMilkCollection & "'"))

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rptBmcCollection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fromDate.Value = DateTime.Now()
        dtpToDate.Value = DateTime.Now()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Reset()
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select Route_No,Route_Desc from TSPL_ROUTE_MASTER where 2=2 "
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += "  and MCC_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            End If

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "Route_No", "Route_Desc", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
