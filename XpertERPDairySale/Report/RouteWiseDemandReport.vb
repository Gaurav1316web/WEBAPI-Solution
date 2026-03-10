Imports common
Imports System.IO
Public Class RouteWiseDemandReport

    Private Sub RouteWiseDemandReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        ToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        rdbBranch.IsChecked = True
        rdbAll.IsChecked = False
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Close()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        fromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        ToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        rdbBranch.IsChecked = True
        rdbAll.IsChecked = False
        txtRoute.arrValueMember = Nothing
        txtMultCustomer.arrValueMember = Nothing
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        'Dim strtxtfDate As String = clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")
        'Dim strToDate As String = clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")
        Dim qry As String = Nothing
        Dim whr As String = Nothing
        Try

            '     qry = " SELECT DISTINCT
            '             TSPL_CUSTOMER_MASTER.Route_No AS [Route No],
            '             TSPL_CUSTOMER_MASTER.Route_Desc AS [descpition],
            '             TSPL_CUSTOMER_MASTER.Customer_Name AS [Distubuter],
            '             TSPL_DEMAND_BOOKING_MASTER.ShiftType AS [shift],
            '             CASE 
            '                 WHEN TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate' 
            '                 THEN TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise 
            '                 ELSE 0 
            '             END AS Crate,
            '             CASE 
            '                 WHEN TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch' 
            '                 THEN TSPL_DEMAND_BOOKING_DETAIL.Qty 
            '                 ELSE 0 
            '             END AS Pouch
            '         FROM TSPL_DEMAND_BOOKING_MASTER
            '         LEFT OUTER JOIN TSPL_DEMAND_BOOKING_DETAIL
            '             ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
            '         LEFT OUTER JOIN TSPL_CUSTOMER_MASTER
            '             ON TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_CUSTOMER_MASTER.Route_No
            'where convert(TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) >= '" + fromDate.Value + "' and convert(TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) <= '" + ToDate.Value + "' "

            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                whr = " and TSPL_CUSTOMER_MASTER.Route_No in(" & clsCommon.GetMulcallString(txtRoute.arrValueMember) & ")" & Environment.NewLine
            End If

            If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
                whr += " and TSPL_CUSTOMER_MASTER.Cust_Code in(" & clsCommon.GetMulcallString(txtMultCustomer.arrValueMember) & ")" & Environment.NewLine
            End If

            'qry = " 	SELECT
            '            CONVERT(VARCHAR(10), TSPL_DEMAND_BOOKING_MASTER.Document_Date, 103) AS Document_Date,
            '            TSPL_CUSTOMER_MASTER.Route_No AS [Route No],
            '            TSPL_CUSTOMER_MASTER.Route_Desc AS [Description],
            '            TSPL_CUSTOMER_MASTER.Customer_Name AS [Distributor],

            '            CASE 
            '                WHEN MAX(CASE WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Morning' THEN 1 ELSE 0 END) = 1 
            '                THEN 'YES' ELSE 'NO' 
            '            END AS [Morning Demand Status],

            '             SUM(CASE 
            '                WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Morning' 
            '                AND TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate'
            '                THEN TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise 
            '                ELSE 0 END) AS QTY,

            '           SUM(CASE 
            '                WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Morning' 
            '                AND TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch'
            '                THEN TSPL_DEMAND_BOOKING_DETAIL.Qty 
            '                ELSE 0 END) AS Qty_Pouch,

            '            CASE 
            '                WHEN MAX(CASE WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Evening' THEN 1 ELSE 0 END) = 1 
            '                THEN 'YES' ELSE 'NO' 
            '            END AS [Evening Demand Status],


            '            SUM(CASE 
            '                WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Evening' 
            '                AND TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate'
            '                THEN TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise 
            '                ELSE 0 END) AS QTY,

            '            SUM(CASE 
            '                WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Evening' 
            '                AND TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Pouch'
            '                THEN TSPL_DEMAND_BOOKING_DETAIL.Qty 
            '                ELSE 0 END) AS Qty_Pouch

            '        FROM TSPL_DEMAND_BOOKING_MASTER

            '        LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL
            '            ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No

            '        LEFT JOIN TSPL_CUSTOMER_MASTER
            '            ON TSPL_DEMAND_BOOKING_MASTER.Route_No = TSPL_CUSTOMER_MASTER.Route_No  where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)" + whr + "
            '        GROUP BY TSPL_CUSTOMER_MASTER.Route_No,TSPL_CUSTOMER_MASTER.Route_Desc,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_DEMAND_BOOKING_MASTER.Document_Date "


            qry = "SELECT  
                    CONVERT(VARCHAR(10), CAST(TSPL_DEMAND_BOOKING_MASTER.Document_Date AS DATE), 103) AS Document_Date,
                    TSPL_CUSTOMER_MASTER.Route_No AS [Route No],
                    TSPL_CUSTOMER_MASTER.Route_Desc AS [Description],
                    MAX(tab.Customer_Name) AS [Distributor],

                    CASE 
                        WHEN MAX(CASE WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Morning' THEN 1 ELSE 0 END) = 1 
                        THEN 'YES' ELSE 'NO'
                    END AS [Morning Demand Status],

                    SUM(CASE 
                        WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Morning'
                        AND TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate'
                        THEN TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise
                        ELSE 0
                    END) AS Morning_QTY,

                    CASE 
                        WHEN MAX(CASE WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Evening' THEN 1 ELSE 0 END) = 1 
                        THEN 'YES' ELSE 'NO'
                    END AS [Evening Demand Status],

                    SUM(CASE 
                        WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Evening'
                        AND TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate'
                        THEN TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise
                        ELSE 0
                    END) AS Evening_QTY

                FROM TSPL_DEMAND_BOOKING_MASTER

                LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL
                    ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No

                LEFT JOIN TSPL_CUSTOMER_MASTER
                    ON TSPL_DEMAND_BOOKING_DETAIL.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code

	                outer apply (
	                select top 1 TSPL_CUSTOMER_MASTER.Customer_Name  from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER
                left join TSPL_DISTRIBUTOR_ROUTE on TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Code=TSPL_DISTRIBUTOR_ROUTE.Code
                Left Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code
                                where TSPL_DISTRIBUTOR_ROUTE.Status=1 and TSPL_DISTRIBUTOR_ROUTE.ItemType='M'
                 and CONVERT(date,TSPL_DISTRIBUTOR_ROUTE.Start_Date,103) <=convert(date,'" + fromDate.Value + "',103) and TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No=TSPL_DEMAND_BOOKING_MASTER.Route_No and 2=
                 (Case when TSPL_DISTRIBUTOR_ROUTE.End_Date is null then 2 else 
                 (Case when CONVERT(date,TSPL_DISTRIBUTOR_ROUTE.End_Date,103) >=convert(date,'" + ToDate.Value + "',103) then 2 else 3 end) end) 
                 order by Start_Date desc
	                ) as tab

                where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)
                      

                GROUP BY
                    CAST(TSPL_DEMAND_BOOKING_MASTER.Document_Date AS DATE),
                    TSPL_CUSTOMER_MASTER.Route_No,
                    TSPL_CUSTOMER_MASTER.Route_Desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                RadPageView1.SelectedPage = RadPageViewPage2
                gv2.GroupDescriptors.Clear()
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                gv2.DataSource = dt
                'SetGridFormationgvData()
                gv2.AutoExpandGroups = True
                gv2.ShowGroupPanel = True
                gv2.ShowRowHeaderColumn = False
                gv2.AllowAddNewRow = False
                gv2.AllowDeleteRow = False
                gv2.EnableFiltering = True
                gv2.ShowFilteringRow = True
                gv2.BestFitColumns()
            End If
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            '    Dim frmCRV As New frmCrystalReportViewer()
            '    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, "crptDemandReturn", "")

            'Else
            '    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            '    Exit Sub
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Griddata(False)
    End Sub

    Public Sub Griddata(ByVal print As Boolean)
        Try
            Dim qry As String = Nothing
            qry = " SELECT '" & clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") & "' as from_date, '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") & "' as to_date,
                         CONVERT(VARCHAR(10), CAST(TSPL_DEMAND_BOOKING_MASTER.Document_Date AS DATE), 103) AS Document_Date,
                    TSPL_CUSTOMER_MASTER.Route_No AS [Route No],
                    TSPL_CUSTOMER_MASTER.Route_Desc AS [Description],
                    MAX(tab.Customer_Name) AS [Distributor],

                    CASE 
                        WHEN MAX(CASE WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Morning' THEN 1 ELSE 0 END) = 1 
                        THEN 'YES' ELSE 'NO'
                    END AS [Morning Demand Status],

                    SUM(CASE 
                        WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Morning'
                        AND TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate'
                        THEN TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise
                        ELSE 0
                    END) AS Morning_QTY,

                    CASE 
                        WHEN MAX(CASE WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Evening' THEN 1 ELSE 0 END) = 1 
                        THEN 'YES' ELSE 'NO'
                    END AS [Evening Demand Status],

                    SUM(CASE 
                        WHEN TSPL_DEMAND_BOOKING_MASTER.ShiftType = 'Evening'
                        AND TSPL_DEMAND_BOOKING_DETAIL.Unit_Code = 'Crate'
                        THEN TSPL_DEMAND_BOOKING_DETAIL.TotalLtr_ItemWise
                        ELSE 0
                    END) AS Evening_QTY

                FROM TSPL_DEMAND_BOOKING_MASTER

                LEFT JOIN TSPL_DEMAND_BOOKING_DETAIL
                    ON TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No

                LEFT JOIN TSPL_CUSTOMER_MASTER
                    ON TSPL_DEMAND_BOOKING_DETAIL.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code

	                outer apply (
	                select top 1 TSPL_CUSTOMER_MASTER.Customer_Name  from TSPL_DISTRIBUTOR_ROUTE_CUSTOMER
                left join TSPL_DISTRIBUTOR_ROUTE on TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Code=TSPL_DISTRIBUTOR_ROUTE.Code
                Left Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Cust_Code
                                where TSPL_DISTRIBUTOR_ROUTE.Status=1 and TSPL_DISTRIBUTOR_ROUTE.ItemType='M'
                 and CONVERT(date,TSPL_DISTRIBUTOR_ROUTE.Start_Date,103) <=convert(date,'" + fromDate.Value + "',103) and TSPL_DISTRIBUTOR_ROUTE_CUSTOMER.Route_No=TSPL_DEMAND_BOOKING_MASTER.Route_No and 2=
                 (Case when TSPL_DISTRIBUTOR_ROUTE.End_Date is null then 2 else 
                 (Case when CONVERT(date,TSPL_DISTRIBUTOR_ROUTE.End_Date,103) >=convert(date,'" + ToDate.Value + "',103) then 2 else 3 end) end) 
                 order by Start_Date desc
	                ) as tab

                where convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)
                      

                GROUP BY
                    CAST(TSPL_DEMAND_BOOKING_MASTER.Document_Date AS DATE),
                    TSPL_CUSTOMER_MASTER.Route_No,
                    TSPL_CUSTOMER_MASTER.Route_Desc "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, "RouteWiseDemand", "")

            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim strQry As String = ""
        strQry = "select DISTINCT Route_No  as [Code],Route_Desc as [Name] from TSPL_CUSTOMER_MASTER"
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("txtRoute@Master", strQry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub

    Private Sub txtMultCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultCustomer._My_Click
        Dim strQry As String = ""
        strQry = " select Cust_Code as [Code] , Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
        txtMultCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultCustomer", strQry, "Code", "Name", txtMultCustomer.arrValueMember, txtMultCustomer.arrDispalyMember)
    End Sub


End Class