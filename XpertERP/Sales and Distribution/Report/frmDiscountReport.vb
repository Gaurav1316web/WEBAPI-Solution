'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 29/11/2012-------------------------------------
'--------------------------------Last modify Time - 11:10 AM -------------------------------------
'--------------------------------Last modify Date - 08/01/2013 02:15 PM -------------------------------------
'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 29/01/2013-------------------------------------
'--------------------------------Last modify Time - 04:22 PM -------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------

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
Public Class FrmDiscountReport
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery, strType As String
    Dim dr As SqlDataReader
    Dim ArrDBName As ArrayList = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strLocation, strCustAll, strPost, strInterPost, strReturnPost, strSeq, strInterGrp, strGrp, strRetGrp, strOrder As String


    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            For Each dr As DataRow In dt1.Rows
                l1User = dr(0).ToString()
                l2User = dr(1).ToString()
                l3User = dr(2).ToString()
                l4User = dr(3).ToString()
                l5User = dr(4).ToString()

            Next
        End If
    End Sub
    Sub LoadItem()
        ''Dim qry As String = "select distinct Item_Code as [Item Code],Scheme_Applicable as [Scheme Applicable] from TSPL_SALE_INVOICE_DETAIL"
        Dim qry As String = "select Item_Code as [Item Code],Item_Desc as [Item Description] from TSPL_ITEM_MASTER"
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item Code"
        cbgItem.DisplayMember = "Item Description"
    End Sub


    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        'Dim qry As String = "select Loc_Segment_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "

        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ItemDiscountReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmDiscountReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Export()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub



    Private Sub FrmDiscountReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadItem()
        chkItemAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        rdbPack.IsChecked = True
        rdbNonSampling.IsChecked = True
        rdbDirect.IsChecked = True
        rdbSummary.IsChecked = True
        LoadCompany()
        LoadLocation()
        chkLocationAll.IsChecked = True
        rdbAll.IsChecked = True
        LoadCustomer()
        chkCustAll.IsChecked = True
        rbtnCompanySelect.IsChecked = True
        Dim arrDBName As New ArrayList()
        arrDBName.Add(objCommonVar.CurrDatabase)
        cbgCompany.CheckedValue = arrDBName

        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print ")

        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub
    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name],Customer_Class as [Customer Class] from TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Code"
    End Sub
    Private Sub chkCustAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    
  
    '''' <summary>
    ''''  Customer class <> F and <> S
    '''' </summary>
    '''' <remarks> pending </remarks>
    '''' 
    Private Sub CreateTableSampligDetail()
        Try
            

            If rdbDirect.IsChecked = True Then
                strType = "Shipment_Type='Transfer'"
            ElseIf rdbIndirect.IsChecked = True Then
                strType = "Shipment_Type='Sale'"
            ElseIf rdbBoth.IsChecked = True Then
                strType = "(Shipment_Type='Sale' or Shipment_Type='Transfer')"
            End If


            If rdbAll.IsChecked = True Then
                strPost = ""
                strReturnPost = ""
                strInterPost = ""
            Else
                strPost = " and Is_Post='Y' "
                strReturnPost = " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Is_Post='Y'"
                strInterPost = " and Is_Post=1 "
            End If

            If rdbSku.IsChecked Then
                strSeq = "Sku_Seq"
                strGrp = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code"
                strOrder = ""
                strRetGrp = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code"
                strInterGrp = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code"
            Else
                strSeq = "Pack_Seq"
                strGrp = "(Class_Desc ) as Item_Code"
                strRetGrp = "(Class_Desc ) as Item_Code"
                strInterGrp = "(Class_Desc ) as Item_Code"
                strOrder = "Order by Pack_Seq asc"
            End If

            If chkLocationAll.IsChecked = True Then
                strLocation = "Y"
            Else
                strLocation = "N"
            End If
            If chkCustAll.IsChecked = True Then
                strCustAll = "Y"
            Else
                strCustAll = "N"
            End If

            Dim dt As New DataTable
            Dim strQry, strQry1, strQry2, strMainQry, strPivot, strPivotSummary, Desc, DescAmount, strDiscCodeSummary, strDiscAmtSummary, strsum, strsumQty, strPivotqry1, strPivotQry2, strPivotqry3, strPivotSum, strPivotSumQty As String
            Dim strDiscCode, strDiscCodestring, strDiscAmt, strDiscAmtString, strMainDiscCode, strMainDiscCodeString, strMainDiscAmt, strMainDiscAmtString As String
            strPivotSummary = ""
            strDiscCodestring = ""
            strDiscCode = ""
            strDiscCodestring = ""
            strDiscAmt = ""
            strDiscAmtString = ""
            strMainDiscCode = ""
            strMainDiscCodeString = ""
            strMainDiscAmt = ""
            strMainDiscAmtString = ""
            strsum = ""
            strsumQty = ""
            strDiscCodeSummary = ""
            strDiscAmtSummary = ""
            Desc = ""
            strPivotSum = ""
            strPivotSumQty = ""
            strPivotqry1 = ""
            strPivotQry2 = ""
            strPivotqry3 = ""
            strQry = "select distinct  TSPL_Discount_Master.Description as Descr from " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master RIGHT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Discount_Code ON  " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
                  "where convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
                  "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103) and (Discount_Code <> '' or Discount_Code=null) and sampling='Y' and " & strType & " and (Cust_Type_Code <> 'F') " & strPost & " "

            If strLocation = "N" Then
                strQry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQry, ArrDBName, False)
            strQuery = "select distinct * from (" + strQuery + ")abc"
            dt = clsDBFuncationality.GetDataTable(strQuery)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows

                    Desc = clsCommon.myCstr(dr("Descr"))
                    'Desc = Replace(Desc, " ", "_")
                    'Desc = Replace(Desc, "&", "and")

                    DescAmount = Desc + " Amount"

                    strDiscCodestring = strDiscCodestring & "[" & Desc & "]" & ","
                    strDiscCode = strDiscCode & "[" & Desc & "]" & ","
                    strMainDiscCodeString = strMainDiscCodeString & "  isnull(" & "Sum(" & "[" & Desc & "]" & " ) " & ",0)  " & "as  " & "[" & Desc & "]" & ","

                    strDiscAmtString = strDiscAmtString & "[" & DescAmount & "]" & ","
                    strMainDiscAmtString = strMainDiscAmtString & "  isnull(" & "Sum(" & "[" & DescAmount & "]" & " ) " & ",0)  " & "as  " & "[" & DescAmount & "]" & ","

                    strsum = strsum & "[" & DescAmount & "]" & "+"
                    strsumQty = strsumQty & "[" & Desc & "]" & "+"


                    strDiscCodeSummary = strDiscCodeSummary & " Sum(" & "[" & Desc & "]" & " ) " & "as  " & "[" & Desc & "]" & ","
                    strDiscAmtSummary = strDiscAmtSummary & " Sum(" & "[" & DescAmount & "]" & " ) " & "as  " & "[" & DescAmount & "]" & ","
                Next
            Else
                common.clsCommon.MyMessageBoxShow("No Record to Print")
                Exit Sub
            End If
            If Desc <> "" Then
                strDiscCodestring = strDiscCodestring.Substring(0, strDiscCodestring.Length - 1)
                'strMainDiscCodeString = strMainDiscCodeString.Substring(0, strMainDiscCodeString.Length - 1)
                strDiscAmtString = strDiscAmtString.Substring(0, strDiscAmtString.Length - 1)
                strMainDiscAmtString = strMainDiscAmtString.Substring(0, strMainDiscAmtString.Length - 1)
                strsum = strsum.Substring(0, strsum.Length - 1)
                strsumQty = strsumQty.Substring(0, strsumQty.Length - 1)

                strPivotqry1 = ", " & strDiscCode & " " & strDiscAmtString & ""
                strPivotQry2 = ", " & strMainDiscCodeString & " " & strMainDiscAmtString & ""
                strPivotqry3 = " pivot (sum(DiscQty) for DiscCode in ( " & strDiscCodestring & " )) as pvt1  " & _
                               "pivot (sum(DiscAmt) for DiscCodeAmt in ( " & strDiscAmtString & " )) as Pvt2 "
                strPivotSum = " " & strsum & ""
                strPivotSumQty = "  " & strsumQty & ""


                strDiscAmtSummary = strDiscAmtSummary.Substring(0, strDiscAmtSummary.Length - 1)
                strPivotSummary = ", " & strDiscCodeSummary & " " & strDiscAmtSummary & ""
            End If



            strMainQry = "select (Sale_Invoice_No),(Item_Code)," & strSeq & ",(MRPCase),MRPBottle,(TP),DP, " & _
            "Location_Code,Location_Desc,Route_No,Route_Desc,Cust_Code, " & _
            "Customer_Name, Cust_Type_Code, Cust_Type_Desc, Cust_Group_Code, Cust_Group_Desc,Salesman_Code,Salesman_Desc,   " & _
            "(" & strPivotSumQty & ") as [Total Qty], " & _
            "(" & strPivotSum & ") as [Total Amount] " & _
            " " & strPivotqry1 & " " & _
            "from ( " & _
            "select   " & strSeq & ", MRPCase/MrpBottleConvRate as MRPBottle,Sale_Invoice_No, item_code, MRPCase,TP,DP,Location_Code,Location_Desc,Route_No,Route_Desc, " & _
            "Cust_Code,Customer_Name,Cust_Type_Code,Cust_Type_Desc,Cust_Group_Code,Cust_Group_Desc,Salesman_Code,Salesman_Desc   " & _
            " " & strPivotQry2 & " " & _
            " from " & _
             " (  "
            strQry1 = "SELECT  " & strSeq & ",TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MrpBottleConvRate, " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as CaseConF, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, Cust_Type_Desc,Cust_Group_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id, " & strGrp & " , " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code,Salesman_Desc  , " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRPCase, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 AS TP, " & _
                                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9) AS DP, " & _
            "CASE WHEN Discount_Code <> '' AND sampling = 'Y' THEN TSPL_Discount_Master.Description ELSE '' END AS DiscCode, " & _
            "CASE WHEN Discount_Code <> '' AND sampling = 'Y' THEN (TSPL_Discount_Master.Description) + ' Amount' ELSE '' END AS DiscCodeAmt, " & _
            "CASE WHEN Discount_Code <> '' AND sampling = 'Y' THEN CONVERT(decimal(18, 2), Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END AS DiscQty, " & _
            "CASE WHEN Discount_Code <> '' AND sampling = 'Y' THEN CONVERT(decimal(18, 2), (Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END AS DiscAmt " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Discount_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
            "left outer join  TSPL_ITEM_DETAILS on TSPL_SALE_INVOICE_DETAIL.item_code= TSPL_ITEM_DETAILS.item_code left outer join TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
            "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " & _
            "TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "WHERE  convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103) AND " & _
            " " & strType & " AND (" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code <> 'F') and " & _
            "( Class_Name='size' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB') and ( sampling='Y' ) "

            strQry2 = " SELECT   " & strSeq & ",TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MrpBottleConvRate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as CaseConF, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No AS Sale_Invoice_No, " & _
            "Cust_Type_Desc,Cust_Group_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_Id AS Sale_Invoice_Id, " & _
            "" & strRetGrp & " , " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Salesman_Code, " & _
            "Salesman_Desc  , " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRPCase, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount1 AS TP, " & _
                                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9) AS DP, " & _
            "CASE WHEN Discount_Code <> '' AND sampling = 'Y' THEN TSPL_Discount_Master.Description ELSE '' END AS DiscCode, " & _
            "CASE WHEN Discount_Code <> '' AND sampling = 'Y' THEN (TSPL_Discount_Master.Description) + ' Amount' ELSE '' END AS DiscCodeAmt, " & _
            "-(CASE WHEN Discount_Code <> '' AND sampling = 'Y' THEN CONVERT(decimal(18, 2), Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS DiscQty, " & _
            "-(CASE WHEN Discount_Code <> '' AND sampling = 'Y' THEN CONVERT(decimal(18, 2), (Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS DiscAmt " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code " & _
            "RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_Discount_Master RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Discount_Code  " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code  " & _
            "RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
            "left outer join  TSPL_ITEM_DETAILS on " & _
            "TSPL_SALE_RETURN_DETAIL.item_code= TSPL_ITEM_DETAILS.item_code left outer join " & _
            "TSPL_ITEM_MASTER on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on " & _
            "TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code WHERE " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & ToDate.Value & "',103) AND  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code <> 'F') and " & _
            "( Class_Name='size' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB') and " & _
            "(sampling='Y' )  and " & strType & " " & strReturnPost & "  "

            'strInterComp = "SELECT   " & strSeq & ",TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MrpBottleConvRate, " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as CaseConF, " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No AS Sale_Invoice_No, " & _
            '"Cust_Type_Desc,Cust_Group_Desc, 1 AS Sale_Invoice_Id, " & _
            '"" & strInterGrp & " , " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_Desc, " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code, Salesman_Desc  , " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRPCase, " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount1 AS TP, " & _
            '"'' AS DiscCode,'' AS DiscCodeAmt, " & _
            '"0 AS DiscQty, 0 AS DiscAmt FROM  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code " & _
            '"= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code  " & _
            '"= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code RIGHT OUTER JOIN " & _
            '"TSPL_SALE_RETURN_INTER_DETAIL LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            '"RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD ON " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No ON " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code " & _
            '"LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " & _
            '"" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
            '"left outer join  TSPL_ITEM_DETAILS on " & _
            '"TSPL_SALE_RETURN_INTER_DETAIL.item_code= TSPL_ITEM_DETAILS.item_code left outer join " & _
            '"TSPL_ITEM_MASTER on TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
            '"left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on " & _
            '"TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code WHERE " & _
            '"convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) >= " & _
            '" convert(date, '" & fromDate.Value & "',103) AND " & _
            '"convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) <= " & _
            '" convert(date, '" & ToDate.Value & "',103) and ( Class_Name='size' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB') and " & _
            '"(Price_Amount4 <> 0 or Price_Amount5 <> 0  ) " & strInterPost & "  "


            If strLocation = "N" Then
                strQry1 += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                strQry2 += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                'strInterComp += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If strCustAll = "N" Then
                strQry1 += " and TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                strQry2 += " and TSPL_SALE_RETURN_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "

            End If
            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If

            If rdbIterComp.Checked Then
                strQry = strQry1 & " Union All " & strQry2
            Else
                strQry = strQry1 & " Union All " & strQry2
            End If

            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQry, ArrDBName, False)
            dt = clsDBFuncationality.GetDataTable(strQuery) '

            strPivot = " )a " & strPivotqry3 & "  " & _
            "group by Sale_Invoice_No,item_code,MRPCase,TP,DP,Location_Code,Location_Desc, " & _
            "Route_No, Route_Desc, Cust_Code, Customer_Name, Cust_Type_Code, Cust_Type_Desc, " & _
            "Cust_Group_Code, Cust_Group_Desc,Salesman_Code,Salesman_Desc," & strSeq & ",MrpBottleConvRate " & _
            "  ) b"

            If rdbSummary.IsChecked Then
                strQry = "select distinct Item_Code," & strSeq & ",TP,DP,MRPCase,MRPBottle," & _
                "SUM([Total Qty]) as [Total Qty], " & _
                "SUM([Total Amount]) as [Total Amount], " & _
                "case when SUM([Total Qty]) <> 0 then sum([Total Amount])/SUM([Total Qty]) else 0 end as Amountcase  " & strPivotSummary & "  " & _
                "from   ( " & strMainQry & strQuery & strPivot & " )C  group by (Item_Code),(MRPCase),(TP),DP,MRPBottle, " & strSeq & "  " & strOrder & " "
            Else
                strQry = strMainQry & strQuery & strPivot
            End If



            'strQry = strQry

            dt = clsDBFuncationality.GetDataTable(strQry)
            gvReport.DataSource = Nothing
            gvReport.Columns.Clear()
            gvReport.Rows.Clear()
            gvReport.GroupDescriptors.Clear()
            gvReport.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                gvReport.DataSource = dt
                SetGridFormationOFgvReport()
            End If





        

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try



        'MsgBox("successfully")
    End Sub
   
   
    '''' <summary>
    ''''  Customer class <> F and <> S
    '''' </summary>
    '''' <remarks> pending </remarks>
    '''' 
    Private Sub CreateTableOtherDetail()
        Try

            If rdbDirect.IsChecked = True Then
                strType = "Shipment_Type='Transfer'"
            ElseIf rdbIndirect.IsChecked = True Then
                strType = "Shipment_Type='Sale'"
            ElseIf rdbBoth.IsChecked = True Then
                strType = "(Shipment_Type='Sale' or Shipment_Type='Transfer')"
            End If


            If rdbAll.IsChecked = True Then
                strPost = ""
                strReturnPost = ""
                strInterPost = ""
            Else
                strPost = " and Is_Post='Y' "
                strReturnPost = " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Is_Post='Y'"
                strInterPost = " and Is_Post=1 "
            End If

            If rdbSku.IsChecked Then
                strSeq = "Sku_Seq"
                strGrp = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code"
                strOrder = ""
                strRetGrp = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code"
                strInterGrp = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code"
            Else
                strSeq = "Pack_Seq"
                strGrp = "(Class_Desc ) as Item_Code"
                strRetGrp = "(Class_Desc ) as Item_Code"
                strInterGrp = "(Class_Desc ) as Item_Code"
                strOrder = "Order by Pack_Seq asc"
            End If

            If chkLocationAll.IsChecked = True Then
                strLocation = "Y"
            Else
                strLocation = "N"
            End If
            If chkCustAll.IsChecked = True Then
                strCustAll = "Y"
            Else
                strCustAll = "N"
            End If

            Dim dt As New DataTable
            Dim strQry, strQry1, strQry2, strMainQry, strPivot, strPivotSummary, Desc, DescAmount, strDiscCodeSummary, strDiscAmtSummary, strsum, strsumQty, strPivotqry1, strPivotQry2, strPivotqry3, strPivotSum, strPivotSumQty As String
            Dim strDiscCode, strDiscCodestring, strDiscAmtString, strMainDiscCodeString, strMainDiscAmtString As String
            strPivotSummary = ""
            strDiscCodestring = ""
            strPivotQry2 = ""
            strPivotqry3 = ""
            strDiscCode = ""
            strPivotSumQty = ""
            strPivotSum = ""
            strPivotqry1 = ""
            strMainDiscCodeString = ""
            strDiscAmtString = ""
            strMainDiscAmtString = ""
            strsum = ""
            Desc = ""
            strsumQty = ""
            strDiscCodeSummary = ""
            strDiscAmtSummary = ""
            strQry = "select distinct  TSPL_Discount_Master.Description as Descr from " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master RIGHT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Discount_Code ON  " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
                  "where convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
                  "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103) and (Discount_Code <> '' or Discount_Code=null) and Other='Y' and " & strType & " and (Cust_Type_Code <> 'F') " & strPost & " "

            If strLocation = "N" Then
                strQry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQry, ArrDBName, False)
            strQuery = "select distinct * from (" + strQuery + ")abc"
            dt = clsDBFuncationality.GetDataTable(strQuery)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows

                    Desc = clsCommon.myCstr(dr("Descr"))
                    'Desc = Replace(Desc, " ", "_")
                    'Desc = Replace(Desc, "&", "and")

                    DescAmount = Desc + " Amount"

                    strDiscCodestring = strDiscCodestring & "[" & Desc & "]" & ","
                    strDiscCode = strDiscCode & "[" & Desc & "]" & ","
                    strMainDiscCodeString = strMainDiscCodeString & "  isnull(" & "Sum(" & "[" & Desc & "]" & " ) " & ",0)  " & "as  " & "[" & Desc & "]" & ","

                    strDiscAmtString = strDiscAmtString & "[" & DescAmount & "]" & ","
                    strMainDiscAmtString = strMainDiscAmtString & "  isnull(" & "Sum(" & "[" & DescAmount & "]" & " ) " & ",0)  " & "as  " & "[" & DescAmount & "]" & ","

                    strsum = strsum & "[" & DescAmount & "]" & "+"
                    strsumQty = strsumQty & "[" & Desc & "]" & "+"


                    strDiscCodeSummary = strDiscCodeSummary & " Sum(" & "[" & Desc & "]" & " ) " & "as  " & "[" & Desc & "]" & ","
                    strDiscAmtSummary = strDiscAmtSummary & " Sum(" & "[" & DescAmount & "]" & " ) " & "as  " & "[" & DescAmount & "]" & ","
                Next
            Else
                common.clsCommon.MyMessageBoxShow("No Record to Print")
                Exit Sub
            End If

            If Desc <> "" Then
                strDiscCodestring = strDiscCodestring.Substring(0, strDiscCodestring.Length - 1)
                'strMainDiscCodeString = strMainDiscCodeString.Substring(0, strMainDiscCodeString.Length - 1)
                strDiscAmtString = strDiscAmtString.Substring(0, strDiscAmtString.Length - 1)
                strMainDiscAmtString = strMainDiscAmtString.Substring(0, strMainDiscAmtString.Length - 1)
                strsum = strsum.Substring(0, strsum.Length - 1)
                strsumQty = strsumQty.Substring(0, strsumQty.Length - 1)

                strPivotqry1 = ", " & strDiscCode & " " & strDiscAmtString & ""
                strPivotQry2 = ", " & strMainDiscCodeString & " " & strMainDiscAmtString & ""
                strPivotqry3 = " pivot (sum(DiscQty) for DiscCode in ( " & strDiscCodestring & " )) as pvt1  " & _
                               "pivot (sum(DiscAmt) for DiscCodeAmt in ( " & strDiscAmtString & " )) as Pvt2 "
                strPivotSum = " " & strsum & ""
                strPivotSumQty = "  " & strsumQty & ""


                strDiscAmtSummary = strDiscAmtSummary.Substring(0, strDiscAmtSummary.Length - 1)
                strPivotSummary = ", " & strDiscCodeSummary & " " & strDiscAmtSummary & ""
            End If



            strMainQry = "select (Sale_Invoice_No),(Item_Code)," & strSeq & ",(MRPCase),MRPBottle,(TP),DP, " & _
            "Location_Code,Location_Desc,Route_No,Route_Desc,Cust_Code, " & _
            "Customer_Name, Cust_Type_Code, Cust_Type_Desc, Cust_Group_Code, Cust_Group_Desc,Salesman_Code,Salesman_Desc,   " & _
            "(" & strPivotSumQty & ") as [Total Qty], " & _
            "(" & strPivotSum & ") as [Total Amount] " & _
            " " & strPivotqry1 & " " & _
            "from ( " & _
            "select   " & strSeq & ", MRPCase/MrpBottleConvRate as MRPBottle,Sale_Invoice_No, item_code, MRPCase,TP,DP,Location_Code,Location_Desc,Route_No,Route_Desc, " & _
            "Cust_Code,Customer_Name,Cust_Type_Code,Cust_Type_Desc,Cust_Group_Code,Cust_Group_Desc,Salesman_Code,Salesman_Desc   " & _
            " " & strPivotQry2 & " " & _
            " from " & _
             " (  "
            strQry1 = "SELECT  " & strSeq & ",TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MrpBottleConvRate, " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as CaseConF, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, Cust_Type_Desc,Cust_Group_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id, " & strGrp & " , " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code,Salesman_Desc  , " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRPCase, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 AS TP, " & _
                                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9) AS DP, " & _
            "CASE WHEN Discount_Code <> '' AND Other = 'Y' THEN TSPL_Discount_Master.Description ELSE '' END AS DiscCode, " & _
            "CASE WHEN Discount_Code <> '' AND Other = 'Y' THEN (TSPL_Discount_Master.Description) + ' Amount' ELSE '' END AS DiscCodeAmt, " & _
            "CASE WHEN Discount_Code <> '' AND Other = 'Y' THEN CONVERT(decimal(18, 2), Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END AS DiscQty, " & _
            "CASE WHEN Discount_Code <> '' AND Other = 'Y' THEN CONVERT(decimal(18, 2), (Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END AS DiscAmt " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Discount_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
            "left outer join  TSPL_ITEM_DETAILS on TSPL_SALE_INVOICE_DETAIL.item_code= TSPL_ITEM_DETAILS.item_code left outer join TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
            "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " & _
            "TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "WHERE  convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103) AND " & _
            " " & strType & " AND (" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code <> 'F') and " & _
            "( Class_Name='size' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB') and ( Other='Y' ) "

            strQry2 = " SELECT   " & strSeq & ",TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MrpBottleConvRate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as CaseConF, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No AS Sale_Invoice_No, " & _
            "Cust_Type_Desc,Cust_Group_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_Id AS Sale_Invoice_Id, " & _
            "" & strRetGrp & " , " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Salesman_Code, " & _
            "Salesman_Desc  , " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRPCase, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount1 AS TP, " & _
                                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9) AS DP, " & _
            "CASE WHEN Discount_Code <> '' AND Other = 'Y' THEN TSPL_Discount_Master.Description ELSE '' END AS DiscCode, " & _
            "CASE WHEN Discount_Code <> '' AND Other = 'Y' THEN (TSPL_Discount_Master.Description) + ' Amount' ELSE '' END AS DiscCodeAmt, " & _
            "-(CASE WHEN Discount_Code <> '' AND Other = 'Y' THEN CONVERT(decimal(18, 2), Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS DiscQty, " & _
            "-(CASE WHEN Discount_Code <> '' AND Other = 'Y' THEN CONVERT(decimal(18, 2), (Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS DiscAmt " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code " & _
            "RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_Discount_Master RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Discount_Code  " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code  " & _
            "RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
            "left outer join  TSPL_ITEM_DETAILS on " & _
            "TSPL_SALE_RETURN_DETAIL.item_code= TSPL_ITEM_DETAILS.item_code left outer join " & _
            "TSPL_ITEM_MASTER on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on " & _
            "TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code WHERE " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & ToDate.Value & "',103) AND  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code <> 'F') and " & _
            "( Class_Name='size' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB') and " & _
            "(Other='Y' )  and " & strType & " " & strReturnPost & "  "



            If strLocation = "N" Then
                strQry1 += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                strQry2 += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                'strInterComp += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If strCustAll = "N" Then
                strQry1 += " and TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                strQry2 += " and TSPL_SALE_RETURN_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "

            End If
            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If

            If rdbIterComp.Checked Then
                strQry = strQry1 & " Union All " & strQry2
            Else
                strQry = strQry1 & " Union All " & strQry2
            End If

            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQry, ArrDBName, False)
            dt = clsDBFuncationality.GetDataTable(strQuery) '

            strPivot = " )a " & strPivotqry3 & "  " & _
            "group by Sale_Invoice_No,item_code,MRPCase,TP,DP,Location_Code,Location_Desc, " & _
            "Route_No, Route_Desc, Cust_Code, Customer_Name, Cust_Type_Code, Cust_Type_Desc, " & _
            "Cust_Group_Code, Cust_Group_Desc,Salesman_Code,Salesman_Desc," & strSeq & ",MrpBottleConvRate " & _
            "  ) b"

            If rdbSummary.IsChecked Then
                strQry = "select distinct Item_Code," & strSeq & ",TP,DP,MRPCase,MRPBottle," & _
                "SUM([Total Qty]) as [Total Qty], " & _
                "SUM([Total Amount]) as [Total Amount], " & _
                "case when SUM([Total Qty]) <> 0 then sum([Total Amount])/SUM([Total Qty]) else 0 end as Amountcase  " & strPivotSummary & "  " & _
                "from   ( " & strMainQry & strQuery & strPivot & " )C  group by (Item_Code),(MRPCase),(TP),DP,MRPBottle, " & strSeq & "  " & strOrder & " "
            Else
                strQry = strMainQry & strQuery & strPivot
            End If



            'strQry = strQry

            dt = clsDBFuncationality.GetDataTable(strQry)
            gvReport.DataSource = Nothing
            gvReport.Columns.Clear()
            gvReport.Rows.Clear()
            gvReport.GroupDescriptors.Clear()
            gvReport.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                gvReport.DataSource = dt
                SetGridFormationOFgvReport()
            End If


         


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try



        'MsgBox("successfully")
    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub
    
   
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
        gvReport.DataSource = Nothing
        gvReport.Columns.Clear()
        gvReport.Rows.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub reset()
        LoadItem()
        chkItemAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        rdbPack.IsChecked = True
        rdbNonSampling.IsChecked = True
        rdbDirect.IsChecked = True
        rdbSummary.IsChecked = True
        rbtnCompanyAll.IsChecked = True
        LoadLocation()
        rdbAll.IsChecked = True
    End Sub
    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub

    
    Private Sub RadPageView1_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadPageView1.SelectedPageChanged

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            Return

        End If

        gvReport.EnableFiltering = True
        If rdbSummary.IsChecked = True Then
            Dim strCollection As String = ""
            Dim str As String = ""
            Dim strCmd As String = ""
            Dim dt As DataTable
            Dim StrBase As String
            If rdbNonSampling.IsChecked = True Then
                CreateTableNonSampligDetail()
                
                If rdbFlavour.IsChecked = True Then
                   
                ElseIf rdbPack.IsChecked = True Then
                   

                ElseIf rdbSku.IsChecked = True Then
                   

                End If

            ElseIf rdbSampling.IsChecked = True Then
                CreateTableSampligDetail()
                
                If rdbFlavour.IsChecked = True Then
                  
                ElseIf rdbPack.IsChecked = True Then
                    strCollection = ""
                    str = ""
                  
                ElseIf rdbSku.IsChecked = True Then

                    strCollection = ""
                    str = ""
                    
                End If

            ElseIf rdbVSD.IsChecked = True Then
                CreateTableVSandDDetail()
                StrBase = "select COLUMN_NAME ,'['+REPLACE(COLUMN_NAME,'_',' ')+']'  from INFORMATION_SCHEMA.COLUMNS  where table_name='tspl_tempVSD_discount'"
                dt = clsDBFuncationality.GetDataTable(StrBase)
                For Each dr As DataRow In dt.Rows
                    'strCollection += "tspl_tempVSD_discount." + dr(0).ToString()
                    'strCollection += (" As ")
                    'strCollection += dr(1).ToString() + ","
                Next
                If rdbFlavour.IsChecked = True Then
                   
                ElseIf rdbPack.IsChecked = True Then
                   
                ElseIf rdbSku.IsChecked = True Then
                   
                End If

            ElseIf rdbOther.IsChecked = True Then
                CreateTableOtherDetail()
                
                If rdbFlavour.IsChecked = True Then
                   
                ElseIf rdbPack.IsChecked = True Then
                    
                ElseIf rdbSku.IsChecked = True Then
                   
                End If

            End If
        Else
            Dim strCollection As String = ""
            Dim str As String = ""
            Dim strCmd As String = ""
            Dim dt As DataTable
            Dim StrBase As String
            If rdbNonSampling.IsChecked = True Then
                CreateTableNonSampligDetail()
                StrBase = "select COLUMN_NAME ,'['+REPLACE(COLUMN_NAME,'_',' ')+']'  from INFORMATION_SCHEMA.COLUMNS  where table_name='TSPL_TEMP_DISCOUNT'"
                dt = clsDBFuncationality.GetDataTable(StrBase)

                strCollection = ""
                str = ""
                
            ElseIf rdbSampling.IsChecked = True Then
                CreateTableSampligDetail()
                StrBase = "select COLUMN_NAME ,'['+REPLACE(COLUMN_NAME,'_',' ')+']'  from INFORMATION_SCHEMA.COLUMNS  where table_name='TSPL_TEMPSampling_DISCOUNT'"
                dt = clsDBFuncationality.GetDataTable(StrBase)

                strCollection = ""
                str = ""
                
            ElseIf rdbVSD.IsChecked = True Then
                CreateTableVSandDDetail()
               
            ElseIf rdbOther.IsChecked = True Then
                CreateTableOtherDetail()
                'StrBase = "select COLUMN_NAME ,'['+REPLACE(COLUMN_NAME,'_',' ')+']'  from INFORMATION_SCHEMA.COLUMNS  where table_name='TSPL_TEMPOther_DISCOUNT'"
                'dt = clsDBFuncationality.GetDataTable(StrBase)
                'strCollection = ""
                'str = ""
                
            End If


        End If

    End Sub

    Sub SetGridFormationOFgvReport()
        Dim strItemCode As String



        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            gvReport.Columns(ii).IsVisible = False
        Next

        If rdbNonSampling.IsChecked = True Then
            If rdbSummary.IsChecked = True Then
                If rdbSku.IsChecked = True Then
                    gvReport.Columns("Item_Code").IsVisible = True
                    gvReport.Columns("Item_Code").Width = 50
                    gvReport.Columns("Item_Code").HeaderText = "Item Code"
                    'gvReport.Columns("Item Code").BestFit()
                Else
                    gvReport.Columns("Item_Code").IsVisible = True
                    gvReport.Columns("Item_Code").Width = 50
                    gvReport.Columns("Item_Code").HeaderText = "Pack Code"
                    'gvReport.Columns("Pack").BestFit()
                End If

                If rdbSku.IsChecked = True Then
                    gvReport.Columns("Sku_Seq").IsVisible = True
                    gvReport.Columns("Sku_Seq").Width = 50
                    gvReport.Columns("Sku_Seq").HeaderText = "Sku Seq No"
                    'gvReport.Columns("Item Code").BestFit()
                Else
                    gvReport.Columns("Pack_Seq").IsVisible = True
                    gvReport.Columns("Pack_Seq").Width = 50
                    gvReport.Columns("Pack_Seq").HeaderText = "Pack Seq No"
                End If
              

                gvReport.Columns("MRPBottle").IsVisible = True
                gvReport.Columns("MRPBottle").Width = 50
                gvReport.Columns("MRPBottle").HeaderText = "MRP Per Bottle"
                'gvReport.Columns("MRP Per Bottle").BestFit()

                gvReport.Columns("MRPCase").IsVisible = True
                gvReport.Columns("MRPCase").Width = 50
                gvReport.Columns("MRPCase").HeaderText = "MRP Per Case"
                'gvReport.Columns("MRP Per Case").BestFit()

                gvReport.Columns("TP").IsVisible = True
                gvReport.Columns("TP").Width = 100
                gvReport.Columns("TP").HeaderText = "Trade Price"

                gvReport.Columns("DP").IsVisible = True
                gvReport.Columns("DP").Width = 100
                gvReport.Columns("DP").HeaderText = "Distributor Price"
                'gvReport.Columns("Trade Price").BestFit()

                gvReport.Columns("BottScheme").IsVisible = True
                gvReport.Columns("BottScheme").Width = 70
                gvReport.Columns("BottScheme").HeaderText = "BS Scheme"
                'gvReport.Columns("BS Scheme").BestFit()

                gvReport.Columns("Sale").IsVisible = True
                gvReport.Columns("Sale").Width = 150
                gvReport.Columns("Sale").HeaderText = "Sale"
                'gvReport.Columns("Sale").BestFit()

                gvReport.Columns("Trade Discount Amount").IsVisible = True
                gvReport.Columns("Trade Discount Amount").Width = 70
                gvReport.Columns("Trade Discount Amount").HeaderText = "Distributor Disc Amt"
                'gvReport.Columns("Trade Discount Amt").BestFit()


                gvReport.Columns("Cash Discount Qty").IsVisible = True
                gvReport.Columns("Cash Discount Qty").Width = 70
                gvReport.Columns("Cash Discount Qty").HeaderText = "Cash Discount Quantity"
                'gvReport.Columns("Cash Discount Quantity").BestFit()


                gvReport.Columns("Cash Discount Amount").IsVisible = True
                gvReport.Columns("Cash Discount Amount").Width = 70
                gvReport.Columns("Cash Discount Amount").HeaderText = "Cash Discount Amount"
                'gvReport.Columns("Cash Discount Amount").BestFit()


                gvReport.Columns("Key Account and MT Quantity").IsVisible = True
                gvReport.Columns("Key Account and MT Quantity").Width = 50
                gvReport.Columns("Key Account and MT Quantity").HeaderText = "Key Account and MT Quantity"
                'gvReport.Columns("Key Account and MT Quantity").BestFit()


                gvReport.Columns("Key Account and MT Amount").IsVisible = True
                gvReport.Columns("Key Account and MT Amount").Width = 50
                gvReport.Columns("Key Account and MT Amount").HeaderText = "Key Account and MT Amount"
                'gvReport.Columns("Key Account and MT Amount").BestFit()


                gvReport.Columns("Total Quantity").IsVisible = True
                gvReport.Columns("Total Quantity").Width = 50
                gvReport.Columns("Total Quantity").HeaderText = "Total Quantity"
                'gvReport.Columns("Total Quantity").BestFit()

                gvReport.Columns("Total Discount Amount").IsVisible = True
                gvReport.Columns("Total Discount Amount").Width = 80
                gvReport.Columns("Total Discount Amount").HeaderText = "Total Discount Amount"
                'gvReport.Columns("Total Discount Amount").BestFit()

                gvReport.Columns("Amountcase").IsVisible = True
                gvReport.Columns("Amountcase").Width = 80
                gvReport.Columns("Amountcase").HeaderText = "Dicount Amount Per Case"
                'gvReport.Columns("Dicount Amount Per Case").BestFit()




                For ii As Integer = 15 To gvReport.Columns.Count - 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    gvReport.Columns("" & strItemCode & "").IsVisible = True
                    gvReport.Columns("" & strItemCode & "").Width = 80
                    gvReport.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
                    'gvReport.Columns("" & strItemCode & "").BestFit()
                Next


                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0

                Dim item1 As New GridViewSummaryItem("Sale", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("Trade Discount Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("Cash Discount Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                Dim item4 As New GridViewSummaryItem("Cash Discount Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item4)
                Dim item5 As New GridViewSummaryItem("Key Account and MT Quantity", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item5)
                Dim item6 As New GridViewSummaryItem("Key Account and MT Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item6)
                Dim item7 As New GridViewSummaryItem("Total Quantity", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item7)
                Dim item8 As New GridViewSummaryItem("Total Discount Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item8)

                For ii As Integer = 15 To gvReport.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Else

                gvReport.Columns("Sale_Invoice_No").IsVisible = True
                gvReport.Columns("Sale_Invoice_No").Width = 100
                gvReport.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"
                'gvReport.Columns("Sale Invoice No").BestFit()

                gvReport.Columns("Location_Code").IsVisible = True
                gvReport.Columns("Location_Code").Width = 50
                gvReport.Columns("Location_Code").HeaderText = "Location"

                gvReport.Columns("Location_Desc").IsVisible = True
                gvReport.Columns("Location_Desc").Width = 50
                gvReport.Columns("Location_Desc").HeaderText = "Location Desc"
                'gvReport.Columns("Location").BestFit()

                gvReport.Columns("Cust_Code").IsVisible = True
                gvReport.Columns("Cust_Code").Width = 50
                gvReport.Columns("Cust_Code").HeaderText = "Cust Code"

                gvReport.Columns("Customer_Name").IsVisible = True
                gvReport.Columns("Customer_Name").Width = 50
                gvReport.Columns("Customer_Name").HeaderText = "Cust Name"
                'gvReport.Columns("Cust Code").BestFit()

                gvReport.Columns("Cust_Group_Code").IsVisible = True
                gvReport.Columns("Cust_Group_Code").Width = 50
                gvReport.Columns("Cust_Group_Code").HeaderText = "Cust Group Code"
                'gvReport.Columns("Cust Group Code").BestFit()

                gvReport.Columns("Cust_Group_Desc").IsVisible = True
                gvReport.Columns("Cust_Group_Desc").Width = 50
                gvReport.Columns("Cust_Group_Desc").HeaderText = "Cust Group Desc"
                'gvReport.Columns("Cust Group Desc").BestFit()

                gvReport.Columns("Cust_Type_Code").IsVisible = True
                gvReport.Columns("Cust_Type_Code").Width = 20
                gvReport.Columns("Cust_Type_Code").HeaderText = "Cust Type Code"
                'gvReport.Columns("Cust Type Code").BestFit()


                gvReport.Columns("Cust_Type_Desc").IsVisible = True
                gvReport.Columns("Cust_Type_Desc").Width = 50
                gvReport.Columns("Cust_Type_Desc").HeaderText = "Cust Type Desc"
                'gvReport.Columns("Cust Type Desc").BestFit()

                gvReport.Columns("Salesman_Code").IsVisible = True
                gvReport.Columns("Salesman_Code").Width = 50
                gvReport.Columns("Salesman_Code").HeaderText = "Salesman Code"
                'gvReport.Columns("Salesman Code").BestFit()

                gvReport.Columns("Salesman_Desc").IsVisible = True
                gvReport.Columns("Salesman_Desc").Width = 50
                gvReport.Columns("Salesman_Desc").HeaderText = "Salesman Desc"
                'gvReport.Columns("Salesman Desc").BestFit()

                gvReport.Columns("Route_No").IsVisible = True
                gvReport.Columns("Route_No").Width = 50
                gvReport.Columns("Route_No").HeaderText = "Route No"
                'gvReport.Columns("Route No").BestFit()


                gvReport.Columns("Route_Desc").IsVisible = True
                gvReport.Columns("Route_Desc").Width = 50
                gvReport.Columns("Route_Desc").HeaderText = "Route Desc"
                'gvReport.Columns("Route Desc").BestFit()

                gvReport.Columns("Item_Code").IsVisible = True
                gvReport.Columns("Item_Code").Width = 50
                gvReport.Columns("Item_Code").HeaderText = "Item Code"
                'gvReport.Columns("Item Code").BestFit()

                'gvReport.Columns("MRP Per Bottle").IsVisible = True
                'gvReport.Columns("MRP Per Bottle").Width = 50
                'gvReport.Columns("MRP Per Bottle").HeaderText = "MRP Per Bottle"
                ''gvReport.Columns("MRP Per Bottle").BestFit()

                gvReport.Columns("MRPCase").IsVisible = True
                gvReport.Columns("MRPCase").Width = 50
                gvReport.Columns("MRPCase").HeaderText = "MRP Per Case"
                'gvReport.Columns("MRP Per Case").BestFit()

                gvReport.Columns("TP").IsVisible = True
                gvReport.Columns("TP").Width = 50
                gvReport.Columns("TP").HeaderText = "Trade Price"


                gvReport.Columns("DP").IsVisible = True
                gvReport.Columns("DP").Width = 100
                gvReport.Columns("DP").HeaderText = "Distributor Price"
                'gvReport.Columns("BS Scheme").IsVisible = True
                'gvReport.Columns("BS Scheme").Width = 70
                'gvReport.Columns("BS Scheme").HeaderText = "BS Scheme"
                'gvReport.Columns("BS Scheme").BestFit()

                gvReport.Columns("Sale").IsVisible = True
                gvReport.Columns("Sale").Width = 70
                gvReport.Columns("Sale").HeaderText = "Sale"
                'gvReport.Columns("Sale").BestFit()

                gvReport.Columns("FOCQty").IsVisible = True
                gvReport.Columns("FOCQty").Width = 70
                gvReport.Columns("FOCQty").HeaderText = "Distributor Disc Qty"

                gvReport.Columns("Trade Discount Amount").IsVisible = True
                gvReport.Columns("Trade Discount Amount").Width = 70
                gvReport.Columns("Trade Discount Amount").HeaderText = "Distributor Disc Amount"
                'gvReport.Columns("Trade Discount Amt").BestFit()


                gvReport.Columns("Cash Discount Qty").IsVisible = True
                gvReport.Columns("Cash Discount Qty").Width = 70
                gvReport.Columns("Cash Discount Qty").HeaderText = "Cash Discount Quantity"
                'gvReport.Columns("Cash Discount Quantity").BestFit()


                gvReport.Columns("Cash Discount Amount").IsVisible = True
                gvReport.Columns("Cash Discount Amount").Width = 70
                gvReport.Columns("Cash Discount Amount").HeaderText = "Cash Discount Amount"
                'gvReport.Columns("Cash Discount Amount").BestFit()


                gvReport.Columns("Key Account and MT Quantity").IsVisible = True
                gvReport.Columns("Key Account and MT Quantity").Width = 50
                gvReport.Columns("Key Account and MT Quantity").HeaderText = "Key Account and MT Quantity"
                'gvReport.Columns("Key Account and MT Quantity").BestFit()


                gvReport.Columns("Key Account and MT Amount").IsVisible = True
                gvReport.Columns("Key Account and MT Amount").Width = 50
                gvReport.Columns("Key Account and MT Amount").HeaderText = "Key Account and MT Amount"
                'gvReport.Columns("Key Account and MT Amount").BestFit()


                gvReport.Columns("Total Quantity").IsVisible = True
                gvReport.Columns("Total Quantity").Width = 50
                gvReport.Columns("Total Quantity").HeaderText = "Total Quantity"
                'gvReport.Columns("Total Quantity").BestFit()

                gvReport.Columns("Total Discount Amount").IsVisible = True
                gvReport.Columns("Total Discount Amount").Width = 80
                gvReport.Columns("Total Discount Amount").HeaderText = "Total Discount Amount"
                'gvReport.Columns("Total Discount Amount").BestFit()

                'gvReport.Columns("Dicount Amount Per Case").IsVisible = True
                'gvReport.Columns("Dicount Amount Per Case").Width = 80
                'gvReport.Columns("Dicount Amount Per Case").HeaderText = "Dicount Amount Per Case"
                ''gvReport.Columns("Dicount Amount Per Case").BestFit()




                For ii As Integer = 28 To gvReport.Columns.Count - 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    gvReport.Columns("" & strItemCode & "").IsVisible = True
                    gvReport.Columns("" & strItemCode & "").Width = 80
                    gvReport.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
                    'gvReport.Columns("" & strItemCode & "").BestFit()
                Next


                'gvReport.GroupDescriptors.Add(New GridGroupByExpression("HierDesc as HierDesc format ""{0}: {1}"" Group By HierDesc"))
                'gvReport.MasterTemplate.ExpandAllGroups()
                'gvReport.ShowGroupPanel = True
                'gvReport.MasterTemplate.AutoExpandGroups = True

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0

                Dim item1 As New GridViewSummaryItem("Sale", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("Trade Discount Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("Cash Discount Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                Dim item4 As New GridViewSummaryItem("Cash Discount Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item4)
                Dim item5 As New GridViewSummaryItem("Key Account and MT Quantity", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item5)
                Dim item6 As New GridViewSummaryItem("Key Account and MT Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item6)
                Dim item7 As New GridViewSummaryItem("Total Quantity", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item7)
                Dim item8 As New GridViewSummaryItem("Total Discount Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item8)
                Dim item9 As New GridViewSummaryItem("FOCQty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item9)

                For ii As Integer = 28 To gvReport.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If



        ElseIf rdbSampling.IsChecked = True Then

            If rdbSummary.IsChecked Then
                If rdbSku.IsChecked = True Then
                    gvReport.Columns("Item_Code").IsVisible = True
                    gvReport.Columns("Item_Code").Width = 50
                    gvReport.Columns("Item_Code").HeaderText = "Item Code"
                    'gvReport.Columns("Item Code").BestFit()
                Else
                    gvReport.Columns("Item_Code").IsVisible = True
                    gvReport.Columns("Item_Code").Width = 50
                    gvReport.Columns("Item_Code").HeaderText = "Pack Code"
                    'gvReport.Columns("Pack").BestFit()
                End If

                If rdbSku.IsChecked = True Then
                    gvReport.Columns("Sku_Seq").IsVisible = True
                    gvReport.Columns("Sku_Seq").Width = 50
                    gvReport.Columns("Sku_Seq").HeaderText = "Sku Seq No"
                    'gvReport.Columns("Item Code").BestFit()
                Else
                    gvReport.Columns("Pack_Seq").IsVisible = True
                    gvReport.Columns("Pack_Seq").Width = 50
                    gvReport.Columns("Pack_Seq").HeaderText = "Pack Seq No"
                End If


                gvReport.Columns("MRPBottle").IsVisible = True
                gvReport.Columns("MRPBottle").Width = 50
                gvReport.Columns("MRPBottle").HeaderText = "MRP Per Bottle"
                'gvReport.Columns("MRP Per Bottle").BestFit()

                gvReport.Columns("MRPCase").IsVisible = True
                gvReport.Columns("MRPCase").Width = 50
                gvReport.Columns("MRPCase").HeaderText = "MRP Per Case"
                'gvReport.Columns("MRP Per Case").BestFit()

                gvReport.Columns("TP").IsVisible = True
                gvReport.Columns("TP").Width = 100
                gvReport.Columns("TP").HeaderText = "Trade Price"
                'gvReport.Columns("Trade Price").BestFit()


                gvReport.Columns("DP").IsVisible = True
                gvReport.Columns("DP").Width = 100
                gvReport.Columns("DP").HeaderText = "Distributor Price"

                gvReport.Columns("Total Qty").IsVisible = True
                gvReport.Columns("Total Qty").Width = 50
                gvReport.Columns("Total Qty").HeaderText = "Total Quantity"
                'gvReport.Columns("Total Quantity").BestFit()

                gvReport.Columns("Total Amount").IsVisible = True
                gvReport.Columns("Total Amount").Width = 80
                gvReport.Columns("Total Amount").HeaderText = "Total Amount"

                gvReport.Columns("Amountcase").IsVisible = True
                gvReport.Columns("Amountcase").Width = 80
                gvReport.Columns("Amountcase").HeaderText = "Dicount Amount Per Case"
                'gvReport.Columns("Amount Per Case").BestFit()




                For ii As Integer = 8 To gvReport.Columns.Count - 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    gvReport.Columns("" & strItemCode & "").IsVisible = True
                    gvReport.Columns("" & strItemCode & "").Width = 80
                    gvReport.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
                    'gvReport.Columns("" & strItemCode & "").BestFit()
                Next


                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0

               
                Dim item5 As New GridViewSummaryItem("Total Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item5)
                Dim item6 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item6)

                For ii As Integer = 8 To gvReport.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Else

                gvReport.Columns("Sale_Invoice_No").IsVisible = True
                gvReport.Columns("Sale_Invoice_No").Width = 100
                gvReport.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"
                'gvReport.Columns("Sale Invoice No").BestFit()

                gvReport.Columns("Location_Code").IsVisible = True
                gvReport.Columns("Location_Code").Width = 50
                gvReport.Columns("Location_Code").HeaderText = "Location"

                gvReport.Columns("Location_Desc").IsVisible = True
                gvReport.Columns("Location_Desc").Width = 50
                gvReport.Columns("Location_Desc").HeaderText = "Location Desc"
                'gvReport.Columns("Location").BestFit()

                gvReport.Columns("Cust_Code").IsVisible = True
                gvReport.Columns("Cust_Code").Width = 50
                gvReport.Columns("Cust_Code").HeaderText = "Cust Code"

                gvReport.Columns("Customer_Name").IsVisible = True
                gvReport.Columns("Customer_Name").Width = 50
                gvReport.Columns("Customer_Name").HeaderText = "Cust Name"
                'gvReport.Columns("Cust Code").BestFit()

                gvReport.Columns("Cust_Group_Code").IsVisible = True
                gvReport.Columns("Cust_Group_Code").Width = 50
                gvReport.Columns("Cust_Group_Code").HeaderText = "Cust Group Code"
                'gvReport.Columns("Cust Group Code").BestFit()

                gvReport.Columns("Cust_Group_Desc").IsVisible = True
                gvReport.Columns("Cust_Group_Desc").Width = 50
                gvReport.Columns("Cust_Group_Desc").HeaderText = "Cust Group Desc"
                'gvReport.Columns("Cust Group Desc").BestFit()

                gvReport.Columns("Cust_Type_Code").IsVisible = True
                gvReport.Columns("Cust_Type_Code").Width = 20
                gvReport.Columns("Cust_Type_Code").HeaderText = "Cust Type Code"
                'gvReport.Columns("Cust Type Code").BestFit()


                gvReport.Columns("Cust_Type_Desc").IsVisible = True
                gvReport.Columns("Cust_Type_Desc").Width = 50
                gvReport.Columns("Cust_Type_Desc").HeaderText = "Cust Type Desc"
                'gvReport.Columns("Cust Type Desc").BestFit()

                gvReport.Columns("Salesman_Code").IsVisible = True
                gvReport.Columns("Salesman_Code").Width = 50
                gvReport.Columns("Salesman_Code").HeaderText = "Salesman Code"
                'gvReport.Columns("Salesman Code").BestFit()

                gvReport.Columns("Salesman_Desc").IsVisible = True
                gvReport.Columns("Salesman_Desc").Width = 50
                gvReport.Columns("Salesman_Desc").HeaderText = "Salesman Desc"
                'gvReport.Columns("Salesman Desc").BestFit()

                gvReport.Columns("Route_No").IsVisible = True
                gvReport.Columns("Route_No").Width = 50
                gvReport.Columns("Route_No").HeaderText = "Route No"
                'gvReport.Columns("Route No").BestFit()


                gvReport.Columns("Route_Desc").IsVisible = True
                gvReport.Columns("Route_Desc").Width = 50
                gvReport.Columns("Route_Desc").HeaderText = "Route Desc"
                'gvReport.Columns("Route Desc").BestFit()

                gvReport.Columns("Item_Code").IsVisible = True
                gvReport.Columns("Item_Code").Width = 50
                gvReport.Columns("Item_Code").HeaderText = "Item Code"
                'gvReport.Columns("Item Code").BestFit()

                'gvReport.Columns("MRP Per Bottle").IsVisible = True
                'gvReport.Columns("MRP Per Bottle").Width = 50
                'gvReport.Columns("MRP Per Bottle").HeaderText = "MRP Per Bottle"
                ''gvReport.Columns("MRP Per Bottle").BestFit()

                gvReport.Columns("MRPCase").IsVisible = True
                gvReport.Columns("MRPCase").Width = 50
                gvReport.Columns("MRPCase").HeaderText = "MRP Per Case"
                'gvReport.Columns("MRP Per Case").BestFit()

                gvReport.Columns("TP").IsVisible = True
                gvReport.Columns("TP").Width = 50
                gvReport.Columns("TP").HeaderText = "Trade Price"


                gvReport.Columns("DP").IsVisible = True
                gvReport.Columns("DP").Width = 100
                gvReport.Columns("DP").HeaderText = "Distributor Price"
                'gvReport.Columns("Trade Price").BestFit()

                gvReport.Columns("Total Qty").IsVisible = True
                gvReport.Columns("Total Qty").Width = 50
                gvReport.Columns("Total Qty").HeaderText = "Total Quantity"
                'gvReport.Columns("Total Quantity").BestFit()

                gvReport.Columns("Total Amount").IsVisible = True
                gvReport.Columns("Total Amount").Width = 80
                gvReport.Columns("Total Amount").HeaderText = "Total Amount"
                'gvReport.Columns("Total Amount").BestFit()

                'gvReport.Columns("Amount Per Case").IsVisible = True
                'gvReport.Columns("Amount Per Case").Width = 80
                'gvReport.Columns("Amount Per Case").HeaderText = "Amount Per Case"
                'gvReport.Columns("Amount Per Case").BestFit()




                For ii As Integer = 20 To gvReport.Columns.Count - 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    gvReport.Columns("" & strItemCode & "").IsVisible = True
                    gvReport.Columns("" & strItemCode & "").Width = 80
                    gvReport.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
                    'gvReport.Columns("" & strItemCode & "").BestFit()
                Next


                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0

              
                Dim item5 As New GridViewSummaryItem("Total Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item5)
                Dim item6 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item6)

                For ii As Integer = 20 To gvReport.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If

        ElseIf rdbVSD.IsChecked = True Then
            If rdbSummary.IsChecked Then
                If rdbSku.IsChecked = True Then
                    gvReport.Columns("Item_Code").IsVisible = True
                    gvReport.Columns("Item_Code").Width = 50
                    gvReport.Columns("Item_Code").HeaderText = "Item Code"
                    'gvReport.Columns("Item Code").BestFit()
                Else
                    gvReport.Columns("Item_Code").IsVisible = True
                    gvReport.Columns("Item_Code").Width = 50
                    gvReport.Columns("Item_Code").HeaderText = "Pack Code"
                    'gvReport.Columns("Pack").BestFit()
                End If

                If rdbSku.IsChecked = True Then
                    gvReport.Columns("Sku_Seq").IsVisible = True
                    gvReport.Columns("Sku_Seq").Width = 50
                    gvReport.Columns("Sku_Seq").HeaderText = "Sku Seq No"
                    'gvReport.Columns("Item Code").BestFit()
                Else
                    gvReport.Columns("Pack_Seq").IsVisible = True
                    gvReport.Columns("Pack_Seq").Width = 50
                    gvReport.Columns("Pack_Seq").HeaderText = "Pack Seq No"
                End If


                gvReport.Columns("MRPBottle").IsVisible = True
                gvReport.Columns("MRPBottle").Width = 50
                gvReport.Columns("MRPBottle").HeaderText = "MRP Per Bottle"
                'gvReport.Columns("MRP Per Bottle").BestFit()

                gvReport.Columns("MRPCase").IsVisible = True
                gvReport.Columns("MRPCase").Width = 50
                gvReport.Columns("MRPCase").HeaderText = "MRP Per Case"
                'gvReport.Columns("MRP Per Case").BestFit()

                gvReport.Columns("TP").IsVisible = True
                gvReport.Columns("TP").Width = 100
                gvReport.Columns("TP").HeaderText = "Trade Price"
                'gvReport.Columns("Trade Price").BestFit()



                gvReport.Columns("DP").IsVisible = True
                gvReport.Columns("DP").Width = 100
                gvReport.Columns("DP").HeaderText = "Distributor Price"


                gvReport.Columns("Agency_Gross_Quantity").IsVisible = True
                gvReport.Columns("Agency_Gross_Quantity").Width = 50
                gvReport.Columns("Agency_Gross_Quantity").HeaderText = "Agency Gross Quantity"
                'gvReport.Columns("Agency Gross Quantity").BestFit()

                gvReport.Columns("Agency_Gross_Amount").IsVisible = True
                gvReport.Columns("Agency_Gross_Amount").Width = 70
                gvReport.Columns("Agency_Gross_Amount").HeaderText = "Agency Gross Amount"
                'gvReport.Columns("Agency Gross Amount").BestFit()

                gvReport.Columns("Distributor_Gross_Quantity").IsVisible = True
                gvReport.Columns("Distributor_Gross_Quantity").Width = 50
                gvReport.Columns("Distributor_Gross_Quantity").HeaderText = "Distributor Gross Quantity"
                'gvReport.Columns("Distributor Gross Quantity").BestFit()

                gvReport.Columns("Distributor_Gross_Amount").IsVisible = True
                gvReport.Columns("Distributor_Gross_Amount").Width = 50
                gvReport.Columns("Distributor_Gross_Amount").HeaderText = "Distributor Gross Amount"
                'gvReport.Columns("Distributor Gross Amount").BestFit()

                gvReport.Columns("Total Qty").IsVisible = True
                gvReport.Columns("Total Qty").Width = 50
                gvReport.Columns("Total Qty").HeaderText = "Total Quantity"
                'gvReport.Columns("Total Quantity").BestFit()

                gvReport.Columns("Total Amount").IsVisible = True
                gvReport.Columns("Total Amount").Width = 80
                gvReport.Columns("Total Amount").HeaderText = "Total Amount"

                gvReport.Columns("Amountcase").IsVisible = True
                gvReport.Columns("Amountcase").Width = 80
                gvReport.Columns("Amountcase").HeaderText = "Dicount Amount Per Case"
                'gvReport.Columns("Amount Per Case").BestFit()




                For ii As Integer = 12 To gvReport.Columns.Count - 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    gvReport.Columns("" & strItemCode & "").IsVisible = True
                    gvReport.Columns("" & strItemCode & "").Width = 80
                    gvReport.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
                    'gvReport.Columns("" & strItemCode & "").BestFit()
                Next


                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0

                Dim item1 As New GridViewSummaryItem("Agency_Gross_Quantity", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("Agency_Gross_Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("Distributor_Gross_Quantity", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                Dim item4 As New GridViewSummaryItem("Distributor_Gross_Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item4)
                Dim item5 As New GridViewSummaryItem("Total Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item5)
                Dim item6 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item6)

                For ii As Integer = 12 To gvReport.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Else

                gvReport.Columns("Sale_Invoice_No").IsVisible = True
                gvReport.Columns("Sale_Invoice_No").Width = 100
                gvReport.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"
                'gvReport.Columns("Sale Invoice No").BestFit()

                gvReport.Columns("Location_Code").IsVisible = True
                gvReport.Columns("Location_Code").Width = 50
                gvReport.Columns("Location_Code").HeaderText = "Location"

                gvReport.Columns("Location_Desc").IsVisible = True
                gvReport.Columns("Location_Desc").Width = 50
                gvReport.Columns("Location_Desc").HeaderText = "Location Desc"
                'gvReport.Columns("Location").BestFit()

                gvReport.Columns("Cust_Code").IsVisible = True
                gvReport.Columns("Cust_Code").Width = 50
                gvReport.Columns("Cust_Code").HeaderText = "Cust Code"

                gvReport.Columns("Customer_Name").IsVisible = True
                gvReport.Columns("Customer_Name").Width = 50
                gvReport.Columns("Customer_Name").HeaderText = "Cust Name"
                'gvReport.Columns("Cust Code").BestFit()

                gvReport.Columns("Cust_Group_Code").IsVisible = True
                gvReport.Columns("Cust_Group_Code").Width = 50
                gvReport.Columns("Cust_Group_Code").HeaderText = "Cust Group Code"
                'gvReport.Columns("Cust Group Code").BestFit()

                gvReport.Columns("Cust_Group_Desc").IsVisible = True
                gvReport.Columns("Cust_Group_Desc").Width = 50
                gvReport.Columns("Cust_Group_Desc").HeaderText = "Cust Group Desc"
                'gvReport.Columns("Cust Group Desc").BestFit()

                gvReport.Columns("Cust_Type_Code").IsVisible = True
                gvReport.Columns("Cust_Type_Code").Width = 20
                gvReport.Columns("Cust_Type_Code").HeaderText = "Cust Type Code"
                'gvReport.Columns("Cust Type Code").BestFit()


                gvReport.Columns("Cust_Type_Desc").IsVisible = True
                gvReport.Columns("Cust_Type_Desc").Width = 50
                gvReport.Columns("Cust_Type_Desc").HeaderText = "Cust Type Desc"
                'gvReport.Columns("Cust Type Desc").BestFit()

                gvReport.Columns("Salesman_Code").IsVisible = True
                gvReport.Columns("Salesman_Code").Width = 50
                gvReport.Columns("Salesman_Code").HeaderText = "Salesman Code"
                'gvReport.Columns("Salesman Code").BestFit()

                gvReport.Columns("Salesman_Desc").IsVisible = True
                gvReport.Columns("Salesman_Desc").Width = 50
                gvReport.Columns("Salesman_Desc").HeaderText = "Salesman Desc"
                'gvReport.Columns("Salesman Desc").BestFit()

                gvReport.Columns("Route_No").IsVisible = True
                gvReport.Columns("Route_No").Width = 50
                gvReport.Columns("Route_No").HeaderText = "Route No"
                'gvReport.Columns("Route No").BestFit()


                gvReport.Columns("Route_Desc").IsVisible = True
                gvReport.Columns("Route_Desc").Width = 50
                gvReport.Columns("Route_Desc").HeaderText = "Route Desc"
                'gvReport.Columns("Route Desc").BestFit()

                gvReport.Columns("Item_Code").IsVisible = True
                gvReport.Columns("Item_Code").Width = 50
                gvReport.Columns("Item_Code").HeaderText = "Item Code"
                'gvReport.Columns("Item Code").BestFit()

                'gvReport.Columns("MRP Per Bottle").IsVisible = True
                'gvReport.Columns("MRP Per Bottle").Width = 50
                'gvReport.Columns("MRP Per Bottle").HeaderText = "MRP Per Bottle"
                ''gvReport.Columns("MRP Per Bottle").BestFit()

                gvReport.Columns("MRPCase").IsVisible = True
                gvReport.Columns("MRPCase").Width = 50
                gvReport.Columns("MRPCase").HeaderText = "MRP Per Case"
                'gvReport.Columns("MRP Per Case").BestFit()

                gvReport.Columns("TP").IsVisible = True
                gvReport.Columns("TP").Width = 50
                gvReport.Columns("TP").HeaderText = "Trade Price"
                'gvReport.Columns("Trade Price").BestFit()



                gvReport.Columns("DP").IsVisible = True
                gvReport.Columns("DP").Width = 100
                gvReport.Columns("DP").HeaderText = "Distributor Price"

                gvReport.Columns("Agency_Gross_Quantity").IsVisible = True
                gvReport.Columns("Agency_Gross_Quantity").Width = 50
                gvReport.Columns("Agency_Gross_Quantity").HeaderText = "Agency Gross Quantity"
                'gvReport.Columns("Agency Gross Quantity").BestFit()

                gvReport.Columns("Agency_Gross_Amount").IsVisible = True
                gvReport.Columns("Agency_Gross_Amount").Width = 70
                gvReport.Columns("Agency_Gross_Amount").HeaderText = "Agency Gross Amount"
                'gvReport.Columns("Agency Gross Amount").BestFit()

                gvReport.Columns("Distributor_Gross_Quantity").IsVisible = True
                gvReport.Columns("Distributor_Gross_Quantity").Width = 50
                gvReport.Columns("Distributor_Gross_Quantity").HeaderText = "Distributor Gross Quantity"
                'gvReport.Columns("Distributor Gross Quantity").BestFit()

                gvReport.Columns("Distributor_Gross_Amount").IsVisible = True
                gvReport.Columns("Distributor_Gross_Amount").Width = 50
                gvReport.Columns("Distributor_Gross_Amount").HeaderText = "Distributor Gross Amount"
                'gvReport.Columns("Distributor Gross Amount").BestFit()

                gvReport.Columns("Total Qty").IsVisible = True
                gvReport.Columns("Total Qty").Width = 50
                gvReport.Columns("Total Qty").HeaderText = "Total Quantity"
                'gvReport.Columns("Total Quantity").BestFit()

                gvReport.Columns("Total Amount").IsVisible = True
                gvReport.Columns("Total Amount").Width = 80
                gvReport.Columns("Total Amount").HeaderText = "Total Amount"
                'gvReport.Columns("Total Amount").BestFit()

                'gvReport.Columns("Amount Per Case").IsVisible = True
                'gvReport.Columns("Amount Per Case").Width = 80
                'gvReport.Columns("Amount Per Case").HeaderText = "Amount Per Case"
                'gvReport.Columns("Amount Per Case").BestFit()




                For ii As Integer = 24 To gvReport.Columns.Count - 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    gvReport.Columns("" & strItemCode & "").IsVisible = True
                    gvReport.Columns("" & strItemCode & "").Width = 80
                    gvReport.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
                    'gvReport.Columns("" & strItemCode & "").BestFit()
                Next


                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0

                Dim item1 As New GridViewSummaryItem("Agency_Gross_Quantity", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("Agency_Gross_Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("Distributor_Gross_Quantity", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                Dim item4 As New GridViewSummaryItem("Distributor_Gross_Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item4)
                Dim item5 As New GridViewSummaryItem("Total Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item5)
                Dim item6 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item6)

                For ii As Integer = 24 To gvReport.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If

        ElseIf rdbOther.IsChecked = True Then

            If rdbSummary.IsChecked Then
                If rdbSku.IsChecked = True Then
                    gvReport.Columns("Item_Code").IsVisible = True
                    gvReport.Columns("Item_Code").Width = 50
                    gvReport.Columns("Item_Code").HeaderText = "Item Code"
                    'gvReport.Columns("Item Code").BestFit()
                Else
                    gvReport.Columns("Item_Code").IsVisible = True
                    gvReport.Columns("Item_Code").Width = 50
                    gvReport.Columns("Item_Code").HeaderText = "Pack Code"
                    'gvReport.Columns("Pack").BestFit()
                End If

                If rdbSku.IsChecked = True Then
                    gvReport.Columns("Sku_Seq").IsVisible = True
                    gvReport.Columns("Sku_Seq").Width = 50
                    gvReport.Columns("Sku_Seq").HeaderText = "Sku Seq No"
                    'gvReport.Columns("Item Code").BestFit()
                Else
                    gvReport.Columns("Pack_Seq").IsVisible = True
                    gvReport.Columns("Pack_Seq").Width = 50
                    gvReport.Columns("Pack_Seq").HeaderText = "Pack Seq No"
                End If


                gvReport.Columns("MRPBottle").IsVisible = True
                gvReport.Columns("MRPBottle").Width = 50
                gvReport.Columns("MRPBottle").HeaderText = "MRP Per Bottle"
                'gvReport.Columns("MRP Per Bottle").BestFit()

                gvReport.Columns("MRPCase").IsVisible = True
                gvReport.Columns("MRPCase").Width = 50
                gvReport.Columns("MRPCase").HeaderText = "MRP Per Case"
                'gvReport.Columns("MRP Per Case").BestFit()

                gvReport.Columns("TP").IsVisible = True
                gvReport.Columns("TP").Width = 100
                gvReport.Columns("TP").HeaderText = "Trade Price"
                'gvReport.Columns("Trade Price").BestFit()


                gvReport.Columns("DP").IsVisible = True
                gvReport.Columns("DP").Width = 100
                gvReport.Columns("DP").HeaderText = "Distributor Price"

                gvReport.Columns("Total Qty").IsVisible = True
                gvReport.Columns("Total Qty").Width = 50
                gvReport.Columns("Total Qty").HeaderText = "Total Quantity"
                'gvReport.Columns("Total Quantity").BestFit()

                gvReport.Columns("Total Amount").IsVisible = True
                gvReport.Columns("Total Amount").Width = 80
                gvReport.Columns("Total Amount").HeaderText = "Total Amount"

                gvReport.Columns("Amountcase").IsVisible = True
                gvReport.Columns("Amountcase").Width = 80
                gvReport.Columns("Amountcase").HeaderText = "Dicount Amount Per Case"
                'gvReport.Columns("Amount Per Case").BestFit()




                For ii As Integer = 8 To gvReport.Columns.Count - 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    gvReport.Columns("" & strItemCode & "").IsVisible = True
                    gvReport.Columns("" & strItemCode & "").Width = 80
                    gvReport.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
                    'gvReport.Columns("" & strItemCode & "").BestFit()
                Next


                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0


                Dim item5 As New GridViewSummaryItem("Total Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item5)
                Dim item6 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item6)

                For ii As Integer = 8 To gvReport.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Else

                gvReport.Columns("Sale_Invoice_No").IsVisible = True
                gvReport.Columns("Sale_Invoice_No").Width = 100
                gvReport.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"
                'gvReport.Columns("Sale Invoice No").BestFit()

                gvReport.Columns("Location_Code").IsVisible = True
                gvReport.Columns("Location_Code").Width = 50
                gvReport.Columns("Location_Code").HeaderText = "Location"

                gvReport.Columns("Location_Desc").IsVisible = True
                gvReport.Columns("Location_Desc").Width = 50
                gvReport.Columns("Location_Desc").HeaderText = "Location Desc"
                'gvReport.Columns("Location").BestFit()

                gvReport.Columns("Cust_Code").IsVisible = True
                gvReport.Columns("Cust_Code").Width = 50
                gvReport.Columns("Cust_Code").HeaderText = "Cust Code"

                gvReport.Columns("Customer_Name").IsVisible = True
                gvReport.Columns("Customer_Name").Width = 50
                gvReport.Columns("Customer_Name").HeaderText = "Cust Name"
                'gvReport.Columns("Cust Code").BestFit()

                gvReport.Columns("Cust_Group_Code").IsVisible = True
                gvReport.Columns("Cust_Group_Code").Width = 50
                gvReport.Columns("Cust_Group_Code").HeaderText = "Cust Group Code"
                'gvReport.Columns("Cust Group Code").BestFit()

                gvReport.Columns("Cust_Group_Desc").IsVisible = True
                gvReport.Columns("Cust_Group_Desc").Width = 50
                gvReport.Columns("Cust_Group_Desc").HeaderText = "Cust Group Desc"
                'gvReport.Columns("Cust Group Desc").BestFit()

                gvReport.Columns("Cust_Type_Code").IsVisible = True
                gvReport.Columns("Cust_Type_Code").Width = 20
                gvReport.Columns("Cust_Type_Code").HeaderText = "Cust Type Code"
                'gvReport.Columns("Cust Type Code").BestFit()


                gvReport.Columns("Cust_Type_Desc").IsVisible = True
                gvReport.Columns("Cust_Type_Desc").Width = 50
                gvReport.Columns("Cust_Type_Desc").HeaderText = "Cust Type Desc"
                'gvReport.Columns("Cust Type Desc").BestFit()

                gvReport.Columns("Salesman_Code").IsVisible = True
                gvReport.Columns("Salesman_Code").Width = 50
                gvReport.Columns("Salesman_Code").HeaderText = "Salesman Code"
                'gvReport.Columns("Salesman Code").BestFit()

                gvReport.Columns("Salesman_Desc").IsVisible = True
                gvReport.Columns("Salesman_Desc").Width = 50
                gvReport.Columns("Salesman_Desc").HeaderText = "Salesman Desc"
                'gvReport.Columns("Salesman Desc").BestFit()

                gvReport.Columns("Route_No").IsVisible = True
                gvReport.Columns("Route_No").Width = 50
                gvReport.Columns("Route_No").HeaderText = "Route No"
                'gvReport.Columns("Route No").BestFit()


                gvReport.Columns("Route_Desc").IsVisible = True
                gvReport.Columns("Route_Desc").Width = 50
                gvReport.Columns("Route_Desc").HeaderText = "Route Desc"
                'gvReport.Columns("Route Desc").BestFit()

                gvReport.Columns("Item_Code").IsVisible = True
                gvReport.Columns("Item_Code").Width = 50
                gvReport.Columns("Item_Code").HeaderText = "Item Code"
                'gvReport.Columns("Item Code").BestFit()

                'gvReport.Columns("MRP Per Bottle").IsVisible = True
                'gvReport.Columns("MRP Per Bottle").Width = 50
                'gvReport.Columns("MRP Per Bottle").HeaderText = "MRP Per Bottle"
                ''gvReport.Columns("MRP Per Bottle").BestFit()

                gvReport.Columns("MRPCase").IsVisible = True
                gvReport.Columns("MRPCase").Width = 50
                gvReport.Columns("MRPCase").HeaderText = "MRP Per Case"
                'gvReport.Columns("MRP Per Case").BestFit()

                gvReport.Columns("TP").IsVisible = True
                gvReport.Columns("TP").Width = 50
                gvReport.Columns("TP").HeaderText = "Trade Price"


                gvReport.Columns("DP").IsVisible = True
                gvReport.Columns("DP").Width = 100
                gvReport.Columns("DP").HeaderText = "Distributor Price"

                'gvReport.Columns("Trade Price").BestFit()

                gvReport.Columns("Total Qty").IsVisible = True
                gvReport.Columns("Total Qty").Width = 50
                gvReport.Columns("Total Qty").HeaderText = "Total Quantity"
                'gvReport.Columns("Total Quantity").BestFit()

                gvReport.Columns("Total Amount").IsVisible = True
                gvReport.Columns("Total Amount").Width = 80
                gvReport.Columns("Total Amount").HeaderText = "Total Amount"
                'gvReport.Columns("Total Amount").BestFit()

                'gvReport.Columns("Amount Per Case").IsVisible = True
                'gvReport.Columns("Amount Per Case").Width = 80
                'gvReport.Columns("Amount Per Case").HeaderText = "Amount Per Case"
                'gvReport.Columns("Amount Per Case").BestFit()




                For ii As Integer = 20 To gvReport.Columns.Count - 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    gvReport.Columns("" & strItemCode & "").IsVisible = True
                    gvReport.Columns("" & strItemCode & "").Width = 80
                    gvReport.Columns("" & strItemCode & "").HeaderText = "" & strItemCode & ""
                    'gvReport.Columns("" & strItemCode & "").BestFit()
                Next


                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim intCount As Integer = 0


                Dim item5 As New GridViewSummaryItem("Total Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item5)
                Dim item6 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item6)

                For ii As Integer = 20 To gvReport.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gvReport.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If

        End If

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub ExportToExcel()
        Try
            Dim strReportTitle As String = ""
            If rdbNonSampling.IsChecked Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "Discount D & A Summary wise"
                Else
                    strReportTitle = "Discount D & A Detail wise"
                End If
            ElseIf rdbSampling.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "Discount Sampling Summary wise"
                Else
                    strReportTitle = "Discount Sampling Detail wise"
                End If
            ElseIf rdbVSD.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "Discount VS and D Summary wise"
                Else
                    strReportTitle = "Discount VS and D Detail wise"
                End If
            ElseIf rdbOther.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "Discount Other Summary wise"
                Else
                    strReportTitle = "Discount Other Detail wise"
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
            'Dim i As Integer = 0
            'For i = 0 To gvReport.ColumnCount - 1
            '    Dim grow As GridViewRowInfo = TryCast(gvReport.Rows(0), GridViewRowInfo)
            '    If TypeOf grow.Cells(i).Value Is DateTime Then
            '        Dim datecol As GridViewDateTimeColumn = TryCast(gvReport.Columns(i), GridViewDateTimeColumn)
            '        datecol.ExcelExportType = DisplayFormatType.ShortDate
            '    End If
            'Next i
            Dim exporter As New ExportToExcelML(gvReport)
            exporter.SummariesExportOption = SummariesOption.ExportAll
            'If rdbSummary.IsChecked = True Then
            'exporter.ExportVisualSettings = True
            'End If
            exporter.ExportHierarchy = True
            exporter.HiddenColumnOption = HiddenOption.DoNotExport
            exporter.SheetMaxRows = ExcelMaxRows._1048576
            'AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
            AddHandler exporter.ExcelTableCreated, AddressOf exporter_ExcelTableCreated
            exporter.SheetName = strReportTitle
            exporter.RunExport(Fullpath)
            Me.Controls.Remove(gvReport)
            Dim xlsApp As Microsoft.Office.Interop.Excel.Application
            Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
            xlsApp = New Microsoft.Office.Interop.Excel.Application
            xlsApp.Visible = True
            xlsWB = xlsApp.Workbooks.Open(Fullpath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
        ''If saveDialog1.ShowDialog(Me) = System.System.Windows.Forms.DialogResult.OK Then
        ''    Dim thread2 As New Thread(New ParameterizedThreadStart(AddressOf RunExportToExcelML))
        ''    thread2.Start(saveDialog1.FileName)
        ''End If
    End Sub

    Private Sub RunExportToExcelML(ByVal fileName As Object)
        Try
            'Dim exporter As New ExportToExcelML(gvReport)
            'exporter.SummariesExportOption = SummariesOption.ExportAll
            ''If rdbSummary.IsChecked = True Then
            ''    exporter.ExportVisualSetting = True
            ''End If
            'exporter.ExportHierarchy = True
            'exporter.HiddenColumnOption = HiddenOption.DoNotExport
            'exporter.SheetMaxRows = ExcelMaxRows._1048576
            'AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
            'AddHandler exporter.ExcelTableCreated, AddressOf exporter_ExcelTableCreated
            'exporter.RunExport(fileName.ToString())
            'Dim text As String = "Export finished successfully!"
            'Dim xlApp As Excel.Application
            'xlApp = New Excel.ApplicationClass
            'Process.Start(fileName.ToString())

            'common.clsCommon.MyMessageBoxShow(text)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    '''' <summary>        
    '''' '''' using ExcelCellFormatting event for updating progress bar and applying custom format in excel file        
    '''' '''' </summary>        

    'Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelCellFormattingEventArgs)
    '    If e.GridRowInfoType Is GetType(GridViewDataRowInfo) Then
    '    End If
    'End Sub

    '''' <summary>        
    '''' '''' using ExcelTableCreated event for adding custom header row        
    '''' '''' </summary>        

    Private Sub exporter_ExcelTableCreated(ByVal sender As Object, ByVal e As ExcelTableCreatedEventArgs)
        Dim strReportTitle, strType As String

        strType = ""
        strReportTitle = ""
        If rdbDirect.IsChecked = True Then
            strType = "Direct"
        ElseIf rdbIndirect.IsChecked Then
            strType = "InDirect"
        ElseIf rdbBoth.IsChecked Then
            strType = "Both"
        End If
        If rdbNonSampling.IsChecked Then
            If rdbSku.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "D & A Summary SKU wise "
                Else
                    strReportTitle = "D & A Detail SKU wise "
                End If
            ElseIf rdbPack.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "D & A Summary Pack wise "
                Else
                    strReportTitle = "D & A Detail Pack wise "
                End If
            ElseIf rdbFlavour.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "D & A Summary Flavour wise "
                Else
                    strReportTitle = "D & A Detail Flavour wise "
                End If
            End If
        ElseIf rdbSampling.IsChecked = True Then
            If rdbSku.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "Sampling Summary SKU wise"
                Else
                    strReportTitle = "Sampling Detail SKU wise"
                End If
            ElseIf rdbPack.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "Sampling Summary Pack wise"
                Else
                    strReportTitle = "Sampling Detail Pack  wise"
                End If
            ElseIf rdbFlavour.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "Sampling Summary Flavour wise"
                Else
                    strReportTitle = "Sampling Detail Flavour wise"
                End If
            End If
        ElseIf rdbVSD.IsChecked = True Then
            If rdbSku.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "VS and D Summary SKU wise"
                Else
                    strReportTitle = "VS and D Detail SKU wise"
                End If
            ElseIf rdbPack.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "VS and D Summary Pack wise"
                Else
                    strReportTitle = "VS and D Detail Pack wise"
                End If
            ElseIf rdbFlavour.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "VS and D Summary Flavour wise"
                Else
                    strReportTitle = "VS and D Detail Flavour wise"
                End If
            End If
        ElseIf rdbOther.IsChecked = True Then
            If rdbSku.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "Other Summary SKU wise"
                Else
                    strReportTitle = "Other Detail SKU wise"
                End If
            ElseIf rdbPack.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "Other Summary Pack wise"
                Else
                    strReportTitle = "Other Detail Pack wise"
                End If
            ElseIf rdbFlavour.IsChecked = True Then
                If rdbSummary.IsChecked = True Then
                    strReportTitle = "Other Summary Flavour wise"
                Else
                    strReportTitle = "Other Detail Flavour wise"
                End If
            End If
        End If
        If e.SheetIndex = 0 Then 'add header row only for the first excel sheet                

            Dim style1 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Item Discount Report : " + strReportTitle)
            Dim style3 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Start Date : = " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy"))
            Dim style4 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "End Date : = " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            Dim style5 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Report Type : = " + strType)

            Dim strLoca As String = ""
            For Each Str As String In cbgLocation.CheckedDisplayMember
                If clsCommon.myLen(strLoca) > 0 Then
                    strLoca += ", "
                End If
                strLoca += Str
            Next
            Dim style6 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Location : " + strLoca)


        End If
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Export()
    End Sub
    Sub Export()
        If gvReport.Rows.Count > 0 Then
            ExportToExcel()
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        reset()
        gvReport.DataSource = Nothing
        gvReport.Columns.Clear()
        gvReport.Rows.Clear()
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub

    Private Sub RadPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPanel1.Paint

    End Sub

    Private Sub rdbDetail_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbDetail.ToggleStateChanged
        rdbPack.Visible = False
        rdbSku.IsChecked = True
    End Sub

    Private Sub rdbSummary_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbSummary.ToggleStateChanged
        rdbPack.Visible = True
    End Sub

   

    '''' <summary>
    '''' '  Customer class <> F and <> S
    '''' </summary>
    '''' <remarks> done </remarks >
    '''' 
    Private Sub CreateTableNonSampligDetail()
        Try


            If rdbDirect.IsChecked = True Then
                strType = "Shipment_Type='Transfer'"
            ElseIf rdbIndirect.IsChecked = True Then
                strType = "Shipment_Type='Sale'"
            ElseIf rdbBoth.IsChecked = True Then
                strType = "(Shipment_Type='Sale' or Shipment_Type='Transfer')"
            End If

            If rdbAll.IsChecked = True Then
                strPost = ""
                strReturnPost = ""
                strInterPost = ""
            Else
                strPost = " and Is_Post='Y' "
                strReturnPost = " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Is_Post='Y'"
                strInterPost = " and Is_Post=1 "
            End If

            If rdbSku.IsChecked Then
                strSeq = "Sku_Seq"
                strGrp = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code"
                strOrder = ""
                strRetGrp = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code"
                strInterGrp = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code"
            Else
                strSeq = "Pack_Seq"
                strGrp = "(Class_Desc ) as Item_Code"
                strRetGrp = "(Class_Desc ) as Item_Code"
                strInterGrp = "(Class_Desc ) as Item_Code"
                strOrder = "Order by Pack_Seq asc"
            End If

            'If rdbSummary.IsChecked Then
            '    strSeq = strSeq
            'Else
            '    strSeq = ""
            'End If

            If chkLocationAll.IsChecked = True Then
                strLocation = "Y"
            Else
                strLocation = "N"
            End If
            If chkCustAll.IsChecked = True Then
                strCustAll = "Y"
            Else
                strCustAll = "N"
            End If

            Dim dt As New DataTable
            Dim strInterComp, strQry, strQry1, strMainQry, strPivot, Desc, DescAmount, strsum, strPivotSummary, strPivotqry1, strPivotQry2, strPivotqry3, strPivotSum As String
            Dim strDiscCodeSummary, strDiscAmtSummary, strDiscCode, strDiscCodestring, strDiscAmtString, strMainDiscCodeString, strMainDiscAmtString As String
            strDiscCodestring = ""
            strMainDiscCodeString = ""
            strDiscAmtSummary = ""
            strDiscCodeSummary = ""
            strPivotSummary = ""
            strPivotqry3 = ""
            strsum = ""
            strDiscCode = ""
            strPivotSum = ""
            strPivotqry1 = ""
            strPivotQry2 = ""
            Desc = ""
            strDiscAmtString = ""
            strMainDiscAmtString = ""
            strQry = "select distinct  TSPL_Discount_Master.Description as Descr from " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master RIGHT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Discount_Code ON  " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
                  "where convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
                  "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103) and (Discount_Code <> '' or Discount_Code=null) and Discount='Y' and " & strType & " and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') " & strPost & " "

            If strLocation = "N" Then
                strQry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQry, ArrDBName, False)
            strQuery = "select distinct * from (" + strQuery + ")abc"
            dt = clsDBFuncationality.GetDataTable(strQuery)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows

                    Desc = clsCommon.myCstr(dr("Descr"))
                    'Desc = Replace(Desc, " ", "_")
                    'Desc = Replace(Desc, "&", "and")

                    DescAmount = Desc + " Amount"

                    strDiscCodestring = strDiscCodestring & "[" & Desc & "]" & ","
                    strDiscCode = strDiscCode & "[" & Desc & "]" & ","
                    strMainDiscCodeString = strMainDiscCodeString & "  isnull(" & "Sum(" & "[" & Desc & "]" & " ) " & ",0)  " & "as  " & "[" & Desc & "]" & ","

                    strDiscAmtString = strDiscAmtString & "[" & DescAmount & "]" & ","
                    strMainDiscAmtString = strMainDiscAmtString & "  isnull(" & "Sum(" & "[" & DescAmount & "]" & " ) " & ",0)  " & "as  " & "[" & DescAmount & "]" & ","

                    strsum = strsum & "[" & DescAmount & "]" & "+"


                    strDiscCodeSummary = strDiscCodeSummary & " Sum(" & "[" & Desc & "]" & " ) " & "as  " & "[" & Desc & "]" & ","
                    strDiscAmtSummary = strDiscAmtSummary & " Sum(" & "[" & DescAmount & "]" & " ) " & "as  " & "[" & DescAmount & "]" & ","

                Next
            End If
            If Desc <> "" Then
                strDiscCodestring = strDiscCodestring.Substring(0, strDiscCodestring.Length - 1)
                strDiscAmtString = strDiscAmtString.Substring(0, strDiscAmtString.Length - 1)
                strMainDiscAmtString = strMainDiscAmtString.Substring(0, strMainDiscAmtString.Length - 1)
                strsum = strsum.Substring(0, strsum.Length - 1)
                strPivotqry1 = ", " & strDiscCode & " " & strDiscAmtString & ""
                strPivotQry2 = ", " & strMainDiscCodeString & " " & strMainDiscAmtString & ""
                strPivotqry3 = " pivot (sum(DiscQty) for DiscCode in ( " & strDiscCodestring & " )) as pvt1  " & _
                               "pivot (sum(DiscAmt) for DiscCodeAmt in ( " & strDiscAmtString & " )) as Pvt2 "
                strPivotSum = " + " & strsum & ""


                strDiscAmtSummary = strDiscAmtSummary.Substring(0, strDiscAmtSummary.Length - 1)
                strPivotSummary = ", " & strDiscCodeSummary & " " & strDiscAmtSummary & ""
            End If



            strMainQry = "select (Sale_Invoice_No),(Item_Code)," & strSeq & ",(MRPCase),MRPBottle,(TP),DP,FOCQty as BottScheme, " & _
            "Location_Code,Location_Desc,Route_No,Route_Desc,Cust_Code, " & _
            "Customer_Name, Cust_Type_Code, Cust_Type_Desc, Cust_Group_Code, Cust_Group_Desc,Salesman_Code,Salesman_Desc,   " & _
            "Invoice_Qty as Sale,FOCQty,FOCAMt as [Trade Discount Amount], " & _
            "Cust_DiscQty as [Cash Discount Qty],Cust_DiscAmt as [Cash Discount Amount], " & _
            "AcctQty as [Key Account and MT Quantity],Acctamt as [Key Account and MT Amount], " & _
            "TotQty as [Total Quantity], " & _
            "FOCAMt + Cust_DiscAmt + Acctamt  " & strPivotSum & " as [Total Discount Amount] " & _
            " " & strPivotqry1 & " " & _
            "from ( " & _
            "select   Sale_Invoice_No, item_code, " & strSeq & ", MRPCase,MRPCase/MrpBottleConvRate as MRPBottle,TP/MrpBottleConvRate as BottScheme,TP,DP,Location_Code,Location_Desc,Route_No,Route_Desc, " & _
            "Cust_Code,Customer_Name,Cust_Type_Code,Cust_Type_Desc,Cust_Group_Code,Cust_Group_Desc,Salesman_Code,Salesman_Desc,   " & _
            "convert(decimal (18,2),sum(Invoice_Qty)) as Invoice_Qty, " & _
            "convert(decimal (18,2),SUM(FOCQty)) as FOCQty, " & _
            "SUM(FOCAMt) as FOCAMt, " & _
            "convert(decimal (18,2),SUM(Cust_DiscQty)) as Cust_DiscQty, " & _
            "convert(decimal (18,2),SUM(Cust_DiscAmt )) as Cust_DiscAmt, " & _
            "convert(decimal (18,2),SUM(AcctQty)) as AcctQty, " & _
            "convert(decimal (18,2),SUM(Acctamt)) as Acctamt, " & _
            "SUM(TotQty) as  TotQty " & _
            " " & strPivotQry2 & " " & _
            " from " & _
             " (  "
            strQry1 = "SELECT  " & strSeq & ",TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MrpBottleConvRate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as CaseConF, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, Cust_Type_Desc,Cust_Group_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id, " & strGrp & ", " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code,Salesman_Desc  , " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRPCase, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 AS TP, " & _
                    "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9) AS DP, " & _
            "CASE WHEN (Price_Amount2 = 0 AND Price_Amount3 = 0) THEN (Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END AS Invoice_Qty, " & _
                        "CASE WHEN Scheme_Item = 'Y' AND (Discount_Code = '' OR Discount_Code = NULL) THEN (Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END AS FOCQty, " & _
            "CASE WHEN Scheme_Item = 'Y' AND (Discount_Code = '' OR Discount_Code = NULL) THEN ((Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END AS FOCAMt, " & _
            "CASE WHEN Cust_Discount <> 0 AND (Scheme_Item <> 'Y' AND Promo_Scheme_Item <> 'Y' AND Sampling_Item <> 'y') AND (Discount_Code = '' OR Discount_Code = NULL) THEN (Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END AS Cust_DiscQty, " & _
            "CASE WHEN Cust_Discount <> 0 AND (Scheme_Item <> 'Y' AND Promo_Scheme_Item <> 'Y' AND Sampling_Item <> 'y') AND (Discount_Code = '' OR Discount_Code = NULL) THEN (Invoice_Qty * Cust_Discount) ELSE 0 END AS Cust_DiscAmt, " & _
            "CASE WHEN (Price_Amount2 <> 0 OR Price_Amount3 <> 0) THEN (Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END AS AcctQty, " & _
            "CASE WHEN (Price_Amount2 <> 0 OR Price_Amount3 <> 0) THEN ((isnull(Price_Amount2, 0) + isnull(Price_Amount3, 0)) * Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END AS Acctamt, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS TotQty, " & _
            "CASE WHEN Discount_Code <> '' AND Discount = 'Y' THEN TSPL_Discount_Master.Description ELSE '' END AS DiscCode, " & _
            "CASE WHEN Discount_Code <> '' AND Discount = 'Y' THEN (TSPL_Discount_Master.Description) + ' Amount' ELSE '' END AS DiscCodeAmt, " & _
            "CASE WHEN Discount_Code <> '' AND Discount = 'Y' THEN CONVERT(decimal(18, 2), Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END AS DiscQty, " & _
            "CASE WHEN Discount_Code <> '' AND Discount = 'Y' THEN CONVERT(decimal(18, 2), (Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END AS DiscAmt " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Discount_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
            "left outer join  TSPL_ITEM_DETAILS on TSPL_SALE_INVOICE_DETAIL.item_code= TSPL_ITEM_DETAILS.item_code left outer join TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
            "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " & _
            "TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "WHERE  convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103) AND " & _
            " " & strType & " AND   Class_Name='size' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB'   " & strPost & "  "

            Dim strQry2 As String = "SELECT  " & strSeq & ",TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MrpBottleConvRate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as CaseConF, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No as Sale_Invoice_No, " & _
            "Cust_Type_Desc,Cust_Group_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_Id as Sale_Invoice_Id, " & _
            "" & strRetGrp & ", " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Salesman_Code, " & _
            "Salesman_Desc  , " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRPCase, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount1 AS TP, " & _
                                "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9) AS DP, " & _
            "-(CASE WHEN (Price_Amount2 = 0 AND Price_Amount3 = 0) THEN (Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Invoice_Qty, " & _
            "-(CASE WHEN Scheme_Item = 'Y' AND (Discount_Code = '' OR Discount_Code = NULL) THEN (Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  ELSE 0 END) AS FOCQty, " & _
            "-(CASE WHEN Scheme_Item = 'Y' AND (Discount_Code = '' OR Discount_Code = NULL) THEN ((Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS FOCAMt, " & _
            "-(CASE WHEN Cust_Discount <> 0 AND (Scheme_Item <> 'Y' AND Promo_Scheme_Item <> 'Y' AND Sampling_Item <> 'y') AND (Discount_Code = '' OR Discount_Code = NULL) THEN (Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Cust_DiscQty, " & _
            "-(CASE WHEN Cust_Discount <> 0 AND (Scheme_Item <> 'Y' AND Promo_Scheme_Item <> 'Y' AND Sampling_Item <> 'y') AND (Discount_Code = '' OR Discount_Code = NULL) THEN (Return_Qty * Cust_Discount) ELSE 0 END) AS Cust_DiscAmt, " & _
            "-(CASE WHEN (Price_Amount2 <> 0 OR Price_Amount3 <> 0) THEN (Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS AcctQty, " & _
            "-(CASE WHEN (Price_Amount2 <> 0 OR Price_Amount3 <> 0) THEN ((isnull(Price_Amount2, 0) + isnull(Price_Amount3, 0)) * Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Acctamt, " & _
            "-(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) AS TotQty, " & _
            "CASE WHEN Discount_Code <> '' AND Discount = 'Y' THEN TSPL_Discount_Master.Description ELSE '' END AS DiscCode, " & _
            "CASE WHEN Discount_Code <> '' AND Discount = 'Y' THEN (TSPL_Discount_Master.Description) + ' Amount' ELSE '' END AS DiscCodeAmt, " & _
            "-(CASE WHEN Discount_Code <> '' AND Discount = 'Y' THEN CONVERT(decimal(18, 2), Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS DiscQty, " & _
            "-(CASE WHEN Discount_Code <> '' AND Discount = 'Y' THEN CONVERT(decimal(18, 2), (Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS DiscAmt " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Discount_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code left outer join " & _
            "TSPL_ITEM_DETAILS on TSPL_SALE_RETURN_DETAIL.item_code= TSPL_ITEM_DETAILS.item_code left outer join " & _
            "TSPL_ITEM_MASTER on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on " & _
            "TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code WHERE " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date >= convert(date, '" & fromDate.Value & "',103)  AND  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date <= convert(date, '" & ToDate.Value & "',103)  AND  " & _
            "Class_Name='size' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and " & strType & " " & strReturnPost & "  "

            strInterComp = "SELECT  " & strSeq & ",TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MrpBottleConvRate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as CaseConF, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No as Sale_Invoice_No, " & _
            "Cust_Type_Desc,Cust_Group_Desc, 1 as Sale_Invoice_Id, " & _
            "" & strInterGrp & ", " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code, " & _
            "Salesman_Desc  , " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRPCase, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount1 AS TP, " & _
                                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9) AS DP, " & _
            "-(CASE WHEN (Price_Amount2 = 0 AND Price_Amount3 = 0) THEN (Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Invoice_Qty, " & _
            "0 AS FOCQTy, " & _
            "0 AS FOCAMt, " & _
            "0 AS Cust_DiscQty, " & _
            "0 AS Cust_DiscAmt, " & _
            "-(CASE WHEN (Price_Amount2 <> 0 OR Price_Amount3 <> 0) THEN (Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS AcctQty, " & _
            "-(CASE WHEN (Price_Amount2 <> 0 OR Price_Amount3 <> 0) THEN ((isnull(Price_Amount2, 0) + isnull(Price_Amount3, 0)) * Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Acctamt, " & _
            "-(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty / " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor) AS TotQty, " & _
            " '' AS DiscCode, " & _
            "'' AS DiscCodeAmt, " & _
            "0 AS DiscQty, " & _
            "0 AS DiscAmt FROM  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL  LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code left outer join " & _
            "TSPL_ITEM_DETAILS on TSPL_SALE_RETURN_INTER_DETAIL.item_code= TSPL_ITEM_DETAILS.item_code left outer join " & _
            "TSPL_ITEM_MASTER on TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on " & _
            "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code WHERE " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) <= convert(date, '" & ToDate.Value & "',103)   and  " & _
            "Class_Name='size' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' " & strInterPost & " "

            If strLocation = "N" Then
                strQry1 += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                strQry2 += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                strInterComp += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "

            End If

            If strCustAll = "N" Then
                strQry1 += " and TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                strQry2 += " and TSPL_SALE_RETURN_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                strInterComp += " and TSPL_SALE_RETURN_INTER_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "

            End If

            If rdbIterComp.Checked Then
                strQry = strQry1 & " Union All " & strQry2 & " Union All " & strInterComp
            Else
                strQry = strQry1 & " Union All " & strQry2
            End If


            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If

            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQry, ArrDBName, False)
            dt = clsDBFuncationality.GetDataTable(strQuery) '

            strPivot = " )a " & strPivotqry3 & "  " & _
            "group by Sale_Invoice_No,item_code,MRPCase,TP,DP,Location_Code,Location_Desc, " & _
            "Route_No, Route_Desc, Cust_Code, Customer_Name, Cust_Type_Code, Cust_Type_Desc, " & _
            "Cust_Group_Code, Cust_Group_Desc,Salesman_Code,Salesman_Desc," & strSeq & ",MrpBottleConvRate,CaseConF " & _
            "  ) b"

            '"case when (sum([Total Quantity]) =0 or avg(BottScheme)=0) then 0  " & _
            '"else sum([Total Discount Amount])/sum([Total Quantity])/avg(BottScheme) end as BottScheme, " & _

            If rdbSummary.IsChecked Then
                strQry = "select distinct (Item_Code)," & strSeq & ",(MRPCase),MRPBottle, " & _
            "(BottScheme)  as BottScheme, " & _
            "(TP),DP,sum(Sale) as Sale ," & _
            "sum([Trade Discount Amount]) as [Trade Discount Amount] , " & _
            "SUM([Cash Discount Qty]) as [Cash Discount Qty], " & _
            "sum([Cash Discount Amount]) as [Cash Discount Amount], " & _
            "SUM([Key Account and MT Quantity]) as [Key Account and MT Quantity], " & _
            "sum([Key Account and MT Amount]) as [Key Account and MT Amount] , " & _
            "sum([Total Quantity]) as [Total Quantity], " & _
            "sum([Total Discount Amount]) as [Total Discount Amount] , " & _
            "case when sum([Total Quantity]) <> 0 then sum([Total Discount Amount])/sum([Total Quantity]) else 0 end as Amountcase  " & strPivotSummary & "  " & _
            "from   ( " & strMainQry & strQuery & strPivot & " )C  group by (Item_Code),(MRPCase),(TP),DP," & strSeq & " ,MRPBottle,BottScheme " & strOrder & " "
            Else
                strQry = strMainQry & strQuery & strPivot
            End If


            'strQry = strQry

            dt = clsDBFuncationality.GetDataTable(strQry)
            gvReport.DataSource = Nothing
            gvReport.Columns.Clear()
            gvReport.Rows.Clear()
            gvReport.GroupDescriptors.Clear()
            gvReport.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                gvReport.DataSource = dt
                SetGridFormationOFgvReport()
            End If
















        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try



        'MsgBox("successfully")
    End Sub


    '''' <summary>
    ''''  Customer class <> F
    '''' </summary>
    '''' <remarks> pending </remarks>
    '''' 
    Private Sub CreateTableVSandDDetail()
        Try

            If rdbDirect.IsChecked = True Then
                strType = "Shipment_Type='Transfer'"
            ElseIf rdbIndirect.IsChecked = True Then
                strType = "Shipment_Type='Sale'"
            ElseIf rdbBoth.IsChecked = True Then
                strType = "(Shipment_Type='Sale' or Shipment_Type='Transfer')"
            End If

            If rdbAll.IsChecked = True Then
                strPost = ""
                strReturnPost = ""
                strInterPost = ""
            Else
                strPost = " and Is_Post='Y' "
                strReturnPost = " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Is_Post='Y'"
                strInterPost = " and Is_Post=1 "
            End If

            If rdbSku.IsChecked Then
                strSeq = "Sku_Seq"
                strGrp = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code"
                strOrder = ""
                strRetGrp = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code"
                strInterGrp = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code"
            Else
                strSeq = "Pack_Seq"
                strGrp = "(Class_Desc ) as Item_Code"
                strRetGrp = "(Class_Desc ) as Item_Code"
                strInterGrp = "(Class_Desc ) as Item_Code"
                strOrder = "Order by Pack_Seq asc"
            End If

            If chkLocationAll.IsChecked = True Then
                strLocation = "Y"
            Else
                strLocation = "N"
            End If
            If chkCustAll.IsChecked = True Then
                strCustAll = "Y"
            Else
                strCustAll = "N"
            End If

            Dim dt As New DataTable
            Dim strInterComp, strQry, strQry1, strQry2, strMainQry, strPivot, strPivotSummary, Desc, DescAmount, strDiscCodeSummary, strDiscAmtSummary, strsum, strsumQty, strPivotqry1, strPivotQry2, strPivotqry3, strPivotSum, strPivotSumQty As String
            Dim strDiscCode, strDiscCodestring, strDiscAmtString, strMainDiscCodeString, strMainDiscAmtString As String
            strDiscCodestring = ""
            strMainDiscCodeString = ""
            strDiscCode = ""
            strsumQty = ""
            strsum = ""
            strMainDiscAmtString = ""
            strDiscAmtString = ""
            Desc = ""
            strDiscAmtSummary = ""
            strDiscCodeSummary = ""
            strPivotSumQty = ""
            strPivotqry1 = ""
            strPivotQry2 = ""
            strPivotSummary = ""
            strPivotqry3 = ""
            strPivotSum = ""
            strQry = "select distinct  TSPL_Discount_Master.Description as Descr from " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master RIGHT OUTER JOIN " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Discount_Code ON  " & _
                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
                  "where convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
                  "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103) and (Discount_Code <> '' or Discount_Code=null) and VSND_Type='Y' and " & strType & " and (Cust_Type_Code <> 'F') " & strPost & " "

            If strLocation = "N" Then
                strQry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQry, ArrDBName, False)
            strQuery = "select distinct * from (" + strQuery + ")abc"
            dt = clsDBFuncationality.GetDataTable(strQuery)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows

                    Desc = clsCommon.myCstr(dr("Descr"))
                    'Desc = Replace(Desc, " ", "_")
                    'Desc = Replace(Desc, "&", "and")

                    DescAmount = Desc + " Amount"

                    strDiscCodestring = strDiscCodestring & "[" & Desc & "]" & ","
                    strDiscCode = strDiscCode & "[" & Desc & "]" & ","
                    strMainDiscCodeString = strMainDiscCodeString & "  isnull(" & "Sum(" & "[" & Desc & "]" & " ) " & ",0)  " & "as  " & "[" & Desc & "]" & ","

                    strDiscAmtString = strDiscAmtString & "[" & DescAmount & "]" & ","
                    strMainDiscAmtString = strMainDiscAmtString & "  isnull(" & "Sum(" & "[" & DescAmount & "]" & " ) " & ",0)  " & "as  " & "[" & DescAmount & "]" & ","

                    strsum = strsum & "[" & DescAmount & "]" & "+"
                    strsumQty = strsumQty & "[" & Desc & "]" & "+"


                    strDiscCodeSummary = strDiscCodeSummary & " Sum(" & "[" & Desc & "]" & " ) " & "as  " & "[" & Desc & "]" & ","
                    strDiscAmtSummary = strDiscAmtSummary & " Sum(" & "[" & DescAmount & "]" & " ) " & "as  " & "[" & DescAmount & "]" & ","
                Next
            End If
            If Desc <> "" Then
                strDiscCodestring = strDiscCodestring.Substring(0, strDiscCodestring.Length - 1)
                'strMainDiscCodeString = strMainDiscCodeString.Substring(0, strMainDiscCodeString.Length - 1)
                strDiscAmtString = strDiscAmtString.Substring(0, strDiscAmtString.Length - 1)
                strMainDiscAmtString = strMainDiscAmtString.Substring(0, strMainDiscAmtString.Length - 1)
                strsum = strsum.Substring(0, strsum.Length - 1)
                strsumQty = strsumQty.Substring(0, strsumQty.Length - 1)

                strPivotqry1 = ", " & strDiscCode & " " & strDiscAmtString & ""
                strPivotQry2 = ", " & strMainDiscCodeString & " " & strMainDiscAmtString & ""
                strPivotqry3 = " pivot (sum(DiscQty) for DiscCode in ( " & strDiscCodestring & " )) as pvt1  " & _
                               "pivot (sum(DiscAmt) for DiscCodeAmt in ( " & strDiscAmtString & " )) as Pvt2 "
                strPivotSum = " + " & strsum & ""
                strPivotSumQty = " + " & strsumQty & ""


                strDiscAmtSummary = strDiscAmtSummary.Substring(0, strDiscAmtSummary.Length - 1)
                strPivotSummary = ", " & strDiscCodeSummary & " " & strDiscAmtSummary & ""
            End If



            strMainQry = "select (Sale_Invoice_No),(Item_Code)," & strSeq & ",(MRPCase),MRPBottle,(TP),DP, " & _
            "Location_Code,Location_Desc,Route_No,Route_Desc,Cust_Code, " & _
            "Customer_Name, Cust_Type_Code, Cust_Type_Desc, Cust_Group_Code, Cust_Group_Desc,Salesman_Code,Salesman_Desc,   " & _
            "Agency_Gross_Quantity , Agency_Gross_Amount," & _
            "Distributor_Gross_Quantity, Distributor_Gross_Amount, " & _
            "(Agency_Gross_Quantity + Distributor_Gross_Quantity " & strPivotSumQty & ") as [Total Qty], " & _
            "(Agency_Gross_Amount + Distributor_Gross_Amount  " & strPivotSum & ") as [Total Amount] " & _
            " " & strPivotqry1 & " " & _
            "from ( " & _
            "select   " & strSeq & ", MRPCase/MrpBottleConvRate as MRPBottle,Sale_Invoice_No, item_code, MRPCase,TP,DP,Location_Code,Location_Desc,Route_No,Route_Desc, " & _
            "Cust_Code,Customer_Name,Cust_Type_Code,Cust_Type_Desc,Cust_Group_Code,Cust_Group_Desc,Salesman_Code,Salesman_Desc,   " & _
            "convert(decimal (18,2),sum(Agency_Gross_Quantity)) as Agency_Gross_Quantity, " & _
            "convert(decimal (18,2),SUM(Agency_Gross_Amount)) as Agency_Gross_Amount, " & _
            "convert(decimal (18,2),SUM(Distributor_Gross_Quantity)) as Distributor_Gross_Quantity, " & _
            "convert(decimal (18,2),SUM(Distributor_Gross_Amount)) as Distributor_Gross_Amount " & _
            " " & strPivotQry2 & " " & _
            " from " & _
             " (  "
            strQry1 = "SELECT  " & strSeq & ",TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MrpBottleConvRate, " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as CaseConF, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No, Cust_Type_Desc,Cust_Group_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id, " & strGrp & " , " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code,Salesman_Desc  , " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRPCase, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Price_Amount1 AS TP, " & _
                                "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9) AS DP, " & _
            "CASE WHEN Price_Amount5 <> 0 THEN (Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END AS Agency_Gross_Quantity, " & _
            "CASE WHEN Price_Amount5 <> 0 THEN Price_Amount5 * (Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END AS Agency_Gross_Amount, " & _
            "CASE WHEN Price_Amount4 <> 0 THEN (Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END AS Distributor_Gross_Quantity, " & _
            "CASE WHEN Price_Amount4 <> 0 THEN Price_Amount4 * (Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END AS Distributor_Gross_Amount , " & _
            "CASE WHEN Discount_Code <> '' AND VSND_Type = 'Y' THEN TSPL_Discount_Master.Description ELSE '' END AS DiscCode, " & _
            "CASE WHEN Discount_Code <> '' AND VSND_Type = 'Y' THEN (TSPL_Discount_Master.Description) + ' Amount' ELSE '' END AS DiscCodeAmt, " & _
            "CASE WHEN Discount_Code <> '' AND VSND_Type = 'Y' THEN CONVERT(decimal(18, 2), Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END AS DiscQty, " & _
            "CASE WHEN Discount_Code <> '' AND VSND_Type = 'Y' THEN CONVERT(decimal(18, 2), (Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END AS DiscAmt " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Discount_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
            "left outer join  TSPL_ITEM_DETAILS on TSPL_SALE_INVOICE_DETAIL.item_code= TSPL_ITEM_DETAILS.item_code left outer join TSPL_ITEM_MASTER on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
            "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " & _
            "TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "WHERE  convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103) AND " & _
            " " & strType & " AND (" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code <> 'F') and " & _
            "( Class_Name='size' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB') and (Price_Amount4 <> 0 or Price_Amount5 <> 0 or VSND_Type='Y' ) "

            strQry2 = " SELECT   " & strSeq & ",TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MrpBottleConvRate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as CaseConF, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No AS Sale_Invoice_No, " & _
            "Cust_Type_Desc,Cust_Group_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_Id AS Sale_Invoice_Id, " & _
            "" & strRetGrp & " , " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_No, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Route_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Salesman_Code, " & _
            "Salesman_Desc  , " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRPCase, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Price_Amount1 AS TP, " & _
                                            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9) AS DP, " & _
            "-(CASE WHEN Price_Amount5 <> 0 THEN (Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Agency_Gross_Quantity, " & _
            "-(CASE WHEN Price_Amount5 <> 0 THEN Price_Amount5 * (Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Agency_Gross_Amount, " & _
            "-(CASE WHEN Price_Amount4 <> 0 THEN (Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Distributor_Gross_Quantity, " & _
            "-(CASE WHEN Price_Amount4 <> 0 THEN Price_Amount4 * (Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Distributor_Gross_Amount , " & _
            "CASE WHEN Discount_Code <> '' AND VSND_Type = 'Y' THEN TSPL_Discount_Master.Description ELSE '' END AS DiscCode, " & _
            "CASE WHEN Discount_Code <> '' AND VSND_Type = 'Y' THEN (TSPL_Discount_Master.Description) + ' Amount' ELSE '' END AS DiscCodeAmt, " & _
            "-(CASE WHEN Discount_Code <> '' AND VSND_Type = 'Y' THEN CONVERT(decimal(18, 2), Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS DiscQty, " & _
            "-(CASE WHEN Discount_Code <> '' AND VSND_Type = 'Y' THEN CONVERT(decimal(18, 2), (Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * (MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END) AS DiscAmt " & _
            "FROM  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code " & _
            "RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_Discount_Master RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_Discount_Master.Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Discount_Code  " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code  " & _
            "RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD ON  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Cust_Code " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
            "left outer join  TSPL_ITEM_DETAILS on " & _
            "TSPL_SALE_RETURN_DETAIL.item_code= TSPL_ITEM_DETAILS.item_code left outer join " & _
            "TSPL_ITEM_MASTER on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  left outer join " & _
            "TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on " & _
            "TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code WHERE " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= convert(date, '" & ToDate.Value & "',103) AND  " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code <> 'F') and " & _
            "( Class_Name='size' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB') and " & _
            "(Price_Amount4 <> 0 or Price_Amount5 <> 0 or VSND_Type='Y' )  and " & strType & " " & strReturnPost & "  "

            strInterComp = "SELECT   " & strSeq & ",TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor as MrpBottleConvRate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor as CaseConF, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No AS Sale_Invoice_No, " & _
            "Cust_Type_Desc,Cust_Group_Desc, 1 AS Sale_Invoice_Id, " & _
            "" & strInterGrp & " , " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code, " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_No, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Route_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Name, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code, Salesman_Desc  , " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS MRPCase, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount1 AS TP, " & _
                                                        "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt * " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9) AS DP, " & _
            "-(CASE WHEN Price_Amount5 <> 0 THEN (Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Agency_Gross_Quantity, " & _
            "-(CASE WHEN Price_Amount5 <> 0 THEN Price_Amount5 * (Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Agency_Gross_Amount, " & _
            "-(CASE WHEN Price_Amount4 <> 0 THEN (Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Distributor_Gross_Quantity, " & _
            "-(CASE WHEN Price_Amount4 <> 0 THEN Price_Amount4 * (Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) ELSE 0 END) AS Distributor_Gross_Amount , " & _
            "'' AS DiscCode,'' AS DiscCodeAmt, " & _
            "0 AS DiscQty, 0 AS DiscAmt FROM  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code " & _
            "= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code  " & _
            "= " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code RIGHT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code " & _
            "RIGHT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
            "left outer join  TSPL_ITEM_DETAILS on " & _
            "TSPL_SALE_RETURN_INTER_DETAIL.item_code= TSPL_ITEM_DETAILS.item_code left outer join " & _
            "TSPL_ITEM_MASTER on TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  " & _
            "left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on " & _
            "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code WHERE " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) >= " & _
            " convert(date, '" & fromDate.Value & "',103) AND " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) <= " & _
            " convert(date, '" & ToDate.Value & "',103) and ( Class_Name='size' and TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB') and " & _
            "(Price_Amount4 <> 0 or Price_Amount5 <> 0  ) " & strInterPost & "  "


            If strLocation = "N" Then
                strQry1 += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                strQry2 += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                strInterComp += " and " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If strCustAll = "N" Then
                strQry1 += " and TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                strQry2 += " and TSPL_SALE_RETURN_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                strInterComp += " and TSPL_SALE_RETURN_INTER_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "

            End If

            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If

            If rdbIterComp.Checked Then
                strQry = strQry1 & " Union All " & strQry2 & " Union All " & strInterComp
            Else
                strQry = strQry1 & " Union All " & strQry2
            End If

            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQry, ArrDBName, False)
            dt = clsDBFuncationality.GetDataTable(strQuery) '

            strPivot = " )a " & strPivotqry3 & "  " & _
            "group by Sale_Invoice_No,item_code,MRPCase,TP,DP,Location_Code,Location_Desc, " & _
            "Route_No, Route_Desc, Cust_Code, Customer_Name, Cust_Type_Code, Cust_Type_Desc, " & _
            "Cust_Group_Code, Cust_Group_Desc,Salesman_Code,Salesman_Desc," & strSeq & ",MrpBottleConvRate " & _
            "  ) b"

            If rdbSummary.IsChecked Then
                strQry = "select distinct Item_Code," & strSeq & ",TP,DP,MRPCase,MRPBottle," & _
                "SUM(Agency_Gross_Quantity) as  Agency_Gross_Quantity, " & _
                "SUM(Agency_Gross_Amount) as Agency_Gross_Amount, " & _
                "SUM(Distributor_Gross_Quantity) as Distributor_Gross_Quantity, " & _
                "sum(Distributor_Gross_Amount) as Distributor_Gross_Amount, " & _
                "SUM([Total Qty]) as [Total Qty], " & _
                "SUM([Total Amount]) as [Total Amount], " & _
                "case when SUM([Total Qty]) <> 0 then sum([Total Amount])/SUM([Total Qty]) else 0 end as Amountcase  " & strPivotSummary & "  " & _
                "from   ( " & strMainQry & strQuery & strPivot & " )C  group by (Item_Code),(MRPCase),(TP),DP,MRPBottle, " & strSeq & "  " & strOrder & " "
            Else
                strQry = strMainQry & strQuery & strPivot
            End If



            'strQry = strQry

            dt = clsDBFuncationality.GetDataTable(strQry)
            gvReport.DataSource = Nothing
            gvReport.Columns.Clear()
            gvReport.Rows.Clear()
            gvReport.GroupDescriptors.Clear()
            gvReport.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                gvReport.DataSource = dt
                SetGridFormationOFgvReport()
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try



        'MsgBox("successfully")
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub
End Class
