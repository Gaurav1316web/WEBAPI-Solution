Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Public Class rptTankerProfitLossReport
    Private Sub rptTankerProfitLossReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Griddata(False)
    End Sub

    Sub SetGridFormat1()

        Gv1.AutoExpandGroups = False
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True


        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Gv1.Columns("Comp_Name").IsVisible = False
        Gv1.Columns("City_Code").IsVisible = False
        Gv1.Columns("ROUTE_NAME").IsVisible = False
        Gv1.Columns("Mcc_Code_VLC_Uploader").IsVisible = False
        Gv1.Columns("Tanker_No").IsVisible = False
        Gv1.Columns("Storage_Capacity").IsVisible = False
        Gv1.Columns("StorageCapacityDesc").IsVisible = False

        Gv1.Columns("Qty").HeaderText = "Qty(In kg)"
        Gv1.Columns("FAT_Per").HeaderText = "FAT %"
        Gv1.Columns("SNF_Per").HeaderText = "SNF %"
        Gv1.Columns("Fat_kg").HeaderText = "Kg FAT"
        Gv1.Columns("Fat_kg").FormatString = "{0:n3}"
        Gv1.Columns("SNF_kg").HeaderText = "Kf SNF"
        Gv1.Columns("SNF_kg").FormatString = "{0:n3}"

        Gv1.Columns("LinkDCSQty").HeaderText = "Qty (In Kg)"
        Gv1.Columns("LinkDCSQty").IsVisible = True

        Gv1.Columns("LinkDCSFAT_KG").HeaderText = "Kg FAT"
        Gv1.Columns("LinkDCSFAT_KG").IsVisible = True
        Gv1.Columns("LinkDCSFAT_KG").FormatString = "{0:n3}"

        Gv1.Columns("LinkDCSSNF_KG").HeaderText = "Kg SNF"
        Gv1.Columns("LinkDCSSNF_KG").IsVisible = True
        Gv1.Columns("LinkDCSSNF_KG").FormatString = "{0:n3}"

        Gv1.Columns("LinkDCSFAT").HeaderText = " FAT %"
        Gv1.Columns("LinkDCSFAT").IsVisible = True
        Gv1.Columns("LinkDCSFAT").FormatString = "{0:n1}"

        Gv1.Columns("LinkDCSSNF").HeaderText = "SNF %"
        Gv1.Columns("LinkDCSSNF").IsVisible = True
        Gv1.Columns("LinkDCSSNF").FormatString = "{0:n1}"

        Gv1.Columns("TotalQty").HeaderText = "Qty(In Kg)"

        Gv1.Columns("Total_FAT").HeaderText = " FAT %"
        Gv1.Columns("Total_FAT").IsVisible = True
        Gv1.Columns("Total_FAT").FormatString = "{0:n1}"

        Gv1.Columns("Total_SNF").HeaderText = "SNF %"
        Gv1.Columns("Total_SNF").IsVisible = True
        Gv1.Columns("Total_SNF").FormatString = "{0:n1}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Qty", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("FAT_Per", "{0:F2}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("SNF_Per", "{0:F2}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Fat_kg", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("SNF_kg", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("LinkDCSQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("LinkDCSFAT_KG", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("LinkDCSSNF_KG", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("LinkDCSFAT", "{0:F1}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("LinkDCSSNF", "{0:F1}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("TotalQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("Total_FAT", "{0:F1}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item12)
        Dim item13 As New GridViewSummaryItem("Total_SNF", "{0:F1}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item13)

        Gv1.ShowGroupPanel = True
        Gv1.MasterTemplate.AutoExpandGroups = True
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        View()
    End Sub
    Sub View()

        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup("Name of BMC"))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_NAME").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Total Milk of BMC"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Qty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("FAT_Per").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("SNF_Per").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Fat_kg").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("SNF_kg").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Total Milk of Link DCS"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("LinkDCSQty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("LinkDCSFAT_KG").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("LinkDCSSNF_KG").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("LinkDCSFAT").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("LinkDCSSNF").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Total Milk of BMC DCS"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("TotalQty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Total_FAT").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Total_SNF").Name)

            Gv1.ViewDefinition = view
        End If
    End Sub
    Public Sub Griddata(ByVal print As Boolean)
        Try

            Dim FinalQuery As String = Nothing
            Dim BaseQuery As String = Nothing
            Dim qry As String = Nothing

            If clsCommon.myLen(txtTankerNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Tanker no", Me.Text)
                txtTankerNo.Focus()
                Exit Sub
            End If
            BaseQuery = "SELECT TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.City_Code,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME,(TSPL_MCC_MASTER.MCC_NAME)MCC_NAME,(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader,(TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Tanker_No)Tanker_No,(TSPL_TANKER_MASTER.Storage_Capacity)Storage_Capacity,(TSPL_TANKER_MASTER.StorageCapacityDesc)StorageCapacityDesc,(Entered_Qty) as Qty,(TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.FAT) as FAT_Per ,(CLR) as SNF_Per,((TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.FAT*Entered_Qty)/100) as Fat_kg,((CLR*Entered_Qty)/100) as SNF_kg,case when TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code<>TSPL_VLC_MASTER_HEAD.VLC_Code then TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Qty else 0 end as Link_Qty1
            , (TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Qty)as Link_Qty,case when TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code<>TSPL_VLC_MASTER_HEAD.VLC_Code then TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.FAT else 0 end as Link_Fat1,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.FAT as Link_Fat,case when TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code<>TSPL_VLC_MASTER_HEAD.VLC_Code then TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.SNF else 0 end as Link_SNF1,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.SNF AS Link_SNF,case when TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code<>TSPL_VLC_MASTER_HEAD.VLC_Code then TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.SNFKG else 0 end as Link_SNFKG1,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.SNFKG as Link_SNFKG,case when TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code<>TSPL_VLC_MASTER_HEAD.VLC_Code then TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.FATKG else 0 end as Link_FATKG1,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.FATKG as Link_FATKG,TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code,
            case when TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code then TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader else '' end as SameUploader,

            CASE WHEN Mcc_Code_VLC_Uploader=(case when TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code then TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader else '' end) THEN (Qty) ELSE 0 END AS UnLinkedQty,
            CASE WHEN Mcc_Code_VLC_Uploader=(case when TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code then TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader else '' end) THEN (TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.FAT) ELSE 0 END AS UnLinkedFat,
            CASE WHEN Mcc_Code_VLC_Uploader=(case when TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code then TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader else '' end) THEN (TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.SNF) ELSE 0 END AS UnLinkedSNF,
            CASE WHEN Mcc_Code_VLC_Uploader=(case when TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code then TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader else '' end) THEN (TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.SNFKG) ELSE 0 END AS UnLinkedSNFKG,
            CASE WHEN Mcc_Code_VLC_Uploader=(case when TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code then TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader else '' end) THEN (TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.FATKG) ELSE 0 END AS UnLinkedFATKG


                FROM TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL 
                left outer join TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS on TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_No=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS_DETAIL.Document_No
                left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.MCC_Code 
                left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Tanker_No
                left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader
                 left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code 
                left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO= TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Route_Code 
                 left outer join TSPL_COMPANY_MASTER on 1=1
                where convert(date, TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Document_Date,103)= convert(date,'" + txtDate.Value + "',103) and TSPL_MILK_COLLECTION_DCS_MULTIPLE_DAYS.Tanker_No='" + txtTankerNo.Value + "' "

            FinalQuery = " select *,(Qty-LinkDCSQty) as TotalQty,'" + txtDate.Value + "' as Date,CASE WHEN (Qty - LinkDCSQty) = 0 THEN 0 ELSE ((Fat_kg - LinkDCSFAT_KG) * 100) / (Qty - LinkDCSQty)END as Total_FAT,CASE WHEN (Qty - LinkDCSQty) = 0 THEN 0 ELSE ((SNF_kg - LinkDCSSNF_KG) * 100) / (Qty - LinkDCSQty) END as Total_SNF from (select max(VV.Comp_Name)Comp_Name,MAX(vv.City_Code)City_Code,MAX(ROUTE_NAME)ROUTE_NAME,max(vv.MCC_NAME)MCC_NAME,(VV.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader,max(VV.Tanker_No)Tanker_No,max(VV.Storage_Capacity)Storage_Capacity,max(vv.StorageCapacityDesc)StorageCapacityDesc,max(vv.Qty)Qty,max(vv.FAT_Per)FAT_Per,max(vv.SNF_Per)SNF_Per,max(vv.Fat_kg)Fat_kg,max(vv.SNF_kg)SNF_kg, case when (vv.Mcc_Code_VLC_Uploader)=max(vv.SameUploader) then sum(Link_Qty)-sum(UnLinkedQty) else sum(Link_Qty)   end as  LinkDCSQty,case when (vv.Mcc_Code_VLC_Uploader)=max(vv.SameUploader) then sum(Link_Fat)-sum(UnLinkedFat) else sum(Link_Fat)   end as  LinkDCSFAT,case when (vv.Mcc_Code_VLC_Uploader)=max(vv.SameUploader) then sum(link_SNF)-sum(UnLinkedSNF) else sum(Link_SNF)   end as  LinkDCSSNF,case when (vv.Mcc_Code_VLC_Uploader)=max(vv.SameUploader) then sum(Link_SNFKG)-sum(UnLinkedSNFKG) else sum(Link_SNFKG)   end as  LinkDCSSNF_KG,case when (vv.Mcc_Code_VLC_Uploader)=max(vv.SameUploader) then sum(Link_FATKG)-sum(UnLinkedFATKG) else sum(Link_FATKG)   end as  LinkDCSFAT_KG from( " + BaseQuery + ") "
            FinalQuery += "VV group by Mcc_Code_VLC_Uploader)xx"



            Dim dt As DataTable = clsDBFuncationality.GetDataTable(FinalQuery)
            Dim subquery As String = "select Gross_Weight,Tare_Weight,Net_Weight,Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0)) As [FAT %] , Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0)) As [SNF %],(Net_Weight*t_FAT.Param_Field_Value)/100 as FATKG,(Net_Weight*t_SNF.Param_Field_Value)/100 as SNFKG from TSPL_Weighment_Detail 
                left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.Weighment_No = TSPL_Weighment_Detail.Weighment_No
                Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No
                Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF')t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No
                where TSPL_Weighment_Detail.Tanker_No='" + txtTankerNo.Value + "' and convert(date, TSPL_Weighment_Detail.Weighment_date,103)= convert(date,'" + txtDate.Value + "',103) "

            Dim dtsub As DataTable = clsDBFuncationality.GetDataTable(subquery)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

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
                Gv1.EnableFiltering = True
                SetGridFormat1()
                Gv1.BestFitColumns()
                If print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dtsub, "rptTankerProfitLoss", "ProfitLoss", "SubTankerProfitLoss.rpt")
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtTankerNo.Value = ""
        btnGo.Enabled = True
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Griddata(True)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt")) ' clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy HH:MM"))
                arrHeader.Add("User : " + objCommonVar.CurrentUser)
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    'transportSql.QuickExportToExcel(Gv1, "", "", , arrHeader)
                    clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtTankerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerNo._MYValidating
        Try
            Dim qry As String = " select Tanker_No from TSPL_TANKER_MASTER where Tanker_No like '%" + txtTankerNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows.Count = 1 Then
                    txtTankerNo.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                End If
            End If
            txtTankerNo.Value = clsfrmTankerMaster.GetFinder("", txtTankerNo.Value, isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class