
'''''''''''''' New Sales Analysis Report added by Panch Raj
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

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
Imports System.IO

Public Class frmSalesAnalysisReport
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery, strType As String
    'Dim dr As SqlDataReader
    Dim ArrDBName As ArrayList = Nothing
    Dim strLocation As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
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
    Sub LoadLocation()
        Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub
    '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptSalesAnalysis)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Sub LoadCustomer()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Cust_Code,Customer_Name from TSPL_CUSTOMER_MASTER where Parent_Customer_YN='N'")
        chkCustomer.DataSource = dt
        chkCustomer.ValueMember = "Cust_Code"
        chkCustomer.DisplayMember = "Customer_Name"
    End Sub


    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub frmSalesAnalysisReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dtpFdate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        rbtnCategoryAll.IsChecked = True
        LoadCategory()
        LoadLocation()
        chkLocAll.IsChecked = True
        LoadCustomer()
        SetUserMgmtNew()
        chkClassAll.IsChecked = True
        rdbDocSummary.IsChecked = True
        Me.Text = "Sales Analysis Report"
        rdbWithFOC.IsChecked = True
        RadGroupBox1.Visible = True
        ddlSaleType.SelectedIndex = 0

    End Sub
    Sub LoadCategory()
        Dim qry As String = "select Code,Name,Parent from ("
        qry += " select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE"
        qry += " union all"
        qry += " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
        qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE"
        qry += " Union all"
        qry += " select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
        qry += " )xxx order by Sno"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        tvCategory.DataSource = Nothing
        tvCategory.TreeViewElement.AutoSizeItems = True
        tvCategory.ShowLines = True
        tvCategory.ShowRootLines = True
        tvCategory.TreeViewElement.ViewElement.Margin = New Padding(4)
        tvCategory.ShowExpandCollapse = True
        tvCategory.TreeIndent = 15
        tvCategory.FullRowSelect = False
        tvCategory.ShowLines = True
        tvCategory.LineStyle = TreeLineStyle.Dot
        tvCategory.LineColor = Color.FromArgb(110, 153, 210)
        tvCategory.ExpandAnimation = ExpandAnimation.Opacity
        tvCategory.AllowEdit = False
        tvCategory.ShowRootLines = False
        tvCategory.TreeViewElement.AllowAlternatingRowColor = True
        tvCategory.TreeViewElement.AlternatingRowColor = Color.AliceBlue

        tvCategory.TreeViewElement.DrawBorder = True
        tvCategory.ValueMember = "Code"
        tvCategory.DisplayMember = "Name"
        tvCategory.ChildMember = "Code"
        tvCategory.ParentMember = "Parent"
        tvCategory.DataSource = dt
        tvCategory.CheckBoxes = True

        'tvCategory.ExpandAll()
    End Sub

    Sub Reset()

        dtpFdate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        rbtnCategoryAll.IsChecked = True
        LoadCategory()
        LoadLocation()
        chkLocAll.IsChecked = True
        LoadCustomer()
        chkClassAll.IsChecked = True
        'LoadHierarchy()
        rdbDetail.IsChecked = True
        rdbWithFOC.IsChecked = True
    End Sub

    Sub print()
        Try
            Dim dt As DataTable
            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
                Return
            ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomer.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one Customer or select ALL")
                Return
            End If
            Dim strSql, strSettlement, strFOC, strLocAll, strClassAll As String
            strFOC = ""
            If chkLocAll.IsChecked = True Then
                strLocAll = "Y"
            Else
                strLocAll = "N"
            End If

            If chkClassAll.IsChecked = True Then
                strClassAll = "Y"
            Else
                strClassAll = "N"
            End If

            If rdbWithFOC.IsChecked Then
                strFOC = ""
            ElseIf rdbWoFOC.IsChecked Then
                strFOC = " and coalesce(Scheme_Item,'N')='N'"
            ElseIf rdbFOC.IsChecked Then
                strFOC = " and coalesce(Scheme_Item,'N')='Y'"
            End If


            'Dim HierCode, HierColumn As String

            Dim strLoca As String = ""
            Dim strClass As String = ""
            If chkLocSelect.IsChecked Then
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
            End If

            If chkClassSelect.IsChecked Then
                For Each Str As String In chkCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strClass) > 0 Then
                        strClass += ", "
                    End If
                    strClass += Str
                Next
            End If




            '''''   Main qyery

            Dim strInterComp As String
            Dim strMain As String = " select Discount_Amt,Scheme_Item,InvoiceQty,Excisable,Location_Code,Location_Desc,Route_No,Route_Desc, " & _
            "Type,Document_Code as Sale_Invoice_No,CONVERT(varchar, DocDate,103) as DocDate,Transfer_No,Transfer_Date,ParentCode, Customer_Code,Customer_Name,Item_Code, " & _
            "MICode,MIUnit,mainqty," & _
            "Unit_Code," & _
            "convert(decimal(18,2),mrp) as mrp,convert(decimal(18,2),mrpBottle) as mrpBottle,TP, " & _
                    "([GrossQTY]) as [Gross Qty], " & _
                    "CONVERT(DECIMAL(18,2),(DiscQTY)) as [Qty Disc], " & _
                    "CONVERT(DECIMAL(18,2),(NetQty)) as [Net Qty], " & _
                    "CONVERT(DECIMAL(18,2),basic_rate) as basic_rate," & _
                    "(Total_Basic_Amt)  AS [Total Basic Amt],  " & _
                    "CONVERT(DECIMAL(18,2),(ExciseAmt))  as [Excise Amt], " & _
                    "CONVERT(DECIMAL(18,2),(ECessAmt))    as [Cess Amt], " & _
                    "CONVERT(DECIMAL(18,2),(HCessAmt))   as [Hcess Amt], " & _
                    "CONVERT(DECIMAL(18,2),Excise) as Excise, " & _
                    "CONVERT(DECIMAL(18,2),(Cess))   as Cess, " & _
                    "CONVERT(DECIMAL(18,2),(Hcess))  as Hcess, " & _
                    "convert(decimal (18,2),(FOCAMt)) as FOCAMt, " & _
                    "convert(decimal(18,2),InvoiceAmt) as InvoiceAmt, " & _
                    "CONVERT(DECIMAL(18,2),((Total_Disc_Amt) ))  as DISC, " & _
                    "CONVERT(DECIMAL(18,2),(CSTTaxAmt))  as [CST], " & _
                    "CONVERT(DECIMAL(18,2),(VatTaxAmt))  as [VAT], " & _
                    "CONVERT(DECIMAL(18,2),(AddTaxAmt + OtherTaxAmt))  as [Other Tax], " & _
                    "DocAmt, " & _
                    "CONVERT(DECIMAL(18,2),(DVAT))   as DVAT, " & _
                    "[TPT Rate], " & _
                    "CONVERT(DECIMAL(18,2),(MRP - Margin))  AS [T.Rate], " & _
                    "(Margin)  as Margin, " & _
                    "CONVERT(DECIMAL(18,2),(MRP - Margin))  AS [T.Price], " & _
                    "CONVERT(DECIMAL(18,2),(Total_TPT))  as [TPT Amt], " & _
                    "CONVERT(DECIMAL(18,2),((MRP - Margin) * InvoiceQty))   as [T.Rate Amt], " & _
                    "CONVERT(DECIMAL(18,2),(Total_MRP_Amt))  as [Total MRP] , " & _
                    "CONVERT(DECIMAL(18,2),(Margin * InvoiceQty))  as [T.Margin], " & _
                    "CONVERT(DECIMAL(18,2),((MRP - Margin) * InvoiceQty))  as [T.Price Amt], " & _
                    "CONVERT(DECIMAL(18,2),(isnull(ISNULL(commamt,CommHisamt) * GrossQTY,0)))  as COMMAmt, " & _
                    "[Sale Account Amt], " & _
                    "case when type='Sale Invoice' then case when Excisable='F'  and coalesce(Scheme_Item,'N')='N' then   ([Total_Basic_Amt] - [Sale Account Amt] - Total_Disc_Amt) else 0 end " & _
                    "else case when Excisable='F' and coalesce(Scheme_Item,'N')='N' then   ([Total_Basic_Amt] - [Sale Account Amt] - Total_Disc_Amt) else 0  end end as Exciseamt, " & _
                    "Total_Cust_Discount,Adjustment_Amount,DocType from "

            Dim str1 As String = "Select * from ( " & _
            "SELECT TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,Scheme_Item,'' as Main_Item, TSPL_SD_SALE_INVOICE_HEAD.Document_Date as  DocDate,case when (coalesce(Scheme_Item,'N')='N') then   TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt else 0 end as  DocAmt,Excisable,Location_Code,Location_Desc,TSPL_SD_SALE_INVOICE_HEAD.Route_No, TSPL_SD_SALE_INVOICE_HEAD.Route_Desc, " & _
            "null  as Transfer_Date,TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt, " & _
            "TSPL_CUSTOMER_MASTER.Customer_Name,case when Unit_code='FC' then  TSPL_SD_SALE_INVOICE_DETAIL.MRP / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else  TSPL_SD_SALE_INVOICE_DETAIL.MRP end AS MRPBottle, " & _
            "TSPL_CUSTOMER_MASTER.Parent_Customer_No, TSPL_CUSTOMER_MASTER.Cust_Code,0 as Adjustment_Amount,'Sale Invoice' as Type, " & _
            "TSPL_SD_SALE_INVOICE_HEAD.Document_Code ,'' as Transfer_No, " & _
            " TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,   TSPL_SD_SALE_INVOICE_DETAIL.MRP *  TSPL_ITEM_UOM_DETAIL.Conversion_Factor as mrp," & _
            " TSPL_SD_SALE_INVOICE_DETAIL.MRP *  TSPL_ITEM_UOM_DETAIL.Conversion_Factor -  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount1 AS TP, " & _
            " (TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost) as Basic_Rate, " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt ELSE 0 END AS Excise," & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt ELSE 0 END AS Cess,  " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt ELSE 0 END AS Hcess, " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt +  TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt ELSE  TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt END AS DVAT," & _
            " 0 as [TPT Rate]," & _
            "( TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount1 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount2 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount3 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount4  +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount5 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount6 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount7 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount8 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount9)  as Margin," & _
            " TSPL_SD_SALE_INVOICE_DETAIL.Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as GrossQTY," & _
            "case when coalesce(Scheme_Item,'N')='Y' then 0 else Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end AS NetQty," & _
            "case when coalesce(Scheme_Item,'N')='Y' then  Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS DiscQTY, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Amount as Total_Basic_Amt,TSPL_SD_SALE_INVOICE_DETAIL.MRP as Total_MRP_Amt,  " & _
            "Qty as Invoiceqty, " & _
            "case when coalesce(Scheme_Item,'N')='Y' then 0 else Qty end AS netInvoice_Qty, " & _
            "case when coalesce(Scheme_Item,'N')='Y' then Qty else 0 end AS disc,Cust_Discount, " & _
            "0 as Commamt, " & _
            "0 as CommHisamt, " & _
            "CASE WHEN coalesce(Scheme_Item,'N') = 'Y' THEN ((Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((Item_Cost * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END AS FOCAMt, " & _
            " 0 as [Sale Account Amt],Total_Cust_Discount,0 as Total_TPT,'SD-IN'  as DocType, " & _
            " TSPL_SD_SALE_INVOICE_DETAIL.TAX1, TSPL_SD_SALE_INVOICE_DETAIL.TAX2, TSPL_SD_SALE_INVOICE_DETAIL.TAX3, " & _
            " TSPL_SD_SALE_INVOICE_DETAIL.TAX4, TSPL_SD_SALE_INVOICE_DETAIL.TAX5, " & _
            " TSPL_SD_SALE_INVOICE_DETAIL.TAX6, TSPL_SD_SALE_INVOICE_DETAIL.TAX7, " & _
            " TSPL_SD_SALE_INVOICE_DETAIL.TAX8, TSPL_SD_SALE_INVOICE_DETAIL.TAX9, " & _
            " TSPL_SD_SALE_INVOICE_DETAIL.TAX10," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt) as TAX1_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt) as TAX2_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt) as TAX3_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt) as TAX4_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt) as TAX5_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt) as TAX6_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt) as TAX7_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt) as TAX8_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt) as TAX9_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt) as TAX10_Amt" & _
            " FROM  TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
             " TSPL_SD_SALE_INVOICE_HEAD INNER JOIN " & _
             " TSPL_SD_SALE_INVOICE_DETAIL ON  TSPL_SD_SALE_INVOICE_HEAD.Document_Code =  TSPL_SD_SALE_INVOICE_DETAIL.Document_Code INNER JOIN " & _
             " TSPL_ITEM_UOM_DETAIL ON  TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =  TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
             " TSPL_SD_SALE_INVOICE_DETAIL.Unit_code =  TSPL_ITEM_UOM_DETAIL.UOM_Code ON " & _
             " TSPL_LOCATION_MASTER.Location_Code =  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location LEFT OUTER JOIN " & _
             " TSPL_CUSTOMER_TYPE_MASTER RIGHT OUTER JOIN " & _
             " TSPL_CUSTOMER_MASTER ON  TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code =  TSPL_CUSTOMER_MASTER.Cust_Type_Code ON " & _
             " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code =  TSPL_CUSTOMER_MASTER.Cust_Code " & _
              "left outer join  TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " & _
                      " TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "left outer join  TSPL_SD_SHIPMENT_HEAD on  TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No= TSPL_SD_SHIPMENT_HEAD.document_code " & _
             "where /*TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and */  convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND  " & _
            "convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date, '" & dtpToDate.Value & "',103) and " & _
            "TSPL_SD_SHIPMENT_HEAD.Status='1'  " & strFOC & "  "
            If strLocAll = "N" Then
                str1 += " and  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If
            If strClassAll = "N" Then
                str1 += " and  TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(chkCustomer.CheckedValue) + ") "
            End If
            'If strHierAll = "N" Then
            '    str1 += " and  TSPL_SD_SALE_INVOICE_HEAD." & HierCode & " in (" + clsCommon.GetMulcallString(cbgHier.CheckedValue) + ") "
            'End If

            str1 += " ) b " & _
            "left outer join (select Item_Code as MICode, case when count(Unit_code) >  1 then 'FC' else MAX(Unit_code) end as MIUnit , " & _
            "Document_Code as MainInvoice,case when COUNT(unit_code) > 1 then (select Qty from TSPL_SD_SALE_INVOICE_DETAIL where " & _
            "Document_Code=a.Document_Code and Item_Code=a.Item_Code and Unit_code='FC' and coalesce(Scheme_Item,'N')='N') else MAX(Qty)  end as mainqty, " & _
            "0 as FOCQty   from (SELECT Item_Code,Unit_code,TSPL_SD_SALE_INVOICE_HEAD.Document_Code,Qty FROM TSPL_SD_SALE_INVOICE_HEAD " & _
            "left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code " & _
            "WHERE coalesce(Scheme_Item,'N')='N' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
            "convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date, '" & dtpToDate.Value & "',103)    " & _
            ") a  group by Document_Code ,item_Code ) as c  on b.Main_Item=c.MICode and b.Document_Code=c.MainInvoice  "

            Dim str2 As String = " Select * from ( " & _
            " SELECT TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,'N' as Scheme_Item,'' as Main_Item, TSPL_SD_SALE_RETURN_HEAD.Document_Date as DocDate, " & _
            "-case when ('N'='N') then   TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt else 0 end as DocAmt, " & _
            "TSPL_LOCATION_MASTER_1.Excisable,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,'' as Route_No,'' as Route_Desc,null as Transfer_Date,- TSPL_SD_SALE_RETURN_DETAIL.Disc_Amt as Total_Disc_Amt, " & _
            "TSPL_CUSTOMER_MASTER.Customer_Name, case when Unit_code='FC' then  TSPL_SD_SALE_RETURN_DETAIL.MRP / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else  TSPL_SD_SALE_RETURN_DETAIL.MRP end AS MRPBottle, " & _
            "TSPL_CUSTOMER_MASTER.Parent_Customer_No, TSPL_CUSTOMER_MASTER.Cust_Code,0 as Adjustment_Amount,'Sale Return' as Type,TSPL_SD_SALE_RETURN_HEAD.Document_Code,'' as Transfer_No, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.Item_Code, TSPL_SD_SALE_RETURN_DETAIL.Unit_code, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.MRP *  TSPL_ITEM_UOM_DETAIL.Conversion_Factor as mrp, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.MRP *  TSPL_ITEM_UOM_DETAIL.Conversion_Factor -  0 AS TP," & _
            " (TSPL_SD_SALE_RETURN_DETAIL.Item_Cost)  as Basic_Rate, " & _
            "CASE WHEN TSPL_LOCATION_MASTER_1.Excisable = 'T' THEN  TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt ELSE 0 END AS Excise, " & _
            "CASE WHEN TSPL_LOCATION_MASTER_1.Excisable = 'T' THEN  TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt ELSE 0 END AS Cess, " & _
            "CASE WHEN TSPL_LOCATION_MASTER_1.Excisable = 'T' THEN  TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt ELSE 0 END AS Hcess, " & _
            "CASE WHEN TSPL_LOCATION_MASTER_1.Excisable = 'T' THEN  TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt +  TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt ELSE  TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt END AS DVAT, " & _
            " 0  as [TPT Rate], " & _
            "( 0 +  0 +  0 +  0  +  0 +  0 +  0 +  0 +  0)  as Margin, " & _
            "- ( TSPL_SD_SALE_RETURN_DETAIL.Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  as GrossQTY, " & _
            "-(case when 'N'='Y' then 0 else Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end) AS NetQty, " & _
            "-(case when 'N'='Y' then  Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end) AS DiscQTY, " & _
            "-( TSPL_SD_SALE_RETURN_DETAIL.Amount ) as Total_Basic_Amt, " & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.MRP) as Total_MRP_Amt, " & _
            "-(Qty) as Invoiceqty, " & _
            "-(case when 'N'='Y' then 0 else Qty end) AS netInvoice_Qty, " & _
            "-(case when 'N'='Y' then Qty else 0 end) AS disc, - (0) as Cust_Discount, " & _
            "0 as Commamt, " & _
            "0 as CommHisamt, " & _
            "-(CASE WHEN 'N' = 'Y'  THEN " & _
            "((Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((Item_Cost * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (0 + 0 + 0 + 0 + 0 + 0 + 0 + 0 + 0))) ELSE 0 END) AS FOCAMt, " & _
            "-0 as [Sale Account Amt],- (0) as Total_Cust_Discount,-(0) as Total_TPT ,'SD-SR' as DocType, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.TAX1, TSPL_SD_SALE_RETURN_DETAIL.TAX2, TSPL_SD_SALE_RETURN_DETAIL.TAX3, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.TAX4, TSPL_SD_SALE_RETURN_DETAIL.TAX5, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.TAX6, TSPL_SD_SALE_RETURN_DETAIL.TAX7, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.TAX8, TSPL_SD_SALE_RETURN_DETAIL.TAX9, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.TAX10," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt ) as TAX1_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt) as TAX2_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt) as TAX3_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt) as TAX4_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt) as TAX5_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt) as TAX6_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt) as TAX7_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt) as TAX8_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX9_Amt) as TAX9_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX10_Amt) as TAX10_Amt" & _
            " FROM  TSPL_LOCATION_MASTER RIGHT OUTER JOIN  TSPL_SD_SALE_RETURN_HEAD INNER JOIN " & _
            " TSPL_SD_SALE_RETURN_DETAIL ON  TSPL_SD_SALE_RETURN_HEAD.Document_Code =  TSPL_SD_SALE_RETURN_DETAIL.Document_Code INNER JOIN " & _
            " TSPL_ITEM_UOM_DETAIL ON  TSPL_SD_SALE_RETURN_DETAIL.Item_Code =  TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
            " TSPL_SD_SALE_RETURN_DETAIL.Unit_code =  TSPL_ITEM_UOM_DETAIL.UOM_Code ON " & _
            " TSPL_LOCATION_MASTER.Location_Code =  TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location LEFT OUTER JOIN " & _
            " TSPL_CUSTOMER_TYPE_MASTER RIGHT OUTER JOIN  TSPL_CUSTOMER_MASTER ON " & _
            " TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code =  TSPL_CUSTOMER_MASTER.Cust_Type_Code ON " & _
            " TSPL_SD_SALE_RETURN_HEAD.Customer_Code =  TSPL_CUSTOMER_MASTER.Cust_Code  " & _
             "left outer join  TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " & _
            " TSPL_SD_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            " left outer join  TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
            "where convert(date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
            "convert(date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <= convert(date, '" & dtpToDate.Value & "',103) and " & _
            "TSPL_SD_SALE_RETURN_HEAD.Status='1'  /* " & strFOC & " */ "
            If strLocAll = "N" Then
                str2 += " and  TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            'TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and  


            If strClassAll = "N" Then
                str2 += " and  TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(chkCustomer.CheckedValue) + ") "
            End If

            'If strHierAll = "N" Then
            '    str2 += " and  TSPL_SD_SALE_RETURN_HEAD." & HierCode & " in (" + clsCommon.GetMulcallString(cbgHier.CheckedValue) + ") "
            'End If

            str2 += " ) b " & _
            " left outer join (select Item_Code as MICode , case when count(Unit_code) >  1 then 'FC' else MAX(Unit_code) end as MIUnit , " & _
            "Document_Code as MainInvoice, case when COUNT(unit_code) > 1 then (select Qty from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code=a.Document_Code " & _
            "and Item_Code=a.Item_Code and Unit_code='FC' and coalesce(Scheme_Item,'N')='N') else MAX(Qty)  end as mainqty,0 as FOCQty " & _
            "from (SELECT Item_Code,Unit_code,TSPL_SD_SALE_INVOICE_HEAD.Document_Code,Qty FROM TSPL_SD_SALE_INVOICE_HEAD left outer join " & _
            "TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code WHERE " & _
            "coalesce(Scheme_Item,'N')='N' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date, '18/09/2013 1:48:47 PM',103) AND " & _
            "convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date, '18/09/2013 1:48:47 PM',103)  ) a  " & _
            "group by Document_Code ,item_Code ) as c  on b.Main_Item=c.MICode and b.Document_Code=c.MainInvoice  "

            strInterComp = "  SELECT 0 as Discount_Amt,Scheme_Item,'' as Main_Item, TSPL_SALE_RETURN_INTER_HEAD.Document_Date as DocDate ,case when (coalesce(Scheme_Item,'N')='N') then  TSPL_SALE_RETURN_INTER_DETAIL.Item_Net_Amt else 0 end as DocAmt ,Excisable,Location_Code,Location_Desc,TSPL_SALE_RETURN_INTER_HEAD.Route_No,TSPL_SALE_RETURN_INTER_HEAD.Route_Desc,null as Transfer_Date,0 as Total_Disc_Amt, " & _
            "TSPL_CUSTOMER_MASTER.Customer_Name, case when Unit_code='FC' then  TSPL_SALE_RETURN_INTER_DETAIL.MRP_amt / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else  TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt end AS MRPBottle, " & _
            "TSPL_CUSTOMER_MASTER.Parent_Customer_No, TSPL_CUSTOMER_MASTER.Cust_Code,0 as Adjustment_Amount,'Sale InerCompany' as Type,TSPL_SALE_RETURN_INTER_HEAD.Document_No,'' as Transfer_No, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.Item_Code, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.Unit_code, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt *  TSPL_ITEM_UOM_DETAIL.Conversion_Factor as mrp, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt *  TSPL_ITEM_UOM_DETAIL.Conversion_Factor -  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount1 AS TP, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.Basic_Rate  as Basic_Rate, " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt ELSE 0 END AS Excise, " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt ELSE 0 END AS Cess, " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Amt ELSE 0 END AS Hcess, " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Amt +  TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Amt ELSE  TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt END AS DVAT, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.TPT  as [TPT Rate], " & _
            "( TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount1 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount2 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount3 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount4  +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount5 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount6 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount7 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount8 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount9)  as Margin, " & _
            "-  TSPL_SALE_RETURN_INTER_DETAIL.Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as GrossQTY, " & _
            "- case when coalesce(Scheme_Item,'N')='Y' then 0 else qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end AS NetQty, " & _
            "- case when coalesce(Scheme_Item,'N')='Y' then  Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS DiscQTY, " & _
            "-  TSPL_SALE_RETURN_INTER_DETAIL.Total_Basic_Amt, " & _
            "-  TSPL_SALE_RETURN_INTER_DETAIL.Total_MRP_Amt, " & _
            "- Qty as Invoiceqty, " & _
            "- case when coalesce(Scheme_Item,'N')='Y' then 0 else Qty end AS netInvoice_Qty, " & _
            "- case when coalesce(Scheme_Item,'N')='Y' then Qty else 0 end AS disc,- Cust_Discount, " & _
            "0 as Commamt, " & _
            "0 as CommHisamt, " & _
            "0 AS FOCAMt, -  0 as [Sale Account Amt],- Total_Cust_Discount,-( TSPL_SALE_RETURN_INTER_DETAIL.Total_TPT ),'SD-IN' as  DocType," & _
            " TSPL_SALE_RETURN_INTER_DETAIL.TAX1, TSPL_SALE_RETURN_INTER_DETAIL.TAX2, TSPL_SALE_RETURN_INTER_DETAIL.TAX3, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.TAX4, TSPL_SALE_RETURN_INTER_DETAIL.TAX5, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.TAX6, TSPL_SALE_RETURN_INTER_DETAIL.TAX7, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.TAX8, TSPL_SALE_RETURN_INTER_DETAIL.TAX9, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.TAX10," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt ) as TAX1_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt) as TAX2_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Amt) as TAX3_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Amt) as TAX4_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Amt) as TAX5_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Amt) as TAX6_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX7_Amt) as TAX7_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX8_Amt) as TAX8_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX9_Amt) as TAX9_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX10_Amt) as TAX10_Amt" & _
            ",'' as MICode,'' as MIUnit,'' as MainInvoice,0 as mainqty,0 as FOCQty from   TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
            " TSPL_SALE_RETURN_INTER_HEAD INNER JOIN " & _
            " TSPL_SALE_RETURN_INTER_DETAIL ON " & _
            " TSPL_SALE_RETURN_INTER_HEAD.Document_No =  TSPL_SALE_RETURN_INTER_DETAIL.Document_No INNER JOIN " & _
            " TSPL_ITEM_UOM_DETAIL ON  TSPL_SALE_RETURN_INTER_DETAIL.Item_Code =  TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.Unit_code =  TSPL_ITEM_UOM_DETAIL.UOM_Code ON " & _
            " TSPL_LOCATION_MASTER.Location_Code =  TSPL_SALE_RETURN_INTER_HEAD.Location LEFT OUTER JOIN " & _
            " TSPL_CUSTOMER_TYPE_MASTER RIGHT OUTER JOIN  TSPL_CUSTOMER_MASTER ON " & _
            " TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code =  TSPL_CUSTOMER_MASTER.Cust_Type_Code ON " & _
            " TSPL_SALE_RETURN_INTER_HEAD.Cust_Code =  TSPL_CUSTOMER_MASTER.Cust_Code " & _
             "left outer join  TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "where 1=1 and convert(date, TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) >=  convert(date, '" & dtpFdate.Value & "',103) AND " & _
            "convert(date, TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) <=  convert(date, '" & dtpToDate.Value & "',103) and  Is_Post=1 "

            'TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' 



            strSettlement = " SELECT TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt, TSPL_SD_SALE_INVOICE_HEAD.Document_Date as DocDate, TSPL_SD_SALE_INVOICE_HEAD.Total_Invoice_Amt as DocAmt,Excisable,Location_Code,Location_Desc,'' as Route_No,'' as Route_Desc,null as Transfer_Date,0 as Total_Disc_Amt, " & _
            "TSPL_CUSTOMER_MASTER.Customer_Name, 0 as MRPBottle, TSPL_CUSTOMER_MASTER.Cust_Code,Adjustment_Amount,'Sale Invoice' as Type, " & _
            "TSPL_SD_SALE_INVOICE_HEAD.Document_Code,'' as Transfer_No,'' as Item_Code,'' as Unit_code, " & _
            "0 as mrp,0 AS TP, 0 as Basic_Rate, 0 AS Excise,0 AS Cess,  0 AS Hcess, 0 AS DVAT, " & _
            "0 as [TPT Rate],0  as Margin,0  as GrossQTY,0 AS NetQty,0 AS DiscQTY, 0 as Total_Basic_Amt, " & _
            "0 as Total_MRP_Amt, 0 as Invoiceqty, 0 AS netInvoice_Qty, 0 AS disc,0 as Cust_Discount, " & _
            "0 as Commamt, 0 as CommHisamt, 0 AS FOCAMt, 0 as [Sale Account Amt],0 as Total_Cust_Discount,0 as Total_TPT,'SD-SR' as  DocType FROM  " & _
            " TSPL_LOCATION_MASTER RIGHT OUTER JOIN  TSPL_SD_SALE_INVOICE_HEAD " & _
            "ON  TSPL_LOCATION_MASTER.Location_Code =  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
            "LEFT OUTER JOIN  TSPL_CUSTOMER_TYPE_MASTER RIGHT OUTER JOIN " & _
            " TSPL_CUSTOMER_MASTER ON  TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code " & _
            "=  TSPL_CUSTOMER_MASTER.Cust_Type_Code ON " & _
            " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code =  TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "Right outer join TSPL_Receipt_Adjustment_Header on TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
            "=TSPL_Receipt_Adjustment_Header.Doc_No where  " & _
            "convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= " & _
            "convert(date, '" & dtpFdate.Value & "',103) AND " & _
            "convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= " & _
            "convert(date, '" & dtpToDate.Value & "',103) "

            If strLocAll = "N" Then
                strInterComp += " and  TSPL_SALE_RETURN_INTER_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                strSettlement += " and  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "

            End If

            If strClassAll = "N" Then
                strInterComp += " and  TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(chkCustomer.CheckedValue) + ") "
                strSettlement += " and  TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(chkCustomer.CheckedValue) + ") "
            End If

            'If strHierAll = "N" Then
            '    strInterComp += " and  TSPL_SALE_RETURN_INTER_HEAD." & HierCode & " in (" + clsCommon.GetMulcallString(cbgHier.CheckedValue) + ") "
            '    strSettlement += " and  TSPL_SD_SALE_INVOICE_HEAD." & HierCode & " in (" + clsCommon.GetMulcallString(cbgHier.CheckedValue) + ") "
            'End If
            Dim strUnion As String = " Union all"
            'strSql = strMain & "( " & str1 & " " & strUnion & "  " & str2 & ") a "

            'If rdbIterComp.Checked Then
            If ddlSaleType.SelectedIndex = 0 Then
                strSql = str1 & strUnion & str2 & strUnion & strInterComp
            ElseIf ddlSaleType.SelectedIndex = 1 Then
                strSql = str1 & strUnion & strInterComp
            Else
                strSql = str2 & strUnion & strInterComp
            End If
            'strSql = str1 & strUnion & str2 & strUnion & strInterComp
            'Else
            'strSql = str1 & strUnion & str2
            'End If

            strSql = " select Discount_Amt,Scheme_Item,replace (convert(varchar(12),DocDate,106),' ','/') as DocDate,case when Discount_Amt=100 then 0 else DocAmt end as DocAmt,xxxxx.Excisable,xxxxx.Location_Code, " & _
            "xxxxx.Location_Desc ,Route_No,Route_Desc, replace (convert(varchar(12),null,106),' ','/') as Transfer_Date, " & _
            "Total_Disc_Amt,Customer_Name,MRPBottle,Parent_Customer_No as ParentCode,Cust_Code as Customer_Code,Adjustment_Amount,xxxxx.Type,Document_Code,'' as Transfer_No, " & _
            "Item_Code,Unit_code, " & _
            "case when MICode IS null and coalesce(Scheme_Item,'N')='Y'  then 'Target Discount' else MICode end as MICode,MIUnit,mainqty," & _
            "mrp,TP,Basic_Rate,Excise,Cess,Hcess,DVAT,[TPT Rate],Margin,GrossQTY,NetQty,DiscQTY, " & _
            "Total_Basic_Amt,Total_MRP_Amt,Invoiceqty,netInvoice_Qty,disc,Cust_Discount,Commamt,CommHisamt,FOCAMt, " & _
            "[Sale Account Amt],Total_Cust_Discount,Total_TPT,DocType, (case when  TaxM1.Type='A' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " & _
            "+case when  TaxM2.Type='A' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " & _
             "+case when  TaxM3.Type='A' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " & _
             "+case when  TaxM4.Type='A' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " & _
             "+case when  TaxM5.Type='A' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " & _
             "+case when  TaxM6.Type='A' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " & _
             "+case when  TaxM7.Type='A' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " & _
             "+case when  TaxM8.Type='A' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " & _
             "+case when  TaxM9.Type='A' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " & _
             "+case when  TaxM10.Type='A' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " & _
             ") as AddTaxAmt," & _
             " (case when  TaxM1.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX1_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM2.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX2_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM3.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX3_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM4.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX4_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM5.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX5_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM6.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX6_Amt else 0 end),0)else 0 end " & _
             " +case when  TaxM7.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX7_Amt else 0 end),0)else 0 end " & _
             " +case when  TaxM8.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX8_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM9.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX9_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM10.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX10_Amt else 0 end),0)else 0 end " & _
             " ) as VatTaxAmt," & _
             "(case when  TaxM1.Type='O' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " & _
             "+case when  TaxM2.Type='O' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " & _
             "+case when  TaxM3.Type='O' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " & _
             "+case when  TaxM4.Type='O' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " & _
             "+case when  TaxM5.Type='O' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " & _
             "+case when  TaxM6.Type='O' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " & _
             "+case when  TaxM7.Type='O' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " & _
             "+case when  TaxM8.Type='O' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " & _
             " +case when  TaxM9.Type='O' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " & _
             "+case when  TaxM10.Type='O' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " & _
             ") as OtherTaxAmt, " & _
             "(case when  TaxM1.Type='C' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end  " & _
             "+case when  TaxM2.Type='C' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " & _
             "+case when  TaxM3.Type='C' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " & _
             "+case when  TaxM4.Type='C' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " & _
             "+case when  TaxM5.Type='C' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " & _
             "+case when  TaxM6.Type='C' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " & _
             "+case when  TaxM7.Type='C' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " & _
             "+case when  TaxM8.Type='C' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " & _
             "+case when  TaxM9.Type='C' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " & _
             "+case when  TaxM10.Type='C' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " & _
             ") as CSTTaxAmt, " & _
             "(case when  TaxM1.Type='E' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end  " & _
             ") as ExciseAmt, " & _
             "(case when  TaxM2.Type='E' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end  " & _
             ") as ECessAmt, " & _
             "(case when  TaxM3.Type='E' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end  " & _
             ") as HCessAmt, " & _
             "case when coalesce(Scheme_Item,'N')='Y'  then ( Total_Basic_Amt +  (case when  TaxM1.Type='E' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end  " & _
             ") + (case when  TaxM2.Type='E' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end  " & _
             ") + (case when  TaxM3.Type='E' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end  " & _
             ") ) else 0 end as InvoiceAmt " & _
             " from ( " & strSql & " ) xxxxx " & _
             "left outer join TSPL_TAX_MASTER as TaxM1 on TaxM1.Tax_Code=xxxxx.TAX1 " & _
             "left outer join TSPL_TAX_MASTER as TaxM2 on TaxM2.Tax_Code=xxxxx.TAX2 " & _
             "left outer join TSPL_TAX_MASTER as TaxM3 on TaxM3.Tax_Code=xxxxx.TAX3 " & _
             "left outer join TSPL_TAX_MASTER as TaxM4 on TaxM4.Tax_Code=xxxxx.TAX4 " & _
             "left outer join TSPL_TAX_MASTER as TaxM5 on TaxM5.Tax_Code=xxxxx.TAX5 " & _
             "left outer join TSPL_TAX_MASTER as TaxM6 on TaxM6.Tax_Code=xxxxx.TAX6 " & _
             "left outer join TSPL_TAX_MASTER as TaxM7 on TaxM7.Tax_Code=xxxxx.TAX7 " & _
             "left outer join TSPL_TAX_MASTER as TaxM8 on TaxM8.Tax_Code=xxxxx.TAX8 " & _
             "left outer join TSPL_TAX_MASTER as TaxM9 on TaxM9.Tax_Code=xxxxx.TAX9 " & _
             "left outer join TSPL_TAX_MASTER as TaxM10 on TaxM10.Tax_Code=xxxxx.TAX10 "

            'Dim ArrDBName As ArrayList = Nothing
            'If rbtnCompanyAll.IsChecked Then
            '    ArrDBName = cbgCompany.AllValue
            'Else
            '    ArrDBName = cbgCompany.CheckedValue
            'End If
            'strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strSql, ArrDBName, False)
            strQuery = strSql
            Dim tmp_strQuery As String = strMain & "( " & strQuery & ") a "
            strQuery = strMain & "( " & strQuery & ") a "

            '*****************new catrgory/subcategory************************inclded query
            strQuery = "select asss1.* from (select TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code as [MainGroupCode],  TSPL_ITEM_CATEGORY_LEVEL.description as [Main Group],TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values as [GroupCode], TSPL_ITEM_CATEGORY_LEVEL_VALUES.description as [Group Name],aaa.* from (" + strQuery + ")aaa left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=aaa.Item_Code left outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=TSPL_ITEM_MASTER.Item_Code  LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values)asss1"

            Dim qry As String = strQuery

            Dim whrcate As String = ""

            If rbtnCategorySelect.IsChecked Then
                Dim isFirstTime As Boolean = True
                strQuery += " where exists (select 1  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from (" + qry + ")aaa1) and ( " + Environment.NewLine
                For Each Ctr As RadTreeNode In tvCategory.CheckedNodes
                    If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
                        If Not isFirstTime Then
                            strQuery += " or "
                            whrcate += " or "
                        End If
                        strQuery += " ( maingroupcode='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and groupcode='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        whrcate += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        isFirstTime = False
                    End If
                Next
                strQuery += " ))"
                If isFirstTime Then
                    Throw New Exception("Please select at least one Category")
                End If
            End If

            If clsCommon.myLen(whrcate) > 0 Then
                whrcate = " and " + whrcate
            End If
            strQuery = "select distinct ttt.Discount_amt,ttt.scheme_item,ttt.invoiceqty,ttt.excisable,ttt.location_code,ttt.location_desc,ttt.route_no,ttt.route_desc,ttt.type,ttt.sale_invoice_no,ttt.docdate,ttt.transfer_no,ttt.transfer_date,ttt.ParentCode,ttt.customer_code,ttt.customer_name,ttt.item_code,ttt.micode,ttt.miunit,ttt.mainqty,ttt.unit_code,ttt.mrp,ttt.mrpbottle,ttt.tp,ttt.[gross qty],ttt.[qty disc],ttt.[net qty],ttt.basic_rate,ttt.[total basic amt],ttt.[excise amt],ttt.[cess amt],ttt.[hcess amt],ttt.excise,ttt.cess,ttt.hcess,ttt.focamt,ttt.invoiceamt,ttt.disc,ttt.cst,ttt.vat,ttt.[other tax],ttt.docamt,ttt.dvat,ttt.[tpt rate],ttt.[t.rate],ttt.margin,ttt.[t.price],ttt.[tpt amt],ttt.[t.rate amt],ttt.[total mrp],ttt.[t.price amt],ttt.commamt,ttt.[sale account amt],ttt.exciseamt,ttt.total_cust_discount,ttt.adjustment_amount,ttt.doctype,( select distinct (select ','+TSPL_ITEM_CATEGORY_LEVEL.description, ','+TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Item_code=ttt.Item_Code " + whrcate + " for XML path(''))) as Category from (" + strQuery + ")ttt"
            tmp_strQuery = strQuery

            strQuery = strQuery + " ORDER BY CONVERT(DATE, DocDate,103) "



            '***************************************************************************

            If rdbItemSummary.IsChecked = True Then
                strQuery = tmp_strQuery
                strQuery = "select max(Type) AS Type,Item_Code,mrp,mrpBottle,SUM([Gross Qty]) as [Gross Qty]," & _
                "SUM([Qty Disc]) as [Qty Disc],sum([Net Qty]) as [Net Qty], " & _
                "SUM([Total Basic Amt]) as [Total Basic Amt], " & _
                "SUM(Exciseamt) as [Excise Amt]," & _
                "SUM([Cess Amt]) as [Cess Amt], " & _
                "SUM([Hcess Amt]) as [Hcess Amt], " & _
                "SUM(FOCAMt) as FOCAMt, " & _
                "SUM(InvoiceAmt) as InvoiceAmt, " & _
                "SUM(DISC) as DISC, " & _
                "SUM([CST]) as [CST], " & _
                "SUM([VAT]) as [VAT], " & _
                "SUM([Other Tax]) as [Other Tax], " & _
                "SUM([DocAmt]) as DocAmt, " & _
                "SUM([TPT Amt]) as [TPT Amt], " & _
                "SUM([T.Rate Amt]) as [T.Rate Amt], " & _
                "SUM([Total MRP])  as [Total MRP], " & _
                "SUM([Margin]) as [T.Margin], " & _
                "SUM([T.Price Amt]) as [T.Price Amt], " & _
                "SUM(COMMAmt) as COMMAmt, " & _
                "sum([Sale Account Amt]) as [Sale Account Amt], " & _
                "sum(Exciseamt) as Exciseamt, " & _
                "sum(Total_Cust_Discount) as Total_Cust_Discount,Category from ( " & tmp_strQuery & " )b group by Item_Code,mrp,mrpBottle,Category"

            ElseIf rdbDocSummary.IsChecked = True Then
                strQuery = tmp_strQuery

                strQuery = "select max(Type) AS Type,MAX(Location_Code) as Location_Code,MAX(Location_Desc) as Location_Desc, " & _
                "MAX(b.Route_No) as Route_No,MAX(b.Route_Desc) as Route_Desc, " & _
                "max(Type) AS Type,Sale_Invoice_No,max(CONVERT(varchar, DocDate,103)) as DocDate,'' as Transfer_No,Transfer_Date,MAX(ParentCode) as ParentCode,MAX(Parent_Master.Customer_Name) as ParentName,Customer_Code,max(b.Customer_Name) as Customer_Name,SUM([Gross Qty]) as [Gross Qty]," & _
                "SUM([Qty Disc]) as [Qty Disc], sum([Net Qty]) as [Net Qty]," & _
                "SUM([Total Basic Amt]) as [Total Basic Amt], " & _
                "SUM([Excise Amt]) as [Excise Amt], " & _
                "SUM([Cess Amt]) as [Cess Amt], " & _
                "SUM([Hcess Amt]) as [Hcess Amt], " & _
                "SUM(FOCAMt) as FOCAMt, " & _
                "SUM(InvoiceAmt) as InvoiceAmt, " & _
                "SUM(DISC) as DISC, " & _
                "SUM(b.[CST]) as [CST], " & _
                "SUM([VAT]) as [VAT], " & _
                "SUM([Other Tax]) as [Other Tax], " & _
                "case when max(Discount_Amt)=100 then 0 else sum(DocAmt) end as DocAmt, " & _
                "SUM([TPT Amt]) as [TPT Amt], " & _
                "SUM([T.Rate Amt]) as [T.Rate Amt], " & _
                "SUM([Total MRP])  as [Total MRP], " & _
                "SUM([Margin]) as [T.Margin], " & _
                "SUM([T.Price Amt]) as [T.Price Amt], " & _
                "SUM(COMMAmt) as COMMAmt, " & _
                "sum([Sale Account Amt]) as [Sale Account Amt], " & _
                "sum(Exciseamt) as Exciseamt, " & _
                "SUM(Adjustment_Amount) as Adjustment_Amount, " & _
                "sum(Total_Cust_Discount) as Total_Cust_Discount,DocType from ( " & tmp_strQuery & " )b  left outer join TSPL_CUSTOMER_MASTER as Parent_Master on Parent_Master.Cust_Code=b.ParentCode group by Excisable,Sale_Invoice_No,Transfer_No,DocType,Customer_Code,Transfer_Date ORDER BY MAX(CONVERT(DATE, DocDate,103))  "
            End If
            dt = clsDBFuncationality.GetDataTable(strQuery)

            If rdbDocSummary.IsChecked AndAlso pnlAdminSetting.Visible AndAlso chkReconcile.Checked Then
                Dim strComponent As String = ""
                Dim strColName As String = ""
                If rbtnSaleAccount.IsChecked Then
                    strComponent = clsRecoSettingReportComponent.SaleRecoChartSaleAccount
                    strColName = "[Sale Account Amt]"
                ElseIf rbtnExciseAccount.IsChecked Then
                    strComponent = clsRecoSettingReportComponent.SaleRecoChartExciseAmount
                    strColName = "[Excise Amt]"
                ElseIf rbtnECessAccount.IsChecked Then
                    strComponent = clsRecoSettingReportComponent.SaleRecoChartECess
                    strColName = "[Cess Amt]"
                ElseIf rbtnHCessAccount.IsChecked Then
                    strComponent = clsRecoSettingReportComponent.SaleRecoChartHCess
                    strColName = "[Hcess Amt]"
                ElseIf rbtnTPT.IsChecked Then
                    strComponent = clsRecoSettingReportComponent.SaleRecoChartTPT
                    strColName = "[TPT Amt]"
                Else
                    Throw New Exception("Not a Valid Option for Reconcilation")
                End If


                Dim dtAS As DataTable = clsReconciliationSetting.GetAccounts(clsRecoSettingReportName.SaleRecoChart, strComponent, dtpFdate.Value, dtpToDate.Value)
                Dim arr As Dictionary(Of String, clsTempDrCrAmt) = Nothing
                dt.Columns.Add("SubledgerAmt", GetType(Double))
                If dtAS IsNot Nothing AndAlso dtAS.Rows.Count > 0 Then
                    arr = New Dictionary(Of String, clsTempDrCrAmt)
                    For Each dr As DataRow In dtAS.Rows
                        Dim obj As clsTempDrCrAmt = New clsTempDrCrAmt()
                        obj.DrAmt = -1 * clsCommon.myCdbl(dr("SubledgerAmt"))
                        arr.Add(clsCommon.myCstr(clsCommon.myCstr(dr("docNo")) + clsCommon.myCstr(dr("DocType"))).ToUpper(), obj)
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        Dim strSourceDocNo As String = clsCommon.myCstr(clsCommon.myCstr(dt.Rows(ii)("Document_Code")) + clsCommon.myCstr(dt.Rows(ii)("DocType"))).ToUpper()
                        If arr.ContainsKey(strSourceDocNo) Then
                            dt.Rows(ii)("SubledgerAmt") = clsCommon.myCdbl(arr.Item(strSourceDocNo).DrAmt)
                        End If
                    Next
                End If
                If chkMismatch.Checked Then
                    Try
                        Dim dtView As DataView = dt.DefaultView
                        dtView.RowFilter = "  (SubledgerAmt<>" + strColName + ")"
                    Catch ex As Exception

                    End Try

                End If
            End If

            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                gv1.DataSource = dt
                SetGridFormationOFGV1()
                ReStoreGridLayout()
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub




    Sub SetGridFormationOFGV1()
        ' Dim strItemCode, head2 As String

        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next

        If rdbDetail.IsChecked Then
            gv1.Columns("Location_Code").IsVisible = True
            gv1.Columns("Location_Code").Width = 80
            gv1.Columns("Location_Code").HeaderText = "Location"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 80
            gv1.Columns("Location_Desc").HeaderText = "Location Name"

            gv1.Columns("Route_No").IsVisible = True
            gv1.Columns("Route_No").Width = 80
            gv1.Columns("Route_No").HeaderText = "Route No"

            gv1.Columns("Route_Desc").IsVisible = True
            gv1.Columns("Route_Desc").Width = 80
            gv1.Columns("Route_Desc").HeaderText = "Route Desc"

            gv1.Columns("Transfer_Date").IsVisible = False
            gv1.Columns("Transfer_Date").Width = 80
            gv1.Columns("Transfer_Date").HeaderText = "Transfer Date"


            gv1.Columns("Adjustment_Amount").IsVisible = False
            gv1.Columns("Adjustment_Amount").Width = 80
            gv1.Columns("Adjustment_Amount").HeaderText = "Settlement Amount"


            gv1.Columns("Type").IsVisible = True
            gv1.Columns("Type").Width = 80
            gv1.Columns("Type").HeaderText = "Type"

            gv1.Columns("Sale_Invoice_No").IsVisible = True
            gv1.Columns("Sale_Invoice_No").Width = 80
            gv1.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

            gv1.Columns("DocDate").IsVisible = True
            gv1.Columns("DocDate").Width = 100
            gv1.Columns("DocDate").HeaderText = "Document Date"

            gv1.Columns("DocAmt").IsVisible = True
            gv1.Columns("DocAmt").Width = 100
            gv1.Columns("DocAmt").HeaderText = "Document Amount"

            gv1.Columns("Transfer_No").IsVisible = False
            gv1.Columns("Transfer_No").Width = 80
            gv1.Columns("Transfer_No").HeaderText = "Transfer No"

            gv1.Columns("ParentCode").IsVisible = True
            gv1.Columns("ParentCode").Width = 80
            gv1.Columns("ParentCode").HeaderText = "Parent Customer Code"

            gv1.Columns("ParentName").IsVisible = True
            gv1.Columns("ParentName").Width = 80
            gv1.Columns("ParentName").HeaderText = "Parent Name"


            gv1.Columns("Customer_Code").IsVisible = True
            gv1.Columns("Customer_Code").Width = 80
            gv1.Columns("Customer_Code").HeaderText = "Customer Code"

            gv1.Columns("Customer_Name").IsVisible = True
            gv1.Columns("Customer_Name").Width = 80
            gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 80
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Unit_Code").IsVisible = True
            gv1.Columns("Unit_Code").Width = 50
            gv1.Columns("Unit_Code").HeaderText = "Unit Code"

            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 70
            gv1.Columns("MRP").HeaderText = "MRP"

            gv1.Columns("MRPBottle").IsVisible = False
            gv1.Columns("MRPBottle").Width = 70
            gv1.Columns("MRPBottle").HeaderText = "MRPBottle"

            gv1.Columns("TP").IsVisible = False
            gv1.Columns("TP").Width = 70
            gv1.Columns("TP").HeaderText = "TP"

            gv1.Columns("Basic_Rate").IsVisible = True
            gv1.Columns("Basic_Rate").Width = 100
            gv1.Columns("Basic_Rate").HeaderText = "Basic Rate"

            gv1.Columns("Excise").IsVisible = False
            gv1.Columns("Excise").Width = 100
            gv1.Columns("Excise").HeaderText = "Excise"

            gv1.Columns("Cess").IsVisible = False
            gv1.Columns("Cess").Width = 80
            gv1.Columns("Cess").HeaderText = "Cess"

            gv1.Columns("Hcess").IsVisible = False
            gv1.Columns("Hcess").Width = 80
            gv1.Columns("Hcess").HeaderText = "Hcess"

            gv1.Columns("DVAT").IsVisible = False
            gv1.Columns("DVAT").Width = 80
            gv1.Columns("DVAT").HeaderText = "Tax"

            gv1.Columns("TPT Rate").IsVisible = False
            gv1.Columns("TPT Rate").Width = 80
            gv1.Columns("TPT Rate").HeaderText = "TPT Rate"

            gv1.Columns("T.Rate").IsVisible = False
            gv1.Columns("T.Rate").Width = 80
            gv1.Columns("T.Rate").HeaderText = "T.Rate"

            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 80
            gv1.Columns("MRP").HeaderText = "MRP"

            gv1.Columns("MRPBottle").IsVisible = False
            gv1.Columns("MRPBottle").Width = 70
            gv1.Columns("MRPBottle").HeaderText = "MRPBottle"


            gv1.Columns("Margin").IsVisible = False
            gv1.Columns("Margin").Width = 80
            gv1.Columns("Margin").HeaderText = "Margin"

            gv1.Columns("T.Price").IsVisible = False
            gv1.Columns("T.Price").Width = 80
            gv1.Columns("T.Price").HeaderText = "T.Price"

            gv1.Columns("Gross Qty").IsVisible = True
            gv1.Columns("Gross Qty").Width = 80
            gv1.Columns("Gross Qty").HeaderText = "Gross Qty"

            gv1.Columns("Net Qty").IsVisible = True
            gv1.Columns("Net Qty").Width = 80
            gv1.Columns("Net Qty").HeaderText = "Net Qty"

            gv1.Columns("Qty Disc").IsVisible = True
            gv1.Columns("Qty Disc").Width = 80
            gv1.Columns("Qty Disc").HeaderText = "Qty Disc"

            'If rdbInvoice.Checked = True Then
            gv1.Columns("FOCAMt").IsVisible = True
            gv1.Columns("FOCAMt").Width = 80
            gv1.Columns("FOCAMt").HeaderText = "Trade Discount Amt"

            gv1.Columns("InvoiceAmt").IsVisible = False
            gv1.Columns("InvoiceAmt").Width = 80
            gv1.Columns("InvoiceAmt").HeaderText = "Trade Discount Amt"
            'Else
            '    gv1.Columns("FOCAMt").IsVisible = True
            '    gv1.Columns("FOCAMt").Width = 80
            '    gv1.Columns("FOCAMt").HeaderText = "Trade Discount Amt"

            '    gv1.Columns("InvoiceAmt").IsVisible = False
            '    gv1.Columns("InvoiceAmt").Width = 80
            '    gv1.Columns("InvoiceAmt").HeaderText = "Trade Discount Amt"
            'End If


            gv1.Columns("Total Basic Amt").IsVisible = True
            gv1.Columns("Total Basic Amt").Width = 80
            gv1.Columns("Total Basic Amt").HeaderText = "Basic Amount"

            gv1.Columns("Excise Amt").IsVisible = True
            gv1.Columns("Excise Amt").Width = 80
            gv1.Columns("Excise Amt").HeaderText = "Excise Amt"

            gv1.Columns("Cess Amt").IsVisible = True
            gv1.Columns("Cess Amt").Width = 80
            gv1.Columns("Cess Amt").HeaderText = "Cess Amt"

            gv1.Columns("Hcess Amt").IsVisible = True
            gv1.Columns("Hcess Amt").Width = 80
            gv1.Columns("Hcess Amt").HeaderText = "Hcess Amt"

            'gv1.Columns("DVAT Amt").IsVisible = True
            'gv1.Columns("DVAT Amt").Width = 80
            'gv1.Columns("DVAT Amt").HeaderText = "Tax Amt"

            gv1.Columns("CST").IsVisible = True
            gv1.Columns("CST").Width = 80
            gv1.Columns("CST").HeaderText = "CST"

            gv1.Columns("VAT").IsVisible = True
            gv1.Columns("VAT").Width = 80
            gv1.Columns("VAT").HeaderText = "VAT"

            gv1.Columns("Other Tax").IsVisible = True
            gv1.Columns("Other Tax").Width = 80
            gv1.Columns("Other Tax").HeaderText = "Other Tax"


            gv1.Columns("TPT Amt").IsVisible = False
            gv1.Columns("TPT Amt").Width = 80
            gv1.Columns("TPT Amt").HeaderText = "TPT Amt"

            gv1.Columns("T.Rate Amt").IsVisible = False
            gv1.Columns("T.Rate Amt").Width = 80
            gv1.Columns("T.Rate Amt").HeaderText = "T.Rate Amt"

            gv1.Columns("Total MRP").IsVisible = False
            gv1.Columns("Total MRP").Width = 80
            gv1.Columns("Total MRP").HeaderText = "Total MRP"

            gv1.Columns("T.Margin").IsVisible = False
            gv1.Columns("T.Margin").Width = 80
            gv1.Columns("T.Margin").HeaderText = "T.Margin"

            gv1.Columns("T.Price Amt").IsVisible = False
            gv1.Columns("T.Price Amt").Width = 80
            gv1.Columns("T.Price Amt").HeaderText = "T.Price Amt"

            gv1.Columns("COMMAmt").IsVisible = False
            gv1.Columns("COMMAmt").Width = 80
            gv1.Columns("COMMAmt").HeaderText = "COMMAmt"

            gv1.Columns("DISC").IsVisible = True
            gv1.Columns("DISC").Width = 80
            gv1.Columns("DISC").HeaderText = "Discount"

            gv1.Columns("Sale Account Amt").IsVisible = True
            gv1.Columns("Sale Account Amt").Width = 80
            gv1.Columns("Sale Account Amt").HeaderText = "Sale Account Amt"

            gv1.Columns("Exciseamt").IsVisible = False
            gv1.Columns("Exciseamt").Width = 80
            gv1.Columns("Exciseamt").HeaderText = "Excise Recovered Amount"

            gv1.Columns("Total_Cust_Discount").IsVisible = False
            gv1.Columns("Total_Cust_Discount").Width = 80
            gv1.Columns("Total_Cust_Discount").HeaderText = "Customer Disc"

            gv1.Columns("MICode").IsVisible = True
            gv1.Columns("MICode").Width = 80
            gv1.Columns("MICode").HeaderText = "Main Item"


            gv1.Columns("MIUnit").IsVisible = True
            gv1.Columns("MIUnit").Width = 80
            gv1.Columns("MIUnit").HeaderText = "Main Unit"


            gv1.Columns("mainqty").IsVisible = True
            gv1.Columns("mainqty").Width = 80
            gv1.Columns("mainqty").HeaderText = "Main Qty"

            'gv1.Columns("InvoiceAmt").IsVisible = True
            'gv1.Columns("InvoiceAmt").Width = 80
            'gv1.Columns("InvoiceAmt").HeaderText = "InvoiceAmt"

            Try
                gv1.Columns("Category").IsVisible = True
                gv1.Columns("Category").Width = 220
                gv1.Columns("Category").HeaderText = "Item Category"
            Catch ex As Exception
            End Try

        ElseIf rdbDocSummary.IsChecked Then

            gv1.Columns("Type").IsVisible = True
            gv1.Columns("Type").Width = 80
            gv1.Columns("Type").HeaderText = "Type"

            gv1.Columns("Location_Code").IsVisible = True
            gv1.Columns("Location_Code").Width = 80
            gv1.Columns("Location_Code").HeaderText = "Location"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 80
            gv1.Columns("Location_Desc").HeaderText = "Location Name"

            gv1.Columns("Route_No").IsVisible = True
            gv1.Columns("Route_No").Width = 80
            gv1.Columns("Route_No").HeaderText = "Route No"

            gv1.Columns("Route_Desc").IsVisible = True
            gv1.Columns("Route_Desc").Width = 80
            gv1.Columns("Route_Desc").HeaderText = "Route Desc"

            gv1.Columns("Transfer_Date").IsVisible = False
            gv1.Columns("Transfer_Date").Width = 80
            gv1.Columns("Transfer_Date").HeaderText = "Transfer Date"


            gv1.Columns("Transfer_No").IsVisible = False
            gv1.Columns("Transfer_No").Width = 80
            gv1.Columns("Transfer_No").HeaderText = "Transfer No"

            gv1.Columns("Adjustment_Amount").IsVisible = False
            gv1.Columns("Adjustment_Amount").Width = 80
            gv1.Columns("Adjustment_Amount").HeaderText = "Settlement Amount"

            gv1.Columns("Sale_Invoice_No").IsVisible = True
            gv1.Columns("Sale_Invoice_No").Width = 80
            gv1.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

            gv1.Columns("DocDate").IsVisible = True
            gv1.Columns("DocDate").Width = 100
            gv1.Columns("DocDate").HeaderText = "Document Date"

            gv1.Columns("DocAmt").IsVisible = True
            gv1.Columns("DocAmt").Width = 100
            gv1.Columns("DocAmt").HeaderText = "Document Amount"

            gv1.Columns("ParentCode").IsVisible = True
            gv1.Columns("ParentCode").Width = 80
            gv1.Columns("ParentCode").HeaderText = "Parent Customer Code"

            gv1.Columns("ParentName").IsVisible = True
            gv1.Columns("ParentName").Width = 80
            gv1.Columns("ParentName").HeaderText = "Parent Name"


            gv1.Columns("Customer_Code").IsVisible = True
            gv1.Columns("Customer_Code").Width = 80
            gv1.Columns("Customer_Code").HeaderText = "Customer Code"

            gv1.Columns("Customer_Name").IsVisible = True
            gv1.Columns("Customer_Name").Width = 80
            gv1.Columns("Customer_Name").HeaderText = "Customer Name"


            gv1.Columns("Gross Qty").IsVisible = True
            gv1.Columns("Gross Qty").Width = 80
            gv1.Columns("Gross Qty").HeaderText = "Gross Qty"

            gv1.Columns("Net Qty").IsVisible = True
            gv1.Columns("Net Qty").Width = 80
            gv1.Columns("Net Qty").HeaderText = "Net Qty"

            gv1.Columns("Qty Disc").IsVisible = True
            gv1.Columns("Qty Disc").Width = 80
            gv1.Columns("Qty Disc").HeaderText = "Qty Disc"

            'If rdbInvoice.Checked = True Then
            gv1.Columns("FOCAMt").IsVisible = True
            gv1.Columns("FOCAMt").Width = 80
            gv1.Columns("FOCAMt").HeaderText = "Trade Discount Amt"

            gv1.Columns("InvoiceAmt").IsVisible = False
            gv1.Columns("InvoiceAmt").Width = 80
            gv1.Columns("InvoiceAmt").HeaderText = "Trade Discount Amt"
            'Else
            '    gv1.Columns("FOCAMt").IsVisible = True
            '    gv1.Columns("FOCAMt").Width = 80
            '    gv1.Columns("FOCAMt").HeaderText = "Trade Discount Amt"

            '    gv1.Columns("InvoiceAmt").IsVisible = False
            '    gv1.Columns("InvoiceAmt").Width = 80
            '    gv1.Columns("InvoiceAmt").HeaderText = "Trade Discount Amt"
            'End If

            gv1.Columns("Total Basic Amt").IsVisible = True
            gv1.Columns("Total Basic Amt").Width = 80
            gv1.Columns("Total Basic Amt").HeaderText = "Basic Amount"

            gv1.Columns("Excise Amt").IsVisible = True
            gv1.Columns("Excise Amt").Width = 80
            gv1.Columns("Excise Amt").HeaderText = "Excise Amt"

            gv1.Columns("Cess Amt").IsVisible = True
            gv1.Columns("Cess Amt").Width = 80
            gv1.Columns("Cess Amt").HeaderText = "Cess Amt"

            gv1.Columns("Hcess Amt").IsVisible = True
            gv1.Columns("Hcess Amt").Width = 80
            gv1.Columns("Hcess Amt").HeaderText = "Hcess Amt"

            'gv1.Columns("DVAT Amt").IsVisible = True
            'gv1.Columns("DVAT Amt").Width = 80
            'gv1.Columns("DVAT Amt").HeaderText = "Tax Amt"

            gv1.Columns("CST").IsVisible = True
            gv1.Columns("CST").Width = 80
            gv1.Columns("CST").HeaderText = "CST"

            gv1.Columns("VAT").IsVisible = True
            gv1.Columns("VAT").Width = 80
            gv1.Columns("VAT").HeaderText = "VAT"

            gv1.Columns("Other Tax").IsVisible = True
            gv1.Columns("Other Tax").Width = 80
            gv1.Columns("Other Tax").HeaderText = "Other Tax"

            gv1.Columns("TPT Amt").IsVisible = False
            gv1.Columns("TPT Amt").Width = 80
            gv1.Columns("TPT Amt").HeaderText = "TPT Amt"

            gv1.Columns("T.Rate Amt").IsVisible = False
            gv1.Columns("T.Rate Amt").Width = 80
            gv1.Columns("T.Rate Amt").HeaderText = "T.Rate Amt"

            gv1.Columns("Total MRP").IsVisible = False
            gv1.Columns("Total MRP").Width = 80
            gv1.Columns("Total MRP").HeaderText = "Total MRP"

            gv1.Columns("T.Margin").IsVisible = False
            gv1.Columns("T.Margin").Width = 80
            gv1.Columns("T.Margin").HeaderText = "T.Margin"

            gv1.Columns("T.Price Amt").IsVisible = False
            gv1.Columns("T.Price Amt").Width = 80
            gv1.Columns("T.Price Amt").HeaderText = "T.Price Amt"

            gv1.Columns("COMMAmt").IsVisible = False
            gv1.Columns("COMMAmt").Width = 80
            gv1.Columns("COMMAmt").HeaderText = "COMMAmt"

            gv1.Columns("DISC").IsVisible = True
            gv1.Columns("DISC").Width = 80
            gv1.Columns("DISC").HeaderText = "DISC"

            gv1.Columns("Sale Account Amt").IsVisible = True
            gv1.Columns("Sale Account Amt").Width = 80
            gv1.Columns("Sale Account Amt").HeaderText = "Sale Account Amt"

            gv1.Columns("Exciseamt").IsVisible = False
            gv1.Columns("Exciseamt").Width = 80
            gv1.Columns("Exciseamt").HeaderText = "Excise Recovered Amount"


            gv1.Columns("Total_Cust_Discount").IsVisible = False
            gv1.Columns("Total_Cust_Discount").Width = 80
            gv1.Columns("Total_Cust_Discount").HeaderText = "Customer Disc"

            Try
                gv1.Columns("Category").IsVisible = True
                gv1.Columns("Category").Width = 220
                gv1.Columns("Category").HeaderText = "Item Category"
            Catch ex As Exception
            End Try

        ElseIf rdbItemSummary.IsChecked Then
            'gv1.Columns("Sale_Invoice_No").IsVisible = True
            'gv1.Columns("Sale_Invoice_No").Width = 80
            'gv1.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 80
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            'gv1.Columns("Unit_Code").IsVisible = True
            'gv1.Columns("Unit_Code").Width = 50
            'gv1.Columns("Unit_Code").HeaderText = "Unit Code"

            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 70
            gv1.Columns("MRP").HeaderText = "MRP"

            gv1.Columns("MRPBottle").IsVisible = False
            gv1.Columns("MRPBottle").Width = 70
            gv1.Columns("MRPBottle").HeaderText = "MRPBottle"

            'gv1.Columns("TP").IsVisible = False
            'gv1.Columns("TP").Width = 70
            'gv1.Columns("TP").HeaderText = "TP"

            gv1.Columns("DocAmt").IsVisible = True
            gv1.Columns("DocAmt").Width = 100
            gv1.Columns("DocAmt").HeaderText = "Document Amount"


            gv1.Columns("Gross Qty").IsVisible = True
            gv1.Columns("Gross Qty").Width = 80
            gv1.Columns("Gross Qty").HeaderText = "Gross Qty"

            gv1.Columns("Net Qty").IsVisible = True
            gv1.Columns("Net Qty").Width = 80
            gv1.Columns("Net Qty").HeaderText = "Net Qty"

            gv1.Columns("Qty Disc").IsVisible = True
            gv1.Columns("Qty Disc").Width = 80
            gv1.Columns("Qty Disc").HeaderText = "Qty Disc"

            'If rdbInvoice.Checked = True Then
            gv1.Columns("FOCAMt").IsVisible = True
            gv1.Columns("FOCAMt").Width = 80
            gv1.Columns("FOCAMt").HeaderText = "Trade Discount Amt"

            gv1.Columns("InvoiceAmt").IsVisible = False
            gv1.Columns("InvoiceAmt").Width = 80
            gv1.Columns("InvoiceAmt").HeaderText = "Trade Discount Amt"
            'Else
            '    gv1.Columns("FOCAMt").IsVisible = True
            '    gv1.Columns("FOCAMt").Width = 80
            '    gv1.Columns("FOCAMt").HeaderText = "Trade Discount Amt"

            '    gv1.Columns("InvoiceAmt").IsVisible = False
            '    gv1.Columns("InvoiceAmt").Width = 80
            '    gv1.Columns("InvoiceAmt").HeaderText = "Trade Discount Amt"
            'End If

            gv1.Columns("Total Basic Amt").IsVisible = True
            gv1.Columns("Total Basic Amt").Width = 80
            gv1.Columns("Total Basic Amt").HeaderText = "Basic Amount"

            gv1.Columns("Excise Amt").IsVisible = True
            gv1.Columns("Excise Amt").Width = 80
            gv1.Columns("Excise Amt").HeaderText = "Excise Amt"

            gv1.Columns("Cess Amt").IsVisible = True
            gv1.Columns("Cess Amt").Width = 80
            gv1.Columns("Cess Amt").HeaderText = "Cess Amt"

            gv1.Columns("Hcess Amt").IsVisible = True
            gv1.Columns("Hcess Amt").Width = 80
            gv1.Columns("Hcess Amt").HeaderText = "Hcess Amt"

            'gv1.Columns("DVAT Amt").IsVisible = True
            'gv1.Columns("DVAT Amt").Width = 80
            'gv1.Columns("DVAT Amt").HeaderText = "Tax Amt"

            gv1.Columns("CST").IsVisible = True
            gv1.Columns("CST").Width = 80
            gv1.Columns("CST").HeaderText = "CST"

            gv1.Columns("VAT").IsVisible = True
            gv1.Columns("VAT").Width = 80
            gv1.Columns("VAT").HeaderText = "VAT"

            gv1.Columns("Other Tax").IsVisible = True
            gv1.Columns("Other Tax").Width = 80
            gv1.Columns("Other Tax").HeaderText = "Other Tax"

            gv1.Columns("TPT Amt").IsVisible = False
            gv1.Columns("TPT Amt").Width = 80
            gv1.Columns("TPT Amt").HeaderText = "TPT Amt"

            gv1.Columns("T.Rate Amt").IsVisible = False
            gv1.Columns("T.Rate Amt").Width = 80
            gv1.Columns("T.Rate Amt").HeaderText = "T.Rate Amt"

            gv1.Columns("Total MRP").IsVisible = False
            gv1.Columns("Total MRP").Width = 80
            gv1.Columns("Total MRP").HeaderText = "Total MRP"

            gv1.Columns("T.Margin").IsVisible = False
            gv1.Columns("T.Margin").Width = 80
            gv1.Columns("T.Margin").HeaderText = "T.Margin"

            gv1.Columns("T.Price Amt").IsVisible = False
            gv1.Columns("T.Price Amt").Width = 80
            gv1.Columns("T.Price Amt").HeaderText = "T.Price Amt"

            gv1.Columns("COMMAmt").IsVisible = False
            gv1.Columns("COMMAmt").Width = 80
            gv1.Columns("COMMAmt").HeaderText = "COMMAmt"

            gv1.Columns("DISC").IsVisible = True
            gv1.Columns("DISC").Width = 80
            gv1.Columns("DISC").HeaderText = "Discount"

            gv1.Columns("Sale Account Amt").IsVisible = True
            gv1.Columns("Sale Account Amt").Width = 80
            gv1.Columns("Sale Account Amt").HeaderText = "Sale Account Amt"

            gv1.Columns("Total_Cust_Discount").IsVisible = False
            gv1.Columns("Total_Cust_Discount").Width = 80
            gv1.Columns("Total_Cust_Discount").HeaderText = "Customer Disc"

            gv1.Columns("Exciseamt").IsVisible = False
            gv1.Columns("Exciseamt").Width = 80
            gv1.Columns("Exciseamt").HeaderText = "Excise Recovered Amount"

            Try
                gv1.Columns("Category").IsVisible = True
                gv1.Columns("Category").Width = 220
                gv1.Columns("Category").HeaderText = "Item Category"
            Catch ex As Exception
            End Try

        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Gross Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Net Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Qty Disc", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Total Basic Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Excise Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Cess Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Hcess Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        'Dim item8 As New GridViewSummaryItem("DVAT Amt", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item8)

        Dim item8 As New GridViewSummaryItem("CST", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item8a As New GridViewSummaryItem("VAT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8a)
        Dim item8b As New GridViewSummaryItem("Other Tax", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8b)

        Dim item9 As New GridViewSummaryItem("TPT Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("T.Rate Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("Total MRP", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("T.Margin", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        Dim item13 As New GridViewSummaryItem("T.Price Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("COMMAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("DISC", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)
        Dim item16 As New GridViewSummaryItem("FOCAMt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item16)
        Dim item17 As New GridViewSummaryItem("Sale Account Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item17)
        Dim item18 As New GridViewSummaryItem("Total_Cust_Discount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item18)
        Dim item20 As New GridViewSummaryItem("Exciseamt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item20)
        Dim item21 As New GridViewSummaryItem("DocAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item21)
        Dim item22 As New GridViewSummaryItem("mainqty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item22)
        Dim item23 As New GridViewSummaryItem("InvoiceAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item23)


        If rdbItemSummary.IsChecked = False Then
            Dim item19 As New GridViewSummaryItem("Adjustment_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item19)
        End If

        If rdbDocSummary.IsChecked AndAlso pnlAdminSetting.Visible AndAlso chkReconcile.Checked Then
            gv1.Columns("SubledgerAmt").IsVisible = True
            gv1.Columns("SubledgerAmt").Width = 100
            gv1.Columns("SubledgerAmt").HeaderText = "Subledger Amt"

            Dim emptydr As New GridViewSummaryItem("SubledgerAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(emptydr)


        End If

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub ExportToExcel()
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")
            arrHeader.Add(strtemp)

            'If chkHierSelect.IsChecked Then
            '    strtemp = ""
            '    For Each Str As String In cbgHier.CheckedDisplayMember
            '        If clsCommon.myLen(strtemp) > 0 Then
            '            strtemp += ", "
            '        End If
            '        strtemp += Str
            '    Next
            '    arrHeader.Add("Hierarchy : " + strtemp)
            'End If
            If chkClassSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In chkCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Salesname : " + strtemp)
            End If

            If chkLocSelect.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Location Segment : " + strtemp)
            End If


            clsCommon.MyExportToExcel("Sales Analysis Report", gv1, arrHeader, Me.Text)

            '' ''Dim strReportTitle As String
            '' ''strReportTitle = "Sale Discount"
            '' ''Dim saveDialog1 As New SaveFileDialog()
            '' ''saveDialog1.FileName = strReportTitle
            '' ''saveDialog1.Filter = "Excel Workbooks (*.xls;*.xlsx)|*.xls;*.xlsx"
            '' ''Dim Fullpath As String

            '' ''Dim path = "C:\\ERPTempFolder"
            '' ''Dim IsExists As Boolean = System.IO.Directory.Exists(path)
            '' ''If IsExists = False Then
            '' ''    System.IO.Directory.CreateDirectory(path)
            '' ''End If

            '' ''Fullpath = path + "\" + saveDialog1.FileName
            '' ''Dim i As Integer = 0
            '' ''For i = 0 To GV1.ColumnCount - 1
            '' ''    Dim grow As GridViewRowInfo = TryCast(GV1.Rows(0), GridViewRowInfo)
            '' ''    If TypeOf grow.Cells(i).Value Is DateTime Then
            '' ''        Dim datecol As GridViewDateTimeColumn = TryCast(GV1.Columns(i), GridViewDateTimeColumn)
            '' ''        datecol.ExcelExportType = DisplayFormatType.ShortDate
            '' ''    End If
            '' ''Next i
            '' ''Dim exporter As New ExportToExcelML(GV1)
            '' ''exporter.SummariesExportOption = SummariesOption.ExportAll
            ' '' ''If rdbSummary.IsChecked = True Then
            '' ''exporter.ExportVisualSettings = True
            ' '' ''End If
            '' ''exporter.ExportHierarchy = True
            '' ''exporter.HiddenColumnOption = HiddenOption.DoNotExport
            '' ''exporter.SheetMaxRows = ExcelMaxRows._1048576
            '' ''AddHandler exporter.ExcelCellFormatting, AddressOf exporter_ExcelCellFormatting
            '' ''AddHandler exporter.ExcelTableCreated, AddressOf exporter_ExcelTableCreated
            '' ''exporter.SheetName = strReportTitle
            '' ''exporter.RunExport(Fullpath)
            '' ''Me.Controls.Remove(GV1)
            '' ''Dim xlsApp As Microsoft.Office.Interop.Excel.ApplicationClass
            '' ''Dim xlsWB As Microsoft.Office.Interop.Excel.WorkbookClass
            '' ''xlsApp = New Microsoft.Office.Interop.Excel.ApplicationClass
            '' ''xlsApp.Visible = True
            '' ''xlsWB = xlsApp.Workbooks.Open(Fullpath)
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
        ' Dim strReportTitle, strConverted, strOrderby, head2, strSummary, strQty, strType As String



        If e.SheetIndex = 0 Then 'add header row only for the first excel sheet                

            Dim style1 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Sale Discount Report ")
            Dim style2 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Start Date : = " + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy"))
            Dim style3 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "End Date : = " + clsCommon.GetPrintDate(dtpFdate.Value, "dd/MM/yyyy"))

            If chkLocSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
                If strLoca = "" Then
                    strLoca = "All"
                End If
                Dim style4 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Location : " + strLoca)
            End If

            If chkClassSelect.IsChecked Then
                Dim strClass As String = ""
                For Each Str As String In chkCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strClass) > 0 Then
                        strClass += ", "
                    End If
                    strClass += Str
                Next
                If strClass = "" Then
                    strClass = "All"
                End If
                Dim style5 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Customer Type : " + strClass)

            End If

            'If chkHierSelect.IsChecked Then
            '    Dim strHier As String = ""
            '    For Each Str As String In 
            '.CheckedDisplayMember()
            '        If clsCommon.myLen(strHier) > 0 Then
            '            strHier += ", "
            '        End If
            '        strHier += Str
            '    Next
            '    If strHier = "" Then
            '        strHier = "All"
            '    End If
            '    Dim style5 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Hierarchy : " + strHier)

            'End If
        End If
    End Sub

    Sub Export()
        If gv1.Rows.Count > 0 Then
            ExportToExcel()
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Export()
    End Sub

    Private Sub chkClassAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkClassAll.ToggleStateChanged
        chkCustomer.Enabled = Not chkClassAll.IsChecked
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub


    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        gv1.EnableFiltering = True
        print()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub rdbDocSummary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbDocSummary.ToggleStateChanged

    End Sub

    Private Sub frmSalesAnalysisReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F12 Then
            If pnlAdminSetting.Visible Then
                pnlAdminSetting.Visible = False
            Else
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    pnlAdminSetting.Visible = True
                End If
            End If


        End If
    End Sub

    Private Sub chkReconcile_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkReconcile.ToggleStateChanged
        If chkReconcile.Checked Then
            rdbWoFOC.IsChecked = True
            rdbDocSummary.IsChecked = True
        End If
    End Sub

    Private Sub btSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveLayout.Click
        If clsCommon.myLen(GetReportID()) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = GetReportID()
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Public Function GetReportID() As String

        If rdbItemSummary.IsChecked Then
            Return "SAleRecoDiscounSummary"
        ElseIf rdbDocSummary.IsChecked Then
            Return "SAleRecoDocSummary"
        Else
            Return "SAleRecoDetails"
        End If


    End Function

    Private Sub btnDeleteLayour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteLayour.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(GetReportID()) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(GetReportID(), "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rdbWoFOC_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbWoFOC.ToggleStateChanged

    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv1.CellDoubleClick
        If rdbDocSummary.IsChecked Then
            If gv1.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Type").Value)
                Dim strDoc = gv1.CurrentRow.Cells("Sale_Invoice_No").Value
                If strTransType = "Sale Invoice" Then
                    strTransType = "SD-IN"
                Else
                    strTransType = "Sale Return"
                End If
                Select Case strTransType
                    Case "SD-IN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, strDoc)
                    Case "Sale Return"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleReturn, strDoc)
                End Select
            End If
        End If
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        tvCategory.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Sub cbgLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbgLocation.Load

    End Sub

    '------------------------function for drill down
    Function PrintDrillDown() As DataTable
        Dim dt As DataTable = Nothing
        Try
            LoadCategory()

            LoadLocation()

            chkLocSelect.IsChecked = False
            chkLocAll.IsChecked = True
            rbtnCategoryAll.IsChecked = True
            rbtnCategorySelect.IsChecked = False

            rdbDetail.IsChecked = False
            rdbDocSummary.IsChecked = True
            rdbItemSummary.IsChecked = False
            rdbWithFOC.IsChecked = True
            rdbWoFOC.IsChecked = False
            rdbFOC.IsChecked = False
            ddlSaleType.SelectedValue = "Sale Invoice"
            chkMismatch.Checked = True
            chkReconcile.Checked = False
            rbtnSaleAccount.IsChecked = True
            rbtnExciseAccount.IsChecked = False
            rbtnECessAccount.IsChecked = False
            rbtnHCessAccount.IsChecked = False
            rbtnTPT.IsChecked = False


            'If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select at least one Location or select ALL")
            '    'Return
            'ElseIf chkClassSelect.IsChecked = True AndAlso chkCustomer.CheckedValue.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select at least one Customer or select ALL")
            '    'Return
            'End If
            Dim strSql, strSettlement, strFOC, strLocAll, strClassAll As String
            strFOC = ""
            If chkLocAll.IsChecked = True Then
                strLocAll = "Y"
            Else
                strLocAll = "N"
            End If

            If chkClassAll.IsChecked = True Then
                strClassAll = "Y"
            Else
                strClassAll = "N"
            End If

            If rdbWithFOC.IsChecked Then
                strFOC = ""
            ElseIf rdbWoFOC.IsChecked Then
                strFOC = " and coalesce(Scheme_Item,'N')='N'"
            ElseIf rdbFOC.IsChecked Then
                strFOC = " and coalesce(Scheme_Item,'N')='Y'"
            End If


            'Dim HierCode, HierColumn As String

            Dim strLoca As String = ""
            Dim strClass As String = ""
            If chkLocSelect.IsChecked Then
                For Each Str As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLoca) > 0 Then
                        strLoca += ", "
                    End If
                    strLoca += Str
                Next
            End If

            If chkClassSelect.IsChecked Then
                For Each Str As String In chkCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strClass) > 0 Then
                        strClass += ", "
                    End If
                    strClass += Str
                Next
            End If




            '''''   Main qyery

            Dim strInterComp As String
            Dim strMain As String = " select Discount_Amt,Scheme_Item,InvoiceQty,Excisable,Location_Code,Location_Desc,Route_No,Route_Desc, " & _
            "Type,Document_Code as Sale_Invoice_No,CONVERT(varchar, DocDate,103) as DocDate,Transfer_No,Transfer_Date, Customer_Code,Customer_Name,Item_Code, " & _
            "MICode,MIUnit,mainqty," & _
            "Unit_Code," & _
            "convert(decimal(18,2),mrp) as mrp,convert(decimal(18,2),mrpBottle) as mrpBottle,TP, " & _
                    "([GrossQTY]) as [Gross Qty], " & _
                    "CONVERT(DECIMAL(18,2),(DiscQTY)) as [Qty Disc], " & _
                    "CONVERT(DECIMAL(18,2),(NetQty)) as [Net Qty], " & _
                    "CONVERT(DECIMAL(18,2),basic_rate) as basic_rate," & _
                    "(Total_Basic_Amt)  AS [Total Basic Amt],  " & _
                    "CONVERT(DECIMAL(18,2),(ExciseAmt))  as [Excise Amt], " & _
                    "CONVERT(DECIMAL(18,2),(ECessAmt))    as [Cess Amt], " & _
                    "CONVERT(DECIMAL(18,2),(HCessAmt))   as [Hcess Amt], " & _
                    "CONVERT(DECIMAL(18,2),Excise) as Excise, " & _
                    "CONVERT(DECIMAL(18,2),(Cess))   as Cess, " & _
                    "CONVERT(DECIMAL(18,2),(Hcess))  as Hcess, " & _
                    "convert(decimal (18,2),(FOCAMt)) as FOCAMt, " & _
                    "convert(decimal(18,2),InvoiceAmt) as InvoiceAmt, " & _
                    "CONVERT(DECIMAL(18,2),((Total_Disc_Amt) ))  as DISC, " & _
                    "CONVERT(DECIMAL(18,2),(CSTTaxAmt))  as [CST], " & _
                    "CONVERT(DECIMAL(18,2),(VatTaxAmt))  as [VAT], " & _
                    "CONVERT(DECIMAL(18,2),(AddTaxAmt + OtherTaxAmt))  as [Other Tax], " & _
                    "DocAmt, " & _
                    "CONVERT(DECIMAL(18,2),(DVAT))   as DVAT, " & _
                    "[TPT Rate], " & _
                    "CONVERT(DECIMAL(18,2),(MRP - Margin))  AS [T.Rate], " & _
                    "(Margin)  as Margin, " & _
                    "CONVERT(DECIMAL(18,2),(MRP - Margin))  AS [T.Price], " & _
                    "CONVERT(DECIMAL(18,2),(Total_TPT))  as [TPT Amt], " & _
                    "CONVERT(DECIMAL(18,2),((MRP - Margin) * InvoiceQty))   as [T.Rate Amt], " & _
                    "CONVERT(DECIMAL(18,2),(Total_MRP_Amt))  as [Total MRP] , " & _
                    "CONVERT(DECIMAL(18,2),(Margin * InvoiceQty))  as [T.Margin], " & _
                    "CONVERT(DECIMAL(18,2),((MRP - Margin) * InvoiceQty))  as [T.Price Amt], " & _
                    "CONVERT(DECIMAL(18,2),(isnull(ISNULL(commamt,CommHisamt) * GrossQTY,0)))  as COMMAmt, " & _
                    "[Sale Account Amt], " & _
                    "case when type='Sale Invoice' then case when Excisable='F'  and coalesce(Scheme_Item,'N')='N' then   ([Total_Basic_Amt] - [Sale Account Amt] - Total_Disc_Amt) else 0 end " & _
                    "else case when Excisable='F' and coalesce(Scheme_Item,'N')='N' then   ([Total_Basic_Amt] - [Sale Account Amt] - Total_Disc_Amt) else 0  end end as Exciseamt, " & _
                    "Total_Cust_Discount,Adjustment_Amount,DocType from "

            Dim str1 As String = "Select * from ( " & _
            "SELECT TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,Scheme_Item,'' as Main_Item, TSPL_SD_SALE_INVOICE_HEAD.Document_Date as  DocDate,case when (coalesce(Scheme_Item,'N')='N') then   TSPL_SD_SALE_INVOICE_DETAIL.Item_Net_Amt else 0 end as  DocAmt,Excisable,Location_Code,Location_Desc,TSPL_SD_SALE_INVOICE_HEAD.Route_No, TSPL_SD_SALE_INVOICE_HEAD.Route_Desc, " & _
            "null  as Transfer_Date,TSPL_SD_SALE_INVOICE_DETAIL.Total_Disc_Amt, " & _
            "TSPL_CUSTOMER_MASTER.Customer_Name,case when Unit_code='FC' then  TSPL_SD_SALE_INVOICE_DETAIL.MRP / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else  TSPL_SD_SALE_INVOICE_DETAIL.MRP end AS MRPBottle, " & _
            " TSPL_CUSTOMER_MASTER.Cust_Code,0 as Adjustment_Amount,'Sale Invoice' as Type, " & _
            "TSPL_SD_SALE_INVOICE_HEAD.Document_Code ,'' as Transfer_No, " & _
            " TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, TSPL_SD_SALE_INVOICE_DETAIL.Unit_code,   TSPL_SD_SALE_INVOICE_DETAIL.MRP *  TSPL_ITEM_UOM_DETAIL.Conversion_Factor as mrp," & _
            " TSPL_SD_SALE_INVOICE_DETAIL.MRP *  TSPL_ITEM_UOM_DETAIL.Conversion_Factor -  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount1 AS TP, " & _
            " (TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost) as Basic_Rate, " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt ELSE 0 END AS Excise," & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt ELSE 0 END AS Cess,  " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt ELSE 0 END AS Hcess, " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt +  TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt ELSE  TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt END AS DVAT," & _
            " 0 as [TPT Rate]," & _
            "( TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount1 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount2 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount3 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount4  +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount5 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount6 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount7 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount8 +  TSPL_SD_SALE_INVOICE_DETAIL.Price_Amount9)  as Margin," & _
            " TSPL_SD_SALE_INVOICE_DETAIL.Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as GrossQTY," & _
            "case when coalesce(Scheme_Item,'N')='Y' then 0 else Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end AS NetQty," & _
            "case when coalesce(Scheme_Item,'N')='Y' then  Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS DiscQTY, " & _
            "TSPL_SD_SALE_INVOICE_DETAIL.Amount as Total_Basic_Amt,TSPL_SD_SALE_INVOICE_DETAIL.MRP as Total_MRP_Amt,  " & _
            "Qty as Invoiceqty, " & _
            "case when coalesce(Scheme_Item,'N')='Y' then 0 else Qty end AS netInvoice_Qty, " & _
            "case when coalesce(Scheme_Item,'N')='Y' then Qty else 0 end AS disc,Cust_Discount, " & _
            "0 as Commamt, " & _
            "0 as CommHisamt, " & _
            "CASE WHEN coalesce(Scheme_Item,'N') = 'Y' THEN ((Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((Item_Cost * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9))) ELSE 0 END AS FOCAMt, " & _
            " 0 as [Sale Account Amt],Total_Cust_Discount,0 as Total_TPT,'SD-IN'  as DocType, " & _
            " TSPL_SD_SALE_INVOICE_DETAIL.TAX1, TSPL_SD_SALE_INVOICE_DETAIL.TAX2, TSPL_SD_SALE_INVOICE_DETAIL.TAX3, " & _
            " TSPL_SD_SALE_INVOICE_DETAIL.TAX4, TSPL_SD_SALE_INVOICE_DETAIL.TAX5, " & _
            " TSPL_SD_SALE_INVOICE_DETAIL.TAX6, TSPL_SD_SALE_INVOICE_DETAIL.TAX7, " & _
            " TSPL_SD_SALE_INVOICE_DETAIL.TAX8, TSPL_SD_SALE_INVOICE_DETAIL.TAX9, " & _
            " TSPL_SD_SALE_INVOICE_DETAIL.TAX10," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt) as TAX1_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt) as TAX2_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt) as TAX3_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt) as TAX4_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt) as TAX5_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt) as TAX6_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt) as TAX7_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt) as TAX8_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt) as TAX9_Amt," & _
            " ( TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt) as TAX10_Amt" & _
            " FROM  TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
             " TSPL_SD_SALE_INVOICE_HEAD INNER JOIN " & _
             " TSPL_SD_SALE_INVOICE_DETAIL ON  TSPL_SD_SALE_INVOICE_HEAD.Document_Code =  TSPL_SD_SALE_INVOICE_DETAIL.Document_Code INNER JOIN " & _
             " TSPL_ITEM_UOM_DETAIL ON  TSPL_SD_SALE_INVOICE_DETAIL.Item_Code =  TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
             " TSPL_SD_SALE_INVOICE_DETAIL.Unit_code =  TSPL_ITEM_UOM_DETAIL.UOM_Code ON " & _
             " TSPL_LOCATION_MASTER.Location_Code =  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location LEFT OUTER JOIN " & _
             " TSPL_CUSTOMER_TYPE_MASTER RIGHT OUTER JOIN " & _
             " TSPL_CUSTOMER_MASTER ON  TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code =  TSPL_CUSTOMER_MASTER.Cust_Type_Code ON " & _
             " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code =  TSPL_CUSTOMER_MASTER.Cust_Code " & _
              "left outer join  TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " & _
                      " TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "left outer join  TSPL_SD_SHIPMENT_HEAD on  TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No= TSPL_SD_SHIPMENT_HEAD.document_code " & _
             "where /*TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and */  convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND  " & _
            "convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date, '" & dtpToDate.Value & "',103) and " & _
            "TSPL_SD_SHIPMENT_HEAD.Status='1'  " & strFOC & "  "
            If strLocAll = "N" Then
                str1 += " and  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If
            If strClassAll = "N" Then
                str1 += " and  TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(chkCustomer.CheckedValue) + ") "
            End If
            'If strHierAll = "N" Then
            '    str1 += " and  TSPL_SD_SALE_INVOICE_HEAD." & HierCode & " in (" + clsCommon.GetMulcallString(cbgHier.CheckedValue) + ") "
            'End If

            str1 += " ) b " & _
            "left outer join (select Item_Code as MICode, case when count(Unit_code) >  1 then 'FC' else MAX(Unit_code) end as MIUnit , " & _
            "Document_Code as MainInvoice,case when COUNT(unit_code) > 1 then (select Qty from TSPL_SD_SALE_INVOICE_DETAIL where " & _
            "Document_Code=a.Document_Code and Item_Code=a.Item_Code and Unit_code='FC' and coalesce(Scheme_Item,'N')='N') else MAX(Qty)  end as mainqty, " & _
            "0 as FOCQty   from (SELECT Item_Code,Unit_code,TSPL_SD_SALE_INVOICE_HEAD.Document_Code,Qty FROM TSPL_SD_SALE_INVOICE_HEAD " & _
            "left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code " & _
            "WHERE coalesce(Scheme_Item,'N')='N' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
            "convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date, '" & dtpToDate.Value & "',103)    " & _
            ") a  group by Document_Code ,item_Code ) as c  on b.Main_Item=c.MICode and b.Document_Code=c.MainInvoice  "

            Dim str2 As String = " Select * from ( " & _
            " SELECT TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt,'N' as Scheme_Item,'' as Main_Item, TSPL_SD_SALE_RETURN_HEAD.Document_Date as DocDate, " & _
            "-case when ('N'='N') then   TSPL_SD_SALE_RETURN_DETAIL.Item_Net_Amt else 0 end as DocAmt, " & _
            "TSPL_LOCATION_MASTER_1.Excisable,TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,'' as Route_No,'' as Route_Desc,null as Transfer_Date,- TSPL_SD_SALE_RETURN_DETAIL.Disc_Amt as Total_Disc_Amt, " & _
            "TSPL_CUSTOMER_MASTER.Customer_Name, case when Unit_code='FC' then  TSPL_SD_SALE_RETURN_DETAIL.MRP / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else  TSPL_SD_SALE_RETURN_DETAIL.MRP end AS MRPBottle, " & _
            " TSPL_CUSTOMER_MASTER.Cust_Code,0 as Adjustment_Amount,'Sale Return' as Type,TSPL_SD_SALE_RETURN_HEAD.Document_Code,'' as Transfer_No, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.Item_Code, TSPL_SD_SALE_RETURN_DETAIL.Unit_code, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.MRP *  TSPL_ITEM_UOM_DETAIL.Conversion_Factor as mrp, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.MRP *  TSPL_ITEM_UOM_DETAIL.Conversion_Factor -  0 AS TP," & _
            " (TSPL_SD_SALE_RETURN_DETAIL.Item_Cost)  as Basic_Rate, " & _
            "CASE WHEN TSPL_LOCATION_MASTER_1.Excisable = 'T' THEN  TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt ELSE 0 END AS Excise, " & _
            "CASE WHEN TSPL_LOCATION_MASTER_1.Excisable = 'T' THEN  TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt ELSE 0 END AS Cess, " & _
            "CASE WHEN TSPL_LOCATION_MASTER_1.Excisable = 'T' THEN  TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt ELSE 0 END AS Hcess, " & _
            "CASE WHEN TSPL_LOCATION_MASTER_1.Excisable = 'T' THEN  TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt +  TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt ELSE  TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt END AS DVAT, " & _
            " 0  as [TPT Rate], " & _
            "( 0 +  0 +  0 +  0  +  0 +  0 +  0 +  0 +  0)  as Margin, " & _
            "- ( TSPL_SD_SALE_RETURN_DETAIL.Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor)  as GrossQTY, " & _
            "-(case when 'N'='Y' then 0 else Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end) AS NetQty, " & _
            "-(case when 'N'='Y' then  Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end) AS DiscQTY, " & _
            "-( TSPL_SD_SALE_RETURN_DETAIL.Amount ) as Total_Basic_Amt, " & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.MRP) as Total_MRP_Amt, " & _
            "-(Qty) as Invoiceqty, " & _
            "-(case when 'N'='Y' then 0 else Qty end) AS netInvoice_Qty, " & _
            "-(case when 'N'='Y' then Qty else 0 end) AS disc, - (0) as Cust_Discount, " & _
            "0 as Commamt, " & _
            "0 as CommHisamt, " & _
            "-(CASE WHEN 'N' = 'Y'  THEN " & _
            "((Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((Item_Cost * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (0 + 0 + 0 + 0 + 0 + 0 + 0 + 0 + 0))) ELSE 0 END) AS FOCAMt, " & _
            "-0 as [Sale Account Amt],- (0) as Total_Cust_Discount,-(0) as Total_TPT ,'SD-SR' as DocType, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.TAX1, TSPL_SD_SALE_RETURN_DETAIL.TAX2, TSPL_SD_SALE_RETURN_DETAIL.TAX3, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.TAX4, TSPL_SD_SALE_RETURN_DETAIL.TAX5, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.TAX6, TSPL_SD_SALE_RETURN_DETAIL.TAX7, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.TAX8, TSPL_SD_SALE_RETURN_DETAIL.TAX9, " & _
            " TSPL_SD_SALE_RETURN_DETAIL.TAX10," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt ) as TAX1_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt) as TAX2_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt) as TAX3_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt) as TAX4_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt) as TAX5_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt) as TAX6_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt) as TAX7_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt) as TAX8_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX9_Amt) as TAX9_Amt," & _
            " -( TSPL_SD_SALE_RETURN_DETAIL.TAX10_Amt) as TAX10_Amt" & _
            " FROM  TSPL_LOCATION_MASTER RIGHT OUTER JOIN  TSPL_SD_SALE_RETURN_HEAD INNER JOIN " & _
            " TSPL_SD_SALE_RETURN_DETAIL ON  TSPL_SD_SALE_RETURN_HEAD.Document_Code =  TSPL_SD_SALE_RETURN_DETAIL.Document_Code INNER JOIN " & _
            " TSPL_ITEM_UOM_DETAIL ON  TSPL_SD_SALE_RETURN_DETAIL.Item_Code =  TSPL_ITEM_UOM_DETAIL.Item_Code AND  " & _
            " TSPL_SD_SALE_RETURN_DETAIL.Unit_code =  TSPL_ITEM_UOM_DETAIL.UOM_Code ON " & _
            " TSPL_LOCATION_MASTER.Location_Code =  TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location LEFT OUTER JOIN " & _
            " TSPL_CUSTOMER_TYPE_MASTER RIGHT OUTER JOIN  TSPL_CUSTOMER_MASTER ON " & _
            " TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code =  TSPL_CUSTOMER_MASTER.Cust_Type_Code ON " & _
            " TSPL_SD_SALE_RETURN_HEAD.Customer_Code =  TSPL_CUSTOMER_MASTER.Cust_Code  " & _
             "left outer join  TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " & _
            " TSPL_SD_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            " left outer join  TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.Against_Invoice_No left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
            "where convert(date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date, '" & dtpFdate.Value & "',103) AND " & _
            "convert(date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <= convert(date, '" & dtpToDate.Value & "',103) and " & _
            "TSPL_SD_SALE_RETURN_HEAD.Status='1'  /* " & strFOC & " */ "
            If strLocAll = "N" Then
                str2 += " and  TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            'TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' and  


            If strClassAll = "N" Then
                str2 += " and  TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(chkCustomer.CheckedValue) + ") "
            End If

            'If strHierAll = "N" Then
            '    str2 += " and  TSPL_SD_SALE_RETURN_HEAD." & HierCode & " in (" + clsCommon.GetMulcallString(cbgHier.CheckedValue) + ") "
            'End If

            str2 += " ) b " & _
            " left outer join (select Item_Code as MICode , case when count(Unit_code) >  1 then 'FC' else MAX(Unit_code) end as MIUnit , " & _
            "Document_Code as MainInvoice, case when COUNT(unit_code) > 1 then (select Qty from TSPL_SD_SALE_INVOICE_DETAIL where Document_Code=a.Document_Code " & _
            "and Item_Code=a.Item_Code and Unit_code='FC' and coalesce(Scheme_Item,'N')='N') else MAX(Qty)  end as mainqty,0 as FOCQty " & _
            "from (SELECT Item_Code,Unit_code,TSPL_SD_SALE_INVOICE_HEAD.Document_Code,Qty FROM TSPL_SD_SALE_INVOICE_HEAD left outer join " & _
            "TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.Document_Code WHERE " & _
            "coalesce(Scheme_Item,'N')='N' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date, '18/09/2013 1:48:47 PM',103) AND " & _
            "convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date, '18/09/2013 1:48:47 PM',103)  ) a  " & _
            "group by Document_Code ,item_Code ) as c  on b.Main_Item=c.MICode and b.Document_Code=c.MainInvoice  "

            strInterComp = "  SELECT 0 as Discount_Amt,Scheme_Item,'' as Main_Item, TSPL_SALE_RETURN_INTER_HEAD.Document_Date as DocDate ,case when (coalesce(Scheme_Item,'N')='N') then  TSPL_SALE_RETURN_INTER_DETAIL.Item_Net_Amt else 0 end as DocAmt ,Excisable,Location_Code,Location_Desc,TSPL_SALE_RETURN_INTER_HEAD.Route_No,TSPL_SALE_RETURN_INTER_HEAD.Route_Desc,null as Transfer_Date,0 as Total_Disc_Amt, " & _
            "TSPL_CUSTOMER_MASTER.Customer_Name, case when Unit_code='FC' then  TSPL_SALE_RETURN_INTER_DETAIL.MRP_amt / TSPL_ITEM_UOM_DETAIL_1.Conversion_Factor else  TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt end AS MRPBottle, " & _
            " TSPL_CUSTOMER_MASTER.Cust_Code,0 as Adjustment_Amount,'Sale InerCompany' as Type,TSPL_SALE_RETURN_INTER_HEAD.Document_No,'' as Transfer_No, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.Item_Code, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.Unit_code, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt *  TSPL_ITEM_UOM_DETAIL.Conversion_Factor as mrp, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.MRP_Amt *  TSPL_ITEM_UOM_DETAIL.Conversion_Factor -  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount1 AS TP, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.Basic_Rate  as Basic_Rate, " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt ELSE 0 END AS Excise, " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt ELSE 0 END AS Cess, " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Amt ELSE 0 END AS Hcess, " & _
            "CASE WHEN Excisable = 'T' THEN  TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Amt +  TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Amt ELSE  TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt END AS DVAT, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.TPT  as [TPT Rate], " & _
            "( TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount1 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount2 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount3 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount4  +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount5 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount6 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount7 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount8 +  TSPL_SALE_RETURN_INTER_DETAIL.Price_Amount9)  as Margin, " & _
            "-  TSPL_SALE_RETURN_INTER_DETAIL.Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor  as GrossQTY, " & _
            "- case when coalesce(Scheme_Item,'N')='Y' then 0 else qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end AS NetQty, " & _
            "- case when coalesce(Scheme_Item,'N')='Y' then  Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end AS DiscQTY, " & _
            "-  TSPL_SALE_RETURN_INTER_DETAIL.Total_Basic_Amt, " & _
            "-  TSPL_SALE_RETURN_INTER_DETAIL.Total_MRP_Amt, " & _
            "- Qty as Invoiceqty, " & _
            "- case when coalesce(Scheme_Item,'N')='Y' then 0 else Qty end AS netInvoice_Qty, " & _
            "- case when coalesce(Scheme_Item,'N')='Y' then Qty else 0 end AS disc,- Cust_Discount, " & _
            "0 as Commamt, " & _
            "0 as CommHisamt, " & _
            "0 AS FOCAMt, -  0 as [Sale Account Amt],- Total_Cust_Discount,-( TSPL_SALE_RETURN_INTER_DETAIL.Total_TPT ),'SD-IN' as  DocType," & _
            " TSPL_SALE_RETURN_INTER_DETAIL.TAX1, TSPL_SALE_RETURN_INTER_DETAIL.TAX2, TSPL_SALE_RETURN_INTER_DETAIL.TAX3, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.TAX4, TSPL_SALE_RETURN_INTER_DETAIL.TAX5, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.TAX6, TSPL_SALE_RETURN_INTER_DETAIL.TAX7, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.TAX8, TSPL_SALE_RETURN_INTER_DETAIL.TAX9, " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.TAX10," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX1_Amt ) as TAX1_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX2_Amt) as TAX2_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX3_Amt) as TAX3_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX4_Amt) as TAX4_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX5_Amt) as TAX5_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX6_Amt) as TAX6_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX7_Amt) as TAX7_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX8_Amt) as TAX8_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX9_Amt) as TAX9_Amt," & _
            " -( TSPL_SALE_RETURN_INTER_DETAIL.TAX10_Amt) as TAX10_Amt" & _
            ",'' as MICode,'' as MIUnit,'' as MainInvoice,0 as mainqty,0 as FOCQty from   TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
            " TSPL_SALE_RETURN_INTER_HEAD INNER JOIN " & _
            " TSPL_SALE_RETURN_INTER_DETAIL ON " & _
            " TSPL_SALE_RETURN_INTER_HEAD.Document_No =  TSPL_SALE_RETURN_INTER_DETAIL.Document_No INNER JOIN " & _
            " TSPL_ITEM_UOM_DETAIL ON  TSPL_SALE_RETURN_INTER_DETAIL.Item_Code =  TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.Unit_code =  TSPL_ITEM_UOM_DETAIL.UOM_Code ON " & _
            " TSPL_LOCATION_MASTER.Location_Code =  TSPL_SALE_RETURN_INTER_HEAD.Location LEFT OUTER JOIN " & _
            " TSPL_CUSTOMER_TYPE_MASTER RIGHT OUTER JOIN  TSPL_CUSTOMER_MASTER ON " & _
            " TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code =  TSPL_CUSTOMER_MASTER.Cust_Type_Code ON " & _
            " TSPL_SALE_RETURN_INTER_HEAD.Cust_Code =  TSPL_CUSTOMER_MASTER.Cust_Code " & _
             "left outer join  TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAIL_1 on  " & _
            " TSPL_SALE_RETURN_INTER_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL_1.Item_Code " & _
            "where 1=1 and convert(date, TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) >=  convert(date, '" & dtpFdate.Value & "',103) AND " & _
            "convert(date, TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) <=  convert(date, '" & dtpToDate.Value & "',103) and  Is_Post=1 "

            'TSPL_ITEM_UOM_DETAIL_1.UOM_Code='FB' 



            strSettlement = " SELECT TSPL_SD_SALE_INVOICE_HEAD.Discount_Amt, TSPL_SD_SALE_INVOICE_HEAD.Document_Date as DocDate, TSPL_SD_SALE_INVOICE_HEAD.Total_Invoice_Amt as DocAmt,Excisable,Location_Code,Location_Desc,'' as Route_No,'' as Route_Desc,null as Transfer_Date,0 as Total_Disc_Amt, " & _
            "TSPL_CUSTOMER_MASTER.Customer_Name, 0 as MRPBottle, TSPL_CUSTOMER_MASTER.Cust_Code,Adjustment_Amount,'Sale Invoice' as Type, " & _
            "TSPL_SD_SALE_INVOICE_HEAD.Document_Code,'' as Transfer_No,'' as Item_Code,'' as Unit_code, " & _
            "0 as mrp,0 AS TP, 0 as Basic_Rate, 0 AS Excise,0 AS Cess,  0 AS Hcess, 0 AS DVAT, " & _
            "0 as [TPT Rate],0  as Margin,0  as GrossQTY,0 AS NetQty,0 AS DiscQTY, 0 as Total_Basic_Amt, " & _
            "0 as Total_MRP_Amt, 0 as Invoiceqty, 0 AS netInvoice_Qty, 0 AS disc,0 as Cust_Discount, " & _
            "0 as Commamt, 0 as CommHisamt, 0 AS FOCAMt, 0 as [Sale Account Amt],0 as Total_Cust_Discount,0 as Total_TPT,'SD-SR' as  DocType FROM  " & _
            " TSPL_LOCATION_MASTER RIGHT OUTER JOIN  TSPL_SD_SALE_INVOICE_HEAD " & _
            "ON  TSPL_LOCATION_MASTER.Location_Code =  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
            "LEFT OUTER JOIN  TSPL_CUSTOMER_TYPE_MASTER RIGHT OUTER JOIN " & _
            " TSPL_CUSTOMER_MASTER ON  TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code " & _
            "=  TSPL_CUSTOMER_MASTER.Cust_Type_Code ON " & _
            " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code =  TSPL_CUSTOMER_MASTER.Cust_Code " & _
            "Right outer join TSPL_Receipt_Adjustment_Header on TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
            "=TSPL_Receipt_Adjustment_Header.Doc_No where  " & _
            "convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= " & _
            "convert(date, '" & dtpFdate.Value & "',103) AND " & _
            "convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= " & _
            "convert(date, '" & dtpToDate.Value & "',103) "

            If strLocAll = "N" Then
                strInterComp += " and  TSPL_SALE_RETURN_INTER_HEAD.Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                strSettlement += " and  TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "

            End If

            If strClassAll = "N" Then
                strInterComp += " and  TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(chkCustomer.CheckedValue) + ") "
                strSettlement += " and  TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(chkCustomer.CheckedValue) + ") "
            End If

            'If strHierAll = "N" Then
            '    strInterComp += " and  TSPL_SALE_RETURN_INTER_HEAD." & HierCode & " in (" + clsCommon.GetMulcallString(cbgHier.CheckedValue) + ") "
            '    strSettlement += " and  TSPL_SD_SALE_INVOICE_HEAD." & HierCode & " in (" + clsCommon.GetMulcallString(cbgHier.CheckedValue) + ") "
            'End If
            Dim strUnion As String = " Union all"
            'strSql = strMain & "( " & str1 & " " & strUnion & "  " & str2 & ") a "

            'If rdbIterComp.Checked Then
            If ddlSaleType.SelectedIndex = 0 Then
                strSql = str1 & strUnion & str2 & strUnion & strInterComp
            ElseIf ddlSaleType.SelectedIndex = 1 Then
                strSql = str1 & strUnion & strInterComp
            Else
                strSql = str2 & strUnion & strInterComp
            End If
            'strSql = str1 & strUnion & str2 & strUnion & strInterComp
            'Else
            'strSql = str1 & strUnion & str2
            'End If

            strSql = " select Discount_Amt,Scheme_Item,replace (convert(varchar(12),DocDate,106),' ','/') as DocDate,case when Discount_Amt=100 then 0 else DocAmt end as DocAmt,xxxxx.Excisable,xxxxx.Location_Code, " & _
            "xxxxx.Location_Desc ,Route_No,Route_Desc, replace (convert(varchar(12),null,106),' ','/') as Transfer_Date, " & _
            "Total_Disc_Amt,Customer_Name,MRPBottle,Cust_Code as Customer_Code,Adjustment_Amount,xxxxx.Type,Document_Code,'' as Transfer_No, " & _
            "Item_Code,Unit_code, " & _
            "case when MICode IS null and coalesce(Scheme_Item,'N')='Y'  then 'Target Discount' else MICode end as MICode,MIUnit,mainqty," & _
            "mrp,TP,Basic_Rate,Excise,Cess,Hcess,DVAT,[TPT Rate],Margin,GrossQTY,NetQty,DiscQTY, " & _
            "Total_Basic_Amt,Total_MRP_Amt,Invoiceqty,netInvoice_Qty,disc,Cust_Discount,Commamt,CommHisamt,FOCAMt, " & _
            "[Sale Account Amt],Total_Cust_Discount,Total_TPT,DocType, (case when  TaxM1.Type='A' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " & _
            "+case when  TaxM2.Type='A' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " & _
             "+case when  TaxM3.Type='A' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " & _
             "+case when  TaxM4.Type='A' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " & _
             "+case when  TaxM5.Type='A' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " & _
             "+case when  TaxM6.Type='A' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " & _
             "+case when  TaxM7.Type='A' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " & _
             "+case when  TaxM8.Type='A' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " & _
             "+case when  TaxM9.Type='A' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " & _
             "+case when  TaxM10.Type='A' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " & _
             ") as AddTaxAmt," & _
             " (case when  TaxM1.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX1_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM2.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX2_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM3.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX3_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM4.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX4_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM5.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX5_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM6.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX6_Amt else 0 end),0)else 0 end " & _
             " +case when  TaxM7.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX7_Amt else 0 end),0)else 0 end " & _
             " +case when  TaxM8.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX8_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM9.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX9_Amt else 0 end),0)else 0 end " & _
             "+case when  TaxM10.Type='V' then ISNULL((case when xxxxx.Scheme_Item='N' then  xxxxx.TAX10_Amt else 0 end),0)else 0 end " & _
             " ) as VatTaxAmt," & _
             "(case when  TaxM1.Type='O' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end " & _
             "+case when  TaxM2.Type='O' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " & _
             "+case when  TaxM3.Type='O' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " & _
             "+case when  TaxM4.Type='O' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " & _
             "+case when  TaxM5.Type='O' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " & _
             "+case when  TaxM6.Type='O' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " & _
             "+case when  TaxM7.Type='O' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " & _
             "+case when  TaxM8.Type='O' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " & _
             " +case when  TaxM9.Type='O' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " & _
             "+case when  TaxM10.Type='O' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " & _
             ") as OtherTaxAmt, " & _
             "(case when  TaxM1.Type='C' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end  " & _
             "+case when  TaxM2.Type='C' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end " & _
             "+case when  TaxM3.Type='C' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end " & _
             "+case when  TaxM4.Type='C' then ISNULL(xxxxx.TAX4_Amt,0)else 0 end " & _
             "+case when  TaxM5.Type='C' then ISNULL(xxxxx.TAX5_Amt,0)else 0 end " & _
             "+case when  TaxM6.Type='C' then ISNULL(xxxxx.TAX6_Amt,0)else 0 end " & _
             "+case when  TaxM7.Type='C' then ISNULL(xxxxx.TAX7_Amt,0)else 0 end " & _
             "+case when  TaxM8.Type='C' then ISNULL(xxxxx.TAX8_Amt,0)else 0 end " & _
             "+case when  TaxM9.Type='C' then ISNULL(xxxxx.TAX9_Amt,0)else 0 end " & _
             "+case when  TaxM10.Type='C' then ISNULL(xxxxx.TAX10_Amt,0)else 0 end " & _
             ") as CSTTaxAmt, " & _
             "(case when  TaxM1.Type='E' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end  " & _
             ") as ExciseAmt, " & _
             "(case when  TaxM2.Type='E' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end  " & _
             ") as ECessAmt, " & _
             "(case when  TaxM3.Type='E' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end  " & _
             ") as HCessAmt, " & _
             "case when coalesce(Scheme_Item,'N')='Y'  then ( Total_Basic_Amt +  (case when  TaxM1.Type='E' then ISNULL(xxxxx.TAX1_Amt,0)else 0 end  " & _
             ") + (case when  TaxM2.Type='E' then ISNULL(xxxxx.TAX2_Amt,0)else 0 end  " & _
             ") + (case when  TaxM3.Type='E' then ISNULL(xxxxx.TAX3_Amt,0)else 0 end  " & _
             ") ) else 0 end as InvoiceAmt " & _
             " from ( " & strSql & " ) xxxxx " & _
             "left outer join TSPL_TAX_MASTER as TaxM1 on TaxM1.Tax_Code=xxxxx.TAX1 " & _
             "left outer join TSPL_TAX_MASTER as TaxM2 on TaxM2.Tax_Code=xxxxx.TAX2 " & _
             "left outer join TSPL_TAX_MASTER as TaxM3 on TaxM3.Tax_Code=xxxxx.TAX3 " & _
             "left outer join TSPL_TAX_MASTER as TaxM4 on TaxM4.Tax_Code=xxxxx.TAX4 " & _
             "left outer join TSPL_TAX_MASTER as TaxM5 on TaxM5.Tax_Code=xxxxx.TAX5 " & _
             "left outer join TSPL_TAX_MASTER as TaxM6 on TaxM6.Tax_Code=xxxxx.TAX6 " & _
             "left outer join TSPL_TAX_MASTER as TaxM7 on TaxM7.Tax_Code=xxxxx.TAX7 " & _
             "left outer join TSPL_TAX_MASTER as TaxM8 on TaxM8.Tax_Code=xxxxx.TAX8 " & _
             "left outer join TSPL_TAX_MASTER as TaxM9 on TaxM9.Tax_Code=xxxxx.TAX9 " & _
             "left outer join TSPL_TAX_MASTER as TaxM10 on TaxM10.Tax_Code=xxxxx.TAX10 "

            'Dim ArrDBName As ArrayList = Nothing
            'If rbtnCompanyAll.IsChecked Then
            '    ArrDBName = cbgCompany.AllValue
            'Else
            '    ArrDBName = cbgCompany.CheckedValue
            'End If
            'strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strSql, ArrDBName, False)
            strQuery = strSql
            Dim tmp_strQuery As String = strMain & "( " & strQuery & ") a "
            strQuery = strMain & "( " & strQuery & ") a "

            '*****************new catrgory/subcategory************************inclded query
            strQuery = "select asss1.* from (select TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code as [MainGroupCode],  TSPL_ITEM_CATEGORY_LEVEL.description as [Main Group],TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values as [GroupCode], TSPL_ITEM_CATEGORY_LEVEL_VALUES.description as [Group Name],aaa.* from (" + strQuery + ")aaa left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=aaa.Item_Code left outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=TSPL_ITEM_MASTER.Item_Code  LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values)asss1"

            Dim qry As String = strQuery

            Dim whrcate As String = ""

            If rbtnCategorySelect.IsChecked Then
                Dim isFirstTime As Boolean = True
                strQuery += " where exists (select 1  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from (" + qry + ")aaa1) and ( " + Environment.NewLine
                For Each Ctr As RadTreeNode In tvCategory.CheckedNodes
                    If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
                        If Not isFirstTime Then
                            strQuery += " or "
                            whrcate += " or "
                        End If
                        strQuery += " ( maingroupcode='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and groupcode='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        whrcate += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
                        isFirstTime = False
                    End If
                Next
                strQuery += " ))"
                If isFirstTime Then
                    Throw New Exception("Please select at least one Category")
                End If
            End If

            If clsCommon.myLen(whrcate) > 0 Then
                whrcate = " and " + whrcate
            End If
            strQuery = "select distinct ttt.Discount_amt,ttt.scheme_item,ttt.invoiceqty,ttt.excisable,ttt.location_code,ttt.location_desc,ttt.route_no,ttt.route_desc,ttt.type,ttt.sale_invoice_no,ttt.docdate,ttt.transfer_no,ttt.transfer_date,ttt.customer_code,ttt.customer_name,ttt.item_code,ttt.micode,ttt.miunit,ttt.mainqty,ttt.unit_code,ttt.mrp,ttt.mrpbottle,ttt.tp,ttt.[gross qty],ttt.[qty disc],ttt.[net qty],ttt.basic_rate,ttt.[total basic amt],ttt.[excise amt],ttt.[cess amt],ttt.[hcess amt],ttt.excise,ttt.cess,ttt.hcess,ttt.focamt,ttt.invoiceamt,ttt.disc,ttt.cst,ttt.vat,ttt.[other tax],ttt.docamt,ttt.dvat,ttt.[tpt rate],ttt.[t.rate],ttt.margin,ttt.[t.price],ttt.[tpt amt],ttt.[t.rate amt],ttt.[total mrp],ttt.[t.price amt],ttt.commamt,ttt.[sale account amt],ttt.exciseamt,ttt.total_cust_discount,ttt.adjustment_amount,ttt.doctype,( select distinct (select ','+TSPL_ITEM_CATEGORY_LEVEL.description, ','+TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Item_code=ttt.Item_Code " + whrcate + " for XML path(''))) as Category from (" + strQuery + ")ttt"
            tmp_strQuery = strQuery

            strQuery = strQuery + " ORDER BY CONVERT(DATE, DocDate,103) "



            '***************************************************************************

            If rdbItemSummary.IsChecked = True Then
                strQuery = tmp_strQuery
                strQuery = "select max(Type) AS Type,Item_Code,mrp,mrpBottle,SUM([Gross Qty]) as [Gross Qty]," & _
                "SUM([Qty Disc]) as [Qty Disc],sum([Net Qty]) as [Net Qty], " & _
                "SUM([Total Basic Amt]) as [Total Basic Amt], " & _
                "SUM(Exciseamt) as [Excise Amt]," & _
                "SUM([Cess Amt]) as [Cess Amt], " & _
                "SUM([Hcess Amt]) as [Hcess Amt], " & _
                "SUM(FOCAMt) as FOCAMt, " & _
                "SUM(InvoiceAmt) as InvoiceAmt, " & _
                "SUM(DISC) as DISC, " & _
                "SUM([CST]) as [CST], " & _
                "SUM([VAT]) as [VAT], " & _
                "SUM([Other Tax]) as [Other Tax], " & _
                "SUM([DocAmt]) as DocAmt, " & _
                "SUM([TPT Amt]) as [TPT Amt], " & _
                "SUM([T.Rate Amt]) as [T.Rate Amt], " & _
                "SUM([Total MRP])  as [Total MRP], " & _
                "SUM([Margin]) as [T.Margin], " & _
                "SUM([T.Price Amt]) as [T.Price Amt], " & _
                "SUM(COMMAmt) as COMMAmt, " & _
                "sum([Sale Account Amt]) as [Sale Account Amt], " & _
                "sum(Exciseamt) as Exciseamt, " & _
                "sum(Total_Cust_Discount) as Total_Cust_Discount,Category from ( " & tmp_strQuery & " )b group by Item_Code,mrp,mrpBottle,Category"

            ElseIf rdbDocSummary.IsChecked = True Then
                strQuery = tmp_strQuery

                strQuery = "select max(Type) AS Type,MAX(Location_Code) as Location_Code,MAX(Location_Desc) as Location_Desc, " & _
                "MAX(Route_No) as Route_No,MAX(Route_Desc) as Route_Desc, " & _
                "max(Type) AS Type,Sale_Invoice_No,max(CONVERT(varchar, DocDate,103)) as DocDate,'' as Transfer_No,Transfer_Date,Customer_Code,max(Customer_Name) as Customer_Name,SUM([Gross Qty]) as [Gross Qty]," & _
                "SUM([Qty Disc]) as [Qty Disc], sum([Net Qty]) as [Net Qty]," & _
                "SUM([Total Basic Amt]) as [Total Basic Amt], " & _
                "SUM([Excise Amt]) as [Excise Amt], " & _
                "SUM([Cess Amt]) as [Cess Amt], " & _
                "SUM([Hcess Amt]) as [Hcess Amt], " & _
                "SUM(FOCAMt) as FOCAMt, " & _
                "SUM(InvoiceAmt) as InvoiceAmt, " & _
                "SUM(DISC) as DISC, " & _
                "SUM([CST]) as [CST], " & _
                "SUM([VAT]) as [VAT], " & _
                "SUM([Other Tax]) as [Other Tax], " & _
                "case when max(Discount_Amt)=100 then 0 else sum(DocAmt) end as DocAmt, " & _
                "SUM([TPT Amt]) as [TPT Amt], " & _
                "SUM([T.Rate Amt]) as [T.Rate Amt], " & _
                "SUM([Total MRP])  as [Total MRP], " & _
                "SUM([Margin]) as [T.Margin], " & _
                "SUM([T.Price Amt]) as [T.Price Amt], " & _
                "SUM(COMMAmt) as COMMAmt, " & _
                "sum([Sale Account Amt]) as [Sale Account Amt], " & _
                "sum(Exciseamt) as Exciseamt, " & _
                "SUM(Adjustment_Amount) as Adjustment_Amount, " & _
                "sum(Total_Cust_Discount) as Total_Cust_Discount,DocType from ( " & tmp_strQuery & " )b group by Excisable,Sale_Invoice_No,Transfer_No,DocType,Customer_Code,Transfer_Date ORDER BY MAX(CONVERT(DATE, DocDate,103))  "
            End If
            dt = clsDBFuncationality.GetDataTable(strQuery)

            If rdbDocSummary.IsChecked AndAlso pnlAdminSetting.Visible AndAlso chkReconcile.Checked Then
                Dim strComponent As String = ""
                Dim strColName As String = ""
                If rbtnSaleAccount.IsChecked Then
                    strComponent = clsRecoSettingReportComponent.SaleRecoChartSaleAccount
                    strColName = "[Sale Account Amt]"
                ElseIf rbtnExciseAccount.IsChecked Then
                    strComponent = clsRecoSettingReportComponent.SaleRecoChartExciseAmount
                    strColName = "[Excise Amt]"
                ElseIf rbtnECessAccount.IsChecked Then
                    strComponent = clsRecoSettingReportComponent.SaleRecoChartECess
                    strColName = "[Cess Amt]"
                ElseIf rbtnHCessAccount.IsChecked Then
                    strComponent = clsRecoSettingReportComponent.SaleRecoChartHCess
                    strColName = "[Hcess Amt]"
                ElseIf rbtnTPT.IsChecked Then
                    strComponent = clsRecoSettingReportComponent.SaleRecoChartTPT
                    strColName = "[TPT Amt]"
                Else
                    Throw New Exception("Not a Valid Option for Reconcilation")
                End If


                Dim dtAS As DataTable = clsReconciliationSetting.GetAccounts(clsRecoSettingReportName.SaleRecoChart, strComponent, dtpFdate.Value, dtpToDate.Value)
                Dim arr As Dictionary(Of String, clsTempDrCrAmt) = Nothing
                dt.Columns.Add("SubledgerAmt", GetType(Double))
                If dtAS IsNot Nothing AndAlso dtAS.Rows.Count > 0 Then
                    arr = New Dictionary(Of String, clsTempDrCrAmt)
                    For Each dr As DataRow In dtAS.Rows
                        Dim obj As clsTempDrCrAmt = New clsTempDrCrAmt()
                        obj.DrAmt = -1 * clsCommon.myCdbl(dr("SubledgerAmt"))
                        arr.Add(clsCommon.myCstr(clsCommon.myCstr(dr("docNo")) + clsCommon.myCstr(dr("DocType"))).ToUpper(), obj)
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        Dim strSourceDocNo As String = clsCommon.myCstr(clsCommon.myCstr(dt.Rows(ii)("Document_Code")) + clsCommon.myCstr(dt.Rows(ii)("DocType"))).ToUpper()
                        If arr.ContainsKey(strSourceDocNo) Then
                            dt.Rows(ii)("SubledgerAmt") = clsCommon.myCdbl(arr.Item(strSourceDocNo).DrAmt)
                        End If
                    Next
                End If
                If chkMismatch.Checked Then
                    Try
                        Dim dtView As DataView = dt.DefaultView
                        dtView.RowFilter = "  (SubledgerAmt<>" + strColName + ")"
                    Catch ex As Exception

                    End Try

                End If
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        Return dt
    End Function
End Class
