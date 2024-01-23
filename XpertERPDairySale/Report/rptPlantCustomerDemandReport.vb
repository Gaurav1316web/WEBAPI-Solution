Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'======================Created By preeti Gupta=========================
Public Class RptPlantCustomerDemandReport
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
    Private Sub MultLocation__My_Click(sender As Object, e As EventArgs) Handles MultLocation._My_Click
        Try
            Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] ,Type from TSPL_LOCATION_MASTER where Type ='PLANT'"
            MultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("bank", qry, "Code", "Name", MultLocation.arrValueMember, MultLocation.arrDispalyMember)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub MultCustomer__My_Click(sender As Object, e As EventArgs) Handles MultCustomer._My_Click
        Try

            Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_master order by Cust_Code"
            MultCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust", qry, "Code", "Name", MultCustomer.arrValueMember, MultCustomer.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub RptPlantCustomerDemandReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Reset()
            SetUserMgmtNew()
            rdbAll.Visible = False
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
            ButtonToolTip.SetToolTip(BtnReset, "Press Alt+R Adding New")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ' KUNAL > TICKET : BM00000009967 : DATE 14 -OCT -2016
    Private Sub RptPlantCustomerDemandReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Try
                If rdbBranch.IsChecked Then
                    Report_BranchWiseBooking()
                ElseIf rdbCustomer.IsChecked Then
                    Report_CustomerWiseBooking()
                Else

                End If

            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()

        End If
    End Sub
    ' KUNAL > TICKET : BM00000009967 : DATE 14 -OCT -2016
    Private Sub Report_CustomerWiseBooking()

        Try
            Dim dt As DataTable
            Dim qry As String = Nothing
            Dim finalQuery As String = Nothing
            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                txtFromDate.Focus()
                Exit Sub
            End If

            ' PIVOT VARIABLES ===========================================
            Dim strPivotForInternalqueryBranch As String = Nothing
            Dim strPivotForInternalBranch As String = Nothing
            Dim strPivotForOuterqueryBranch As String = Nothing
            Dim strPivotForOuterbranch As String = Nothing
            Dim StrToatlQuerybranch As String = Nothing
            Dim StrToatlBranch As String = Nothing
            Dim StrCustFill As String = Nothing
            Dim StrVecFill As String = Nothing
            If MultCustomer.arrValueMember IsNot Nothing Then
                StrCustFill = " AND CM.Cust_Code IN (" + clsCommon.GetMulcallString(MultCustomer.arrValueMember) + ")"
            End If
            If MultVehicle.arrValueMember IsNot Nothing Then
                StrVecFill = " AND VM.Number IN (" + clsCommon.GetMulcallString(MultVehicle.arrValueMember) + ")"
            End If

            ' EXTRA COLUMNS ===========================================
            Dim branchVehiclesCols As DataTable = Nothing
            branchVehiclesCols = clsDBFuncationality.GetDataTable("select distinct  coalesce(CM.Customer_Name,'')   +'%###%'+ coalesce(vm.Number,'') FROM TSPL_BOOKING_MATSER BM  LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No  LEFT JOIN TSPL_CUSTOMER_MASTER CM ON BD.Cust_Code = CM.Cust_Code  LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code  LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id LEFT JOIN TSPL_LOCATION_MASTER LM ON BM.location_code = LM.Location_Code WHERE  BD.Cust_Code is not null and vm.Number IS NOT NULL AND BM.location_code IS NOT NULL " + StrCustFill + " " + StrVecFill + " ")

            ' PIVOTS COULMNS BY XML PATHS ===========================================
            strPivotForInternalqueryBranch = " select STUFF(a.strr,1,1,'') from (select(select distinct + ',['+ coalesce(CM.Customer_Name,'')   +'%###%'+ coalesce(vm.Number,'') +']' FROM TSPL_BOOKING_MATSER BM  LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No "
            strPivotForInternalqueryBranch += " LEFT JOIN TSPL_CUSTOMER_MASTER CM ON BD.Cust_Code = CM.Cust_Code"
            strPivotForInternalqueryBranch += " LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code"
            strPivotForInternalqueryBranch += " LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id"
            strPivotForInternalqueryBranch += " LEFT JOIN TSPL_LOCATION_MASTER LM ON BM.location_code = LM.Location_Code WHERE  BD.Cust_Code is not null and vm.Number IS NOT NULL AND BM.location_code IS NOT NULL " + StrCustFill + " " + StrVecFill + " "
            strPivotForInternalqueryBranch += " for xml path ('')) as strr)as a "
            strPivotForInternalBranch = clsDBFuncationality.getSingleValue(strPivotForInternalqueryBranch)
            If strPivotForInternalBranch.Contains("&amp;") Then
                strPivotForInternalBranch = strPivotForInternalBranch.Replace("&amp;", "&")
            End If
            If strPivotForInternalBranch.Contains("[],") Then
                strPivotForInternalBranch = strPivotForInternalBranch.Replace("[],", "")
            End If

            ' PIVOTS COULMNS BY XML PATHS ===========================================
            strPivotForOuterqueryBranch = " (select(select distinct + ',sum(isnull(['+ coalesce(CM.Customer_Name,'')  +'%###%'+ coalesce(vm.Number,'') +'],0)) as '+'['+ coalesce(CM.Customer_Name,'')  +'%###%'+ coalesce(vm.Number,'') +']' FROM TSPL_BOOKING_MATSER BM  LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No "
            strPivotForOuterqueryBranch += " LEFT JOIN TSPL_CUSTOMER_MASTER CM ON BD.Cust_Code = CM.Cust_Code"
            strPivotForOuterqueryBranch += " LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code"
            strPivotForOuterqueryBranch += " LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id"
            strPivotForOuterqueryBranch += " LEFT JOIN TSPL_LOCATION_MASTER LM ON BM.location_code = LM.Location_Code WHERE  BD.Cust_Code is not null and vm.Number IS NOT NULL AND BM.location_code IS NOT NULL " + StrCustFill + " " + StrVecFill + " "
            strPivotForOuterqueryBranch += "  for xml path ('')) as strr)"
            strPivotForOuterbranch = clsDBFuncationality.getSingleValue(strPivotForOuterqueryBranch)
            If strPivotForOuterbranch.Contains("&amp;") Then
                strPivotForOuterbranch = strPivotForOuterbranch.Replace("&amp;", "&")
            End If
            If strPivotForOuterbranch.Contains("sum(isnull([],0)) as [],") Then
                strPivotForOuterbranch = strPivotForOuterbranch.Replace("sum(isnull([],0)) as [],", "")
                'sum(isnull([],0)) as [],
            End If

            ' PIVOTS COULMNS BY XML PATHS ===========================================
            StrToatlQuerybranch = "select +','+STUFF(a.strr,1,1,'') from (select(select distinct + '+sum(isnull(['+ coalesce(CM.Customer_Name,'')  +'%###%'+ coalesce(vm.Number,'') +'],0))' FROM TSPL_BOOKING_MATSER BM  LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No"
            StrToatlQuerybranch += " LEFT JOIN TSPL_CUSTOMER_MASTER CM ON BD.Cust_Code = CM.Cust_Code"
            StrToatlQuerybranch += " LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code"
            StrToatlQuerybranch += " LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id"
            StrToatlQuerybranch += " LEFT JOIN TSPL_LOCATION_MASTER LM ON BM.location_code = LM.Location_Code WHERE  BD.Cust_Code is not null and vm.Number IS NOT NULL AND BM.location_code IS NOT NULL " + StrCustFill + " " + StrVecFill + " "
            StrToatlQuerybranch += " for xml path ('')) as strr)as a"
            StrToatlBranch = clsDBFuncationality.getSingleValue(StrToatlQuerybranch)
            If StrToatlBranch.Contains("&amp;") Then
                StrToatlBranch = StrToatlBranch.Replace("&amp;", "&")
            End If
            If StrToatlBranch.Contains("sum(isnull([],0))+") Then
                StrToatlBranch = StrToatlBranch.Replace("sum(isnull([],0))+", "")
                'sum(isnull([],0))+,
            End If

            ' QUERY OF REPORT CUSTOMER WISE BOOKING  ===========================================

            ' KUNAL > TICKET : BM00000010085 > 21 -OCT -2016
            Dim qryBranch As String = "SELECT ([PLANT CODE]) [PLANT CODE], MAX([LOC TYPE]) [TYPE], ([PRODUCT CODE]) [PRODUCT CODE], MAX([PRODUCT NAME]) [PRODUCT NAME], [UOM], [CUSTOMER CODE], MAX([CUSTOMER NAME]) [CUSTOMER NAME], CASE WHEN SUM([BOOKING QTY]) > 0.00 THEN SUM([BOOKING QTY])  END  [BOOKING QTY], [VEHICLE ID], MAX([VEHICLE NAME]) [VEHICLE NAME], MAX([CUSTOMER NAME]) + '%###%' + MAX([VEHICLE NAME]) AS [CUSTOMER-VEHICLE-TEMP] " & _
                                        "FROM ( SELECT BM.location_code [PLANT CODE], (SELECT Type FROM TSPL_LOCATION_MASTER WHERE Location_Code = BM.location_code) [LOC TYPE], BM.Document_No [BOOKING NO], BD.Item_Code [PRODUCT CODE], IM.Item_Desc [PRODUCT NAME], BD.Unit_code [UOM], BD.Booking_Qty [BOOKING QTY], BD.Vehicle_Code [VEHICLE ID], VM.Number [VEHICLE NAME], BD.Cust_Code [CUSTOMER CODE], CL.Customer_Name as [CUSTOMER NAME] FROM TSPL_BOOKING_MATSER BM " & _
                                        " LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No LEFT JOIN TSPL_LOCATION_MASTER LM ON BM.location_code = LM.Location_Code LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id LEFT JOIN TSPL_CUSTOMER_MASTER CL ON CL.Cust_Code = BD.Cust_Code" & _
                                        " WHERE BM.Document_Date>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND  BM.Document_Date<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' AND BD.FOC_Item <>1 AND BD.Cust_Code IS NOT NULL AND VM.Number IS NOT NULL "

            ' LOCATION FINDER  ===========================================
            If MultLocation.arrValueMember IsNot Nothing Then
                qryBranch += " AND BM.location_code IN (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ")"
            End If

            ' CUSTOMER FINDER  ===========================================
            If MultCustomer.arrValueMember IsNot Nothing Then
                qryBranch += " AND BD.Cust_Code IN (" + clsCommon.GetMulcallString(MultCustomer.arrValueMember) + ")"
            End If

            ' VEHICLE FINDER  ===========================================
            If MultVehicle.arrValueMember IsNot Nothing Then
                qryBranch += " AND VM.Number IN (" + clsCommon.GetMulcallString(MultVehicle.arrValueMember) + ")"
            End If


            qryBranch += ") TBL_0 GROUP BY [CUSTOMER CODE], [PLANT CODE], [PRODUCT CODE], [UOM], [VEHICLE ID]"

            If rdbCustomer.IsChecked Then
                qry = " SELECT [PLANT CODE], [PRODUCT CODE], MAX([PRODUCT NAME]) AS [PRODUCT NAME], MAX([UOM]) AS [UOM], [CUSTOMER CODE], MAX([CUSTOMER NAME]) [CUSTOMER NAME]" + strPivotForOuterbranch + " " + StrToatlBranch + " as total_Qty FROM ("
                qry += "select * from ( "
                qry += " " & qryBranch & " "
                qry += ") AS t PIVOT (SUM([BOOKING QTY]) FOR [CUSTOMER-VEHICLE-TEMP] IN (" + strPivotForInternalBranch + ")) AS p"
                qry += " ) as aa GROUP BY [PLANT CODE], [PRODUCT CODE], [UOM], [VEHICLE ID], [CUSTOMER CODE] "
                qry += "ORDER BY [PLANT CODE], [CUSTOMER CODE] "
            ElseIf rdbCustomer.IsChecked Or rdbAll.IsChecked Then
                clsCommon.MyMessageBoxShow(Me, "Under Development", Me.Text)
            End If

            ' FILL RESULT IN GRID VIEW  ===========================================
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            gv2.DataSource = Nothing
            gv2.Rows.Clear()
            gv2.Columns.Clear()
            gv2.DataSource = dt
            gv2.GroupDescriptors.Clear()
            gv2.MasterTemplate.SummaryRowsBottom.Clear()

            FormatGrid_CustomerReport(branchVehiclesCols)
            gv2.BestFitColumns()
            RadPageView1.SelectedPage = RadPageViewPage2

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Report_VechileWiseBooking()

        Try
            Dim dt As DataTable
            Dim qry As String = Nothing
            Dim finalQuery As String = Nothing
            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                txtFromDate.Focus()
                Exit Sub
            End If

            ' PIVOT VARIABLES ===========================================
            Dim strPivotForInternalqueryBranch As String = Nothing
            Dim strPivotForInternalBranch As String = Nothing
            Dim strPivotForOuterqueryBranch As String = Nothing
            Dim strPivotForOuterbranch As String = Nothing
            Dim StrToatlQuerybranch As String = Nothing
            Dim StrToatlBranch As String = Nothing
            Dim StrVecFill As String = Nothing
            If MultVehicle.arrValueMember IsNot Nothing Then
                StrVecFill = " AND VM.Number IN (" + clsCommon.GetMulcallString(MultVehicle.arrValueMember) + ")"
            End If
            ' EXTRA COLUMNS ===========================================
            Dim branchVehiclesCols As DataTable = Nothing
            branchVehiclesCols = clsDBFuncationality.GetDataTable("select distinct  coalesce(vm.Number,'') FROM TSPL_BOOKING_MATSER BM  LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No  LEFT JOIN TSPL_CUSTOMER_MASTER CM ON BD.Cust_Code = CM.Cust_Code  LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code  LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id LEFT JOIN TSPL_LOCATION_MASTER LM ON BM.location_code = LM.Location_Code WHERE  BD.Cust_Code is not null and vm.Number IS NOT NULL AND BM.location_code IS NOT NULL " + StrVecFill + "")

            ' PIVOTS COULMNS BY XML PATHS ===========================================
            strPivotForInternalqueryBranch = " select STUFF(a.strr,1,1,'') from (select(select distinct + ',['+ coalesce(vm.Number,'') +']' FROM TSPL_BOOKING_MATSER BM  LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No "
            strPivotForInternalqueryBranch += " LEFT JOIN TSPL_CUSTOMER_MASTER CM ON BD.Cust_Code = CM.Cust_Code"
            strPivotForInternalqueryBranch += " LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code"
            strPivotForInternalqueryBranch += " LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id"
            strPivotForInternalqueryBranch += " LEFT JOIN TSPL_LOCATION_MASTER LM ON BM.location_code = LM.Location_Code WHERE  BD.Cust_Code is not null and vm.Number IS NOT NULL AND BM.location_code IS NOT NULL " + StrVecFill + ""
            strPivotForInternalqueryBranch += " for xml path ('')) as strr)as a "
            strPivotForInternalBranch = clsDBFuncationality.getSingleValue(strPivotForInternalqueryBranch)
            If strPivotForInternalBranch.Contains("[],") Then
                strPivotForInternalBranch = strPivotForInternalBranch.Replace("[],", "")
            End If

            ' PIVOTS COULMNS BY XML PATHS ===========================================
            strPivotForOuterqueryBranch = " (select(select distinct + ',sum(isnull(['+ coalesce(vm.Number,'') +'],0)) as '+'['+ coalesce(vm.Number,'') +']' FROM TSPL_BOOKING_MATSER BM  LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No "
            strPivotForOuterqueryBranch += " LEFT JOIN TSPL_CUSTOMER_MASTER CM ON BD.Cust_Code = CM.Cust_Code"
            strPivotForOuterqueryBranch += " LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code"
            strPivotForOuterqueryBranch += " LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id"
            strPivotForOuterqueryBranch += " LEFT JOIN TSPL_LOCATION_MASTER LM ON BM.location_code = LM.Location_Code WHERE  BD.Cust_Code is not null and vm.Number IS NOT NULL AND BM.location_code IS NOT NULL " + StrVecFill + ""
            strPivotForOuterqueryBranch += "  for xml path ('')) as strr)"
            strPivotForOuterbranch = clsDBFuncationality.getSingleValue(strPivotForOuterqueryBranch)
            If strPivotForOuterbranch.Contains("sum(isnull([],0)) as [],") Then
                strPivotForOuterbranch = strPivotForOuterbranch.Replace("sum(isnull([],0)) as [],", "")
                'sum(isnull([],0)) as [],
            End If

            ' PIVOTS COULMNS BY XML PATHS ===========================================
            StrToatlQuerybranch = "select +','+STUFF(a.strr,1,1,'') from (select(select distinct + '+sum(isnull(['+ coalesce(vm.Number,'') +'],0))' FROM TSPL_BOOKING_MATSER BM  LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No"
            StrToatlQuerybranch += " LEFT JOIN TSPL_CUSTOMER_MASTER CM ON BD.Cust_Code = CM.Cust_Code"
            StrToatlQuerybranch += " LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code"
            StrToatlQuerybranch += " LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id"
            StrToatlQuerybranch += " LEFT JOIN TSPL_LOCATION_MASTER LM ON BM.location_code = LM.Location_Code WHERE  BD.Cust_Code is not null and vm.Number IS NOT NULL AND BM.location_code IS NOT NULL " + StrVecFill + ""
            StrToatlQuerybranch += " for xml path ('')) as strr)as a"
            StrToatlBranch = clsDBFuncationality.getSingleValue(StrToatlQuerybranch)
            If StrToatlBranch.Contains("sum(isnull([],0))+") Then
                StrToatlBranch = StrToatlBranch.Replace("sum(isnull([],0))+", "")
                'sum(isnull([],0))+,
            End If

            ' QUERY OF REPORT CUSTOMER WISE BOOKING  ===========================================

            ' KUNAL > TICKET : BM00000010085 > 21 -OCT -2016
            Dim qryBranch As String = "SELECT ([PLANT CODE]) [PLANT CODE], MAX([LOC TYPE]) [TYPE], ([PRODUCT CODE]) [PRODUCT CODE], MAX([PRODUCT NAME]) [PRODUCT NAME], [UOM], MAX([CUSTOMER CODE]) AS [CUSTOMER CODE], MAX([CUSTOMER NAME]) [CUSTOMER NAME], CASE WHEN SUM([BOOKING QTY]) > 0.00 THEN SUM([BOOKING QTY])  END  [BOOKING QTY], [VEHICLE ID], MAX([VEHICLE NAME]) [VEHICLE NAME],MAX([VEHICLE NAME]) AS [VEHICLE-TEMP] " & _
                                        "FROM ( SELECT BM.location_code [PLANT CODE], (SELECT Type FROM TSPL_LOCATION_MASTER WHERE Location_Code = BM.location_code) [LOC TYPE], BM.Document_No [BOOKING NO], BD.Item_Code [PRODUCT CODE], IM.Item_Desc [PRODUCT NAME], BD.Unit_code [UOM], BD.Booking_Qty [BOOKING QTY], BD.Vehicle_Code [VEHICLE ID], VM.Number [VEHICLE NAME], BD.Cust_Code [CUSTOMER CODE], CL.Customer_Name [CUSTOMER NAME] FROM TSPL_BOOKING_MATSER BM " & _
                                        " LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No  LEFT JOIN TSPL_LOCATION_MASTER LM ON BM.location_code = LM.Location_Code LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id LEFT JOIN TSPL_CUSTOMER_MASTER CL ON CL.Cust_Code = BD.Cust_Code" & _
                                        " WHERE BM.Document_Date>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' AND  BM.Document_Date<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' AND BD.FOC_Item <>1 AND BD.Cust_Code IS NOT NULL AND VM.Number IS NOT NULL "

            ' LOCATION FINDER  ===========================================
            If MultLocation.arrValueMember IsNot Nothing Then
                qryBranch += " AND BM.location_code IN (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ")"
            End If

            ' CUSTOMER FINDER  ===========================================
            If MultCustomer.arrValueMember IsNot Nothing Then
                qryBranch += " AND BD.Cust_Code IN (" + clsCommon.GetMulcallString(MultCustomer.arrValueMember) + ")"
            End If

            ' VEHICLE FINDER  ===========================================
            If MultVehicle.arrValueMember IsNot Nothing Then
                qryBranch += " AND VM.Number IN (" + clsCommon.GetMulcallString(MultVehicle.arrValueMember) + ")"
            End If


            qryBranch += ") TBL_0 GROUP BY [PLANT CODE], [PRODUCT CODE], [UOM], [VEHICLE ID]"

            qry = " SELECT [PLANT CODE], [PRODUCT CODE], MAX([PRODUCT NAME]) AS [PRODUCT NAME], MAX([UOM]) AS [UOM], max([CUSTOMER CODE]) as [CUSTOMER CODE], MAX([CUSTOMER NAME]) [CUSTOMER NAME]" + strPivotForOuterbranch + " " + StrToatlBranch + " as total_Qty FROM ("
            qry += "select * from ( "
            qry += " " & qryBranch & " "
            qry += ") AS t PIVOT (SUM([BOOKING QTY]) FOR [VEHICLE-TEMP] IN (" + strPivotForInternalBranch + ")) AS p"
            qry += " ) as aa GROUP BY [PLANT CODE], [PRODUCT CODE], [UOM] "
            qry += "ORDER BY [PLANT CODE], [CUSTOMER CODE] "

            ' FILL RESULT IN GRID VIEW  ===========================================
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            gv2.DataSource = Nothing
            gv2.Rows.Clear()
            gv2.Columns.Clear()
            gv2.DataSource = dt
            gv2.GroupDescriptors.Clear()
            gv2.MasterTemplate.SummaryRowsBottom.Clear()

            FormatGrid_CustomerReport(branchVehiclesCols)
            gv2.BestFitColumns()
            RadPageView1.SelectedPage = RadPageViewPage2

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ' KUNAL > TICKET : BM00000009967 : DATE 14 -OCT -2016
    Private Sub Report_BranchWiseBooking()

        Try
            Dim dt As DataTable
            Dim qry As String = Nothing
            Dim finalQuery As String = Nothing
            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                txtFromDate.Focus()
                Exit Sub
            End If

            '============================For Branch============================
            Dim strPivotForInternalqueryBranch As String = Nothing
            ' BRANCH PIVOT QUERY
            Dim strPivotForInternalBranch As String = Nothing
            Dim strPivotForOuterqueryBranch As String = Nothing
            ' BRANCH PIVOT QUERY
            Dim strPivotForOuterbranch As String = Nothing
            Dim StrToatlQuerybranch As String = Nothing
            ' BRANCH PIVOT QUERY
            Dim StrToatlBranch As String = Nothing
            '================================END Branch==========================


            '======================================For Branch====================================================================


            Dim branchVehiclesCols As DataTable = Nothing
            branchVehiclesCols = clsDBFuncationality.GetDataTable("select distinct coalesce(TSPL_LOCATION_MASTER.Location_Desc,'')   +'%###%'+ coalesce(vm.Number,'')  FROM TSPL_BOOKING_MATSER BM LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No LEFT JOIN TSPL_LOCATION_PLANTDEPOT_DETAIL PD ON BM.location_code = PD.Plant_Location_Code LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =PD.Depot_Location_Code WHERE PD.Depot_Location_Code IS NOT NULL")


            strPivotForInternalqueryBranch = " select STUFF(a.strr,1,1,'') from (select(select distinct + ',['+ coalesce(TSPL_LOCATION_MASTER.Location_Desc,'')   +'%###%'+ coalesce(vm.Number,'') +']' FROM TSPL_BOOKING_MATSER BM LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No"
            strPivotForInternalqueryBranch += " LEFT JOIN TSPL_LOCATION_PLANTDEPOT_DETAIL PD ON BM.location_code = PD.Plant_Location_Code"
            strPivotForInternalqueryBranch += " LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code"
            strPivotForInternalqueryBranch += " LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id"
            strPivotForInternalqueryBranch += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =PD.Depot_Location_Code WHERE PD.Depot_Location_Code IS NOT NULL"
            strPivotForInternalqueryBranch += " for xml path ('')) as strr)as a "
            strPivotForInternalBranch = clsCommon.myCstr(clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForInternalqueryBranch)))

            If String.IsNullOrEmpty(strPivotForInternalBranch) Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            If strPivotForInternalBranch.Contains("[],") Then
                strPivotForInternalBranch = strPivotForInternalBranch.Replace("[],", "")
            End If


            strPivotForOuterqueryBranch = " (select(select distinct + ',sum(isnull(['+ coalesce(TSPL_LOCATION_MASTER.Location_Desc,'')  +'%###%'+ coalesce(vm.Number,'') +'],0)) as '+'['+ coalesce(TSPL_LOCATION_MASTER.Location_Desc,'')  +'%###%'+ coalesce(vm.Number,'') +']' FROM TSPL_BOOKING_MATSER BM LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No"
            strPivotForOuterqueryBranch += " LEFT JOIN TSPL_LOCATION_PLANTDEPOT_DETAIL PD ON BM.location_code = PD.Plant_Location_Code"
            strPivotForOuterqueryBranch += " LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code"
            strPivotForOuterqueryBranch += " LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id"
            strPivotForOuterqueryBranch += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =PD.Depot_Location_Code WHERE PD.Depot_Location_Code IS NOT NULL"
            strPivotForOuterqueryBranch += "  for xml path ('')) as strr)"
            strPivotForOuterbranch = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuterqueryBranch))

            If strPivotForOuterbranch.Contains("sum(isnull([],0)) as [],") Then
                strPivotForOuterbranch = strPivotForOuterbranch.Replace("sum(isnull([],0)) as [],", "")
                'sum(isnull([],0)) as [],
            End If

            StrToatlQuerybranch = "select +','+STUFF(a.strr,1,1,'') from (select(select distinct + '+sum(isnull(['+ coalesce(TSPL_LOCATION_MASTER.Location_Desc,'')  +'%###%'+ coalesce(vm.Number,'') +'],0))'FROM TSPL_BOOKING_MATSER BM LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No"
            StrToatlQuerybranch += " LEFT JOIN TSPL_LOCATION_PLANTDEPOT_DETAIL PD ON BM.location_code = PD.Plant_Location_Code"
            StrToatlQuerybranch += " LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code"
            StrToatlQuerybranch += " LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id"
            StrToatlQuerybranch += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =PD.Depot_Location_Code WHERE PD.Depot_Location_Code IS NOT NULL"
            StrToatlQuerybranch += " for xml path ('')) as strr)as a"
            StrToatlBranch = clsCommon.myCstr(clsDBFuncationality.getSingleValue(StrToatlQuerybranch))

            If StrToatlBranch.Contains("sum(isnull([],0))+") Then
                StrToatlBranch = StrToatlBranch.Replace("sum(isnull([],0))+", "")
                'sum(isnull([],0))+,
            End If

            Dim qryBranch As String = "SELECT ([PLANT CODE]) [PLANT CODE], MAX([LOC TYPE]) [TYPE], ([PRODUCT CODE]) [PRODUCT CODE], " & _
                                     " MAX([PRODUCT NAME]) [PRODUCT NAME], [UOM],  [BRANCH CODE], MAX([BRANCH NAME]) [BRANCH NAME] , MAX([BRANCH TYPE]) [BRANCH TYPE] , SUM([BOOKING QTY]) [BOOKING QTY], [VEHICLE ID]," & _
                                     "  MAX([VEHICLE NAME]) [VEHICLE NAME] ,max([BRANCH NAME]) + '%###%'  + max([VEHICLE NAME]) as [BRANCH-VEHICLE-TEMP] " & _
                                    " FROM (SELECT BM.location_code [PLANT CODE],(SELECT Type FROM TSPL_LOCATION_MASTER WHERE Location_Code = BM.location_code)[LOC TYPE]," & _
                                    " BM.Document_No [BOOKING NO],BD.Item_Code [PRODUCT CODE],IM.Item_Desc [PRODUCT NAME],BD.Unit_code [UOM],BD.Booking_Qty [BOOKING QTY]," & _
                                     " BD.Vehicle_Code [VEHICLE ID],VM.Number [VEHICLE NAME],BD.Cust_Code [CUSTOMER CODE],CL.Customer_Name [CUSTOMER NAME]," & _
                                    " PD.Depot_Location_Code [BRANCH CODE],(SELECT Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code = PD.Depot_Location_Code) [BRANCH NAME]," & _
                                    " (SELECT Type FROM TSPL_LOCATION_MASTER WHERE Location_Code = PD.Depot_Location_Code) [BRANCH TYPE] " & _
                                    " FROM TSPL_BOOKING_MATSER BM " & _
                                    " LEFT JOIN TSPL_BOOKING_DETAIL BD ON BM.Document_No = BD.Document_No " & _
                                    " LEFT JOIN TSPL_LOCATION_MASTER LM ON BM.location_code = LM.Location_Code " & _
                                    " LEFT JOIN TSPL_LOCATION_PLANTDEPOT_DETAIL PD ON BM.location_code = PD.Plant_Location_Code " & _
                                    " LEFT JOIN TSPL_ITEM_MASTER IM ON BD.Item_Code = IM.Item_Code LEFT JOIN TSPL_VEHICLE_MASTER VM ON BD.Vehicle_Code = VM.Vehicle_Id " & _
                                    " LEFT JOIN TSPL_CUSTOMER_MASTER CL ON CL.Cust_Code = BD.Cust_Code WHERE PD.Depot_Location_Code IS NOT NULL "
            If MultLocation.arrValueMember IsNot Nothing Then
                qryBranch += " AND BM.location_code IN ('" + clsCommon.GetMulcallStringWithComma(MultLocation.arrValueMember) + "')"
            End If

            If MultCustomer.arrValueMember IsNot Nothing Then
                qryBranch += " AND BD.Cust_Code IN ('" + clsCommon.GetMulcallStringWithComma(MultCustomer.arrValueMember) + "')"
            End If

            If MultVehicle.arrValueMember IsNot Nothing Then
                qryBranch += " AND VM.Number IN ('" + clsCommon.GetMulcallStringWithComma(MultVehicle.arrValueMember) + "')"
            End If


            qryBranch += ") TBL_0 GROUP BY [BRANCH CODE],[PLANT CODE],[PRODUCT CODE],[UOM],[VEHICLE ID]"

            If rdbBranch.IsChecked Then
                qry = "SELECT [PLANT CODE],[PRODUCT CODE],max([PRODUCT NAME]) as [PRODUCT NAME],max([UOM]) as [UOM] , [BRANCH CODE], MAX([BRANCH NAME]) [BRANCH NAME] " + strPivotForOuterbranch + " " + StrToatlBranch + " as total_Qty FROM ("
                qry += "select * from ( "
                qry += " " & qryBranch & " "
                qry += ") AS t PIVOT ( sum([BOOKING QTY]) FOR [BRANCH-VEHICLE-TEMP] IN (" + strPivotForInternalBranch + ")) AS p"
                qry += " ) as aa GROUP BY [PLANT CODE],[PRODUCT CODE],[UOM], [BRANCH CODE] "
                qry += "order by [PLANT CODE] ,  [BRANCH CODE] "
            ElseIf rdbCustomer.IsChecked Or rdbAll.IsChecked Then
                clsCommon.MyMessageBoxShow(Me, "Under Development", Me.Text)
            End If

            '===============================================================================
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            gv2.DataSource = Nothing
            gv2.Rows.Clear()
            gv2.Columns.Clear()
            gv2.DataSource = dt
            gv2.GroupDescriptors.Clear()
            gv2.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid_BranchReport(branchVehiclesCols)
            gv2.BestFitColumns()
            RadPageView1.SelectedPage = RadPageViewPage2

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        ' KUNAL > TICKET : BM00000009967 : DATE 14 -OCT -2016
        ' Sanjeet > Modification and added radiobutton vehicle : DATE 22 -11 -2017
        Try
            PageSetupReport_ID = MyBase.Form_ID
            If rdbBranch.IsChecked Then
                Report_BranchWiseBooking()
            ElseIf rdbCustomer.IsChecked Then
                Report_CustomerWiseBooking()
            ElseIf rbtnVechile.IsChecked Then
                Report_VechileWiseBooking()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub Reset()
        Try
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = txtToDate.Value.AddMonths(-1)
            RadPageView1.SelectedPage = RadPageViewPage1
            rdbCustomer.IsChecked = True
            gv2.DataSource = Nothing
            ' rdbBranch.IsChecked = True
            MultLocation.arrValueMember = Nothing
            MultCustomer.arrValueMember = Nothing
            MultVehicle.arrValueMember = Nothing
            rdbAll.Visible = False
            rdbBranch.Visible = False
        Catch ex As Exception

        End Try
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

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    'Private Sub LoadData()
    '    Try


    '        Dim dt As DataTable
    '        Dim finalQuery As String = Nothing
    '        If txtFromDate.Value > txtToDate.Value Then
    '            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
    '            txtFromDate.Focus()
    '            Exit Sub
    '        End If
    '        '=============================================pivot variables================================================
    '        Dim strPivotForInternalCustomer As String = Nothing
    '        Dim strPivotForOuterCustomer As String = Nothing
    '        Dim StrPivotForLastDay As String = Nothing
    '        Dim StrPivotForLastSumDay As String = Nothing
    '        Dim strPivotForInternalqueryCustomer As String = Nothing
    '        Dim strPivotForOuterqueryCustomer As String = Nothing
    '        Dim StrPivotForLastDayquery As String = Nothing
    '        Dim StrPivotForLastDaySumquery As String = Nothing
    '        Dim StrDateDiffQuery As String = Nothing
    '        Dim StrDateDiff As String = Nothing
    '        Dim StrToatlQueryCustomer As String = Nothing
    '        Dim StrToatlCustomer As String = Nothing
    '        Dim StrToatlSumQuery As String = Nothing
    '        Dim StrToatlsum As String = Nothing
    '        '============================For Branch============================
    '        Dim strPivotForInternalqueryBranch As String = Nothing
    '        Dim strPivotForInternalBranch As String = Nothing
    '        Dim strPivotForOuterqueryBranch As String = Nothing
    '        Dim strPivotForOuterbranch As String = Nothing
    '        Dim StrToatlQuerybranch As String = Nothing
    '        Dim StrToatlBranch As String = Nothing
    '        '================================END Branch==========================
    '        '==============================================For Customer========================================================================
    '        Dim dtExtraColumnCustomer As DataTable = Nothing
    '        dtExtraColumnCustomer = clsDBFuncationality.GetDataTable("select distinct  tspl_item_master.short_description from TSPL_BOOKING_DETAIL left join tspl_item_master on tspl_item_master.item_code = TSPL_BOOKING_DETAIL.item_code   ")


    '        strPivotForInternalqueryCustomer = " select STUFF(a.strr,1,1,'') from (select(select distinct + ',['+ tspl_item_master.short_description+']' from TSPL_BOOKING_DETAIL"
    '        strPivotForInternalqueryCustomer += " left join tspl_item_master on tspl_item_master.item_code = TSPL_BOOKING_DETAIL.item_code   for xml path ('')) as strr)as a "
    '        strPivotForInternalCustomer = clsDBFuncationality.getSingleValue(strPivotForInternalqueryCustomer)

    '        strPivotForOuterqueryCustomer = " (select(select distinct + ',sum(isnull(['+ tspl_item_master.short_description+'],0)) as '+'['+ tspl_item_master.short_description+']' from TSPL_BOOKING_DETAIL"
    '        strPivotForOuterqueryCustomer += " left join tspl_item_master on tspl_item_master.item_code = TSPL_BOOKING_DETAIL.item_code   for xml path ('')) as strr)"
    '        strPivotForOuterCustomer = clsDBFuncationality.getSingleValue(strPivotForOuterqueryCustomer)

    '        StrToatlQueryCustomer = "select +','+STUFF(a.strr,1,1,'') from (select(select distinct + '+sum(isnull(['+ tspl_item_master.short_description+'],0))' from TSPL_BOOKING_DETAIL"
    '        StrToatlQueryCustomer += " left join tspl_item_master on tspl_item_master.item_code = TSPL_BOOKING_DETAIL.item_code   for xml path ('')) as strr)as a"
    '        StrToatlCustomer = clsDBFuncationality.getSingleValue(StrToatlQueryCustomer)
    '        '=============================================END For Customer=================================================================

    '        '======================================For Branch====================================================================


    '        Dim dtExtraColumnTransfer As DataTable = Nothing
    '        dtExtraColumnTransfer = clsDBFuncationality.GetDataTable("select distinct  tspl_item_master.short_description from TSPL_TRANSFER_ORDER_DETAIL left join tspl_item_master on tspl_item_master.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code   ")


    '        strPivotForInternalqueryBranch = " select STUFF(a.strr,1,1,'') from (select(select distinct + ',['+ tspl_item_master.short_description+']' from TSPL_TRANSFER_ORDER_DETAIL"
    '        strPivotForInternalqueryBranch += " left join tspl_item_master on tspl_item_master.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code   for xml path ('')) as strr)as a "
    '        strPivotForInternalBranch = clsDBFuncationality.getSingleValue(strPivotForInternalqueryBranch)

    '        strPivotForOuterqueryBranch = " (select(select distinct + ',sum(isnull(['+ tspl_item_master.short_description+'],0)) as '+'['+ tspl_item_master.short_description+']' from TSPL_TRANSFER_ORDER_DETAIL"
    '        strPivotForOuterqueryBranch += " left join tspl_item_master on tspl_item_master.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code   for xml path ('')) as strr)"
    '        strPivotForOuterbranch = clsDBFuncationality.getSingleValue(strPivotForOuterqueryBranch)

    '        StrToatlQuerybranch = "select +','+STUFF(a.strr,1,1,'') from (select(select distinct + '+sum(isnull(['+ tspl_item_master.short_description+'],0))' from TSPL_TRANSFER_ORDER_DETAIL"
    '        StrToatlQuerybranch += " left join tspl_item_master on tspl_item_master.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code   for xml path ('')) as strr)as a"
    '        StrToatlBranch = clsDBFuncationality.getSingleValue(StrToatlQuerybranch)
    '        '=============================================END For Customer=================================================================

    '        '====================================================All=============================
    '        '======================================For Branch====================================================================
    '        Dim strPivotForInternalquery As String = Nothing
    '        Dim strPivotForInternal As String = Nothing
    '        Dim strPivotForOuterquery As String = Nothing
    '        Dim strPivotForOuter As String = Nothing
    '        Dim StrToatlQuery As String = Nothing
    '        Dim StrToatl As String = Nothing

    '        Dim dtExtraColumn As DataTable = Nothing
    '        dtExtraColumn = clsDBFuncationality.GetDataTable("select distinct  tspl_item_master.short_description from TSPL_TRANSFER_ORDER_DETAIL left join tspl_item_master on tspl_item_master.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code   ")


    '        strPivotForInternalquery = " select STUFF(a.strr,1,1,'') from (select(select distinct + ',['+short_description+']' from(select tspl_item_master.short_description  from TSPL_TRANSFER_ORDER_DETAIL left join tspl_item_master on tspl_item_master.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code union all"
    '        strPivotForInternalquery += " select short_description from TSPL_TRANSFER_ORDER_DETAIL left join tspl_item_master on tspl_item_master.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code ) as pp"
    '        strPivotForInternalquery += "  for xml path ('')) as strr)as a"
    '        strPivotForInternal = clsDBFuncationality.getSingleValue(strPivotForInternalquery)

    '        strPivotForOuterquery = "   (select(select distinct + ',sum(isnull(['+ short_description+'],0)) as '+'['+ short_description+']' from( select  tspl_item_master.short_description from TSPL_TRANSFER_ORDER_DETAIL "
    '        strPivotForOuterquery += " left join tspl_item_master on tspl_item_master.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code"
    '        strPivotForOuterquery += "  union all"
    '        strPivotForOuterquery += " select short_description from TSPL_TRANSFER_ORDER_DETAIL "
    '        strPivotForOuterquery += "left join tspl_item_master on tspl_item_master.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code"
    '        strPivotForOuterquery += ") as pp for xml path ('')) as strr) "
    '        strPivotForOuter = clsDBFuncationality.getSingleValue(strPivotForOuterquery)

    '        StrToatlQuery = " select +','+STUFF(a.strr,1,1,'') from (select(select distinct + '+sum(isnull(['+short_description+'],0))' from (select  tspl_item_master.short_description from TSPL_TRANSFER_ORDER_DETAIL "
    '        StrToatlQuery += " left join tspl_item_master on tspl_item_master.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code"
    '        StrToatlQuery += "   union all"
    '        StrToatlQuery += " select short_description from TSPL_TRANSFER_ORDER_DETAIL "
    '        StrToatlQuery += "  left join tspl_item_master on tspl_item_master.item_code = TSPL_TRANSFER_ORDER_DETAIL.item_code) as pp"
    '        StrToatlQuery += "  for xml path ('')) as strr)as a"
    '        StrToatl = clsDBFuncationality.getSingleValue(StrToatlQuery)
    '        '=============================================END For Customer=================================================================
    '        '===================================================================================

    '        '================================================================end here=============================================================================
    '        Dim qry As String = Nothing
    '        Dim qryCustomer As String = " select max(Type) as Type, Route_No ,max(Route_Desc) as Route_Desc,max(To_location) as To_location,max(Location_desc) as Location_desc,Vehicle_Code,max(Vechicle_Name) as Vechicle_Name,Item_Code,max(short_description) as short_description,max(Item_Desc) as Item_Desc,Cust_Code,max(Customer_Name) as Customer_Name,max(Unit_code) as Unit_code,sum(DocumentAmount) as DocumentAmount,sum(Booking_Qty) as Booking_Qty from (" & _
    '                                " select 'Customer' as Type, TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_MATSER.Document_Date,TSPL_CUSTOMER_MASTER.Route_No,TSPL_ROUTE_MASTER.Route_Desc,'' as To_location,'' as Location_desc,TSPL_BOOKING_DETAIL.Vehicle_Code,tspl_vehicle_master.Number as Vechicle_Name,TSPL_BOOKING_DETAIL.Item_Code, tspl_item_master.short_description  ,TSPL_BOOKING_DETAIL.Cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_BOOKING_DETAIL.Unit_code   ,TSPL_BOOKING_DETAIL.DocumentAmount ,TSPL_ITEM_MASTER.Item_Desc , TSPL_BOOKING_DETAIL.Booking_Qty   " & _
    '                                "  from TSPL_BOOKING_DETAIL  left join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No =TSPL_BOOKING_DETAIL.Document_No " & _
    '                                " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_BOOKING_MATSER.Location_code " & _
    '                                "  left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_BOOKING_DETAIL.Cust_Code  " & _
    '                                " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_BOOKING_DETAIL.Item_Code " & _
    '                                " left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No =TSPL_CUSTOMER_MASTER.Route_No " & _
    '                                " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_BOOKING_DETAIL.vehicle_code where 2=2 and  convert(date,TSPL_BOOKING_MATSER.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"


    '        'If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
    '        '    qry += " and TSPL_LOCATION_MASTER. Location_Code   IN (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ") "
    '        'End If
    '        If MultCustomer.arrValueMember IsNot Nothing AndAlso MultCustomer.arrValueMember.Count > 0 Then
    '            qryCustomer += "and TSPL_BOOKING_DETAIL.Cust_Code  in (" + clsCommon.GetMulcallString(MultCustomer.arrValueMember) + ") " + Environment.NewLine
    '        End If
    '        If MultVehicle.arrValueMember IsNot Nothing AndAlso MultVehicle.arrValueMember.Count > 0 Then
    '            qryCustomer += "and TSPL_BOOKING_DETAIL.Vehicle_Code in (" + clsCommon.GetMulcallString(MultVehicle.arrValueMember) + ") " + Environment.NewLine
    '        End If
    '        qryCustomer += " ) as xx " & _
    '                                "  group by xx.Cust_Code ,xx.Route_No ,xx.Vehicle_Code,xx.Item_Code"


    '        Dim qryBranch As String = "select max(Type) as Type, Route_No ,max(Route_Desc) as Route_Desc,(To_location) as To_location,max(Location_desc) as Location_desc,Vehicle_Code,max(Vechicle_Name) as Vechicle_Name,Item_Code,max(short_description) as short_description,max(Item_Desc) as Item_Desc,max(Cust_Code) as Cust_Code,max(Cust_Name  ) as Customer_Name,max(Unit_code) as Unit_code,sum(Amount ) as DocumentAmount,sum(Out_Qty ) as outQty" & _
    '                                " from  (select 'Branch' as Type,TSPL_TRANSFER_ORDER_HEAD.Document_No ,TSPL_TRANSFER_ORDER_HEAD.Document_Date,TSPL_ROUTE_MASTER.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_TRANSFER_ORDER_HEAD.To_Location,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code,tspl_vehicle_master.Number as Vechicle_Name    ,TSPL_TRANSFER_ORDER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Short_Description ,'' as Cust_code,'' as Cust_Name ,TSPL_TRANSFER_ORDER_DETAIL.Unit_code ,TSPL_TRANSFER_ORDER_DETAIL.Amount,TSPL_ITEM_MASTER.Item_Desc  ,TSPL_TRANSFER_ORDER_DETAIL.Out_Qty  from TSPL_TRANSFER_ORDER_DETAIL " & _
    '                                " left join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_ORDER_DETAIL.Document_No " & _
    '                                " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_TRANSFER_ORDER_DETAIL.Item_Code " & _
    '                                " left join TSPL_ROUTE_MASTER on  TSPL_ROUTE_MASTER.vehicle_code =TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code " & _
    '                                " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_TRANSFER_ORDER_HEAD.vehicle_code " & _
    '                                " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.GIT_Location =TSPL_TRANSFER_ORDER_HEAD.To_Location " & _
    '                                " where 2=2 and  convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"

    '        If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
    '            qryBranch += " and TSPL_TRANSFER_ORDER_HEAD.To_Location   IN (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ") "
    '        End If
    '        'If MultCustomer.arrValueMember IsNot Nothing AndAlso MultCustomer.arrValueMember.Count > 0 Then
    '        '    qry += "and TSPL_BOOKING_DETAIL.Cust_Code  in (" + clsCommon.GetMulcallString(MultCustomer.arrValueMember) + ") " + Environment.NewLine
    '        'End If
    '        If MultVehicle.arrValueMember IsNot Nothing AndAlso MultVehicle.arrValueMember.Count > 0 Then
    '            qryBranch += " and TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(MultVehicle.arrValueMember) + ") " + Environment.NewLine
    '        End If
    '        qryBranch += " ) as xx " & _
    '                                " group by xx.To_Location ,xx.Route_No ,xx.Vehicle_Code,xx.Item_Code"
    '        'If rdbCustomer.IsChecked Then

    '        '    qry = " Select max(Type) as Type, Route_No ,max(Route_Desc) as Route_Desc,max(To_location) as To_location,max(Location_desc) as Location_desc,Vehicle_Code,max(Vechicle_Name) as Vechicle_Name,Cust_Code,max(Customer_Name) as Customer_Name,sum(DocumentAmount) as DocumentAmount " + strPivotForOuterCustomer + " " + StrToatlCustomer + " as total_Qty from (" & _
    '        ' "select * from ( " & _
    '        ' " " & qryCustomer & "  " & _
    '        '  " ) as pp pivot (sum(pp.booking_qty) for short_description in (" + strPivotForInternalCustomer + "))t " & _
    '        '" ) as aa group by Cust_Code , Route_No ,Vehicle_Code"
    '        'Else
    '        '    qry = " Select max(Type) as Type, Route_No ,max(Route_Desc) as Route_Desc,(To_location) as To_location,max(Location_desc) as Location_desc,Vehicle_Code,max(Vechicle_Name) as Vechicle_Name,max(Cust_Code) as Cust_Code,max(Customer_Name) as Customer_Name,sum(DocumentAmount) as DocumentAmount " + strPivotForOuterbranch + " " + StrToatlBranch + " as total_Qty from (" & _
    '        '     "select * from ( " & _
    '        '     " " & qryBranch & "  " & _
    '        '    " ) as pp pivot (sum(pp.outQty) for short_description in (" + strPivotForInternalBranch + "))t " & _
    '        '    " ) as aa group by To_location , Route_No ,Vehicle_Code"
    '        'End If

    '        '===============================================================================

    '        qry = " Select max(Type) as Type, Route_No ,max(Route_Desc) as Route_Desc,(To_location) as To_location,max(Location_desc) as Location_desc,Vehicle_Code,max(Vechicle_Name) as Vechicle_Name,(Cust_Code) as Cust_Code,max(Customer_Name) as Customer_Name,sum(DocumentAmount) as DocumentAmount " + strPivotForOuter + " " + StrToatl + " as total_Qty from (" & _
    '         " select * from ( " & _
    '          " select * from ( " & _
    '         "" & qryCustomer & " " & _
    '         " union all " & _
    '         "" & qryBranch & "" & _
    '         " ) as xx where 2=2 "
    '        If rdbCustomer.IsChecked Then
    '            qry += " and  Type='Customer' "
    '        ElseIf rdbBranch.IsChecked Then
    '            qry += " and  Type='Branch' "
    '        End If
    '        qry += " ) as pp pivot (sum(pp.booking_Qty) for short_description in (" + strPivotForInternal + "))t " & _
    '        " ) as aa group by To_location,Cust_Code , Route_No ,Vehicle_Code"





    '        '===============================================================================
    '        dt = clsDBFuncationality.GetDataTable(qry)
    '        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '            clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
    '            Exit Sub
    '        End If
    '        gv2.DataSource = Nothing
    '        gv2.Rows.Clear()
    '        gv2.Columns.Clear()
    '        gv2.DataSource = dt
    '        gv2.GroupDescriptors.Clear()
    '        gv2.MasterTemplate.SummaryRowsBottom.Clear()
    '        FormatGrid(dtExtraColumn)
    '        If rdbCustomer.IsChecked Then
    '            FormatGridforCustomer(dtExtraColumnCustomer)
    '        Else
    '            FormatGrid(dtExtraColumnTransfer)
    '        End If
    '        gv2.BestFitColumns()
    '        RadPageView1.SelectedPage = RadPageViewPage2
    '        ReStoreGridLayout()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv2.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv2.Columns.Count - 1 Step ii + 1
                        gv2.Columns(ii).IsVisible = False
                        gv2.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv2.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Dim colsName As List(Of String) = Nothing
    ' KUNAL > TICKET : BM00000009967 : DATE 14 -OCT -2016
    Sub FormatGrid_CustomerReport(ByVal branchVehiclesCols As DataTable)

        gv2.TableElement.TableHeaderHeight = 30
        gv2.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = True
            gv2.Columns(ii).Width = 80
        Next

        gv2.Columns("PLANT CODE").IsVisible = True
        gv2.Columns("PLANT CODE").Width = 50
        gv2.Columns("PLANT CODE").HeaderText = "PLANT CODE"
        '  gv2.Columns("PLANT CODE").ReadOnly = True

        gv2.Columns("PRODUCT CODE").IsVisible = True
        gv2.Columns("PRODUCT CODE").Width = 50
        gv2.Columns("PRODUCT CODE").HeaderText = "PRODUCT CODE"
        '  gv2.Columns("PRODUCT CODE").ReadOnly = True

        gv2.Columns("PRODUCT NAME").IsVisible = True
        gv2.Columns("PRODUCT NAME").Width = 100
        gv2.Columns("PRODUCT NAME").HeaderText = "PRODUCT NAME"
        '  gv2.Columns("PRODUCT NAME").ReadOnly = True

        gv2.Columns("UOM").IsVisible = True
        gv2.Columns("UOM").Width = 30
        gv2.Columns("UOM").HeaderText = "UOM"
        '   gv2.Columns("UOM").ReadOnly = True

        gv2.Columns("CUSTOMER CODE").IsVisible = False
        gv2.Columns("CUSTOMER CODE").Width = 30
        gv2.Columns("CUSTOMER CODE").HeaderText = "CUSTOMER CODE"
        '  gv2.Columns("BRANCH CODE").ReadOnly = True

        gv2.Columns("CUSTOMER NAME").IsVisible = False
        gv2.Columns("CUSTOMER NAME").Width = 100
        gv2.Columns("CUSTOMER NAME").HeaderText = "CUSTOMER NAME"
        '  gv2.Columns("BRANCH NAME").ReadOnly = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        If branchVehiclesCols IsNot Nothing AndAlso branchVehiclesCols.Rows.Count > 0 Then
            For Each dr As DataRow In branchVehiclesCols.Rows
                Dim item1 As New GridViewSummaryItem(clsCommon.myCstr(dr(0)), "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
        End If
        Dim item3 As New GridViewSummaryItem("total_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        '= SPLITTING COLUMNS NAMES AND SET GRID VIEW COLUMNS CAPTIONS ================
        colsName = New List(Of String)()
        Dim splitColumnsName As String = Nothing
        If branchVehiclesCols IsNot Nothing AndAlso branchVehiclesCols.Rows.Count > 0 Then
            For Each dr As DataRow In branchVehiclesCols.Rows
                If clsCommon.myLen(dr(0)) > 0 Then
                    splitColumnsName = dr(0)
                    If (splitColumnsName.Contains("%###%")) Then
                        splitColumnsName = splitColumnsName.Replace("%###%", "" + Environment.NewLine + "")
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



        gv2.ShowGroupPanel = False
        gv2.MasterTemplate.AutoExpandGroups = True
        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


    End Sub

    ' KUNAL > TICKET : BM00000009967 : DATE 14 -OCT -2016
    Sub FormatGrid_BranchReport(ByVal branchVehiclesCols As DataTable)

        gv2.TableElement.TableHeaderHeight = 30
        gv2.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = True
            gv2.Columns(ii).Width = 80
        Next

        gv2.Columns("PLANT CODE").IsVisible = True
        gv2.Columns("PLANT CODE").Width = 50
        gv2.Columns("PLANT CODE").HeaderText = "PLANT CODE"
        '  gv2.Columns("PLANT CODE").ReadOnly = True

        gv2.Columns("PRODUCT CODE").IsVisible = True
        gv2.Columns("PRODUCT CODE").Width = 50
        gv2.Columns("PRODUCT CODE").HeaderText = "PRODUCT CODE"
        '  gv2.Columns("PRODUCT CODE").ReadOnly = True

        gv2.Columns("PRODUCT NAME").IsVisible = True
        gv2.Columns("PRODUCT NAME").Width = 100
        gv2.Columns("PRODUCT NAME").HeaderText = "PRODUCT NAME"
        '  gv2.Columns("PRODUCT NAME").ReadOnly = True

        gv2.Columns("UOM").IsVisible = True
        gv2.Columns("UOM").Width = 30
        gv2.Columns("UOM").HeaderText = "UOM"
        '   gv2.Columns("UOM").ReadOnly = True

        gv2.Columns("BRANCH CODE").IsVisible = True
        gv2.Columns("BRANCH CODE").Width = 30
        gv2.Columns("BRANCH CODE").HeaderText = "BRANCH CODE"
        '  gv2.Columns("BRANCH CODE").ReadOnly = True

        gv2.Columns("BRANCH NAME").IsVisible = True
        gv2.Columns("BRANCH NAME").Width = 100
        gv2.Columns("BRANCH NAME").HeaderText = "BRANCH NAME"
        '  gv2.Columns("BRANCH NAME").ReadOnly = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        If branchVehiclesCols IsNot Nothing AndAlso branchVehiclesCols.Rows.Count > 0 Then
            For Each dr As DataRow In branchVehiclesCols.Rows
                Dim item1 As New GridViewSummaryItem(clsCommon.myCstr(dr(0)), "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
        End If
        Dim item3 As New GridViewSummaryItem("total_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        '= SPLITTING COLUMNS NAMES AND SET GRID VIEW COLUMNS CAPTIONS ================
        colsName = New List(Of String)()
        Dim splitColumnsName As String = Nothing
        If branchVehiclesCols IsNot Nothing AndAlso branchVehiclesCols.Rows.Count > 0 Then
            For Each dr As DataRow In branchVehiclesCols.Rows
                If clsCommon.myLen(dr(0)) > 0 Then
                    splitColumnsName = dr(0)
                    If (splitColumnsName.Contains("%###%")) Then
                        splitColumnsName = splitColumnsName.Replace("%###%", "" + Environment.NewLine + "")
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



        gv2.ShowGroupPanel = False
        gv2.MasterTemplate.AutoExpandGroups = True
        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


    End Sub

    Sub FormatGridforCustomer(ByVal dtExtraColumnForCustomer As DataTable)
        ' Dim strItemCode, head2 As String

        gv2.TableElement.TableHeaderHeight = 25
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = True
            gv2.Columns(ii).Width = 80

        Next

        gv2.Columns("Type").IsVisible = True
        gv2.Columns("Type").Width = 40
        gv2.Columns("Type").HeaderText = "Type"

        gv2.Columns("Route_No").IsVisible = False
        gv2.Columns("Route_No").Width = 40
        gv2.Columns("Route_No").HeaderText = "Route No"

        gv2.Columns("Route_Desc").IsVisible = True
        gv2.Columns("Route_Desc").Width = 40
        gv2.Columns("Route_Desc").HeaderText = "Route Name"

        gv2.Columns("To_location").IsVisible = False
        gv2.Columns("To_location").Width = 40
        gv2.Columns("To_location").HeaderText = "Location Code"

        gv2.Columns("Location_desc").IsVisible = False
        gv2.Columns("Location_desc").Width = 40
        gv2.Columns("Location_desc").HeaderText = "Location Description"

        gv2.Columns("Vehicle_Code").IsVisible = False
        gv2.Columns("Vehicle_Code").Width = 40
        gv2.Columns("Vehicle_Code").HeaderText = "Vehicle No"

        gv2.Columns("Vechicle_Name").IsVisible = True
        gv2.Columns("Vechicle_Name").Width = 100
        gv2.Columns("Vechicle_Name").HeaderText = "Vehicle Name"

        gv2.Columns("Cust_Code").IsVisible = False
        gv2.Columns("Cust_Code").Width = 100
        gv2.Columns("Cust_Code").HeaderText = "Customer Code"

        gv2.Columns("Customer_Name").IsVisible = True
        gv2.Columns("Customer_Name").Width = 100
        gv2.Columns("Customer_Name").HeaderText = "Customer Name"


        gv2.Columns("total_Qty").IsVisible = True
        gv2.Columns("total_Qty").Width = 100
        gv2.Columns("total_Qty").HeaderText = "Total Qty"

        gv2.Columns("DocumentAmount").IsVisible = False
        gv2.Columns("DocumentAmount").Width = 100
        gv2.Columns("DocumentAmount").HeaderText = "DocumentAmount"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim intCount As Integer = 0
        If dtExtraColumnForCustomer IsNot Nothing AndAlso dtExtraColumnForCustomer.Rows.Count > 0 Then
            For Each dr As DataRow In dtExtraColumnForCustomer.Rows
                Dim item1 As New GridViewSummaryItem(clsCommon.myCstr(dr(0)), "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
        End If

        Dim summaryItem1 As New GridViewSummaryItem()

        'Dim item2 As New GridViewSummaryItem("DocumentAmount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("total_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        gv2.GroupDescriptors.Add(New GridGroupByExpression("Route_No as Item format ""{0}: {1}"" Group By Route_No"))


        gv2.ShowGroupPanel = False
        gv2.MasterTemplate.AutoExpandGroups = True

        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'gv2.MasterTemplate.ShowTotals = True
    End Sub
    Sub FormatGridBranch(ByVal dtExtraColumnForBranch As DataTable)
        ' Dim strItemCode, head2 As String

        gv2.TableElement.TableHeaderHeight = 25
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = True
            gv2.Columns(ii).Width = 80

        Next

        gv2.Columns("Type").IsVisible = True
        gv2.Columns("Type").Width = 40
        gv2.Columns("Type").HeaderText = "Type"

        gv2.Columns("Route_No").IsVisible = False
        gv2.Columns("Route_No").Width = 40
        gv2.Columns("Route_No").HeaderText = "Route No"

        gv2.Columns("Route_Desc").IsVisible = True
        gv2.Columns("Route_Desc").Width = 40
        gv2.Columns("Route_Desc").HeaderText = "Route Name"

        gv2.Columns("To_location").IsVisible = False
        gv2.Columns("To_location").Width = 40
        gv2.Columns("To_location").HeaderText = "Location Code"

        gv2.Columns("Location_desc").IsVisible = True
        gv2.Columns("Location_desc").Width = 40
        gv2.Columns("Location_desc").HeaderText = "Location Description"

        gv2.Columns("Vehicle_Code").IsVisible = False
        gv2.Columns("Vehicle_Code").Width = 40
        gv2.Columns("Vehicle_Code").HeaderText = "Vehicle No"

        gv2.Columns("Vechicle_Name").IsVisible = True
        gv2.Columns("Vechicle_Name").Width = 100
        gv2.Columns("Vechicle_Name").HeaderText = "Vehicle Name"

        gv2.Columns("Cust_Code").IsVisible = False
        gv2.Columns("Cust_Code").Width = 100
        gv2.Columns("Cust_Code").HeaderText = "Customer Code"

        gv2.Columns("Customer_Name").IsVisible = False
        gv2.Columns("Customer_Name").Width = 100
        gv2.Columns("Customer_Name").HeaderText = "Customer Name"


        gv2.Columns("total_Qty").IsVisible = True
        gv2.Columns("total_Qty").Width = 100
        gv2.Columns("total_Qty").HeaderText = "Total Qty"


        gv2.Columns("DocumentAmount").IsVisible = False
        gv2.Columns("DocumentAmount").Width = 100
        gv2.Columns("DocumentAmount").HeaderText = "DocumentAmount"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim intCount As Integer = 0
        If dtExtraColumnForBranch IsNot Nothing AndAlso dtExtraColumnForBranch.Rows.Count > 0 Then
            For Each dr As DataRow In dtExtraColumnForBranch.Rows
                Dim item1 As New GridViewSummaryItem(clsCommon.myCstr(dr(0)), "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            Next
        End If

        Dim summaryItem1 As New GridViewSummaryItem()

        'Dim item2 As New GridViewSummaryItem("DocumentAmount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("total_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        gv2.GroupDescriptors.Add(New GridGroupByExpression("Route_No as Item format ""{0}: {1}"" Group By Route_No"))


        gv2.ShowGroupPanel = False
        gv2.MasterTemplate.AutoExpandGroups = True

        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'gv2.MasterTemplate.ShowTotals = True





    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPlantCustomerDemand & "'"))

            If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(MultLocation.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If
            If MultCustomer.arrValueMember IsNot Nothing AndAlso MultCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(MultCustomer.arrDispalyMember))
            End If
            If MultVehicle.arrValueMember IsNot Nothing AndAlso MultVehicle.arrValueMember.Count > 0 Then
                arrHeader.Add("Vehicle : " + clsCommon.GetMulcallStringWithComma(MultVehicle.arrDispalyMember))
            End If


            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv2, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv2, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs)
        print(EnumExportTo.PDF)
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            gv2.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv2.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv2.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub MultVehicle__My_Click(sender As Object, e As EventArgs) Handles MultVehicle._My_Click
        Dim qry As String = "select Number as Code ,Description as Name  from TSPL_VEHICLE_MASTER "
        MultVehicle.arrValueMember = clsCommon.ShowMultipleSelectForm("VEHICLE", qry, "Code", "Name", MultVehicle.arrValueMember, MultVehicle.arrDispalyMember)
    End Sub

    Private Sub rmPDF_Click_1(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
