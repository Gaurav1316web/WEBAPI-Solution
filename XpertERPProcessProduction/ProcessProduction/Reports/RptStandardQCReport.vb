Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'================Created By Parteek-==============BHA/17/08/18-000447
'Ticket No-ERO/17/07/19-000952 ,Sanjay ,Add TS% AND TSKG
Public Class RptStandardQCReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Sub Print(ByVal IsPrint As Exporter)

        Dim Query As String = ""
        Dim dt As DataTable
        Dim counting As Integer = 0
        If clsCommon.GetDateWithEndTime(txtToDate.Value) < clsCommon.GetDateWithStartTime(txtFromDate.Value) Then
            clsCommon.MyMessageBoxShow("To Date cant be less than from date", Me.Text)
            Exit Sub
        End If

        If RbtnDetail.IsChecked Then
            Query = " select * from (select 'Standardization Final QC' as [Transaction Type],TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code as [Standardization Document No],TSPL_PP_STD_FINALQC_HEAD.QC_Code as [Document No],convert(date,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date,103) as [Document Date],TSPL_PP_STD_FINALQC_HEAD.Child_Batch_Code as [Batch No],TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code as  [Main Location],TSPL_LOCATION_MASTER.Location_Desc  as [Descrption] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code as [Location],l2.Location_Desc as [Location Desc] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code as [Item Code] ,TSPL_ITEM_MASTER.Item_Desc as [Item Description] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Unit_Code,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Quantity as [Total Qty],convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Avg_Cost/TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Quantity) as [Item Rate],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_FAT_per as [Fat %],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_FAT_KG as [Fat KG],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_SNF_per as [SNF %],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_SNF_KG as [SNF KG],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_FAT_per+TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_SNF_per as [TS %],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_FAT_KG+TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_SNF_KG as [TS KG],convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Fat_Rate) as [Fat Rate],convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.SNF_Rate) as [SNF Rate]"
            Query += " ,convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Fat_Amt) as [Fat Amount],convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.SNF_Amt) as [SNF Amount],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Avg_Cost as [Total Amount],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Quantity as [QC OK] ,0 as [QC Reject],0 as [QC ReProcess] ,TSPL_PP_STD_FINALQC_HEAD.Created_Date,case when TSPL_PP_STD_FINALQC_HEAD.Status='A' then 'Accept' end as [Status] "
            Query += " from TSPL_PP_STANDARDIZATION_HEAD left outer join TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code=TSPL_PP_STD_FINALQC_HEAD.Against_STD_Code left outer join TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.QC_Code=TSPL_PP_STD_FINALQC_HEAD.QC_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code left outer join TSPL_LOCATION_MASTER as L2 on L2.Location_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code  where 2=2 and TSPL_PP_STD_FINALQC_HEAD.Status='A'   and"
            Query += " TSPL_PP_STANDARDIZATION_HEAD.Modified_Date<TSPL_PP_STD_FINALQC_HEAD.Created_Date"

            Query += " union all  select 'Standardization Final QC' as [Transaction Type],TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code as [Standardization Document No],TSPL_PP_STD_FINALQC_HEAD.QC_Code as [Document No],TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date as [Document Date],TSPL_PP_STD_FINALQC_HEAD.Child_Batch_Code as [Batch No],TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code as [Location],l2.Location_Desc as [Location Desc] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc as [Item Description] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Unit_Code,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Quantity as [Total Qty],convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Avg_Cost/TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Quantity) as [Item Rate],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_FAT_per as [Fat %],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_FAT_KG as [Fat KG],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_SNF_per as [SNF %],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_SNF_KG as [SNF KG],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_FAT_per+TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_SNF_per as [TS %],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_FAT_KG+TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_SNF_KG as [TS KG],convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Fat_Rate) as [Fat Rate],convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.SNF_Rate) as [SNF Rate]"
            Query += " ,convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Fat_Amt) as [Fat Amount],convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.SNF_Amt) as [SNF Amount],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Avg_Cost as [Total Amount],0 as [QC OK],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Quantity as [QC Reject] ,0 as [QC ReProcess] ,TSPL_PP_STD_FINALQC_HEAD.Created_Date,case when TSPL_PP_STD_FINALQC_HEAD.Status='R' then 'Reject' end as [Status] "
            Query += "  from TSPL_PP_STANDARDIZATION_HEAD"
            Query += " left outer join TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code=TSPL_PP_STD_FINALQC_HEAD.Against_STD_Code left outer join TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.QC_Code=TSPL_PP_STD_FINALQC_HEAD.QC_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code left outer join TSPL_LOCATION_MASTER as L2 on L2.Location_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code  where 2=2 and TSPL_PP_STD_FINALQC_HEAD.Status='R' "
            Query += " and TSPL_PP_STANDARDIZATION_HEAD.Modified_Date<TSPL_PP_STD_FINALQC_HEAD.Created_Date"

            Query += " union all  select 'Standardization Final QC' as [Transaction Type],TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code as [Standardization Document No],TSPL_PP_STD_FINALQC_HEAD.QC_Code as [Document No],TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date as [Document Date],TSPL_PP_STD_FINALQC_HEAD.Child_Batch_Code as [Batch No],TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code as [Location],l2.Location_Desc as [Location Desc] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc as [Item Description] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Unit_Code,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Quantity as [Total Qty],convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Avg_Cost/TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Quantity) as [Item Rate],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_FAT_per as [Fat %],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_FAT_KG as [Fat KG],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_SNF_per as [SNF %],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_SNF_KG as [SNF KG],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_FAT_per+TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_SNF_per as [TS %],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_FAT_KG+TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Produced_SNF_KG as [TS KG],convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Fat_Rate) as [Fat Rate],convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.SNF_Rate) as [SNF Rate]"
            Query += " ,convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Fat_Amt) as [Fat Amount],convert(decimal(18,3),TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.SNF_Amt) as [SNF Amount],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Avg_Cost as [Total Amount],0 as [QC OK],0 as [QC Reject] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Quantity as [QC ReProcess]  ,TSPL_PP_STD_FINALQC_HEAD.Created_Date,case when TSPL_PP_STD_FINALQC_HEAD.Status='P' then 'Re-Process' end as [Status] "
            Query += "   from TSPL_PP_STANDARDIZATION_HEAD"
            Query += " left outer join TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code=TSPL_PP_STD_FINALQC_HEAD.Against_STD_Code left outer join TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.QC_Code=TSPL_PP_STD_FINALQC_HEAD.QC_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code left outer join TSPL_LOCATION_MASTER as L2 on L2.Location_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code  where 2=2 and TSPL_PP_STD_FINALQC_HEAD.Status='P'"
            Query += " and TSPL_PP_STANDARDIZATION_HEAD.Modified_Date<TSPL_PP_STD_FINALQC_HEAD.Created_Date"

            Query += " )Production where 2=2 "
            Query += " and convert(date,Production.[Document Date] ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Production.[Document Date] ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
            If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                Query += " and  Production.[Item Code] in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
            End If

            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                Query += " and Production.[Main Location] in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
            End If
            If txtSubLocation.arrValueMember IsNot Nothing AndAlso txtSubLocation.arrValueMember.Count > 0 Then
                Query += " and Production.[Location] in (" + clsCommon.GetMulcallString(txtSubLocation.arrValueMember) + ")  "
            End If
        Else
            Query = " select max(Production.Status) as Status,max(Production.[Main Location]) as [Main Location],max(Production.Descrption) as Descrption,max(Production.Location) as Location,max(Production.[Location Desc]) as [Location Desc],max(Production.[Item Code]) as [Item Code],max(Production.[Item Description]) as [Item Description],max(Production.Unit_Code) as [UOM],sum(Production.[QC OK]) as [QC OK],sum(Production.[QC Reject]) as  [QC Reject],sum(Production.[QC ReProcess]) as [QC ReProcess] from (select case when TSPL_PP_STD_FINALQC_HEAD.Status='A' then 'Approved' end as [Status],TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code as [Standardization Document No],TSPL_PP_STD_FINALQC_HEAD.QC_Code as [Document No],TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date as [Document Date],TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code as  [Main Location],TSPL_LOCATION_MASTER.Location_Desc  as [Descrption] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code as [Location],l2.Location_Desc as [Location Desc] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code as [Item Code] ,TSPL_ITEM_MASTER.Item_Desc as [Item Description] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Unit_Code,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Quantity as [QC OK] ,0 as [QC Reject],0 as [QC ReProcess] ,TSPL_PP_STD_FINALQC_HEAD.Created_Date "
            Query += " from TSPL_PP_STANDARDIZATION_HEAD left outer join TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code=TSPL_PP_STD_FINALQC_HEAD.Against_STD_Code left outer join TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.QC_Code=TSPL_PP_STD_FINALQC_HEAD.QC_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code left outer join TSPL_LOCATION_MASTER as L2 on L2.Location_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code  where 2=2 and TSPL_PP_STD_FINALQC_HEAD.Status='A'   and"
            Query += " TSPL_PP_STANDARDIZATION_HEAD.Modified_Date<TSPL_PP_STD_FINALQC_HEAD.Created_Date"

            Query += " union all  select case when TSPL_PP_STD_FINALQC_HEAD.Status='R' then 'Reject' end as [Status],TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code as [Standardization Document No],TSPL_PP_STD_FINALQC_HEAD.QC_Code as [Document No],TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date as [Document Date],TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code as [Location],l2.Location_Desc as [Location Desc] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc as [Item Description] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Unit_Code,0 as [QC OK],TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Quantity as [QC Reject] ,0 as [QC ReProcess] ,TSPL_PP_STD_FINALQC_HEAD.Created_Date "
            Query += "  from TSPL_PP_STANDARDIZATION_HEAD"
            Query += " left outer join TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code=TSPL_PP_STD_FINALQC_HEAD.Against_STD_Code left outer join TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.QC_Code=TSPL_PP_STD_FINALQC_HEAD.QC_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code left outer join TSPL_LOCATION_MASTER as L2 on L2.Location_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code  where 2=2 and TSPL_PP_STD_FINALQC_HEAD.Status='R' "
            Query += " and TSPL_PP_STANDARDIZATION_HEAD.Modified_Date<TSPL_PP_STD_FINALQC_HEAD.Created_Date"

            Query += " union all  select case when TSPL_PP_STD_FINALQC_HEAD.Status='P' then 'Re-Process' end as [Status],TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code as [Standardization Document No],TSPL_PP_STD_FINALQC_HEAD.QC_Code as [Document No],TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date as [Document Date],TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code as [Location],l2.Location_Desc as [Location Desc] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc as [Item Description] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Unit_Code,0 as [QC OK],0 as [QC Reject] ,TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Quantity as [QC ReProcess]  ,TSPL_PP_STD_FINALQC_HEAD.Created_Date "
            Query += "   from TSPL_PP_STANDARDIZATION_HEAD"
            Query += " left outer join TSPL_PP_STD_FINALQC_HEAD on TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code=TSPL_PP_STD_FINALQC_HEAD.Against_STD_Code left outer join TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL on TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.QC_Code=TSPL_PP_STD_FINALQC_HEAD.QC_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PP_STANDARDIZATION_HEAD.Loaction_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.Item_Code left outer join TSPL_LOCATION_MASTER as L2 on L2.Location_Code=TSPL_PP_STD_FINALQC_BATCH_ITEM_DETAIL.STD_Loaction_Code  where 2=2 and TSPL_PP_STD_FINALQC_HEAD.Status='P'"
            Query += " and TSPL_PP_STANDARDIZATION_HEAD.Modified_Date<TSPL_PP_STD_FINALQC_HEAD.Created_Date"

            Query += " )Production where 2=2"
            Query += " and convert(date,Production.[Document Date] ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Production.[Document Date] ,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
            If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                Query += " and  Production.[Item Code] in (" + clsCommon.GetMulcallString(txtItemMult.arrValueMember) + ")  "
            End If

            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                Query += " and Production.[Main Location] in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
            End If
            If txtSubLocation.arrValueMember IsNot Nothing AndAlso txtSubLocation.arrValueMember.Count > 0 Then
                Query += " and Production.[Location] in (" + clsCommon.GetMulcallString(txtSubLocation.arrValueMember) + ")  "
            End If
            Query += " group by Production.[Item Code],Production.Location"

        End If


        dt = clsDBFuncationality.GetDataTable(Query)

        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False

            gv1.EnableFiltering = True



            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If

        gv1.DataSource = dt
        SetGridFormationOFGV1()
        gv1.BestFitColumns()
        'FindAndRestoreGridLayout(Me)
        ReStoreGridLayout()

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
            MessageBox.Show(err.Message)
        End Try
    End Sub
    
    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).BestFit()
        Next

        gv1.Columns("Status").IsVisible = False

        If RbtnDetail.IsChecked = True Then
            gv1.Columns("Standardization Document No").IsVisible = False
            gv1.Columns("Created_Date").IsVisible = False
            gv1.Columns("Status").IsVisible = True
        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("QC OK", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("QC Reject", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("QC ReProcess", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        txtItemMult.arrValueMember = Nothing
        TxtMultiLocation.arrValueMember = Nothing
        txtSubLocation.arrValueMember = Nothing
        RbtnSummary.IsChecked = True
    End Sub
    Private Sub txtItemMult__My_Click(sender As Object, e As EventArgs) Handles txtItemMult._My_Click
        Dim qry As String = " select TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name from TSPL_ITEM_MASTER "
        txtItemMult.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultItemNo", qry, "Code", "Name", txtItemMult.arrValueMember, txtItemMult.arrDispalyMember)
    End Sub

    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Pro", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
    End Sub
    Private Sub txtSubLocation__My_Click(sender As Object, e As EventArgs) Handles txtSubLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        txtSubLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ProSub", qry, "Code", "Name", txtSubLocation.arrValueMember, txtSubLocation.arrDispalyMember)
    End Sub


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetReportGridID()
        PageSetupReport_ID = MyBase.Form_ID + IIf(RbtnSummary.IsChecked = True, "S", "D")
        TemplateGridview = gv1
        Print(Exporter.Refresh)
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If RbtnDetail.IsChecked = True Then
            VarID += "_DE"
        Else
            RbtnSummary.IsChecked = True
            VarID += "_SU"
        End If
        gv1.VarID = VarID
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rptAvailableQtyForProduction_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Private Sub rptAvailableQtyForProduction_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                arrHeader.Add(strtemp)

                If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
                End If

                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
                End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Standardization QC Report", gv1, arrHeader, Me.Text)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                Dim strtemp As String = "Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
                arrHeader.Add(strtemp)

                If txtItemMult.arrValueMember IsNot Nothing AndAlso txtItemMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtItemMult.arrDispalyMember))
                End If

                If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
                End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Standardization QC Report", gv1, arrHeader, "Standardization QC Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class
