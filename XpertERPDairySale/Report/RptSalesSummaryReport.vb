Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls

Public Class RptSalesSummaryReport
    Inherits FrmMainTranScreen


    Sub Reset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
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

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Reset()
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

    Private Sub RptSalesSummaryReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fromDate.Value = DateTime.Now()
        dtpToDate.Value = DateTime.Now()
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select Route_No,Route_Desc from TSPL_ROUTE_MASTER where 2=2 "

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "Route_No", "Route_Desc", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Public Sub LoadData()
        Try
            Dim whrd As String = ""
            Dim obj As clsDemandBookingSale = Nothing
            Dim dt As New DataTable
            Dim Qry As String = Nothing


            Dim itemName1 As String = Nothing
            Dim itemNames As String = Nothing
            Dim itemNamesQty As String = Nothing

            Qry = "select Distinct tspl_item_master.Item_Code,tspl_item_master.Short_Description from TSPL_DEMAND_BOOKING_DETAIL
                 left outer join TSPL_DEMAND_BOOKING_master on TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
	             inner join tspl_item_master on tspl_item_master.Item_Code=TSPL_DEMAND_BOOKING_DETAIL.Item_Code where TSPL_DEMAND_BOOKING_MASTER.Route_no  in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") and TSPL_DEMAND_BOOKING_DETAIL.Qty>0 and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "' "
            Dim dtitemName As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dtitemName.Rows.Count > 0 Then
                For i As Integer = 0 To dtitemName.Rows.Count - 1
                    itemName1 += "Sum(IsNull([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]" + ","
                    'FinalItemNamesQty += "SUM(XXFINAL.[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]) As [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "]" + ","

                    If i = 0 Then
                        itemNamesQty += "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)"
                        itemNames += "[" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "] "
                    Else
                        itemNamesQty += "+" + "ISNULL([" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "],0)"
                        itemNames += ", [" + clsCommon.myCstr(dtitemName.Rows(i)("Short_Description")) + "] "
                    End If
                Next
            Else
                Gv1.DataSource = Nothing
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
                Exit Sub
            End If

            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                whrd = " where Route_no  in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            End If
            Qry = "     Select * from (
                        SELECT route_no,MAx(tspl_item_master.Short_Description),SUM(TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise) AS TotalCrates_ItemWise,
                        SUM(TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise) AS TotalLtr_ItemWise
                        FROM TSPL_DEMAND_BOOKING_DETAIL 
                        left outer join tspl_item_master on  TSPL_DEMAND_BOOKING_DETAIL.item_code=tspl_item_master.item_code 
                        left outer join TSPL_DEMAND_BOOKING_master on TSPL_DEMAND_BOOKING_DETAIL.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
                        " + whrd + " and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'  
                        GROUP BY 
                        Document_Date,
                        route_no,
                        tspl_item_master.Item_Code,                   
                        TSPL_DEMAND_BOOKING_DETAIL.ShiftType
                        ) xx
						PIVOT (SUM(TotalLtr_ItemWise)
         FOR Short_Description IN (" + itemNames + ")
         ) AS pivot_crate"

            'order by TSPL_DEMAND_BOOKING_DETAIL.Cust_Code,TSPL_DEMAND_BOOKING_DETAIL.ShiftType asc"
            dt = clsDBFuncationality.GetDataTable(Qry)
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
                ' FormatGrid()
                'SetGridFormation()
                ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If

            Gv1.BestFitColumns()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

End Class
