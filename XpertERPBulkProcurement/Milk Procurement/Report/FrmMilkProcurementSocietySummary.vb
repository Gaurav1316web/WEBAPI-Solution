Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports System.IO


Public Class FrmMilkProcurementSocietySummary
    Inherits FrmMainTranScreen
#Region "Variables"
    'Dim dt As DataTable
    Dim settMaxFATPerLimit As Decimal = 0
    Dim settMaxSNFPerLimit As Decimal = 0
    Dim AreaWiseBilling As Boolean = False

#End Region

    Private Sub FrmMilkProcurementSocietySummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        settMaxFATPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimitforReport, clsFixedParameterCode.MaxFATPerLimitforReport, Nothing))
        settMaxSNFPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimitforReport, clsFixedParameterCode.MaxSNFPerLimitforReport, Nothing))
        AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)
        txtAreaCode.Visible = AreaWiseBilling
        lblBMC.Visible = AreaWiseBilling
        funreset()
    End Sub
    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Try
            Dim qry As String = " Select MCC_Code as Code,MCC_NAME,MCC_Area,Mcc_Code_VLC_Uploader from TSPL_MCC_MASTER where 2=2 "
            If txtAreaCode.Value IsNot Nothing AndAlso txtAreaCode.Value.Count > 0 Then
                qry += " and TSPL_MCC_MASTER.Area_Location_Code ='" + clsCommon.myCstr(txtAreaCode.Value) + "' "
            End If
            txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("BMCRoute", qry, "Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        EnableDisableControls(True)
        Gv1.DataSource = Nothing
        txtMCC.arrValueMember = Nothing
        txtArea.arrValueMember = Nothing
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = MyBase.Form_ID
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            common.clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Dim qry As String = ""
            Dim dt As DataTable
            qry = " Select *,(Good_Milk+Good_FailMilk+Sour_Milk+Curd_Milk)Total_Milk from (Select [VSP Code],max([Vlc Uploader Code])DCS_Code,max([VSP Name])DCS_Name,max(MCC)MCC_Code,max([MCC Name])MCC_Name ,
                    SUM(CASE WHEN RejectType = 'SWEET' THEN ISNULL([Milk Weight],0) ELSE 0 END) - SUM(ISNULL([Good FailMilk],0)) AS Good_Milk,
                    SUM([Good FailMilk]) AS Good_FailMilk,
                    SUM(CASE WHEN RejectType = 'SOUR'  THEN ISNULL([Milk Weight],0) ELSE 0 END) AS Sour_Milk,
                    SUM(CASE WHEN RejectType = 'CURD'  THEN ISNULL([Milk Weight],0) ELSE 0 END) AS Curd_Milk,
                    max(Comp_Name)Comp_Name,max(Add1)Add1
                    from (Select   Case When TSPL_MILK_SRN_DETAIL.FAT_PER < " & settMaxFATPerLimit & " or TSPL_MILK_SRN_DETAIL.SNF_PER < " & settMaxSNFPerLimit & " Then (TSPL_MILK_SRN_DETAIL.QTY) Else 0 End [Good FailMilk],TSPL_MILK_SRN_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],TSPL_LOCATION_MASTER.Location_Desc as[Area],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name],  Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date],  TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name],  TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_MILK_SRN_DETAIL.QTY As [Milk Weight], TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No],  Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type IS Null Then Case When TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type IS NUll Then 'SWEET' Else TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type End Else TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type End as RejectType  
                    ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1
                    from TSPL_MILK_SRN_DETAIL
                     Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE
                     Left Outer Join TSPL_MILK_SHIFT_UPLOADER_DETAIL On TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No   left outer join TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                     Left Outer Join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL ON TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No 
                     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code 
                     Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
                     Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  
                     Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE 
                     Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
                     Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE
                     left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
                     left outer join TSPL_BULK_ROUTE_MASTER On TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_SRN_HEAD.ROUTE_CODE 
                     Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE
                     left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code
                     left outer join TSPL_COMPANY_MASTER on 2 =2
                     where 2 = 2  and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) >='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'
                     and Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
            If clsCommon.myLen(txtMCC.arrValueMember) > 0 Then
                qry += "And TSPL_MILK_SRN_HEAD.mcc_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If
            If clsCommon.myLen(txtAreaCode.Value) > 0 Then
                qry += " And TSPL_MCC_MASTER.Area_Location_Code = '" + txtAreaCode.Value + "' "
            End If

            qry += " )XX  Group  by [VSP Code])XX "
            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterView.Refresh()
            If dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                Gv1.GroupDescriptors.Clear()
                Gv1.EnableFiltering = True
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                SetGridFormation()
                Gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.AllowAddNewRow = False
                Gv1.ShowGroupPanel = False
                EnableDisableControls(True)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormation()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
        Next

        Gv1.Columns("DCS_Code").HeaderText = "DCS Code"
        Gv1.Columns("DCS_Name").HeaderText = "DCS Name"
        Gv1.Columns("Good_Milk").HeaderText = "Good Milk"
        Gv1.Columns("Good_FailMilk").HeaderText = "Good Fail Milk"
        Gv1.Columns("Sour_Milk").HeaderText = "Sour Milk"
        Gv1.Columns("Curd_Milk").HeaderText = "Curd Milk"
        Gv1.Columns("MCC_Code").HeaderText = "MCC Code"
        Gv1.Columns("MCC_Name").HeaderText = "MCC Name"
        Gv1.Columns("Comp_Name").HeaderText = "Comp Name"
        Gv1.Columns("Comp_Name").IsVisible = False
        Gv1.Columns("Add1").HeaderText = "Add1"
        Gv1.Columns("Add1").IsVisible = False
        Gv1.Columns("Total_Milk").HeaderText = "Total Milk"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Good_Milk", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Sour_Milk", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item5 As New GridViewSummaryItem("Good_FailMilk", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item3 As New GridViewSummaryItem("Curd_Milk", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Total_Milk", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom


    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim qry As String = ""
            qry = " Select *,(Good_Milk+Good_FailMilk+Sour_Milk+Curd_Milk)Total_Milk,'" + clsCommon.GetPrintDate(txtFromDate.Value) + "' As FromDate,'" + clsCommon.GetPrintDate(txtToDate.Value) + "' As ToDate from (Select [VSP Code],max([Vlc Uploader Code])DCS_Code,max([VSP Name])DCS_Name,max(MCC)MCC_Code,max([MCC Name])MCC_Name ,
                    SUM(CASE WHEN RejectType = 'SWEET' THEN ISNULL([Milk Weight],0) ELSE 0 END) - SUM(ISNULL([Good FailMilk],0)) AS Good_Milk,
                    SUM([Good FailMilk]) AS Good_FailMilk,
                    SUM(CASE WHEN RejectType = 'SOUR'  THEN ISNULL([Milk Weight],0) ELSE 0 END) AS Sour_Milk,
                    SUM(CASE WHEN RejectType = 'CURD'  THEN ISNULL([Milk Weight],0) ELSE 0 END) AS Curd_Milk,
                    max(Comp_Name)Comp_Name,max(Add1)Add1
                    from (Select   Case When TSPL_MILK_SRN_DETAIL.FAT_PER < " & settMaxFATPerLimit & " or TSPL_MILK_SRN_DETAIL.SNF_PER < " & settMaxSNFPerLimit & " Then (TSPL_MILK_SRN_DETAIL.QTY) Else 0 End [Good FailMilk],TSPL_MILK_SRN_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],TSPL_LOCATION_MASTER.Location_Desc as[Area],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name],  Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date],  TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name],  TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_MILK_SRN_DETAIL.QTY As [Milk Weight], TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No],  Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type IS Null Then Case When TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type IS NUll Then 'SWEET' Else TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type End Else TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Type End as RejectType  
                    ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1
                    from TSPL_MILK_SRN_DETAIL
                     Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE
                     Left Outer Join TSPL_MILK_SHIFT_UPLOADER_DETAIL On TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No   left outer join TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_HEAD.Document_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                     Left Outer Join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL ON TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No 
                     left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code 
                     Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
                     Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  
                     Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE 
                     Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
                     Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE
                     left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
                     left outer join TSPL_BULK_ROUTE_MASTER On TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_SRN_HEAD.ROUTE_CODE 
                     Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE
                     left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code
                     left outer join TSPL_COMPANY_MASTER on 2 =2
                     where 2 = 2  and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) >='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'
                     and Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "

            If clsCommon.myLen(txtMCC.arrValueMember) > 0 Then
                qry += "And TSPL_MILK_SRN_HEAD.mcc_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If
            If clsCommon.myLen(txtAreaCode.Value) > 0 Then
                qry += " And TSPL_MCC_MASTER.Area_Location_Code = '" + txtAreaCode.Value + "' "
            End If

            qry += " )XX  Group  by [VSP Code])XX "

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt2.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dt2, "rptMilkProcurementSocietySummary", "Sale Order")
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim strTemp As String = ""
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmMilkProcurementSocietySummary & "'"))
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                Else
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtAreaCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtAreaCode._MYValidating
        Try
            Dim sQuery As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc, Type from TSPL_LOCATION_MASTER "
            txtAreaCode.Value = clsCommon.ShowSelectForm("Location@Plant@Master", sQuery, "Code", "TSPL_LOCATION_MASTER.Type <> 'PLANT' OR TSPL_LOCATION_MASTER.Location_Category <> 'Mcc'", txtAreaCode.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub
End Class