'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 13/07/2012-------------------------------------
'--------------------------------Last modify Time - 04:30 pm -------------------------------------
'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------

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
Imports common
Public Class FrmPrimarySalesReport
    Inherits FrmMainTranScreen


    Dim l1User, l2User, l3User, l4User, l5User As String
    Dim userCode, companyCode As String
    Dim sql As String

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                'dr.Read()
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()
            Next
        End If
    End Sub
    Sub LoadCustomerClass()
        Dim qry As String = "select Cust_Type_Code as [Code],Cust_Type_Desc as [Name] from TSPL_CUSTOMER_type_master"
        chkCustomerClass.DataSource = clsDBFuncationality.GetDataTable(qry)
        chkCustomerClass.ValueMember = "Code"
        chkCustomerClass.DisplayMember = "Name"
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

        'dr = dt.NewRow()
        'dr("Code") = "F"
        'dr("Name") = "FRANCHISEE"
        'dt.Rows.Add(dr)

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
        'Dim qry As String = "select Location_code as [Location],TSPL_LOCATION_MASTER.Location_Desc as[Location Description] from TSPL_LOCATION_MASTER where location_type='physical'"
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
    'Sub LoadCompany()
    '    Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
    '    Dim ArrHideColumn As New List(Of String)
    '    ArrHideColumn.Add("DataBase_Name")
    '    cbgCompany.ArrHideColumns = ArrHideColumn
    '    cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgCompany.ValueMember = "DataBase_Name"
    'End Sub

    ''''''''''''''''''''''''''Fills The Data OF Filter 'Company''''
    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub

    Private Function GetSelectedDatabase() As List(Of String)
        Dim arrDBName As New List(Of String)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If ((clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) OrElse rbtnAllCompany.IsChecked) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function

    Private Sub btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprint.Click
        print()
    End Sub
    Sub print()
        Try
            Dim strSQL1Group, strSQL2Group, strSQL3Group, strDate, StrMonth, strYear, strOrderColumn, strOrderBy, strTitle As String
            Dim strMTD, strYTD As String
            strOrderBy = ""
            strSQL1Group = ""
            strOrderColumn = ""
            strTitle = ""
            strSQL2Group = ""
            strSQL3Group = ""
            If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                Return
            ElseIf chkRouteSelect.IsChecked = True AndAlso cbgRoute.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Item or select ALL")
                Return
            ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomerClass.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer Class or select ALL")
                Return
            End If

            strDate = "01"
            StrMonth = dtpFdate.Value.Month
            strYear = (dtpFdate.Value.Year)

            strMTD = strDate & "/" & StrMonth & "/" & strYear
            strYTD = strDate & "/" & "01" & "/" & strYear

            If rdbSku.IsChecked = True Then
                'strSQL1Group = "TSPL_TRANSFER_DETAIL.Item_Code"
                'strSQL2Group = "TSPL_SALE_INVOICE_DETAIL.Item_Code"
                'strSQL3Group = "TSPL_SRN_DETAIL.Item_Code"
                'strOrderColumn = "TSPL_ITEM_MASTER.Sku_Seq"
                'strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
                strSQL1Group = " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code"
                strSQL2Group = " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code"
                strSQL3Group = " " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code"
                strOrderColumn = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq"
                strTitle = "Sku wise"
                'strOrderBy = "Order By " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq"
            ElseIf rdbPack.IsChecked = True Then
                'strSQL1Group = "TSPL_ITEM_DETAILS.Class_Code"
                'strSQL2Group = " TSPL_ITEM_DETAILS.Class_Code"
                'strSQL3Group = " TSPL_ITEM_DETAILS.Class_Code"
                'strOrderColumn = "TSPL_ITEM_MASTER.Pack_Seq"
                'strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
                strSQL1Group = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc"
                strSQL2Group = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc"
                strSQL3Group = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc"
                strOrderColumn = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq"
                strTitle = "Pack wise"
                'strOrderBy = "Order By " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq"
            ElseIf rdbFlavour.IsChecked = True Then
                strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
                strSQL2Group = "TSPL_ITEM_DETAILS_1.Class_Desc"
                strSQL3Group = " TSPL_ITEM_DETAILS_1.Class_Desc"
                strOrderColumn = "TSPL_ITEM_MASTER.Flavour_Seq"
                strTitle = "Flavour wise"
                'strOrderBy = "Order By TSPL_ITEM_MASTER.Flavour_Seq"

            End If
            Dim strLoca As String = ""
            Dim strClass As String = ""

            If chkLocationSelect.IsChecked Then

                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
            End If

            If chkClassSelect.IsChecked Then

                For Each Str As String In chkCustomerClass.CheckedDisplayMember
                    If clsCommon.myLen(strClass) > 0 Then
                        strClass += ", "
                    End If
                    strClass += Str
                Next
            End If

            If rdbShip.IsChecked = True Then
                '' Transfer date wise entry where from location is excisable


                Dim strSql1 As String = "SELECT " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, convert(varchar, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date,103) AS Period, " & _
                "" & strSQL1Group & " as Item_Code, " & _
                "case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end AS Item_Qty, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, " & _
                "TSPL_ITEM_DETAILS_1.Class_Code AS Flavour, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location as Loc," & strOrderColumn & " as OrderBy,CONVERT(date, '" & dtpFdate.Value & "', 103) as SelectDate,'" & strTitle & "' as ReportType,'" & strLoca & "' as Location,'" & strClass & "' as SelectClass FROM " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code " & _
                "WHERE " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date = CONVERT(date, '" & dtpFdate.Value & "', 103) AND (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND " & _
                "(TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and Excisable='T' and Post='Y'"
                Dim Un1 As String = " Union All "

                '' Transfer Month wise entry where from location is excisable


                Dim strSql2 As String = "SELECT " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, 'MTD' AS Period," & strSQL1Group & " as Item_Code, " & _
                " case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then  (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end AS Item_Qty, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, TSPL_ITEM_DETAILS_1.Class_Code AS Flavour, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location as Loc," & strOrderColumn & " as OrderBy,CONVERT(date, '" & dtpFdate.Value & "', 103) as SelectDate,'" & strTitle & "' as ReportType,'" & strLoca & "' as Location,'" & strClass & "' as SelectClass  FROM " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL ON " & _
                " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                " inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code  " & _
                " WHERE " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date > = CONVERT(date, '" & strMTD & "', 103) and " & _
                " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date  <= CONVERT(date, '" & dtpFdate.Value & "', 103) AND " & _
                "( " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and  Excisable='T' and Post='Y'"
                Dim Un2 As String = " Union All "

                '' Transfer Year wise entry where from location is excisable

                Dim strSql3 As String = "SELECT " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, 'YTD' AS Period," & strSQL1Group & " as Item_Code, " & _
                "case when TSPL_TRANSFER_DETAIL.UOM <> 'SH' then  (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) else 0 end AS Item_Qty, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, TSPL_ITEM_DETAILS_1.Class_Code AS Flavour, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location as Loc," & strOrderColumn & " as OrderBy,CONVERT(date, '" & dtpFdate.Value & "', 103) as SelectDate,'" & strTitle & "' as ReportType,'" & strLoca & "' as Location,'" & strClass & "' as SelectClass  " & _
                " FROM " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL ON " & _
                " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                "inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code " & _
                " WHERE " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date > = CONVERT(date, '" & strYTD & "', 103) and " & _
                " " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date  <= CONVERT(date, '" & dtpFdate.Value & "', 103) AND " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and Excisable='T' and Post='Y'"
                Dim Un3 As String = " Union All "

                '' Sale Invoice Entry Date wise where location is excisable and customer class <> Franchise and intercompany


                Dim strSql4 As String = "select " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, convert(varchar, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) AS Period, " & _
                " " & strSQL2Group & " as Item_Code, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  as Item_Qty," & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, TSPL_ITEM_DETAILS_1.Class_Code AS Flavour, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location as Loc," & strOrderColumn & " as OrderBy,CONVERT(date, '" & dtpFdate.Value & "', 103) as SelectDate,'" & strTitle & "' as ReportType,'" & strLoca & "' as Location,'" & strClass & "' as SelectClass  " & _
                " from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                " INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                " inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code  where " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date = CONVERT(date, '" & dtpFdate.Value & "', 103) AND " & _
                "( " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
                "(Excisable='T' or DutyPaid='Y')  and Cust_Type_Code Not In ('F') and Is_Post='Y'"
                Dim Un4 As String = " Union All "

                '' Sale Invoice Entry Month wise where location is excisable and customer class <> Franchise and intercompany

                Dim strSql5 As String = "select  " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, 'MTD' AS Period, " & strSQL2Group & " as Item_Code, " & _
                " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  as Item_Qty, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, " & _
                " TSPL_ITEM_DETAILS_1.Class_Code AS Flavour, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location as Loc," & strOrderColumn & " as OrderBy,CONVERT(date, '" & dtpFdate.Value & "', 103) as SelectDate,'" & strTitle & "' as ReportType,'" & strLoca & "' as Location,'" & strClass & "' as SelectClass   from " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                " INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                " inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code where " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date > = CONVERT(date, '" & strMTD & "', 103) AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date < = CONVERT(date, '" & dtpFdate.Value & "', 103) and " & _
                " (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
                " (Excisable='T' or DutyPaid='Y')  and Cust_Type_Code Not In ('F') and Is_Post='Y'"
                Dim Un5 As String = " Union All "

                '' Sale Invoice Entry Year wise where location is excisable and customer class <> Franchise and intercompany

                Dim strSql6 As String = "select " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, 'YTD' AS Period, " & strSQL2Group & " as Item_Code, " & _
                " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  as Item_Qty, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, " & _
                " TSPL_ITEM_DETAILS_1.Class_Code AS Flavour, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location as Loc ," & strOrderColumn & " as OrderBy,CONVERT(date, '" & dtpFdate.Value & "', 103) as SelectDate,'" & strTitle & "' as ReportType,'" & strLoca & "' as Location,'" & strClass & "' as SelectClass  from " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                " INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                " inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code where " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date > = CONVERT(date, '" & strYTD & "', 103) AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date < = CONVERT(date, '" & dtpFdate.Value & "', 103) and " & _
                " (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') " & _
                " and (Excisable='T' or DutyPaid='Y')  and Cust_Type_Code Not In ('F') and Is_Post='Y'"
                Dim Un6 As String = " Union All "

                '' SRN Entry Date wise where location is Physical and non excisable and customer class <> Franchise and intercompany

                Dim strSql7 As String = "select " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, convert(varchar, " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_Date,103) AS Period," & strSQL3Group & " as Item_Code, " & _
                " (" + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.SRN_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Item_Qty, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, " & _
                " TSPL_ITEM_DETAILS_1.Class_Code AS Flavour, '' as Route_N0, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Location as Loc," & strOrderColumn & " as OrderBy,CONVERT(date, '" & dtpFdate.Value & "', 103) as SelectDate,'" & strTitle & "' as ReportType,'" & strLoca & "' as Location,'" & strClass & "' as SelectClass  from " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_No= " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.SRN_No inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                " INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_VENDOR_MAPPING INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code ON  " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code = " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.Vendor_Code " & _
                " inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code where " & _
                " convert(date, " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_Date,103) = CONVERT(date, '" & dtpFdate.Value & "', 103) AND " & _
                " (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
                " (Excisable='F' and DutyPaid='N')  and Location_Type='Physical' and Posting_Date <> ''"
                Dim Un7 As String = " Union All "

                '' SRN Entry Month wise where location is Physical and non excisable and customer class <> Franchise and intercompany


                Dim strSql8 As String = "select " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, 'MTD' AS Period, " & strSQL3Group & " as Item_Code, " & _
                " (" + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.SRN_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Item_Qty, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, " & _
                " TSPL_ITEM_DETAILS_1.Class_Code AS Flavour,'' as Route_N0, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Location as Loc," & strOrderColumn & " as OrderBy,CONVERT(date, '" & dtpFdate.Value & "', 103) as SelectDate,'" & strTitle & "' as ReportType,'" & strLoca & "' as Location,'" & strClass & "' as SelectClass  from " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_No= " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.SRN_No inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                " INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code =" + clsCommon.ReplicateDBString + " TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_VENDOR_MAPPING INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code ON  " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code = " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.Vendor_Code " & _
                " inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code where " & _
                " convert(date, " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_Date,103) > = CONVERT(date, '" & strMTD & "', 103) and " & _
                " convert(date, " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_Date,103) < = CONVERT(date, '" & dtpFdate.Value & "', 103) AND " & _
                " (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
                "(Excisable='F' and DutyPaid='N')   and Location_Type='Physical' and Posting_Date <> ''"
                Dim Un8 As String = " Union All "

                '' SRN Entry Year wise where location is Physical and non excisable and customer class <> Franchise and intercompany

                Dim strSql9 As String = "select " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, 'YTD' AS Period, " & strSQL3Group & " as Item_Code, " & _
                            " (" + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.SRN_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Item_Qty, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, " & _
                            " TSPL_ITEM_DETAILS_1.Class_Code AS Flavour,'' as Route_N0, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Location as Loc, " & strOrderColumn & " as OrderBy,CONVERT(date, '" & dtpFdate.Value & "', 103) as SelectDate,'" & strTitle & "' as ReportType,'" & strLoca & "' as Location,'" & strClass & "' as SelectClass  from  " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD inner join " & _
                            " " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_No=" + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.SRN_No inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " & _
                            " " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code inner join " & _
                            " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                            " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                            " INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                           " " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_VENDOR_MAPPING INNER JOIN " & _
                            " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code ON  " & _
                            " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code = " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.Vendor_Code " & _
                            " inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code where " & _
                            " convert(date, " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_Date,103) > = CONVERT(date, '" & strYTD & "', 103) and " & _
                            " convert(date, " + clsCommon.ReplicateDBString + "TSPL_SRN_HEAD.SRN_Date,103) < = CONVERT(date, '" & dtpFdate.Value & "', 103) AND " & _
                            " (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') " & _
                            "and (Excisable='F' and DutyPaid='N')   and Location_Type='Physical' and Posting_Date <> '' "

                Dim strLocAll, strRouteAll, strCustClass As String
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
                If chkClassAll.IsChecked = True Then
                    strCustClass = "Y"
                Else
                    strCustClass = "N"
                End If
                Dim strQuery As String
                If strLocAll = "N" Then
                    strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strSql3 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strSql4 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strSql5 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strSql6 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strSql7 += " and " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strSql8 += " and " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strSql9 += " and " + clsCommon.ReplicateDBString + "TSPL_SRN_DETAIL.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                End If

                If strCustClass = "N" Then
                    strSql1 = strSql1
                    strSql2 = strSql2
                    strSql3 = strSql2
                    strSql4 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                    strSql5 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                    strSql6 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                    strSql7 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                    strSql8 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                    strSql9 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                End If

                If strRouteAll = "N" Then
                    strSql1 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                    strSql2 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                    strSql3 += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                    strSql4 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                    strSql5 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                    strSql6 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                    strQuery = strSql1 & Un1 & strSql2 & Un2 & strSql3 & Un3 & strSql4 & Un4 & strSql5 & Un5 & strSql6 & strOrderBy
                Else
                    strQuery = strSql1 & Un1 & strSql2 & Un2 & strSql3 & Un3 & strSql4 & Un4 & strSql5 & Un5 & strSql6 & Un6 & strSql7 & Un7 & strSql8 & Un8 & strSql9 & strOrderBy
                End If

                strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQuery, GetSelectedDatabase(), False)
                strQuery += " Order By OrderBy "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)

                If dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Item Found")
                Else
                    If rdbSummary.IsChecked = True Then
                        Dim frmcrystal As New frmCrystalReportViewer()
                        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptPrimarySaleSummaryShipping", "Primary Sale Report")
                    Else
                        Dim frmcrystal As New frmCrystalReportViewer()
                        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptPrimarySaleDetailShipping", "Primary Sale Report")
                    End If
                End If
            Else


                '''''' For accounts report addded by priti on 28.03.2012

                '' Sale Invoice Entry Date wise where location is excisable and customer class <> Franchise and intercompany


                Dim strSql4 As String = "select " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, convert(varchar, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) AS Period, " & _
                " " & strSQL2Group & " as Item_Code, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Item_Qty," & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, TSPL_ITEM_DETAILS_1.Class_Code AS Flavour, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location as Loc," & strOrderColumn & " as OrderBy,CONVERT(date, '" & dtpFdate.Value & "', 103) as SelectDate,'" & strTitle & "' as ReportType,'" & strLoca & "' as Location,'" & strClass & "' as SelectClass " & _
                " from " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                " INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                " inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code  where " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date = CONVERT(date, '" & dtpFdate.Value & "', 103) AND " & _
                "( " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
                "Cust_Type_Code Not In ('F')"
                Dim Un4 As String = " Union All "

                '' Sale Invoice Entry Month wise where location is excisable and customer class <> Franchise and intercompany

                Dim strSql5 As String = "select  " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, 'MTD' AS Period, " & strSQL2Group & " as Item_Code, " & _
                " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Item_Qty, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, " & _
                " TSPL_ITEM_DETAILS_1.Class_Code AS Flavour, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location as Loc," & strOrderColumn & " as OrderBy,CONVERT(date, '" & dtpFdate.Value & "', 103) as SelectDate,'" & strTitle & "' as ReportType,'" & strLoca & "' as Location,'" & strClass & "' as SelectClass  from " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                " INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                " inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code where " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date > = CONVERT(date, '" & strMTD & "', 103) AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date < = CONVERT(date, '" & dtpFdate.Value & "', 103) and " & _
                " (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
                " Cust_Type_Code Not In ('F')"
                Dim Un5 As String = " Union All "

                '' Sale Invoice Entry Year wise where location is excisable and customer class <> Franchise and intercompany

                Dim strSql6 As String = "select " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, 'YTD' AS Period, " & strSQL2Group & " as Item_Code, " & _
                " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Item_Qty, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, " & _
                " TSPL_ITEM_DETAILS_1.Class_Code AS Flavour, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Location as Loc ," & strOrderColumn & " as OrderBy,CONVERT(date, '" & dtpFdate.Value & "', 103) as SelectDate,'" & strTitle & "' as ReportType,'" & strLoca & "' as Location,'" & strClass & "' as SelectClass from " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " & _
                " " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location= " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
                " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
                " INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
                " inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code= " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code where " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date > = CONVERT(date, '" & strYTD & "', 103) AND " & _
                " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date < = CONVERT(date, '" & dtpFdate.Value & "', 103) and " & _
                " (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') " & _
                "  and Cust_Type_Code Not In ('F')"

                Dim strLocAll, strRouteAll, strCustClass As String
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
                If chkClassAll.IsChecked = True Then
                    strCustClass = "Y"
                Else
                    strCustClass = "N"
                End If
                Dim strQuery As String
                If strLocAll = "N" Then
                    strSql4 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strSql5 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strSql6 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "

                End If

                If strCustClass = "N" Then
                    strSql4 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                    strSql5 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                    strSql6 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                End If

                If strRouteAll = "N" Then

                    strSql4 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                    strSql5 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                    strSql6 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                    strQuery = strSql4 & Un4 & strSql5 & Un5 & strSql6 & strOrderBy
                Else
                    strQuery = strSql4 & Un4 & strSql5 & Un5 & strSql6 & strOrderBy
                End If

                strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQuery, GetSelectedDatabase(), False)
                strQuery += " Order By OrderBy "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)

                If dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Item Found")
                Else
                    If rdbSummary.IsChecked = True Then
                        Dim frmcrystal As New frmCrystalReportViewer()
                        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptPrimarySaleSummaryAccount", "Primary Sale Report")
                    Else
                        Dim frmcrystal As New frmCrystalReportViewer()
                        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptPrimarySaleDetailAccount", "Primary Sale Report")
                    End If
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.PrimarySales)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmPrimarySalesReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub FrmPrimarySalesReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")
        SetUserMgmtNew()
        gbRoot.Visible = False '''''By Pankaj(By Varun)
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        dtpFdate.Value = serverDate()
        rdbSku.IsChecked = True
        rdbSummary.IsChecked = True
        LoadCustomerClass()
        chkClassAll.IsChecked = True
        SetDataBaseGrid()
        'rbtnAllCompany.IsChecked = True
        rbtnSelectCompany.IsChecked = True
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompCode).Value), objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                gvDB.Rows(ii).Cells(colSelect).Value = 1
            End If
        Next

        rdbShip.IsChecked = True
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Sub reset()
        'gbRoot.Visible = False '''''By Pankaj(By Varun)
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        dtpFdate.Value = serverDate()
        rdbSku.IsChecked = True
        rdbSummary.IsChecked = True
        LoadCustomerClass()
        chkClassAll.IsChecked = True
        SetDataBaseGrid()
        rbtnAllCompany.IsChecked = True
        rdbShip.IsChecked = True
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged, chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkRouteAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkRouteAll.ToggleStateChanged, chkRouteSelect.ToggleStateChanged
        cbgRoute.Enabled = Not chkRouteAll.IsChecked
    End Sub
    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkClassAll.ToggleStateChanged
        chkCustomerClass.Enabled = Not chkClassAll.IsChecked
    End Sub


    Private Sub rbtnAllCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnAllCompany.ToggleStateChanged
        gvDB.Enabled = False
    End Sub

    Private Sub rbtnSelectCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnSelectCompany.ToggleStateChanged
        gvDB.Enabled = True
    End Sub


End Class
