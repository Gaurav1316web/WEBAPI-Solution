Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.IO
Public Class FrmExciseSummaryNew
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ExportToExcel As Boolean = False
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.ExciseSummary1)

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        btnExp.Visible = MyBase.isExport
        btnReferesh.Enabled = MyBase.isReadFlag

        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub print(ByVal IsPrint As Boolean)
        Dim strReportName As String
        Dim startDate As String = clsCommon.GetPrintDate(dtStart.Value, "dd-MMM-yyyy hh:mm tt")
        Dim EndDate As String = clsCommon.GetPrintDate(dtEnd.Value, "dd-MMM-yyyy hh:mm tt")
        Dim startTime As String = dtStart.Value.ToString("hh:mm tt")
        Dim EndTime As String = dtEnd.Value.ToString("hh:mm tt")
        Dim dt As DataTable

        '=============for getting tax name in column========================================================================================
        Dim AllTax As String = "select ROW_NUMBER() over(order by tax_code) as sno,Tax_Code,Tax_Code_Desc from TSPL_TAX_MASTER where Excisable='Y'"
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(AllTax)

        Dim InnerTaxCase As String = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                InnerTaxCase += ",isnull((case when subF.TAX1='" + clsCommon.myCstr(dr("tax_code")) + "' then subf.TAX1_Amt else case when SubF.TAX2='" + clsCommon.myCstr(dr("tax_code")) + "' then SubF.TAX2_Amt else case when SubF.TAX3='" + clsCommon.myCstr(dr("tax_code")) + "' then SubF.TAX3_Amt else case when SubF.TAX4='" + clsCommon.myCstr(dr("tax_code")) + "' then SubF.TAX4_Amt else case when SubF.TAX5='" + clsCommon.myCstr(dr("tax_code")) + "' then SubF.TAX5_Amt else case when SubF.TAX6='" + clsCommon.myCstr(dr("tax_code")) + "' then SubF.TAX6_Amt else case when SubF.TAX7='" + clsCommon.myCstr(dr("tax_code")) + "' then SubF.TAX7_Amt else case when SubF.TAX8='" + clsCommon.myCstr(dr("tax_code")) + "' then SubF.TAX2_Amt else case when SubF.TAX9='" + clsCommon.myCstr(dr("tax_code")) + "' then SubF.TAX9_Amt else case when SubF.TAX10='" + clsCommon.myCstr(dr("tax_code")) + "' then SubF.TAX10_Amt end end end end end end end end end end),0) as [" + clsCommon.myCstr(dr("tax_code_desc")) + "]"
            Next
        End If

        Dim SumTaxVariables As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select ',sum(XXX.['+Tax_Code_Desc+']) as ['+Tax_Code_Desc+']' from TSPL_TAX_MASTER where Excisable='Y' for xml path('')) as Tax"))
        Dim TaxVariables As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select ',['+Tax_Code_Desc+']' from TSPL_TAX_MASTER where Excisable='Y' for xml path('')) as Tax"))
        Dim totaltax As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select '['+Tax_Code_Desc+']+' from TSPL_TAX_MASTER where Excisable='Y' for xml path('')) as Tax"))
        If clsCommon.myLen(totaltax) > 0 Then
            totaltax = "," + totaltax + "0 as TotalTax"
        End If
        '===================end here============================================================

        If chkLocSelect.IsChecked Then
            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast One Location Code.", Me.Text)
                Return
            End If
        End If
        Dim LocFilter As String = ""
        If cbgLocation.CheckedValue.Count > 0 Then
            LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
            LocFilter = LocFilter.Replace("'", "")
        End If

        If rdUOMSelect.IsChecked Then
            If ChkUOM.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Atleast One UOM.", Me.Text)
                Return
            End If
            If ChkUOM.CheckedValue.Count > 1 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select only one UOM at a time.", Me.Text)
                Return
            End If
        End If

        '---------------------------------------------Finished Goods------------------------------------
        Dim strQryFG As String = "select "
        If rdbtnPosted.IsChecked Then
            strQryFG += " 'Status: Posted' as Status"
        Else
            strQryFG += " 'Status: All ' as Status"
        End If
        '  strQryFG += " , '" + startDate + "' AS StartDate, '" + EndDate + "' AS [End Date], '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time] ,'' as LocFilter,Type as [Transaction Type],From_Location_Code as [From Location Code],From_Location as [From Location] ,To_Location_Code as [To Location Code] ,To_Location as [To Location], Document_Code  as [Invoice No], convert(varchar,Sale_Invoice_Date,103) as [Invoice Date],Sale_Invoice_Date as DocDate, TSPL_ITEM_MASTER.Item_Code as [Item Code], ItemCode, TSPL_ITEM_MASTER.Item_Desc as [Item Desc],UOM,TSPL_CHAPTER_HEAD.Description as [Cheapter Heads],  Qty as QUANTITY, Abatement_Per,Total_Assessable_Amt, Date_Time_Removal, MRP as MRP,  " & _
        strQryFG += " , '" + startDate + "' AS StartDate, '" + EndDate + "' AS [End Date], '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time] ,'' as LocFilter,Type as [Transaction Type],From_Location_Code as [From Location Code],From_Location as [From Location] ,To_Location_Code as [To Location Code] ,To_Location as [To Location], Document_Code  as [Invoice No], convert(varchar,Sale_Invoice_Date,103) as [Invoice Date],convert(date,Sale_Invoice_Date,103) as DocDate,  TSPL_ITEM_MASTER.Item_Code as [Item Code], ItemCode, TSPL_ITEM_MASTER.Item_Desc as [Item Desc],TSPL_CHAPTER_HEAD.Description as [Cheapter Heads], "
        If rdUOMSelect.IsChecked Then
            strQryFG += "  uomcnvrsn.UOM_Code as UOM, convert(decimal(18,2),isnull(case when isnull(uomcnvrsn.conversion_factor,0)=0 then 0 else (final.Qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/uomcnvrsn.conversion_factor end,0)) as QUANTITY,"
        Else
            strQryFG += " UOM,Qty as QUANTITY,"
        End If
        strQryFG += "  Abatement_Per,Total_Assessable_Amt, Date_Time_Removal, MRP as MRP,  " & _
             "  Location " + TaxVariables + "" + totaltax + "  from (  " & _
            "SELECT Max(Type) as Type,max(Bill_To_Location ) as From_Location_Code,max(frmlocation ) as From_Location,max(Ship_To_Location ) as To_Location_Code,max(Tolocation ) as To_Location,Document_Code, MAX(Sale_Invoice_Date) as Sale_Invoice_Date, Item_Code, Item_Code+' ( '+CONVERT(varchar,MRP)+' )' as ItemCode, MAX(Item_Desc) as Item_Desc, " & _
            "SUM(Qty) as Qty,max(Abatement_Per) as Abatement_Per, SUM(Abatement_Amt) as Total_Assessable_Amt, MAX(TAX1_Rate) as TAX1_Rate, MAX(Date_Time_Removal) as Date_Time_Removal, " & _
            "MRP, MAX(Location) as Location,MAX(UOM) as UOM " + SumTaxVariables + " from ( " & _
            "SELECT subF.Type as Type,subF.UOM as UOM,subF.Bill_To_Location,subF.frmlocation,subF.Ship_To_Location,subF.Tolocation,subF.Document_Code,subF.Sale_Invoice_Date, subF.Item_Code,subF.Item_Desc, " & _
            "subF.Qty,subF.Abatement_Per,subF.Abatement_Amt,subF.TAX1_Rate,subF.Date_Time_Removal, " & _
            "subF.MRP,subF.Location" + InnerTaxCase + " from (" & _
            "SELECT 'Product Sale' as Type, TSPL_sd_SALE_INVOICE_DETAIL.Unit_code as UOM,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location,frmlocation.Location_Desc as frmlocation ,TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location,Tolocation.Location_Desc as Tolocation,TSPL_SD_SALE_INVOICE_HEAD.Document_Code , cast(TSPL_SD_SALE_INVOICE_HEAD.Document_Date as date ) as Sale_Invoice_Date, TSPL_sd_SALE_INVOICE_DETAIL.Item_Code, " & _
            "TSPL_ITEM_MASTER.item_desc,   TSPL_sd_SALE_INVOICE_DETAIL.Qty  AS Qty,   Abatement_Per,Abatement_Amt, " & _
            " TSPL_sd_SALE_INVOICE_DETAIL.TAX1 ,TSPL_sd_SALE_INVOICE_DETAIL.TAX1_Rate, TSPL_sd_SALE_INVOICE_DETAIL.TAX1_Amt as [TAX1_Amt],  TSPL_sd_SALE_INVOICE_DETAIL.TAX2,TSPL_sd_SALE_INVOICE_DETAIL.TAX2_Rate,   TSPL_sd_SALE_INVOICE_DETAIL.TAX2_Amt as [TAX2_Amt], TSPL_sd_SALE_INVOICE_DETAIL.TAX3,TSPL_sd_SALE_INVOICE_DETAIL.TAX3_Rate,  TSPL_sd_SALE_INVOICE_DETAIL.TAX3_Amt as [TAX3_Amt], " & _
            " TSPL_sd_SALE_INVOICE_DETAIL.TAX4,TSPL_sd_SALE_INVOICE_DETAIL.TAX4_Rate,  TSPL_sd_SALE_INVOICE_DETAIL.TAX4_Amt as [TAX4_Amt], " & _
            " TSPL_sd_SALE_INVOICE_DETAIL.TAX5,TSPL_sd_SALE_INVOICE_DETAIL.TAX5_Rate,  TSPL_sd_SALE_INVOICE_DETAIL.TAX5_Amt as [TAX5_Amt], " & _
            " TSPL_sd_SALE_INVOICE_DETAIL.TAX6 ,  TSPL_sd_SALE_INVOICE_DETAIL.TAX6_Rate, TSPL_sd_SALE_INVOICE_DETAIL.TAX6_Amt as [TAX6_Amt],  TSPL_sd_SALE_INVOICE_DETAIL.TAX7,TSPL_sd_SALE_INVOICE_DETAIL.TAX7_Rate,   TSPL_sd_SALE_INVOICE_DETAIL.TAX7_Amt as [TAX7_Amt], TSPL_sd_SALE_INVOICE_DETAIL.TAX8,TSPL_sd_SALE_INVOICE_DETAIL.TAX8_Rate,  TSPL_sd_SALE_INVOICE_DETAIL.TAX8_Amt as [TAX8_Amt], " & _
            " TSPL_sd_SALE_INVOICE_DETAIL.TAX9,TSPL_sd_SALE_INVOICE_DETAIL.TAX9_Rate,  TSPL_sd_SALE_INVOICE_DETAIL.TAX9_Amt as [TAX9_Amt], " & _
            " TSPL_sd_SALE_INVOICE_DETAIL.TAX10,TSPL_sd_SALE_INVOICE_DETAIL.TAX10_Rate,  TSPL_sd_SALE_INVOICE_DETAIL.TAX10_Amt as [TAX10_Amt]," & _
            " TSPL_SD_SALE_INVOICE_HEAD.Document_Date Date_Time_Removal, MRP , " & _
            "TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Location FROM TSPL_SD_SALE_INVOICE_HEAD  Left Outer JOIN  TSPL_sd_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_sd_SALE_INVOICE_DETAIL.Document_Code " & _
            " Left Outer JOIN  TSPL_LOCATION_MASTER frmlocation ON TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location = frmlocation.Location_Code" & _
            " Left Outer JOIN  TSPL_LOCATION_MASTER Tolocation ON TSPL_SD_SALE_INVOICE_HEAD.Ship_To_Location  = Tolocation.GIT_Location and coalesce(Tolocation.GIT_Location,'')<>'' " & _
            " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_sd_SALE_INVOICE_DETAIL.Item_Code where 2=2  and " & _
            " TSPL_SD_SALE_INVOICE_HEAD.Status =1  and Invoice_Type='E' and Trans_Type in ('PS','CSA') " & _
            "union ALL " & _
            " SELECT 'Transfer' as Type,TSPL_TRANSFER_ORDER_DETAIL.Unit_code as UOM,frm.Location_Code as [Frm Location Code],frm.Location_Desc as [frm Location Name],recv.Location_Code as [To Location Code],recv.Location_Desc as [To Location Name],TSPL_TRANSFER_ORDER_HEAD.Document_No  ,cast(TSPL_TRANSFER_ORDER_HEAD.Document_Date as Date) as Document_Date , TSPL_TRANSFER_ORDER_DETAIL.Item_Code ,TSPL_TRANSFER_ORDER_DETAIL.Item_Desc , " & _
            "TSPL_TRANSFER_ORDER_DETAIL.Out_Qty as Qty , TSPL_TRANSFER_ORDER_DETAIL.Abatement_Per ,Abatement_Amt,  " & _
             " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate , TSPL_TRANSFER_ORDER_DETAIL.TAX1_Amt  , TSPL_TRANSFER_ORDER_DETAIL.TAX2,TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate,TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt, " & _
            " TSPL_TRANSFER_ORDER_DETAIL.TAX3,TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate, TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt, " & _
            " TSPL_TRANSFER_ORDER_DETAIL.TAX4,TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate,TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt, " & _
            " TSPL_TRANSFER_ORDER_DETAIL.TAX5,TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate, TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt, " & _
            " TSPL_TRANSFER_ORDER_DETAIL.TAX6 ,TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate , TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt  , TSPL_TRANSFER_ORDER_DETAIL.TAX7,TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate,TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt, " & _
            " TSPL_TRANSFER_ORDER_DETAIL.TAX8,TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate, TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt, " & _
            " TSPL_TRANSFER_ORDER_DETAIL.TAX9,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt, " & _
            " TSPL_TRANSFER_ORDER_DETAIL.TAX10,TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate, TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt, " & _
             " '' AS Date_Time_Removal , MRP , TSPL_TRANSFER_ORDER_HEAD.From_Location as Location    from TSPL_TRANSFER_ORDER_HEAD " & _
            "Left Outer Join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_TRANSFER_ORDER_DETAIL.Document_No  " & _
           "  left join TSPL_LOCATION_MASTER recv on recv.GIT_Location=TSPL_TRANSFER_ORDER_HEAD.To_Location " & _
            " left join TSPL_LOCATION_MASTER frm on frm.Location_Code=TSPL_TRANSFER_ORDER_HEAD.From_Location" & _
            " left outer join TSPL_TAX_MASTER on TSPL_TRANSFER_ORDER_DETAIL.TAX1=TSPL_TAX_MASTER.Tax_Code  where 2=2  and  TSPL_TRANSFER_ORDER_HEAD.Status=1  and " & _
            "TSPL_TAX_MASTER.Excisable='Y' and Transfer_Type='O'  " & _
            "UNION ALL " & _
            " SELECT 'CSA Transfer' as Type,TSPL_CSA_TRANSFER_DETAIL.Unit_code as UOM,TSPL_csa_transfer_HEAD.From_Location_Code,frmlocation .Location_Desc as fromlocation ,TSPL_csa_transfer_HEAD.To_Location_Code,Tolocation .Location_Desc as ToLocation,TSPL_csa_transfer_HEAD.DOC_CODE , cast(TSPL_csa_transfer_HEAD.Transfer_Date as Date) as  Sale_Invoice_Date, TSPL_CSA_TRANSFER_DETAIL.Item_Code, " & _
            "TSPL_ITEM_MASTER.item_desc,   TSPL_CSA_TRANSFER_DETAIL.Qty  AS Qty, Abatement_Pers,Abatement_Amt ,  " & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX1 ,TSPL_CSA_TRANSFER_DETAIL.TAX1_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX1_Amt as [TAX1_Amt], " & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX2,TSPL_CSA_TRANSFER_DETAIL.TAX2_Rate,   TSPL_CSA_TRANSFER_DETAIL.TAX2_Amt as [TAX2_Amt]," & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX3,TSPL_CSA_TRANSFER_DETAIL.TAX3_Rate,  TSPL_CSA_TRANSFER_DETAIL.TAX3_Amt as [TAX3_Amt]," & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX4,TSPL_CSA_TRANSFER_DETAIL.TAX4_Rate,   TSPL_CSA_TRANSFER_DETAIL.TAX4_Amt as [TAX4_Amt]," & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX5,TSPL_CSA_TRANSFER_DETAIL.TAX5_Rate,  TSPL_CSA_TRANSFER_DETAIL.TAX5_Amt as [TAX5_Amt]," & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX6,TSPL_CSA_TRANSFER_DETAIL.TAX6_Rate,   TSPL_CSA_TRANSFER_DETAIL.TAX6_Amt as [TAX6_Amt]," & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX7,TSPL_CSA_TRANSFER_DETAIL.TAX7_Rate,  TSPL_CSA_TRANSFER_DETAIL.TAX7_Amt as [TAX7_Amt]," & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX8,TSPL_CSA_TRANSFER_DETAIL.TAX8_Rate,   TSPL_CSA_TRANSFER_DETAIL.TAX8_Amt as [TAX8_Amt]," & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX9,TSPL_CSA_TRANSFER_DETAIL.TAX9_Rate,  TSPL_CSA_TRANSFER_DETAIL.TAX9_Amt as [TAX9_Amt]," & _
            " TSPL_CSA_TRANSFER_DETAIL.TAX10,TSPL_CSA_TRANSFER_DETAIL.TAX10_Rate,   TSPL_CSA_TRANSFER_DETAIL.TAX10_Amt as [TAX10_Amt]," & _
             " TSPL_csa_transfer_HEAD.Transfer_Date AS Date_Time_Removal, MRP , " & _
            "TSPL_csa_transfer_HEAD.From_Location_Code as Location FROM TSPL_csa_transfer_HEAD  Left Outer JOIN  TSPL_CSA_TRANSFER_DETAIL ON " & _
            "TSPL_csa_transfer_HEAD.DOC_CODE = TSPL_CSA_TRANSFER_DETAIL.DOC_CODE" & _
            " Left Outer JOIN  TSPL_LOCATION_MASTER as frmlocation ON TSPL_csa_transfer_HEAD.From_Location_Code = frmlocation.Location_Code " & _
              "Left Outer JOIN  TSPL_LOCATION_MASTER as Tolocation ON TSPL_csa_transfer_HEAD.To_Location_Code  = Tolocation.GIT_Location" & _
            "  Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_CSA_TRANSFER_DETAIL.Item_Code where 2=2  and TSPL_csa_transfer_HEAD.Status =1  and TSPL_csa_transfer_HEAD.Excisable='1'" & _
            ")subF ) XXX  GROUP BY Document_Code, Item_Code, MRP  "
        strQryFG += " ) as final Left OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =final.Item_Code LEFT OUTER JOIN TSPL_CHAPTER_HEAD on TSPL_ITEM_MASTER.Cheapter_Heads=TSPL_CHAPTER_HEAD.Chapter_Head_Code "

        If rdUOMSelect.IsChecked Then
            strQryFG += "   LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL  ON TSPL_ITEM_UOM_DETAIL.Item_Code=final.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code=FINAL.UOM" & _
            " left outer join TSPL_ITEM_UOM_DETAIL uomcnvrsn on uomcnvrsn.Item_Code=final.Item_Code and uomcnvrsn.UOM_Code in (" + clsCommon.GetMulcallString(ChkUOM.CheckedValue) + ")"
        End If
        strQryFG += " where (Sale_Invoice_Date >= '" + startDate + "') AND (Sale_Invoice_Date <= '" + EndDate + "') "
        If rdUOMSelect.IsChecked Then
            strQryFG += " AND isnull(uomcnvrsn.conversion_factor,0)<>0 "
        End If
        If chkLocSelect.IsChecked Then
            strQryFG += " and final.Location in   (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")  "
        Else
            strQryFG += " and final.Location in   (" + clsCommon.GetMulcallString(cbgLocation.AllValue) + ")  "
        End If


        '------------------------------------------------------------------------------------------------

        '--------------------------------------------Others Goods----------------------------------------

        If rbtnSummary.IsChecked = True Then
            strReportName = "crptExciseSummary"
        Else
            strReportName = "crptExciseDetails"
        End If


        Try

            If rbtnSummary.IsChecked = True Then
                SumTaxVariables = SumTaxVariables.Replace("XXX", "SUMMARY")
                totaltax = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select 'sum(['+Tax_Code_Desc+'])+' from TSPL_TAX_MASTER where Excisable='Y' for xml path('')) as Tax"))
                If clsCommon.myLen(totaltax) > 0 Then
                    totaltax = "," + totaltax + "0 as TotalTax"
                End If
                'If chkCHapterWise.Checked Then
                '    strQryFG = "SELECT '" + IIf(rdbtnPosted.IsChecked, "Status:Posted", "Status:All") + "' As Status, '" + startDate + "' AS StartDate, '" + EndDate + "' AS EndDate, '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time], '" + clsCommon.GETSERVERDATE() + "' AS RunDate, Max([Transaction Type] ) as [Transaction Type],[Invoice No] , MAX([Invoice Date] ) AS [Invoice Date],max(DocDate)  as DocDate, SUM(QUANTITY ) as Qty, SUM(Total_Assessable_Amt) as [Total Assessable Amt] " + SumTaxVariables + "" + totaltax + " from ( " + strQryFG + " ) SUMMARY Group By Cheapter_Heads"
                'Else
                '    strQryFG = "SELECT '" + IIf(rdbtnPosted.IsChecked, "Status:Posted", "Status:All") + "' As Status, '" + startDate + "' AS StartDate, '" + EndDate + "' AS EndDate, '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time], '" + clsCommon.GETSERVERDATE() + "' AS RunDate, Max([Transaction Type] ) as [Transaction Type],[Invoice No] , MAX([Invoice Date] ) AS [Invoice Date],MAX([Invoice Date] ) as DocDate, SUM(QUANTITY ) as Qty, SUM(Total_Assessable_Amt) as [Total Assessable Amt] " + SumTaxVariables + "" + totaltax + " from ( " + strQryFG + " ) SUMMARY Group By [Invoice No]"
                'End If
                If chkCHapterWise.Checked Then
                    strQryFG = "SELECT '" + IIf(rdbtnPosted.IsChecked, "Status:Posted", "Status:All") + "' As Status, '" + startDate + "' AS StartDate, '" + EndDate + "' AS EndDate, '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time], '" + clsCommon.GETSERVERDATE() + "' AS RunDate, Max([Transaction Type] ) as [Transaction Type],[Invoice No] , convert(varchar , MAX([Invoice Date] ),103) AS [Invoice Date],max(DocDate)  as DocDate, SUM(QUANTITY ) as Qty, SUM(Total_Assessable_Amt) as [Total Assessable Amt] " + SumTaxVariables + "" + totaltax + " from ( " + strQryFG + " ) SUMMARY Group By Cheapter_Heads"
                Else
                    strQryFG = "SELECT '" + IIf(rdbtnPosted.IsChecked, "Status:Posted", "Status:All") + "' As Status, '" + startDate + "' AS StartDate, '" + EndDate + "' AS EndDate, '" + startTime + "' AS [Start Time], '" + EndTime + "' AS [End Time], '" + clsCommon.GETSERVERDATE() + "' AS RunDate, Max([Transaction Type] ) as [Transaction Type],[Invoice No] , convert(varchar , MAX([Invoice Date] ),103) AS [Invoice Date],convert(date,MAX([Invoice Date] ),103) as DocDate, SUM(QUANTITY ) as Qty, SUM(Total_Assessable_Amt) as [Total Assessable Amt] " + SumTaxVariables + "" + totaltax + " from ( " + strQryFG + " ) SUMMARY Group By [Invoice No]"
                End If
            End If
            strQryFG = strQryFG & " order by DocDate"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQryFG)
            gv1.DataSource = Nothing
            gv1.DataSource = dt


            FormatGrid()
            'gv1.Columns("Status").IsVisible = False
            'gv1.Columns("StartDate").IsVisible = False
            'gv1.Columns("End Date").IsVisible = False
            'gv1.Columns("Start Time").IsVisible = False
            'gv1.Columns("End Time").IsVisible = False
            'gv1.Columns("LocFilter").IsVisible = False
            'gv1.Columns("From Location Code").IsVisible = False
            'gv1.Columns("To Location Code").IsVisible = False
            'gv1.Columns("DocDate").IsVisible = False
            'gv1.Columns("ItemCode").IsVisible = False
            'gv1.Columns("Date_Time_Removal").IsVisible = False
            'gv1.Columns("Location").IsVisible = False
            'gv1.Columns("Abatement_Per").IsVisible = False
            'gv1.Columns("Total_Assessable_Amt").IsVisible = False
            gv1.BestFitColumns()
            ReStoreGridLayout()
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtStart.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtEnd.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If chkLocSelect.IsChecked Then
                Dim strlocName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strlocName) > 0 Then
                        strlocName += ", "
                    End If
                    strlocName += StrName
                Next
                Dim strlocCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strlocCode) > 0 Then
                        strlocCode += ", "
                    End If
                    strlocCode += StrCode
                Next
                arrHeader.Add(("Location : " + strlocName + " "))

            End If

            If IsPrint Then
                If rbtnSummary.IsChecked = True Then
                    Dim FRMcrys As New frmCrystalReportViewer
                    FRMcrys.funreport(CrystalReportFolder.SalesReport, dt, strReportName, "Excise Report")
                Else
                    Dim FRMcrys As New frmCrystalReportViewer
                    FRMcrys.funreport(CrystalReportFolder.SalesReport, dt, strReportName, "Excise Report")
                End If
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FormatGrid()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        'For Each col As GridViewColumn In gv1.Columns
        '    col.IsVisible = False
        'Next
        If rbtnSummary.IsChecked Then
            If chkCHapterWise.Checked Then
                gv1.Columns("Cheapter_Heads").IsVisible = True
                gv1.Columns("Cheapter_Heads").HeaderText = "CHAPTER HEAD"
                gv1.Columns("Cheapter_Heads").Width = 100
            Else
                gv1.Columns("Invoice No").IsVisible = True
                gv1.Columns("Invoice No").HeaderText = "INVOICE NO"
                gv1.Columns("Invoice No").Width = 120
            End If

            gv1.Columns("Status").IsVisible = False
            gv1.Columns("StartDate").IsVisible = False
            gv1.Columns("EndDate").IsVisible = False
            gv1.Columns("Start Time").IsVisible = False
            gv1.Columns("End Time").IsVisible = False
            gv1.Columns("RunDate").IsVisible = False
            gv1.Columns("DocDate").IsVisible = False

            'gv1.Columns("Invoice Date").FormatString = "{0:D}"


            gv1.Columns("TotalTax").IsVisible = True
            gv1.Columns("TotalTax").HeaderText = "G.TOTAL"
            gv1.Columns("TotalTax").Width = 170

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim SumQty As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumQty)

            Dim SumBed As New GridViewSummaryItem()
            For Each col As GridViewColumn In gv1.Columns
                If col.Index > 11 Then
                    SumBed = New GridViewSummaryItem("" + clsCommon.myCstr(col.HeaderText) + "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(SumBed)
                End If
            Next
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Else

            gv1.Columns("Status").IsVisible = False
            gv1.Columns("StartDate").IsVisible = False
            gv1.Columns("End Date").IsVisible = False
            gv1.Columns("Start Time").IsVisible = False
            gv1.Columns("End Time").IsVisible = False
            gv1.Columns("LocFilter").IsVisible = False
            gv1.Columns("From Location Code").IsVisible = False
            gv1.Columns("To Location Code").IsVisible = False
            gv1.Columns("DocDate").IsVisible = False
            gv1.Columns("ItemCode").IsVisible = False
            gv1.Columns("Date_Time_Removal").IsVisible = False
            gv1.Columns("Location").IsVisible = False
            gv1.Columns("Abatement_Per").IsVisible = False
            gv1.Columns("Total_Assessable_Amt").IsVisible = False

            'gv1.Columns("Invoice Date").FormatString = "{0:d}"
            gv1.Columns("Invoice Date").FormatString = "{0:dd/MM/yyyy}"

            'gv1.Columns("Type").IsVisible = True
            'gv1.Columns("Type").HeaderText = "Transaction Type"
            'gv1.Columns("Type").Width = 100

            'gv1.Columns("From_Location_Code").IsVisible = False
            'gv1.Columns("From_Location_Code").HeaderText = "From Location Code"
            'gv1.Columns("From_Location_Code").Width = 120

            'gv1.Columns("From_Location").IsVisible = True
            'gv1.Columns("From_Location").HeaderText = "From Location"
            'gv1.Columns("From_Location").Width = 120

            'gv1.Columns("To_Location_Code").IsVisible = False
            'gv1.Columns("To_Location_Code").HeaderText = "To Location Code"
            'gv1.Columns("To_Location_Code").Width = 120

            'gv1.Columns("To_Location").IsVisible = True
            'gv1.Columns("To_Location").HeaderText = "To Location"
            'gv1.Columns("To_Location").Width = 120

            'gv1.Columns("Sale_Invoice_No").IsVisible = True
            'gv1.Columns("Sale_Invoice_No").HeaderText = "Invoice No"
            'gv1.Columns("Sale_Invoice_No").Width = 120

            'gv1.Columns("Sale_Invoice_Date").IsVisible = True
            'gv1.Columns("Sale_Invoice_Date").HeaderText = "Invoice Date"
            'gv1.Columns("Sale_Invoice_Date").Width = 150

            'gv1.Columns("Item_Code").IsVisible = True
            'gv1.Columns("Item_Code").HeaderText = "Item Code"
            'gv1.Columns("Item_Code").Width = 80

            'gv1.Columns("Item_Desc").IsVisible = True
            'gv1.Columns("Item_Desc").HeaderText = "Item Desc"
            'gv1.Columns("Item_Desc").Width = 200

            'gv1.Columns("Cheapter_Heads").IsVisible = True
            'gv1.Columns("Cheapter_Heads").HeaderText = "Cheapter Heads"
            'gv1.Columns("Cheapter_Heads").Width = 100

            'gv1.Columns("MRP_Amt").IsVisible = True
            'gv1.Columns("MRP_Amt").HeaderText = "MRP"
            'gv1.Columns("MRP_Amt").Width = 80

            'gv1.Columns("Qty").IsVisible = True
            'gv1.Columns("Qty").HeaderText = "QUANTITY"
            'gv1.Columns("Qty").Width = 80

            ''gv1.Columns("TAX1_Amt").IsVisible = True
            ''gv1.Columns("TAX1_Amt").HeaderText = "B.E.D."
            ''gv1.Columns("TAX1_Amt").Width = 100

            ''gv1.Columns("TAX2_Amt").IsVisible = True
            ''gv1.Columns("TAX2_Amt").HeaderText = "EDN CESSS"
            ''gv1.Columns("TAX2_Amt").Width = 100

            ''gv1.Columns("TAX3_Amt").IsVisible = True
            ''gv1.Columns("TAX3_Amt").HeaderText = "SH.ED CESS"
            ''gv1.Columns("TAX3_Amt").Width = 100

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim SumBed As New GridViewSummaryItem()
            For Each col As GridViewColumn In gv1.Columns
                If col.Index > 23 Then
                    SumBed = New GridViewSummaryItem("" + clsCommon.myCstr(col.HeaderText) + "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(SumBed)
                End If
            Next
            'Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim SumQty As New GridViewSummaryItem("QUANTITY", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumQty)
            Dim SumAmount As New GridViewSummaryItem("Total_Assessable_Amt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(SumAmount)
            'Dim SumBed As New GridViewSummaryItem("TAX1_Amt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SumBed)
            'Dim SumEcess As New GridViewSummaryItem("TAX2_Amt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SumEcess)
            'Dim SumHCess As New GridViewSummaryItem("TAX3_Amt", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(SumHCess)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
    End Sub
    Private Function funSetUserAccess() As Boolean
        Try

            Dim strRights As String
            Dim strTemp() As String
            Dim strProgCode = ""
            strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
            strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
            strTemp = Split(strRights, ",")
            If strTemp(0) = "0" Then
                MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
                funSetUserAccess = False
                blnRead = False
                Me.Close()
                Exit Function
            Else
                blnRead = True
            End If
            funSetUserAccess = True
        Catch er As Exception
            myMessages.myExceptions(er)
            Return False
        End Try
        Return True
    End Function
    Sub LoadLocation()
        Dim qry As String = " select Location_Code,Location_Desc from TSPL_LOCATION_MASTER where Location_Type='Physical' and Excisable='T' "
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location_Code"
        cbgLocation.DisplayMember = "Location_Desc"
    End Sub
    Sub LoadUOM()
        Dim qry As String = " Select Unit_Code ,Unit_Desc  from tspl_unit_master "
        ChkUOM.DataSource = clsDBFuncationality.GetDataTable(qry)
        ChkUOM.ValueMember = "Unit_Code"
        ChkUOM.DisplayMember = "Unit_Desc"
    End Sub

    Private Sub ExportToExcelGV(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)
            If chkCHapterWise.Checked Then
                arrHeader.Add("EXCISE DUTY CALCULATION  - CHAPTERWISE " + clsCommon.GETSERVERDATE() + " ")
            Else
                arrHeader.Add("EXCISE DUTY CALCULATION  " + clsCommon.GETSERVERDATE() + " ")
            End If
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtStart.Value, "dd/MMM/yyyy") + " To " + clsCommon.GetPrintDate(dtEnd.Value, "dd/MMM/yyyy") + " ")
            arrHeader.Add("From Time : " + clsCommon.GetPrintDate(dtStart.Value, "hh:mm tt") + " To " + clsCommon.GetPrintDate(dtEnd.Value, "hh:mm tt") + " ")


            clsCommon.MyExportToExcelGrid("Excise Summary", gv1, arrHeader, Me.Text)

        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            'clsCommon.ProgressBarHide()
        End Try
    End Sub
    Private Sub ShowButtons()
        btnPrint.Visible = False
        btnExp.Visible = True
    End Sub
#End Region
#Region "#Events"

    Private Sub FrmExciseSummaryReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print(True)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub FrmExciseSummaryReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtStart.Value = System.DateTime.Now()
        dtEnd.Value = System.DateTime.Now()
        LoadLocation()
        LoadUOM()
        chkLocAll.IsChecked = True
        rduomAll.IsChecked = True
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        ' Ticket No : KDI/03/10/18-000435
        SetUserMgmtNew()
        'btnExport.Visible = False
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print ")
    End Sub
    Private Sub btnReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferesh.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnSummary.IsChecked = True, "S", "D")
        TemplateGridview = gv1
        print(False)
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print(True)
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub
    Private Sub rbtnDetail_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnDetail.ToggleStateChanged
        ShowButtons()
        If rbtnDetail.IsChecked Then
            chkIQtywise.Checked = False
            chkAllItems.Enabled = False
            chkCHapterWise.Enabled = False
            chkIQtywise.Enabled = True
            chkImrpWise.Enabled = True
        End If
    End Sub
    Private Sub rbtnSummary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnSummary.ToggleStateChanged
        ShowButtons()
        If rbtnSummary.IsChecked Then
            chkImrpWise.Enabled = False
            chkIQtywise.Enabled = False
            chkAllItems.Checked = True
            chkCHapterWise.Checked = False
            chkAllItems.Enabled = True
            chkCHapterWise.Enabled = True
        End If
    End Sub
    Private Sub chkImrpWise_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkImrpWise.ToggleStateChanged
        ShowButtons()
        If chkImrpWise.Checked Then
            chkIQtywise.Checked = False
            btnPrint.Visible = False
        End If
    End Sub
    Private Sub chkIQtywise_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIQtywise.ToggleStateChanged
        ShowButtons()
        If chkIQtywise.Checked Then
            chkImrpWise.Checked = False
            btnPrint.Visible = False
        End If
    End Sub
    Private Sub chkDocWise_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAllItems.ToggleStateChanged, chkAllItems.ToggleStateChanged
        If chkAllItems.Checked Then
            chkCHapterWise.Checked = False
        End If
    End Sub
    Private Sub chkCHapterWise_ToggleStateChanged_1(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCHapterWise.ToggleStateChanged
        ShowButtons()
        If chkCHapterWise.Checked Then
            chkAllItems.Checked = False
            btnPrint.Visible = False
        End If
    End Sub
#End Region

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(dtStart.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtEnd.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))

                'If chkCHapterWise.Checked Then
                '    arrHeader.Add("EXCISE DUTY CALCULATION  - CHAPTERWISE " + clsCommon.GETSERVERDATE() + " ")
                'Else
                '    arrHeader.Add("EXCISE DUTY CALCULATION  " + clsCommon.GETSERVERDATE() + " ")
                'End If
                arrHeader.Add("From Date : " + clsCommon.GetPrintDate(dtStart.Value, "dd/MMM/yyyy") + " To " + clsCommon.GetPrintDate(dtEnd.Value, "dd/MMM/yyyy") + " ")
                arrHeader.Add("From Time : " + clsCommon.GetPrintDate(dtStart.Value, "hh:mm tt") + " To " + clsCommon.GetPrintDate(dtEnd.Value, "hh:mm tt") + " ")


            If chkLocSelect.IsChecked Then
                Dim strlocName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strlocName) > 0 Then
                        strlocName += ", "
                    End If
                    strlocName += StrName
                Next
                Dim strlocCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strlocCode) > 0 Then
                        strlocCode += ", "
                    End If
                    strlocCode += StrCode
                Next
                arrHeader.Add(("Location : " + strlocName + " "))

            End If

            If rdUOMSelect.IsChecked Then
                Dim strlocName As String = ""
                For Each StrName As String In ChkUOM.CheckedDisplayMember
                    If clsCommon.myLen(strlocName) > 0 Then
                        strlocName += ", "
                    End If
                    strlocName += StrName
                Next
                Dim strlocCode As String = ""
                For Each StrCode As String In ChkUOM.CheckedValue
                    If clsCommon.myLen(strlocCode) > 0 Then
                        strlocCode += ", "
                    End If
                    strlocCode += StrCode
                Next
                arrHeader.Add(("UOM : " + strlocName + " "))

            End If


            If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("EXCISE DUTY CALCULATION", gv1, arrHeader, "EXCISE DUTY CALCULATION", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                RadMessageBox.Show(Me, "No Data Found to Display", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rduomAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rduomAll.ToggleStateChanged
        ChkUOM.Enabled = Not rduomAll.IsChecked
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            common.clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub
End Class
