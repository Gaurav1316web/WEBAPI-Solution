Imports System.IO
Imports common
Imports System.Text
Imports common.UserControls
Public Class ItemRateStatusReport
    Inherits FrmMainTranScreen

    Private Sub ItemRateStatusReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fromDate.Value = clsCommon.GETSERVERDATE()

    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code as [Code], Item_Desc as [Name] ,CASE WHEN Active = 1 THEN 'Active' WHEN Active = 0 THEN 'Inactive'END AS Status, CASE 
       -- WHEN Is_FreshItem = 1 AND Is_FreshAmbient = 1 THEN 'Fresh Item & Ambient'
        WHEN ISNULL(Is_FreshItem,0) = 1 THEN 'Fresh Item'
        WHEN ISNULL(Is_FreshAmbient,0) = 1 THEN 'Fresh Ambient'
   ELSE '0'
    END AS ItemType  from TSPL_ITEM_MASTER "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub

    Private Sub txtPriceCode__My_Click(sender As Object, e As EventArgs) Handles txtPriceCode._My_Click
        '        Dim qry As String = " select TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code,TSPL_ITEM_PRICE_PLAN_DETAIL.price_Code from TSPL_ITEM_PRICE_PLAN_HEADER
        'left outer join TSPL_ITEM_PRICE_PLAN_DETAIL on TSPL_ITEM_PRICE_PLAN_DETAIL.Plan_Code=TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code "
        'Dim qry As String = "SELECT Price_Code as [Code],Price_Code_Desc as [Name] ,CASE 
        'WHEN ISNULL(Inactive, 0) = 0 THEN 'InActive'
        'ELSE 'active' FROM TSPL_PRICE_COMPONENT_MAPPING"
        Dim qry As String = " SELECT Plan_Code AS Code,Remarks as [Price Name], CASE WHEN ISNULL(Post_Status, 0) = 0 THEN 'InActive'ELSE 'active' end as Status FROM TSPL_ITEM_PRICE_PLAN_HEADER"

        txtPriceCode.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "", txtPriceCode.arrValueMember, txtItem.arrDispalyMember)

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Item_Rate_Status_Report()
    End Sub
    Private Sub Item_Rate_Status_Report()
        Dim q As String = ""
        Dim qry As String = " "
        Dim Item_Type As String = Nothing
        Dim dt As New DataTable()
        Try
            Dim whr As String = Nothing
            'whr += " where Convert( Date, Start_Date,103) = convert(Date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "',103) "
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                whr += " and TSPL_ITEM_PRICE_PLAN_detail.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")  "
            End If

            If txtPriceCode.arrValueMember IsNot Nothing AndAlso txtPriceCode.arrValueMember.Count > 0 Then
                whr += " and Plan_Code in (" + clsCommon.GetMulcallString(txtPriceCode.arrValueMember) + ")  "
            End If
            Dim strPricecode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TOP 1 Plan_Code
FROM TSPL_ITEM_PRICE_PLAN_HEADER
WHERE Start_Date <= CONVERT(DATE, '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "', 103)
ORDER BY Start_Date DESC"))

            '            qry = "select Start_Date as Date,TSPL_ITEM_PRICE_PLAN_detail.Item_Code as Item_code,UOM,TSPL_ITEM_MASTER.Item_Desc,Item_MRP as MPR,PRICE_CODE,
            '(Price_Amount1+Price_Amount2+Price_Amount3+Price_Amount4+Price_Amount5+Price_Amount6+Price_Amount7+Price_Amount8+Price_Amount9+Price_Amount10 ) AS Margin,Item_Basic_Price
            'as [Inc. Rate], Item_Selling_Price as [Exc. Rate]
            'from  TSPL_ITEM_PRICE_PLAN_HEADER 
            'left outer join TSPL_ITEM_PRICE_PLAN_detail on TSPL_ITEM_PRICE_PLAN_detail.Plan_Code=TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code
            'LEFT JOIN TSPL_ITEM_MASTER  
            '    ON TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PRICE_PLAN_detail.Item_Code

            '" + whr + " and " + strPricecode + ""
            '            qry = "DECLARE @cols NVARCHAR(MAX) = '';
            'DECLARE @sql NVARCHAR(MAX) = '';

            '-- Step 1: Build dynamic column list based on distinct PRICE_CODEs
            'SELECT @cols = @cols + 
            '    ',[' + PRICE_CODE + ' MRP]' +
            '    ',[' + PRICE_CODE + ' Margin]' +
            '    ',[' + PRICE_CODE + ' Inc_Rate]' +
            '    ',[' + PRICE_CODE + ' Exc_Rate]'
            'FROM (
            '    SELECT DISTINCT TSPL_ITEM_PRICE_PLAN_detail.PRICE_CODE
            '    FROM TSPL_ITEM_PRICE_PLAN_HEADER 
            '    LEFT JOIN TSPL_ITEM_PRICE_PLAN_detail  ON TSPL_ITEM_PRICE_PLAN_detail.Plan_Code = TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code
            '    WHERE TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code = '" + strPricecode + "'
            '      AND TSPL_ITEM_PRICE_PLAN_detail.PRICE_CODE IS NOT NULL
            ') AS pc
            'ORDER BY PRICE_CODE;

            '-- Remove leading comma
            'SET @cols = STUFF(@cols, 1, 1, '');

            '-- Step 2: Build dynamic pivot query
            'SET @sql = '
            'WITH Base AS (
            '    SELECT 
            '        CONVERT(VARCHAR,TSPL_ITEM_PRICE_PLAN_HEADER.Start_Date,103) AS Date,
            '        TSPL_ITEM_PRICE_PLAN_detail.Item_Code,
            '        TSPL_ITEM_MASTER.Item_Desc,
            '        TSPL_ITEM_PRICE_PLAN_detail.UOM,
            '        TSPL_ITEM_PRICE_PLAN_detail.PRICE_CODE,
            '        TSPL_ITEM_PRICE_PLAN_detail.Item_MRP                                                          AS MRP,
            '        (TSPL_ITEM_PRICE_PLAN_detail.Price_Amount1 + TSPL_ITEM_PRICE_PLAN_detail.Price_Amount2 + TSPL_ITEM_PRICE_PLAN_detail.Price_Amount3 +
            '         TSPL_ITEM_PRICE_PLAN_detail.Price_Amount4 + TSPL_ITEM_PRICE_PLAN_detail.Price_Amount5 + TSPL_ITEM_PRICE_PLAN_detail.Price_Amount6 +
            '         TSPL_ITEM_PRICE_PLAN_detail.Price_Amount7 + TSPL_ITEM_PRICE_PLAN_detail.Price_Amount8 + TSPL_ITEM_PRICE_PLAN_detail.Price_Amount9 +
            '         TSPL_ITEM_PRICE_PLAN_detail.Price_Amount10)                                                  AS Margin,
            '        TSPL_ITEM_PRICE_PLAN_detail.Item_Basic_Price                                                  AS Inc_Rate,
            '        TSPL_ITEM_PRICE_PLAN_detail.Item_Selling_Price                                                AS Exc_Rate
            '    FROM TSPL_ITEM_PRICE_PLAN_HEADER 
            '    LEFT JOIN TSPL_ITEM_PRICE_PLAN_detail  ON TSPL_ITEM_PRICE_PLAN_detail.Plan_Code = TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code
            '    LEFT JOIN TSPL_ITEM_MASTER            ON TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PRICE_PLAN_detail.Item_Code
            '    	left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=TSPL_ITEM_MASTER.item_code

            '    WHERE 2=2 "
            '            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            '                qry += " and TSPL_ITEM_PRICE_PLAN_detail.Item_Code in ('" + clsCommon.GetMulcallString(txtItem.arrValueMember) + "')  "
            '            End If
            '            If txtPriceCode.arrValueMember IsNot Nothing AndAlso txtPriceCode.arrValueMember.Count > 0 Then
            '                qry += " and PRICE_CODE in ('" + clsCommon.GetMulcallString(txtPriceCode.arrValueMember) + "')  "
            '            End If
            '            qry += " AND TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code = ''" + strPricecode + "''"
            '            If rbtnDefaultUOM.IsChecked Then
            '                qry += "and Default_UOM=''1''"
            '            ElseIf rbtnReportUOM.IsChecked Then
            '                qry += "and Report_UOM=''1''"
            '            ElseIf rbtnPrintUOM.IsChecked Then
            '                qry += "and Print_UOM=''1''"

            '            End If

            '            qry +="),
            'Unpivoted AS (
            '    SELECT Date, Item_Code, Item_Desc, UOM,
            '           PRICE_CODE + ''_MRP''      AS ColName, CAST(MRP      AS NVARCHAR(50)) AS Val FROM Base
            '    UNION ALL
            '    SELECT Date, Item_Code, Item_Desc, UOM,
            '           PRICE_CODE + ''_Margin''   AS ColName, CAST(Margin   AS NVARCHAR(50)) AS Val FROM Base
            '    UNION ALL
            '    SELECT Date, Item_Code, Item_Desc, UOM,
            '           PRICE_CODE + ''_Inc_Rate'' AS ColName, CAST(Inc_Rate AS NVARCHAR(50)) AS Val FROM Base
            '    UNION ALL
            '    SELECT Date, Item_Code, Item_Desc, UOM,
            '           PRICE_CODE + ''_Exc_Rate'' AS ColName, CAST(Exc_Rate AS NVARCHAR(50)) AS Val FROM Base
            ')
            'SELECT Date, Item_Code, Item_Desc, UOM, ' + @cols + '
            'FROM Unpivoted
            'PIVOT (
            '    MAX(Val) FOR ColName IN (' + @cols + ')
            ') AS PivotResult
            'ORDER BY Item_Code;
            '';

            '-- Step 3: Execute
            'EXEC sp_executesql @sql;"



            qry = "DECLARE @cols NVARCHAR(MAX) = '';
DECLARE @sql NVARCHAR(MAX) = '';

-- Step 1: Build dynamic column list based on distinct PRICE_CODEs
SELECT @cols = @cols + 
    ',[' + PRICE_CODE + ' MRP]' +
    ',[' + PRICE_CODE + ' Margin]' +
    ',[' + PRICE_CODE + ' Inc_Rate]' +
    ',[' + PRICE_CODE + ' Exc_Rate]'
FROM (
    SELECT DISTINCT TSPL_ITEM_PRICE_PLAN_detail.PRICE_CODE
    FROM TSPL_ITEM_PRICE_PLAN_HEADER 
    LEFT JOIN TSPL_ITEM_PRICE_PLAN_detail  ON TSPL_ITEM_PRICE_PLAN_detail.Plan_Code = TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code"
            If txtPriceCode.arrValueMember IsNot Nothing AndAlso txtPriceCode.arrValueMember.Count > 0 Then
                qry += " Where TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code IN (" + clsCommon.GetMulcallString(txtPriceCode.arrValueMember) + ")"
            Else
                qry += " WHERE TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code = '" + strPricecode + "'"
                'qry += " And PRICE_CODE In ('" + clsCommon.GetMulcallString(txtPriceCode.arrValueMember) + "')  "
            End If


            qry += "
      AND TSPL_ITEM_PRICE_PLAN_detail.PRICE_CODE IS NOT NULL
) AS pc
ORDER BY PRICE_CODE;

-- Remove leading comma
SET @cols = STUFF(@cols, 1, 1, '');

-- Step 2: Build dynamic pivot query
SET @sql = '
WITH Base AS (
    SELECT 
        CONVERT(VARCHAR,TSPL_ITEM_PRICE_PLAN_HEADER.Start_Date,103) AS Date,
        TSPL_ITEM_PRICE_PLAN_detail.Item_Code,
        TSPL_ITEM_MASTER.Item_Desc,
        TSPL_ITEM_PRICE_PLAN_detail.UOM,
        TSPL_ITEM_PRICE_PLAN_detail.PRICE_CODE,
        TSPL_ITEM_PRICE_PLAN_detail.Item_MRP                                                          AS MRP,
        (TSPL_ITEM_PRICE_PLAN_detail.Price_Amount1 + TSPL_ITEM_PRICE_PLAN_detail.Price_Amount2 + TSPL_ITEM_PRICE_PLAN_detail.Price_Amount3 +
         TSPL_ITEM_PRICE_PLAN_detail.Price_Amount4 + TSPL_ITEM_PRICE_PLAN_detail.Price_Amount5 + TSPL_ITEM_PRICE_PLAN_detail.Price_Amount6 +
         TSPL_ITEM_PRICE_PLAN_detail.Price_Amount7 + TSPL_ITEM_PRICE_PLAN_detail.Price_Amount8 + TSPL_ITEM_PRICE_PLAN_detail.Price_Amount9 +
         TSPL_ITEM_PRICE_PLAN_detail.Price_Amount10)                                                  AS Margin,
        TSPL_ITEM_PRICE_PLAN_detail.Item_Basic_Price                                                  AS Inc_Rate,
        TSPL_ITEM_PRICE_PLAN_detail.Item_Selling_Price                                                AS Exc_Rate
    FROM TSPL_ITEM_PRICE_PLAN_HEADER 
    LEFT JOIN TSPL_ITEM_PRICE_PLAN_detail  ON TSPL_ITEM_PRICE_PLAN_detail.Plan_Code = TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code
    LEFT JOIN TSPL_ITEM_MASTER            ON TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PRICE_PLAN_detail.Item_Code
	    	left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.item_code=TSPL_ITEM_MASTER.item_code

    WHERE 1=1  "


            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += " AND TSPL_ITEM_PRICE_PLAN_detail.Item_Code IN ('" + clsCommon.GetMulcallString(txtItem.arrValueMember).Replace(",", "','") + "')"
                ' qry += " and TSPL_ITEM_PRICE_PLAN_detail.Item_Code in ('" + clsCommon.GetMulcallString(txtItem.arrValueMember) + "')  "
            End If
            If txtPriceCode.arrValueMember IsNot Nothing AndAlso txtPriceCode.arrValueMember.Count > 0 Then
                qry += " AND TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code IN ('" + clsCommon.GetMulcallString(txtPriceCode.arrValueMember).Replace(",", "','") + "')"
            Else
                qry += " and  TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code  = ''" + strPricecode + "''"
                'qry += " And PRICE_CODE In ('" + clsCommon.GetMulcallString(txtPriceCode.arrValueMember) + "')  "
            End If
            'qry += " AND TSPL_ITEM_PRICE_PLAN_HEADER.Plan_Code = ''" + strPricecode + "''"
            If rbtnDefaultUOM.IsChecked Then
                qry += "and Default_UOM=''1''"
            ElseIf rbtnReportUOM.IsChecked Then
                qry += "and Report_UOM=''1''"
            ElseIf rbtnPrintUOM.IsChecked Then
                qry += "and Print_UOM=''1''"

            End If

            qry += "),
Unpivoted AS (
    SELECT Date, Item_Code, Item_Desc, UOM,
           PRICE_CODE + '' MRP''      AS ColName, CAST(MRP      AS NVARCHAR(50)) AS Val FROM Base
    UNION ALL
    SELECT Date, Item_Code, Item_Desc, UOM,
           PRICE_CODE + '' Margin''   AS ColName, CAST(Margin   AS NVARCHAR(50)) AS Val FROM Base
    UNION ALL
    SELECT Date, Item_Code, Item_Desc, UOM,
           PRICE_CODE + '' Inc_Rate'' AS ColName, CAST(Inc_Rate AS NVARCHAR(50)) AS Val FROM Base
    UNION ALL
    SELECT Date, Item_Code, Item_Desc, UOM,
           PRICE_CODE + '' Exc_Rate'' AS ColName, CAST(Exc_Rate AS NVARCHAR(50)) AS Val FROM Base
)
SELECT Date, Item_Code, Item_Desc, UOM, ' + @cols + '
FROM Unpivoted
PIVOT (
    MAX(Val) FOR ColName IN (' + @cols + ')
) AS PivotResult
ORDER BY Item_Code;
';

EXEC sp_executesql @sql;"



            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.GroupDescriptors.Clear()
                Gv1.SummaryRowsBottom.Clear()
                Gv1.DataSource = dt
                'gv1.Columns("TransType").IsVisible = False
                'gv1.Columns("PROD_ENTRY_CODE").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                FormatGrid()
                EnableDisableCntrl(False)
                'ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display.", "Item Stock Report")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub EnableDisableCntrl(ByVal val As Boolean)
        RadGroupBox3.Enabled = False
        RadGroupBox2.Enabled = False

    End Sub
    Sub FormatGrid()
        Gv1.AutoExpandGroups = False
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Gv1.Columns("Item_Code").HeaderText = "Item Code"
        Gv1.Columns("Item_Code").IsVisible = True
        Gv1.Columns("Item_Desc").IsVisible = True
        Gv1.Columns("Item_Desc").HeaderText = "Item Name"

        Gv1.ShowGroupPanel = True
        Gv1.MasterTemplate.AutoExpandGroups = True
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        '  fromDate.Value = clsCommon.GETSERVERDATE()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        RadGroupBox2.Enabled = True
        RadGroupBox3.Enabled = True
        rbtnReportUOM.IsChecked = True
        rbtnPrintUOM.IsChecked = False
        rbtnDefaultUOM.IsChecked = False
        txtItem.arrValueMember = Nothing
        txtPriceCode.arrValueMember = Nothing

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Export(EnumExportTo.Excel)
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.ItemRateStatusReport & "'"))

                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


        'Try
        '    If Gv1.Rows.Count > 0 Then
        '        Dim arrHeader As List(Of String) = New List(Of String)()
        '        arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        '        arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.ItemRateStatusReport & "'"))
        '        transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
        '    Else
        '        clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        'End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim BaseQuery As String = Nothing
            BaseQuery = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQuery)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                'SetGridFormat()
                ' SetGridFormationOFGV1Collection()
                ' View()
                EnableDisableCntrl(False)
                Gv1.BestFitColumns()

            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class