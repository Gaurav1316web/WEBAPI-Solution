Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO


Public Class rptBookingReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
    End Sub
    Sub Reset()

        txtCustomer.arrValueMember = Nothing
        txtCustomer.arrDispalyMember = Nothing
        txtZone.arrValueMember = Nothing
        txtZone.arrDispalyMember = Nothing
        txtRoute.arrValueMember = Nothing
        txtRoute.arrDispalyMember = Nothing
        txtUOM.Value = ""
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        'RadGroupBox3.Enabled = True
        'gbDocStatus.Enabled = True
        'txtUOM.Enabled = True
        'txtCustomer.Enabled = True
        'txtZone.Enabled = True
        'txtRoute.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If rdbDay.Checked = True Then
            VarID += "_D"
        ElseIf rdbMonth.Checked = True Then
            VarID += "_M"
        ElseIf rdbYear.Checked = True Then
            VarID += "_Y"
        End If
        Gv1.VarID = VarID
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            GetReportGridID()
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable

            If clsCommon.myLen(txtUOM.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select UOM first", Me.Text)
                Exit Sub
            End If
            Dim strDateForPivot As String = ""
            Dim strDateWithIsNull As String = ""
            Dim strDateForTotal As String = ""
            Dim whr As String = " and 2= 2 and Is_Cancelled = 0 "
            Dim dateFormatewise As String = ""
            If rdbStatusPosted.Checked = True Then
                whr += " and  TSPL_BOOKING_MATSER.Posted  = 1 "
            ElseIf rdbStatusUnposted.Checked = True Then
                whr += " and  TSPL_BOOKING_MATSER.Posted  = 0 "
            Else
            End If

            If rdbDay.Checked = True Then
                strDateForPivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ',['+thedate+']'  from (select  convert (varchar,thedate,103) as thedate from ExplodeDates ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "')    ) XXX order by convert (date, thedate,103) asc   For XML Path('')),1,1,'') "))
                If clsCommon.myLen(strDateForPivot) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                strDateWithIsNull = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ',isnull (['+thedate+'],0) as ['+thedate+'] '  from (select  convert (varchar,thedate,103) as thedate from ExplodeDates ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "')    ) XXX order by convert (date, thedate,103) asc   For XML Path('')),1,1,'') "))
                strDateForTotal = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select '+ isnull (['+thedate+'],0)  ' from (select  convert (varchar,thedate,103) as thedate from ExplodeDates ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "')    ) XXX order by convert (date, thedate,103) asc   For XML Path('')),1,1,'') "))
                whr += " and convert (date, TSPL_BOOKING_MATSER.Document_Date,103) > =convert (date, '" + fromDate.Value + "',103)  and  convert (date,TSPL_BOOKING_MATSER.Document_Date,103)  < = convert (date, '" + ToDate.Value + "',103)   "
                dateFormatewise = "  convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103)  "
            ElseIf rdbMonth.Checked = True Then

                strDateForPivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ',['+thedate+']'  from (select  distinct  convert (varchar,DATENAME(month,(thedate)))+ '-'+ convert (varchar, year((thedate))) as thedate, month((thedate)) as MonthNo, year((thedate)) as YearNo from ExplodeDates ('" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "' )    ) XXX order by YearNo, MonthNo asc   For XML Path('')),1,1,'') "))
                If clsCommon.myLen(strDateForPivot) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If

                strDateWithIsNull = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ',isnull (['+thedate+'],0) as ['+thedate+'] '  from (select  distinct  convert (varchar,DATENAME(month,(thedate)))+ '-'+ convert (varchar, year((thedate))) as thedate, month((thedate)) as MonthNo, year((thedate)) as YearNo from ExplodeDates ('" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "' )    ) XXX order by YearNo, MonthNo asc   For XML Path('')),1,1,'') "))


                strDateForTotal = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select '+ isnull (['+thedate+'],0)  '  from (select  distinct  convert (varchar,DATENAME(month,(thedate)))+ '-'+ convert (varchar, year((thedate))) as thedate, month((thedate)) as MonthNo, year((thedate)) as YearNo from ExplodeDates ('" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "' )    ) XXX order by YearNo, MonthNo asc   For XML Path('')),1,1,'') "))

                Dim EndDayOfToDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select day( EOMONTH('" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy")) + "'))"))
                whr += " and convert (date, TSPL_BOOKING_MATSER.Document_Date,103) > =convert (date, '01-" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "MMM")) + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "yyyy")) + "',103)  and  convert (date,TSPL_BOOKING_MATSER.Document_Date,103)  < = convert (date, '" + EndDayOfToDate + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "MMM")) + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "yyyy")) + "',103)   "
                dateFormatewise = " convert (varchar, DATENAME( month ,TSPL_BOOKING_MATSER.Document_Date)) + '-'+ convert (varchar, Year (TSPL_BOOKING_MATSER.Document_Date))  "

            ElseIf rdbYear.Checked = True Then

                strDateForPivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ',['+thedate+']'  from (select  distinct   convert (varchar, year((thedate))) as thedate, year((thedate)) as YearNo from ExplodeDates ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "' )    ) XXX order by YearNo asc   For XML Path('')),1,1,'') "))
                If clsCommon.myLen(strDateForPivot) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If

                strDateWithIsNull = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ',isnull (['+thedate+'],0) as ['+thedate+'] '  from (select  distinct   convert (varchar, year((thedate))) as thedate, year((thedate)) as YearNo from ExplodeDates ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "' )    ) XXX order by YearNo asc   For XML Path('')),1,1,'') "))

                strDateForTotal = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select '+ isnull (['+thedate+'],0)  '  from (select  distinct   convert (varchar, year((thedate))) as thedate, year((thedate)) as YearNo from ExplodeDates ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "' )    ) XXX order by YearNo asc   For XML Path('')),1,1,'') "))
                whr += " and convert (date, TSPL_BOOKING_MATSER.Document_Date,103) > =convert (date, '01-Jan-" + fromDate.Text + "',103)  and  convert (date,TSPL_BOOKING_MATSER.Document_Date,103)  < = convert (date, '31-Dec-" + ToDate.Text + "',103)   "
                dateFormatewise = "  convert (varchar, Year (TSPL_BOOKING_MATSER.Document_Date))  "

            End If



            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                whr += " and TSPL_BOOKING_DETAIL.Cust_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
            End If
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                whr += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ") "
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                whr += " and TSPL_BOOKING_DETAIL.route_no in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If




            qry = "  select Customer_Code as [Customer Code] , Customer_Name as [Customer Name], Zone_Code as [Zone Code],Zone_Name as [Zone Name] , route_no as [Route Code], Route_Desc as [Route Name] , Uom, " + strDateForTotal + "  as [Grand Total] ,  " + strDateWithIsNull + " from (
                     select final .Document_Date, final.Customer_Code, max(final.Customer_Name) as Customer_Name , max( final.Zone_Code )  as Zone_Code , max(final.Zone_Name) as  Zone_Name, final.route_no ,max( final.Route_Desc) as  Route_Desc,max(final.Phone1)  as  [Mobile No] ,'" + clsCommon.myCstr(txtUOM.Value) + "' as Uom ,  sum(Final_Qty) as Qty  from (
                     select   TSPL_BOOKING_MATSER.Document_No as Document_Code,   " + dateFormatewise + " as Document_Date, TSPL_BOOKING_DETAIL.Cust_Code as Customer_Code ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Zone_Code , TSPL_ZONE_MASTER.Description as Zone_Name ,TSPL_CUSTOMER_MASTER.Phone1, TSPL_BOOKING_DETAIL.route_no ,TSPL_ROUTE_MASTER.Route_Desc  , TSPL_BOOKING_DETAIL.Item_Code, TSPL_BOOKING_DETAIL.Unit_code, convert (decimal(18,2) , (TSPL_BOOKING_DETAIL.Booking_Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty from TSPL_BOOKING_DETAIL 
                     left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No  = TSPL_BOOKING_DETAIL.Document_No
                     left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code
                     left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code 
				     left outer join TSPL_ROUTE_MASTER  on TSPL_ROUTE_MASTER.Route_No = TSPL_BOOKING_DETAIL.route_no
                     left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_BOOKING_DETAIL.Item_Code
                     left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_BOOKING_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_BOOKING_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                     Where 2=2  " + whr + " 
                     ) final group by final .Document_Date, final.Customer_Code , final.route_no
                     ) XFinal
                      pivot ( sum(XFinal.Qty) for Document_Date  in (" + strDateForPivot + ")  ) as Pivo; "


            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                ' Gv1.Columns("Trans Type").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2


                If Gv1.Rows.Count > 0 Then
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = 7 To Gv1.Columns.Count - 1
                        Dim aa = Gv1.Columns(i).HeaderText()
                        Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    Next

                    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                End If
                Gv1.EnableFiltering = True
                Gv1.BestFitColumns()

                'RadGroupBox3.Enabled = False
                'gbDocStatus.Enabled = False
                'txtUOM.Enabled = False
                'txtCustomer.Enabled = False
                'txtZone.Enabled = False
                'txtRoute.Enabled = False

            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBookingReport & "'"))

            If rdbDay.Checked = True Then
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Report Type : " & "Day wise")
            ElseIf rdbMonth.Checked = True Then
                arrHeader.Add(("Range: " + clsCommon.GetPrintDate(fromDate.Value, "MMM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "MMM/yyyy")) + " ")
                arrHeader.Add("Report Type : " & "Monthly")
            ElseIf rdbYear.Checked = True Then
                arrHeader.Add(("Range: " + clsCommon.GetPrintDate(fromDate.Value, "yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "yyyy")) + " ")
                arrHeader.Add("Report Type : " & "Yearly")
            End If
            If rdbStatusPosted.Checked = True Then
                arrHeader.Add("Booking Status : " & "Posted")
            ElseIf rdbStatusUnposted.Checked = True Then
                arrHeader.Add("Booking Status : " & "Unposted")
            Else
                arrHeader.Add("Booking Status : " & "Posted and Unposted")
            End If
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtCustomer.arrDispalyMember IsNot Nothing AndAlso txtCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If txtZone.arrDispalyMember IsNot Nothing AndAlso txtZone.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Zone : " + clsCommon.GetMulcallStringWithComma(txtZone.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Booking Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Booking Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster@CustomerWiseSalesReport", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " Select Cust_Code as Code, Customer_Name as Name from TSPL_CUSTOMER_MASTER "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustomerCode@CustWiseSaleRpt", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Dim qry As String = " select Zone_Code  as Code, Description as Name from TSPL_ZONE_MASTER "
        txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneCode@CustWiseSaleRpt", qry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
    End Sub

    Private Sub rdbDay_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDay.CheckedChanged
        Try
            If rdbDay.Checked = True Then
                fromDate.ShowUpDown = False
                ToDate.ShowUpDown = False
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "dd/MM/yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "dd/MM/yyyy"
            ElseIf rdbMonth.Checked = True Then
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "MMM/yyyy"
                fromDate.ShowUpDown = True
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "MMM/yyyy"
                ToDate.ShowUpDown = True
            ElseIf rdbYear.Checked = True Then
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "yyyy"
                fromDate.ShowUpDown = True
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "yyyy"
                ToDate.ShowUpDown = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbMonth_CheckedChanged(sender As Object, e As EventArgs) Handles rdbMonth.CheckedChanged
        Try
            If rdbDay.Checked = True Then
                fromDate.ShowUpDown = False
                ToDate.ShowUpDown = False
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "dd/MM/yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "dd/MM/yyyy"
            ElseIf rdbMonth.Checked = True Then
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "MMM/yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "MMM/yyyy"
            ElseIf rdbYear.Checked = True Then
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "yyyy"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbYear_CheckedChanged(sender As Object, e As EventArgs) Handles rdbYear.CheckedChanged
        Try
            If rdbDay.Checked = True Then
                fromDate.ShowUpDown = False
                ToDate.ShowUpDown = False
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "dd/MM/yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "dd/MM/yyyy"
            ElseIf rdbMonth.Checked = True Then
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "MMM/yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "MMM/yyyy"
            ElseIf rdbYear.Checked = True Then
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "yyyy"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim strQry As String = ""
        strQry = "select Route_No  as [Code],Route_Desc as [Name] from TSPL_ROUTE_MASTER"
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("txtRoute@BookingReport", strQry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub
End Class
