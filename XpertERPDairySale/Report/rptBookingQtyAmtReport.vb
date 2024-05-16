Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO


Public Class rptBookingQtyAmtReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim isSchemeItem As Boolean = False
    Dim strItemNameOnly As String = ""
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        isSchemeItem = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowSchemeItemQty, clsFixedParameterCode.AllowSchemeItemQty, Nothing)) = 1, True, False)
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
        cboDocumentType.SelectedIndex = 0
        'fromDate.Value = ToDate.Value.AddMonths(-1)
    End Sub
    Sub Reset()
        cboDocumentType.SelectedIndex = 0
        txtItem.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        txtItemType.arrValueMember = Nothing
        txtRouteCode.Value = ""
        lblRouteCode.Text = ""

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub PrintView(ByVal printnotpad As Boolean)
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

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                strWhrClause2 += " and TSPL_BOOKING_DETAIL.Cust_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
            End If

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
            If chkDistributor.Checked Then
                Distributor = " Distributor.[Distributor Code],Distributor.[Distributor Name],"
                DistributorQry = " [Distributor Code],Max([Distributor Name])[Distributor Name],max([route no])[route no], "
                DistributorSubQry = " Max([Distributor Code]) AS [Distributor Code],Max([Distributor Name])[Distributor Name],"
                'DistributorJoin = " Left Outer Join (Select Cust_Code As [Distributor Code],Customer_Name As [Distributor Name],Cust_Group_Code from TSPL_CUSTOMER_MASTER) As Distributor ON Distributor.[Distributor Code]=TSPL_CUSTOMER_MASTER.Distributor_Code "
            Else
                Distributor = " Distributor.[Distributor Code],Distributor.[Distributor Name],"
                DistributorSubQry = " Max([Distributor Code]) AS [Distributor Code],Max([Distributor Name])[Distributor Name],"
                DistributorQry = " [Distributor Code],Max([Distributor Name])[Distributor Name],[Customer Code],[WdName] as [Customer Name],max([route no])[route no], "
                UnionQry = " '' As [Distributor Code],'' As [Distributor Name], "
                'DistributorJoin = " Left Outer Join (Select Cust_Code As [Distributor Code],Customer_Name As [Distributor Name],Cust_Group_Code from TSPL_CUSTOMER_MASTER) As Distributor ON Distributor.[Distributor Code]=TSPL_CUSTOMER_MASTER.Distributor_Code "
            End If
            Dim whr As String = ""
            Dim whr1 As String = ""
            Dim whr2 As String = ""
            Dim whr3 As String = ""
            If rbtnMilk.Checked Then
                whr = "  and ThisTableHead.Is_Taxable=0 "
                whr1 = " and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0 "
            ElseIf rbtnProduct.Checked Then
                whr = "  and ThisTableHead.Is_Taxable=1 "
                whr1 = " and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1 "
            Else
                whr = "  "
                whr1 = "  "
            End If

            qry = " select  " + DistributorQry + " " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from ( select * from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code]," + DistributorSubQry + " max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,max(zzz.Description_Creat) as Description_Creat,max(zzz.Description_Pouch) as Description_Pouch,max(zzz.Description_Ltr) as Description_Ltr, max(zzz.Description_Amount) as Description_Amount
                                          , sum (Qty_Create) as Qty_Create,sum (Qty_Ltr) as Qty_Ltr,sum (Qty_Pouch) as Qty_Pouch,sum(Amount_Ltr) as Amount_Ltr,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], " + Distributor + " TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], " + strCustomerName + " As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , "
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
            If chkDistributor.Checked Then
                qry += " group by  XXXFinal.[Distributor Code]	"
            Else
                qry += " group by  XXXFinal.[Customer Code], XXXFinal.WdName,XXXFinal.[Distributor Code] " ' XXXFinal.Document_No,XXXFinal.[VEHICLE NO],XXXFinal.[WdName],XXXFinal.[Group],XXXFinal.[Cust Group Desc],XXXFinal.[Customer Category Code],XXXFinal.[Zone],XXXFinal.[Route No],XXXFinal.[DO NO],XXXFinal.[Short Close]
            End If

            qry = qry + " Union All 
                          select " + UnionQry + "'' as  [Customer Code],N'" + strTotalRow + "' as [Customer Name],max([route no])[route no], " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from ( select * from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,max(zzz.Description_Creat) as Description_Creat,max(zzz.Description_Pouch) as Description_Pouch,max(zzz.Description_Ltr) as Description_Ltr, max(zzz.Description_Amount) as Description_Amount
                                          , sum (Qty_Create) as Qty_Create,sum (Qty_Ltr) as Qty_Ltr,sum (Qty_Pouch) as Qty_Pouch,sum(Amount_Ltr) as Amount_Ltr,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], " + strCustomerName + " As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , "
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
                          select " + UnionQry + "'' as  [Customer Code],N'" + strTotalLtrRow + "' as [Customer Name],max([route no])[route no], " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from ( select * from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,max(zzz.Description_Creat) as Description_Creat,max(zzz.Description_Pouch) as Description_Pouch,max(zzz.Description_Ltr) as Description_Ltr, max(zzz.Description_Amount) as Description_Amount
                                          , sum (Qty_Create) as Qty_Create,sum (Qty_Ltr) as Qty_Ltr,sum (Qty_Pouch) as Qty_Pouch,sum(Amount_Ltr) as Amount_Ltr,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], " + strCustomerName + " As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , "
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
                          select " + UnionQry + "'' as  [Customer Code],N'" + strTotalAmountRow + "' as [Customer Name],max([route no])[route no], " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr] from ( select * from (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date, zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,max(zzz.Description_Creat) as Description_Creat,max(zzz.Description_Pouch) as Description_Pouch,max(zzz.Description_Ltr) as Description_Ltr, max(zzz.Description_Amount) as Description_Amount
                                          , sum (Qty_Create) as Qty_Create,sum (Qty_Ltr) as Qty_Ltr,sum (Qty_Pouch) as Qty_Pouch,sum(Amount_Ltr) as Amount_Ltr,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date,TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], " + strCustomerName + " As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , "
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
            If printnotpad = True Then
                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = objCommonVar.CurrentCompanyName
                obj.ShowPageNo = True
                obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("From Date ", clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy")))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("To Date ", clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Route ", clsCommon.myCstr(txtRouteCode.Value)))




                obj.arrColumn = New List(Of clsDosPrintColumn)()
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Route_Desc", "ROUTE", True, DosPrintAlignment.Left, 7, False, DecimalPlaces.NA))
                ' obj.arrColumn.Add(clsDosPrintColumn.SetColumn(strItem, "Item", True, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn(strItem2_Crate, " ", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Zero))
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn(strItem2_Pouch, " ", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Zero))
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn(strItem2_Ltr, " ", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Zero))
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn(strItem2_Amount, " ", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Zero))
                ''obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total", "Total", False, DosPrintAlignment.Right, 12, True, DecimalPlaces.Zero))

                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CrateQty_New", "CRATES", False, DosPrintAlignment.Right, 9, True, DecimalPlaces.Zero))
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Desh", "", False, DosPrintAlignment.Right, 3, True, DecimalPlaces.NA))
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("PendingPcsQty_New", "   ", False, DosPrintAlignment.Left, 5, True, DecimalPlaces.Zero))
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("LtrQty", "LITRES/KG", False, DosPrintAlignment.Right, 14, True, DecimalPlaces.Two))
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Amount_with_Tax", "AMOUNT", False, DosPrintAlignment.Right, 14, True, DecimalPlaces.Two))

                ''If clsCommon.CompairString(cboABSReportType.SelectedValue, "Total Abstract") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboABSReportType.SelectedValue, "Milk Abstract") = CompairStringResult.Equal OrElse clsCommon.CompairString(cboABSReportType.SelectedValue, "Product Abstract") = CompairStringResult.Equal Then
                ''    Dim strLRQty As Double = 0
                ''    Dim strPQQty As Double = 0
                ''    Dim strESQty As Double = 0
                ''    Dim strLRPQES_TotalQty As Double = 0


                ''    qry = " Select   1 as ID, isnull (sum(LR),0) as LR , isnull(sum (PQ),0) as PQ , isnull (Sum ( ES ),0) as ES ,isnull (sum(LR),0) + isnull(sum (PQ),0) + isnull (Sum ( ES ),0) as  Total_LR_PQ_ES_Qty from  " &
                ''          " ( " &
                ''          " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Code, convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 'LTR' as  Unit_code ,isnull( Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty),0) as LR , 0 as PQ , 0 as ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='LTR'  Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD on TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no = TSPL_SD_SHIPMENT_HEAD.customer_complaint_no left outer join TSPL_CUSTOMER_COMPLAINT_detail on TSPL_CUSTOMER_COMPLAINT_detail.Complaint_no=TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_no and  TSPL_CUSTOMER_COMPLAINT_detail.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code  where   Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(txtABSDate.Value, "dd/MMM/yyyy") + "',103) and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(txtABSDate.Value, "dd/MMM/yyyy") + "',103)    and TSPL_SD_SHIPMENT_HEAD.status = 1     and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1 and TSPL_CUSTOMER_COMPLAINT_detail.Complaint_Code = 'LR' " + whr_LR_PQ_ES + " " &
                ''          " Union All " &
                ''          " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Code, convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 'LTR' as  Unit_code ,0 as LR , isnull( Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty),0) as PQ , 0 as ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='LTR'  Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD on TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no = TSPL_SD_SHIPMENT_HEAD.customer_complaint_no left outer join TSPL_CUSTOMER_COMPLAINT_detail on TSPL_CUSTOMER_COMPLAINT_detail.Complaint_no=TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_no and  TSPL_CUSTOMER_COMPLAINT_detail.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code  where   Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(txtABSDate.Value, "dd/MMM/yyyy") + "',103) and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(txtABSDate.Value, "dd/MMM/yyyy") + "',103)    and TSPL_SD_SHIPMENT_HEAD.status = 1     and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1 and TSPL_CUSTOMER_COMPLAINT_detail.Complaint_Code = 'PQ' " + whr_LR_PQ_ES + " " &
                ''          " Union All " &
                ''          " Select TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_Code, convert (varchar,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code as Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SHIPMENT_HEAD.Route_No ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Zone_Code ,TSPL_Zone_Master.Description as Zone_Name , TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Alies_Name  , 'LTR' as  Unit_code ,0 as LR , 0 as PQ , isnull( Convert (decimal(18,2), ( isnull( Source_Conv.Conversion_Factor,0) / nullif ( isnull(Target_Conv.Conversion_Factor,0),0) ) *  TSPL_SD_SHIPMENT_DETAIL.Qty),0) as ES from TSPL_SD_SHIPMENT_DETAIL Left  Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.Document_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SHIPMENT_HEAD.Customer_Code left Outer Join TSPL_ROUTE_MASTER on TSPL_SD_SHIPMENT_HEAD.Route_No=TSPL_ROUTE_MASTER.Route_No   left Outer Join TSPL_Zone_Master on TSPL_Zone_Master.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code left Outer Join TSPL_ITEM_MASTER on   TSPL_ITEM_MASTER.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code left Outer Join TSPL_ITEM_UOM_DETAIL as Source_Conv on Source_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Source_Conv.UOM_Code = TSPL_SD_SHIPMENT_DETAIL.Unit_Code left Outer join TSPL_ITEM_UOM_DETAIL as Target_Conv on Target_Conv.Item_Code = TSPL_SD_SHIPMENT_DETAIL.Item_Code and Target_Conv.UOM_Code ='LTR'  Left Outer Join TSPL_CUSTOMER_COMPLAINT_HEAD on TSPL_CUSTOMER_COMPLAINT_HEAD.complaint_no = TSPL_SD_SHIPMENT_HEAD.customer_complaint_no left outer join TSPL_CUSTOMER_COMPLAINT_detail on TSPL_CUSTOMER_COMPLAINT_detail.Complaint_no=TSPL_CUSTOMER_COMPLAINT_HEAD.Complaint_no and  TSPL_CUSTOMER_COMPLAINT_detail.Item_code = TSPL_SD_SHIPMENT_DETAIL.Item_Code  where    Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) >=  Convert(date, '" + clsCommon.GetPrintDate(txtABSDate.Value, "dd/MMM/yyyy") + "',103) and Convert(date, TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=  Convert(date, '" + clsCommon.GetPrintDate(txtABSDate.Value, "dd/MMM/yyyy") + "',103)   and TSPL_SD_SHIPMENT_HEAD.status = 1     and TSPL_SD_SHIPMENT_HEAD.IsReplacement = 1 and TSPL_CUSTOMER_COMPLAINT_detail.Complaint_Code = 'ES' " + whr_LR_PQ_ES + " " &
                ''          " )XXXX1 "
                ''    Dim dtLRPQES As DataTable = clsDBFuncationality.GetDataTable(qry)
                'If dtLRPQES IsNot Nothing And dtLRPQES.Rows.Count > 0 Then
                '    strLRQty = clsCommon.myCstr(dtLRPQES.Rows(0)("LR"))
                '    strPQQty = clsCommon.myCstr(dtLRPQES.Rows(0)("PQ"))
                '    strESQty = clsCommon.myCstr(dtLRPQES.Rows(0)("ES"))
                '    strLRPQES_TotalQty = clsCommon.myCstr(dtLRPQES.Rows(0)("Total_LR_PQ_ES_Qty"))
                'End If

                '    obj.arrReportFooter = New List(Of clsDosPrintReportFooter)
                '    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Scheme Quantity", "", "", "", ""))
                '    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("CASH Qty", "0", "", " ", ":"))
                '    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Card Qty", "0", "", " ", ":"))
                '    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Credit Qty", "0", "", " ", ":"))
                '    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("SO Qty", "0", "", "", ":"))
                '    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Credit Qty", "0", "", " ", ":"))

                '    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("-----------------------------------", "------------------------------------------------", "", "", ""))
                '    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Leakage Replacement Quantity(Ltrs)", strLRQty, "", "", ":"))
                '    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Poor Quality Replacement", strPQQty, "", "", ":"))
                '    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("Market Returns Replacement", strESQty, "", "", ":"))
                '    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("-----------------------------------", "---------------------", "------------", "", ""))
                '    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("TOTAL", strLRPQES_TotalQty, "", "", ":"))
                '    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("-----------------------------------", "---------------------", "------------", "", ""))
                'End If

                obj.Print(obj, dt, PageSetup.Landscap)

            End If
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
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
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
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
    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " select Code,Name from TSPL_INVENTORY_SOURCE_CODE "

        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub
    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String
        qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel@Batch", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = ""
        If clsCommon.myLen(txtRouteCode.Value) > 0 Then
            qry = " Select Cust_Code as Code , Customer_Name as [Name], Route_No as [Route Code] from TSPL_CUSTOMER_MASTER  where Route_No = '" + txtRouteCode.Value + "' "
        Else
            qry = " Select Cust_Code as Code , Customer_Name as [Name], Route_No as [Route Code] from TSPL_CUSTOMER_MASTER  "
        End If
        ' where TSPL_CUSTOMER_MASTER.Status='N'
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Cust@DemandBooking", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBookingQtyAmtReport & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            'If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            'End If
            'If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            'End If
            'If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
            'End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                'clsCommon.MyExportToExcelGrid("Demand Booking Report", Gv1, arrHeader, Me.Text)
                transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, True)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                'clsCommon.MyExportToPDF("Demand Booking Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, True)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypForBatchItemRep", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    End Sub
    Private Sub DemandReportFormat()
        If Gv1.Rows.Count > 0 Then

            'TOTAL
            Dim summaryRowItemTotal As New GridViewSummaryRowItem()
            'For i As Integer = 0 To Gv1.Columns.Count - 1
            '    Dim aa As String = Gv1.Columns(i).HeaderText()
            '    Dim summaryItemTotal As New GridViewSummaryItem()
            '    summaryItemTotal.FormatString = "{0:n0}"
            '    summaryItemTotal.Name = aa
            '    summaryItemTotal.AggregateExpression = "sum(" + aa + ")"
            '    summaryRowItemTotal.Add(summaryItemTotal)
            'Next

            Dim aa As String = "DTM 200 Ml_Crate"
            Dim summaryItemTotal As New GridViewSummaryItem()
            summaryItemTotal.FormatString = "{0:n0}"
            summaryItemTotal.Name = aa
            summaryItemTotal.AggregateExpression = "sum(" + aa + ")"
            summaryRowItemTotal.Add(summaryItemTotal)



            'LTR TOTAL
            'Dim summaryRowItemLtrTotal As New GridViewSummaryRowItem()
            'For i As Integer = 0 To Gv1.Columns.Count - 1
            '    Dim aa = Gv1.Columns(i).HeaderText()
            '    Dim summaryItemLtrTotal As New GridViewSummaryItem()
            '    summaryItemLtrTotal.FormatString = "{0:n2}"
            '    summaryItemLtrTotal.Name = aa

            '    If clsCommon.CompairString(aa.Substring(aa.Length - 6, 6), "_Crate") = CompairStringResult.Equal Then

            '        Dim item1 As New GridViewSummaryItem()
            '        item1.FormatString = "{0:F2}"
            '        item1.Name = aa
            '        'item1.AggregateExpression = "sum([" + aa.Substring(0, aa.Length - 2) + "_A" + "])/sum([" + aa.Substring(0, aa.Length - 2) + "])"
            '        item1.AggregateExpression = "sum([" + aa.Substring(0, aa.Length - 6) + "_Ltr" + "])"


            '        ',  sum([" + aa.Substring(0, aa.Length - 2) + "_A" + "])/sum([" + aa.Substring(0, aa.Length - 2) + "])"
            '        summaryRowItemLtrTotal.Add(item1)
            '    End If

            'Next


            ''''''''''''''''''''''''''''''''''''''

            Gv1.MasterTemplate.Rows.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemTotal)
            'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemLtrTotal)
            ''''''''''''''''''''''''''''''''''''''
            'Dim summaryRowItemB As New GridViewSummaryRowItem()
            'Dim summaryRowItemC As New GridViewSummaryRowItem()


            'Dim MilkTypeB As New GridViewSummaryItem("Customer Name", "Total Ltr", GridAggregateFunction.Max)
            'summaryRowItemB.Add(MilkTypeB)
            'Dim MilkTypeC As New GridViewSummaryItem("Customer Name", "Total Amt", GridAggregateFunction.Max)
            'summaryRowItemC.Add(MilkTypeC)

            'For i As Integer = 2 To dtgv.Rows.Count - 1
            '    Dim aa = Gv1.Columns(i).HeaderText()

            '    Dim summaryItemB As New GridViewSummaryItem()
            '    summaryItemB.FormatString = "{0:n2}"
            '    summaryItemB.Name = aa
            '    summaryItemB.AggregateExpression = "sum(IIF([Milk Type]='B',[" + aa + "],0))"
            '    summaryRowItemB.Add(summaryItemB)

            '    Dim summaryItemC As New GridViewSummaryItem()
            '    summaryItemC.FormatString = "{0:n2}"
            '    summaryItemC.Name = aa
            '    summaryItemC.AggregateExpression = "sum(IIF([Milk Type]='C',[" + aa + "],0))"
            '    summaryRowItemC.Add(summaryItemC)

            'Next

            ''For i As Integer = 9 To 9 + 2 + dtREJECT.Rows.Count - 1
            ''    Dim aa = Gv1.Columns(i).HeaderText()

            ''    Dim summaryItemB As New GridViewSummaryItem()
            ''    summaryItemB.FormatString = "{0:n2}"
            ''    summaryItemB.Name = aa + "(%)"
            ''    summaryItemB.AggregateExpression = "sum(IIF([Milk Type]='B',[" + aa + "],0))*100/sum(IIF([Milk Type]='B',[Milk Weight],0))"
            ''    summaryRowItemB.Add(summaryItemB)

            ''    Dim summaryItemC As New GridViewSummaryItem()
            ''    summaryItemC.FormatString = "{0:n2}"
            ''    summaryItemC.Name = aa + "(%)"
            ''    summaryItemC.AggregateExpression = "sum(IIF([Milk Type]='C',[" + aa + "],0))*100/sum(IIF([Milk Type]='C',[Milk Weight],0))"
            ''    summaryRowItemC.Add(summaryItemC)

            ''Next


            'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
            'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemC)
        End If

        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        'Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Sub View()

        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(" "))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())

            If chkDistributor.Checked Then
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Distributor Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Distributor Name").Name)
                If rdbHindi.IsChecked = True Then
                    Gv1.Columns("Distributor Code").HeaderText = "वितरणकर्ता का कोड"
                    Gv1.Columns("Distributor Name").HeaderText = "वितरणकर्ता का नाम"
                End If
            Else
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Distributor Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Distributor Name").Name)
                If rdbHindi.IsChecked = True Then
                    Gv1.Columns("Distributor Code").HeaderText = "वितरणकर्ता का कोड"
                    Gv1.Columns("Distributor Name").HeaderText = "वितरणकर्ता का नाम"
                End If

                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Customer Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Customer Name").Name)
                If rdbHindi.IsChecked = True Then
                    Gv1.Columns("Customer Code").HeaderText = "ग्राहक का कोड"
                    Gv1.Columns("Customer Name").HeaderText = "ग्राहक का नाम"
                End If
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

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            'If clsCommon.myLen(txtRouteCode) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please select Route first...")
            '    Return
            'End If

            ToDate.Value = fromDate.Value
            Dim whr As String = "  and convert (date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103) = convert (date, '" + fromDate.Text + "',103) "
            If clsCommon.myLen(txtRouteCode.Value) > 0 Then
                whr += " and TSPL_DEMAND_BOOKING_MASTER.Route_No = '" + txtRouteCode.Value + "'"
            End If

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                whr += " and TSPL_DEMAND_BOOKING_DETAIL.cust_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")"
            End If

            Dim qry As String = "  select XFinal.Route_No,max(TSPL_ROUTE_MASTER.Route_Desc) as Route_Name, max(Doc_Year) as Doc_Year , max(Doc_Day) as Doc_Day ,max(Doc_Month) as Doc_Month,
            max(Doc_Year_Next) as Doc_Year_Next , max(Doc_Day_Next) as Doc_Day_Next ,max(Doc_Month_Next) as Doc_Month_Next
            ,XFinal.cust_Code , max(TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name,  max(TSPL_CUSTOMER_MASTER.Customer_Name_Hindi) as Customer_Name_Hindi
            ,sum ([Item1#M]) as [Item1#M],sum([Item2#M]) as [Item2#M],sum([Item3#M]) as [Item3#M],sum([Item4#M]) as [Item4#M],sum ([Item5#M]) as [Item5#M],sum([Item6#M]) as [Item6#M],sum([Item7#M]) as [Item7#M],sum([Item8#M]) as [Item8#M],sum([Item9#M]) as [Item9#M] ,sum([Item10#M]) as [Item10#M], sum([Item11#M]) as [Item11#M] , sum([Total#MA]) as [Total#MA] 

            ,sum ([Item1#E]) as [Item1#E],sum([Item2#E]) as [Item2#E],sum([Item3#E]) as [Item3#E],sum([Item4#E]) as [Item4#E],sum ([Item5#E]) as [Item5#E],sum([Item6#E]) as [Item6#E],sum([Item7#E]) as [Item7#E],sum([Item8#E]) as [Item8#E],sum([Item9#E]) as [Item9#E] ,sum([Item10#E]) as [Item10#E], sum([Item11#E]) as [Item11#E] , sum([Total#EA]) as [Total#EA] 

            ,sum ([Item1#M]) + sum ([Item1#E]) as [Item1#T],sum([Item2#M]) + sum([Item2#E]) as [Item2#T],sum([Item3#M]) + sum([Item3#E]) as [Item3#T],sum([Item4#M]) + sum([Item4#E]) as [Item4#T],sum ([Item5#M]) + sum ([Item5#E]) as [Item5#T],sum([Item6#M]) + sum([Item6#E]) as [Item6#T],sum([Item7#M]) + sum([Item7#E]) as [Item7#T],sum([Item8#M]) + sum([Item8#E]) as [Item8#T],sum([Item9#M]) + sum([Item9#E]) as [Item9#T] ,sum([Item10#M]) + sum([Item10#E]) as [Item10#T], sum([Item11#M]) + sum([Item11#E]) as [Item11#T] , sum([Total#MA]) + sum([Total#EA]) as [Total#T]
            , sum([Item1#MA]+[Item1#EA]) as [Item1#MEA], sum([Item2#MA]+[Item2#EA]) as [Item2#MEA], sum([Item3#MA]+[Item3#EA]) as [Item3#MEA] , sum([Item4#MA]+[Item4#EA]) as [Item4#MEA] , sum([Item5#MA]+[Item5#EA]) as [Item5#MEA] , sum([Item6#MA]+[Item6#EA]) as [Item6#MEA] , sum([Item7#MA]+[Item7#EA]) as [Item7#MEA], sum([Item8#MA]+[Item8#EA]) as [Item8#MEA] , sum([Item9#MA]+ [Item9#EA]) as [Item9#MEA] , sum([Item10#MA]+[Item10#EA]) as [Item10#MEA] , Sum([Item11#MA]+[Item11#EA]) as [Item11#MEA]



            from (
            select Route_No, Doc_Year,Doc_Day,Doc_Month, Doc_Year_Next,Doc_Day_Next,Doc_Month_Next,cust_Code , isnull ([Item1#M],0) as [Item1#M] ,isnull ([Item2#M],0) as [Item2#M],isnull ( [Item3#M],0) as [Item3#M] ,isnull([Item4#M],0) as [Item4#M],isnull([Item5#M],0) as [Item5#M],isnull([Item6#M],0) as [Item6#M] ,isnull([Item7#M],0) as [Item7#M],isnull([Item8#M],0) as [Item8#M],isnull([Item9#M],0) as [Item9#M] ,isnull([Item10#M],0) as [Item10#M],isnull([Item11#M],0) as [Item11#M]  ,

            isnull ([Item1#E],0) as [Item1#E] ,isnull ([Item2#E],0) as [Item2#E],isnull ( [Item3#E],0) as [Item3#E] ,isnull([Item4#E],0) as [Item4#E],isnull([Item5#E],0) as [Item5#E],isnull([Item6#E],0) as [Item6#E] ,isnull([Item7#E],0) as [Item7#E],isnull([Item8#E],0) as [Item8#E],isnull([Item9#E],0) as [Item9#E] ,isnull([Item10#E],0) as [Item10#E],isnull([Item11#E],0) as [Item11#E] ,

            isnull([Item1#MA],0) as [Item1#MA], isnull([Item2#MA],0) as [Item2#MA], isnull([Item3#MA],0) as [Item3#MA] , isnull([Item4#MA],0) as [Item4#MA] , isnull([Item5#MA],0) as [Item5#MA] , isnull([Item6#MA],0) as [Item6#MA] , isnull([Item7#MA],0) as [Item7#MA], isnull([Item8#MA],0) as [Item8#MA] , isnull([Item9#MA],0) as [Item9#MA] , isnull([Item10#MA],0) as [Item10#MA] , isnull([Item11#MA],0) as [Item11#MA],

            isnull([Item1#MA],0)+isnull([Item2#MA],0)+isnull([Item3#MA],0)+isnull([Item4#MA],0)+isnull([Item5#MA],0)+isnull([Item6#MA],0)+isnull([Item7#MA],0)+isnull([Item8#MA],0)+isnull([Item9#MA],0)+isnull([Item10#MA],0)+isnull([Item11#MA],0) as [Total#MA]

            ,isnull([Item1#EA],0) as [Item1#EA] , isnull([Item2#EA],0) as [Item2#EA] , isnull([Item3#EA],0) as [Item3#EA], isnull([Item4#EA],0) as [Item4#EA] , isnull([Item5#EA],0) as [Item5#EA] , isnull([Item6#EA],0) as [Item6#EA] , isnull([Item7#EA],0) as [Item7#EA], isnull([Item8#EA],0) as [Item8#EA] , isnull([Item9#EA],0) as [Item9#EA] , isnull([Item10#EA],0) as [Item10#EA] , isnull([Item11#EA],0) as [Item11#EA]
            , isnull([Item1#EA],0)+isnull([Item2#EA],0)+isnull([Item3#EA],0)+isnull([Item4#EA],0)+isnull([Item5#EA],0)+isnull([Item6#EA],0)+isnull([Item7#EA],0)+isnull([Item8#EA],0)+isnull([Item9#EA],0)+isnull([Item10#EA],0)+isnull([Item11#EA],0) as [Total#EA]

            from (
            select TSPL_DEMAND_BOOKING_MASTER.Route_No , datepart ( year, convert (date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)) as Doc_Year ,  datepart ( DAY, convert (date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103))  as  Doc_Day , left( convert (varchar, datename(mm, ( datepart ( MONTH, convert (date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103))))),3)  as  Doc_Month,

             datepart ( year, convert (date, DATEADD(DY,1,TSPL_DEMAND_BOOKING_MASTER.Document_Date),103)) as Doc_Year_Next ,  datepart ( DAY, convert (date, DATEADD(DY,1,TSPL_DEMAND_BOOKING_MASTER.Document_Date),103))  as  Doc_Day_Next , left( convert (varchar, datename(mm, ( datepart ( MONTH, convert (date, DATEADD(DY,1,TSPL_DEMAND_BOOKING_MASTER.Document_Date),103))))),3)  as  Doc_Month_Next,

            TSPL_DEMAND_BOOKING_DETAIL.cust_Code,
            Case 
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00001' then 'Item1'  
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00005' then 'Item2'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00006' then 'Item3'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00008' then 'Item4'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00009' then 'Item5'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00002' then 'Item6'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00011' then 'Item7'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00003' then 'Item8'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00004' then 'Item9'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00007' then 'Item10'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00010' then 'Item11' end  + '#M' Item_Code_M , 
            Case 
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00001' then 'Item1'  
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00005' then 'Item2'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00006' then 'Item3'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00008' then 'Item4'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00009' then 'Item5'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00002' then 'Item6'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00011' then 'Item7'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00003' then 'Item8'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00004' then 'Item9'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00007' then 'Item10'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00010' then 'Item11' end  + '#MA' Item_Code_MA,

            TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise  as Qty_M , TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount as Amount_M
            , '' as Item_Code_E , '' as Item_Code_EA ,0.0 as Qty_E , 0.00 as Amount_E
            from TSPL_DEMAND_BOOKING_DETAIL left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No where TSPL_DEMAND_BOOKING_DETAIL.ShiftType = 'Morning' 
            " + whr + "

            union All

            select TSPL_DEMAND_BOOKING_MASTER.Route_No , datepart ( year, convert (date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103)) as Doc_Year ,  datepart ( DAY, convert (date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103))  as  Doc_Day , left( convert (varchar, datename(mm, ( datepart ( MONTH, convert (date, TSPL_DEMAND_BOOKING_MASTER.Document_Date,103))))),3)  as  Doc_Month,
             datepart ( year, convert (date, DATEADD(DY,1,TSPL_DEMAND_BOOKING_MASTER.Document_Date),103)) as Doc_Year_Next ,  datepart ( DAY, convert (date, DATEADD(DY,1,TSPL_DEMAND_BOOKING_MASTER.Document_Date),103))  as  Doc_Day_Next , left( convert (varchar, datename(mm, ( datepart ( MONTH, convert (date, DATEADD(DY,1,TSPL_DEMAND_BOOKING_MASTER.Document_Date),103))))),3)  as  Doc_Month_Next,

            TSPL_DEMAND_BOOKING_DETAIL.cust_Code,'' as Item_Code_M, '' as Item_Code_MA , 0.00 as Qty_M , 0.00 as  Amount_M, 
            Case 
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00001' then 'Item1'  
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00005' then 'Item2'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00006' then 'Item3'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00008' then 'Item4'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00009' then 'Item5'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00002' then 'Item6'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00011' then 'Item7'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00003' then 'Item8'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00004' then 'Item9'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00007' then 'Item10'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00010' then 'Item11' end  + '#E' Item_Code_E , 
            Case 
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00001' then 'Item1'  
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00005' then 'Item2'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00006' then 'Item3'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00008' then 'Item4'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00009' then 'Item5'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00002' then 'Item6'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00011' then 'Item7'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00003' then 'Item8'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00004' then 'Item9'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00007' then 'Item10'
            when TSPL_DEMAND_BOOKING_DETAIL.Item_Code = 'FG00010' then 'Item11' end  + '#EA' Item_Code_EA,

            TSPL_DEMAND_BOOKING_DETAIL.TotalCrates_ItemWise  as Qty_E , TSPL_DEMAND_BOOKING_DETAIL.ItemNetAmount as Amount_E
            from TSPL_DEMAND_BOOKING_DETAIL left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No where TSPL_DEMAND_BOOKING_DETAIL.ShiftType = 'Evening' 
            " + whr + "

            ) XXXFinal
            pivot ( sum(Qty_M) for Item_code_M in ([Item1#M],[Item2#M],[Item3#M],[Item4#M],[Item5#M],[Item6#M],[Item7#M],[Item8#M],[Item9#M],[Item10#M],[Item11#M])) as MItemPivot
            pivot ( sum(Amount_M) for Item_Code_MA in ([Item1#MA],[Item2#MA],[Item3#MA],[Item4#MA],[Item5#MA],[Item6#MA],[Item7#MA],[Item8#MA],[Item9#MA],[Item10#MA],[Item11#MA])) as MAItemPivot
            pivot ( sum(Qty_E) for Item_code_E in ([Item1#E],[Item2#E],[Item3#E],[Item4#E],[Item5#E],[Item6#E],[Item7#E],[Item8#E],[Item9#E],[Item10#E],[Item11#E])) as MItemPivot
            pivot ( sum(Amount_E) for Item_Code_EA in ([Item1#EA],[Item2#EA],[Item3#EA],[Item4#EA],[Item5#EA],[Item6#EA],[Item7#EA],[Item8#EA],[Item9#EA],[Item10#EA],[Item11#EA])) as MAItemPivot

            ) XFinal
            left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No = XFinal.Route_No
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = XFinal.Cust_Code
            group by XFinal.Route_No , XFinal.Cust_Code "
            Dim dtPrint As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtPrint IsNot Nothing And dtPrint.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(False, CrystalReportFolder.SalesReport, dtPrint, "rptDayShiftWiseDmand", "Day Shift wise Demand Print of Customer")
                frmCRV = Nothing
                Return
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtRouteCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteCode._MYValidating
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER"
        txtRouteCode.Value = clsCommon.ShowSelectForm("Demand@Print@DayshiftWise", qry, "Code", "", txtRouteCode.Value, "Code", isButtonClicked)
        lblRouteCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc as Name from TSPL_ROUTE_MASTER where Route_No ='" + txtRouteCode.Value + "' "))
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PrintView(False)
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        PrintView(True)
    End Sub
End Class
