Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class RptDairyBookingDistributorReport
    Inherits FrmMainTranScreen
    Dim strQry As String = ""
    Dim IsFormLoad As Boolean = False

    Private Sub RptDairyBookingDistributorReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IsFormLoad = False
        fromDate.Value = clsCommon.GetDateWithStartTime(clsCommon.GETSERVERDATE)
        'ToDate.Value = clsCommon.GETSERVERDATE(obj.Document_Date, "dd/MMM/yyyy hh:mm tt")
        ToDate.Value = clsCommon.GetDateWithEndTime(fromDate.Value)
        Reset()
        IsFormLoad = True
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GHO") = CompairStringResult.Equal Then
            RadGroupBox2.Visible = False
        Else
            RadGroupBox2.Visible = True
        End If
    End Sub
    Sub EnableDisableControl()
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GHO") = CompairStringResult.Equal Then
            Panel1.Visible = False
            Panel2.Visible = True
        Else
            Panel1.Visible = True
            Panel2.Visible = False
        End If
    End Sub
    Sub Reset()
        'ToDate.Value = clsCommon.GETSERVERDATE()
        'fromDate.Value = clsCommon.GETSERVERDATE() 'ToDate.Value.AddMonths(-1)
        txtVehicle.arrValueMember = Nothing
        TxtMultiSelectFinder1.arrValueMember = Nothing
        TxtMultiSelectFinder2.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.DataSource = Nothing
        btnGo.Enabled = True
        btnReset.Enabled = False
        RadSplitButton1.Enabled = False
        EnableDisableControl()
        LoadReportType()
        txtMultCustomerGroup.arrValueMember = Nothing
        txtMultCustomer.arrValueMember = Nothing
        txtMultRoute.arrValueMember = Nothing
        txtMultLocation.arrValueMember = Nothing
        txtMultSalesMan.arrValueMember = Nothing
        RadioButton1.Checked = True
        rdbBoth.Checked = True
    End Sub

    Private Sub txtVehicle__My_Click(sender As Object, e As EventArgs) Handles txtVehicle._My_Click
        strQry = " select Cust_Code as [Code] , Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
        txtVehicle.arrValueMember = clsCommon.ShowMultipleSelectForm("TransCNFMulSel", strQry, "Code", "Name", txtVehicle.arrValueMember, txtVehicle.arrDispalyMember)
        TxtMultiSelectFinder1.arrValueMember = Nothing
    End Sub

    Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSelectFinder1._My_Click
        If IsNothing(txtVehicle.arrValueMember) = True Then
            strQry = " select Cust_Code as [Code],Customer_Name as [Name] from TSPL_SECONDARY_CUSTOMER_MASTER where CUSTOMER_TYPE= 'D'  "
        Else
            strQry = " select Cust_Code as [Code],Customer_Name as [Name] from TSPL_SECONDARY_CUSTOMER_MASTER where CUSTOMER_TYPE= 'D' and Parent_Customer_No in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") "
        End If

        TxtMultiSelectFinder1.arrValueMember = clsCommon.ShowMultipleSelectForm("TransDistributorMulSel", strQry, "Code", "Name", TxtMultiSelectFinder1.arrValueMember, TxtMultiSelectFinder1.arrDispalyMember)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GHO") = CompairStringResult.Equal Then
            loaddataGHO()
        Else
            Loaddata()
        End If
    End Sub
    Sub FormatGrid()
        ' Dim strItemCode, head2 As String
        Dim summaryItem As New GridViewSummaryItem()
        Gv1.TableElement.TableHeaderHeight = 150
        Gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns(ii).FormatString = "{0:n2}"
            Gv1.Columns(ii).WrapText = True
        Next
        

        Dim summaryRowItem As New GridViewSummaryRowItem()
        If Gv1.Rows.Count > 0 Then

            For i As Integer = 6 To Gv1.Columns.Count - 1
                Dim aa = Gv1.Columns(i).HeaderText()
                Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Gv1.Columns(i).WrapText = True
            Next
        End If


        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Sub FormatGridUDL()
        ' Dim strItemCode, head2 As String
        Dim summaryItem As New GridViewSummaryItem()
        Gv1.TableElement.TableHeaderHeight = 150
        Gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns(ii).FormatString = "{0:n2}"
            Gv1.Columns(ii).WrapText = True
        Next


        Dim summaryRowItem As New GridViewSummaryRowItem()
        If Gv1.Rows.Count > 0 Then

            For i As Integer = 10 To Gv1.Columns.Count - 1
                Dim aa = Gv1.Columns(i).HeaderText()
                Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Gv1.Columns(i).WrapText = True
            Next
        End If


        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub TxtMultiSelectFinder2__My_Click(sender As Object, e As EventArgs) Handles TxtMultiSelectFinder2._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        TxtMultiSelectFinder2.arrValueMember = clsCommon.ShowMultipleSelectForm("TransLocMulSel", strQry, "Code", "Name", TxtMultiSelectFinder2.arrValueMember, TxtMultiSelectFinder2.arrDispalyMember)
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try

            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Dairy Booking Distributor Report")
                If RadioButton1.Checked Then
                    arrHeader.Add("Document Type : Posted ")
                End If
                If RadioButton2.Checked Then
                    arrHeader.Add("Document Type : Pending ")
                End If

                clsCommon.MyExportToExcelGrid("Dairy Booking Distributor Report", Gv1, arrHeader, "Dairy Booking Distributor Report")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtMultCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtMultCustomerGroup._My_Click
        strQry = "select TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code as Code ,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as Name  from TSPL_CUSTOMER_GROUP_MASTER  "
        txtMultCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultCustomerGroup", strQry, "Code", "Name", txtMultCustomerGroup.arrValueMember, txtMultCustomerGroup.arrDispalyMember)
        'txtMultCustomerGroup.arrValueMember = Nothing
    End Sub

    Private Sub txtMultRoute__My_Click(sender As Object, e As EventArgs) Handles txtMultRoute._My_Click
        strQry = "select TSPL_ROUTE_MASTER.Route_No as Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER "
        txtMultRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultRoute", strQry, "Code", "Name", txtMultRoute.arrValueMember, txtMultRoute.arrDispalyMember)
        'txtMultCustomerGroup.arrValueMember = Nothing
    End Sub
    Sub LoadReportType()
        ddlReporType.DataSource = GetInvoiceType()
        ddlReporType.ValueMember = "Code"
        ddlReporType.DisplayMember = "Name"
    End Sub
    Public Shared Function GetInvoiceType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Dairy Booking"
        dr("Name") = "Dairy Booking"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Dairy Delivery Order"
        dr("Name") = "Dairy Delivery Order"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "Dairy Dispatch"
        dr("Name") = "Dairy Dispatch"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Dairy Sale Invoice"
        dr("Name") = "Dairy Sale Invoice"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Sub Loaddata()
        Try
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim strshortDesp As String = String.Empty
            Dim strtotalShort As String = String.Empty
            Dim strtotalShortTotal As String = String.Empty
            Dim strCustCodeString As String = ""
            Dim status As Integer = 1
            If RadioButton1.Checked = True Then
                status = 1
            End If
            If RadioButton2.Checked = True Then
                status = 0
            End If

            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim CNFDistributor As String = " and 2=2 "
            qry = " select Item_Desc from tspl_item_master where Active=1"
            'qry = " select  case when len(tspl_item_master.Short_Description) <=0 then tspl_item_master.Item_Desc else tspl_item_master.Short_Description end  as Item_Code  from tspl_item_master "
            dt = clsDBFuncationality.GetDataTable(qry)

            Dim arrItemName As New List(Of String) ''BHA/22/11/18-000695 by balwinder on 22/10/2018
            For Each dr As DataRow In dt.Rows
                strshortDesp = clsCommon.myCstr(dr("Item_Desc"))
                If arrItemName.Contains(strshortDesp.ToUpper()) Then
                    Throw New Exception("Repeate item description found [" + strshortDesp + "]")
                Else
                    arrItemName.Add(strshortDesp.ToUpper())
                End If

                If clsCommon.myLen(strshortDesp) > 0 Then
                    strtotalShort = strtotalShort + "," + "[" + strshortDesp + "]"
                    strCustCodeString = strCustCodeString + "," + "  isnull([" & strshortDesp & "],0)  " & "as  " & "[" & strshortDesp & "]" & ""
                    strtotalShortTotal = strtotalShortTotal + "+" + "  isnull([" & strshortDesp & "],0)  " & "  "
                End If
            Next
            If strtotalShort.Substring(0, 1) = "," Then
                strtotalShort = strtotalShort.Substring(1, strtotalShort.Length - 1)
                strCustCodeString = strCustCodeString.Substring(1, strCustCodeString.Length - 1)

            End If


            ' remove + 
            strtotalShortTotal = strtotalShortTotal.Remove(0, 1)





            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                CNFDistributor += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") "
            End If

            If TxtMultiSelectFinder1.arrValueMember IsNot Nothing AndAlso TxtMultiSelectFinder1.arrValueMember.Count > 0 Then
                CNFDistributor += " and TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code  in (" + clsCommon.GetMulcallString(TxtMultiSelectFinder1.arrValueMember) + ") "
            End If

            If TxtMultiSelectFinder2.arrValueMember IsNot Nothing AndAlso TxtMultiSelectFinder2.arrValueMember.Count > 0 Then
                CNFDistributor += " and TSPL_BOOKING_DETAIL.Loc_Code  in (" + clsCommon.GetMulcallString(TxtMultiSelectFinder2.arrValueMember) + ") "
            End If

            qry = " select CNF_Code,CNF_Name,Distributor_Code,Loc_Code,Distributor_Name,Location_Desc, " & strCustCodeString & " , (" & strtotalShortTotal & ") as Total from (  " & _
                  " select TSPL_BOOKING_DETAIL.Cust_Code as CNF_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as CNF_Name, TSPL_BOOKING_MATSER.Distributor_Code  , max(TSPL_SECONDARY_CUSTOMER_MASTER.Customer_Name) as Distributor_Name , max( TSPL_ITEM_MASTER.Item_Desc) as Item_Desc  ,TSPL_BOOKING_DETAIL.Loc_Code,max (TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc, sum (TSPL_BOOKING_DETAIL.Booking_Qty ) as Qty  from TSPL_BOOKING_DETAIL LEFT OUTER JOIN TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No " & _
                  " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code  =TSPL_BOOKING_DETAIL.Loc_Code " & _
                  " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code " & _
                  " left outer join TSPL_SECONDARY_CUSTOMER_MASTER on TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_MATSER.Distributor_Code " & _
                  " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " & _
                  " where TSPL_BOOKING_MATSER.Posted =" & status & " and TSPL_BOOKING_DETAIL.Unit_code= 'CRATE' and convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103)   and  convert(date,TSPL_BOOKING_MATSER.Document_Date,103)<= convert(date,'" + ToDate.Value + "',103)  " + CNFDistributor + "  group by TSPL_BOOKING_DETAIL.Cust_Code ,TSPL_BOOKING_MATSER.Distributor_Code,TSPL_BOOKING_DETAIL.Item_Code,Loc_Code )  aaa " & _
                  " pivot  (sum (aaa.Qty) for Item_Desc in (" & strtotalShort & ")) as ItemName "


            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = clsDBFuncationality.GetDataTable(qry)


            FormatGrid()
            'Gv1.BestFitColumns()
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns(2).IsPinned = True
            Gv1.Columns(3).IsPinned = True
            Gv1.Columns(4).IsPinned = True
            Gv1.Columns(5).IsPinned = True


            RadPageView1.SelectedPage = RadPageViewPage2
            btnGo.Enabled = False
            btnReset.Enabled = True
            RadSplitButton1.Enabled = True
            Gv1.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub loaddataGHO()
        If clsCommon.CompairString(ddlReporType.Text, "Dairy Booking") = CompairStringResult.Equal Then
            LoaddataDairyBookingGHO()
        ElseIf clsCommon.CompairString(ddlReporType.Text, "Dairy Delivery Order") = CompairStringResult.Equal Then
            LoaddataDairyDeliveryGHO()
        ElseIf clsCommon.CompairString(ddlReporType.Text, "Dairy Dispatch") = CompairStringResult.Equal Then
            LoaddataDairyDispatchGHO()
        ElseIf clsCommon.CompairString(ddlReporType.Text, "Dairy Sale Invoice") = CompairStringResult.Equal Then
            LoaddataDairySaleInvoiceGHO()
        End If
    End Sub

    Sub LoaddataDairyBookingGHO()
        Try
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim strshortDesp As String = String.Empty
            Dim strItemCode As String = String.Empty
            Dim strtotalShort As String = String.Empty
            Dim strtotalShortTotal As String = String.Empty
            Dim strCustCodeString As String = ""
            Dim status As Integer = 1
            Dim Scheme As String = Nothing
            If RadioButton1.Checked = True Then
                status = 1
            End If
            If RadioButton2.Checked = True Then
                status = 0
            End If
            If RadioButton3.Checked = True Then
                status = 5
            End If


            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim dtqry As DataTable = Nothing
            Dim WhrCls As String = " and 2=2 "
            qry = " select  distinct TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc   from TSPL_BOOKING_DETAIL " & _
                 " left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No " & _
                 " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_BOOKING_DETAIL.Item_Code " & _
                " where 2=2 and TSPL_BOOKING_MATSER.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   and  TSPL_BOOKING_MATSER.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
            'qry = " select  case when len(tspl_item_master.Short_Description) <=0 then tspl_item_master.Item_Desc else tspl_item_master.Short_Description end  as Item_Code  from tspl_item_master "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display")
                Exit Sub
            End If
            For Each dr As DataRow In dt.Rows
                strshortDesp = clsCommon.myCstr(dr("Item_Desc"))
                strItemCode = clsCommon.myCstr(dr("Item_Code"))
                If clsCommon.myLen(strshortDesp) > 0 Then
                    strtotalShort = strtotalShort + "," + "[" + strItemCode + " - " + strshortDesp + "]" + "," + "[" + strItemCode + " - " + strshortDesp + " (Scheme)" + "]"
                    strCustCodeString = strCustCodeString + "," + "  isnull([" & strItemCode & " - " & strshortDesp & "],0)  " & "as  " & "[" & strItemCode & " - " & strshortDesp & "]" + "," + "  isnull([" & strItemCode & " - " & strshortDesp + " (Scheme)" & "],0)  " & "as  " & "[" & strItemCode & " - " & strshortDesp & " (Scheme)" & "]" & ""
                    strtotalShortTotal = strtotalShortTotal + "+" + "  isnull([" & strItemCode & " - " & strshortDesp & "],0)  " + "+" + "  isnull([" & strItemCode & " - " & strshortDesp + " (Scheme)" & "],0)  " & "  "
                End If
            Next
            If strtotalShort.Substring(0, 1) = "," Then
                strtotalShort = strtotalShort.Substring(1, strtotalShort.Length - 1)
                strCustCodeString = strCustCodeString.Substring(1, strCustCodeString.Length - 1)

            End If


            ' remove + 
            strtotalShortTotal = strtotalShortTotal.Remove(0, 1)


            If rdbSchemeYes.Checked = True Then
                WhrCls = " and TSPL_BOOKING_DETAIL.scheme_item  = 'Y "
            ElseIf rdbNo.Checked Then
                WhrCls = " and TSPL_BOOKING_DETAIL.scheme_item  = 'N' "
            End If


            If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtMultCustomer.arrValueMember) + ") "
            End If

            If txtMultSalesMan.arrValueMember IsNot Nothing AndAlso txtMultSalesMan.arrValueMember.Count > 0 Then
                WhrCls += " and tspl_customer_master.salesman_code  in (" + clsCommon.GetMulcallString(txtMultSalesMan.arrValueMember) + ") "
            End If

            If txtMultCustomerGroup.arrValueMember IsNot Nothing AndAlso txtMultCustomerGroup.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code  in (" + clsCommon.GetMulcallString(txtMultCustomerGroup.arrValueMember) + ") "
            End If

            If txtMultRoute.arrValueMember IsNot Nothing AndAlso txtMultRoute.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_ROUTE_MASTER.Route_No  in (" + clsCommon.GetMulcallString(txtMultRoute.arrValueMember) + ") "
            End If

            If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_BOOKING_DETAIL.Loc_Code  in (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
            End If

            qry = " select Document_No AS [Booking No],Cust_Group_Code as [Cust Group Code] ,Cust_Group_Desc as [Cust Group Desc],CNF_Code as [Cust Code],CNF_Name as [Cust Name],Route_No as [Route Code],Route_Desc as [Route Name],Emp_Name as [Salesman],Loc_Code as [Loc Code],Location_Desc as [Loc Desc], " & strCustCodeString & " , (" & strtotalShortTotal & ") as Total from (  " & _
                  " select TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Cust_Code as CNF_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as CNF_Name,max(tspl_employee_master.Emp_Name) as Emp_Name, TSPL_ROUTE_MASTER.Route_No ,max(TSPL_ROUTE_MASTER.Route_Desc) as Route_Desc , max(TSPL_ITEM_MASTER.Item_Code  + ' - ' + TSPL_ITEM_MASTER.Item_Desc) as Item_Desc  ,TSPL_BOOKING_DETAIL.Loc_Code,max (TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc, sum (TSPL_BOOKING_DETAIL.Booking_Qty ) as Qty ,TSPL_BOOKING_MATSER.Cust_Group_Code,max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc from TSPL_BOOKING_DETAIL LEFT OUTER JOIN TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No " & _
                  " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code  =TSPL_BOOKING_DETAIL.Loc_Code " & _
                  " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code " & _
                  " left outer join TSPL_SECONDARY_CUSTOMER_MASTER on TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_MATSER.Distributor_Code " & _
                  " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " & _
                  " left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_BOOKING_MATSER.Cust_Group_Code" & _
                  " left join tspl_route_master on tspl_route_master.route_no= TSPL_CUSTOMER_MASTER.Route_No " & _
                  " left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_CUSTOMER_MASTER.salesman_code and Emp_type ='Salesman'" & _
                  " where TSPL_BOOKING_MATSER.Posted =" & status & "  and isnull(TSPL_BOOKING_DETAIL.Scheme_Item,'N')='N' and TSPL_BOOKING_MATSER.Document_Date>='" + clsCommon.GetPrintDate((fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   and  TSPL_BOOKING_MATSER.Document_Date <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy hh:mm tt") + "'  " + WhrCls + "  group by TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Cust_Code ,TSPL_BOOKING_MATSER.Distributor_Code,TSPL_BOOKING_DETAIL.Item_Code,Loc_Code,TSPL_BOOKING_MATSER.Cust_Group_Code,TSPL_ROUTE_MASTER.Route_No " & _
                  " union all " & _
                  " select TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Cust_Code as CNF_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as CNF_Name,max(tspl_employee_master.Emp_Name) as Emp_Name, TSPL_ROUTE_MASTER.Route_No ,max(TSPL_ROUTE_MASTER.Route_Desc) as Route_Desc , max(TSPL_ITEM_MASTER.Item_Code  + ' - ' + TSPL_ITEM_MASTER.Item_Desc)+' (Scheme)' as Item_Desc  ,TSPL_BOOKING_DETAIL.Loc_Code,max (TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc, sum (TSPL_BOOKING_DETAIL.Booking_Qty ) as Qty ,TSPL_BOOKING_MATSER.Cust_Group_Code,max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc from TSPL_BOOKING_DETAIL LEFT OUTER JOIN TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No " & _
                  " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code  =TSPL_BOOKING_DETAIL.Loc_Code " & _
                  " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code " & _
                  " left outer join TSPL_SECONDARY_CUSTOMER_MASTER on TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_MATSER.Distributor_Code " & _
                  " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " & _
                  " left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_BOOKING_MATSER.Cust_Group_Code" & _
                  " left join tspl_route_master on tspl_route_master.route_no= TSPL_CUSTOMER_MASTER.Route_No " & _
                  " left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_CUSTOMER_MASTER.salesman_code and Emp_type ='Salesman'" & _
                  " where TSPL_BOOKING_MATSER.Posted =" & status & " AND isnull(TSPL_BOOKING_DETAIL.Scheme_Item,'N')='Y' and TSPL_BOOKING_MATSER.Document_Date>='" + clsCommon.GetPrintDate((fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   and  TSPL_BOOKING_MATSER.Document_Date <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy hh:mm tt") + "'  " + WhrCls + "  group by TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Cust_Code ,TSPL_BOOKING_MATSER.Distributor_Code,TSPL_BOOKING_DETAIL.Item_Code,Loc_Code,TSPL_BOOKING_MATSER.Cust_Group_Code,TSPL_ROUTE_MASTER.Route_No " & _
                  " )  aaa " & _
                  " pivot  (sum (aaa.Qty) for Item_Desc in (" & strtotalShort & ")) as ItemName "

            dtqry = clsDBFuncationality.GetDataTable(qry)
            If dtqry Is Nothing OrElse dtqry.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display")
                Exit Sub
            End If
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtqry
            


            FormatGridUDL()
            'Gv1.BestFitColumns()
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns(2).IsPinned = True
            Gv1.Columns(3).IsPinned = True
            Gv1.Columns(4).IsPinned = True
            Gv1.Columns(5).IsPinned = True
            Gv1.Columns(6).IsPinned = True
            Gv1.Columns(7).IsPinned = True
            Gv1.Columns(8).IsPinned = True
            Gv1.Columns(9).IsPinned = True

            RadPageView1.SelectedPage = RadPageViewPage2
            btnGo.Enabled = False
            btnReset.Enabled = True
            RadSplitButton1.Enabled = True
            Gv1.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub LoaddataDairyDeliveryGHO()
        Try
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim strshortDesp As String = String.Empty
            Dim strItemCode As String = String.Empty
            Dim strtotalShort As String = String.Empty
            Dim strtotalShortTotal As String = String.Empty
            Dim strCustCodeString As String = ""
            Dim status As Integer = 1
            Dim Scheme As String = Nothing
            If RadioButton1.Checked = True Then
                status = 1
            End If
            If RadioButton2.Checked = True Then
                status = 0
            End If
           

            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim dtqry As DataTable = Nothing
            Dim WhrCls As String = " and 2=2 "
            qry = " select  distinct TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc   from TSPL_DELIVERY_NOTE_detail_FRESHSALE " & _
                 " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_DELIVERY_NOTE_detail_FRESHSALE.Document_No " & _
                 " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_DELIVERY_NOTE_detail_FRESHSALE.Item_Code " & _
                " where 2=2 and convert(date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date ,103)>=convert(date,'" & fromDate.Value & "',103)   and  convert(date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date,103)<= convert(date,'" & ToDate.Value & "',103)"
            'qry = " select  case when len(tspl_item_master.Short_Description) <=0 then tspl_item_master.Item_Desc else tspl_item_master.Short_Description end  as Item_Code  from tspl_item_master "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display")
                Exit Sub
            End If
            For Each dr As DataRow In dt.Rows
                strshortDesp = clsCommon.myCstr(dr("Item_Desc"))
                strItemCode = clsCommon.myCstr(dr("Item_Code"))
                If clsCommon.myLen(strshortDesp) > 0 Then
                    strtotalShort = strtotalShort + "," + "[" + strItemCode + " - " + strshortDesp + "]"
                    strCustCodeString = strCustCodeString + "," + "  isnull([" & strItemCode + " - " + strshortDesp & "],0)  " & "as  " & "[" & strItemCode + " - " + strshortDesp & "]" & ""
                    strtotalShortTotal = strtotalShortTotal + "+" + "  isnull([" & strItemCode + " - " + strshortDesp & "],0)  " & "  "
                End If
            Next
            If strtotalShort.Substring(0, 1) = "," Then
                strtotalShort = strtotalShort.Substring(1, strtotalShort.Length - 1)
                strCustCodeString = strCustCodeString.Substring(1, strCustCodeString.Length - 1)

            End If


            ' remove + 
            strtotalShortTotal = strtotalShortTotal.Remove(0, 1)


            If rdbSchemeYes.Checked = True Then
                WhrCls = " and TSPL_DELIVERY_NOTE_detail_FRESHSALE.scheme_item  = 'Y "
            ElseIf rdbNo.Checked Then
                WhrCls = " and TSPL_DELIVERY_NOTE_detail_FRESHSALE.scheme_item  = 'N' "
            End If


            If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtMultCustomer.arrValueMember) + ") "
            End If

            If txtMultSalesMan.arrValueMember IsNot Nothing AndAlso txtMultSalesMan.arrValueMember.Count > 0 Then
                WhrCls += " and tspl_customer_master.salesman_code  in (" + clsCommon.GetMulcallString(txtMultSalesMan.arrValueMember) + ") "
            End If

            If txtMultCustomerGroup.arrValueMember IsNot Nothing AndAlso txtMultCustomerGroup.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code  in (" + clsCommon.GetMulcallString(txtMultCustomerGroup.arrValueMember) + ") "
            End If

            If txtMultRoute.arrValueMember IsNot Nothing AndAlso txtMultRoute.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_ROUTE_MASTER.Route_No  in (" + clsCommon.GetMulcallString(txtMultRoute.arrValueMember) + ") "
            End If

            If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code  in (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
            End If

            qry = " select Document_No AS [Delivery Order No],Cust_Group_Code as [Cust Group Code] ,Cust_Group_Desc as [Cust Group Desc],CNF_Code as [Cust Code],CNF_Name as [Cust Name],Route_No as [Route Code],Route_Desc as [Route Name],Emp_Name as [Salesman],Location_Code as [Loc Code],Location_Desc as [Loc Desc], " & strCustCodeString & " , (" & strtotalShortTotal & ") as Total from (  " & _
                    " select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code ,max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc , TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code  as CNF_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as CNF_Name, tspl_route_master.route_no,max(tspl_employee_master.Emp_Name) as Emp_Name,max(tspl_route_master.route_desc) as route_desc," & _
                    "  max(TSPL_ITEM_MASTER.Item_Code  + ' - ' + TSPL_ITEM_MASTER.Item_Desc) as Item_Desc  ,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code ,max (TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc, sum (TSPL_DELIVERY_NOTE_detail_FRESHSALE.Qty  ) as Qty  from TSPL_DELIVERY_NOTE_detail_FRESHSALE " & _
                    " LEFT OUTER JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_DELIVERY_NOTE_detail_FRESHSALE.Document_No  " & _
                   " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code  =TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code " & _
                   "  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code   " & _
                   " left join tspl_route_master on tspl_route_master.route_no= TSPL_CUSTOMER_MASTER.Route_No " & _
                   " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DELIVERY_NOTE_detail_FRESHSALE.Item_Code " & _
                   " left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code " & _
                    " left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_CUSTOMER_MASTER.salesman_code and Emp_type ='Salesman'" & _
                   " where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted =" & status & "  and TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date >='" & clsCommon.GetPrintDate((fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "'   and  TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date <= '" & clsCommon.GetPrintDate((ToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'   " & _
                   " " + WhrCls + "  group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code  ," & _
                     " TSPL_DELIVERY_NOTE_detail_FRESHSALE.Item_Code, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code, tspl_route_master.route_no  )  aaa  " & _
                    " pivot  (sum (aaa.Qty) for Item_Desc in (" & strtotalShort & ")) as ItemName "

            dtqry = clsDBFuncationality.GetDataTable(qry)
            If dtqry Is Nothing OrElse dtqry.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display")
                Exit Sub
            End If
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtqry


            FormatGridUDL()
                'Gv1.BestFitColumns()
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns(2).IsPinned = True
            Gv1.Columns(3).IsPinned = True
            Gv1.Columns(4).IsPinned = True
            Gv1.Columns(5).IsPinned = True
            Gv1.Columns(6).IsPinned = True
            Gv1.Columns(7).IsPinned = True
            Gv1.Columns(8).IsPinned = True
            Gv1.Columns(9).IsPinned = True

            RadPageView1.SelectedPage = RadPageViewPage2
            btnGo.Enabled = False
            btnReset.Enabled = True
            RadSplitButton1.Enabled = True
            Gv1.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Sub LoaddataDairyDispatchGHO()
        Try
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim strshortDesp As String = String.Empty
            Dim strItemCode As String = String.Empty
            Dim strtotalShort As String = String.Empty
            Dim strtotalShortTotal As String = String.Empty
            Dim strCustCodeString As String = ""
            Dim status As Integer = 1
            Dim Scheme As String = Nothing
            If RadioButton1.Checked = True Then
                status = 1
            End If
            If RadioButton2.Checked = True Then
                status = 0
            End If


            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim dtqry As DataTable = Nothing
            Dim WhrCls As String = " and 2=2 "
            qry = " select  distinct TSPL_ITEM_MASTER.Item_Code ,TSPL_ITEM_MASTER.Item_Desc   from TSPL_SD_SHIPMENT_DETAIL " & _
                 " left join TSPL_SD_SHIPMENT_head on TSPL_SD_SHIPMENT_head.document_code =TSPL_SD_SHIPMENT_DETAIL.document_code " & _
                 " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SHIPMENT_DETAIL.Item_Code " & _
                " where 2=2 and convert(date,TSPL_SD_SHIPMENT_head.Document_Date ,103)>=convert(date,'" & fromDate.Value & "',103)   and  convert(date,TSPL_SD_SHIPMENT_head.Document_Date,103)<= convert(date,'" & ToDate.Value & "',103)"
            'qry = " select  case when len(tspl_item_master.Short_Description) <=0 then tspl_item_master.Item_Desc else tspl_item_master.Short_Description end  as Item_Code  from tspl_item_master "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display")
                Exit Sub
            End If
            For Each dr As DataRow In dt.Rows
                strshortDesp = clsCommon.myCstr(dr("Item_Desc"))
                strItemCode = clsCommon.myCstr(dr("Item_Code"))
                If clsCommon.myLen(strshortDesp) > 0 Then
                    strtotalShort = strtotalShort + "," + "[" + strItemCode + " - " + strshortDesp + "]"
                    strCustCodeString = strCustCodeString + "," + "  isnull([" & strItemCode + " - " + strshortDesp & "],0)  " & "as  " & "[" & strItemCode + " - " + strshortDesp & "]" & ""
                    strtotalShortTotal = strtotalShortTotal + "+" + "  isnull([" & strItemCode + " - " + strshortDesp & "],0)  " & "  "
                End If
            Next
            If strtotalShort.Substring(0, 1) = "," Then
                strtotalShort = strtotalShort.Substring(1, strtotalShort.Length - 1)
                strCustCodeString = strCustCodeString.Substring(1, strCustCodeString.Length - 1)

            End If


            ' remove + 
            strtotalShortTotal = strtotalShortTotal.Remove(0, 1)

            If rdbSchemeYes.Checked = True Then
                WhrCls = " and TSPL_SD_SHIPMENT_DETAIL.scheme_item = 'Y "
            ElseIf rdbNo.Checked Then
                WhrCls = " and TSPL_SD_SHIPMENT_DETAIL.scheme_item = 'N' "
            End If



            If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtMultCustomer.arrValueMember) + ") "
            End If

            If txtMultSalesMan.arrValueMember IsNot Nothing AndAlso txtMultSalesMan.arrValueMember.Count > 0 Then
                WhrCls += " and tspl_customer_master.salesman_code  in (" + clsCommon.GetMulcallString(txtMultSalesMan.arrValueMember) + ") "
            End If

            If txtMultCustomerGroup.arrValueMember IsNot Nothing AndAlso txtMultCustomerGroup.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code  in (" + clsCommon.GetMulcallString(txtMultCustomerGroup.arrValueMember) + ") "
            End If

            If txtMultRoute.arrValueMember IsNot Nothing AndAlso txtMultRoute.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_ROUTE_MASTER.Route_No  in (" + clsCommon.GetMulcallString(txtMultRoute.arrValueMember) + ") "
            End If

            If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_SD_SHIPMENT_head.bill_to_location  in (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
            End If

            qry = " select Document_Code AS [Dispatch No],Cust_Group_Code as [Cust Group Code] ,Cust_Group_Desc as [Cust Group Desc],CNF_Code as [Cust Code],CNF_Name as [Cust Name],Route_No as [Route Code],Route_Desc as [Route Name],Emp_Name as [Salesman],Location_Code as [Loc Code],Location_Desc as [Loc Desc], " & strCustCodeString & " , (" & strtotalShortTotal & ") as Total from (  " & _
                    " select TSPL_SD_SHIPMENT_head.Document_Code,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code ,max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc , " & _
                    " TSPL_SD_SHIPMENT_head.Customer_Code  as CNF_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as CNF_Name, tspl_route_master.route_no,max(tspl_employee_master.Emp_Name) as Emp_Name,max(tspl_route_master.route_desc) as route_desc," & _
                    "  max(TSPL_ITEM_MASTER.Item_Code  + ' - ' + TSPL_ITEM_MASTER.Item_Desc) as Item_Desc  ,TSPL_SD_SHIPMENT_head.bill_to_location as Location_Code ,max (TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc," & _
                    "  sum (TSPL_SD_SHIPMENT_DETAIL.Qty  ) as Qty " & _
                    " from TSPL_SD_SHIPMENT_DETAIL " & _
                    " LEFT OUTER JOIN TSPL_SD_SHIPMENT_head on TSPL_SD_SHIPMENT_head.document_code = TSPL_SD_SHIPMENT_DETAIL.document_code   " & _
                    " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code  =TSPL_SD_SHIPMENT_head.bill_to_location  " & _
                    " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_head.Customer_Code   " & _
                    " left join tspl_route_master on tspl_route_master.route_no= TSPL_CUSTOMER_MASTER.Route_No  " & _
                    " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code  " & _
                    " left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code  " & _
                      " left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_CUSTOMER_MASTER.salesman_code and Emp_type ='Salesman'" & _
                    " where TSPL_SD_SHIPMENT_head.status =" & status & "  and TSPL_SD_SHIPMENT_head.trans_type='PS' and TSPL_SD_SHIPMENT_head.Document_Date >='" & clsCommon.GetPrintDate((fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "'   and  TSPL_SD_SHIPMENT_head.Document_Date <= '" & clsCommon.GetPrintDate((ToDate.Value), "dd/MMM/yyyy hh:mm tt") & "'    " + WhrCls + "   group by TSPL_SD_SHIPMENT_head.Document_Code,TSPL_SD_SHIPMENT_head.Customer_Code  , TSPL_SD_SHIPMENT_DETAIL.Item_Code, TSPL_SD_SHIPMENT_head.bill_to_location, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code, tspl_route_master.route_no  )  aaa" & _
                    " pivot  (sum (aaa.Qty) for Item_Desc in (" & strtotalShort & ")) as ItemName "

            dtqry = clsDBFuncationality.GetDataTable(qry)
            If dtqry Is Nothing OrElse dtqry.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display")
                Exit Sub
            End If
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtqry


            FormatGridUDL()
            'Gv1.BestFitColumns()
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns(2).IsPinned = True
            Gv1.Columns(3).IsPinned = True
            Gv1.Columns(4).IsPinned = True
            Gv1.Columns(5).IsPinned = True
            Gv1.Columns(6).IsPinned = True
            Gv1.Columns(7).IsPinned = True
            Gv1.Columns(8).IsPinned = True
            Gv1.Columns(9).IsPinned = True

            RadPageView1.SelectedPage = RadPageViewPage2
            btnGo.Enabled = False
            btnReset.Enabled = True
            RadSplitButton1.Enabled = True
            Gv1.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Sub LoaddataDairySaleInvoiceGHO()
        Try
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim strshortDesp As String = String.Empty
            Dim strItemCode As String = String.Empty
            Dim strtotalShort As String = String.Empty
            Dim strtotalShortTotal As String = String.Empty
            Dim strCustCodeString As String = ""
            Dim Scheme As String = Nothing
            Dim status As Integer = 1
            If RadioButton1.Checked = True Then
                status = 1
            End If
            If RadioButton2.Checked = True Then
                status = 0
            End If
            
            Dim dtqry As DataTable = Nothing
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim WhrCls As String = " and 2=2 "
            qry = " select  distinct TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc   from TSPL_SD_SALE_INVOICE_DETAIL " & _
                 " left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code =TSPL_SD_SALE_INVOICE_DETAIL.document_code " & _
                 " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
                " where 2=2 and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=convert(date,'" & fromDate.Value & "',103)   and  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,'" & ToDate.Value & "',103)"
            'qry = " select  case when len(tspl_item_master.Short_Description) <=0 then tspl_item_master.Item_Desc else tspl_item_master.Short_Description end  as Item_Code  from tspl_item_master "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display")
                Exit Sub
            End If
            For Each dr As DataRow In dt.Rows
                strshortDesp = clsCommon.myCstr(dr("Item_Desc"))
                strItemCode = clsCommon.myCstr(dr("Item_Code"))
                If clsCommon.myLen(strshortDesp) > 0 Then
                    strtotalShort = strtotalShort + "," + "[" + strItemCode + " - " + strshortDesp + "]"
                    strCustCodeString = strCustCodeString + "," + "  isnull([" & strItemCode + " - " + strshortDesp & "],0)  " & "as  " & "[" & strItemCode + " - " + strshortDesp & "]" & ""
                    strtotalShortTotal = strtotalShortTotal + "+" + "  isnull([" & strItemCode + " - " + strshortDesp & "],0)  " & "  "
                End If
            Next
            If strtotalShort.Substring(0, 1) = "," Then
                strtotalShort = strtotalShort.Substring(1, strtotalShort.Length - 1)
                strCustCodeString = strCustCodeString.Substring(1, strCustCodeString.Length - 1)

            End If


            ' remove + 
            strtotalShortTotal = strtotalShortTotal.Remove(0, 1)


            If rdbSchemeYes.Checked = True Then
                WhrCls = " and TSPL_SD_SALE_INVOICE_DETAIL.scheme_item = 'Y' "
            ElseIf rdbNo.Checked Then
                WhrCls = " and TSPL_SD_SALE_INVOICE_DETAIL.scheme_item = 'N' "
            End If


            If txtMultCustomer.arrValueMember IsNot Nothing AndAlso txtMultCustomer.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtMultCustomer.arrValueMember) + ") "
            End If

            If txtMultSalesMan.arrValueMember IsNot Nothing AndAlso txtMultSalesMan.arrValueMember.Count > 0 Then
                WhrCls += " and tspl_customer_master.salesman_code  in (" + clsCommon.GetMulcallString(txtMultSalesMan.arrValueMember) + ") "
            End If

            If txtMultCustomerGroup.arrValueMember IsNot Nothing AndAlso txtMultCustomerGroup.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code  in (" + clsCommon.GetMulcallString(txtMultCustomerGroup.arrValueMember) + ") "
            End If

            If txtMultRoute.arrValueMember IsNot Nothing AndAlso txtMultRoute.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_ROUTE_MASTER.Route_No  in (" + clsCommon.GetMulcallString(txtMultRoute.arrValueMember) + ") "
            End If

            If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                WhrCls += " and TSPL_SD_SALE_INVOICE_HEAD.bill_to_location  in (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
            End If

            qry = " select Document_Code AS [Invoice No],Cust_Group_Code as [Cust Group Code] ,Cust_Group_Desc as [Cust Group Desc],CNF_Code as [Cust Code],CNF_Name as [Cust Name],Route_No as [Route Code],Route_Desc as [Route Name],Emp_Name as [Salesman],Location_Code as [Loc Code],Location_Desc as [Loc Desc], " & strCustCodeString & " , (" & strtotalShortTotal & ") as Total from (  " & _
                  " select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code ,max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group_Desc ,  TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  as CNF_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as CNF_Name, tspl_route_master.route_no,max(tspl_employee_master.Emp_Name) as Emp_Name,max(tspl_route_master.route_desc) as route_desc,  max(TSPL_ITEM_MASTER.Item_Code  + ' - ' + TSPL_ITEM_MASTER.Item_Desc) as Item_Desc  ,TSPL_SD_SALE_INVOICE_HEAD.bill_to_location as Location_Code ,max (TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc,  sum (TSPL_SD_SALE_INVOICE_DETAIL.Qty  ) as Qty  " & _
                  " from TSPL_SD_SALE_INVOICE_DETAIL " & _
                    " LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code = TSPL_SD_SALE_INVOICE_DETAIL.document_code " & _
                    " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code  =TSPL_SD_SALE_INVOICE_HEAD.bill_to_location   " & _
                     " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  " & _
                    " left join tspl_route_master on tspl_route_master.route_no= TSPL_CUSTOMER_MASTER.Route_No   " & _
                    " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  " & _
                    " left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code  " & _
                    " left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_CUSTOMER_MASTER.salesman_code and Emp_type ='Salesman'" & _
                    " where TSPL_SD_SALE_INVOICE_HEAD.status =" & status & "   and TSPL_SD_SALE_INVOICE_HEAD.trans_type='PS' and TSPL_SD_SALE_INVOICE_HEAD.Document_Date  >= '" & clsCommon.GetPrintDate((fromDate.Value), "dd/MMM/yyyy hh:mm tt") & "'   and  TSPL_SD_SALE_INVOICE_HEAD.Document_Date <= '" + clsCommon.GetPrintDate((ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   " + WhrCls + "   group by TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, TSPL_SD_SALE_INVOICE_HEAD.bill_to_location, TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code, tspl_route_master.route_no  )  aaa " & _
                    " pivot  (sum (aaa.Qty) for Item_Desc in (" & strtotalShort & ")) as ItemName "

            dtqry = clsDBFuncationality.GetDataTable(qry)
            If dtqry Is Nothing OrElse dtqry.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display")
                Exit Sub
            End If
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtqry


            FormatGridUDL()
            'Gv1.BestFitColumns()
            Gv1.Columns(0).IsPinned = True
            Gv1.Columns(1).IsPinned = True
            Gv1.Columns(2).IsPinned = True
            Gv1.Columns(3).IsPinned = True
            Gv1.Columns(4).IsPinned = True
            Gv1.Columns(5).IsPinned = True
            Gv1.Columns(6).IsPinned = True
            Gv1.Columns(7).IsPinned = True
            Gv1.Columns(8).IsPinned = True
            Gv1.Columns(9).IsPinned = True

            RadPageView1.SelectedPage = RadPageViewPage2

            btnGo.Enabled = False
            btnReset.Enabled = True
            RadSplitButton1.Enabled = True
            Gv1.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Private Sub txtMultCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultCustomer._My_Click
        strQry = " select Cust_Code as [Code] , Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
        txtMultCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultCustomer", strQry, "Code", "Name", txtMultCustomer.arrValueMember, txtMultCustomer.arrDispalyMember)
    End Sub

    Private Sub txtMultLocation__My_Click(sender As Object, e As EventArgs) Handles txtMultLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtMultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultLocation", strQry, "Code", "Name", txtMultLocation.arrValueMember, txtMultLocation.arrDispalyMember)
    End Sub

    Private Sub txtMultSalesMan__My_Click(sender As Object, e As EventArgs) Handles txtMultSalesMan._My_Click
        strQry = "select TSPL_EMPLOYEE_MASTER.EMP_CODE as Code ,TSPL_EMPLOYEE_MASTER.Emp_Name as Name from TSPL_EMPLOYEE_MASTER where Emp_type ='Salesman'"
        txtMultSalesMan.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultSalesMan", strQry, "Code", "Name", txtMultSalesMan.arrValueMember, txtMultSalesMan.arrDispalyMember)
    End Sub

    Private Sub ddlReporType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlReporType.SelectedIndexChanged
        If IsFormLoad = False Then
            Exit Sub
        End If
        If ddlReporType.SelectedValue = "Dairy Booking" Then
            RadioButton3.Enabled = True
        Else
            RadioButton1.Checked = True
            RadioButton3.Enabled = False
        End If
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Dairy Booking Distributor Report")
                If RadioButton1.Checked Then
                    arrHeader.Add("Document Type : Posted ")
                End If
                If RadioButton2.Checked Then
                    arrHeader.Add("Document Type : Pending ")
                End If

                clsCommon.MyExportToPDF("Dairy Booking Distributor Report", Gv1, arrHeader, "Dairy Booking Distributor Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
