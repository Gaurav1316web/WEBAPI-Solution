Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'======================Created By preeti Gupta against ticket no [BM00000009857]=========================
Public Class RptDemandForSingleBranch
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptPlantCustomerDemand)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        btnGo.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub RptDemandForSingleBranch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+R Adding New")
    End Sub

    Private Sub MultLocation__My_Click(sender As Object, e As EventArgs) Handles MultLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.type='Depot'"
        MultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Loc", qry, "Code", "Name", MultLocation.arrValueMember, MultLocation.arrDispalyMember)
    End Sub

    Private Sub MultCustomer__My_Click(sender As Object, e As EventArgs) Handles MultCustomer._My_Click
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_master order by Cust_Code"
        MultCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust", qry, "Code", "Name", MultCustomer.arrValueMember, MultCustomer.arrDispalyMember)
    End Sub

    Private Sub MultVehicle__My_Click(sender As Object, e As EventArgs) Handles MultVehicle._My_Click
        Dim qry As String = "select Vehicle_Id as Code ,Description as Name  from TSPL_VEHICLE_MASTER "
        MultVehicle.arrValueMember = clsCommon.ShowMultipleSelectForm("VEHICLE", qry, "Code", "Name", MultVehicle.arrValueMember, MultVehicle.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        LoadDataForPivot()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(MultLocation.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If
            If MultCustomer.arrValueMember IsNot Nothing AndAlso MultCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(MultCustomer.arrValueMember))
            Else
                arrHeader.Add((" Customer: All"))
            End If
            If MultVehicle.arrValueMember IsNot Nothing AndAlso MultVehicle.arrValueMember.Count > 0 Then
                arrHeader.Add("Vehicle : " + clsCommon.GetMulcallStringWithComma(MultVehicle.arrValueMember))
            Else
                arrHeader.Add(("Vehicle : All"))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Plant Customer Demand Report", gv2, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Plant Customer Demand Report", gv2, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
        rdbCustomer.IsChecked = True
        gv2.DataSource = Nothing
        MultVehicle.arrValueMember = Nothing
        MultCustomer.arrValueMember = Nothing
        MultLocation.arrValueMember = Nothing
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub LoadDataForPivot()
        Dim dt As DataTable
        Dim finalQuery As String = Nothing
        '=============================================pivot variables================================================
        Dim strPivotForInternal As String = Nothing
        Dim strPivotForOuter As String = Nothing
        Dim StrPivotForLastDay As String = Nothing
        Dim StrPivotForLastSumDay As String = Nothing
        Dim strPivotForInternalquery As String = Nothing
        Dim strPivotForOuterquery As String = Nothing
        Dim StrPivotForLastDayquery As String = Nothing
        Dim StrPivotForLastDaySumquery As String = Nothing
        Dim StrDateDiffQuery As String = Nothing
        Dim StrDateDiff As String = Nothing
        Dim StrToatlQuery As String = Nothing
        Dim StrToatl As String = Nothing
        Dim StrToatlSumQuery As String = Nothing
        Dim StrToatlsum As String = Nothing
        Dim strPivotForInternalqueryVehicle As String = Nothing
        Dim strPivotForInternalVehicle As String = Nothing
        Dim strPivotForOuterqueryVehicle As String = Nothing
        Dim strPivotForOutervehicle As String = Nothing
        Dim dtExtraColumnQry As String = Nothing
        Dim dtExtraColumn As DataTable = Nothing

        ' KUNAL > TICKET : BM00000010061 > DATE : 22 -OCT -2016
        dtExtraColumnQry = "select distinct   tspl_vehicle_master.Number+'%#%'+TSPL_CUSTOMER_MASTER.Customer_Name  from TSPL_BOOKING_DETAIL left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No  left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_BOOKING_MATSER.Location_code  left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code  left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_BOOKING_DETAIL.Item_Code  left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =TSPL_CUSTOMER_MASTER.Route_No   left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.vehicle_code  "
        dtExtraColumnQry += " where 2=2  and  convert(date,TSPL_BOOKING_MATSER.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and TSPL_LOCATION_MASTER.type='Depot'"
        If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
            dtExtraColumnQry += " and TSPL_BOOKING_MATSER. Location_Code   IN (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ") "
        End If
        If MultCustomer.arrValueMember IsNot Nothing AndAlso MultCustomer.arrValueMember.Count > 0 Then
            dtExtraColumnQry += "and TSPL_BOOKING_DETAIL.Cust_Code  in (" + clsCommon.GetMulcallString(MultCustomer.arrValueMember) + ") " + Environment.NewLine
        End If
        If MultVehicle.arrValueMember IsNot Nothing AndAlso MultVehicle.arrValueMember.Count > 0 Then
            dtExtraColumnQry += " and TSPL_BOOKING_DETAIL.Vehicle_Code in (" + clsCommon.GetMulcallString(MultVehicle.arrValueMember) + ") " + Environment.NewLine
        End If
        strPivotForInternalquery += " for xml path ('')) as strr)as a "
        dtExtraColumn = clsDBFuncationality.GetDataTable(dtExtraColumnQry)
        If dtExtraColumn.Rows.Count > 0 Then
            If String.IsNullOrEmpty(clsCommon.myCstr(dtExtraColumn.Rows(0)(0))) Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        End If
       

        strPivotForInternalquery = " select STUFF(a.strr,1,1,'') from (select(select distinct + ',['+ tspl_vehicle_master.Number+'%#%'+TSPL_CUSTOMER_MASTER.Customer_Name +']'from TSPL_BOOKING_DETAIL "
        strPivotForInternalquery += " left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No "
        strPivotForInternalquery += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_BOOKING_MATSER.Location_code "
        strPivotForInternalquery += " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code "
        strPivotForInternalquery += "  left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_BOOKING_DETAIL.Item_Code "
        strPivotForInternalquery += " left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =TSPL_CUSTOMER_MASTER.Route_No "
        strPivotForInternalquery += "  left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.vehicle_code"
        strPivotForInternalquery += " where 2=2  and  convert(date,TSPL_BOOKING_MATSER.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and TSPL_LOCATION_MASTER.type='Depot'"
        If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
            strPivotForInternalquery += " and TSPL_BOOKING_MATSER. Location_Code   IN (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ") "
        End If
        If MultCustomer.arrValueMember IsNot Nothing AndAlso MultCustomer.arrValueMember.Count > 0 Then
            strPivotForInternalquery += "and TSPL_BOOKING_DETAIL.Cust_Code  in (" + clsCommon.GetMulcallString(MultCustomer.arrValueMember) + ") " + Environment.NewLine
        End If
        If MultVehicle.arrValueMember IsNot Nothing AndAlso MultVehicle.arrValueMember.Count > 0 Then
            strPivotForInternalquery += " and TSPL_BOOKING_DETAIL.Vehicle_Code in (" + clsCommon.GetMulcallString(MultVehicle.arrValueMember) + ") " + Environment.NewLine
        End If
        strPivotForInternalquery += " for xml path ('')) as strr)as a "
        strPivotForInternal = clsDBFuncationality.getSingleValue(strPivotForInternalquery)

        strPivotForInternalqueryVehicle = " select STUFF(a.strr,1,1,'') from (select(select distinct + ',['+ tspl_vehicle_master.Number +']' from TSPL_BOOKING_DETAIL"
        strPivotForInternalqueryVehicle += " left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No "
        strPivotForInternalqueryVehicle += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_BOOKING_MATSER.Location_code "
        strPivotForInternalqueryVehicle += " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code "
        strPivotForInternalqueryVehicle += "  left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_BOOKING_DETAIL.Item_Code "
        strPivotForInternalqueryVehicle += " left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =TSPL_CUSTOMER_MASTER.Route_No "
        strPivotForInternalqueryVehicle += "  left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.vehicle_code "
        strPivotForInternalqueryVehicle += " where 2=2  and  convert(date,TSPL_BOOKING_MATSER.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and TSPL_LOCATION_MASTER.type='Depot'"
        If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
            strPivotForInternalqueryVehicle += " and TSPL_BOOKING_MATSER. Location_Code   IN (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ") "
        End If
        If MultCustomer.arrValueMember IsNot Nothing AndAlso MultCustomer.arrValueMember.Count > 0 Then
            strPivotForInternalqueryVehicle += "and TSPL_BOOKING_DETAIL.Cust_Code  in (" + clsCommon.GetMulcallString(MultCustomer.arrValueMember) + ") " + Environment.NewLine
        End If
        If MultVehicle.arrValueMember IsNot Nothing AndAlso MultVehicle.arrValueMember.Count > 0 Then
            strPivotForInternalqueryVehicle += "and TSPL_BOOKING_DETAIL.Vehicle_Code in (" + clsCommon.GetMulcallString(MultVehicle.arrValueMember) + ") " + Environment.NewLine
        End If
        strPivotForInternalqueryVehicle += "  for xml path ('')) as strr)as a "
        strPivotForInternalVehicle = clsDBFuncationality.getSingleValue(strPivotForInternalqueryVehicle)

        strPivotForOuterquery = " (select(select distinct + ',sum(isnull(['+ tspl_vehicle_master.Number+'%#%'+TSPL_CUSTOMER_MASTER.Customer_Name +'],0)) as '+'['+ tspl_vehicle_master.Number+'%#%'+TSPL_CUSTOMER_MASTER.Customer_Name +']' from TSPL_BOOKING_DETAIL"
        strPivotForOuterquery += " left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No "
        strPivotForOuterquery += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_BOOKING_MATSER.Location_code "
        strPivotForOuterquery += " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code "
        strPivotForOuterquery += "  left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_BOOKING_DETAIL.Item_Code "
        strPivotForOuterquery += " left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =TSPL_CUSTOMER_MASTER.Route_No "
        strPivotForOuterquery += "  left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.vehicle_code"
        strPivotForOuterquery += " where 2=2  and  convert(date,TSPL_BOOKING_MATSER.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and TSPL_LOCATION_MASTER.type='Depot'"
        If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
            strPivotForOuterquery += " and TSPL_BOOKING_MATSER. Location_Code   IN (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ") "
        End If
        If MultCustomer.arrValueMember IsNot Nothing AndAlso MultCustomer.arrValueMember.Count > 0 Then
            strPivotForOuterquery += "and TSPL_BOOKING_DETAIL.Cust_Code  in (" + clsCommon.GetMulcallString(MultCustomer.arrValueMember) + ") " + Environment.NewLine
        End If
        If MultVehicle.arrValueMember IsNot Nothing AndAlso MultVehicle.arrValueMember.Count > 0 Then
            strPivotForOuterquery += "and TSPL_BOOKING_DETAIL.Vehicle_Code in (" + clsCommon.GetMulcallString(MultVehicle.arrValueMember) + ") " + Environment.NewLine
        End If
        strPivotForOuterquery += " for xml path ('')) as strr)"
        strPivotForOuter = clsDBFuncationality.getSingleValue(strPivotForOuterquery)

        strPivotForOuterqueryVehicle = " (select(select distinct + ',sum(isnull(['+ tspl_vehicle_master.Number +'],0)) as '+'['+ tspl_vehicle_master.Number +']' from TSPL_BOOKING_DETAIL"
        strPivotForOuterqueryVehicle += " left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No "
        strPivotForOuterqueryVehicle += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_BOOKING_MATSER.Location_code "
        strPivotForOuterqueryVehicle += " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code "
        strPivotForOuterqueryVehicle += "  left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_BOOKING_DETAIL.Item_Code "
        strPivotForOuterqueryVehicle += " left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =TSPL_CUSTOMER_MASTER.Route_No "
        strPivotForOuterqueryVehicle += "  left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.vehicle_code "
        strPivotForOuterqueryVehicle += " where 2=2  and  convert(date,TSPL_BOOKING_MATSER.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and TSPL_LOCATION_MASTER.type='Depot'"
        If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
            strPivotForOuterqueryVehicle += " and TSPL_BOOKING_MATSER. Location_Code   IN (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ") "
        End If
        If MultCustomer.arrValueMember IsNot Nothing AndAlso MultCustomer.arrValueMember.Count > 0 Then
            strPivotForOuterqueryVehicle += "and TSPL_BOOKING_DETAIL.Cust_Code  in (" + clsCommon.GetMulcallString(MultCustomer.arrValueMember) + ") " + Environment.NewLine
        End If
        If MultVehicle.arrValueMember IsNot Nothing AndAlso MultVehicle.arrValueMember.Count > 0 Then
            strPivotForOuterqueryVehicle += "and TSPL_BOOKING_DETAIL.Vehicle_Code in (" + clsCommon.GetMulcallString(MultVehicle.arrValueMember) + ") " + Environment.NewLine
        End If
        strPivotForOuterqueryVehicle += " for xml path ('')) as strr)"
        strPivotForOutervehicle = clsDBFuncationality.getSingleValue(strPivotForOuterqueryVehicle)

        StrToatlQuery = "select +','+STUFF(a.strr,1,1,'') from (select(select distinct + '+sum(isnull(['+ tspl_vehicle_master.Number+'%#%'+TSPL_CUSTOMER_MASTER.Customer_Name +'],0))' from TSPL_BOOKING_DETAIL"
        StrToatlQuery += " left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No "
        StrToatlQuery += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_BOOKING_MATSER.Location_code "
        StrToatlQuery += " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code "
        StrToatlQuery += "  left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_BOOKING_DETAIL.Item_Code "
        StrToatlQuery += " left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =TSPL_CUSTOMER_MASTER.Route_No "
        StrToatlQuery += "  left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.vehicle_code "
        StrToatlQuery += " where 2=2  and  convert(date,TSPL_BOOKING_MATSER.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and TSPL_LOCATION_MASTER.type='Depot'"
        If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
            StrToatlQuery += " and TSPL_BOOKING_MATSER. Location_Code   IN (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ") "
        End If
        If MultCustomer.arrValueMember IsNot Nothing AndAlso MultCustomer.arrValueMember.Count > 0 Then
            StrToatlQuery += "and TSPL_BOOKING_DETAIL.Cust_Code  in (" + clsCommon.GetMulcallString(MultCustomer.arrValueMember) + ") " + Environment.NewLine
        End If
        If MultVehicle.arrValueMember IsNot Nothing AndAlso MultVehicle.arrValueMember.Count > 0 Then
            StrToatlQuery += "and TSPL_BOOKING_DETAIL.Vehicle_Code in (" + clsCommon.GetMulcallString(MultVehicle.arrValueMember) + ") " + Environment.NewLine
        End If
        StrToatlQuery += " for xml path ('')) as strr)as a"
        StrToatl = clsDBFuncationality.getSingleValue(StrToatlQuery)


        '================================================================end here=============================================================================
        Dim qry As String = "  select tspl_vehicle_master.Number as Vechicle_Name,TSPL_ROUTE_MASTER.Route_Desc, tspl_item_master.short_description," & _
                            " TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_MATSER.Document_Date ,TSPL_BOOKING_MATSER.Location_code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_BOOKING_DETAIL.Unit_code  ," & _
                            " TSPL_BOOKING_DETAIL.Cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Route_No ,TSPL_BOOKING_DETAIL.Vehicle_Code ,TSPL_BOOKING_DETAIL.DocumentAmount ," & _
                            " TSPL_BOOKING_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc , TSPL_BOOKING_DETAIL.Booking_Qty, tspl_vehicle_master.Number+'%#%'+TSPL_CUSTOMER_MASTER.Customer_Name  as VechTemp,0 as VehicleTotal " & _
                            " from TSPL_BOOKING_DETAIL" & _
                            " left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No " & _
                            " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_BOOKING_MATSER.Location_code " & _
                            " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code " & _
                            " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_BOOKING_DETAIL.Item_Code " & _
                            " left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =TSPL_CUSTOMER_MASTER.Route_No " & _
                            " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.vehicle_code"
        ''" left join (select sum(Booking_Qty) as Booking_Qty,Vehicle_Code   from TSPL_BOOKING_DETAIL group by Vehicle_Code,Cust_Code  ) as VehicleTotal on VehicleTotal.Vehicle_Code =TSPL_BOOKING_DETAIL.Vehicle_Code "
        qry += " where 2=2  and  convert(date,TSPL_BOOKING_MATSER.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and TSPL_LOCATION_MASTER.type='Depot'"
        If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
            qry += " and TSPL_BOOKING_MATSER. Location_Code   IN (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ") "
        End If
        If MultCustomer.arrValueMember IsNot Nothing AndAlso MultCustomer.arrValueMember.Count > 0 Then
            qry += "and TSPL_BOOKING_DETAIL.Cust_Code  in (" + clsCommon.GetMulcallString(MultCustomer.arrValueMember) + ") " + Environment.NewLine
        End If
        If MultVehicle.arrValueMember IsNot Nothing AndAlso MultVehicle.arrValueMember.Count > 0 Then
            qry += "and TSPL_BOOKING_DETAIL.Vehicle_Code in (" + clsCommon.GetMulcallString(MultVehicle.arrValueMember) + ") " + Environment.NewLine
        End If

        finalQuery = " select ROW_NUMBER() over( order by Item_Code ) as SNO , (Item_Code) as Item_Code,max(Item_Desc) as Item_Desc,max(Unit_code) as Unit_code " + strPivotForOuter + "  " + StrToatl + " as total_Qty from ("
        finalQuery += " select  Vehicle_Code,max(Vechicle_Name) as Vechicle_Name,Item_Code,max(Item_Desc) as Item_Desc,max(Unit_code) as Unit_code,Cust_Code,max(Customer_Name) as Customer_Name,sum(Booking_Qty) as Booking_Qty,max(VechTemp) as VechTemp,max(VehicleTotal) as VehicleTotal from ("
        finalQuery += "" + qry + ""
        finalQuery += " ) as pp group by Location_code , Cust_Code ,Route_No ,Vehicle_Code,Item_Code "
        finalQuery += " ) pp pivot (sum(pp.booking_qty) for VechTemp in  (" + strPivotForInternal + "))t "
        'finalQuery += " Pivot ( sum(VehicleTotal) for Vehicle_Code in (" + strPivotForInternalVehicle + "))pp"

        finalQuery += " group by t.Item_Code "

        dt = clsDBFuncationality.GetDataTable(finalQuery)
        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        gv2.DataSource = dt
        gv2.GroupDescriptors.Clear()
        gv2.MasterTemplate.SummaryRowsBottom.Clear()
        FormatGrid(dtExtraColumn)
        gv2.BestFitColumns()
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        End If
        RadPageView1.SelectedPage = RadPageViewPage2
        'ReStoreGridLayout()
    End Sub

    Dim colsName As List(Of String) = Nothing
    Sub FormatGrid(ByVal dtExtraColumn As DataTable)
        ' Dim strItemCode, head2 As String

        gv2.TableElement.TableHeaderHeight = 40
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = True
            gv2.Columns(ii).Width = 80
        Next

        'gv2.Columns("Document_No").IsVisible = False
        'gv2.Columns("Document_No").Width = 40
        'gv2.Columns("Document_No").HeaderText = "Document_No"

        gv2.Columns("SNO").IsVisible = True
        gv2.Columns("SNO").Width = 40
        gv2.Columns("SNO").HeaderText = "SNO"

        gv2.Columns("Item_Code").IsVisible = True
        gv2.Columns("Item_Code").Width = 40
        gv2.Columns("Item_Code").HeaderText = "Product Code"

        gv2.Columns("Item_Desc").IsVisible = True
        gv2.Columns("Item_Desc").Width = 40
        gv2.Columns("Item_Desc").HeaderText = "Product Name"

        gv2.Columns("Unit_code").IsVisible = True
        gv2.Columns("Unit_code").Width = 40
        gv2.Columns("Unit_code").HeaderText = "Unit Code"



        gv2.Columns("total_Qty").IsVisible = True
        gv2.Columns("total_Qty").Width = 100
        gv2.Columns("total_Qty").HeaderText = "Total Qty"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim intCount As Integer = 0
        If dtExtraColumn IsNot Nothing AndAlso dtExtraColumn.Rows.Count > 0 Then
            For Each dr As DataRow In dtExtraColumn.Rows
                Dim item1 As New GridViewSummaryItem(clsCommon.myCstr(dr(0)), "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
            Dim item2 As New GridViewSummaryItem(clsCommon.myCstr("total_Qty"), "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
        End If

        ' SPLITTING COLUMNS NAMES AND SET GRID VIEW COLUMNS CAPTIONS ================

        'colsName = New List(Of String)()
        'Dim splitColumnsName As String = Nothing
        'If dtExtraColumn IsNot Nothing AndAlso dtExtraColumn.Rows.Count > 0 Then
        '    For Each dr As DataRow In dtExtraColumn.Rows
        '        If clsCommon.myLen(dr(0)) > 0 Then
        '            splitColumnsName = dr(0)
        '            If (splitColumnsName.Contains("%#%")) Then
        '                splitColumnsName = splitColumnsName.Replace("%#%", "" + Environment.NewLine + "")
        '                If splitColumnsName IsNot Nothing AndAlso splitColumnsName.Length > 0 Then
        '                    colsName.Add(splitColumnsName)
        '                End If
        '            End If
        '        End If
        '    Next
        'End If

        'Dim counter As Integer = 0
        'Try
        '    For Each dtcol As GridViewColumn In gv2.Columns
        '        If (dtcol.Index >= ((gv2.Columns.Count - colsName.Count - 1))) Then
        '            If dtcol.Index <= colsName.Count Then
        '                dtcol.HeaderText = colsName(counter)
        '                counter = counter + 1
        '            End If
        '        End If
        '    Next
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try

        ' KUNAL > TICKET : BM00000010061 > DATE : 22 -OCT -2016
        colsName = New List(Of String)()
        Dim splitColumnsName As String = Nothing
        If dtExtraColumn IsNot Nothing AndAlso dtExtraColumn.Rows.Count > 0 Then
            For Each dr As DataRow In dtExtraColumn.Rows
                If clsCommon.myLen(dr(0)) > 0 Then
                    splitColumnsName = dr(0)
                    If (splitColumnsName.Contains("%#%")) Then
                        splitColumnsName = splitColumnsName.Replace("%#%", "" + Environment.NewLine + "")
                        If splitColumnsName IsNot Nothing AndAlso splitColumnsName.Length > 0 Then
                            colsName.Add(splitColumnsName)
                        End If
                    End If
                End If
            Next
        End If
        Dim counter As Integer = 0

        For Each dtcol As GridViewColumn In gv2.Columns
            If (dtcol.Index >= ((gv2.Columns.Count - colsName.Count - 1))) Then
                If counter < colsName.Count Then
                    dtcol.HeaderText = colsName(counter)
                End If
                counter = counter + 1
            End If

        Next



        'Dim summaryItem1 As New GridViewSummaryItem()

        ''Dim item2 As New GridViewSummaryItem("DocumentAmount", "{0:F2}", GridAggregateFunction.Sum)
        ''summaryRowItem.Add(item2)
        'Dim item3 As New GridViewSummaryItem("total_Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)
        'gv2.GroupDescriptors.Add(New GridGroupByExpression("Vehicle_Code as Item format ""{0}: {1}"" Group By Vehicle_Code"))


        gv2.ShowGroupPanel = False
        gv2.MasterTemplate.AutoExpandGroups = True

        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'gv2.MasterTemplate.ShowTotals = True
    End Sub
    Private Sub RptDemandForSingleBranch_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadDataForPivot()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()

        End If
    End Sub

  
    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
