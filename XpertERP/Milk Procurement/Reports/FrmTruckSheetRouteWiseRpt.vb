Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmTruckSheetRouteWiseRpt
    Inherits FrmMainTranScreen
#Region "Varaibles"

    Const colSno As String = "colSno"
    Const colDocNo As String = "colDocNo"
    Const colVLCCode As String = "colVLCCode"
    Const colDate As String = "colDate"
    Const colRouteName As String = "colRouteName"
    Const colVlcName As String = "colVlcName"
    Const colMccQty As String = "colMccQty"
    Const colShift As String = "colShift"
    Const colsnf As String = "colsnf"
    Const colFAT As String = "colFAT"
    Const colAmount As String = "colAmount"

#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmTruckSheetRouteWiseRpt)
        btnExp.Visible = MyBase.isExport
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
   Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= dgvreport.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To dgvreport.Columns.Count - 1 Step ii + 1
                        dgvreport.Columns(ii).IsVisible = False
                        dgvreport.Columns(ii).VisibleInColumnChooser = True
                    Next
                    dgvreport.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Function RetQuery() As String
        Dim strQuery As String = Nothing
        Dim strwhrcls As String = Nothing

        strQuery = "select MAX(TSPL_MILK_RECEIPT_HEAD.DOC_CODE) AS [Document No],CONVERT(VARCHAR(15),MAX(TSPL_MILK_RECEIPT_HEAD.DOC_DATE),103) AS [DATE],MAX(TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE) AS [ROUTE CODE],MAX(TSPL_MCC_ROUTE_MASTER.Route_Name) AS [ROUTE NAME], MAX(TSPL_MILK_RECEIPT_DETAIL.VLC_CODE) AS [VLC CODE],max(TSPL_VLC_MASTER_HEAD.VLC_Name) as VLC_NAME,MAX(TSPL_MILK_RECEIPT_DETAIL.SHIFT) AS SHIFT,max(TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT) AS [MILK QTY IN LTRS], MAX(TSPL_MILK_SAMPLE_DETAIL.FAT) AS [FAT %],MAX(TSPL_MILK_SAMPLE_DETAIL.SNF) AS [SNF %],max(TSPL_MILK_SRN_DETAIL.NET_AMOUNT) AS [PRODUCER VALUE (RS)]   from TSPL_MILK_RECEIPT_HEAD "
        strQuery += " left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE=TSPL_MILK_RECEIPT_HEAD.DOC_CODE "
        strQuery += " left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_head.MILK_RECEIPT_CODE=TSPL_MILK_RECEIPT_HEAD.DOC_CODE "
        strQuery += " left outer join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE "
        strQuery += " and TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO=TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO"
        strQuery += " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE "
        strQuery += " left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE and TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE=TSPL_MILK_SRN_HEAD.ROUTE_CODE "
        strQuery += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE=TSPL_MILK_SRN_HEAD.VLC_CODE "
        strQuery += " LEFT JOIN TSPL_MCC_ROUTE_MASTER ON TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE=TSPL_MCC_ROUTE_MASTER.Route_Code "
        strQuery += " Left join TSPL_VLC_MASTER_HEAD on TSPL_MILK_RECEIPT_DETAIL.VLC_CODE=TSPL_VLC_MASTER_HEAD.VLC_Code  "

        strQuery += " where 1=1 "
        If txtMccCode.arrValueMember IsNot Nothing AndAlso txtMccCode.arrValueMember.Count > 0 Then
            strwhrcls = " and TSPL_MILK_RECEIPT_DETAIL.MCC_CODE IN (" + clsCommon.GetMulcallString(txtMccCode.arrValueMember) + ")  "
        Else
            strwhrcls = "and TSPL_MILK_RECEIPT_DETAIL.MCC_CODE=TSPL_MILK_RECEIPT_DETAIL.MCC_CODE  "
        End If
        If txtRouteCode.arrValueMember IsNot Nothing AndAlso txtRouteCode.arrValueMember.Count > 0 Then
            strwhrcls += "AND TSPL_MILK_RECEIPT_DETAIL.Route_Code IN(" + clsCommon.GetMulcallString(txtRouteCode.arrValueMember) + ") "
        Else
            strwhrcls += "and TSPL_MILK_RECEIPT_DETAIL.Route_Code=TSPL_MILK_RECEIPT_DETAIL.Route_Code "
        End If
        If txtVlcCode.arrValueMember IsNot Nothing AndAlso txtVlcCode.arrValueMember.Count > 0 Then
            strwhrcls += " And TSPL_MILK_RECEIPT_DETAIL.VLC_CODE IN(" + clsCommon.GetMulcallString(txtVlcCode.arrValueMember) + ")  "
        Else
            strwhrcls += "and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE "
        End If
        If clsCommon.myLen(cboShift.SelectedValue) > 0 Then
            strwhrcls += " and TSPL_MILK_RECEIPT_DETAIL.SHIFT = '" + clsCommon.myCstr(cboShift.SelectedValue) + "' "
        Else
            strwhrcls += "and TSPL_MILK_RECEIPT_DETAIL.SHIFT=TSPL_MILK_RECEIPT_DETAIL.SHIFT  "
        End If

        strwhrcls += "and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)>=convert(date,'" & clsCommon.myCDate(DtpDocfDate.Value) & "',103) and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)<=convert(date,'" & clsCommon.myCDate(dtDoctToDate.Value) & "',103) GROUP BY TSPL_MILK_RECEIPT_DETAIL.VLC_CODE"

        strQuery += strwhrcls
        Return strQuery
    End Function

    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = dgvreport
            dgvreport.DataSource = Nothing
            dgvreport.Columns.Clear()
            dgvreport.Rows.Clear()
            dgvreport.GroupDescriptors.Clear()
            dgvreport.MasterTemplate.SummaryRowsBottom.Clear()
            dgvreport.ShowGroupPanel = False
            dgvreport.EnableFiltering = True

            Dim strQuery As String = Nothing
            strQuery = RetQuery()

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            If dt.Rows.Count > 0 Then
                dgvreport.DataSource = dt
                SetGridFormationOFGV1()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else

                clsCommon.MyMessageBoxShow(Me, "No Data Found..", Me.Text)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        dgvreport.GroupDescriptors.Clear()
        dgvreport.TableElement.TableHeaderHeight = 40
        dgvreport.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To dgvreport.Columns.Count - 1
            dgvreport.Columns(ii).ReadOnly = True
            dgvreport.Columns(ii).IsVisible = False
        Next
        dgvreport.Columns("Document No").IsVisible = False
        dgvreport.Columns("Document No").Width = 100
        dgvreport.Columns("Document No").HeaderText = "Document No"

        dgvreport.Columns("DATE").IsVisible = True
        dgvreport.Columns("DATE").Width = 100
        dgvreport.Columns("DATE").HeaderText = "Date"

        dgvreport.Columns("ROUTE CODE").IsVisible = True
        dgvreport.Columns("ROUTE CODE").Width = 100
        dgvreport.Columns("ROUTE CODE").HeaderText = "Route Code"

        dgvreport.Columns("ROUTE NAME").IsVisible = True
        dgvreport.Columns("ROUTE NAME").Width = 100
        dgvreport.Columns("ROUTE NAME").HeaderText = "Route Name"

        dgvreport.Columns("VLC CODE").IsVisible = True
        dgvreport.Columns("VLC CODE").Width = 100
        dgvreport.Columns("VLC CODE").HeaderText = "VLC Code"

        dgvreport.Columns("VLC_NAME").IsVisible = True
        dgvreport.Columns("VLC_NAME").Width = 100
        dgvreport.Columns("VLC_NAME").HeaderText = "VLC Name"

        dgvreport.Columns("SHIFT").IsVisible = True
        dgvreport.Columns("SHIFT").Width = 100
        dgvreport.Columns("SHIFT").HeaderText = "SHIFT"

        dgvreport.Columns("MILK QTY IN LTRS").IsVisible = True
        dgvreport.Columns("MILK QTY IN LTRS").Width = 100
        dgvreport.Columns("MILK QTY IN LTRS").HeaderText = "Milk Qty in (Ltr)"

        dgvreport.Columns("FAT %").IsVisible = True
        dgvreport.Columns("FAT %").Width = 100
        dgvreport.Columns("FAT %").HeaderText = "Fat %"

        dgvreport.Columns("SNF %").IsVisible = True
        dgvreport.Columns("SNF %").Width = 100
        dgvreport.Columns("SNF %").HeaderText = "SNF %"

        dgvreport.Columns("PRODUCER VALUE (RS)").IsVisible = True
        dgvreport.Columns("PRODUCER VALUE (RS)").Width = 100
        dgvreport.Columns("PRODUCER VALUE (RS)").HeaderText = "PRODUCER VALUE (RS)"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim Smitem As New GridViewSummaryItem("MILK QTY IN LTRS", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem)
        Dim Smitem1 As New GridViewSummaryItem("PRODUCER VALUE (RS)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Smitem1)
        dgvreport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        dgvreport.BestFitColumns()

    End Sub
    Public Sub LoadBlankGrid()
        dgvreport.Rows.Clear()
        dgvreport.Columns.Clear()

        Dim SNO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SNO.FormatString = ""
        SNO.HeaderText = "S.NO"
        SNO.Name = colSno
        SNO.Width = 600
        SNO.IsVisible = False
        SNO.ReadOnly = True
        dgvreport.MasterTemplate.Columns.Add(SNO)

        Dim DocNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DocNo.FormatString = ""
        DocNo.HeaderText = "S.NO"
        DocNo.Name = colDocNo
        DocNo.Width = 600
        DocNo.IsVisible = False
        DocNo.ReadOnly = True
        dgvreport.MasterTemplate.Columns.Add(DocNo)

        Dim clDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        clDate.FormatString = ""
        clDate.HeaderText = "DATE"
        clDate.Name = colDate
        clDate.Width = 350
        clDate.IsVisible = False
        clDate.ReadOnly = True
        dgvreport.MasterTemplate.Columns.Add(clDate)

        Dim clRouteNae As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        clRouteNae.FormatString = ""
        clRouteNae.HeaderText = "ROUTE NAME"
        clRouteNae.Name = colRouteName
        clRouteNae.Width = 600
        clRouteNae.IsVisible = False
        clRouteNae.ReadOnly = True
        dgvreport.MasterTemplate.Columns.Add(clRouteNae)


        Dim clvlcCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        clvlcCode.FormatString = ""
        clvlcCode.HeaderText = "VLC CODE"
        clvlcCode.Name = colVLCCode
        clvlcCode.Width = 600
        clvlcCode.IsVisible = False
        clvlcCode.ReadOnly = True
        dgvreport.MasterTemplate.Columns.Add(clvlcCode)


        Dim clVlcName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        clVlcName.FormatString = ""
        clVlcName.HeaderText = "VLC NAME"
        clVlcName.Name = colVlcName
        clVlcName.Width = 600
        clVlcName.IsVisible = False
        clVlcName.ReadOnly = True
        dgvreport.MasterTemplate.Columns.Add(clVlcName)


        Dim clShift As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        clShift.FormatString = ""
        clShift.HeaderText = "SHIFT"
        clShift.Name = colShift
        clShift.Width = 200
        clShift.IsVisible = False
        clShift.ReadOnly = True
        dgvreport.MasterTemplate.Columns.Add(clShift)

        Dim clMilkQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        clMilkQty.FormatString = ""
        clMilkQty.HeaderText = "MILK QTY IN LTRS"
        clMilkQty.Name = colMccQty
        clMilkQty.Width = 350
        clMilkQty.IsVisible = False
        clMilkQty.ReadOnly = True
        dgvreport.MasterTemplate.Columns.Add(clMilkQty)

        Dim clFAT As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        clFAT.FormatString = ""
        clFAT.HeaderText = "FAT %"
        clFAT.Name = colFAT
        clFAT.Width = 300
        clFAT.IsVisible = False
        clFAT.ReadOnly = True
        dgvreport.MasterTemplate.Columns.Add(clFAT)

        Dim clSNF As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        clSNF.FormatString = ""
        clSNF.HeaderText = "SNF %"
        clSNF.Name = colsnf
        clSNF.Width = 300
        clSNF.IsVisible = False
        clSNF.ReadOnly = True
        dgvreport.MasterTemplate.Columns.Add(clSNF)

        Dim clAmount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        clAmount.FormatString = ""
        clAmount.HeaderText = "PRODUCER VALUE (RS)"
        clAmount.Name = colAmount
        clAmount.Width = 400
        clAmount.IsVisible = False
        clAmount.ReadOnly = True
        dgvreport.MasterTemplate.Columns.Add(clAmount)

        dgvreport.AllowDeleteRow = False
        dgvreport.ShowGroupPanel = False
        dgvreport.AllowColumnReorder = True
        dgvreport.AllowRowReorder = False
        dgvreport.AllowAddNewRow = False
        dgvreport.EnableSorting = True
        dgvreport.EnableFiltering = True
        dgvreport.EnableAlternatingRowColor = True
        dgvreport.AutoSizeRows = False
        dgvreport.AllowRowResize = True
        dgvreport.VerticalScrollState = ScrollState.AlwaysShow
        dgvreport.HorizontalScrollState = ScrollState.AlwaysShow
        dgvreport.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvreport.MasterTemplate.ShowRowHeaderColumn = False
        dgvreport.TableElement.TableHeaderHeight = 40
        dgvreport.ShowFilteringRow = True
        'dgvreport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Public Sub GetshiftType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        cboShift.DataSource = dt
        cboShift.ValueMember = "Code"
        cboShift.DisplayMember = "Name"
    End Sub

    Private Sub FrmTruckSheetRouteWiseRpt_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        GetshiftType()
        LoadBlankGrid()
    End Sub

    Private Sub txtMccCode__My_Click(sender As Object, e As EventArgs) Handles txtMccCode._My_Click
        Try
            Dim qry As String = "select Location_Code as Location,Location_Desc as Description  from TSPL_LOCATION_MASTER "
            txtMccCode.arrValueMember = clsCommon.ShowMultipleSelectForm("LocatMast", qry, "Location", "Description", txtMccCode.arrValueMember, txtMccCode.arrDispalyMember)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtRouteCode__My_Click(sender As Object, e As EventArgs) Handles txtRouteCode._My_Click
        Dim qry As String = "select Distinct TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as Description from TSPL_MCC_ROUTE_MASTER "
        txtRouteCode.arrValueMember = clsCommon.ShowMultipleSelectForm("Routed", qry, "Code", "Description", txtRouteCode.arrValueMember, txtRouteCode.arrDispalyMember)
    End Sub

    Private Sub txtVlcCode__My_Click(sender As Object, e As EventArgs) Handles txtVlcCode._My_Click
        Dim qry As String = "select TSPL_VLC_MASTER_HEAD.vlc_code as Code,TSPL_VLC_MASTER_HEAD.vlc_name as Name,TSPL_VLC_MASTER_HEAD.vehical_name as [Vehical Name],TSPL_VLC_MASTER_HEAD.vsp_code as [VSP Code],TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.mcc as [MCC Code],TSPL_MCC_MASTER.mcc_name as [MCC Name],TSPL_VLC_MASTER_HEAD.created_by as [Created By],TSPL_VLC_MASTER_HEAD.created_date as [Created Date],TSPL_VLC_MASTER_HEAD.modified_by as [Modified By],TSPL_VLC_MASTER_HEAD.modified_date as [Modified Date] from TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code and TSPL_VENDOR_MASTER.Form_Type='VSP' left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc"
        txtVlcCode.arrValueMember = clsCommon.ShowMultipleSelectForm("VLCMast", qry, "Code", "Name", txtVlcCode.arrValueMember, txtVlcCode.arrDispalyMember)
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If dgvreport.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(DtpDocfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtDoctToDate.Value, "dd/MM/yyyy")) + " ")
                If txtMccCode.arrValueMember IsNot Nothing AndAlso txtMccCode.arrValueMember.Count > 0 Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In txtMccCode.arrDispalyMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In txtMccCode.arrValueMember
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add(("MCC Name: " + strMCCName + " "))
                End If
                If txtRouteCode.arrValueMember IsNot Nothing AndAlso txtRouteCode.arrValueMember.Count > 0 Then
                    Dim strRouteName As String = ""
                    For Each StrRName As String In txtRouteCode.arrDispalyMember
                        If clsCommon.myLen(strRouteName) > 0 Then
                            strRouteName += ", "
                        End If
                        strRouteName += StrRName
                    Next
                    Dim strRouteCode As String = ""
                    For Each StrRCode As String In txtRouteCode.arrValueMember
                        If clsCommon.myLen(strRouteCode) > 0 Then
                            strRouteCode += ", "
                        End If
                        strRouteCode += StrRCode
                    Next
                    arrHeader.Add(("Route Name: " + strRouteName + " "))
                End If
                If txtVlcCode.arrValueMember IsNot Nothing AndAlso txtVlcCode.arrValueMember.Count > 0 Then
                    Dim strVLCName As String = ""
                    For Each StrVName As String In txtVlcCode.arrDispalyMember
                        If clsCommon.myLen(strVLCName) > 0 Then
                            strVLCName += ", "
                        End If
                        strVLCName += StrVName
                    Next
                    Dim strVLCCode As String = ""
                    For Each StrVCode As String In txtRouteCode.arrValueMember
                        If clsCommon.myLen(strVLCCode) > 0 Then
                            strVLCCode += ", "
                        End If
                        strVLCCode += StrVCode
                    Next
                    arrHeader.Add(("VLC Name: " + strVLCCode + " "))
                End If
                If clsCommon.myLen(cboShift.SelectedValue) > 0 Then
                    arrHeader.Add(("SHIFT: " + cboShift.Text + " "))
                End If
                transportSql.applyExportTemplate(dgvreport, PageSetupReport_ID)
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("TRUCK SHEET FORMAT ROUTE VISE", dgvreport, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF("TRUCK SHEET FORMAT ROUTE VISE", dgvreport, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Sub Reset()
        dgvreport.DataSource = Nothing
        dgvreport.Columns.Clear()
        dgvreport.Rows.Clear()
        dgvreport.GroupDescriptors.Clear()
        txtRouteCode.arrValueMember = Nothing
        txtMccCode.arrValueMember = Nothing
        txtVlcCode.arrValueMember = Nothing
        DtpDocfDate.Text = clsCommon.GETSERVERDATE
        dtDoctToDate.Text = clsCommon.GETSERVERDATE
        RadPageView1.SelectedPage = RadPageViewPage1
        'ReStoreGridLayout()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Dim strQuery As String = Nothing
        strQuery = RetQuery()

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "RptTruckSheetRouteWise", "Truck Sheet")
            frmCRV = Nothing
        End If

    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            dgvreport.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            dgvreport.SaveLayout(obj.GridLayout)
            obj.GridColumns = dgvreport.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        'ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
