'--------------------------------Last modify By - Priti ------------------------------------
'--------------------------------Last modify date - 22/03/2012-------------------------------------
'--------------------------------Last modify Time - 11:00 AM -------------------------------------
Imports XpertERPEngine
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
Imports Telerik.WinControls.UI.Export.ExcelML

Public Class FrmDistribuorDiscount
    Inherits FrmMainTranScreen
    Dim l1User, l2User, l3User, l4User, l5User As String
    Const colName As String = "Name"
    Const colCode As String = "Code"
    Dim userCode, companyCode, sql, strQuery, strType As String
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ArrDBName As ArrayList = Nothing

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
        sql = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_USER_MASTER WHERE User_Code='" + userCode + "'"
        dt = clsDBFuncationality.GetDataTable(sql)
        ' dr.Read()
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
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




    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.DistributedDiscountReport)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmDistribuorDiscount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
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



    Private Sub FrmDistribuorDiscount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadItem()
        chkItemAll.IsChecked = True
        fromDate.Value = serverDate()
        ToDate.Value = serverDate()
        rdbSku.IsChecked = True
        rdbSummary.IsChecked = True
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        LoadLocation()
        chkLocationAll.IsChecked = True
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")

        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "DSTR-D-RPT"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

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
        rdbSku.IsChecked = True
        rdbSummary.IsChecked = True
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        LoadLocation()
        chkLocationAll.IsChecked = True
    End Sub
    Private Sub CreateTableDistribution()
        Try
            Dim dt As New DataTable
            Dim strQry, strLocation As String

            If chkLocationAll.IsChecked = True Then
                strLocation = "Y"
            Else
                strLocation = "N"
            End If

            strQry = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TSPL_TEMPDist_DISCOUNT]') " & _
                      " and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[TSPL_TEMPDist_DISCOUNT]"
            clsDBFuncationality.ExecuteNonQuery(strQry)

            strQry = "CREATE TABLE TSPL_TEMPDist_DISCOUNT (Item_Code VARCHAR(50),MRP_Per_Bottle decimal(18,2) DEFAULT 0 , " & _
            "MRP_Per_Case decimal(18,2) DEFAULT 0 ,Trade_Price decimal(18,2) DEFAULT 0 , " & _
            "Trade_Discount_Amt decimal(18,2) DEFAULT 0,SchemeQty decimal(18,2)  DEFAULT 0,SchemeAmt decimal(18,2) DEFAULT 0,OtherChargesQty decimal(18,2) DEFAULT 0,OtherChargesAmt decimal(18,2) DEFAULT 0, " & _
            "TotalAmt decimal(18,2) DEFAULT 0 ) "
            clsDBFuncationality.ExecuteNonQuery(strQry)


            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

            Dim strItemCode As String
            Dim DecMRPBottle, DecMRPCase, ConvRate, DecTP, decSaleQty, decFOCamt As Decimal
            Dim DecSchemeQty, DecSchemeAmt, DecDistQty, DecDistamt, DecDiscQty, DecDiscAmt As Decimal
            Dim decTotAmt As Decimal

            ''''' To insert Item,MRP,TP


            Dim str1 As String = "Select Distinct " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
           "0 as Invoice_Qty,0 as FOCAMt,0 as SchemeQty,0 as SchemeAmt,0 as DistQty,0 as Distamt,0 as TotQty  from " & _
           " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
           "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
           "where  convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
           "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103) and  (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "


            ''  To insert FOC amt of Quantitative scheme

            Dim str3 As String = "Select  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
            "0 as Invoice_Qty,(Invoice_Qty/Conversion_Factor * ((MRP_Amt * Conversion_Factor)  - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)))  as FOCAMt,0 as SchemeQty,0 as SchemeAmt,0 as DistQty,0 as Distamt,0 as TotQty  from " & _
            " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
            "where   Scheme_Item='Y'  and Scheme_Code_Qty <> 'MS1'  and (Discount_Code = '' or Discount_Code=null) and (Discount_Code = '' or Discount_Code=null) and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "

            ''  To insert FOC Amt of manual scheme

            Dim str4 As String = "Select  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
           "0 as Invoice_Qty,(Invoice_Qty/Conversion_Factor * ((MRP_Amt * Conversion_Factor)  - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)))  as FOCAMt,0 as SchemeQty,0 as SchemeAmt,0 as DistQty,0 as Distamt,0 as TotQty  from " & _
           " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
           "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
           "where   Scheme_Code_Qty='MS1'  and Scheme_Item='Y'  and (Discount_Code = '' or Discount_Code=null) and (Discount_Code = '' or Discount_Code=null) and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
           "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "

            ''  To insert Scheme qty

            Dim str5 As String = "Select  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
          "0 as Invoice_Qty,0  as FOCAMt,(Invoice_Qty/Conversion_Factor) as SchemeQty,0 as SchemeAmt,0 as DistQty,0 as Distamt,0 as TotQty  from " & _
          " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
          "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
          "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
          "where   price_amount7 <> 0 and (Scheme_Item <> 'Y' and Promo_Scheme_Item <> 'Y' and Sampling_Item <> 'y') and (Discount_Code = '' or Discount_Code=null) and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
          "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "

            ''  To insert Scheme amount

            Dim str6 As String = "Select  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
         "0 as Invoice_Qty,0  as FOCAMt,0 as SchemeQty,(Invoice_Qty * price_amount7) as SchemeAmt,0 as DistQty,0 as Distamt,0 as TotQty  from " & _
         " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
         "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
         "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
         "where   price_amount7 <> 0 and (Scheme_Item <> 'Y' and Promo_Scheme_Item <> 'Y' and Sampling_Item <> 'y') and (Discount_Code = '' or Discount_Code=null) and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
         "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "

            ''  To insert Distributed qty

            Dim str7 As String = "Select  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
        "0 as Invoice_Qty,0  as FOCAMt,0 as SchemeQty,0 as SchemeAmt,(Invoice_Qty/Conversion_Factor) as DistQty,0 as Distamt,0 as TotQty  from " & _
        " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
        "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
        "where   (Price_Amount6 <> 0 or Price_Amount8 <> 0  or Price_Amount9 <> 0) and (Scheme_Item <> 'Y' and Promo_Scheme_Item <> 'Y' and Sampling_Item <> 'y') and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "

            ''  To insert Distributed amt

            Dim str8 As String = "Select  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
       "0 as Invoice_Qty,0  as FOCAMt,0 as SchemeQty,0 as SchemeAmt,0 as DistQty,((isnull(Price_Amount6,0) + isnull(Price_Amount8,0) + Price_Amount9) * Invoice_Qty/Conversion_Factor) as Distamt,0 as TotQty  from " & _
       " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
       "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
       "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
       "where   (Price_Amount6 <> 0 or Price_Amount8 <> 0  or Price_Amount9 <> 0 ) and (Scheme_Item <> 'Y' and Promo_Scheme_Item <> 'Y' and Sampling_Item <> 'y') and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
       "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "

            Dim strUnion As String = " Union All "
            If strLocation = "N" Then
                str1 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str3 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str4 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str5 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str6 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str7 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str8 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            strQry = str1 & strUnion & str3 & strUnion & str4 & strUnion & str5 & strUnion & str6 & strUnion & str7 & strUnion & str8
            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQry, ArrDBName, False)
            strQuery = "select distinct item_code,MRPCase,TP,sum(Invoice_Qty) as Invoice_Qty,SUM(FOCAMt) as FOCAMt,SUM(SchemeQty)as SchemeQty, " & _
            "SUM(SchemeAmt ) as SchemeAmt,SUM(DistQty) as DistQty,SUM(Distamt) as Distamt from (" + strQuery + ")a  group by a.Item_Code,a.TP,a.MRPCase"
            dt = clsDBFuncationality.GetDataTable(strQuery, trans)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    strItemCode = clsCommon.myCstr(dr("Item_code"))
                    DecMRPCase = clsCommon.myCdbl(dr("MRPCase"))
                    DecTP = clsCommon.myCdbl(dr("TP"))
                    decSaleQty = clsCommon.myCdbl(dr("Invoice_Qty"))
                    decFOCamt = clsCommon.myCdbl(dr("FOCAMt"))
                    DecSchemeQty = clsCommon.myCdbl(dr("SchemeQty"))
                    DecSchemeAmt = clsCommon.myCdbl(dr("SchemeAmt"))
                    DecDistQty = clsCommon.myCdbl(dr("DistQty"))
                    DecDistamt = clsCommon.myCdbl(dr("Distamt"))

                    strQry = " Select Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL WHERE " & _
                    "Item_Code = '" & strItemCode & "' AND UOM_Code = 'fb'"
                    dt = clsDBFuncationality.GetDataTable(strQry, trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        ConvRate = clsCommon.myCdbl(dt.Rows(0)("Conversion_Factor"))
                    End If
                    DecMRPBottle = DecMRPCase / ConvRate
                    strQry = "Insert into TSPL_TEMPDist_DISCOUNT (Item_Code,MRP_Per_Bottle,MRP_Per_Case,Trade_Price,Trade_Discount_Amt,SchemeQty,SchemeAmt,OtherChargesQty,OtherChargesAmt ) values ('" & strItemCode & "'," & DecMRPBottle & "," & DecMRPCase & "," & DecTP & "," & decFOCamt & "," & DecSchemeQty & "," & DecSchemeAmt & "," & DecDiscQty & "," & DecDiscAmt & ")"
                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                Next
            End If



            ''  To insert TotalAmt
            strQry = "select Item_Code,MRP_Per_Case,Trade_Price,(SchemeAmt + OtherChargesAmt ) as TotAmt from TSPL_TEMPDist_DISCOUNT"
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    strItemCode = clsCommon.myCstr(dr("Item_code"))
                    DecTP = clsCommon.myCdbl(dr("Trade_Price"))
                    DecMRPCase = clsCommon.myCdbl(dr("MRP_Per_Case"))
                    decTotAmt = clsCommon.myCdbl(dr("TotAmt"))

                    strQry = "Update TSPL_TEMPDist_DISCOUNT  set TotalAmt='" & decTotAmt & "' where Item_Code='" & strItemCode & "' and MRP_Per_Case=" & DecMRPCase & " and Trade_Price=" & DecTP & ""
                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                Next
            End If




            trans.Commit()


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub CreateTableDistributionDetail()
        Try


            Dim dt As New DataTable
            Dim strQry, strLocation As String

            If chkLocationAll.IsChecked = True Then
                strLocation = "Y"
            Else
                strLocation = "N"
            End If

            strQry = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TSPL_TEMPDist_DISCOUNT]') " & _
                      " and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[TSPL_TEMPDist_DISCOUNT]"
            clsDBFuncationality.ExecuteNonQuery(strQry)

            strQry = "CREATE TABLE TSPL_TEMPDist_DISCOUNT (Sale_Invoice_No varchar(30),Location varchar(12),Cust_Code varchar(12),Cust_Group_Code varchar(12), " & _
            "Cust_Group_Desc varchar(50),Cust_Type_Code varchar(12),Cust_Type_Desc varchar(50),Salesman_Code varchar(12),Salesman_Desc varchar(50), " & _
            "Route_No varchar(12),Route_Desc varchar(50),Item_Code VARCHAR(50),MRP_Per_Bottle decimal(18,2) DEFAULT 0 , " & _
            "MRP_Per_Case decimal(18,2) DEFAULT 0 ,Trade_Price decimal(18,2) DEFAULT 0  , " & _
            "Trade_Discount_Amt decimal(18,2) DEFAULT 0,SchemeQty decimal(18,2)  DEFAULT 0,SchemeAmt decimal(18,2) DEFAULT 0,OtherChargesQty decimal(18,2) DEFAULT 0,OtherChargesAmt decimal(18,2) DEFAULT 0, " & _
            "TotalAmt decimal(18,2) DEFAULT 0 ) "
            clsDBFuncationality.ExecuteNonQuery(strQry)


            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

            Dim strItemCode, strInvoiceNo, strLoc, strCust, strCustGrpCode, strCustGrpname, strCustTypeCode, strCustTypeName, strSaleCode, strSaleName, strRouteCode, strRouteDesc As String
            Dim DecMRPBottle, DecMRPCase, ConvRate, DecTP, decSaleQty, decFOCamt As Decimal
            Dim DecSchemeQty, DecSchemeAmt, DecDistQty, DecDistamt, DecDiscQty, DecDiscAmt As Decimal
            Dim decTotAmt As Decimal


            Dim str1 As String = "Select Distinct " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
           "0 as Invoice_Qty,0 as FOCAMt,0 as SchemeQty,0 as SchemeAmt,0 as DistQty,0 as DistAmt,0 as TotQty  from " & _
           " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
           "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
           "where  convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
           "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "


            ''  To insert FOC amt of Quantitative scheme

            Dim str3 As String = "Select  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
            "0 as Invoice_Qty,(Invoice_Qty/Conversion_Factor * ((MRP_Amt * Conversion_Factor)  - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)))  as FOCAMt,0 as SchemeQty,0 as SchemeAmt,0 as DistQty,0 as DistAmt,0 as TotQty  from " & _
            " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
            "where   Scheme_Item='Y'  and Scheme_Code_Qty <> 'MS1'  and (Discount_Code = '' or Discount_Code=null) and (Discount_Code = '' or Discount_Code=null) and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103) and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "

            ''  To insert FOC Amt of manual scheme

            Dim str4 As String = "Select  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
           "0 as Invoice_Qty,(Invoice_Qty/Conversion_Factor * ((MRP_Amt * Conversion_Factor)  - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)))  as FOCAMt,0 as SchemeQty,0 as SchemeAmt,0 as DistQty,0 as DistAmt,0 as TotQty  from " & _
           " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
           "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
           "where   Scheme_Code_Qty='MS1'  and Scheme_Item='Y'  and (Discount_Code = '' or Discount_Code=null) and (Discount_Code = '' or Discount_Code=null) and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
           "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "

            ''  To insert Main Scheme Qty

            Dim str5 As String = "Select  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
          "0 as Invoice_Qty,0  as FOCAMt,(Invoice_Qty/Conversion_Factor) as SchemeQty,0 as SchemeAmt,0 as DistQty,0 as DistAmt,0 as TotQty  from " & _
          " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
          "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
          "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
          "where   price_amount7 <> 0 and (Scheme_Item <> 'Y' and Promo_Scheme_Item <> 'Y' and Sampling_Item <> 'y') and (Discount_Code = '' or Discount_Code=null)  and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
          "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "

            ''  To insert Scheme amount

            Dim str6 As String = "Select  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
         "0 as Invoice_Qty,0  as FOCAMt,0 as SchemeQty,(Invoice_Qty * Cust_Discount) as SchemeAmt,0 as DistQty,0 as DistAmt,0 as TotQty  from " & _
         " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
         "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
         "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
         "where   price_amount7 <> 0 and (Scheme_Item <> 'Y' and Promo_Scheme_Item <> 'Y' and Sampling_Item <> 'y') and (Discount_Code = '' or Discount_Code=null) and convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
         "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "

            ''  To insert Distribution qty

            Dim str7 As String = "Select  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
        "0 as Invoice_Qty,0  as FOCAMt,0 as SchemeQty,0 as SchemeAmt,(Invoice_Qty/Conversion_Factor) as DistQty,0 as DistAmt,0 as TotQty  from " & _
        " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
        "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
        "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
        "where   (Price_Amount6 <> 0 or Price_Amount8 <> 0  or Price_Amount9 <> 0) and (Scheme_Item <> 'Y' and Promo_Scheme_Item <> 'Y' and Sampling_Item <> 'y') and  convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
        "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "

            ''  To insert Distribution amt

            Dim str8 As String = "Select  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_code,MRP_Amt * Conversion_Factor as MRPCase,((MRP_Amt * Conversion_Factor) -  (Price_Amount1)) as TP, " & _
       "0 as Invoice_Qty,0  as FOCAMt,0 as SchemeQty,0 as SchemeAmt,0 as DistQty,((isnull(Price_Amount2,0) + isnull(Price_Amount3,0)) * Invoice_Qty/Conversion_Factor) as DistAmt,0 as TotQty  from " & _
       " " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD inner join " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No INNER JOIN  " & _
       "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code=" + clsCommon.ReplicateDBString + "tspl_item_uom_detail.item_code and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code inner join " & _
       "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code=" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code " & _
       "where   (Price_Amount6 <> 0 or Price_Amount8 <> 0  or Price_Amount9 <> 0) and (Scheme_Item <> 'Y' and Promo_Scheme_Item <> 'Y' and Sampling_Item <> 'y') and  convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
       "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and (Cust_Type_Code <> 'F' and Cust_Type_Code <> 'S') and Is_Post='Y' "


            Dim strUnion As String = " Union All "
            If strLocation = "N" Then
                str1 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str3 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str4 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str5 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str6 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str7 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                str8 += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            strQry = str1 & strUnion & str3 & strUnion & str4 & strUnion & str5 & strUnion & str6 & strUnion & str7 & strUnion & str8
            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQry, ArrDBName, False)
            strQuery = "select distinct a.Sale_Invoice_No,item_code,MRPCase,TP,sum(Invoice_Qty) as Invoice_Qty,SUM(FOCAMt) as FOCAMt,SUM(SchemeQty)as SchemeQty, " & _
            "SUM(SchemeAmt ) as SchemeAmt,SUM(DistQty) as DistQty,SUM(DistAmt) as DistAmt from (" + strQuery + ")a  group by a.Item_Code,a.TP,a.MRPCase,a.Sale_Invoice_No"
            dt = clsDBFuncationality.GetDataTable(strQuery, trans)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    strInvoiceNo = clsCommon.myCstr(dr("Sale_Invoice_No"))
                    strItemCode = clsCommon.myCstr(dr("Item_code"))
                    DecMRPCase = clsCommon.myCdbl(dr("MRPCase"))
                    DecTP = clsCommon.myCdbl(dr("TP"))
                    decSaleQty = clsCommon.myCdbl(dr("Invoice_Qty"))
                    decFOCamt = clsCommon.myCdbl(dr("FOCAMt"))
                    DecSchemeQty = clsCommon.myCdbl(dr("SchemeQty"))
                    DecSchemeAmt = clsCommon.myCdbl(dr("SchemeAmt"))
                    DecDistQty = clsCommon.myCdbl(dr("DistQty"))
                    DecDistamt = clsCommon.myCdbl(dr("DistAmt"))

                    strQry = " Select Conversion_Factor FROM TSPL_ITEM_UOM_DETAIL WHERE " & _
                    "Item_Code = '" & strItemCode & "' AND UOM_Code = 'fb'"
                    dt = clsDBFuncationality.GetDataTable(strQry, trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        ConvRate = clsCommon.myCdbl(dt.Rows(0)("Conversion_Factor"))
                    End If
                    DecMRPBottle = DecMRPCase / ConvRate
                    strQry = "Insert into TSPL_TEMPDist_DISCOUNT (Sale_Invoice_No,Item_Code,MRP_Per_Bottle,MRP_Per_Case,Trade_Price,Trade_Discount_Amt,SchemeQty,SchemeAmt,OtherChargesQty,OtherChargesAmt ) values ('" & strInvoiceNo & "','" & strItemCode & "'," & DecMRPBottle & "," & DecMRPCase & "," & DecTP & "," & decFOCamt & "," & DecSchemeQty & "," & DecSchemeAmt & "," & DecDiscQty & "," & DecDiscAmt & ")"
                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                Next
            End If

            '''''' To Insert custcode,loc,and customer information
            strQry = "Select Distinct " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc, " & _
            "" + clsCommon.ReplicateDBString + " TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code, " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Desc, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code,  " & _
            " " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Salesman_Desc," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc from " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No = " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location = " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code AND " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code LEFT OUTER JOIN " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Group_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code  " & _
           "where  convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) >= convert(date, '" & fromDate.Value & "',103) AND  " & _
            "convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) <= convert(date, '" & ToDate.Value & "',103)  and (TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code <> 'F' and TSPL_CUSTOMER_TYPE_MASTER.Cust_Type_Code <> 'S') and Is_Post='Y' "

            If strLocation = "N" Then
                strQry += " and TSPL_LOCATION_MASTER.Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            End If

            If rbtnCompanyAll.IsChecked Then
                ArrDBName = cbgCompany.AllValue
            Else
                ArrDBName = cbgCompany.CheckedValue
            End If
            strQuery = clsCommon.GetQueryWithAllSelectedDataBase(strQry, ArrDBName, False)
            dt = clsDBFuncationality.GetDataTable(strQuery, trans)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows

                    strInvoiceNo = clsCommon.myCstr(dr("Sale_Invoice_No"))
                    strLoc = clsCommon.myCstr(dr("Location"))
                    strCust = clsCommon.myCstr(dr("Cust_Code"))
                    strCustGrpCode = clsCommon.myCstr(dr("Cust_Group_Code"))
                    strCustGrpname = clsCommon.myCstr(dr("Cust_Group_Desc"))
                    strCustTypeCode = clsCommon.myCstr(dr("Cust_Type_Code"))
                    strCustTypeName = clsCommon.myCstr(dr("Cust_Type_Desc"))
                    strSaleCode = clsCommon.myCstr(dr("Salesman_Code"))
                    strSaleName = clsCommon.myCstr(dr("Salesman_Desc"))
                    strRouteCode = clsCommon.myCstr(dr("Route_No"))
                    strRouteDesc = clsCommon.myCstr(dr("Route_Desc"))

                    strQry = "Update TSPL_TEMPDist_DISCOUNT set Location='" & strLoc & "',Cust_Code='" & strCust & "',Cust_Group_Code='" & strCustGrpCode & "',Cust_Group_Desc='" & strCustGrpname & "', " & _
                    "Cust_Type_Code='" & strCustTypeCode & "',Cust_Type_Desc='" & strCustTypeName & "',Salesman_Code='" & strSaleCode & "',Salesman_Desc='" & strSaleName & "',Route_No='" & strRouteCode & "',Route_Desc='" & strRouteDesc & "'  where Sale_Invoice_No='" & strInvoiceNo & "'"
                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                Next
            End If


            ''  To insert TotalAmt
            strQry = "select Item_Code,MRP_Per_Case,Trade_Price,(SchemeAmt + OtherChargesAmt ) as TotAmt from TSPL_TEMPDist_DISCOUNT"
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    strItemCode = clsCommon.myCstr(dr("Item_code"))
                    DecTP = clsCommon.myCdbl(dr("Trade_Price"))
                    DecMRPCase = clsCommon.myCdbl(dr("MRP_Per_Case"))
                    decTotAmt = clsCommon.myCdbl(dr("TotAmt"))

                    strQry = "Update TSPL_TEMPDist_DISCOUNT  set TotalAmt='" & decTotAmt & "' where Item_Code='" & strItemCode & "' and MRP_Per_Case=" & DecMRPCase & " and Trade_Price=" & DecTP & ""
                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                Next
            End If




            trans.Commit()


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        gvReport.EnableFiltering = True
        Dim strCollection As String = ""
        Dim str As String = ""
        Dim strCmd As String = ""
        Dim dt As DataTable
        Dim StrBase As String
        If rdbSummary.IsChecked = True Then


            CreateTableDistribution()
            StrBase = "select COLUMN_NAME ,'['+REPLACE(COLUMN_NAME,'_',' ')+']'  from INFORMATION_SCHEMA.COLUMNS  where table_name='TSPL_TEMPDist_DISCOUNT'"
            dt = clsDBFuncationality.GetDataTable(StrBase)
            For Each dr As DataRow In dt.Rows
                strCollection += "TSPL_TEMPDist_DISCOUNT." + dr(0).ToString()
                strCollection += (" As ")
                strCollection += dr(1).ToString() + ","
            Next
            If rdbPack.IsChecked = True Then
                strCollection = ""
                str = ""
                For Each dr As DataRow In dt.Rows
                    str = dr(0).ToString()
                    If Not (clsCommon.CompairString(str, "item_code") = CompairStringResult.Equal Or clsCommon.CompairString(str, "MRP_Per_Bottle") = CompairStringResult.Equal Or clsCommon.CompairString(str, "MRP_Per_Case") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Trade_Price") = CompairStringResult.Equal) Then
                        strCollection += "sum(TSPL_TEMPDist_DISCOUNT." + dr(0).ToString() + ")"
                        strCollection += (" As ")
                        strCollection += dr(1).ToString() + ","
                    End If
                Next
                strCollection = strCollection.Remove(strCollection.Length - 1, 1)
                strCmd = " SELECT DISTINCT max(TSPL_ITEM_DETAILS.Class_Desc) as [Pack],TSPL_TEMPDist_DISCOUNT.Trade_Price as [Trade Price] ,TSPL_TEMPDist_DISCOUNT.MRP_Per_Case as [MRP Per Case] ,TSPL_TEMPDist_DISCOUNT.MRP_Per_Bottle as [MRP Per Bottle], " + strCollection + " " & _
                                " ,max(TSPL_ITEM_MASTER.Pack_Seq  ) FROM  TSPL_TEMPDist_DISCOUNT INNER JOIN TSPL_ITEM_DETAILS ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code  INNER JOIN TSPL_ITEM_MASTER ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_MASTER.Item_Code WHERE   2=2"
                strCmd += "and (TSPL_ITEM_DETAILS.Class_Name = 'size')  and TSPL_ITEM_UOM_DETAIL.UOM_Code ='FB' group by  TSPL_ITEM_DETAILS.Class_Code,TSPL_TEMPDist_DISCOUNT.Trade_Price ,TSPL_TEMPDist_DISCOUNT.MRP_Per_Case ,TSPL_TEMPDist_DISCOUNT.MRP_Per_Bottle order by  max(TSPL_ITEM_MASTER.Pack_Seq  ) "


                dt = clsDBFuncationality.GetDataTable(strCmd)
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
            ElseIf rdbSku.IsChecked = True Then
                strCollection = ""
                str = ""
                For Each dr As DataRow In dt.Rows
                    str = dr(0).ToString()
                    If Not (clsCommon.CompairString(str, "item_code") = CompairStringResult.Equal Or clsCommon.CompairString(str, "MRP_Per_Bottle") = CompairStringResult.Equal Or clsCommon.CompairString(str, "MRP_Per_Case") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Trade_Price") = CompairStringResult.Equal) Then
                        strCollection += "sum(TSPL_TEMPDist_DISCOUNT." + dr(0).ToString() + ")"
                        strCollection += (" As ")
                        strCollection += dr(1).ToString() + ","
                    End If
                Next
                strCollection = strCollection.Remove(strCollection.Length - 1, 1)
                strCmd = " SELECT DISTINCT max(TSPL_TEMPDist_DISCOUNT.Item_Code) as [Item Code],TSPL_TEMPDist_DISCOUNT.Trade_Price as [Trade Price] ,TSPL_TEMPDist_DISCOUNT.MRP_Per_Case as [MRP Per Case] ,TSPL_TEMPDist_DISCOUNT.MRP_Per_Bottle as [MRP Per Bottle], " + strCollection + " " & _
                                " ,max(TSPL_ITEM_MASTER.Sku_Seq  ) FROM  TSPL_TEMPDist_DISCOUNT INNER JOIN TSPL_ITEM_DETAILS ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code  INNER JOIN TSPL_ITEM_MASTER ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_MASTER.Item_Code WHERE   2=2"
                strCmd += "and (TSPL_ITEM_DETAILS.Class_Name = 'size')  and TSPL_ITEM_UOM_DETAIL.UOM_Code ='FB' group by  TSPL_ITEM_DETAILS.Class_Code,TSPL_TEMPDist_DISCOUNT.Trade_Price ,TSPL_TEMPDist_DISCOUNT.MRP_Per_Case ,TSPL_TEMPDist_DISCOUNT.MRP_Per_Bottle order by  max(TSPL_ITEM_MASTER.Sku_Seq  ) "

                dt = clsDBFuncationality.GetDataTable(strCmd)
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
            End If




        Else

            CreateTableDistributionDetail()
            StrBase = "select COLUMN_NAME ,'['+REPLACE(COLUMN_NAME,'_',' ')+']'  from INFORMATION_SCHEMA.COLUMNS  where table_name='TSPL_TEMPDist_DISCOUNT'"
            dt = clsDBFuncationality.GetDataTable(StrBase)
            str = ""
            For Each dr As DataRow In dt.Rows
                str = dr(0).ToString()
                If Not (clsCommon.CompairString(str, "sale_invoice_no") = CompairStringResult.Equal Or clsCommon.CompairString(str, "item_code") = CompairStringResult.Equal Or clsCommon.CompairString(str, "MRP_Per_Bottle") = CompairStringResult.Equal Or clsCommon.CompairString(str, "MRP_Per_Case") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Trade_Price") = CompairStringResult.Equal Or clsCommon.CompairString(str, "BS_Scheme") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Location") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Cust_Code") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Cust_Group_Code") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Cust_Group_Desc") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Cust_Type_Code") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Cust_Type_Desc") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Salesman_Code") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Salesman_Desc") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Route_No") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Route_Desc") = CompairStringResult.Equal) Then
                    strCollection += "sum(TSPL_TEMPDist_DISCOUNT." + dr(0).ToString() + ")"
                    strCollection += (" As ")
                    strCollection += dr(1).ToString() + ","
                End If
            Next
            strCollection = strCollection.Remove(strCollection.Length - 1, 1)
            strCmd = " SELECT DISTINCT  (Sale_Invoice_No) as [Sale Invoice No],max(Location) as Location,max(Cust_Code) as [Cust Code],max(Cust_Group_Code) as [Cust Group Code],max(Cust_Group_Desc) as [Cust Group Desc], " & _
            "max(Cust_Type_Code) as [Cust Type Code],max(Cust_Type_Desc) as [Cust Type Desc],max(Salesman_Code) as  [Salesman Code],max(Salesman_Desc) as [Salesman Desc],max(Route_No) as [Route No],max(Route_Desc) as [Route Desc], " & _
            "max(TSPL_TEMPDist_DISCOUNT.Item_Code) as [Item Code],TSPL_TEMPDist_DISCOUNT.Trade_Price as [Trade Price] ,TSPL_TEMPDist_DISCOUNT.MRP_Per_Case as [MRP Per Case] ,TSPL_TEMPDist_DISCOUNT.MRP_Per_Bottle as [MRP Per Bottle], " + strCollection + " " & _
                            " ,max(TSPL_ITEM_MASTER.Sku_Seq  ) FROM  TSPL_TEMPDist_DISCOUNT INNER JOIN TSPL_ITEM_DETAILS ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code  INNER JOIN TSPL_ITEM_MASTER ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_MASTER.Item_Code WHERE   2=2"
            strCmd += "and (TSPL_ITEM_DETAILS.Class_Name = 'size')  and TSPL_ITEM_UOM_DETAIL.UOM_Code ='FB' group by  Sale_Invoice_No,TSPL_TEMPDist_DISCOUNT.Item_Code,TSPL_TEMPDist_DISCOUNT.Trade_Price ,TSPL_TEMPDist_DISCOUNT.MRP_Per_Case ,TSPL_TEMPDist_DISCOUNT.MRP_Per_Bottle order by  max(TSPL_ITEM_MASTER.Sku_Seq  ) "


            dt = clsDBFuncationality.GetDataTable(strCmd)
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

        End If
    End Sub


    Sub SetGridFormationOFgvReport()
        'Dim strItemCode, head2 As String



        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            gvReport.Columns(ii).IsVisible = False
        Next


        If rdbSummary.IsChecked = True Then
            If rdbSku.IsChecked = True Then
                gvReport.Columns("Item Code").IsVisible = True
                gvReport.Columns("Item Code").Width = 80
                gvReport.Columns("Item Code").HeaderText = "Item Code"
                'gvReport.Columns("Item Code").BestFit()
            Else
                gvReport.Columns("Pack").IsVisible = True
                gvReport.Columns("Pack").Width = 80
                gvReport.Columns("Pack").HeaderText = "Pack Code"
                'gvReport.Columns("Pack").BestFit()
            End If

            gvReport.Columns("MRP Per Bottle").IsVisible = True
            gvReport.Columns("MRP Per Bottle").Width = 80
            gvReport.Columns("MRP Per Bottle").HeaderText = "MRP Per Bottle"
            'gvReport.Columns("MRP Per Bottle").BestFit()

            gvReport.Columns("MRP Per Case").IsVisible = True
            gvReport.Columns("MRP Per Case").Width = 80
            gvReport.Columns("MRP Per Case").HeaderText = "MRP Per Case"
            'gvReport.Columns("MRP Per Case").BestFit()

            gvReport.Columns("Trade Price").IsVisible = True
            gvReport.Columns("Trade Price").Width = 50
            gvReport.Columns("Trade Price").HeaderText = "Trade Price"
            'gvReport.Columns("Trade Price").BestFit()

            gvReport.Columns("Trade Discount Amt").IsVisible = True
            gvReport.Columns("Trade Discount Amt").Width = 80
            gvReport.Columns("Trade Discount Amt").HeaderText = "Trade Discount Amount"
            'gvReport.Columns("Trade Discount Amt").BestFit()

            gvReport.Columns("SchemeQty").IsVisible = True
            gvReport.Columns("SchemeQty").Width = 80
            gvReport.Columns("SchemeQty").HeaderText = "SchemeQty"
            'gvReport.Columns("SchemeQty").BestFit()

            gvReport.Columns("SchemeAmt").IsVisible = True
            gvReport.Columns("SchemeAmt").Width = 80
            gvReport.Columns("SchemeAmt").HeaderText = "SchemeAmt"
            'gvReport.Columns("SchemeAmt").BestFit()

            gvReport.Columns("OtherChargesQty").IsVisible = True
            gvReport.Columns("OtherChargesQty").Width = 80
            gvReport.Columns("OtherChargesQty").HeaderText = "OtherChargesQty"
            'gvReport.Columns("OtherChargesQty").BestFit()

            gvReport.Columns("OtherChargesAmt").IsVisible = True
            gvReport.Columns("OtherChargesAmt").Width = 80
            gvReport.Columns("OtherChargesAmt").HeaderText = "OtherChargesAmt"
            'gvReport.Columns("OtherChargesAmt").BestFit()


            gvReport.Columns("TotalAmt").IsVisible = True
            gvReport.Columns("TotalAmt").Width = 80
            gvReport.Columns("TotalAmt").HeaderText = "Total Amount"
            'gvReport.Columns("TotalAmt").BestFit()





            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            Dim item1 As New GridViewSummaryItem("SchemeQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("SchemeAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("OtherChargesQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("OtherChargesAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("Trade Discount Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("TotalAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)


            gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Else

            gvReport.Columns("Sale Invoice No").IsVisible = True
            gvReport.Columns("Sale Invoice No").Width = 100
            gvReport.Columns("Sale Invoice No").HeaderText = "Sale Invoice No"
            'gvReport.Columns("Sale Invoice No").BestFit()

            gvReport.Columns("Location").IsVisible = True
            gvReport.Columns("Location").Width = 50
            gvReport.Columns("Location").HeaderText = "Location"
            'gvReport.Columns("Location").BestFit()

            gvReport.Columns("Cust Code").IsVisible = True
            gvReport.Columns("Cust Code").Width = 50
            gvReport.Columns("Cust Code").HeaderText = "Cust Code"
            'gvReport.Columns("Cust Code").BestFit()


            gvReport.Columns("Cust Group Code").IsVisible = True
            gvReport.Columns("Cust Group Code").Width = 50
            gvReport.Columns("Cust Group Code").HeaderText = "Cust Group Code"
            'gvReport.Columns("Cust Group Code").BestFit()

            gvReport.Columns("Cust Group Desc").IsVisible = True
            gvReport.Columns("Cust Group Desc").Width = 50
            gvReport.Columns("Cust Group Desc").HeaderText = "Cust Group Desc"
            'gvReport.Columns("Cust Group Desc").BestFit()

            gvReport.Columns("Cust Type Code").IsVisible = True
            gvReport.Columns("Cust Type Code").Width = 20
            gvReport.Columns("Cust Type Code").HeaderText = "Cust Type Code"
            'gvReport.Columns("Cust Type Code").BestFit()


            gvReport.Columns("Cust Type Desc").IsVisible = True
            gvReport.Columns("Cust Type Desc").Width = 50
            gvReport.Columns("Cust Type Desc").HeaderText = "Cust Type Desc"
            'gvReport.Columns("Cust Type Desc").BestFit()

            gvReport.Columns("Salesman Code").IsVisible = True
            gvReport.Columns("Salesman Code").Width = 50
            gvReport.Columns("Salesman Code").HeaderText = "Salesman Code"
            'gvReport.Columns("Salesman Code").BestFit()

            gvReport.Columns("Salesman Desc").IsVisible = True
            gvReport.Columns("Salesman Desc").Width = 50
            gvReport.Columns("Salesman Desc").HeaderText = "Salesman Desc"
            'gvReport.Columns("Salesman Desc").BestFit()

            gvReport.Columns("Route No").IsVisible = True
            gvReport.Columns("Route No").Width = 50
            gvReport.Columns("Route No").HeaderText = "Route No"
            'gvReport.Columns("Route No").BestFit()


            gvReport.Columns("Route Desc").IsVisible = True
            gvReport.Columns("Route Desc").Width = 50
            gvReport.Columns("Route Desc").HeaderText = "Route Desc"
            'gvReport.Columns("Route Desc").BestFit()

            gvReport.Columns("Item Code").IsVisible = True
            gvReport.Columns("Item Code").Width = 50
            gvReport.Columns("Item Code").HeaderText = "Item Code"
            'gvReport.Columns("Item Code").BestFit()

            gvReport.Columns("MRP Per Bottle").IsVisible = True
            gvReport.Columns("MRP Per Bottle").Width = 50
            gvReport.Columns("MRP Per Bottle").HeaderText = "MRP Per Bottle"
            'gvReport.Columns("MRP Per Bottle").BestFit()

            gvReport.Columns("MRP Per Case").IsVisible = True
            gvReport.Columns("MRP Per Case").Width = 50
            gvReport.Columns("MRP Per Case").HeaderText = "MRP Per Case"
            'gvReport.Columns("MRP Per Case").BestFit()

            gvReport.Columns("Trade Price").IsVisible = True
            gvReport.Columns("Trade Price").Width = 50
            gvReport.Columns("Trade Price").HeaderText = "Trade Price"

            gvReport.Columns("Trade Discount Amt").IsVisible = True
            gvReport.Columns("Trade Discount Amt").Width = 70
            gvReport.Columns("Trade Discount Amt").HeaderText = "Trade Discount Amt"

            gvReport.Columns("SchemeQty").IsVisible = True
            gvReport.Columns("SchemeQty").Width = 50
            gvReport.Columns("SchemeQty").HeaderText = "SchemeQty"
            'gvReport.Columns("SchemeQty").BestFit()

            gvReport.Columns("SchemeAmt").IsVisible = True
            gvReport.Columns("SchemeAmt").Width = 70
            gvReport.Columns("SchemeAmt").HeaderText = "SchemeAmt"
            'gvReport.Columns("SchemeAmt").BestFit()

            gvReport.Columns("OtherChargesQty").IsVisible = True
            gvReport.Columns("OtherChargesQty").Width = 50
            gvReport.Columns("OtherChargesQty").HeaderText = "OtherChargesQty"
            'gvReport.Columns("OtherChargesQty").BestFit()

            gvReport.Columns("OtherChargesAmt").IsVisible = True
            gvReport.Columns("OtherChargesAmt").Width = 50
            gvReport.Columns("OtherChargesAmt").HeaderText = "OtherChargesAmt"
            'gvReport.Columns("OtherChargesAmt").BestFit()



            gvReport.Columns("TotalAmt").IsVisible = True
            gvReport.Columns("TotalAmt").Width = 80
            gvReport.Columns("TotalAmt").HeaderText = "Total Amount"
            'gvReport.Columns("TotalAmt").BestFit()




            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            Dim item1 As New GridViewSummaryItem("SchemeQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)
            Dim item2 As New GridViewSummaryItem("SchemeAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("OtherChargesQty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("OtherChargesAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("Trade Discount Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("TotalAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)


            gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If



        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub
    'Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
    '    print()

    'End Sub
    Sub print()
        Dim strCollection As String = ""
        Dim str As String = ""
        Dim strCmd As String = ""
        Dim dt As DataTable
        Dim StrBase As String
        CreateTableDistribution()
        If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select at least one item or select ALL")
            Return
        End If

        StrBase = "select COLUMN_NAME ,'['+REPLACE(COLUMN_NAME,'_',' ')+']'  from INFORMATION_SCHEMA.COLUMNS  where table_name='TSPL_TEMPDist_DISCOUNT'"
        dt = clsDBFuncationality.GetDataTable(StrBase)
        For Each dr As DataRow In dt.Rows
            str = dr(0).ToString()
            strCollection += "TSPL_TEMPDist_DISCOUNT." + dr(0).ToString()
            strCollection += (" As ")
            strCollection += dr(1).ToString() + ","
        Next

        If rdbPack.IsChecked = True Then
            strCollection = ""
            str = ""
            For Each dr As DataRow In dt.Rows
                str = dr(0).ToString()
                If Not (clsCommon.CompairString(str, "item_code") = CompairStringResult.Equal Or clsCommon.CompairString(str, "mrp_bottle") = CompairStringResult.Equal Or clsCommon.CompairString(str, "mrp_case") = CompairStringResult.Equal Or clsCommon.CompairString(str, "TP") = CompairStringResult.Equal Or clsCommon.CompairString(str, "BS_Scheme") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Cust_Code") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Location") = CompairStringResult.Equal Or clsCommon.CompairString(str, "Sale_Invoice_No") = CompairStringResult.Equal) Then
                    strCollection += "sum(TSPL_TEMPDist_DISCOUNT." + dr(0).ToString() + ")"
                    strCollection += (" As ")
                    strCollection += dr(1).ToString() + ","
                    'ElseIf str = "BS_Scheme" Then
                    '    strCollection += " (sum(TSPL_TEMPDist_DISCOUNT.Trade_Disc_Amt)/sum(TSPL_TEMPDist_DISCOUNT.TotalQty)/(avg(TSPL_TEMPDist_DISCOUNT.TP)/avg(TSPL_ITEM_UOM_DETAIL.Conversion_Factor)))"
                    '    strCollection += (" As ")
                    '    strCollection += dr(1).ToString() + ","
                End If
            Next
            strCollection = strCollection.Remove(strCollection.Length - 1, 1)
            strCmd = " SELECT DISTINCT  max(TSPL_ITEM_DETAILS.Class_Desc) as [Pack],TSPL_TEMPDist_DISCOUNT.TP as [TP] ,TSPL_TEMPDist_DISCOUNT.MRP_case as [MRP CASE] ,TSPL_TEMPDist_DISCOUNT.MRP_Bottle as [MRP Bottle], (TSPL_TEMPDist_DISCOUNT.Sale_Invoice_No) As [Sale Invoice No],(TSPL_TEMPDist_DISCOUNT.Location) As [Location],(TSPL_TEMPDist_DISCOUNT.Cust_Code) As [Cust Code], " + strCollection + "  " & _
                            " , max(TSPL_ITEM_MASTER.Pack_Seq  ) FROM  TSPL_TEMPDist_DISCOUNT INNER JOIN TSPL_ITEM_DETAILS ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code  INNER JOIN TSPL_ITEM_MASTER ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_MASTER.Item_Code WHERE TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' and  2=2"
            strCmd += "and (TSPL_ITEM_DETAILS.Class_Name = 'size')  and TSPL_ITEM_UOM_DETAIL.UOM_Code ='FB' group by  TSPL_ITEM_DETAILS.Class_Code,TSPL_TEMPDist_DISCOUNT.TP ,TSPL_TEMPDist_DISCOUNT.MRP_case ,TSPL_TEMPDist_DISCOUNT.MRP_Bottle ,TSPL_TEMPDist_DISCOUNT.Sale_Invoice_No,TSPL_TEMPDist_DISCOUNT.Location,TSPL_TEMPDist_DISCOUNT.Cust_Code order by  max(TSPL_ITEM_MASTER.Pack_Seq  ) "
        ElseIf rdbSku.IsChecked = True Then
            strCollection = strCollection.Remove(strCollection.Length - 1, 1)
            strCmd = " SELECT DISTINCT " + strCollection + " " & _
                 " ,TSPL_ITEM_MASTER.Sku_Seq FROM  TSPL_TEMPDist_DISCOUNT INNER JOIN TSPL_ITEM_DETAILS ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_DETAILS.Item_Code INNER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_UOM_DETAIL.Item_Code  inner join TSPL_ITEM_MASTER on TSPL_TEMPDist_DISCOUNT.Item_Code = TSPL_ITEM_MASTER.Item_Code WHERE TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' and  2=2  order by TSPL_ITEM_MASTER.Sku_Seq "
        End If
        transportSql.OpenExporttoExcel(strCmd, Me)
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub
    Sub LoadLocation()
        ' Dim qry As String = "select distinct Location,Item_Code as [Item Code] from TSPL_SALE_INVOICE_DETAIL"
        'Dim qry As String = "select Loc_Segment_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'"
        Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "

        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"
    End Sub

    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
    End Sub

    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub


    Private Sub RadPageView1_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadPageView1.SelectedPageChanged

    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        Export()
    End Sub
    Private Sub ExportToExcel()
        Try
            Dim strReportTitle As String

            If rdbSummary.IsChecked = True Then
                strReportTitle = "Distributed Discount Summary wise"
            Else
                strReportTitle = "Distributed Discount Detail wise"
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

            Dim exporter As New ExportToExcelML(gvReport)
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
            Me.Controls.Remove(gvReport)
            Dim xlsApp As Microsoft.Office.Interop.Excel.Application
            Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
            xlsApp = New Microsoft.Office.Interop.Excel.Application
            xlsApp.Visible = True
            xlsWB = xlsApp.Workbooks.Open(Fullpath)
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

        End If

        If e.SheetIndex = 0 Then 'add header row only for the first excel sheet                
            Dim style1 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Distributed Discount Report : " + strReportTitle)
            Dim style3 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "Start Date : = " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy"))
            Dim style4 As SingleStyleElement = (CType(sender, ExportToExcelML)).AddCustomExcelRow(e.ExcelTableElement, 20, "End Date : = " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))

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

    Sub Export()
        If gvReport.Rows.Count > 0 Then
            ExportToExcel()
        Else
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        End If
    End Sub
End Class
