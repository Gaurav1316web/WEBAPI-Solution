'                                            Modified by = Priit (19/12/2012   03:50 PM)

'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------
'by vipin for pdf work on 06/02/2013


Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common

Public Class FrmFilledOutWard
    Inherits FrmMainTranScreen
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmFilledOutWard)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

    End Sub

    Sub LoadCustomerClass()
        Dim qry As String = "select Cust_Type_Code as [Code],Cust_Type_Desc as [Name] from TSPL_CUSTOMER_type_master"
        chkCustomerClass.DataSource = clsDBFuncationality.GetDataTable(qry)
        chkCustomerClass.ValueMember = "Code"
        chkCustomerClass.DisplayMember = "Name"
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
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub
    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        chkCustomerClass.Enabled = Not chkClassAll.IsChecked
    End Sub



    Private Sub FrmFilledOutWard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        dtpFdate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        rdbSku.IsChecked = True
        ddlConvert.Text = "Raw"
        LoadCustomerClass()
        chkClassAll.IsChecked = True
        LoadCompany()
        'rbtnCompanyAll.IsChecked = True
        rdbSummary.IsChecked = True
        ddlcategory.Text = "HOS"
        rbtnCompanyAll.IsChecked = True
        ddltype.Text = "Out"

        rbtnCompanySelect.IsChecked = True
        Dim arrDBName As New ArrayList()
        arrDBName.Add(objCommonVar.CurrDatabase)
        cbgCompany.CheckedValue = arrDBName


        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")


    End Sub

    Sub reset()
        LoadLocation()
        ddltype.Text = "Out"

        chkLocationAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        dtpFdate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        rdbSku.IsChecked = True
        LoadCustomerClass()
        chkClassAll.IsChecked = True
        ddlConvert.Text = "Raw"
        LoadCompany()
        rbtnCompanyAll.IsChecked = True '' Added by abhishek as on 29 june 2012
        rdbSummary.IsChecked = True
        ddlcategory.Text = "HOS"
        rdbSummary.IsChecked = True
        rdbSku.IsChecked = True
        chkExcelOption.Checked = False
        ' gv.Rows.Clear()
        gv.Columns.Clear()
        gv.DataSource = Nothing
        gv.MasterTemplate.SummaryRowsBottom.Clear()


    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        reset()
    End Sub

   

    Private Sub btnprint_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        printdata(False, 0)
    End Sub


    Public Sub printdata(ByVal chk As Boolean, ByVal exporter As EnumExportTo)
        Try

            Dim head2, strConverted, rpttype As String
            head2 = ""
            strConverted = ""
            rpttype = ""
            If ddlcategory.Text = "HOS" Then
                head2 = "HOS"
            ElseIf ddlcategory.Text = "TDM" Then
                head2 = "TDM"
            ElseIf ddlcategory.Text = "ADC" Then
                head2 = "ADC"
            ElseIf ddlcategory.Text = "CE" Then
                head2 = "CE"
            ElseIf ddlcategory.Text = "CE" Then
                head2 = "CE"
            ElseIf ddlcategory.Text = "SalesMan" Then
                head2 = "SalesMan"
            End If


            If ddlConvert.Text = "Converted" Then
                strConverted = "Converted"
            ElseIf ddlConvert.Text = "8oz" Then
                strConverted = "8oz"
            ElseIf ddlConvert.Text = "Raw" Then
                strConverted = "Raw"
            End If

            If ddltype.Text = "Out" Then
                rpttype = "Outward"
            ElseIf ddltype.Text = "In" Then
                rpttype = "Inward"
            End If




            Dim dt1 As New DataTable
            dt1 = print()


            If dt1 IsNot Nothing Then


                If chk = True Then
                    Dim str As String = "Filled Outward/Inward Report"



                    If rdbDetail.IsChecked = True Then
                        Dim arr As New List(Of String)()
                        arr.Add(objCommonVar.CurrentCompanyName)
                        arr.Add("Filled Outward Report (Detail)")
                        arr.Add("" + head2 + "-Wise")
                        arr.Add("Convertion :" + strConverted + "      Report Type:" + rpttype + "")
                        arr.Add("Start Date:-" + dtpFdate.Value + "")
                        arr.Add("End Date:-" + dtpToDate.Value + "")
                        'clsCommon.MyExportToExcel(str, gv, arr, "FilledOutwardReportDetail")

                        If exporter = EnumExportTo.Excel Then
                            clsCommon.MyExportToExcel(str, gv, arr, "FilledOutwardReportDetail")
                        Else
                            clsCommon.MyExportToPDF(str, gv, arr, "FilledOutwardReportDetail", True)
                        End If



                    ElseIf rdbSummary.IsChecked = True Then
                        Dim arr As New List(Of String)()
                        arr.Add(objCommonVar.CurrentCompanyName)
                        arr.Add("Filled Outward Report (Summary)")
                        arr.Add("" + head2 + "-Wise")
                        arr.Add("Convertion :" + strConverted + "      Report Type:" + rpttype + "")
                        arr.Add("Start Date:-" + dtpFdate.Value + "")
                        arr.Add("End Date:-" + dtpToDate.Value + "")
                        ' clsCommon.MyExportToExcel(str, gv, arr, "FilledOutwardReportSummary")
                        If exporter = EnumExportTo.Excel Then
                            clsCommon.MyExportToExcel(str, gv, arr, "FilledOutwardReportSummary")
                        Else
                            clsCommon.MyExportToPDF(str, gv, arr, "FilledOutwardReportSummary", True)
                        End If
                    ElseIf rdbRoutewise.IsChecked = True Then
                        Dim arr As New List(Of String)()
                        arr.Add(objCommonVar.CurrentCompanyName)
                        arr.Add("Filled Outward Report (Routewise)")
                        arr.Add("" + head2 + "-Wise")
                        arr.Add("Convertion :" + strConverted + "      Report Type:" + rpttype + "")
                        arr.Add("Start Date:-" + dtpFdate.Value + "")
                        arr.Add("End Date:-" + dtpToDate.Value + "")
                        'clsCommon.MyExportToExcel(str, gv, arr, "FilledOutwardReportRoutewise")
                        If exporter = EnumExportTo.Excel Then
                            clsCommon.MyExportToExcel(str, gv, arr, "FilledOutwardReportRoutewise")
                        Else
                            clsCommon.MyExportToPDF(str, gv, arr, "FilledOutwardReportRoutewise", True)
                        End If

                    ElseIf rdbHierwise.IsChecked = True Then
                        Dim arr As New List(Of String)()
                        arr.Add(objCommonVar.CurrentCompanyName)
                        arr.Add("Filled Outward Report (Hierarchywise)")
                        arr.Add("" + head2 + "-Wise")
                        arr.Add("Convertion :" + strConverted + "      Report Type:" + rpttype + "")
                        arr.Add("Start Date:-" + dtpFdate.Value + "")
                        arr.Add("End Date:-" + dtpToDate.Value + "")
                        'clsCommon.MyExportToExcel(str, gv, arr, "FilledOutwardReportHierarchywise")

                        If exporter = EnumExportTo.Excel Then
                            clsCommon.MyExportToExcel(str, gv, arr, "FilledOutwardReportHierarchywise")
                        Else
                            clsCommon.MyExportToPDF(str, gv, arr, "FilledOutwardReportHierarchywise", True)
                        End If
                    End If






                Else
                    'Dim dt1 As New DataTable
                    'dt1 = clsDBFuncationality.GetDataTable(strQuery)



                    If rdbDetail.IsChecked = True Then

                        frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt1, "FillOutwardDetailCrossTab", " Fill Outward Detail Report")
                    ElseIf rdbSummary.IsChecked = True Then

                        frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt1, "FillOutwardSummaryCrossTab", " Fill Outward Summary Report")
                    ElseIf rdbRoutewise.IsChecked = True Then
                        frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt1, "FillOutwardRouteCrossTab", " Fill Outward Routewise Report")
                    ElseIf rdbHierwise.IsChecked = True Then
                        frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt1, "FillOutwardHierCrossTab", " Fill Outward Hierarchywise Report")
                    End If



                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Function print() As DataTable
        Dim dt1 As DataTable = Nothing
        Try

            Dim rpttype, strSQL1Group, strReportTitle, strOrderColumn, strOrderBy, strConverted, head1, head2, TDMCOdecolumn, group1, additional As String
            rpttype = ""
            strSQL1Group = ""
            strReportTitle = ""
            strOrderColumn = ""
            strOrderBy = ""
            strConverted = ""
            head1 = ""
            head2 = ""
            TDMCOdecolumn = ""
            group1 = ""
            additional = ""

            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                Return Nothing
            ElseIf chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Route or select ALL")
                Return Nothing
            ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomerClass.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer Class or select ALL")
                Return Nothing
            ElseIf rbtnCompanySelect.IsChecked = True AndAlso cbgCompany.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Company or select ALL")
                Return Nothing
            End If

            If chkMrp.Checked Then
                If rdbSku.IsChecked = True Then
                    strSQL1Group = "TSPL_TRANSFER_DETAIL.Item_Code + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP * Conversion_Factor) ) +  ' ) '"
                    strReportTitle = "Provisional Sale Sku wise"
                    strOrderColumn = "TSPL_ITEM_MASTER.Sku_Seq"
                    strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
                ElseIf rdbPack.IsChecked = True Then
                    strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')' + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP * Conversion_Factor) ) +  ' ) ' "
                    strReportTitle = "Provisional Sale Pack wise"
                    strOrderColumn = "TSPL_ITEM_MASTER.Pack_Seq"
                    strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
                ElseIf rdbFlavour.IsChecked = True Then
                    strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc +'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')' + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP * Conversion_Factor) ) +  ' ) ' "
                    strReportTitle = "Provisional Sale Flavour wise"
                    strOrderColumn = "TSPL_ITEM_MASTER.Flavour_Seq"
                    strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"
                End If
            Else

                If rdbSku.IsChecked = True Then
                    strSQL1Group = "TSPL_TRANSFER_DETAIL.Item_Code "
                    strReportTitle = "Provisional Sale Sku wise"
                    strOrderColumn = "TSPL_ITEM_MASTER.Sku_Seq"
                    strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
                ElseIf rdbPack.IsChecked = True Then
                    strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')' "
                    strReportTitle = "Provisional Sale Pack wise"
                    strOrderColumn = "TSPL_ITEM_MASTER.Pack_Seq"
                    strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
                ElseIf rdbFlavour.IsChecked = True Then
                    strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc +'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')' "
                    strReportTitle = "Provisional Sale Flavour wise"
                    strOrderColumn = "TSPL_ITEM_MASTER.Flavour_Seq"
                    strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"
                End If

            End If


            Dim strSubQry1 As String = ""
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

            If ddltype.Text = "Out" Then
                rpttype = "Outward"
            ElseIf ddltype.Text = "In" Then
                rpttype = "Inward"
            End If


            Dim strSql1 As String = ""

            If ddltype.Text = "Out" Then
                strSql1 = "SELECT   '" + rpttype + "' as type , " & strSQL1Group & " AS Group1, '" + head2 + "' AS heading2, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " AS HierCode, " & _
                "emp.Emp_Name AS HierDesc," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No, " & _
                " (case when Uom <> 'SH' then (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end) *  " & strSubQry1 & " AS Sale, 0 AS LoadIn, '" & dtpFdate.Value & "' AS fromDate, '" & dtpToDate.Value & "' AS Todate, " & _
                "'" & strReportTitle & "' AS ReportTitle, " & strOrderColumn & " AS OrderBy, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Desc," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Vehicle_No, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location AS LoadOut_Location, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.FromLoc_Desc AS Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, '" & strConverted & "' AS Convertion, " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty *  " & strSubQry1 & ") * (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax +  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TPT_Value) " & _
                "AS Value, 0 AS Inamt, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code " & _
                "FROM   " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " & _
                             " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode  Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER  as emp on emp.EMP_CODE =" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + group1 + "  LEFT OUTER JOIN " & _
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
                "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date, '" & dtpToDate.Value & "',103) and Post='Y' "


            ElseIf ddltype.Text = "In" Then
                '----------'Sale' name is using as Alias for loadin and LoadIn as '0'--------------

                strSql1 = "SELECT    '" + rpttype + "' as type , " & strSQL1Group & " AS Group1, '" + head2 + "' AS heading2, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " AS HierCode, " & _
        "emp.Emp_Name AS HierDesc," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No, " & _
        " 0 AS LoadIn, case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then ((" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.LoadIn_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) + Burst/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor + Leak/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor + Shortage/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor ) *  " & strSubQry1 & "  else 0 end AS Sale, '" & dtpFdate.Value & "' AS fromDate, '" & dtpToDate.Value & "' AS Todate, " & _
        "'" & strReportTitle & "' AS ReportTitle, " & strOrderColumn & " AS OrderBy, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode, " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Desc," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Vehicle_No, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location AS LoadOut_Location, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.ToLoc_Desc AS Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code, " & _
        "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, '" & strConverted & "' AS Convertion, " & _
        "0 AS Value, ((LoadIn_Qty+ Burst + Leak + Shortage )*  " & strSubQry1 & ")  * (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.BasicPrice_WithTax + " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.TPT_Value) AS Inamt, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code " & _
        "FROM   " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN " & _
                     " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER  as emp on emp.EMP_CODE =" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + group1 + " LEFT OUTER JOIN " & _
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
 "convert(date,TSPL_TRANSFER_HEAD.Transfer_Date,103) <= convert(date, '" & dtpToDate.Value & "',103) and Post='Y'"

            End If

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

                If ddltype.Text = "Out" Then
                    strSql1 += " and From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                ElseIf ddltype.Text = "In" Then
                    strSql1 += " and To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                End If
            End If


            If strRouteAll = "N" Then
                If ddltype.Text = "Out" Then
                    strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                ElseIf ddltype.Text = "In" Then
                    strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                End If
            End If

            Dim StrSql As String = strSql1
            Dim ArrDBName As ArrayList = Nothing
            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            Dim strQuery As String = clsCommon.GetQueryWithAllSelectedDataBase(StrSql, ArrDBName, False)

            Dim strItemCodestring, strItemCode, strMainItemCode, strmainItemCodeString, strPivot, strsum As String
            strItemCodestring = ""
            strItemCode = ""
            strMainItemCode = ""
            strmainItemCodeString = ""
            strPivot = ""
            strsum = ""

            '----------this is for finding distinct items-------------------
            Dim itemcount As String = ""

            If rdbSku.IsChecked = True Then
                If rdbDetail.IsChecked = True Then
                    itemcount = " select  distinct Group1,OrderBy  from (" + strQuery + ") abc order by OrderBy "
                ElseIf rdbSummary.IsChecked = True Then
                    itemcount = " select  distinct Group1,OrderBy  from (" + strQuery + ") abc order by OrderBy "
                ElseIf rdbRoutewise.IsChecked = True Then
                    itemcount = " select  distinct Group1,OrderBy  from (" + strQuery + ") abc order by OrderBy"
                ElseIf rdbHierwise.IsChecked = True Then
                    itemcount = " select  distinct Group1,OrderBy  from (" + strQuery + ") abc order by OrderBy"
                End If
            Else
                If rdbDetail.IsChecked = True Then
                    itemcount = " select  distinct Group1  from (" + strQuery + ") abc  "
                ElseIf rdbSummary.IsChecked = True Then
                    itemcount = " select  distinct Group1 from (" + strQuery + ") abc  "
                ElseIf rdbRoutewise.IsChecked = True Then
                    itemcount = " select  distinct Group1  from (" + strQuery + ") abc "
                ElseIf rdbHierwise.IsChecked = True Then
                    itemcount = " select  distinct Group1  from (" + strQuery + ") abc "
                End If
            End If




            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(itemcount)

            'Dim dr As SqlDataReader = connectSql.RunSqlReturnDR(itemcount)

            Dim arritem As New ArrayList
            'While dr.Read
            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                For Each dr As DataRow In dt2.Rows
                    strItemCode = CStr(dr(0).ToString())
                    strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","
                    arritem.Add(strItemCode)
                    strMainItemCode = CStr(dr(0).ToString())
                    strmainItemCodeString = strmainItemCodeString & "  isnull(" & "[" & strItemCode & "]" & ",0)  " & "as  " & "[" & strItemCode & "]" & ","
                    'strsum = strItemCode + "+"

                    strsum = strsum & "  isnull(" & "[" & strItemCode & "]" & ",0)" & "+"
                Next
            End If
            ' End While


            If strItemCode <> "" Then
                strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
                strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)
                strsum = strsum.Substring(0, strsum.Length - 1)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Return Nothing
            End If


            Dim strmain As String = ""
            If rdbDetail.IsChecked = True Then

                strmain = " select   HierDesc as Name,Transfer_No as [Transfer No],Transfer_Date as [Transfer Date],Route_No as Route,Route_Desc as [Route Description],Salesmancode as [Salesman Code],Emp_Name as [Salesman Desc],Vehicle_No as [Vehicle No] ," & strmainItemCodeString & " ,(" + strsum + ")as Total from(select Comp_Name,HierCode,HierDesc,Transfer_No,Transfer_Date ,Route_No,Route_Desc,Salesmancode,Emp_Name,Vehicle_No,convert(decimal(18,2),round(sum(Sale),2)) as Sale,Group1 from (" + strQuery + ")aaa group by aaa.Group1,aaa.Comp_Name,aaa.HierCode,aaa.HierDesc,aaa.Transfer_No,aaa.Transfer_Date,aaa.Route_No,aaa.Route_Desc,aaa.Salesmancode,aaa.Emp_Name,aaa.Vehicle_No) down pivot (SUM(Sale) FOR Group1 IN ( " & strItemCodestring & ")) AS pvt1 "
            ElseIf rdbSummary.IsChecked = True Then
                strmain = " select   HierDesc as Name,Route_No as Route,Route_Desc as [Route Description] ," & strmainItemCodeString & " ,(" + strsum + ")as Total from(select Comp_Name,HierCode,HierDesc,Route_No,Route_Desc,convert(decimal(18,2),round(sum(Sale),2)) as Sale,Group1 from (" + strQuery + ")aaa group by aaa.Group1,aaa.Comp_Name,aaa.HierCode,aaa.HierDesc,aaa.Route_No,aaa.Route_Desc) down pivot (SUM(Sale) FOR Group1 IN ( " & strItemCodestring & ")) AS pvt1 "
            ElseIf rdbRoutewise.IsChecked = True Then
                strmain = " select   Route_No as Route,Route_Desc as [Route Description] ," & strmainItemCodeString & " ,(" + strsum + ")as Total from(select Route_No,Route_Desc,convert(decimal(18,2),round(sum(Sale),2)) as Sale,Group1 from (" + strQuery + ")aaa group by aaa.Group1,aaa.Route_No,aaa.Route_Desc) down pivot (SUM(Sale) FOR Group1 IN ( " & strItemCodestring & ")) AS pvt1 "
            ElseIf rdbHierwise.IsChecked = True Then
                strmain = " select   HierDesc as Name ," & strmainItemCodeString & " ,(" + strsum + ")as Total from(select HierCode,HierDesc,convert(decimal(18,2),round(sum(Sale),2)) as Sale,Group1 from (" + strQuery + ")aaa group by aaa.Group1,aaa.HierCode,aaa.HierDesc) down pivot (SUM(Sale) FOR Group1 IN ( " & strItemCodestring & ")) AS pvt1 "
            End If

            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(strmain)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dt
            End If
            gridformat(arritem)



            dt1 = clsDBFuncationality.GetDataTable(strQuery)


            '----------------------------------------------------------------------------------------------
            ''If chkExcelOption.Checked = True Then

            ''    Dim str As String = "Filled Outward Report"
            ''    If rdbDetail.IsChecked = True Then
            ''        Dim arr As New List(Of String)()
            ''        arr.Add(objCommonVar.CurrentCompanyName)
            ''        arr.Add("" + head2 + "-Wise")
            ''        arr.Add("Convertion :" + strConverted + "")
            ''        arr.Add("Filled Outward Report (Detail)")
            ''        arr.Add("Start Date:-" + dtpFdate.Value + "")
            ''        arr.Add("End Date:-" + dtpToDate.Value + "")
            ''        clsCommon.MyExportToExcel(str, gv, arr, "FilledOutwardReportDetail")
            ''    ElseIf rdbSummary.IsChecked = True Then
            ''        Dim arr As New List(Of String)()
            ''        arr.Add(objCommonVar.CurrentCompanyName)
            ''        arr.Add("" + head2 + "-Wise")
            ''        arr.Add("Convertion :" + strConverted + "")
            ''        arr.Add("Filled Outward Report (Routewise)")
            ''        arr.Add("Start Date:-" + dtpFdate.Value + "")
            ''        arr.Add("End Date:-" + dtpToDate.Value + "")
            ''        clsCommon.MyExportToExcel(str, gv, arr, "FilledOutwardReportRoutewise")
            ''    ElseIf rdbRoutewise.IsChecked = True Then
            ''        Dim arr As New List(Of String)()
            ''        arr.Add(objCommonVar.CurrentCompanyName)
            ''        arr.Add("" + head2 + "-Wise")
            ''        arr.Add("Convertion :" + strConverted + "")
            ''        arr.Add("Filled Outward Report (Hierarchywise)")
            ''        arr.Add("Start Date:-" + dtpFdate.Value + "")
            ''        arr.Add("End Date:-" + dtpToDate.Value + "")
            ''        clsCommon.MyExportToExcel(str, gv, arr, "FilledOutwardReportHierarchywise")
            ''    ElseIf rdbHierwise.IsChecked = True Then
            ''        Dim arr As New List(Of String)()
            ''        arr.Add(objCommonVar.CurrentCompanyName)
            ''        arr.Add("" + head2 + "-Wise")
            ''        arr.Add("Convertion :" + strConverted + "")
            ''        arr.Add("Filled Outward Report (Summary)")
            ''        arr.Add("Start Date:-" + dtpFdate.Value + "")
            ''        arr.Add("End Date:-" + dtpToDate.Value + "")
            ''        clsCommon.MyExportToExcel(str, gv, arr, "FilledOutwardReportSummary")
            ''    End If

            ''Else

            ''    Dim dt1 As New DataTable
            ''    dt1 = clsDBFuncationality.GetDataTable(strQuery)
            ''    Dim frm As New SalesReportViewer()
            ''    If rdbDetail.IsChecked = True Then

            ''        frm.funreport(dt1, "FillOutwardDetailCrossTab", " Fill Outward Detail Report")
            ''    ElseIf rdbSummary.IsChecked = True Then

            ''        frm.funreport(dt1, "FillOutwardSummaryCrossTab", " Fill Outward Summary Report")
            ''    ElseIf rdbRoutewise.IsChecked = True Then
            ''        frm.funreport(dt1, "FillOutwardRouteCrossTab", " Fill Outward Routewise Report")
            ''    ElseIf rdbHierwise.IsChecked = True Then
            ''        frm.funreport(dt1, "FillOutwardHierCrossTab", " Fill Outward Hierarchywise Report")

            ''    End If

            ''End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return dt1
    End Function

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim arr As New List(Of String)()
            arr.Add("First Filter")
            arr.Add("Second Filter")
            Dim str As String = "New Report"
            clsCommon.MyExportToExcel(str, gv, arr, "Loadout")
        Catch ex As Exception

        End Try
    End Sub
    Public Sub gridformat(ByVal arritem As ArrayList)
        Try
            'gv.DataSource = Nothing
            'gv.Rows.Clear()
            'gv.Columns.Clear()

            gv.MasterTemplate.SummaryRowsBottom.Clear()

            Dim strItemCode As String
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            gv.AllowAddNewRow = False

            If rdbDetail.IsChecked = True Then


                gv.Columns("Name").IsVisible = True
                gv.Columns("Name").Width = 150
                gv.Columns("Name").HeaderText = "Name"

                gv.Columns("Transfer No").IsVisible = True
                gv.Columns("Transfer No").Width = 100
                gv.Columns("Transfer No").HeaderText = "Transfer No"


                gv.Columns("Transfer Date").IsVisible = True
                gv.Columns("Transfer Date").Width = 100
                gv.Columns("Transfer Date").HeaderText = "Transfer Date"

                gv.Columns("Route").IsVisible = True
                gv.Columns("Route").Width = 50
                gv.Columns("Route").HeaderText = "Route"

                gv.Columns("Route Description").IsVisible = True
                gv.Columns("Route Description").Width = 200
                gv.Columns("Route Description").HeaderText = "Route Description"

                gv.Columns("Salesman Code").IsVisible = True
                gv.Columns("Salesman Code").Width = 70
                gv.Columns("Salesman Code").HeaderText = "Salesman Code"


                gv.Columns("Salesman Desc").IsVisible = True
                gv.Columns("Salesman Desc").Width = 200
                gv.Columns("Salesman Desc").HeaderText = "Salesman Desc"

                gv.Columns("Vehicle No").IsVisible = True
                gv.Columns("Vehicle No").Width = 100
                gv.Columns("Vehicle No").HeaderText = "Vehicle No"

               

                For ii As Integer = 8 To gv.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gv.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next

                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                'For j As Integer = 0 To arritem.Count - 1
                '    gv.Columns(arritem.Item(j)).IsVisible = True
                '    gv.Columns(arritem.Item(j)).Width = 100
                '    gv.Columns(arritem.Item(j)).HeaderText = arritem.Item(j)

                'Next

            ElseIf rdbSummary.IsChecked = True Then



                gv.Columns("Name").IsVisible = True
                gv.Columns("Name").Width = 150
                gv.Columns("Name").HeaderText = "Name"


                gv.Columns("Route").IsVisible = True
                gv.Columns("Route").Width = 50
                gv.Columns("Route").HeaderText = "Route"

                gv.Columns("Route Description").IsVisible = True
                gv.Columns("Route Description").Width = 200
                gv.Columns("Route Description").HeaderText = "Route Description"
              
                For ii As Integer = 3 To gv.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gv.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


            ElseIf rdbRoutewise.IsChecked = True Then


                gv.Columns("Route").IsVisible = True
                gv.Columns("Route").Width = 50
                gv.Columns("Route").HeaderText = "Route"

                gv.Columns("Route Description").IsVisible = True
                gv.Columns("Route Description").Width = 200
                gv.Columns("Route Description").HeaderText = "Route Description"

               
                For ii As Integer = 2 To gv.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gv.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            ElseIf rdbHierwise.IsChecked = True Then
                gv.Columns("Name").IsVisible = True
                gv.Columns("Name").Width = 150
                gv.Columns("Name").HeaderText = "Name"
              
                For ii As Integer = 1 To gv.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gv.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


            End If

            For j As Integer = 0 To arritem.Count - 1
                gv.Columns(arritem.Item(j)).IsVisible = True
                gv.Columns(arritem.Item(j)).Width = 70
                gv.Columns(arritem.Item(j)).HeaderText = arritem.Item(j)

            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()


    End Sub

     Private Sub btnReset_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

   
    
    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Try
            gv.EnableFiltering = True
            Dim dt As New DataTable()
            dt = print()

            RadPageView1.SelectedPage = RadPageViewPage2

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnExcel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        printdata(True, EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        printdata(True, EnumExportTo.PDF)
    End Sub
End Class
