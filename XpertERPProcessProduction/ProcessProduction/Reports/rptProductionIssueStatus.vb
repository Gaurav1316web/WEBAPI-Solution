Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'================Created By Preeti Gupta-==============Ticket No:BM00000009236
'Ticket No-No  ERO/09/07/19-000677 ,Sanjay Add TSKG,TS%
Public Class RptProductionIssueStatus
    Dim arrBack As New List(Of String)
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrBatchNo As ArrayList
    Dim ArrLocation As ArrayList
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptProductionIssueStatusReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub

    Sub Print(ByVal IsPrint As Exporter)
        ' KUNAL > KDIL > 21-DEC-2016 > FIXED WITHOUT TICKETS 
        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")

        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
        If txtmultBatchNo.arrDispalyMember IsNot Nothing AndAlso txtmultBatchNo.arrDispalyMember.Count > 0 Then
            arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtmultBatchNo.arrDispalyMember))
        End If
        If txtMultItemNo.arrDispalyMember IsNot Nothing AndAlso txtMultItemNo.arrDispalyMember.Count > 0 Then
            arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtMultItemNo.arrDispalyMember))
        End If

        ArrLocation = GetLocation()

        Dim qry As String = Nothing
        ' Change by Prabhakar Ticket Ref : BM00000009236 , BHA/16/07/18-000170 By Prabhakar For Manual Batch No
        Dim Baseqry As String = "select  TSPL_PP_BATCH_ORDER_HEAD.Batch_Code,convert(date,TSPL_PP_BATCH_ORDER_HEAD.Batch_Date,103) as Batch_Date,TSPL_PP_BATCH_ORDER_HEAD.ManualBatchNo ,TSPL_PP_ISSUE_HEAD.Issue_Code,convert(date,TSPL_PP_ISSUE_HEAD.Issue_Date,103) as Issue_Date, "
        Baseqry += " TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code,tspl_item_master.Item_Desc , TSPL_PP_ISSUE_HEAD.Main_Location_Code, (select Location_Desc from  TSPL_LOCATION_MASTER where 1=1 and Location_Code in (TSPL_PP_ISSUE_HEAD.Main_Location_Code)) AS [Main Loc Desc], tspl_pp_issue_item_detail.From_Loaction_Code ,  (select Location_Desc from  TSPL_LOCATION_MASTER where 1=1 and Location_Code in  (tspl_pp_issue_item_detail.From_Loaction_Code)) AS [From Loc Desc],  tspl_pp_issue_item_detail.To_Location_Code, (select Location_Desc from  TSPL_LOCATION_MASTER where 1=1 and Location_Code in (tspl_pp_issue_item_detail.To_Location_Code)) AS [To Loc Desc], "
        If chk_stockingunit.Checked Then
            Baseqry += " ((isnull(unitconvraw.Conversion_Factor,1)*isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity,0))/isnull(stockunitconv.Conversion_Factor,1)) as Required_Qty,"
            Baseqry += " TSPL_PP_ISSUE_HEAD.Section_Code, "
            Baseqry += " ((isnull(unitconv.Conversion_Factor,1)*TSPL_PP_ISSUE_ITEM_DETAIL.qty)/isnull(stockunitconv.Conversion_Factor,1)) as Issue_Qty,stockunitconv.uom_code as Unit_Code, "
        Else
            Baseqry += " isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity,0)  as Required_Qty,"
            Baseqry += " TSPL_PP_ISSUE_HEAD.Section_Code, "
            Baseqry += " TSPL_PP_ISSUE_ITEM_DETAIL.qty as Issue_Qty,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_Code, "
        End If

        'If chkWithBatch.IsChecked Then
        '    Baseqry += "    TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity  as Required_Qty,"
        'Else
        '    Baseqry += "   0 as Required_Qty,"
        'End If
        'Baseqry += " case when isnull(TSPL_PP_ISSUE_HEAD.Batch_Code,'') ='' then '0' else TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity end as Required_Qty "
        'Baseqry += " TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Pers ,TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Pers,isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.FAT_KG,0) as Required_FAT_KG,isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.SNF_KG,0) as Required_SNF_KG ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG,0) as Issued_FAT_KG,ISNULL( TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG,0) as Issued_SNF_KG,TSPL_PP_ISSUE_HEAD.Status  from TSPL_PP_ISSUE_HEAD"
        Baseqry += " TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Pers ,TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Pers, TSPL_PP_ISSUE_ITEM_DETAIL.FAT_Pers+TSPL_PP_ISSUE_ITEM_DETAIL.SNF_Pers AS TS_Pers,isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.FAT_KG,0) as Required_FAT_KG,isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.SNF_KG,0) as Required_SNF_KG,isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.FAT_KG,0) + isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.SNF_KG,0) as Required_TS_KG ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG,0) as Issued_FAT_KG,ISNULL( TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG,0) as Issued_SNF_KG,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.FAT_KG,0) +ISNULL( TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG,0) as Issued_TS_KG,TSPL_PP_ISSUE_HEAD.Status  from TSPL_PP_ISSUE_HEAD"
        Baseqry += " left join TSPL_PP_ISSUE_ITEM_DETAIL on TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code =TSPL_PP_ISSUE_HEAD.Issue_Code "
        Baseqry += " left join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.Batch_Code =TSPL_PP_ISSUE_HEAD.Batch_Code "
        'If chkWithBatch.IsChecked Then
        Baseqry += " left join TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL on TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code =TSPL_PP_BATCH_ORDER_HEAD.Batch_Code and TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code = TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code  "
        'End If
        Baseqry += " left join tspl_item_master on tspl_item_master.Item_Code =TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code"
        Baseqry += " left join tspl_location_master on tspl_location_master.Location_Code =TSPL_PP_ISSUE_HEAD.From_Loaction_Code "
        If chk_stockingunit.Checked Then
            Baseqry += " left join (select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code) as stockunitconv on stockunitconv.Item_Code=TSPL_PP_ISSUE_ITEM_DETAIL.ITEM_CODE   "
            Baseqry += " left join TSPL_ITEM_UOM_DETAIL unitconv on unitconv.Item_Code=TSPL_PP_ISSUE_ITEM_DETAIL.ITEM_CODE and unitconv.UOM_Code=TSPL_PP_ISSUE_ITEM_DETAIL.unit_code"
            Baseqry += " left join TSPL_ITEM_UOM_DETAIL unitconvraw on unitconvraw.Item_Code=TSPL_PP_ISSUE_ITEM_DETAIL.ITEM_CODE and unitconvraw.UOM_Code=TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.unit_code"
        End If
        Baseqry += " where 2=2  "
        If ChkWithoutBatch.IsChecked Then
            Baseqry += " and TSPL_PP_BATCH_ORDER_HEAD.Batch_Code is null"
        ElseIf chkWithBatch.IsChecked Then
            Baseqry += " and TSPL_PP_BATCH_ORDER_HEAD.Batch_Code is not null"
        End If

        Baseqry += "  and convert(date, TSPL_PP_ISSUE_HEAD.Issue_Date,103)>=convert(date,'" + txtfromDate.Value + "',103) and convert(date, TSPL_PP_ISSUE_HEAD.Issue_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If txtmultBatchNo.arrValueMember IsNot Nothing AndAlso txtmultBatchNo.arrValueMember.Count > 0 Then
            Baseqry += " and  TSPL_PP_BATCH_ORDER_HEAD.Batch_Code in (" + clsCommon.GetMulcallString(txtmultBatchNo.arrValueMember) + ")  "
        End If
        If txtMultItemNo.arrValueMember IsNot Nothing AndAlso txtMultItemNo.arrValueMember.Count > 0 Then
            Baseqry += " and  TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(txtMultItemNo.arrValueMember) + ")  "
        End If
        'If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
        '    Baseqry += " and  TSPL_PP_ISSUE_HEAD.From_Loaction_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ")  "
        'End If
        If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
            Baseqry += " and tspl_pp_issue_item_detail.From_Loaction_Code in (" + clsCommon.GetMulcallString(ArrLocation) + ")  "
        End If
        ' Change by Prabhakar Ticket Ref : BM00000009236  
        If rbtnDetail.IsChecked Then
            qry = "" & Baseqry & ""
            qry += " order by convert(date,Issue_Date,103)  , issue_code "
        End If
        If rbtnSummary.IsChecked Then
            qry = "select  final.Batch_Code,max(final.Batch_Date) as Batch_Date  ,max(final.Issue_Code ) as Issue_Code,convert(varchar, max(final.Issue_Date),103) as Issue_Date,(final.Item_Code) as Item_Code,max(final.Item_Desc) as Item_Desc,From_Loaction_Code,max(Location_Desc) as Location_Desc,max(final.Required_Qty) as Required_Qty,convert(varchar, max(final.Issue_Date),103) as Issue_Date,convert(decimal(18,2),max(final.Required_FAT_KG)) as Required_FAT_KG,convert(decimal(18,2),max(final.Required_SNF_KG)) as Required_SNF_KG,convert(decimal(18,2), case when max(final.Required_Qty)=0 then 0 else max(final.Required_FAT_KG)*100/max(final.Required_Qty) end) as Required_FAT_Per,convert(decimal(18,2),case when max(final.Required_Qty)=0 then 0 else max(final.Required_SNF_KG)*100/max(final.Required_Qty) end) as Required_SNF_Per,max(final.Section_Code ) Section_Code,sum(final.Issue_Qty ) as Issue_Qty,convert(decimal(18,2),sum(final.Issued_FAT_KG)) as Issued_FAT_KG,convert(decimal(18,2),sum(final.Issued_SNF_KG)) as Issued_SNF_KG,convert(decimal(18,2),case when sum(final.Issue_Qty)=0 then 0 else sum(final.Issued_FAT_KG)*100/sum(final.Issue_Qty) end) as Issued_FAT_Per,convert(decimal(18,2),case when sum(final.Issue_Qty)=0 then 0 else sum(final.Issued_SNF_KG)*100/sum(final.Issue_Qty) end) as Issued_SNF_Per,convert(Decimal(18,2),max(final.Required_Qty)- sum(final.Issue_Qty ))as Pending,convert(decimal(18,2),max(final.Required_FAT_KG)- sum(final.Issued_FAT_KG ))  as Pending_FAT_KG,convert(decimal(18,2), max(final.Required_SNF_KG)- sum(final.Issued_SNF_KG ))  as Pending_SNF_KG,convert(decimal(18,2),case when convert(decimal(18,2),max(final.Required_Qty)- sum(final.Issue_Qty ))=0 then 0 else (max(final.Required_FAT_KG)- sum(final.Issued_FAT_KG ))*100/(max(final.Required_Qty)- sum(final.Issue_Qty )) end) as Pending_FAT_Per,convert(decimal(18,2),case when convert(decimal(18,2),max(final.Required_Qty)- sum(final.Issue_Qty ))=0 then 0 else (max(final.Required_SNF_KG)- sum(final.Issued_SNF_KG ))*100/(max(final.Required_Qty)- sum(final.Issue_Qty )) end) as Pending_SNF_Per,max(Status ) as Status,max(Unit_Code) as Unit_Code from ("
            qry += "" & Baseqry & ""
            qry += " )as final  group by final.Batch_Code,Item_Code,From_Loaction_Code  "
            qry += " order by convert(date,max(Batch_Date),103) "
        End If





        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

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
        chk_stockingunit.Enabled = False
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
            gv1.Columns(ii).IsVisible = False
        Next

        gv1.Columns("Batch_Code").Width = 100
        gv1.Columns("Batch_Code").IsVisible = True
        gv1.Columns("Batch_Code").HeaderText = "Batch Code"

        gv1.Columns("Item_Code").Width = 100
        gv1.Columns("Item_Code").IsVisible = True
        gv1.Columns("Item_Code").HeaderText = "Item Code"

        gv1.Columns("ManualBatchNo").Width = 100
        gv1.Columns("ManualBatchNo").IsVisible = True
        gv1.Columns("ManualBatchNo").HeaderText = "Manual Batch No"

        gv1.Columns("Item_Desc").Width = 100
        gv1.Columns("Item_Desc").IsVisible = True
        gv1.Columns("Item_Desc").HeaderText = "Item Name"

        gv1.Columns("Required_Qty").Width = 100
        gv1.Columns("Required_Qty").IsVisible = True
        gv1.Columns("Required_Qty").HeaderText = "Batch Required Qty"

        gv1.Columns("Issue_Qty").Width = 100
        gv1.Columns("Issue_Qty").IsVisible = True
        gv1.Columns("Issue_Qty").HeaderText = "Issue Qty"

        gv1.Columns("Unit_Code").Width = 100
        gv1.Columns("Unit_Code").IsVisible = True
        gv1.Columns("Unit_Code").HeaderText = "Unit Code"
        ' kunal
        ' main location ----
        gv1.Columns("main_location_code").Width = 100
        gv1.Columns("main_location_code").IsVisible = True
        gv1.Columns("main_location_code").HeaderText = "Main Loc Code"

        gv1.Columns("Main Loc Desc").Width = 100
        gv1.Columns("Main Loc Desc").IsVisible = True
        gv1.Columns("Main Loc Desc").HeaderText = "Main Loc Desc"

        ' from  location ----
        gv1.Columns("from_loaction_code").Width = 100
        gv1.Columns("from_loaction_code").IsVisible = True
        gv1.Columns("from_loaction_code").HeaderText = "From Location Code"

        gv1.Columns("From Loc Desc").Width = 100
        gv1.Columns("From Loc Desc").IsVisible = True
        gv1.Columns("From Loc Desc").HeaderText = "From Loc Desc"

        ' to  location ----
        gv1.Columns("to_location_code").Width = 100
        gv1.Columns("to_location_code").IsVisible = True
        gv1.Columns("to_location_code").HeaderText = "To Loc Code"


        gv1.Columns("To Loc Desc").Width = 100
        gv1.Columns("To Loc Desc").IsVisible = True
        gv1.Columns("To Loc Desc").HeaderText = "To Loc Desc"

        'end


        'gv1.Columns("Location_Desc").Width = 100
        'gv1.Columns("Location_Desc").IsVisible = True
        'gv1.Columns("Location_Desc").HeaderText = "Location Name"

        gv1.Columns("Batch_Date").Width = 100
        gv1.Columns("Batch_Date").IsVisible = True
        gv1.Columns("Batch_Date").HeaderText = "Batch Date"
        gv1.Columns("Batch_Date").FormatString = "{0:d}"

        gv1.Columns("Section_Code").Width = 100
        gv1.Columns("Section_Code").IsVisible = True
        gv1.Columns("Section_Code").HeaderText = "Section Code"

        gv1.Columns("Issue_Code").Width = 100
        gv1.Columns("Issue_Code").IsVisible = True
        gv1.Columns("Issue_Code").HeaderText = "Issue Code"

        gv1.Columns("Issue_Date").Width = 100
        gv1.Columns("Issue_Date").IsVisible = True
        gv1.Columns("Issue_Date").HeaderText = "Issue Date"
        gv1.Columns("Issue_Date").FormatString = "{0:d}"

        If rbtnSummary.IsChecked Then
            gv1.Columns("Pending").Width = 100
            gv1.Columns("Pending").IsVisible = True
            gv1.Columns("Pending").HeaderText = "Pending"

            gv1.Columns("Status").Width = 100
            gv1.Columns("Status").IsVisible = True
            gv1.Columns("Status").HeaderText = "Status"

            gv1.Columns("Required_FAT_KG").Width = 100
            gv1.Columns("Required_FAT_KG").IsVisible = True
            gv1.Columns("Required_FAT_KG").HeaderText = "Required FAT KG"

            gv1.Columns("Required_SNF_KG").Width = 100
            gv1.Columns("Required_SNF_KG").IsVisible = True
            gv1.Columns("Required_SNF_KG").HeaderText = "Required SNF KG"

            gv1.Columns("Required_FAT_Per").Width = 100
            gv1.Columns("Required_FAT_Per").IsVisible = True
            gv1.Columns("Required_FAT_Per").HeaderText = "Required FAT %"

            gv1.Columns("Required_SNF_Per").Width = 100
            gv1.Columns("Required_SNF_Per").IsVisible = True
            gv1.Columns("Required_SNF_Per").HeaderText = "Required SNF %"

            gv1.Columns("Issued_FAT_KG").Width = 100
            gv1.Columns("Issued_FAT_KG").IsVisible = True
            gv1.Columns("Issued_FAT_KG").HeaderText = "Issued FAT KG"

            gv1.Columns("Issued_SNF_KG").Width = 100
            gv1.Columns("Issued_SNF_KG").IsVisible = True
            gv1.Columns("Issued_SNF_KG").HeaderText = "Issued SNF KG"

            gv1.Columns("Issued_FAT_Per").Width = 100
            gv1.Columns("Issued_FAT_Per").IsVisible = True
            gv1.Columns("Issued_FAT_Per").HeaderText = "Issued FAT %"

            gv1.Columns("Issued_SNF_Per").Width = 100
            gv1.Columns("Issued_SNF_Per").IsVisible = True
            gv1.Columns("Issued_SNF_Per").HeaderText = "Issued SNF %"

            gv1.Columns("Pending_FAT_KG").Width = 100
            gv1.Columns("Pending_FAT_KG").IsVisible = True
            gv1.Columns("Pending_FAT_KG").HeaderText = "Pending FAT KG"

            gv1.Columns("Pending_SNF_KG").Width = 100
            gv1.Columns("Pending_SNF_KG").IsVisible = True
            gv1.Columns("Pending_SNF_KG").HeaderText = "Pending SNF KG"

            gv1.Columns("Pending_FAT_Per").Width = 100
            gv1.Columns("Pending_FAT_Per").IsVisible = True
            gv1.Columns("Pending_FAT_Per").HeaderText = "Pending FAT %"

            gv1.Columns("Pending_SNF_Per").Width = 100
            gv1.Columns("Pending_SNF_Per").IsVisible = True
            gv1.Columns("Pending_SNF_Per").HeaderText = "Pending SNF %"

            gv1.Columns("Required_TS_Pers").Width = 100
            gv1.Columns("Required_TS_Pers").IsVisible = True
            gv1.Columns("Required_TS_Pers").HeaderText = "Required TS %"
            gv1.Columns("Required_TS_KG").Width = 100
            gv1.Columns("Required_TS_KG").IsVisible = True
            gv1.Columns("Required_TS_KG").HeaderText = "Required TS KG"

            gv1.Columns("Issued_TS_Pers").Width = 100
            gv1.Columns("Issued_TS_Pers").IsVisible = True
            gv1.Columns("Issued_TS_Pers").HeaderText = "Issued TS %"
            gv1.Columns("Issued_TS_KG").Width = 100
            gv1.Columns("Issued_TS_KG").IsVisible = True
            gv1.Columns("Issued_TS_KG").HeaderText = "Issued TS KG"

            gv1.Columns("Pending_TS_Pers").Width = 100
            gv1.Columns("Pending_TS_Pers").IsVisible = True
            gv1.Columns("Pending_TS_Pers").HeaderText = "Pending TS %"
            gv1.Columns("Pending_TS_KG").Width = 100
            gv1.Columns("Pending_TS_KG").IsVisible = True
            gv1.Columns("Pending_TS_KG").HeaderText = "Pending TS KG"

        Else
            gv1.Columns("FAT_Pers").Width = 100
            gv1.Columns("FAT_Pers").IsVisible = True
            gv1.Columns("FAT_Pers").HeaderText = "FAT %"

            gv1.Columns("SNF_Pers").Width = 100
            gv1.Columns("SNF_Pers").IsVisible = True
            gv1.Columns("SNF_Pers").HeaderText = "SNF %"

            gv1.Columns("Issued_FAT_KG").Width = 100
            gv1.Columns("Issued_FAT_KG").IsVisible = True
            gv1.Columns("Issued_FAT_KG").HeaderText = "FAT KG"

            gv1.Columns("Issued_SNF_KG").Width = 100
            gv1.Columns("Issued_SNF_KG").IsVisible = True
            gv1.Columns("Issued_SNF_KG").HeaderText = "SNF KG"

            gv1.Columns("TS_Pers").Width = 100
            gv1.Columns("TS_Pers").IsVisible = True
            gv1.Columns("TS_Pers").HeaderText = "TS %"
            gv1.Columns("Issued_TS_KG").Width = 100
            gv1.Columns("Issued_TS_KG").IsVisible = True
            gv1.Columns("Issued_TS_KG").HeaderText = "TS KG"

        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim item1 As New GridViewSummaryItem("Stock_Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem("Cost", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        'ReStoreGridLayout()
    End Sub

    Sub Reset()
        Try
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtfromDate.Value = txtToDate.Value.AddMonths(-1)
            LoadLocation()
            gv1.DataSource = Nothing
            rbtnSummary.IsChecked = True
            chkAll.IsChecked = True
            RadPageView1.SelectedPage = RadPageViewPage1
            txtmultBatchNo.arrValueMember = Nothing
            TxtMultiLocation.arrValueMember = Nothing
            txtMultItemNo.arrValueMember = Nothing
            rbtnSummary.IsChecked = False
            rbtnDetail.IsChecked = True
            chk_stockingunit.Enabled = True
            chk_stockingunit.Checked = False
        Catch ex As Exception

        End Try

    End Sub

    Sub LoadLocation()
        gvLocation.DataSource = Nothing
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME,case when Is_Jobwork=1 then 'Yes' else 'No' end as [Job Location] from TSPL_LOCATION_MASTER where 1=1 and ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') ) "
        qry += " order by Location_Code"
        gvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvLocation.Columns("SEL").ReadOnly = False
        gvLocation.Columns("SEL").Width = 30
        gvLocation.Columns("SEL").HeaderText = " "

        gvLocation.Columns("CODE").ReadOnly = True
        gvLocation.Columns("CODE").Width = 100
        gvLocation.Columns("CODE").HeaderText = "Code"

        gvLocation.Columns("NAME").ReadOnly = True
        gvLocation.Columns("NAME").Width = 200
        gvLocation.Columns("NAME").HeaderText = "Description"

        gvLocation.ShowGroupPanel = False
        gvLocation.AllowAddNewRow = False
        gvLocation.AllowColumnReorder = False
        gvLocation.AllowRowReorder = False
        gvLocation.EnableSorting = False
        gvLocation.ShowFilteringRow = True
        gvLocation.EnableFiltering = True
        gvLocation.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvLocation.MasterTemplate.ShowRowHeaderColumn = True
    End Sub
    Sub DrillDown()
        Try
            If rbtnSummary.IsChecked Then
                If Not arrBack.Contains("Summary") Then
                    arrBack.Add("Summary")
                End If
                rbtnDetail.IsChecked = True
                arrBatchNo = New ArrayList()
                arrBatchNo = txtmultBatchNo.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Batch_Code").Value))
                txtmultBatchNo.arrValueMember = tmp
                Print(Exporter.Refresh)


            ElseIf rbtnDetail.IsChecked Then

                Dim strTransCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Issue_Code").Value)
                'If clsCommon.myLen(strTransCode) > 0 Then
                '    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.product, strTransCode)
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RptProductionIssueStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
            rbtnDetail.CheckState = CheckState.Checked
            rbtnSummary.CheckState = CheckState.Unchecked
            chkWithBatch.CheckState = CheckState.Checked
            ButtonToolTip.SetToolTip(btnReset, "Pres%s Alt+N Adding New")
            txtmultBatchNo.arrValueMember = arrBatchNo
            Reset()
            rbtnLocationAll.IsChecked = True
            rbtnSummary.IsChecked = False
            rbtnDetail.IsChecked = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RptProductionIssueStatus_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub



    Private Sub txtmultBatchNo__My_Click(sender As Object, e As EventArgs) Handles txtmultBatchNo._My_Click
        Dim qry As String = " select TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as Code,TSPL_PP_BATCH_ORDER_HEAD.Batch_Code as Name  from TSPL_PP_BATCH_ORDER_HEAD "
        txtmultBatchNo.arrValueMember = clsCommon.ShowMultipleSelectForm("multBatchNo", qry, "Code", "Name", txtmultBatchNo.arrValueMember, txtmultBatchNo.arrDispalyMember)

    End Sub

    Private Sub txtMultItemNo__My_Click(sender As Object, e As EventArgs) Handles txtMultItemNo._My_Click
        Dim qry As String = " select TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name from TSPL_ITEM_MASTER "
        txtMultItemNo.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultItemNo", qry, "Code", "Name", txtMultItemNo.arrValueMember, txtMultItemNo.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
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

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If rbtnDetail.IsChecked Then
                arrBack.Remove("Summary")
                txtmultBatchNo.arrValueMember = arrBatchNo
                rbtnSummary.IsChecked = True
                Print(Exporter.Refresh)
            Else
                RadPageView1.SelectedPage = RadPageViewPage1
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        DrillDown()
    End Sub

    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER  "
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Pro", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptProductionIssueStatusReport & "'"))


            If txtmultBatchNo.arrValueMember IsNot Nothing AndAlso txtmultBatchNo.arrValueMember.Count > 0 Then
                arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtmultBatchNo.arrDispalyMember))
            End If
            If txtMultItemNo.arrValueMember IsNot Nothing AndAlso txtMultItemNo.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtMultItemNo.arrDispalyMember))
            End If
            'If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
            'End If
            If rbtnLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add(" Location : " + strLoca)
            End If

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
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptProductionIssueStatusReport & "'"))


            If txtmultBatchNo.arrValueMember IsNot Nothing AndAlso txtmultBatchNo.arrValueMember.Count > 0 Then
                arrHeader.Add(" Batch No : " + clsCommon.GetMulcallStringWithComma(txtmultBatchNo.arrDispalyMember))
            End If
            If txtMultItemNo.arrValueMember IsNot Nothing AndAlso txtMultItemNo.arrValueMember.Count > 0 Then
                arrHeader.Add(" Item : " + clsCommon.GetMulcallStringWithComma(txtMultItemNo.arrDispalyMember))
            End If
            'If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(TxtMultiLocation.arrDispalyMember))
            'End If
            If rbtnLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add(" Location : " + strLoca)
            End If
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Production Issue Status", gv1, arrHeader, "Production Issue Status", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function GetLocation() As ArrayList
        Dim strWhrCatg As String = ""
        Dim qry As String
        Dim arrLocation As ArrayList = Nothing
        If rbtnLocationSelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvLocation.RowCount - 1
                If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
                    If IsApplicable Then
                        strWhrCatg += " Or "
                    End If
                    strWhrCatg += " ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "') "
                    IsApplicable = True
                    Dim arr As Dictionary(Of String, Object) = gvLocation.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        strWhrCatg += " and Location_Code in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                    End If
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one location")
            End If
            qry = "select Location_Code from TSPL_LOCATION_MASTER where 2=2 and (" + strWhrCatg + ")"
            Dim dtLoc As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dtLoc IsNot Nothing AndAlso dtLoc.Rows.Count > 0 Then
                arrLocation = New ArrayList
                For Each dr As DataRow In dtLoc.Rows
                    arrLocation.Add(dr("Location_Code"))
                Next
            End If
        End If
        Return arrLocation
    End Function

    Private Sub GvLocation_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvLocation.CellDoubleClick
        If clsCommon.myCBool(gvLocation.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 3
            frm.strCode = clsCommon.myCstr(gvLocation.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvLocation.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvLocation.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub

    Private Sub RbtnLocationAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub RbtnLocationSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationSelect.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells("SEL").Value = True
        Next
    End Sub

    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        CheckedAll(gvLocation)
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        UnCheckedAll(gvLocation)
    End Sub
End Class
