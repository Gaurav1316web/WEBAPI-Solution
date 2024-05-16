Imports common
Imports System.IO
Public Class rptDailyLeakageReplacementReport
    Inherits FrmMainTranScreen
    Dim ReportName As String = ""
    Dim strQry As String = ""
    Dim dt As DataTable


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID + clsCommon.myCstr(IIf(chkMonthlyReplacement.Checked = True, "MR", "")) + clsCommon.myCstr(IIf(chkAbstract.Checked = True, "AM", ""))
        If chkMonthlyReplacement.Checked = True Then
            ReportName = "Monthly Replacement Report"
        ElseIf chkAbstract.Checked = True Then
            ReportName = "Abstract Monthly Report"
        ElseIf chkDocumentWise.Checked = True Then
            ReportName = "Document Wise Report"
        Else
            ReportName = "Daily Leakage Replacement Report"
        End If

        Print(Exporter.Refresh)
    End Sub
    Private Sub Print(ByVal IsPrint As Exporter)
        Try
            If chkMonthlyReplacement.Checked = True AndAlso chkAbstract.Checked = True Then
                Throw New Exception("Select only one check box at a time Monthly Replacement / Abstract (Monthly) ")
            End If

            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Dim dtgv As New DataTable

            Dim MainQuery As String = String.Empty
            'Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty

            Dim ItemInUse As String = ""
            Dim query As String = ""

            Dim StartDate As String = ""
            Dim EndDate As String = ""
            StartDate = "01/" + clsCommon.myCstr(Month(fromDate.Value)) + "/" + clsCommon.myCstr(Year(fromDate.Value))
            EndDate = clsCommon.myCstr(Month(fromDate.Value)) + "/01/" + clsCommon.myCstr(Year(fromDate.Value))
            EndDate = clsDBFuncationality.getSingleValue("select EOMONTH('" + EndDate + "')")
            If chkAbstract.Checked = True Then
                Dim strComplaintType As String = clsDBFuncationality.getSingleValue("  Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) Select   STUFF((Select distinct ',' + QUOTENAME(TSPL_CUSTOMER_COMPLAINT_MASTER.Description ) as Alies_Name FROM TSPL_CUSTOMER_COMPLAINT_MASTER order by Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                Dim strComplaintSum As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME(TSPL_CUSTOMER_COMPLAINT_MASTER.Description) +',0))' +' as ' + QUOTENAME( TSPL_CUSTOMER_COMPLAINT_MASTER.Description) as Alies_Name  FROM TSPL_CUSTOMER_COMPLAINT_MASTER order by Alies_Name  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                Dim strTotalComplaint As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME(TSPL_CUSTOMER_COMPLAINT_MASTER.Description) +',0))'  as Alies_Name  FROM TSPL_CUSTOMER_COMPLAINT_MASTER order by Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")

                If String.IsNullOrEmpty(strComplaintType) Then
                    clsCommon.MyMessageBoxShow(Me, "No Item Found to Display", Me.Text)
                    Exit Sub
                End If

                strWhrClause2 = " and TSPL_CUSTOMER_COMPLAINT_HEAD.IsPosted='Y' and convert(date, TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Date,103) >= convert(date,('" + StartDate + "'),103) and  convert(date, TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Date,103) <= convert(date,('" + EndDate + "'),103)"

                Dim dtComplaint As New DataTable
                Dim dtReplacement As New DataTable
                Dim StrComplaint = " select 1 as [SNo],'Returned From Market' as [Particulars], " + strComplaintSum + " , " + strTotalComplaint + "  as [Grand Total] from (select zzz.Item_Code,TSPL_CUSTOMER_COMPLAINT_MASTER.Description,cast(sum(QtyLtr) as decimal(18,2)) as QtyLtr from  ( " &
                " Select TSPL_CUSTOMER_COMPLAINT_DETAIL.complaint_code, TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code As Item_Code  , TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Qty As Qty,(Case When TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Qty=0 Then 0 Else (TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor End) As QtyLtr  From TSPL_CUSTOMER_COMPLAINT_DETAIL   Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD On TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_No = TSPL_CUSTOMER_COMPLAINT_DETAIL.Complaint_No   Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_CUSTOMER_COMPLAINT_HEAD.Cust_Code   left outer join  TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_CUSTOMER_COMPLAINT_HEAD.Invoice_No  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code   left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Uom =TSPL_ITEM_UOM_DETAILUOM.UOM_Code  where   isnull(TSPL_CUSTOMER_COMPLAINT_DETAIL.Complaint_Code,'')<>'' and TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Qty>0  " + strWhrClause2 + "" &
              " )zzz   left outer join TSPL_CUSTOMER_COMPLAINT_MASTER on TSPL_CUSTOMER_COMPLAINT_MASTER.code=zzz.complaint_code WHERE 1=1 group by zzz.Item_Code,TSPL_CUSTOMER_COMPLAINT_MASTER.Description) as s pivot (  sum(QtyLtr) for Description in ( " + strComplaintType + " ) ) as zpivot  "

                dtComplaint = clsDBFuncationality.GetDataTable(StrComplaint)

                Dim StrReplacement = " select 2 as [SNo],'Replaced By MPF - Hyd' as [Particulars], " + strComplaintSum + " , " + strTotalComplaint + "  as [Grand Total] from (select zzz.Item_Code,TSPL_CUSTOMER_COMPLAINT_MASTER.Description,cast(sum(QtyLtr) as decimal(18,2)) as QtyLtr from  ( " &
                "  Select TSPL_CUSTOMER_COMPLAINT_DETAIL.complaint_code,TSPL_SD_SHIPMENT_HEAD.Document_Code, TSPL_SD_SHIPMENT_DETAIL.Item_Code As Item_Code " &
              " ,(Case When TSPL_SD_SHIPMENT_DETAIL.Qty=0 Then 0 Else (TSPL_SD_SHIPMENT_DETAIL.Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor End) As QtyLtr From TSPL_CUSTOMER_COMPLAINT_HEAD  " &
             " Left Outer Join TSPL_SD_SHIPMENT_HEAD On  TSPL_SD_SHIPMENT_HEAD.customer_complaint_no = TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no   left outer join TSPL_SD_SHIPMENT_DETAIL    On TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code   Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code    Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code   left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code  " &
             " Left Outer Join TSPL_CUSTOMER_COMPLAINT_DETAIL  On TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_No = TSPL_CUSTOMER_COMPLAINT_DETAIL.Complaint_No  " &
             " AND TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code AND TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_UOM=TSPL_SD_SHIPMENT_DETAIL.Unit_code " &
              " where TSPL_SD_SHIPMENT_HEAD.IsReplacement=1  " + strWhrClause2 + "" &
              " )zzz   left outer join TSPL_CUSTOMER_COMPLAINT_MASTER on TSPL_CUSTOMER_COMPLAINT_MASTER.code=zzz.complaint_code WHERE 1=1 group by zzz.Item_Code,TSPL_CUSTOMER_COMPLAINT_MASTER.Description) as s pivot (  sum(QtyLtr) for Description in ( " + strComplaintType + " ) ) as zpivot  "


                dtReplacement = clsDBFuncationality.GetDataTable(StrReplacement)
                Dim dtData As DataTable

                If (dtComplaint IsNot Nothing AndAlso dtComplaint.Rows.Count > 0) Then

                    dtData = dtComplaint.Copy()


                    If (dtReplacement IsNot Nothing AndAlso dtReplacement.Rows.Count > 0) Then
                        dtData.Merge(dtReplacement, True, MissingSchemaAction.Ignore)
                    Else
                        Dim RowReplacement As DataRow = dtData.NewRow
                        RowReplacement("SNo") = 2
                        RowReplacement("Particulars") = clsCommon.myCstr("Replaced By MPF-Hyd")
                        dtData.Rows.Add(RowReplacement)
                    End If

                    Dim RowDifference As DataRow = dtData.NewRow
                    RowDifference("SNo") = 3
                    RowDifference("Particulars") = clsCommon.myCstr("Difference")

                    For j As Integer = 2 To dtData.Columns.Count - 1
                        'Dim TempColDiff As Decimal = Convert.ToDecimal(dtData.Rows(0)(j)) - Convert.ToDecimal(dtData.Rows(1)(j))
                        Dim TempCol1 As Decimal = Convert.ToDecimal(IIf(DBNull.Value.Equals(dtData.Rows(0)(j)), 0, dtData.Rows(0)(j)))
                        Dim TempCol2 As Decimal = Convert.ToDecimal(IIf(DBNull.Value.Equals(dtData.Rows(1)(j)), 0, dtData.Rows(1)(j)))
                        Dim TempColDiff As Decimal = TempCol1 - TempCol2
                        RowDifference(dtData.Columns(j).ColumnName.ToString()) = TempColDiff
                    Next
                    dtData.Rows.Add(RowDifference)
                    dtgv = Nothing
                    dtgv = dtData.Copy()
                End If

            ElseIf chkMonthlyReplacement.Checked = True Then

                    strWhrClause2 = " and TSPL_SD_SHIPMENT_HEAD.IsReplacement=1 and convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >= convert(date,('" + StartDate + "'),103) and  convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <= convert(date,('" + EndDate + "'),103) "
                    ItemInUse = " TSPL_SD_SHIPMENT_DETAIL Left Outer Join TSPL_SD_SHIPMENT_HEAD On TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code where (TSPL_ITEM_MASTER.Alies_Name !='' or TSPL_ITEM_MASTER.Alies_Name is null) "
                    ItemInUse += strWhrClause2
                    ItemInUse += " order by Alies_Name "

                    Dim strItem2 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))

                    If String.IsNullOrEmpty(strItem2) Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                    End If

                    ''Dim strGrandTotalWithoutScheme As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +',0))'  as Alies_Name  FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                    Dim strItem2WithSum As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +',0))' + ' as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                    '', (" + strGrandTotalWithoutScheme + ") as [Grand Total]
                    query = " select   Document_Date as [Document Date] , " + strItem2WithSum + "  ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from (select max(zzz.item_code) as item_code, max(Document_Date) as Document_Date,zzz.Description ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select TSPL_SD_SHIPMENT_HEAD.Document_Code, Convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date, TSPL_SD_SHIPMENT_DETAIL.Item_Code as Item_Code , TSPL_ITEM_MASTER.Alies_Name As [Description] , TSPL_SD_SHIPMENT_DETAIL.Qty as Qty,(CASE WHEN TSPL_SD_SHIPMENT_DETAIL.Qty=0 THEN 0 ELSE (TSPL_SD_SHIPMENT_DETAIL.Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor END) AS QtyLtr From TSPL_SD_SHIPMENT_DETAIL Left Outer Join TSPL_SD_SHIPMENT_HEAD On TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code  " &
                                    " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                    "  where 2=2 " + strWhrClause2 + " )zzz where 1=1 group by zzz.Document_Date,zzz.Description ) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Document_Date order by convert(date,zpivot.Document_Date,103) "


                Else
                    strWhrClause2 = " and TSPL_CUSTOMER_COMPLAINT_HEAD.IsPosted='Y' and convert(date, TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Date,103) = '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "'"
                ItemInUse = " TSPL_CUSTOMER_COMPLAINT_DETAIL Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD On TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_No = TSPL_CUSTOMER_COMPLAINT_DETAIL.Complaint_No Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code where (TSPL_ITEM_MASTER.Alies_Name !='' or TSPL_ITEM_MASTER.Alies_Name is null) "
                ItemInUse += strWhrClause2 + " and TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Qty>0 "
                ItemInUse += " order by Alies_Name "

                Dim strReplacementItem As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name+'(R)') as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))

                Dim strItem As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))


                Dim strItem2 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))

                If String.IsNullOrEmpty(strItem2) Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                'Dim strItmeHeadingReplacement As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +  QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +' as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name+'(R)')  as Alies_Name FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))


                Dim strSumItemOnly As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +',0))' +' as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name  FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))


                Dim strSumItemReplacementOnly As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name+'(R)') +',0))' +' as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name+'(R)') as Alies_Name  FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))

                Dim strSumItemReplacementOnly2 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +',0))' +' as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name+'(R)') as Alies_Name  FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
                ''Dim strGrandTotal As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +',0))' +' + ' + +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name+'(R)') +',0))'  as Alies_Name  FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")


                ''Dim strGrandTotalWithoutScheme As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +',0))'  as Alies_Name  FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")


                ''Dim strItem2WithSum As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +',0))' + ' as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                'Dim StrReplacement = "select  [VEHICLE NO],[WdName],[Group],[Cust Group Desc],[Customer Category Code],[Zone], " + strItem + " , " + strItmeHeadingReplacement + " from (select zzz.[VEHICLE NO],zzz.WdName,zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.cust_category_code as [Customer Category Code],zzz.Zone_Code as [Zone] ,sum(qty) as qty from  (Select TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Item_Code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No,TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.cust_category_code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '') As [VEHICLE NO], TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.QTY as Qty, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc] From TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE Left Outer Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where 2=2  " + strWhrClause2 + " )zzz  WHERE zzz.Scheme_Item='Y' group by zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.Cust_Group_Code,zzz.cust_category_code,zzz.Zone_Code 	) as s pivot (  sum(Qty) for Description in (" + strItem2 + ") ) as zpivot  "

                If chkDocumentWise.Checked = True Then
                    Dim StrReplacement = " select Complaint_no as [Complaint No],document_code as [Shipment No],sale_invoice_no as [Invoice No],Cust_Code as [Customer Code],status as [Shipment Status], " + strItem + " , " + strSumItemReplacementOnly2 + ",0 as [QtyLtr] ,cast(SUM(QtyLtr) as decimal(18,2)) as [QtyLtrR] from (select zzz.status,zzz.Complaint_no,zzz.Document_Code,zzz.sale_invoice_no,zzz.Item_Code,zzz.Cust_Code,zzz.Description,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  ( " &
                "  Select TSPL_SD_SHIPMENT_HEAD.status,TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_no,TSPL_SD_SHIPMENT_HEAD.sale_invoice_no,TSPL_SD_SHIPMENT_HEAD.Document_Code, Convert(varchar, TSPL_SD_SHIPMENT_HEAD.Document_Date, 103) As Document_Date, TSPL_SD_SHIPMENT_DETAIL.Item_Code As Item_Code , TSPL_ITEM_MASTER.Alies_Name As [Description] , TSPL_SD_SHIPMENT_DETAIL.Qty As Qty,(Case When TSPL_SD_SHIPMENT_DETAIL.Qty=0 Then 0 Else (TSPL_SD_SHIPMENT_DETAIL.Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor End) As QtyLtr ,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_code  " &
                  " From TSPL_CUSTOMER_COMPLAINT_HEAD " &
                  " Left Outer Join TSPL_SD_SHIPMENT_HEAD On  TSPL_SD_SHIPMENT_HEAD.customer_complaint_no = TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no  " &
                  " left outer join TSPL_SD_SHIPMENT_DETAIL    On TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code   Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code   " &
                  " Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code   left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                " where TSPL_SD_SHIPMENT_HEAD.IsReplacement=1  " + strWhrClause2 + " )zzz WHERE 1=1 group by zzz.Complaint_no,zzz.Document_Code,zzz.sale_invoice_no,zzz.Item_Code,zzz.Cust_Code,zzz.Description,zzz.status 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Complaint_no,zpivot.Document_Code,zpivot.sale_invoice_no,zpivot.Cust_Code,zpivot.status "

                    Dim StrComplaint = " select Complaint_no as [Complaint No],document_code as [Shipment No],sale_invoice_no as [Invoice No],Cust_Code as [Customer Code],0 as [Shipment Status], " + strSumItemOnly + " , " + strReplacementItem + ",cast(SUM(QtyLtr) as decimal(18,2)) as [QtyLtr] ,0 as [QtyLtrR] from (select zzz.Complaint_no,zzz.Document_Code,zzz.sale_invoice_no,zzz.Item_Code,zzz.Cust_Code,zzz.Description,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  ( " &
                " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_No,'' as sale_invoice_no,'' as Document_Code, Convert(varchar, TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Date, 103) As Document_Date, TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code As Item_Code , TSPL_ITEM_MASTER.Alies_Name As [Description] , TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Qty As Qty,(Case When TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Qty=0 Then 0 Else (TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor End) As QtyLtr ,TSPL_CUSTOMER_COMPLAINT_HEAD.Cust_Code From TSPL_CUSTOMER_COMPLAINT_DETAIL  " &
                " Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD On TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_No = TSPL_CUSTOMER_COMPLAINT_DETAIL.Complaint_No  " &
                " Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_CUSTOMER_COMPLAINT_HEAD.Cust_Code  " &
                " Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code  " &
                " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Uom =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                " where TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Qty>0  " + strWhrClause2 + " )zzz WHERE 1=1 group by zzz.Complaint_no,zzz.Document_Code,zzz.sale_invoice_no,zzz.Item_Code,zzz.Cust_Code,zzz.Description 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Complaint_no,zpivot.Document_Code,zpivot.sale_invoice_no,zpivot.Cust_Code "
                    query = " Select [Complaint No],max([Shipment No]) as [Shipment No],max([Invoice No]) as [Invoice No],[Customer Code] as [Booth],max([Shipment Status]) as [Shipment Status], " + strSumItemOnly + ",cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr], " + strSumItemReplacementOnly + ",cast(SUM(QtyLtrR) as decimal(18,2)) as [Total In Ltr(R)]  from (  " &
                "  " + StrReplacement + "  " &
                " Union All " &
                "  " + StrComplaint + " " &
                " ) xyzp  group by xyzp.[Complaint No],xyzp.[Customer Code]  order by xyzp.[Complaint No] "

                Else
                    Dim StrReplacement = " select Route_No as [Route No],Cust_Code as [Customer Code], " + strItem + " , " + strSumItemReplacementOnly2 + ",0 as [QtyLtr] ,cast(SUM(QtyLtr) as decimal(18,2)) as [QtyLtrR] from (select zzz.Item_Code,zzz.Route_No,zzz.Cust_Code,zzz.Description,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  ( " &
                "  Select TSPL_SD_SHIPMENT_HEAD.Document_Code, Convert(varchar, TSPL_SD_SHIPMENT_HEAD.Document_Date, 103) As Document_Date, TSPL_SD_SHIPMENT_DETAIL.Item_Code As Item_Code , TSPL_ITEM_MASTER.Alies_Name As [Description] , TSPL_SD_SHIPMENT_DETAIL.Qty As Qty,(Case When TSPL_SD_SHIPMENT_DETAIL.Qty=0 Then 0 Else (TSPL_SD_SHIPMENT_DETAIL.Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor End) As QtyLtr ,TSPL_SD_SHIPMENT_HEAD.Route_No,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_code  " &
                  " From TSPL_CUSTOMER_COMPLAINT_HEAD " &
                  " Left Outer Join TSPL_SD_SHIPMENT_HEAD On  TSPL_SD_SHIPMENT_HEAD.customer_complaint_no = TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no  " &
                  " left outer join TSPL_SD_SHIPMENT_DETAIL    On TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code   Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code   " &
                  " Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code   left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_SD_SHIPMENT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_SD_SHIPMENT_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                " where TSPL_SD_SHIPMENT_HEAD.IsReplacement=1  " + strWhrClause2 + " )zzz WHERE 1=1 group by zzz.Item_Code,zzz.Route_No,zzz.Cust_Code,zzz.Description 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Route_No,zpivot.Cust_Code "
                    'Dim StrComplaint = " select  [Route No],[Customer Code], " + strItem2 + " , " + strReplacementItem + " from (select zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.cust_category_code as [Customer Category Code],zzz.Zone_Code as [Zone] ,sum(qty) as qty from  (Select TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No,TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.cust_category_code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Qty as Qty, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc] From TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE Left Outer Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_DELIVERY_NOTE_DETAIL_FRESHSALE.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Lorry_No Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where 2=2  " + strWhrClause2 + " )zzz WHERE ZZZ.Scheme_Item='N' group by zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.Cust_Group_Code,zzz.cust_category_code,zzz.Zone_Code 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot  "
                    Dim StrComplaint = " select Route_No as [Route No],Cust_Code as [Customer Code], " + strSumItemOnly + " , " + strReplacementItem + ",cast(SUM(QtyLtr) as decimal(18,2)) as [QtyLtr] ,0 as [QtyLtrR] from (select zzz.Item_Code,zzz.Route_No,zzz.Cust_Code,zzz.Description,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  ( " &
                " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_No, Convert(varchar, TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Date, 103) As Document_Date, TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code As Item_Code , TSPL_ITEM_MASTER.Alies_Name As [Description] , TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Qty As Qty,(Case When TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Qty=0 Then 0 Else (TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor End) As QtyLtr ,TSPL_SD_SALE_INVOICE_HEAD.Route_No,TSPL_CUSTOMER_COMPLAINT_HEAD.Cust_Code From TSPL_CUSTOMER_COMPLAINT_DETAIL  " &
                " Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD On TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_No = TSPL_CUSTOMER_COMPLAINT_DETAIL.Complaint_No  " &
                " Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_CUSTOMER_COMPLAINT_HEAD.Cust_Code  " &
                " left outer join  TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.document_code=TSPL_CUSTOMER_COMPLAINT_HEAD.Invoice_No " &
                " Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code  " &
                " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_CUSTOMER_COMPLAINT_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Uom =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                " where TSPL_CUSTOMER_COMPLAINT_DETAIL.Damage_Qty>0  " + strWhrClause2 + " )zzz WHERE 1=1 group by zzz.Item_Code,zzz.Route_No,zzz.Cust_Code,zzz.Description 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.Route_No,zpivot.Cust_Code "
                    query = " Select [Route No],[Customer Code] as [Booth], " + strSumItemOnly + ",cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr], " + strSumItemReplacementOnly + ",cast(SUM(QtyLtrR) as decimal(18,2)) as [Total In Ltr(R)]  from (  " &
                "  " + StrReplacement + "  " &
                " Union All " &
                "  " + StrComplaint + " " &
                " ) xyzp  group by xyzp.[Route No],xyzp.[Customer Code]  order by xyzp.[Route No],xyzp.[Customer Code]  "

                End If


            End If


            If chkAbstract.Checked = False Then
                dtgv = clsDBFuncationality.GetDataTable(query)
            End If


            Gv1.DataSource = Nothing

                Gv1.Rows.Clear()
            Gv1.Columns.Clear()


            Gv1.DataSource = dtgv
                Gv1.BestFitColumns()

                If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
                End If

            RadPageView1.SelectedPage = RadPageViewPage2

            If chkAbstract.Checked = False Then
                Dim item As Integer = 0

                If chkMonthlyReplacement.Checked = True Then
                    item = 1
                ElseIf chkDocumentWise.Checked = True Then
                    item = 5
                Else
                    item = 2
                End If
                If Gv1.Rows.Count > 0 Then
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = item To Gv1.Columns.Count - 1
                        Dim aa = Gv1.Columns(i).HeaderText()
                        Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)


                    Next

                    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                End If
            End If

            'Pinned column 
            If chkMonthlyReplacement.Checked = True Then
                Gv1.Columns(0).IsPinned = True
            ElseIf chkDocumentWise.Checked = True Then
                Gv1.Columns(0).IsPinned = True
                Gv1.Columns(1).IsPinned = True
                Gv1.Columns(2).IsPinned = True
                Gv1.Columns(3).IsPinned = True
                Gv1.Columns(4).IsPinned = True
            Else
                Gv1.Columns(0).IsPinned = True
                Gv1.Columns(1).IsPinned = True
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rptDailyLeakageReplacementReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        ReportName = ""
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub



    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If chkMonthlyReplacement.Checked = True OrElse chkAbstract.Checked = True Then
                arrHeader.Add("Month-Year : " + clsCommon.myCstr(Month(fromDate.Value)) + " - " + clsCommon.myCstr(Year(fromDate.Value)) + "")
            Else
                arrHeader.Add("Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "")
            End If
            arrHeader.Add("Name : " & ReportName + "")
            transportSql.QuickExportToExcel(Gv1, "", ReportName, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If chkMonthlyReplacement.Checked = True OrElse chkAbstract.Checked = True Then
                arrHeader.Add("Month-Year : " + clsCommon.myCstr(Month(fromDate.Value)) + " - " + clsCommon.myCstr(Year(fromDate.Value)) + "")
            Else
                arrHeader.Add("Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "")
            End If
            arrHeader.Add("Name : " & ReportName + "")

            clsCommon.MyExportToPDF(ReportName, Gv1, arrHeader, ReportName, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class
