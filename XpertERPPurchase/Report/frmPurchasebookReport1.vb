'--04/03/2013-5:44PM---Updation By--Pankaj Kumar--Created Document Wise report--------------By-Balwinder Singh
''''''''' modified by priti on bug no BM00000000464
''''''''' modified by priti on bug no BM00000000493
''''''''' modified by priti on bug no BM00000000539
'---Updation by--[Pankaj Kumar Chaudhary]--Against Ticket No--[BM00000000768, BM00000000866, BM00000000924, BM00000001061, BM00000001416, BM00000002141]
''BM00000000664
'--------------Shipra --------Ticket no:-BM00000000865---------'''''''''''''
'''' BM00000001074 by priti
'''' '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
'''' '---Preeti Gupta--Ticket No-BM00000003031
'''' BM00000003620
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports common
'' MRP Column Added By abhishek on 1 Nov 2012 4:00 Pm---
''BM00000002420 for multiple user. 23-04-2013 -balwinder
'Puran Singh Negi- Ticket No- BM00000003421

Public Class FrmPurchasebookReport1
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ds As New DataSet()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dtCategory As DataTable
    Public arrCat As Dictionary(Of String, Object) = Nothing
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPurchasebookReport1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub

    Sub LoadItemType()
        
    End Sub

    Private Sub FrmPurchasebookReport1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1

        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        RbCategoryAll.IsChecked = True
        'rbtnCategoryAll.IsChecked = True
        chkItemAll.IsChecked = True
        chkLocationAll.IsChecked = True

        chkPoInvoiceAll.IsChecked = True
        chkVendorAll.IsChecked = True
        chkAccountAll.IsChecked = True
        ItemLoad()
        LoadCategory()
        LoadLocation()
        LoadPoInvoice()
        LoadVendor()
        LoadAccounts()
        LoadItemType()
        rdbtnFinishedGood.IsChecked = True
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")


        MyLabel1.Visible = objCommonVar.IsDemoERP
        txtUOM.Visible = objCommonVar.IsDemoERP

        If arrCat IsNot Nothing AndAlso arrCat.Count > 0 Then
            RbcategorySelect.IsChecked = True
            For Each str As String In arrCat.Keys
                For ii As Integer = 0 To gvCategory.RowCount - 1
                    If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                        gvCategory.Rows(ii).Cells("SEL").Value = True
                        gvCategory.Rows(ii).Tag = arrCat(str)
                    End If
                Next
            Next
        End If

        If objCommonVar.IsDemoERP Then
            grpItemType.Visible = False
            rdbtnOther.IsChecked = True
        End If
    End Sub
    Sub LoadCategory()
        'Dim qry As String = "select Code,Name,Parent from ("
        'qry += " select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE"
        'qry += " union all"
        'qry += " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
        'qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE"
        'qry += " Union all"
        'qry += " select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
        'qry += " )xxx order by Sno"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'tvCategory.DataSource = Nothing
        'tvCategory.TreeViewElement.AutoSizeItems = True
        'tvCategory.ShowLines = True
        'tvCategory.ShowRootLines = True
        'tvCategory.TreeViewElement.ViewElement.Margin = New Padding(4)
        'tvCategory.ShowExpandCollapse = True
        'tvCategory.TreeIndent = 15
        'tvCategory.FullRowSelect = False
        'tvCategory.ShowLines = True
        'tvCategory.LineStyle = TreeLineStyle.Dot
        'tvCategory.LineColor = Color.FromArgb(110, 153, 210)
        'tvCategory.ExpandAnimation = ExpandAnimation.Opacity
        'tvCategory.AllowEdit = False
        'tvCategory.ShowRootLines = False
        'tvCategory.TreeViewElement.AllowAlternatingRowColor = True
        'tvCategory.TreeViewElement.AlternatingRowColor = Color.AliceBlue

        'tvCategory.TreeViewElement.DrawBorder = True
        'tvCategory.ValueMember = "Code"
        'tvCategory.DisplayMember = "Name"
        'tvCategory.ChildMember = "Code"
        'tvCategory.ParentMember = "Parent"
        'tvCategory.DataSource = dt
        'tvCategory.CheckBoxes = True

        'tvCategory.ExpandAll()
        '===========================
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True
    End Sub
    Sub LoadAccounts()
        Dim qry1 As String = " Select Distinct Account_Code as Code , Description from TSPL_GL_ACCOUNTS"
        cbgAccounts.DataSource = clsDBFuncationality.GetDataTable(qry1)
        cbgAccounts.ValueMember = "Code"
        cbgAccounts.DisplayMember = "Description"
    End Sub
  
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Public Sub PrintData()
        Try
            Dim whrcate As String = ""

            'If chkGroupBy.Checked AndAlso chkCategory.Checked Then
            '    clsCommon.MyMessageBoxShow("Please select only one Group by at a time")
            '    Return
            'End If
            'If chkPoInvoiceSelect.IsChecked = True AndAlso cbgPoInvoice.CheckedValue.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one PoInvoice")
            '    Return
            'End If
            'If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            '    Return
            'End If
            'If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one Item")
            '    Return
            'End If
            'If chkVendorSelect.IsChecked = True AndAlso cbgVendor.CheckedValue.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atleast one Vendor")
            '    Return
            'End If
            If chkAccountSelect.IsChecked = True AndAlso cbgAccounts.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Accounts")
                Return
            End If



            Dim qry As String
            Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            Dim PoInvoiceNoArr As ArrayList = cbgPoInvoice.CheckedValue
            Dim ItemArr As ArrayList = cbgItem.CheckedValue
            'Dim CategoryArr As ArrayList = cbgCategory.CheckedValue
            'Dim SubCategoryArr As ArrayList = cbgSubCategroy.CheckedValue
            Dim locationArr As ArrayList = cbgLocation.CheckedValue
            Dim VendorArr As ArrayList = cbgVendor.CheckedValue
            Dim strFG As String = ""
            If rdbtnFinishedGood.IsChecked Then
                strFG = " , xxx.Uom, Isnull(xxx.leakage,0) as [Leakage] ,Isnull(xxx.Burst,0)as [Burst]  ,Isnull(xxx.Short,0) as [Short],xxx.[Basic Value],xxx.[MRP] "
            End If
            qry = "  select xxx.Document_No AS [Document No], xxx.PJV_No,xxx.[Gin No.] ,convert(varchar,xxx.[GIN Date],103) as [GIN Date],xxx.[Bill NO] ,convert(varchar,xxx.[Bill Date],103) as  [Bill Date],xxx.[Vr No.] ,CONVERT(varchar, xxx.[Vr Date],103) as [Vr Date] ,xxx.Unit ,xxx.Party ,xxx.[Party Name] ,xxx.[Item Code], xxx.Uom, xxx.[Item Name],cast('ITF Code'+(xxx.itf_code)as varchar) as itf_code , xxx.[FA Post A/C] ,xxx.[FA Post A/C Name]  ,xxx.Qty "
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal AndAlso chkItem.Checked = False AndAlso chkCategory.Checked = False AndAlso chkGroupBy.Checked = False Then
                qry += " ,xxx.[Basic Value], xxx.Disc_Amt, xxx.[NO VAT], CONVERT(decimal(18,2), xxx.Others) as Others, ( xxx.[Basic Value] + xxx.Disc_Amt + xxx.[NO VAT]+xxx.Others) as [Total Basic], xxx.Excise, xxx.ECess, xxx.HCess, xxx.VAT, CONVERT(Decimal(18,2),xxx.[LandedCost/Item]) as [LandedCost/Item], xxx.[FA Post] as [FA Post],xxx.[Basic Value]-xxx.Disc_Amt +xxx.[NO VAT]+xxx.VAT +xxx.Excise +xxx.ECess +xxx.HCess+xxx.Others as [Total],(xxx.[FA Post]-(xxx.[Basic Value] + xxx.Disc_Amt + xxx.[NO VAT]+xxx.Others+xxx.NonRecoTax)) as [Difference], "
            Else
                qry += " ,xxx.[Basic Value], xxx.Disc_Amt,xxx.Excise,xxx.ExciseRate ,xxx.ECess,xxx.ECessRate ,xxx.HCess,xxx.HCessRate,xxx.[NO VAT],xxx.[NO VAT RATE],xxx.VAT,xxx.[VATRATE] ,CONVERT(decimal(18,2),  xxx.Others) as Others,0 as [Entry Tax],0 as [TCS], CONVERT(Decimal(18,2),xxx.[LandedCost/Item]) as [LandedCost/Item], xxx.[FA Post] as [FA Post],xxx.[Basic Value]-xxx.Disc_Amt +xxx.[NO VAT]+xxx.VAT +xxx.Excise +xxx.ECess +xxx.HCess+xxx.Others as [Total], "
            End If
            'qry += " DocType,xxx.remarks,CURRENCY_CODE,(case when ConvRate=0 then 1 else ConvRate end) as ConvRate ,(case when ConvRate=0 then 1 else ConvRate end )*(xxx.[Basic Value] +xxx.[NO VAT]+xxx.VAT +xxx.Excise +xxx.ECess +xxx.HCess+xxx.Others) as [Total in Base Currency]   from (SELECT d.PI_No, TSPL_VENDOR_INVOICE_HEAD.Document_No, TSPL_Item_Category.Category_Code as [MainGroupCode],  TSPL_Item_Category.Category_Name as [Main Group],TSPL_ITEM_SUB_CATEGORY .Sub_Category_Code as [GroupCode], TSPL_ITEM_SUB_CATEGORY.Description as [Group Name], H.Against_SRN AS [Gin No.], TSPL_SRN_HEAD.SRN_Date AS [GIN Date],   H.Vendor_Invoice_No AS [Bill NO], H.InvoiceDate AS [Bill Date], TSPL_PJV_HEAD.Invoice_No AS [Vr No.],TSPL_PJV_HEAD.PJV_Date AS [Vr Date], D.Location AS Unit, H.Vendor_Code AS Party,H.Vendor_Name AS [Party Name],D.Item_Code as [Item Code],D.Item_Desc as [Item Name],D.Item_GL_Account as [FA Post A/C],D.Item_GL_Account_desc as [FA Post A/C Name], D.PI_Qty + (Isnull(D.Free_Qty ,0)) AS Qty, D.Amount AS [Basic Value],D.MRP as [MRP],  " & _  '****commented because new style of category and sub-category used
            qry += " DocType,xxx.remarks,CURRENCY_CODE,(case when ConvRate=0 then 1 else ConvRate end) as ConvRate ,(case when ConvRate=0 then 1 else ConvRate end )*(xxx.[Basic Value]-xxx.Disc_Amt +xxx.[NO VAT]+xxx.VAT +xxx.Excise +xxx.ECess +xxx.HCess+xxx.Others) as [Total in Base Currency]  from (SELECT d.PI_No, TSPL_VENDOR_INVOICE_HEAD.Document_No, H.Against_SRN AS [Gin No.], TSPL_SRN_HEAD.SRN_Date AS [GIN Date],   H.Vendor_Invoice_No AS [Bill NO], H.InvoiceDate AS [Bill Date], TSPL_PJV_HEAD.Invoice_No AS [Vr No.],TSPL_PJV_HEAD.PJV_Date AS [Vr Date], D.Location AS Unit, H.Vendor_Code AS Party,H.Vendor_Name AS [Party Name],D.Item_Code as [Item Code],D.Item_Desc as [Item Name],tspl_item_master.itf_code,D.Item_GL_Account as [FA Post A/C],D.Item_GL_Account_desc as [FA Post A/C Name], D.PI_Qty + (Isnull(D.Free_Qty ,0)) AS Qty, D.Amount AS [Basic Value],D.MRP as [MRP],  " & _
                "(D.PI_Qty+D.Leak_Qty+D.Burst_Qty +D.Short_Qty) *D.Total_AddtionalCost_PerUnit   as Others,D.Leak_Qty as leakage,d.Burst_Qty as Burst,d.Short_Qty as Short,d.Unit_code as Uom, " & _
                    "(CASE WHEN D.TAX1_Amt<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'V' AND TSPL_TAX_MASTER.Tax_Code = D.tax1) > 0 THEN D.TAX1_Amt " & _
                    "ELSE CASE WHEN D.TAX2_Amt<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'V' AND TSPL_TAX_MASTER.Tax_Code = D.TAX2) > 0 THEN D.TAX2_Amt " & _
                    "ELSE CASE WHEN D.TAX3_Amt<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'V' AND TSPL_TAX_MASTER.Tax_Code = D.TAX3) > 0 THEN D.TAX3_Amt " & _
                    "ELSE CASE WHEN D.TAX4_Amt<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'V' AND TSPL_TAX_MASTER.Tax_Code = D.tax4) > 0 THEN D.TAX4_Amt " & _
                    "ELSE CASE WHEN D.TAX5_Amt<>0 AND (SELECT COUNT(Tax_Code)FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'V' AND TSPL_TAX_MASTER.Tax_Code = D.tax5) > 0 THEN D.TAX5_Amt " & _
                    "ELSE CASE WHEN D.TAX6_Amt<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER  WHERE TSPL_TAX_MASTER.Type = 'V' AND TSPL_TAX_MASTER.Tax_Code = D.tax6) > 0 THEN D.TAX6_Amt ELSE 0 END END END END END END) AS VAT, " & _
                    "(CASE WHEN D.TAX1_Rate<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'V' AND TSPL_TAX_MASTER.Tax_Code = D.tax1) > 0 THEN D.TAX1_Rate " & _
                    "ELSE CASE WHEN D.TAX2_Rate<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'V' AND TSPL_TAX_MASTER.Tax_Code = D.TAX2) > 0 THEN D.TAX2_Rate " & _
                    "ELSE CASE WHEN D.TAX3_Rate<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'V' AND TSPL_TAX_MASTER.Tax_Code = D.TAX3) > 0 THEN D.TAX3_Rate " & _
                    "ELSE CASE WHEN D.TAX4_Rate<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'V' AND TSPL_TAX_MASTER.Tax_Code = D.tax4) > 0 THEN D.TAX4_Rate " & _
                    "ELSE CASE WHEN D.TAX5_Rate<>0 AND (SELECT COUNT(Tax_Code)FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'V' AND TSPL_TAX_MASTER.Tax_Code = D.tax5) > 0 THEN D.TAX5_Rate " & _
                    "ELSE CASE WHEN D.TAX6_Rate<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER  WHERE TSPL_TAX_MASTER.Type = 'V' AND TSPL_TAX_MASTER.Tax_Code = D.tax6) > 0 THEN D.TAX6_Rate ELSE 0 END END END END END END) AS VATRate, " & _
                    "(CASE WHEN D.TAX1_Amt<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER  WHERE TSPL_TAX_MASTER.Type = 'C' AND TSPL_TAX_MASTER.Tax_Code = D .tax1) > 0 THEN D .TAX1_Amt " & _
                    "ELSE CASE WHEN D.TAX2_Amt<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'C' AND TSPL_TAX_MASTER.Tax_Code = D .TAX2) > 0 THEN D .TAX2_Amt " & _
                    "ELSE CASE WHEN D.TAX3_Amt<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER  WHERE TSPL_TAX_MASTER.Type = 'C' AND TSPL_TAX_MASTER.Tax_Code = D .TAX3) > 0 THEN D .TAX3_Amt " & _
                    "ELSE CASE WHEN D.TAX4_Amt<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'C' AND TSPL_TAX_MASTER.Tax_Code = D .tax4) > 0 THEN D .TAX4_Amt " & _
                    "ELSE CASE WHEN D.TAX5_Amt<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'C' AND TSPL_TAX_MASTER.Tax_Code = D .tax5) > 0 THEN D .TAX5_Amt " & _
                    "ELSE CASE WHEN D.TAX6_Amt<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'C' AND TSPL_TAX_MASTER.Tax_Code = D .tax6) > 0 THEN D .TAX6_Amt ELSE 0 END END END END END END) AS [NO VAT], " & _
                    " (CASE WHEN D.TAX1_Rate<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER  WHERE TSPL_TAX_MASTER.Type = 'C' AND TSPL_TAX_MASTER.Tax_Code = D .tax1) > 0 THEN D .TAX1_Rate " & _
                    "ELSE CASE WHEN D.TAX2_Rate<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'C' AND TSPL_TAX_MASTER.Tax_Code = D .TAX2) > 0 THEN D .TAX2_Rate " & _
                    "ELSE CASE WHEN D.TAX3_Rate<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'C' AND TSPL_TAX_MASTER.Tax_Code = D .TAX3) > 0 THEN D .TAX3_Rate " & _
                    "ELSE CASE WHEN D.TAX4_Rate<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'C' AND TSPL_TAX_MASTER.Tax_Code = D .tax4) > 0 THEN D .TAX4_Rate " & _
                    "ELSE CASE WHEN D.TAX5_Rate<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'C' AND TSPL_TAX_MASTER.Tax_Code = D .tax5) > 0 THEN D .TAX5_Rate " & _
                    "ELSE CASE WHEN D.TAX6_Rate<>0 AND (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'C' AND TSPL_TAX_MASTER.Tax_Code = D .tax6) > 0 THEN D .TAX6_Rate  ELSE 0 END END END END END END) AS [NO VAT Rate], " & _
                    "(CASE WHEN (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER WHERE TSPL_TAX_MASTER.Type = 'E' AND TSPL_TAX_MASTER.Tax_Code = D .tax1) > 0 THEN D .TAX1_Amt ELSE 0 END) AS Excise, " & _
                    "(CASE WHEN (SELECT COUNT(Tax_Code) FROM TSPL_TAX_MASTER  WHERE TSPL_TAX_MASTER.Type = 'E' AND TSPL_TAX_MASTER.Tax_Code = D .tax1) > 0 THEN D .TAX1_Rate ELSE 0 END) AS ExciseRate, " & _
                    " Case When d.Row_Type='MISC' Then 0 Else d.Landed_Cost_Amount/(D.PI_Qty + Isnull(D.Free_Qty ,0)) End as [LandedCost/Item], (D.Landed_Cost_Amount)  as [FA Post], (CASE WHEN (SELECT     COUNT(Tax_Code) FROM          TSPL_TAX_MASTER WHERE      TSPL_TAX_MASTER.Type = 'E' AND TSPL_TAX_MASTER.Tax_Code = D .tax2) > 0 THEN D .TAX2_Amt ELSE 0 END) AS ECess," & _
                    " (CASE WHEN (SELECT     COUNT(Tax_Code) FROM          TSPL_TAX_MASTER WHERE      TSPL_TAX_MASTER.Type = 'E' AND TSPL_TAX_MASTER.Tax_Code = D .tax2) > 0 THEN D .TAX2_Rate ELSE 0 END) AS ECessRate," & _
                    " (CASE WHEN (SELECT     COUNT(Tax_Code) " & _
                    " FROM          TSPL_TAX_MASTER WHERE      TSPL_TAX_MASTER.Type = 'E' AND TSPL_TAX_MASTER.Tax_Code = D .tax3) > 0 THEN D .TAX3_Amt ELSE 0 END) AS HCess, " & _
                    "   (CASE WHEN (SELECT     COUNT(Tax_Code)  FROM   TSPL_TAX_MASTER WHERE      TSPL_TAX_MASTER.Type = 'E' AND TSPL_TAX_MASTER.Tax_Code = D .tax3) > 0 THEN D .TAX3_Rate ELSE 0 END) AS HCessRate ,'AP-IN' as DocType,'' as remarks,PJV_No,D.Disc_Amt, " & _
                    " CASE WHEN (SELECT Tax_Recoverable FROM TSPL_TAX_MASTER  WHERE D.TAX4=TSPL_TAX_MASTER.Tax_Code) = 'N' THEN D .TAX4_Amt Else 0 End as NonRecoTax,TSPL_VENDOR_INVOICE_HEAD.CURRENCY_CODE,TSPL_VENDOR_INVOICE_HEAD.ConvRate " & _
                    " FROM  TSPL_PI_HEAD AS H " & _
                    " LEFT OUTER JOIN TSPL_PI_DETAIL as D  ON D.PI_No = H.PI_No " & _
                    " LEFT OUTER JOIN  TSPL_SRN_HEAD ON H.Against_SRN = TSPL_SRN_HEAD.SRN_No " & _
                    " LEFT OUTER JOIN  TSPL_PJV_HEAD ON D.PI_No = TSPL_PJV_HEAD.Invoice_No " & _
                    " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=D.Item_Code " & _
                    " LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON H.PI_No = TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No  " & _
                    " WHERE      H.Status =1 and      (D.PI_Qty+D.Leak_Qty +D.Short_Qty+D.Burst_Qty ) >0 and convert(date,H.PI_Date,103) >= convert(date,'" + fromdate + "',103) and convert(date,H.PI_Date,103) <=convert(date,'" + Todate + "',103)"

            '-------------------------------------------------------------------------------------------------
            Dim cond As String = ""
            If rdfinish.Checked = True Then
                cond = " AND TSPL_ITEM_MASTER.Item_Type in ('F')"
            End If
            If rdother.Checked = True Then
                cond = " AND TSPL_ITEM_MASTER.Item_Type ='O'"
            End If
            If rdraw.Checked = True Then
                cond = " AND TSPL_ITEM_MASTER.Item_Type ='R'"
            End If
            If rdasset.Checked = True Then
                cond = " AND TSPL_ITEM_MASTER.Item_Type ='A'"
            End If

            If rdfinish.Checked = True AndAlso rdraw.Checked = True Then
                cond = " AND (TSPL_ITEM_MASTER.Item_Type in ('F') or TSPL_ITEM_MASTER.Item_Type in ('R'))"
            End If
            If rdfinish.Checked = True AndAlso rdother.Checked = True Then
                cond = " AND (TSPL_ITEM_MASTER.Item_Type in ('F') or TSPL_ITEM_MASTER.Item_Type in ('O'))"
            End If
            If rdfinish.Checked = True AndAlso rdasset.Checked = True Then
                cond = " AND (TSPL_ITEM_MASTER.Item_Type in ('F') or TSPL_ITEM_MASTER.Item_Type in ('A'))"
            End If
            If rdraw.Checked = True AndAlso rdother.Checked = True Then
                cond = " AND (TSPL_ITEM_MASTER.Item_Type in ('R') or TSPL_ITEM_MASTER.Item_Type in ('O'))"
            End If
            If rdraw.Checked = True AndAlso rdasset.Checked = True Then
                cond = " AND (TSPL_ITEM_MASTER.Item_Type in ('R') or TSPL_ITEM_MASTER.Item_Type in ('A'))"
            End If
            If rdother.Checked = True AndAlso rdasset.Checked = True Then
                cond = " AND (TSPL_ITEM_MASTER.Item_Type in ('O') or TSPL_ITEM_MASTER.Item_Type in ('A'))"
            End If

            If rdother.Checked = True AndAlso rdasset.Checked = True AndAlso rdfinish.Checked = True Then
                cond = " AND (TSPL_ITEM_MASTER.Item_Type in ('O') or TSPL_ITEM_MASTER.Item_Type in ('A') or TSPL_ITEM_MASTER.Item_Type in ('F'))"
            End If
            If rdother.Checked = True AndAlso rdasset.Checked = True AndAlso rdraw.Checked = True Then
                cond = " AND (TSPL_ITEM_MASTER.Item_Type in ('O') or TSPL_ITEM_MASTER.Item_Type in ('A') or TSPL_ITEM_MASTER.Item_Type in ('R'))"
            End If
            If rdraw.Checked = True AndAlso rdasset.Checked = True AndAlso rdfinish.Checked = True Then
                cond = " AND (TSPL_ITEM_MASTER.Item_Type in ('R') or TSPL_ITEM_MASTER.Item_Type in ('A') or TSPL_ITEM_MASTER.Item_Type in ('F'))"
            End If
            If rdother.Checked = True AndAlso rdfinish.Checked = True AndAlso rdraw.Checked = True Then
                cond = " AND (TSPL_ITEM_MASTER.Item_Type in ('O') or TSPL_ITEM_MASTER.Item_Type in ('F') or TSPL_ITEM_MASTER.Item_Type in ('R'))"
            End If

            If rdother.Checked = True AndAlso rdfinish.Checked = True AndAlso rdraw.Checked = True AndAlso rdasset.Checked = True Then
                cond = ""
            End If
            qry += cond
            cond = ""
            '-------------------------------------------------------------------------------------------------------
            qry += " OR D.Row_Type='MISC'"
            qry += Environment.NewLine + " UNION ALL " + Environment.NewLine
            qry += "   select Against_PI , Document_No, SRN_No as [Gin No.], SRN_Date as [GIN Date], Vendor_Invoice_No AS [Bill NO], InvoiceDate AS [Bill Date], PR_No as [Vr No.],   PR_Date  as [Vr Date], Location as Unit, Vendor_Code as [Party], Vendor_Name as [Party Name], Item_Code,Item_Desc,itf_code, '' as [FA Post A/C], '' as [FA Post A/C Name], PR_Qty, Amount as [Basic Value], MRP, 0 as Others, 0 as Leakage, 0 as Burst, 0 as Short, Unit_code as Uom, 0 as VAT, 0 as VATRATE, 0 as [NO VAT], 0 as [NO VAT RATE], ExciseAmt as Excise, convert(decimal(18,2), (case when ExciseBaseAmt=0 then 0 else (ExciseAmt*100)/ExciseBaseAmt  end)) as ExciseRate, 0 as [LandedCost/Item], 0 as [FA Post], ECessAmt as ECess, convert(decimal(18,2),(case when ECessBaseAmt=0 then 0 else (ECessAmt*100)/ECessBaseAmt end)) as ECessRate, HCessAmt as HCess, convert(decimal(18,2),(case when HCessBaseAmt=0 then 0 else (HCessAmt*100)/HCessBaseAmt end)) as HCessRate, 'PR' as DocType,Remarks ,PJVNo,Disc_Amt, 0 as NonRecoTax,CURRENCY_CODE, ConvRate from "  '','' as [FA Post A/C] , '' as [FA Post A/C Name]  from"

            qry += "     ( select "

            qry += "       TSPL_PR_HEAD.Against_PI,tspl_item_master.itf_code,"
            'qry += "    TSPL_VENDOR_INVOICE_HEAD.Document_No, TSPL_Item_Category.Category_Code as [MainGroupCode],  TSPL_Item_Category.Category_Name as [Main Group],TSPL_ITEM_SUB_CATEGORY .Sub_Category_Code as [GroupCode], TSPL_ITEM_SUB_CATEGORY.Description as [Group Name], TSPL_SRN_HEAD.SRN_No, TSPL_SRN_HEAD.SRN_Date, TSPL_PI_HEAD.Vendor_Invoice_No, TSPL_PI_HEAD.InvoiceDate, TSPL_PR_DETAIL.PR_No, TSPL_PR_HEAD.PR_Date, TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_PR_HEAD.Vendor_Code,TSPL_PR_HEAD.Vendor_Name, TSPL_PR_DETAIL.Unit_code,TSPL_PR_DETAIL.Item_Code,TSPL_PR_DETAIL.Item_Desc, (-1*TSPL_PR_DETAIL.PR_Qty) as PR_Qty,(-1* TSPL_PR_DETAIL.MRP)as MRP , (-1*TSPL_PR_DETAIL.Amount) as Amount,"

            qry += "    TSPL_VENDOR_INVOICE_HEAD.Document_No,  TSPL_SRN_HEAD.SRN_No, TSPL_SRN_HEAD.SRN_Date, TSPL_PI_HEAD.Vendor_Invoice_No, TSPL_PI_HEAD.InvoiceDate, TSPL_PR_DETAIL.PR_No, TSPL_PR_HEAD.PR_Date, TSPL_LOCATION_MASTER.Location_Code as Location,TSPL_PR_HEAD.Vendor_Code,TSPL_PR_HEAD.Vendor_Name, TSPL_PR_DETAIL.Unit_code,TSPL_PR_DETAIL.Item_Code,TSPL_PR_DETAIL.Item_Desc, (-1*TSPL_PR_DETAIL.PR_Qty) as PR_Qty,(-1* TSPL_PR_DETAIL.MRP)as MRP , (-1*TSPL_PR_DETAIL.Amount) as Amount,"
            qry += "      (case when  TaxM1.Type='E' then ISNULL((-1*TSPL_PR_DETAIL.TAX1_Base_Amt),0)else 0 end  ) as ExciseBaseAmt, (case when  TaxM1.Type='E' then ISNULL((-1*TSPL_PR_DETAIL.TAX1_Amt),0)else 0 end ) as ExciseAmt, (case when  TaxM2.Type='E' then ISNULL((-1*TSPL_PR_DETAIL.TAX2_Base_Amt),0)else 0 end  ) as ECessBaseAmt, (case when  TaxM2.Type='E' then ISNULL((-1*TSPL_PR_DETAIL.TAX2_Amt),0)else 0 end ) as ECessAmt, (case when  TaxM3.Type='E' then ISNULL((-1*TSPL_PR_DETAIL.TAX3_Base_Amt),0)else 0 end  ) as HCessBaseAmt,  (case when  TaxM3.Type='E' then ISNULL((-1*TSPL_PR_DETAIL.TAX3_Amt),0)else 0 end  ) as HCessAmt,TSPL_PR_HEAD.Remarks,(select top 1 (PJV_No) from TSPL_PJV_HEAD  left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_SRN_HEAD.Against_PO =TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No where  TSPL_SRN_HEAD.SRN_No=SRN_No )  as PJVNo,TSPL_PR_DETAIL.Disc_Amt,TSPL_PR_HEAD.CURRENCY_CODE,TSPL_PR_HEAD.ConvRate from TSPL_PR_DETAIL   left outer join TSPL_PR_HEAD on TSPL_PR_HEAD.PR_No=TSPL_PR_DETAIL.PR_No "
            qry += "    LEFT OUTER JOIN TSPL_PI_HEAD ON TSPL_PI_HEAD.PI_No=TSPL_PR_HEAD.Against_PI"

            qry += "     LEFT OUTER JOIN  TSPL_SRN_HEAD ON TSPL_PR_HEAD.Against_SRN = TSPL_SRN_HEAD.SRN_No "
            qry += "     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PR_DETAIL.Item_Code "
            'qry += "    LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code=  TSPL_ITEM_MASTER.item_code"
            'qry += "  LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.code= TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PR_HEAD.Bill_To_Location  left outer join TSPL_TAX_MASTER as TaxM1 on TaxM1.Tax_Code=TSPL_PR_DETAIL.TAX1 left outer join TSPL_TAX_MASTER as TaxM2 on TaxM2.Tax_Code=TSPL_PR_DETAIL.TAX2 left outer join TSPL_TAX_MASTER as TaxM3 on TaxM3.Tax_Code=TSPL_PR_DETAIL.TAX3 left outer join TSPL_TAX_MASTER as TaxM4 on TaxM4.Tax_Code=TSPL_PR_DETAIL.TAX4 left outer join TSPL_TAX_MASTER as TaxM5 on TaxM5.Tax_Code=TSPL_PR_DETAIL.TAX5 left outer join TSPL_TAX_MASTER as TaxM6 on TaxM6.Tax_Code=TSPL_PR_DETAIL.TAX6 left outer join TSPL_TAX_MASTER as TaxM7 on TaxM7.Tax_Code=TSPL_PR_DETAIL.TAX7 left outer join TSPL_TAX_MASTER as TaxM8 on TaxM8.Tax_Code=TSPL_PR_DETAIL.TAX8 left outer join TSPL_TAX_MASTER as TaxM9 on TaxM9.Tax_Code=TSPL_PR_DETAIL.TAX9 left outer join TSPL_TAX_MASTER as TaxM10 on TaxM10.Tax_Code=TSPL_PR_DETAIL.TAX10 LEFT OUTER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_PI_HEAD.PI_No = TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No"   '          left outer join TSPL_Item_Category on TSPL_Item_Category.Category_Code=TSPL_ITEM_MASTER.item_category  left outer join TSPL_ITEM_SUB_CATEGORY on TSPL_ITEM_SUB_CATEGORY.Sub_Category_Code=TSPL_ITEM_MASTER.Sub_item_category 



            qry += " where 2=2  and TSPL_PR_HEAD.PR_Date >= CONVERT(DATE,'" + fromdate + "',103) and TSPL_PR_HEAD.PR_Date <= CONVERT(DATE,'" + Todate + "',103) "

            '-------------------------------------------------------
            If rdfinish.Checked = True Then
                cond = " AND TSPL_PR_HEAD.Item_Type in ('F')"
            End If
            If rdother.Checked = True Then
                cond = " AND TSPL_PR_HEAD.Item_Type ='O'"
            End If
            If rdraw.Checked = True Then
                cond = " AND TSPL_PR_HEAD.Item_Type ='R'"
            End If
            If rdasset.Checked = True Then
                cond = " AND TSPL_PR_HEAD.Item_Type ='A'"
            End If

            If rdfinish.Checked = True AndAlso rdraw.Checked = True Then
                cond = " AND (TSPL_PR_HEAD.Item_Type in ('F') or TSPL_PR_HEAD.Item_Type in ('R'))"
            End If
            If rdfinish.Checked = True AndAlso rdother.Checked = True Then
                cond = " AND (TSPL_PR_HEAD.Item_Type in ('F') or TSPL_PR_HEAD.Item_Type in ('O'))"
            End If
            If rdfinish.Checked = True AndAlso rdasset.Checked = True Then
                cond = " AND (TSPL_PR_HEAD.Item_Type in ('F') or TSPL_PR_HEAD.Item_Type in ('A'))"
            End If
            If rdraw.Checked = True AndAlso rdother.Checked = True Then
                cond = " AND (TSPL_PR_HEAD.Item_Type in ('R') or TSPL_PR_HEAD.Item_Type in ('O'))"
            End If
            If rdraw.Checked = True AndAlso rdasset.Checked = True Then
                cond = " AND (TSPL_PR_HEAD.Item_Type in ('R') or TSPL_PR_HEAD.Item_Type in ('A'))"
            End If
            If rdother.Checked = True AndAlso rdasset.Checked = True Then
                cond = " AND (TSPL_PR_HEAD.Item_Type in ('O') or TSPL_PR_HEAD.Item_Type in ('A'))"
            End If

            If rdother.Checked = True AndAlso rdasset.Checked = True AndAlso rdfinish.Checked = True Then
                cond = " AND (TSPL_PR_HEAD.Item_Type in ('O') or TSPL_PR_HEAD.Item_Type in ('A') or TSPL_PR_HEAD.Item_Type in ('F'))"
            End If
            If rdother.Checked = True AndAlso rdasset.Checked = True AndAlso rdraw.Checked = True Then
                cond = " AND (TSPL_PR_HEAD.Item_Type in ('O') or TSPL_PR_HEAD.Item_Type in ('A') or TSPL_PR_HEAD.Item_Type in ('R'))"
            End If
            If rdraw.Checked = True AndAlso rdasset.Checked = True AndAlso rdfinish.Checked = True Then
                cond = " AND (TSPL_PR_HEAD.Item_Type in ('R') or TSPL_PR_HEAD.Item_Type in ('A') or TSPL_PR_HEAD.Item_Type in ('F'))"
            End If
            If rdother.Checked = True AndAlso rdfinish.Checked = True AndAlso rdraw.Checked = True Then
                cond = " AND (TSPL_PR_HEAD.Item_Type in ('O') or TSPL_PR_HEAD.Item_Type in ('F') or TSPL_PR_HEAD.Item_Type in ('R'))"
            End If

            If rdother.Checked = True AndAlso rdfinish.Checked = True AndAlso rdraw.Checked = True AndAlso rdasset.Checked = True Then
                cond = ""
            End If
            qry += cond
            '--------------------------------------------------------------

            qry += " )xxx "

            'qry += " ) as xxx left outer join TSPL_PI_DETAIL  on TSPL_PI_DETAIL.PI_No=xxx.PI_No and TSPL_PI_DETAIL.Item_Code=xxx.[Item Code] and TSPL_PI_DETAIL.MRP=xxx.MRP  where 2=2  "
            qry += " ) as xxx  where 2=2  "

            'If chkCategorySelect.IsChecked Then
            '    qry += " and xxx.[MainGroupCode] in (" + clsCommon.GetMulcallString(CategoryArr) + ")"
            'End If

            'If chkSubCategroySelect.IsChecked Then
            '    qry += " and xxx.[GroupCode] in (" + clsCommon.GetMulcallString(SubCategoryArr) + ")"
            'End If
           

            'If rbtnCategorySelect.IsChecked Then
            '    Dim isFirstTime As Boolean = True
            '    qry += " and exists (select 1  from TSPL_ITEM_MASTER_CATEGORY where Item_code in (select distinct item_code from TSPL_PI_DETAIL) and ( " + Environment.NewLine
            '    For Each Ctr As RadTreeNode In tvCategory.CheckedNodes
            '        If (Ctr.Checked) And Ctr.Parent IsNot Nothing Then
            '            If Not isFirstTime Then
            '                qry += " or "
            '                whrcate += " or "
            '            End If
            '            'qry += " ( [MainGroupCode]='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and [GroupCode]='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
            '            qry += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
            '            whrcate += " ( TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code='" + clsCommon.myCstr(Ctr.Parent.Value) + "' and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + clsCommon.myCstr(Ctr.Value) + "' )" + Environment.NewLine
            '            isFirstTime = False
            '        End If
            '    Next
            '    qry += " ))"
            '    If isFirstTime Then
            '        Throw New Exception("Please select at least one Category")
            '    End If
            'End If
            '========
          
            If txtPOInvoice.arrValueMember IsNot Nothing AndAlso txtPOInvoice.arrValueMember.Count > 0 Then
                qry += " and xxx.[Bill NO]  in (" + clsCommon.GetMulcallString(txtPOInvoice.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += "  and xxx.Unit  in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += " and xxx.[Item Code] in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                qry += " and xxx.Party in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
            End If
            '===
            'If chkPoInvoiceSelect.IsChecked Then
            '    qry += " and xxx.[Bill NO]  In (" + clsCommon.GetMulcallString(PoInvoiceNoArr) + ")"
            'End If

            'If chkLocationSelect.IsChecked Then
            '    qry += " and xxx.Unit  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            'End If

            'If chkItemSelect.IsChecked Then
            '    qry += " and xxx.[Item Code]  in (" + clsCommon.GetMulcallString(ItemArr) + ")"
            'End If
            'If chkVendorSelect.IsChecked Then
            '    qry += " and xxx.Party  in (" + clsCommon.GetMulcallString(VendorArr) + ")"
            'End If
            If chkAccountSelect.IsChecked Then
                qry += " and xxx.[FA Post A/C]  in (" + clsCommon.GetMulcallString(cbgAccounts.CheckedValue) + ")"
            End If

            '====for category
            Dim strCodeColumn As String = ""
            Dim strCodeColumnMax As String = ""
            Dim strCodeDescColumn As String = ""
            Dim strCodeDescColumnMax As String = ""

            Dim strCategoryTable As String = ""
            If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strCodeColumn += ","
                        strCodeColumnMax += ","
                        strCodeDescColumn += ","
                        strCodeDescColumnMax += ","
                    End If
                    strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                    strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"
                Next
                strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine & _
                " select * from ( " + Environment.NewLine & _
                " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc " + Environment.NewLine & _
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
                " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
                " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
                " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
                " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
                " where 2=2 " + Environment.NewLine & _
                " )xx" + Environment.NewLine & _
                " Pivot " + Environment.NewLine & _
                " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine & _
                " ) Pivt" + Environment.NewLine & _
                " Pivot " + Environment.NewLine & _
                " (" + Environment.NewLine & _
                " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine & _
                " ) Pivt1 " + Environment.NewLine & _
                " ) xxx group by Item_Code "
                ''End of Category Table start now.
            End If
            '===


            Dim strqry As String = qry
            qry = "  select [Document No],PJV_No,[Gin No.] ,[GIN Date],[Bill NO] ,[Bill Date],[Vr No.] ,[Vr Date] ,Unit ,Party ,[Party Name] ,[Item Code],Uom, [Item Name],itf_code , [FA Post A/C] ,[FA Post A/C Name]  ,Qty "
            If clsCommon.myLen(strCategoryTable) > 0 Then
                qry += "," + strCodeColumn + "," + strCodeDescColumn
            End If
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal AndAlso chkItem.Checked = False AndAlso chkCategory.Checked = False AndAlso chkGroupBy.Checked = False Then
                qry += " ,[Basic Value],Disc_Amt,[NO VAT],Others,[Total Basic],Excise,ECess,HCess,VAT,[LandedCost/Item],[FA Post],[Total],[Difference], "
            Else
                qry += " ,[Basic Value],Disc_Amt,Excise,ExciseRate ,ECess,ECessRate ,HCess,HCessRate,[NO VAT],[NO VAT RATE],VAT,[VATRATE] ,Others,[Entry Tax],[TCS],[LandedCost/Item],[FA Post],[Total], "
            End If

            qry += " DocType,remarks,CURRENCY_CODE,ConvRate ,[Total in Base Currency] from (" + strqry + ")ass "

            If clsCommon.myLen(strCategoryTable) > 0 Then
                qry += " left outer join (" + strCategoryTable + ") as virtualcategorytabel on  virtualcategorytabel.item_code= ass.[item code] where 2=2"
            End If
            '=======
            Dim strWhrCatg As String = ""
             If RbcategorySelect.IsChecked Then
                Dim IsApplicable As Boolean = False
                For ii As Integer = 0 To gvCategory.RowCount - 1
                    If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                        If IsApplicable Then
                            strWhrCatg += " and "
                        End If
                        IsApplicable = True
                        strWhrCatg += "("
                        Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            strWhrCatg += " [" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "] in ("
                            Dim isFirstTime As Boolean = True
                            For Each strInn As String In arr.Keys
                                If Not isFirstTime Then
                                    strWhrCatg += ","
                                End If
                                strWhrCatg += "'" + strInn + "'"
                                isFirstTime = False
                            Next
                            strWhrCatg += ")"
                        Else
                            strWhrCatg += " 2=2  "
                        End If
                        strWhrCatg += ")"
                    End If
                Next
                If Not IsApplicable Then
                    Throw New Exception("Please select at least one category")
                End If
                qry += " and (" + strWhrCatg + ")"
            End If


            If clsCommon.myLen(txtUOM.Value) > 0 Then
                Dim extraColumn As String = ""
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal AndAlso chkItem.Checked = False AndAlso chkCategory.Checked = False AndAlso chkGroupBy.Checked = False Then
                    extraColumn = " ,[Difference],[Total Basic]"
                Else
                    extraColumn = ",ExciseRate,ECessRate,HCessRate,[NO VAT Rate],VATRate,[Entry Tax],TCS"
                End If
                Dim strQuery As String
                strQuery = " select currency_code,convrate,[Total in base currency],[Document No], PJV_No ,[Gin No.] ,[GIN Date],[Bill NO] ,[Bill Date],[Vr No.] ,  [Vr Date] ,Unit ,Party ,[Party Name] ,[Item Code],'" + txtUOM.Value + "' as  Uom, [Item Name],itf_code ,[FA Post A/C] ,  [FA Post A/C Name]  ,convert(decimal(18,2), Qty/DivideConverstionFactor) as Qty  ,[Basic Value], Disc_Amt, [NO VAT], Others,   Excise, ECess, HCess, VAT,  [LandedCost/Item],  [FA Post], [Total],  DocType,remarks" + extraColumn + " "
                If clsCommon.myLen(strCategoryTable) > 0 Then
                    strQuery += "," + strCodeColumn + "," + strCodeDescColumn
                End If
                strQuery += " from( select [Document No], PJV_No ,[Gin No.] ,[GIN Date],[Bill NO] ,[Bill Date],[Vr No.] ,  [Vr Date] ,Unit ,Party ,[Party Name] ,[Item Code], Uom, [Item Name],itf_code ,[FA Post A/C] ,  [FA Post A/C Name]  ,(Qty*(TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) as Qty ,[Basic Value], Disc_Amt, [NO VAT], Others,   Excise, ECess, HCess, VAT,  [LandedCost/Item],  [FA Post], [Total],  DocType,remarks,currency_code,convrate,[Total in base currency],TSPL_ITEM_UOM_DETAILForDivide.Conversion_Factor as DivideConverstionFactor" + extraColumn + " "
                If clsCommon.myLen(strCategoryTable) > 0 Then
                    strQuery += "," + strCodeColumn + "," + strCodeDescColumn
                End If
                strQuery += " from(" + qry + ") xxxxxx  left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxxxxx.[Item Code] and TSPL_ITEM_UOM_DETAIL.UOM_Code=xxxxxx.Uom left outer join TSPL_ITEM_UOM_DETAIL as TSPL_ITEM_UOM_DETAILForDivide on TSPL_ITEM_UOM_DETAILForDivide.Item_Code=xxxxxx.[Item Code] and TSPL_ITEM_UOM_DETAILForDivide.UOM_Code='" + txtUOM.Value + "' )xxxxxxx where DivideConverstionFactor is not null"
                qry = strQuery
            End If

            '-----------------------------------------------
            'If clsCommon.myLen(whrcate) > 0 Then
            '    whrcate = " and " + whrcate
            'End If
            'qry = "select nxn.*,( select distinct (select distinct ','+TSPL_ITEM_CATEGORY_LEVEL.description, ','+TSPL_ITEM_CATEGORY_LEVEL_VALUES.description from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER_CATEGORY.Item_code=TSPL_ITEM_MASTER.Item_Code left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code where TSPL_ITEM_MASTER_CATEGORY.Item_code=nxn.[Item code] " + whrcate + " for XML path(''))) as Category from (" + qry + ")nxn"
            qry = "select nxn.* from (" + qry + ")nxn"
            '-------------------------------------------------------
            Dim orderbyCls As String = ""
            If chkGroupBy.Checked = True Then
                qry = "Select [Document No], MAX([Gin No.]) as [Gin No.], MAX([GIN Date]) as [GIN Date], MAX([Bill NO]) as [Bill NO], MAX([Bill Date]) as [Bill Date], MAX([Vr No.]) as [Vr No.], MAX([Vr Date]) as [Vr Date], MAX(Unit) as [Unit], MAX(Party) as Party, MAX([Party Name]) as [Party Name], MAX([Item Code]) as [Item Code], MAX([Item Name]) as [Item Name],max(itf_code) as [ITF_code], MAX([FA Post A/C]) as [FA Post A/C], MAX([FA Post A/C Name]) [FA Post A/C Name], SUM([Basic Value]) as [Basic Value],sum(Disc_Amt) as Disc_Amt, SUM(Excise) as Excise, SUM(ECess) AS ECess, SUM(HCess) as HCess, SUM([NO VAT]) As [NO VAT], SUM(VAT) as VAT, SUM(TCS) as TCS, SUM(Others) as Others, SUM([LandedCost/Item]) as [LandedCost/Item], SUM([FA Post]) As [FA Post], SUM(Total) as Total,DocType, Remarks,max(PJV_No) as PJV_No, MAX(CURRENCY_CODE) as CURRENCY_CODE, MAX([ConvRate]) as [ConvRate], SUM([Total in Base Currency]) as [Total in Base Currency],max(category) as Category from (" + qry + ") ZZZ Group By [Document No],DocType,remarks,PJV_No  order by CONVERT (date,MAX( [Vr Date]) ,103)"
            ElseIf chkCategory.Checked = True Then
                qry = "Select MAX([Document No]) as [Document No], MAX([Gin No.]) as [Gin No.], MAX([GIN Date]) as [GIN Date], MAX([Bill NO]) as [Bill NO], MAX([Bill Date]) as [Bill Date], MAX([Vr No.]) as [Vr No.], MAX([Vr Date]) as [Vr Date], MAX(Unit) as [Unit], MAX(Party) as Party, MAX([Party Name]) as [Party Name], MAX([Item Code]) as [Item Code], MAX([Item Name]) as [Item Name],max(itf_code) as [ITF_code],MAX(Qty ) as Qty,MAX(ExciseRate  ) as ExciseRate,MAX(HCessRate ) as HCessRate,MAX(ECessRate  ) as ECessRate,MAX(VATRATE  ) as VATRATE,MAX([NO VAT RATE]  ) as [NO VAT RATE],MAX([Entry Tax]  ) as [Entry Tax], MAX([FA Post A/C]) as [FA Post A/C], MAX([FA Post A/C Name]) [FA Post A/C Name], SUM([Basic Value]) as [Basic Value],sum(Disc_Amt) as Disc_Amt, SUM(Excise) as Excise, SUM(ECess) AS ECess, SUM(HCess) as HCess, SUM([NO VAT]) As [NO VAT], SUM(VAT) as VAT, SUM(TCS) as TCS, SUM(Others) as Others, MAX([LandedCost/Item]) as [LandedCost/Item], SUM([FA Post]) As [FA Post], SUM(Total) as Total,DocType, Remarks,max(PJV_No) as PJV_No, MAX(CURRENCY_CODE) as CURRENCY_CODE, MAX([ConvRate]) as [ConvRate], SUM([Total in Base Currency]) as [Total in Base Currency],Category from (" + qry + ") ZZZ   Group By category,[Item Code],DocType,remarks,PJV_No order by  CONVERT (date,MAX( [Vr Date]) ,103) "
            ElseIf chkItem.Checked Then
                qry = "Select MAX([Item Code]) as [Item Code], MAX([Item Name]) as [Item Name],max(itf_code) as [ITF_code], MAX([Bill Date]) as [Bill Date], MAX([Bill NO]) as [Bill NO], MAX(Party) as Party, MAX([Party Name]) as [Party Name], MAX(Uom) As Uom, MAX(Qty ) as Qty, MAX([LandedCost/Item]) as [LandedCost/Item], CONVERT(Decimal(18,2), SUM(Total)) as Total,Max(category) as Category from ( " + qry + " ) ZZZ Group By [Item Code], Uom order by [Item Code], CONVERT (date,MAX( [Vr Date]) ,103)"
            Else
                orderbyCls = " Order by CONVERT(date, [Vr Date],103) "
            End If
            Dim s_qry As String = ""
            If chkSerializeInv.Checked Then
                s_qry = "select [Document No],PJV_No, [Gin No.],[GIN Date],[Bill NO] ,[Bill Date],[Vr No.] ,[Vr Date] ,Unit ,Party ,[Party Name] ,[FA Post A/C],[FA Post A/C Name],TSPL_SERIAL_ITEM.Auto_Sr_No  [Serial No], [Item Code],Uom, [Item Name], Total "
                If clsCommon.myLen(strCategoryTable) > 0 Then
                    s_qry += "," + strCodeColumn + "," + strCodeDescColumn
                End If
                s_qry += " from ( " + qry + ") as final "
                s_qry += " left outer join TSPL_SERIAL_ITEM on TSPL_SERIAL_ITEM.Document_Code= final.[Gin No.] and TSPL_SERIAL_ITEM.Item_Code=final.[Item Code] where Auto_Sr_No <> ''"
                qry = s_qry
            End If

            qry += orderbyCls

            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(qry)

            If chkSerializeInv.Checked Then
                Dim lstItems As New List(Of String)
                For Each dr As DataRow In dt.Rows
                    If lstItems.Contains(dr("Item Code").ToString()) Then
                        dr("Total") = 0.0
                    Else
                        lstItems.Add(dr("Item Code").ToString())
                    End If
                Next
            End If

            GV1.DataSource = Nothing
            GV1.Columns.Clear()
            GV1.Rows.Clear()
            GV1.GroupDescriptors.Clear()
            GV1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                GV1.DataSource = dt
                If chkItem.Checked Then
                    FormatGridItemWise()
                Else
                    SetGridFormationOFGV1()
                End If
            End If
            'ExporttoMyExcel(qry, Me)
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Sub SetGridFormationOFGV1()
        ' Dim strItemCode, head2 As String

        GV1.TableElement.TableHeaderHeight = 40
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).IsVisible = False
        Next
        For Each dr As DataRow In dtCategory.Rows
            Dim strCol As String = clsCommon.myCstr(dr("CodeDescColumn"))
            GV1.Columns(strCol).IsVisible = True
            GV1.Columns(strCol).Width = 100
            GV1.Columns(strCol).HeaderText = clsCommon.myCstr(dr("DescColumn"))
        Next

        GV1.Columns("Document No").IsVisible = True
        GV1.Columns("Document No").Width = 50
        GV1.Columns("Document No").HeaderText = "Document No"

        GV1.Columns("PJV_No").IsVisible = True
        GV1.Columns("PJV_No").Width = 70
        GV1.Columns("PJV_No").HeaderText = "PJV No"

        GV1.Columns("Gin No.").IsVisible = True
        GV1.Columns("Gin No.").Width = 100
        GV1.Columns("Gin No.").HeaderText = "Gin No."

        GV1.Columns("GIN Date").IsVisible = True
        GV1.Columns("GIN Date").Width = 120
        GV1.Columns("GIN Date").HeaderText = "GIN Date"

        GV1.Columns("Bill NO").IsVisible = True
        GV1.Columns("Bill NO").Width = 80
        GV1.Columns("Bill NO").HeaderText = "Bill NO"

        GV1.Columns("Bill Date").IsVisible = True
        GV1.Columns("Bill Date").Width = 80
        GV1.Columns("Bill Date").HeaderText = "Bill Date"

        GV1.Columns("Vr No.").IsVisible = True
        GV1.Columns("Vr No.").Width = 80
        GV1.Columns("Vr No.").HeaderText = "Vr No."

        GV1.Columns("Vr Date").IsVisible = True
        GV1.Columns("Vr Date").Width = 80
        GV1.Columns("Vr Date").HeaderText = "Vr Date"

        GV1.Columns("Unit").IsVisible = True
        GV1.Columns("Unit").Width = 80
        GV1.Columns("Unit").HeaderText = "Unit"

        GV1.Columns("Party").IsVisible = True
        GV1.Columns("Party").Width = 80
        GV1.Columns("Party").HeaderText = "Party"

        GV1.Columns("Party Name").IsVisible = True
        GV1.Columns("Party Name").Width = 50
        GV1.Columns("Party Name").HeaderText = "Party Name"

        GV1.Columns("FA Post A/C").IsVisible = True
        GV1.Columns("FA Post A/C").Width = 50
        GV1.Columns("FA Post A/C").HeaderText = "GL Code"

        GV1.Columns("FA Post A/C Name").IsVisible = True
        GV1.Columns("FA Post A/C Name").Width = 50
        GV1.Columns("FA Post A/C Name").HeaderText = "GL Account"

        GV1.Columns("Total").IsVisible = True
        GV1.Columns("Total").Width = 50
        GV1.Columns("Total").HeaderText = "Total"


        If Not chkGroupBy.Checked Then
            GV1.Columns("Item Code").IsVisible = True
            GV1.Columns("Item Code").Width = 80
            GV1.Columns("Item Code").HeaderText = "Item Code"

            GV1.Columns("Item Name").IsVisible = True
            GV1.Columns("Item Name").Width = 50
            GV1.Columns("Item Name").HeaderText = "Item Name"

            If Not chkSerializeInv.Checked Then
                GV1.Columns("itf_code").IsVisible = True
                GV1.Columns("itf_code").Width = 60
                GV1.Columns("itf_code").HeaderText = "ITF Code"

                GV1.Columns("Qty").IsVisible = True
                GV1.Columns("Qty").Width = 50
                GV1.Columns("Qty").HeaderText = "Qty"

                GV1.Columns("LandedCost/Item").IsVisible = True
                GV1.Columns("LandedCost/Item").Width = 50
                GV1.Columns("LandedCost/Item").HeaderText = "LandedCost/Item"
            Else
                GV1.Columns("Serial No").IsVisible = True
                GV1.Columns("Serial No").Width = 50
                GV1.Columns("Serial No").HeaderText = "Item Serial No"

            End If



            If Not (clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal AndAlso chkItem.Checked = False AndAlso chkCategory.Checked = False AndAlso chkGroupBy.Checked = False) AndAlso chkSerializeInv.Checked = False Then
                GV1.Columns("ExciseRate").IsVisible = True
                GV1.Columns("ExciseRate").Width = 50
                GV1.Columns("ExciseRate").HeaderText = "ExciseRate"

                GV1.Columns("HCessRate").IsVisible = True
                GV1.Columns("HCessRate").Width = 50
                GV1.Columns("HCessRate").HeaderText = "HCessRate"

                GV1.Columns("ECessRate").IsVisible = True
                GV1.Columns("ECessRate").Width = 50
                GV1.Columns("ECessRate").HeaderText = "ECessRate"

                GV1.Columns("VATRATE").IsVisible = True
                GV1.Columns("VATRATE").Width = 50
                GV1.Columns("VATRATE").HeaderText = "VATRATE"

                GV1.Columns("NO VAT RATE").IsVisible = True
                GV1.Columns("NO VAT RATE").Width = 50
                GV1.Columns("NO VAT RATE").HeaderText = "NO VAT RATE"

                GV1.Columns("Entry Tax").IsVisible = False
                'GV1.Columns("Entry Tax").VisibleInColumnChooser
                GV1.Columns("Entry Tax").HeaderText = "Entry Tax"

            End If


            Try
                GV1.Columns("Category").IsVisible = True
                GV1.Columns("Category").Width = 100
                GV1.Columns("Category").HeaderText = "Item Category"
            Catch ex As Exception
            End Try
        End If

        If Not chkSerializeInv.Checked Then
            GV1.Columns("Basic Value").IsVisible = True
            GV1.Columns("Basic Value").Width = 50
            GV1.Columns("Basic Value").HeaderText = "Basic Value"

            GV1.Columns("Disc_Amt").IsVisible = True
            GV1.Columns("Disc_Amt").Width = 50
            GV1.Columns("Disc_Amt").HeaderText = "Discount Amount"

            GV1.Columns("Excise").IsVisible = True
            GV1.Columns("Excise").Width = 50
            GV1.Columns("Excise").HeaderText = "Excise"

            GV1.Columns("ECess").IsVisible = True
            GV1.Columns("ECess").Width = 50
            GV1.Columns("ECess").HeaderText = "ECess"

            GV1.Columns("HCess").IsVisible = True
            GV1.Columns("HCess").Width = 50
            GV1.Columns("HCess").HeaderText = "HCess"

            GV1.Columns("NO VAT").IsVisible = True
            GV1.Columns("NO VAT").Width = 50
            GV1.Columns("NO VAT").HeaderText = "NO VAT"

            GV1.Columns("VAT").IsVisible = True
            GV1.Columns("VAT").Width = 50
            GV1.Columns("VAT").HeaderText = "VAT"

            GV1.Columns("Others").IsVisible = True
            GV1.Columns("Others").Width = 50
            GV1.Columns("Others").HeaderText = "Others"


            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GUNTUR") = CompairStringResult.Equal AndAlso chkItem.Checked = False AndAlso chkCategory.Checked = False AndAlso chkGroupBy.Checked = False Then
                GV1.Columns("Total Basic").IsVisible = True
                GV1.Columns("Total Basic").Width = 50
                GV1.Columns("Total Basic").HeaderText = "Total Basic"

                GV1.Columns("Difference").IsVisible = True
                GV1.Columns("Difference").Width = 50
                GV1.Columns("Difference").HeaderText = "Difference"
            Else
                GV1.Columns("TCS").IsVisible = False
                'GV1.Columns("TCS").Width = 50
                GV1.Columns("TCS").HeaderText = "TCS"

            End If


            If chkCategory.Checked Then
                GV1.Columns("LandedCost/Item").IsVisible = True
                GV1.Columns("LandedCost/Item").Width = 50
                GV1.Columns("LandedCost/Item").HeaderText = "LandedCost/Item"
            End If

            GV1.Columns("FA Post").IsVisible = True
            GV1.Columns("FA Post").Width = 50
            GV1.Columns("FA Post").HeaderText = "Total Landed Cost"

            GV1.Columns("DocType").IsVisible = True
            GV1.Columns("DocType").Width = 50
            GV1.Columns("DocType").HeaderText = "DocType"

            GV1.Columns("remarks").IsVisible = True
            GV1.Columns("remarks").Width = 50
            GV1.Columns("remarks").HeaderText = "Remarks"
            If chkItem.Checked = False AndAlso chkCategory.Checked = False AndAlso chkGroupBy.Checked = False Then
                GV1.Columns("Uom").IsVisible = True
                GV1.Columns("Uom").Width = 50
                GV1.Columns("Uom").HeaderText = "UOM"

            End If
            '' currency details
            If objCommonVar.IsDemoERP Then
                GV1.Columns("CURRENCY_CODE").IsVisible = True
                GV1.Columns("CURRENCY_CODE").Width = 50
                GV1.Columns("CURRENCY_CODE").HeaderText = "Currency Code"

                GV1.Columns("ConvRate").IsVisible = True
                GV1.Columns("ConvRate").Width = 50
                GV1.Columns("ConvRate").HeaderText = "Conversion Rate"

                GV1.Columns("Total In Base Currency").IsVisible = True
                GV1.Columns("Total In Base Currency").Width = 100
                GV1.Columns("Total In Base Currency").HeaderText = "Total In Base Currency"

                Try
                    GV1.Columns("Category").IsVisible = True
                    GV1.Columns("Category").Width = 100
                    GV1.Columns("Category").HeaderText = "Item Category"
                Catch ex As Exception
                End Try
            End If

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0


            Dim item8 As New GridViewSummaryItem("Basic Value", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item8)

            If clsCommon.myLen(txtUOM.Value) > 0 Then
                Dim item9 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item9)
            End If


            Dim item7 As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item7)
            Dim item10 As New GridViewSummaryItem("Total in Base Currency", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item10)

            GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
        RadPageView1.SelectedPage = RadPageViewPage2

    End Sub

    Private Sub FormatGridItemWise()
        GV1.AllowAddNewRow = False
        GV1.ShowGroupPanel = False
        GV1.Columns("Item Code").IsVisible = False
        GV1.Columns("itf_code").IsVisible = False

        GV1.Columns("Bill Date").Width = 80
        GV1.Columns("Bill Date").HeaderText = "DATE"

        GV1.Columns("Bill NO").Width = 100
        GV1.Columns("Bill NO").HeaderText = "BILL"

        GV1.Columns("Party").Width = 120
        GV1.Columns("Party").HeaderText = "VENDOR CODE"

        GV1.Columns("Party Name").Width = 300
        GV1.Columns("Party Name").HeaderText = "VENDOR NAME"

        Try
            GV1.Columns("Uom").IsVisible = True
            GV1.Columns("Uom").Width = 50
            GV1.Columns("Uom").HeaderText = "UOM"
        Catch ex As Exception

        End Try

        GV1.Columns("Qty").Width = 70
        GV1.Columns("Qty").HeaderText = "QUANTITY"

        GV1.Columns("LandedCost/Item").Width = 50
        GV1.Columns("LandedCost/Item").HeaderText = "RATE"

        GV1.Columns("Total").Width = 100
        GV1.Columns("Total").HeaderText = "AMOUNT"

        GV1.GroupDescriptors.Add(New GridGroupByExpression("Category format ""{1}"" Group By Category"))
        'GV1.GroupDescriptors.Add(New GridGroupByExpression("[Sub Category] as SubCategory format ""{0}: {1}"" Group By [Sub Category]"))
        GV1.GroupDescriptors.Add(New GridGroupByExpression("[Item Name] as Item format ""{0}: {1}"" Group By [Item Name]"))
        GV1.MasterTemplate.ExpandAllGroups()
        GV1.MasterTemplate.AutoExpandGroups = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim Qty As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Qty)
        Dim total As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(total)
        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub


    Public Sub FillGridView(ByVal sql As String, ByVal gv As RadGridView)
        Dim bs As New BindingSource()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql)

        If chkGroupBy.Checked AndAlso pnlAdminSetting.Visible AndAlso chkReconcile.Checked Then
            Dim dtAS As DataTable = clsReconciliationSetting.GetAccounts(clsRecoSettingReportName.PurchaseBook, IIf(rdbtnFinishedGood.IsChecked, clsRecoSettingReportComponent.PurchaseBookFAAccountFG, clsRecoSettingReportComponent.PurchaseBookFAAccountOG), dtpFromdate1.Value, dtpToDate.Value)
            Dim arr As Dictionary(Of String, clsTempDrCrAmt) = Nothing
            dt.Columns.Add("SubledgerAmt", GetType(Double))

            If dtAS IsNot Nothing AndAlso dtAS.Rows.Count > 0 Then
                arr = New Dictionary(Of String, clsTempDrCrAmt)
                For Each dr As DataRow In dtAS.Rows
                    Dim obj As clsTempDrCrAmt = New clsTempDrCrAmt()
                    obj.DrAmt = clsCommon.myCdbl(dr("SubledgerAmt"))

                    arr.Add(clsCommon.myCstr(clsCommon.myCstr(dr("docNo")) + clsCommon.myCstr(dr("DocType"))).ToUpper(), obj)
                Next
                For ii As Integer = 0 To dt.Rows.Count - 1
                    Dim strSourceDocNo As String = clsCommon.myCstr(clsCommon.myCstr(dt.Rows(ii)("Document No")) + clsCommon.myCstr(dt.Rows(ii)("DocType"))).ToUpper()
                    If arr.ContainsKey(strSourceDocNo) Then
                        dt.Rows(ii)("SubledgerAmt") = clsCommon.myCdbl(arr.Item(strSourceDocNo).DrAmt)

                    End If
                Next
            End If

            If chkMismatch.Checked Then
                Dim dtView As DataView = dt.DefaultView
                dtView.RowFilter = "  (SubledgerAmt<>[FA Post])"
            End If
        End If


        bs.DataSource = dt
        gv.DataSource = bs
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)

        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromdate1.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))

            'If chkCategorySelect.IsChecked Then
            '    strTemp = ""
            '    For Each Str As String In cbgCategory.CheckedDisplayMember
            '        If clsCommon.myLen(strTemp) > 0 Then
            '            strTemp += ", "
            '        End If
            '        strTemp += Str
            '    Next
            '    arrHeader.Add("Customer Category : " + strTemp)
            'End If
            'If chkSubCategroySelect.IsChecked Then
            '    strTemp = ""
            '    For Each Str As String In cbgSubCategroy.CheckedValue
            '        If clsCommon.myLen(strTemp) > 0 Then
            '            strTemp += ", "
            '        End If
            '        strTemp += Str
            '    Next
            '    arrHeader.Add("Sub Category : " + strTemp)
            'End If
            If txtPOInvoice.arrDispalyMember IsNot Nothing AndAlso txtPOInvoice.arrDispalyMember.Count > 0 Then
                arrHeader.Add("PO Invoice : " + clsCommon.GetMulcallStringWithComma(txtPOInvoice.arrDispalyMember))
            End If
          
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                arrHeader.Add(" Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If

            'If chkPoInvoiceSelect.IsChecked Then
            '    strTemp = ""
            '    For Each Str As String In cbgPoInvoice.CheckedValue
            '        If clsCommon.myLen(strTemp) > 0 Then
            '            strTemp += ", "
            '        End If
            '        strTemp += Str
            '    Next
            '    arrHeader.Add("PO Invoice : " + strTemp)
            'End If

            'If chkLocationSelect.IsChecked Then
            '    strTemp = ""
            '    For Each Str As String In cbgLocation.CheckedValue
            '        If clsCommon.myLen(strTemp) > 0 Then
            '            strTemp += ", "
            '        End If
            '        strTemp += Str
            '    Next
            '    arrHeader.Add("Location : " + strTemp)
            'End If

            'If chkItemSelect.IsChecked Then
            '    strTemp = ""
            '    For Each Str As String In cbgItem.CheckedValue
            '        If clsCommon.myLen(strTemp) > 0 Then
            '            strTemp += ", "
            '        End If
            '        strTemp += Str
            '    Next
            '    arrHeader.Add("Item : " + strTemp)
            'End If

            'If chkVendorSelect.IsChecked Then
            '    strTemp = ""
            '    For Each Str As String In cbgVendor.CheckedValue
            '        If clsCommon.myLen(strTemp) > 0 Then
            '            strTemp += ", "
            '        End If
            '        strTemp += Str
            '    Next
            '    arrHeader.Add("Vendor : " + strTemp)
            'End If


            If exporter = EnumExportTo.Excel Then
                If Not chkItem.Checked Then
                    clsCommon.MyExportToExcel("Purchase Book", GV1, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToExcelGrid("Purchase Book", GV1, arrHeader, Me.Text)
                End If
            Else
                clsCommon.MyExportToPDF("Purchase Book", GV1, arrHeader, Me.Text, True)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub exporter_ExcelCellFormatting(ByVal sender As Object, ByVal e As ExcelML.ExcelCellFormattingEventArgs)
    End Sub
    Public Sub ResetData()
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        'rbtnCategoryAll.IsChecked = True
        RbCategoryAll.IsChecked = True
        chkItemAll.IsChecked = True
        chkLocationAll.IsChecked = True
        rbtnCategorySelect.IsChecked = False
        chkPoInvoiceAll.IsChecked = True
        chkVendorAll.IsChecked = True
        chkAccountAll.IsChecked = True
        LoadAccounts()
        ItemLoad()
        LoadCategory()
        LoadLocation()
        LoadPoInvoice()
        LoadVendor()
        LoadItemType()
        chkCategory.Checked = False
        chkGroupBy.Checked = False
        'rdbtnFinishedGood.IsChecked = True
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Private Sub chkPoInvoiceAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkPoInvoiceAll.ToggleStateChanged
        cbgPoInvoice.Enabled = Not chkPoInvoiceAll.IsChecked
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub
    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        ResetData()
    End Sub
    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox1.Click

    End Sub


    Private Sub FrmPurchasebookReport1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown


        If e.Alt And e.KeyCode = Keys.P Then
            PrintData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            ResetData()
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F12 Then
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
            chkGroupBy.Checked = True
        End If
    End Sub


    Private Sub chkGroupBy_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkGroupBy.ToggleStateChanged, RadCheckBox1.ToggleStateChanged
        chkCategory.Checked = False
        chkItem.Checked = False
    End Sub

    Private Sub chkItem_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItem.ToggleStateChanged, RadCheckBox2.ToggleStateChanged
        chkCategory.Checked = False
        chkGroupBy.Checked = False
    End Sub

    Private Sub txtUOM__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub

   
    Private Sub Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Export.Click
        If GV1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PDF.Click
        If GV1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub chkAccountAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAccountAll.ToggleStateChanged
        cbgAccounts.Enabled = Not chkAccountAll.IsChecked
    End Sub

    'Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
    '    tvCategory.Enabled = rbtnCategorySelect.IsChecked
    'End Sub

    Private Sub rdraw_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdraw.ToggleStateChanged
        If rdraw.Checked = True Then
            rdbtnFinishedGood.IsChecked = False
            rdbtnOther.IsChecked = False
            chkAll.IsChecked = False
        ElseIf rdraw.Checked = False Then
            rdbtnFinishedGood.IsChecked = False
            rdbtnOther.IsChecked = False
            chkAll.IsChecked = True
        End If
    End Sub

    Private Sub rdfinish_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdfinish.ToggleStateChanged
        If rdfinish.Checked = True Then
            rdbtnFinishedGood.IsChecked = True
            rdbtnOther.IsChecked = False
            chkAll.IsChecked = False
        ElseIf rdfinish.Checked = False Then
            rdbtnFinishedGood.IsChecked = False
            rdbtnOther.IsChecked = False
            chkAll.IsChecked = True
        End If
    End Sub

    Private Sub rdother_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdother.ToggleStateChanged
        If rdother.Checked = True Then
            rdbtnFinishedGood.IsChecked = False
            rdbtnOther.IsChecked = True
            chkAll.IsChecked = False
        ElseIf rdother.Checked = False Then
            rdbtnFinishedGood.IsChecked = False
            rdbtnOther.IsChecked = False
            chkAll.IsChecked = True
        End If
    End Sub

    Private Sub rdasset_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdasset.ToggleStateChanged
        If rdasset.Checked = True Then
            rdbtnFinishedGood.IsChecked = False
            rdbtnOther.IsChecked = False
            chkAll.IsChecked = False
        ElseIf rdasset.Checked = False Then
            rdbtnFinishedGood.IsChecked = False
            rdbtnOther.IsChecked = False
            chkAll.IsChecked = True
        End If
    End Sub

  
    Private Sub GV1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles GV1.CellDoubleClick
        If GV1.Rows.Count > 0 Then
            Dim strDoc
            Dim strTransType As String = clsCommon.myCstr(GV1.CurrentRow.Cells("DocType").Value)
            If GV1.CurrentColumn.HeaderText.Equals("Document No") Then
                strDoc = GV1.CurrentRow.Cells("Document No").Value

                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, strDoc)
            Else
                strDoc = GV1.CurrentRow.Cells("Vr No.").Value
                If clsCommon.CompairString(strTransType, "PR") = CompairStringResult.Equal Then

                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, strDoc)
                Else
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseInvoice, strDoc)
                End If
            End If
        End If
    End Sub

    Private Sub RbCategoryAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles RbCategoryAll.ToggleStateChanged
        gvCategory.Enabled = RbcategorySelect.IsChecked
    End Sub

    Private Sub gvCategory_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCategory.CellDoubleClick
        If clsCommon.myCBool(gvCategory.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 2
            frm.strCode = clsCommon.myCstr(gvCategory.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvCategory.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvCategory.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub
    Public Sub ItemLoad()
        qry = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
    End Sub

    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub

    Public Sub LoadPoInvoice()
        Dim Qry As String = " select Vendor_Invoice_No  as Code ,PI_date as Date from TSPL_PI_HEAD "
        cbgPoInvoice.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgPoInvoice.ValueMember = "Code"
    End Sub

    Public Sub LoadVendor()
        Dim Qry As String = "select Vendor_Code as Code,Vendor_Name as Description from TSPL_VENDOR_MASTER "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgVendor.ValueMember = "Code"
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Description", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = " select Vendor_Code as Code,Vendor_Name as Description from TSPL_VENDOR_MASTER "
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Description", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub txtPOInvoice__My_Click(sender As Object, e As EventArgs) Handles txtPOInvoice._My_Click
        Dim qry As String = " select Vendor_Invoice_No  as Code ,PI_date as Date from TSPL_PI_HEAD "
        txtPOInvoice.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Date", txtPOInvoice.arrValueMember, txtPOInvoice.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub
End Class
