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
Public Class FrmSecondarySales
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Dim userCode, companyCode As String
    Dim sql As String

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.SecondarySales)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
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

        'dr = dt.NewRow()
        'dr("Code") = "S"
        'dr("Name") = "SUPER DISTRIBUTOR"
        'dt.Rows.Add(dr)


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

    Private Sub FrmSecondarySales_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            'resetForm()
            reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnprint.Enabled Then
            Print()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
     
        End If
    End Sub

    Private Sub FrmSecondarySales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
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

        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")







    End Sub

    '''''''''''''''''''''''''Fills The Data OF Filter 'Company''''
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
            Dim strSQL2Group, strDate, StrMonth, strYear, strOrderColumn, strOrderBy, strTitle As String
            Dim strMTD, strYTD As String
            strTitle = ""
            strSQL2Group = ""
            strOrderColumn = ""
            strOrderBy = ""
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
                strSQL2Group = " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code"
                strOrderColumn = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq"
                strTitle = "Sku wise"
                'strOrderBy = "Order By TSPL_ITEM_MASTER.Sku_Seq"
            ElseIf rdbPack.IsChecked = True Then
                strSQL2Group = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc"
                strOrderColumn = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq"
                strTitle = "Pack wise"
                'strOrderBy = "Order By TSPL_ITEM_MASTER.Pack_Seq"
            ElseIf rdbFlavour.IsChecked = True Then
                strSQL2Group = " TSPL_ITEM_DETAILS_1.Class_Desc"
                strOrderColumn = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq"
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


            '' Sale Invoice Entry Date wise where  customer class <> Franchise and intercompany

            'Dim strSql4 As String = "select  convert(varchar,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) AS Period, " & _
            '"" & strSQL2Group & " as Item_Code, (TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Item_Qty," & _
            '"TSPL_ITEM_DETAILS.Class_Code AS pack, TSPL_ITEM_DETAILS_1.Class_Code AS Flavour, " & _
            '"TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_LOCATION_MASTER.Location_Desc,TSPL_SALE_INVOICE_DETAIL.Location as Loc," & strOrderColumn & " as OrderBy " & _
            '"from TSPL_SALE_INVOICE_HEAD inner join TSPL_SALE_INVOICE_DETAIL on " & _
            '"TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No inner join " & _
            '"TSPL_CUSTOMER_MASTER on TSPL_SALE_INVOICE_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code inner join " & _
            '"TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
            '"TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
            '"TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
            '"INNER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            '"TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            '"inner join TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code where " & _
            '"TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date = CONVERT(date, '" & dtpFdate.Value & "', 103) AND " & _
            '"(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
            '"Excisable='T' "
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
            "( " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour')  " & _
            " and Cust_Type_Code Not In ('F', 'S') and Is_Post='Y'"

            Dim Un4 As String = " Union All "


            '' Sale Invoice Entry Month wise where customer class <> Franchise and intercompany

            'Dim strSql5 As String = "select  'MTD' AS Period, " & strSQL2Group & " as Item_Code, " & _
            '"(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Item_Qty,TSPL_ITEM_DETAILS.Class_Code AS pack, " & _
            '"TSPL_ITEM_DETAILS_1.Class_Code AS Flavour,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_LOCATION_MASTER.Location_Desc, " & _
            '"TSPL_SALE_INVOICE_DETAIL.Location as Loc," & strOrderColumn & " as OrderBy  from " & _
            '"TSPL_SALE_INVOICE_HEAD inner join TSPL_SALE_INVOICE_DETAIL on " & _
            '"TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No inner join " & _
            '"TSPL_CUSTOMER_MASTER on TSPL_SALE_INVOICE_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code inner join " & _
            '"TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
            '"TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
            '"TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
            '"INNER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            '"TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            '"inner join TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code where " & _
            '"TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date > = CONVERT(date, '" & strMTD & "', 103) AND " & _
            '"TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date < = CONVERT(date, '" & dtpFdate.Value & "', 103) and " & _
            '"(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') and " & _
            '" Excisable='T' "
            Dim strSql5 As String = "select  " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, 'MTD' AS Period, " & strSQL2Group & " as Item_Code, " & _
            " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  as Item_Qty, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, " & _
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
            " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date < = CONVERT(date, '" & dtpFdate.Value & "', 103) And  " & _
            " (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour')  " & _
            "  and Cust_Type_Code Not In ('F', 'S') and Is_Post='Y'"
            Dim Un5 As String = " Union All "

            '' Sale Invoice Entry Year wise where customer class <> Franchise and intercompany

            'Dim strSql6 As String = "select 'YTD' AS Period, " & strSQL2Group & " as Item_Code, " & _
            '"(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Item_Qty,TSPL_ITEM_DETAILS.Class_Code AS pack, " & _
            '"TSPL_ITEM_DETAILS_1.Class_Code AS Flavour,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_LOCATION_MASTER.Location_Desc, " & _
            '"TSPL_SALE_INVOICE_DETAIL.Location as Loc," & strOrderColumn & " as OrderBy from " & _
            '"TSPL_SALE_INVOICE_HEAD inner join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No inner join " & _
            '"TSPL_CUSTOMER_MASTER on TSPL_SALE_INVOICE_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code inner join " & _
            '"TSPL_LOCATION_MASTER on TSPL_SALE_INVOICE_HEAD.Location=TSPL_LOCATION_MASTER.Location_Code INNER JOIN " & _
            '"TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN " & _
            '"TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
            '"INNER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            '"TSPL_SALE_INVOICE_DETAIL.Unit_code = TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            '"inner join TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code where " & _
            '"TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date > = CONVERT(date, '" & strYTD & "', 103) AND " & _
            '"TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date < = CONVERT(date, '" & dtpFdate.Value & "', 103) and " & _
            '"(TSPL_ITEM_DETAILS.Class_Name = 'size') AND (TSPL_ITEM_DETAILS_1.Class_Name = 'Flavour') " & _
            '" and Excisable='T' "
            Dim strSql6 As String = "select " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code as compCode, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name as CompName, 'YTD' AS Period, " & strSQL2Group & " as Item_Code, " & _
            " (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/ " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor)   as Item_Qty, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Code AS pack, " & _
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
            " and Cust_Type_Code Not In ('F', 'S') and Is_Post='Y'"


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

            If strRouteAll = "N" Then
                strSql4 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                strSql5 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                strSql6 += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If

            If strCustClass = "N" Then
                strSql4 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                strSql5 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
                strSql6 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in (" + clsCommon.GetMulcallString(chkCustomerClass.CheckedValue) + ") "
            End If
            strQuery = strSql4 & Un4 & strSql5 & Un5 & strSql6 & strOrderBy
            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQuery, GetSelectedDatabase(), False)
            strQuery += " Order By OrderBy "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            If dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            Else
                If rdbSummary.IsChecked = True Then
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptSecondarySaleSummary", "Secondary Sale Report")
                Else
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptSecondarySaleDetail", "Secondary Sale Report")
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Sub reset()
        LoadLocation()
        chkLocationAll.IsChecked = True
        chkClassAll.IsChecked = True
        rbtnAllCompany.IsChecked = True
        LoadRoute()
        chkRouteAll.IsChecked = True
        dtpFdate.Value = serverDate()
        rdbSku.IsChecked = True
        rdbSummary.IsChecked = True
        rdbSku.IsChecked = True
    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
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
