'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 03/09/2012-------------------------------------
'--------------------------------Last modify Time - 11:30 pm -------------------------------------

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Public Class FrmProvSaleExcel
    'Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Dim userCode, companyCode As String
    Dim sql As String
    ' Dim dr As SqlDataReader
    Dim dt As DataTable
    Public Delegate Sub ProgressBarValueDelegate(ByVal value As Integer)
    Private Delegate Sub CustomDelegate(ByVal obj As Object, ByVal text As String)

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                ' dr.Read()
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()
            Next
        End If
    End Sub


    Private Sub LoadData()
        Try
            GV1.EnableFiltering = True
            Dim strSQL1Group As String = String.Empty
            Dim strReportTitle As String = String.Empty
            Dim strOrderColumn As String = String.Empty
            Dim strOrderBy As String = String.Empty
            Dim strConverted As String = String.Empty
            Dim head1 As String = String.Empty
            Dim head2 As String = String.Empty
            Dim TDMCOdecolumn As String = String.Empty
            Dim group1 As String = String.Empty
            Dim additional As String = String.Empty
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                Return
            ElseIf chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Route or select ALL")
                Return

            ElseIf rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Company or select ALL")
                Return
            End If


            If rdbSku.IsChecked = True Then
                strSQL1Group = "TEMP_PROVISIONAL_SALES.Item_Code"
                strReportTitle = "Provisional Sale Sku wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Sku_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
            ElseIf rdbPack.IsChecked = True Then
                'strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc"
                strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc + ' ( ' +  convert(varchar(20),TSPL_ITEM_MASTER.Pack_Seq) +  ' ) '"
                strReportTitle = "Provisional Sale Pack wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Pack_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
            ElseIf rdbFlavour.IsChecked = True Then
                'strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
                strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc + ' ( ' +  convert(varchar(20),TSPL_ITEM_MASTER.Flavour_Seq) +  ' ) '"
                strReportTitle = "Provisional Sale Flavour wise"
                strOrderColumn = "TSPL_ITEM_MASTER.Flavour_Seq"
                strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"
            End If

            Dim strSubQry1 As String = String.Empty
            If ddlConvert.Text = "Converted" Then
                strSubQry1 = "(isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con'),1)"
                strConverted = "Converted"
            ElseIf ddlConvert.Text = "8oz" Then
                strSubQry1 = "(isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con'),1) * isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='8oz'),0)"
                strConverted = "8oz"
            ElseIf ddlConvert.Text = "Raw" Then
                'strSubQry1 = "isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Raw'),0)"
                strSubQry1 = "1"
                strConverted = "Raw"
            End If





            If ddlcategory.Text = "HOS" Then
                head2 = "HOS"
                TDMCOdecolumn = "HOS"
                group1 = "HOS"
                additional = "(HOS)"
            ElseIf ddlcategory.Text = "TDM" Then
                head2 = "TDM"
                TDMCOdecolumn = "TDM"
                group1 = "TDM"
                additional = "(TDM)"
            ElseIf ddlcategory.Text = "ADC" Then
                head2 = "ADC"
                TDMCOdecolumn = "ADC"
                group1 = "ADC"
                additional = "(ADC)"
            ElseIf ddlcategory.Text = "CE" Then
                head2 = "CE"
                TDMCOdecolumn = "CE"
                group1 = "CE"
                additional = "(CE)"
            ElseIf ddlcategory.Text = "SalesMan" Then
                head2 = "SalesMan"
                TDMCOdecolumn = "Salesmancode"
                group1 = "Salesmancode"
                additional = "(SalesMan)"

            End If


            Dim strItemCodestring As String = String.Empty
            Dim strItemCode As String = String.Empty
            Dim strMainItemCode As String = String.Empty
            Dim strmainItemCodeString As String = String.Empty
            Dim strPivot As String = String.Empty
            Dim strsum As String = String.Empty
            Dim dt As DataTable
            Dim str1 As String = String.Empty

            If rdbSku.IsChecked = True Then
                strPivot = "TSPL_TRANSFER_DETAIL.Item_Code"

            ElseIf rdbPack.IsChecked = True Then
                strPivot = "TSPL_ITEM_DETAILS.Class_Desc + ' ( ' +  convert(varchar(20),TSPL_ITEM_MASTER.Pack_Seq) +  ' ) '"
            ElseIf rdbFlavour.IsChecked = True Then
                strPivot = "TSPL_ITEM_DETAILS_1.Class_Desc + ' ( ' +  convert(varchar(20),TSPL_ITEM_MASTER.Flavour_Seq) +  ' ) '"
            End If
            If rdbDetail.IsChecked = True Then

                dt = clsDBFuncationality.GetDataTable("SELECT distinct " & strPivot & "," & strOrderColumn & " FROM TSPL_ITEM_DETAILS INNER JOIN " & _
                          "TSPL_TRANSFER_DETAIL ON TSPL_ITEM_DETAILS.Item_Code = TSPL_TRANSFER_DETAIL.Item_Code LEFT OUTER JOIN " & _
                          "TSPL_ITEM_MASTER ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code RIGHT OUTER JOIN " & _
                          "TSPL_TRANSFER_HEAD ON TSPL_TRANSFER_DETAIL.Transfer_No = TSPL_TRANSFER_HEAD.Transfer_No LEFT OUTER JOIN " & _
                          "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code WHERE " & _
                          "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= CONVERT(date, '" & dtpFdate.Value & "', 103) AND " & _
                "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= CONVERT(date, '" & dtpToDate.Value & "', 103)  and " & _
                "TSPL_ITEM_DETAILS.Class_Name='Size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour' " & strOrderBy & "   ")
                ' While dr.Read

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        strItemCode = CStr(dr(0).ToString())
                        strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","

                        strMainItemCode = CStr(dr(0).ToString())
                        strmainItemCodeString = strmainItemCodeString & "  isnull(" & "Sum(" & "[" & strItemCode & "]" & " ) " & ",0)  " & "as  " & "[" & strItemCode & "]" & ","

                        strsum = strsum & " isnull(" & "Sum(" & "[" & strItemCode & "]" & " ) " & ",0)" & "+"
                    Next
                End If
                ' End While
                If strItemCode <> "" Then

                    strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
                    strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)
                    strsum = strsum.Substring(0, strsum.Length - 1)

                Else
                    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                    Exit Sub
                End If

                str1 = "select max(heading2) as heading2,HierCode,Hier_Desc,HierDesc,Route_No,Route_Desc,Transfer_No,max(Transfer_Date) as Transfer_Date, " & _
                "max(ReportTitle) as ReportTitle, max(OrderBy) as OrderBy,max(Salesmancode) as Salesmancode,max(Emp_Name) as Emp_Name, " & _
                "max(Location) as Location,max(Location_Desc) as Location_Desc,max(Comp_Code) as Comp_Code,max(Comp_Name) as Comp_Name,MAX( Convertion) as Convertion,(" + strsum + ")as Total, " & strmainItemCodeString & " " & _
                "from  ( SELECT  " & strSQL1Group & " as Item_Code, '" + head2 + "' AS heading2, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES." + TDMCOdecolumn + " AS HierCode,TSPL_EMPLOYEE_MASTER_1.Emp_Name AS Hier_Desc, " & _
                "TSPL_EMPLOYEE_MASTER_1.Emp_Name AS HierDesc," & _
                "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Desc, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No, " & _
                " CONVERT(DECIMAL(18,2),case when TEMP_PROVISIONAL_SALES.Unit_Code <> 'SH' then ( " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.LoadOutQty / " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Conversion_Factor  " & _
                "- (ISNULL(" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.LoadInQty / " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Conversion_Factor, 0) + ISNULL(" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Breakage, 0) + " & _
                "ISNULL(" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Leak, 0) + ISNULL(" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Shortage, 0) )   ) *  " & strSubQry1 & " else 0 end)  AS sale," & _
                "'25/06/2012 4:25:33 PM' AS fromDate, '29/06/2012 4:25:33 PM' AS ToDate, '" & strReportTitle & "' AS ReportTitle, " & _
                "" & strOrderColumn & " AS OrderBy, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.RouteNo AS Route_No, convert(date," + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_Date,103) as Transfer_Date, " & _
                "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Salesmancode, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_Desc, " & _
                "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.LoadOut_Location as Location, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Comp_Code, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, 'Raw' AS Convertion, " & _
                "CONVERT(DECIMAL(18,2),((Loadout_Amount-LoadOut_EmptyValue) - (" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Amount-LoadIn_EmptyValue))) AS Value " & _
                "FROM  " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER RIGHT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER AS TSPL_EMPLOYEE_MASTER_1 RIGHT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER RIGHT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES ON " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code = " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Comp_Code ON " & _
                          "TSPL_EMPLOYEE_MASTER_1.EMP_CODE = " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES." + TDMCOdecolumn + " LEFT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code AND  " & _
                          "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Pack_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code ON " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Salesmancode LEFT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER ON TEMP_PROVISIONAL_SALES.RouteNo = " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No ON " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.LoadOut_Location LEFT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Flavour_Code = TSPL_ITEM_DETAILS_1.Class_Code AND  " & _
                          "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code = " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code " & _
                "WHERE convert(date,TEMP_PROVISIONAL_SALES.Transfer_Date,103) >= CONVERT(date, '" & dtpFdate.Value & "', 103) AND " & _
                "convert(date,TEMP_PROVISIONAL_SALES.Transfer_Date,103) <= CONVERT(date, '" & dtpToDate.Value & "', 103) and " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.RouteNo <> ''  "

                Dim strLocAll, strRouteAll As String
                If chkLocationAll.IsChecked = True Then
                    strLocAll = "Y"
                Else
                    strLocAll = "N"
                End If
                If chkRouteAll.IsChecked = True Then
                    strRouteAll = "Y"
                Else
                    strRouteAll = "N"
                End If


                If strLocAll = "N" Then
                    str1 += " and LoadOut_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                End If
                If strRouteAll = "N" Then
                    str1 += " and " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.RouteNo  in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                End If
                Dim str2 As String

                If ddlType.Text = "Quantity" Then
                    str2 = " ) down  pivot " & _
                    "(SUM(sale) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1 " & _
                    "group by Transfer_No,Route_No,Route_Desc,HierCode,Hier_Desc,HierDesc "
                Else
                    str2 = " ) down  pivot " & _
                    "(SUM(Value) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1 " & _
                    "group by Transfer_No,Route_No,Route_Desc,HierCode,Hier_Desc,HierDesc "
                End If


                Dim StrSql As String = str1 & str2

                Dim ArrDBName As ArrayList = Nothing
                If rbtnCompanyAll.IsChecked Then
                    ArrDBName = cbgCompany.AllValue
                Else
                    ArrDBName = cbgCompany.CheckedValue
                End If
                Dim qry As String = clsCommon.GetQueryWithAllSelectedDataBase(StrSql, ArrDBName, False)

                dt = clsDBFuncationality.GetDataTable(qry)
                GV1.DataSource = Nothing
                GV1.Columns.Clear()
                GV1.Rows.Clear()
                GV1.GroupDescriptors.Clear()
                GV1.MasterTemplate.SummaryRowsBottom.Clear()

                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                GV1.DataSource = dt
                SetGridFormationOFGV1()

            End If

            If rdbSummary.IsChecked = True Then

                dt = clsDBFuncationality.GetDataTable("SELECT distinct " & strPivot & " ," & strOrderColumn & " FROM TSPL_ITEM_DETAILS INNER JOIN " & _
                          "TSPL_TRANSFER_DETAIL ON TSPL_ITEM_DETAILS.Item_Code = TSPL_TRANSFER_DETAIL.Item_Code LEFT OUTER JOIN " & _
                          "TSPL_ITEM_MASTER ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code RIGHT OUTER JOIN " & _
                          "TSPL_TRANSFER_HEAD ON TSPL_TRANSFER_DETAIL.Transfer_No = TSPL_TRANSFER_HEAD.Transfer_No LEFT OUTER JOIN " & _
                          "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code WHERE " & _
                          "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= CONVERT(date, '" & dtpFdate.Value & "', 103) AND " & _
                "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= CONVERT(date, '" & dtpToDate.Value & "', 103)  and " & _
                "TSPL_ITEM_DETAILS.Class_Name='Size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour' " & strOrderBy & "   ")

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        'While dr.Read
                        strItemCode = CStr(dr(0).ToString())
                        strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","

                        strMainItemCode = CStr(dr(0).ToString())
                        strmainItemCodeString = strmainItemCodeString & "  isnull(" & "Sum(" & "[" & strItemCode & "]" & " ) " & ",0)  " & "as  " & "[" & strItemCode & "]" & ","
                        strsum = strsum & " isnull(" & "Sum(" & "[" & strItemCode & "]" & " ) " & ",0)" & "+"
                    Next
                End If
                'End While
                If strItemCode <> "" Then

                    strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
                    strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)
                    strsum = strsum.Substring(0, strsum.Length - 1)
                Else
                    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                    Exit Sub
                End If

                str1 = "select max(heading2) as heading2,HierCode,Hier_Desc,HierDesc,Route_No,Route_Desc,max(Transfer_No) as Transfer_No,max(Transfer_Date) as Transfer_Date, " & _
                "max(ReportTitle) as ReportTitle, max(OrderBy) as OrderBy,max(Salesmancode) as Salesmancode,max(Emp_Name) as Emp_Name, " & _
                "max(Location) as Location,max(Location_Desc) as Location_Desc,max(Comp_Code) as Comp_Code,max(Comp_Name) as Comp_Name,MAX( Convertion) as Convertion,(" + strsum + ")as Total, " & strmainItemCodeString & " " & _
                "from  ( SELECT  " & strSQL1Group & " as Item_Code, '" + head2 + "' AS heading2, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES." + TDMCOdecolumn + " AS HierCode,TSPL_EMPLOYEE_MASTER_1.Emp_Name AS Hier_Desc, " & _
                "TSPL_EMPLOYEE_MASTER_1.Emp_Name AS HierDesc," & _
                "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Desc, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No, " & _
                " CONVERT(DECIMAL(18,2),case when TEMP_PROVISIONAL_SALES.Unit_Code <> 'SH' then ( " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.LoadOutQty / " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Conversion_Factor  " & _
                "- (ISNULL(" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.LoadInQty / " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Conversion_Factor, 0) + ISNULL(" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Breakage, 0) + " & _
                "ISNULL(" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Leak, 0) + ISNULL(" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Shortage, 0) )   ) *  " & strSubQry1 & " else 0 end)  AS sale," & _
                "'25/06/2012 4:25:33 PM' AS fromDate, '29/06/2012 4:25:33 PM' AS ToDate, '" & strReportTitle & "' AS ReportTitle, " & _
                "" & strOrderColumn & " AS OrderBy, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.RouteNo AS Route_No, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_Date, " & _
                "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Salesmancode, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_Desc, " & _
                "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.LoadOut_Location as Location, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Comp_Code, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, 'Raw' AS Convertion, " & _
                "CONVERT(DECIMAL(18,2),((Loadout_Amount-LoadOut_EmptyValue) - (" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Amount-LoadIn_EmptyValue))) AS Value " & _
                "FROM  " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER RIGHT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER AS TSPL_EMPLOYEE_MASTER_1 RIGHT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER RIGHT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES ON " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code = " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Comp_Code ON " & _
                          "TSPL_EMPLOYEE_MASTER_1.EMP_CODE = " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES." + TDMCOdecolumn + " LEFT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code AND  " & _
                          "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Pack_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code ON " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Salesmancode LEFT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER ON TEMP_PROVISIONAL_SALES.RouteNo = " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No ON " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.LoadOut_Location LEFT OUTER JOIN " & _
                          "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Flavour_Code = TSPL_ITEM_DETAILS_1.Class_Code AND  " & _
                          "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code = " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code " & _
                "WHERE convert(date,TEMP_PROVISIONAL_SALES.Transfer_Date,103) >= CONVERT(date, '" & dtpFdate.Value & "', 103) AND " & _
                "convert(date,TEMP_PROVISIONAL_SALES.Transfer_Date,103) <= CONVERT(date, '" & dtpToDate.Value & "', 103) and " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.RouteNo <> ''  "

                Dim strLocAll, strRouteAll As String
                If chkLocationAll.IsChecked = True Then
                    strLocAll = "Y"
                Else
                    strLocAll = "N"
                End If
                If chkRouteAll.IsChecked = True Then
                    strRouteAll = "Y"
                Else
                    strRouteAll = "N"
                End If


                If strLocAll = "N" Then
                    str1 += " and LoadOut_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                End If
                If strRouteAll = "N" Then
                    str1 += " and " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.RouteNo  in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                End If
                Dim str2 As String

                If ddlType.Text = "Quantity" Then
                    str2 = " ) down  pivot " & _
                    "(SUM(sale) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1 " & _
                    "group by Route_No,Route_Desc,HierCode,Hier_Desc,HierDesc "
                Else
                    str2 = " ) down  pivot " & _
                    "(SUM(Value) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1 " & _
                    "group by Route_No,Route_Desc,HierCode,Hier_Desc,HierDesc "
                End If

                Dim StrSql As String = str1 & str2

                Dim ArrDBName As ArrayList = Nothing
                If rbtnCompanyAll.IsChecked Then
                    ArrDBName = cbgCompany.AllValue
                Else
                    ArrDBName = cbgCompany.CheckedValue
                End If
                Dim qry As String = clsCommon.GetQueryWithAllSelectedDataBase(StrSql, ArrDBName, False)

                dt = clsDBFuncationality.GetDataTable(qry)
                GV1.DataSource = Nothing
                GV1.Columns.Clear()
                GV1.Rows.Clear()
                GV1.GroupDescriptors.Clear()
                GV1.MasterTemplate.SummaryRowsBottom.Clear()

                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                GV1.DataSource = dt
                SetGridFormationOFGV1()
            End If
        Catch ex As Exception
            If ex.Message.Substring(0, 10) = "The column" Then
                If rdbPack.IsChecked = True Then
                    MsgBox("Please check Pack Seq no ")
                ElseIf rdbFlavour.IsChecked = True Then
                    MsgBox("Please check Flavour Seq no ")
                End If
            Else
                common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        End Try
    End Sub


    Sub SetGridFormationOFGV1()
        Dim strItemCode As String = String.Empty
        Dim head2 As String = String.Empty

        If ddlcategory.Text = "HOS" Then
            head2 = "HOS"
        ElseIf ddlcategory.Text = "TDM" Then
            head2 = "TDM"
        ElseIf ddlcategory.Text = "ADC" Then
            head2 = "ADC"
        ElseIf ddlcategory.Text = "CE" Then
            head2 = "CE"
        ElseIf ddlcategory.Text = "SalesMan" Then
            head2 = "SalesMan"
        End If

        GV1.TableElement.TableHeaderHeight = 40
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).IsVisible = False
        Next

        If rdbSummary.IsChecked = True Then
            GV1.Columns("heading2").IsVisible = False
            GV1.Columns("heading2").Width = 50
            GV1.Columns("heading2").HeaderText = "Hier level"

            GV1.Columns("HierCode").IsVisible = False
            GV1.Columns("HierCode").Width = 50
            GV1.Columns("HierCode").HeaderText = "HierCode"

            GV1.Columns("Hier_Desc").IsVisible = True
            GV1.Columns("Hier_Desc").Width = 200
            GV1.Columns("Hier_Desc").HeaderText = "(" & head2 & ")  " & "Name"
            'GV1.Columns("Hier_Desc").BestFit()
            GV1.Columns("Hier_Desc").PinPosition = PinnedColumnPosition.Left

            GV1.Columns("HierDesc").IsVisible = True
            GV1.Columns("HierDesc").Width = 100
            GV1.Columns("HierDesc").HeaderText = ""

            GV1.Columns("Route_No").IsVisible = True
            GV1.Columns("Route_No").Width = 70
            GV1.Columns("Route_No").HeaderText = "Route"
            'GV1.Columns("Route_No").BestFit()
            GV1.Columns("Route_No").PinPosition = PinnedColumnPosition.Left


            GV1.Columns("Route_Desc").IsVisible = True
            GV1.Columns("Route_Desc").Width = 150
            GV1.Columns("Route_Desc").HeaderText = "Desc"
            'GV1.Columns("Route_Desc").BestFit()
            GV1.Columns("Route_Desc").PinPosition = PinnedColumnPosition.Left


            GV1.Columns("Transfer_No").IsVisible = False
            GV1.Columns("Transfer_No").Width = 100
            GV1.Columns("Transfer_No").HeaderText = "Transfer No"


            GV1.Columns("Transfer_Date").IsVisible = False
            GV1.Columns("Transfer_Date").Width = 70
            GV1.Columns("Transfer_Date").HeaderText = "Transfer Date"


            GV1.Columns("ReportTitle").IsVisible = False
            GV1.Columns("ReportTitle").Width = 70
            GV1.Columns("ReportTitle").HeaderText = "Report Title"

            GV1.Columns("OrderBy").IsVisible = False
            GV1.Columns("OrderBy").Width = 70
            GV1.Columns("OrderBy").HeaderText = "OrderBy"

            GV1.Columns("Salesmancode").IsVisible = False
            GV1.Columns("Salesmancode").Width = 50
            GV1.Columns("Salesmancode").HeaderText = "Salesman Code"

            GV1.Columns("Emp_Name").IsVisible = False
            GV1.Columns("Emp_Name").Width = 50
            GV1.Columns("Emp_Name").HeaderText = "Salesman Name"

            GV1.Columns("Location").IsVisible = False
            GV1.Columns("Location").Width = 50
            GV1.Columns("Location").HeaderText = "Location"

            GV1.Columns("Location_Desc").IsVisible = False
            GV1.Columns("Location_Desc").Width = 80
            GV1.Columns("Location_Desc").HeaderText = "Location Desc"

            GV1.Columns("Comp_Code").IsVisible = False
            GV1.Columns("Comp_Code").Width = 80
            GV1.Columns("Comp_Code").HeaderText = "Comp Code"

            GV1.Columns("Comp_Name").IsVisible = False
            GV1.Columns("Comp_Name").Width = 80
            GV1.Columns("Comp_Name").HeaderText = "Comp Name"

            GV1.Columns("Convertion").IsVisible = False
            GV1.Columns("Convertion").Width = 80
            GV1.Columns("Convertion").HeaderText = "Convertion"

            GV1.Columns("Total").IsVisible = True
            GV1.Columns("Total").Width = 80
            GV1.Columns("Total").HeaderText = "Total"
            'GV1.Columns("Total").BestFit()
            GV1.Columns("Total").PinPosition = PinnedColumnPosition.Left

            For ii As Integer = 18 To GV1.Columns.Count - 1
                strItemCode = GV1.Columns(ii).FieldName
                GV1.Columns("" & strItemCode & "").IsVisible = True
                GV1.Columns("" & strItemCode & "").Width = 80
                GV1.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
            Next





            GV1.GroupDescriptors.Add(New GridGroupByExpression("HierDesc as HierDesc format ""{0}: {1}"" Group By HierDesc"))
            GV1.MasterTemplate.ExpandAllGroups()
            GV1.ShowGroupPanel = False
            GV1.MasterTemplate.AutoExpandGroups = True
        End If
        'GV1.MasterTemplate.SummaryRowsBottom = True

        'End If
        If rdbDetail.IsChecked = True Then
            GV1.Columns("heading2").IsVisible = False
            GV1.Columns("heading2").Width = 50
            GV1.Columns("heading2").HeaderText = "Hier level"

            GV1.Columns("HierCode").IsVisible = False
            GV1.Columns("HierCode").Width = 50
            GV1.Columns("HierCode").HeaderText = "HierCode"

            GV1.Columns("Hier_Desc").IsVisible = True
            GV1.Columns("Hier_Desc").Width = 100
            GV1.Columns("Hier_Desc").HeaderText = "(" & head2 & ")  " & "Name"
            'GV1.Columns("Hier_Desc").BestFit()
            GV1.Columns("Hier_Desc").PinPosition = PinnedColumnPosition.Left

            GV1.Columns("HierDesc").IsVisible = False
            GV1.Columns("HierDesc").Width = 100
            GV1.Columns("HierDesc").HeaderText = ""

            GV1.Columns("Route_No").IsVisible = True
            GV1.Columns("Route_No").Width = 30
            GV1.Columns("Route_No").HeaderText = "Route"
            'GV1.Columns("Route_No").BestFit()
            GV1.Columns("Route_No").PinPosition = PinnedColumnPosition.Left


            GV1.Columns("Route_Desc").IsVisible = True
            GV1.Columns("Route_Desc").Width = 100
            GV1.Columns("Route_Desc").HeaderText = "Desc"
            'GV1.Columns("Route_Desc").BestFit()
            GV1.Columns("Route_Desc").PinPosition = PinnedColumnPosition.Left


            GV1.Columns("Transfer_No").IsVisible = True
            GV1.Columns("Transfer_No").Width = 100
            GV1.Columns("Transfer_No").HeaderText = "Transfer No"
            'GV1.Columns("Transfer_No").BestFit()
            GV1.Columns("Transfer_No").PinPosition = PinnedColumnPosition.Left

            GV1.Columns("Transfer_Date").IsVisible = True
            GV1.Columns("Transfer_Date").Width = 50
            GV1.Columns("Transfer_Date").HeaderText = "Transfer Date"
            'GV1.Columns("Transfer_Date").BestFit()
            GV1.Columns("Transfer_Date").PinPosition = PinnedColumnPosition.Left


            GV1.Columns("ReportTitle").IsVisible = False
            GV1.Columns("ReportTitle").Width = 70
            GV1.Columns("ReportTitle").HeaderText = "Report Title"


            GV1.Columns("OrderBy").IsVisible = False
            GV1.Columns("OrderBy").Width = 70
            GV1.Columns("OrderBy").HeaderText = "OrderBy"

            GV1.Columns("Salesmancode").IsVisible = True
            GV1.Columns("Salesmancode").Width = 50
            GV1.Columns("Salesmancode").HeaderText = "Salesman Code"
            'GV1.Columns("Salesmancode").BestFit()
            GV1.Columns("Salesmancode").PinPosition = PinnedColumnPosition.Left

            GV1.Columns("Emp_Name").IsVisible = True
            GV1.Columns("Emp_Name").Width = 80
            GV1.Columns("Emp_Name").HeaderText = "Salesman Name"
            'GV1.Columns("Emp_Name").BestFit()
            GV1.Columns("Emp_Name").PinPosition = PinnedColumnPosition.Left

            GV1.Columns("Location").IsVisible = True
            GV1.Columns("Location").Width = 50
            GV1.Columns("Location").HeaderText = "Location"
            'GV1.Columns("Location").BestFit()
            GV1.Columns("Location").PinPosition = PinnedColumnPosition.Left


            GV1.Columns("Location_Desc").IsVisible = True
            GV1.Columns("Location_Desc").Width = 80
            GV1.Columns("Location_Desc").HeaderText = "Location Desc"
            'GV1.Columns("Location_Desc").BestFit()
            GV1.Columns("Location_Desc").PinPosition = PinnedColumnPosition.Left

            GV1.Columns("Comp_Code").IsVisible = False
            GV1.Columns("Comp_Code").Width = 80
            GV1.Columns("Comp_Code").HeaderText = "Comp Code"

            GV1.Columns("Comp_Name").IsVisible = False
            GV1.Columns("Comp_Name").Width = 80
            GV1.Columns("Comp_Name").HeaderText = "Comp Name"

            GV1.Columns("Convertion").IsVisible = False
            GV1.Columns("Convertion").Width = 80
            GV1.Columns("Convertion").HeaderText = "Convertion"

            GV1.Columns("Total").IsVisible = True
            GV1.Columns("Total").Width = 80
            GV1.Columns("Total").HeaderText = "Total"
            'GV1.Columns("Total").BestFit()
            GV1.Columns("Total").PinPosition = PinnedColumnPosition.Left



            For ii As Integer = 18 To GV1.Columns.Count - 1
                strItemCode = GV1.Columns(ii).FieldName
                GV1.Columns("" & strItemCode & "").IsVisible = True
                GV1.Columns("" & strItemCode & "").Width = 80
                GV1.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
            Next





            'GV1.GroupDescriptors.Add(New GridGroupByExpression("HierDesc as HierDesc format ""{0}: {1}"" Group By HierDesc"))
            'GV1.MasterTemplate.ExpandAllGroups()
            'GV1.ShowGroupPanel = True
            'GV1.MasterTemplate.AutoExpandGroups = True
        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        For ii As Integer = 18 To GV1.Columns.Count - 1
            intCount = intCount + 1
            strItemCode = GV1.Columns(ii).FieldName
            Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item16)
        Next
        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        GV1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles GV1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    'Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
    '    LoadData()
    '    ExportToExcel()
    'End Sub

    Private Sub ExportToExcel()
        Try
            Dim strReportTitle As String = String.Empty
            If rdbSummary.IsChecked Then
                If rdbSku.IsChecked = True Then
                    strReportTitle = "Prov Sale Summary Sku wise"
                ElseIf rdbPack.IsChecked = True Then
                    strReportTitle = "Prov Sale Summary Pack wise"
                ElseIf rdbFlavour.IsChecked = True Then
                    strReportTitle = "Prov Sale Summary Flavour wise"
                End If
            ElseIf rdbDetail.IsChecked = True Then
                If rdbSku.IsChecked = True Then
                    strReportTitle = "Prov Sale Detail Sku wise"
                ElseIf rdbPack.IsChecked = True Then
                    strReportTitle = "Prov Sale Detail pack wise"
                ElseIf rdbFlavour.IsChecked = True Then
                    strReportTitle = "Prov Sale Detail Flavour wise"
                End If
            End If
            Dim saveDialog1 As New SaveFileDialog()
            saveDialog1.FileName = strReportTitle
            saveDialog1.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"
            Dim Fullpath As String

            Dim path = "C:\\ERPTempFolder"
            Dim IsExists As Boolean = System.IO.Directory.Exists(path)
            If IsExists = False Then
                System.IO.Directory.CreateDirectory(path)
            End If

            Fullpath = path + "\" + saveDialog1.FileName
            Dim i As Integer = 0
            For i = 0 To GV1.ColumnCount - 1
                Dim grow As GridViewRowInfo = TryCast(GV1.Rows(0), GridViewRowInfo)
                If TypeOf grow.Cells(i).Value Is DateTime Then
                    Dim datecol As GridViewDateTimeColumn = TryCast(GV1.Columns(i), GridViewDateTimeColumn)
                    datecol.ExcelExportType = DisplayFormatType.ShortDate
                End If
            Next i
            Dim exporter As New ExportToExcelML(GV1)
            exporter.SummariesExportOption = SummariesOption.ExportAll
            'If rdbSummary.IsChecked = True Then
            exporter.ExportVisualSettings = True
            'End If
            exporter.ExportHierarchy = True
            exporter.HiddenColumnOption = HiddenOption.DoNotExport
            exporter.SheetMaxRows = ExcelMaxRows._1048576
            AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
            AddHandler exporter.ExcelTableCreated, AddressOf exporter_ExcelTableCreated
            exporter.SheetName = strReportTitle
            exporter.RunExport(Fullpath)
            Me.Controls.Remove(GV1)
            Dim xlsApp As Microsoft.Office.Interop.Excel.Application
            Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
            xlsApp = New Microsoft.Office.Interop.Excel.Application
            xlsApp.Visible = True
            xlsWB = xlsApp.Workbooks.Open(Fullpath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub RunExportToExcelML(ByVal fileName As Object)
        Try
            Dim exporter As New ExportToExcelML(gv1)
            exporter.SummariesExportOption = SummariesOption.ExportAll
            'If rdbSummary.IsChecked = True Then
            '    exporter.ExportVisualSetting = True
            'End If
            exporter.ExportHierarchy = True
            exporter.HiddenColumnOption = HiddenOption.DoNotExport
            exporter.SheetMaxRows = ExcelMaxRows._1048576
            AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
            AddHandler exporter.ExcelTableCreated, AddressOf exporter_ExcelTableCreated
            exporter.RunExport(fileName.ToString())
            Dim text As String = "Export finished successfully!"

            Dim xlApp As Excel.Application
            xlApp = New Excel.Application
            Process.Start(fileName.ToString())

            common.clsCommon.MyMessageBoxShow(text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    '''' <summary>        
    '''' '''' using ExcelCellFormatting event for updating progress bar and applying custom format in excel file        
    '''' '''' </summary>        

    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelCellFormattingEventArgs)
        If e.GridRowInfoType Is GetType(GridViewDataRowInfo) Then
        End If
    End Sub

    '''' <summary>        
    '''' '''' using ExcelTableCreated event for adding custom header row        
    '''' '''' </summary>        

    Private Sub exporter_ExcelTableCreated(ByVal sender As Object, ByVal e As ExcelTableCreatedEventArgs)
        Dim strReportTitle As String = String.Empty
        Dim strConverted As String = String.Empty
        Dim strOrderby As String = String.Empty
        Dim head2 As String = String.Empty
        Dim strSummary As String = String.Empty
        Dim strQty As String = String.Empty

        If rdbSku.IsChecked = True Then
            strReportTitle = "Provisional Sale Sku wise"
            strOrderby = "SKU - WISE Figures"

        ElseIf rdbPack.IsChecked = True Then
            strReportTitle = "Provisional Sale Pack wise"
            strOrderby = "PACK - WISE Figures"

        ElseIf rdbFlavour.IsChecked = True Then
            strReportTitle = "Provisional Sale Flavour wise"
            strOrderby = "FLAVOUR - WISE Figures"

        End If

        If ddlConvert.Text = "Converted" Then
            strConverted = "Converted"
        ElseIf ddlConvert.Text = "8oz" Then
            strConverted = "8oz"
        ElseIf ddlConvert.Text = "Raw" Then
            strConverted = "Raw"
        End If

        If ddlcategory.Text = "HOS" Then
            head2 = "HOS"
        ElseIf ddlcategory.Text = "TDM" Then
            head2 = "TDM"
        ElseIf ddlcategory.Text = "ADC" Then
            head2 = "ADC"
        ElseIf ddlcategory.Text = "CE" Then
            head2 = "CE"
        ElseIf ddlcategory.Text = "SalesMan" Then
            head2 = "SalesMan"
        End If

        If rdbSummary.IsChecked = True Then
            strSummary = "Summary"
        Else
            strSummary = "Detail"
        End If
        If ddlType.Text = "Quantity" Then
            strQty = "(Qty Wise)"
        Else
            strQty = "(Value Wise)"
        End If
        If e.SheetIndex = 0 Then 'add header row only for the first excel sheet                
            'Dim style As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 30, "Provisional Sale Report")
            'style.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center
            'style.AlignmentElement.VerticalAlignment = VerticalAlignmentType.Center
            'style.InteriorStyle.Pattern = InteriorPatternType.Solid
            ''style.InteriorStyle.Color = Color.Red
            'style.FontStyle.Color = Color.Black
            'style.FontStyle.Bold = True
            'style.FontStyle.Size = 16
            Dim style1 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, strSummary & strQty)
            Dim style2 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, strOrderby & strConverted)
            Dim style3 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Provisiaonal Sale Report : " + head2)
            Dim style4 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Start Date : = " + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy"))
            Dim style5 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "End Date : = " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))

            If chkLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
                Dim style6 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Location : " + strLoca)

            End If

        End If
    End Sub

    Private Sub btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprint.Click
        print()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Export()
    End Sub
    Sub Export()
        If GV1.Rows.Count > 0 Then
            ExportToExcel()
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadData()
    End Sub
    Sub print()
        Dim strSQL1Group As String = String.Empty
        Dim strReportTitle As String = String.Empty
        Dim strOrderColumn As String = String.Empty
        Dim strOrderBy As String = String.Empty
        Dim strConverted As String = String.Empty
        Dim head1 As String = String.Empty
        Dim head2 As String = String.Empty
        Dim TDMCOdecolumn As String = String.Empty
        Dim group1 As String = String.Empty
        Dim additional As String = String.Empty
        Dim LocFilter As String = String.Empty


        If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            Return


        ElseIf chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Route or select ALL")
            Return

        ElseIf rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Company or select ALL")
            Return
        End If
        If cbgLocation.CheckedValue.Count > 0 Then
            LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
            LocFilter = LocFilter.Replace("'", "")
        End If

        If rdbSku.IsChecked = True Then
            strSQL1Group = "TSPL_TRANSFER_DETAIL.Item_Code"
            strReportTitle = "Provisional Sale Sku wise"
            strOrderColumn = "TSPL_ITEM_MASTER.Sku_Seq"
            strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
        ElseIf rdbPack.IsChecked = True Then
            strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc"
            strReportTitle = "Provisional Sale Pack wise"
            strOrderColumn = "TSPL_ITEM_MASTER.Pack_Seq"
            strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
        ElseIf rdbFlavour.IsChecked = True Then
            strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
            strReportTitle = "Provisional Sale Flavour wise"
            strOrderColumn = "TSPL_ITEM_MASTER.Flavour_Seq"
            strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"
        End If

        Dim strSubQry1 As String = String.Empty
        If ddlConvert.Text = "Converted" Then
            strSubQry1 = "(isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con'),1)"
            strConverted = "Converted"
        ElseIf ddlConvert.Text = "8oz" Then
            strSubQry1 = "(isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con'),1) * isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='8oz'),0)"
            strConverted = "8oz"
        ElseIf ddlConvert.Text = "Raw" Then
            'strSubQry1 = "isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TEMP_PROVISIONAL_SALES.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Raw'),0)"
            strSubQry1 = "1"
            strConverted = "Raw"
        End If





        If ddlcategory.Text = "HOS" Then
            head2 = "HOS"
            TDMCOdecolumn = "HOS"
            group1 = "HOS"
            additional = "(HOS)"
        ElseIf ddlcategory.Text = "TDM" Then
            head2 = "TDM"
            TDMCOdecolumn = "TDM"
            group1 = "TDM"
            additional = "(TDM)"
        ElseIf ddlcategory.Text = "ADC" Then
            head2 = "ADC"
            TDMCOdecolumn = "ADC"
            group1 = "ADC"
            additional = "(ADC)"
        ElseIf ddlcategory.Text = "CE" Then
            head2 = "CE"
            TDMCOdecolumn = "CE"
            group1 = "CE"
            additional = "(CE)"

        ElseIf ddlcategory.Text = "CE" Then
            head2 = "CE"
            TDMCOdecolumn = "CE"
            group1 = "CE"
            additional = "(CE)"

        ElseIf ddlcategory.Text = "SalesMan" Then
            head2 = "SalesMan"
            TDMCOdecolumn = "Salesmancode"
            group1 = "Salesmancode"
            additional = "(SalesMan)"

        End If




        '---------------By vipin for hierarchry--------------------------------
        'Dim StrSql As String = "select " & strSQL1Group & " as Group1,'" + head2 + "' as heading2," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " as HierCode,TSPL_EMPLOYEE_MASTER_1.Emp_Name AS HierDesc," + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Desc ," + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No, " & _
        '           "   case when TEMP_PROVISIONAL_SALES.unit_code <> 'SH' then (LoadOutQty/Conversion_Factor - (isnull(LoadInQty/Conversion_Factor,0) + isnull(TEMP_PROVISIONAL_SALES.Breakage,0)+isnull(TEMP_PROVISIONAL_SALES.Leak,0) + isnull(TEMP_PROVISIONAL_SALES.Shortage,0))) *  " & strSubQry1 & " else 0 end as sale, " & _
        '           "'" & dtpFdate.Value & "' as fromDate,'" & dtpToDate.Value & "' as ToDate,'" & strReportTitle & "' as ReportTitle," & strOrderColumn & " as OrderBy, " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_Date," + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Salesmancode, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name, " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_Desc, " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.LoadOut_Location, " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code, TSPL_COMPANY_MASTER.Comp_Name,'" & strConverted & "' AS Convertion,isnull(((BasicPrice_WithTax + Empty_Value + TPT_Value) * LoadoutQty),0) - " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Amount as Value FROM " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER  as TSPL_EMPLOYEE_MASTER_1 ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " = TSPL_EMPLOYEE_MASTER_1.EMP_CODE INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code AND  " & _
        '           "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Pack_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Flavour_Code = TSPL_ITEM_DETAILS_1.Class_Code AND " & _
        '           "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No AND " & _
        '           "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No AND " & _
        '           "" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code  AND " & _
        '           " " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.MRP = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.MRP LEFT OUTER JOIN " & _
        '           "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No = " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No " & _
        '           "where " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_Date > = CONVERT(date,'" & dtpFdate.Value & "',103) and " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_Date < = CONVERT(date,'" & dtpToDate.Value & "',103) "

        Dim strSql1 As String = "SELECT  '" + LocFilter + "' as LocFilter,   " & strSQL1Group & " AS Group1, '" + head2 + "' AS heading2, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " AS HierCode, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS HierDesc," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No, " & _
        " (case when Uom <> 'SH' then (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end) *  " & strSubQry1 & " AS Sale, 0 AS LoadIn, '" & dtpFdate.Value & "' AS fromDate, '" & dtpToDate.Value & "' AS Todate, " & _
        "'" & strReportTitle & "' AS ReportTitle, " & strOrderColumn & " AS OrderBy, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode, TSPL_EMPLOYEE_MASTER_1.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Desc, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location AS LoadOut_Location, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.FromLoc_Desc AS Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, '" & strConverted & "' AS Convertion, " & _
        "(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty *  " & strSubQry1 & ") * (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax +  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TPT_Value) " & _
        "AS Value, 0 AS Inamt, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code " & _
        "FROM " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER INNER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + "  LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER AS TSPL_EMPLOYEE_MASTER_1 ON " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode = TSPL_EMPLOYEE_MASTER_1.EMP_CODE LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL RIGHT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code AND " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom ON  " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
                    "  " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " & _
                    "  " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
                    "  " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
                    "  " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
        " WHERE     (TSPL_TRANSFER_HEAD.Transfer_Type = 'LO') AND (TSPL_LOCATION_MASTER.Location_Type = 'logical')  and TSPL_ITEM_DETAILS.Class_Name='Size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour' and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
        "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date, '" & dtpToDate.Value & "',103) and unit_code <> 'sh' "
        Dim Un1 As String = "Union All "
        Dim strSql2 As String = "SELECT   '" + LocFilter + "' as LocFilter,  " & strSQL1Group & " AS Group1, '" + head2 + "' AS heading2, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " AS HierCode, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS HierDesc," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No, " & _
        " 0 AS Sale, case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then ((" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.LoadIn_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) + Burst/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor + Leak/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor + Shortage/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor ) *  " & strSubQry1 & "  else 0 end AS LoadIn, '" & dtpFdate.Value & "' AS fromDate, '" & dtpToDate.Value & "' AS Todate, " & _
        "'" & strReportTitle & "' AS ReportTitle, " & strOrderColumn & " AS OrderBy, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode,TSPL_EMPLOYEE_MASTER_1.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Desc, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location AS LoadOut_Location, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.ToLoc_Desc AS Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, '" & strConverted & "' AS Convertion, " & _
        "0 AS Value, ((LoadIn_Qty+ Burst + Leak + Shortage )*  " & strSubQry1 & ")  * (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TPT_Value) AS Inamt, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code " & _
        "FROM " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER INNER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + "  LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER AS TSPL_EMPLOYEE_MASTER_1 ON " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode = TSPL_EMPLOYEE_MASTER_1.EMP_CODE LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL RIGHT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code AND  " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom ON  " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON TSPL_TRANSFER_HEAD.From_Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code LEFT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code" & _
        " WHERE     (TSPL_TRANSFER_HEAD.Transfer_Type = 'LI') AND (TSPL_LOCATION_MASTER.Location_Type = 'logical')  and TSPL_ITEM_DETAILS.Class_Name='Size' and TSPL_ITEM_DETAILS_1.Class_Name='flavour' and convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
 "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date, '" & dtpToDate.Value & "',103) "







        Dim strLocAll, strRouteAll As String
        If chkLocationAll.IsChecked = True Then
            strLocAll = "Y"
        Else
            strLocAll = "N"
        End If
        If chkRouteAll.IsChecked = True Then
            strRouteAll = "Y"
        Else
            strRouteAll = "N"
        End If


        If strLocAll = "N" Then
            strSql1 += " and From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            strSql2 += " and To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "

        End If
        If strRouteAll = "N" Then
            strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "

        End If

        Dim StrSql As String = strSql1 & Un1 & strSql2
        Dim ArrDBName As ArrayList = Nothing
        If rbtnCompanyAll.IsChecked Then
            ArrDBName = cbgCompany.AllValue
        Else
            ArrDBName = cbgCompany.CheckedValue
        End If
        Dim strQuery As String = clsCommon.GetQueryWithAllSelectedDataBase(StrSql, ArrDBName, False)
        If ddlType.Text = "Both" Then
        ElseIf ddlType.Text = "Quantity" Then
        ElseIf ddlType.Text = "Value" Then
        End If
        Dim strReportName As String = String.Empty

        If rdbSummary.IsChecked = True Then
            If ddlType.Text = "Both" Then
                strReportName = "crptProvisionalSalesRoutewiseBoth"
            ElseIf ddlType.Text = "Quantity" Then
                strReportName = "crptProvisionalSalesRoutewiseQty"
            ElseIf ddlType.Text = "Value" Then
                strReportName = "crptProvisionalSalesRoutewiseValue"
            End If
            '' Added by Abhishek kumar as on 29 june 2012 For Excel Format
            If chkExcelOption.Checked = True Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funExcelForamtReport(CrystalReportFolder.SalesReport, strQuery, "" & strReportName & "", "Provisional Sale Detail Report ")
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "" & strReportName & "", "Provisional Sale Detail Report ")
            End If

        ElseIf rdbDetail.IsChecked = True Then
            If ddlType.Text = "Both" Then
                strReportName = "crptProvisionalSalesItemwiseBoth"
            ElseIf ddlType.Text = "Quantity" Then
                strReportName = "crptProvisionalSalesItemwiseQty"
            ElseIf ddlType.Text = "Value" Then
                strReportName = "crptProvisionalSalesItemwiseValue"
            End If
            '' Added by Abhishek kumar as on 29 june 2012 For Excel Format
            If chkExcelOption.Checked = True Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funExcelForamtReport(CrystalReportFolder.SalesReport, strQuery, "" & strReportName & "", "Provisional Sale Detail Report ")
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "" & strReportName & "", "Provisional Sale Detail Report ")
            End If

        ElseIf rdbHierwise.IsChecked = True Then
            If ddlType.Text = "Both" Then
                strReportName = "crptProvisionalSalesHierBoth"
            ElseIf ddlType.Text = "Quantity" Then
                strReportName = "crptProvisionalSalesHierQtywise"
            ElseIf ddlType.Text = "Value" Then
                strReportName = "crptProvisionalSalesHierwisevalue"
            End If
            '' Added by Abhishek kumar as on 29 june 2012 For Excel Format
            If chkExcelOption.Checked = True Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funExcelForamtReport(CrystalReportFolder.SalesReport, strQuery, "" & strReportName & "", "Provisional Sale Detail Report ")
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "" & strReportName & "", "Provisional Sale Detail Report ")
            End If
        ElseIf rdbRoutewise.IsChecked = True Then
            If ddlType.Text = "Both" Then
                strReportName = "crptProvisionalSalesRoutewiseBothSummary"
            ElseIf ddlType.Text = "Quantity" Then
                strReportName = "crptProvisionalSalesRoutewiseQtySummary"
            ElseIf ddlType.Text = "Value" Then
                strReportName = "crptProvisionalSalesRoutewiseValueSummary"
            End If
            '' Added by Abhishek kumar as on 29 june 2012 For Excel Format
            If chkExcelOption.Checked = True Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funExcelForamtReport(CrystalReportFolder.SalesReport, strQuery, "" & strReportName & "", "Provisional Sale Detail Report ")
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "" & strReportName & "", "Provisional Sale Detail Report ")
            End If
        End If
    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub
    Function LoadClass() As DataTable

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        'dr("Code") = ""
        'dr("Name") = "Select"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "AGENCY"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "D"
        dr("Name") = "DIRECT ROUTE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "FRANCHISEE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "MODERN TRADE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "PRE-SALE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "SUPER DISTRIBUTOR"
        dt.Rows.Add(dr)


        Return dt
    End Function
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged, chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged, chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub
  
    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged, rbtnCompanySelect.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub

    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        '  Dim qry As String = "select Location_Code as [Location],Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Sub LoadRoute()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Route_No as [Route],Route_Desc as [Route Description] from TSPL_ROUTE_MASTER"
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Route"
        cbgRoute.DisplayMember = "Route Description"
    End Sub

    Private Sub FrmProvSaleExcel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'SetUserMgmtNew()
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        dtpFdate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        rdbSku.IsChecked = True
        ddlConvert.Text = "Raw"
        
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        rdbSummary.IsChecked = True
        ddlcategory.Text = "HOS"
        ddlType.Text = "Quantity"
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        dtpFdate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        rdbSku.IsChecked = True
        ddlConvert.Text = "Raw"
       
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        rdbSummary.IsChecked = True
        ddlcategory.Text = "HOS"
        ddlType.Text = "Quantity"


        GV1.DataSource = Nothing
        GV1.Columns.Clear()
        GV1.Rows.Clear()

        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmProvSaleExcel_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            'printreport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub RadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton.Click
        LoadData()
    End Sub

    Private Sub ddlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlType.SelectedIndexChanged
        If ddlType.Text = "Both" Then
            btnExcel.Enabled = False
            RadButton.Enabled = False
        Else
            btnExcel.Enabled = True
            RadButton.Enabled = True
        End If
    End Sub
End Class
