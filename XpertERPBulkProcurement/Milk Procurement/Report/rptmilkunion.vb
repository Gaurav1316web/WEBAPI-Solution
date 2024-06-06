Imports common
Public Class rptmilkunion
    Inherits FrmMainTranScreen

    Private Sub rptmilkunion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.Shift AndAlso e.Alt AndAlso e.KeyCode = Keys.F12 Then
            chkRJSBNS.Visible = True
            chkRJSBNS.Checked = True
        Else
            chkRJSBNS.Visible = False
        End If
    End Sub
    Private Sub rptmilkunion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Dim serverDate As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        Dim previousDay As DateTime = serverDate.AddDays(-1)
        Dim previousDayString As String = clsCommon.GetPrintDate(previousDay, "dd/MMM/yyyy")
        txtFromDate.Value = previousDayString
        txtToDate.Value = previousDayString
        rdbPosted.Checked = True
        rdbUnposted.Checked = False
        chkRJSBNS.Visible = False
        chkRJSBNS.Checked = True
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Griddata(True)
    End Sub
    Sub SetGridFormat1()
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Plant as Plant format ""{0}: {1}"" Group By Plant"))
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Mcc as Mcc format ""{0}: {1}"" Group By Mcc"))
        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True


        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
        Next
        gv1.Columns("SNo").Name = "SNo"
        gv1.Columns("SNo").IsVisible = True '

        gv1.Columns("Union Name").HeaderText = "Union Name"
        gv1.Columns("Union Name").Width = 500
        gv1.Columns("Union Name").IsVisible = True

        gv1.Columns("Fromdate").HeaderText = "From Date"
        gv1.Columns("Fromdate").Width = 500
        gv1.Columns("Fromdate").IsVisible = False

        gv1.Columns("Todate").HeaderText = "To Date"
        gv1.Columns("Todate").Width = 500
        gv1.Columns("Todate").IsVisible = False



        gv1.Columns("Dis_QtyInLTR").HeaderText = "Proc QTY"
        gv1.Columns("Dis_QtyInLTR").Width = 200
        gv1.Columns("Dis_QtyInLTR").FormatString = "{0:n3}"

        gv1.Columns("Dis_FATKG").HeaderText = "Proc FATKG"
        gv1.Columns("Dis_FATKG").FormatString = "{0:n3}"

        gv1.Columns("Dis_SNFKG").HeaderText = " Proc SNFKG"
        gv1.Columns("Dis_SNFKG").IsVisible = True
        gv1.Columns("Dis_SNFKG").FormatString = "{0:n3}"


        gv1.Columns("TotalLtr_ItemWiseDemand").HeaderText = "Demand QTY"
        gv1.Columns("TotalLtr_ItemWiseDemand").IsVisible = True
        gv1.Columns("TotalLtr_ItemWiseDemand").FormatString = ""

        gv1.Columns("FATKGDemand").HeaderText = "Demand FATKG"
        gv1.Columns("FATKGDemand").IsVisible = True
        gv1.Columns("FATKGDemand").FormatString = "{0:n3}"

        gv1.Columns("SNFKGDemand").HeaderText = "Demand SNFKG"
        gv1.Columns("SNFKGDemand").IsVisible = True
        gv1.Columns("SNFKGDemand").FormatString = "{0:n3}"


        gv1.Columns("Milk_WeightProc").HeaderText = "Dispatch QTY"
        gv1.Columns("Milk_WeightProc").IsVisible = True
        gv1.Columns("Milk_WeightProc").FormatString = "{0:n3}"

        gv1.Columns("FATKGProc").HeaderText = "Dispatch FATKG"
        gv1.Columns("FATKGProc").IsVisible = True
        gv1.Columns("FATKGProc").FormatString = "{0:n3}"

        gv1.Columns("SNFKGProc").HeaderText = "Dispatch SNFKG"
        gv1.Columns("SNFKGProc").IsVisible = True
        gv1.Columns("SNFKGProc").FormatString = "{0:n3}"

        gv1.Columns("Sale_Voucher").HeaderText = "Sale Voucher"
        gv1.Columns("Sale_Voucher").IsVisible = True
        gv1.Columns("Sale_Voucher").FormatString = "{0:n3}"

        gv1.Columns("Purchase_Voucher").HeaderText = "Purchase Voucher"
        gv1.Columns("Purchase_Voucher").IsVisible = True
        gv1.Columns("Purchase_Voucher").FormatString = "{0:n3}"

        gv1.Columns("Last_Salary").HeaderText = "Last Salary"
        gv1.Columns("Last_Salary").IsVisible = True

        gv1.ShowGroupPanel = True
        gv1.MasterTemplate.AutoExpandGroups = True

        View()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub View()

        If gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup("Union"))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Union Name").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(" Milk Procurement"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Milk_WeightProc").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("FATKGProc").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SNFKGProc").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Demand"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("TotalLtr_ItemWiseDemand").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("FATKGDemand").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SNFKGDemand").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Dispatch"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("Dis_QtyInLTR").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("Dis_FATKG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("Dis_SNFKG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Accounts"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Sale_Voucher").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns("Purchase_Voucher").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("PayRoll"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(gv1.Columns("Last_Salary").Name)

            gv1.ViewDefinition = view
        End If
    End Sub
    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Griddata(False)
    End Sub
    Public Sub Griddata(ByVal print As Boolean)
        Try
            Dim query As String
            Dim qry As String
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                query += "union all
  SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
  union all
  SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
  ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(query)
            query = ""
            Dim dtUnion As DataTable = New DataTable()
            Dim dt2 As DataTable = New DataTable()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        '  query += " UNION ALL "
                    End If
                    Dim status1 As String
                    Dim status2 As String
                    Dim status3 As String
                    Dim status4 As String
                    Dim status5 As String
                    Dim status6 As String
                    Dim status7 As String
                    Dim status8 As String
                    Dim status9 As String
                    If rdbPosted.Checked Then
                        status1 = " and  sh.Status=1 "
                        status2 = " and pe.POSTED= 1 "
                        status3 = " and dbm.Posted= 1 "
                        status4 = " and muh.Status= 1 "
                        status5 = "and msh.Status= 1"
                        status6 = "and TSPL_GENERATE_SALARY.posted= 1 "
                        status7 = "and TSPL_SD_SALE_INVOICE_HEAD.Status= 1 "
                        status8 = "and TSPL_MILK_PURCHASE_INVOICE_HEAD.Posted= 1 "
                        status9 = "and TSPL_Dispatch_BulkSale.Posted= 1 "
                    ElseIf rdbUnposted.Checked Then
                        status1 = " and  sh.Status= 0 "
                        status2 = " and pe.POSTED= 0 "
                        status3 = " and dbm.Posted= 0 "
                        status4 = " and muh.Status= 0 "
                        status5 = "and msh.Status= 0 "
                        status6 = "and TSPL_GENERATE_SALARY.posted= 0 "
                        status7 = "and TSPL_SD_SALE_INVOICE_HEAD.Status= 0 "
                        status8 = "and TSPL_MILK_PURCHASE_INVOICE_HEAD.Posted= 0 "
                        status9 = "and TSPL_Dispatch_BulkSale.Posted= 0 "
                    Else
                        status1 = " "
                        status2 = " "
                        status3 = " "
                        status4 = " "
                        status5 = " "
                        status6 = " "
                        status7 = " "
                        status8 = " "
                        status9 = " "
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "BKN") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "CHT") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "BNS") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "RJS") = CompairStringResult.Equal Then
                        qry = "  Select max(SNo)As SNo, max([Union Name])[Union Name] , MAX(Fromdate)Fromdate , MAX(Todate)Todate , MAX(username)username , SUM(Dis_QtyInLTR)Dis_QtyInLTR, SUM(Dis_FATKG)Dis_FATKG , SUM(Dis_SNFKG)Dis_SNFKG , SUM(TotalLtr_ItemWiseDemand)TotalLtr_ItemWiseDemand , SUM(FATKGDemand)FATKGDemand , SUM(SNFKGDemand)SNFKGDemand , SUM(Milk_WeightProc)Milk_WeightProc , SUM(FATKGProc)FATKGProc , SUM(SNFKGProc)SNFKGProc ,
                        SUM(Sale_Voucher)Sale_Voucher , SUM(Purchase_Voucher)Purchase_Voucher , MAX(Last_Salary)Last_Salary from ( " & Environment.NewLine & " "
                    End If
                    query = " select * from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Disbursement.Dis_QtyInLTR), 0) AS Dis_QtyInLTR,
                    ISNULL(SUM(Dis_Disbursement.Dis_FATKG), 0) AS Dis_FATKG,
                    ISNULL(SUM(Dis_Disbursement.Dis_SNFKG), 0) AS Dis_SNFKG,
                    ISNULL(SUM(Dis_Demand.TotalLtr_ItemWiseDemand), 0) AS TotalLtr_ItemWiseDemand,
                    ISNULL(SUM(Dis_Demand.FATKGDemand), 0) AS FATKGDemand,
                    ISNULL(SUM(Dis_Demand.SNFKGDemand), 0) AS SNFKGDemand,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc,
                    ISNULL(SUM(Sale_invoice.Sale_Voucher),0) AS Sale_Voucher,
					ISNULL(SUM(Milk_Purchase_invoice.Purchase_Voucher),0) AS Purchase_Voucher,
                    (SELECT TOP 1 DATENAME(MONTH, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO) + ' ' + CONVERT(VARCHAR(4), YEAR([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO)) FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GENERATE_SALARY
left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PAYPERIOD_MASTER on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GENERATE_SALARY.PAY_PERIOD_CODE = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE
where 2=2" + status6 + " ORDER BY DATE_TO DESC) as Last_Salary
                FROM 
(SELECT 
                     SUM(Dis_QtyInLTR) AS Dis_QtyInLTR,
                        SUM(Dis_FATKG) AS Dis_FATKG,
                        SUM(Dis_SNFKG) AS Dis_SNFKG
                    FROM (
                    ( SELECT 
                        SUM(CASE 
                                WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                ELSE 0 
                            END) AS Dis_QtyInLTR,
                        SUM(ISNULL((    
                            CASE 
                                WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                ELSE 0 
                            END
                        ) * STD_FatPer / 100,
                        0
                        )) AS Dis_FATKG,
                        SUM(ISNULL((
                            CASE 
                                WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                ELSE 0 
                            END
                        ) * STD_SNFPer / 100,
                        0
                        )) AS Dis_SNFKG
                    FROM 
                         [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL sd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master im ON im.Item_Code = sd.Item_Code
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD sh ON sh.Document_Code = sd.DOCUMENT_CODE
                    LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Crate') AS icc ON icc.Item_code = sd.Item_Code
                    LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Pouch') AS icp ON icp.Item_code = sd.Item_Code
                    LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'LTR') AS ilt ON ilt.Item_code = sd.Item_Code
                    WHERE 
                        CONVERT(DATE, sh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        AND im.IsTaxable = 0 
                        AND im.Is_FreshItem = 1  
                        " + status1 + "
                        union all
                        SELECT 
                      cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,
                        SUM(xxxx.Fat_KG ) AS FATKGDemand,
                        SUM(xxxx.SNF_KG) AS SNFKGDemand
from (
SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
LEFT JOIN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale  ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status9 + "
)xxxx ))Disp_BUlksale
                    ) AS Dis_Disbursement,


                        (SELECT 
                        SUM(TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand,
                        SUM(FATKGDemand) AS FATKGDemand,
                        SUM(SNFKGDemand) AS SNFKGDemand
                    FROM (
                    (SELECT 
                        SUM(TotalLtr_ItemWise) AS TotalLtr_ItemWiseDemand,
                        SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,
                        SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand
                    FROM 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
                    WHERE 
                        CONVERT(DATE, dbm.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        AND im.IsTaxable = 0
                        AND im.Is_FreshItem = 1
                        " + status3 + " 
                        union all
                        SELECT 
                      cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,
                        SUM(xxxx.Fat_KG ) AS FATKGDemand,
                        SUM(xxxx.SNF_KG) AS SNFKGDemand
from (
SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
FROM  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale
LEFT JOIN  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale  ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_No = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
inner join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

WHERE CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status9 + "
)xxxx ))Disp_BUlksale
                    ) AS Dis_Demand,



                    (SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status4 + "

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status5 + "

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        ) AS Procurement
                    ) AS Dis_Procurement,
                    (select sum([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt) as Sale_Voucher from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_DETAIL
left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Code
where  CONVERT(DATE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status7 + ") AS Sale_invoice,
(select SUM(TOTAL_AMOUNT) AS Purchase_Voucher from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PURCHASE_INVOICE_HEAD where  CONVERT(DATE, DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status8 + ") AS Milk_Purchase_invoice)final"


                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "BKN") = CompairStringResult.Equal Then
                        query = " " & qry & "" & query & " union all " & Environment.NewLine & "  select * from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                        '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                    ISNULL(SUM(Dis_Disbursement.Dis_QtyInLTR), 0) AS Dis_QtyInLTR,
                    ISNULL(SUM(Dis_Disbursement.Dis_FATKG), 0) AS Dis_FATKG,
                    ISNULL(SUM(Dis_Disbursement.Dis_SNFKG), 0) AS Dis_SNFKG,
                    ISNULL(SUM(Dis_Demand.TotalLtr_ItemWiseDemand), 0) AS TotalLtr_ItemWiseDemand,
                    ISNULL(SUM(Dis_Demand.FATKGDemand), 0) AS FATKGDemand,
                    ISNULL(SUM(Dis_Demand.SNFKGDemand), 0) AS SNFKGDemand,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc,
                    ISNULL(SUM(Sale_invoice.Sale_Voucher),0) AS Sale_Voucher,
					ISNULL(SUM(Milk_Purchase_invoice.Purchase_Voucher),0) AS Purchase_Voucher,
                    (SELECT TOP 1 DATENAME(MONTH, [BKNTEST].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO) + ' ' + CONVERT(VARCHAR(4), YEAR([BKNTEST].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO)) FROM [BKNTEST].[dbo].TSPL_GENERATE_SALARY
left join [BKNTEST].[dbo].TSPL_PAYPERIOD_MASTER on [BKNTEST].[dbo].TSPL_GENERATE_SALARY.PAY_PERIOD_CODE = [BKNTEST].[dbo].TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE
where 2=2" + status6 + " ORDER BY DATE_TO DESC) as Last_Salary
                FROM 
(SELECT 
                     SUM(Dis_QtyInLTR) AS Dis_QtyInLTR,
                        SUM(Dis_FATKG) AS Dis_FATKG,
                        SUM(Dis_SNFKG) AS Dis_SNFKG
                    FROM (
                    ( SELECT 
                        SUM(CASE 
                                WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                ELSE 0 
                            END) AS Dis_QtyInLTR,
                        SUM(ISNULL((    
                            CASE 
                                WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                ELSE 0 
                            END
                        ) * STD_FatPer / 100,
                        0
                        )) AS Dis_FATKG,
                        SUM(ISNULL((
                            CASE 
                                WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                ELSE 0 
                            END
                        ) * STD_SNFPer / 100,
                        0
                        )) AS Dis_SNFKG
                    FROM 
                         [BKNTEST].[dbo].TSPL_SD_SHIPMENT_DETAIL sd
                    LEFT JOIN 
                        [BKNTEST].[dbo].tspl_item_master im ON im.Item_Code = sd.Item_Code
                    LEFT JOIN 
                        [BKNTEST].[dbo].TSPL_SD_SHIPMENT_HEAD sh ON sh.Document_Code = sd.DOCUMENT_CODE
                    LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [BKNTEST].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Crate') AS icc ON icc.Item_code = sd.Item_Code
                    LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [BKNTEST].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Pouch') AS icp ON icp.Item_code = sd.Item_Code
                    LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [BKNTEST].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'LTR') AS ilt ON ilt.Item_code = sd.Item_Code
                    WHERE 
                        CONVERT(DATE, sh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        AND im.IsTaxable = 0 
                        AND im.Is_FreshItem = 1  
                        " + status1 + "
                        union all
                        SELECT 
                      cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,
                        SUM(xxxx.Fat_KG ) AS FATKGDemand,
                        SUM(xxxx.SNF_KG) AS SNFKGDemand
from (
SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,[BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
FROM  [BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale
LEFT JOIN  [BKNTEST].[dbo].TSPL_Dispatch_BulkSale  ON [BKNTEST].[dbo].TSPL_Dispatch_BulkSale.Document_No = [BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
inner join [BKNTEST].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
inner join [BKNTEST].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

WHERE CONVERT(DATE, [BKNTEST].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status9 + "
)xxxx ))Disp_BUlksale
                    ) AS Dis_Disbursement,


                        (SELECT 
                        SUM(TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand,
                        SUM(FATKGDemand) AS FATKGDemand,
                        SUM(SNFKGDemand) AS SNFKGDemand
                    FROM (
                    (SELECT 
                        SUM(TotalLtr_ItemWise) AS TotalLtr_ItemWiseDemand,
                        SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,
                        SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand
                    FROM 
                        [BKNTEST].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                    LEFT JOIN 
                        [BKNTEST].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                    LEFT JOIN 
                        [BKNTEST].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
                    WHERE 
                        CONVERT(DATE, dbm.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        AND im.IsTaxable = 0
                        AND im.Is_FreshItem = 1
                        " + status3 + " 
                        union all
                        SELECT 
                      cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,
                        SUM(xxxx.Fat_KG ) AS FATKGDemand,
                        SUM(xxxx.SNF_KG) AS SNFKGDemand
from (
SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,[BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
FROM  [BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale
LEFT JOIN  [BKNTEST].[dbo].TSPL_Dispatch_BulkSale  ON [BKNTEST].[dbo].TSPL_Dispatch_BulkSale.Document_No = [BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
inner join [BKNTEST].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
inner join [BKNTEST].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[BKNTEST].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

WHERE CONVERT(DATE, [BKNTEST].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status9 + "
)xxxx ))Disp_BUlksale
                    ) AS Dis_Demand,



                    (SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG
                        FROM 
                            [BKNTEST].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [BKNTEST].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status4 + "

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG
                        FROM 
                            [BKNTEST].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [BKNTEST].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status5 + "

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG
                        FROM 
                            [BKNTEST].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [BKNTEST].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                           mcs.Status = 0
                           AND
                            CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        ) AS Procurement
                    ) AS Dis_Procurement,
                    (select sum([BKNTEST].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt) as Sale_Voucher from [BKNTEST].[dbo].TSPL_SD_SALE_INVOICE_DETAIL
left outer join [BKNTEST].[dbo].TSPL_SD_SALE_INVOICE_HEAD on [BKNTEST].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = [BKNTEST].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Code
where  CONVERT(DATE, [BKNTEST].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status7 + ") AS Sale_invoice,
(select SUM(TOTAL_AMOUNT) AS Purchase_Voucher from [BKNTEST].[dbo].TSPL_MILK_PURCHASE_INVOICE_HEAD where  CONVERT(DATE, DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status8 + ") AS Milk_Purchase_invoice)final ) xx group by SNo "

                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "BNS") = CompairStringResult.Equal Then

                        query = " " & qry & "" & query & " union all " & Environment.NewLine & "  select * from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                                            '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                                        ISNULL(SUM(Dis_Disbursement.Dis_QtyInLTR), 0) AS Dis_QtyInLTR,
                                        ISNULL(SUM(Dis_Disbursement.Dis_FATKG), 0) AS Dis_FATKG,
                                        ISNULL(SUM(Dis_Disbursement.Dis_SNFKG), 0) AS Dis_SNFKG,
                                        ISNULL(SUM(Dis_Demand.TotalLtr_ItemWiseDemand), 0) AS TotalLtr_ItemWiseDemand,
                                        ISNULL(SUM(Dis_Demand.FATKGDemand), 0) AS FATKGDemand,
                                        ISNULL(SUM(Dis_Demand.SNFKGDemand), 0) AS SNFKGDemand,
                                        ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                                        ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                                        ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc,
                                        ISNULL(SUM(Sale_invoice.Sale_Voucher),0) AS Sale_Voucher,
                    					ISNULL(SUM(Milk_Purchase_invoice.Purchase_Voucher),0) AS Purchase_Voucher,
                                        (SELECT TOP 1 DATENAME(MONTH, [Banswara].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO) + ' ' + CONVERT(VARCHAR(4), YEAR([Banswara].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO)) FROM [Banswara].[dbo].TSPL_GENERATE_SALARY
                    left join [Banswara].[dbo].TSPL_PAYPERIOD_MASTER on [Banswara].[dbo].TSPL_GENERATE_SALARY.PAY_PERIOD_CODE = [Banswara].[dbo].TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE
                    where 2=2" + status6 + " ORDER BY DATE_TO DESC) as Last_Salary
                                    FROM 
                    (SELECT 
                                         SUM(Dis_QtyInLTR) AS Dis_QtyInLTR,
                                            SUM(Dis_FATKG) AS Dis_FATKG,
                                            SUM(Dis_SNFKG) AS Dis_SNFKG
                                        FROM (
                                        ( SELECT 
                                            SUM(CASE 
                                                    WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                                    WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                                    WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                                    ELSE 0 
                                                END) AS Dis_QtyInLTR,
                                            SUM(ISNULL((    
                                                CASE 
                                                    WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                                    WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                                    WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                                    ELSE 0 
                                                END
                                            ) * STD_FatPer / 100,
                                            0
                                            )) AS Dis_FATKG,
                                            SUM(ISNULL((
                                                CASE 
                                                    WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                                    WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                                    WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                                    ELSE 0 
                                                END
                                            ) * STD_SNFPer / 100,
                                            0
                                            )) AS Dis_SNFKG
                                        FROM 
                                             [Banswara].[dbo].TSPL_SD_SHIPMENT_DETAIL sd
                                        LEFT JOIN 
                                            [Banswara].[dbo].tspl_item_master im ON im.Item_Code = sd.Item_Code
                                        LEFT JOIN 
                                            [Banswara].[dbo].TSPL_SD_SHIPMENT_HEAD sh ON sh.Document_Code = sd.DOCUMENT_CODE
                                        LEFT JOIN 
                                            (SELECT Conversion_factor, Item_code FROM [Banswara].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Crate') AS icc ON icc.Item_code = sd.Item_Code
                                        LEFT JOIN 
                                            (SELECT Conversion_factor, Item_code FROM [Banswara].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Pouch') AS icp ON icp.Item_code = sd.Item_Code
                                        LEFT JOIN 
                                            (SELECT Conversion_factor, Item_code FROM [Banswara].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'LTR') AS ilt ON ilt.Item_code = sd.Item_Code
                                        WHERE 
                                            CONVERT(DATE, sh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                            AND im.IsTaxable = 0 
                                            AND im.Is_FreshItem = 1  
                                            " + status1 + "
                                            union all
                                            SELECT 
                                          cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,
                                            SUM(xxxx.Fat_KG ) AS FATKGDemand,
                                            SUM(xxxx.SNF_KG) AS SNFKGDemand
                    from (
                    SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,[Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
                    FROM  [Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale
                    LEFT JOIN  [Banswara].[dbo].TSPL_Dispatch_BulkSale  ON [Banswara].[dbo].TSPL_Dispatch_BulkSale.Document_No = [Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                    inner join [Banswara].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                    inner join [Banswara].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

                    WHERE CONVERT(DATE, [Banswara].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                " + status9 + "
                    )xxxx ))Disp_BUlksale
                                        ) AS Dis_Disbursement,


                                            (SELECT 
                                            SUM(TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand,
                                            SUM(FATKGDemand) AS FATKGDemand,
                                            SUM(SNFKGDemand) AS SNFKGDemand
                                        FROM (
                                        (SELECT 
                                            SUM(TotalLtr_ItemWise) AS TotalLtr_ItemWiseDemand,
                                            SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,
                                            SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand
                                        FROM 
                                            [Banswara].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                                        LEFT JOIN 
                                            [Banswara].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                                        LEFT JOIN 
                                            [Banswara].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
                                        WHERE 
                                            CONVERT(DATE, dbm.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                            AND im.IsTaxable = 0
                                            AND im.Is_FreshItem = 1
                                            " + status3 + " 
                                            union all
                                            SELECT 
                                          cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,
                                            SUM(xxxx.Fat_KG ) AS FATKGDemand,
                                            SUM(xxxx.SNF_KG) AS SNFKGDemand
                    from (
                    SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,[Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
                    FROM  [Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale
                    LEFT JOIN  [Banswara].[dbo].TSPL_Dispatch_BulkSale  ON [Banswara].[dbo].TSPL_Dispatch_BulkSale.Document_No = [Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                    inner join [Banswara].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                    inner join [Banswara].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[Banswara].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

                    WHERE CONVERT(DATE, [Banswara].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                " + status9 + "
                    )xxxx ))Disp_BUlksale
                                        ) AS Dis_Demand,



                                        (SELECT 
                                            SUM(milk_weight) AS Milk_WeightProc,
                                            SUM(fatkg) AS FATKGProc,
                                            SUM(SNFKG) AS SNFKGProc
                                        FROM (
                                            SELECT 
                                                SUM(Milk_Weight) AS Milk_Weight,
                                                SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                                                SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG
                                            FROM 
                                                [Banswara].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                                            LEFT JOIN 
                                                [Banswara].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                                            WHERE 
                                                CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                " + status4 + "

                                            UNION ALL

                                            SELECT 
                                                SUM(Milk_Weight) AS Milk_Weight,
                                                SUM(Milk_Weight * FAT / 100) AS FATKG,
                                                SUM(Milk_Weight * SNF / 100) AS SNFKG
                                            FROM 
                                                [Banswara].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                                            LEFT JOIN 
                                                [Banswara].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                                            WHERE 
                                                CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                " + status5 + "

                                            UNION ALL

                                            SELECT 
                                                ISNULL(SUM(QTY),0) AS QTY,
                                                ISNULL(SUM(FATKG),0) AS FATKG,
                                                ISNULL(SUM(SNFKG),0) AS SNFKG
                                            FROM 
                                                [Banswara].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                                            LEFT JOIN 
                                                [Banswara].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                                            WHERE 
                                               mcs.Status = 0
                                               AND
                                                CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                            ) AS Procurement
                                        ) AS Dis_Procurement,
                                        (select sum([Banswara].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt) as Sale_Voucher from [Banswara].[dbo].TSPL_SD_SALE_INVOICE_DETAIL
                    left outer join [Banswara].[dbo].TSPL_SD_SALE_INVOICE_HEAD on [Banswara].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = [Banswara].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Code
                    where  CONVERT(DATE, [Banswara].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status7 + ") AS Sale_invoice,
                    (select SUM(TOTAL_AMOUNT) AS Purchase_Voucher from [Banswara].[dbo].TSPL_MILK_PURCHASE_INVOICE_HEAD where  CONVERT(DATE, DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status8 + ") AS Milk_Purchase_invoice)final ) xx group by SNo "
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "CHT") = CompairStringResult.Equal Then
                        query = " " & qry & "" & query & " union all " & Environment.NewLine & "  select * from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                                            '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                                        ISNULL(SUM(Dis_Disbursement.Dis_QtyInLTR), 0) AS Dis_QtyInLTR,
                                        ISNULL(SUM(Dis_Disbursement.Dis_FATKG), 0) AS Dis_FATKG,
                                        ISNULL(SUM(Dis_Disbursement.Dis_SNFKG), 0) AS Dis_SNFKG,
                                        ISNULL(SUM(Dis_Demand.TotalLtr_ItemWiseDemand), 0) AS TotalLtr_ItemWiseDemand,
                                        ISNULL(SUM(Dis_Demand.FATKGDemand), 0) AS FATKGDemand,
                                        ISNULL(SUM(Dis_Demand.SNFKGDemand), 0) AS SNFKGDemand,
                                        ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                                        ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                                        ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc,
                                        ISNULL(SUM(Sale_invoice.Sale_Voucher),0) AS Sale_Voucher,
                    					ISNULL(SUM(Milk_Purchase_invoice.Purchase_Voucher),0) AS Purchase_Voucher,
                                        (SELECT TOP 1 DATENAME(MONTH, [CHITTORGARH].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO) + ' ' + CONVERT(VARCHAR(4), YEAR([CHITTORGARH].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO)) FROM [CHITTORGARH].[dbo].TSPL_GENERATE_SALARY
                    left join [CHITTORGARH].[dbo].TSPL_PAYPERIOD_MASTER on [CHITTORGARH].[dbo].TSPL_GENERATE_SALARY.PAY_PERIOD_CODE = [CHITTORGARH].[dbo].TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE
                    where 2=2" + status6 + " ORDER BY DATE_TO DESC) as Last_Salary
                                    FROM 
                    (SELECT 
                                         SUM(Dis_QtyInLTR) AS Dis_QtyInLTR,
                                            SUM(Dis_FATKG) AS Dis_FATKG,
                                            SUM(Dis_SNFKG) AS Dis_SNFKG
                                        FROM (
                                        ( SELECT 
                                            SUM(CASE 
                                                    WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                                    WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                                    WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                                    ELSE 0 
                                                END) AS Dis_QtyInLTR,
                                            SUM(ISNULL((    
                                                CASE 
                                                    WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                                    WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                                    WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                                    ELSE 0 
                                                END
                                            ) * STD_FatPer / 100,
                                            0
                                            )) AS Dis_FATKG,
                                            SUM(ISNULL((
                                                CASE 
                                                    WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                                    WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                                    WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                                    ELSE 0 
                                                END
                                            ) * STD_SNFPer / 100,
                                            0
                                            )) AS Dis_SNFKG
                                        FROM 
                                             [CHITTORGARH].[dbo].TSPL_SD_SHIPMENT_DETAIL sd
                                        LEFT JOIN 
                                            [CHITTORGARH].[dbo].tspl_item_master im ON im.Item_Code = sd.Item_Code
                                        LEFT JOIN 
                                            [CHITTORGARH].[dbo].TSPL_SD_SHIPMENT_HEAD sh ON sh.Document_Code = sd.DOCUMENT_CODE
                                        LEFT JOIN 
                                            (SELECT Conversion_factor, Item_code FROM [CHITTORGARH].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Crate') AS icc ON icc.Item_code = sd.Item_Code
                                        LEFT JOIN 
                                            (SELECT Conversion_factor, Item_code FROM [CHITTORGARH].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Pouch') AS icp ON icp.Item_code = sd.Item_Code
                                        LEFT JOIN 
                                            (SELECT Conversion_factor, Item_code FROM [CHITTORGARH].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'LTR') AS ilt ON ilt.Item_code = sd.Item_Code
                                        WHERE 
                                            CONVERT(DATE, sh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                            AND im.IsTaxable = 0 
                                            AND im.Is_FreshItem = 1  
                                            " + status1 + "
                                            union all
                                            SELECT 
                                          cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,
                                            SUM(xxxx.Fat_KG ) AS FATKGDemand,
                                            SUM(xxxx.SNF_KG) AS SNFKGDemand
                    from (
                    SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,[CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
                    FROM  [CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale
                    LEFT JOIN  [CHITTORGARH].[dbo].TSPL_Dispatch_BulkSale  ON [CHITTORGARH].[dbo].TSPL_Dispatch_BulkSale.Document_No = [CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                    inner join [CHITTORGARH].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                    inner join [CHITTORGARH].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

                    WHERE CONVERT(DATE, [CHITTORGARH].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                " + status9 + "
                    )xxxx ))Disp_BUlksale
                                        ) AS Dis_Disbursement,


                                            (SELECT 
                                            SUM(TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand,
                                            SUM(FATKGDemand) AS FATKGDemand,
                                            SUM(SNFKGDemand) AS SNFKGDemand
                                        FROM (
                                        (SELECT 
                                            SUM(TotalLtr_ItemWise) AS TotalLtr_ItemWiseDemand,
                                            SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,
                                            SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand
                                        FROM 
                                            [CHITTORGARH].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                                        LEFT JOIN 
                                            [CHITTORGARH].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                                        LEFT JOIN 
                                            [CHITTORGARH].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
                                        WHERE 
                                            CONVERT(DATE, dbm.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                            AND im.IsTaxable = 0
                                            AND im.Is_FreshItem = 1
                                            " + status3 + " 
                                            union all
                                            SELECT 
                                          cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,
                                            SUM(xxxx.Fat_KG ) AS FATKGDemand,
                                            SUM(xxxx.SNF_KG) AS SNFKGDemand
                    from (
                    SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,[CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
                    FROM  [CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale
                    LEFT JOIN  [CHITTORGARH].[dbo].TSPL_Dispatch_BulkSale  ON [CHITTORGARH].[dbo].TSPL_Dispatch_BulkSale.Document_No = [CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                    inner join [CHITTORGARH].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                    inner join [CHITTORGARH].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[CHITTORGARH].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

                    WHERE CONVERT(DATE, [CHITTORGARH].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                " + status9 + "
                    )xxxx ))Disp_BUlksale
                                        ) AS Dis_Demand,

                                        (SELECT 
                                            SUM(milk_weight) AS Milk_WeightProc,
                                            SUM(fatkg) AS FATKGProc,
                                            SUM(SNFKG) AS SNFKGProc
                                        FROM (
                                            SELECT 
                                                SUM(Milk_Weight) AS Milk_Weight,
                                                SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                                                SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG
                                            FROM 
                                                [CHITTORGARH].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                                            LEFT JOIN 
                                                [CHITTORGARH].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                                            WHERE 
                                                CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                " + status4 + "

                                            UNION ALL

                                            SELECT 
                                                SUM(Milk_Weight) AS Milk_Weight,
                                                SUM(Milk_Weight * FAT / 100) AS FATKG,
                                                SUM(Milk_Weight * SNF / 100) AS SNFKG
                                            FROM 
                                                [CHITTORGARH].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                                            LEFT JOIN 
                                                [CHITTORGARH].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                                            WHERE 
                                                CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                " + status5 + "

                                            UNION ALL

                                            SELECT 
                                                ISNULL(SUM(QTY),0) AS QTY,
                                                ISNULL(SUM(FATKG),0) AS FATKG,
                                                ISNULL(SUM(SNFKG),0) AS SNFKG
                                            FROM 
                                                [CHITTORGARH].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                                            LEFT JOIN 
                                                [CHITTORGARH].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                                            WHERE 
                                               mcs.Status = 0
                                               AND
                                                CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                            ) AS Procurement
                                        ) AS Dis_Procurement,
                                        (select sum([CHITTORGARH].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt) as Sale_Voucher from [CHITTORGARH].[dbo].TSPL_SD_SALE_INVOICE_DETAIL
                    left outer join [CHITTORGARH].[dbo].TSPL_SD_SALE_INVOICE_HEAD on [CHITTORGARH].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = [CHITTORGARH].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Code
                    where  CONVERT(DATE, [CHITTORGARH].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status7 + ") AS Sale_invoice,
                    (select SUM(TOTAL_AMOUNT) AS Purchase_Voucher from [CHITTORGARH].[dbo].TSPL_MILK_PURCHASE_INVOICE_HEAD where  CONVERT(DATE, DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status8 + ") AS Milk_Purchase_invoice)final ) xx group by SNo "
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "RJS") = CompairStringResult.Equal Then
                        query = " " & qry & "" & query & " union all " & Environment.NewLine & "  select * from (select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                                            '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,
                                        ISNULL(SUM(Dis_Disbursement.Dis_QtyInLTR), 0) AS Dis_QtyInLTR,
                                        ISNULL(SUM(Dis_Disbursement.Dis_FATKG), 0) AS Dis_FATKG,
                                        ISNULL(SUM(Dis_Disbursement.Dis_SNFKG), 0) AS Dis_SNFKG,
                                        ISNULL(SUM(Dis_Demand.TotalLtr_ItemWiseDemand), 0) AS TotalLtr_ItemWiseDemand,
                                        ISNULL(SUM(Dis_Demand.FATKGDemand), 0) AS FATKGDemand,
                                        ISNULL(SUM(Dis_Demand.SNFKGDemand), 0) AS SNFKGDemand,
                                        ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                                        ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                                        ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc,
                                        ISNULL(SUM(Sale_invoice.Sale_Voucher),0) AS Sale_Voucher,
                    					ISNULL(SUM(Milk_Purchase_invoice.Purchase_Voucher),0) AS Purchase_Voucher,
                                        (SELECT TOP 1 DATENAME(MONTH, [Rajsamand].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO) + ' ' + CONVERT(VARCHAR(4), YEAR([Rajsamand].[dbo].TSPL_PAYPERIOD_MASTER.DATE_TO)) FROM [Rajsamand].[dbo].TSPL_GENERATE_SALARY
                    left join [Rajsamand].[dbo].TSPL_PAYPERIOD_MASTER on [Rajsamand].[dbo].TSPL_GENERATE_SALARY.PAY_PERIOD_CODE = [Rajsamand].[dbo].TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE
                    where 2=2" + status6 + " ORDER BY DATE_TO DESC) as Last_Salary
                                    FROM 
                    (SELECT 
                                         SUM(Dis_QtyInLTR) AS Dis_QtyInLTR,
                                            SUM(Dis_FATKG) AS Dis_FATKG,
                                            SUM(Dis_SNFKG) AS Dis_SNFKG
                                        FROM (
                                        ( SELECT 
                                            SUM(CASE 
                                                    WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                                    WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                                    WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                                    ELSE 0 
                                                END) AS Dis_QtyInLTR,
                                            SUM(ISNULL((    
                                                CASE 
                                                    WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                                    WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                                    WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                                    ELSE 0 
                                                END
                                            ) * STD_FatPer / 100,
                                            0
                                            )) AS Dis_FATKG,
                                            SUM(ISNULL((
                                                CASE 
                                                    WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                                    WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                                    WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                                    ELSE 0 
                                                END
                                            ) * STD_SNFPer / 100,
                                            0
                                            )) AS Dis_SNFKG
                                        FROM 
                                             [Rajsamand].[dbo].TSPL_SD_SHIPMENT_DETAIL sd
                                        LEFT JOIN 
                                            [Rajsamand].[dbo].tspl_item_master im ON im.Item_Code = sd.Item_Code
                                        LEFT JOIN 
                                            [Rajsamand].[dbo].TSPL_SD_SHIPMENT_HEAD sh ON sh.Document_Code = sd.DOCUMENT_CODE
                                        LEFT JOIN 
                                            (SELECT Conversion_factor, Item_code FROM [Rajsamand].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Crate') AS icc ON icc.Item_code = sd.Item_Code
                                        LEFT JOIN 
                                            (SELECT Conversion_factor, Item_code FROM [Rajsamand].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Pouch') AS icp ON icp.Item_code = sd.Item_Code
                                        LEFT JOIN 
                                            (SELECT Conversion_factor, Item_code FROM [Rajsamand].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'LTR') AS ilt ON ilt.Item_code = sd.Item_Code
                                        WHERE 
                                            CONVERT(DATE, sh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                            AND im.IsTaxable = 0 
                                            AND im.Is_FreshItem = 1  
                                            " + status1 + "
                                            union all
                                            SELECT 
                                          cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,
                                            SUM(xxxx.Fat_KG ) AS FATKGDemand,
                                            SUM(xxxx.SNF_KG) AS SNFKGDemand
                    from (
                    SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,[Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
                    FROM  [Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale
                    LEFT JOIN  [Rajsamand].[dbo].TSPL_Dispatch_BulkSale  ON [Rajsamand].[dbo].TSPL_Dispatch_BulkSale.Document_No = [Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                    inner join [Rajsamand].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                    inner join [Rajsamand].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

                    WHERE CONVERT(DATE, [Rajsamand].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                " + status9 + "
                    )xxxx ))Disp_BUlksale
                                        ) AS Dis_Disbursement,


                                            (SELECT 
                                            SUM(TotalLtr_ItemWiseDemand) AS TotalLtr_ItemWiseDemand,
                                            SUM(FATKGDemand) AS FATKGDemand,
                                            SUM(SNFKGDemand) AS SNFKGDemand
                                        FROM (
                                        (SELECT 
                                            SUM(TotalLtr_ItemWise) AS TotalLtr_ItemWiseDemand,
                                            SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,
                                            SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand
                                        FROM 
                                            [Rajsamand].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                                        LEFT JOIN 
                                            [Rajsamand].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                                        LEFT JOIN 
                                            [Rajsamand].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
                                        WHERE 
                                            CONVERT(DATE, dbm.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                            AND im.IsTaxable = 0
                                            AND im.Is_FreshItem = 1
                                            " + status3 + " 
                                            union all
                                            SELECT 
                                          cast(  SUM(xxxx.Qty) as decimal(18,2)) AS TotalLtr_ItemWiseDemand,
                                            SUM(xxxx.Fat_KG ) AS FATKGDemand,
                                            SUM(xxxx.SNF_KG) AS SNFKGDemand
                    from (
                    SELECT case when isnull(ConvertDiv.Conversion_Factor,0)=0 then 0 else  [Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Qty*ConvertMul.Conversion_Factor/ConvertDiv.Conversion_Factor end as Qty ,[Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Fat_KG ,[Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.SNF_KG,[Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code
                    FROM  [Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale
                    LEFT JOIN  [Rajsamand].[dbo].TSPL_Dispatch_BulkSale  ON [Rajsamand].[dbo].TSPL_Dispatch_BulkSale.Document_No = [Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Document_No
                    inner join [Rajsamand].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertMul on ConvertMul.Item_Code=[Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertMul.UOM_Code=[Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Unit_code 
                    inner join [Rajsamand].[dbo].TSPL_ITEM_UOM_DETAIL as ConvertDiv on ConvertDiv .Item_Code=[Rajsamand].[dbo].TSPL_Dispatch_Detail_BulkSale.Item_Code and ConvertDiv.UOM_Code='LTR'  

                    WHERE CONVERT(DATE, [Rajsamand].[dbo].TSPL_Dispatch_BulkSale.Document_Date, 103)  BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                " + status9 + "
                    )xxxx ))Disp_BUlksale
                                        ) AS Dis_Demand,



                                        (SELECT 
                                            SUM(milk_weight) AS Milk_WeightProc,
                                            SUM(fatkg) AS FATKGProc,
                                            SUM(SNFKG) AS SNFKGProc
                                        FROM (
                                            SELECT 
                                                SUM(Milk_Weight) AS Milk_Weight,
                                                SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                                                SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG
                                            FROM 
                                                [Rajsamand].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                                            LEFT JOIN 
                                                [Rajsamand].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                                            WHERE 
                                                CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                " + status4 + "

                                            UNION ALL

                                            SELECT 
                                                SUM(Milk_Weight) AS Milk_Weight,
                                                SUM(Milk_Weight * FAT / 100) AS FATKG,
                                                SUM(Milk_Weight * SNF / 100) AS SNFKG
                                            FROM 
                                                [Rajsamand].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                                            LEFT JOIN 
                                                [Rajsamand].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                                            WHERE 
                                                CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                                " + status5 + "

                                            UNION ALL

                                            SELECT 
                                                ISNULL(SUM(QTY),0) AS QTY,
                                                ISNULL(SUM(FATKG),0) AS FATKG,
                                                ISNULL(SUM(SNFKG),0) AS SNFKG
                                            FROM 
                                                [Rajsamand].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                                            LEFT JOIN 
                                                [Rajsamand].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                                            WHERE 
                                               mcs.Status = 0
                                               AND
                                                CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                                            ) AS Procurement
                                        ) AS Dis_Procurement,
                                        (select sum([Rajsamand].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.Total_Basic_Amt) as Sale_Voucher from [Rajsamand].[dbo].TSPL_SD_SALE_INVOICE_DETAIL
                    left outer join [Rajsamand].[dbo].TSPL_SD_SALE_INVOICE_HEAD on [Rajsamand].[dbo].TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE = [Rajsamand].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Code
                    where  CONVERT(DATE, [Rajsamand].[dbo].TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status7 + ") AS Sale_invoice,
                    (select SUM(TOTAL_AMOUNT) AS Purchase_Voucher from [Rajsamand].[dbo].TSPL_MILK_PURCHASE_INVOICE_HEAD where  CONVERT(DATE, DOC_DATE, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + status8 + ") AS Milk_Purchase_invoice)final ) xx group by SNo "

                    End If
                    dtUnion = clsDBFuncationality.GetDataTable(query)
                    dt2.Merge(dtUnion)
                Next
            End If
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.MasterView.Refresh()
                    gv1.DataSource = dt2
                    For ii As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Columns(ii).ReadOnly = True
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.EnableFiltering = True
                    SetGridFormat1()
                    gv1.BestFitColumns()
                Else
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptmilkunionreport", "") ''report for both (RCDF And RCDFCF)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ' ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        Dim serverDate As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        Dim previousDay As DateTime = serverDate.AddDays(-1)
        Dim previousDayString As String = clsCommon.GetPrintDate(previousDay, "dd/MMM/yyyy")
        txtFromDate.Value = previousDayString
        txtToDate.Value = previousDayString
        btngo.Enabled = True
        rdbPosted.Checked = True
        rdbUnposted.Checked = False
    End Sub
    Sub printview(ByVal print As Boolean)
        Try

            Dim query As String
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim docNo As String = ""
            query = ""
            dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    Dim status1 As String
                    Dim status2 As String
                    Dim status3 As String
                    Dim status4 As String
                    Dim status5 As String
                    If rdbPosted.Checked Then
                        status1 = " and  sh.Status=1 "
                        status2 = " and pe.POSTED= 1 "
                        status3 = " and dbm.Posted= 1 "
                        status4 = " and muh.Status= 1 "
                        status5 = "and msh.Status= 1"
                    ElseIf rdbUnposted.Checked Then
                        status1 = " and  sh.Status= 0 "
                        status2 = " and pe.POSTED= 0 "
                        status3 = " and dbm.Posted= 0 "
                        status4 = " and muh.Status= 0 "
                        status5 = "and msh.Status= 0 "
                    Else
                        status1 = " "
                        status2 = " "
                        status3 = " "
                        status4 = " "
                        status5 = " "
                    End If
                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
                    query += ",'" + clsCommon.GetPrintDate(txtFromDate.Value) + "'as Fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "'as Todate,'" + objCommonVar.CurrentUser + "' as username,ISNULL(SUM(Dis_Disbursement.Dis_QtyInLTR), 0) AS Dis_QtyInLTR,
                    ISNULL(SUM(Dis_Disbursement.Dis_FATKG), 0) AS Dis_FATKG,
                    ISNULL(SUM(Dis_Disbursement.Dis_SNFKG), 0) AS Dis_SNFKG,
                    ISNULL(SUM(Dis_Demand.TotalLtr_ItemWiseDemand), 0) AS TotalLtr_ItemWiseDemand,
                    ISNULL(SUM(Dis_Demand.FATKGDemand), 0) AS FATKGDemand,
                    ISNULL(SUM(Dis_Demand.SNFKGDemand), 0) AS SNFKGDemand,
                    ISNULL(SUM(Dis_Procurement.Milk_WeightProc), 0) AS Milk_WeightProc,
                    ISNULL(SUM(Dis_Procurement.FATKGProc), 0) AS FATKGProc,
                    ISNULL(SUM(Dis_Procurement.SNFKGProc), 0) AS SNFKGProc
                FROM 
                    (SELECT 
                        SUM(CASE 
                                WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                ELSE 0 
                            END) AS Dis_QtyInLTR,
                        SUM(ISNULL((
                            CASE 
                                WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                ELSE 0 
                            END
                        ) * STD_FatPer / 100,
                        0
                        )) AS Dis_FATKG,
                        SUM(ISNULL((
                            CASE 
                                WHEN sd.Unit_code = 'POUCH' THEN sd.qty * icp.Conversion_Factor / ilt.Conversion_Factor 
                                WHEN sd.Unit_code = 'CRATE' THEN sd.qty * icc.Conversion_Factor / ilt.Conversion_Factor
                                WHEN sd.Unit_code = 'LTR' THEN sd.qty  
                                ELSE 0 
                            END
                        ) * STD_SNFPer / 100,
                        0
                        )) AS Dis_SNFKG
                    FROM 
                         [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_DETAIL sd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].tspl_item_master im ON im.Item_Code = sd.Item_Code
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD sh ON sh.Document_Code = sd.DOCUMENT_CODE
                    LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Crate') AS icc ON icc.Item_code = sd.Item_Code
                    LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'Pouch') AS icp ON icp.Item_code = sd.Item_Code
                    LEFT JOIN 
                        (SELECT Conversion_factor, Item_code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_UOM_DETAIL WHERE UOM_code = 'LTR') AS ilt ON ilt.Item_code = sd.Item_Code
                    WHERE 
                        CONVERT(DATE, sh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        AND im.IsTaxable = 0 
                        AND im.Is_FreshItem = 1  
                        " + status1 + "
                    ) AS Dis_Disbursement,
                    (SELECT 
                        SUM(TotalLtr_ItemWise) AS TotalLtr_ItemWiseDemand,
                        SUM(TotalLtr_ItemWise * im.STD_FatPer / 100) AS FATKGDemand,
                        SUM(TotalLtr_ItemWise * im.STD_SNFPer / 100) AS SNFKGDemand
                    FROM 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_DETAIL dbd
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DEMAND_BOOKING_master dbm ON dbm.Document_No = dbd.Document_No
                    LEFT JOIN 
                        [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ITEM_MASTER im ON im.Item_Code = dbd.Item_Code
                    WHERE 
                        CONVERT(DATE, dbm.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        AND im.IsTaxable = 0
                        AND im.Is_FreshItem = 1
                        " + status3 + " 
                    ) AS Dis_Demand,
                    (SELECT 
                        SUM(milk_weight) AS Milk_WeightProc,
                        SUM(fatkg) AS FATKGProc,
                        SUM(SNFKG) AS SNFKGProc
                    FROM (
                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(CAST(Milk_Weight * FAT / 100 AS DECIMAL(18,3))) AS FATKg,
                            SUM(CAST(Milk_Weight * SNF / 100 AS DECIMAL(18,3))) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL mud
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_PROCUREMENT_UPLOADER_HEAD muh ON muh.Document_No = mud.Document_No
                        WHERE 
                            CONVERT(DATE, muh.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status4 + "

                        UNION ALL

                        SELECT 
                            SUM(Milk_Weight) AS Milk_Weight,
                            SUM(Milk_Weight * FAT / 100) AS FATKG,
                            SUM(Milk_Weight * SNF / 100) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_DETAIL msd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SHIFT_UPLOADER_HEAD msh ON msh.Document_No = msd.Document_No
                        WHERE 
                            CONVERT(DATE, msh.Shift_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                            " + status5 + "

                        UNION ALL

                        SELECT 
                            ISNULL(SUM(QTY),0) AS QTY,
                            ISNULL(SUM(FATKG),0) AS FATKG,
                            ISNULL(SUM(SNFKG),0) AS SNFKG
                        FROM 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS_DETAIL mcd
                        LEFT JOIN 
                            [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_COLLECTION_DCS mcs ON mcs.Document_No = mcd.Document_No
                        WHERE 
                            mcs.Status = 0
                            AND CONVERT(DATE, mcs.Document_Date, 103) BETWEEN '" + clsCommon.GetPrintDate(txtFromDate.Value) + "' AND '" + clsCommon.GetPrintDate(txtToDate.Value) + "'
                        ) AS Procurement
                    ) AS Dis_Procurement
                   "
                Next
            End If
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then
                If print = False Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.MasterView.Refresh()
                    gv1.DataSource = dt2
                    For ii As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Columns(ii).ReadOnly = True
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.EnableFiltering = True
                    SetGridFormat1()
                    gv1.BestFitColumns()
                Else
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptmilkunionreport", "") ''report for both (RCDF And RCDFCF)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub '  
End Class