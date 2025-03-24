Imports common
Public Class rptAbsentBMCDCS
    Inherits FrmMainTranScreen
    Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        gv2.DataSource = Nothing
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub
    Private Sub BMCData()
        Try
            Dim Qry As String = ""
            Dim dt As DataTable = Nothing
            Qry = "select Case when In_active<>1 then TSPL_MCC_MASTER.MCC_Code else 'NA' end as [BMC Code],TSPL_MCC_MASTER.MCC_NAME as [BMC Name],TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [MCC Uploader Code],TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code as [BMC CodeIN],MCC_NAME as [BMC NameIN],TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [MCC Uploader CodeIN],TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,case when REF_PK_ID_BMCDCS_TRIP<>'' then 'Mobile' else 'ERP' end as [Entry Source],case when TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail then 'YES' else 'No' end as IsDCSGenerated

                        from TSPL_MILK_COLLECTION_MCC_DETAIL
                        left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
                        left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code
                        left join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail =TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id
                        where convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' "
            dt = clsDBFuncationality.GetDataTable(Qry)
            Gv1.DataSource = Nothing
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
            Else
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.DataSource = dt
                Gv1.AutoExpandGroups = True
                Gv1.ShowGroupPanel = True
                Gv1.ShowRowHeaderColumn = False
                Gv1.AllowAddNewRow = False
                Gv1.AllowDeleteRow = False
                Gv1.EnableFiltering = True
                Gv1.ShowFilteringRow = True
                Gv1.BestFitColumns()
                FormatGridBMC()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        BMCData()
        DCSData()
    End Sub
    Private Sub DCSData()
        Try
            Dim dt As DataTable = Nothing
            Dim Qry As String = "SELECT  case when TSPL_VENDOR_MASTER.Status='N'  then TSPL_VENDOR_MASTER.Vendor_Code else 'NA' end as [DCS Code],Vendor_Name as [DCS Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VLC_Code_VLC_UploaderIN
                    ,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty,case when TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id <>'' and Against_Milk_Collection_MCC_Detail<>'' then 'Mobile' else 'ERP' end as [Entry Source]
                    FROM TSPL_MILK_COLLECTION_DCS_DETAIL 
                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code
                    left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
                    left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
                    inner join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No 
                    left join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail
                    where  convert(date,TSPL_MILK_COLLECTION_DCS.Document_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value) + "'"
            dt = clsDBFuncationality.GetDataTable(Qry)
            gv2.DataSource = Nothing
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv2.GroupDescriptors.Clear()
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                gv2.DataSource = dt
                gv2.AutoExpandGroups = True
                gv2.ShowGroupPanel = True
                gv2.ShowRowHeaderColumn = False
                gv2.AllowAddNewRow = False
                gv2.AllowDeleteRow = False
                gv2.EnableFiltering = True
                gv2.ShowFilteringRow = True
                gv2.BestFitColumns()
                FormatGrid()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Sub FormatGrid()

        gv2.AutoExpandGroups = True
        gv2.ShowGroupPanel = True
        gv2.ShowRowHeaderColumn = False
        gv2.AllowAddNewRow = False
        gv2.AllowDeleteRow = False
        gv2.EnableFiltering = True
        gv2.ShowFilteringRow = True


        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).BestFit()
        Next
        gv2.Columns("DCS Code").Name = "DCS Code"
        gv2.Columns("DCS Code").Width = 150

        gv2.Columns("DCS Name").HeaderText = "DCS Name"
        gv2.Columns("DCS Name").Width = 200
        gv2.Columns("DCS Name").IsVisible = True

        gv2.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Uploader Code"
        gv2.Columns("VLC_Code_VLC_Uploader").Width = 150
        gv2.Columns("VLC_Code_VLC_Uploader").IsVisible = False


        gv2.Columns("Vendor_Code").HeaderText = "DCS Code"
        gv2.Columns("Vendor_Code").Width = 150
        gv2.Columns("Vendor_Code").FormatString = ""

        gv2.Columns("VLC_Name").HeaderText = "DCS Name"
        gv2.Columns("VLC_Name").Width = 200
        gv2.Columns("VLC_Name").FormatString = ""

        gv2.Columns("VLC_Code_VLC_UploaderIN").HeaderText = "DCS Uploader Code"
        gv2.Columns("VLC_Code_VLC_UploaderIN").IsVisible = True
        gv2.Columns("VLC_Code_VLC_UploaderIN").Width = 150


        gv2.Columns("Qty").Width = 150
        gv2.Columns("Qty").FormatString = "{0:n2}"

        gv2.Columns("Entry Source").HeaderText = "Entry Source"
        gv2.Columns("Entry Source").IsVisible = True
        gv2.Columns("Entry Source").Width = 150

        gv2.ShowGroupPanel = True
        gv2.MasterTemplate.AutoExpandGroups = True
        Dim summaryRowItem As New GridViewSummaryRowItem()
        summaryRowItem.Add(New GridViewSummaryItem("QTY", "{0:n2}", GridAggregateFunction.Sum))

        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv2.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        ViewGv2()
    End Sub
    Sub FormatGridBMC()

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
        Gv1.Columns("BMC Code").Name = "BMC Code"
        Gv1.Columns("BMC Code").Width = 150

        Gv1.Columns("BMC Name").HeaderText = "BMC Name"
        Gv1.Columns("BMC Name").Width = 200
        Gv1.Columns("BMC Name").IsVisible = True

        Gv1.Columns("MCC Uploader Code").HeaderText = "MCC Uploader Code"
        Gv1.Columns("MCC Uploader Code").Width = 150
        Gv1.Columns("MCC Uploader Code").IsVisible = False


        Gv1.Columns("BMC CodeIN").HeaderText = "BMC Code"
        Gv1.Columns("BMC CodeIN").Width = 150
        Gv1.Columns("BMC CodeIN").FormatString = ""

        Gv1.Columns("BMC NameIN").HeaderText = "BMC Name"
        Gv1.Columns("BMC NameIN").Width = 200
        Gv1.Columns("BMC NameIN").FormatString = ""

        Gv1.Columns("MCC Uploader CodeIN").HeaderText = "MCC Uploader Code"
        Gv1.Columns("MCC Uploader CodeIN").IsVisible = True
        Gv1.Columns("MCC Uploader CodeIN").Width = 150

        Gv1.Columns("Qty").Width = 150
        Gv1.Columns("Qty").FormatString = "{0:n2}"

        Gv1.Columns("Entry Source").HeaderText = "Entry Source"
        Gv1.Columns("Entry Source").IsVisible = True
        Gv1.Columns("Entry Source").Width = 150

        Gv1.Columns("IsDCSGenerated").HeaderText = "Is DCS Generated "
        Gv1.Columns("IsDCSGenerated").IsVisible = True
        Gv1.Columns("IsDCSGenerated").Width = 150

        Gv1.ShowGroupPanel = True
        Gv1.MasterTemplate.AutoExpandGroups = True
        Dim summaryRowItem As New GridViewSummaryRowItem()
        summaryRowItem.Add(New GridViewSummaryItem("QTY", "{0:n2}", GridAggregateFunction.Sum))

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        View()
    End Sub
    Private Sub rptAbsentBMCDCS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        reset()
        SetUserMgmtNew()
    End Sub
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBMCDCSAbsent & "'"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Report Type : 'BMC' ")
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                Else
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Absent BMC/DCS Report", Gv1, arrHeader, "Absent BMC/DCS Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ExportGridGV2(ByVal exporter As EnumExportTo)
        Try
            If gv2.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBMCDCSAbsent & "'"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Report Type : 'DCS' ")
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv2, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv2, "", Me.Text, , arrHeader)
                Else
                    transportSql.applyExportTemplate(gv2, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(" Absent BMC/DCS Report ", gv2, arrHeader, " Absent BMC/DCS Report ", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
        ExportGridGV2(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
        ExportGridGV2(EnumExportTo.PDF)
    End Sub
    Sub View()

        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup("In Master"))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("BMC Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("BMC Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC Uploader Code").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("In Entry"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("BMC CodeIN").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("BMC NameIN").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("MCC Uploader CodeIN").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Quantity"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Qty").Name)



            view.ColumnGroups.Add(New GridViewColumnGroup("Entry Source"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Entry Source").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Is DCS Generated"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv1.Columns("IsDCSGenerated").Name)

            Gv1.ViewDefinition = view
        End If
    End Sub
    Sub ViewGv2()

        If gv2.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup("In Master"))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("DCS Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("DCS Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv2.Columns("VLC_Code_VLC_Uploader").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("In Entry"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv2.Columns("Vendor_Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv2.Columns("VLC_Name").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv2.Columns("VLC_Code_VLC_UploaderIN").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Quantity"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv2.Columns("Qty").Name)



            view.ColumnGroups.Add(New GridViewColumnGroup("Entry Source"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv2.Columns("Entry Source").Name)

            gv2.ViewDefinition = view
        End If
    End Sub
End Class