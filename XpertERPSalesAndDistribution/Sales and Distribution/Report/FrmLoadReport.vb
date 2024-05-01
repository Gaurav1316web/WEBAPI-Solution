'                                            Modified by = Priit (04/12/2012   02:05 PM)
'                                            Modified by = Priit (12/12/2012   10:40 AM)
'by priti for bug no BM00000000638,BM00000000685
Imports XpertERPEngine
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


Public Class FrmLoadReport


    Inherits FrmMainTranScreen
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmLoadReport)
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

    Sub LoadRouteType()
        Dim qry As String = "select Route_Type_Id as Code,Route_Type_Desc as Name from TSPL_ROUTE_TYPE"
        cbgRouteType.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRouteType.ValueMember = "Code"
        cbgRouteType.DisplayMember = "Name"
    End Sub




    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        chkCustomerClass.Enabled = Not chkClassAll.IsChecked
    End Sub



    Private Sub FrmFilledOutWard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        LoadLocation()
        'cbgLocation.CheckedAll()

        LoadRoute()
        'cbgRoute.CheckedAll()

        LoadRouteType()
        Dim arrRouteType As New ArrayList
        arrRouteType.Add("A")
        arrRouteType.Add("D")
        arrRouteType.Add("M")
        arrRouteType.Add("P")
        arrRouteType.Add("T")
        cbgRouteType.CheckedValue = arrRouteType

        dtpFdate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        rdbSku.IsChecked = True
        ddlConvert.Text = "Converted"
        LoadCustomerClass()
        chkClassAll.IsChecked = True
        LoadCompany()

        rdbSummary.IsChecked = True
        ddlcategory.Text = "HOS"

        'rdbFlavour.Visible = False
        'rdbPack.Visible = False

        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        'ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        'ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")
        ' btnprint.Enabled = True
        ' btnprint.Visible = True
        btnrefresh.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1
        'cbgCompany.CheckedAll()
        'rbtnCompanySelect.IsChecked = True
        Dim arrDBName As New ArrayList()
        arrDBName.Add(objCommonVar.CurrDatabase)
        cbgCompany.CheckedValue = arrDBName



    End Sub

    Sub reset()
        LoadLocation()



        LoadRoute()

        dtpFdate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        rdbSku.IsChecked = True
        LoadCustomerClass()
        chkClassAll.IsChecked = True
        ddlConvert.Text = "Converted"
        LoadCompany()

        rdbSummary.IsChecked = True
        ddlcategory.Text = "HOS"
        rdbSummary.IsChecked = True
        rdbSku.IsChecked = True

        ' gv.Rows.Clear()
        gv.Columns.Clear()
        gv.DataSource = Nothing
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        rbtnAll.IsChecked = True
        chkMrp.Checked = False
        chkDepot.Checked = False
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        reset()
    End Sub



    Private Sub btnprint_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
       
    End Sub

    Sub printdata(ByVal exporter As EnumExportTo)
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



            Dim dt1 As New DataTable
            dt1 = print()


            RadPageView1.SelectedPage = RadPageViewPage2


            Dim strRouteType As String = ""
            For Each Str As String In cbgRouteType.CheckedDisplayMember
                If clsCommon.myLen(strRouteType) > 0 Then
                    strRouteType += ", "
                End If
                strRouteType += Str
            Next




            If dt1 IsNot Nothing Then
                Dim str As String

                If rdbDetail.IsChecked = True Then
                    str = "Combine Sale Report (Detail)"
                    Dim arr As New List(Of String)()
                    arr.Add(objCommonVar.CurrentCompanyName)
                    arr.Add("Combine Sale Report (Detail)")
                    arr.Add("" + head2 + "-Wise")
                    arr.Add("Convertion :" + strConverted + "")
                    arr.Add("Start Date:-" + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy") + "")
                    arr.Add("End Date:-" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "")
                    arr.Add("Route Type : " + strRouteType)
                    If rbtnLocationSelect.IsChecked Then
                        str = ""
                        For Each Str2 As String In cbgLocation.CheckedDisplayMember
                            If clsCommon.myLen(str) > 0 Then
                                str += ", "
                            End If
                            str += Str2
                        Next
                        arr.Add("Location Segment : " + str)
                    End If
                    If rbtnRouteSelect.IsChecked Then
                        str = ""
                        For Each Str2 As String In cbgRoute.CheckedDisplayMember
                            If clsCommon.myLen(str) > 0 Then
                                str += ", "
                            End If
                            str += Str2
                        Next
                        arr.Add("Route Segment : " + str)
                    End If

                    arr.Add("Transaction Type : " + IIf(rbtnAll.IsChecked, "All", "Posted"))
                    'clsCommon.MyExportToExcel(str, gv, arr, "CombineSaleReport(Detail)")

                    If exporter = EnumExportTo.Excel Then
                        If chkMrp.Checked Then
                            clsCommon.MyExportToExcelGrid(str, gv, arr, "CombineSaleReport(Detail)")
                        Else
                            clsCommon.MyExportToExcel(str, gv, arr, "CombineSaleReport(Detail)")

                        End If

                    Else
                        clsCommon.MyExportToPDF(str, gv, arr, "CombineSaleReport(Detail)", True)
                    End If

                ElseIf chkConsolidate.IsChecked = True Then
                    str = "Combine Sale Report (Consolidated)"
                    Dim arr As New List(Of String)()
                    arr.Add(objCommonVar.CurrentCompanyName)
                    arr.Add("Combine Sale Report (Consolidated)")
                    arr.Add("" + head2 + "-Wise")
                    arr.Add("Convertion :" + strConverted + "")
                    arr.Add("Start Date:-" + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy") + "")
                    arr.Add("End Date:-" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "")
                    arr.Add("Route Type : " + strRouteType)
                    arr.Add("Transaction Type : " + IIf(rbtnAll.IsChecked, "All", "Posted"))
                    If rbtnLocationSelect.IsChecked Then
                        str = ""
                        For Each Str2 As String In cbgLocation.CheckedDisplayMember
                            If clsCommon.myLen(str) > 0 Then
                                str += ", "
                            End If
                            str += Str2
                        Next
                        arr.Add("Location Segment : " + str)
                    End If
                    If rbtnRouteSelect.IsChecked Then
                        str = ""
                        For Each Str2 As String In cbgRoute.CheckedDisplayMember
                            If clsCommon.myLen(str) > 0 Then
                                str += ", "
                            End If
                            str += Str2
                        Next
                        arr.Add("Route Segment : " + str)
                    End If

                    ' clsCommon.MyExportToExcel(str, gv, arr, "CombineSaleReport(Consolidated)")

                    If exporter = EnumExportTo.Excel Then
                        clsCommon.MyExportToExcelGrid(str, gv, arr, "CombineSaleReport(Consolidated)")
                    Else
                        clsCommon.MyExportToPDF(str, gv, arr, "CombineSaleReport(Consolidated)", True)
                    End If


                ElseIf rdbSummary.IsChecked = True Then
                    str = "Combine Sale Report (Summary)"
                    Dim arr As New List(Of String)()
                    arr.Add(objCommonVar.CurrentCompanyName)
                    arr.Add("Combine Sale Report (Summary)")
                    arr.Add("" + head2 + "-Wise")
                    arr.Add("Convertion :" + strConverted + "")
                    arr.Add("Start Date:-" + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy") + "")
                    arr.Add("End Date:-" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "")
                    arr.Add("Route Type : " + strRouteType)
                    arr.Add("Transaction Type : " + IIf(rbtnAll.IsChecked, "All", "Posted"))
                    If rbtnLocationSelect.IsChecked Then
                        str = ""
                        For Each Str2 As String In cbgLocation.CheckedDisplayMember
                            If clsCommon.myLen(str) > 0 Then
                                str += ", "
                            End If
                            str += Str2
                        Next
                        arr.Add("Location Segment : " + str)
                    End If
                    If rbtnRouteSelect.IsChecked Then
                        str = ""
                        For Each Str2 As String In cbgRoute.CheckedDisplayMember
                            If clsCommon.myLen(str) > 0 Then
                                str += ", "
                            End If
                            str += Str2
                        Next
                        arr.Add("Route Segment : " + str)
                    End If

                    ' clsCommon.MyExportToExcel(str, gv, arr, "CombineSaleReport(Summary)")

                    If exporter = EnumExportTo.Excel Then
                        If chkMrp.Checked Then
                            clsCommon.MyExportToExcelGrid(str, gv, arr, "CombineSaleReport(Summary)")
                        Else
                            clsCommon.MyExportToExcel(str, gv, arr, "CombineSaleReport(Summary)")
                        End If

                    Else
                        clsCommon.MyExportToPDF(str, gv, arr, "CombineSaleReport(Summary)", True)
                    End If
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

  

    Public Function print() As DataTable
        Dim dt1 As DataTable = Nothing
        Try
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.EnableFiltering = True
            Dim strSQL1Group, strsum, strReportTitle, strOrderColumn, strOrderBy, strConverted, head1, head2, TDMCOdecolumn, group1, additional As String
            strSQL1Group = ""
            strsum = ""
            strReportTitle = ""
            strOrderColumn = ""
            strOrderBy = ""
            strConverted = ""
            head1 = ""
            head2 = ""
            TDMCOdecolumn = ""
            group1 = ""
            additional = ""

            If rbtnLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                Return Nothing
                Exit Function
            ElseIf rbtnRouteSelect.IsChecked AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Route or select ALL")
                Return Nothing
                Exit Function
            ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomerClass.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer Class or select ALL")
                Return Nothing
                Exit Function
            ElseIf cbgCompany.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Company ")
                Return Nothing
                Exit Function
            ElseIf cbgRouteType.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Route type")
                Return Nothing
                Exit Function
            End If

            Dim strClass As String = ""
            If chkMrp.Checked Then
                If rdbSku.IsChecked = True Then
                    strSQL1Group = "TSPL_TRANSFER_DETAIL.Item_Code + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP * Conversion_Factor) ) +  ' ) '"
                    strReportTitle = "SKU-Wise"
                    strOrderColumn = "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq"
                    strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
                    strClass = "Size"
                ElseIf rdbPack.IsChecked = True Then
                    strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')' + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP * Conversion_Factor) ) +  ' ) ' "
                    strReportTitle = "Pack-Wise"
                    strOrderColumn = "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq"
                    strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
                    strClass = "Size"
                ElseIf rdbFlavour.IsChecked = True Then
                    strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')' + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP * Conversion_Factor) ) +  ' ) ' "
                    strReportTitle = "Flavour-Wise"
                    strOrderColumn = "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq"
                    strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"
                    strClass = "Flavour"
                End If
            Else
                If rdbSku.IsChecked = True Then
                    strSQL1Group = "TSPL_TRANSFER_DETAIL.Item_Code "
                    strReportTitle = "SKU-Wise"
                    strOrderColumn = "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq"
                    strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
                    strClass = "Size"
                ElseIf rdbPack.IsChecked = True Then
                    strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')'  "
                    strReportTitle = "Pack-Wise"
                    strOrderColumn = "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq"
                    strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
                    strClass = "Size"
                ElseIf rdbFlavour.IsChecked = True Then
                    strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')' "
                    strReportTitle = "Flavour-Wise"
                    strOrderColumn = "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq"
                    strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"
                    strClass = "Flavour"
                End If
            End If


            Dim strSubQry1 As String = ""
            If ddlConvert.Text = "Converted" Then
                strSubQry1 = "(isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB'),0))/isnull((select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='Con'),1)"
                'strSubQry1 = " (TSPL_ITEM_UOM_DETAIL.Conversion_Factor) / isnull(TSPL_ITEM_UOM_DETAIL_2.Conversion_Factor, 1)"
                strConverted = "Converted"
            ElseIf ddlConvert.Text = "8oz" Then
                'strSubQry1 = " (TSPL_ITEM_UOM_DETAIL.Conversion_Factor) / isnull(TSPL_ITEM_UOM_DETAIL_2.Conversion_Factor, 1)"
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
            ElseIf ddlcategory.Text = "SalesMan" Then
                head2 = "SalesMan"
                TDMCOdecolumn = "Salesmancode"
                group1 = "Salesmancode"
                additional = "(SalesMan)"

            End If


            Dim strItemCodestring, strItemCode, strMainItemCode, strmainItemCodeString, strPivot As String
            strItemCodestring = ""
            strItemCode = ""
            strMainItemCode = ""
            strmainItemCodeString = ""
            strPivot = ""
            Dim dr As DataTable
            Dim str1 As String = ""

            If rdbSku.IsChecked = True Then
                strPivot = "TSPL_TRANSFER_DETAIL.Item_Code"

            ElseIf rdbPack.IsChecked = True Then
                strPivot = "TSPL_ITEM_DETAILS.Class_Desc"
            ElseIf rdbFlavour.IsChecked = True Then
                strPivot = "TSPL_ITEM_DETAILS.Class_Desc"
            End If
            Dim loadqry As String = printLoadout()
            Dim strWhere As String
            If chkDepot.Checked Then
                strWhere = "and ((Trans_Type='excise' and Reference_Doc_No='' and TSPL_TRANSFER_HEAD.Item_Type='full') or (Trans_Type='Depot'  and Reference_Doc_No='' and TSPL_TRANSFER_HEAD.Route_No ='') OR (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Location_Type = 'logical'))"
            Else
                strWhere = " and (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Location_Type = 'logical') "
            End If


            clsDBFuncationality.ExecuteNonQuery(" UPDATE TSPL_TRANSFER_HEAD SET Route_Type_Id ='DE' WHERE Route_Type_Id ='' OR Route_Type_Id IS NULL")

            Dim strSql1 As String = "SELECT  line_no,TSPL_TRANSFER_HEAD.Vehicle_No,CASE WHEN Trans_Type='Route' then TSPL_ROUTE_MASTER.Route_Desc " & _
                      "when (Trans_Type='Excise' Or Trans_Type='Depot') then " & _
                      "(select Location_Desc from tspl_location_master where GIT_Location= To_Location) else '' end as Party, " & _
                      "0 as Ret,Reference_Doc_No,'' as cust,'' as custdesc,MRP * Conversion_Factor as MRP," & strSQL1Group & " AS Item_Code, '" + head2 + "' AS heading2, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " AS HierCode, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS Hier_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Desc, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No AS DocNo, (CASE WHEN Uom <> 'SH' THEN (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty) ELSE 0 END) " & _
                      "* " & strSubQry1 & " AS Sale, '01/09/2012 11:19:14 AM' AS fromDate, " & _
                      "'22/09/201' AS Todate, 'Provisional Sale Sku wise' AS ReportTitle, " + strOrderColumn + " AS OrderBy, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date AS DocDate, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode, " & _
                      "TSPL_EMPLOYEE_MASTER_1.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location AS Location, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.FromLoc_Desc AS Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name,  " & _
                      "'Raw' AS Convertion, " + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE.Route_Type_Desc " & _
                      "FROM " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON   " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " LEFT OUTER JOIN  " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Type_Id = " + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE.Route_Type_Id LEFT OUTER JOIN  " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER AS TSPL_EMPLOYEE_MASTER_1 ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode = TSPL_EMPLOYEE_MASTER_1.EMP_CODE LEFT OUTER JOIN  " & _
                      " " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL ON  " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code And " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom ON  " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No LEFT OUTER JOIN  " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN  " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN   " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code  " & _
                      "left outer join TSPL_ROUTE_MASTER on TSPL_TRANSFER_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No " & _
                      "WHERE (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type = 'LO') AND  " & _
                      "(" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = '" & strClass & "') AND " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date >= '" & clsCommon.GetPrintDate(dtpFdate.Value, "dd/MMM/yyyy") & "' AND " & _
                      "(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "') AND (" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Unit_Code <> 'sh') " & strWhere & " "

            Dim strSql2 As String = "SELECT     line_no,TSPL_TRANSFER_HEAD.Vehicle_No,CASE WHEN TSPL_TRANSFER_HEAD_1.Trans_Type='Route' then TSPL_ROUTE_MASTER.Route_Desc " & _
                      "when (TSPL_TRANSFER_HEAD_1.Trans_Type='Excise' Or TSPL_TRANSFER_HEAD_1.Trans_Type='Depot') then " & _
                      "(select Location_Desc from tspl_location_master where GIT_Location= TSPL_TRANSFER_HEAD_1.To_Location) else '' end as Party, " & _
                      "0 as Ret,TSPL_TRANSFER_HEAD_1.Reference_Doc_No,'' as cust,'' as custdesc,MRP * Conversion_Factor as MRP," & strSQL1Group & "  AS Item_Code, '" + head2 + "' AS heading2, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " AS HierCode, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS Hier_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Desc, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Load_Out_No AS DocNo, " & _
                      "CASE WHEN " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.UOM <> 'SH' THEN ((" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.LoadIn_Qty + Leak + Burst + Shortage) " & _
                      "/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * " & strSubQry1 & " * (- 1) " & _
                      "ELSE 0 END AS Sale, '01/09/2012 11:19:14 AM' AS fromDate, '22/09/2012 11:19:14 AM' AS Todate, 'Provisional Sale Sku wise' AS ReportTitle, " & _
                      "" + strOrderColumn + " AS OrderBy, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, TSPL_TRANSFER_HEAD_1.Transfer_Date AS DocDate, " & _
                      " TSPL_TRANSFER_HEAD_1.Salesmancode, TSPL_EMPLOYEE_MASTER_1.Emp_Name, " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_Desc, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location AS LoadOut_Location, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.ToLoc_Desc AS Location_Desc, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, 'Raw' AS Convertion, " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE.Route_Type_Desc  " & _
                      "FROM " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER RIGHT OUTER JOIN  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." + TDMCOdecolumn + " LEFT OUTER JOIN " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Type_Id = " + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE.Route_Type_Id  " & _
                      "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code And " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON  " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code LEFT OUTER JOIN  " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code " & _
                      "left outer join TSPL_ROUTE_MASTER on TSPL_TRANSFER_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No " & _
                      "left outer join TSPL_TRANSFER_HEAD as TSPL_TRANSFER_HEAD_1 on TSPL_TRANSFER_HEAD.Load_Out_No=TSPL_TRANSFER_HEAD_1.Transfer_No " & _
                                            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER AS TSPL_EMPLOYEE_MASTER_1 ON  TSPL_TRANSFER_HEAD_1.Salesmancode = TSPL_EMPLOYEE_MASTER_1.EMP_CODE  " & _
                      "WHERE (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Type = 'LI') AND " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Location_Type = 'logical' AND " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = '" & strClass & "' AND " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date >= '" & clsCommon.GetPrintDate(dtpFdate.Value, "dd/MMM/yyyy") & "' AND " & _
                      "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "' "
            Dim un1 As String = " Union All "

            strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ")"
            strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ")"


            If rbtnPost.IsChecked Then
                str1 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Post='Y'"
            End If

            Dim strLocAll, strRouteAll As String
            If rbtnLocationAll.IsChecked = True Then
                strLocAll = "Y"
            Else
                strLocAll = "N"
            End If
            If rbtnRouteAll.IsChecked = True Then
                strRouteAll = "Y"
            Else
                strRouteAll = "N"
            End If


            If strLocAll = "N" Then
                strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "


            End If
            If strRouteAll = "N" Then
                strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No  in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No  in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "

            End If

            str1 = strSql1 & un1 & strSql2

            str1 = " select    line_no,Vehicle_No,Party,sum(Ret) as Ret,max(Reference_Doc_No) as Reference_Doc_No,max(cust) as cust,max(custdesc) as custdesc,MRP,Item_code,max (heading2) heading2 , HierCode, Hier_Desc,Item_Desc, DocNo,sum(Sale) as Sale,max(fromdate) as fromdate,max(todate) as todate, max(ReportTitle) as ReportTitle,OrderBy, Route_No,DocDate,Salesmancode,Emp_name, Route_desc,location , Location_Desc,max(Comp_Code) as Comp_Code,max(Comp_Name) as Comp_Name, max (Convertion) as Convertion,MAX(Route_Type_Desc) AS Route_Type_Desc from  (" + str1 + " ) asd group by line_no,MRP,Item_Code,HierCode,Hier_Desc,Item_Desc,DocNo,OrderBy,Route_No,DocDate,Salesmancode,Emp_name,Route_Desc,location,Location_Desc,Vehicle_No,Party"


            Dim ArrDBName As ArrayList = cbgCompany.CheckedValue

            Dim qry As String = clsCommon.GetQueryWithAllSelectedDataBase(str1, ArrDBName, False)



            Dim finalqry As String = " select   line_no,Vehicle_No,Party,sum(Ret) as Ret, Reference_Doc_No,cust,custdesc,Item_code,max (heading2) heading2 , HierCode, Hier_Desc,Item_Desc, DocNo,sum(Sale) as Sale,max(fromdate) as fromdate,max(todate) as todate, max(ReportTitle) as ReportTitle,OrderBy, Route_No,DocDate,Salesmancode,Emp_name, Route_desc,location , Location_Desc,max(Comp_Code) as Comp_Code,max(Comp_Name) as Comp_Name, max (Convertion) as conversion,MAX(Route_Type_Desc) AS Route_Type_Desc from  (" + qry + " union all " + loadqry + " ) asd group by line_no,Item_Code,HierCode,Hier_Desc,Item_Desc,DocNo,OrderBy,Route_No,DocDate,Salesmancode,Emp_name,Route_Desc,location,Location_Desc,cust,custdesc,Reference_Doc_No,Vehicle_No,Party "

            Dim mainitems As String
            If rdbSku.IsChecked = True Then
                mainitems = "select distinct Item_Code,OrderBy from (select  distinct Item_Code ,OrderBy from (" + qry + ") xx Union all  select distinct item_code ,OrderBy from (" + loadqry + ")dd)gg order by OrderBy"
            Else
                mainitems = "select distinct Item_Code,OrderBy from (select  distinct (Item_Code ) as Item_Code,OrderBy from (" + qry + ") xx Union all  select distinct (Item_Code ) as Item_Code ,OrderBy from (" + loadqry + ")dd)gg order by OrderBy "
            End If

            dr = clsDBFuncationality.GetDataTable(mainitems)
            Dim arritemCount As New ArrayList
            Dim arritemCountSKU As New ArrayList
            If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                For Each dtdr As DataRow In dr.Rows
                    strItemCode = CStr(dtdr(0).ToString())
                    strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","
                    arritemCount.Add(strItemCode)
                    strMainItemCode = CStr(dtdr(0).ToString())
                    'strmainItemCodeString = strmainItemCodeString & "  isnull(" & "[" & strItemCode & "]" & ",0)  " & "as  " & "[" & strItemCode & "]" & ","
                    strmainItemCodeString = strmainItemCodeString & "  isnull(" & "Sum(" & "[" & strItemCode & "]" & " ) " & ",0)  " & "as  " & "[" & strItemCode & "]" & ","

                    strsum = strsum & "  isnull(" & "[" & strItemCode & "]" & ",0)" & "+"
                Next
            End If

            If clsCommon.myLen(strItemCode) > 0 Then
                strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
                strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)
                strsum = strsum.Substring(0, strsum.Length - 1)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)

                Return Nothing
            End If


            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim str2 As String = ""
            If chkConsolidate.IsChecked = True Then

                str2 = "select Reference_Doc_No,Party,Vehicle_No,cust,custdesc,Hier_Desc,Route_No,Route_Desc,Route_Type_Desc as [Route Type], DocNo,CONVERT( varchar(12),DocDate,103 )as DocDate,Salesmancode, Emp_Name, Location,Location_Desc, convert(decimal(18,2),round(sum(SaleOut),2))  as SaleOut,convert(decimal(18,2),round(sum(Ret),2)) as Ret,sum(" + strsum + ") as Total  from ( Select  convert(decimal(18,2),round(sum(Sale),2)) as SaleOut,Line_no,Vehicle_No,Party,convert(decimal(18,2),round(sum(Ret),2)) as Ret,Reference_Doc_No,cust,custdesc,Item_Code,HierCode,Hier_Desc,Route_No,Route_Desc, DocNo,DocDate,Salesmancode, Emp_Name, Location,Location_Desc ,convert(decimal(18,2),round(sum(Sale) - sum(ret),2)) as sale,max(Route_Type_Desc) as Route_Type_Desc from  (" + finalqry + ")innerqry   group by  line_no,Item_Code,HierCode,Hier_Desc, DocNo,DocDate ,Route_No,Route_Desc,Salesmancode,Emp_name,Location,Location_Desc,cust,custdesc,Reference_Doc_No,Vehicle_No,Party  ) down  pivot " & _
              "(SUM(sale) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1  group by Reference_Doc_No,cust,custdesc,Hier_Desc,Route_No,Route_Desc,Route_Type_Desc,DocNo ,DocDate,Salesmancode,Emp_Name,Location,Location_Desc,Vehicle_No,Party  ORDER BY CONVERT( date,DocDate,103)"

            ElseIf rdbDetail.IsChecked = True Then

                str2 = "select Reference_Doc_No,Party,Vehicle_No,cust,custdesc,Hier_Desc,Route_No,Route_Desc,Route_Type_Desc as [Route Type], DocNo,CONVERT( varchar(12),DocDate,103 )as DocDate,Salesmancode, Emp_Name, Location,Location_Desc,convert(decimal(18,2),round(sum(SaleOut),2)) as SaleOut,convert(decimal(18,2),round(sum(Ret),2)) as Ret,sum(" + strsum + ") as Total," & strmainItemCodeString & "  from ( Select convert(decimal(18,2),round(sum(Sale),2)) as SaleOut,line_no,Vehicle_No,Party, convert(decimal(18,2),round(sum(Ret) ,2)) as Ret,Reference_Doc_No,cust,custdesc,Item_Code,HierCode,Hier_Desc,Route_No,Route_Desc, DocNo,DocDate,Salesmancode, Emp_Name, Location,Location_Desc ,convert(decimal(18,2),round(sum(Sale) - sum(ret),2)) as sale,max(Route_Type_Desc) as Route_Type_Desc from  (" + finalqry + ")innerqry   group by  line_no,Item_Code,HierCode,Hier_Desc, DocNo,DocDate ,Route_No,Route_Desc,Salesmancode,Emp_name,Location,Location_Desc,cust,custdesc,Reference_Doc_No,Vehicle_No,Party  ) down  pivot " & _
             "(SUM(sale) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1 group by Reference_Doc_No,cust,custdesc,Hier_Desc,Route_No,Route_Desc,Route_Type_Desc,DocNo ,DocDate,Salesmancode,Emp_Name,Location,Location_Desc,Vehicle_No,Party   ORDER BY CONVERT( date,DocDate,103) "

            ElseIf rdbSummary.IsChecked = True Then
                'str2 = "select Hier_Desc,Route_No,Route_Desc," & strmainItemCodeString & " ,(" + strsum + ")as Total from ( Select  Item_Code,HierCode,Hier_Desc,Route_No,Route_Desc,convert(decimal(18,2),round(sum(Sale),2)) as sale from  (" + finalqry + ")innerqry   group by  Item_Code,HierCode,Hier_Desc, Route_No,Route_Desc  ) down  pivot " & _
                '             "(SUM(sale) FOR item_code IN ( " & strItemCodestring & ")) AS pvt1 "


                str2 = "select Party,Vehicle_No,Hier_Desc,Route_No,Route_Desc,Route_Type_Desc as [Route Type],Location,Location_Desc,convert(decimal(18,2),round(sum(SaleOut),2)) as SaleOut,convert(decimal(18,2),round(sum(Ret),2)) as Ret, sum(" + strsum + ") as Total ," & strmainItemCodeString & " from ( Select convert(decimal(18,2),round(sum(Sale),2)) as SaleOut,line_no,Vehicle_No,Party, convert(decimal(18,2),round(sum(Ret),2))  as Ret,Item_Code,HierCode,Hier_Desc,Route_No,Route_Desc,Location,Location_Desc,convert(decimal(18,2),round(sum(Sale)-sum(ret),2)) as sale,max(Route_Type_Desc) as Route_Type_Desc from  (" + finalqry + ")innerqry   group by  line_no,Item_Code,HierCode,Hier_Desc, Route_No,Route_Desc,Location,Location_Desc,Vehicle_No,Party  ) down  pivot " & _
                             "(SUM(sale) FOR item_code IN ( " & strItemCodestring & "))  AS pvt1      group by Hier_Desc,Route_No,Route_Desc,Route_Type_Desc ,Location,Location_Desc,Vehicle_No,Party "



            End If


            dt1 = clsDBFuncationality.GetDataTable(str2)


            If rdbSku.IsChecked Then
                For ii As Integer = 0 To dt1.Columns.Count - 1
                    Dim oldColumnName As String = dt1.Columns(ii).ColumnName
                    Dim NewColumnName As String = ""
                    If oldColumnName.Contains("(") Then
                        NewColumnName = oldColumnName.Replace("(", Environment.NewLine)
                        If NewColumnName.Contains(")") Then
                            NewColumnName = NewColumnName.Replace(")", "")
                        End If
                    End If
                    If clsCommon.myLen(NewColumnName) > 0 Then
                        dt1.Columns(ii).ColumnName = NewColumnName
                    End If
                Next

                For ii As Integer = 0 To arritemCount.Count - 1
                    Dim Value As String = arritemCount(ii)
                    If Value.Contains("(") Then
                        Value = Value.Replace("(", Environment.NewLine)
                    End If
                    If Value.Contains(")") Then
                        Value = Value.Replace(")", "")
                    End If
                    arritemCountSKU.Add(Value)
                Next

            End If

            If dt1 IsNot Nothing And dt1.Rows.Count > 0 Then

                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dt1
            End If
            gridformat(IIf(rdbSku.IsChecked, arritemCountSKU, arritemCount))

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return dt1
    End Function


    Public Function printLoadout() As String
        Dim mainstr As String = ""
        Try
            Dim qry, qry2, strGroupingInv, strGroupingRet, strGroupingInter, qry3 As String
            strGroupingInv = ""
            strGroupingRet = ""
            Dim head1 As String = ""
            Dim head2 As String = ""
            Dim TDMCOdecolumn As String = ""
            Dim group1 As String = ""
            Dim additional As String = ""
            Dim strOrderColumn As String = ""
            Dim strOrderBy As String = ""
            Dim qryqty As String = ""
            Dim strReturnQty, strInterQty As String
            Dim conversion As String = ""
            strReturnQty = ""
            'Dim visifilter As String
            strInterQty = ""
            strGroupingInter = ""
            '-----------------By Vipin (for reducing the qty of SaleReturn from Sale Invoice)----------------------------

            If ddlConvert.Text = "Converted" Then
                conversion = "Converted"
                qryqty = "isnull(((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) /  ISNULL((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),1))* (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0) as Qty  "
                strReturnQty = "(isnull(((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) /  isnull((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),1))* (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0))  as Ret  "
                strInterQty = "- (isnull(((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) /  isnull((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),1))* (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0))  as Qty  "

            ElseIf ddlConvert.Text = "Raw" Then
                conversion = "Raw"
                qryqty = "(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )   as Qty  "
                strReturnQty = " ((case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ) )   as Ret  "
                strInterQty = " - ((case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ) )   as Qty  "

            ElseIf ddlConvert.Text = "8oz" Then
                conversion = "8oz"
                qryqty = " isnull((( (select " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) / (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   ) *(select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   *(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )),0) as   Qty "
                strReturnQty = " (isnull((( (select " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) / (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   ) *(select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   *(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )),0) ) as   Ret "
                strInterQty = " - (isnull((( (select " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) / (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   ) *(select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   *(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )),0) ) as   Qty "

            End If
            '-----------------------------------------------------------------------------------------
            Dim strMRP As String
            If chkMrp.Checked = True Then
                strMRP = " + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP_amt * Conversion_Factor) ) +  ' ) ' "
            Else
                strMRP = ""
            End If

            If rdbSku.IsChecked = True Then
                strGroupingInv = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code " & strMRP & " "
                strGroupingRet = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code " & strMRP & ""
                strGroupingInter = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code  " & strMRP & ""
                If ddlcategory.Text = "HOS" Then
                    head1 = "SKU-Wise"
                    head2 = "HOS"
                    TDMCOdecolumn = "Level2_User_code"
                    group1 = "Level1_User_Code"
                    additional = "(HOS)"
                ElseIf ddlcategory.Text = "TDM" Then
                    head1 = "SKU-Wise"
                    head2 = "TDM"
                    TDMCOdecolumn = "Level3_User_code"
                    group1 = "Level2_User_Code"
                    additional = "(TDM)"
                ElseIf ddlcategory.Text = "ADC" Then
                    head1 = "SKU-Wise"
                    head2 = "ADC"
                    TDMCOdecolumn = "Level4_User_code"
                    group1 = "Level3_User_Code"
                    additional = "(ADC)"
                ElseIf ddlcategory.Text = "CE" Then
                    head1 = "SKU-Wise"
                    head2 = "CE"
                    TDMCOdecolumn = "Level5_User_code"
                    group1 = "Level4_User_Code"
                    additional = "(CE)"

                ElseIf ddlcategory.Text = "SalesMan" Then
                    head1 = "SKU-Wise"
                    head2 = "SalesMan"
                    TDMCOdecolumn = "Salesman_Code"
                    group1 = "Level5_User_Code"
                    additional = "(SalesMan)"
                End If
                strOrderColumn = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq"
                ' strOrderBy = " Order By " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq"
                strOrderBy = " where xxx.QTY<>0  Order By xxx.OrderBy"


            ElseIf rdbFlavour.IsChecked = True Then
                strGroupingInv = "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')'  " & strMRP & " "
                strGroupingRet = "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')' " & strMRP & ""
                strGroupingInter = "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')' " & strMRP & ""
                If ddlcategory.Text = "HOS" Then
                    head1 = "Flavour-Wise"
                    head2 = "HOS"
                    TDMCOdecolumn = "Level2_User_code"
                    group1 = "Level1_User_Code"
                    additional = "(HOS)"
                ElseIf ddlcategory.Text = "TDM" Then
                    head1 = "Flavour-Wise"
                    head2 = "TDM"
                    TDMCOdecolumn = "Level3_User_code"
                    group1 = "Level2_User_Code"
                    additional = "(TDM)"
                ElseIf ddlcategory.Text = "ADC" Then
                    head1 = "Flavour-Wise"
                    head2 = "ADC"
                    TDMCOdecolumn = "Level4_User_code"
                    group1 = "Level3_User_Code"
                    additional = "(ADC)"
                ElseIf ddlcategory.Text = "CE" Then
                    head1 = "Flavour-Wise"
                    head2 = "CE"
                    TDMCOdecolumn = "Level5_User_code"
                    group1 = "Level4_User_Code"
                    additional = "(CE)"
                ElseIf ddlcategory.Text = "SalesMan" Then
                    head1 = "Flavour-Wise"
                    head2 = "SalesMan"
                    TDMCOdecolumn = "Salesman_Code"
                    group1 = "Level5_User_Code"
                    additional = "(SalesMan)"
                End If
                strOrderColumn = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq"
                'strOrderBy = " Order By OrderBy"
                strOrderBy = " Order By xxx.OrderBy"


            ElseIf rdbPack.IsChecked = True Then
                strGroupingInv = "TSPL_ITEM_DETAILS_1.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')' " & strMRP & " "
                strGroupingRet = "  TSPL_ITEM_DETAILS_1.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')' " & strMRP & " "
                strGroupingInter = "  TSPL_ITEM_DETAILS_1.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')' " & strMRP & " "

                If ddlcategory.Text = "HOS" Then
                    head1 = "Pack-Wise"
                    head2 = "HOS"
                    TDMCOdecolumn = "Level2_User_code"
                    group1 = "Level1_User_Code"
                    additional = "(HOS)"
                ElseIf ddlcategory.Text = "TDM" Then
                    head1 = "Pack-Wise"
                    head2 = "TDM"
                    TDMCOdecolumn = "Level3_User_code"
                    group1 = "Level2_User_Code"
                    additional = "(TDM)"
                ElseIf ddlcategory.Text = "ADC" Then
                    head1 = "Pack-Wise"
                    head2 = "ADC"
                    TDMCOdecolumn = "Level4_User_code"
                    group1 = "Level3_User_Code"
                    additional = "(ADC)"
                ElseIf ddlcategory.Text = "CE" Then
                    head1 = "Pack-Wise"
                    head2 = "CE"
                    TDMCOdecolumn = "Level5_User_code"
                    group1 = "Level4_User_Code"
                    additional = "(CE)"
                ElseIf ddlcategory.Text = "SalesMan" Then
                    head1 = "Pack-Wise"
                    head2 = "SalesMan"
                    TDMCOdecolumn = "Salesman_Code"
                    group1 = "Level5_User_Code"
                    additional = "(SalesMan)"

                End If
                strOrderColumn = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq"
                strOrderBy = " Order By xxx.OrderBy"

            End If



            qry = "  Select TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id as line_no,Vehicle_No,TSPL_CUSTOMER_MASTER.Customer_Name as Party, '0' as Ret,'' as Reference_Doc_No,MRP_Amt as MRP,'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as PrintDate,  '" + objCommonVar.CurrentCompanyName + "' as CurrentComp, (Select Add1  from TSPL_COMPANY_MASTER WHERE Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as CurrentCompAdd,'" + conversion + "' as conversion, '" + dtpFdate.Value + "' as fromdate,'" + dtpToDate.Value + "' as todate,'" + head1 + "' as heading,'" + head2 + "' as heading2, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AS invoiceno,(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date) AS DocDate, " & _
            "" & strGroupingInv & " as grouping, " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.location) as location, (" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc) as LocDesc, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No) AS route , (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc ) as routedesc, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code) as cust, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code) as Salesmancode,emp.Emp_Name as Emp_name, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name) as custdesc,(" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code) as CustType, " + qryqty + ", (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD." + TDMCOdecolumn + ") AS TDMCOde, (" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name) AS TDMName," + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_code, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add1," + strOrderColumn + " as  OrderBy," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Code," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Desc," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc," + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE.Route_Type_Desc   FROM  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL " & _
                  " INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  " & _
                   "  INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code " & _
                  " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING.Salesman_Code " & _
                  "  LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL." + group1 + " = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
                  "  LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER as emp ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = emp.EMP_CODE " & _
                   " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code " & _
                    "  inner  JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                 " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
                 " Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code  " & _
                 " left Outer Join  " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on  " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location " & _
                 "     left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code    and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code" & _
                  " left outer join " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER on " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No" & _
            " left outer join " + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE on " + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE.Route_Type_Id= " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Type " & _
               " WHERE    (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'flavour') and (TSPL_ITEM_DETAILS_1.Class_Name = 'size')  AND (CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103) >= CONVERT(date,'" + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy") + "', 103))   " & _
              "  AND (CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103) <= CONVERT(date, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + " ', 103))  and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale'     "
            '" Union All " & _
            qry2 = "SELECT   Sale_Return_Id as line_no,TSPL_SALE_INVOICE_HEAD.Vehicle_No,TSPL_CUSTOMER_MASTER.Customer_Name as Party," & strReturnQty & ",'' as Reference_Doc_No,MRP_Amt as MRP,'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' AS PrintDate, " & _
            " '" + objCommonVar.CurrentCompanyName + "' AS CurrentComp, " & _
            "(Select Add1  from TSPL_COMPANY_MASTER WHERE Comp_Code='" + objCommonVar.CurrentCompanyCode + "') AS CurrentCompAdd, " & _
            "'" + conversion + "' AS conversion,   " & _
            "'" + dtpFdate.Value + "' as fromdate,'" + dtpToDate.Value + "' as todate, " & _
            "'" + head1 + "' as heading,'" + head2 + "' as heading2,  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No) AS invoiceno," & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date) as DocDate, " & _
            " " & strGroupingRet & " AS grouping, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location AS location, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc AS LocDesc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No AS route,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc AS routedesc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code AS cust, " & _
            "TSPL_SALE_INVOICE_HEAD.Salesman_Code AS Salesmancode, " & _
            "emp.Emp_Name AS Emp_name, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name AS custdesc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code AS CustType, " & _
            " 0 as Qty , " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD." & TDMCOdecolumn & " AS TDMCOde,  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS TDMName, " & _
            "TSPL_COMPANY_MASTER_1.Comp_Code, TSPL_COMPANY_MASTER_1.Comp_Name,  " & _
            "TSPL_COMPANY_MASTER_1.Add1, " & _
            "" & strOrderColumn & " AS OrderBy, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Code," & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE.Route_Type_Desc " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER FULL OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER AS emp RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD." & TDMCOdecolumn & " = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING.Salesman_Code ON  " & _
            "emp.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER AS TSPL_COMPANY_MASTER_1 ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code = TSPL_COMPANY_MASTER_1.Comp_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No = " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE ON TSPL_ROUTE_TYPE.Route_Type_Id = TSPL_ROUTE_MASTER.Type FULL OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL RIGHT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code AND  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code " & _
            " WHERE    (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'flavour') and " & _
            "(TSPL_ITEM_DETAILS_1.Class_Name = 'size')  AND " & _
            "(CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date, 103) >= CONVERT(date,'" + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy") + "', 103))   " & _
            "  AND (CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date, 103) <= CONVERT(date, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + " ', 103))  and " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Shipment_Type='Sale'     "


            qry3 = " Union All SELECT    line_no,Vehicle_No,TSPL_CUSTOMER_MASTER.Customer_Name as Party,'0' as Ret,'' as Reference_Doc_No,MRP_Amt as MRP,'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' AS PrintDate, " & _
            "'" + objCommonVar.CurrentCompanyName + "' AS CurrentComp, " & _
            " (Select Add1  from TSPL_COMPANY_MASTER WHERE Comp_Code='" + objCommonVar.CurrentCompanyCode + "') AS CurrentCompAdd, '" + conversion + "' AS conversion, " & _
            "'" + dtpFdate.Value + "' as fromdate,'" + dtpToDate.Value + "' as todate, " & _
            "'" + head1 + "' as heading,'" + head2 + "' as heading2,  " & _
            " (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No) AS invoiceno, " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date) as DocDate, " & _
            " " & strGroupingInter & "   AS grouping, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location AS location, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc AS LocDesc," & _
            " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_No AS route,  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_Desc AS routedesc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code AS cust, TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code AS Salesmancode, emp.Emp_Name AS Emp_name, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Name AS custdesc, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code AS CustType, " & _
            "   " & strInterQty & "   , " & _
            " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD." & TDMCOdecolumn & " AS TDMCOde, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS TDMName, " & _
            " TSPL_COMPANY_MASTER_1.Comp_Code, TSPL_COMPANY_MASTER_1.Comp_Name,  TSPL_COMPANY_MASTER_1.Add1, " & _
            "" & strOrderColumn & "  AS OrderBy, " & _
            " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Code," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code, " & _
            " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Desc, " + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE.Route_Type_Desc " & _
            "FROM " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL ON " & _
            " " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Document_No " & _
            " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD." & TDMCOdecolumn & " = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
            "LEFT outer join TSPL_EMPLOYEE_MASTER as emp on TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code=TSPL_EMPLOYEE_MASTER.EMP_CODE left OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
             "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER AS TSPL_COMPANY_MASTER_1 ON  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Comp_Code = TSPL_COMPANY_MASTER_1.Comp_Code " & _
            " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_No = " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No " & _
             "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE ON " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Type = " + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE.Route_Type_Id " & _
            "LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code left outer join " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER  on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code  = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code " & _
            "WHERE    (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'flavour') and (TSPL_ITEM_DETAILS_1.Class_Name = 'size')  AND " & _
            "(CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date, 103) >=CONVERT(date,'" + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy") + "' , 103))  AND  " & _
            "(CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date, 103) <=CONVERT(date,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "', 103))  "




            If rbtnLocationSelect.IsChecked Then
                qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                qry2 += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                qry3 += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "

            End If

            If rbtnRouteSelect.IsChecked Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in  (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
                qry2 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in  (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"
                qry3 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_No in  (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ")"

            End If


            qry += " And " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ")"
            qry2 += " And " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Type_Id in (" + clsCommon.GetMulcallString(cbgRouteType.CheckedValue) + ")"



            If rbtnPost.IsChecked Then
                qry += " And " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post ='Y'"
                qry2 += " And " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post ='Y'"
                qry3 += " And " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Is_Post =1"

            End If

            If chkIC.Checked = False Then
                qry3 = ""
            End If
            Dim strSql As String = qry & " Union All " & qry2 & qry3
            Dim ArrDBName As ArrayList = cbgCompany.CheckedValue
            qry = clsCommon.GetQueryWithAllSelectedDataBase(strSql, ArrDBName, False)

            mainstr = " select line_no,Vehicle_No,Party,sum(Ret) as Ret,Reference_Doc_No,cust,custdesc,MRP,grouping as Item_code,max (heading2) heading2 ,TDMCOde as HierCode,TDMName as HierDesc,Item_Desc,invoiceno as DocNo,sum(Qty) as Sale,max(fromdate) as fromdate,max(todate) as todate,max(heading) as [Report Title],OrderBy,route as Route_No,DocDate,Salesmancode,Emp_name,routedesc as Route_desc,location ,LocDesc as Location_Desc,max(Comp_Code) as Comp_Code,max(Comp_Name) as Comp_Name, max (conversion) as conversion,max(Route_Type_Desc) as Route_Type_Desc  from (" + qry + ")main group by line_no,grouping,MRP,TDMCOde,TDMName,Item_Desc,invoiceno,OrderBy,route,DocDate,Salesmancode,Emp_name,routedesc,location,LocDesc,cust,custdesc,Reference_Doc_No,Party,Vehicle_No "

        Catch ex As Exception

        End Try
        Return mainstr
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

            If rdbDetail.IsChecked = True OrElse chkConsolidate.IsChecked = True Then
                gv.Columns("Party").IsVisible = True
                gv.Columns("Party").Width = 100
                gv.Columns("Party").HeaderText = "Party"

                gv.Columns("Vehicle_No").IsVisible = True
                gv.Columns("Vehicle_No").Width = 100
                gv.Columns("Vehicle_No").HeaderText = "Vehicle No"

                gv.Columns("Cust").IsVisible = True
                gv.Columns("Cust").Width = 50
                gv.Columns("Cust").HeaderText = "Customer"

                gv.Columns("Custdesc").IsVisible = True
                gv.Columns("Custdesc").Width = 100
                gv.Columns("Custdesc").HeaderText = "Customer Name"

                gv.Columns("Hier_Desc").IsVisible = True
                gv.Columns("Hier_Desc").Width = 150
                gv.Columns("Hier_Desc").HeaderText = "Employee Name"

                gv.Columns("Route_No").IsVisible = True
                gv.Columns("Route_No").Width = 50
                gv.Columns("Route_No").HeaderText = "Route No"


                gv.Columns("Route_Desc").IsVisible = True
                gv.Columns("Route_Desc").Width = 100
                gv.Columns("Route_Desc").HeaderText = "Route Description"

                gv.Columns("Reference_Doc_No").IsVisible = True
                gv.Columns("Reference_Doc_No").Width = 100
                gv.Columns("Reference_Doc_No").HeaderText = "Ref DocNo"

                gv.Columns("DocNo").IsVisible = True
                gv.Columns("DocNo").Width = 100
                gv.Columns("DocNo").HeaderText = "Document No"

                gv.Columns("DocDate").IsVisible = True
                gv.Columns("DocDate").Width = 70
                gv.Columns("DocDate").HeaderText = "Document Date"

                gv.Columns("Salesmancode").IsVisible = True
                gv.Columns("Salesmancode").Width = 50
                gv.Columns("Salesmancode").HeaderText = "Salesman Code"


                gv.Columns("Emp_Name").IsVisible = True
                gv.Columns("Emp_Name").Width = 200
                gv.Columns("Emp_Name").HeaderText = "Salesman Desc"

                gv.Columns("Location").IsVisible = True
                gv.Columns("Location").Width = 100
                gv.Columns("Location").HeaderText = "Location"


                gv.Columns("Location_Desc").IsVisible = True
                gv.Columns("Location_Desc").Width = 170
                gv.Columns("Location_Desc").HeaderText = "Location Desc"

                gv.Columns("Total").IsVisible = True
                gv.Columns("Total").Width = 70
                gv.Columns("Total").HeaderText = "Total"



                For ii As Integer = 15 To gv.Columns.Count - 1
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

                gv.Columns("Party").IsVisible = True
                gv.Columns("Party").Width = 100
                gv.Columns("Party").HeaderText = "Party"

                gv.Columns("Vehicle_No").IsVisible = True
                gv.Columns("Vehicle_No").Width = 100
                gv.Columns("Vehicle_No").HeaderText = "Vehicle No"


                gv.Columns("Hier_Desc").IsVisible = True
                gv.Columns("Hier_Desc").Width = 100
                gv.Columns("Hier_Desc").HeaderText = "Employee Name"

                gv.Columns("Route_No").IsVisible = True
                gv.Columns("Route_No").Width = 100
                gv.Columns("Route_No").HeaderText = "Route No"


                gv.Columns("Route_Desc").IsVisible = True
                gv.Columns("Route_Desc").Width = 100
                gv.Columns("Route_Desc").HeaderText = "Route Description"


                gv.Columns("Total").IsVisible = True
                gv.Columns("Total").Width = 70
                gv.Columns("Total").HeaderText = "Total"

                gv.Columns("Location").IsVisible = True
                gv.Columns("Location").Width = 100
                gv.Columns("Location").HeaderText = "Location"


                gv.Columns("Location_Desc").IsVisible = True
                gv.Columns("Location_Desc").Width = 170
                gv.Columns("Location_Desc").HeaderText = "Location Desc"


                For ii As Integer = 8 To gv.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gv.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)



            End If
            If chkConsolidate.IsChecked = False Then
                For j As Integer = 0 To arritem.Count - 1
                    gv.Columns(arritem.Item(j)).IsVisible = True
                    gv.Columns(arritem.Item(j)).Width = 150
                    gv.Columns(arritem.Item(j)).HeaderText = arritem.Item(j)
                Next
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
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
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnUpdateCombineSaleReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateCombineSaleReport.Click
        Try
            clsSaleHead.SetTempProvisionSale()
            myMessages.update()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

        ' btnprint.Enabled = True
        btnrefresh.Enabled = True
    End Sub

    Private Sub btnprint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint1.Click
        Try

            Dim strConverted As String = ""
            Dim head2 As String = ""
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



            Dim dt1 As New DataTable
            dt1 = print()


            RadPageView1.SelectedPage = RadPageViewPage2


            Dim strRouteType As String = ""
            For Each Str As String In cbgRouteType.CheckedDisplayMember
                If clsCommon.myLen(strRouteType) > 0 Then
                    strRouteType += ", "
                End If
                strRouteType += Str
            Next




            If dt1 IsNot Nothing Then
                Dim str As String
                If rdbDetail.IsChecked = True Then
                    str = "Combine Sale Report (Detail)"
                    Dim arr As New List(Of String)()
                    arr.Add(objCommonVar.CurrentCompanyName)
                    arr.Add("Combine Sale Report (Detail)")
                    arr.Add("" + head2 + "-Wise")
                    arr.Add("Convertion :" + strConverted + "")
                    arr.Add("Start Date:-" + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy") + "")
                    arr.Add("End Date:-" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "")
                    arr.Add("Route Type : " + strRouteType)
                    arr.Add("Transaction Type : " + IIf(rbtnAll.IsChecked, "All", "Posted"))
                    clsCommon.MyExportToExcel(str, gv, arr, "CombineSaleReport(Detail)")
                ElseIf chkConsolidate.IsChecked = True Then
                    str = "Combine Sale Report (Consolidated)"
                    Dim arr As New List(Of String)()
                    arr.Add(objCommonVar.CurrentCompanyName)
                    arr.Add("Combine Sale Report (Consolidated)")
                    arr.Add("" + head2 + "-Wise")
                    arr.Add("Convertion :" + strConverted + "")
                    arr.Add("Start Date:-" + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy") + "")
                    arr.Add("End Date:-" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "")
                    arr.Add("Route Type : " + strRouteType)
                    arr.Add("Transaction Type : " + IIf(rbtnAll.IsChecked, "All", "Posted"))
                    clsCommon.MyExportToExcel(str, gv, arr, "CombineSaleReport(Consolidated)")
                ElseIf rdbSummary.IsChecked = True Then
                    str = "Combine Sale Report (Summary)"
                    Dim arr As New List(Of String)()
                    arr.Add(objCommonVar.CurrentCompanyName)
                    arr.Add("Combine Sale Report (Summary)")
                    arr.Add("" + head2 + "-Wise")
                    arr.Add("Convertion :" + strConverted + "")
                    arr.Add("Start Date:-" + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy") + "")
                    arr.Add("End Date:-" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "")
                    arr.Add("Route Type : " + strRouteType)
                    arr.Add("Transaction Type : " + IIf(rbtnAll.IsChecked, "All", "Posted"))
                    clsCommon.MyExportToExcel(str, gv, arr, "CombineSaleReport(Summary)")
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged, rbtnLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = Not rbtnLocationAll.IsChecked
    End Sub

    Private Sub rbtnRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnRouteAll.ToggleStateChanged, rbtnRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = Not rbtnRouteAll.IsChecked
    End Sub


    Private Sub btnExcel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        printdata(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        printdata(EnumExportTo.PDF)
    End Sub

    Private Sub chkMrp_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMrp.ToggleStateChanged

    End Sub

    Private Sub Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub


End Class
