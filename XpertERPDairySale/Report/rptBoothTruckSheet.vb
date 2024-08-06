Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Imports System.Globalization
Imports System.Data.SqlClient

Imports System.Text.RegularExpressions
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net

Public Class rptBoothTruckSheet
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim isSchemeItem As Boolean = False
    Dim strItemNameOnly As String = ""
    Private Sub rptBoothTruckSheet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isSchemeItem = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowSchemeItemQty, clsFixedParameterCode.AllowSchemeItemQty, Nothing)) = 1, True, False)

        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
        cboDocumentType.SelectedIndex = 0
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PrintView(False)
    End Sub

    Sub PrintView(ByVal isPrint As Boolean)
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim strDate As String = "Document_Date"
            Dim strWhrClause2 As String = String.Empty

            '  strWhrClause2 = " and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'  "
            If cboDocumentType.SelectedIndex = 1 Then
                strWhrClause2 = " and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' and TSPL_BOOKING_MATSER.Posted = 1 "
            ElseIf cboDocumentType.SelectedIndex = 2 Then
                strWhrClause2 = " and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' and TSPL_BOOKING_MATSER.Posted = 0 "
            Else
                strWhrClause2 = " and convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_BOOKING_MATSER." + strDate + ",103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            End If

            'If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            '    strWhrClause2 += " and TSPL_BOOKING_DETAIL.Cust_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
            'End If

            Dim ItemInUse As String = ""
            ItemInUse = " TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where (TSPL_ITEM_MASTER.Alies_Name !='' or TSPL_ITEM_MASTER.Alies_Name is null) "
            If isSchemeItem = False Then
                ItemInUse += " and Scheme_Item='N' "
            End If
            ItemInUse += strWhrClause2
            ItemInUse += " order by Alies_Name "



            Dim strAliasCol As String = "( TSPL_ITEM_MASTER.Alies_Name )"

            Dim strAliasCol_Crate As String = ""
            Dim strAliasCol_Pouch As String = ""
            Dim strAliasCol_Ltr As String = ""
            Dim strAliasCol_Amount As String = ""

            If rdbEnglish.IsChecked = True Then
                strAliasCol = "( TSPL_ITEM_MASTER.Alies_Name )"
                strAliasCol_Crate = "( TSPL_ITEM_MASTER.Alies_Name+'_Crate' )"
                strAliasCol_Pouch = "( TSPL_ITEM_MASTER.Alies_Name+'_Pouch' )"
                strAliasCol_Ltr = "( TSPL_ITEM_MASTER.Alies_Name+'_Ltr' )"
                strAliasCol_Amount = "( TSPL_ITEM_MASTER.Alies_Name+'_Amount' )"
            Else
                strAliasCol = "( TSPL_ITEM_MASTER.Alies_Name_Hindi )"

                strAliasCol_Crate = "( TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Crate' )"
                strAliasCol_Pouch = "( TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Pouch' )"
                strAliasCol_Ltr = "( TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Ltr' )"
                strAliasCol_Amount = "( TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Amount' )"
            End If


            Dim strSchemeItem As String = Nothing
            strSchemeItem = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as ' + QUOTENAME( " + strAliasCol + "+'(S)') as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
            If String.IsNullOrEmpty(strSchemeItem) Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            Dim strItem As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as ' + QUOTENAME( " + strAliasCol_Crate + ") + ',' + '0 as ' + QUOTENAME( " + strAliasCol_Pouch + ") + ',' + '0 as ' + QUOTENAME( " + strAliasCol_Ltr + ") + ',' + '0 as ' + QUOTENAME( " + strAliasCol_Amount + ") as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strItem2 As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( " + strAliasCol_Crate + ") + ',' + QUOTENAME( " + strAliasCol_Pouch + ") + ',' + QUOTENAME( " + strAliasCol_Ltr + ") + ',' + QUOTENAME( " + strAliasCol_Amount + ") as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            strItemNameOnly = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( " + strAliasCol + ")   as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strItem2_Crate As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( " + strAliasCol_Crate + ") as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strItem2_Pouch As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( " + strAliasCol_Pouch + ") as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strItem2_Ltr As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( " + strAliasCol_Ltr + ") as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strItem2_Amount As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( " + strAliasCol_Amount + ") as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strItmeHeadingScheme As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +  QUOTENAME( " + strAliasCol_Crate + ") +' as ' + QUOTENAME( " + strAliasCol_Crate + "+'(S)') + ',' +  QUOTENAME( " + strAliasCol_Pouch + ") +' as ' + QUOTENAME( " + strAliasCol_Pouch + "+'(S)')  + ',' +  QUOTENAME( " + strAliasCol_Ltr + ") +' as ' + QUOTENAME( " + strAliasCol_Ltr + "+'(S)') +',' +  QUOTENAME( " + strAliasCol_Amount + ") +' as ' + QUOTENAME( " + strAliasCol_Amount + "+'(S)') as Alies_Name FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strSumItemOnly As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Crate + ") +',0))' +' as ' + QUOTENAME( " + strAliasCol_Crate + ") + ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Pouch + ") +',0))' +' as ' + QUOTENAME( " + strAliasCol_Pouch + ") + ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Ltr + ") +',0))' +' as ' + QUOTENAME( " + strAliasCol_Ltr + ") + ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Amount + ") +',0))' +' as ' + QUOTENAME( " + strAliasCol_Amount + ") as Alies_Name  FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strSumItemSchemeOnly As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Crate + "+'(S)') +',0))' +' as ' + QUOTENAME( " + strAliasCol_Crate + "+'(S)') + ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Pouch + "+'(S)') +',0))' +' as ' + QUOTENAME( " + strAliasCol_Pouch + "+'(S)') + ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Ltr + "+'(S)') +',0))' +' as ' + QUOTENAME( " + strAliasCol_Ltr + "+'(S)') + ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Amount + "+'(S)') +',0))' +' as ' + QUOTENAME( " + strAliasCol_Amount + "+'(S)')  as Alies_Name  FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strGrandTotal As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Crate + ") +',0))' +' + ' + +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Crate + "+'(S)') +',0))' + '+' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Pouch + ") +',0))' +' + ' + +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Pouch + "+'(S)') +',0))' + '+' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Ltr + ") +',0))' +' + ' + +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Ltr + "+'(S)') +',0))' + '+' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Amount + ") +',0))' +' + ' + +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Amount + "+'(S)') +',0))'   as Alies_Name  FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strGrandTotalWithoutScheme As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Crate + ") +',0))' + '+' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Pouch + ") +',0))' + '+' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Ltr + ") +',0))' + '+' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Amount + ") +',0))'      as Alies_Name  FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
            Dim strItem2WithSum As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Crate + ") +',0))' + ' as ' + QUOTENAME( " + strAliasCol_Crate + ") + ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Pouch + ") +',0))' + ' as ' + QUOTENAME( " + strAliasCol_Pouch + ") + ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Ltr + ") +',0))' + ' as ' + QUOTENAME( " + strAliasCol_Ltr + ") + ',' +'Sum(isnull(' + QUOTENAME( " + strAliasCol_Amount + ") +',0))' + ' as ' + QUOTENAME( " + strAliasCol_Amount + ") as Alies_Name FROM " + ItemInUse + "    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")

            Dim strCreateConv As String = ""
            If isSchemeItem = False Then
                strCreateConv = " TSPL_BOOKING_DETAIL.Booking_Qty "
            End If
            Dim qry As String = ""
            Dim strCustomerName As String = ""
            Dim strTotalRow As String = ""
            Dim strTotalLtrRow As String = ""
            Dim strTotalAmountRow As String = ""

            If rdbEnglish.IsChecked = True Then
                strCustomerName = " TSPL_CUSTOMER_MASTER.Customer_Name  "
                strTotalRow = "Total"
                strTotalLtrRow = "Total Ltr"
                strTotalAmountRow = "Total Amount"
            Else
                strCustomerName = " TSPL_CUSTOMER_MASTER.Customer_Name_Hindi  "
                strTotalRow = "कुल"
                strTotalLtrRow = "कुल लीटर"
                strTotalAmountRow = "कुल राशि"
            End If

            Dim UnionQry As String = Nothing
            Dim Distributor As String = Nothing
            Dim DistributorSubQry As String = Nothing
            Dim DistributorQry As String = Nothing
            Dim DistributorJoin As String = Nothing
            'If chkDistributor.Checked Then
            '    Distributor = " Distributor.[Distributor Code],Distributor.[Distributor Name],"
            '    DistributorQry = " [Distributor Code],Max([Distributor Name])[Distributor Name],max([route no])[route no], "
            '    DistributorSubQry = " Max([Distributor Code]) AS [Distributor Code],Max([Distributor Name])[Distributor Name],"
            'DistributorJoin = " Left Outer Join (Select Cust_Code As [Distributor Code],Customer_Name As [Distributor Name],Cust_Group_Code from TSPL_CUSTOMER_MASTER) As Distributor ON Distributor.[Distributor Code]=TSPL_CUSTOMER_MASTER.Distributor_Code "
            'Else
            'Distributor = " Distributor.[Distributor Code],Distributor.[Distributor Name],"
            'DistributorSubQry = " Max([Distributor Code]) AS [Distributor Code],Max([Distributor Name])[Distributor Name],"
            DistributorQry = " [Booth Code],[WdName] as [Booth Name],max([route no])[route no], "
            'UnionQry = " '' As [Distributor Code],'' As [Distributor Name], "
            DistributorJoin = " Left Outer Join (Select Cust_Group_Code from TSPL_CUSTOMER_MASTER) As Distributor ON Distributor.[Distributor Code]=TSPL_CUSTOMER_MASTER.Distributor_Code "
            'End If
            Dim whr As String = ""
            Dim whr1 As String = ""
            Dim whr2 As String = ""
            Dim whr3 As String = ""
            If rbtnMilk.Checked Then
                whr = "  and ThisTableHead.Is_Taxable=0 "
                whr1 = " and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0 "
            ElseIf rbtnproduct.Checked Then
                whr = "  and ThisTableHead.Is_Taxable=1 "
                whr1 = " and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1 "
            Else
                whr = "  "
                whr1 = "  "
            End If

            qry = " select  " + DistributorQry + " " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from ( select * from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Booth Code]) as  [Booth Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,max(zzz.Description_Creat) as Description_Creat,max(zzz.Description_Pouch) as Description_Pouch,max(zzz.Description_Ltr) as Description_Ltr, max(zzz.Description_Amount) as Description_Amount
                                          , sum (Qty_Create) as Qty_Create,sum (Qty_Ltr) as Qty_Ltr,sum (Qty_Pouch) as Qty_Pouch,sum(Amount_Ltr) as Amount_Ltr,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], " + Distributor + " TSPL_BOOKING_DETAIL.Cust_Code As [Booth Code], " + strCustomerName + " As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , "
            If rdbEnglish.IsChecked = True Then
                qry += " TSPL_ITEM_MASTER.Alies_Name As [Description] ,
                                            TSPL_ITEM_MASTER.Alies_Name+'_Crate' As [Description_Creat]
                                           ,TSPL_ITEM_MASTER.Alies_Name+'_Pouch' As [Description_Pouch]
                                            ,TSPL_ITEM_MASTER.Alies_Name+'_Ltr' As [Description_Ltr]
                                            ,TSPL_ITEM_MASTER.Alies_Name+'_Amount' As [Description_Amount] "
            Else
                qry += " TSPL_ITEM_MASTER.Alies_Name As [Description] ,
                                            TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Crate' As [Description_Creat]
                                           ,TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Pouch' As [Description_Pouch]
                                            ,TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Ltr' As [Description_Ltr]
                                            ,TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Amount' As [Description_Amount] "
            End If


            qry += ",TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], " + strCreateConv + " as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr , convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILCREATE.Conversion_Factor,0)) as Qty_Create
                                          , convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor,0)) as Qty_Ltr
                                          , convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILPOUCH.Conversion_Factor,0)) as Qty_Pouch
                                          , Amount_with_Tax as Amount_Ltr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code Left Outer Join (Select Cust_Code As [Distributor Code],Customer_Name As [Distributor Name],Cust_Group_Code from TSPL_CUSTOMER_MASTER) As Distributor ON Distributor.[Distributor Code]=TSPL_CUSTOMER_MASTER.Distributor_Code " &
                                  " Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 " & whr1 & "  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  " ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 " & whr1 & "  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  "  ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                                  " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No  " &
                                  " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'  join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILPOUCH on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILPOUCH.Item_Code and TSPL_ITEM_UOM_DETAILPOUCH.UOM_Code='Pouch'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                  "  where 2=2   " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] 	) as s 
                                      pivot (  sum(Qty_Create) for Description_Creat in ( " + strItem2_Crate + " ) ) as zpivot 
                                      pivot (  sum(Qty_Pouch) for Description_Pouch in ( " + strItem2_Pouch + " ) ) as zpivot2
                                      pivot (  sum(Qty_Ltr) for Description_Ltr in ( " + strItem2_Ltr + " ) ) as zpivot3
                                      pivot (  sum(Amount_Ltr) for Description_Amount in ( " + strItem2_Amount + " ) ) as zpivot4
                                      ) XXXFinal"
            'If chkDistributor.Checked Then
            '    qry += " group by  XXXFinal.[Distributor Code]	"
            'Else
            qry += " group by  XXXFinal.[Booth Code], XXXFinal.WdName" ',XXXFinal.[Distributor Code] 

            ' XXXFinal.Document_No,XXXFinal.[VEHICLE NO],XXXFinal.[WdName],XXXFinal.[Group],XXXFinal.[Cust Group Desc],XXXFinal.[Customer Category Code],XXXFinal.[Zone],XXXFinal.[Route No],XXXFinal.[DO NO],XXXFinal.[Short Close]
            'End If

            qry = qry + " Union All 
                          select '' as  [Booth Code],N'" + strTotalRow + "' as [Customer Name],max([route no])[route no], " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from ( select * from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Booth Code]) as  [Booth Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,max(zzz.Description_Creat) as Description_Creat,max(zzz.Description_Pouch) as Description_Pouch,max(zzz.Description_Ltr) as Description_Ltr, max(zzz.Description_Amount) as Description_Amount
                                          , sum (Qty_Create) as Qty_Create,sum (Qty_Ltr) as Qty_Ltr,sum (Qty_Pouch) as Qty_Pouch,sum(Amount_Ltr) as Amount_Ltr,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Booth Code], " + strCustomerName + " As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , "
            If rdbEnglish.IsChecked = True Then
                qry += " TSPL_ITEM_MASTER.Alies_Name As [Description] ,
                                            TSPL_ITEM_MASTER.Alies_Name+'_Crate' As [Description_Creat]
                                          ,TSPL_ITEM_MASTER.Alies_Name+'_Pouch' As [Description_Pouch]
                                          ,TSPL_ITEM_MASTER.Alies_Name+'_Ltr' As [Description_Ltr]
                                          ,TSPL_ITEM_MASTER.Alies_Name+'_Amount' As [Description_Amount], "
            Else
                qry += " TSPL_ITEM_MASTER.Alies_Name_Hindi As [Description] ,
                                            TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Crate' As [Description_Creat]
                                          ,TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Pouch' As [Description_Pouch]
                                          ,TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Ltr' As [Description_Ltr]
                                          ,TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Amount' As [Description_Amount], "
            End If


            qry += " TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], " + strCreateConv + " as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr , convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILCREATE.Conversion_Factor,0)) as Qty_Create
                                          , convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor,0)) as Qty_Ltr
                                          , convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILPOUCH.Conversion_Factor,0)) as Qty_Pouch
                                          , Amount_with_Tax as Amount_Ltr  From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                  " Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 " & whr1 & "  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  " ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 " & whr1 & "  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  "  ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                                  " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No  " &
                                  " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'  join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILPOUCH on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILPOUCH.Item_Code and TSPL_ITEM_UOM_DETAILPOUCH.UOM_Code='Pouch'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                  "  where 2=2   " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] 	) as s 
                                      pivot (  sum(Qty_Create) for Description_Creat in ( " + strItem2_Crate + " ) ) as zpivot 
                                      pivot (  sum(Qty_Pouch) for Description_Pouch in ( " + strItem2_Pouch + " ) ) as zpivot2
                                      pivot (  sum(Qty_Ltr) for Description_Ltr in ( " + strItem2_Ltr + " ) ) as zpivot3
                                      pivot (  sum(Amount_Ltr) for Description_Amount in ( " + strItem2_Amount + " ) ) as zpivot4
                                      ) XXXFinal

                                      "
            qry = qry + " Union All 
                          select '' as  [Booth Code],N'" + strTotalLtrRow + "' as [Customer Name],max([route no])[route no], " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from ( select * from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Booth Code]) as  [Booth Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,max(zzz.Description_Creat) as Description_Creat,max(zzz.Description_Pouch) as Description_Pouch,max(zzz.Description_Ltr) as Description_Ltr, max(zzz.Description_Amount) as Description_Amount
                                          , sum (Qty_Create) as Qty_Create,sum (Qty_Ltr) as Qty_Ltr,sum (Qty_Pouch) as Qty_Pouch,sum(Amount_Ltr) as Amount_Ltr,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Booth Code], " + strCustomerName + " As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , "
            If rdbEnglish.IsChecked = True Then
                qry += " TSPL_ITEM_MASTER.Alies_Name As [Description] ,
                                            TSPL_ITEM_MASTER.Alies_Name+'_Crate' As [Description_Creat]
                                          ,TSPL_ITEM_MASTER.Alies_Name+'_Pouch' As [Description_Pouch]
                                          ,TSPL_ITEM_MASTER.Alies_Name+'_Ltr' As [Description_Ltr]
                                          ,TSPL_ITEM_MASTER.Alies_Name+'_Amount' As [Description_Amount], "
            Else
                qry += " TSPL_ITEM_MASTER.Alies_Name_Hindi As [Description] ,
                                            TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Crate' As [Description_Creat]
                                          ,TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Pouch' As [Description_Pouch]
                                          ,TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Ltr' As [Description_Ltr]
                                          ,TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Amount' As [Description_Amount], "
            End If


            qry += " TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], " + strCreateConv + " as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr , convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILCREATE.Conversion_Factor,0)) * 0 as Qty_Create
                                          , convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor,0)) as Qty_Ltr
                                          , convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILPOUCH.Conversion_Factor,0)) * 0 as Qty_Pouch
                                          , Amount_with_Tax * 0 as Amount_Ltr  From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                  " Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  " & whr & "  and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 " & whr1 & "  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  " ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 " & whr1 & "  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  "  ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                                  " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No  " &
                                  " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'  join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILPOUCH on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILPOUCH.Item_Code and TSPL_ITEM_UOM_DETAILPOUCH.UOM_Code='Pouch'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                  "  where 2=2   " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] 	) as s 
                                      pivot (  sum(Qty_Ltr ) for Description_Creat in ( " + strItem2_Crate + " ) ) as zpivot 
                                      pivot (  sum(Qty_Pouch) for Description_Pouch in ( " + strItem2_Pouch + " ) ) as zpivot2
                                      pivot (  sum(Qty_Create) for Description_Ltr in ( " + strItem2_Ltr + " ) ) as zpivot3
                                      pivot (  sum(Amount_Ltr) for Description_Amount in ( " + strItem2_Amount + " ) ) as zpivot4
                                      ) XXXFinal

                                      "
            qry = qry + " Union All 
                          select '' as  [Booth Code],N'" + strTotalAmountRow + "' as [Customer Name],max([route no])[route no], " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from ( select * from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Booth Code]) as  [Booth Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,max(zzz.Description_Creat) as Description_Creat,max(zzz.Description_Pouch) as Description_Pouch,max(zzz.Description_Ltr) as Description_Ltr, max(zzz.Description_Amount) as Description_Amount
                                          , sum (Qty_Create) as Qty_Create,sum (Qty_Ltr) as Qty_Ltr,sum (Qty_Pouch) as Qty_Pouch,sum(Amount_Ltr) as Amount_Ltr,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Booth Code], " + strCustomerName + " As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , "
            If rdbEnglish.IsChecked = True Then
                qry += " TSPL_ITEM_MASTER.Alies_Name As [Description] ,
                                            TSPL_ITEM_MASTER.Alies_Name+'_Crate' As [Description_Creat]
                                          ,TSPL_ITEM_MASTER.Alies_Name+'_Pouch' As [Description_Pouch]
                                          ,TSPL_ITEM_MASTER.Alies_Name+'_Ltr' As [Description_Ltr]
                                          ,TSPL_ITEM_MASTER.Alies_Name+'_Amount' As [Description_Amount], "
            Else
                qry += " TSPL_ITEM_MASTER.Alies_Name_Hindi As [Description] ,
                                            TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Crate' As [Description_Creat]
                                          ,TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Pouch' As [Description_Pouch]
                                          ,TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Ltr' As [Description_Ltr]
                                          ,TSPL_ITEM_MASTER.Alies_Name_Hindi+'_Amount' As [Description_Amount], "

            End If

            qry += " TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO], " + strCreateConv + " as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr , convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILCREATE.Conversion_Factor,0)) * 0 as Qty_Create
                                          , convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILLTR.Conversion_Factor,0)) * 0 as Qty_Ltr
                                          , convert (decimal(18,2), TSPL_BOOKING_DETAIL.Booking_Qty * TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor / nullif ( TSPL_ITEM_UOM_DETAILPOUCH.Conversion_Factor,0)) * 0 as Qty_Pouch
                                          , Amount_with_Tax as Amount_Ltr  From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code " &
                                  " Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 " & whr1 & "  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  " ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No " & whr & " and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 " & whr1 & "  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No " &
                                  "  ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No " &
                                  "  left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where  " &
                                  " Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No  " &
                                  " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'  join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILPOUCH on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILPOUCH.Item_Code and TSPL_ITEM_UOM_DETAILPOUCH.UOM_Code='Pouch'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code " &
                                  "  where 2=2   " + strWhrClause2 + " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] 	) as s 
                                      pivot (  sum(Amount_Ltr) for Description_Creat in ( " + strItem2_Crate + " ) ) as zpivot 
                                      pivot (  sum(Qty_Pouch) for Description_Pouch in ( " + strItem2_Pouch + " ) ) as zpivot2
                                      pivot (  sum(Qty_Ltr) for Description_Ltr in ( " + strItem2_Ltr + " ) ) as zpivot3
                                      pivot (  sum(Qty_Create) for Description_Amount in ( " + strItem2_Amount + " ) ) as zpivot4
                                      ) XXXFinal"



            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                If isPrint Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDairySaleGatePass11", "Booth Truck Sheet", "", "rptCompanyAddress.rpt")
                    frmCRV = Nothing
                Else
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
                    Gv1.BestFitColumns()
                    Gv1.EnableFiltering = False
                    Gv1.EnableGrouping = False
                    'DemandReportFormat()
                    'Gv1.Columns("Qty in Stocking UOM").FormatString = "{0:n2}"

                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    'Dim itemQty As New GridViewSummaryItem("Qty in Stocking UOM", "{0:F2}", GridAggregateFunction.Sum)
                    'summaryRowItem.Add(itemQty)
                    'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

                    'Dim summaryItem5 As New GridViewSummaryItem()
                    'summaryItem5.FormatString = "{0:F2}"
                    'summaryItem5.Name = "DTM 200 Ml_Crate"
                    'summaryItem5.AggregateExpression = "sum(DTM 200 Ml_Crate)"
                    'summaryRowItem.Add(summaryItem5)
                    'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    'gv1.MasterTemplate.ShowTotals = True

                    Gv1.Rows(Gv1.Rows.Count - 3).IsPinned = True
                    Gv1.Rows(Gv1.Rows.Count - 2).IsPinned = True
                    Gv1.Rows(Gv1.Rows.Count - 1).IsPinned = True

                    Gv1.Rows(Gv1.Rows.Count - 3).PinPosition = PinnedRowPosition.Bottom
                    Gv1.Rows(Gv1.Rows.Count - 2).PinPosition = PinnedRowPosition.Bottom
                    Gv1.Rows(Gv1.Rows.Count - 1).PinPosition = PinnedRowPosition.Bottom
                    View()
                    Gv1.BestFitColumns()
                    ReStoreGridLayout()
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Public Sub funPrint(ByVal strDocNo As String)
    '    Try
    '        Dim qry As Boolean
    '        qry = PrintView()
    '        'Dim Qry2 As String = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin  ,Main_Final.* from ( " + Qry + " WHERE TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No='" + strDocNo + "') as Main_Final " &
    '        '" LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Main_Final.Comp_Code "
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '        If dt.Rows.Count > 0 Then
    '            ' frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptGatePassDairySale", "Retail Invoice", clsCommon.myCDate(dt.Rows(0)("Document_Date")), "rptCompanyAddress.rpt")
    '            Dim frmCRV As New frmCrystalReportViewer()
    '            frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDairySaleGatePass", "Dairy Gate Pass", clsCommon.myCDate(dt.Rows(0)("GatePass_Date")), "rptCompanyAddress.rpt")
    '            frmCRV = Nothing
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub View()

        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(" "))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())


            'view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Distributor Code").Name)
            '    view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Distributor Name").Name)
            '    If rdbHindi.IsChecked = True Then
            '        Gv1.Columns("Distributor Code").HeaderText = "वितरणकर्ता का कोड"
            '        Gv1.Columns("Distributor Name").HeaderText = "वितरणकर्ता का नाम"
            '    End If

            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Booth Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Booth Name").Name)
            If rdbHindi.IsChecked = True Then
                Gv1.Columns("Booth Code").HeaderText = "ग्राहक का कोड"
                Gv1.Columns("Booth Name").HeaderText = "ग्राहक का नाम"
            End If



            Dim dblGroupNo As Integer = 0
            Dim str As String
            Dim strArr() As String
            Dim count As Integer
            str = strItemNameOnly
            strArr = str.Split(",")
            For count = 0 To strArr.Length - 1
                'MsgBox(strArr(count))
                Dim strName As String = strArr(count).Replace("[", " ").Trim()
                strName = strName.Replace("]", " ").Trim()
                dblGroupNo = dblGroupNo + 1
                view.ColumnGroups.Add(New GridViewColumnGroup(strName))
                view.ColumnGroups(dblGroupNo).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strName + "_Crate").Name)
                If rdbEnglish.IsChecked = True Then
                    Gv1.Columns(strName + "_Crate").HeaderText = "Crate"
                Else
                    Gv1.Columns(strName + "_Crate").HeaderText = "कैरेट"
                End If

                view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strName + "_Pouch").Name)
                If rdbEnglish.IsChecked = True Then
                    Gv1.Columns(strName + "_Pouch").HeaderText = "Pouch"
                Else
                    Gv1.Columns(strName + "_Pouch").HeaderText = "थैली"
                End If

                view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strName + "_Ltr").Name)
                Gv1.Columns(strName + "_Ltr").IsVisible = False
                view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strName + "_Amount").Name)
                Gv1.Columns(strName + "_Amount").IsVisible = False
            Next


            'For i As Integer = 0 To Gv1.ColumnNames.Count - 1 Step 4

            '    Dim aa As String = Gv1.Columns(i + 1).HeaderText()
            '    Dim aaa As String = aa
            '    Dim strItemName As String = ""
            '    If aa.Contains("_Crate") = True Then
            '        strItemName = clsCommon.myCstr(Replace(aaa, "_Crate", " "))
            '    ElseIf aa.Contains("_Pouch") = True Then
            '        strItemName = clsCommon.myCstr(Replace(aaa, "_Pouch", " "))
            '    ElseIf aa.Contains("_Ltr") = True Then
            '        strItemName = clsCommon.myCstr(Replace(aaa, "_Ltr", " "))
            '    ElseIf aa.Contains("_Amount") = True Then
            '        strItemName = clsCommon.myCstr(Replace(aaa, "_Amount", " "))
            '    End If
            '    If clsCommon.myLen(strItemName) > 0 Then
            '        dblGroupNo = dblGroupNo + 1
            '        view.ColumnGroups.Add(New GridViewColumnGroup(strItemName))
            '        view.ColumnGroups(dblGroupNo).Rows.Add(New GridViewColumnGroupRow())
            '        view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strItemName + "_Crate"))
            '        view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strItemName + "_Pouch"))
            '        view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strItemName + "_Ltr"))
            '        view.ColumnGroups(dblGroupNo).Rows(0).ColumnNames.Add(Gv1.Columns(strItemName + "_Amount"))
            '    End If
            'Next
            If rdbEnglish.IsChecked = True Then
                view.ColumnGroups.Add(New GridViewColumnGroup("Total"))
            Else
                view.ColumnGroups.Add(New GridViewColumnGroup("कुल"))
            End If

            view.ColumnGroups(dblGroupNo + 1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(dblGroupNo + 1).Rows(0).ColumnNames.Add(Gv1.Columns("Grand Total").Name)
            If rdbEnglish.IsChecked = True Then
                Gv1.Columns("Grand Total").HeaderText = "Grand Total"
            Else
                Gv1.Columns("Grand Total").HeaderText = "कुल योग"
            End If
            view.ColumnGroups(dblGroupNo + 1).Rows(0).ColumnNames.Add(Gv1.Columns("Total In Ltr").Name)
            If rdbEnglish.IsChecked = True Then
                Gv1.Columns("Total In Ltr").HeaderText = "Total In Ltr"
            Else
                Gv1.Columns("Total In Ltr").HeaderText = "कुल लीटर में"
            End If
            Gv1.ViewDefinition = view
        End If
    End Sub

    Private Sub txtRouteCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteCode._MYValidating
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER"
        txtRouteCode.Value = clsCommon.ShowSelectForm("Demand@Print@DayshiftWise", qry, "Code", "", txtRouteCode.Value, "Code", isButtonClicked)
        lblRouteCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc as Name from TSPL_ROUTE_MASTER where Route_No ='" + txtRouteCode.Value + "' "))
    End Sub
    Sub Reset()
        cboDocumentType.SelectedIndex = 0
        txtRouteCode.Value = ""
        lblRouteCode.Text = ""

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PrintView(True)
    End Sub

    'Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
    '    Try

    '        PrintView()
    '        'Dim Qry2 As String = "select TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.Pincode as Comp_Pin  ,Main_Final.* from ( " + Qry + " WHERE TSPL_GATEPASS_DETAIL_DAIRYSALE.Document_No='" + strDocNo + "') as Main_Final " &
    '        '" LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Main_Final.Comp_Code "

    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '        If dt.Rows.Count > 0 Then
    '            ' frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptGatePassDairySale", "Retail Invoice", clsCommon.myCDate(dt.Rows(0)("Document_Date")), "rptCompanyAddress.rpt")
    '            Dim frmCRV As New frmCrystalReportViewer()
    '            frmCRV.funsubreportWithdt(CrystalReportFolder.NewSalesReports, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDairySaleGatePass", "Dairy Gate Pass", clsCommon.myCDate(dt.Rows(0)("GatePass_Date")), "rptCompanyAddress.rpt")
    '            frmCRV = Nothing
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try

    'End Sub
End Class