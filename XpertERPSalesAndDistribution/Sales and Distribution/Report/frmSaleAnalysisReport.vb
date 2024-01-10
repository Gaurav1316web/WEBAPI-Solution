Imports common
Imports System.IO
Public Class frmSaleAnalysisReport

#Region "Variable"
    Dim PreviousFromDate As Date = Nothing
    Dim PreviousToDate As Date = Nothing
#End Region
    Private Sub frmSaleAnalysisReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If rbtnDaily.Checked Then
                fromDate.Value = clsCommon.GETSERVERDATE()
                ToDate.Value = fromDate.Value
                ToDate.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Reset()
        Enable()
    End Sub
    Sub Enable()
        RadGroupBox1.Enabled = True
        RadGroupBox3.Enabled = True
        txtZone.Enabled = True
        TxtRoute.Enabled = True
        txtMultBooth.Enabled = True
        txtMultDistributor.Enabled = True
        txtItemCode.Enabled = True
    End Sub
    Sub Disable()
        RadGroupBox1.Enabled = False
        RadGroupBox3.Enabled = False
        txtZone.Enabled = False
        TxtRoute.Enabled = False
        txtMultBooth.Enabled = False
        txtMultDistributor.Enabled = False
        txtItemCode.Enabled = False
    End Sub

    Private Sub fromDate_Validated(sender As Object, e As EventArgs) Handles fromDate.Validated
        Try
            If rbtnDaily.Checked Then
                If clsCommon.myLen(fromDate.Value) > 0 Then
                    ToDate.Value = fromDate.Value
                    ToDate.ReadOnly = True
                    PreviousFromDate = fromDate.Value.AddDays(-1)
                    PreviousToDate = fromDate.Value
                End If
            ElseIf rbtnWeekly.Checked Then
                If clsCommon.myLen(fromDate.Value) > 0 Then
                    ToDate.Value = fromDate.Value.AddDays(6)
                    ToDate.ReadOnly = True
                    PreviousFromDate = fromDate.Value.AddDays(-6)
                    PreviousToDate = fromDate.Value
                End If
            Else
                If clsCommon.myLen(fromDate.Value) > 0 Then
                    fromDate.Value = clsCommon.GetPrintDate(fromDate.Value, "MM/yyyy")
                    ToDate.Value = clsCommon.GetPrintDate(fromDate.Value, "MM/yyyy")
                    ToDate.ReadOnly = True
                    PreviousFromDate = clsCommon.GetPrintDate(fromDate.Value.AddMonths(-1), "MM/yyyy")
                    PreviousToDate = clsCommon.GetPrintDate(fromDate.Value, "MM/yyyy")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnMonthly_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnMonthly.CheckedChanged
        Try
            fromDate.CustomFormat = "MM/yyyy"
            ToDate.CustomFormat = "MM/yyyy"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnWeekly_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnWeekly.CheckedChanged
        Try
            fromDate.CustomFormat = "dd/MM/yyyy"
            ToDate.Value = fromDate.Value.AddDays(6)
            ToDate.CustomFormat = "dd/MM/yyyy"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnDaily_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnDaily.CheckedChanged
        Try
            fromDate.CustomFormat = "dd/MM/yyyy"
            ToDate.Value = fromDate.Value
            ToDate.CustomFormat = "dd/MM/yyyy"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            'Dim Qry As String = Nothing
            'Dim BaseQry As String = BaseQuery()
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQry)
            'If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            '    Gv1.DataSource = Nothing
            '    Gv1.Rows.Clear()
            '    Gv1.Columns.Clear()
            '    Gv1.GroupDescriptors.Clear()
            '    Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            '    Gv1.MasterView.Refresh()
            '    Gv1.DataSource = dt
            '    SetGridFormat()
            '    Disable()
            '    dt = Nothing
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat()
        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AutoExpandGroups = True
        Gv1.ShowGroupPanel = True
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        'Gv1.Columns("From Date").IsVisible = False
        'Gv1.Columns("To Date").IsVisible = False
        'Gv1.Columns("Location").IsVisible = False
        'Gv1.Columns("Comp_Name").IsVisible = False
        'Gv1.Columns("Regn_No").IsVisible = False
        'Gv1.Columns("Comp Contact No").IsVisible = False

        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True

        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim item As New GridViewSummaryItem
        'If Gv1.Rows IsNot Nothing AndAlso Gv1.Columns.Count > 0 Then
        '    For ii As Integer = 0 To Gv1.Columns.Count - 1
        '        If ii > 5 Then
        '            item = New GridViewSummaryItem(Gv1.Columns(ii).HeaderText, "{0:n2}", GridAggregateFunction.Sum)
        '            summaryRowItem.Add(item)
        '        End If
        '    Next
        'End If
        'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Public Function BaseQuery() As String
        Dim Qry As String = "Select xxx.*,(xxx.QtyLtr-xxx.PreviousQtyLtr) As [Grow Qty Ltr], (xxx.DocumentAmount-xxx.PreviousDocumentAmount) As [Grow Amount] from (
                            Select CurrentData.*,IsNull(PreviousData.QtyLtr,0) As [PreviousQtyLtr],IsNull(PreviousData.DocumentAmount,0) As [PreviousDocumentAmount] From(select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date,max([Time]) as [Time]  ,zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr 
                            from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date, case when LTRIM(RIGHT(CONVERT(VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100), 7)) = '12:00AM' then LTRIM(RIGHT(CONVERT(VARCHAR(20),TSPL_BOOKING_MATSER.Created_Date , 100), 7)) else LTRIM(RIGHT(CONVERT(VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100), 7)) end  as [Time],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO],  TSPL_BOOKING_DETAIL.Booking_Qty  as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr 
                            From TSPL_BOOKING_DETAIL 
                            Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No 
                            Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code 
                            Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code And TSPL_ITEM_MASTER.Item_Type='F' And TSPL_ITEM_MASTER.Is_FreshItem='1'
                            Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code 
                            Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code 
                            left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code 
                            Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  and ThisTableHead.Is_Taxable=1  and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=1 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No  ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No
                            Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No   ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No
                            left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where   Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  
                            LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No   
                            left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  
                            left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' 
                            left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'
                            left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code   
                            where 2=2 "
        If rbtnMonthly.Checked Then
            Qry += " and convert(Varchar(7), TSPL_BOOKING_MATSER.Document_Date,126) >= Convert(Varchar(7),'" + fromDate.Value + "',126) and  convert(Varchar(7), TSPL_BOOKING_MATSER.Document_Date,126) <= Convert(Varchar(7),'" + ToDate.Value + "',126) "
        Else
            Qry += " and convert(date, TSPL_BOOKING_MATSER.Document_Date,103) >= Convert(date,'" + fromDate.Value + "',103) and  convert(date, TSPL_BOOKING_MATSER.Document_Date,103) <= Convert(date,'" + ToDate.Value + "',103) "
        End If

        If txtZone.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtZone.arrValueMember) > 0 Then
            Qry += " and TSPL_CUSTOMER_MASTER.Zone_Code In (" & clsCommon.GetMulcallString(txtZone.arrValueMember) & ")"
        End If

        If TxtRoute.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(TxtRoute.arrValueMember) > 0 Then
            Qry += " and TSPL_BOOKING_DETAIL.Route_No In (" & clsCommon.GetMulcallString(TxtRoute.arrValueMember) & ")"
        End If

        If txtMultBooth.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtMultBooth.arrValueMember) > 0 Then
            Qry += " "
        End If

        If txtMultDistributor.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtMultDistributor.arrValueMember) > 0 Then
            Qry += " "
        End If


        If txtItemCode.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtItemCode.arrValueMember) > 0 Then
            Qry += " and TSPL_BOOKING_DETAIL.Item_Code In (" & clsCommon.GetMulcallString(txtItemCode.arrValueMember) & ")"
        End If

        Qry += ")zzz  where zzz.Scheme_Item='N'group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] ) CurrentData
                            Left Outer Join 
                            (
                            (select max(zzz.item_code) as item_code,zzz.Document_No, max(Document_Date) as Document_Date,max([Time]) as [Time]  ,zzz.[DO No], zzz.[Short Close],max([Dispatch No(NT)]) as [Dispatch No(NT)], max([Invoice No(NT)]) as [Invoice No(NT)] ,max([Dispatch No(T)]) as [Dispatch No(T)],max([Invoice No(T)]) as [Invoice No(T)],max(Scheme_Booking_Qty) as Scheme_Booking_Qty,max(Booking_Type) as Booking_Type,max(BookingThrough) as [BookingThrough], max(TruckSheetGenerate) as TruckSheetGenerate, max(AgainstGatePass) as AgainstGatePass,max(is_Cancelled) as is_Cancelled,max(Payment_Mode) as Payment_Mode, max(GatePass_Type) as [Booking Time(AM/PM)],Max(Created_By) as Created_By,max(Created_Date) as Created_Date,max(Modified_By) as Modified_By,max(Modified_Date) as Modified_Date ,max(DocumentAmount) as DocumentAmount,max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as qty,sum(QtyLtr) as QtyLtr 
                            from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date, case when LTRIM(RIGHT(CONVERT(VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100), 7)) = '12:00AM' then LTRIM(RIGHT(CONVERT(VARCHAR(20),TSPL_BOOKING_MATSER.Created_Date , 100), 7)) else LTRIM(RIGHT(CONVERT(VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100), 7)) end  as [Time],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO],  TSPL_BOOKING_DETAIL.Booking_Qty  as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.OldName as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr 
                            From TSPL_BOOKING_DETAIL 
                            Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No 
                            Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code 
                            Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code And TSPL_ITEM_MASTER.Item_Type='F' And TSPL_ITEM_MASTER.Is_FreshItem='1'
                            Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code 
                            Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code 
                            left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code 
                            Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  and ThisTableHead.Is_Taxable=1  and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=1 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No  ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No
                            Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No   ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No
                            left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where   Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  
                            LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No   
                            left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  
                            left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' 
                            left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'
                            left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code   
                            where 2=2  "
        If rbtnMonthly.Checked Then
            Qry += " and convert(Varchar(7), TSPL_BOOKING_MATSER.Document_Date,126) >= Convert(Varchar(7),'" + PreviousFromDate + "',126) and  convert(Varchar(7), TSPL_BOOKING_MATSER.Document_Date,126) < Convert(Varchar(7),'" + PreviousToDate + "',126) "
        Else
            Qry += " and convert(date, TSPL_BOOKING_MATSER.Document_Date,103) >= Convert(date'" + PreviousFromDate + "',103) and  convert(date, TSPL_BOOKING_MATSER.Document_Date,103) < Convert(date,'" + PreviousToDate + "',103) "
        End If

        If txtZone.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtZone.arrValueMember) > 0 Then
            Qry += " and TSPL_CUSTOMER_MASTER.Zone_Code In (" & clsCommon.GetMulcallString(txtZone.arrValueMember) & ")"
        End If

        If TxtRoute.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(TxtRoute.arrValueMember) > 0 Then
            Qry += " and TSPL_BOOKING_DETAIL.Route_No In (" & clsCommon.GetMulcallString(TxtRoute.arrValueMember) & ")"
        End If

        If txtMultBooth.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtMultBooth.arrValueMember) > 0 Then
            Qry += " "
        End If

        If txtMultDistributor.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtMultDistributor.arrValueMember) > 0 Then
            Qry += " "
        End If


        If txtItemCode.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtItemCode.arrValueMember) > 0 Then
            Qry += " and TSPL_BOOKING_DETAIL.Item_Code In (" & clsCommon.GetMulcallString(txtItemCode.arrValueMember) & ")"
        End If

        Qry += ")zzz where zzz.Scheme_Item='N'group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] ) 
                            ) PreviousData On  PreviousData.item_code=CurrentData.item_code And PreviousData.[Customer Code]=CurrentData.[Customer Code]) xxx"
        Return Qry
    End Function

    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Try
            Dim qry As String = "select distinct TSPL_ZONE_MASTER.Zone_Code AS Code,TSPL_ZONE_MASTER.Description as Name ,TSPL_ZONE_MASTER.City_Code as [City Code],TSPL_CITY_MASTER.City_Name as [City Name]  from TSPL_ZONE_MASTER left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code = TSPL_ZONE_MASTER.City_Code 
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Zone_Code = TSPL_ZONE_MASTER.Zone_Code"

            txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("SaleAnalysisZone", qry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtRoute__My_Click(sender As Object, e As EventArgs) Handles TxtRoute._My_Click
        Try
            Dim qry As String = "select Route_No As [Route Code],Route_Desc As [Discription],City_Code As [City] from TSPL_ROUTE_MASTER Order By Route_No"
            TxtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("SaleAnalysisRoute", qry, "Route Code", "Discription", TxtRoute.arrValueMember, TxtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultBooth__My_Click(sender As Object, e As EventArgs) Handles txtMultBooth._My_Click
        Try
            Dim qry As String = "Select Cust_Code As [Code], Customer_Name As [Name], City_Code As [City], State  from TSPL_CUSTOMER_MASTER "
            txtMultBooth.arrValueMember = clsCommon.ShowMultipleSelectForm("SaleAnalysisBooth", qry, "Code", "Name", txtMultBooth.arrValueMember, txtMultBooth.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMultDistributor__My_Click(sender As Object, e As EventArgs) Handles txtMultDistributor._My_Click
        Try
            Dim Qry As String = "Select Cust_Code As [Code], Customer_Name as [Name], City_Code As [City], State  from TSPL_CUSTOMER_MASTER Where IsDistributor='Y'"
            txtMultDistributor.arrValueMember = clsCommon.ShowMultipleSelectForm("SaleAnalysisDistributor", Qry, "Code", "Name", txtMultDistributor.arrValueMember, txtMultDistributor.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtItemCode__My_Click(sender As Object, e As EventArgs) Handles txtItemCode._My_Click
        Try
            Dim Qry As String = "Select Item_Code As [Code],Item_Desc As [Description],Structure_Code As [Structure Code],Case When Item_Type='F' Then 'Fresh' Else '' End As [Item Type] from TSPL_Item_Master where TSPL_ITEM_MASTER.Item_Type='F' And TSPL_ITEM_MASTER.Is_FreshItem='1'"
            txtItemCode.arrValueMember = clsCommon.ShowMultipleSelectForm("SaleAnalysisItem", Qry, "Code", "Description", txtItemCode.arrValueMember, txtItemCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class