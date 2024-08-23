Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class RptSecondaryTransporterReport
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing


    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport

    End Sub
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub bindSummaryType()

        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add(1, "Transporter Wise")
        dt.Rows.Add(2, "MCC Wise")
        cbSummaryFor.DataSource = dt
        cbSummaryFor.ValueMember = "Code"
        cbSummaryFor.DisplayMember = "Name"
    End Sub

    Private Sub RptSecondaryTransporterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LOCATIONRIGTHS()
            SetUserMgmtNew()
            bindSummaryType()
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
            ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
            RadPageView1.SelectedPage = RadPageViewPage1
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub chkMCCAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If

    End Sub

    Sub Reset()
        Try

            RadPageView1.SelectedPage = RadPageViewPage1
            If chkMCCAll.IsChecked Then
                cbgMCC.CheckedAll()
            Else
                cbgMCC.UnCheckedAll()
            End If
            chkDetail.IsChecked = True
            LoadMCC()
            LoadPlant()
            LoadTanker()
            chkMCCAll.CheckState = CheckState.Checked
            chkAllRoute.CheckState = CheckState.Checked
            chkAllTransporter.CheckState = CheckState.Checked
            'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
            EnableDisableControl(True)
            gv.DataSource = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

    End Sub
    Sub LoadMCC()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then

            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Location_Code in (" + arrLoc + ") "

        Else
            btnGo.Enabled = False
        End If

        cbgMCC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgMCC.ValueMember = "Code"
        cbgMCC.DisplayMember = "Name"

    End Sub

    Sub LoadPlant()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then

            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Location_Code in (" + arrLoc + ") "

        Else
            btnGo.Enabled = False
        End If

        cbgPlantCode.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgPlantCode.ValueMember = "Code"
        cbgPlantCode.DisplayMember = "Name"

    End Sub
    Sub LoadTanker()
        Dim qry As String = Nothing
        qry = "Select TSPL_TANKER_MASTER.Tanker_No As Code,  TSPL_TANKER_MASTER.Tanker_Name As Name From TSPL_TANKER_MASTER "
        cbgTanker.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgTanker.ValueMember = "Code"
        cbgTanker.DisplayMember = "Name"
    End Sub

    Private Sub chkAllRoute_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAllRoute.ToggleStateChanged
        cbgPlantCode.Enabled = Not chkAllRoute.IsChecked
    End Sub

    Private Sub chkAllTransporter_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAllTransporter.ToggleStateChanged
        cbgTanker.Enabled = Not chkAllTransporter.IsChecked
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            btnReferesh = True
            PageSetupReport_ID = IIf(chkSummary.IsChecked = True, clsERPFuncationality.GetReportID(MyBase.Form_ID, cbSummaryFor.Text), MyBase.Form_ID + "D")
            TemplateGridview = gv
            GetReportID()
            LoadData()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If chkSummary.IsChecked Then
            VarID += "_S"
        ElseIf chkDetail.IsChecked Then
            VarID += "_D"
        End If
        gv.VarID = VarID
    End Sub

    Private Sub LoadData()
        If txtFromDate.Value > txtToDate.Value Then
            txtFromDate.Focus()
            Throw New Exception("From date can not be greater then to Date")
        End If

        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If
        If ChkSelectPlant.IsChecked AndAlso cbgPlantCode.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Plant or select all.", Me.Text)
            Exit Sub
        End If
        If ChkSelectTanker.IsChecked AndAlso cbgTanker.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Tanker or select all.", Me.Text)
            Exit Sub
        End If
        Dim companyADD, CompName, CompCode As String
        Dim qry As String
        Dim Qry1 As String = Nothing
        qry = ""
        qry += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        companyADD = dt1.Rows(0).Item("comp_address")

        qry = ""
        qry += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
        CompName = dt2.Rows(0).Item("Comp_Name")


        qry = ""
        qry += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(qry)
        CompCode = dt5.Rows(0).Item("Comp_Code")

        Dim fromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")

        Dim Todate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        qry = ""
        If chkSummary.IsChecked Then
            qry += " WITH TMP AS ("
        End If
        qry += " Select *, final.[Freight Amount ] + final.[Toll tax] + final.[Other Expenses] As [Bill Amount] From "
        qry += " (Select TSPL_MCC_Dispatch_Challan.Dispatch_Date, Convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) As [Dispatch Date], TSPL_MCC_Dispatch_Challan.MCC_Code As [MCC Code], TSPL_LOCATION_MASTER_MCC.Location_Desc As [Mcc Name], TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code As [Plant Code], TSPL_LOCATION_MASTER_Plant.Location_Desc As [Plant Name], Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Tanker Receiving date], TSPL_MCC_Dispatch_Challan.Tanker_No As [Tanker No], IsNull(TSPL_MCC_Dispatch_Challan.Net_Qty, 0) As [Dispatch Qty], IsNull(TSPL_Weighment_Detail.Net_Weight, 0) As [Receiving Qty], TSPL_MCC_Dispatch_Challan.Chalan_NO As [Chalan No], TSPL_MCC_Dispatch_Challan.Tanker_Transporter_Name As [Transporter Name], IsNull(tspl_location_distance_master.Distance, 0) As [K.M], IsNull(TSPL_TANKER_MASTER.Price_KM, 0) As [Rate/Km], IsNull(TSPL_MCC_Dispatch_Challan.Payment_Amount, 0) As [Freight Amount ], 0 As [Toll tax], 0 As [Other Expenses], 0 As [Insurance Amount], 0 As [Fat/ Snf Loss Amount], 0 As [Other Deduction], 0 As [Net Payable Amount ]    From TSPL_MCC_Dispatch_Challan Left Join TSPL_LOCATION_MASTER As TSPL_LOCATION_MASTER_MCC On TSPL_LOCATION_MASTER_MCC.Location_Code = TSPL_MCC_Dispatch_Challan.MCC_Code Left Join TSPL_LOCATION_MASTER As TSPL_LOCATION_MASTER_Plant On TSPL_LOCATION_MASTER_Plant.Location_Code = TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code Left Join Tspl_Gate_Entry_Details On Tspl_Gate_Entry_Details.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO Left Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO Left Join TSPL_TANKER_MASTER On TSPL_TANKER_MASTER.Tanker_No = TSPL_MCC_Dispatch_Challan.Tanker_No And TSPL_TANKER_MASTER.Status = 'Rate/K.M' Left Join tspl_location_distance_master On tspl_location_distance_master.From_Location_code = TSPL_MCC_Dispatch_Challan.MCC_Code And tspl_location_distance_master.to_Location_Code = TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code ) As final where ([Dispatch_Date] between '" + fromDate + "' and '" + Todate + "') "

        If cbgMCC.CheckedValue.Count > 0 AndAlso chkMCCSelect.IsChecked Then
            qry += " and [MCC Code]  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If
        If cbgPlantCode.CheckedValue.Count > 0 AndAlso ChkSelectPlant.IsChecked Then
            qry += " and [Plant Code]  IN (" + clsCommon.GetMulcallString(cbgPlantCode.CheckedValue) + ") "
        End If
        If cbgTanker.CheckedValue.Count > 0 > 0 AndAlso ChkSelectTanker.IsChecked Then
            qry += " and [Tanker No]  IN (" + clsCommon.GetMulcallString(cbgTanker.CheckedValue) + ") "
        End If

        If chkSummary.IsChecked Then
            Dim SummaryFor = String.Empty
            If (cbSummaryFor.SelectedIndex = 0) Then
                SummaryFor = " [Transporter Name] "
            ElseIf (cbSummaryFor.SelectedIndex = 1) Then
                SummaryFor = " [MCC Code],[Mcc Name] "
            End If
            qry += " ) "
            qry += " SELECT " + SummaryFor + " ,[Dispatch Qty]=ROUND(SUM([Dispatch Qty]),2),[Receiving Qty]=ROUND(SUM([Receiving Qty]),2),[Rate/Km]=ROUND(AVG([Rate/Km]),2),[Freight Amount ]=ROUND(SUM([Freight Amount ]),2), "
            qry += " [Toll tax]=ROUND(SUM([Toll tax]),2),[Other Expenses]=ROUND(SUM([Other Expenses]),2),[Insurance Amount]=ROUND(SUM([Insurance Amount]),2),[Fat/ Snf Loss Amount]=ROUND(SUM([Fat/ Snf Loss Amount]),2),[Other Deduction]=ROUND(SUM([Other Deduction]),2),[Net Payable Amount ]=ROUND(SUM([Net Payable Amount ]),2) "
            qry += " FROM TMP GROUP BY " + SummaryFor
        End If

        dt = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        gv.DataSource = dt
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        'FormatGrid()

        Dim summaryRowItem As New GridViewSummaryRowItem()
        gv.SummaryRowsBottom.Clear()
        For iii As Integer = 0 To gv.Columns.Count - 1
            If TypeOf (gv.Columns(iii)) Is GridViewDecimalColumn Then
                summaryRowItem.Add(New GridViewSummaryItem(gv.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
            End If
        Next
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        If chkDetail.IsChecked Then
            gv.Columns("Dispatch_Date").IsVisible = False
        End If
        gv.ReadOnly = True
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        End If
        RadPageView1.SelectedPage = RadPageViewPage2
        gv.BestFitColumns()
        ReStoreGridLayout()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptSecondaryTransporterReport & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                If chkMCCSelect.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgMCC.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgMCC.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add(("MCC Name: " + strMCCName + " "))
                End If

                If ChkSelectPlant.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgPlantCode.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgPlantCode.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add(("Plant Name: " + strMCCName + " "))
                End If


                If ChkSelectTanker.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgTanker.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgTanker.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add(("Tanker Name: " + strMCCName + " "))
                End If

                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Secondary Transporter Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No data to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub chkSummary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSummary.ToggleStateChanged
        ChkSummaryFor()
    End Sub

    Public Sub ChkSummaryFor()
        If chkSummary.IsChecked Then
            cbSummaryFor.Enabled = True
        Else
            cbSummaryFor.Enabled = False
        End If
    End Sub
    Private Sub chkDetail_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkDetail.ToggleStateChanged
        ChkSummaryFor()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
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
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
