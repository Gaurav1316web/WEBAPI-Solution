Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Public Class FrmInventoryAgeingReport
    Inherits FrmMainTranScreen
#Region "variables"
    Dim isDataLoad As Boolean = False
    Public asofdt As Date
    Public cuttoff As Date
    Public strType As String
    Public arrLocation As ArrayList
    Public arrItem As ArrayList
    Public arrCategory As Array
#End Region
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmInventoryAgeingReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        radbtnBulkExp.Visible = MyBase.isQuickExportFlag
    End Sub
    Private Sub FrmInventoryAgeingReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        txtFromDate.Text = clsCommon.GETSERVERDATE()
        txtToDate.Text = clsCommon.GETSERVERDATE()
        cboType.Text = "Detail"
        LoadLocation()
        LoadTransactionType()
        LoadBucketType()
        LoadFatSnf()
        cbFatSnf.SelectedIndex = 0
        cbType.SelectedIndex = 0
        rbtnLocationAll.IsChecked = True
        radbtnBulkExp.Visible = False
    End Sub

    Sub LoadLocation()
        gvLocation.DataSource = Nothing

        ''===============when WIP then only section/sublocation seen in location finders=====24/03/2017==========================
        Dim whrCls As String = " and ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') )"

        ''====================================================================================================================

        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where 1=1 " + whrCls + ""

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

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        'txtFromDate.Text = clsCommon.GETSERVERDATE()
        'txtToDate.Text = clsCommon.GETSERVERDATE()

        'LoadTransactionType()
        'LoadBucketType()
        'cboType.Text = "Detail"
        'cbFatSnf.SelectedIndex = 0
        'cbType.SelectedIndex = 0
        'txtLocation.arrValueMember = Nothing
        'txtLocation.arrDispalyMember = Nothing

        'txtItem.arrValueMember = Nothing
        'txtItem.arrDispalyMember = Nothing
        'gv1.DataSource = Nothing
        'gv1.Columns.Clear()
        'gv1.Rows.Clear()
        'gv1.GroupDescriptors.Clear()
        'gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadGroupBox3.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage3
    End Sub
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmInventoryAgeingReport & "'"))

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click

        GetDataNew()

    End Sub
    Public Sub GetDataNew()
        Try

            If txtFromDate.Value > txtToDate.Value Then
                clsCommon.MyMessageBoxShow("Cuttoff Date cant be greater than As on date", Me.Text)
                Return
            End If
            Dim dtLoc As New DataTable()

            clsCommon.ProgressBarShow()
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim Location As String = String.Empty
            Dim objFilter As New clsStockRecoFilters
            objFilter.arrLocation = New List(Of clsCode)
            If rbtnLocationSelect.IsChecked = True Then
                For ii As Integer = 0 To gvLocation.Rows.Count - 1
                    If clsCommon.myCBool(gvLocation.Rows(ii).Cells("Sel").Value) = True Then
                        ' Location += gvLocation.Rows(ii).Cells("Code").Value + ","
                        Dim arr As Dictionary(Of String, Object) = gvLocation.Rows(ii).Tag
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            For Each strInn As String In arr.Keys
                                Location += strInn + ","
                            Next
                        Else

                            Dim strQry As String = " select Location_Code from TSPL_LOCATION_MASTER where (Main_Location_Code = '" + gvLocation.Rows(ii).Cells("Code").Value + "' or location_code='" + gvLocation.Rows(ii).Cells("Code").Value + "') "
                            dtLoc = clsDBFuncationality.GetDataTable(strQry)
                            For Each dr As DataRow In dtLoc.Rows
                                Location += dr(0) + ","
                            Next


                        End If
                    End If

                Next
            End If
            If Location.Length > 0 Then
                Location = Location.Remove(Location.Length - 1, 1)
            End If
            Dim qry As String = ""
            qry += "EXEC SP_GET_INVENTORY_AGEING_REPORT '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") + "', '" + clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") + "', '"
            ' qry += clsCommon.myCstr(cboType.SelectedItem.Value) + "', '" + clsCommon.myCstr(cbType.SelectedItem.Value) + "', '" + clsCommon.GetMulcallString(txtLocation.arrValueMember).Replace("'", "") + "', '" + clsCommon.GetMulcallString(txtItem.arrValueMember).Replace("'", "") + "', '"
            qry += clsCommon.myCstr(cboType.SelectedItem.Value) + "', '" + clsCommon.myCstr(cbType.SelectedItem.Value) + "', '" + Location + "', '" + clsCommon.GetMulcallString(txtItem.arrValueMember).Replace("'", "") + "', '"
            qry += clsCommon.myCstr(cbFatSnf.SelectedItem.Value) + "', " + clsCommon.myCstr(Integer.Parse(txt1.Text)) + ", " + clsCommon.myCstr(Integer.Parse(txt2.Text)) + ", " + clsCommon.myCstr(Integer.Parse(txt3.Text)) + ", " + clsCommon.myCstr(IIf(rbtnLocationAll.IsChecked, 1, 0))
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                'gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = dt
                For Each col As GridViewColumn In gv1.Columns
                    col.Width = 100
                Next
                gv1.AllowAddNewRow = False
                gv1.AllowDeleteRow = False
                gv1.EnableFiltering = True
                gv1.ShowGroupedColumns = False
                gv1.ReadOnly = True
                gv1.ShowGroupPanel = False
                'If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedItem.Value), "Detail") = CompairStringResult.Equal Then
                '    'gv1.Columns("Date").HeaderText = "Date"
                '    gv1.Columns("Date").FormatString = "{0: dd/MM/yyyy}"
                'End If

                Dim Bkt1 = ("0-" + clsCommon.myCstr(txt1.Text))
                Dim Bkt2 = (clsCommon.myCstr(Integer.Parse(txt1.Text) + 1) + "-" + clsCommon.myCstr(txt2.Text))
                Dim Bkt3 = (clsCommon.myCstr(Integer.Parse(txt2.Text) + 1) + "-" + clsCommon.myCstr(txt3.Text))
                Dim Bkt4 = ("Over-" + clsCommon.myCstr(Integer.Parse(txt3.Text) + 1))
                'gv1.Columns("Location_Code").HeaderText = "Location Code"
                'gv1.Columns("Location_Desc").HeaderText = "Location Desc"
                'gv1.Columns("Item_Code").HeaderText = "Item Code"
                'gv1.Columns("item_desc").HeaderText = "Item Desc"
                'gv1.Columns("stock_uom").HeaderText = "Stock UOM"
                'gv1.Columns("op").HeaderText = "Opening"
                'gv1.Columns("IN_QTY").HeaderText = "Qty(In)"
                'gv1.Columns("OUT_QTY").HeaderText = "Qty(Out)"
                'gv1.Columns("SNF_PER").HeaderText = "SNF(%)"
                'gv1.Columns("SNF_IN").HeaderText = "SNF KG(In)"
                'gv1.Columns("SNF_OUT").HeaderText = "SNF KG(Out)"
                'gv1.Columns("FAT_PER").HeaderText = "FAT(%)"
                'gv1.Columns("FAT_IN").HeaderText = "FAT KG(In)"
                'gv1.Columns("FAT_OUT").HeaderText = "FAT KG(Out)"
                'gv1.Columns("ClosingBalance").HeaderText = "Outstanding"
                gv1.Columns("BKT1").HeaderText = Bkt1
                gv1.Columns("BKT2").HeaderText = Bkt2
                gv1.Columns("BKT3").HeaderText = Bkt3
                gv1.Columns("BKT4").HeaderText = Bkt4
                gv1.Columns("Item Desc").Width = 200
                'gv1.MasterTemplate.SummaryRowsBottom.Clear()
                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim op As New GridViewSummaryItem("Opening", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(op)

                'Dim IN_QTY As New GridViewSummaryItem("Qty(In)", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(IN_QTY)
                'Dim OUT_QTY As New GridViewSummaryItem("Qty(Out)", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(OUT_QTY)
                'Dim CLOSING_BALANCE As New GridViewSummaryItem("Outstanding", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(CLOSING_BALANCE)

                'Dim SNF_IN As New GridViewSummaryItem("SNF KG(In)", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(SNF_IN)
                'Dim SNF_OUT As New GridViewSummaryItem("SNF KG(Out)", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(SNF_OUT)
                'Dim FAT_IN As New GridViewSummaryItem("FAT KG(In)", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(FAT_IN)
                'Dim FAT_OUT As New GridViewSummaryItem("FAT KG(Out)", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(FAT_OUT)
                'Dim BK1 As New GridViewSummaryItem(Bkt1, "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(BK1)
                'Dim BK2 As New GridViewSummaryItem(Bkt2, "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(BK2)
                'Dim BK3 As New GridViewSummaryItem(Bkt3, "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(BK3)
                'Dim BK4 As New GridViewSummaryItem(Bkt4, "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(BK4)

                'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

                RadPageView1.SelectedPage = RadPageViewPage2
                RadGroupBox3.Enabled = False
            Else
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Data Not Found")
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub
    Public Sub GetData()
        Try


            If txtFromDate.Value > txtToDate.Value Then
                clsCommon.MyMessageBoxShow("Cuttoff Date cant be greater than As on date", Me.Text)
                Return
            End If

            clsCommon.ProgressBarShow()
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim QryCond As String = " where 2=2 "
            QryCond += " and cast(Punching_Date as date) between '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "'"
            'If txtLocation.arrValueMember IsNot Nothing Then
            '    QryCond += " and Location_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ") "
            'End If
            If txtItem.arrValueMember IsNot Nothing Then
                QryCond += " and Item_Code in (" & clsCommon.GetMulcallString(txtItem.arrValueMember) & ") "
            End If

            Dim QryCond1 As String = " where 2=2 "
            QryCond1 += " and cast(Punching_Date as date) < '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "'"
            'If txtLocation.arrValueMember IsNot Nothing Then
            '    QryCond1 += " and Location_Code in (" & clsCommon.GetMulcallString(txtLocation.arrValueMember) & ") "
            'End If
            If txtItem.arrValueMember IsNot Nothing Then
                QryCond1 += " and Item_Code in (" & clsCommon.GetMulcallString(txtItem.arrValueMember) & ") "
            End If

            Dim qry = " with a as ("
            qry += clsInventoryMovement.GetBaseQueryWithOpening(QryCond, QryCond1, clsCommon.myCstr(cbType.SelectedItem.Value))
            qry += ") "
            qry += " select * into #abc from a"
            'qry += " select NULL as punching_date,Item_Code,max(Item_Desc) as Item_Desc,max(Stock_UOM) as Stock_UOM,sum(stock_qty) as stock_qty into #out from #abc where InOut='O' group by Item_Code order by punching_date,Item_Code,Item_Desc,Stock_UOM asc "
            'qry += " select punching_date,Item_Code,max(Item_Desc) as Item_Desc,max(Stock_UOM) as Stock_UOM,sum(stock_qty) as stock_qty into #In from #abc where InOut='I' group by punching_date,Item_Code order by punching_date,Item_Code,Item_Desc,Stock_UOM asc "
            'qry += " declare @asOn date=cast('" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "' as date) "
            'qry += "  select row_number() over (partition by item_code order by [date],item_code) as id,fq.* into #final from ( "
            'qry += " select convert(date, ff.[date],103) as [date],ff.item_desc,ff.item_code,ff.stock_uom,(sum(ff.ClosingBalance) -sum((ff.in_q+ff.out_Q))) as op,sum(ff.in_q) as in_q,sum(ff.out_Q) as out_Q,sum(ff.[QtyOut]) [QtyOut],sum(ff.ClosingBalance) as ClosingBalance "
            'qry += " , diff=(datediff(day,ff.[date],@asOn)),  (case when datediff(day,ff.[date],@asOn)<='" + clsCommon.myCstr(Integer.Parse(txt1.Text)) + "' then ((sum(ff.ClosingBalance) -sum((ff.in_q+ff.out_Q)))+sum(isnull(ff.in_q,0))-sum(isnull(ff.out_Q,0))) else NULL end) as [0-" + clsCommon.myCstr(txt1.Text) + "]  ,  (case when datediff(day,ff.[date],@asOn)>'" + clsCommon.myCstr(Integer.Parse(txt1.Text)) + "' and datediff(day,ff.[date],@asOn)<='" + clsCommon.myCstr(Integer.Parse(txt2.Text)) + "' then ((sum(ff.ClosingBalance) -sum((ff.in_q+ff.out_Q)))+sum(isnull(ff.in_q,0))-sum(isnull(ff.out_Q,0))) else NULL end) as  [" + clsCommon.myCstr((Integer.Parse(txt1.Text) + 1)) + "-" + clsCommon.myCstr(txt2.Text) + "] , (case when datediff(day,ff.[date],@asOn)>'" + clsCommon.myCstr(Integer.Parse(txt2.Text)) + "' and datediff(day,ff.[date],@asOn)<=" + clsCommon.myCstr(Integer.Parse(txt2.Text)) + " then ((sum(ff.ClosingBalance) -sum((ff.in_q+ff.out_Q)))+sum(isnull(ff.in_q,0))-sum(isnull(ff.out_Q,0))) else NULL end) as  [" + clsCommon.myCstr((Integer.Parse(txt2.Text) + 1)) + "-" + clsCommon.myCstr(txt3.Text) + "]  , (case when datediff(day,ff.[date],@asOn)>90 and datediff(day,ff.[date],@asOn)< '" + clsCommon.myCstr(Integer.Parse(txt2.Text)) + "' then ((sum(ff.ClosingBalance) -sum((ff.in_q+ff.out_Q)))+sum(isnull(ff.in_q,0))-sum(isnull(ff.out_Q,0))) else NULL end) as  [" + clsCommon.myCstr((Integer.Parse(txt3.Text) + 1)) + "-Over] "
            'qry += " from ( "
            'qry += " select convert(date, final.[Date],103) [Date],final.item_code,max(final.item_desc) as item_desc,max(final.stock_uom) as stock_uom,sum(final.[in]) as in_q,sum(final.[out]) as out_Q,sum(final.[QtyOut]) as [QtyOut],sum(sum(final.[in]) -sum(final.[out])) over (partition by item_code order by date,item_code) as ClosingBalance from ( "
            'qry += " select convert(varchar(10),i.Punching_Date,103) as [Date],i.Item_Code,i.Item_Desc ,i.Stock_UOM,sum(isnull(i.stock_qty,0)) as [In],0 "
            'qry += " as [Out], sum(isnull(o.stock_qty,0)) as [QtyOut],  "
            'qry += " sum(isnull(o.stock_qty,0))-sum(isnull(i.stock_qty,0)) as newout "
            'qry += " from #in i left join #out o on   i.Item_Code=o.Item_Code and i.Item_Desc=o.Item_Desc and i.Stock_UOM=o.Stock_UOM "
            'qry += " GROUP BY i.Punching_Date ,i.Item_Code,i.Item_Desc,i.Stock_UOM "
            'qry += " ) final group by final.[Date],final.item_code "
            'qry += " )ff group by ff.[date],ff.item_desc,ff.item_code,ff.stock_uom)fq where convert(date,fq.[date],103) between  '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "' "
            'qry += " select a1.id,a1.[date],a1.Item_Code,a1.item_desc,a1.stock_uom,a1.op,a1.in_q,a1.out_Q,a1.QtyOut,a1.ClosingBalance,a1.diff, "
            'qry += " (case  "
            'qry += " when a1.in_q<=(a1.qtyout-isnull((select sum(in_q) from #final where id<a1.id and Item_Code=a1.item_code),0)) "
            'qry += " then  a1.in_q  "
            'qry += " when a1.in_q>(a1.qtyout-isnull((select sum(in_q) from #final where id<a1.id and Item_Code=a1.item_code),0)) "
            'qry += " then  (a1.qtyout-isnull((select sum(in_q) from #final where id<a1.id and Item_Code=a1.item_code),0)) "
            'qry += " else "
            'qry += " (a1.qtyout-isnull((select sum(in_q) from #final  where id<=a1.id and Item_Code=a1.item_code),0)) "
            'qry += " end  ) as [Out] "
            'qry += " ,[0-" + clsCommon.myCstr(txt1.Text) + "],[" + clsCommon.myCstr((Integer.Parse(txt1.Text) + 1)) + "-" + clsCommon.myCstr(txt2.Text) + "],  [" + clsCommon.myCstr((Integer.Parse(txt2.Text) + 1)) + "-" + clsCommon.myCstr(txt3.Text) + "] , [" + clsCommon.myCstr((Integer.Parse(txt3.Text) + 1)) + "-Over] "
            'qry += "  from #final a1 order by date,item_code "
            'qry += " drop table #abc "
            'qry += " drop table #in "
            'qry += " drop table #out "
            'qry += " drop table #final "

            qry += " select NULL as punching_date,location_code,max(Location_Desc) as Location_Desc,Item_Code,max(Item_Desc) as Item_Desc,max(Stock_UOM) as Stock_UOM,sum(stock_qty) as stock_qty,sum(FAT_Kg) as FAT_Kg,sum(SNF_Kg) as SNF_Kg into #out from #abc where InOut='O' group by location_code,Item_Code order by punching_date,Item_Code,Item_Desc,Stock_UOM asc  "
            qry += " select punching_date,location_code,max(Location_Desc) as Location_Desc,Item_Code,max(Item_Desc) as Item_Desc,max(Stock_UOM) as Stock_UOM,sum(stock_qty) as stock_qty,sum(FAT_Kg) as FAT_Kg,sum(SNF_Kg) as SNF_Kg into #In from #abc where InOut='I' and stock_qty>0 group by punching_date,Item_Code,location_code order by punching_date,location_code,Item_Code,Item_Desc,Stock_UOM asc  "
            qry += " select convert(date,i.Punching_Date,103) as [Date],i.Location_Code,i.Location_Desc,i.Item_Code,i.Item_Desc ,i.Stock_UOM,sum(isnull(i.stock_qty,0)) as [In], sum(isnull(o.stock_qty,0)) as [Out],sum(isnull(i.SNF_Kg,0)) as SNF_Kg_in,sum(isnull(o.SNF_Kg,0)) as SNF_Kg_out,sum(isnull(i.FAT_Kg,0)) as FAT_Kg_in,sum(isnull(o.FAT_Kg,0)) as FAT_Kg_out  INTO #TEMP1  from #in i left join #out o on   i.Item_Code=o.Item_Code and i.Stock_UOM=o.Stock_UOM  AND I.Location_Code=O.Location_Code  GROUP BY i.Punching_Date ,i.Item_Code,i.Item_Desc,i.Stock_UOM,i.Location_Code,i.Location_Desc  " + Environment.NewLine
            qry += " select row_number() over (partition by item_code order by [date],item_code) as id,convert(date, final.[Date],103) [Date],final.Location_Code,max(final.Location_Desc) as Location_Desc,final.item_code,max(final.item_desc) as item_desc,max(final.stock_uom) as stock_uom,sum(final.[in]) as in_q,avg(final.[out]) as out_Q,sum(final.SNF_Kg_in) as SNF_Kg_in,sum(final.SNF_Kg_out) as SNF_Kg_out,sum(final.FAT_Kg_in) as FAT_Kg_in,sum(final.FAT_Kg_out) as FAT_Kg_out into #final"
            qry += " from #TEMP1  final group by final.[Date],final.item_code,final.Location_Code  "
            qry += " SELECT *,(out_Q-ISNULL((SELECT SUM(B.in_q) FROM #final B WHERE A.ID>B.ID AND A.Item_Code=B.Item_Code AND A.Location_Code=B.Location_Code AND A.stock_uom=B.stock_uom),0)) BALANCE,"
            qry += " (CASE WHEN in_q<(out_Q-ISNULL((SELECT SUM(B.in_q) FROM #final B WHERE A.ID>B.ID AND A.Item_Code=B.Item_Code AND A.Location_Code=B.Location_Code AND A.stock_uom=B.stock_uom),0)) THEN in_q ELSE (CASE WHEN (out_Q>ISNULL((SELECT SUM(B.in_q) "
            qry += " FROM #final B WHERE A.ID>B.ID AND A.Item_Code=B.Item_Code AND A.Location_Code=B.Location_Code AND A.stock_uom=B.stock_uom),0)) THEN  (out_Q-ISNULL((SELECT SUM(B.in_q) FROM #final B WHERE A.ID>B.ID AND A.Item_Code=B.Item_Code AND A.Location_Code=B.Location_Code AND A.stock_uom=B.stock_uom),0)) ELSE 0 END) END  ) AS OUT_QTY,"
            qry += " (CASE WHEN SNF_Kg_in<(SNF_Kg_out-ISNULL((SELECT SUM(B.SNF_Kg_in) FROM #final B WHERE A.ID>B.ID AND A.Item_Code=B.Item_Code AND A.Location_Code=B.Location_Code AND A.stock_uom=B.stock_uom),0)) THEN SNF_Kg_in ELSE (CASE WHEN (SNF_Kg_out>ISNULL((SELECT SUM(B.SNF_Kg_in) "
            qry += " FROM #final B WHERE A.ID>B.ID AND A.Item_Code=B.Item_Code AND A.Location_Code=B.Location_Code AND A.stock_uom=B.stock_uom),0)) THEN  (SNF_Kg_out-ISNULL((SELECT SUM(B.SNF_Kg_in) FROM #final B WHERE A.ID>B.ID AND A.Item_Code=B.Item_Code AND A.Location_Code=B.Location_Code AND A.stock_uom=B.stock_uom),0)) ELSE 0 END) END  ) AS SNF_OUT_QTY,"
            qry += " (CASE WHEN FAT_Kg_in<(FAT_Kg_out-ISNULL((SELECT SUM(B.FAT_Kg_in) FROM #final B WHERE A.ID>B.ID AND A.Item_Code=B.Item_Code AND A.Location_Code=B.Location_Code AND A.stock_uom=B.stock_uom),0)) THEN FAT_Kg_in ELSE (CASE WHEN (FAT_Kg_out>ISNULL((SELECT SUM(B.FAT_Kg_in) "
            qry += " FROM #final B WHERE A.ID>B.ID AND A.Item_Code=B.Item_Code AND A.Location_Code=B.Location_Code AND A.stock_uom=B.stock_uom),0)) THEN  (FAT_Kg_out-ISNULL((SELECT SUM(B.FAT_Kg_in) FROM #final B WHERE A.ID>B.ID AND A.Item_Code=B.Item_Code AND A.Location_Code=B.Location_Code AND A.stock_uom=B.stock_uom),0)) ELSE 0 END) END  ) AS FAT_OUT_QTY INTO #A"
            qry += " FROM #final A"
            qry += " SELECT Date,Location_Code,max(Location_Desc) as Location_Desc,Item_Code,max(item_desc) as item_desc,max(stock_uom) as stock_uom,SUM(in_q) AS IN_QTY,SUM(OUT_QTY) AS OUT_QTY,"
            qry += " sum(sum(ISNULL(in_q,0)) -sum(ISNULL(OUT_QTY,0))) over (partition by Location_Code,item_code,stock_uom order by date,item_code) as ClosingBalance,(CASE WHEN SUM(in_q) >0 THEN sum(FAT_Kg_in) ELSE 0 END) as FAT_IN,(CASE WHEN SUM(OUT_QTY) >0 THEN SUM(FAT_OUT_QTY) ELSE 0 END) AS FAT_OUT,(CASE WHEN SUM(in_q) >0 THEN SUM(SNF_Kg_in) ELSE 0 END) AS SNF_IN,(CASE WHEN SUM(OUT_QTY) >0 THEN SUM(SNF_OUT_QTY) ELSE 0 END) AS SNF_OUT INTO #TEMP3 "
            qry += " FROM #A  GROUP  BY Date,Item_Code,Location_Code , stock_uom"
            qry += " SELECT FINAL.DATE,final.Location_Code,max(final.Location_Desc) as Location_Desc,FINAL.Item_Code,max(FINAL.item_desc) as item_desc,max(final.stock_uom) as stock_uom,(sum(FINAL.ClosingBalance) -sum(FINAL.IN_QTY)+sum(FINAL.OUT_QTY) )as op,sum(FINAL.IN_QTY) IN_QTY,sum(FINAL.OUT_QTY) AS OUT_QTY,sum(FINAL.ClosingBalance) AS CLOSING_BALANCE,sum(FINAL.FAT_IN) as FAT_IN,SUM(FINAL.FAT_OUT) AS FAT_OUT,SUM(FINAL.SNF_IN) AS SNF_IN,SUM(FINAL.SNF_OUT) AS SNF_OUT INTO #TEMP2 "
            qry += " FROM #TEMP3 AS FINAL"
            qry += " where  convert(date,FINAL.Date,103) between  '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "'  GROUP BY FINAL.DATE,FINAL.Item_Code,FINAL.Location_Code  "

            qry += " declare @asOn date=cast('" & clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy") & "' as date) "
            qry += "  select fin.Date,fin.Location_Code,max(fin.Location_Desc) as Location_Desc,fin.Item_Code,max(fin.item_desc) as item_desc,max(fin.stock_uom) as stock_uom,sum(fin.op) as op,sum(fin.IN_QTY) IN_QTY,sum(fin.OUT_QTY) AS OUT_QTY,FAT_PER=ROUND(((sum(FIN.FAT_IN) *100)/sum(FIN.IN_QTY) ),2),ROUND(sum(FIN.FAT_IN),2) as FAT_IN,ROUND(SUM(FAT_OUT),2) AS FAT_OUT,SNF_PER=ROUND(((sum(FIN.SNF_IN) *100)/sum(FIN.IN_QTY) ),2),ROUND(SUM(SNF_IN),2) AS SNF_IN,ROUND(SUM(SNF_OUT),2) AS SNF_OUT,"
            '-----Bucket 1------
            qry += " (case when datediff(day,fin.[date],@asOn)<='" + clsCommon.myCstr(Integer.Parse(txt1.Text)) + "' then "
            If clsCommon.CompairString(clsCommon.myCstr(cbType.SelectedItem.Value), "Outstanding") = CompairStringResult.Equal Then
                qry += " ((sum(isnull(fin.op,0))+sum(isnull(fin.IN_QTY,0)))-sum(isnull(fin.OUT_QTY,0))) "
            Else
                qry += " sum(isnull(fin.IN_QTY,0))-sum(isnull(fin.OUT_QTY,0)) "
            End If
            qry += "  else NULL end) as [0-" + clsCommon.myCstr(txt1.Text) + "]  ,  "
            '-----Bucket 2------
            qry += " (case when datediff(day,fin.[date],@asOn)>'" + clsCommon.myCstr(Integer.Parse(txt1.Text)) + "' and datediff(day,fin.[date],@asOn)<='" + clsCommon.myCstr(Integer.Parse(txt2.Text)) + "' then  "
            If clsCommon.CompairString(clsCommon.myCstr(cbType.SelectedItem.Value), "Outstanding") = CompairStringResult.Equal Then
                qry += " (sum(isnull(fin.op,0))+sum(isnull(fin.IN_QTY,0))-sum(isnull(fin.OUT_QTY,0))) "
            Else
                qry += " sum(isnull(fin.IN_QTY,0))-sum(isnull(fin.OUT_QTY,0)) "
            End If
            qry += " else NULL end) as   [" + clsCommon.myCstr((Integer.Parse(txt1.Text) + 1)) + "-" + clsCommon.myCstr(txt2.Text) + "] , "
            '-----Bucket 3------
            qry += " (case when datediff(day,fin.[date],@asOn)>" + clsCommon.myCstr(Integer.Parse(txt2.Text)) + " and datediff(day,fin.[date],@asOn)<=" + clsCommon.myCstr(Integer.Parse(txt2.Text)) + "  then "
            If clsCommon.CompairString(clsCommon.myCstr(cbType.SelectedItem.Value), "Outstanding") = CompairStringResult.Equal Then
                qry += " (sum(isnull(fin.op,0))+sum(isnull(fin.IN_QTY,0))-sum(isnull(fin.OUT_QTY,0))) "
            Else
                qry += " sum(isnull(fin.IN_QTY,0))-sum(isnull(fin.OUT_QTY,0)) "
            End If
            qry += "  else NULL end) as   [" + clsCommon.myCstr((Integer.Parse(txt2.Text) + 1)) + "-" + clsCommon.myCstr(txt3.Text) + "]  , "
            '-----Bucket 4------
            qry += " (case when datediff(day,fin.[date],@asOn)>" + clsCommon.myCstr((Integer.Parse(txt2.Text) + 1)) + " then "
            If clsCommon.CompairString(clsCommon.myCstr(cbType.SelectedItem.Value), "Outstanding") = CompairStringResult.Equal Then
                qry += " (sum(isnull(fin.op,0))+sum(isnull(fin.IN_QTY,0))-sum(isnull(fin.OUT_QTY,0))) "
            Else
                qry += " sum(isnull(fin.IN_QTY,0))-sum(isnull(fin.OUT_QTY,0)) "
            End If
            qry += "  else NULL end) as  [Over-" + clsCommon.myCstr((Integer.Parse(txt3.Text) + 1)) + "]  "
            qry += " ,sum(fin.CLOSING_BALANCE) AS CLOSING_BALANCE INTO #TEMP_FINAL  "
            qry += " from #TEMP2 fin "
            qry += "   where  1=1 "
            If clsCommon.CompairString(clsCommon.myCstr(cbFatSnf.SelectedItem.Value), "FAT & SNF") = CompairStringResult.Equal Then
                qry += " AND FAT_IN>0 AND SNF_IN>0 "
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cbFatSnf.SelectedItem.Value), "NONE") = CompairStringResult.Equal Then
                qry += " AND FAT_IN=0 AND SNF_IN=0 "
            Else

            End If
            qry += " GROUP BY fin.DATE,fin.Location_Code,fin.Item_Code order by fin.DATE,fin.Item_Code,fin.Location_Code "

            qry += "SELECT "
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedItem.Value), "Detail") = CompairStringResult.Equal Then
                qry += " Date, "
            End If
            qry += " Item_Code,Location_Code,max(Location_Desc) as Location_Desc,max(item_desc) as item_desc,max(stock_uom) as stock_uom,SUM(OP) AS op,SUM(IN_QTY) AS IN_QTY,SUM(OUT_QTY) AS OUT_QTY,ROUND(((sum(FAT_IN) *100)/sum(IN_QTY) ),2) AS FAT_PER,SUM(FAT_IN) AS FAT_IN,SUM(FAT_OUT) AS FAT_OUT,ROUND(((sum(SNF_IN) *100)/sum(IN_QTY) ),2) AS SNF_PER,SUM(SNF_IN) AS SNF_IN,SUM(SNF_OUT) AS SNF_OUT  "
            If clsCommon.CompairString(clsCommon.myCstr(cbType.SelectedItem.Value), "Outstanding") = CompairStringResult.Equal Then
                qry += " , SUM(ISNULL([0-" + clsCommon.myCstr(txt1.Text) + "],0))  AS [0-" + clsCommon.myCstr(txt1.Text) + "] "
                qry += " , SUM(ISNULL([" + clsCommon.myCstr((Integer.Parse(txt1.Text) + 1)) + "-" + clsCommon.myCstr(txt2.Text) + "],0))  AS  [" + clsCommon.myCstr((Integer.Parse(txt1.Text) + 1)) + "-" + clsCommon.myCstr(txt2.Text) + "]  "
                qry += " , SUM(ISNULL([" + clsCommon.myCstr((Integer.Parse(txt2.Text) + 1)) + "-" + clsCommon.myCstr(txt3.Text) + "],0)) AS [" + clsCommon.myCstr((Integer.Parse(txt2.Text) + 1)) + "-" + clsCommon.myCstr(txt3.Text) + "] "
                qry += " , SUM(ISNULL([Over-" + clsCommon.myCstr((Integer.Parse(txt3.Text) + 1)) + "],0))  AS [Over-" + clsCommon.myCstr((Integer.Parse(txt3.Text) + 1)) + "] "
            Else
                qry += " ,(CASE WHEN SUM(OUT_QTY)>0 THEN SUM(ISNULL([0-" + clsCommon.myCstr(txt1.Text) + "],0)) ELSE 0 END) AS [0-" + clsCommon.myCstr(txt1.Text) + "] "
                qry += " ,(CASE WHEN SUM(OUT_QTY)>0 THEN SUM(ISNULL([" + clsCommon.myCstr((Integer.Parse(txt1.Text) + 1)) + "-" + clsCommon.myCstr(txt2.Text) + "],0)) ELSE 0 END) AS  [" + clsCommon.myCstr((Integer.Parse(txt1.Text) + 1)) + "-" + clsCommon.myCstr(txt2.Text) + "]  "
                qry += " ,(CASE WHEN SUM(OUT_QTY)>0 THEN SUM(ISNULL([" + clsCommon.myCstr((Integer.Parse(txt2.Text) + 1)) + "-" + clsCommon.myCstr(txt3.Text) + "],0)) ELSE 0 END) AS [" + clsCommon.myCstr((Integer.Parse(txt2.Text) + 1)) + "-" + clsCommon.myCstr(txt3.Text) + "] "
                qry += " ,(CASE WHEN SUM(OUT_QTY)>0 THEN SUM(ISNULL([Over-" + clsCommon.myCstr((Integer.Parse(txt3.Text) + 1)) + "],0)) ELSE 0 END) AS [Over-" + clsCommon.myCstr((Integer.Parse(txt3.Text) + 1)) + "] "
            End If
            qry += " , SUM(CLOSING_BALANCE) AS ClosingBalance "
            qry += "  FROM #TEMP_FINAL "
            qry += " GROUP BY "
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedItem.Value), "Detail") = CompairStringResult.Equal Then
                qry += " DATE,Location_Code,  Item_Code order by DATE,Item_Code "
            Else
                qry += " Location_Code, Item_Code order by Item_Code "
            End If
            qry += " drop table #abc "
            qry += " drop table #in "
            qry += " drop table #out "
            qry += " drop table #final "
            qry += " DROP TABLE #A "
            qry += " DROP TABLE #TEMP_FINAL "
            qry += " DROP TABLE #TEMP1 "
            qry += " DROP TABLE #TEMP2  "
            qry += " DROP TABLE #TEMP3 "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                'gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = dt
                For Each col As GridViewColumn In gv1.Columns
                    col.Width = 100
                Next
                gv1.AllowAddNewRow = False
                gv1.AllowDeleteRow = False
                gv1.EnableFiltering = True
                gv1.ShowGroupedColumns = False
                gv1.ReadOnly = True
                gv1.ShowGroupPanel = False
                If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedItem.Value), "Detail") = CompairStringResult.Equal Then
                    gv1.Columns("Date").HeaderText = "Date"
                    gv1.Columns("Date").FormatString = "{0: dd/MM/yyyy}"
                End If
                gv1.Columns("Location_Code").HeaderText = "Location Code"
                gv1.Columns("Location_Desc").HeaderText = "Location Desc"
                gv1.Columns("Item_Code").HeaderText = "Item Code"
                gv1.Columns("item_desc").HeaderText = "Item Desc"
                gv1.Columns("stock_uom").HeaderText = "Stock UOM"
                gv1.Columns("op").HeaderText = "Opening"
                gv1.Columns("IN_QTY").HeaderText = "Qty(In)"
                gv1.Columns("OUT_QTY").HeaderText = "Qty(Out)"
                gv1.Columns("SNF_PER").HeaderText = "SNF(%)"
                gv1.Columns("SNF_IN").HeaderText = "SNF KG(In)"
                gv1.Columns("SNF_OUT").HeaderText = "SNF KG(Out)"
                gv1.Columns("FAT_PER").HeaderText = "FAT(%)"
                gv1.Columns("FAT_IN").HeaderText = "FAT KG(In)"
                gv1.Columns("FAT_OUT").HeaderText = "FAT KG(Out)"
                gv1.Columns("ClosingBalance").HeaderText = "Outstanding"
                gv1.Columns("item_desc").Width = 200
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim op As New GridViewSummaryItem("op", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(op)

                Dim IN_QTY As New GridViewSummaryItem("IN_QTY", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(IN_QTY)
                Dim OUT_QTY As New GridViewSummaryItem("OUT_QTY", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(OUT_QTY)
                Dim CLOSING_BALANCE As New GridViewSummaryItem("ClosingBalance", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(CLOSING_BALANCE)

                Dim SNF_IN As New GridViewSummaryItem("SNF_IN", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(SNF_IN)
                Dim SNF_OUT As New GridViewSummaryItem("SNF_OUT", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(SNF_OUT)
                Dim FAT_IN As New GridViewSummaryItem("FAT_IN", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(FAT_IN)
                Dim FAT_OUT As New GridViewSummaryItem("FAT_OUT", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(FAT_OUT)
                Dim BK1 As New GridViewSummaryItem(("0-" + clsCommon.myCstr(txt1.Text)), "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(BK1)
                Dim BK2 As New GridViewSummaryItem((clsCommon.myCstr(Integer.Parse(txt1.Text) + 1) + "-" + clsCommon.myCstr(txt2.Text)), "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(BK2)
                Dim BK3 As New GridViewSummaryItem((clsCommon.myCstr(Integer.Parse(txt2.Text) + 1) + "-" + clsCommon.myCstr(txt3.Text)), "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(BK3)
                Dim BK4 As New GridViewSummaryItem(("Over-" + clsCommon.myCstr(Integer.Parse(txt3.Text) + 1)), "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(BK4)

                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Data Not Found")
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub
    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles BulkExportCsv.Click
        BulkExport("csv")
    End Sub
    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles BulkExportXls.Click
        BulkExport("xls")
    End Sub

    Sub BulkExport(ByVal FormatType As String)
        Try
            clsCommon.ProgressBarPercentShow()
            clsCommon.ProgressBarPercentUpdate(0, "Generating query for the report..")
            Dim qry As String = ""

            clsCommon.ProgressBarPercentUpdate(10, "Query generated..starting Inventory Ageing export..")
            If cboType.SelectedValue = "Summary" Then
                transportSql.BulkExport("Summary", qry, "", FormatType)
            Else
                transportSql.BulkExport("Detail", qry, "", FormatType)

            End If

            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Data exported successfully")
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub





    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
            gv1.Columns(ii).Width = 125
        Next
        gv1.Columns("comp_name").IsVisible = True
        gv1.Columns("comp_name").Width = 100
        gv1.Columns("comp_name").HeaderText = "Company"

        gv1.Columns("prnttrans").IsVisible = True
        gv1.Columns("prnttrans").Width = 100
        gv1.Columns("prnttrans").HeaderText = "Transcation Type"

        gv1.Columns("asofdate").IsVisible = True
        gv1.Columns("asofdate").Width = 100
        gv1.Columns("asofdate").HeaderText = "Aged Transcation As of "

        gv1.Columns("cutoffdt").IsVisible = True
        gv1.Columns("cutoffdt").Width = 100
        gv1.Columns("cutoffdt").HeaderText = "Cutoff Document date"

        gv1.Columns("Location_Desc").IsVisible = True
        gv1.Columns("Location_Desc").Width = 100
        gv1.Columns("Location_Desc").HeaderText = "Location"

        gv1.Columns("category_desc").IsVisible = True
        gv1.Columns("category_desc").Width = 150
        gv1.Columns("category_desc").HeaderText = "Category Description"

        gv1.Columns("Item id").IsVisible = True
        gv1.Columns("Item id").Width = 100
        gv1.Columns("Item id").HeaderText = "Item Code"

        gv1.Columns("Item name").IsVisible = True
        gv1.Columns("Item name").Width = 150
        gv1.Columns("Item name").HeaderText = "Item Description"

        gv1.Columns("uom").IsVisible = True
        gv1.Columns("uom").Width = 100
        gv1.Columns("uom").HeaderText = "UOM"

        gv1.Columns("qty").IsVisible = True
        gv1.Columns("qty").Width = 100
        gv1.Columns("qty").HeaderText = "Quantity"

        gv1.Columns("cost").IsVisible = True
        gv1.Columns("cost").Width = 100
        gv1.Columns("cost").HeaderText = "Cost"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("cost", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub


    Sub LoadTransactionType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Detail"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Summary"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"
    End Sub
    Sub LoadBucketType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Outstanding")
        dt.Rows.Add("Out")
        cbType.DataSource = dt
        cbType.ValueMember = "Code"
        cbType.DisplayMember = "Code"
    End Sub

    Sub LoadFatSnf()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("MILK", "With FAT & SNF")
        dt.Rows.Add("OTHER", "Without FAT & SNF")
        dt.Rows.Add("BOTH", "BOTH")
        cbFatSnf.DataSource = dt
        cbFatSnf.ValueMember = "Code"
        cbFatSnf.DisplayMember = "Name"
    End Sub

    Private Sub TreeView_NodeCheckedChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.TreeNodeCheckedEventArgs)
        TreeCheckBoxes(e.Node, e.Node.Checked)
    End Sub

    Public Sub TreeCheckBoxes(ByVal CurrentNode As RadTreeNode, ByVal val As Boolean)
        For Each Ctr As RadTreeNode In CurrentNode.Nodes
            Ctr.Checked = val
            TreeCheckBoxes(Ctr, val)
        Next
    End Sub

    Private Sub txt1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            txt1.Select()
            Return
        End If
    End Sub

    Private Sub txt2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt2.Focus()
            txt2.Select()
            Return
        End If
    End Sub

    Private Sub txt3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt3.Focus()
            txt3.Select()
            Return
        End If
    End Sub

    Private Sub txt3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.txtover.Text = Me.txt3.Text
    End Sub




    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs)
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Description from tspl_location_master where location_type='physical' order by location_code"
            'txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Location", qry, "Code", "Description", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = "select Item_Code as Code,Item_Desc as Description from tspl_item_master order by item_code"
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("Item", qry, "Code", "Description", txtItem.arrValueMember, txtItem.arrDispalyMember)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtCategory__My_Click(sender As Object, e As EventArgs)
        Try
            Dim qry As String = "select Code ,Name as Description,Parent from ("
            qry += " select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE"
            qry += " union all"
            qry += " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
            qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE"
            qry += " Union all"
            qry += " select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
            qry += " )xxx order by Sno"
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("Category", qry, "Code", "Description", txtItem.arrValueMember, txtItem.arrDispalyMember)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txt3_TextChanged_1(sender As Object, e As EventArgs) Handles txt3.TextChanged
        txtover.Text = txt3.Text

    End Sub



    Private Sub cbType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cbType.SelectedIndexChanged
        txtItem.arrValueMember = Nothing
    End Sub

    Private Sub MyComboBox1_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs)

    End Sub

    Private Sub RadGroupBox3_Click(sender As Object, e As EventArgs) Handles RadGroupBox3.Click

    End Sub

    Private Sub rbtnLocationSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationSelect.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        CheckedAll(gvLocation)
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

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        UnCheckedAll(gvLocation)
    End Sub

    Private Sub gvLocation_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvLocation.CellDoubleClick
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
End Class
