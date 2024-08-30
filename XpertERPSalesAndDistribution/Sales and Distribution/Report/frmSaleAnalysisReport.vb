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

    Sub Enable()
        RadGroupBox1.Enabled = True
        RadGroupBox2.Enabled = True
        RadGroupBox3.Enabled = True
        txtZone.Enabled = True
        TxtRoute.Enabled = True
        txtMultBooth.Enabled = True
        txtMultDistributor.Enabled = True
        txtItemCode.Enabled = True
    End Sub
    Sub Disable()
        RadGroupBox1.Enabled = False
        RadGroupBox2.Enabled = False
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
                    'fromDate.Value = clsCommon.GetPrintDate(fromDate.Value, "MM/yyyy")
                    'ToDate.Value = clsCommon.GetPrintDate(fromDate.Value.AddMonths(1), "MM/yyyy")
                    fromDate.Value = clsDBFuncationality.getSingleValue("SELECT DATEADD(DAY, 1, EOMONTH(DATEADD(MONTH, -1, (Convert(date,'" + fromDate.Value + "',103)))))")
                    ToDate.Value = clsDBFuncationality.getSingleValue("SELECT CAST(eomonth(Convert(date,'" + fromDate.Value + "',103)) AS Date)")
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
            fromDate.Value = clsDBFuncationality.getSingleValue("SELECT DATEADD(DAY, 1, EOMONTH(DATEADD(MONTH, -1, (Convert(date,'" + fromDate.Value + "',103)))))")
            fromDate.CustomFormat = "MM/yyyy"
            ToDate.Value = clsDBFuncationality.getSingleValue("SELECT CAST(eomonth(Convert(date,'" + fromDate.Value + "',103)) AS Date)")
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
    Sub GetReportGridID()
        Dim VarID As String = ""
        If rbtnDetails.Checked = True Then
            VarID += "_DE"
        Else rbtnSummary.Checked = True
            VarID += "_SU"
        End If
        If rbtnDaily.Checked = True Then
            VarID += "_DA"
        ElseIf rbtnWeekly.Checked = True Then
            VarID += "_WE"
        ElseIf rbtnMonthly.Checked = True Then
            VarID += "_MO"
        End If
        Gv1.VarID = VarID
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            GetReportGridID()
            Dim Qry As String = Nothing
            Dim BaseQry As String = BaseQuery()
            Dim PreviousWise As String = Nothing
            If rbtnDetails.Checked Then
                Qry = "Select "
                If rbtnDaily.Checked Then
                    PreviousWise = "Daily"
                    Qry += "'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "' As [Daily]"
                ElseIf rbtnWeekly.Checked Then
                    PreviousWise = "Weekly"
                    Qry += "'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "'+' - '+'" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "' As [Weekly]"
                Else
                    PreviousWise = "Monthly"
                    Qry += "'" + clsCommon.myCDate(fromDate.Value, "dd/MM/yyyy") + "'+' - '+'" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "' As [Monthly]"
                End If
                Qry += " ,Final.item_code As [Item Code],Max(Final.Description)Description,Convert(Int,Sum(Final.Qty)) As [Crate Qty],Convert(Decimal(18,2),Sum(Final.QtyLtr))[Qty Ltr],Convert(Int,Sum(Final.[Pervious Qty]))[Pervious " + PreviousWise + " Qty],Convert(Decimal(18,2),Sum(Final.[Previous Qty Ltr]))[Previous " + PreviousWise + " Qty Ltr],Sum(Final.DocumentAmount) As [Amount],Sum(Final.[Previous Document Amount])[Previous " + PreviousWise + " Amount],Convert(Decimal(18,2),Convert(Int,(Sum(Final.[Pervious Qty])-Sum(Final.Qty)))) As [Grow Qty],Convert(Decimal(18,2),(Sum(Final.[Previous Qty Ltr])-Sum(Final.QtyLtr))) As [Grow Qty Ltr],(Sum(Final.[Previous Document Amount])-Sum(Final.DocumentAmount)) As [Grow Amount] from (" + BaseQry + ") Final Group By item_code Order By item_code"
            Else
                Qry = "Select "
                If rbtnDaily.Checked Then
                    PreviousWise = "Daily"
                    Qry += "'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "' As [Daily]"
                ElseIf rbtnWeekly.Checked Then
                    PreviousWise = "Weekly"
                    Qry += "'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "'+' - '+'" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "' As [Weekly]"
                Else
                    PreviousWise = "Monthly"
                    Qry += "'" + clsCommon.myCDate(fromDate.Value, "dd/MM/yyyy") + "'+' - '+'" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "' As [Monthly]"
                End If
                Qry += " , Final.[Route No],Convert(Int,Sum(Final.Qty)) As [Crate Qty],Convert(Decimal(18,2),Sum(Final.QtyLtr))[Qty Ltr],Convert(Int,Sum(Final.[Pervious Qty]))[Pervious " + PreviousWise + " Qty],Convert(Decimal(18,2),Sum(Final.[Previous Qty Ltr]))[Previous " + PreviousWise + " Qty Ltr],Sum(Final.DocumentAmount) As [Amount],Sum(Final.[Previous Document Amount])[Previous " + PreviousWise + " Amount],Convert(Decimal(18,2),Convert(Int,(Sum(Final.[Pervious Qty])-Sum(Final.Qty)))) As [Grow Qty],Convert(Decimal(18,2),(Sum(Final.[Previous Qty Ltr])-Sum(Final.QtyLtr))) As [Grow Qty Ltr],(Sum(Final.[Previous Document Amount])-Sum(Final.DocumentAmount)) As [Grow Amount] From (" + BaseQry + ") Final Group By [Route No] Order By [Route No]"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()
                Gv1.DataSource = dt
                SetGridFormat()
                Disable()
                dt = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found", Me.Text)
            End If
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
        Gv1.MasterTemplate.AutoExpandGroups = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item As New GridViewSummaryItem
        If Gv1.Rows IsNot Nothing AndAlso Gv1.Columns.Count > 0 Then
            If rbtnDetails.Checked Then
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    If ii > 2 Then
                        item = New GridViewSummaryItem(Gv1.Columns(ii).HeaderText, "{0:n2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item)
                    End If
                Next
            Else
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    If ii > 1 Then
                        item = New GridViewSummaryItem(Gv1.Columns(ii).HeaderText, "{0:n2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item)
                    End If
                Next
            End If
        End If
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Gv1.BestFitColumns()
    End Sub

    Public Function BaseQuery() As String
        Dim Qry As String = "(((select max(zzz.item_code) as item_code,Sum(DocumentAmount) as DocumentAmount,0 As [Previous Document Amount],max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,sum(qty) as Qty,0 As [Pervious Qty],sum(QtyLtr) as QtyLtr, 0 As [Previous Qty Ltr]
                            from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date, case when LTRIM(RIGHT(CONVERT(VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100), 7)) = '12:00AM' then LTRIM(RIGHT(CONVERT(VARCHAR(20),TSPL_BOOKING_MATSER.Created_Date , 100), 7)) else LTRIM(RIGHT(CONVERT(VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100), 7)) end  as [Time],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO],  TSPL_BOOKING_DETAIL.Booking_Qty  as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.Cust_Code as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code  Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  and ThisTableHead.Is_Taxable=1  and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=1 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No  ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No   Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No   ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where   Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No   left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code   where 2=2  And TSPL_ITEM_MASTER.Item_Type='F' And TSPL_ITEM_MASTER.Is_FreshItem='1' "
        Qry += " and convert(date, TSPL_BOOKING_MATSER.Document_Date,103) >= Convert(date,'" + fromDate.Value + "',103) and  convert(date, TSPL_BOOKING_MATSER.Document_Date,103) <= Convert(date,'" + ToDate.Value + "',103) "

        If txtZone.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtZone.arrValueMember) > 0 Then
            Qry += " and TSPL_CUSTOMER_MASTER.Zone_Code In (" & clsCommon.GetMulcallString(txtZone.arrValueMember) & ")"
        End If

        If TxtRoute.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(TxtRoute.arrValueMember) > 0 Then
            Qry += " and TSPL_BOOKING_DETAIL.Route_No In (" & clsCommon.GetMulcallString(TxtRoute.arrValueMember) & ")"
        End If

        If txtMultBooth.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtMultBooth.arrValueMember) > 0 Then
            Qry += " and TSPL_CUSTOMER_MASTER.Cust_Code In (" & clsCommon.GetMulcallString(txtMultBooth.arrValueMember) & ")"
        End If

        If txtMultDistributor.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtMultDistributor.arrValueMember) > 0 Then
            Qry += " and TSPL_CUSTOMER_MASTER.Cust_Code In (" & clsCommon.GetMulcallString(txtMultBooth.arrValueMember) & ")"
        End If

        If txtItemCode.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtItemCode.arrValueMember) > 0 Then
            Qry += " and TSPL_BOOKING_DETAIL.Item_Code In (" & clsCommon.GetMulcallString(txtItemCode.arrValueMember) & ")"
        End If

        Qry += ") zzz  where zzz.Scheme_Item='N'group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] ))
                    Union All
                 (select max(zzz.item_code) as item_code,0 As DocumentAmount,Sum(DocumentAmount) as [Previous Document Amount],max(Booth) as [Booth] , max([Customer Category Code]) as  [Customer Category Code], max([Customer Code]) as  [Customer Code],zzz.[VEHICLE NO],zzz.[WdName],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone]  ,zzz.[Route No] ,0 As Qty,sum(qty) as [Previous Qty],0 As QtyLtr ,sum(QtyLtr) as [Previous Qty Ltr] 
                from  (Select isnull(TSPL_BOOKING_MATSER.GatePass_Type,'') as GatePass_Type,TSPL_BOOKING_MATSER.Document_No, Convert (varchar,TSPL_BOOKING_MATSER.Document_Date,103) as Document_Date, case when LTRIM(RIGHT(CONVERT(VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100), 7)) = '12:00AM' then LTRIM(RIGHT(CONVERT(VARCHAR(20),TSPL_BOOKING_MATSER.Created_Date , 100), 7)) else LTRIM(RIGHT(CONVERT(VARCHAR(20), TSPL_BOOKING_MATSER.Document_Date, 100), 7)) end  as [Time],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No as [DO No],isnull(TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Short_Close,'N') as [Short Close],TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_MATSER.BookingThrough,Convert (varchar,TSPL_BOOKING_MATSER.TruckSheetGenerate) as TruckSheetGenerate , Convert (varchar,TSPL_BOOKING_MATSER.AgainstGatePass) as AgainstGatePass,Convert (varchar,TSPL_BOOKING_MATSER.is_Cancelled) as is_Cancelled,TSPL_BOOKING_MATSER.Payment_Mode,( format ( TSPL_BOOKING_MATSER.Created_Date, 'HH') +'.'+ format ( TSPL_BOOKING_MATSER.Created_Date, 'mm') ) as Created_Date_Time,TSPL_BOOKING_MATSER.Created_By,convert (varchar,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_MATSER.Modified_By, Convert (varchar,TSPL_BOOKING_MATSER.Modified_Date,103) as Modified_Date,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_BOOKING_MATSER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,isnull(TSPL_CUSTOMER_MASTER.cust_category_code,'') as [Customer Category Code], TSPL_BOOKING_DETAIL.Cust_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As WdName, TSPL_BOOKING_DETAIL.Item_Code as Item_Code,TSPL_BOOKING_DETAIL.Unit_code as UOM,TSPL_BOOKING_DETAIL.route_no as [Route No] , TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_VEHICLE_MASTER.Description [Lorry_No],TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,IsNull(TSPL_VEHICLE_MASTER.Description, '''') As [VEHICLE NO],  TSPL_BOOKING_DETAIL.Booking_Qty  as Qty, TSPL_BOOKING_MATSER.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc], TSPL_CUSTOMER_MASTER.Cust_Code as Booth,TBL_DISPATCH_INVOICE_NON_Taxable.[Dispatch_No] as [Dispatch No(NT)], TBL_DISPATCH_INVOICE_NON_Taxable.[Invoice_No] as [Invoice No(NT)],  TBL_DISPATCH_INVOICE_Taxable.[Dispatch_No] as  [Dispatch No(T)], TBL_DISPATCH_INVOICE_Taxable.[Invoice_No] as [Invoice No(T)],TBL_SCHEME_VALUE.Scheme_Booking_Qty,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_BOOKING_DETAIL.Vehicle_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_BOOKING_MATSER.location_code left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code  Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code  WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No  and ThisTableHead.Is_Taxable=1  and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=1 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =1  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No  ) TBL_DISPATCH_INVOICE_Taxable on TBL_DISPATCH_INVOICE_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No   Left Outer Join ( Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No,  STUFF((SELECT ', ' + QUOTENAME (TSPL_SD_SHIPMENT_DETAIL.Document_Code) FROM TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Dispatch_No],  STUFF((SELECT ', ' + QUOTENAME (ThisTableHead.Sale_Invoice_No)  FROM   TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ThisTableDetail  left outer join ( select distinct Document_code,delivery_Code from TSPL_SD_SHIPMENT_DETAIL ) as TSPL_SD_SHIPMENT_DETAIL on ThisTableDetail.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code left outer Join TSPL_SD_SHIPMENT_HEAD as ThisTableHead on ThisTableHead.Document_code = TSPL_SD_SHIPMENT_DETAIL .Document_code   WHERE TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = ThisTableDetail.Booking_No and ThisTableHead.Is_Taxable=0 and ThisTableDetail.Posted =1 FOR XML PATH ('')),1,2,'') AS [Invoice_No]   from TSPL_DELIVERY_NOTE_MASTER_FRESHSALE inner Join  TSPL_SD_SHIPMENT_DETAIL on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No =TSPL_SD_SHIPMENT_DETAIL.delivery_Code inner join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_code = TSPL_SD_SHIPMENT_DETAIL.Document_code    where TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Posted = 1 and TSPL_SD_SHIPMENT_HEAD.Is_Taxable =0  Group by TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No   ) TBL_DISPATCH_INVOICE_NON_Taxable on TBL_DISPATCH_INVOICE_NON_Taxable.Booking_No = TSPL_BOOKING_MATSER.Document_No   left Outer Join (Select TSPL_BOOKING_DETAIL.Document_No,Sum (isnull(Booking_Qty,0)) as Scheme_Booking_Qty  from TSPL_BOOKING_DETAIL where   Scheme_Item = 'Y' Group by TSPL_BOOKING_DETAIL.Document_No ) TBL_SCHEME_VALUE    On    TBL_SCHEME_VALUE.Document_No = TSPL_BOOKING_MATSER.Document_No  LEFT JOIN TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No   left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILCREATE on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILCREATE.Item_Code and TSPL_ITEM_UOM_DETAILCREATE.UOM_Code='CRATE'  left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code   where 2=2  And TSPL_ITEM_MASTER.Item_Type='F' And TSPL_ITEM_MASTER.Is_FreshItem='1'"
        Qry += " and convert(date, TSPL_BOOKING_MATSER.Document_Date,103) >= Convert(date,'" + PreviousFromDate + "',103) and  convert(date, TSPL_BOOKING_MATSER.Document_Date,103) < Convert(date,'" + PreviousToDate + "',103) "

        If txtZone.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtZone.arrValueMember) > 0 Then
            Qry += " and TSPL_CUSTOMER_MASTER.Zone_Code In (" & clsCommon.GetMulcallString(txtZone.arrValueMember) & ")"
        End If

        If TxtRoute.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(TxtRoute.arrValueMember) > 0 Then
            Qry += " and TSPL_BOOKING_DETAIL.Route_No In (" & clsCommon.GetMulcallString(TxtRoute.arrValueMember) & ")"
        End If

        If txtMultBooth.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtMultBooth.arrValueMember) > 0 Then
            Qry += " and TSPL_CUSTOMER_MASTER.Cust_Code In (" & clsCommon.GetMulcallString(txtMultBooth.arrValueMember) & ")"
        End If

        If txtMultDistributor.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtMultDistributor.arrValueMember) > 0 Then
            Qry += " and TSPL_CUSTOMER_MASTER.Cust_Code In (" & clsCommon.GetMulcallString(txtMultBooth.arrValueMember) & ")"
        End If

        If txtItemCode.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtItemCode.arrValueMember) > 0 Then
            Qry += " and TSPL_BOOKING_DETAIL.Item_Code In (" & clsCommon.GetMulcallString(txtItemCode.arrValueMember) & ")"
        End If

        Qry += ")zzz  where zzz.Scheme_Item='N'group by zzz.Document_No,zzz.[VEHICLE NO] ,zzz.WdName,zzz.Description,zzz.[Customer Category Code],zzz.Cust_Group_Code,zzz.Zone_Code,zzz.[Route No],zzz.[DO NO],zzz.[Short Close] ))"
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

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Enable()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            Export(EnumExportTo.Excel)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            Export(EnumExportTo.PDF)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBatchItemReport1 & "'"))
            'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            'If clsCommon.myLen(txtPaymentCycleCode.Value) > 0 Then
            '    arrHeader.Add("Cycle Code : " + txtPaymentCycleCode.Value)
            'End If
            'If clsCommon.myLen(txtMcc.Value) > 0 Then
            '    arrHeader.Add("Location : " + txtMcc.Value)
            'End If

            'If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            'End If
            'If txtVSP.arrDispalyMember IsNot Nothing AndAlso txtVSP.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("VSP : " + clsCommon.GetMulcallStringWithComma(txtVSP.arrDispalyMember))
            'End If
            'If txtDocumentNo.arrDispalyMember IsNot Nothing AndAlso txtDocumentNo.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Document : " + clsCommon.GetMulcallStringWithComma(txtDocumentNo.arrDispalyMember))
            'End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Sale Analysis Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                'clsCommon.MyExportToPDF("Sale Analysis Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                Dim style As New GridPrintStyle()
                'style.FitWidthMode = PrintFitWidthMode.FitPageWidth
                'style.PrintGrouping = True
                style.HeaderCellBackColor = Color.White
                style.GroupRowBackColor = Color.White
                style.SummaryCellBackColor = Color.White
                'style.PrintSummaries = False
                'style.PrintHeaderOnEachPage = True
                'style.PrintHiddenColumns = False
                Gv1.PrintStyle = style

                Dim doc As New clsMyPrintDocument()

                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 90
                doc.Landscape = True
                doc.AssociatedObject = Gv1

                'doc.DocumentName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTemporaryPaymentDeductionSummary & "'"))
                doc.DocumentName = objCommonVar.CurrentCompanyName
                'doc.MiddleHeader = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTemporaryPaymentDeductionSummary & "'"))
                'Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Name from TSPL_MCC_MASTER where MCC_Code='" + txtMCC.Value + "'"))
                doc.MiddleHeader = objCommonVar.CurrentCompanyName + Environment.NewLine + Environment.NewLine + "Sale Analysis Report" + Environment.NewLine

                'doc.MiddleHeader += " Balance Report of" + " " + strLocation + " " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
                doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Bold)

                'doc.LeftUpperText = "Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
                'doc.LeftUpperFont = New Font("Segoe UI", 8, FontStyle.Regular)

                doc.AssociatedObject = Gv1
                'doc.Print()
                doc.RightFooter = "Page [Page #] of [Total Pages]"

                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.Show(Me)
                doc = Nothing
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class